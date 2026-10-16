using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000508 RID: 1288
	internal static class UIElementsRuntimeUtility
	{
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060027EA RID: 10218 RVA: 0x00091EC4 File Offset: 0x000900C4
		// (remove) Token: 0x060027EB RID: 10219 RVA: 0x00091EF8 File Offset: 0x000900F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<BaseRuntimePanel> onCreatePanel;

		// Token: 0x060027EC RID: 10220 RVA: 0x00091F2C File Offset: 0x0009012C
		static UIElementsRuntimeUtility()
		{
			Canvas.externBeginRenderOverlays = new Action<int>(UIElementsRuntimeUtility.BeginRenderOverlays);
			Canvas.externRenderOverlaysBefore = delegate(int displayIndex, int sortOrder)
			{
				UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, (float)sortOrder);
			};
			Canvas.externEndRenderOverlays = new Action<int>(UIElementsRuntimeUtility.EndRenderOverlays);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00091FBC File Offset: 0x000901BC
		public static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00091FDC File Offset: 0x000901DC
		public static BaseRuntimePanel FindOrCreateRuntimePanel(ScriptableObject ownerObject, UIElementsRuntimeUtility.CreateRuntimePanelDelegate createDelegate)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				BaseRuntimePanel baseRuntimePanel = panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					return baseRuntimePanel;
				}
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
			BaseRuntimePanel baseRuntimePanel2 = createDelegate(ownerObject);
			baseRuntimePanel2.IMGUIEventInterests = new EventInterests
			{
				wantsMouseMove = true,
				wantsMouseEnterLeaveWindow = true
			};
			UIElementsRuntimeUtility.RegisterCachedPanelInternal(ownerObject.GetInstanceID(), baseRuntimePanel2);
			Action<BaseRuntimePanel> action = UIElementsRuntimeUtility.onCreatePanel;
			if (action != null)
			{
				action(baseRuntimePanel2);
			}
			return baseRuntimePanel2;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00092070 File Offset: 0x00090270
		public static void DisposeRuntimePanel(ScriptableObject ownerObject)
		{
			Panel panel;
			bool flag = UIElementsUtility.TryGetPanel(ownerObject.GetInstanceID(), out panel);
			if (flag)
			{
				panel.Dispose();
				UIElementsRuntimeUtility.RemoveCachedPanelInternal(ownerObject.GetInstanceID());
			}
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000920A4 File Offset: 0x000902A4
		private static void RegisterCachedPanelInternal(int instanceID, IPanel panel)
		{
			UIElementsUtility.RegisterCachedPanel(instanceID, panel as Panel);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			bool flag = !UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = true;
				UIElementsRuntimeUtility.RegisterPlayerloopCallback();
				Canvas.SetExternalCanvasEnabled(true);
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000920E8 File Offset: 0x000902E8
		private static void RemoveCachedPanelInternal(int instanceID)
		{
			UIElementsUtility.RemoveCachedPanel(instanceID);
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			bool flag = UIElementsRuntimeUtility.s_SortedRuntimePanels.Count == 0;
			if (flag)
			{
				UIElementsRuntimeUtility.s_RegisteredPlayerloopCallback = false;
				UIElementsRuntimeUtility.UnregisterPlayerloopCallback();
				Canvas.SetExternalCanvasEnabled(false);
			}
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x00092140 File Offset: 0x00090340
		public static void RenderOffscreenPanels()
		{
			Camera current = Camera.current;
			RenderTexture active = RenderTexture.active;
			foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				bool flag = !baseRuntimePanel.drawsInCameras && baseRuntimePanel.targetTexture != null;
				if (flag)
				{
					UIElementsRuntimeUtility.RenderPanel(baseRuntimePanel, false);
				}
			}
			Camera.SetupCurrent(current);
			RenderTexture.active = active;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000921D8 File Offset: 0x000903D8
		public static void RepaintPanel(BaseRuntimePanel panel)
		{
			Camera current = Camera.current;
			RenderTexture active = RenderTexture.active;
			using (UIElementsRuntimeUtility.s_RepaintProfilerMarker.Auto())
			{
				panel.Repaint(Event.current);
			}
			Camera.SetupCurrent(current);
			RenderTexture.active = active;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0009223C File Offset: 0x0009043C
		public static void RenderPanel(BaseRuntimePanel panel, bool restoreState = true)
		{
			Debug.Assert(!panel.drawsInCameras);
			Camera current = Camera.current;
			RenderTexture active = RenderTexture.active;
			panel.Render();
			bool flag = !panel.drawsInCameras && restoreState;
			if (flag)
			{
				Camera.SetupCurrent(current);
				RenderTexture.active = active;
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0009228B File Offset: 0x0009048B
		internal static void BeginRenderOverlays(int displayIndex)
		{
			UIElementsRuntimeUtility.currentOverlayIndex = 0;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x00092294 File Offset: 0x00090494
		internal static void RenderOverlaysBeforePriority(int displayIndex, float maxPriority)
		{
			bool flag = UIElementsRuntimeUtility.currentOverlayIndex < 0;
			if (!flag)
			{
				List<Panel> sortedPlayerPanels = UIElementsRuntimeUtility.GetSortedPlayerPanels();
				while (UIElementsRuntimeUtility.currentOverlayIndex < sortedPlayerPanels.Count)
				{
					BaseRuntimePanel baseRuntimePanel = sortedPlayerPanels[UIElementsRuntimeUtility.currentOverlayIndex] as BaseRuntimePanel;
					bool flag2 = baseRuntimePanel != null;
					if (flag2)
					{
						bool flag3 = baseRuntimePanel.sortingPriority >= maxPriority;
						if (flag3)
						{
							break;
						}
						bool flag4 = baseRuntimePanel.targetDisplay == displayIndex && !baseRuntimePanel.drawsInCameras && baseRuntimePanel.targetTexture == null;
						if (flag4)
						{
							UIElementsRuntimeUtility.RenderPanel(baseRuntimePanel, true);
						}
					}
					UIElementsRuntimeUtility.currentOverlayIndex++;
				}
			}
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00092339 File Offset: 0x00090539
		internal static void EndRenderOverlays(int displayIndex)
		{
			UIElementsRuntimeUtility.RenderOverlaysBeforePriority(displayIndex, float.MaxValue);
			UIElementsRuntimeUtility.currentOverlayIndex = -1;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00092350 File Offset: 0x00090550
		public static void RepaintPanels(bool onlyOffscreen)
		{
			foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				bool flag = !onlyOffscreen || baseRuntimePanel.targetTexture != null;
				if (flag)
				{
					UIElementsRuntimeUtility.RepaintPanel(baseRuntimePanel);
				}
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x000923C4 File Offset: 0x000905C4
		// (set) Token: 0x060027FA RID: 10234 RVA: 0x000923CB File Offset: 0x000905CB
		internal static Object activeEventSystem { get; private set; }

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x000923D3 File Offset: 0x000905D3
		internal static bool useDefaultEventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem == null;
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000923E0 File Offset: 0x000905E0
		public static void RegisterEventSystem(Object eventSystem)
		{
			bool flag = UIElementsRuntimeUtility.activeEventSystem != null && UIElementsRuntimeUtility.activeEventSystem != eventSystem && eventSystem.GetType().Name == "EventSystem";
			if (flag)
			{
				Debug.LogWarning("There can be only one active Event System.");
			}
			UIElementsRuntimeUtility.activeEventSystem = eventSystem;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x00092438 File Offset: 0x00090638
		public static void UnregisterEventSystem(Object eventSystem)
		{
			bool flag = UIElementsRuntimeUtility.activeEventSystem == eventSystem;
			if (flag)
			{
				UIElementsRuntimeUtility.activeEventSystem = null;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x0009245C File Offset: 0x0009065C
		internal static DefaultEventSystem defaultEventSystem
		{
			get
			{
				DefaultEventSystem defaultEventSystem;
				if ((defaultEventSystem = UIElementsRuntimeUtility.s_DefaultEventSystem) == null)
				{
					defaultEventSystem = (UIElementsRuntimeUtility.s_DefaultEventSystem = new DefaultEventSystem());
				}
				return defaultEventSystem;
			}
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00092474 File Offset: 0x00090674
		public static void UpdatePanels()
		{
			UIElementsRuntimeUtility.RemoveUnusedPanels();
			UIRenderDevice.ProcessDeviceFreeQueue();
			foreach (Panel panel in UIElementsRuntimeUtility.GetSortedPlayerPanels())
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)panel;
				baseRuntimePanel.Update();
			}
			bool useDefaultEventSystem = UIElementsRuntimeUtility.useDefaultEventSystem;
			if (useDefaultEventSystem)
			{
				UIElementsRuntimeUtility.defaultEventSystem.isInputReady = true;
				UIElementsRuntimeUtility.defaultEventSystem.Update(DefaultEventSystem.UpdateMode.IgnoreIfAppNotFocused);
			}
			else
			{
				bool flag = UIElementsRuntimeUtility.s_DefaultEventSystem != null;
				if (flag)
				{
					UIElementsRuntimeUtility.s_DefaultEventSystem.isInputReady = false;
				}
			}
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00092520 File Offset: 0x00090720
		internal static void MarkPotentiallyEmpty(PanelSettings settings)
		{
			bool flag = !UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Contains(settings);
			if (flag)
			{
				UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Add(settings);
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0009254C File Offset: 0x0009074C
		internal static void RemoveUnusedPanels()
		{
			foreach (PanelSettings panelSettings in UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings)
			{
				UIDocumentList attachedUIDocumentsList = panelSettings.m_AttachedUIDocumentsList;
				bool flag = attachedUIDocumentsList == null || attachedUIDocumentsList.m_AttachedUIDocuments.Count == 0;
				if (flag)
				{
					panelSettings.DisposePanel();
				}
			}
			UIElementsRuntimeUtility.s_PotentiallyEmptyPanelSettings.Clear();
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000925D0 File Offset: 0x000907D0
		public static void RegisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.RegisterPlayerloopCallback();
			UIElementsRuntimeUtilityNative.UpdatePanelsCallback = new Action(UIElementsRuntimeUtility.UpdatePanels);
			UIElementsRuntimeUtilityNative.RepaintPanelsCallback = new Action<bool>(UIElementsRuntimeUtility.RepaintPanels);
			UIElementsRuntimeUtilityNative.RenderOffscreenPanelsCallback = new Action(UIElementsRuntimeUtility.RenderOffscreenPanels);
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0009260C File Offset: 0x0009080C
		public static void UnregisterPlayerloopCallback()
		{
			UIElementsRuntimeUtilityNative.UnregisterPlayerloopCallback();
			UIElementsRuntimeUtilityNative.UpdatePanelsCallback = null;
			UIElementsRuntimeUtilityNative.RepaintPanelsCallback = null;
			UIElementsRuntimeUtilityNative.RenderOffscreenPanelsCallback = null;
			bool flag = UIElementsRuntimeUtility.s_DefaultEventSystem != null;
			if (flag)
			{
				UIElementsRuntimeUtility.s_DefaultEventSystem.isInputReady = false;
			}
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0009264A File Offset: 0x0009084A
		internal static void SetPanelOrderingDirty()
		{
			UIElementsRuntimeUtility.s_PanelOrderingDirty = true;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x00092654 File Offset: 0x00090854
		internal static List<Panel> GetSortedPlayerPanels()
		{
			bool flag = UIElementsRuntimeUtility.s_PanelOrderingDirty;
			if (flag)
			{
				UIElementsRuntimeUtility.SortPanels();
			}
			return UIElementsRuntimeUtility.s_SortedRuntimePanels;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0009267C File Offset: 0x0009087C
		private static void SortPanels()
		{
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Clear();
			UIElementsUtility.GetAllPanels(UIElementsRuntimeUtility.s_SortedRuntimePanels, ContextType.Player);
			UIElementsRuntimeUtility.s_SortedRuntimePanels.Sort(delegate(Panel a, Panel b)
			{
				BaseRuntimePanel baseRuntimePanel2 = a as BaseRuntimePanel;
				BaseRuntimePanel baseRuntimePanel3 = b as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel2 == null || baseRuntimePanel3 == null;
				int num;
				if (flag2)
				{
					num = 0;
				}
				else
				{
					float num2 = baseRuntimePanel2.sortingPriority - baseRuntimePanel3.sortingPriority;
					bool flag3 = Mathf.Approximately(0f, num2);
					if (flag3)
					{
						num = baseRuntimePanel2.m_RuntimePanelCreationIndex.CompareTo(baseRuntimePanel3.m_RuntimePanelCreationIndex);
					}
					else
					{
						num = ((num2 < 0f) ? (-1) : 1);
					}
				}
				return num;
			});
			for (int i = 0; i < UIElementsRuntimeUtility.s_SortedRuntimePanels.Count; i++)
			{
				BaseRuntimePanel baseRuntimePanel = UIElementsRuntimeUtility.s_SortedRuntimePanels[i] as BaseRuntimePanel;
				bool flag = baseRuntimePanel != null;
				if (flag)
				{
					baseRuntimePanel.resolvedSortingIndex = i;
				}
			}
			UIElementsRuntimeUtility.s_ResolvedSortingIndexMax = UIElementsRuntimeUtility.s_SortedRuntimePanels.Count - 1;
			UIElementsRuntimeUtility.s_PanelOrderingDirty = false;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x00092720 File Offset: 0x00090920
		internal static Vector2 MultiDisplayBottomLeftToPanelPosition(Vector2 position, out int? targetDisplay)
		{
			Vector2 vector = UIElementsRuntimeUtility.MultiDisplayToLocalScreenPosition(position, out targetDisplay);
			return UIElementsRuntimeUtility.ScreenBottomLeftToPanelPosition(vector, targetDisplay.GetValueOrDefault());
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x00092748 File Offset: 0x00090948
		internal static Vector2 MultiDisplayToLocalScreenPosition(Vector2 position, out int? targetDisplay)
		{
			Vector3 vector = Display.RelativeMouseAt(position);
			bool flag = vector != Vector3.zero;
			Vector2 vector2;
			if (flag)
			{
				targetDisplay = new int?((int)vector.z);
				vector2 = vector;
			}
			else
			{
				targetDisplay = null;
				vector2 = position;
			}
			return vector2;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0009279C File Offset: 0x0009099C
		internal static Vector2 ScreenBottomLeftToPanelPosition(Vector2 position, int targetDisplay)
		{
			return UIElementsRuntimeUtility.FlipY(position, UIElementsRuntimeUtility.GetRuntimeDisplayHeight(targetDisplay));
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000927BC File Offset: 0x000909BC
		internal static Vector2 ScreenBottomLeftToPanelDelta(Vector2 delta)
		{
			return UIElementsRuntimeUtility.FlipDeltaY(delta);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000927D4 File Offset: 0x000909D4
		internal static Vector2 PanelToScreenBottomLeftPosition(Vector2 panelPosition, int targetDisplay)
		{
			return UIElementsRuntimeUtility.FlipY(panelPosition, UIElementsRuntimeUtility.GetRuntimeDisplayHeight(targetDisplay));
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000927F4 File Offset: 0x000909F4
		internal static Vector2 FlipY(Vector2 p, float displayHeight)
		{
			p.y = displayHeight - p.y;
			return p;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x00092818 File Offset: 0x00090A18
		private static Vector2 FlipDeltaY(Vector2 delta)
		{
			delta.y = -delta.y;
			return delta;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0009283C File Offset: 0x00090A3C
		private static float GetRuntimeDisplayHeight(int targetDisplay)
		{
			bool flag = targetDisplay > 0 && targetDisplay < Display.displays.Length;
			float num;
			if (flag)
			{
				num = (float)Display.displays[targetDisplay].systemHeight;
			}
			else
			{
				num = (float)Screen.height;
			}
			return num;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0009287C File Offset: 0x00090A7C
		internal static float GetEditorDisplayHeight(int targetDisplay)
		{
			return UIElementsRuntimeUtility.GetRuntimeDisplayHeight(targetDisplay);
		}

		// Token: 0x04001212 RID: 4626
		private static bool s_RegisteredPlayerloopCallback = false;

		// Token: 0x04001213 RID: 4627
		private static List<Panel> s_SortedRuntimePanels = new List<Panel>();

		// Token: 0x04001214 RID: 4628
		private static bool s_PanelOrderingDirty = true;

		// Token: 0x04001215 RID: 4629
		internal static int s_ResolvedSortingIndexMax = 0;

		// Token: 0x04001216 RID: 4630
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsRuntimeUtility.DoDispatch(Repaint Event)";

		// Token: 0x04001217 RID: 4631
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsRuntimeUtility.s_RepaintProfilerMarkerName);

		// Token: 0x04001218 RID: 4632
		private static int currentOverlayIndex = -1;

		// Token: 0x0400121A RID: 4634
		private static DefaultEventSystem s_DefaultEventSystem;

		// Token: 0x0400121B RID: 4635
		private static List<PanelSettings> s_PotentiallyEmptyPanelSettings = new List<PanelSettings>();

		// Token: 0x02000509 RID: 1289
		// (Invoke) Token: 0x06002811 RID: 10257
		public delegate BaseRuntimePanel CreateRuntimePanelDelegate(ScriptableObject ownerObject);
	}
}
