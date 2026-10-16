using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D6 RID: 1494
	internal sealed class RetargetingMethodSymbol : WrappedMethodSymbol
	{
		// Token: 0x06007CE3 RID: 31971 RVA: 0x001C1F7B File Offset: 0x001C017B
		public RetargetingMethodSymbol(RetargetingModuleSymbol retargetingModule, MethodSymbol underlyingMethod)
		{
			this._retargetingModule = retargetingModule;
			this._underlyingMethod = underlyingMethod;
		}

		// Token: 0x170024B8 RID: 9400
		// (get) Token: 0x06007CE4 RID: 31972 RVA: 0x001C1FA7 File Offset: 0x001C01A7
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x170024B9 RID: 9401
		// (get) Token: 0x06007CE5 RID: 31973 RVA: 0x001C1FB4 File Offset: 0x001C01B4
		public RetargetingModuleSymbol RetargetingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x170024BA RID: 9402
		// (get) Token: 0x06007CE6 RID: 31974 RVA: 0x001C1FBC File Offset: 0x001C01BC
		public override MethodSymbol UnderlyingMethod
		{
			get
			{
				return this._underlyingMethod;
			}
		}

		// Token: 0x170024BB RID: 9403
		// (get) Token: 0x06007CE7 RID: 31975 RVA: 0x001C1FC4 File Offset: 0x001C01C4
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				if (this._lazyTypeParameters.IsDefault)
				{
					if (!this.IsGenericMethod)
					{
						this._lazyTypeParameters = ImmutableArray<TypeParameterSymbol>.Empty;
					}
					else
					{
						ImmutableInterlocked.InterlockedCompareExchange<TypeParameterSymbol>(ref this._lazyTypeParameters, this.RetargetingTranslator.Retarget(this._underlyingMethod.TypeParameters), default(ImmutableArray<TypeParameterSymbol>));
					}
				}
				return this._lazyTypeParameters;
			}
		}

		// Token: 0x170024BC RID: 9404
		// (get) Token: 0x06007CE8 RID: 31976 RVA: 0x001C2024 File Offset: 0x001C0224
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				if (this.IsGenericMethod)
				{
					return base.GetTypeParametersAsTypeArguments();
				}
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x06007CE9 RID: 31977 RVA: 0x001C203C File Offset: 0x001C023C
		public override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				if (this._lazyReturnType == null)
				{
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyReturnType, new TypeWithAnnotations.Boxed(this.RetargetingTranslator.Retarget(this._underlyingMethod.ReturnTypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, this.ContainingType)), null);
				}
				return this._lazyReturnType.Value;
			}
		}

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x06007CEA RID: 31978 RVA: 0x001C208B File Offset: 0x001C028B
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this.RetargetingTranslator.RetargetModifiers(this._underlyingMethod.RefCustomModifiers, ref this._lazyRefCustomModifiers);
			}
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x06007CEB RID: 31979 RVA: 0x001C20AC File Offset: 0x001C02AC
		public override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				if (this._lazyParameters.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<ParameterSymbol>(ref this._lazyParameters, this.RetargetParameters(), default(ImmutableArray<ParameterSymbol>));
				}
				return this._lazyParameters;
			}
		}

		// Token: 0x06007CEC RID: 31980 RVA: 0x001C20E8 File Offset: 0x001C02E8
		private ImmutableArray<ParameterSymbol> RetargetParameters()
		{
			ImmutableArray<ParameterSymbol> parameters = this._underlyingMethod.Parameters;
			int length = parameters.Length;
			if (length == 0)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			ParameterSymbol[] array = new ParameterSymbol[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = new RetargetingMethodParameterSymbol(this, parameters[i]);
			}
			return array.AsImmutableOrNull<ParameterSymbol>();
		}

		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x06007CED RID: 31981 RVA: 0x001C213C File Offset: 0x001C033C
		public override Symbol AssociatedSymbol
		{
			get
			{
				Symbol associatedSymbol = this._underlyingMethod.AssociatedSymbol;
				if (associatedSymbol != null)
				{
					return this.RetargetingTranslator.Retarget(associatedSymbol);
				}
				return null;
			}
		}

		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x06007CEE RID: 31982 RVA: 0x001C2166 File Offset: 0x001C0366
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingMethod.ContainingSymbol);
			}
		}

		// Token: 0x170024C2 RID: 9410
		// (get) Token: 0x06007CEF RID: 31983 RVA: 0x001C217E File Offset: 0x001C037E
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator.Retarget(this._underlyingMethod.ReturnValueMarshallingInformation);
			}
		}

		// Token: 0x06007CF0 RID: 31984 RVA: 0x001C219B File Offset: 0x001C039B
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingMethod.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x06007CF1 RID: 31985 RVA: 0x001C21B9 File Offset: 0x001C03B9
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.RetargetingTranslator.RetargetAttributes(this._underlyingMethod.GetCustomAttributesToEmit(moduleBuilder));
		}

		// Token: 0x06007CF2 RID: 31986 RVA: 0x001C21D2 File Offset: 0x001C03D2
		public override ImmutableArray<CSharpAttributeData> GetReturnTypeAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingMethod.GetReturnTypeAttributes(), ref this._lazyReturnTypeCustomAttributes);
		}

		// Token: 0x06007CF3 RID: 31987 RVA: 0x001C21F0 File Offset: 0x001C03F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			if (this._lazyUnmanagedAttributeData == UnmanagedCallersOnlyAttributeData.Uninitialized)
			{
				UnmanagedCallersOnlyAttributeData unmanagedCallersOnlyAttributeData = this._underlyingMethod.GetUnmanagedCallersOnlyAttributeData(forceComplete);
				if (unmanagedCallersOnlyAttributeData == UnmanagedCallersOnlyAttributeData.Uninitialized || unmanagedCallersOnlyAttributeData == UnmanagedCallersOnlyAttributeData.AttributePresentDataNotBound)
				{
					return unmanagedCallersOnlyAttributeData;
				}
				if (unmanagedCallersOnlyAttributeData != null && !unmanagedCallersOnlyAttributeData.CallingConventionTypes.IsEmpty)
				{
					PooledHashSet<INamedTypeSymbolInternal> instance = PooledHashSet<INamedTypeSymbolInternal>.GetInstance();
					foreach (INamedTypeSymbolInternal namedTypeSymbolInternal in unmanagedCallersOnlyAttributeData.CallingConventionTypes)
					{
						instance.Add((INamedTypeSymbolInternal)this.RetargetingTranslator.Retarget((NamedTypeSymbol)namedTypeSymbolInternal));
					}
					unmanagedCallersOnlyAttributeData = UnmanagedCallersOnlyAttributeData.Create(instance.ToImmutableHashSet<INamedTypeSymbolInternal>());
					instance.Free();
				}
				Interlocked.CompareExchange<UnmanagedCallersOnlyAttributeData>(ref this._lazyUnmanagedAttributeData, unmanagedCallersOnlyAttributeData, UnmanagedCallersOnlyAttributeData.Uninitialized);
			}
			return this._lazyUnmanagedAttributeData;
		}

		// Token: 0x170024C3 RID: 9411
		// (get) Token: 0x06007CF4 RID: 31988 RVA: 0x001C22C8 File Offset: 0x001C04C8
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x170024C4 RID: 9412
		// (get) Token: 0x06007CF5 RID: 31989 RVA: 0x001C22D5 File Offset: 0x001C04D5
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x170024C5 RID: 9413
		// (get) Token: 0x06007CF6 RID: 31990 RVA: 0x001C22DD File Offset: 0x001C04DD
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this._underlyingMethod.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x170024C6 RID: 9414
		// (get) Token: 0x06007CF7 RID: 31991 RVA: 0x001C22EC File Offset: 0x001C04EC
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<MethodSymbol>(ref this._lazyExplicitInterfaceImplementations, this.RetargetExplicitInterfaceImplementations(), default(ImmutableArray<MethodSymbol>));
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x001C2328 File Offset: 0x001C0528
		private ImmutableArray<MethodSymbol> RetargetExplicitInterfaceImplementations()
		{
			ImmutableArray<MethodSymbol> explicitInterfaceImplementations = this._underlyingMethod.ExplicitInterfaceImplementations;
			if (explicitInterfaceImplementations.IsEmpty)
			{
				return explicitInterfaceImplementations;
			}
			ArrayBuilder<MethodSymbol> instance = ArrayBuilder<MethodSymbol>.GetInstance();
			for (int i = 0; i < explicitInterfaceImplementations.Length; i++)
			{
				MethodSymbol methodSymbol = this.RetargetingTranslator.Retarget(explicitInterfaceImplementations[i], MemberSignatureComparer.RetargetedExplicitImplementationComparer);
				if (methodSymbol != null)
				{
					instance.Add(methodSymbol);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170024C7 RID: 9415
		// (get) Token: 0x06007CF9 RID: 31993 RVA: 0x001C2390 File Offset: 0x001C0590
		internal MethodSymbol ExplicitlyOverriddenClassMethod
		{
			get
			{
				bool flag;
				if (!this._underlyingMethod.RequiresExplicitOverride(out flag))
				{
					return null;
				}
				return this.RetargetingTranslator.Retarget(this._underlyingMethod.OverriddenMethod, MemberSignatureComparer.RetargetedExplicitImplementationComparer);
			}
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x001C23CC File Offset: 0x001C05CC
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (!this._lazyCachedUseSiteInfo.IsInitialized)
			{
				AssemblySymbol primaryDependency = base.PrimaryDependency;
				UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(primaryDependency);
				base.CalculateUseSiteDiagnostic(ref useSiteInfo);
				this._lazyCachedUseSiteInfo.Initialize(primaryDependency, useSiteInfo);
			}
			return this._lazyCachedUseSiteInfo.ToUseSiteInfo(base.PrimaryDependency);
		}

		// Token: 0x170024C8 RID: 9416
		// (get) Token: 0x06007CFB RID: 31995 RVA: 0x001C241C File Offset: 0x001C061C
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170024C9 RID: 9417
		// (get) Token: 0x06007CFC RID: 31996 RVA: 0x001C241F File Offset: 0x001C061F
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007CFD RID: 31997 RVA: 0x001C2422 File Offset: 0x001C0622
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06007CFE RID: 31998 RVA: 0x001C2429 File Offset: 0x001C0629
		internal override bool IsNullableAnalysisEnabled()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04001D63 RID: 7523
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D64 RID: 7524
		private readonly MethodSymbol _underlyingMethod;

		// Token: 0x04001D65 RID: 7525
		private ImmutableArray<TypeParameterSymbol> _lazyTypeParameters;

		// Token: 0x04001D66 RID: 7526
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001D67 RID: 7527
		private ImmutableArray<CustomModifier> _lazyRefCustomModifiers;

		// Token: 0x04001D68 RID: 7528
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001D69 RID: 7529
		private ImmutableArray<CSharpAttributeData> _lazyReturnTypeCustomAttributes;

		// Token: 0x04001D6A RID: 7530
		private ImmutableArray<MethodSymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001D6B RID: 7531
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001D6C RID: 7532
		private TypeWithAnnotations.Boxed _lazyReturnType;

		// Token: 0x04001D6D RID: 7533
		private UnmanagedCallersOnlyAttributeData _lazyUnmanagedAttributeData = UnmanagedCallersOnlyAttributeData.Uninitialized;
	}
}
