using System;

namespace NAudio.SoundFont
{
	// Token: 0x020000BE RID: 190
	public class ModulatorType
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		internal ModulatorType(ushort raw)
		{
			this.polarity = (raw & 512) == 512;
			this.direction = (raw & 256) == 256;
			this.midiContinuousController = (raw & 128) == 128;
			this.sourceType = (SourceTypeEnum)((raw & 64512) >> 10);
			this.controllerSource = (ControllerSourceEnum)(raw & 127);
			this.midiContinuousControllerNumber = raw & 127;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000DE68 File Offset: 0x0000C068
		public override string ToString()
		{
			if (this.midiContinuousController)
			{
				return string.Format("{0} CC{1}", this.sourceType, this.midiContinuousControllerNumber);
			}
			return string.Format("{0} {1}", this.sourceType, this.controllerSource);
		}

		// Token: 0x040004FB RID: 1275
		private bool polarity;

		// Token: 0x040004FC RID: 1276
		private bool direction;

		// Token: 0x040004FD RID: 1277
		private bool midiContinuousController;

		// Token: 0x040004FE RID: 1278
		private ControllerSourceEnum controllerSource;

		// Token: 0x040004FF RID: 1279
		private SourceTypeEnum sourceType;

		// Token: 0x04000500 RID: 1280
		private ushort midiContinuousControllerNumber;
	}
}
