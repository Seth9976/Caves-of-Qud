using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E9B RID: 3739
	[Serializable]
	public class DischargeOnDeath : IPart
	{
		// Token: 0x06009658 RID: 38488 RVA: 0x00367B8C File Offset: 0x00365D8C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06009659 RID: 38489 RVA: 0x00367BA4 File Offset: 0x00365DA4
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.ParentObject.CurrentCell != null)
			{
				int num = this.Arcs.RollCached();
				if (num > 0)
				{
					List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
					for (int i = 0; i < num; i++)
					{
						Cell randomElement = adjacentCells.GetRandomElement<Cell>(null);
						if (randomElement == null)
						{
							break;
						}
						this.ParentObject.Discharge(randomElement, this.Voltage, 0, this.DamageRange, null, this.ParentObject, this.ParentObject, null, null, null, null, null, 0, false, false, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600965A RID: 38490 RVA: 0x00367C2E File Offset: 0x00365E2E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003E44 RID: 15940
		public string Arcs = "1";

		// Token: 0x04003E45 RID: 15941
		public int Voltage = 3;

		// Token: 0x04003E46 RID: 15942
		public string DamageRange = "1d8";
	}
}
