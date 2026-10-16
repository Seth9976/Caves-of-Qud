using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000033 RID: 51
	public interface ILayerable
	{
		// Token: 0x0600026A RID: 618
		Playable CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount);
	}
}
