using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200050E RID: 1294
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class UIElementsUtility : IUIElementsUtility
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x00092D70 File Offset: 0x00090F70
		public static bool isOSXContextualMenuPlatform
		{
			get
			{
				RuntimePlatform platform = Application.platform;
				return platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer || UIElementsUtility.s_EnableOSXContextualMenuEventsOnNonOSXPlatforms;
			}
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00092D97 File Offset: 0x00090F97
		internal static void EnableOSXContextualMenuEventsOnNonOSXPlatforms()
		{
			UIElementsUtility.s_EnableOSXContextualMenuEventsOnNonOSXPlatforms = true;
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00092DA0 File Offset: 0x00090FA0
		internal static void ResetOSXContextualMenuEventsOnNonOSXPlatforms()
		{
			UIElementsUtility.s_EnableOSXContextualMenuEventsOnNonOSXPlatforms = false;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00092DA9 File Offset: 0x00090FA9
		private UIElementsUtility()
		{
			UIEventRegistration.RegisterUIElementSystem(this);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00092DBC File Offset: 0x00090FBC
		internal static IMGUIContainer GetCurrentIMGUIContainer()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			IMGUIContainer imguicontainer;
			if (flag)
			{
				imguicontainer = UIElementsUtility.s_ContainerStack.Peek();
			}
			else
			{
				imguicontainer = null;
			}
			return imguicontainer;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00092DF0 File Offset: 0x00090FF0
		bool IUIElementsUtility.MakeCurrentIMGUIContainerDirty()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool flag2;
			if (flag)
			{
				UIElementsUtility.s_ContainerStack.Peek().MarkDirtyLayout();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00092E2C File Offset: 0x0009102C
		bool IUIElementsUtility.TakeCapture()
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			bool flag2;
			if (flag)
			{
				IMGUIContainer imguicontainer = UIElementsUtility.s_ContainerStack.Peek();
				imguicontainer.CaptureMouse();
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00092E68 File Offset: 0x00091068
		bool IUIElementsUtility.ReleaseCapture()
		{
			return false;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00092E7C File Offset: 0x0009107C
		bool IUIElementsUtility.ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled)
		{
			Panel panel;
			bool flag = nativeEventPtr != IntPtr.Zero && UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
			bool flag3;
			if (flag)
			{
				bool flag2 = panel.contextType == ContextType.Editor;
				if (flag2)
				{
					UIElementsUtility.s_EventInstance.CopyFromPtr(nativeEventPtr);
					eventHandled = UIElementsUtility.DoDispatch(panel);
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00092ED8 File Offset: 0x000910D8
		bool IUIElementsUtility.CleanupRoots()
		{
			UIElementsUtility.s_EventInstance = null;
			UIElementsUtility.s_UIElementsCache = null;
			UIElementsUtility.s_ContainerStack = null;
			return false;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00092F00 File Offset: 0x00091100
		bool IUIElementsUtility.EndContainerGUIFromException(Exception exception)
		{
			bool flag = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
			return false;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00092F38 File Offset: 0x00091138
		void IUIElementsUtility.UpdateSchedulers()
		{
			UIElementsUtility.s_PanelsIterationList.Clear();
			UIElementsUtility.GetAllPanels(UIElementsUtility.s_PanelsIterationList, ContextType.Editor);
			foreach (Panel panel in UIElementsUtility.s_PanelsIterationList)
			{
				panel.timerEventScheduler.UpdateScheduledEvents();
				panel.ValidateFocus();
				panel.UpdateDataBinding();
				panel.UpdateAnimations();
				panel.UpdateBindings();
			}
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00092FC8 File Offset: 0x000911C8
		void IUIElementsUtility.RequestRepaintForPanels(Action<ScriptableObject> repaintCallback)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				Panel value = keyValuePair.Value;
				bool flag = value.contextType != ContextType.Editor;
				if (!flag)
				{
					bool isDirty = value.isDirty;
					if (isDirty)
					{
						repaintCallback(value.ownerObject);
					}
				}
			}
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0009302A File Offset: 0x0009122A
		public static void RegisterCachedPanel(int instanceID, Panel panel)
		{
			UIElementsUtility.s_UIElementsCache.Add(instanceID, panel);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0009303A File Offset: 0x0009123A
		public static void RemoveCachedPanel(int instanceID)
		{
			UIElementsUtility.s_UIElementsCache.Remove(instanceID);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0009304C File Offset: 0x0009124C
		public static bool TryGetPanel(int instanceID, out Panel panel)
		{
			return UIElementsUtility.s_UIElementsCache.TryGetValue(instanceID, out panel);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0009306C File Offset: 0x0009126C
		internal static void BeginContainerGUI(GUILayoutUtility.LayoutCache cache, Event evt, IMGUIContainer container)
		{
			bool useOwnerObjectGUIState = container.useOwnerObjectGUIState;
			if (useOwnerObjectGUIState)
			{
				GUIUtility.BeginContainerFromOwner(container.elementPanel.ownerObject);
			}
			else
			{
				GUIUtility.BeginContainer(container.guiState);
			}
			UIElementsUtility.s_ContainerStack.Push(container);
			GUIUtility.s_SkinMode = (int)container.contextType;
			GUIUtility.s_OriginalID = container.elementPanel.ownerObject.GetInstanceID();
			bool flag = Event.current == null;
			if (flag)
			{
				Event.current = evt;
			}
			else
			{
				Event.current.CopyFrom(evt);
			}
			GUI.enabled = container.enabledInHierarchy;
			GUILayoutUtility.BeginContainer(cache);
			GUIUtility.ResetGlobalState();
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x00093114 File Offset: 0x00091314
		internal static void EndContainerGUI(Event evt, Rect layoutSize)
		{
			bool flag = Event.current.type == EventType.Layout && UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag)
			{
				GUILayoutUtility.LayoutFromContainer(layoutSize.width, layoutSize.height);
			}
			GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
			GUIContent.ClearStaticCache();
			bool flag2 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag2)
			{
			}
			evt.CopyFrom(Event.current);
			bool flag3 = UIElementsUtility.s_ContainerStack.Count > 0;
			if (flag3)
			{
				GUIUtility.EndContainer();
				UIElementsUtility.s_ContainerStack.Pop();
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000931B0 File Offset: 0x000913B0
		internal static EventBase CreateEvent(Event systemEvent)
		{
			return UIElementsUtility.CreateEvent(systemEvent, systemEvent.rawType);
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000931D0 File Offset: 0x000913D0
		internal static EventBase CreateEvent(Event systemEvent, EventType eventType)
		{
			switch (eventType)
			{
			case EventType.MouseDown:
				goto IL_0097;
			case EventType.MouseUp:
				goto IL_00C2;
			case EventType.MouseMove:
				break;
			case EventType.MouseDrag:
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			case EventType.KeyDown:
				return KeyboardEventBase<KeyDownEvent>.GetPooled(systemEvent);
			case EventType.KeyUp:
				return KeyboardEventBase<KeyUpEvent>.GetPooled(systemEvent);
			case EventType.ScrollWheel:
				return WheelEvent.GetPooled(systemEvent);
			case EventType.Repaint:
			case EventType.Layout:
			case EventType.DragUpdated:
			case EventType.DragPerform:
			case EventType.Ignore:
			case EventType.Used:
			case EventType.DragExited:
			case (EventType)17:
			case (EventType)18:
			case (EventType)19:
				goto IL_0134;
			case EventType.ValidateCommand:
				return CommandEventBase<ValidateCommandEvent>.GetPooled(systemEvent);
			case EventType.ExecuteCommand:
				return CommandEventBase<ExecuteCommandEvent>.GetPooled(systemEvent);
			case EventType.ContextClick:
				return MouseEventBase<ContextClickEvent>.GetPooled(systemEvent);
			case EventType.MouseEnterWindow:
				return MouseEventBase<MouseEnterWindowEvent>.GetPooled(systemEvent);
			case EventType.MouseLeaveWindow:
				return MouseLeaveWindowEvent.GetPooled(systemEvent);
			default:
				switch (eventType)
				{
				case EventType.TouchDown:
					goto IL_0097;
				case EventType.TouchUp:
					goto IL_00C2;
				case EventType.TouchMove:
					break;
				default:
					goto IL_0134;
				}
				break;
			}
			return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			IL_0097:
			bool flag = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
			if (flag)
			{
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			}
			return PointerEventBase<PointerDownEvent>.GetPooled(systemEvent);
			IL_00C2:
			bool flag2 = PointerDeviceState.HasAdditionalPressedButtons(PointerId.mousePointerId, systemEvent.button);
			if (flag2)
			{
				return PointerEventBase<PointerMoveEvent>.GetPooled(systemEvent);
			}
			return PointerEventBase<PointerUpEvent>.GetPooled(systemEvent);
			IL_0134:
			return IMGUIEvent.GetPooled(systemEvent);
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0009331C File Offset: 0x0009151C
		private static bool DoDispatch(BaseVisualElementPanel panel)
		{
			Debug.Assert(panel.contextType == ContextType.Editor, "panel.contextType == ContextType.Editor");
			bool flag = false;
			bool flag2 = UIElementsUtility.s_EventInstance.type == EventType.Repaint;
			if (flag2)
			{
				Camera current = Camera.current;
				RenderTexture active = RenderTexture.active;
				Camera.SetupCurrent(null);
				RenderTexture.active = null;
				using (UIElementsUtility.s_RepaintProfilerMarker.Auto())
				{
					panel.Repaint(UIElementsUtility.s_EventInstance);
					panel.Render();
				}
				flag = panel.IMGUIContainersCount > 0;
				Camera.SetupCurrent(current);
				RenderTexture.active = active;
			}
			else
			{
				panel.ValidateLayout();
				using (EventBase eventBase = UIElementsUtility.CreateEvent(UIElementsUtility.s_EventInstance))
				{
					bool flag3 = UIElementsUtility.s_EventInstance.type == EventType.Used || UIElementsUtility.s_EventInstance.type == EventType.Layout || UIElementsUtility.s_EventInstance.type == EventType.ExecuteCommand || UIElementsUtility.s_EventInstance.type == EventType.ValidateCommand;
					using (UIElementsUtility.s_EventProfilerMarker.Auto())
					{
						panel.SendEvent(eventBase, flag3 ? DispatchMode.Immediate : DispatchMode.Default);
					}
					bool isPropagationStopped = eventBase.isPropagationStopped;
					if (isPropagationStopped)
					{
						panel.visualTree.IncrementVersion(VersionChangeType.Repaint);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000934A8 File Offset: 0x000916A8
		internal static void GetAllPanels(List<Panel> panels, ContextType contextType)
		{
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				bool flag = keyValuePair.Value.contextType == contextType;
				if (flag)
				{
					keyValuePair = panelsIterator.Current;
					panels.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x00093500 File Offset: 0x00091700
		internal static Dictionary<int, Panel>.Enumerator GetPanelsIterator()
		{
			return UIElementsUtility.s_UIElementsCache.GetEnumerator();
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0009351C File Offset: 0x0009171C
		internal static float PixelsPerUnitScaleForElement(VisualElement ve, Sprite sprite)
		{
			bool flag = ve == null || ve.elementPanel == null || sprite == null;
			float num;
			if (flag)
			{
				num = 1f;
			}
			else
			{
				float referenceSpritePixelsPerUnit = ve.elementPanel.referenceSpritePixelsPerUnit;
				float num2 = sprite.pixelsPerUnit;
				num2 = Mathf.Max(0.01f, num2);
				num = referenceSpritePixelsPerUnit / num2;
			}
			return num;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x00093574 File Offset: 0x00091774
		internal static string ParseMenuName(string menuName)
		{
			bool flag = string.IsNullOrEmpty(menuName);
			string text;
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				string text2 = menuName.TrimEnd();
				int num = text2.LastIndexOf(' ');
				bool flag2 = num > -1;
				if (flag2)
				{
					int num2 = Array.IndexOf<char>(UIElementsUtility.s_Modifiers, text2[num + 1]);
					bool flag3 = text2.Length > num + 1 && num2 > -1;
					if (flag3)
					{
						text2 = text2.Substring(0, num).TrimEnd();
					}
				}
				text = text2;
			}
			return text;
		}

		// Token: 0x04001220 RID: 4640
		private static Stack<IMGUIContainer> s_ContainerStack = new Stack<IMGUIContainer>();

		// Token: 0x04001221 RID: 4641
		private static Dictionary<int, Panel> s_UIElementsCache = new Dictionary<int, Panel>();

		// Token: 0x04001222 RID: 4642
		private static Event s_EventInstance = new Event();

		// Token: 0x04001223 RID: 4643
		internal static Color editorPlayModeTintColor = Color.white;

		// Token: 0x04001224 RID: 4644
		internal static float singleLineHeight = 18f;

		// Token: 0x04001225 RID: 4645
		public const string hiddenClassName = "unity-hidden";

		// Token: 0x04001226 RID: 4646
		internal static bool s_EnableOSXContextualMenuEventsOnNonOSXPlatforms;

		// Token: 0x04001227 RID: 4647
		private static UIElementsUtility s_Instance = new UIElementsUtility();

		// Token: 0x04001228 RID: 4648
		internal static List<Panel> s_PanelsIterationList = new List<Panel>();

		// Token: 0x04001229 RID: 4649
		internal static readonly string s_RepaintProfilerMarkerName = "UIElementsUtility.DoDispatch(Repaint Event)";

		// Token: 0x0400122A RID: 4650
		internal static readonly string s_EventProfilerMarkerName = "UIElementsUtility.DoDispatch(Non Repaint Event)";

		// Token: 0x0400122B RID: 4651
		private static readonly ProfilerMarker s_RepaintProfilerMarker = new ProfilerMarker(UIElementsUtility.s_RepaintProfilerMarkerName);

		// Token: 0x0400122C RID: 4652
		private static readonly ProfilerMarker s_EventProfilerMarker = new ProfilerMarker(UIElementsUtility.s_EventProfilerMarkerName);

		// Token: 0x0400122D RID: 4653
		internal static char[] s_Modifiers = new char[] { '&', '%', '^', '#', '_' };
	}
}
