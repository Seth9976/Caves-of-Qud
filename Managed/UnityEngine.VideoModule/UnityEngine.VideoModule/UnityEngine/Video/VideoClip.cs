using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	// Token: 0x02000004 RID: 4
	[RequiredByNativeCode]
	[NativeHeader("Modules/Video/Public/VideoClip.h")]
	public sealed class VideoClip : Object
	{
		// Token: 0x06000025 RID: 37 RVA: 0x0000257F File Offset: 0x0000077F
		private VideoClip()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000258C File Offset: 0x0000078C
		public string originalPath
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					VideoClip.get_originalPath_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025CC File Offset: 0x000007CC
		public ulong frameCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_frameCount_Injected(intPtr);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025F0 File Offset: 0x000007F0
		public double frameRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_frameRate_Injected(intPtr);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002614 File Offset: 0x00000814
		[NativeName("Duration")]
		public double length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_length_Injected(intPtr);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002638 File Offset: 0x00000838
		public uint width
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_width_Injected(intPtr);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000265C File Offset: 0x0000085C
		public uint height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_height_Injected(intPtr);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002680 File Offset: 0x00000880
		public uint pixelAspectRatioNumerator
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_pixelAspectRatioNumerator_Injected(intPtr);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026A4 File Offset: 0x000008A4
		public uint pixelAspectRatioDenominator
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_pixelAspectRatioDenominator_Injected(intPtr);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026C8 File Offset: 0x000008C8
		public bool sRGB
		{
			[NativeName("IssRGB")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_sRGB_Injected(intPtr);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026EC File Offset: 0x000008EC
		public ushort audioTrackCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoClip.get_audioTrackCount_Injected(intPtr);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002710 File Offset: 0x00000910
		public ushort GetAudioChannelCount(ushort audioTrackIdx)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoClip.GetAudioChannelCount_Injected(intPtr, audioTrackIdx);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002734 File Offset: 0x00000934
		public uint GetAudioSampleRate(ushort audioTrackIdx)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoClip.GetAudioSampleRate_Injected(intPtr, audioTrackIdx);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002758 File Offset: 0x00000958
		public string GetAudioLanguage(ushort audioTrackIdx)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoClip>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VideoClip.GetAudioLanguage_Injected(intPtr, audioTrackIdx, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_originalPath_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_frameCount_Injected(IntPtr _unity_self);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_frameRate_Injected(IntPtr _unity_self);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_length_Injected(IntPtr _unity_self);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_width_Injected(IntPtr _unity_self);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_height_Injected(IntPtr _unity_self);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_pixelAspectRatioNumerator_Injected(IntPtr _unity_self);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_pixelAspectRatioDenominator_Injected(IntPtr _unity_self);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sRGB_Injected(IntPtr _unity_self);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort get_audioTrackCount_Injected(IntPtr _unity_self);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetAudioChannelCount_Injected(IntPtr _unity_self, ushort audioTrackIdx);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetAudioSampleRate_Injected(IntPtr _unity_self, ushort audioTrackIdx);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAudioLanguage_Injected(IntPtr _unity_self, ushort audioTrackIdx, out ManagedSpanWrapper ret);
	}
}
