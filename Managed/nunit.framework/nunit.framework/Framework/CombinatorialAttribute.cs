using System;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class CombinatorialAttribute : CombiningStrategyAttribute
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00003ADA File Offset: 0x00001CDA
		public CombinatorialAttribute()
			: base(new CombinatorialStrategy(), new ParameterDataSourceProvider())
		{
		}
	}
}
