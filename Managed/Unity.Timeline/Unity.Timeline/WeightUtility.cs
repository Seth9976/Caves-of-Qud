using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200005A RID: 90
	internal static class WeightUtility
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000BA28 File Offset: 0x00009C28
		public static float NormalizeMixer(Playable mixer)
		{
			if (!mixer.IsValid<Playable>())
			{
				return 0f;
			}
			int inputCount = mixer.GetInputCount<Playable>();
			float num = 0f;
			for (int i = 0; i < inputCount; i++)
			{
				num += mixer.GetInputWeight(i);
			}
			if (num > Mathf.Epsilon && num < 1f)
			{
				for (int j = 0; j < inputCount; j++)
				{
					mixer.SetInputWeight(j, mixer.GetInputWeight(j) / num);
				}
			}
			return Mathf.Clamp01(num);
		}
	}
}
