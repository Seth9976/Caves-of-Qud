using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001C RID: 28
	internal static class CurveEditUtility
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00007574 File Offset: 0x00005774
		public static AnimationCurve CreateMatchingCurve(AnimationCurve curve)
		{
			Keyframe[] keys = curve.keys;
			for (int num = 0; num != keys.Length; num++)
			{
				if (!float.IsPositiveInfinity(keys[num].inTangent))
				{
					keys[num].inTangent = -keys[num].inTangent;
				}
				if (!float.IsPositiveInfinity(keys[num].outTangent))
				{
					keys[num].outTangent = -keys[num].outTangent;
				}
				keys[num].value = 1f - keys[num].value;
			}
			return new AnimationCurve(keys);
		}
	}
}
