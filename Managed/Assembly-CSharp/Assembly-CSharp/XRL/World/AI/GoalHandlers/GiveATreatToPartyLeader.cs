using System;
using XRL.World.Capabilities;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000BA0 RID: 2976
	[Serializable]
	public class GiveATreatToPartyLeader : GoalHandler
	{
		// Token: 0x060076A0 RID: 30368 RVA: 0x002C9679 File Offset: 0x002C7879
		public GiveATreatToPartyLeader()
		{
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x002C9681 File Offset: 0x002C7881
		public GiveATreatToPartyLeader(string treatTable)
			: this()
		{
			this.treatTable = treatTable;
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x002C9690 File Offset: 0x002C7890
		public override bool Finished()
		{
			return false;
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x002C9693 File Offset: 0x002C7893
		public override void Create()
		{
		}

		// Token: 0x060076A4 RID: 30372 RVA: 0x002C9698 File Offset: 0x002C7898
		public override void TakeAction()
		{
			if (!base.ParentObject.IsPlayerLed())
			{
				base.Pop();
				return;
			}
			if (base.ParentObject.DistanceTo(The.Player) <= 1)
			{
				GameObject gameObject = GameObject.Create(PopulationManager.RollOneFrom(this.treatTable, null, null).Blueprint);
				if (gameObject != null)
				{
					Messaging.XDidYToZ(base.ParentObject, "give", null, The.Player, "a treat", null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					The.Player.ReceiveObject(gameObject, false, null);
				}
				base.Pop();
				return;
			}
			this.ParentBrain.PushGoal(new MoveTo(The.Player, false, false, 1, false, false, false, -1, -1, 95));
		}

		// Token: 0x04003658 RID: 13912
		private string treatTable;
	}
}
