using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRequest : ResourceRequest
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000030B4 File Offset: 0x000012B4
		[NativeMethod("GetLoadedAsset")]
		protected override Object GetResult()
		{
			IntPtr intPtr = AssetBundleRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(AssetBundleRequest.GetResult_Injected(intPtr));
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000030DC File Offset: 0x000012DC
		public new Object asset
		{
			get
			{
				return this.GetResult();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000030F4 File Offset: 0x000012F4
		public Object[] allAssets
		{
			[NativeMethod("GetAllLoadedAssets")]
			get
			{
				IntPtr intPtr = AssetBundleRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AssetBundleRequest.get_allAssets_Injected(intPtr);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003116 File Offset: 0x00001316
		public AssetBundleRequest()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003120 File Offset: 0x00001320
		private AssetBundleRequest(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetResult_Injected(IntPtr _unity_self);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] get_allAssets_Injected(IntPtr _unity_self);

		// Token: 0x0200000B RID: 11
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000089 RID: 137 RVA: 0x0000312B File Offset: 0x0000132B
			public static AssetBundleRequest ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleRequest(ptr);
			}

			// Token: 0x0600008A RID: 138 RVA: 0x00002DA3 File Offset: 0x00000FA3
			public static IntPtr ConvertToNative(AssetBundleRequest request)
			{
				return request.m_Ptr;
			}
		}
	}
}
