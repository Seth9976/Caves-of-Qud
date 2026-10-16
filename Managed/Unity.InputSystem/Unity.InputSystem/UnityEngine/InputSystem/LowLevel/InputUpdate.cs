using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F5 RID: 245
	internal static class InputUpdate
	{
		// Token: 0x06000E8C RID: 3724 RVA: 0x00047D61 File Offset: 0x00045F61
		internal static void OnBeforeUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnBeforeUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00047D8D File Offset: 0x00045F8D
		internal static void OnUpdate(InputUpdateType type)
		{
			InputUpdate.s_LatestUpdateType = type;
			if (type - InputUpdateType.Dynamic <= 1 || type == InputUpdateType.Manual)
			{
				InputUpdate.s_PlayerUpdateStepCount.OnUpdate();
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00047DBC File Offset: 0x00045FBC
		public static InputUpdate.SerializedState Save()
		{
			return new InputUpdate.SerializedState
			{
				lastUpdateType = InputUpdate.s_LatestUpdateType,
				playerUpdateStepCount = InputUpdate.s_PlayerUpdateStepCount
			};
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00047DEC File Offset: 0x00045FEC
		public static void Restore(InputUpdate.SerializedState state)
		{
			InputUpdate.s_LatestUpdateType = state.lastUpdateType;
			InputUpdate.s_PlayerUpdateStepCount = state.playerUpdateStepCount;
			InputUpdateType inputUpdateType = InputUpdate.s_LatestUpdateType;
			if (inputUpdateType - InputUpdateType.Dynamic <= 1 || inputUpdateType == InputUpdateType.Manual)
			{
				InputUpdate.s_UpdateStepCount = InputUpdate.s_PlayerUpdateStepCount.value;
				return;
			}
			InputUpdate.s_UpdateStepCount = 0U;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00047E36 File Offset: 0x00046036
		public static InputUpdateType GetUpdateTypeForPlayer(this InputUpdateType mask)
		{
			if ((mask & InputUpdateType.Manual) != InputUpdateType.None)
			{
				return InputUpdateType.Manual;
			}
			if ((mask & InputUpdateType.Dynamic) != InputUpdateType.None)
			{
				return InputUpdateType.Dynamic;
			}
			if ((mask & InputUpdateType.Fixed) != InputUpdateType.None)
			{
				return InputUpdateType.Fixed;
			}
			return InputUpdateType.None;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00047E50 File Offset: 0x00046050
		public static bool IsPlayerUpdate(this InputUpdateType updateType)
		{
			return updateType != InputUpdateType.Editor && updateType > InputUpdateType.None;
		}

		// Token: 0x04000601 RID: 1537
		public static uint s_UpdateStepCount;

		// Token: 0x04000602 RID: 1538
		public static InputUpdateType s_LatestUpdateType;

		// Token: 0x04000603 RID: 1539
		public static InputUpdate.UpdateStepCount s_PlayerUpdateStepCount;

		// Token: 0x0200021E RID: 542
		[Serializable]
		public struct UpdateStepCount
		{
			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x06001546 RID: 5446 RVA: 0x000634BC File Offset: 0x000616BC
			// (set) Token: 0x06001547 RID: 5447 RVA: 0x000634C4 File Offset: 0x000616C4
			public uint value { readonly get; private set; }

			// Token: 0x06001548 RID: 5448 RVA: 0x000634D0 File Offset: 0x000616D0
			public void OnBeforeUpdate()
			{
				this.m_WasUpdated = true;
				uint value = this.value;
				this.value = value + 1U;
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x000634F4 File Offset: 0x000616F4
			public void OnUpdate()
			{
				if (!this.m_WasUpdated)
				{
					uint value = this.value;
					this.value = value + 1U;
				}
				this.m_WasUpdated = false;
			}

			// Token: 0x04000BC5 RID: 3013
			private bool m_WasUpdated;
		}

		// Token: 0x0200021F RID: 543
		[Serializable]
		public struct SerializedState
		{
			// Token: 0x04000BC7 RID: 3015
			public InputUpdateType lastUpdateType;

			// Token: 0x04000BC8 RID: 3016
			public InputUpdate.UpdateStepCount playerUpdateStepCount;
		}
	}
}
