using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D6 RID: 5078
	[Serializable]
	public class Narcolepsy : BaseMutation
	{
		// Token: 0x0600C43A RID: 50234 RVA: 0x00477B1A File Offset: 0x00475D1A
		public Narcolepsy()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C43B RID: 50235 RVA: 0x00477B2D File Offset: 0x00475D2D
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C43C RID: 50236 RVA: 0x00477B30 File Offset: 0x00475D30
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600C43D RID: 50237 RVA: 0x00477B48 File Offset: 0x00475D48
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!this.ParentObject.OnWorldMap() && !this.ParentObject.HasEffect<Asleep>() && 2.in1000() && ((!this.ParentObject.IsPlayer() && !this.ParentObject.WasPlayer()) || this.ParentObject.IsInCombat()))
			{
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_physicalDefect_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (this.ParentObject.ForceApplyEffect(new Asleep(Stat.Random(20, 29), true, false, false, true), null))
				{
					this.ParentObject.StopMoving();
					base.DidX("fall", "asleep", "!", null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C43E RID: 50238 RVA: 0x00477C39 File Offset: 0x00475E39
		public override string GetDescription()
		{
			return "You fall asleep involuntarily from time to time.\n\nSmall chance each round you're in combat that you fall asleep for 20-29 rounds.";
		}

		// Token: 0x0600C43F RID: 50239 RVA: 0x00477C40 File Offset: 0x00475E40
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C440 RID: 50240 RVA: 0x00477C47 File Offset: 0x00475E47
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C441 RID: 50241 RVA: 0x00477C4A File Offset: 0x00475E4A
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}
	}
}
