using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x0200107E RID: 4222
	[Serializable]
	public class PopulationLiquidFiller : IPart
	{
		// Token: 0x0600A80A RID: 43018 RVA: 0x003D7131 File Offset: 0x003D5331
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A80B RID: 43019 RVA: 0x003D7148 File Offset: 0x003D5348
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume != null)
			{
				PopulationResult populationResult = null;
				int num = 0;
				while (++num < 5)
				{
					List<PopulationResult> list = PopulationManager.Generate(this.Table, null, null, null, null, null);
					populationResult = ((list != null) ? list.GetRandomElement<PopulationResult>(null) : null);
					if (populationResult != null && (populationResult.Blueprint == "empty" || populationResult.Blueprint.StartsWith("!") || this.ParentObject.IsSafeContainerForLiquid(populationResult.Blueprint)))
					{
						break;
					}
				}
				string text = populationResult.Blueprint;
				if (text.StartsWith("!"))
				{
					text = text.Substring(1);
				}
				liquidVolume.Empty(false, false);
				if (text != "empty")
				{
					int num2 = 1;
					if (populationResult.Number != 1 || string.IsNullOrEmpty(this.Volume))
					{
						num2 = populationResult.Number;
					}
					else if (!string.IsNullOrEmpty(this.Volume))
					{
						num2 = this.Volume.RollCached();
					}
					liquidVolume.AddDrams(text, num2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400455A RID: 17754
		public string Table;

		// Token: 0x0400455B RID: 17755
		public string Volume;
	}
}
