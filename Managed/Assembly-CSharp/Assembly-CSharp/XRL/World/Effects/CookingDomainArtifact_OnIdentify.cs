using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C57 RID: 3159
	public class CookingDomainArtifact_OnIdentify : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060080B8 RID: 32952 RVA: 0x002F4FFB File Offset: 0x002F31FB
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature identify an artifact,";
		}

		// Token: 0x060080B9 RID: 32953 RVA: 0x002F5002 File Offset: 0x002F3202
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<ExamineSuccessEvent>.ID;
		}

		// Token: 0x060080BA RID: 32954 RVA: 0x002F5018 File Offset: 0x002F3218
		public override bool HandleEvent(ExamineSuccessEvent E)
		{
			if (E.Complete)
			{
				base.Trigger();
			}
			return base.HandleEvent(E);
		}
	}
}
