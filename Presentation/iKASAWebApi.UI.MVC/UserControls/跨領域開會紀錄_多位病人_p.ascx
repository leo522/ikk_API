<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="跨領域開會紀錄_多位病人_p.ascx.cs" Inherits="UserControls_跨領域開會紀錄_多位病人_p" %>

<style type="text/css">

    p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman","serif";
	    margin-left: 0cm;
        margin-right: 0cm;
        margin-top: 0cm;
    }
 table.MsoNormalTable
	{font-size:10.0pt;
	font-family:"Times New Roman","serif";
    }
    .shape {behavior:url(#default#VML);}
p.MsoFooter
	{margin-bottom:.0001pt;
	tab-stops:center 207.65pt right 415.3pt;
	layout-grid-mode:char;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	    margin-left: 0cm;
        margin-right: 0cm;
        margin-top: 0cm;
    }
    .RadPicker{vertical-align:middle}.RadPicker{vertical-align:middle}.rdfd_{position:absolute}.rdfd_{position:absolute}.RadPicker .rcTable{table-layout:auto}.RadPicker .rcTable{table-layout:auto}.RadPicker .RadInput{vertical-align:baseline}.RadPicker .RadInput{vertical-align:baseline}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput_Default{font:12px "segoe ui",arial,sans-serif}.RadInput{vertical-align:middle}.RadInput .riTextBox{height:17px}.RadInput .riTextBox{height:17px}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker_Default .rcCalPopup{background-position:0 0}.RadPicker_Default .rcCalPopup{background-image:url('mvwres://Telerik.Web.UI, Version=2013.3.1114.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif')}.RadPicker .rcCalPopup{display:block;overflow:hidden;width:22px;height:22px;background-color:transparent;background-repeat:no-repeat;text-indent:-2222px;text-align:center;-webkit-box-sizing:content-box;-moz-box-sizing:content-box;box-sizing:content-box}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}.RadPicker td a{position:relative;outline:0;z-index:2;margin:0 2px;text-decoration:none}
    div.WordSection1 {
	page: WordSection1;
}
.Default.reWrapper{border:1px solid #828282}.Default.RadEditor{background-color:#eee}.Default.reWrapper{border:1px solid #828282}.Default.RadEditor{background-color:#eee}.RadEditor{box-sizing:content-box;-moz-box-sizing:content-box}.RadEditor{box-sizing:content-box;-moz-box-sizing:content-box}.RadEditor *{box-sizing:content-box;-moz-box-sizing:content-box}.RadEditor *{box-sizing:content-box;-moz-box-sizing:content-box}.reWrapper_corner{font-size:1px;line-height:1px}.reWrapper_corner{font-size:1px;line-height:1px}.reWrapper_center{font-size:1px;line-height:1px}.reWrapper_center{font-size:1px;line-height:1px}.reLeftVerticalSide{padding:1px}.reLeftVerticalSide{font-size:1px;line-height:1px}.reLeftVerticalSide{padding:1px}.reLeftVerticalSide{font-size:1px;line-height:1px}.RadEditor.reWrapper .reToolCell{vertical-align:top;padding-bottom:1px}.RadEditor.reWrapper .reToolCell{vertical-align:top;padding-bottom:1px}.reToolbarWrapper{margin-top:0}.reToolbarWrapper{margin-top:0}.reRightVerticalSide{padding:1px}.reRightVerticalSide{font-size:1px;line-height:1px}.reRightVerticalSide{padding:1px}.reRightVerticalSide{font-size:1px;line-height:1px}.Default.RadEditor .reContentCell{background-color:white}.Default.RadEditor .reContentCell{border:1px solid #828282}.Default.RadEditor .reContentCell{background-color:white}.Default.RadEditor .reContentCell{border:1px solid #828282}.RadEditor .reContentCell{vertical-align:top}.RadEditor .reContentCell{vertical-align:top}.reContentCell{background-color:#fff}.reContentCell{background-color:#fff}.Default.RadEditor .reEditorModes{background-color:#eee}.Default.RadEditor .reEditorModes{background-color:#eee}.reEditorModes{float:left;padding:1px 0!important}.reEditorModes{float:left;padding:1px 0!important}.reBottomZone{text-align:right;vertical-align:bottom}.reBottomZone{text-align:right;vertical-align:bottom}.RadEditor .reResizeCell{vertical-align:bottom;text-align:right}.RadEditor .reResizeCell{vertical-align:bottom;text-align:right}
    div.RadPicker table.rcSingle .rcInputCell{padding-right:0}div.RadPicker table.rcSingle .rcInputCell{padding-right:0}.RadPicker table.rcTable .rcInputCell{padding:0 4px 0 0}.RadPicker table.rcTable .rcInputCell{padding:0 4px 0 0}
    </style>
<form runat="server">
            <div class="WordSection1" style="-ms-layout-grid: both loose 18pt none;">
                <table border="1" cellpadding="0" cellspacing="0" width="728">

                    <tr>
                        <td colspan="4" valign="top">
                            <p style="text-align: justify; line-height: 15pt; -ms-text-justify: inter-ideograph;">
                                <asp:RadioButton ID="RadioButton1" runat="server" GroupName="meettype1" Text="晨報會" />
                                <asp:RadioButton ID="RadioButton2" runat="server" GroupName="meettype1" Text="臨床研討" />
                                <asp:RadioButton ID="RadioButton3" runat="server" GroupName="meettype1" Text="聯合討論" />
                                <asp:RadioButton ID="RadioButton4" runat="server" GroupName="meettype1" Text="文獻研討" />
                                <asp:RadioButton ID="RadioButton5" runat="server" GroupName="meettype1" Text="影像診療" />
                                <asp:RadioButton ID="RadioButton6" runat="server" GroupName="meettype1" Text="臨床病理" />
                            </p>
                            <p style="text-align: justify; line-height: 15pt; -ms-text-justify: inter-ideograph;">
                                <asp:RadioButton ID="RadioButton7" runat="server" GroupName="meettype1" Text="外科病理" />
                                <asp:RadioButton ID="RadioButton8" runat="server" GroupName="meettype1" Text="死亡及併發症" />
                                <asp:RadioButton ID="RadioButton9" runat="server" GroupName="meettype1" Text="主治醫師教學" />
                                <asp:RadioButton ID="RadioButton10" runat="server" GroupName="meettype1" Text="住院醫師教學" />
                            </p>
                            <p style="text-align: justify; line-height: 15pt; -ms-text-justify: inter-ideograph;">
                                <asp:RadioButton ID="RadioButton11" runat="server" GroupName="meettype1" Text="跨領域聯合視訊討論會" />
                            </p>
                        </td>
                        <td width="128">
                            <p align="center" style="text-align: center; -ms-layout-grid-mode: char;">
                                <b><span style="font-family: 標楷體; font-size: 14pt;">會議記錄</span></b></p>
                        </td>
                    </tr>
                    <tr>
                        <td width="76">
                            <p align="center" style="text-align: center;">
                                <span style="font-family: 標楷體;">地點</span></p>
                        </td>
                        <td width="173">
          <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </td>
                        <td width="58">
                            <p align="center" style="text-align: center;">
                                <span style="font-family: 標楷體;">主持者</span></p>
                        </td>
                        <td valign="top" width="172" colspan="2">
          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="86">
                            <p align="center" style="text-align: center;">
                                <span style="font-family: 標楷體;">授課講演者</span></p>
                        </td>
                        <td  valign="center" width="173">
          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                        </td>
                        <td width="58">
                            <p align="center" style="text-align: center;">
                                <span style="font-family: 標楷體;">紀錄人</span></p>
                        </td>
                        <td valign="center" width="172" colspan="2">
          <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td width="76">
                            <p style="text-align: justify; -ms-text-justify: inter-ideograph;">
                                <span style="color: black; font-family: 標楷體;">其他教師</span></p>
                        </td>
                        <td colspan="4" width="652">
                            <p style="text-align: justify; line-height: 15pt; -ms-text-justify: inter-ideograph;">
                                <asp:CheckBox ID="CheckBox1" runat="server" Text="引言者、考評者" />
                                <span style="font-family: &quot;新細明體&quot;,&quot;serif&quot;;">
          <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                </span></p>
                            <p style="text-align: justify; line-height: 15pt; -ms-text-justify: inter-ideograph;">
                                <span style="font-family: &quot;新細明體&quot;,&quot;serif&quot;;">
                                <asp:CheckBox ID="CheckBox3" runat="server" Text="審查者、指導者" />
          <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                </span></p>
                        </td>
                    </tr>
                    <tr>
                        <td width="76">
                            <p style="line-height: 14pt;">
                                <span style="font-family: 標楷體;">與會科別</span></p>
                        </td>
                        <td colspan="4" width="652">
                            <asp:Literal ID="RadEditor4" runat="server"></asp:Literal>
            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" valign="top" width="705">
                            <p style="text-align: justify; -ms-text-justify: inter-ideograph;">
                                <b><span style="font-family: 標楷體;">關鍵詞：</span><span lang="EN-US">(</span><span style="font-family: 標楷體;">由會議主持人勾選，可複選</span><span lang="EN-US">)</span></b></p>
                            <p style="text-align: justify; -ms-text-justify: inter-ideograph;">
                                <asp:CheckBox ID="CheckBox5" runat="server" Text="醫療品質" />
                                <asp:CheckBox ID="CheckBox6" runat="server" Text="醫病溝通" />
                                <asp:CheckBox ID="CheckBox7" runat="server" Text="實證醫學" />
                                <asp:CheckBox ID="CheckBox8" runat="server" Text="病歷寫作" />
                                <asp:CheckBox ID="CheckBox9" runat="server" Text="醫學倫理與法律" />
                                <asp:CheckBox ID="CheckBox10" runat="server" Text="感染控制" />
                            </p>
                            <p style="text-align: justify; -ms-text-justify: inter-ideograph;">
                                <asp:CheckBox ID="CheckBox11" runat="server" Text="全人醫療" />
                                <asp:CheckBox ID="CheckBox12" runat="server" Text="病人安全" />
                                <asp:CheckBox ID="CheckBox13" runat="server" Text="病人權利" />
                                <asp:CheckBox ID="CheckBox14" runat="server" Text="基層與社區醫療" />
                                <asp:CheckBox ID="CheckBox15" runat="server" Text="醫學與臨床教育" />
                            </p>
                            <p style="text-align: justify; -ms-text-justify: inter-ideograph;">
                                <asp:CheckBox ID="CheckBox16" runat="server" Text="預防醫學及觀念" />
                                <asp:CheckBox ID="CheckBox17" runat="server" Checked="True" Text="醫療模式及團隊" />
                                <asp:CheckBox ID="CheckBox18" runat="server" Text="危機管理" />
                                <asp:CheckBox ID="CheckBox19" runat="server" Text="其他" />
                                <span style="font-family: &quot;新細明體&quot;,&quot;serif&quot;;">
          <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                                </span>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" valign="top" width="705">
                            <p>
                                <table border="1" cellpadding="0" cellspacing="0" class="MsoNormalTable" width="751">
                                    <tr>
                                        <td rowspan="2">
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">西<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">醫<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">及<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">各<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">醫<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">事<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">職<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">類<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">臨<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">床<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">照<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">護<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">及<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">問<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">題<span lang="EN-US"><o:p></o:p></span></span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體">討</span></p>
                                            <p align="center" class="MsoNormal">
                                                <span style="font-size:14.0pt;font-family:標楷體;mso-bidi-font-family:&quot;Times New Roman&quot;;
mso-ansi-language:EN-US;mso-fareast-language:ZH-TW;mso-bidi-language:AR-SA">論</span></p>
                                        </td>
                                        <td>
                                            <asp:Literal ID="RadEditor22" runat="server"></asp:Literal>
            
                                        </td>
                                    </tr>
                                    <tr>
                                       <td><span style="font-family: 標楷體;"><span lang="EN-US">主持人總結：(</span>可列出建議措施和目標<span lang="EN-US">)</span><br />
                                           <asp:Literal ID="RadEditor8" runat="server"></asp:Literal>
                                           
                            </span>
                                        </td> 
                                    </tr>
                                </table>
                                <p><span style="font-size:12.0pt;font-family:標楷體;
mso-bidi-font-family:&quot;Times New Roman&quot;;mso-ansi-language:EN-US;mso-fareast-language:
ZH-TW;mso-bidi-language:AR-SA">附註：跨團隊職類含醫師、護理人員、藥師、營養師、物理治療師、職能治療師、語言治療師、社工師、呼吸治療師、心理師等，若頁面不敷使用請自行增加。</span></p>
                        </td>
                    </tr>
                    </table>
</div>

</form>