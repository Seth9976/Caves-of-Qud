using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E2B RID: 3627
	[Serializable]
	public class AIShopper : AIBehaviorPart
	{
		// Token: 0x06009363 RID: 37731 RVA: 0x0035165F File Offset: 0x0034F85F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID;
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x00351675 File Offset: 0x0034F875
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (this.ShouldGoShopping() && 25.in100())
			{
				this.ParentObject.Brain.PushGoal(new GoOnAShoppingSpree());
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009365 RID: 37733 RVA: 0x003516A8 File Offset: 0x0034F8A8
		public bool ShouldGoShopping()
		{
			return this.ParentObject.PartyLeader == null && (!this.ParentObject.HasProperName || (this.ParentObject.CurrentZone != null && GoOnAShoppingSpree.TargetZones.Contains(this.ParentObject.CurrentZone.ZoneID))) && this.ParentObject.FireEvent("CanAIDoIndependentBehavior");
		}
	}
}
