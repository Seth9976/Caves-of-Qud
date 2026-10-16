using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioDistortionFilter : Behaviour
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00004264 File Offset: 0x00002464
		public float distortionLevel
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioDistortionFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AudioDistortionFilter.get_distortionLevel_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioDistortionFilter>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AudioDistortionFilter.set_distortionLevel_Injected(intPtr, value);
			}
		}

		// Token: 0x0600018F RID: 399
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_distortionLevel_Injected(IntPtr _unity_self);

		// Token: 0x06000190 RID: 400
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_distortionLevel_Injected(IntPtr _unity_self, float value);
	}
}
