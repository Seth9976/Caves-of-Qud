using System;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000069 RID: 105
	public interface ILogger
	{
		// Token: 0x0600038D RID: 909
		void Error(string message);

		// Token: 0x0600038E RID: 910
		void Error(string message, params object[] args);

		// Token: 0x0600038F RID: 911
		void Warning(string message);

		// Token: 0x06000390 RID: 912
		void Warning(string message, params object[] args);

		// Token: 0x06000391 RID: 913
		void Info(string message);

		// Token: 0x06000392 RID: 914
		void Info(string message, params object[] args);

		// Token: 0x06000393 RID: 915
		void Debug(string message);

		// Token: 0x06000394 RID: 916
		void Debug(string message, params object[] args);
	}
}
