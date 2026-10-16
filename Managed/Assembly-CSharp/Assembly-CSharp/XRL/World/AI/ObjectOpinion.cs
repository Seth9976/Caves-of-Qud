using System;

namespace XRL.World.AI
{
	// Token: 0x02000B6A RID: 2922
	[Serializable]
	public class ObjectOpinion
	{
		// Token: 0x06007532 RID: 30002 RVA: 0x002BF2E0 File Offset: 0x002BD4E0
		public ObjectOpinion(int Feeling)
		{
			this.Disposition = Feeling;
		}

		// Token: 0x06007533 RID: 30003 RVA: 0x002BF2EF File Offset: 0x002BD4EF
		public ObjectOpinion(ObjectOpinion src)
		{
			this.Disposition = src.Disposition;
		}

		// Token: 0x04003595 RID: 13717
		public int Disposition;
	}
}
