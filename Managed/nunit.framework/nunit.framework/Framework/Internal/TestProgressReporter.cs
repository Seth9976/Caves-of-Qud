using System;
using System.Web.UI;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000085 RID: 133
	public class TestProgressReporter : ITestListener
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public TestProgressReporter(ICallbackEventHandler handler)
		{
			this.handler = handler;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000BBE4 File Offset: 0x00009DE4
		public void TestStarted(ITest test)
		{
			string text = ((test is TestSuite) ? "start-suite" : "start-test");
			ITest parent = TestProgressReporter.GetParent(test);
			try
			{
				string text2 = string.Format("<{0} id=\"{1}\" parentId=\"{2}\" name=\"{3}\" fullname=\"{4}\"/>", new object[]
				{
					text,
					test.Id,
					(parent != null) ? parent.Id : string.Empty,
					TestProgressReporter.FormatAttributeValue(test.Name),
					TestProgressReporter.FormatAttributeValue(test.FullName)
				});
				this.handler.RaiseCallbackEvent(text2);
			}
			catch (Exception ex)
			{
				TestProgressReporter.log.Error("Exception processing " + test.FullName + Env.NewLine + ex.ToString());
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		public void TestFinished(ITestResult result)
		{
			try
			{
				TNode tnode = result.ToXml(false);
				ITest parent = TestProgressReporter.GetParent(result.Test);
				tnode.Attributes.Add("parentId", (parent != null) ? parent.Id : string.Empty);
				this.handler.RaiseCallbackEvent(tnode.OuterXml);
			}
			catch (Exception ex)
			{
				TestProgressReporter.log.Error("Exception processing " + result.FullName + Env.NewLine + ex.ToString());
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public void TestOutput(TestOutput output)
		{
			try
			{
				this.handler.RaiseCallbackEvent(output.ToXml());
			}
			catch (Exception ex)
			{
				TestProgressReporter.log.Error("Exception processing TestOutput event" + Env.NewLine + ex.ToString());
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000BD80 File Offset: 0x00009F80
		private static ITest GetParent(ITest test)
		{
			if (test == null || test.Parent == null)
			{
				return null;
			}
			if (!test.Parent.IsSuite)
			{
				return TestProgressReporter.GetParent(test.Parent);
			}
			return test.Parent;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		private static string FormatAttributeValue(string original)
		{
			return original.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;")
				.Replace("<", "&lt;")
				.Replace(">", "&gt;");
		}

		// Token: 0x040000FF RID: 255
		private static Logger log = InternalTrace.GetLogger("TestProgressReporter");

		// Token: 0x04000100 RID: 256
		private ICallbackEventHandler handler;
	}
}
