using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000CD RID: 205
	public class MidiEvent
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public static MidiEvent FromRawMessage(int rawMessage)
		{
			long num = 0L;
			int num2 = rawMessage & 255;
			int num3 = (rawMessage >> 8) & 255;
			int num4 = (rawMessage >> 16) & 255;
			int num5 = 1;
			MidiCommandCode midiCommandCode;
			if ((num2 & 240) == 240)
			{
				midiCommandCode = (MidiCommandCode)num2;
			}
			else
			{
				midiCommandCode = (MidiCommandCode)(num2 & 240);
				num5 = (num2 & 15) + 1;
			}
			MidiCommandCode midiCommandCode2 = midiCommandCode;
			if (midiCommandCode2 <= MidiCommandCode.ControlChange)
			{
				if (midiCommandCode2 <= MidiCommandCode.NoteOn)
				{
					if (midiCommandCode2 != MidiCommandCode.NoteOff && midiCommandCode2 != MidiCommandCode.NoteOn)
					{
						goto IL_0177;
					}
				}
				else if (midiCommandCode2 != MidiCommandCode.KeyAfterTouch)
				{
					if (midiCommandCode2 != MidiCommandCode.ControlChange)
					{
						goto IL_0177;
					}
					return new ControlChangeEvent(num, num5, (MidiController)num3, num4);
				}
				if (num4 > 0 && midiCommandCode == MidiCommandCode.NoteOn)
				{
					return new NoteOnEvent(num, num5, num3, num4, 0);
				}
				return new NoteEvent(num, num5, midiCommandCode, num3, num4);
			}
			else if (midiCommandCode2 <= MidiCommandCode.ChannelAfterTouch)
			{
				if (midiCommandCode2 == MidiCommandCode.PatchChange)
				{
					return new PatchChangeEvent(num, num5, num3);
				}
				if (midiCommandCode2 == MidiCommandCode.ChannelAfterTouch)
				{
					return new ChannelAfterTouchEvent(num, num5, num3);
				}
			}
			else
			{
				if (midiCommandCode2 == MidiCommandCode.PitchWheelChange)
				{
					return new PitchWheelChangeEvent(num, num5, num3 + (num4 << 7));
				}
				if (midiCommandCode2 != MidiCommandCode.Sysex)
				{
					switch (midiCommandCode2)
					{
					case MidiCommandCode.TimingClock:
					case MidiCommandCode.StartSequence:
					case MidiCommandCode.ContinueSequence:
					case MidiCommandCode.StopSequence:
					case MidiCommandCode.AutoSensing:
						return new MidiEvent(num, num5, midiCommandCode);
					}
				}
			}
			IL_0177:
			throw new FormatException(string.Format("Unsupported MIDI Command Code for Raw Message {0}", midiCommandCode));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000EE0C File Offset: 0x0000D00C
		public static MidiEvent ReadNextEvent(BinaryReader br, MidiEvent previous)
		{
			int num = MidiEvent.ReadVarInt(br);
			int num2 = 1;
			byte b = br.ReadByte();
			MidiCommandCode midiCommandCode;
			if ((b & 128) == 0)
			{
				midiCommandCode = previous.CommandCode;
				num2 = previous.Channel;
				br.BaseStream.Position -= 1L;
			}
			else if ((b & 240) == 240)
			{
				midiCommandCode = (MidiCommandCode)b;
			}
			else
			{
				midiCommandCode = (MidiCommandCode)(b & 240);
				num2 = (int)((b & 15) + 1);
			}
			MidiCommandCode midiCommandCode2 = midiCommandCode;
			MidiEvent midiEvent;
			if (midiCommandCode2 <= MidiCommandCode.ControlChange)
			{
				if (midiCommandCode2 <= MidiCommandCode.NoteOn)
				{
					if (midiCommandCode2 != MidiCommandCode.NoteOff)
					{
						if (midiCommandCode2 != MidiCommandCode.NoteOn)
						{
							goto IL_015F;
						}
						midiEvent = new NoteOnEvent(br);
						goto IL_0175;
					}
				}
				else if (midiCommandCode2 != MidiCommandCode.KeyAfterTouch)
				{
					if (midiCommandCode2 != MidiCommandCode.ControlChange)
					{
						goto IL_015F;
					}
					midiEvent = new ControlChangeEvent(br);
					goto IL_0175;
				}
				midiEvent = new NoteEvent(br);
				goto IL_0175;
			}
			if (midiCommandCode2 <= MidiCommandCode.ChannelAfterTouch)
			{
				if (midiCommandCode2 == MidiCommandCode.PatchChange)
				{
					midiEvent = new PatchChangeEvent(br);
					goto IL_0175;
				}
				if (midiCommandCode2 == MidiCommandCode.ChannelAfterTouch)
				{
					midiEvent = new ChannelAfterTouchEvent(br);
					goto IL_0175;
				}
			}
			else
			{
				if (midiCommandCode2 == MidiCommandCode.PitchWheelChange)
				{
					midiEvent = new PitchWheelChangeEvent(br);
					goto IL_0175;
				}
				if (midiCommandCode2 == MidiCommandCode.Sysex)
				{
					midiEvent = SysexEvent.ReadSysexEvent(br);
					goto IL_0175;
				}
				switch (midiCommandCode2)
				{
				case MidiCommandCode.TimingClock:
				case MidiCommandCode.StartSequence:
				case MidiCommandCode.ContinueSequence:
				case MidiCommandCode.StopSequence:
					midiEvent = new MidiEvent();
					goto IL_0175;
				case MidiCommandCode.MetaEvent:
					midiEvent = MetaEvent.ReadMetaEvent(br);
					goto IL_0175;
				}
			}
			IL_015F:
			throw new FormatException(string.Format("Unsupported MIDI Command Code {0:X2}", (byte)midiCommandCode));
			IL_0175:
			midiEvent.channel = num2;
			midiEvent.deltaTime = num;
			midiEvent.commandCode = midiCommandCode;
			return midiEvent;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public virtual int GetAsShortMessage()
		{
			return (int)((byte)(this.channel - 1) + this.commandCode);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000EFB9 File Offset: 0x0000D1B9
		protected MidiEvent()
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000EFC1 File Offset: 0x0000D1C1
		public MidiEvent(long absoluteTime, int channel, MidiCommandCode commandCode)
		{
			this.absoluteTime = absoluteTime;
			this.Channel = channel;
			this.commandCode = commandCode;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000EFDE File Offset: 0x0000D1DE
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0000EFE6 File Offset: 0x0000D1E6
		public virtual int Channel
		{
			get
			{
				return this.channel;
			}
			set
			{
				if (value < 1 || value > 16)
				{
					throw new ArgumentOutOfRangeException("value", value, string.Format("Channel must be 1-16 (Got {0})", value));
				}
				this.channel = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000F019 File Offset: 0x0000D219
		public int DeltaTime
		{
			get
			{
				return this.deltaTime;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000F021 File Offset: 0x0000D221
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000F029 File Offset: 0x0000D229
		public long AbsoluteTime
		{
			get
			{
				return this.absoluteTime;
			}
			set
			{
				this.absoluteTime = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000F032 File Offset: 0x0000D232
		public MidiCommandCode CommandCode
		{
			get
			{
				return this.commandCode;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000F03C File Offset: 0x0000D23C
		public static bool IsNoteOff(MidiEvent midiEvent)
		{
			if (midiEvent == null)
			{
				return false;
			}
			if (midiEvent.CommandCode == MidiCommandCode.NoteOn)
			{
				NoteEvent noteEvent = (NoteEvent)midiEvent;
				return noteEvent.Velocity == 0;
			}
			return midiEvent.CommandCode == MidiCommandCode.NoteOff;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000F07C File Offset: 0x0000D27C
		public static bool IsNoteOn(MidiEvent midiEvent)
		{
			if (midiEvent != null && midiEvent.CommandCode == MidiCommandCode.NoteOn)
			{
				NoteEvent noteEvent = (NoteEvent)midiEvent;
				return noteEvent.Velocity > 0;
			}
			return false;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		public static bool IsEndTrack(MidiEvent midiEvent)
		{
			if (midiEvent != null)
			{
				MetaEvent metaEvent = midiEvent as MetaEvent;
				if (metaEvent != null)
				{
					return metaEvent.MetaEventType == MetaEventType.EndTrack;
				}
			}
			return false;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		public override string ToString()
		{
			if (this.commandCode >= MidiCommandCode.Sysex)
			{
				return string.Format("{0} {1}", this.absoluteTime, this.commandCode);
			}
			return string.Format("{0} {1} Ch: {2}", this.absoluteTime, this.commandCode, this.channel);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000F13C File Offset: 0x0000D33C
		public static int ReadVarInt(BinaryReader br)
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				byte b = br.ReadByte();
				num <<= 7;
				num += (int)(b & 127);
				if ((b & 128) == 0)
				{
					return num;
				}
			}
			throw new FormatException("Invalid Var Int");
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000F180 File Offset: 0x0000D380
		public static void WriteVarInt(BinaryWriter writer, int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", value, "Cannot write a negative Var Int");
			}
			if (value > 268435455)
			{
				throw new ArgumentOutOfRangeException("value", value, "Maximum allowed Var Int is 0x0FFFFFFF");
			}
			int i = 0;
			byte[] array = new byte[4];
			do
			{
				array[i++] = (byte)(value & 127);
				value >>= 7;
			}
			while (value > 0);
			while (i > 0)
			{
				i--;
				if (i > 0)
				{
					writer.Write(array[i] | 128);
				}
				else
				{
					writer.Write(array[i]);
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000F20C File Offset: 0x0000D40C
		public virtual void Export(ref long absoluteTime, BinaryWriter writer)
		{
			if (this.absoluteTime < absoluteTime)
			{
				throw new FormatException("Can't export unsorted MIDI events");
			}
			MidiEvent.WriteVarInt(writer, (int)(this.absoluteTime - absoluteTime));
			absoluteTime = this.absoluteTime;
			int num = (int)this.commandCode;
			if (this.commandCode != MidiCommandCode.MetaEvent)
			{
				num += this.channel - 1;
			}
			writer.Write((byte)num);
		}

		// Token: 0x0400053D RID: 1341
		private MidiCommandCode commandCode;

		// Token: 0x0400053E RID: 1342
		private int channel;

		// Token: 0x0400053F RID: 1343
		private int deltaTime;

		// Token: 0x04000540 RID: 1344
		private long absoluteTime;
	}
}
