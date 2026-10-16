using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000032 RID: 50
	[TrackClipType(typeof(TrackAsset))]
	[SupportsChildTracks(null, 2147483647)]
	[ExcludeFromPreset]
	[Serializable]
	public class GroupTrack : TrackAsset
	{
		// Token: 0x06000267 RID: 615 RVA: 0x00008CA9 File Offset: 0x00006EA9
		internal override bool CanCompileClips()
		{
			return false;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008CAC File Offset: 0x00006EAC
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}
	}
}
