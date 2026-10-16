using System;
using System.Linq;
using JoppaTutorial;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000DBB RID: 3515
	[Serializable]
	public class TutorialBear : IPart
	{
		// Token: 0x06008C3C RID: 35900 RVA: 0x0031BB8D File Offset: 0x00319D8D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeTakeActionEvent>.ID || ID == BeforeDeathRemovalEvent.ID || ID == AttackerDealingDamageEvent.ID || ID == GetDefenderHitDiceEvent.ID || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x06008C3D RID: 35901 RVA: 0x0031BBCB File Offset: 0x00319DCB
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (TutorialManager.currentStep == null)
			{
				return true;
			}
			E.Chance = ((E.Weapon.Blueprint == "Bear_Bite" && !E.Inherited) ? 100 : (-100));
			return false;
		}

		// Token: 0x06008C3E RID: 35902 RVA: 0x0031BC04 File Offset: 0x00319E04
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("DefenderHit");
			Registrar.Register("GetDefenderDV");
			Registrar.Register("TakeDamage");
			Registrar.Register("DealDamage");
			Registrar.Register("AttackerGetDefenderDV");
			Registrar.Register("Regenerating");
			Registrar.Register("Healing");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008C3F RID: 35903 RVA: 0x0031BC74 File Offset: 0x00319E74
		public override bool FireEvent(Event E)
		{
			if (TutorialManager.currentStep == null)
			{
				return true;
			}
			if (E.ID == "Healing")
			{
				E.SetParameter("Amount", 0);
				return false;
			}
			if (E.ID == "Regenerating")
			{
				E.SetParameter("Amount", 0);
				return false;
			}
			if (this.BehaviorStage <= TutorialBear.BehaviorStageType.ATTACKANDMISS || this.BehaviorStage == TutorialBear.BehaviorStageType.PURSUE)
			{
				if (TutorialManager.currentStep == null || TutorialManager.currentStep.GetType() != typeof(FightBear))
				{
					return true;
				}
				if (E.ID == "GetDefenderDV" && this.BehaviorStage == TutorialBear.BehaviorStageType.ATTACKANDMISS)
				{
					int num = Stat.Random(2, 2);
					E.SetParameter("NaturalHitResult", num);
					E.SetParameter("Result", num);
					this.attackerMissed = true;
				}
				if (E.ID == "AttackerGetDefenderDV")
				{
					int num2 = Stat.Random(2, 2);
					E.SetParameter("NaturalHitResult", num2);
					E.SetParameter("Result", num2);
					this.defenderMissed = true;
				}
				if (this.attackerMissed && this.defenderMissed)
				{
					(TutorialManager.currentStep as FightBear).step = 400;
					this.BehaviorStage = TutorialBear.BehaviorStageType.ATTACKANDHIT;
				}
			}
			else if (this.BehaviorStage <= TutorialBear.BehaviorStageType.ATTACKANDHIT)
			{
				if (TutorialManager.currentStep == null || TutorialManager.currentStep.GetType() != typeof(FightBear))
				{
					return true;
				}
				if (E.ID == "GetDefenderDV")
				{
					int num3 = Stat.Random(18, 19);
					E.SetParameter("NaturalHitResult", num3);
					E.SetParameter("Result", num3);
					this.attackerHit = true;
				}
				if (E.ID == "AttackerGetDefenderDV")
				{
					int num4 = Stat.Random(18, 19);
					if (this.defenderHit)
					{
						num4 = 2;
					}
					E.SetParameter("NaturalHitResult", num4);
					E.SetParameter("Result", num4);
					this.defenderHit = true;
				}
				if (E.ID == "AttackerHit")
				{
					E.SetParameter("Penetrations", 1);
				}
				if (E.ID == "DefenderHit")
				{
					E.SetParameter("Penetrations", 0);
				}
				if (E.ID == "DealDamage")
				{
					Damage parameter = E.GetParameter<Damage>("Damage");
					if (The.Player.hitpoints <= 4)
					{
						The.Player.hitpoints = 5;
					}
					parameter.Amount = 4;
				}
				if (this.attackerHit && this.defenderHit)
				{
					(TutorialManager.currentStep as FightBear).step = 500;
					this.BehaviorStage = TutorialBear.BehaviorStageType.PURSUE;
				}
			}
			return true;
		}

		// Token: 0x06008C40 RID: 35904 RVA: 0x0031BF2C File Offset: 0x0031A12C
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (TutorialManager.currentStep == null || TutorialManager.currentStep.GetType() != typeof(FightBear))
			{
				return true;
			}
			if (E.Damage.HasAttribute("Cold"))
			{
				E.Damage.Amount = this.ParentObject.hitpoints * 2;
			}
			return true;
		}

		// Token: 0x06008C41 RID: 35905 RVA: 0x0031BF88 File Offset: 0x0031A188
		public override bool HandleEvent(AttackerDealingDamageEvent E)
		{
			if (TutorialManager.currentStep == null || TutorialManager.currentStep.GetType() != typeof(FightBear))
			{
				return true;
			}
			if (E.Damage.Amount >= The.Player.hitpoints)
			{
				E.Damage.Amount = The.Player.hitpoints - 1;
			}
			return true;
		}

		// Token: 0x06008C42 RID: 35906 RVA: 0x0031BFE8 File Offset: 0x0031A1E8
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (TutorialManager.currentStep == null || TutorialManager.currentStep.GetType() != typeof(FightBear))
			{
				return true;
			}
			(TutorialManager.currentStep as FightBear).chemCell = this.ParentObject.CurrentCell.AddObject("TutorialChemCell", null, null, null, null);
			GameObject chemCell = (TutorialManager.currentStep as FightBear).chemCell;
			if (chemCell != null)
			{
				chemCell.RequireID();
			}
			(TutorialManager.currentStep as FightBear).bearDead = true;
			this.ParentObject.GetEquippedObjects().ToList<GameObject>().ForEach(delegate(GameObject i)
			{
				i.Obliterate(null, false, null);
			});
			this.ParentObject.GetPart<Inventory>().DropOnDeath = false;
			this.ParentObject.Inventory.Clear();
			this.ParentObject.Obliterate(null, false, null);
			return true;
		}

		// Token: 0x06008C43 RID: 35907 RVA: 0x0031C0D4 File Offset: 0x0031A2D4
		public override bool HandleEvent(BeforeTakeActionEvent E)
		{
			if (this.BehaviorStage == TutorialBear.BehaviorStageType.START)
			{
				this.ParentObject.hitpoints = 2;
				this.BehaviorStage = TutorialBear.BehaviorStageType.STEPSOUTH;
			}
			if (this.BehaviorStage != TutorialBear.BehaviorStageType.STEPSOUTH)
			{
				return true;
			}
			if (!this.ParentObject.IsVisible())
			{
				return false;
			}
			this.BehaviorStage = TutorialBear.BehaviorStageType.ATTACKANDMISS;
			this.ParentObject.Brain.Target = The.Player;
			this.ParentObject.Brain.PushGoal(new Kill(The.Player, false));
			return false;
		}

		// Token: 0x04003AF7 RID: 15095
		public TutorialBear.BehaviorStageType BehaviorStage;

		// Token: 0x04003AF8 RID: 15096
		private bool attackerMissed;

		// Token: 0x04003AF9 RID: 15097
		private bool defenderMissed;

		// Token: 0x04003AFA RID: 15098
		private bool attackerHit;

		// Token: 0x04003AFB RID: 15099
		private bool defenderHit;

		// Token: 0x02001C3A RID: 7226
		public enum BehaviorStageType
		{
			// Token: 0x04006F8A RID: 28554
			START,
			// Token: 0x04006F8B RID: 28555
			STEPSOUTH = 100,
			// Token: 0x04006F8C RID: 28556
			ATTACKANDMISS = 200,
			// Token: 0x04006F8D RID: 28557
			ATTACKANDHIT = 300,
			// Token: 0x04006F8E RID: 28558
			PURSUE = 400
		}
	}
}
