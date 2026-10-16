using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200043D RID: 1085
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConversionOperatorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x06005C53 RID: 23635 RVA: 0x0016834C File Offset: 0x0016654C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ConversionOperatorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken implicitOrExplicitKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(implicitOrExplicitKeyword);
			this.implicitOrExplicitKeyword = implicitOrExplicitKeyword;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
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

		// Token: 0x06005C54 RID: 23636 RVA: 0x0016840C File Offset: 0x0016660C
		internal ConversionOperatorDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken, SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(implicitOrExplicitKeyword);
			this.implicitOrExplicitKeyword = implicitOrExplicitKeyword;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
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

		// Token: 0x06005C55 RID: 23637 RVA: 0x001684D0 File Offset: 0x001666D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal ConversionOperatorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken implicitOrExplicitKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken operatorKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
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
			base.AdjustFlagsAndWidth(implicitOrExplicitKeyword);
			this.implicitOrExplicitKeyword = implicitOrExplicitKeyword;
			base.AdjustFlagsAndWidth(operatorKeyword);
			this.operatorKeyword = operatorKeyword;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
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

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x0016858A File Offset: 0x0016678A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06005C57 RID: 23639 RVA: 0x00168597 File Offset: 0x00166797
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06005C58 RID: 23640 RVA: 0x001685A4 File Offset: 0x001667A4
		public SyntaxToken ImplicitOrExplicitKeyword
		{
			get
			{
				return this.implicitOrExplicitKeyword;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06005C59 RID: 23641 RVA: 0x001685AC File Offset: 0x001667AC
		public SyntaxToken OperatorKeyword
		{
			get
			{
				return this.operatorKeyword;
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x001685B4 File Offset: 0x001667B4
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x06005C5B RID: 23643 RVA: 0x001685BC File Offset: 0x001667BC
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x001685C4 File Offset: 0x001667C4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.body;
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06005C5D RID: 23645 RVA: 0x001685CC File Offset: 0x001667CC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06005C5E RID: 23646 RVA: 0x001685D4 File Offset: 0x001667D4
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x001685DC File Offset: 0x001667DC
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
				greenNode = this.implicitOrExplicitKeyword;
				break;
			case 3:
				greenNode = this.operatorKeyword;
				break;
			case 4:
				greenNode = this.type;
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

		// Token: 0x06005C60 RID: 23648 RVA: 0x00168669 File Offset: 0x00166869
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConversionOperatorDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005C61 RID: 23649 RVA: 0x00168673 File Offset: 0x00166873
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConversionOperatorDeclaration(this);
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x0016867C File Offset: 0x0016687C
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConversionOperatorDeclaration(this);
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x00168688 File Offset: 0x00166888
		public ConversionOperatorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || implicitOrExplicitKeyword != this.ImplicitOrExplicitKeyword || operatorKeyword != this.OperatorKeyword || type != this.Type || parameterList != this.ParameterList || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				ConversionOperatorDeclarationSyntax conversionOperatorDeclarationSyntax = SyntaxFactory.ConversionOperatorDeclaration(attributeLists, modifiers, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					conversionOperatorDeclarationSyntax = conversionOperatorDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					conversionOperatorDeclarationSyntax = conversionOperatorDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return conversionOperatorDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x0016873C File Offset: 0x0016693C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConversionOperatorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.implicitOrExplicitKeyword, this.operatorKeyword, this.type, this.parameterList, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x00168794 File Offset: 0x00166994
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConversionOperatorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.implicitOrExplicitKeyword, this.operatorKeyword, this.type, this.parameterList, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x001687EC File Offset: 0x001669EC
		internal ConversionOperatorDeclarationSyntax(ObjectReader reader)
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
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.implicitOrExplicitKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.operatorKeyword = syntaxToken2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
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

		// Token: 0x06005C67 RID: 23655 RVA: 0x00168914 File Offset: 0x00166B14
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.implicitOrExplicitKeyword);
			writer.WriteValue(this.operatorKeyword);
			writer.WriteValue(this.type);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x00168994 File Offset: 0x00166B94
		static ConversionOperatorDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConversionOperatorDeclarationSyntax), (ObjectReader r) => new ConversionOperatorDeclarationSyntax(r));
		}

		// Token: 0x04001854 RID: 6228
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001855 RID: 6229
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x04001856 RID: 6230
		internal readonly SyntaxToken implicitOrExplicitKeyword;

		// Token: 0x04001857 RID: 6231
		internal readonly SyntaxToken operatorKeyword;

		// Token: 0x04001858 RID: 6232
		internal readonly TypeSyntax type;

		// Token: 0x04001859 RID: 6233
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x0400185A RID: 6234
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BlockSyntax body;

		// Token: 0x0400185B RID: 6235
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x0400185C RID: 6236
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
