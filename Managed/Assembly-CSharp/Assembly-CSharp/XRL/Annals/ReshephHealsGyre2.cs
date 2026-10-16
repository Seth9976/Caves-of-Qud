using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000618 RID: 1560
	[Serializable]
	public class ReshephHealsGyre2 : HistoricEvent
	{
		// Token: 0x060041C0 RID: 16832 RVA: 0x00150650 File Offset: 0x0014E850
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, the Gyre widened, and the second triad of plagues afflicted the land. The legs of kith and kin annealed to iron, darkness bloomed from the earth, and the svardym blackened the sky. Resheph walked below the chrome arches and healed the sick.";
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("gyreplagues", "true");
		}
	}
}
