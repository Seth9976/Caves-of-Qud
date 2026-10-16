using System;

namespace NAudio.Dmo
{
	// Token: 0x02000092 RID: 146
	public class MediaObjectSizeInfo
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000AA35 File Offset: 0x00008C35
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000AA3D File Offset: 0x00008C3D
		public int Size { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000AA46 File Offset: 0x00008C46
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000AA4E File Offset: 0x00008C4E
		public int MaxLookahead { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000AA57 File Offset: 0x00008C57
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000AA5F File Offset: 0x00008C5F
		public int Alignment { get; private set; }

		// Token: 0x0600033A RID: 826 RVA: 0x0000AA68 File Offset: 0x00008C68
		public MediaObjectSizeInfo(int size, int maxLookahead, int alignment)
		{
			this.Size = size;
			this.MaxLookahead = maxLookahead;
			this.Alignment = alignment;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000AA85 File Offset: 0x00008C85
		public override string ToString()
		{
			return string.Format("Size: {0}, Alignment {1}, MaxLookahead {2}", this.Size, this.Alignment, this.MaxLookahead);
		}
	}
}
