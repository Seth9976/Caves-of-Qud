using System;

namespace Steamworks
{
	// Token: 0x0200014E RID: 334
	public enum EGameSearchErrorCode_t
	{
		// Token: 0x04000928 RID: 2344
		k_EGameSearchErrorCode_OK = 1,
		// Token: 0x04000929 RID: 2345
		k_EGameSearchErrorCode_Failed_Search_Already_In_Progress,
		// Token: 0x0400092A RID: 2346
		k_EGameSearchErrorCode_Failed_No_Search_In_Progress,
		// Token: 0x0400092B RID: 2347
		k_EGameSearchErrorCode_Failed_Not_Lobby_Leader,
		// Token: 0x0400092C RID: 2348
		k_EGameSearchErrorCode_Failed_No_Host_Available,
		// Token: 0x0400092D RID: 2349
		k_EGameSearchErrorCode_Failed_Search_Params_Invalid,
		// Token: 0x0400092E RID: 2350
		k_EGameSearchErrorCode_Failed_Offline,
		// Token: 0x0400092F RID: 2351
		k_EGameSearchErrorCode_Failed_NotAuthorized,
		// Token: 0x04000930 RID: 2352
		k_EGameSearchErrorCode_Failed_Unknown_Error
	}
}
