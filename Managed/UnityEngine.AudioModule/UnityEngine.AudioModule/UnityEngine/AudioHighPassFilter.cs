using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioHighPassFilter : Behaviour
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000041B0 File Offset: 0x000023B0
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000041D4 File Offset: 0x000023D4
		public float cutoffFrequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioHighPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioHighPassFilter.get_cutoffFrequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioHighPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioHighPassFilter.set_cutoffFrequency_Injected(intPtr, value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000041F8 File Offset: 0x000023F8
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000421C File Offset: 0x0000241C
		public float highpassResonanceQ
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioHighPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioHighPassFilter.get_highpassResonanceQ_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioHighPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioHighPassFilter.set_highpassResonanceQ_Injected(intPtr, value);
			}
		}

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_cutoffFrequency_Injected(IntPtr _unity_self);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cutoffFrequency_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_highpassResonanceQ_Injected(IntPtr _unity_self);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_highpassResonanceQ_Injected(IntPtr _unity_self, float value);
	}
}
