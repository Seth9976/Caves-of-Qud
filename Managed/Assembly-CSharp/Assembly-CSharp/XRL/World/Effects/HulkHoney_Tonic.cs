using System;
using HistoryKit;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D4B RID: 3403
	[Serializable]
	public class HulkHoney_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x0600867A RID: 34426 RVA: 0x00305CD3 File Offset: 0x00303ED3
		public HulkHoney_Tonic()
		{
		}

		// Token: 0x0600867B RID: 34427 RVA: 0x00305CDB File Offset: 0x00303EDB
		public HulkHoney_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600867C RID: 34428 RVA: 0x00305CEA File Offset: 0x00303EEA
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdosed = true;
			}
			this.Duration = Stat.Roll(41, 50);
		}

		// Token: 0x0600867D RID: 34429 RVA: 0x00305D0B File Offset: 0x00303F0B
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600867E RID: 34430 RVA: 0x00305D0E File Offset: 0x00303F0E
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600867F RID: 34431 RVA: 0x00305D11 File Offset: 0x00303F11
		public override string GetDescription()
		{
			return "{{G|hulk}} {{w|honey}} tonic";
		}

		// Token: 0x06008680 RID: 34432 RVA: 0x00305D18 File Offset: 0x00303F18
		public override string GetDetails()
		{
			if (base.Object.IsTrueKin())
			{
				return "+9 Strength\n+3 temporary hit points per level.\n-25 Move Speed\nCan't feel pain.\nImmune to fear.\nSuffers 1% of max hit points in damage each turn (can't be reduced below one hit point by this effect).";
			}
			return "+6 Strength\n+2 temporary hit points per level.\n-25 Move Speed\nCan't feel pain.\nImmune to fear.\nSuffers 1% of max hit points in damage each turn (can't be reduced below one hit point by this effect).";
		}

		// Token: 0x06008681 RID: 34433 RVA: 0x00305D34 File Offset: 0x00303F34
		public override bool Apply(GameObject Object)
		{
			bool flag = true;
			HulkHoney_Tonic effect = Object.GetEffect<HulkHoney_Tonic>();
			if (effect != null)
			{
				if (effect.Duration < this.Duration)
				{
					effect.Duration = this.Duration;
				}
				flag = false;
			}
			else
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_hulkTonic", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Object.IsPlayer())
				{
					Popup.Show("Your muscles bulge grotesquely.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				this.ApplyChanges();
			}
			int num = 0;
			while (Object.HasEffect<Terrified>() && ++num < 10)
			{
				Object.RemoveEffect<Terrified>();
			}
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdosed)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return flag;
		}

		// Token: 0x06008682 RID: 34434 RVA: 0x00305E05 File Offset: 0x00304005
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("Your muscles deflate to their usual size.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.UnapplyChanges();
		}

		// Token: 0x06008683 RID: 34435 RVA: 0x00305E2C File Offset: 0x0030402C
		private void ApplyChanges()
		{
			this.BonusHP = base.Object.Stat("Level", 0) * (base.Object.IsTrueKin() ? 3 : 2);
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", 25, false);
			base.StatShifter.SetStatShift(base.Object, "Strength", base.Object.IsTrueKin() ? 9 : 6, false);
			base.StatShifter.SetStatShift(base.Object, "Hitpoints", this.BonusHP, true);
			if (base.Object.HasStat("Hitpoints"))
			{
				Statistic statistic = base.Object.Statistics["Hitpoints"];
				if (this.TempHPPenalty > 0)
				{
					statistic.Penalty += this.TempHPPenalty;
					this.TempHPPenalty = 0;
				}
			}
			base.Object.ModIntProperty("Analgesia", 1, false);
		}

		// Token: 0x06008684 RID: 34436 RVA: 0x00305F24 File Offset: 0x00304124
		private void UnapplyChanges()
		{
			if (base.Object.HasStat("Hitpoints"))
			{
				Statistic statistic = base.Object.Statistics["Hitpoints"];
				if (statistic.Penalty >= statistic.BaseValue - this.BonusHP)
				{
					int num = statistic.BaseValue - this.BonusHP - 1;
					this.TempHPPenalty = statistic.Penalty - num;
					statistic.Penalty = num;
				}
			}
			base.StatShifter.RemoveStatShifts(base.Object);
			base.Object.ModIntProperty("Analgesia", -1, true);
			if (this.bOverdosed)
			{
				base.Object.ModIntProperty("ConfusionLevel", -1, false);
				base.Object.ModIntProperty("FuriousConfusionLevel", -1, false);
				this.bOverdosed = false;
			}
		}

		// Token: 0x06008685 RID: 34437 RVA: 0x00305FEC File Offset: 0x003041EC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008686 RID: 34438 RVA: 0x00306004 File Offset: 0x00304204
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0 && base.Object.HasStat("Hitpoints"))
			{
				this.HPLoss += (float)base.Object.BaseStat("Hitpoints", 0) * 0.01f;
				int num = Math.Min(base.Object.hitpoints - 1, (int)Math.Floor((double)this.HPLoss));
				if (num > 0)
				{
					base.Object.TakeDamage(num, "from the {{G|hulk}} {{w|honey}}!", "Metabolic Expected Unavoidable", null, null, null, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					this.HPLoss -= (float)num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008687 RID: 34439 RVA: 0x003060BC File Offset: 0x003042BC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("ApplyFear");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CanApplyFear");
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008688 RID: 34440 RVA: 0x00306108 File Offset: 0x00304308
		public override void ApplyAllergy(GameObject Object)
		{
			if (Object.GetLongProperty("Overdosing", 0L) == 1L)
			{
				Popup.Show("Your mutant physiology reacts adversely to the tonic. Aaaaaaaaargh!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else
			{
				Popup.Show("The tonics you ingested react adversely to each other. Aaaaaaaaargh!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			Object.ApplyEffect(new HulkHoney_Tonic_Allergy(this.Duration), null);
		}

		// Token: 0x06008689 RID: 34441 RVA: 0x00306168 File Offset: 0x00304368
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanApplyFear" || E.ID == "ApplyFear")
			{
				return false;
			}
			if (E.ID == "Overdose")
			{
				if (this.Duration > 0 && base.Object.IsPlayer())
				{
					Achievement.OVERDOSE_HULKHONEY.Unlock();
					if (base.Object.GetLongProperty("Overdosing", 0L) == 1L)
					{
						Popup.Show("Your mutant physiology reacts adversely to the tonic. Aaaaaaaaargh!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("The tonics you ingested react adversely to each other. Aaaaaaaaargh!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					this.bOverdosed = true;
					base.Object.ModIntProperty("ConfusionLevel", 1, false);
					base.Object.ModIntProperty("FuriousConfusionLevel", 1, false);
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600868A RID: 34442 RVA: 0x00306280 File Offset: 0x00304480
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "!";
				if (Stat.RandomCosmetic(1, 3) == 1)
				{
					E.ColorString = "&G";
				}
			}
			return true;
		}

		// Token: 0x040039DF RID: 14815
		private bool bOverdosed;

		// Token: 0x040039E0 RID: 14816
		private int BonusHP;

		// Token: 0x040039E1 RID: 14817
		private float HPLoss;

		// Token: 0x040039E2 RID: 14818
		[NonSerialized]
		private int TempHPPenalty;

		// Token: 0x040039E3 RID: 14819
		public int x;

		// Token: 0x040039E4 RID: 14820
		public int y;

		// Token: 0x040039E5 RID: 14821
		public string zone;
	}
}
