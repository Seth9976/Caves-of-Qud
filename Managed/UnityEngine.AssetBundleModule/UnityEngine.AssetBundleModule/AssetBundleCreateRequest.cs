using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromAsyncOperation.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleCreateRequest : AsyncOperation
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002D30 File Offset: 0x00000F30
		public AssetBundle assetBundle
		{
			[NativeMethod("GetAssetBundleBlocking")]
			get
			{
				IntPtr intPtr = AssetBundleCreateRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AssetBundle>(AssetBundleCreateRequest.get_assetBundle_Injected(intPtr));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D58 File Offset: 0x00000F58
		[NativeMethod("SetEnableCompatibilityChecks")]
		private void SetEnableCompatibilityChecks(bool set)
		{
			IntPtr intPtr = AssetBundleCreateRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AssetBundleCreateRequest.SetEnableCompatibilityChecks_Injected(intPtr, set);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D7B File Offset: 0x00000F7B
		internal void DisableCompatibilityChecks()
		{
			this.SetEnableCompatibilityChecks(false);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D86 File Offset: 0x00000F86
		public AssetBundleCreateRequest()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D90 File Offset: 0x00000F90
		private AssetBundleCreateRequest(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0600005E RID: 94
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_assetBundle_Injected(IntPtr _unity_self);

		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnableCompatibilityChecks_Injected(IntPtr _unity_self, bool set);

		// Token: 0x02000005 RID: 5
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000060 RID: 96 RVA: 0x00002D9B File Offset: 0x00000F9B
			public static AssetBundleCreateRequest ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleCreateRequest(ptr);
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002DA3 File Offset: 0x00000FA3
			public static IntPtr ConvertToNative(AssetBundleCreateRequest assetBundleCreateRequest)
			{
				return assetBundleCreateRequest.m_Ptr;
			}
		}
	}
}
