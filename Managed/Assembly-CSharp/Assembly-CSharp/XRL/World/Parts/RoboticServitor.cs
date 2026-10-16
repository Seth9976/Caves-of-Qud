using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EF9 RID: 3833
	[Serializable]
	public class RoboticServitor : IBondedCompanion
	{
		// Token: 0x06009909 RID: 39177 RVA: 0x003789F4 File Offset: 0x00376BF4
		public RoboticServitor()
			: base(null, null, null, null, null, false)
		{
		}

		// Token: 0x0600990A RID: 39178 RVA: 0x00378A02 File Offset: 0x00376C02
		public RoboticServitor(GameObject ServitorOf = null, string Faction = null, string Honorific = null, string Title = null, string ConversationID = null, bool StripGear = false)
			: base(ServitorOf, Faction, Honorific, Title, ConversationID, StripGear)
		{
		}
	}
}
