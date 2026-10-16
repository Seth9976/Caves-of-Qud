using System;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Builders
{
	// Token: 0x020000BB RID: 187
	public class NUnitTestCaseBuilder
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x000105C4 File Offset: 0x0000E7C4
		public NUnitTestCaseBuilder()
		{
			this._nameGenerator = new TestNameGenerator();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000105E4 File Offset: 0x0000E7E4
		public TestMethod BuildTestMethod(IMethodInfo method, Test parentSuite, TestCaseParameters parms)
		{
			TestMethod testMethod = new TestMethod(method, parentSuite)
			{
				Seed = this._randomizer.Next()
			};
			NUnitTestCaseBuilder.CheckTestMethodSignature(testMethod, parms);
			if (parms == null || parms.Arguments == null)
			{
				testMethod.ApplyAttributesToTest(method.MethodInfo);
			}
			string text = testMethod.Method.TypeInfo.FullName;
			if (parentSuite != null)
			{
				text = parentSuite.FullName;
			}
			if (parms != null)
			{
				parms.ApplyToTest(testMethod);
				if (parms.TestName != null)
				{
					testMethod.Name = (parms.TestName.Contains("{") ? new TestNameGenerator(parms.TestName).GetDisplayName(testMethod, parms.OriginalArguments) : parms.TestName);
				}
				else
				{
					testMethod.Name = this._nameGenerator.GetDisplayName(testMethod, parms.OriginalArguments);
				}
			}
			else
			{
				testMethod.Name = this._nameGenerator.GetDisplayName(testMethod, null);
			}
			testMethod.FullName = text + "." + testMethod.Name;
			return testMethod;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000106D4 File Offset: 0x0000E8D4
		private static bool CheckTestMethodSignature(TestMethod testMethod, TestCaseParameters parms)
		{
			if (testMethod.Method.IsAbstract)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Method is abstract");
			}
			if (!testMethod.Method.IsPublic)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Method is not public");
			}
			IParameterInfo[] array = testMethod.Method.GetParameters();
			int num = 0;
			IParameterInfo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (!array2[i].IsOptional)
				{
					num++;
				}
			}
			int num2 = array.Length;
			object[] array3 = null;
			int num3 = 0;
			if (parms != null)
			{
				testMethod.parms = parms;
				testMethod.RunState = parms.RunState;
				array3 = parms.Arguments;
				if (array3 != null)
				{
					num3 = array3.Length;
				}
				if (testMethod.RunState != RunState.Runnable)
				{
					return false;
				}
			}
			ITypeInfo returnType = testMethod.Method.ReturnType;
			if (AsyncInvocationRegion.IsAsyncOperation(testMethod.Method.MethodInfo))
			{
				if (returnType.IsType(typeof(void)))
				{
					return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Async test method must have non-void return type");
				}
				bool flag = returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>);
				if (flag && (parms == null || !parms.HasExpectedResult))
				{
					return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Async test method must have non-generic Task return type when no result is expected");
				}
				if (!flag && parms != null && parms.HasExpectedResult)
				{
					return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Async test method must have Task<T> return type when a result is expected");
				}
			}
			else if (returnType.IsType(typeof(void)))
			{
				if (parms != null && parms.HasExpectedResult)
				{
					return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Method returning void cannot have an expected result");
				}
			}
			else if (parms == null || !parms.HasExpectedResult)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Method has non-void return value, but no result is expected");
			}
			if (num3 > 0 && num2 == 0)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Arguments provided for method with no parameters");
			}
			if (num3 == 0 && num > 0)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "No arguments were provided");
			}
			if (num3 < num)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, string.Format("Not enough arguments provided, provide at least {0} arguments.", num));
			}
			if (num3 > num2)
			{
				return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, string.Format("Too many arguments provided, provide at most {0} arguments.", num2));
			}
			if (testMethod.Method.IsGenericMethodDefinition && array3 != null)
			{
				Type[] typeArguments = new GenericMethodHelper(testMethod.Method.MethodInfo).GetTypeArguments(array3);
				foreach (Type type in typeArguments)
				{
					if (type == null || type == TypeHelper.NonmatchingType)
					{
						return NUnitTestCaseBuilder.MarkAsNotRunnable(testMethod, "Unable to determine type arguments for method");
					}
				}
				testMethod.Method = testMethod.Method.MakeGenericMethod(typeArguments);
				array = testMethod.Method.GetParameters();
			}
			if (array3 != null && array != null)
			{
				TypeHelper.ConvertArgumentList(array3, array);
			}
			return true;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00010950 File Offset: 0x0000EB50
		private static bool MarkAsNotRunnable(TestMethod testMethod, string reason)
		{
			testMethod.RunState = RunState.NotRunnable;
			testMethod.Properties.Set("_SKIPREASON", reason);
			return false;
		}

		// Token: 0x04000165 RID: 357
		private readonly Randomizer _randomizer = Randomizer.CreateRandomizer();

		// Token: 0x04000166 RID: 358
		private readonly TestNameGenerator _nameGenerator;
	}
}
