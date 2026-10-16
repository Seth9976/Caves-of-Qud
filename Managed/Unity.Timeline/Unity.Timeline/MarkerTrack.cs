using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002A RID: 42
	[TrackBindingType(typeof(GameObject))]
	[HideInMenu]
	[ExcludeFromPreset]
	[Serializable]
	public class MarkerTrack : TrackAsset
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00008784 File Offset: 0x00006984
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				TimelineAsset timelineAsset = base.timelineAsset;
				if (!(this == ((timelineAsset != null) ? timelineAsset.markerTrack : null)))
				{
					return base.outputs;
				}
				return new List<PlayableBinding> { ScriptPlayableBinding.Create(base.name, null, typeof(GameObject)) };
			}
		}
	}
}
