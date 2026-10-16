using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UploadHandler : IDisposable
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00005AAC File Offset: 0x00003CAC
		[NativeMethod(IsThreadSafe = true)]
		private void ReleaseFromScripting()
		{
			IntPtr intPtr = UploadHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UploadHandler.ReleaseFromScripting_Injected(intPtr);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000033DE File Offset: 0x000015DE
		internal UploadHandler()
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005AD0 File Offset: 0x00003CD0
		~UploadHandler()
		{
			this.Dispose();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005B00 File Offset: 0x00003D00
		public virtual void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.ReleaseFromScripting();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005B38 File Offset: 0x00003D38
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005B50 File Offset: 0x00003D50
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005B68 File Offset: 0x00003D68
		public string contentType
		{
			get
			{
				return this.GetContentType();
			}
			set
			{
				this.SetContentType(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005B74 File Offset: 0x00003D74
		public float progress
		{
			get
			{
				return this.GetProgress();
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005B8C File Offset: 0x00003D8C
		internal virtual byte[] GetData()
		{
			return null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005BA0 File Offset: 0x00003DA0
		internal virtual string GetContentType()
		{
			return this.InternalGetContentType();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005BB8 File Offset: 0x00003DB8
		internal virtual void SetContentType(string newContentType)
		{
			this.InternalSetContentType(newContentType);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005BC4 File Offset: 0x00003DC4
		internal virtual float GetProgress()
		{
			return this.InternalGetProgress();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005BDC File Offset: 0x00003DDC
		[NativeMethod("GetContentType")]
		private string InternalGetContentType()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UploadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				UploadHandler.InternalGetContentType_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005C1C File Offset: 0x00003E1C
		[NativeMethod("SetContentType")]
		private unsafe void InternalSetContentType(string newContentType)
		{
			try
			{
				IntPtr intPtr = UploadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(newContentType, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = newContentType.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				UploadHandler.InternalSetContentType_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005C80 File Offset: 0x00003E80
		[NativeMethod("GetProgress")]
		private float InternalGetProgress()
		{
			IntPtr intPtr = UploadHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UploadHandler.InternalGetProgress_Injected(intPtr);
		}

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseFromScripting_Injected(IntPtr _unity_self);

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetContentType_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetContentType_Injected(IntPtr _unity_self, ref ManagedSpanWrapper newContentType);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetProgress_Injected(IntPtr _unity_self);

		// Token: 0x04000068 RID: 104
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0200001A RID: 26
		internal static class BindingsMarshaller
		{
			// Token: 0x06000156 RID: 342 RVA: 0x00005CA2 File Offset: 0x00003EA2
			public static IntPtr ConvertToNative(UploadHandler uploadHandler)
			{
				return uploadHandler.m_Ptr;
			}
		}
	}
}
