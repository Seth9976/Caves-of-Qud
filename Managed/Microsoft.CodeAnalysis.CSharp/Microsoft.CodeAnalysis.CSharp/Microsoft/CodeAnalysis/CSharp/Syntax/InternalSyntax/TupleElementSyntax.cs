using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000389 RID: 905
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TupleElementSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005192 RID: 20882 RVA: 0x00148586 File Offset: 0x00146786
		internal TupleElementSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (identifier != null)
			{
				base.AdjustFlagsAndWidth(identifier);
				this.identifier = identifier;
			}
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x001485B9 File Offset: 0x001467B9
		internal TupleElementSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (identifier != null)
			{
				base.AdjustFlagsAndWidth(identifier);
				this.identifier = identifier;
			}
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x001485F0 File Offset: 0x001467F0
		internal TupleElementSyntax(SyntaxKind kind, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(type);
			this.type = type;
			if (identifier != null)
			{
				base.AdjustFlagsAndWidth(identifier);
				this.identifier = identifier;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06005195 RID: 20885 RVA: 0x0014861F File Offset: 0x0014681F
		public TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x00148627 File Offset: 0x00146827
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x00148630 File Offset: 0x00146830
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
					greenNode = this.identifier;
				}
			}
			else
			{
				greenNode = this.type;
			}
			return greenNode;
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0014865B File Offset: 0x0014685B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TupleElementSyntax(this, parent, position);
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00148665 File Offset: 0x00146865
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTupleElement(this);
		}

		// Token: 0x0600519A RID: 20890 RVA: 0x0014866E File Offset: 0x0014686E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTupleElement(this);
		}

		// Token: 0x0600519B RID: 20891 RVA: 0x00148678 File Offset: 0x00146878
		public TupleElementSyntax Update(TypeSyntax type, SyntaxToken identifier)
		{
			if (type != this.Type || identifier != this.Identifier)
			{
				TupleElementSyntax tupleElementSyntax = SyntaxFactory.TupleElement(type, identifier);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					tupleElementSyntax = tupleElementSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					tupleElementSyntax = tupleElementSyntax.WithAnnotationsGreen(annotations);
				}
				return tupleElementSyntax;
			}
			return this;
		}

		// Token: 0x0600519C RID: 20892 RVA: 0x001486CE File Offset: 0x001468CE
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TupleElementSyntax(base.Kind, this.type, this.identifier, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600519D RID: 20893 RVA: 0x001486EE File Offset: 0x001468EE
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TupleElementSyntax(base.Kind, this.type, this.identifier, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x00148710 File Offset: 0x00146910
		internal TupleElementSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(typeSyntax);
			this.type = typeSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.identifier = syntaxToken;
			}
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x00148762 File Offset: 0x00146962
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.type);
			writer.WriteValue(this.identifier);
		}

		// Token: 0x060051A0 RID: 20896 RVA: 0x00148783 File Offset: 0x00146983
		static TupleElementSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TupleElementSyntax), (ObjectReader r) => new TupleElementSyntax(r));
		}

		// Token: 0x04001600 RID: 5632
		internal readonly TypeSyntax type;

		// Token: 0x04001601 RID: 5633
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken identifier;
	}
}
