using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Provides information about a Program Debug Database (PDB) file.</summary>
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CodeViewDebugDirectoryData
	{
		/// <summary>The Globally Unique Identifier (GUID) of the associated PDB.</summary>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000052D2 File Offset: 0x000034D2
		public Guid Guid { get; }

		/// <summary>The iteration of the PDB. The first iteration is 1. The iteration is incremented each time the PDB content is augmented.</summary>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000052DA File Offset: 0x000034DA
		public int Age { get; }

		/// <summary>The path to the .pdb file that contains debug information for the PE/COFF file.</summary>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000052E2 File Offset: 0x000034E2
		public string Path { get; }

		// Token: 0x06000189 RID: 393 RVA: 0x000052EA File Offset: 0x000034EA
		internal CodeViewDebugDirectoryData(Guid guid, int age, string path)
		{
			this.Path = path;
			this.Guid = guid;
			this.Age = age;
		}
	}
}
