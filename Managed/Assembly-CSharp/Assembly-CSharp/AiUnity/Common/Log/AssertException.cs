using System;

namespace AiUnity.Common.Log
{
	// Token: 0x020002D1 RID: 721
	public class AssertException : Exception
	{
		// Token: 0x060018E7 RID: 6375 RVA: 0x000803F7 File Offset: 0x0007E5F7
		public AssertException(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}
	}
}
