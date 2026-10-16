using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Video
{
	// Token: 0x0200000D RID: 13
	[RequireComponent(typeof(Transform))]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Video/Public/VideoPlayer.h")]
	public sealed class VideoPlayer : Behaviour
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002798 File Offset: 0x00000998
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000027BC File Offset: 0x000009BC
		public VideoSource source
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_source_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_source_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002804 File Offset: 0x00000A04
		public VideoTimeUpdateMode timeUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_timeUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_timeUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002828 File Offset: 0x00000A28
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002868 File Offset: 0x00000A68
		[NativeName("VideoUrl")]
		public unsafe string url
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					VideoPlayer.get_url_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					VideoPlayer.set_url_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028CC File Offset: 0x00000ACC
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000028F4 File Offset: 0x00000AF4
		[NativeName("VideoClip")]
		public VideoClip clip
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<VideoClip>(VideoPlayer.get_clip_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_clip_Injected(intPtr, Object.MarshalledUnityObject.Marshal<VideoClip>(value));
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000291C File Offset: 0x00000B1C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002940 File Offset: 0x00000B40
		public VideoRenderMode renderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_renderMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_renderMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002964 File Offset: 0x00000B64
		public bool canSetTimeUpdateMode
		{
			[NativeName("CanSetTimeUpdateMode")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetTimeUpdateMode_Injected(intPtr);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002988 File Offset: 0x00000B88
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000029B0 File Offset: 0x00000BB0
		[NativeHeader("Runtime/Camera/Camera.h")]
		public Camera targetCamera
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Camera>(VideoPlayer.get_targetCamera_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetCamera_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(value));
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000029D8 File Offset: 0x00000BD8
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002A00 File Offset: 0x00000C00
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		public RenderTexture targetTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(VideoPlayer.get_targetTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(value));
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002A28 File Offset: 0x00000C28
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002A50 File Offset: 0x00000C50
		[NativeHeader("Runtime/Graphics/Renderer.h")]
		public Renderer targetMaterialRenderer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Renderer>(VideoPlayer.get_targetMaterialRenderer_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetMaterialRenderer_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Renderer>(value));
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A78 File Offset: 0x00000C78
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public unsafe string targetMaterialProperty
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					VideoPlayer.get_targetMaterialProperty_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					VideoPlayer.set_targetMaterialProperty_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002B1C File Offset: 0x00000D1C
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002B40 File Offset: 0x00000D40
		public VideoAspectRatio aspectRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_aspectRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_aspectRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002B64 File Offset: 0x00000D64
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002B88 File Offset: 0x00000D88
		public float targetCameraAlpha
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_targetCameraAlpha_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetCameraAlpha_Injected(intPtr, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002BAC File Offset: 0x00000DAC
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public Video3DLayout targetCamera3DLayout
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_targetCamera3DLayout_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_targetCamera3DLayout_Injected(intPtr, value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002BF4 File Offset: 0x00000DF4
		[NativeHeader("Runtime/Graphics/Texture.h")]
		public Texture texture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(VideoPlayer.get_texture_Injected(intPtr));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C1C File Offset: 0x00000E1C
		public void Prepare()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Prepare_Injected(intPtr);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002C40 File Offset: 0x00000E40
		public bool isPrepared
		{
			[NativeName("IsPrepared")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isPrepared_Injected(intPtr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002C64 File Offset: 0x00000E64
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002C88 File Offset: 0x00000E88
		public bool waitForFirstFrame
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_waitForFirstFrame_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_waitForFirstFrame_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002CAC File Offset: 0x00000EAC
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public bool playOnAwake
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_playOnAwake_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_playOnAwake_Injected(intPtr, value);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CF4 File Offset: 0x00000EF4
		public void Play()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Play_Injected(intPtr);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D18 File Offset: 0x00000F18
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Pause_Injected(intPtr);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D3C File Offset: 0x00000F3C
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.Stop_Injected(intPtr);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002D60 File Offset: 0x00000F60
		public bool isPlaying
		{
			[NativeName("IsPlaying")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isPlaying_Injected(intPtr);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002D84 File Offset: 0x00000F84
		public bool isPaused
		{
			[NativeName("IsPaused")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isPaused_Injected(intPtr);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public bool canSetTime
		{
			[NativeName("CanSetTime")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetTime_Injected(intPtr);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002DF0 File Offset: 0x00000FF0
		[NativeName("SecPosition")]
		public double time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002E38 File Offset: 0x00001038
		[NativeName("FramePosition")]
		public long frame
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_frame_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_frame_Injected(intPtr, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002E5C File Offset: 0x0000105C
		public double clockTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_clockTime_Injected(intPtr);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002E80 File Offset: 0x00001080
		public bool canStep
		{
			[NativeName("CanStep")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canStep_Injected(intPtr);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002EA4 File Offset: 0x000010A4
		public void StepForward()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.StepForward_Injected(intPtr);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002EC8 File Offset: 0x000010C8
		public bool canSetPlaybackSpeed
		{
			[NativeName("CanSetPlaybackSpeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetPlaybackSpeed_Injected(intPtr);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002EEC File Offset: 0x000010EC
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002F10 File Offset: 0x00001110
		public float playbackSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_playbackSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_playbackSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002F34 File Offset: 0x00001134
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002F58 File Offset: 0x00001158
		[NativeName("Loop")]
		public bool isLooping
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_isLooping_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_isLooping_Injected(intPtr, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F7C File Offset: 0x0000117C
		[Obsolete("VideoPlayer.canSetTimeSource is deprecated. Use canSetTimeUpdateMode instead. (UnityUpgradable) -> canSetTimeUpdateMode")]
		public bool canSetTimeSource
		{
			[NativeName("CanSetTimeSource")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetTimeSource_Injected(intPtr);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002FA0 File Offset: 0x000011A0
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002FC4 File Offset: 0x000011C4
		[Obsolete("VideoPlayer.timeSource is deprecated. Use timeUpdateMode instead. (UnityUpgradable) -> timeUpdateMode")]
		public VideoTimeSource timeSource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_timeSource_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_timeSource_Injected(intPtr, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000300C File Offset: 0x0000120C
		public VideoTimeReference timeReference
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_timeReference_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_timeReference_Injected(intPtr, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003054 File Offset: 0x00001254
		public double externalReferenceTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_externalReferenceTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_externalReferenceTime_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003078 File Offset: 0x00001278
		public bool canSetSkipOnDrop
		{
			[NativeName("CanSetSkipOnDrop")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetSkipOnDrop_Injected(intPtr);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000309C File Offset: 0x0000129C
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000030C0 File Offset: 0x000012C0
		public bool skipOnDrop
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_skipOnDrop_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_skipOnDrop_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000030E4 File Offset: 0x000012E4
		public ulong frameCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_frameCount_Injected(intPtr);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003108 File Offset: 0x00001308
		public float frameRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_frameRate_Injected(intPtr);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000312C File Offset: 0x0000132C
		[NativeName("Duration")]
		public double length
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_length_Injected(intPtr);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003150 File Offset: 0x00001350
		public uint width
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_width_Injected(intPtr);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003174 File Offset: 0x00001374
		public uint height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_height_Injected(intPtr);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003198 File Offset: 0x00001398
		public uint pixelAspectRatioNumerator
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_pixelAspectRatioNumerator_Injected(intPtr);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000031BC File Offset: 0x000013BC
		public uint pixelAspectRatioDenominator
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_pixelAspectRatioDenominator_Injected(intPtr);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000031E0 File Offset: 0x000013E0
		public ushort audioTrackCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_audioTrackCount_Injected(intPtr);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003204 File Offset: 0x00001404
		public string GetAudioLanguageCode(ushort trackIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VideoPlayer.GetAudioLanguageCode_Injected(intPtr, trackIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003244 File Offset: 0x00001444
		public ushort GetAudioChannelCount(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetAudioChannelCount_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003268 File Offset: 0x00001468
		public uint GetAudioSampleRate(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetAudioSampleRate_Injected(intPtr, trackIndex);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000087 RID: 135
		public static extern ushort controlledAudioTrackMaxCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000328C File Offset: 0x0000148C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000032A4 File Offset: 0x000014A4
		public ushort controlledAudioTrackCount
		{
			get
			{
				return this.GetControlledAudioTrackCount();
			}
			set
			{
				int controlledAudioTrackMaxCount = (int)VideoPlayer.controlledAudioTrackMaxCount;
				bool flag = (int)value > controlledAudioTrackMaxCount;
				if (flag)
				{
					throw new ArgumentException(string.Format("Cannot control more than {0} tracks.", controlledAudioTrackMaxCount), "value");
				}
				this.SetControlledAudioTrackCount(value);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000032E4 File Offset: 0x000014E4
		private ushort GetControlledAudioTrackCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetControlledAudioTrackCount_Injected(intPtr);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003308 File Offset: 0x00001508
		private void SetControlledAudioTrackCount(ushort value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetControlledAudioTrackCount_Injected(intPtr, value);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000332C File Offset: 0x0000152C
		public void EnableAudioTrack(ushort trackIndex, bool enabled)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.EnableAudioTrack_Injected(intPtr, trackIndex, enabled);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003350 File Offset: 0x00001550
		public bool IsAudioTrackEnabled(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.IsAudioTrackEnabled_Injected(intPtr, trackIndex);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003374 File Offset: 0x00001574
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003398 File Offset: 0x00001598
		public VideoAudioOutputMode audioOutputMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_audioOutputMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_audioOutputMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000033BC File Offset: 0x000015BC
		public bool canSetDirectAudioVolume
		{
			[NativeName("CanSetDirectAudioVolume")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_canSetDirectAudioVolume_Injected(intPtr);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033E0 File Offset: 0x000015E0
		public float GetDirectAudioVolume(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetDirectAudioVolume_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003404 File Offset: 0x00001604
		public void SetDirectAudioVolume(ushort trackIndex, float volume)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetDirectAudioVolume_Injected(intPtr, trackIndex, volume);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003428 File Offset: 0x00001628
		public bool GetDirectAudioMute(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VideoPlayer.GetDirectAudioMute_Injected(intPtr, trackIndex);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000344C File Offset: 0x0000164C
		public void SetDirectAudioMute(ushort trackIndex, bool mute)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetDirectAudioMute_Injected(intPtr, trackIndex, mute);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003470 File Offset: 0x00001670
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		public AudioSource GetTargetAudioSource(ushort trackIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<AudioSource>(VideoPlayer.GetTargetAudioSource_Injected(intPtr, trackIndex));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003498 File Offset: 0x00001698
		public void SetTargetAudioSource(ushort trackIndex, AudioSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VideoPlayer.SetTargetAudioSource_Injected(intPtr, trackIndex, Object.MarshalledUnityObject.Marshal<AudioSource>(source));
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000097 RID: 151 RVA: 0x000034C4 File Offset: 0x000016C4
		// (remove) Token: 0x06000098 RID: 152 RVA: 0x000034FC File Offset: 0x000016FC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler prepareCompleted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000099 RID: 153 RVA: 0x00003534 File Offset: 0x00001734
		// (remove) Token: 0x0600009A RID: 154 RVA: 0x0000356C File Offset: 0x0000176C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler loopPointReached;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600009B RID: 155 RVA: 0x000035A4 File Offset: 0x000017A4
		// (remove) Token: 0x0600009C RID: 156 RVA: 0x000035DC File Offset: 0x000017DC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler started;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600009D RID: 157 RVA: 0x00003614 File Offset: 0x00001814
		// (remove) Token: 0x0600009E RID: 158 RVA: 0x0000364C File Offset: 0x0000184C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler frameDropped;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600009F RID: 159 RVA: 0x00003684 File Offset: 0x00001884
		// (remove) Token: 0x060000A0 RID: 160 RVA: 0x000036BC File Offset: 0x000018BC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.ErrorEventHandler errorReceived;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000A1 RID: 161 RVA: 0x000036F4 File Offset: 0x000018F4
		// (remove) Token: 0x060000A2 RID: 162 RVA: 0x0000372C File Offset: 0x0000192C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.EventHandler seekCompleted;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060000A3 RID: 163 RVA: 0x00003764 File Offset: 0x00001964
		// (remove) Token: 0x060000A4 RID: 164 RVA: 0x0000379C File Offset: 0x0000199C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.TimeEventHandler clockResyncOccurred;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000037D4 File Offset: 0x000019D4
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000037F8 File Offset: 0x000019F8
		public bool sendFrameReadyEvents
		{
			[NativeName("AreFrameReadyEventsEnabled")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return VideoPlayer.get_sendFrameReadyEvents_Injected(intPtr);
			}
			[NativeName("EnableFrameReadyEvents")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<VideoPlayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VideoPlayer.set_sendFrameReadyEvents_Injected(intPtr, value);
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000A7 RID: 167 RVA: 0x0000381C File Offset: 0x00001A1C
		// (remove) Token: 0x060000A8 RID: 168 RVA: 0x00003854 File Offset: 0x00001A54
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VideoPlayer.FrameReadyEventHandler frameReady;

		// Token: 0x060000A9 RID: 169 RVA: 0x0000388C File Offset: 0x00001A8C
		[RequiredByNativeCode]
		private static void InvokePrepareCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.prepareCompleted != null;
			if (flag)
			{
				source.prepareCompleted(source);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000038B4 File Offset: 0x00001AB4
		[RequiredByNativeCode]
		private static void InvokeFrameReadyCallback_Internal(VideoPlayer source, long frameIdx)
		{
			bool flag = source.frameReady != null;
			if (flag)
			{
				source.frameReady(source, frameIdx);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000038E0 File Offset: 0x00001AE0
		[RequiredByNativeCode]
		private static void InvokeLoopPointReachedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.loopPointReached != null;
			if (flag)
			{
				source.loopPointReached(source);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003908 File Offset: 0x00001B08
		[RequiredByNativeCode]
		private static void InvokeStartedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.started != null;
			if (flag)
			{
				source.started(source);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003930 File Offset: 0x00001B30
		[RequiredByNativeCode]
		private static void InvokeFrameDroppedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.frameDropped != null;
			if (flag)
			{
				source.frameDropped(source);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003958 File Offset: 0x00001B58
		[RequiredByNativeCode]
		private static void InvokeErrorReceivedCallback_Internal(VideoPlayer source, string errorStr)
		{
			bool flag = source.errorReceived != null;
			if (flag)
			{
				source.errorReceived(source, errorStr);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003984 File Offset: 0x00001B84
		[RequiredByNativeCode]
		private static void InvokeSeekCompletedCallback_Internal(VideoPlayer source)
		{
			bool flag = source.seekCompleted != null;
			if (flag)
			{
				source.seekCompleted(source);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000039AC File Offset: 0x00001BAC
		[RequiredByNativeCode]
		private static void InvokeClockResyncOccurredCallback_Internal(VideoPlayer source, double seconds)
		{
			bool flag = source.clockResyncOccurred != null;
			if (flag)
			{
				source.clockResyncOccurred(source, seconds);
			}
		}

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoSource get_source_Injected(IntPtr _unity_self);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_source_Injected(IntPtr _unity_self, VideoSource value);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoTimeUpdateMode get_timeUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeUpdateMode_Injected(IntPtr _unity_self, VideoTimeUpdateMode value);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_url_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_url_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_clip_Injected(IntPtr _unity_self);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clip_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoRenderMode get_renderMode_Injected(IntPtr _unity_self);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderMode_Injected(IntPtr _unity_self, VideoRenderMode value);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetTimeUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetCamera_Injected(IntPtr _unity_self);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCamera_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetTexture_Injected(IntPtr _unity_self);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetMaterialRenderer_Injected(IntPtr _unity_self);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetMaterialRenderer_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetMaterialProperty_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetMaterialProperty_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoAspectRatio get_aspectRatio_Injected(IntPtr _unity_self);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aspectRatio_Injected(IntPtr _unity_self, VideoAspectRatio value);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_targetCameraAlpha_Injected(IntPtr _unity_self);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCameraAlpha_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Video3DLayout get_targetCamera3DLayout_Injected(IntPtr _unity_self);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCamera3DLayout_Injected(IntPtr _unity_self, Video3DLayout value);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_texture_Injected(IntPtr _unity_self);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Prepare_Injected(IntPtr _unity_self);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPrepared_Injected(IntPtr _unity_self);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_waitForFirstFrame_Injected(IntPtr _unity_self);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_waitForFirstFrame_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_playOnAwake_Injected(IntPtr _unity_self);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playOnAwake_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPlaying_Injected(IntPtr _unity_self);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPaused_Injected(IntPtr _unity_self);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetTime_Injected(IntPtr _unity_self);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_time_Injected(IntPtr _unity_self);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, double value);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long get_frame_Injected(IntPtr _unity_self);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_frame_Injected(IntPtr _unity_self, long value);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_clockTime_Injected(IntPtr _unity_self);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canStep_Injected(IntPtr _unity_self);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StepForward_Injected(IntPtr _unity_self);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetPlaybackSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_playbackSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playbackSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isLooping_Injected(IntPtr _unity_self);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isLooping_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetTimeSource_Injected(IntPtr _unity_self);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoTimeSource get_timeSource_Injected(IntPtr _unity_self);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeSource_Injected(IntPtr _unity_self, VideoTimeSource value);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoTimeReference get_timeReference_Injected(IntPtr _unity_self);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeReference_Injected(IntPtr _unity_self, VideoTimeReference value);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_externalReferenceTime_Injected(IntPtr _unity_self);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_externalReferenceTime_Injected(IntPtr _unity_self, double value);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetSkipOnDrop_Injected(IntPtr _unity_self);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_skipOnDrop_Injected(IntPtr _unity_self);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_skipOnDrop_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_frameCount_Injected(IntPtr _unity_self);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_frameRate_Injected(IntPtr _unity_self);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_length_Injected(IntPtr _unity_self);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_width_Injected(IntPtr _unity_self);

		// Token: 0x060000F2 RID: 242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_height_Injected(IntPtr _unity_self);

		// Token: 0x060000F3 RID: 243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_pixelAspectRatioNumerator_Injected(IntPtr _unity_self);

		// Token: 0x060000F4 RID: 244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_pixelAspectRatioDenominator_Injected(IntPtr _unity_self);

		// Token: 0x060000F5 RID: 245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort get_audioTrackCount_Injected(IntPtr _unity_self);

		// Token: 0x060000F6 RID: 246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAudioLanguageCode_Injected(IntPtr _unity_self, ushort trackIndex, out ManagedSpanWrapper ret);

		// Token: 0x060000F7 RID: 247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetAudioChannelCount_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000F8 RID: 248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetAudioSampleRate_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000F9 RID: 249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort GetControlledAudioTrackCount_Injected(IntPtr _unity_self);

		// Token: 0x060000FA RID: 250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetControlledAudioTrackCount_Injected(IntPtr _unity_self, ushort value);

		// Token: 0x060000FB RID: 251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableAudioTrack_Injected(IntPtr _unity_self, ushort trackIndex, bool enabled);

		// Token: 0x060000FC RID: 252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAudioTrackEnabled_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VideoAudioOutputMode get_audioOutputMode_Injected(IntPtr _unity_self);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_audioOutputMode_Injected(IntPtr _unity_self, VideoAudioOutputMode value);

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canSetDirectAudioVolume_Injected(IntPtr _unity_self);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDirectAudioVolume_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDirectAudioVolume_Injected(IntPtr _unity_self, ushort trackIndex, float volume);

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetDirectAudioMute_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDirectAudioMute_Injected(IntPtr _unity_self, ushort trackIndex, bool mute);

		// Token: 0x06000104 RID: 260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTargetAudioSource_Injected(IntPtr _unity_self, ushort trackIndex);

		// Token: 0x06000105 RID: 261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTargetAudioSource_Injected(IntPtr _unity_self, ushort trackIndex, IntPtr source);

		// Token: 0x06000106 RID: 262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sendFrameReadyEvents_Injected(IntPtr _unity_self);

		// Token: 0x06000107 RID: 263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sendFrameReadyEvents_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0200000E RID: 14
		// (Invoke) Token: 0x06000109 RID: 265
		public delegate void EventHandler(VideoPlayer source);

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x0600010D RID: 269
		public delegate void ErrorEventHandler(VideoPlayer source, string message);

		// Token: 0x02000010 RID: 16
		// (Invoke) Token: 0x06000111 RID: 273
		public delegate void FrameReadyEventHandler(VideoPlayer source, long frameIdx);

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x06000115 RID: 277
		public delegate void TimeEventHandler(VideoPlayer source, double seconds);
	}
}
