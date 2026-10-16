using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000445 RID: 1093
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class IndexerDeclarationSyntax : BasePropertyDeclarationSyntax
	{
		// Token: 0x06005CE3 RID: 23779 RVA: 0x0016A6D0 File Offset: 0x001688D0
		internal IndexerDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(thisKeyword);
			this.thisKeyword = thisKeyword;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
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
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x0016A794 File Offset: 0x00168994
		internal IndexerDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(thisKeyword);
			this.thisKeyword = thisKeyword;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
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
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x0016A85C File Offset: 0x00168A5C
		internal IndexerDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
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
			base.AdjustFlagsAndWidth(thisKeyword);
			this.thisKeyword = thisKeyword;
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
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
			if (semicolonToken != null)
			{
				base.AdjustFlagsAndWidth(semicolonToken);
				this.semicolonToken = semicolonToken;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x0016A91A File Offset: 0x00168B1A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06005CE7 RID: 23783 RVA: 0x0016A927 File Offset: 0x00168B27
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06005CE8 RID: 23784 RVA: 0x0016A934 File Offset: 0x00168B34
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.type;
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06005CE9 RID: 23785 RVA: 0x0016A93C File Offset: 0x00168B3C
		public override ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			get
			{
				return this.explicitInterfaceSpecifier;
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06005CEA RID: 23786 RVA: 0x0016A944 File Offset: 0x00168B44
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken ThisKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.thisKeyword;
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x06005CEB RID: 23787 RVA: 0x0016A94C File Offset: 0x00168B4C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public BracketedParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06005CEC RID: 23788 RVA: 0x0016A954 File Offset: 0x00168B54
		public override AccessorListSyntax AccessorList
		{
			get
			{
				return this.accessorList;
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x06005CED RID: 23789 RVA: 0x0016A95C File Offset: 0x00168B5C
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x0016A964 File Offset: 0x00168B64
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x0016A96C File Offset: 0x00168B6C
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
				greenNode = this.thisKeyword;
				break;
			case 5:
				greenNode = this.parameterList;
				break;
			case 6:
				greenNode = this.accessorList;
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

		// Token: 0x06005CF0 RID: 23792 RVA: 0x0016A9F9 File Offset: 0x00168BF9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new IndexerDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x0016AA03 File Offset: 0x00168C03
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitIndexerDeclaration(this);
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x0016AA0C File Offset: 0x00168C0C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitIndexerDeclaration(this);
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x0016AA18 File Offset: 0x00168C18
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public IndexerDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || type != this.Type || explicitInterfaceSpecifier != this.ExplicitInterfaceSpecifier || thisKeyword != this.ThisKeyword || parameterList != this.ParameterList || accessorList != this.AccessorList || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				IndexerDeclarationSyntax indexerDeclarationSyntax = SyntaxFactory.IndexerDeclaration(attributeLists, modifiers, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					indexerDeclarationSyntax = indexerDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					indexerDeclarationSyntax = indexerDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return indexerDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x0016AACC File Offset: 0x00168CCC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new IndexerDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, this.explicitInterfaceSpecifier, this.thisKeyword, this.parameterList, this.accessorList, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x0016AB24 File Offset: 0x00168D24
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new IndexerDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, this.explicitInterfaceSpecifier, this.thisKeyword, this.parameterList, this.accessorList, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x0016AB7C File Offset: 0x00168D7C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal IndexerDeclarationSyntax(ObjectReader reader)
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
			this.thisKeyword = syntaxToken;
			BracketedParameterListSyntax bracketedParameterListSyntax = (BracketedParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(bracketedParameterListSyntax);
			this.parameterList = bracketedParameterListSyntax;
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
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			if (syntaxToken2 != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken2);
				this.semicolonToken = syntaxToken2;
			}
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x0016ACA8 File Offset: 0x00168EA8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.type);
			writer.WriteValue(this.explicitInterfaceSpecifier);
			writer.WriteValue(this.thisKeyword);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.accessorList);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x0016AD28 File Offset: 0x00168F28
		static IndexerDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(IndexerDeclarationSyntax), (ObjectReader r) => new IndexerDeclarationSyntax(r));
		}

		// Token: 0x04001883 RID: 6275
		internal readonly GreenNode attributeLists;

		// Token: 0x04001884 RID: 6276
		internal readonly GreenNode modifiers;

		// Token: 0x04001885 RID: 6277
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly TypeSyntax type;

		// Token: 0x04001886 RID: 6278
		internal readonly ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier;

		// Token: 0x04001887 RID: 6279
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken thisKeyword;

		// Token: 0x04001888 RID: 6280
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly BracketedParameterListSyntax parameterList;

		// Token: 0x04001889 RID: 6281
		internal readonly AccessorListSyntax accessorList;

		// Token: 0x0400188A RID: 6282
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x0400188B RID: 6283
		internal readonly SyntaxToken semicolonToken;
	}
}
