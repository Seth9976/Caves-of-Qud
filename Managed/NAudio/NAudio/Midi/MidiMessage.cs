using System;

namespace NAudio.Midi
{
	// Token: 0x020000E3 RID: 227
	public class MidiMessage
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x00010508 File Offset: 0x0000E708
		public MidiMessage(int status, int data1, int data2)
		{
			this.rawData = status + (data1 << 8) + (data2 << 16);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00010520 File Offset: 0x0000E720
		public MidiMessage(int rawData)
		{
			this.rawData = rawData;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001052F File Offset: 0x0000E72F
		public static MidiMessage StartNote(int note, int volume, int channel)
		{
			return new MidiMessage(144 + channel - 1, note, volume);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00010541 File Offset: 0x0000E741
		public static MidiMessage StopNote(int note, int volume, int channel)
		{
			return new MidiMessage(128 + channel - 1, note, volume);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00010553 File Offset: 0x0000E753
		public static MidiMessage ChangePatch(int patch, int channel)
		{
			return new MidiMessage(192 + channel - 1, patch, 0);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00010565 File Offset: 0x0000E765
		public static MidiMessage ChangeControl(int controller, int value, int channel)
		{
			return new MidiMessage(176 + channel - 1, controller, value);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00010577 File Offset: 0x0000E777
		public int RawData
		{
			get
			{
				return this.rawData;
			}
		}

		// Token: 0x040005AE RID: 1454
		private int rawData;
	}
}
