using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004AD RID: 1197
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class EventSymbol : Symbol, IEventDefinition, ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition
	{
		// Token: 0x06006353 RID: 25427 RVA: 0x0017CFB6 File Offset: 0x0017B1B6
		IEnumerable<IMethodReference> IEventDefinition.GetAccessors(EmitContext context)
		{
			MethodSymbol addMethod = this.AdaptedEventSymbol.AddMethod;
			MethodSymbol methodSymbol = ((addMethod != null) ? addMethod.GetCciAdapter() : null);
			if (methodSymbol.ShouldInclude(context))
			{
				yield return methodSymbol;
			}
			MethodSymbol removeMethod = this.AdaptedEventSymbol.RemoveMethod;
			MethodSymbol methodSymbol2 = ((removeMethod != null) ? removeMethod.GetCciAdapter() : null);
			if (methodSymbol2.ShouldInclude(context))
			{
				yield return methodSymbol2;
			}
			yield break;
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x06006354 RID: 25428 RVA: 0x0017CFCD File Offset: 0x0017B1CD
		IMethodReference IEventDefinition.Adder
		{
			get
			{
				MethodSymbol addMethod = this.AdaptedEventSymbol.AddMethod;
				if (addMethod == null)
				{
					return null;
				}
				return addMethod.GetCciAdapter();
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06006355 RID: 25429 RVA: 0x0017CFE5 File Offset: 0x0017B1E5
		IMethodReference IEventDefinition.Remover
		{
			get
			{
				MethodSymbol removeMethod = this.AdaptedEventSymbol.RemoveMethod;
				if (removeMethod == null)
				{
					return null;
				}
				return removeMethod.GetCciAdapter();
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06006356 RID: 25430 RVA: 0x0017CFFD File Offset: 0x0017B1FD
		bool IEventDefinition.IsRuntimeSpecial
		{
			get
			{
				return this.AdaptedEventSymbol.HasRuntimeSpecialName;
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06006357 RID: 25431 RVA: 0x0017D00A File Offset: 0x0017B20A
		bool IEventDefinition.IsSpecialName
		{
			get
			{
				return this.AdaptedEventSymbol.HasSpecialName;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06006358 RID: 25432 RVA: 0x0017D017 File Offset: 0x0017B217
		[global::System.Runtime.CompilerServices.Nullable(2)]
		IMethodReference IEventDefinition.Caller
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x0017D01A File Offset: 0x0017B21A
		ITypeReference IEventDefinition.GetType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.AdaptedEventSymbol.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x0600635A RID: 25434 RVA: 0x0017D048 File Offset: 0x0017B248
		ITypeDefinition ITypeDefinitionMember.ContainingTypeDefinition
		{
			get
			{
				return this.AdaptedEventSymbol.ContainingType.GetCciAdapter();
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x0600635B RID: 25435 RVA: 0x0017D05A File Offset: 0x0017B25A
		TypeMemberVisibility ITypeDefinitionMember.Visibility
		{
			get
			{
				return PEModuleBuilder.MemberVisibility(this.AdaptedEventSymbol);
			}
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0017D067 File Offset: 0x0017B267
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return this.AdaptedEventSymbol.ContainingType.GetCciAdapter();
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x0017D079 File Offset: 0x0017B279
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x0017D082 File Offset: 0x0017B282
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x0600635F RID: 25439 RVA: 0x0017D085 File Offset: 0x0017B285
		string INamedEntity.Name
		{
			get
			{
				return this.AdaptedEventSymbol.MetadataName;
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x06006360 RID: 25440 RVA: 0x0017D092 File Offset: 0x0017B292
		internal EventSymbol AdaptedEventSymbol
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x0017D095 File Offset: 0x0017B295
		internal new EventSymbol GetCciAdapter()
		{
			return this;
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x06006362 RID: 25442 RVA: 0x0017D098 File Offset: 0x0017B298
		internal virtual bool HasRuntimeSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x0017D09B File Offset: 0x0017B29B
		internal EventSymbol()
		{
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x06006364 RID: 25444 RVA: 0x0017D0A3 File Offset: 0x0017B2A3
		public new virtual EventSymbol OriginalDefinition
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x06006365 RID: 25445 RVA: 0x0017D0A6 File Offset: 0x0017B2A6
		protected sealed override Symbol OriginalSymbolDefinition
		{
			get
			{
				return this.OriginalDefinition;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x06006366 RID: 25446
		public abstract TypeWithAnnotations TypeWithAnnotations { get; }

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x06006367 RID: 25447 RVA: 0x0017D0B0 File Offset: 0x0017B2B0
		public TypeSymbol Type
		{
			get
			{
				return this.TypeWithAnnotations.Type;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x06006368 RID: 25448
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract MethodSymbol AddMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x06006369 RID: 25449
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract MethodSymbol RemoveMethod
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x0600636A RID: 25450 RVA: 0x0017D0CB File Offset: 0x0017B2CB
		internal bool HasAssociatedField
		{
			get
			{
				return this.AssociatedField != null;
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x0600636B RID: 25451 RVA: 0x0017D0D6 File Offset: 0x0017B2D6
		public virtual bool RequiresInstanceReceiver
		{
			get
			{
				return !this.IsStatic;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x0600636C RID: 25452
		public abstract bool IsWindowsRuntimeEvent { get; }

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x0600636D RID: 25453 RVA: 0x0017D0E1 File Offset: 0x0017B2E1
		internal virtual bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x0600636E RID: 25454
		internal abstract bool HasSpecialName { get; }

		// Token: 0x0600636F RID: 25455 RVA: 0x0017D0E4 File Offset: 0x0017B2E4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<CSharpAttributeData> GetFieldAttributes()
		{
			if (this.AssociatedField != null)
			{
				return this.AssociatedField.GetAttributes();
			}
			return ImmutableArray<CSharpAttributeData>.Empty;
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x06006370 RID: 25456 RVA: 0x0017D0FF File Offset: 0x0017B2FF
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal virtual FieldSymbol AssociatedField
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x06006371 RID: 25457 RVA: 0x0017D102 File Offset: 0x0017B302
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public EventSymbol OverriddenEvent
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				if (!this.IsOverride)
				{
					return null;
				}
				if (base.IsDefinition)
				{
					return (EventSymbol)this.OverriddenOrHiddenMembers.GetOverriddenMember();
				}
				return (EventSymbol)OverriddenOrHiddenMembersResult.GetOverriddenMember(this, this.OriginalDefinition.OverriddenEvent);
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06006372 RID: 25458 RVA: 0x0017D13D File Offset: 0x0017B33D
		internal virtual OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				return this.MakeOverriddenOrHiddenMembers();
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06006373 RID: 25459 RVA: 0x0017D148 File Offset: 0x0017B348
		internal bool HidesBaseEventsByName
		{
			get
			{
				MethodSymbol methodSymbol = this.AddMethod ?? this.RemoveMethod;
				return methodSymbol != null && methodSymbol.HidesBaseMethodsByName;
			}
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x0017D174 File Offset: 0x0017B374
		internal EventSymbol GetLeastOverriddenEvent([global::System.Runtime.CompilerServices.Nullable(2)] NamedTypeSymbol accessingTypeOpt)
		{
			accessingTypeOpt = ((accessingTypeOpt != null) ? accessingTypeOpt.OriginalDefinition : null);
			EventSymbol eventSymbol = this;
			while (eventSymbol.IsOverride && !eventSymbol.HidesBaseEventsByName)
			{
				EventSymbol overriddenEvent = eventSymbol.OverriddenEvent;
				CompoundUseSiteInfo<AssemblySymbol> discarded = CompoundUseSiteInfo<AssemblySymbol>.Discarded;
				if (overriddenEvent == null || (accessingTypeOpt != null && !AccessCheck.IsSymbolAccessible(overriddenEvent, accessingTypeOpt, ref discarded, null)))
				{
					break;
				}
				eventSymbol = overriddenEvent;
			}
			return eventSymbol;
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06006375 RID: 25461 RVA: 0x0017D1C5 File Offset: 0x0017B3C5
		internal virtual bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.ExplicitInterfaceImplementations.Any<EventSymbol>();
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06006376 RID: 25462
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x06006377 RID: 25463 RVA: 0x0017D1D2 File Offset: 0x0017B3D2
		public sealed override SymbolKind Kind
		{
			get
			{
				return SymbolKind.Event;
			}
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x0017D1D5 File Offset: 0x0017B3D5
		internal override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TArgument, [global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TArgument, TResult> visitor, TArgument argument)
		{
			return visitor.VisitEvent(this, argument);
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x0017D1DF File Offset: 0x0017B3DF
		public override void Accept(CSharpSymbolVisitor visitor)
		{
			visitor.VisitEvent(this);
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x0017D1E8 File Offset: 0x0017B3E8
		public override TResult Accept<[global::System.Runtime.CompilerServices.Nullable(0)] TResult>(CSharpSymbolVisitor<TResult> visitor)
		{
			return visitor.VisitEvent(this);
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x0017D1F1 File Offset: 0x0017B3F1
		internal EventSymbol AsMember(NamedTypeSymbol newOwner)
		{
			if (!newOwner.IsDefinition)
			{
				return new SubstitutedEventSymbol(newOwner as SubstitutedNamedTypeSymbol, this);
			}
			return this;
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x0600637C RID: 25468
		internal abstract bool MustCallMethodsDirectly { get; }

		// Token: 0x0600637D RID: 25469 RVA: 0x0017D209 File Offset: 0x0017B409
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			if (base.IsDefinition)
			{
				return new UseSiteInfo<AssemblySymbol>(base.PrimaryDependency);
			}
			return this.OriginalDefinition.GetUseSiteInfo();
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x0017D22C File Offset: 0x0017B42C
		internal bool CalculateUseSiteDiagnostic([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref UseSiteInfo<AssemblySymbol> result)
		{
			if (base.DeriveUseSiteInfoFromType(ref result, this.TypeWithAnnotations, Symbol.AllowedRequiredModifierType.None))
			{
				return true;
			}
			if (this.ContainingModule.HasUnifiedReferences)
			{
				HashSet<TypeSymbol> hashSet = null;
				DiagnosticInfo diagnosticInfo = result.DiagnosticInfo;
				if (this.TypeWithAnnotations.GetUnificationUseSiteDiagnosticRecursive(ref diagnosticInfo, this, ref hashSet))
				{
					result = result.AdjustDiagnosticInfo(diagnosticInfo);
					return true;
				}
				result = result.AdjustDiagnosticInfo(diagnosticInfo);
			}
			return false;
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x0017D293 File Offset: 0x0017B493
		protected override int HighestPriorityUseSiteError
		{
			get
			{
				return 570;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06006380 RID: 25472 RVA: 0x0017D29C File Offset: 0x0017B49C
		public sealed override bool HasUnsupportedMetadata
		{
			get
			{
				DiagnosticInfo diagnosticInfo = this.GetUseSiteInfo().DiagnosticInfo;
				return diagnosticInfo != null && diagnosticInfo.Code == 570;
			}
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x0017D2C7 File Offset: 0x0017B4C7
		protected sealed override ISymbol CreateISymbol()
		{
			return new EventSymbol(this);
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x0017D2D0 File Offset: 0x0017B4D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			EventSymbol eventSymbol = obj as EventSymbol;
			return eventSymbol != null && (this == eventSymbol || (TypeSymbol.Equals(this.ContainingType, eventSymbol.ContainingType, compareKind) && this.OriginalDefinition == eventSymbol.OriginalDefinition));
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x0017D314 File Offset: 0x0017B514
		public override int GetHashCode()
		{
			int num = 1;
			num = Hash.Combine<NamedTypeSymbol>(this.ContainingType, num);
			return Hash.Combine<string>(this.Name, num);
		}
	}
}
