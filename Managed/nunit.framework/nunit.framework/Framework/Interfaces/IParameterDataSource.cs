using System;
using System.Collections;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000CC RID: 204
	public interface IParameterDataSource
	{
		// Token: 0x060006AE RID: 1710
		IEnumerable GetData(IParameterInfo parameter);
	}
}
