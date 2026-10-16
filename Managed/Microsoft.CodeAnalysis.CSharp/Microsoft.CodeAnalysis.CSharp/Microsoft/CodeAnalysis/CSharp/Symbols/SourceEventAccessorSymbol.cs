using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000533 RID: 1331
	internal abstract class SourceEventAccessorSymbol : SourceMemberMethodSymbol
	{
		// Token: 0x06006FAA RID: 28586 RVA: 0x0019A500 File Offset: 0x00198700
		public SourceEventAccessorSymbol(SourceEventSymbol @event, SyntaxReference syntaxReference, ImmutableArray<Location> locations, EventSymbol explicitlyImplementedEventOpt, string aliasQualifierOpt, bool isAdder, bool isIterator, bool isNullableAnalysisEnabled)
			: base(@event.containingType, syntaxReference, locations, isIterator)
		{
			this._event = @event;
			string text;
			ImmutableArray<MethodSymbol> immutableArray;
			if (explicitlyImplementedEventOpt == null)
			{
				text = SourceEventSymbol.GetAccessorName(@event.Name, isAdder);
				immutableArray = ImmutableArray<MethodSymbol>.Empty;
			}
			else
			{
				MethodSymbol methodSymbol = (isAdder ? explicitlyImplementedEventOpt.AddMethod : explicitlyImplementedEventOpt.RemoveMethod);
				text = ExplicitInterfaceHelpers.GetMemberName((methodSymbol != null) ? methodSymbol.Name : SourceEventSymbol.GetAccessorName(explicitlyImplementedEventOpt.Name, isAdder), explicitlyImplementedEventOpt.ContainingType, aliasQualifierOpt);
				immutableArray = ((methodSymbol == null) ? ImmutableArray<MethodSymbol>.Empty : ImmutableArray.Create<MethodSymbol>(methodSymbol));
			}
			this._explicitInterfaceImplementations = immutableArray;
			base.MakeFlags(isAdder ? MethodKind.EventAdd : MethodKind.EventRemove, @event.Modifiers, false, false, isNullableAnalysisEnabled, @event.IsExplicitInterfaceImplementation);
			this._name = this.GetOverriddenAccessorName(@event, isAdder) ?? text;
		}

		// Token: 0x17001DD5 RID: 7637
		// (get) Token: 0x06006FAB RID: 28587 RVA: 0x0019A5C5 File Offset: 0x001987C5
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001DD6 RID: 7638
		// (get) Token: 0x06006FAC RID: 28588 RVA: 0x0019A5CD File Offset: 0x001987CD
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this._event.IsExplicitInterfaceImplementation;
			}
		}

		// Token: 0x17001DD7 RID: 7639
		// (get) Token: 0x06006FAD RID: 28589 RVA: 0x0019A5DA File Offset: 0x001987DA
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return this._explicitInterfaceImplementations;
			}
		}

		// Token: 0x17001DD8 RID: 7640
		// (get) Token: 0x06006FAE RID: 28590 RVA: 0x0019A5E2 File Offset: 0x001987E2
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return !this._event.HasSkipLocalsInitAttribute && base.AreLocalsZeroed;
			}
		}

		// Token: 0x17001DD9 RID: 7641
		// (get) Token: 0x06006FAF RID: 28591 RVA: 0x0019A5F9 File Offset: 0x001987F9
		public SourceEventSymbol AssociatedEvent
		{
			get
			{
				return this._event;
			}
		}

		// Token: 0x17001DDA RID: 7642
		// (get) Token: 0x06006FB0 RID: 28592 RVA: 0x0019A601 File Offset: 0x00198801
		public sealed override Symbol AssociatedSymbol
		{
			get
			{
				return this._event;
			}
		}

		// Token: 0x06006FB1 RID: 28593 RVA: 0x0019A60C File Offset: 0x0019880C
		protected sealed override void MethodChecks(BindingDiagnosticBag diagnostics)
		{
			if (this._lazyReturnType.IsDefault)
			{
				CSharpCompilation declaringCompilation = this.DeclaringCompilation;
				if (this._event.IsWindowsRuntimeEvent)
				{
					TypeSymbol wellKnownType = declaringCompilation.GetWellKnownType(WellKnownType.System_Runtime_InteropServices_WindowsRuntime_EventRegistrationToken);
					Binder.ReportUseSite(wellKnownType, diagnostics, this.Location);
					if (this.MethodKind == MethodKind.EventAdd)
					{
						this._lazyReturnType = TypeWithAnnotations.Create(wellKnownType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
						SynthesizedAccessorValueParameterSymbol synthesizedAccessorValueParameterSymbol = new SynthesizedAccessorValueParameterSymbol(this, this._event.TypeWithAnnotations, 0);
						this._lazyParameters = ImmutableArray.Create<ParameterSymbol>(synthesizedAccessorValueParameterSymbol);
						return;
					}
					TypeSymbol specialType = declaringCompilation.GetSpecialType(SpecialType.System_Void);
					Binder.ReportUseSite(specialType, diagnostics, this.Location);
					this._lazyReturnType = TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					base.SetReturnsVoid(true);
					SynthesizedAccessorValueParameterSymbol synthesizedAccessorValueParameterSymbol2 = new SynthesizedAccessorValueParameterSymbol(this, TypeWithAnnotations.Create(wellKnownType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0);
					this._lazyParameters = ImmutableArray.Create<ParameterSymbol>(synthesizedAccessorValueParameterSymbol2);
					return;
				}
				else
				{
					TypeSymbol specialType2 = declaringCompilation.GetSpecialType(SpecialType.System_Void);
					Binder.ReportUseSite(specialType2, diagnostics, this.Location);
					this._lazyReturnType = TypeWithAnnotations.Create(specialType2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
					base.SetReturnsVoid(true);
					SynthesizedAccessorValueParameterSymbol synthesizedAccessorValueParameterSymbol3 = new SynthesizedAccessorValueParameterSymbol(this, this._event.TypeWithAnnotations, 0);
					this._lazyParameters = ImmutableArray.Create<ParameterSymbol>(synthesizedAccessorValueParameterSymbol3);
				}
			}
		}

		// Token: 0x17001DDB RID: 7643
		// (get) Token: 0x06006FB2 RID: 28594 RVA: 0x0019A74D File Offset: 0x0019894D
		public sealed override bool ReturnsVoid
		{
			get
			{
				base.LazyMethodChecks();
				return base.ReturnsVoid;
			}
		}

		// Token: 0x17001DDC RID: 7644
		// (get) Token: 0x06006FB3 RID: 28595 RVA: 0x0019A75B File Offset: 0x0019895B
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x17001DDD RID: 7645
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x0019A75E File Offset: 0x0019895E
		public sealed override TypeWithAnnotations ReturnTypeWithAnnotations
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyReturnType;
			}
		}

		// Token: 0x17001DDE RID: 7646
		// (get) Token: 0x06006FB5 RID: 28597 RVA: 0x0019A76C File Offset: 0x0019896C
		public sealed override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06006FB6 RID: 28598 RVA: 0x0019A773 File Offset: 0x00198973
		public sealed override ImmutableArray<ParameterSymbol> Parameters
		{
			get
			{
				base.LazyMethodChecks();
				return this._lazyParameters;
			}
		}

		// Token: 0x17001DE0 RID: 7648
		// (get) Token: 0x06006FB7 RID: 28599 RVA: 0x0019A781 File Offset: 0x00198981
		public sealed override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001DE1 RID: 7649
		// (get) Token: 0x06006FB8 RID: 28600 RVA: 0x0019A784 File Offset: 0x00198984
		public sealed override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x06006FB9 RID: 28601 RVA: 0x0019A78B File Offset: 0x0019898B
		public sealed override ImmutableArray<ImmutableArray<TypeWithAnnotations>> GetTypeParameterConstraintTypes()
		{
			return ImmutableArray<ImmutableArray<TypeWithAnnotations>>.Empty;
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x0019A792 File Offset: 0x00198992
		public sealed override ImmutableArray<TypeParameterConstraintKind> GetTypeParameterConstraintKinds()
		{
			return ImmutableArray<TypeParameterConstraintKind>.Empty;
		}

		// Token: 0x17001DE2 RID: 7650
		// (get) Token: 0x06006FBB RID: 28603 RVA: 0x0019A79C File Offset: 0x0019899C
		internal Location Location
		{
			get
			{
				return this.Locations[0];
			}
		}

		// Token: 0x06006FBC RID: 28604 RVA: 0x0019A7B8 File Offset: 0x001989B8
		protected string GetOverriddenAccessorName(SourceEventSymbol @event, bool isAdder)
		{
			if (this.IsOverride)
			{
				EventSymbol overriddenEvent = @event.OverriddenEvent;
				if (overriddenEvent != null)
				{
					MethodSymbol ownOrInheritedAccessor = overriddenEvent.GetOwnOrInheritedAccessor(isAdder);
					if (ownOrInheritedAccessor != null)
					{
						return ownOrInheritedAccessor.Name;
					}
					return null;
				}
			}
			return null;
		}

		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06006FBD RID: 28605 RVA: 0x0019A7EC File Offset: 0x001989EC
		internal override bool IsExpressionBodied
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B5C RID: 7004
		private readonly SourceEventSymbol _event;

		// Token: 0x04001B5D RID: 7005
		private readonly string _name;

		// Token: 0x04001B5E RID: 7006
		private readonly ImmutableArray<MethodSymbol> _explicitInterfaceImplementations;

		// Token: 0x04001B5F RID: 7007
		private ImmutableArray<ParameterSymbol> _lazyParameters;

		// Token: 0x04001B60 RID: 7008
		private TypeWithAnnotations _lazyReturnType;
	}
}
