using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web.Configuration;

namespace System.Web.UI.WebControls
{
	/// <summary>Allows a control to create e-mail messages from text files or strings. This class cannot be inherited.</summary>
	// Token: 0x020003C8 RID: 968
	[Bindable(false)]
	[ParseChildren(true)]
	public sealed class MailDefinition : IStateManager
	{
		/// <summary>Gets or sets the name of the file that contains text for the body of the e-mail message.</summary>
		/// <returns>The name of the file that contains the message body text. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0006833E File Offset: 0x0006653E
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x00068355 File Offset: 0x00066555
		[UrlProperty("*.*")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.MailDefinitionBodyFileNameEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string BodyFileName
		{
			get
			{
				return this._bag.GetString("BodyFileName", string.Empty);
			}
			set
			{
				this._bag["BodyFileName"] = value;
			}
		}

		/// <summary>Gets or sets a comma-separated list of e-mail addresses to send a copy (CC) of the message to.</summary>
		/// <returns>A comma-separated list of e-mail addresses to send a copy (CC) of the message to. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x00068368 File Offset: 0x00066568
		// (set) Token: 0x060028B0 RID: 10416 RVA: 0x0006837F File Offset: 0x0006657F
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string CC
		{
			get
			{
				return this._bag.GetString("CC", string.Empty);
			}
			set
			{
				this._bag["CC"] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> instances, typically used to embed images in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object before sending an e-mail to a user.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.EmbeddedMailObjectsCollection" /> instances used to embed images in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object before sending an e-mail to a user.</returns>
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x00003A2B File Offset: 0x00001C2B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public EmbeddedMailObjectsCollection EmbeddedObjects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the e-mail address of the message sender.</summary>
		/// <returns>The e-mail address of the message sender. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x00068392 File Offset: 0x00066592
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000683A9 File Offset: 0x000665A9
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string From
		{
			get
			{
				return this._bag.GetString("From", string.Empty);
			}
			set
			{
				this._bag["From"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the body of the e-mail is HTML.</summary>
		/// <returns>true if the body of the e-mail is HTML; otherwise, false.</returns>
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000683BC File Offset: 0x000665BC
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000683CF File Offset: 0x000665CF
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool IsBodyHtml
		{
			get
			{
				return this._bag.GetBool("IsBodyHtml", false);
			}
			set
			{
				this._bag["IsBodyHtml"] = value;
			}
		}

		/// <summary>Gets or sets the priority of the e-mail message.</summary>
		/// <returns>One of the <see cref="T:System.Net.Mail.MailPriority" /> values. The default is <see cref="F:System.Net.Mail.MailPriority.Normal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Net.Mail.MailPriority" /> values.</exception>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x000683E7 File Offset: 0x000665E7
		// (set) Token: 0x060028B7 RID: 10423 RVA: 0x00068412 File Offset: 0x00066612
		[DefaultValue(MailPriority.Normal)]
		[NotifyParentProperty(true)]
		public MailPriority Priority
		{
			get
			{
				if (this._bag["Priority"] != null)
				{
					return (MailPriority)this._bag["Priority"];
				}
				return MailPriority.Normal;
			}
			set
			{
				this._bag["Priority"] = value;
			}
		}

		/// <summary>Gets or sets the subject line of the e-mail message.</summary>
		/// <returns>The subject line of the e-mail message. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0006842A File Offset: 0x0006662A
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x00068441 File Offset: 0x00066641
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Subject
		{
			get
			{
				return this._bag.GetString("Subject", string.Empty);
			}
			set
			{
				this._bag["Subject"] = value;
			}
		}

		/// <summary>Creates an e-mail message from a text file to send by means of SMTP (Simple Mail Transfer Protocol).</summary>
		/// <returns>The e-mail message from a text file.</returns>
		/// <param name="recipients">A comma-separated list of message recipients.</param>
		/// <param name="replacements">An <see cref="T:System.Collections.IDictionary" /> containing a list of strings and their replacement strings.</param>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this <see cref="T:System.Web.UI.WebControls.MailDefinition" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacements" /> does not contain strings.</exception>
		/// <exception cref="T:System.Web.HttpException">The From value in the SMTP section of the configuration file is null or the empty string- or -<paramref name="recipients" /> contains an incorrect e-mail address.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x060028BA RID: 10426 RVA: 0x00068454 File Offset: 0x00066654
		public MailMessage CreateMailMessage(string recipients, IDictionary replacements, Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			string text = null;
			if (this.BodyFileName.Length > 0)
			{
				string text2;
				if (Path.IsPathRooted(this.BodyFileName))
				{
					text2 = this.BodyFileName;
				}
				else
				{
					text2 = HttpContext.Current.Request.MapPath(VirtualPathUtility.Combine(owner.TemplateSourceDirectory, this.BodyFileName));
				}
				using (StreamReader streamReader = new StreamReader(text2))
				{
					text = streamReader.ReadToEnd();
					goto IL_0077;
				}
			}
			text = "";
			IL_0077:
			return this.CreateMailMessage(recipients, replacements, text, owner);
		}

		/// <summary>Creates an e-mail message with replacements from a text file to send by means of SMTP (Simple Mail Transfer Protocol).</summary>
		/// <returns>The e-mail message with replacements from a text file.</returns>
		/// <param name="recipients">The comma-separated list of recipients.</param>
		/// <param name="replacements">An <see cref="T:System.Collections.IDictionary" /> containing a list of strings and their replacement strings.</param>
		/// <param name="body">The text of the e-mail message.</param>
		/// <param name="owner">The <see cref="T:System.Web.UI.Control" /> that owns this <see cref="T:System.Web.UI.WebControls.MailDefinition" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="replacements" /> does not contain strings.</exception>
		/// <exception cref="T:System.Web.HttpException">The From value in the SMTP section of the configuration file is null or an empty string ("").- or -<paramref name="recipients" /> contains an incorrect e-mail address.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x060028BB RID: 10427 RVA: 0x000684F4 File Offset: 0x000666F4
		public MailMessage CreateMailMessage(string recipients, IDictionary replacements, string body, Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			MailMessage mailMessage = new MailMessage();
			if (this.CC.Length > 0)
			{
				mailMessage.CC.Add(this.CC);
			}
			mailMessage.IsBodyHtml = this.IsBodyHtml;
			mailMessage.Priority = this.Priority;
			mailMessage.Subject = this.Subject;
			mailMessage.Body = body;
			if (this.From.Length > 0)
			{
				mailMessage.From = new MailAddress(this.From);
			}
			else
			{
				SmtpSection smtpSection = (SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp");
				if (smtpSection != null)
				{
					if (string.IsNullOrEmpty(smtpSection.From))
					{
						throw new HttpException("A from e-mail address must be specified in the From property or the system.net/mailSettings/smtp config section");
					}
					mailMessage.From = new MailAddress(smtpSection.From);
				}
			}
			string[] array = recipients.Split(',', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				mailMessage.To.Add(array[i]);
			}
			foreach (object obj in replacements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				mailMessage.Body = mailMessage.Body.Replace((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
			}
			return mailMessage;
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.IStateManager.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x060028BC RID: 10428 RVA: 0x00068658 File Offset: 0x00066858
		void IStateManager.LoadViewState(object state)
		{
			this._bag.LoadViewState(state);
		}

		/// <summary>Saves any server control view-state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>The server control's current view state.</returns>
		// Token: 0x060028BD RID: 10429 RVA: 0x00068666 File Offset: 0x00066866
		object IStateManager.SaveViewState()
		{
			return this._bag.SaveViewState();
		}

		/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060028BE RID: 10430 RVA: 0x00068673 File Offset: 0x00066873
		void IStateManager.TrackViewState()
		{
			this._bag.TrackViewState();
		}

		/// <summary>Gets a value that indicates whether the server control is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x00068680 File Offset: 0x00066880
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._bag.IsTrackingViewState;
			}
		}

		// Token: 0x04001A8D RID: 6797
		private StateBag _bag = new StateBag();
	}
}
