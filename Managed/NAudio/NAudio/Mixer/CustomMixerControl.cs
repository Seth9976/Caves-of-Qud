using System;

namespace NAudio.Mixer
{
	// Token: 0x020000F5 RID: 245
	public class CustomMixerControl : MixerControl
	{
		// Token: 0x0600058F RID: 1423 RVA: 0x000122E8 File Offset: 0x000104E8
		internal CustomMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0001231F File Offset: 0x0001051F
		protected override void GetDetails(IntPtr pDetails)
		{
		}
	}
}
