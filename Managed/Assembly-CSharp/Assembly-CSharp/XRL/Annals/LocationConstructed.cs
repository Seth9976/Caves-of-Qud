using System;
using HistoryKit;
using XRL.Core;

namespace XRL.Annals
{
	// Token: 0x020005FA RID: 1530
	[Serializable]
	public class LocationConstructed : HistoricEvent
	{
		// Token: 0x06004170 RID: 16752 RVA: 0x0014AB6C File Offset: 0x00148D6C
		public override void Generate()
		{
			string[] array = new string[] { "Tomb", "Palace", "Library", "Abbey" };
			base.SetEntityProperty("name", XRLCore.Core.GenerateRandomPlayerName("") + "'s " + array[base.Random(0, 3)]);
			base.AddEntityListItem("spice", "glass", false);
			this.duration = (long)base.Random(1, 120);
			base.SetEventProperty("gospel", "construction begain in " + this.year.ToString() + " and ended in " + (this.year + this.duration).ToString());
		}
	}
}
