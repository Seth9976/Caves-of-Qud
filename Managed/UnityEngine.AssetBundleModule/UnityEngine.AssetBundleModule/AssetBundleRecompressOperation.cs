using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleRecompressOperation.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AssetBundleRecompressOperation : AsyncOperation
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002FA4 File Offset: 0x000011A4
		public string humanReadableResult
		{
			[NativeMethod("GetResultStr")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = AssetBundleRecompressOperation.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					AssetBundleRecompressOperation.get_humanReadableResult_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FE4 File Offset: 0x000011E4
		public string inputPath
		{
			[NativeMethod("GetInputPath")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = AssetBundleRecompressOperation.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					AssetBundleRecompressOperation.get_inputPath_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003024 File Offset: 0x00001224
		public string outputPath
		{
			[NativeMethod("GetOutputPath")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = AssetBundleRecompressOperation.BindingsMarshaller.ConvertToNative(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					AssetBundleRecompressOperation.get_outputPath_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003064 File Offset: 0x00001264
		public AssetBundleLoadResult result
		{
			[NativeMethod("GetResult")]
			get
			{
				IntPtr intPtr = AssetBundleRecompressOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AssetBundleRecompressOperation.get_result_Injected(intPtr);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003088 File Offset: 0x00001288
		public bool success
		{
			[NativeMethod("GetSuccess")]
			get
			{
				IntPtr intPtr = AssetBundleRecompressOperation.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AssetBundleRecompressOperation.get_success_Injected(intPtr);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002D86 File Offset: 0x00000F86
		public AssetBundleRecompressOperation()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002D90 File Offset: 0x00000F90
		private AssetBundleRecompressOperation(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_humanReadableResult_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_inputPath_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_outputPath_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AssetBundleLoadResult get_result_Injected(IntPtr _unity_self);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_success_Injected(IntPtr _unity_self);

		// Token: 0x02000009 RID: 9
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000080 RID: 128 RVA: 0x000030AA File Offset: 0x000012AA
			public static AssetBundleRecompressOperation ConvertToManaged(IntPtr ptr)
			{
				return new AssetBundleRecompressOperation(ptr);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x00002DA3 File Offset: 0x00000FA3
			public static IntPtr ConvertToNative(AssetBundleRecompressOperation op)
			{
				return op.m_Ptr;
			}
		}
	}
}
