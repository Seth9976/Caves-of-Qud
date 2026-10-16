using System;
using HistoryKit;

namespace XRL.Annals
{
	// Token: 0x0200060A RID: 1546
	[Serializable]
	public class Adopted : HistoricEvent
	{
		// Token: 0x06004195 RID: 16789 RVA: 0x0014E7AD File Offset: 0x0014C9AD
		public override void Generate()
		{
			this.duration = (long)base.Random(5, 8);
			base.SetEventProperty("gospel", "<$chosenSpice=spice.elements.entity$elements[random]><entity.name> was adopted by <$chosenSpice.professions.!random> who love <$chosenSpice.practices.!random>.");
		}
	}
}
