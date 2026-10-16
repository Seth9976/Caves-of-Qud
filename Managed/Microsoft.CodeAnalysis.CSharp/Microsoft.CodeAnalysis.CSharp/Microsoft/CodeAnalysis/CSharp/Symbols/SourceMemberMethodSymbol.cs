using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000542 RID: 1346
	internal abstract class SourceMemberMethodSymbol : SourceMethodSymbolWithAttributes, IAttributeTargetSymbol
	{
		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06007145 RID: 28997 RVA: 0x001A4982 File Offset: 0x001A2B82
		internal ImmutableArray<Diagnostic> Diagnostics
		{
			get
			{
				return this._cachedDiagnostics;
			}
		}

		// Token: 0x06007146 RID: 28998 RVA: 0x001A498A File Offset: 0x001A2B8A
		internal ImmutableArray<Diagnostic> SetDiagnostics(ImmutableArray<Diagnostic> newSet, out bool diagsWritten)
		{
			diagsWritten = ImmutableInterlocked.InterlockedInitialize<Diagnostic>(ref this._cachedDiagnostics, newSet);
			return this._cachedDiagnostics;
		}

		// Token: 0x06007147 RID: 28999 RVA: 0x001A49A0 File Offset: 0x001A2BA0
		protected SourceMemberMethodSymbol(NamedTypeSymbol containingType, SyntaxReference syntaxReferenceOpt, Location location, bool isIterator)
			: this(containingType, syntaxReferenceOpt, ImmutableArray.Create<Location>(location), isIterator)
		{
		}

		// Token: 0x06007148 RID: 29000 RVA: 0x001A49B2 File Offset: 0x001A2BB2
		protected SourceMemberMethodSymbol(NamedTypeSymbol containingType, SyntaxReference syntaxReferenceOpt, ImmutableArray<Location> locations, bool isIterator)
			: base(syntaxReferenceOpt)
		{
			this._containingType = containingType;
			this.locations = locations;
			if (isIterator)
			{
				this._lazyIteratorElementType = TypeWithAnnotations.Boxed.Sentinel;
			}
		}

		// Token: 0x06007149 RID: 29001 RVA: 0x001A49D8 File Offset: 0x001A2BD8
		protected void CheckEffectiveAccessibility(TypeWithAnnotations returnType, ImmutableArray<ParameterSymbol> parameters, BindingDiagnosticBag diagnostics)
		{
			if (this.DeclaredAccessibility <= Accessibility.Private || this.MethodKind == MethodKind.ExplicitInterfaceImplementation)
			{
				return;
			}
			ErrorCode errorCode = ((this.MethodKind == MethodKind.Conversion || this.MethodKind == MethodKind.UserDefinedOperator) ? ErrorCode.ERR_BadVisOpReturn : ErrorCode.ERR_BadVisReturnType);
			CompoundUseSiteInfo<AssemblySymbol> compoundUseSiteInfo = new CompoundUseSiteInfo<AssemblySymbol>(diagnostics, this.ContainingAssembly);
			if (!this.IsNoMoreVisibleThan(returnType, ref compoundUseSiteInfo))
			{
				diagnostics.Add(errorCode, this.Locations[0], new object[] { this, returnType.Type });
			}
			errorCode = ((this.MethodKind == MethodKind.Conversion || this.MethodKind == MethodKind.UserDefinedOperator) ? ErrorCode.ERR_BadVisOpParam : ErrorCode.ERR_BadVisParamType);
			foreach (ParameterSymbol parameterSymbol in parameters)
			{
				if (!parameterSymbol.TypeWithAnnotations.IsAtLeastAsVisibleAs(this, ref compoundUseSiteInfo))
				{
					diagnostics.Add(errorCode, this.Locations[0], new object[] { this, parameterSymbol.Type });
				}
			}
			diagnostics.Add(this.Locations[0], compoundUseSiteInfo);
		}

		// Token: 0x0600714A RID: 29002 RVA: 0x001A4AE3 File Offset: 0x001A2CE3
		protected void MakeFlags(MethodKind methodKind, DeclarationModifiers declarationModifiers, bool returnsVoid, bool isExtensionMethod, bool isNullableAnalysisEnabled, bool isMetadataVirtualIgnoringModifiers = false)
		{
			this.DeclarationModifiers = declarationModifiers;
			this.flags = new SourceMemberMethodSymbol.Flags(methodKind, declarationModifiers, returnsVoid, isExtensionMethod, isNullableAnalysisEnabled, isMetadataVirtualIgnoringModifiers);
		}

		// Token: 0x0600714B RID: 29003 RVA: 0x001A4B00 File Offset: 0x001A2D00
		protected void SetReturnsVoid(bool returnsVoid)
		{
			this.flags.SetReturnsVoid(returnsVoid);
		}

		// Token: 0x0600714C RID: 29004
		protected abstract void MethodChecks(BindingDiagnosticBag diagnostics);

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x0600714D RID: 29005 RVA: 0x001A4B0E File Offset: 0x001A2D0E
		protected virtual object MethodChecksLockObject
		{
			get
			{
				return this.syntaxReferenceOpt;
			}
		}

		// Token: 0x0600714E RID: 29006 RVA: 0x001A4B18 File Offset: 0x001A2D18
		protected void LazyMethodChecks()
		{
			if (!this.state.HasComplete(CompletionPart.StartMemberChecks))
			{
				object methodChecksLockObject = this.MethodChecksLockObject;
				lock (methodChecksLockObject)
				{
					if (this.state.NotePartComplete(CompletionPart.SynthesizedExplicitImplementations))
					{
						BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
						try
						{
							this.MethodChecks(instance);
							this.AddDeclarationDiagnostics(instance);
						}
						finally
						{
							this.state.NotePartComplete(CompletionPart.StartMemberChecks);
							instance.Free();
						}
					}
				}
			}
		}

		// Token: 0x0600714F RID: 29007 RVA: 0x001A4BB0 File Offset: 0x001A2DB0
		protected virtual void LazyAsyncMethodChecks(CancellationToken cancellationToken)
		{
			this.state.NotePartComplete(CompletionPart.Members);
			this.state.NotePartComplete(CompletionPart.TypeMembers);
		}

		// Token: 0x17001E77 RID: 7799
		// (get) Token: 0x06007150 RID: 29008 RVA: 0x001A4BD4 File Offset: 0x001A2DD4
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001E78 RID: 7800
		// (get) Token: 0x06007151 RID: 29009 RVA: 0x001A4BDC File Offset: 0x001A2DDC
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001E79 RID: 7801
		// (get) Token: 0x06007152 RID: 29010 RVA: 0x001A4BE4 File Offset: 0x001A2DE4
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E7A RID: 7802
		// (get) Token: 0x06007153 RID: 29011 RVA: 0x001A4BE8 File Offset: 0x001A2DE8
		public override bool ReturnsVoid
		{
			get
			{
				bool flag;
				this.flags.TryGetReturnsVoid(out flag);
				return flag;
			}
		}

		// Token: 0x17001E7B RID: 7803
		// (get) Token: 0x06007154 RID: 29012 RVA: 0x001A4C04 File Offset: 0x001A2E04
		public sealed override MethodKind MethodKind
		{
			get
			{
				return this.flags.MethodKind;
			}
		}

		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x06007155 RID: 29013 RVA: 0x001A4C11 File Offset: 0x001A2E11
		public override bool IsExtensionMethod
		{
			get
			{
				return this.flags.IsExtensionMethod;
			}
		}

		// Token: 0x06007156 RID: 29014 RVA: 0x001A4C20 File Offset: 0x001A2E20
		internal override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			if (this.IsExplicitInterfaceImplementation && this._containingType.IsInterface)
			{
				return false;
			}
			if (!this.IsOverride)
			{
				return this.IsMetadataVirtual(ignoreInterfaceImplementationChanges);
			}
			bool flag;
			return this.RequiresExplicitOverride(out flag);
		}

		// Token: 0x06007157 RID: 29015 RVA: 0x001A4C5C File Offset: 0x001A2E5C
		internal override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return this.flags.IsMetadataVirtual(ignoreInterfaceImplementationChanges);
		}

		// Token: 0x06007158 RID: 29016 RVA: 0x001A4C6A File Offset: 0x001A2E6A
		internal void EnsureMetadataVirtual()
		{
			this.flags.EnsureMetadataVirtual();
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06007159 RID: 29017 RVA: 0x001A4C77 File Offset: 0x001A2E77
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return ModifierUtils.EffectiveAccessibility(this.DeclarationModifiers);
			}
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x0600715A RID: 29018 RVA: 0x001A4C84 File Offset: 0x001A2E84
		internal bool HasExternModifier
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Extern) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x0600715B RID: 29019 RVA: 0x001A4C95 File Offset: 0x001A2E95
		public override bool IsExtern
		{
			get
			{
				return this.HasExternModifier;
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x0600715C RID: 29020 RVA: 0x001A4C9D File Offset: 0x001A2E9D
		public sealed override bool IsSealed
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Sealed) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x0600715D RID: 29021 RVA: 0x001A4CAA File Offset: 0x001A2EAA
		public sealed override bool IsAbstract
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Abstract) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x0600715E RID: 29022 RVA: 0x001A4CB7 File Offset: 0x001A2EB7
		public sealed override bool IsOverride
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Override) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x0600715F RID: 29023 RVA: 0x001A4CC8 File Offset: 0x001A2EC8
		internal bool IsPartial
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Partial) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x06007160 RID: 29024 RVA: 0x001A4CD9 File Offset: 0x001A2ED9
		public sealed override bool IsVirtual
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Virtual) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E85 RID: 7813
		// (get) Token: 0x06007161 RID: 29025 RVA: 0x001A4CEA File Offset: 0x001A2EEA
		internal bool IsNew
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.New) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x06007162 RID: 29026 RVA: 0x001A4CF7 File Offset: 0x001A2EF7
		public sealed override bool IsStatic
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Static) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06007163 RID: 29027 RVA: 0x001A4D04 File Offset: 0x001A2F04
		internal bool IsUnsafe
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Unsafe) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E88 RID: 7816
		// (get) Token: 0x06007164 RID: 29028 RVA: 0x001A4D15 File Offset: 0x001A2F15
		public sealed override bool IsAsync
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.Async) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E89 RID: 7817
		// (get) Token: 0x06007165 RID: 29029 RVA: 0x001A4D26 File Offset: 0x001A2F26
		internal override bool IsDeclaredReadOnly
		{
			get
			{
				return (this.DeclarationModifiers & DeclarationModifiers.ReadOnly) > DeclarationModifiers.None;
			}
		}

		// Token: 0x17001E8A RID: 7818
		// (get) Token: 0x06007166 RID: 29030 RVA: 0x001A4D37 File Offset: 0x001A2F37
		internal override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E8B RID: 7819
		// (get) Token: 0x06007167 RID: 29031 RVA: 0x001A4D3C File Offset: 0x001A2F3C
		internal sealed override CallingConvention CallingConvention
		{
			get
			{
				CallingConvention callingConvention = (this.IsVararg ? CallingConvention.ExtraArguments : CallingConvention.Default);
				if (this.IsGenericMethod)
				{
					callingConvention |= CallingConvention.Generic;
				}
				if (!this.IsStatic)
				{
					callingConvention |= CallingConvention.HasThis;
				}
				return callingConvention;
			}
		}

		// Token: 0x17001E8C RID: 7820
		// (get) Token: 0x06007168 RID: 29032 RVA: 0x001A4D74 File Offset: 0x001A2F74
		[TupleElementNames(new string[] { "blockBody", "arrowBody" })]
		internal ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax> Bodies
		{
			[return: TupleElementNames(new string[] { "blockBody", "arrowBody" })]
			get
			{
				CSharpSyntaxNode syntaxNode = this.SyntaxNode;
				BaseMethodDeclarationSyntax baseMethodDeclarationSyntax = syntaxNode as BaseMethodDeclarationSyntax;
				if (baseMethodDeclarationSyntax != null)
				{
					return new ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax>(baseMethodDeclarationSyntax.Body, baseMethodDeclarationSyntax.ExpressionBody);
				}
				AccessorDeclarationSyntax accessorDeclarationSyntax = syntaxNode as AccessorDeclarationSyntax;
				if (accessorDeclarationSyntax != null)
				{
					return new ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax>(accessorDeclarationSyntax.Body, accessorDeclarationSyntax.ExpressionBody);
				}
				ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = syntaxNode as ArrowExpressionClauseSyntax;
				if (arrowExpressionClauseSyntax != null)
				{
					return new ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax>(null, arrowExpressionClauseSyntax);
				}
				BlockSyntax blockSyntax = syntaxNode as BlockSyntax;
				if (blockSyntax == null)
				{
					return new ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax>(null, null);
				}
				return new ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax>(blockSyntax, null);
			}
		}

		// Token: 0x06007169 RID: 29033 RVA: 0x001A4DF4 File Offset: 0x001A2FF4
		private Binder TryGetInMethodBinder(BinderFactory binderFactoryOpt = null)
		{
			CSharpSyntaxNode inMethodSyntaxNode = base.GetInMethodSyntaxNode();
			if (inMethodSyntaxNode == null)
			{
				return null;
			}
			return (binderFactoryOpt ?? this.DeclaringCompilation.GetBinderFactory(inMethodSyntaxNode.SyntaxTree, false)).GetBinder(inMethodSyntaxNode, null, null);
		}

		// Token: 0x0600716A RID: 29034 RVA: 0x001A4E2C File Offset: 0x001A302C
		internal virtual ExecutableCodeBinder TryGetBodyBinder(BinderFactory binderFactoryOpt = null, bool ignoreAccessibility = false)
		{
			Binder binder = this.TryGetInMethodBinder(binderFactoryOpt);
			if (binder != null)
			{
				return new ExecutableCodeBinder(this.SyntaxNode, this, binder.WithAdditionalFlags(ignoreAccessibility ? BinderFlags.IgnoreAccessibility : BinderFlags.None), null);
			}
			return null;
		}

		// Token: 0x17001E8D RID: 7821
		// (get) Token: 0x0600716B RID: 29035 RVA: 0x001A4E64 File Offset: 0x001A3064
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return this.locations;
			}
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x001A4E6C File Offset: 0x001A306C
		public override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			ref string ptr = (ref expandIncludes ? ref this.lazyExpandedDocComment : ref this.lazyDocComment);
			return SourceDocumentationCommentUtils.GetAndCacheDocumentationComment(this, expandIncludes, ref ptr);
		}

		// Token: 0x17001E8E RID: 7822
		// (get) Token: 0x0600716D RID: 29037 RVA: 0x001A4E93 File Offset: 0x001A3093
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x17001E8F RID: 7823
		// (get) Token: 0x0600716E RID: 29038 RVA: 0x001A4E9A File Offset: 0x001A309A
		public sealed override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return base.GetTypeParametersAsTypeArguments();
			}
		}

		// Token: 0x17001E90 RID: 7824
		// (get) Token: 0x0600716F RID: 29039 RVA: 0x001A4EA4 File Offset: 0x001A30A4
		public sealed override int Arity
		{
			get
			{
				return this.TypeParameters.Length;
			}
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x001A4EBF File Offset: 0x001A30BF
		internal sealed override bool TryGetThisParameter(out ParameterSymbol thisParameter)
		{
			thisParameter = this._lazyThisParameter;
			if (thisParameter != null || this.IsStatic)
			{
				return true;
			}
			Interlocked.CompareExchange<ParameterSymbol>(ref this._lazyThisParameter, new ThisParameterSymbol(this), null);
			thisParameter = this._lazyThisParameter;
			return true;
		}

		// Token: 0x17001E91 RID: 7825
		// (get) Token: 0x06007171 RID: 29041 RVA: 0x001A4EF4 File Offset: 0x001A30F4
		// (set) Token: 0x06007172 RID: 29042 RVA: 0x001A4F1A File Offset: 0x001A311A
		internal override TypeWithAnnotations IteratorElementTypeWithAnnotations
		{
			get
			{
				TypeWithAnnotations.Boxed lazyIteratorElementType = this._lazyIteratorElementType;
				if (lazyIteratorElementType == null)
				{
					return default(TypeWithAnnotations);
				}
				return lazyIteratorElementType.Value;
			}
			set
			{
				Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyIteratorElementType, new TypeWithAnnotations.Boxed(value), TypeWithAnnotations.Boxed.Sentinel);
			}
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06007173 RID: 29043 RVA: 0x001A4F33 File Offset: 0x001A3133
		internal override bool IsIterator
		{
			get
			{
				return this._lazyIteratorElementType != null;
			}
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x06007174 RID: 29044 RVA: 0x001A4F3E File Offset: 0x001A313E
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x06007175 RID: 29045 RVA: 0x001A4F45 File Offset: 0x001A3145
		internal sealed override OverriddenOrHiddenMembersResult OverriddenOrHiddenMembers
		{
			get
			{
				this.LazyMethodChecks();
				if (this._lazyOverriddenOrHiddenMembers == null)
				{
					Interlocked.CompareExchange<OverriddenOrHiddenMembersResult>(ref this._lazyOverriddenOrHiddenMembers, this.MakeOverriddenOrHiddenMembers(), null);
				}
				return this._lazyOverriddenOrHiddenMembers;
			}
		}

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x06007176 RID: 29046 RVA: 0x001A4F6E File Offset: 0x001A316E
		internal sealed override bool RequiresCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007177 RID: 29047 RVA: 0x001A4F71 File Offset: 0x001A3171
		internal sealed override bool HasComplete(CompletionPart part)
		{
			return this.state.HasComplete(part);
		}

		// Token: 0x06007178 RID: 29048 RVA: 0x001A4F80 File Offset: 0x001A3180
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				CompletionPart nextIncompletePart = this.state.NextIncompletePart;
				if (nextIncompletePart <= CompletionPart.Members)
				{
					switch (nextIncompletePart)
					{
					case CompletionPart.None:
						return;
					case CompletionPart.Attributes:
						this.GetAttributes();
						break;
					case CompletionPart.ReturnTypeAttributes:
						this.GetReturnTypeAttributes();
						break;
					case CompletionPart.Attributes | CompletionPart.ReturnTypeAttributes:
					case CompletionPart.Attributes | CompletionPart.Parameters:
					case CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters:
					case CompletionPart.Attributes | CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters:
						goto IL_013D;
					case CompletionPart.Parameters:
						foreach (ParameterSymbol parameterSymbol in this.Parameters)
						{
							parameterSymbol.ForceComplete(locationOpt, cancellationToken);
						}
						this.state.NotePartComplete(CompletionPart.Parameters);
						break;
					case CompletionPart.Type:
					{
						TypeWithAnnotations returnTypeWithAnnotations = this.ReturnTypeWithAnnotations;
						this.state.NotePartComplete(CompletionPart.Type);
						break;
					}
					default:
						if (nextIncompletePart != CompletionPart.TypeParameters)
						{
							if (nextIncompletePart != CompletionPart.Members)
							{
								goto IL_013D;
							}
							goto IL_012B;
						}
						else
						{
							foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameters)
							{
								typeParameterSymbol.ForceComplete(locationOpt, cancellationToken);
							}
							this.state.NotePartComplete(CompletionPart.TypeParameters);
						}
						break;
					}
				}
				else
				{
					if (nextIncompletePart == CompletionPart.TypeMembers)
					{
						goto IL_012B;
					}
					if (nextIncompletePart != CompletionPart.SynthesizedExplicitImplementations && nextIncompletePart != CompletionPart.StartMemberChecks)
					{
						goto IL_013D;
					}
					break;
				}
				IL_014E:
				this.state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				continue;
				IL_012B:
				this.LazyAsyncMethodChecks(cancellationToken);
				goto IL_014E;
				IL_013D:
				this.state.NotePartComplete(CompletionPart.StartBaseType | CompletionPart.FinishBaseType | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
				goto IL_014E;
			}
			this.LazyMethodChecks();
			CompletionPart completionPart = CompletionPart.MethodSymbolAll;
			this.state.SpinWaitComplete(completionPart, cancellationToken);
		}

		// Token: 0x06007179 RID: 29049 RVA: 0x001A5100 File Offset: 0x001A3300
		protected sealed override void NoteAttributesComplete(bool forReturnType)
		{
			CompletionPart completionPart = (forReturnType ? CompletionPart.ReturnTypeAttributes : CompletionPart.Attributes);
			this.state.NotePartComplete(completionPart);
		}

		// Token: 0x0600717A RID: 29050 RVA: 0x001A5124 File Offset: 0x001A3324
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			base.AfterAddingTypeMembersChecks(conversions, diagnostics);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Location location = this.locations[0];
			if (this.IsDeclaredReadOnly && !this.ContainingType.IsReadOnly)
			{
				declaringCompilation.EnsureIsReadOnlyAttributeExists(diagnostics, location, true);
			}
			byte b;
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && base.ShouldEmitNullableContextValue(out b))
			{
				declaringCompilation.EnsureNullableContextAttributeExists(diagnostics, location, true);
			}
		}

		// Token: 0x0600717B RID: 29051 RVA: 0x001A5188 File Offset: 0x001A3388
		internal override byte? GetLocalNullableContextValue()
		{
			byte? b;
			if (!this.flags.TryGetNullableContext(out b))
			{
				b = this.ComputeNullableContextValue();
				this.flags.SetNullableContext(b);
			}
			return b;
		}

		// Token: 0x0600717C RID: 29052 RVA: 0x001A51BC File Offset: 0x001A33BC
		private byte? ComputeNullableContextValue()
		{
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			if (!declaringCompilation.ShouldEmitNullableAttributes(this))
			{
				return default(byte?);
			}
			MostCommonNullableValueBuilder mostCommonNullableValueBuilder = default(MostCommonNullableValueBuilder);
			foreach (TypeParameterSymbol typeParameterSymbol in this.TypeParameters)
			{
				typeParameterSymbol.GetCommonNullableValues(declaringCompilation, ref mostCommonNullableValueBuilder);
			}
			mostCommonNullableValueBuilder.AddValue(this.ReturnTypeWithAnnotations);
			foreach (ParameterSymbol parameterSymbol in this.Parameters)
			{
				parameterSymbol.GetCommonNullableValues(declaringCompilation, ref mostCommonNullableValueBuilder);
			}
			return mostCommonNullableValueBuilder.MostCommonValue;
		}

		// Token: 0x0600717D RID: 29053 RVA: 0x001A5254 File Offset: 0x001A3454
		internal override bool IsNullableAnalysisEnabled()
		{
			return this.flags.IsNullableAnalysisEnabled;
		}

		// Token: 0x0600717E RID: 29054 RVA: 0x001A5264 File Offset: 0x001A3464
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			if (this.IsDeclaredReadOnly && !this.ContainingType.IsReadOnly)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeIsReadOnlyAttribute(this));
			}
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			byte b;
			if (declaringCompilation.ShouldEmitNullableAttributes(this) && base.ShouldEmitNullableContextValue(out b))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizeNullableContextAttribute(this, b));
			}
			bool flag;
			if (this.RequiresExplicitOverride(out flag))
			{
				Symbol.AddSynthesizedAttribute(ref attributes, moduleBuilder.SynthesizePreserveBaseOverridesAttribute());
			}
			bool isAsync = this.IsAsync;
			bool isIterator = this.IsIterator;
			if (!isAsync && !isIterator)
			{
				return;
			}
			NamedTypeSymbol namedTypeSymbol;
			if (moduleBuilder.CompilationState.TryGetStateMachineType(this, out namedTypeSymbol))
			{
				TypedConstant typedConstant = new TypedConstant(declaringCompilation.GetWellKnownType(WellKnownType.System_Type), TypedConstantKind.Type, namedTypeSymbol.GetUnboundGenericTypeOrSelf());
				if (isAsync && isIterator)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_AsyncIteratorStateMachineAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
				}
				else if (isAsync)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_AsyncStateMachineAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
				}
				else if (isIterator)
				{
					Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_IteratorStateMachineAttribute__ctor, ImmutableArray.Create<TypedConstant>(typedConstant), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
				}
			}
			if (isAsync && !isIterator)
			{
				Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.SynthesizeDebuggerStepThroughAttribute());
			}
		}

		// Token: 0x0600717F RID: 29055 RVA: 0x001A53A4 File Offset: 0x001A35A4
		protected void CheckModifiersForBody(Location location, BindingDiagnosticBag diagnostics)
		{
			if (this.IsExtern && !this.IsAbstract)
			{
				diagnostics.Add(ErrorCode.ERR_ExternHasBody, location, new object[] { this });
				return;
			}
			if (this.IsAbstract && !this.IsExtern)
			{
				diagnostics.Add(ErrorCode.ERR_AbstractHasBody, location, new object[] { this });
			}
		}

		// Token: 0x06007180 RID: 29056 RVA: 0x001A5400 File Offset: 0x001A3600
		protected void CheckFeatureAvailabilityAndRuntimeSupport(SyntaxNode declarationSyntax, Location location, bool hasBody, BindingDiagnosticBag diagnostics)
		{
			if (this._containingType.IsInterface)
			{
				if (hasBody || this.IsExplicitInterfaceImplementation)
				{
					Binder.CheckFeatureAvailability(declarationSyntax, MessageID.IDS_DefaultInterfaceImplementation, diagnostics, location);
				}
				if ((hasBody || this.IsExplicitInterfaceImplementation || this.IsExtern) && !this.ContainingAssembly.RuntimeSupportsDefaultInterfaceImplementation)
				{
					diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation, location);
				}
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x06007181 RID: 29057
		internal abstract bool IsExpressionBodied { get; }

		// Token: 0x06007182 RID: 29058 RVA: 0x001A5464 File Offset: 0x001A3664
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			ValueTuple<BlockSyntax, ArrowExpressionClauseSyntax> bodies = this.Bodies;
			BlockSyntax item = bodies.Item1;
			ArrowExpressionClauseSyntax item2 = bodies.Item2;
			CSharpSyntaxNode csharpSyntaxNode;
			if (item != null && item.Span.Contains(localPosition))
			{
				csharpSyntaxNode = item;
			}
			else
			{
				if (item2 == null || !item2.Span.Contains(localPosition))
				{
					return -1;
				}
				csharpSyntaxNode = item2;
			}
			return localPosition - csharpSyntaxNode.SpanStart;
		}

		// Token: 0x04001BA7 RID: 7079
		protected SymbolCompletionState state;

		// Token: 0x04001BA8 RID: 7080
		protected DeclarationModifiers DeclarationModifiers;

		// Token: 0x04001BA9 RID: 7081
		protected SourceMemberMethodSymbol.Flags flags;

		// Token: 0x04001BAA RID: 7082
		private readonly NamedTypeSymbol _containingType;

		// Token: 0x04001BAB RID: 7083
		private ParameterSymbol _lazyThisParameter;

		// Token: 0x04001BAC RID: 7084
		private TypeWithAnnotations.Boxed _lazyIteratorElementType;

		// Token: 0x04001BAD RID: 7085
		private OverriddenOrHiddenMembersResult _lazyOverriddenOrHiddenMembers;

		// Token: 0x04001BAE RID: 7086
		protected ImmutableArray<Location> locations;

		// Token: 0x04001BAF RID: 7087
		protected string lazyDocComment;

		// Token: 0x04001BB0 RID: 7088
		protected string lazyExpandedDocComment;

		// Token: 0x04001BB1 RID: 7089
		private ImmutableArray<Diagnostic> _cachedDiagnostics;

		// Token: 0x0200092F RID: 2351
		protected struct Flags
		{
			// Token: 0x06009022 RID: 36898 RVA: 0x001FAA94 File Offset: 0x001F8C94
			public bool TryGetReturnsVoid(out bool value)
			{
				int flags = this._flags;
				value = (flags & 512) != 0;
				return (flags & 1024) != 0;
			}

			// Token: 0x06009023 RID: 36899 RVA: 0x001FAABE File Offset: 0x001F8CBE
			public void SetReturnsVoid(bool value)
			{
				ThreadSafeFlagOperations.Set(ref this._flags, 1024 | (value ? 512 : 0));
			}

			// Token: 0x170028A6 RID: 10406
			// (get) Token: 0x06009024 RID: 36900 RVA: 0x001FAADD File Offset: 0x001F8CDD
			public MethodKind MethodKind
			{
				get
				{
					return (MethodKind)(this._flags & 31);
				}
			}

			// Token: 0x170028A7 RID: 10407
			// (get) Token: 0x06009025 RID: 36901 RVA: 0x001FAAE8 File Offset: 0x001F8CE8
			public bool IsExtensionMethod
			{
				get
				{
					return (this._flags & 32) != 0;
				}
			}

			// Token: 0x170028A8 RID: 10408
			// (get) Token: 0x06009026 RID: 36902 RVA: 0x001FAAF6 File Offset: 0x001F8CF6
			public bool IsNullableAnalysisEnabled
			{
				get
				{
					return (this._flags & 16384) != 0;
				}
			}

			// Token: 0x170028A9 RID: 10409
			// (get) Token: 0x06009027 RID: 36903 RVA: 0x001FAB07 File Offset: 0x001F8D07
			public bool IsMetadataVirtualLocked
			{
				get
				{
					return (this._flags & 256) != 0;
				}
			}

			// Token: 0x06009028 RID: 36904 RVA: 0x001FAB18 File Offset: 0x001F8D18
			private static bool ModifiersRequireMetadataVirtual(DeclarationModifiers modifiers)
			{
				return (modifiers & (DeclarationModifiers.Abstract | DeclarationModifiers.Virtual | DeclarationModifiers.Override)) > DeclarationModifiers.None;
			}

			// Token: 0x06009029 RID: 36905 RVA: 0x001FAB24 File Offset: 0x001F8D24
			public Flags(MethodKind methodKind, DeclarationModifiers declarationModifiers, bool returnsVoid, bool isExtensionMethod, bool isNullableAnalysisEnabled, bool isMetadataVirtualIgnoringModifiers = false)
			{
				object obj = isMetadataVirtualIgnoringModifiers || SourceMemberMethodSymbol.Flags.ModifiersRequireMetadataVirtual(declarationModifiers);
				int num = (int)(methodKind & (MethodKind)31);
				int num2 = (isExtensionMethod ? 32 : 0);
				int num3 = (isNullableAnalysisEnabled ? 16384 : 0);
				object obj2 = obj;
				int num4 = ((obj2 != null) ? 64 : 0);
				int num5 = ((obj2 != null) ? 64 : 0);
				this._flags = num | num2 | num3 | num4 | num5 | (returnsVoid ? 512 : 0) | 1024;
			}

			// Token: 0x0600902A RID: 36906 RVA: 0x001FAB8E File Offset: 0x001F8D8E
			public bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
			{
				if (ignoreInterfaceImplementationChanges)
				{
					return (this._flags & 64) != 0;
				}
				if (!this.IsMetadataVirtualLocked)
				{
					ThreadSafeFlagOperations.Set(ref this._flags, 256);
				}
				return (this._flags & 64) != 0;
			}

			// Token: 0x0600902B RID: 36907 RVA: 0x001FABC5 File Offset: 0x001F8DC5
			public void EnsureMetadataVirtual()
			{
				if ((this._flags & 64) == 0)
				{
					ThreadSafeFlagOperations.Set(ref this._flags, 64);
				}
			}

			// Token: 0x0600902C RID: 36908 RVA: 0x001FABE0 File Offset: 0x001F8DE0
			public bool TryGetNullableContext(out byte? value)
			{
				return ((NullableContextKind)((this._flags >> 11) & 7)).TryGetByte(out value);
			}

			// Token: 0x0600902D RID: 36909 RVA: 0x001FABF4 File Offset: 0x001F8DF4
			public bool SetNullableContext(byte? value)
			{
				return ThreadSafeFlagOperations.Set(ref this._flags, (int)((int)(value.ToNullableContextFlags() & (NullableContextKind)7) << 11));
			}

			// Token: 0x040027DA RID: 10202
			private int _flags;

			// Token: 0x040027DB RID: 10203
			private const int MethodKindOffset = 0;

			// Token: 0x040027DC RID: 10204
			private const int MethodKindSize = 5;

			// Token: 0x040027DD RID: 10205
			private const int IsExtensionMethodOffset = 5;

			// Token: 0x040027DE RID: 10206
			private const int IsExtensionMethodSize = 1;

			// Token: 0x040027DF RID: 10207
			private const int IsMetadataVirtualIgnoringInterfaceChangesOffset = 6;

			// Token: 0x040027E0 RID: 10208
			private const int IsMetadataVirtualIgnoringInterfaceChangesSize = 1;

			// Token: 0x040027E1 RID: 10209
			private const int IsMetadataVirtualOffset = 7;

			// Token: 0x040027E2 RID: 10210
			private const int IsMetadataVirtualSize = 1;

			// Token: 0x040027E3 RID: 10211
			private const int IsMetadataVirtualLockedOffset = 8;

			// Token: 0x040027E4 RID: 10212
			private const int IsMetadataVirtualLockedSize = 1;

			// Token: 0x040027E5 RID: 10213
			private const int ReturnsVoidOffset = 9;

			// Token: 0x040027E6 RID: 10214
			private const int ReturnsVoidSize = 2;

			// Token: 0x040027E7 RID: 10215
			private const int NullableContextOffset = 11;

			// Token: 0x040027E8 RID: 10216
			private const int NullableContextSize = 3;

			// Token: 0x040027E9 RID: 10217
			private const int IsNullableAnalysisEnabledOffset = 14;

			// Token: 0x040027EA RID: 10218
			private const int IsNullableAnalysisEnabledSize = 1;

			// Token: 0x040027EB RID: 10219
			private const int MethodKindMask = 31;

			// Token: 0x040027EC RID: 10220
			private const int IsExtensionMethodBit = 32;

			// Token: 0x040027ED RID: 10221
			private const int IsMetadataVirtualIgnoringInterfaceChangesBit = 64;

			// Token: 0x040027EE RID: 10222
			private const int IsMetadataVirtualBit = 64;

			// Token: 0x040027EF RID: 10223
			private const int IsMetadataVirtualLockedBit = 256;

			// Token: 0x040027F0 RID: 10224
			private const int ReturnsVoidBit = 512;

			// Token: 0x040027F1 RID: 10225
			private const int ReturnsVoidIsSetBit = 1024;

			// Token: 0x040027F2 RID: 10226
			private const int NullableContextMask = 7;

			// Token: 0x040027F3 RID: 10227
			private const int IsNullableAnalysisEnabledBit = 16384;
		}
	}
}
