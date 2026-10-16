using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200037F RID: 895
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ArrayTypeSyntax : TypeSyntax
	{
		// Token: 0x060050F7 RID: 20727 RVA: 0x00146CEF File Offset: 0x00144EEF
		internal ArrayTypeSyntax(SyntaxKind kind, TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode rankSpecifiers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			if (rankSpecifiers != null)
			{
				base.AdjustFlagsAndWidth(rankSpecifiers);
				this.rankSpecifiers = rankSpecifiers;
			}
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x00146D22 File Offset: 0x00144F22
		internal ArrayTypeSyntax(SyntaxKind kind, TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode rankSpecifiers, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			if (rankSpecifiers != null)
			{
				base.AdjustFlagsAndWidth(rankSpecifiers);
				this.rankSpecifiers = rankSpecifiers;
			}
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x00146D59 File Offset: 0x00144F59
		internal ArrayTypeSyntax(SyntaxKind kind, TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode rankSpecifiers)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(elementType);
			this.elementType = elementType;
			if (rankSpecifiers != null)
			{
				base.AdjustFlagsAndWidth(rankSpecifiers);
				this.rankSpecifiers = rankSpecifiers;
			}
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x00146D88 File Offset: 0x00144F88
		public TypeSyntax ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x060050FB RID: 20731 RVA: 0x00146D90 File Offset: 0x00144F90
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<ArrayRankSpecifierSyntax> RankSpecifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<ArrayRankSpecifierSyntax>(this.rankSpecifiers);
			}
		}

		// Token: 0x060050FC RID: 20732 RVA: 0x00146DA0 File Offset: 0x00144FA0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.rankSpecifiers;
				}
			}
			else
			{
				greenNode = this.elementType;
			}
			return greenNode;
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x00146DCB File Offset: 0x00144FCB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ArrayTypeSyntax(this, parent, position);
		}

		// Token: 0x060050FE RID: 20734 RVA: 0x00146DD5 File Offset: 0x00144FD5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitArrayType(this);
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x00146DDE File Offset: 0x00144FDE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitArrayType(this);
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x00146DE8 File Offset: 0x00144FE8
		public ArrayTypeSyntax Update(TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
		{
			if (elementType != this.ElementType || rankSpecifiers != this.RankSpecifiers)
			{
				ArrayTypeSyntax arrayTypeSyntax = SyntaxFactory.ArrayType(elementType, rankSpecifiers);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					arrayTypeSyntax = arrayTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					arrayTypeSyntax = arrayTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return arrayTypeSyntax;
			}
			return this;
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x00146E43 File Offset: 0x00145043
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ArrayTypeSyntax(base.Kind, this.elementType, this.rankSpecifiers, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x00146E63 File Offset: 0x00145063
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ArrayTypeSyntax(base.Kind, this.elementType, this.rankSpecifiers, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x00146E84 File Offset: 0x00145084
		internal ArrayTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.elementType = typeSyntax;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.rankSpecifiers = greenNode;
			}
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x00146ED6 File Offset: 0x001450D6
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.elementType);
			writer.WriteValue(this.rankSpecifiers);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x00146EF7 File Offset: 0x001450F7
		static ArrayTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ArrayTypeSyntax), (ObjectReader r) => new ArrayTypeSyntax(r));
		}

		// Token: 0x040015E7 RID: 5607
		internal readonly TypeSyntax elementType;

		// Token: 0x040015E8 RID: 5608
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode rankSpecifiers;
	}
}
