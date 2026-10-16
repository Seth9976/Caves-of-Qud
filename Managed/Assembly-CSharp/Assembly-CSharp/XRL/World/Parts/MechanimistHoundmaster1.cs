using System;
using System.Collections.Generic;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000DDD RID: 3549
	[Serializable]
	public class MechanimistHoundmaster1 : IPart
	{
		// Token: 0x06009044 RID: 36932 RVA: 0x00340C90 File Offset: 0x0033EE90
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009045 RID: 36933 RVA: 0x00340C93 File Offset: 0x0033EE93
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009046 RID: 36934 RVA: 0x00340CA8 File Offset: 0x0033EEA8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				try
				{
					if (this.bCreated)
					{
						return true;
					}
					this.bCreated = true;
					Physics physics = this.ParentObject.Physics;
					List<Cell> list = new List<Cell>();
					physics.CurrentCell.GetAdjacentCells(4, list, true, true);
					List<Cell> list2 = new List<Cell>();
					foreach (Cell cell in list)
					{
						if (cell.IsEmpty())
						{
							list2.Add(cell);
						}
					}
					List<string> list3 = new List<string>();
					for (int i = 0; i < 9; i++)
					{
						list3.Add("Hyrkhound");
					}
					int num = 0;
					while (num < list3.Count && list2.Count > 0)
					{
						GameObject gameObject = GameObject.Create(list3[num]);
						gameObject.SetAlliedLeader<AllyHoundmaster>(this.ParentObject, 0, false);
						Cell randomElement = list2.GetRandomElement<Cell>(null);
						randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						gameObject.RequirePart<Frenzy>(false);
						gameObject.MakeActive(false);
						list2.Remove(randomElement);
						num++;
					}
				}
				catch
				{
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003C2B RID: 15403
		public bool bCreated;
	}
}
