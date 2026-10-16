using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Contains values passed to the ASP.NET compiler during precompilation.</summary>
	// Token: 0x02000645 RID: 1605
	[Serializable]
	public class ClientBuildManagerParameter
	{
		/// <summary>Gets or sets the flags that determine precompilation behavior.</summary>
		/// <returns>The <see cref="T:System.Web.Compilation.PrecompilationFlags" /> for a client build.</returns>
		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x0600450F RID: 17679 RVA: 0x000BB0F6 File Offset: 0x000B92F6
		// (set) Token: 0x06004510 RID: 17680 RVA: 0x000BB0FE File Offset: 0x000B92FE
		public PrecompilationFlags PrecompilationFlags
		{
			get
			{
				return this.precompilationFlags;
			}
			set
			{
				this.precompilationFlags = value;
			}
		}

		/// <summary>Gets or sets the key container used during compilation.</summary>
		/// <returns>A <see cref="T:System.String" /> of the value for the key container.</returns>
		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x06004511 RID: 17681 RVA: 0x000BB107 File Offset: 0x000B9307
		// (set) Token: 0x06004512 RID: 17682 RVA: 0x000BB10F File Offset: 0x000B930F
		public string StrongNameKeyContainer
		{
			get
			{
				return this.strongNameKeyContainer;
			}
			set
			{
				this.strongNameKeyContainer = value;
			}
		}

		/// <summary>Gets or sets the key file used during compilation.</summary>
		/// <returns>A <see cref="T:System.String" /> of the value for the key file.</returns>
		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06004513 RID: 17683 RVA: 0x000BB118 File Offset: 0x000B9318
		// (set) Token: 0x06004514 RID: 17684 RVA: 0x000BB120 File Offset: 0x000B9320
		public string StrongNameKeyFile
		{
			get
			{
				return this.strongNameKeyFile;
			}
			set
			{
				this.strongNameKeyFile = value;
			}
		}

		/// <summary>Gets or sets excluded virtual paths.</summary>
		/// <returns>The excluded virtual paths.</returns>
		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06004515 RID: 17685 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public List<string> ExcludedVirtualPaths
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		// Token: 0x040024D1 RID: 9425
		private PrecompilationFlags precompilationFlags;

		// Token: 0x040024D2 RID: 9426
		private string strongNameKeyContainer;

		// Token: 0x040024D3 RID: 9427
		private string strongNameKeyFile;
	}
}
