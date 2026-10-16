using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Builds PE directories.</summary>
	// Token: 0x02000023 RID: 35
	public sealed class PEDirectoriesBuilder
	{
		/// <summary>The address of the entry point relative to the image base when the PE file is loaded into memory.</summary>
		/// <returns>For program images, this is the starting address. For device drivers, this is the address of the initialization function. An entry point is optional for DLLs. When no entry point is present, this field must be zero.</returns>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00004DA8 File Offset: 0x00002FA8
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public int AddressOfEntryPoint { get; set; }

		/// <summary>The export table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004DB9 File Offset: 0x00002FB9
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004DC1 File Offset: 0x00002FC1
		public DirectoryEntry ExportTable { get; set; }

		/// <summary>The import table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004DCA File Offset: 0x00002FCA
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004DD2 File Offset: 0x00002FD2
		public DirectoryEntry ImportTable { get; set; }

		/// <summary>The resource table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004DDB File Offset: 0x00002FDB
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public DirectoryEntry ResourceTable { get; set; }

		/// <summary>The exception table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004DEC File Offset: 0x00002FEC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004DF4 File Offset: 0x00002FF4
		public DirectoryEntry ExceptionTable { get; set; }

		/// <summary>The base relocation table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00004DFD File Offset: 0x00002FFD
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00004E05 File Offset: 0x00003005
		public DirectoryEntry BaseRelocationTable { get; set; }

		/// <summary>The debug table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004E0E File Offset: 0x0000300E
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00004E16 File Offset: 0x00003016
		public DirectoryEntry DebugTable { get; set; }

		/// <summary>The copyright/architecture image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004E1F File Offset: 0x0000301F
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004E27 File Offset: 0x00003027
		public DirectoryEntry CopyrightTable { get; set; }

		/// <summary>The global pointer table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004E30 File Offset: 0x00003030
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004E38 File Offset: 0x00003038
		public DirectoryEntry GlobalPointerTable { get; set; }

		/// <summary>The thread local storage (TLS) table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004E41 File Offset: 0x00003041
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00004E49 File Offset: 0x00003049
		public DirectoryEntry ThreadLocalStorageTable { get; set; }

		/// <summary>The load configuration table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004E52 File Offset: 0x00003052
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004E5A File Offset: 0x0000305A
		public DirectoryEntry LoadConfigTable { get; set; }

		/// <summary>The bound import image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004E63 File Offset: 0x00003063
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00004E6B File Offset: 0x0000306B
		public DirectoryEntry BoundImportTable { get; set; }

		/// <summary>The import address table (IAT) image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004E74 File Offset: 0x00003074
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00004E7C File Offset: 0x0000307C
		public DirectoryEntry ImportAddressTable { get; set; }

		/// <summary>The delay import table image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004E85 File Offset: 0x00003085
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00004E8D File Offset: 0x0000308D
		public DirectoryEntry DelayImportTable { get; set; }

		/// <summary>The COM descriptortable image directory entry.</summary>
		/// <returns>A directory entry instance.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004E96 File Offset: 0x00003096
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00004E9E File Offset: 0x0000309E
		public DirectoryEntry CorHeaderTable { get; set; }
	}
}
