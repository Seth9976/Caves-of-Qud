using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200043E RID: 1086
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ConstructorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x06005C69 RID: 23657 RVA: 0x001689B8 File Offset: 0x00166BB8
		internal ConstructorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
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

		// Token: 0x06005C6A RID: 23658 RVA: 0x00168A6C File Offset: 0x00166C6C
		internal ConstructorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
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

		// Token: 0x06005C6B RID: 23659 RVA: 0x00168B24 File Offset: 0x00166D24
		internal ConstructorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 8;
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
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
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

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06005C6C RID: 23660 RVA: 0x00168BD1 File Offset: 0x00166DD1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06005C6D RID: 23661 RVA: 0x00168BDE File Offset: 0x00166DDE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06005C6E RID: 23662 RVA: 0x00168BEB File Offset: 0x00166DEB
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06005C6F RID: 23663 RVA: 0x00168BF3 File Offset: 0x00166DF3
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x06005C70 RID: 23664 RVA: 0x00168BFB File Offset: 0x00166DFB
		public ConstructorInitializerSyntax Initializer
		{
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x06005C71 RID: 23665 RVA: 0x00168C03 File Offset: 0x00166E03
		public override BlockSyntax Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x06005C72 RID: 23666 RVA: 0x00168C0B File Offset: 0x00166E0B
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x06005C73 RID: 23667 RVA: 0x00168C13 File Offset: 0x00166E13
		public override SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x00168C1C File Offset: 0x00166E1C
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
				greenNode = this.identifier;
				break;
			case 3:
				greenNode = this.parameterList;
				break;
			case 4:
				greenNode = this.initializer;
				break;
			case 5:
				greenNode = this.body;
				break;
			case 6:
				greenNode = this.expressionBody;
				break;
			case 7:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x00168C9C File Offset: 0x00166E9C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ConstructorDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x00168CA6 File Offset: 0x00166EA6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitConstructorDeclaration(this);
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x00168CAF File Offset: 0x00166EAF
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitConstructorDeclaration(this);
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x00168CB8 File Offset: 0x00166EB8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public ConstructorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken identifier, ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || identifier != this.Identifier || parameterList != this.ParameterList || initializer != this.Initializer || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				ConstructorDeclarationSyntax constructorDeclarationSyntax = SyntaxFactory.ConstructorDeclaration(attributeLists, modifiers, identifier, parameterList, initializer, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					constructorDeclarationSyntax = constructorDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					constructorDeclarationSyntax = constructorDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return constructorDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x00168D60 File Offset: 0x00166F60
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ConstructorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.identifier, this.parameterList, this.initializer, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x00168DB0 File Offset: 0x00166FB0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ConstructorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.identifier, this.parameterList, this.initializer, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x00168E00 File Offset: 0x00167000
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal ConstructorDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 8;
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
			this.identifier = syntaxToken;
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(parameterListSyntax);
			this.parameterList = parameterListSyntax;
			ConstructorInitializerSyntax constructorInitializerSyntax = (ConstructorInitializerSyntax)reader.ReadValue();
			if (constructorInitializerSyntax != null)
			{
				base.AdjustFlagsAndWidth(constructorInitializerSyntax);
				this.initializer = constructorInitializerSyntax;
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

		// Token: 0x06005C7C RID: 23676 RVA: 0x00168F10 File Offset: 0x00167110
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.initializer);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00168F84 File Offset: 0x00167184
		static ConstructorDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ConstructorDeclarationSyntax), (ObjectReader r) => new ConstructorDeclarationSyntax(r));
		}

		// Token: 0x0400185D RID: 6237
		internal readonly GreenNode attributeLists;

		// Token: 0x0400185E RID: 6238
		internal readonly GreenNode modifiers;

		// Token: 0x0400185F RID: 6239
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken identifier;

		// Token: 0x04001860 RID: 6240
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x04001861 RID: 6241
		internal readonly ConstructorInitializerSyntax initializer;

		// Token: 0x04001862 RID: 6242
		internal readonly BlockSyntax body;

		// Token: 0x04001863 RID: 6243
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x04001864 RID: 6244
		internal readonly SyntaxToken semicolonToken;
	}
}
