using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200037C RID: 892
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeArgumentListSyntax : CSharpSyntaxNode
	{
		// Token: 0x060050C9 RID: 20681 RVA: 0x001465F4 File Offset: 0x001447F4
		internal TypeArgumentListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken greaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00146644 File Offset: 0x00144844
		internal TypeArgumentListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken greaterThanToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x00146696 File Offset: 0x00144896
		internal TypeArgumentListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken greaterThanToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x001466D5 File Offset: 0x001448D5
		public SyntaxToken LessThanToken
		{
			get
			{
				return this.lessThanToken;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x060050CD RID: 20685 RVA: 0x001466DD File Offset: 0x001448DD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TypeSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<TypeSyntax>(new SyntaxList<CSharpSyntaxNode>(this.arguments));
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x060050CE RID: 20686 RVA: 0x001466F4 File Offset: 0x001448F4
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return this.greaterThanToken;
			}
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x001466FC File Offset: 0x001448FC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.lessThanToken;
				break;
			case 1:
				greenNode = this.arguments;
				break;
			case 2:
				greenNode = this.greaterThanToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x0014673B File Offset: 0x0014493B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeArgumentListSyntax(this, parent, position);
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00146745 File Offset: 0x00144945
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeArgumentList(this);
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x0014674E File Offset: 0x0014494E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeArgumentList(this);
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x00146758 File Offset: 0x00144958
		public TypeArgumentListSyntax Update(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
		{
			if (lessThanToken == this.LessThanToken)
			{
				SeparatedSyntaxList<TypeSyntax> separatedSyntaxList = this.Arguments;
				if (!((in arguments) != (in separatedSyntaxList)) && greaterThanToken == this.GreaterThanToken)
				{
					return this;
				}
			}
			TypeArgumentListSyntax typeArgumentListSyntax = SyntaxFactory.TypeArgumentList(lessThanToken, arguments, greaterThanToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				typeArgumentListSyntax = typeArgumentListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				typeArgumentListSyntax = typeArgumentListSyntax.WithAnnotationsGreen(annotations);
			}
			return typeArgumentListSyntax;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x001467C1 File Offset: 0x001449C1
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeArgumentListSyntax(base.Kind, this.lessThanToken, this.arguments, this.greaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x001467E7 File Offset: 0x001449E7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeArgumentListSyntax(base.Kind, this.lessThanToken, this.arguments, this.greaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00146810 File Offset: 0x00144A10
		internal TypeArgumentListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.lessThanToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.arguments = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.greaterThanToken = syntaxToken2;
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0014687C File Offset: 0x00144A7C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanToken);
			writer.WriteValue(this.arguments);
			writer.WriteValue(this.greaterThanToken);
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x001468A9 File Offset: 0x00144AA9
		static TypeArgumentListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeArgumentListSyntax), (ObjectReader r) => new TypeArgumentListSyntax(r));
		}

		// Token: 0x040015E0 RID: 5600
		internal readonly SyntaxToken lessThanToken;

		// Token: 0x040015E1 RID: 5601
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode arguments;

		// Token: 0x040015E2 RID: 5602
		internal readonly SyntaxToken greaterThanToken;
	}
}
