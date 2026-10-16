using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000060 RID: 96
	public class ActionsHelper
	{
		// Token: 0x06000340 RID: 832 RVA: 0x000072C3 File Offset: 0x000054C3
		public static void ExecuteBeforeActions(IEnumerable<ITestAction> actions, ITest test)
		{
			ActionsHelper.ExecuteActions(ActionsHelper.ActionPhase.Before, actions, test);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000072CD File Offset: 0x000054CD
		public static void ExecuteAfterActions(IEnumerable<ITestAction> actions, ITest test)
		{
			ActionsHelper.ExecuteActions(ActionsHelper.ActionPhase.After, actions, test);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000072D8 File Offset: 0x000054D8
		private static void ExecuteActions(ActionsHelper.ActionPhase phase, IEnumerable<ITestAction> actions, ITest test)
		{
			if (actions == null)
			{
				return;
			}
			foreach (ITestAction testAction in ActionsHelper.GetFilteredAndSortedActions(actions, phase))
			{
				if (phase == ActionsHelper.ActionPhase.Before)
				{
					testAction.BeforeTest(test);
				}
				else
				{
					testAction.AfterTest(test);
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00007316 File Offset: 0x00005516
		public static ITestAction[] GetActionsFromTestAssembly(TestAssembly testAssembly)
		{
			return ActionsHelper.GetActionsFromAttributeProvider(testAssembly.Assembly);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00007323 File Offset: 0x00005523
		public static ITestAction[] GetActionsFromTestMethodInfo(IMethodInfo testAssembly)
		{
			return ActionsHelper.GetActionsFromAttributeProvider(testAssembly.MethodInfo);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00007330 File Offset: 0x00005530
		public static ITestAction[] GetActionsFromAttributeProvider(ICustomAttributeProvider attributeProvider)
		{
			if (attributeProvider == null)
			{
				return new ITestAction[0];
			}
			List<ITestAction> list = new List<ITestAction>((ITestAction[])attributeProvider.GetCustomAttributes(typeof(ITestAction), false));
			list.Sort(new Comparison<ITestAction>(ActionsHelper.SortByTargetDescending));
			return list.ToArray();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00007370 File Offset: 0x00005570
		public static ITestAction[] GetActionsFromTypesAttributes(Type type)
		{
			if (type == null)
			{
				return new ITestAction[0];
			}
			if (type == typeof(object))
			{
				return new ITestAction[0];
			}
			List<ITestAction> list = new List<ITestAction>();
			list.AddRange(ActionsHelper.GetActionsFromTypesAttributes(type.GetTypeInfo().BaseType));
			foreach (Type type2 in ActionsHelper.GetDeclaredInterfaces(type))
			{
				list.AddRange(ActionsHelper.GetActionsFromAttributeProvider(type2.GetTypeInfo()));
			}
			list.AddRange(ActionsHelper.GetActionsFromAttributeProvider(type.GetTypeInfo()));
			return list.ToArray();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00007404 File Offset: 0x00005604
		private static Type[] GetDeclaredInterfaces(Type type)
		{
			List<Type> list = new List<Type>(type.GetInterfaces());
			if (type.GetTypeInfo().BaseType == typeof(object))
			{
				return list.ToArray();
			}
			List<Type> list2 = new List<Type>(type.GetTypeInfo().BaseType.GetInterfaces());
			List<Type> list3 = new List<Type>();
			foreach (Type type2 in list)
			{
				if (!list2.Contains(type2))
				{
					list3.Add(type2);
				}
			}
			return list3.ToArray();
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000074B0 File Offset: 0x000056B0
		private static ITestAction[] GetFilteredAndSortedActions(IEnumerable<ITestAction> actions, ActionsHelper.ActionPhase phase)
		{
			List<ITestAction> list = new List<ITestAction>();
			foreach (ITestAction testAction in actions)
			{
				if (!list.Contains(testAction))
				{
					list.Add(testAction);
				}
			}
			if (phase == ActionsHelper.ActionPhase.After)
			{
				list.Reverse();
			}
			return list.ToArray();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007518 File Offset: 0x00005718
		private static int SortByTargetDescending(ITestAction x, ITestAction y)
		{
			return y.Targets.CompareTo(x.Targets);
		}

		// Token: 0x02000170 RID: 368
		private enum ActionPhase
		{
			// Token: 0x04000253 RID: 595
			Before,
			// Token: 0x04000254 RID: 596
			After
		}
	}
}
