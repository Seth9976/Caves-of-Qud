using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200037B RID: 891
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class GenericNameSyntax : SimpleNameSyntax
	{
		// Token: 0x060050BA RID: 20666 RVA: 0x001463DF File Offset: 0x001445DF
		internal GenericNameSyntax(SyntaxKind kind, SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(typeArgumentList);
			this.typeArgumentList = typeArgumentList;
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0014640F File Offset: 0x0014460F
		internal GenericNameSyntax(SyntaxKind kind, SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(typeArgumentList);
			this.typeArgumentList = typeArgumentList;
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x00146443 File Offset: 0x00144643
		internal GenericNameSyntax(SyntaxKind kind, SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			base.AdjustFlagsAndWidth(typeArgumentList);
			this.typeArgumentList = typeArgumentList;
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x0014646F File Offset: 0x0014466F
		public override SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x00146477 File Offset: 0x00144677
		public TypeArgumentListSyntax TypeArgumentList
		{
			get
			{
				return this.typeArgumentList;
			}
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x00146480 File Offset: 0x00144680
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
					greenNode = this.typeArgumentList;
				}
			}
			else
			{
				greenNode = this.identifier;
			}
			return greenNode;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x001464AB File Offset: 0x001446AB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new GenericNameSyntax(this, parent, position);
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x001464B5 File Offset: 0x001446B5
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitGenericName(this);
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x001464BE File Offset: 0x001446BE
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitGenericName(this);
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x001464C8 File Offset: 0x001446C8
		public GenericNameSyntax Update(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
		{
			if (identifier != this.Identifier || typeArgumentList != this.TypeArgumentList)
			{
				GenericNameSyntax genericNameSyntax = SyntaxFactory.GenericName(identifier, typeArgumentList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					genericNameSyntax = genericNameSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					genericNameSyntax = genericNameSyntax.WithAnnotationsGreen(annotations);
				}
				return genericNameSyntax;
			}
			return this;
		}

		// Token: 0x060050C4 RID: 20676 RVA: 0x0014651E File Offset: 0x0014471E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new GenericNameSyntax(base.Kind, this.identifier, this.typeArgumentList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0014653E File Offset: 0x0014473E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new GenericNameSyntax(base.Kind, this.identifier, this.typeArgumentList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00146560 File Offset: 0x00144760
		internal GenericNameSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			TypeArgumentListSyntax typeArgumentListSyntax = (TypeArgumentListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeArgumentListSyntax);
			this.typeArgumentList = typeArgumentListSyntax;
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x001465AF File Offset: 0x001447AF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.typeArgumentList);
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x001465D0 File Offset: 0x001447D0
		static GenericNameSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(GenericNameSyntax), (ObjectReader r) => new GenericNameSyntax(r));
		}

		// Token: 0x040015DE RID: 5598
		internal readonly SyntaxToken identifier;

		// Token: 0x040015DF RID: 5599
		internal readonly TypeArgumentListSyntax typeArgumentList;
	}
}
