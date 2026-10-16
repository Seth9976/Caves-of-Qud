using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000295 RID: 661
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class FunctionPointerCallingConventionSyntax : CSharpSyntaxNode
	{
		// Token: 0x06003E0F RID: 15887 RVA: 0x00109B6A File Offset: 0x00107D6A
		internal FunctionPointerCallingConventionSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06003E10 RID: 15888 RVA: 0x00109B75 File Offset: 0x00107D75
		public SyntaxToken ManagedOrUnmanagedKeyword
		{
			get
			{
				return new SyntaxToken(this, ((FunctionPointerCallingConventionSyntax)base.Green).managedOrUnmanagedKeyword, base.Position, 0);
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06003E11 RID: 15889 RVA: 0x00109B94 File Offset: 0x00107D94
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public FunctionPointerUnmanagedCallingConventionListSyntax UnmanagedCallingConventionList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.GetRed<FunctionPointerUnmanagedCallingConventionListSyntax>(ref this.unmanagedCallingConventionList, 1);
			}
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00109BA3 File Offset: 0x00107DA3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetNodeSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return base.GetRed<FunctionPointerUnmanagedCallingConventionListSyntax>(ref this.unmanagedCallingConventionList, 1);
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00109BB8 File Offset: 0x00107DB8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override SyntaxNode GetCachedSlot(int index)
		{
			if (index != 1)
			{
				return null;
			}
			return this.unmanagedCallingConventionList;
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00109BC6 File Offset: 0x00107DC6
		public override void Accept(CSharpSyntaxVisitor visitor)
		{
			visitor.VisitFunctionPointerCallingConvention(this);
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x00109BCF File Offset: 0x00107DCF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override TResult Accept<TResult>([global::System.Runtime.CompilerServices.Nullable(1)] CSharpSyntaxVisitor<TResult> visitor)
		{
			return visitor.VisitFunctionPointerCallingConvention(this);
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00109BD8 File Offset: 0x00107DD8
		public FunctionPointerCallingConventionSyntax Update(SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
		{
			if (!(managedOrUnmanagedKeyword != this.ManagedOrUnmanagedKeyword) && unmanagedCallingConventionList == this.UnmanagedCallingConventionList)
			{
				return this;
			}
			FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = SyntaxFactory.FunctionPointerCallingConvention(managedOrUnmanagedKeyword, unmanagedCallingConventionList);
			SyntaxAnnotation[] annotations = base.GetAnnotations();
			if (annotations == null || annotations.Length == 0)
			{
				return functionPointerCallingConventionSyntax;
			}
			return functionPointerCallingConventionSyntax.WithAnnotations(annotations);
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x00109C1D File Offset: 0x00107E1D
		public FunctionPointerCallingConventionSyntax WithManagedOrUnmanagedKeyword(SyntaxToken managedOrUnmanagedKeyword)
		{
			return this.Update(managedOrUnmanagedKeyword, this.UnmanagedCallingConventionList);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00109C2C File Offset: 0x00107E2C
		public FunctionPointerCallingConventionSyntax WithUnmanagedCallingConventionList([global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
		{
			return this.Update(this.ManagedOrUnmanagedKeyword, unmanagedCallingConventionList);
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00109C3C File Offset: 0x00107E3C
		public FunctionPointerCallingConventionSyntax AddUnmanagedCallingConventionListCallingConventions(params FunctionPointerUnmanagedCallingConventionSyntax[] items)
		{
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax;
			if ((functionPointerUnmanagedCallingConventionListSyntax = this.UnmanagedCallingConventionList) == null)
			{
				functionPointerUnmanagedCallingConventionListSyntax = SyntaxFactory.FunctionPointerUnmanagedCallingConventionList(default(SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax>));
			}
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax2 = functionPointerUnmanagedCallingConventionListSyntax;
			return this.WithUnmanagedCallingConventionList(functionPointerUnmanagedCallingConventionListSyntax2.WithCallingConventions(functionPointerUnmanagedCallingConventionListSyntax2.CallingConventions.AddRange(items)));
		}

		// Token: 0x04001453 RID: 5203
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList;
	}
}
