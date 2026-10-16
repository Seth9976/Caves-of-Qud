using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioEchoFilter : Behaviour
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004288 File Offset: 0x00002488
		// (set) Token: 0x06000192 RID: 402 RVA: 0x000042AC File Offset: 0x000024AC
		public float delay
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioEchoFilter.get_delay_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioEchoFilter.set_delay_Injected(intPtr, value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000042D0 File Offset: 0x000024D0
		// (set) Token: 0x06000194 RID: 404 RVA: 0x000042F4 File Offset: 0x000024F4
		public float decayRatio
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioEchoFilter.get_decayRatio_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioEchoFilter.set_decayRatio_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004318 File Offset: 0x00002518
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000433C File Offset: 0x0000253C
		public float dryMix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioEchoFilter.get_dryMix_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioEchoFilter.set_dryMix_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004360 File Offset: 0x00002560
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00004384 File Offset: 0x00002584
		public float wetMix
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioEchoFilter.get_wetMix_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioEchoFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioEchoFilter.set_wetMix_Injected(intPtr, value);
			}
		}

		// Token: 0x0600019A RID: 410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_delay_Injected(IntPtr _unity_self);

		// Token: 0x0600019B RID: 411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_delay_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600019C RID: 412
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_decayRatio_Injected(IntPtr _unity_self);

		// Token: 0x0600019D RID: 413
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_decayRatio_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600019E RID: 414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_dryMix_Injected(IntPtr _unity_self);

		// Token: 0x0600019F RID: 415
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_dryMix_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001A0 RID: 416
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wetMix_Injected(IntPtr _unity_self);

		// Token: 0x060001A1 RID: 417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wetMix_Injected(IntPtr _unity_self, float value);
	}
}
