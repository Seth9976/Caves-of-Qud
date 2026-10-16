using System;
using Genkit;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B9C RID: 2972
	[Serializable]
	public class LayMineGoal : GoalHandler
	{
		// Token: 0x0600768F RID: 30351 RVA: 0x002C9034 File Offset: 0x002C7234
		public LayMineGoal(Location2D Target, string Mine, string MineName = "", string Timer = "-1", int HideDifficulty = 0)
		{
			this.Target = Target;
			this.Mine = Mine;
			this.MineName = MineName;
			this.Timer = Timer;
			this.HideDifficulty = HideDifficulty;
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x002C906C File Offset: 0x002C726C
		public override void Create()
		{
			base.Think("I'm trying to lay mines!");
		}

		// Token: 0x06007691 RID: 30353 RVA: 0x002C9079 File Offset: 0x002C7279
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x06007692 RID: 30354 RVA: 0x002C907C File Offset: 0x002C727C
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x06007693 RID: 30355 RVA: 0x002C9080 File Offset: 0x002C7280
		public override void TakeAction()
		{
			if (this.Target == null)
			{
				base.Think("I don't have a target anymore!");
				base.FailToParent();
				return;
			}
			Miner miner;
			if (!base.ParentObject.TryGetPart<Miner>(out miner))
			{
				base.Think("I'm not a miner any more!");
				base.FailToParent();
				return;
			}
			if (!miner.IsMyActivatedAbilityAIUsable(miner.ActivatedAbilityID, null))
			{
				base.Think("I'm not supposed to be laying mines any more!");
				base.FailToParent();
				return;
			}
			if (base.ParentObject.DistanceTo(this.Target) == 1)
			{
				int num = this.Timer.RollCached();
				base.Think((num > 0) ? "I'm going to set a bomb!" : "I'm going to lay a mine!");
				GameObject gameObject = ((num > 0) ? Tinkering_LayMine.CreateBomb(this.Mine, base.ParentObject, num) : Tinkering_LayMine.CreateMine(this.Mine, base.ParentObject));
				if (this.HideDifficulty > 0)
				{
					gameObject.RequirePart<Hidden>(false).Difficulty = this.HideDifficulty;
				}
				base.ParentObject.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				base.ParentObject.UseEnergy(1000);
				base.FailToParent();
				base.ParentObject.Brain.DidXToY("place", gameObject, null, null, null, null, base.ParentObject, null, false, false, true, false, false, null, null, null, false, true, false, false, false, null);
				return;
			}
			if (!base.MoveTowards(base.ParentObject.CurrentZone.GetCell(this.Target), false, true))
			{
				base.Think("I can't get to my target!");
				base.FailToParent();
			}
		}

		// Token: 0x0400364F RID: 13903
		public Location2D Target;

		// Token: 0x04003650 RID: 13904
		public string Mine;

		// Token: 0x04003651 RID: 13905
		public string MineName;

		// Token: 0x04003652 RID: 13906
		public string Timer = "-1";

		// Token: 0x04003653 RID: 13907
		public int HideDifficulty;
	}
}
