using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Audio
{
	// Token: 0x0200003D RID: 61
	[NativeHeader("Modules/Audio/Public/AudioRandomContainer.h")]
	[ExcludeFromPreset]
	internal sealed class AudioRandomContainer : AudioResource
	{
		// Token: 0x06000332 RID: 818 RVA: 0x000066AB File Offset: 0x000048AB
		internal AudioRandomContainer()
		{
			AudioRandomContainer.Internal_Create(this);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000333 RID: 819 RVA: 0x000066BC File Offset: 0x000048BC
		// (set) Token: 0x06000334 RID: 820 RVA: 0x000066E0 File Offset: 0x000048E0
		internal float volume
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_volume_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_volume_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00006704 File Offset: 0x00004904
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000672C File Offset: 0x0000492C
		internal Vector2 volumeRandomizationRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AudioRandomContainer.get_volumeRandomizationRange_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_volumeRandomizationRange_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00006750 File Offset: 0x00004950
		// (set) Token: 0x06000338 RID: 824 RVA: 0x00006774 File Offset: 0x00004974
		internal bool volumeRandomizationEnabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_volumeRandomizationEnabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_volumeRandomizationEnabled_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00006798 File Offset: 0x00004998
		// (set) Token: 0x0600033A RID: 826 RVA: 0x000067BC File Offset: 0x000049BC
		internal float pitch
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_pitch_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_pitch_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000067E0 File Offset: 0x000049E0
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00006808 File Offset: 0x00004A08
		internal Vector2 pitchRandomizationRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AudioRandomContainer.get_pitchRandomizationRange_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_pitchRandomizationRange_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000682C File Offset: 0x00004A2C
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00006850 File Offset: 0x00004A50
		internal bool pitchRandomizationEnabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_pitchRandomizationEnabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_pitchRandomizationEnabled_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00006874 File Offset: 0x00004A74
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00006898 File Offset: 0x00004A98
		internal AudioContainerElement[] elements
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_elements_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_elements_Injected(intPtr, value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000068BC File Offset: 0x00004ABC
		// (set) Token: 0x06000342 RID: 834 RVA: 0x000068E0 File Offset: 0x00004AE0
		internal AudioRandomContainerTriggerMode triggerMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_triggerMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_triggerMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00006904 File Offset: 0x00004B04
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00006928 File Offset: 0x00004B28
		internal AudioRandomContainerPlaybackMode playbackMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_playbackMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_playbackMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000694C File Offset: 0x00004B4C
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00006970 File Offset: 0x00004B70
		internal int avoidRepeatingLast
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_avoidRepeatingLast_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_avoidRepeatingLast_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00006994 File Offset: 0x00004B94
		// (set) Token: 0x06000348 RID: 840 RVA: 0x000069B8 File Offset: 0x00004BB8
		internal AudioRandomContainerAutomaticTriggerMode automaticTriggerMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_automaticTriggerMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_automaticTriggerMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000069DC File Offset: 0x00004BDC
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00006A00 File Offset: 0x00004C00
		internal float automaticTriggerTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_automaticTriggerTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_automaticTriggerTime_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00006A24 File Offset: 0x00004C24
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00006A4C File Offset: 0x00004C4C
		internal Vector2 automaticTriggerTimeRandomizationRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AudioRandomContainer.get_automaticTriggerTimeRandomizationRange_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_automaticTriggerTimeRandomizationRange_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00006A70 File Offset: 0x00004C70
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00006A94 File Offset: 0x00004C94
		internal bool automaticTriggerTimeRandomizationEnabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_automaticTriggerTimeRandomizationEnabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_automaticTriggerTimeRandomizationEnabled_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00006AB8 File Offset: 0x00004CB8
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00006ADC File Offset: 0x00004CDC
		internal AudioRandomContainerLoopMode loopMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_loopMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_loopMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00006B00 File Offset: 0x00004D00
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00006B24 File Offset: 0x00004D24
		internal int loopCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_loopCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_loopCount_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00006B48 File Offset: 0x00004D48
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00006B70 File Offset: 0x00004D70
		internal Vector2 loopCountRandomizationRange
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				AudioRandomContainer.get_loopCountRandomizationRange_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_loopCountRandomizationRange_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00006B94 File Offset: 0x00004D94
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00006BB8 File Offset: 0x00004DB8
		internal bool loopCountRandomizationEnabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioRandomContainer.get_loopCountRandomizationEnabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioRandomContainer.set_loopCountRandomizationEnabled_Injected(intPtr, value);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal void NotifyObservers(AudioRandomContainer.ChangeEventType eventType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioRandomContainer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AudioRandomContainer.NotifyObservers_Injected(intPtr, eventType);
		}

		// Token: 0x06000358 RID: 856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AudioRandomContainer self);

		// Token: 0x06000359 RID: 857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_volume_Injected(IntPtr _unity_self);

		// Token: 0x0600035A RID: 858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volume_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600035B RID: 859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_volumeRandomizationRange_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600035C RID: 860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volumeRandomizationRange_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x0600035D RID: 861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_volumeRandomizationEnabled_Injected(IntPtr _unity_self);

		// Token: 0x0600035E RID: 862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_volumeRandomizationEnabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600035F RID: 863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pitch_Injected(IntPtr _unity_self);

		// Token: 0x06000360 RID: 864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pitch_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000361 RID: 865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pitchRandomizationRange_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000362 RID: 866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pitchRandomizationRange_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000363 RID: 867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_pitchRandomizationEnabled_Injected(IntPtr _unity_self);

		// Token: 0x06000364 RID: 868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pitchRandomizationEnabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000365 RID: 869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioContainerElement[] get_elements_Injected(IntPtr _unity_self);

		// Token: 0x06000366 RID: 870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_elements_Injected(IntPtr _unity_self, AudioContainerElement[] value);

		// Token: 0x06000367 RID: 871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioRandomContainerTriggerMode get_triggerMode_Injected(IntPtr _unity_self);

		// Token: 0x06000368 RID: 872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_triggerMode_Injected(IntPtr _unity_self, AudioRandomContainerTriggerMode value);

		// Token: 0x06000369 RID: 873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioRandomContainerPlaybackMode get_playbackMode_Injected(IntPtr _unity_self);

		// Token: 0x0600036A RID: 874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playbackMode_Injected(IntPtr _unity_self, AudioRandomContainerPlaybackMode value);

		// Token: 0x0600036B RID: 875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_avoidRepeatingLast_Injected(IntPtr _unity_self);

		// Token: 0x0600036C RID: 876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_avoidRepeatingLast_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600036D RID: 877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioRandomContainerAutomaticTriggerMode get_automaticTriggerMode_Injected(IntPtr _unity_self);

		// Token: 0x0600036E RID: 878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticTriggerMode_Injected(IntPtr _unity_self, AudioRandomContainerAutomaticTriggerMode value);

		// Token: 0x0600036F RID: 879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_automaticTriggerTime_Injected(IntPtr _unity_self);

		// Token: 0x06000370 RID: 880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticTriggerTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000371 RID: 881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_automaticTriggerTimeRandomizationRange_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000372 RID: 882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticTriggerTimeRandomizationRange_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x06000373 RID: 883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_automaticTriggerTimeRandomizationEnabled_Injected(IntPtr _unity_self);

		// Token: 0x06000374 RID: 884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_automaticTriggerTimeRandomizationEnabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000375 RID: 885
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioRandomContainerLoopMode get_loopMode_Injected(IntPtr _unity_self);

		// Token: 0x06000376 RID: 886
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopMode_Injected(IntPtr _unity_self, AudioRandomContainerLoopMode value);

		// Token: 0x06000377 RID: 887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_loopCount_Injected(IntPtr _unity_self);

		// Token: 0x06000378 RID: 888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000379 RID: 889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_loopCountRandomizationRange_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600037A RID: 890
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopCountRandomizationRange_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x0600037B RID: 891
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_loopCountRandomizationEnabled_Injected(IntPtr _unity_self);

		// Token: 0x0600037C RID: 892
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_loopCountRandomizationEnabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600037D RID: 893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NotifyObservers_Injected(IntPtr _unity_self, AudioRandomContainer.ChangeEventType eventType);

		// Token: 0x0200003E RID: 62
		internal enum ChangeEventType
		{
			// Token: 0x04000090 RID: 144
			Volume,
			// Token: 0x04000091 RID: 145
			Pitch,
			// Token: 0x04000092 RID: 146
			List
		}
	}
}
