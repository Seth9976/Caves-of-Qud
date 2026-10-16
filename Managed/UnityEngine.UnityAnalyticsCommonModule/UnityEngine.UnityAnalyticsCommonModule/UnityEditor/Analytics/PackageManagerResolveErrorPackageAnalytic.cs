using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000023 RID: 35
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerResolveErrorPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000024CF File Offset: 0x000006CF
		public PackageManagerResolveErrorPackageAnalytic()
			: base("resolveErrorUserAction")
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000024E0 File Offset: 0x000006E0
		[RequiredByNativeCode]
		internal static PackageManagerResolveErrorPackageAnalytic CreatePackageManagerResolveErrorPackageAnalytic()
		{
			return new PackageManagerResolveErrorPackageAnalytic();
		}

		// Token: 0x04000056 RID: 86
		public string reason;

		// Token: 0x04000057 RID: 87
		public string action;
	}
}
