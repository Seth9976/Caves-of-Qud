using System;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework
{
	// Token: 0x02000032 RID: 50
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class SequentialAttribute : CombiningStrategyAttribute
	{
		// Token: 0x06000186 RID: 390 RVA: 0x00004A36 File Offset: 0x00002C36
		public SequentialAttribute()
			: base(new SequentialStrategy(), new ParameterDataSourceProvider())
		{
		}
	}
}
