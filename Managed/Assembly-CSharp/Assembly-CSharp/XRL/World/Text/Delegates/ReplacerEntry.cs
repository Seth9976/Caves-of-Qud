using System;

namespace XRL.World.Text.Delegates
{
	// Token: 0x02001469 RID: 5225
	public struct ReplacerEntry
	{
		// Token: 0x0600CD98 RID: 52632 RVA: 0x004A7E69 File Offset: 0x004A6069
		public ReplacerEntry(Replacer Delegate, string Default = null, int Flags = 0, bool Capitalize = false)
		{
			this.Delegate = Delegate;
			this.Default = Default;
			this.Flags = Flags;
			this.Capitalize = Capitalize;
		}

		// Token: 0x04004F6D RID: 20333
		public Replacer Delegate;

		// Token: 0x04004F6E RID: 20334
		public string Default;

		// Token: 0x04004F6F RID: 20335
		public int Flags;

		// Token: 0x04004F70 RID: 20336
		public bool Capitalize;
	}
}
