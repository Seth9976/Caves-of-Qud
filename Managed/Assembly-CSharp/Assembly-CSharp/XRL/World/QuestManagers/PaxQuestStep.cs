using System;

namespace XRL.World.QuestManagers
{
	// Token: 0x020009BC RID: 2492
	[Serializable]
	public class PaxQuestStep : IComposite
	{
		// Token: 0x0400310B RID: 12555
		public string Name;

		// Token: 0x0400310C RID: 12556
		public string Text;

		// Token: 0x0400310D RID: 12557
		public string Target;

		// Token: 0x0400310E RID: 12558
		public bool Finished;
	}
}
