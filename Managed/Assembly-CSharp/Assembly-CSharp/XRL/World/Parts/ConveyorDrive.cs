using System;

namespace XRL.World.Parts
{
	// Token: 0x02001311 RID: 4881
	[Serializable]
	public class ConveyorDrive : IPart
	{
		// Token: 0x0600BEF8 RID: 48888 RVA: 0x0045EBC9 File Offset: 0x0045CDC9
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BEF9 RID: 48889 RVA: 0x0045EBCC File Offset: 0x0045CDCC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BEFA RID: 48890 RVA: 0x0045EBEC File Offset: 0x0045CDEC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" || E.ID == "EnteredCell")
			{
				if (base.IsEMPed() || base.IsBroken() || base.IsRusted())
				{
					return true;
				}
				this.CurrentTurn++;
				if (this.CurrentTurn >= this.TurnsPerImpulse)
				{
					this.CurrentTurn = 0;
					if (this.ParentObject.Physics.CurrentCell != null)
					{
						foreach (Cell cell in this.ParentObject.Physics.CurrentCell.GetCardinalAdjacentCells(false, true, false))
						{
							foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("ConveyorPad"))
							{
								gameObject.GetPart<ConveyorPad>().ConveyorImpulse(60, Event.NewGameObjectList());
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004CFE RID: 19710
		public int TurnsPerImpulse = 2;

		// Token: 0x04004CFF RID: 19711
		public int CurrentTurn;
	}
}
