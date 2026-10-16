using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E2 RID: 738
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ChangedText : SourceText
	{
		// Token: 0x06001F8A RID: 8074 RVA: 0x000610A4 File Offset: 0x0005F2A4
		public ChangedText(SourceText oldText, SourceText newText, [Nullable(0)] ImmutableArray<TextChangeRange> changeRanges)
		{
			SourceHashAlgorithm checksumAlgorithm = oldText.ChecksumAlgorithm;
			base..ctor(default(ImmutableArray<byte>), checksumAlgorithm, null);
			ChangedText.RequiresChangeRangesAreValid(oldText, newText, changeRanges);
			this._newText = newText;
			WeakReference<SourceText> weakReference = new WeakReference<SourceText>(oldText);
			ChangedText changedText = oldText as ChangedText;
			this._info = new ChangedText.ChangeInfo(changeRanges, weakReference, (changedText != null) ? changedText._info : null);
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000610FC File Offset: 0x0005F2FC
		private static void RequiresChangeRangesAreValid(SourceText oldText, SourceText newText, [Nullable(0)] ImmutableArray<TextChangeRange> changeRanges)
		{
			int num = 0;
			foreach (TextChangeRange textChangeRange in changeRanges)
			{
				num += textChangeRange.NewLength - textChangeRange.Span.Length;
			}
			if (oldText.Length + num != newText.Length)
			{
				throw new InvalidOperationException("Delta length difference of change ranges didn't match before/after text length.");
			}
			int num2 = 0;
			foreach (TextChangeRange textChangeRange2 in changeRanges)
			{
				if (textChangeRange2.Span.Start < num2)
				{
					throw new InvalidOperationException("Change preceded current position in oldText");
				}
				if (textChangeRange2.Span.Start > oldText.Length)
				{
					throw new InvalidOperationException("Change start was after the end of oldText");
				}
				if (textChangeRange2.Span.End > oldText.Length)
				{
					throw new InvalidOperationException("Change end was after the end of oldText");
				}
				num2 = textChangeRange2.Span.End;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001F8C RID: 8076 RVA: 0x000611F2 File Offset: 0x0005F3F2
		[Nullable(2)]
		public override Encoding Encoding
		{
			[NullableContext(2)]
			get
			{
				return this._newText.Encoding;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x000611FF File Offset: 0x0005F3FF
		public IEnumerable<TextChangeRange> Changes
		{
			get
			{
				return this._info.ChangeRanges;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001F8E RID: 8078 RVA: 0x00061211 File Offset: 0x0005F411
		public override int Length
		{
			get
			{
				return this._newText.Length;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0006121E File Offset: 0x0005F41E
		internal override int StorageSize
		{
			get
			{
				return this._newText.StorageSize;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0006122B File Offset: 0x0005F42B
		[Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<SourceText> Segments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._newText.Segments;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x00061238 File Offset: 0x0005F438
		internal override SourceText StorageKey
		{
			get
			{
				return this._newText.StorageKey;
			}
		}

		// Token: 0x17000815 RID: 2069
		public override char this[int position]
		{
			get
			{
				return this._newText[position];
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00061253 File Offset: 0x0005F453
		public override string ToString(TextSpan span)
		{
			return this._newText.ToString(span);
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x00061261 File Offset: 0x0005F461
		public override SourceText GetSubText(TextSpan span)
		{
			return this._newText.GetSubText(span);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0006126F File Offset: 0x0005F46F
		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			this._newText.CopyTo(sourceIndex, destination, destinationIndex, count);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00061284 File Offset: 0x0005F484
		public override SourceText WithChanges(IEnumerable<TextChange> changes)
		{
			ChangedText changedText = this._newText.WithChanges(changes) as ChangedText;
			if (changedText != null)
			{
				return new ChangedText(this, changedText._newText, changedText._info.ChangeRanges);
			}
			return this;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000612C0 File Offset: 0x0005F4C0
		public override IReadOnlyList<TextChangeRange> GetChangeRanges(SourceText oldText)
		{
			if (oldText == null)
			{
				throw new ArgumentNullException("oldText");
			}
			if (this == oldText)
			{
				return TextChangeRange.NoChanges;
			}
			SourceText sourceText;
			if (this._info.WeakOldText.TryGetTarget(ref sourceText) && sourceText == oldText)
			{
				return this._info.ChangeRanges;
			}
			if (this.IsChangedFrom(oldText))
			{
				IReadOnlyList<ImmutableArray<TextChangeRange>> changesBetween = ChangedText.GetChangesBetween(oldText, this);
				if (changesBetween.Count > 1)
				{
					return ChangedText.Merge(changesBetween);
				}
			}
			if (sourceText != null && sourceText.GetChangeRanges(oldText).Count == 0)
			{
				return this._info.ChangeRanges;
			}
			return ImmutableArray.Create<TextChangeRange>(new TextChangeRange(new TextSpan(0, oldText.Length), this._newText.Length));
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0006137C File Offset: 0x0005F57C
		private bool IsChangedFrom(SourceText oldText)
		{
			for (ChangedText.ChangeInfo changeInfo = this._info; changeInfo != null; changeInfo = changeInfo.Previous)
			{
				SourceText sourceText;
				if (changeInfo.WeakOldText.TryGetTarget(ref sourceText) && sourceText == oldText)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x000613B4 File Offset: 0x0005F5B4
		[return: Nullable(new byte[] { 1, 0 })]
		private static IReadOnlyList<ImmutableArray<TextChangeRange>> GetChangesBetween(SourceText oldText, ChangedText newText)
		{
			List<ImmutableArray<TextChangeRange>> list = new List<ImmutableArray<TextChangeRange>>();
			ChangedText.ChangeInfo changeInfo = newText._info;
			list.Add(changeInfo.ChangeRanges);
			while (changeInfo != null)
			{
				SourceText sourceText;
				changeInfo.WeakOldText.TryGetTarget(ref sourceText);
				if (sourceText == oldText)
				{
					return list;
				}
				changeInfo = changeInfo.Previous;
				if (changeInfo != null)
				{
					list.Insert(0, changeInfo.ChangeRanges);
				}
			}
			list.Clear();
			return list;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00061414 File Offset: 0x0005F614
		[NullableContext(0)]
		private static ImmutableArray<TextChangeRange> Merge([Nullable(new byte[] { 1, 0 })] IReadOnlyList<ImmutableArray<TextChangeRange>> changeSets)
		{
			ImmutableArray<TextChangeRange> immutableArray = changeSets[0];
			for (int i = 1; i < changeSets.Count; i++)
			{
				immutableArray = TextChangeRangeExtensions.Merge(immutableArray, changeSets[i]);
			}
			return immutableArray;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0006144C File Offset: 0x0005F64C
		protected override TextLineCollection GetLinesCore()
		{
			SourceText sourceText;
			TextLineCollection textLineCollection;
			if (!this._info.WeakOldText.TryGetTarget(ref sourceText) || !sourceText.TryGetLines(out textLineCollection))
			{
				return base.GetLinesCore();
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			instance.Add(0);
			int num = 0;
			int num2 = 0;
			bool flag = false;
			foreach (TextChangeRange textChangeRange in this._info.ChangeRanges)
			{
				if (textChangeRange.Span.Start > num)
				{
					if (flag && this._newText[num + num2] == '\n')
					{
						instance.RemoveLast();
					}
					LinePositionSpan linePositionSpan = textLineCollection.GetLinePositionSpan(TextSpan.FromBounds(num, textChangeRange.Span.Start));
					for (int i = linePositionSpan.Start.Line + 1; i <= linePositionSpan.End.Line; i++)
					{
						instance.Add(textLineCollection[i].Start + num2);
					}
					flag = sourceText[textChangeRange.Span.Start - 1] == '\r';
					if (flag && textChangeRange.Span.Start < sourceText.Length && sourceText[textChangeRange.Span.Start] == '\n')
					{
						instance.Add(textChangeRange.Span.Start + num2);
					}
				}
				if (textChangeRange.NewLength > 0)
				{
					int num3 = textChangeRange.Span.Start + num2;
					SourceText subText = this.GetSubText(new TextSpan(num3, textChangeRange.NewLength));
					if (flag && subText[0] == '\n')
					{
						instance.RemoveLast();
					}
					for (int j = 1; j < subText.Lines.Count; j++)
					{
						instance.Add(num3 + subText.Lines[j].Start);
					}
					flag = subText[textChangeRange.NewLength - 1] == '\r';
				}
				num = textChangeRange.Span.End;
				num2 += textChangeRange.NewLength - textChangeRange.Span.Length;
			}
			if (num < sourceText.Length)
			{
				if (flag && this._newText[num + num2] == '\n')
				{
					instance.RemoveLast();
				}
				LinePositionSpan linePositionSpan2 = textLineCollection.GetLinePositionSpan(TextSpan.FromBounds(num, sourceText.Length));
				for (int k = linePositionSpan2.Start.Line + 1; k <= linePositionSpan2.End.Line; k++)
				{
					instance.Add(textLineCollection[k].Start + num2);
				}
			}
			return new SourceText.LineInfo(this, instance.ToArrayAndFree());
		}

		// Token: 0x04000DE3 RID: 3555
		private readonly SourceText _newText;

		// Token: 0x04000DE4 RID: 3556
		private readonly ChangedText.ChangeInfo _info;

		// Token: 0x02000679 RID: 1657
		[NullableContext(0)]
		private class ChangeInfo
		{
			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x0600392A RID: 14634 RVA: 0x0009EE6C File Offset: 0x0009D06C
			public ImmutableArray<TextChangeRange> ChangeRanges { get; }

			// Token: 0x17000FD6 RID: 4054
			// (get) Token: 0x0600392B RID: 14635 RVA: 0x0009EE74 File Offset: 0x0009D074
			[Nullable(1)]
			public WeakReference<SourceText> WeakOldText
			{
				[NullableContext(1)]
				get;
			}

			// Token: 0x17000FD7 RID: 4055
			// (get) Token: 0x0600392C RID: 14636 RVA: 0x0009EE7C File Offset: 0x0009D07C
			// (set) Token: 0x0600392D RID: 14637 RVA: 0x0009EE84 File Offset: 0x0009D084
			[Nullable(2)]
			public ChangedText.ChangeInfo Previous
			{
				[NullableContext(2)]
				get;
				[NullableContext(2)]
				private set;
			}

			// Token: 0x0600392E RID: 14638 RVA: 0x0009EE8D File Offset: 0x0009D08D
			public ChangeInfo(ImmutableArray<TextChangeRange> changeRanges, [Nullable(1)] WeakReference<SourceText> weakOldText, [Nullable(2)] ChangedText.ChangeInfo previous)
			{
				this.ChangeRanges = changeRanges;
				this.WeakOldText = weakOldText;
				this.Previous = previous;
				this.Clean();
			}

			// Token: 0x0600392F RID: 14639 RVA: 0x0009EEB0 File Offset: 0x0009D0B0
			private void Clean()
			{
				ChangedText.ChangeInfo changeInfo = this;
				for (ChangedText.ChangeInfo changeInfo2 = this; changeInfo2 != null; changeInfo2 = changeInfo2.Previous)
				{
					SourceText sourceText;
					if (changeInfo2.WeakOldText.TryGetTarget(ref sourceText))
					{
						changeInfo = changeInfo2;
					}
				}
				while (changeInfo != null)
				{
					ChangedText.ChangeInfo previous = changeInfo.Previous;
					changeInfo.Previous = null;
					changeInfo = previous;
				}
			}
		}

		// Token: 0x0200067A RID: 1658
		[NullableContext(0)]
		internal static class TestAccessor
		{
			// Token: 0x06003930 RID: 14640 RVA: 0x0009EEF3 File Offset: 0x0009D0F3
			public static ImmutableArray<TextChangeRange> Merge(ImmutableArray<TextChangeRange> oldChanges, ImmutableArray<TextChangeRange> newChanges)
			{
				return TextChangeRangeExtensions.Merge(oldChanges, newChanges);
			}
		}
	}
}
