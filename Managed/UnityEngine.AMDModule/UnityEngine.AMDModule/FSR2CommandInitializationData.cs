using System;

namespace UnityEngine.AMD
{
	// Token: 0x02000005 RID: 5
	public struct FSR2CommandInitializationData
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		public void SetFlag(FfxFsr2InitializationFlags flag, bool value)
		{
			if (value)
			{
				this.ffxFsrFlags |= flag;
			}
			else
			{
				this.ffxFsrFlags &= ~flag;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002088 File Offset: 0x00000288
		public bool GetFlag(FfxFsr2InitializationFlags flag)
		{
			return (this.ffxFsrFlags & flag) > (FfxFsr2InitializationFlags)0;
		}

		// Token: 0x0400000F RID: 15
		public uint maxRenderSizeWidth;

		// Token: 0x04000010 RID: 16
		public uint maxRenderSizeHeight;

		// Token: 0x04000011 RID: 17
		public uint displaySizeWidth;

		// Token: 0x04000012 RID: 18
		public uint displaySizeHeight;

		// Token: 0x04000013 RID: 19
		public FfxFsr2InitializationFlags ffxFsrFlags;

		// Token: 0x04000014 RID: 20
		internal uint featureSlot;
	}
}
