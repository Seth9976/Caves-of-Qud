using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NAudio.FileFormats.Map
{
	// Token: 0x020000A2 RID: 162
	public class CakewalkMapFile
	{
		// Token: 0x06000387 RID: 903 RVA: 0x0000BF6C File Offset: 0x0000A16C
		public CakewalkMapFile(string filename)
		{
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(filename), Encoding.Unicode))
			{
				this.drumMappings = new List<CakewalkDrumMapping>();
				this.ReadMapHeader(binaryReader);
				for (int i = 0; i < this.mapEntryCount; i++)
				{
					this.drumMappings.Add(this.ReadMapEntry(binaryReader));
				}
				this.ReadMapName(binaryReader);
				this.ReadOutputsSection1(binaryReader);
				if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
				{
					this.ReadOutputsSection2(binaryReader);
					if (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
					{
						this.ReadOutputsSection3(binaryReader);
					}
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000C034 File Offset: 0x0000A234
		public List<CakewalkDrumMapping> DrumMappings
		{
			get
			{
				return this.drumMappings;
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000C03C File Offset: 0x0000A23C
		private void ReadMapHeader(BinaryReader reader)
		{
			this.fileHeader1 = MapBlockHeader.Read(reader);
			this.fileHeader2 = MapBlockHeader.Read(reader);
			this.mapEntryCount = reader.ReadInt32();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C064 File Offset: 0x0000A264
		private CakewalkDrumMapping ReadMapEntry(BinaryReader reader)
		{
			CakewalkDrumMapping cakewalkDrumMapping = new CakewalkDrumMapping();
			reader.ReadInt32();
			cakewalkDrumMapping.InNote = reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			reader.ReadInt32();
			cakewalkDrumMapping.VelocityScale = reader.ReadSingle();
			cakewalkDrumMapping.Channel = reader.ReadInt32();
			cakewalkDrumMapping.OutNote = reader.ReadInt32();
			cakewalkDrumMapping.OutPort = reader.ReadInt32();
			cakewalkDrumMapping.VelocityAdjust = reader.ReadInt32();
			char[] array = reader.ReadChars(32);
			int num = 0;
			while (num < array.Length && array[num] != '\0')
			{
				num++;
			}
			cakewalkDrumMapping.NoteName = new string(array, 0, num);
			return cakewalkDrumMapping;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000C11C File Offset: 0x0000A31C
		private void ReadMapName(BinaryReader reader)
		{
			this.mapNameHeader = MapBlockHeader.Read(reader);
			char[] array = reader.ReadChars(34);
			int num = 0;
			while (num < array.Length && array[num] != '\0')
			{
				num++;
			}
			this.mapName = new string(array, 0, num);
			reader.ReadBytes(98);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000C168 File Offset: 0x0000A368
		private void ReadOutputsSection1(BinaryReader reader)
		{
			this.outputs1Header = MapBlockHeader.Read(reader);
			this.outputs1Count = reader.ReadInt32();
			for (int i = 0; i < this.outputs1Count; i++)
			{
				reader.ReadBytes(20);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000C1A8 File Offset: 0x0000A3A8
		private void ReadOutputsSection2(BinaryReader reader)
		{
			this.outputs2Header = MapBlockHeader.Read(reader);
			this.outputs2Count = reader.ReadInt32();
			for (int i = 0; i < this.outputs2Count; i++)
			{
				reader.ReadBytes(24);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		private void ReadOutputsSection3(BinaryReader reader)
		{
			this.outputs3Header = MapBlockHeader.Read(reader);
			if (this.outputs3Header.Length > 0)
			{
				this.outputs3Count = reader.ReadInt32();
				for (int i = 0; i < this.outputs3Count; i++)
				{
					reader.ReadBytes(36);
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000C238 File Offset: 0x0000A438
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("FileHeader1: {0}\r\n", this.fileHeader1);
			stringBuilder.AppendFormat("FileHeader2: {0}\r\n", this.fileHeader2);
			stringBuilder.AppendFormat("MapEntryCount: {0}\r\n", this.mapEntryCount);
			foreach (CakewalkDrumMapping cakewalkDrumMapping in this.drumMappings)
			{
				stringBuilder.AppendFormat("   Map: {0}\r\n", cakewalkDrumMapping);
			}
			stringBuilder.AppendFormat("MapNameHeader: {0}\r\n", this.mapNameHeader);
			stringBuilder.AppendFormat("MapName: {0}\r\n", this.mapName);
			stringBuilder.AppendFormat("Outputs1Header: {0} Count: {1}\r\n", this.outputs1Header, this.outputs1Count);
			stringBuilder.AppendFormat("Outputs2Header: {0} Count: {1}\r\n", this.outputs2Header, this.outputs2Count);
			stringBuilder.AppendFormat("Outputs3Header: {0} Count: {1}\r\n", this.outputs3Header, this.outputs3Count);
			return stringBuilder.ToString();
		}

		// Token: 0x04000447 RID: 1095
		private int mapEntryCount;

		// Token: 0x04000448 RID: 1096
		private readonly List<CakewalkDrumMapping> drumMappings;

		// Token: 0x04000449 RID: 1097
		private MapBlockHeader fileHeader1;

		// Token: 0x0400044A RID: 1098
		private MapBlockHeader fileHeader2;

		// Token: 0x0400044B RID: 1099
		private MapBlockHeader mapNameHeader;

		// Token: 0x0400044C RID: 1100
		private MapBlockHeader outputs1Header;

		// Token: 0x0400044D RID: 1101
		private MapBlockHeader outputs2Header;

		// Token: 0x0400044E RID: 1102
		private MapBlockHeader outputs3Header;

		// Token: 0x0400044F RID: 1103
		private int outputs1Count;

		// Token: 0x04000450 RID: 1104
		private int outputs2Count;

		// Token: 0x04000451 RID: 1105
		private int outputs3Count;

		// Token: 0x04000452 RID: 1106
		private string mapName;
	}
}
