using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000422 RID: 1058
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeParameterSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005AAA RID: 23210 RVA: 0x001626A4 File Offset: 0x001608A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal TypeParameterSyntax(SyntaxKind kind, GreenNode attributeLists, SyntaxToken varianceKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (varianceKeyword != null)
			{
				base.AdjustFlagsAndWidth(varianceKeyword);
				this.varianceKeyword = varianceKeyword;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x001626F8 File Offset: 0x001608F8
		internal TypeParameterSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken varianceKeyword, SyntaxToken identifier, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (varianceKeyword != null)
			{
				base.AdjustFlagsAndWidth(varianceKeyword);
				this.varianceKeyword = varianceKeyword;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x00162750 File Offset: 0x00160950
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal TypeParameterSyntax(SyntaxKind kind, GreenNode attributeLists, SyntaxToken varianceKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier)
			: base(kind)
		{
			base.SlotCount = 3;
			if (attributeLists != null)
			{
				base.AdjustFlagsAndWidth(attributeLists);
				this.attributeLists = attributeLists;
			}
			if (varianceKeyword != null)
			{
				base.AdjustFlagsAndWidth(varianceKeyword);
				this.varianceKeyword = varianceKeyword;
			}
			base.AdjustFlagsAndWidth(identifier);
			this.identifier = identifier;
		}

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x06005AAD RID: 23213 RVA: 0x0016279D File Offset: 0x0016099D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<AttributeListSyntax>(this.attributeLists);
			}
		}

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x001627AA File Offset: 0x001609AA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxToken VarianceKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.varianceKeyword;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06005AAF RID: 23215 RVA: 0x001627B2 File Offset: 0x001609B2
		public SyntaxToken Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x001627BC File Offset: 0x001609BC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.attributeLists;
				break;
			case 1:
				greenNode = this.varianceKeyword;
				break;
			case 2:
				greenNode = this.identifier;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x001627FB File Offset: 0x001609FB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeParameterSyntax(this, parent, position);
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x00162805 File Offset: 0x00160A05
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeParameter(this);
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x0016280E File Offset: 0x00160A0E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameter(this);
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x00162818 File Offset: 0x00160A18
		public TypeParameterSyntax Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken varianceKeyword, SyntaxToken identifier)
		{
			if (attributeLists != this.AttributeLists || varianceKeyword != this.VarianceKeyword || identifier != this.Identifier)
			{
				TypeParameterSyntax typeParameterSyntax = SyntaxFactory.TypeParameter(attributeLists, varianceKeyword, identifier);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					typeParameterSyntax = typeParameterSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					typeParameterSyntax = typeParameterSyntax.WithAnnotationsGreen(annotations);
				}
				return typeParameterSyntax;
			}
			return this;
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x0016287D File Offset: 0x00160A7D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeParameterSyntax(base.Kind, this.attributeLists, this.varianceKeyword, this.identifier, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x001628A3 File Offset: 0x00160AA3
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeParameterSyntax(base.Kind, this.attributeLists, this.varianceKeyword, this.identifier, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x001628CC File Offset: 0x00160ACC
		internal TypeParameterSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributeLists = greenNode;
			}
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			if (syntaxToken != null)
			{
				base.AdjustFlagsAndWidth(syntaxToken);
				this.varianceKeyword = syntaxToken;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.identifier = syntaxToken2;
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x0016293B File Offset: 0x00160B3B
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.attributeLists);
			writer.WriteValue(this.varianceKeyword);
			writer.WriteValue(this.identifier);
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x00162968 File Offset: 0x00160B68
		static TypeParameterSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeParameterSyntax), (ObjectReader r) => new TypeParameterSyntax(r));
		}

		// Token: 0x040017DE RID: 6110
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributeLists;

		// Token: 0x040017DF RID: 6111
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly SyntaxToken varianceKeyword;

		// Token: 0x040017E0 RID: 6112
		internal readonly SyntaxToken identifier;
	}
}
