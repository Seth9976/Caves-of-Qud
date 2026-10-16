using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item to embed in an e-mail message constructed using the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
	// Token: 0x0200038E RID: 910
	public sealed class EmbeddedMailObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> class.</summary>
		// Token: 0x060023B4 RID: 9140 RVA: 0x00002050 File Offset: 0x00000250
		public EmbeddedMailObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> class, using the specified identifier name and path to populate the object.</summary>
		/// <param name="name">The name used as the identifier of the item to embed in the mail message. For more information, see <see cref="P:System.Web.UI.WebControls.EmbeddedMailObject.Name" />.</param>
		/// <param name="path">The path used to retrieve an item to embed in the mail message. For more information, see <see cref="P:System.Web.UI.WebControls.EmbeddedMailObject.Path" />.</param>
		// Token: 0x060023B5 RID: 9141 RVA: 0x0005AA13 File Offset: 0x00058C13
		public EmbeddedMailObject(string name, string path)
		{
			this.Name = name;
			this.Path = path;
		}

		/// <summary>Gets or sets the name that is used as the identifier of the item to be embedded in a mail message constructed with the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
		/// <returns>Returns the identifier of the item to embed in a mail message.</returns>
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x0005AA29 File Offset: 0x00058C29
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x0005AA31 File Offset: 0x00058C31
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string Name { get; set; }

		/// <summary>Gets or sets the path that is used to retrieve an item to embed in a mail message constructed with the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
		/// <returns>Returns the path to the item to embed in a mail message.</returns>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0005AA3A File Offset: 0x00058C3A
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x0005AA42 File Offset: 0x00058C42
		[UrlProperty]
		[Editor("System.Web.UI.Design.MailFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Path { get; set; }
	}
}
