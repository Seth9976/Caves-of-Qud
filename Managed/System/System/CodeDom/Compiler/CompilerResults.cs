using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the results of compilation that are returned from a compiler.</summary>
	// Token: 0x02000350 RID: 848
	[Serializable]
	public class CompilerResults
	{
		/// <summary>Indicates the evidence object that represents the security policy permissions of the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object that represents the security policy permissions of the compiled assembly.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0006674C File Offset: 0x0006494C
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0006675F File Offset: 0x0006495F
		[Obsolete("CAS policy is obsolete and will be removed in a future release of the .NET Framework. Please see http://go2.microsoft.com/fwlink/?LinkId=131738 for more information.")]
		public Evidence Evidence
		{
			get
			{
				Evidence evidence = this._evidence;
				if (evidence == null)
				{
					return null;
				}
				return evidence.Clone();
			}
			set
			{
				this._evidence = ((value != null) ? value.Clone() : null);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerResults" /> class that uses the specified temporary files.</summary>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06001C05 RID: 7173 RVA: 0x00066773 File Offset: 0x00064973
		public CompilerResults(TempFileCollection tempFiles)
		{
			this._tempFiles = tempFiles;
		}

		/// <summary>Gets or sets the temporary file collection to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x00066798 File Offset: 0x00064998
		// (set) Token: 0x06001C07 RID: 7175 RVA: 0x000667A0 File Offset: 0x000649A0
		public TempFileCollection TempFiles
		{
			get
			{
				return this._tempFiles;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that indicates the compiled assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x000667A9 File Offset: 0x000649A9
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x000667E3 File Offset: 0x000649E3
		public Assembly CompiledAssembly
		{
			get
			{
				if (this._compiledAssembly == null && this.PathToAssembly != null)
				{
					this._compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.PathToAssembly
					});
				}
				return this._compiledAssembly;
			}
			set
			{
				this._compiledAssembly = value;
			}
		}

		/// <summary>Gets the collection of compiler errors and warnings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that indicates the errors and warnings resulting from compilation, if any.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x000667EC File Offset: 0x000649EC
		public CompilerErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the compiler output messages.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the output messages.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000667F4 File Offset: 0x000649F4
		public StringCollection Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets or sets the path of the compiled assembly.</summary>
		/// <returns>The path of the assembly, or null if the assembly was generated in memory.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x000667FC File Offset: 0x000649FC
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x00066804 File Offset: 0x00064A04
		public string PathToAssembly { get; set; }

		/// <summary>Gets or sets the compiler's return value.</summary>
		/// <returns>The compiler's return value.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0006680D File Offset: 0x00064A0D
		// (set) Token: 0x06001C0F RID: 7183 RVA: 0x00066815 File Offset: 0x00064A15
		public int NativeCompilerReturnValue { get; set; }

		// Token: 0x04000E67 RID: 3687
		private Evidence _evidence;

		// Token: 0x04000E68 RID: 3688
		private readonly CompilerErrorCollection _errors = new CompilerErrorCollection();

		// Token: 0x04000E69 RID: 3689
		private readonly StringCollection _output = new StringCollection();

		// Token: 0x04000E6A RID: 3690
		private Assembly _compiledAssembly;

		// Token: 0x04000E6B RID: 3691
		private TempFileCollection _tempFiles;
	}
}
