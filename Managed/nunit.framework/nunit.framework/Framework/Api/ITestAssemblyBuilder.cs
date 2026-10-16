using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Api
{
	// Token: 0x02000152 RID: 338
	public interface ITestAssemblyBuilder
	{
		// Token: 0x060009EF RID: 2543
		ITest Build(Assembly assembly, IDictionary<string, object> options);

		// Token: 0x060009F0 RID: 2544
		ITest Build(string assemblyName, IDictionary<string, object> options);
	}
}
