using System;
using HistoryKit;
using XRL.Names;

namespace XRL.Annals
{
	// Token: 0x020005EE RID: 1518
	[Serializable]
	public class Regionalize : HistoricEvent
	{
		// Token: 0x06004156 RID: 16726 RVA: 0x0014960C File Offset: 0x0014780C
		public override void Generate()
		{
			base.SetEntityProperty("name", "regionalizationParameters");
			base.SetEntityProperty("government", base.ExpandString("<spice.history.regions.government.types.!random>"));
			base.SetEntityProperty("topology", base.ExpandString("<spice.history.regions.topology.types.!random>"));
			base.SetEntityProperty("organizingPrinciple", base.ExpandString("<spice.history.regions.organizingPrinciple.types.!random>"));
			base.SetEntityProperty("successorChance", base.Random(0, 100).ToString());
			base.SetEntityProperty("siteName1", NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null));
			base.SetEntityProperty("siteName2", NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null));
			base.SetEntityProperty("siteName3", NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null));
			base.SetEntityProperty("siteTopologyTheChance", ((base.Random(0, 1) == 0) ? 20 : 80).ToString());
			string text = base.ExpandString("<spice.siteModifiers1.!random>");
			base.SetEntityProperty("siteTopology1", text);
			string text2;
			do
			{
				text2 = base.ExpandString("<spice.siteModifiers2.!random>");
			}
			while (text2 == text);
			base.SetEntityProperty("siteTopology2", text2);
			this.duration = 0L;
		}
	}
}
