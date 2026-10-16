using System;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x0200010D RID: 269
	public class UnsignedMixerControl : MixerControl
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x00012C7F File Offset: 0x00010E7F
		internal UnsignedMixerControl(MixerInterop.MIXERCONTROL mixerControl, IntPtr mixerHandle, MixerFlags mixerHandleType, int nChannels)
		{
			this.mixerControl = mixerControl;
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.nChannels = nChannels;
			this.mixerControlDetails = default(MixerInterop.MIXERCONTROLDETAILS);
			base.GetControlDetails();
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00012CB8 File Offset: 0x00010EB8
		protected override void GetDetails(IntPtr pDetails)
		{
			this.unsignedDetails = new MixerInterop.MIXERCONTROLDETAILS_UNSIGNED[this.nChannels];
			for (int i = 0; i < this.nChannels; i++)
			{
				this.unsignedDetails[i] = (MixerInterop.MIXERCONTROLDETAILS_UNSIGNED)Marshal.PtrToStructure(this.mixerControlDetails.paDetails, typeof(MixerInterop.MIXERCONTROLDETAILS_UNSIGNED));
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00012D17 File Offset: 0x00010F17
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00012D30 File Offset: 0x00010F30
		public uint Value
		{
			get
			{
				base.GetControlDetails();
				return this.unsignedDetails[0].dwValue;
			}
			set
			{
				int num = Marshal.SizeOf(this.unsignedDetails[0]);
				this.mixerControlDetails.paDetails = Marshal.AllocHGlobal(num * this.nChannels);
				for (int i = 0; i < this.nChannels; i++)
				{
					this.unsignedDetails[i].dwValue = value;
					long num2 = this.mixerControlDetails.paDetails.ToInt64() + (long)(num * i);
					Marshal.StructureToPtr(this.unsignedDetails[i], (IntPtr)num2, false);
				}
				MmException.Try(MixerInterop.mixerSetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType), "mixerSetControlDetails");
				Marshal.FreeHGlobal(this.mixerControlDetails.paDetails);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00012DFD File Offset: 0x00010FFD
		public uint MinValue
		{
			get
			{
				return (uint)this.mixerControl.Bounds.minimum;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00012E0F File Offset: 0x0001100F
		public uint MaxValue
		{
			get
			{
				return (uint)this.mixerControl.Bounds.maximum;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00012E21 File Offset: 0x00011021
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00012E4C File Offset: 0x0001104C
		public double Percent
		{
			get
			{
				return 100.0 * (this.Value - this.MinValue) / (this.MaxValue - this.MinValue);
			}
			set
			{
				this.Value = (uint)(this.MinValue + value / 100.0 * (this.MaxValue - this.MinValue));
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00012E79 File Offset: 0x00011079
		public override string ToString()
		{
			return string.Format("{0} {1}%", base.ToString(), this.Percent);
		}

		// Token: 0x0400069C RID: 1692
		private MixerInterop.MIXERCONTROLDETAILS_UNSIGNED[] unsignedDetails;
	}
}
