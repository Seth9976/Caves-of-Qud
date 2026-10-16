using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x02000617 RID: 1559
	[Serializable]
	public class ReshephHealsGyre1 : HistoricEvent
	{
		// Token: 0x060041BE RID: 16830 RVA: 0x001505FC File Offset: 0x0014E7FC
		public override void Generate()
		{
			this.duration = 0L;
			string text = "In %" + this.year.ToString() + "%, the Gyre widened, and the first triad of plagues afflicted the land. The water was poisoned with salt, girshlings ravaged the fruit and wheat, and tongues rotted away in the mouths of kith and kin. Resheph walked below the chrome arches and healed the sick.";
			base.SetEventProperty("gospel", text);
			base.SetEventProperty("gyreplagues", "true");
		}
	}
}
