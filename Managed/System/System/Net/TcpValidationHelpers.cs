using System;

namespace System.Net
{
	// Token: 0x02000571 RID: 1393
	internal static class TcpValidationHelpers
	{
		// Token: 0x06002CFC RID: 11516 RVA: 0x0009A1A7 File Offset: 0x000983A7
		public static bool ValidatePortNumber(int port)
		{
			return port >= 0 && port <= 65535;
		}
	}
}
