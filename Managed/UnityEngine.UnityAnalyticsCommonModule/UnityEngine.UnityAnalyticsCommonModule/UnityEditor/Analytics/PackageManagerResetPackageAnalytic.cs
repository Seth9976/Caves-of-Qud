using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000022 RID: 34
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerResetPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000024A7 File Offset: 0x000006A7
		public PackageManagerResetPackageAnalytic()
			: base("resetToDefaultDependencies")
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024B8 File Offset: 0x000006B8
		[RequiredByNativeCode]
		internal static PackageManagerResetPackageAnalytic CreatePackageManagerResetPackageAnalytic()
		{
			return new PackageManagerResetPackageAnalytic();
		}
	}
}
