using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Audio;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	// Token: 0x02000015 RID: 21
	[UsedByNativeCode]
	[NativeHeader("Modules/Video/Public/Base/MediaComponent.h")]
	internal class VideoPlayback
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000039DE File Offset: 0x00001BDE
		private VideoPlayback(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000039F0 File Offset: 0x00001BF0
		public void StartPlayback()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.StartPlayback_Injected(intPtr);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00003A14 File Offset: 0x00001C14
		public void PausePlayback()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.PausePlayback_Injected(intPtr);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003A38 File Offset: 0x00001C38
		public void StopPlayback()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.StopPlayback_Injected(intPtr);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003A5C File Offset: 0x00001C5C
		public VideoError GetStatus()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetStatus_Injected(intPtr);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003A80 File Offset: 0x00001C80
		public bool IsReady()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.IsReady_Injected(intPtr);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public bool IsPlaying()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.IsPlaying_Injected(intPtr);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public void Step()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.Step_Injected(intPtr);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003AEC File Offset: 0x00001CEC
		public bool CanStep()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.CanStep_Injected(intPtr);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003B10 File Offset: 0x00001D10
		public uint GetWidth()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetWidth_Injected(intPtr);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003B34 File Offset: 0x00001D34
		public uint GetHeight()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetHeight_Injected(intPtr);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00003B58 File Offset: 0x00001D58
		public float GetFrameRate()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetFrameRate_Injected(intPtr);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00003B7C File Offset: 0x00001D7C
		public float GetDuration()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetDuration_Injected(intPtr);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public ulong GetFrameCount()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetFrameCount_Injected(intPtr);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public uint GetPixelAspectRatioNumerator()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetPixelAspectRatioNumerator_Injected(intPtr);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public uint GetPixelAspectRatioDenominator()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetPixelAspectRatioDenominator_Injected(intPtr);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003C0C File Offset: 0x00001E0C
		public VideoPixelFormat GetPixelFormat()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetPixelFormat_Injected(intPtr);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003C30 File Offset: 0x00001E30
		public bool CanNotSkipOnDrop()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.CanNotSkipOnDrop_Injected(intPtr);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003C54 File Offset: 0x00001E54
		public void SetSkipOnDrop(bool skipOnDrop)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SetSkipOnDrop_Injected(intPtr, skipOnDrop);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003C78 File Offset: 0x00001E78
		public bool GetTexture(Texture texture, out long outputFrameNum)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(texture), out outputFrameNum);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public void SeekToFrame(long frameIndex, VideoPlayback.Callback seekCompletedCallback)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SeekToFrame_Injected(intPtr, frameIndex, seekCompletedCallback);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public void SeekToTime(double secs, VideoPlayback.Callback seekCompletedCallback)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SeekToTime_Injected(intPtr, secs, seekCompletedCallback);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003CEC File Offset: 0x00001EEC
		public float GetPlaybackSpeed()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetPlaybackSpeed_Injected(intPtr);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003D10 File Offset: 0x00001F10
		public void SetPlaybackSpeed(float value)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SetPlaybackSpeed_Injected(intPtr, value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00003D34 File Offset: 0x00001F34
		public bool GetLoop()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetLoop_Injected(intPtr);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00003D58 File Offset: 0x00001F58
		public void SetLoop(bool value)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SetLoop_Injected(intPtr, value);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003D7C File Offset: 0x00001F7C
		public void SetAdjustToLinearSpace(bool enable)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SetAdjustToLinearSpace_Injected(intPtr, enable);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003DA0 File Offset: 0x00001FA0
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		public ushort GetAudioTrackCount()
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetAudioTrackCount_Injected(intPtr);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public ushort GetAudioChannelCount(ushort trackIdx)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetAudioChannelCount_Injected(intPtr, trackIdx);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003DE8 File Offset: 0x00001FE8
		public uint GetAudioSampleRate(ushort trackIdx)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetAudioSampleRate_Injected(intPtr, trackIdx);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003E0C File Offset: 0x0000200C
		public string GetAudioLanguageCode(ushort trackIdx)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VideoPlayback.GetAudioLanguageCode_Injected(intPtr, trackIdx, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00003E4C File Offset: 0x0000204C
		public void SetAudioTarget(ushort trackIdx, bool enabled, bool softwareOutput, AudioSource audioSource)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayback.SetAudioTarget_Injected(intPtr, trackIdx, enabled, softwareOutput, Object.MarshalledUnityObject.Marshal<AudioSource>(audioSource));
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00003E78 File Offset: 0x00002078
		private uint GetAudioSampleProviderId(ushort trackIndex)
		{
			IntPtr intPtr = VideoPlayback.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayback.GetAudioSampleProviderId_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003E9C File Offset: 0x0000209C
		public AudioSampleProvider GetAudioSampleProvider(ushort trackIndex)
		{
			bool flag = trackIndex >= this.GetAudioTrackCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayback has " + this.GetAudioTrackCount().ToString() + " tracks.");
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(this.GetAudioSampleProviderId(trackIndex), null, trackIndex);
			bool flag2 = audioSampleProvider == null;
			if (flag2)
			{
				throw new InvalidOperationException("VideoPlayback.GetAudioSampleProvider got null provider.");
			}
			bool flag3 = audioSampleProvider.owner != null;
			if (flag3)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got unexpected non-null provider owner.");
			}
			bool flag4 = audioSampleProvider.trackIndex != trackIndex;
			if (flag4)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got provider for track " + audioSampleProvider.trackIndex.ToString() + " instead of " + trackIndex.ToString());
			}
			return audioSampleProvider;
		}

		// Token: 0x0600013A RID: 314
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool PlatformSupportsH265();

		// Token: 0x0600013B RID: 315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartPlayback_Injected(IntPtr _unity_self);

		// Token: 0x0600013C RID: 316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PausePlayback_Injected(IntPtr _unity_self);

		// Token: 0x0600013D RID: 317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopPlayback_Injected(IntPtr _unity_self);

		// Token: 0x0600013E RID: 318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoError GetStatus_Injected(IntPtr _unity_self);

		// Token: 0x0600013F RID: 319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsReady_Injected(IntPtr _unity_self);

		// Token: 0x06000140 RID: 320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPlaying_Injected(IntPtr _unity_self);

		// Token: 0x06000141 RID: 321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Step_Injected(IntPtr _unity_self);

		// Token: 0x06000142 RID: 322
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanStep_Injected(IntPtr _unity_self);

		// Token: 0x06000143 RID: 323
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetWidth_Injected(IntPtr _unity_self);

		// Token: 0x06000144 RID: 324
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetHeight_Injected(IntPtr _unity_self);

		// Token: 0x06000145 RID: 325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFrameRate_Injected(IntPtr _unity_self);

		// Token: 0x06000146 RID: 326
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDuration_Injected(IntPtr _unity_self);

		// Token: 0x06000147 RID: 327
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetFrameCount_Injected(IntPtr _unity_self);

		// Token: 0x06000148 RID: 328
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetPixelAspectRatioNumerator_Injected(IntPtr _unity_self);

		// Token: 0x06000149 RID: 329
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetPixelAspectRatioDenominator_Injected(IntPtr _unity_self);

		// Token: 0x0600014A RID: 330
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoPixelFormat GetPixelFormat_Injected(IntPtr _unity_self);

		// Token: 0x0600014B RID: 331
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanNotSkipOnDrop_Injected(IntPtr _unity_self);

		// Token: 0x0600014C RID: 332
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSkipOnDrop_Injected(IntPtr _unity_self, bool skipOnDrop);

		// Token: 0x0600014D RID: 333
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetTexture_Injected(IntPtr _unity_self, IntPtr texture, out long outputFrameNum);

		// Token: 0x0600014E RID: 334
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SeekToFrame_Injected(IntPtr _unity_self, long frameIndex, VideoPlayback.Callback seekCompletedCallback);

		// Token: 0x0600014F RID: 335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SeekToTime_Injected(IntPtr _unity_self, double secs, VideoPlayback.Callback seekCompletedCallback);

		// Token: 0x06000150 RID: 336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPlaybackSpeed_Injected(IntPtr _unity_self);

		// Token: 0x06000151 RID: 337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlaybackSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetLoop_Injected(IntPtr _unity_self);

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLoop_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAdjustToLinearSpace_Injected(IntPtr _unity_self, bool enable);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetAudioTrackCount_Injected(IntPtr _unity_self);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetAudioChannelCount_Injected(IntPtr _unity_self, ushort trackIdx);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetAudioSampleRate_Injected(IntPtr _unity_self, ushort trackIdx);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAudioLanguageCode_Injected(IntPtr _unity_self, ushort trackIdx, out ManagedSpanWrapper ret);

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAudioTarget_Injected(IntPtr _unity_self, ushort trackIdx, bool enabled, bool softwareOutput, IntPtr audioSource);

		// Token: 0x0600015A RID: 346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetAudioSampleProviderId_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x04000041 RID: 65
		internal IntPtr m_Ptr;

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x0600015C RID: 348
		public delegate void Callback();

		// Token: 0x02000017 RID: 23
		internal static class BindingsMarshaller
		{
			// Token: 0x0600015F RID: 351 RVA: 0x00003F6A File Offset: 0x0000216A
			public static VideoPlayback ConvertToManaged(IntPtr ptr)
			{
				return new VideoPlayback(ptr);
			}

			// Token: 0x06000160 RID: 352 RVA: 0x00003F72 File Offset: 0x00002172
			public static IntPtr ConvertToNative(VideoPlayback videoPlayback)
			{
				return videoPlayback.m_Ptr;
			}
		}
	}
}
