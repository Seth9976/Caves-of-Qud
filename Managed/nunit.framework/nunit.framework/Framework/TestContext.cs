using System;
using System.IO;
using System.Reflection;
using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;

namespace NUnit.Framework
{
	// Token: 0x0200005C RID: 92
	public class TestContext
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x00006DAD File Offset: 0x00004FAD
		public TestContext(ITestExecutionContext testExecutionContext)
		{
			this._testExecutionContext = testExecutionContext;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00006DBC File Offset: 0x00004FBC
		public static TestContext CurrentContext
		{
			get
			{
				return new TestContext(TestContext.CurrentTestExecutionContext ?? TestExecutionContext.CurrentContext);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00006DD1 File Offset: 0x00004FD1
		public static TextWriter Out
		{
			get
			{
				return (TestContext.CurrentTestExecutionContext ?? TestExecutionContext.CurrentContext).OutWriter;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public TestContext.TestAdapter Test
		{
			get
			{
				TestContext.TestAdapter testAdapter;
				if ((testAdapter = this._test) == null)
				{
					testAdapter = (this._test = new TestContext.TestAdapter(this._testExecutionContext.CurrentTest));
				}
				return testAdapter;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00006E18 File Offset: 0x00005018
		public TestContext.ResultAdapter Result
		{
			get
			{
				TestContext.ResultAdapter resultAdapter;
				if ((resultAdapter = this._result) == null)
				{
					resultAdapter = (this._result = new TestContext.ResultAdapter(this._testExecutionContext.CurrentResult));
				}
				return resultAdapter;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00006E48 File Offset: 0x00005048
		public string WorkerId
		{
			get
			{
				return this._testExecutionContext.WorkerId;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00006E58 File Offset: 0x00005058
		public string TestDirectory
		{
			get
			{
				Test currentTest = this._testExecutionContext.CurrentTest;
				if (currentTest != null)
				{
					return AssemblyHelper.GetDirectoryName(currentTest.TypeInfo.Assembly);
				}
				return AssemblyHelper.GetDirectoryName(Assembly.GetCallingAssembly());
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00006E8F File Offset: 0x0000508F
		public string WorkDirectory
		{
			get
			{
				return this._testExecutionContext.WorkDirectory;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00006E9C File Offset: 0x0000509C
		public Randomizer Random
		{
			get
			{
				return this._testExecutionContext.RandomGenerator;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00006EA9 File Offset: 0x000050A9
		public static void Write(bool value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00006EB6 File Offset: 0x000050B6
		public static void Write(char value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00006EC3 File Offset: 0x000050C3
		public static void Write(char[] value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00006ED0 File Offset: 0x000050D0
		public static void Write(double value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00006EDD File Offset: 0x000050DD
		public static void Write(int value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00006EEA File Offset: 0x000050EA
		public static void Write(long value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00006EF7 File Offset: 0x000050F7
		public static void Write(decimal value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00006F04 File Offset: 0x00005104
		public static void Write(object value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00006F11 File Offset: 0x00005111
		public static void Write(float value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00006F1E File Offset: 0x0000511E
		public static void Write(string value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00006F2B File Offset: 0x0000512B
		[CLSCompliant(false)]
		public static void Write(uint value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00006F38 File Offset: 0x00005138
		[CLSCompliant(false)]
		public static void Write(ulong value)
		{
			TestContext.Out.Write(value);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00006F45 File Offset: 0x00005145
		public static void Write(string format, object arg1)
		{
			TestContext.Out.Write(format, arg1);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00006F53 File Offset: 0x00005153
		public static void Write(string format, object arg1, object arg2)
		{
			TestContext.Out.Write(format, arg1, arg2);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00006F62 File Offset: 0x00005162
		public static void Write(string format, object arg1, object arg2, object arg3)
		{
			TestContext.Out.Write(format, arg1, arg2, arg3);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00006F72 File Offset: 0x00005172
		public static void Write(string format, params object[] args)
		{
			TestContext.Out.Write(format, args);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00006F80 File Offset: 0x00005180
		public static void WriteLine()
		{
			TestContext.Out.WriteLine();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00006F8C File Offset: 0x0000518C
		public static void WriteLine(bool value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00006F99 File Offset: 0x00005199
		public static void WriteLine(char value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00006FA6 File Offset: 0x000051A6
		public static void WriteLine(char[] value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00006FB3 File Offset: 0x000051B3
		public static void WriteLine(double value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00006FC0 File Offset: 0x000051C0
		public static void WriteLine(int value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00006FCD File Offset: 0x000051CD
		public static void WriteLine(long value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00006FDA File Offset: 0x000051DA
		public static void WriteLine(decimal value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00006FE7 File Offset: 0x000051E7
		public static void WriteLine(object value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00006FF4 File Offset: 0x000051F4
		public static void WriteLine(float value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00007001 File Offset: 0x00005201
		public static void WriteLine(string value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000700E File Offset: 0x0000520E
		[CLSCompliant(false)]
		public static void WriteLine(uint value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000701B File Offset: 0x0000521B
		[CLSCompliant(false)]
		public static void WriteLine(ulong value)
		{
			TestContext.Out.WriteLine(value);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00007028 File Offset: 0x00005228
		public static void WriteLine(string format, object arg1)
		{
			TestContext.Out.WriteLine(format, arg1);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00007036 File Offset: 0x00005236
		public static void WriteLine(string format, object arg1, object arg2)
		{
			TestContext.Out.WriteLine(format, arg1, arg2);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00007045 File Offset: 0x00005245
		public static void WriteLine(string format, object arg1, object arg2, object arg3)
		{
			TestContext.Out.WriteLine(format, arg1, arg2, arg3);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00007055 File Offset: 0x00005255
		public static void WriteLine(string format, params object[] args)
		{
			TestContext.Out.WriteLine(format, args);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00007063 File Offset: 0x00005263
		public static void AddFormatter(ValueFormatterFactory formatterFactory)
		{
			TestExecutionContext.CurrentContext.AddFormatter(formatterFactory);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00007070 File Offset: 0x00005270
		public static void AddFormatter<TSUPPORTED>(ValueFormatter formatter)
		{
			TestContext.AddFormatter((ValueFormatter next) => delegate(object val)
			{
				if (!(val is TSUPPORTED))
				{
					return next(val);
				}
				return formatter(val);
			});
		}

		// Token: 0x0400005F RID: 95
		public static ITestExecutionContext CurrentTestExecutionContext;

		// Token: 0x04000060 RID: 96
		private TestContext.TestAdapter _test;

		// Token: 0x04000061 RID: 97
		private TestContext.ResultAdapter _result;

		// Token: 0x04000062 RID: 98
		public static TextWriter Error = new EventListenerTextWriter("Error", Console.Error);

		// Token: 0x04000063 RID: 99
		public static readonly TextWriter Progress = new EventListenerTextWriter("Progress", Console.Error);

		// Token: 0x04000064 RID: 100
		public static readonly TestParameters Parameters = new TestParameters();

		// Token: 0x04000065 RID: 101
		private ITestExecutionContext _testExecutionContext;

		// Token: 0x0200016C RID: 364
		public class TestAdapter
		{
			// Token: 0x06000A77 RID: 2679 RVA: 0x0001955C File Offset: 0x0001775C
			public TestAdapter(Test test)
			{
				this._test = test;
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001956B File Offset: 0x0001776B
			public string ID
			{
				get
				{
					return this._test.Id;
				}
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00019578 File Offset: 0x00017778
			public string Name
			{
				get
				{
					return this._test.Name;
				}
			}

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00019585 File Offset: 0x00017785
			public string MethodName
			{
				get
				{
					if (!(this._test is TestMethod))
					{
						return null;
					}
					return this._test.Method.Name;
				}
			}

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000195A6 File Offset: 0x000177A6
			public string FullName
			{
				get
				{
					return this._test.FullName;
				}
			}

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x06000A7C RID: 2684 RVA: 0x000195B3 File Offset: 0x000177B3
			public string ClassName
			{
				get
				{
					return this._test.ClassName;
				}
			}

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000195C0 File Offset: 0x000177C0
			public IPropertyBag Properties
			{
				get
				{
					return this._test.Properties;
				}
			}

			// Token: 0x0400024D RID: 589
			private readonly Test _test;
		}

		// Token: 0x0200016D RID: 365
		public class ResultAdapter
		{
			// Token: 0x06000A7E RID: 2686 RVA: 0x000195CD File Offset: 0x000177CD
			public ResultAdapter(TestResult result)
			{
				this._result = result;
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000A7F RID: 2687 RVA: 0x000195DC File Offset: 0x000177DC
			public ResultState Outcome
			{
				get
				{
					return this._result.ResultState;
				}
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000195E9 File Offset: 0x000177E9
			public string Message
			{
				get
				{
					return this._result.Message;
				}
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000195F6 File Offset: 0x000177F6
			public virtual string StackTrace
			{
				get
				{
					return this._result.StackTrace;
				}
			}

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00019603 File Offset: 0x00017803
			public int FailCount
			{
				get
				{
					return this._result.FailCount;
				}
			}

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00019610 File Offset: 0x00017810
			public int PassCount
			{
				get
				{
					return this._result.PassCount;
				}
			}

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001961D File Offset: 0x0001781D
			public int SkipCount
			{
				get
				{
					return this._result.SkipCount;
				}
			}

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0001962A File Offset: 0x0001782A
			public int InconclusiveCount
			{
				get
				{
					return this._result.InconclusiveCount;
				}
			}

			// Token: 0x0400024E RID: 590
			private readonly TestResult _result;
		}
	}
}
