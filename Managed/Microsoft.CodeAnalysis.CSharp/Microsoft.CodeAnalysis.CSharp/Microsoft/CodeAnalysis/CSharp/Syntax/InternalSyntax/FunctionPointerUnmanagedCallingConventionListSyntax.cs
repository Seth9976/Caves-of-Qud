using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000385 RID: 901
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class FunctionPointerUnmanagedCallingConventionListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005155 RID: 20821 RVA: 0x00147C44 File Offset: 0x00145E44
		internal FunctionPointerUnmanagedCallingConventionListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode callingConventions, SyntaxToken closeBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (callingConventions != null)
			{
				base.AdjustFlagsAndWidth(callingConventions);
				this.callingConventions = callingConventions;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x00147C94 File Offset: 0x00145E94
		internal FunctionPointerUnmanagedCallingConventionListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode callingConventions, SyntaxToken closeBracketToken, SyntaxFactoryContext context)
			: base(kind)
		{
			base.SetFactoryContext(context);
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (callingConventions != null)
			{
				base.AdjustFlagsAndWidth(callingConventions);
				this.callingConventions = callingConventions;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x00147CE6 File Offset: 0x00145EE6
		internal FunctionPointerUnmanagedCallingConventionListSyntax(SyntaxKind kind, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] GreenNode callingConventions, SyntaxToken closeBracketToken)
			: base(kind)
		{
			base.SlotCount = 3;
			base.AdjustFlagsAndWidth(openBracketToken);
			this.openBracketToken = openBracketToken;
			if (callingConventions != null)
			{
				base.AdjustFlagsAndWidth(callingConventions);
				this.callingConventions = callingConventions;
			}
			base.AdjustFlagsAndWidth(closeBracketToken);
			this.closeBracketToken = closeBracketToken;
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06005158 RID: 20824 RVA: 0x00147D25 File Offset: 0x00145F25
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return this.openBracketToken;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x06005159 RID: 20825 RVA: 0x00147D2D File Offset: 0x00145F2D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> CallingConventions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return new SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax>(new SyntaxList<CSharpSyntaxNode>(this.callingConventions));
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x00147D44 File Offset: 0x00145F44
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return this.closeBracketToken;
			}
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x00147D4C File Offset: 0x00145F4C
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
				greenNode = this.callingConventions;
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

		// Token: 0x0600515C RID: 20828 RVA: 0x00147D8B File Offset: 0x00145F8B
		internal override SyntaxNode CreateRed([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
		{
			return new FunctionPointerUnmanagedCallingConventionListSyntax(this, parent, position);
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00147D95 File Offset: 0x00145F95
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerUnmanagedCallingConventionList(this);
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00147D9E File Offset: 0x00145F9E
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerUnmanagedCallingConventionList(this);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00147DA8 File Offset: 0x00145FA8
		public FunctionPointerUnmanagedCallingConventionListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
		{
			if (openBracketToken == this.OpenBracketToken)
			{
				SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> separatedSyntaxList = this.CallingConventions;
				if (!((in callingConventions) != (in separatedSyntaxList)) && closeBracketToken == this.CloseBracketToken)
				{
					return this;
				}
			}
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax = SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(openBracketToken, callingConventions, closeBracketToken);
			DiagnosticInfo[] diagnostics = base.GetDiagnostics();
			if (diagnostics != null && diagnostics.Length != 0)
			{
				functionPointerUnmanagedCallingConventionListSyntax = functionPointerUnmanagedCallingConventionListSyntax.WithDiagnosticsGreen(diagnostics);
			}
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations != null && annotations.Length != 0)
			{
				functionPointerUnmanagedCallingConventionListSyntax = functionPointerUnmanagedCallingConventionListSyntax.WithAnnotationsGreen(annotations);
			}
			return functionPointerUnmanagedCallingConventionListSyntax;
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x00147E11 File Offset: 0x00146011
		internal override GreenNode SetDiagnostics([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics)
		{
			return new FunctionPointerUnmanagedCallingConventionListSyntax(base.Kind, this.openBracketToken, this.callingConventions, this.closeBracketToken, diagnostics, base.GetAnnotations());
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x00147E37 File Offset: 0x00146037
		internal override GreenNode SetAnnotations([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
		{
			return new FunctionPointerUnmanagedCallingConventionListSyntax(base.Kind, this.openBracketToken, this.callingConventions, this.closeBracketToken, base.GetDiagnostics(), annotations);
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x00147E60 File Offset: 0x00146060
		internal FunctionPointerUnmanagedCallingConventionListSyntax(ObjectReader reader)
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
				this.callingConventions = greenNode;
			}
			SyntaxToken syntaxToken2 = (SyntaxToken)reader.ReadValue();
			base.AdjustFlagsAndWidth(syntaxToken2);
			this.closeBracketToken = syntaxToken2;
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x00147ECC File Offset: 0x001460CC
		internal override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteValue(this.openBracketToken);
			writer.WriteValue(this.callingConventions);
			writer.WriteValue(this.closeBracketToken);
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x00147EF9 File Offset: 0x001460F9
		static FunctionPointerUnmanagedCallingConventionListSyntax()
		{
			ObjectBinder.RegisterTypeReader(typeof(FunctionPointerUnmanagedCallingConventionListSyntax), (ObjectReader r) => new FunctionPointerUnmanagedCallingConventionListSyntax(r));
		}

		// Token: 0x040015F7 RID: 5623
		internal readonly SyntaxToken openBracketToken;

		// Token: 0x040015F8 RID: 5624
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal readonly GreenNode callingConventions;

		// Token: 0x040015F9 RID: 5625
		internal readonly SyntaxToken closeBracketToken;
	}
}
