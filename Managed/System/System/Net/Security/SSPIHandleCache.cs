using System;
using System.Threading;

namespace System.Net.Security
{
	// Token: 0x02000850 RID: 2128
	internal static class SSPIHandleCache
	{
		// Token: 0x060043A0 RID: 17312 RVA: 0x000EBDEC File Offset: 0x000E9FEC
		internal static void CacheCredential(SafeFreeCredentials newHandle)
		{
			try
			{
				SafeCredentialReference safeCredentialReference = SafeCredentialReference.CreateReference(newHandle);
				if (safeCredentialReference != null)
				{
					int num = Interlocked.Increment(ref SSPIHandleCache.s_current) & 31;
					safeCredentialReference = Interlocked.Exchange<SafeCredentialReference>(ref SSPIHandleCache.s_cacheSlots[num], safeCredentialReference);
					if (safeCredentialReference != null)
					{
						safeCredentialReference.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionCheck.IsFatal(ex))
				{
					NetEventSource.Fail(null, "Attempted to throw: {e}", "CacheCredential");
				}
			}
		}

		// Token: 0x040028F2 RID: 10482
		private const int c_MaxCacheSize = 31;

		// Token: 0x040028F3 RID: 10483
		private static SafeCredentialReference[] s_cacheSlots = new SafeCredentialReference[32];

		// Token: 0x040028F4 RID: 10484
		private static int s_current = -1;
	}
}
