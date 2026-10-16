using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/UnityWebRequestTexture/Public/DownloadHandlerTexture.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerTexture : DownloadHandler
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002114 File Offset: 0x00000314
		private static IntPtr Create([Unmarshalled] DownloadHandlerTexture obj, DownloadedTextureParams parameters)
		{
			return DownloadHandlerTexture.Create_Injected(obj, ref parameters);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002129 File Offset: 0x00000329
		private void InternalCreateTexture(DownloadedTextureParams parameters)
		{
			this.m_Ptr = DownloadHandlerTexture.Create(this, parameters);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002139 File Offset: 0x00000339
		public DownloadHandlerTexture()
			: this(true)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002144 File Offset: 0x00000344
		public DownloadHandlerTexture(bool readable)
		{
			DownloadedTextureParams @default = DownloadedTextureParams.Default;
			@default.readable = readable;
			this.InternalCreateTexture(@default);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002170 File Offset: 0x00000370
		public DownloadHandlerTexture(DownloadedTextureParams parameters)
		{
			this.InternalCreateTexture(parameters);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002184 File Offset: 0x00000384
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021A2 File Offset: 0x000003A2
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021B8 File Offset: 0x000003B8
		public Texture2D texture
		{
			get
			{
				return this.InternalGetTextureNative();
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D0 File Offset: 0x000003D0
		[NativeThrows]
		private Texture2D InternalGetTextureNative()
		{
			IntPtr intPtr = DownloadHandlerTexture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(DownloadHandlerTexture.InternalGetTextureNative_Injected(intPtr));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F8 File Offset: 0x000003F8
		public static Texture2D GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerTexture>(www).texture;
		}

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(DownloadHandlerTexture obj, [In] ref DownloadedTextureParams parameters);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetTextureNative_Injected(IntPtr _unity_self);

		// Token: 0x04000008 RID: 8
		private NativeArray<byte> m_NativeData;

		// Token: 0x02000005 RID: 5
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000015 RID: 21 RVA: 0x00002215 File Offset: 0x00000415
			public static IntPtr ConvertToNative(DownloadHandlerTexture handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
