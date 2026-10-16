using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000306 RID: 774
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AnalyzerFileReference : AnalyzerReference, IEquatable<AnalyzerReference>
	{
		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x0006BFDF File Offset: 0x0006A1DF
		public override string FullPath { get; }

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060021F9 RID: 8697 RVA: 0x0006BFE8 File Offset: 0x0006A1E8
		// (remove) Token: 0x060021FA RID: 8698 RVA: 0x0006C020 File Offset: 0x0006A220
		[Nullable(new byte[] { 2, 1 })]
		[field: Nullable(new byte[] { 2, 1 })]
		public event EventHandler<AnalyzerLoadFailureEventArgs> AnalyzerLoadFailed;

		// Token: 0x060021FB RID: 8699 RVA: 0x0006C058 File Offset: 0x0006A258
		public AnalyzerFileReference(string fullPath, IAnalyzerAssemblyLoader assemblyLoader)
		{
			CompilerPathUtilities.RequireAbsolutePath(fullPath, "fullPath");
			this.FullPath = fullPath;
			if (assemblyLoader == null)
			{
				throw new ArgumentNullException("assemblyLoader");
			}
			this._assemblyLoader = assemblyLoader;
			this._diagnosticAnalyzers = new AnalyzerFileReference.Extensions<DiagnosticAnalyzer>(this, typeof(DiagnosticAnalyzerAttribute), new AnalyzerFileReference.AttributeLanguagesFunc(AnalyzerFileReference.GetDiagnosticsAnalyzerSupportedLanguages), true);
			this._generators = new AnalyzerFileReference.Extensions<ISourceGenerator>(this, typeof(GeneratorAttribute), new AnalyzerFileReference.AttributeLanguagesFunc(AnalyzerFileReference.GetGeneratorSupportedLanguages), false);
			assemblyLoader.AddDependencyLocation(fullPath);
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0006C0E0 File Offset: 0x0006A2E0
		public IAnalyzerAssemblyLoader AssemblyLoader
		{
			get
			{
				return this._assemblyLoader;
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x0006C0E8 File Offset: 0x0006A2E8
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AnalyzerFileReference);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x0006C0F6 File Offset: 0x0006A2F6
		[NullableContext(2)]
		public bool Equals(AnalyzerFileReference other)
		{
			return this == other || (other != null && this._assemblyLoader == other._assemblyLoader && this.FullPath == other.FullPath);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x0006C124 File Offset: 0x0006A324
		[NullableContext(2)]
		public bool Equals(AnalyzerReference other)
		{
			if (this == other)
			{
				return true;
			}
			if (other == null)
			{
				return false;
			}
			AnalyzerFileReference analyzerFileReference = other as AnalyzerFileReference;
			if (analyzerFileReference != null)
			{
				return this.Equals(analyzerFileReference);
			}
			return this.FullPath == other.FullPath;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x0006C15F File Offset: 0x0006A35F
		public override int GetHashCode()
		{
			return Hash.Combine(RuntimeHelpers.GetHashCode(this._assemblyLoader), this.FullPath.GetHashCode());
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x0006C17C File Offset: 0x0006A37C
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<DiagnosticAnalyzer> GetAnalyzersForAllLanguages()
		{
			return this._diagnosticAnalyzers.GetExtensionsForAllLanguages(true);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0006C18A File Offset: 0x0006A38A
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<DiagnosticAnalyzer> GetAnalyzers(string language)
		{
			return this._diagnosticAnalyzers.GetExtensions(language);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0006C198 File Offset: 0x0006A398
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ISourceGenerator> GetGeneratorsForAllLanguages()
		{
			return this._generators.GetExtensionsForAllLanguages(false);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0006C1A6 File Offset: 0x0006A3A6
		[Obsolete("Use GetGenerators(string language) or GetGeneratorsForAllLanguages()")]
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ISourceGenerator> GetGenerators()
		{
			return this._generators.GetExtensions("C#");
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0006C1B8 File Offset: 0x0006A3B8
		[return: Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<ISourceGenerator> GetGenerators(string language)
		{
			return this._generators.GetExtensions(language);
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0006C1C6 File Offset: 0x0006A3C6
		public override string Display
		{
			get
			{
				if (this._lazyDisplay == null)
				{
					this.InitializeDisplayAndId();
				}
				return this._lazyDisplay;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x0006C1DC File Offset: 0x0006A3DC
		public override object Id
		{
			get
			{
				if (this._lazyIdentity == null)
				{
					this.InitializeDisplayAndId();
				}
				return this._lazyIdentity;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0006C1F4 File Offset: 0x0006A3F4
		[MemberNotNull(new string[] { "_lazyIdentity", "_lazyDisplay" })]
		private void InitializeDisplayAndId()
		{
			try
			{
				using (PEReader pereader = new PEReader(FileUtilities.OpenRead(this.FullPath)))
				{
					AssemblyIdentity assemblyIdentity = pereader.GetMetadataReader().ReadAssemblyIdentityOrThrow();
					this._lazyDisplay = assemblyIdentity.Name;
					this._lazyIdentity = assemblyIdentity;
				}
			}
			catch
			{
				this._lazyDisplay = FileNameUtilities.GetFileName(this.FullPath, false);
				this._lazyIdentity = this._lazyDisplay;
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0006C27C File Offset: 0x0006A47C
		internal void AddAnalyzers(ImmutableArray<DiagnosticAnalyzer>.Builder builder, string language)
		{
			this._diagnosticAnalyzers.AddExtensions(builder, language);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x0006C28B File Offset: 0x0006A48B
		internal void AddGenerators(ImmutableArray<ISourceGenerator>.Builder builder, string language)
		{
			this._generators.AddExtensions(builder, language);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x0006C29C File Offset: 0x0006A49C
		private static AnalyzerLoadFailureEventArgs CreateAnalyzerFailedArgs(Exception e, [Nullable(2)] string typeName = null)
		{
			e = (e as TargetInvocationException) ?? e;
			string text = e.Message.Replace("\r", "").Replace("\n", "");
			return new AnalyzerLoadFailureEventArgs((typeName != null) ? AnalyzerLoadFailureEventArgs.FailureErrorCode.UnableToCreateAnalyzer : AnalyzerLoadFailureEventArgs.FailureErrorCode.UnableToLoadAnalyzer, text, e, typeName);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x0006C2EA File Offset: 0x0006A4EA
		internal ImmutableSortedDictionary<string, ImmutableSortedSet<string>> GetAnalyzerTypeNameMap()
		{
			return this._diagnosticAnalyzers.GetExtensionTypeNameMap();
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x0006C2F8 File Offset: 0x0006A4F8
		private static ImmutableSortedDictionary<string, ImmutableSortedSet<string>> GetAnalyzerTypeNameMap(string fullPath, Type attributeType, AnalyzerFileReference.AttributeLanguagesFunc languagesFunc)
		{
			ImmutableSortedDictionary<string, ImmutableSortedSet<string>> immutableSortedDictionary;
			using (AssemblyMetadata assemblyMetadata = AssemblyMetadata.CreateFromFile(fullPath))
			{
				immutableSortedDictionary = Enumerable.GroupBy(Enumerable.SelectMany(Enumerable.Select(Enumerable.Where(Enumerable.Select(Enumerable.Select(from module in assemblyMetadata.GetModules()
					from typeDefHandle in module.MetadataReader.TypeDefinitions
					select new { module, typeDefHandle }, <>h__TransparentIdentifier0 => new
				{
					<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
					typeDef = <>h__TransparentIdentifier0.module.MetadataReader.GetTypeDefinition(<>h__TransparentIdentifier0.typeDefHandle)
				}), <>h__TransparentIdentifier1 => new
				{
					<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
					supportedLanguages = AnalyzerFileReference.GetSupportedLanguages(<>h__TransparentIdentifier1.typeDef, <>h__TransparentIdentifier1.<>h__TransparentIdentifier0.module.Module, attributeType, languagesFunc)
				}), <>h__TransparentIdentifier2 => Enumerable.Any<string>(<>h__TransparentIdentifier2.supportedLanguages)), <>h__TransparentIdentifier2 => new
				{
					<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
					typeName = AnalyzerFileReference.GetFullyQualifiedTypeName(<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.typeDef, <>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.module.Module)
				}), <>h__TransparentIdentifier3 => <>h__TransparentIdentifier3.<>h__TransparentIdentifier2.supportedLanguages, (<>h__TransparentIdentifier3, string supportedLanguage) => new { <>h__TransparentIdentifier3, supportedLanguage }), <>h__TransparentIdentifier4 => <>h__TransparentIdentifier4.supportedLanguage, <>h__TransparentIdentifier4 => <>h__TransparentIdentifier4.<>h__TransparentIdentifier3.typeName).ToImmutableSortedDictionary<IGrouping<string, string>, string, ImmutableSortedSet<string>>((IGrouping<string, string> g) => g.Key, (IGrouping<string, string> g) => g.ToImmutableSortedSet<string>(StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);
			}
			return immutableSortedDictionary;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
		private static IEnumerable<string> GetSupportedLanguages(TypeDefinition typeDef, PEModule peModule, Type attributeType, AnalyzerFileReference.AttributeLanguagesFunc languagesFunc)
		{
			IEnumerable<string> enumerable = null;
			foreach (CustomAttributeHandle customAttributeHandle in typeDef.GetCustomAttributes())
			{
				EntityHandle entityHandle;
				if (peModule.IsTargetAttribute(customAttributeHandle, attributeType.Namespace, attributeType.Name, out entityHandle, false))
				{
					IEnumerable<string> enumerable2 = languagesFunc(peModule, customAttributeHandle);
					if (enumerable2 != null)
					{
						if (enumerable == null)
						{
							enumerable = enumerable2;
						}
						else
						{
							enumerable = Enumerable.Concat<string>(enumerable, enumerable2);
						}
					}
				}
			}
			return enumerable ?? SpecializedCollections.EmptyEnumerable<string>();
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0006C574 File Offset: 0x0006A774
		private static IEnumerable<string> GetDiagnosticsAnalyzerSupportedLanguages(PEModule peModule, CustomAttributeHandle customAttrHandle)
		{
			BlobReader memoryReaderOrThrow = peModule.GetMemoryReaderOrThrow(peModule.GetCustomAttributeValueOrThrow(customAttrHandle));
			return AnalyzerFileReference.ReadLanguagesFromAttribute(ref memoryReaderOrThrow);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0006C598 File Offset: 0x0006A798
		private static IEnumerable<string> GetGeneratorSupportedLanguages(PEModule peModule, CustomAttributeHandle customAttrHandle)
		{
			BlobReader memoryReaderOrThrow = peModule.GetMemoryReaderOrThrow(peModule.GetCustomAttributeValueOrThrow(customAttrHandle));
			if (memoryReaderOrThrow.Length == 4)
			{
				return ImmutableArray.Create<string>("C#");
			}
			return AnalyzerFileReference.ReadLanguagesFromAttribute(ref memoryReaderOrThrow);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0006C5D4 File Offset: 0x0006A7D4
		private static IEnumerable<string> ReadLanguagesFromAttribute(ref BlobReader argsReader)
		{
			if (argsReader.Length > 4 && argsReader.ReadByte() == 1 && argsReader.ReadByte() == 0)
			{
				string text;
				if (!PEModule.CrackStringInAttributeValue(out text, ref argsReader))
				{
					return SpecializedCollections.EmptyEnumerable<string>();
				}
				ImmutableArray<string> immutableArray;
				if (PEModule.CrackStringArrayInAttributeValue(out immutableArray, ref argsReader))
				{
					if (immutableArray.Length == 0)
					{
						return SpecializedCollections.SingletonEnumerable<string>(text);
					}
					return immutableArray.Insert(0, text);
				}
			}
			return SpecializedCollections.EmptyEnumerable<string>();
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0006C63C File Offset: 0x0006A83C
		private static string GetFullyQualifiedTypeName(TypeDefinition typeDef, PEModule peModule)
		{
			TypeDefinitionHandle declaringType = typeDef.GetDeclaringType();
			if (declaringType.IsNil)
			{
				return peModule.GetFullNameOrThrow(typeDef.Namespace, typeDef.Name);
			}
			return AnalyzerFileReference.GetFullyQualifiedTypeName(peModule.MetadataReader.GetTypeDefinition(declaringType), peModule) + "+" + peModule.MetadataReader.GetString(typeDef.Name);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0006C69D File Offset: 0x0006A89D
		public Assembly GetAssembly()
		{
			if (this._lazyAssembly == null)
			{
				this._lazyAssembly = this._assemblyLoader.LoadFromPath(this.FullPath);
			}
			return this._lazyAssembly;
		}

		// Token: 0x04000EA5 RID: 3749
		private readonly IAnalyzerAssemblyLoader _assemblyLoader;

		// Token: 0x04000EA6 RID: 3750
		private readonly AnalyzerFileReference.Extensions<DiagnosticAnalyzer> _diagnosticAnalyzers;

		// Token: 0x04000EA7 RID: 3751
		private readonly AnalyzerFileReference.Extensions<ISourceGenerator> _generators;

		// Token: 0x04000EA8 RID: 3752
		[Nullable(2)]
		private string _lazyDisplay;

		// Token: 0x04000EA9 RID: 3753
		[Nullable(2)]
		private object _lazyIdentity;

		// Token: 0x04000EAA RID: 3754
		[Nullable(2)]
		private Assembly _lazyAssembly;

		// Token: 0x020006CD RID: 1741
		// (Invoke) Token: 0x06003A77 RID: 14967
		[NullableContext(0)]
		private delegate IEnumerable<string> AttributeLanguagesFunc(PEModule module, CustomAttributeHandle attribute);

		// Token: 0x020006CE RID: 1742
		[Nullable(0)]
		private sealed class Extensions<TExtension> where TExtension : class
		{
			// Token: 0x06003A7A RID: 14970 RVA: 0x000A43A3 File Offset: 0x000A25A3
			internal Extensions(AnalyzerFileReference reference, Type attributeType, AnalyzerFileReference.AttributeLanguagesFunc languagesFunc, bool allowNetFramework)
			{
				this._reference = reference;
				this._attributeType = attributeType;
				this._languagesFunc = languagesFunc;
				this._allowNetFramework = allowNetFramework;
				this._lazyAllExtensions = default(ImmutableArray<TExtension>);
				this._lazyExtensionsPerLanguage = ImmutableDictionary<string, ImmutableArray<TExtension>>.Empty;
			}

			// Token: 0x06003A7B RID: 14971 RVA: 0x000A43DF File Offset: 0x000A25DF
			[return: Nullable(new byte[] { 0, 1 })]
			internal ImmutableArray<TExtension> GetExtensionsForAllLanguages(bool includeDuplicates)
			{
				if (this._lazyAllExtensions.IsDefault)
				{
					ImmutableInterlocked.InterlockedInitialize<TExtension>(ref this._lazyAllExtensions, AnalyzerFileReference.Extensions<TExtension>.CreateExtensionsForAllLanguages(this, includeDuplicates));
				}
				return this._lazyAllExtensions;
			}

			// Token: 0x06003A7C RID: 14972 RVA: 0x000A4408 File Offset: 0x000A2608
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<TExtension> CreateExtensionsForAllLanguages(AnalyzerFileReference.Extensions<TExtension> extensions, bool includeDuplicates)
			{
				ImmutableSortedDictionary<string, ImmutableArray<TExtension>>.Builder builder = ImmutableSortedDictionary.CreateBuilder<string, ImmutableArray<TExtension>>(StringComparer.OrdinalIgnoreCase);
				extensions.AddExtensions(builder);
				ImmutableArray<TExtension>.Builder builder2 = ImmutableArray.CreateBuilder<TExtension>();
				foreach (ImmutableArray<TExtension> immutableArray in builder.Values)
				{
					foreach (TExtension textension in immutableArray)
					{
						builder2.Add(textension);
					}
				}
				if (includeDuplicates)
				{
					return builder2.ToImmutable();
				}
				return Enumerable.Distinct<TExtension>(builder2, AnalyzerFileReference.Extensions<TExtension>.ExtTypeComparer.Instance).ToImmutableArray<TExtension>();
			}

			// Token: 0x06003A7D RID: 14973 RVA: 0x000A44A8 File Offset: 0x000A26A8
			[return: Nullable(new byte[] { 0, 1 })]
			internal ImmutableArray<TExtension> GetExtensions(string language)
			{
				if (string.IsNullOrEmpty(language))
				{
					throw new ArgumentException("language");
				}
				return ImmutableInterlocked.GetOrAdd<string, ImmutableArray<TExtension>, AnalyzerFileReference.Extensions<TExtension>>(ref this._lazyExtensionsPerLanguage, language, new Func<string, AnalyzerFileReference.Extensions<TExtension>, ImmutableArray<TExtension>>(AnalyzerFileReference.Extensions<TExtension>.CreateLanguageSpecificExtensions), this);
			}

			// Token: 0x06003A7E RID: 14974 RVA: 0x000A44D8 File Offset: 0x000A26D8
			[return: Nullable(new byte[] { 0, 1 })]
			private static ImmutableArray<TExtension> CreateLanguageSpecificExtensions(string language, AnalyzerFileReference.Extensions<TExtension> extensions)
			{
				ImmutableArray<TExtension>.Builder builder = ImmutableArray.CreateBuilder<TExtension>();
				extensions.AddExtensions(builder, language);
				return builder.ToImmutable();
			}

			// Token: 0x06003A7F RID: 14975 RVA: 0x000A44FC File Offset: 0x000A26FC
			internal ImmutableSortedDictionary<string, ImmutableSortedSet<string>> GetExtensionTypeNameMap()
			{
				if (this._lazyExtensionTypeNameMap == null)
				{
					ImmutableSortedDictionary<string, ImmutableSortedSet<string>> analyzerTypeNameMap = AnalyzerFileReference.GetAnalyzerTypeNameMap(this._reference.FullPath, this._attributeType, this._languagesFunc);
					Interlocked.CompareExchange<ImmutableSortedDictionary<string, ImmutableSortedSet<string>>>(ref this._lazyExtensionTypeNameMap, analyzerTypeNameMap, null);
				}
				return this._lazyExtensionTypeNameMap;
			}

			// Token: 0x06003A80 RID: 14976 RVA: 0x000A4544 File Offset: 0x000A2744
			internal void AddExtensions([Nullable(new byte[] { 1, 1, 0, 1 })] ImmutableSortedDictionary<string, ImmutableArray<TExtension>>.Builder builder)
			{
				ImmutableSortedDictionary<string, ImmutableSortedSet<string>> extensionTypeNameMap;
				Assembly assembly;
				try
				{
					extensionTypeNameMap = this.GetExtensionTypeNameMap();
					if (extensionTypeNameMap.Count == 0)
					{
						return;
					}
					assembly = this._reference.GetAssembly();
				}
				catch (Exception ex)
				{
					EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed = this._reference.AnalyzerLoadFailed;
					if (analyzerLoadFailed != null)
					{
						analyzerLoadFailed.Invoke(this._reference, AnalyzerFileReference.CreateAnalyzerFailedArgs(ex, null));
					}
					return;
				}
				int count = builder.Count;
				bool flag = false;
				foreach (KeyValuePair<string, ImmutableSortedSet<string>> keyValuePair in extensionTypeNameMap)
				{
					string text;
					ImmutableSortedSet<string> immutableSortedSet;
					keyValuePair.Deconstruct<string, ImmutableSortedSet<string>>(out text, out immutableSortedSet);
					string text2 = text;
					if (text2 != null)
					{
						ImmutableArray<TExtension> languageSpecificAnalyzers = this.GetLanguageSpecificAnalyzers(assembly, extensionTypeNameMap, text2, ref flag);
						builder.Add(text2, languageSpecificAnalyzers);
					}
				}
				if (builder.Count == count && !flag)
				{
					EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed2 = this._reference.AnalyzerLoadFailed;
					if (analyzerLoadFailed2 == null)
					{
						return;
					}
					analyzerLoadFailed2.Invoke(this._reference, new AnalyzerLoadFailureEventArgs(AnalyzerLoadFailureEventArgs.FailureErrorCode.NoAnalyzers, CodeAnalysisResources.NoAnalyzersFound, null, null));
				}
			}

			// Token: 0x06003A81 RID: 14977 RVA: 0x000A464C File Offset: 0x000A284C
			internal void AddExtensions(ImmutableArray<TExtension>.Builder builder, string language)
			{
				ImmutableSortedDictionary<string, ImmutableSortedSet<string>> extensionTypeNameMap;
				Assembly assembly;
				try
				{
					extensionTypeNameMap = this.GetExtensionTypeNameMap();
					if (!extensionTypeNameMap.ContainsKey(language))
					{
						return;
					}
					assembly = this._reference.GetAssembly();
					if (assembly == null)
					{
						return;
					}
				}
				catch (Exception ex)
				{
					EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed = this._reference.AnalyzerLoadFailed;
					if (analyzerLoadFailed != null)
					{
						analyzerLoadFailed.Invoke(this._reference, AnalyzerFileReference.CreateAnalyzerFailedArgs(ex, null));
					}
					return;
				}
				int count = builder.Count;
				bool flag = false;
				ImmutableArray<TExtension> languageSpecificAnalyzers = this.GetLanguageSpecificAnalyzers(assembly, extensionTypeNameMap, language, ref flag);
				builder.AddRange(languageSpecificAnalyzers);
				if (builder.Count == count && !flag)
				{
					EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed2 = this._reference.AnalyzerLoadFailed;
					if (analyzerLoadFailed2 == null)
					{
						return;
					}
					analyzerLoadFailed2.Invoke(this._reference, new AnalyzerLoadFailureEventArgs(AnalyzerLoadFailureEventArgs.FailureErrorCode.NoAnalyzers, CodeAnalysisResources.NoAnalyzersFound, null, null));
				}
			}

			// Token: 0x06003A82 RID: 14978 RVA: 0x000A4714 File Offset: 0x000A2914
			[return: Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<TExtension> GetLanguageSpecificAnalyzers(Assembly analyzerAssembly, ImmutableSortedDictionary<string, ImmutableSortedSet<string>> analyzerTypeNameMap, string language, ref bool reportedError)
			{
				ImmutableSortedSet<string> immutableSortedSet;
				if (!analyzerTypeNameMap.TryGetValue(language, out immutableSortedSet))
				{
					return ImmutableArray<TExtension>.Empty;
				}
				return this.GetAnalyzersForTypeNames(analyzerAssembly, immutableSortedSet, ref reportedError);
			}

			// Token: 0x06003A83 RID: 14979 RVA: 0x000A473C File Offset: 0x000A293C
			[return: Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<TExtension> GetAnalyzersForTypeNames(Assembly analyzerAssembly, IEnumerable<string> analyzerTypeNames, ref bool reportedError)
			{
				ImmutableArray<TExtension>.Builder builder = ImmutableArray.CreateBuilder<TExtension>();
				foreach (string text in analyzerTypeNames)
				{
					Type type;
					try
					{
						type = analyzerAssembly.GetType(text, true, false);
					}
					catch (Exception ex)
					{
						EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed = this._reference.AnalyzerLoadFailed;
						if (analyzerLoadFailed != null)
						{
							analyzerLoadFailed.Invoke(this._reference, AnalyzerFileReference.CreateAnalyzerFailedArgs(ex, text));
						}
						reportedError = true;
						continue;
					}
					if (!this._allowNetFramework)
					{
						TargetFrameworkAttribute customAttribute = CustomAttributeExtensions.GetCustomAttribute<TargetFrameworkAttribute>(analyzerAssembly);
						if (customAttribute != null && customAttribute.FrameworkName.StartsWith(".NETFramework", 5))
						{
							EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed2 = this._reference.AnalyzerLoadFailed;
							if (analyzerLoadFailed2 == null)
							{
								continue;
							}
							analyzerLoadFailed2.Invoke(this._reference, new AnalyzerLoadFailureEventArgs(AnalyzerLoadFailureEventArgs.FailureErrorCode.ReferencesFramework, string.Format(CodeAnalysisResources.AssemblyReferencesNetFramework, text), null, text));
							continue;
						}
					}
					TExtension textension;
					try
					{
						textension = Activator.CreateInstance(type) as TExtension;
					}
					catch (Exception ex2)
					{
						EventHandler<AnalyzerLoadFailureEventArgs> analyzerLoadFailed3 = this._reference.AnalyzerLoadFailed;
						if (analyzerLoadFailed3 != null)
						{
							analyzerLoadFailed3.Invoke(this._reference, AnalyzerFileReference.CreateAnalyzerFailedArgs(ex2, text));
						}
						reportedError = true;
						continue;
					}
					if (textension != null)
					{
						builder.Add(textension);
					}
				}
				return builder.ToImmutable();
			}

			// Token: 0x04001A99 RID: 6809
			private readonly AnalyzerFileReference _reference;

			// Token: 0x04001A9A RID: 6810
			private readonly Type _attributeType;

			// Token: 0x04001A9B RID: 6811
			private readonly AnalyzerFileReference.AttributeLanguagesFunc _languagesFunc;

			// Token: 0x04001A9C RID: 6812
			private readonly bool _allowNetFramework;

			// Token: 0x04001A9D RID: 6813
			[Nullable(new byte[] { 0, 1 })]
			private ImmutableArray<TExtension> _lazyAllExtensions;

			// Token: 0x04001A9E RID: 6814
			[Nullable(new byte[] { 1, 1, 0, 1 })]
			private ImmutableDictionary<string, ImmutableArray<TExtension>> _lazyExtensionsPerLanguage;

			// Token: 0x04001A9F RID: 6815
			[Nullable(new byte[] { 2, 1, 1, 1 })]
			private ImmutableSortedDictionary<string, ImmutableSortedSet<string>> _lazyExtensionTypeNameMap;

			// Token: 0x020007DF RID: 2015
			[NullableContext(0)]
			private class ExtTypeComparer : IEqualityComparer<TExtension>
			{
				// Token: 0x0600402F RID: 16431 RVA: 0x000B36DA File Offset: 0x000B18DA
				[NullableContext(2)]
				public bool Equals(TExtension x, TExtension y)
				{
					TExtension textension = x;
					object obj = ((textension != null) ? textension.GetType() : null);
					TExtension textension2 = y;
					return object.Equals(obj, (textension2 != null) ? textension2.GetType() : null);
				}

				// Token: 0x06004030 RID: 16432 RVA: 0x000B3705 File Offset: 0x000B1905
				[NullableContext(1)]
				public int GetHashCode(TExtension obj)
				{
					return obj.GetType().GetHashCode();
				}

				// Token: 0x04001E2F RID: 7727
				[Nullable(new byte[] { 1, 0 })]
				public static readonly AnalyzerFileReference.Extensions<TExtension>.ExtTypeComparer Instance = new AnalyzerFileReference.Extensions<TExtension>.ExtTypeComparer();
			}
		}
	}
}
