using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000243 RID: 579
	[NullableContext(1)]
	public interface ILocalSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600163C RID: 5692
		ITypeSymbol Type { get; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600163D RID: 5693
		NullableAnnotation NullableAnnotation { get; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600163E RID: 5694
		bool IsConst { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600163F RID: 5695
		bool IsRef { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001640 RID: 5696
		RefKind RefKind { get; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001641 RID: 5697
		bool HasConstantValue { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001642 RID: 5698
		[Nullable(2)]
		object ConstantValue
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001643 RID: 5699
		bool IsFunctionValue { get; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001644 RID: 5700
		bool IsFixed { get; }
	}
}
