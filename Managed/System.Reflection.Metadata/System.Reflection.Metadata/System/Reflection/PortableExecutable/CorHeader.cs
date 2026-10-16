using System;

namespace System.Reflection.PortableExecutable
{
	/// <summary>Represents the Common Language Infrastructure (CLI) header of the .NET executable image.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class CorHeader
	{
		/// <summary>Gets the major part of the runtime version required to load this image.</summary>
		/// <returns>The major part of the runtime version</returns>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000051DB File Offset: 0x000033DB
		public ushort MajorRuntimeVersion { get; }

		/// <summary>Gets the minor part of the runtime version required to load this image.</summary>
		/// <returns>The minor part of the runtime version.</returns>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000051E3 File Offset: 0x000033E3
		public ushort MinorRuntimeVersion { get; }

		/// <summary>Gets the Metadata entry.</summary>
		/// <returns>The Metadata entry.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000051EB File Offset: 0x000033EB
		public DirectoryEntry MetadataDirectory { get; }

		/// <summary>Gets the runtime flags for this image.</summary>
		/// <returns>A bitwise combination of runtime flags.</returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000051F3 File Offset: 0x000033F3
		public CorFlags Flags { get; }

		/// <summary>Gets the metadata token of the entry point method of this image.</summary>
		/// <returns>The metadata token of the entry point method.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000051FB File Offset: 0x000033FB
		public int EntryPointTokenOrRelativeVirtualAddress { get; }

		/// <summary>Gets the Resources entry.</summary>
		/// <returns>The Resources entry.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005203 File Offset: 0x00003403
		public DirectoryEntry ResourcesDirectory { get; }

		/// <summary>Gets the Strong Name Signature entry.</summary>
		/// <returns>The Strong Name Signature entry.</returns>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000520B File Offset: 0x0000340B
		public DirectoryEntry StrongNameSignatureDirectory { get; }

		/// <summary>Gets the Code Manager Table entry.</summary>
		/// <returns>The Code Manager Table entry.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005213 File Offset: 0x00003413
		public DirectoryEntry CodeManagerTableDirectory { get; }

		/// <summary>Gets the VTable Fixups entry.</summary>
		/// <returns>The VTable Fixups entry.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000521B File Offset: 0x0000341B
		public DirectoryEntry VtableFixupsDirectory { get; }

		/// <summary>Gets the Export Address Table Jumps entry.</summary>
		/// <returns>The Export Address Table Jumps entry.</returns>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005223 File Offset: 0x00003423
		public DirectoryEntry ExportAddressTableJumpsDirectory { get; }

		/// <summary>Gets the Managed Native Header entry.</summary>
		/// <returns>The Managed Native Header entry.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000522B File Offset: 0x0000342B
		public DirectoryEntry ManagedNativeHeaderDirectory { get; }

		// Token: 0x06000185 RID: 389 RVA: 0x00005234 File Offset: 0x00003434
		internal CorHeader(ref PEBinaryReader reader)
		{
			reader.ReadInt32();
			this.MajorRuntimeVersion = reader.ReadUInt16();
			this.MinorRuntimeVersion = reader.ReadUInt16();
			this.MetadataDirectory = new DirectoryEntry(ref reader);
			this.Flags = (CorFlags)reader.ReadUInt32();
			this.EntryPointTokenOrRelativeVirtualAddress = reader.ReadInt32();
			this.ResourcesDirectory = new DirectoryEntry(ref reader);
			this.StrongNameSignatureDirectory = new DirectoryEntry(ref reader);
			this.CodeManagerTableDirectory = new DirectoryEntry(ref reader);
			this.VtableFixupsDirectory = new DirectoryEntry(ref reader);
			this.ExportAddressTableJumpsDirectory = new DirectoryEntry(ref reader);
			this.ManagedNativeHeaderDirectory = new DirectoryEntry(ref reader);
		}
	}
}
