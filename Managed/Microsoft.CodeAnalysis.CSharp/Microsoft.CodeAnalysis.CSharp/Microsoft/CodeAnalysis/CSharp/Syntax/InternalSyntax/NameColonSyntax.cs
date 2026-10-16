using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003AD RID: 941
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NameColonSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005397 RID: 21399 RVA: 0x0014D7B9 File Offset: 0x0014B9B9
		internal NameColonSyntax(SyntaxKind kind, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005398 RID: 21400 RVA: 0x0014D7E9 File Offset: 0x0014B9E9
		internal NameColonSyntax(SyntaxKind kind, IdentifierNameSyntax name, SyntaxToken colonToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x06005399 RID: 21401 RVA: 0x0014D81D File Offset: 0x0014BA1D
		internal NameColonSyntax(SyntaxKind kind, IdentifierNameSyntax name, SyntaxToken colonToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(colonToken);
			this.colonToken = colonToken;
		}

		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x0600539A RID: 21402 RVA: 0x0014D849 File Offset: 0x0014BA49
		public IdentifierNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x0600539B RID: 21403 RVA: 0x0014D851 File Offset: 0x0014BA51
		public SyntaxToken ColonToken
		{
			get
			{
				return this.colonToken;
			}
		}

		// Token: 0x0600539C RID: 21404 RVA: 0x0014D85C File Offset: 0x0014BA5C
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
					greenNode = this.colonToken;
				}
			}
			else
			{
				greenNode = this.name;
			}
			return greenNode;
		}

		// Token: 0x0600539D RID: 21405 RVA: 0x0014D887 File Offset: 0x0014BA87
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new NameColonSyntax(this, parent, position);
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x0014D891 File Offset: 0x0014BA91
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNameColon(this);
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x0014D89A File Offset: 0x0014BA9A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNameColon(this);
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x0014D8A4 File Offset: 0x0014BAA4
		public NameColonSyntax Update(IdentifierNameSyntax name, SyntaxToken colonToken)
		{
			if (name != this.Name || colonToken != this.ColonToken)
			{
				NameColonSyntax nameColonSyntax = SyntaxFactory.NameColon(name, colonToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					nameColonSyntax = nameColonSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					nameColonSyntax = nameColonSyntax.WithAnnotationsGreen(annotations);
				}
				return nameColonSyntax;
			}
			return this;
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0014D8FA File Offset: 0x0014BAFA
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new NameColonSyntax(base.Kind, this.name, this.colonToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060053A2 RID: 21410 RVA: 0x0014D91A File Offset: 0x0014BB1A
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new NameColonSyntax(base.Kind, this.name, this.colonToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060053A3 RID: 21411 RVA: 0x0014D93C File Offset: 0x0014BB3C
		internal NameColonSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(identifierNameSyntax);
			this.name = identifierNameSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.colonToken = syntaxToken;
		}

		// Token: 0x060053A4 RID: 21412 RVA: 0x0014D98B File Offset: 0x0014BB8B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.colonToken);
		}

		// Token: 0x060053A5 RID: 21413 RVA: 0x0014D9AC File Offset: 0x0014BBAC
		static NameColonSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(NameColonSyntax), (ObjectReader r) => new NameColonSyntax(r));
		}

		// Token: 0x04001658 RID: 5720
		internal readonly IdentifierNameSyntax name;

		// Token: 0x04001659 RID: 5721
		internal readonly SyntaxToken colonToken;
	}
}
