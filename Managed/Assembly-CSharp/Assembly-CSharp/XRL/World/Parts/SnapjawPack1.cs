using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000DDE RID: 3550
	[Serializable]
	public class SnapjawPack1 : IPart
	{
		// Token: 0x06009048 RID: 36936 RVA: 0x00340E28 File Offset: 0x0033F028
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009049 RID: 36937 RVA: 0x00340E2B File Offset: 0x0033F02B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.Created);
		}

		// Token: 0x0600904A RID: 36938 RVA: 0x00340E4C File Offset: 0x0033F04C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Created)
			{
				this.Created = true;
				try
				{
					List<Cell> list = Event.NewCellList();
					foreach (Cell cell in this.ParentObject.CurrentCell.GetAdjacentCells(4, true, true))
					{
						if (cell.IsEmptyOfSolid(true))
						{
							list.Add(cell);
						}
					}
					int num = Stat.Random(1, 3);
					int num2 = Stat.Random(4, 8);
					List<string> list2 = new List<string>(num + num2);
					for (int i = 0; i < num; i++)
					{
						list2.Add("Snapjaw Hunter 1");
					}
					for (int j = 0; j < num2; j++)
					{
						list2.Add("Snapjaw Scavenger 1");
					}
					int k = 0;
					int count = list2.Count;
					while (k < count)
					{
						Cell randomElement = list.GetRandomElement<Cell>(null);
						if (randomElement == null)
						{
							break;
						}
						GameObject gameObject = GameObject.Create(list2[k]);
						gameObject.PartyLeader = this.ParentObject;
						randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject.MakeActive(false);
						list.Remove(randomElement);
						k++;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("SnapjawPack1 setup", ex);
				}
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C2C RID: 15404
		public bool Created;
	}
}
