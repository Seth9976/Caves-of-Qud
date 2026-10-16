using System;

namespace NAudio.Midi
{
	// Token: 0x020000D2 RID: 210
	public enum MetaEventType : byte
	{
		// Token: 0x0400054A RID: 1354
		TrackSequenceNumber,
		// Token: 0x0400054B RID: 1355
		TextEvent,
		// Token: 0x0400054C RID: 1356
		Copyright,
		// Token: 0x0400054D RID: 1357
		SequenceTrackName,
		// Token: 0x0400054E RID: 1358
		TrackInstrumentName,
		// Token: 0x0400054F RID: 1359
		Lyric,
		// Token: 0x04000550 RID: 1360
		Marker,
		// Token: 0x04000551 RID: 1361
		CuePoint,
		// Token: 0x04000552 RID: 1362
		ProgramName,
		// Token: 0x04000553 RID: 1363
		DeviceName,
		// Token: 0x04000554 RID: 1364
		MidiChannel = 32,
		// Token: 0x04000555 RID: 1365
		MidiPort,
		// Token: 0x04000556 RID: 1366
		EndTrack = 47,
		// Token: 0x04000557 RID: 1367
		SetTempo = 81,
		// Token: 0x04000558 RID: 1368
		SmpteOffset = 84,
		// Token: 0x04000559 RID: 1369
		TimeSignature = 88,
		// Token: 0x0400055A RID: 1370
		KeySignature,
		// Token: 0x0400055B RID: 1371
		SequencerSpecific = 127
	}
}
