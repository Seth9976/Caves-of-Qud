using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NAudio.Mixer
{
	// Token: 0x020000F7 RID: 247
	public class Mixer
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001235A File Offset: 0x0001055A
		public static int NumberOfDevices
		{
			get
			{
				return MixerInterop.mixerGetNumDevs();
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00012364 File Offset: 0x00010564
		public Mixer(int mixerIndex)
		{
			if (mixerIndex < 0 || mixerIndex >= Mixer.NumberOfDevices)
			{
				throw new ArgumentOutOfRangeException("mixerID");
			}
			this.caps = default(MixerInterop.MIXERCAPS);
			MmException.Try(MixerInterop.mixerGetDevCaps((IntPtr)mixerIndex, ref this.caps, Marshal.SizeOf(this.caps)), "mixerGetDevCaps");
			this.mixerHandle = (IntPtr)mixerIndex;
			this.mixerHandleType = MixerFlags.Mixer;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000123D8 File Offset: 0x000105D8
		public int DestinationCount
		{
			get
			{
				return (int)this.caps.cDestinations;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000123E5 File Offset: 0x000105E5
		public string Name
		{
			get
			{
				return this.caps.szPname;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000123F2 File Offset: 0x000105F2
		public Manufacturers Manufacturer
		{
			get
			{
				return (Manufacturers)this.caps.wMid;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x000123FF File Offset: 0x000105FF
		public int ProductID
		{
			get
			{
				return (int)this.caps.wPid;
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001240C File Offset: 0x0001060C
		public MixerLine GetDestination(int destinationIndex)
		{
			if (destinationIndex < 0 || destinationIndex >= this.DestinationCount)
			{
				throw new ArgumentOutOfRangeException("destinationIndex");
			}
			return new MixerLine(this.mixerHandle, destinationIndex, this.mixerHandleType);
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00012540 File Offset: 0x00010740
		public IEnumerable<MixerLine> Destinations
		{
			get
			{
				for (int destination = 0; destination < this.DestinationCount; destination++)
				{
					yield return this.GetDestination(destination);
				}
				yield break;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00012650 File Offset: 0x00010850
		public static IEnumerable<Mixer> Mixers
		{
			get
			{
				for (int device = 0; device < Mixer.NumberOfDevices; device++)
				{
					yield return new Mixer(device);
				}
				yield break;
			}
		}

		// Token: 0x040005E4 RID: 1508
		private MixerInterop.MIXERCAPS caps;

		// Token: 0x040005E5 RID: 1509
		private IntPtr mixerHandle;

		// Token: 0x040005E6 RID: 1510
		private MixerFlags mixerHandleType;
	}
}
