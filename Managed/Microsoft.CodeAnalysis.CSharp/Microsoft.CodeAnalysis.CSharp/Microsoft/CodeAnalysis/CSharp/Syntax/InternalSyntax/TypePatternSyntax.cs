using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003DF RID: 991
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypePatternSyntax : PatternSyntax
	{
		// Token: 0x06005675 RID: 22133 RVA: 0x001552CD File Offset: 0x001534CD
		internal TypePatternSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005676 RID: 22134 RVA: 0x001552EE File Offset: 0x001534EE
		internal TypePatternSyntax(SyntaxKind kind, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005677 RID: 22135 RVA: 0x00155313 File Offset: 0x00153513
		internal TypePatternSyntax(SyntaxKind kind, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06005678 RID: 22136 RVA: 0x00155331 File Offset: 0x00153531
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x00155339 File Offset: 0x00153539
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x0600567A RID: 22138 RVA: 0x00155346 File Offset: 0x00153546
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypePatternSyntax(this, parent, position);
		}

		// Token: 0x0600567B RID: 22139 RVA: 0x00155350 File Offset: 0x00153550
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypePattern(this);
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x00155359 File Offset: 0x00153559
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypePattern(this);
		}

		// Token: 0x0600567D RID: 22141 RVA: 0x00155364 File Offset: 0x00153564
		public TypePatternSyntax Update(TypeSyntax type)
		{
			if (type != this.Type)
			{
				TypePatternSyntax typePatternSyntax = SyntaxFactory.TypePattern(type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					typePatternSyntax = typePatternSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					typePatternSyntax = typePatternSyntax.WithAnnotationsGreen(annotations);
				}
				return typePatternSyntax;
			}
			return this;
		}

		// Token: 0x0600567E RID: 22142 RVA: 0x001553B0 File Offset: 0x001535B0
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypePatternSyntax(base.Kind, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600567F RID: 22143 RVA: 0x001553CA File Offset: 0x001535CA
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypePatternSyntax(base.Kind, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005680 RID: 22144 RVA: 0x001553E4 File Offset: 0x001535E4
		internal TypePatternSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x06005681 RID: 22145 RVA: 0x00155419 File Offset: 0x00153619
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0015542E File Offset: 0x0015362E
		static TypePatternSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypePatternSyntax), (ObjectReader r) => new TypePatternSyntax(r));
		}

		// Token: 0x040016E0 RID: 5856
		internal readonly TypeSyntax type;
	}
}
