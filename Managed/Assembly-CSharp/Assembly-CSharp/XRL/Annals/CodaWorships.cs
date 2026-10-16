using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000623 RID: 1571
	[Serializable]
	public class CodaWorships : HistoricEvent
	{
		// Token: 0x060041DA RID: 16858 RVA: 0x00151B90 File Offset: 0x0014FD90
		public override void Generate()
		{
			string text = this.entity.GetListProperties("sacredThings", -1L, true).GetRandomElement<string>(null) ?? this.entity.GetEntityProperty("defaultSacredThing", -1L);
			string entityProperty = this.history.GetEntity("CodaSultan").GetEntityProperty("name", -1L);
			base.AddEntityListItem("itemAdjectiveRoots", entityProperty, false);
			base.AddEntityListItem("sacredThings", entityProperty, false);
			base.AddEntityListItem("profaneThings", base.ExpandString("<spice.commonPhrases.profanity.!random> toward ") + entityProperty, false);
			string text2 = string.Format("The {0} of {1} was revealed to the people of {2} through {3}.|{4}", new object[]
			{
				base.ExpandString("<spice.instancesOf.divineFeeling.!random>"),
				entityProperty,
				this.entity.GetEntityProperty("name", -1L),
				text,
				this.id
			});
			base.AddEntityListItem("Gospels", text2, false);
		}
	}
}
