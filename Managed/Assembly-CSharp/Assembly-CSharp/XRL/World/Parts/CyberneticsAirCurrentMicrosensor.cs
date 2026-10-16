using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F15 RID: 3861
	[Serializable]
	public class CyberneticsAirCurrentMicrosensor : IPart
	{
		// Token: 0x060099DA RID: 39386 RVA: 0x0037E14E File Offset: 0x0037C34E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060099DB RID: 39387 RVA: 0x0037E151 File Offset: 0x0037C351
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GenericDeepNotifyEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x060099DC RID: 39388 RVA: 0x0037E177 File Offset: 0x0037C377
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.RegisterEvent(this, EnteredCellEvent.ID, 0, true);
			return base.HandleEvent(E);
		}

		// Token: 0x060099DD RID: 39389 RVA: 0x0037E193 File Offset: 0x0037C393
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.UnregisterEvent(this, EnteredCellEvent.ID);
			return base.HandleEvent(E);
		}

		// Token: 0x060099DE RID: 39390 RVA: 0x0037E1B0 File Offset: 0x0037C3B0
		public override bool HandleEvent(GenericDeepNotifyEvent E)
		{
			if (E.Notify == "MemoriesEaten" || E.Notify == "AmnesiaTriggered")
			{
				GameObject implantee = this.ParentObject.Implantee;
				if (implantee != null && E.Subject == implantee && implantee.IsPlayer())
				{
					this.RevealStairs();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099DF RID: 39391 RVA: 0x0037E20F File Offset: 0x0037C40F
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (E.Actor != null && CyberneticsAirCurrentMicrosensor.ShouldRevealStairs(E.Actor) && E.Cell.ParentZone != this.lastZone)
			{
				this.RevealStairs();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060099E0 RID: 39392 RVA: 0x0037E248 File Offset: 0x0037C448
		public int RevealStairs()
		{
			int num = 0;
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && CyberneticsAirCurrentMicrosensor.ShouldRevealStairs(implantee))
			{
				this.lastZone = implantee.CurrentZone;
				if (this.lastZone != null)
				{
					foreach (Cell cell in this.lastZone.GetCells())
					{
						if (cell.HasStairs() && !cell.IsExplored())
						{
							cell.SetExplored(true);
							num++;
						}
					}
				}
				if (num > 0)
				{
					implantee.PlayWorldSound("Sounds/Interact/sfx_interact_mapDevice_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return num;
		}

		// Token: 0x060099E1 RID: 39393 RVA: 0x0037E318 File Offset: 0x0037C518
		public static bool ShouldRevealStairs(GameObject POV)
		{
			return POV.IsPlayer() || (POV.IsPlayerLed() && POV.IsAudible(The.Player, 20));
		}

		// Token: 0x04004026 RID: 16422
		[NonSerialized]
		public Zone lastZone;
	}
}
