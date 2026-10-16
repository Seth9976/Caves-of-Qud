using System;
using System.Reflection;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000169 RID: 361
	public interface IAnalyzerAssemblyLoader
	{
		// Token: 0x06000E86 RID: 3718
		Assembly LoadFromPath(string fullPath);

		// Token: 0x06000E87 RID: 3719
		void AddDependencyLocation(string fullPath);
	}
}
