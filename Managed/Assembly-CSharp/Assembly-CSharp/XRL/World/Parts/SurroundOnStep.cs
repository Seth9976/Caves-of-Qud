using System;
using System.Linq;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000F09 RID: 3849
	[Serializable]
	public class SurroundOnStep : IPart
	{
		// Token: 0x0600998E RID: 39310 RVA: 0x0037C412 File Offset: 0x0037A612
		public static string GetRandomRainbowColor()
		{
			return SurroundOnStep.ColorList.GetRandomElement<string>(null);
		}

		// Token: 0x0600998F RID: 39311 RVA: 0x0037C41F File Offset: 0x0037A61F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009990 RID: 39312 RVA: 0x0037C438 File Offset: 0x0037A638
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (!this.Harvested && E.Object != null && E.Object != this.ParentObject && E.Object.IsCombatObject() && this.ParentObject.IsHostileTowards(E.Object) && this.ParentObject.PhaseAndFlightMatches(E.Object))
			{
				Hidden part = this.ParentObject.GetPart<Hidden>();
				if (this.AllowHarvest && (part == null || part.Found) && E.Object.HasPart<CookingAndGathering_Harvestry>())
				{
					CookingAndGathering_Harvestry part2 = E.Object.GetPart<CookingAndGathering_Harvestry>();
					if (part2.IsMyActivatedAbilityToggledOn(part2.ActivatedAbilityID, null))
					{
						this.Harvested = true;
						return true;
					}
				}
				if (part != null)
				{
					part.Found = true;
				}
				foreach (Cell cell in from c in E.Cell.GetLocalAdjacentCells(this.Radius, false)
					where c.Location.ManhattanDistance(this.ParentObject.Physics.CurrentCell.Location) == this.Radius
					select c)
				{
					if (!this.EmptyOnly || cell.IsEmpty())
					{
						cell.AddObject(this.SurroundObject, "Spawning", null, null, null);
						if (50.in100())
						{
							The.ParticleManager.AddSinusoidal(SurroundOnStep.GetRandomRainbowColor() + "\r", (float)cell.X, (float)cell.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
						}
						else
						{
							The.ParticleManager.AddSinusoidal(SurroundOnStep.GetRandomRainbowColor() + "\u000e", (float)cell.X, (float)cell.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003FFC RID: 16380
		public string SurroundObject = "PlantWall";

		// Token: 0x04003FFD RID: 16381
		public bool EmptyOnly = true;

		// Token: 0x04003FFE RID: 16382
		public bool AllowHarvest;

		// Token: 0x04003FFF RID: 16383
		public bool Harvested;

		// Token: 0x04004000 RID: 16384
		public int Radius = 1;

		// Token: 0x04004001 RID: 16385
		[NonSerialized]
		public static string[] ColorList = new string[] { "&R", "&G", "&B", "&M", "&Y", "&W" };
	}
}
