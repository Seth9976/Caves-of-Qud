using System;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D1 RID: 209
	public interface ISuiteBuilder
	{
		// Token: 0x060006BD RID: 1725
		bool CanBuildFrom(ITypeInfo typeInfo);

		// Token: 0x060006BE RID: 1726
		TestSuite BuildFrom(ITypeInfo typeInfo);
	}
}
