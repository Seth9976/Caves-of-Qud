using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Holds text, markup, and server controls to render to a <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control in a master page.</summary>
	// Token: 0x02000354 RID: 852
	[ToolboxItem(false)]
	[Designer("System.Web.UI.Design.WebControls.ContentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlBuilder(typeof(ContentBuilderInternal))]
	public class Content : Control, INamingContainer, INonBindingContainer
	{
		/// <summary>Gets or sets the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control that is associated with the current content.</summary>
		/// <returns>A string containing the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> associated with the current content. The default is an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to set the property at run time.</exception>
		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001F88 RID: 8072 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x06001F89 RID: 8073 RVA: 0x00003A0D File Offset: 0x00001C0D
		[Themeable(false)]
		[IDReferenceProperty(typeof(ContentPlaceHolder))]
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public string ContentPlaceHolderID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Occurs when the control binds to a data source.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06001F8A RID: 8074 RVA: 0x0004DA5C File Offset: 0x0004BC5C
		// (remove) Token: 0x06001F8B RID: 8075 RVA: 0x0004DA65 File Offset: 0x0004BC65
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler DataBinding
		{
			add
			{
				base.DataBinding += value;
			}
			remove
			{
				base.DataBinding -= value;
			}
		}

		/// <summary>Occurs when the control is released from memory.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06001F8C RID: 8076 RVA: 0x0004DA6E File Offset: 0x0004BC6E
		// (remove) Token: 0x06001F8D RID: 8077 RVA: 0x0004DA77 File Offset: 0x0004BC77
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Disposed
		{
			add
			{
				base.Disposed += value;
			}
			remove
			{
				base.Disposed -= value;
			}
		}

		/// <summary>Occurs when the control is initialized.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06001F8E RID: 8078 RVA: 0x0004DA80 File Offset: 0x0004BC80
		// (remove) Token: 0x06001F8F RID: 8079 RVA: 0x0004DA89 File Offset: 0x0004BC89
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Init
		{
			add
			{
				base.Init += value;
			}
			remove
			{
				base.Init -= value;
			}
		}

		/// <summary>Occurs when the server control is loaded into the <see cref="T:System.Web.UI.Page" /> control. </summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06001F90 RID: 8080 RVA: 0x0004DA92 File Offset: 0x0004BC92
		// (remove) Token: 0x06001F91 RID: 8081 RVA: 0x0004DA9B File Offset: 0x0004BC9B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Load
		{
			add
			{
				base.Load += value;
			}
			remove
			{
				base.Load -= value;
			}
		}

		/// <summary>Occurs when the server control is about to render to its containing <see cref="T:System.Web.UI.Page" /> control.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06001F92 RID: 8082 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
		// (remove) Token: 0x06001F93 RID: 8083 RVA: 0x0004DAAD File Offset: 0x0004BCAD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler PreRender
		{
			add
			{
				base.PreRender += value;
			}
			remove
			{
				base.PreRender -= value;
			}
		}

		/// <summary>Occurs when the server control is unloaded from memory.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06001F94 RID: 8084 RVA: 0x0004DAB6 File Offset: 0x0004BCB6
		// (remove) Token: 0x06001F95 RID: 8085 RVA: 0x0004DABF File Offset: 0x0004BCBF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Unload
		{
			add
			{
				base.Unload += value;
			}
			remove
			{
				base.Unload -= value;
			}
		}
	}
}
