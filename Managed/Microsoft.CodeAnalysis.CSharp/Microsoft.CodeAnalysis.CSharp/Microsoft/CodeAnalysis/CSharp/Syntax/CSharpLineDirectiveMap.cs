using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000252 RID: 594
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
	internal class CSharpLineDirectiveMap : LineDirectiveMap<DirectiveTriviaSyntax>
	{
		// Token: 0x060038B4 RID: 14516 RVA: 0x000FB52F File Offset: 0x000F972F
		public CSharpLineDirectiveMap(SyntaxTree syntaxTree)
			: base(syntaxTree)
		{
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000FB538 File Offset: 0x000F9738
		protected override bool ShouldAddDirective(DirectiveTriviaSyntax directive)
		{
			return directive.IsActive && directive.Kind() == SyntaxKind.LineDirectiveTrivia;
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000FB554 File Offset: 0x000F9754
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry GetEntry(DirectiveTriviaSyntax directiveNode, SourceText sourceText, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry previous)
		{
			LineDirectiveTriviaSyntax lineDirectiveTriviaSyntax = (LineDirectiveTriviaSyntax)directiveNode;
			int num = sourceText.Lines.IndexOf(lineDirectiveTriviaSyntax.SpanStart) + 1;
			int num2 = num;
			int num3 = previous.MappedLine + num - previous.UnmappedLine;
			string text = previous.MappedPathOpt;
			LineDirectiveMap<DirectiveTriviaSyntax>.PositionState positionState = LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Unmapped;
			SyntaxToken line = lineDirectiveTriviaSyntax.Line;
			if (!line.IsMissing)
			{
				SyntaxKind syntaxKind = line.Kind();
				if (syntaxKind != SyntaxKind.DefaultKeyword)
				{
					if (syntaxKind != SyntaxKind.HiddenKeyword)
					{
						if (syntaxKind == SyntaxKind.NumericLiteralToken)
						{
							if (!line.ContainsDiagnostics)
							{
								object value = line.Value;
								if (value is int)
								{
									num3 = (int)value - 1;
								}
								if (lineDirectiveTriviaSyntax.File.Kind() == SyntaxKind.StringLiteralToken)
								{
									text = (string)lineDirectiveTriviaSyntax.File.Value;
								}
								positionState = LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Remapped;
							}
						}
					}
					else
					{
						positionState = LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Hidden;
					}
				}
				else
				{
					num3 = num2;
					text = null;
					positionState = LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Unmapped;
				}
			}
			return new LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry(num2, num3, text, positionState);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000FB63D File Offset: 0x000F983D
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry InitializeFirstEntry()
		{
			return new LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry(0, 0, null, LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Unmapped);
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000FB648 File Offset: 0x000F9848
		public override LineVisibility GetLineVisibility(SourceText sourceText, int position)
		{
			LinePosition linePosition = sourceText.Lines.GetLinePosition(position);
			if (this.Entries.Length == 1)
			{
				return LineVisibility.Visible;
			}
			int num = base.FindEntryIndex(linePosition.Line);
			LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry lineMappingEntry = this.Entries[num];
			switch (lineMappingEntry.State)
			{
			case LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Unmapped:
				if (num == 0)
				{
					return LineVisibility.BeforeFirstLineDirective;
				}
				return LineVisibility.Visible;
			case LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Remapped:
				return LineVisibility.Visible;
			case LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Hidden:
				return LineVisibility.Hidden;
			}
			throw ExceptionUtilities.UnexpectedValue(lineMappingEntry.State);
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000FB6CC File Offset: 0x000F98CC
		internal override FileLinePositionSpan TranslateSpanAndVisibility(SourceText sourceText, string treeFilePath, TextSpan span, out bool isHiddenPosition)
		{
			TextLineCollection lines = sourceText.Lines;
			LinePosition linePosition = lines.GetLinePosition(span.Start);
			LinePosition linePosition2 = lines.GetLinePosition(span.End);
			if (this.Entries.Length == 1)
			{
				isHiddenPosition = false;
				return new FileLinePositionSpan(treeFilePath, linePosition, linePosition2);
			}
			LineDirectiveMap<DirectiveTriviaSyntax>.LineMappingEntry lineMappingEntry = base.FindEntry(linePosition.Line);
			isHiddenPosition = lineMappingEntry.State == LineDirectiveMap<DirectiveTriviaSyntax>.PositionState.Hidden;
			return base.TranslateSpan(lineMappingEntry, treeFilePath, linePosition, linePosition2);
		}
	}
}
