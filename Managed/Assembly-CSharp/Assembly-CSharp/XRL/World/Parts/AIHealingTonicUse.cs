using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E1F RID: 3615
	[Serializable]
	public class AIHealingTonicUse : AIBehaviorPart
	{
		// Token: 0x0600932C RID: 37676 RVA: 0x00350845 File Offset: 0x0034EA45
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600932D RID: 37677 RVA: 0x00350848 File Offset: 0x0034EA48
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID || ID == AIGetPassiveItemListEvent.ID;
		}

		// Token: 0x0600932E RID: 37678 RVA: 0x00350866 File Offset: 0x0034EA66
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			this.AIUseHealingTonic(E);
			return base.HandleEvent(E);
		}

		// Token: 0x0600932F RID: 37679 RVA: 0x00350876 File Offset: 0x0034EA76
		public override bool HandleEvent(AIGetPassiveItemListEvent E)
		{
			this.AIUseHealingTonic(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009330 RID: 37680 RVA: 0x00350888 File Offset: 0x0034EA88
		public void AIUseHealingTonic(IAICommandListEvent E)
		{
			if (!this.ParentObject.IsBroken() && !this.ParentObject.IsRusted() && !this.ParentObject.IsImportant() && E.Actor.FireEvent("CanApplyTonic") && E.Actor.GetTonicEffectCount() < E.Actor.GetTonicCapacity() && GetUtilityScoreEvent.GetFor(E.Actor, this.ParentObject, null, false) > 0)
			{
				E.Add("Apply", 100, this.ParentObject, true, true, null, null);
			}
		}
	}
}
