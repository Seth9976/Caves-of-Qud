using System;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B78 RID: 2936
	[Serializable]
	public class TombPatrolGoal : GoalHandler
	{
		// Token: 0x06007599 RID: 30105 RVA: 0x002C209B File Offset: 0x002C029B
		public TombPatrolGoal()
		{
		}

		// Token: 0x0600759A RID: 30106 RVA: 0x002C20AE File Offset: 0x002C02AE
		public TombPatrolGoal(string ZoneID)
			: this()
		{
			this.TargetZoneID = ZoneID;
		}

		// Token: 0x0600759B RID: 30107 RVA: 0x002C20BD File Offset: 0x002C02BD
		public TombPatrolGoal(GlobalLocation loc)
			: this(loc.ZoneID)
		{
		}

		// Token: 0x0600759C RID: 30108 RVA: 0x002C20CB File Offset: 0x002C02CB
		public override bool Finished()
		{
			return base.ParentObject.InZone(this.TargetZoneID);
		}

		// Token: 0x0600759D RID: 30109 RVA: 0x002C20DE File Offset: 0x002C02DE
		public override void Create()
		{
		}

		// Token: 0x0600759E RID: 30110 RVA: 0x002C20E0 File Offset: 0x002C02E0
		public void MoveToTargetZone()
		{
		}

		// Token: 0x0600759F RID: 30111 RVA: 0x002C20E2 File Offset: 0x002C02E2
		public override void TakeAction()
		{
			this.ParentBrain.PushGoal(new MoveToZone(this.TargetZoneID, false, -1, 95));
		}

		// Token: 0x040035D6 RID: 13782
		public string TargetZoneID = "JoppaWorld.5.2.1.1.10";
	}
}
