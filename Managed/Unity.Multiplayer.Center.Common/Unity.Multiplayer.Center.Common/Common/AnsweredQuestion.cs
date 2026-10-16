using System;
using System.Collections.Generic;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class AnsweredQuestion
	{
		// Token: 0x0400000C RID: 12
		public string QuestionId;

		// Token: 0x0400000D RID: 13
		public List<string> Answers;
	}
}
