using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Bindings
{
	// Token: 0x0200036C RID: 876
	[VisibleToOtherModules]
	internal static class ExceptionMarshaller
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x0004EBD8 File Offset: 0x0004CDD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CheckPendingException()
		{
			Exception ex = ExceptionMarshaller.s_pendingException;
			bool flag = ex != null;
			if (flag)
			{
				ExceptionMarshaller.s_pendingException = null;
				throw ex;
			}
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0004EBFD File Offset: 0x0004CDFD
		[RequiredByNativeCode]
		private static void SetPendingException(Exception ex)
		{
			ExceptionMarshaller.s_pendingException = ex;
		}

		// Token: 0x04000B8E RID: 2958
		[ThreadStatic]
		private static Exception s_pendingException;
	}
}
