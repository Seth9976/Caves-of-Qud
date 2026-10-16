using System;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001046 RID: 4166
	[Serializable]
	public class GreatMachineGauge : IPart
	{
		// Token: 0x0600A5D5 RID: 42453 RVA: 0x003CAEE4 File Offset: 0x003C90E4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			GameObject equipped = Object.Equipped;
			if (GameObject.Validate(equipped))
			{
				Registrar.Register(equipped, PooledEvent<GenericCommandEvent>.ID, 0, false);
				Registrar.Register(equipped, AfterMentalAttackEvent.ID, 0, false);
			}
		}

		// Token: 0x0600A5D6 RID: 42454 RVA: 0x003CAF1C File Offset: 0x003C911C
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A5D7 RID: 42455 RVA: 0x003CAF30 File Offset: 0x003C9130
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, PooledEvent<GenericCommandEvent>.ID, 0, false);
			E.Actor.RegisterEvent(this, AfterMentalAttackEvent.ID, 0, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5D8 RID: 42456 RVA: 0x003CAF5F File Offset: 0x003C915F
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, PooledEvent<GenericCommandEvent>.ID);
			E.Actor.UnregisterEvent(this, AfterMentalAttackEvent.ID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5D9 RID: 42457 RVA: 0x003CAF8C File Offset: 0x003C918C
		public override bool HandleEvent(GenericCommandEvent E)
		{
			if (E.Command == Persuasion_Berate.COMMAND_ATTACK)
			{
				GameObject gameObject = E.Object as GameObject;
				if (gameObject != null)
				{
					this.Confuse(gameObject);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5DA RID: 42458 RVA: 0x003CAFC8 File Offset: 0x003C91C8
		public override bool HandleEvent(AfterMentalAttackEvent E)
		{
			if (E.Penetrations > 0 && (E.Command == Persuasion_MenacingStare.COMMAND_ATTACK || E.Command == Persuasion_Intimidate.COMMAND_ATTACK))
			{
				this.Confuse(E.Defender);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5DB RID: 42459 RVA: 0x003CB018 File Offset: 0x003C9218
		public void Confuse(GameObject Object)
		{
			GameObject gameObject = this.ParentObject.EquippedProperlyBy();
			if (!GameObject.Validate(gameObject))
			{
				return;
			}
			int level = Confusion.GetConfusionLevel(10);
			int penalty = Confusion.GetMentalPenalty(10);
			base.PerformMentalAttack((MentalAttackEvent MAE) => Confusion.Confuse(MAE, true, level, penalty, false), gameObject, Object, this.ParentObject, "Confuse GreatMachineGauge", "1d8", 8388610, Confusion.GetDuration(10).RollCached(), int.MinValue, Math.Max(gameObject.StatMod("Ego", 0), 10), 0);
		}

		// Token: 0x0400447B RID: 17531
		public const int CONFUSION_LEVEL = 10;
	}
}
