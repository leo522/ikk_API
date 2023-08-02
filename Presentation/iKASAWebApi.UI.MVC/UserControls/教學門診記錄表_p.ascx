<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="教學門診記錄表_p.ascx.cs" Inherits="KMUH.iKASAWebApi.UI.MVC.UserControls.教學門診記錄表_p" %>

<div class="WordSection1" style="layout-grid:18.0pt">
    <form runat="server">
                <p style="line-height:150%">
                    <b><span style="font-size:18.0pt;
line-height:150%;font-family:標楷體">高雄醫學大學附設中和紀念醫院教學病歷紀錄表</span></b></p>
                <p>
                    <b><span style="font-size:14.0pt;font-family:標楷體">指導老師：<span lang="EN-US"><span style="font-family: 標楷體"><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    </span>&nbsp;<span lang="EN-US" style="font-size: 14.0pt; font-family: 標楷體">記錄者：<span style="font-family: 標楷體"><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                    </span>&nbsp;</span></span></span></b></p>
                <table border="1" cellpadding="0" cellspacing="0" width="704">
                    <tr>
                        <td colspan="2" width="504">
                            <p style="line-height:12.0pt">
                                <b><span style="font-family:
  標楷體">病 患 基 本 資 料</span></b></p>
                        </td>
                        <td width="200">
                            <p style="line-height:12.0pt">
                                <b><span style="font-family:標楷體">教 學 類 別</span></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td width="265">
                            <p>
                                <span style="font-family:標楷體">姓<span lang="EN-US">&nbsp;&nbsp;&nbsp; </span>名：<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </span>
                            </p>
                        </td>
                        <td rowspan="2" width="239">
                            <p>
                                <span style="font-family:標楷體">病歷號碼：<span style="font-family: 標楷體"><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </span></span>
                            </p>
                            <p>
                                <span style="font-family:標楷體">性<span lang="EN-US">&nbsp;&nbsp;&nbsp; </span>別：<asp:RadioButton ID="RadioButton1" runat="server" GroupName="sex" Text="男" />
                                <span style="font-family: 標楷體">
                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="sex" Text="女" />
                                </span></span>
                            </p>
                            <p>
                                <span style="font-family:標楷體">類<span lang="EN-US">&nbsp;&nbsp;&nbsp; </span>別：</span><span style="font-family: 標楷體"><asp:RadioButton ID="RadioButton3" runat="server" GroupName="regtype" Text="初診" />
                                <asp:RadioButton ID="RadioButton4" runat="server" GroupName="regtype" Text="複診" />
                                </span>
                            </p>
                        </td>
                        <td rowspan="2" width="200">
                            <p style="line-height:20.0pt">
                                <span style="font-family:標楷體"><span style="font-family: 標楷體">
                                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="eduType" Text="教學門診" />
                                </span></span>
                            </p>
                            <p style="line-height:20.0pt">
                                <span style="font-family:標楷體"><span style="font-family: 標楷體">
                                <asp:RadioButton ID="RadioButton6" runat="server" GroupName="eduType" Text="教學住診" />
                                </span></span>
                            </p>
                            <p style="line-height:20.0pt">
                                <span style="font-family:標楷體"><span style="font-family: 標楷體">
                                <asp:RadioButton ID="RadioButton7" runat="server" GroupName="eduType" Text="診斷教學" />
                                </span></span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="265">
                            <p>
                                <span style="font-family:標楷體">年<span lang="EN-US">&nbsp;&nbsp;&nbsp; </span>齡：<span style="font-family: 標楷體"><asp:TextBox ID="TextBox2" runat="server" Width="47px"></asp:TextBox>
                                </span>歲</span></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" width="704">
                            <p>
                                <b><span style="font-family:標楷體">教 學 內 容<span lang="EN-US"> </span></span></b>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" width="704">
                            <p style="line-height:150%">
                                <span lang="EN-US" style="font-family:標楷體">1.</span><span style="font-family:標楷體">病史（包含現在、過去病史、藥物過敏史及家族史）：</span></p>
                            <asp:Literal ID="RadEditor3" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" width="704">
                            <p style="text-align:justify;text-justify:inter-ideograph;
  line-height:150%">
                                <span lang="EN-US" style="font-family:標楷體">2.</span><span style="font-family:標楷體">理學及神經學檢查：</span></p>

                            <asp:Literal ID="RadEditor4" runat="server"></asp:Literal>
           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" width="704">
                            <p style="text-align:justify;text-justify:inter-ideograph;
  line-height:150%">
                                <span lang="EN-US" style="font-family:標楷體">3.</span><span style="font-family:標楷體">診斷、臆斷及鑑別診斷：</span></p>
<asp:Literal ID="RadEditor5" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" width="704">
                            <p style="line-height:150%">
                                <span lang="EN-US" style="font-family:標楷體">4.</span><span style="font-family:標楷體">處置計畫（含治療檢查、衛教及其他）：</span></p>
                            <asp:Literal ID="RadEditor6" runat="server"></asp:Literal>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" width="704">
                            <p style="text-align:justify;text-justify:inter-ideograph;
  line-height:150%">
                                <span lang="EN-US" style="font-family:標楷體">5.</span><span style="font-family:標楷體">社會、心理、倫理、法律及醫病溝通問題：</span></p>

                            <asp:Literal ID="RadEditor7" runat="server"></asp:Literal>
            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" width="704">
                            <p style="line-height:150%">
                                <span lang="EN-US" style="font-family:標楷體">6.</span><span style="font-family:標楷體">討論及其他：<span lang="EN-US">(</span>＊請被指導者務必填寫<span lang="EN-US">)</span></span></p>

                            <asp:Literal ID="RadEditor8" runat="server"></asp:Literal>
            
                        </td>
                    </tr>
                </table>
        </form>
</div>
<br clear="all" style="page-break-before:always" />
<div class="WordSection2" style="layout-grid:18.0pt">
</div>