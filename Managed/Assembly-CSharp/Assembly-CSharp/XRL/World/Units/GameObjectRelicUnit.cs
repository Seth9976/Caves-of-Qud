using System;

namespace XRL.World.Units
{
	// Token: 0x020009B1 RID: 2481
	[Serializable]
	public class GameObjectRelicUnit : GameObjectUnit
	{
		// Token: 0x06006AF1 RID: 27377 RVA: 0x0022722F File Offset: 0x0022542F
		public override void Apply(GameObject Object)
		{
			Object.ReceiveObject(RelicGenerator.GenerateRelic(this.Tier.RollCached(), true), false, null);
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x0022724B File Offset: 0x0022544B
		public override void Reset()
		{
			base.Reset();
			this.Tier = null;
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x0022725C File Offset: 0x0022545C
		public override string GetDescription(bool Inscription = false)
		{
			int num = this.Tier.RollMax();
			string text = "low";
			if (num > 5)
			{
				text = "high";
			}
			else if (num > 2)
			{
				text = "mid";
			}
			return "Spawns with a " + text + "-tier relic";
		}

		// Token: 0x040030F9 RID: 12537
		public string Tier;
	}
}
