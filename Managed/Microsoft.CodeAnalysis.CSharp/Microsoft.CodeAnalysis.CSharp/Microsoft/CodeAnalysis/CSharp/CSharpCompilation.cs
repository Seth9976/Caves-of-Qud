using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE;
using Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel;
using Microsoft.CodeAnalysis.CSharp.Symbols.Retargeting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.RuntimeMembers;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000103 RID: 259
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CSharpCompilation : Compilation
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x0006A309 File Offset: 0x00068509
		internal Conversions Conversions
		{
			get
			{
				if (this._conversions == null)
				{
					Interlocked.CompareExchange<Conversions>(ref this._conversions, new BuckStopsHereBinder(this).Conversions, null);
				}
				return this._conversions;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0006A331 File Offset: 0x00068531
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		internal ImmutableHashSet<SyntaxTree> UsageOfUsingsRecordedInTrees
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
			get
			{
				return Volatile.Read<ImmutableHashSet<SyntaxTree>>(ref this._usageOfUsingsRecordedInTrees);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0006A33E File Offset: 0x0006853E
		public override string Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0006A345 File Offset: 0x00068545
		public override bool IsCaseSensitive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0006A348 File Offset: 0x00068548
		public new CSharpCompilationOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0006A350 File Offset: 0x00068550
		internal AnonymousTypeManager AnonymousTypeManager
		{
			get
			{
				return this._anonymousTypeManager;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x0006A358 File Offset: 0x00068558
		internal override CommonAnonymousTypeManager CommonAnonymousTypeManager
		{
			get
			{
				return this.AnonymousTypeManager;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0006A360 File Offset: 0x00068560
		internal bool FeatureStrictEnabled
		{
			get
			{
				return base.Feature("strict") != null;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0006A370 File Offset: 0x00068570
		internal bool IsPeVerifyCompatEnabled
		{
			get
			{
				return this.LanguageVersion < LanguageVersion.CSharp7_2 || base.Feature("peverify-compat") != null;
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0006A38F File Offset: 0x0006858F
		internal bool IsNullableAnalysisEnabledIn(SyntaxNode syntax)
		{
			return this.IsNullableAnalysisEnabledIn((CSharpSyntaxTree)syntax.SyntaxTree, syntax.Span);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0006A3A8 File Offset: 0x000685A8
		internal bool IsNullableAnalysisEnabledIn(CSharpSyntaxTree tree, TextSpan span)
		{
			bool? nullableAnalysisValue = this.GetNullableAnalysisValue();
			if (nullableAnalysisValue != null)
			{
				return nullableAnalysisValue.GetValueOrDefault();
			}
			bool? flag = tree.IsNullableAnalysisEnabled(span);
			if (flag == null)
			{
				return (this.Options.NullableContextOptions & NullableContextOptions.Warnings) > NullableContextOptions.Disable;
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0006A3F8 File Offset: 0x000685F8
		internal bool IsNullableAnalysisEnabledIn(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method)
		{
			bool? nullableAnalysisValue = this.GetNullableAnalysisValue();
			if (nullableAnalysisValue == null)
			{
				return method.IsNullableAnalysisEnabled();
			}
			return nullableAnalysisValue.GetValueOrDefault();
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0006A424 File Offset: 0x00068624
		internal bool IsNullableAnalysisEnabledAlways
		{
			get
			{
				bool? nullableAnalysisValue = this.GetNullableAnalysisValue();
				bool flag = true;
				return (nullableAnalysisValue.GetValueOrDefault() == flag) & (nullableAnalysisValue != null);
			}
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0006A44C File Offset: 0x0006864C
		private bool? GetNullableAnalysisValue()
		{
			string text = base.Feature("run-nullable-analysis");
			bool? flag;
			if (!(text == "always"))
			{
				if (!(text == "never"))
				{
					flag = default(bool?);
				}
				else
				{
					flag = new bool?(false);
				}
			}
			else
			{
				flag = new bool?(true);
			}
			return flag;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0006A49E File Offset: 0x0006869E
		public LanguageVersion LanguageVersion { get; }

		// Token: 0x0600138F RID: 5007 RVA: 0x0006A4A6 File Offset: 0x000686A6
		protected override INamedTypeSymbol CommonCreateErrorTypeSymbol([global::System.Runtime.CompilerServices.Nullable(2)] INamespaceOrTypeSymbol container, string name, int arity)
		{
			return new ExtendedErrorTypeSymbol(container.EnsureCSharpSymbolOrNull("container"), name, arity, null, false, false).GetPublicSymbol();
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0006A4C2 File Offset: 0x000686C2
		protected override INamespaceSymbol CommonCreateErrorNamespaceSymbol(INamespaceSymbol container, string name)
		{
			return new MissingNamespaceSymbol(container.EnsureCSharpSymbolOrNull("container"), name).GetPublicSymbol();
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0006A4DA File Offset: 0x000686DA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static CSharpCompilation Create(string assemblyName, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxTree> syntaxTrees = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<MetadataReference> references = null, CSharpCompilationOptions options = null)
		{
			return CSharpCompilation.Create(assemblyName, options ?? CSharpCompilation.s_defaultOptions, syntaxTrees, references, null, null, null, false);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0006A4F4 File Offset: 0x000686F4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static CSharpCompilation CreateScriptCompilation([global::System.Runtime.CompilerServices.Nullable(1)] string assemblyName, SyntaxTree syntaxTree = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<MetadataReference> references = null, CSharpCompilationOptions options = null, CSharpCompilation previousScriptCompilation = null, Type returnType = null, Type globalsType = null)
		{
			Compilation.CheckSubmissionOptions(options);
			Compilation.ValidateScriptCompilationParameters(previousScriptCompilation, returnType, ref globalsType);
			CSharpCompilationOptions csharpCompilationOptions = ((options != null) ? options.WithReferencesSupersedeLowerVersions(true) : null) ?? CSharpCompilation.s_defaultSubmissionOptions;
			IEnumerable<SyntaxTree> enumerable;
			if (syntaxTree == null)
			{
				enumerable = SpecializedCollections.EmptyEnumerable<SyntaxTree>();
			}
			else
			{
				IEnumerable<SyntaxTree> enumerable2 = new SyntaxTree[] { syntaxTree };
				enumerable = enumerable2;
			}
			return CSharpCompilation.Create(assemblyName, csharpCompilationOptions, enumerable, references, previousScriptCompilation, returnType, globalsType, true);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0006A54C File Offset: 0x0006874C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		private static CSharpCompilation Create(string assemblyName, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilationOptions options, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxTree> syntaxTrees, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<MetadataReference> references, CSharpCompilation previousSubmission, Type returnType, Type hostObjectType, bool isSubmission)
		{
			ImmutableArray<MetadataReference> immutableArray = Compilation.ValidateReferences<CSharpCompilationReference>(references);
			CSharpCompilation csharpCompilation = new CSharpCompilation(assemblyName, options, immutableArray, previousSubmission, returnType, hostObjectType, isSubmission, null, false, new SyntaxAndDeclarationManager(ImmutableArray<SyntaxTree>.Empty, options.ScriptClassName, options.SourceReferenceResolver, Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, isSubmission, null), null, null);
			if (syntaxTrees != null)
			{
				csharpCompilation = csharpCompilation.AddSyntaxTrees(syntaxTrees);
			}
			return csharpCompilation;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0006A5A0 File Offset: 0x000687A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private CSharpCompilation(string assemblyName, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilationOptions options, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> references, CSharpCompilation previousSubmission, Type submissionReturnType, Type hostObjectType, bool isSubmission, CSharpCompilation.ReferenceManager referenceManager, bool reuseReferenceManager, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxAndDeclarationManager syntaxAndDeclarations, SemanticModelProvider semanticModelProvider, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] AsyncQueue<CompilationEvent> eventQueue = null)
			: this(assemblyName, options, references, previousSubmission, submissionReturnType, hostObjectType, isSubmission, referenceManager, reuseReferenceManager, syntaxAndDeclarations, Compilation.SyntaxTreeCommonFeatures(syntaxAndDeclarations.ExternalSyntaxTrees), semanticModelProvider, eventQueue)
		{
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0006A5DC File Offset: 0x000687DC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private CSharpCompilation(string assemblyName, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilationOptions options, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> references, CSharpCompilation previousSubmission, Type submissionReturnType, Type hostObjectType, bool isSubmission, CSharpCompilation.ReferenceManager referenceManager, bool reuseReferenceManager, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxAndDeclarationManager syntaxAndDeclarations, [global::System.Runtime.CompilerServices.Nullable(1)] IReadOnlyDictionary<string, string> features, SemanticModelProvider semanticModelProvider, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] AsyncQueue<CompilationEvent> eventQueue = null)
			: base(assemblyName, references, features, isSubmission, semanticModelProvider, eventQueue)
		{
			this.WellKnownMemberSignatureComparer = new CSharpCompilation.WellKnownMembersSignatureComparer(this);
			this._options = options;
			this.builtInOperators = new BuiltInOperators(this);
			this._scriptClass = new Lazy<ImplicitNamedTypeSymbol>(new Func<ImplicitNamedTypeSymbol>(this.BindScriptClass));
			this._globalImports = new Lazy<ImmutableArray<NamespaceOrTypeAndUsingDirective>>(new Func<ImmutableArray<NamespaceOrTypeAndUsingDirective>>(this.BindGlobalImports));
			this._usingsFromOptions = new Lazy<CSharpCompilation.UsingsFromOptionsAndDiagnostics>(new Func<CSharpCompilation.UsingsFromOptionsAndDiagnostics>(this.BindUsingsFromOptions));
			this._previousSubmissionImports = new Lazy<Imports>(new Func<Imports>(this.ExpandPreviousSubmissionImports));
			this._globalNamespaceAlias = new Lazy<Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol>(new Func<Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol>(this.CreateGlobalNamespaceAlias));
			this._anonymousTypeManager = new AnonymousTypeManager(this);
			this.LanguageVersion = CSharpCompilation.CommonLanguageVersion(syntaxAndDeclarations.ExternalSyntaxTrees);
			if (isSubmission)
			{
				this.ScriptCompilationInfo = new CSharpScriptCompilationInfo(previousSubmission, submissionReturnType, hostObjectType);
			}
			if (reuseReferenceManager)
			{
				if (referenceManager == null)
				{
					throw new ArgumentNullException("referenceManager");
				}
				this._referenceManager = referenceManager;
			}
			else
			{
				this._referenceManager = new CSharpCompilation.ReferenceManager(base.MakeSourceAssemblySimpleName(), this.Options.AssemblyIdentityComparer, (referenceManager != null) ? referenceManager.ObservedMetadata : null);
			}
			this._syntaxAndDeclarations = syntaxAndDeclarations;
			if (base.EventQueue != null)
			{
				base.EventQueue.TryEnqueue(new CompilationStartedEvent(this));
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0006A740 File Offset: 0x00068940
		internal override void ValidateDebugEntryPoint(IMethodSymbol debugEntryPoint, DiagnosticBag diagnostics)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol methodSymbol = debugEntryPoint as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.MethodSymbol;
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol2 = ((methodSymbol != null) ? methodSymbol.UnderlyingMethodSymbol : null);
			if (((methodSymbol2 != null) ? methodSymbol2.DeclaringCompilation : null) != this || !methodSymbol2.IsDefinition)
			{
				diagnostics.Add(ErrorCode.ERR_DebugEntryPointNotSourceMethodDefinition, Location.None);
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0006A788 File Offset: 0x00068988
		private static LanguageVersion CommonLanguageVersion([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxTree> syntaxTrees)
		{
			LanguageVersion? languageVersion = default(LanguageVersion?);
			LanguageVersion? languageVersion3;
			foreach (SyntaxTree syntaxTree in syntaxTrees)
			{
				LanguageVersion languageVersion2 = ((CSharpParseOptions)syntaxTree.Options).LanguageVersion;
				if (languageVersion == null)
				{
					languageVersion = new LanguageVersion?(languageVersion2);
				}
				else
				{
					languageVersion3 = languageVersion;
					LanguageVersion languageVersion4 = languageVersion2;
					if (!((languageVersion3.GetValueOrDefault() == languageVersion4) & (languageVersion3 != null)))
					{
						throw new ArgumentException(CodeAnalysisResources.InconsistentLanguageVersions, "syntaxTrees");
					}
				}
			}
			languageVersion3 = languageVersion;
			if (languageVersion3 == null)
			{
				return LanguageVersion.Default.MapSpecifiedToEffectiveVersion();
			}
			return languageVersion3.GetValueOrDefault();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0006A820 File Offset: 0x00068A20
		public new CSharpCompilation Clone()
		{
			return new CSharpCompilation(base.AssemblyName, this._options, base.ExternalReferences, this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, this._referenceManager, true, this._syntaxAndDeclarations, base.SemanticModelProvider, null);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0006A870 File Offset: 0x00068A70
		private CSharpCompilation Update(CSharpCompilation.ReferenceManager referenceManager, bool reuseReferenceManager, SyntaxAndDeclarationManager syntaxAndDeclarations)
		{
			return new CSharpCompilation(base.AssemblyName, this._options, base.ExternalReferences, this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, referenceManager, reuseReferenceManager, syntaxAndDeclarations, base.SemanticModelProvider, null);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0006A8B8 File Offset: 0x00068AB8
		public new CSharpCompilation WithAssemblyName([global::System.Runtime.CompilerServices.Nullable(2)] string assemblyName)
		{
			return new CSharpCompilation(assemblyName, this._options, base.ExternalReferences, this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, this._referenceManager, assemblyName == base.AssemblyName, this._syntaxAndDeclarations, base.SemanticModelProvider, null);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0006A910 File Offset: 0x00068B10
		public new CSharpCompilation WithReferences([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<MetadataReference> references)
		{
			return new CSharpCompilation(base.AssemblyName, this._options, Compilation.ValidateReferences<CSharpCompilationReference>(references), this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, null, false, this._syntaxAndDeclarations, base.SemanticModelProvider, null);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0006A95B File Offset: 0x00068B5B
		public new CSharpCompilation WithReferences(params MetadataReference[] references)
		{
			return this.WithReferences(references);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0006A964 File Offset: 0x00068B64
		public CSharpCompilation WithOptions(CSharpCompilationOptions options)
		{
			CSharpCompilationOptions options2 = this.Options;
			bool flag = options2.CanReuseCompilationReferenceManager(options);
			bool flag2 = options2.ScriptClassName == options.ScriptClassName && options2.SourceReferenceResolver == options.SourceReferenceResolver;
			return new CSharpCompilation(base.AssemblyName, options, base.ExternalReferences, this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, this._referenceManager, flag, flag2 ? this._syntaxAndDeclarations : new SyntaxAndDeclarationManager(this._syntaxAndDeclarations.ExternalSyntaxTrees, options.ScriptClassName, options.SourceReferenceResolver, this._syntaxAndDeclarations.MessageProvider, this._syntaxAndDeclarations.IsSubmission, null), base.SemanticModelProvider, null);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0006AA1C File Offset: 0x00068C1C
		public CSharpCompilation WithScriptCompilationInfo([global::System.Runtime.CompilerServices.Nullable(2)] CSharpScriptCompilationInfo info)
		{
			if (info == this.ScriptCompilationInfo)
			{
				return this;
			}
			CSharpScriptCompilationInfo scriptCompilationInfo = this.ScriptCompilationInfo;
			bool flag = ((scriptCompilationInfo != null) ? scriptCompilationInfo.PreviousScriptCompilation : null) == ((info != null) ? info.PreviousScriptCompilation : null);
			return new CSharpCompilation(base.AssemblyName, this._options, base.ExternalReferences, (info != null) ? info.PreviousScriptCompilation : null, (info != null) ? info.ReturnTypeOpt : null, (info != null) ? info.GlobalsType : null, info != null, this._referenceManager, flag, this._syntaxAndDeclarations, base.SemanticModelProvider, null);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0006AAA8 File Offset: 0x00068CA8
		internal override Compilation WithSemanticModelProvider([global::System.Runtime.CompilerServices.Nullable(2)] SemanticModelProvider semanticModelProvider)
		{
			if (base.SemanticModelProvider == semanticModelProvider)
			{
				return this;
			}
			return new CSharpCompilation(base.AssemblyName, this._options, base.ExternalReferences, this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, this._referenceManager, true, this._syntaxAndDeclarations, semanticModelProvider, null);
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0006AB00 File Offset: 0x00068D00
		internal override Compilation WithEventQueue([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] AsyncQueue<CompilationEvent> eventQueue)
		{
			return new CSharpCompilation(base.AssemblyName, this._options, base.ExternalReferences, this.PreviousSubmission, base.SubmissionReturnType, base.HostObjectType, base.IsSubmission, this._referenceManager, true, this._syntaxAndDeclarations, base.SemanticModelProvider, eventQueue);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0006AB50 File Offset: 0x00068D50
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new CSharpScriptCompilationInfo ScriptCompilationInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0006AB58 File Offset: 0x00068D58
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override ScriptCompilationInfo CommonScriptCompilationInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ScriptCompilationInfo;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0006AB60 File Offset: 0x00068D60
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal CSharpCompilation PreviousSubmission
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				CSharpScriptCompilationInfo scriptCompilationInfo = this.ScriptCompilationInfo;
				if (scriptCompilationInfo == null)
				{
					return null;
				}
				return scriptCompilationInfo.PreviousScriptCompilation;
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0006AB74 File Offset: 0x00068D74
		internal override bool HasSubmissionResult()
		{
			SyntaxTree syntaxTree = this._syntaxAndDeclarations.ExternalSyntaxTrees.SingleOrDefault<SyntaxTree>();
			if (syntaxTree == null)
			{
				return false;
			}
			CompilationUnitSyntax compilationUnitRoot = syntaxTree.GetCompilationUnitRoot(default(CancellationToken));
			if (compilationUnitRoot.HasErrors)
			{
				return false;
			}
			if (Enumerable.Any<SyntaxNode>(compilationUnitRoot.DescendantNodes((SyntaxNode n) => n is GlobalStatementSyntax || n is StatementSyntax || n is CompilationUnitSyntax, false), (SyntaxNode n) => n.IsKind(SyntaxKind.ReturnStatement)))
			{
				return true;
			}
			GlobalStatementSyntax globalStatementSyntax = (GlobalStatementSyntax)Enumerable.LastOrDefault<MemberDeclarationSyntax>(compilationUnitRoot.Members, (MemberDeclarationSyntax m) => m.IsKind(SyntaxKind.GlobalStatement));
			if (globalStatementSyntax != null)
			{
				StatementSyntax statement = globalStatementSyntax.Statement;
				if (statement.IsKind(SyntaxKind.ExpressionStatement))
				{
					ExpressionStatementSyntax expressionStatementSyntax = (ExpressionStatementSyntax)statement;
					if (expressionStatementSyntax.SemicolonToken.IsMissing)
					{
						SemanticModel semanticModel = base.GetSemanticModel(syntaxTree, false);
						ExpressionSyntax expression = expressionStatementSyntax.Expression;
						ITypeSymbol convertedType = semanticModel.GetTypeInfo(expression, default(CancellationToken)).ConvertedType;
						return convertedType == null || convertedType.SpecialType != SpecialType.System_Void;
					}
				}
			}
			return false;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0006ACA5 File Offset: 0x00068EA5
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public new ImmutableArray<SyntaxTree> SyntaxTrees
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._syntaxAndDeclarations.GetLazyState().SyntaxTrees;
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0006ACB7 File Offset: 0x00068EB7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public new bool ContainsSyntaxTree(SyntaxTree syntaxTree)
		{
			return syntaxTree != null && this._syntaxAndDeclarations.GetLazyState().RootNamespaces.ContainsKey(syntaxTree);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0006ACD4 File Offset: 0x00068ED4
		public new CSharpCompilation AddSyntaxTrees(params SyntaxTree[] trees)
		{
			return this.AddSyntaxTrees(trees);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0006ACE0 File Offset: 0x00068EE0
		public new CSharpCompilation AddSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			if (trees == null)
			{
				throw new ArgumentNullException("trees");
			}
			if (trees.IsEmpty<SyntaxTree>())
			{
				return this;
			}
			PooledHashSet<SyntaxTree> instance = PooledHashSet<SyntaxTree>.GetInstance();
			SyntaxAndDeclarationManager syntaxAndDeclarationManager = this._syntaxAndDeclarations;
			instance.AddAll<SyntaxTree>(syntaxAndDeclarationManager.ExternalSyntaxTrees);
			bool flag = true;
			int num = 0;
			foreach (CSharpSyntaxTree csharpSyntaxTree in Enumerable.Cast<CSharpSyntaxTree>(trees))
			{
				if (csharpSyntaxTree == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "trees", num));
				}
				if (!csharpSyntaxTree.HasCompilationUnitRoot)
				{
					throw new ArgumentException(CSharpResources.TreeMustHaveARootNodeWith, string.Format("{0}[{1}]", "trees", num));
				}
				if (instance.Contains(csharpSyntaxTree))
				{
					throw new ArgumentException(CSharpResources.SyntaxTreeAlreadyPresent, string.Format("{0}[{1}]", "trees", num));
				}
				if (base.IsSubmission && csharpSyntaxTree.Options.Kind == SourceCodeKind.Regular)
				{
					throw new ArgumentException(CSharpResources.SubmissionCanOnlyInclude, string.Format("{0}[{1}]", "trees", num));
				}
				instance.Add(csharpSyntaxTree);
				flag &= !csharpSyntaxTree.HasReferenceOrLoadDirectives;
				num++;
			}
			instance.Free();
			if (base.IsSubmission && num > 1)
			{
				throw new ArgumentException(CSharpResources.SubmissionCanHaveAtMostOne, "trees");
			}
			syntaxAndDeclarationManager = syntaxAndDeclarationManager.AddSyntaxTrees(trees);
			return this.Update(this._referenceManager, flag, syntaxAndDeclarationManager);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0006AE64 File Offset: 0x00069064
		public new CSharpCompilation RemoveSyntaxTrees(params SyntaxTree[] trees)
		{
			return this.RemoveSyntaxTrees(trees);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0006AE70 File Offset: 0x00069070
		public new CSharpCompilation RemoveSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			if (trees == null)
			{
				throw new ArgumentNullException("trees");
			}
			if (trees.IsEmpty<SyntaxTree>())
			{
				return this;
			}
			PooledHashSet<SyntaxTree> instance = PooledHashSet<SyntaxTree>.GetInstance();
			PooledHashSet<SyntaxTree> instance2 = PooledHashSet<SyntaxTree>.GetInstance();
			SyntaxAndDeclarationManager syntaxAndDeclarationManager = this._syntaxAndDeclarations;
			instance2.AddAll<SyntaxTree>(syntaxAndDeclarationManager.ExternalSyntaxTrees);
			bool flag = true;
			int num = 0;
			foreach (CSharpSyntaxTree csharpSyntaxTree in Enumerable.Cast<CSharpSyntaxTree>(trees))
			{
				if (!instance2.Contains(csharpSyntaxTree))
				{
					ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap = syntaxAndDeclarationManager.GetLazyState().LoadedSyntaxTreeMap;
					if (SyntaxAndDeclarationManager.IsLoadedSyntaxTree(csharpSyntaxTree, loadedSyntaxTreeMap))
					{
						throw new ArgumentException(CSharpResources.SyntaxTreeFromLoadNoRemoveReplace, string.Format("{0}[{1}]", "trees", num));
					}
					throw new ArgumentException(CSharpResources.SyntaxTreeNotFoundToRemove, string.Format("{0}[{1}]", "trees", num));
				}
				else
				{
					instance.Add(csharpSyntaxTree);
					flag &= !csharpSyntaxTree.HasReferenceOrLoadDirectives;
					num++;
				}
			}
			instance2.Free();
			syntaxAndDeclarationManager = syntaxAndDeclarationManager.RemoveSyntaxTrees(instance);
			instance.Free();
			return this.Update(this._referenceManager, flag, syntaxAndDeclarationManager);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0006AFA4 File Offset: 0x000691A4
		public new CSharpCompilation RemoveAllSyntaxTrees()
		{
			SyntaxAndDeclarationManager syntaxAndDeclarations = this._syntaxAndDeclarations;
			return this.Update(this._referenceManager, !syntaxAndDeclarations.MayHaveReferenceDirectives(), syntaxAndDeclarations.WithExternalSyntaxTrees(ImmutableArray<SyntaxTree>.Empty));
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0006AFD8 File Offset: 0x000691D8
		public new CSharpCompilation ReplaceSyntaxTree(SyntaxTree oldTree, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTree newTree)
		{
			oldTree = (CSharpSyntaxTree)oldTree;
			newTree = (CSharpSyntaxTree)newTree;
			if (oldTree == null)
			{
				throw new ArgumentNullException("oldTree");
			}
			if (newTree == null)
			{
				return this.RemoveSyntaxTrees(new SyntaxTree[] { oldTree });
			}
			if (newTree == oldTree)
			{
				return this;
			}
			if (!newTree.HasCompilationUnitRoot)
			{
				throw new ArgumentException(CSharpResources.TreeMustHaveARootNodeWith, "newTree");
			}
			SyntaxAndDeclarationManager syntaxAndDeclarationManager = this._syntaxAndDeclarations;
			ImmutableArray<SyntaxTree> externalSyntaxTrees = syntaxAndDeclarationManager.ExternalSyntaxTrees;
			if (!externalSyntaxTrees.Contains(oldTree))
			{
				ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap = syntaxAndDeclarationManager.GetLazyState().LoadedSyntaxTreeMap;
				if (SyntaxAndDeclarationManager.IsLoadedSyntaxTree(oldTree, loadedSyntaxTreeMap))
				{
					throw new ArgumentException(CSharpResources.SyntaxTreeFromLoadNoRemoveReplace, "oldTree");
				}
				throw new ArgumentException(CSharpResources.SyntaxTreeNotFoundToRemove, "oldTree");
			}
			else
			{
				if (externalSyntaxTrees.Contains(newTree))
				{
					throw new ArgumentException(CSharpResources.SyntaxTreeAlreadyPresent, "newTree");
				}
				bool flag = !oldTree.HasReferenceOrLoadDirectives() && !newTree.HasReferenceOrLoadDirectives();
				syntaxAndDeclarationManager = syntaxAndDeclarationManager.ReplaceSyntaxTree(oldTree, newTree);
				return this.Update(this._referenceManager, flag, syntaxAndDeclarationManager);
			}
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0006B0C8 File Offset: 0x000692C8
		internal override int GetSyntaxTreeOrdinal(SyntaxTree tree)
		{
			return this._syntaxAndDeclarations.GetLazyState().OrdinalMap[tree];
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0006B0E0 File Offset: 0x000692E0
		internal override CommonReferenceManager CommonGetBoundReferenceManager()
		{
			return this.GetBoundReferenceManager();
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0006B0E8 File Offset: 0x000692E8
		internal new CSharpCompilation.ReferenceManager GetBoundReferenceManager()
		{
			if (this._lazyAssemblySymbol == null)
			{
				this._referenceManager.CreateSourceAssemblyForCompilation(this);
			}
			return this._referenceManager;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0006B104 File Offset: 0x00069304
		internal bool ReferenceManagerEquals(CSharpCompilation other)
		{
			return this._referenceManager == other._referenceManager;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0006B114 File Offset: 0x00069314
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MetadataReference> DirectiveReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.GetBoundReferenceManager().DirectiveReferences;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0006B121 File Offset: 0x00069321
		[TupleElementNames(new string[] { "path", "content" })]
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1, 1 })]
		internal override IDictionary<ValueTuple<string, string>, MetadataReference> ReferenceDirectiveMap
		{
			[return: TupleElementNames(new string[] { "path", "content" })]
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1, 1 })]
			get
			{
				return this.GetBoundReferenceManager().ReferenceDirectiveMap;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0006B12E File Offset: 0x0006932E
		internal IEnumerable<string> ExternAliases
		{
			get
			{
				return this.GetBoundReferenceManager().ExternAliases;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0006B13C File Offset: 0x0006933C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal new Symbol GetAssemblyOrModuleSymbol(MetadataReference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			if (reference.Properties.Kind == MetadataImageKind.Assembly)
			{
				return this.GetBoundReferenceManager().GetReferencedAssemblySymbol(reference);
			}
			int referencedModuleIndex = this.GetBoundReferenceManager().GetReferencedModuleIndex(reference);
			if (referencedModuleIndex >= 0)
			{
				return this.Assembly.Modules[referencedModuleIndex];
			}
			return null;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0006B19B File Offset: 0x0006939B
		public override IEnumerable<AssemblyIdentity> ReferencedAssemblyNames
		{
			get
			{
				return Enumerable.SelectMany<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol, AssemblyIdentity>(this.Assembly.Modules, (Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol module) => module.GetReferencedAssemblies());
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x0006B1D1 File Offset: 0x000693D1
		internal override IEnumerable<ReferenceDirective> ReferenceDirectives
		{
			get
			{
				return this.Declarations.ReferenceDirectives;
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0006B1E0 File Offset: 0x000693E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public MetadataReference GetDirectiveReference(ReferenceDirectiveTriviaSyntax directive)
		{
			MetadataReference metadataReference;
			if (!this.ReferenceDirectiveMap.TryGetValue(new ValueTuple<string, string>(directive.SyntaxTree.FilePath, directive.File.ValueText), ref metadataReference))
			{
				return null;
			}
			return metadataReference;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0006B21D File Offset: 0x0006941D
		public new CSharpCompilation AddReferences(params MetadataReference[] references)
		{
			return (CSharpCompilation)base.AddReferences(references);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0006B22B File Offset: 0x0006942B
		public new CSharpCompilation AddReferences(IEnumerable<MetadataReference> references)
		{
			return (CSharpCompilation)base.AddReferences(references);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0006B239 File Offset: 0x00069439
		public new CSharpCompilation RemoveReferences(params MetadataReference[] references)
		{
			return (CSharpCompilation)base.RemoveReferences(references);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0006B247 File Offset: 0x00069447
		public new CSharpCompilation RemoveReferences(IEnumerable<MetadataReference> references)
		{
			return (CSharpCompilation)base.RemoveReferences(references);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0006B255 File Offset: 0x00069455
		public new CSharpCompilation RemoveAllReferences()
		{
			return (CSharpCompilation)base.RemoveAllReferences();
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0006B262 File Offset: 0x00069462
		public new CSharpCompilation ReplaceReference(MetadataReference oldReference, MetadataReference newReference)
		{
			return (CSharpCompilation)base.ReplaceReference(oldReference, newReference);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0006B271 File Offset: 0x00069471
		public override CompilationReference ToMetadataReference([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases = default(ImmutableArray<string>), bool embedInteropTypes = false)
		{
			return new CSharpCompilationReference(this, aliases, embedInteropTypes);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0006B27C File Offset: 0x0006947C
		private void GetAllUnaliasedModules(ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules)
		{
			modules.AddRange(this.Assembly.Modules);
			CSharpCompilation.ReferenceManager boundReferenceManager = this.GetBoundReferenceManager();
			for (int i = 0; i < boundReferenceManager.ReferencedAssemblies.Length; i++)
			{
				if (boundReferenceManager.DeclarationsAccessibleWithoutAlias(i))
				{
					modules.AddRange(boundReferenceManager.ReferencedAssemblies[i].Modules);
				}
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0006B2E0 File Offset: 0x000694E0
		internal void GetUnaliasedReferencedAssemblies(ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> assemblies)
		{
			CSharpCompilation.ReferenceManager boundReferenceManager = this.GetBoundReferenceManager();
			for (int i = 0; i < boundReferenceManager.ReferencedAssemblies.Length; i++)
			{
				if (boundReferenceManager.DeclarationsAccessibleWithoutAlias(i))
				{
					assemblies.Add(boundReferenceManager.ReferencedAssemblies[i]);
				}
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0006B32B File Offset: 0x0006952B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public new MetadataReference GetMetadataReference(IAssemblySymbol assemblySymbol)
		{
			return base.GetMetadataReference(assemblySymbol);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0006B334 File Offset: 0x00069534
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private protected override MetadataReference CommonGetMetadataReference(IAssemblySymbol assemblySymbol)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.AssemblySymbol assemblySymbol2 = assemblySymbol as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.AssemblySymbol;
			if (assemblySymbol2 != null)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol underlyingAssemblySymbol = assemblySymbol2.UnderlyingAssemblySymbol;
				return this.GetMetadataReference(underlyingAssemblySymbol);
			}
			return null;
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0006B35B File Offset: 0x0006955B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal MetadataReference GetMetadataReference(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol)
		{
			return this.GetBoundReferenceManager().GetMetadataReference(assemblySymbol);
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0006B369 File Offset: 0x00069569
		internal Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol SourceAssembly
		{
			get
			{
				this.GetBoundReferenceManager();
				return this._lazyAssemblySymbol;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0006B378 File Offset: 0x00069578
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol Assembly
		{
			get
			{
				return this.SourceAssembly;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0006B380 File Offset: 0x00069580
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol SourceModule
		{
			get
			{
				return this.Assembly.Modules[0];
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0006B3A4 File Offset: 0x000695A4
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol GlobalNamespace
		{
			get
			{
				if (this._lazyGlobalNamespace == null)
				{
					ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> instance = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol>.GetInstance();
					this.GetAllUnaliasedModules(instance);
					Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol namespaceSymbol = MergedNamespaceSymbol.Create(new NamespaceExtent(this), null, instance.SelectDistinct<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol>(([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol m) => m.GlobalNamespace), null);
					instance.Free();
					Interlocked.CompareExchange<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol>(ref this._lazyGlobalNamespace, namespaceSymbol, null);
				}
				return this._lazyGlobalNamespace;
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0006B414 File Offset: 0x00069614
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol GetCompilationNamespace(INamespaceSymbol namespaceSymbol)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.NamespaceSymbol namespaceSymbol2 = namespaceSymbol as Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.NamespaceSymbol;
			if (namespaceSymbol2 != null && namespaceSymbol.NamespaceKind == NamespaceKind.Compilation && namespaceSymbol.ContainingCompilation == this)
			{
				return namespaceSymbol2.UnderlyingNamespaceSymbol;
			}
			INamespaceSymbol containingNamespace = namespaceSymbol.ContainingNamespace;
			if (containingNamespace == null)
			{
				return this.GlobalNamespace;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol compilationNamespace = this.GetCompilationNamespace(containingNamespace);
			if (compilationNamespace != null)
			{
				return compilationNamespace.GetNestedNamespace(namespaceSymbol.Name);
			}
			return null;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0006B470 File Offset: 0x00069670
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol GetCompilationNamespace(Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol namespaceSymbol)
		{
			if (namespaceSymbol.NamespaceKind == NamespaceKind.Compilation && namespaceSymbol.ContainingCompilation == this)
			{
				return namespaceSymbol;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol containingNamespace = namespaceSymbol.ContainingNamespace;
			if (containingNamespace == null)
			{
				return this.GlobalNamespace;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol compilationNamespace = this.GetCompilationNamespace(containingNamespace);
			if (compilationNamespace != null)
			{
				return compilationNamespace.GetNestedNamespace(namespaceSymbol.Name);
			}
			return null;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0006B4C4 File Offset: 0x000696C4
		internal bool GetExternAliasTarget(string aliasName, out Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol @namespace)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol namespaceSymbol;
			if (this._externAliasTargets == null)
			{
				Interlocked.CompareExchange<ConcurrentDictionary<string, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol>>(ref this._externAliasTargets, new ConcurrentDictionary<string, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol>(), null);
			}
			else if (this._externAliasTargets.TryGetValue(aliasName, ref namespaceSymbol))
			{
				@namespace = namespaceSymbol;
				return !(@namespace is MissingNamespaceSymbol);
			}
			ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol> arrayBuilder = null;
			CSharpCompilation.ReferenceManager boundReferenceManager = this.GetBoundReferenceManager();
			for (int i = 0; i < boundReferenceManager.ReferencedAssemblies.Length; i++)
			{
				if (boundReferenceManager.AliasesOfReferencedAssemblies[i].Contains(aliasName))
				{
					arrayBuilder = arrayBuilder ?? ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol>.GetInstance();
					arrayBuilder.Add(boundReferenceManager.ReferencedAssemblies[i].GlobalNamespace);
				}
			}
			bool flag = arrayBuilder != null;
			@namespace = (flag ? MergedNamespaceSymbol.Create(new NamespaceExtent(this), null, arrayBuilder.ToImmutableAndFree(), null) : new MissingNamespaceSymbol(new MissingModuleSymbol(new MissingAssemblySymbol(new AssemblyIdentity(Guid.NewGuid().ToString(), null, null, default(ImmutableArray<byte>), false, false, 0)), -1)));
			@namespace = this._externAliasTargets.GetOrAdd(aliasName, @namespace);
			return flag;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0006B5E1 File Offset: 0x000697E1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol ScriptClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this._scriptClass.Value;
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0006B5EE File Offset: 0x000697EE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private ImplicitNamedTypeSymbol BindScriptClass()
		{
			return (ImplicitNamedTypeSymbol)base.CommonBindScriptClass().GetSymbol();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0006B600 File Offset: 0x00069800
		internal bool IsSubmissionSyntaxTree(SyntaxTree tree)
		{
			return base.IsSubmission && tree == this._syntaxAndDeclarations.ExternalSyntaxTrees.SingleOrDefault<SyntaxTree>();
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0006B61F File Offset: 0x0006981F
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal ImmutableArray<NamespaceOrTypeAndUsingDirective> GlobalImports
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				return this._globalImports.Value;
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0006B62C File Offset: 0x0006982C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private ImmutableArray<NamespaceOrTypeAndUsingDirective> BindGlobalImports()
		{
			CSharpCompilation.UsingsFromOptionsAndDiagnostics usingsFromOptions = this.UsingsFromOptions;
			CSharpCompilation previousSubmission = this.PreviousSubmission;
			ImmutableArray<NamespaceOrTypeAndUsingDirective> immutableArray = ((previousSubmission != null) ? Imports.ExpandPreviousSubmissionImports(previousSubmission.GlobalImports, this) : ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty);
			if (usingsFromOptions.UsingNamespacesOrTypes.IsEmpty)
			{
				return immutableArray;
			}
			if (immutableArray.IsEmpty)
			{
				return usingsFromOptions.UsingNamespacesOrTypes;
			}
			ArrayBuilder<NamespaceOrTypeAndUsingDirective> instance = ArrayBuilder<NamespaceOrTypeAndUsingDirective>.GetInstance();
			PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol> instance2 = PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol>.GetInstance();
			instance.AddRange(usingsFromOptions.UsingNamespacesOrTypes);
			instance2.AddAll<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol>(usingsFromOptions.UsingNamespacesOrTypes.Select<NamespaceOrTypeAndUsingDirective, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol>((NamespaceOrTypeAndUsingDirective unt) => unt.NamespaceOrType));
			foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in immutableArray)
			{
				if (instance2.Add(namespaceOrTypeAndUsingDirective.NamespaceOrType))
				{
					instance.Add(namespaceOrTypeAndUsingDirective);
				}
			}
			instance2.Free();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0006B70E File Offset: 0x0006990E
		private CSharpCompilation.UsingsFromOptionsAndDiagnostics UsingsFromOptions
		{
			get
			{
				return this._usingsFromOptions.Value;
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0006B71B File Offset: 0x0006991B
		private CSharpCompilation.UsingsFromOptionsAndDiagnostics BindUsingsFromOptions()
		{
			return CSharpCompilation.UsingsFromOptionsAndDiagnostics.FromOptions(this);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0006B724 File Offset: 0x00069924
		internal Imports GetSubmissionImports()
		{
			SyntaxTree syntaxTree = this._syntaxAndDeclarations.ExternalSyntaxTrees.SingleOrDefault<SyntaxTree>();
			if (syntaxTree == null)
			{
				return Imports.Empty;
			}
			return ((SourceNamespaceSymbol)this.SourceModule.GlobalNamespace).GetImports((CSharpSyntaxNode)syntaxTree.GetRoot(default(CancellationToken)), null);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0006B775 File Offset: 0x00069975
		internal Imports GetPreviousSubmissionImports()
		{
			return this._previousSubmissionImports.Value;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0006B784 File Offset: 0x00069984
		private Imports ExpandPreviousSubmissionImports()
		{
			CSharpCompilation previousSubmission = this.PreviousSubmission;
			if (previousSubmission == null)
			{
				return Imports.Empty;
			}
			return Imports.ExpandPreviousSubmissionImports(previousSubmission.GetPreviousSubmissionImports(), this).Concat(Imports.ExpandPreviousSubmissionImports(previousSubmission.GetSubmissionImports(), this));
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0006B7BE File Offset: 0x000699BE
		internal Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol GlobalNamespaceAlias
		{
			get
			{
				return this._globalNamespaceAlias.Value;
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0006B7CC File Offset: 0x000699CC
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol GetSpecialType(SpecialType specialType)
		{
			if (specialType <= SpecialType.None || specialType > SpecialType.System_Runtime_CompilerServices_PreserveBaseOverridesAttribute)
			{
				throw new ArgumentOutOfRangeException("specialType", string.Format("Unexpected SpecialType: '{0}'.", (int)specialType));
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol;
			if (base.IsTypeMissing(specialType))
			{
				MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(specialType.GetMetadataName(), true, -1);
				namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(this.Assembly.CorLibrary.Modules[0], ref metadataTypeName, specialType, null);
			}
			else
			{
				namedTypeSymbol = this.Assembly.GetSpecialType(specialType);
			}
			return namedTypeSymbol;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0006B847 File Offset: 0x00069A47
		internal Symbol GetSpecialTypeMember(SpecialMember specialMember)
		{
			return this.Assembly.GetSpecialTypeMember(specialMember);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0006B855 File Offset: 0x00069A55
		internal override ISymbolInternal CommonGetSpecialTypeMember(SpecialMember specialMember)
		{
			return this.GetSpecialTypeMember(specialMember);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0006B860 File Offset: 0x00069A60
		internal Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetTypeByReflectionType(Type type, BindingDiagnosticBag diagnostics)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = this.Assembly.GetTypeByReflectionType(type, true);
			if (typeSymbol == null)
			{
				ExtendedErrorTypeSymbol extendedErrorTypeSymbol = new ExtendedErrorTypeSymbol(this, type.Name, 0, CSharpCompilation.CreateReflectionTypeNotFoundError(type), false, false);
				diagnostics.Add(extendedErrorTypeSymbol.ErrorInfo, NoLocation.Singleton);
				typeSymbol = extendedErrorTypeSymbol;
			}
			return typeSymbol;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0006B8A8 File Offset: 0x00069AA8
		private static CSDiagnosticInfo CreateReflectionTypeNotFoundError(Type type)
		{
			return new CSDiagnosticInfo(ErrorCode.ERR_GlobalSingleTypeNameNotFound, new object[] { type.AssemblyQualifiedName ?? "" }, ImmutableArray<Symbol>.Empty, ImmutableArray<Location>.Empty);
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0006B8D6 File Offset: 0x00069AD6
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected override ITypeSymbol CommonScriptGlobalsType
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol hostObjectTypeSymbol = this.GetHostObjectTypeSymbol();
				if (hostObjectTypeSymbol == null)
				{
					return null;
				}
				return hostObjectTypeSymbol.GetPublicSymbol();
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0006B8EC File Offset: 0x00069AEC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetHostObjectTypeSymbol()
		{
			if (base.HostObjectType != null && this._lazyHostObjectTypeSymbol == null)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = this.Assembly.GetTypeByReflectionType(base.HostObjectType, true);
				if (typeSymbol == null)
				{
					MetadataTypeName metadataTypeName = MetadataTypeName.FromNamespaceAndTypeName(base.HostObjectType.Namespace ?? string.Empty, base.HostObjectType.Name, true, -1);
					typeSymbol = new MissingMetadataTypeSymbol.TopLevel(new MissingAssemblySymbol(AssemblyIdentity.FromAssemblyDefinition(IntrospectionExtensions.GetTypeInfo(base.HostObjectType).Assembly)).Modules[0], ref metadataTypeName, SpecialType.None, CSharpCompilation.CreateReflectionTypeNotFoundError(base.HostObjectType));
				}
				Interlocked.CompareExchange<Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol>(ref this._lazyHostObjectTypeSymbol, typeSymbol, null);
			}
			return this._lazyHostObjectTypeSymbol;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0006B9A2 File Offset: 0x00069BA2
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedInteractiveInitializerMethod GetSubmissionInitializer()
		{
			if (!base.IsSubmission || this.ScriptClass == null)
			{
				return null;
			}
			return this.ScriptClass.GetScriptInitializer();
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0006B9C4 File Offset: 0x00069BC4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol GetTypeByMetadataName(string fullyQualifiedMetadataName)
		{
			ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> valueTuple;
			return this.Assembly.GetTypeByMetadataName(fullyQualifiedMetadataName, true, false, out valueTuple, false, null, false);
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0006B9E4 File Offset: 0x00069BE4
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol DynamicType
		{
			get
			{
				return Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol.DynamicType;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0006B9EB File Offset: 0x00069BEB
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol ObjectType
		{
			get
			{
				return this.Assembly.ObjectType;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0006B9F8 File Offset: 0x00069BF8
		internal bool DeclaresTheObjectClass
		{
			get
			{
				return this.SourceAssembly.DeclaresTheObjectClass;
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0006BA05 File Offset: 0x00069C05
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol GetEntryPoint(CancellationToken cancellationToken)
		{
			return this.GetEntryPointAndDiagnostics(cancellationToken).MethodSymbol;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0006BA14 File Offset: 0x00069C14
		internal CSharpCompilation.EntryPoint GetEntryPointAndDiagnostics(CancellationToken cancellationToken)
		{
			if (this._lazyEntryPoint == null)
			{
				SynthesizedSimpleProgramEntryPointSymbol simpleProgramEntryPoint = SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this);
				CSharpCompilation.EntryPoint entryPoint;
				if (!this.Options.OutputKind.IsApplication() && this.ScriptClass == null)
				{
					if (simpleProgramEntryPoint != null)
					{
						BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
						instance.Add(ErrorCode.ERR_SimpleProgramNotAnExecutable, simpleProgramEntryPoint.ReturnTypeSyntax.Location);
						entryPoint = new CSharpCompilation.EntryPoint(null, instance.ToReadOnlyAndFree());
					}
					else
					{
						entryPoint = CSharpCompilation.EntryPoint.None;
					}
				}
				else
				{
					entryPoint = null;
					if (this.Options.MainTypeName != null && !this.Options.MainTypeName.IsValidClrTypeName())
					{
						entryPoint = CSharpCompilation.EntryPoint.None;
					}
					if (entryPoint == null)
					{
						ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> immutableBindingDiagnostic;
						entryPoint = new CSharpCompilation.EntryPoint(this.FindEntryPoint(simpleProgramEntryPoint, cancellationToken, out immutableBindingDiagnostic), immutableBindingDiagnostic);
					}
					if (this.Options.MainTypeName != null && simpleProgramEntryPoint != null)
					{
						DiagnosticBag instance2 = DiagnosticBag.GetInstance();
						instance2.Add(ErrorCode.ERR_SimpleProgramDisallowsMainType, NoLocation.Singleton);
						entryPoint = new CSharpCompilation.EntryPoint(entryPoint.MethodSymbol, new ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(entryPoint.Diagnostics.Diagnostics.Concat<Diagnostic>(instance2.ToReadOnlyAndFree()), entryPoint.Diagnostics.Dependencies));
					}
				}
				Interlocked.CompareExchange<CSharpCompilation.EntryPoint>(ref this._lazyEntryPoint, entryPoint, null);
			}
			return this._lazyEntryPoint;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0006BB38 File Offset: 0x00069D38
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol FindEntryPoint(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol simpleProgramEntryPointSymbol, CancellationToken cancellationToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> sealedDiagnostics)
		{
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance2 = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol5;
			try
			{
				string mainTypeName = this.Options.MainTypeName;
				Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol globalNamespace = this.SourceModule.GlobalNamespace;
				Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol scriptClass = this.ScriptClass;
				Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol;
				if (mainTypeName != null)
				{
					if (scriptClass != null)
					{
						instance.Add(ErrorCode.WRN_MainIgnored, NoLocation.Singleton, new object[] { mainTypeName });
						return scriptClass.GetScriptEntryPoint();
					}
					Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol namespaceOrTypeSymbol = globalNamespace.GetNamespaceOrTypeByQualifiedName(mainTypeName.Split(new char[] { '.' })).OfMinimalArity();
					if (namespaceOrTypeSymbol == null)
					{
						instance.Add(ErrorCode.ERR_MainClassNotFound, NoLocation.Singleton, new object[] { mainTypeName });
						return null;
					}
					namedTypeSymbol = namespaceOrTypeSymbol as Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol;
					if (namedTypeSymbol == null || namedTypeSymbol.IsGenericType || (namedTypeSymbol.TypeKind != TypeKind.Class && namedTypeSymbol.TypeKind != TypeKind.Struct && !namedTypeSymbol.IsInterface))
					{
						instance.Add(ErrorCode.ERR_MainClassNotClass, namespaceOrTypeSymbol.Locations.First<Location>(), new object[] { namespaceOrTypeSymbol });
						return null;
					}
					CSharpCompilation.AddEntryPointCandidates(instance2, namedTypeSymbol.GetMembersUnordered());
				}
				else
				{
					namedTypeSymbol = null;
					CSharpCompilation.AddEntryPointCandidates(instance2, this.GetSymbolsWithNameCore("Main", SymbolFilter.Member, cancellationToken));
					if (scriptClass != null || simpleProgramEntryPointSymbol != null)
					{
						foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol in instance2)
						{
							instance.Add(ErrorCode.WRN_MainIgnored, methodSymbol.Locations.First<Location>(), new object[] { methodSymbol });
						}
						if (scriptClass != null)
						{
							return scriptClass.GetScriptEntryPoint();
						}
						instance2.Clear();
						instance2.Add(simpleProgramEntryPointSymbol);
					}
				}
				ArrayBuilder<ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag>> instance3 = ArrayBuilder<ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag>>.GetInstance();
				CSharpCompilation.<>c__DisplayClass160_0 CS$<>8__locals1;
				CS$<>8__locals1.noMainFoundDiagnostics = BindingDiagnosticBag.GetInstance(instance);
				ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> instance4 = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>.GetInstance();
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol2 in instance2)
				{
					BindingDiagnosticBag instance5 = BindingDiagnosticBag.GetInstance(instance);
					ValueTuple<bool, bool> valueTuple = this.HasEntryPointSignature(methodSymbol2, instance5);
					bool item = valueTuple.Item1;
					if (valueTuple.Item2)
					{
						instance3.Add(new ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag>(item, methodSymbol2, instance5));
					}
					else
					{
						if (CSharpCompilation.<FindEntryPoint>g__checkValid|160_0(methodSymbol2, item, instance5, ref CS$<>8__locals1))
						{
							if (methodSymbol2.IsAsync)
							{
								instance.Add(ErrorCode.ERR_NonTaskMainCantBeAsync, methodSymbol2.Locations.First<Location>(), new object[] { methodSymbol2 });
							}
							else
							{
								instance.AddRange(instance5, false);
								instance4.Add(methodSymbol2);
							}
						}
						instance5.Free();
					}
				}
				if (instance4.Count == 0)
				{
					foreach (ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag> valueTuple2 in instance3)
					{
						bool item2 = valueTuple2.Item1;
						Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol item3 = valueTuple2.Item2;
						BindingDiagnosticBag item4 = valueTuple2.Item3;
						if (CSharpCompilation.<FindEntryPoint>g__checkValid|160_0(item3, item2, item4, ref CS$<>8__locals1) && Binder.CheckFeatureAvailability(item3.ExtractReturnTypeSyntax(), MessageID.IDS_FeatureAsyncMain, instance, null))
						{
							instance.AddRange(item4, false);
							instance4.Add(item3);
						}
					}
				}
				else if (this.LanguageVersion >= MessageID.IDS_FeatureAsyncMain.RequiredVersion() && instance3.Count > 0)
				{
					ImmutableArray<Symbol> immutableArray = instance3.SelectAsArray<ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag>, Symbol>(([TupleElementNames(new string[] { "IsValid", "Candidate", "SpecificDiagnostics" })] ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag> s) => s.Item2);
					ImmutableArray<Location> immutableArray2 = immutableArray.SelectAsArray<Symbol, Location>((Symbol s) => s.Locations[0]);
					foreach (Symbol symbol in immutableArray)
					{
						CSDiagnosticInfo csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.WRN_SyncAndAsyncEntryPoints, new object[]
						{
							symbol,
							instance4[0]
						}, immutableArray, immutableArray2);
						instance.Add(new CSDiagnostic(csdiagnosticInfo, symbol.Locations[0], false));
					}
				}
				foreach (ValueTuple<bool, Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, BindingDiagnosticBag> valueTuple3 in instance3)
				{
					valueTuple3.Item3.Free();
				}
				if (instance4.Count == 0)
				{
					instance.AddRange(CS$<>8__locals1.noMainFoundDiagnostics, false);
				}
				else if (namedTypeSymbol == null)
				{
					foreach (Diagnostic diagnostic in CS$<>8__locals1.noMainFoundDiagnostics.DiagnosticBag.AsEnumerable())
					{
						if (diagnostic.Code == 28 || diagnostic.Code == 402)
						{
							instance.Add(diagnostic);
						}
					}
					instance.AddDependencies(CS$<>8__locals1.noMainFoundDiagnostics, false);
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol3 = null;
				if (instance4.Count == 0)
				{
					if (namedTypeSymbol == null)
					{
						instance.Add(ErrorCode.ERR_NoEntryPoint, NoLocation.Singleton);
					}
					else
					{
						instance.Add(ErrorCode.ERR_NoMainInClass, namedTypeSymbol.Locations.First<Location>(), new object[] { namedTypeSymbol });
					}
				}
				else
				{
					foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol4 in instance4)
					{
						if (methodSymbol4.GetUnmanagedCallersOnlyAttributeData(true) != null)
						{
							instance.Add(ErrorCode.ERR_EntryPointCannotBeUnmanagedCallersOnly, methodSymbol4.Locations.First<Location>());
						}
					}
					if (instance4.Count > 1)
					{
						instance4.Sort(LexicalOrderSymbolComparer.Instance);
						CSDiagnosticInfo csdiagnosticInfo2 = new CSDiagnosticInfo(ErrorCode.ERR_MultipleEntryPoints, Array.Empty<object>(), Enumerable.OfType<Symbol>(instance4).AsImmutable<Symbol>(), Enumerable.OfType<Location>(Enumerable.Select<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Location>(instance4, (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol m) => m.Locations.First<Location>())).AsImmutable<Location>());
						instance.Add(new CSDiagnostic(csdiagnosticInfo2, instance4.First().Locations.First<Location>(), false));
					}
					else
					{
						methodSymbol3 = instance4[0];
					}
				}
				instance3.Free();
				instance4.Free();
				CS$<>8__locals1.noMainFoundDiagnostics.Free();
				methodSymbol5 = methodSymbol3;
			}
			finally
			{
				instance2.Free();
				sealedDiagnostics = instance.ToReadOnlyAndFree();
			}
			return methodSymbol5;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0006C10C File Offset: 0x0006A30C
		private static void AddEntryPointCandidates(ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> entryPointCandidates, IEnumerable<Symbol> members)
		{
			foreach (Symbol symbol in members)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = symbol as Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol;
				if (methodSymbol != null && methodSymbol.IsEntryPointCandidate)
				{
					entryPointCandidates.Add(methodSymbol);
				}
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0006C164 File Offset: 0x0006A364
		internal bool ReturnsAwaitableToVoidOrInt(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method, BindingDiagnosticBag diagnostics)
		{
			if (method.ReturnType.IsVoidType() || method.ReturnType.SpecialType == SpecialType.System_Int32)
			{
				return false;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = method.ReturnType as Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol;
			if (namedTypeSymbol == null)
			{
				return false;
			}
			if (!Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(namedTypeSymbol.ConstructedFrom, this.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task), TypeCompareKind.ConsiderEverything) && !Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(namedTypeSymbol.ConstructedFrom, this.GetWellKnownType(WellKnownType.System_Threading_Tasks_Task_T), TypeCompareKind.ConsiderEverything))
			{
				return false;
			}
			CSharpSyntaxNode csharpSyntaxNode = method.ExtractReturnTypeSyntax();
			BoundLiteral boundLiteral = new BoundLiteral(csharpSyntaxNode, ConstantValue.Null, namedTypeSymbol);
			BoundExpression boundExpression;
			return this.GetBinder(csharpSyntaxNode).GetAwaitableExpressionInfo(boundLiteral, out boundExpression, csharpSyntaxNode, diagnostics) && (boundExpression.Type.IsVoidType() || boundExpression.Type.SpecialType == SpecialType.System_Int32);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0006C218 File Offset: 0x0006A418
		[return: TupleElementNames(new string[] { "IsCandidate", "IsTaskLike" })]
		[return: global::System.Runtime.CompilerServices.Nullable(0)]
		internal ValueTuple<bool, bool> HasEntryPointSignature(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method, BindingDiagnosticBag bag)
		{
			if (method.IsVararg)
			{
				return new ValueTuple<bool, bool>(false, false);
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol returnType = method.ReturnType;
			bool flag = false;
			if (returnType.SpecialType != SpecialType.System_Int32 && !returnType.IsVoidType())
			{
				flag = this.ReturnsAwaitableToVoidOrInt(method, bag);
				if (!flag)
				{
					return new ValueTuple<bool, bool>(false, false);
				}
			}
			if (method.RefKind != RefKind.None)
			{
				return new ValueTuple<bool, bool>(false, flag);
			}
			if (method.Parameters.Length == 0)
			{
				return new ValueTuple<bool, bool>(true, flag);
			}
			if (method.Parameters.Length > 1)
			{
				return new ValueTuple<bool, bool>(false, flag);
			}
			if (!method.ParameterRefKinds.IsDefault)
			{
				return new ValueTuple<bool, bool>(false, flag);
			}
			TypeWithAnnotations typeWithAnnotations = method.Parameters[0].TypeWithAnnotations;
			if (typeWithAnnotations.TypeKind != TypeKind.Array)
			{
				return new ValueTuple<bool, bool>(false, flag);
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol arrayTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol)typeWithAnnotations.Type;
			return new ValueTuple<bool, bool>(arrayTypeSymbol.IsSZArray && arrayTypeSymbol.ElementType.SpecialType == SpecialType.System_String, flag);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0006C315 File Offset: 0x0006A515
		internal override bool IsUnreferencedAssemblyIdentityDiagnosticCode(int code)
		{
			return code == 12;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0006C31C File Offset: 0x0006A51C
		internal bool MightContainNoPiaLocalTypes()
		{
			return this.SourceAssembly.MightContainNoPiaLocalTypes();
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0006C32C File Offset: 0x0006A52C
		public Conversion ClassifyConversion(ITypeSymbol source, ITypeSymbol destination)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = source.EnsureCSharpSymbolOrNull("source");
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol2 = destination.EnsureCSharpSymbolOrNull("destination");
			CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
			return this.Conversions.ClassifyConversionFromType(typeSymbol, typeSymbol2, ref discarded, false);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0006C384 File Offset: 0x0006A584
		public override CommonConversion ClassifyCommonConversion(ITypeSymbol source, ITypeSymbol destination)
		{
			return this.ClassifyConversion(source, destination).ToCommonConversion();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0006C3A4 File Offset: 0x0006A5A4
		internal override IConvertibleConversion ClassifyConvertibleConversion(IOperation source, [global::System.Runtime.CompilerServices.Nullable(2)] ITypeSymbol destination, [global::System.Runtime.CompilerServices.Nullable(2)] out ConstantValue constantValue)
		{
			constantValue = null;
			if (destination == null)
			{
				return Conversion.NoConversion;
			}
			ITypeSymbol type = source.Type;
			ConstantValue constantValue2 = source.GetConstantValue();
			if (type != null)
			{
				Conversion conversion = this.ClassifyConversion(type, destination);
				if (conversion.IsReference && constantValue2 != null && constantValue2.IsNull)
				{
					constantValue = constantValue2;
				}
				return conversion;
			}
			if (constantValue2 != null && constantValue2.IsNull && destination.IsReferenceType)
			{
				constantValue = constantValue2;
				return Conversion.NullLiteral;
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0006C428 File Offset: 0x0006A628
		internal Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol CreateArrayTypeSymbol(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol elementType, int rank = 1, NullableAnnotation elementNullableAnnotation = NullableAnnotation.Oblivious)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (rank < 1)
			{
				throw new ArgumentException("rank");
			}
			return Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol.CreateCSharpArray(this.Assembly, TypeWithAnnotations.Create(elementType, elementNullableAnnotation, default(ImmutableArray<CustomModifier>)), rank);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0006C470 File Offset: 0x0006A670
		internal Microsoft.CodeAnalysis.CSharp.Symbols.PointerTypeSymbol CreatePointerTypeSymbol(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol elementType, NullableAnnotation elementNullableAnnotation = NullableAnnotation.Oblivious)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			return new Microsoft.CodeAnalysis.CSharp.Symbols.PointerTypeSymbol(TypeWithAnnotations.Create(elementType, elementNullableAnnotation, default(ImmutableArray<CustomModifier>)));
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0006C4A0 File Offset: 0x0006A6A0
		private protected override bool IsSymbolAccessibleWithinCore(ISymbol symbol, ISymbol within, [global::System.Runtime.CompilerServices.Nullable(2)] ITypeSymbol throughType)
		{
			Symbol symbol2 = symbol.EnsureCSharpSymbolOrNull("symbol");
			Symbol symbol3 = within.EnsureCSharpSymbolOrNull("within");
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = throughType.EnsureCSharpSymbolOrNull("throughType");
			CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
			if (symbol3.Kind != SymbolKind.Assembly)
			{
				return AccessCheck.IsSymbolAccessible(symbol2, (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)symbol3, ref discarded, typeSymbol);
			}
			return AccessCheck.IsSymbolAccessible(symbol2, (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol)symbol3, ref discarded);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0006C4FE File Offset: 0x0006A6FE
		[Obsolete("Compilation.IsSymbolAccessibleWithin is not designed for use within the compilers", true)]
		internal new bool IsSymbolAccessibleWithin(ISymbol symbol, ISymbol within, [global::System.Runtime.CompilerServices.Nullable(2)] ITypeSymbol throughType = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0006C505 File Offset: 0x0006A705
		internal void AddModuleInitializerMethod(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method)
		{
			LazyInitializer.EnsureInitialized<ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>>(ref this._moduleInitializerMethods).Add(method);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0006C51C File Offset: 0x0006A71C
		public new SemanticModel GetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility)
		{
			if (syntaxTree == null)
			{
				throw new ArgumentNullException("syntaxTree");
			}
			if (!this._syntaxAndDeclarations.GetLazyState().RootNamespaces.ContainsKey(syntaxTree))
			{
				throw new ArgumentException(CSharpResources.SyntaxTreeNotFound, "syntaxTree");
			}
			SemanticModel semanticModel = null;
			if (base.SemanticModelProvider != null)
			{
				semanticModel = base.SemanticModelProvider.GetSemanticModel(syntaxTree, this, ignoreAccessibility);
			}
			return semanticModel ?? this.CreateSemanticModel(syntaxTree, ignoreAccessibility);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0006C585 File Offset: 0x0006A785
		internal override SemanticModel CreateSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility)
		{
			return new SyntaxTreeSemanticModel(this, syntaxTree, ignoreAccessibility);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0006C58F File Offset: 0x0006A78F
		internal BinderFactory GetBinderFactory(SyntaxTree syntaxTree, bool ignoreAccessibility = false)
		{
			if (ignoreAccessibility && SimpleProgramNamedTypeSymbol.GetSimpleProgramEntryPoint(this) != null)
			{
				return this.GetBinderFactory(syntaxTree, true, ref this._ignoreAccessibilityBinderFactories);
			}
			return this.GetBinderFactory(syntaxTree, false, ref this._binderFactories);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0006C5BC File Offset: 0x0006A7BC
		private BinderFactory GetBinderFactory(SyntaxTree syntaxTree, bool ignoreAccessibility, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref WeakReference<BinderFactory>[] cachedBinderFactories)
		{
			int syntaxTreeOrdinal = this.GetSyntaxTreeOrdinal(syntaxTree);
			WeakReference<BinderFactory>[] array = cachedBinderFactories;
			if (array == null)
			{
				array = new WeakReference<BinderFactory>[this.SyntaxTrees.Length];
				array = Interlocked.CompareExchange<WeakReference<BinderFactory>[]>(ref cachedBinderFactories, array, null) ?? array;
			}
			WeakReference<BinderFactory> weakReference = array[syntaxTreeOrdinal];
			BinderFactory binderFactory;
			if (weakReference != null && weakReference.TryGetTarget(ref binderFactory))
			{
				return binderFactory;
			}
			return this.AddNewFactory(syntaxTree, ignoreAccessibility, ref array[syntaxTreeOrdinal]);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0006C61C File Offset: 0x0006A81C
		private BinderFactory AddNewFactory(SyntaxTree syntaxTree, bool ignoreAccessibility, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] [NotNull] ref WeakReference<BinderFactory> slot)
		{
			BinderFactory binderFactory = new BinderFactory(this, syntaxTree, ignoreAccessibility);
			WeakReference<BinderFactory> weakReference = new WeakReference<BinderFactory>(binderFactory);
			BinderFactory binderFactory2;
			for (;;)
			{
				WeakReference<BinderFactory> weakReference2 = slot;
				if (weakReference2 != null && weakReference2.TryGetTarget(ref binderFactory2))
				{
					break;
				}
				if (Interlocked.CompareExchange<WeakReference<BinderFactory>>(ref slot, weakReference, weakReference2) == weakReference2)
				{
					return binderFactory;
				}
			}
			return binderFactory2;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0006C657 File Offset: 0x0006A857
		internal Binder GetBinder(CSharpSyntaxNode syntax)
		{
			return this.GetBinderFactory(syntax.SyntaxTree, false).GetBinder(syntax, null, null);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0006C66E File Offset: 0x0006A86E
		private Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol CreateGlobalNamespaceAlias()
		{
			return Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol.CreateGlobalNamespaceAlias(this.GlobalNamespace);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0006C67C File Offset: 0x0006A87C
		private void CompleteTree(SyntaxTree tree)
		{
			if (this._lazyCompilationUnitCompletedTrees == null)
			{
				Interlocked.CompareExchange<HashSet<SyntaxTree>>(ref this._lazyCompilationUnitCompletedTrees, new HashSet<SyntaxTree>(), null);
			}
			HashSet<SyntaxTree> lazyCompilationUnitCompletedTrees = this._lazyCompilationUnitCompletedTrees;
			lock (lazyCompilationUnitCompletedTrees)
			{
				if (this._lazyCompilationUnitCompletedTrees.Add(tree))
				{
					AsyncQueue<CompilationEvent> eventQueue = base.EventQueue;
					if (eventQueue != null)
					{
						eventQueue.TryEnqueue(new CompilationUnitCompletedEvent(this, tree));
					}
					if (this._lazyCompilationUnitCompletedTrees.Count == this.SyntaxTrees.Length)
					{
						base.CompleteCompilationEventQueue_NoLock();
					}
				}
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0006C718 File Offset: 0x0006A918
		internal override void ReportUnusedImports(DiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			this.ReportUnusedImports(null, new BindingDiagnosticBag(diagnostics), cancellationToken);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0006C728 File Offset: 0x0006A928
		private void ReportUnusedImports([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTree filterTree, BindingDiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			if (this._lazyImportInfos != null && (filterTree == null || Compilation.ReportUnusedImportsInTree(filterTree)))
			{
				PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol> pooledHashSet = null;
				if (diagnostics.DependenciesBag != null)
				{
					pooledHashSet = PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol>.GetInstance();
				}
				foreach (KeyValuePair<CSharpCompilation.ImportInfo, ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> keyValuePair in this._lazyImportInfos)
				{
					cancellationToken.ThrowIfCancellationRequested();
					CSharpCompilation.ImportInfo key = keyValuePair.Key;
					SyntaxTree tree = key.Tree;
					if ((filterTree == null || filterTree == tree) && Compilation.ReportUnusedImportsInTree(tree))
					{
						TextSpan span = key.Span;
						if (!base.IsImportDirectiveUsed(tree, span.Start))
						{
							ErrorCode errorCode = ((key.Kind == SyntaxKind.ExternAliasDirective) ? ErrorCode.HDN_UnusedExternAlias : ErrorCode.HDN_UnusedUsingDirective);
							diagnostics.Add(errorCode, tree.GetLocation(span));
						}
						else if (diagnostics.DependenciesBag != null)
						{
							ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> value = keyValuePair.Value;
							if (!value.IsDefaultOrEmpty)
							{
								diagnostics.AddDependencies(value);
							}
							else if (key.Kind == SyntaxKind.ExternAliasDirective)
							{
								ExternAliasDirectiveSyntax externAliasDirectiveSyntax = key.Tree.GetRoot(cancellationToken).FindToken(key.Span.Start, false).Parent.FirstAncestorOrSelf<ExternAliasDirectiveSyntax>(null, true);
								Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol namespaceSymbol;
								if (externAliasDirectiveSyntax != null && this.GetExternAliasTarget(externAliasDirectiveSyntax.Identifier.ValueText, out namespaceSymbol))
								{
									pooledHashSet.Add(namespaceSymbol);
								}
							}
						}
					}
				}
				if (pooledHashSet != null)
				{
					BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(null, PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.GetInstance());
					foreach (Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol namespaceSymbol2 in pooledHashSet)
					{
						bindingDiagnosticBag.Clear();
						bindingDiagnosticBag.AddAssembliesUsedByNamespaceReference(namespaceSymbol2);
						ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> lazyUsedAssemblyReferences = this._lazyUsedAssemblyReferences;
						if ((lazyUsedAssemblyReferences != null && !lazyUsedAssemblyReferences.IsEmpty) || diagnostics.DependenciesBag.Count != 0)
						{
							foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol in bindingDiagnosticBag.DependenciesBag)
							{
								ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> lazyUsedAssemblyReferences2 = this._lazyUsedAssemblyReferences;
								if ((lazyUsedAssemblyReferences2 != null && lazyUsedAssemblyReferences2.Contains(assemblySymbol)) || diagnostics.DependenciesBag.Contains(assemblySymbol))
								{
									bindingDiagnosticBag.DependenciesBag.Clear();
									break;
								}
							}
						}
						diagnostics.AddDependencies(bindingDiagnosticBag, false);
					}
					bindingDiagnosticBag.Free();
					pooledHashSet.Free();
				}
			}
			this.CompleteTrees(filterTree);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0006C9D8 File Offset: 0x0006ABD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override void CompleteTrees(SyntaxTree filterTree)
		{
			if (base.EventQueue != null)
			{
				if (filterTree != null)
				{
					this.CompleteTree(filterTree);
				}
				else
				{
					foreach (SyntaxTree syntaxTree in this.SyntaxTrees)
					{
						this.CompleteTree(syntaxTree);
					}
				}
			}
			if (filterTree == null)
			{
				this._usageOfUsingsRecordedInTrees = null;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0006CA2C File Offset: 0x0006AC2C
		internal void RecordImport(UsingDirectiveSyntax syntax)
		{
			this.RecordImportInternal(syntax);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0006CA35 File Offset: 0x0006AC35
		internal void RecordImport(ExternAliasDirectiveSyntax syntax)
		{
			this.RecordImportInternal(syntax);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0006CA40 File Offset: 0x0006AC40
		private void RecordImportInternal(CSharpSyntaxNode syntax)
		{
			LazyInitializer.EnsureInitialized<ConcurrentDictionary<CSharpCompilation.ImportInfo, ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>>(ref this._lazyImportInfos).TryAdd(new CSharpCompilation.ImportInfo(syntax.SyntaxTree, syntax.Kind(), syntax.Span), default(ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0006CA80 File Offset: 0x0006AC80
		internal void RecordImportDependencies(UsingDirectiveSyntax syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> dependencies)
		{
			this._lazyImportInfos.TryUpdate(new CSharpCompilation.ImportInfo(syntax.SyntaxTree, syntax.Kind(), syntax.Span), dependencies, default(ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>));
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0006CABA File Offset: 0x0006ACBA
		internal override CommonMessageProvider MessageProvider
		{
			get
			{
				return this._syntaxAndDeclarations.MessageProvider;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0006CAC8 File Offset: 0x0006ACC8
		internal DiagnosticBag DeclarationDiagnostics
		{
			get
			{
				if (this._lazyDeclarationDiagnostics == null)
				{
					DiagnosticBag diagnosticBag = new DiagnosticBag();
					Interlocked.CompareExchange<DiagnosticBag>(ref this._lazyDeclarationDiagnostics, diagnosticBag, null);
				}
				return this._lazyDeclarationDiagnostics;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0006CAF7 File Offset: 0x0006ACF7
		internal DiagnosticBag AdditionalCodegenWarnings
		{
			get
			{
				return this._additionalCodegenWarnings;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0006CAFF File Offset: 0x0006ACFF
		internal DeclarationTable Declarations
		{
			get
			{
				return this._syntaxAndDeclarations.GetLazyState().DeclarationTable;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0006CB11 File Offset: 0x0006AD11
		internal MergedNamespaceDeclaration MergedRootDeclaration
		{
			get
			{
				return this.Declarations.GetMergedRoot(this);
			}
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0006CB1F File Offset: 0x0006AD1F
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetParseDiagnostics(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDiagnostics(CompilationStage.Parse, false, cancellationToken);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0006CB2A File Offset: 0x0006AD2A
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetDeclarationDiagnostics(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDiagnostics(CompilationStage.Declare, false, cancellationToken);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0006CB35 File Offset: 0x0006AD35
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetMethodBodyDiagnostics(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDiagnostics(CompilationStage.Compile, false, cancellationToken);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0006CB40 File Offset: 0x0006AD40
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<Diagnostic> GetDiagnostics(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetDiagnostics(CompilationStage.Compile, true, cancellationToken);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0006CB4C File Offset: 0x0006AD4C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<Diagnostic> GetDiagnostics(CompilationStage stage, bool includeEarlierStages, CancellationToken cancellationToken)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			this.GetDiagnostics(stage, includeEarlierStages, instance, cancellationToken);
			return instance.ToReadOnlyAndFree();
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0006CB70 File Offset: 0x0006AD70
		internal override void GetDiagnostics(CompilationStage stage, bool includeEarlierStages, DiagnosticBag diagnostics, CancellationToken cancellationToken = default(CancellationToken))
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			this.GetDiagnosticsWithoutFiltering(stage, includeEarlierStages, new BindingDiagnosticBag(instance), cancellationToken);
			base.FilterAndAppendAndFreeDiagnostics(diagnostics, ref instance, cancellationToken);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		private void GetDiagnosticsWithoutFiltering(CompilationStage stage, bool includeEarlierStages, BindingDiagnosticBag builder, CancellationToken cancellationToken)
		{
			if (stage == CompilationStage.Parse || (stage > CompilationStage.Parse && includeEarlierStages))
			{
				ImmutableArray<SyntaxTree> syntaxTrees = this.SyntaxTrees;
				if (this.Options.ConcurrentBuild)
				{
					RoslynParallel.For(0, syntaxTrees.Length, UICultureUtilities.WithCurrentUICulture<int>(delegate(int i)
					{
						SyntaxTree syntaxTree4 = syntaxTrees[i];
						CSharpCompilation.AppendLoadDirectiveDiagnostics(builder.DiagnosticBag, this._syntaxAndDeclarations, syntaxTree4, null);
						builder.AddRange(syntaxTree4.GetDiagnostics(cancellationToken));
					}), cancellationToken);
				}
				else
				{
					foreach (SyntaxTree syntaxTree in syntaxTrees)
					{
						cancellationToken.ThrowIfCancellationRequested();
						CSharpCompilation.AppendLoadDirectiveDiagnostics(builder.DiagnosticBag, this._syntaxAndDeclarations, syntaxTree, null);
						cancellationToken.ThrowIfCancellationRequested();
						builder.AddRange(syntaxTree.GetDiagnostics(cancellationToken));
					}
				}
				HashSet<ParseOptions> hashSet = new HashSet<ParseOptions>();
				foreach (SyntaxTree syntaxTree2 in syntaxTrees)
				{
					cancellationToken.ThrowIfCancellationRequested();
					if (!syntaxTree2.Options.Errors.IsDefaultOrEmpty && hashSet.Add(syntaxTree2.Options))
					{
						Location location = syntaxTree2.GetLocation(TextSpan.FromBounds(0, 0));
						foreach (Diagnostic diagnostic in syntaxTree2.Options.Errors)
						{
							builder.Add(diagnostic.WithLocation(location));
						}
					}
				}
			}
			if (stage == CompilationStage.Declare || (stage > CompilationStage.Declare && includeEarlierStages))
			{
				base.CheckAssemblyName(builder.DiagnosticBag);
				builder.AddRange<Diagnostic>(this.Options.Errors);
				if (this.Options.NullableContextOptions != NullableContextOptions.Disable && this.LanguageVersion < MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion() && this._syntaxAndDeclarations.ExternalSyntaxTrees.Any<SyntaxTree>())
				{
					builder.Add(new CSDiagnostic(new CSDiagnosticInfo(ErrorCode.ERR_NullableOptionNotAvailable, new object[]
					{
						"NullableContextOptions",
						this.Options.NullableContextOptions,
						this.LanguageVersion.ToDisplayString(),
						new CSharpRequiredLanguageVersion(MessageID.IDS_FeatureNullableReferenceTypes.RequiredVersion())
					}), Location.None, false));
				}
				cancellationToken.ThrowIfCancellationRequested();
				builder.AddRange<Diagnostic>(this.GetBoundReferenceManager().Diagnostics);
				cancellationToken.ThrowIfCancellationRequested();
				BindingDiagnosticBag<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> builder2 = builder;
				SyntaxTree syntaxTree3 = null;
				CancellationToken cancellationToken2 = cancellationToken;
				builder2.AddRange(this.GetSourceDeclarationDiagnostics(syntaxTree3, default(TextSpan?), null, cancellationToken2), true);
				if (base.EventQueue != null && this.SyntaxTrees.Length == 0)
				{
					base.EnsureCompilationEventQueueCompleted();
				}
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (stage == CompilationStage.Compile || (stage > CompilationStage.Compile && includeEarlierStages))
			{
				BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(DiagnosticBag.GetInstance(), (builder.DependenciesBag != null) ? new ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>() : null);
				this.GetDiagnosticsForAllMethodBodies(bindingDiagnosticBag, false, cancellationToken);
				builder.AddRange(bindingDiagnosticBag, false);
				bindingDiagnosticBag.DiagnosticBag.Free();
			}
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0006CECC File Offset: 0x0006B0CC
		private static void AppendLoadDirectiveDiagnostics(DiagnosticBag builder, SyntaxAndDeclarationManager syntaxAndDeclarations, SyntaxTree syntaxTree, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1, 1 })] Func<IEnumerable<Diagnostic>, IEnumerable<Diagnostic>> locationFilterOpt = null)
		{
			ImmutableArray<LoadDirective> immutableArray;
			if (syntaxAndDeclarations.GetLazyState().LoadDirectiveMap.TryGetValue(syntaxTree, out immutableArray))
			{
				foreach (LoadDirective loadDirective in immutableArray)
				{
					IEnumerable<Diagnostic> enumerable = loadDirective.Diagnostics;
					if (locationFilterOpt != null)
					{
						enumerable = locationFilterOpt.Invoke(enumerable);
					}
					builder.AddRange(enumerable);
				}
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0006CF28 File Offset: 0x0006B128
		private void GetDiagnosticsForAllMethodBodies(BindingDiagnosticBag diagnostics, bool doLowering, CancellationToken cancellationToken)
		{
			MethodCompiler.CompileMethodBodies(this, doLowering ? ((PEModuleBuilder)this.CreateModuleBuilder(EmitOptions.Default, null, null, null, null, null, diagnostics.DiagnosticBag, cancellationToken)) : null, false, false, false, false, diagnostics, null, cancellationToken);
			DocumentationCommentCompiler.WriteDocumentationCommentXml(this, null, null, diagnostics, cancellationToken, null, default(TextSpan?));
			this.ReportUnusedImports(null, diagnostics, cancellationToken);
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0006CF84 File Offset: 0x0006B184
		private static bool IsDefinedOrImplementedInSourceTree(Symbol symbol, SyntaxTree tree, TextSpan? span)
		{
			return symbol.IsDefinedInSourceTree(tree, span, default(CancellationToken)) || (symbol.Kind == SymbolKind.Method && symbol.IsImplicitlyDeclared && ((Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol)symbol).MethodKind == MethodKind.Constructor && CSharpCompilation.IsDefinedOrImplementedInSourceTree(symbol.ContainingType, tree, span));
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0006CFD8 File Offset: 0x0006B1D8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> GetDiagnosticsForMethodBodiesInTree(SyntaxTree tree, TextSpan? span, CancellationToken cancellationToken)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(instance);
			bool flag = (span == null || span.Value == tree.GetRoot(cancellationToken).FullSpan) && Compilation.ReportUnusedImportsInTree(tree);
			bool flag2 = false;
			if (flag && this.UsageOfUsingsRecordedInTrees != null)
			{
				foreach (SingleNamespaceDeclaration singleNamespaceDeclaration in ((SourceNamespaceSymbol)this.SourceModule.GlobalNamespace).MergedDeclaration.Declarations)
				{
					if (singleNamespaceDeclaration.SyntaxReference.SyntaxTree == tree)
					{
						if (singleNamespaceDeclaration.HasGlobalUsings)
						{
							flag2 = true;
							break;
						}
						break;
					}
				}
			}
			if (flag2)
			{
				ImmutableHashSet<SyntaxTree> usageOfUsingsRecordedInTrees = this.UsageOfUsingsRecordedInTrees;
				if (usageOfUsingsRecordedInTrees != null && usageOfUsingsRecordedInTrees.IsEmpty)
				{
					this.<GetDiagnosticsForMethodBodiesInTree>g__compileMethodBodiesAndDocComments|217_0(null, default(TextSpan?), bindingDiagnosticBag, cancellationToken);
					this._usageOfUsingsRecordedInTrees = null;
					goto IL_0166;
				}
			}
			this.<GetDiagnosticsForMethodBodiesInTree>g__compileMethodBodiesAndDocComments|217_0(tree, span, bindingDiagnosticBag, cancellationToken);
			if (flag)
			{
				this.<GetDiagnosticsForMethodBodiesInTree>g__registeredUsageOfUsingsInTree|217_1(tree);
			}
			if (flag2)
			{
				BindingDiagnosticBag bindingDiagnosticBag2 = new BindingDiagnosticBag(DiagnosticBag.GetInstance());
				foreach (SyntaxTree syntaxTree in this.SyntaxTrees)
				{
					ImmutableHashSet<SyntaxTree> usageOfUsingsRecordedInTrees2 = this.UsageOfUsingsRecordedInTrees;
					if (usageOfUsingsRecordedInTrees2 == null)
					{
						break;
					}
					if (!usageOfUsingsRecordedInTrees2.Contains(syntaxTree))
					{
						this.<GetDiagnosticsForMethodBodiesInTree>g__compileMethodBodiesAndDocComments|217_0(syntaxTree, default(TextSpan?), bindingDiagnosticBag2, cancellationToken);
						this.<GetDiagnosticsForMethodBodiesInTree>g__registeredUsageOfUsingsInTree|217_1(syntaxTree);
						bindingDiagnosticBag2.DiagnosticBag.Clear();
					}
				}
				bindingDiagnosticBag2.DiagnosticBag.Free();
			}
			IL_0166:
			if (flag)
			{
				this.ReportUnusedImports(tree, bindingDiagnosticBag, cancellationToken);
			}
			return instance.ToReadOnlyAndFree();
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0006D160 File Offset: 0x0006B360
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> GetSourceDeclarationDiagnostics(SyntaxTree syntaxTree = null, TextSpan? filterSpanWithinTree = null, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Func<IEnumerable<Diagnostic>, SyntaxTree, TextSpan?, IEnumerable<Diagnostic>> locationFilterOpt = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.UsingsFromOptions.Complete(this, cancellationToken);
			SourceLocation sourceLocation = null;
			if (syntaxTree != null)
			{
				SyntaxNode root = syntaxTree.GetRoot(cancellationToken);
				sourceLocation = ((filterSpanWithinTree != null) ? new SourceLocation(syntaxTree, filterSpanWithinTree.Value) : new SourceLocation(root));
			}
			this.Assembly.ForceComplete(sourceLocation, cancellationToken);
			if (syntaxTree == null)
			{
				this._declarationDiagnosticsFrozen = true;
				this._needsGeneratedAttributes_IsFrozen = true;
			}
			DiagnosticBag lazyDeclarationDiagnostics = this._lazyDeclarationDiagnostics;
			IEnumerable<Diagnostic> enumerable = ((lazyDeclarationDiagnostics != null) ? lazyDeclarationDiagnostics.AsEnumerable() : null) ?? Enumerable.Empty<Diagnostic>();
			if (locationFilterOpt != null)
			{
				enumerable = locationFilterOpt.Invoke(enumerable, syntaxTree, filterSpanWithinTree);
			}
			ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> clsComplianceDiagnostics = this.GetClsComplianceDiagnostics(syntaxTree, filterSpanWithinTree, cancellationToken);
			return new ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(enumerable.AsImmutable<Diagnostic>().Concat<Diagnostic>(clsComplianceDiagnostics.Diagnostics), clsComplianceDiagnostics.Dependencies);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0006D21C File Offset: 0x0006B41C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> GetClsComplianceDiagnostics(SyntaxTree syntaxTree, TextSpan? filterSpanWithinTree, CancellationToken cancellationToken)
		{
			if (syntaxTree != null)
			{
				BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance(true, false);
				ClsComplianceChecker.CheckCompliance(this, instance, cancellationToken, syntaxTree, filterSpanWithinTree);
				return instance.ToReadOnlyAndFree();
			}
			if (this._lazyClsComplianceDiagnostics.IsDefault || this._lazyClsComplianceDependencies.IsDefault)
			{
				BindingDiagnosticBag instance2 = BindingDiagnosticBag.GetInstance();
				ClsComplianceChecker.CheckCompliance(this, instance2, cancellationToken, null, default(TextSpan?));
				ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> immutableBindingDiagnostic = instance2.ToReadOnlyAndFree();
				ImmutableInterlocked.InterlockedInitialize<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(ref this._lazyClsComplianceDependencies, immutableBindingDiagnostic.Dependencies);
				ImmutableInterlocked.InterlockedInitialize<Diagnostic>(ref this._lazyClsComplianceDiagnostics, immutableBindingDiagnostic.Diagnostics);
			}
			return new ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(this._lazyClsComplianceDiagnostics, this._lazyClsComplianceDependencies);
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0006D2B5 File Offset: 0x0006B4B5
		private static IEnumerable<Diagnostic> FilterDiagnosticsByLocation(IEnumerable<Diagnostic> diagnostics, SyntaxTree tree, TextSpan? filterSpanWithinTree)
		{
			foreach (Diagnostic diagnostic in diagnostics)
			{
				if (diagnostic.HasIntersectingLocation(tree, filterSpanWithinTree))
				{
					yield return diagnostic;
				}
			}
			IEnumerator<Diagnostic> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0006D2D4 File Offset: 0x0006B4D4
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<Diagnostic> GetDiagnosticsForSyntaxTree(CompilationStage stage, SyntaxTree syntaxTree, TextSpan? filterSpanWithinTree, bool includeEarlierStages, CancellationToken cancellationToken = default(CancellationToken))
		{
			cancellationToken.ThrowIfCancellationRequested();
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			if (stage == CompilationStage.Parse || (stage > CompilationStage.Parse && includeEarlierStages))
			{
				CSharpCompilation.AppendLoadDirectiveDiagnostics(instance, this._syntaxAndDeclarations, syntaxTree, (IEnumerable<Diagnostic> diagnostics) => CSharpCompilation.FilterDiagnosticsByLocation(diagnostics, syntaxTree, filterSpanWithinTree));
				IEnumerable<Diagnostic> enumerable = syntaxTree.GetDiagnostics(cancellationToken);
				enumerable = CSharpCompilation.FilterDiagnosticsByLocation(enumerable, syntaxTree, filterSpanWithinTree);
				instance.AddRange(enumerable);
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (stage == CompilationStage.Declare || (stage > CompilationStage.Declare && includeEarlierStages))
			{
				ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> sourceDeclarationDiagnostics = this.GetSourceDeclarationDiagnostics(syntaxTree, filterSpanWithinTree, new Func<IEnumerable<Diagnostic>, SyntaxTree, TextSpan?, IEnumerable<Diagnostic>>(CSharpCompilation.FilterDiagnosticsByLocation), cancellationToken);
				instance.AddRange<Diagnostic>(sourceDeclarationDiagnostics.Diagnostics);
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (stage == CompilationStage.Compile || (stage > CompilationStage.Compile && includeEarlierStages))
			{
				IEnumerable<Diagnostic> enumerable2 = this.GetDiagnosticsForMethodBodiesInTree(syntaxTree, filterSpanWithinTree, cancellationToken);
				enumerable2 = CSharpCompilation.FilterDiagnosticsByLocation(enumerable2, syntaxTree, filterSpanWithinTree);
				instance.AddRange(enumerable2);
			}
			DiagnosticBag instance2 = DiagnosticBag.GetInstance();
			base.FilterAndAppendAndFreeDiagnostics(instance2, ref instance, cancellationToken);
			return instance2.ToReadOnlyAndFree<Diagnostic>();
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0006D3FC File Offset: 0x0006B5FC
		protected override void AppendDefaultVersionResource(Stream resourceStream)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssembly = this.SourceAssembly;
			string text = sourceAssembly.FileVersion ?? sourceAssembly.Identity.Version.ToString();
			bool flag = !this.Options.OutputKind.IsApplication();
			string text2 = text;
			string name = this.SourceModule.Name;
			string name2 = this.SourceModule.Name;
			string text3 = sourceAssembly.InformationalVersion ?? text;
			string text4 = sourceAssembly.Title ?? " ";
			Win32ResourceConversions.AppendVersionToResourceStream(resourceStream, flag, text2, name, name2, text3, sourceAssembly.Identity.Version, text4, sourceAssembly.Copyright ?? " ", sourceAssembly.Trademark, sourceAssembly.Product, sourceAssembly.Description, sourceAssembly.Company);
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0006D4A9 File Offset: 0x0006B6A9
		internal override byte LinkerMajorVersion
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0006D4AD File Offset: 0x0006B6AD
		internal override bool IsDelaySigned
		{
			get
			{
				return this.SourceAssembly.IsDelaySigned;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0006D4BA File Offset: 0x0006B6BA
		internal override StrongNameKeys StrongNameKeys
		{
			get
			{
				return this.SourceAssembly.StrongNameKeys;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0006D4C8 File Offset: 0x0006B6C8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override CommonPEModuleBuilder CreateModuleBuilder([global::System.Runtime.CompilerServices.Nullable(1)] EmitOptions emitOptions, IMethodSymbol debugEntryPoint, Stream sourceLinkStream, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, CompilationTestData testData, [global::System.Runtime.CompilerServices.Nullable(1)] DiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			string runtimeMetadataVersion = this.GetRuntimeMetadataVersion(emitOptions, diagnostics);
			if (runtimeMetadataVersion == null)
			{
				return null;
			}
			ModulePropertiesForSerialization modulePropertiesForSerialization = base.ConstructModuleSerializationProperties(emitOptions, runtimeMetadataVersion, default(Guid));
			if (manifestResources == null)
			{
				manifestResources = SpecializedCollections.EmptyEnumerable<ResourceDescription>();
			}
			PEModuleBuilder pemoduleBuilder;
			if (this._options.OutputKind.IsNetModule())
			{
				pemoduleBuilder = new PENetModuleBuilder((SourceModuleSymbol)this.SourceModule, emitOptions, modulePropertiesForSerialization, manifestResources);
			}
			else
			{
				OutputKind outputKind = (this._options.OutputKind.IsValid() ? this._options.OutputKind : OutputKind.DynamicallyLinkedLibrary);
				pemoduleBuilder = new PEAssemblyBuilder(this.SourceAssembly, emitOptions, outputKind, modulePropertiesForSerialization, manifestResources);
			}
			if (debugEntryPoint != null)
			{
				pemoduleBuilder.SetDebugEntryPoint(debugEntryPoint.GetSymbol(), diagnostics);
			}
			pemoduleBuilder.SourceLinkStreamOpt = sourceLinkStream;
			if (embeddedTexts != null)
			{
				pemoduleBuilder.EmbeddedTexts = embeddedTexts;
			}
			if (testData != null)
			{
				pemoduleBuilder.SetMethodTestData(testData.Methods);
				testData.Module = pemoduleBuilder;
			}
			return pemoduleBuilder;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0006D59C File Offset: 0x0006B79C
		internal override bool CompileMethods(CommonPEModuleBuilder moduleBuilder, bool emittingPdb, bool emitMetadataOnly, bool emitTestCoverageData, DiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Predicate<ISymbolInternal> filterOpt, CancellationToken cancellationToken)
		{
			PooledHashSet<int> pooledHashSet = null;
			if (emitMetadataOnly)
			{
				pooledHashSet = PooledHashSet<int>.GetInstance();
				pooledHashSet.Add(501);
			}
			bool flag = !base.FilterAndAppendDiagnostics(diagnostics, this.GetDiagnostics(CompilationStage.Declare, true, cancellationToken), pooledHashSet, cancellationToken);
			if (pooledHashSet != null)
			{
				pooledHashSet.Free();
			}
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)moduleBuilder;
			if (emitMetadataOnly)
			{
				if (flag)
				{
					return false;
				}
				if (pemoduleBuilder.SourceModule.HasBadAttributes)
				{
					diagnostics.Add(ErrorCode.ERR_ModuleEmitFailure, NoLocation.Singleton, new object[]
					{
						((INamedEntity)pemoduleBuilder).Name,
						new LocalizableResourceString("ModuleHasInvalidAttributes", CodeAnalysisResources.ResourceManager, typeof(CodeAnalysisResources))
					});
					return false;
				}
				SynthesizedMetadataCompiler.ProcessSynthesizedMembers(this, pemoduleBuilder, cancellationToken);
			}
			else
			{
				if ((emittingPdb || emitTestCoverageData) && !base.CreateDebugDocuments(pemoduleBuilder.DebugDocumentsBuilder, pemoduleBuilder.EmbeddedTexts, diagnostics))
				{
					return false;
				}
				DiagnosticBag instance = DiagnosticBag.GetInstance();
				MethodCompiler.CompileMethodBodies(this, pemoduleBuilder, emittingPdb, emitTestCoverageData, flag, true, new BindingDiagnosticBag(instance), filterOpt, cancellationToken);
				if (!flag && !CommonCompiler.HasUnsuppressableErrors(instance))
				{
					this.GenerateModuleInitializer(pemoduleBuilder, instance);
				}
				bool flag2 = !base.FilterAndAppendAndFreeDiagnostics(diagnostics, ref instance, cancellationToken);
				if (flag || flag2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0006D6B4 File Offset: 0x0006B8B4
		private void GenerateModuleInitializer(PEModuleBuilder moduleBeingBuilt, DiagnosticBag methodBodyDiagnosticBag)
		{
			if (this._moduleInitializerMethods != null)
			{
				ILBuilder ilbuilder = new ILBuilder(moduleBeingBuilt, new LocalSlotManager(null), OptimizationLevel.Release, false);
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol in this._moduleInitializerMethods.OrderBy<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol>(LexicalOrderSymbolComparer.Instance))
				{
					ilbuilder.EmitOpCode(ILOpCode.Call, 0);
					ilbuilder.EmitToken(moduleBeingBuilt.Translate(methodSymbol, methodBodyDiagnosticBag, true), CSharpSyntaxTree.Dummy.GetRoot(default(CancellationToken)), methodBodyDiagnosticBag);
				}
				ilbuilder.EmitRet(true);
				ilbuilder.Realize();
				moduleBeingBuilt.RootModuleType.SetStaticConstructorBody(ilbuilder.RealizedIL);
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0006D768 File Offset: 0x0006B968
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override bool GenerateResourcesAndDocumentationComments([global::System.Runtime.CompilerServices.Nullable(1)] CommonPEModuleBuilder moduleBuilder, Stream xmlDocStream, Stream win32Resources, bool useRawWin32Resources, string outputNameOverride, [global::System.Runtime.CompilerServices.Nullable(1)] DiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			base.SetupWin32Resources(moduleBuilder, win32Resources, useRawWin32Resources, instance);
			base.ReportManifestResourceDuplicates(moduleBuilder.ManifestResources, Enumerable.Select<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol, string>(Enumerable.Skip<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol>(this.SourceAssembly.Modules, 1), (Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol m) => m.Name), this.AddedModulesResourceNames(instance), instance);
			if (!base.FilterAndAppendAndFreeDiagnostics(diagnostics, ref instance, cancellationToken))
			{
				return false;
			}
			cancellationToken.ThrowIfCancellationRequested();
			DiagnosticBag instance2 = DiagnosticBag.GetInstance();
			string text = FileNameUtilities.ChangeExtension(outputNameOverride, null);
			DocumentationCommentCompiler.WriteDocumentationCommentXml(this, text, xmlDocStream, new BindingDiagnosticBag(instance2), cancellationToken, null, default(TextSpan?));
			return base.FilterAndAppendAndFreeDiagnostics(diagnostics, ref instance2, cancellationToken);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0006D820 File Offset: 0x0006BA20
		private IEnumerable<string> AddedModulesResourceNames(DiagnosticBag diagnostics)
		{
			ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules = this.SourceAssembly.Modules;
			int i = 1;
			while (i < modules.Length)
			{
				PEModuleSymbol pemoduleSymbol = (PEModuleSymbol)modules[i];
				ImmutableArray<EmbeddedResource> embeddedResourcesOrThrow;
				try
				{
					embeddedResourcesOrThrow = pemoduleSymbol.Module.GetEmbeddedResourcesOrThrow();
				}
				catch (BadImageFormatException)
				{
					diagnostics.Add(new CSDiagnosticInfo(ErrorCode.ERR_BindToBogus, new object[] { pemoduleSymbol }), NoLocation.Singleton);
					goto IL_00DC;
				}
				goto IL_008A;
				IL_00DC:
				int num = i;
				i = num + 1;
				continue;
				IL_008A:
				foreach (EmbeddedResource embeddedResource in embeddedResourcesOrThrow)
				{
					yield return embeddedResource.Name;
				}
				ImmutableArray<EmbeddedResource>.Enumerator enumerator = default(ImmutableArray<EmbeddedResource>.Enumerator);
				goto IL_00DC;
			}
			yield break;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0006D838 File Offset: 0x0006BA38
		internal override EmitDifferenceResult EmitDifference(EmitBaseline baseline, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol, Stream metadataStream, Stream ilStream, Stream pdbStream, ICollection<MethodDefinitionHandle> updatedMethods, [global::System.Runtime.CompilerServices.Nullable(2)] CompilationTestData testData, CancellationToken cancellationToken)
		{
			return EmitHelpers.EmitDifference(this, baseline, edits, isAddedSymbol, metadataStream, ilStream, pdbStream, updatedMethods, testData, cancellationToken);
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0006D85C File Offset: 0x0006BA5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal string GetRuntimeMetadataVersion(EmitOptions emitOptions, DiagnosticBag diagnostics)
		{
			string runtimeMetadataVersion = this.GetRuntimeMetadataVersion(emitOptions);
			if (runtimeMetadataVersion != null)
			{
				return runtimeMetadataVersion;
			}
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			instance.Add(ErrorCode.WRN_NoRuntimeMetadataVersion, NoLocation.Singleton);
			if (!base.FilterAndAppendAndFreeDiagnostics(diagnostics, ref instance, CancellationToken.None))
			{
				return null;
			}
			return string.Empty;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0006D8A4 File Offset: 0x0006BAA4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private string GetRuntimeMetadataVersion(EmitOptions emitOptions)
		{
			PEAssemblySymbol peassemblySymbol = this.Assembly.CorLibrary as PEAssemblySymbol;
			if (peassemblySymbol != null)
			{
				return peassemblySymbol.Assembly.ManifestModule.MetadataVersion;
			}
			return emitOptions.RuntimeMetadataVersion;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0006D8DC File Offset: 0x0006BADC
		internal override void AddDebugSourceDocumentsForChecksumDirectives(DebugDocumentsBuilder documentsBuilder, SyntaxTree tree, DiagnosticBag diagnostics)
		{
			foreach (DirectiveTriviaSyntax directiveTriviaSyntax in tree.GetRoot(default(CancellationToken)).GetDirectives((DirectiveTriviaSyntax d) => d.Kind() == SyntaxKind.PragmaChecksumDirectiveTrivia && !d.ContainsDiagnostics))
			{
				PragmaChecksumDirectiveTriviaSyntax pragmaChecksumDirectiveTriviaSyntax = (PragmaChecksumDirectiveTriviaSyntax)directiveTriviaSyntax;
				string valueText = pragmaChecksumDirectiveTriviaSyntax.File.ValueText;
				string valueText2 = pragmaChecksumDirectiveTriviaSyntax.Bytes.ValueText;
				string text = documentsBuilder.NormalizeDebugDocumentPath(valueText, tree.FilePath);
				DebugSourceDocument debugSourceDocument = documentsBuilder.TryGetDebugDocumentForNormalizedPath(text);
				if (debugSourceDocument != null)
				{
					if (!debugSourceDocument.IsComputedChecksum)
					{
						DebugSourceInfo sourceInfo = debugSourceDocument.GetSourceInfo();
						if (!CSharpCompilation.ChecksumMatches(valueText2, sourceInfo.Checksum) || !(Guid.Parse(pragmaChecksumDirectiveTriviaSyntax.Guid.ValueText) == sourceInfo.ChecksumAlgorithmId))
						{
							diagnostics.Add(ErrorCode.WRN_ConflictingChecksum, new SourceLocation(pragmaChecksumDirectiveTriviaSyntax), new object[] { valueText });
						}
					}
				}
				else
				{
					DebugSourceDocument debugSourceDocument2 = new DebugSourceDocument(text, DebugSourceDocument.CorSymLanguageTypeCSharp, CSharpCompilation.MakeChecksumBytes(valueText2), Guid.Parse(pragmaChecksumDirectiveTriviaSyntax.Guid.ValueText));
					documentsBuilder.AddDebugDocument(debugSourceDocument2);
				}
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0006DA30 File Offset: 0x0006BC30
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static bool ChecksumMatches([global::System.Runtime.CompilerServices.Nullable(1)] string bytesText, ImmutableArray<byte> bytes)
		{
			if (bytesText.Length != bytes.Length * 2)
			{
				return false;
			}
			int i = 0;
			int num = bytesText.Length / 2;
			while (i < num)
			{
				if (SyntaxFacts.HexValue(bytesText.get_Chars(i * 2)) * 16 + SyntaxFacts.HexValue(bytesText.get_Chars(i * 2 + 1)) != (int)bytes[i])
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0006DA94 File Offset: 0x0006BC94
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private static ImmutableArray<byte> MakeChecksumBytes([global::System.Runtime.CompilerServices.Nullable(1)] string bytesText)
		{
			int num = bytesText.Length / 2;
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance(num);
			for (int i = 0; i < num; i++)
			{
				int num2 = SyntaxFacts.HexValue(bytesText.get_Chars(i * 2)) * 16 + SyntaxFacts.HexValue(bytesText.get_Chars(i * 2 + 1));
				instance.Add((byte)num2);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0006DAEE File Offset: 0x0006BCEE
		internal override Guid DebugSourceDocumentLanguageId
		{
			get
			{
				return DebugSourceDocument.CorSymLanguageTypeCSharp;
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006DAF8 File Offset: 0x0006BCF8
		internal override bool HasCodeToEmit()
		{
			ImmutableArray<SyntaxTree>.Enumerator enumerator = this.SyntaxTrees.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetCompilationUnitRoot(default(CancellationToken)).Members.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0006DB47 File Offset: 0x0006BD47
		protected override Compilation CommonWithReferences(IEnumerable<MetadataReference> newReferences)
		{
			return this.WithReferences(newReferences);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0006DB50 File Offset: 0x0006BD50
		protected override Compilation CommonWithAssemblyName([global::System.Runtime.CompilerServices.Nullable(2)] string assemblyName)
		{
			return this.WithAssemblyName(assemblyName);
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0006DB59 File Offset: 0x0006BD59
		protected override IAssemblySymbol CommonAssembly
		{
			get
			{
				return this.Assembly.GetPublicSymbol();
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0006DB66 File Offset: 0x0006BD66
		protected override INamespaceSymbol CommonGlobalNamespace
		{
			get
			{
				return this.GlobalNamespace.GetPublicSymbol();
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0006DB73 File Offset: 0x0006BD73
		protected override CompilationOptions CommonOptions
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0006DB7B File Offset: 0x0006BD7B
		protected override SemanticModel CommonGetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility)
		{
			return this.GetSemanticModel(syntaxTree, ignoreAccessibility);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0006DB85 File Offset: 0x0006BD85
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		protected override ImmutableArray<SyntaxTree> CommonSyntaxTrees
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this.SyntaxTrees;
			}
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0006DB8D File Offset: 0x0006BD8D
		protected override Compilation CommonAddSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			return this.AddSyntaxTrees(trees);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0006DB96 File Offset: 0x0006BD96
		protected override Compilation CommonRemoveSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			return this.RemoveSyntaxTrees(trees);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0006DB9F File Offset: 0x0006BD9F
		protected override Compilation CommonRemoveAllSyntaxTrees()
		{
			return this.RemoveAllSyntaxTrees();
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0006DBA7 File Offset: 0x0006BDA7
		protected override Compilation CommonReplaceSyntaxTree(SyntaxTree oldTree, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTree newTree)
		{
			return this.ReplaceSyntaxTree(oldTree, newTree);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0006DBB1 File Offset: 0x0006BDB1
		protected override Compilation CommonWithOptions(CompilationOptions options)
		{
			return this.WithOptions((CSharpCompilationOptions)options);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0006DBBF File Offset: 0x0006BDBF
		protected override Compilation CommonWithScriptCompilationInfo([global::System.Runtime.CompilerServices.Nullable(2)] ScriptCompilationInfo info)
		{
			return this.WithScriptCompilationInfo((CSharpScriptCompilationInfo)info);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0006DBCD File Offset: 0x0006BDCD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override bool CommonContainsSyntaxTree(SyntaxTree syntaxTree)
		{
			return this.ContainsSyntaxTree(syntaxTree);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0006DBD6 File Offset: 0x0006BDD6
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override ISymbol CommonGetAssemblyOrModuleSymbol(MetadataReference reference)
		{
			return this.GetAssemblyOrModuleSymbol(reference).GetPublicSymbol();
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0006DBE4 File Offset: 0x0006BDE4
		protected override Compilation CommonClone()
		{
			return this.Clone();
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0006DBEC File Offset: 0x0006BDEC
		protected override IModuleSymbol CommonSourceModule
		{
			get
			{
				return this.SourceModule.GetPublicSymbol();
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0006DBF9 File Offset: 0x0006BDF9
		private protected override INamedTypeSymbolInternal CommonGetSpecialType(SpecialType specialType)
		{
			return this.GetSpecialType(specialType);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0006DC02 File Offset: 0x0006BE02
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override INamespaceSymbol CommonGetCompilationNamespace(INamespaceSymbol namespaceSymbol)
		{
			return this.GetCompilationNamespace(namespaceSymbol).GetPublicSymbol();
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0006DC10 File Offset: 0x0006BE10
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		protected override INamedTypeSymbol CommonGetTypeByMetadataName(string metadataName)
		{
			return this.GetTypeByMetadataName(metadataName).GetPublicSymbol();
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0006DC1E File Offset: 0x0006BE1E
		[global::System.Runtime.CompilerServices.Nullable(2)]
		protected override INamedTypeSymbol CommonScriptClass
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.ScriptClass.GetPublicSymbol();
			}
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0006DC2B File Offset: 0x0006BE2B
		protected override IArrayTypeSymbol CommonCreateArrayTypeSymbol(ITypeSymbol elementType, int rank, NullableAnnotation elementNullableAnnotation)
		{
			return this.CreateArrayTypeSymbol(elementType.EnsureCSharpSymbolOrNull("elementType"), rank, elementNullableAnnotation.ToInternalAnnotation()).GetPublicSymbol();
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0006DC4A File Offset: 0x0006BE4A
		protected override IPointerTypeSymbol CommonCreatePointerTypeSymbol(ITypeSymbol elementType)
		{
			return this.CreatePointerTypeSymbol(elementType.EnsureCSharpSymbolOrNull("elementType"), elementType.NullableAnnotation.ToInternalAnnotation()).GetPublicSymbol();
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0006DC70 File Offset: 0x0006BE70
		protected override IFunctionPointerTypeSymbol CommonCreateFunctionPointerTypeSymbol(ITypeSymbol returnType, RefKind returnRefKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> parameterTypes, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> parameterRefKinds, SignatureCallingConvention callingConvention, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<INamedTypeSymbol> callingConventionTypes)
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			if (parameterTypes.IsDefault)
			{
				throw new ArgumentNullException("parameterTypes");
			}
			for (int j = 0; j < parameterTypes.Length; j++)
			{
				if (parameterTypes[j] == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "parameterTypes", j));
				}
			}
			if (parameterRefKinds.IsDefault)
			{
				throw new ArgumentNullException("parameterRefKinds");
			}
			if (parameterRefKinds.Length != parameterTypes.Length)
			{
				throw new ArgumentException(string.Format(CSharpResources.NotSameNumberParameterTypesAndRefKinds, parameterTypes.Length, parameterRefKinds.Length));
			}
			if (returnRefKind == RefKind.Out)
			{
				throw new ArgumentException(CSharpResources.OutIsNotValidForReturn);
			}
			if (callingConvention != SignatureCallingConvention.Unmanaged && !callingConventionTypes.IsDefaultOrEmpty)
			{
				throw new ArgumentException(string.Format(CSharpResources.CallingConventionTypesRequireUnmanaged, "callingConventionTypes", "callingConvention"));
			}
			if (!callingConvention.IsValid())
			{
				throw new ArgumentOutOfRangeException("callingConvention");
			}
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(returnType.EnsureCSharpSymbolOrNull("returnType"), returnType.NullableAnnotation.ToInternalAnnotation(), default(ImmutableArray<CustomModifier>));
			ImmutableArray<TypeWithAnnotations> immutableArray = parameterTypes.SelectAsArray<ITypeSymbol, TypeWithAnnotations>((ITypeSymbol type) => TypeWithAnnotations.Create(type.EnsureCSharpSymbolOrNull("parameterTypes"), type.NullableAnnotation.ToInternalAnnotation(), default(ImmutableArray<CustomModifier>)));
			CallingConvention callingConvention2 = callingConvention.FromSignatureConvention();
			ImmutableArray<CustomModifier> immutableArray2;
			if (callingConvention2 != CallingConvention.Unmanaged || callingConventionTypes.IsDefaultOrEmpty)
			{
				immutableArray2 = ImmutableArray<CustomModifier>.Empty;
			}
			else
			{
				immutableArray2 = callingConventionTypes.SelectAsArray((INamedTypeSymbol type, int i, CSharpCompilation @this) => CSharpCompilation.<CommonCreateFunctionPointerTypeSymbol>g__getCustomModifierForType|272_2(type, @this, i), this);
			}
			ImmutableArray<CustomModifier> immutableArray3 = immutableArray2;
			return Microsoft.CodeAnalysis.CSharp.Symbols.FunctionPointerTypeSymbol.CreateFromParts(callingConvention2, immutableArray3, typeWithAnnotations, returnRefKind, immutableArray, parameterRefKinds, this).GetPublicSymbol();
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0006DE0F File Offset: 0x0006C00F
		protected override INamedTypeSymbol CommonCreateNativeIntegerTypeSymbol(bool signed)
		{
			return this.CreateNativeIntegerTypeSymbol(signed).GetPublicSymbol();
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0006DE1D File Offset: 0x0006C01D
		internal new Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol CreateNativeIntegerTypeSymbol(bool signed)
		{
			return this.GetSpecialType(signed ? SpecialType.System_IntPtr : SpecialType.System_UIntPtr).AsNativeInteger();
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0006DE34 File Offset: 0x0006C034
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		protected override INamedTypeSymbol CommonCreateTupleTypeSymbol([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> elementTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations, ImmutableArray<NullableAnnotation> elementNullableAnnotations)
		{
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance(elementTypes.Length);
			for (int i = 0; i < elementTypes.Length; i++)
			{
				ITypeSymbol typeSymbol = elementTypes[i];
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol2 = typeSymbol.EnsureCSharpSymbolOrNull(string.Format("{0}[{1}]", "elementTypes", i));
				NullableAnnotation nullableAnnotation = (elementNullableAnnotations.IsDefault ? typeSymbol.NullableAnnotation : elementNullableAnnotations[i]).ToInternalAnnotation();
				instance.Add(TypeWithAnnotations.Create(typeSymbol2, nullableAnnotation, default(ImmutableArray<CustomModifier>)));
			}
			return Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol.CreateTuple(null, instance.ToImmutableAndFree(), elementLocations, elementNames, this, false, false, default(ImmutableArray<bool>), null, null).GetPublicSymbol();
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0006DEE0 File Offset: 0x0006C0E0
		protected override INamedTypeSymbol CommonCreateTupleTypeSymbol(INamedTypeSymbol underlyingType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<NullableAnnotation> elementNullableAnnotations)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = underlyingType.EnsureCSharpSymbolOrNull("underlyingType");
			int num;
			if (!namedTypeSymbol.IsTupleTypeOfCardinality(out num))
			{
				throw new ArgumentException(CodeAnalysisResources.TupleUnderlyingTypeMustBeTupleCompatible, "underlyingType");
			}
			elementNames = Compilation.CheckTupleElementNames(num, elementNames);
			Compilation.CheckTupleElementLocations(num, elementLocations);
			Compilation.CheckTupleElementNullableAnnotations(num, elementNullableAnnotations);
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol2 = Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol.CreateTuple(namedTypeSymbol, elementNames, default(ImmutableArray<bool>), elementLocations, default(ImmutableArray<Location>));
			if (!elementNullableAnnotations.IsDefault)
			{
				namedTypeSymbol2 = namedTypeSymbol2.WithElementTypes(namedTypeSymbol2.TupleElementTypesWithAnnotations.ZipAsArray<TypeWithAnnotations, NullableAnnotation, TypeWithAnnotations>(elementNullableAnnotations, (TypeWithAnnotations t, NullableAnnotation a) => TypeWithAnnotations.Create(t.Type, a.ToInternalAnnotation(), default(ImmutableArray<CustomModifier>))));
			}
			return namedTypeSymbol2.GetPublicSymbol();
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0006DF8C File Offset: 0x0006C18C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		protected override INamedTypeSymbol CommonCreateAnonymousTypeSymbol([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> memberTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> memberNames, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> memberLocations, ImmutableArray<bool> memberIsReadOnly, ImmutableArray<NullableAnnotation> memberNullableAnnotations)
		{
			int i = 0;
			int length = memberTypes.Length;
			while (i < length)
			{
				memberTypes[i].EnsureCSharpSymbolOrNull(string.Format("{0}[{1}]", "memberTypes", i));
				i++;
			}
			if (!memberIsReadOnly.IsDefault)
			{
				if (memberIsReadOnly.Any<bool>((bool v) => !v))
				{
					throw new ArgumentException("Non-ReadOnly members are not supported in C# anonymous types.");
				}
			}
			ArrayBuilder<AnonymousTypeField> instance = ArrayBuilder<AnonymousTypeField>.GetInstance();
			int j = 0;
			int length2 = memberTypes.Length;
			while (j < length2)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol symbol = memberTypes[j].GetSymbol();
				string text = memberNames[j];
				Location location = (memberLocations.IsDefault ? Location.None : memberLocations[j]);
				NullableAnnotation nullableAnnotation = (memberNullableAnnotations.IsDefault ? NullableAnnotation.Oblivious : memberNullableAnnotations[j].ToInternalAnnotation());
				instance.Add(new AnonymousTypeField(text, location, TypeWithAnnotations.Create(symbol, nullableAnnotation, default(ImmutableArray<CustomModifier>))));
				j++;
			}
			AnonymousTypeDescriptor anonymousTypeDescriptor = new AnonymousTypeDescriptor(instance.ToImmutableAndFree(), Location.None);
			return this.AnonymousTypeManager.ConstructAnonymousTypeSymbol(anonymousTypeDescriptor).GetPublicSymbol();
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0006E0C3 File Offset: 0x0006C2C3
		protected override ITypeSymbol CommonDynamicType
		{
			get
			{
				return this.DynamicType.GetPublicSymbol();
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0006E0D0 File Offset: 0x0006C2D0
		protected override INamedTypeSymbol CommonObjectType
		{
			get
			{
				return this.ObjectType.GetPublicSymbol();
			}
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0006E0DD File Offset: 0x0006C2DD
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		protected override IMethodSymbol CommonGetEntryPoint(CancellationToken cancellationToken)
		{
			return this.GetEntryPoint(cancellationToken).GetPublicSymbol();
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0006E0EC File Offset: 0x0006C2EC
		internal override int CompareSourceLocations(Location loc1, Location loc2)
		{
			int num = base.CompareSyntaxTreeOrdering(loc1.SourceTree, loc2.SourceTree);
			if (num != 0)
			{
				return num;
			}
			return loc1.SourceSpan.Start - loc2.SourceSpan.Start;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0006E130 File Offset: 0x0006C330
		internal override int CompareSourceLocations(SyntaxReference loc1, SyntaxReference loc2)
		{
			int num = base.CompareSyntaxTreeOrdering(loc1.SyntaxTree, loc2.SyntaxTree);
			if (num != 0)
			{
				return num;
			}
			return loc1.Span.Start - loc2.Span.Start;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0006E172 File Offset: 0x0006C372
		public override bool ContainsSymbolsWithName(Func<string, bool> predicate, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (filter == SymbolFilter.None)
			{
				throw new ArgumentException(CSharpResources.NoNoneSearchCriteria, "filter");
			}
			return DeclarationTable.ContainsName(this.MergedRootDeclaration, predicate, filter, cancellationToken);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0006E1A3 File Offset: 0x0006C3A3
		public override IEnumerable<ISymbol> GetSymbolsWithName(Func<string, bool> predicate, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			if (filter == SymbolFilter.None)
			{
				throw new ArgumentException(CSharpResources.NoNoneSearchCriteria, "filter");
			}
			return new CSharpCompilation.PredicateSymbolSearcher(this, filter, predicate, cancellationToken).GetSymbolsWithName().GetPublicSymbols();
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0006E1D9 File Offset: 0x0006C3D9
		public override bool ContainsSymbolsWithName(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (filter == SymbolFilter.None)
			{
				throw new ArgumentException(CSharpResources.NoNoneSearchCriteria, "filter");
			}
			return DeclarationTable.ContainsName(this.MergedRootDeclaration, name, filter, cancellationToken);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0006E20A File Offset: 0x0006C40A
		public override IEnumerable<ISymbol> GetSymbolsWithName(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.GetSymbolsWithNameCore(name, filter, cancellationToken).GetPublicSymbols();
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0006E21A File Offset: 0x0006C41A
		internal IEnumerable<Symbol> GetSymbolsWithNameCore(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (filter == SymbolFilter.None)
			{
				throw new ArgumentException(CSharpResources.NoNoneSearchCriteria, "filter");
			}
			return new CSharpCompilation.NameSymbolSearcher(this, filter, name, cancellationToken).GetSymbolsWithName();
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0006E24B File Offset: 0x0006C44B
		internal bool HasDynamicEmitAttributes(BindingDiagnosticBag diagnostics, Location location)
		{
			return Binder.GetWellKnownTypeMember(this, WellKnownMember.System_Runtime_CompilerServices_DynamicAttribute__ctor, diagnostics, location, null, false) != null && Binder.GetWellKnownTypeMember(this, WellKnownMember.System_Runtime_CompilerServices_DynamicAttribute__ctorTransformFlags, diagnostics, location, null, false) != null;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0006E26C File Offset: 0x0006C46C
		internal bool HasTupleNamesAttributes(BindingDiagnosticBag diagnostics, Location location)
		{
			return Binder.GetWellKnownTypeMember(this, WellKnownMember.System_Runtime_CompilerServices_TupleElementNamesAttribute__ctorTransformNames, diagnostics, location, null, false) != null;
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0006E280 File Offset: 0x0006C480
		internal bool CanEmitBoolean()
		{
			return this.CanEmitSpecialType(SpecialType.System_Boolean);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0006E28C File Offset: 0x0006C48C
		internal bool CanEmitSpecialType(SpecialType type)
		{
			DiagnosticInfo diagnosticInfo = this.GetSpecialType(type).GetUseSiteInfo().DiagnosticInfo;
			return diagnosticInfo == null || diagnosticInfo.Severity != DiagnosticSeverity.Error;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		internal bool EmitNullablePublicOnly
		{
			get
			{
				if (!this._lazyEmitNullablePublicOnly.HasValue())
				{
					SyntaxTree syntaxTree = this.SyntaxTrees.FirstOrDefault<SyntaxTree>();
					bool flag;
					if (syntaxTree == null)
					{
						flag = false;
					}
					else
					{
						ParseOptions options = syntaxTree.Options;
						bool? flag2;
						if (options == null)
						{
							flag2 = default(bool?);
						}
						else
						{
							IReadOnlyDictionary<string, string> features = options.Features;
							flag2 = ((features != null) ? new bool?(features.ContainsKey("nullablePublicOnly")) : default(bool?));
						}
						bool? flag3 = flag2;
						bool flag4 = true;
						flag = (flag3.GetValueOrDefault() == flag4) & (flag3 != null);
					}
					bool flag5 = flag;
					this._lazyEmitNullablePublicOnly = flag5.ToThreeState();
				}
				return this._lazyEmitNullablePublicOnly.Value();
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0006E34C File Offset: 0x0006C54C
		internal bool ShouldEmitNullableAttributes(Symbol symbol)
		{
			if (symbol.ContainingModule != this.SourceModule)
			{
				return false;
			}
			if (!this.EmitNullablePublicOnly)
			{
				return true;
			}
			symbol = CSharpCompilation.<ShouldEmitNullableAttributes>g__getExplicitAccessibilitySymbol|296_0(symbol);
			bool flag;
			return AccessCheck.IsEffectivelyPublicOrInternal(symbol, out flag) && (!flag || this.SourceAssembly.InternalsAreVisible);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0006E39C File Offset: 0x0006C59C
		internal override AnalyzerDriver CreateAnalyzerDriver([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, SeverityFilter severityFilter)
		{
			Func<SyntaxNode, SyntaxKind> func = (SyntaxNode node) => node.Kind();
			Func<SyntaxTrivia, bool> func2 = (SyntaxTrivia trivia) => trivia.Kind() == SyntaxKind.SingleLineCommentTrivia || trivia.Kind() == SyntaxKind.MultiLineCommentTrivia;
			return new AnalyzerDriver<SyntaxKind>(analyzers, func, analyzerManager, severityFilter, func2);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0006E3F3 File Offset: 0x0006C5F3
		internal void SymbolDeclaredEvent(Symbol symbol)
		{
			AsyncQueue<CompilationEvent> eventQueue = base.EventQueue;
			if (eventQueue == null)
			{
				return;
			}
			eventQueue.TryEnqueue(new SymbolDeclaredCompilationEvent(this, symbol.GetPublicSymbol(), null));
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0006E414 File Offset: 0x0006C614
		internal override void SerializePdbEmbeddedCompilationOptions(BlobBuilder builder)
		{
			CSharpCompilation.<>c__DisplayClass299_0 CS$<>8__locals1;
			CS$<>8__locals1.builder = builder;
			CSharpCompilation.<SerializePdbEmbeddedCompilationOptions>g__WriteValue|299_0("language-version", this.LanguageVersion.ToDisplayString(), ref CS$<>8__locals1);
			if (this.Options.CheckOverflow)
			{
				CSharpCompilation.<SerializePdbEmbeddedCompilationOptions>g__WriteValue|299_0("checked", this.Options.CheckOverflow.ToString(), ref CS$<>8__locals1);
			}
			if (this.Options.NullableContextOptions != NullableContextOptions.Disable)
			{
				CSharpCompilation.<SerializePdbEmbeddedCompilationOptions>g__WriteValue|299_0("nullable", this.Options.NullableContextOptions.ToString(), ref CS$<>8__locals1);
			}
			if (this.Options.AllowUnsafe)
			{
				CSharpCompilation.<SerializePdbEmbeddedCompilationOptions>g__WriteValue|299_0("unsafe", this.Options.AllowUnsafe.ToString(), ref CS$<>8__locals1);
			}
			ImmutableArray<string> preprocessorSymbols = this.GetPreprocessorSymbols();
			if (preprocessorSymbols.Any<string>())
			{
				CSharpCompilation.<SerializePdbEmbeddedCompilationOptions>g__WriteValue|299_0("define", string.Join(",", preprocessorSymbols), ref CS$<>8__locals1);
			}
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0006E4F8 File Offset: 0x0006C6F8
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<string> GetPreprocessorSymbols()
		{
			CSharpSyntaxTree csharpSyntaxTree = (CSharpSyntaxTree)this.SyntaxTrees.FirstOrDefault<SyntaxTree>();
			if (csharpSyntaxTree == null)
			{
				return ImmutableArray<string>.Empty;
			}
			return csharpSyntaxTree.Options.PreprocessorSymbolNames.ToImmutableArray<string>();
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0006E530 File Offset: 0x0006C730
		internal bool EnableEnumArrayBlockInitialization
		{
			get
			{
				Symbol wellKnownTypeMember = this.GetWellKnownTypeMember(WellKnownMember.System_Runtime_GCLatencyMode__SustainedLowLatency);
				return wellKnownTypeMember != null && wellKnownTypeMember.ContainingAssembly == this.Assembly.CorLibrary;
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0006E56C File Offset: 0x0006C76C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<MetadataReference> GetUsedAssemblyReferences(CancellationToken cancellationToken = default(CancellationToken))
		{
			ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> completeSetOfUsedAssemblies = this.GetCompleteSetOfUsedAssemblies(cancellationToken);
			if (completeSetOfUsedAssemblies == null)
			{
				return ImmutableArray<MetadataReference>.Empty;
			}
			HashSet<MetadataReference> hashSet = new HashSet<MetadataReference>(ReferenceEqualityComparer.Instance);
			ImmutableDictionary<MetadataReference, ImmutableArray<MetadataReference>> mergedAssemblyReferencesMap = this.GetBoundReferenceManager().MergedAssemblyReferencesMap;
			foreach (MetadataReference metadataReference in base.References)
			{
				if (metadataReference.Properties.Kind == MetadataImageKind.Assembly)
				{
					Symbol referencedAssemblySymbol = this.GetBoundReferenceManager().GetReferencedAssemblySymbol(metadataReference);
					ImmutableArray<MetadataReference> immutableArray;
					if (referencedAssemblySymbol != null && completeSetOfUsedAssemblies.Contains((Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol)referencedAssemblySymbol) && hashSet.Add(metadataReference) && mergedAssemblyReferencesMap.TryGetValue(metadataReference, out immutableArray))
					{
						hashSet.AddAll<MetadataReference>(immutableArray);
					}
				}
			}
			ArrayBuilder<MetadataReference> instance = ArrayBuilder<MetadataReference>.GetInstance(hashSet.Count);
			foreach (MetadataReference metadataReference2 in base.References)
			{
				if (hashSet.Contains(metadataReference2))
				{
					instance.Add(metadataReference2);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0006E694 File Offset: 0x0006C894
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> GetCompleteSetOfUsedAssemblies(CancellationToken cancellationToken)
		{
			if (!this._usedAssemblyReferencesFrozen && !Volatile.Read(ref this._usedAssemblyReferencesFrozen))
			{
				BindingDiagnosticBag bindingDiagnosticBag = new BindingDiagnosticBag(DiagnosticBag.GetInstance(), new ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>());
				this.GetDiagnosticsWithoutFiltering(CompilationStage.Declare, true, bindingDiagnosticBag, cancellationToken);
				bool flag = bindingDiagnosticBag.HasAnyErrors();
				if (!flag)
				{
					bindingDiagnosticBag.DiagnosticBag.Clear();
					this.GetDiagnosticsForAllMethodBodies(bindingDiagnosticBag, true, cancellationToken);
					flag = bindingDiagnosticBag.HasAnyErrors();
					if (!flag)
					{
						this.AddUsedAssemblies(bindingDiagnosticBag.DependenciesBag);
					}
				}
				this.<GetCompleteSetOfUsedAssemblies>g__completeTheSetOfUsedAssemblies|310_2(flag, cancellationToken);
				bindingDiagnosticBag.DiagnosticBag.Free();
			}
			return this._lazyUsedAssemblyReferences;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0006E720 File Offset: 0x0006C920
		internal void AddUsedAssemblies([global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] ICollection<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> assemblies)
		{
			if (!assemblies.IsNullOrEmpty<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>())
			{
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol in assemblies)
				{
					this.AddUsedAssembly(assemblySymbol);
				}
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0006E774 File Offset: 0x0006C974
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal bool AddUsedAssembly(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assembly)
		{
			if (assembly == null || assembly == this.SourceAssembly || assembly.IsMissing)
			{
				return false;
			}
			if (this._lazyUsedAssemblyReferences == null)
			{
				Interlocked.CompareExchange<ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>(ref this._lazyUsedAssemblyReferences, new ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(), null);
			}
			return this._lazyUsedAssemblyReferences.Add(assembly);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0006E7C2 File Offset: 0x0006C9C2
		internal EmbeddableAttributes GetNeedsGeneratedAttributes()
		{
			this._needsGeneratedAttributes_IsFrozen = true;
			return (EmbeddableAttributes)this._needsGeneratedAttributes;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0006E7D1 File Offset: 0x0006C9D1
		private void SetNeedsGeneratedAttributes(EmbeddableAttributes attributes)
		{
			ThreadSafeFlagOperations.Set(ref this._needsGeneratedAttributes, (int)attributes);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0006E7E0 File Offset: 0x0006C9E0
		internal bool GetUsesNullableAttributes()
		{
			this._needsGeneratedAttributes_IsFrozen = true;
			return this._usesNullableAttributes;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0006E7EF File Offset: 0x0006C9EF
		private void SetUsesNullableAttributes()
		{
			this._usesNullableAttributes = true;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal Symbol GetWellKnownTypeMember(WellKnownMember member)
		{
			if (base.IsMemberMissing(member))
			{
				return null;
			}
			if (this._lazyWellKnownTypeMembers == null || this._lazyWellKnownTypeMembers[(int)member] == Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol.UnknownResultType)
			{
				if (this._lazyWellKnownTypeMembers == null)
				{
					Symbol[] array = new Symbol[418];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol.UnknownResultType;
					}
					Interlocked.CompareExchange<Symbol[]>(ref this._lazyWellKnownTypeMembers, array, null);
				}
				MemberDescriptor descriptor = WellKnownMembers.GetDescriptor(member);
				Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = ((descriptor.DeclaringTypeId <= 45) ? this.GetSpecialType((SpecialType)descriptor.DeclaringTypeId) : this.GetWellKnownType((WellKnownType)descriptor.DeclaringTypeId));
				Symbol symbol = null;
				if (!namedTypeSymbol.IsErrorType())
				{
					symbol = CSharpCompilation.GetRuntimeMember(namedTypeSymbol, in descriptor, this.WellKnownMemberSignatureComparer, this.Assembly);
				}
				Interlocked.CompareExchange<Symbol>(ref this._lazyWellKnownTypeMembers[(int)member], symbol, Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol.UnknownResultType);
			}
			return this._lazyWellKnownTypeMembers[(int)member];
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0006E8D4 File Offset: 0x0006CAD4
		internal Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol GetWellKnownType(WellKnownType type)
		{
			bool flag = this.Options.TopLevelBinderFlags.Includes(BinderFlags.IgnoreCorLibraryDuplicatedTypes);
			int num = type - WellKnownType.First;
			if (this._lazyWellKnownTypes == null || this._lazyWellKnownTypes[num] == null)
			{
				if (this._lazyWellKnownTypes == null)
				{
					Interlocked.CompareExchange<Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol[]>(ref this._lazyWellKnownTypes, new Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol[257], null);
				}
				string metadataName = type.GetMetadataName();
				DiagnosticBag instance = DiagnosticBag.GetInstance();
				ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> valueTuple = default(ValueTuple<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>);
				Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol;
				if (base.IsTypeMissing(type))
				{
					namedTypeSymbol = null;
				}
				else
				{
					DiagnosticBag diagnosticBag = ((type <= WellKnownType.System_IFormatProvider) ? instance : null);
					namedTypeSymbol = this.Assembly.GetTypeByMetadataName(metadataName, true, true, out valueTuple, true, diagnosticBag, flag);
				}
				if (namedTypeSymbol == null)
				{
					MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(metadataName, true, -1);
					if (type.IsValueTupleType())
					{
						CSDiagnosticInfo csdiagnosticInfo;
						if (valueTuple.Item1 == null)
						{
							csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_PredefinedValueTupleTypeNotFound, new object[] { metadataTypeName.FullName });
						}
						else
						{
							csdiagnosticInfo = new CSDiagnosticInfo(ErrorCode.ERR_PredefinedValueTupleTypeAmbiguous3, new object[] { metadataTypeName.FullName, valueTuple.Item1, valueTuple.Item2 });
						}
						namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(this.Assembly.Modules[0], ref metadataTypeName, type, csdiagnosticInfo);
					}
					else
					{
						namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(this.Assembly.Modules[0], ref metadataTypeName, type, null);
					}
				}
				if (Interlocked.CompareExchange<Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol>(ref this._lazyWellKnownTypes[num], namedTypeSymbol, null) == null)
				{
					this.AdditionalCodegenWarnings.AddRange(instance);
				}
				instance.Free();
			}
			return this._lazyWellKnownTypes[num];
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0006EA5C File Offset: 0x0006CC5C
		internal bool IsAttributeType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> discarded = CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Discarded;
			return this.IsEqualOrDerivedFromWellKnownClass(type, WellKnownType.System_Attribute, ref discarded);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0006EA7A File Offset: 0x0006CC7A
		internal override bool IsAttributeType(ITypeSymbol type)
		{
			return this.IsAttributeType(type.EnsureCSharpSymbolOrNull("type"));
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0006EA8D File Offset: 0x0006CC8D
		internal bool IsExceptionType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> useSiteInfo)
		{
			return this.IsEqualOrDerivedFromWellKnownClass(type, WellKnownType.System_Exception, ref useSiteInfo);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0006EA99 File Offset: 0x0006CC99
		internal bool IsReadOnlySpanType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			return Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals(type.OriginalDefinition, this.GetWellKnownType(WellKnownType.System_ReadOnlySpan_T), TypeCompareKind.ConsiderEverything);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0006EAB4 File Offset: 0x0006CCB4
		internal bool IsEqualOrDerivedFromWellKnownClass(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, WellKnownType wellKnownType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ref CompoundUseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> useSiteInfo)
		{
			if (type.Kind != SymbolKind.NamedType || type.TypeKind != TypeKind.Class)
			{
				return false;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol wellKnownType2 = this.GetWellKnownType(wellKnownType);
			return type.Equals(wellKnownType2, TypeCompareKind.ConsiderEverything) || type.IsDerivedFrom(wellKnownType2, TypeCompareKind.ConsiderEverything, ref useSiteInfo);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0006EAF3 File Offset: 0x0006CCF3
		internal override bool IsSystemTypeReference(ITypeSymbolInternal type)
		{
			return Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol.Equals((Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol)type, this.GetWellKnownType(WellKnownType.System_Type), TypeCompareKind.ConsiderEverything);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0006EB09 File Offset: 0x0006CD09
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override ISymbolInternal CommonGetWellKnownTypeMember(WellKnownMember member)
		{
			return this.GetWellKnownTypeMember(member);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0006EB12 File Offset: 0x0006CD12
		internal override ITypeSymbolInternal CommonGetWellKnownType(WellKnownType wellknownType)
		{
			return this.GetWellKnownType(wellknownType);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0006EB1B File Offset: 0x0006CD1B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal static Symbol GetRuntimeMember(Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol declaringType, in MemberDescriptor descriptor, SignatureComparer<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol> comparer, [global::System.Runtime.CompilerServices.Nullable(2)] Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol accessWithinOpt)
		{
			return CSharpCompilation.GetRuntimeMember(declaringType.GetMembers(descriptor.Name), in descriptor, comparer, accessWithinOpt);
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0006EB34 File Offset: 0x0006CD34
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static Symbol GetRuntimeMember([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Symbol> members, in MemberDescriptor descriptor, [global::System.Runtime.CompilerServices.Nullable(1)] SignatureComparer<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol> comparer, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol accessWithinOpt)
		{
			MethodKind methodKind = MethodKind.Ordinary;
			bool flag = (descriptor.Flags & MemberFlags.Static) > (MemberFlags)0;
			Symbol symbol = null;
			MemberFlags memberFlags = descriptor.Flags & MemberFlags.KindMask;
			switch (memberFlags)
			{
			case MemberFlags.Method:
			{
				SymbolKind symbolKind = SymbolKind.Method;
				goto IL_0071;
			}
			case MemberFlags.Field:
			{
				SymbolKind symbolKind = SymbolKind.Field;
				goto IL_0071;
			}
			case MemberFlags.Method | MemberFlags.Field:
				break;
			case MemberFlags.Constructor:
			{
				SymbolKind symbolKind = SymbolKind.Method;
				methodKind = MethodKind.Constructor;
				goto IL_0071;
			}
			default:
				if (memberFlags == MemberFlags.PropertyGet)
				{
					SymbolKind symbolKind = SymbolKind.Method;
					methodKind = MethodKind.PropertyGet;
					goto IL_0071;
				}
				if (memberFlags == MemberFlags.Property)
				{
					SymbolKind symbolKind = SymbolKind.Property;
					goto IL_0071;
				}
				break;
			}
			throw ExceptionUtilities.UnexpectedValue(descriptor.Flags);
			IL_0071:
			foreach (Symbol symbol2 in members)
			{
				SymbolKind symbolKind;
				if (symbol2.Name.Equals(descriptor.Name) && symbol2.Kind == symbolKind && symbol2.IsStatic == flag && (symbol2.DeclaredAccessibility == Accessibility.Public || (accessWithinOpt != null && Symbol.IsSymbolAccessible(symbol2, accessWithinOpt))))
				{
					if (symbolKind != SymbolKind.Field)
					{
						if (symbolKind != SymbolKind.Method)
						{
							if (symbolKind != SymbolKind.Property)
							{
								throw ExceptionUtilities.UnexpectedValue(symbolKind);
							}
							Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol propertySymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol)symbol2;
							if ((descriptor.Flags & MemberFlags.Virtual) > (MemberFlags)0 != (propertySymbol.IsVirtual || propertySymbol.IsOverride || propertySymbol.IsAbstract))
							{
								continue;
							}
							if (!comparer.MatchPropertySignature(propertySymbol, descriptor.Signature))
							{
								continue;
							}
						}
						else
						{
							Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol)symbol2;
							MethodKind methodKind2 = methodSymbol.MethodKind;
							if (methodKind2 == MethodKind.Conversion || methodKind2 == MethodKind.UserDefinedOperator)
							{
								methodKind2 = MethodKind.Ordinary;
							}
							if (methodSymbol.Arity != (int)descriptor.Arity || methodKind2 != methodKind || (descriptor.Flags & MemberFlags.Virtual) > (MemberFlags)0 != (methodSymbol.IsVirtual || methodSymbol.IsOverride || methodSymbol.IsAbstract))
							{
								continue;
							}
							if (!comparer.MatchMethodSignature(methodSymbol, descriptor.Signature))
							{
								continue;
							}
						}
					}
					else if (!comparer.MatchFieldSignature((Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)symbol2, descriptor.Signature))
					{
						continue;
					}
					if (symbol != null)
					{
						symbol = null;
						break;
					}
					symbol = symbol2;
				}
			}
			return symbol;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0006ED28 File Offset: 0x0006CF28
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal SynthesizedAttributeData TrySynthesizeAttribute(WellKnownMember constructor, ImmutableArray<TypedConstant> arguments = default(ImmutableArray<TypedConstant>), ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>> namedArguments = default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), bool isOptionalUse = false)
		{
			UseSiteInfo<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> useSiteInfo;
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol)Binder.GetWellKnownTypeMember(this, constructor, out useSiteInfo, true);
			if (methodSymbol == null)
			{
				return null;
			}
			if (arguments.IsDefault)
			{
				arguments = ImmutableArray<TypedConstant>.Empty;
			}
			ImmutableArray<KeyValuePair<string, TypedConstant>> immutableArray;
			if (namedArguments.IsDefault)
			{
				immutableArray = ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty;
			}
			else
			{
				ArrayBuilder<KeyValuePair<string, TypedConstant>> arrayBuilder = new ArrayBuilder<KeyValuePair<string, TypedConstant>>(namedArguments.Length);
				foreach (KeyValuePair<WellKnownMember, TypedConstant> keyValuePair in namedArguments)
				{
					Symbol wellKnownTypeMember = Binder.GetWellKnownTypeMember(this, keyValuePair.Key, out useSiteInfo, true);
					if (wellKnownTypeMember == null || wellKnownTypeMember is Microsoft.CodeAnalysis.CSharp.Symbols.ErrorTypeSymbol)
					{
						return null;
					}
					arrayBuilder.Add(new KeyValuePair<string, TypedConstant>(wellKnownTypeMember.Name, keyValuePair.Value));
				}
				immutableArray = arrayBuilder.ToImmutableAndFree();
			}
			return new SynthesizedAttributeData(methodSymbol, arguments, immutableArray);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0006EDE4 File Offset: 0x0006CFE4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedAttributeData TrySynthesizeAttribute(SpecialMember constructor, bool isOptionalUse = false)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol)this.GetSpecialTypeMember(constructor);
			if (methodSymbol == null)
			{
				return null;
			}
			return new SynthesizedAttributeData(methodSymbol, ImmutableArray<TypedConstant>.Empty, ImmutableArray<KeyValuePair<string, TypedConstant>>.Empty);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0006EE14 File Offset: 0x0006D014
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedAttributeData SynthesizeDecimalConstantAttribute(decimal value)
		{
			bool flag;
			byte b;
			uint num;
			uint num2;
			uint num3;
			value.GetBits(out flag, out b, out num, out num2, out num3);
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol specialType = this.GetSpecialType(SpecialType.System_Byte);
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol specialType2 = this.GetSpecialType(SpecialType.System_UInt32);
			return this.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_DecimalConstantAttribute__ctor, ImmutableArray.Create<TypedConstant>(new TypedConstant[]
			{
				new TypedConstant(specialType, TypedConstantKind.Primitive, b),
				new TypedConstant(specialType, TypedConstantKind.Primitive, flag ? 128 : 0),
				new TypedConstant(specialType2, TypedConstantKind.Primitive, num3),
				new TypedConstant(specialType2, TypedConstantKind.Primitive, num2),
				new TypedConstant(specialType2, TypedConstantKind.Primitive, num)
			}), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0006EED8 File Offset: 0x0006D0D8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedAttributeData SynthesizeDebuggerBrowsableNeverAttribute()
		{
			if (this.Options.OptimizationLevel != OptimizationLevel.Debug)
			{
				return null;
			}
			return this.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerBrowsableAttribute__ctor, ImmutableArray.Create<TypedConstant>(new TypedConstant(this.GetWellKnownType(WellKnownType.System_Diagnostics_DebuggerBrowsableState), TypedConstantKind.Enum, 0)), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0006EF24 File Offset: 0x0006D124
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedAttributeData SynthesizeDebuggerStepThroughAttribute()
		{
			if (this.Options.OptimizationLevel != OptimizationLevel.Debug)
			{
				return null;
			}
			return this.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggerStepThroughAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0006EF5B File Offset: 0x0006D15B
		private void EnsureEmbeddableAttributeExists(EmbeddableAttributes attribute, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			if (this.CheckIfAttributeShouldBeEmbedded(attribute, diagnostics, location) && modifyCompilation)
			{
				this.SetNeedsGeneratedAttributes(attribute);
			}
			if ((attribute & (EmbeddableAttributes.NullableAttribute | EmbeddableAttributes.NullableContextAttribute)) > (EmbeddableAttributes)0 && modifyCompilation)
			{
				this.SetUsesNullableAttributes();
			}
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0006EF84 File Offset: 0x0006D184
		internal void EnsureIsReadOnlyAttributeExists([global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.IsReadOnlyAttribute, diagnostics, location, modifyCompilation);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0006EF90 File Offset: 0x0006D190
		internal void EnsureIsByRefLikeAttributeExists([global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.IsByRefLikeAttribute, diagnostics, location, modifyCompilation);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0006EF9C File Offset: 0x0006D19C
		internal void EnsureIsUnmanagedAttributeExists([global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.IsUnmanagedAttribute, diagnostics, location, modifyCompilation);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0006EFA8 File Offset: 0x0006D1A8
		internal void EnsureNullableAttributeExists([global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.NullableAttribute, diagnostics, location, modifyCompilation);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0006EFB4 File Offset: 0x0006D1B4
		internal void EnsureNullableContextAttributeExists([global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.NullableContextAttribute, diagnostics, location, modifyCompilation);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0006EFC1 File Offset: 0x0006D1C1
		internal void EnsureNativeIntegerAttributeExists([global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnostics, Location location, bool modifyCompilation)
		{
			this.EnsureEmbeddableAttributeExists(EmbeddableAttributes.NativeIntegerAttribute, diagnostics, location, modifyCompilation);
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0006EFD0 File Offset: 0x0006D1D0
		internal bool CheckIfAttributeShouldBeEmbedded(EmbeddableAttributes attribute, [global::System.Runtime.CompilerServices.Nullable(2)] BindingDiagnosticBag diagnosticsOpt, Location locationOpt)
		{
			if (attribute <= EmbeddableAttributes.NullableAttribute)
			{
				switch (attribute)
				{
				case EmbeddableAttributes.IsReadOnlyAttribute:
					return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_IsReadOnlyAttribute, WellKnownMember.System_Runtime_CompilerServices_IsReadOnlyAttribute__ctor, default(WellKnownMember?));
				case EmbeddableAttributes.IsByRefLikeAttribute:
					return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_IsByRefLikeAttribute, WellKnownMember.System_Runtime_CompilerServices_IsByRefLikeAttribute__ctor, default(WellKnownMember?));
				case EmbeddableAttributes.IsReadOnlyAttribute | EmbeddableAttributes.IsByRefLikeAttribute:
					break;
				case EmbeddableAttributes.IsUnmanagedAttribute:
					return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_IsUnmanagedAttribute, WellKnownMember.System_Runtime_CompilerServices_IsUnmanagedAttribute__ctor, default(WellKnownMember?));
				default:
					if (attribute == EmbeddableAttributes.NullableAttribute)
					{
						return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_NullableAttribute, WellKnownMember.System_Runtime_CompilerServices_NullableAttribute__ctorByte, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_NullableAttribute__ctorTransformFlags));
					}
					break;
				}
			}
			else
			{
				if (attribute == EmbeddableAttributes.NullableContextAttribute)
				{
					return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_NullableContextAttribute, WellKnownMember.System_Runtime_CompilerServices_NullableContextAttribute__ctor, default(WellKnownMember?));
				}
				if (attribute == EmbeddableAttributes.NullablePublicOnlyAttribute)
				{
					return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_NullablePublicOnlyAttribute, WellKnownMember.System_Runtime_CompilerServices_NullablePublicOnlyAttribute__ctor, default(WellKnownMember?));
				}
				if (attribute == EmbeddableAttributes.NativeIntegerAttribute)
				{
					return this.CheckIfAttributeShouldBeEmbedded(diagnosticsOpt, locationOpt, WellKnownType.System_Runtime_CompilerServices_NativeIntegerAttribute, WellKnownMember.System_Runtime_CompilerServices_NativeIntegerAttribute__ctor, new WellKnownMember?(WellKnownMember.System_Runtime_CompilerServices_NativeIntegerAttribute__ctorTransformFlags));
				}
			}
			throw ExceptionUtilities.UnexpectedValue(attribute);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0006F0F0 File Offset: 0x0006D2F0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private bool CheckIfAttributeShouldBeEmbedded(BindingDiagnosticBag diagnosticsOpt, Location locationOpt, WellKnownType attributeType, WellKnownMember attributeCtor, WellKnownMember? secondAttributeCtor = null)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol wellKnownType = this.GetWellKnownType(attributeType);
			if (wellKnownType is MissingMetadataTypeSymbol)
			{
				if (this.Options.OutputKind != OutputKind.NetModule)
				{
					return true;
				}
				if (diagnosticsOpt != null)
				{
					Binder.ReportUseSite(wellKnownType, diagnosticsOpt, locationOpt);
				}
			}
			else if (diagnosticsOpt != null && Binder.GetWellKnownTypeMember(this, attributeCtor, diagnosticsOpt, locationOpt, null, false) != null && secondAttributeCtor != null)
			{
				Binder.GetWellKnownTypeMember(this, secondAttributeCtor.Value, diagnosticsOpt, locationOpt, null, false);
			}
			return false;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0006F160 File Offset: 0x0006D360
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedAttributeData SynthesizeDebuggableAttribute()
		{
			if (this.GetWellKnownType(WellKnownType.System_Diagnostics_DebuggableAttribute) is MissingMetadataTypeSymbol)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol wellKnownType = this.GetWellKnownType(WellKnownType.System_Diagnostics_DebuggableAttribute__DebuggingModes);
			if (wellKnownType is MissingMetadataTypeSymbol)
			{
				return null;
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)this.GetWellKnownTypeMember(WellKnownMember.System_Diagnostics_DebuggableAttribute_DebuggingModes__IgnoreSymbolStoreSequencePoints);
			if (fieldSymbol == null || !fieldSymbol.HasConstantValue)
			{
				return null;
			}
			int num = fieldSymbol.GetConstantValue(ConstantFieldsInProgress.Empty, false).Int32Value;
			if (this._options.OptimizationLevel == OptimizationLevel.Debug)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol2 = (Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)this.GetWellKnownTypeMember(WellKnownMember.System_Diagnostics_DebuggableAttribute_DebuggingModes__Default);
				if (fieldSymbol2 == null || !fieldSymbol2.HasConstantValue)
				{
					return null;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol3 = (Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)this.GetWellKnownTypeMember(WellKnownMember.System_Diagnostics_DebuggableAttribute_DebuggingModes__DisableOptimizations);
				if (fieldSymbol3 == null || !fieldSymbol3.HasConstantValue)
				{
					return null;
				}
				num |= fieldSymbol2.GetConstantValue(ConstantFieldsInProgress.Empty, false).Int32Value;
				num |= fieldSymbol3.GetConstantValue(ConstantFieldsInProgress.Empty, false).Int32Value;
			}
			if (this._options.EnableEditAndContinue)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol fieldSymbol4 = (Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol)this.GetWellKnownTypeMember(WellKnownMember.System_Diagnostics_DebuggableAttribute_DebuggingModes__EnableEditAndContinue);
				if (fieldSymbol4 == null || !fieldSymbol4.HasConstantValue)
				{
					return null;
				}
				num |= fieldSymbol4.GetConstantValue(ConstantFieldsInProgress.Empty, false).Int32Value;
			}
			TypedConstant typedConstant = new TypedConstant(wellKnownType, TypedConstantKind.Enum, num);
			return this.TrySynthesizeAttribute(WellKnownMember.System_Diagnostics_DebuggableAttribute__ctorDebuggingModes, ImmutableArray.Create<TypedConstant>(typedConstant), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0006F2A0 File Offset: 0x0006D4A0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal SynthesizedAttributeData SynthesizeDynamicAttribute(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int customModifiersCount, RefKind refKindOpt = RefKind.None)
		{
			if (type.IsDynamic() && refKindOpt == RefKind.None && customModifiersCount == 0)
			{
				return this.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_DynamicAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol specialType = this.GetSpecialType(SpecialType.System_Boolean);
			ImmutableArray<TypedConstant> immutableArray = CSharpCompilation.DynamicTransformsEncoder.Encode(type, refKindOpt, customModifiersCount, specialType);
			ImmutableArray<TypedConstant> immutableArray2 = ImmutableArray.Create<TypedConstant>(new TypedConstant(Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol.CreateSZArray(specialType.ContainingAssembly, TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), immutableArray));
			return this.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_DynamicAttribute__ctorTransformFlags, immutableArray2, default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0006F328 File Offset: 0x0006D528
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		internal SynthesizedAttributeData SynthesizeTupleNamesAttribute(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
		{
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol specialType = this.GetSpecialType(SpecialType.System_String);
			ImmutableArray<TypedConstant> immutableArray = CSharpCompilation.TupleNamesEncoder.Encode(type, specialType);
			ImmutableArray<TypedConstant> immutableArray2 = ImmutableArray.Create<TypedConstant>(new TypedConstant(Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol.CreateSZArray(specialType.ContainingAssembly, TypeWithAnnotations.Create(specialType, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>))), immutableArray));
			return this.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_TupleElementNamesAttribute__ctorTransformNames, immutableArray2, default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0006F384 File Offset: 0x0006D584
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal SynthesizedAttributeData SynthesizeAttributeUsageAttribute(AttributeTargets targets, bool allowMultiple, bool inherited)
		{
			ITypeSymbolInternal wellKnownType = this.GetWellKnownType(WellKnownType.System_AttributeTargets);
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol specialType = this.GetSpecialType(SpecialType.System_Boolean);
			ImmutableArray<TypedConstant> immutableArray = ImmutableArray.Create<TypedConstant>(new TypedConstant(wellKnownType, TypedConstantKind.Enum, targets));
			ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>> immutableArray2 = ImmutableArray.Create<KeyValuePair<WellKnownMember, TypedConstant>>(new KeyValuePair<WellKnownMember, TypedConstant>(WellKnownMember.System_AttributeUsageAttribute__AllowMultiple, new TypedConstant(specialType, TypedConstantKind.Primitive, allowMultiple)), new KeyValuePair<WellKnownMember, TypedConstant>(WellKnownMember.System_AttributeUsageAttribute__Inherited, new TypedConstant(specialType, TypedConstantKind.Primitive, inherited)));
			return this.TrySynthesizeAttribute(WellKnownMember.System_AttributeUsageAttribute__ctor, immutableArray, immutableArray2, false);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0006F470 File Offset: 0x0006D670
		[CompilerGenerated]
		internal static bool <FindEntryPoint>g__checkValid|160_0(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol candidate, bool isCandidate, BindingDiagnosticBag specificDiagnostics, ref CSharpCompilation.<>c__DisplayClass160_0 A_3)
		{
			if (!isCandidate)
			{
				A_3.noMainFoundDiagnostics.Add(ErrorCode.WRN_InvalidMainSig, candidate.Locations.First<Location>(), new object[] { candidate });
				A_3.noMainFoundDiagnostics.AddRange(specificDiagnostics, false);
				return false;
			}
			if (candidate.IsGenericMethod || candidate.ContainingType.IsGenericType)
			{
				A_3.noMainFoundDiagnostics.Add(ErrorCode.WRN_MainCantBeGeneric, candidate.Locations.First<Location>(), new object[] { candidate });
				return false;
			}
			return true;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0006F4F0 File Offset: 0x0006D6F0
		[CompilerGenerated]
		private void <GetDiagnosticsForMethodBodiesInTree>g__compileMethodBodiesAndDocComments|217_0([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxTree filterTree, TextSpan? filterSpan, BindingDiagnosticBag bindingDiagnostics, CancellationToken cancellationToken)
		{
			MethodCompiler.CompileMethodBodies(this, null, false, false, false, false, bindingDiagnostics, (filterTree != null) ? ((Symbol s) => CSharpCompilation.IsDefinedOrImplementedInSourceTree(s, filterTree, filterSpan)) : null, cancellationToken);
			DocumentationCommentCompiler.WriteDocumentationCommentXml(this, null, null, bindingDiagnostics, cancellationToken, filterTree, filterSpan);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0006F550 File Offset: 0x0006D750
		[CompilerGenerated]
		private void <GetDiagnosticsForMethodBodiesInTree>g__registeredUsageOfUsingsInTree|217_1(SyntaxTree tree)
		{
			ImmutableHashSet<SyntaxTree> immutableHashSet3;
			for (ImmutableHashSet<SyntaxTree> immutableHashSet = this.UsageOfUsingsRecordedInTrees; immutableHashSet != null; immutableHashSet = immutableHashSet3)
			{
				ImmutableHashSet<SyntaxTree> immutableHashSet2 = immutableHashSet.Add(tree);
				if (immutableHashSet2 == immutableHashSet)
				{
					break;
				}
				if (immutableHashSet2.Count == this.SyntaxTrees.Length)
				{
					this._usageOfUsingsRecordedInTrees = null;
					return;
				}
				immutableHashSet3 = Interlocked.CompareExchange<ImmutableHashSet<SyntaxTree>>(ref this._usageOfUsingsRecordedInTrees, immutableHashSet2, immutableHashSet);
				if (immutableHashSet3 == immutableHashSet)
				{
					break;
				}
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0006F5A8 File Offset: 0x0006D7A8
		[CompilerGenerated]
		internal static CustomModifier <CommonCreateFunctionPointerTypeSymbol>g__getCustomModifierForType|272_2(INamedTypeSymbol type, CSharpCompilation @this, int index)
		{
			if (type == null)
			{
				throw new ArgumentNullException(string.Format("{0}[{1}]", "callingConventionTypes", index));
			}
			Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = type.EnsureCSharpSymbolOrNull(string.Format("{0}[{1}]", "callingConventionTypes", index));
			if (!Microsoft.CodeAnalysis.CSharp.Symbols.FunctionPointerTypeSymbol.IsCallingConventionModifier(namedTypeSymbol) || @this.Assembly.CorLibrary != namedTypeSymbol.ContainingAssembly)
			{
				throw new ArgumentException(string.Format(CSharpResources.CallingConventionTypeIsInvalid, type.ToDisplayString(null)));
			}
			return CSharpCustomModifier.CreateOptional(namedTypeSymbol);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0006F62C File Offset: 0x0006D82C
		[CompilerGenerated]
		internal static Symbol <ShouldEmitNullableAttributes>g__getExplicitAccessibilitySymbol|296_0(Symbol symbol)
		{
			for (;;)
			{
				SymbolKind kind = symbol.Kind;
				if (kind != SymbolKind.Event)
				{
					switch (kind)
					{
					case SymbolKind.Parameter:
					case SymbolKind.Property:
					case SymbolKind.TypeParameter:
						goto IL_002A;
					}
					break;
				}
				IL_002A:
				symbol = symbol.ContainingSymbol;
			}
			return symbol;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0006F66E File Offset: 0x0006D86E
		[CompilerGenerated]
		internal static void <SerializePdbEmbeddedCompilationOptions>g__WriteValue|299_0(string key, string value, ref CSharpCompilation.<>c__DisplayClass299_0 A_2)
		{
			A_2.builder.WriteUTF8(key, true);
			A_2.builder.WriteByte(0);
			A_2.builder.WriteUTF8(value, true);
			A_2.builder.WriteByte(0);
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0006F6A2 File Offset: 0x0006D8A2
		[CompilerGenerated]
		private void <GetCompleteSetOfUsedAssemblies>g__addUsedAssembly|310_0(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol dependency, ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> stack)
		{
			if (this.AddUsedAssembly(dependency))
			{
				stack.Push(dependency);
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0006F6B4 File Offset: 0x0006D8B4
		[CompilerGenerated]
		private void <GetCompleteSetOfUsedAssemblies>g__addReferencedAssemblies|310_1(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assembly, bool includeMainModule, ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> stack)
		{
			for (int i = (includeMainModule ? 0 : 1); i < assembly.Modules.Length; i++)
			{
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol in assembly.Modules[i].ReferencedAssemblySymbols)
				{
					this.<GetCompleteSetOfUsedAssemblies>g__addUsedAssembly|310_0(assemblySymbol, stack);
				}
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0006F71C File Offset: 0x0006D91C
		[CompilerGenerated]
		private void <GetCompleteSetOfUsedAssemblies>g__completeTheSetOfUsedAssemblies|310_2(bool seenErrors, CancellationToken cancellationToken)
		{
			if (this._usedAssemblyReferencesFrozen || Volatile.Read(ref this._usedAssemblyReferencesFrozen))
			{
				return;
			}
			if (seenErrors)
			{
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol in this.SourceModule.ReferencedAssemblySymbols)
				{
					this.AddUsedAssembly(assemblySymbol);
				}
			}
			else
			{
				for (int i = 1; i < this.SourceAssembly.Modules.Length; i++)
				{
					foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol2 in this.SourceAssembly.Modules[i].ReferencedAssemblySymbols)
					{
						this.AddUsedAssembly(assemblySymbol2);
					}
				}
				if (this._usedAssemblyReferencesFrozen || Volatile.Read(ref this._usedAssemblyReferencesFrozen))
				{
					return;
				}
				if (this._lazyUsedAssemblyReferences != null)
				{
					ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> lazyUsedAssemblyReferences = this._lazyUsedAssemblyReferences;
					lock (lazyUsedAssemblyReferences)
					{
						if (this._usedAssemblyReferencesFrozen || Volatile.Read(ref this._usedAssemblyReferencesFrozen))
						{
							return;
						}
						ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> instance = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.GetInstance(this._lazyUsedAssemblyReferences.Count);
						instance.AddRange(this._lazyUsedAssemblyReferences);
						while (instance.Count != 0)
						{
							Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol3 = instance.Pop<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>();
							Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssemblySymbol = assemblySymbol3 as Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol;
							if (sourceAssemblySymbol == null)
							{
								RetargetingAssemblySymbol retargetingAssemblySymbol = assemblySymbol3 as RetargetingAssemblySymbol;
								if (retargetingAssemblySymbol == null)
								{
									this.<GetCompleteSetOfUsedAssemblies>g__addReferencedAssemblies|310_1(assemblySymbol3, true, instance);
								}
								else
								{
									ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> concurrentSet = retargetingAssemblySymbol.UnderlyingAssembly.DeclaringCompilation.GetCompleteSetOfUsedAssemblies(cancellationToken);
									if (concurrentSet != null)
									{
										foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol4 in retargetingAssemblySymbol.UnderlyingAssembly.SourceModule.ReferencedAssemblySymbols)
										{
											if (!assemblySymbol4.IsLinked && concurrentSet.Contains(assemblySymbol4))
											{
												Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol5;
												if (!((RetargetingModuleSymbol)retargetingAssemblySymbol.Modules[0]).RetargetingDefinitions(assemblySymbol4, out assemblySymbol5))
												{
													assemblySymbol5 = assemblySymbol4;
												}
												this.<GetCompleteSetOfUsedAssemblies>g__addUsedAssembly|310_0(assemblySymbol5, instance);
											}
										}
									}
									this.<GetCompleteSetOfUsedAssemblies>g__addReferencedAssemblies|310_1(retargetingAssemblySymbol, false, instance);
								}
							}
							else
							{
								ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> concurrentSet = sourceAssemblySymbol.DeclaringCompilation.GetCompleteSetOfUsedAssemblies(cancellationToken);
								if (concurrentSet != null)
								{
									foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol6 in concurrentSet)
									{
										this.<GetCompleteSetOfUsedAssemblies>g__addUsedAssembly|310_0(assemblySymbol6, instance);
									}
								}
							}
						}
						instance.Free();
					}
				}
				if (this.SourceAssembly.CorLibrary != null)
				{
					this.AddUsedAssembly(this.SourceAssembly.CorLibrary);
				}
			}
			this._usedAssemblyReferencesFrozen = true;
		}

		// Token: 0x040005FC RID: 1532
		private readonly CSharpCompilationOptions _options;

		// Token: 0x040005FD RID: 1533
		private readonly Lazy<CSharpCompilation.UsingsFromOptionsAndDiagnostics> _usingsFromOptions;

		// Token: 0x040005FE RID: 1534
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0 })]
		private readonly Lazy<ImmutableArray<NamespaceOrTypeAndUsingDirective>> _globalImports;

		// Token: 0x040005FF RID: 1535
		private readonly Lazy<Imports> _previousSubmissionImports;

		// Token: 0x04000600 RID: 1536
		private readonly Lazy<Microsoft.CodeAnalysis.CSharp.Symbols.AliasSymbol> _globalNamespaceAlias;

		// Token: 0x04000601 RID: 1537
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })]
		private readonly Lazy<ImplicitNamedTypeSymbol> _scriptClass;

		// Token: 0x04000602 RID: 1538
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol _lazyHostObjectTypeSymbol;

		// Token: 0x04000603 RID: 1539
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })]
		private ConcurrentDictionary<CSharpCompilation.ImportInfo, ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> _lazyImportInfos;

		// Token: 0x04000604 RID: 1540
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> _lazyClsComplianceDiagnostics;

		// Token: 0x04000605 RID: 1541
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> _lazyClsComplianceDependencies;

		// Token: 0x04000606 RID: 1542
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Conversions _conversions;

		// Token: 0x04000607 RID: 1543
		private readonly AnonymousTypeManager _anonymousTypeManager;

		// Token: 0x04000608 RID: 1544
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol _lazyGlobalNamespace;

		// Token: 0x04000609 RID: 1545
		internal readonly BuiltInOperators builtInOperators;

		// Token: 0x0400060A RID: 1546
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol _lazyAssemblySymbol;

		// Token: 0x0400060B RID: 1547
		private CSharpCompilation.ReferenceManager _referenceManager;

		// Token: 0x0400060C RID: 1548
		private readonly SyntaxAndDeclarationManager _syntaxAndDeclarations;

		// Token: 0x0400060D RID: 1549
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private CSharpCompilation.EntryPoint _lazyEntryPoint;

		// Token: 0x0400060E RID: 1550
		private ThreeState _lazyEmitNullablePublicOnly;

		// Token: 0x0400060F RID: 1551
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private HashSet<SyntaxTree> _lazyCompilationUnitCompletedTrees;

		// Token: 0x04000610 RID: 1552
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private ImmutableHashSet<SyntaxTree> _usageOfUsingsRecordedInTrees = ImmutableHashSet<SyntaxTree>.Empty;

		// Token: 0x04000611 RID: 1553
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal CSharpCompilation.NullableData NullableAnalysisData;

		// Token: 0x04000613 RID: 1555
		private static readonly CSharpCompilationOptions s_defaultOptions = new CSharpCompilationOptions(OutputKind.ConsoleApplication, false, null, null, null, null, OptimizationLevel.Debug, false, false, null, null, default(ImmutableArray<byte>), default(bool?), Platform.AnyCpu, ReportDiagnostic.Default, 4, null, true, false, null, null, null, null, null, false, MetadataImportOptions.Public, NullableContextOptions.Disable);

		// Token: 0x04000614 RID: 1556
		private static readonly CSharpCompilationOptions s_defaultSubmissionOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, false, null, null, null, null, OptimizationLevel.Debug, false, false, null, null, default(ImmutableArray<byte>), default(bool?), Platform.AnyCpu, ReportDiagnostic.Default, 4, null, true, false, null, null, null, null, null, false, MetadataImportOptions.Public, NullableContextOptions.Disable).WithReferencesSupersedeLowerVersions(true);

		// Token: 0x04000616 RID: 1558
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private ConcurrentDictionary<string, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol> _externAliasTargets;

		// Token: 0x04000617 RID: 1559
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol> _moduleInitializerMethods;

		// Token: 0x04000618 RID: 1560
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private WeakReference<BinderFactory>[] _binderFactories;

		// Token: 0x04000619 RID: 1561
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private WeakReference<BinderFactory>[] _ignoreAccessibilityBinderFactories;

		// Token: 0x0400061A RID: 1562
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private DiagnosticBag _lazyDeclarationDiagnostics;

		// Token: 0x0400061B RID: 1563
		private bool _declarationDiagnosticsFrozen;

		// Token: 0x0400061C RID: 1564
		private readonly DiagnosticBag _additionalCodegenWarnings = new DiagnosticBag();

		// Token: 0x0400061D RID: 1565
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
		private ConcurrentSet<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> _lazyUsedAssemblyReferences;

		// Token: 0x0400061E RID: 1566
		private bool _usedAssemblyReferencesFrozen;

		// Token: 0x0400061F RID: 1567
		internal readonly CSharpCompilation.WellKnownMembersSignatureComparer WellKnownMemberSignatureComparer;

		// Token: 0x04000620 RID: 1568
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol[] _lazyWellKnownTypes;

		// Token: 0x04000621 RID: 1569
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private Symbol[] _lazyWellKnownTypeMembers;

		// Token: 0x04000622 RID: 1570
		private bool _usesNullableAttributes;

		// Token: 0x04000623 RID: 1571
		private int _needsGeneratedAttributes;

		// Token: 0x04000624 RID: 1572
		private bool _needsGeneratedAttributes_IsFrozen;

		// Token: 0x0200069B RID: 1691
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal sealed class NullableData
		{
			// Token: 0x06008385 RID: 33669 RVA: 0x001DCBF5 File Offset: 0x001DADF5
			internal NullableData(int maxRecursionDepth = -1)
			{
				this.MaxRecursionDepth = maxRecursionDepth;
				this.Data = new ConcurrentDictionary<object, NullableWalker.Data>();
			}

			// Token: 0x040020FF RID: 8447
			internal readonly int MaxRecursionDepth;

			// Token: 0x04002100 RID: 8448
			[global::System.Runtime.CompilerServices.Nullable(1)]
			internal readonly ConcurrentDictionary<object, NullableWalker.Data> Data;
		}

		// Token: 0x0200069C RID: 1692
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal class EntryPoint
		{
			// Token: 0x06008386 RID: 33670 RVA: 0x001DCC0F File Offset: 0x001DAE0F
			public EntryPoint(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol methodSymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> diagnostics)
			{
				this.MethodSymbol = methodSymbol;
				this.Diagnostics = diagnostics;
			}

			// Token: 0x04002101 RID: 8449
			public readonly Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol MethodSymbol;

			// Token: 0x04002102 RID: 8450
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			public readonly ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> Diagnostics;

			// Token: 0x04002103 RID: 8451
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public static readonly CSharpCompilation.EntryPoint None = new CSharpCompilation.EntryPoint(null, ImmutableBindingDiagnostic<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Empty);
		}

		// Token: 0x0200069D RID: 1693
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private struct ImportInfo : IEquatable<CSharpCompilation.ImportInfo>
		{
			// Token: 0x06008388 RID: 33672 RVA: 0x001DCC37 File Offset: 0x001DAE37
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public ImportInfo(SyntaxTree tree, SyntaxKind kind, TextSpan span)
			{
				this.Tree = tree;
				this.Kind = kind;
				this.Span = span;
			}

			// Token: 0x06008389 RID: 33673 RVA: 0x001DCC4E File Offset: 0x001DAE4E
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			public override bool Equals(object obj)
			{
				return obj is CSharpCompilation.ImportInfo && this.Equals((CSharpCompilation.ImportInfo)obj);
			}

			// Token: 0x0600838A RID: 33674 RVA: 0x001DCC66 File Offset: 0x001DAE66
			public bool Equals(CSharpCompilation.ImportInfo other)
			{
				return other.Kind == this.Kind && other.Tree == this.Tree && other.Span == this.Span;
			}

			// Token: 0x0600838B RID: 33675 RVA: 0x001DCC97 File Offset: 0x001DAE97
			public override int GetHashCode()
			{
				return Hash.Combine<SyntaxTree>(this.Tree, this.Span.Start);
			}

			// Token: 0x04002104 RID: 8452
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public readonly SyntaxTree Tree;

			// Token: 0x04002105 RID: 8453
			public readonly SyntaxKind Kind;

			// Token: 0x04002106 RID: 8454
			public readonly TextSpan Span;
		}

		// Token: 0x0200069E RID: 1694
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private abstract class AbstractSymbolSearcher
		{
			// Token: 0x0600838C RID: 33676 RVA: 0x001DCCB0 File Offset: 0x001DAEB0
			protected AbstractSymbolSearcher(CSharpCompilation compilation, SymbolFilter filter, CancellationToken cancellationToken)
			{
				this._cache = PooledDictionary<Declaration, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol>.GetInstance();
				this._compilation = compilation;
				this._includeNamespace = (filter & SymbolFilter.Namespace) == SymbolFilter.Namespace;
				this._includeType = (filter & SymbolFilter.Type) == SymbolFilter.Type;
				this._includeMember = (filter & SymbolFilter.Member) == SymbolFilter.Member;
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x0600838D RID: 33677
			protected abstract bool Matches(string name);

			// Token: 0x0600838E RID: 33678
			protected abstract bool ShouldCheckTypeForMembers(MergedTypeDeclaration current);

			// Token: 0x0600838F RID: 33679 RVA: 0x001DCD00 File Offset: 0x001DAF00
			public IEnumerable<Symbol> GetSymbolsWithName()
			{
				HashSet<Symbol> hashSet = new HashSet<Symbol>();
				ArrayBuilder<MergedNamespaceOrTypeDeclaration> instance = ArrayBuilder<MergedNamespaceOrTypeDeclaration>.GetInstance();
				this.AppendSymbolsWithName(instance, this._compilation.MergedRootDeclaration, hashSet);
				instance.Free();
				this._cache.Free();
				return hashSet;
			}

			// Token: 0x06008390 RID: 33680 RVA: 0x001DCD40 File Offset: 0x001DAF40
			private void AppendSymbolsWithName(ArrayBuilder<MergedNamespaceOrTypeDeclaration> spine, MergedNamespaceOrTypeDeclaration current, HashSet<Symbol> set)
			{
				if (current.Kind == DeclarationKind.Namespace)
				{
					if (this._includeNamespace && this.Matches(current.Name))
					{
						Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol spineSymbol = this.GetSpineSymbol(spine);
						Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol symbol = this.GetSymbol(spineSymbol, current);
						if (symbol != null)
						{
							set.Add(symbol);
						}
					}
				}
				else
				{
					if (this._includeType && this.Matches(current.Name))
					{
						Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol spineSymbol2 = this.GetSpineSymbol(spine);
						Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol symbol2 = this.GetSymbol(spineSymbol2, current);
						if (symbol2 != null)
						{
							set.Add(symbol2);
						}
					}
					if (this._includeMember)
					{
						MergedTypeDeclaration mergedTypeDeclaration = (MergedTypeDeclaration)current;
						if (this.ShouldCheckTypeForMembers(mergedTypeDeclaration))
						{
							this.AppendMemberSymbolsWithName(spine, mergedTypeDeclaration, set);
						}
					}
				}
				spine.Add(current);
				foreach (Declaration declaration in current.Children)
				{
					MergedNamespaceOrTypeDeclaration mergedNamespaceOrTypeDeclaration = declaration as MergedNamespaceOrTypeDeclaration;
					if (mergedNamespaceOrTypeDeclaration != null && (this._includeMember || this._includeType || declaration.Kind == DeclarationKind.Namespace))
					{
						this.AppendSymbolsWithName(spine, mergedNamespaceOrTypeDeclaration, set);
					}
				}
				spine.RemoveAt(spine.Count - 1);
			}

			// Token: 0x06008391 RID: 33681 RVA: 0x001DCE5C File Offset: 0x001DB05C
			private void AppendMemberSymbolsWithName(ArrayBuilder<MergedNamespaceOrTypeDeclaration> spine, MergedTypeDeclaration current, HashSet<Symbol> set)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				spine.Add(current);
				Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol spineSymbol = this.GetSpineSymbol(spine);
				if (spineSymbol != null)
				{
					foreach (Symbol symbol in spineSymbol.GetMembers())
					{
						if (!symbol.IsTypeOrTypeAlias() && (symbol.CanBeReferencedByName || symbol.IsExplicitInterfaceImplementation() || symbol.IsIndexer()) && this.Matches(symbol.Name))
						{
							set.Add(symbol);
						}
					}
				}
				spine.RemoveAt(spine.Count - 1);
			}

			// Token: 0x06008392 RID: 33682 RVA: 0x001DCEFC File Offset: 0x001DB0FC
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol GetSpineSymbol(ArrayBuilder<MergedNamespaceOrTypeDeclaration> spine)
			{
				if (spine.Count == 0)
				{
					return null;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol cachedSymbol = this.GetCachedSymbol(spine[spine.Count - 1]);
				if (cachedSymbol != null)
				{
					return cachedSymbol;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol namespaceOrTypeSymbol = this._compilation.GlobalNamespace;
				for (int i = 1; i < spine.Count; i++)
				{
					namespaceOrTypeSymbol = this.GetSymbol(namespaceOrTypeSymbol, spine[i]);
				}
				return namespaceOrTypeSymbol;
			}

			// Token: 0x06008393 RID: 33683 RVA: 0x001DCF60 File Offset: 0x001DB160
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			private Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol GetCachedSymbol(MergedNamespaceOrTypeDeclaration declaration)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol namespaceOrTypeSymbol;
				if (!this._cache.TryGetValue(declaration, ref namespaceOrTypeSymbol))
				{
					return null;
				}
				return namespaceOrTypeSymbol;
			}

			// Token: 0x06008394 RID: 33684 RVA: 0x001DCF80 File Offset: 0x001DB180
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			private Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol GetSymbol(Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol container, [global::System.Runtime.CompilerServices.Nullable(1)] MergedNamespaceOrTypeDeclaration declaration)
			{
				if (container == null)
				{
					return this._compilation.GlobalNamespace;
				}
				if (declaration.Kind == DeclarationKind.Namespace)
				{
					this.AddCache(container.GetMembers(declaration.Name).OfType<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol>());
				}
				else
				{
					this.AddCache(container.GetTypeMembers(declaration.Name));
				}
				return this.GetCachedSymbol(declaration);
			}

			// Token: 0x06008395 RID: 33685 RVA: 0x001DCFE4 File Offset: 0x001DB1E4
			private void AddCache(IEnumerable<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol> symbols)
			{
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol namespaceOrTypeSymbol in symbols)
				{
					MergedNamespaceSymbol mergedNamespaceSymbol = namespaceOrTypeSymbol as MergedNamespaceSymbol;
					if (mergedNamespaceSymbol != null)
					{
						this._cache[Enumerable.First<SourceNamespaceSymbol>(mergedNamespaceSymbol.ConstituentNamespaces.OfType<SourceNamespaceSymbol>()).MergedDeclaration] = namespaceOrTypeSymbol;
					}
					else
					{
						SourceNamespaceSymbol sourceNamespaceSymbol = namespaceOrTypeSymbol as SourceNamespaceSymbol;
						if (sourceNamespaceSymbol != null)
						{
							this._cache[sourceNamespaceSymbol.MergedDeclaration] = sourceNamespaceSymbol;
						}
						else
						{
							SourceMemberContainerTypeSymbol sourceMemberContainerTypeSymbol = namespaceOrTypeSymbol as SourceMemberContainerTypeSymbol;
							if (sourceMemberContainerTypeSymbol != null)
							{
								this._cache[sourceMemberContainerTypeSymbol.MergedDeclaration] = sourceMemberContainerTypeSymbol;
							}
						}
					}
				}
			}

			// Token: 0x04002107 RID: 8455
			private readonly PooledDictionary<Declaration, Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol> _cache;

			// Token: 0x04002108 RID: 8456
			private readonly CSharpCompilation _compilation;

			// Token: 0x04002109 RID: 8457
			private readonly bool _includeNamespace;

			// Token: 0x0400210A RID: 8458
			private readonly bool _includeType;

			// Token: 0x0400210B RID: 8459
			private readonly bool _includeMember;

			// Token: 0x0400210C RID: 8460
			private readonly CancellationToken _cancellationToken;
		}

		// Token: 0x0200069F RID: 1695
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class PredicateSymbolSearcher : CSharpCompilation.AbstractSymbolSearcher
		{
			// Token: 0x06008396 RID: 33686 RVA: 0x001DD0A8 File Offset: 0x001DB2A8
			public PredicateSymbolSearcher(CSharpCompilation compilation, SymbolFilter filter, Func<string, bool> predicate, CancellationToken cancellationToken)
				: base(compilation, filter, cancellationToken)
			{
				this._predicate = predicate;
			}

			// Token: 0x06008397 RID: 33687 RVA: 0x001DD0BB File Offset: 0x001DB2BB
			protected override bool ShouldCheckTypeForMembers(MergedTypeDeclaration current)
			{
				return true;
			}

			// Token: 0x06008398 RID: 33688 RVA: 0x001DD0BE File Offset: 0x001DB2BE
			protected override bool Matches(string name)
			{
				return this._predicate.Invoke(name);
			}

			// Token: 0x0400210D RID: 8461
			private readonly Func<string, bool> _predicate;
		}

		// Token: 0x020006A0 RID: 1696
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private class NameSymbolSearcher : CSharpCompilation.AbstractSymbolSearcher
		{
			// Token: 0x06008399 RID: 33689 RVA: 0x001DD0CC File Offset: 0x001DB2CC
			public NameSymbolSearcher(CSharpCompilation compilation, SymbolFilter filter, string name, CancellationToken cancellationToken)
				: base(compilation, filter, cancellationToken)
			{
				this._name = name;
			}

			// Token: 0x0600839A RID: 33690 RVA: 0x001DD0E0 File Offset: 0x001DB2E0
			protected override bool ShouldCheckTypeForMembers(MergedTypeDeclaration current)
			{
				ImmutableArray<SingleTypeDeclaration>.Enumerator enumerator = current.Declarations.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.MemberNames.Contains(this._name))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600839B RID: 33691 RVA: 0x001DD123 File Offset: 0x001DB323
			protected override bool Matches(string name)
			{
				return this._name == name;
			}

			// Token: 0x0400210E RID: 8462
			private readonly string _name;
		}

		// Token: 0x020006A1 RID: 1697
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		private class UsingsFromOptionsAndDiagnostics
		{
			// Token: 0x1700269D RID: 9885
			// (get) Token: 0x0600839C RID: 33692 RVA: 0x001DD131 File Offset: 0x001DB331
			// (set) Token: 0x0600839D RID: 33693 RVA: 0x001DD139 File Offset: 0x001DB339
			public ImmutableArray<NamespaceOrTypeAndUsingDirective> UsingNamespacesOrTypes { get; set; }

			// Token: 0x1700269E RID: 9886
			// (get) Token: 0x0600839E RID: 33694 RVA: 0x001DD142 File Offset: 0x001DB342
			// (set) Token: 0x0600839F RID: 33695 RVA: 0x001DD14A File Offset: 0x001DB34A
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public DiagnosticBag Diagnostics
			{
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				get;
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				set;
			}

			// Token: 0x060083A0 RID: 33696 RVA: 0x001DD154 File Offset: 0x001DB354
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			public static CSharpCompilation.UsingsFromOptionsAndDiagnostics FromOptions(CSharpCompilation compilation)
			{
				ImmutableArray<string> usings = compilation.Options.Usings;
				if (usings.Length == 0)
				{
					return CSharpCompilation.UsingsFromOptionsAndDiagnostics.Empty;
				}
				DiagnosticBag diagnosticBag = new DiagnosticBag();
				InContainerBinder inContainerBinder = new InContainerBinder(compilation.GlobalNamespace, new BuckStopsHereBinder(compilation));
				ArrayBuilder<NamespaceOrTypeAndUsingDirective> instance = ArrayBuilder<NamespaceOrTypeAndUsingDirective>.GetInstance();
				PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol> instance2 = PooledHashSet<Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol>.GetInstance();
				foreach (string text in usings)
				{
					if (text.IsValidClrNamespaceName())
					{
						string[] array = text.Split(new char[] { '.' });
						NameSyntax nameSyntax = SyntaxFactory.IdentifierName(array[0]);
						for (int i = 1; i < array.Length; i++)
						{
							nameSyntax = SyntaxFactory.QualifiedName(nameSyntax, SyntaxFactory.IdentifierName(array[i]));
						}
						BindingDiagnosticBag instance3 = BindingDiagnosticBag.GetInstance();
						Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol namespaceOrTypeSymbol = inContainerBinder.BindNamespaceOrTypeSymbol(nameSyntax, instance3, null).NamespaceOrTypeSymbol;
						if (instance2.Add(namespaceOrTypeSymbol))
						{
							instance.Add(new NamespaceOrTypeAndUsingDirective(namespaceOrTypeSymbol, null, instance3.DependenciesBag.ToImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>()));
						}
						diagnosticBag.AddRange(instance3.DiagnosticBag);
						instance3.Free();
					}
				}
				if (diagnosticBag.IsEmptyWithoutResolution)
				{
					diagnosticBag = null;
				}
				instance2.Free();
				if (instance.Count == 0 && diagnosticBag == null)
				{
					instance.Free();
					return CSharpCompilation.UsingsFromOptionsAndDiagnostics.Empty;
				}
				return new CSharpCompilation.UsingsFromOptionsAndDiagnostics
				{
					UsingNamespacesOrTypes = instance.ToImmutableAndFree(),
					Diagnostics = diagnosticBag
				};
			}

			// Token: 0x060083A1 RID: 33697 RVA: 0x001DD2AC File Offset: 0x001DB4AC
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			internal void Complete(CSharpCompilation compilation, CancellationToken cancellationToken)
			{
				for (;;)
				{
					cancellationToken.ThrowIfCancellationRequested();
					CompletionPart nextIncompletePart = this._state.NextIncompletePart;
					if (nextIncompletePart == CompletionPart.None)
					{
						break;
					}
					if (nextIncompletePart != CompletionPart.StartBaseType)
					{
						if (nextIncompletePart != CompletionPart.FinishBaseType)
						{
							this._state.NotePartComplete(CompletionPart.Attributes | CompletionPart.ReturnTypeAttributes | CompletionPart.Parameters | CompletionPart.Type | CompletionPart.StartInterfaces | CompletionPart.FinishInterfaces | CompletionPart.EnumUnderlyingType | CompletionPart.TypeArguments | CompletionPart.TypeParameters | CompletionPart.Members | CompletionPart.TypeMembers | CompletionPart.SynthesizedExplicitImplementations | CompletionPart.StartMemberChecks | CompletionPart.FinishMemberChecks | CompletionPart.MembersCompleted);
						}
						else
						{
							this._state.SpinWaitComplete(CompletionPart.FinishBaseType, cancellationToken);
						}
					}
					else if (this._state.NotePartComplete(CompletionPart.StartBaseType))
					{
						this.Validate(compilation);
						this._state.NotePartComplete(CompletionPart.FinishBaseType);
					}
					this._state.SpinWaitComplete(nextIncompletePart, cancellationToken);
				}
			}

			// Token: 0x060083A2 RID: 33698 RVA: 0x001DD334 File Offset: 0x001DB534
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			private void Validate(CSharpCompilation compilation)
			{
				CSharpCompilation.UsingsFromOptionsAndDiagnostics.<>c__DisplayClass12_0 CS$<>8__locals1;
				CS$<>8__locals1.compilation = compilation;
				if (this == CSharpCompilation.UsingsFromOptionsAndDiagnostics.Empty)
				{
					return;
				}
				DiagnosticBag declarationDiagnostics = CS$<>8__locals1.compilation.DeclarationDiagnostics;
				CS$<>8__locals1.diagnostics = BindingDiagnosticBag.GetInstance();
				TypeConversions typeConversions = new TypeConversions(CS$<>8__locals1.compilation.SourceAssembly.CorLibrary, false);
				foreach (NamespaceOrTypeAndUsingDirective namespaceOrTypeAndUsingDirective in this.UsingNamespacesOrTypes)
				{
					CS$<>8__locals1.diagnostics.Clear();
					CS$<>8__locals1.diagnostics.AddDependencies(namespaceOrTypeAndUsingDirective.Dependencies);
					Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol namespaceOrType = namespaceOrTypeAndUsingDirective.NamespaceOrType;
					if (namespaceOrType.IsType)
					{
						Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol)namespaceOrType;
						Location singleton = NoLocation.Singleton;
						typeSymbol.CheckAllConstraints(CS$<>8__locals1.compilation, typeConversions, singleton, CS$<>8__locals1.diagnostics);
					}
					declarationDiagnostics.AddRange(CS$<>8__locals1.diagnostics.DiagnosticBag);
					CSharpCompilation.UsingsFromOptionsAndDiagnostics.<Validate>g__recordImportDependencies|12_0(namespaceOrType, ref CS$<>8__locals1);
				}
				if (this.Diagnostics != null && !this.Diagnostics.IsEmptyWithoutResolution)
				{
					declarationDiagnostics.AddRange(this.Diagnostics.AsEnumerable());
				}
				CS$<>8__locals1.diagnostics.Free();
			}

			// Token: 0x060083A5 RID: 33701 RVA: 0x001DD467 File Offset: 0x001DB667
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			[CompilerGenerated]
			internal static void <Validate>g__recordImportDependencies|12_0(Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceOrTypeSymbol target, ref CSharpCompilation.UsingsFromOptionsAndDiagnostics.<>c__DisplayClass12_0 A_1)
			{
				if (target.IsNamespace)
				{
					A_1.diagnostics.AddAssembliesUsedByNamespaceReference((Microsoft.CodeAnalysis.CSharp.Symbols.NamespaceSymbol)target);
				}
				A_1.compilation.AddUsedAssemblies(A_1.diagnostics.DependenciesBag);
			}

			// Token: 0x0400210F RID: 8463
			[global::System.Runtime.CompilerServices.Nullable(1)]
			public static readonly CSharpCompilation.UsingsFromOptionsAndDiagnostics Empty = new CSharpCompilation.UsingsFromOptionsAndDiagnostics
			{
				UsingNamespacesOrTypes = ImmutableArray<NamespaceOrTypeAndUsingDirective>.Empty,
				Diagnostics = null
			};

			// Token: 0x04002112 RID: 8466
			private SymbolCompletionState _state;
		}

		// Token: 0x020006A2 RID: 1698
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class TupleNamesEncoder
		{
			// Token: 0x060083A6 RID: 33702 RVA: 0x001DD498 File Offset: 0x001DB698
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			public static ImmutableArray<string> Encode(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
			{
				ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
				if (!CSharpCompilation.TupleNamesEncoder.TryGetNames(type, instance))
				{
					instance.Free();
					return default(ImmutableArray<string>);
				}
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060083A7 RID: 33703 RVA: 0x001DD4CC File Offset: 0x001DB6CC
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			public static ImmutableArray<TypedConstant> Encode(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol stringType)
			{
				ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
				if (!CSharpCompilation.TupleNamesEncoder.TryGetNames(type, instance))
				{
					instance.Free();
					return default(ImmutableArray<TypedConstant>);
				}
				ImmutableArray<TypedConstant> immutableArray = instance.SelectAsArray((string name, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol constantType) => new TypedConstant(constantType, TypedConstantKind.Primitive, name), stringType);
				instance.Free();
				return immutableArray;
			}

			// Token: 0x060083A8 RID: 33704 RVA: 0x001DD524 File Offset: 0x001DB724
			internal static bool TryGetNames(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] ArrayBuilder<string> namesBuilder)
			{
				type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol t, ArrayBuilder<string> builder, bool _ignore) => CSharpCompilation.TupleNamesEncoder.AddNames(t, builder), namesBuilder, false);
				return namesBuilder.Any<string>((string name) => name != null);
			}

			// Token: 0x060083A9 RID: 33705 RVA: 0x001DD580 File Offset: 0x001DB780
			private static bool AddNames(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] ArrayBuilder<string> namesBuilder)
			{
				if (type.IsTupleType)
				{
					if (type.TupleElementNames.IsDefaultOrEmpty)
					{
						namesBuilder.AddMany(null, type.TupleElementTypesWithAnnotations.Length);
					}
					else
					{
						namesBuilder.AddRange(type.TupleElementNames);
					}
				}
				return false;
			}
		}

		// Token: 0x020006A3 RID: 1699
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class DynamicTransformsEncoder
		{
			// Token: 0x060083AA RID: 33706 RVA: 0x001DD5CC File Offset: 0x001DB7CC
			[return: global::System.Runtime.CompilerServices.Nullable(0)]
			internal static ImmutableArray<TypedConstant> Encode(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, RefKind refKind, int customModifiersCount, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol booleanType)
			{
				ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance();
				CSharpCompilation.DynamicTransformsEncoder.Encode(type, customModifiersCount, refKind, instance, true);
				ImmutableArray<TypedConstant> immutableArray = instance.SelectAsArray((bool flag, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol constantType) => new TypedConstant(constantType, TypedConstantKind.Primitive, flag), booleanType);
				instance.Free();
				return immutableArray;
			}

			// Token: 0x060083AB RID: 33707 RVA: 0x001DD618 File Offset: 0x001DB818
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal static ImmutableArray<bool> Encode([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, RefKind refKind, int customModifiersCount)
			{
				ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance();
				CSharpCompilation.DynamicTransformsEncoder.Encode(type, customModifiersCount, refKind, instance, true);
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060083AC RID: 33708 RVA: 0x001DD63C File Offset: 0x001DB83C
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			internal static ImmutableArray<bool> EncodeWithoutCustomModifierFlags([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, RefKind refKind)
			{
				ArrayBuilder<bool> instance = ArrayBuilder<bool>.GetInstance();
				CSharpCompilation.DynamicTransformsEncoder.Encode(type, -1, refKind, instance, false);
				return instance.ToImmutableAndFree();
			}

			// Token: 0x060083AD RID: 33709 RVA: 0x001DD660 File Offset: 0x001DB860
			internal static void Encode(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int customModifiersCount, RefKind refKind, ArrayBuilder<bool> transformFlagsBuilder, bool addCustomModifierFlags)
			{
				if (refKind != RefKind.None)
				{
					transformFlagsBuilder.Add(false);
				}
				if (addCustomModifierFlags)
				{
					CSharpCompilation.DynamicTransformsEncoder.HandleCustomModifiers(customModifiersCount, transformFlagsBuilder);
					type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol, ArrayBuilder<bool> builder, bool isNested) => CSharpCompilation.DynamicTransformsEncoder.AddFlags(typeSymbol, builder, isNested, true), transformFlagsBuilder, false);
					return;
				}
				type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol, ArrayBuilder<bool> builder, bool isNested) => CSharpCompilation.DynamicTransformsEncoder.AddFlags(typeSymbol, builder, isNested, false), transformFlagsBuilder, false);
			}

			// Token: 0x060083AE RID: 33710 RVA: 0x001DD6D4 File Offset: 0x001DB8D4
			private static bool AddFlags(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, ArrayBuilder<bool> transformFlagsBuilder, bool isNestedNamedType, bool addCustomModifierFlags)
			{
				TypeKind typeKind = type.TypeKind;
				if (typeKind <= TypeKind.Dynamic)
				{
					if (typeKind == TypeKind.Array)
					{
						if (addCustomModifierFlags)
						{
							CSharpCompilation.DynamicTransformsEncoder.HandleCustomModifiers(((Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol)type).ElementTypeWithAnnotations.CustomModifiers.Length, transformFlagsBuilder);
						}
						transformFlagsBuilder.Add(false);
						return false;
					}
					if (typeKind == TypeKind.Dynamic)
					{
						transformFlagsBuilder.Add(true);
						return false;
					}
				}
				else
				{
					if (typeKind == TypeKind.Pointer)
					{
						if (addCustomModifierFlags)
						{
							CSharpCompilation.DynamicTransformsEncoder.HandleCustomModifiers(((Microsoft.CodeAnalysis.CSharp.Symbols.PointerTypeSymbol)type).PointedAtTypeWithAnnotations.CustomModifiers.Length, transformFlagsBuilder);
						}
						transformFlagsBuilder.Add(false);
						return false;
					}
					if (typeKind == TypeKind.FunctionPointer)
					{
						CSharpCompilation.DynamicTransformsEncoder.<AddFlags>g__handleFunctionPointerType|4_0((Microsoft.CodeAnalysis.CSharp.Symbols.FunctionPointerTypeSymbol)type, transformFlagsBuilder, addCustomModifierFlags);
						return true;
					}
				}
				if (!isNestedNamedType)
				{
					transformFlagsBuilder.Add(false);
				}
				return false;
			}

			// Token: 0x060083AF RID: 33711 RVA: 0x001DD77F File Offset: 0x001DB97F
			private static void HandleCustomModifiers(int customModifiersCount, ArrayBuilder<bool> transformFlagsBuilder)
			{
				transformFlagsBuilder.AddMany(false, customModifiersCount);
			}

			// Token: 0x060083B0 RID: 33712 RVA: 0x001DD78C File Offset: 0x001DB98C
			[CompilerGenerated]
			internal static void <AddFlags>g__handleFunctionPointerType|4_0(Microsoft.CodeAnalysis.CSharp.Symbols.FunctionPointerTypeSymbol funcPtr, ArrayBuilder<bool> transformFlagsBuilder, bool addCustomModifierFlags)
			{
				CSharpCompilation.DynamicTransformsEncoder.<>c__DisplayClass4_0 CS$<>8__locals1;
				CS$<>8__locals1.addCustomModifierFlags = addCustomModifierFlags;
				CS$<>8__locals1.transformFlagsBuilder = transformFlagsBuilder;
				CS$<>8__locals1.visitor = (Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, [TupleElementNames(new string[] { "builder", "addCustomModifierFlags" })] [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ValueTuple<ArrayBuilder<bool>, bool> param, bool isNestedNamedType) => CSharpCompilation.DynamicTransformsEncoder.AddFlags(type, param.Item1, isNestedNamedType, param.Item2);
				CS$<>8__locals1.transformFlagsBuilder.Add(false);
				FunctionPointerMethodSymbol signature = funcPtr.Signature;
				CSharpCompilation.DynamicTransformsEncoder.<AddFlags>g__handle|4_2(signature.RefKind, signature.RefCustomModifiers, signature.ReturnTypeWithAnnotations, ref CS$<>8__locals1);
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameterSymbol in signature.Parameters)
				{
					CSharpCompilation.DynamicTransformsEncoder.<AddFlags>g__handle|4_2(parameterSymbol.RefKind, parameterSymbol.RefCustomModifiers, parameterSymbol.TypeWithAnnotations, ref CS$<>8__locals1);
				}
			}

			// Token: 0x060083B1 RID: 33713 RVA: 0x001DD83C File Offset: 0x001DBA3C
			[CompilerGenerated]
			internal static void <AddFlags>g__handle|4_2(RefKind refKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> customModifiers, TypeWithAnnotations twa, ref CSharpCompilation.DynamicTransformsEncoder.<>c__DisplayClass4_0 A_3)
			{
				if (A_3.addCustomModifierFlags)
				{
					CSharpCompilation.DynamicTransformsEncoder.HandleCustomModifiers(customModifiers.Length, A_3.transformFlagsBuilder);
				}
				if (refKind != RefKind.None)
				{
					A_3.transformFlagsBuilder.Add(false);
				}
				if (A_3.addCustomModifierFlags)
				{
					CSharpCompilation.DynamicTransformsEncoder.HandleCustomModifiers(twa.CustomModifiers.Length, A_3.transformFlagsBuilder);
				}
				twa.Type.VisitType(A_3.visitor, new ValueTuple<ArrayBuilder<bool>, bool>(A_3.transformFlagsBuilder, A_3.addCustomModifierFlags), false);
			}
		}

		// Token: 0x020006A4 RID: 1700
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static class NativeIntegerTransformsEncoder
		{
			// Token: 0x060083B2 RID: 33714 RVA: 0x001DD8B9 File Offset: 0x001DBAB9
			internal static void Encode(ArrayBuilder<bool> builder, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
			{
				type.VisitType(([global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol typeSymbol, ArrayBuilder<bool> builder, bool isNested) => CSharpCompilation.NativeIntegerTransformsEncoder.AddFlags(typeSymbol, builder), builder, false);
			}

			// Token: 0x060083B3 RID: 33715 RVA: 0x001DD8E4 File Offset: 0x001DBAE4
			private static bool AddFlags(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, ArrayBuilder<bool> builder)
			{
				SpecialType specialType = type.SpecialType;
				if (specialType - SpecialType.System_IntPtr <= 1)
				{
					builder.Add(type.IsNativeIntegerType);
				}
				return false;
			}
		}

		// Token: 0x020006A5 RID: 1701
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1, 1, 1 })]
		internal class SpecialMembersSignatureComparer : SignatureComparer<Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol, Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol, Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol>
		{
			// Token: 0x060083B4 RID: 33716 RVA: 0x001DD90C File Offset: 0x001DBB0C
			protected SpecialMembersSignatureComparer()
			{
			}

			// Token: 0x060083B5 RID: 33717 RVA: 0x001DD914 File Offset: 0x001DBB14
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetMDArrayElementType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
			{
				if (type.Kind != SymbolKind.ArrayType)
				{
					return null;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol arrayTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol)type;
				if (arrayTypeSymbol.IsSZArray)
				{
					return null;
				}
				return arrayTypeSymbol.ElementType;
			}

			// Token: 0x060083B6 RID: 33718 RVA: 0x001DD943 File Offset: 0x001DBB43
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetFieldType(Microsoft.CodeAnalysis.CSharp.Symbols.FieldSymbol field)
			{
				return field.Type;
			}

			// Token: 0x060083B7 RID: 33719 RVA: 0x001DD94B File Offset: 0x001DBB4B
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetPropertyType(Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol property)
			{
				return property.Type;
			}

			// Token: 0x060083B8 RID: 33720 RVA: 0x001DD954 File Offset: 0x001DBB54
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetGenericTypeArgument(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int argumentIndex)
			{
				if (type.Kind != SymbolKind.NamedType)
				{
					return null;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)type;
				if (namedTypeSymbol.Arity <= argumentIndex)
				{
					return null;
				}
				if (namedTypeSymbol.ContainingType != null)
				{
					return null;
				}
				return namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[argumentIndex].Type;
			}

			// Token: 0x060083B9 RID: 33721 RVA: 0x001DD9A0 File Offset: 0x001DBBA0
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetGenericTypeDefinition(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
			{
				if (type.Kind != SymbolKind.NamedType)
				{
					return null;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol namedTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.NamedTypeSymbol)type;
				if (namedTypeSymbol.ContainingType != null)
				{
					return null;
				}
				if (namedTypeSymbol.Arity == 0)
				{
					return null;
				}
				return namedTypeSymbol.OriginalDefinition;
			}

			// Token: 0x060083BA RID: 33722 RVA: 0x001DD9DA File Offset: 0x001DBBDA
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			protected override ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol> GetParameters(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method)
			{
				return method.Parameters;
			}

			// Token: 0x060083BB RID: 33723 RVA: 0x001DD9E2 File Offset: 0x001DBBE2
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			protected override ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol> GetParameters(Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol property)
			{
				return property.Parameters;
			}

			// Token: 0x060083BC RID: 33724 RVA: 0x001DD9EA File Offset: 0x001DBBEA
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetParamType(Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameter)
			{
				return parameter.Type;
			}

			// Token: 0x060083BD RID: 33725 RVA: 0x001DD9F2 File Offset: 0x001DBBF2
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetPointedToType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
			{
				if (type.Kind != SymbolKind.PointerType)
				{
					return null;
				}
				return ((Microsoft.CodeAnalysis.CSharp.Symbols.PointerTypeSymbol)type).PointedAtType;
			}

			// Token: 0x060083BE RID: 33726 RVA: 0x001DDA0B File Offset: 0x001DBC0B
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetReturnType(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method)
			{
				return method.ReturnType;
			}

			// Token: 0x060083BF RID: 33727 RVA: 0x001DDA14 File Offset: 0x001DBC14
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol GetSZArrayElementType(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type)
			{
				if (type.Kind != SymbolKind.ArrayType)
				{
					return null;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol arrayTypeSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol)type;
				if (!arrayTypeSymbol.IsSZArray)
				{
					return null;
				}
				return arrayTypeSymbol.ElementType;
			}

			// Token: 0x060083C0 RID: 33728 RVA: 0x001DDA43 File Offset: 0x001DBC43
			protected override bool IsByRefParam(Microsoft.CodeAnalysis.CSharp.Symbols.ParameterSymbol parameter)
			{
				return parameter.RefKind > RefKind.None;
			}

			// Token: 0x060083C1 RID: 33729 RVA: 0x001DDA4E File Offset: 0x001DBC4E
			protected override bool IsByRefMethod(Microsoft.CodeAnalysis.CSharp.Symbols.MethodSymbol method)
			{
				return method.RefKind > RefKind.None;
			}

			// Token: 0x060083C2 RID: 33730 RVA: 0x001DDA59 File Offset: 0x001DBC59
			protected override bool IsByRefProperty(Microsoft.CodeAnalysis.CSharp.Symbols.PropertySymbol property)
			{
				return property.RefKind > RefKind.None;
			}

			// Token: 0x060083C3 RID: 33731 RVA: 0x001DDA64 File Offset: 0x001DBC64
			protected override bool IsGenericMethodTypeParam(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int paramPosition)
			{
				if (type.Kind != SymbolKind.TypeParameter)
				{
					return false;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeParameterSymbol typeParameterSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.TypeParameterSymbol)type;
				return typeParameterSymbol.ContainingSymbol.Kind == SymbolKind.Method && typeParameterSymbol.Ordinal == paramPosition;
			}

			// Token: 0x060083C4 RID: 33732 RVA: 0x001DDAA0 File Offset: 0x001DBCA0
			protected override bool IsGenericTypeParam(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int paramPosition)
			{
				if (type.Kind != SymbolKind.TypeParameter)
				{
					return false;
				}
				Microsoft.CodeAnalysis.CSharp.Symbols.TypeParameterSymbol typeParameterSymbol = (Microsoft.CodeAnalysis.CSharp.Symbols.TypeParameterSymbol)type;
				return typeParameterSymbol.ContainingSymbol.Kind == SymbolKind.NamedType && typeParameterSymbol.Ordinal == paramPosition;
			}

			// Token: 0x060083C5 RID: 33733 RVA: 0x001DDADA File Offset: 0x001DBCDA
			protected override bool MatchArrayRank(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int countOfDimensions)
			{
				return type.Kind == SymbolKind.ArrayType && ((Microsoft.CodeAnalysis.CSharp.Symbols.ArrayTypeSymbol)type).Rank == countOfDimensions;
			}

			// Token: 0x060083C6 RID: 33734 RVA: 0x001DDAF5 File Offset: 0x001DBCF5
			protected override bool MatchTypeToTypeId(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int typeId)
			{
				return (int)type.OriginalDefinition.SpecialType == typeId && (type.IsDefinition || type.Equals(type.OriginalDefinition, TypeCompareKind.IgnoreNullableModifiersForReferenceTypes));
			}

			// Token: 0x04002113 RID: 8467
			public static readonly CSharpCompilation.SpecialMembersSignatureComparer Instance = new CSharpCompilation.SpecialMembersSignatureComparer();
		}

		// Token: 0x020006A6 RID: 1702
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal sealed class WellKnownMembersSignatureComparer : CSharpCompilation.SpecialMembersSignatureComparer
		{
			// Token: 0x060083C8 RID: 33736 RVA: 0x001DDB2A File Offset: 0x001DBD2A
			public WellKnownMembersSignatureComparer(CSharpCompilation compilation)
			{
				this._compilation = compilation;
			}

			// Token: 0x060083C9 RID: 33737 RVA: 0x001DDB3C File Offset: 0x001DBD3C
			protected override bool MatchTypeToTypeId(Microsoft.CodeAnalysis.CSharp.Symbols.TypeSymbol type, int typeId)
			{
				if (((WellKnownType)typeId).IsWellKnownType())
				{
					return type.Equals(this._compilation.GetWellKnownType((WellKnownType)typeId), TypeCompareKind.IgnoreNullableModifiersForReferenceTypes);
				}
				return base.MatchTypeToTypeId(type, typeId);
			}

			// Token: 0x04002114 RID: 8468
			private readonly CSharpCompilation _compilation;
		}

		// Token: 0x020006A7 RID: 1703
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
		internal sealed class ReferenceManager : CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>
		{
			// Token: 0x060083CA RID: 33738 RVA: 0x001DDB6F File Offset: 0x001DBD6F
			public ReferenceManager(string simpleAssemblyName, AssemblyIdentityComparer identityComparer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] Dictionary<MetadataReference, object> observedMetadata)
				: base(simpleAssemblyName, identityComparer, observedMetadata)
			{
			}

			// Token: 0x1700269F RID: 9887
			// (get) Token: 0x060083CB RID: 33739 RVA: 0x001DDB7A File Offset: 0x001DBD7A
			protected override CommonMessageProvider MessageProvider
			{
				get
				{
					return Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance;
				}
			}

			// Token: 0x060083CC RID: 33740 RVA: 0x001DDB81 File Offset: 0x001DBD81
			protected override CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData CreateAssemblyDataForFile(PEAssembly assembly, WeakList<IAssemblySymbolInternal> cachedSymbols, DocumentationProvider documentationProvider, string sourceAssemblySimpleName, MetadataImportOptions importOptions, bool embedInteropTypes)
			{
				return new CSharpCompilation.ReferenceManager.AssemblyDataForFile(assembly, cachedSymbols, embedInteropTypes, documentationProvider, sourceAssemblySimpleName, importOptions);
			}

			// Token: 0x060083CD RID: 33741 RVA: 0x001DDB94 File Offset: 0x001DBD94
			protected override CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData CreateAssemblyDataForCompilation(CompilationReference compilationReference)
			{
				CSharpCompilationReference csharpCompilationReference = compilationReference as CSharpCompilationReference;
				if (csharpCompilationReference == null)
				{
					throw new NotSupportedException(string.Format(CSharpResources.CantReferenceCompilationOf, compilationReference.GetType(), "C#"));
				}
				return new CSharpCompilation.ReferenceManager.AssemblyDataForCompilation(csharpCompilationReference.Compilation, csharpCompilationReference.Properties.EmbedInteropTypes);
			}

			// Token: 0x060083CE RID: 33742 RVA: 0x001DDBE0 File Offset: 0x001DBDE0
			protected override bool CheckPropertiesConsistency(MetadataReference primaryReference, MetadataReference duplicateReference, DiagnosticBag diagnostics)
			{
				if (primaryReference.Properties.EmbedInteropTypes != duplicateReference.Properties.EmbedInteropTypes)
				{
					diagnostics.Add(ErrorCode.ERR_AssemblySpecifiedForLinkAndRef, NoLocation.Singleton, new object[] { duplicateReference.Display, primaryReference.Display });
					return false;
				}
				return true;
			}

			// Token: 0x060083CF RID: 33743 RVA: 0x001DDC37 File Offset: 0x001DBE37
			protected override bool WeakIdentityPropertiesEquivalent(AssemblyIdentity identity1, AssemblyIdentity identity2)
			{
				return AssemblyIdentityComparer.CultureComparer.Equals(identity1.CultureName, identity2.CultureName);
			}

			// Token: 0x060083D0 RID: 33744 RVA: 0x001DDC50 File Offset: 0x001DBE50
			protected override void GetActualBoundReferencesUsedBy(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] List<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> referencedAssemblySymbols)
			{
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol moduleSymbol in assemblySymbol.Modules)
				{
					referencedAssemblySymbols.AddRange(moduleSymbol.GetReferencedAssemblySymbols());
				}
				for (int i = 0; i < referencedAssemblySymbols.Count; i++)
				{
					if (referencedAssemblySymbols[i].IsMissing)
					{
						referencedAssemblySymbols[i] = null;
					}
				}
			}

			// Token: 0x060083D1 RID: 33745 RVA: 0x001DDCB7 File Offset: 0x001DBEB7
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			protected override ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> GetNoPiaResolutionAssemblies(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol candidateAssembly)
			{
				if (candidateAssembly is Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol)
				{
					return ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Empty;
				}
				return candidateAssembly.GetNoPiaResolutionAssemblies();
			}

			// Token: 0x060083D2 RID: 33746 RVA: 0x001DDCCD File Offset: 0x001DBECD
			protected override bool IsLinked(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol candidateAssembly)
			{
				return candidateAssembly.IsLinked;
			}

			// Token: 0x060083D3 RID: 33747 RVA: 0x001DDCD8 File Offset: 0x001DBED8
			[return: global::System.Runtime.CompilerServices.Nullable(2)]
			protected override Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol GetCorLibrary(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol candidateAssembly)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol corLibrary = candidateAssembly.CorLibrary;
				if (!corLibrary.IsMissing)
				{
					return corLibrary;
				}
				return null;
			}

			// Token: 0x060083D4 RID: 33748 RVA: 0x001DDCF8 File Offset: 0x001DBEF8
			public void CreateSourceAssemblyForCompilation(CSharpCompilation compilation)
			{
				if (base.IsBound || !this.CreateAndSetSourceAssemblyFullBind(compilation))
				{
					if (!base.HasCircularReference)
					{
						this.CreateAndSetSourceAssemblyReuseData(compilation);
						return;
					}
					new CSharpCompilation.ReferenceManager(this.SimpleAssemblyName, this.IdentityComparer, this.ObservedMetadata).CreateAndSetSourceAssemblyFullBind(compilation);
				}
			}

			// Token: 0x060083D5 RID: 33749 RVA: 0x001DDD44 File Offset: 0x001DBF44
			public PEAssemblySymbol CreatePEAssemblyForAssemblyMetadata(AssemblyMetadata metadata, MetadataImportOptions importOptions, out ImmutableDictionary<AssemblyIdentity, AssemblyIdentity> assemblyReferenceIdentityMap)
			{
				AssemblyIdentityMap<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> assemblyIdentityMap = new AssemblyIdentityMap<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>();
				foreach (Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol in base.ReferencedAssemblies)
				{
					assemblyIdentityMap.Add(assemblySymbol.Identity, assemblySymbol);
				}
				PEAssembly assembly = metadata.GetAssembly();
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> immutableArray = assembly.AssemblyReferences.SelectAsArray(new Func<AssemblyIdentity, AssemblyIdentityMap<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(CSharpCompilation.ReferenceManager.MapAssemblyIdentityToResolvedSymbol), assemblyIdentityMap);
				assemblyReferenceIdentityMap = CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.GetAssemblyReferenceIdentityBaselineMap(immutableArray, assembly.AssemblyReferences);
				PEAssemblySymbol peassemblySymbol = new PEAssemblySymbol(assembly, DocumentationProvider.Default, false, importOptions);
				ImmutableArray<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> immutableArray2 = base.UnifiedAssemblies.WhereAsArray((UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> unified, AssemblyIdentityMap<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> referencedAssembliesByIdentity) => referencedAssembliesByIdentity.Contains(unified.OriginalReference, false), assemblyIdentityMap);
				this.InitializeAssemblyReuseData(peassemblySymbol, immutableArray, immutableArray2);
				if (assembly.ContainsNoPiaLocalTypes())
				{
					peassemblySymbol.SetNoPiaResolutionAssemblies(base.ReferencedAssemblies);
				}
				return peassemblySymbol;
			}

			// Token: 0x060083D6 RID: 33750 RVA: 0x001DDE14 File Offset: 0x001DC014
			private static Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol MapAssemblyIdentityToResolvedSymbol(AssemblyIdentity identity, AssemblyIdentityMap<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> map)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol;
				if (map.TryGetValue(identity, out assemblySymbol, new Func<Version, Version, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol, bool>(CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.CompareVersionPartsSpecifiedInSource)))
				{
					return assemblySymbol;
				}
				if (map.TryGetValue(identity, out assemblySymbol, (Version v1, Version v2, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol s) => true))
				{
					throw new NotSupportedException(string.Format(CodeAnalysisResources.ChangingVersionOfAssemblyReferenceIsNotAllowedDuringDebugging, identity, assemblySymbol.Identity.Version));
				}
				return new MissingAssemblySymbol(identity);
			}

			// Token: 0x060083D7 RID: 33751 RVA: 0x001DDE88 File Offset: 0x001DC088
			private void CreateAndSetSourceAssemblyReuseData(CSharpCompilation compilation)
			{
				string text = compilation.MakeSourceModuleName();
				Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssemblySymbol = new Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol(compilation, this.SimpleAssemblyName, text, base.ReferencedModules);
				this.InitializeAssemblyReuseData(sourceAssemblySymbol, base.ReferencedAssemblies, base.UnifiedAssemblies);
				if (compilation._lazyAssemblySymbol == null)
				{
					object symbolCacheAndReferenceManagerStateGuard = CommonReferenceManager.SymbolCacheAndReferenceManagerStateGuard;
					lock (symbolCacheAndReferenceManagerStateGuard)
					{
						if (compilation._lazyAssemblySymbol == null)
						{
							compilation._lazyAssemblySymbol = sourceAssemblySymbol;
						}
					}
				}
			}

			// Token: 0x060083D8 RID: 33752 RVA: 0x001DDF08 File Offset: 0x001DC108
			private void InitializeAssemblyReuseData(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> referencedAssemblies, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> unifiedAssemblies)
			{
				assemblySymbol.SetCorLibrary(base.CorLibraryOpt ?? assemblySymbol);
				ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> moduleReferences = new ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(referencedAssemblies.SelectAsArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol, AssemblyIdentity>((Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol a) => a.Identity), referencedAssemblies, unifiedAssemblies);
				assemblySymbol.Modules[0].SetReferences(moduleReferences, null);
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules = assemblySymbol.Modules;
				ImmutableArray<ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> referencedModulesReferences = base.ReferencedModulesReferences;
				for (int i = 1; i < modules.Length; i++)
				{
					modules[i].SetReferences(referencedModulesReferences[i - 1], null);
				}
			}

			// Token: 0x060083D9 RID: 33753 RVA: 0x001DDFA8 File Offset: 0x001DC1A8
			private bool CreateAndSetSourceAssemblyFullBind(CSharpCompilation compilation)
			{
				DiagnosticBag instance = DiagnosticBag.GetInstance();
				PooledDictionary<string, List<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.ReferencedAssemblyIdentity>> instance2 = PooledDictionary<string, List<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.ReferencedAssemblyIdentity>>.GetInstance();
				bool referencesSupersedeLowerVersions = compilation.Options.ReferencesSupersedeLowerVersions;
				bool flag3;
				try
				{
					ImmutableArray<MetadataReference> immutableArray2;
					IDictionary<ValueTuple<string, string>, MetadataReference> dictionary;
					ImmutableArray<MetadataReference> immutableArray3;
					ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData> immutableArray4;
					ImmutableArray<PEModule> immutableArray5;
					ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.ResolvedReference> immutableArray = base.ResolveMetadataReferences(compilation, instance2, out immutableArray2, out dictionary, out immutableArray3, out immutableArray4, out immutableArray5, instance);
					CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyDataForAssemblyBeingBuilt assemblyDataForAssemblyBeingBuilt = new CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyDataForAssemblyBeingBuilt(new AssemblyIdentity(true, this.SimpleAssemblyName, null, null, default(ImmutableArray<byte>), false, false, 0), immutableArray4, immutableArray5);
					ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData> immutableArray6 = immutableArray4.Insert(0, assemblyDataForAssemblyBeingBuilt);
					CSharpScriptCompilationInfo scriptCompilationInfo = compilation.ScriptCompilationInfo;
					ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> immutableDictionary;
					if (scriptCompilationInfo == null)
					{
						immutableDictionary = null;
					}
					else
					{
						CSharpCompilation previousScriptCompilation = scriptCompilationInfo.PreviousScriptCompilation;
						immutableDictionary = ((previousScriptCompilation != null) ? previousScriptCompilation.GetBoundReferenceManager().ImplicitReferenceResolutions : null);
					}
					ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> immutableDictionary2 = immutableDictionary ?? ImmutableDictionary<AssemblyIdentity, PortableExecutableReference>.Empty;
					ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData> immutableArray7;
					ImmutableArray<MetadataReference> immutableArray8;
					ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.ResolvedReference> immutableArray9;
					bool flag;
					int num;
					CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] array = base.Bind(compilation, immutableArray6, immutableArray5, immutableArray2, immutableArray, compilation.Options.MetadataReferenceResolver, compilation.Options.MetadataImportOptions, referencesSupersedeLowerVersions, instance2, out immutableArray7, out immutableArray8, out immutableArray9, ref immutableDictionary2, instance, out flag, out num);
					ImmutableArray<MetadataReference> immutableArray10 = immutableArray2.AddRange(immutableArray8);
					immutableArray = immutableArray.AddRange(immutableArray9);
					Dictionary<MetadataReference, int> dictionary2;
					Dictionary<MetadataReference, int> dictionary3;
					ImmutableArray<ImmutableArray<string>> immutableArray11;
					Dictionary<MetadataReference, ImmutableArray<MetadataReference>> dictionary4;
					CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BuildReferencedAssembliesAndModulesMaps(array, immutableArray10, immutableArray, immutableArray5.Length, immutableArray4.Length, instance2, referencesSupersedeLowerVersions, out dictionary2, out dictionary3, out immutableArray11, out dictionary4);
					List<int> list = new List<int>();
					for (int i = 1; i < array.Length; i++)
					{
						ref CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly ptr = ref array[i];
						if (ptr.AssemblySymbol == null)
						{
							ptr.AssemblySymbol = ((CSharpCompilation.ReferenceManager.AssemblyDataForMetadataOrCompilation)immutableArray7[i]).CreateAssemblySymbol();
							list.Add(i);
						}
					}
					Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssemblySymbol = new Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol(compilation, this.SimpleAssemblyName, compilation.MakeSourceModuleName(), immutableArray5);
					Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol;
					if (num == 0)
					{
						assemblySymbol = sourceAssemblySymbol;
					}
					else if (num > 0)
					{
						assemblySymbol = array[num].AssemblySymbol;
					}
					else
					{
						assemblySymbol = MissingCorLibrarySymbol.Instance;
					}
					sourceAssemblySymbol.SetCorLibrary(assemblySymbol);
					Dictionary<AssemblyIdentity, MissingAssemblySymbol> dictionary5 = null;
					int num2 = immutableArray7.Length - 1;
					ImmutableArray<ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> immutableArray12;
					CSharpCompilation.ReferenceManager.SetupReferencesForSourceAssembly(sourceAssemblySymbol, immutableArray5, num2, array, ref dictionary5, out immutableArray12);
					if (list.Count > 0)
					{
						if (flag)
						{
							array[0].AssemblySymbol = sourceAssemblySymbol;
						}
						CSharpCompilation.ReferenceManager.InitializeNewSymbols(list, sourceAssemblySymbol, immutableArray7, array, dictionary5);
					}
					if (compilation._lazyAssemblySymbol == null)
					{
						object symbolCacheAndReferenceManagerStateGuard = CommonReferenceManager.SymbolCacheAndReferenceManagerStateGuard;
						lock (symbolCacheAndReferenceManagerStateGuard)
						{
							if (compilation._lazyAssemblySymbol == null)
							{
								if (base.IsBound)
								{
									return false;
								}
								CSharpCompilation.ReferenceManager.UpdateSymbolCacheNoLock(list, immutableArray7, array);
								base.InitializeNoLock(dictionary2, dictionary3, dictionary, immutableArray3, immutableArray2, immutableDictionary2, flag, instance.ToReadOnly(), (assemblySymbol == sourceAssemblySymbol) ? null : assemblySymbol, immutableArray5, immutableArray12, sourceAssemblySymbol.SourceModule.GetReferencedAssemblySymbols(), immutableArray11, sourceAssemblySymbol.SourceModule.GetUnifiedAssemblies(), dictionary4);
								compilation._referenceManager = this;
								compilation._lazyAssemblySymbol = sourceAssemblySymbol;
							}
						}
					}
					flag3 = true;
				}
				finally
				{
					instance.Free();
					instance2.Free();
				}
				return flag3;
			}

			// Token: 0x060083DA RID: 33754 RVA: 0x001DE27C File Offset: 0x001DC47C
			private static void InitializeNewSymbols(List<int> newSymbols, Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssembly, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1 })] ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData> assemblies, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] bindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] Dictionary<AssemblyIdentity, MissingAssemblySymbol> missingAssemblies)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol corLibrary = sourceAssembly.CorLibrary;
				foreach (int num in newSymbols)
				{
					if (assemblies[num] is CSharpCompilation.ReferenceManager.AssemblyDataForCompilation)
					{
						CSharpCompilation.ReferenceManager.SetupReferencesForRetargetingAssembly(bindingResult, ref bindingResult[num], ref missingAssemblies, sourceAssembly);
					}
					else
					{
						CSharpCompilation.ReferenceManager.SetupReferencesForFileAssembly((CSharpCompilation.ReferenceManager.AssemblyDataForFile)assemblies[num], bindingResult, ref bindingResult[num], ref missingAssemblies, sourceAssembly);
					}
				}
				ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> instance = ArrayBuilder<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.GetInstance();
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> referencedAssemblySymbols = sourceAssembly.Modules[0].GetReferencedAssemblySymbols();
				foreach (int num2 in newSymbols)
				{
					ref CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly ptr = ref bindingResult[num2];
					if (assemblies[num2].ContainsNoPiaLocalTypes)
					{
						ptr.AssemblySymbol.SetNoPiaResolutionAssemblies(referencedAssemblySymbols);
					}
					instance.Clear();
					if (assemblies[num2].IsLinked)
					{
						instance.Add(ptr.AssemblySymbol);
					}
					foreach (CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding in ptr.ReferenceBinding)
					{
						if (assemblyReferenceBinding.IsBound && assemblies[assemblyReferenceBinding.DefinitionIndex].IsLinked)
						{
							Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol = bindingResult[assemblyReferenceBinding.DefinitionIndex].AssemblySymbol;
							instance.Add(assemblySymbol);
						}
					}
					if (instance.Count > 0)
					{
						instance.RemoveDuplicates();
						ptr.AssemblySymbol.SetLinkedReferencedAssemblies(instance.ToImmutable());
					}
					ptr.AssemblySymbol.SetCorLibrary(corLibrary);
				}
				instance.Free();
				if (missingAssemblies != null)
				{
					foreach (MissingAssemblySymbol missingAssemblySymbol in missingAssemblies.Values)
					{
						missingAssemblySymbol.SetCorLibrary(corLibrary);
					}
				}
			}

			// Token: 0x060083DB RID: 33755 RVA: 0x001DE48C File Offset: 0x001DC68C
			private static void UpdateSymbolCacheNoLock(List<int> newSymbols, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1 })] ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData> assemblies, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] bindingResult)
			{
				foreach (int num in newSymbols)
				{
					ref CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly ptr = ref bindingResult[num];
					CSharpCompilation.ReferenceManager.AssemblyDataForCompilation assemblyDataForCompilation = assemblies[num] as CSharpCompilation.ReferenceManager.AssemblyDataForCompilation;
					if (assemblyDataForCompilation != null)
					{
						assemblyDataForCompilation.Compilation.CacheRetargetingAssemblySymbolNoLock(ptr.AssemblySymbol);
					}
					else
					{
						((CSharpCompilation.ReferenceManager.AssemblyDataForFile)assemblies[num]).CachedSymbols.Add((PEAssemblySymbol)ptr.AssemblySymbol);
					}
				}
			}

			// Token: 0x060083DC RID: 33756 RVA: 0x001DE524 File Offset: 0x001DC724
			private static void SetupReferencesForRetargetingAssembly([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] bindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly currentBindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref Dictionary<AssemblyIdentity, MissingAssemblySymbol> missingAssemblies, Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssemblyDebugOnly)
			{
				RetargetingAssemblySymbol retargetingAssemblySymbol = (RetargetingAssemblySymbol)currentBindingResult.AssemblySymbol;
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules = retargetingAssemblySymbol.Modules;
				int length = modules.Length;
				int num = 0;
				for (int i = 0; i < length; i++)
				{
					ImmutableArray<AssemblyIdentity> immutableArray = retargetingAssemblySymbol.UnderlyingAssembly.Modules[i].GetReferencedAssemblies();
					if (i == 0)
					{
						ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> referencedAssemblySymbols = retargetingAssemblySymbol.UnderlyingAssembly.Modules[0].GetReferencedAssemblySymbols();
						int num2 = 0;
						ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.Enumerator enumerator = referencedAssemblySymbols.GetEnumerator();
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.IsLinked)
							{
								num2++;
							}
						}
						if (num2 > 0)
						{
							AssemblyIdentity[] array = new AssemblyIdentity[immutableArray.Length - num2];
							int num3 = 0;
							for (int j = 0; j < referencedAssemblySymbols.Length; j++)
							{
								if (!referencedAssemblySymbols[j].IsLinked)
								{
									array[num3] = immutableArray[j];
									num3++;
								}
							}
							immutableArray = array.AsImmutableOrNull<AssemblyIdentity>();
						}
					}
					int length2 = immutableArray.Length;
					Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol[] array2 = new Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol[length2];
					ArrayBuilder<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> arrayBuilder = null;
					for (int k = 0; k < length2; k++)
					{
						CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding = currentBindingResult.ReferenceBinding[num + k];
						if (assemblyReferenceBinding.IsBound)
						{
							array2[k] = CSharpCompilation.ReferenceManager.GetAssemblyDefinitionSymbol(bindingResult, assemblyReferenceBinding, ref arrayBuilder);
						}
						else
						{
							array2[k] = CSharpCompilation.ReferenceManager.GetOrAddMissingAssemblySymbol(immutableArray[k], ref missingAssemblies);
						}
					}
					ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> moduleReferences = new ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(immutableArray, array2.AsImmutableOrNull<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(), arrayBuilder.AsImmutableOrEmpty<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>());
					modules[i].SetReferences(moduleReferences, sourceAssemblyDebugOnly);
					num += length2;
				}
			}

			// Token: 0x060083DD RID: 33757 RVA: 0x001DE6C0 File Offset: 0x001DC8C0
			private static void SetupReferencesForFileAssembly(CSharpCompilation.ReferenceManager.AssemblyDataForFile fileData, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] bindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] ref CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly currentBindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref Dictionary<AssemblyIdentity, MissingAssemblySymbol> missingAssemblies, Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssemblyDebugOnly)
			{
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules = ((PEAssemblySymbol)currentBindingResult.AssemblySymbol).Modules;
				int length = modules.Length;
				int num = 0;
				for (int i = 0; i < length; i++)
				{
					int num2 = fileData.Assembly.ModuleReferenceCounts[i];
					AssemblyIdentity[] array = new AssemblyIdentity[num2];
					Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol[] array2 = new Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol[num2];
					fileData.AssemblyReferences.CopyTo(num, array, 0, num2);
					ArrayBuilder<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> arrayBuilder = null;
					for (int j = 0; j < num2; j++)
					{
						CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding = currentBindingResult.ReferenceBinding[num + j];
						if (assemblyReferenceBinding.IsBound)
						{
							array2[j] = CSharpCompilation.ReferenceManager.GetAssemblyDefinitionSymbol(bindingResult, assemblyReferenceBinding, ref arrayBuilder);
						}
						else
						{
							array2[j] = CSharpCompilation.ReferenceManager.GetOrAddMissingAssemblySymbol(array[j], ref missingAssemblies);
						}
					}
					ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> moduleReferences = new ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(array.AsImmutableOrNull<AssemblyIdentity>(), array2.AsImmutableOrNull<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(), arrayBuilder.AsImmutableOrEmpty<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>());
					modules[i].SetReferences(moduleReferences, sourceAssemblyDebugOnly);
					num += num2;
				}
			}

			// Token: 0x060083DE RID: 33758 RVA: 0x001DE7BC File Offset: 0x001DC9BC
			private static void SetupReferencesForSourceAssembly(Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol sourceAssembly, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> modules, int totalReferencedAssemblyCount, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] bindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref Dictionary<AssemblyIdentity, MissingAssemblySymbol> missingAssemblies, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] out ImmutableArray<ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> moduleReferences)
			{
				ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules2 = sourceAssembly.Modules;
				ArrayBuilder<ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> arrayBuilder = ((modules2.Length > 1) ? ArrayBuilder<ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>.GetInstance() : null);
				int num = 0;
				for (int i = 0; i < modules2.Length; i++)
				{
					int num2 = ((i == 0) ? totalReferencedAssemblyCount : modules[i - 1].ReferencedAssemblies.Length);
					AssemblyIdentity[] array = new AssemblyIdentity[num2];
					Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol[] array2 = new Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol[num2];
					ArrayBuilder<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> arrayBuilder2 = null;
					for (int j = 0; j < num2; j++)
					{
						CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding = bindingResult[0].ReferenceBinding[num + j];
						if (assemblyReferenceBinding.IsBound)
						{
							array2[j] = CSharpCompilation.ReferenceManager.GetAssemblyDefinitionSymbol(bindingResult, assemblyReferenceBinding, ref arrayBuilder2);
						}
						else
						{
							array2[j] = CSharpCompilation.ReferenceManager.GetOrAddMissingAssemblySymbol(assemblyReferenceBinding.ReferenceIdentity, ref missingAssemblies);
						}
						array[j] = assemblyReferenceBinding.ReferenceIdentity;
					}
					ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> moduleReferences2 = new ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(array.AsImmutableOrNull<AssemblyIdentity>(), array2.AsImmutableOrNull<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(), arrayBuilder2.AsImmutableOrEmpty<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>());
					if (i > 0)
					{
						arrayBuilder.Add(moduleReferences2);
					}
					modules2[i].SetReferences(moduleReferences2, sourceAssembly);
					num += num2;
				}
				moduleReferences = arrayBuilder.ToImmutableOrEmptyAndFree<ModuleReferences<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>();
			}

			// Token: 0x060083DF RID: 33759 RVA: 0x001DE8E0 File Offset: 0x001DCAE0
			private static Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol GetAssemblyDefinitionSymbol([global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.BoundInputAssembly[] bindingResult, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })] CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyReferenceBinding referenceBinding, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 0, 1 })] ref ArrayBuilder<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>> unifiedAssemblies)
			{
				Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol = bindingResult[referenceBinding.DefinitionIndex].AssemblySymbol;
				if (referenceBinding.VersionDifference != 0)
				{
					if (unifiedAssemblies == null)
					{
						unifiedAssemblies = new ArrayBuilder<UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>>();
					}
					unifiedAssemblies.Add(new UnifiedAssembly<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(assemblySymbol, referenceBinding.ReferenceIdentity));
				}
				return assemblySymbol;
			}

			// Token: 0x060083E0 RID: 33760 RVA: 0x001DE92C File Offset: 0x001DCB2C
			private static MissingAssemblySymbol GetOrAddMissingAssemblySymbol(AssemblyIdentity assemblyIdentity, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })] ref Dictionary<AssemblyIdentity, MissingAssemblySymbol> missingAssemblies)
			{
				MissingAssemblySymbol missingAssemblySymbol;
				if (missingAssemblies == null)
				{
					missingAssemblies = new Dictionary<AssemblyIdentity, MissingAssemblySymbol>();
				}
				else if (missingAssemblies.TryGetValue(assemblyIdentity, ref missingAssemblySymbol))
				{
					return missingAssemblySymbol;
				}
				missingAssemblySymbol = new MissingAssemblySymbol(assemblyIdentity);
				missingAssemblies.Add(assemblyIdentity, missingAssemblySymbol);
				return missingAssemblySymbol;
			}

			// Token: 0x060083E1 RID: 33761 RVA: 0x001DE965 File Offset: 0x001DCB65
			internal static bool IsSourceAssemblySymbolCreated(CSharpCompilation compilation)
			{
				return compilation._lazyAssemblySymbol != null;
			}

			// Token: 0x060083E2 RID: 33762 RVA: 0x001DE970 File Offset: 0x001DCB70
			internal static bool IsReferenceManagerInitialized(CSharpCompilation compilation)
			{
				return compilation._referenceManager.IsBound;
			}

			// Token: 0x020009C7 RID: 2503
			[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1 })]
			private abstract class AssemblyDataForMetadataOrCompilation : CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData
			{
				// Token: 0x06009304 RID: 37636 RVA: 0x002019ED File Offset: 0x001FFBED
				protected AssemblyDataForMetadataOrCompilation(AssemblyIdentity identity, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<AssemblyIdentity> referencedAssemblies, bool embedInteropTypes)
				{
					this._embedInteropTypes = embedInteropTypes;
					this._identity = identity;
					this._referencedAssemblies = referencedAssemblies;
				}

				// Token: 0x06009305 RID: 37637
				internal abstract Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol CreateAssemblySymbol();

				// Token: 0x1700292F RID: 10543
				// (get) Token: 0x06009306 RID: 37638 RVA: 0x00201A0A File Offset: 0x001FFC0A
				public override AssemblyIdentity Identity
				{
					get
					{
						return this._identity;
					}
				}

				// Token: 0x17002930 RID: 10544
				// (get) Token: 0x06009307 RID: 37639 RVA: 0x00201A12 File Offset: 0x001FFC12
				public override IEnumerable<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> AvailableSymbols
				{
					get
					{
						if (this._assemblies == null)
						{
							this._assemblies = new List<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>();
							this.AddAvailableSymbols(this._assemblies);
						}
						return this._assemblies;
					}
				}

				// Token: 0x06009308 RID: 37640
				protected abstract void AddAvailableSymbols(List<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> assemblies);

				// Token: 0x17002931 RID: 10545
				// (get) Token: 0x06009309 RID: 37641 RVA: 0x00201A39 File Offset: 0x001FFC39
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				public override ImmutableArray<AssemblyIdentity> AssemblyReferences
				{
					[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
					get
					{
						return this._referencedAssemblies;
					}
				}

				// Token: 0x0600930A RID: 37642 RVA: 0x00201A41 File Offset: 0x001FFC41
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 1, 0, 1, 1 })]
				public override CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyReferenceBinding[] BindAssemblyReferences([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1, 1, 1 })] ImmutableArray<CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.AssemblyData> assemblies, AssemblyIdentityComparer assemblyIdentityComparer)
				{
					return CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.ResolveReferencedAssemblies(this._referencedAssemblies, assemblies, 0, assemblyIdentityComparer);
				}

				// Token: 0x17002932 RID: 10546
				// (get) Token: 0x0600930B RID: 37643 RVA: 0x00201A51 File Offset: 0x001FFC51
				public sealed override bool IsLinked
				{
					get
					{
						return this._embedInteropTypes;
					}
				}

				// Token: 0x040029B1 RID: 10673
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })]
				private List<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> _assemblies;

				// Token: 0x040029B2 RID: 10674
				private readonly AssemblyIdentity _identity;

				// Token: 0x040029B3 RID: 10675
				[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				private readonly ImmutableArray<AssemblyIdentity> _referencedAssemblies;

				// Token: 0x040029B4 RID: 10676
				private readonly bool _embedInteropTypes;
			}

			// Token: 0x020009C8 RID: 2504
			[global::System.Runtime.CompilerServices.Nullable(0)]
			private sealed class AssemblyDataForFile : CSharpCompilation.ReferenceManager.AssemblyDataForMetadataOrCompilation
			{
				// Token: 0x0600930C RID: 37644 RVA: 0x00201A59 File Offset: 0x001FFC59
				public AssemblyDataForFile(PEAssembly assembly, WeakList<IAssemblySymbolInternal> cachedSymbols, bool embedInteropTypes, DocumentationProvider documentationProvider, string sourceAssemblySimpleName, MetadataImportOptions compilationImportOptions)
					: base(assembly.Identity, assembly.AssemblyReferences, embedInteropTypes)
				{
					this.CachedSymbols = cachedSymbols;
					this.Assembly = assembly;
					this.DocumentationProvider = documentationProvider;
					this._compilationImportOptions = compilationImportOptions;
					this._sourceAssemblySimpleName = sourceAssemblySimpleName;
				}

				// Token: 0x0600930D RID: 37645 RVA: 0x00201A94 File Offset: 0x001FFC94
				internal override Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol CreateAssemblySymbol()
				{
					return new PEAssemblySymbol(this.Assembly, this.DocumentationProvider, this.IsLinked, this.EffectiveImportOptions);
				}

				// Token: 0x17002933 RID: 10547
				// (get) Token: 0x0600930E RID: 37646 RVA: 0x00201AB3 File Offset: 0x001FFCB3
				internal bool InternalsMayBeVisibleToCompilation
				{
					get
					{
						if (!this._internalsVisibleComputed)
						{
							this._internalsPotentiallyVisibleToCompilation = CommonReferenceManager<CSharpCompilation, Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>.InternalsMayBeVisibleToAssemblyBeingCompiled(this._sourceAssemblySimpleName, this.Assembly);
							this._internalsVisibleComputed = true;
						}
						return this._internalsPotentiallyVisibleToCompilation;
					}
				}

				// Token: 0x17002934 RID: 10548
				// (get) Token: 0x0600930F RID: 37647 RVA: 0x00201AE1 File Offset: 0x001FFCE1
				internal MetadataImportOptions EffectiveImportOptions
				{
					get
					{
						if (this.InternalsMayBeVisibleToCompilation && this._compilationImportOptions == MetadataImportOptions.Public)
						{
							return MetadataImportOptions.Internal;
						}
						return this._compilationImportOptions;
					}
				}

				// Token: 0x06009310 RID: 37648 RVA: 0x00201AFC File Offset: 0x001FFCFC
				protected override void AddAvailableSymbols(List<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> assemblies)
				{
					object symbolCacheAndReferenceManagerStateGuard = CommonReferenceManager.SymbolCacheAndReferenceManagerStateGuard;
					lock (symbolCacheAndReferenceManagerStateGuard)
					{
						foreach (IAssemblySymbolInternal assemblySymbolInternal in this.CachedSymbols)
						{
							PEAssemblySymbol peassemblySymbol = assemblySymbolInternal as PEAssemblySymbol;
							if (this.IsMatchingAssembly(peassemblySymbol))
							{
								assemblies.Add(peassemblySymbol);
							}
						}
					}
				}

				// Token: 0x06009311 RID: 37649 RVA: 0x00201B80 File Offset: 0x001FFD80
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				public override bool IsMatchingAssembly(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol candidateAssembly)
				{
					return this.IsMatchingAssembly(candidateAssembly as PEAssemblySymbol);
				}

				// Token: 0x06009312 RID: 37650 RVA: 0x00201B90 File Offset: 0x001FFD90
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				private bool IsMatchingAssembly(PEAssemblySymbol peAssembly)
				{
					return peAssembly != null && peAssembly.Assembly == this.Assembly && this.EffectiveImportOptions == peAssembly.PrimaryModule.ImportOptions && peAssembly.DocumentationProvider.Equals(this.DocumentationProvider);
				}

				// Token: 0x17002935 RID: 10549
				// (get) Token: 0x06009313 RID: 37651 RVA: 0x00201BDD File Offset: 0x001FFDDD
				public override bool ContainsNoPiaLocalTypes
				{
					get
					{
						return this.Assembly.ContainsNoPiaLocalTypes();
					}
				}

				// Token: 0x17002936 RID: 10550
				// (get) Token: 0x06009314 RID: 37652 RVA: 0x00201BEA File Offset: 0x001FFDEA
				public override bool DeclaresTheObjectClass
				{
					get
					{
						return this.Assembly.DeclaresTheObjectClass;
					}
				}

				// Token: 0x17002937 RID: 10551
				// (get) Token: 0x06009315 RID: 37653 RVA: 0x00201BF7 File Offset: 0x001FFDF7
				[global::System.Runtime.CompilerServices.Nullable(2)]
				public override Compilation SourceCompilation
				{
					[global::System.Runtime.CompilerServices.NullableContext(2)]
					get
					{
						return null;
					}
				}

				// Token: 0x040029B5 RID: 10677
				public readonly PEAssembly Assembly;

				// Token: 0x040029B6 RID: 10678
				public readonly WeakList<IAssemblySymbolInternal> CachedSymbols;

				// Token: 0x040029B7 RID: 10679
				public readonly DocumentationProvider DocumentationProvider;

				// Token: 0x040029B8 RID: 10680
				private readonly MetadataImportOptions _compilationImportOptions;

				// Token: 0x040029B9 RID: 10681
				private readonly string _sourceAssemblySimpleName;

				// Token: 0x040029BA RID: 10682
				private bool _internalsVisibleComputed;

				// Token: 0x040029BB RID: 10683
				private bool _internalsPotentiallyVisibleToCompilation;
			}

			// Token: 0x020009C9 RID: 2505
			[global::System.Runtime.CompilerServices.Nullable(0)]
			private sealed class AssemblyDataForCompilation : CSharpCompilation.ReferenceManager.AssemblyDataForMetadataOrCompilation
			{
				// Token: 0x06009316 RID: 37654 RVA: 0x00201BFA File Offset: 0x001FFDFA
				public AssemblyDataForCompilation(CSharpCompilation compilation, bool embedInteropTypes)
					: base(compilation.Assembly.Identity, CSharpCompilation.ReferenceManager.AssemblyDataForCompilation.GetReferencedAssemblies(compilation), embedInteropTypes)
				{
					this.Compilation = compilation;
				}

				// Token: 0x06009317 RID: 37655 RVA: 0x00201C1C File Offset: 0x001FFE1C
				[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
				private static ImmutableArray<AssemblyIdentity> GetReferencedAssemblies(CSharpCompilation compilation)
				{
					ArrayBuilder<AssemblyIdentity> instance = ArrayBuilder<AssemblyIdentity>.GetInstance();
					ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.ModuleSymbol> modules = compilation.Assembly.Modules;
					ImmutableArray<AssemblyIdentity> referencedAssemblies = modules[0].GetReferencedAssemblies();
					ImmutableArray<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> referencedAssemblySymbols = modules[0].GetReferencedAssemblySymbols();
					for (int i = 0; i < referencedAssemblies.Length; i++)
					{
						if (!referencedAssemblySymbols[i].IsLinked)
						{
							instance.Add(referencedAssemblies[i]);
						}
					}
					for (int j = 1; j < modules.Length; j++)
					{
						instance.AddRange(modules[j].GetReferencedAssemblies());
					}
					return instance.ToImmutableAndFree();
				}

				// Token: 0x06009318 RID: 37656 RVA: 0x00201CBC File Offset: 0x001FFEBC
				internal override Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol CreateAssemblySymbol()
				{
					return new RetargetingAssemblySymbol(this.Compilation.SourceAssembly, this.IsLinked);
				}

				// Token: 0x06009319 RID: 37657 RVA: 0x00201CD4 File Offset: 0x001FFED4
				protected override void AddAvailableSymbols(List<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol> assemblies)
				{
					assemblies.Add(this.Compilation.Assembly);
					object symbolCacheAndReferenceManagerStateGuard = CommonReferenceManager.SymbolCacheAndReferenceManagerStateGuard;
					lock (symbolCacheAndReferenceManagerStateGuard)
					{
						this.Compilation.AddRetargetingAssemblySymbolsNoLock<Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol>(assemblies);
					}
				}

				// Token: 0x0600931A RID: 37658 RVA: 0x00201D2C File Offset: 0x001FFF2C
				[global::System.Runtime.CompilerServices.NullableContext(2)]
				public override bool IsMatchingAssembly(Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol candidateAssembly)
				{
					RetargetingAssemblySymbol retargetingAssemblySymbol = candidateAssembly as RetargetingAssemblySymbol;
					Microsoft.CodeAnalysis.CSharp.Symbols.AssemblySymbol assemblySymbol;
					if (retargetingAssemblySymbol != null)
					{
						assemblySymbol = retargetingAssemblySymbol.UnderlyingAssembly;
					}
					else
					{
						assemblySymbol = candidateAssembly as Microsoft.CodeAnalysis.CSharp.Symbols.SourceAssemblySymbol;
					}
					return assemblySymbol == this.Compilation.Assembly;
				}

				// Token: 0x17002938 RID: 10552
				// (get) Token: 0x0600931B RID: 37659 RVA: 0x00201D61 File Offset: 0x001FFF61
				public override bool ContainsNoPiaLocalTypes
				{
					get
					{
						return this.Compilation.MightContainNoPiaLocalTypes();
					}
				}

				// Token: 0x17002939 RID: 10553
				// (get) Token: 0x0600931C RID: 37660 RVA: 0x00201D6E File Offset: 0x001FFF6E
				public override bool DeclaresTheObjectClass
				{
					get
					{
						return this.Compilation.DeclaresTheObjectClass;
					}
				}

				// Token: 0x1700293A RID: 10554
				// (get) Token: 0x0600931D RID: 37661 RVA: 0x00201D7B File Offset: 0x001FFF7B
				public override Compilation SourceCompilation
				{
					get
					{
						return this.Compilation;
					}
				}

				// Token: 0x040029BC RID: 10684
				public readonly CSharpCompilation Compilation;
			}
		}
	}
}
