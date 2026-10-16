using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000019 RID: 25
	public class AudioMeterInformationChannels
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000044B0 File Offset: 0x000026B0
		public int Count
		{
			get
			{
				int num;
				Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetMeteringChannelCount(out num));
				return num;
			}
		}

		// Token: 0x17000023 RID: 35
		public float this[int index]
		{
			get
			{
				float[] array = new float[this.Count];
				GCHandle gchandle = GCHandle.Alloc(array, 3);
				Marshal.ThrowExceptionForHR(this.audioMeterInformation.GetChannelsPeakValues(array.Length, gchandle.AddrOfPinnedObject()));
				gchandle.Free();
				return array[index];
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004515 File Offset: 0x00002715
		internal AudioMeterInformationChannels(IAudioMeterInformation parent)
		{
			this.audioMeterInformation = parent;
		}

		// Token: 0x0400005B RID: 91
		private readonly IAudioMeterInformation audioMeterInformation;
	}
}
