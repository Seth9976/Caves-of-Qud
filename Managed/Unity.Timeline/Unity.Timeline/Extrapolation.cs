using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000050 RID: 80
	internal static class Extrapolation
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000A954 File Offset: 0x00008B54
		internal static void CalculateExtrapolationTimes(this TrackAsset asset)
		{
			TimelineClip[] clips = asset.clips;
			if (clips == null || clips.Length == 0)
			{
				return;
			}
			if (!clips[0].SupportsExtrapolation())
			{
				return;
			}
			TimelineClip[] array = Extrapolation.SortClipsByStartTime(clips);
			if (array.Length != 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					double num = double.PositiveInfinity;
					for (int j = 0; j < array.Length; j++)
					{
						if (i != j)
						{
							double num2 = array[j].start - array[i].end;
							if (num2 >= -TimeUtility.kTimeEpsilon && num2 < num)
							{
								num = Math.Min(num, num2);
							}
							if (array[j].start <= array[i].end && array[j].end > array[i].end)
							{
								num = 0.0;
							}
						}
					}
					num = ((num <= Extrapolation.kMinExtrapolationTime) ? 0.0 : num);
					array[i].SetPostExtrapolationTime(num);
				}
				array[0].SetPreExtrapolationTime(Math.Max(0.0, array[0].start));
				for (int k = 1; k < array.Length; k++)
				{
					double num3 = 0.0;
					int num4 = -1;
					for (int l = 0; l < k; l++)
					{
						if (array[l].end > array[k].start)
						{
							num4 = -1;
							num3 = 0.0;
							break;
						}
						double num5 = array[k].start - array[l].end;
						if (num4 == -1 || num5 < num3)
						{
							num3 = num5;
							num4 = l;
						}
					}
					if (num4 >= 0 && array[num4].postExtrapolationMode != TimelineClip.ClipExtrapolation.None)
					{
						num3 = 0.0;
					}
					num3 = ((num3 <= Extrapolation.kMinExtrapolationTime) ? 0.0 : num3);
					array[k].SetPreExtrapolationTime(num3);
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000AB18 File Offset: 0x00008D18
		private static TimelineClip[] SortClipsByStartTime(TimelineClip[] clips)
		{
			TimelineClip[] array = new TimelineClip[clips.Length];
			Array.Copy(clips, array, clips.Length);
			Array.Sort<TimelineClip>(array, (TimelineClip clip1, TimelineClip clip2) => clip1.start.CompareTo(clip2.start));
			return array;
		}

		// Token: 0x0400010D RID: 269
		internal static readonly double kMinExtrapolationTime = TimeUtility.kTimeEpsilon * 1000.0;
	}
}
