using System;
using System.Collections.Generic;
using System.Linq;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013DE RID: 5086
	[Serializable]
	public class UnwelcomeGermination : BaseMutation
	{
		// Token: 0x0600C47F RID: 50303 RVA: 0x004785E9 File Offset: 0x004767E9
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C480 RID: 50304 RVA: 0x004785EC File Offset: 0x004767EC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C481 RID: 50305 RVA: 0x00478601 File Offset: 0x00476801
		public override string GetDescription()
		{
			return "You spasmodically engender wild plant growth around yourself for a short period.\n\nThere is a small chance each round that you enter into a compulsive state of mind for 30-39 rounds.\n\nDuring this time, there is a 25% chance each round that you summon several hostile plants nearby.";
		}

		// Token: 0x0600C482 RID: 50306 RVA: 0x00478608 File Offset: 0x00476808
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C483 RID: 50307 RVA: 0x00478610 File Offset: 0x00476810
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.ParentObject.CurrentZone == null || this.ParentObject.OnWorldMap())
				{
					return true;
				}
				if (2.in1000() && this.Germinating <= 0)
				{
					Popup.Show("{{r|Unwelcome images of {{G|budding plants}} fill your mind.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.Germinating = Stat.Random(30, 39);
				}
				if (this.Germinating > 0 && 25.in100())
				{
					UnwelcomeGermination.Germinate(this.ParentObject, Stat.Random(1, 10), 80, false, null);
					base.UseEnergy(1000, "Mental Mutation");
				}
				if (this.Germinating > 0)
				{
					this.Germinating--;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C484 RID: 50308 RVA: 0x004786DC File Offset: 0x004768DC
		public static void Germinate(GameObject gameObject, int level, int range = 80, bool friendly = false, Cell targetCell = null)
		{
			List<Cell> list = new List<Cell>();
			if (targetCell != null)
			{
				list.Add(targetCell);
				list.AddRange(targetCell.GetAdjacentCells(true));
				Burgeoning.PlantSummoning(list, friendly, gameObject, level, false);
				return;
			}
			List<Cell> list2 = (from c in gameObject.Physics.CurrentCell.ParentZone.GetEmptyVisibleCells()
				where c != null && c.DistanceTo(gameObject) <= range
				select c).ToList<Cell>();
			if (list2.Count > 0)
			{
				list.Add(list2.GetRandomElement<Cell>(null));
				list.AddRange(list[0].GetAdjacentCells(true));
				Burgeoning.PlantSummoning(list, friendly, gameObject, level, false);
			}
		}

		// Token: 0x0600C485 RID: 50309 RVA: 0x00478796 File Offset: 0x00476996
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x04004DD7 RID: 19927
		public int Germinating;
	}
}
