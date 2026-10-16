using System;
using System.Reflection.PortableExecutable;

namespace Microsoft.Cci
{
	// Token: 0x02000077 RID: 119
	internal sealed class ModulePropertiesForSerialization
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000B739 File Offset: 0x00009939
		public DllCharacteristics DllCharacteristics { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000B741 File Offset: 0x00009941
		public Characteristics ImageCharacteristics { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000B749 File Offset: 0x00009949
		public Subsystem Subsystem { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000B751 File Offset: 0x00009951
		public CorFlags CorFlags { get; }

		// Token: 0x0600031D RID: 797 RVA: 0x0000B75C File Offset: 0x0000995C
		internal ModulePropertiesForSerialization(Guid persistentIdentifier, CorFlags corFlags, int fileAlignment, int sectionAlignment, string targetRuntimeVersion, Machine machine, ulong baseAddress, ulong sizeOfHeapReserve, ulong sizeOfHeapCommit, ulong sizeOfStackReserve, ulong sizeOfStackCommit, DllCharacteristics dllCharacteristics, Characteristics imageCharacteristics, Subsystem subsystem, ushort majorSubsystemVersion, ushort minorSubsystemVersion, byte linkerMajorVersion, byte linkerMinorVersion)
		{
			this.PersistentIdentifier = persistentIdentifier;
			this.FileAlignment = fileAlignment;
			this.SectionAlignment = sectionAlignment;
			this.TargetRuntimeVersion = targetRuntimeVersion;
			this.Machine = machine;
			this.BaseAddress = baseAddress;
			this.SizeOfHeapReserve = sizeOfHeapReserve;
			this.SizeOfHeapCommit = sizeOfHeapCommit;
			this.SizeOfStackReserve = sizeOfStackReserve;
			this.SizeOfStackCommit = sizeOfStackCommit;
			this.LinkerMajorVersion = linkerMajorVersion;
			this.LinkerMinorVersion = linkerMinorVersion;
			this.MajorSubsystemVersion = majorSubsystemVersion;
			this.MinorSubsystemVersion = minorSubsystemVersion;
			this.ImageCharacteristics = imageCharacteristics;
			this.Subsystem = subsystem;
			this.DllCharacteristics = dllCharacteristics;
			this.CorFlags = corFlags;
		}

		// Token: 0x040000DF RID: 223
		public readonly int FileAlignment;

		// Token: 0x040000E0 RID: 224
		public readonly int SectionAlignment;

		// Token: 0x040000E1 RID: 225
		public readonly string TargetRuntimeVersion;

		// Token: 0x040000E2 RID: 226
		public readonly Machine Machine;

		// Token: 0x040000E3 RID: 227
		public readonly Guid PersistentIdentifier;

		// Token: 0x040000E4 RID: 228
		public readonly ulong BaseAddress;

		// Token: 0x040000E5 RID: 229
		public readonly ulong SizeOfHeapReserve;

		// Token: 0x040000E6 RID: 230
		public readonly ulong SizeOfHeapCommit;

		// Token: 0x040000E7 RID: 231
		public readonly ulong SizeOfStackReserve;

		// Token: 0x040000E8 RID: 232
		public readonly ulong SizeOfStackCommit;

		// Token: 0x040000E9 RID: 233
		public readonly ushort MajorSubsystemVersion;

		// Token: 0x040000EA RID: 234
		public readonly ushort MinorSubsystemVersion;

		// Token: 0x040000EB RID: 235
		public readonly byte LinkerMajorVersion;

		// Token: 0x040000EC RID: 236
		public readonly byte LinkerMinorVersion;

		// Token: 0x040000F1 RID: 241
		public const ulong DefaultExeBaseAddress32Bit = 4194304UL;

		// Token: 0x040000F2 RID: 242
		public const ulong DefaultExeBaseAddress64Bit = 5368709120UL;

		// Token: 0x040000F3 RID: 243
		public const ulong DefaultDllBaseAddress32Bit = 268435456UL;

		// Token: 0x040000F4 RID: 244
		public const ulong DefaultDllBaseAddress64Bit = 6442450944UL;

		// Token: 0x040000F5 RID: 245
		public const ulong DefaultSizeOfHeapReserve32Bit = 1048576UL;

		// Token: 0x040000F6 RID: 246
		public const ulong DefaultSizeOfHeapReserve64Bit = 4194304UL;

		// Token: 0x040000F7 RID: 247
		public const ulong DefaultSizeOfHeapCommit32Bit = 4096UL;

		// Token: 0x040000F8 RID: 248
		public const ulong DefaultSizeOfHeapCommit64Bit = 8192UL;

		// Token: 0x040000F9 RID: 249
		public const ulong DefaultSizeOfStackReserve32Bit = 1048576UL;

		// Token: 0x040000FA RID: 250
		public const ulong DefaultSizeOfStackReserve64Bit = 4194304UL;

		// Token: 0x040000FB RID: 251
		public const ulong DefaultSizeOfStackCommit32Bit = 4096UL;

		// Token: 0x040000FC RID: 252
		public const ulong DefaultSizeOfStackCommit64Bit = 16384UL;

		// Token: 0x040000FD RID: 253
		public const ushort DefaultFileAlignment32Bit = 512;

		// Token: 0x040000FE RID: 254
		public const ushort DefaultFileAlignment64Bit = 512;

		// Token: 0x040000FF RID: 255
		public const ushort DefaultSectionAlignment = 8192;
	}
}
