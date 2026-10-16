using System;
using System.Security.Cryptography;
using System.Web.Util;

namespace System.Web.SessionState
{
	// Token: 0x02000497 RID: 1175
	internal class SessionId
	{
		// Token: 0x06003553 RID: 13651 RVA: 0x0008947C File Offset: 0x0008767C
		internal static string Create()
		{
			byte[] array = new byte[12];
			RandomNumberGenerator randomNumberGenerator = SessionId.rng;
			lock (randomNumberGenerator)
			{
				SessionId.rng.GetBytes(array);
			}
			return MachineKeySectionUtils.GetHexString(array);
		}

		// Token: 0x04001D52 RID: 7506
		internal const int IdLength = 24;

		// Token: 0x04001D53 RID: 7507
		private const int half_len = 12;

		// Token: 0x04001D54 RID: 7508
		private static RandomNumberGenerator rng = RandomNumberGenerator.Create();
	}
}
