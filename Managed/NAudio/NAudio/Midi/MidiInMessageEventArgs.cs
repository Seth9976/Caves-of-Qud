using System;

namespace NAudio.Midi
{
	// Token: 0x02000067 RID: 103
	public class MidiInMessageEventArgs : EventArgs
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000074C8 File Offset: 0x000056C8
		public MidiInMessageEventArgs(int message, int timestamp)
		{
			this.RawMessage = message;
			this.Timestamp = timestamp;
			try
			{
				this.MidiEvent = MidiEvent.FromRawMessage(message);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000750C File Offset: 0x0000570C
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00007514 File Offset: 0x00005714
		public int RawMessage { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000751D File Offset: 0x0000571D
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00007525 File Offset: 0x00005725
		public MidiEvent MidiEvent { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000752E File Offset: 0x0000572E
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00007536 File Offset: 0x00005736
		public int Timestamp { get; private set; }
	}
}
