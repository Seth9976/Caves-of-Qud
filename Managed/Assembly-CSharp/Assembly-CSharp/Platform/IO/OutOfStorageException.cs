using System;

namespace Platform.IO
{
	// Token: 0x02000339 RID: 825
	public class OutOfStorageException : Exception
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x0009870D File Offset: 0x0009690D
		public OutOfStorageException(string message)
			: base(message)
		{
		}
	}
}
