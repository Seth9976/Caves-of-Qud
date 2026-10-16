using System;
using System.Linq;
using Genkit;
using JoppaTutorial;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000DBC RID: 3516
	[Serializable]
	public class TutorialSnapjaw : IPart
	{
		// Token: 0x06008C45 RID: 35909 RVA: 0x0031C15D File Offset: 0x0031A35D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeAITakingActionEvent>.ID || ID == BeforeDeathRemovalEvent.ID || ID == AttackerDealingDamageEvent.ID;
		}

		// Token: 0x06008C46 RID: 35910 RVA: 0x0031C183 File Offset: 0x0031A383
		public override bool HandleEvent(AttackerDealingDamageEvent E)
		{
			if (E.Damage.Amount >= The.Player.hitpoints)
			{
				E.Damage.Amount = The.Player.hitpoints - 1;
			}
			return true;
		}

		// Token: 0x06008C47 RID: 35911 RVA: 0x0031C1B4 File Offset: 0x0031A3B4
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			(TutorialManager.currentStep as FightSnapjaw).leatherArmor = this.ParentObject.CurrentCell.AddObject("TutorialLeatherArmor", null, null, null, null);
			GameObject leatherArmor = (TutorialManager.currentStep as FightSnapjaw).leatherArmor;
			if (leatherArmor != null)
			{
				leatherArmor.RequireID();
			}
			(TutorialManager.currentStep as FightSnapjaw).snapjawDead = true;
			this.ParentObject.GetEquippedObjects().ToList<GameObject>().ForEach(delegate(GameObject i)
			{
				i.Obliterate(null, false, null);
			});
			this.ParentObject.GetPart<Inventory>().DropOnDeath = false;
			this.ParentObject.Inventory.Clear();
			this.ParentObject.Obliterate(null, false, null);
			return true;
		}

		// Token: 0x06008C48 RID: 35912 RVA: 0x0031C27C File Offset: 0x0031A47C
		public override bool HandleEvent(BeforeAITakingActionEvent E)
		{
			if (this.BehaviorStage == TutorialSnapjaw.BehaviorStageType.START)
			{
				if (!this.ParentObject.IsVisible() && this.ParentObject.DistanceTo(The.PlayerCell) > 3)
				{
					this.ParentObject.Energy.BaseValue = 999;
					return false;
				}
				this.ParentObject.ForceUnequipRemoveAndRemoveContents(true, true);
				this.BehaviorStage = TutorialSnapjaw.BehaviorStageType.STEPSOUTH;
			}
			if (this.BehaviorStage == TutorialSnapjaw.BehaviorStageType.STEPSOUTH)
			{
				this.ParentObject.Move("S", false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
				this.BehaviorStage = TutorialSnapjaw.BehaviorStageType.ATTACK;
				this.ParentObject.Brain.Target = The.Player;
				this.ParentObject.Brain.PushGoal(new Kill(The.Player, false));
				FightSnapjaw fightSnapjaw = TutorialManager.currentStep as FightSnapjaw;
				if (fightSnapjaw != null)
				{
					GameObject parentObject = this.ParentObject;
					Location2D location2D;
					if (parentObject == null)
					{
						location2D = null;
					}
					else
					{
						Cell currentCell = parentObject.CurrentCell;
						location2D = ((currentCell != null) ? currentCell.Location : null);
					}
					fightSnapjaw.SnapjawSeen(location2D);
				}
				this.ParentObject.Energy.BaseValue = -11;
				return false;
			}
			return true;
		}

		// Token: 0x04003AFC RID: 15100
		public TutorialSnapjaw.BehaviorStageType BehaviorStage;

		// Token: 0x02001C3C RID: 7228
		public enum BehaviorStageType
		{
			// Token: 0x04006F92 RID: 28562
			START,
			// Token: 0x04006F93 RID: 28563
			STEPSOUTH = 100,
			// Token: 0x04006F94 RID: 28564
			ATTACK = 200
		}
	}
}
