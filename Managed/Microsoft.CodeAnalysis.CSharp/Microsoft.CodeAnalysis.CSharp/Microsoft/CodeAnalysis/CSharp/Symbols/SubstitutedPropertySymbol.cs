using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000566 RID: 1382
	internal sealed class SubstitutedPropertySymbol : WrappedPropertySymbol
	{
		// Token: 0x060074B6 RID: 29878 RVA: 0x001B37D3 File Offset: 0x001B19D3
		internal SubstitutedPropertySymbol(SubstitutedNamedTypeSymbol containingType, PropertySymbol originalDefinition)
			: base(originalDefinition)
		{
			this._containingType = containingType;
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x001B37E4 File Offset: 0x001B19E4
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				if (this._lazyType == null)
				{
					TypeWithAnnotations typeWithAnnotations = this._containingType.TypeSubstitution.SubstituteType(this.OriginalDefinition.TypeWithAnnotations);
					Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
				}
				return this._lazyType.Value;
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x060074B8 RID: 29880 RVA: 0x001B3833 File Offset: 0x001B1A33
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x001B383B File Offset: 0x001B1A3B
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x060074BA RID: 29882 RVA: 0x001B3843 File Offset: 0x001B1A43
		public override PropertySymbol OriginalDefinition
		{
			get
			{
				return this._underlyingProperty;
			}
		}

		// Token: 0x060074BB RID: 29883 RVA: 0x001B384B File Offset: 0x001B1A4B
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.OriginalDefinition.GetAttributes();
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x060074BC RID: 29884 RVA: 0x001B3858 File Offset: 0x001B1A58
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return this._containingType.TypeSubstitution.SubstituteCustomModifiers(this.OriginalDefinition.RefCustomModifiers);
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x060074BD RID: 29885 RVA: 0x001B3878 File Offset: 0x001B1A78
		public override ImmutableArray<ParameterSymbol> Parameters
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

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x060074BE RID: 29886 RVA: 0x001B38B4 File Offset: 0x001B1AB4
		public override MethodSymbol GetMethod
		{
			get
			{
				MethodSymbol getMethod = this.OriginalDefinition.GetMethod;
				if (getMethod != null)
				{
					return getMethod.AsMember(this._containingType);
				}
				return null;
			}
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x060074BF RID: 29887 RVA: 0x001B38E0 File Offset: 0x001B1AE0
		public override MethodSymbol SetMethod
		{
			get
			{
				MethodSymbol setMethod = this.OriginalDefinition.SetMethod;
				if (setMethod != null)
				{
					return setMethod.AsMember(this._containingType);
				}
				return null;
			}
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x060074C0 RID: 29888 RVA: 0x001B390A File Offset: 0x001B1B0A
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.OriginalDefinition.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x060074C1 RID: 29889 RVA: 0x001B3918 File Offset: 0x001B1B18
		public override ImmutableArray<PropertySymbol> ExplicitInterfaceImplementations
		{
			get
			{
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<PropertySymbol>(ref this._lazyExplicitInterfaceImplementations, ExplicitInterfaceHelpers.SubstituteExplicitInterfaceImplementations<PropertySymbol>(this.OriginalDefinition.ExplicitInterfaceImplementations, this._containingType.TypeSubstitution), default(ImmutableArray<PropertySymbol>));
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x060074C2 RID: 29890 RVA: 0x001B3968 File Offset: 0x001B1B68
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return this.OriginalDefinition.MustCallMethodsDirectly;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x060074C3 RID: 29891 RVA: 0x001B3975 File Offset: 0x001B1B75
		internal override OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
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

		// Token: 0x060074C4 RID: 29892 RVA: 0x001B3998 File Offset: 0x001B1B98
		private ImmutableArray<ParameterSymbol> SubstituteParameters()
		{
			ImmutableArray<ParameterSymbol> parameters = this.OriginalDefinition.Parameters;
			if (parameters.IsEmpty)
			{
				return parameters;
			}
			int length = parameters.Length;
			ParameterSymbol[] array = new ParameterSymbol[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = new SubstitutedParameterSymbol(this, this._containingType.TypeSubstitution, parameters[i]);
			}
			return array.AsImmutableOrNull<ParameterSymbol>();
		}

		// Token: 0x04001C5A RID: 7258
		private readonly SubstitutedNamedTypeSymbol _containingType;

		// Token: 0x04001C5B RID: 7259
		private TypeWithAnnotations.Boxed _lazyType;

		// Token: 0x04001C5C RID: 7260
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001C5D RID: 7261
		private ImmutableArray<PropertySymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001C5E RID: 7262
		private OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembers;
	}
}
