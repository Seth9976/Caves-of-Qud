using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x020000FC RID: 252
	public class MixerLine
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x00012668 File Offset: 0x00010868
		public MixerLine(IntPtr mixerHandle, int destinationIndex, MixerFlags mixerHandleType)
		{
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.mixerLine = default(MixerInterop.MIXERLINE);
			this.mixerLine.cbStruct = Marshal.SizeOf(this.mixerLine);
			this.mixerLine.dwDestination = destinationIndex;
			MmException.Try(MixerInterop.mixerGetLineInfo(mixerHandle, ref this.mixerLine, mixerHandleType), "mixerGetLineInfo");
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000126D4 File Offset: 0x000108D4
		public MixerLine(IntPtr mixerHandle, int destinationIndex, int sourceIndex, MixerFlags mixerHandleType)
		{
			this.mixerHandle = mixerHandle;
			this.mixerHandleType = mixerHandleType;
			this.mixerLine = default(MixerInterop.MIXERLINE);
			this.mixerLine.cbStruct = Marshal.SizeOf(this.mixerLine);
			this.mixerLine.dwDestination = destinationIndex;
			this.mixerLine.dwSource = sourceIndex;
			MmException.Try(MixerInterop.mixerGetLineInfo(mixerHandle, ref this.mixerLine, mixerHandleType | MixerFlags.ListText), "mixerGetLineInfo");
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00012750 File Offset: 0x00010950
		public static int GetMixerIdForWaveIn(int waveInDevice)
		{
			int num = -1;
			MmException.Try(MixerInterop.mixerGetID((IntPtr)waveInDevice, out num, MixerFlags.WaveIn), "mixerGetID");
			return num;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0001277C File Offset: 0x0001097C
		public string Name
		{
			get
			{
				return this.mixerLine.szName;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00012789 File Offset: 0x00010989
		public string ShortName
		{
			get
			{
				return this.mixerLine.szShortName;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00012796 File Offset: 0x00010996
		public int LineId
		{
			get
			{
				return this.mixerLine.dwLineID;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x000127A3 File Offset: 0x000109A3
		public MixerLineComponentType ComponentType
		{
			get
			{
				return this.mixerLine.dwComponentType;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000127B0 File Offset: 0x000109B0
		public string TypeDescription
		{
			get
			{
				MixerLineComponentType dwComponentType = this.mixerLine.dwComponentType;
				switch (dwComponentType)
				{
				case MixerLineComponentType.DestinationUndefined:
					return "Undefined Destination";
				case MixerLineComponentType.DestinationDigital:
					return "Digital Destination";
				case MixerLineComponentType.DestinationLine:
					return "Line Level Destination";
				case MixerLineComponentType.DestinationMonitor:
					return "Monitor Destination";
				case MixerLineComponentType.DestinationSpeakers:
					return "Speakers Destination";
				case MixerLineComponentType.DestinationHeadphones:
					return "Headphones Destination";
				case MixerLineComponentType.DestinationTelephone:
					return "Telephone Destination";
				case MixerLineComponentType.DestinationWaveIn:
					return "Wave Input Destination";
				case MixerLineComponentType.DestinationVoiceIn:
					return "Voice Recognition Destination";
				default:
					switch (dwComponentType)
					{
					case MixerLineComponentType.SourceUndefined:
						return "Undefined Source";
					case MixerLineComponentType.SourceDigital:
						return "Digital Source";
					case MixerLineComponentType.SourceLine:
						return "Line Level Source";
					case MixerLineComponentType.SourceMicrophone:
						return "Microphone Source";
					case MixerLineComponentType.SourceSynthesizer:
						return "Synthesizer Source";
					case MixerLineComponentType.SourceCompactDisc:
						return "Compact Disk Source";
					case MixerLineComponentType.SourceTelephone:
						return "Telephone Source";
					case MixerLineComponentType.SourcePcSpeaker:
						return "PC Speaker Source";
					case MixerLineComponentType.SourceWaveOut:
						return "Wave Out Source";
					case MixerLineComponentType.SourceAuxiliary:
						return "Auxiliary Source";
					case MixerLineComponentType.SourceAnalog:
						return "Analog Source";
					default:
						return "Invalid Component Type";
					}
					break;
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x000128AA File Offset: 0x00010AAA
		public int Channels
		{
			get
			{
				return this.mixerLine.cChannels;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x000128B7 File Offset: 0x00010AB7
		public int SourceCount
		{
			get
			{
				return this.mixerLine.cConnections;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000128C4 File Offset: 0x00010AC4
		public int ControlsCount
		{
			get
			{
				return this.mixerLine.cControls;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x000128D1 File Offset: 0x00010AD1
		public bool IsActive
		{
			get
			{
				return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_ACTIVE) != (MixerInterop.MIXERLINE_LINEF)0;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000128E6 File Offset: 0x00010AE6
		public bool IsDisconnected
		{
			get
			{
				return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_DISCONNECTED) != (MixerInterop.MIXERLINE_LINEF)0;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000128FF File Offset: 0x00010AFF
		public bool IsSource
		{
			get
			{
				return (this.mixerLine.fdwLine & MixerInterop.MIXERLINE_LINEF.MIXERLINE_LINEF_SOURCE) != (MixerInterop.MIXERLINE_LINEF)0;
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00012918 File Offset: 0x00010B18
		public MixerLine GetSource(int sourceIndex)
		{
			if (sourceIndex < 0 || sourceIndex >= this.SourceCount)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			return new MixerLine(this.mixerHandle, this.mixerLine.dwDestination, sourceIndex, this.mixerHandleType);
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001294F File Offset: 0x00010B4F
		public IEnumerable<MixerControl> Controls
		{
			get
			{
				return MixerControl.GetMixerControls(this.mixerHandle, this, this.mixerHandleType);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00012A6C File Offset: 0x00010C6C
		public IEnumerable<MixerLine> Sources
		{
			get
			{
				for (int source = 0; source < this.SourceCount; source++)
				{
					yield return this.GetSource(source);
				}
				yield break;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00012A89 File Offset: 0x00010C89
		public string TargetName
		{
			get
			{
				return this.mixerLine.szPname;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00012A98 File Offset: 0x00010C98
		public override string ToString()
		{
			return string.Format("{0} {1} ({2} controls, ID={3})", new object[]
			{
				this.Name,
				this.TypeDescription,
				this.ControlsCount,
				this.mixerLine.dwLineID
			});
		}

		// Token: 0x04000621 RID: 1569
		private MixerInterop.MIXERLINE mixerLine;

		// Token: 0x04000622 RID: 1570
		private IntPtr mixerHandle;

		// Token: 0x04000623 RID: 1571
		private MixerFlags mixerHandleType;
	}
}
