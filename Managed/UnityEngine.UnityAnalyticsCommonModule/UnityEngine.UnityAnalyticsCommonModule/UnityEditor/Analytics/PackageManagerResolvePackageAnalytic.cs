using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000020 RID: 32
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerResolvePackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002457 File Offset: 0x00000657
		public PackageManagerResolvePackageAnalytic()
			: base("resolvePackages")
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002468 File Offset: 0x00000668
		[RequiredByNativeCode]
		internal static PackageManagerResolvePackageAnalytic CreatePackageManagerResolvePackageAnalytic()
		{
			return new PackageManagerResolvePackageAnalytic();
		}

		// Token: 0x04000050 RID: 80
		public string[] packages;

		// Token: 0x04000051 RID: 81
		public string[] package_registries;

		// Token: 0x04000052 RID: 82
		public string[] package_signatures;

		// Token: 0x04000053 RID: 83
		public string[] package_sources;

		// Token: 0x04000054 RID: 84
		public string[] package_types;

		// Token: 0x04000055 RID: 85
		public string[] package_compliance_statuses;
	}
}
