using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Text;

namespace RoslynCSharp.Compiler
{
	// Token: 0x0200000D RID: 13
	public class RoslynCSharpCompiler
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002C84 File Offset: 0x00000E84
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002C8C File Offset: 0x00000E8C
		public string OutputDirectory
		{
			get
			{
				return this.outputDirectory;
			}
			set
			{
				this.outputDirectory = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C95 File Offset: 0x00000E95
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002C9D File Offset: 0x00000E9D
		public string OutputName
		{
			get
			{
				return this.outputName;
			}
			set
			{
				this.outputName = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002CA6 File Offset: 0x00000EA6
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002CAE File Offset: 0x00000EAE
		public string OutputPDBExtension
		{
			get
			{
				return this.outputPDBExtension;
			}
			set
			{
				this.outputPDBExtension = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002CB7 File Offset: 0x00000EB7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002CBF File Offset: 0x00000EBF
		public bool AllowUnsafe
		{
			get
			{
				return this.allowUnsafe;
			}
			set
			{
				this.allowUnsafe = value;
				this.UpdateCompilerOptions();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002CCE File Offset: 0x00000ECE
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public bool AllowOptimize
		{
			get
			{
				return this.allowOptimize;
			}
			set
			{
				this.allowOptimize = value;
				this.UpdateCompilerOptions();
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002CE5 File Offset: 0x00000EE5
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002CED File Offset: 0x00000EED
		public bool AllowConcurrentCompile
		{
			get
			{
				return this.allowConcurrentCompile;
			}
			set
			{
				this.allowConcurrentCompile = value;
				this.UpdateCompilerOptions();
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002CFC File Offset: 0x00000EFC
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D04 File Offset: 0x00000F04
		public bool GenerateInMemory
		{
			get
			{
				return this.generateInMemory;
			}
			set
			{
				this.generateInMemory = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002D0D File Offset: 0x00000F0D
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002D15 File Offset: 0x00000F15
		public bool GenerateSymbols
		{
			get
			{
				return this.generateSymbols;
			}
			set
			{
				this.generateSymbols = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D1E File Offset: 0x00000F1E
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002D26 File Offset: 0x00000F26
		public int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
				this.UpdateCompilerOptions();
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D35 File Offset: 0x00000F35
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002D3D File Offset: 0x00000F3D
		public LanguageVersion LanguageVersion
		{
			get
			{
				return this.languageVersion;
			}
			set
			{
				this.languageVersion = value;
				this.UpdateParserOptions();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002D4C File Offset: 0x00000F4C
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002D54 File Offset: 0x00000F54
		public OutputKind OutputKind
		{
			get
			{
				return this.outputKind;
			}
			set
			{
				this.outputKind = value;
				this.UpdateCompilerOptions();
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D63 File Offset: 0x00000F63
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002D6B File Offset: 0x00000F6B
		public Platform TargetPlatform
		{
			get
			{
				return this.targetPlatform;
			}
			set
			{
				this.targetPlatform = value;
				this.UpdateCompilerOptions();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D7A File Offset: 0x00000F7A
		public IList<string> DefineSymbols
		{
			get
			{
				return this.defineSymbols;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002D82 File Offset: 0x00000F82
		public IList<IMetadataReferenceProvider> ReferenceAssemblies
		{
			get
			{
				return this.referenceAssemblies;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002D8C File Offset: 0x00000F8C
		public string DefaultOutputExtension
		{
			get
			{
				switch (this.outputKind)
				{
				case 0:
				case 1:
				case 3:
					return ".exe";
				case 2:
					return ".dll";
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public CompilationResult LastCompileResult
		{
			get
			{
				return this.lastCompileResult;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002DCF File Offset: 0x00000FCF
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public DebugInformationFormat DebugSymbolType
		{
			get
			{
				return this.debugSymbolType;
			}
			set
			{
				this.debugSymbolType = value;
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public RoslynCSharpCompiler(bool includeDefaultReferenceAssemblies = true, bool generateInMemory = true, OutputKind outputKind = 2, LanguageVersion languageVersion = 0, AppDomain loadAssemblyDomain = null)
		{
			this.generateInMemory = generateInMemory;
			this.outputKind = outputKind;
			this.languageVersion = languageVersion;
			this.loadAssemblyDomain = loadAssemblyDomain;
			this.UpdateParserOptions();
			this.UpdateCompilerOptions();
			this.defineSymbols.CollectionChanged += delegate(object o, NotifyCollectionChangedEventArgs e)
			{
				this.UpdateParserOptions();
			};
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002EA0 File Offset: 0x000010A0
		public RoslynCSharpCompiler(string outputName, bool includeDefaultReferenceAssemblies = true, bool generateInMemory = true, OutputKind outputKing = 2, LanguageVersion languageVersion = 0, AppDomain loadAssemblyDomain = null)
		{
			this.outputName = outputName;
			this.generateInMemory = generateInMemory;
			this.outputKind = this.OutputKind;
			this.languageVersion = this.LanguageVersion;
			this.loadAssemblyDomain = loadAssemblyDomain;
			if (includeDefaultReferenceAssemblies)
			{
				foreach (IMetadataReferenceProvider metadataReferenceProvider in RoslynCSharpCompiler.defaultReferenceAssemblies)
				{
					this.referenceAssemblies.Add(metadataReferenceProvider);
				}
			}
			this.UpdateParserOptions();
			this.UpdateCompilerOptions();
			this.defineSymbols.CollectionChanged += delegate(object o, NotifyCollectionChangedEventArgs e)
			{
				this.UpdateParserOptions();
			};
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002F98 File Offset: 0x00001198
		public CompilationResult CompileFromSource(string cSharpSource, IMetadataReferenceProvider[] additionalAssemblyReferences = null)
		{
			if (cSharpSource == null)
			{
				throw new ArgumentNullException("cSharpSource");
			}
			SyntaxTree[] array = RoslynCSharpCompiler.ParseSource(cSharpSource, this.parseOptions);
			return this.CompileFromSyntaxTree(array, additionalAssemblyReferences);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002FC8 File Offset: 0x000011C8
		public CompilationResult CompileFromSources(string[] cSharpSources, IMetadataReferenceProvider[] additionalAssemblyReferences = null)
		{
			for (int i = 0; i < cSharpSources.Length; i++)
			{
				if (cSharpSources[i] == null)
				{
					throw new ArgumentNullException(string.Format("Source array element '{0}' is null", i));
				}
			}
			SyntaxTree[] array = RoslynCSharpCompiler.ParseSources(cSharpSources, this.parseOptions);
			return this.CompileFromSyntaxTree(array, additionalAssemblyReferences);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003014 File Offset: 0x00001214
		public CompilationResult CompileFromFile(string cSharpFile, IMetadataReferenceProvider[] additionalAssemblyReferences = null)
		{
			if (cSharpFile == null)
			{
				throw new ArgumentNullException("cSharpFile");
			}
			SyntaxTree[] array = RoslynCSharpCompiler.ParseFile(cSharpFile, this.parseOptions);
			return this.CompileFromSyntaxTree(array, additionalAssemblyReferences);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003044 File Offset: 0x00001244
		public CompilationResult CompileFromFiles(string[] cSharpFiles, IMetadataReferenceProvider[] additionalAssemblyReferences = null)
		{
			for (int i = 0; i < cSharpFiles.Length; i++)
			{
				if (cSharpFiles[i] == null)
				{
					throw new ArgumentNullException(string.Format("Source array element '{0}' is null", i));
				}
			}
			SyntaxTree[] array = RoslynCSharpCompiler.ParseFiles(cSharpFiles, this.parseOptions);
			return this.CompileFromSyntaxTree(array, additionalAssemblyReferences);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003090 File Offset: 0x00001290
		private CompilationResult CompileFromSyntaxTree(SyntaxTree[] syntaxTrees, IMetadataReferenceProvider[] additionalAsemblyReferences)
		{
			string text = this.outputName;
			if (string.IsNullOrEmpty(text))
			{
				text = Guid.NewGuid().ToString();
			}
			if (!Path.HasExtension(text))
			{
				text += this.DefaultOutputExtension;
			}
			string text2;
			if (string.IsNullOrEmpty(this.outputDirectory))
			{
				text2 = text;
			}
			else
			{
				text2 = Path.Combine(this.outputDirectory, text);
			}
			Exception ex = null;
			MetadataReference[] array = this.UpdateReferences(additionalAsemblyReferences, out ex);
			if (ex != null)
			{
				throw ex;
			}
			Compilation compilation = RoslynCSharpCompiler.CreateCompilationObject(text, array, syntaxTrees, this.compileOptions);
			DirectoryInfo parent = Directory.GetParent(text2);
			if (!this.generateInMemory && !parent.Exists)
			{
				parent.Create();
			}
			Stream stream = (this.generateInMemory ? new MemoryStream() : new FileStream(text2, FileMode.OpenOrCreate, FileAccess.ReadWrite));
			Stream stream2 = null;
			if (this.generateSymbols)
			{
				string text3 = Path.ChangeExtension(text2, this.outputPDBExtension);
				stream2 = (this.generateInMemory ? new MemoryStream() : new FileStream(text3, FileMode.OpenOrCreate, FileAccess.ReadWrite));
			}
			CompilationResult compilationResult2;
			using (stream)
			{
				using (stream2)
				{
					CompilationResult compilationResult = RoslynCSharpCompiler.EmitCompilationObject(compilation, stream, stream2, RoslynCSharpCompiler.loadCompiledAssemblies, this.loadAssemblyDomain, this.debugSymbolType);
					this.lastCompileResult = compilationResult;
					compilationResult2 = compilationResult;
				}
			}
			return compilationResult2;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000031F0 File Offset: 0x000013F0
		private void UpdateParserOptions()
		{
			this.parseOptions = new CSharpParseOptions(this.languageVersion, 1, 0, this.defineSymbols);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000320C File Offset: 0x0000140C
		private void UpdateCompilerOptions()
		{
			OptimizationLevel optimizationLevel = (this.allowOptimize ? 1 : 0);
			this.compileOptions = new CSharpCompilationOptions(this.outputKind, false, null, null, null, null, optimizationLevel, false, this.allowUnsafe, null, null, default(ImmutableArray<byte>), null, this.targetPlatform, 0, this.warningLevel, null, this.allowConcurrentCompile, false, null, null, null, null, null, false, 0);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003274 File Offset: 0x00001474
		private MetadataReference[] UpdateReferences(IMetadataReferenceProvider[] additionalReferences, out Exception error)
		{
			error = null;
			this.referenceBuilder.Clear();
			this.referenceExceptions.Clear();
			this.UpdateReferencesFromProviderSource(this.referenceAssemblies, this.referenceBuilder, this.referenceExceptions);
			if (additionalReferences != null)
			{
				this.UpdateReferencesFromProviderSource(additionalReferences, this.referenceBuilder, this.referenceExceptions);
			}
			if (this.referenceExceptions.Count > 0)
			{
				error = new AssemblyReferenceException(this.referenceExceptions);
			}
			return this.referenceBuilder.ToArray();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032F0 File Offset: 0x000014F0
		private void UpdateReferencesFromProviderSource(IEnumerable<IMetadataReferenceProvider> providerSource, IList<MetadataReference> references, IList<Exception> exceptions)
		{
			using (IEnumerator<IMetadataReferenceProvider> enumerator = providerSource.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MetadataReference metadataReference;
					Exception ex;
					if (enumerator.Current.TryResolveReference(out metadataReference, out ex))
					{
						references.Add(metadataReference);
					}
					else
					{
						exceptions.Add(ex);
					}
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000334C File Offset: 0x0000154C
		public static SyntaxTree[] ParseSource(string cSharpSource, CSharpParseOptions parseOptions = null)
		{
			return RoslynCSharpCompiler.ParseSources(new string[] { cSharpSource }, parseOptions);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003360 File Offset: 0x00001560
		public static SyntaxTree[] ParseSources(string[] cSharpSources, CSharpParseOptions parseOptions = null)
		{
			if (cSharpSources.Length == 0)
			{
				return new SyntaxTree[0];
			}
			SyntaxTree[] array = new SyntaxTree[cSharpSources.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = CSharpSyntaxTree.ParseText(cSharpSources[i], parseOptions, "", Encoding.Default, default(CancellationToken));
			}
			return array;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000033AF File Offset: 0x000015AF
		public static SyntaxTree[] ParseFile(string cSharpFile, CSharpParseOptions parseOptions = null)
		{
			return RoslynCSharpCompiler.ParseFiles(new string[] { cSharpFile }, parseOptions);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000033C4 File Offset: 0x000015C4
		public static SyntaxTree[] ParseFiles(string[] cSharpFiles, CSharpParseOptions parseOptions = null)
		{
			if (cSharpFiles.Length == 0)
			{
				return new SyntaxTree[0];
			}
			SyntaxTree[] array = new SyntaxTree[cSharpFiles.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (!File.Exists(cSharpFiles[i]))
				{
					throw new IOException(string.Format("The specified C# source file '{0}' does not exist", cSharpFiles[i]));
				}
				using (Stream stream = File.OpenRead(cSharpFiles[i]))
				{
					using (TextReader textReader = new StreamReader(stream))
					{
						array[i] = CSharpSyntaxTree.ParseText(SourceText.From(textReader, (int)stream.Length, Encoding.Default, 1), parseOptions, cSharpFiles[i], default(CancellationToken));
					}
				}
			}
			return array;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000347C File Offset: 0x0000167C
		public static Compilation CreateCompilationObject(string assemblyName, string[] references, SyntaxTree[] syntaxTrees, CSharpCompilationOptions options = null)
		{
			if (string.IsNullOrEmpty(assemblyName))
			{
				throw new ArgumentException("A valid assembly name must be provided");
			}
			if (syntaxTrees == null || syntaxTrees.Length == 0)
			{
				throw new ArgumentException("You must provide at least one syntax tree for the creation of a compilation object");
			}
			if (references == null)
			{
				references = new string[0];
			}
			MetadataReference[] array = new MetadataReference[references.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = MetadataReference.CreateFromFile(references[i], default(MetadataReferenceProperties), null);
			}
			return RoslynCSharpCompiler.CreateCompilationObject(assemblyName, array, syntaxTrees, options);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000034EE File Offset: 0x000016EE
		public static Compilation CreateCompilationObject(string assemblyName, MetadataReference[] references, SyntaxTree[] syntaxTrees, CSharpCompilationOptions options = null)
		{
			if (string.IsNullOrEmpty(assemblyName))
			{
				throw new ArgumentException("A valid assembly name must be provided");
			}
			if (syntaxTrees == null || syntaxTrees.Length == 0)
			{
				throw new ArgumentException("You must provide at least one syntax tree for the creation of a compilation object");
			}
			if (references == null)
			{
				references = new MetadataReference[0];
			}
			return CSharpCompilation.Create(assemblyName, syntaxTrees, references, options);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000352C File Offset: 0x0000172C
		public static CompilationResult EmitCompilationObject(Compilation emitObject, Stream targetStream, Stream targetPDBStream = null, bool loadCompiledAssembly = true, AppDomain loadAssemblyDomain = null, DebugInformationFormat debugFormat = 2)
		{
			if (emitObject == null)
			{
				throw new ArgumentNullException("emitObject");
			}
			if (targetStream == null)
			{
				throw new ArgumentNullException("targetStream");
			}
			long position = targetStream.Position;
			if (targetPDBStream != null)
			{
				long position2 = targetPDBStream.Position;
			}
			EmitOptions emitOptions = null;
			if (targetPDBStream != null)
			{
				emitOptions = new EmitOptions(false, debugFormat, null, null, 0, 0UL, false, default(SubsystemVersion), null, false, true, default(ImmutableArray<InstrumentationKind>), null);
			}
			EmitResult emitResult = emitObject.Emit(targetStream, targetPDBStream, null, null, null, emitOptions, null, null, null, null, default(CancellationToken));
			CompilationResult compilationResult = new CompilationResult(emitResult.Success, emitResult.Diagnostics);
			if (targetStream is FileStream)
			{
				FileStream fileStream = targetStream as FileStream;
				compilationResult.OutputFile = fileStream.Name;
				fileStream.Flush();
			}
			if (compilationResult.Success)
			{
				if (targetPDBStream != null)
				{
					if (targetPDBStream is FileStream)
					{
						compilationResult.OutputPDBFile = (targetPDBStream as FileStream).Name;
					}
					if (targetPDBStream.CanSeek)
					{
						long position3 = targetPDBStream.Position;
						targetPDBStream.Seek(position, SeekOrigin.Begin);
						byte[] array = new byte[position3 - position];
						targetPDBStream.Read(array, 0, array.Length);
						compilationResult.OutputPDBImage = array;
					}
					targetPDBStream.Dispose();
				}
				RoslynCSharpCompiler.LoadAssemblyImageFromStream(targetStream, position, compilationResult);
				targetStream.Dispose();
				if (loadCompiledAssembly)
				{
					AppDomain appDomain = AppDomain.CurrentDomain;
					if (loadAssemblyDomain != null)
					{
						appDomain = loadAssemblyDomain;
					}
					if (!string.IsNullOrEmpty(compilationResult.OutputFile))
					{
						compilationResult.OutputAssembly = appDomain.Load(new AssemblyName
						{
							CodeBase = compilationResult.OutputFile
						});
					}
					else
					{
						compilationResult.OutputAssembly = appDomain.Load(compilationResult.OutputAssemblyImage);
					}
				}
			}
			return compilationResult;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000036BC File Offset: 0x000018BC
		private static void LoadAssemblyImageFromStream(Stream targetStream, long streamStart, CompilationResult result)
		{
			if (!targetStream.CanSeek)
			{
				throw new IOException("Unable to load assembly definition because the specified output stream is not seekable");
			}
			long position = targetStream.Position;
			targetStream.Seek(streamStart, SeekOrigin.Begin);
			byte[] array = new byte[position - streamStart];
			targetStream.Read(array, 0, array.Length);
			result.OutputAssemblyImage = array;
		}

		// Token: 0x0400001A RID: 26
		private AppDomain loadAssemblyDomain;

		// Token: 0x0400001B RID: 27
		private CSharpParseOptions parseOptions;

		// Token: 0x0400001C RID: 28
		private CSharpCompilationOptions compileOptions;

		// Token: 0x0400001D RID: 29
		private string outputDirectory = "";

		// Token: 0x0400001E RID: 30
		private string outputName;

		// Token: 0x0400001F RID: 31
		private string outputPDBExtension = ".dll.pdb";

		// Token: 0x04000020 RID: 32
		private bool allowUnsafe;

		// Token: 0x04000021 RID: 33
		private bool allowOptimize = true;

		// Token: 0x04000022 RID: 34
		private bool allowConcurrentCompile = true;

		// Token: 0x04000023 RID: 35
		private bool generateInMemory = true;

		// Token: 0x04000024 RID: 36
		private bool generateSymbols;

		// Token: 0x04000025 RID: 37
		private int warningLevel = 4;

		// Token: 0x04000026 RID: 38
		private LanguageVersion languageVersion;

		// Token: 0x04000027 RID: 39
		private OutputKind outputKind = 2;

		// Token: 0x04000028 RID: 40
		private Platform targetPlatform;

		// Token: 0x04000029 RID: 41
		private DebugInformationFormat debugSymbolType = 2;

		// Token: 0x0400002A RID: 42
		private ObservableCollection<string> defineSymbols = new ObservableCollection<string>();

		// Token: 0x0400002B RID: 43
		private ObservableCollection<IMetadataReferenceProvider> referenceAssemblies = new ObservableCollection<IMetadataReferenceProvider>();

		// Token: 0x0400002C RID: 44
		private List<MetadataReference> referenceBuilder = new List<MetadataReference>();

		// Token: 0x0400002D RID: 45
		private List<Exception> referenceExceptions = new List<Exception>();

		// Token: 0x0400002E RID: 46
		private CompilationResult lastCompileResult;

		// Token: 0x0400002F RID: 47
		public static bool loadCompiledAssemblies = true;

		// Token: 0x04000030 RID: 48
		public static readonly IMetadataReferenceProvider[] defaultReferenceAssemblies = new IMetadataReferenceProvider[]
		{
			new AssemblyReferenceFromAssemblyObject(typeof(object).Assembly)
		};
	}
}
