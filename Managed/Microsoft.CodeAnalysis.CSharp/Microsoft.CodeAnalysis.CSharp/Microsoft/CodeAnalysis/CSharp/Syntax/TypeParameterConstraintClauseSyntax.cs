using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000316 RID: 790
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class TypeParameterConstraintClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x06004458 RID: 17496 RVA: 0x00114B4A File Offset: 0x00112D4A
		internal TypeParameterConstraintClauseSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x00114B55 File Offset: 0x00112D55
		public SyntaxToken WhereKeyword
		{
			get
			{
				return new SyntaxToken(this, ((TypeParameterConstraintClauseSyntax)base.Green).whereKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x00114B74 File Offset: 0x00112D74
		public IdentifierNameSyntax Name
		{
			get
			{
				return base.GetRed<IdentifierNameSyntax>(ref this.name, 1);
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x00114B83 File Offset: 0x00112D83
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((TypeParameterConstraintClauseSyntax)base.Green).colonToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x00114BAC File Offset: 0x00112DAC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TypeParameterConstraintSyntax> Constraints
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.constraints, 3);
				if (red == null)
				{
					return default(SeparatedSyntaxList<TypeParameterConstraintSyntax>);
				}
				return new SeparatedSyntaxList<TypeParameterConstraintSyntax>(red, base.GetChildIndex(3));
			}
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x00114BE4 File Offset: 0x00112DE4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed(ref this.constraints, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRed<IdentifierNameSyntax>(ref this.name, 1);
			}
			return syntaxNode;
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x00114C20 File Offset: 0x00112E20
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 1)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.constraints;
				}
			}
			else
			{
				syntaxNode = this.name;
			}
			return syntaxNode;
		}

		// Token: 0x0600445F RID: 17503 RVA: 0x00114C4C File Offset: 0x00112E4C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeParameterConstraintClause(this);
		}

		// Token: 0x06004460 RID: 17504 RVA: 0x00114C55 File Offset: 0x00112E55
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameterConstraintClause(this);
		}

		// Token: 0x06004461 RID: 17505 RVA: 0x00114C60 File Offset: 0x00112E60
		public TypeParameterConstraintClauseSyntax Update(SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
		{
			if (!(whereKeyword != this.WhereKeyword) && name == this.Name && !(colonToken != this.ColonToken) && !(constraints != this.Constraints))
			{
				return this;
			}
			TypeParameterConstraintClauseSyntax typeParameterConstraintClauseSyntax = SyntaxFactory.TypeParameterConstraintClause(whereKeyword, name, colonToken, constraints);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return typeParameterConstraintClauseSyntax;
			}
			return typeParameterConstraintClauseSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x00114CC5 File Offset: 0x00112EC5
		public TypeParameterConstraintClauseSyntax WithWhereKeyword(SyntaxToken whereKeyword)
		{
			return this.Update(whereKeyword, this.Name, this.ColonToken, this.Constraints);
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x00114CE0 File Offset: 0x00112EE0
		public TypeParameterConstraintClauseSyntax WithName(IdentifierNameSyntax name)
		{
			return this.Update(this.WhereKeyword, name, this.ColonToken, this.Constraints);
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x00114CFB File Offset: 0x00112EFB
		public TypeParameterConstraintClauseSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.WhereKeyword, this.Name, colonToken, this.Constraints);
		}

		// Token: 0x06004465 RID: 17509 RVA: 0x00114D16 File Offset: 0x00112F16
		public TypeParameterConstraintClauseSyntax WithConstraints([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
		{
			return this.Update(this.WhereKeyword, this.Name, this.ColonToken, constraints);
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x00114D34 File Offset: 0x00112F34
		public TypeParameterConstraintClauseSyntax AddConstraints(params TypeParameterConstraintSyntax[] items)
		{
			return this.WithConstraints(this.Constraints.AddRange(items));
		}

		// Token: 0x040014F5 RID: 5365
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private IdentifierNameSyntax name;

		// Token: 0x040014F6 RID: 5366
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraints;
	}
}
