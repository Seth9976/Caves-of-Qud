using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies the permitted access to X.509 certificate stores.</summary>
	// Token: 0x0200029D RID: 669
	[Flags]
	[Serializable]
	public enum StorePermissionFlags
	{
		/// <summary>Permission is not given to perform any certificate or store operations.</summary>
		// Token: 0x04000BE9 RID: 3049
		NoFlags = 0,
		/// <summary>The ability to create a new store.</summary>
		// Token: 0x04000BEA RID: 3050
		CreateStore = 1,
		/// <summary>The ability to delete a store.</summary>
		// Token: 0x04000BEB RID: 3051
		DeleteStore = 2,
		/// <summary>The ability to enumerate the stores on a computer.</summary>
		// Token: 0x04000BEC RID: 3052
		EnumerateStores = 4,
		/// <summary>The ability to open a store.</summary>
		// Token: 0x04000BED RID: 3053
		OpenStore = 16,
		/// <summary>The ability to add a certificate to a store.</summary>
		// Token: 0x04000BEE RID: 3054
		AddToStore = 32,
		/// <summary>The ability to remove a certificate from a store.</summary>
		// Token: 0x04000BEF RID: 3055
		RemoveFromStore = 64,
		/// <summary>The ability to enumerate the certificates in a store.</summary>
		// Token: 0x04000BF0 RID: 3056
		EnumerateCertificates = 128,
		/// <summary>The ability to perform all certificate and store operations.</summary>
		// Token: 0x04000BF1 RID: 3057
		AllFlags = 247
	}
}
