using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AssetBundlePatching
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/AssetBundle/Public/AssetBundlePatching.h")]
	public static class AssetBundleUtility
	{
		// Token: 0x06000091 RID: 145
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PatchAssetBundles(AssetBundle[] bundles, string[] filenames);
	}
}
