using System;
using System.IO;
using System.Text;

namespace NAudio.SoundFont
{
	// Token: 0x020000B8 RID: 184
	internal class InstrumentBuilder : StructureBuilder<Instrument>
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public override Instrument Read(BinaryReader br)
		{
			Instrument instrument = new Instrument();
			string text = Encoding.UTF8.GetString(br.ReadBytes(20), 0, 20);
			if (text.IndexOf('\0') >= 0)
			{
				text = text.Substring(0, text.IndexOf('\0'));
			}
			instrument.Name = text;
			instrument.startInstrumentZoneIndex = br.ReadUInt16();
			if (this.lastInstrument != null)
			{
				this.lastInstrument.endInstrumentZoneIndex = instrument.startInstrumentZoneIndex - 1;
			}
			this.data.Add(instrument);
			this.lastInstrument = instrument;
			return instrument;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000DC19 File Offset: 0x0000BE19
		public override void Write(BinaryWriter bw, Instrument instrument)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000DC1B File Offset: 0x0000BE1B
		public override int Length
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public void LoadZones(Zone[] zones)
		{
			for (int i = 0; i < this.data.Count - 1; i++)
			{
				Instrument instrument = this.data[i];
				instrument.Zones = new Zone[(int)(instrument.endInstrumentZoneIndex - instrument.startInstrumentZoneIndex + 1)];
				Array.Copy(zones, (int)instrument.startInstrumentZoneIndex, instrument.Zones, 0, instrument.Zones.Length);
			}
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000DC9F File Offset: 0x0000BE9F
		public Instrument[] Instruments
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x040004E6 RID: 1254
		private Instrument lastInstrument;
	}
}
