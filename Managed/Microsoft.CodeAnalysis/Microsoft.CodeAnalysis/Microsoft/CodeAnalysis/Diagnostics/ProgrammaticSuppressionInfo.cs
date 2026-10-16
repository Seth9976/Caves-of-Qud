using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000352 RID: 850
	[NullableContext(2)]
	[Nullable(0)]
	internal sealed class ProgrammaticSuppressionInfo : IEquatable<ProgrammaticSuppressionInfo>
	{
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x00071C65 File Offset: 0x0006FE65
		[TupleElementNames(new string[] { "Id", "Justification" })]
		[Nullable(new byte[] { 1, 0, 1, 1 })]
		public ImmutableHashSet<ValueTuple<string, LocalizableString>> Suppressions
		{
			[return: TupleElementNames(new string[] { "Id", "Justification" })]
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			get;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00071C6D File Offset: 0x0006FE6D
		internal ProgrammaticSuppressionInfo([TupleElementNames(new string[] { "Id", "Justification" })] [Nullable(new byte[] { 1, 0, 1, 1 })] ImmutableHashSet<ValueTuple<string, LocalizableString>> suppressions)
		{
			this.Suppressions = suppressions;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00071C7C File Offset: 0x0006FE7C
		public bool Equals(ProgrammaticSuppressionInfo other)
		{
			return this == other || (other != null && this.Suppressions.SetEquals(other.Suppressions));
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00071C9A File Offset: 0x0006FE9A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ProgrammaticSuppressionInfo);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00071CA8 File Offset: 0x0006FEA8
		public override int GetHashCode()
		{
			return this.Suppressions.Count;
		}
	}
}
