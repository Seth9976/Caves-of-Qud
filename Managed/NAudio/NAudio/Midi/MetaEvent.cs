using System;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
	// Token: 0x020000D0 RID: 208
	public class MetaEvent : MidiEvent
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000F45F File Offset: 0x0000D65F
		public MetaEventType MetaEventType
		{
			get
			{
				return this.metaEvent;
			}
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000F467 File Offset: 0x0000D667
		protected MetaEvent()
		{
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000F46F File Offset: 0x0000D66F
		public MetaEvent(MetaEventType metaEventType, int metaDataLength, long absoluteTime)
			: base(absoluteTime, 1, MidiCommandCode.MetaEvent)
		{
			this.metaEvent = metaEventType;
			this.metaDataLength = metaDataLength;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000F48C File Offset: 0x0000D68C
		public static MetaEvent ReadMetaEvent(BinaryReader br)
		{
			MetaEventType metaEventType = (MetaEventType)br.ReadByte();
			int num = MidiEvent.ReadVarInt(br);
			MetaEvent metaEvent = new MetaEvent();
			MetaEventType metaEventType2 = metaEventType;
			if (metaEventType2 <= MetaEventType.SetTempo)
			{
				switch (metaEventType2)
				{
				case MetaEventType.TrackSequenceNumber:
					metaEvent = new TrackSequenceNumberEvent(br, num);
					goto IL_00E9;
				case MetaEventType.TextEvent:
				case MetaEventType.Copyright:
				case MetaEventType.SequenceTrackName:
				case MetaEventType.TrackInstrumentName:
				case MetaEventType.Lyric:
				case MetaEventType.Marker:
				case MetaEventType.CuePoint:
				case MetaEventType.ProgramName:
				case MetaEventType.DeviceName:
					metaEvent = new TextEvent(br, num);
					goto IL_00E9;
				default:
					if (metaEventType2 != MetaEventType.EndTrack)
					{
						if (metaEventType2 == MetaEventType.SetTempo)
						{
							metaEvent = new TempoEvent(br, num);
							goto IL_00E9;
						}
					}
					else
					{
						if (num != 0)
						{
							throw new FormatException("End track length");
						}
						goto IL_00E9;
					}
					break;
				}
			}
			else
			{
				if (metaEventType2 == MetaEventType.SmpteOffset)
				{
					metaEvent = new SmpteOffsetEvent(br, num);
					goto IL_00E9;
				}
				switch (metaEventType2)
				{
				case MetaEventType.TimeSignature:
					metaEvent = new TimeSignatureEvent(br, num);
					goto IL_00E9;
				case MetaEventType.KeySignature:
					metaEvent = new KeySignatureEvent(br, num);
					goto IL_00E9;
				default:
					if (metaEventType2 == MetaEventType.SequencerSpecific)
					{
						metaEvent = new SequencerSpecificEvent(br, num);
						goto IL_00E9;
					}
					break;
				}
			}
			metaEvent.data = br.ReadBytes(num);
			if (metaEvent.data.Length != num)
			{
				throw new FormatException("Failed to read metaevent's data fully");
			}
			IL_00E9:
			metaEvent.metaEvent = metaEventType;
			metaEvent.metaDataLength = num;
			return metaEvent;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000F594 File Offset: 0x0000D794
		public override string ToString()
		{
			if (this.data == null)
			{
				return string.Format("{0} {1}", base.AbsoluteTime, this.metaEvent);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:X2} ", b);
			}
			return string.Format("{0} {1}\r\n{2}", base.AbsoluteTime, this.metaEvent, stringBuilder.ToString());
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000F621 File Offset: 0x0000D821
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)this.metaEvent);
			MidiEvent.WriteVarInt(writer, this.metaDataLength);
			if (this.data != null)
			{
				writer.Write(this.data, 0, this.data.Length);
			}
		}

		// Token: 0x04000544 RID: 1348
		private MetaEventType metaEvent;

		// Token: 0x04000545 RID: 1349
		internal int metaDataLength;

		// Token: 0x04000546 RID: 1350
		private byte[] data;
	}
}
