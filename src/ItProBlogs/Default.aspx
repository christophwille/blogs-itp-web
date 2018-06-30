<%@ Page Language="C#" MasterPageFile="~/Blogs.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="blogs.dotnetgerman.com.Default" Title="itproblogs.de" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="itemPlaceHolder">
	<ItemTemplate>

			<h2><asp:HyperLink ID="ItemHyperLink" runat="server"><asp:Label ID="TitleLabel" runat="server" /></asp:HyperLink></h2>
			<p style="overflow:auto;padding-bottom:10px">
			<!-- padding-bottom to avoid overflow in p-->
            <asp:Label ID="NameLabel" runat="server"/>
            </p>
        </ItemTemplate>
	<LayoutTemplate>	
	<div id="itemPlaceHolder" runat="server"/>

	</LayoutTemplate>	
	</asp:ListView>
</asp:Content>
