using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200043B RID: 1083
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class MethodDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x06005C25 RID: 23589 RVA: 0x00167530 File Offset: 0x00165730
		internal MethodDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, GreenNode constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 11;
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
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
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

		// Token: 0x06005C26 RID: 23590 RVA: 0x0016761C File Offset: 0x0016581C
		internal MethodDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, GreenNode constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 11;
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
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
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

		// Token: 0x06005C27 RID: 23591 RVA: 0x0016770C File Offset: 0x0016590C
		internal MethodDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, GreenNode constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 11;
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
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (typeParameterList != null)
			{
				base.AdjustFlagsAndWidth(typeParameterList);
				this.typeParameterList = typeParameterList;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (constraintClauses != null)
			{
				base.AdjustFlagsAndWidth(constraintClauses);
				this.constraintClauses = constraintClauses;
			}
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

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x06005C28 RID: 23592 RVA: 0x001677F2 File Offset: 0x001659F2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x001677FF File Offset: 0x001659FF
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700154E RID: 5454
		// (get) Token: 0x06005C2A RID: 23594 RVA: 0x0016780C File Offset: 0x00165A0C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public TypeSyntax ReturnType
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700154F RID: 5455
		// (get) Token: 0x06005C2B RID: 23595 RVA: 0x00167814 File Offset: 0x00165A14
		public ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			get
			{
				return this.explicitInterfaceSpecifier;
			}
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x06005C2C RID: 23596 RVA: 0x0016781C File Offset: 0x00165A1C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001551 RID: 5457
		// (get) Token: 0x06005C2D RID: 23597 RVA: 0x00167824 File Offset: 0x00165A24
		public TypeParameterListSyntax TypeParameterList
		{
			get
			{
				return this.typeParameterList;
			}
		}

		// Token: 0x17001552 RID: 5458
		// (get) Token: 0x06005C2E RID: 23598 RVA: 0x0016782C File Offset: 0x00165A2C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x17001553 RID: 5459
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x00167834 File Offset: 0x00165A34
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<TypeParameterConstraintClauseSyntax>(this.constraintClauses);
			}
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06005C30 RID: 23600 RVA: 0x00167841 File Offset: 0x00165A41
		public override BlockSyntax Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x00167849 File Offset: 0x00165A49
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06005C32 RID: 23602 RVA: 0x00167851 File Offset: 0x00165A51
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x0016785C File Offset: 0x00165A5C
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
				greenNode = this.explicitInterfaceSpecifier;
				break;
			case 4:
				greenNode = this.identifier;
				break;
			case 5:
				greenNode = this.typeParameterList;
				break;
			case 6:
				greenNode = this.parameterList;
				break;
			case 7:
				greenNode = this.constraintClauses;
				break;
			case 8:
				greenNode = this.body;
				break;
			case 9:
				greenNode = this.expressionBody;
				break;
			case 10:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00167903 File Offset: 0x00165B03
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new MethodDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x0016790D File Offset: 0x00165B0D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMethodDeclaration(this);
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x00167916 File Offset: 0x00165B16
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMethodDeclaration(this);
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x00167920 File Offset: 0x00165B20
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public MethodDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || returnType != this.ReturnType || explicitInterfaceSpecifier != this.ExplicitInterfaceSpecifier || identifier != this.Identifier || typeParameterList != this.TypeParameterList || parameterList != this.ParameterList || constraintClauses != this.ConstraintClauses || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				MethodDeclarationSyntax methodDeclarationSyntax = SyntaxFactory.MethodDeclaration(attributeLists, modifiers, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					methodDeclarationSyntax = methodDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					methodDeclarationSyntax = methodDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return methodDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x001679F0 File Offset: 0x00165BF0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new MethodDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.returnType, this.explicitInterfaceSpecifier, this.identifier, this.typeParameterList, this.parameterList, this.constraintClauses, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x00167A54 File Offset: 0x00165C54
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new MethodDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.returnType, this.explicitInterfaceSpecifier, this.identifier, this.typeParameterList, this.parameterList, this.constraintClauses, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x00167AB8 File Offset: 0x00165CB8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal MethodDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 11;
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
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = (ExplicitInterfaceSpecifierSyntax)reader.ReadValue();
			if (explicitInterfaceSpecifierSyntax != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifierSyntax);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifierSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)reader.ReadValue();
			if (typeParameterListSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeParameterListSyntax);
				this.typeParameterList = typeParameterListSyntax;
			}
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(parameterListSyntax);
			this.parameterList = parameterListSyntax;
			GreenNode greenNode3 = (GreenNode)reader.ReadValue();
			if (greenNode3 != null)
			{
				base.AdjustFlagsAndWidth(greenNode3);
				this.constraintClauses = greenNode3;
			}
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
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.semicolonToken = syntaxToken2;
			}
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x00167C24 File Offset: 0x00165E24
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.returnType);
			writer.WriteValue(this.explicitInterfaceSpecifier);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeParameterList);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.constraintClauses);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x00167CBC File Offset: 0x00165EBC
		static MethodDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(MethodDeclarationSyntax), (ObjectReader r) => new MethodDeclarationSyntax(r));
		}

		// Token: 0x04001840 RID: 6208
		internal readonly GreenNode attributeLists;

		// Token: 0x04001841 RID: 6209
		internal readonly GreenNode modifiers;

		// Token: 0x04001842 RID: 6210
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly TypeSyntax returnType;

		// Token: 0x04001843 RID: 6211
		internal readonly ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x04001844 RID: 6212
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken identifier;

		// Token: 0x04001845 RID: 6213
		internal readonly TypeParameterListSyntax typeParameterList;

		// Token: 0x04001846 RID: 6214
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x04001847 RID: 6215
		internal readonly GreenNode constraintClauses;

		// Token: 0x04001848 RID: 6216
		internal readonly BlockSyntax body;

		// Token: 0x04001849 RID: 6217
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x0400184A RID: 6218
		internal readonly SyntaxToken semicolonToken;
	}
}
