using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= file&gt; element on the server.</summary>
	// Token: 0x0200025F RID: 607
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputFile : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> class.</summary>
		// Token: 0x060018B5 RID: 6325 RVA: 0x000409E6 File Offset: 0x0003EBE6
		public HtmlInputFile()
			: base("file")
		{
		}

		/// <summary>Gets or sets a comma-separated list of MIME encodings used to constrain the file types the user can select.</summary>
		/// <returns>The comma-separated list of MIME encodings.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000409F4 File Offset: 0x0003EBF4
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x00040A1C File Offset: 0x0003EC1C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[DefaultValue("")]
		public string Accept
		{
			get
			{
				string text = base.Attributes["accept"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("accept");
					return;
				}
				base.Attributes["accept"] = value;
			}
		}

		/// <summary>Gets or sets the maximum length of the file path for the file to upload from the client computer.</summary>
		/// <returns>The maximum length of the file path. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00040A44 File Offset: 0x0003EC44
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x00040A6D File Offset: 0x0003EC6D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebSysDescription("")]
		public int MaxLength
		{
			get
			{
				string text = base.Attributes["maxlength"];
				if (text == null)
				{
					return -1;
				}
				return Convert.ToInt32(text);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("maxlength");
					return;
				}
				base.Attributes["maxlength"] = value.ToString();
			}
		}

		/// <summary>Gets access to the uploaded file specified by a client.</summary>
		/// <returns>A <see cref="T:System.Web.HttpPostedFile" /> that accesses the file to be uploaded.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00040A9B File Offset: 0x0003EC9B
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public HttpPostedFile PostedFile
		{
			get
			{
				return this.posted_file;
			}
		}

		/// <summary>Gets or sets the width of the text box in which the file path is entered.</summary>
		/// <returns>The width of the file-path text box. The default value is -1, which indicates that the property has not been set.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00040AA4 File Offset: 0x0003ECA4
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x00040ACD File Offset: 0x0003ECCD
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("-1")]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
				if (text == null)
				{
					return -1;
				}
				return Convert.ToInt32(text);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("size");
					return;
				}
				base.Attributes["size"] = value.ToString();
			}
		}

		/// <summary>Gets the full path of the file on the client's computer.</summary>
		/// <returns>The full path of the client's file.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to this property. </exception>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x00040AFC File Offset: 0x0003ECFC
		// (set) Token: 0x060018BE RID: 6334 RVA: 0x00040B1F File Offset: 0x0003ED1F
		[Browsable(false)]
		public override string Value
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				if (postedFile == null)
				{
					return string.Empty;
				}
				return postedFile.FileName;
			}
			set
			{
				throw new NotSupportedException("The value property on HtmlInputFile is not settable.");
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060018BF RID: 6335 RVA: 0x00040B2C File Offset: 0x0003ED2C
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && !base.Disabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
			HtmlForm htmlForm = (HtmlForm)this.SearchParentByType(typeof(HtmlForm));
			if (htmlForm != null && htmlForm.Enctype == string.Empty)
			{
				htmlForm.Enctype = "multipart/form-data";
			}
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00040B98 File Offset: 0x0003ED98
		private Control SearchParentByType(Type type)
		{
			for (Control control = this.Parent; control != null; control = control.Parent)
			{
				if (type.IsAssignableFrom(control.GetType()))
				{
					return control;
				}
			}
			return null;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00040BCC File Offset: 0x0003EDCC
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			Page page = this.Page;
			if (page != null)
			{
				this.posted_file = page.Request.Files[postDataKey];
			}
			return false;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00003946 File Offset: 0x00001B46
		private void RaisePostDataChangedEventInternal()
		{
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control.</summary>
		/// <returns>This method always returns false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018C3 RID: 6339 RVA: 0x00040BFB File Offset: 0x0003EDFB
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control that the state of the control has changed.</summary>
		// Token: 0x060018C4 RID: 6340 RVA: 0x00040C05 File Offset: 0x0003EE05
		protected virtual void RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputFile" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018C5 RID: 6341 RVA: 0x00040C0D File Offset: 0x0003EE0D
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x060018C6 RID: 6342 RVA: 0x00040C17 File Offset: 0x0003EE17
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x0400162F RID: 5679
		private HttpPostedFile posted_file;
	}
}
