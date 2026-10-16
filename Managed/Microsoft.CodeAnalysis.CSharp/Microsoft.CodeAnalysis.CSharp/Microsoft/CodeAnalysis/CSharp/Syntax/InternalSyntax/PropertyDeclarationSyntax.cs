using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000442 RID: 1090
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PropertyDeclarationSyntax : BasePropertyDeclarationSyntax
	{
		// Token: 0x06005CA9 RID: 23721 RVA: 0x00169850 File Offset: 0x00167A50
		internal PropertyDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (accessorList != null)
			{
				base.AdjustFlagsAndWidth(accessorList);
				this.accessorList = accessorList;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x00169918 File Offset: 0x00167B18
		internal PropertyDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (accessorList != null)
			{
				base.AdjustFlagsAndWidth(accessorList);
				this.accessorList = accessorList;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x001699E4 File Offset: 0x00167BE4
		internal PropertyDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
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
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (explicitInterfaceSpecifier != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifier);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifier;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (accessorList != null)
			{
				base.AdjustFlagsAndWidth(accessorList);
				this.accessorList = accessorList;
			}
			if (expressionBody != null)
			{
				base.AdjustFlagsAndWidth(expressionBody);
				this.expressionBody = expressionBody;
			}
			if (initializer != null)
			{
				base.AdjustFlagsAndWidth(initializer);
				this.initializer = initializer;
			}
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06005CAC RID: 23724 RVA: 0x00169AA6 File Offset: 0x00167CA6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x00169AB3 File Offset: 0x00167CB3
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06005CAE RID: 23726 RVA: 0x00169AC0 File Offset: 0x00167CC0
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06005CAF RID: 23727 RVA: 0x00169AC8 File Offset: 0x00167CC8
		public override ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			get
			{
				return this.explicitInterfaceSpecifier;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06005CB0 RID: 23728 RVA: 0x00169AD0 File Offset: 0x00167CD0
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06005CB1 RID: 23729 RVA: 0x00169AD8 File Offset: 0x00167CD8
		public override AccessorListSyntax AccessorList
		{
			get
			{
				return this.accessorList;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06005CB2 RID: 23730 RVA: 0x00169AE0 File Offset: 0x00167CE0
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06005CB3 RID: 23731 RVA: 0x00169AE8 File Offset: 0x00167CE8
		public EqualsValueClauseSyntax Initializer
		{
			get
			{
				return this.initializer;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06005CB4 RID: 23732 RVA: 0x00169AF0 File Offset: 0x00167CF0
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x00169AF8 File Offset: 0x00167CF8
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
				greenNode = this.type;
				break;
			case 3:
				greenNode = this.explicitInterfaceSpecifier;
				break;
			case 4:
				greenNode = this.identifier;
				break;
			case 5:
				greenNode = this.accessorList;
				break;
			case 6:
				greenNode = this.expressionBody;
				break;
			case 7:
				greenNode = this.initializer;
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

		// Token: 0x06005CB6 RID: 23734 RVA: 0x00169B85 File Offset: 0x00167D85
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PropertyDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x00169B8F File Offset: 0x00167D8F
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPropertyDeclaration(this);
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00169B98 File Offset: 0x00167D98
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPropertyDeclaration(this);
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x00169BA4 File Offset: 0x00167DA4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public PropertyDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || type != this.Type || explicitInterfaceSpecifier != this.ExplicitInterfaceSpecifier || identifier != this.Identifier || accessorList != this.AccessorList || expressionBody != this.ExpressionBody || initializer != this.Initializer || semicolonToken != this.SemicolonToken)
			{
				PropertyDeclarationSyntax propertyDeclarationSyntax = SyntaxFactory.PropertyDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					propertyDeclarationSyntax = propertyDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					propertyDeclarationSyntax = propertyDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return propertyDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x00169C58 File Offset: 0x00167E58
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PropertyDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, this.explicitInterfaceSpecifier, this.identifier, this.accessorList, this.expressionBody, this.initializer, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x00169CB0 File Offset: 0x00167EB0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PropertyDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, this.explicitInterfaceSpecifier, this.identifier, this.accessorList, this.expressionBody, this.initializer, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x00169D08 File Offset: 0x00167F08
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal PropertyDeclarationSyntax(ObjectReader reader)
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
			this.type = typeSyntax;
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = (ExplicitInterfaceSpecifierSyntax)reader.ReadValue();
			if (explicitInterfaceSpecifierSyntax != null)
			{
				base.AdjustFlagsAndWidth(explicitInterfaceSpecifierSyntax);
				this.explicitInterfaceSpecifier = explicitInterfaceSpecifierSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			AccessorListSyntax accessorListSyntax = (AccessorListSyntax)reader.ReadValue();
			if (accessorListSyntax != null)
			{
				base.AdjustFlagsAndWidth(accessorListSyntax);
				this.accessorList = accessorListSyntax;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = (ArrowExpressionClauseSyntax)reader.ReadValue();
			if (arrowExpressionClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(arrowExpressionClauseSyntax);
				this.expressionBody = arrowExpressionClauseSyntax;
			}
			EqualsValueClauseSyntax equalsValueClauseSyntax = (EqualsValueClauseSyntax)reader.ReadValue();
			if (equalsValueClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(equalsValueClauseSyntax);
				this.initializer = equalsValueClauseSyntax;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.semicolonToken = syntaxToken2;
			}
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x00169E38 File Offset: 0x00168038
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.type);
			writer.WriteValue(this.explicitInterfaceSpecifier);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.accessorList);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.initializer);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x00169EB8 File Offset: 0x001680B8
		static PropertyDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PropertyDeclarationSyntax), (ObjectReader r) => new PropertyDeclarationSyntax(r));
		}

		// Token: 0x04001870 RID: 6256
		internal readonly GreenNode attributeLists;

		// Token: 0x04001871 RID: 6257
		internal readonly GreenNode modifiers;

		// Token: 0x04001872 RID: 6258
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly TypeSyntax type;

		// Token: 0x04001873 RID: 6259
		internal readonly ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x04001874 RID: 6260
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken identifier;

		// Token: 0x04001875 RID: 6261
		internal readonly AccessorListSyntax accessorList;

		// Token: 0x04001876 RID: 6262
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x04001877 RID: 6263
		internal readonly EqualsValueClauseSyntax initializer;

		// Token: 0x04001878 RID: 6264
		internal readonly SyntaxToken semicolonToken;
	}
}
