using System;

namespace Ionic.Zip
{
	// Token: 0x0200003B RID: 59
	public class SelfExtractorSaveOptions
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000FE04 File Offset: 0x0000E004
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000FE0C File Offset: 0x0000E00C
		public SelfExtractorFlavor Flavor { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000FE15 File Offset: 0x0000E015
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000FE1D File Offset: 0x0000E01D
		public string PostExtractCommandLine { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000FE26 File Offset: 0x0000E026
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000FE2E File Offset: 0x0000E02E
		public string DefaultExtractDirectory { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000FE37 File Offset: 0x0000E037
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000FE3F File Offset: 0x0000E03F
		public string IconFile { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000FE48 File Offset: 0x0000E048
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000FE50 File Offset: 0x0000E050
		public bool Quiet { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000FE59 File Offset: 0x0000E059
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000FE61 File Offset: 0x0000E061
		public ExtractExistingFileAction ExtractExistingFile { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000FE6A File Offset: 0x0000E06A
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000FE72 File Offset: 0x0000E072
		public bool RemoveUnpackedFilesAfterExecute { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000FE7B File Offset: 0x0000E07B
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000FE83 File Offset: 0x0000E083
		public Version FileVersion { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000FE8C File Offset: 0x0000E08C
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000FE94 File Offset: 0x0000E094
		public string ProductVersion { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000FE9D File Offset: 0x0000E09D
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000FEA5 File Offset: 0x0000E0A5
		public string Copyright { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000FEAE File Offset: 0x0000E0AE
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000FEB6 File Offset: 0x0000E0B6
		public string Description { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000FEBF File Offset: 0x0000E0BF
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000FEC7 File Offset: 0x0000E0C7
		public string ProductName { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		public string SfxExeWindowTitle { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000FEE1 File Offset: 0x0000E0E1
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
		public string AdditionalCompilerSwitches { get; set; }
	}
}
