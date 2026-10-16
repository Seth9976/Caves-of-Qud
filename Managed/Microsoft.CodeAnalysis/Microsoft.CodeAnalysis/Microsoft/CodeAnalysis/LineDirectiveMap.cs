using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000274 RID: 628
	internal abstract class LineDirectiveMap<TDirective> where TDirective : SyntaxNode
	{
		// Token: 0x06001804 RID: 6148
		[NullableContext(1)]
		protected abstract bool ShouldAddDirective(TDirective directive);

		// Token: 0x06001805 RID: 6149
		protected abstract LineDirectiveMap<TDirective>.LineMappingEntry GetEntry([Nullable(1)] TDirective directive, [Nullable(1)] SourceText sourceText, LineDirectiveMap<TDirective>.LineMappingEntry previous);

		// Token: 0x06001806 RID: 6150
		protected abstract LineDirectiveMap<TDirective>.LineMappingEntry InitializeFirstEntry();

		// Token: 0x06001807 RID: 6151 RVA: 0x00047730 File Offset: 0x00045930
		[NullableContext(1)]
		protected LineDirectiveMap(SyntaxTree syntaxTree)
		{
			IList<TDirective> directives = syntaxTree.GetRoot(default(CancellationToken)).GetDirectives<TDirective>(new Func<TDirective, bool>(this.ShouldAddDirective));
			this.Entries = this.CreateEntryMap(syntaxTree, directives);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0004777C File Offset: 0x0004597C
		[NullableContext(1)]
		public FileLinePositionSpan TranslateSpan(SourceText sourceText, string treeFilePath, TextSpan span)
		{
			LinePosition linePosition = sourceText.Lines.GetLinePosition(span.Start);
			LinePosition linePosition2 = sourceText.Lines.GetLinePosition(span.End);
			LineDirectiveMap<TDirective>.LineMappingEntry lineMappingEntry = this.FindEntry(linePosition.Line);
			return this.TranslateSpan(lineMappingEntry, treeFilePath, linePosition, linePosition2);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000477C8 File Offset: 0x000459C8
		protected FileLinePositionSpan TranslateSpan(LineDirectiveMap<TDirective>.LineMappingEntry entry, [Nullable(1)] string treeFilePath, LinePosition unmappedStartPos, LinePosition unmappedEndPos)
		{
			string text = entry.MappedPathOpt ?? treeFilePath;
			int num = unmappedStartPos.Line - entry.UnmappedLine + entry.MappedLine;
			int num2 = unmappedEndPos.Line - entry.UnmappedLine + entry.MappedLine;
			return new FileLinePositionSpan(text, new LinePositionSpan((num == -1) ? new LinePosition(unmappedStartPos.Character) : new LinePosition(num, unmappedStartPos.Character), (num2 == -1) ? new LinePosition(unmappedEndPos.Character) : new LinePosition(num2, unmappedEndPos.Character)), entry.MappedPathOpt != null);
		}

		// Token: 0x0600180A RID: 6154
		[NullableContext(1)]
		public abstract LineVisibility GetLineVisibility(SourceText sourceText, int position);

		// Token: 0x0600180B RID: 6155
		[NullableContext(1)]
		internal abstract FileLinePositionSpan TranslateSpanAndVisibility(SourceText sourceText, string treeFilePath, TextSpan span, out bool isHiddenPosition);

		// Token: 0x0600180C RID: 6156 RVA: 0x0004785D File Offset: 0x00045A5D
		public bool HasAnyHiddenRegions()
		{
			return Enumerable.Any<LineDirectiveMap<TDirective>.LineMappingEntry>(this.Entries, (LineDirectiveMap<TDirective>.LineMappingEntry e) => e.State == LineDirectiveMap<TDirective>.PositionState.Hidden);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0004788C File Offset: 0x00045A8C
		protected LineDirectiveMap<TDirective>.LineMappingEntry FindEntry(int lineNumber)
		{
			int num = this.FindEntryIndex(lineNumber);
			return this.Entries[num];
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000478B0 File Offset: 0x00045AB0
		protected int FindEntryIndex(int lineNumber)
		{
			int num = Array.BinarySearch<LineDirectiveMap<TDirective>.LineMappingEntry>(this.Entries, new LineDirectiveMap<TDirective>.LineMappingEntry(lineNumber));
			if (num < 0)
			{
				return ~num - 1;
			}
			return num;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000478DC File Offset: 0x00045ADC
		[NullableContext(1)]
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private LineDirectiveMap<TDirective>.LineMappingEntry[] CreateEntryMap(SyntaxTree tree, IList<TDirective> directives)
		{
			LineDirectiveMap<TDirective>.LineMappingEntry[] array = new LineDirectiveMap<TDirective>.LineMappingEntry[directives.Count + 1];
			LineDirectiveMap<TDirective>.LineMappingEntry lineMappingEntry = this.InitializeFirstEntry();
			int num = 0;
			array[num] = lineMappingEntry;
			if (directives.Count > 0)
			{
				SourceText text = tree.GetText(default(CancellationToken));
				foreach (TDirective tdirective in directives)
				{
					lineMappingEntry = this.GetEntry(tdirective, text, lineMappingEntry);
					num++;
					array[num] = lineMappingEntry;
				}
			}
			return array;
		}

		// Token: 0x040009F6 RID: 2550
		[Nullable(new byte[] { 1, 0, 0 })]
		protected readonly LineDirectiveMap<TDirective>.LineMappingEntry[] Entries;

		// Token: 0x020005F8 RID: 1528
		public enum PositionState : byte
		{
			// Token: 0x0400175F RID: 5983
			Unknown,
			// Token: 0x04001760 RID: 5984
			Unmapped,
			// Token: 0x04001761 RID: 5985
			Remapped,
			// Token: 0x04001762 RID: 5986
			RemappedAfterUnknown,
			// Token: 0x04001763 RID: 5987
			RemappedAfterHidden,
			// Token: 0x04001764 RID: 5988
			Hidden
		}

		// Token: 0x020005F9 RID: 1529
		protected readonly struct LineMappingEntry : IComparable<LineDirectiveMap<TDirective>.LineMappingEntry>
		{
			// Token: 0x06003725 RID: 14117 RVA: 0x0009A9A3 File Offset: 0x00098BA3
			public LineMappingEntry(int unmappedLine)
			{
				this.UnmappedLine = unmappedLine;
				this.MappedLine = unmappedLine;
				this.MappedPathOpt = null;
				this.State = LineDirectiveMap<TDirective>.PositionState.Unmapped;
			}

			// Token: 0x06003726 RID: 14118 RVA: 0x0009A9C1 File Offset: 0x00098BC1
			public LineMappingEntry(int unmappedLine, int mappedLine, [Nullable(2)] string mappedPathOpt, LineDirectiveMap<TDirective>.PositionState state)
			{
				this.UnmappedLine = unmappedLine;
				this.MappedLine = mappedLine;
				this.MappedPathOpt = mappedPathOpt;
				this.State = state;
			}

			// Token: 0x06003727 RID: 14119 RVA: 0x0009A9E0 File Offset: 0x00098BE0
			public int CompareTo(LineDirectiveMap<TDirective>.LineMappingEntry other)
			{
				return this.UnmappedLine.CompareTo(other.UnmappedLine);
			}

			// Token: 0x04001765 RID: 5989
			public readonly int UnmappedLine;

			// Token: 0x04001766 RID: 5990
			public readonly int MappedLine;

			// Token: 0x04001767 RID: 5991
			[Nullable(2)]
			public readonly string MappedPathOpt;

			// Token: 0x04001768 RID: 5992
			public readonly LineDirectiveMap<TDirective>.PositionState State;
		}
	}
}
