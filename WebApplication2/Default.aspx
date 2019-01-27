<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.Default" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <style type="text/css">
        .GvW{
            position:absolute;
            left:0px;
            width:auto;
            height:auto;
    
        }
        .GvK{
            position:absolute;
            left:500px;
            width:auto;
            height:auto;
    
        }
        .GvU{
            position:absolute;
            left:900px;
            width:auto;
            height:auto;
    
        }
        .T{
            position:absolute;
            top:300px;
            width:auto;
            height:auto;
        }
        
        </style>
  
</head>
<body>

    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="gvWychowawca" runat="server" AutoGenerateColumns="false" DataKeyNames="id" CssClass="GvW" ShowFooter="true" AutoGenerateSelectButton="false"
                BackColor="White"  BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvWychowawca_RowDataBound" OnSelectedIndexChanged="gvWychowawca_SelectedIndexChanged" OnRowCommand="gvWychowawca_RowCommand" OnRowEditing="gvWychowawca_RowEditing" OnRowUpdating="gvWychowawca_RowUpdating">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="SelectW" Text="Select" runat="server" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="IdW" Text='<%# Eval("id") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="txtId" ReadOnly="true" Text='<%# Eval("id") %>' runat="server" />
                        </EditItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imie">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Imie") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtImie" Text='<%# Eval("Imie") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                          <asp:TextBox ID="txtImieFooter" runat="server" />
                        </FooterTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nazwisko">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Nazwisko") %>' runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNazwisko" Text='<%# Eval("Nazwisko") %>' runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                          <asp:TextBox ID="txtNazwiskoFooter" runat="server" />
                        </FooterTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button Text="Edit" runat="server" CommandName="Edit" />
                            <asp:Button Text="Delete" runat="server" CommandName="DeleteW" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Button Text="Save" runat="server" CommandName="Update" />
                            <asp:Button Text="Cancel" runat="server" CommandName="Cancel" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button Text="Add" runat="server" CommandName="AddW"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="gvKlasa" runat="server" AutoGenerateColumns="false" DataKeyNames="id" CssClass="GvK"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvKlasa_RowDataBound" OnSelectedIndexChanged="gvKlasa_SelectedIndexChanged">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />

                    <Columns>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="IdK" Text='<%# Eval("id") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nazwa">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Nazwa") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id Wychowawca">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id_Wychowawca") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="gvUczniowie" runat="server" AutoGenerateColumns="false" DataKeyNames="id" CssClass="GvU"
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowDataBound="gvUczniowie_RowDataBound" OnSelectedIndexChanged="gvUczniowie_SelectedIndexChanged">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                 
                <Columns>
                    <asp:TemplateField HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="IdU" Text='<%# Eval("id") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Imie">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Imie") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nazwisko">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Nazwisko") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id Klasy">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Id_Klasy") %>' runat="server" />
                        </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>

          <table class="T">
              <tr>
                  <td>
                      <asp:Label ID="lblSuccessMessage" runat="server" ForeColor="Green" />
                  </td>
                  </tr>
              <tr>
                  <td>
                      <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" />
                  </td>
              </tr>
          </table>            
        </div>
        
        
        
    </form>
    
</body>
</html>
