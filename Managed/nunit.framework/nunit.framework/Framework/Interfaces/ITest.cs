using System;
using System.Collections.Generic;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000D2 RID: 210
	public interface ITest : IXmlNodeBuilder
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006BF RID: 1727
		string Id { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006C0 RID: 1728
		string Name { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060006C1 RID: 1729
		string FullName { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060006C2 RID: 1730
		string ClassName { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060006C3 RID: 1731
		string MethodName { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006C4 RID: 1732
		ITypeInfo TypeInfo { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006C5 RID: 1733
		IMethodInfo Method { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006C6 RID: 1734
		RunState RunState { get; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006C7 RID: 1735
		int TestCaseCount { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006C8 RID: 1736
		IPropertyBag Properties { get; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006C9 RID: 1737
		ITest Parent { get; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006CA RID: 1738
		bool IsSuite { get; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006CB RID: 1739
		bool HasChildren { get; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006CC RID: 1740
		IList<ITest> Tests { get; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006CD RID: 1741
		object Fixture { get; }
	}
}
