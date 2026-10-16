using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000DD9 RID: 3545
	[Serializable]
	public class NaphtaaliPack : IPart
	{
		// Token: 0x06009034 RID: 36916 RVA: 0x00340512 File Offset: 0x0033E712
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009035 RID: 36917 RVA: 0x00340515 File Offset: 0x0033E715
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x06009036 RID: 36918 RVA: 0x0034052C File Offset: 0x0033E72C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			try
			{
				if (!this.Created)
				{
					this.Created = true;
					List<Cell> list = new List<Cell>();
					this.ParentObject.CurrentCell.GetAdjacentCells(4, list, true, true);
					List<Cell> list2 = new List<Cell>();
					foreach (Cell cell in list)
					{
						if (cell.IsEmpty())
						{
							list2.Add(cell);
						}
					}
					List<string> list3 = new List<string>();
					int num = Stat.Random(1, 3);
					int num2 = Stat.Random(1, 3);
					int num3 = Stat.Random(1, 3);
					int num4 = Stat.Random(1, 3);
					int num5 = Stat.Random(4, 8);
					for (int i = 0; i < num; i++)
					{
						list3.Add("Naphtaali Forager");
					}
					for (int j = 0; j < num2; j++)
					{
						list3.Add("Naphtaali Runt");
					}
					for (int k = 0; k < num3; k++)
					{
						list3.Add("Naphtaali Blowgunner");
					}
					for (int l = 0; l < num4; l++)
					{
						list3.Add("Naphtaali Stalker");
					}
					for (int m = 0; m < num5; m++)
					{
						list3.Add("Naphtaali Forager");
					}
					int num6 = 0;
					while (num6 < list3.Count && list2.Count > 0)
					{
						GameObject gameObject = GameObject.Create(list3[num6]);
						gameObject.SetAlliedLeader<AllyPack>(this.ParentObject, 0, false);
						Cell randomElement = list2.GetRandomElement<Cell>(null);
						randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject.MakeActive(false);
						list2.Remove(randomElement);
						num6++;
					}
				}
			}
			catch
			{
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04003C23 RID: 15395
		public bool Created;
	}
}
