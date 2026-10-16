using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Multiplayer.Center.Common
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class AnswerData
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BF File Offset: 0x000002BF
		public AnswerData Clone()
		{
			return JsonUtility.FromJson(JsonUtility.ToJson(this), typeof(AnswerData)) as AnswerData;
		}

		// Token: 0x0400000B RID: 11
		public List<AnsweredQuestion> Answers = new List<AnsweredQuestion>();
	}
}
