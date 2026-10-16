using System;
using System.Collections.Specialized;
using System.Security.Policy;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the parameters used to invoke a compiler.</summary>
	// Token: 0x0200034F RID: 847
	[Serializable]
	public class CompilerParameters
	{
		/// <summary>Specifies an evidence object that represents the security policy permissions to grant the compiled assembly.</summary>
		/// <returns>An  object that represents the security policy permissions to grant the compiled assembly.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0006659E File Offset: 0x0006479E
		// (set) Token: 0x06001BE3 RID: 7139 RVA: 0x000665B1 File Offset: 0x000647B1
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

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class.</summary>
		// Token: 0x06001BE4 RID: 7140 RVA: 0x000665C5 File Offset: 0x000647C5
		public CompilerParameters()
			: this(null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		// Token: 0x06001BE5 RID: 7141 RVA: 0x000665CF File Offset: 0x000647CF
		public CompilerParameters(string[] assemblyNames)
			: this(assemblyNames, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names and output file name.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		// Token: 0x06001BE6 RID: 7142 RVA: 0x000665DA File Offset: 0x000647DA
		public CompilerParameters(string[] assemblyNames, string outputName)
			: this(assemblyNames, outputName, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> class using the specified assembly names, output name, and a value indicating whether to include debug information.</summary>
		/// <param name="assemblyNames">The names of the assemblies to reference. </param>
		/// <param name="outputName">The output file name. </param>
		/// <param name="includeDebugInformation">true to include debug information; false to exclude debug information. </param>
		// Token: 0x06001BE7 RID: 7143 RVA: 0x000665E8 File Offset: 0x000647E8
		public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation)
		{
			if (assemblyNames != null)
			{
				this.ReferencedAssemblies.AddRange(assemblyNames);
			}
			this.OutputAssembly = outputName;
			this.IncludeDebugInformation = includeDebugInformation;
		}

		/// <summary>Gets or sets the name of the core or standard assembly that contains basic types such as <see cref="T:System.Object" />, <see cref="T:System.String" />, or <see cref="T:System.Int32" />.</summary>
		/// <returns>The name of the core assembly that contains basic types.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x0006664B File Offset: 0x0006484B
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00066653 File Offset: 0x00064853
		public string CoreAssemblyFileName { get; set; } = string.Empty;

		/// <summary>Gets or sets a value indicating whether to generate an executable.</summary>
		/// <returns>true if an executable should be generated; otherwise, false.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0006665C File Offset: 0x0006485C
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x00066664 File Offset: 0x00064864
		public bool GenerateExecutable { get; set; }

		/// <summary>Gets or sets a value indicating whether to generate the output in memory.</summary>
		/// <returns>true if the compiler should generate the output in memory; otherwise, false.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0006666D File Offset: 0x0006486D
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x00066675 File Offset: 0x00064875
		public bool GenerateInMemory { get; set; }

		/// <summary>Gets the assemblies referenced by the current project.</summary>
		/// <returns>A collection that contains the assembly names that are referenced by the source to compile.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0006667E File Offset: 0x0006487E
		public StringCollection ReferencedAssemblies
		{
			get
			{
				return this._assemblyNames;
			}
		}

		/// <summary>Gets or sets the name of the main class.</summary>
		/// <returns>The name of the main class.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00066686 File Offset: 0x00064886
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x0006668E File Offset: 0x0006488E
		public string MainClass { get; set; }

		/// <summary>Gets or sets the name of the output assembly.</summary>
		/// <returns>The name of the output assembly.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00066697 File Offset: 0x00064897
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x0006669F File Offset: 0x0006489F
		public string OutputAssembly { get; set; }

		/// <summary>Gets or sets the collection that contains the temporary files.</summary>
		/// <returns>A collection that contains the temporary files.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000666A8 File Offset: 0x000648A8
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x000666CD File Offset: 0x000648CD
		public TempFileCollection TempFiles
		{
			get
			{
				TempFileCollection tempFileCollection;
				if ((tempFileCollection = this._tempFiles) == null)
				{
					tempFileCollection = (this._tempFiles = new TempFileCollection());
				}
				return tempFileCollection;
			}
			set
			{
				this._tempFiles = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to include debug information in the compiled executable.</summary>
		/// <returns>true if debug information should be generated; otherwise, false.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000666D6 File Offset: 0x000648D6
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x000666DE File Offset: 0x000648DE
		public bool IncludeDebugInformation { get; set; }

		/// <summary>Gets or sets a value indicating whether to treat warnings as errors.</summary>
		/// <returns>true if warnings should be treated as errors; otherwise, false.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000666E7 File Offset: 0x000648E7
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x000666EF File Offset: 0x000648EF
		public bool TreatWarningsAsErrors { get; set; }

		/// <summary>Gets or sets the warning level at which the compiler aborts compilation.</summary>
		/// <returns>The warning level at which the compiler aborts compilation.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x000666F8 File Offset: 0x000648F8
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00066700 File Offset: 0x00064900
		public int WarningLevel { get; set; } = -1;

		/// <summary>Gets or sets optional command-line arguments to use when invoking the compiler.</summary>
		/// <returns>Any additional command-line arguments for the compiler.</returns>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00066709 File Offset: 0x00064909
		// (set) Token: 0x06001BFC RID: 7164 RVA: 0x00066711 File Offset: 0x00064911
		public string CompilerOptions { get; set; }

		/// <summary>Gets or sets the file name of a Win32 resource file to link into the compiled assembly.</summary>
		/// <returns>A Win32 resource file that will be linked into the compiled assembly.</returns>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x0006671A File Offset: 0x0006491A
		// (set) Token: 0x06001BFE RID: 7166 RVA: 0x00066722 File Offset: 0x00064922
		public string Win32Resource { get; set; }

		/// <summary>Gets the .NET Framework resource files to include when compiling the assembly output.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources to include in the generated assembly.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0006672B File Offset: 0x0006492B
		public StringCollection EmbeddedResources
		{
			get
			{
				return this._embeddedResources;
			}
		}

		/// <summary>Gets the .NET Framework resource files that are referenced in the current source.</summary>
		/// <returns>A collection that contains the file paths of .NET Framework resources that are referenced by the source.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00066733 File Offset: 0x00064933
		public StringCollection LinkedResources
		{
			get
			{
				return this._linkedResources;
			}
		}

		/// <summary>Gets or sets the user token to use when creating the compiler process.</summary>
		/// <returns>The user token to use.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0006673B File Offset: 0x0006493B
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x00066743 File Offset: 0x00064943
		public IntPtr UserToken { get; set; }

		// Token: 0x04000E57 RID: 3671
		private Evidence _evidence;

		// Token: 0x04000E58 RID: 3672
		private readonly StringCollection _assemblyNames = new StringCollection();

		// Token: 0x04000E59 RID: 3673
		private readonly StringCollection _embeddedResources = new StringCollection();

		// Token: 0x04000E5A RID: 3674
		private readonly StringCollection _linkedResources = new StringCollection();

		// Token: 0x04000E5B RID: 3675
		private TempFileCollection _tempFiles;
	}
}
