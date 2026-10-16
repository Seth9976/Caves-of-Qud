using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001D RID: 29
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioChorusFilter : Behaviour
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000043A8 File Offset: 0x000025A8
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000043CC File Offset: 0x000025CC
		public float dryMix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_dryMix_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_dryMix_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000043F0 File Offset: 0x000025F0
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00004414 File Offset: 0x00002614
		public float wetMix1
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_wetMix1_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_wetMix1_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00004438 File Offset: 0x00002638
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000445C File Offset: 0x0000265C
		public float wetMix2
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_wetMix2_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_wetMix2_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004480 File Offset: 0x00002680
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000044A4 File Offset: 0x000026A4
		public float wetMix3
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_wetMix3_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_wetMix3_Injected(intPtr, value);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000044C8 File Offset: 0x000026C8
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000044EC File Offset: 0x000026EC
		public float delay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_delay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_delay_Injected(intPtr, value);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004510 File Offset: 0x00002710
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00004534 File Offset: 0x00002734
		public float rate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_rate_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_rate_Injected(intPtr, value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00004558 File Offset: 0x00002758
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000457C File Offset: 0x0000277C
		public float depth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioChorusFilter.get_depth_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioChorusFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioChorusFilter.set_depth_Injected(intPtr, value);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000045A0 File Offset: 0x000027A0
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000045C2 File Offset: 0x000027C2
		[Obsolete("Warning! Feedback is deprecated. This property does nothing.")]
		public float feedback
		{
			get
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
				return 0f;
			}
			set
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
			}
		}

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dryMix_Injected(IntPtr _unity_self);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dryMix_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wetMix1_Injected(IntPtr _unity_self);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wetMix1_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wetMix2_Injected(IntPtr _unity_self);

		// Token: 0x060001B8 RID: 440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wetMix2_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B9 RID: 441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wetMix3_Injected(IntPtr _unity_self);

		// Token: 0x060001BA RID: 442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wetMix3_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001BB RID: 443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_delay_Injected(IntPtr _unity_self);

		// Token: 0x060001BC RID: 444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_delay_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001BD RID: 445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rate_Injected(IntPtr _unity_self);

		// Token: 0x060001BE RID: 446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rate_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001BF RID: 447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_depth_Injected(IntPtr _unity_self);

		// Token: 0x060001C0 RID: 448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_depth_Injected(IntPtr _unity_self, float value);
	}
}
