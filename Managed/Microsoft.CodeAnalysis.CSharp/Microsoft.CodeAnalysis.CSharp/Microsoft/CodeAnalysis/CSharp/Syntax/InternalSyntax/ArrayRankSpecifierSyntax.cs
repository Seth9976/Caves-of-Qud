using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000380 RID: 896
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArrayRankSpecifierSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005106 RID: 20742 RVA: 0x00146F18 File Offset: 0x00145118
		internal ArrayRankSpecifierSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode sizes, SyntaxToken closeBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (sizes != null)
			{
				base.AdjustFlagsAndWidth(sizes);
				this.sizes = sizes;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x00146F68 File Offset: 0x00145168
		internal ArrayRankSpecifierSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode sizes, SyntaxToken closeBracketToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (sizes != null)
			{
				base.AdjustFlagsAndWidth(sizes);
				this.sizes = sizes;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x00146FBA File Offset: 0x001451BA
		internal ArrayRankSpecifierSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode sizes, SyntaxToken closeBracketToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (sizes != null)
			{
				base.AdjustFlagsAndWidth(sizes);
				this.sizes = sizes;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x00146FF9 File Offset: 0x001451F9
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x00147001 File Offset: 0x00145201
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ExpressionSyntax> Sizes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ExpressionSyntax>(new SyntaxList<CSharpSyntaxNode>(this.sizes));
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x0600510B RID: 20747 RVA: 0x00147018 File Offset: 0x00145218
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x00147020 File Offset: 0x00145220
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBracketToken;
				break;
			case 1:
				greenNode = this.sizes;
				break;
			case 2:
				greenNode = this.closeBracketToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x0014705F File Offset: 0x0014525F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ArrayRankSpecifierSyntax(this, parent, position);
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x00147069 File Offset: 0x00145269
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrayRankSpecifier(this);
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x00147072 File Offset: 0x00145272
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrayRankSpecifier(this);
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x0014707C File Offset: 0x0014527C
		public ArrayRankSpecifierSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
		{
			if (openBracketToken == this.OpenBracketToken)
			{
				SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList = this.Sizes;
				if (!((in sizes) != (in separatedSyntaxList)) && closeBracketToken == this.CloseBracketToken)
				{
					return this;
				}
			}
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = SyntaxFactory.ArrayRankSpecifier(openBracketToken, sizes, closeBracketToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				arrayRankSpecifierSyntax = arrayRankSpecifierSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				arrayRankSpecifierSyntax = arrayRankSpecifierSyntax.WithAnnotationsGreen(annotations);
			}
			return arrayRankSpecifierSyntax;
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x001470E5 File Offset: 0x001452E5
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ArrayRankSpecifierSyntax(base.Kind, this.openBracketToken, this.sizes, this.closeBracketToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x0014710B File Offset: 0x0014530B
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ArrayRankSpecifierSyntax(base.Kind, this.openBracketToken, this.sizes, this.closeBracketToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00147134 File Offset: 0x00145334
		internal ArrayRankSpecifierSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBracketToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.sizes = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBracketToken = syntaxToken2;
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x001471A0 File Offset: 0x001453A0
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.sizes);
			writer.WriteValue(this.closeBracketToken);
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x001471CD File Offset: 0x001453CD
		static ArrayRankSpecifierSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ArrayRankSpecifierSyntax), (ObjectReader r) => new ArrayRankSpecifierSyntax(r));
		}

		// Token: 0x040015E9 RID: 5609
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x040015EA RID: 5610
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode sizes;

		// Token: 0x040015EB RID: 5611
		internal readonly SyntaxToken closeBracketToken;
	}
}
