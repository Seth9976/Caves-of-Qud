using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020011B4 RID: 4532
	[Serializable]
	public class AmbientPowerReceiver : IPoweredPart
	{
		// Token: 0x0600B3F7 RID: 46071 RVA: 0x0041A945 File Offset: 0x00418B45
		public AmbientPowerReceiver()
		{
			this.ChargeUse = 0;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B3F8 RID: 46072 RVA: 0x0041A970 File Offset: 0x00418B70
		public override bool SameAs(IPart p)
		{
			AmbientPowerReceiver ambientPowerReceiver = p as AmbientPowerReceiver;
			return ambientPowerReceiver != null && ambientPowerReceiver.MaxChargeRate == this.MaxChargeRate && ambientPowerReceiver.Grid == this.Grid && base.SameAs(p);
		}

		// Token: 0x0600B3F9 RID: 46073 RVA: 0x0041A9B1 File Offset: 0x00418BB1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteringZoneEvent.ID || ID == PrimePowerSystemsEvent.ID;
		}

		// Token: 0x0600B3FA RID: 46074 RVA: 0x0041A9CF File Offset: 0x00418BCF
		public override bool HandleEvent(PrimePowerSystemsEvent E)
		{
			if (this.ParentObject.HasPropertyOrTag("Furniture"))
			{
				this.ReceiveCharge(1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3FB RID: 46075 RVA: 0x0041A9F4 File Offset: 0x00418BF4
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			if (!E.Cell.ParentZone.IsWorldMap() && base.IsReady(false, true, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num;
				if (this.CheckAmbientPowerDistributionOf(E.Cell.ParentZone, out num))
				{
					if (this.ChargeRate == 0 && this.ParentObject.IsPlayerControlled())
					{
						base.DidX("are", "back on the ambient broadcast power grid", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
				}
				else if (this.ChargeRate > 0)
				{
					if (this.ParentObject.IsPlayer() && !E.System)
					{
						if (Popup.ShowYesNo("You are leaving the ambient broadcast power grid and transitioning to backup power. Are you sure?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
						{
							return false;
						}
					}
					else
					{
						base.DidX("are", "leaving the ambient broadcast power grid and transitioning to backup power", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
					}
				}
				this.ChargeRate = num;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3FC RID: 46076 RVA: 0x0041AAFC File Offset: 0x00418CFC
		public bool CheckAmbientPowerDistributionOf(Zone Z, out int ChargeRate)
		{
			ChargeRate = 0;
			if (Z == null)
			{
				return false;
			}
			string text;
			if (!The.ZoneManager.TryGetZoneProperty<string>(Z.ZoneID, "AmbientPowerGrid", out text))
			{
				return false;
			}
			if (this.Grid != "*" && !this.Grid.HasDelimitedSubstring(',', text, StringComparison.Ordinal))
			{
				return false;
			}
			int num;
			ChargeRate = (The.ZoneManager.TryGetZoneProperty<int>(Z.ZoneID, "AmbientPowerChargeRate", out num) ? Math.Min(num, this.MaxChargeRate) : this.MaxChargeRate);
			return ChargeRate > 0;
		}

		// Token: 0x0600B3FD RID: 46077 RVA: 0x0041AB85 File Offset: 0x00418D85
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B3FE RID: 46078 RVA: 0x0041AB88 File Offset: 0x00418D88
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ReceiveCharge(Amount);
		}

		// Token: 0x0600B3FF RID: 46079 RVA: 0x0041AB94 File Offset: 0x00418D94
		public void ReceiveCharge(int Turns = 1)
		{
			if (this.ChargeRate == -1)
			{
				this.CheckAmbientPowerDistributionOf(this.GetAnyBasisZone(), out this.ChargeRate);
			}
			if (this.ChargeRate > 0 && base.IsReady(true, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				this.ParentObject.ChargeAvailable(this.ChargeRate, 0L, Turns, false, null);
			}
		}

		// Token: 0x0400488F RID: 18575
		public int MaxChargeRate;

		// Token: 0x04004890 RID: 18576
		public string Grid = "*";

		// Token: 0x04004891 RID: 18577
		[NonSerialized]
		public int ChargeRate = -1;
	}
}
