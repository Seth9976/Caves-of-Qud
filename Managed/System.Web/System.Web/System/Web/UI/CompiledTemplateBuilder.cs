using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>An <see cref="T:System.Web.UI.ITemplate" /> implementation that is called from the generated page class code. This class cannot be inherited.</summary>
	// Token: 0x020001AE RID: 430
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class CompiledTemplateBuilder : ITemplate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.CompiledTemplateBuilder" /> class.</summary>
		/// <param name="buildTemplateMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledTemplateBuilder.InstantiateIn(System.Web.UI.Control)" /> method call.</param>
		// Token: 0x06001083 RID: 4227 RVA: 0x0002B862 File Offset: 0x00029A62
		public CompiledTemplateBuilder(BuildTemplateMethod buildTemplateMethod)
		{
			this.templateMethod = buildTemplateMethod;
		}

		/// <summary>Populates the <see cref="T:System.Web.UI.Control" /> object with the child controls contained in the template.</summary>
		/// <param name="container">A <see cref="T:System.Web.UI.Control" /> that represents the container used to store the child controls in the template.</param>
		// Token: 0x06001084 RID: 4228 RVA: 0x0002B871 File Offset: 0x00029A71
		public void InstantiateIn(Control container)
		{
			this.templateMethod(container);
		}

		// Token: 0x04001392 RID: 5010
		private BuildTemplateMethod templateMethod;
	}
}
