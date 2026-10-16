using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x0200029F RID: 671
	[Serializable]
	public class ConditionEvaluationException : Exception
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x0007D1C7 File Offset: 0x0007B3C7
		public ConditionEvaluationException()
		{
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0007D1CF File Offset: 0x0007B3CF
		public ConditionEvaluationException(string message)
			: base(message)
		{
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0007D1D8 File Offset: 0x0007B3D8
		public ConditionEvaluationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
