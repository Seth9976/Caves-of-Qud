using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200022C RID: 556
	internal interface IMemberNotNullAttributeTarget
	{
		// Token: 0x060015CB RID: 5579
		void AddNotNullMember(string memberName);

		// Token: 0x060015CC RID: 5580
		void AddNotNullMember(ArrayBuilder<string> memberNames);

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060015CD RID: 5581
		ImmutableArray<string> NotNullMembers { get; }

		// Token: 0x060015CE RID: 5582
		void AddNotNullWhenMember(bool sense, string memberName);

		// Token: 0x060015CF RID: 5583
		void AddNotNullWhenMember(bool sense, ArrayBuilder<string> memberNames);

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060015D0 RID: 5584
		ImmutableArray<string> NotNullWhenTrueMembers { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060015D1 RID: 5585
		ImmutableArray<string> NotNullWhenFalseMembers { get; }
	}
}
