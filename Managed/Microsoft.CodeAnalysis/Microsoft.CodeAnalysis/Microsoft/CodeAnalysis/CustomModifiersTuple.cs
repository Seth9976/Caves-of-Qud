using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000238 RID: 568
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CustomModifiersTuple
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x00045546 File Offset: 0x00043746
		private CustomModifiersTuple([Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> typeCustomModifiers, [Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers)
		{
			this._typeCustomModifiers = typeCustomModifiers.NullToEmpty<CustomModifier>();
			this._refCustomModifiers = refCustomModifiers.NullToEmpty<CustomModifier>();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00045566 File Offset: 0x00043766
		public static CustomModifiersTuple Create([Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> typeCustomModifiers, [Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers)
		{
			if (typeCustomModifiers.IsDefaultOrEmpty && refCustomModifiers.IsDefaultOrEmpty)
			{
				return CustomModifiersTuple.Empty;
			}
			return new CustomModifiersTuple(typeCustomModifiers, refCustomModifiers);
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00045587 File Offset: 0x00043787
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<CustomModifier> TypeCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._typeCustomModifiers;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0004558F File Offset: 0x0004378F
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._refCustomModifiers;
			}
		}

		// Token: 0x0400091B RID: 2331
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<CustomModifier> _typeCustomModifiers;

		// Token: 0x0400091C RID: 2332
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<CustomModifier> _refCustomModifiers;

		// Token: 0x0400091D RID: 2333
		public static readonly CustomModifiersTuple Empty = new CustomModifiersTuple(ImmutableArray<CustomModifier>.Empty, ImmutableArray<CustomModifier>.Empty);
	}
}
