using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/UnityWebRequestAudio/Public/DownloadHandlerAudioClip.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerAudioClip : DownloadHandler
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private unsafe static IntPtr Create([Unmarshalled] DownloadHandlerAudioClip obj, string url, AudioType audioType)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = DownloadHandlerAudioClip.Create_Injected(obj, ref managedSpanWrapper, audioType);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020A8 File Offset: 0x000002A8
		private void InternalCreateAudioClip(string url, AudioType audioType)
		{
			this.m_Ptr = DownloadHandlerAudioClip.Create(this, url, audioType);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020B9 File Offset: 0x000002B9
		public DownloadHandlerAudioClip(string url, AudioType audioType)
		{
			this.InternalCreateAudioClip(url, audioType);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public DownloadHandlerAudioClip(Uri uri, AudioType audioType)
		{
			this.InternalCreateAudioClip(uri.AbsoluteUri, audioType);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002102 File Offset: 0x00000302
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002118 File Offset: 0x00000318
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported for audio clips");
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		[NativeThrows]
		public AudioClip audioClip
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAudioClip.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioClip>(DownloadHandlerAudioClip.get_audioClip_Injected(intPtr));
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002174 File Offset: 0x00000374
		public bool streamAudio
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAudioClip.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DownloadHandlerAudioClip.get_streamAudio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = DownloadHandlerAudioClip.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DownloadHandlerAudioClip.set_streamAudio_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000021BC File Offset: 0x000003BC
		public bool compressed
		{
			get
			{
				IntPtr intPtr = DownloadHandlerAudioClip.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DownloadHandlerAudioClip.get_compressed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = DownloadHandlerAudioClip.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DownloadHandlerAudioClip.set_compressed_Injected(intPtr, value);
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E0 File Offset: 0x000003E0
		public static AudioClip GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerAudioClip>(www).audioClip;
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(DownloadHandlerAudioClip obj, ref ManagedSpanWrapper url, AudioType audioType);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_audioClip_Injected(IntPtr _unity_self);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_streamAudio_Injected(IntPtr _unity_self);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_streamAudio_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_compressed_Injected(IntPtr _unity_self);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_compressed_Injected(IntPtr _unity_self, bool value);

		// Token: 0x04000001 RID: 1
		private NativeArray<byte> m_NativeData;

		// Token: 0x02000003 RID: 3
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000014 RID: 20 RVA: 0x000021FD File Offset: 0x000003FD
			public static IntPtr ConvertToNative(DownloadHandlerAudioClip handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
