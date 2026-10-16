using System;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000045 RID: 69
	public static class AnimationPlayableBinding
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00007EC4 File Offset: 0x000060C4
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), new PlayableBinding.CreateOutputMethod(AnimationPlayableBinding.CreateAnimationOutput));
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00007EF4 File Offset: 0x000060F4
		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
}
