using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000046 RID: 70
	internal static class AwaiterActions
	{
		// Token: 0x0600018D RID: 397 RVA: 0x00006923 File Offset: 0x00004B23
		[DebuggerHidden]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Continuation(object state)
		{
			((Action)state)();
		}

		// Token: 0x0400009A RID: 154
		internal static readonly Action<object> InvokeContinuationDelegate = new Action<object>(AwaiterActions.Continuation);
	}
}
