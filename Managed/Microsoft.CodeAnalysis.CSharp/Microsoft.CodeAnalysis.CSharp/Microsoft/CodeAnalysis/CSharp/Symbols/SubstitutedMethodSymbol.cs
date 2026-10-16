using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000563 RID: 1379
	internal class SubstitutedMethodSymbol : WrappedMethodSymbol
	{
		// Token: 0x0600745D RID: 29789 RVA: 0x001B29CD File Offset: 0x001B0BCD
		internal SubstitutedMethodSymbol(NamedTypeSymbol containingSymbol, MethodSymbol originalDefinition)
			: this(containingSymbol, containingSymbol.TypeSubstitution, originalDefinition, null)
		{
		}

		// Token: 0x0600745E RID: 29790 RVA: 0x001B29E0 File Offset: 0x001B0BE0
		protected SubstitutedMethodSymbol(NamedTypeSymbol containingSymbol, TypeMap map, MethodSymbol originalDefinition, MethodSymbol constructedFrom)
		{
			this._containingType = containingSymbol;
			this._underlyingMethod = originalDefinition;
			this._inputMap = map;
			if (constructedFrom != null)
			{
				this._constructedFrom = constructedFrom;
				this._lazyTypeParameters = constructedFrom.TypeParameters;
				this._lazyMap = map;
				return;
			}
			this._constructedFrom = this;
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x0600745F RID: 29791 RVA: 0x001B2A30 File Offset: 0x001B0C30
		public override MethodSymbol UnderlyingMethod
		{
			get
			{
				return this._underlyingMethod;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x06007460 RID: 29792 RVA: 0x001B2A38 File Offset: 0x001B0C38
		public override MethodSymbol ConstructedFrom
		{
			get
			{
				return this._constructedFrom;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06007461 RID: 29793 RVA: 0x001B2A40 File Offset: 0x001B0C40
		private TypeMap Map
		{
			get
			{
				this.EnsureMapAndTypeParameters();
				return this._lazyMap;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06007462 RID: 29794 RVA: 0x001B2A4E File Offset: 0x001B0C4E
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				this.EnsureMapAndTypeParameters();
				return this._lazyTypeParameters;
			}
		}

		// Token: 0x06007463 RID: 29795 RVA: 0x001B2A5C File Offset: 0x001B0C5C
		private void EnsureMapAndTypeParameters()
		{
			if (!this._lazyTypeParameters.IsDefault)
			{
				return;
			}
			ImmutableArray<TypeParameterSymbol> immutableArray;
			TypeMap typeMap = this._inputMap.WithAlphaRename(this.OriginalDefinition, this, out immutableArray);
			TypeMap typeMap2 = Interlocked.CompareExchange<TypeMap>(ref this._lazyMap, typeMap, null);
			if (typeMap2 != null)
			{
				immutableArray = typeMap2.SubstituteTypeParameters(this.OriginalDefinition.TypeParameters);
			}
			ImmutableInterlocked.InterlockedCompareExchange<TypeParameterSymbol>(ref this._lazyTypeParameters, immutableArray, default(ImmutableArray<TypeParameterSymbol>));
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06007464 RID: 29796 RVA: 0x001B2AC5 File Offset: 0x001B0CC5
		public sealed override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this.OriginalDefinition.ContainingAssembly;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06007465 RID: 29797 RVA: 0x001B2AD2 File Offset: 0x001B0CD2
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x06007466 RID: 29798 RVA: 0x001B2ADA File Offset: 0x001B0CDA
		public sealed override MethodSymbol OriginalDefinition
		{
			get
			{
				return this._underlyingMethod;
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x06007467 RID: 29799 RVA: 0x001B2AE4 File Offset: 0x001B0CE4
		internal sealed override MethodSymbol CallsiteReducedFromMethod
		{
			get
			{
				MethodSymbol reducedFrom = this.OriginalDefinition.ReducedFrom;
				if (reducedFrom != null)
				{
					return reducedFrom.Construct(this.TypeArgumentsWithAnnotations);
				}
				return null;
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x06007468 RID: 29800 RVA: 0x001B2B10 File Offset: 0x001B0D10
		public override TypeSymbol ReceiverType
		{
			get
			{
				MethodSymbol callsiteReducedFromMethod = this.CallsiteReducedFromMethod;
				if (callsiteReducedFromMethod == null)
				{
					return this.ContainingType;
				}
				return callsiteReducedFromMethod.Parameters[0].Type;
			}
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x001B2B44 File Offset: 0x001B0D44
		public override TypeSymbol GetTypeInferredDuringReduction(TypeParameterSymbol reducedFromTypeParameter)
		{
			this.OriginalDefinition.GetTypeInferredDuringReduction(reducedFromTypeParameter);
			return this.TypeArgumentsWithAnnotations[reducedFromTypeParameter.Ordinal].Type;
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x0600746A RID: 29802 RVA: 0x001B2B7A File Offset: 0x001B0D7A
		public sealed override MethodSymbol ReducedFrom
		{
			get
			{
				return this.OriginalDefinition.ReducedFrom;
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x0600746B RID: 29803 RVA: 0x001B2B87 File Offset: 0x001B0D87
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x0600746C RID: 29804 RVA: 0x001B2B8F File Offset: 0x001B0D8F
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x0600746D RID: 29805 RVA: 0x001B2B97 File Offset: 0x001B0D97
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.OriginalDefinition.GetAttributes();
		}

		// Token: 0x0600746E RID: 29806 RVA: 0x001B2BA4 File Offset: 0x001B0DA4
		public override ImmutableArray<CSharpAttributeData> GetReturnTypeAttributes()
		{
			return this.OriginalDefinition.GetReturnTypeAttributes();
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x001B2BB1 File Offset: 0x001B0DB1
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return this.OriginalDefinition.GetUnmanagedCallersOnlyAttributeData(forceComplete);
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x06007470 RID: 29808 RVA: 0x001B2BC0 File Offset: 0x001B0DC0
		public sealed override Symbol AssociatedSymbol
		{
			get
			{
				Symbol associatedSymbol = this.OriginalDefinition.AssociatedSymbol;
				if (associatedSymbol != null)
				{
					return associatedSymbol.SymbolAsMember(this.ContainingType);
				}
				return null;
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x06007471 RID: 29809 RVA: 0x001B2BEC File Offset: 0x001B0DEC
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				if (this._lazyReturnType == null)
				{
					TypeWithAnnotations typeWithAnnotations = this.Map.SubstituteType(this.OriginalDefinition.ReturnTypeWithAnnotations);
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyReturnType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
				}
				return this._lazyReturnType.Value;
			}
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x06007472 RID: 29810 RVA: 0x001B2C36 File Offset: 0x001B0E36
		public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this.Map.SubstituteCustomModifiers(this.OriginalDefinition.RefCustomModifiers);
			}
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06007473 RID: 29811 RVA: 0x001B2C50 File Offset: 0x001B0E50
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				if (this._lazyParameters.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<ParameterSymbol>(ref this._lazyParameters, this.SubstituteParameters(), default(ImmutableArray<ParameterSymbol>));
				}
				return this._lazyParameters;
			}
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06007474 RID: 29812 RVA: 0x001B2C8B File Offset: 0x001B0E8B
		internal sealed override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.OriginalDefinition.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06007475 RID: 29813 RVA: 0x001B2C98 File Offset: 0x001B0E98
		public sealed override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				if (this.ConstructedFrom != this)
				{
					return ImmutableArray<MethodSymbol>.Empty;
				}
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<MethodSymbol>(ref this._lazyExplicitInterfaceImplementations, ExplicitInterfaceHelpers.SubstituteExplicitInterfaceImplementations<MethodSymbol>(this.OriginalDefinition.ExplicitInterfaceImplementations, this.Map), default(ImmutableArray<MethodSymbol>));
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x06007476 RID: 29814 RVA: 0x001B2CF2 File Offset: 0x001B0EF2
		internal sealed override OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				if (this._lazyOverriddenOrHiddenMembers == null)
				{
					Interlocked.CompareExchange<OverriddenOrHiddenMembersResult>(ref this._lazyOverriddenOrHiddenMembers, this.MakeOverriddenOrHiddenMembers(), null);
				}
				return this._lazyOverriddenOrHiddenMembers;
			}
		}

		// Token: 0x06007477 RID: 29815 RVA: 0x001B2D15 File Offset: 0x001B0F15
		internal sealed override bool CallsAreOmitted(SyntaxTree syntaxTree)
		{
			return this.OriginalDefinition.CallsAreOmitted(syntaxTree);
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x001B2D23 File Offset: 0x001B0F23
		internal sealed override TypeMap TypeSubstitution
		{
			get
			{
				return this.Map;
			}
		}

		// Token: 0x06007479 RID: 29817 RVA: 0x001B2D2C File Offset: 0x001B0F2C
		internal sealed override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			ParameterSymbol parameterSymbol;
			if (!this.OriginalDefinition.TryGetThisParameter(out parameterSymbol))
			{
				thisParameter = null;
				return false;
			}
			thisParameter = ((parameterSymbol != null) ? new ThisParameterSymbol(this) : null);
			return true;
		}

		// Token: 0x0600747A RID: 29818 RVA: 0x001B2D5C File Offset: 0x001B0F5C
		private ImmutableArray<ParameterSymbol> SubstituteParameters()
		{
			ImmutableArray<ParameterSymbol> parameters = this.OriginalDefinition.Parameters;
			int length = parameters.Length;
			if (length == 0)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance(length);
			TypeMap map = this.Map;
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				instance.Add(new SubstitutedParameterSymbol(this, map, parameterSymbol));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600747B RID: 29819 RVA: 0x001B2DC6 File Offset: 0x001B0FC6
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600747C RID: 29820 RVA: 0x001B2DCD File Offset: 0x001B0FCD
		internal override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600747D RID: 29821 RVA: 0x001B2DD4 File Offset: 0x001B0FD4
		private int ComputeHashCode()
		{
			int num = this.OriginalDefinition.GetHashCode();
			int hashCode = this._containingType.GetHashCode();
			if (hashCode == this.OriginalDefinition.ContainingType.GetHashCode() && SubstitutedMethodSymbol.<ComputeHashCode>g__wasConstructedForAnnotations|63_0(this))
			{
				return num;
			}
			num = Hash.Combine(hashCode, num);
			if (this.ConstructedFrom != this)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in this.TypeArgumentsWithAnnotations)
				{
					num = Hash.Combine<TypeSymbol>(typeWithAnnotations.Type, num);
				}
			}
			if (num == 0)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0600747E RID: 29822 RVA: 0x001B2E60 File Offset: 0x001B1060
		public sealed override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			MethodSymbol methodSymbol = obj as MethodSymbol;
			if (methodSymbol == null)
			{
				return false;
			}
			if (this.OriginalDefinition != methodSymbol.OriginalDefinition && this.OriginalDefinition != methodSymbol.OriginalDefinition)
			{
				return false;
			}
			if (!TypeSymbol.Equals(this.ContainingType, methodSymbol.ContainingType, compareKind))
			{
				return false;
			}
			bool flag = this == this.ConstructedFrom;
			bool flag2 = methodSymbol == methodSymbol.ConstructedFrom;
			if (flag || flag2)
			{
				return flag && flag2;
			}
			int arity = this.Arity;
			for (int i = 0; i < arity; i++)
			{
				if (!this.TypeArgumentsWithAnnotations[i].Equals(methodSymbol.TypeArgumentsWithAnnotations[i], compareKind))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x001B2F18 File Offset: 0x001B1118
		public override int GetHashCode()
		{
			int num = this._hashCode;
			if (num == 0)
			{
				num = this.ComputeHashCode();
				this._hashCode = num;
			}
			return num;
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x001B2F40 File Offset: 0x001B1140
		[CompilerGenerated]
		internal static bool <ComputeHashCode>g__wasConstructedForAnnotations|63_0(SubstitutedMethodSymbol method)
		{
			ImmutableArray<TypeWithAnnotations> typeArgumentsWithAnnotations = method.TypeArgumentsWithAnnotations;
			ImmutableArray<TypeParameterSymbol> typeParameters = method.OriginalDefinition.TypeParameters;
			for (int i = 0; i < typeArgumentsWithAnnotations.Length; i++)
			{
				if (!typeParameters[i].Equals(typeArgumentsWithAnnotations[i].Type, TypeCompareKind.ConsiderEverything))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001C45 RID: 7237
		private readonly NamedTypeSymbol _containingType;

		// Token: 0x04001C46 RID: 7238
		private readonly MethodSymbol _underlyingMethod;

		// Token: 0x04001C47 RID: 7239
		private readonly TypeMap _inputMap;

		// Token: 0x04001C48 RID: 7240
		private readonly MethodSymbol _constructedFrom;

		// Token: 0x04001C49 RID: 7241
		private TypeWithAnnotations.Boxed _lazyReturnType;

		// Token: 0x04001C4A RID: 7242
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001C4B RID: 7243
		private TypeMap _lazyMap;

		// Token: 0x04001C4C RID: 7244
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x04001C4D RID: 7245
		private ImmutableArray<MethodSymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001C4E RID: 7246
		private OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembers;

		// Token: 0x04001C4F RID: 7247
		private int _hashCode;
	}
}
