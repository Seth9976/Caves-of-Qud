using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000246 RID: 582
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class AttributeTargetSpecifierSyntax : CSharpSyntaxNode
	{
		// Token: 0x060037D3 RID: 14291 RVA: 0x000F9FB5 File Offset: 0x000F81B5
		internal AttributeLocation GetAttributeLocation()
		{
			return this.Identifier.ToAttributeLocation();
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000F9FC2 File Offset: 0x000F81C2
		internal AttributeTargetSpecifierSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060037D5 RID: 14293 RVA: 0x000F9FCD File Offset: 0x000F81CD
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((AttributeTargetSpecifierSyntax)base.Green).identifier, base.Position, 0);
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060037D6 RID: 14294 RVA: 0x000F9FEC File Offset: 0x000F81EC
		public SyntaxToken ColonToken
		{
			get
			{
				return new SyntaxToken(this, ((AttributeTargetSpecifierSyntax)base.Green).colonToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000FA012 File Offset: 0x000F8212
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000FA015 File Offset: 0x000F8215
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000FA018 File Offset: 0x000F8218
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeTargetSpecifier(this);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000FA021 File Offset: 0x000F8221
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeTargetSpecifier(this);
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000FA02C File Offset: 0x000F822C
		public AttributeTargetSpecifierSyntax Update(SyntaxToken identifier, SyntaxToken colonToken)
		{
			if (!(identifier != this.Identifier) && !(colonToken != this.ColonToken))
			{
				return this;
			}
			AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax = SyntaxFactory.AttributeTargetSpecifier(identifier, colonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return attributeTargetSpecifierSyntax;
			}
			return attributeTargetSpecifierSyntax.WithAnnotations(annotations);
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000FA076 File Offset: 0x000F8276
		public AttributeTargetSpecifierSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(identifier, this.ColonToken);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000FA085 File Offset: 0x000F8285
		public AttributeTargetSpecifierSyntax WithColonToken(SyntaxToken colonToken)
		{
			return this.Update(this.Identifier, colonToken);
		}
	}
}
