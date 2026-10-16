using System;

namespace XRL.World
{
	// Token: 0x02000975 RID: 2421
	public static class ZoneBuilderPriority
	{
		// Token: 0x04002EFD RID: 12029
		public const int OVERRIDE = -1000;

		// Token: 0x04002EFE RID: 12030
		public const int EXTREMELY_EARLY = 1000;

		// Token: 0x04002EFF RID: 12031
		public const int VERY_EARLY = 2000;

		// Token: 0x04002F00 RID: 12032
		public const int EARLY = 3000;

		// Token: 0x04002F01 RID: 12033
		public const int NORMAL = 4000;

		// Token: 0x04002F02 RID: 12034
		public const int MID = 4500;

		// Token: 0x04002F03 RID: 12035
		public const int LATE = 5000;

		// Token: 0x04002F04 RID: 12036
		public const int VERY_LATE = 6000;

		// Token: 0x04002F05 RID: 12037
		public const int AFTER_TERRAIN = 6000;

		// Token: 0x04002F06 RID: 12038
		public const int EXTREMELY_LATE = 7000;

		// Token: 0x04002F07 RID: 12039
		public const int ADJUST_SMALL = 1;

		// Token: 0x04002F08 RID: 12040
		public const int ADJUST_MEDIUM = 10;

		// Token: 0x04002F09 RID: 12041
		public const int ADJUST_LARGE = 100;
	}
}
