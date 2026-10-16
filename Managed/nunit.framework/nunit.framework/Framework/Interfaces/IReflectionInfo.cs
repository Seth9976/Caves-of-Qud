using System;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000CF RID: 207
	public interface IReflectionInfo
	{
		// Token: 0x060006BA RID: 1722
		T[] GetCustomAttributes<T>(bool inherit) where T : class;

		// Token: 0x060006BB RID: 1723
		bool IsDefined<T>(bool inherit);
	}
}
