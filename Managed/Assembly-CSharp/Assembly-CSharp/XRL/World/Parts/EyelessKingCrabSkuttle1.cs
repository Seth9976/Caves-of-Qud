using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000DDA RID: 3546
	[Serializable]
	public class EyelessKingCrabSkuttle1 : IPart
	{
		// Token: 0x06009038 RID: 36920 RVA: 0x00340728 File Offset: 0x0033E928
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009039 RID: 36921 RVA: 0x0034072B File Offset: 0x0033E92B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600903A RID: 36922 RVA: 0x00340744 File Offset: 0x0033E944
		public override bool HandleEvent(EnteredCellEvent E)
		{
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
				int num = Stat.Random(1, 4);
				int num2 = Stat.Random(0, 1);
				List<string> list2 = new List<string>(num + num2);
				for (int i = 0; i < num; i++)
				{
					list2.Add("Eyeless Crab");
				}
				for (int j = 0; j < num2; j++)
				{
					list2.Add("Rustacean");
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
				MetricsManager.LogError("BethsaidaWorshipers1 setup", ex);
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04003C24 RID: 15396
		public bool Created;
	}
}
