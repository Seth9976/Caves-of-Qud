using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200039D RID: 925
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class ThisExpressionSyntax : InstanceExpressionSyntax
	{
		// Token: 0x060052A2 RID: 21154 RVA: 0x0014AE66 File Offset: 0x00149066
		internal ThisExpressionSyntax(SyntaxKind kind, SyntaxToken token, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x060052A3 RID: 21155 RVA: 0x0014AE87 File Offset: 0x00149087
		internal ThisExpressionSyntax(SyntaxKind kind, SyntaxToken token, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x0014AEAC File Offset: 0x001490AC
		internal ThisExpressionSyntax(SyntaxKind kind, SyntaxToken token)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(token);
			this.token = token;
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x0014AECA File Offset: 0x001490CA
		public SyntaxToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x060052A6 RID: 21158 RVA: 0x0014AED2 File Offset: 0x001490D2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.token;
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x0014AEDF File Offset: 0x001490DF
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new ThisExpressionSyntax(this, parent, position);
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x0014AEE9 File Offset: 0x001490E9
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitThisExpression(this);
		}

		// Token: 0x060052A9 RID: 21161 RVA: 0x0014AEF2 File Offset: 0x001490F2
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitThisExpression(this);
		}

		// Token: 0x060052AA RID: 21162 RVA: 0x0014AEFC File Offset: 0x001490FC
		public ThisExpressionSyntax Update(SyntaxToken token)
		{
			if (token != this.Token)
			{
				ThisExpressionSyntax thisExpressionSyntax = SyntaxFactory.ThisExpression(token);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					thisExpressionSyntax = thisExpressionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					thisExpressionSyntax = thisExpressionSyntax.WithAnnotationsGreen(annotations);
				}
				return thisExpressionSyntax;
			}
			return this;
		}

		// Token: 0x060052AB RID: 21163 RVA: 0x0014AF48 File Offset: 0x00149148
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new ThisExpressionSyntax(base.Kind, this.token, diagnostics, base.GetAnnotations());
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0014AF62 File Offset: 0x00149162
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new ThisExpressionSyntax(base.Kind, this.token, base.GetDiagnostics(), annotations);
		}

		// Token: 0x060052AD RID: 21165 RVA: 0x0014AF7C File Offset: 0x0014917C
		internal ThisExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.token = syntaxToken;
		}

		// Token: 0x060052AE RID: 21166 RVA: 0x0014AFB1 File Offset: 0x001491B1
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.token);
		}

		// Token: 0x060052AF RID: 21167 RVA: 0x0014AFC6 File Offset: 0x001491C6
		static ThisExpressionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(ThisExpressionSyntax), (ObjectReader r) => new ThisExpressionSyntax(r));
		}

		// Token: 0x0400162A RID: 5674
		internal readonly SyntaxToken token;
	}
}
