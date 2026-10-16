using System;
using System.IO;
using NAudio.Utils;

namespace NAudio.SoundFont
{
	// Token: 0x020000C5 RID: 197
	internal class SampleHeaderBuilder : StructureBuilder<SampleHeader>
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		public override SampleHeader Read(BinaryReader br)
		{
			SampleHeader sampleHeader = new SampleHeader();
			byte[] array = br.ReadBytes(20);
			sampleHeader.SampleName = ByteEncoding.Instance.GetString(array, 0, array.Length);
			sampleHeader.Start = br.ReadUInt32();
			sampleHeader.End = br.ReadUInt32();
			sampleHeader.StartLoop = br.ReadUInt32();
			sampleHeader.EndLoop = br.ReadUInt32();
			sampleHeader.SampleRate = br.ReadUInt32();
			sampleHeader.OriginalPitch = br.ReadByte();
			sampleHeader.PitchCorrection = br.ReadSByte();
			sampleHeader.SampleLink = br.ReadUInt16();
			sampleHeader.SFSampleLink = (SFSampleLink)br.ReadUInt16();
			this.data.Add(sampleHeader);
			return sampleHeader;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E89A File Offset: 0x0000CA9A
		public override void Write(BinaryWriter bw, SampleHeader sampleHeader)
		{
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public override int Length
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		internal void RemoveEOS()
		{
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000E8BA File Offset: 0x0000CABA
		public SampleHeader[] SampleHeaders
		{
			get
			{
				return this.data.ToArray();
			}
		}
	}
}
