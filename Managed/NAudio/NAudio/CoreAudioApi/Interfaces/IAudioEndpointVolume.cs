using System;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
	// Token: 0x0200011D RID: 285
	[InterfaceType(1)]
	[Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
	internal interface IAudioEndpointVolume
	{
		// Token: 0x0600061A RID: 1562
		int RegisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

		// Token: 0x0600061B RID: 1563
		int UnregisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

		// Token: 0x0600061C RID: 1564
		int GetChannelCount(out int pnChannelCount);

		// Token: 0x0600061D RID: 1565
		int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);

		// Token: 0x0600061E RID: 1566
		int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

		// Token: 0x0600061F RID: 1567
		int GetMasterVolumeLevel(out float pfLevelDB);

		// Token: 0x06000620 RID: 1568
		int GetMasterVolumeLevelScalar(out float pfLevel);

		// Token: 0x06000621 RID: 1569
		int SetChannelVolumeLevel(uint nChannel, float fLevelDB, Guid pguidEventContext);

		// Token: 0x06000622 RID: 1570
		int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, Guid pguidEventContext);

		// Token: 0x06000623 RID: 1571
		int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);

		// Token: 0x06000624 RID: 1572
		int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);

		// Token: 0x06000625 RID: 1573
		int SetMute([MarshalAs(2)] bool bMute, Guid pguidEventContext);

		// Token: 0x06000626 RID: 1574
		int GetMute(out bool pbMute);

		// Token: 0x06000627 RID: 1575
		int GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);

		// Token: 0x06000628 RID: 1576
		int VolumeStepUp(Guid pguidEventContext);

		// Token: 0x06000629 RID: 1577
		int VolumeStepDown(Guid pguidEventContext);

		// Token: 0x0600062A RID: 1578
		int QueryHardwareSupport(out uint pdwHardwareSupportMask);

		// Token: 0x0600062B RID: 1579
		int GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
	}
}
