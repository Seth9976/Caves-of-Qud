using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200037D RID: 893
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AliasQualifiedNameSyntax : NameSyntax
	{
		// Token: 0x060050D9 RID: 20697 RVA: 0x001468CA File Offset: 0x00144ACA
		internal AliasQualifiedNameSyntax(SyntaxKind kind, IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(alias);
			this.alias = alias;
			base.AdjustFlagsAndWidth(colonColonToken);
			this.colonColonToken = colonColonToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x0014690C File Offset: 0x00144B0C
		internal AliasQualifiedNameSyntax(SyntaxKind kind, IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(alias);
			this.alias = alias;
			base.AdjustFlagsAndWidth(colonColonToken);
			this.colonColonToken = colonColonToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x0014695B File Offset: 0x00144B5B
		internal AliasQualifiedNameSyntax(SyntaxKind kind, IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(alias);
			this.alias = alias;
			base.AdjustFlagsAndWidth(colonColonToken);
			this.colonColonToken = colonColonToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x060050DC RID: 20700 RVA: 0x00146997 File Offset: 0x00144B97
		public IdentifierNameSyntax Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x060050DD RID: 20701 RVA: 0x0014699F File Offset: 0x00144B9F
		public SyntaxToken ColonColonToken
		{
			get
			{
				return this.colonColonToken;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x060050DE RID: 20702 RVA: 0x001469A7 File Offset: 0x00144BA7
		public SimpleNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x001469B0 File Offset: 0x00144BB0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.alias;
				break;
			case 1:
				greenNode = this.colonColonToken;
				break;
			case 2:
				greenNode = this.name;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x001469EF File Offset: 0x00144BEF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AliasQualifiedNameSyntax(this, parent, position);
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x001469F9 File Offset: 0x00144BF9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAliasQualifiedName(this);
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x00146A02 File Offset: 0x00144C02
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAliasQualifiedName(this);
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x00146A0C File Offset: 0x00144C0C
		public AliasQualifiedNameSyntax Update(IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
		{
			if (alias != this.Alias || colonColonToken != this.ColonColonToken || name != this.Name)
			{
				AliasQualifiedNameSyntax aliasQualifiedNameSyntax = SyntaxFactory.AliasQualifiedName(alias, colonColonToken, name);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					aliasQualifiedNameSyntax = aliasQualifiedNameSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					aliasQualifiedNameSyntax = aliasQualifiedNameSyntax.WithAnnotationsGreen(annotations);
				}
				return aliasQualifiedNameSyntax;
			}
			return this;
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x00146A6C File Offset: 0x00144C6C
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AliasQualifiedNameSyntax(base.Kind, this.alias, this.colonColonToken, this.name, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x00146A92 File Offset: 0x00144C92
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AliasQualifiedNameSyntax(base.Kind, this.alias, this.colonColonToken, this.name, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x00146AB8 File Offset: 0x00144CB8
		internal AliasQualifiedNameSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(identifierNameSyntax);
			this.alias = identifierNameSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.colonColonToken = syntaxToken;
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(simpleNameSyntax);
			this.name = simpleNameSyntax;
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x00146B21 File Offset: 0x00144D21
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.alias);
			writer.WriteValue(this.colonColonToken);
			writer.WriteValue(this.name);
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x00146B4E File Offset: 0x00144D4E
		static AliasQualifiedNameSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AliasQualifiedNameSyntax), (ObjectReader r) => new AliasQualifiedNameSyntax(r));
		}

		// Token: 0x040015E3 RID: 5603
		internal readonly IdentifierNameSyntax alias;

		// Token: 0x040015E4 RID: 5604
		internal readonly SyntaxToken colonColonToken;

		// Token: 0x040015E5 RID: 5605
		internal readonly SimpleNameSyntax name;
	}
}
