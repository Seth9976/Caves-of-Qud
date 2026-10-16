using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000420 RID: 1056
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NameEqualsSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A8B RID: 23179 RVA: 0x001621B5 File Offset: 0x001603B5
		internal NameEqualsSyntax(SyntaxKind kind, IdentifierNameSyntax name, SyntaxToken equalsToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x001621E5 File Offset: 0x001603E5
		internal NameEqualsSyntax(SyntaxKind kind, IdentifierNameSyntax name, SyntaxToken equalsToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x00162219 File Offset: 0x00160419
		internal NameEqualsSyntax(SyntaxKind kind, IdentifierNameSyntax name, SyntaxToken equalsToken)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
			base.AdjustFlagsAndWidth(equalsToken);
			this.equalsToken = equalsToken;
		}

		// Token: 0x170014D5 RID: 5333
		// (get) Token: 0x06005A8E RID: 23182 RVA: 0x00162245 File Offset: 0x00160445
		public IdentifierNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170014D6 RID: 5334
		// (get) Token: 0x06005A8F RID: 23183 RVA: 0x0016224D File Offset: 0x0016044D
		public SyntaxToken EqualsToken
		{
			get
			{
				return this.equalsToken;
			}
		}

		// Token: 0x06005A90 RID: 23184 RVA: 0x00162258 File Offset: 0x00160458
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
					greenNode = this.equalsToken;
				}
			}
			else
			{
				greenNode = this.name;
			}
			return greenNode;
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x00162283 File Offset: 0x00160483
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new NameEqualsSyntax(this, parent, position);
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x0016228D File Offset: 0x0016048D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitNameEquals(this);
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x00162296 File Offset: 0x00160496
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitNameEquals(this);
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x001622A0 File Offset: 0x001604A0
		public NameEqualsSyntax Update(IdentifierNameSyntax name, SyntaxToken equalsToken)
		{
			if (name != this.Name || equalsToken != this.EqualsToken)
			{
				NameEqualsSyntax nameEqualsSyntax = SyntaxFactory.NameEquals(name, equalsToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					nameEqualsSyntax = nameEqualsSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					nameEqualsSyntax = nameEqualsSyntax.WithAnnotationsGreen(annotations);
				}
				return nameEqualsSyntax;
			}
			return this;
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x001622F6 File Offset: 0x001604F6
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new NameEqualsSyntax(base.Kind, this.name, this.equalsToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x00162316 File Offset: 0x00160516
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new NameEqualsSyntax(base.Kind, this.name, this.equalsToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x00162338 File Offset: 0x00160538
		internal NameEqualsSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(identifierNameSyntax);
			this.name = identifierNameSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.equalsToken = syntaxToken;
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x00162387 File Offset: 0x00160587
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
			writer.WriteValue(this.equalsToken);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x001623A8 File Offset: 0x001605A8
		static NameEqualsSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(NameEqualsSyntax), (ObjectReader r) => new NameEqualsSyntax(r));
		}

		// Token: 0x040017D9 RID: 6105
		internal readonly IdentifierNameSyntax name;

		// Token: 0x040017DA RID: 6106
		internal readonly SyntaxToken equalsToken;
	}
}
