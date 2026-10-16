using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioLowPassFilter : Behaviour
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00004084 File Offset: 0x00002284
		private AnimationCurve GetCustomLowpassLevelCurveCopy()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioLowPassFilter>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr customLowpassLevelCurveCopy_Injected = AudioLowPassFilter.GetCustomLowpassLevelCurveCopy_Injected(intPtr);
			return (customLowpassLevelCurveCopy_Injected == 0) ? null : AnimationCurve.BindingsMarshaller.ConvertToManaged(customLowpassLevelCurveCopy_Injected);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000040B4 File Offset: 0x000022B4
		[NativeThrows]
		[NativeMethod(Name = "AudioLowPassFilterBindings::SetCustomLowpassLevelCurveHelper", IsFreeFunction = true)]
		private static void SetCustomLowpassLevelCurveHelper([NotNull] AudioLowPassFilter source, AnimationCurve curve)
		{
			if (source == null)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioLowPassFilter>(source);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(source, "source");
			}
			AudioLowPassFilter.SetCustomLowpassLevelCurveHelper_Injected(intPtr, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000040FC File Offset: 0x000022FC
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00004114 File Offset: 0x00002314
		public AnimationCurve customCutoffCurve
		{
			get
			{
				return this.GetCustomLowpassLevelCurveCopy();
			}
			set
			{
				AudioLowPassFilter.SetCustomLowpassLevelCurveHelper(this, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004120 File Offset: 0x00002320
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00004144 File Offset: 0x00002344
		public float cutoffFrequency
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioLowPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioLowPassFilter.get_cutoffFrequency_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioLowPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioLowPassFilter.set_cutoffFrequency_Injected(intPtr, value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004168 File Offset: 0x00002368
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000418C File Offset: 0x0000238C
		public float lowpassResonanceQ
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioLowPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioLowPassFilter.get_lowpassResonanceQ_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioLowPassFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioLowPassFilter.set_lowpassResonanceQ_Injected(intPtr, value);
			}
		}

		// Token: 0x0600017D RID: 381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCustomLowpassLevelCurveCopy_Injected(IntPtr _unity_self);

		// Token: 0x0600017E RID: 382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomLowpassLevelCurveHelper_Injected(IntPtr source, IntPtr curve);

		// Token: 0x0600017F RID: 383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_cutoffFrequency_Injected(IntPtr _unity_self);

		// Token: 0x06000180 RID: 384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cutoffFrequency_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000181 RID: 385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_lowpassResonanceQ_Injected(IntPtr _unity_self);

		// Token: 0x06000182 RID: 386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lowpassResonanceQ_Injected(IntPtr _unity_self, float value);
	}
}
