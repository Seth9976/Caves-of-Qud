using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class TheoryAttribute : CombiningStrategyAttribute, ITestBuilder, IImplyFixture
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00005DB6 File Offset: 0x00003FB6
		public TheoryAttribute()
			: base(new CombinatorialStrategy(), new ParameterDataProvider(new IParameterDataProvider[]
			{
				new DatapointProvider(),
				new ParameterDataSourceProvider()
			}))
		{
		}
	}
}
