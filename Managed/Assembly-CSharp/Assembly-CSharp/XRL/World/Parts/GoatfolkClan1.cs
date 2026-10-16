using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000DDC RID: 3548
	[Serializable]
	public class GoatfolkClan1 : IPart
	{
		// Token: 0x06009040 RID: 36928 RVA: 0x00340A61 File Offset: 0x0033EC61
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009041 RID: 36929 RVA: 0x00340A64 File Offset: 0x0033EC64
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x06009042 RID: 36930 RVA: 0x00340A7C File Offset: 0x0033EC7C
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
				int num = Stat.Random(2, 4);
				int num2 = Stat.Random(1, 2);
				int num3 = Stat.Random(1, 2);
				int num4 = (95.in100() ? 1 : 0);
				List<string> list2 = new List<string>(num + num2 + num3 + num4);
				for (int i = 0; i < num; i++)
				{
					list2.Add("Goatfolk Bully");
				}
				for (int j = 0; j < num2; j++)
				{
					list2.Add("Goatfolk Sower");
				}
				for (int k = 0; k < num3; k++)
				{
					list2.Add("Goatfolk Yurtwarden");
				}
				for (int l = 0; l < num4; l++)
				{
					list2.Add("Goatfolk Hornblower");
				}
				int m = 0;
				int count = list2.Count;
				while (m < count)
				{
					Cell randomElement = list.GetRandomElement<Cell>(null);
					if (randomElement == null)
					{
						break;
					}
					GameObject gameObject = GameObject.Create(list2[m]);
					if (this.Photosynthetic)
					{
						Mutations mutations = gameObject.RequirePart<Mutations>(false);
						if (!mutations.HasMutation("PhotosyntheticSkin"))
						{
							mutations.AddMutation(new PhotosyntheticSkin(), 1, true);
						}
					}
					gameObject.SetAlliedLeader<AllyClan>(this.ParentObject, 0, false);
					randomElement.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.MakeActive(false);
					list.Remove(randomElement);
					m++;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("GoatfolkClan1 setup", ex);
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04003C29 RID: 15401
		public bool Created;

		// Token: 0x04003C2A RID: 15402
		[NonSerialized]
		public bool Photosynthetic;
	}
}
