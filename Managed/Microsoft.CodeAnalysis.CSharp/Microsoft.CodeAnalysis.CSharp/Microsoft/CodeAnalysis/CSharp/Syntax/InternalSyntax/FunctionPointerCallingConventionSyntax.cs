using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000384 RID: 900
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerCallingConventionSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005146 RID: 20806 RVA: 0x00147A26 File Offset: 0x00145C26
		internal FunctionPointerCallingConventionSyntax(SyntaxKind kind, SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(managedOrUnmanagedKeyword);
			this.managedOrUnmanagedKeyword = managedOrUnmanagedKeyword;
			if (unmanagedCallingConventionList != null)
			{
				base.AdjustFlagsAndWidth(unmanagedCallingConventionList);
				this.unmanagedCallingConventionList = unmanagedCallingConventionList;
			}
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00147A59 File Offset: 0x00145C59
		internal FunctionPointerCallingConventionSyntax(SyntaxKind kind, SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(managedOrUnmanagedKeyword);
			this.managedOrUnmanagedKeyword = managedOrUnmanagedKeyword;
			if (unmanagedCallingConventionList != null)
			{
				base.AdjustFlagsAndWidth(unmanagedCallingConventionList);
				this.unmanagedCallingConventionList = unmanagedCallingConventionList;
			}
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00147A90 File Offset: 0x00145C90
		internal FunctionPointerCallingConventionSyntax(SyntaxKind kind, SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
			: base(kind)
		{
			base.SlotCount = 2;
			base.AdjustFlagsAndWidth(managedOrUnmanagedKeyword);
			this.managedOrUnmanagedKeyword = managedOrUnmanagedKeyword;
			if (unmanagedCallingConventionList != null)
			{
				base.AdjustFlagsAndWidth(unmanagedCallingConventionList);
				this.unmanagedCallingConventionList = unmanagedCallingConventionList;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x00147ABF File Offset: 0x00145CBF
		public SyntaxToken ManagedOrUnmanagedKeyword
		{
			get
			{
				return this.managedOrUnmanagedKeyword;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x00147AC7 File Offset: 0x00145CC7
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public FunctionPointerUnmanagedCallingConventionListSyntax UnmanagedCallingConventionList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.unmanagedCallingConventionList;
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x00147AD0 File Offset: 0x00145CD0
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
					greenNode = this.unmanagedCallingConventionList;
				}
			}
			else
			{
				greenNode = this.managedOrUnmanagedKeyword;
			}
			return greenNode;
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00147AFB File Offset: 0x00145CFB
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FunctionPointerCallingConventionSyntax(this, parent, position);
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00147B05 File Offset: 0x00145D05
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerCallingConvention(this);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x00147B0E File Offset: 0x00145D0E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerCallingConvention(this);
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x00147B18 File Offset: 0x00145D18
		public FunctionPointerCallingConventionSyntax Update(SyntaxToken managedOrUnmanagedKeyword, FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
		{
			if (managedOrUnmanagedKeyword != this.ManagedOrUnmanagedKeyword || unmanagedCallingConventionList != this.UnmanagedCallingConventionList)
			{
				FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword, unmanagedCallingConventionList);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					functionPointerCallingConventionSyntax = functionPointerCallingConventionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					functionPointerCallingConventionSyntax = functionPointerCallingConventionSyntax.WithAnnotationsGreen(annotations);
				}
				return functionPointerCallingConventionSyntax;
			}
			return this;
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x00147B6E File Offset: 0x00145D6E
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FunctionPointerCallingConventionSyntax(base.Kind, this.managedOrUnmanagedKeyword, this.unmanagedCallingConventionList, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x00147B8E File Offset: 0x00145D8E
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FunctionPointerCallingConventionSyntax(base.Kind, this.managedOrUnmanagedKeyword, this.unmanagedCallingConventionList, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00147BB0 File Offset: 0x00145DB0
		internal FunctionPointerCallingConventionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 2;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.managedOrUnmanagedKeyword = syntaxToken;
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax = (FunctionPointerUnmanagedCallingConventionListSyntax)reader.ReadValue();
			if (functionPointerUnmanagedCallingConventionListSyntax != null)
			{
				base.AdjustFlagsAndWidth(functionPointerUnmanagedCallingConventionListSyntax);
				this.unmanagedCallingConventionList = functionPointerUnmanagedCallingConventionListSyntax;
			}
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x00147C02 File Offset: 0x00145E02
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.managedOrUnmanagedKeyword);
			writer.WriteValue(this.unmanagedCallingConventionList);
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x00147C23 File Offset: 0x00145E23
		static FunctionPointerCallingConventionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FunctionPointerCallingConventionSyntax), (ObjectReader r) => new FunctionPointerCallingConventionSyntax(r));
		}

		// Token: 0x040015F5 RID: 5621
		internal readonly SyntaxToken managedOrUnmanagedKeyword;

		// Token: 0x040015F6 RID: 5622
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList;
	}
}
