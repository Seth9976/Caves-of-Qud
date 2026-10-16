using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000262 RID: 610
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class GenericNameSyntax : SimpleNameSyntax
	{
		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000FF66E File Offset: 0x000FD86E
		public bool IsUnboundGenericName
		{
			get
			{
				return this.TypeArgumentList.Arguments.Any<TypeSyntax>(SyntaxKind.OmittedTypeArgument);
			}
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000FF688 File Offset: 0x000FD888
		internal override string ErrorDisplayName()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			instance.Builder.Append(this.Identifier.ValueText).Append("<").Append(',', base.Arity - 1)
				.Append(">");
			return instance.ToStringAndFree();
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000FF6DC File Offset: 0x000FD8DC
		internal GenericNameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000FF6E7 File Offset: 0x000FD8E7
		public override SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((GenericNameSyntax)base.Green).identifier, base.Position, 0);
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000FF706 File Offset: 0x000FD906
		public TypeArgumentListSyntax TypeArgumentList
		{
			get
			{
				return base.GetRed<TypeArgumentListSyntax>(ref this.typeArgumentList, 1);
			}
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000FF715 File Offset: 0x000FD915
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<TypeArgumentListSyntax>(ref this.typeArgumentList, 1);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000FF72A File Offset: 0x000FD92A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.typeArgumentList;
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000FF738 File Offset: 0x000FD938
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGenericName(this);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000FF741 File Offset: 0x000FD941
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGenericName(this);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000FF74C File Offset: 0x000FD94C
		public GenericNameSyntax Update(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
		{
			if (!(identifier != this.Identifier) && typeArgumentList == this.TypeArgumentList)
			{
				return this;
			}
			GenericNameSyntax genericNameSyntax = SyntaxFactory.GenericName(identifier, typeArgumentList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return genericNameSyntax;
			}
			return genericNameSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000FF791 File Offset: 0x000FD991
		internal override SimpleNameSyntax WithIdentifierCore(SyntaxToken identifier)
		{
			return this.WithIdentifier(identifier);
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000FF79A File Offset: 0x000FD99A
		public new GenericNameSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(identifier, this.TypeArgumentList);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000FF7A9 File Offset: 0x000FD9A9
		public GenericNameSyntax WithTypeArgumentList(TypeArgumentListSyntax typeArgumentList)
		{
			return this.Update(this.Identifier, typeArgumentList);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000FF7B8 File Offset: 0x000FD9B8
		public GenericNameSyntax AddTypeArgumentListArguments(params TypeSyntax[] items)
		{
			return this.WithTypeArgumentList(this.TypeArgumentList.WithArguments(this.TypeArgumentList.Arguments.AddRange(items)));
		}

		// Token: 0x040013D6 RID: 5078
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeArgumentListSyntax typeArgumentList;
	}
}
