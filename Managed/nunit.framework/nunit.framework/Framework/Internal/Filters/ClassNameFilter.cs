using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Filters
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public class ClassNameFilter : ValueMatchFilter
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0000D425 File Offset: 0x0000B625
		public ClassNameFilter(string expectedValue)
			: base(expectedValue)
		{
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000D42E File Offset: 0x0000B62E
		public override bool Match(ITest test)
		{
			return test.IsSuite && !(test is ParameterizedMethodSuite) && test.ClassName != null && base.Match(test.ClassName);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000D456 File Offset: 0x0000B656
		protected override string ElementName
		{
			get
			{
				return "class";
			}
		}
	}
}
