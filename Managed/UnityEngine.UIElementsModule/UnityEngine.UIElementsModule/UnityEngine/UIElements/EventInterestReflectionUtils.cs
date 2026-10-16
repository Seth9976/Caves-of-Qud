using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x020005B8 RID: 1464
	internal static class EventInterestReflectionUtils
	{
		// Token: 0x06002B86 RID: 11142 RVA: 0x0009D03C File Offset: 0x0009B23C
		internal static void GetDefaultEventInterests(Type elementType, out int defaultActionCategories, out int defaultActionAtTargetCategories, out int handleEventTrickleDownCategories, out int handleEventBubbleUpCategories)
		{
			EventInterestReflectionUtils.DefaultEventInterests defaultEventInterests;
			bool flag = !EventInterestReflectionUtils.s_DefaultEventInterests.TryGetValue(elementType, out defaultEventInterests);
			if (flag)
			{
				Type baseType = elementType.BaseType;
				bool flag2 = baseType != null;
				if (flag2)
				{
					EventInterestReflectionUtils.GetDefaultEventInterests(baseType, out defaultEventInterests.DefaultActionCategories, out defaultEventInterests.DefaultActionAtTargetCategories, out defaultEventInterests.HandleEventTrickleDownCategories, out defaultEventInterests.HandleEventBubbleUpCategories);
				}
				defaultEventInterests.DefaultActionCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultAction") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultActionDisabled");
				defaultEventInterests.DefaultActionAtTargetCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultActionAtTarget") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultActionDisabledAtTarget");
				defaultEventInterests.HandleEventTrickleDownCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventTrickleDown") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventTrickleDownDisabled");
				defaultEventInterests.HandleEventBubbleUpCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventBubbleUp") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventBubbleUpDisabled");
				EventInterestReflectionUtils.s_DefaultEventInterests.Add(elementType, defaultEventInterests);
			}
			defaultActionCategories = defaultEventInterests.DefaultActionCategories;
			defaultActionAtTargetCategories = defaultEventInterests.DefaultActionAtTargetCategories;
			handleEventTrickleDownCategories = defaultEventInterests.HandleEventTrickleDownCategories;
			handleEventBubbleUpCategories = defaultEventInterests.HandleEventBubbleUpCategories;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x0009D150 File Offset: 0x0009B350
		private static int ComputeDefaultEventInterests(Type elementType, string methodName)
		{
			MethodInfo method = elementType.GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			bool flag = method == null;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = false;
				int num2 = 0;
				object[] customAttributes = method.GetCustomAttributes(typeof(EventInterestAttribute), false);
				foreach (EventInterestAttribute eventInterestAttribute in customAttributes)
				{
					flag2 = true;
					bool flag3 = eventInterestAttribute.eventTypes != null;
					if (flag3)
					{
						foreach (Type type in eventInterestAttribute.eventTypes)
						{
							num2 |= 1 << (int)EventInterestReflectionUtils.GetEventCategory(type);
						}
					}
					num2 |= (int)eventInterestAttribute.categoryFlags;
				}
				num = (flag2 ? num2 : (-1));
			}
			return num;
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x0009D218 File Offset: 0x0009B418
		internal static EventCategory GetEventCategory(Type eventType)
		{
			EventCategory category;
			bool flag = EventInterestReflectionUtils.s_EventCategories.TryGetValue(eventType, out category);
			EventCategory eventCategory;
			if (flag)
			{
				eventCategory = category;
			}
			else
			{
				object[] customAttributes = eventType.GetCustomAttributes(typeof(EventCategoryAttribute), true);
				object[] array = customAttributes;
				int num = 0;
				if (num >= array.Length)
				{
					throw new ArgumentOutOfRangeException("eventType", "Type must derive from EventBase<T>");
				}
				EventCategoryAttribute eventCategoryAttribute = (EventCategoryAttribute)array[num];
				category = eventCategoryAttribute.category;
				EventInterestReflectionUtils.s_EventCategories.Add(eventType, category);
				eventCategory = category;
			}
			return eventCategory;
		}

		// Token: 0x040013B2 RID: 5042
		private static readonly Dictionary<Type, EventInterestReflectionUtils.DefaultEventInterests> s_DefaultEventInterests = new Dictionary<Type, EventInterestReflectionUtils.DefaultEventInterests>();

		// Token: 0x040013B3 RID: 5043
		private static readonly Dictionary<Type, EventCategory> s_EventCategories = new Dictionary<Type, EventCategory>();

		// Token: 0x020005B9 RID: 1465
		private struct DefaultEventInterests
		{
			// Token: 0x040013B4 RID: 5044
			public int DefaultActionCategories;

			// Token: 0x040013B5 RID: 5045
			public int DefaultActionAtTargetCategories;

			// Token: 0x040013B6 RID: 5046
			public int HandleEventTrickleDownCategories;

			// Token: 0x040013B7 RID: 5047
			public int HandleEventBubbleUpCategories;
		}
	}
}
