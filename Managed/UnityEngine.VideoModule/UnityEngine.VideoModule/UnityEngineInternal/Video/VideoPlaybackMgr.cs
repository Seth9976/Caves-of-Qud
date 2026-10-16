using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	// Token: 0x02000018 RID: 24
	[UsedByNativeCode]
	[NativeHeader("Modules/Video/Public/Base/VideoMediaPlayback.h")]
	internal class VideoPlaybackMgr : IDisposable
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00003F7A File Offset: 0x0000217A
		public VideoPlaybackMgr()
		{
			this.m_Ptr = VideoPlaybackMgr.Internal_Create();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003F90 File Offset: 0x00002190
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VideoPlaybackMgr.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000165 RID: 357 RVA: 0x00003FD4 File Offset: 0x000021D4
		public unsafe VideoPlayback CreateVideoPlayback(string fileName, VideoPlaybackMgr.MessageCallback errorCallback, VideoPlaybackMgr.Callback readyCallback, VideoPlaybackMgr.Callback reachedEndCallback, bool splitAlpha = false)
		{
			VideoPlayback videoPlayback;
			try
			{
				IntPtr intPtr = VideoPlaybackMgr.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fileName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fileName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = VideoPlaybackMgr.CreateVideoPlayback_Injected(intPtr, ref managedSpanWrapper, errorCallback, readyCallback, reachedEndCallback, splitAlpha);
			}
			finally
			{
				IntPtr intPtr2;
				IntPtr intPtr3 = intPtr2;
				videoPlayback = ((intPtr3 == 0) ? null : VideoPlayback.BindingsMarshaller.ConvertToManaged(intPtr3));
				char* ptr = null;
			}
			return videoPlayback;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004054 File Offset: 0x00002254
		public void ReleaseVideoPlayback(VideoPlayback playback)
		{
			IntPtr intPtr = VideoPlaybackMgr.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlaybackMgr.ReleaseVideoPlayback_Injected(intPtr, (playback == null) ? ((IntPtr)0) : VideoPlayback.BindingsMarshaller.ConvertToNative(playback));
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004088 File Offset: 0x00002288
		public ulong videoPlaybackCount
		{
			get
			{
				IntPtr intPtr = VideoPlaybackMgr.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlaybackMgr.get_videoPlaybackCount_Injected(intPtr);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000040AC File Offset: 0x000022AC
		public void Update()
		{
			IntPtr intPtr = VideoPlaybackMgr.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlaybackMgr.Update_Injected(intPtr);
		}

		// Token: 0x06000169 RID: 361
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ProcessOSMainLoopMessagesForTesting();

		// Token: 0x0600016A RID: 362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateVideoPlayback_Injected(IntPtr _unity_self, ref ManagedSpanWrapper fileName, VideoPlaybackMgr.MessageCallback errorCallback, VideoPlaybackMgr.Callback readyCallback, VideoPlaybackMgr.Callback reachedEndCallback, bool splitAlpha);

		// Token: 0x0600016B RID: 363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseVideoPlayback_Injected(IntPtr _unity_self, IntPtr playback);

		// Token: 0x0600016C RID: 364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_videoPlaybackCount_Injected(IntPtr _unity_self);

		// Token: 0x0600016D RID: 365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x04000042 RID: 66
		internal IntPtr m_Ptr;

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x0600016F RID: 367
		public delegate void Callback();

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x06000173 RID: 371
		public delegate void MessageCallback(string message);

		// Token: 0x0200001B RID: 27
		internal static class BindingsMarshaller
		{
			// Token: 0x06000176 RID: 374 RVA: 0x000040CE File Offset: 0x000022CE
			public static IntPtr ConvertToNative(VideoPlaybackMgr videoPlaybackMgr)
			{
				return videoPlaybackMgr.m_Ptr;
			}
		}
	}
}
