using System;
using System.Collections.Generic;

namespace UnityEngine.Playables
{
	// Token: 0x0200044D RID: 1101
	public interface IPlayableAsset
	{
		// Token: 0x06002A48 RID: 10824
		Playable CreatePlayable(PlayableGraph graph, GameObject owner);

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002A49 RID: 10825
		double duration { get; }

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002A4A RID: 10826
		IEnumerable<PlayableBinding> outputs { get; }
	}
}
