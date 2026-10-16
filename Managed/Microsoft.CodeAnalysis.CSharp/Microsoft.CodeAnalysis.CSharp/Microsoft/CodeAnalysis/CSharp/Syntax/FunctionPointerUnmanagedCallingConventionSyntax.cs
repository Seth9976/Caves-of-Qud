using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000297 RID: 663
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FunctionPointerUnmanagedCallingConventionSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003E27 RID: 15911 RVA: 0x00109DF2 File Offset: 0x00107FF2
		internal FunctionPointerUnmanagedCallingConventionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x00109DFD File Offset: 0x00107FFD
		public SyntaxToken Name
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerUnmanagedCallingConventionSyntax)base.Green).name, base.Position, 0);
			}
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x00109E1C File Offset: 0x0010801C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			return null;
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x00109E1F File Offset: 0x0010801F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			return null;
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x00109E22 File Offset: 0x00108022
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerUnmanagedCallingConvention(this);
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x00109E2B File Offset: 0x0010802B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerUnmanagedCallingConvention(this);
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x00109E34 File Offset: 0x00108034
		public FunctionPointerUnmanagedCallingConventionSyntax Update(SyntaxToken name)
		{
			if (!(name != this.Name))
			{
				return this;
			}
			FunctionPointerUnmanagedCallingConventionSyntax functionPointerUnmanagedCallingConventionSyntax = SyntaxFactory.FunctionPointerUnmanagedCallingConvention(name);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return functionPointerUnmanagedCallingConventionSyntax;
			}
			return functionPointerUnmanagedCallingConventionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x00109E6F File Offset: 0x0010806F
		public FunctionPointerUnmanagedCallingConventionSyntax WithName(SyntaxToken name)
		{
			return this.Update(name);
		}
	}
}
