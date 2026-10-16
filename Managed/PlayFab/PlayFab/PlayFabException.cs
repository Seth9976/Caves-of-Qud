using System;

namespace PlayFab
{
	// Token: 0x0200001C RID: 28
	public class PlayFabException : Exception
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000F924 File Offset: 0x0000DB24
		public PlayFabException(PlayFabExceptionCode code, string message)
			: base(message)
		{
			this.Code = code;
		}

		// Token: 0x0400027C RID: 636
		public readonly PlayFabExceptionCode Code;
	}
}
