using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynCSharp.Compiler;

namespace RoslynCSharp.CodeDom.Compiler
{
	// Token: 0x02000002 RID: 2
	public class RoslynCSharpCodeDomCompiler : ICodeCompiler
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string OutputDirectory
		{
			get
			{
				return this.outputDirectory;
			}
			set
			{
				this.outputDirectory = value;
				if (this.outputDirectory == null)
				{
					this.outputDirectory = string.Empty;
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		public CompilerResults CompileAssemblyFromFile(CompilerParameters parameters, string fileName)
		{
			CSharpParseOptions parseOptions = this.GetParseOptions(parameters);
			CSharpCompilationOptions compilationOptions = this.GetCompilationOptions(parameters);
			SyntaxTree[] array = RoslynCSharpCompiler.ParseFile(fileName, parseOptions);
			string[] compilationReferences = this.GetCompilationReferences(parameters);
			Compilation compilation = RoslynCSharpCompiler.CreateCompilationObject(parameters.OutputAssembly, compilationReferences, array, compilationOptions);
			CompilerResults compilerResults;
			using (Stream outputStream = this.GetOutputStream(parameters))
			{
				CompilationResult compilationResult = RoslynCSharpCompiler.EmitCompilationObject(compilation, outputStream, null, true, null, 2);
				compilerResults = this.GetCompilerResults(compilationResult);
			}
			return compilerResults;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		public CompilerResults CompileAssemblyFromFileBatch(CompilerParameters parameters, string[] fileNames)
		{
			CSharpParseOptions parseOptions = this.GetParseOptions(parameters);
			CSharpCompilationOptions compilationOptions = this.GetCompilationOptions(parameters);
			SyntaxTree[] array = RoslynCSharpCompiler.ParseFiles(fileNames, parseOptions);
			string[] compilationReferences = this.GetCompilationReferences(parameters);
			Compilation compilation = RoslynCSharpCompiler.CreateCompilationObject(parameters.OutputAssembly, compilationReferences, array, compilationOptions);
			CompilerResults compilerResults;
			using (Stream outputStream = this.GetOutputStream(parameters))
			{
				CompilationResult compilationResult = RoslynCSharpCompiler.EmitCompilationObject(compilation, outputStream, null, true, null, 2);
				compilerResults = this.GetCompilerResults(compilationResult);
			}
			return compilerResults;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002174 File Offset: 0x00000374
		public CompilerResults CompileAssemblyFromSource(CompilerParameters parameters, string source)
		{
			CSharpParseOptions parseOptions = this.GetParseOptions(parameters);
			CSharpCompilationOptions compilationOptions = this.GetCompilationOptions(parameters);
			SyntaxTree[] array = RoslynCSharpCompiler.ParseSource(source, parseOptions);
			string[] compilationReferences = this.GetCompilationReferences(parameters);
			Compilation compilation = RoslynCSharpCompiler.CreateCompilationObject(parameters.OutputAssembly, compilationReferences, array, compilationOptions);
			CompilerResults compilerResults;
			using (Stream outputStream = this.GetOutputStream(parameters))
			{
				CompilationResult compilationResult = RoslynCSharpCompiler.EmitCompilationObject(compilation, outputStream, null, true, null, 2);
				compilerResults = this.GetCompilerResults(compilationResult);
			}
			return compilerResults;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021F4 File Offset: 0x000003F4
		public CompilerResults CompileAssemblyFromSourceBatch(CompilerParameters parameters, string[] sources)
		{
			CSharpParseOptions parseOptions = this.GetParseOptions(parameters);
			CSharpCompilationOptions compilationOptions = this.GetCompilationOptions(parameters);
			SyntaxTree[] array = RoslynCSharpCompiler.ParseSources(sources, parseOptions);
			string[] compilationReferences = this.GetCompilationReferences(parameters);
			Compilation compilation = RoslynCSharpCompiler.CreateCompilationObject(parameters.OutputAssembly, compilationReferences, array, compilationOptions);
			CompilerResults compilerResults;
			using (Stream outputStream = this.GetOutputStream(parameters))
			{
				CompilationResult compilationResult = RoslynCSharpCompiler.EmitCompilationObject(compilation, outputStream, null, true, null, 2);
				compilerResults = this.GetCompilerResults(compilationResult);
			}
			return compilerResults;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002274 File Offset: 0x00000474
		public CompilerResults CompileAssemblyFromDom(CompilerParameters options, CodeCompileUnit compilationUnit)
		{
			throw new NotSupportedException("Use compile from file or compile from source");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002280 File Offset: 0x00000480
		public CompilerResults CompileAssemblyFromDomBatch(CompilerParameters options, CodeCompileUnit[] compilationUnits)
		{
			throw new NotSupportedException("Use compile from file or compile from source");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000228C File Offset: 0x0000048C
		private CSharpParseOptions GetParseOptions(CompilerParameters parameters)
		{
			string parameterOption = this.GetParameterOption(parameters, "/languageversion");
			LanguageVersion languageVersion = 0;
			decimal num;
			if (parameterOption != null && decimal.TryParse(parameterOption, out num))
			{
				foreach (object obj in Enum.GetValues(typeof(LanguageVersion)))
				{
					LanguageVersion languageVersion2 = (LanguageVersion)obj;
					string text = languageVersion2.ToString();
					if (text.Contains("CSharp"))
					{
						text = text.Replace("CSharp", string.Empty);
						if (num.ToString().Replace(".", "_") == text)
						{
							languageVersion = languageVersion2;
							break;
						}
					}
				}
			}
			string parameterOption2 = this.GetParameterOption(parameters, "/define");
			List<string> list = new List<string>();
			if (parameterOption2 != null)
			{
				foreach (string text2 in parameterOption2.Split(new char[] { ';' }))
				{
					if (!string.IsNullOrEmpty(text2))
					{
						list.Add(text2);
					}
				}
			}
			return new CSharpParseOptions(languageVersion, 0, 0, list);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023C4 File Offset: 0x000005C4
		private CSharpCompilationOptions GetCompilationOptions(CompilerParameters parameters)
		{
			OutputKind outputKind = (parameters.GenerateExecutable ? 1 : 2);
			OptimizationLevel optimizationLevel = ((this.GetParameterOption(parameters, "/optimize") != null) ? 1 : 0);
			bool parameterOption = this.GetParameterOption(parameters, "/unsafe") != null;
			Platform platform = 0;
			bool flag = true;
			return new CSharpCompilationOptions(outputKind, false, null, null, null, null, optimizationLevel, false, parameterOption, null, null, default(ImmutableArray<byte>), null, platform, 0, parameters.WarningLevel, null, flag, false, null, null, null, null, null, false, 0);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000243C File Offset: 0x0000063C
		private CompilerResults GetCompilerResults(CompilationResult result)
		{
			CompilerResults compilerResults = new CompilerResults(null);
			compilerResults.NativeCompilerReturnValue = 0;
			compilerResults.PathToAssembly = result.OutputFile;
			compilerResults.CompiledAssembly = result.OutputAssembly;
			foreach (CompilationError compilationError in result.Errors)
			{
				if (!compilationError.IsInfo)
				{
					CompilerError compilerError = new CompilerError(compilationError.SourceFile, compilationError.SourceLine, compilationError.SourceColumn, compilationError.Code, compilationError.Message);
					compilerError.IsWarning = compilationError.IsWarning;
					compilerResults.Errors.Add(compilerError);
				}
			}
			return compilerResults;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000024D4 File Offset: 0x000006D4
		private Stream GetOutputStream(CompilerParameters parameters)
		{
			Stream stream;
			if (parameters.GenerateInMemory)
			{
				stream = new MemoryStream();
			}
			else
			{
				string text = ".dll";
				if (parameters.GenerateExecutable)
				{
					text = ".exe";
				}
				stream = new FileStream(Path.Combine(this.outputDirectory, parameters.OutputAssembly + text), FileMode.Create);
			}
			return stream;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002528 File Offset: 0x00000728
		private string[] GetCompilationReferences(CompilerParameters parameters)
		{
			string[] array = new string[parameters.ReferencedAssemblies.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters.ReferencedAssemblies[i];
			}
			return array;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002564 File Offset: 0x00000764
		private string GetParameterOption(CompilerParameters parameters, string targetOption)
		{
			string[] array = parameters.CompilerOptions.Split(new char[] { ' ' });
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				if (text.IndexOf(targetOption) == 0)
				{
					int num = text.IndexOf(':');
					if (num >= 0)
					{
						return text.Remove(0, num);
					}
					return text;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		// Token: 0x04000001 RID: 1
		private string outputDirectory = string.Empty;
	}
}
