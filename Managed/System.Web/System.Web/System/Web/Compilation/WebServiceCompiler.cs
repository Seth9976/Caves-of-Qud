using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000670 RID: 1648
	internal class WebServiceCompiler : BaseCompiler
	{
		// Token: 0x06004693 RID: 18067 RVA: 0x000C3F28 File Offset: 0x000C2128
		public WebServiceCompiler(SimpleWebHandlerParser wService)
			: base(null)
		{
			this.parser = wService;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000C3F38 File Offset: 0x000C2138
		public static Type CompileIntoType(SimpleWebHandlerParser wService)
		{
			return new WebServiceCompiler(wService).GetCompiledType();
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000C3F48 File Offset: 0x000C2148
		public override Type GetCompiledType()
		{
			Type type = CachingCompiler.GetTypeFromCache(this.parser.PhysicalPath);
			if (type != null)
			{
				return type;
			}
			if (this.parser.Program.Trim() == "")
			{
				type = Type.GetType(this.parser.ClassName, false);
				if (type == null)
				{
					type = this.parser.GetTypeFromBin(this.parser.ClassName);
				}
				CachingCompiler.InsertTypeFileDep(type, this.parser.PhysicalPath);
				return type;
			}
			string language = this.parser.Language;
			string text;
			int num;
			string text2;
			CodeDomProvider codeDomProvider = (base.Provider = BaseCompiler.CreateProvider(this.parser.Context, language, out text, out num, out text2));
			if (base.Provider == null)
			{
				throw new HttpException("Configuration error. Language not supported: " + language, 500);
			}
			CompilerParameters compilerParameters = (base.CompilerParameters = CachingCompiler.GetOptions(this.parser.Assemblies));
			compilerParameters.IncludeDebugInformation = this.parser.Debug;
			compilerParameters.CompilerOptions = text;
			compilerParameters.WarningLevel = num;
			bool flag = Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") != null;
			TempFileCollection tempFileCollection = new TempFileCollection(text2, flag);
			compilerParameters.TempFiles = tempFileCollection;
			this.inputFile = tempFileCollection.AddExtension(codeDomProvider.FileExtension);
			StreamWriter streamWriter = new StreamWriter(File.OpenWrite(this.inputFile));
			streamWriter.WriteLine(this.parser.Program);
			streamWriter.Close();
			string fileName = Path.GetFileName(tempFileCollection.AddExtension("dll", true));
			compilerParameters.OutputAssembly = Path.Combine(base.DynamicDir(), fileName);
			CompilerResults compilerResults = CachingCompiler.Compile(this);
			this.CheckCompilerErrors(compilerResults);
			Assembly assembly = compilerResults.CompiledAssembly;
			if (assembly == null)
			{
				if (!File.Exists(compilerParameters.OutputAssembly))
				{
					throw new CompilationException(this.inputFile, compilerResults.Errors, "No assembly returned after compilation!?");
				}
				assembly = Assembly.LoadFrom(compilerParameters.OutputAssembly);
			}
			compilerResults.TempFiles.Delete();
			type = assembly.GetType(this.parser.ClassName, true);
			CachingCompiler.InsertTypeFileDep(type, this.parser.PhysicalPath);
			return type;
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000C4165 File Offset: 0x000C2365
		private void CheckCompilerErrors(CompilerResults results)
		{
			if (results.NativeCompilerReturnValue == 0)
			{
				return;
			}
			throw new CompilationException(this.parser.PhysicalPath, results.Errors, this.parser.Program);
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x000C4191 File Offset: 0x000C2391
		internal new SimpleWebHandlerParser Parser
		{
			get
			{
				return this.parser;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x000C4199 File Offset: 0x000C2399
		internal string InputFile
		{
			get
			{
				return this.inputFile;
			}
		}

		// Token: 0x04002549 RID: 9545
		private SimpleWebHandlerParser parser;

		// Token: 0x0400254A RID: 9546
		private string inputFile;
	}
}
