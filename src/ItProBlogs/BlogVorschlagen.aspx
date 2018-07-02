<%@ Page Language="C#" MasterPageFile="~/Blogs.Master" AutoEventWireup="true" CodeBehind="BlogVorschlagen.aspx.cs"
	Inherits="blogs.dotnetgerman.com.BlogVorschlagen" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<h2 title="Blog anmelden">
		Blog anmelden</h2>
	<p>
		Die bei itproblogs.de gelisteten Artikel werden via RSS- oder Atom-Feed 
		bei itproblogs.de eingelesen.<br />
		<br />
		Sollen auch Deine Artikel hier gelistet werden, benötigen wir von Dir einen 
		validen RSS- oder Atom-Feed.<br />
		<br />
		Erstelle hierfür bitte eine Blog-Kategorie &quot;itproblogs.de&quot;, in der alle 
		Blog-Postings liegen müssen, die bei itproblogs.de gelistet werden 
		sollen. Diese Kategorie muß als RSS-Feed abrufbar sein. Alternativ wenn Du 
		bereits eine reine IT Pro Kategorie besitzt, kannst Du diese auch gleich anmelden.
		<br />
		<br />
		Aufnahmekritierien:</p>
	<ul>
		<li>Sprache: deutsch</li>
		<li>Themen: Microsoft Technologien-zentrisch <span style="color:Red"> (keine Familienstories, kein 
            Fußball, keine Hunde, keine Katzen etc. - entsprechende Anfragen werden ohne 
            Antwort gelöscht)</span>.</li>
		<li>Mindestlänge für Artikel: keine</li>
		<li>Regelmäßiges Posten</li>
	</ul>
	<p>
	Um Dein Blog anzumelden, fülle einfach das Formular vollständig aus.
	</p>
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<asp:UpdatePanel ID="AddBlogUpdatePanel" runat="server">
		<ContentTemplate>
			<div style="border: 1px dashed #F89939; padding: 10px; width: 350px;">
				
					<h6>Vorname und Nachname:</h6>
					<p><asp:TextBox ID="nameTextBox" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="nameRequired" runat="server" ControlToValidate="nameTextBox"
						ErrorMessage="Vorname und Nachname fehlen." ValidationGroup="AddBlog" Display="Dynamic"
						ToolTip="Vorname und Nachname fehlen." EnableClientScript="False" CssClass="error" ForeColor="">*</asp:RequiredFieldValidator>
				</p>
				
					<h6>Blog-Url:</h6>
					<p><asp:TextBox ID="blogUrlTextBox" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="blogUrlRequired" runat="server" ControlToValidate="blogUrlTextBox"
						ErrorMessage="Blog-Url fehlt." ValidationGroup="AddBlog" Display="Dynamic" 
						ToolTip="Blog-Url fehlt." EnableClientScript="False" CssClass="error" ForeColor="">*</asp:RequiredFieldValidator>
				</p>
				
				<h6>Blog-Feed-Url (RSS oder Atom):</h6>
				<p>
					<asp:TextBox ID="blogFeedUrlTextBox" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="blogUrlRequired0" runat="server" ControlToValidate="blogFeedUrlTextBox"
						ErrorMessage="Blog-Feed-Url fehlt." ValidationGroup="AddBlog" Display="Dynamic"
						ToolTip="Blog-Feed-Url fehlt." EnableClientScript="False" CssClass="error" ForeColor="">*</asp:RequiredFieldValidator>
				</p>
				
					<h6>E-Mail:</h6>
					<p><asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="emailTextBox"
						ErrorMessage="E-Mail fehlt." ValidationGroup="AddBlog" Display="Dynamic" 
						ToolTip="E-Mail fehlt." EnableClientScript="False" CssClass="error" ForeColor="">*</asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="emailValid" runat="server" ControlToValidate="emailTextBox"
						Display="Dynamic" ErrorMessage="Keine gültige E-Mail." ToolTip="Keine gültige E-Mail."
						ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
						ValidationGroup="AddBlog" EnableClientScript="False" CssClass="error" ForeColor="">*</asp:RegularExpressionValidator>
				</p>
				<h6>Spam-Schutz</h6>
				<p>Bitte die Zahl aus dem Bild in das Feld darunter eintippen:<br />
					<asp:Image ID="Image2" runat="server" ImageUrl="JpegImage.aspx" /><br />
					<asp:TextBox ID="CodeNumberTextBox" runat="server" />
					<asp:RequiredFieldValidator ID="CaptchaRequired" runat="server" 
						ControlToValidate="CodeNumberTextBox" Display="Dynamic" 
						ErrorMessage="Prüfnummer fehlt." ToolTip="Prüfnummer fehlt." 
						ValidationGroup="AddBlog" EnableClientScript="False" CssClass="error" ForeColor="">*</asp:RequiredFieldValidator>
					
					<asp:CustomValidator ID="CaptchaValidation" runat="server" Display="Dynamic" 
						EnableClientScript="False" ErrorMessage="Falsche Prüfsumme." 
						onservervalidate="CaptchaValidation_ServerValidate" 
						ToolTip="Falsche Prüfsumme." ValidationGroup="AddBlog" CssClass="error" ForeColor="">*</asp:CustomValidator>
					
				</p>
				<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
					HeaderText="Bei der Anmeldung sind Fehler auftreten:" ValidationGroup="AddBlog" 
					EnableClientScript="False" CssClass="error" ForeColor="" />
				<p>
				<div style="color:Red; font-weight:bold">
				Nochmal der Hinweis (da es weiter oben offenbar nicht gelesen wird): Es werden nur Blogs aufgenommen, die sich mit Microsoft Technologien und damit eng verwandten Themen befassen.<br />Blogs über Hunde, Katzen, Fußball, Versicherungen oder sonstiges werden nicht aufgenommen. Es wird keine Antwort verschickt - die Anfragen werden direkt gelöscht. Bitte spart uns und Euch diese unnötige Arbeit. Danke.
				</div>
					<asp:Button ID="addButton" runat="server" Text="Blog anmelden" 
						ValidationGroup="AddBlog" onclick="addButton_Click" />
				</p>
				<asp:UpdateProgress ID="UpdateProgress1" runat="server">
					<ProgressTemplate>
						Bloganmeldung wird durchgeführt
						<asp:Image ID="Image1" runat="server" ImageUrl="~/images/ajax-loader.gif" 
							ToolTip="Bloganmeldung wird durchgeführt" />
					</ProgressTemplate>
				</asp:UpdateProgress>
				<asp:Label ID="StateLabel" runat="server" Visible="False" >Die Bloganmeldung 
				wurde erfolgreich durchgeführt.</asp:Label>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
