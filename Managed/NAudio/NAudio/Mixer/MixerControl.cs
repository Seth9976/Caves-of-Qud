using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x020000F3 RID: 243
	public abstract class MixerControl
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x00011B30 File Offset: 0x0000FD30
		public static IList<MixerControl> GetMixerControls(IntPtr mixerHandle, MixerLine mixerLine, MixerFlags mixerHandleType)
		{
			List<MixerControl> list = new List<MixerControl>();
			if (mixerLine.ControlsCount > 0)
			{
				int num = Marshal.SizeOf(typeof(MixerInterop.MIXERCONTROL));
				MixerInterop.MIXERLINECONTROLS mixerlinecontrols = default(MixerInterop.MIXERLINECONTROLS);
				IntPtr intPtr = Marshal.AllocHGlobal(num * mixerLine.ControlsCount);
				mixerlinecontrols.cbStruct = Marshal.SizeOf(mixerlinecontrols);
				mixerlinecontrols.dwLineID = mixerLine.LineId;
				mixerlinecontrols.cControls = mixerLine.ControlsCount;
				mixerlinecontrols.pamxctrl = intPtr;
				mixerlinecontrols.cbmxctrl = Marshal.SizeOf(typeof(MixerInterop.MIXERCONTROL));
				try
				{
					MmResult mmResult = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerlinecontrols, mixerHandleType);
					if (mmResult != MmResult.NoError)
					{
						throw new MmException(mmResult, "mixerGetLineControls");
					}
					for (int i = 0; i < mixerlinecontrols.cControls; i++)
					{
						long num2 = intPtr.ToInt64() + (long)(num * i);
						MixerInterop.MIXERCONTROL mixercontrol = (MixerInterop.MIXERCONTROL)Marshal.PtrToStructure((IntPtr)num2, typeof(MixerInterop.MIXERCONTROL));
						MixerControl mixerControl = MixerControl.GetMixerControl(mixerHandle, mixerLine.LineId, mixercontrol.dwControlID, mixerLine.Channels, mixerHandleType);
						list.Add(mixerControl);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return list;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00011C5C File Offset: 0x0000FE5C
		public static MixerControl GetMixerControl(IntPtr mixerHandle, int nLineID, int controlId, int nChannels, MixerFlags mixerFlags)
		{
			MixerInterop.MIXERLINECONTROLS mixerlinecontrols = default(MixerInterop.MIXERLINECONTROLS);
			MixerInterop.MIXERCONTROL mixercontrol = default(MixerInterop.MIXERCONTROL);
			IntPtr intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(mixercontrol));
			mixerlinecontrols.cbStruct = Marshal.SizeOf(mixerlinecontrols);
			mixerlinecontrols.cControls = 1;
			mixerlinecontrols.dwControlID = controlId;
			mixerlinecontrols.cbmxctrl = Marshal.SizeOf(mixercontrol);
			mixerlinecontrols.pamxctrl = intPtr;
			mixerlinecontrols.dwLineID = nLineID;
			MmResult mmResult = MixerInterop.mixerGetLineControls(mixerHandle, ref mixerlinecontrols, MixerFlags.ListText | mixerFlags);
			if (mmResult != MmResult.NoError)
			{
				Marshal.FreeCoTaskMem(intPtr);
				throw new MmException(mmResult, "mixerGetLineControls");
			}
			mixercontrol = (MixerInterop.MIXERCONTROL)Marshal.PtrToStructure(mixerlinecontrols.pamxctrl, typeof(MixerInterop.MIXERCONTROL));
			Marshal.FreeCoTaskMem(intPtr);
			if (MixerControl.IsControlBoolean(mixercontrol.dwControlType))
			{
				return new BooleanMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlSigned(mixercontrol.dwControlType))
			{
				return new SignedMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlUnsigned(mixercontrol.dwControlType))
			{
				return new UnsignedMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlListText(mixercontrol.dwControlType))
			{
				return new ListTextMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			if (MixerControl.IsControlCustom(mixercontrol.dwControlType))
			{
				return new CustomMixerControl(mixercontrol, mixerHandle, mixerFlags, nChannels);
			}
			throw new InvalidOperationException(string.Format("Unknown mixer control type {0}", mixercontrol.dwControlType));
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00011DAC File Offset: 0x0000FFAC
		protected void GetControlDetails()
		{
			this.mixerControlDetails.cbStruct = Marshal.SizeOf(this.mixerControlDetails);
			this.mixerControlDetails.dwControlID = this.mixerControl.dwControlID;
			if (this.IsCustom)
			{
				this.mixerControlDetails.cChannels = 0;
			}
			else if ((this.mixerControl.fdwControl & 1U) != 0U)
			{
				this.mixerControlDetails.cChannels = 1;
			}
			else
			{
				this.mixerControlDetails.cChannels = this.nChannels;
			}
			if ((this.mixerControl.fdwControl & 2U) != 0U)
			{
				this.mixerControlDetails.hwndOwner = (IntPtr)((long)((ulong)this.mixerControl.cMultipleItems));
			}
			else if (this.IsCustom)
			{
				this.mixerControlDetails.hwndOwner = IntPtr.Zero;
			}
			else
			{
				this.mixerControlDetails.hwndOwner = IntPtr.Zero;
			}
			if (this.IsBoolean)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_BOOLEAN));
			}
			else if (this.IsListText)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_LISTTEXT));
			}
			else if (this.IsSigned)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_SIGNED));
			}
			else if (this.IsUnsigned)
			{
				this.mixerControlDetails.cbDetails = Marshal.SizeOf(default(MixerInterop.MIXERCONTROLDETAILS_UNSIGNED));
			}
			else
			{
				this.mixerControlDetails.cbDetails = this.mixerControl.Metrics.customData;
			}
			int num = this.mixerControlDetails.cbDetails * this.mixerControlDetails.cChannels;
			if ((this.mixerControl.fdwControl & 2U) != 0U)
			{
				num *= (int)this.mixerControl.cMultipleItems;
			}
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			this.mixerControlDetails.paDetails = intPtr;
			MmResult mmResult = MixerInterop.mixerGetControlDetails(this.mixerHandle, ref this.mixerControlDetails, this.mixerHandleType);
			if (mmResult == MmResult.NoError)
			{
				this.GetDetails(this.mixerControlDetails.paDetails);
			}
			Marshal.FreeCoTaskMem(intPtr);
			if (mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "mixerGetControlDetails");
			}
		}

		// Token: 0x0600057C RID: 1404
		protected abstract void GetDetails(IntPtr pDetails);

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00011FCF File Offset: 0x000101CF
		public string Name
		{
			get
			{
				return this.mixerControl.szName;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00011FDC File Offset: 0x000101DC
		public MixerControlType ControlType
		{
			get
			{
				return this.mixerControl.dwControlType;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00011FEC File Offset: 0x000101EC
		private static bool IsControlBoolean(MixerControlType controlType)
		{
			if (controlType <= MixerControlType.StereoEnhance)
			{
				if (controlType != MixerControlType.BooleanMeter)
				{
					switch (controlType)
					{
					case MixerControlType.Boolean:
					case MixerControlType.OnOff:
					case MixerControlType.Mute:
					case MixerControlType.Mono:
					case MixerControlType.Loudness:
					case MixerControlType.StereoEnhance:
						break;
					default:
						return false;
					}
				}
			}
			else if (controlType != MixerControlType.Button)
			{
				switch (controlType)
				{
				case MixerControlType.SingleSelect:
				case MixerControlType.Mux:
					break;
				default:
					switch (controlType)
					{
					case MixerControlType.MultipleSelect:
					case MixerControlType.Mixer:
						break;
					default:
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00012066 File Offset: 0x00010266
		public bool IsBoolean
		{
			get
			{
				return MixerControl.IsControlBoolean(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00012078 File Offset: 0x00010278
		private static bool IsControlListText(MixerControlType controlType)
		{
			if (controlType != MixerControlType.Equalizer)
			{
				switch (controlType)
				{
				case MixerControlType.SingleSelect:
				case MixerControlType.Mux:
					break;
				default:
					switch (controlType)
					{
					case MixerControlType.MultipleSelect:
					case MixerControlType.Mixer:
						break;
					default:
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x000120BC File Offset: 0x000102BC
		public bool IsListText
		{
			get
			{
				return MixerControl.IsControlListText(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000120D0 File Offset: 0x000102D0
		private static bool IsControlSigned(MixerControlType controlType)
		{
			if (controlType <= MixerControlType.Signed)
			{
				switch (controlType)
				{
				case MixerControlType.SignedMeter:
				case MixerControlType.PeakMeter:
					break;
				default:
					if (controlType != MixerControlType.Signed)
					{
						return false;
					}
					break;
				}
			}
			else if (controlType != MixerControlType.Decibels)
			{
				switch (controlType)
				{
				case MixerControlType.Slider:
				case MixerControlType.Pan:
				case MixerControlType.QSoundPan:
					break;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001212A File Offset: 0x0001032A
		public bool IsSigned
		{
			get
			{
				return MixerControl.IsControlSigned(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001213C File Offset: 0x0001033C
		private static bool IsControlUnsigned(MixerControlType controlType)
		{
			if (controlType <= MixerControlType.Percent)
			{
				if (controlType != MixerControlType.UnsignedMeter && controlType != MixerControlType.Unsigned && controlType != MixerControlType.Percent)
				{
					return false;
				}
			}
			else
			{
				switch (controlType)
				{
				case MixerControlType.Fader:
				case MixerControlType.Volume:
				case MixerControlType.Bass:
				case MixerControlType.Treble:
				case MixerControlType.Equalizer:
					break;
				default:
					if (controlType != MixerControlType.MicroTime && controlType != MixerControlType.MilliTime)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000121A0 File Offset: 0x000103A0
		public bool IsUnsigned
		{
			get
			{
				return MixerControl.IsControlUnsigned(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000121B2 File Offset: 0x000103B2
		private static bool IsControlCustom(MixerControlType controlType)
		{
			return controlType == MixerControlType.Custom;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000121B8 File Offset: 0x000103B8
		public bool IsCustom
		{
			get
			{
				return MixerControl.IsControlCustom(this.mixerControl.dwControlType);
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000121CA File Offset: 0x000103CA
		public override string ToString()
		{
			return string.Format("{0} {1}", this.Name, this.ControlType);
		}

		// Token: 0x040005DE RID: 1502
		internal MixerInterop.MIXERCONTROL mixerControl;

		// Token: 0x040005DF RID: 1503
		internal MixerInterop.MIXERCONTROLDETAILS mixerControlDetails;

		// Token: 0x040005E0 RID: 1504
		protected IntPtr mixerHandle;

		// Token: 0x040005E1 RID: 1505
		protected int nChannels;

		// Token: 0x040005E2 RID: 1506
		protected MixerFlags mixerHandleType;
	}
}
