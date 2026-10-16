using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A0 RID: 672
	internal abstract class AbstractWarningStateMap<TWarningState> where TWarningState : struct
	{
		// Token: 0x06001C71 RID: 7281 RVA: 0x00053C32 File Offset: 0x00051E32
		[NullableContext(1)]
		protected AbstractWarningStateMap(SyntaxTree syntaxTree)
		{
			this._warningStateMapEntries = this.CreateWarningStateMapEntries(syntaxTree);
		}

		// Token: 0x06001C72 RID: 7282
		[NullableContext(1)]
		[return: Nullable(new byte[] { 1, 0, 0 })]
		protected abstract AbstractWarningStateMap<TWarningState>.WarningStateMapEntry[] CreateWarningStateMapEntries(SyntaxTree syntaxTree);

		// Token: 0x06001C73 RID: 7283 RVA: 0x00053C48 File Offset: 0x00051E48
		public TWarningState GetWarningState([Nullable(1)] string id, int position)
		{
			AbstractWarningStateMap<TWarningState>.WarningStateMapEntry entryAtOrBeforePosition = this.GetEntryAtOrBeforePosition(position);
			TWarningState twarningState;
			if (entryAtOrBeforePosition.SpecificWarningOption.TryGetValue(id, out twarningState))
			{
				return twarningState;
			}
			return entryAtOrBeforePosition.GeneralWarningOption;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00053C78 File Offset: 0x00051E78
		private AbstractWarningStateMap<TWarningState>.WarningStateMapEntry GetEntryAtOrBeforePosition(int position)
		{
			int num = Array.BinarySearch<AbstractWarningStateMap<TWarningState>.WarningStateMapEntry>(this._warningStateMapEntries, new AbstractWarningStateMap<TWarningState>.WarningStateMapEntry(position));
			return this._warningStateMapEntries[(num >= 0) ? num : (~num - 1)];
		}

		// Token: 0x04000D53 RID: 3411
		[Nullable(new byte[] { 1, 0, 0 })]
		private readonly AbstractWarningStateMap<TWarningState>.WarningStateMapEntry[] _warningStateMapEntries;

		// Token: 0x0200064E RID: 1614
		protected readonly struct WarningStateMapEntry : IComparable<AbstractWarningStateMap<TWarningState>.WarningStateMapEntry>
		{
			// Token: 0x0600389C RID: 14492 RVA: 0x0009D432 File Offset: 0x0009B632
			public WarningStateMapEntry(int position)
			{
				this.Position = position;
				this.GeneralWarningOption = default(TWarningState);
				this.SpecificWarningOption = ImmutableDictionary.Create<string, TWarningState>();
			}

			// Token: 0x0600389D RID: 14493 RVA: 0x0009D452 File Offset: 0x0009B652
			public WarningStateMapEntry(int position, TWarningState general, [Nullable(new byte[] { 1, 1, 0 })] ImmutableDictionary<string, TWarningState> specific)
			{
				this.Position = position;
				this.GeneralWarningOption = general;
				this.SpecificWarningOption = specific ?? ImmutableDictionary.Create<string, TWarningState>();
			}

			// Token: 0x0600389E RID: 14494 RVA: 0x0009D472 File Offset: 0x0009B672
			public int CompareTo(AbstractWarningStateMap<TWarningState>.WarningStateMapEntry other)
			{
				return this.Position - other.Position;
			}

			// Token: 0x04001855 RID: 6229
			public readonly int Position;

			// Token: 0x04001856 RID: 6230
			public readonly TWarningState GeneralWarningOption;

			// Token: 0x04001857 RID: 6231
			[Nullable(new byte[] { 1, 1, 0 })]
			public readonly ImmutableDictionary<string, TWarningState> SpecificWarningOption;
		}
	}
}
