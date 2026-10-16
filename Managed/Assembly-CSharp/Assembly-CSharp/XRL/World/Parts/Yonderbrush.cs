using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000E97 RID: 3735
	[Serializable]
	public class Yonderbrush : IPart
	{
		// Token: 0x06009648 RID: 38472 RVA: 0x003676FD File Offset: 0x003658FD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == GetNavigationWeightEvent.ID && !this.Harvested) || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009649 RID: 38473 RVA: 0x00367724 File Offset: 0x00365924
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!this.Harvested)
			{
				Hidden part = this.ParentObject.GetPart<Hidden>();
				if ((part == null || part.Found) && E.PhaseMatches(this.ParentObject))
				{
					E.MinWeight(95);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600964A RID: 38474 RVA: 0x00367770 File Offset: 0x00365970
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (!this.Harvested && this.ParentObject.CurrentCell != null)
			{
				if (E.Object != null && E.Object.IsCombat && this.ParentObject.Brain.IsHostileTowards(E.Object) && E.Object != this.ParentObject)
				{
					Hidden part = this.ParentObject.GetPart<Hidden>();
					if ((part == null || part.Found) && E.Object.HasPart<CookingAndGathering_Harvestry>())
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
					E.Object.RandomTeleport(true, null, null, null, null, 0, 0, true, null, false, true, true, false);
				}
				else if (E.Object != null && E.Object.IsPlayer() && !this.ParentObject.IsHostileTowards(E.Object))
				{
					Hidden part3 = this.ParentObject.GetPart<Hidden>();
					if (part3 != null)
					{
						part3.Reveal();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003E3E RID: 15934
		public bool Harvested;
	}
}
