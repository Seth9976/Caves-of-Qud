using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000382 RID: 898
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerTypeSyntax : TypeSyntax
	{
		// Token: 0x06005125 RID: 20773 RVA: 0x00147404 File Offset: 0x00145604
		internal FunctionPointerTypeSyntax(SyntaxKind kind, SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			base.AdjustFlagsAndWidth(asteriskToken);
			this.asteriskToken = asteriskToken;
			if (callingConvention != null)
			{
				base.AdjustFlagsAndWidth(callingConvention);
				this.callingConvention = callingConvention;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
		}

		// Token: 0x06005126 RID: 20774 RVA: 0x00147464 File Offset: 0x00145664
		internal FunctionPointerTypeSyntax(SyntaxKind kind, SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			base.AdjustFlagsAndWidth(asteriskToken);
			this.asteriskToken = asteriskToken;
			if (callingConvention != null)
			{
				base.AdjustFlagsAndWidth(callingConvention);
				this.callingConvention = callingConvention;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
		}

		// Token: 0x06005127 RID: 20775 RVA: 0x001474C8 File Offset: 0x001456C8
		internal FunctionPointerTypeSyntax(SyntaxKind kind, SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
			: base(kind)
		{
			base.SlotCount = 4;
			base.AdjustFlagsAndWidth(delegateKeyword);
			this.delegateKeyword = delegateKeyword;
			base.AdjustFlagsAndWidth(asteriskToken);
			this.asteriskToken = asteriskToken;
			if (callingConvention != null)
			{
				base.AdjustFlagsAndWidth(callingConvention);
				this.callingConvention = callingConvention;
			}
			base.AdjustFlagsAndWidth(parameterList);
			this.parameterList = parameterList;
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x00147523 File Offset: 0x00145723
		public SyntaxToken DelegateKeyword
		{
			get
			{
				return this.delegateKeyword;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x0014752B File Offset: 0x0014572B
		public SyntaxToken AsteriskToken
		{
			get
			{
				return this.asteriskToken;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x0600512A RID: 20778 RVA: 0x00147533 File Offset: 0x00145733
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public FunctionPointerCallingConventionSyntax CallingConvention
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.callingConvention;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x0014753B File Offset: 0x0014573B
		public FunctionPointerParameterListSyntax ParameterList
		{
			get
			{
				return this.parameterList;
			}
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x00147544 File Offset: 0x00145744
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			GreenNode greenNode;
			switch (index)
			{
			case 0:
				greenNode = this.delegateKeyword;
				break;
			case 1:
				greenNode = this.asteriskToken;
				break;
			case 2:
				greenNode = this.callingConvention;
				break;
			case 3:
				greenNode = this.parameterList;
				break;
			default:
				greenNode = null;
				break;
			}
			return greenNode;
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x00147590 File Offset: 0x00145790
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FunctionPointerTypeSyntax(this, parent, position);
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x0014759A File Offset: 0x0014579A
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x0600512F RID: 20783 RVA: 0x001475A3 File Offset: 0x001457A3
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06005130 RID: 20784 RVA: 0x001475AC File Offset: 0x001457AC
		public FunctionPointerTypeSyntax Update(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
		{
			if (delegateKeyword != this.DelegateKeyword || asteriskToken != this.AsteriskToken || callingConvention != this.CallingConvention || parameterList != this.ParameterList)
			{
				FunctionPointerTypeSyntax functionPointerTypeSyntax = SyntaxFactory.FunctionPointerType(delegateKeyword, asteriskToken, callingConvention, parameterList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					functionPointerTypeSyntax = functionPointerTypeSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					functionPointerTypeSyntax = functionPointerTypeSyntax.WithAnnotationsGreen(annotations);
				}
				return functionPointerTypeSyntax;
			}
			return this;
		}

		// Token: 0x06005131 RID: 20785 RVA: 0x00147618 File Offset: 0x00145818
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FunctionPointerTypeSyntax(base.Kind, this.delegateKeyword, this.asteriskToken, this.callingConvention, this.parameterList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005132 RID: 20786 RVA: 0x00147644 File Offset: 0x00145844
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FunctionPointerTypeSyntax(base.Kind, this.delegateKeyword, this.asteriskToken, this.callingConvention, this.parameterList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005133 RID: 20787 RVA: 0x00147670 File Offset: 0x00145870
		internal FunctionPointerTypeSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 4;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.delegateKeyword = syntaxToken;
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.asteriskToken = syntaxToken2;
			FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = (FunctionPointerCallingConventionSyntax)reader.ReadValue();
			if (functionPointerCallingConventionSyntax != null)
			{
				base.AdjustFlagsAndWidth(functionPointerCallingConventionSyntax);
				this.callingConvention = functionPointerCallingConventionSyntax;
			}
			FunctionPointerParameterListSyntax functionPointerParameterListSyntax = (FunctionPointerParameterListSyntax)reader.ReadValue();
			base.AdjustFlagsAndWidth(functionPointerParameterListSyntax);
			this.parameterList = functionPointerParameterListSyntax;
		}

		// Token: 0x06005134 RID: 20788 RVA: 0x001476F6 File Offset: 0x001458F6
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.delegateKeyword);
			writer.WriteValue(this.asteriskToken);
			writer.WriteValue(this.callingConvention);
			writer.WriteValue(this.parameterList);
		}

		// Token: 0x06005135 RID: 20789 RVA: 0x0014772F File Offset: 0x0014592F
		static FunctionPointerTypeSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FunctionPointerTypeSyntax), (ObjectReader r) => new FunctionPointerTypeSyntax(r));
		}

		// Token: 0x040015EE RID: 5614
		internal readonly SyntaxToken delegateKeyword;

		// Token: 0x040015EF RID: 5615
		internal readonly SyntaxToken asteriskToken;

		// Token: 0x040015F0 RID: 5616
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly FunctionPointerCallingConventionSyntax callingConvention;

		// Token: 0x040015F1 RID: 5617
		internal readonly FunctionPointerParameterListSyntax parameterList;
	}
}
