using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000302 RID: 770
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CatchDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x060042D2 RID: 17106 RVA: 0x001118CE File Offset: 0x0010FACE
		internal CatchDeclarationSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x001118D9 File Offset: 0x0010FAD9
		public SyntaxToken OpenParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CatchDeclarationSyntax)base.Green).openParenToken, base.Position, 0);
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x060042D4 RID: 17108 RVA: 0x001118F8 File Offset: 0x0010FAF8
		public TypeSyntax Type
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.type, 1);
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x00111908 File Offset: 0x0010FB08
		public SyntaxToken Identifier
		{
			get
			{
				SyntaxToken identifier = ((CatchDeclarationSyntax)base.Green).identifier;
				if (identifier == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, identifier, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060042D6 RID: 17110 RVA: 0x00111948 File Offset: 0x0010FB48
		public SyntaxToken CloseParenToken
		{
			get
			{
				return new SyntaxToken(this, ((CatchDeclarationSyntax)base.Green).closeParenToken, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x0011196E File Offset: 0x0010FB6E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<TypeSyntax>(ref this.type, 1);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x00111983 File Offset: 0x0010FB83
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x00111991 File Offset: 0x0010FB91
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitCatchDeclaration(this);
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x0011199A File Offset: 0x0010FB9A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitCatchDeclaration(this);
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x001119A4 File Offset: 0x0010FBA4
		public CatchDeclarationSyntax Update(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken closeParenToken)
		{
			if (!(openParenToken != this.OpenParenToken) && type == this.Type && !(identifier != this.Identifier) && !(closeParenToken != this.CloseParenToken))
			{
				return this;
			}
			CatchDeclarationSyntax catchDeclarationSyntax = SyntaxFactory.CatchDeclaration(openParenToken, type, identifier, closeParenToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return catchDeclarationSyntax;
			}
			return catchDeclarationSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x00111A09 File Offset: 0x0010FC09
		public CatchDeclarationSyntax WithOpenParenToken(SyntaxToken openParenToken)
		{
			return this.Update(openParenToken, this.Type, this.Identifier, this.CloseParenToken);
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x00111A24 File Offset: 0x0010FC24
		public CatchDeclarationSyntax WithType(TypeSyntax type)
		{
			return this.Update(this.OpenParenToken, type, this.Identifier, this.CloseParenToken);
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x00111A3F File Offset: 0x0010FC3F
		public CatchDeclarationSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.OpenParenToken, this.Type, identifier, this.CloseParenToken);
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00111A5A File Offset: 0x0010FC5A
		public CatchDeclarationSyntax WithCloseParenToken(SyntaxToken closeParenToken)
		{
			return this.Update(this.OpenParenToken, this.Type, this.Identifier, closeParenToken);
		}

		// Token: 0x040014D3 RID: 5331
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax type;
	}
}
