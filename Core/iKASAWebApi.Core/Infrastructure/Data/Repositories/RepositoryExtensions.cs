namespace KMUH.iKASAWebApi.Infrastructure.Data.Repositories
{
#if include_gsqlparser
	using System;
	using System.Data;
	using System.Linq;
	using System.Collections.Generic;
	using System.Reflection;
	using gudusoft.gsqlparser;
	using AppFramework.Infrastructure.Data.Repositories;

	public enum DbVendor
    {
        DbVAccess = 3,
        DbVDB2 = 5,
        DbVfirebird = 9,
        DbVGeneric = 4,
        DbVInformix = 7,
        DbVMdx = 10,
        DbVMssql = 0,
        DbVMysql = 2,
        DbVOracle = 1,
        DbVPostgresql = 8,
        DbVSybase = 6
    }

	public static class RepositoryExtensions
	{
        private static readonly string sqlFieldName = "sql";
        private static readonly string parametersFieldName = "parameters";
		private static string prefix = "@prefix";
		private static DbVendor _dbVendor = DbVendor.DbVOracle;

		private static TCustomSqlStatement ParseSql(string sql, DbVendor dbVendor)
		{
			var t1 = DateTime.Now;
			var sqlparser = new TGSqlParser(((TDbVendor)dbVendor));
			sqlparser.SqlText.Text = sql;
			int ret = sqlparser.Parse();
			var t2 = DateTime.Now;
			System.Diagnostics.Debug.WriteLine(string.Format("ParseSql():{0}", (t2 - t1).TotalMilliseconds));
			if (ret != 0)
			{
				throw new Exception("SQL Parser Error:" + sqlparser.ErrorMessages + "\r\n" + sqlparser.SqlText.Text);
			}
			var sqlStatement = sqlparser.SqlStatements.First();
			switch (dbVendor)
            {
                case DbVendor.DbVOracle:
                    prefix = ":";
                    break;
                default:
                    prefix = "@";
                    break;
            }
			return sqlStatement;
		}
		
		public static string FixWhereStatement(this string sql, params System.Data.Common.DbParameter[] parameters)
        {
            var removedVars = parameters.Cast<System.Data.Common.DbParameter>().Select(c => c.ParameterName).ToArray();
            return FixWhereStatement(sql, _dbVendor, false, removedVars);
        }
		
		public static string FixWhereStatement(this string sql, bool include, params System.Data.Common.DbParameter[] parameters)
        {
            var removedVars = parameters.Cast<System.Data.Common.DbParameter>().Select(c => c.ParameterName).ToArray();
            return FixWhereStatement(sql, _dbVendor, include, removedVars);
        }

        public static string FixWhereStatement(this string sql, params string[] removedVars)
        {
            return FixWhereStatement(sql, _dbVendor, true, removedVars);
        }

        public static string FixWhereStatement(this string sql, bool include, params string[] removedVars)
        {
            return FixWhereStatement(sql, _dbVendor, include, removedVars);
        }

        public static string FixWhereStatement(this string sql, DbVendor dbVendor, bool include, params string[] removedVars)
        {
            var t1 = DateTime.Now;
            var sqlStatement = ParseSql(sql, dbVendor);

            if (sqlStatement.SqlStatementType != TSqlStatementType.sstSelect)
                return "";

            if (sqlStatement.WhereClause == null)
                return "";

            if (sqlStatement.Params.Count() == 0 && sqlStatement.SqlVars.Count() == 0)
                return "";

            if (!include)
            {
                var parameters = new List<string>();
                var iterator = sqlStatement.Params.GetEnumerator();
                while (iterator.MoveNext())
                {
                    var item = iterator.Current;
                    parameters.Add(item.SourceToken.SourceCode.Replace(prefix, ""));
                }
                iterator = sqlStatement.SqlVars.GetEnumerator();
                while (iterator.MoveNext())
                {
                    var item = iterator.Current;
                    parameters.Add(item.SourceToken.SourceCode.Replace(prefix, ""));
                }
                removedVars = parameters.Except(removedVars, StringComparer.InvariantCultureIgnoreCase).ToArray();
            }

            foreach (var removedVar in removedVars)
            {
                TSourceToken start = sqlStatement.WhereClause.StartToken;
                TSourceToken end = sqlStatement.WhereClause.EndToken;
                TSourceTokenList stlist = start.Container;
                for (int k = start.posinlist; k <= end.posinlist; k++)
                {
                    if (stlist[k].TokenType == TTokenType.ttBindVar || stlist[k].TokenType == TTokenType.ttSqlVar)
                    {
                        if (stlist[k].TokenStatus == TTokenStatus.tsDeleted)
                            continue;

                        if (stlist[k].SourceCode.Replace(prefix, "").Equals(removedVar, StringComparison.InvariantCultureIgnoreCase))
                        {
                            stlist[k].TokenType = TTokenType.ttSqlVar;
                            stlist[k].TokenStatus = TTokenStatus.tsDeleted;
                        }
                    }
                }
            }

            // rebuild the sql text
            sqlStatement.ReBuildSql(TRebuildFlags.rfClearRemovedVars);
            sql = sqlStatement.SqlDesc;
            var t2 = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(string.Format("FixWhereStatement():{0}", (t2 - t1).TotalMilliseconds));
            return sql;
        }


        public static string FixWhereStatement(this string sql, string whereStatement, bool replaceWhereStatement)
        {
            return FixWhereStatement(sql, _dbVendor, whereStatement, replaceWhereStatement);
        }		
		

        public static string FixWhereStatement(this string sql, DbVendor dbVendor, string whereStatement, bool replaceWhereStatement)
		{
            var t1 = DateTime.Now;
            var sqlStatement = ParseSql(sql, dbVendor);

            if (sqlStatement.SqlStatementType == TSqlStatementType.sstSelect)
            {
                sqlStatement.WhereClauseText = replaceWhereStatement ? whereStatement : sqlStatement.WhereClauseText + whereStatement;
                sql = sqlStatement.SqlDesc;
            }
            else
                sql = "";

            var t2 = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(string.Format("FixWhereStatement():{0}", (t2 - t1).TotalMilliseconds));
            return sql;
		}
		
		public static void FixWhereStatement<TEntity>(this IRepository<TEntity> repository)
        {
            FixWhereStatement<TEntity>(repository, _dbVendor);
        }
		
		public static void FixWhereStatement<TEntity>(this IRepository<TEntity> repository, DbVendor dbVendor)
        {
            var t1 = DateTime.Now;

            var sql = repository.GetType()
                .GetField(sqlFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(repository).ToString();

            var parameters = (List<Telerik.OpenAccess.Data.Common.OAParameter>)repository.GetType()
                .GetField(parametersFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(repository);

            var sqlStatement = ParseSql(sql, dbVendor);

            if (sqlStatement.SqlStatementType != TSqlStatementType.sstSelect)
                return;

            if (sqlStatement.WhereClause == null)
                return;

            if (sqlStatement.Params.Count() == 0 && sqlStatement.SqlVars.Count() == 0)
                return;

            var properties = repository.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (property.GetValue(repository, null) != null)
                    continue;

                TSourceToken start = sqlStatement.WhereClause.StartToken;
                TSourceToken end = sqlStatement.WhereClause.EndToken;
                TSourceTokenList stlist = start.Container;
                for (int k = start.posinlist; k <= end.posinlist; k++)
                {
                    if (stlist[k].TokenType == TTokenType.ttBindVar || stlist[k].TokenType == TTokenType.ttSqlVar)
                    {
                        if (stlist[k].TokenStatus == TTokenStatus.tsDeleted)
                            continue;

                        if (stlist[k].SourceCode.Replace(prefix, "").Equals(property.Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            stlist[k].TokenType = TTokenType.ttSqlVar;
                            stlist[k].TokenStatus = TTokenStatus.tsDeleted;
                            parameters.Remove(parameters.Where(c => c.ParameterName == property.Name).FirstOrDefault());
                        }
                    }
                }
            }
            // rebuild the sql text
            var t3 = DateTime.Now;
            sqlStatement.ReBuildSql(TRebuildFlags.rfClearRemovedVars);
            sql = sqlStatement.SqlDesc;
            repository.GetType()
                .GetField(sqlFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(repository, sql);
            var t4 = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(string.Format("ReBuildSql():{0}", (t4 - t3).TotalMilliseconds));

            var t2 = DateTime.Now;
            System.Diagnostics.Debug.WriteLine(string.Format("FixWhereStatement():{0}", (t2 - t1).TotalMilliseconds));
        }

        public static void FixWhereStatement<T>(this IRepository<T> repository, params string[] removedVars)
        {
            FixWhereStatement<T>(repository, _dbVendor, removedVars);
        }

        public static void FixWhereStatement<T>(this IRepository<T> repository, DbVendor dbVendor, params string[] removedVars)
        {
            var sql = repository.GetType()
                .GetField(sqlFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(repository).ToString();

            var parameters = (List<Telerik.OpenAccess.Data.Common.OAParameter>)repository.GetType()
                .GetField(parametersFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(repository);

            var sqlStatement = ParseSql(sql, dbVendor);

            if (sqlStatement.SqlStatementType != TSqlStatementType.sstSelect)
                return;

            if (sqlStatement.WhereClause == null)
                return;

            if (sqlStatement.Params.Count() == 0 && sqlStatement.SqlVars.Count() == 0)
                return;

            var properties = repository.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (!removedVars.Contains(property.Name))
                    continue;

                TSourceToken start = sqlStatement.WhereClause.StartToken;
                TSourceToken end = sqlStatement.WhereClause.EndToken;
                TSourceTokenList stlist = start.Container;
                for (int k = start.posinlist; k <= end.posinlist; k++)
                {
                    if (stlist[k].TokenType == TTokenType.ttBindVar || stlist[k].TokenType == TTokenType.ttSqlVar)
                    {
                        if (stlist[k].TokenStatus == TTokenStatus.tsDeleted)
                            continue;

                        if (stlist[k].SourceCode.Replace(prefix, "").Equals(property.Name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            stlist[k].TokenType = TTokenType.ttSqlVar;
                            stlist[k].TokenStatus = TTokenStatus.tsDeleted;
                            parameters.Remove(parameters.Where(c => c.ParameterName == property.Name).FirstOrDefault());
                        }
                    }
                }
            }
            // rebuild the sql text
            sqlStatement.ReBuildSql(TRebuildFlags.rfClearRemovedVars);
            sql = sqlStatement.SqlDesc;
            repository.GetType()
                .GetField(sqlFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(repository, sql);
        }

        public static void FixWhereStatement<T>(this IRepository<T> repository, string whereStatement, bool replaceWhereStatement)
        {
            FixWhereStatement<T>(repository, _dbVendor, whereStatement, replaceWhereStatement);
        }

        public static void FixWhereStatement<T>(this IRepository<T> repository, DbVendor dbVendor, string whereStatement, bool replaceWhereStatement)
        {
            var sql = repository.GetType()
                .GetField(sqlFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(repository).ToString();

            var sqlStatement = ParseSql(sql, dbVendor);

            if (sqlStatement.SqlStatementType != TSqlStatementType.sstSelect)
                return;

            sqlStatement.WhereClauseText = replaceWhereStatement ? whereStatement : sqlStatement.WhereClauseText + whereStatement;
            sql = sqlStatement.SqlDesc;
            repository.GetType()
                .GetField(sqlFieldName, BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(repository, sql);
        }
	}
#endif
}