using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000077 RID: 119
	public abstract class TestResult : ITestResult, IXmlNodeBuilder
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x00009F26 File Offset: 0x00008126
		public TestResult(ITest test)
		{
			this.Test = test;
			this.ResultState = ResultState.Inconclusive;
			this.OutWriter = TextWriter.Synchronized(new StringWriter(this._output));
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00009F61 File Offset: 0x00008161
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00009F69 File Offset: 0x00008169
		public ITest Test { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00009F72 File Offset: 0x00008172
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00009F7A File Offset: 0x0000817A
		public ResultState ResultState
		{
			get
			{
				return this._resultState;
			}
			private set
			{
				this._resultState = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00009F83 File Offset: 0x00008183
		public virtual string Name
		{
			get
			{
				return this.Test.Name;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00009F90 File Offset: 0x00008190
		public virtual string FullName
		{
			get
			{
				return this.Test.FullName;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00009F9D File Offset: 0x0000819D
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00009FA5 File Offset: 0x000081A5
		public double Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = ((value >= 1E-06) ? value : 1E-06);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00009FC5 File Offset: 0x000081C5
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00009FCD File Offset: 0x000081CD
		public DateTime StartTime { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00009FD6 File Offset: 0x000081D6
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00009FDE File Offset: 0x000081DE
		public DateTime EndTime { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00009FE7 File Offset: 0x000081E7
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00009FEF File Offset: 0x000081EF
		public string Message
		{
			get
			{
				return this._message;
			}
			private set
			{
				this._message = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00009FF8 File Offset: 0x000081F8
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000A000 File Offset: 0x00008200
		public virtual string StackTrace
		{
			get
			{
				return this._stackTrace;
			}
			private set
			{
				this._stackTrace = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000A009 File Offset: 0x00008209
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000A011 File Offset: 0x00008211
		public int AssertCount
		{
			get
			{
				return this.InternalAssertCount;
			}
			internal set
			{
				this.InternalAssertCount = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000464 RID: 1124
		public abstract int FailCount { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000465 RID: 1125
		public abstract int PassCount { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000466 RID: 1126
		public abstract int SkipCount { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000467 RID: 1127
		public abstract int InconclusiveCount { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000468 RID: 1128
		public abstract bool HasChildren { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000469 RID: 1129
		public abstract IEnumerable<ITestResult> Children { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000A01A File Offset: 0x0000821A
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000A022 File Offset: 0x00008222
		public TextWriter OutWriter { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000A02B File Offset: 0x0000822B
		public string Output
		{
			get
			{
				return this._output.ToString();
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000A038 File Offset: 0x00008238
		public TNode ToXml(bool recursive)
		{
			return this.AddToXml(new TNode("dummy"), recursive);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000A04C File Offset: 0x0000824C
		public virtual TNode AddToXml(TNode parentNode, bool recursive)
		{
			TNode tnode = this.Test.AddToXml(parentNode, false);
			tnode.AddAttribute("result", this.ResultState.Status.ToString());
			if (this.ResultState.Label != string.Empty)
			{
				tnode.AddAttribute("label", this.ResultState.Label);
			}
			if (this.ResultState.Site != FailureSite.Test)
			{
				tnode.AddAttribute("site", this.ResultState.Site.ToString());
			}
			tnode.AddAttribute("start-time", this.StartTime.ToString("u"));
			tnode.AddAttribute("end-time", this.EndTime.ToString("u"));
			tnode.AddAttribute("duration", this.Duration.ToString("0.000000", NumberFormatInfo.InvariantInfo));
			if (this.Test is TestSuite)
			{
				tnode.AddAttribute("total", (this.PassCount + this.FailCount + this.SkipCount + this.InconclusiveCount).ToString());
				tnode.AddAttribute("passed", this.PassCount.ToString());
				tnode.AddAttribute("failed", this.FailCount.ToString());
				tnode.AddAttribute("inconclusive", this.InconclusiveCount.ToString());
				tnode.AddAttribute("skipped", this.SkipCount.ToString());
			}
			tnode.AddAttribute("asserts", this.AssertCount.ToString());
			TestStatus status = this.ResultState.Status;
			if (status > TestStatus.Passed)
			{
				if (status == TestStatus.Failed)
				{
					this.AddFailureElement(tnode);
				}
			}
			else if (this.Message != null)
			{
				this.AddReasonElement(tnode);
			}
			if (this.Output.Length > 0)
			{
				this.AddOutputElement(tnode);
			}
			if (recursive && this.HasChildren)
			{
				foreach (ITestResult testResult in this.Children)
				{
					((TestResult)testResult).AddToXml(tnode, recursive);
				}
			}
			return tnode;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public void SetResult(ResultState resultState)
		{
			this.SetResult(resultState, null, null);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000A2AF File Offset: 0x000084AF
		public void SetResult(ResultState resultState, string message)
		{
			this.SetResult(resultState, message, null);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000A2BA File Offset: 0x000084BA
		public void SetResult(ResultState resultState, string message, string stackTrace)
		{
			this.ResultState = resultState;
			this.Message = message;
			this.StackTrace = stackTrace;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public void RecordException(Exception ex)
		{
			if (ex is NUnitException)
			{
				ex = ex.InnerException;
			}
			if (ex is ResultStateException)
			{
				this.SetResult(((ResultStateException)ex).ResultState, ex.Message, StackFilter.Filter(ex.StackTrace));
				return;
			}
			if (ex is ThreadAbortException)
			{
				this.SetResult(ResultState.Cancelled, "Test cancelled by user", ex.StackTrace);
				return;
			}
			this.SetResult(ResultState.Error, ExceptionHelper.BuildMessage(ex), ExceptionHelper.BuildStackTrace(ex));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000A354 File Offset: 0x00008554
		public void RecordException(Exception ex, FailureSite site)
		{
			if (ex is NUnitException)
			{
				ex = ex.InnerException;
			}
			if (ex is ResultStateException)
			{
				this.SetResult(((ResultStateException)ex).ResultState.WithSite(site), ex.Message, StackFilter.Filter(ex.StackTrace));
				return;
			}
			if (ex is ThreadAbortException)
			{
				this.SetResult(ResultState.Cancelled.WithSite(site), "Test cancelled by user", ex.StackTrace);
				return;
			}
			this.SetResult(ResultState.Error.WithSite(site), ExceptionHelper.BuildMessage(ex), ExceptionHelper.BuildStackTrace(ex));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public void RecordTearDownException(Exception ex)
		{
			if (ex is NUnitException)
			{
				ex = ex.InnerException;
			}
			ResultState resultState = ((this.ResultState == ResultState.Cancelled) ? ResultState.Cancelled : ResultState.Error);
			if (this.Test.IsSuite)
			{
				resultState = resultState.WithSite(FailureSite.TearDown);
			}
			string text = "TearDown : " + ExceptionHelper.BuildMessage(ex);
			if (this.Message != null)
			{
				text = this.Message + Env.NewLine + text;
			}
			string text2 = "--TearDown" + Env.NewLine + ExceptionHelper.BuildStackTrace(ex);
			if (this.StackTrace != null)
			{
				text2 = this.StackTrace + Env.NewLine + text2;
			}
			this.SetResult(resultState, text, text2);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000A494 File Offset: 0x00008694
		private TNode AddReasonElement(TNode targetNode)
		{
			return targetNode.AddElement("reason").AddElementWithCDATA("message", this.Message);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000A4B4 File Offset: 0x000086B4
		private TNode AddFailureElement(TNode targetNode)
		{
			TNode tnode = targetNode.AddElement("failure");
			if (this.Message != null)
			{
				tnode.AddElementWithCDATA("message", this.Message);
			}
			if (this.StackTrace != null)
			{
				tnode.AddElementWithCDATA("stack-trace", this.StackTrace);
			}
			return tnode;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000A502 File Offset: 0x00008702
		private TNode AddOutputElement(TNode targetNode)
		{
			return targetNode.AddElementWithCDATA("output", this.Output);
		}

		// Token: 0x040000B9 RID: 185
		internal static readonly string CHILD_ERRORS_MESSAGE = "One or more child tests had errors";

		// Token: 0x040000BA RID: 186
		internal static readonly string CHILD_IGNORE_MESSAGE = "One or more child tests were ignored";

		// Token: 0x040000BB RID: 187
		internal const double MIN_DURATION = 1E-06;

		// Token: 0x040000BC RID: 188
		private StringBuilder _output = new StringBuilder();

		// Token: 0x040000BD RID: 189
		private double _duration;

		// Token: 0x040000BE RID: 190
		protected int InternalAssertCount;

		// Token: 0x040000BF RID: 191
		private ResultState _resultState;

		// Token: 0x040000C0 RID: 192
		private string _message;

		// Token: 0x040000C1 RID: 193
		private string _stackTrace;
	}
}
