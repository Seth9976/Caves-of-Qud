using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200060E RID: 1550
	internal class AppResourcesAssemblyBuilder
	{
		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x060042AC RID: 17068 RVA: 0x000AE8B1 File Offset: 0x000ACAB1
		public CodeDomProvider Provider
		{
			get
			{
				if (this._provider != null)
				{
					return this._provider;
				}
				this._provider = this.ci.CreateProvider();
				if (this._provider == null)
				{
					throw new ApplicationException("Failed to instantiate the default compiler.");
				}
				return this._provider;
			}
		}

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x060042AD RID: 17069 RVA: 0x000AE8EE File Offset: 0x000ACAEE
		public Assembly MainAssembly
		{
			get
			{
				return this.mainAssembly;
			}
		}

		// Token: 0x060042AE RID: 17070 RVA: 0x000AE8F8 File Offset: 0x000ACAF8
		public AppResourcesAssemblyBuilder(string canonicAssemblyName, string baseAssemblyPath, AppResourcesCompiler appres)
		{
			this.appResourcesCompiler = appres;
			this.baseAssemblyPath = baseAssemblyPath;
			this.baseAssemblyDirectory = Path.GetDirectoryName(baseAssemblyPath);
			this.canonicAssemblyName = canonicAssemblyName;
			this.config = WebConfigurationManager.GetWebApplicationSection("system.web/compilation") as CompilationSection;
			if (this.config == null || !CodeDomProvider.IsDefinedLanguage(this.config.DefaultLanguage))
			{
				throw new ApplicationException("Could not get the default compiler.");
			}
			this.ci = CodeDomProvider.GetCompilerInfo(this.config.DefaultLanguage);
			if (this.ci == null || !this.ci.IsCodeDomProviderTypeValid)
			{
				throw new ApplicationException("Failed to obtain the default compiler information.");
			}
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x000AE99C File Offset: 0x000ACB9C
		public void Build()
		{
			this.Build(null);
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x000AE9A8 File Offset: 0x000ACBA8
		public void Build(CodeCompileUnit unit)
		{
			Dictionary<string, List<string>> cultureFiles = this.appResourcesCompiler.CultureFiles;
			List<string> defaultCultureFiles = this.appResourcesCompiler.DefaultCultureFiles;
			if (defaultCultureFiles != null)
			{
				this.BuildDefaultAssembly(defaultCultureFiles, unit);
			}
			foreach (KeyValuePair<string, List<string>> keyValuePair in cultureFiles)
			{
				this.BuildSatelliteAssembly(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x000AEA24 File Offset: 0x000ACC24
		private void BuildDefaultAssembly(List<string> files, CodeCompileUnit unit)
		{
			AssemblyBuilder assemblyBuilder = new AssemblyBuilder(this.Provider);
			if (unit != null)
			{
				assemblyBuilder.AddCodeCompileUnit(unit);
			}
			CompilerParameters compilerParameters = this.ci.CreateDefaultCompilerParameters();
			compilerParameters.OutputAssembly = this.baseAssemblyPath;
			compilerParameters.GenerateExecutable = false;
			compilerParameters.TreatWarningsAsErrors = true;
			compilerParameters.IncludeDebugInformation = this.config.Debug;
			foreach (string text in files)
			{
				compilerParameters.EmbeddedResources.Add(text);
			}
			CompilerResults compilerResults = assemblyBuilder.BuildAssembly(compilerParameters);
			if (compilerResults == null)
			{
				return;
			}
			if (compilerResults.NativeCompilerReturnValue == 0)
			{
				this.mainAssembly = compilerResults.CompiledAssembly;
				BuildManager.TopLevelAssemblies.Add(this.mainAssembly);
				HttpRuntime.WritePreservationFile(this.mainAssembly, this.canonicAssemblyName);
				HttpRuntime.EnableAssemblyMapping(true);
				return;
			}
			if (HttpContext.Current.IsCustomErrorEnabled)
			{
				throw new ApplicationException("An error occurred while compiling global resources.");
			}
			throw new CompilationException(null, compilerResults.Errors, null);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x000AEB38 File Offset: 0x000ACD38
		private void BuildSatelliteAssembly(string cultureName, List<string> files)
		{
			string text = this.BuildAssemblyPath(cultureName);
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			Process process = new Process();
			StringBuilder stringBuilder = new StringBuilder(this.SetAlPath(processStartInfo));
			stringBuilder.Append("/c:\"" + cultureName + "\" ");
			stringBuilder.Append("/t:lib ");
			stringBuilder.Append("/out:\"" + text + "\" ");
			if (this.mainAssembly != null)
			{
				stringBuilder.Append("/template:\"" + this.mainAssembly.Location + "\" ");
			}
			string text2 = text + ".response";
			using (FileStream fileStream = File.OpenWrite(text2))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					foreach (string text3 in files)
					{
						streamWriter.WriteLine("/embed:\"" + text3 + "\" ");
					}
				}
			}
			stringBuilder.Append("@\"" + text2 + "\"");
			processStartInfo.Arguments = stringBuilder.ToString();
			processStartInfo.CreateNoWindow = true;
			processStartInfo.UseShellExecute = false;
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.RedirectStandardError = true;
			process.StartInfo = processStartInfo;
			StringCollection alOutput = new StringCollection();
			Mutex alMutex = new Mutex();
			DataReceivedEventHandler dataReceivedEventHandler = delegate(object sender, DataReceivedEventArgs args)
			{
				if (args.Data != null)
				{
					alMutex.WaitOne();
					alOutput.Add(args.Data);
					alMutex.ReleaseMutex();
				}
			};
			process.ErrorDataReceived += dataReceivedEventHandler;
			process.OutputDataReceived += dataReceivedEventHandler;
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				throw new HttpException(string.Format("Error running {0}", process.StartInfo.FileName), ex);
			}
			Exception ex2 = null;
			int num = 0;
			try
			{
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				num = process.ExitCode;
			}
			catch (Exception ex2)
			{
			}
			finally
			{
				process.CancelErrorRead();
				process.CancelOutputRead();
				process.Close();
			}
			if (num != 0 || ex2 != null)
			{
				CompilerErrorCollection compilerErrorCollection = null;
				if (alOutput.Count != 0)
				{
					foreach (string text4 in alOutput)
					{
						if (text4.StartsWith("ALINK: error ", StringComparison.Ordinal))
						{
							if (compilerErrorCollection == null)
							{
								compilerErrorCollection = new CompilerErrorCollection();
							}
							int num2 = text4.IndexOf(':', 13);
							string text5 = ((num2 != -1) ? text4.Substring(13, num2 - 13) : "Unknown");
							string text6 = ((num2 != -1) ? text4.Substring(num2 + 1) : text4.Substring(13));
							compilerErrorCollection.Add(new CompilerError(Path.GetFileName(text), 0, 0, text5, text6));
						}
					}
				}
				throw new CompilationException(Path.GetFileName(text), compilerErrorCollection, null);
			}
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x000AEE70 File Offset: 0x000AD070
		private string SetAlPath(ProcessStartInfo info)
		{
			if (RuntimeHelpers.RunningOnWindows)
			{
				info.FileName = global::System.MonoToolsLocator.Mono;
				return global::System.MonoToolsLocator.AssemblyLinker + " ";
			}
			info.FileName = global::System.MonoToolsLocator.AssemblyLinker;
			return string.Empty;
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x000AEEA4 File Offset: 0x000AD0A4
		private string BuildAssemblyPath(string cultureName)
		{
			string text = Path.Combine(this.baseAssemblyDirectory, cultureName);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string text2 = Path.GetFileNameWithoutExtension(this.baseAssemblyPath) + ".resources.dll";
			return Path.Combine(text, text2);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000AEEEC File Offset: 0x000AD0EC
		private CodeCompileUnit GenerateAssemblyInfo(string cultureName)
		{
			CodeAttributeArgument[] array = new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(cultureName))
			};
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.Reflection.AssemblyCultureAttribute"), array));
			array = new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression("ASP.NET")),
				new CodeAttributeArgument(new CodePrimitiveExpression(Environment.Version.ToString()))
			};
			codeCompileUnit.AssemblyCustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.CodeDom.Compiler.GeneratedCodeAttribute"), array));
			return codeCompileUnit;
		}

		// Token: 0x040023CD RID: 9165
		private CompilationSection config;

		// Token: 0x040023CE RID: 9166
		private CompilerInfo ci;

		// Token: 0x040023CF RID: 9167
		private CodeDomProvider _provider;

		// Token: 0x040023D0 RID: 9168
		private string baseAssemblyPath;

		// Token: 0x040023D1 RID: 9169
		private string baseAssemblyDirectory;

		// Token: 0x040023D2 RID: 9170
		private string canonicAssemblyName;

		// Token: 0x040023D3 RID: 9171
		private Assembly mainAssembly;

		// Token: 0x040023D4 RID: 9172
		private AppResourcesCompiler appResourcesCompiler;
	}
}
