using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000FF RID: 255
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class CSharpCompiler : CommonCompiler
	{
		// Token: 0x0600134A RID: 4938 RVA: 0x00068A1C File Offset: 0x00066C1C
		protected CSharpCompiler(CSharpCommandLineParser parser, [global::System.Runtime.CompilerServices.Nullable(2)] string responseFile, string[] args, BuildPaths buildPaths, [global::System.Runtime.CompilerServices.Nullable(2)] string additionalReferenceDirectories, IAnalyzerAssemblyLoader assemblyLoader)
			: base(parser, responseFile, args, buildPaths, additionalReferenceDirectories, assemblyLoader)
		{
			this._diagnosticFormatter = new CommandLineDiagnosticFormatter(buildPaths.WorkingDirectory, this.Arguments.PrintFullPaths, this.Arguments.ShouldIncludeErrorEndLocation);
			this._tempDirectory = buildPaths.TempDirectory;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x00068A6D File Offset: 0x00066C6D
		public override DiagnosticFormatter DiagnosticFormatter
		{
			get
			{
				return this._diagnosticFormatter;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00068A75 File Offset: 0x00066C75
		protected internal new CSharpCommandLineArguments Arguments
		{
			get
			{
				return (CSharpCommandLineArguments)base.Arguments;
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00068A84 File Offset: 0x00066C84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override Compilation CreateCompilation([global::System.Runtime.CompilerServices.Nullable(1)] TextWriter consoleOutput, TouchedFileLogger touchedFilesLogger, ErrorLogger errorLogger, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<AnalyzerConfigOptionsResult> analyzerConfigOptions, AnalyzerConfigOptionsResult globalConfigOptions)
		{
			CSharpParseOptions parseOptions = this.Arguments.ParseOptions;
			CSharpParseOptions scriptParseOptions = parseOptions.WithKind(SourceCodeKind.Script);
			bool hadErrors = false;
			ImmutableArray<CommandLineSourceFile> sourceFiles = this.Arguments.SourceFiles;
			SyntaxTree[] trees = new SyntaxTree[sourceFiles.Length];
			string[] normalizedFilePaths = new string[sourceFiles.Length];
			DiagnosticBag diagnosticBag = DiagnosticBag.GetInstance();
			if (this.Arguments.CompilationOptions.ConcurrentBuild)
			{
				RoslynParallel.For(0, sourceFiles.Length, UICultureUtilities.WithCurrentUICulture<int>(delegate(int i)
				{
					trees[i] = this.ParseFile(parseOptions, scriptParseOptions, ref hadErrors, sourceFiles[i], diagnosticBag, out normalizedFilePaths[i]);
				}), CancellationToken.None);
			}
			else
			{
				for (int k = 0; k < sourceFiles.Length; k++)
				{
					trees[k] = this.ParseFile(parseOptions, scriptParseOptions, ref hadErrors, sourceFiles[k], diagnosticBag, out normalizedFilePaths[k]);
				}
			}
			if (base.ReportDiagnostics(diagnosticBag.ToReadOnlyAndFree(), consoleOutput, errorLogger, null))
			{
				return null;
			}
			List<DiagnosticInfo> list = new List<DiagnosticInfo>();
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			for (int j = 0; j < sourceFiles.Length; j++)
			{
				string text = normalizedFilePaths[j];
				if (!hashSet.Add(text))
				{
					list.Add(new DiagnosticInfo(base.MessageProvider, 2002, new object[] { this.Arguments.PrintFullPaths ? text : this._diagnosticFormatter.RelativizeNormalizedPath(text) }));
					trees[j] = null;
				}
			}
			if (this.Arguments.TouchedFilesPath != null)
			{
				foreach (string text2 in hashSet)
				{
					touchedFilesLogger.AddRead(text2);
				}
			}
			DesktopAssemblyIdentityComparer desktopAssemblyIdentityComparer = DesktopAssemblyIdentityComparer.Default;
			string appConfigPath = this.Arguments.AppConfigPath;
			if (appConfigPath != null)
			{
				try
				{
					using (FileStream fileStream = new FileStream(appConfigPath, 3, 1))
					{
						desktopAssemblyIdentityComparer = DesktopAssemblyIdentityComparer.LoadFromXml(fileStream);
					}
					if (touchedFilesLogger != null)
					{
						touchedFilesLogger.AddRead(appConfigPath);
					}
				}
				catch (Exception ex)
				{
					list.Add(new DiagnosticInfo(base.MessageProvider, 7093, new object[] { appConfigPath, ex.Message }));
				}
			}
			CommonCompiler.LoggingXmlFileResolver loggingXmlFileResolver = new CommonCompiler.LoggingXmlFileResolver(this.Arguments.BaseDirectory, touchedFilesLogger);
			CommonCompiler.LoggingSourceFileResolver loggingSourceFileResolver = new CommonCompiler.LoggingSourceFileResolver(ImmutableArray<string>.Empty, this.Arguments.BaseDirectory, this.Arguments.PathMap, touchedFilesLogger);
			MetadataReferenceResolver metadataReferenceResolver;
			List<MetadataReference> list2 = base.ResolveMetadataReferences(list, touchedFilesLogger, out metadataReferenceResolver);
			if (base.ReportDiagnostics(list, consoleOutput, errorLogger, null))
			{
				return null;
			}
			CommonCompiler.LoggingStrongNameFileSystem loggingStrongNameFileSystem = new CommonCompiler.LoggingStrongNameFileSystem(touchedFilesLogger, this._tempDirectory);
			CompilerSyntaxTreeOptionsProvider compilerSyntaxTreeOptionsProvider = new CompilerSyntaxTreeOptionsProvider(trees, analyzerConfigOptions, globalConfigOptions);
			return CSharpCompilation.Create(this.Arguments.CompilationName, trees.WhereNotNull<SyntaxTree>(), list2, this.Arguments.CompilationOptions.WithMetadataReferenceResolver(metadataReferenceResolver).WithAssemblyIdentityComparer(desktopAssemblyIdentityComparer).WithXmlReferenceResolver(loggingXmlFileResolver)
				.WithStrongNameProvider(this.Arguments.GetStrongNameProvider(loggingStrongNameFileSystem))
				.WithSourceReferenceResolver(loggingSourceFileResolver)
				.WithSyntaxTreeOptionsProvider(compilerSyntaxTreeOptionsProvider));
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00068DF4 File Offset: 0x00066FF4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		private SyntaxTree ParseFile(CSharpParseOptions parseOptions, CSharpParseOptions scriptParseOptions, ref bool addedDiagnostics, CommandLineSourceFile file, DiagnosticBag diagnostics, [global::System.Runtime.CompilerServices.Nullable(2)] out string normalizedFilePath)
		{
			List<DiagnosticInfo> list = new List<DiagnosticInfo>();
			SourceText sourceText = base.TryReadFileContent(file, list, out normalizedFilePath);
			if (sourceText == null)
			{
				foreach (DiagnosticInfo diagnosticInfo in list)
				{
					diagnostics.Add(base.MessageProvider.CreateDiagnostic(diagnosticInfo));
				}
				list.Clear();
				addedDiagnostics = true;
				return null;
			}
			return CSharpCompiler.ParseFile(parseOptions, scriptParseOptions, sourceText, file);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00068E78 File Offset: 0x00067078
		private static SyntaxTree ParseFile(CSharpParseOptions parseOptions, CSharpParseOptions scriptParseOptions, SourceText content, CommandLineSourceFile file)
		{
			SyntaxTree syntaxTree = SyntaxFactory.ParseSyntaxTree(content, file.IsScript ? scriptParseOptions : parseOptions, file.Path, default(CancellationToken));
			bool flag;
			syntaxTree.GetMappedLineSpanAndVisibility(default(TextSpan), out flag);
			return syntaxTree;
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00068EBC File Offset: 0x000670BC
		protected override string GetOutputFileName(Compilation compilation, CancellationToken cancellationToken)
		{
			if (this.Arguments.OutputFileName != null)
			{
				return this.Arguments.OutputFileName;
			}
			CSharpCompilation csharpCompilation = (CSharpCompilation)compilation;
			Symbol symbol = csharpCompilation.ScriptClass;
			if (symbol == null)
			{
				MethodSymbol entryPoint = csharpCompilation.GetEntryPoint(cancellationToken);
				if (entryPoint == null)
				{
					return "error";
				}
				symbol = entryPoint.PartialImplementationPart ?? entryPoint;
			}
			return Path.ChangeExtension(PathUtilities.GetFileName(symbol.Locations.First<Location>().SourceTree.FilePath, true), ".exe");
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00068F37 File Offset: 0x00067137
		internal override bool SuppressDefaultResponseFile(IEnumerable<string> args)
		{
			return Enumerable.Any<string>(args, (string arg) => new string[] { "/noconfig", "-noconfig" }.Contains(arg.ToLowerInvariant(), null));
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00068F5E File Offset: 0x0006715E
		public override void PrintLogo(TextWriter consoleOutput)
		{
			consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LogoLine1, this.Culture), this.GetToolName(), base.GetCompilerVersion());
			consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LogoLine2, this.Culture));
			consoleOutput.WriteLine();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00068FA0 File Offset: 0x000671A0
		public override void PrintLangVersions(TextWriter consoleOutput)
		{
			consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_LangVersions, this.Culture));
			LanguageVersion languageVersion = LanguageVersion.Default.MapSpecifiedToEffectiveVersion();
			LanguageVersion languageVersion2 = LanguageVersion.Latest.MapSpecifiedToEffectiveVersion();
			foreach (LanguageVersion languageVersion3 in (LanguageVersion[])Enum.GetValues(typeof(LanguageVersion)))
			{
				if (languageVersion3 == languageVersion)
				{
					consoleOutput.WriteLine(languageVersion3.ToDisplayString() + " (default)");
				}
				else if (languageVersion3 == languageVersion2)
				{
					consoleOutput.WriteLine(languageVersion3.ToDisplayString() + " (latest)");
				}
				else
				{
					consoleOutput.WriteLine(languageVersion3.ToDisplayString());
				}
			}
			consoleOutput.WriteLine();
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x0006904C File Offset: 0x0006724C
		internal override Type Type
		{
			get
			{
				return typeof(CSharpCompiler);
			}
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00069058 File Offset: 0x00067258
		internal override string GetToolName()
		{
			return ErrorFacts.GetMessage(MessageID.IDS_ToolName, this.Culture);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0006906A File Offset: 0x0006726A
		public override void PrintHelp(TextWriter consoleOutput)
		{
			consoleOutput.WriteLine(ErrorFacts.GetMessage(MessageID.IDS_CSCHelp, this.Culture));
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00069082 File Offset: 0x00067282
		protected override bool TryGetCompilerDiagnosticCode(string diagnosticId, out uint code)
		{
			return CommonCompiler.TryGetCompilerDiagnosticCode(diagnosticId, "CS", out code);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00069090 File Offset: 0x00067290
		protected override void ResolveAnalyzersFromArguments(List<DiagnosticInfo> diagnostics, CommonMessageProvider messageProvider, bool skipAnalyzers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<DiagnosticAnalyzer> analyzers, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<ISourceGenerator> generators)
		{
			this.Arguments.ResolveAnalyzersFromArguments("C#", diagnostics, messageProvider, base.AssemblyLoader, skipAnalyzers, out analyzers, out generators);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000690B0 File Offset: 0x000672B0
		protected override void ResolveEmbeddedFilesFromExternalSourceDirectives(SyntaxTree tree, SourceReferenceResolver resolver, OrderedSet<string> embeddedFiles, DiagnosticBag diagnostics)
		{
			foreach (DirectiveTriviaSyntax directiveTriviaSyntax in tree.GetRoot(default(CancellationToken)).GetDirectives((DirectiveTriviaSyntax d) => d.IsActive && !d.HasErrors && d.Kind() == SyntaxKind.LineDirectiveTrivia))
			{
				LineDirectiveTriviaSyntax lineDirectiveTriviaSyntax = (LineDirectiveTriviaSyntax)directiveTriviaSyntax;
				string text = (string)lineDirectiveTriviaSyntax.File.Value;
				if (text != null)
				{
					string text2 = resolver.ResolveReference(text, tree.FilePath);
					if (text2 == null)
					{
						diagnostics.Add(base.MessageProvider.CreateDiagnostic(1504, lineDirectiveTriviaSyntax.File.GetLocation(), new object[]
						{
							text,
							CSharpResources.CouldNotFindFile
						}));
					}
					else
					{
						embeddedFiles.Add(text2);
					}
				}
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0006919C File Offset: 0x0006739C
		private protected override Compilation RunGenerators(Compilation input, ParseOptions parseOptions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators, AnalyzerConfigOptionsProvider analyzerConfigProvider, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts, DiagnosticBag diagnostics)
		{
			Compilation compilation;
			ImmutableArray<Diagnostic> immutableArray;
			CSharpGeneratorDriver.Create(generators, additionalTexts, (CSharpParseOptions)parseOptions, analyzerConfigProvider).RunGeneratorsAndUpdateCompilation(input, out compilation, out immutableArray, default(CancellationToken));
			diagnostics.AddRange<Diagnostic>(immutableArray);
			return compilation;
		}

		// Token: 0x040005F1 RID: 1521
		internal const string ResponseFileName = "csc.rsp";

		// Token: 0x040005F2 RID: 1522
		private readonly CommandLineDiagnosticFormatter _diagnosticFormatter;

		// Token: 0x040005F3 RID: 1523
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private readonly string _tempDirectory;
	}
}
