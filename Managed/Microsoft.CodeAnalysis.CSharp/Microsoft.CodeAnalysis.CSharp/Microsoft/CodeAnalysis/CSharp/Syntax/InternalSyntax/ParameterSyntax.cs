using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200044C RID: 1100
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ParameterSyntax : BaseParameterSyntax
	{
		// Token: 0x06005D46 RID: 23878 RVA: 0x0016BAD8 File Offset: 0x00169CD8
		internal ParameterSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, EqualsValueClauseSyntax @default, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (@default != null)
			{
				base.AdjustFlagsAndWidth(@default);
				this.@default = @default;
			}
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x0016BB54 File Offset: 0x00169D54
		internal ParameterSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, EqualsValueClauseSyntax @default, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (@default != null)
			{
				base.AdjustFlagsAndWidth(@default);
				this.@default = @default;
			}
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x0016BBD4 File Offset: 0x00169DD4
		internal ParameterSyntax(SyntaxKind kind, GreenNode attributeLists, GreenNode modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, EqualsValueClauseSyntax @default)
			: base(kind)
		{
			base.SlotCount = 5;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (modifiers != null)
			{
				base.AdjustFlagsAndWidth(modifiers);
				this.modifiers = modifiers;
			}
			if (type != null)
			{
				base.AdjustFlagsAndWidth(type);
				this.type = type;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
			if (@default != null)
			{
				base.AdjustFlagsAndWidth(@default);
				this.@default = @default;
			}
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06005D49 RID: 23881 RVA: 0x0016BC49 File Offset: 0x00169E49
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06005D4A RID: 23882 RVA: 0x0016BC56 File Offset: 0x00169E56
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<SyntaxToken>(this.modifiers);
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06005D4B RID: 23883 RVA: 0x0016BC63 File Offset: 0x00169E63
		public override TypeSyntax Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x0016BC6B File Offset: 0x00169E6B
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06005D4D RID: 23885 RVA: 0x0016BC73 File Offset: 0x00169E73
		public EqualsValueClauseSyntax Default
		{
			get
			{
				return this.@default;
			}
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x0016BC7C File Offset: 0x00169E7C
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.modifiers;
				break;
			case 2:
				greenNode = this.type;
				break;
			case 3:
				greenNode = this.identifier;
				break;
			case 4:
				greenNode = this.@default;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x0016BCD5 File Offset: 0x00169ED5
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ParameterSyntax(this, parent, position);
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x0016BCDF File Offset: 0x00169EDF
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitParameter(this);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x0016BCE8 File Offset: 0x00169EE8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitParameter(this);
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x0016BCF4 File Offset: 0x00169EF4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public ParameterSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax @default)
		{
			if (attributeLists != this.AttributeLists || modifiers != this.Modifiers || type != this.Type || identifier != this.Identifier || @default != this.Default)
			{
				ParameterSyntax parameterSyntax = SyntaxFactory.Parameter(attributeLists, modifiers, type, identifier, @default);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					parameterSyntax = parameterSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					parameterSyntax = parameterSyntax.WithAnnotationsGreen(annotations);
				}
				return parameterSyntax;
			}
			return this;
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x0016BD76 File Offset: 0x00169F76
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ParameterSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, this.identifier, this.@default, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x0016BDA8 File Offset: 0x00169FA8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ParameterSyntax(base.Kind, this.attributeLists, this.modifiers, this.type, this.identifier, this.@default, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x0016BDDC File Offset: 0x00169FDC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal ParameterSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 5;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			GreenNode greenNode2 = (GreenNode)reader.ReadValue();
			if (greenNode2 != null)
			{
				base.AdjustFlagsAndWidth(greenNode2);
				this.modifiers = greenNode2;
			}
			TypeSyntax typeSyntax = (TypeSyntax)reader.ReadValue();
			if (typeSyntax != null)
			{
				base.AdjustFlagsAndWidth(typeSyntax);
				this.type = typeSyntax;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.identifier = syntaxToken;
			EqualsValueClauseSyntax equalsValueClauseSyntax = (EqualsValueClauseSyntax)reader.ReadValue();
			if (equalsValueClauseSyntax != null)
			{
				base.AdjustFlagsAndWidth(equalsValueClauseSyntax);
				this.@default = equalsValueClauseSyntax;
			}
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x0016BE8C File Offset: 0x0016A08C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.modifiers);
			writer.WriteValue(this.type);
			writer.WriteValue(this.identifier);
			writer.WriteValue(this.@default);
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x0016BEDC File Offset: 0x0016A0DC
		static ParameterSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ParameterSyntax), (ObjectReader r) => new ParameterSyntax(r));
		}

		// Token: 0x0400189B RID: 6299
		internal readonly GreenNode attributeLists;

		// Token: 0x0400189C RID: 6300
		internal readonly GreenNode modifiers;

		// Token: 0x0400189D RID: 6301
		internal readonly TypeSyntax type;

		// Token: 0x0400189E RID: 6302
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal readonly SyntaxToken identifier;

		// Token: 0x0400189F RID: 6303
		internal readonly EqualsValueClauseSyntax @default;
	}
}
