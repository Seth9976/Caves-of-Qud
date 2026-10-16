using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000024 RID: 36
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerStartServerPackageAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000024F7 File Offset: 0x000006F7
		public PackageManagerStartServerPackageAnalytic()
			: base("startPackageManagerServer")
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002508 File Offset: 0x00000708
		[RequiredByNativeCode]
		internal static PackageManagerStartServerPackageAnalytic CreatePackageManagerStartServerPackageAnalytic()
		{
			return new PackageManagerStartServerPackageAnalytic();
		}
	}
}
