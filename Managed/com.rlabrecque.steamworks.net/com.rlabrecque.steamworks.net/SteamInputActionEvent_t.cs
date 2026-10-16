using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public struct SteamInputActionEvent_t
	{
		// Token: 0x04000AD3 RID: 2771
		public InputHandle_t controllerHandle;

		// Token: 0x04000AD4 RID: 2772
		public ESteamInputActionEventType eEventType;

		// Token: 0x04000AD5 RID: 2773
		public SteamInputActionEvent_t.OptionValue m_val;

		// Token: 0x020001F4 RID: 500
		[Serializable]
		public struct AnalogAction_t
		{
			// Token: 0x04000B68 RID: 2920
			public InputAnalogActionHandle_t actionHandle;

			// Token: 0x04000B69 RID: 2921
			public InputAnalogActionData_t analogActionData;
		}

		// Token: 0x020001F5 RID: 501
		[Serializable]
		public struct DigitalAction_t
		{
			// Token: 0x04000B6A RID: 2922
			public InputDigitalActionHandle_t actionHandle;

			// Token: 0x04000B6B RID: 2923
			public InputDigitalActionData_t digitalActionData;
		}

		// Token: 0x020001F6 RID: 502
		[Serializable]
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			// Token: 0x04000B6C RID: 2924
			[FieldOffset(0)]
			public SteamInputActionEvent_t.AnalogAction_t analogAction;

			// Token: 0x04000B6D RID: 2925
			[FieldOffset(0)]
			public SteamInputActionEvent_t.DigitalAction_t digitalAction;
		}
	}
}
