using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D0 RID: 976
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class InterpolatedStringExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600559B RID: 21915 RVA: 0x001531F0 File Offset: 0x001513F0
		internal InterpolatedStringExpressionSyntax(SyntaxKind kind, SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode contents, SyntaxToken stringEndToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(stringStartToken);
			this.stringStartToken = stringStartToken;
			if (contents != null)
			{
				base.AdjustFlagsAndWidth(contents);
				this.contents = contents;
			}
			base.AdjustFlagsAndWidth(stringEndToken);
			this.stringEndToken = stringEndToken;
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x00153240 File Offset: 0x00151440
		internal InterpolatedStringExpressionSyntax(SyntaxKind kind, SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode contents, SyntaxToken stringEndToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(stringStartToken);
			this.stringStartToken = stringStartToken;
			if (contents != null)
			{
				base.AdjustFlagsAndWidth(contents);
				this.contents = contents;
			}
			base.AdjustFlagsAndWidth(stringEndToken);
			this.stringEndToken = stringEndToken;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x00153292 File Offset: 0x00151492
		internal InterpolatedStringExpressionSyntax(SyntaxKind kind, SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode contents, SyntaxToken stringEndToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(stringStartToken);
			this.stringStartToken = stringStartToken;
			if (contents != null)
			{
				base.AdjustFlagsAndWidth(contents);
				this.contents = contents;
			}
			base.AdjustFlagsAndWidth(stringEndToken);
			this.stringEndToken = stringEndToken;
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x0600559E RID: 21918 RVA: 0x001532D1 File Offset: 0x001514D1
		public SyntaxToken StringStartToken
		{
			get
			{
				return this.stringStartToken;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x0600559F RID: 21919 RVA: 0x001532D9 File Offset: 0x001514D9
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SyntaxList<InterpolatedStringContentSyntax> Contents
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SyntaxList<InterpolatedStringContentSyntax>(this.contents);
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x060055A0 RID: 21920 RVA: 0x001532E6 File Offset: 0x001514E6
		public SyntaxToken StringEndToken
		{
			get
			{
				return this.stringEndToken;
			}
		}

		// Token: 0x060055A1 RID: 21921 RVA: 0x001532F0 File Offset: 0x001514F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.stringStartToken;
				break;
			case 1:
				greenNode = this.contents;
				break;
			case 2:
				greenNode = this.stringEndToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x060055A2 RID: 21922 RVA: 0x0015332F File Offset: 0x0015152F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new InterpolatedStringExpressionSyntax(this, parent, position);
		}

		// Token: 0x060055A3 RID: 21923 RVA: 0x00153339 File Offset: 0x00151539
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitInterpolatedStringExpression(this);
		}

		// Token: 0x060055A4 RID: 21924 RVA: 0x00153342 File Offset: 0x00151542
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitInterpolatedStringExpression(this);
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0015334C File Offset: 0x0015154C
		public InterpolatedStringExpressionSyntax Update(SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
		{
			if (stringStartToken != this.StringStartToken || contents != this.Contents || stringEndToken != this.StringEndToken)
			{
				InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax = SyntaxFactory.InterpolatedStringExpression(stringStartToken, contents, stringEndToken);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					interpolatedStringExpressionSyntax = interpolatedStringExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					interpolatedStringExpressionSyntax = interpolatedStringExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return interpolatedStringExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x001533B1 File Offset: 0x001515B1
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new InterpolatedStringExpressionSyntax(base.Kind, this.stringStartToken, this.contents, this.stringEndToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x001533D7 File Offset: 0x001515D7
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new InterpolatedStringExpressionSyntax(base.Kind, this.stringStartToken, this.contents, this.stringEndToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x00153400 File Offset: 0x00151600
		internal InterpolatedStringExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.stringStartToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.contents = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.stringEndToken = syntaxToken2;
		}

		// Token: 0x060055A9 RID: 21929 RVA: 0x0015346C File Offset: 0x0015166C
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.stringStartToken);
			writer.WriteValue(this.contents);
			writer.WriteValue(this.stringEndToken);
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x00153499 File Offset: 0x00151699
		static InterpolatedStringExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(InterpolatedStringExpressionSyntax), (ObjectReader r) => new InterpolatedStringExpressionSyntax(r));
		}

		// Token: 0x040016BF RID: 5823
		internal readonly SyntaxToken stringStartToken;

		// Token: 0x040016C0 RID: 5824
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode contents;

		// Token: 0x040016C1 RID: 5825
		internal readonly SyntaxToken stringEndToken;
	}
}
