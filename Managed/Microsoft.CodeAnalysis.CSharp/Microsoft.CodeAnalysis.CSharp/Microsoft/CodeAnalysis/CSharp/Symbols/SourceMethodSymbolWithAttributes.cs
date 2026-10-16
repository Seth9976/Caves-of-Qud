using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000544 RID: 1348
	internal abstract class SourceMethodSymbolWithAttributes : SourceMethodSymbol, IAttributeTargetSymbol
	{
		// Token: 0x0600718A RID: 29066 RVA: 0x001A5600 File Offset: 0x001A3800
		protected SourceMethodSymbolWithAttributes(SyntaxReference syntaxReferenceOpt)
		{
			this.syntaxReferenceOpt = syntaxReferenceOpt;
		}

		// Token: 0x0600718B RID: 29067 RVA: 0x001A5610 File Offset: 0x001A3810
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected CSharpSyntaxNode GetInMethodSyntaxNode()
		{
			CSharpSyntaxNode syntaxNode = this.SyntaxNode;
			ConstructorDeclarationSyntax constructorDeclarationSyntax = syntaxNode as ConstructorDeclarationSyntax;
			if (constructorDeclarationSyntax != null)
			{
				ConstructorInitializerSyntax constructorInitializerSyntax;
				if ((constructorInitializerSyntax = constructorDeclarationSyntax.Initializer) == null)
				{
					constructorInitializerSyntax = constructorDeclarationSyntax.Body ?? constructorDeclarationSyntax.ExpressionBody;
				}
				return constructorInitializerSyntax;
			}
			BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = syntaxNode as BaseMethodDeclarationSyntax;
			if (baseMethodDeclarationSyntax != null)
			{
				return baseMethodDeclarationSyntax.Body ?? baseMethodDeclarationSyntax.ExpressionBody;
			}
			AccessorDeclarationSyntax accessorDeclarationSyntax = syntaxNode as AccessorDeclarationSyntax;
			if (accessorDeclarationSyntax != null)
			{
				return accessorDeclarationSyntax.Body ?? accessorDeclarationSyntax.ExpressionBody;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = syntaxNode as ArrowExpressionClauseSyntax;
			if (arrowExpressionClauseSyntax != null)
			{
				return arrowExpressionClauseSyntax;
			}
			LocalFunctionStatementSyntax localFunctionStatementSyntax = syntaxNode as LocalFunctionStatementSyntax;
			if (localFunctionStatementSyntax == null)
			{
				if (!(syntaxNode is CompilationUnitSyntax))
				{
					RecordDeclarationSyntax recordDeclarationSyntax = syntaxNode as RecordDeclarationSyntax;
					if (recordDeclarationSyntax != null)
					{
						return recordDeclarationSyntax;
					}
				}
				else
				{
					SynthesizedSimpleProgramEntryPointSymbol synthesizedSimpleProgramEntryPointSymbol = this as SynthesizedSimpleProgramEntryPointSymbol;
					if (synthesizedSimpleProgramEntryPointSymbol != null)
					{
						return (CSharpSyntaxNode)synthesizedSimpleProgramEntryPointSymbol.ReturnTypeSyntax;
					}
				}
				return null;
			}
			return localFunctionStatementSyntax.Body ?? localFunctionStatementSyntax.ExpressionBody;
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x0600718C RID: 29068 RVA: 0x001A56E5 File Offset: 0x001A38E5
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal virtual Binder SignatureBinder
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x0600718D RID: 29069 RVA: 0x001A56E8 File Offset: 0x001A38E8
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal virtual Binder ParameterBinder
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x0600718E RID: 29070 RVA: 0x001A56EB File Offset: 0x001A38EB
		internal SyntaxReference SyntaxRef
		{
			get
			{
				return this.syntaxReferenceOpt;
			}
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x0600718F RID: 29071 RVA: 0x001A56F4 File Offset: 0x001A38F4
		internal virtual CSharpSyntaxNode SyntaxNode
		{
			get
			{
				if (this.syntaxReferenceOpt != null)
				{
					return (CSharpSyntaxNode)this.syntaxReferenceOpt.GetSyntax(default(CancellationToken));
				}
				return null;
			}
		}

		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x06007190 RID: 29072 RVA: 0x001A5724 File Offset: 0x001A3924
		internal SyntaxTree SyntaxTree
		{
			get
			{
				if (this.syntaxReferenceOpt != null)
				{
					return this.syntaxReferenceOpt.SyntaxTree;
				}
				return null;
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x06007191 RID: 29073 RVA: 0x001A573B File Offset: 0x001A393B
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				if (this.syntaxReferenceOpt != null)
				{
					return ImmutableArray.Create<SyntaxReference>(this.syntaxReferenceOpt);
				}
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x06007192 RID: 29074 RVA: 0x001A5756 File Offset: 0x001A3956
		public override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return SourceMethodSymbolWithAttributes.DecodeReturnTypeAnnotationAttributes(this.GetDecodedReturnTypeWellKnownAttributeData());
			}
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x06007193 RID: 29075 RVA: 0x001A5763 File Offset: 0x001A3963
		public override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				ReturnTypeWellKnownAttributeData decodedReturnTypeWellKnownAttributeData = this.GetDecodedReturnTypeWellKnownAttributeData();
				return ((decodedReturnTypeWellKnownAttributeData != null) ? decodedReturnTypeWellKnownAttributeData.NotNullIfParameterNotNull : null) ?? ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06007194 RID: 29076 RVA: 0x001A5780 File Offset: 0x001A3980
		protected virtual SourceMemberMethodSymbol BoundAttributesSource
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06007195 RID: 29077 RVA: 0x001A5783 File Offset: 0x001A3983
		protected virtual IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06007196 RID: 29078 RVA: 0x001A5786 File Offset: 0x001A3986
		IAttributeTargetSymbol IAttributeTargetSymbol.AttributesOwner
		{
			get
			{
				return this.AttributeOwner;
			}
		}

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x06007197 RID: 29079 RVA: 0x001A578E File Offset: 0x001A398E
		AttributeLocation IAttributeTargetSymbol.DefaultAttributeLocation
		{
			get
			{
				return AttributeLocation.Method;
			}
		}

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x06007198 RID: 29080 RVA: 0x001A5794 File Offset: 0x001A3994
		AttributeLocation IAttributeTargetSymbol.AllowedAttributeLocations
		{
			get
			{
				MethodKind methodKind = this.MethodKind;
				switch (methodKind)
				{
				case MethodKind.Constructor:
				case MethodKind.Destructor:
					break;
				case MethodKind.Conversion:
				case MethodKind.DelegateInvoke:
				case MethodKind.EventRaise:
					return AttributeLocation.Method | AttributeLocation.Return;
				case MethodKind.EventAdd:
				case MethodKind.EventRemove:
					return AttributeLocation.Method | AttributeLocation.Parameter | AttributeLocation.Return;
				default:
					if (methodKind == MethodKind.PropertySet)
					{
						return AttributeLocation.Method | AttributeLocation.Parameter | AttributeLocation.Return;
					}
					if (methodKind != MethodKind.StaticConstructor)
					{
						return AttributeLocation.Method | AttributeLocation.Return;
					}
					break;
				}
				return AttributeLocation.Method;
			}
		}

		// Token: 0x06007199 RID: 29081 RVA: 0x001A57E4 File Offset: 0x001A39E4
		internal virtual OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(default(SyntaxList<AttributeListSyntax>));
		}

		// Token: 0x0600719A RID: 29082 RVA: 0x001A57FF File Offset: 0x001A39FF
		internal virtual OneOrMany<SyntaxList<AttributeListSyntax>> GetReturnTypeAttributeDeclarations()
		{
			return this.GetAttributeDeclarations();
		}

		// Token: 0x0600719B RID: 29083 RVA: 0x001A5808 File Offset: 0x001A3A08
		internal MethodEarlyWellKnownAttributeData GetEarlyDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (MethodEarlyWellKnownAttributeData)customAttributesBag.EarlyDecodedWellKnownAttributeData;
		}

		// Token: 0x0600719C RID: 29084 RVA: 0x001A583C File Offset: 0x001A3A3C
		protected MethodWellKnownAttributeData GetDecodedWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetAttributesBag();
			}
			return (MethodWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x0600719D RID: 29085 RVA: 0x001A5870 File Offset: 0x001A3A70
		internal ReturnTypeWellKnownAttributeData GetDecodedReturnTypeWellKnownAttributeData()
		{
			CustomAttributesBag<CSharpAttributeData> customAttributesBag = this._lazyReturnTypeCustomAttributesBag;
			if (customAttributesBag == null || !customAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				customAttributesBag = this.GetReturnTypeAttributesBag();
			}
			return (ReturnTypeWellKnownAttributeData)customAttributesBag.DecodedWellKnownAttributeData;
		}

		// Token: 0x0600719E RID: 29086 RVA: 0x001A58A4 File Offset: 0x001A3AA4
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag()
		{
			CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
			if (lazyCustomAttributesBag != null && lazyCustomAttributesBag.IsSealed)
			{
				return lazyCustomAttributesBag;
			}
			return this.GetAttributesBag(ref this._lazyCustomAttributesBag, false);
		}

		// Token: 0x0600719F RID: 29087 RVA: 0x001A58D4 File Offset: 0x001A3AD4
		private CustomAttributesBag<CSharpAttributeData> GetReturnTypeAttributesBag()
		{
			CustomAttributesBag<CSharpAttributeData> lazyReturnTypeCustomAttributesBag = this._lazyReturnTypeCustomAttributesBag;
			if (lazyReturnTypeCustomAttributesBag != null && lazyReturnTypeCustomAttributesBag.IsSealed)
			{
				return lazyReturnTypeCustomAttributesBag;
			}
			return this.GetAttributesBag(ref this._lazyReturnTypeCustomAttributesBag, true);
		}

		// Token: 0x060071A0 RID: 29088 RVA: 0x001A5904 File Offset: 0x001A3B04
		private CustomAttributesBag<CSharpAttributeData> GetAttributesBag(ref CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag, bool forReturnType)
		{
			SourceMemberMethodSymbol boundAttributesSource = this.BoundAttributesSource;
			bool flag;
			if (boundAttributesSource != null)
			{
				CustomAttributesBag<CSharpAttributeData> customAttributesBag = (forReturnType ? boundAttributesSource.GetReturnTypeAttributesBag() : boundAttributesSource.GetAttributesBag());
				flag = Interlocked.CompareExchange<CustomAttributesBag<CSharpAttributeData>>(ref lazyCustomAttributesBag, customAttributesBag, null) == null;
			}
			else
			{
				OneOrMany<SyntaxList<AttributeListSyntax>> oneOrMany;
				AttributeLocation attributeLocation;
				if (!forReturnType)
				{
					oneOrMany = this.GetAttributeDeclarations();
					attributeLocation = AttributeLocation.None;
				}
				else
				{
					oneOrMany = this.GetReturnTypeAttributeDeclarations();
					attributeLocation = AttributeLocation.Return;
				}
				flag = base.LoadAndValidateAttributes(oneOrMany, ref lazyCustomAttributesBag, attributeLocation, false, this.SignatureBinder, null);
			}
			if (flag)
			{
				this.NoteAttributesComplete(forReturnType);
			}
			return lazyCustomAttributesBag;
		}

		// Token: 0x060071A1 RID: 29089
		protected abstract void NoteAttributesComplete(bool forReturnType);

		// Token: 0x060071A2 RID: 29090 RVA: 0x001A597B File Offset: 0x001A3B7B
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.GetAttributesBag().Attributes;
		}

		// Token: 0x060071A3 RID: 29091 RVA: 0x001A5988 File Offset: 0x001A3B88
		public override ImmutableArray<CSharpAttributeData> GetReturnTypeAttributes()
		{
			return this.GetReturnTypeAttributesBag().Attributes;
		}

		// Token: 0x060071A4 RID: 29092 RVA: 0x001A5998 File Offset: 0x001A3B98
		internal sealed override CSharpAttributeData EarlyDecodeWellKnownAttribute(ref EarlyDecodeWellKnownAttributeArguments<EarlyWellKnownAttributeBinder, NamedTypeSymbol, AttributeSyntax, AttributeLocation> arguments)
		{
			if (arguments.SymbolPart == AttributeLocation.None)
			{
				if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.ConditionalAttribute))
				{
					bool flag;
					CSharpAttributeData attribute = arguments.Binder.GetAttribute(arguments.AttributeSyntax, arguments.AttributeType, out flag);
					if (!attribute.HasErrors)
					{
						string constructorArgument = attribute.GetConstructorArgument<string>(0, SpecialType.System_String);
						arguments.GetOrCreateData<MethodEarlyWellKnownAttributeData>().AddConditionalSymbol(constructorArgument);
						if (!flag)
						{
							return attribute;
						}
					}
					return null;
				}
				CSharpAttributeData csharpAttributeData;
				ObsoleteAttributeData obsoleteAttributeData;
				if (Symbol.EarlyDecodeDeprecatedOrExperimentalOrObsoleteAttribute(ref arguments, out csharpAttributeData, out obsoleteAttributeData))
				{
					if (obsoleteAttributeData != null)
					{
						arguments.GetOrCreateData<MethodEarlyWellKnownAttributeData>().ObsoleteAttributeData = obsoleteAttributeData;
					}
					return csharpAttributeData;
				}
				if (CSharpAttributeData.IsTargetEarlyAttribute(arguments.AttributeType, arguments.AttributeSyntax, AttributeDescription.UnmanagedCallersOnlyAttribute))
				{
					arguments.GetOrCreateData<MethodEarlyWellKnownAttributeData>().UnmanagedCallersOnlyAttributePresent = true;
					return null;
				}
			}
			return base.EarlyDecodeWellKnownAttribute(ref arguments);
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x060071A5 RID: 29093 RVA: 0x001A5A53 File Offset: 0x001A3C53
		public override bool AreLocalsZeroed
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return (decodedWellKnownAttributeData == null || !decodedWellKnownAttributeData.HasSkipLocalsInitAttribute) && base.AreContainingSymbolLocalsZeroed;
			}
		}

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x060071A6 RID: 29094 RVA: 0x001A5A74 File Offset: 0x001A3C74
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = this.ContainingSymbol as SourceMemberContainerTypeSymbol;
				if (sourceMemberContainerTypeSymbol != null && !sourceMemberContainerTypeSymbol.AnyMemberHasAttributes)
				{
					return null;
				}
				CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
				if (lazyCustomAttributesBag != null && lazyCustomAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
				{
					MethodEarlyWellKnownAttributeData methodEarlyWellKnownAttributeData = (MethodEarlyWellKnownAttributeData)lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData;
					if (methodEarlyWellKnownAttributeData == null)
					{
						return null;
					}
					return methodEarlyWellKnownAttributeData.ObsoleteAttributeData;
				}
				else
				{
					if (this.syntaxReferenceOpt == null)
					{
						return null;
					}
					return ObsoleteAttributeData.Uninitialized;
				}
			}
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x001A5AD4 File Offset: 0x001A3CD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			if (this.syntaxReferenceOpt == null)
			{
				return null;
			}
			if (forceComplete)
			{
				this.GetAttributes();
			}
			CustomAttributesBag<CSharpAttributeData> lazyCustomAttributesBag = this._lazyCustomAttributesBag;
			if (lazyCustomAttributesBag == null || !lazyCustomAttributesBag.IsEarlyDecodedWellKnownAttributeDataComputed)
			{
				return UnmanagedCallersOnlyAttributeData.Uninitialized;
			}
			if (lazyCustomAttributesBag.IsDecodedWellKnownAttributeDataComputed)
			{
				MethodWellKnownAttributeData methodWellKnownAttributeData = (MethodWellKnownAttributeData)lazyCustomAttributesBag.DecodedWellKnownAttributeData;
				if (methodWellKnownAttributeData == null)
				{
					return null;
				}
				return methodWellKnownAttributeData.UnmanagedCallersOnlyAttributeData;
			}
			else
			{
				MethodEarlyWellKnownAttributeData methodEarlyWellKnownAttributeData = (MethodEarlyWellKnownAttributeData)lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData;
				if (methodEarlyWellKnownAttributeData == null || !methodEarlyWellKnownAttributeData.UnmanagedCallersOnlyAttributePresent)
				{
					return null;
				}
				return UnmanagedCallersOnlyAttributeData.AttributePresentDataNotBound;
			}
		}

		// Token: 0x060071A8 RID: 29096 RVA: 0x001A5B4C File Offset: 0x001A3D4C
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			MethodEarlyWellKnownAttributeData earlyDecodedWellKnownAttributeData = this.GetEarlyDecodedWellKnownAttributeData();
			if (earlyDecodedWellKnownAttributeData == null)
			{
				return ImmutableArray<string>.Empty;
			}
			return earlyDecodedWellKnownAttributeData.ConditionalSymbols;
		}

		// Token: 0x060071A9 RID: 29097 RVA: 0x001A5B6F File Offset: 0x001A3D6F
		internal sealed override void DecodeWellKnownAttribute(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			if (arguments.SymbolPart == AttributeLocation.None)
			{
				this.DecodeWellKnownAttributeAppliedToMethod(ref arguments);
				return;
			}
			this.DecodeWellKnownAttributeAppliedToReturnValue(ref arguments);
		}

		// Token: 0x060071AA RID: 29098 RVA: 0x001A5B88 File Offset: 0x001A3D88
		private void DecodeWellKnownAttributeAppliedToMethod(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			if (attribute.IsTargetAttribute(this, AttributeDescription.PreserveSigAttribute))
			{
				arguments.GetOrCreateData<MethodWellKnownAttributeData>().SetPreserveSignature(arguments.Index);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.MethodImplAttribute))
			{
				AttributeData.DecodeMethodImplAttribute<MethodWellKnownAttributeData, AttributeSyntax, CSharpAttributeData, AttributeLocation>(ref arguments, MessageProvider.Instance);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.DllImportAttribute))
			{
				this.DecodeDllImportAttribute(ref arguments);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SpecialNameAttribute))
			{
				arguments.GetOrCreateData<MethodWellKnownAttributeData>().HasSpecialNameAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.ExcludeFromCodeCoverageAttribute))
			{
				arguments.GetOrCreateData<MethodWellKnownAttributeData>().HasExcludeFromCodeCoverageAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.ConditionalAttribute))
			{
				this.ValidateConditionalAttribute(attribute, arguments.AttributeSyntaxOpt, bindingDiagnosticBag);
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.SuppressUnmanagedCodeSecurityAttribute))
			{
				arguments.GetOrCreateData<MethodWellKnownAttributeData>().HasSuppressUnmanagedCodeSecurityAttribute = true;
				return;
			}
			if (attribute.IsTargetAttribute(this, AttributeDescription.DynamicSecurityMethodAttribute))
			{
				arguments.GetOrCreateData<MethodWellKnownAttributeData>().HasDynamicSecurityMethodAttribute = true;
				return;
			}
			if (!this.VerifyObsoleteAttributeAppliedToMethod(ref arguments, AttributeDescription.ObsoleteAttribute) && !this.VerifyObsoleteAttributeAppliedToMethod(ref arguments, AttributeDescription.DeprecatedAttribute) && !base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.IsReadOnlyAttribute | Symbol.ReservedAttributes.IsUnmanagedAttribute | Symbol.ReservedAttributes.IsByRefLikeAttribute | Symbol.ReservedAttributes.NullableContextAttribute | Symbol.ReservedAttributes.CaseSensitiveExtensionAttribute))
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.SecurityCriticalAttribute) || attribute.IsTargetAttribute(this, AttributeDescription.SecuritySafeCriticalAttribute))
				{
					if (this.IsAsync)
					{
						bindingDiagnosticBag.Add(ErrorCode.ERR_SecurityCriticalOrSecuritySafeCriticalOnAsync, arguments.AttributeSyntaxOpt.Location, new object[] { arguments.AttributeSyntaxOpt.GetErrorDisplayName() });
						return;
					}
				}
				else
				{
					if (attribute.IsTargetAttribute(this, AttributeDescription.SkipLocalsInitAttribute))
					{
						CSharpAttributeData.DecodeSkipLocalsInitAttribute<MethodWellKnownAttributeData>(this.DeclaringCompilation, ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.DoesNotReturnAttribute))
					{
						arguments.GetOrCreateData<MethodWellKnownAttributeData>().HasDoesNotReturnAttribute = true;
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.MemberNotNullAttribute))
					{
						MessageID.IDS_FeatureMemberNotNull.CheckFeatureAvailability(bindingDiagnosticBag, arguments.AttributeSyntaxOpt, null);
						CSharpAttributeData.DecodeMemberNotNullAttribute<MethodWellKnownAttributeData>(this.ContainingType, ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.MemberNotNullWhenAttribute))
					{
						MessageID.IDS_FeatureMemberNotNull.CheckFeatureAvailability(bindingDiagnosticBag, arguments.AttributeSyntaxOpt, null);
						CSharpAttributeData.DecodeMemberNotNullWhenAttribute<MethodWellKnownAttributeData>(this.ContainingType, ref arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.ModuleInitializerAttribute))
					{
						MessageID.IDS_FeatureModuleInitializers.CheckFeatureAvailability(bindingDiagnosticBag, arguments.AttributeSyntaxOpt, null);
						this.DecodeModuleInitializerAttribute(arguments);
						return;
					}
					if (attribute.IsTargetAttribute(this, AttributeDescription.UnmanagedCallersOnlyAttribute))
					{
						this.DecodeUnmanagedCallersOnlyAttribute(ref arguments);
						return;
					}
					CSharpCompilation declaringCompilation = this.DeclaringCompilation;
					if (attribute.IsSecurityAttribute(declaringCompilation))
					{
						attribute.DecodeSecurityAttribute<MethodWellKnownAttributeData>(this, declaringCompilation, ref arguments);
					}
				}
			}
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x060071AB RID: 29099 RVA: 0x001A5DF6 File Offset: 0x001A3FF6
		internal override ImmutableArray<string> NotNullMembers
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return ImmutableArray<string>.Empty;
				}
				return decodedWellKnownAttributeData.NotNullMembers;
			}
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x060071AC RID: 29100 RVA: 0x001A5E0D File Offset: 0x001A400D
		internal override ImmutableArray<string> NotNullWhenTrueMembers
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return ImmutableArray<string>.Empty;
				}
				return decodedWellKnownAttributeData.NotNullWhenTrueMembers;
			}
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x060071AD RID: 29101 RVA: 0x001A5E24 File Offset: 0x001A4024
		internal override ImmutableArray<string> NotNullWhenFalseMembers
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				if (decodedWellKnownAttributeData == null)
				{
					return ImmutableArray<string>.Empty;
				}
				return decodedWellKnownAttributeData.NotNullWhenFalseMembers;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x060071AE RID: 29102 RVA: 0x001A5E3B File Offset: 0x001A403B
		public override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return SourceMethodSymbolWithAttributes.DecodeFlowAnalysisAttributes(this.GetDecodedWellKnownAttributeData());
			}
		}

		// Token: 0x060071AF RID: 29103 RVA: 0x001A5E48 File Offset: 0x001A4048
		private static FlowAnalysisAnnotations DecodeFlowAnalysisAttributes(MethodWellKnownAttributeData attributeData)
		{
			if (attributeData == null || !attributeData.HasDoesNotReturnAttribute)
			{
				return FlowAnalysisAnnotations.None;
			}
			return FlowAnalysisAnnotations.DoesNotReturn;
		}

		// Token: 0x060071B0 RID: 29104 RVA: 0x001A5E5C File Offset: 0x001A405C
		private bool VerifyObsoleteAttributeAppliedToMethod(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments, AttributeDescription description)
		{
			if (arguments.Attribute.IsTargetAttribute(this, description))
			{
				if (this.IsAccessor())
				{
					BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
					if (this is SourceEventAccessorSymbol)
					{
						AttributeUsageInfo attributeUsageInfo = arguments.Attribute.AttributeClass.GetAttributeUsageInfo();
						bindingDiagnosticBag.Add(ErrorCode.ERR_AttributeNotOnEventAccessor, arguments.AttributeSyntaxOpt.Name.Location, new object[]
						{
							description.FullName,
							attributeUsageInfo.GetValidTargetsErrorArgument()
						});
					}
					else
					{
						MessageID.IDS_FeatureObsoleteOnPropertyAccessor.CheckFeatureAvailability(bindingDiagnosticBag, arguments.AttributeSyntaxOpt, null);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060071B1 RID: 29105 RVA: 0x001A5EF4 File Offset: 0x001A40F4
		private void ValidateConditionalAttribute(CSharpAttributeData attribute, AttributeSyntax node, BindingDiagnosticBag diagnostics)
		{
			if (this.IsAccessor())
			{
				AttributeUsageInfo attributeUsageInfo = attribute.AttributeClass.GetAttributeUsageInfo();
				diagnostics.Add(ErrorCode.ERR_AttributeNotOnAccessor, node.Name.Location, new object[]
				{
					node.GetErrorDisplayName(),
					attributeUsageInfo.GetValidTargetsErrorArgument()
				});
				return;
			}
			if (this.ContainingType.IsInterfaceType())
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalOnInterfaceMethod, node.Location);
				return;
			}
			if (this.IsOverride)
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalOnOverride, node.Location, new object[] { this });
				return;
			}
			if (!base.CanBeReferencedByName || this.MethodKind == MethodKind.Destructor)
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalOnSpecialMethod, node.Location, new object[] { this });
				return;
			}
			if (!this.ReturnsVoid)
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalMustReturnVoid, node.Location, new object[] { this });
				return;
			}
			if (this.HasAnyOutParameter())
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalWithOutParam, node.Location, new object[] { this });
				return;
			}
			if (this != null && this.MethodKind == MethodKind.LocalFunction && !this.IsStatic)
			{
				diagnostics.Add(ErrorCode.ERR_ConditionalOnLocalFunction, node.Location, new object[] { this });
				return;
			}
			string constructorArgument = attribute.GetConstructorArgument<string>(0, SpecialType.System_String);
			if (constructorArgument == null || !SyntaxFacts.IsValidIdentifier(constructorArgument))
			{
				CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(0, node);
				diagnostics.Add(ErrorCode.ERR_BadArgumentToAttribute, attributeArgumentSyntax.Location, new object[] { node.GetErrorDisplayName() });
			}
		}

		// Token: 0x060071B2 RID: 29106 RVA: 0x001A6078 File Offset: 0x001A4278
		private bool HasAnyOutParameter()
		{
			ImmutableArray<ParameterSymbol>.Enumerator enumerator = this.Parameters.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.RefKind == RefKind.Out)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060071B3 RID: 29107 RVA: 0x001A60B4 File Offset: 0x001A42B4
		private void DecodeWellKnownAttributeAppliedToReturnValue(ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			if (attribute.IsTargetAttribute(this, AttributeDescription.MarshalAsAttribute))
			{
				MarshalAsAttributeDecoder<ReturnTypeWellKnownAttributeData, AttributeSyntax, CSharpAttributeData, AttributeLocation>.Decode(ref arguments, 8192, MessageProvider.Instance);
				return;
			}
			if (!base.ReportExplicitUseOfReservedAttributes(in arguments, Symbol.ReservedAttributes.DynamicAttribute | Symbol.ReservedAttributes.IsReadOnlyAttribute | Symbol.ReservedAttributes.IsUnmanagedAttribute | Symbol.ReservedAttributes.IsByRefLikeAttribute | Symbol.ReservedAttributes.TupleElementNamesAttribute | Symbol.ReservedAttributes.NullableAttribute | Symbol.ReservedAttributes.NativeIntegerAttribute))
			{
				if (attribute.IsTargetAttribute(this, AttributeDescription.MaybeNullAttribute))
				{
					arguments.GetOrCreateData<ReturnTypeWellKnownAttributeData>().HasMaybeNullAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullAttribute))
				{
					arguments.GetOrCreateData<ReturnTypeWellKnownAttributeData>().HasNotNullAttribute = true;
					return;
				}
				if (attribute.IsTargetAttribute(this, AttributeDescription.NotNullIfNotNullAttribute))
				{
					arguments.GetOrCreateData<ReturnTypeWellKnownAttributeData>().AddNotNullIfParameterNotNull(attribute.DecodeNotNullIfNotNullAttribute());
				}
			}
		}

		// Token: 0x060071B4 RID: 29108 RVA: 0x001A6158 File Offset: 0x001A4358
		private void DecodeDllImportAttribute([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			CSharpAttributeData attribute = arguments.Attribute;
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			bool flag = false;
			MethodSymbol methodSymbol = this.PartialImplementationPart ?? this;
			if (!methodSymbol.IsExtern || !methodSymbol.IsStatic)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_DllImportOnInvalidMethod, arguments.AttributeSyntaxOpt.Name.Location);
				flag = true;
			}
			bool flag2 = false;
			for (MethodSymbol methodSymbol2 = this; methodSymbol2 != null; methodSymbol2 = methodSymbol2.ContainingSymbol as MethodSymbol)
			{
				if (methodSymbol2.IsGenericMethod)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				NamedTypeSymbol containingType = this.ContainingType;
				if (containingType == null || !containingType.IsGenericType)
				{
					goto IL_00AD;
				}
			}
			bindingDiagnosticBag.Add(ErrorCode.ERR_DllImportOnGenericMethod, arguments.AttributeSyntaxOpt.Name.Location);
			flag = true;
			IL_00AD:
			string text = attribute.GetConstructorArgument<string>(0, SpecialType.System_String);
			if (!MetadataHelpers.IsValidMetadataIdentifier(text))
			{
				CSharpSyntaxNode attributeArgumentSyntax = attribute.GetAttributeArgumentSyntax(0, arguments.AttributeSyntaxOpt);
				bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidAttributeArgument, attributeArgumentSyntax.Location, new object[] { arguments.AttributeSyntaxOpt.GetErrorDisplayName() });
				flag = true;
				text = null;
			}
			CharSet charSet = base.GetEffectiveDefaultMarshallingCharSet() ?? 1;
			string text2 = null;
			bool flag3 = true;
			CallingConvention callingConvention = 1;
			bool flag4 = false;
			bool flag5 = false;
			bool? flag6 = default(bool?);
			bool? flag7 = default(bool?);
			int num = 1;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in attribute.CommonNamedArguments)
			{
				string key = keyValuePair.Key;
				uint num2 = global::<PrivateImplementationDetails>.ComputeStringHash(key);
				if (num2 <= 1683410592U)
				{
					if (num2 <= 610589883U)
					{
						if (num2 != 568184203U)
						{
							if (num2 == 610589883U)
							{
								if (key == "EntryPoint")
								{
									text2 = keyValuePair.Value.ValueInternal as string;
									if (!MetadataHelpers.IsValidMetadataIdentifier(text2))
									{
										bindingDiagnosticBag.Add(ErrorCode.ERR_InvalidNamedArgument, arguments.AttributeSyntaxOpt.ArgumentList.Arguments[num].Location, new object[] { keyValuePair.Key });
										flag = true;
										text2 = null;
									}
								}
							}
						}
						else if (key == "SetLastError")
						{
							flag4 = keyValuePair.Value.DecodeValue<bool>(SpecialType.System_Boolean);
						}
					}
					else if (num2 != 799797190U)
					{
						if (num2 == 1683410592U)
						{
							if (key == "BestFitMapping")
							{
								flag6 = new bool?(keyValuePair.Value.DecodeValue<bool>(SpecialType.System_Boolean));
							}
						}
					}
					else if (key == "PreserveSig")
					{
						flag3 = keyValuePair.Value.DecodeValue<bool>(SpecialType.System_Boolean);
					}
				}
				else if (num2 <= 2102564466U)
				{
					if (num2 != 1916247947U)
					{
						if (num2 == 2102564466U)
						{
							if (key == "ExactSpelling")
							{
								flag5 = keyValuePair.Value.DecodeValue<bool>(SpecialType.System_Boolean);
							}
						}
					}
					else if (key == "CharSet")
					{
						charSet = keyValuePair.Value.DecodeValue<CharSet>(SpecialType.System_Enum);
					}
				}
				else if (num2 != 2826917920U)
				{
					if (num2 == 3232400899U)
					{
						if (key == "ThrowOnUnmappableChar")
						{
							flag7 = new bool?(keyValuePair.Value.DecodeValue<bool>(SpecialType.System_Boolean));
						}
					}
				}
				else if (key == "CallingConvention")
				{
					callingConvention = keyValuePair.Value.DecodeValue<CallingConvention>(SpecialType.System_Enum);
				}
				num++;
			}
			if (!flag)
			{
				arguments.GetOrCreateData<MethodWellKnownAttributeData>().SetDllImport(arguments.Index, text, text2 ?? this.Name, DllImportData.MakeFlags(flag5, charSet, flag4, callingConvention, flag6, flag7), flag3);
			}
		}

		// Token: 0x060071B5 RID: 29109 RVA: 0x001A6554 File Offset: 0x001A4754
		private void DecodeModuleInitializerAttribute([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			if (this.MethodKind != MethodKind.Ordinary)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ModuleInitializerMethodMustBeOrdinary, arguments.AttributeSyntaxOpt.Location);
				return;
			}
			bool flag = false;
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(bindingDiagnosticBag, this.ContainingAssembly);
			if (!AccessCheck.IsSymbolAccessible(this, this.ContainingAssembly, ref compoundUseSiteInfo))
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ModuleInitializerMethodMustBeAccessibleOutsideTopLevelType, arguments.AttributeSyntaxOpt.Location, new object[] { this.Name });
				flag = true;
			}
			bindingDiagnosticBag.Add(arguments.AttributeSyntaxOpt, compoundUseSiteInfo);
			if (!this.IsStatic || this.ParameterCount > 0 || !this.ReturnsVoid)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ModuleInitializerMethodMustBeStaticParameterlessVoid, arguments.AttributeSyntaxOpt.Location, new object[] { this.Name });
				flag = true;
			}
			if (this.IsGenericMethod || this.ContainingType.IsGenericType)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ModuleInitializerMethodAndContainingTypesMustNotBeGeneric, arguments.AttributeSyntaxOpt.Location, new object[] { this.Name });
				flag = true;
			}
			MethodEarlyWellKnownAttributeData methodEarlyWellKnownAttributeData = this._lazyCustomAttributesBag.EarlyDecodedWellKnownAttributeData as MethodEarlyWellKnownAttributeData;
			if (methodEarlyWellKnownAttributeData != null && methodEarlyWellKnownAttributeData.UnmanagedCallersOnlyAttributePresent)
			{
				bindingDiagnosticBag.Add(ErrorCode.ERR_ModuleInitializerCannotBeUnmanagedCallersOnly, arguments.AttributeSyntaxOpt.Location);
				flag = true;
			}
			if (!flag && !this.CallsAreOmitted(arguments.AttributeSyntaxOpt.SyntaxTree))
			{
				this.DeclaringCompilation.AddModuleInitializerMethod(this);
			}
		}

		// Token: 0x060071B6 RID: 29110 RVA: 0x001A66C4 File Offset: 0x001A48C4
		private void DecodeUnmanagedCallersOnlyAttribute([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref DecodeWellKnownAttributeArguments<AttributeSyntax, CSharpAttributeData, AttributeLocation> arguments)
		{
			BindingDiagnosticBag bindingDiagnosticBag = (BindingDiagnosticBag)arguments.Diagnostics;
			arguments.GetOrCreateData<MethodWellKnownAttributeData>().UnmanagedCallersOnlyAttributeData = SourceMethodSymbolWithAttributes.<DecodeUnmanagedCallersOnlyAttribute>g__DecodeUnmanagedCallersOnlyAttributeData|66_1(this, arguments.Attribute, arguments.AttributeSyntaxOpt.Location, bindingDiagnosticBag);
			base.CheckAndReportValidUnmanagedCallersOnlyTarget(arguments.AttributeSyntaxOpt.Name.Location, bindingDiagnosticBag);
			CSharpSyntaxNode csharpSyntaxNode = this.ExtractReturnTypeSyntax();
			if (csharpSyntaxNode == CSharpSyntaxTree.Dummy.GetRoot(default(CancellationToken)))
			{
				return;
			}
			SourceMethodSymbolWithAttributes.<DecodeUnmanagedCallersOnlyAttribute>g__checkAndReportManagedTypes|66_0(base.ReturnType, csharpSyntaxNode, false, bindingDiagnosticBag);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				SourceMethodSymbolWithAttributes.<DecodeUnmanagedCallersOnlyAttribute>g__checkAndReportManagedTypes|66_0(parameterSymbol.Type, parameterSymbol.GetNonNullSyntaxNode(), true, bindingDiagnosticBag);
			}
		}

		// Token: 0x060071B7 RID: 29111 RVA: 0x001A6780 File Offset: 0x001A4980
		internal sealed override void PostDecodeWellKnownAttributes(ImmutableArray<CSharpAttributeData> boundAttributes, ImmutableArray<AttributeSyntax> allAttributeSyntaxNodes, BindingDiagnosticBag diagnostics, AttributeLocation symbolPart, WellKnownAttributeData decodedData)
		{
			if (symbolPart != AttributeLocation.Return)
			{
				NamedTypeSymbol namedTypeSymbol = this.ContainingSymbol as NamedTypeSymbol;
				if (namedTypeSymbol != null && namedTypeSymbol.IsComImport && namedTypeSymbol.TypeKind == TypeKind.Class)
				{
					MethodKind methodKind = this.MethodKind;
					if (methodKind == MethodKind.Constructor || methodKind == MethodKind.StaticConstructor)
					{
						if (!this.IsImplicitlyDeclared)
						{
							diagnostics.Add(ErrorCode.ERR_ComImportWithUserCtor, this.Locations[0]);
						}
					}
					else if (!this.IsAbstract && !this.IsExtern)
					{
						diagnostics.Add(ErrorCode.ERR_ComImportWithImpl, this.Locations[0], new object[] { this, this.ContainingType });
					}
				}
				if (this.IsExtern && !this.IsAbstract && !this.IsPartialMethod() && this.GetInMethodSyntaxNode() == null && boundAttributes.IsEmpty && !this.ContainingType.IsComImport)
				{
					ErrorCode errorCode = ((this.MethodKind == MethodKind.Constructor || this.MethodKind == MethodKind.StaticConstructor) ? ErrorCode.WRN_ExternCtorNoImplementation : ErrorCode.WRN_ExternMethodNoImplementation);
					diagnostics.Add(errorCode, this.Locations[0], new object[] { this });
				}
			}
			base.PostDecodeWellKnownAttributes(boundAttributes, allAttributeSyntaxNodes, diagnostics, symbolPart, decodedData);
		}

		// Token: 0x060071B8 RID: 29112 RVA: 0x001A68B4 File Offset: 0x001A4AB4
		protected void AsyncMethodChecks(BindingDiagnosticBag diagnostics)
		{
			if (this.IsAsync)
			{
				Location location = this.Locations[0];
				bool flag = false;
				if (this.RefKind != RefKind.None)
				{
					CSharpSyntaxNode syntaxNode = this.SyntaxNode;
					MethodDeclarationSyntax methodDeclarationSyntax = syntaxNode as MethodDeclarationSyntax;
					TypeSyntax typeSyntax;
					if (methodDeclarationSyntax != null)
					{
						TypeSyntax returnType = methodDeclarationSyntax.ReturnType;
						typeSyntax = returnType;
					}
					else
					{
						LocalFunctionStatementSyntax localFunctionStatementSyntax = syntaxNode as LocalFunctionStatementSyntax;
						if (localFunctionStatementSyntax == null)
						{
							throw ExceptionUtilities.UnexpectedValue(syntaxNode);
						}
						TypeSyntax returnType2 = localFunctionStatementSyntax.ReturnType;
						typeSyntax = returnType2;
					}
					SourceMethodSymbol.ReportBadRefToken(typeSyntax, diagnostics);
					flag = true;
				}
				else if (base.ReturnType.IsBadAsyncReturn(this.DeclaringCompilation))
				{
					diagnostics.Add(ErrorCode.ERR_BadAsyncReturn, location);
					flag = true;
				}
				for (NamedTypeSymbol namedTypeSymbol = this.ContainingType; namedTypeSymbol != null; namedTypeSymbol = namedTypeSymbol.ContainingType)
				{
					SourceNamedTypeSymbol sourceNamedTypeSymbol = namedTypeSymbol as SourceNamedTypeSymbol;
					if (sourceNamedTypeSymbol != null && sourceNamedTypeSymbol.HasSecurityCriticalAttributes)
					{
						diagnostics.Add(ErrorCode.ERR_SecurityCriticalOrSecuritySafeCriticalOnAsyncInClassOrStruct, location);
						flag = true;
						break;
					}
				}
				if ((this.ImplementationAttributes & 32) != null)
				{
					diagnostics.Add(ErrorCode.ERR_SynchronizedAsyncMethod, location);
					flag = true;
				}
				if (!flag)
				{
					base.ReportAsyncParameterErrors(diagnostics, location);
				}
				NamedTypeSymbol wellKnownType = this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Collections_Generic_IAsyncEnumerable_T);
				if (base.ReturnType.OriginalDefinition.Equals(wellKnownType) && this.GetInMethodSyntaxNode() != null)
				{
					NamedTypeSymbol cancellationTokenType = this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Threading_CancellationToken);
					int num = this.Parameters.Count<ParameterSymbol>((ParameterSymbol p) => p.IsSourceParameterWithEnumeratorCancellationAttribute());
					if (num == 0 && base.ParameterTypesWithAnnotations.Any<TypeWithAnnotations>((TypeWithAnnotations p) => p.Type.Equals(cancellationTokenType)))
					{
						diagnostics.Add(ErrorCode.WRN_UndecoratedCancellationTokenParameter, location, new object[] { this });
					}
					if (num > 1)
					{
						diagnostics.Add(ErrorCode.ERR_MultipleEnumeratorCancellationAttributes, location);
					}
				}
			}
		}

		// Token: 0x060071B9 RID: 29113 RVA: 0x001A6A88 File Offset: 0x001A4C88
		private static FlowAnalysisAnnotations DecodeReturnTypeAnnotationAttributes(ReturnTypeWellKnownAttributeData attributeData)
		{
			FlowAnalysisAnnotations flowAnalysisAnnotations = FlowAnalysisAnnotations.None;
			if (attributeData != null)
			{
				if (attributeData.HasMaybeNullAttribute)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.MaybeNull;
				}
				if (attributeData.HasNotNullAttribute)
				{
					flowAnalysisAnnotations |= FlowAnalysisAnnotations.NotNull;
				}
			}
			return flowAnalysisAnnotations;
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x060071BA RID: 29114 RVA: 0x001A6AB5 File Offset: 0x001A4CB5
		public sealed override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x060071BB RID: 29115 RVA: 0x001A6AB8 File Offset: 0x001A4CB8
		internal sealed override bool HasRuntimeSpecialName
		{
			get
			{
				return base.HasRuntimeSpecialName || this.IsVtableGapInterfaceMethod();
			}
		}

		// Token: 0x060071BC RID: 29116 RVA: 0x001A6ACA File Offset: 0x001A4CCA
		private bool IsVtableGapInterfaceMethod()
		{
			return this.ContainingType.IsInterface && ModuleExtensions.GetVTableGapSize(this.MetadataName) > 0;
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x060071BD RID: 29117 RVA: 0x001A6AEC File Offset: 0x001A4CEC
		internal override bool HasSpecialName
		{
			get
			{
				switch (this.MethodKind)
				{
				case MethodKind.Constructor:
				case MethodKind.Conversion:
				case MethodKind.EventAdd:
				case MethodKind.EventRemove:
				case MethodKind.UserDefinedOperator:
				case MethodKind.PropertyGet:
				case MethodKind.PropertySet:
				case MethodKind.StaticConstructor:
					return true;
				}
				if (this.IsVtableGapInterfaceMethod())
				{
					return true;
				}
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasSpecialNameAttribute;
			}
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x060071BE RID: 29118 RVA: 0x001A6B60 File Offset: 0x001A4D60
		internal sealed override bool IsDirectlyExcludedFromCodeCoverage
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasExcludeFromCodeCoverageAttribute;
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x060071BF RID: 29119 RVA: 0x001A6B74 File Offset: 0x001A4D74
		internal override bool RequiresSecurityObject
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasDynamicSecurityMethodAttribute;
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x060071C0 RID: 29120 RVA: 0x001A6B94 File Offset: 0x001A4D94
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				return decodedWellKnownAttributeData != null && decodedWellKnownAttributeData.HasDeclarativeSecurity;
			}
		}

		// Token: 0x060071C1 RID: 29121 RVA: 0x001A6BB4 File Offset: 0x001A4DB4
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			CustomAttributesBag<CSharpAttributeData> attributesBag = this.GetAttributesBag();
			MethodWellKnownAttributeData methodWellKnownAttributeData = (MethodWellKnownAttributeData)attributesBag.DecodedWellKnownAttributeData;
			if (methodWellKnownAttributeData != null)
			{
				SecurityWellKnownAttributeData securityInformation = methodWellKnownAttributeData.SecurityInformation;
				if (securityInformation != null)
				{
					return securityInformation.GetSecurityAttributes<CSharpAttributeData>(attributesBag.Attributes);
				}
			}
			return SpecializedCollections.EmptyEnumerable<SecurityAttribute>();
		}

		// Token: 0x060071C2 RID: 29122 RVA: 0x001A6BF4 File Offset: 0x001A4DF4
		public override DllImportData GetDllImportData()
		{
			MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
			if (decodedWellKnownAttributeData == null)
			{
				return null;
			}
			return decodedWellKnownAttributeData.DllImportPlatformInvokeData;
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x060071C3 RID: 29123 RVA: 0x001A6C14 File Offset: 0x001A4E14
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				ReturnTypeWellKnownAttributeData decodedReturnTypeWellKnownAttributeData = this.GetDecodedReturnTypeWellKnownAttributeData();
				if (decodedReturnTypeWellKnownAttributeData == null)
				{
					return null;
				}
				return decodedReturnTypeWellKnownAttributeData.MarshallingInformation;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x060071C4 RID: 29124 RVA: 0x001A6C34 File Offset: 0x001A4E34
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				MethodWellKnownAttributeData decodedWellKnownAttributeData = this.GetDecodedWellKnownAttributeData();
				MethodImplAttributes methodImplAttributes = ((decodedWellKnownAttributeData != null) ? decodedWellKnownAttributeData.MethodImplAttributes : 0);
				if (this.ContainingType.IsComImport && this.MethodKind == MethodKind.Constructor)
				{
					methodImplAttributes |= 4099;
				}
				return methodImplAttributes;
			}
		}

		// Token: 0x060071C5 RID: 29125 RVA: 0x001A6C74 File Offset: 0x001A4E74
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static void <DecodeUnmanagedCallersOnlyAttribute>g__checkAndReportManagedTypes|66_0(TypeSymbol type, SyntaxNode syntax, bool isParam, BindingDiagnosticBag diagnostics)
		{
			ManagedKind managedKindNoUseSiteDiagnostics = type.ManagedKindNoUseSiteDiagnostics;
			if (managedKindNoUseSiteDiagnostics - ManagedKind.Unmanaged <= 1)
			{
				return;
			}
			if (managedKindNoUseSiteDiagnostics != ManagedKind.Managed)
			{
				throw ExceptionUtilities.UnexpectedValue(type.ManagedKindNoUseSiteDiagnostics);
			}
			diagnostics.Add(ErrorCode.ERR_CannotUseManagedTypeInUnmanagedCallersOnly, syntax.Location, new object[]
			{
				type,
				(isParam ? MessageID.IDS_Parameter : MessageID.IDS_Return).Localize()
			});
		}

		// Token: 0x060071C6 RID: 29126 RVA: 0x001A6CE0 File Offset: 0x001A4EE0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[CompilerGenerated]
		internal static UnmanagedCallersOnlyAttributeData <DecodeUnmanagedCallersOnlyAttribute>g__DecodeUnmanagedCallersOnlyAttributeData|66_1(SourceMethodSymbolWithAttributes @this, CSharpAttributeData attribute, Location location, BindingDiagnosticBag diagnostics)
		{
			ImmutableHashSet<INamedTypeSymbolInternal> immutableHashSet = null;
			if (!attribute.CommonNamedArguments.IsDefaultOrEmpty)
			{
				NamedTypeSymbol wellKnownType = @this.DeclaringCompilation.GetWellKnownType(WellKnownType.System_Type);
				foreach (KeyValuePair<string, TypedConstant> keyValuePair in attribute.CommonNamedArguments)
				{
					string text;
					TypedConstant typedConstant;
					keyValuePair.Deconstruct<string, TypedConstant>(out text, out typedConstant);
					string text2 = text;
					TypedConstant typedConstant2 = typedConstant;
					bool flag = attribute.AttributeClass.GetMembers(text2).Any(delegate(Symbol m, NamedTypeSymbol systemType)
					{
						FieldSymbol fieldSymbol = m as FieldSymbol;
						if (fieldSymbol != null)
						{
							ArrayTypeSymbol arrayTypeSymbol = fieldSymbol.Type as ArrayTypeSymbol;
							if (arrayTypeSymbol != null)
							{
								NamedTypeSymbol namedTypeSymbol = arrayTypeSymbol.ElementType as NamedTypeSymbol;
								if (namedTypeSymbol != null)
								{
									return namedTypeSymbol.Equals(systemType, TypeCompareKind.ConsiderEverything);
								}
							}
						}
						return false;
					}, wellKnownType);
					ValueTuple<bool, ImmutableHashSet<INamedTypeSymbolInternal>> valueTuple = MethodSymbol.TryDecodeUnmanagedCallersOnlyCallConvsField(text2, typedConstant2, flag, location, diagnostics);
					if (valueTuple.Item1)
					{
						immutableHashSet = valueTuple.Item2;
					}
				}
			}
			return UnmanagedCallersOnlyAttributeData.Create(immutableHashSet);
		}

		// Token: 0x04001BB2 RID: 7090
		private CustomAttributesBag<CSharpAttributeData> _lazyCustomAttributesBag;

		// Token: 0x04001BB3 RID: 7091
		private CustomAttributesBag<CSharpAttributeData> _lazyReturnTypeCustomAttributesBag;

		// Token: 0x04001BB4 RID: 7092
		protected readonly SyntaxReference syntaxReferenceOpt;
	}
}
