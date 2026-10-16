using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Api
{
	// Token: 0x02000153 RID: 339
	public interface ITestAssemblyRunner
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060009F1 RID: 2545
		ITest LoadedTest { get; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009F2 RID: 2546
		ITestResult Result { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009F3 RID: 2547
		bool IsTestLoaded { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009F4 RID: 2548
		bool IsTestRunning { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060009F5 RID: 2549
		bool IsTestComplete { get; }

		// Token: 0x060009F6 RID: 2550
		ITest Load(string assemblyName, IDictionary<string, object> settings);

		// Token: 0x060009F7 RID: 2551
		ITest Load(Assembly assembly, IDictionary<string, object> settings);

		// Token: 0x060009F8 RID: 2552
		int CountTestCases(ITestFilter filter);

		// Token: 0x060009F9 RID: 2553
		ITestResult Run(ITestListener listener, ITestFilter filter);

		// Token: 0x060009FA RID: 2554
		void RunAsync(ITestListener listener, ITestFilter filter);

		// Token: 0x060009FB RID: 2555
		bool WaitForCompletion(int timeout);

		// Token: 0x060009FC RID: 2556
		void StopRun(bool force);
	}
}
