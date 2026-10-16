using System;

namespace XRL.World.Text
{
	// Token: 0x02001463 RID: 5219
	public struct TextArgument
	{
		// Token: 0x0600CD23 RID: 52515 RVA: 0x004A5CFE File Offset: 0x004A3EFE
		public TextArgument(GameObject Object, string Explicit = null, IPronounProvider Pronouns = null)
		{
			this.Object = Object;
			this.Explicit = Explicit;
			this.Pronouns = Pronouns;
		}

		// Token: 0x0600CD24 RID: 52516 RVA: 0x004A5D15 File Offset: 0x004A3F15
		public TextArgument(string Explicit, IPronounProvider Pronouns = null)
		{
			this = new TextArgument(null, Explicit, Pronouns);
		}

		// Token: 0x0600CD25 RID: 52517 RVA: 0x004A5D20 File Offset: 0x004A3F20
		public TextArgument(string Explicit, bool ExplicitPlural)
		{
			this = new TextArgument(null, Explicit, ExplicitPlural);
		}

		// Token: 0x0600CD26 RID: 52518 RVA: 0x004A5D2B File Offset: 0x004A3F2B
		public TextArgument(GameObject Object, string Explicit, bool ExplicitPlural = false)
		{
			this = new TextArgument(Object, Explicit, ExplicitPlural ? Gender.DefaultPlural : Gender.DefaultNeuter);
		}

		// Token: 0x04004F57 RID: 20311
		public GameObject Object;

		// Token: 0x04004F58 RID: 20312
		public string Explicit;

		// Token: 0x04004F59 RID: 20313
		public IPronounProvider Pronouns;
	}
}
