using System;
using System.IO;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000015 RID: 21
	public class OffsetTable
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004CF4 File Offset: 0x00002EF4
		internal OffsetTable()
		{
			int platform = Environment.OSVersion.Platform;
			if (platform != 4 && platform != 128)
			{
				this.FileFlags |= OffsetTable.Flags.WindowsFileNames;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004D44 File Offset: 0x00002F44
		internal OffsetTable(BinaryReader reader, int major_version, int minor_version)
		{
			this.TotalFileSize = reader.ReadInt32();
			this.DataSectionOffset = reader.ReadInt32();
			this.DataSectionSize = reader.ReadInt32();
			this.CompileUnitCount = reader.ReadInt32();
			this.CompileUnitTableOffset = reader.ReadInt32();
			this.CompileUnitTableSize = reader.ReadInt32();
			this.SourceCount = reader.ReadInt32();
			this.SourceTableOffset = reader.ReadInt32();
			this.SourceTableSize = reader.ReadInt32();
			this.MethodCount = reader.ReadInt32();
			this.MethodTableOffset = reader.ReadInt32();
			this.MethodTableSize = reader.ReadInt32();
			this.TypeCount = reader.ReadInt32();
			this.AnonymousScopeCount = reader.ReadInt32();
			this.AnonymousScopeTableOffset = reader.ReadInt32();
			this.AnonymousScopeTableSize = reader.ReadInt32();
			this.LineNumberTable_LineBase = reader.ReadInt32();
			this.LineNumberTable_LineRange = reader.ReadInt32();
			this.LineNumberTable_OpcodeBase = reader.ReadInt32();
			this.FileFlags = (OffsetTable.Flags)reader.ReadInt32();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004E60 File Offset: 0x00003060
		internal void Write(BinaryWriter bw, int major_version, int minor_version)
		{
			bw.Write(this.TotalFileSize);
			bw.Write(this.DataSectionOffset);
			bw.Write(this.DataSectionSize);
			bw.Write(this.CompileUnitCount);
			bw.Write(this.CompileUnitTableOffset);
			bw.Write(this.CompileUnitTableSize);
			bw.Write(this.SourceCount);
			bw.Write(this.SourceTableOffset);
			bw.Write(this.SourceTableSize);
			bw.Write(this.MethodCount);
			bw.Write(this.MethodTableOffset);
			bw.Write(this.MethodTableSize);
			bw.Write(this.TypeCount);
			bw.Write(this.AnonymousScopeCount);
			bw.Write(this.AnonymousScopeTableOffset);
			bw.Write(this.AnonymousScopeTableSize);
			bw.Write(this.LineNumberTable_LineBase);
			bw.Write(this.LineNumberTable_LineRange);
			bw.Write(this.LineNumberTable_OpcodeBase);
			bw.Write((int)this.FileFlags);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004F60 File Offset: 0x00003160
		public override string ToString()
		{
			return string.Format("OffsetTable [{0} - {1}:{2} - {3}:{4}:{5} - {6}:{7}:{8} - {9}]", new object[] { this.TotalFileSize, this.DataSectionOffset, this.DataSectionSize, this.SourceCount, this.SourceTableOffset, this.SourceTableSize, this.MethodCount, this.MethodTableOffset, this.MethodTableSize, this.TypeCount });
		}

		// Token: 0x04000069 RID: 105
		public const int MajorVersion = 50;

		// Token: 0x0400006A RID: 106
		public const int MinorVersion = 0;

		// Token: 0x0400006B RID: 107
		public const long Magic = 5037318119232611860L;

		// Token: 0x0400006C RID: 108
		public int TotalFileSize;

		// Token: 0x0400006D RID: 109
		public int DataSectionOffset;

		// Token: 0x0400006E RID: 110
		public int DataSectionSize;

		// Token: 0x0400006F RID: 111
		public int CompileUnitCount;

		// Token: 0x04000070 RID: 112
		public int CompileUnitTableOffset;

		// Token: 0x04000071 RID: 113
		public int CompileUnitTableSize;

		// Token: 0x04000072 RID: 114
		public int SourceCount;

		// Token: 0x04000073 RID: 115
		public int SourceTableOffset;

		// Token: 0x04000074 RID: 116
		public int SourceTableSize;

		// Token: 0x04000075 RID: 117
		public int MethodCount;

		// Token: 0x04000076 RID: 118
		public int MethodTableOffset;

		// Token: 0x04000077 RID: 119
		public int MethodTableSize;

		// Token: 0x04000078 RID: 120
		public int TypeCount;

		// Token: 0x04000079 RID: 121
		public int AnonymousScopeCount;

		// Token: 0x0400007A RID: 122
		public int AnonymousScopeTableOffset;

		// Token: 0x0400007B RID: 123
		public int AnonymousScopeTableSize;

		// Token: 0x0400007C RID: 124
		public OffsetTable.Flags FileFlags;

		// Token: 0x0400007D RID: 125
		public int LineNumberTable_LineBase = -1;

		// Token: 0x0400007E RID: 126
		public int LineNumberTable_LineRange = 8;

		// Token: 0x0400007F RID: 127
		public int LineNumberTable_OpcodeBase = 9;

		// Token: 0x02000025 RID: 37
		[Flags]
		public enum Flags
		{
			// Token: 0x040000B3 RID: 179
			IsAspxSource = 1,
			// Token: 0x040000B4 RID: 180
			WindowsFileNames = 2
		}
	}
}
