using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000293 RID: 659
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FunctionPointerTypeSyntax : TypeSyntax
	{
		// Token: 0x06003DF3 RID: 15859 RVA: 0x0010980B File Offset: 0x00107A0B
		internal FunctionPointerTypeSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x00109816 File Offset: 0x00107A16
		public SyntaxToken DelegateKeyword
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerTypeSyntax)base.Green).delegateKeyword, base.Position, 0);
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x00109835 File Offset: 0x00107A35
		public SyntaxToken AsteriskToken
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerTypeSyntax)base.Green).asteriskToken, this.GetChildPosition(1), base.GetChildIndex(1));
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x0010985B File Offset: 0x00107A5B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public FunctionPointerCallingConventionSyntax CallingConvention
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<FunctionPointerCallingConventionSyntax>(ref this.callingConvention, 2);
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x0010986A File Offset: 0x00107A6A
		public FunctionPointerParameterListSyntax ParameterList
		{
			get
			{
				return base.GetRed<FunctionPointerParameterListSyntax>(ref this.parameterList, 3);
			}
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x0010987C File Offset: 0x00107A7C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 2)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = base.GetRed<FunctionPointerParameterListSyntax>(ref this.parameterList, 3);
				}
			}
			else
			{
				syntaxNode = base.GetRed<FunctionPointerCallingConventionSyntax>(ref this.callingConvention, 2);
			}
			return syntaxNode;
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x001098B8 File Offset: 0x00107AB8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			SyntaxNode syntaxNode;
			if (index != 2)
			{
				if (index != 3)
				{
					syntaxNode = null;
				}
				else
				{
					syntaxNode = this.parameterList;
				}
			}
			else
			{
				syntaxNode = this.callingConvention;
			}
			return syntaxNode;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x001098E4 File Offset: 0x00107AE4
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x001098ED File Offset: 0x00107AED
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerType(this);
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x001098F8 File Offset: 0x00107AF8
		public FunctionPointerTypeSyntax Update(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
		{
			if (!(delegateKeyword != this.DelegateKeyword) && !(asteriskToken != this.AsteriskToken) && callingConvention == this.CallingConvention && parameterList == this.ParameterList)
			{
				return this;
			}
			FunctionPointerTypeSyntax functionPointerTypeSyntax = SyntaxFactory.FunctionPointerType(delegateKeyword, asteriskToken, callingConvention, parameterList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return functionPointerTypeSyntax;
			}
			return functionPointerTypeSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00109958 File Offset: 0x00107B58
		public FunctionPointerTypeSyntax WithDelegateKeyword(SyntaxToken delegateKeyword)
		{
			return this.Update(delegateKeyword, this.AsteriskToken, this.CallingConvention, this.ParameterList);
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00109973 File Offset: 0x00107B73
		public FunctionPointerTypeSyntax WithAsteriskToken(SyntaxToken asteriskToken)
		{
			return this.Update(this.DelegateKeyword, asteriskToken, this.CallingConvention, this.ParameterList);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x0010998E File Offset: 0x00107B8E
		public FunctionPointerTypeSyntax WithCallingConvention([global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention)
		{
			return this.Update(this.DelegateKeyword, this.AsteriskToken, callingConvention, this.ParameterList);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x001099A9 File Offset: 0x00107BA9
		public FunctionPointerTypeSyntax WithParameterList(FunctionPointerParameterListSyntax parameterList)
		{
			return this.Update(this.DelegateKeyword, this.AsteriskToken, this.CallingConvention, parameterList);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x001099C4 File Offset: 0x00107BC4
		public FunctionPointerTypeSyntax AddParameterListParameters(params FunctionPointerParameterSyntax[] items)
		{
			return this.WithParameterList(this.ParameterList.WithParameters(this.ParameterList.Parameters.AddRange(items)));
		}

		// Token: 0x04001450 RID: 5200
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private FunctionPointerCallingConventionSyntax callingConvention;

		// Token: 0x04001451 RID: 5201
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private FunctionPointerParameterListSyntax parameterList;
	}
}
