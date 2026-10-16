using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000115 RID: 277
	public class AudioEndpointVolume : IDisposable
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060005F7 RID: 1527 RVA: 0x000133AC File Offset: 0x000115AC
		// (remove) Token: 0x060005F8 RID: 1528 RVA: 0x000133E4 File Offset: 0x000115E4
		public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00013419 File Offset: 0x00011619
		public AudioEndpointVolumeVolumeRange VolumeRange
		{
			get
			{
				return this.volumeRange;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00013421 File Offset: 0x00011621
		public EEndpointHardwareSupport HardwareSupport
		{
			get
			{
				return this.hardwareSupport;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00013429 File Offset: 0x00011629
		public AudioEndpointVolumeStepInformation StepInformation
		{
			get
			{
				return this.stepInformation;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00013431 File Offset: 0x00011631
		public AudioEndpointVolumeChannels Channels
		{
			get
			{
				return this.channels;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001343C File Offset: 0x0001163C
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0001345C File Offset: 0x0001165C
		public float MasterVolumeLevel
		{
			get
			{
				float num;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMasterVolumeLevel(out num));
				return num;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty));
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00013474 File Offset: 0x00011674
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00013494 File Offset: 0x00011694
		public float MasterVolumeLevelScalar
		{
			get
			{
				float num;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMasterVolumeLevelScalar(out num));
				return num;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x000134AC File Offset: 0x000116AC
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x000134CC File Offset: 0x000116CC
		public bool Mute
		{
			get
			{
				bool flag;
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.GetMute(out flag));
				return flag;
			}
			set
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.SetMute(value, Guid.Empty));
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000134E4 File Offset: 0x000116E4
		public void VolumeStepUp()
		{
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.VolumeStepUp(Guid.Empty));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000134FB File Offset: 0x000116FB
		public void VolumeStepDown()
		{
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.VolumeStepDown(Guid.Empty));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00013514 File Offset: 0x00011714
		internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume)
		{
			this.audioEndPointVolume = realEndpointVolume;
			this.channels = new AudioEndpointVolumeChannels(this.audioEndPointVolume);
			this.stepInformation = new AudioEndpointVolumeStepInformation(this.audioEndPointVolume);
			uint num;
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.QueryHardwareSupport(out num));
			this.hardwareSupport = (EEndpointHardwareSupport)num;
			this.volumeRange = new AudioEndpointVolumeVolumeRange(this.audioEndPointVolume);
			this.callBack = new AudioEndpointVolumeCallback(this);
			Marshal.ThrowExceptionForHR(this.audioEndPointVolume.RegisterControlChangeNotify(this.callBack));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001359C File Offset: 0x0001179C
		internal void FireNotification(AudioVolumeNotificationData notificationData)
		{
			AudioEndpointVolumeNotificationDelegate onVolumeNotification = this.OnVolumeNotification;
			if (onVolumeNotification != null)
			{
				onVolumeNotification(notificationData);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000135BA File Offset: 0x000117BA
		public void Dispose()
		{
			if (this.callBack != null)
			{
				Marshal.ThrowExceptionForHR(this.audioEndPointVolume.UnregisterControlChangeNotify(this.callBack));
				this.callBack = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000135E8 File Offset: 0x000117E8
		~AudioEndpointVolume()
		{
			this.Dispose();
		}

		// Token: 0x040006C5 RID: 1733
		private readonly IAudioEndpointVolume audioEndPointVolume;

		// Token: 0x040006C6 RID: 1734
		private readonly AudioEndpointVolumeChannels channels;

		// Token: 0x040006C7 RID: 1735
		private readonly AudioEndpointVolumeStepInformation stepInformation;

		// Token: 0x040006C8 RID: 1736
		private readonly AudioEndpointVolumeVolumeRange volumeRange;

		// Token: 0x040006C9 RID: 1737
		private readonly EEndpointHardwareSupport hardwareSupport;

		// Token: 0x040006CA RID: 1738
		private AudioEndpointVolumeCallback callBack;
	}
}
