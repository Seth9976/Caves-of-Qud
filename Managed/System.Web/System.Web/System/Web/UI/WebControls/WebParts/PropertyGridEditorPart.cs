using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides an editor control that enables end users to edit custom properties on an associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or server control. This class cannot be inherited.</summary>
	// Token: 0x020007B2 RID: 1970
	public sealed class PropertyGridEditorPart : EditorPart
	{
		/// <summary>Creates a new instance of the class.</summary>
		// Token: 0x06004F87 RID: 20359 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PropertyGridEditorPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the values from a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control to the corresponding properties in the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the action of saving values from the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> is successful; otherwise (if an error occurs), false.</returns>
		/// <exception cref="T:System.Exception">An error occurred when trying to set the value for a property on the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />.</exception>
		// Token: 0x06004F88 RID: 20360 RVA: 0x000C8F4C File Offset: 0x000C714C
		public override bool ApplyChanges()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Retrieves the values from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, and updates the corresponding controls used to edit those property values within a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		// Token: 0x06004F89 RID: 20361 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void SyncChanges()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
