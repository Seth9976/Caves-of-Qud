using System;
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi.Interfaces;

namespace NAudio.Dmo
{
	// Token: 0x02000204 RID: 516
	public class DmoResampler : IDisposable
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x00022A6C File Offset: 0x00020C6C
		public DmoResampler()
		{
			this.mediaComObject = new ResamplerMediaComObject();
			this.mediaObject = new MediaObject((IMediaObject)this.mediaComObject);
			this.propertyStoreInterface = (IPropertyStore)this.mediaComObject;
			this.resamplerPropsInterface = (IWMResamplerProps)this.mediaComObject;
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00022AC2 File Offset: 0x00020CC2
		public MediaObject MediaObject
		{
			get
			{
				return this.mediaObject;
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00022ACC File Offset: 0x00020CCC
		public void Dispose()
		{
			if (this.propertyStoreInterface != null)
			{
				Marshal.ReleaseComObject(this.propertyStoreInterface);
				this.propertyStoreInterface = null;
			}
			if (this.resamplerPropsInterface != null)
			{
				Marshal.ReleaseComObject(this.resamplerPropsInterface);
				this.resamplerPropsInterface = null;
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

		// Token: 0x04000C20 RID: 3104
		private MediaObject mediaObject;

		// Token: 0x04000C21 RID: 3105
		private IPropertyStore propertyStoreInterface;

		// Token: 0x04000C22 RID: 3106
		private IWMResamplerProps resamplerPropsInterface;

		// Token: 0x04000C23 RID: 3107
		private ResamplerMediaComObject mediaComObject;
	}
}
