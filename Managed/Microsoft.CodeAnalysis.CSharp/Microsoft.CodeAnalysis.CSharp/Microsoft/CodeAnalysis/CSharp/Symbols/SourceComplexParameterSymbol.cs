using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200052A RID: 1322
	internal class SourceComplexParameterSymbol : SourceParameterSymbol, IAttributeTargetSymbol
	{
		// Token: 0x06006F05 RID: 28421 RVA: 0x001979F8 File Offset: 0x00195BF8
		internal SourceComplexParameterSymbol(Symbol owner, int ordinal, TypeWithAnnotations parameterType, RefKind refKind, string name, ImmutableArray<Location> locations, SyntaxReference syntaxRef, bool isParams, bool isExtensionMethodThis)
			: base(owner, parameterType, ordinal, refKind, name, locations)
		{
			this._lazyHasOptionalAttribute = ThreeState.Unknown;
			this._syntaxRef = syntaxRef;
			if (isParams)
			{
				this._parameterSyntaxKind |= SourceComplexParameterSymbol.ParameterSyntaxKind.ParamsParameter;
			}
			if (isExtensionMethodThis)
			{
				this._parameterSyntaxKind |= SourceComplexParameterSymbol.ParameterSyntaxKind.ExtensionThisParameter;
			}
			ParameterSyntax csharpSyntaxNode = this.CSharpSyntaxNode;
			if (csharpSyntaxNode != null && csharpSyntaxNode.Default != null)
			{
				this._parameterSyntaxKind |= SourceComplexParameterSymbol.ParameterSyntaxKind.DefaultParameter;
			}
			this._lazyDefaultSyntaxValue = ConstantValue.Unset;
		}

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x06006F06 RID: 28422 RVA: 0x00197A72 File Offset: 0x00195C72
		private Binder ParameterBinderOpt
		{
			get
			{
				SourceMethodSymbolWithAttributes sourceMethodSymbolWithAttributes = this.ContainingSymbol as SourceMethodSymbolWithAttributes;
				if (sourceMethodSymbolWithAttributes == null)
				{
					return null;
				}
				return sourceMethodSymbolWithAttributes.ParameterBinder;
			}
		}

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x06006F07 RID: 28423 RVA: 0x00197A8A File Offset: 0x00195C8A
		internal sealed override SyntaxReference SyntaxReference
		{
			get
			{
				return this._syntaxRef;
			}
		}

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06006F08 RID: 28424 RVA: 0x00197A94 File Offset: 0x00195C94
		private ParameterSyntax CSharpSyntaxNode
		{
			get
			{
				SyntaxReference syntaxRef = this._syntaxRef;
				return (ParameterSyntax)((syntaxRef != null) ? syntaxRef.GetSyntax(default(CancellationToken)) : null);
			}
		}

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06006F09 RID: 28425 RVA: 0x00197AC1 File Offset: 0x00195CC1
		public override bool IsDiscard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D86 RID: 7558
		// (get) Token: 0x06006F0A RID: 28426 RVA: 0x00197AC4 File Offset: 0x00195CC4
		internal sealed override ConstantValue ExplicitDefaultConstantValue
		{
			get
			{
				return this.DefaultSyntaxValue ?? this.DefaultValueFromAttributes;
			}
		}

		// Token: 0x17001D87 RID: 7559
		// (get) Token: 0x06006F0B RID: 28427 RVA: 0x00197AD8 File Offset: 0x00195CD8
		internal sealed override ConstantValue DefaultValueFromAttributes
		{
			get
			{
				ParameterEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
				if (earlyDecodedWellKnownAttributeData == null || !(earlyDecodedWellKnownAttributeData.DefaultParameterValue != ConstantValue.Unset))
				{
					return null;
				}
				return earlyDecodedWellKnownAttributeData.DefaultParameterValue;
			}
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x06006F0C RID: 28428 RVA: 0x00197B09 File Offset: 0x00195D09
		internal sealed override bool IsIDispatchConstant
		{
			get
			{
				ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasIDispatchConstantAttribute;
			}
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x06006F0D RID: 28429 RVA: 0x00197B1C File Offset: 0x00195D1C
		internal override bool IsIUnknownConstant
		{
			get
			{
				ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasIUnknownConstantAttribute;
			}
		}

		// Token: 0x17001D8A RID: 7562
		// (get) Token: 0x06006F0E RID: 28430 RVA: 0x00197B2F File Offset: 0x00195D2F
		private bool HasCallerLineNumberAttribute
		{
			get
			{
				ParameterEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
				return earlyDecodedWellKnownAttributeData != null && earlyDecodedWellKnownAttributeData.HasCallerLineNumberAttribute;
			}
		}

		// Token: 0x17001D8B RID: 7563
		// (get) Token: 0x06006F0F RID: 28431 RVA: 0x00197B42 File Offset: 0x00195D42
		private bool HasCallerFilePathAttribute
		{
			get
			{
				ParameterEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
				return earlyDecodedWellKnownAttributeData != null && earlyDecodedWellKnownAttributeData.HasCallerFilePathAttribute;
			}
		}

		// Token: 0x17001D8C RID: 7564
		// (get) Token: 0x06006F10 RID: 28432 RVA: 0x00197B55 File Offset: 0x00195D55
		private bool HasCallerMemberNameAttribute
		{
			get
			{
				ParameterEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
				return earlyDecodedWellKnownAttributeData != null && earlyDecodedWellKnownAttributeData.HasCallerMemberNameAttribute;
			}
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x06006F11 RID: 28433 RVA: 0x00197B68 File Offset: 0x00195D68
		internal sealed override bool IsCallerLineNumber
		{
			get
			{
				return this.HasCallerLineNumberAttribute;
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x06006F12 RID: 28434 RVA: 0x00197B70 File Offset: 0x00195D70
		internal sealed override bool IsCallerFilePath
		{
			get
			{
				return !this.HasCallerLineNumberAttribute && this.HasCallerFilePathAttribute;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x06006F13 RID: 28435 RVA: 0x00197B82 File Offset: 0x00195D82
		internal sealed override bool IsCallerMemberName
		{
			get
			{
				return !this.HasCallerLineNumberAttribute && !this.HasCallerFilePathAttribute && this.HasCallerMemberNameAttribute;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x06006F14 RID: 28436 RVA: 0x00197B9C File Offset: 0x00195D9C
		internal override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return SourceComplexParameterSymbol.DecodeFlowAnalysisAttributes(this.GetDecodedWellKnownAttributeData());
			}
		}

		// Token: 0x06006F15 RID: 28437 RVA: 0x00197BAC File Offset: 0x00195DAC
		private static FlowAnalysisAnnotations DecodeFlowAnalysisAttributes(ParameterWellKnownAttributeData attributeData)
		{
			if (attributeData == null)
			{
				return FlowAnalysisAnnotations.None;
			}
			FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
			if (attributeData.HasAllowNullAttribute)
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.AllowNull;
			}
			if (attributeData.HasDisallowNullAttribute)
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.DisallowNull;
			}
			bool? flag;
			if (attributeData.HasMaybeNullAttribute)
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.MaybeNull;
			}
			else
			{
				flag = attributeData.MaybeNullWhenAttribute;
				if (flag != null)
				{
					bool valueOrDefault = flag.GetValueOrDefault();
					flowAnalysisAnnotations |= (valueOrDefault ? FlowAnalysisAnnotations.MaybeNullWhenTrue : FlowAnalysisAnnotations.MaybeNullWhenFalse);
				}
			}
			if (attributeData.HasNotNullAttribute)
			{
				flowAnalysisAnnotations |= FlowAnalysisAnnotations.NotNull;
			}
			else
			{
				flag = attributeData.NotNullWhenAttribute;
				if (flag != null)
				{
					bool valueOrDefault2 = flag.GetValueOrDefault();
					flowAnalysisAnnotations |= (valueOrDefault2 ? FlowAnalysisAnnotations.NotNullWhenTrue : FlowAnalysisAnnotations.NotNullWhenFalse);
				}
			}
			flag = attributeData.DoesNotReturnIfAttribute;
			if (flag != null)
			{
				bool valueOrDefault3 = flag.GetValueOrDefault();
				flowAnalysisAnnotations |= (valueOrDefault3 ? FlowAnalysisAnnotations.DoesNotReturnIfTrue : FlowAnalysisAnnotations.DoesNotReturnIfFalse);
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x06006F16 RID: 28438 RVA: 0x00197C66 File Offset: 0x00195E66
		internal override ImmutableHashSet<string> NotNullIfParameterNotNull
		{
			get
			{
				ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return ((decodedWellKnownAttributeData != null) ? decodedWellKnownAttributeData.NotNullIfParameterNotNull : null) ?? ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x06006F17 RID: 28439 RVA: 0x00197C83 File Offset: 0x00195E83
		internal bool HasEnumeratorCancellationAttribute
		{
			get
			{
				ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasEnumeratorCancellationAttribute;
			}
		}

		// Token: 0x06006F18 RID: 28440 RVA: 0x00197C96 File Offset: 0x00195E96
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static SyntaxNode GetDefaultValueSyntaxForIsNullableAnalysisEnabled(ParameterSyntax parameterSyntax)
		{
			if (parameterSyntax == null)
			{
				return null;
			}
			EqualsValueClauseSyntax @default = parameterSyntax.Default;
			if (@default == null)
			{
				return null;
			}
			return @default.Value;
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x06006F19 RID: 28441 RVA: 0x00197CB0 File Offset: 0x00195EB0
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private ConstantValue DefaultSyntaxValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				if (this.state.NotePartComplete(CompletionPart.Members))
				{
					BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
					Binder binder;
					BoundParameterEqualsValue boundParameterEqualsValue;
					Interlocked.CompareExchange<ConstantValue>(ref this._lazyDefaultSyntaxValue, this.MakeDefaultExpression(instance, out binder, out boundParameterEqualsValue), ConstantValue.Unset);
					this.state.NotePartComplete(CompletionPart.TypeMembers);
					if (boundParameterEqualsValue != null)
					{
						if (binder != null)
						{
							SyntaxNode defaultValueSyntaxForIsNullableAnalysisEnabled = SourceComplexParameterSymbol.GetDefaultValueSyntaxForIsNullableAnalysisEnabled(this.CSharpSyntaxNode);
							if (defaultValueSyntaxForIsNullableAnalysisEnabled != null)
							{
								NullableWalker.AnalyzeIfNeeded(binder, boundParameterEqualsValue, defaultValueSyntaxForIsNullableAnalysisEnabled, instance.DiagnosticBag);
							}
						}
						if (!this._lazyDefaultSyntaxValue.IsBad)
						{
							this.VerifyParamDefaultValueMatchesAttributeIfAny(this._lazyDefaultSyntaxValue, boundParameterEqualsValue.Value.Syntax, instance);
						}
					}
					this.AddDeclarationDiagnostics(instance);
					instance.Free();
					this.state.NotePartComplete(CompletionPart.SynthesizedExplicitImplementations);
				}
				this.state.SpinWaitComplete(CompletionPart.TypeMembers, default(CancellationToken));
				return this._lazyDefaultSyntaxValue;
			}
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x00197D8C File Offset: 0x00195F8C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private Binder GetBinder(SyntaxNode syntax)
		{
			Binder binder = this.ParameterBinderOpt;
			if (binder == null)
			{
				binder = this.DeclaringCompilation.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax, null, null);
			}
			return binder;
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x00197DC0 File Offset: 0x00195FC0
		private void NullableAnalyzeParameterDefaultValueFromAttributes()
		{
			ParameterSyntax csharpSyntaxNode = this.CSharpSyntaxNode;
			if (csharpSyntaxNode == null)
			{
				return;
			}
			SyntaxNode node = csharpSyntaxNode.AttributeLists.Node;
			if (node == null || !NullableWalker.NeedsAnalysis(this.DeclaringCompilation, node))
			{
				return;
			}
			ConstantValue defaultValueFromAttributes = this.DefaultValueFromAttributes;
			if (defaultValueFromAttributes == null || defaultValueFromAttributes.IsBad)
			{
				return;
			}
			Binder binder = this.GetBinder(csharpSyntaxNode);
			BoundParameterEqualsValue boundParameterEqualsValue = new BoundParameterEqualsValue(csharpSyntaxNode, this, ImmutableArray<LocalSymbol>.Empty, new BoundLiteral(csharpSyntaxNode, defaultValueFromAttributes, base.Type), false);
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, false);
			NullableWalker.AnalyzeIfNeeded(binder, boundParameterEqualsValue, csharpSyntaxNode, instance.DiagnosticBag);
			this.AddDeclarationDiagnostics(instance);
			instance.Free();
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x00197E5C File Offset: 0x0019605C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private ConstantValue MakeDefaultExpression(BindingDiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(2)] out Binder binder, [global::System.Runtime.CompilerServices.Nullable(2)] out BoundParameterEqualsValue parameterEqualsValue)
		{
			binder = null;
			parameterEqualsValue = null;
			ParameterSyntax csharpSyntaxNode = this.CSharpSyntaxNode;
			if (csharpSyntaxNode == null)
			{
				return null;
			}
			EqualsValueClauseSyntax @default = csharpSyntaxNode.Default;
			if (@default == null)
			{
				return null;
			}
			binder = this.GetBinder(@default);
			Binder binder2 = binder.CreateBinderForParameterDefaultValue(this, @default);
			BoundExpression boundExpression;
			parameterEqualsValue = binder2.BindParameterDefaultValue(@default, this, diagnostics, out boundExpression);
			if (boundExpression.HasErrors)
			{
				return ConstantValue.Bad;
			}
			BoundExpression boundExpression2 = parameterEqualsValue.Value;
			if (ParameterHelpers.ReportDefaultParameterErrors(binder, this.ContainingSymbol, csharpSyntaxNode, this, boundExpression, boundExpression2, diagnostics))
			{
				return ConstantValue.Bad;
			}
			if (boundExpression2.ConstantValue == null && boundExpression2.Kind == BoundKind.Conversion && ((BoundConversion)boundExpression2).ConversionKind != ConversionKind.DefaultLiteral && this.parameterType.Type.IsNullableType())
			{
				boundExpression2 = binder.GenerateConversionForAssignment(this.parameterType.Type.GetNullableUnderlyingType(), boundExpression, diagnostics, true, false);
			}
			return boundExpression2.ConstantValue ?? ConstantValue.Null;
		}

		// Token: 0x17001D94 RID: 7572
		// (get) Token: 0x06006F1D RID: 28445 RVA: 0x00197F44 File Offset: 0x00196144
		public override string MetadataName
		{
			get
			{
				SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = this.ContainingSymbol as SourceOrdinaryMethodSymbol;
				if (sourceOrdinaryMethodSymbol == null)
				{
					return base.MetadataName;
				}
				SourceOrdinaryMethodSymbol sourcePartialDefinition = sourceOrdinaryMethodSymbol.SourcePartialDefinition;
				if (sourcePartialDefinition == null)
				{
					return base.MetadataName;
				}
				return sourcePartialDefinition.Parameters[this.Ordinal].MetadataName;
			}
		}

		// Token: 0x17001D95 RID: 7573
		// (get) Token: 0x06006F1E RID: 28446 RVA: 0x00197F91 File Offset: 0x00196191
		protected virtual IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001D96 RID: 7574
		// (get) Token: 0x06006F1F RID: 28447 RVA: 0x00197F94 File Offset: 0x00196194
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this.AttributeOwner;
			}
		}

		// Token: 0x17001D97 RID: 7575
		// (get) Token: 0x06006F20 RID: 28448 RVA: 0x00197F9C File Offset: 0x0019619C
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Parameter;
			}
		}

		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06006F21 RID: 28449 RVA: 0x00197FA3 File Offset: 0x001961A3
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				if (SynthesizedRecordPropertySymbol.HaveCorrespondingSynthesizedRecordPropertySymbol(this))
				{
					return AttributeLocation.Field | AttributeLocation.Property | AttributeLocation.Parameter;
				}
				return AttributeLocation.Parameter;
			}
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x06006F22 RID: 28450 RVA: 0x00197FB8 File Offset: 0x001961B8
		private SourceParameterSymbol BoundAttributesSource
		{
			get
			{
				SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = this.ContainingSymbol as SourceOrdinaryMethodSymbol;
				if (sourceOrdinaryMethodSymbol == null)
				{
					return null;
				}
				SourceOrdinaryMethodSymbol sourcePartialImplementation = sourceOrdinaryMethodSymbol.SourcePartialImplementation;
				if (sourcePartialImplementation == null)
				{
					return null;
				}
				return (SourceParameterSymbol)sourcePartialImplementation.Parameters[this.Ordinal];
			}
		}

		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x06006F23 RID: 28451 RVA: 0x00197FFC File Offset: 0x001961FC
		internal sealed override SyntaxList<AttributeListSyntax> AttributeDeclarationList
		{
			get
			{
				ParameterSyntax csharpSyntaxNode = this.CSharpSyntaxNode;
				if (csharpSyntaxNode == null)
				{
					return default(SyntaxList<AttributeListSyntax>);
				}
				return csharpSyntaxNode.AttributeLists;
			}
		}

		// Token: 0x06006F24 RID: 28452 RVA: 0x00198024 File Offset: 0x00196224
		internal virtual OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			SyntaxList<AttributeListSyntax> attributeDeclarationList = this.AttributeDeclarationList;
			SourceOrdinaryMethodSymbol sourceOrdinaryMethodSymbol = this.ContainingSymbol as SourceOrdinaryMethodSymbol;
			if (sourceOrdinaryMethodSymbol == null)
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(attributeDeclarationList);
			}
			SourceOrdinaryMethodSymbol otherPartOfPartial = sourceOrdinaryMethodSymbol.OtherPartOfPartial;
			SyntaxList<AttributeListSyntax> syntaxList;
			if (otherPartOfPartial != null)
			{
				syntaxList = ((SourceParameterSymbol)otherPartOfPartial.Parameters[this.Ordinal]).AttributeDeclarationList;
			}
			else
			{
				syntaxList = default(SyntaxList<AttributeListSyntax>);
			}
			if (attributeDeclarationList.Equals(default(SyntaxList<AttributeListSyntax>)))
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(syntaxList);
			}
			if (syntaxList.Equals(default(SyntaxList<AttributeListSyntax>)))
			{
				return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(attributeDeclarationList);
			}
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(ImmutableArray.Create<SyntaxList<AttributeListSyntax>>(attributeDeclarationList, syntaxList));
		}

		// Token: 0x06006F25 RID: 28453 RVA: 0x001980C4 File Offset: 0x001962C4
		internal ParameterWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (ParameterWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x06006F26 RID: 28454 RVA: 0x001980F8 File Offset: 0x001962F8
		internal ParameterEarlyWellKnownAttributeData GetEarlyDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (ParameterEarlyWellKnownAttributeData)customAttributesBag.EarlyDecodedWellKnownAttributeData;
		}

		// Token: 0x06006F27 RID: 28455 RVA: 0x0019812C File Offset: 0x0019632C
		internal sealed override CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			if (this._lazyCustomAttributesBag == null || !this._lazyCustomAttributesBag.IsSealed)
			{
				SourceParameterSymbol boundAttributesSource = this.BoundAttributesSource;
				bool flag;
				if (boundAttributesSource != null)
				{
					CustomAttributesBag<CSharpAttributeData> attributesBag = boundAttributesSource.GetAttributesBag();
					flag = Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref this._lazyCustomAttributesBag, attributesBag, null) == null;
				}
				else
				{
					OneOrMany<SyntaxList<AttributeListSyntax>> attributeDeclarations = this.GetAttributeDeclarations();
					flag = base.LoadAndValidateAttributes(attributeDeclarations, ref this._lazyCustomAttributesBag, AttributeLocation.None, false, this.ParameterBinderOpt, null);
				}
				if (flag)
				{
					this.NullableAnalyzeParameterDefaultValueFromAttributes();
					this.state.NotePartComplete(CompletionPart.Attributes);
				}
			}
			return this._lazyCustomAttributesBag;
		}

		// Token: 0x06006F28 RID: 28456 RVA: 0x001981AC File Offset: 0x001963AC
		internal override void EarlyDecodeWellKnownAttributeType(NamedTypeSymbol attributeType, AttributeSyntax attributeSyntax)
		{
			if (CSharpAttributeData.IsTargetEarlyAttribute(attributeType, attributeSyntax, AttributeDescription.OptionalAttribute))
			{
				this._lazyHasOptionalAttribute = ThreeState.True;
			}
		}

		// Token: 0x06006F29 RID: 28457 RVA: 0x001981C3 File Offset: 0x001963C3
		internal override void PostEarlyDecodeWellKnownAttributeTypes()
		{
			if (this._lazyHasOptionalAttribute == ThreeState.Unknown)
			{
				this._lazyHasOptionalAttribute = ThreeState.False;
			}
			base.PostEarlyDecodeWellKnownAttributeTypes();
		}

		// Token: 0x06006F2A RID: 28458 RVA: 0x001981DC File Offset: 0x001963DC
		internal override CSharpAttributeData EarlyDecodeWellKnownAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.DefaultParameterValueAttribute))
			{
				return this.EarlyDecodeAttributeForDefaultParameterValue(AttributeDescription.DefaultParameterValueAttribute, ref arguments);
			}
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.DecimalConstantAttribute))
			{
				return this.EarlyDecodeAttributeForDefaultParameterValue(AttributeDescription.DecimalConstantAttribute, ref arguments);
			}
			if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.DateTimeConstantAttribute))
			{
				return this.EarlyDecodeAttributeForDefaultParameterValue(AttributeDescription.DateTimeConstantAttribute, ref arguments);
			}
			if (!this.IsOnPartialImplementation(arguments.AttributeSyntax))
			{
				if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.CallerLineNumberAttribute))
				{
					arguments.GetOrCreateData<ParameterEarlyWellKnownAttributeData>().HasCallerLineNumberAttribute = true;
				}
				else if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.CallerFilePathAttribute))
				{
					arguments.GetOrCreateData<ParameterEarlyWellKnownAttributeData>().HasCallerFilePathAttribute = true;
				}
				else if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.CallerMemberNameAttribute))
				{
					arguments.GetOrCreateData<ParameterEarlyWellKnownAttributeData>().HasCallerMemberNameAttribute = true;
				}
			}
			return base.EarlyDecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x06006F2B RID: 28459 RVA: 0x001982E0 File Offset: 0x001964E0
		private CSharpAttributeData EarlyDecodeAttributeForDefaultParameterValue(AttributeDescription description, ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			bool flag;
			CSharpAttributeData attribute = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
			ConstantValue constantValue;
			if (attribute.HasErrors)
			{
				constantValue = ConstantValue.Bad;
				flag = true;
			}
			else
			{
				constantValue = this.DecodeDefaultParameterValueAttribute(description, attribute, arguments.AttributeSyntax, false, null);
			}
			ParameterEarlyWellKnownAttributeData orCreateData = arguments.GetOrCreateData<ParameterEarlyWellKnownAttributeData>();
			if (orCreateData.DefaultParameterValue == ConstantValue.Unset)
			{
				orCreateData.DefaultParameterValue = constantValue;
			}
			if (flag)
			{
				return null;
			}
			return attribute;
		}

		// Token: 0x06006F2C RID: 28460 RVA: 0x00198350 File Offset: 0x00196550
		internal override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			if (attribute.IsTargetAttribute(this, AttributeDescription.DefaultParameterValueAttribute))
			{
				this.DecodeDefaultParameterValueAttribute(AttributeDescription.DefaultParameterValueAttribute, ref arguments);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.DecimalConstantAttribute))
			{
				this.DecodeDefaultParameterValueAttribute(AttributeDescription.DecimalConstantAttribute, ref arguments);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.DateTimeConstantAttribute))
			{
				this.DecodeDefaultParameterValueAttribute(AttributeDescription.DateTimeConstantAttribute, ref arguments);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.OptionalAttribute))
			{
				if (this.HasDefaultArgumentSyntax)
				{
					bindingDiagnosticBag.Add(ErrorCode.ERR_DefaultValueUsedWithAttributes, arguments.AttributeSyntaxOpt.Name.Location);
					return;
				}
			}
			else
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.ParamArrayAttribute))
				{
					bindingDiagnosticBag.Add(ErrorCode.ERR_ExplicitParamArray, arguments.AttributeSyntaxOpt.Name.Location);
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.InAttribute))
				{
					arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasInAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.OutAttribute))
				{
					arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasOutAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.MarshalAsAttribute))
				{
					MarshalAsAttributeDecoder<ParameterWellKnownAttributeData, AttributeSyntax, CSharpAttributeData, AttributeLocation>.Decode(ref arguments, 2048, MessageProvider.Instance);
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.IDispatchConstantAttribute))
				{
					arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasIDispatchConstantAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.IUnknownConstantAttribute))
				{
					arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasIUnknownConstantAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.CallerLineNumberAttribute))
				{
					this.ValidateCallerLineNumberAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.CallerFilePathAttribute))
				{
					this.ValidateCallerFilePathAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.CallerMemberNameAttribute))
				{
					this.ValidateCallerMemberNameAttribute(arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
					return;
				}
				if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.DynamicAttribute | Symbol.ReservedAttributes.IsReadOnlyAttribute | Symbol.ReservedAttributes.IsUnmanagedAttribute | Symbol.ReservedAttributes.IsByRefLikeAttribute | Symbol.ReservedAttributes.TupleElementNamesAttribute | Symbol.ReservedAttributes.NullableAttribute | Symbol.ReservedAttributes.NativeIntegerAttribute))
				{
					if (attribute.IsTargetAttribute(this, AttributeDescription.AllowNullAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasAllowNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.DisallowNullAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasDisallowNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.MaybeNullAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasMaybeNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.MaybeNullWhenAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().MaybeNullWhenAttribute = SourceComplexParameterSymbol.DecodeMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(AttributeDescription.MaybeNullWhenAttribute, attribute);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasNotNullAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullWhenAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().NotNullWhenAttribute = SourceComplexParameterSymbol.DecodeMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(AttributeDescription.NotNullWhenAttribute, attribute);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.DoesNotReturnIfAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().DoesNotReturnIfAttribute = SourceComplexParameterSymbol.DecodeMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(AttributeDescription.DoesNotReturnIfAttribute, attribute);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullIfNotNullAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().AddNotNullIfParameterNotNull(attribute.DecodeNotNullIfNotNullAttribute());
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.EnumeratorCancellationAttribute))
					{
						arguments.GetOrCreateData<ParameterWellKnownAttributeData>().HasEnumeratorCancellationAttribute = true;
						this.ValidateCancellationTokenAttribute(arguments.AttributeSyntaxOpt, (BindingDiagnosticBag)arguments.Diagnostics);
					}
				}
			}
		}

		// Token: 0x06006F2D RID: 28461 RVA: 0x00198640 File Offset: 0x00196840
		private static bool? DecodeMaybeNullWhenOrNotNullWhenOrDoesNotReturnIfAttribute(AttributeDescription description, CSharpAttributeData attribute)
		{
			ImmutableArray<TypedConstant> commonConstructorArguments = attribute.CommonConstructorArguments;
			bool flag;
			if (commonConstructorArguments.Length != 1 || !commonConstructorArguments[0].TryDecodeValue<bool>(SpecialType.System_Boolean, out flag))
			{
				return default(bool?);
			}
			return new bool?(flag);
		}

		// Token: 0x06006F2E RID: 28462 RVA: 0x00198684 File Offset: 0x00196884
		private void DecodeDefaultParameterValueAttribute(AttributeDescription description, ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			AttributeSyntax attributeSyntaxOpt = arguments.AttributeSyntaxOpt;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			ConstantValue constantValue = this.DecodeDefaultParameterValueAttribute(description, attribute, attributeSyntaxOpt, true, bindingDiagnosticBag);
			if (!constantValue.IsBad)
			{
				this.VerifyParamDefaultValueMatchesAttributeIfAny(constantValue, attributeSyntaxOpt, bindingDiagnosticBag);
			}
		}

		// Token: 0x06006F2F RID: 28463 RVA: 0x001986C8 File Offset: 0x001968C8
		private void VerifyParamDefaultValueMatchesAttributeIfAny(ConstantValue value, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			ParameterEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
			if (earlyDecodedWellKnownAttributeData != null)
			{
				ConstantValue defaultParameterValue = earlyDecodedWellKnownAttributeData.DefaultParameterValue;
				if (defaultParameterValue != ConstantValue.Unset && value != defaultParameterValue)
				{
					diagnostics.Add(ErrorCode.ERR_ParamDefaultValueDiffersFromAttribute, syntax.Location);
				}
			}
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x00198710 File Offset: 0x00196910
		private ConstantValue DecodeDefaultParameterValueAttribute(AttributeDescription description, CSharpAttributeData attribute, AttributeSyntax node, bool diagnose, BindingDiagnosticBag diagnosticsOpt)
		{
			if (description.Equals(AttributeDescription.DefaultParameterValueAttribute))
			{
				return this.DecodeDefaultParameterValueAttribute(attribute, node, diagnose, diagnosticsOpt);
			}
			if (description.Equals(AttributeDescription.DecimalConstantAttribute))
			{
				return attribute.DecodeDecimalConstantValue();
			}
			return attribute.DecodeDateTimeConstantValue();
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x0019876C File Offset: 0x0019696C
		private ConstantValue DecodeDefaultParameterValueAttribute(CSharpAttributeData attribute, AttributeSyntax node, bool diagnose, BindingDiagnosticBag diagnosticsOpt)
		{
			if (this.HasDefaultArgumentSyntax)
			{
				if (diagnose)
				{
					diagnosticsOpt.Add(ErrorCode.ERR_DefaultValueUsedWithAttributes, node.Name.Location);
				}
				return ConstantValue.Bad;
			}
			TypedConstant typedConstant = attribute.CommonConstructorArguments[0];
			SpecialType specialType = ((typedConstant.Kind == TypedConstantKind.Enum) ? ((NamedTypeSymbol)typedConstant.TypeInternal).EnumUnderlyingType.SpecialType : typedConstant.TypeInternal.SpecialType);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			ConstantValueTypeDiscriminator constantValueTypeDiscriminator = ConstantValue.GetDiscriminator(specialType);
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnosticsOpt, this.ContainingAssembly);
			if (constantValueTypeDiscriminator == ConstantValueTypeDiscriminator.Bad)
			{
				if (typedConstant.Kind == TypedConstantKind.Array || typedConstant.ValueInternal != null)
				{
					if (diagnose)
					{
						diagnosticsOpt.Add(ErrorCode.ERR_DefaultValueBadValueType, node.Name.Location, new object[] { typedConstant.TypeInternal });
					}
					return ConstantValue.Bad;
				}
				if (!base.Type.IsReferenceType)
				{
					if (diagnose)
					{
						diagnosticsOpt.Add(ErrorCode.ERR_DefaultValueTypeMustMatch, node.Name.Location);
					}
					return ConstantValue.Bad;
				}
				constantValueTypeDiscriminator = ConstantValueTypeDiscriminator.Nothing;
			}
			else if (!declaringCompilation.Conversions.ClassifyConversionFromType((TypeSymbol)typedConstant.TypeInternal, base.Type, ref compoundUseSiteInfo, false).Kind.IsImplicitConversion())
			{
				if (diagnose)
				{
					diagnosticsOpt.Add(ErrorCode.ERR_DefaultValueTypeMustMatch, node.Name.Location);
					diagnosticsOpt.Add(node.Name.Location, compoundUseSiteInfo);
				}
				return ConstantValue.Bad;
			}
			if (diagnose)
			{
				diagnosticsOpt.Add(node.Name.Location, compoundUseSiteInfo);
			}
			return ConstantValue.Create(typedConstant.ValueInternal, constantValueTypeDiscriminator);
		}

		// Token: 0x06006F32 RID: 28466 RVA: 0x00198903 File Offset: 0x00196B03
		private bool IsValidCallerInfoContext(AttributeSyntax node)
		{
			return !this.ContainingSymbol.IsExplicitInterfaceImplementation() && !this.ContainingSymbol.IsOperator() && !this.IsOnPartialImplementation(node);
		}

		// Token: 0x06006F33 RID: 28467 RVA: 0x0019892C File Offset: 0x00196B2C
		private bool IsOnPartialImplementation(AttributeSyntax node)
		{
			MethodSymbol methodSymbol = this.ContainingSymbol as MethodSymbol;
			if (methodSymbol == null)
			{
				return false;
			}
			MethodSymbol methodSymbol2 = (methodSymbol.IsPartialImplementation() ? methodSymbol : methodSymbol.PartialImplementationPart);
			if (methodSymbol2 == null)
			{
				return false;
			}
			ParameterListSyntax parameterListSyntax = node.Parent.Parent.Parent as ParameterListSyntax;
			if (parameterListSyntax == null)
			{
				return false;
			}
			MethodDeclarationSyntax methodDeclarationSyntax = parameterListSyntax.Parent as MethodDeclarationSyntax;
			if (methodDeclarationSyntax == null)
			{
				return false;
			}
			ImmutableArray<SyntaxReference>.Enumerator enumerator = methodSymbol2.DeclaringSyntaxReferences.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetSyntax(default(CancellationToken)) == methodDeclarationSyntax)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006F34 RID: 28468 RVA: 0x001989C8 File Offset: 0x00196BC8
		private void ValidateCallerLineNumberAttribute(AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			if (!this.IsValidCallerInfoContext(node))
			{
				diagnostics.Add(ErrorCode.WRN_CallerLineNumberParamForUnconsumedLocation, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
			else if (!declaringCompilation.Conversions.HasCallerLineNumberConversion(this.TypeWithAnnotations.Type, ref compoundUseSiteInfo))
			{
				TypeSymbol specialType = declaringCompilation.GetSpecialType(SpecialType.System_Int32);
				diagnostics.Add(ErrorCode.ERR_NoConversionForCallerLineNumberParam, node.Name.Location, new object[]
				{
					specialType,
					this.TypeWithAnnotations.Type
				});
			}
			else if (!base.HasExplicitDefaultValue && !this.ContainingSymbol.IsPartialImplementation())
			{
				diagnostics.Add(ErrorCode.ERR_BadCallerLineNumberParamWithoutDefaultValue, node.Name.Location);
			}
			diagnostics.Add(node.Name.Location, compoundUseSiteInfo);
		}

		// Token: 0x06006F35 RID: 28469 RVA: 0x00198AC8 File Offset: 0x00196CC8
		private void ValidateCallerFilePathAttribute(AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			if (!this.IsValidCallerInfoContext(node))
			{
				diagnostics.Add(ErrorCode.WRN_CallerFilePathParamForUnconsumedLocation, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
			else if (!declaringCompilation.Conversions.HasCallerInfoStringConversion(this.TypeWithAnnotations.Type, ref compoundUseSiteInfo))
			{
				TypeSymbol specialType = declaringCompilation.GetSpecialType(SpecialType.System_String);
				diagnostics.Add(ErrorCode.ERR_NoConversionForCallerFilePathParam, node.Name.Location, new object[]
				{
					specialType,
					this.TypeWithAnnotations.Type
				});
			}
			else if (!base.HasExplicitDefaultValue && !this.ContainingSymbol.IsPartialImplementation())
			{
				diagnostics.Add(ErrorCode.ERR_BadCallerFilePathParamWithoutDefaultValue, node.Name.Location);
			}
			else if (this.HasCallerLineNumberAttribute)
			{
				diagnostics.Add(ErrorCode.WRN_CallerLineNumberPreferredOverCallerFilePath, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
			diagnostics.Add(node.Name.Location, compoundUseSiteInfo);
		}

		// Token: 0x06006F36 RID: 28470 RVA: 0x00198C04 File Offset: 0x00196E04
		private void ValidateCallerMemberNameAttribute(AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			if (!this.IsValidCallerInfoContext(node))
			{
				diagnostics.Add(ErrorCode.WRN_CallerMemberNameParamForUnconsumedLocation, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
			else if (!declaringCompilation.Conversions.HasCallerInfoStringConversion(this.TypeWithAnnotations.Type, ref compoundUseSiteInfo))
			{
				TypeSymbol specialType = declaringCompilation.GetSpecialType(SpecialType.System_String);
				diagnostics.Add(ErrorCode.ERR_NoConversionForCallerMemberNameParam, node.Name.Location, new object[]
				{
					specialType,
					this.TypeWithAnnotations.Type
				});
			}
			else if (!base.HasExplicitDefaultValue && !this.ContainingSymbol.IsPartialImplementation())
			{
				diagnostics.Add(ErrorCode.ERR_BadCallerMemberNameParamWithoutDefaultValue, node.Name.Location);
			}
			else if (this.HasCallerLineNumberAttribute)
			{
				diagnostics.Add(ErrorCode.WRN_CallerLineNumberPreferredOverCallerMemberName, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
			else if (this.HasCallerFilePathAttribute)
			{
				diagnostics.Add(ErrorCode.WRN_CallerFilePathPreferredOverCallerMemberName, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
			diagnostics.Add(node.Name.Location, compoundUseSiteInfo);
		}

		// Token: 0x06006F37 RID: 28471 RVA: 0x00198D80 File Offset: 0x00196F80
		private void ValidateCancellationTokenAttribute(AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			if (this.<ValidateCancellationTokenAttribute>g__needsReporting|81_0())
			{
				diagnostics.Add(ErrorCode.WRN_UnconsumedEnumeratorCancellationAttributeUsage, node.Name.Location, new object[] { this.CSharpSyntaxNode.Identifier.ValueText });
			}
		}

		// Token: 0x06006F38 RID: 28472 RVA: 0x00198DC8 File Offset: 0x00196FC8
		internal override void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
			ParameterWellKnownAttributeData parameterWellKnownAttributeData = (ParameterWellKnownAttributeData)decodedData;
			if (parameterWellKnownAttributeData != null)
			{
				switch (this.RefKind)
				{
				case RefKind.Ref:
					if (parameterWellKnownAttributeData.HasOutAttribute && !parameterWellKnownAttributeData.HasInAttribute)
					{
						diagnostics.Add(ErrorCode.ERR_OutAttrOnRefParam, this.Locations[0]);
					}
					break;
				case RefKind.Out:
					if (parameterWellKnownAttributeData.HasInAttribute)
					{
						diagnostics.Add(ErrorCode.ERR_InAttrOnOutParam, this.Locations[0]);
					}
					break;
				case RefKind.In:
					if (parameterWellKnownAttributeData.HasOutAttribute)
					{
						diagnostics.Add(ErrorCode.ERR_OutAttrOnInParam, this.Locations[0]);
					}
					break;
				}
			}
			base.PostDecodeWellKnownAttributes(boundAttributes, allAttributeSyntaxNodes, diagnostics, symbolPart, decodedData);
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x06006F39 RID: 28473 RVA: 0x00198E7F File Offset: 0x0019707F
		internal override bool HasDefaultArgumentSyntax
		{
			get
			{
				return (this._parameterSyntaxKind & SourceComplexParameterSymbol.ParameterSyntaxKind.DefaultParameter) > SourceComplexParameterSymbol.ParameterSyntaxKind.Regular;
			}
		}

		// Token: 0x17001D9C RID: 7580
		// (get) Token: 0x06006F3A RID: 28474 RVA: 0x00198E8C File Offset: 0x0019708C
		internal sealed override bool HasOptionalAttribute
		{
			get
			{
				if (this._lazyHasOptionalAttribute == ThreeState.Unknown)
				{
					SourceParameterSymbol boundAttributesSource = this.BoundAttributesSource;
					if (boundAttributesSource != null)
					{
						this._lazyHasOptionalAttribute = boundAttributesSource.HasOptionalAttribute.ToThreeState();
					}
					else if (!this.GetAttributes().Any<CSharpAttributeData>())
					{
						this._lazyHasOptionalAttribute = ThreeState.False;
					}
				}
				return this._lazyHasOptionalAttribute.Value();
			}
		}

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06006F3B RID: 28475 RVA: 0x00198EDD File Offset: 0x001970DD
		internal override bool IsMetadataOptional
		{
			get
			{
				return this.HasDefaultArgumentSyntax || this.HasOptionalAttribute;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06006F3C RID: 28476 RVA: 0x00198EEF File Offset: 0x001970EF
		internal sealed override bool IsMetadataIn
		{
			get
			{
				if (!base.IsMetadataIn)
				{
					ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
					return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasInAttribute;
				}
				return true;
			}
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x06006F3D RID: 28477 RVA: 0x00198F0C File Offset: 0x0019710C
		internal sealed override bool IsMetadataOut
		{
			get
			{
				if (!base.IsMetadataOut)
				{
					ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
					return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasOutAttribute;
				}
				return true;
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x06006F3E RID: 28478 RVA: 0x00198F29 File Offset: 0x00197129
		internal sealed override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			get
			{
				ParameterWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return null;
				}
				return decodedWellKnownAttributeData.MarshallingInformation;
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x06006F3F RID: 28479 RVA: 0x00198F3C File Offset: 0x0019713C
		public override bool IsParams
		{
			get
			{
				return (this._parameterSyntaxKind & SourceComplexParameterSymbol.ParameterSyntaxKind.ParamsParameter) > SourceComplexParameterSymbol.ParameterSyntaxKind.Regular;
			}
		}

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x06006F40 RID: 28480 RVA: 0x00198F49 File Offset: 0x00197149
		internal override bool IsExtensionMethodThis
		{
			get
			{
				return (this._parameterSyntaxKind & SourceComplexParameterSymbol.ParameterSyntaxKind.ExtensionThisParameter) > SourceComplexParameterSymbol.ParameterSyntaxKind.Regular;
			}
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x06006F41 RID: 28481 RVA: 0x00198F56 File Offset: 0x00197156
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x06006F42 RID: 28482 RVA: 0x00198F5D File Offset: 0x0019715D
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			this.GetAttributes();
			ConstantValue explicitDefaultConstantValue = this.ExplicitDefaultConstantValue;
			this.state.SpinWaitComplete(CompletionPart.ComplexParameterSymbolAll, cancellationToken);
		}

		// Token: 0x06006F43 RID: 28483 RVA: 0x00198F80 File Offset: 0x00197180
		[CompilerGenerated]
		private bool <ValidateCancellationTokenAttribute>g__needsReporting|81_0()
		{
			if (!base.Type.Equals(this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Threading_CancellationToken)))
			{
				return true;
			}
			MethodSymbol methodSymbol = this.ContainingSymbol as MethodSymbol;
			return methodSymbol == null || !methodSymbol.IsAsync || !methodSymbol.ReturnType.OriginalDefinition.Equals(this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T));
		}

		// Token: 0x04001B47 RID: 6983
		private readonly SyntaxReference _syntaxRef;

		// Token: 0x04001B48 RID: 6984
		private readonly SourceComplexParameterSymbol.ParameterSyntaxKind _parameterSyntaxKind;

		// Token: 0x04001B49 RID: 6985
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001B4A RID: 6986
		private ThreeState _lazyHasOptionalAttribute;

		// Token: 0x04001B4B RID: 6987
		protected ConstantValue _lazyDefaultSyntaxValue;

		// Token: 0x02000914 RID: 2324
		[Flags]
		private enum ParameterSyntaxKind : byte
		{
			// Token: 0x04002767 RID: 10087
			Regular = 0,
			// Token: 0x04002768 RID: 10088
			ParamsParameter = 1,
			// Token: 0x04002769 RID: 10089
			ExtensionThisParameter = 2,
			// Token: 0x0400276A RID: 10090
			DefaultParameter = 4
		}
	}
}
