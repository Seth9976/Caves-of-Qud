using System;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	// Token: 0x02000035 RID: 53
	public static class AudioPlayableBinding
	{
		// Token: 0x06000310 RID: 784 RVA: 0x00006390 File Offset: 0x00004590
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(AudioSource), new PlayableBinding.CreateOutputMethod(AudioPlayableBinding.CreateAudioOutput));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000063C0 File Offset: 0x000045C0
		private static PlayableOutput CreateAudioOutput(PlayableGraph graph, string name)
		{
			return AudioPlayableOutput.Create(graph, name, null);
		}
	}
}
