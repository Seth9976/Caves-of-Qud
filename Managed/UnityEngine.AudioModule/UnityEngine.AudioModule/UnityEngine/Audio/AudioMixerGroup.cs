using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine.Audio
{
	// Token: 0x02000032 RID: 50
	[NativeHeader("Modules/Audio/Public/AudioMixerGroup.h")]
	public class AudioMixerGroup : Object, ISubAssetNotDuplicatable
	{
		// Token: 0x06000301 RID: 769 RVA: 0x00005913 File Offset: 0x00003B13
		internal AudioMixerGroup()
		{
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00006218 File Offset: 0x00004418
		[NativeProperty]
		public AudioMixer audioMixer
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AudioMixerGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<AudioMixer>(AudioMixerGroup.get_audioMixer_Injected(intPtr));
			}
		}

		// Token: 0x06000303 RID: 771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_audioMixer_Injected(IntPtr _unity_self);
	}
}
