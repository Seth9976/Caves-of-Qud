using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class GUILayoutx
{
	// Token: 0x06000534 RID: 1332 RVA: 0x00021CBF File Offset: 0x0001FEBF
	public static int SelectionList(int selected, GUIContent[] list)
	{
		return GUILayoutx.SelectionList(selected, list, "Label", null);
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x00021CD3 File Offset: 0x0001FED3
	public static int SelectionList(int selected, GUIContent[] list, GUIStyle elementStyle)
	{
		return GUILayoutx.SelectionList(selected, list, elementStyle, null);
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x00021CDE File Offset: 0x0001FEDE
	public static int SelectionList(int selected, GUIContent[] list, GUILayoutx.DoubleClickCallback callback)
	{
		return GUILayoutx.SelectionList(selected, list, "Label", callback);
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00021CF4 File Offset: 0x0001FEF4
	public static int SelectionList(int selected, GUIContent[] list, GUIStyle elementStyle, GUILayoutx.DoubleClickCallback callback)
	{
		for (int i = 0; i < list.Length; i++)
		{
			Rect rect = GUILayoutUtility.GetRect(list[i], elementStyle);
			bool flag = rect.Contains(Event.current.mousePosition);
			if (flag && Event.current.type == EventType.MouseDown)
			{
				selected = i;
				callback(i);
				Event.current.Use();
			}
			else if (flag && callback != null && Event.current.type == EventType.MouseUp && Event.current.clickCount == 2)
			{
				Event.current.Use();
			}
			else if (Event.current.type == EventType.Repaint)
			{
				elementStyle.Draw(rect, list[i], flag, false, i == selected, false);
			}
		}
		return selected;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00021DA1 File Offset: 0x0001FFA1
	public static int SelectionList(int selected, string[] list)
	{
		return GUILayoutx.SelectionList(selected, list, "Label", null);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00021DB5 File Offset: 0x0001FFB5
	public static int SelectionList(int selected, string[] list, GUIStyle elementStyle)
	{
		return GUILayoutx.SelectionList(selected, list, elementStyle, null);
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00021DC0 File Offset: 0x0001FFC0
	public static int SelectionList(int selected, string[] list, GUILayoutx.DoubleClickCallback callback)
	{
		return GUILayoutx.SelectionList(selected, list, "Label", callback);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00021DD4 File Offset: 0x0001FFD4
	public static int SelectionList(int selected, string[] list, GUIStyle elementStyle, GUILayoutx.DoubleClickCallback callback)
	{
		for (int i = 0; i < list.Length; i++)
		{
			Rect rect = GUILayoutUtility.GetRect(new GUIContent(list[i]), elementStyle);
			bool flag = rect.Contains(Event.current.mousePosition);
			if (flag && Event.current.type == EventType.MouseDown)
			{
				selected = i;
				Event.current.Use();
			}
			else if (flag && callback != null && Event.current.type == EventType.MouseUp && Event.current.clickCount == 2)
			{
				callback(i);
				Event.current.Use();
			}
			else if (Event.current.type == EventType.Repaint)
			{
				elementStyle.Draw(rect, list[i], flag, false, i == selected, false);
			}
		}
		return selected;
	}

	// Token: 0x020015A7 RID: 5543
	// (Invoke) Token: 0x0600D5E1 RID: 54753
	public delegate void DoubleClickCallback(int index);
}
