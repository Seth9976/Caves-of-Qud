using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000DD8 RID: 3544
	[Serializable]
	public class BaboonHero1Pack : IPart
	{
		// Token: 0x06009030 RID: 36912 RVA: 0x00340334 File Offset: 0x0033E534
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009031 RID: 36913 RVA: 0x00340337 File Offset: 0x0033E537
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x06009032 RID: 36914 RVA: 0x00340350 File Offset: 0x0033E550
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
				int num = Stat.Random(4, 8);
				int num2 = Stat.Random(1, 3);
				List<string> list2 = new List<string>(num + num2);
				int num3 = 0;
				while ((float)num3 < (float)num * this.FollowerMultiplier)
				{
					list2.Add("Baboon");
					num3++;
				}
				int num4 = 0;
				while ((float)num4 < (float)num2 * this.FollowerMultiplier)
				{
					list2.Add("Hulking Baboon");
					num4++;
				}
				int i = 0;
				int count = list2.Count;
				while (i < count)
				{
					Cell randomElement = list.GetRandomElement<Cell>(null);
					if (randomElement == null)
					{
						break;
					}
					GameObject gameObject = GameObject.Create(list2[i]);
					if (this.Hat)
					{
						gameObject.ReceiveObject("Leather Cap", false, 0, 0, null, null, null, null);
					}
					gameObject.SetAlliedLeader<AllyPack>(this.ParentObject, 0, false);
					randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					list.Remove(randomElement);
					i++;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("BaboonHero setup", ex);
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04003C1F RID: 15391
		public bool Created;

		// Token: 0x04003C20 RID: 15392
		[NonSerialized]
		public bool Hat;

		// Token: 0x04003C21 RID: 15393
		[NonSerialized]
		public int rings = 1;

		// Token: 0x04003C22 RID: 15394
		[NonSerialized]
		public float FollowerMultiplier = 1f;
	}
}
