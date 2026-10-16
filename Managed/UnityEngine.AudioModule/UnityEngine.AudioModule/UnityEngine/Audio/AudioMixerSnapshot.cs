using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Audio/Public/AudioMixerSnapshot.h")]
	public class AudioMixerSnapshot : Object, ISubAssetNotDuplicatable
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00005913 File Offset: 0x00003B13
		internal AudioMixerSnapshot()
		{
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00006358 File Offset: 0x00004558
		[NativeProperty]
		public AudioMixer audioMixer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixerSnapshot>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioMixer>(AudioMixerSnapshot.get_audioMixer_Injected(intPtr));
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000637F File Offset: 0x0000457F
		public void TransitionTo(float timeToReach)
		{
			this.audioMixer.TransitionToSnapshot(this, timeToReach);
		}

		// Token: 0x0600030F RID: 783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_audioMixer_Injected(IntPtr _unity_self);
	}
}
