using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000487 RID: 1159
	internal sealed class ExpandedVarargsMethodReference : IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity, IGenericMethodInstanceReference, ISpecializedMethodReference
	{
		// Token: 0x06006097 RID: 24727 RVA: 0x001754FB File Offset: 0x001736FB
		public ExpandedVarargsMethodReference(IMethodReference underlyingMethod, ImmutableArray<IParameterTypeInformation> argListParams)
		{
			this._underlyingMethod = underlyingMethod;
			this._argListParams = argListParams;
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06006098 RID: 24728 RVA: 0x00175511 File Offset: 0x00173711
		bool IMethodReference.AcceptsExtraArguments
		{
			get
			{
				return this._underlyingMethod.AcceptsExtraArguments;
			}
		}

		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06006099 RID: 24729 RVA: 0x0017551E File Offset: 0x0017371E
		ushort IMethodReference.GenericParameterCount
		{
			get
			{
				return this._underlyingMethod.GenericParameterCount;
			}
		}

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x0600609A RID: 24730 RVA: 0x0017552B File Offset: 0x0017372B
		bool IMethodReference.IsGeneric
		{
			get
			{
				return this._underlyingMethod.IsGeneric;
			}
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x00175538 File Offset: 0x00173738
		IMethodDefinition IMethodReference.GetResolvedMethod(EmitContext context)
		{
			return this._underlyingMethod.GetResolvedMethod(context);
		}

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x0600609C RID: 24732 RVA: 0x00175546 File Offset: 0x00173746
		ImmutableArray<IParameterTypeInformation> IMethodReference.ExtraParameters
		{
			get
			{
				return this._argListParams;
			}
		}

		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x0600609D RID: 24733 RVA: 0x0017554E File Offset: 0x0017374E
		IGenericMethodInstanceReference IMethodReference.AsGenericMethodInstanceReference
		{
			get
			{
				if (this._underlyingMethod.AsGenericMethodInstanceReference == null)
				{
					return null;
				}
				return this;
			}
		}

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x00175560 File Offset: 0x00173760
		ISpecializedMethodReference IMethodReference.AsSpecializedMethodReference
		{
			get
			{
				if (this._underlyingMethod.AsSpecializedMethodReference == null)
				{
					return null;
				}
				return this;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x0600609F RID: 24735 RVA: 0x00175572 File Offset: 0x00173772
		CallingConvention ISignature.CallingConvention
		{
			get
			{
				return this._underlyingMethod.CallingConvention;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x0017557F File Offset: 0x0017377F
		ushort ISignature.ParameterCount
		{
			get
			{
				return this._underlyingMethod.ParameterCount;
			}
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x0017558C File Offset: 0x0017378C
		ImmutableArray<IParameterTypeInformation> ISignature.GetParameters(EmitContext context)
		{
			return this._underlyingMethod.GetParameters(context);
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x0017559A File Offset: 0x0017379A
		ImmutableArray<ICustomModifier> ISignature.ReturnValueCustomModifiers
		{
			get
			{
				return this._underlyingMethod.ReturnValueCustomModifiers;
			}
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x001755A7 File Offset: 0x001737A7
		ImmutableArray<ICustomModifier> ISignature.RefCustomModifiers
		{
			get
			{
				return this._underlyingMethod.RefCustomModifiers;
			}
		}

		// Token: 0x1700167B RID: 5755
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x001755B4 File Offset: 0x001737B4
		bool ISignature.ReturnValueIsByRef
		{
			get
			{
				return this._underlyingMethod.ReturnValueIsByRef;
			}
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x001755C1 File Offset: 0x001737C1
		ITypeReference ISignature.GetType(EmitContext context)
		{
			return this._underlyingMethod.GetType(context);
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x001755CF File Offset: 0x001737CF
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return this._underlyingMethod.GetContainingType(context);
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x001755DD File Offset: 0x001737DD
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return this._underlyingMethod.GetAttributes(context);
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x001755EB File Offset: 0x001737EB
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			if (((IMethodReference)this).AsGenericMethodInstanceReference != null)
			{
				visitor.Visit(this);
				return;
			}
			if (((IMethodReference)this).AsSpecializedMethodReference != null)
			{
				visitor.Visit(this);
				return;
			}
			visitor.Visit(this);
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x00175614 File Offset: 0x00173814
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x00175617 File Offset: 0x00173817
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x1700167C RID: 5756
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x0017561A File Offset: 0x0017381A
		string INamedEntity.Name
		{
			get
			{
				return this._underlyingMethod.Name;
			}
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x00175627 File Offset: 0x00173827
		IEnumerable<ITypeReference> IGenericMethodInstanceReference.GetGenericArguments(EmitContext context)
		{
			return this._underlyingMethod.AsGenericMethodInstanceReference.GetGenericArguments(context);
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x0017563A File Offset: 0x0017383A
		IMethodReference IGenericMethodInstanceReference.GetGenericMethod(EmitContext context)
		{
			return new ExpandedVarargsMethodReference(this._underlyingMethod.AsGenericMethodInstanceReference.GetGenericMethod(context), this._argListParams);
		}

		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x00175658 File Offset: 0x00173858
		IMethodReference ISpecializedMethodReference.UnspecializedVersion
		{
			get
			{
				return new ExpandedVarargsMethodReference(this._underlyingMethod.AsSpecializedMethodReference.UnspecializedVersion, this._argListParams);
			}
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x00175678 File Offset: 0x00173878
		public override string ToString()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			ExpandedVarargsMethodReference.Append(instance, this._underlyingMethod.GetInternalSymbol() ?? this._underlyingMethod);
			instance.Builder.Append(" with __arglist( ");
			bool flag = true;
			foreach (IParameterTypeInformation parameterTypeInformation in this._argListParams)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					instance.Builder.Append(", ");
				}
				if (parameterTypeInformation.IsByReference)
				{
					instance.Builder.Append("ref ");
				}
				ExpandedVarargsMethodReference.Append(instance, parameterTypeInformation.GetType(default(EmitContext)));
			}
			instance.Builder.Append(")");
			return instance.ToStringAndFree();
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x0017573C File Offset: 0x0017393C
		private static void Append(PooledStringBuilder result, object value)
		{
			ISymbolInternal symbolInternal = value as ISymbolInternal;
			ISymbol symbol = ((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
			if (symbol != null)
			{
				result.Builder.Append(symbol.ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat));
				return;
			}
			result.Builder.Append(value);
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x00175784 File Offset: 0x00173984
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x0017578B File Offset: 0x0017398B
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001955 RID: 6485
		private readonly IMethodReference _underlyingMethod;

		// Token: 0x04001956 RID: 6486
		private readonly ImmutableArray<IParameterTypeInformation> _argListParams;
	}
}
