using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x020002D1 RID: 721
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class JoinIntoClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060040C1 RID: 16577 RVA: 0x0010E67A File Offset: 0x0010C87A
		internal JoinIntoClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x060040C2 RID: 16578 RVA: 0x0010E685 File Offset: 0x0010C885
		public SyntaxToken IntoKeyword
		{
			get
			{
				return new SyntaxToken(this, ((JoinIntoClauseSyntax)base.Green).intoKeyword, base.Position, 0);
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x060040C3 RID: 16579 RVA: 0x0010E6A4 File Offset: 0x0010C8A4
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((JoinIntoClauseSyntax)base.Green).identifier, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0010E6CA File Offset: 0x0010C8CA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0010E6CD File Offset: 0x0010C8CD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0010E6D0 File Offset: 0x0010C8D0
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitJoinIntoClause(this);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x0010E6D9 File Offset: 0x0010C8D9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitJoinIntoClause(this);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x0010E6E4 File Offset: 0x0010C8E4
		public JoinIntoClauseSyntax Update(SyntaxToken intoKeyword, SyntaxToken identifier)
		{
			if (!(intoKeyword != this.IntoKeyword) && !(identifier != this.Identifier))
			{
				return this;
			}
			JoinIntoClauseSyntax joinIntoClauseSyntax = SyntaxFactory.JoinIntoClause(intoKeyword, identifier);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return joinIntoClauseSyntax;
			}
			return joinIntoClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0010E72E File Offset: 0x0010C92E
		public JoinIntoClauseSyntax WithIntoKeyword(SyntaxToken intoKeyword)
		{
			return this.Update(intoKeyword, this.Identifier);
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0010E73D File Offset: 0x0010C93D
		public JoinIntoClauseSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.IntoKeyword, identifier);
		}
	}
}
