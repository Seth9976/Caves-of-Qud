using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200058C RID: 1420
	internal abstract class SynthesizedEntryPointSymbol : MethodSymbol
	{
		// Token: 0x0600766E RID: 30318 RVA: 0x001B7724 File Offset: 0x001B5924
		internal static SynthesizedEntryPointSymbol Create(SynthesizedInteractiveInitializerMethod initializerMethod, BindingDiagnosticBag diagnostics)
		{
			NamedTypeSymbol containingType = initializerMethod.ContainingType;
			CSharpCompilation declaringCompilation = containingType.DeclaringCompilation;
			if (declaringCompilation.IsSubmission)
			{
				NamedTypeSymbol specialType = Binder.GetSpecialType(declaringCompilation, SpecialType.System_Object, SynthesizedEntryPointSymbol.DummySyntax(), diagnostics);
				ArrayTypeSymbol arrayTypeSymbol = declaringCompilation.CreateArrayTypeSymbol(specialType, 1, NullableAnnotation.Oblivious);
				diagnostics.ReportUseSite(arrayTypeSymbol, NoLocation.Singleton);
				return new SynthesizedEntryPointSymbol.SubmissionEntryPoint(containingType, initializerMethod.ReturnTypeWithAnnotations, arrayTypeSymbol);
			}
			NamedTypeSymbol specialType2 = Binder.GetSpecialType(declaringCompilation, SpecialType.System_Void, SynthesizedEntryPointSymbol.DummySyntax(), diagnostics);
			return new SynthesizedEntryPointSymbol.ScriptEntryPoint(containingType, TypeWithAnnotations.Create(specialType2, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)));
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x001B77A1 File Offset: 0x001B59A1
		private SynthesizedEntryPointSymbol(NamedTypeSymbol containingType)
		{
			this._containingType = containingType;
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06007670 RID: 30320 RVA: 0x001B77B0 File Offset: 0x001B59B0
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007671 RID: 30321
		internal abstract BoundBlock CreateBody(BindingDiagnosticBag diagnostics);

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06007672 RID: 30322 RVA: 0x001B77B3 File Offset: 0x001B59B3
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06007673 RID: 30323
		public abstract override string Name { get; }

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06007674 RID: 30324 RVA: 0x001B77BB File Offset: 0x001B59BB
		internal override bool HasSpecialName
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06007675 RID: 30325 RVA: 0x001B77BE File Offset: 0x001B59BE
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06007676 RID: 30326 RVA: 0x001B77C1 File Offset: 0x001B59C1
		internal override bool RequiresSecurityObject
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06007677 RID: 30327 RVA: 0x001B77C4 File Offset: 0x001B59C4
		public override bool IsVararg
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06007678 RID: 30328 RVA: 0x001B77C7 File Offset: 0x001B59C7
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06007679 RID: 30329 RVA: 0x001B77CE File Offset: 0x001B59CE
		public override Accessibility DeclaredAccessibility
		{
			get
			{
				return Accessibility.Private;
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x0600767A RID: 30330 RVA: 0x001B77D1 File Offset: 0x001B59D1
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x0600767B RID: 30331 RVA: 0x001B77D8 File Offset: 0x001B59D8
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x0600767C RID: 30332 RVA: 0x001B77DF File Offset: 0x001B59DF
		public override RefKind RefKind
		{
			get
			{
				return RefKind.None;
			}
		}

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x0600767D RID: 30333 RVA: 0x001B77E2 File Offset: 0x001B59E2
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x0600767E RID: 30334 RVA: 0x001B77E9 File Offset: 0x001B59E9
		public override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotations
		{
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x0600767F RID: 30335 RVA: 0x001B77F0 File Offset: 0x001B59F0
		public override Symbol AssociatedSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x06007680 RID: 30336 RVA: 0x001B77F3 File Offset: 0x001B59F3
		public override int Arity
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06007681 RID: 30337 RVA: 0x001B77F6 File Offset: 0x001B59F6
		public override bool ReturnsVoid
		{
			get
			{
				return base.ReturnType.IsVoidType();
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x001B7803 File Offset: 0x001B5A03
		public sealed override FlowAnalysisAnnotations ReturnTypeFlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06007683 RID: 30339 RVA: 0x001B7806 File Offset: 0x001B5A06
		public sealed override ImmutableHashSet<string> ReturnNotNullIfParameterNotNull
		{
			get
			{
				return ImmutableHashSet<string>.Empty;
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06007684 RID: 30340 RVA: 0x001B780D File Offset: 0x001B5A0D
		public sealed override FlowAnalysisAnnotations FlowAnalysisAnnotations
		{
			get
			{
				return FlowAnalysisAnnotations.None;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06007685 RID: 30341 RVA: 0x001B7810 File Offset: 0x001B5A10
		public override MethodKind MethodKind
		{
			get
			{
				return MethodKind.Ordinary;
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x06007686 RID: 30342 RVA: 0x001B7814 File Offset: 0x001B5A14
		public override bool IsExtern
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06007687 RID: 30343 RVA: 0x001B7817 File Offset: 0x001B5A17
		public override bool IsSealed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06007688 RID: 30344 RVA: 0x001B781A File Offset: 0x001B5A1A
		public override bool IsAbstract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06007689 RID: 30345 RVA: 0x001B781D File Offset: 0x001B5A1D
		public override bool IsOverride
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x0600768A RID: 30346 RVA: 0x001B7820 File Offset: 0x001B5A20
		public override bool IsVirtual
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x0600768B RID: 30347 RVA: 0x001B7823 File Offset: 0x001B5A23
		public override bool IsStatic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x0600768C RID: 30348 RVA: 0x001B7826 File Offset: 0x001B5A26
		public override bool IsAsync
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x0600768D RID: 30349 RVA: 0x001B7829 File Offset: 0x001B5A29
		public override bool HidesBaseMethodsByName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x0600768E RID: 30350 RVA: 0x001B782C File Offset: 0x001B5A2C
		public override bool IsExtensionMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x0600768F RID: 30351 RVA: 0x001B782F File Offset: 0x001B5A2F
		internal sealed override ObsoleteAttributeData ObsoleteAttributeData
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007690 RID: 30352 RVA: 0x001B7832 File Offset: 0x001B5A32
		internal sealed override UnmanagedCallersOnlyAttributeData GetUnmanagedCallersOnlyAttributeData(bool forceComplete)
		{
			return null;
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06007691 RID: 30353 RVA: 0x001B7835 File Offset: 0x001B5A35
		internal override CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.Default;
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06007692 RID: 30354 RVA: 0x001B7838 File Offset: 0x001B5A38
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06007693 RID: 30355 RVA: 0x001B783B File Offset: 0x001B5A3B
		public override ImmutableArray<MethodSymbol> ExplicitInterfaceImplementations
		{
			get
			{
				return ImmutableArray<MethodSymbol>.Empty;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06007694 RID: 30356 RVA: 0x001B7842 File Offset: 0x001B5A42
		internal sealed override bool IsDeclaredReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06007695 RID: 30357 RVA: 0x001B7845 File Offset: 0x001B5A45
		internal sealed override bool IsInitOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007696 RID: 30358 RVA: 0x001B7848 File Offset: 0x001B5A48
		internal sealed override bool IsMetadataNewSlot(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x06007697 RID: 30359 RVA: 0x001B784B File Offset: 0x001B5A4B
		internal sealed override bool IsMetadataVirtual(bool ignoreInterfaceImplementationChanges = false)
		{
			return false;
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06007698 RID: 30360 RVA: 0x001B784E File Offset: 0x001B5A4E
		internal override bool IsMetadataFinal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06007699 RID: 30361 RVA: 0x001B7851 File Offset: 0x001B5A51
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600769A RID: 30362 RVA: 0x001B7854 File Offset: 0x001B5A54
		public override DllImportData GetDllImportData()
		{
			return null;
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x0600769B RID: 30363 RVA: 0x001B7857 File Offset: 0x001B5A57
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.ContainingType.AreLocalsZeroed;
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x0600769C RID: 30364 RVA: 0x001B7864 File Offset: 0x001B5A64
		internal override MarshalPseudoCustomAttributeData ReturnValueMarshallingInformation
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x0600769D RID: 30365 RVA: 0x001B7867 File Offset: 0x001B5A67
		internal override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600769E RID: 30366 RVA: 0x001B786A File Offset: 0x001B5A6A
		internal override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x001B7871 File Offset: 0x001B5A71
		internal sealed override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x001B7878 File Offset: 0x001B5A78
		internal override int CalculateLocalSyntaxOffset(int localPosition, SyntaxTree localTree)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x001B7880 File Offset: 0x001B5A80
		private static CSharpSyntaxNode DummySyntax()
		{
			return (CSharpSyntaxNode)CSharpSyntaxTree.Dummy.GetRoot(default(CancellationToken));
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x001B78A8 File Offset: 0x001B5AA8
		private static BoundCall CreateParameterlessCall(CSharpSyntaxNode syntax, BoundExpression receiver, MethodSymbol method)
		{
			return new BoundCall(syntax, receiver, method, ImmutableArray<BoundExpression>.Empty, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, method.ReturnType, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x001B78F9 File Offset: 0x001B5AF9
		internal sealed override bool IsNullableAnalysisEnabled()
		{
			return false;
		}

		// Token: 0x04001CC5 RID: 7365
		internal const string MainName = "<Main>";

		// Token: 0x04001CC6 RID: 7366
		internal const string FactoryName = "<Factory>";

		// Token: 0x04001CC7 RID: 7367
		private readonly NamedTypeSymbol _containingType;

		// Token: 0x02000953 RID: 2387
		internal sealed class AsyncForwardEntryPoint : SynthesizedEntryPointSymbol
		{
			// Token: 0x060090D4 RID: 37076 RVA: 0x001FC580 File Offset: 0x001FA780
			internal AsyncForwardEntryPoint(CSharpCompilation compilation, NamedTypeSymbol containingType, MethodSymbol userMain)
				: base(containingType)
			{
				this.UserMain = userMain;
				this._userMainReturnTypeSyntax = userMain.ExtractReturnTypeSyntax();
				Binder binder = compilation.GetBinder(this._userMainReturnTypeSyntax);
				this._parameters = SynthesizedParameterSymbol.DeriveParameters(userMain, this);
				ImmutableArray<BoundExpression> immutableArray = this.Parameters.SelectAsArray((ParameterSymbol p, CSharpSyntaxNode s) => new BoundParameter(s, p, p.Type), this._userMainReturnTypeSyntax);
				BoundCall boundCall = new BoundCall(this._userMainReturnTypeSyntax, null, userMain, immutableArray, default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, false, false, default(ImmutableArray<int>), default(BitVector), LookupResultKind.Viable, userMain.ReturnType, false)
				{
					WasCompilerGenerated = true
				};
				binder.GetAwaitableExpressionInfo(boundCall, out this._getAwaiterGetResultCall, this._userMainReturnTypeSyntax, BindingDiagnosticBag.Discarded);
			}

			// Token: 0x060090D5 RID: 37077 RVA: 0x001FC651 File Offset: 0x001FA851
			internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
			{
				base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
				Symbol.AddSynthesizedAttribute(ref attributes, this.DeclaringCompilation.SynthesizeDebuggerStepThroughAttribute());
			}

			// Token: 0x170028D6 RID: 10454
			// (get) Token: 0x060090D6 RID: 37078 RVA: 0x001FC66C File Offset: 0x001FA86C
			public override string Name
			{
				get
				{
					return "<Main>";
				}
			}

			// Token: 0x170028D7 RID: 10455
			// (get) Token: 0x060090D7 RID: 37079 RVA: 0x001FC673 File Offset: 0x001FA873
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x170028D8 RID: 10456
			// (get) Token: 0x060090D8 RID: 37080 RVA: 0x001FC67C File Offset: 0x001FA87C
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return TypeWithAnnotations.Create(this._getAwaiterGetResultCall.Type, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
				}
			}

			// Token: 0x060090D9 RID: 37081 RVA: 0x001FC6A4 File Offset: 0x001FA8A4
			internal override BoundBlock CreateBody(BindingDiagnosticBag diagnostics)
			{
				CSharpSyntaxNode userMainReturnTypeSyntax = this._userMainReturnTypeSyntax;
				if (this.ReturnsVoid)
				{
					return new BoundBlock(userMainReturnTypeSyntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(new BoundExpressionStatement(userMainReturnTypeSyntax, this._getAwaiterGetResultCall, false)
					{
						WasCompilerGenerated = true
					}, new BoundReturnStatement(userMainReturnTypeSyntax, RefKind.None, null, false)
					{
						WasCompilerGenerated = true
					}), false)
					{
						WasCompilerGenerated = true
					};
				}
				return new BoundBlock(userMainReturnTypeSyntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(new BoundReturnStatement(userMainReturnTypeSyntax, RefKind.None, this._getAwaiterGetResultCall, false)), false)
				{
					WasCompilerGenerated = true
				};
			}

			// Token: 0x0400284D RID: 10317
			private readonly CSharpSyntaxNode _userMainReturnTypeSyntax;

			// Token: 0x0400284E RID: 10318
			private readonly BoundExpression _getAwaiterGetResultCall;

			// Token: 0x0400284F RID: 10319
			private readonly ImmutableArray<ParameterSymbol> _parameters;

			// Token: 0x04002850 RID: 10320
			internal readonly MethodSymbol UserMain;
		}

		// Token: 0x02000954 RID: 2388
		private sealed class ScriptEntryPoint : SynthesizedEntryPointSymbol
		{
			// Token: 0x060090DA RID: 37082 RVA: 0x001FC723 File Offset: 0x001FA923
			internal ScriptEntryPoint(NamedTypeSymbol containingType, TypeWithAnnotations returnType)
				: base(containingType)
			{
				this._returnType = returnType;
			}

			// Token: 0x170028D9 RID: 10457
			// (get) Token: 0x060090DB RID: 37083 RVA: 0x001FC733 File Offset: 0x001FA933
			public override string Name
			{
				get
				{
					return "<Main>";
				}
			}

			// Token: 0x170028DA RID: 10458
			// (get) Token: 0x060090DC RID: 37084 RVA: 0x001FC73A File Offset: 0x001FA93A
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return ImmutableArray<ParameterSymbol>.Empty;
				}
			}

			// Token: 0x170028DB RID: 10459
			// (get) Token: 0x060090DD RID: 37085 RVA: 0x001FC741 File Offset: 0x001FA941
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return this._returnType;
				}
			}

			// Token: 0x060090DE RID: 37086 RVA: 0x001FC74C File Offset: 0x001FA94C
			internal override BoundBlock CreateBody(BindingDiagnosticBag diagnostics)
			{
				CSharpSyntaxNode csharpSyntaxNode = SynthesizedEntryPointSymbol.DummySyntax();
				CSharpCompilation declaringCompilation = this._containingType.DeclaringCompilation;
				Binder binder = WithUsingNamespacesAndTypesBinder.Create(declaringCompilation.GlobalImports, new BuckStopsHereBinder(declaringCompilation), true);
				binder = new InContainerBinder(declaringCompilation.GlobalNamespace, binder);
				SynthesizedInstanceConstructor scriptConstructor = this._containingType.GetScriptConstructor();
				SynthesizedInteractiveInitializerMethod scriptInitializer = this._containingType.GetScriptInitializer();
				BoundLocal boundLocal = new BoundLocal(csharpSyntaxNode, new SynthesizedLocal(this, TypeWithAnnotations.Create(this._containingType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LoweringTemp, null, false, RefKind.None), null, this._containingType, false)
				{
					WasCompilerGenerated = true
				};
				BoundCall boundCall = SynthesizedEntryPointSymbol.CreateParameterlessCall(csharpSyntaxNode, boundLocal, scriptInitializer);
				BoundExpression boundExpression;
				if (!binder.GetAwaitableExpressionInfo(boundCall, out boundExpression, csharpSyntaxNode, diagnostics))
				{
					return new BoundBlock(csharpSyntaxNode, ImmutableArray<LocalSymbol>.Empty, ImmutableArray<BoundStatement>.Empty, true);
				}
				return new BoundBlock(csharpSyntaxNode, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundStatement>(new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, boundLocal, new BoundObjectCreationExpression(csharpSyntaxNode, scriptConstructor, Array.Empty<BoundExpression>())
				{
					WasCompilerGenerated = true
				}, this._containingType, false, false)
				{
					WasCompilerGenerated = true
				}, false)
				{
					WasCompilerGenerated = true
				}, new BoundExpressionStatement(csharpSyntaxNode, boundExpression, false)
				{
					WasCompilerGenerated = true
				}, new BoundReturnStatement(csharpSyntaxNode, RefKind.None, null, false)
				{
					WasCompilerGenerated = true
				}), false)
				{
					WasCompilerGenerated = true
				};
			}

			// Token: 0x04002851 RID: 10321
			private readonly TypeWithAnnotations _returnType;
		}

		// Token: 0x02000955 RID: 2389
		private sealed class SubmissionEntryPoint : SynthesizedEntryPointSymbol
		{
			// Token: 0x060090DF RID: 37087 RVA: 0x001FC880 File Offset: 0x001FAA80
			internal SubmissionEntryPoint(NamedTypeSymbol containingType, TypeWithAnnotations returnType, TypeSymbol submissionArrayType)
				: base(containingType)
			{
				this._parameters = ImmutableArray.Create<ParameterSymbol>(SynthesizedParameterSymbol.Create(this, TypeWithAnnotations.Create(submissionArrayType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), 0, RefKind.None, "submissionArray", default(ImmutableArray<CustomModifier>), null));
				this._returnType = returnType;
			}

			// Token: 0x170028DC RID: 10460
			// (get) Token: 0x060090E0 RID: 37088 RVA: 0x001FC8CD File Offset: 0x001FAACD
			public override string Name
			{
				get
				{
					return "<Factory>";
				}
			}

			// Token: 0x170028DD RID: 10461
			// (get) Token: 0x060090E1 RID: 37089 RVA: 0x001FC8D4 File Offset: 0x001FAAD4
			public override ImmutableArray<ParameterSymbol> Parameters
			{
				get
				{
					return this._parameters;
				}
			}

			// Token: 0x170028DE RID: 10462
			// (get) Token: 0x060090E2 RID: 37090 RVA: 0x001FC8DC File Offset: 0x001FAADC
			public override TypeWithAnnotations ReturnTypeWithAnnotations
			{
				get
				{
					return this._returnType;
				}
			}

			// Token: 0x060090E3 RID: 37091 RVA: 0x001FC8E4 File Offset: 0x001FAAE4
			internal override BoundBlock CreateBody(BindingDiagnosticBag diagnostics)
			{
				CSharpSyntaxNode csharpSyntaxNode = SynthesizedEntryPointSymbol.DummySyntax();
				SynthesizedInstanceConstructor scriptConstructor = this._containingType.GetScriptConstructor();
				SynthesizedInteractiveInitializerMethod scriptInitializer = this._containingType.GetScriptInitializer();
				BoundParameter boundParameter = new BoundParameter(csharpSyntaxNode, this._parameters[0])
				{
					WasCompilerGenerated = true
				};
				BoundLocal boundLocal = new BoundLocal(csharpSyntaxNode, new SynthesizedLocal(this, TypeWithAnnotations.Create(this._containingType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>)), SynthesizedLocalKind.LoweringTemp, null, false, RefKind.None), null, this._containingType, false)
				{
					WasCompilerGenerated = true
				};
				BoundExpressionStatement boundExpressionStatement = new BoundExpressionStatement(csharpSyntaxNode, new BoundAssignmentOperator(csharpSyntaxNode, boundLocal, new BoundObjectCreationExpression(csharpSyntaxNode, scriptConstructor, ImmutableArray.Create<BoundExpression>(boundParameter), default(ImmutableArray<string>), default(ImmutableArray<RefKind>), false, default(ImmutableArray<int>), default(BitVector), null, null, this._containingType, false)
				{
					WasCompilerGenerated = true
				}, this._containingType, false, false)
				{
					WasCompilerGenerated = true
				}, false)
				{
					WasCompilerGenerated = true
				};
				BoundCall boundCall = SynthesizedEntryPointSymbol.CreateParameterlessCall(csharpSyntaxNode, boundLocal, scriptInitializer);
				BoundReturnStatement boundReturnStatement = new BoundReturnStatement(csharpSyntaxNode, RefKind.None, boundCall, false)
				{
					WasCompilerGenerated = true
				};
				return new BoundBlock(csharpSyntaxNode, ImmutableArray.Create<LocalSymbol>(boundLocal.LocalSymbol), ImmutableArray.Create<BoundStatement>(boundExpressionStatement, boundReturnStatement), false)
				{
					WasCompilerGenerated = true
				};
			}

			// Token: 0x04002852 RID: 10322
			private readonly ImmutableArray<ParameterSymbol> _parameters;

			// Token: 0x04002853 RID: 10323
			private readonly TypeWithAnnotations _returnType;
		}
	}
}
