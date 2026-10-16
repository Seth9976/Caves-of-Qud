using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EF3 RID: 3827
	[Serializable]
	public class PsychicThrall : IBondedCompanion
	{
		// Token: 0x060098EC RID: 39148 RVA: 0x003781E1 File Offset: 0x003763E1
		public PsychicThrall()
			: base(null, null, null, null, null, false)
		{
		}

		// Token: 0x060098ED RID: 39149 RVA: 0x003781EF File Offset: 0x003763EF
		public PsychicThrall(GameObject EnthralledBy = null, string Faction = "Seekers", string Honorific = null, string Title = "psychic thrall", string ConversationID = "PsychicThrall", bool StripGear = false)
			: base(EnthralledBy, Faction, Honorific, Title, ConversationID, StripGear)
		{
		}
	}
}
