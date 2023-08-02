using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using AppFramework.DomainModel.Entities.Converter;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Telerik.OpenAccess.Metadata.Relational;


namespace KMUH.iKASAWebApi.DomainModel.Entities
{
    public partial class iKASAWebApiContext
    {


        private static void InitMetaColumnConvert(Telerik.OpenAccess.Metadata.MetadataContainer metadataContainer,
            string tableName, string columnName, Type convertType)
        {
            MetaTable metaTable = metadataContainer.Tables.FirstOrDefault(x => x.Name.Equals(tableName));
            if (metaTable != null)
            {
                MetaColumn metaColumn = metaTable.Columns.FirstOrDefault(x => x.Name.Equals(columnName));
				if (convertType == null)
					metaColumn.Converter = null;
				else
					metaColumn.Converter = convertType.FullName;
            }
        }
		
		private static void ClearMetaColumnConvert(Telerik.OpenAccess.Metadata.MetadataContainer metadataContainer,
            string tableName, string columnName)
        {
            MetaTable metaTable = metadataContainer.Tables.FirstOrDefault(x => x.Name.Equals(tableName));
            if (metaTable != null)
            {
                MetaColumn metaColumn = metaTable.Columns.FirstOrDefault(x => x.Name.Equals(columnName));
                metaColumn.Converter = null;
            }
        }

        protected override void OnDatabaseOpen(BackendConfiguration backendConfiguration, MetadataContainer metadataContainer, MetadataContainer aggregatedMetadataContainer)
        {
            base.OnDatabaseOpen(backendConfiguration, metadataContainer, aggregatedMetadataContainer);

            //Init YNToBooleanconverter
            //var primitiveMembers = from p in metadataContainer.PersistentTypes
            //                       from m in p.Members
            //                       where m.MemberType is MetaPrimitiveType &&
            //                        (((MetaPrimitiveType)m.MemberType).ClrType == typeof(bool) || ((MetaPrimitiveType)m.MemberType).ClrType == typeof(bool?))
            //                       select m as MetaPrimitiveMember;

            //primitiveMembers.ToList().ForEach(m => m.Column.Converter = typeof(YNToBooleanConverter).FullName);

            //Init others converters
            // TODO: Add your custom converters here

            //InitMetaColumnConvert(metadataContainer, "your table name", "your column name", typeof(TFToBooleanConverter));
        }

        
    }
}
#pragma warning restore 1591