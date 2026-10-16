using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E2F RID: 3631
	[Serializable]
	public class AITonicUse : AIBehaviorPart
	{
		// Token: 0x06009375 RID: 37749 RVA: 0x003519BB File Offset: 0x0034FBBB
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009376 RID: 37750 RVA: 0x003519BE File Offset: 0x0034FBBE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID;
		}

		// Token: 0x06009377 RID: 37751 RVA: 0x003519D4 File Offset: 0x0034FBD4
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			this.AIUseTonic(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009378 RID: 37752 RVA: 0x003519E4 File Offset: 0x0034FBE4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009379 RID: 37753 RVA: 0x003519E8 File Offset: 0x0034FBE8
		public void AIUseTonic(IAICommandListEvent E)
		{
			if (!this.ParentObject.IsBroken() && !this.ParentObject.IsRusted() && !this.ParentObject.IsImportant() && E.Actor.FireEvent("CanApplyTonic") && E.Actor.GetTonicEffectCount() < E.Actor.GetTonicCapacity())
			{
				int @for = GetUtilityScoreEvent.GetFor(E.Actor, this.ParentObject, null, false);
				if (@for > 0)
				{
					E.Add("Apply", Math.Max(@for / 10, 1), this.ParentObject, true, true, null, null);
				}
			}
		}
	}
}
