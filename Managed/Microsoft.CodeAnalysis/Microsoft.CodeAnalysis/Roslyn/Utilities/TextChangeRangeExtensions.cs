using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;

namespace Roslyn.Utilities
{
	// Token: 0x02000066 RID: 102
	internal static class TextChangeRangeExtensions
	{
		// Token: 0x0600025F RID: 607 RVA: 0x000081EC File Offset: 0x000063EC
		[NullableContext(1)]
		public static TextChangeRange? Accumulate(this TextChangeRange? accumulatedTextChangeSoFar, IEnumerable<TextChangeRange> changesInNextVersion)
		{
			if (!Enumerable.Any<TextChangeRange>(changesInNextVersion))
			{
				return accumulatedTextChangeSoFar;
			}
			TextChangeRange textChangeRange = TextChangeRange.Collapse(changesInNextVersion);
			if (accumulatedTextChangeSoFar == null)
			{
				return new TextChangeRange?(textChangeRange);
			}
			int num = accumulatedTextChangeSoFar.Value.Span.Start;
			int num2 = accumulatedTextChangeSoFar.Value.Span.End;
			int num3 = accumulatedTextChangeSoFar.Value.Span.Start + accumulatedTextChangeSoFar.Value.NewLength;
			if (textChangeRange.Span.Start < num)
			{
				num = textChangeRange.Span.Start;
			}
			if (num3 > textChangeRange.Span.End)
			{
				num3 = num3 + textChangeRange.NewLength - textChangeRange.Span.Length;
			}
			else
			{
				num2 = num2 + textChangeRange.Span.End - num3;
				num3 = textChangeRange.Span.Start + textChangeRange.NewLength;
			}
			return new TextChangeRange?(new TextChangeRange(TextSpan.FromBounds(num, num2), num3 - num));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000830F File Offset: 0x0000650F
		public static TextChangeRange ToTextChangeRange(this TextChange textChange)
		{
			TextSpan span = textChange.Span;
			string newText = textChange.NewText;
			return new TextChangeRange(span, (newText != null) ? newText.Length : 0);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008330 File Offset: 0x00006530
		public static ImmutableArray<TextChangeRange> Merge(ImmutableArray<TextChangeRange> oldChanges, ImmutableArray<TextChangeRange> newChanges)
		{
			TextChangeRangeExtensions.<>c__DisplayClass2_0 CS$<>8__locals1;
			CS$<>8__locals1.oldChanges = oldChanges;
			CS$<>8__locals1.newChanges = newChanges;
			if (CS$<>8__locals1.oldChanges.IsEmpty)
			{
				throw new ArgumentException("oldChanges");
			}
			if (CS$<>8__locals1.newChanges.IsEmpty)
			{
				throw new ArgumentException("newChanges");
			}
			ArrayBuilder<TextChangeRange> instance = ArrayBuilder<TextChangeRange>.GetInstance();
			CS$<>8__locals1.oldChange = CS$<>8__locals1.oldChanges[0];
			CS$<>8__locals1.newChange = new TextChangeRangeExtensions.UnadjustedNewChange(CS$<>8__locals1.newChanges[0]);
			CS$<>8__locals1.oldIndex = 0;
			CS$<>8__locals1.newIndex = 0;
			int num = 0;
			for (;;)
			{
				if (CS$<>8__locals1.oldChange.Span.Length == 0 && CS$<>8__locals1.oldChange.NewLength == 0)
				{
					if (!TextChangeRangeExtensions.<Merge>g__tryGetNextOldChange|2_0(ref CS$<>8__locals1))
					{
						break;
					}
				}
				else if (CS$<>8__locals1.newChange.SpanLength == 0 && CS$<>8__locals1.newChange.NewLength == 0)
				{
					if (!TextChangeRangeExtensions.<Merge>g__tryGetNextNewChange|2_1(ref CS$<>8__locals1))
					{
						break;
					}
				}
				else if (CS$<>8__locals1.newChange.SpanEnd <= CS$<>8__locals1.oldChange.Span.Start + num)
				{
					TextChangeRangeExtensions.<Merge>g__adjustAndAddNewChange|2_3(instance, num, CS$<>8__locals1.newChange);
					if (!TextChangeRangeExtensions.<Merge>g__tryGetNextNewChange|2_1(ref CS$<>8__locals1))
					{
						break;
					}
				}
				else if (CS$<>8__locals1.newChange.SpanStart >= CS$<>8__locals1.oldChange.NewEnd() + num)
				{
					TextChangeRangeExtensions.<Merge>g__addAndAdjustOldDelta|2_2(instance, ref num, CS$<>8__locals1.oldChange);
					if (!TextChangeRangeExtensions.<Merge>g__tryGetNextOldChange|2_0(ref CS$<>8__locals1))
					{
						break;
					}
				}
				else if (CS$<>8__locals1.newChange.SpanStart < CS$<>8__locals1.oldChange.Span.Start + num)
				{
					int num2 = CS$<>8__locals1.oldChange.Span.Start + num - CS$<>8__locals1.newChange.SpanStart;
					TextChangeRangeExtensions.<Merge>g__adjustAndAddNewChange|2_3(instance, num, new TextChangeRangeExtensions.UnadjustedNewChange(CS$<>8__locals1.newChange.SpanStart, num2, 0));
					CS$<>8__locals1.newChange = new TextChangeRangeExtensions.UnadjustedNewChange(CS$<>8__locals1.oldChange.Span.Start + num, CS$<>8__locals1.newChange.SpanLength - num2, CS$<>8__locals1.newChange.NewLength);
				}
				else if (CS$<>8__locals1.newChange.SpanStart > CS$<>8__locals1.oldChange.Span.Start + num)
				{
					int num3 = CS$<>8__locals1.newChange.SpanStart - (CS$<>8__locals1.oldChange.Span.Start + num);
					int num4 = Math.Min(CS$<>8__locals1.oldChange.Span.Length, num3);
					TextChangeRangeExtensions.<Merge>g__addAndAdjustOldDelta|2_2(instance, ref num, new TextChangeRange(new TextSpan(CS$<>8__locals1.oldChange.Span.Start, num4), num3));
					CS$<>8__locals1.oldChange = new TextChangeRange(new TextSpan(CS$<>8__locals1.newChange.SpanStart - num, CS$<>8__locals1.oldChange.Span.Length - num4), CS$<>8__locals1.oldChange.NewLength - num3);
				}
				else if (CS$<>8__locals1.newChange.SpanLength <= CS$<>8__locals1.oldChange.NewLength)
				{
					CS$<>8__locals1.oldChange = new TextChangeRange(CS$<>8__locals1.oldChange.Span, CS$<>8__locals1.oldChange.NewLength - CS$<>8__locals1.newChange.SpanLength);
					num += CS$<>8__locals1.newChange.SpanLength;
					CS$<>8__locals1.newChange = new TextChangeRangeExtensions.UnadjustedNewChange(CS$<>8__locals1.newChange.SpanEnd, 0, CS$<>8__locals1.newChange.NewLength);
					TextChangeRangeExtensions.<Merge>g__adjustAndAddNewChange|2_3(instance, num, CS$<>8__locals1.newChange);
					if (!TextChangeRangeExtensions.<Merge>g__tryGetNextNewChange|2_1(ref CS$<>8__locals1))
					{
						break;
					}
				}
				else
				{
					num = num - CS$<>8__locals1.oldChange.Span.Length + CS$<>8__locals1.oldChange.NewLength;
					int num5 = CS$<>8__locals1.newChange.SpanLength + CS$<>8__locals1.oldChange.Span.Length - CS$<>8__locals1.oldChange.NewLength;
					CS$<>8__locals1.newChange = new TextChangeRangeExtensions.UnadjustedNewChange(CS$<>8__locals1.oldChange.Span.Start + num, num5, CS$<>8__locals1.newChange.NewLength);
					if (!TextChangeRangeExtensions.<Merge>g__tryGetNextOldChange|2_0(ref CS$<>8__locals1))
					{
						break;
					}
				}
			}
			bool flag = CS$<>8__locals1.oldIndex == CS$<>8__locals1.oldChanges.Length;
			bool flag2 = CS$<>8__locals1.newIndex == CS$<>8__locals1.newChanges.Length;
			if (flag)
			{
				if (flag2)
				{
					goto IL_044B;
				}
			}
			else if (!flag2)
			{
				goto IL_044B;
			}
			while (CS$<>8__locals1.oldIndex < CS$<>8__locals1.oldChanges.Length)
			{
				TextChangeRangeExtensions.<Merge>g__addAndAdjustOldDelta|2_2(instance, ref num, CS$<>8__locals1.oldChange);
				TextChangeRangeExtensions.<Merge>g__tryGetNextOldChange|2_0(ref CS$<>8__locals1);
			}
			while (CS$<>8__locals1.newIndex < CS$<>8__locals1.newChanges.Length)
			{
				TextChangeRangeExtensions.<Merge>g__adjustAndAddNewChange|2_3(instance, num, CS$<>8__locals1.newChange);
				TextChangeRangeExtensions.<Merge>g__tryGetNextNewChange|2_1(ref CS$<>8__locals1);
			}
			return instance.ToImmutableAndFree();
			IL_044B:
			throw new InvalidOperationException();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000087EC File Offset: 0x000069EC
		private static int NewEnd(this TextChangeRange range)
		{
			return range.Span.Start + range.NewLength;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008810 File Offset: 0x00006A10
		[CompilerGenerated]
		internal static bool <Merge>g__tryGetNextOldChange|2_0(ref TextChangeRangeExtensions.<>c__DisplayClass2_0 A_0)
		{
			int oldIndex = A_0.oldIndex;
			A_0.oldIndex = oldIndex + 1;
			if (A_0.oldIndex < A_0.oldChanges.Length)
			{
				A_0.oldChange = A_0.oldChanges[A_0.oldIndex];
				return true;
			}
			A_0.oldChange = default(TextChangeRange);
			return false;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00008868 File Offset: 0x00006A68
		[CompilerGenerated]
		internal static bool <Merge>g__tryGetNextNewChange|2_1(ref TextChangeRangeExtensions.<>c__DisplayClass2_0 A_0)
		{
			int newIndex = A_0.newIndex;
			A_0.newIndex = newIndex + 1;
			if (A_0.newIndex < A_0.newChanges.Length)
			{
				A_0.newChange = new TextChangeRangeExtensions.UnadjustedNewChange(A_0.newChanges[A_0.newIndex]);
				return true;
			}
			A_0.newChange = default(TextChangeRangeExtensions.UnadjustedNewChange);
			return false;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000088C4 File Offset: 0x00006AC4
		[NullableContext(1)]
		[CompilerGenerated]
		internal static void <Merge>g__addAndAdjustOldDelta|2_2(ArrayBuilder<TextChangeRange> builder, ref int oldDelta, TextChangeRange oldChange)
		{
			oldDelta = oldDelta - oldChange.Span.Length + oldChange.NewLength;
			TextChangeRangeExtensions.<Merge>g__add|2_4(builder, oldChange);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000088F4 File Offset: 0x00006AF4
		[NullableContext(1)]
		[CompilerGenerated]
		internal static void <Merge>g__adjustAndAddNewChange|2_3(ArrayBuilder<TextChangeRange> builder, int oldDelta, TextChangeRangeExtensions.UnadjustedNewChange newChange)
		{
			TextChangeRangeExtensions.<Merge>g__add|2_4(builder, new TextChangeRange(new TextSpan(newChange.SpanStart - oldDelta, newChange.SpanLength), newChange.NewLength));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00008920 File Offset: 0x00006B20
		[NullableContext(1)]
		[CompilerGenerated]
		internal static void <Merge>g__add|2_4(ArrayBuilder<TextChangeRange> builder, TextChangeRange change)
		{
			if (builder.Count > 0)
			{
				int num = builder.Count - 1;
				TextChangeRange textChangeRange = builder[num];
				if (textChangeRange.Span.End == change.Span.Start)
				{
					num = builder.Count - 1;
					builder[num] = new TextChangeRange(new TextSpan(textChangeRange.Span.Start, textChangeRange.Span.Length + change.Span.Length), textChangeRange.NewLength + change.NewLength);
					return;
				}
				if (textChangeRange.Span.End > change.Span.Start)
				{
					throw new ArgumentOutOfRangeException("change");
				}
			}
			builder.Add(change);
		}

		// Token: 0x0200050F RID: 1295
		private readonly struct UnadjustedNewChange
		{
			// Token: 0x17000EB2 RID: 3762
			// (get) Token: 0x06003312 RID: 13074 RVA: 0x00090D26 File Offset: 0x0008EF26
			public int SpanStart { get; }

			// Token: 0x17000EB3 RID: 3763
			// (get) Token: 0x06003313 RID: 13075 RVA: 0x00090D2E File Offset: 0x0008EF2E
			public int SpanLength { get; }

			// Token: 0x17000EB4 RID: 3764
			// (get) Token: 0x06003314 RID: 13076 RVA: 0x00090D36 File Offset: 0x0008EF36
			public int NewLength { get; }

			// Token: 0x17000EB5 RID: 3765
			// (get) Token: 0x06003315 RID: 13077 RVA: 0x00090D3E File Offset: 0x0008EF3E
			public int SpanEnd
			{
				get
				{
					return this.SpanStart + this.SpanLength;
				}
			}

			// Token: 0x06003316 RID: 13078 RVA: 0x00090D4D File Offset: 0x0008EF4D
			public UnadjustedNewChange(int spanStart, int spanLength, int newLength)
			{
				this.SpanStart = spanStart;
				this.SpanLength = spanLength;
				this.NewLength = newLength;
			}

			// Token: 0x06003317 RID: 13079 RVA: 0x00090D64 File Offset: 0x0008EF64
			public UnadjustedNewChange(TextChangeRange range)
			{
				this = new TextChangeRangeExtensions.UnadjustedNewChange(range.Span.Start, range.Span.Length, range.NewLength);
			}
		}
	}
}
