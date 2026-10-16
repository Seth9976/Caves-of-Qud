using System;

namespace System.Web.UI
{
	/// <summary>Specifies whether view-state information is encrypted.</summary>
	// Token: 0x02000194 RID: 404
	public enum ViewStateEncryptionMode
	{
		/// <summary>The view-state information is encrypted if a control requests encryption by calling the <see cref="M:System.Web.UI.Page.RegisterRequiresViewStateEncryption" /> method. This is the default.</summary>
		// Token: 0x04001337 RID: 4919
		Auto,
		/// <summary>The view-state information is always encrypted.</summary>
		// Token: 0x04001338 RID: 4920
		Always,
		/// <summary>The view-state information is never encrypted, even if a control requests it.</summary>
		// Token: 0x04001339 RID: 4921
		Never
	}
}
