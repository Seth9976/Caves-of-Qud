using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200038F RID: 911
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TupleExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060051D5 RID: 20949 RVA: 0x00148EC4 File Offset: 0x001470C4
		internal TupleExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x060051D6 RID: 20950 RVA: 0x00148F14 File Offset: 0x00147114
		internal TupleExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken, SyntaxFactoryContext context)
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

		// Token: 0x060051D7 RID: 20951 RVA: 0x00148F66 File Offset: 0x00147166
		internal TupleExpressionSyntax(SyntaxKind kind, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeParenToken)
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

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x00148FA5 File Offset: 0x001471A5
		public SyntaxToken OpenParenToken
		{
			get
			{
				return this.openParenToken;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x00148FAD File Offset: 0x001471AD
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ArgumentSyntax>(new SyntaxList<CSharpSyntaxNode>(this.arguments));
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x00148FC4 File Offset: 0x001471C4
		public SyntaxToken CloseParenToken
		{
			get
			{
				return this.closeParenToken;
			}
		}

		// Token: 0x060051DB RID: 20955 RVA: 0x00148FCC File Offset: 0x001471CC
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

		// Token: 0x060051DC RID: 20956 RVA: 0x0014900B File Offset: 0x0014720B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new TupleExpressionSyntax(this, parent, position);
		}

		// Token: 0x060051DD RID: 20957 RVA: 0x00149015 File Offset: 0x00147215
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitTupleExpression(this);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x0014901E File Offset: 0x0014721E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitTupleExpression(this);
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00149028 File Offset: 0x00147228
		public TupleExpressionSyntax Update(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			if (openParenToken == this.OpenParenToken)
			{
				SeparatedSyntaxList<ArgumentSyntax> separatedSyntaxList = this.Arguments;
				if (!((in arguments) != (in separatedSyntaxList)) && closeParenToken == this.CloseParenToken)
				{
					return this;
				}
			}
			TupleExpressionSyntax tupleExpressionSyntax = SyntaxFactory.TupleExpression(openParenToken, arguments, closeParenToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				tupleExpressionSyntax = tupleExpressionSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				tupleExpressionSyntax = tupleExpressionSyntax.WithAnnotationsGreen(annotations);
			}
			return tupleExpressionSyntax;
		}

		// Token: 0x060051E0 RID: 20960 RVA: 0x00149091 File Offset: 0x00147291
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new TupleExpressionSyntax(base.Kind, this.openParenToken, this.arguments, this.closeParenToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060051E1 RID: 20961 RVA: 0x001490B7 File Offset: 0x001472B7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new TupleExpressionSyntax(base.Kind, this.openParenToken, this.arguments, this.closeParenToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060051E2 RID: 20962 RVA: 0x001490E0 File Offset: 0x001472E0
		internal TupleExpressionSyntax(ObjectReader reader)
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

		// Token: 0x060051E3 RID: 20963 RVA: 0x0014914C File Offset: 0x0014734C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openParenToken);
			writer.WriteValue(this.arguments);
			writer.WriteValue(this.closeParenToken);
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x00149179 File Offset: 0x00147379
		static TupleExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(TupleExpressionSyntax), (ObjectReader r) => new TupleExpressionSyntax(r));
		}

		// Token: 0x04001609 RID: 5641
		internal readonly SyntaxToken openParenToken;

		// Token: 0x0400160A RID: 5642
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode arguments;

		// Token: 0x0400160B RID: 5643
		internal readonly SyntaxToken closeParenToken;
	}
}
