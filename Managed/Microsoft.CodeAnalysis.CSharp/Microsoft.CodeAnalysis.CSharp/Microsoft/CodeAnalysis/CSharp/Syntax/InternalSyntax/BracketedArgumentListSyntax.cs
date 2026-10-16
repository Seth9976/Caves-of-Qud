using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003AB RID: 939
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BracketedArgumentListSyntax : BaseArgumentListSyntax
	{
		// Token: 0x06005377 RID: 21367 RVA: 0x0014D208 File Offset: 0x0014B408
		internal BracketedArgumentListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x0014D258 File Offset: 0x0014B458
		internal BracketedArgumentListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeBracketToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x0014D2AA File Offset: 0x0014B4AA
		internal BracketedArgumentListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode arguments, SyntaxToken closeBracketToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (arguments != null)
			{
				base.AdjustFlagsAndWidth(arguments);
				this.arguments = arguments;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x0014D2E9 File Offset: 0x0014B4E9
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x0014D2F1 File Offset: 0x0014B4F1
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override SeparatedSyntaxList<ArgumentSyntax> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<ArgumentSyntax>(new SyntaxList<CSharpSyntaxNode>(this.arguments));
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x0600537C RID: 21372 RVA: 0x0014D308 File Offset: 0x0014B508
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x0014D310 File Offset: 0x0014B510
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
				greenNode = this.arguments;
				break;
			case 2:
				greenNode = this.closeBracketToken;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x0014D34F File Offset: 0x0014B54F
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new BracketedArgumentListSyntax(this, parent, position);
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0014D359 File Offset: 0x0014B559
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitBracketedArgumentList(this);
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x0014D362 File Offset: 0x0014B562
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitBracketedArgumentList(this);
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x0014D36C File Offset: 0x0014B56C
		public BracketedArgumentListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
		{
			if (openBracketToken == this.OpenBracketToken)
			{
				SeparatedSyntaxList<ArgumentSyntax> separatedSyntaxList = this.Arguments;
				if (!((in arguments) != (in separatedSyntaxList)) && closeBracketToken == this.CloseBracketToken)
				{
					return this;
				}
			}
			BracketedArgumentListSyntax bracketedArgumentListSyntax = SyntaxFactory.BracketedArgumentList(openBracketToken, arguments, closeBracketToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				bracketedArgumentListSyntax = bracketedArgumentListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				bracketedArgumentListSyntax = bracketedArgumentListSyntax.WithAnnotationsGreen(annotations);
			}
			return bracketedArgumentListSyntax;
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x0014D3D5 File Offset: 0x0014B5D5
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new BracketedArgumentListSyntax(base.Kind, this.openBracketToken, this.arguments, this.closeBracketToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x0014D3FB File Offset: 0x0014B5FB
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new BracketedArgumentListSyntax(base.Kind, this.openBracketToken, this.arguments, this.closeBracketToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x0014D424 File Offset: 0x0014B624
		internal BracketedArgumentListSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 3;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.openBracketToken = syntaxToken;
			GreenNode greenNode = (GreenNode)reader.ReadValue();
			if (greenNode != null)
			{
				base.AdjustFlagsAndWidth(greenNode);
				this.arguments = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBracketToken = syntaxToken2;
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0014D490 File Offset: 0x0014B690
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.arguments);
			writer.WriteValue(this.closeBracketToken);
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x0014D4BD File Offset: 0x0014B6BD
		static BracketedArgumentListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(BracketedArgumentListSyntax), (ObjectReader r) => new BracketedArgumentListSyntax(r));
		}

		// Token: 0x04001652 RID: 5714
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x04001653 RID: 5715
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode arguments;

		// Token: 0x04001654 RID: 5716
		internal readonly SyntaxToken closeBracketToken;
	}
}
