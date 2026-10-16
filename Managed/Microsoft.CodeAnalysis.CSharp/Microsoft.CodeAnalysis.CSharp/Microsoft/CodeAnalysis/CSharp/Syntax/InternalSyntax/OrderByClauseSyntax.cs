using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003CA RID: 970
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class OrderByClauseSyntax : QueryClauseSyntax
	{
		// Token: 0x0600553F RID: 21823 RVA: 0x0015241D File Offset: 0x0015061D
		internal OrderByClauseSyntax(SyntaxKind kind, SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode orderings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(orderByKeyword);
			this.orderByKeyword = orderByKeyword;
			if (orderings != null)
			{
				base.AdjustFlagsAndWidth(orderings);
				this.orderings = orderings;
			}
		}

		// Token: 0x06005540 RID: 21824 RVA: 0x00152450 File Offset: 0x00150650
		internal OrderByClauseSyntax(SyntaxKind kind, SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode orderings, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(orderByKeyword);
			this.orderByKeyword = orderByKeyword;
			if (orderings != null)
			{
				base.AdjustFlagsAndWidth(orderings);
				this.orderings = orderings;
			}
		}

		// Token: 0x06005541 RID: 21825 RVA: 0x00152487 File Offset: 0x00150687
		internal OrderByClauseSyntax(SyntaxKind kind, SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode orderings)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(orderByKeyword);
			this.orderByKeyword = orderByKeyword;
			if (orderings != null)
			{
				base.AdjustFlagsAndWidth(orderings);
				this.orderings = orderings;
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06005542 RID: 21826 RVA: 0x001524B6 File Offset: 0x001506B6
		public SyntaxToken OrderByKeyword
		{
			get
			{
				return this.orderByKeyword;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06005543 RID: 21827 RVA: 0x001524BE File Offset: 0x001506BE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<OrderingSyntax> Orderings
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<OrderingSyntax>(new SyntaxList<CSharpSyntaxNode>(this.orderings));
			}
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x001524D8 File Offset: 0x001506D8
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
					greenNode = this.orderings;
				}
			}
			else
			{
				greenNode = this.orderByKeyword;
			}
			return greenNode;
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x00152503 File Offset: 0x00150703
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new OrderByClauseSyntax(this, parent, position);
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x0015250D File Offset: 0x0015070D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitOrderByClause(this);
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x00152516 File Offset: 0x00150716
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitOrderByClause(this);
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x00152520 File Offset: 0x00150720
		public OrderByClauseSyntax Update(SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<OrderingSyntax> orderings)
		{
			if (orderByKeyword == this.OrderByKeyword)
			{
				SeparatedSyntaxList<OrderingSyntax> separatedSyntaxList = this.Orderings;
				if (!((in orderings) != (in separatedSyntaxList)))
				{
					return this;
				}
			}
			OrderByClauseSyntax orderByClauseSyntax = SyntaxFactory.OrderByClause(orderByKeyword, orderings);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				orderByClauseSyntax = orderByClauseSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				orderByClauseSyntax = orderByClauseSyntax.WithAnnotationsGreen(annotations);
			}
			return orderByClauseSyntax;
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x0015257F File Offset: 0x0015077F
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new OrderByClauseSyntax(base.Kind, this.orderByKeyword, this.orderings, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600554A RID: 21834 RVA: 0x0015259F File Offset: 0x0015079F
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new OrderByClauseSyntax(base.Kind, this.orderByKeyword, this.orderings, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x001525C0 File Offset: 0x001507C0
		internal OrderByClauseSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.orderByKeyword = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.orderings = greenNode;
			}
		}

		// Token: 0x0600554C RID: 21836 RVA: 0x00152612 File Offset: 0x00150812
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.orderByKeyword);
			writer.WriteValue(this.orderings);
		}

		// Token: 0x0600554D RID: 21837 RVA: 0x00152633 File Offset: 0x00150833
		static OrderByClauseSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(OrderByClauseSyntax), (ObjectReader r) => new OrderByClauseSyntax(r));
		}

		// Token: 0x040016B1 RID: 5809
		internal readonly SyntaxToken orderByKeyword;

		// Token: 0x040016B2 RID: 5810
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode orderings;
	}
}
