using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F05 RID: 3845
	[Serializable]
	public class SpawningEggSac : IPart
	{
		// Token: 0x0600995F RID: 39263 RVA: 0x0037A9D4 File Offset: 0x00378BD4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009960 RID: 39264 RVA: 0x0037A9D7 File Offset: 0x00378BD7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06009961 RID: 39265 RVA: 0x0037A9F0 File Offset: 0x00378BF0
		public void tickEgg()
		{
			if (this.TurnsLeft == -2147483648)
			{
				this.TurnsLeft = Stat.Roll(this.Turns, null);
			}
			this.TurnsLeft--;
			if (this.TurnsLeft > 0)
			{
				return;
			}
			int num = Stat.Roll(this.SpawnCount, null);
			int num2 = num;
			Cell currentCell = this.ParentObject.CurrentCell;
			List<Cell> list = ((currentCell != null) ? currentCell.GetConnectedSpawnLocations(num2).Shuffle<Cell>(null) : null);
			if (list.IsNullOrEmpty<Cell>())
			{
				return;
			}
			foreach (Cell cell in list)
			{
				cell.AddObject(this.SpawnBlueprint, null, null, null, null);
				num2--;
				if (num2 <= 0)
				{
					break;
				}
			}
			if (this.ParentObject.IsVisible())
			{
				IComponent<GameObject>.AddPlayerMessage("The membrane of the egg sac snots apart.", null, true);
				if (num > 1)
				{
					IComponent<GameObject>.AddPlayerMessage("The svardym eggs hatch.", null, true);
				}
				else
				{
					IComponent<GameObject>.AddPlayerMessage("The svardym egg hatches.", null, true);
				}
			}
			this.ParentObject.PlayWorldSound("sfx_creature_appear_eggCrack", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			this.ParentObject.Slimesplatter(false, "&g.");
			this.ParentObject.Destroy(null, false, false, null);
		}

		// Token: 0x06009962 RID: 39266 RVA: 0x0037AB48 File Offset: 0x00378D48
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.ParentObject.CurrentCell != null)
			{
				this.tickEgg();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009963 RID: 39267 RVA: 0x0037AB64 File Offset: 0x00378D64
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003FE8 RID: 16360
		public string Turns = "10-20";

		// Token: 0x04003FE9 RID: 16361
		public int TurnsLeft = int.MinValue;

		// Token: 0x04003FEA RID: 16362
		public string SpawnCount = "2-4";

		// Token: 0x04003FEB RID: 16363
		public string SpawnBlueprint = "Svardym Hatchling";
	}
}
