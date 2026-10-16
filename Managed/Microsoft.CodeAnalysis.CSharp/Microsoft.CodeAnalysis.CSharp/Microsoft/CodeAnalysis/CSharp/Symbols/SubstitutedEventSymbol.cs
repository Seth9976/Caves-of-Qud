using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000561 RID: 1377
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SubstitutedEventSymbol : WrappedEventSymbol
	{
		// Token: 0x06007445 RID: 29765 RVA: 0x001B269D File Offset: 0x001B089D
		internal SubstitutedEventSymbol(SubstitutedNamedTypeSymbol containingType, EventSymbol originalDefinition)
			: base(originalDefinition)
		{
			this._containingType = containingType;
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06007446 RID: 29766 RVA: 0x001B26B0 File Offset: 0x001B08B0
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

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06007447 RID: 29767 RVA: 0x001B26FF File Offset: 0x001B08FF
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06007448 RID: 29768 RVA: 0x001B2707 File Offset: 0x001B0907
		public override EventSymbol OriginalDefinition
		{
			get
			{
				return this._underlyingEvent;
			}
		}

		// Token: 0x06007449 RID: 29769 RVA: 0x001B270F File Offset: 0x001B090F
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.OriginalDefinition.GetAttributes();
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x0600744A RID: 29770 RVA: 0x001B271C File Offset: 0x001B091C
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override MethodSymbol AddMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				MethodSymbol addMethod = this.OriginalDefinition.AddMethod;
				if (addMethod != null)
				{
					return addMethod.AsMember(this._containingType);
				}
				return null;
			}
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x0600744B RID: 29771 RVA: 0x001B2748 File Offset: 0x001B0948
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override MethodSymbol RemoveMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				MethodSymbol removeMethod = this.OriginalDefinition.RemoveMethod;
				if (removeMethod != null)
				{
					return removeMethod.AsMember(this._containingType);
				}
				return null;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x0600744C RID: 29772 RVA: 0x001B2774 File Offset: 0x001B0974
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override FieldSymbol AssociatedField
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				FieldSymbol associatedField = this.OriginalDefinition.AssociatedField;
				if (associatedField != null)
				{
					return associatedField.AsMember(this._containingType);
				}
				return null;
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x0600744D RID: 29773 RVA: 0x001B279E File Offset: 0x001B099E
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.OriginalDefinition.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x0600744E RID: 29774 RVA: 0x001B27AC File Offset: 0x001B09AC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (this._lazyExplicitInterfaceImplementations.IsDefault)
				{
					ImmutableInterlocked.InterlockedCompareExchange<EventSymbol>(ref this._lazyExplicitInterfaceImplementations, ExplicitInterfaceHelpers.SubstituteExplicitInterfaceImplementations<EventSymbol>(this.OriginalDefinition.ExplicitInterfaceImplementations, this._containingType.TypeSubstitution), default(ImmutableArray<EventSymbol>));
				}
				return this._lazyExplicitInterfaceImplementations;
			}
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x0600744F RID: 29775 RVA: 0x001B27FC File Offset: 0x001B09FC
		internal override bool MustCallMethodsDirectly
		{
			get
			{
				return this.OriginalDefinition.MustCallMethodsDirectly;
			}
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x06007450 RID: 29776 RVA: 0x001B2809 File Offset: 0x001B0A09
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

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x06007451 RID: 29777 RVA: 0x001B282C File Offset: 0x001B0A2C
		public override bool IsWindowsRuntimeEvent
		{
			get
			{
				return this.OriginalDefinition.IsWindowsRuntimeEvent;
			}
		}

		// Token: 0x04001C3F RID: 7231
		private readonly SubstitutedNamedTypeSymbol _containingType;

		// Token: 0x04001C40 RID: 7232
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private TypeWithAnnotations.Boxed _lazyType;

		// Token: 0x04001C41 RID: 7233
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<EventSymbol> _lazyExplicitInterfaceImplementations;

		// Token: 0x04001C42 RID: 7234
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembers;
	}
}
