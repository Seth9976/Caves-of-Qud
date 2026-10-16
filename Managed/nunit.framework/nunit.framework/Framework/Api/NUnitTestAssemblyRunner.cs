using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;

namespace NUnit.Framework.Api
{
	// Token: 0x02000154 RID: 340
	public class NUnitTestAssemblyRunner : ITestAssemblyRunner
	{
		// Token: 0x060009FD RID: 2557 RVA: 0x000187B9 File Offset: 0x000169B9
		public NUnitTestAssemblyRunner(ITestAssemblyBuilder builder)
		{
			this._builder = builder;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x000187D4 File Offset: 0x000169D4
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x000187DC File Offset: 0x000169DC
		public ITest LoadedTest { get; protected set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000187E5 File Offset: 0x000169E5
		public ITestResult Result
		{
			get
			{
				if (this.TopLevelWorkItem != null)
				{
					return this.TopLevelWorkItem.Result;
				}
				return null;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000187FC File Offset: 0x000169FC
		public bool IsTestLoaded
		{
			get
			{
				return this.LoadedTest != null;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00018807 File Offset: 0x00016A07
		public bool IsTestRunning
		{
			get
			{
				return this.TopLevelWorkItem != null && this.TopLevelWorkItem.State == WorkItemState.Running;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00018821 File Offset: 0x00016A21
		public bool IsTestComplete
		{
			get
			{
				return this.TopLevelWorkItem != null && this.TopLevelWorkItem.State == WorkItemState.Complete;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0001883B File Offset: 0x00016A3B
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00018843 File Offset: 0x00016A43
		protected IDictionary<string, object> Settings { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0001884C File Offset: 0x00016A4C
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x00018854 File Offset: 0x00016A54
		private WorkItem TopLevelWorkItem { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001885D File Offset: 0x00016A5D
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x00018865 File Offset: 0x00016A65
		private TestExecutionContext Context { get; set; }

		// Token: 0x06000A0A RID: 2570 RVA: 0x00018870 File Offset: 0x00016A70
		public ITest Load(string assemblyName, IDictionary<string, object> settings)
		{
			this.Settings = settings;
			if (settings.ContainsKey("RandomSeed"))
			{
				Randomizer.InitialSeed = (int)settings["RandomSeed"];
			}
			return this.LoadedTest = this._builder.Build(assemblyName, settings);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000188BC File Offset: 0x00016ABC
		public ITest Load(Assembly assembly, IDictionary<string, object> settings)
		{
			this.Settings = settings;
			if (settings.ContainsKey("RandomSeed"))
			{
				Randomizer.InitialSeed = (int)settings["RandomSeed"];
			}
			return this.LoadedTest = this._builder.Build(assembly, settings);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00018908 File Offset: 0x00016B08
		public int CountTestCases(ITestFilter filter)
		{
			if (this.LoadedTest == null)
			{
				throw new InvalidOperationException("The CountTestCases method was called but no test has been loaded");
			}
			return this.CountTestCases(this.LoadedTest, filter);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001892A File Offset: 0x00016B2A
		public ITestResult Run(ITestListener listener, ITestFilter filter)
		{
			this.RunAsync(listener, filter);
			this.WaitForCompletion(-1);
			return this.Result;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00018944 File Offset: 0x00016B44
		public void RunAsync(ITestListener listener, ITestFilter filter)
		{
			NUnitTestAssemblyRunner.log.Info("Running tests");
			if (this.LoadedTest == null)
			{
				throw new InvalidOperationException("The Run method was called but no test has been loaded");
			}
			this._runComplete.Reset();
			this.CreateTestExecutionContext(listener);
			this.TopLevelWorkItem = WorkItem.CreateWorkItem(this.LoadedTest, filter);
			this.TopLevelWorkItem.InitializeContext(this.Context);
			this.TopLevelWorkItem.Completed += this.OnRunCompleted;
			this.StartRun(listener);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000189C7 File Offset: 0x00016BC7
		public bool WaitForCompletion(int timeout)
		{
			return this._runComplete.WaitOne(timeout, false);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000189D6 File Offset: 0x00016BD6
		public void StopRun(bool force)
		{
			if (this.IsTestRunning)
			{
				this.Context.ExecutionStatus = (force ? TestExecutionStatus.AbortRequested : TestExecutionStatus.StopRequested);
				this.Context.Dispatcher.CancelRun(force);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00018A04 File Offset: 0x00016C04
		private void StartRun(ITestListener listener)
		{
			this._savedOut = Console.Out;
			this._savedErr = Console.Error;
			Console.SetOut(new EventListenerTextWriter("Out", Console.Out));
			Console.SetError(new EventListenerTextWriter("Error", Console.Error));
			if (!Debugger.IsAttached && this.Settings.ContainsKey("DebugTests") && (bool)this.Settings["DebugTests"])
			{
				Debugger.Launch();
			}
			this.Context.Dispatcher.Dispatch(this.TopLevelWorkItem);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00018A9C File Offset: 0x00016C9C
		private void CreateTestExecutionContext(ITestListener listener)
		{
			this.Context = new TestExecutionContext();
			if (this.Settings.ContainsKey("DefaultTimeout"))
			{
				this.Context.TestCaseTimeout = (int)this.Settings["DefaultTimeout"];
			}
			if (this.Settings.ContainsKey("StopOnError"))
			{
				this.Context.StopOnError = (bool)this.Settings["StopOnError"];
			}
			if (this.Settings.ContainsKey("WorkDirectory"))
			{
				this.Context.WorkDirectory = (string)this.Settings["WorkDirectory"];
			}
			else
			{
				this.Context.WorkDirectory = Env.DefaultWorkDirectory;
			}
			this.Context.Listener = listener;
			this.Context.Dispatcher = new SimpleWorkItemDispatcher();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00018B78 File Offset: 0x00016D78
		private void OnRunCompleted(object sender, EventArgs e)
		{
			Console.SetOut(this._savedOut);
			Console.SetError(this._savedErr);
			this._runComplete.Set();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00018B9C File Offset: 0x00016D9C
		private int CountTestCases(ITest test, ITestFilter filter)
		{
			if (!test.IsSuite)
			{
				return 1;
			}
			int num = 0;
			foreach (ITest test2 in test.Tests)
			{
				if (filter.Pass(test2))
				{
					num += this.CountTestCases(test2, filter);
				}
			}
			return num;
		}

		// Token: 0x04000219 RID: 537
		private static Logger log = InternalTrace.GetLogger("DefaultTestAssemblyRunner");

		// Token: 0x0400021A RID: 538
		private ITestAssemblyBuilder _builder;

		// Token: 0x0400021B RID: 539
		private ManualResetEvent _runComplete = new ManualResetEvent(false);

		// Token: 0x0400021C RID: 540
		private TextWriter _savedOut;

		// Token: 0x0400021D RID: 541
		private TextWriter _savedErr;
	}
}
