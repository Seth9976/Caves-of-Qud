using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200048C RID: 1164
	internal abstract class MethodReference : TypeMemberReference, IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x060060C7 RID: 24775 RVA: 0x001758FD File Offset: 0x00173AFD
		public MethodReference(MethodSymbol underlyingMethod)
		{
			this.UnderlyingMethod = underlyingMethod;
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x060060C8 RID: 24776 RVA: 0x0017590C File Offset: 0x00173B0C
		protected override Symbol UnderlyingSymbol
		{
			get
			{
				return this.UnderlyingMethod;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x060060C9 RID: 24777 RVA: 0x00175914 File Offset: 0x00173B14
		bool IMethodReference.AcceptsExtraArguments
		{
			get
			{
				return this.UnderlyingMethod.IsVararg;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x060060CA RID: 24778 RVA: 0x00175921 File Offset: 0x00173B21
		ushort IMethodReference.GenericParameterCount
		{
			get
			{
				return (ushort)this.UnderlyingMethod.Arity;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x060060CB RID: 24779 RVA: 0x0017592F File Offset: 0x00173B2F
		bool IMethodReference.IsGeneric
		{
			get
			{
				return this.UnderlyingMethod.IsGenericMethod;
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x060060CC RID: 24780 RVA: 0x0017593C File Offset: 0x00173B3C
		ushort ISignature.ParameterCount
		{
			get
			{
				return (ushort)this.UnderlyingMethod.ParameterCount;
			}
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x0017594A File Offset: 0x00173B4A
		IMethodDefinition IMethodReference.GetResolvedMethod(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x060060CE RID: 24782 RVA: 0x0017594D File Offset: 0x00173B4D
		ImmutableArray<IParameterTypeInformation> IMethodReference.ExtraParameters
		{
			get
			{
				return ImmutableArray<IParameterTypeInformation>.Empty;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x060060CF RID: 24783 RVA: 0x00175954 File Offset: 0x00173B54
		CallingConvention ISignature.CallingConvention
		{
			get
			{
				return this.UnderlyingMethod.CallingConvention;
			}
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x00175961 File Offset: 0x00173B61
		ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingMethod.Parameters);
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x060060D1 RID: 24785 RVA: 0x00175980 File Offset: 0x00173B80
		ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.UnderlyingMethod.ReturnTypeWithAnnotations.CustomModifiers);
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x060060D2 RID: 24786 RVA: 0x001759A5 File Offset: 0x00173BA5
		ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(this.UnderlyingMethod.RefCustomModifiers);
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x060060D3 RID: 24787 RVA: 0x001759B7 File Offset: 0x00173BB7
		bool ISignature.ReturnValueIsByRef
		{
			get
			{
				return this.UnderlyingMethod.RefKind.IsManagedReference();
			}
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x001759C9 File Offset: 0x00173BC9
		ITypeReference ISignature.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingMethod.ReturnType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x060060D5 RID: 24789 RVA: 0x001759F7 File Offset: 0x00173BF7
		public virtual IGenericMethodInstanceReference AsGenericMethodInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x060060D6 RID: 24790 RVA: 0x001759FA File Offset: 0x00173BFA
		public virtual ISpecializedMethodReference AsSpecializedMethodReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001957 RID: 6487
		protected readonly MethodSymbol UnderlyingMethod;
	}
}
