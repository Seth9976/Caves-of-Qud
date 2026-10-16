using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200043C RID: 1084
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OperatorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x06005C3D RID: 23613 RVA: 0x00167CE0 File Offset: 0x00165EE0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal OperatorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 9;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(returnType);
			this.returnType = returnType;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (body != null)
			{
				base.AdjustFlagsAndWidth(body);
				this.body = body;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x00167DA0 File Offset: 0x00165FA0
		internal OperatorDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 9;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(returnType);
			this.returnType = returnType;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (body != null)
			{
				base.AdjustFlagsAndWidth(body);
				this.body = body;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x00167E64 File Offset: 0x00166064
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal OperatorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 9;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			base.AdjustFlagsAndWidth(returnType);
			this.returnType = returnType;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (body != null)
			{
				base.AdjustFlagsAndWidth(body);
				this.body = body;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06005C40 RID: 23616 RVA: 0x00167F1E File Offset: 0x0016611E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x00167F2B File Offset: 0x0016612B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06005C42 RID: 23618 RVA: 0x00167F38 File Offset: 0x00166138
		public TypeSyntax ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x00167F40 File Offset: 0x00166140
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return this.operatorKeyword;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x00167F48 File Offset: 0x00166148
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x00167F50 File Offset: 0x00166150
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x00167F58 File Offset: 0x00166158
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.body;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x00167F60 File Offset: 0x00166160
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06005C48 RID: 23624 RVA: 0x00167F68 File Offset: 0x00166168
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00167F70 File Offset: 0x00166170
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.modifiers;
				break;
			case 2:
				greenNode = this.returnType;
				break;
			case 3:
				greenNode = this.operatorKeyword;
				break;
			case 4:
				greenNode = this.operatorToken;
				break;
			case 5:
				greenNode = this.parameterList;
				break;
			case 6:
				greenNode = this.body;
				break;
			case 7:
				greenNode = this.expressionBody;
				break;
			case 8:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005C4A RID: 23626 RVA: 0x00167FFD File Offset: 0x001661FD
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new OperatorDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005C4B RID: 23627 RVA: 0x00168007 File Offset: 0x00166207
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOperatorDeclaration(this);
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x00168010 File Offset: 0x00166210
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOperatorDeclaration(this);
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x0016801C File Offset: 0x0016621C
		public OperatorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || returnType != this.ReturnType || operatorKeyword != this.OperatorKeyword || operatorToken != this.OperatorToken || parameterList != this.ParameterList || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				OperatorDeclarationSyntax operatorDeclarationSyntax = SyntaxFactory.OperatorDeclaration(attributeLists, modifiers, returnType, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					operatorDeclarationSyntax = operatorDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					operatorDeclarationSyntax = operatorDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return operatorDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x001680D0 File Offset: 0x001662D0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new OperatorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.returnType, this.operatorKeyword, this.operatorToken, this.parameterList, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x00168128 File Offset: 0x00166328
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new OperatorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.returnType, this.operatorKeyword, this.operatorToken, this.parameterList, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x00168180 File Offset: 0x00166380
		internal OperatorDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 9;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.returnType = typeSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.operatorToken = syntaxToken2;
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(parameterListSyntax);
			this.parameterList = parameterListSyntax;
			BlockSyntax blockSyntax = (BlockSyntax)reader.ReadValue();
			if (blockSyntax != null)
			{
				base.AdjustFlagsAndWidth(blockSyntax);
				this.body = blockSyntax;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = (ArrowExpressionClauseSyntax)reader.ReadValue();
			if (arrowExpressionClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(arrowExpressionClauseSyntax);
				this.expressionBody = arrowExpressionClauseSyntax;
			}
			SyntaxToken syntaxToken3 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken3 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken3);
				this.semicolonToken = syntaxToken3;
			}
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x001682A8 File Offset: 0x001664A8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.returnType);
			writer.WriteValue(this.operatorKeyword);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x00168328 File Offset: 0x00166528
		static OperatorDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(OperatorDeclarationSyntax), (ObjectReader r) => new OperatorDeclarationSyntax(r));
		}

		// Token: 0x0400184B RID: 6219
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x0400184C RID: 6220
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x0400184D RID: 6221
		internal readonly TypeSyntax returnType;

		// Token: 0x0400184E RID: 6222
		internal readonly SyntaxToken operatorKeyword;

		// Token: 0x0400184F RID: 6223
		internal readonly SyntaxToken operatorToken;

		// Token: 0x04001850 RID: 6224
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x04001851 RID: 6225
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BlockSyntax body;

		// Token: 0x04001852 RID: 6226
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x04001853 RID: 6227
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
