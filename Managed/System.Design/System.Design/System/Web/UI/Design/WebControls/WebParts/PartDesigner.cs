using System;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls.WebParts
{
	/// <summary>Extends design-time behavior for controls that implement the <see cref="T:System.Web.UI.WebControls.WebParts.Part" /> abstract class.</summary>
	// Token: 0x020001BF RID: 447
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public abstract class PartDesigner : CompositeControlDesigner
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x00009525 File Offset: 0x00007725
		internal PartDesigner()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
