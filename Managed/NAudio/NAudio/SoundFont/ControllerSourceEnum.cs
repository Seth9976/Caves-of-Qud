using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000BC RID: 188
	public enum ControllerSourceEnum
	{
		// Token: 0x040004EF RID: 1263
		NoController,
		// Token: 0x040004F0 RID: 1264
		NoteOnVelocity = 2,
		// Token: 0x040004F1 RID: 1265
		NoteOnKeyNumber,
		// Token: 0x040004F2 RID: 1266
		PolyPressure = 10,
		// Token: 0x040004F3 RID: 1267
		ChannelPressure = 13,
		// Token: 0x040004F4 RID: 1268
		PitchWheel,
		// Token: 0x040004F5 RID: 1269
		PitchWheelSensitivity = 16
	}
}
