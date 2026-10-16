using System;
using RoslynCSharp.Compiler;

namespace RoslynCSharp
{
	// Token: 0x02000003 RID: 3
	public sealed class AsyncCompileOperation : AsyncOperation
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public ScriptDomain CompileDomain
		{
			get
			{
				ScriptDomain scriptDomain = this.compileDomain;
				ScriptDomain scriptDomain2;
				lock (scriptDomain)
				{
					scriptDomain2 = this.compileDomain;
				}
				return scriptDomain2;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public ScriptType CompiledType
		{
			get
			{
				object obj = this.assemblyAccessLock;
				ScriptType scriptType;
				lock (obj)
				{
					if (this.compileResult == null)
					{
						scriptType = null;
					}
					else
					{
						scriptType = this.compileResult.MainType;
					}
				}
				return scriptType;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		public ScriptAssembly CompiledAssembly
		{
			get
			{
				object obj = this.assemblyAccessLock;
				ScriptAssembly scriptAssembly;
				lock (obj)
				{
					scriptAssembly = this.compileResult;
				}
				return scriptAssembly;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000219C File Offset: 0x0000039C
		public bool IsSecurityVerified
		{
			get
			{
				return this.isSecurityVerified;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021A4 File Offset: 0x000003A4
		internal AsyncCompileOperation(ScriptDomain domain, bool isCSharpSource, ScriptSecurityMode securityMode, string[] sourceOrFiles, IMetadataReferenceProvider[] additionalReferenceAssemblies = null)
		{
			this.compileDomain = domain;
			this.sourceCompileType = (isCSharpSource ? AsyncCompileOperation.CompileType.CompileSource : AsyncCompileOperation.CompileType.CompileFile);
			this.securityMode = securityMode;
			this.sourceOrFiles = sourceOrFiles;
			this.additionalReferences = additionalReferenceAssemblies;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E4 File Offset: 0x000003E4
		protected override void RunAsyncOperation()
		{
			ScriptAssembly scriptAssembly = null;
			ScriptDomain scriptDomain = this.compileDomain;
			lock (scriptDomain)
			{
				AsyncCompileOperation.CompileType compileType = this.sourceCompileType;
				if (compileType != AsyncCompileOperation.CompileType.CompileSource)
				{
					if (compileType == AsyncCompileOperation.CompileType.CompileFile)
					{
						if (this.sourceOrFiles.Length == 1)
						{
							scriptAssembly = this.compileDomain.CompileAndLoadFile(this.sourceOrFiles[0], this.securityMode, this.additionalReferences);
						}
						else
						{
							scriptAssembly = this.compileDomain.CompileAndLoadFiles(this.sourceOrFiles, this.securityMode, this.additionalReferences);
						}
					}
				}
				else if (this.sourceOrFiles.Length == 1)
				{
					scriptAssembly = this.compileDomain.CompileAndLoadSource(this.sourceOrFiles[0], this.securityMode, this.additionalReferences);
				}
				else
				{
					scriptAssembly = this.compileDomain.CompileAndLoadSources(this.sourceOrFiles, this.securityMode, this.additionalReferences);
				}
				object obj = this.assemblyAccessLock;
				lock (obj)
				{
					this.compileResult = scriptAssembly;
				}
				this.isSuccessful = this.compileDomain.CompileResult.Success;
				this.isSecurityVerified = this.compileDomain.SecurityResult != null && this.compileDomain.SecurityResult.IsSecurityVerified;
			}
		}

		// Token: 0x04000001 RID: 1
		private object assemblyAccessLock = new object();

		// Token: 0x04000002 RID: 2
		private ScriptDomain compileDomain;

		// Token: 0x04000003 RID: 3
		private ScriptAssembly compileResult;

		// Token: 0x04000004 RID: 4
		private ScriptSecurityMode securityMode;

		// Token: 0x04000005 RID: 5
		private bool isSecurityVerified;

		// Token: 0x04000006 RID: 6
		private AsyncCompileOperation.CompileType sourceCompileType;

		// Token: 0x04000007 RID: 7
		private string[] sourceOrFiles;

		// Token: 0x04000008 RID: 8
		private IMetadataReferenceProvider[] additionalReferences;

		// Token: 0x02000012 RID: 18
		private enum CompileType
		{
			// Token: 0x04000065 RID: 101
			CompileSource,
			// Token: 0x04000066 RID: 102
			CompileFile
		}
	}
}
