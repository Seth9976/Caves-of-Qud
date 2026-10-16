using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynCSharp.Compiler;
using Trivial.CodeSecurity;
using UnityEngine;

namespace RoslynCSharp
{
	// Token: 0x02000009 RID: 9
	public class ScriptDomain : IDisposable
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F37 File Offset: 0x00001137
		public static ScriptDomain Active
		{
			get
			{
				return ScriptDomain.active;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002F3E File Offset: 0x0000113E
		public string Name
		{
			get
			{
				this.CheckDisposed();
				return this.name;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F4C File Offset: 0x0000114C
		public AppDomain SandboxDomain
		{
			get
			{
				this.CheckDisposed();
				return this.sandbox;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F5C File Offset: 0x0000115C
		public ScriptAssembly[] Assemblies
		{
			get
			{
				this.CheckDisposed();
				ScriptAssembly[] array;
				lock (this)
				{
					array = this.loadedAssemblies.ToArray();
				}
				return array;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002FA4 File Offset: 0x000011A4
		public ScriptAssembly[] CompiledAssemblies
		{
			get
			{
				this.CheckDisposed();
				ScriptDomain.matchedAssemblies.Clear();
				lock (this)
				{
					foreach (ScriptAssembly scriptAssembly in this.loadedAssemblies)
					{
						if (scriptAssembly.IsRuntimeCompiled)
						{
							ScriptDomain.matchedAssemblies.Add(scriptAssembly);
						}
					}
				}
				return ScriptDomain.matchedAssemblies.ToArray();
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003040 File Offset: 0x00001240
		public IEnumerable<ScriptAssembly> EnumerateAssemblies
		{
			get
			{
				this.CheckDisposed();
				IEnumerable<ScriptAssembly> enumerable;
				lock (this)
				{
					enumerable = this.loadedAssemblies;
				}
				return enumerable;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003084 File Offset: 0x00001284
		public IEnumerable<ScriptAssembly> EnumerateCompiledAssemblies
		{
			get
			{
				this.CheckDisposed();
				ScriptDomain scriptDomain = this;
				lock (scriptDomain)
				{
					foreach (ScriptAssembly scriptAssembly in this.loadedAssemblies)
					{
						if (scriptAssembly.IsRuntimeCompiled)
						{
							yield return scriptAssembly;
						}
					}
					List<ScriptAssembly>.Enumerator enumerator = default(List<ScriptAssembly>.Enumerator);
				}
				scriptDomain = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003094 File Offset: 0x00001294
		public RoslynCSharpCompiler RoslynCompilerService
		{
			get
			{
				this.CheckDisposed();
				return this.sharedCompiler;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000030A2 File Offset: 0x000012A2
		public CompilationResult CompileResult
		{
			get
			{
				this.CheckDisposed();
				return this.compileResult;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000030B0 File Offset: 0x000012B0
		public CodeSecurityReport SecurityResult
		{
			get
			{
				return this.securityResult;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000030B8 File Offset: 0x000012B8
		public bool IsCompilerServiceInitialized
		{
			get
			{
				this.CheckDisposed();
				return this.sharedCompiler != null;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000030C9 File Offset: 0x000012C9
		public bool IsDisposed
		{
			get
			{
				return this.sandbox == null;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030D4 File Offset: 0x000012D4
		private ScriptDomain(string name, AppDomain sandboxDomain = null)
		{
			this.name = name;
			this.sandbox = sandboxDomain;
			if (sandboxDomain == null)
			{
				this.sandbox = AppDomain.CurrentDomain;
			}
			ScriptDomain.activeDomains.Add(this);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003110 File Offset: 0x00001310
		public ScriptAssembly LoadAssemblyFromResources(string resourcePath, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);
			if (textAsset == null)
			{
				throw new DllNotFoundException(string.Format("Failed to load dll from resources path '{0}'", resourcePath));
			}
			return this.LoadAssembly(textAsset.bytes, securityMode);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003154 File Offset: 0x00001354
		public ScriptAssembly LoadAssembly(string fullPath, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			AssemblyName assemblyName = AssemblyName.GetAssemblyName(fullPath);
			Assembly assembly = this.sandbox.Load(assemblyName);
			return this.RegisterAssembly(assembly, fullPath, null, securityMode, false, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003188 File Offset: 0x00001388
		public ScriptAssembly LoadAssembly(AssemblyName name, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			Assembly assembly = this.sandbox.Load(name);
			return this.RegisterAssembly(assembly, assembly.Location, null, securityMode, false, null);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031BC File Offset: 0x000013BC
		public ScriptAssembly LoadAssembly(byte[] assemblyBytes, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			Assembly assembly = this.sandbox.Load(assemblyBytes);
			return this.RegisterAssembly(assembly, null, assemblyBytes, securityMode, false, null);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000031E8 File Offset: 0x000013E8
		public AsyncLoadOperation LoadAssemblyAsync(string fullPath, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			return new AsyncLoadOperation(this, fullPath, securityMode);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031F8 File Offset: 0x000013F8
		public AsyncLoadOperation LoadAssemblyAsync(AssemblyName name, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			return new AsyncLoadOperation(this, name, securityMode);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003208 File Offset: 0x00001408
		public AsyncLoadOperation LoadAssemblyAsync(byte[] assemblyBytes, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			return new AsyncLoadOperation(this, assemblyBytes, securityMode);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003218 File Offset: 0x00001418
		public bool TryLoadAssembly(string fullPath, out ScriptAssembly result, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			bool flag;
			try
			{
				result = this.LoadAssembly(fullPath, securityMode);
				flag = true;
			}
			catch (Exception)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003254 File Offset: 0x00001454
		public bool TryLoadAssembly(AssemblyName name, out ScriptAssembly result, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			bool flag;
			try
			{
				result = this.LoadAssembly(name, securityMode);
				flag = true;
			}
			catch (Exception)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003290 File Offset: 0x00001490
		public bool TryLoadAssembly(byte[] data, out ScriptAssembly result, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings)
		{
			this.CheckDisposed();
			bool flag;
			try
			{
				result = this.LoadAssembly(data, securityMode);
				flag = true;
			}
			catch (Exception)
			{
				result = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000032CC File Offset: 0x000014CC
		public ScriptType CompileAndLoadMainSource(string cSharpSource, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			ScriptAssembly scriptAssembly = this.CompileAndLoadSource(cSharpSource, securityMode, additionalReferenceAssemblies);
			if (scriptAssembly != null && scriptAssembly.MainType != null)
			{
				return scriptAssembly.MainType;
			}
			return null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000032F8 File Offset: 0x000014F8
		public ScriptType CompileAndLoadMainFile(string cSharpFile, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			ScriptAssembly scriptAssembly = this.CompileAndLoadFile(cSharpFile, securityMode, additionalReferenceAssemblies);
			if (scriptAssembly != null && scriptAssembly.MainType != null)
			{
				return scriptAssembly.MainType;
			}
			return null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003324 File Offset: 0x00001524
		public ScriptAssembly CompileAndLoadSource(string cSharpSource, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			ScriptAssembly scriptAssembly;
			lock (this)
			{
				this.compileResult = this.sharedCompiler.CompileFromSource(cSharpSource, additionalReferenceAssemblies);
				this.LogCompilerOutputToConsole();
				scriptAssembly = this.RegisterAssembly(this.compileResult.OutputAssembly, this.compileResult.OutputFile, this.compileResult.OutputAssemblyImage, securityMode, true, this.compileResult);
			}
			return scriptAssembly;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033B0 File Offset: 0x000015B0
		public ScriptAssembly CompileAndLoadFile(string cSharpFile, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			ScriptAssembly scriptAssembly;
			lock (this)
			{
				this.compileResult = this.sharedCompiler.CompileFromFile(cSharpFile, additionalReferenceAssemblies);
				this.LogCompilerOutputToConsole();
				scriptAssembly = this.RegisterAssembly(this.compileResult.OutputAssembly, this.compileResult.OutputFile, this.compileResult.OutputAssemblyImage, securityMode, true, this.compileResult);
			}
			return scriptAssembly;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000343C File Offset: 0x0000163C
		public ScriptAssembly CompileAndLoadSources(string[] cSharpSources, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			ScriptAssembly scriptAssembly;
			lock (this)
			{
				this.compileResult = this.sharedCompiler.CompileFromSources(cSharpSources, additionalReferenceAssemblies);
				this.LogCompilerOutputToConsole();
				scriptAssembly = this.RegisterAssembly(this.compileResult.OutputAssembly, this.compileResult.OutputFile, this.compileResult.OutputAssemblyImage, securityMode, true, this.compileResult);
			}
			return scriptAssembly;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000034C8 File Offset: 0x000016C8
		public ScriptAssembly CompileAndLoadFiles(string[] cSharpFiles, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			ScriptAssembly scriptAssembly;
			lock (this)
			{
				this.compileResult = this.sharedCompiler.CompileFromFiles(cSharpFiles, additionalReferenceAssemblies);
				this.LogCompilerOutputToConsole();
				scriptAssembly = this.RegisterAssembly(this.compileResult.OutputAssembly, this.compileResult.OutputFile, this.compileResult.OutputAssemblyImage, securityMode, true, this.compileResult);
			}
			return scriptAssembly;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003554 File Offset: 0x00001754
		public AsyncCompileOperation CompileAndLoadSourceAsync(string cSharpSource, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			return new AsyncCompileOperation(this, true, securityMode, new string[] { cSharpSource }, additionalReferenceAssemblies);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003575 File Offset: 0x00001775
		public AsyncCompileOperation CompileAndLoadFileAsync(string cSharpFile, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			return new AsyncCompileOperation(this, false, securityMode, new string[] { cSharpFile }, additionalReferenceAssemblies);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003596 File Offset: 0x00001796
		public AsyncCompileOperation CompileAndLoadSourcesAsync(string[] cSharpSources, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			return new AsyncCompileOperation(this, true, securityMode, cSharpSources, additionalReferenceAssemblies);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000035AE File Offset: 0x000017AE
		public AsyncCompileOperation CompileAndLoadFilesAsync(string[] cSharpFiles, ScriptSecurityMode securityMode = ScriptSecurityMode.UseSettings, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.CheckDisposed();
			this.CheckCompiler();
			return new AsyncCompileOperation(this, false, securityMode, cSharpFiles, additionalReferenceAssemblies);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000035C8 File Offset: 0x000017C8
		public void Dispose()
		{
			if (this.sandbox != null)
			{
				if (!this.sandbox.IsDefaultAppDomain())
				{
					AppDomain.Unload(this.sandbox);
				}
				ScriptDomain.activeDomains.Remove(this);
				lock (this)
				{
					this.loadedAssemblies.Clear();
				}
				this.sandbox = null;
				this.sharedCompiler = null;
				this.securityResult = null;
				this.compileResult = null;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003650 File Offset: 0x00001850
		public void InitializeCompilerService()
		{
			if (this.sharedCompiler == null)
			{
				this.sharedCompiler = new RoslynCSharpCompiler(true, true, OutputKind.DynamicallyLinkedLibrary, LanguageVersion.Default, this.sandbox);
				this.ApplyCompilerServiceSettings();
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003678 File Offset: 0x00001878
		public void ApplyCompilerServiceSettings()
		{
			if (this.sharedCompiler == null)
			{
				return;
			}
			RoslynCSharp settings = RoslynCSharp.Settings;
			this.sharedCompiler.AllowUnsafe = settings.AllowUnsafeCode;
			this.sharedCompiler.AllowOptimize = settings.AllowOptimizeCode;
			this.sharedCompiler.AllowConcurrentCompile = settings.AllowConcurrentCompile;
			this.sharedCompiler.GenerateInMemory = settings.GenerateInMemory;
			this.sharedCompiler.GenerateSymbols = settings.GenerateSymbols;
			this.sharedCompiler.WarningLevel = settings.WarningLevel;
			this.sharedCompiler.LanguageVersion = settings.LanguageVersion;
			this.sharedCompiler.TargetPlatform = settings.TargetPlatform;
			this.sharedCompiler.ReferenceAssemblies.Clear();
			foreach (string text in settings.References)
			{
				this.sharedCompiler.ReferenceAssemblies.Add(AssemblyReference.FromNameOrFile(text, null));
			}
			this.sharedCompiler.DefineSymbols.Clear();
			foreach (string text2 in settings.DefineSymbols)
			{
				this.sharedCompiler.DefineSymbols.Add(text2);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000037D4 File Offset: 0x000019D4
		public void LogCompilerOutputToConsole()
		{
			if (this.compileResult == null)
			{
				return;
			}
			bool loggedHeader = false;
			Action action = delegate
			{
				if (!loggedHeader)
				{
					loggedHeader = true;
				}
			};
			foreach (CompilationError compilationError in this.compileResult.Errors)
			{
				if (compilationError.IsError)
				{
					action();
				}
				else if (compilationError.IsWarning)
				{
					action();
				}
				else if (compilationError.IsInfo)
				{
					action();
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003854 File Offset: 0x00001A54
		private void CheckDisposed()
		{
			if (this.sandbox == null)
			{
				throw new ObjectDisposedException("The 'ScriptDomain' has already been disposed");
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003869 File Offset: 0x00001A69
		private void CheckCompiler()
		{
			if (this.sharedCompiler == null)
			{
				throw new Exception("The compiler service has not been initialized");
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003880 File Offset: 0x00001A80
		private ScriptAssembly RegisterAssembly(Assembly assembly, string assemblyPath, byte[] assemblyImage, ScriptSecurityMode securityMode, bool isRuntimeCompiled, CompilationResult compileResult = null)
		{
			if (assembly == null)
			{
				return null;
			}
			this.securityResult = null;
			ScriptAssembly scriptAssembly = new ScriptAssembly(this, assembly, compileResult);
			scriptAssembly.AssemblyPath = assemblyPath;
			scriptAssembly.AssemblyImage = assemblyImage;
			bool flag = securityMode == ScriptSecurityMode.EnsureSecurity;
			if (securityMode == ScriptSecurityMode.UseSettings)
			{
				flag = RoslynCSharp.Settings.SecurityCheckCode;
			}
			if (flag && !scriptAssembly.SecurityCheckAssembly(RoslynCSharp.Settings.SecurityRestrictions, out this.securityResult))
			{
				return null;
			}
			if (isRuntimeCompiled)
			{
				scriptAssembly.MarkAsRuntimeCompiled();
			}
			lock (this)
			{
				this.loadedAssemblies.Add(scriptAssembly);
			}
			return scriptAssembly;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003928 File Offset: 0x00001B28
		public static ScriptDomain CreateDomain(string domainName, bool initCompiler = true, bool makeActiveDomain = true, AppDomain sandboxDomain = null)
		{
			ScriptDomain scriptDomain = new ScriptDomain(domainName, sandboxDomain);
			RoslynCSharp.LoadResources();
			if (initCompiler)
			{
				scriptDomain.InitializeCompilerService();
			}
			if (makeActiveDomain)
			{
				ScriptDomain.MakeDomainActive(scriptDomain);
			}
			return scriptDomain;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003958 File Offset: 0x00001B58
		public static ScriptDomain FindDomain(string domainName)
		{
			foreach (ScriptDomain scriptDomain in ScriptDomain.activeDomains)
			{
				if (scriptDomain.name == domainName)
				{
					return scriptDomain;
				}
			}
			return null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000039B8 File Offset: 0x00001BB8
		public static void MakeDomainActive(ScriptDomain domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			ScriptDomain.active = domain;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000039D0 File Offset: 0x00001BD0
		public static void MakeDomainActive(string domainName)
		{
			ScriptDomain scriptDomain = ScriptDomain.FindDomain(domainName);
			if (scriptDomain != null)
			{
				ScriptDomain.MakeDomainActive(scriptDomain);
			}
		}

		// Token: 0x04000035 RID: 53
		private static List<ScriptDomain> activeDomains = new List<ScriptDomain>();

		// Token: 0x04000036 RID: 54
		private static List<ScriptAssembly> matchedAssemblies = new List<ScriptAssembly>();

		// Token: 0x04000037 RID: 55
		private static ScriptDomain active = null;

		// Token: 0x04000038 RID: 56
		private string name;

		// Token: 0x04000039 RID: 57
		private AppDomain sandbox;

		// Token: 0x0400003A RID: 58
		private List<ScriptAssembly> loadedAssemblies = new List<ScriptAssembly>();

		// Token: 0x0400003B RID: 59
		private RoslynCSharpCompiler sharedCompiler;

		// Token: 0x0400003C RID: 60
		private CodeSecurityReport securityResult;

		// Token: 0x0400003D RID: 61
		private CompilationResult compileResult;
	}
}
