using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200002F RID: 47
	internal static class LoggerExtensions
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00003A6B File Offset: 0x00001C6B
		public static void LogIAPWarning(this ILogger logger, string message)
		{
			logger.LogWarning("Unity IAP", message);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003A79 File Offset: 0x00001C79
		public static void LogIAPError(this ILogger logger, string message)
		{
			logger.LogError("Unity IAP", message);
		}

		// Token: 0x0400006B RID: 107
		private const string IAPLogTag = "Unity IAP";
	}
}
