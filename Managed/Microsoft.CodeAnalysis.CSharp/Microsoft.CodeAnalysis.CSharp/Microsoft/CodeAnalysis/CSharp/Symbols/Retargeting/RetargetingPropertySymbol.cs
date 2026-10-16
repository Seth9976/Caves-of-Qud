using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005DD RID: 1501
	internal sealed class RetargetingPropertySymbol : WrappedPropertySymbol
	{
		// Token: 0x06007D74 RID: 32116 RVA: 0x001C3137 File Offset: 0x001C1337
		public RetargetingPropertySymbol(RetargetingModuleSymbol retargetingModule, PropertySymbol underlyingProperty)
			: base(underlyingProperty)
		{
			this._retargetingModule = retargetingModule;
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x06007D75 RID: 32117 RVA: 0x001C3152 File Offset: 0x001C1352
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x06007D76 RID: 32118 RVA: 0x001C315F File Offset: 0x001C135F
		public RetargetingModuleSymbol RetargetingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x06007D77 RID: 32119 RVA: 0x001C3168 File Offset: 0x001C1368
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				if (this._lazyType == null)
				{
					TypeWithAnnotations typeWithAnnotations = this.RetargetingTranslator.Retarget(this._underlyingProperty.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
					TypeSymbol typeSymbol;
					if (typeWithAnnotations.Type.TryAsDynamicIfNoPia(this.ContainingType, out typeSymbol))
					{
						typeWithAnnotations = TypeWithAnnotations.Create(typeSymbol, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					}
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
				}
				return this._lazyType.Value;
			}
		}

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x06007D78 RID: 32120 RVA: 0x001C31DB File Offset: 0x001C13DB
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this.RetargetingTranslator.RetargetModifiers(this._underlyingProperty.RefCustomModifiers, ref this._lazyRefCustomModifiers);
			}
		}

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x06007D79 RID: 32121 RVA: 0x001C31FC File Offset: 0x001C13FC
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

		// Token: 0x06007D7A RID: 32122 RVA: 0x001C3238 File Offset: 0x001C1438
		private ImmutableArray<ParameterSymbol> RetargetParameters()
		{
			ImmutableArray<ParameterSymbol> parameters = this._underlyingProperty.Parameters;
			int length = parameters.Length;
			if (length == 0)
			{
				return ImmutableArray<ParameterSymbol>.Empty;
			}
			ParameterSymbol[] array = new ParameterSymbol[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = new RetargetingPropertyParameterSymbol(this, parameters[i]);
			}
			return array.AsImmutableOrNull<ParameterSymbol>();
		}

		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x06007D7B RID: 32123 RVA: 0x001C328C File Offset: 0x001C148C
		public override MethodSymbol GetMethod
		{
			get
			{
				if (this._underlyingProperty.GetMethod != null)
				{
					return this.RetargetingTranslator.Retarget(this._underlyingProperty.GetMethod);
				}
				return null;
			}
		}

		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x06007D7C RID: 32124 RVA: 0x001C32B3 File Offset: 0x001C14B3
		public override MethodSymbol SetMethod
		{
			get
			{
				if (this._underlyingProperty.SetMethod != null)
				{
					return this.RetargetingTranslator.Retarget(this._underlyingProperty.SetMethod);
				}
				return null;
			}
		}

		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x06007D7D RID: 32125 RVA: 0x001C32DA File Offset: 0x001C14DA
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this._underlyingProperty.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x06007D7E RID: 32126 RVA: 0x001C32E8 File Offset: 0x001C14E8
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<PropertySymbol>(ref this._lazyExplicitInterfaceImplementations, this.RetargetExplicitInterfaceImplementations(), default(ImmutableArray<PropertySymbol>));
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x06007D7F RID: 32127 RVA: 0x001C3324 File Offset: 0x001C1524
		private ImmutableArray<PropertySymbol> RetargetExplicitInterfaceImplementations()
		{
			ImmutableArray<PropertySymbol> explicitInterfaceImplementations = this._underlyingProperty.ExplicitInterfaceImplementations;
			if (explicitInterfaceImplementations.IsEmpty)
			{
				return explicitInterfaceImplementations;
			}
			ArrayBuilder<PropertySymbol> instance = ArrayBuilder<PropertySymbol>.GetInstance();
			for (int i = 0; i < explicitInterfaceImplementations.Length; i++)
			{
				PropertySymbol propertySymbol = this.RetargetingTranslator.Retarget(explicitInterfaceImplementations[i], MemberSignatureComparer.RetargetedExplicitImplementationComparer);
				if (propertySymbol != null)
				{
					instance.Add(propertySymbol);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x06007D80 RID: 32128 RVA: 0x001C3389 File Offset: 0x001C1589
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingProperty.ContainingSymbol);
			}
		}

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x06007D81 RID: 32129 RVA: 0x001C33A1 File Offset: 0x001C15A1
		public override AssemblySymbol ContainingAssembly
		{
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x1700250F RID: 9487
		// (get) Token: 0x06007D82 RID: 32130 RVA: 0x001C33AE File Offset: 0x001C15AE
		internal override ModuleSymbol ContainingModule
		{
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x06007D83 RID: 32131 RVA: 0x001C33B6 File Offset: 0x001C15B6
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.RetargetingTranslator.GetRetargetedAttributes(this._underlyingProperty.GetAttributes(), ref this._lazyCustomAttributes);
		}

		// Token: 0x06007D84 RID: 32132 RVA: 0x001C33D4 File Offset: 0x001C15D4
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.RetargetingTranslator.RetargetAttributes(this._underlyingProperty.GetCustomAttributesToEmit(moduleBuilder));
		}

		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x06007D85 RID: 32133 RVA: 0x001C33ED File Offset: 0x001C15ED
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return this._underlyingProperty.MustCallMethodsDirectly;
			}
		}

		// Token: 0x06007D86 RID: 32134 RVA: 0x001C33FC File Offset: 0x001C15FC
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			AssemblySymbol primaryDependency = base.PrimaryDependency;
			if (!this._lazyCachedUseSiteInfo.IsInitialized)
			{
				UseSiteInfo<AssemblySymbol> useSiteInfo = new UseSiteInfo<AssemblySymbol>(primaryDependency);
				base.CalculateUseSiteDiagnostic(ref useSiteInfo);
				this._lazyCachedUseSiteInfo.Initialize(primaryDependency, useSiteInfo);
			}
			return this._lazyCachedUseSiteInfo.ToUseSiteInfo(primaryDependency);
		}

		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x06007D87 RID: 32135 RVA: 0x001C3447 File Offset: 0x001C1647
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001D89 RID: 7561
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D8A RID: 7562
		private ImmutableArray<PropertySymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001D8B RID: 7563
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001D8C RID: 7564
		private ImmutableArray<CustomModifier> _lazyRefCustomModifiers;

		// Token: 0x04001D8D RID: 7565
		private ImmutableArray<CSharpAttributeData> _lazyCustomAttributes;

		// Token: 0x04001D8E RID: 7566
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;

		// Token: 0x04001D8F RID: 7567
		private TypeWithAnnotations.Boxed _lazyType;
	}
}
