using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001F RID: 31
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerRemovePackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000242F File Offset: 0x0000062F
		public PackageManagerRemovePackageAnalytic()
			: base("removePackage")
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002440 File Offset: 0x00000640
		[RequiredByNativeCode]
		internal static PackageManagerRemovePackageAnalytic CreatePackageManagerRemovePackageAnalytic()
		{
			return new PackageManagerRemovePackageAnalytic();
		}
	}
}
