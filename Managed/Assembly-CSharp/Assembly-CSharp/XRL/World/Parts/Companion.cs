using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E5A RID: 3674
	[Serializable]
	public class Companion : IBondedCompanion
	{
		// Token: 0x060094AE RID: 38062 RVA: 0x0035BCE7 File Offset: 0x00359EE7
		public Companion()
			: base(null, null, null, null, null, false)
		{
		}

		// Token: 0x060094AF RID: 38063 RVA: 0x0035BCF5 File Offset: 0x00359EF5
		public Companion(GameObject CompanionOf = null, string Faction = null, string Honorific = null, string Title = null, string ConversationID = null, bool StripGear = false)
			: base(CompanionOf, Faction, Honorific, Title, ConversationID, StripGear)
		{
		}
	}
}
