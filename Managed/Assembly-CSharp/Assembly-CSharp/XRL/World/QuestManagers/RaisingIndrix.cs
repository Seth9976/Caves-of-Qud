using System;

namespace XRL.World.QuestManagers
{
	// Token: 0x020009BB RID: 2491
	[Serializable]
	public class RaisingIndrix : QuestManager
	{
		// Token: 0x06006B20 RID: 27424 RVA: 0x00227C21 File Offset: 0x00225E21
		public override void OnQuestAdded()
		{
		}

		// Token: 0x06006B21 RID: 27425 RVA: 0x00227C23 File Offset: 0x00225E23
		public override void OnQuestComplete()
		{
		}

		// Token: 0x06006B22 RID: 27426 RVA: 0x00227C25 File Offset: 0x00225E25
		public override GameObject GetQuestInfluencer()
		{
			return GameObject.FindByBlueprint("Warden Indrix");
		}
	}
}
