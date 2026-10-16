using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000E9 RID: 233
	public class NoteOnEvent : NoteEvent
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x00010C22 File Offset: 0x0000EE22
		public NoteOnEvent(BinaryReader br)
			: base(br)
		{
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00010C2B File Offset: 0x0000EE2B
		public NoteOnEvent(long absoluteTime, int channel, int noteNumber, int velocity, int duration)
			: base(absoluteTime, channel, MidiCommandCode.NoteOn, noteNumber, velocity)
		{
			this.OffEvent = new NoteEvent(absoluteTime, channel, MidiCommandCode.NoteOff, noteNumber, 0);
			this.NoteLength = duration;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00010C59 File Offset: 0x0000EE59
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00010C64 File Offset: 0x0000EE64
		public NoteEvent OffEvent
		{
			get
			{
				return this.offEvent;
			}
			set
			{
				if (!MidiEvent.IsNoteOff(value))
				{
					throw new ArgumentException("OffEvent must be a valid MIDI note off event");
				}
				if (value.NoteNumber != this.NoteNumber)
				{
					throw new ArgumentException("Note Off Event must be for the same note number");
				}
				if (value.Channel != this.Channel)
				{
					throw new ArgumentException("Note Off Event must be for the same channel");
				}
				this.offEvent = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00010CBD File Offset: 0x0000EEBD
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00010CC5 File Offset: 0x0000EEC5
		public override int NoteNumber
		{
			get
			{
				return base.NoteNumber;
			}
			set
			{
				base.NoteNumber = value;
				if (this.OffEvent != null)
				{
					this.OffEvent.NoteNumber = this.NoteNumber;
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00010CE7 File Offset: 0x0000EEE7
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00010CEF File Offset: 0x0000EEEF
		public override int Channel
		{
			get
			{
				return base.Channel;
			}
			set
			{
				base.Channel = value;
				if (this.OffEvent != null)
				{
					this.OffEvent.Channel = this.Channel;
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00010D11 File Offset: 0x0000EF11
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00010D26 File Offset: 0x0000EF26
		public int NoteLength
		{
			get
			{
				return (int)(this.offEvent.AbsoluteTime - base.AbsoluteTime);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("NoteLength must be 0 or greater");
				}
				this.offEvent.AbsoluteTime = base.AbsoluteTime + (long)value;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00010D4C File Offset: 0x0000EF4C
		public override string ToString()
		{
			if (base.Velocity == 0 && this.OffEvent == null)
			{
				return string.Format("{0} (Note Off)", base.ToString());
			}
			return string.Format("{0} Len: {1}", base.ToString(), (this.OffEvent == null) ? "?" : this.NoteLength.ToString());
		}

		// Token: 0x040005CC RID: 1484
		private NoteEvent offEvent;
	}
}
