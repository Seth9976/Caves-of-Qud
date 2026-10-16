using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000082 RID: 130
	public class TestListener : ITestListener
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00003E5D File Offset: 0x0000205D
		public void TestStarted(ITest test)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00003E5D File Offset: 0x0000205D
		public void TestFinished(ITestResult result)
		{
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00003E5D File Offset: 0x0000205D
		public void TestOutput(TestOutput output)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00002050 File Offset: 0x00000250
		private TestListener()
		{
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000B4C1 File Offset: 0x000096C1
		public static ITestListener NULL
		{
			get
			{
				return new TestListener();
			}
		}
	}
}
