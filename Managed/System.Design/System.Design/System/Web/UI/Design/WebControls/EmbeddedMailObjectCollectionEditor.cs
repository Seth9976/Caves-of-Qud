using System;
using System.ComponentModel.Design;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a component editor for embedded mail object collections in a <see cref="T:System.Web.UI.WebControls.MailDefinition" /> object.</summary>
	// Token: 0x02000190 RID: 400
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class EmbeddedMailObjectCollectionEditor : CollectionEditor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.EmbeddedMailObjectCollectionEditor" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the collection to edit.</param>
		// Token: 0x06000B5A RID: 2906 RVA: 0x00009525 File Offset: 0x00007725
		public EmbeddedMailObjectCollectionEditor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
