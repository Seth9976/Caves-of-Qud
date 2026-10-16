using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200012C RID: 300
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CommonCompiler
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x000200B6 File Offset: 0x0001E2B6
		public CommonMessageProvider MessageProvider { get; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x000200BE File Offset: 0x0001E2BE
		public CommandLineArguments Arguments { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x000200C6 File Offset: 0x0001E2C6
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x000200CE File Offset: 0x0001E2CE
		public IAnalyzerAssemblyLoader AssemblyLoader { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000B0C RID: 2828
		public abstract DiagnosticFormatter DiagnosticFormatter { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000200D7 File Offset: 0x0001E2D7
		public IReadOnlySet<string> EmbeddedSourcePaths { get; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000200DF File Offset: 0x0001E2DF
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x000200E7 File Offset: 0x0001E2E7
		internal ICommonCompilerFileSystem FileSystem { get; set; } = StandardFileSystem.Instance;

		// Token: 0x06000B10 RID: 2832
		[NullableContext(2)]
		public abstract Compilation CreateCompilation([Nullable(1)] TextWriter consoleOutput, TouchedFileLogger touchedFilesLogger, ErrorLogger errorLoggerOpt, [Nullable(0)] ImmutableArray<AnalyzerConfigOptionsResult> analyzerConfigOptions, AnalyzerConfigOptionsResult globalConfigOptions);

		// Token: 0x06000B11 RID: 2833
		public abstract void PrintLogo(TextWriter consoleOutput);

		// Token: 0x06000B12 RID: 2834
		public abstract void PrintHelp(TextWriter consoleOutput);

		// Token: 0x06000B13 RID: 2835
		public abstract void PrintLangVersions(TextWriter consoleOutput);

		// Token: 0x06000B14 RID: 2836 RVA: 0x000200F0 File Offset: 0x0001E2F0
		public virtual void PrintVersion(TextWriter consoleOutput)
		{
			consoleOutput.WriteLine(this.GetCompilerVersion());
		}

		// Token: 0x06000B15 RID: 2837
		protected abstract bool TryGetCompilerDiagnosticCode(string diagnosticId, out uint code);

		// Token: 0x06000B16 RID: 2838
		protected abstract void ResolveAnalyzersFromArguments(List<DiagnosticInfo> diagnostics, CommonMessageProvider messageProvider, bool skipAnalyzers, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<ISourceGenerator> generators);

		// Token: 0x06000B17 RID: 2839 RVA: 0x00020100 File Offset: 0x0001E300
		public CommonCompiler(CommandLineParser parser, [Nullable(2)] string responseFile, string[] args, BuildPaths buildPaths, [Nullable(2)] string additionalReferenceDirectories, IAnalyzerAssemblyLoader assemblyLoader)
		{
			IEnumerable<string> enumerable = args;
			if (!this.SuppressDefaultResponseFile(args) && File.Exists(responseFile))
			{
				enumerable = Enumerable.Concat<string>(new string[] { "@" + responseFile }, enumerable);
			}
			this.Arguments = parser.Parse(enumerable, buildPaths.WorkingDirectory, buildPaths.SdkDirectory, additionalReferenceDirectories);
			this.MessageProvider = parser.MessageProvider;
			this.AssemblyLoader = assemblyLoader;
			this.EmbeddedSourcePaths = CommonCompiler.GetEmbeddedSourcePaths(this.Arguments);
			if (this.Arguments.ParseOptions.Features.ContainsKey("debug-determinism"))
			{
				CommonCompiler.EmitDeterminismKey(this.Arguments, args, buildPaths.WorkingDirectory, parser);
			}
		}

		// Token: 0x06000B18 RID: 2840
		internal abstract bool SuppressDefaultResponseFile(IEnumerable<string> args);

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B19 RID: 2841
		internal abstract Type Type { get; }

		// Token: 0x06000B1A RID: 2842 RVA: 0x000201E0 File Offset: 0x0001E3E0
		internal string GetCompilerVersion()
		{
			return CommonCompiler.GetProductVersion(this.Type);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000201F0 File Offset: 0x0001E3F0
		internal static string GetProductVersion(Type type)
		{
			string informationalVersionWithoutHash = CommonCompiler.GetInformationalVersionWithoutHash(type);
			string shortCommitHash = CommonCompiler.GetShortCommitHash(type);
			return informationalVersionWithoutHash + " (" + shortCommitHash + ")";
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002021A File Offset: 0x0001E41A
		[NullableContext(2)]
		[return: NotNullIfNotNull("hash")]
		internal static string ExtractShortCommitHash(string hash)
		{
			if (hash != null && hash.Length >= 8 && hash.get_Chars(0) != '<')
			{
				return hash.Substring(0, 8);
			}
			return hash;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002023D File Offset: 0x0001E43D
		[return: Nullable(2)]
		private static string GetInformationalVersionWithoutHash(Type type)
		{
			AssemblyInformationalVersionAttribute customAttribute = CustomAttributeExtensions.GetCustomAttribute<AssemblyInformationalVersionAttribute>(type.Assembly);
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.InformationalVersion.Split(new char[] { '+' })[0];
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00020267 File Offset: 0x0001E467
		[return: Nullable(2)]
		private static string GetShortCommitHash(Type type)
		{
			CommitHashAttribute customAttribute = CustomAttributeExtensions.GetCustomAttribute<CommitHashAttribute>(type.Assembly);
			return CommonCompiler.ExtractShortCommitHash((customAttribute != null) ? customAttribute.Hash : null);
		}

		// Token: 0x06000B1F RID: 2847
		internal abstract string GetToolName();

		// Token: 0x06000B20 RID: 2848 RVA: 0x00020285 File Offset: 0x0001E485
		[NullableContext(2)]
		internal Version GetAssemblyVersion()
		{
			return IntrospectionExtensions.GetTypeInfo(this.Type).Assembly.GetName().Version;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000202A1 File Offset: 0x0001E4A1
		internal string GetCultureName()
		{
			return this.Culture.Name;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000202AE File Offset: 0x0001E4AE
		internal virtual Func<string, MetadataReferenceProperties, PortableExecutableReference> GetMetadataProvider()
		{
			return (string path, MetadataReferenceProperties properties) => MetadataReference.CreateFromFile(this.FileSystem.OpenFileWithNormalizedException(path, 3, 1, 1), path, properties, null);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000202BC File Offset: 0x0001E4BC
		internal virtual MetadataReferenceResolver GetCommandLineMetadataReferenceResolver([Nullable(2)] TouchedFileLogger loggerOpt)
		{
			return new CommonCompiler.LoggingMetadataFileReferenceResolver(new CommonCompiler.CompilerRelativePathResolver(this.FileSystem, this.Arguments.ReferencePaths, this.Arguments.BaseDirectory), this.GetMetadataProvider(), loggerOpt);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000202EC File Offset: 0x0001E4EC
		internal List<MetadataReference> ResolveMetadataReferences(List<DiagnosticInfo> diagnostics, [Nullable(2)] TouchedFileLogger touchedFiles, out MetadataReferenceResolver referenceDirectiveResolver)
		{
			MetadataReferenceResolver commandLineMetadataReferenceResolver = this.GetCommandLineMetadataReferenceResolver(touchedFiles);
			List<MetadataReference> list = new List<MetadataReference>();
			this.Arguments.ResolveMetadataReferences(commandLineMetadataReferenceResolver, diagnostics, this.MessageProvider, list);
			if (this.Arguments.IsScriptRunner)
			{
				referenceDirectiveResolver = commandLineMetadataReferenceResolver;
			}
			else
			{
				referenceDirectiveResolver = new CommonCompiler.ExistingReferencesResolver(commandLineMetadataReferenceResolver, list.ToImmutableArray<MetadataReference>());
			}
			return list;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00020340 File Offset: 0x0001E540
		[return: Nullable(2)]
		internal SourceText TryReadFileContent(CommandLineSourceFile file, IList<DiagnosticInfo> diagnostics)
		{
			string text;
			return this.TryReadFileContent(file, diagnostics, out text);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00020358 File Offset: 0x0001E558
		[NullableContext(2)]
		internal SourceText TryReadFileContent(CommandLineSourceFile file, [Nullable(1)] IList<DiagnosticInfo> diagnostics, out string normalizedFilePath)
		{
			string path = file.Path;
			SourceText sourceText;
			try
			{
				if (file.IsInputRedirected)
				{
					using (Stream stream = Console.OpenStandardInput())
					{
						normalizedFilePath = path;
						return EncodedStringText.Create(stream, this._fallbackEncoding, this.Arguments.Encoding, this.Arguments.ChecksumAlgorithm, this.EmbeddedSourcePaths.Contains(file.Path));
					}
				}
				using (Stream stream2 = this.OpenFileForReadWithSmallBufferOptimization(path, out normalizedFilePath))
				{
					sourceText = EncodedStringText.Create(stream2, this._fallbackEncoding, this.Arguments.Encoding, this.Arguments.ChecksumAlgorithm, this.EmbeddedSourcePaths.Contains(file.Path));
				}
			}
			catch (Exception ex)
			{
				diagnostics.Add(CommonCompiler.ToFileReadDiagnostics(this.MessageProvider, ex, path));
				normalizedFilePath = null;
				sourceText = null;
			}
			return sourceText;
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00020450 File Offset: 0x0001E650
		internal bool TryGetAnalyzerConfigSet([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> analyzerConfigPaths, DiagnosticBag diagnostics, [Nullable(2)] [NotNullWhen(true)] out AnalyzerConfigSet analyzerConfigSet)
		{
			ArrayBuilder<AnalyzerConfig> instance = ArrayBuilder<AnalyzerConfig>.GetInstance(analyzerConfigPaths.Length);
			PooledHashSet<string> instance2 = PooledHashSet<string>.GetInstance();
			foreach (string text in analyzerConfigPaths)
			{
				string text3;
				string text2 = this.TryReadFileContent(text, diagnostics, out text3);
				if (text2 == null)
				{
					break;
				}
				string text4 = Path.GetDirectoryName(text3) ?? text3;
				AnalyzerConfig analyzerConfig = AnalyzerConfig.Parse(text2, text3);
				if (!analyzerConfig.IsGlobal)
				{
					if (instance2.Contains(text4))
					{
						diagnostics.Add(Diagnostic.Create(this.MessageProvider, this.MessageProvider.ERR_MultipleAnalyzerConfigsInSameDir, new object[] { text4 }));
						break;
					}
					instance2.Add(text4);
				}
				instance.Add(analyzerConfig);
			}
			instance2.Free();
			if (diagnostics.HasAnyErrors())
			{
				instance.Free();
				analyzerConfigSet = null;
				return false;
			}
			ImmutableArray<Diagnostic> immutableArray;
			analyzerConfigSet = AnalyzerConfigSet.Create<ArrayBuilder<AnalyzerConfig>>(instance, out immutableArray);
			diagnostics.AddRange<Diagnostic>(immutableArray);
			return true;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00020535 File Offset: 0x0001E735
		[NullableContext(2)]
		internal Encoding GetFallbackEncoding()
		{
			if (this._fallbackEncoding.IsValueCreated)
			{
				return this._fallbackEncoding.Value;
			}
			return null;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00020554 File Offset: 0x0001E754
		[return: Nullable(2)]
		private string TryReadFileContent(string filePath, DiagnosticBag diagnostics, [Nullable(2)] out string normalizedPath)
		{
			string text;
			try
			{
				using (StreamReader streamReader = new StreamReader(this.OpenFileForReadWithSmallBufferOptimization(filePath, out normalizedPath), Encoding.UTF8))
				{
					text = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				diagnostics.Add(Diagnostic.Create(CommonCompiler.ToFileReadDiagnostics(this.MessageProvider, ex, filePath)));
				normalizedPath = null;
				text = null;
			}
			return text;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000205C4 File Offset: 0x0001E7C4
		private Stream OpenFileForReadWithSmallBufferOptimization(string filePath, out string normalizedFilePath)
		{
			return this.FileSystem.OpenFileEx(filePath, 3, 1, 3, 1, 0, out normalizedFilePath);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000205D8 File Offset: 0x0001E7D8
		[return: Nullable(2)]
		internal EmbeddedText TryReadEmbeddedFileContent(string filePath, DiagnosticBag diagnostics)
		{
			EmbeddedText embeddedText;
			try
			{
				string text;
				using (Stream stream = this.OpenFileForReadWithSmallBufferOptimization(filePath, out text))
				{
					ArraySegment<byte> arraySegment;
					if (stream.Length < 81920L && EncodedStringText.TryGetBytesFromStream(stream, out arraySegment))
					{
						embeddedText = EmbeddedText.FromBytes(filePath, arraySegment, this.Arguments.ChecksumAlgorithm);
					}
					else
					{
						embeddedText = EmbeddedText.FromStream(filePath, stream, this.Arguments.ChecksumAlgorithm);
					}
				}
			}
			catch (Exception ex)
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(CommonCompiler.ToFileReadDiagnostics(this.MessageProvider, ex, filePath)));
				embeddedText = null;
			}
			return embeddedText;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00020680 File Offset: 0x0001E880
		[return: Nullable(new byte[] { 0, 2 })]
		private ImmutableArray<EmbeddedText> AcquireEmbeddedTexts(Compilation compilation, DiagnosticBag diagnostics)
		{
			if (this.Arguments.EmbeddedFiles.IsEmpty)
			{
				return ImmutableArray<EmbeddedText>.Empty;
			}
			Dictionary<string, SyntaxTree> dictionary = new Dictionary<string, SyntaxTree>(this.Arguments.EmbeddedFiles.Length);
			OrderedSet<string> orderedSet = new OrderedSet<string>(this.Arguments.EmbeddedFiles.Select<CommandLineSourceFile, string>((CommandLineSourceFile e) => e.Path));
			foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
			{
				if (this.EmbeddedSourcePaths.Contains(syntaxTree.FilePath) && !dictionary.ContainsKey(syntaxTree.FilePath))
				{
					dictionary.Add(syntaxTree.FilePath, syntaxTree);
					this.ResolveEmbeddedFilesFromExternalSourceDirectives(syntaxTree, compilation.Options.SourceReferenceResolver, orderedSet, diagnostics);
				}
			}
			ImmutableArray<EmbeddedText>.Builder builder = ImmutableArray.CreateBuilder<EmbeddedText>(orderedSet.Count);
			foreach (string text in orderedSet)
			{
				SyntaxTree syntaxTree2;
				EmbeddedText embeddedText;
				if (dictionary.TryGetValue(text, ref syntaxTree2))
				{
					embeddedText = EmbeddedText.FromSource(text, syntaxTree2.GetText(default(CancellationToken)));
				}
				else
				{
					embeddedText = this.TryReadEmbeddedFileContent(text, diagnostics);
				}
				builder.Add(embeddedText);
			}
			return builder.MoveToImmutable();
		}

		// Token: 0x06000B2D RID: 2861
		protected abstract void ResolveEmbeddedFilesFromExternalSourceDirectives(SyntaxTree tree, SourceReferenceResolver resolver, OrderedSet<string> embeddedFiles, DiagnosticBag diagnostics);

		// Token: 0x06000B2E RID: 2862 RVA: 0x000207E8 File Offset: 0x0001E9E8
		private static IReadOnlySet<string> GetEmbeddedSourcePaths(CommandLineArguments arguments)
		{
			if (arguments.EmbeddedFiles.IsEmpty)
			{
				return SpecializedCollections.EmptyReadOnlySet<string>();
			}
			HashSet<string> hashSet = new HashSet<string>(arguments.EmbeddedFiles.Select<CommandLineSourceFile, string>((CommandLineSourceFile f) => f.Path));
			hashSet.IntersectWith(arguments.SourceFiles.Select<CommandLineSourceFile, string>((CommandLineSourceFile f) => f.Path));
			return SpecializedCollections.StronglyTypedReadOnlySet<string>(hashSet);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00020870 File Offset: 0x0001EA70
		internal static DiagnosticInfo ToFileReadDiagnostics(CommonMessageProvider messageProvider, Exception e, string filePath)
		{
			DiagnosticInfo diagnosticInfo;
			if (e is FileNotFoundException || e is DirectoryNotFoundException)
			{
				diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.ERR_FileNotFound, new object[] { filePath });
			}
			else if (e is InvalidDataException)
			{
				diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.ERR_BinaryFile, new object[] { filePath });
			}
			else
			{
				diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.ERR_NoSourceFile, new object[] { filePath, e.Message });
			}
			return diagnosticInfo;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000208E8 File Offset: 0x0001EAE8
		internal bool ReportDiagnostics(IEnumerable<Diagnostic> diagnostics, TextWriter consoleOutput, [Nullable(2)] ErrorLogger errorLoggerOpt, [Nullable(2)] Compilation compilation)
		{
			CommonCompiler.<>c__DisplayClass58_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.errorLoggerOpt = errorLoggerOpt;
			CS$<>8__locals1.consoleOutput = consoleOutput;
			CS$<>8__locals1.hasErrors = false;
			foreach (Diagnostic diagnostic in diagnostics)
			{
				this.<ReportDiagnostics>g__reportDiagnostic|58_0(diagnostic, (compilation == null) ? null : diagnostic.GetSuppressionInfo(compilation), ref CS$<>8__locals1);
			}
			return CS$<>8__locals1.hasErrors;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00020968 File Offset: 0x0001EB68
		private bool ReportDiagnostics(DiagnosticBag diagnostics, TextWriter consoleOutput, [Nullable(2)] ErrorLogger errorLoggerOpt, [Nullable(2)] Compilation compilation)
		{
			return this.ReportDiagnostics(diagnostics.ToReadOnly(), consoleOutput, errorLoggerOpt, compilation);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002097F File Offset: 0x0001EB7F
		internal bool ReportDiagnostics(IEnumerable<DiagnosticInfo> diagnostics, TextWriter consoleOutput, [Nullable(2)] ErrorLogger errorLoggerOpt, [Nullable(2)] Compilation compilation)
		{
			return this.ReportDiagnostics(Enumerable.Select<DiagnosticInfo, Diagnostic>(diagnostics, (DiagnosticInfo info) => Diagnostic.Create(info)), consoleOutput, errorLoggerOpt, compilation);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000209B0 File Offset: 0x0001EBB0
		internal static bool HasUnsuppressableErrors(DiagnosticBag diagnostics)
		{
			using (IEnumerator<Diagnostic> enumerator = diagnostics.AsEnumerable().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsUnsuppressableError())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00020A04 File Offset: 0x0001EC04
		internal static bool HasUnsuppressedErrors(DiagnosticBag diagnostics)
		{
			using (IEnumerator<Diagnostic> enumerator = diagnostics.AsEnumerable().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsUnsuppressedError)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00020A58 File Offset: 0x0001EC58
		protected virtual void PrintError(Diagnostic diagnostic, TextWriter consoleOutput)
		{
			consoleOutput.WriteLine(this.DiagnosticFormatter.Format(diagnostic, this.Culture));
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00020A74 File Offset: 0x0001EC74
		[return: Nullable(2)]
		public SarifErrorLogger GetErrorLogger(TextWriter consoleOutput, CancellationToken cancellationToken)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			Stream stream = this.OpenFile(this.Arguments.ErrorLogOptions.Path, instance, 2, 2, 7);
			SarifErrorLogger sarifErrorLogger;
			if (stream == null)
			{
				sarifErrorLogger = null;
			}
			else
			{
				string toolName = this.GetToolName();
				string compilerVersion = this.GetCompilerVersion();
				Version version = this.GetAssemblyVersion() ?? new Version();
				if (this.Arguments.ErrorLogOptions.SarifVersion == SarifVersion.Sarif1)
				{
					sarifErrorLogger = new SarifV1ErrorLogger(stream, toolName, compilerVersion, version, this.Culture);
				}
				else
				{
					sarifErrorLogger = new SarifV2ErrorLogger(stream, toolName, compilerVersion, version, this.Culture);
				}
			}
			this.ReportDiagnostics(instance.ToReadOnlyAndFree(), consoleOutput, sarifErrorLogger, null);
			return sarifErrorLogger;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00020B18 File Offset: 0x0001ED18
		public virtual int Run(TextWriter consoleOutput, CancellationToken cancellationToken = default(CancellationToken))
		{
			CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
			SarifErrorLogger sarifErrorLogger = null;
			int num;
			try
			{
				CultureInfo culture = this.Culture;
				if (culture != null)
				{
					CultureInfo.CurrentUICulture = culture;
				}
				ErrorLogOptions errorLogOptions = this.Arguments.ErrorLogOptions;
				if (((errorLogOptions != null) ? errorLogOptions.Path : null) != null)
				{
					sarifErrorLogger = this.GetErrorLogger(consoleOutput, cancellationToken);
					if (sarifErrorLogger == null)
					{
						return 1;
					}
				}
				num = this.RunCore(consoleOutput, sarifErrorLogger, cancellationToken);
			}
			catch (OperationCanceledException)
			{
				int err_CompileCancelled = this.MessageProvider.ERR_CompileCancelled;
				if (err_CompileCancelled > 0)
				{
					DiagnosticInfo diagnosticInfo = new DiagnosticInfo(this.MessageProvider, err_CompileCancelled);
					this.ReportDiagnostics(new DiagnosticInfo[] { diagnosticInfo }, consoleOutput, sarifErrorLogger, null);
				}
				num = 1;
			}
			finally
			{
				CultureInfo.CurrentUICulture = currentUICulture;
				if (sarifErrorLogger != null)
				{
					sarifErrorLogger.Dispose();
				}
			}
			return num;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00020BDC File Offset: 0x0001EDDC
		private protected virtual Compilation RunGenerators(Compilation input, ParseOptions parseOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators, AnalyzerConfigOptionsProvider analyzerConfigOptionsProvider, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts, DiagnosticBag generatorDiagnostics)
		{
			return input;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00020BE0 File Offset: 0x0001EDE0
		private int RunCore(TextWriter consoleOutput, [Nullable(2)] ErrorLogger errorLogger, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.Arguments.DisplayVersion)
			{
				this.PrintVersion(consoleOutput);
				return 0;
			}
			if (this.Arguments.DisplayLangVersions)
			{
				this.PrintLangVersions(consoleOutput);
				return 0;
			}
			if (this.Arguments.DisplayLogo)
			{
				this.PrintLogo(consoleOutput);
			}
			if (this.Arguments.DisplayHelp)
			{
				this.PrintHelp(consoleOutput);
				return 0;
			}
			if (this.ReportDiagnostics(this.Arguments.Errors, consoleOutput, errorLogger, null))
			{
				return 1;
			}
			TouchedFileLogger touchedFileLogger = ((this.Arguments.TouchedFilesPath != null) ? new TouchedFileLogger() : null);
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			AnalyzerConfigSet analyzerConfigSet = null;
			ImmutableArray<AnalyzerConfigOptionsResult> immutableArray = default(ImmutableArray<AnalyzerConfigOptionsResult>);
			AnalyzerConfigOptionsResult analyzerConfigOptionsResult = default(AnalyzerConfigOptionsResult);
			if (this.Arguments.AnalyzerConfigPaths.Length > 0)
			{
				if (!this.TryGetAnalyzerConfigSet(this.Arguments.AnalyzerConfigPaths, instance, out analyzerConfigSet))
				{
					this.ReportDiagnostics(instance, consoleOutput, errorLogger, null);
					return 1;
				}
				analyzerConfigOptionsResult = analyzerConfigSet.GlobalConfigOptions;
				immutableArray = this.Arguments.SourceFiles.SelectAsArray<CommandLineSourceFile, AnalyzerConfigOptionsResult>((CommandLineSourceFile f) => analyzerConfigSet.GetOptionsForSourcePath(f.Path));
				foreach (AnalyzerConfigOptionsResult analyzerConfigOptionsResult2 in immutableArray)
				{
					instance.AddRange<Diagnostic>(analyzerConfigOptionsResult2.Diagnostics);
				}
			}
			Compilation compilation = this.CreateCompilation(consoleOutput, touchedFileLogger, errorLogger, immutableArray, analyzerConfigOptionsResult);
			if (compilation == null)
			{
				return 1;
			}
			List<DiagnosticInfo> list = new List<DiagnosticInfo>();
			ImmutableArray<DiagnosticAnalyzer> immutableArray2;
			ImmutableArray<ISourceGenerator> immutableArray3;
			this.ResolveAnalyzersFromArguments(list, this.MessageProvider, this.Arguments.SkipAnalyzers, out immutableArray2, out immutableArray3);
			ImmutableArray<AdditionalTextFile> immutableArray4 = this.ResolveAdditionalFilesFromArguments(list, this.MessageProvider, touchedFileLogger);
			if (this.ReportDiagnostics(list, consoleOutput, errorLogger, compilation))
			{
				return 1;
			}
			ImmutableArray<EmbeddedText> immutableArray5 = this.AcquireEmbeddedTexts(compilation, instance);
			if (this.ReportDiagnostics(instance, consoleOutput, errorLogger, compilation))
			{
				return 1;
			}
			ImmutableArray<AdditionalText> immutableArray6 = ImmutableArray<AdditionalText>.CastUp<AdditionalTextFile>(immutableArray4);
			CancellationTokenSource cancellationTokenSource;
			bool flag;
			AnalyzerDriver analyzerDriver;
			this.CompileAndEmit(touchedFileLogger, ref compilation, immutableArray2, immutableArray3, immutableArray6, analyzerConfigSet, immutableArray, immutableArray5, instance, cancellationToken, out cancellationTokenSource, out flag, out analyzerDriver);
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			int num = (this.ReportDiagnostics(instance, consoleOutput, errorLogger, compilation) ? 1 : 0);
			foreach (AdditionalTextFile additionalTextFile in immutableArray4)
			{
				if (this.ReportDiagnostics(additionalTextFile.Diagnostics, consoleOutput, errorLogger, compilation))
				{
					num = 1;
				}
			}
			instance.Free();
			if (flag)
			{
				CommonCompiler.ReportAnalyzerExecutionTime(consoleOutput, analyzerDriver, this.Culture, compilation.Options.ConcurrentBuild);
			}
			return num;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00020E48 File Offset: 0x0001F048
		private static CompilerAnalyzerConfigOptionsProvider UpdateAnalyzerConfigOptionsProvider(CompilerAnalyzerConfigOptionsProvider existing, IEnumerable<SyntaxTree> syntaxTrees, [Nullable(0)] ImmutableArray<AnalyzerConfigOptionsResult> sourceFileAnalyzerConfigOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles = default(ImmutableArray<AdditionalText>), [Nullable(0)] ImmutableArray<AnalyzerConfigOptionsResult> additionalFileOptions = default(ImmutableArray<AnalyzerConfigOptionsResult>))
		{
			ImmutableDictionary<object, AnalyzerConfigOptions>.Builder builder = ImmutableDictionary.CreateBuilder<object, AnalyzerConfigOptions>();
			int i = 0;
			foreach (SyntaxTree syntaxTree in syntaxTrees)
			{
				ImmutableDictionary<string, string> analyzerOptions = sourceFileAnalyzerConfigOptions[i].AnalyzerOptions;
				if (analyzerOptions.Count > 0)
				{
					builder.Add(syntaxTree, new CompilerAnalyzerConfigOptions(analyzerOptions));
				}
				i++;
			}
			if (!additionalFiles.IsDefault)
			{
				for (i = 0; i < additionalFiles.Length; i++)
				{
					ImmutableDictionary<string, string> analyzerOptions2 = additionalFileOptions[i].AnalyzerOptions;
					if (analyzerOptions2.Count > 0)
					{
						builder.Add(additionalFiles[i], new CompilerAnalyzerConfigOptions(analyzerOptions2));
					}
				}
			}
			return existing.WithAdditionalTreeOptions(builder.ToImmutable());
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00020F1C File Offset: 0x0001F11C
		[NullableContext(2)]
		private void CompileAndEmit(TouchedFileLogger touchedFilesLogger, [Nullable(1)] ref Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<DiagnosticAnalyzer> analyzers, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTextFiles, AnalyzerConfigSet analyzerConfigSet, [Nullable(0)] ImmutableArray<AnalyzerConfigOptionsResult> sourceFileAnalyzerConfigOptions, [Nullable(new byte[] { 0, 2 })] ImmutableArray<EmbeddedText> embeddedTexts, [Nullable(1)] DiagnosticBag diagnostics, CancellationToken cancellationToken, out CancellationTokenSource analyzerCts, out bool reportAnalyzer, out AnalyzerDriver analyzerDriver)
		{
			analyzerCts = null;
			reportAnalyzer = false;
			analyzerDriver = null;
			compilation.GetDiagnostics(CompilationStage.Parse, false, diagnostics, cancellationToken);
			if (CommonCompiler.HasUnsuppressableErrors(diagnostics))
			{
				return;
			}
			DiagnosticBag diagnosticBag = null;
			if (!analyzers.IsEmpty || !generators.IsEmpty)
			{
				CompilerAnalyzerConfigOptionsProvider compilerAnalyzerConfigOptionsProvider = CompilerAnalyzerConfigOptionsProvider.Empty;
				if (this.Arguments.AnalyzerConfigPaths.Length > 0)
				{
					compilerAnalyzerConfigOptionsProvider = compilerAnalyzerConfigOptionsProvider.WithGlobalOptions(new CompilerAnalyzerConfigOptions(analyzerConfigSet.GetOptionsForSourcePath(string.Empty).AnalyzerOptions));
					ImmutableArray<AnalyzerConfigOptionsResult> immutableArray = additionalTextFiles.SelectAsArray<AdditionalText, AnalyzerConfigOptionsResult>((AdditionalText f) => analyzerConfigSet.GetOptionsForSourcePath(f.Path));
					foreach (AnalyzerConfigOptionsResult analyzerConfigOptionsResult in immutableArray)
					{
						diagnostics.AddRange<Diagnostic>(analyzerConfigOptionsResult.Diagnostics);
					}
					compilerAnalyzerConfigOptionsProvider = CommonCompiler.UpdateAnalyzerConfigOptionsProvider(compilerAnalyzerConfigOptionsProvider, compilation.SyntaxTrees, sourceFileAnalyzerConfigOptions, additionalTextFiles, immutableArray);
				}
				if (!generators.IsEmpty)
				{
					compilation = this.RunGenerators(compilation, this.Arguments.ParseOptions, generators, compilerAnalyzerConfigOptionsProvider, additionalTextFiles, diagnostics);
					bool flag = !this.Arguments.AnalyzerConfigPaths.IsEmpty;
					bool flag2 = !string.IsNullOrWhiteSpace(this.Arguments.GeneratedFilesOutputDirectory);
					List<SyntaxTree> list = Enumerable.ToList<SyntaxTree>(Enumerable.Skip<SyntaxTree>(compilation.SyntaxTrees, this.Arguments.SourceFiles.Length));
					ArrayBuilder<AnalyzerConfigOptionsResult> arrayBuilder = (flag ? ArrayBuilder<AnalyzerConfigOptionsResult>.GetInstance(list.Count) : null);
					ArrayBuilder<EmbeddedText> instance = ArrayBuilder<EmbeddedText>.GetInstance(list.Count);
					try
					{
						foreach (SyntaxTree syntaxTree in list)
						{
							cancellationToken.ThrowIfCancellationRequested();
							SourceText text = syntaxTree.GetText(cancellationToken);
							instance.Add(EmbeddedText.FromSource(syntaxTree.FilePath, text));
							if (arrayBuilder != null)
							{
								arrayBuilder.Add(analyzerConfigSet.GetOptionsForSourcePath(syntaxTree.FilePath));
							}
							if (flag2)
							{
								string text2 = Path.Combine(this.Arguments.GeneratedFilesOutputDirectory, syntaxTree.FilePath);
								if (Directory.Exists(this.Arguments.GeneratedFilesOutputDirectory))
								{
									Directory.CreateDirectory(Path.GetDirectoryName(text2));
								}
								Stream stream = this.OpenFile(text2, diagnostics, 2, 2, 7);
								if (stream != null)
								{
									using (new NoThrowStreamDisposer(stream, text2, diagnostics, this.MessageProvider))
									{
										using (StreamWriter streamWriter = new StreamWriter(stream, syntaxTree.Encoding))
										{
											text.Write(streamWriter, cancellationToken);
											if (touchedFilesLogger != null)
											{
												touchedFilesLogger.AddWritten(text2);
											}
										}
									}
								}
							}
						}
						embeddedTexts = embeddedTexts.AddRange(instance);
						if (arrayBuilder != null)
						{
							compilerAnalyzerConfigOptionsProvider = CommonCompiler.UpdateAnalyzerConfigOptionsProvider(compilerAnalyzerConfigOptionsProvider, list, arrayBuilder.ToImmutable(), default(ImmutableArray<AdditionalText>), default(ImmutableArray<AnalyzerConfigOptionsResult>));
						}
					}
					finally
					{
						if (arrayBuilder != null)
						{
							arrayBuilder.Free();
						}
						instance.Free();
					}
				}
				AnalyzerOptions analyzerOptions = this.CreateAnalyzerOptions(additionalTextFiles, compilerAnalyzerConfigOptionsProvider);
				if (!analyzers.IsEmpty)
				{
					analyzerCts = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancellationToken });
					diagnosticBag = new DiagnosticBag();
					SeverityFilter severityFilter = SeverityFilter.Hidden;
					if (this.Arguments.ErrorLogPath == null)
					{
						severityFilter |= SeverityFilter.Info;
					}
					analyzerDriver = AnalyzerDriver.CreateAndAttachToCompilation(compilation, analyzers, analyzerOptions, new AnalyzerManager(analyzers), new Action<Diagnostic>(diagnosticBag.Add), this.Arguments.ReportAnalyzer, severityFilter, out compilation, analyzerCts.Token);
					reportAnalyzer = this.Arguments.ReportAnalyzer && !analyzers.IsEmpty;
				}
			}
			compilation.GetDiagnostics(CompilationStage.Declare, false, diagnostics, cancellationToken);
			if (CommonCompiler.HasUnsuppressableErrors(diagnostics))
			{
				return;
			}
			cancellationToken.ThrowIfCancellationRequested();
			string outputFileName = this.GetOutputFileName(compilation, cancellationToken);
			string outputFilePath = this.Arguments.GetOutputFilePath(outputFileName);
			string pdbFilePath = this.Arguments.GetPdbFilePath(outputFileName);
			string documentationPath = this.Arguments.DocumentationPath;
			NoThrowStreamDisposer noThrowStreamDisposer2 = null;
			try
			{
				EmitOptions emitOptions = this.Arguments.EmitOptions.WithOutputNameOverride(outputFileName).WithPdbFilePath(PathUtilities.NormalizePathPrefix(pdbFilePath, this.Arguments.PathMap));
				if (this.Arguments.ParseOptions.Features.ContainsKey("pdb-path-determinism") && !string.IsNullOrEmpty(emitOptions.PdbFilePath))
				{
					emitOptions = emitOptions.WithPdbFilePath(Path.GetFileName(emitOptions.PdbFilePath));
				}
				if (this.Arguments.SourceLink != null)
				{
					Stream stream2 = this.OpenFile(this.Arguments.SourceLink, diagnostics, 3, 1, 1);
					if (stream2 != null)
					{
						noThrowStreamDisposer2 = new NoThrowStreamDisposer(stream2, this.Arguments.SourceLink, diagnostics, this.MessageProvider);
					}
				}
				if (!PathUtilities.IsValidFilePath(pdbFilePath))
				{
					diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.FTL_InvalidInputFileName, Location.None, new object[] { pdbFilePath }));
				}
				CommonPEModuleBuilder commonPEModuleBuilder = compilation.CheckOptionsAndCreateModuleBuilder(diagnostics, this.Arguments.ManifestResources, emitOptions, null, (noThrowStreamDisposer2 != null) ? noThrowStreamDisposer2.Stream : null, embeddedTexts, null, cancellationToken);
				if (commonPEModuleBuilder != null)
				{
					bool flag3;
					try
					{
						flag3 = compilation.CompileMethods(commonPEModuleBuilder, this.Arguments.EmitPdb, emitOptions.EmitMetadataOnly, emitOptions.EmitTestCoverageData, diagnostics, null, cancellationToken);
						if (analyzerDriver != null && !diagnostics.IsEmptyWithoutResolution)
						{
							analyzerDriver.ApplyProgrammaticSuppressions(diagnostics, compilation);
						}
						if (CommonCompiler.HasUnsuppressedErrors(diagnostics))
						{
							flag3 = false;
						}
						if (flag3)
						{
							NoThrowStreamDisposer noThrowStreamDisposer3 = null;
							if (documentationPath != null)
							{
								Stream stream3 = this.OpenFile(documentationPath, diagnostics, 4, 2, 7);
								if (stream3 == null)
								{
									return;
								}
								try
								{
									stream3.SetLength(0L);
								}
								catch (Exception ex)
								{
									this.MessageProvider.ReportStreamWriteException(ex, documentationPath, diagnostics);
									return;
								}
								noThrowStreamDisposer3 = new NoThrowStreamDisposer(stream3, documentationPath, diagnostics, this.MessageProvider);
							}
							using (noThrowStreamDisposer3)
							{
								using (Stream win32Resources = CommonCompiler.GetWin32Resources(this.FileSystem, this.MessageProvider, this.Arguments, compilation, diagnostics))
								{
									if (CommonCompiler.HasUnsuppressableErrors(diagnostics))
									{
										return;
									}
									flag3 = compilation.GenerateResourcesAndDocumentationComments(commonPEModuleBuilder, (noThrowStreamDisposer3 != null) ? noThrowStreamDisposer3.Stream : null, win32Resources, false, emitOptions.OutputNameOverride, diagnostics, cancellationToken);
								}
							}
							if (noThrowStreamDisposer3 != null && noThrowStreamDisposer3.HasFailedToDispose)
							{
								return;
							}
							if (flag3)
							{
								compilation.ReportUnusedImports(diagnostics, cancellationToken);
							}
						}
						compilation.CompleteTrees(null);
						if (analyzerDriver != null)
						{
							ImmutableArray<Diagnostic> result = analyzerDriver.GetDiagnosticsAsync(compilation).Result;
							diagnostics.AddRange<Diagnostic>(result);
							if (!diagnostics.IsEmptyWithoutResolution)
							{
								analyzerDriver.ApplyProgrammaticSuppressions(diagnostics, compilation);
							}
						}
					}
					finally
					{
						commonPEModuleBuilder.CompilationFinished();
					}
					if (CommonCompiler.HasUnsuppressedErrors(diagnostics))
					{
						flag3 = false;
					}
					if (flag3)
					{
						CommonCompiler.CompilerEmitStreamProvider compilerEmitStreamProvider = new CommonCompiler.CompilerEmitStreamProvider(this, outputFilePath);
						CommonCompiler.CompilerEmitStreamProvider compilerEmitStreamProvider2 = (this.Arguments.EmitPdbFile ? new CommonCompiler.CompilerEmitStreamProvider(this, pdbFilePath) : null);
						string outputRefFilePath = this.Arguments.OutputRefFilePath;
						CommonCompiler.CompilerEmitStreamProvider compilerEmitStreamProvider3 = ((outputRefFilePath != null) ? new CommonCompiler.CompilerEmitStreamProvider(this, outputRefFilePath) : null);
						RSAParameters? rsaparameters = default(RSAParameters?);
						if (compilation.Options.StrongNameProvider != null && compilation.SignUsingBuilder && !compilation.Options.PublicSign)
						{
							rsaparameters = compilation.StrongNameKeys.PrivateKey;
						}
						emitOptions = emitOptions.WithFallbackSourceFileEncoding(this.GetFallbackEncoding());
						flag3 = compilation.SerializeToPeStream(commonPEModuleBuilder, compilerEmitStreamProvider, compilerEmitStreamProvider3, compilerEmitStreamProvider2, null, null, diagnostics, emitOptions, rsaparameters, cancellationToken);
						compilerEmitStreamProvider.Close(diagnostics);
						if (compilerEmitStreamProvider3 != null)
						{
							compilerEmitStreamProvider3.Close(diagnostics);
						}
						if (compilerEmitStreamProvider2 != null)
						{
							compilerEmitStreamProvider2.Close(diagnostics);
						}
						if (flag3 && touchedFilesLogger != null)
						{
							if (compilerEmitStreamProvider2 != null)
							{
								touchedFilesLogger.AddWritten(pdbFilePath);
							}
							if (compilerEmitStreamProvider3 != null)
							{
								touchedFilesLogger.AddWritten(outputRefFilePath);
							}
							touchedFilesLogger.AddWritten(outputFilePath);
						}
					}
				}
				if (CommonCompiler.HasUnsuppressableErrors(diagnostics))
				{
					return;
				}
			}
			finally
			{
				if (noThrowStreamDisposer2 != null)
				{
					noThrowStreamDisposer2.Dispose();
				}
			}
			if (noThrowStreamDisposer2 != null && noThrowStreamDisposer2.HasFailedToDispose)
			{
				return;
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (diagnosticBag != null)
			{
				diagnostics.AddRange(diagnosticBag);
				if (CommonCompiler.HasUnsuppressableErrors(diagnosticBag))
				{
					return;
				}
			}
			cancellationToken.ThrowIfCancellationRequested();
			this.WriteTouchedFiles(diagnostics, touchedFilesLogger, documentationPath);
			return;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000217DC File Offset: 0x0001F9DC
		protected virtual AnalyzerOptions CreateAnalyzerOptions([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTextFiles, AnalyzerConfigOptionsProvider analyzerConfigOptionsProvider)
		{
			return new AnalyzerOptions(additionalTextFiles, analyzerConfigOptionsProvider);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000217E8 File Offset: 0x0001F9E8
		[NullableContext(2)]
		private bool WriteTouchedFiles([Nullable(1)] DiagnosticBag diagnostics, TouchedFileLogger touchedFilesLogger, string finalXmlFilePath)
		{
			if (this.Arguments.TouchedFilesPath != null)
			{
				if (finalXmlFilePath != null)
				{
					touchedFilesLogger.AddWritten(finalXmlFilePath);
				}
				string text = this.Arguments.TouchedFilesPath + ".read";
				string text2 = this.Arguments.TouchedFilesPath + ".write";
				Stream stream = this.OpenFile(text, diagnostics, 4, 3, 0);
				Stream stream2 = this.OpenFile(text2, diagnostics, 4, 3, 0);
				if (stream == null || stream2 == null)
				{
					return false;
				}
				string text3 = null;
				try
				{
					text3 = text;
					using (StreamWriter streamWriter = new StreamWriter(stream))
					{
						touchedFilesLogger.WriteReadPaths(streamWriter);
					}
					text3 = text2;
					using (StreamWriter streamWriter2 = new StreamWriter(stream2))
					{
						touchedFilesLogger.WriteWrittenPaths(streamWriter2);
					}
				}
				catch (Exception ex)
				{
					this.MessageProvider.ReportStreamWriteException(ex, text3, diagnostics);
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000218E8 File Offset: 0x0001FAE8
		[return: Nullable(new byte[] { 0, 1 })]
		protected virtual ImmutableArray<AdditionalTextFile> ResolveAdditionalFilesFromArguments(List<DiagnosticInfo> diagnostics, CommonMessageProvider messageProvider, [Nullable(2)] TouchedFileLogger touchedFilesLogger)
		{
			ImmutableArray<AdditionalTextFile>.Builder builder = ImmutableArray.CreateBuilder<AdditionalTextFile>();
			foreach (CommandLineSourceFile commandLineSourceFile in this.Arguments.AdditionalFiles)
			{
				builder.Add(new AdditionalTextFile(commandLineSourceFile, this));
			}
			return builder.ToImmutableArray<AdditionalTextFile>();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00021938 File Offset: 0x0001FB38
		private static void ReportAnalyzerExecutionTime(TextWriter consoleOutput, AnalyzerDriver analyzerDriver, CultureInfo culture, bool isConcurrentBuild)
		{
			if (analyzerDriver.AnalyzerExecutionTimes.IsEmpty)
			{
				return;
			}
			double num = Enumerable.Sum<KeyValuePair<DiagnosticAnalyzer, TimeSpan>>(analyzerDriver.AnalyzerExecutionTimes, (KeyValuePair<DiagnosticAnalyzer, TimeSpan> kvp) => kvp.Value.TotalSeconds);
			Func<double, string> func = (double d) => d.ToString("##0.000", culture);
			consoleOutput.WriteLine();
			consoleOutput.WriteLine(string.Format(CodeAnalysisResources.AnalyzerTotalExecutionTime, func.Invoke(num)));
			if (isConcurrentBuild)
			{
				consoleOutput.WriteLine(CodeAnalysisResources.MultithreadedAnalyzerExecutionNote);
			}
			IEnumerable<IGrouping<Assembly, KeyValuePair<DiagnosticAnalyzer, TimeSpan>>> enumerable = Enumerable.OrderByDescending<IGrouping<Assembly, KeyValuePair<DiagnosticAnalyzer, TimeSpan>>, long>(Enumerable.GroupBy<KeyValuePair<DiagnosticAnalyzer, TimeSpan>, Assembly>(analyzerDriver.AnalyzerExecutionTimes, (KeyValuePair<DiagnosticAnalyzer, TimeSpan> kvp) => IntrospectionExtensions.GetTypeInfo(kvp.Key.GetType()).Assembly), (IGrouping<Assembly, KeyValuePair<DiagnosticAnalyzer, TimeSpan>> kvp) => Enumerable.Sum<KeyValuePair<DiagnosticAnalyzer, TimeSpan>>(kvp, (KeyValuePair<DiagnosticAnalyzer, TimeSpan> entry) => entry.Value.Ticks));
			consoleOutput.WriteLine();
			func = delegate(double d)
			{
				if (d >= 0.001)
				{
					return string.Format(culture, "{0,8:##0.000}", d);
				}
				return string.Format(culture, "{0,8:<0.000}", 0.001);
			};
			Func<int, string> func2 = (int i) => string.Format("{0,5}", (i < 1) ? "<1" : i.ToString());
			Func<string, string> func3 = ([Nullable(2)] string s) => "   " + s;
			string text = string.Format("{0,8}", CodeAnalysisResources.AnalyzerExecutionTimeColumnHeader);
			string text2 = string.Format("{0,5}", "%");
			string text3 = func3.Invoke(CodeAnalysisResources.AnalyzerNameColumnHeader);
			consoleOutput.WriteLine(text + text2 + text3);
			foreach (IGrouping<Assembly, KeyValuePair<DiagnosticAnalyzer, TimeSpan>> grouping in enumerable)
			{
				double num2 = Enumerable.Sum<KeyValuePair<DiagnosticAnalyzer, TimeSpan>>(grouping, (KeyValuePair<DiagnosticAnalyzer, TimeSpan> kvp) => kvp.Value.TotalSeconds);
				int num3 = (int)(num2 * 100.0 / num);
				text = func.Invoke(num2);
				text2 = func2.Invoke(num3);
				text3 = func3.Invoke(grouping.Key.FullName);
				consoleOutput.WriteLine(text + text2 + text3);
				foreach (KeyValuePair<DiagnosticAnalyzer, TimeSpan> keyValuePair in Enumerable.OrderByDescending<KeyValuePair<DiagnosticAnalyzer, TimeSpan>, TimeSpan>(grouping, (KeyValuePair<DiagnosticAnalyzer, TimeSpan> kvp) => kvp.Value))
				{
					num2 = keyValuePair.Value.TotalSeconds;
					num3 = (int)(num2 * 100.0 / num);
					text = func.Invoke(num2);
					text2 = func2.Invoke(num3);
					string text4 = string.Join(", ", Enumerable.OrderBy<string, string>(Enumerable.Distinct<string>(keyValuePair.Key.SupportedDiagnostics.Select<DiagnosticDescriptor, string>((DiagnosticDescriptor d) => d.Id)), (string id) => id));
					text3 = func3.Invoke(string.Format("   {0} ({1})", keyValuePair.Key, text4));
					consoleOutput.WriteLine(text + text2 + text3);
				}
				consoleOutput.WriteLine();
			}
		}

		// Token: 0x06000B40 RID: 2880
		protected abstract string GetOutputFileName(Compilation compilation, CancellationToken cancellationToken);

		// Token: 0x06000B41 RID: 2881 RVA: 0x00021CA0 File Offset: 0x0001FEA0
		[return: Nullable(2)]
		private Stream OpenFile(string filePath, DiagnosticBag diagnostics, FileMode mode = 3, FileAccess access = 3, FileShare share = 0)
		{
			Stream stream;
			try
			{
				stream = this.FileSystem.OpenFile(filePath, mode, access, share);
			}
			catch (Exception ex)
			{
				this.MessageProvider.ReportStreamWriteException(ex, filePath, diagnostics);
				stream = null;
			}
			return stream;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00021CE8 File Offset: 0x0001FEE8
		[return: Nullable(2)]
		internal static Stream GetWin32ResourcesInternal(ICommonCompilerFileSystem fileSystem, CommonMessageProvider messageProvider, CommandLineArguments arguments, Compilation compilation, out IEnumerable<DiagnosticInfo> errors)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			Stream win32Resources = CommonCompiler.GetWin32Resources(fileSystem, messageProvider, arguments, compilation, instance);
			errors = instance.ToReadOnlyAndFree().SelectAsArray<Diagnostic, DiagnosticInfo>((Diagnostic diag) => new DiagnosticInfo(messageProvider, diag.IsWarningAsError, diag.Code, (object[])diag.Arguments));
			return win32Resources;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00021D38 File Offset: 0x0001FF38
		[return: Nullable(2)]
		private static Stream GetWin32Resources(ICommonCompilerFileSystem fileSystem, CommonMessageProvider messageProvider, CommandLineArguments arguments, Compilation compilation, DiagnosticBag diagnostics)
		{
			if (arguments.Win32ResourceFile != null)
			{
				return CommonCompiler.OpenStream(fileSystem, messageProvider, arguments.Win32ResourceFile, arguments.BaseDirectory, messageProvider.ERR_CantOpenWin32Resource, diagnostics);
			}
			using (Stream stream = CommonCompiler.OpenManifestStream(fileSystem, messageProvider, compilation.Options.OutputKind, arguments, diagnostics))
			{
				using (Stream stream2 = CommonCompiler.OpenStream(fileSystem, messageProvider, arguments.Win32Icon, arguments.BaseDirectory, messageProvider.ERR_CantOpenWin32Icon, diagnostics))
				{
					try
					{
						return compilation.CreateDefaultWin32Resources(true, arguments.NoWin32Manifest, stream, stream2);
					}
					catch (Exception ex)
					{
						diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_ErrorBuildingWin32Resource, Location.None, new object[] { ex.Message }));
					}
				}
			}
			return null;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00021E18 File Offset: 0x00020018
		[return: Nullable(2)]
		private static Stream OpenManifestStream(ICommonCompilerFileSystem fileSystem, CommonMessageProvider messageProvider, OutputKind outputKind, CommandLineArguments arguments, DiagnosticBag diagnostics)
		{
			if (!outputKind.IsNetModule())
			{
				return CommonCompiler.OpenStream(fileSystem, messageProvider, arguments.Win32Manifest, arguments.BaseDirectory, messageProvider.ERR_CantOpenWin32Manifest, diagnostics);
			}
			return null;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00021E40 File Offset: 0x00020040
		[return: Nullable(2)]
		private static Stream OpenStream(ICommonCompilerFileSystem fileSystem, CommonMessageProvider messageProvider, [Nullable(2)] string path, [Nullable(2)] string baseDirectory, int errorCode, DiagnosticBag diagnostics)
		{
			if (path == null)
			{
				return null;
			}
			string text = CommonCompiler.ResolveRelativePath(messageProvider, path, baseDirectory, diagnostics);
			if (text == null)
			{
				return null;
			}
			try
			{
				return fileSystem.OpenFile(text, 3, 1, 1);
			}
			catch (Exception ex)
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(errorCode, Location.None, new object[] { text, ex.Message }));
			}
			return null;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00021EB0 File Offset: 0x000200B0
		[return: Nullable(2)]
		private static string ResolveRelativePath(CommonMessageProvider messageProvider, string path, [Nullable(2)] string baseDirectory, DiagnosticBag diagnostics)
		{
			string text = FileUtilities.ResolveRelativePath(path, baseDirectory);
			if (text == null)
			{
				diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.FTL_InvalidInputFileName, Location.None, new object[] { path ?? "" }));
			}
			return text;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00021EF3 File Offset: 0x000200F3
		internal static bool TryGetCompilerDiagnosticCode(string diagnosticId, string expectedPrefix, out uint code)
		{
			code = 0U;
			return diagnosticId.StartsWith(expectedPrefix, 4) && uint.TryParse(diagnosticId.Substring(expectedPrefix.Length), ref code);
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00021F16 File Offset: 0x00020116
		protected virtual CultureInfo Culture
		{
			get
			{
				return this.Arguments.PreferredUILang ?? CultureInfo.CurrentUICulture;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00021F2C File Offset: 0x0002012C
		private static void EmitDeterminismKey(CommandLineArguments args, string[] rawArgs, string baseDirectory, CommandLineParser parser)
		{
			string text = CommonCompiler.CreateDeterminismKey(args, rawArgs, baseDirectory, parser);
			using (FileStream fileStream = File.Create(Path.Combine(args.OutputDirectory, args.OutputFileName + ".key")))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00021F98 File Offset: 0x00020198
		private static string CreateDeterminismKey(CommandLineArguments args, string[] rawArgs, string baseDirectory, CommandLineParser parser)
		{
			List<Diagnostic> list = new List<Diagnostic>();
			List<string> list2 = new List<string>();
			parser.FlattenArgs(rawArgs, list, list2, null, baseDirectory, null);
			StringBuilder stringBuilder = new StringBuilder();
			string text = ((!string.IsNullOrEmpty(args.OutputFileName)) ? Path.GetFileNameWithoutExtension(Path.GetFileName(args.OutputFileName)) : ("no-output-name-" + Guid.NewGuid().ToString()));
			stringBuilder.AppendLine(text ?? "");
			stringBuilder.AppendLine("Command Line:");
			foreach (string text2 in list2)
			{
				stringBuilder.AppendLine("\t" + text2);
			}
			stringBuilder.AppendLine("Source Files:");
			MD5 md = MD5.Create();
			foreach (CommandLineSourceFile commandLineSourceFile in args.SourceFiles)
			{
				string fileName = Path.GetFileName(commandLineSourceFile.Path);
				string text3;
				try
				{
					byte[] array = File.ReadAllBytes(commandLineSourceFile.Path);
					text3 = BitConverter.ToString(md.ComputeHash(array)).Replace("-", "");
				}
				catch (Exception ex)
				{
					text3 = "Could not compute " + ex.Message;
				}
				stringBuilder.AppendLine("\t" + fileName + " - " + text3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00022144 File Offset: 0x00020344
		[CompilerGenerated]
		private void <ReportDiagnostics>g__reportDiagnostic|58_0(Diagnostic diag, [Nullable(2)] SuppressionInfo suppressionInfo, ref CommonCompiler.<>c__DisplayClass58_0 A_3)
		{
			if (this._reportedDiagnostics.Contains(diag))
			{
				return;
			}
			if (diag.Severity == DiagnosticSeverity.Hidden)
			{
				return;
			}
			ErrorLogger errorLoggerOpt = A_3.errorLoggerOpt;
			if (errorLoggerOpt != null)
			{
				errorLoggerOpt.LogDiagnostic(diag, suppressionInfo);
			}
			if (diag.ProgrammaticSuppressionInfo != null)
			{
				foreach (ValueTuple<string, LocalizableString> valueTuple in diag.ProgrammaticSuppressionInfo.Suppressions)
				{
					string item = valueTuple.Item1;
					LocalizableString item2 = valueTuple.Item2;
					CommonCompiler.SuppressionDiagnostic suppressionDiagnostic = new CommonCompiler.SuppressionDiagnostic(diag, item, item2);
					if (this._reportedDiagnostics.Add(suppressionDiagnostic))
					{
						this.PrintError(suppressionDiagnostic, A_3.consoleOutput);
					}
				}
				this._reportedDiagnostics.Add(diag);
				return;
			}
			if (diag.IsSuppressed)
			{
				return;
			}
			if (diag.Severity == DiagnosticSeverity.Error)
			{
				A_3.hasErrors = true;
			}
			this.PrintError(diag, A_3.consoleOutput);
			this._reportedDiagnostics.Add(diag);
		}

		// Token: 0x04000325 RID: 805
		internal const int Failed = 1;

		// Token: 0x04000326 RID: 806
		internal const int Succeeded = 0;

		// Token: 0x04000327 RID: 807
		private readonly Lazy<Encoding> _fallbackEncoding = new Lazy<Encoding>(new Func<Encoding>(EncodedStringText.CreateFallbackEncoding));

		// Token: 0x0400032D RID: 813
		private readonly HashSet<Diagnostic> _reportedDiagnostics = new HashSet<Diagnostic>();

		// Token: 0x0200056A RID: 1386
		[Nullable(0)]
		private sealed class CompilerEmitStreamProvider : Compilation.EmitStreamProvider
		{
			// Token: 0x0600346B RID: 13419 RVA: 0x00094683 File Offset: 0x00092883
			internal CompilerEmitStreamProvider(CommonCompiler compiler, string filePath)
			{
				this._compiler = compiler;
				this._filePath = filePath;
			}

			// Token: 0x0600346C RID: 13420 RVA: 0x0009469C File Offset: 0x0009289C
			public void Close(DiagnosticBag diagnostics)
			{
				try
				{
					Stream streamToDispose = this._streamToDispose;
					if (streamToDispose != null)
					{
						streamToDispose.Dispose();
					}
				}
				catch (Exception ex)
				{
					CommonMessageProvider messageProvider = this._compiler.MessageProvider;
					DiagnosticInfo diagnosticInfo = new DiagnosticInfo(messageProvider, messageProvider.ERR_OutputWriteFailed, new object[] { this._filePath, ex.Message });
					diagnostics.Add(messageProvider.CreateDiagnostic(diagnosticInfo));
				}
			}

			// Token: 0x17000EED RID: 3821
			// (get) Token: 0x0600346D RID: 13421 RVA: 0x00094710 File Offset: 0x00092910
			[Nullable(2)]
			public override Stream Stream
			{
				[NullableContext(2)]
				get
				{
					return null;
				}
			}

			// Token: 0x0600346E RID: 13422 RVA: 0x00094714 File Offset: 0x00092914
			[return: Nullable(2)]
			protected override Stream CreateStream(DiagnosticBag diagnostics)
			{
				Stream stream;
				try
				{
					try
					{
						stream = this.OpenFileStream();
					}
					catch (IOException ex)
					{
						try
						{
							if (PathUtilities.IsUnixLikePlatform)
							{
								File.Delete(this._filePath);
							}
							else if (ex.HResult == -2147024864)
							{
								string text = Path.Combine(Path.GetDirectoryName(this._filePath), Guid.NewGuid().ToString() + "_" + Path.GetFileName(this._filePath));
								File.Move(this._filePath, text);
								File.SetAttributes(text, 2);
								File.Delete(text);
							}
						}
						catch
						{
							this.ReportOpenFileDiagnostic(diagnostics, ex);
							return null;
						}
						stream = this.OpenFileStream();
					}
				}
				catch (Exception ex2)
				{
					this.ReportOpenFileDiagnostic(diagnostics, ex2);
					stream = null;
				}
				return stream;
			}

			// Token: 0x0600346F RID: 13423 RVA: 0x000947F4 File Offset: 0x000929F4
			private Stream OpenFileStream()
			{
				return this._streamToDispose = this._compiler.FileSystem.OpenFile(this._filePath, 2, 3, 0);
			}

			// Token: 0x06003470 RID: 13424 RVA: 0x00094824 File Offset: 0x00092A24
			private void ReportOpenFileDiagnostic(DiagnosticBag diagnostics, Exception e)
			{
				CommonMessageProvider messageProvider = this._compiler.MessageProvider;
				diagnostics.Add(messageProvider.CreateDiagnostic(messageProvider.ERR_CantOpenFileWrite, Location.None, new object[] { this._filePath, e.Message }));
			}

			// Token: 0x040015A0 RID: 5536
			private readonly CommonCompiler _compiler;

			// Token: 0x040015A1 RID: 5537
			private readonly string _filePath;

			// Token: 0x040015A2 RID: 5538
			[Nullable(2)]
			private Stream _streamToDispose;
		}

		// Token: 0x0200056B RID: 1387
		[Nullable(0)]
		internal sealed class CompilerRelativePathResolver : RelativePathResolver
		{
			// Token: 0x17000EEE RID: 3822
			// (get) Token: 0x06003471 RID: 13425 RVA: 0x0009486C File Offset: 0x00092A6C
			internal ICommonCompilerFileSystem FileSystem { get; }

			// Token: 0x06003472 RID: 13426 RVA: 0x00094874 File Offset: 0x00092A74
			internal CompilerRelativePathResolver(ICommonCompilerFileSystem fileSystem, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> searchPaths, [Nullable(2)] string baseDirectory)
				: base(searchPaths, baseDirectory)
			{
				this.FileSystem = fileSystem;
			}

			// Token: 0x06003473 RID: 13427 RVA: 0x00094885 File Offset: 0x00092A85
			protected override bool FileExists(string fullPath)
			{
				return this.FileSystem.FileExists(fullPath);
			}
		}

		// Token: 0x0200056C RID: 1388
		[Nullable(0)]
		internal sealed class ExistingReferencesResolver : MetadataReferenceResolver, IEquatable<CommonCompiler.ExistingReferencesResolver>
		{
			// Token: 0x06003474 RID: 13428 RVA: 0x00094893 File Offset: 0x00092A93
			public ExistingReferencesResolver(MetadataReferenceResolver resolver, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> availableReferences)
			{
				this._resolver = resolver;
				this._availableReferences = availableReferences;
				this._lazyAvailableReferences = new Lazy<HashSet<AssemblyIdentity>>(() => new HashSet<AssemblyIdentity>(Enumerable.Select(Enumerable.Where(this._availableReferences.Select((MetadataReference reference) => new
				{
					reference = reference,
					identity = CommonCompiler.ExistingReferencesResolver.TryGetIdentity(reference)
				}), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.identity != null), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.identity)));
			}

			// Token: 0x06003475 RID: 13429 RVA: 0x000948C0 File Offset: 0x00092AC0
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<PortableExecutableReference> ResolveReference(string reference, [Nullable(2)] string baseFilePath, MetadataReferenceProperties properties)
			{
				return this._resolver.ResolveReference(reference, baseFilePath, properties).WhereAsArray<PortableExecutableReference>((PortableExecutableReference r) => this._lazyAvailableReferences.Value.Contains(CommonCompiler.ExistingReferencesResolver.TryGetIdentity(r)));
			}

			// Token: 0x06003476 RID: 13430 RVA: 0x000948E4 File Offset: 0x00092AE4
			[return: Nullable(2)]
			private static AssemblyIdentity TryGetIdentity(MetadataReference metadataReference)
			{
				PortableExecutableReference portableExecutableReference = metadataReference as PortableExecutableReference;
				if (portableExecutableReference == null || portableExecutableReference.Properties.Kind != MetadataImageKind.Assembly)
				{
					return null;
				}
				AssemblyIdentity assemblyIdentity;
				try
				{
					assemblyIdentity = ((AssemblyMetadata)portableExecutableReference.GetMetadataNoCopy()).GetAssembly().Identity;
				}
				catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
				{
					assemblyIdentity = null;
				}
				return assemblyIdentity;
			}

			// Token: 0x06003477 RID: 13431 RVA: 0x00094964 File Offset: 0x00092B64
			public override int GetHashCode()
			{
				return this._resolver.GetHashCode();
			}

			// Token: 0x06003478 RID: 13432 RVA: 0x00094971 File Offset: 0x00092B71
			[NullableContext(2)]
			public bool Equals(CommonCompiler.ExistingReferencesResolver other)
			{
				return other != null && this._resolver.Equals(other._resolver) && this._availableReferences.SequenceEqual<MetadataReference, MetadataReference>(other._availableReferences, null);
			}

			// Token: 0x06003479 RID: 13433 RVA: 0x000949A0 File Offset: 0x00092BA0
			[NullableContext(2)]
			public override bool Equals(object other)
			{
				CommonCompiler.ExistingReferencesResolver existingReferencesResolver = other as CommonCompiler.ExistingReferencesResolver;
				return existingReferencesResolver != null && this.Equals(existingReferencesResolver);
			}

			// Token: 0x040015A4 RID: 5540
			private readonly MetadataReferenceResolver _resolver;

			// Token: 0x040015A5 RID: 5541
			[Nullable(new byte[] { 0, 1 })]
			private readonly ImmutableArray<MetadataReference> _availableReferences;

			// Token: 0x040015A6 RID: 5542
			private readonly Lazy<HashSet<AssemblyIdentity>> _lazyAvailableReferences;
		}

		// Token: 0x0200056D RID: 1389
		[Nullable(0)]
		internal sealed class LoggingMetadataFileReferenceResolver : MetadataReferenceResolver, IEquatable<CommonCompiler.LoggingMetadataFileReferenceResolver>
		{
			// Token: 0x0600347C RID: 13436 RVA: 0x00094A5C File Offset: 0x00092C5C
			public LoggingMetadataFileReferenceResolver(RelativePathResolver pathResolver, Func<string, MetadataReferenceProperties, PortableExecutableReference> provider, [Nullable(2)] TouchedFileLogger logger)
			{
				this._pathResolver = pathResolver;
				this._provider = provider;
				this._logger = logger;
			}

			// Token: 0x0600347D RID: 13437 RVA: 0x00094A7C File Offset: 0x00092C7C
			[return: Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<PortableExecutableReference> ResolveReference(string reference, [Nullable(2)] string baseFilePath, MetadataReferenceProperties properties)
			{
				string text = this._pathResolver.ResolvePath(reference, baseFilePath);
				if (text != null)
				{
					TouchedFileLogger logger = this._logger;
					if (logger != null)
					{
						logger.AddRead(text);
					}
					return ImmutableArray.Create<PortableExecutableReference>(this._provider.Invoke(text, properties));
				}
				return ImmutableArray<PortableExecutableReference>.Empty;
			}

			// Token: 0x0600347E RID: 13438 RVA: 0x00094AC4 File Offset: 0x00092CC4
			public override int GetHashCode()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600347F RID: 13439 RVA: 0x00094ACB File Offset: 0x00092CCB
			[NullableContext(2)]
			public bool Equals(CommonCompiler.LoggingMetadataFileReferenceResolver other)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003480 RID: 13440 RVA: 0x00094AD4 File Offset: 0x00092CD4
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				CommonCompiler.LoggingMetadataFileReferenceResolver loggingMetadataFileReferenceResolver = obj as CommonCompiler.LoggingMetadataFileReferenceResolver;
				return loggingMetadataFileReferenceResolver != null && this.Equals(loggingMetadataFileReferenceResolver);
			}

			// Token: 0x040015A7 RID: 5543
			[Nullable(2)]
			private readonly TouchedFileLogger _logger;

			// Token: 0x040015A8 RID: 5544
			private readonly RelativePathResolver _pathResolver;

			// Token: 0x040015A9 RID: 5545
			private readonly Func<string, MetadataReferenceProperties, PortableExecutableReference> _provider;
		}

		// Token: 0x0200056E RID: 1390
		[NullableContext(2)]
		[Nullable(0)]
		internal sealed class LoggingSourceFileResolver : SourceFileResolver
		{
			// Token: 0x06003481 RID: 13441 RVA: 0x00094AF4 File Offset: 0x00092CF4
			public LoggingSourceFileResolver([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> searchPaths, string baseDirectory, [Nullable(new byte[] { 0, 0, 1, 1 })] ImmutableArray<KeyValuePair<string, string>> pathMap, TouchedFileLogger logger)
				: base(searchPaths, baseDirectory, pathMap)
			{
				this._logger = logger;
			}

			// Token: 0x06003482 RID: 13442 RVA: 0x00094B07 File Offset: 0x00092D07
			protected override bool FileExists(string fullPath)
			{
				if (fullPath != null)
				{
					TouchedFileLogger logger = this._logger;
					if (logger != null)
					{
						logger.AddRead(fullPath);
					}
				}
				return base.FileExists(fullPath);
			}

			// Token: 0x06003483 RID: 13443 RVA: 0x00094B25 File Offset: 0x00092D25
			[NullableContext(1)]
			public CommonCompiler.LoggingSourceFileResolver WithBaseDirectory(string value)
			{
				if (!(base.BaseDirectory == value))
				{
					return new CommonCompiler.LoggingSourceFileResolver(base.SearchPaths, value, base.PathMap, this._logger);
				}
				return this;
			}

			// Token: 0x06003484 RID: 13444 RVA: 0x00094B4F File Offset: 0x00092D4F
			[NullableContext(1)]
			public CommonCompiler.LoggingSourceFileResolver WithSearchPaths([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> value)
			{
				if (!(base.SearchPaths == value))
				{
					return new CommonCompiler.LoggingSourceFileResolver(value, base.BaseDirectory, base.PathMap, this._logger);
				}
				return this;
			}

			// Token: 0x040015AA RID: 5546
			private readonly TouchedFileLogger _logger;
		}

		// Token: 0x0200056F RID: 1391
		[NullableContext(2)]
		[Nullable(0)]
		internal sealed class LoggingStrongNameFileSystem : StrongNameFileSystem
		{
			// Token: 0x06003485 RID: 13445 RVA: 0x00094B79 File Offset: 0x00092D79
			public LoggingStrongNameFileSystem(TouchedFileLogger logger, string customTempPath)
				: base(customTempPath)
			{
				this._loggerOpt = logger;
			}

			// Token: 0x06003486 RID: 13446 RVA: 0x00094B89 File Offset: 0x00092D89
			internal override bool FileExists(string fullPath)
			{
				if (fullPath != null)
				{
					TouchedFileLogger loggerOpt = this._loggerOpt;
					if (loggerOpt != null)
					{
						loggerOpt.AddRead(fullPath);
					}
				}
				return base.FileExists(fullPath);
			}

			// Token: 0x06003487 RID: 13447 RVA: 0x00094BA7 File Offset: 0x00092DA7
			[NullableContext(1)]
			internal override byte[] ReadAllBytes(string fullPath)
			{
				TouchedFileLogger loggerOpt = this._loggerOpt;
				if (loggerOpt != null)
				{
					loggerOpt.AddRead(fullPath);
				}
				return base.ReadAllBytes(fullPath);
			}

			// Token: 0x040015AB RID: 5547
			private readonly TouchedFileLogger _loggerOpt;
		}

		// Token: 0x02000570 RID: 1392
		[NullableContext(2)]
		[Nullable(0)]
		internal sealed class LoggingXmlFileResolver : XmlFileResolver
		{
			// Token: 0x06003488 RID: 13448 RVA: 0x00094BC2 File Offset: 0x00092DC2
			public LoggingXmlFileResolver(string baseDirectory, TouchedFileLogger logger)
				: base(baseDirectory)
			{
				this._logger = logger;
			}

			// Token: 0x06003489 RID: 13449 RVA: 0x00094BD2 File Offset: 0x00092DD2
			protected override bool FileExists(string fullPath)
			{
				if (fullPath != null)
				{
					TouchedFileLogger logger = this._logger;
					if (logger != null)
					{
						logger.AddRead(fullPath);
					}
				}
				return base.FileExists(fullPath);
			}

			// Token: 0x040015AC RID: 5548
			private readonly TouchedFileLogger _logger;
		}

		// Token: 0x02000571 RID: 1393
		[Nullable(0)]
		private sealed class SuppressionDiagnostic : Diagnostic
		{
			// Token: 0x0600348A RID: 13450 RVA: 0x00094BF0 File Offset: 0x00092DF0
			public SuppressionDiagnostic(Diagnostic originalDiagnostic, string suppressionId, LocalizableString suppressionJustification)
			{
				this._originalDiagnostic = originalDiagnostic;
				this._suppressionId = suppressionId;
				this._suppressionJustification = suppressionJustification;
			}

			// Token: 0x17000EEF RID: 3823
			// (get) Token: 0x0600348B RID: 13451 RVA: 0x00094C0D File Offset: 0x00092E0D
			public override DiagnosticDescriptor Descriptor
			{
				get
				{
					return CommonCompiler.SuppressionDiagnostic.s_suppressionDiagnosticDescriptor;
				}
			}

			// Token: 0x17000EF0 RID: 3824
			// (get) Token: 0x0600348C RID: 13452 RVA: 0x00094C14 File Offset: 0x00092E14
			public override string Id
			{
				get
				{
					return this.Descriptor.Id;
				}
			}

			// Token: 0x0600348D RID: 13453 RVA: 0x00094C24 File Offset: 0x00092E24
			public override string GetMessage([Nullable(2)] IFormatProvider formatProvider = null)
			{
				string text = CommonCompiler.SuppressionDiagnostic.s_suppressionDiagnosticDescriptor.MessageFormat.ToString(formatProvider);
				return string.Format(formatProvider, text, new object[]
				{
					this._originalDiagnostic.Id,
					this._originalDiagnostic.GetMessage(formatProvider),
					this._suppressionId,
					this._suppressionJustification.ToString(formatProvider)
				});
			}

			// Token: 0x17000EF1 RID: 3825
			// (get) Token: 0x0600348E RID: 13454 RVA: 0x00094C84 File Offset: 0x00092E84
			public override DiagnosticSeverity Severity
			{
				get
				{
					return DiagnosticSeverity.Info;
				}
			}

			// Token: 0x17000EF2 RID: 3826
			// (get) Token: 0x0600348F RID: 13455 RVA: 0x00094C87 File Offset: 0x00092E87
			public override bool IsSuppressed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000EF3 RID: 3827
			// (get) Token: 0x06003490 RID: 13456 RVA: 0x00094C8A File Offset: 0x00092E8A
			public override int WarningLevel
			{
				get
				{
					return Diagnostic.GetDefaultWarningLevel(DiagnosticSeverity.Info);
				}
			}

			// Token: 0x17000EF4 RID: 3828
			// (get) Token: 0x06003491 RID: 13457 RVA: 0x00094C92 File Offset: 0x00092E92
			public override Location Location
			{
				get
				{
					return this._originalDiagnostic.Location;
				}
			}

			// Token: 0x17000EF5 RID: 3829
			// (get) Token: 0x06003492 RID: 13458 RVA: 0x00094C9F File Offset: 0x00092E9F
			public override IReadOnlyList<Location> AdditionalLocations
			{
				get
				{
					return this._originalDiagnostic.AdditionalLocations;
				}
			}

			// Token: 0x17000EF6 RID: 3830
			// (get) Token: 0x06003493 RID: 13459 RVA: 0x00094CAC File Offset: 0x00092EAC
			[Nullable(new byte[] { 1, 1, 2 })]
			public override ImmutableDictionary<string, string> Properties
			{
				[return: Nullable(new byte[] { 1, 1, 2 })]
				get
				{
					return ImmutableDictionary<string, string>.Empty;
				}
			}

			// Token: 0x06003494 RID: 13460 RVA: 0x00094CB4 File Offset: 0x00092EB4
			[NullableContext(2)]
			public override bool Equals(Diagnostic obj)
			{
				if (this == obj)
				{
					return true;
				}
				CommonCompiler.SuppressionDiagnostic suppressionDiagnostic = obj as CommonCompiler.SuppressionDiagnostic;
				return suppressionDiagnostic != null && (object.Equals(this._originalDiagnostic, suppressionDiagnostic._originalDiagnostic) && object.Equals(this._suppressionId, suppressionDiagnostic._suppressionId)) && object.Equals(this._suppressionJustification, suppressionDiagnostic._suppressionJustification);
			}

			// Token: 0x06003495 RID: 13461 RVA: 0x00094D0C File Offset: 0x00092F0C
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				Diagnostic diagnostic = obj as Diagnostic;
				return diagnostic != null && this.Equals(diagnostic);
			}

			// Token: 0x06003496 RID: 13462 RVA: 0x00094D2C File Offset: 0x00092F2C
			public override int GetHashCode()
			{
				return Hash.Combine(this._originalDiagnostic.GetHashCode(), Hash.Combine(this._suppressionId.GetHashCode(), this._suppressionJustification.GetHashCode()));
			}

			// Token: 0x06003497 RID: 13463 RVA: 0x00094D59 File Offset: 0x00092F59
			internal override Diagnostic WithLocation(Location location)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003498 RID: 13464 RVA: 0x00094D60 File Offset: 0x00092F60
			internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06003499 RID: 13465 RVA: 0x00094D67 File Offset: 0x00092F67
			internal override Diagnostic WithIsSuppressed(bool isSuppressed)
			{
				throw new NotSupportedException();
			}

			// Token: 0x040015AD RID: 5549
			private static readonly DiagnosticDescriptor s_suppressionDiagnosticDescriptor = new DiagnosticDescriptor("SP0001", CodeAnalysisResources.SuppressionDiagnosticDescriptorTitle, CodeAnalysisResources.SuppressionDiagnosticDescriptorMessage, "ProgrammaticSuppression", DiagnosticSeverity.Info, true, null, null, Array.Empty<string>());

			// Token: 0x040015AE RID: 5550
			private readonly Diagnostic _originalDiagnostic;

			// Token: 0x040015AF RID: 5551
			private readonly string _suppressionId;

			// Token: 0x040015B0 RID: 5552
			private readonly LocalizableString _suppressionJustification;
		}
	}
}
