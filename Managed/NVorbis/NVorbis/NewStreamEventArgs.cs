using System;

namespace NVorbis
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class NewStreamEventArgs : EventArgs
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003A68 File Offset: 0x00001C68
		public NewStreamEventArgs(IPacketProvider packetProvider)
		{
			if (packetProvider == null)
			{
				throw new ArgumentNullException("packetProvider");
			}
			this.PacketProvider = packetProvider;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003A85 File Offset: 0x00001C85
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00003A8D File Offset: 0x00001C8D
		public IPacketProvider PacketProvider { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003A96 File Offset: 0x00001C96
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00003A9E File Offset: 0x00001C9E
		public bool IgnoreStream { get; set; }
	}
}
