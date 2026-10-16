using System;
using System.Collections;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000CB RID: 203
	public interface IParameterDataProvider
	{
		// Token: 0x060006AC RID: 1708
		bool HasDataFor(IParameterInfo parameter);

		// Token: 0x060006AD RID: 1709
		IEnumerable GetDataFor(IParameterInfo parameter);
	}
}
