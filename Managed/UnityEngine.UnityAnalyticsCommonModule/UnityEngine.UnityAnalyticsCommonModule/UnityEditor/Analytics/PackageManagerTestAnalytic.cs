using System;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200001E RID: 30
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerTestAnalytic : PackageManagerBaseAnalytic
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002407 File Offset: 0x00000607
		public PackageManagerTestAnalytic()
			: base("PackageManager")
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002418 File Offset: 0x00000618
		[RequiredByNativeCode]
		internal static PackageManagerTestAnalytic CreatePackageManagerTestAnalytic()
		{
			return new PackageManagerTestAnalytic();
		}
	}
}
