using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D0 RID: 720
	internal static class GroupBoxUtility
	{
		// Token: 0x060014B7 RID: 5303 RVA: 0x000531E8 File Offset: 0x000513E8
		public static void RegisterGroupBoxOption<T>(this T option) where T : VisualElement, IGroupBoxOption
		{
			VisualElement visualElement = option;
			IGroupBox groupBox = null;
			for (VisualElement visualElement2 = visualElement.hierarchy.parent; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				IGroupBox groupBox2 = visualElement2 as IGroupBox;
				bool flag = groupBox2 != null;
				if (flag)
				{
					groupBox = groupBox2;
					break;
				}
			}
			IGroupBox groupBox3 = groupBox ?? visualElement.elementPanel;
			IGroupManager groupManager = GroupBoxUtility.FindOrCreateGroupManager(groupBox3);
			groupManager.RegisterOption(option);
			GroupBoxUtility.s_GroupOptionManagerCache[option] = groupManager;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0005327C File Offset: 0x0005147C
		public static void UnregisterGroupBoxOption<T>(this T option) where T : VisualElement, IGroupBoxOption
		{
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(option);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[option].UnregisterOption(option);
				GroupBoxUtility.s_GroupOptionManagerCache.Remove(option);
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000532D0 File Offset: 0x000514D0
		public static void OnOptionSelected<T>(this T selectedOption) where T : VisualElement, IGroupBoxOption
		{
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(selectedOption);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[selectedOption].OnOptionSelectionChanged(selectedOption);
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00053314 File Offset: 0x00051514
		public static IGroupBoxOption GetSelectedOption(this IGroupBox groupBox)
		{
			return (!GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox)) ? null : GroupBoxUtility.s_GroupManagers[groupBox].GetSelectedOption();
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x00053348 File Offset: 0x00051548
		public static IGroupManager GetGroupManager(this IGroupBox groupBox)
		{
			return GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox) ? GroupBoxUtility.s_GroupManagers[groupBox] : null;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x00053378 File Offset: 0x00051578
		private static IGroupManager FindOrCreateGroupManager(IGroupBox groupBox)
		{
			bool flag = GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox);
			IGroupManager groupManager;
			if (flag)
			{
				groupManager = GroupBoxUtility.s_GroupManagers[groupBox];
			}
			else
			{
				Type type = null;
				foreach (Type type2 in groupBox.GetType().GetInterfaces())
				{
					bool flag2 = type2.IsGenericType && GroupBoxUtility.k_GenericGroupBoxType.IsAssignableFrom(type2.GetGenericTypeDefinition());
					if (flag2)
					{
						type = type2.GetGenericArguments()[0];
						break;
					}
				}
				IGroupManager groupManager3;
				if (!(type != null))
				{
					IGroupManager groupManager2 = new DefaultGroupManager();
					groupManager3 = groupManager2;
				}
				else
				{
					groupManager3 = (IGroupManager)Activator.CreateInstance(type);
				}
				IGroupManager groupManager4 = groupManager3;
				groupManager4.Init(groupBox);
				BaseVisualElementPanel baseVisualElementPanel = groupBox as BaseVisualElementPanel;
				bool flag3 = baseVisualElementPanel != null;
				if (flag3)
				{
					baseVisualElementPanel.panelDisposed += GroupBoxUtility.OnPanelDestroyed;
				}
				else
				{
					VisualElement visualElement = groupBox as VisualElement;
					bool flag4 = visualElement != null;
					if (flag4)
					{
						visualElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(GroupBoxUtility.OnGroupBoxDetachedFromPanel), TrickleDown.NoTrickleDown);
					}
				}
				GroupBoxUtility.s_GroupManagers[groupBox] = groupManager4;
				groupManager = groupManager4;
			}
			return groupManager;
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x00053492 File Offset: 0x00051692
		private static void OnGroupBoxDetachedFromPanel(DetachFromPanelEvent evt)
		{
			GroupBoxUtility.s_GroupManagers.Remove(evt.currentTarget as IGroupBox);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000534AB File Offset: 0x000516AB
		private static void OnPanelDestroyed(BaseVisualElementPanel panel)
		{
			GroupBoxUtility.s_GroupManagers.Remove(panel);
			panel.panelDisposed -= GroupBoxUtility.OnPanelDestroyed;
		}

		// Token: 0x04000B3B RID: 2875
		private static Dictionary<IGroupBox, IGroupManager> s_GroupManagers = new Dictionary<IGroupBox, IGroupManager>();

		// Token: 0x04000B3C RID: 2876
		private static Dictionary<IGroupBoxOption, IGroupManager> s_GroupOptionManagerCache = new Dictionary<IGroupBoxOption, IGroupManager>();

		// Token: 0x04000B3D RID: 2877
		private static readonly Type k_GenericGroupBoxType = typeof(IGroupBox<>);
	}
}
