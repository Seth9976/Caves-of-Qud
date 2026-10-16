using System;

namespace NAudio.Wave
{
	// Token: 0x020001BC RID: 444
	public class DirectSoundDeviceInfo
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001A99E File Offset: 0x00018B9E
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0001A9A6 File Offset: 0x00018BA6
		public Guid Guid { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001A9AF File Offset: 0x00018BAF
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001A9B7 File Offset: 0x00018BB7
		public string Description { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0001A9C0 File Offset: 0x00018BC0
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		public string ModuleName { get; set; }
	}
}
