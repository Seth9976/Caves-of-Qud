using System;
using System.IO;
using System.Security.Cryptography;
using Trivial.CodeSecurity.CodeEvaluation;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x02000003 RID: 3
	public sealed class CodeSecurityEngine : IDisposable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002171 File Offset: 0x00000371
		public string CheckAssemblyName
		{
			get
			{
				return this.definition.Name.FullName;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002184 File Offset: 0x00000384
		static CodeSecurityEngine()
		{
			string location = typeof(CodeSecurityEngine).Assembly.Location;
			try
			{
				CodeSecurityEngine.assemblyDefinitionBytes = File.ReadAllBytes(location);
			}
			catch
			{
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C8 File Offset: 0x000003C8
		public CodeSecurityEngine(string assemblyDefinitionFile)
		{
			if (assemblyDefinitionFile == null)
			{
				throw new ArgumentNullException("assemblyDefinitionFile");
			}
			this.loadStream = File.OpenRead(assemblyDefinitionFile);
			this.InitializeFromStream(this.loadStream, null);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F8 File Offset: 0x000003F8
		public CodeSecurityEngine(byte[] assemblyImage, byte[] assemblySymbolsImage = null)
		{
			if (assemblyImage == null)
			{
				throw new ArgumentNullException("assemblyImage");
			}
			this.loadStream = new MemoryStream(assemblyImage);
			if (assemblySymbolsImage != null)
			{
				this.loadSymbolStream = new MemoryStream(assemblySymbolsImage);
			}
			this.InitializeFromStream(this.loadStream, this.loadSymbolStream);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002246 File Offset: 0x00000446
		public CodeSecurityEngine(Stream assemblyDefinitionStream, Stream symbolStream = null)
		{
			if (assemblyDefinitionStream == null)
			{
				throw new ArgumentNullException("assemblyDefinitionStream");
			}
			this.InitializeFromStream(assemblyDefinitionStream, symbolStream);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002264 File Offset: 0x00000464
		public void Dispose()
		{
			if (this.definition != null)
			{
				this.definition.Dispose();
				this.definition = null;
				if (this.loadStream != null)
				{
					this.loadStream.Dispose();
					this.loadStream = null;
				}
				if (this.loadSymbolStream != null)
				{
					this.loadSymbolStream.Dispose();
					this.loadSymbolStream = null;
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		public bool SecurityCheckAssembly(CodeSecurityRestrictions restrictions)
		{
			this.CheckDisposed();
			CodeSecurityReport codeSecurityReport;
			return this.SecurityCheckAssembly(restrictions, out codeSecurityReport);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022DC File Offset: 0x000004DC
		public bool SecurityCheckAssembly(CodeSecurityRestrictions restrictions, out CodeSecurityReport report)
		{
			this.CheckDisposed();
			this.restrictions = restrictions;
			return this.SecurityCheckAssembly(restrictions, out report);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022F4 File Offset: 0x000004F4
		public bool SecurityCheckAssembly(ICodeSecurityValidator validator)
		{
			this.CheckDisposed();
			CodeSecurityReport codeSecurityReport;
			return this.SecurityCheckAssembly(validator, out codeSecurityReport);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002310 File Offset: 0x00000510
		public bool SecurityCheckAssembly(ICodeSecurityValidator validator, out CodeSecurityReport report)
		{
			this.CheckDisposed();
			report = new CodeSecurityReport(this.definition, this.restrictions);
			CodeSecurityContext codeSecurityContext = new CodeSecurityContext(this.definition, report, validator);
			ModuleChecker moduleChecker = new ModuleChecker();
			foreach (ModuleDefinition moduleDefinition in this.definition.Modules)
			{
				moduleChecker.SecurityCheckModuleReferences(codeSecurityContext, moduleDefinition);
				moduleChecker.SecurityCheckModuleCode(codeSecurityContext, moduleDefinition);
			}
			return report.IsSecurityVerified;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023A8 File Offset: 0x000005A8
		public byte[] GenerateSecurityHash()
		{
			this.CheckDisposed();
			if (this.definitionBytes == null)
			{
				throw new InvalidOperationException("Cannot generate security hash because the assembly data was not captured on initialize. Make sure you pass a seekable stream to the constructor");
			}
			try
			{
				byte[] array = new SHA256Managed().ComputeHash(this.definitionBytes);
				byte[] array2 = CodeSecurityEngine.SafeGenerateAssemblySecurityHash();
				for (int i = 0; i < array.Length; i++)
				{
					array[i] |= array2[i];
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002418 File Offset: 0x00000618
		public static byte[] GenerateAssemblySecurityHash()
		{
			if (CodeSecurityEngine.assemblyDefinitionBytes == null)
			{
				throw new InvalidOperationException("Cannot generate security hash because the location of the calling assembly could not be determined at runtime");
			}
			return CodeSecurityEngine.SafeGenerateAssemblySecurityHash();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002434 File Offset: 0x00000634
		internal static byte[] SafeGenerateAssemblySecurityHash()
		{
			if (CodeSecurityEngine.assemblyDefinitionBytes == null)
			{
				return null;
			}
			try
			{
				return new SHA256Managed().ComputeHash(CodeSecurityEngine.assemblyDefinitionBytes);
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002474 File Offset: 0x00000674
		private void InitializeFromStream(Stream loadStream, Stream symbolsStream = null)
		{
			this.CaptureAssemblyFromStream(loadStream);
			this.definition = AssemblyDefinition.ReadAssembly(loadStream, new ReaderParameters
			{
				ReadSymbols = true
			});
			if (this.definition == null)
			{
				throw new Exception("Failed to read assembly definition for code evaluation");
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024B8 File Offset: 0x000006B8
		private void CaptureAssemblyFromStream(Stream inputStream)
		{
			if (!inputStream.CanSeek)
			{
				return;
			}
			long position = inputStream.Position;
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[4096];
			int num;
			while ((num = inputStream.Read(array, 0, array.Length)) > 0)
			{
				memoryStream.Write(array, 0, num);
			}
			this.definitionBytes = memoryStream.ToArray();
			memoryStream.Dispose();
			inputStream.Seek(position, 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000251E File Offset: 0x0000071E
		private void CheckDisposed()
		{
			if (this.definition == null)
			{
				throw new ObjectDisposedException("The code security engine has already been disposed");
			}
		}

		// Token: 0x04000007 RID: 7
		private static byte[] assemblyDefinitionBytes;

		// Token: 0x04000008 RID: 8
		private Stream loadStream;

		// Token: 0x04000009 RID: 9
		private Stream loadSymbolStream;

		// Token: 0x0400000A RID: 10
		private AssemblyDefinition definition;

		// Token: 0x0400000B RID: 11
		private CodeSecurityRestrictions restrictions;

		// Token: 0x0400000C RID: 12
		private byte[] definitionBytes;
	}
}
