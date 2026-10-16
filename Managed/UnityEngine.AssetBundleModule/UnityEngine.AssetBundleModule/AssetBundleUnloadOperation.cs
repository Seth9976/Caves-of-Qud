using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUnloadOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleUnloadOperation : AsyncOperation
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003134 File Offset: 0x00001334
		[NativeMethod("WaitForCompletion")]
		public void WaitForCompletion()
		{
			IntPtr intPtr = AssetBundleUnloadOperation.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AssetBundleUnloadOperation.WaitForCompletion_Injected(intPtr);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002D86 File Offset: 0x00000F86
		public AssetBundleUnloadOperation()
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002D90 File Offset: 0x00000F90
		private AssetBundleUnloadOperation(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitForCompletion_Injected(IntPtr _unity_self);

		// Token: 0x0200000D RID: 13
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600008F RID: 143 RVA: 0x00003156 File Offset: 0x00001356
			public static AssetBundleUnloadOperation ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleUnloadOperation(ptr);
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00002DA3 File Offset: 0x00000FA3
			public static IntPtr ConvertToNative(AssetBundleUnloadOperation assetBundleUnloadOperation)
			{
				return assetBundleUnloadOperation.m_Ptr;
			}
		}
	}
}
