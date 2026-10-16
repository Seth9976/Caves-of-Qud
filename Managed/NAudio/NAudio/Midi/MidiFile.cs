using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Midi
{
	// Token: 0x020000D7 RID: 215
	public class MidiFile
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		public MidiFile(string filename)
			: this(filename, true)
		{
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000FBED File Offset: 0x0000DDED
		public int FileFormat
		{
			get
			{
				return (int)this.fileFormat;
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		public MidiFile(string filename, bool strictChecking)
		{
			this.strictChecking = strictChecking;
			BinaryReader binaryReader = new BinaryReader(File.OpenRead(filename));
			using (binaryReader)
			{
				string text = Encoding.UTF8.GetString(binaryReader.ReadBytes(4));
				if (text != "MThd")
				{
					throw new FormatException("Not a MIDI file - header chunk missing");
				}
				uint num = MidiFile.SwapUInt32(binaryReader.ReadUInt32());
				if (num != 6U)
				{
					throw new FormatException("Unexpected header chunk length");
				}
				this.fileFormat = MidiFile.SwapUInt16(binaryReader.ReadUInt16());
				int num2 = (int)MidiFile.SwapUInt16(binaryReader.ReadUInt16());
				this.deltaTicksPerQuarterNote = MidiFile.SwapUInt16(binaryReader.ReadUInt16());
				this.events = new MidiEventCollection((this.fileFormat == 0) ? 0 : 1, (int)this.deltaTicksPerQuarterNote);
				for (int i = 0; i < num2; i++)
				{
					this.events.AddTrack();
				}
				long num3 = 0L;
				for (int j = 0; j < num2; j++)
				{
					if (this.fileFormat == 1)
					{
						num3 = 0L;
					}
					text = Encoding.UTF8.GetString(binaryReader.ReadBytes(4));
					if (text != "MTrk")
					{
						throw new FormatException("Invalid chunk header");
					}
					num = MidiFile.SwapUInt32(binaryReader.ReadUInt32());
					long position = binaryReader.BaseStream.Position;
					MidiEvent midiEvent = null;
					List<NoteOnEvent> list = new List<NoteOnEvent>();
					while (binaryReader.BaseStream.Position < position + (long)((ulong)num))
					{
						midiEvent = MidiEvent.ReadNextEvent(binaryReader, midiEvent);
						num3 += (long)midiEvent.DeltaTime;
						midiEvent.AbsoluteTime = num3;
						this.events[j].Add(midiEvent);
						if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
						{
							NoteEvent noteEvent = (NoteEvent)midiEvent;
							if (noteEvent.Velocity > 0)
							{
								list.Add((NoteOnEvent)noteEvent);
							}
							else
							{
								this.FindNoteOn(noteEvent, list);
							}
						}
						else if (midiEvent.CommandCode == MidiCommandCode.NoteOff)
						{
							this.FindNoteOn((NoteEvent)midiEvent, list);
						}
						else if (midiEvent.CommandCode == MidiCommandCode.MetaEvent)
						{
							MetaEvent metaEvent = (MetaEvent)midiEvent;
							if (metaEvent.MetaEventType == MetaEventType.EndTrack && strictChecking && binaryReader.BaseStream.Position < position + (long)((ulong)num))
							{
								throw new FormatException(string.Format("End Track event was not the last MIDI event on track {0}", j));
							}
						}
					}
					if (list.Count > 0 && strictChecking)
					{
						throw new FormatException(string.Format("Note ons without note offs {0} (file format {1})", list.Count, this.fileFormat));
					}
					if (binaryReader.BaseStream.Position != position + (long)((ulong)num))
					{
						throw new FormatException(string.Format("Read too far {0}+{1}!={2}", num, position, binaryReader.BaseStream.Position));
					}
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000FED8 File Offset: 0x0000E0D8
		public MidiEventCollection Events
		{
			get
			{
				return this.events;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		public int Tracks
		{
			get
			{
				return this.events.Tracks;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000FEED File Offset: 0x0000E0ED
		public int DeltaTicksPerQuarterNote
		{
			get
			{
				return (int)this.deltaTicksPerQuarterNote;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		private void FindNoteOn(NoteEvent offEvent, List<NoteOnEvent> outstandingNoteOns)
		{
			bool flag = false;
			foreach (NoteOnEvent noteOnEvent in outstandingNoteOns)
			{
				if (noteOnEvent.Channel == offEvent.Channel && noteOnEvent.NoteNumber == offEvent.NoteNumber)
				{
					noteOnEvent.OffEvent = offEvent;
					outstandingNoteOns.Remove(noteOnEvent);
					flag = true;
					break;
				}
			}
			if (!flag && this.strictChecking)
			{
				throw new FormatException(string.Format("Got an off without an on {0}", offEvent));
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000FF8C File Offset: 0x0000E18C
		private static uint SwapUInt32(uint i)
		{
			return ((i & 4278190080U) >> 24) | ((i & 16711680U) >> 8) | ((i & 65280U) << 8) | ((i & 255U) << 24);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000FFB7 File Offset: 0x0000E1B7
		private static ushort SwapUInt16(ushort i)
		{
			return (ushort)(((i & 65280) >> 8) | ((int)(i & 255) << 8));
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Format {0}, Tracks {1}, Delta Ticks Per Quarter Note {2}\r\n", this.fileFormat, this.Tracks, this.deltaTicksPerQuarterNote);
			for (int i = 0; i < this.Tracks; i++)
			{
				foreach (MidiEvent midiEvent in this.events[i])
				{
					stringBuilder.AppendFormat("{0}\r\n", midiEvent);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00010074 File Offset: 0x0000E274
		public static void Export(string filename, MidiEventCollection events)
		{
			if (events.MidiFileType == 0 && events.Tracks > 1)
			{
				throw new ArgumentException("Can't export more than one track to a type 0 file");
			}
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Create(filename)))
			{
				binaryWriter.Write(Encoding.UTF8.GetBytes("MThd"));
				binaryWriter.Write(MidiFile.SwapUInt32(6U));
				binaryWriter.Write(MidiFile.SwapUInt16((ushort)events.MidiFileType));
				binaryWriter.Write(MidiFile.SwapUInt16((ushort)events.Tracks));
				binaryWriter.Write(MidiFile.SwapUInt16((ushort)events.DeltaTicksPerQuarterNote));
				for (int i = 0; i < events.Tracks; i++)
				{
					IList<MidiEvent> list = events[i];
					binaryWriter.Write(Encoding.UTF8.GetBytes("MTrk"));
					long position = binaryWriter.BaseStream.Position;
					binaryWriter.Write(MidiFile.SwapUInt32(0U));
					long startAbsoluteTime = events.StartAbsoluteTime;
					MergeSort.Sort<MidiEvent>(list, new MidiEventComparer());
					int count = list.Count;
					foreach (MidiEvent midiEvent in list)
					{
						midiEvent.Export(ref startAbsoluteTime, binaryWriter);
					}
					uint num = (uint)(binaryWriter.BaseStream.Position - position) - 4U;
					binaryWriter.BaseStream.Position = position;
					binaryWriter.Write(MidiFile.SwapUInt32(num));
					binaryWriter.BaseStream.Position += (long)((ulong)num);
				}
			}
		}

		// Token: 0x04000580 RID: 1408
		private MidiEventCollection events;

		// Token: 0x04000581 RID: 1409
		private ushort fileFormat;

		// Token: 0x04000582 RID: 1410
		private ushort deltaTicksPerQuarterNote;

		// Token: 0x04000583 RID: 1411
		private bool strictChecking;
	}
}
