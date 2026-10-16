using System;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000C9 RID: 201
	internal class SFVersionBuilder : StructureBuilder<SFVersion>
	{
		// Token: 0x0600045D RID: 1117 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public override SFVersion Read(BinaryReader br)
		{
			SFVersion sfversion = new SFVersion();
			sfversion.Major = br.ReadInt16();
			sfversion.Minor = br.ReadInt16();
			this.data.Add(sfversion);
			return sfversion;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000E934 File Offset: 0x0000CB34
		public override void Write(BinaryWriter bw, SFVersion v)
		{
			bw.Write(v.Major);
			bw.Write(v.Minor);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000E94E File Offset: 0x0000CB4E
		public override int Length
		{
			get
			{
				return 4;
			}
		}
	}
}
