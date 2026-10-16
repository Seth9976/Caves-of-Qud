using System;

namespace System.IO
{
	// Token: 0x02000B30 RID: 2864
	public class EnumerationOptions
	{
		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06006741 RID: 26433 RVA: 0x0015FFD8 File Offset: 0x0015E1D8
		internal static EnumerationOptions Compatible { get; } = new EnumerationOptions
		{
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06006742 RID: 26434 RVA: 0x0015FFDF File Offset: 0x0015E1DF
		private static EnumerationOptions CompatibleRecursive { get; } = new EnumerationOptions
		{
			RecurseSubdirectories = true,
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06006743 RID: 26435 RVA: 0x0015FFE6 File Offset: 0x0015E1E6
		internal static EnumerationOptions Default { get; } = new EnumerationOptions();

		// Token: 0x06006744 RID: 26436 RVA: 0x0015FFED File Offset: 0x0015E1ED
		public EnumerationOptions()
		{
			this.IgnoreInaccessible = true;
			this.AttributesToSkip = FileAttributes.Hidden | FileAttributes.System;
		}

		// Token: 0x06006745 RID: 26437 RVA: 0x00160003 File Offset: 0x0015E203
		internal static EnumerationOptions FromSearchOption(SearchOption searchOption)
		{
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", "Enum value was out of legal range.");
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				return EnumerationOptions.Compatible;
			}
			return EnumerationOptions.CompatibleRecursive;
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x0016002B File Offset: 0x0015E22B
		// (set) Token: 0x06006747 RID: 26439 RVA: 0x00160033 File Offset: 0x0015E233
		public bool RecurseSubdirectories { get; set; }

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x0016003C File Offset: 0x0015E23C
		// (set) Token: 0x06006749 RID: 26441 RVA: 0x00160044 File Offset: 0x0015E244
		public bool IgnoreInaccessible { get; set; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x0600674A RID: 26442 RVA: 0x0016004D File Offset: 0x0015E24D
		// (set) Token: 0x0600674B RID: 26443 RVA: 0x00160055 File Offset: 0x0015E255
		public int BufferSize { get; set; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x0600674C RID: 26444 RVA: 0x0016005E File Offset: 0x0015E25E
		// (set) Token: 0x0600674D RID: 26445 RVA: 0x00160066 File Offset: 0x0015E266
		public FileAttributes AttributesToSkip { get; set; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600674E RID: 26446 RVA: 0x0016006F File Offset: 0x0015E26F
		// (set) Token: 0x0600674F RID: 26447 RVA: 0x00160077 File Offset: 0x0015E277
		public MatchType MatchType { get; set; }

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06006750 RID: 26448 RVA: 0x00160080 File Offset: 0x0015E280
		// (set) Token: 0x06006751 RID: 26449 RVA: 0x00160088 File Offset: 0x0015E288
		public MatchCasing MatchCasing { get; set; }

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06006752 RID: 26450 RVA: 0x00160091 File Offset: 0x0015E291
		// (set) Token: 0x06006753 RID: 26451 RVA: 0x00160099 File Offset: 0x0015E299
		public bool ReturnSpecialDirectories { get; set; }
	}
}
