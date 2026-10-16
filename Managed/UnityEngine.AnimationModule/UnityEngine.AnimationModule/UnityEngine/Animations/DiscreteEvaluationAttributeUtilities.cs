using System;

namespace UnityEngine.Animations
{
	// Token: 0x02000047 RID: 71
	internal static class DiscreteEvaluationAttributeUtilities
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00007F14 File Offset: 0x00006114
		public unsafe static int ConvertFloatToDiscreteInt(float f)
		{
			float* ptr = &f;
			int* ptr2 = (int*)ptr;
			return *ptr2;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00007F30 File Offset: 0x00006130
		public unsafe static float ConvertDiscreteIntToFloat(int f)
		{
			int* ptr = &f;
			float* ptr2 = (float*)ptr;
			return *ptr2;
		}
	}
}
