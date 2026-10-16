using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000440 RID: 1088
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class DestructorDeclarationSyntax : BaseMethodDeclarationSyntax
	{
		// Token: 0x06005C8E RID: 23694 RVA: 0x00169254 File Offset: 0x00167454
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal DestructorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken tildeToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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
			base.AdjustFlagsAndWidth(tildeToken);
			this.tildeToken = tildeToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
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

		// Token: 0x06005C8F RID: 23695 RVA: 0x00169304 File Offset: 0x00167504
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal DestructorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken tildeToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
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
			base.AdjustFlagsAndWidth(tildeToken);
			this.tildeToken = tildeToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
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

		// Token: 0x06005C90 RID: 23696 RVA: 0x001693B8 File Offset: 0x001675B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal DestructorDeclarationSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken tildeToken, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
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
			base.AdjustFlagsAndWidth(tildeToken);
			this.tildeToken = tildeToken;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
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

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06005C91 RID: 23697 RVA: 0x00169461 File Offset: 0x00167661
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06005C92 RID: 23698 RVA: 0x0016946E File Offset: 0x0016766E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06005C93 RID: 23699 RVA: 0x0016947B File Offset: 0x0016767B
		public SyntaxToken TildeToken
		{
			get
			{
				return this.tildeToken;
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06005C94 RID: 23700 RVA: 0x00169483 File Offset: 0x00167683
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06005C95 RID: 23701 RVA: 0x0016948B File Offset: 0x0016768B
		public override ParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06005C96 RID: 23702 RVA: 0x00169493 File Offset: 0x00167693
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override BlockSyntax Body
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.body;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x0016949B File Offset: 0x0016769B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ArrowExpressionClauseSyntax ExpressionBody
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.expressionBody;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x001694A3 File Offset: 0x001676A3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxToken SemicolonToken
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.semicolonToken;
			}
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x001694AC File Offset: 0x001676AC
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
				greenNode = this.tildeToken;
				break;
			case 3:
				greenNode = this.identifier;
				break;
			case 4:
				greenNode = this.parameterList;
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

		// Token: 0x06005C9A RID: 23706 RVA: 0x0016952C File Offset: 0x0016772C
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new DestructorDeclarationSyntax(this, parent, position);
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x00169536 File Offset: 0x00167736
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitDestructorDeclaration(this);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x0016953F File Offset: 0x0016773F
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitDestructorDeclaration(this);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x00169548 File Offset: 0x00167748
		public DestructorDeclarationSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || tildeToken != this.TildeToken || identifier != this.Identifier || parameterList != this.ParameterList || body != this.Body || expressionBody != this.ExpressionBody || semicolonToken != this.SemicolonToken)
			{
				DestructorDeclarationSyntax destructorDeclarationSyntax = SyntaxFactory.DestructorDeclaration(attributeLists, modifiers, tildeToken, identifier, parameterList, body, expressionBody, semicolonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					destructorDeclarationSyntax = destructorDeclarationSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					destructorDeclarationSyntax = destructorDeclarationSyntax.WithAnnotationsGreen(annotations);
				}
				return destructorDeclarationSyntax;
			}
			return this;
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x001695F0 File Offset: 0x001677F0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new DestructorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.tildeToken, this.identifier, this.parameterList, this.body, this.expressionBody, this.semicolonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00169640 File Offset: 0x00167840
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new DestructorDeclarationSyntax(base.Kind, this.attributeLists, this.modifiers, this.tildeToken, this.identifier, this.parameterList, this.body, this.expressionBody, this.semicolonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00169690 File Offset: 0x00167890
		internal DestructorDeclarationSyntax(ObjectReader reader)
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
			this.tildeToken = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
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

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0016979C File Offset: 0x0016799C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.tildeToken);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.parameterList);
			writer.WriteValue(this.body);
			writer.WriteValue(this.expressionBody);
			writer.WriteValue(this.semicolonToken);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x00169810 File Offset: 0x00167A10
		static DestructorDeclarationSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(DestructorDeclarationSyntax), (ObjectReader r) => new DestructorDeclarationSyntax(r));
		}

		// Token: 0x04001868 RID: 6248
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x04001869 RID: 6249
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode modifiers;

		// Token: 0x0400186A RID: 6250
		internal readonly SyntaxToken tildeToken;

		// Token: 0x0400186B RID: 6251
		internal readonly SyntaxToken identifier;

		// Token: 0x0400186C RID: 6252
		internal readonly ParameterListSyntax parameterList;

		// Token: 0x0400186D RID: 6253
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly BlockSyntax body;

		// Token: 0x0400186E RID: 6254
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly ArrowExpressionClauseSyntax expressionBody;

		// Token: 0x0400186F RID: 6255
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken semicolonToken;
	}
}
