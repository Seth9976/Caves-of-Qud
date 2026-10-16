using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Commands;

namespace NUnit.Framework.Internal.Execution
{
	// Token: 0x020000A1 RID: 161
	public class CompositeWorkItem : WorkItem
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		// (set) Token: 0x060005FF RID: 1535 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		public List<WorkItem> Children
		{
			get
			{
				return this._children;
			}
			private set
			{
				this._children = value;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		public CompositeWorkItem(TestSuite suite, ITestFilter childFilter)
			: base(suite)
		{
			this._suite = suite;
			this._suiteResult = base.Result as TestSuiteResult;
			this._childFilter = childFilter;
			this._countOrder = 0;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000DD48 File Offset: 0x0000BF48
		protected override void PerformWork()
		{
			this.InitializeSetUpAndTearDownCommands();
			if (!this.CheckForCancellation())
			{
				if (base.Test.RunState == RunState.Explicit && !this._childFilter.IsExplicitMatch(base.Test))
				{
					this.SkipFixture(ResultState.Explicit, this.GetSkipReason(), null);
				}
				else
				{
					switch (base.Test.RunState)
					{
					case RunState.NotRunnable:
						this.SkipFixture(ResultState.NotRunnable, this.GetSkipReason(), this.GetProviderStackTrace());
						break;
					default:
						base.Result.SetResult(ResultState.Success);
						this.CreateChildWorkItems();
						if (this._children.Count > 0)
						{
							this.PerformOneTimeSetUp();
							if (!this.CheckForCancellation())
							{
								switch (base.Result.ResultState.Status)
								{
								case TestStatus.Inconclusive:
								case TestStatus.Skipped:
								case TestStatus.Failed:
									this.SkipChildren(this._suite, base.Result.ResultState.WithSite(FailureSite.Parent), "OneTimeSetUp: " + base.Result.Message);
									break;
								case TestStatus.Passed:
									this.RunChildren();
									return;
								}
							}
							if (base.Context.ExecutionStatus != TestExecutionStatus.AbortRequested)
							{
								this.PerformOneTimeTearDown();
							}
						}
						break;
					case RunState.Skipped:
						this.SkipFixture(ResultState.Skipped, this.GetSkipReason(), null);
						break;
					case RunState.Ignored:
						this.SkipFixture(ResultState.Ignored, this.GetSkipReason(), null);
						break;
					}
				}
			}
			base.WorkItemComplete();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000DEB6 File Offset: 0x0000C0B6
		private bool CheckForCancellation()
		{
			if (base.Context.ExecutionStatus != TestExecutionStatus.Running)
			{
				base.Result.SetResult(ResultState.Cancelled, "Test cancelled by user");
				return true;
			}
			return false;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		private void InitializeSetUpAndTearDownCommands()
		{
			List<SetUpTearDownItem> list = ((this._suite.TypeInfo != null) ? CommandBuilder.BuildSetUpTearDownList(this._suite.TypeInfo.Type, typeof(OneTimeSetUpAttribute), typeof(OneTimeTearDownAttribute)) : new List<SetUpTearDownItem>());
			List<TestActionItem> list2 = new List<TestActionItem>();
			foreach (ITestAction testAction in base.Actions)
			{
				bool flag = (testAction.Targets & ActionTargets.Suite) == ActionTargets.Suite || (testAction.Targets == ActionTargets.Default && !(base.Test is ParameterizedMethodSuite));
				bool flag2 = (testAction.Targets & ActionTargets.Test) == ActionTargets.Test && !(base.Test is ParameterizedMethodSuite);
				if (flag)
				{
					list2.Add(new TestActionItem(testAction));
				}
				if (flag2)
				{
					base.Context.UpstreamActions.Add(testAction);
				}
			}
			this._setupCommand = CommandBuilder.MakeOneTimeSetUpCommand(this._suite, list, list2);
			this._teardownCommand = CommandBuilder.MakeOneTimeTearDownCommand(this._suite, list, list2);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000E004 File Offset: 0x0000C204
		private void PerformOneTimeSetUp()
		{
			try
			{
				this._setupCommand.Execute(base.Context);
				base.Context.UpdateContextFromEnvironment();
			}
			catch (Exception innerException)
			{
				if (innerException is NUnitException || innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				base.Result.RecordException(innerException, FailureSite.SetUp);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000E068 File Offset: 0x0000C268
		private void RunChildren()
		{
			int num = this._children.Count;
			if (num == 0)
			{
				throw new InvalidOperationException("RunChildren called but item has no children");
			}
			this._childTestCountdown = new CountdownEvent(num);
			foreach (WorkItem workItem in this._children)
			{
				if (this.CheckForCancellation())
				{
					break;
				}
				workItem.Completed += this.OnChildCompleted;
				workItem.InitializeContext(new TestExecutionContext(base.Context));
				base.Context.Dispatcher.Dispatch(workItem);
				num--;
			}
			if (num > 0)
			{
				while (num-- > 0)
				{
					this.CountDownChildTest();
				}
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000E130 File Offset: 0x0000C330
		private void CreateChildWorkItems()
		{
			this._children = new List<WorkItem>();
			foreach (ITest test in this._suite.Tests)
			{
				if (this._childFilter.Pass(test))
				{
					WorkItem workItem = WorkItem.CreateWorkItem(test, this._childFilter);
					workItem.WorkerId = base.WorkerId;
					if (workItem.TargetApartment == ApartmentState.Unknown && base.TargetApartment != ApartmentState.Unknown)
					{
						workItem.TargetApartment = base.TargetApartment;
					}
					if (test.Properties.ContainsKey("Order"))
					{
						this._children.Insert(0, workItem);
						this._countOrder++;
					}
					else
					{
						this._children.Add(workItem);
					}
				}
			}
			if (this._countOrder != 0)
			{
				this.SortChildren();
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000E21C File Offset: 0x0000C41C
		private void SortChildren()
		{
			this._children.Sort(0, this._countOrder, new CompositeWorkItem.WorkItemOrderComparer());
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000E235 File Offset: 0x0000C435
		private void SkipFixture(ResultState resultState, string message, string stackTrace)
		{
			base.Result.SetResult(resultState.WithSite(FailureSite.SetUp), message, StackFilter.Filter(stackTrace));
			this.SkipChildren(this._suite, resultState.WithSite(FailureSite.Parent), "OneTimeSetUp: " + message);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000E270 File Offset: 0x0000C470
		private void SkipChildren(TestSuite suite, ResultState resultState, string message)
		{
			foreach (ITest test in suite.Tests)
			{
				Test test2 = (Test)test;
				if (this._childFilter.Pass(test2))
				{
					TestResult testResult = test2.MakeTestResult();
					testResult.SetResult(resultState, message);
					this._suiteResult.AddResult(testResult);
					base.Context.Listener.TestFinished(testResult);
					if (test2.IsSuite)
					{
						this.SkipChildren((TestSuite)test2, resultState, message);
					}
				}
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000E30C File Offset: 0x0000C50C
		private void PerformOneTimeTearDown()
		{
			base.Context.EstablishExecutionEnvironment();
			this._teardownCommand.Execute(base.Context);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000E32B File Offset: 0x0000C52B
		private string GetSkipReason()
		{
			return (string)base.Test.Properties.Get("_SKIPREASON");
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000E347 File Offset: 0x0000C547
		private string GetProviderStackTrace()
		{
			return (string)base.Test.Properties.Get("_PROVIDERSTACKTRACE");
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000E364 File Offset: 0x0000C564
		private void OnChildCompleted(object sender, EventArgs e)
		{
			object completionLock = this._completionLock;
			lock (completionLock)
			{
				WorkItem workItem = sender as WorkItem;
				if (workItem != null)
				{
					workItem.Completed -= this.OnChildCompleted;
					this._suiteResult.AddResult(workItem.Result);
					if (base.Context.StopOnError && workItem.Result.ResultState.Status == TestStatus.Failed)
					{
						base.Context.ExecutionStatus = TestExecutionStatus.StopRequested;
					}
					this.CountDownChildTest();
				}
			}
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000E400 File Offset: 0x0000C600
		private void CountDownChildTest()
		{
			this._childTestCountdown.Signal();
			if (this._childTestCountdown.CurrentCount == 0)
			{
				if (base.Context.ExecutionStatus != TestExecutionStatus.AbortRequested)
				{
					this.PerformOneTimeTearDown();
				}
				using (IEnumerator<ITestResult> enumerator = this._suiteResult.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ResultState == ResultState.Cancelled)
						{
							base.Result.SetResult(ResultState.Cancelled, "Cancelled by user");
							break;
						}
					}
				}
				base.WorkItemComplete();
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		private static bool IsStaticClass(Type type)
		{
			return type.GetTypeInfo().IsAbstract && type.GetTypeInfo().IsSealed;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public override void Cancel(bool force)
		{
			object obj = this.cancelLock;
			lock (obj)
			{
				if (this._children != null)
				{
					foreach (WorkItem workItem in this._children)
					{
						TestExecutionContext context = workItem.Context;
						if (context != null)
						{
							context.ExecutionStatus = (force ? TestExecutionStatus.AbortRequested : TestExecutionStatus.StopRequested);
						}
						if (workItem.State == WorkItemState.Running)
						{
							workItem.Cancel(force);
						}
					}
				}
			}
		}

		// Token: 0x04000129 RID: 297
		private TestSuite _suite;

		// Token: 0x0400012A RID: 298
		private TestSuiteResult _suiteResult;

		// Token: 0x0400012B RID: 299
		private ITestFilter _childFilter;

		// Token: 0x0400012C RID: 300
		private TestCommand _setupCommand;

		// Token: 0x0400012D RID: 301
		private TestCommand _teardownCommand;

		// Token: 0x0400012E RID: 302
		private List<WorkItem> _children;

		// Token: 0x0400012F RID: 303
		private int _countOrder;

		// Token: 0x04000130 RID: 304
		private CountdownEvent _childTestCountdown;

		// Token: 0x04000131 RID: 305
		private object _completionLock = new object();

		// Token: 0x04000132 RID: 306
		private object cancelLock = new object();

		// Token: 0x02000189 RID: 393
		private class WorkItemOrderComparer : IComparer<WorkItem>
		{
			// Token: 0x06000AC5 RID: 2757 RVA: 0x0001A274 File Offset: 0x00018474
			public int Compare(WorkItem x, WorkItem y)
			{
				int num = int.MaxValue;
				int num2 = int.MaxValue;
				if (x.Test.Properties.ContainsKey("Order"))
				{
					num = (int)x.Test.Properties["Order"][0];
				}
				if (y.Test.Properties.ContainsKey("Order"))
				{
					num2 = (int)y.Test.Properties["Order"][0];
				}
				return num.CompareTo(num2);
			}
		}
	}
}
