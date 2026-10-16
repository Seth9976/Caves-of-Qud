using System;
using HistoryKit;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000C3A RID: 3130
	[Serializable]
	public class Blaze_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06007F90 RID: 32656 RVA: 0x002F0C4D File Offset: 0x002EEE4D
		public Blaze_Tonic()
		{
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x002F0C55 File Offset: 0x002EEE55
		public Blaze_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x002F0C64 File Offset: 0x002EEE64
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdose = true;
			}
			this.Duration = Stat.Roll(41, 50);
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x002F0C85 File Offset: 0x002EEE85
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x002F0C88 File Offset: 0x002EEE88
		public override string GetDetails()
		{
			if (base.Object.IsTrueKin())
			{
				return "+100 Heat Resistance\nCan't be frozen.\n+25% heat damage dealt.\n-50 Cold Resistance\n+20 Quickness\nTemperature can't be increased by external heat sources.\nMust move around to avoid combusting and losing the effects of this tonic.";
			}
			return "+100 Heat Resistance.\nCan't be frozen.\n+25% heat damage dealt.\n-50 Cold Resistance\n+10 Quickness\nTemperature can't be increased by external heat sources.\nMust move around to avoid combusting and losing the effects of this tonic.";
		}

		// Token: 0x06007F95 RID: 32661 RVA: 0x002F0CA2 File Offset: 0x002EEEA2
		public override string GetDescription()
		{
			return "{{blaze|blaze}} tonic";
		}

		// Token: 0x06007F96 RID: 32662 RVA: 0x002F0CAC File Offset: 0x002EEEAC
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("Your stomach swells with a burning sensation.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (Object.Physics.Temperature <= Object.Physics.FreezeTemperature)
			{
				Object.Physics.Temperature = Object.Physics.FreezeTemperature + 1;
			}
			this.ApplyStats();
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x06007F97 RID: 32663 RVA: 0x002F0D3A File Offset: 0x002EEF3A
		public override void Remove(GameObject Object)
		{
			if (Object != null && Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("The " + this.GetDescription() + " burns out of your system.", null, true);
			}
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x06007F98 RID: 32664 RVA: 0x002F0D70 File Offset: 0x002EEF70
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("Speed", base.Object.IsTrueKin() ? 20 : 10, false);
			base.StatShifter.SetStatShift("ColdResistance", -50, false);
			base.StatShifter.SetStatShift("HeatResistance", 100, false);
		}

		// Token: 0x06007F99 RID: 32665 RVA: 0x002F0DCA File Offset: 0x002EEFCA
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06007F9A RID: 32666 RVA: 0x002F0DDD File Offset: 0x002EEFDD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<EndActionEvent>.ID;
		}

		// Token: 0x06007F9B RID: 32667 RVA: 0x002F0E04 File Offset: 0x002EF004
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == base.Object && E.Damage.IsHeatDamage())
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F9C RID: 32668 RVA: 0x002F0E60 File Offset: 0x002EF060
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (base.Object.Physics.Temperature <= base.Object.Physics.FreezeTemperature)
			{
				base.Object.Physics.Temperature = base.Object.Physics.FreezeTemperature + 1;
			}
			if (base.Object.Physics.Temperature >= base.Object.Physics.FlameTemperature)
			{
				base.Object.RemoveEffect(this, true);
			}
			else if (this.Duration > 0)
			{
				if (this.Duration != 9999)
				{
					this.Duration--;
				}
				if (this.Duration <= 0)
				{
					if (base.Object.IsPlayer())
					{
						Popup.Show("You start to cool off.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					base.Object.RemoveEffect(this, true);
				}
				else if (base.Object.CurrentCell != null)
				{
					this.x = base.Object.CurrentCell.X;
					this.y = base.Object.CurrentCell.Y;
					Zone currentZone = base.Object.CurrentZone;
					this.zone = ((currentZone != null) ? currentZone.ZoneID : null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F9D RID: 32669 RVA: 0x002F0FA5 File Offset: 0x002EF1A5
		public override bool HandleEvent(EndActionEvent E)
		{
			if (this.Duration > 0)
			{
				this.AfterMovement();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F9E RID: 32670 RVA: 0x002F0FC0 File Offset: 0x002EF1C0
		public void AfterMovement()
		{
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell != null)
			{
				if (this.x == currentCell.X && this.y == currentCell.Y && this.zone == currentCell.ParentZone.ZoneID)
				{
					base.Object.Physics.Temperature += 120;
					return;
				}
				if (base.Object.Physics.Temperature > 45)
				{
					base.Object.Physics.Temperature -= 20;
				}
			}
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x002F1058 File Offset: 0x002EF258
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("AttackerDealingDamage");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeforeTemperatureChange");
			Registrar.Register("Overdose");
			Registrar.Register("Juked");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007FA0 RID: 32672 RVA: 0x002F10B0 File Offset: 0x002EF2B0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Juked")
			{
				this.AfterMovement();
			}
			else if (E.ID == "AttackerDealingDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage.HasAttribute("Heat") || damage.HasAttribute("Fire"))
				{
					damage.Amount = (int)Math.Ceiling((double)((float)damage.Amount * 1.25f));
				}
			}
			else if (E.ID == "TonicAutoApplied")
			{
				Damage damage2 = E.GetParameter("Damage") as Damage;
				if (damage2 != null && damage2.IsHeatDamage())
				{
					damage2.Amount = 0;
					return false;
				}
			}
			else if (E.ID == "BeforeTemperatureChange")
			{
				if (E.GetIntParameter("Amount", 0) > 0)
				{
					return false;
				}
			}
			else if (E.ID == "Overdose")
			{
				if (this.Duration > 0)
				{
					this.Duration = 0;
					Blaze_Tonic.ApplyOverdose(base.Object);
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

		// Token: 0x06007FA1 RID: 32673 RVA: 0x002F1201 File Offset: 0x002EF401
		public override void ApplyAllergy(GameObject Object)
		{
			Blaze_Tonic.ApplyOverdose(Object);
		}

		// Token: 0x06007FA2 RID: 32674 RVA: 0x002F120C File Offset: 0x002EF40C
		public static void ApplyOverdose(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetLongProperty("Overdosing", 0L) == 1L)
				{
					Popup.Show("Your mutant physiology reacts adversely to the tonic. You erupt into flames!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("The tonics you ingested react adversely to each other. You erupt into flames!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (Object.Physics != null)
			{
				int num = Object.Physics.FlameTemperature + 200;
				if (Object.Physics.Temperature < num)
				{
					Object.Physics.Temperature = num;
				}
			}
		}

		// Token: 0x06007FA3 RID: 32675 RVA: 0x002F1294 File Offset: 0x002EF494
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "!";
				int num2 = Stat.RandomCosmetic(1, 3);
				if (num2 == 1)
				{
					E.ColorString = "&R";
				}
				else if (num2 == 2)
				{
					E.ColorString = "&W";
				}
				else if (num2 == 3)
				{
					E.ColorString = "&r";
				}
			}
			return true;
		}

		// Token: 0x040038D3 RID: 14547
		public int x;

		// Token: 0x040038D4 RID: 14548
		public int y;

		// Token: 0x040038D5 RID: 14549
		public string zone;

		// Token: 0x040038D6 RID: 14550
		public bool bOverdose;
	}
}
