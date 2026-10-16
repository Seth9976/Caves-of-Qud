using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001D1 RID: 465
	internal class InvalidRuleSetException : Exception
	{
		// Token: 0x0600137C RID: 4988 RVA: 0x0003CD48 File Offset: 0x0003AF48
		public InvalidRuleSetException()
		{
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0003CD50 File Offset: 0x0003AF50
		public InvalidRuleSetException(string message)
			: base(message)
		{
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0003CD59 File Offset: 0x0003AF59
		public InvalidRuleSetException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
