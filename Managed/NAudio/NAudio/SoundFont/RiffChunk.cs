using System;
using System.IO;
using NAudio.Utils;

namespace NAudio.SoundFont
{
	// Token: 0x020000C2 RID: 194
	internal class RiffChunk
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		public static RiffChunk GetTopLevelChunk(BinaryReader file)
		{
			RiffChunk riffChunk = new RiffChunk(file);
			riffChunk.ReadChunk();
			return riffChunk;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000E4C7 File Offset: 0x0000C6C7
		private RiffChunk(BinaryReader file)
		{
			this.riffFile = file;
			this.chunkID = "????";
			this.chunkSize = 0U;
			this.dataOffset = 0L;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		public string ReadChunkID()
		{
			byte[] array = this.riffFile.ReadBytes(4);
			if (array.Length != 4)
			{
				throw new InvalidDataException("Couldn't read Chunk ID");
			}
			return ByteEncoding.Instance.GetString(array, 0, array.Length);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000E52A File Offset: 0x0000C72A
		private void ReadChunk()
		{
			this.chunkID = this.ReadChunkID();
			this.chunkSize = this.riffFile.ReadUInt32();
			this.dataOffset = this.riffFile.BaseStream.Position;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000E560 File Offset: 0x0000C760
		public RiffChunk GetNextSubChunk()
		{
			if (this.riffFile.BaseStream.Position + 8L < this.dataOffset + (long)((ulong)this.chunkSize))
			{
				RiffChunk riffChunk = new RiffChunk(this.riffFile);
				riffChunk.ReadChunk();
				return riffChunk;
			}
			return null;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		public byte[] GetData()
		{
			this.riffFile.BaseStream.Position = this.dataOffset;
			byte[] array = this.riffFile.ReadBytes((int)this.chunkSize);
			if ((long)array.Length != (long)((ulong)this.chunkSize))
			{
				throw new InvalidDataException(string.Format("Couldn't read chunk's data Chunk: {0}, read {1} bytes", this, array.Length));
			}
			return array;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000E604 File Offset: 0x0000C804
		public string GetDataAsString()
		{
			byte[] data = this.GetData();
			if (data == null)
			{
				return null;
			}
			return ByteEncoding.Instance.GetString(data, 0, data.Length);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000E62C File Offset: 0x0000C82C
		public T GetDataAsStructure<T>(StructureBuilder<T> s)
		{
			this.riffFile.BaseStream.Position = this.dataOffset;
			if ((long)s.Length != (long)((ulong)this.chunkSize))
			{
				throw new InvalidDataException(string.Format("Chunk size is: {0} so can't read structure of: {1}", this.chunkSize, s.Length));
			}
			return s.Read(this.riffFile);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000E694 File Offset: 0x0000C894
		public T[] GetDataAsStructureArray<T>(StructureBuilder<T> s)
		{
			this.riffFile.BaseStream.Position = this.dataOffset;
			if ((ulong)this.chunkSize % (ulong)((long)s.Length) != 0UL)
			{
				throw new InvalidDataException(string.Format("Chunk size is: {0} not a multiple of structure size: {1}", this.chunkSize, s.Length));
			}
			int num = (int)((ulong)this.chunkSize / (ulong)((long)s.Length));
			T[] array = new T[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = s.Read(this.riffFile);
			}
			return array;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000E728 File Offset: 0x0000C928
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000E730 File Offset: 0x0000C930
		public string ChunkID
		{
			get
			{
				return this.chunkID;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ChunkID may not be null");
				}
				if (value.Length != 4)
				{
					throw new ArgumentException("ChunkID must be four characters");
				}
				this.chunkID = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000E75B File Offset: 0x0000C95B
		public uint ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000E763 File Offset: 0x0000C963
		public long DataOffset
		{
			get
			{
				return this.dataOffset;
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000E76B File Offset: 0x0000C96B
		public override string ToString()
		{
			return string.Format("RiffChunk ID: {0} Size: {1} Data Offset: {2}", this.ChunkID, this.ChunkSize, this.DataOffset);
		}

		// Token: 0x04000514 RID: 1300
		private string chunkID;

		// Token: 0x04000515 RID: 1301
		private uint chunkSize;

		// Token: 0x04000516 RID: 1302
		private long dataOffset;

		// Token: 0x04000517 RID: 1303
		private BinaryReader riffFile;
	}
}
