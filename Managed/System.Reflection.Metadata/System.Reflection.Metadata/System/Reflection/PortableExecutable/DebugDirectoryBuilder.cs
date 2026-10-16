using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DebugDirectoryBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.PortableExecutable.DebugDirectoryBuilder" /> class.</summary>
		// Token: 0x06000124 RID: 292 RVA: 0x000048C0 File Offset: 0x00002AC0
		public DebugDirectoryBuilder()
		{
			this._entries = new List<DebugDirectoryBuilder.Entry>(3);
			this._dataBuilder = new BlobBuilder(256);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000048E4 File Offset: 0x00002AE4
		internal void AddEntry(DebugDirectoryEntryType type, uint version, uint stamp, int dataSize)
		{
			this._entries.Add(new DebugDirectoryBuilder.Entry
			{
				Stamp = stamp,
				Version = version,
				Type = type,
				DataSize = dataSize
			});
		}

		/// <summary>Adds an entry of the specified type.</summary>
		/// <param name="type">The entry type.</param>
		/// <param name="version">The entry version.</param>
		/// <param name="stamp">The entry stamp.</param>
		// Token: 0x06000126 RID: 294 RVA: 0x00004926 File Offset: 0x00002B26
		public void AddEntry(DebugDirectoryEntryType type, uint version, uint stamp)
		{
			this.AddEntry(type, version, stamp, 0);
		}

		/// <summary>Adds an entry of the specified type and serializes its data.</summary>
		/// <param name="type">The entry type.</param>
		/// <param name="version">The entry version.</param>
		/// <param name="stamp">The entry stamp.</param>
		/// <param name="data">The data to pass to <paramref name="dataSerializer" />.</param>
		/// <param name="dataSerializer">A serializer for serializing data to a <see cref="T:System.Reflection.Metadata.BlobBuilder" />.</param>
		/// <typeparam name="TData">The type of the data passed to <paramref name="dataSerializer" />.</typeparam>
		// Token: 0x06000127 RID: 295 RVA: 0x00004934 File Offset: 0x00002B34
		public void AddEntry<[Nullable(2)] TData>(DebugDirectoryEntryType type, uint version, uint stamp, TData data, Action<BlobBuilder, TData> dataSerializer)
		{
			if (dataSerializer == null)
			{
				Throw.ArgumentNull("dataSerializer");
			}
			int count = this._dataBuilder.Count;
			dataSerializer.Invoke(this._dataBuilder, data);
			int num = this._dataBuilder.Count - count;
			this.AddEntry(type, version, stamp, num);
		}

		/// <summary>Adds a CodeView entry.</summary>
		/// <param name="pdbPath">The path to the PDB. It should not be empty.</param>
		/// <param name="pdbContentId">The unique id of the PDB content.</param>
		/// <param name="portablePdbVersion">The version of Portable PDB format (e.g. 0x0100 for 1.0), or 0 if the PDB is not portable.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pdbPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pdbPath" /> contains a NUL character.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="portablePdbVersion" /> is smaller than 0x0100.</exception>
		// Token: 0x06000128 RID: 296 RVA: 0x00004982 File Offset: 0x00002B82
		public void AddCodeViewEntry(string pdbPath, BlobContentId pdbContentId, ushort portablePdbVersion)
		{
			this.AddCodeViewEntry(pdbPath, pdbContentId, portablePdbVersion, 1);
		}

		/// <summary>Adds a CodeView entry.</summary>
		/// <param name="pdbPath">The path to the PDB. It should not be empty.</param>
		/// <param name="pdbContentId">The unique id of the PDB content.</param>
		/// <param name="portablePdbVersion">The version of Portable PDB format (e.g. 0x0100 for 1.0), or 0 if the PDB is not portable.</param>
		/// <param name="age">Age (iteration) of the PDB. Shall be 1 for Portable PDBs.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pdbPath" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pdbPath" /> contains a NUL character.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///     Either <paramref name="portablePdbVersion" /> is smaller than 0x0100 or <paramref name="age" /> is less than 1.</exception>
		// Token: 0x06000129 RID: 297 RVA: 0x00004990 File Offset: 0x00002B90
		internal void AddCodeViewEntry(string pdbPath, BlobContentId pdbContentId, ushort portablePdbVersion, int age)
		{
			if (pdbPath == null)
			{
				Throw.ArgumentNull("pdbPath");
			}
			if (age < 1)
			{
				Throw.ArgumentOutOfRange("age");
			}
			if (pdbPath.Length == 0 || pdbPath.IndexOf('\0') == 0)
			{
				Throw.InvalidArgument(SR.ExpectedNonEmptyString, "pdbPath");
			}
			if (portablePdbVersion > 0 && portablePdbVersion < 256)
			{
				Throw.ArgumentOutOfRange("portablePdbVersion");
			}
			int num = DebugDirectoryBuilder.WriteCodeViewData(this._dataBuilder, pdbPath, pdbContentId.Guid, age);
			this.AddEntry(DebugDirectoryEntryType.CodeView, (portablePdbVersion == 0) ? 0U : PortablePdbVersions.DebugDirectoryEntryVersion(portablePdbVersion), pdbContentId.Stamp, num);
		}

		/// <summary>Adds a reproducible entry.</summary>
		// Token: 0x0600012A RID: 298 RVA: 0x00004A20 File Offset: 0x00002C20
		public void AddReproducibleEntry()
		{
			this.AddEntry(DebugDirectoryEntryType.Reproducible, 0U, 0U);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004A2C File Offset: 0x00002C2C
		private static int WriteCodeViewData(BlobBuilder builder, string pdbPath, Guid pdbGuid, int age)
		{
			int count = builder.Count;
			builder.WriteByte(82);
			builder.WriteByte(83);
			builder.WriteByte(68);
			builder.WriteByte(83);
			builder.WriteGuid(pdbGuid);
			builder.WriteInt32(age);
			builder.WriteUTF8(pdbPath, true);
			builder.WriteByte(0);
			return builder.Count - count;
		}

		/// <summary>Adds PDB checksum entry.</summary>
		/// <param name="algorithmName">The hash algorithm name (for example, "SHA256").</param>
		/// <param name="checksum">The checksum.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="algorithmName" /> or <paramref name="checksum" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="algorithmName" /> or <paramref name="checksum" /> is empty.</exception>
		// Token: 0x0600012C RID: 300 RVA: 0x00004A88 File Offset: 0x00002C88
		[NullableContext(0)]
		public void AddPdbChecksumEntry([Nullable(1)] string algorithmName, ImmutableArray<byte> checksum)
		{
			if (algorithmName == null)
			{
				Throw.ArgumentNull("algorithmName");
			}
			if (algorithmName.Length == 0)
			{
				Throw.ArgumentEmptyString("algorithmName");
			}
			if (checksum.IsDefault)
			{
				Throw.ArgumentNull("checksum");
			}
			if (checksum.Length == 0)
			{
				Throw.ArgumentEmptyArray("checksum");
			}
			int num = DebugDirectoryBuilder.WritePdbChecksumData(this._dataBuilder, algorithmName, checksum);
			this.AddEntry(DebugDirectoryEntryType.PdbChecksum, 1U, 0U, num);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004AF4 File Offset: 0x00002CF4
		private static int WritePdbChecksumData(BlobBuilder builder, string algorithmName, ImmutableArray<byte> checksum)
		{
			int count = builder.Count;
			builder.WriteUTF8(algorithmName, true);
			builder.WriteByte(0);
			builder.WriteBytes(checksum);
			return builder.Count - count;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004B26 File Offset: 0x00002D26
		internal int TableSize
		{
			get
			{
				return 28 * this._entries.Count;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004B38 File Offset: 0x00002D38
		internal int Size
		{
			get
			{
				int tableSize = this.TableSize;
				BlobBuilder dataBuilder = this._dataBuilder;
				return (tableSize + ((dataBuilder != null) ? new int?(dataBuilder.Count) : default(int?))).GetValueOrDefault();
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004B98 File Offset: 0x00002D98
		internal void Serialize(BlobBuilder builder, SectionLocation sectionLocation, int sectionOffset)
		{
			int num = sectionOffset + this.TableSize;
			foreach (DebugDirectoryBuilder.Entry entry in this._entries)
			{
				int num2;
				int num3;
				if (entry.DataSize > 0)
				{
					num2 = sectionLocation.RelativeVirtualAddress + num;
					num3 = sectionLocation.PointerToRawData + num;
				}
				else
				{
					num2 = 0;
					num3 = 0;
				}
				builder.WriteUInt32(0U);
				builder.WriteUInt32(entry.Stamp);
				builder.WriteUInt32(entry.Version);
				builder.WriteInt32((int)entry.Type);
				builder.WriteInt32(entry.DataSize);
				builder.WriteInt32(num2);
				builder.WriteInt32(num3);
				num += entry.DataSize;
			}
			builder.LinkSuffix(this._dataBuilder);
		}

		/// <summary>Adds an Embedded Portable PDB entry.</summary>
		/// <param name="debugMetadata">A Portable PDB metadata builder.</param>
		/// <param name="portablePdbVersion">The version of Portable PDB format (e.g. 0x0100 for 1.0).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="debugMetadata" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="portablePdbVersion" /> is smaller than 0x0100.</exception>
		// Token: 0x06000131 RID: 305 RVA: 0x00004C70 File Offset: 0x00002E70
		public void AddEmbeddedPortablePdbEntry(BlobBuilder debugMetadata, ushort portablePdbVersion)
		{
			if (debugMetadata == null)
			{
				Throw.ArgumentNull("debugMetadata");
			}
			if (portablePdbVersion < 256)
			{
				Throw.ArgumentOutOfRange("portablePdbVersion");
			}
			int num = DebugDirectoryBuilder.WriteEmbeddedPortablePdbData(this._dataBuilder, debugMetadata);
			this.AddEntry(DebugDirectoryEntryType.EmbeddedPortablePdb, PortablePdbVersions.DebugDirectoryEmbeddedVersion(portablePdbVersion), 0U, num);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004CBC File Offset: 0x00002EBC
		private static int WriteEmbeddedPortablePdbData(BlobBuilder builder, BlobBuilder debugMetadata)
		{
			int count = builder.Count;
			builder.WriteUInt32(1111773261U);
			builder.WriteInt32(debugMetadata.Count);
			MemoryStream memoryStream = new MemoryStream();
			using (DeflateStream deflateStream = new DeflateStream(memoryStream, 0, true))
			{
				foreach (Blob blob in debugMetadata.GetBlobs())
				{
					ArraySegment<byte> bytes = blob.GetBytes();
					deflateStream.Write(bytes.Array, bytes.Offset, bytes.Count);
				}
			}
			builder.WriteBytes(memoryStream.ToArray());
			return builder.Count - count;
		}

		// Token: 0x0400006E RID: 110
		private readonly List<DebugDirectoryBuilder.Entry> _entries;

		// Token: 0x0400006F RID: 111
		private readonly BlobBuilder _dataBuilder;

		// Token: 0x02000191 RID: 401
		private struct Entry
		{
			// Token: 0x040009C2 RID: 2498
			public uint Stamp;

			// Token: 0x040009C3 RID: 2499
			public uint Version;

			// Token: 0x040009C4 RID: 2500
			public DebugDirectoryEntryType Type;

			// Token: 0x040009C5 RID: 2501
			public int DataSize;
		}
	}
}
