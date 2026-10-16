using System;

namespace XRL.World.Parts
{
	// Token: 0x02001099 RID: 4249
	[Serializable]
	public class SometimesBloody : IPart
	{
		// Token: 0x0600A91C RID: 43292 RVA: 0x003DD54E File Offset: 0x003DB74E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A91D RID: 43293 RVA: 0x003DD564 File Offset: 0x003DB764
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Chance.in100())
			{
				this.ParentObject.MakeBloody(this.Liquid, Math.Min(this.Volume.RollCached(), (int)Math.Round(this.ParentObject.GetMaximumLiquidExposureAsDouble() * (double)LiquidVolume.GetLiquid(this.Liquid).Adsorbence / 100.0)), this.Duration.RollCached());
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x040045D2 RID: 17874
		public int Chance = 50;

		// Token: 0x040045D3 RID: 17875
		public string Liquid = "blood";

		// Token: 0x040045D4 RID: 17876
		public string Volume = "3d4";

		// Token: 0x040045D5 RID: 17877
		public string Duration = "1";
	}
}
