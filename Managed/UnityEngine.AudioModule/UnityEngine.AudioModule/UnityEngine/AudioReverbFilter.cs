using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001E RID: 30
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioReverbFilter : Behaviour
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000045D0 File Offset: 0x000027D0
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000045F4 File Offset: 0x000027F4
		public AudioReverbPreset reverbPreset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_reverbPreset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_reverbPreset_Injected(intPtr, value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004618 File Offset: 0x00002818
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000463C File Offset: 0x0000283C
		public float dryLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_dryLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_dryLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004660 File Offset: 0x00002860
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00004684 File Offset: 0x00002884
		public float room
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_room_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_room_Injected(intPtr, value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000046A8 File Offset: 0x000028A8
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000046CC File Offset: 0x000028CC
		public float roomHF
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_roomHF_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_roomHF_Injected(intPtr, value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000046F0 File Offset: 0x000028F0
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00003FE6 File Offset: 0x000021E6
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004714 File Offset: 0x00002914
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00004738 File Offset: 0x00002938
		public float decayTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_decayTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_decayTime_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000475C File Offset: 0x0000295C
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00004780 File Offset: 0x00002980
		public float decayHFRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_decayHFRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_decayHFRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000047A4 File Offset: 0x000029A4
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000047C8 File Offset: 0x000029C8
		public float reflectionsLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_reflectionsLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_reflectionsLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000047EC File Offset: 0x000029EC
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00004810 File Offset: 0x00002A10
		public float reflectionsDelay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_reflectionsDelay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_reflectionsDelay_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004834 File Offset: 0x00002A34
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00004858 File Offset: 0x00002A58
		public float reverbLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_reverbLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_reverbLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000487C File Offset: 0x00002A7C
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x000048A0 File Offset: 0x00002AA0
		public float reverbDelay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_reverbDelay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_reverbDelay_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x000048E8 File Offset: 0x00002AE8
		public float diffusion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_diffusion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_diffusion_Injected(intPtr, value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000490C File Offset: 0x00002B0C
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00004930 File Offset: 0x00002B30
		public float density
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_density_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_density_Injected(intPtr, value);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004954 File Offset: 0x00002B54
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00004978 File Offset: 0x00002B78
		public float hfReference
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_hfReference_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_hfReference_Injected(intPtr, value);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000499C File Offset: 0x00002B9C
		// (set) Token: 0x060001DE RID: 478 RVA: 0x000049C0 File Offset: 0x00002BC0
		public float roomLF
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_roomLF_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_roomLF_Injected(intPtr, value);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000049E4 File Offset: 0x00002BE4
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00004A08 File Offset: 0x00002C08
		public float lfReference
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioReverbFilter.get_lfReference_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioReverbFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioReverbFilter.set_lfReference_Injected(intPtr, value);
			}
		}

		// Token: 0x060001E2 RID: 482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioReverbPreset get_reverbPreset_Injected(IntPtr _unity_self);

		// Token: 0x060001E3 RID: 483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbPreset_Injected(IntPtr _unity_self, AudioReverbPreset value);

		// Token: 0x060001E4 RID: 484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dryLevel_Injected(IntPtr _unity_self);

		// Token: 0x060001E5 RID: 485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dryLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001E6 RID: 486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_room_Injected(IntPtr _unity_self);

		// Token: 0x060001E7 RID: 487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_room_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001E8 RID: 488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_roomHF_Injected(IntPtr _unity_self);

		// Token: 0x060001E9 RID: 489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_roomHF_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001EA RID: 490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_decayTime_Injected(IntPtr _unity_self);

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_decayTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_decayHFRatio_Injected(IntPtr _unity_self);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_decayHFRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reflectionsLevel_Injected(IntPtr _unity_self);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reflectionsLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reflectionsDelay_Injected(IntPtr _unity_self);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reflectionsDelay_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F2 RID: 498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reverbLevel_Injected(IntPtr _unity_self);

		// Token: 0x060001F3 RID: 499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbLevel_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F4 RID: 500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_reverbDelay_Injected(IntPtr _unity_self);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_reverbDelay_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_diffusion_Injected(IntPtr _unity_self);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_diffusion_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_density_Injected(IntPtr _unity_self);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_density_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001FA RID: 506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_hfReference_Injected(IntPtr _unity_self);

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hfReference_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001FC RID: 508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_roomLF_Injected(IntPtr _unity_self);

		// Token: 0x060001FD RID: 509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_roomLF_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001FE RID: 510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_lfReference_Injected(IntPtr _unity_self);

		// Token: 0x060001FF RID: 511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lfReference_Injected(IntPtr _unity_self, float value);
	}
}
