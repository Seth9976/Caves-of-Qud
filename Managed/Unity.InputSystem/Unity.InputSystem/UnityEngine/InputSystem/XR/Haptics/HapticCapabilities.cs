using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000077 RID: 119
	public struct HapticCapabilities
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x00039289 File Offset: 0x00037489
		public HapticCapabilities(uint numChannels, bool supportsImpulse, bool supportsBuffer, uint frequencyHz, uint maxBufferSize, uint optimalBufferSize)
		{
			this.numChannels = numChannels;
			this.supportsImpulse = supportsImpulse;
			this.supportsBuffer = supportsBuffer;
			this.frequencyHz = frequencyHz;
			this.maxBufferSize = maxBufferSize;
			this.optimalBufferSize = optimalBufferSize;
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000392B8 File Offset: 0x000374B8
		public HapticCapabilities(uint numChannels, uint frequencyHz, uint maxBufferSize)
		{
			this = new HapticCapabilities(numChannels, false, false, frequencyHz, maxBufferSize, 0U);
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000392C6 File Offset: 0x000374C6
		public readonly uint numChannels { get; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000392CE File Offset: 0x000374CE
		public readonly bool supportsImpulse { get; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000392D6 File Offset: 0x000374D6
		public readonly bool supportsBuffer { get; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000392DE File Offset: 0x000374DE
		public readonly uint frequencyHz { get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000392E6 File Offset: 0x000374E6
		public readonly uint maxBufferSize { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000392EE File Offset: 0x000374EE
		public readonly uint optimalBufferSize { get; }
	}
}
