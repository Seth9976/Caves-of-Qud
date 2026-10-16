using System;
using System.Text;
using XRL.Core;
using XRL.Language;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000DA7 RID: 3495
	[Serializable]
	public class VehicleUnpowered : Effect
	{
		// Token: 0x06008B86 RID: 35718 RVA: 0x003195DB File Offset: 0x003177DB
		public VehicleUnpowered()
		{
			this.DisplayName = "{{K|unpowered}}";
			this.Duration = 1;
		}

		// Token: 0x06008B87 RID: 35719 RVA: 0x003195F5 File Offset: 0x003177F5
		public VehicleUnpowered(int ChargeMinimum)
			: this()
		{
			this.ChargeMinimum = ChargeMinimum;
		}

		// Token: 0x06008B88 RID: 35720 RVA: 0x00319604 File Offset: 0x00317804
		public override bool Apply(GameObject Object)
		{
			return !Object.HasEffect(typeof(VehicleUnpowered)) && base.Apply(Object);
		}

		// Token: 0x06008B89 RID: 35721 RVA: 0x00319621 File Offset: 0x00317821
		public override int GetEffectType()
		{
			return 1;
		}

		// Token: 0x06008B8A RID: 35722 RVA: 0x00319624 File Offset: 0x00317824
		public override string GetDetails()
		{
			return "Can't take actions.";
		}

		// Token: 0x06008B8B RID: 35723 RVA: 0x0031962C File Offset: 0x0031782C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.GetCurrentFrameAtFPS(60, 1f) % 300;
			if (num >= 80 && num < 180)
			{
				E.Tile = "Items/sw_power_cut_small.png";
				E.ColorString = "&W";
				E.DetailColor = "R";
			}
			return base.Render(E);
		}

		// Token: 0x06008B8C RID: 35724 RVA: 0x00319681 File Offset: 0x00317881
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanMoveExtremities");
			Registrar.Register("IsMobile");
		}

		// Token: 0x06008B8D RID: 35725 RVA: 0x003196A4 File Offset: 0x003178A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
			{
				if (E.HasFlag("ShowMessage"))
				{
					this.PreventActionMessage(base.Object);
				}
				return false;
			}
			return !(E.ID == "IsMobile") && base.FireEvent(E);
		}

		// Token: 0x06008B8E RID: 35726 RVA: 0x0031970C File Offset: 0x0031790C
		public void PreventActionMessage(GameObject Actor)
		{
			if (!Actor.IsPlayer())
			{
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			EnergyCellSocket energyCellSocket;
			if (base.Object.TryGetPart<EnergyCellSocket>(out energyCellSocket))
			{
				if (GameObject.Validate(energyCellSocket.Cell))
				{
					stringBuilder.Append(energyCellSocket.Cell.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false)).Append(" drained or nearly drained.\n\n").Append("Recharge or replace " + energyCellSocket.Cell.it + " to power ")
						.Append(base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, false, false, null, true, null, false))
						.Append('.');
				}
				else
				{
					stringBuilder.Append("Insert ").Append(Grammar.A(energyCellSocket.GetSlotTypeName(), false)).Append(" to power ")
						.Append(base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, false, false, null, true, null, false))
						.Append('.');
				}
			}
			else
			{
				stringBuilder.Append(base.Object.Does("lack", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, false, null, true, null, false)).Append(" the power to act.");
			}
			Popup.ShowFail(Event.FinalizeString(stringBuilder), true, true, true);
		}

		// Token: 0x06008B8F RID: 35727 RVA: 0x00319896 File Offset: 0x00317A96
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == CellChangedEvent.ID || ID == LeaveCellEvent.ID;
		}

		// Token: 0x06008B90 RID: 35728 RVA: 0x003198C4 File Offset: 0x00317AC4
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (E.Object == base.Object)
			{
				if (E.ShowMessage)
				{
					this.PreventActionMessage(E.Object);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B91 RID: 35729 RVA: 0x003198F1 File Offset: 0x00317AF1
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return (E.Object != base.Object || this.CheckCharge() || E.Object.IsPlayer()) && base.HandleEvent(E);
		}

		// Token: 0x06008B92 RID: 35730 RVA: 0x0031991F File Offset: 0x00317B1F
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckCharge();
			return base.HandleEvent(E);
		}

		// Token: 0x06008B93 RID: 35731 RVA: 0x0031992F File Offset: 0x00317B2F
		public override bool HandleEvent(LeaveCellEvent E)
		{
			if (E.Object == base.Object && !E.Forced && !E.System && E.Dragging == null)
			{
				this.PreventActionMessage(E.Object);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B94 RID: 35732 RVA: 0x0031996C File Offset: 0x00317B6C
		public bool CheckCharge()
		{
			if (this.ChargeMinimum > 0 && base.Object.TestCharge(this.ChargeMinimum, false, 0L, true, true))
			{
				base.Object.RemoveEffect(this, true);
				return true;
			}
			return false;
		}

		// Token: 0x04003AD1 RID: 15057
		public int ChargeMinimum;
	}
}
