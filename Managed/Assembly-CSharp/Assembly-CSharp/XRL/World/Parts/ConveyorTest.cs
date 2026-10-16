using System;
using XRL.UI;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02001313 RID: 4883
	[Serializable]
	public class ConveyorTest : IPart
	{
		// Token: 0x0600BF05 RID: 48901 RVA: 0x0045F764 File Offset: 0x0045D964
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BF06 RID: 48902 RVA: 0x0045F767 File Offset: 0x0045D967
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BF07 RID: 48903 RVA: 0x0045F77C File Offset: 0x0045D97C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.EmptyCell, 1, 999, 40, 12, false, AllowVis.Any, null, null, null, null, "Conveyor Start", false, true);
				Cell cell2 = PickTarget.ShowPicker(PickTarget.PickStyle.EmptyCell, 1, 999, 40, 12, false, AllowVis.Any, null, null, null, null, "Conveyor End", false, true);
				new ConveyorBelt
				{
					x1 = cell.X,
					y1 = cell.Y,
					x2 = cell2.X,
					y2 = cell2.Y
				}.BuildZone(this.ParentObject.Physics.CurrentCell.ParentZone);
			}
			return base.FireEvent(E);
		}
	}
}
