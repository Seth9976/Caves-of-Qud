using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000133 RID: 307
	internal static class ExceptionHelpers
	{
		// Token: 0x06001124 RID: 4388 RVA: 0x000522CC File Offset: 0x000504CC
		public static bool IsExceptionIndicatingBugInCode(this Exception exception)
		{
			return exception is NullReferenceException || exception is IndexOutOfRangeException || exception is ArgumentException;
		}
	}
}
