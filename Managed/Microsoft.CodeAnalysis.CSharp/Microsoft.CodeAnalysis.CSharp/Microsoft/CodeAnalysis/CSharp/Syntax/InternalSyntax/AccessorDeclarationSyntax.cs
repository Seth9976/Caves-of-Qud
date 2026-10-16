using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000447 RID: 1095
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AccessorDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005D09 RID: 23817 RVA: 0x0016B018 File Offset: 0x00169218
		internal AccessorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 6;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
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

		// Token: 0x06005D0A RID: 23818 RVA: 0x0016B0A8 File Offset: 0x001692A8
		internal AccessorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 6;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
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

		// Token: 0x06005D0B RID: 23819 RVA: 0x0016B13C File Offset: 0x0016933C
		internal AccessorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
			: base(kind)
		{
			base.SlotCount = 6;
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
			base.AdjustFlagsAndWidth(keyword);
			this.keyword = keyword;
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

		// Token: 0x1700159E RID: 5534
		// (get) Token: 0x06005D0C RID: 23820 RVA: 0x0016B1C5 File Offset: 0x001693C5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x1700159F RID: 5535
		// (get) Token: 0x06005D0D RID: 23821 RVA: 0x0016B1D2 File Offset: 0x001693D2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170015A0 RID: 5536
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x0016B1DF File Offset: 0x001693DF
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken Keyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x170015A1 RID: 5537
		// (get) Token: 0x06005D0F RID: 23823 RVA: 0x0016B1E7 File Offset: 0x001693E7
		public BlockSyntax Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x0016B1EF File Offset: 0x001693EF
		public ArrowExpressionClauseSyntax ExpressionBody
		{
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06005D11 RID: 23825 RVA: 0x0016B1F7 File Offset: 0x001693F7
		public SyntaxToken SemicolonToken
		{
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x0016B200 File Offset: 0x00169400
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
				greenNode = this.keyword;
				break;
			case 3:
				greenNode = this.body;
				break;
			case 4:
				greenNode = this.expressionBody;
				break;
			case 5:
				greenNode = this.semicolonToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x0016B266 File Offset: 0x00169466
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AccessorDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x0016B270 File Offset: 0x00169470
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAccessorDeclaration(this);
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x0016B279 File Offset: 0x00169479
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAccessorDeclaration(this);
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x0016B284 File Offset: 0x00169484
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AccessorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || keyword != this.Keyword || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				AccessorDeclarationSyntax accessorDeclarationSyntax = SyntaxFactory.AccessorDeclaration(base.Kind, attributeLists, modifiers, keyword, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					accessorDeclarationSyntax = accessorDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					accessorDeclarationSyntax = accessorDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return accessorDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x0016B318 File Offset: 0x00169518
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AccessorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x0016B35C File Offset: 0x0016955C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AccessorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.keyword, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x0016B3A0 File Offset: 0x001695A0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal AccessorDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 6;
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
			this.keyword = syntaxToken;
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

		// Token: 0x06005D1A RID: 23834 RVA: 0x0016B470 File Offset: 0x00169670
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.keyword);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x0016B4CC File Offset: 0x001696CC
		static AccessorDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AccessorDeclarationSyntax), (ObjectReader r) => new AccessorDeclarationSyntax(r));
		}

		// Token: 0x0400188F RID: 6287
		internal readonly GreenNode attributeLists;

		// Token: 0x04001890 RID: 6288
		internal readonly GreenNode modifiers;

		// Token: 0x04001891 RID: 6289
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken keyword;

		// Token: 0x04001892 RID: 6290
		internal readonly BlockSyntax body;

		// Token: 0x04001893 RID: 6291
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x04001894 RID: 6292
		internal readonly SyntaxToken semicolonToken;
	}
}
