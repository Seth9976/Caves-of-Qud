using System;
using System.Runtime.InteropServices;

namespace NAudio.Dmo
{
	// Token: 0x02000093 RID: 147
	internal struct MediaParamInfo
	{
		// Token: 0x04000404 RID: 1028
		public MediaParamType mpType;

		// Token: 0x04000405 RID: 1029
		public MediaParamCurveType mopCaps;

		// Token: 0x04000406 RID: 1030
		public float mpdMinValue;

		// Token: 0x04000407 RID: 1031
		public float mpdMaxValue;

		// Token: 0x04000408 RID: 1032
		public float mpdNeutralValue;

		// Token: 0x04000409 RID: 1033
		[MarshalAs(23, SizeConst = 32)]
		public string szUnitText;

		// Token: 0x0400040A RID: 1034
		[MarshalAs(23, SizeConst = 32)]
		public string szLabel;
	}
}
