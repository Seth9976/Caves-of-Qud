using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000265 RID: 613
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class IdentifierNameSyntax : SimpleNameSyntax
	{
		// Token: 0x06003A5C RID: 14940 RVA: 0x000FFC84 File Offset: 0x000FDE84
		internal override string ErrorDisplayName()
		{
			return this.Identifier.ValueText;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000FFC9F File Offset: 0x000FDE9F
		internal IdentifierNameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000FFCAA File Offset: 0x000FDEAA
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((IdentifierNameSyntax)base.Green).identifier, base.Position, 0);
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000FFCC9 File Offset: 0x000FDEC9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000FFCCC File Offset: 0x000FDECC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000FFCCF File Offset: 0x000FDECF
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIdentifierName(this);
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x000FFCD8 File Offset: 0x000FDED8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIdentifierName(this);
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000FFCE4 File Offset: 0x000FDEE4
		public IdentifierNameSyntax Update(SyntaxToken identifier)
		{
			if (!(identifier != this.Identifier))
			{
				return this;
			}
			IdentifierNameSyntax identifierNameSyntax = SyntaxFactory.IdentifierName(identifier);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return identifierNameSyntax;
			}
			return identifierNameSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000FFD1F File Offset: 0x000FDF1F
		internal override SimpleNameSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000FFD28 File Offset: 0x000FDF28
		public new IdentifierNameSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(identifier);
		}
	}
}
