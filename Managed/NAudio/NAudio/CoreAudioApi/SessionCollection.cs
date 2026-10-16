using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.CoreAudioApi
{
	// Token: 0x02000038 RID: 56
	public class SessionCollection
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00005391 File Offset: 0x00003591
		internal SessionCollection(IAudioSessionEnumerator realEnumerator)
		{
			this.audioSessionEnumerator = realEnumerator;
		}

		// Token: 0x17000038 RID: 56
		public AudioSessionControl this[int index]
		{
			get
			{
				IAudioSessionControl audioSessionControl;
				Marshal.ThrowExceptionForHR(this.audioSessionEnumerator.GetSession(index, out audioSessionControl));
				return new AudioSessionControl(audioSessionControl);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000053C8 File Offset: 0x000035C8
		public int Count
		{
			get
			{
				int num;
				Marshal.ThrowExceptionForHR(this.audioSessionEnumerator.GetCount(out num));
				return num;
			}
		}

		// Token: 0x040000B5 RID: 181
		private readonly IAudioSessionEnumerator audioSessionEnumerator;
	}
}
