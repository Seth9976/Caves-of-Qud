using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.DiaSymReader;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000138 RID: 312
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Compilation
	{
		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000B7F RID: 2943
		public abstract bool IsCaseSensitive { get; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x00023628 File Offset: 0x00021828
		[Nullable(2)]
		public ScriptCompilationInfo ScriptCompilationInfo
		{
			[NullableContext(2)]
			get
			{
				return this.CommonScriptCompilationInfo;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000B81 RID: 2945
		[Nullable(2)]
		internal abstract ScriptCompilationInfo CommonScriptCompilationInfo
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00023630 File Offset: 0x00021830
		[NullableContext(2)]
		internal Compilation(string name, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> references, [Nullable(1)] IReadOnlyDictionary<string, string> features, bool isSubmission, SemanticModelProvider semanticModelProvider, [Nullable(new byte[] { 2, 1 })] AsyncQueue<CompilationEvent> eventQueue)
		{
			this.AssemblyName = name;
			this.ExternalReferences = references;
			this.SemanticModelProvider = semanticModelProvider;
			this.EventQueue = eventQueue;
			this._lazySubmissionSlotIndex = (isSubmission ? (-2) : (-3));
			this._features = features;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00023698 File Offset: 0x00021898
		protected static IReadOnlyDictionary<string, string> SyntaxTreeCommonFeatures(IEnumerable<SyntaxTree> trees)
		{
			IReadOnlyDictionary<string, string> readOnlyDictionary = null;
			foreach (SyntaxTree syntaxTree in trees)
			{
				IReadOnlyDictionary<string, string> features = syntaxTree.Options.Features;
				if (readOnlyDictionary == null)
				{
					readOnlyDictionary = features;
				}
				else if (readOnlyDictionary != features && !readOnlyDictionary.SetEquals<KeyValuePair<string, string>>(features))
				{
					throw new ArgumentException(CodeAnalysisResources.InconsistentSyntaxTreeFeature, "trees");
				}
			}
			if (readOnlyDictionary == null)
			{
				readOnlyDictionary = ImmutableDictionary<string, string>.Empty;
			}
			return readOnlyDictionary;
		}

		// Token: 0x06000B84 RID: 2948
		internal abstract AnalyzerDriver CreateAnalyzerDriver([Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, SeverityFilter severityFilter);

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000B85 RID: 2949
		public abstract string Language { get; }

		// Token: 0x06000B86 RID: 2950
		internal abstract void SerializePdbEmbeddedCompilationOptions(BlobBuilder builder);

		// Token: 0x06000B87 RID: 2951 RVA: 0x00023714 File Offset: 0x00021914
		[NullableContext(2)]
		internal static void ValidateScriptCompilationParameters(Compilation previousScriptCompilation, Type returnType, ref Type globalsType)
		{
			if (globalsType != null && !Compilation.IsValidHostObjectType(globalsType))
			{
				throw new ArgumentException(CodeAnalysisResources.ReturnTypeCannotBeValuePointerbyRefOrOpen, "globalsType");
			}
			if (returnType != null && !Compilation.IsValidSubmissionReturnType(returnType))
			{
				throw new ArgumentException(CodeAnalysisResources.ReturnTypeCannotBeVoidByRefOrOpen, "returnType");
			}
			if (previousScriptCompilation != null)
			{
				if (globalsType == null)
				{
					globalsType = previousScriptCompilation.HostObjectType;
				}
				else if (globalsType != previousScriptCompilation.HostObjectType)
				{
					throw new ArgumentException(CodeAnalysisResources.TypeMustBeSameAsHostObjectTypeOfPreviousSubmission, "globalsType");
				}
				if (previousScriptCompilation.GetDiagnostics(default(CancellationToken)).Any<Diagnostic>((Diagnostic d) => d.Severity == DiagnosticSeverity.Error))
				{
					throw new InvalidOperationException(CodeAnalysisResources.PreviousSubmissionHasErrors);
				}
			}
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000237DC File Offset: 0x000219DC
		[NullableContext(2)]
		internal static void CheckSubmissionOptions(CompilationOptions options)
		{
			if (options == null)
			{
				return;
			}
			if (options.OutputKind.IsValid() && options.OutputKind != OutputKind.DynamicallyLinkedLibrary)
			{
				throw new ArgumentException(CodeAnalysisResources.InvalidOutputKindForSubmission, "options");
			}
			if (options.CryptoKeyContainer == null && options.CryptoKeyFile == null && options.DelaySign == null && options.CryptoPublicKey.IsEmpty)
			{
				bool? delaySign = options.DelaySign;
				bool flag = true;
				if (!((delaySign.GetValueOrDefault() == flag) & (delaySign != null)) || !options.PublicSign)
				{
					return;
				}
			}
			throw new ArgumentException(CodeAnalysisResources.InvalidCompilationOptions, "options");
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002387E File Offset: 0x00021A7E
		public Compilation Clone()
		{
			return this.CommonClone();
		}

		// Token: 0x06000B8A RID: 2954
		protected abstract Compilation CommonClone();

		// Token: 0x06000B8B RID: 2955
		internal abstract Compilation WithEventQueue([Nullable(new byte[] { 2, 1 })] AsyncQueue<CompilationEvent> eventQueue);

		// Token: 0x06000B8C RID: 2956
		internal abstract Compilation WithSemanticModelProvider(SemanticModelProvider semanticModelProvider);

		// Token: 0x06000B8D RID: 2957 RVA: 0x00023886 File Offset: 0x00021A86
		public SemanticModel GetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility = false)
		{
			return this.CommonGetSemanticModel(syntaxTree, ignoreAccessibility);
		}

		// Token: 0x06000B8E RID: 2958
		protected abstract SemanticModel CommonGetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility);

		// Token: 0x06000B8F RID: 2959
		internal abstract SemanticModel CreateSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility);

		// Token: 0x06000B90 RID: 2960 RVA: 0x00023890 File Offset: 0x00021A90
		public INamedTypeSymbol CreateErrorTypeSymbol([Nullable(2)] INamespaceOrTypeSymbol container, string name, int arity)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (arity < 0)
			{
				throw new ArgumentException("arity must be >= 0", "arity");
			}
			return this.CommonCreateErrorTypeSymbol(container, name, arity);
		}

		// Token: 0x06000B91 RID: 2961
		protected abstract INamedTypeSymbol CommonCreateErrorTypeSymbol([Nullable(2)] INamespaceOrTypeSymbol container, string name, int arity);

		// Token: 0x06000B92 RID: 2962 RVA: 0x000238BD File Offset: 0x00021ABD
		public INamespaceSymbol CreateErrorNamespaceSymbol(INamespaceSymbol container, string name)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.CommonCreateErrorNamespaceSymbol(container, name);
		}

		// Token: 0x06000B93 RID: 2963
		protected abstract INamespaceSymbol CommonCreateErrorNamespaceSymbol(INamespaceSymbol container, string name);

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000238E3 File Offset: 0x00021AE3
		[Nullable(2)]
		public string AssemblyName
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000238EB File Offset: 0x00021AEB
		internal void CheckAssemblyName(DiagnosticBag diagnostics)
		{
			if (this.AssemblyName != null)
			{
				MetadataHelpers.CheckAssemblyOrModuleName(this.AssemblyName, this.MessageProvider, this.MessageProvider.ERR_BadAssemblyName, diagnostics);
			}
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00023912 File Offset: 0x00021B12
		internal string MakeSourceAssemblySimpleName()
		{
			return this.AssemblyName ?? "?";
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00023923 File Offset: 0x00021B23
		internal string MakeSourceModuleName()
		{
			string text;
			if ((text = this.Options.ModuleName) == null)
			{
				if (this.AssemblyName == null)
				{
					return "?";
				}
				text = this.AssemblyName + this.Options.OutputKind.GetDefaultExtension();
			}
			return text;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002395D File Offset: 0x00021B5D
		public Compilation WithAssemblyName([Nullable(2)] string assemblyName)
		{
			return this.CommonWithAssemblyName(assemblyName);
		}

		// Token: 0x06000B99 RID: 2969
		protected abstract Compilation CommonWithAssemblyName([Nullable(2)] string outputName);

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00023966 File Offset: 0x00021B66
		public CompilationOptions Options
		{
			get
			{
				return this.CommonOptions;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000B9B RID: 2971
		protected abstract CompilationOptions CommonOptions { get; }

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002396E File Offset: 0x00021B6E
		public Compilation WithOptions(CompilationOptions options)
		{
			return this.CommonWithOptions(options);
		}

		// Token: 0x06000B9D RID: 2973
		protected abstract Compilation CommonWithOptions(CompilationOptions options);

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00023977 File Offset: 0x00021B77
		internal bool IsSubmission
		{
			get
			{
				return this._lazySubmissionSlotIndex != -3;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00023986 File Offset: 0x00021B86
		[Nullable(2)]
		private Compilation PreviousSubmission
		{
			[NullableContext(2)]
			get
			{
				ScriptCompilationInfo scriptCompilationInfo = this.ScriptCompilationInfo;
				if (scriptCompilationInfo == null)
				{
					return null;
				}
				return scriptCompilationInfo.PreviousScriptCompilation;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002399C File Offset: 0x00021B9C
		internal int GetSubmissionSlotIndex()
		{
			if (this._lazySubmissionSlotIndex == -2)
			{
				Compilation previousScriptCompilation = this.ScriptCompilationInfo.PreviousScriptCompilation;
				int num = ((previousScriptCompilation != null) ? previousScriptCompilation.GetSubmissionSlotIndex() : 0);
				this._lazySubmissionSlotIndex = (this.HasCodeToEmit() ? (num + 1) : num);
			}
			return this._lazySubmissionSlotIndex;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x000239E5 File Offset: 0x00021BE5
		[Nullable(2)]
		internal Type SubmissionReturnType
		{
			[NullableContext(2)]
			get
			{
				ScriptCompilationInfo scriptCompilationInfo = this.ScriptCompilationInfo;
				if (scriptCompilationInfo == null)
				{
					return null;
				}
				return scriptCompilationInfo.ReturnTypeOpt;
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000239F8 File Offset: 0x00021BF8
		internal static bool IsValidSubmissionReturnType(Type type)
		{
			return !(type == typeof(void)) && !type.IsByRef && !IntrospectionExtensions.GetTypeInfo(type).ContainsGenericParameters;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00023A24 File Offset: 0x00021C24
		[Nullable(2)]
		internal Type HostObjectType
		{
			[NullableContext(2)]
			get
			{
				ScriptCompilationInfo scriptCompilationInfo = this.ScriptCompilationInfo;
				if (scriptCompilationInfo == null)
				{
					return null;
				}
				return scriptCompilationInfo.GlobalsType;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00023A38 File Offset: 0x00021C38
		internal static bool IsValidHostObjectType(Type type)
		{
			TypeInfo typeInfo = IntrospectionExtensions.GetTypeInfo(type);
			return !typeInfo.IsValueType && !typeInfo.IsPointer && !typeInfo.IsByRef && !typeInfo.ContainsGenericParameters;
		}

		// Token: 0x06000BA5 RID: 2981
		internal abstract bool HasSubmissionResult();

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00023A6F File Offset: 0x00021C6F
		public Compilation WithScriptCompilationInfo([Nullable(2)] ScriptCompilationInfo info)
		{
			return this.CommonWithScriptCompilationInfo(info);
		}

		// Token: 0x06000BA7 RID: 2983
		protected abstract Compilation CommonWithScriptCompilationInfo([Nullable(2)] ScriptCompilationInfo info);

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00023A78 File Offset: 0x00021C78
		public IEnumerable<SyntaxTree> SyntaxTrees
		{
			get
			{
				return this.CommonSyntaxTrees;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000BA9 RID: 2985
		[Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<SyntaxTree> CommonSyntaxTrees
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00023A85 File Offset: 0x00021C85
		public Compilation AddSyntaxTrees(params SyntaxTree[] trees)
		{
			return this.CommonAddSyntaxTrees(trees);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00023A8E File Offset: 0x00021C8E
		public Compilation AddSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			return this.CommonAddSyntaxTrees(trees);
		}

		// Token: 0x06000BAC RID: 2988
		protected abstract Compilation CommonAddSyntaxTrees(IEnumerable<SyntaxTree> trees);

		// Token: 0x06000BAD RID: 2989 RVA: 0x00023A97 File Offset: 0x00021C97
		public Compilation RemoveSyntaxTrees(params SyntaxTree[] trees)
		{
			return this.CommonRemoveSyntaxTrees(trees);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00023AA0 File Offset: 0x00021CA0
		public Compilation RemoveSyntaxTrees(IEnumerable<SyntaxTree> trees)
		{
			return this.CommonRemoveSyntaxTrees(trees);
		}

		// Token: 0x06000BAF RID: 2991
		protected abstract Compilation CommonRemoveSyntaxTrees(IEnumerable<SyntaxTree> trees);

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00023AA9 File Offset: 0x00021CA9
		public Compilation RemoveAllSyntaxTrees()
		{
			return this.CommonRemoveAllSyntaxTrees();
		}

		// Token: 0x06000BB1 RID: 2993
		protected abstract Compilation CommonRemoveAllSyntaxTrees();

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00023AB1 File Offset: 0x00021CB1
		public Compilation ReplaceSyntaxTree(SyntaxTree oldTree, SyntaxTree newTree)
		{
			return this.CommonReplaceSyntaxTree(oldTree, newTree);
		}

		// Token: 0x06000BB3 RID: 2995
		protected abstract Compilation CommonReplaceSyntaxTree(SyntaxTree oldTree, SyntaxTree newTree);

		// Token: 0x06000BB4 RID: 2996 RVA: 0x00023ABB File Offset: 0x00021CBB
		public bool ContainsSyntaxTree(SyntaxTree syntaxTree)
		{
			return this.CommonContainsSyntaxTree(syntaxTree);
		}

		// Token: 0x06000BB5 RID: 2997
		[NullableContext(2)]
		protected abstract bool CommonContainsSyntaxTree(SyntaxTree syntaxTree);

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x00023AC4 File Offset: 0x00021CC4
		[Nullable(2)]
		internal SemanticModelProvider SemanticModelProvider
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00023ACC File Offset: 0x00021CCC
		[Nullable(new byte[] { 2, 1 })]
		internal AsyncQueue<CompilationEvent> EventQueue
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00023AD4 File Offset: 0x00021CD4
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<MetadataReference> ValidateReferences<T>([Nullable(new byte[] { 2, 1 })] IEnumerable<MetadataReference> references) where T : CompilationReference
		{
			ImmutableArray<MetadataReference> immutableArray = references.AsImmutableOrEmpty<MetadataReference>();
			for (int i = 0; i < immutableArray.Length; i++)
			{
				MetadataReference metadataReference = immutableArray[i];
				if (metadataReference == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "references", i));
				}
				if (!(metadataReference is PortableExecutableReference) && !(metadataReference is T))
				{
					throw new ArgumentException(string.Format(CodeAnalysisResources.ReferenceOfTypeIsInvalid1, metadataReference.GetType()), string.Format("{0}[{1}]", "references", i));
				}
			}
			return immutableArray;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00023B5D File Offset: 0x00021D5D
		internal CommonReferenceManager GetBoundReferenceManager()
		{
			return this.CommonGetBoundReferenceManager();
		}

		// Token: 0x06000BBA RID: 3002
		internal abstract CommonReferenceManager CommonGetBoundReferenceManager();

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00023B65 File Offset: 0x00021D65
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MetadataReference> ExternalReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000BBC RID: 3004
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<MetadataReference> DirectiveReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000BBD RID: 3005
		internal abstract IEnumerable<ReferenceDirective> ReferenceDirectives { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BBE RID: 3006
		[TupleElementNames(new string[] { "path", "content" })]
		[Nullable(new byte[] { 1, 0, 1, 1, 1 })]
		internal abstract IDictionary<ValueTuple<string, string>, MetadataReference> ReferenceDirectiveMap
		{
			[return: TupleElementNames(new string[] { "path", "content" })]
			[return: Nullable(new byte[] { 1, 0, 1, 1, 1 })]
			get;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x00023B6D File Offset: 0x00021D6D
		public IEnumerable<MetadataReference> References
		{
			get
			{
				foreach (MetadataReference metadataReference in this.ExternalReferences)
				{
					yield return metadataReference;
				}
				ImmutableArray<MetadataReference>.Enumerator enumerator = default(ImmutableArray<MetadataReference>.Enumerator);
				foreach (MetadataReference metadataReference2 in this.DirectiveReferences)
				{
					yield return metadataReference2;
				}
				enumerator = default(ImmutableArray<MetadataReference>.Enumerator);
				yield break;
			}
		}

		// Token: 0x06000BC0 RID: 3008
		public abstract CompilationReference ToMetadataReference([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases = default(ImmutableArray<string>), bool embedInteropTypes = false);

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00023B7D File Offset: 0x00021D7D
		public Compilation WithReferences(IEnumerable<MetadataReference> newReferences)
		{
			return this.CommonWithReferences(newReferences);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00023B86 File Offset: 0x00021D86
		public Compilation WithReferences(params MetadataReference[] newReferences)
		{
			return this.WithReferences(newReferences);
		}

		// Token: 0x06000BC3 RID: 3011
		protected abstract Compilation CommonWithReferences(IEnumerable<MetadataReference> newReferences);

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00023B8F File Offset: 0x00021D8F
		public Compilation AddReferences(params MetadataReference[] references)
		{
			return this.AddReferences(references);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00023B98 File Offset: 0x00021D98
		public Compilation AddReferences(IEnumerable<MetadataReference> references)
		{
			if (references == null)
			{
				throw new ArgumentNullException("references");
			}
			if (references.IsEmpty<MetadataReference>())
			{
				return this;
			}
			return this.CommonWithReferences(Enumerable.Union<MetadataReference>(this.ExternalReferences, references));
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00023BC9 File Offset: 0x00021DC9
		public Compilation RemoveReferences(params MetadataReference[] references)
		{
			return this.RemoveReferences(references);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00023BD4 File Offset: 0x00021DD4
		public Compilation RemoveReferences(IEnumerable<MetadataReference> references)
		{
			if (references == null)
			{
				throw new ArgumentNullException("references");
			}
			if (references.IsEmpty<MetadataReference>())
			{
				return this;
			}
			HashSet<MetadataReference> hashSet = new HashSet<MetadataReference>(this.ExternalReferences);
			foreach (MetadataReference metadataReference in Enumerable.Distinct<MetadataReference>(references))
			{
				if (!hashSet.Remove(metadataReference))
				{
					throw new ArgumentException(string.Format(CodeAnalysisResources.MetadataRefNotFoundToRemove1, metadataReference), "references");
				}
			}
			return this.CommonWithReferences(hashSet);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00023C6C File Offset: 0x00021E6C
		public Compilation RemoveAllReferences()
		{
			return this.CommonWithReferences(SpecializedCollections.EmptyEnumerable<MetadataReference>());
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00023C7C File Offset: 0x00021E7C
		public Compilation ReplaceReference(MetadataReference oldReference, [Nullable(2)] MetadataReference newReference)
		{
			if (oldReference == null)
			{
				throw new ArgumentNullException("oldReference");
			}
			if (newReference == null)
			{
				return this.RemoveReferences(new MetadataReference[] { oldReference });
			}
			return this.RemoveReferences(new MetadataReference[] { oldReference }).AddReferences(new MetadataReference[] { newReference });
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00023CCA File Offset: 0x00021ECA
		[return: Nullable(2)]
		public ISymbol GetAssemblyOrModuleSymbol(MetadataReference reference)
		{
			return this.CommonGetAssemblyOrModuleSymbol(reference);
		}

		// Token: 0x06000BCB RID: 3019
		[return: Nullable(2)]
		protected abstract ISymbol CommonGetAssemblyOrModuleSymbol(MetadataReference reference);

		// Token: 0x06000BCC RID: 3020 RVA: 0x00023CD3 File Offset: 0x00021ED3
		[return: Nullable(2)]
		public MetadataReference GetMetadataReference(IAssemblySymbol assemblySymbol)
		{
			return this.CommonGetMetadataReference(assemblySymbol);
		}

		// Token: 0x06000BCD RID: 3021
		[return: Nullable(2)]
		private protected abstract MetadataReference CommonGetMetadataReference(IAssemblySymbol assemblySymbol);

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BCE RID: 3022
		public abstract IEnumerable<AssemblyIdentity> ReferencedAssemblyNames { get; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00023CDC File Offset: 0x00021EDC
		public IAssemblySymbol Assembly
		{
			get
			{
				return this.CommonAssembly;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000BD0 RID: 3024
		protected abstract IAssemblySymbol CommonAssembly { get; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00023CE4 File Offset: 0x00021EE4
		public IModuleSymbol SourceModule
		{
			get
			{
				return this.CommonSourceModule;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000BD2 RID: 3026
		protected abstract IModuleSymbol CommonSourceModule { get; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00023CEC File Offset: 0x00021EEC
		public INamespaceSymbol GlobalNamespace
		{
			get
			{
				return this.CommonGlobalNamespace;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000BD4 RID: 3028
		protected abstract INamespaceSymbol CommonGlobalNamespace { get; }

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00023CF4 File Offset: 0x00021EF4
		[return: Nullable(2)]
		public INamespaceSymbol GetCompilationNamespace(INamespaceSymbol namespaceSymbol)
		{
			return this.CommonGetCompilationNamespace(namespaceSymbol);
		}

		// Token: 0x06000BD6 RID: 3030
		[return: Nullable(2)]
		protected abstract INamespaceSymbol CommonGetCompilationNamespace(INamespaceSymbol namespaceSymbol);

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000BD7 RID: 3031
		internal abstract CommonAnonymousTypeManager CommonAnonymousTypeManager { get; }

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00023CFD File Offset: 0x00021EFD
		[NullableContext(2)]
		public IMethodSymbol GetEntryPoint(CancellationToken cancellationToken)
		{
			return this.CommonGetEntryPoint(cancellationToken);
		}

		// Token: 0x06000BD9 RID: 3033
		[NullableContext(2)]
		protected abstract IMethodSymbol CommonGetEntryPoint(CancellationToken cancellationToken);

		// Token: 0x06000BDA RID: 3034 RVA: 0x00023D06 File Offset: 0x00021F06
		public INamedTypeSymbol GetSpecialType(SpecialType specialType)
		{
			return (INamedTypeSymbol)this.CommonGetSpecialType(specialType).GetITypeSymbol();
		}

		// Token: 0x06000BDB RID: 3035
		internal abstract ISymbolInternal CommonGetSpecialTypeMember(SpecialMember specialMember);

		// Token: 0x06000BDC RID: 3036
		internal abstract bool IsSystemTypeReference(ITypeSymbolInternal type);

		// Token: 0x06000BDD RID: 3037
		private protected abstract INamedTypeSymbolInternal CommonGetSpecialType(SpecialType specialType);

		// Token: 0x06000BDE RID: 3038
		[NullableContext(2)]
		internal abstract ISymbolInternal CommonGetWellKnownTypeMember(WellKnownMember member);

		// Token: 0x06000BDF RID: 3039
		internal abstract ITypeSymbolInternal CommonGetWellKnownType(WellKnownType wellknownType);

		// Token: 0x06000BE0 RID: 3040
		internal abstract bool IsAttributeType(ITypeSymbol type);

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00023D19 File Offset: 0x00021F19
		public INamedTypeSymbol ObjectType
		{
			get
			{
				return this.CommonObjectType;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000BE2 RID: 3042
		protected abstract INamedTypeSymbol CommonObjectType { get; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00023D21 File Offset: 0x00021F21
		public ITypeSymbol DynamicType
		{
			get
			{
				return this.CommonDynamicType;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000BE4 RID: 3044
		protected abstract ITypeSymbol CommonDynamicType { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00023D29 File Offset: 0x00021F29
		[Nullable(2)]
		internal ITypeSymbol ScriptGlobalsType
		{
			[NullableContext(2)]
			get
			{
				return this.CommonScriptGlobalsType;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000BE6 RID: 3046
		[Nullable(2)]
		protected abstract ITypeSymbol CommonScriptGlobalsType
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x00023D31 File Offset: 0x00021F31
		[Nullable(2)]
		public INamedTypeSymbol ScriptClass
		{
			[NullableContext(2)]
			get
			{
				return this.CommonScriptClass;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000BE8 RID: 3048
		[Nullable(2)]
		protected abstract INamedTypeSymbol CommonScriptClass
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00023D3C File Offset: 0x00021F3C
		[NullableContext(2)]
		protected INamedTypeSymbol CommonBindScriptClass()
		{
			string[] array = (this.Options.ScriptClassName ?? "").Split(new char[] { '.' });
			INamespaceSymbol namespaceSymbol = this.SourceModule.GlobalNamespace;
			for (int i = 0; i < array.Length - 1; i++)
			{
				INamespaceSymbol nestedNamespace = namespaceSymbol.GetNestedNamespace(array[i]);
				if (nestedNamespace == null)
				{
					return null;
				}
				namespaceSymbol = nestedNamespace;
			}
			foreach (INamedTypeSymbol namedTypeSymbol in namespaceSymbol.GetTypeMembers(array[array.Length - 1]))
			{
				if (namedTypeSymbol.IsScriptClass)
				{
					return namedTypeSymbol;
				}
			}
			return null;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00023DD4 File Offset: 0x00021FD4
		[Conditional("DEBUG")]
		private void AssertNoScriptTrees()
		{
			foreach (SyntaxTree syntaxTree in this.CommonSyntaxTrees)
			{
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00023E03 File Offset: 0x00022003
		public IArrayTypeSymbol CreateArrayTypeSymbol(ITypeSymbol elementType, int rank = 1, NullableAnnotation elementNullableAnnotation = NullableAnnotation.None)
		{
			return this.CommonCreateArrayTypeSymbol(elementType, rank, elementNullableAnnotation);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00023E0E File Offset: 0x0002200E
		public IArrayTypeSymbol CreateArrayTypeSymbol(ITypeSymbol elementType, int rank)
		{
			return this.CreateArrayTypeSymbol(elementType, rank, NullableAnnotation.None);
		}

		// Token: 0x06000BED RID: 3053
		protected abstract IArrayTypeSymbol CommonCreateArrayTypeSymbol(ITypeSymbol elementType, int rank, NullableAnnotation elementNullableAnnotation);

		// Token: 0x06000BEE RID: 3054 RVA: 0x00023E19 File Offset: 0x00022019
		public IPointerTypeSymbol CreatePointerTypeSymbol(ITypeSymbol pointedAtType)
		{
			return this.CommonCreatePointerTypeSymbol(pointedAtType);
		}

		// Token: 0x06000BEF RID: 3055
		protected abstract IPointerTypeSymbol CommonCreatePointerTypeSymbol(ITypeSymbol elementType);

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00023E22 File Offset: 0x00022022
		public IFunctionPointerTypeSymbol CreateFunctionPointerTypeSymbol(ITypeSymbol returnType, RefKind returnRefKind, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> parameterTypes, [Nullable(0)] ImmutableArray<RefKind> parameterRefKinds, SignatureCallingConvention callingConvention = SignatureCallingConvention.Default, [Nullable(new byte[] { 0, 1 })] ImmutableArray<INamedTypeSymbol> callingConventionTypes = default(ImmutableArray<INamedTypeSymbol>))
		{
			return this.CommonCreateFunctionPointerTypeSymbol(returnType, returnRefKind, parameterTypes, parameterRefKinds, callingConvention, callingConventionTypes);
		}

		// Token: 0x06000BF1 RID: 3057
		protected abstract IFunctionPointerTypeSymbol CommonCreateFunctionPointerTypeSymbol(ITypeSymbol returnType, RefKind returnRefKind, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> parameterTypes, [Nullable(0)] ImmutableArray<RefKind> parameterRefKinds, SignatureCallingConvention callingConvention, [Nullable(new byte[] { 0, 1 })] ImmutableArray<INamedTypeSymbol> callingConventionTypes);

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00023E33 File Offset: 0x00022033
		public INamedTypeSymbol CreateNativeIntegerTypeSymbol(bool signed)
		{
			return this.CommonCreateNativeIntegerTypeSymbol(signed);
		}

		// Token: 0x06000BF3 RID: 3059
		protected abstract INamedTypeSymbol CommonCreateNativeIntegerTypeSymbol(bool signed);

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00023E3C File Offset: 0x0002203C
		[return: Nullable(2)]
		public INamedTypeSymbol GetTypeByMetadataName(string fullyQualifiedMetadataName)
		{
			INamedTypeSymbol namedTypeSymbol;
			if (!this._getTypeCache.TryGetValue(fullyQualifiedMetadataName, out namedTypeSymbol))
			{
				namedTypeSymbol = this.CommonGetTypeByMetadataName(fullyQualifiedMetadataName);
				this._getTypeCache.TryAdd(fullyQualifiedMetadataName, namedTypeSymbol);
			}
			return namedTypeSymbol;
		}

		// Token: 0x06000BF5 RID: 3061
		[return: Nullable(2)]
		protected abstract INamedTypeSymbol CommonGetTypeByMetadataName(string metadataName);

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00023E70 File Offset: 0x00022070
		[NullableContext(0)]
		[return: Nullable(1)]
		public INamedTypeSymbol CreateTupleTypeSymbol([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> elementTypes, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames = default(ImmutableArray<string>), [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations = default(ImmutableArray<Location>), ImmutableArray<NullableAnnotation> elementNullableAnnotations = default(ImmutableArray<NullableAnnotation>))
		{
			if (elementTypes.IsDefault)
			{
				throw new ArgumentNullException("elementTypes");
			}
			int length = elementTypes.Length;
			if (elementTypes.Length <= 1)
			{
				throw new ArgumentException(CodeAnalysisResources.TuplesNeedAtLeastTwoElements, "elementNames");
			}
			elementNames = Compilation.CheckTupleElementNames(length, elementNames);
			Compilation.CheckTupleElementLocations(length, elementLocations);
			Compilation.CheckTupleElementNullableAnnotations(length, elementNullableAnnotations);
			for (int i = 0; i < length; i++)
			{
				if (elementTypes[i] == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "elementTypes", i));
				}
				if (!elementLocations.IsDefault && elementLocations[i] == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "elementLocations", i));
				}
			}
			return this.CommonCreateTupleTypeSymbol(elementTypes, elementNames, elementLocations, elementNullableAnnotations);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00023F3C File Offset: 0x0002213C
		public INamedTypeSymbol CreateTupleTypeSymbol([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> elementTypes, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations)
		{
			return this.CreateTupleTypeSymbol(elementTypes, elementNames, elementLocations, default(ImmutableArray<NullableAnnotation>));
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00023F5B File Offset: 0x0002215B
		[NullableContext(0)]
		protected static void CheckTupleElementNullableAnnotations(int cardinality, ImmutableArray<NullableAnnotation> elementNullableAnnotations)
		{
			if (!elementNullableAnnotations.IsDefault && elementNullableAnnotations.Length != cardinality)
			{
				throw new ArgumentException(CodeAnalysisResources.TupleElementNullableAnnotationCountMismatch, "elementNullableAnnotations");
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00023F80 File Offset: 0x00022180
		[return: Nullable(new byte[] { 0, 2 })]
		protected static ImmutableArray<string> CheckTupleElementNames(int cardinality, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames)
		{
			if (!elementNames.IsDefault)
			{
				if (elementNames.Length != cardinality)
				{
					throw new ArgumentException(CodeAnalysisResources.TupleElementNameCountMismatch, "elementNames");
				}
				for (int i = 0; i < elementNames.Length; i++)
				{
					if (elementNames[i] == "")
					{
						throw new ArgumentException(CodeAnalysisResources.TupleElementNameEmpty, string.Format("{0}[{1}]", "elementNames", i));
					}
				}
				if (elementNames.All<string>((string n) => n == null))
				{
					return default(ImmutableArray<string>);
				}
			}
			return elementNames;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002402B File Offset: 0x0002222B
		protected static void CheckTupleElementLocations(int cardinality, [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations)
		{
			if (!elementLocations.IsDefault && elementLocations.Length != cardinality)
			{
				throw new ArgumentException(CodeAnalysisResources.TupleElementLocationCountMismatch, "elementLocations");
			}
		}

		// Token: 0x06000BFB RID: 3067
		[NullableContext(0)]
		[return: Nullable(1)]
		protected abstract INamedTypeSymbol CommonCreateTupleTypeSymbol([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> elementTypes, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations, ImmutableArray<NullableAnnotation> elementNullableAnnotations);

		// Token: 0x06000BFC RID: 3068 RVA: 0x00024050 File Offset: 0x00022250
		public INamedTypeSymbol CreateTupleTypeSymbol(INamedTypeSymbol underlyingType, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames = default(ImmutableArray<string>), [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations = default(ImmutableArray<Location>), [Nullable(0)] ImmutableArray<NullableAnnotation> elementNullableAnnotations = default(ImmutableArray<NullableAnnotation>))
		{
			if (underlyingType == null)
			{
				throw new ArgumentNullException("underlyingType");
			}
			return this.CommonCreateTupleTypeSymbol(underlyingType, elementNames, elementLocations, elementNullableAnnotations);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002406C File Offset: 0x0002226C
		public INamedTypeSymbol CreateTupleTypeSymbol(INamedTypeSymbol underlyingType, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations)
		{
			return this.CreateTupleTypeSymbol(underlyingType, elementNames, elementLocations, default(ImmutableArray<NullableAnnotation>));
		}

		// Token: 0x06000BFE RID: 3070
		protected abstract INamedTypeSymbol CommonCreateTupleTypeSymbol(INamedTypeSymbol underlyingType, [Nullable(new byte[] { 0, 2 })] ImmutableArray<string> elementNames, [Nullable(new byte[] { 0, 2 })] ImmutableArray<Location> elementLocations, [Nullable(0)] ImmutableArray<NullableAnnotation> elementNullableAnnotations);

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002408C File Offset: 0x0002228C
		[NullableContext(0)]
		[return: Nullable(1)]
		public INamedTypeSymbol CreateAnonymousTypeSymbol([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> memberTypes, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> memberNames, ImmutableArray<bool> memberIsReadOnly = default(ImmutableArray<bool>), [Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> memberLocations = default(ImmutableArray<Location>), ImmutableArray<NullableAnnotation> memberNullableAnnotations = default(ImmutableArray<NullableAnnotation>))
		{
			if (memberTypes.IsDefault)
			{
				throw new ArgumentNullException("memberTypes");
			}
			if (memberNames.IsDefault)
			{
				throw new ArgumentNullException("memberNames");
			}
			if (memberTypes.Length != memberNames.Length)
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.AnonymousTypeMemberAndNamesCountMismatch2, "memberTypes", "memberNames"));
			}
			if (!memberLocations.IsDefault && memberLocations.Length != memberTypes.Length)
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.AnonymousTypeArgumentCountMismatch2, "memberLocations", "memberNames"));
			}
			if (!memberIsReadOnly.IsDefault && memberIsReadOnly.Length != memberTypes.Length)
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.AnonymousTypeArgumentCountMismatch2, "memberIsReadOnly", "memberNames"));
			}
			if (!memberNullableAnnotations.IsDefault && memberNullableAnnotations.Length != memberTypes.Length)
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.AnonymousTypeArgumentCountMismatch2, "memberNullableAnnotations", "memberNames"));
			}
			int i = 0;
			int length = memberTypes.Length;
			while (i < length)
			{
				if (memberTypes[i] == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "memberTypes", i));
				}
				if (memberNames[i] == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "memberNames", i));
				}
				if (!memberLocations.IsDefault && memberLocations[i] == null)
				{
					throw new ArgumentNullException(string.Format("{0}[{1}]", "memberLocations", i));
				}
				i++;
			}
			return this.CommonCreateAnonymousTypeSymbol(memberTypes, memberNames, memberLocations, memberIsReadOnly, memberNullableAnnotations);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002422C File Offset: 0x0002242C
		[NullableContext(0)]
		[return: Nullable(1)]
		public INamedTypeSymbol CreateAnonymousTypeSymbol([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> memberTypes, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> memberNames, ImmutableArray<bool> memberIsReadOnly, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> memberLocations)
		{
			return this.CreateAnonymousTypeSymbol(memberTypes, memberNames, memberIsReadOnly, memberLocations, default(ImmutableArray<NullableAnnotation>));
		}

		// Token: 0x06000C01 RID: 3073
		[NullableContext(0)]
		[return: Nullable(1)]
		protected abstract INamedTypeSymbol CommonCreateAnonymousTypeSymbol([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> memberTypes, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> memberNames, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> memberLocations, ImmutableArray<bool> memberIsReadOnly, ImmutableArray<NullableAnnotation> memberNullableAnnotations);

		// Token: 0x06000C02 RID: 3074
		public abstract CommonConversion ClassifyCommonConversion(ITypeSymbol source, ITypeSymbol destination);

		// Token: 0x06000C03 RID: 3075 RVA: 0x00024250 File Offset: 0x00022450
		[NullableContext(2)]
		public bool HasImplicitConversion(ITypeSymbol fromType, ITypeSymbol toType)
		{
			return fromType != null && toType != null && this.ClassifyCommonConversion(fromType, toType).IsImplicit;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00024278 File Offset: 0x00022478
		public bool IsSymbolAccessibleWithin(ISymbol symbol, ISymbol within, [Nullable(2)] ITypeSymbol throughType = null)
		{
			if (symbol == null)
			{
				throw new ArgumentNullException("symbol");
			}
			if (within == null)
			{
				throw new ArgumentNullException("within");
			}
			if (!(within is INamedTypeSymbol) && !(within is IAssemblySymbol))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.IsSymbolAccessibleBadWithin, "within"), "within");
			}
			this.<IsSymbolAccessibleWithin>g__checkInCompilationReferences|185_0(symbol, "symbol");
			this.<IsSymbolAccessibleWithin>g__checkInCompilationReferences|185_0(within, "within");
			if (throughType != null)
			{
				this.<IsSymbolAccessibleWithin>g__checkInCompilationReferences|185_0(throughType, "throughType");
			}
			return this.IsSymbolAccessibleWithinCore(symbol, within, throughType);
		}

		// Token: 0x06000C05 RID: 3077
		private protected abstract bool IsSymbolAccessibleWithinCore(ISymbol symbol, ISymbol within, [Nullable(2)] ITypeSymbol throughType);

		// Token: 0x06000C06 RID: 3078
		internal abstract IConvertibleConversion ClassifyConvertibleConversion(IOperation source, ITypeSymbol destination, [Nullable(2)] out ConstantValue constantValue);

		// Token: 0x06000C07 RID: 3079
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetParseDiagnostics(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C08 RID: 3080
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetDeclarationDiagnostics(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C09 RID: 3081
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetMethodBodyDiagnostics(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C0A RID: 3082
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<Diagnostic> GetDiagnostics(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C0B RID: 3083
		internal abstract void GetDiagnostics(CompilationStage stage, bool includeEarlierStages, DiagnosticBag diagnostics, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C0C RID: 3084
		[return: Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<MetadataReference> GetUsedAssemblyReferences(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C0D RID: 3085 RVA: 0x000242FC File Offset: 0x000224FC
		internal void EnsureCompilationEventQueueCompleted()
		{
			AsyncQueue<CompilationEvent> eventQueue = this.EventQueue;
			lock (eventQueue)
			{
				if (!this.EventQueue.IsCompleted)
				{
					this.CompleteCompilationEventQueue_NoLock();
				}
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002434C File Offset: 0x0002254C
		internal void CompleteCompilationEventQueue_NoLock()
		{
			this.EventQueue.TryEnqueue(new CompilationCompletedEvent(this));
			this.EventQueue.PromiseNotToEnqueue();
			this.EventQueue.TryComplete();
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000C0F RID: 3087
		internal abstract CommonMessageProvider MessageProvider { get; }

		// Token: 0x06000C10 RID: 3088 RVA: 0x00024377 File Offset: 0x00022577
		internal bool FilterAndAppendAndFreeDiagnostics(DiagnosticBag accumulator, [Nullable(2)] [DisallowNull] ref DiagnosticBag incoming, CancellationToken cancellationToken)
		{
			bool flag = this.FilterAndAppendDiagnostics(accumulator, incoming.AsEnumerableWithoutResolution(), null, cancellationToken);
			incoming.Free();
			incoming = null;
			return flag;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00024394 File Offset: 0x00022594
		internal bool FilterAndAppendDiagnostics(DiagnosticBag accumulator, IEnumerable<Diagnostic> incoming, [Nullable(2)] HashSet<int> exclude, CancellationToken cancellationToken)
		{
			bool flag = false;
			bool reportSuppressedDiagnostics = this.Options.ReportSuppressedDiagnostics;
			foreach (Diagnostic diagnostic in incoming)
			{
				if (exclude == null || !exclude.Contains(diagnostic.Code))
				{
					Diagnostic diagnostic2 = this.Options.FilterDiagnostic(diagnostic, cancellationToken);
					if (diagnostic2 != null && (reportSuppressedDiagnostics || !diagnostic2.IsSuppressed))
					{
						if (diagnostic2.IsUnsuppressableError())
						{
							flag = true;
						}
						accumulator.Add(diagnostic2);
					}
				}
			}
			return !flag;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002442C File Offset: 0x0002262C
		[NullableContext(2)]
		[return: Nullable(1)]
		public Stream CreateDefaultWin32Resources(bool versionResource, bool noManifest, Stream manifestContents, Stream iconInIcoFormat)
		{
			MemoryStream memoryStream = new MemoryStream(1024);
			Compilation.AppendNullResource(memoryStream);
			if (versionResource)
			{
				this.AppendDefaultVersionResource(memoryStream);
			}
			if (!noManifest)
			{
				if (this.Options.OutputKind.IsApplication() && manifestContents == null)
				{
					manifestContents = IntrospectionExtensions.GetTypeInfo(typeof(Compilation)).Assembly.GetManifestResourceStream("Microsoft.CodeAnalysis.Resources.default.win32manifest");
				}
				if (manifestContents != null)
				{
					Win32ResourceConversions.AppendManifestToResourceStream(memoryStream, manifestContents, !this.Options.OutputKind.IsApplication());
				}
			}
			if (iconInIcoFormat != null)
			{
				Win32ResourceConversions.AppendIconToResourceStream(memoryStream, iconInIcoFormat);
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000244C0 File Offset: 0x000226C0
		internal static void AppendNullResource(Stream resourceStream)
		{
			BinaryWriter binaryWriter = new BinaryWriter(resourceStream);
			binaryWriter.Write(0U);
			binaryWriter.Write(32U);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(0);
			binaryWriter.Write(ushort.MaxValue);
			binaryWriter.Write(0);
			binaryWriter.Write(0U);
			binaryWriter.Write(0);
			binaryWriter.Write(0);
			binaryWriter.Write(0U);
			binaryWriter.Write(0U);
		}

		// Token: 0x06000C14 RID: 3092
		protected abstract void AppendDefaultVersionResource(Stream resourceStream);

		// Token: 0x06000C15 RID: 3093 RVA: 0x00024528 File Offset: 0x00022728
		internal static Compilation.Win32ResourceForm DetectWin32ResourceForm(Stream win32Resources)
		{
			BinaryReader binaryReader = new BinaryReader(win32Resources, Encoding.Unicode);
			long position = win32Resources.Position;
			uint num = binaryReader.ReadUInt32();
			win32Resources.Position = position;
			if (num == 0U)
			{
				return Compilation.Win32ResourceForm.RES;
			}
			if ((num & 4294901760U) != 0U || (num & 65535U) != 65535U)
			{
				return Compilation.Win32ResourceForm.COFF;
			}
			return Compilation.Win32ResourceForm.UNKNOWN;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00024574 File Offset: 0x00022774
		[NullableContext(2)]
		internal ResourceSection MakeWin32ResourcesFromCOFF(Stream win32Resources, [Nullable(1)] DiagnosticBag diagnostics)
		{
			if (win32Resources == null)
			{
				return null;
			}
			ResourceSection resourceSection;
			try
			{
				resourceSection = COFFResourceReader.ReadWin32ResourcesFromCOFF(win32Resources);
			}
			catch (BadImageFormatException ex)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, Location.None, new object[] { ex.Message }));
				return null;
			}
			catch (IOException ex2)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, Location.None, new object[] { ex2.Message }));
				return null;
			}
			catch (ResourceException ex3)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, Location.None, new object[] { ex3.Message }));
				return null;
			}
			return resourceSection;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00024660 File Offset: 0x00022860
		[return: Nullable(new byte[] { 2, 1 })]
		internal List<Win32Resource> MakeWin32ResourceList([Nullable(2)] Stream win32Resources, DiagnosticBag diagnostics)
		{
			if (win32Resources == null)
			{
				return null;
			}
			List<RESOURCE> list;
			try
			{
				list = CvtResFile.ReadResFile(win32Resources);
			}
			catch (ResourceException ex)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, Location.None, new object[] { ex.Message }));
				return null;
			}
			if (list == null)
			{
				return null;
			}
			List<Win32Resource> list2 = new List<Win32Resource>();
			foreach (RESOURCE resource in list)
			{
				Win32Resource win32Resource = new Win32Resource(resource.data, 0U, (uint)resource.LanguageId, (int)((short)resource.pstringName.Ordinal), resource.pstringName.theString, (int)((short)resource.pstringType.Ordinal), resource.pstringType.theString);
				list2.Add(win32Resource);
			}
			return list2;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002475C File Offset: 0x0002295C
		internal void SetupWin32Resources(CommonPEModuleBuilder moduleBeingBuilt, [Nullable(2)] Stream win32Resources, bool useRawWin32Resources, DiagnosticBag diagnostics)
		{
			if (win32Resources == null)
			{
				return;
			}
			if (useRawWin32Resources)
			{
				moduleBeingBuilt.RawWin32Resources = win32Resources;
				return;
			}
			Compilation.Win32ResourceForm win32ResourceForm;
			try
			{
				win32ResourceForm = Compilation.DetectWin32ResourceForm(win32Resources);
			}
			catch (EndOfStreamException)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, NoLocation.Singleton, new object[] { CodeAnalysisResources.UnrecognizedResourceFileFormat }));
				return;
			}
			catch (Exception ex)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, NoLocation.Singleton, new object[] { ex.Message }));
				return;
			}
			if (win32ResourceForm == Compilation.Win32ResourceForm.COFF)
			{
				moduleBeingBuilt.Win32ResourceSection = this.MakeWin32ResourcesFromCOFF(win32Resources, diagnostics);
				return;
			}
			if (win32ResourceForm == Compilation.Win32ResourceForm.RES)
			{
				moduleBeingBuilt.Win32Resources = this.MakeWin32ResourceList(win32Resources, diagnostics);
				return;
			}
			diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_BadWin32Resource, NoLocation.Singleton, new object[] { CodeAnalysisResources.UnrecognizedResourceFileFormat }));
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00024864 File Offset: 0x00022A64
		internal void ReportManifestResourceDuplicates([Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, IEnumerable<string> addedModuleNames, IEnumerable<string> addedModuleResourceNames, DiagnosticBag diagnostics)
		{
			if (this.Options.OutputKind == OutputKind.NetModule && (manifestResources == null || !Enumerable.Any<ResourceDescription>(manifestResources)))
			{
				return;
			}
			HashSet<string> hashSet = new HashSet<string>();
			if (manifestResources != null && Enumerable.Any<ResourceDescription>(manifestResources))
			{
				HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				foreach (ResourceDescription resourceDescription in manifestResources)
				{
					if (!hashSet.Add(resourceDescription.ResourceName))
					{
						diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_ResourceNotUnique, Location.None, new object[] { resourceDescription.ResourceName }));
					}
					string fileName = resourceDescription.FileName;
					if (fileName != null && !hashSet2.Add(fileName))
					{
						diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_ResourceFileNameNotUnique, Location.None, new object[] { fileName }));
					}
				}
				foreach (string text in addedModuleNames)
				{
					if (!hashSet2.Add(text))
					{
						diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_ResourceFileNameNotUnique, Location.None, new object[] { text }));
					}
				}
			}
			if (this.Options.OutputKind != OutputKind.NetModule)
			{
				foreach (string text2 in addedModuleResourceNames)
				{
					if (!hashSet.Add(text2))
					{
						diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_ResourceNotUnique, Location.None, new object[] { text2 }));
					}
				}
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00024A54 File Offset: 0x00022C54
		internal bool SignUsingBuilder
		{
			get
			{
				return string.IsNullOrEmpty(this.StrongNameKeys.KeyContainer) && !this.StrongNameKeys.HasCounterSignature && !this._features.ContainsKey("UseLegacyStrongNameProvider");
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00024A8C File Offset: 0x00022C8C
		internal ModulePropertiesForSerialization ConstructModuleSerializationProperties(EmitOptions emitOptions, [Nullable(2)] string targetRuntimeVersion, Guid moduleVersionId = default(Guid))
		{
			CompilationOptions options = this.Options;
			Platform platform = options.Platform;
			OutputKind outputKind = options.OutputKind;
			if (!platform.IsValid())
			{
				platform = Platform.AnyCpu;
			}
			if (!outputKind.IsValid())
			{
				outputKind = OutputKind.DynamicallyLinkedLibrary;
			}
			bool flag = platform.Requires64Bit();
			bool flag2 = platform.Requires32Bit();
			ushort num;
			if (emitOptions.FileAlignment == 0 || !CompilationOptions.IsValidFileAlignment(emitOptions.FileAlignment))
			{
				num = (flag ? 512 : 512);
			}
			else
			{
				num = (ushort)emitOptions.FileAlignment;
			}
			ulong num2 = (emitOptions.BaseAddress + 32768UL) & (flag ? 18446744073709486080UL : ((ulong)(-65536)));
			if (num2 == 0UL)
			{
				if (outputKind == OutputKind.ConsoleApplication || outputKind == OutputKind.WindowsApplication || outputKind == OutputKind.WindowsRuntimeApplication)
				{
					num2 = (flag ? 5368709120UL : 4194304UL);
				}
				else
				{
					num2 = (flag ? 6442450944UL : 268435456UL);
				}
			}
			ulong num3 = (flag ? 8192UL : 4096UL);
			ulong num4 = (flag ? 4194304UL : 1048576UL);
			ulong num5 = (flag ? 16384UL : 4096UL);
			SubsystemVersion subsystemVersion;
			if (emitOptions.SubsystemVersion.Equals(SubsystemVersion.None) || !emitOptions.SubsystemVersion.IsValid)
			{
				subsystemVersion = SubsystemVersion.Default(outputKind, platform);
			}
			else
			{
				subsystemVersion = emitOptions.SubsystemVersion;
			}
			Machine machine;
			switch (platform)
			{
			case Platform.AnyCpu:
			case Platform.AnyCpu32BitPreferred:
				machine = Machine.Unknown;
				break;
			case Platform.X86:
				machine = Machine.I386;
				break;
			case Platform.X64:
				machine = Machine.Amd64;
				break;
			case Platform.Itanium:
				machine = Machine.IA64;
				break;
			case Platform.Arm:
				machine = Machine.ArmThumb2;
				break;
			case Platform.Arm64:
				machine = Machine.Arm64;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(platform);
			}
			return new ModulePropertiesForSerialization(moduleVersionId, Compilation.GetCorHeaderFlags(machine, this.HasStrongName, platform == Platform.AnyCpu32BitPreferred), (int)num, 8192, targetRuntimeVersion, machine, num2, 1048576UL, num3, num4, num5, Compilation.GetDllCharacteristics(emitOptions.HighEntropyVirtualAddressSpace, options.OutputKind == OutputKind.WindowsRuntimeApplication), Compilation.GetCharacteristics(outputKind, flag2), Compilation.GetSubsystem(outputKind), (ushort)subsystemVersion.Major, (ushort)subsystemVersion.Minor, this.LinkerMajorVersion, 0);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00024CA0 File Offset: 0x00022EA0
		private static CorFlags GetCorHeaderFlags(Machine machine, bool strongNameSigned, bool prefers32Bit)
		{
			CorFlags corFlags = CorFlags.ILOnly;
			if (machine == Machine.I386)
			{
				corFlags |= CorFlags.Requires32Bit;
			}
			if (strongNameSigned)
			{
				corFlags |= CorFlags.StrongNameSigned;
			}
			if (prefers32Bit)
			{
				corFlags |= CorFlags.Requires32Bit | CorFlags.Prefers32Bit;
			}
			return corFlags;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00024CD0 File Offset: 0x00022ED0
		internal static DllCharacteristics GetDllCharacteristics(bool enableHighEntropyVA, bool configureToExecuteInAppContainer)
		{
			DllCharacteristics dllCharacteristics = DllCharacteristics.DynamicBase | DllCharacteristics.NxCompatible | DllCharacteristics.NoSeh | DllCharacteristics.TerminalServerAware;
			if (enableHighEntropyVA)
			{
				dllCharacteristics |= DllCharacteristics.HighEntropyVirtualAddressSpace;
			}
			if (configureToExecuteInAppContainer)
			{
				dllCharacteristics |= DllCharacteristics.AppContainer;
			}
			return dllCharacteristics;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00024CF8 File Offset: 0x00022EF8
		private static Characteristics GetCharacteristics(OutputKind outputKind, bool requires32Bit)
		{
			Characteristics characteristics = Characteristics.ExecutableImage;
			if (requires32Bit)
			{
				characteristics |= Characteristics.Bit32Machine;
			}
			else
			{
				characteristics |= Characteristics.LargeAddressAware;
			}
			switch (outputKind)
			{
			case OutputKind.ConsoleApplication:
			case OutputKind.WindowsApplication:
			case OutputKind.WindowsRuntimeApplication:
				break;
			case OutputKind.DynamicallyLinkedLibrary:
			case OutputKind.NetModule:
			case OutputKind.WindowsRuntimeMetadata:
				characteristics |= Characteristics.Dll;
				break;
			default:
				throw ExceptionUtilities.UnexpectedValue(outputKind);
			}
			return characteristics;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00024D50 File Offset: 0x00022F50
		private static Subsystem GetSubsystem(OutputKind outputKind)
		{
			switch (outputKind)
			{
			case OutputKind.ConsoleApplication:
			case OutputKind.DynamicallyLinkedLibrary:
			case OutputKind.NetModule:
			case OutputKind.WindowsRuntimeMetadata:
				return Subsystem.WindowsCui;
			case OutputKind.WindowsApplication:
			case OutputKind.WindowsRuntimeApplication:
				return Subsystem.WindowsGui;
			default:
				throw ExceptionUtilities.UnexpectedValue(outputKind);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000C20 RID: 3104
		internal abstract byte LinkerMajorVersion { get; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00024D81 File Offset: 0x00022F81
		internal bool HasStrongName
		{
			get
			{
				return !this.IsDelaySigned && this.Options.OutputKind != OutputKind.NetModule && this.StrongNameKeys.CanProvideStrongName;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00024DA6 File Offset: 0x00022FA6
		internal bool IsRealSigned
		{
			get
			{
				return !this.IsDelaySigned && !this.Options.PublicSign && this.Options.OutputKind != OutputKind.NetModule && this.StrongNameKeys.CanSign;
			}
		}

		// Token: 0x06000C23 RID: 3107
		internal abstract bool HasCodeToEmit();

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000C24 RID: 3108
		internal abstract bool IsDelaySigned { get; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000C25 RID: 3109
		internal abstract StrongNameKeys StrongNameKeys { get; }

		// Token: 0x06000C26 RID: 3110
		[NullableContext(2)]
		internal abstract CommonPEModuleBuilder CreateModuleBuilder([Nullable(1)] EmitOptions emitOptions, IMethodSymbol debugEntryPoint, Stream sourceLinkStream, [Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, CompilationTestData testData, [Nullable(1)] DiagnosticBag diagnostics, CancellationToken cancellationToken);

		// Token: 0x06000C27 RID: 3111
		internal abstract bool CompileMethods(CommonPEModuleBuilder moduleBuilder, bool emittingPdb, bool emitMetadataOnly, bool emitTestCoverageData, DiagnosticBag diagnostics, [Nullable(new byte[] { 2, 1 })] Predicate<ISymbolInternal> filterOpt, CancellationToken cancellationToken);

		// Token: 0x06000C28 RID: 3112 RVA: 0x00024DD8 File Offset: 0x00022FD8
		internal bool CreateDebugDocuments(DebugDocumentsBuilder documentsBuilder, IEnumerable<EmbeddedText> embeddedTexts, DiagnosticBag diagnostics)
		{
			bool flag = true;
			foreach (SyntaxTree syntaxTree in this.CommonSyntaxTrees)
			{
				if (!string.IsNullOrEmpty(syntaxTree.FilePath) && syntaxTree.GetText(default(CancellationToken)).Encoding == null)
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_EncodinglessSyntaxTree, syntaxTree.GetRoot(default(CancellationToken)).GetLocation()));
					flag = false;
				}
			}
			if (!flag)
			{
				return false;
			}
			if (!embeddedTexts.IsEmpty<EmbeddedText>())
			{
				using (IEnumerator<EmbeddedText> enumerator2 = embeddedTexts.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						EmbeddedText text = enumerator2.Current;
						string text3 = documentsBuilder.NormalizeDebugDocumentPath(text.FilePath, null);
						if (documentsBuilder.TryGetDebugDocumentForNormalizedPath(text3) == null)
						{
							DebugSourceDocument debugSourceDocument = new DebugSourceDocument(text3, this.DebugSourceDocumentLanguageId, () => text.GetDebugSourceInfo());
							documentsBuilder.AddDebugDocument(debugSourceDocument);
						}
					}
				}
			}
			ImmutableArray<SyntaxTree>.Enumerator enumerator = this.CommonSyntaxTrees.GetEnumerator();
			while (enumerator.MoveNext())
			{
				SyntaxTree tree = enumerator.Current;
				if (!string.IsNullOrEmpty(tree.FilePath))
				{
					string text2 = documentsBuilder.NormalizeDebugDocumentPath(tree.FilePath, null);
					if (documentsBuilder.TryGetDebugDocumentForNormalizedPath(text2) == null)
					{
						documentsBuilder.AddDebugDocument(new DebugSourceDocument(text2, this.DebugSourceDocumentLanguageId, () => tree.GetDebugSourceInfo()));
					}
				}
			}
			foreach (SyntaxTree syntaxTree2 in this.CommonSyntaxTrees)
			{
				this.AddDebugSourceDocumentsForChecksumDirectives(documentsBuilder, syntaxTree2, diagnostics);
			}
			return true;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000C29 RID: 3113
		internal abstract Guid DebugSourceDocumentLanguageId { get; }

		// Token: 0x06000C2A RID: 3114
		internal abstract void AddDebugSourceDocumentsForChecksumDirectives(DebugDocumentsBuilder documentsBuilder, SyntaxTree tree, DiagnosticBag diagnostics);

		// Token: 0x06000C2B RID: 3115
		[NullableContext(2)]
		internal abstract bool GenerateResourcesAndDocumentationComments([Nullable(1)] CommonPEModuleBuilder moduleBeingBuilt, Stream xmlDocumentationStream, Stream win32ResourcesStream, bool useRawWin32Resources, string outputNameOverride, [Nullable(1)] DiagnosticBag diagnostics, CancellationToken cancellationToken);

		// Token: 0x06000C2C RID: 3116
		internal abstract void ReportUnusedImports(DiagnosticBag diagnostics, CancellationToken cancellationToken);

		// Token: 0x06000C2D RID: 3117 RVA: 0x00024FA8 File Offset: 0x000231A8
		internal static bool ReportUnusedImportsInTree(SyntaxTree tree)
		{
			return tree.Options.DocumentationMode > DocumentationMode.None;
		}

		// Token: 0x06000C2E RID: 3118
		[NullableContext(2)]
		internal abstract void CompleteTrees(SyntaxTree filterTree);

		// Token: 0x06000C2F RID: 3119 RVA: 0x00024FB8 File Offset: 0x000231B8
		internal bool Compile(CommonPEModuleBuilder moduleBuilder, bool emittingPdb, DiagnosticBag diagnostics, [Nullable(new byte[] { 2, 1 })] Predicate<ISymbolInternal> filterOpt, CancellationToken cancellationToken)
		{
			bool flag;
			try
			{
				flag = this.CompileMethods(moduleBuilder, emittingPdb, false, false, diagnostics, filterOpt, cancellationToken);
			}
			finally
			{
				moduleBuilder.CompilationFinished();
			}
			return flag;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00024FF0 File Offset: 0x000231F0
		internal void EnsureAnonymousTypeTemplates(CancellationToken cancellationToken)
		{
			if (this.GetSubmissionSlotIndex() >= 0 && this.HasCodeToEmit())
			{
				if (!this.CommonAnonymousTypeManager.AreTemplatesSealed)
				{
					DiagnosticBag instance = DiagnosticBag.GetInstance();
					CommonPEModuleBuilder commonPEModuleBuilder = this.CreateModuleBuilder(EmitOptions.Default, null, null, null, null, null, instance, cancellationToken);
					if (commonPEModuleBuilder != null)
					{
						this.Compile(commonPEModuleBuilder, false, instance, null, cancellationToken);
					}
					instance.Free();
					return;
				}
			}
			else
			{
				ScriptCompilationInfo scriptCompilationInfo = this.ScriptCompilationInfo;
				if (scriptCompilationInfo == null)
				{
					return;
				}
				Compilation previousScriptCompilation = scriptCompilationInfo.PreviousScriptCompilation;
				if (previousScriptCompilation == null)
				{
					return;
				}
				previousScriptCompilation.EnsureAnonymousTypeTemplates(cancellationToken);
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00025068 File Offset: 0x00023268
		[EditorBrowsable(1)]
		public EmitResult Emit(Stream peStream, [Nullable(2)] Stream pdbStream, [Nullable(2)] Stream xmlDocumentationStream, [Nullable(2)] Stream win32Resources, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, EmitOptions options, CancellationToken cancellationToken)
		{
			return this.Emit(peStream, pdbStream, xmlDocumentationStream, win32Resources, manifestResources, options, null, null, null, cancellationToken);
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002508C File Offset: 0x0002328C
		[EditorBrowsable(1)]
		public EmitResult Emit(Stream peStream, Stream pdbStream, Stream xmlDocumentationStream, Stream win32Resources, IEnumerable<ResourceDescription> manifestResources, EmitOptions options, IMethodSymbol debugEntryPoint, CancellationToken cancellationToken)
		{
			return this.Emit(peStream, pdbStream, xmlDocumentationStream, win32Resources, manifestResources, options, debugEntryPoint, null, null, cancellationToken);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000250B0 File Offset: 0x000232B0
		[NullableContext(2)]
		[return: Nullable(1)]
		public EmitResult Emit([Nullable(1)] Stream peStream, Stream pdbStream, Stream xmlDocumentationStream, Stream win32Resources, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, [Nullable(1)] EmitOptions options, IMethodSymbol debugEntryPoint, Stream sourceLinkStream, [Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts, CancellationToken cancellationToken)
		{
			return this.Emit(peStream, pdbStream, xmlDocumentationStream, win32Resources, manifestResources, options, debugEntryPoint, sourceLinkStream, embeddedTexts, null, cancellationToken);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000250D8 File Offset: 0x000232D8
		[NullableContext(2)]
		[return: Nullable(1)]
		public EmitResult Emit([Nullable(1)] Stream peStream, Stream pdbStream = null, Stream xmlDocumentationStream = null, Stream win32Resources = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources = null, EmitOptions options = null, IMethodSymbol debugEntryPoint = null, Stream sourceLinkStream = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts = null, Stream metadataPEStream = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.Emit(peStream, pdbStream, xmlDocumentationStream, win32Resources, manifestResources, options, debugEntryPoint, sourceLinkStream, embeddedTexts, metadataPEStream, null, cancellationToken);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00025100 File Offset: 0x00023300
		[NullableContext(2)]
		[return: Nullable(1)]
		internal EmitResult Emit([Nullable(1)] Stream peStream, Stream pdbStream, Stream xmlDocumentationStream, Stream win32Resources, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, EmitOptions options, IMethodSymbol debugEntryPoint, Stream sourceLinkStream, [Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts, Stream metadataPEStream, RebuildData rebuildData, CancellationToken cancellationToken)
		{
			if (peStream == null)
			{
				throw new ArgumentNullException("peStream");
			}
			if (!peStream.CanWrite)
			{
				throw new ArgumentException(CodeAnalysisResources.StreamMustSupportWrite, "peStream");
			}
			if (pdbStream != null)
			{
				if (options != null && options.DebugInformationFormat == DebugInformationFormat.Embedded)
				{
					throw new ArgumentException(CodeAnalysisResources.PdbStreamUnexpectedWhenEmbedding, "pdbStream");
				}
				if (!pdbStream.CanWrite)
				{
					throw new ArgumentException(CodeAnalysisResources.StreamMustSupportWrite, "pdbStream");
				}
				if (options != null && options.EmitMetadataOnly)
				{
					throw new ArgumentException(CodeAnalysisResources.PdbStreamUnexpectedWhenEmittingMetadataOnly, "pdbStream");
				}
			}
			if (metadataPEStream != null && options != null && options.EmitMetadataOnly)
			{
				throw new ArgumentException(CodeAnalysisResources.MetadataPeStreamUnexpectedWhenEmittingMetadataOnly, "metadataPEStream");
			}
			if (metadataPEStream != null && options != null && options.IncludePrivateMembers)
			{
				throw new ArgumentException(CodeAnalysisResources.IncludingPrivateMembersUnexpectedWhenEmittingToMetadataPeStream, "metadataPEStream");
			}
			if (metadataPEStream == null && options != null && !options.EmitMetadataOnly)
			{
				options = options.WithIncludePrivateMembers(true);
			}
			if (options != null && options.DebugInformationFormat == DebugInformationFormat.Embedded && options != null && options.EmitMetadataOnly)
			{
				throw new ArgumentException(CodeAnalysisResources.EmbeddingPdbUnexpectedWhenEmittingMetadata, "metadataPEStream");
			}
			if (this.Options.OutputKind == OutputKind.NetModule)
			{
				if (metadataPEStream != null)
				{
					throw new ArgumentException(CodeAnalysisResources.CannotTargetNetModuleWhenEmittingRefAssembly, "metadataPEStream");
				}
				if (options != null && options.EmitMetadataOnly)
				{
					throw new ArgumentException(CodeAnalysisResources.CannotTargetNetModuleWhenEmittingRefAssembly, "EmitMetadataOnly");
				}
			}
			if (win32Resources != null && (!win32Resources.CanRead || !win32Resources.CanSeek))
			{
				throw new ArgumentException(CodeAnalysisResources.StreamMustSupportReadAndSeek, "win32Resources");
			}
			if (sourceLinkStream != null && !sourceLinkStream.CanRead)
			{
				throw new ArgumentException(CodeAnalysisResources.StreamMustSupportRead, "sourceLinkStream");
			}
			if (embeddedTexts != null && !embeddedTexts.IsEmpty<EmbeddedText>() && pdbStream == null && (options == null || options.DebugInformationFormat != DebugInformationFormat.Embedded))
			{
				throw new ArgumentException(CodeAnalysisResources.EmbeddedTextsRequirePdb, "embeddedTexts");
			}
			return this.Emit(peStream, metadataPEStream, pdbStream, xmlDocumentationStream, win32Resources, manifestResources, options, debugEntryPoint, sourceLinkStream, embeddedTexts, rebuildData, null, cancellationToken);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000252E8 File Offset: 0x000234E8
		[NullableContext(2)]
		[return: Nullable(1)]
		internal EmitResult Emit([Nullable(1)] Stream peStream, Stream metadataPEStream, Stream pdbStream, Stream xmlDocumentationStream, Stream win32Resources, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, EmitOptions options, IMethodSymbol debugEntryPoint, Stream sourceLinkStream, [Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts, RebuildData rebuildData, CompilationTestData testData, CancellationToken cancellationToken)
		{
			options = options ?? EmitOptions.Default.WithIncludePrivateMembers(metadataPEStream == null);
			bool flag = options.DebugInformationFormat == DebugInformationFormat.Embedded;
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			CommonPEModuleBuilder commonPEModuleBuilder = this.CheckOptionsAndCreateModuleBuilder(instance, manifestResources, options, debugEntryPoint, sourceLinkStream, embeddedTexts, testData, cancellationToken);
			bool flag2 = false;
			if (commonPEModuleBuilder != null)
			{
				try
				{
					flag2 = this.CompileMethods(commonPEModuleBuilder, pdbStream != null || flag, options.EmitMetadataOnly, options.EmitTestCoverageData, instance, null, cancellationToken);
					if (!options.EmitMetadataOnly)
					{
						if (!this.GenerateResourcesAndDocumentationComments(commonPEModuleBuilder, xmlDocumentationStream, win32Resources, rebuildData != null, options.OutputNameOverride, instance, cancellationToken))
						{
							flag2 = false;
						}
						if (flag2)
						{
							this.ReportUnusedImports(instance, cancellationToken);
						}
					}
				}
				finally
				{
					commonPEModuleBuilder.CompilationFinished();
				}
				RSAParameters? rsaparameters = default(RSAParameters?);
				if (this.Options.StrongNameProvider != null && this.SignUsingBuilder && !this.Options.PublicSign)
				{
					rsaparameters = this.StrongNameKeys.PrivateKey;
				}
				if (!options.EmitMetadataOnly && CommonCompiler.HasUnsuppressedErrors(instance))
				{
					flag2 = false;
				}
				if (flag2)
				{
					flag2 = this.SerializeToPeStream(commonPEModuleBuilder, new Compilation.SimpleEmitStreamProvider(peStream), (metadataPEStream != null) ? new Compilation.SimpleEmitStreamProvider(metadataPEStream) : null, (pdbStream != null) ? new Compilation.SimpleEmitStreamProvider(pdbStream) : null, rebuildData, (testData != null) ? testData.SymWriterFactory : null, instance, options, rsaparameters, cancellationToken);
				}
			}
			return new EmitResult(flag2, instance.ToReadOnlyAndFree());
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0002543C File Offset: 0x0002363C
		public EmitDifferenceResult EmitDifference(EmitBaseline baseline, IEnumerable<SemanticEdit> edits, Stream metadataStream, Stream ilStream, Stream pdbStream, ICollection<MethodDefinitionHandle> updatedMethods, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.EmitDifference(baseline, edits, (ISymbol s) => false, metadataStream, ilStream, pdbStream, updatedMethods, cancellationToken);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002547C File Offset: 0x0002367C
		public EmitDifferenceResult EmitDifference(EmitBaseline baseline, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol, Stream metadataStream, Stream ilStream, Stream pdbStream, ICollection<MethodDefinitionHandle> updatedMethods, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (baseline == null)
			{
				throw new ArgumentNullException("baseline");
			}
			if (edits == null)
			{
				throw new ArgumentNullException("edits");
			}
			if (isAddedSymbol == null)
			{
				throw new ArgumentNullException("isAddedSymbol");
			}
			if (metadataStream == null)
			{
				throw new ArgumentNullException("metadataStream");
			}
			if (ilStream == null)
			{
				throw new ArgumentNullException("ilStream");
			}
			if (pdbStream == null)
			{
				throw new ArgumentNullException("pdbStream");
			}
			return this.EmitDifference(baseline, edits, isAddedSymbol, metadataStream, ilStream, pdbStream, updatedMethods, null, cancellationToken);
		}

		// Token: 0x06000C39 RID: 3129
		internal abstract EmitDifferenceResult EmitDifference(EmitBaseline baseline, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol, Stream metadataStream, Stream ilStream, Stream pdbStream, ICollection<MethodDefinitionHandle> updatedMethodHandles, [Nullable(2)] CompilationTestData testData, CancellationToken cancellationToken);

		// Token: 0x06000C3A RID: 3130 RVA: 0x000254F4 File Offset: 0x000236F4
		[NullableContext(2)]
		internal CommonPEModuleBuilder CheckOptionsAndCreateModuleBuilder([Nullable(1)] DiagnosticBag diagnostics, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources, [Nullable(1)] EmitOptions options, IMethodSymbol debugEntryPoint, Stream sourceLinkStream, [Nullable(new byte[] { 2, 1 })] IEnumerable<EmbeddedText> embeddedTexts, CompilationTestData testData, CancellationToken cancellationToken)
		{
			options.ValidateOptions(diagnostics, this.MessageProvider, this.Options.Deterministic);
			if (debugEntryPoint != null)
			{
				this.ValidateDebugEntryPoint(debugEntryPoint, diagnostics);
			}
			if (this.Options.OutputKind == OutputKind.NetModule && manifestResources != null)
			{
				using (IEnumerator<ResourceDescription> enumerator = manifestResources.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.FileName != null)
						{
							diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_ResourceInModule, Location.None));
						}
					}
				}
			}
			if (CommonCompiler.HasUnsuppressableErrors(diagnostics))
			{
				return null;
			}
			if (this.IsSubmission && !this.HasCodeToEmit())
			{
				diagnostics.AddRange<Diagnostic>(this.GetDiagnostics(cancellationToken));
				return null;
			}
			return this.CreateModuleBuilder(options, debugEntryPoint, sourceLinkStream, embeddedTexts, manifestResources, testData, diagnostics, cancellationToken);
		}

		// Token: 0x06000C3B RID: 3131
		internal abstract void ValidateDebugEntryPoint(IMethodSymbol debugEntryPoint, DiagnosticBag diagnostics);

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000255D0 File Offset: 0x000237D0
		internal bool IsEmitDeterministic
		{
			get
			{
				return this.Options.Deterministic;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000255E0 File Offset: 0x000237E0
		internal bool SerializeToPeStream(CommonPEModuleBuilder moduleBeingBuilt, Compilation.EmitStreamProvider peStreamProvider, [Nullable(2)] Compilation.EmitStreamProvider metadataPEStreamProvider, [Nullable(2)] Compilation.EmitStreamProvider pdbStreamProvider, [Nullable(2)] RebuildData rebuildData, [Nullable(new byte[] { 2, 1, 1 })] Func<ISymWriterMetadataProvider, SymUnmanagedWriter> testSymWriterFactory, DiagnosticBag diagnostics, EmitOptions emitOptions, RSAParameters? privateKeyOpt, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			PdbWriter pdbWriter = null;
			DiagnosticBag metadataDiagnostics = null;
			DiagnosticBag diagnosticBag = null;
			bool isEmitDeterministic = this.IsEmitDeterministic;
			string text = emitOptions.PdbFilePath;
			if (moduleBeingBuilt.DebugInformationFormat == DebugInformationFormat.Embedded || pdbStreamProvider != null)
			{
				text = text ?? FileNameUtilities.ChangeExtension(this.SourceModule.Name, "pdb");
			}
			else
			{
				text = null;
			}
			if (moduleBeingBuilt.DebugInformationFormat == DebugInformationFormat.Embedded && !RoslynString.IsNullOrEmpty(text))
			{
				text = PathUtilities.GetFileName(text, true);
			}
			Compilation.EmitStream emitStream = null;
			Compilation.EmitStream emitStream2 = null;
			bool flag;
			try
			{
				Compilation.EmitStreamSignKind emitStreamSignKind = (this.IsRealSigned ? (this.SignUsingBuilder ? Compilation.EmitStreamSignKind.SignedWithBuilder : Compilation.EmitStreamSignKind.SignedWithFile) : Compilation.EmitStreamSignKind.None);
				emitStream = new Compilation.EmitStream(peStreamProvider, emitStreamSignKind, this.Options.StrongNameProvider);
				emitStream2 = ((metadataPEStreamProvider == null) ? null : new Compilation.EmitStream(metadataPEStreamProvider, emitStreamSignKind, this.Options.StrongNameProvider));
				metadataDiagnostics = DiagnosticBag.GetInstance();
				if (moduleBeingBuilt.DebugInformationFormat == DebugInformationFormat.Pdb && pdbStreamProvider != null)
				{
					pdbWriter = new PdbWriter(text, testSymWriterFactory, isEmitDeterministic ? moduleBeingBuilt.PdbChecksumAlgorithm : default(HashAlgorithmName));
				}
				Func<Stream> func = ((moduleBeingBuilt.DebugInformationFormat != DebugInformationFormat.PortablePdb || pdbStreamProvider == null) ? null : (() => Compilation.ConditionalGetOrCreateStream(pdbStreamProvider, metadataDiagnostics)));
				try
				{
					if (Compilation.SerializePeToStream(moduleBeingBuilt, metadataDiagnostics, this.MessageProvider, emitStream.GetCreateStreamFunc(metadataDiagnostics), (emitStream2 != null) ? emitStream2.GetCreateStreamFunc(metadataDiagnostics) : null, func, pdbWriter, text, rebuildData, emitOptions.EmitMetadataOnly, emitOptions.IncludePrivateMembers, isEmitDeterministic, emitOptions.EmitTestCoverageData, privateKeyOpt, cancellationToken) && pdbWriter != null)
					{
						Stream orCreateStream = pdbStreamProvider.GetOrCreateStream(metadataDiagnostics);
						if (orCreateStream != null)
						{
							pdbWriter.WriteTo(orCreateStream);
						}
					}
				}
				catch (SymUnmanagedWriterException ex)
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_PdbWritingFailed, Location.None, new object[] { ex.Message }));
					return false;
				}
				catch (PeWritingException ex2)
				{
					CommonMessageProvider messageProvider = this.MessageProvider;
					int err_PeWritingFailure = this.MessageProvider.ERR_PeWritingFailure;
					Location none = Location.None;
					object[] array = new object[1];
					int num = 0;
					Exception innerException = ex2.InnerException;
					array[num] = ((innerException != null) ? innerException.ToString() : null) ?? "";
					diagnostics.Add(messageProvider.CreateDiagnostic(err_PeWritingFailure, none, array));
					return false;
				}
				catch (ResourceException ex3)
				{
					CommonMessageProvider messageProvider2 = this.MessageProvider;
					int err_CantReadResource = this.MessageProvider.ERR_CantReadResource;
					Location none2 = Location.None;
					object[] array2 = new object[2];
					array2[0] = ex3.Message;
					int num2 = 1;
					Exception innerException2 = ex3.InnerException;
					array2[num2] = ((innerException2 != null) ? innerException2.Message : null) ?? "";
					diagnostics.Add(messageProvider2.CreateDiagnostic(err_CantReadResource, none2, array2));
					return false;
				}
				catch (PermissionSetFileReadException ex4)
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_PermissionSetAttributeFileReadError, Location.None, new object[] { ex4.FileName, ex4.PropertyName, ex4.Message }));
					return false;
				}
				if (!this.FilterAndAppendAndFreeDiagnostics(diagnostics, ref metadataDiagnostics, cancellationToken))
				{
					flag = false;
				}
				else
				{
					flag = emitStream.Complete(this.StrongNameKeys, this.MessageProvider, diagnostics) && (emitStream2 == null || emitStream2.Complete(this.StrongNameKeys, this.MessageProvider, diagnostics));
				}
			}
			finally
			{
				if (pdbWriter != null)
				{
					pdbWriter.Dispose();
				}
				if (emitStream != null)
				{
					emitStream.Close();
				}
				if (emitStream2 != null)
				{
					emitStream2.Close();
				}
				if (diagnosticBag != null)
				{
					diagnosticBag.Free();
				}
				DiagnosticBag metadataDiagnostics2 = metadataDiagnostics;
				if (metadataDiagnostics2 != null)
				{
					metadataDiagnostics2.Free();
				}
			}
			return flag;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x000259D8 File Offset: 0x00023BD8
		[return: Nullable(2)]
		private static Stream ConditionalGetOrCreateStream(Compilation.EmitStreamProvider metadataPEStreamProvider, DiagnosticBag metadataDiagnostics)
		{
			if (metadataDiagnostics.HasAnyErrors())
			{
				return null;
			}
			return metadataPEStreamProvider.GetOrCreateStream(metadataDiagnostics);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000259EC File Offset: 0x00023BEC
		[NullableContext(2)]
		internal static bool SerializePeToStream([Nullable(1)] CommonPEModuleBuilder moduleBeingBuilt, [Nullable(1)] DiagnosticBag metadataDiagnostics, [Nullable(1)] CommonMessageProvider messageProvider, [Nullable(new byte[] { 1, 2 })] Func<Stream> getPeStream, Func<Stream> getMetadataPeStreamOpt, Func<Stream> getPortablePdbStreamOpt, PdbWriter nativePdbWriterOpt, string pdbPathOpt, RebuildData rebuildData, bool metadataOnly, bool includePrivateMembers, bool isDeterministic, bool emitTestCoverageData, RSAParameters? privateKeyOpt, CancellationToken cancellationToken)
		{
			bool flag = getMetadataPeStreamOpt != null;
			bool flag2 = !metadataOnly || includePrivateMembers;
			bool flag3 = (metadataOnly && !includePrivateMembers) || isDeterministic;
			return PeWriter.WritePeToStream(new EmitContext(moduleBeingBuilt, metadataDiagnostics, metadataOnly, flag2, null, rebuildData), messageProvider, getPeStream, getPortablePdbStreamOpt, nativePdbWriterOpt, pdbPathOpt, metadataOnly, flag3, emitTestCoverageData, privateKeyOpt, cancellationToken) && (!flag || PeWriter.WritePeToStream(new EmitContext(moduleBeingBuilt, null, metadataDiagnostics, true, false), messageProvider, getMetadataPeStreamOpt, null, null, null, true, true, false, privateKeyOpt, cancellationToken));
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00025A64 File Offset: 0x00023C64
		[return: Nullable(2)]
		internal EmitBaseline SerializeToDeltaStreams(CommonPEModuleBuilder moduleBeingBuilt, EmitBaseline baseline, DefinitionMap definitionMap, SymbolChanges changes, Stream metadataStream, Stream ilStream, Stream pdbStream, ICollection<MethodDefinitionHandle> updatedMethods, DiagnosticBag diagnostics, [Nullable(new byte[] { 2, 1, 1 })] Func<ISymWriterMetadataProvider, SymUnmanagedWriter> testSymWriterFactory, [Nullable(2)] string pdbFilePath, CancellationToken cancellationToken)
		{
			PdbWriter pdbWriter = ((moduleBeingBuilt.DebugInformationFormat != DebugInformationFormat.Pdb) ? null : new PdbWriter(pdbFilePath ?? FileNameUtilities.ChangeExtension(this.SourceModule.Name, "pdb"), testSymWriterFactory, default(HashAlgorithmName)));
			EmitBaseline emitBaseline;
			using (pdbWriter)
			{
				EmitContext emitContext = new EmitContext(moduleBeingBuilt, diagnostics, false, true, null, null);
				Guid guid = Guid.NewGuid();
				try
				{
					DeltaMetadataWriter deltaMetadataWriter = new DeltaMetadataWriter(emitContext, this.MessageProvider, baseline, guid, definitionMap, changes, cancellationToken);
					MetadataSizes metadataSizes;
					deltaMetadataWriter.WriteMetadataAndIL(pdbWriter, metadataStream, ilStream, (pdbWriter == null) ? pdbStream : null, out metadataSizes);
					deltaMetadataWriter.GetMethodTokens(updatedMethods);
					if (pdbWriter != null)
					{
						pdbWriter.WriteTo(pdbStream);
					}
					emitBaseline = (diagnostics.HasAnyErrors() ? null : deltaMetadataWriter.GetDelta(baseline, this, guid, metadataSizes));
				}
				catch (SymUnmanagedWriterException ex)
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_PdbWritingFailed, Location.None, new object[] { ex.Message }));
					emitBaseline = null;
				}
				catch (PeWritingException ex2)
				{
					CommonMessageProvider messageProvider = this.MessageProvider;
					int err_PeWritingFailure = this.MessageProvider.ERR_PeWritingFailure;
					Location none = Location.None;
					object[] array = new object[1];
					int num = 0;
					Exception innerException = ex2.InnerException;
					array[num] = ((innerException != null) ? innerException.ToString() : null) ?? "";
					diagnostics.Add(messageProvider.CreateDiagnostic(err_PeWritingFailure, none, array));
					emitBaseline = null;
				}
				catch (PermissionSetFileReadException ex3)
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_PermissionSetAttributeFileReadError, Location.None, new object[] { ex3.FileName, ex3.PropertyName, ex3.Message }));
					emitBaseline = null;
				}
			}
			return emitBaseline;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00025C68 File Offset: 0x00023E68
		[return: Nullable(2)]
		internal string Feature(string p)
		{
			string text;
			if (!this._features.TryGetValue(p, ref text))
			{
				return null;
			}
			return text;
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x00025C88 File Offset: 0x00023E88
		private ConcurrentDictionary<SyntaxTree, SmallConcurrentSetOfInts> TreeToUsedImportDirectivesMap
		{
			get
			{
				return RoslynLazyInitializer.EnsureInitialized<ConcurrentDictionary<SyntaxTree, SmallConcurrentSetOfInts>>(ref this._lazyTreeToUsedImportDirectivesMap);
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00025C98 File Offset: 0x00023E98
		internal void MarkImportDirectiveAsUsed(SyntaxReference node)
		{
			this.MarkImportDirectiveAsUsed(node.SyntaxTree, node.Span.Start);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00025CBF File Offset: 0x00023EBF
		[NullableContext(2)]
		internal void MarkImportDirectiveAsUsed(SyntaxTree syntaxTree, int position)
		{
			if (!this.IsSubmission && syntaxTree != null)
			{
				this.TreeToUsedImportDirectivesMap.GetOrAdd(syntaxTree, Compilation.s_createSetCallback).Add(position);
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00025CE4 File Offset: 0x00023EE4
		internal bool IsImportDirectiveUsed(SyntaxTree syntaxTree, int position)
		{
			SmallConcurrentSetOfInts smallConcurrentSetOfInts;
			return this.IsSubmission || (syntaxTree != null && this.TreeToUsedImportDirectivesMap.TryGetValue(syntaxTree, ref smallConcurrentSetOfInts) && smallConcurrentSetOfInts.Contains(position));
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00025D17 File Offset: 0x00023F17
		internal int CompareSyntaxTreeOrdering(SyntaxTree tree1, SyntaxTree tree2)
		{
			if (tree1 == tree2)
			{
				return 0;
			}
			return this.GetSyntaxTreeOrdinal(tree1) - this.GetSyntaxTreeOrdinal(tree2);
		}

		// Token: 0x06000C47 RID: 3143
		internal abstract int GetSyntaxTreeOrdinal(SyntaxTree tree);

		// Token: 0x06000C48 RID: 3144
		internal abstract int CompareSourceLocations(Location loc1, Location loc2);

		// Token: 0x06000C49 RID: 3145
		internal abstract int CompareSourceLocations(SyntaxReference loc1, SyntaxReference loc2);

		// Token: 0x06000C4A RID: 3146 RVA: 0x00025D2E File Offset: 0x00023F2E
		internal TLocation FirstSourceLocation<[Nullable(0)] TLocation>(TLocation first, TLocation second) where TLocation : Location
		{
			if (this.CompareSourceLocations(first, second) <= 0)
			{
				return first;
			}
			return second;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00025D48 File Offset: 0x00023F48
		[NullableContext(0)]
		[return: Nullable(2)]
		internal TLocation FirstSourceLocation<TLocation>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TLocation> locations) where TLocation : Location
		{
			if (locations.IsEmpty)
			{
				return default(TLocation);
			}
			TLocation tlocation = locations[0];
			for (int i = 1; i < locations.Length; i++)
			{
				tlocation = this.FirstSourceLocation<TLocation>(tlocation, locations[i]);
			}
			return tlocation;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00025D94 File Offset: 0x00023F94
		internal string GetMessage(CompilationStage stage)
		{
			return string.Format("{0} ({1})", this.AssemblyName, stage.ToString());
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00025DB4 File Offset: 0x00023FB4
		internal string GetMessage(ITypeSymbol source, ITypeSymbol destination)
		{
			if (source == null || destination == null)
			{
				return this.AssemblyName ?? "";
			}
			return string.Format("{0}: {1} {2} -> {3} {4}", new object[]
			{
				this.AssemblyName,
				source.TypeKind.ToString(),
				source.Name,
				destination.TypeKind.ToString(),
				destination.Name
			});
		}

		// Token: 0x06000C4E RID: 3150
		public abstract bool ContainsSymbolsWithName(Func<string, bool> predicate, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C4F RID: 3151
		public abstract IEnumerable<ISymbol> GetSymbolsWithName(Func<string, bool> predicate, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C50 RID: 3152
		public abstract bool ContainsSymbolsWithName(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C51 RID: 3153
		public abstract IEnumerable<ISymbol> GetSymbolsWithName(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06000C52 RID: 3154 RVA: 0x00025E30 File Offset: 0x00024030
		internal void MakeMemberMissing(WellKnownMember member)
		{
			this.MakeMemberMissing((int)member);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00025E39 File Offset: 0x00024039
		internal void MakeMemberMissing(SpecialMember member)
		{
			this.MakeMemberMissing(-(int)member - SpecialMember.System_String__ConcatStringString);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00025E45 File Offset: 0x00024045
		internal bool IsMemberMissing(WellKnownMember member)
		{
			return this.IsMemberMissing((int)member);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00025E4E File Offset: 0x0002404E
		internal bool IsMemberMissing(SpecialMember member)
		{
			return this.IsMemberMissing(-(int)member - SpecialMember.System_String__ConcatStringString);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00025E5A File Offset: 0x0002405A
		private void MakeMemberMissing(int member)
		{
			if (this._lazyMakeMemberMissingMap == null)
			{
				this._lazyMakeMemberMissingMap = new SmallDictionary<int, bool>();
			}
			this._lazyMakeMemberMissingMap[member] = true;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00025E7C File Offset: 0x0002407C
		private bool IsMemberMissing(int member)
		{
			return this._lazyMakeMemberMissingMap != null && this._lazyMakeMemberMissingMap.ContainsKey(member);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00025E94 File Offset: 0x00024094
		internal void MakeTypeMissing(SpecialType type)
		{
			this.MakeTypeMissing((int)type);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00025E9D File Offset: 0x0002409D
		internal void MakeTypeMissing(WellKnownType type)
		{
			this.MakeTypeMissing((int)type);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00025EA6 File Offset: 0x000240A6
		private void MakeTypeMissing(int type)
		{
			if (this._lazyMakeWellKnownTypeMissingMap == null)
			{
				this._lazyMakeWellKnownTypeMissingMap = new SmallDictionary<int, bool>();
			}
			this._lazyMakeWellKnownTypeMissingMap[type] = true;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00025EC8 File Offset: 0x000240C8
		internal bool IsTypeMissing(SpecialType type)
		{
			return this.IsTypeMissing((int)type);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00025ED1 File Offset: 0x000240D1
		internal bool IsTypeMissing(WellKnownType type)
		{
			return this.IsTypeMissing((int)type);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00025EDA File Offset: 0x000240DA
		private bool IsTypeMissing(int type)
		{
			return this._lazyMakeWellKnownTypeMissingMap != null && this._lazyMakeWellKnownTypeMissingMap.ContainsKey(type);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00025EF4 File Offset: 0x000240F4
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<AssemblyIdentity> GetUnreferencedAssemblyIdentities(Diagnostic diagnostic)
		{
			if (diagnostic == null)
			{
				throw new ArgumentNullException("diagnostic");
			}
			if (!this.IsUnreferencedAssemblyIdentityDiagnosticCode(diagnostic.Code))
			{
				return ImmutableArray<AssemblyIdentity>.Empty;
			}
			ArrayBuilder<AssemblyIdentity> instance = ArrayBuilder<AssemblyIdentity>.GetInstance();
			foreach (object obj in diagnostic.Arguments)
			{
				AssemblyIdentity assemblyIdentity = obj as AssemblyIdentity;
				if (assemblyIdentity != null)
				{
					instance.Add(assemblyIdentity);
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000C5F RID: 3167
		internal abstract bool IsUnreferencedAssemblyIdentityDiagnosticCode(int code);

		// Token: 0x06000C60 RID: 3168 RVA: 0x00025F78 File Offset: 0x00024178
		[return: Nullable(2)]
		public static string GetRequiredLanguageVersion(Diagnostic diagnostic)
		{
			if (diagnostic == null)
			{
				throw new ArgumentNullException("diagnostic");
			}
			string text = null;
			if (diagnostic.Arguments != null)
			{
				foreach (object obj in diagnostic.Arguments)
				{
					RequiredLanguageVersion requiredLanguageVersion = obj as RequiredLanguageVersion;
					if (requiredLanguageVersion != null)
					{
						text = requiredLanguageVersion.ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00025FE8 File Offset: 0x000241E8
		internal void CacheRetargetingAssemblySymbolNoLock(IAssemblySymbolInternal assembly)
		{
			this._retargetingAssemblySymbols.Add(assembly);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00025FF8 File Offset: 0x000241F8
		internal void AddRetargetingAssemblySymbolsNoLock<[Nullable(0)] T>(List<T> result) where T : IAssemblySymbolInternal
		{
			foreach (IAssemblySymbolInternal assemblySymbolInternal in this._retargetingAssemblySymbols)
			{
				result.Add((T)((object)assemblySymbolInternal));
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002604C File Offset: 0x0002424C
		internal WeakList<IAssemblySymbolInternal> RetargetingAssemblySymbols
		{
			get
			{
				return this._retargetingAssemblySymbols;
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002606B File Offset: 0x0002426B
		[CompilerGenerated]
		private void <IsSymbolAccessibleWithin>g__checkInCompilationReferences|185_0(ISymbol s, string parameterName)
		{
			if (!this.<IsSymbolAccessibleWithin>g__isContainingAssemblyInReferences|185_3(s))
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.IsSymbolAccessibleWrongAssembly, parameterName), parameterName);
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00026088 File Offset: 0x00024288
		[CompilerGenerated]
		private bool <IsSymbolAccessibleWithin>g__assemblyIsInReferences|185_1(IAssemblySymbol a)
		{
			if (Compilation.<IsSymbolAccessibleWithin>g__assemblyIsInCompilationReferences|185_2(a, this))
			{
				return true;
			}
			if (this.IsSubmission)
			{
				for (Compilation compilation = this.PreviousSubmission; compilation != null; compilation = compilation.PreviousSubmission)
				{
					if (Compilation.<IsSymbolAccessibleWithin>g__assemblyIsInCompilationReferences|185_2(a, compilation))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000260C8 File Offset: 0x000242C8
		[CompilerGenerated]
		internal static bool <IsSymbolAccessibleWithin>g__assemblyIsInCompilationReferences|185_2(IAssemblySymbol a, Compilation compilation)
		{
			if (a.Equals(compilation.Assembly))
			{
				return true;
			}
			foreach (MetadataReference metadataReference in compilation.References)
			{
				if (a.Equals(compilation.GetAssemblyOrModuleSymbol(metadataReference)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00026134 File Offset: 0x00024334
		[CompilerGenerated]
		private bool <IsSymbolAccessibleWithin>g__isContainingAssemblyInReferences|185_3(ISymbol s)
		{
			for (;;)
			{
				SymbolKind kind = s.Kind;
				switch (kind)
				{
				case SymbolKind.Alias:
					s = ((IAliasSymbol)s).Target;
					break;
				case SymbolKind.ArrayType:
					s = ((IArrayTypeSymbol)s).ElementType;
					break;
				case SymbolKind.Assembly:
					goto IL_0053;
				case SymbolKind.DynamicType:
				case SymbolKind.ErrorType:
					goto IL_00F9;
				default:
					switch (kind)
					{
					case SymbolKind.Namespace:
					case SymbolKind.Preprocessing:
						goto IL_00F9;
					case SymbolKind.PointerType:
						s = ((IPointerTypeSymbol)s).PointedAtType;
						continue;
					case SymbolKind.Discard:
						s = ((IDiscardSymbol)s).Type;
						continue;
					case SymbolKind.FunctionPointerType:
						goto IL_00A2;
					}
					goto Block_1;
				}
			}
			Block_1:
			goto IL_0110;
			IL_0053:
			return this.<IsSymbolAccessibleWithin>g__assemblyIsInReferences|185_1((IAssemblySymbol)s);
			IL_00A2:
			IFunctionPointerTypeSymbol functionPointerTypeSymbol = (IFunctionPointerTypeSymbol)s;
			if (!this.<IsSymbolAccessibleWithin>g__isContainingAssemblyInReferences|185_3(functionPointerTypeSymbol.Signature.ReturnType))
			{
				return false;
			}
			foreach (IParameterSymbol parameterSymbol in functionPointerTypeSymbol.Signature.Parameters)
			{
				if (!this.<IsSymbolAccessibleWithin>g__isContainingAssemblyInReferences|185_3(parameterSymbol.Type))
				{
					return false;
				}
			}
			return true;
			IL_00F9:
			return this.<IsSymbolAccessibleWithin>g__assemblyIsInReferences|185_1(s.ContainingAssembly ?? this.Assembly);
			IL_0110:
			return this.<IsSymbolAccessibleWithin>g__assemblyIsInReferences|185_1(s.ContainingAssembly);
		}

		// Token: 0x04000345 RID: 837
		[Nullable(2)]
		private SmallDictionary<int, bool> _lazyMakeWellKnownTypeMissingMap;

		// Token: 0x04000346 RID: 838
		[Nullable(2)]
		private SmallDictionary<int, bool> _lazyMakeMemberMissingMap;

		// Token: 0x04000347 RID: 839
		protected readonly IReadOnlyDictionary<string, string> _features;

		// Token: 0x04000348 RID: 840
		internal const string UnspecifiedModuleAssemblyName = "?";

		// Token: 0x0400034A RID: 842
		private int _lazySubmissionSlotIndex;

		// Token: 0x0400034B RID: 843
		private const int SubmissionSlotIndexNotApplicable = -3;

		// Token: 0x0400034C RID: 844
		private const int SubmissionSlotIndexToBeAllocated = -2;

		// Token: 0x04000350 RID: 848
		[Nullable(new byte[] { 1, 1, 2 })]
		private readonly ConcurrentCache<string, INamedTypeSymbol> _getTypeCache = new ConcurrentCache<string, INamedTypeSymbol>(50, ReferenceEqualityComparer.Instance);

		// Token: 0x04000351 RID: 849
		internal const CompilationStage DefaultDiagnosticsStage = CompilationStage.Compile;

		// Token: 0x04000352 RID: 850
		[Nullable(new byte[] { 2, 1, 1 })]
		private ConcurrentDictionary<SyntaxTree, SmallConcurrentSetOfInts> _lazyTreeToUsedImportDirectivesMap;

		// Token: 0x04000353 RID: 851
		private static readonly Func<SyntaxTree, SmallConcurrentSetOfInts> s_createSetCallback = (SyntaxTree t) => new SmallConcurrentSetOfInts();

		// Token: 0x04000354 RID: 852
		private readonly WeakList<IAssemblySymbolInternal> _retargetingAssemblySymbols = new WeakList<IAssemblySymbolInternal>();

		// Token: 0x0200057D RID: 1405
		[NullableContext(0)]
		internal enum EmitStreamSignKind
		{
			// Token: 0x040015D2 RID: 5586
			None,
			// Token: 0x040015D3 RID: 5587
			SignedWithBuilder,
			// Token: 0x040015D4 RID: 5588
			SignedWithFile
		}

		// Token: 0x0200057E RID: 1406
		[Nullable(0)]
		internal sealed class EmitStream
		{
			// Token: 0x060034C5 RID: 13509 RVA: 0x00095248 File Offset: 0x00093448
			internal EmitStream(Compilation.EmitStreamProvider emitStreamProvider, Compilation.EmitStreamSignKind emitStreamSignKind, [Nullable(2)] StrongNameProvider strongNameProvider)
			{
				this._emitStreamProvider = emitStreamProvider;
				this._emitStreamSignKind = emitStreamSignKind;
				this._strongNameProvider = strongNameProvider;
			}

			// Token: 0x060034C6 RID: 13510 RVA: 0x00095265 File Offset: 0x00093465
			[return: Nullable(new byte[] { 1, 2 })]
			internal Func<Stream> GetCreateStreamFunc(DiagnosticBag diagnostics)
			{
				return () => this.CreateStream(diagnostics);
			}

			// Token: 0x060034C7 RID: 13511 RVA: 0x00095288 File Offset: 0x00093488
			internal void Close()
			{
				this._stream = null;
				if (this._tempInfo != null)
				{
					ValueTuple<Stream, string> valueOrDefault = this._tempInfo.GetValueOrDefault();
					Stream item = valueOrDefault.Item1;
					string item2 = valueOrDefault.Item2;
					this._tempInfo = default(ValueTuple<Stream, string>?);
					try
					{
						item.Dispose();
					}
					finally
					{
						try
						{
							File.Delete(item2);
						}
						catch
						{
						}
					}
				}
			}

			// Token: 0x060034C8 RID: 13512 RVA: 0x00095300 File Offset: 0x00093500
			[return: Nullable(2)]
			private Stream CreateStream(DiagnosticBag diagnostics)
			{
				if (diagnostics.HasAnyErrors())
				{
					return null;
				}
				this._stream = this._emitStreamProvider.GetOrCreateStream(diagnostics);
				if (this._stream == null)
				{
					return null;
				}
				if (this._emitStreamSignKind == Compilation.EmitStreamSignKind.SignedWithFile)
				{
					string text;
					Stream stream;
					try
					{
						StrongNameFileSystem fileSystem = this._strongNameProvider.FileSystem;
						Func<string, Stream> func = (string path) => fileSystem.CreateFileStream(path, 2, 3, 3);
						text = Path.Combine(fileSystem.GetTempPath(), Guid.NewGuid().ToString("N"));
						stream = FileUtilities.CreateFileStreamChecked(func, text, null);
					}
					catch (IOException ex)
					{
						throw new PeWritingException(ex);
					}
					this._tempInfo = new ValueTuple<Stream, string>?(new ValueTuple<Stream, string>(stream, text));
					return stream;
				}
				return this._stream;
			}

			// Token: 0x060034C9 RID: 13513 RVA: 0x000953BC File Offset: 0x000935BC
			internal bool Complete(StrongNameKeys strongNameKeys, CommonMessageProvider messageProvider, DiagnosticBag diagnostics)
			{
				try
				{
					if (this._tempInfo != null)
					{
						ValueTuple<Stream, string> valueOrDefault = this._tempInfo.GetValueOrDefault();
						Stream item = valueOrDefault.Item1;
						string item2 = valueOrDefault.Item2;
						try
						{
							item.Dispose();
							this._strongNameProvider.SignFile(strongNameKeys, item2);
							using (FileStream fileStream = new FileStream(item2, 3))
							{
								fileStream.CopyTo(this._stream);
							}
						}
						catch (DesktopStrongNameProvider.ClrStrongNameMissingException)
						{
							diagnostics.Add(StrongNameKeys.GetError(strongNameKeys.KeyFilePath, strongNameKeys.KeyContainer, new CodeAnalysisResourcesLocalizableErrorArgument("AssemblySigningNotSupported"), messageProvider));
							return false;
						}
						catch (IOException ex)
						{
							diagnostics.Add(StrongNameKeys.GetError(strongNameKeys.KeyFilePath, strongNameKeys.KeyContainer, ex.Message, messageProvider));
							return false;
						}
					}
				}
				finally
				{
					this.Close();
				}
				return true;
			}

			// Token: 0x040015D5 RID: 5589
			private readonly Compilation.EmitStreamProvider _emitStreamProvider;

			// Token: 0x040015D6 RID: 5590
			private readonly Compilation.EmitStreamSignKind _emitStreamSignKind;

			// Token: 0x040015D7 RID: 5591
			[Nullable(2)]
			private readonly StrongNameProvider _strongNameProvider;

			// Token: 0x040015D8 RID: 5592
			[TupleElementNames(new string[] { "tempStream", "tempFilePath" })]
			[Nullable(new byte[] { 0, 1, 1 })]
			private ValueTuple<Stream, string>? _tempInfo;

			// Token: 0x040015D9 RID: 5593
			[Nullable(2)]
			private Stream _stream;
		}

		// Token: 0x0200057F RID: 1407
		[Nullable(0)]
		internal abstract class EmitStreamProvider
		{
			// Token: 0x17000EF9 RID: 3833
			// (get) Token: 0x060034CA RID: 13514
			[Nullable(2)]
			public abstract Stream Stream
			{
				[NullableContext(2)]
				get;
			}

			// Token: 0x060034CB RID: 13515
			[return: Nullable(2)]
			protected abstract Stream CreateStream(DiagnosticBag diagnostics);

			// Token: 0x060034CC RID: 13516 RVA: 0x000954B8 File Offset: 0x000936B8
			[return: Nullable(2)]
			public Stream GetOrCreateStream(DiagnosticBag diagnostics)
			{
				return this.Stream ?? this.CreateStream(diagnostics);
			}
		}

		// Token: 0x02000580 RID: 1408
		[Nullable(0)]
		internal sealed class SimpleEmitStreamProvider : Compilation.EmitStreamProvider
		{
			// Token: 0x060034CE RID: 13518 RVA: 0x000954D3 File Offset: 0x000936D3
			internal SimpleEmitStreamProvider(Stream stream)
			{
				this._stream = stream;
			}

			// Token: 0x17000EFA RID: 3834
			// (get) Token: 0x060034CF RID: 13519 RVA: 0x000954E2 File Offset: 0x000936E2
			public override Stream Stream
			{
				get
				{
					return this._stream;
				}
			}

			// Token: 0x060034D0 RID: 13520 RVA: 0x000954EA File Offset: 0x000936EA
			protected override Stream CreateStream(DiagnosticBag diagnostics)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x040015DA RID: 5594
			private readonly Stream _stream;
		}

		// Token: 0x02000581 RID: 1409
		[NullableContext(0)]
		internal enum Win32ResourceForm : byte
		{
			// Token: 0x040015DC RID: 5596
			UNKNOWN,
			// Token: 0x040015DD RID: 5597
			COFF,
			// Token: 0x040015DE RID: 5598
			RES
		}
	}
}
