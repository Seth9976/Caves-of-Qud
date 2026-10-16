using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000294 RID: 660
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FunctionPointerParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003E02 RID: 15874 RVA: 0x001099F6 File Offset: 0x00107BF6
		internal FunctionPointerParameterListSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x00109A01 File Offset: 0x00107C01
		public SyntaxToken LessThanToken
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerParameterListSyntax)base.Green).lessThanToken, base.Position, 0);
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x00109A20 File Offset: 0x00107C20
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public SeparatedSyntaxList<FunctionPointerParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				SyntaxNode red = base.GetRed(ref this.parameters, 1);
				if (red == null)
				{
					return default(SeparatedSyntaxList<FunctionPointerParameterSyntax>);
				}
				return new SeparatedSyntaxList<FunctionPointerParameterSyntax>(red, base.GetChildIndex(1));
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x00109A55 File Offset: 0x00107C55
		public SyntaxToken GreaterThanToken
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerParameterListSyntax)base.Green).greaterThanToken, this.GetChildPosition(2), base.GetChildIndex(2));
			}
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00109A7B File Offset: 0x00107C7B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed(ref this.parameters, 1);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00109A90 File Offset: 0x00107C90
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.parameters;
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x00109A9E File Offset: 0x00107C9E
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerParameterList(this);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00109AA7 File Offset: 0x00107CA7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerParameterList(this);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00109AB0 File Offset: 0x00107CB0
		public FunctionPointerParameterListSyntax Update(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			if (!(lessThanToken != this.LessThanToken) && !(parameters != this.Parameters) && !(greaterThanToken != this.GreaterThanToken))
			{
				return this;
			}
			FunctionPointerParameterListSyntax functionPointerParameterListSyntax = SyntaxFactory.FunctionPointerParameterList(lessThanToken, parameters, greaterThanToken);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return functionPointerParameterListSyntax;
			}
			return functionPointerParameterListSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00109B09 File Offset: 0x00107D09
		public FunctionPointerParameterListSyntax WithLessThanToken(SyntaxToken lessThanToken)
		{
			return this.Update(lessThanToken, this.Parameters, this.GreaterThanToken);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00109B1E File Offset: 0x00107D1E
		public FunctionPointerParameterListSyntax WithParameters([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters)
		{
			return this.Update(this.LessThanToken, parameters, this.GreaterThanToken);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00109B33 File Offset: 0x00107D33
		public FunctionPointerParameterListSyntax WithGreaterThanToken(SyntaxToken greaterThanToken)
		{
			return this.Update(this.LessThanToken, this.Parameters, greaterThanToken);
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00109B48 File Offset: 0x00107D48
		public FunctionPointerParameterListSyntax AddParameters(params FunctionPointerParameterSyntax[] items)
		{
			return this.WithParameters(this.Parameters.AddRange(items));
		}

		// Token: 0x04001452 RID: 5202
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxNode parameters;
	}
}
