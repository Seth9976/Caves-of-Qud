using System;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000ED9 RID: 3801
	[Serializable]
	public class MaintenanceSystems : IPoweredPart
	{
		// Token: 0x0600981F RID: 38943 RVA: 0x0037412F File Offset: 0x0037232F
		public MaintenanceSystems()
		{
			this.ChargeUse = 0;
			this.IsRustSensitive = false;
			this.IsPowerSwitchSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x06009820 RID: 38944 RVA: 0x00374153 File Offset: 0x00372353
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009821 RID: 38945 RVA: 0x00374156 File Offset: 0x00372356
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<HealsNaturallyEvent>.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x06009822 RID: 38946 RVA: 0x0037417C File Offset: 0x0037237C
		public override bool HandleEvent(HealsNaturallyEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x06009823 RID: 38947 RVA: 0x00374195 File Offset: 0x00372395
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.ProcessDamageControl(1);
			return base.HandleEvent(E);
		}

		// Token: 0x06009824 RID: 38948 RVA: 0x003741A6 File Offset: 0x003723A6
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.HitCounter = 10;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009825 RID: 38949 RVA: 0x003741C5 File Offset: 0x003723C5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009826 RID: 38950 RVA: 0x003741C8 File Offset: 0x003723C8
		public bool ProcessDamageControl(int Turns = 1)
		{
			if (this.HitCounter > 0)
			{
				this.HitCounter -= Turns;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return false;
			}
			bool flag = this.ParentObject.HasPart<Regeneration>();
			if (this.HitCounter > 0 && !flag)
			{
				return false;
			}
			int num = (20 + 2 * this.ParentObject.StatMod("Toughness", 0) + 2 * this.ParentObject.StatMod("Willpower", 0)) * Turns;
			MaintenanceSystems.eRegenerating.ID = "Regenerating";
			MaintenanceSystems.eRegenerating.SetParameter("Amount", num);
			this.ParentObject.FireEvent(MaintenanceSystems.eRegenerating);
			MaintenanceSystems.eRegenerating.ID = "Regenerating2";
			this.ParentObject.FireEvent(MaintenanceSystems.eRegenerating);
			num = MaintenanceSystems.eRegenerating.GetIntParameter("Amount", 0);
			if (num < 0)
			{
				num = 0;
			}
			if (this.HitCounter > 0)
			{
				num /= 2;
			}
			if (this.ParentObject.HasEffect<Meditating>())
			{
				num *= 3;
			}
			if (this.ParentObject.HasPart<LuminousInfection>() && base.IsDay() && this.ParentObject.CurrentZone != null && this.ParentObject.CurrentZone.Z <= 10)
			{
				num = num * 85 / 100;
			}
			this.RegenCounter += num;
			if (this.RegenCounter > 100)
			{
				int num2 = (int)Math.Floor((double)this.RegenCounter / 100.0);
				this.RegenCounter %= 100;
				this.ParentObject.GetStat("Hitpoints").Penalty -= num2;
				if (this.ParentObject.IsPlayer())
				{
					Sidebar.Update();
				}
			}
			return true;
		}

		// Token: 0x04003F5C RID: 16220
		public int RegenCounter;

		// Token: 0x04003F5D RID: 16221
		public int HitCounter;

		// Token: 0x04003F5E RID: 16222
		[NonSerialized]
		private static Event eRegenerating = new Event("Regenerating", "Amount", 0);
	}
}
