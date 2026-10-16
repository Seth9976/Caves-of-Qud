using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000395 RID: 917
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class MemberBindingExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005232 RID: 21042 RVA: 0x00149D2F File Offset: 0x00147F2F
		internal MemberBindingExpressionSyntax(SyntaxKind kind, SyntaxToken operatorToken, SimpleNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x00149D5F File Offset: 0x00147F5F
		internal MemberBindingExpressionSyntax(SyntaxKind kind, SyntaxToken operatorToken, SimpleNameSyntax name, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x00149D93 File Offset: 0x00147F93
		internal MemberBindingExpressionSyntax(SyntaxKind kind, SyntaxToken operatorToken, SimpleNameSyntax name)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(operatorToken);
			this.operatorToken = operatorToken;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06005235 RID: 21045 RVA: 0x00149DBF File Offset: 0x00147FBF
		public SyntaxToken OperatorToken
		{
			get
			{
				return this.operatorToken;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06005236 RID: 21046 RVA: 0x00149DC7 File Offset: 0x00147FC7
		public SimpleNameSyntax Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x00149DD0 File Offset: 0x00147FD0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			if (index != 0)
			{
				if (index != 1)
				{
					greenNode = null;
				}
				else
				{
					greenNode = this.name;
				}
			}
			else
			{
				greenNode = this.operatorToken;
			}
			return greenNode;
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00149DFB File Offset: 0x00147FFB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new MemberBindingExpressionSyntax(this, parent, position);
		}

		// Token: 0x06005239 RID: 21049 RVA: 0x00149E05 File Offset: 0x00148005
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitMemberBindingExpression(this);
		}

		// Token: 0x0600523A RID: 21050 RVA: 0x00149E0E File Offset: 0x0014800E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitMemberBindingExpression(this);
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x00149E18 File Offset: 0x00148018
		public MemberBindingExpressionSyntax Update(SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			if (operatorToken != this.OperatorToken || name != this.Name)
			{
				MemberBindingExpressionSyntax memberBindingExpressionSyntax = SyntaxFactory.MemberBindingExpression(operatorToken, name);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					memberBindingExpressionSyntax = memberBindingExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					memberBindingExpressionSyntax = memberBindingExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return memberBindingExpressionSyntax;
			}
			return this;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x00149E6E File Offset: 0x0014806E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new MemberBindingExpressionSyntax(base.Kind, this.operatorToken, this.name, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x00149E8E File Offset: 0x0014808E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new MemberBindingExpressionSyntax(base.Kind, this.operatorToken, this.name, base.GetDiagnostics(), annotations);
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x00149EB0 File Offset: 0x001480B0
		internal MemberBindingExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.operatorToken = syntaxToken;
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(simpleNameSyntax);
			this.name = simpleNameSyntax;
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x00149EFF File Offset: 0x001480FF
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.operatorToken);
			writer.WriteValue(this.name);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x00149F20 File Offset: 0x00148120
		static MemberBindingExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(MemberBindingExpressionSyntax), (ObjectReader r) => new MemberBindingExpressionSyntax(r));
		}

		// Token: 0x04001618 RID: 5656
		internal readonly SyntaxToken operatorToken;

		// Token: 0x04001619 RID: 5657
		internal readonly SimpleNameSyntax name;
	}
}
