using System;

namespace NAudio.Midi
{
	// Token: 0x020000D4 RID: 212
	public enum MidiController : byte
	{
		// Token: 0x0400056D RID: 1389
		BankSelect,
		// Token: 0x0400056E RID: 1390
		Modulation,
		// Token: 0x0400056F RID: 1391
		BreathController,
		// Token: 0x04000570 RID: 1392
		FootController = 4,
		// Token: 0x04000571 RID: 1393
		MainVolume = 7,
		// Token: 0x04000572 RID: 1394
		Pan = 10,
		// Token: 0x04000573 RID: 1395
		Expression,
		// Token: 0x04000574 RID: 1396
		BankSelectLsb = 32,
		// Token: 0x04000575 RID: 1397
		Sustain = 64,
		// Token: 0x04000576 RID: 1398
		Portamento,
		// Token: 0x04000577 RID: 1399
		Sostenuto,
		// Token: 0x04000578 RID: 1400
		SoftPedal,
		// Token: 0x04000579 RID: 1401
		LegatoFootswitch,
		// Token: 0x0400057A RID: 1402
		ResetAllControllers = 121,
		// Token: 0x0400057B RID: 1403
		AllNotesOff = 123
	}
}
