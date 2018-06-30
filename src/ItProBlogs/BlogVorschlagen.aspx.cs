using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace blogs.dotnetgerman.com {

	public partial class BlogVorschlagen : System.Web.UI.Page {

		private Random random = new Random();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)

				// Create a random code and store it in the Session object.
				this.Session["CaptchaImageText"] = GenerateRandomCode();

			//else {
			//    // On a postback, check the user input.
			//    if (this.CodeNumberTextBox.Text == this.Session["CaptchaImageText"].ToString()) {
			//        // Display an informational message.
			//        this.MessageLabel.CssClass = "info";
			//        this.MessageLabel.Text = "Correct!";
			//    }
			//    else {
			//        // Display an error message.
			//        this.MessageLabel.CssClass = "error";
			//        this.MessageLabel.Text = "ERROR: Incorrect, try again.";

			//        // Clear the input and create a new random code.
			//        this.CodeNumberTextBox.Text = "";
			//        this.Session["CaptchaImageText"] = GenerateRandomCode();
			//    }
			//}
		}


		private string GenerateRandomCode()
		{
			string s = "";
			for (int i = 0; i < 6; i++)
				s = String.Concat(s, this.random.Next(10).ToString());
			return s;
		}

		protected void addButton_Click(object sender, EventArgs e)
		{
			if (Page.IsValid) {
				this.addButton.Enabled = false;
				string adminMailAddressesSetting =
					ConfigurationSettings.AppSettings["AdminMailAddresses"];
				if (!string.IsNullOrEmpty(adminMailAddressesSetting)) {
					string[] adminMailAddresses = adminMailAddressesSetting.Split(new string[] { "," }, StringSplitOptions.None);

					MailDefinition md = new MailDefinition();
					md.BodyFileName = Server.MapPath(
						ConfigurationSettings.AppSettings["AddBlogMailTemplatePath"]);
					ListDictionary replacements = new ListDictionary();
					replacements.Add("<%Fullname%>", this.nameTextBox.Text);
					replacements.Add("<%BlogUrl%>", this.blogUrlTextBox.Text);
					replacements.Add("<%BlogFeedUrl%>", this.blogFeedUrlTextBox.Text);
					replacements.Add("<%EMail%>", this.emailTextBox.Text);
					MailMessage mm = null;

					mm = md.CreateMailMessage(adminMailAddressesSetting, replacements, this);
					mm.Subject = "Neue Bloganmeldung für itproblogs.de";
					SmtpClient smtp = new SmtpClient();
					smtp.Send(mm);
					this.StateLabel.Visible = true;
				}
			}
		}

		protected void CaptchaValidation_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (this.CaptchaRequired.IsValid == true) {
				if (this.Session["CaptchaImageText"].ToString() == this.CodeNumberTextBox.Text) {
					args.IsValid = true;
				}
				else {
					args.IsValid = false;
				}
			}
		}
	}
}
