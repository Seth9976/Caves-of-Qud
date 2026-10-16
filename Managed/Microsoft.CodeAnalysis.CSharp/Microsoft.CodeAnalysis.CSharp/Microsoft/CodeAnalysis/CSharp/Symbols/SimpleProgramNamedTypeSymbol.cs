using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000528 RID: 1320
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SimpleProgramNamedTypeSymbol : SourceMemberContainerTypeSymbol
	{
		// Token: 0x06006EC7 RID: 28359 RVA: 0x00197590 File Offset: 0x00195790
		internal SimpleProgramNamedTypeSymbol(NamespaceSymbol globalNamespace, MergedTypeDeclaration declaration, BindingDiagnosticBag diagnostics)
			: base(globalNamespace, declaration, diagnostics, null)
		{
			this.state.NotePartComplete(CompletionPart.EnumUnderlyingType);
		}

		// Token: 0x06006EC8 RID: 28360 RVA: 0x001975B0 File Offset: 0x001957B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static SynthesizedSimpleProgramEntryPointSymbol GetSimpleProgramEntryPoint(CSharpCompilation compilation)
		{
			SimpleProgramNamedTypeSymbol simpleProgramNamedTypeSymbol = SimpleProgramNamedTypeSymbol.GetSimpleProgramNamedTypeSymbol(compilation);
			return (SynthesizedSimpleProgramEntryPointSymbol)((simpleProgramNamedTypeSymbol != null) ? simpleProgramNamedTypeSymbol.GetMembersAndInitializers().NonTypeMembers[0] : null);
		}

		// Token: 0x06006EC9 RID: 28361 RVA: 0x001975E4 File Offset: 0x001957E4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private static SimpleProgramNamedTypeSymbol GetSimpleProgramNamedTypeSymbol(CSharpCompilation compilation)
		{
			return Enumerable.SingleOrDefault<SimpleProgramNamedTypeSymbol>(compilation.SourceModule.GlobalNamespace.GetTypeMembers("<Program>$").OfType<SimpleProgramNamedTypeSymbol>());
		}

		// Token: 0x06006ECA RID: 28362 RVA: 0x00197614 File Offset: 0x00195814
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static SynthesizedSimpleProgramEntryPointSymbol GetSimpleProgramEntryPoint(CSharpCompilation compilation, CompilationUnitSyntax compilationUnit, bool fallbackToMainEntryPoint)
		{
			SimpleProgramNamedTypeSymbol simpleProgramNamedTypeSymbol = SimpleProgramNamedTypeSymbol.GetSimpleProgramNamedTypeSymbol(compilation);
			if (simpleProgramNamedTypeSymbol == null)
			{
				return null;
			}
			ImmutableArray<Symbol> nonTypeMembers = simpleProgramNamedTypeSymbol.GetMembersAndInitializers().NonTypeMembers;
			foreach (Symbol symbol in nonTypeMembers)
			{
				SynthesizedSimpleProgramEntryPointSymbol synthesizedSimpleProgramEntryPointSymbol = (SynthesizedSimpleProgramEntryPointSymbol)symbol;
				if (synthesizedSimpleProgramEntryPointSymbol.SyntaxTree == compilationUnit.SyntaxTree && synthesizedSimpleProgramEntryPointSymbol.SyntaxNode == compilationUnit)
				{
					return synthesizedSimpleProgramEntryPointSymbol;
				}
			}
			if (!fallbackToMainEntryPoint)
			{
				return null;
			}
			return (SynthesizedSimpleProgramEntryPointSymbol)nonTypeMembers[0];
		}

		// Token: 0x06006ECB RID: 28363 RVA: 0x00197684 File Offset: 0x00195884
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006ECC RID: 28364 RVA: 0x0019768B File Offset: 0x0019588B
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			this.state.NotePartComplete(CompletionPart.Attributes);
			return ImmutableArray<CSharpAttributeData>.Empty;
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x0019769F File Offset: 0x0019589F
		internal override AttributeUsageInfo GetAttributeUsageInfo()
		{
			return AttributeUsageInfo.Null;
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x001976A6 File Offset: 0x001958A6
		protected override Location GetCorrespondingBaseListLocation(NamedTypeSymbol @base)
		{
			return NoLocation.Singleton;
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x06006ECF RID: 28367 RVA: 0x001976AD File Offset: 0x001958AD
		internal override NamedTypeSymbol BaseTypeNoUseSiteDiagnostics
		{
			get
			{
				return this.DeclaringCompilation.GetSpecialType(SpecialType.System_Object);
			}
		}

		// Token: 0x06006ED0 RID: 28368 RVA: 0x001976BB File Offset: 0x001958BB
		protected override void CheckBase(BindingDiagnosticBag diagnostics)
		{
			Binder.GetSpecialType(this.DeclaringCompilation, SpecialType.System_Object, NoLocation.Singleton, diagnostics);
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x001976D0 File Offset: 0x001958D0
		internal override NamedTypeSymbol GetDeclaredBaseType(ConsList<TypeSymbol> basesBeingResolved)
		{
			return this.BaseTypeNoUseSiteDiagnostics;
		}

		// Token: 0x06006ED2 RID: 28370 RVA: 0x001976D8 File Offset: 0x001958D8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> InterfacesNoUseSiteDiagnostics(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006ED3 RID: 28371 RVA: 0x001976DF File Offset: 0x001958DF
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<NamedTypeSymbol> GetDeclaredInterfaces(ConsList<TypeSymbol> basesBeingResolved)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x001976E6 File Offset: 0x001958E6
		protected override void CheckInterfaces(BindingDiagnosticBag diagnostics)
		{
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x06006ED5 RID: 28373 RVA: 0x001976E8 File Offset: 0x001958E8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<TypeParameterSymbol> TypeParameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<TypeParameterSymbol>.Empty;
			}
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06006ED6 RID: 28374 RVA: 0x001976EF File Offset: 0x001958EF
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal override ImmutableArray<TypeWithAnnotations> TypeArgumentsWithAnnotationsNoUseSiteDiagnostics
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return ImmutableArray<TypeWithAnnotations>.Empty;
			}
		}

		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x06006ED7 RID: 28375 RVA: 0x001976F6 File Offset: 0x001958F6
		public sealed override bool AreLocalsZeroed
		{
			get
			{
				return this.ContainingModule.AreLocalsZeroed;
			}
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x06006ED8 RID: 28376 RVA: 0x00197703 File Offset: 0x00195903
		internal override bool IsComImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x06006ED9 RID: 28377 RVA: 0x00197706 File Offset: 0x00195906
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol ComImportCoClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x06006EDA RID: 28378 RVA: 0x00197709 File Offset: 0x00195909
		internal override bool HasSpecialName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x06006EDB RID: 28379 RVA: 0x0019770C File Offset: 0x0019590C
		internal override bool ShouldAddWinRTMembers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06006EDC RID: 28380 RVA: 0x0019770F File Offset: 0x0019590F
		internal sealed override bool IsWindowsRuntimeImport
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06006EDD RID: 28381 RVA: 0x00197712 File Offset: 0x00195912
		public sealed override bool IsSerializable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06006EDE RID: 28382 RVA: 0x00197718 File Offset: 0x00195918
		internal sealed override TypeLayout Layout
		{
			get
			{
				return default(TypeLayout);
			}
		}

		// Token: 0x17001D65 RID: 7525
		// (get) Token: 0x06006EDF RID: 28383 RVA: 0x0019772E File Offset: 0x0019592E
		internal bool HasStructLayoutAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D66 RID: 7526
		// (get) Token: 0x06006EE0 RID: 28384 RVA: 0x00197731 File Offset: 0x00195931
		internal override CharSet MarshallingCharSet
		{
			get
			{
				return base.DefaultMarshallingCharSet;
			}
		}

		// Token: 0x17001D67 RID: 7527
		// (get) Token: 0x06006EE1 RID: 28385 RVA: 0x00197739 File Offset: 0x00195939
		internal sealed override bool HasDeclarativeSecurity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x0019773C File Offset: 0x0019593C
		internal sealed override IEnumerable<SecurityAttribute> GetSecurityInformation()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x00197743 File Offset: 0x00195943
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<string> GetAppliedConditionalSymbols()
		{
			return ImmutableArray<string>.Empty;
		}

		// Token: 0x17001D68 RID: 7528
		// (get) Token: 0x06006EE4 RID: 28388 RVA: 0x0019774A File Offset: 0x0019594A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ObsoleteAttributeData ObsoleteAttributeData
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001D69 RID: 7529
		// (get) Token: 0x06006EE5 RID: 28389 RVA: 0x0019774D File Offset: 0x0019594D
		internal override bool HasCodeAnalysisEmbeddedAttribute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x00197750 File Offset: 0x00195950
		protected override SourceMemberContainerTypeSymbol.MembersAndInitializers BuildMembersAndInitializers(BindingDiagnosticBag diagnostics)
		{
			bool flag = false;
			foreach (SingleTypeDeclaration singleTypeDeclaration in this.declaration.Declarations)
			{
				if (flag)
				{
					Binder.Error(diagnostics, ErrorCode.ERR_SimpleProgramMultipleUnitsWithTopLevelStatements, singleTypeDeclaration.NameLocation);
				}
				else
				{
					flag = true;
				}
			}
			return new SourceMemberContainerTypeSymbol.MembersAndInitializers(this.declaration.Declarations.SelectAsArray<SingleTypeDeclaration, Symbol>((SingleTypeDeclaration singleDeclaration) => new SynthesizedSimpleProgramEntryPointSymbol(this, singleDeclaration, diagnostics)), ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>>.Empty, ImmutableArray<ImmutableArray<FieldOrPropertyInitializer>>.Empty, false, false, false);
		}

		// Token: 0x17001D6A RID: 7530
		// (get) Token: 0x06006EE7 RID: 28391 RVA: 0x001977E7 File Offset: 0x001959E7
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x001977EC File Offset: 0x001959EC
		internal override bool IsDefinedInSourceTree(SyntaxTree tree, TextSpan? definedWithinSpan, CancellationToken cancellationToken)
		{
			foreach (Symbol symbol in base.GetMembersAndInitializers().NonTypeMembers)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (symbol.IsDefinedInSourceTree(tree, definedWithinSpan, cancellationToken))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x00197834 File Offset: 0x00195A34
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			Symbol.AddSynthesizedAttribute(ref attributes, this.DeclaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x0019786F File Offset: 0x00195A6F
		internal override NamedTypeSymbol AsNativeInteger()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001D6B RID: 7531
		// (get) Token: 0x06006EEB RID: 28395 RVA: 0x00197876 File Offset: 0x00195A76
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override NamedTypeSymbol NativeIntegerUnderlyingType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}
	}
}
