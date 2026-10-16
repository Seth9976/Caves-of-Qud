using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting
{
	// Token: 0x020005D4 RID: 1492
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class RetargetingEventSymbol : WrappedEventSymbol
	{
		// Token: 0x06007CC4 RID: 31940 RVA: 0x001C1C11 File Offset: 0x001BFE11
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public RetargetingEventSymbol(RetargetingModuleSymbol retargetingModule, EventSymbol underlyingEvent)
			: base(underlyingEvent)
		{
			this._retargetingModule = retargetingModule;
		}

		// Token: 0x170024A3 RID: 9379
		// (get) Token: 0x06007CC5 RID: 31941 RVA: 0x001C1C2C File Offset: 0x001BFE2C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private RetargetingModuleSymbol.RetargetingSymbolTranslator RetargetingTranslator
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._retargetingModule.RetargetingTranslator;
			}
		}

		// Token: 0x170024A4 RID: 9380
		// (get) Token: 0x06007CC6 RID: 31942 RVA: 0x001C1C39 File Offset: 0x001BFE39
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingEvent.TypeWithAnnotations, RetargetOptions.RetargetPrimitiveTypesByTypeCode, null);
			}
		}

		// Token: 0x170024A5 RID: 9381
		// (get) Token: 0x06007CC7 RID: 31943 RVA: 0x001C1C53 File Offset: 0x001BFE53
		public override MethodSymbol AddMethod
		{
			get
			{
				if (this._underlyingEvent.AddMethod != null)
				{
					return this.RetargetingTranslator.Retarget(this._underlyingEvent.AddMethod);
				}
				return null;
			}
		}

		// Token: 0x170024A6 RID: 9382
		// (get) Token: 0x06007CC8 RID: 31944 RVA: 0x001C1C7A File Offset: 0x001BFE7A
		public override MethodSymbol RemoveMethod
		{
			get
			{
				if (this._underlyingEvent.RemoveMethod != null)
				{
					return this.RetargetingTranslator.Retarget(this._underlyingEvent.RemoveMethod);
				}
				return null;
			}
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x06007CC9 RID: 31945 RVA: 0x001C1CA1 File Offset: 0x001BFEA1
		internal override FieldSymbol AssociatedField
		{
			get
			{
				if (this._underlyingEvent.AssociatedField != null)
				{
					return this.RetargetingTranslator.Retarget(this._underlyingEvent.AssociatedField);
				}
				return null;
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x06007CCA RID: 31946 RVA: 0x001C1CC8 File Offset: 0x001BFEC8
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this._underlyingEvent.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x06007CCB RID: 31947 RVA: 0x001C1CD8 File Offset: 0x001BFED8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<EventSymbol>(ref this._lazyExplicitInterfaceImplementations, this.RetargetExplicitInterfaceImplementations(), default(ImmutableArray<EventSymbol>));
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x06007CCC RID: 31948 RVA: 0x001C1D14 File Offset: 0x001BFF14
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<EventSymbol> RetargetExplicitInterfaceImplementations()
		{
			ImmutableArray<EventSymbol> explicitInterfaceImplementations = this._underlyingEvent.ExplicitInterfaceImplementations;
			if (explicitInterfaceImplementations.IsEmpty)
			{
				return explicitInterfaceImplementations;
			}
			ArrayBuilder<EventSymbol> instance = ArrayBuilder<EventSymbol>.GetInstance();
			for (int i = 0; i < explicitInterfaceImplementations.Length; i++)
			{
				EventSymbol eventSymbol = this.RetargetingTranslator.Retarget(explicitInterfaceImplementations[i]);
				if (eventSymbol != null)
				{
					instance.Add(eventSymbol);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x06007CCD RID: 31949 RVA: 0x001C1D74 File Offset: 0x001BFF74
		public override Symbol ContainingSymbol
		{
			get
			{
				return this.RetargetingTranslator.Retarget(this._underlyingEvent.ContainingSymbol);
			}
		}

		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x06007CCE RID: 31950 RVA: 0x001C1D8C File Offset: 0x001BFF8C
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override AssemblySymbol ContainingAssembly
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._retargetingModule.ContainingAssembly;
			}
		}

		// Token: 0x170024AC RID: 9388
		// (get) Token: 0x06007CCF RID: 31951 RVA: 0x001C1D99 File Offset: 0x001BFF99
		[global::System.Runtime.CompilerServices.Nullable(1)]
		internal override ModuleSymbol ContainingModule
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._retargetingModule;
			}
		}

		// Token: 0x06007CD0 RID: 31952 RVA: 0x001C1DA1 File Offset: 0x001BFFA1
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._underlyingEvent.GetAttributes();
		}

		// Token: 0x06007CD1 RID: 31953 RVA: 0x001C1DAE File Offset: 0x001BFFAE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override IEnumerable<CSharpAttributeData> GetCustomAttributesToEmit(PEModuleBuilder moduleBuilder)
		{
			return this.RetargetingTranslator.RetargetAttributes(this._underlyingEvent.GetCustomAttributesToEmit(moduleBuilder));
		}

		// Token: 0x170024AD RID: 9389
		// (get) Token: 0x06007CD2 RID: 31954 RVA: 0x001C1DC7 File Offset: 0x001BFFC7
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return this._underlyingEvent.MustCallMethodsDirectly;
			}
		}

		// Token: 0x06007CD3 RID: 31955 RVA: 0x001C1DD4 File Offset: 0x001BFFD4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
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

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x06007CD4 RID: 31956 RVA: 0x001C1E24 File Offset: 0x001C0024
		internal sealed override CSharpCompilation DeclaringCompilation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001D5D RID: 7517
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly RetargetingModuleSymbol _retargetingModule;

		// Token: 0x04001D5E RID: 7518
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<EventSymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001D5F RID: 7519
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private CachedUseSiteInfo<AssemblySymbol> _lazyCachedUseSiteInfo = CachedUseSiteInfo<AssemblySymbol>.Uninitialized;
	}
}
