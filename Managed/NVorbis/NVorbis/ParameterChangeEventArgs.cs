using System;

namespace NVorbis
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public class ParameterChangeEventArgs : EventArgs
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x000054EE File Offset: 0x000036EE
		public ParameterChangeEventArgs(DataPacket firstPacket)
		{
			this.FirstPacket = firstPacket;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000054FD File Offset: 0x000036FD
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00005505 File Offset: 0x00003705
		public DataPacket FirstPacket { get; private set; }
	}
}
