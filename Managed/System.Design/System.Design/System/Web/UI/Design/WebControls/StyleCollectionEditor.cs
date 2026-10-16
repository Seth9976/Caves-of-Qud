using System;
using System.ComponentModel.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a design-time editor in a design host, such as Microsoft Visual Studio 2005, for a <see cref="T:System.Web.UI.WebControls.StyleCollection" /> object.</summary>
	// Token: 0x020001AF RID: 431
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class StyleCollectionEditor : CollectionEditor
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.WebControls.StyleCollectionEditor" /> class.</summary>
		/// <param name="type">The type to create an instance of.</param>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x00009525 File Offset: 0x00007725
		public StyleCollectionEditor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
