using System;

namespace System.Net
{
	// Token: 0x02000565 RID: 1381
	internal static class ExceptionCheck
	{
		// Token: 0x06002CA5 RID: 11429 RVA: 0x00098352 File Offset: 0x00096552
		internal static bool IsFatal(Exception exception)
		{
			return exception is OutOfMemoryException;
		}
	}
}
