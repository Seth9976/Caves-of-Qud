using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000C8 RID: 200
	public interface IFixtureBuilder
	{
		// Token: 0x0600069E RID: 1694
		IEnumerable<TestSuite> BuildFrom(ITypeInfo typeInfo);
	}
}
