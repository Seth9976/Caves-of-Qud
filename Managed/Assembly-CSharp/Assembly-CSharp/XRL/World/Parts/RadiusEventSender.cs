using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001084 RID: 4228
	[Serializable]
	public class RadiusEventSender : IPoweredPart
	{
		// Token: 0x0600A845 RID: 43077 RVA: 0x003D8980 File Offset: 0x003D6B80
		public RadiusEventSender()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600A846 RID: 43078 RVA: 0x003D8998 File Offset: 0x003D6B98
		public override bool SameAs(IPart p)
		{
			RadiusEventSender radiusEventSender = p as RadiusEventSender;
			return !(radiusEventSender.Event != this.Event) && radiusEventSender.Radius == this.Radius && radiusEventSender.RealRadius == this.RealRadius && base.SameAs(p);
		}

		// Token: 0x0600A847 RID: 43079 RVA: 0x003D89E8 File Offset: 0x003D6BE8
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A848 RID: 43080 RVA: 0x003D89EB File Offset: 0x003D6BEB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("EnteredCell");
			Registrar.Register("Equipped");
			Registrar.Register("Unequipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A849 RID: 43081 RVA: 0x003D8A24 File Offset: 0x003D6C24
		public void SendEvent()
		{
			if (!string.IsNullOrEmpty(this.Event) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null && currentCell.ParentZone != null)
				{
					if (this.Cells == null)
					{
						this.Cells = currentCell.ParentZone.FastFloodNeighbors(currentCell.X, currentCell.Y, this.Radius);
					}
					foreach (Cell cell in this.Cells)
					{
						if (!this.RealRadius || currentCell.RealDistanceTo(cell, true) <= (double)this.Radius)
						{
							cell.FireEvent(this.Event);
						}
					}
					if (this.ParentObject.InInventory != null)
					{
						this.Cells = null;
					}
				}
			}
		}

		// Token: 0x0600A84A RID: 43082 RVA: 0x003D8B30 File Offset: 0x003D6D30
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.SendEvent();
			}
			else if (E.ID == "EnteredCell")
			{
				this.Cells = null;
			}
			else if (E.ID == "Equipped")
			{
				E.GetGameObjectParameter("EquippingObject").RegisterPartEvent(this, "EnteredCell");
			}
			else if (E.ID == "Unequipped")
			{
				E.GetGameObjectParameter("UnequippingObject").UnregisterPartEvent(this, "EnteredCell");
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004578 RID: 17784
		public string Event;

		// Token: 0x04004579 RID: 17785
		public int Radius = 1;

		// Token: 0x0400457A RID: 17786
		public bool RealRadius;

		// Token: 0x0400457B RID: 17787
		[NonSerialized]
		private List<Cell> Cells;
	}
}
