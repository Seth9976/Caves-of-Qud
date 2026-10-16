using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200041E RID: 1054
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class AttributeArgumentListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A6B RID: 23147 RVA: 0x00161C04 File Offset: 0x0015FE04
		internal AttributeArgumentListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005A6C RID: 23148 RVA: 0x00161C54 File Offset: 0x0015FE54
		internal AttributeArgumentListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005A6D RID: 23149 RVA: 0x00161CA6 File Offset: 0x0015FEA6
		internal AttributeArgumentListSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170014CF RID: 5327
		// (get) Token: 0x06005A6E RID: 23150 RVA: 0x00161CE5 File Offset: 0x0015FEE5
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170014D0 RID: 5328
		// (get) Token: 0x06005A6F RID: 23151 RVA: 0x00161CED File Offset: 0x0015FEED
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<AttributeArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<AttributeArgumentSyntax>(new SyntaxList<CSharpSyntaxNode>(this.arguments));
			}
		}

		// Token: 0x170014D1 RID: 5329
		// (get) Token: 0x06005A70 RID: 23152 RVA: 0x00161D04 File Offset: 0x0015FF04
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x00161D0C File Offset: 0x0015FF0C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.openParenToken;
				break;
			case 1:
				greenNode = this.arguments;
				break;
			case 2:
				greenNode = this.closeParenToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x00161D4B File Offset: 0x0015FF4B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new AttributeArgumentListSyntax(this, parent, position);
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x00161D55 File Offset: 0x0015FF55
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitAttributeArgumentList(this);
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x00161D5E File Offset: 0x0015FF5E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitAttributeArgumentList(this);
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x00161D68 File Offset: 0x0015FF68
		public AttributeArgumentListSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<AttributeArgumentSyntax> separatedSyntaxList = this.Arguments;
				if (!((in arguments) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			AttributeArgumentListSyntax attributeArgumentListSyntax = SyntaxFactory.AttributeArgumentList(openParenToken, arguments, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				attributeArgumentListSyntax = attributeArgumentListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				attributeArgumentListSyntax = attributeArgumentListSyntax.WithAnnotationsGreen(annotations);
			}
			return attributeArgumentListSyntax;
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x00161DD1 File Offset: 0x0015FFD1
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new AttributeArgumentListSyntax(base.Kind, this.openParenToken, this.arguments, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x00161DF7 File Offset: 0x0015FFF7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new AttributeArgumentListSyntax(base.Kind, this.openParenToken, this.arguments, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x00161E20 File Offset: 0x00160020
		internal AttributeArgumentListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openParenToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.arguments = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x00161E8C File Offset: 0x0016008C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.arguments);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x00161EB9 File Offset: 0x001600B9
		static AttributeArgumentListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(AttributeArgumentListSyntax), (ObjectReader r) => new AttributeArgumentListSyntax(r));
		}

		// Token: 0x040017D3 RID: 6099
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040017D4 RID: 6100
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode arguments;

		// Token: 0x040017D5 RID: 6101
		internal readonly SyntaxToken closeParenToken;
	}
}
