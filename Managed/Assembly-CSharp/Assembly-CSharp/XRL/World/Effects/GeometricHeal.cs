using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D40 RID: 3392
	[Serializable]
	public class GeometricHeal : Effect, ITierInitialized
	{
		// Token: 0x060085EC RID: 34284 RVA: 0x0030414A File Offset: 0x0030234A
		public GeometricHeal()
		{
			this.DisplayName = "healing";
		}

		// Token: 0x060085ED RID: 34285 RVA: 0x00304164 File Offset: 0x00302364
		public GeometricHeal(double Amount, int Ratio, int Duration = 3)
			: this()
		{
			this.Amount = Amount;
			this.Ratio = Ratio;
			this.Duration = Duration;
		}

		// Token: 0x060085EE RID: 34286 RVA: 0x00304181 File Offset: 0x00302381
		public void Initialize(int Tier)
		{
			this.Amount = (double)(Stat.Roll(10, 12) + Tier * 2);
			this.Ratio = -50;
			this.Duration = Stat.Roll(3, 5);
		}

		// Token: 0x060085EF RID: 34287 RVA: 0x003041AC File Offset: 0x003023AC
		public override int GetEffectType()
		{
			return 83886084;
		}

		// Token: 0x060085F0 RID: 34288 RVA: 0x003041B3 File Offset: 0x003023B3
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060085F1 RID: 34289 RVA: 0x003041B6 File Offset: 0x003023B6
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060085F2 RID: 34290 RVA: 0x003041BC File Offset: 0x003023BC
		public override string GetDetails()
		{
			double num = 1.0 + (double)this.Ratio / 100.0;
			double num2 = this.Amount * (1.0 - Math.Pow(num, (double)this.Duration)) / (1.0 - num);
			return string.Format("Recovering {0} hit points over {1} {2}.", (int)num2, this.Duration, (this.Duration == 1) ? "turn" : "turns");
		}

		// Token: 0x060085F3 RID: 34291 RVA: 0x00304244 File Offset: 0x00302444
		public override bool Apply(GameObject Object)
		{
			GeometricHeal geometricHeal;
			if (Object.TryGetEffect<GeometricHeal>(out geometricHeal))
			{
				if (this.Duration >= geometricHeal.Duration)
				{
					geometricHeal.Duration = this.Duration;
					geometricHeal.Amount = this.Amount;
					geometricHeal.Initial = this.Initial;
					if (geometricHeal.Initial)
					{
						geometricHeal.Heal();
					}
				}
				else if (this.Initial)
				{
					this.Heal();
				}
				return false;
			}
			if (!Object.HasStat("Hitpoints"))
			{
				return false;
			}
			if (!Object.FireEvent("CanApplyGeometricHeal") || !ApplyEffectEvent.Check(Object, "GeometricHeal", this, null))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyGeometricHeal"))
			{
				return false;
			}
			base.DidX("begin", "healing", null, null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
			if (this.Initial)
			{
				this.Heal();
			}
			return true;
		}

		// Token: 0x060085F4 RID: 34292 RVA: 0x00304317 File Offset: 0x00302517
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndActionEvent>.ID;
		}

		// Token: 0x060085F5 RID: 34293 RVA: 0x0030432D File Offset: 0x0030252D
		public override bool HandleEvent(EndActionEvent E)
		{
			if (this.Initial)
			{
				this.Initial = false;
			}
			else
			{
				this.Heal();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085F6 RID: 34294 RVA: 0x0030434D File Offset: 0x0030254D
		public void Heal()
		{
			base.Object.Heal((int)this.Amount, false, true, false);
			this.Amount += this.Amount * (double)this.Ratio / 100.0;
		}

		// Token: 0x060085F7 RID: 34295 RVA: 0x0030438C File Offset: 0x0030258C
		public override bool Render(RenderEvent E)
		{
			if (this.Amount > 0.0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 25 && num < 35)
				{
					E.Tile = null;
					E.RenderString = "Z";
					E.ColorString = "&g";
				}
			}
			return true;
		}

		// Token: 0x040039CB RID: 14795
		public double Amount;

		// Token: 0x040039CC RID: 14796
		public int Ratio;

		// Token: 0x040039CD RID: 14797
		public bool Initial = true;
	}
}
