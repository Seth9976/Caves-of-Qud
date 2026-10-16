using System;
using UnityEngine.Playables;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200004B RID: 75
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationWindowPreview
	{
		// Token: 0x060003B6 RID: 950
		void StartPreview();

		// Token: 0x060003B7 RID: 951
		void StopPreview();

		// Token: 0x060003B8 RID: 952
		void UpdatePreviewGraph(PlayableGraph graph);

		// Token: 0x060003B9 RID: 953
		Playable BuildPreviewGraph(PlayableGraph graph, Playable inputPlayable);
	}
}
