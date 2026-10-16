using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class PlayableTrack : TrackAsset
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x00009DB6 File Offset: 0x00007FB6
		protected override void OnCreateClip(TimelineClip clip)
		{
			if (clip.asset != null)
			{
				clip.displayName = clip.asset.GetType().Name;
			}
		}
	}
}
