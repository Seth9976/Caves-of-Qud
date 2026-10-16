using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200042F RID: 1071
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PrimaryConstructorBaseTypeSyntax : BaseTypeSyntax
	{
		// Token: 0x06005B89 RID: 23433 RVA: 0x00165E0B File Offset: 0x0016400B
		internal PrimaryConstructorBaseTypeSyntax(SyntaxKind kind, TypeSyntax type, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x00165E3B File Offset: 0x0016403B
		internal PrimaryConstructorBaseTypeSyntax(SyntaxKind kind, TypeSyntax type, ArgumentListSyntax argumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x00165E6F File Offset: 0x0016406F
		internal PrimaryConstructorBaseTypeSyntax(SyntaxKind kind, TypeSyntax type, ArgumentListSyntax argumentList)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			base.AdjustFlagsAndWidth(argumentList);
			this.argumentList = argumentList;
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06005B8C RID: 23436 RVA: 0x00165E9B File Offset: 0x0016409B
		public override TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06005B8D RID: 23437 RVA: 0x00165EA3 File Offset: 0x001640A3
		public ArgumentListSyntax ArgumentList
		{
			get
			{
				return this.argumentList;
			}
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x00165EAC File Offset: 0x001640AC
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
					greenNode = this.argumentList;
				}
			}
			else
			{
				greenNode = this.type;
			}
			return greenNode;
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x00165ED7 File Offset: 0x001640D7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new PrimaryConstructorBaseTypeSyntax(this, parent, position);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x00165EE1 File Offset: 0x001640E1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitPrimaryConstructorBaseType(this);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x00165EEA File Offset: 0x001640EA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitPrimaryConstructorBaseType(this);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x00165EF4 File Offset: 0x001640F4
		public PrimaryConstructorBaseTypeSyntax Update(TypeSyntax type, ArgumentListSyntax argumentList)
		{
			if (type != this.Type || argumentList != this.ArgumentList)
			{
				PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = SyntaxFactory.PrimaryConstructorBaseType(type, argumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					primaryConstructorBaseTypeSyntax = primaryConstructorBaseTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					primaryConstructorBaseTypeSyntax = primaryConstructorBaseTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return primaryConstructorBaseTypeSyntax;
			}
			return this;
		}

		// Token: 0x06005B93 RID: 23443 RVA: 0x00165F4A File Offset: 0x0016414A
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new PrimaryConstructorBaseTypeSyntax(base.Kind, this.type, this.argumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005B94 RID: 23444 RVA: 0x00165F6A File Offset: 0x0016416A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new PrimaryConstructorBaseTypeSyntax(base.Kind, this.type, this.argumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005B95 RID: 23445 RVA: 0x00165F8C File Offset: 0x0016418C
		internal PrimaryConstructorBaseTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(argumentListSyntax);
			this.argumentList = argumentListSyntax;
		}

		// Token: 0x06005B96 RID: 23446 RVA: 0x00165FDB File Offset: 0x001641DB
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
			writer.WriteValue(this.argumentList);
		}

		// Token: 0x06005B97 RID: 23447 RVA: 0x00165FFC File Offset: 0x001641FC
		static PrimaryConstructorBaseTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(PrimaryConstructorBaseTypeSyntax), (ObjectReader r) => new PrimaryConstructorBaseTypeSyntax(r));
		}

		// Token: 0x04001828 RID: 6184
		internal readonly TypeSyntax type;

		// Token: 0x04001829 RID: 6185
		internal readonly ArgumentListSyntax argumentList;
	}
}
