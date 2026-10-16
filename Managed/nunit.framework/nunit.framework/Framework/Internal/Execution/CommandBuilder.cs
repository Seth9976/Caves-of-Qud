using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A0 RID: 160
	public static class CommandBuilder
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public static TestCommand MakeOneTimeSetUpCommand(TestSuite suite, List<SetUpTearDownItem> setUpTearDown, List<TestActionItem> actions)
		{
			if (suite.RunState != RunState.Runnable && suite.RunState != RunState.Explicit)
			{
				return CommandBuilder.MakeSkipCommand(suite);
			}
			TestCommand testCommand = new OneTimeSetUpCommand(suite, setUpTearDown, actions);
			IList<IApplyToContext> list = null;
			if (suite.TypeInfo != null)
			{
				list = suite.TypeInfo.GetCustomAttributes<IApplyToContext>(true);
			}
			else if (suite.Method != null)
			{
				list = suite.Method.GetCustomAttributes<IApplyToContext>(true);
			}
			else
			{
				TestAssembly testAssembly = suite as TestAssembly;
				if (testAssembly != null)
				{
					list = (IApplyToContext[])testAssembly.Assembly.GetCustomAttributes(typeof(IApplyToContext), true);
				}
			}
			if (list != null && list.Count > 0)
			{
				testCommand = new ApplyChangesToContextCommand(testCommand, list);
			}
			return testCommand;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public static TestCommand MakeOneTimeTearDownCommand(TestSuite suite, List<SetUpTearDownItem> setUpTearDownItems, List<TestActionItem> actions)
		{
			TestCommand testCommand = new OneTimeTearDownCommand(suite, setUpTearDownItems, actions);
			if (suite.TestType == "Theory")
			{
				testCommand = new TheoryResultCommand(testCommand);
			}
			return testCommand;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public static TestCommand MakeTestCommand(TestMethod test)
		{
			TestCommand testCommand = new TestMethodCommand(test);
			IWrapTestMethod[] customAttributes = test.Method.GetCustomAttributes<IWrapTestMethod>(true);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				testCommand = customAttributes[i].Wrap(testCommand);
			}
			testCommand = new TestActionCommand(testCommand);
			testCommand = new SetUpTearDownCommand(testCommand);
			IWrapSetUpTearDown[] customAttributes2 = test.Method.GetCustomAttributes<IWrapSetUpTearDown>(true);
			for (int i = 0; i < customAttributes2.Length; i++)
			{
				testCommand = customAttributes2[i].Wrap(testCommand);
			}
			IApplyToContext[] customAttributes3 = test.Method.GetCustomAttributes<IApplyToContext>(true);
			if (customAttributes3.Length != 0)
			{
				testCommand = new ApplyChangesToContextCommand(testCommand, customAttributes3);
			}
			return testCommand;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000DBED File Offset: 0x0000BDED
		public static SkipCommand MakeSkipCommand(Test test)
		{
			return new SkipCommand(test);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		public static List<SetUpTearDownItem> BuildSetUpTearDownList(Type fixtureType, Type setUpType, Type tearDownType)
		{
			MethodInfo[] methodsWithAttribute = Reflect.GetMethodsWithAttribute(fixtureType, setUpType, true);
			MethodInfo[] methodsWithAttribute2 = Reflect.GetMethodsWithAttribute(fixtureType, tearDownType, true);
			List<SetUpTearDownItem> list = new List<SetUpTearDownItem>();
			while (fixtureType != null && !fixtureType.Equals(typeof(object)))
			{
				SetUpTearDownItem setUpTearDownItem = CommandBuilder.BuildNode(fixtureType, methodsWithAttribute, methodsWithAttribute2);
				if (setUpTearDownItem.HasMethods)
				{
					list.Add(setUpTearDownItem);
				}
				fixtureType = fixtureType.GetTypeInfo().BaseType;
			}
			return list;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000DC60 File Offset: 0x0000BE60
		private static SetUpTearDownItem BuildNode(Type fixtureType, IList<MethodInfo> setUpMethods, IList<MethodInfo> tearDownMethods)
		{
			IList<MethodInfo> list = CommandBuilder.SelectMethodsByDeclaringType(fixtureType, setUpMethods);
			List<MethodInfo> list2 = CommandBuilder.SelectMethodsByDeclaringType(fixtureType, tearDownMethods);
			return new SetUpTearDownItem(list, list2);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000DC84 File Offset: 0x0000BE84
		private static List<MethodInfo> SelectMethodsByDeclaringType(Type type, IList<MethodInfo> methods)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.DeclaringType == type)
				{
					list.Add(methodInfo);
				}
			}
			return list;
		}
	}
}
