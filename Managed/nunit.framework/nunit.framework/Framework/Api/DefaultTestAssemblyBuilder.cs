using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework.Api
{
	// Token: 0x02000150 RID: 336
	public class DefaultTestAssemblyBuilder : ITestAssemblyBuilder
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x00017C18 File Offset: 0x00015E18
		public DefaultTestAssemblyBuilder()
		{
			this._defaultSuiteBuilder = new DefaultSuiteBuilder();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00017C2C File Offset: 0x00015E2C
		public ITest Build(Assembly assembly, IDictionary<string, object> options)
		{
			DefaultTestAssemblyBuilder.log.Debug("Loading {0} in AppDomain {1}", new object[]
			{
				assembly.FullName,
				AppDomain.CurrentDomain.FriendlyName
			});
			string assemblyPath = AssemblyHelper.GetAssemblyPath(assembly);
			return this.Build(assembly, assemblyPath, options);
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00017C74 File Offset: 0x00015E74
		public ITest Build(string assemblyName, IDictionary<string, object> options)
		{
			DefaultTestAssemblyBuilder.log.Debug("Loading {0} in AppDomain {1}", new object[]
			{
				assemblyName,
				AppDomain.CurrentDomain.FriendlyName
			});
			TestSuite testSuite = null;
			try
			{
				Assembly assembly = AssemblyHelper.Load(assemblyName);
				testSuite = this.Build(assembly, assemblyName, options);
			}
			catch (Exception ex)
			{
				testSuite = new TestAssembly(assemblyName);
				testSuite.RunState = RunState.NotRunnable;
				testSuite.Properties.Set("_SKIPREASON", ex.Message);
			}
			return testSuite;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00017CF4 File Offset: 0x00015EF4
		private TestSuite Build(Assembly assembly, string assemblyPath, IDictionary<string, object> options)
		{
			TestSuite testSuite = null;
			try
			{
				if (options.ContainsKey("DefaultTestNamePattern"))
				{
					TestNameGenerator.DefaultTestNamePattern = options["DefaultTestNamePattern"] as string;
				}
				if (options.ContainsKey("TestParameters"))
				{
					string text = options["TestParameters"] as string;
					if (!string.IsNullOrEmpty(text))
					{
						foreach (string text2 in text.Split(new char[] { ';' }))
						{
							int num = text2.IndexOf("=");
							if (num > 0 && num < text2.Length - 1)
							{
								string text3 = text2.Substring(0, num);
								string text4 = text2.Substring(num + 1);
								TestContext.Parameters.Add(text3, text4);
							}
						}
					}
				}
				IList list = null;
				if (options.ContainsKey("LOAD"))
				{
					list = options["LOAD"] as IList;
				}
				IList<Test> fixtures = this.GetFixtures(assembly, list);
				testSuite = this.BuildTestAssembly(assembly, assemblyPath, fixtures);
			}
			catch (Exception ex)
			{
				testSuite = new TestAssembly(assemblyPath);
				testSuite.RunState = RunState.NotRunnable;
				testSuite.Properties.Set("_SKIPREASON", ex.Message);
			}
			return testSuite;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00017E34 File Offset: 0x00016034
		private IList<Test> GetFixtures(Assembly assembly, IList names)
		{
			List<Test> list = new List<Test>();
			DefaultTestAssemblyBuilder.log.Debug("Examining assembly for test fixtures");
			IList<Type> candidateFixtureTypes = this.GetCandidateFixtureTypes(assembly, names);
			DefaultTestAssemblyBuilder.log.Debug("Found {0} classes to examine", new object[] { candidateFixtureTypes.Count });
			int num = 0;
			foreach (Type type in candidateFixtureTypes)
			{
				TypeWrapper typeWrapper = new TypeWrapper(type);
				try
				{
					if (this._defaultSuiteBuilder.CanBuildFrom(typeWrapper))
					{
						Test test = this._defaultSuiteBuilder.BuildFrom(typeWrapper);
						list.Add(test);
						num += test.TestCaseCount;
					}
				}
				catch (Exception ex)
				{
					DefaultTestAssemblyBuilder.log.Error(ex.ToString());
				}
			}
			DefaultTestAssemblyBuilder.log.Debug("Found {0} fixtures with {1} test cases", new object[] { list.Count, num });
			return list;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00017F40 File Offset: 0x00016140
		private IList<Type> GetCandidateFixtureTypes(Assembly assembly, IList names)
		{
			Type[] types = assembly.GetTypes();
			if (names == null || names.Count == 0)
			{
				return types;
			}
			List<Type> list = new List<Type>();
			foreach (object obj in names)
			{
				string text = (string)obj;
				Type type = assembly.GetType(text);
				if (type != null)
				{
					list.Add(type);
				}
				else
				{
					string text2 = text + ".";
					foreach (Type type2 in types)
					{
						if (type2.FullName.StartsWith(text2))
						{
							list.Add(type2);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0001800C File Offset: 0x0001620C
		[SecuritySafeCritical]
		private TestSuite BuildTestAssembly(Assembly assembly, string assemblyName, IList<Test> fixtures)
		{
			TestSuite testSuite = new TestAssembly(assembly, assemblyName);
			if (fixtures.Count == 0)
			{
				testSuite.RunState = RunState.NotRunnable;
				testSuite.Properties.Set("_SKIPREASON", "Has no TestFixtures");
			}
			else
			{
				NamespaceTreeBuilder namespaceTreeBuilder = new NamespaceTreeBuilder(testSuite);
				namespaceTreeBuilder.Add(fixtures);
				testSuite = namespaceTreeBuilder.RootSuite;
			}
			testSuite.ApplyAttributesToTest(assembly);
			testSuite.Properties.Set("_PID", Process.GetCurrentProcess().Id);
			testSuite.Properties.Set("_APPDOMAIN", AppDomain.CurrentDomain.FriendlyName);
			testSuite.Sort();
			return testSuite;
		}

		// Token: 0x04000210 RID: 528
		private static Logger log = InternalTrace.GetLogger(typeof(DefaultTestAssemblyBuilder));

		// Token: 0x04000211 RID: 529
		private ISuiteBuilder _defaultSuiteBuilder;
	}
}
