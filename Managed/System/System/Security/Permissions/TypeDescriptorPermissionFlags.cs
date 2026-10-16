using System;

namespace System.Security.Permissions
{
	/// <summary>Defines permission settings for type descriptors. </summary>
	// Token: 0x02000296 RID: 662
	[Flags]
	[Serializable]
	public enum TypeDescriptorPermissionFlags
	{
		/// <summary>No permission flags are set on the type descriptor.</summary>
		// Token: 0x04000BD8 RID: 3032
		NoFlags = 0,
		/// <summary>The type descriptor may be called from partially trusted code.</summary>
		// Token: 0x04000BD9 RID: 3033
		RestrictedRegistrationAccess = 1
	}
}
