using System;

namespace XRL.World.Parts
{
	// Token: 0x02000ECB RID: 3787
	[Serializable]
	public class HiredGuard : IBondedCompanion
	{
		// Token: 0x060097B8 RID: 38840 RVA: 0x00371844 File Offset: 0x0036FA44
		public HiredGuard()
			: base(null, null, null, null, null, false)
		{
		}

		// Token: 0x060097B9 RID: 38841 RVA: 0x00371852 File Offset: 0x0036FA52
		public HiredGuard(GameObject HiredBy = null, string Faction = "Merchants", string Honorific = null, string Title = "hired guard", string ConversationID = "MerchantGuard", bool StripGear = false)
			: base(HiredBy, Faction, Honorific, Title, ConversationID, StripGear)
		{
		}
	}
}
