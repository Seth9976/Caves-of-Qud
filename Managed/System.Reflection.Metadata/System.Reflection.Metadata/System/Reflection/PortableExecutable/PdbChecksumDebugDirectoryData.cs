using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents a PDB Checksum debug directory entry.</summary>
	// Token: 0x02000022 RID: 34
	public readonly struct PdbChecksumDebugDirectoryData
	{
		/// <summary>The name of the crypto hash algorithm used to calculate the checksum.</summary>
		/// <returns>A string representing the name of the crypto hash algorithm used to calculate the checksum.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00004D88 File Offset: 0x00002F88
		[Nullable(1)]
		public string AlgorithmName
		{
			[NullableContext(1)]
			get;
		}

		/// <summary>The checksum of the PDB content.</summary>
		/// <returns>An immutable array of bytes representing the checksum of the PDB content.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00004D90 File Offset: 0x00002F90
		public ImmutableArray<byte> Checksum { get; }

		// Token: 0x06000135 RID: 309 RVA: 0x00004D98 File Offset: 0x00002F98
		internal PdbChecksumDebugDirectoryData([Nullable(1)] string algorithmName, ImmutableArray<byte> checksum)
		{
			this.AlgorithmName = algorithmName;
			this.Checksum = checksum;
		}
	}
}
