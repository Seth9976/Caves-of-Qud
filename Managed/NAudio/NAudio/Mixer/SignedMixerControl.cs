using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x0200010C RID: 268
	public class SignedMixerControl : MixerControl
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x00012AF2 File Offset: 0x00010CF2
		internal SignedMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00012B29 File Offset: 0x00010D29
		protected override void GetDetails(IntPtr pDetails)
		{
			this.signedDetails = (MixerInterop.MIXERCONTROLDETAILS_SIGNED)Marshal.PtrToStructure(this.mixerControlDetails.paDetails, typeof(MixerInterop.MIXERCONTROLDETAILS_SIGNED));
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00012B50 File Offset: 0x00010D50
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00012B64 File Offset: 0x00010D64
		public int Value
		{
			get
			{
				base.GetControlDetails();
				return this.signedDetails.lValue;
			}
			set
			{
				this.signedDetails.lValue = value;
				this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(Marshal.SizeOf(this.signedDetails));
				Marshal.StructureToPtr(this.signedDetails, this.mixerControlDetails.paDetails, false);
				MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
				Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00012BEA File Offset: 0x00010DEA
		public int MinValue
		{
			get
			{
				return this.mixerControl.Bounds.minimum;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x00012BFC File Offset: 0x00010DFC
		public int MaxValue
		{
			get
			{
				return this.mixerControl.Bounds.maximum;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x00012C0E File Offset: 0x00010E0E
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00012C37 File Offset: 0x00010E37
		public double Percent
		{
			get
			{
				return 100.0 * (double)(this.Value - this.MinValue) / (double)(this.MaxValue - this.MinValue);
			}
			set
			{
				this.Value = (int)((double)this.MinValue + value / 100.0 * (double)(this.MaxValue - this.MinValue));
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00012C62 File Offset: 0x00010E62
		public override string ToString()
		{
			return string.Format("{0} {1}%", base.ToString(), this.Percent);
		}

		// Token: 0x0400069B RID: 1691
		private MixerInterop.MIXERCONTROLDETAILS_SIGNED signedDetails;
	}
}
