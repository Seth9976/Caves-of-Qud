using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x020002FA RID: 762
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct AnalysisContextInfo
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x0006469C File Offset: 0x0006289C
		public AnalysisContextInfo(Compilation compilation)
		{
			this = new AnalysisContextInfo(compilation, null, null, default(SourceOrAdditionalFile?), null);
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000646BC File Offset: 0x000628BC
		public AnalysisContextInfo(SemanticModel model)
		{
			this = new AnalysisContextInfo(model.Compilation, new SourceOrAdditionalFile(model.SyntaxTree));
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000646D8 File Offset: 0x000628D8
		public AnalysisContextInfo(Compilation compilation, ISymbol symbol)
		{
			this = new AnalysisContextInfo(compilation, null, symbol, default(SourceOrAdditionalFile?), null);
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000646F8 File Offset: 0x000628F8
		public AnalysisContextInfo(Compilation compilation, SourceOrAdditionalFile file)
		{
			this = new AnalysisContextInfo(compilation, null, null, new SourceOrAdditionalFile?(file), null);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x0006470A File Offset: 0x0006290A
		public AnalysisContextInfo(Compilation compilation, SyntaxNode node)
		{
			this = new AnalysisContextInfo(compilation, null, null, new SourceOrAdditionalFile?(new SourceOrAdditionalFile(node.SyntaxTree)), node);
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00064726 File Offset: 0x00062926
		public AnalysisContextInfo(Compilation compilation, IOperation operation)
		{
			this = new AnalysisContextInfo(compilation, operation, null, new SourceOrAdditionalFile?(new SourceOrAdditionalFile(operation.Syntax.SyntaxTree)), operation.Syntax);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0006474C File Offset: 0x0006294C
		public AnalysisContextInfo(Compilation compilation, ISymbol symbol, SyntaxNode node)
		{
			this = new AnalysisContextInfo(compilation, null, symbol, new SourceOrAdditionalFile?(new SourceOrAdditionalFile(node.SyntaxTree)), node);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00064768 File Offset: 0x00062968
		[NullableContext(2)]
		private AnalysisContextInfo(Compilation compilation, IOperation operation, ISymbol symbol, SourceOrAdditionalFile? file, SyntaxNode node)
		{
			this._compilation = compilation;
			this._operation = operation;
			this._symbol = symbol;
			this._file = file;
			this._node = node;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00064790 File Offset: 0x00062990
		public string GetContext()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Compilation compilation = this._compilation;
			if (((compilation != null) ? compilation.AssemblyName : null) != null)
			{
				stringBuilder.AppendLine("Compilation: " + this._compilation.AssemblyName);
			}
			if (this._operation != null)
			{
				stringBuilder.AppendLine(string.Format("{0}: {1}", "IOperation", this._operation.Kind));
			}
			ISymbol symbol = this._symbol;
			if (((symbol != null) ? symbol.Name : null) != null)
			{
				stringBuilder.AppendLine(string.Format("{0}: {1} ({2})", "ISymbol", this._symbol.Name, this._symbol.Kind));
			}
			if (this._file != null)
			{
				if (this._file.Value.SourceTree != null)
				{
					stringBuilder.AppendLine("SyntaxTree: " + this._file.Value.SourceTree.FilePath);
				}
				else
				{
					stringBuilder.AppendLine("AdditionalText: " + this._file.Value.AdditionalFile.Path);
				}
			}
			if (this._node != null)
			{
				SourceText text = this._file.Value.SourceTree.GetText(default(CancellationToken));
				LinePositionSpan? linePositionSpan;
				if (text == null)
				{
					linePositionSpan = default(LinePositionSpan?);
				}
				else
				{
					TextLineCollection lines = text.Lines;
					linePositionSpan = ((lines != null) ? new LinePositionSpan?(lines.GetLinePositionSpan(this._node.Span)) : default(LinePositionSpan?));
				}
				LinePositionSpan? linePositionSpan2 = linePositionSpan;
				stringBuilder.AppendLine(string.Format("{0}: {1} [{2}]@{3} {4}", new object[]
				{
					"SyntaxNode",
					this.GetFlattenedNodeText(this._node),
					this._node.GetType().Name,
					this._node.Span,
					(linePositionSpan2 != null) ? linePositionSpan2.Value.ToString() : string.Empty
				}));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x000649A8 File Offset: 0x00062BA8
		private string GetFlattenedNodeText(SyntaxNode node)
		{
			int num = node.Span.Start;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SyntaxToken syntaxToken in node.DescendantTokens(null, false))
			{
				if (syntaxToken.Span.Start - num > 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(syntaxToken.ToString());
				num = syntaxToken.Span.End;
				if (stringBuilder.Length > 30)
				{
					break;
				}
			}
			return stringBuilder.ToString() + ((stringBuilder.Length > 30) ? " ..." : string.Empty);
		}

		// Token: 0x04000E2E RID: 3630
		[Nullable(2)]
		private readonly Compilation _compilation;

		// Token: 0x04000E2F RID: 3631
		[Nullable(2)]
		private readonly IOperation _operation;

		// Token: 0x04000E30 RID: 3632
		[Nullable(2)]
		private readonly ISymbol _symbol;

		// Token: 0x04000E31 RID: 3633
		private readonly SourceOrAdditionalFile? _file;

		// Token: 0x04000E32 RID: 3634
		[Nullable(2)]
		private readonly SyntaxNode _node;
	}
}
