using System;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class PairwiseAttribute : CombiningStrategyAttribute
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00004010 File Offset: 0x00002210
		public PairwiseAttribute()
			: base(new PairwiseStrategy(), new ParameterDataSourceProvider())
		{
		}
	}
}
