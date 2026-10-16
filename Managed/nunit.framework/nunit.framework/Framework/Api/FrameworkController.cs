using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Web.UI;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Api
{
	// Token: 0x02000151 RID: 337
	public class FrameworkController : LongLivedMarshalByRefObject
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x000180B7 File Offset: 0x000162B7
		public FrameworkController(string assemblyNameOrPath, string idPrefix, IDictionary settings)
		{
			this.Initialize(assemblyNameOrPath, settings);
			this.Builder = new DefaultTestAssemblyBuilder();
			this.Runner = new NUnitTestAssemblyRunner(this.Builder);
			Test.IdPrefix = idPrefix;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000180E9 File Offset: 0x000162E9
		public FrameworkController(Assembly assembly, string idPrefix, IDictionary settings)
			: this(assembly.FullName, idPrefix, settings)
		{
			this._testAssembly = assembly;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00018100 File Offset: 0x00016300
		public FrameworkController(string assemblyNameOrPath, string idPrefix, IDictionary settings, string runnerType, string builderType)
		{
			this.Initialize(assemblyNameOrPath, settings);
			this.Builder = (ITestAssemblyBuilder)Reflect.Construct(Type.GetType(builderType));
			this.Runner = (ITestAssemblyRunner)Reflect.Construct(Type.GetType(runnerType), new object[] { this.Builder });
			Test.IdPrefix = idPrefix ?? "";
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00018167 File Offset: 0x00016367
		public FrameworkController(Assembly assembly, string idPrefix, IDictionary settings, string runnerType, string builderType)
			: this(assembly.FullName, idPrefix, settings, runnerType, builderType)
		{
			this._testAssembly = assembly;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00018184 File Offset: 0x00016384
		[SecuritySafeCritical]
		private void Initialize(string assemblyPath, IDictionary settings)
		{
			this.AssemblyNameOrPath = assemblyPath;
			IDictionary<string, object> dictionary = settings as IDictionary<string, object>;
			IDictionary<string, object> dictionary2;
			if ((dictionary2 = dictionary) == null)
			{
				dictionary2 = settings.Cast<DictionaryEntry>().ToDictionary<DictionaryEntry, string, object>((DictionaryEntry de) => (string)de.Key, (DictionaryEntry de) => de.Value);
			}
			this.Settings = dictionary2;
			if (this.Settings.ContainsKey("InternalTraceLevel"))
			{
				InternalTraceLevel internalTraceLevel = (InternalTraceLevel)Enum.Parse(typeof(InternalTraceLevel), (string)this.Settings["InternalTraceLevel"], true);
				if (this.Settings.ContainsKey("InternalTraceWriter"))
				{
					InternalTrace.Initialize((TextWriter)this.Settings["InternalTraceWriter"], internalTraceLevel);
					return;
				}
				string text = (this.Settings.ContainsKey("WorkDirectory") ? ((string)this.Settings["WorkDirectory"]) : Env.DefaultWorkDirectory);
				string text2 = string.Format("InternalTrace.{0}.{1}.log", Process.GetCurrentProcess().Id, Path.GetFileName(assemblyPath));
				InternalTrace.Initialize(Path.Combine(text, text2), internalTraceLevel);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000182BB File Offset: 0x000164BB
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x000182C3 File Offset: 0x000164C3
		public ITestAssemblyBuilder Builder { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x000182CC File Offset: 0x000164CC
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x000182D4 File Offset: 0x000164D4
		public ITestAssemblyRunner Runner { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000182DD File Offset: 0x000164DD
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x000182E5 File Offset: 0x000164E5
		public string AssemblyNameOrPath { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x000182EE File Offset: 0x000164EE
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x000182F6 File Offset: 0x000164F6
		public Assembly Assembly { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000182FF File Offset: 0x000164FF
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x00018307 File Offset: 0x00016507
		internal IDictionary<string, object> Settings { get; private set; }

		// Token: 0x060009DE RID: 2526 RVA: 0x00018310 File Offset: 0x00016510
		public string LoadTests()
		{
			if (this._testAssembly != null)
			{
				this.Runner.Load(this._testAssembly, this.Settings);
			}
			else
			{
				this.Runner.Load(this.AssemblyNameOrPath, this.Settings);
			}
			return this.Runner.LoadedTest.ToXml(false).OuterXml;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00018373 File Offset: 0x00016573
		public string ExploreTests(string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			if (this.Runner.LoadedTest == null)
			{
				throw new InvalidOperationException("The Explore method was called but no test has been loaded");
			}
			return this.Runner.LoadedTest.ToXml(true).OuterXml;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000183B0 File Offset: 0x000165B0
		public string RunTests(string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			TNode tnode = this.Runner.Run(new TestProgressReporter(null), TestFilter.FromXml(filter)).ToXml(true);
			if (this.Settings != null)
			{
				FrameworkController.InsertSettingsElement(tnode, this.Settings);
			}
			FrameworkController.InsertEnvironmentElement(tnode);
			TestExecutionContext.ClearCurrentContext();
			return tnode.OuterXml;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00018410 File Offset: 0x00016610
		public string RunTests(Action<string> callback, string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			FrameworkController.ActionCallback actionCallback = new FrameworkController.ActionCallback(callback);
			TNode tnode = this.Runner.Run(new TestProgressReporter(actionCallback), TestFilter.FromXml(filter)).ToXml(true);
			if (this.Settings != null)
			{
				FrameworkController.InsertSettingsElement(tnode, this.Settings);
			}
			FrameworkController.InsertEnvironmentElement(tnode);
			TestExecutionContext.ClearCurrentContext();
			return tnode.OuterXml;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00018474 File Offset: 0x00016674
		private void RunAsync(Action<string> callback, string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			FrameworkController.ActionCallback actionCallback = new FrameworkController.ActionCallback(callback);
			this.Runner.RunAsync(new TestProgressReporter(actionCallback), TestFilter.FromXml(filter));
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000184AA File Offset: 0x000166AA
		public void StopRun(bool force)
		{
			this.Runner.StopRun(force);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000184B8 File Offset: 0x000166B8
		public int CountTests(string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			return this.Runner.CountTestCases(TestFilter.FromXml(filter));
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000184D6 File Offset: 0x000166D6
		private void LoadTests(ICallbackEventHandler handler)
		{
			handler.RaiseCallbackEvent(this.LoadTests());
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000184E4 File Offset: 0x000166E4
		private void ExploreTests(ICallbackEventHandler handler, string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			if (this.Runner.LoadedTest == null)
			{
				throw new InvalidOperationException("The Explore method was called but no test has been loaded");
			}
			handler.RaiseCallbackEvent(this.Runner.LoadedTest.ToXml(true).OuterXml);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00018530 File Offset: 0x00016730
		private void RunTests(ICallbackEventHandler handler, string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			TNode tnode = this.Runner.Run(new TestProgressReporter(handler), TestFilter.FromXml(filter)).ToXml(true);
			if (this.Settings != null)
			{
				FrameworkController.InsertSettingsElement(tnode, this.Settings);
			}
			FrameworkController.InsertEnvironmentElement(tnode);
			TestExecutionContext.ClearCurrentContext();
			handler.RaiseCallbackEvent(tnode.OuterXml);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00018593 File Offset: 0x00016793
		private void RunAsync(ICallbackEventHandler handler, string filter)
		{
			Guard.ArgumentNotNull(filter, "filter");
			this.Runner.RunAsync(new TestProgressReporter(handler), TestFilter.FromXml(filter));
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000185B7 File Offset: 0x000167B7
		private void StopRun(ICallbackEventHandler handler, bool force)
		{
			this.StopRun(force);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000185C0 File Offset: 0x000167C0
		private void CountTests(ICallbackEventHandler handler, string filter)
		{
			handler.RaiseCallbackEvent(this.CountTests(filter).ToString());
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000185E4 File Offset: 0x000167E4
		public static TNode InsertEnvironmentElement(TNode targetNode)
		{
			TNode tnode = new TNode("environment");
			targetNode.ChildNodes.Insert(0, tnode);
			tnode.AddAttribute("framework-version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
			tnode.AddAttribute("clr-version", Environment.Version.ToString());
			tnode.AddAttribute("os-version", Environment.OSVersion.ToString());
			tnode.AddAttribute("platform", Environment.OSVersion.Platform.ToString());
			tnode.AddAttribute("cwd", Environment.CurrentDirectory);
			tnode.AddAttribute("machine-name", Environment.MachineName);
			tnode.AddAttribute("user", Environment.UserName);
			tnode.AddAttribute("user-domain", Environment.UserDomainName);
			tnode.AddAttribute("culture", CultureInfo.CurrentCulture.ToString());
			tnode.AddAttribute("uiculture", CultureInfo.CurrentUICulture.ToString());
			tnode.AddAttribute("os-architecture", FrameworkController.GetProcessorArchitecture());
			return tnode;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000186F0 File Offset: 0x000168F0
		private static string GetProcessorArchitecture()
		{
			if (IntPtr.Size != 8)
			{
				return "x86";
			}
			return "x64";
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00018708 File Offset: 0x00016908
		public static TNode InsertSettingsElement(TNode targetNode, IDictionary<string, object> settings)
		{
			TNode tnode = new TNode("settings");
			targetNode.ChildNodes.Insert(0, tnode);
			foreach (string text in settings.Keys)
			{
				FrameworkController.AddSetting(tnode, text, settings[text]);
			}
			return tnode;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00018778 File Offset: 0x00016978
		private static void AddSetting(TNode settingsNode, string name, object value)
		{
			TNode tnode = new TNode("setting");
			tnode.AddAttribute("name", name);
			tnode.AddAttribute("value", value.ToString());
			settingsNode.ChildNodes.Add(tnode);
		}

		// Token: 0x04000212 RID: 530
		private const string LOG_FILE_FORMAT = "InternalTrace.{0}.{1}.log";

		// Token: 0x04000213 RID: 531
		private Assembly _testAssembly;

		// Token: 0x020001BD RID: 445
		private class ActionCallback : ICallbackEventHandler
		{
			// Token: 0x06000B57 RID: 2903 RVA: 0x0001B690 File Offset: 0x00019890
			public ActionCallback(Action<string> callback)
			{
				this._callback = callback;
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x0001B69F File Offset: 0x0001989F
			public string GetCallbackResult()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B59 RID: 2905 RVA: 0x0001B6A6 File Offset: 0x000198A6
			public void RaiseCallbackEvent(string report)
			{
				if (this._callback != null)
				{
					this._callback(report);
				}
			}

			// Token: 0x040002D1 RID: 721
			private Action<string> _callback;
		}

		// Token: 0x020001BE RID: 446
		public abstract class FrameworkControllerAction : LongLivedMarshalByRefObject
		{
		}

		// Token: 0x020001BF RID: 447
		public class LoadTestsAction : FrameworkController.FrameworkControllerAction
		{
			// Token: 0x06000B5B RID: 2907 RVA: 0x0001B6C4 File Offset: 0x000198C4
			public LoadTestsAction(FrameworkController controller, object handler)
			{
				controller.LoadTests((ICallbackEventHandler)handler);
			}
		}

		// Token: 0x020001C0 RID: 448
		public class ExploreTestsAction : FrameworkController.FrameworkControllerAction
		{
			// Token: 0x06000B5C RID: 2908 RVA: 0x0001B6D8 File Offset: 0x000198D8
			public ExploreTestsAction(FrameworkController controller, string filter, object handler)
			{
				controller.ExploreTests((ICallbackEventHandler)handler, filter);
			}
		}

		// Token: 0x020001C1 RID: 449
		public class CountTestsAction : FrameworkController.FrameworkControllerAction
		{
			// Token: 0x06000B5D RID: 2909 RVA: 0x0001B6ED File Offset: 0x000198ED
			public CountTestsAction(FrameworkController controller, string filter, object handler)
			{
				controller.CountTests((ICallbackEventHandler)handler, filter);
			}
		}

		// Token: 0x020001C2 RID: 450
		public class RunTestsAction : FrameworkController.FrameworkControllerAction
		{
			// Token: 0x06000B5E RID: 2910 RVA: 0x0001B702 File Offset: 0x00019902
			public RunTestsAction(FrameworkController controller, string filter, object handler)
			{
				controller.RunTests((ICallbackEventHandler)handler, filter);
			}
		}

		// Token: 0x020001C3 RID: 451
		public class RunAsyncAction : FrameworkController.FrameworkControllerAction
		{
			// Token: 0x06000B5F RID: 2911 RVA: 0x0001B717 File Offset: 0x00019917
			public RunAsyncAction(FrameworkController controller, string filter, object handler)
			{
				controller.RunAsync((ICallbackEventHandler)handler, filter);
			}
		}

		// Token: 0x020001C4 RID: 452
		public class StopRunAction : FrameworkController.FrameworkControllerAction
		{
			// Token: 0x06000B60 RID: 2912 RVA: 0x0001B72C File Offset: 0x0001992C
			public StopRunAction(FrameworkController controller, bool force, object handler)
			{
				controller.StopRun((ICallbackEventHandler)handler, force);
			}
		}
	}
}
