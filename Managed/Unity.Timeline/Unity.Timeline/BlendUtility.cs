using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004F RID: 79
	internal static class BlendUtility
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0000A78C File Offset: 0x0000898C
		private static bool Overlaps(TimelineClip blendOut, TimelineClip blendIn)
		{
			if (blendIn == blendOut)
			{
				return false;
			}
			if (Math.Abs(blendIn.start - blendOut.start) < TimeUtility.kTimeEpsilon)
			{
				return blendIn.duration >= blendOut.duration;
			}
			return blendIn.start >= blendOut.start && blendIn.start < blendOut.end;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A7E8 File Offset: 0x000089E8
		public static void ComputeBlendsFromOverlaps(TimelineClip[] clips)
		{
			foreach (TimelineClip timelineClip in clips)
			{
				timelineClip.blendInDuration = -1.0;
				timelineClip.blendOutDuration = -1.0;
			}
			Array.Sort<TimelineClip>(clips, delegate(TimelineClip c1, TimelineClip c2)
			{
				if (Math.Abs(c1.start - c2.start) >= TimeUtility.kTimeEpsilon)
				{
					return c1.start.CompareTo(c2.start);
				}
				return c1.duration.CompareTo(c2.duration);
			});
			for (int j = 0; j < clips.Length; j++)
			{
				TimelineClip timelineClip2 = clips[j];
				if (timelineClip2.SupportsBlending())
				{
					TimelineClip timelineClip3 = timelineClip2;
					TimelineClip timelineClip4 = null;
					TimelineClip timelineClip5 = clips[Math.Max(j - 1, 0)];
					if (BlendUtility.Overlaps(timelineClip5, timelineClip3))
					{
						timelineClip4 = timelineClip5;
					}
					if (timelineClip4 != null)
					{
						BlendUtility.UpdateClipIntersection(timelineClip4, timelineClip3);
					}
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000A894 File Offset: 0x00008A94
		private static void UpdateClipIntersection(TimelineClip blendOutClip, TimelineClip blendInClip)
		{
			if (!blendOutClip.SupportsBlending() || !blendInClip.SupportsBlending())
			{
				return;
			}
			if (blendInClip.start - blendOutClip.start < blendOutClip.duration - blendInClip.duration)
			{
				return;
			}
			double num = Math.Max(0.0, blendOutClip.start + blendOutClip.duration - blendInClip.start);
			blendOutClip.blendOutDuration = num;
			blendInClip.blendInDuration = num;
			TimelineClip.BlendCurveMode blendInCurveMode = blendInClip.blendInCurveMode;
			TimelineClip.BlendCurveMode blendOutCurveMode = blendOutClip.blendOutCurveMode;
			if (blendInCurveMode == TimelineClip.BlendCurveMode.Manual && blendOutCurveMode == TimelineClip.BlendCurveMode.Auto)
			{
				blendOutClip.mixOutCurve = CurveEditUtility.CreateMatchingCurve(blendInClip.mixInCurve);
				return;
			}
			if (blendInCurveMode == TimelineClip.BlendCurveMode.Auto && blendOutCurveMode == TimelineClip.BlendCurveMode.Manual)
			{
				blendInClip.mixInCurve = CurveEditUtility.CreateMatchingCurve(blendOutClip.mixOutCurve);
				return;
			}
			if (blendInCurveMode == TimelineClip.BlendCurveMode.Auto && blendOutCurveMode == TimelineClip.BlendCurveMode.Auto)
			{
				blendInClip.mixInCurve = null;
				blendOutClip.mixOutCurve = null;
			}
		}
	}
}
