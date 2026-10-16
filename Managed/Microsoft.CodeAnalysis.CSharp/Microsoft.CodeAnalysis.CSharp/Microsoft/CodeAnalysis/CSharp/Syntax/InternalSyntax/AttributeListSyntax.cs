using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200041B RID: 1051
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AttributeListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A3C RID: 23100 RVA: 0x0016145C File Offset: 0x0015F65C
		internal AttributeListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken closeBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (target != null)
			{
				base.AdjustFlagsAndWidth(target);
				this.target = target;
			}
			if (attributes != null)
			{
				base.AdjustFlagsAndWidth(attributes);
				this.attributes = attributes;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005A3D RID: 23101 RVA: 0x001614C0 File Offset: 0x0015F6C0
		internal AttributeListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken closeBracketToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (target != null)
			{
				base.AdjustFlagsAndWidth(target);
				this.target = target;
			}
			if (attributes != null)
			{
				base.AdjustFlagsAndWidth(attributes);
				this.attributes = attributes;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005A3E RID: 23102 RVA: 0x00161528 File Offset: 0x0015F728
		internal AttributeListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode attributes, SyntaxToken closeBracketToken)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (target != null)
			{
				base.AdjustFlagsAndWidth(target);
				this.target = target;
			}
			if (attributes != null)
			{
				base.AdjustFlagsAndWidth(attributes);
				this.attributes = attributes;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06005A3F RID: 23103 RVA: 0x00161586 File Offset: 0x0015F786
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06005A40 RID: 23104 RVA: 0x0016158E File Offset: 0x0015F78E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public AttributeTargetSpecifierSyntax Target
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.target;
			}
		}

		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06005A41 RID: 23105 RVA: 0x00161596 File Offset: 0x0015F796
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<AttributeSyntax> Attributes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<AttributeSyntax>(new SyntaxList<CSharpSyntaxNode>(this.attributes));
			}
		}

		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06005A42 RID: 23106 RVA: 0x001615AD File Offset: 0x0015F7AD
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x001615B8 File Offset: 0x0015F7B8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openBracketToken;
				break;
			case 1:
				greenNode = this.target;
				break;
			case 2:
				greenNode = this.attributes;
				break;
			case 3:
				greenNode = this.closeBracketToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x00161604 File Offset: 0x0015F804
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AttributeListSyntax(this, parent, position);
		}

		// Token: 0x06005A45 RID: 23109 RVA: 0x0016160E File Offset: 0x0015F80E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeList(this);
		}

		// Token: 0x06005A46 RID: 23110 RVA: 0x00161617 File Offset: 0x0015F817
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeList(this);
		}

		// Token: 0x06005A47 RID: 23111 RVA: 0x00161620 File Offset: 0x0015F820
		public AttributeListSyntax Update(SyntaxToken openBracketToken, AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
		{
			if (openBracketToken == this.OpenBracketToken && target == this.Target)
			{
				SeparatedSyntaxList<AttributeSyntax> separatedSyntaxList = this.Attributes;
				if (!((in attributes) != (in separatedSyntaxList)) && closeBracketToken == this.CloseBracketToken)
				{
					return this;
				}
			}
			AttributeListSyntax attributeListSyntax = SyntaxFactory.AttributeList(openBracketToken, target, attributes, closeBracketToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				attributeListSyntax = attributeListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				attributeListSyntax = attributeListSyntax.WithAnnotationsGreen(annotations);
			}
			return attributeListSyntax;
		}

		// Token: 0x06005A48 RID: 23112 RVA: 0x00161695 File Offset: 0x0015F895
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AttributeListSyntax(base.Kind, this.openBracketToken, this.target, this.attributes, this.closeBracketToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A49 RID: 23113 RVA: 0x001616C1 File Offset: 0x0015F8C1
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AttributeListSyntax(base.Kind, this.openBracketToken, this.target, this.attributes, this.closeBracketToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A4A RID: 23114 RVA: 0x001616F0 File Offset: 0x0015F8F0
		internal AttributeListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBracketToken = syntaxToken;
			AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax = (AttributeTargetSpecifierSyntax)reader.ReadValue();
			if (attributeTargetSpecifierSyntax != null)
			{
				base.AdjustFlagsAndWidth(attributeTargetSpecifierSyntax);
				this.target = attributeTargetSpecifierSyntax;
			}
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.attributes = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBracketToken = syntaxToken2;
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x00161779 File Offset: 0x0015F979
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.target);
			writer.WriteValue(this.attributes);
			writer.WriteValue(this.closeBracketToken);
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x001617B2 File Offset: 0x0015F9B2
		static AttributeListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AttributeListSyntax), (ObjectReader r) => new AttributeListSyntax(r));
		}

		// Token: 0x040017CB RID: 6091
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x040017CC RID: 6092
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly AttributeTargetSpecifierSyntax target;

		// Token: 0x040017CD RID: 6093
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode attributes;

		// Token: 0x040017CE RID: 6094
		internal readonly SyntaxToken closeBracketToken;
	}
}
