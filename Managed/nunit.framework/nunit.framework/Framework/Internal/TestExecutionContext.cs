using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Principal;
using System.Threading;
using NUnit.Compatibility;
using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Execution;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200007E RID: 126
	public class TestExecutionContext : LongLivedMarshalByRefObject, ITestExecutionContext
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0000ABD0 File Offset: 0x00008DD0
		public TestExecutionContext()
		{
			this._priorContext = null;
			this.TestCaseTimeout = 0;
			this.UpstreamActions = new List<ITestAction>();
			this._currentCulture = CultureInfo.CurrentCulture;
			this._currentUICulture = CultureInfo.CurrentUICulture;
			this._currentPrincipal = Thread.CurrentPrincipal;
			this.CurrentValueFormatter = (object val) => MsgUtils.DefaultValueFormatter(val);
			this.IsSingleThreaded = false;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000AC54 File Offset: 0x00008E54
		public TestExecutionContext(TestExecutionContext other)
		{
			this._priorContext = other;
			this.CurrentTest = other.CurrentTest;
			this.CurrentResult = other.CurrentResult;
			this.TestObject = other.TestObject;
			this.WorkDirectory = other.WorkDirectory;
			this._listener = other._listener;
			this.StopOnError = other.StopOnError;
			this.TestCaseTimeout = other.TestCaseTimeout;
			this.UpstreamActions = new List<ITestAction>(other.UpstreamActions);
			this._currentCulture = other.CurrentCulture;
			this._currentUICulture = other.CurrentUICulture;
			this._currentPrincipal = other.CurrentPrincipal;
			this.CurrentValueFormatter = other.CurrentValueFormatter;
			this.Dispatcher = other.Dispatcher;
			this.ParallelScope = other.ParallelScope;
			this.IsSingleThreaded = other.IsSingleThreaded;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000AD32 File Offset: 0x00008F32
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000AD4A File Offset: 0x00008F4A
		public static TestExecutionContext CurrentContext
		{
			get
			{
				if (TestExecutionContext._currentContext == null)
				{
					TestExecutionContext._currentContext = new TestExecutionContext();
				}
				return TestExecutionContext._currentContext;
			}
			private set
			{
				TestExecutionContext._currentContext = value;
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000AD52 File Offset: 0x00008F52
		public static TestExecutionContext GetTestExecutionContext()
		{
			return TestExecutionContext._currentContext;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000AD59 File Offset: 0x00008F59
		public static void ClearCurrentContext()
		{
			TestExecutionContext.CurrentContext = null;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000AD61 File Offset: 0x00008F61
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000AD69 File Offset: 0x00008F69
		public Test CurrentTest { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000AD72 File Offset: 0x00008F72
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000AD7A File Offset: 0x00008F7A
		public DateTime StartTime { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000AD83 File Offset: 0x00008F83
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000AD8B File Offset: 0x00008F8B
		public long StartTicks { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000AD94 File Offset: 0x00008F94
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public TestResult CurrentResult
		{
			get
			{
				return this._currentResult;
			}
			set
			{
				this._currentResult = value;
				if (value != null)
				{
					this.OutWriter = value.OutWriter;
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000ADBC File Offset: 0x00008FBC
		public TextWriter OutWriter { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000ADC5 File Offset: 0x00008FC5
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000ADCD File Offset: 0x00008FCD
		public object TestObject { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000ADD6 File Offset: 0x00008FD6
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000ADDE File Offset: 0x00008FDE
		public string WorkDirectory { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000ADE7 File Offset: 0x00008FE7
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000ADEF File Offset: 0x00008FEF
		public bool StopOnError { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0000AE21 File Offset: 0x00009021
		public TestExecutionStatus ExecutionStatus
		{
			get
			{
				if (this._executionStatus == TestExecutionStatus.Running && this._priorContext != null)
				{
					this._executionStatus = this._priorContext.ExecutionStatus;
				}
				return this._executionStatus;
			}
			set
			{
				this._executionStatus = value;
				if (this._priorContext != null)
				{
					this._priorContext.ExecutionStatus = value;
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000AE3E File Offset: 0x0000903E
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0000AE46 File Offset: 0x00009046
		internal ITestListener Listener
		{
			get
			{
				return this._listener;
			}
			set
			{
				this._listener = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000AE4F File Offset: 0x0000904F
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000AE57 File Offset: 0x00009057
		public IWorkItemDispatcher Dispatcher { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000AE60 File Offset: 0x00009060
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000AE68 File Offset: 0x00009068
		public ParallelScope ParallelScope { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000AE71 File Offset: 0x00009071
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000AE79 File Offset: 0x00009079
		public string WorkerId { get; internal set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000AE82 File Offset: 0x00009082
		public Randomizer RandomGenerator
		{
			get
			{
				if (this._randomGenerator == null)
				{
					this._randomGenerator = new Randomizer(this.CurrentTest.Seed);
				}
				return this._randomGenerator;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000AEA8 File Offset: 0x000090A8
		internal int AssertCount
		{
			get
			{
				return this._assertCount;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000AEB0 File Offset: 0x000090B0
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public int TestCaseTimeout { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000AEC1 File Offset: 0x000090C1
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000AEC9 File Offset: 0x000090C9
		public List<ITestAction> UpstreamActions { get; private set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000AED2 File Offset: 0x000090D2
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0000AEDA File Offset: 0x000090DA
		public CultureInfo CurrentCulture
		{
			get
			{
				return this._currentCulture;
			}
			set
			{
				this._currentCulture = value;
				Thread.CurrentThread.CurrentCulture = this._currentCulture;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000AEF3 File Offset: 0x000090F3
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000AEFB File Offset: 0x000090FB
		public CultureInfo CurrentUICulture
		{
			get
			{
				return this._currentUICulture;
			}
			set
			{
				this._currentUICulture = value;
				Thread.CurrentThread.CurrentUICulture = this._currentUICulture;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000AF14 File Offset: 0x00009114
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000AF1C File Offset: 0x0000911C
		public IPrincipal CurrentPrincipal
		{
			get
			{
				return this._currentPrincipal;
			}
			set
			{
				this._currentPrincipal = value;
				Thread.CurrentPrincipal = this._currentPrincipal;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000AF30 File Offset: 0x00009130
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000AF38 File Offset: 0x00009138
		public ValueFormatter CurrentValueFormatter { get; private set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000AF41 File Offset: 0x00009141
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000AF49 File Offset: 0x00009149
		public bool IsSingleThreaded { get; set; }

		// Token: 0x060004EF RID: 1263 RVA: 0x0000AF52 File Offset: 0x00009152
		public void UpdateContextFromEnvironment()
		{
			this._currentCulture = CultureInfo.CurrentCulture;
			this._currentUICulture = CultureInfo.CurrentUICulture;
			this._currentPrincipal = Thread.CurrentPrincipal;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000AF75 File Offset: 0x00009175
		public void EstablishExecutionEnvironment()
		{
			Thread.CurrentThread.CurrentCulture = this._currentCulture;
			Thread.CurrentThread.CurrentUICulture = this._currentUICulture;
			Thread.CurrentPrincipal = this._currentPrincipal;
			TestExecutionContext.CurrentContext = this;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000AFA8 File Offset: 0x000091A8
		public void IncrementAssertCount()
		{
			Interlocked.Increment(ref this._assertCount);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000AFB6 File Offset: 0x000091B6
		public void IncrementAssertCount(int count)
		{
			while (count-- > 0)
			{
				Interlocked.Increment(ref this._assertCount);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000AFCF File Offset: 0x000091CF
		public void AddFormatter(ValueFormatterFactory formatterFactory)
		{
			this.CurrentValueFormatter = formatterFactory(this.CurrentValueFormatter);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000020A6 File Offset: 0x000002A6
		[SecurityCritical]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x040000D8 RID: 216
		private TestExecutionContext _priorContext;

		// Token: 0x040000D9 RID: 217
		private TestExecutionStatus _executionStatus;

		// Token: 0x040000DA RID: 218
		private ITestListener _listener = TestListener.NULL;

		// Token: 0x040000DB RID: 219
		private int _assertCount;

		// Token: 0x040000DC RID: 220
		private Randomizer _randomGenerator;

		// Token: 0x040000DD RID: 221
		private CultureInfo _currentCulture;

		// Token: 0x040000DE RID: 222
		private CultureInfo _currentUICulture;

		// Token: 0x040000DF RID: 223
		private TestResult _currentResult;

		// Token: 0x040000E0 RID: 224
		private IPrincipal _currentPrincipal;

		// Token: 0x040000E1 RID: 225
		[ThreadStatic]
		private static TestExecutionContext _currentContext;
	}
}
