using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI
{
	/// <summary>Created when a user control (.ascx file) is specified for output caching, using either the @ OutputCache page directive or the <see cref="T:System.Web.UI.PartialCachingAttribute" /> attribute, and the user control is inserted into a page's control hierarchy by dynamically loading the user control with the <see cref="M:System.Web.UI.TemplateControl.LoadControl(System.String)" /> method.</summary>
	// Token: 0x02000215 RID: 533
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class PartialCachingControl : BasePartialCachingControl
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x00038D56 File Offset: 0x00036F56
		internal PartialCachingControl(Type type, object[] parameters)
		{
			this.type = type;
			this.parameters = parameters;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00038D6C File Offset: 0x00036F6C
		internal override Control CreateControl()
		{
			this.control = (Control)Activator.CreateInstance(this.type, this.parameters);
			if (this.control is UserControl)
			{
				((UserControl)this.control).InitializeAsUserControl(this.Page);
			}
			return this.control;
		}

		/// <summary>Gets a reference to the user control that is cached.</summary>
		/// <returns>The user control that is cached.</returns>
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00038DBE File Offset: 0x00036FBE
		public Control CachedControl
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal PartialCachingControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001544 RID: 5444
		private Type type;

		// Token: 0x04001545 RID: 5445
		private object[] parameters;

		// Token: 0x04001546 RID: 5446
		private Control control;
	}
}
