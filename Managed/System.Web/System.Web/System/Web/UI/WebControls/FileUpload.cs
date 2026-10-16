using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays a text box control and a browse button that enable users to select a file to upload to the server.</summary>
	// Token: 0x02000390 RID: 912
	[Designer("DesignerBaseTypeNameSystem.ComponentModel.Design.IDesignerDesignerTypeNameSystem.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ValidationProperty("FileName")]
	[ControlValueProperty("FileBytes")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal, Unrestricted = false)]
	public class FileUpload : WebControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FileUpload" /> class.</summary>
		// Token: 0x060023C4 RID: 9156 RVA: 0x00047517 File Offset: 0x00045717
		public FileUpload()
			: base(HtmlTextWriterTag.Input)
		{
		}

		/// <summary>Gets an array of the bytes in a file that is specified by using a <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the contents of the specified file.</returns>
		/// <exception cref="T:System.Web.HttpException">The entire file was not read.</exception>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x0005AA4C File Offset: 0x00058C4C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true, BindingDirection.OneWay)]
		[Browsable(false)]
		public byte[] FileBytes
		{
			get
			{
				if (this.cachedBytes == null)
				{
					this.cachedBytes = new byte[this.FileContent.Length];
					this.FileContent.Read(this.cachedBytes, 0, this.cachedBytes.Length);
				}
				return (byte[])this.cachedBytes.Clone();
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that points to a file to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that points to a file to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0005AAA4 File Offset: 0x00058CA4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Stream FileContent
		{
			get
			{
				if (this.PostedFile == null)
				{
					return Stream.Null;
				}
				Stream inputStream = this.PostedFile.InputStream;
				if (inputStream != null)
				{
					inputStream.Position = 0L;
				}
				return inputStream;
			}
		}

		/// <summary>Gets the name of a file on a client to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>A string that specifies the name of a file on a client to upload using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FileName
		{
			get
			{
				if (this.PostedFile == null)
				{
					return string.Empty;
				}
				return Path.GetFileName(this.PostedFile.FileName);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control contains a file.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.FileUpload" /> contains a file; otherwise, false.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x0005AAF8 File Offset: 0x00058CF8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool HasFile
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				return postedFile != null && !string.IsNullOrEmpty(postedFile.FileName);
			}
		}

		/// <summary>Gets the underlying <see cref="T:System.Web.HttpPostedFile" /> object for a file that is uploaded by using the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.HttpPostedFile" /> for a file uploaded by using the <see cref="T:System.Web.UI.WebControls.FileUpload" />.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060023C9 RID: 9161 RVA: 0x0005AB20 File Offset: 0x00058D20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpPostedFile PostedFile
		{
			get
			{
				Page page = this.Page;
				if (page == null || !page.IsPostBack)
				{
					return null;
				}
				if (this.Context == null || this.Context.Request == null)
				{
					return null;
				}
				return this.Context.Request.Files[this.UniqueID];
			}
		}

		/// <summary>Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.FileUpload" /> control to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x060023CA RID: 9162 RVA: 0x0005AB73 File Offset: 0x00058D73
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "file", false);
			if (!string.IsNullOrEmpty(this.UniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event for the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060023CB RID: 9163 RVA: 0x0005ABA8 File Offset: 0x00058DA8
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null)
			{
				page.Form.Enctype = "multipart/form-data";
			}
		}

		/// <summary>Sends the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to render on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the <see cref="T:System.Web.UI.WebControls.FileUpload" /> control content. </param>
		// Token: 0x060023CC RID: 9164 RVA: 0x0005ABD8 File Offset: 0x00058DD8
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			base.Render(writer);
		}

		/// <summary>Saves the contents of an uploaded file to a specified path on the Web server.</summary>
		/// <param name="filename">A string that specifies the full path of the location of the server on which to save the uploaded file. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="filename" /> is not a full path.</exception>
		// Token: 0x060023CD RID: 9165 RVA: 0x0005AC00 File Offset: 0x00058E00
		public void SaveAs(string filename)
		{
			HttpPostedFile postedFile = this.PostedFile;
			if (postedFile != null)
			{
				postedFile.SaveAs(filename);
			}
		}

		/// <summary>Gets or sets a value that specifies whether multiple files can be selected for upload.</summary>
		/// <returns>true if multiple files can be selected; otherwise, false.</returns>
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x0005AC20 File Offset: 0x00058E20
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool AllowMultiple
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether any files have been uploaded.</summary>
		/// <returns>true if any files have been uploaded; otherwise, false.</returns>
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x0005AC3C File Offset: 0x00058E3C
		public bool HasFiles
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the collection of uploaded files.</summary>
		/// <returns>The collection of uploaded files.</returns>
		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IList<HttpPostedFile> PostedFiles
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x0400198E RID: 6542
		private byte[] cachedBytes;
	}
}
