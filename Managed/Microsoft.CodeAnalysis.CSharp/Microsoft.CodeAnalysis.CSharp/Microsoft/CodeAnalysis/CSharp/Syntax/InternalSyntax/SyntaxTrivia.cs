using System;
using System.IO;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000376 RID: 886
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SyntaxTrivia : CSharpSyntaxNode
	{
		// Token: 0x06005084 RID: 20612 RVA: 0x00145EEE File Offset: 0x001440EE
		internal SyntaxTrivia(SyntaxKind kind, string text, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations = null)
			: base(kind, diagnostics, annotations, text.Length)
		{
			this.Text = text;
			if (kind == SyntaxKind.PreprocessingMessageTrivia)
			{
				this.flags |= GreenNode.NodeFlags.ContainsSkippedText;
			}
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x00145F1D File Offset: 0x0014411D
		internal SyntaxTrivia(ObjectReader reader)
			: base(reader)
		{
			this.Text = reader.ReadString();
			base.FullWidth = this.Text.Length;
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00145F43 File Offset: 0x00144143
		static SyntaxTrivia()
		{
			ObjectBinder.RegisterTypeReader(typeof(SyntaxTrivia), (ObjectReader r) => new SyntaxTrivia(r));
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06005087 RID: 20615 RVA: 0x00145F64 File Offset: 0x00144164
		public override bool IsTrivia
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06005088 RID: 20616 RVA: 0x00145F67 File Offset: 0x00144167
		internal override bool ShouldReuseInSerialization
		{
			get
			{
				return base.Kind == SyntaxKind.WhitespaceTrivia && base.FullWidth < 42;
			}
		}

		// Token: 0x06005089 RID: 20617 RVA: 0x00145F82 File Offset: 0x00144182
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteString(this.Text);
		}

		// Token: 0x0600508A RID: 20618 RVA: 0x00145F97 File Offset: 0x00144197
		internal static SyntaxTrivia Create(SyntaxKind kind, string text)
		{
			return new SyntaxTrivia(kind, text, null, null);
		}

		// Token: 0x0600508B RID: 20619 RVA: 0x00145FA2 File Offset: 0x001441A2
		public override string ToFullString()
		{
			return this.Text;
		}

		// Token: 0x0600508C RID: 20620 RVA: 0x00145FAA File Offset: 0x001441AA
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x0600508D RID: 20621 RVA: 0x00145FB2 File Offset: 0x001441B2
		internal override GreenNode GetSlot(int index)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x00145FB9 File Offset: 0x001441B9
		public override int Width
		{
			get
			{
				return base.FullWidth;
			}
		}

		// Token: 0x0600508F RID: 20623 RVA: 0x00145FC1 File Offset: 0x001441C1
		public override int GetLeadingTriviaWidth()
		{
			return 0;
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x00145FC4 File Offset: 0x001441C4
		public override int GetTrailingTriviaWidth()
		{
			return 0;
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x00145FC7 File Offset: 0x001441C7
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SyntaxTrivia(base.Kind, this.Text, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x00145FE1 File Offset: 0x001441E1
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SyntaxTrivia(base.Kind, this.Text, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00145FFB File Offset: 0x001441FB
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTrivia(this);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00146004 File Offset: 0x00144204
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTrivia(this);
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x0014600D File Offset: 0x0014420D
		protected override void WriteTriviaTo(TextWriter writer)
		{
			writer.Write(this.Text);
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x0014601C File Offset: 0x0014421C
		public static implicit operator SyntaxTrivia(SyntaxTrivia trivia)
		{
			SyntaxToken syntaxToken = default(SyntaxToken);
			return new SyntaxTrivia(in syntaxToken, trivia, 0, 0);
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x0014603A File Offset: 0x0014423A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool IsEquivalentTo(GreenNode other)
		{
			return base.IsEquivalentTo(other) && !(this.Text != ((SyntaxTrivia)other).Text);
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00146062 File Offset: 0x00144262
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040015DA RID: 5594
		public readonly string Text;
	}
}
