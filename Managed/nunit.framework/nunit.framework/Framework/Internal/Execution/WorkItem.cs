using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A6 RID: 166
	public abstract class WorkItem
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x0000E780 File Offset: 0x0000C980
		public static WorkItem CreateWorkItem(ITest test, ITestFilter filter)
		{
			TestSuite testSuite = test as TestSuite;
			if (testSuite != null)
			{
				return new CompositeWorkItem(testSuite, filter);
			}
			return new SimpleWorkItem((TestMethod)test, filter);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		public WorkItem(Test test)
		{
			this.Test = test;
			this.Result = test.MakeTestResult();
			this.State = WorkItemState.Ready;
			this.Actions = new List<ITestAction>();
			this.TargetApartment = (this.Test.Properties.ContainsKey("ApartmentState") ? ((ApartmentState)this.Test.Properties.Get("ApartmentState")) : ApartmentState.Unknown);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000E82C File Offset: 0x0000CA2C
		public void InitializeContext(TestExecutionContext context)
		{
			Guard.OperationValid(this.Context == null, "The context has already been initialized");
			this.Context = context;
			if (this.Test is TestAssembly)
			{
				this.Actions.AddRange(ActionsHelper.GetActionsFromAttributeProvider(((TestAssembly)this.Test).Assembly));
				return;
			}
			if (this.Test is ParameterizedMethodSuite)
			{
				this.Actions.AddRange(ActionsHelper.GetActionsFromAttributeProvider(this.Test.Method.MethodInfo));
				return;
			}
			if (this.Test.TypeInfo != null)
			{
				this.Actions.AddRange(ActionsHelper.GetActionsFromTypesAttributes(this.Test.TypeInfo.Type));
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000621 RID: 1569 RVA: 0x0000E8DC File Offset: 0x0000CADC
		// (remove) Token: 0x06000622 RID: 1570 RVA: 0x0000E914 File Offset: 0x0000CB14
		public event EventHandler Completed;

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000E949 File Offset: 0x0000CB49
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000E951 File Offset: 0x0000CB51
		public WorkItemState State { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000E95A File Offset: 0x0000CB5A
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000E962 File Offset: 0x0000CB62
		public Test Test { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000E96B File Offset: 0x0000CB6B
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000E973 File Offset: 0x0000CB73
		public TestExecutionContext Context { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000E97C File Offset: 0x0000CB7C
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0000E984 File Offset: 0x0000CB84
		public string WorkerId { get; internal set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000E98D File Offset: 0x0000CB8D
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0000E995 File Offset: 0x0000CB95
		public List<ITestAction> Actions { get; private set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000E99E File Offset: 0x0000CB9E
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		public TestResult Result { get; protected set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000E9AF File Offset: 0x0000CBAF
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000E9B7 File Offset: 0x0000CBB7
		internal ApartmentState TargetApartment { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		private ApartmentState CurrentApartment { get; set; }

		// Token: 0x06000633 RID: 1587 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		public virtual void Execute()
		{
			int num = this.Context.TestCaseTimeout;
			if (this.Test.Properties.ContainsKey("Timeout"))
			{
				num = (int)this.Test.Properties.Get("Timeout");
			}
			WorkItem.OwnThreadReason ownThreadReason = WorkItem.OwnThreadReason.NotNeeded;
			if (this.Test.RequiresThread)
			{
				ownThreadReason |= WorkItem.OwnThreadReason.RequiresThread;
			}
			if (num > 0 && this.Test is TestMethod)
			{
				ownThreadReason |= WorkItem.OwnThreadReason.Timeout;
			}
			this.CurrentApartment = Thread.CurrentThread.GetApartmentState();
			if (this.CurrentApartment != this.TargetApartment && this.TargetApartment != ApartmentState.Unknown)
			{
				ownThreadReason |= WorkItem.OwnThreadReason.DifferentApartment;
			}
			ownThreadReason = WorkItem.OwnThreadReason.NotNeeded;
			if (ownThreadReason == WorkItem.OwnThreadReason.NotNeeded)
			{
				this.RunTest();
				return;
			}
			if (this.Context.IsSingleThreaded)
			{
				string text = "Test is not runnable in single-threaded context. " + ownThreadReason.ToString();
				WorkItem.log.Error(text);
				this.Result.SetResult(ResultState.NotRunnable, text);
				this.WorkItemComplete();
				return;
			}
			WorkItem.log.Debug("Running test on own thread. " + ownThreadReason.ToString());
			ApartmentState apartmentState = (((ownThreadReason | WorkItem.OwnThreadReason.DifferentApartment) != WorkItem.OwnThreadReason.NotNeeded) ? this.TargetApartment : this.CurrentApartment);
			this.RunTestOnOwnThread(num, apartmentState);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000EB03 File Offset: 0x0000CD03
		private void RunTestOnOwnThread(int timeout, ApartmentState apartment)
		{
			this.thread = new Thread(new ThreadStart(this.RunTest));
			this.thread.SetApartmentState(apartment);
			this.RunThread(timeout);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000EB30 File Offset: 0x0000CD30
		private void RunThread(int timeout)
		{
			this.thread.CurrentCulture = this.Context.CurrentCulture;
			this.thread.CurrentUICulture = this.Context.CurrentUICulture;
			this.thread.Start();
			if (timeout <= 0)
			{
				timeout = -1;
			}
			if (!this.thread.Join(timeout))
			{
				if (Debugger.IsAttached)
				{
					this.thread.Join();
					return;
				}
				object obj = this.threadLock;
				Thread thread;
				lock (obj)
				{
					if (this.thread == null)
					{
						return;
					}
					thread = this.thread;
					this.thread = null;
				}
				if (this.Context.ExecutionStatus == TestExecutionStatus.AbortRequested)
				{
					return;
				}
				WorkItem.log.Debug("Killing thread {0}, which exceeded timeout", new object[] { thread.ManagedThreadId });
				ThreadUtility.Kill(thread);
				thread.Join();
				WorkItem.log.Debug("Changing result from {0} to Timeout Failure", new object[] { this.Result.ResultState });
				this.Result.SetResult(ResultState.Failure, string.Format("Test exceeded Timeout value of {0}ms", timeout));
				this.WorkItemComplete();
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000EC70 File Offset: 0x0000CE70
		private void RunTest()
		{
			this.Context.CurrentTest = this.Test;
			this.Context.CurrentResult = this.Result;
			this.Context.Listener.TestStarted(this.Test);
			this.Context.StartTime = DateTime.UtcNow;
			this.Context.StartTicks = Stopwatch.GetTimestamp();
			this.Context.WorkerId = this.WorkerId;
			this.Context.EstablishExecutionEnvironment();
			this.State = WorkItemState.Running;
			this.PerformWork();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public virtual void Cancel(bool force)
		{
			if (this.Context != null)
			{
				this.Context.ExecutionStatus = (force ? TestExecutionStatus.AbortRequested : TestExecutionStatus.StopRequested);
			}
			if (!force)
			{
				return;
			}
			object obj = this.threadLock;
			Thread thread;
			lock (obj)
			{
				if (this.thread == null)
				{
					return;
				}
				thread = this.thread;
				this.thread = null;
			}
			if (!thread.Join(0))
			{
				WorkItem.log.Debug("Killing thread {0} for cancel", new object[] { thread.ManagedThreadId });
				ThreadUtility.Kill(thread);
				thread.Join();
				WorkItem.log.Debug("Changing result from {0} to Cancelled", new object[] { this.Result.ResultState });
				this.Result.SetResult(ResultState.Cancelled, "Cancelled by user");
				this.WorkItemComplete();
			}
		}

		// Token: 0x06000638 RID: 1592
		protected abstract void PerformWork();

		// Token: 0x06000639 RID: 1593 RVA: 0x0000EDEC File Offset: 0x0000CFEC
		protected void WorkItemComplete()
		{
			this.State = WorkItemState.Complete;
			this.Result.StartTime = this.Context.StartTime;
			this.Result.EndTime = DateTime.UtcNow;
			double num = (double)(Stopwatch.GetTimestamp() - this.Context.StartTicks) / (double)Stopwatch.Frequency;
			this.Result.Duration = num;
			this.Result.AssertCount += this.Context.AssertCount;
			this.Context.Listener.TestFinished(this.Result);
			if (this.Completed != null)
			{
				this.Completed(this, EventArgs.Empty);
			}
			this.Context.TestObject = null;
			this.Test.Fixture = null;
		}

		// Token: 0x04000138 RID: 312
		private static Logger log = InternalTrace.GetLogger("WorkItem");

		// Token: 0x04000142 RID: 322
		private Thread thread;

		// Token: 0x04000143 RID: 323
		private object threadLock = new object();

		// Token: 0x0200018A RID: 394
		[Flags]
		private enum OwnThreadReason
		{
			// Token: 0x0400027D RID: 637
			NotNeeded = 0,
			// Token: 0x0400027E RID: 638
			RequiresThread = 1,
			// Token: 0x0400027F RID: 639
			Timeout = 2,
			// Token: 0x04000280 RID: 640
			DifferentApartment = 4
		}
	}
}
