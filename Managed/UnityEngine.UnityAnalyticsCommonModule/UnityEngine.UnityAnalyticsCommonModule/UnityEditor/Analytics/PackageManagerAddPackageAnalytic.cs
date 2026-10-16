using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001D RID: 29
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerAddPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000023E1 File Offset: 0x000005E1
		public PackageManagerAddPackageAnalytic()
			: base("addPackage")
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000023F0 File Offset: 0x000005F0
		[RequiredByNativeCode]
		internal static PackageManagerAddPackageAnalytic CreatePackageManagerAddPackageAnalytic()
		{
			return new PackageManagerAddPackageAnalytic();
		}
	}
}
