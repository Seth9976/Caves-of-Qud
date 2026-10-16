using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Purchasing.Stores.Util
{
	// Token: 0x02000095 RID: 149
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	internal interface IRetryPolicy
	{
		// Token: 0x0600036C RID: 876
		void Invoke(Action<Action> actionToTry, [global::System.Runtime.CompilerServices.Nullable(2)] Action onRetryAction = null);
	}
}
