using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	public class ConditionParseException : Exception
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x0007D77B File Offset: 0x0007B97B
		public ConditionParseException()
		{
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0007D783 File Offset: 0x0007B983
		public ConditionParseException(string message)
			: base(message)
		{
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0007D78C File Offset: 0x0007B98C
		public ConditionParseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
