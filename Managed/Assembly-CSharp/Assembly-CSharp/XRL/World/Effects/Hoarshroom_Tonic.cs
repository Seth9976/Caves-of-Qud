using System;
using HistoryKit;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D47 RID: 3399
	[Serializable]
	public class Hoarshroom_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06008646 RID: 34374 RVA: 0x00304F64 File Offset: 0x00303164
		public Hoarshroom_Tonic()
		{
		}

		// Token: 0x06008647 RID: 34375 RVA: 0x00304F73 File Offset: 0x00303173
		public Hoarshroom_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008648 RID: 34376 RVA: 0x00304F82 File Offset: 0x00303182
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdose = true;
			}
			this.Duration = Stat.Roll(180, 220);
		}

		// Token: 0x06008649 RID: 34377 RVA: 0x00304FA9 File Offset: 0x003031A9
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600864A RID: 34378 RVA: 0x00304FAC File Offset: 0x003031AC
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600864B RID: 34379 RVA: 0x00304FAF File Offset: 0x003031AF
		public override string GetDescription()
		{
			return "{{C|illuminated}}";
		}

		// Token: 0x0600864C RID: 34380 RVA: 0x00304FB6 File Offset: 0x003031B6
		public override string GetDetails()
		{
			return "Casts light in radius 2.\nFor the first 5 rounds after imbibing, gains 0.6 hit points per level (minimum 3) each turn.\n+20 Cold Resist\nCan't be poisoned.";
		}

		// Token: 0x0600864D RID: 34381 RVA: 0x00304FC0 File Offset: 0x003031C0
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_hoarshroomTonic", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				Popup.Show("You feel a cool swelling as your organs start to glow through your skin.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.ApplyStats();
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x0600864E RID: 34382 RVA: 0x0030504B File Offset: 0x0030324B
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("The cool swelling deflates as your organs dim.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.UnapplyStats();
		}

		// Token: 0x0600864F RID: 34383 RVA: 0x00305070 File Offset: 0x00303270
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "ColdResistance", 20, false);
		}

		// Token: 0x06008650 RID: 34384 RVA: 0x0030508C File Offset: 0x0030328C
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008651 RID: 34385 RVA: 0x0030509F File Offset: 0x0030329F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06008652 RID: 34386 RVA: 0x003050B5 File Offset: 0x003032B5
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			base.AddLight(2, LightLevel.Light, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008653 RID: 34387 RVA: 0x003050CC File Offset: 0x003032CC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("ApplyPoison");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("EndAction");
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008654 RID: 34388 RVA: 0x00305118 File Offset: 0x00303318
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyPoison")
			{
				return false;
			}
			if (E.ID == "EndAction")
			{
				if (base.Object.HasStat("Hitpoints") && base.Object.HasStat("Level") && this.HealRounds > 0)
				{
					this.HealAmount += (float)Math.Max(5.0, Math.Ceiling(0.6 * (double)((float)base.Object.BaseStat("Level", 0))));
					if (this.HealAmount >= 1f)
					{
						base.Object.Heal((int)Math.Floor((double)this.HealAmount), true, true, true);
						this.HealAmount -= (float)Math.Floor((double)this.HealAmount);
					}
					this.HealRounds--;
				}
			}
			else if (E.ID == "Overdose")
			{
				if (this.Duration > 0)
				{
					this.Duration = 0;
					Hoarshroom_Tonic.ApplyOverdose(base.Object);
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008655 RID: 34389 RVA: 0x0030527D File Offset: 0x0030347D
		public override void ApplyAllergy(GameObject Object)
		{
			Hoarshroom_Tonic.ApplyOverdose(Object);
		}

		// Token: 0x06008656 RID: 34390 RVA: 0x00305288 File Offset: 0x00303488
		public static void ApplyOverdose(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetLongProperty("Overdosing", 0L) == 1L)
				{
					Popup.Show("Your mutant physiology reacts adversely to the tonic. You feel awfully frigid.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("The tonics you ingested react adversely to each other. You feel awfully frigid.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (Object.Physics != null)
			{
				int num = Object.Physics.BrittleTemperature - 20;
				if (Object.Physics.Temperature > num)
				{
					Object.Physics.Temperature = num;
				}
			}
		}

		// Token: 0x06008657 RID: 34391 RVA: 0x0030530C File Offset: 0x0030350C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 40 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "@";
				int num2 = Stat.RandomCosmetic(1, 3);
				if (num2 == 1)
				{
					E.ColorString = "&C";
				}
				else if (num2 == 2)
				{
					E.ColorString = "&B";
				}
				else if (num2 == 3)
				{
					E.ColorString = "&c";
				}
			}
			return true;
		}

		// Token: 0x040039D6 RID: 14806
		public int x;

		// Token: 0x040039D7 RID: 14807
		public int y;

		// Token: 0x040039D8 RID: 14808
		public string zone;

		// Token: 0x040039D9 RID: 14809
		public int HealRounds = 5;

		// Token: 0x040039DA RID: 14810
		public float HealAmount;

		// Token: 0x040039DB RID: 14811
		public bool bOverdose;
	}
}
