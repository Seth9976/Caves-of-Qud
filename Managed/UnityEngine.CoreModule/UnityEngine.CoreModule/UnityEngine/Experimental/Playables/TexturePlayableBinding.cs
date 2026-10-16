using System;
using UnityEngine.Playables;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x0200058A RID: 1418
	public static class TexturePlayableBinding
	{
		// Token: 0x060036E1 RID: 14049 RVA: 0x0006C584 File Offset: 0x0006A784
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(RenderTexture), new PlayableBinding.CreateOutputMethod(TexturePlayableBinding.CreateTextureOutput));
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x0006C5B4 File Offset: 0x0006A7B4
		private static PlayableOutput CreateTextureOutput(PlayableGraph graph, string name)
		{
			return TexturePlayableOutput.Create(graph, name, null);
		}
	}
}
