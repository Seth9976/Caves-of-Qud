using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200054A RID: 1354
	internal abstract class SourceParameterSymbol : SourceParameterSymbolBase
	{
		// Token: 0x060072B1 RID: 29361 RVA: 0x001AD0A0 File Offset: 0x001AB2A0
		public static SourceParameterSymbol Create(Binder context, Symbol owner, TypeWithAnnotations parameterType, ParameterSyntax syntax, RefKind refKind, SyntaxToken identifier, int ordinal, bool isParams, bool isExtensionMethodThis, bool addRefReadOnlyModifier, BindingDiagnosticBag declarationDiagnostics)
		{
			string valueText = identifier.ValueText;
			ImmutableArray<Location> immutableArray = ImmutableArray.Create<Location>(new SourceLocation(in identifier));
			if (isParams)
			{
				Binder.ReportUseSiteDiagnosticForSynthesizedAttribute(context.Compilation, WellKnownMember.System_ParamArrayAttribute__ctor, declarationDiagnostics, identifier.Parent.GetLocation(), null);
			}
			ImmutableArray<CustomModifier> immutableArray2 = ParameterHelpers.ConditionallyCreateInModifiers(refKind, addRefReadOnlyModifier, context, declarationDiagnostics, syntax);
			if (!immutableArray2.IsDefaultOrEmpty)
			{
				return new SourceComplexParameterSymbolWithCustomModifiersPrecedingByRef(owner, ordinal, parameterType, refKind, immutableArray2, valueText, immutableArray, syntax.GetReference(), isParams, isExtensionMethodThis);
			}
			if (!isParams && !isExtensionMethodThis && syntax.Default == null && syntax.AttributeLists.Count == 0 && !owner.IsPartialMethod())
			{
				return new SourceSimpleParameterSymbol(owner, parameterType, ordinal, refKind, valueText, immutableArray);
			}
			return new SourceComplexParameterSymbol(owner, ordinal, parameterType, refKind, valueText, immutableArray, syntax.GetReference(), isParams, isExtensionMethodThis);
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x001AD15F File Offset: 0x001AB35F
		protected SourceParameterSymbol(Symbol owner, TypeWithAnnotations parameterType, int ordinal, RefKind refKind, string name, ImmutableArray<Location> locations)
			: base(owner, ordinal)
		{
			this.parameterType = parameterType;
			this._refKind = refKind;
			this._name = name;
			this._locations = locations;
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x001AD188 File Offset: 0x001AB388
		internal override ParameterSymbol WithCustomModifiersAndParams(TypeSymbol newType, ImmutableArray<CustomModifier> newCustomModifiers, ImmutableArray<CustomModifier> newRefCustomModifiers, bool newIsParams)
		{
			return this.WithCustomModifiersAndParamsCore(newType, newCustomModifiers, newRefCustomModifiers, newIsParams);
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x001AD198 File Offset: 0x001AB398
		internal SourceParameterSymbol WithCustomModifiersAndParamsCore(TypeSymbol newType, ImmutableArray<CustomModifier> newCustomModifiers, ImmutableArray<CustomModifier> newRefCustomModifiers, bool newIsParams)
		{
			newType = CustomModifierUtils.CopyTypeCustomModifiers(newType, base.Type, this.ContainingAssembly);
			TypeWithAnnotations typeWithAnnotations = this.TypeWithAnnotations.WithTypeAndModifiers(newType, newCustomModifiers);
			if (newRefCustomModifiers.IsEmpty)
			{
				return new SourceComplexParameterSymbol(this.ContainingSymbol, this.Ordinal, typeWithAnnotations, this._refKind, this._name, this._locations, this.SyntaxReference, newIsParams, this.IsExtensionMethodThis);
			}
			return new SourceComplexParameterSymbolWithCustomModifiersPrecedingByRef(this.ContainingSymbol, this.Ordinal, typeWithAnnotations, this._refKind, newRefCustomModifiers, this._name, this._locations, this.SyntaxReference, newIsParams, this.IsExtensionMethodThis);
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x060072B5 RID: 29365 RVA: 0x001AD239 File Offset: 0x001AB439
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x001AD23C File Offset: 0x001AB43C
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this.state.HasComplete(part);
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x001AD24A File Offset: 0x001AB44A
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			this.state.DefaultForceComplete(this, cancellationToken);
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x060072B8 RID: 29368
		internal abstract bool HasOptionalAttribute { get; }

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x060072B9 RID: 29369
		internal abstract bool HasDefaultArgumentSyntax { get; }

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x060072BA RID: 29370
		internal abstract SyntaxList<AttributeListSyntax> AttributeDeclarationList { get; }

		// Token: 0x060072BB RID: 29371
		internal abstract CustomAttributesBag<CSharpAttributeData> GetAttributesBag();

		// Token: 0x060072BC RID: 29372 RVA: 0x001AD259 File Offset: 0x001AB459
		public sealed override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x001AD266 File Offset: 0x001AB466
		internal override void AddDeclarationDiagnostics(BindingDiagnosticBag diagnostics)
		{
			this.ContainingSymbol.AddDeclarationDiagnostics(diagnostics);
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x060072BE RID: 29374
		internal abstract SyntaxReference SyntaxReference { get; }

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x060072BF RID: 29375
		internal abstract bool IsExtensionMethodThis { get; }

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x060072C0 RID: 29376 RVA: 0x001AD274 File Offset: 0x001AB474
		public sealed override RefKind RefKind
		{
			get
			{
				return this._refKind;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x060072C1 RID: 29377 RVA: 0x001AD27C File Offset: 0x001AB47C
		public sealed override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x060072C2 RID: 29378 RVA: 0x001AD284 File Offset: 0x001AB484
		public sealed override ImmutableArray<Location> Locations
		{
			get
			{
				return this._locations;
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x060072C3 RID: 29379 RVA: 0x001AD28C File Offset: 0x001AB48C
		public sealed override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				if (!this.IsImplicitlyDeclared)
				{
					return Symbol.GetDeclaringSyntaxReferenceHelper<ParameterSyntax>(this._locations);
				}
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x060072C4 RID: 29380 RVA: 0x001AD2A7 File Offset: 0x001AB4A7
		public sealed override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this.parameterType;
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x060072C5 RID: 29381 RVA: 0x001AD2B0 File Offset: 0x001AB4B0
		public override bool IsImplicitlyDeclared
		{
			get
			{
				MethodSymbol methodSymbol = this.ContainingSymbol as MethodSymbol;
				return methodSymbol != null && methodSymbol.IsAccessor();
			}
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x060072C6 RID: 29382 RVA: 0x001AD2D4 File Offset: 0x001AB4D4
		internal override bool IsMetadataIn
		{
			get
			{
				return this.RefKind == RefKind.In;
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x001AD2DF File Offset: 0x001AB4DF
		internal override bool IsMetadataOut
		{
			get
			{
				return this.RefKind == RefKind.Out;
			}
		}

		// Token: 0x04001BEA RID: 7146
		protected SymbolCompletionState state;

		// Token: 0x04001BEB RID: 7147
		protected readonly TypeWithAnnotations parameterType;

		// Token: 0x04001BEC RID: 7148
		private readonly string _name;

		// Token: 0x04001BED RID: 7149
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x04001BEE RID: 7150
		private readonly RefKind _refKind;
	}
}
