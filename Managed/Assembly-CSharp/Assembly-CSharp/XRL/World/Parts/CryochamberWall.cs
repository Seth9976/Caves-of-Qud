using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200129E RID: 4766
	[Serializable]
	public class CryochamberWall : IPart
	{
		// Token: 0x0600BC65 RID: 48229 RVA: 0x0044E142 File Offset: 0x0044C342
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC66 RID: 48230 RVA: 0x0044E145 File Offset: 0x0044C345
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC67 RID: 48231 RVA: 0x0044E15C File Offset: 0x0044C35C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WalltrapTrigger")
			{
				Cell cell = this.ParentObject.GetCurrentCell();
				if (cell != null && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed())
				{
					List<string> adjacentDirections = Directions.GetAdjacentDirections(this.Direction, 1);
					Cell cellFromDirection = cell.GetCellFromDirection(adjacentDirections[0], true);
					Cell cellFromDirection2 = cell.GetCellFromDirection(adjacentDirections[1], true);
					if (cellFromDirection != null && cellFromDirection.IsEmpty() && 50.in100())
					{
						cell = cellFromDirection;
					}
					if (cellFromDirection2 != null && cellFromDirection2.IsEmpty() && 50.in100())
					{
						cell = cellFromDirection2;
					}
					if (cell != null && !cell.IsSolid(true))
					{
						if (!cell.HasObjectWithPart("Gas"))
						{
							cell.AddObject("CryoGas", null, null, null, null);
						}
						cell = cell.GetCellFromDirection(this.Direction, true);
						if (cell != null && !cell.IsSolid(true) && !cell.HasObjectWithPart("Gas"))
						{
							cell.AddObject("CryoGas", null, null, null, null);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04004B80 RID: 19328
		public string Direction = "SE";
	}
}
