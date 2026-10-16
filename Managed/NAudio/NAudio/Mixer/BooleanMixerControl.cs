using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x020000F4 RID: 244
	public class BooleanMixerControl : MixerControl
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x000121EF File Offset: 0x000103EF
		internal BooleanMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00012226 File Offset: 0x00010426
		protected override void GetDetails(IntPtr pDetails)
		{
			this.boolDetails = (MixerInterop.MIXERCONTROLDETAILS_BOOLEAN)Marshal.PtrToStructure(pDetails, typeof(MixerInterop.MIXERCONTROLDETAILS_BOOLEAN));
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00012243 File Offset: 0x00010443
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0001225C File Offset: 0x0001045C
		public bool Value
		{
			get
			{
				base.GetControlDetails();
				return this.boolDetails.fValue == 1;
			}
			set
			{
				this.boolDetails.fValue = (value ? 1 : 0);
				this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(Marshal.SizeOf(this.boolDetails));
				Marshal.StructureToPtr(this.boolDetails, this.mixerControlDetails.paDetails, false);
				MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
				Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
			}
		}

		// Token: 0x040005E3 RID: 1507
		private MixerInterop.MIXERCONTROLDETAILS_BOOLEAN boolDetails;
	}
}
