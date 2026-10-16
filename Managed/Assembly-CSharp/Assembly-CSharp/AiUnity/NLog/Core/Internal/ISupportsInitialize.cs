using System;
using AiUnity.NLog.Core.Config;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000266 RID: 614
	public interface ISupportsInitialize
	{
		// Token: 0x0600163D RID: 5693
		void Initialize(LoggingConfiguration configuration);

		// Token: 0x0600163E RID: 5694
		void Close();
	}
}
