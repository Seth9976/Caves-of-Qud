using System;
using System.Globalization;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000023 RID: 35
	public class AudioStreamVolume : IDisposable
	{
		// Token: 0x06000096 RID: 150 RVA: 0x00004ACA File Offset: 0x00002CCA
		internal AudioStreamVolume(IAudioStreamVolume audioStreamVolumeInterface)
		{
			this.audioStreamVolumeInterface = audioStreamVolumeInterface;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004ADC File Offset: 0x00002CDC
		private void CheckChannelIndex(int channelIndex, string parameter)
		{
			int channelCount = this.ChannelCount;
			if (channelIndex >= channelCount)
			{
				throw new ArgumentOutOfRangeException(parameter, "You must supply a valid channel index < current count of channels: " + channelCount.ToString());
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004B0C File Offset: 0x00002D0C
		public float[] GetAllVolumes()
		{
			uint num;
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelCount(out num));
			float[] array = new float[num];
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetAllVolumes(num, array));
			return array;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004B48 File Offset: 0x00002D48
		public int ChannelCount
		{
			get
			{
				uint num;
				Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelCount(out num));
				return (int)num;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004B68 File Offset: 0x00002D68
		public float GetChannelVolume(int channelIndex)
		{
			this.CheckChannelIndex(channelIndex, "channelIndex");
			float num;
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.GetChannelVolume((uint)channelIndex, out num));
			return num;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004B98 File Offset: 0x00002D98
		public void SetAllVolumes(float[] levels)
		{
			int channelCount = this.ChannelCount;
			if (levels == null)
			{
				throw new ArgumentNullException("levels");
			}
			if (levels.Length != channelCount)
			{
				throw new ArgumentOutOfRangeException("levels", string.Format(CultureInfo.InvariantCulture, "SetAllVolumes MUST be supplied with a volume level for ALL channels. The AudioStream has {0} channels and you supplied {1} channels.", new object[] { channelCount, levels.Length }));
			}
			for (int i = 0; i < levels.Length; i++)
			{
				float num = levels[i];
				if (num < 0f)
				{
					throw new ArgumentOutOfRangeException("levels", "All volumes must be between 0.0 and 1.0. Invalid volume at index: " + i.ToString());
				}
				if (num > 1f)
				{
					throw new ArgumentOutOfRangeException("levels", "All volumes must be between 0.0 and 1.0. Invalid volume at index: " + i.ToString());
				}
			}
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.SetAllVoumes((uint)channelCount, levels));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004C64 File Offset: 0x00002E64
		public void SetChannelVolume(int index, float level)
		{
			this.CheckChannelIndex(index, "index");
			if (level < 0f)
			{
				throw new ArgumentOutOfRangeException("level", "Volume must be between 0.0 and 1.0");
			}
			if (level > 1f)
			{
				throw new ArgumentOutOfRangeException("level", "Volume must be between 0.0 and 1.0");
			}
			Marshal.ThrowExceptionForHR(this.audioStreamVolumeInterface.SetChannelVolume((uint)index, level));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CBF File Offset: 0x00002EBF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004CCE File Offset: 0x00002ECE
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.audioStreamVolumeInterface != null)
			{
				Marshal.ReleaseComObject(this.audioStreamVolumeInterface);
				this.audioStreamVolumeInterface = null;
			}
		}

		// Token: 0x04000074 RID: 116
		private IAudioStreamVolume audioStreamVolumeInterface;
	}
}
