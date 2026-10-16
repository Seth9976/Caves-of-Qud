using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012B5 RID: 4789
	[Serializable]
	public class VehicleMeleeInfiltration : IPart
	{
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600BD4C RID: 48460 RVA: 0x004548F0 File Offset: 0x00452AF0
		public Vehicle Vehicle
		{
			get
			{
				Vehicle vehicle;
				if ((vehicle = this._Vehicle) == null)
				{
					vehicle = (this._Vehicle = this.ParentObject.GetPart<Vehicle>());
				}
				return vehicle;
			}
		}

		// Token: 0x0600BD4D RID: 48461 RVA: 0x0045491C File Offset: 0x00452B1C
		public bool TryInfiltrate(GameObject Actor, Interior Interior)
		{
			uint stableHashCode = this.ParentObject.CurrentZone.ZoneID.GetStableHashCode32();
			Interior.ZoneID.GetStableHashCode32(ref stableHashCode);
			if (VehicleMeleeInfiltration.LastAttempt != stableHashCode)
			{
				if (Popup.ShowYesNo(Event.FinalizeString(Event.NewStringBuilder(this.ParentObject.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false)).Append(" occupied.\n\n").Append("Attempting to enter is a hostile action and will function as an attack with ")
					.Append(this.ChancePerPenetration)
					.Append("% infiltration chance per penetration.\n\n")
					.Append("Are you sure you want to proceed?")), "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
				{
					return false;
				}
				VehicleMeleeInfiltration.LastAttempt = stableHashCode;
			}
			if (The.Core.IDKFA)
			{
				return true;
			}
			try
			{
				this.InfiltrationChance = -1;
				this.ParentObject.RegisterPartEvent(this, "DefenderAfterAttack");
				Combat.PerformMeleeAttack(Actor, this.ParentObject, 1000, 0, 0, 0, null, false);
			}
			finally
			{
				this.ParentObject.UnregisterPartEvent(this, "DefenderAfterAttack");
			}
			return this.ParentObject.IsValid() && this.InfiltrationChance.in100();
		}

		// Token: 0x0600BD4E RID: 48462 RVA: 0x00454A58 File Offset: 0x00452C58
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<CanEnterInteriorEvent>.ID;
		}

		// Token: 0x0600BD4F RID: 48463 RVA: 0x00454A70 File Offset: 0x00452C70
		public override bool HandleEvent(CanEnterInteriorEvent E)
		{
			if (E.Object == this.ParentObject && !E.Status.HasBit(30) && !this.Vehicle.PilotID.IsNullOrEmpty())
			{
				if (this.AllowAllied && E.Object.IsAlliedTowards(E.Actor))
				{
					return base.HandleEvent(E);
				}
				if (this.AllowNeutral && !E.Object.IsHostileTowards(E.Actor))
				{
					return base.HandleEvent(E);
				}
				if (!E.Actor.IsPlayer())
				{
					E.Status = 2;
					return false;
				}
				if (!E.Action)
				{
					(ref E.Status).SetBit(1, false);
				}
				else
				{
					if (this.TryInfiltrate(E.Actor, E.Interior))
					{
						E.Status = 0;
						if (!Achievement.INFILTRATE_MECHA.Achieved && E.Actor.IsPlayer() && this.ParentObject.GetBlueprint(true).DescendsFrom("VehicleTemplarMech"))
						{
							Achievement.INFILTRATE_MECHA.Unlock();
						}
						if (E.ShowMessage)
						{
							IComponent<GameObject>.EmitMessage(E.Actor, E.Actor.Does("infiltrate", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + E.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", ' ', false, false, false, E.Actor, null);
						}
					}
					else
					{
						E.Status = 1;
					}
					E.ShowMessage = false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD50 RID: 48464 RVA: 0x00454C28 File Offset: 0x00452E28
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DefenderAfterAttack" && GameObject.Validate(this.ParentObject) && this.InfiltrationChance == -1)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				if (GameObject.Validate(gameObjectParameter) && gameObjectParameter.IsPlayer())
				{
					this.InfiltrationChance = E.GetIntParameter("Penetrations", 0) * this.ChancePerPenetration;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004BDA RID: 19418
		public int ChancePerPenetration = 10;

		// Token: 0x04004BDB RID: 19419
		public bool AllowAllied;

		// Token: 0x04004BDC RID: 19420
		public bool AllowNeutral;

		// Token: 0x04004BDD RID: 19421
		[NonSerialized]
		private static uint LastAttempt;

		// Token: 0x04004BDE RID: 19422
		[NonSerialized]
		private int InfiltrationChance;

		// Token: 0x04004BDF RID: 19423
		[NonSerialized]
		private Vehicle _Vehicle;
	}
}
