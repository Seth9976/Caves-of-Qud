using System;
using System.Threading;
using AiUnity.Common.Log;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x02000262 RID: 610
	internal static class ExceptionHelper
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x00077FC0 File Offset: 0x000761C0
		public static bool MustBeRethrown(this Exception exception)
		{
			return exception is AssertException || exception is StackOverflowException || exception is ThreadAbortException || exception is OutOfMemoryException || exception is NLogConfigurationException || exception.GetType().IsSubclassOf(typeof(NLogConfigurationException));
		}
	}
}
