using System;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006B2 RID: 1714
	internal struct LayoutDefaults
	{
		// Token: 0x060031F8 RID: 12792 RVA: 0x000BCAD4 File Offset: 0x000BACD4
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static LayoutDefaults()
		{
			FixedBuffer9<LayoutValue> fixedBuffer = default(FixedBuffer9<LayoutValue>);
			*fixedBuffer[0] = LayoutValue.Undefined();
			*fixedBuffer[1] = LayoutValue.Undefined();
			*fixedBuffer[2] = LayoutValue.Undefined();
			*fixedBuffer[3] = LayoutValue.Undefined();
			*fixedBuffer[4] = LayoutValue.Undefined();
			*fixedBuffer[5] = LayoutValue.Undefined();
			*fixedBuffer[6] = LayoutValue.Undefined();
			*fixedBuffer[7] = LayoutValue.Undefined();
			*fixedBuffer[8] = LayoutValue.Undefined();
			LayoutDefaults.EdgeValuesUnit = fixedBuffer;
			LayoutDefaults.DimensionValues = new float[] { float.NaN, float.NaN };
			FixedBuffer2<LayoutValue> fixedBuffer2 = default(FixedBuffer2<LayoutValue>);
			*fixedBuffer2[0] = LayoutValue.Undefined();
			*fixedBuffer2[1] = LayoutValue.Undefined();
			LayoutDefaults.DimensionValuesUnit = fixedBuffer2;
			fixedBuffer2 = default(FixedBuffer2<LayoutValue>);
			*fixedBuffer2[0] = LayoutValue.Auto();
			*fixedBuffer2[1] = LayoutValue.Auto();
			LayoutDefaults.DimensionValuesAutoUnit = fixedBuffer2;
		}

		// Token: 0x040018B5 RID: 6325
		public static readonly FixedBuffer9<LayoutValue> EdgeValuesUnit;

		// Token: 0x040018B6 RID: 6326
		public static readonly float[] DimensionValues;

		// Token: 0x040018B7 RID: 6327
		public static readonly FixedBuffer2<LayoutValue> DimensionValuesUnit;

		// Token: 0x040018B8 RID: 6328
		public static readonly FixedBuffer2<LayoutValue> DimensionValuesAutoUnit;
	}
}
