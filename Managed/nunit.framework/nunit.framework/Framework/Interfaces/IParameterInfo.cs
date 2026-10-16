using System;
using System.Reflection;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000CD RID: 205
	public interface IParameterInfo : IReflectionInfo
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006AF RID: 1711
		bool IsOptional { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006B0 RID: 1712
		IMethodInfo Method { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060006B1 RID: 1713
		ParameterInfo ParameterInfo { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060006B2 RID: 1714
		Type ParameterType { get; }
	}
}
