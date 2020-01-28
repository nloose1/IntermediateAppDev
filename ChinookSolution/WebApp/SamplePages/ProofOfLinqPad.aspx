﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProofOfLinqPad.aspx.cs" Inherits="WebApp.SamplePages.ProofOfLinqPad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Proof of LinqPad</h1>
    <blockquote class="alert alert-info">This page uses a query developed in LinqPad using the the program editor window.
        The POCO class was created and tested in LinqPad. The query was created and tested in LinqPad.
        the tested code was then placed within the application with one change to the query: the context. was placed infront of the data collection expression
    </blockquote>


    <asp:GridView ID="LinqPadQueryList" runat="server" AutoGenerateColumns="False" DataSourceID="LinqPadQueryListODS" AllowPaging="true" GridLines="Horizontal" BorderStyle="None" CssClass="table table-striped">
        <Columns>
            <asp:BoundField DataField="AlbumTitle" HeaderText="AlbumTitle" SortExpression="AlbumTitle"></asp:BoundField>
            <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"></asp:BoundField>
            <asp:BoundField DataField="ArtistName" HeaderText="ArtistName" SortExpression="ArtistName"></asp:BoundField>
        </Columns>
    </asp:GridView>

    <asp:ObjectDataSource ID="LinqPadQueryListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_AlbumArtists" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
</asp:Content>
