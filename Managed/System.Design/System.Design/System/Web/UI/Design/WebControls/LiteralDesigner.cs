using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides design-time support in a visual designer for the <see cref="T:System.Web.UI.WebControls.Literal" /> Web server control.</summary>
	// Token: 0x02000197 RID: 407
	[SupportsPreviewControl(true)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class LiteralDesigner : ControlDesigner
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.LiteralDesigner" /> class.</summary>
		// Token: 0x06000B63 RID: 2915 RVA: 0x00009525 File Offset: 0x00007725
		public LiteralDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
