using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200044F RID: 1103
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SkippedTokensTriviaSyntax : StructuredTriviaSyntax
	{
		// Token: 0x06005D78 RID: 23928 RVA: 0x0016C4EC File Offset: 0x0016A6EC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SkippedTokensTriviaSyntax(SyntaxKind kind, GreenNode tokens, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			if (tokens != null)
			{
				base.AdjustFlagsAndWidth(tokens);
				this.tokens = tokens;
			}
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x0016C510 File Offset: 0x0016A710
		internal SkippedTokensTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode tokens, SyntaxFactoryContext context)
			: base(kind, null, null)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			if (tokens != null)
			{
				base.AdjustFlagsAndWidth(tokens);
				this.tokens = tokens;
			}
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x0016C53A File Offset: 0x0016A73A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SkippedTokensTriviaSyntax(SyntaxKind kind, GreenNode tokens)
			: base(kind, null, null)
		{
			base.SlotCount = 1;
			if (tokens != null)
			{
				base.AdjustFlagsAndWidth(tokens);
				this.tokens = tokens;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x0016C55D File Offset: 0x0016A75D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> Tokens
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.tokens);
			}
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x0016C56A File Offset: 0x0016A76A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.tokens;
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x0016C577 File Offset: 0x0016A777
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SkippedTokensTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x0016C581 File Offset: 0x0016A781
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSkippedTokensTrivia(this);
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x0016C58A File Offset: 0x0016A78A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSkippedTokensTrivia(this);
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x0016C594 File Offset: 0x0016A794
		public SkippedTokensTriviaSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> tokens)
		{
			if (tokens != this.Tokens)
			{
				SkippedTokensTriviaSyntax skippedTokensTriviaSyntax = SyntaxFactory.SkippedTokensTrivia(tokens);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					skippedTokensTriviaSyntax = skippedTokensTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					skippedTokensTriviaSyntax = skippedTokensTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return skippedTokensTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x0016C5E5 File Offset: 0x0016A7E5
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SkippedTokensTriviaSyntax(base.Kind, this.tokens, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x0016C5FF File Offset: 0x0016A7FF
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SkippedTokensTriviaSyntax(base.Kind, this.tokens, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x0016C61C File Offset: 0x0016A81C
		internal SkippedTokensTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.tokens = greenNode;
			}
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x0016C654 File Offset: 0x0016A854
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.tokens);
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x0016C669 File Offset: 0x0016A869
		static SkippedTokensTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SkippedTokensTriviaSyntax), (ObjectReader r) => new SkippedTokensTriviaSyntax(r));
		}

		// Token: 0x040018A6 RID: 6310
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode tokens;
	}
}
