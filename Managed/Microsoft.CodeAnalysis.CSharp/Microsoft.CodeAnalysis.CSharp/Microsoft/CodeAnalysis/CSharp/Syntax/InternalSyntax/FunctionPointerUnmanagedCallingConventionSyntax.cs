using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000386 RID: 902
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerUnmanagedCallingConventionSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005165 RID: 20837 RVA: 0x00147F1A File Offset: 0x0014611A
		internal FunctionPointerUnmanagedCallingConventionSyntax(SyntaxKind kind, SyntaxToken name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x00147F3B File Offset: 0x0014613B
		internal FunctionPointerUnmanagedCallingConventionSyntax(SyntaxKind kind, SyntaxToken name, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x00147F60 File Offset: 0x00146160
		internal FunctionPointerUnmanagedCallingConventionSyntax(SyntaxKind kind, SyntaxToken name)
			: base(kind)
		{
			base.SlotCount = 1;
			base.AdjustFlagsAndWidth(name);
			this.name = name;
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x06005168 RID: 20840 RVA: 0x00147F7E File Offset: 0x0014617E
		public SyntaxToken Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00147F86 File Offset: 0x00146186
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override GreenNode GetSlot(int index)
		{
			if (index != 0)
			{
				return null;
			}
			return this.name;
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x00147F93 File Offset: 0x00146193
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FunctionPointerUnmanagedCallingConventionSyntax(this, parent, position);
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x00147F9D File Offset: 0x0014619D
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerUnmanagedCallingConvention(this);
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x00147FA6 File Offset: 0x001461A6
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerUnmanagedCallingConvention(this);
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x00147FB0 File Offset: 0x001461B0
		public FunctionPointerUnmanagedCallingConventionSyntax Update(SyntaxToken name)
		{
			if (name != this.Name)
			{
				FunctionPointerUnmanagedCallingConventionSyntax functionPointerUnmanagedCallingConventionSyntax = SyntaxFactory.FunctionPointerUnmanagedCallingConvention(name);
				DiagnosticInfo[] diagnostics = base.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					functionPointerUnmanagedCallingConventionSyntax = functionPointerUnmanagedCallingConventionSyntax.WithDiagnosticsGreen(diagnostics);
				}
				SyntaxAnnotation[] annotations = base.GetAnnotations();
				if (annotations != null && annotations.Length != 0)
				{
					functionPointerUnmanagedCallingConventionSyntax = functionPointerUnmanagedCallingConventionSyntax.WithAnnotationsGreen(annotations);
				}
				return functionPointerUnmanagedCallingConventionSyntax;
			}
			return this;
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x00147FFC File Offset: 0x001461FC
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FunctionPointerUnmanagedCallingConventionSyntax(base.Kind, this.name, diagnostics, base.GetAnnotations());
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x00148016 File Offset: 0x00146216
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FunctionPointerUnmanagedCallingConventionSyntax(base.Kind, this.name, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005170 RID: 20848 RVA: 0x00148030 File Offset: 0x00146230
		internal FunctionPointerUnmanagedCallingConventionSyntax(ObjectReader reader)
			: base(reader)
		{
			base.SlotCount = 1;
			SyntaxToken syntaxToken = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken);
			this.name = syntaxToken;
		}

		// Token: 0x06005171 RID: 20849 RVA: 0x00148065 File Offset: 0x00146265
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.name);
		}

		// Token: 0x06005172 RID: 20850 RVA: 0x0014807A File Offset: 0x0014627A
		static FunctionPointerUnmanagedCallingConventionSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FunctionPointerUnmanagedCallingConventionSyntax), (ObjectReader r) => new FunctionPointerUnmanagedCallingConventionSyntax(r));
		}

		// Token: 0x040015FA RID: 5626
		internal readonly SyntaxToken name;
	}
}
