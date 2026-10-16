using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200105A RID: 4186
	[Serializable]
	public class LeakWhenBroken : IPart
	{
		// Token: 0x0600A69C RID: 42652 RVA: 0x003CF121 File Offset: 0x003CD321
		public override bool SameAs(IPart p)
		{
			return !((p as LeakWhenBroken).PercentPerTurn != this.PercentPerTurn) && base.SameAs(p);
		}

		// Token: 0x0600A69D RID: 42653 RVA: 0x003CF144 File Offset: 0x003CD344
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A69E RID: 42654 RVA: 0x003CF147 File Offset: 0x003CD347
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckLeaking(Amount);
		}

		// Token: 0x0600A69F RID: 42655 RVA: 0x003CF150 File Offset: 0x003CD350
		private void CheckLeaking(int Turns = 1)
		{
			if (!base.IsBroken())
			{
				return;
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null || liquidVolume.Volume <= 0 || liquidVolume.IsOpenVolume())
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < Turns; i++)
			{
				int num2 = this.PercentPerTurn.RollCached();
				int num3 = liquidVolume.Volume * num2 / 100;
				if (num3 < 1 && num2 > 0)
				{
					num3 = 1;
				}
				num += num3;
			}
			if (num <= 0)
			{
				return;
			}
			this.DistributeLiquid(liquidVolume.Split(num));
		}

		// Token: 0x0600A6A0 RID: 42656 RVA: 0x003CF1D4 File Offset: 0x003CD3D4
		private void DistributeLiquid(LiquidVolume produced)
		{
			if (produced == null || produced.Volume <= 0)
			{
				return;
			}
			GameObject objectContext = this.ParentObject.GetObjectContext();
			if (this.ParentObject.GetCellContext() == null)
			{
				objectContext.GetCellContext();
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			if (base.Visible())
			{
				IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
				{
					this.ParentObject.The,
					this.ParentObject.ShortDisplayName,
					this.ParentObject.GetVerb("leak", true, false, false, true, false),
					" ",
					produced.Volume.ToString(),
					" ",
					(produced.Volume == 1) ? "dram" : "drams",
					" of ",
					produced.GetLiquidName(),
					"."
				}), null, true);
			}
			this.ParentObject.ApplyEffect(new LiquidCovered(produced, 1, 1, false, null, false), null);
			if (produced.Volume <= 0)
			{
				return;
			}
			if (currentCell.IsSolid(true))
			{
				return;
			}
			LiquidVolume V;
			if (!currentCell.ForeachObjectWithPart("LiquidVolume", delegate(GameObject GO)
			{
				V = GO.LiquidVolume;
				if (V.MaxVolume != -1 && V.Collector && V.Volume < V.MaxVolume)
				{
					V.MixWith(produced, null, null, null, false);
					return false;
				}
				return true;
			}))
			{
				return;
			}
			if (!currentCell.ForeachObjectWithPart("LiquidVolume", delegate(GameObject GO)
			{
				V = GO.LiquidVolume;
				if (V.MaxVolume == -1)
				{
					V.MixWith(produced, null, null, null, false);
					return false;
				}
				return true;
			}))
			{
				return;
			}
			GameObject gameObject = GameObject.Create("Water");
			V = gameObject.LiquidVolume;
			string groundLiquid = currentCell.GroundLiquid;
			if (string.IsNullOrEmpty(groundLiquid))
			{
				V.InitialLiquid = produced.GetLiquidDesignation(false);
			}
			else
			{
				V.Volume = produced.Volume;
				V.InitialLiquid = groundLiquid;
				V.MixWith(produced, null, null, null, false);
			}
			currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x040044D3 RID: 17619
		public string PercentPerTurn = "10-20";
	}
}
