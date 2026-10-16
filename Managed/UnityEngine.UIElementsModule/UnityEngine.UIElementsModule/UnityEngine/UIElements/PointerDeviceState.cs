using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000283 RID: 643
	internal static class PointerDeviceState
	{
		// Token: 0x06001291 RID: 4753 RVA: 0x0004C888 File Offset: 0x0004AA88
		internal static void Reset()
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				PointerDeviceState.s_PlayerPointerLocations[i].SetLocation(Vector2.zero, null);
				PointerDeviceState.s_PressedButtons[i] = 0;
				PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] = null;
			}
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0004C8D4 File Offset: 0x0004AAD4
		internal static void RemovePanelData(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				bool flag = PointerDeviceState.s_PlayerPointerLocations[i].Panel == panel;
				if (flag)
				{
					PointerDeviceState.s_PlayerPointerLocations[i].SetLocation(Vector2.zero, null);
				}
				bool flag2 = PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] == panel;
				if (flag2)
				{
					PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[i] = null;
				}
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0004C93C File Offset: 0x0004AB3C
		public static void SavePointerPosition(int pointerId, Vector2 position, IPanel panel, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			PointerDeviceState.s_PlayerPointerLocations[pointerId].SetLocation(position, panel);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0004C969 File Offset: 0x0004AB69
		public static void PressButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0, "PressButton expects buttonId >= 0");
			Debug.Assert(buttonId < 32, "PressButton expects buttonId < 32");
			PointerDeviceState.s_PressedButtons[pointerId] |= 1 << buttonId;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004C9A3 File Offset: 0x0004ABA3
		public static void ReleaseButton(int pointerId, int buttonId)
		{
			Debug.Assert(buttonId >= 0, "ReleaseButton expects buttonId >= 0");
			Debug.Assert(buttonId < 32, "ReleaseButton expects buttonId < 32");
			PointerDeviceState.s_PressedButtons[pointerId] &= ~(1 << buttonId);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004C9DE File Offset: 0x0004ABDE
		public static void ReleaseAllButtons(int pointerId)
		{
			PointerDeviceState.s_PressedButtons[pointerId] = 0;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004C9EC File Offset: 0x0004ABEC
		public static Vector2 GetPointerPosition(int pointerId, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.s_PlayerPointerLocations[pointerId].Position;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004CA18 File Offset: 0x0004AC18
		public static IPanel GetPanel(int pointerId, ContextType contextType)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.s_PlayerPointerLocations[pointerId].Panel;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004CA44 File Offset: 0x0004AC44
		private static bool HasFlagFast(PointerDeviceState.LocationFlag flagSet, PointerDeviceState.LocationFlag flag)
		{
			return (flagSet & flag) == flag;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004CA5C File Offset: 0x0004AC5C
		public static bool HasLocationFlag(int pointerId, ContextType contextType, PointerDeviceState.LocationFlag flag)
		{
			if (contextType > ContextType.Editor)
			{
			}
			return PointerDeviceState.HasFlagFast(PointerDeviceState.s_PlayerPointerLocations[pointerId].Flags, flag);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004CA90 File Offset: 0x0004AC90
		public static int GetPressedButtons(int pointerId)
		{
			return PointerDeviceState.s_PressedButtons[pointerId];
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004CAAC File Offset: 0x0004ACAC
		internal static bool HasAdditionalPressedButtons(int pointerId, int exceptButtonId)
		{
			return (PointerDeviceState.s_PressedButtons[pointerId] & ~(1 << exceptButtonId)) != 0;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004CAD0 File Offset: 0x0004ACD0
		internal static void SetPlayerPanelWithSoftPointerCapture(int pointerId, IPanel panel)
		{
			PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[pointerId] = panel;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004CADC File Offset: 0x0004ACDC
		internal static IPanel GetPlayerPanelWithSoftPointerCapture(int pointerId)
		{
			return PointerDeviceState.s_PlayerPanelWithSoftPointerCapture[pointerId];
		}

		// Token: 0x04000A6D RID: 2669
		private static PointerDeviceState.PointerLocation[] s_PlayerPointerLocations = new PointerDeviceState.PointerLocation[PointerId.maxPointers];

		// Token: 0x04000A6E RID: 2670
		private static int[] s_PressedButtons = new int[PointerId.maxPointers];

		// Token: 0x04000A6F RID: 2671
		private static readonly IPanel[] s_PlayerPanelWithSoftPointerCapture = new IPanel[PointerId.maxPointers];

		// Token: 0x02000284 RID: 644
		[Flags]
		internal enum LocationFlag
		{
			// Token: 0x04000A71 RID: 2673
			None = 0,
			// Token: 0x04000A72 RID: 2674
			OutsidePanel = 1
		}

		// Token: 0x02000285 RID: 645
		private struct PointerLocation
		{
			// Token: 0x1700035C RID: 860
			// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004CB24 File Offset: 0x0004AD24
			// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0004CB2C File Offset: 0x0004AD2C
			internal Vector2 Position { readonly get; private set; }

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0004CB35 File Offset: 0x0004AD35
			// (set) Token: 0x060012A3 RID: 4771 RVA: 0x0004CB3D File Offset: 0x0004AD3D
			internal IPanel Panel { readonly get; private set; }

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004CB46 File Offset: 0x0004AD46
			// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0004CB4E File Offset: 0x0004AD4E
			internal PointerDeviceState.LocationFlag Flags { readonly get; private set; }

			// Token: 0x060012A6 RID: 4774 RVA: 0x0004CB58 File Offset: 0x0004AD58
			internal void SetLocation(Vector2 position, IPanel panel)
			{
				this.Position = position;
				this.Panel = panel;
				this.Flags = PointerDeviceState.LocationFlag.None;
				bool flag = panel == null || !panel.visualTree.layout.Contains(position);
				if (flag)
				{
					this.Flags |= PointerDeviceState.LocationFlag.OutsidePanel;
				}
			}
		}
	}
}
