using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000434 RID: 1076
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeConstraintSyntax : TypeParameterConstraintSyntax
	{
		// Token: 0x06005BCB RID: 23499 RVA: 0x00166868 File Offset: 0x00164A68
		internal TypeConstraintSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x00166889 File Offset: 0x00164A89
		internal TypeConstraintSyntax(SyntaxKind kind, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x001668AE File Offset: 0x00164AAE
		internal TypeConstraintSyntax(SyntaxKind kind, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06005BCE RID: 23502 RVA: 0x001668CC File Offset: 0x00164ACC
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x001668D4 File Offset: 0x00164AD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x001668E1 File Offset: 0x00164AE1
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeConstraintSyntax(this, parent, position);
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x001668EB File Offset: 0x00164AEB
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeConstraint(this);
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x001668F4 File Offset: 0x00164AF4
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeConstraint(this);
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x00166900 File Offset: 0x00164B00
		public TypeConstraintSyntax Update(TypeSyntax type)
		{
			if (type != this.Type)
			{
				TypeConstraintSyntax typeConstraintSyntax = SyntaxFactory.TypeConstraint(type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					typeConstraintSyntax = typeConstraintSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					typeConstraintSyntax = typeConstraintSyntax.WithAnnotationsGreen(annotations);
				}
				return typeConstraintSyntax;
			}
			return this;
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x0016694C File Offset: 0x00164B4C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeConstraintSyntax(base.Kind, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x00166966 File Offset: 0x00164B66
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeConstraintSyntax(base.Kind, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x00166980 File Offset: 0x00164B80
		internal TypeConstraintSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x001669B5 File Offset: 0x00164BB5
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x001669CA File Offset: 0x00164BCA
		static TypeConstraintSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeConstraintSyntax), (ObjectReader r) => new TypeConstraintSyntax(r));
		}

		// Token: 0x04001833 RID: 6195
		internal readonly TypeSyntax type;
	}
}
