using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.Dmo
{
	// Token: 0x02000098 RID: 152
	public class WindowsMediaMp3Decoder : IDisposable
	{
		// Token: 0x0600033F RID: 831 RVA: 0x0000AD0F File Offset: 0x00008F0F
		public WindowsMediaMp3Decoder()
		{
			this.mediaComObject = new WindowsMediaMp3DecoderComObject();
			this.mediaObject = new MediaObject((IMediaObject)this.mediaComObject);
			this.propertyStoreInterface = (IPropertyStore)this.mediaComObject;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000AD49 File Offset: 0x00008F49
		public MediaObject MediaObject
		{
			get
			{
				return this.mediaObject;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000AD54 File Offset: 0x00008F54
		public void Dispose()
		{
			if (this.propertyStoreInterface != null)
			{
				Marshal.ReleaseComObject(this.propertyStoreInterface);
				this.propertyStoreInterface = null;
			}
			if (this.mediaObject != null)
			{
				this.mediaObject.Dispose();
				this.mediaObject = null;
			}
			if (this.mediaComObject != null)
			{
				Marshal.ReleaseComObject(this.mediaComObject);
				this.mediaComObject = null;
			}
		}

		// Token: 0x04000425 RID: 1061
		private MediaObject mediaObject;

		// Token: 0x04000426 RID: 1062
		private IPropertyStore propertyStoreInterface;

		// Token: 0x04000427 RID: 1063
		private WindowsMediaMp3DecoderComObject mediaComObject;
	}
}
