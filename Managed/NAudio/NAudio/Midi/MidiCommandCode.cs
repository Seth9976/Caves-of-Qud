using System;

namespace NAudio.Midi
{
	// Token: 0x020000D3 RID: 211
	public enum MidiCommandCode : byte
	{
		// Token: 0x0400055D RID: 1373
		NoteOff = 128,
		// Token: 0x0400055E RID: 1374
		NoteOn = 144,
		// Token: 0x0400055F RID: 1375
		KeyAfterTouch = 160,
		// Token: 0x04000560 RID: 1376
		ControlChange = 176,
		// Token: 0x04000561 RID: 1377
		PatchChange = 192,
		// Token: 0x04000562 RID: 1378
		ChannelAfterTouch = 208,
		// Token: 0x04000563 RID: 1379
		PitchWheelChange = 224,
		// Token: 0x04000564 RID: 1380
		Sysex = 240,
		// Token: 0x04000565 RID: 1381
		Eox = 247,
		// Token: 0x04000566 RID: 1382
		TimingClock,
		// Token: 0x04000567 RID: 1383
		StartSequence = 250,
		// Token: 0x04000568 RID: 1384
		ContinueSequence,
		// Token: 0x04000569 RID: 1385
		StopSequence,
		// Token: 0x0400056A RID: 1386
		AutoSensing = 254,
		// Token: 0x0400056B RID: 1387
		MetaEvent
	}
}
