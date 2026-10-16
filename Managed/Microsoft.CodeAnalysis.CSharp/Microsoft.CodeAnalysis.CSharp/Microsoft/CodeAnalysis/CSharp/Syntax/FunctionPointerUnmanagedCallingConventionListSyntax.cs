using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000296 RID: 662
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FunctionPointerUnmanagedCallingConventionListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003E1A RID: 15898 RVA: 0x00109C7D File Offset: 0x00107E7D
		internal FunctionPointerUnmanagedCallingConventionListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06003E1B RID: 15899 RVA: 0x00109C88 File Offset: 0x00107E88
		public SyntaxToken OpenBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerUnmanagedCallingConventionListSyntax)base.Green).openBracketToken, base.Position, 0);
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003E1C RID: 15900 RVA: 0x00109CA8 File Offset: 0x00107EA8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> CallingConventions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.callingConventions, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax>);
				}
				return new SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x00109CDD File Offset: 0x00107EDD
		public SyntaxToken CloseBracketToken
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerUnmanagedCallingConventionListSyntax)base.Green).closeBracketToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00109D03 File Offset: 0x00107F03
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.callingConventions, 1);
		}

		// Token: 0x06003E1F RID: 15903 RVA: 0x00109D18 File Offset: 0x00107F18
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.callingConventions;
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x00109D26 File Offset: 0x00107F26
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerUnmanagedCallingConventionList(this);
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x00109D2F File Offset: 0x00107F2F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerUnmanagedCallingConventionList(this);
		}

		// Token: 0x06003E22 RID: 15906 RVA: 0x00109D38 File Offset: 0x00107F38
		public FunctionPointerUnmanagedCallingConventionListSyntax Update(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
		{
			if (!(openBracketToken != this.OpenBracketToken) && !(callingConventions != this.CallingConventions) && !(closeBracketToken != this.CloseBracketToken))
			{
				return this;
			}
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax = SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(openBracketToken, callingConventions, closeBracketToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return functionPointerUnmanagedCallingConventionListSyntax;
			}
			return functionPointerUnmanagedCallingConventionListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E23 RID: 15907 RVA: 0x00109D91 File Offset: 0x00107F91
		public FunctionPointerUnmanagedCallingConventionListSyntax WithOpenBracketToken(SyntaxToken openBracketToken)
		{
			return this.Update(openBracketToken, this.CallingConventions, this.CloseBracketToken);
		}

		// Token: 0x06003E24 RID: 15908 RVA: 0x00109DA6 File Offset: 0x00107FA6
		public FunctionPointerUnmanagedCallingConventionListSyntax WithCallingConventions([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions)
		{
			return this.Update(this.OpenBracketToken, callingConventions, this.CloseBracketToken);
		}

		// Token: 0x06003E25 RID: 15909 RVA: 0x00109DBB File Offset: 0x00107FBB
		public FunctionPointerUnmanagedCallingConventionListSyntax WithCloseBracketToken(SyntaxToken closeBracketToken)
		{
			return this.Update(this.OpenBracketToken, this.CallingConventions, closeBracketToken);
		}

		// Token: 0x06003E26 RID: 15910 RVA: 0x00109DD0 File Offset: 0x00107FD0
		public FunctionPointerUnmanagedCallingConventionListSyntax AddCallingConventions(params FunctionPointerUnmanagedCallingConventionSyntax[] items)
		{
			return this.WithCallingConventions(this.CallingConventions.AddRange(items));
		}

		// Token: 0x04001454 RID: 5204
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode callingConventions;
	}
}
