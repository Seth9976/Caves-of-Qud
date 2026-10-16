using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200048E RID: 1166
	internal abstract class NamedTypeReference : INamedTypeReference, ITypeReference, IReference, INamedEntity
	{
		// Token: 0x060060E2 RID: 24802 RVA: 0x00175ABC File Offset: 0x00173CBC
		public NamedTypeReference(NamedTypeSymbol underlyingNamedType)
		{
			this.UnderlyingNamedType = underlyingNamedType;
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x060060E3 RID: 24803 RVA: 0x00175ACB File Offset: 0x00173CCB
		ushort INamedTypeReference.GenericParameterCount
		{
			get
			{
				return (ushort)this.UnderlyingNamedType.Arity;
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x060060E4 RID: 24804 RVA: 0x00175AD9 File Offset: 0x00173CD9
		bool INamedTypeReference.MangleName
		{
			get
			{
				return this.UnderlyingNamedType.MangleName;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x060060E5 RID: 24805 RVA: 0x00175AE6 File Offset: 0x00173CE6
		string INamedEntity.Name
		{
			get
			{
				return this.UnderlyingNamedType.MetadataName;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x00175AF3 File Offset: 0x00173CF3
		bool ITypeReference.IsEnum
		{
			get
			{
				return this.UnderlyingNamedType.IsEnumType();
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x060060E7 RID: 24807 RVA: 0x00175B00 File Offset: 0x00173D00
		bool ITypeReference.IsValueType
		{
			get
			{
				return this.UnderlyingNamedType.IsValueType;
			}
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x00175B0D File Offset: 0x00173D0D
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return null;
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x060060E9 RID: 24809 RVA: 0x00175B10 File Offset: 0x00173D10
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x00175B14 File Offset: 0x00173D14
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x060060EB RID: 24811 RVA: 0x00175B2A File Offset: 0x00173D2A
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060060EC RID: 24812
		public abstract IGenericTypeInstanceReference AsGenericTypeInstanceReference { get; }

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x060060ED RID: 24813 RVA: 0x00175B2D File Offset: 0x00173D2D
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060060EE RID: 24814 RVA: 0x00175B30 File Offset: 0x00173D30
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x060060EF RID: 24815
		public abstract INamespaceTypeReference AsNamespaceTypeReference { get; }

		// Token: 0x060060F0 RID: 24816 RVA: 0x00175B33 File Offset: 0x00173D33
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x060060F1 RID: 24817
		public abstract INestedTypeReference AsNestedTypeReference { get; }

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060060F2 RID: 24818
		public abstract ISpecializedNestedTypeReference AsSpecializedNestedTypeReference { get; }

		// Token: 0x060060F3 RID: 24819 RVA: 0x00175B36 File Offset: 0x00173D36
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x00175B39 File Offset: 0x00173D39
		public override string ToString()
		{
			return this.UnderlyingNamedType.ToDisplayString(SymbolDisplayFormat.ILVisualizationFormat);
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x00175B4B File Offset: 0x00173D4B
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060060F6 RID: 24822
		public abstract void Dispatch(MetadataVisitor visitor);

		// Token: 0x060060F7 RID: 24823 RVA: 0x00175B52 File Offset: 0x00173D52
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060060F8 RID: 24824 RVA: 0x00175B55 File Offset: 0x00173D55
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return this.UnderlyingNamedType;
		}

		// Token: 0x060060F9 RID: 24825 RVA: 0x00175B5D File Offset: 0x00173D5D
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x00175B64 File Offset: 0x00173D64
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0400195A RID: 6490
		protected readonly NamedTypeSymbol UnderlyingNamedType;
	}
}
