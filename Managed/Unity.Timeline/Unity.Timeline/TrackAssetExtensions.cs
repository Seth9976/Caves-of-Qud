using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000031 RID: 49
	public static class TrackAssetExtensions
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00008BAB File Offset: 0x00006DAB
		public static GroupTrack GetGroup(this TrackAsset asset)
		{
			if (asset == null)
			{
				return null;
			}
			return asset.parent as GroupTrack;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008BC4 File Offset: 0x00006DC4
		public static void SetGroup(this TrackAsset asset, GroupTrack group)
		{
			if (asset == null || asset == group || asset.parent == group)
			{
				return;
			}
			if (group != null && asset.timelineAsset != group.timelineAsset)
			{
				throw new InvalidOperationException("Cannot assign to a group in a different timeline");
			}
			TimelineAsset timelineAsset = asset.timelineAsset;
			TrackAsset trackAsset = asset.parent as TrackAsset;
			TimelineAsset timelineAsset2 = asset.parent as TimelineAsset;
			if (trackAsset != null || timelineAsset2 != null)
			{
				if (timelineAsset2 != null)
				{
					timelineAsset2.RemoveTrack(asset);
				}
				else
				{
					trackAsset.RemoveSubTrack(asset);
				}
			}
			if (group == null)
			{
				asset.parent = asset.timelineAsset;
				timelineAsset.AddTrackInternal(asset);
				return;
			}
			group.AddChild(asset);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00008C88 File Offset: 0x00006E88
		internal static void ComputeBlendsFromOverlaps(this TrackAsset asset, bool force = false)
		{
			if (!asset.blendsValid || force)
			{
				BlendUtility.ComputeBlendsFromOverlaps(asset.clips);
				asset.blendsValid = true;
			}
		}
	}
}
