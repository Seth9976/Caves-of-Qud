using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000450 RID: 1104
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DocumentationCommentTriviaSyntax : StructuredTriviaSyntax
	{
		// Token: 0x06005D86 RID: 23942 RVA: 0x0016C68A File Offset: 0x0016A88A
		internal DocumentationCommentTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode content, SyntaxToken endOfComment, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			if (content != null)
			{
				base.AdjustFlagsAndWidth(content);
				this.content = content;
			}
			base.AdjustFlagsAndWidth(endOfComment);
			this.endOfComment = endOfComment;
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x0016C6BD File Offset: 0x0016A8BD
		internal DocumentationCommentTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode content, SyntaxToken endOfComment, SyntaxFactoryContext context)
			: base(kind, null, null)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			if (content != null)
			{
				base.AdjustFlagsAndWidth(content);
				this.content = content;
			}
			base.AdjustFlagsAndWidth(endOfComment);
			this.endOfComment = endOfComment;
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x0016C6F6 File Offset: 0x0016A8F6
		internal DocumentationCommentTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode content, SyntaxToken endOfComment)
			: base(kind, null, null)
		{
			base.SlotCount = 2;
			if (content != null)
			{
				base.AdjustFlagsAndWidth(content);
				this.content = content;
			}
			base.AdjustFlagsAndWidth(endOfComment);
			this.endOfComment = endOfComment;
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x0016C727 File Offset: 0x0016A927
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<XmlNodeSyntax> Content
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<XmlNodeSyntax>(this.content);
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x0016C734 File Offset: 0x0016A934
		public SyntaxToken EndOfComment
		{
			get
			{
				return this.endOfComment;
			}
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x0016C73C File Offset: 0x0016A93C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.endOfComment;
				}
			}
			else
			{
				greenNode = this.content;
			}
			return greenNode;
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x0016C767 File Offset: 0x0016A967
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DocumentationCommentTriviaSyntax(this, parent, position);
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x0016C771 File Offset: 0x0016A971
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDocumentationCommentTrivia(this);
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x0016C77A File Offset: 0x0016A97A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDocumentationCommentTrivia(this);
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x0016C784 File Offset: 0x0016A984
		public DocumentationCommentTriviaSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, SyntaxToken endOfComment)
		{
			if (content != this.Content || endOfComment != this.EndOfComment)
			{
				DocumentationCommentTriviaSyntax documentationCommentTriviaSyntax = SyntaxFactory.DocumentationCommentTrivia(base.Kind, content, endOfComment);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					documentationCommentTriviaSyntax = documentationCommentTriviaSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					documentationCommentTriviaSyntax = documentationCommentTriviaSyntax.WithAnnotationsGreen(annotations);
				}
				return documentationCommentTriviaSyntax;
			}
			return this;
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x0016C7E5 File Offset: 0x0016A9E5
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DocumentationCommentTriviaSyntax(base.Kind, this.content, this.endOfComment, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x0016C805 File Offset: 0x0016AA05
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DocumentationCommentTriviaSyntax(base.Kind, this.content, this.endOfComment, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x0016C828 File Offset: 0x0016AA28
		internal DocumentationCommentTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.content = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.endOfComment = syntaxToken;
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x0016C87A File Offset: 0x0016AA7A
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.content);
			writer.WriteValue(this.endOfComment);
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x0016C89B File Offset: 0x0016AA9B
		static DocumentationCommentTriviaSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DocumentationCommentTriviaSyntax), (ObjectReader r) => new DocumentationCommentTriviaSyntax(r));
		}

		// Token: 0x040018A7 RID: 6311
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode content;

		// Token: 0x040018A8 RID: 6312
		internal readonly SyntaxToken endOfComment;
	}
}
