using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200042E RID: 1070
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SimpleBaseTypeSyntax : BaseTypeSyntax
	{
		// Token: 0x06005B7B RID: 23419 RVA: 0x00165C89 File Offset: 0x00163E89
		internal SimpleBaseTypeSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x00165CAA File Offset: 0x00163EAA
		internal SimpleBaseTypeSyntax(SyntaxKind kind, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x00165CCF File Offset: 0x00163ECF
		internal SimpleBaseTypeSyntax(SyntaxKind kind, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06005B7E RID: 23422 RVA: 0x00165CED File Offset: 0x00163EED
		public override TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x00165CF5 File Offset: 0x00163EF5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x00165D02 File Offset: 0x00163F02
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new SimpleBaseTypeSyntax(this, parent, position);
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x00165D0C File Offset: 0x00163F0C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitSimpleBaseType(this);
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x00165D15 File Offset: 0x00163F15
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitSimpleBaseType(this);
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x00165D20 File Offset: 0x00163F20
		public SimpleBaseTypeSyntax Update(TypeSyntax type)
		{
			if (type != this.Type)
			{
				SimpleBaseTypeSyntax simpleBaseTypeSyntax = SyntaxFactory.SimpleBaseType(type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					simpleBaseTypeSyntax = simpleBaseTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					simpleBaseTypeSyntax = simpleBaseTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return simpleBaseTypeSyntax;
			}
			return this;
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x00165D6C File Offset: 0x00163F6C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new SimpleBaseTypeSyntax(base.Kind, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x00165D86 File Offset: 0x00163F86
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new SimpleBaseTypeSyntax(base.Kind, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x00165DA0 File Offset: 0x00163FA0
		internal SimpleBaseTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x06005B87 RID: 23431 RVA: 0x00165DD5 File Offset: 0x00163FD5
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x00165DEA File Offset: 0x00163FEA
		static SimpleBaseTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(SimpleBaseTypeSyntax), (ObjectReader r) => new SimpleBaseTypeSyntax(r));
		}

		// Token: 0x04001827 RID: 6183
		internal readonly TypeSyntax type;
	}
}
