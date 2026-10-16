using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000421 RID: 1057
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A9A RID: 23194 RVA: 0x001623CC File Offset: 0x001605CC
		internal TypeParameterListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken greaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x0016241C File Offset: 0x0016061C
		internal TypeParameterListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken greaterThanToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x0016246E File Offset: 0x0016066E
		internal TypeParameterListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken greaterThanToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(lessThanToken);
			this.lessThanToken = lessThanToken;
			if (parameters != null)
			{
				base.AdjustFlagsAndWidth(parameters);
				this.parameters = parameters;
			}
			base.AdjustFlagsAndWidth(greaterThanToken);
			this.greaterThanToken = greaterThanToken;
		}

		// Token: 0x170014D7 RID: 5335
		// (get) Token: 0x06005A9D RID: 23197 RVA: 0x001624AD File Offset: 0x001606AD
		public SyntaxToken LessThanToken
		{
			get
			{
				return this.lessThanToken;
			}
		}

		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x06005A9E RID: 23198 RVA: 0x001624B5 File Offset: 0x001606B5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TypeParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<TypeParameterSyntax>(new SyntaxList<CSharpSyntaxNode>(this.parameters));
			}
		}

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06005A9F RID: 23199 RVA: 0x001624CC File Offset: 0x001606CC
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return this.greaterThanToken;
			}
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001624D4 File Offset: 0x001606D4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.lessThanToken;
				break;
			case 1:
				greenNode = this.parameters;
				break;
			case 2:
				greenNode = this.greaterThanToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x00162513 File Offset: 0x00160713
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TypeParameterListSyntax(this, parent, position);
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x0016251D File Offset: 0x0016071D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTypeParameterList(this);
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x00162526 File Offset: 0x00160726
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTypeParameterList(this);
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x00162530 File Offset: 0x00160730
		public TypeParameterListSyntax Update(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			if (lessThanToken == this.LessThanToken)
			{
				SeparatedSyntaxList<TypeParameterSyntax> separatedSyntaxList = this.Parameters;
				if (!((in parameters) != (in separatedSyntaxList)) && greaterThanToken == this.GreaterThanToken)
				{
					return this;
				}
			}
			TypeParameterListSyntax typeParameterListSyntax = SyntaxFactory.TypeParameterList(lessThanToken, parameters, greaterThanToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				typeParameterListSyntax = typeParameterListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				typeParameterListSyntax = typeParameterListSyntax.WithAnnotationsGreen(annotations);
			}
			return typeParameterListSyntax;
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x00162599 File Offset: 0x00160799
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TypeParameterListSyntax(base.Kind, this.lessThanToken, this.parameters, this.greaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x001625BF File Offset: 0x001607BF
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TypeParameterListSyntax(base.Kind, this.lessThanToken, this.parameters, this.greaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x001625E8 File Offset: 0x001607E8
		internal TypeParameterListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.lessThanToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.parameters = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.greaterThanToken = syntaxToken2;
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x00162654 File Offset: 0x00160854
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanToken);
			writer.WriteValue(this.parameters);
			writer.WriteValue(this.greaterThanToken);
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x00162681 File Offset: 0x00160881
		static TypeParameterListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TypeParameterListSyntax), (ObjectReader r) => new TypeParameterListSyntax(r));
		}

		// Token: 0x040017DB RID: 6107
		internal readonly SyntaxToken lessThanToken;

		// Token: 0x040017DC RID: 6108
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode parameters;

		// Token: 0x040017DD RID: 6109
		internal readonly SyntaxToken greaterThanToken;
	}
}
