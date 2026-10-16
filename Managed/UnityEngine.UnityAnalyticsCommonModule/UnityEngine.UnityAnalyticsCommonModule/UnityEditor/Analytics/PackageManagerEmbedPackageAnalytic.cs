using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000021 RID: 33
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerEmbedPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000247F File Offset: 0x0000067F
		public PackageManagerEmbedPackageAnalytic()
			: base("embedPackage")
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002490 File Offset: 0x00000690
		[RequiredByNativeCode]
		internal static PackageManagerEmbedPackageAnalytic CreatePackageManagerEmbedPackageAnalytic()
		{
			return new PackageManagerEmbedPackageAnalytic();
		}
	}
}
