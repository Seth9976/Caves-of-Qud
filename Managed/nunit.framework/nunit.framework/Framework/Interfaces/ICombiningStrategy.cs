using System;
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000E5 RID: 229
	public interface ICombiningStrategy
	{
		// Token: 0x0600073E RID: 1854
		IEnumerable<ITestCaseData> GetTestCases(IEnumerable[] sources);
	}
}
