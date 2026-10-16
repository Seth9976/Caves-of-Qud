using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000393 RID: 915
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class MemberAccessExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005212 RID: 21010 RVA: 0x001497DD File Offset: 0x001479DD
		internal MemberAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00149820 File Offset: 0x00147A20
		internal MemberAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x0014986F File Offset: 0x00147A6F
		internal MemberAccessExpressionSyntax(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(expression);
			this.expression = expression;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06005215 RID: 21013 RVA: 0x001498AB File Offset: 0x00147AAB
		public ExpressionSyntax Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06005216 RID: 21014 RVA: 0x001498B3 File Offset: 0x00147AB3
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06005217 RID: 21015 RVA: 0x001498BB File Offset: 0x00147ABB
		public SimpleNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06005218 RID: 21016 RVA: 0x001498C4 File Offset: 0x00147AC4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.expression;
				break;
			case 1:
				greenNode = this.operatorToken;
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

		// Token: 0x06005219 RID: 21017 RVA: 0x00149903 File Offset: 0x00147B03
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new MemberAccessExpressionSyntax(this, parent, position);
		}

		// Token: 0x0600521A RID: 21018 RVA: 0x0014990D File Offset: 0x00147B0D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMemberAccessExpression(this);
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00149916 File Offset: 0x00147B16
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMemberAccessExpression(this);
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x00149920 File Offset: 0x00147B20
		public MemberAccessExpressionSyntax Update(ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			if (expression != this.Expression || operatorToken != this.OperatorToken || name != this.Name)
			{
				MemberAccessExpressionSyntax memberAccessExpressionSyntax = SyntaxFactory.MemberAccessExpression(base.Kind, expression, operatorToken, name);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					memberAccessExpressionSyntax = memberAccessExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					memberAccessExpressionSyntax = memberAccessExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return memberAccessExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600521D RID: 21021 RVA: 0x00149986 File Offset: 0x00147B86
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new MemberAccessExpressionSyntax(base.Kind, this.expression, this.operatorToken, this.name, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600521E RID: 21022 RVA: 0x001499AC File Offset: 0x00147BAC
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new MemberAccessExpressionSyntax(base.Kind, this.expression, this.operatorToken, this.name, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600521F RID: 21023 RVA: 0x001499D4 File Offset: 0x00147BD4
		internal MemberAccessExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(expressionSyntax);
			this.expression = expressionSyntax;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(simpleNameSyntax);
			this.name = simpleNameSyntax;
		}

		// Token: 0x06005220 RID: 21024 RVA: 0x00149A3D File Offset: 0x00147C3D
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.expression);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.name);
		}

		// Token: 0x06005221 RID: 21025 RVA: 0x00149A6A File Offset: 0x00147C6A
		static MemberAccessExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(MemberAccessExpressionSyntax), (ObjectReader r) => new MemberAccessExpressionSyntax(r));
		}

		// Token: 0x04001612 RID: 5650
		internal readonly ExpressionSyntax expression;

		// Token: 0x04001613 RID: 5651
		internal readonly SyntaxToken operatorToken;

		// Token: 0x04001614 RID: 5652
		internal readonly SimpleNameSyntax name;
	}
}
