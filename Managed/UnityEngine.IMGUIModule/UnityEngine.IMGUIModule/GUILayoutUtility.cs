using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/IMGUI/GUILayoutUtility.bindings.h")]
	public class GUILayoutUtility
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000A24C File Offset: 0x0000844C
		private static Rect Internal_GetWindowRect(int windowID)
		{
			Rect rect;
			GUILayoutUtility.Internal_GetWindowRect_Injected(windowID, out rect);
			return rect;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A264 File Offset: 0x00008464
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.Internal_MoveWindow_Injected(windowID, ref r);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A27C File Offset: 0x0000847C
		internal static Rect GetWindowsBounds()
		{
			Rect rect;
			GUILayoutUtility.GetWindowsBounds_Injected(out rect);
			return rect;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000A291 File Offset: 0x00008491
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000A298 File Offset: 0x00008498
		internal static int unbalancedgroupscount { get; set; }

		// Token: 0x0600028E RID: 654 RVA: 0x0000A2A0 File Offset: 0x000084A0
		internal static void CleanupRoots()
		{
			GUILayoutUtility.s_SpaceStyle = null;
			GUILayoutUtility.s_StoredLayouts.Clear();
			GUILayoutUtility.s_StoredWindows.Clear();
			GUILayoutUtility.current = new GUILayoutUtility.LayoutCache(-1);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A2CC File Offset: 0x000084CC
		internal static GUILayoutUtility.LayoutCache GetLayoutCache(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			GUILayoutUtility.LayoutCache layoutCache;
			dictionary.TryGetValue(instanceID, out layoutCache);
			return layoutCache;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A2FC File Offset: 0x000084FC
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.GetLayoutCache(instanceID, isWindow);
			bool flag = layoutCache == null;
			if (flag)
			{
				layoutCache = new GUILayoutUtility.LayoutCache(instanceID);
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A374 File Offset: 0x00008574
		internal static void RemoveSelectedIdList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			bool flag = dictionary.ContainsKey(instanceID);
			if (flag)
			{
				dictionary.Remove(instanceID);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000A3A8 File Offset: 0x000085A8
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A460 File Offset: 0x00008660
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void BeginContainer(GUILayoutUtility.LayoutCache cache)
		{
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				cache.topLevel = new GUILayoutGroup();
				cache.layoutGroups.Clear();
				cache.layoutGroups.Push(cache.topLevel);
				cache.windows = new GUILayoutGroup();
			}
			GUILayoutUtility.current.topLevel = cache.topLevel;
			GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
			GUILayoutUtility.current.windows = cache.windows;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A4E8 File Offset: 0x000086E8
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				bool flag2 = options != null;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000027FD File Offset: 0x000009FD
		[Obsolete("BeginGroup has no effect and will be removed", false)]
		public static void BeginGroup(string GroupName)
		{
		}

		// Token: 0x06000296 RID: 662 RVA: 0x000027FD File Offset: 0x000009FD
		[Obsolete("EndGroup has no effect and will be removed", false)]
		public static void EndGroup(string groupName)
		{
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A5DC File Offset: 0x000087DC
		internal static void Layout()
		{
			bool flag = GUILayoutUtility.current.topLevel.windowID == -1;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A6C4 File Offset: 0x000088C4
		internal static void LayoutFromEditorWindow()
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A768 File Offset: 0x00008968
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal static void LayoutFromContainer(float w, float h)
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, w);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, h);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A7F4 File Offset: 0x000089F4
		internal static float LayoutFromInspector(float width)
		{
			bool flag = GUILayoutUtility.current.topLevel != null && GUILayoutUtility.current.topLevel.windowID == -1;
			float num;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, width);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				float minHeight = GUILayoutUtility.current.topLevel.minHeight;
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
				num = minHeight;
			}
			else
			{
				bool flag2 = GUILayoutUtility.current.topLevel != null;
				if (flag2)
				{
					GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				}
				num = 0f;
			}
			return num;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A8E4 File Offset: 0x00008AE4
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup guilayoutGroup = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(guilayoutGroup);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000A948 File Offset: 0x00008B48
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			bool flag = !i.isWindow;
			if (flag)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000AA4C File Offset: 0x00008C4C
		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			bool flag = !typeof(GUILayoutGroup).IsAssignableFrom(LayoutType);
			if (flag)
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup", "LayoutType");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AA90 File Offset: 0x00008C90
		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type layoutType)
		{
			GUILayoutUtility.unbalancedgroupscount++;
			EventType type = Event.current.type;
			EventType eventType = type;
			GUILayoutGroup guilayoutGroup;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup;
				bool flag = guilayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type.ToString());
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				bool flag2 = options != null;
				if (flag2)
				{
					guilayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AB6C File Offset: 0x00008D6C
		internal static void EndLayoutGroup()
		{
			GUILayoutUtility.unbalancedgroupscount--;
			bool flag = GUILayoutUtility.current.layoutGroups.Count == 0;
			if (flag)
			{
				Debug.LogError("EndLayoutGroup: BeginLayoutGroup must be called first.");
			}
			else
			{
				GUILayoutUtility.current.layoutGroups.Pop();
				bool flag2 = 0 < GUILayoutUtility.current.layoutGroups.Count;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
				}
				else
				{
					GUILayoutUtility.current.topLevel = new GUILayoutGroup();
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000AC00 File Offset: 0x00008E00
		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type layoutType)
		{
			GUILayoutUtility.unbalancedgroupscount++;
			EventType type = Event.current.type;
			EventType eventType = type;
			GUILayoutGroup guilayoutGroup;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup;
				bool flag = guilayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type.ToString());
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000ACC9 File Offset: 0x00008EC9
		internal static void EndLayoutArea()
		{
			GUILayoutUtility.unbalancedgroupscount--;
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000AD08 File Offset: 0x00008F08
		internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type layoutType)
		{
			return GUILayoutUtility.BeginLayoutArea(style, layoutType);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AD21 File Offset: 0x00008F21
		internal static GUILayoutGroup topLevel
		{
			get
			{
				return GUILayoutUtility.current.topLevel;
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AD30 File Offset: 0x00008F30
		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(content, style, null);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000AD68 File Offset: 0x00008F68
		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					GUILayoutEntry next = GUILayoutUtility.current.topLevel.GetNext();
					rect = next.rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				bool isHeightDependantOnWidth = style.isHeightDependantOnWidth;
				if (isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 vector = new Vector2(0f, 0f);
					bool flag = options != null;
					if (flag)
					{
						foreach (GUILayoutOption guilayoutOption in options)
						{
							GUILayoutOption.Type type2 = guilayoutOption.type;
							GUILayoutOption.Type type3 = type2;
							if (type3 != GUILayoutOption.Type.maxWidth)
							{
								if (type3 == GUILayoutOption.Type.maxHeight)
								{
									vector.y = (float)guilayoutOption.value;
								}
							}
							else
							{
								vector.x = (float)guilayoutOption.value;
							}
						}
					}
					Vector2 vector2 = style.CalcSizeWithConstraints(content, vector);
					vector2.x = Mathf.Ceil(vector2.x);
					vector2.y = Mathf.Ceil(vector2.y);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector2.x, vector2.x, vector2.y, vector2.y, style, options));
				}
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000AEDC File Offset: 0x000090DC
		public static Rect GetRect(float width, float height)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000AF00 File Offset: 0x00009100
		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, null);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AF20 File Offset: 0x00009120
		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AF44 File Offset: 0x00009144
		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000AF64 File Offset: 0x00009164
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000AF88 File Offset: 0x00009188
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000AFCC File Offset: 0x000091CC
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000AFEC File Offset: 0x000091EC
		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				rect = new Rect(0f, 0f, maxWidth, maxHeight);
			}
			return rect;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000B068 File Offset: 0x00009268
		public static Rect GetLastRect()
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect last;
			if (eventType != EventType.Layout && eventType != EventType.Used)
			{
				last = GUILayoutUtility.current.topLevel.GetLast();
			}
			else
			{
				last = GUILayoutUtility.kDummyRect;
			}
			return last;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B0AC File Offset: 0x000092AC
		public static Rect GetAspectRect(float aspect)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B0E4 File Offset: 0x000092E4
		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B100 File Offset: 0x00009300
		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B11C File Offset: 0x0000931C
		private static Rect DoGetAspectRect(float aspect, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			EventType eventType = type;
			Rect rect;
			if (eventType != EventType.Layout)
			{
				if (eventType != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIAspectSizer(aspect, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B184 File Offset: 0x00009384
		internal static GUIStyle spaceStyle
		{
			get
			{
				bool flag = GUILayoutUtility.s_SpaceStyle == null;
				if (flag)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		// Token: 0x060002B9 RID: 697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetWindowRect_Injected(int windowID, out Rect ret);

		// Token: 0x060002BA RID: 698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MoveWindow_Injected(int windowID, [In] ref Rect r);

		// Token: 0x060002BB RID: 699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWindowsBounds_Injected(out Rect ret);

		// Token: 0x04000098 RID: 152
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000099 RID: 153
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x0400009A RID: 154
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache(-1);

		// Token: 0x0400009B RID: 155
		internal static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x0400009D RID: 157
		private static GUIStyle s_SpaceStyle;

		// Token: 0x02000024 RID: 36
		internal readonly struct LayoutCacheState
		{
			// Token: 0x060002BC RID: 700 RVA: 0x0000B1FC File Offset: 0x000093FC
			public LayoutCacheState(GUILayoutUtility.LayoutCache cache)
			{
				this.id = cache.id;
				this.topLevel = cache.topLevel;
				this.layoutGroups = cache.layoutGroups;
				this.windows = cache.windows;
			}

			// Token: 0x0400009E RID: 158
			public readonly int id;

			// Token: 0x0400009F RID: 159
			public readonly GUILayoutGroup topLevel;

			// Token: 0x040000A0 RID: 160
			public readonly GenericStack layoutGroups;

			// Token: 0x040000A1 RID: 161
			public readonly GUILayoutGroup windows;
		}

		// Token: 0x02000025 RID: 37
		[DebuggerDisplay("id={id}, groups={layoutGroups.Count}")]
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal sealed class LayoutCache
		{
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060002BD RID: 701 RVA: 0x0000B22F File Offset: 0x0000942F
			// (set) Token: 0x060002BE RID: 702 RVA: 0x0000B237 File Offset: 0x00009437
			internal int id { get; private set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B240 File Offset: 0x00009440
			public GUILayoutUtility.LayoutCacheState State
			{
				get
				{
					return new GUILayoutUtility.LayoutCacheState(this);
				}
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000B248 File Offset: 0x00009448
			public LayoutCache(int instanceID = -1)
			{
				this.id = instanceID;
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000B298 File Offset: 0x00009498
			internal void CopyState(GUILayoutUtility.LayoutCacheState other)
			{
				this.id = other.id;
				this.topLevel = other.topLevel;
				this.layoutGroups = other.layoutGroups;
				this.windows = other.windows;
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000B2CC File Offset: 0x000094CC
			public void ResetCursor()
			{
				this.windows.ResetCursor();
				this.topLevel.ResetCursor();
				foreach (object obj in this.layoutGroups)
				{
					((GUILayoutGroup)obj).ResetCursor();
				}
			}

			// Token: 0x040000A3 RID: 163
			public GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x040000A4 RID: 164
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x040000A5 RID: 165
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
