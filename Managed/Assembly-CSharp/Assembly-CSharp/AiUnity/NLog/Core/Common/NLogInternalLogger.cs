using System;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;

namespace AiUnity.NLog.Core.Common
{
	// Token: 0x020002B9 RID: 697
	public class NLogInternalLogger : InternalLogger<NLogInternalLogger>
	{
		// Token: 0x06001863 RID: 6243 RVA: 0x0007EF3C File Offset: 0x0007D13C
		static NLogInternalLogger()
		{
			Singleton<NLogInternalLogger>.Instance.Assert(true, "This log statement is executed prior to unity editor serialization due to InitializeOnLoad attribute.  The allows NLog logger to work in all phases of Unity Editor compile (ie. serialization).", Array.Empty<object>());
			Singleton<CommonInternalLogger>.Instance.Assert(true, "This log statement is executed prior to unity editor serialization due to InitializeOnLoad attribute.  The allows Common logger to work in all phases of Unity Editor compile (ie. serialization).", Array.Empty<object>());
		}
	}
}
