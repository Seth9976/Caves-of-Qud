using System;
using System.Text.RegularExpressions;

namespace NAudio.Wave
{
	// Token: 0x020001DF RID: 479
	public class Cue
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001EB80 File Offset: 0x0001CD80
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0001EB88 File Offset: 0x0001CD88
		public int Position { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001EB91 File Offset: 0x0001CD91
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0001EB99 File Offset: 0x0001CD99
		public string Label { get; private set; }

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001EBA2 File Offset: 0x0001CDA2
		public Cue(int position, string label)
		{
			this.Position = position;
			if (label == null)
			{
				label = "";
			}
			this.Label = Regex.Replace(label, "[^\\u0000-\\u00FF]", "");
		}
	}
}
