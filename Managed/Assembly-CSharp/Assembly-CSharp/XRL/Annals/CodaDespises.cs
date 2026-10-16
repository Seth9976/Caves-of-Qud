using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000622 RID: 1570
	[Serializable]
	public class CodaDespises : HistoricEvent
	{
		// Token: 0x060041D8 RID: 16856 RVA: 0x00151AC8 File Offset: 0x0014FCC8
		public override void Generate()
		{
			string text = this.entity.GetListProperties("profaneThings", -1L, true).GetRandomElement<string>(null) ?? this.entity.GetEntityProperty("defaultProfaneThing", -1L);
			string entityProperty = this.history.GetEntity("CodaSultan").GetEntityProperty("name", -1L);
			base.AddEntityListItem("profaneThings", entityProperty, false);
			string text2 = string.Format("The {0} of {1} was revealed to the people of {2} through {3}.|{4}", new object[]
			{
				base.ExpandString("<spice.instancesOf.profaneFeeling.!random>"),
				entityProperty,
				this.entity.GetEntityProperty("name", -1L),
				text,
				this.id
			});
			base.AddEntityListItem("Gospels", text2, false);
		}
	}
}
