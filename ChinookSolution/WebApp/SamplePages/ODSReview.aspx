<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSReview.aspx.cs" Inherits="WebApp.SamplePages.ODSReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Filter Queries: Albums</h1>

    <%-- dropdown list by artist --%>
    <%--<asp:Label ID="Label1" runat="server" Text="Select an Artist"></asp:Label>&nbsp;&nbsp;
    <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" AppendDataBoundItems="true">
        <asp:ListItem Value="0">Select...</asp:ListItem>
    </asp:DropDownList>--%>

     <asp:Label ID="Label2" runat="server" Text="Enter by album title"></asp:Label>&nbsp;&nbsp;
    <asp:TextBox ID="AlbumTitleArg" runat="server" placeholder="album title" ToolTip="Enter a album title or part of the title"></asp:TextBox>&nbsp;&nbsp;
    <asp:Button ID="Fetch" runat="server" Text="Fetch" />
    <br /><br />
    <asp:GridView ID="AlbumList" runat="server" AutoGenerateColumns="False" DataSourceID="AlbumListODS" AllowPaging="true" PageSize="10" Caption="Album List" GridLines="Horizontal" CssClass="table table-striped">
        <Columns>
            <asp:TemplateField HeaderText="AlbumId" SortExpression="AlbumId">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("AlbumId") %>' ID="AlbumId"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="Title"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artist" SortExpression="ArtistId">
                <ItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" selectedvalue='<%# Bind("ArtistId") %>' AppendDataBoundItems="true" Enabled="false" Width="250px">
                        <asp:ListItem Value="0">Select...</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year" SortExpression="ReleaseYear">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("ReleaseYear") %>' ID="ReleaseYear"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Label" SortExpression="ReleaseLabel">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Eval("ReleaseLabel") == null ? "----------": Eval("ReleaseLabel") %>' ID="ReleaseLabel"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No Album data to display at this time</EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_FindByTitle" TypeName="ChinookSystem.BLL.AlbumController">
        <SelectParameters>
            <asp:ControlParameter ControlID="AlbumTitleArg" PropertyName="Text" DefaultValue="zxxdvx" Name="title" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
</asp:Content>
