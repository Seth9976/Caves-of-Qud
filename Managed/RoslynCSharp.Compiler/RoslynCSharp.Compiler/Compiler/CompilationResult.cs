using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x0200000B RID: 11
	public sealed class CompilationResult : IMetadataReferenceProvider
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002A10 File Offset: 0x00000C10
		public MetadataReference Reference
		{
			get
			{
				if (!this.success)
				{
					throw new InvalidDataException("Cannot get matadata reference from compliation result because the compile was unsuccessful");
				}
				if (this.outputFile != null)
				{
					return new AssemblyReferenceFromFile(this.outputFile).Reference;
				}
				if (this.outputAssemblyImage != null)
				{
					return new AssemblyReferenceFromImage(this.outputAssemblyImage).Reference;
				}
				return null;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002A69 File Offset: 0x00000C69
		public bool Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002A71 File Offset: 0x00000C71
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002A79 File Offset: 0x00000C79
		public string OutputFile
		{
			get
			{
				return this.outputFile;
			}
			internal set
			{
				this.outputFile = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002A82 File Offset: 0x00000C82
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002A8A File Offset: 0x00000C8A
		public string OutputPDBFile
		{
			get
			{
				return this.outputPDBFile;
			}
			internal set
			{
				this.outputPDBFile = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A93 File Offset: 0x00000C93
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002A9B File Offset: 0x00000C9B
		public byte[] OutputAssemblyImage
		{
			get
			{
				return this.outputAssemblyImage;
			}
			internal set
			{
				this.outputAssemblyImage = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002AA4 File Offset: 0x00000CA4
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002AAC File Offset: 0x00000CAC
		public byte[] OutputPDBImage
		{
			get
			{
				return this.outputPDBImage;
			}
			internal set
			{
				this.outputPDBImage = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002AB5 File Offset: 0x00000CB5
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002ABD File Offset: 0x00000CBD
		public Assembly OutputAssembly
		{
			get
			{
				return this.outputAssembly;
			}
			internal set
			{
				this.outputAssembly = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public CompilationError[] Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public int ErrorCount
		{
			get
			{
				int num = 0;
				CompilationError[] array = this.errors;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsError)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002B04 File Offset: 0x00000D04
		public int WarningCount
		{
			get
			{
				int num = 0;
				CompilationError[] array = this.errors;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsWarning)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002B38 File Offset: 0x00000D38
		public int InfoCount
		{
			get
			{
				int num = 0;
				CompilationError[] array = this.errors;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].IsInfo)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B6B File Offset: 0x00000D6B
		internal CompilationResult(bool success, IEnumerable<Diagnostic> diagnostics)
		{
			this.success = success;
			this.CreateErrors(diagnostics);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B84 File Offset: 0x00000D84
		public Assembly LoadCompiledAssembly(AppDomain loadDomain = null)
		{
			if (!this.success)
			{
				return null;
			}
			if (this.outputAssembly != null)
			{
				return this.outputAssembly;
			}
			if (loadDomain == null)
			{
				loadDomain = AppDomain.CurrentDomain;
			}
			if (!string.IsNullOrEmpty(this.outputFile))
			{
				this.outputAssembly = loadDomain.Load(this.outputFile);
			}
			else if (this.outputAssemblyImage != null)
			{
				this.outputAssembly = loadDomain.Load(this.outputAssemblyImage);
			}
			return this.outputAssembly;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BFC File Offset: 0x00000DFC
		private void CreateErrors(IEnumerable<Diagnostic> diagnostics)
		{
			List<CompilationError> list = new List<CompilationError>();
			foreach (Diagnostic diagnostic in diagnostics)
			{
				string id = diagnostic.Id;
				if (!(id == "CS1701") && !(id == "CS1702") && diagnostic.Severity != null)
				{
					list.Add(new CompilationError(diagnostic));
				}
			}
			this.errors = list.ToArray();
		}

		// Token: 0x04000013 RID: 19
		private bool success;

		// Token: 0x04000014 RID: 20
		private string outputFile;

		// Token: 0x04000015 RID: 21
		private string outputPDBFile;

		// Token: 0x04000016 RID: 22
		private byte[] outputAssemblyImage;

		// Token: 0x04000017 RID: 23
		private byte[] outputPDBImage;

		// Token: 0x04000018 RID: 24
		private Assembly outputAssembly;

		// Token: 0x04000019 RID: 25
		private CompilationError[] errors;
	}
}
