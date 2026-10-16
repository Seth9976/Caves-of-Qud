using System;
using XRL.World.Parts;

namespace XRL.World.Units
{
	// Token: 0x020009A7 RID: 2471
	[Serializable]
	public class GameObjectBaetylUnit : GameObjectUnit
	{
		// Token: 0x06006ABA RID: 27322 RVA: 0x00225F88 File Offset: 0x00224188
		public override void Apply(GameObject Object)
		{
			int num = this.Amount.RollCached();
			int num2 = this.Tier.RollCached();
			if (this.Delay)
			{
				Object.AddPart<GameObjectBaetylUnit.RewardOnPlace>(new GameObjectBaetylUnit.RewardOnPlace
				{
					Amount = num,
					Tier = num2
				}, true, false);
				return;
			}
			GameObjectBaetylUnit.GiveRewards(Object, num, num2);
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x00225FDC File Offset: 0x002241DC
		public static void GiveRewards(GameObject Object, int Amount, int Tier)
		{
			for (int i = 0; i < Amount; i++)
			{
				RandomAltarBaetyl randomAltarBaetyl = new RandomAltarBaetyl();
				randomAltarBaetyl.RewardTier = Tier;
				randomAltarBaetyl.GenerateDemand(null);
				randomAltarBaetyl.GenerateReward(100);
				if (!randomAltarBaetyl.RewardID.IsNullOrEmpty())
				{
					GameObject gameObject = The.ZoneManager.peekCachedObject(randomAltarBaetyl.RewardID);
					if (gameObject != null)
					{
						gameObject.ApplyModification("ModGigantic", true, null, false);
					}
				}
				string text = randomAltarBaetyl.GiveReward(Object, true, true, false, false, false, true, false, true, 100);
				if (!text.IsNullOrEmpty())
				{
					Object.Physics.DidX("gain", text, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x00226082 File Offset: 0x00224282
		public override void Reset()
		{
			base.Reset();
			this.Amount = null;
			this.Tier = null;
		}

		// Token: 0x06006ABD RID: 27325 RVA: 0x00226098 File Offset: 0x00224298
		public override string GetDescription(bool Inscription = false)
		{
			return "Spawns with " + this.Amount + " random baetyl reward" + ((this.Amount == "1") ? "" : "s");
		}

		// Token: 0x040030DA RID: 12506
		public string Amount;

		// Token: 0x040030DB RID: 12507
		public string Tier;

		// Token: 0x040030DC RID: 12508
		public bool Delay;

		// Token: 0x02001AC2 RID: 6850
		public class RewardOnPlace : IPart
		{
			// Token: 0x0600E8FD RID: 59645 RVA: 0x0053A7A7 File Offset: 0x005389A7
			public override bool WantEvent(int ID, int cascade)
			{
				return ID == EnteredCellEvent.ID;
			}

			// Token: 0x0600E8FE RID: 59646 RVA: 0x0053A7B1 File Offset: 0x005389B1
			public override bool HandleEvent(EnteredCellEvent E)
			{
				GameObjectBaetylUnit.GiveRewards(this.ParentObject, this.Amount, this.Tier);
				this.ParentObject.RemovePart(this);
				return base.HandleEvent(E);
			}

			// Token: 0x04006A81 RID: 27265
			public int Amount;

			// Token: 0x04006A82 RID: 27266
			public int Tier;
		}
	}
}
