using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class SingleThreadedAttribute : NUnitAttribute, IApplyToContext
	{
		// Token: 0x0600014D RID: 333 RVA: 0x00003F90 File Offset: 0x00002190
		public void ApplyToContext(ITestExecutionContext context)
		{
			context.IsSingleThreaded = true;
		}
	}
}
