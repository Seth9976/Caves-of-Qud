using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000116 RID: 278
	public class AudioMeterInformation
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x00013614 File Offset: 0x00011814
		internal AudioMeterInformation(IAudioMeterInformation realInterface)
		{
			this.audioMeterInformation = realInterface;
			int num;
			Marshal.ThrowExceptionForHR(this.audioMeterInformation.QueryHardwareSupport(out num));
			this.hardwareSupport = (EEndpointHardwareSupport)num;
			this.channels = new AudioMeterInformationChannels(this.audioMeterInformation);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00013658 File Offset: 0x00011858
		public AudioMeterInformationChannels PeakValues
		{
			get
			{
				return this.channels;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00013660 File Offset: 0x00011860
		public EEndpointHardwareSupport HardwareSupport
		{
			get
			{
				return this.hardwareSupport;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00013668 File Offset: 0x00011868
		public float MasterPeakValue
		{
			get
			{
				float num;
				Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetPeakValue(out num));
				return num;
			}
		}

		// Token: 0x040006CC RID: 1740
		private readonly IAudioMeterInformation audioMeterInformation;

		// Token: 0x040006CD RID: 1741
		private readonly EEndpointHardwareSupport hardwareSupport;

		// Token: 0x040006CE RID: 1742
		private readonly AudioMeterInformationChannels channels;
	}
}
