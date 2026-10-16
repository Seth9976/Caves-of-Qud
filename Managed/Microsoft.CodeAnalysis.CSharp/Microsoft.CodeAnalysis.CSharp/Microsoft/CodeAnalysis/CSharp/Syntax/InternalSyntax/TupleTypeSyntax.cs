using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000388 RID: 904
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TupleTypeSyntax : TypeSyntax
	{
		// Token: 0x06005182 RID: 20866 RVA: 0x001482B0 File Offset: 0x001464B0
		internal TupleTypeSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode elements, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (elements != null)
			{
				base.AdjustFlagsAndWidth(elements);
				this.elements = elements;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005183 RID: 20867 RVA: 0x00148300 File Offset: 0x00146500
		internal TupleTypeSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode elements, SyntaxToken closeParenToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (elements != null)
			{
				base.AdjustFlagsAndWidth(elements);
				this.elements = elements;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x06005184 RID: 20868 RVA: 0x00148352 File Offset: 0x00146552
		internal TupleTypeSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode elements, SyntaxToken closeParenToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openParenToken);
			this.openParenToken = openParenToken;
			if (elements != null)
			{
				base.AdjustFlagsAndWidth(elements);
				this.elements = elements;
			}
			base.AdjustFlagsAndWidth(closeParenToken);
			this.closeParenToken = closeParenToken;
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x00148391 File Offset: 0x00146591
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06005186 RID: 20870 RVA: 0x00148399 File Offset: 0x00146599
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<TupleElementSyntax> Elements
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<TupleElementSyntax>(new SyntaxList<CSharpSyntaxNode>(this.elements));
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06005187 RID: 20871 RVA: 0x001483B0 File Offset: 0x001465B0
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x001483B8 File Offset: 0x001465B8
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
				greenNode = this.elements;
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

		// Token: 0x06005189 RID: 20873 RVA: 0x001483F7 File Offset: 0x001465F7
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TupleTypeSyntax(this, parent, position);
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x00148401 File Offset: 0x00146601
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTupleType(this);
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0014840A File Offset: 0x0014660A
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTupleType(this);
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00148414 File Offset: 0x00146614
		public TupleTypeSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TupleElementSyntax> elements, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<TupleElementSyntax> separatedSyntaxList = this.Elements;
				if (!((in elements) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			TupleTypeSyntax tupleTypeSyntax = SyntaxFactory.TupleType(openParenToken, elements, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				tupleTypeSyntax = tupleTypeSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				tupleTypeSyntax = tupleTypeSyntax.WithAnnotationsGreen(annotations);
			}
			return tupleTypeSyntax;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x0014847D File Offset: 0x0014667D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TupleTypeSyntax(base.Kind, this.openParenToken, this.elements, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x001484A3 File Offset: 0x001466A3
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TupleTypeSyntax(base.Kind, this.openParenToken, this.elements, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x001484CC File Offset: 0x001466CC
		internal TupleTypeSyntax(ObjectReader reader)
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
				this.elements = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeParenToken = syntaxToken2;
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00148538 File Offset: 0x00146738
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.elements);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00148565 File Offset: 0x00146765
		static TupleTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TupleTypeSyntax), (ObjectReader r) => new TupleTypeSyntax(r));
		}

		// Token: 0x040015FD RID: 5629
		internal readonly SyntaxToken openParenToken;

		// Token: 0x040015FE RID: 5630
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode elements;

		// Token: 0x040015FF RID: 5631
		internal readonly SyntaxToken closeParenToken;
	}
}
