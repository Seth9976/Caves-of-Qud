using System;
using System.Text;
using HistoryKit;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D7B RID: 3451
	[Serializable]
	public class Rubbergum_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x060088ED RID: 35053 RVA: 0x0031085C File Offset: 0x0030EA5C
		public Rubbergum_Tonic()
		{
		}

		// Token: 0x060088EE RID: 35054 RVA: 0x00310864 File Offset: 0x0030EA64
		public Rubbergum_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060088EF RID: 35055 RVA: 0x00310873 File Offset: 0x0030EA73
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdose = true;
			}
			this.Duration = Stat.Roll(41, 50);
		}

		// Token: 0x060088F0 RID: 35056 RVA: 0x00310894 File Offset: 0x0030EA94
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060088F1 RID: 35057 RVA: 0x00310897 File Offset: 0x0030EA97
		public override string GetDescription()
		{
			return "{{rubbergum|rubbergum}} tonic";
		}

		// Token: 0x060088F2 RID: 35058 RVA: 0x0031089E File Offset: 0x0030EA9E
		public override string GetStateDescription()
		{
			return "under the effects of {{rubbergum|rubbergum}} tonic";
		}

		// Token: 0x060088F3 RID: 35059 RVA: 0x003108A8 File Offset: 0x0030EAA8
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Compound("+100 electric resistance", '\n');
			if (base.Object.IsTrueKin())
			{
				stringBuilder.Compound("+50 cold resistance", '\n');
			}
			else
			{
				stringBuilder.Compound("+25 cold resistance", '\n');
			}
			stringBuilder.Compound("-1 AV", '\n');
			SavingThrows.AppendSaveBonusDescription(stringBuilder, 5, "Restraint", false, false, true);
			SavingThrows.AppendSaveBonusDescription(stringBuilder, -5, "Bleeding", false, false, true);
			if (base.Object.IsTrueKin())
			{
				stringBuilder.Compound("Only suffers 25% damage from bludgeoning attacks.", '\n').Compound("Only suffers 25% damage from falling.", '\n');
			}
			else
			{
				stringBuilder.Compound("Only suffers 50% damage from bludgeoning attacks.", '\n').Compound("Only suffers 50% damage from falling.", '\n');
			}
			stringBuilder.Compound("Can't be grabbed.", '\n');
			return stringBuilder.ToString();
		}

		// Token: 0x060088F4 RID: 35060 RVA: 0x00310979 File Offset: 0x0030EB79
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060088F5 RID: 35061 RVA: 0x0031097C File Offset: 0x0030EB7C
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_rubbergumTonic", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Object.IsPlayer())
			{
				Popup.Show("Your skin shrivels and dimples.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.ApplyChanges();
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x060088F6 RID: 35062 RVA: 0x00310A07 File Offset: 0x0030EC07
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("Your skin flattens out and stretches tautly around your body once again.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x060088F7 RID: 35063 RVA: 0x00310A34 File Offset: 0x0030EC34
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift(base.Object, "AV", -1, false);
			base.StatShifter.SetStatShift(base.Object, "ElectricResistance", 100, false);
			base.StatShifter.SetStatShift(base.Object, "ColdResistance", base.Object.IsTrueKin() ? 50 : 25, false);
		}

		// Token: 0x060088F8 RID: 35064 RVA: 0x00310A9F File Offset: 0x0030EC9F
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x060088F9 RID: 35065 RVA: 0x00310AB2 File Offset: 0x0030ECB2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ModifyDefendingSaveEvent.ID || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060088FA RID: 35066 RVA: 0x00310AD0 File Offset: 0x0030ECD0
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Restraint", E))
			{
				E.Roll += 5;
			}
			if (SavingThrows.Applicable("Bleeding", E))
			{
				E.Roll += -5;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088FB RID: 35067 RVA: 0x00310B10 File Offset: 0x0030ED10
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Overdosed && !base.Object.IsFlying && 35.in100())
			{
				base.Object.ApplyEffect(new Prone(), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088FC RID: 35068 RVA: 0x00310B4C File Offset: 0x0030ED4C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeApplyDamage");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeforeGrabbed");
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x060088FD RID: 35069 RVA: 0x00310B98 File Offset: 0x0030ED98
		public static bool AffectsDamage(Damage Dmg)
		{
			return Dmg != null && (Dmg.HasAttribute("Crushing") || Dmg.HasAttribute("Falling") || Dmg.HasAttribute("Cudgel") || Dmg.HasAttribute("Concussion"));
		}

		// Token: 0x060088FE RID: 35070 RVA: 0x00310BD4 File Offset: 0x0030EDD4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeGrabbed")
			{
				return false;
			}
			if (E.ID == "BeforeApplyDamage" || E.ID == "TonicAutoApplied")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (Rubbergum_Tonic.AffectsDamage(damage))
				{
					if (base.Object.IsTrueKin())
					{
						damage.Amount /= 4;
					}
					else
					{
						damage.Amount /= 2;
					}
				}
			}
			if (E.ID == "Overdose")
			{
				if (!this.Overdosed)
				{
					if (base.Object.IsPlayer())
					{
						if (base.Object.GetLongProperty("Overdosing", 0L) == 1L)
						{
							Popup.Show("Your mutant physiology reacts adversely to the tonic. Your skin starts to knot and misshape.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							Popup.Show("The tonics you ingested react adversely to each other. Your skin starts to knot and misshape.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					this.Overdosed = true;
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

		// Token: 0x060088FF RID: 35071 RVA: 0x00310D0C File Offset: 0x0030EF0C
		public override void ApplyAllergy(GameObject Object)
		{
			if (Object.GetLongProperty("Overdosing", 0L) == 1L)
			{
				Popup.Show("Your mutant physiology reacts adversely to the tonic. Your skin starts to knot and misshape.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else
			{
				Popup.Show("The tonics you ingested react adversely to each other. Your skin starts to knot and misshape.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			Object.ApplyEffect(new Rubbergum_Tonic_Allergy(this.Duration), null);
		}

		// Token: 0x06008900 RID: 35072 RVA: 0x00310D6C File Offset: 0x0030EF6C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 50 && num < 60)
			{
				E.Tile = null;
				E.RenderString = "O";
				E.ColorString = "&r";
			}
			return true;
		}

		// Token: 0x04003A63 RID: 14947
		public const int SAVE_BONUS = 5;

		// Token: 0x04003A64 RID: 14948
		public const string SAVE_BONUS_VS = "Restraint";

		// Token: 0x04003A65 RID: 14949
		public const int SAVE_PENALTY = -5;

		// Token: 0x04003A66 RID: 14950
		public const string SAVE_PENALTY_VS = "Bleeding";

		// Token: 0x04003A67 RID: 14951
		public bool bOverdose;

		// Token: 0x04003A68 RID: 14952
		private bool Overdosed;
	}
}
