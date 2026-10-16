using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Implements the basic functionality required by Web controls that contain child controls.</summary>
	// Token: 0x02000351 RID: 849
	[Designer("System.Web.UI.Design.WebControls.CompositeControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class CompositeControl : WebControl, INamingContainer, ICompositeControlDesignerAccessor
	{
		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Binds a data source to the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> and all its child controls.</summary>
		// Token: 0x06001F75 RID: 8053 RVA: 0x0004D945 File Offset: 0x0004BB45
		public override void DataBind()
		{
			this.EnsureChildControls();
			base.DataBind();
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001F76 RID: 8054 RVA: 0x0004D953 File Offset: 0x0004BB53
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			base.Render(writer);
		}

		/// <summary>Enables a designer to recreate the composite control's collection of child controls in the design-time environment.</summary>
		// Token: 0x06001F77 RID: 8055 RVA: 0x0004D962 File Offset: 0x0004BB62
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.RecreateChildControls();
		}

		/// <summary>Recreates the child controls in a control derived from <see cref="T:System.Web.UI.WebControls.CompositeControl" />. </summary>
		// Token: 0x06001F78 RID: 8056 RVA: 0x0004D96A File Offset: 0x0004BB6A
		[global::System.MonoTODO("not sure exactly what this one does..")]
		protected virtual void RecreateChildControls()
		{
			this.CreateChildControls();
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls in a <see cref="T:System.Web.UI.WebControls.CompositeControl" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that represents the child controls in the <see cref="T:System.Web.UI.WebControls.CompositeControl" />.</returns>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x0004505A File Offset: 0x0004325A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}
	}
}
