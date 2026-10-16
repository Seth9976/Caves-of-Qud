using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200026A RID: 618
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class LocalFunctionStatementSyntax : StatementSyntax
	{
		// Token: 0x06003AC7 RID: 15047 RVA: 0x00100A00 File Offset: 0x000FEC00
		public LocalFunctionStatementSyntax Update(SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x00100A28 File Offset: 0x000FEC28
		internal LocalFunctionStatementSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x00100A33 File Offset: 0x000FEC33
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(base.GetRed(ref this.attributeLists, 0));
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x00100A48 File Offset: 0x000FEC48
		public SyntaxTokenList Modifiers
		{
			get
			{
				GreenNode slot = base.Green.GetSlot(1);
				if (slot == null)
				{
					return default(SyntaxTokenList);
				}
				return new SyntaxTokenList(this, slot, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00100A84 File Offset: 0x000FEC84
		public TypeSyntax ReturnType
		{
			get
			{
				return base.GetRed<TypeSyntax>(ref this.returnType, 2);
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x00100A93 File Offset: 0x000FEC93
		public SyntaxToken Identifier
		{
			get
			{
				return new SyntaxToken(this, ((LocalFunctionStatementSyntax)base.Green).identifier, this.GetChildPosition(3), base.GetChildIndex(3));
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x00100AB9 File Offset: 0x000FECB9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 4);
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x00100AC8 File Offset: 0x000FECC8
		public ParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 5);
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x00100AD7 File Offset: 0x000FECD7
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(base.GetRed(ref this.constraintClauses, 6));
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x00100AEB File Offset: 0x000FECEB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<BlockSyntax>(ref this.body, 7);
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x00100AFA File Offset: 0x000FECFA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 8);
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x00100B0C File Offset: 0x000FED0C
		public SyntaxToken SemicolonToken
		{
			get
			{
				SyntaxToken semicolonToken = ((LocalFunctionStatementSyntax)base.Green).semicolonToken;
				if (semicolonToken == null)
				{
					return default(SyntaxToken);
				}
				return new SyntaxToken(this, semicolonToken, this.GetChildPosition(9), base.GetChildIndex(9));
			}
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x00100B50 File Offset: 0x000FED50
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			switch (index)
			{
			case 0:
				return base.GetRedAtZero(ref this.attributeLists);
			case 2:
				return base.GetRed<TypeSyntax>(ref this.returnType, 2);
			case 4:
				return base.GetRed<TypeParameterListSyntax>(ref this.typeParameterList, 4);
			case 5:
				return base.GetRed<ParameterListSyntax>(ref this.parameterList, 5);
			case 6:
				return base.GetRed(ref this.constraintClauses, 6);
			case 7:
				return base.GetRed<BlockSyntax>(ref this.body, 7);
			case 8:
				return base.GetRed<ArrowExpressionClauseSyntax>(ref this.expressionBody, 8);
			}
			return null;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x00100BFC File Offset: 0x000FEDFC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			switch (index)
			{
			case 0:
				return this.attributeLists;
			case 2:
				return this.returnType;
			case 4:
				return this.typeParameterList;
			case 5:
				return this.parameterList;
			case 6:
				return this.constraintClauses;
			case 7:
				return this.body;
			case 8:
				return this.expressionBody;
			}
			return null;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x00100C77 File Offset: 0x000FEE77
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitLocalFunctionStatement(this);
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x00100C80 File Offset: 0x000FEE80
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitLocalFunctionStatement(this);
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x00100C8C File Offset: 0x000FEE8C
		public LocalFunctionStatementSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxTokenList modifiers, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (!(attributeLists != this.AttributeLists) && !(modifiers != this.Modifiers) && returnType == this.ReturnType && !(identifier != this.Identifier) && typeParameterList == this.TypeParameterList && parameterList == this.ParameterList && !(constraintClauses != this.ConstraintClauses) && body == this.Body && expressionBody == this.ExpressionBody && !(semicolonToken != this.SemicolonToken))
			{
				return this;
			}
			LocalFunctionStatementSyntax localFunctionStatementSyntax = SyntaxFactory.LocalFunctionStatement(attributeLists, modifiers, returnType, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return localFunctionStatementSyntax;
			}
			return localFunctionStatementSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00100D43 File Offset: 0x000FEF43
		internal override StatementSyntax WithAttributeListsCore([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.WithAttributeLists(attributeLists);
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x00100D4C File Offset: 0x000FEF4C
		public new LocalFunctionStatementSyntax WithAttributeLists([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists)
		{
			return this.Update(attributeLists, this.Modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x00100D98 File Offset: 0x000FEF98
		public LocalFunctionStatementSyntax WithModifiers(SyntaxTokenList modifiers)
		{
			return this.Update(this.AttributeLists, modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x00100DE4 File Offset: 0x000FEFE4
		public LocalFunctionStatementSyntax WithReturnType(TypeSyntax returnType)
		{
			return this.Update(this.AttributeLists, this.Modifiers, returnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003ADC RID: 15068 RVA: 0x00100E30 File Offset: 0x000FF030
		public LocalFunctionStatementSyntax WithIdentifier(SyntaxToken identifier)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003ADD RID: 15069 RVA: 0x00100E7C File Offset: 0x000FF07C
		public LocalFunctionStatementSyntax WithTypeParameterList([global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.Identifier, typeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x00100EC8 File Offset: 0x000FF0C8
		public LocalFunctionStatementSyntax WithParameterList(ParameterListSyntax parameterList)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, parameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003ADF RID: 15071 RVA: 0x00100F14 File Offset: 0x000FF114
		public LocalFunctionStatementSyntax WithConstraintClauses([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, constraintClauses, this.Body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00100F60 File Offset: 0x000FF160
		public LocalFunctionStatementSyntax WithBody([global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, body, this.ExpressionBody, this.SemicolonToken);
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00100FAC File Offset: 0x000FF1AC
		public LocalFunctionStatementSyntax WithExpressionBody([global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, expressionBody, this.SemicolonToken);
		}

		// Token: 0x06003AE2 RID: 15074 RVA: 0x00100FF8 File Offset: 0x000FF1F8
		public LocalFunctionStatementSyntax WithSemicolonToken(SyntaxToken semicolonToken)
		{
			return this.Update(this.AttributeLists, this.Modifiers, this.ReturnType, this.Identifier, this.TypeParameterList, this.ParameterList, this.ConstraintClauses, this.Body, this.ExpressionBody, semicolonToken);
		}

		// Token: 0x06003AE3 RID: 15075 RVA: 0x00101042 File Offset: 0x000FF242
		internal override StatementSyntax AddAttributeListsCore(params AttributeListSyntax[] items)
		{
			return this.AddAttributeLists(items);
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x0010104C File Offset: 0x000FF24C
		public new LocalFunctionStatementSyntax AddAttributeLists(params AttributeListSyntax[] items)
		{
			return this.WithAttributeLists(this.AttributeLists.AddRange(items));
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x00101070 File Offset: 0x000FF270
		public LocalFunctionStatementSyntax AddModifiers(params SyntaxToken[] items)
		{
			return this.WithModifiers(this.Modifiers.AddRange(items));
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x00101094 File Offset: 0x000FF294
		public LocalFunctionStatementSyntax AddTypeParameterListParameters(params TypeParameterSyntax[] items)
		{
			TypeParameterListSyntax typeParameterListSyntax;
			if ((typeParameterListSyntax = this.TypeParameterList) == null)
			{
				typeParameterListSyntax = SyntaxFactory.TypeParameterList(default(SeparatedSyntaxList<TypeParameterSyntax>));
			}
			TypeParameterListSyntax typeParameterListSyntax2 = typeParameterListSyntax;
			return this.WithTypeParameterList(typeParameterListSyntax2.WithParameters(typeParameterListSyntax2.Parameters.AddRange(items)));
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x001010D8 File Offset: 0x000FF2D8
		public LocalFunctionStatementSyntax AddParameterListParameters(params ParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x06003AE8 RID: 15080 RVA: 0x0010110C File Offset: 0x000FF30C
		public LocalFunctionStatementSyntax AddConstraintClauses(params TypeParameterConstraintClauseSyntax[] items)
		{
			return this.WithConstraintClauses(this.ConstraintClauses.AddRange(items));
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x00101130 File Offset: 0x000FF330
		public LocalFunctionStatementSyntax AddBodyAttributeLists(params AttributeListSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithAttributeLists(blockSyntax2.AttributeLists.AddRange(items)));
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x00101174 File Offset: 0x000FF374
		public LocalFunctionStatementSyntax AddBodyStatements(params StatementSyntax[] items)
		{
			BlockSyntax blockSyntax;
			if ((blockSyntax = this.Body) == null)
			{
				blockSyntax = SyntaxFactory.Block(default(SyntaxList<StatementSyntax>));
			}
			BlockSyntax blockSyntax2 = blockSyntax;
			return this.WithBody(blockSyntax2.WithStatements(blockSyntax2.Statements.AddRange(items)));
		}

		// Token: 0x040013E7 RID: 5095
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode attributeLists;

		// Token: 0x040013E8 RID: 5096
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeSyntax returnType;

		// Token: 0x040013E9 RID: 5097
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeParameterListSyntax typeParameterList;

		// Token: 0x040013EA RID: 5098
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ParameterListSyntax parameterList;

		// Token: 0x040013EB RID: 5099
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode constraintClauses;

		// Token: 0x040013EC RID: 5100
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private BlockSyntax body;

		// Token: 0x040013ED RID: 5101
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrowExpressionClauseSyntax expressionBody;
	}
}
