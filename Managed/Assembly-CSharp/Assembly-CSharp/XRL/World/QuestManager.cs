using System;

namespace XRL.World
{
	// Token: 0x02000961 RID: 2401
	[Serializable]
	public class QuestManager : IPart
	{
		// Token: 0x0600630A RID: 25354 RVA: 0x001E7122 File Offset: 0x001E5322
		public virtual void AfterQuestAdded()
		{
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x001E7124 File Offset: 0x001E5324
		public virtual void OnQuestAdded()
		{
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x001E7126 File Offset: 0x001E5326
		public virtual void OnStepComplete(string StepName)
		{
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x001E7128 File Offset: 0x001E5328
		public virtual void OnQuestComplete()
		{
		}

		// Token: 0x0600630E RID: 25358 RVA: 0x001E712A File Offset: 0x001E532A
		public virtual GameObject GetQuestInfluencer()
		{
			return null;
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x001E712D File Offset: 0x001E532D
		public virtual string GetQuestZoneID()
		{
			return null;
		}

		// Token: 0x04002E92 RID: 11922
		public string MyQuestID;
	}
}
