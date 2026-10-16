using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000452 RID: 1106
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeCrefSyntax : CrefSyntax
	{
		// Token: 0x06005D98 RID: 23960 RVA: 0x0016C8D9 File Offset: 0x0016AAD9
		internal TypeCrefSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x0016C8FA File Offset: 0x0016AAFA
		internal TypeCrefSyntax(SyntaxKind kind, TypeSyntax type, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x0016C91F File Offset: 0x0016AB1F
		internal TypeCrefSyntax(SyntaxKind kind, TypeSyntax type)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06005D9B RID: 23963 RVA: 0x0016C93D File Offset: 0x0016AB3D
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x0016C945 File Offset: 0x0016AB45
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.type;
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x0016C952 File Offset: 0x0016AB52
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeCrefSyntax(this, parent, position);
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x0016C95C File Offset: 0x0016AB5C
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeCref(this);
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x0016C965 File Offset: 0x0016AB65
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeCref(this);
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x0016C970 File Offset: 0x0016AB70
		public TypeCrefSyntax Update(TypeSyntax type)
		{
			if (type != this.Type)
			{
				TypeCrefSyntax typeCrefSyntax = SyntaxFactory.TypeCref(type);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					typeCrefSyntax = typeCrefSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					typeCrefSyntax = typeCrefSyntax.WithAnnotationsGreen(annotations);
				}
				return typeCrefSyntax;
			}
			return this;
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x0016C9BC File Offset: 0x0016ABBC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeCrefSyntax(base.Kind, this.type, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x0016C9D6 File Offset: 0x0016ABD6
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeCrefSyntax(base.Kind, this.type, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x0016C9F0 File Offset: 0x0016ABF0
		internal TypeCrefSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x0016CA25 File Offset: 0x0016AC25
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x0016CA3A File Offset: 0x0016AC3A
		static TypeCrefSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeCrefSyntax), (ObjectReader r) => new TypeCrefSyntax(r));
		}

		// Token: 0x040018A9 RID: 6313
		internal readonly TypeSyntax type;
	}
}
