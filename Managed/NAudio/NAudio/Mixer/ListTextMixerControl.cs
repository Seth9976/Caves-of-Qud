using System;

namespace NAudio.Mixer
{
	// Token: 0x020000F6 RID: 246
	public class ListTextMixerControl : MixerControl
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00012321 File Offset: 0x00010521
		internal ListTextMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00012358 File Offset: 0x00010558
		protected override void GetDetails(IntPtr pDetails)
		{
		}
	}
}
