using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000018 RID: 24
	[NativeHeader("Modules/Audio/Public/AudioReverbZone.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class AudioReverbZone : Behaviour
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00003BD4 File Offset: 0x00001DD4
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public float minDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_minDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_minDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00003C1C File Offset: 0x00001E1C
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00003C40 File Offset: 0x00001E40
		public float maxDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_maxDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_maxDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003C64 File Offset: 0x00001E64
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003C88 File Offset: 0x00001E88
		public AudioReverbPreset reverbPreset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_reverbPreset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_reverbPreset_Injected(intPtr, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003CAC File Offset: 0x00001EAC
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public int room
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_room_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_room_Injected(intPtr, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003CF4 File Offset: 0x00001EF4
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003D18 File Offset: 0x00001F18
		public int roomHF
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_roomHF_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_roomHF_Injected(intPtr, value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003D3C File Offset: 0x00001F3C
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003D60 File Offset: 0x00001F60
		public int roomLF
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_roomLF_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_roomLF_Injected(intPtr, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003D84 File Offset: 0x00001F84
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public float decayTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_decayTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_decayTime_Injected(intPtr, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00003DCC File Offset: 0x00001FCC
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00003DF0 File Offset: 0x00001FF0
		public float decayHFRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_decayHFRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_decayHFRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003E14 File Offset: 0x00002014
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00003E38 File Offset: 0x00002038
		public int reflections
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_reflections_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_reflections_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003E5C File Offset: 0x0000205C
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00003E80 File Offset: 0x00002080
		public float reflectionsDelay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_reflectionsDelay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_reflectionsDelay_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003EA4 File Offset: 0x000020A4
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00003EC8 File Offset: 0x000020C8
		public int reverb
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_reverb_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_reverb_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00003EEC File Offset: 0x000020EC
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00003F10 File Offset: 0x00002110
		public float reverbDelay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_reverbDelay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_reverbDelay_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00003F34 File Offset: 0x00002134
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00003F58 File Offset: 0x00002158
		public float HFReference
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_HFReference_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_HFReference_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00003F7C File Offset: 0x0000217C
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00003FA0 File Offset: 0x000021A0
		public float LFReference
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_LFReference_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_LFReference_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003FC4 File Offset: 0x000021C4
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00003FE6 File Offset: 0x000021E6
		[Obsolete("Warning! roomRolloffFactor is no longer supported.")]
		public float roomRolloffFactor
		{
			get
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
				return 10f;
			}
			set
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00003FF4 File Offset: 0x000021F4
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004018 File Offset: 0x00002218
		public float diffusion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_diffusion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_diffusion_Injected(intPtr, value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000403C File Offset: 0x0000223C
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004060 File Offset: 0x00002260
		public float density
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbZone.get_density_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbZone>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbZone.set_density_Injected(intPtr, value);
			}
		}

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_minDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_minDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maxDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioReverbPreset get_reverbPreset_Injected(IntPtr _unity_self);

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbPreset_Injected(IntPtr _unity_self, AudioReverbPreset value);

		// Token: 0x0600015A RID: 346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_room_Injected(IntPtr _unity_self);

		// Token: 0x0600015B RID: 347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_room_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600015C RID: 348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_roomHF_Injected(IntPtr _unity_self);

		// Token: 0x0600015D RID: 349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_roomHF_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600015E RID: 350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_roomLF_Injected(IntPtr _unity_self);

		// Token: 0x0600015F RID: 351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_roomLF_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_decayTime_Injected(IntPtr _unity_self);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_decayTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000162 RID: 354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_decayHFRatio_Injected(IntPtr _unity_self);

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_decayHFRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_reflections_Injected(IntPtr _unity_self);

		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reflections_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reflectionsDelay_Injected(IntPtr _unity_self);

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reflectionsDelay_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000168 RID: 360
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_reverb_Injected(IntPtr _unity_self);

		// Token: 0x06000169 RID: 361
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverb_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600016A RID: 362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reverbDelay_Injected(IntPtr _unity_self);

		// Token: 0x0600016B RID: 363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbDelay_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600016C RID: 364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_HFReference_Injected(IntPtr _unity_self);

		// Token: 0x0600016D RID: 365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_HFReference_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600016E RID: 366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_LFReference_Injected(IntPtr _unity_self);

		// Token: 0x0600016F RID: 367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_LFReference_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000170 RID: 368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_diffusion_Injected(IntPtr _unity_self);

		// Token: 0x06000171 RID: 369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_diffusion_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_density_Injected(IntPtr _unity_self);

		// Token: 0x06000173 RID: 371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_density_Injected(IntPtr _unity_self, float value);
	}
}
