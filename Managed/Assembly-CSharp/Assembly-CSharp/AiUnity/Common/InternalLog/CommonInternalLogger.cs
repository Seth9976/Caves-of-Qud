using System;
using AiUnity.Common.Patterns;

namespace AiUnity.Common.InternalLog
{
	// Token: 0x020002D8 RID: 728
	public class CommonInternalLogger : InternalLogger<CommonInternalLogger>
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x00080401 File Offset: 0x0007E601
		static CommonInternalLogger()
		{
			Singleton<CommonInternalLogger>.Instance.Assert(true, "This log statement is executed prior to unity editor serialization due to InitializeOnLoad attribute.  The allows Common logger to work in all phases of Unity Editor compile (ie. serialization).", Array.Empty<object>());
		}
	}
}
