using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000383 RID: 899
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005136 RID: 20790 RVA: 0x00147750 File Offset: 0x00145950
		internal FunctionPointerParameterListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken greaterThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
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

		// Token: 0x06005137 RID: 20791 RVA: 0x001477A0 File Offset: 0x001459A0
		internal FunctionPointerParameterListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken greaterThanToken, SyntaxFactoryContext context)
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

		// Token: 0x06005138 RID: 20792 RVA: 0x001477F2 File Offset: 0x001459F2
		internal FunctionPointerParameterListSyntax(SyntaxKind kind, SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode parameters, SyntaxToken greaterThanToken)
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

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06005139 RID: 20793 RVA: 0x00147831 File Offset: 0x00145A31
		public SyntaxToken LessThanToken
		{
			get
			{
				return this.lessThanToken;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x0600513A RID: 20794 RVA: 0x00147839 File Offset: 0x00145A39
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<FunctionPointerParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<FunctionPointerParameterSyntax>(new SyntaxList<CSharpSyntaxNode>(this.parameters));
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x00147850 File Offset: 0x00145A50
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return this.greaterThanToken;
			}
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x00147858 File Offset: 0x00145A58
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

		// Token: 0x0600513D RID: 20797 RVA: 0x00147897 File Offset: 0x00145A97
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FunctionPointerParameterListSyntax(this, parent, position);
		}

		// Token: 0x0600513E RID: 20798 RVA: 0x001478A1 File Offset: 0x00145AA1
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerParameterList(this);
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x001478AA File Offset: 0x00145AAA
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerParameterList(this);
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x001478B4 File Offset: 0x00145AB4
		public FunctionPointerParameterListSyntax Update(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			if (lessThanToken == this.LessThanToken)
			{
				SeparatedSyntaxList<FunctionPointerParameterSyntax> separatedSyntaxList = this.Parameters;
				if (!((in parameters) != (in separatedSyntaxList)) && greaterThanToken == this.GreaterThanToken)
				{
					return this;
				}
			}
			FunctionPointerParameterListSyntax functionPointerParameterListSyntax = SyntaxFactory.FunctionPointerParameterList(lessThanToken, parameters, greaterThanToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				functionPointerParameterListSyntax = functionPointerParameterListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				functionPointerParameterListSyntax = functionPointerParameterListSyntax.WithAnnotationsGreen(annotations);
			}
			return functionPointerParameterListSyntax;
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0014791D File Offset: 0x00145B1D
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FunctionPointerParameterListSyntax(base.Kind, this.lessThanToken, this.parameters, this.greaterThanToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x00147943 File Offset: 0x00145B43
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FunctionPointerParameterListSyntax(base.Kind, this.lessThanToken, this.parameters, this.greaterThanToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0014796C File Offset: 0x00145B6C
		internal FunctionPointerParameterListSyntax(ObjectReader reader)
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

		// Token: 0x06005144 RID: 20804 RVA: 0x001479D8 File Offset: 0x00145BD8
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.lessThanToken);
			writer.WriteValue(this.parameters);
			writer.WriteValue(this.greaterThanToken);
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x00147A05 File Offset: 0x00145C05
		static FunctionPointerParameterListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FunctionPointerParameterListSyntax), (ObjectReader r) => new FunctionPointerParameterListSyntax(r));
		}

		// Token: 0x040015F2 RID: 5618
		internal readonly SyntaxToken lessThanToken;

		// Token: 0x040015F3 RID: 5619
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode parameters;

		// Token: 0x040015F4 RID: 5620
		internal readonly SyntaxToken greaterThanToken;
	}
}
