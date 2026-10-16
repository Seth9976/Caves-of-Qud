using System;

namespace NVorbis
{
	// Token: 0x02000007 RID: 7
	public interface IPacketProvider : IDisposable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45
		int StreamSerial { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46
		bool CanSeek { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47
		long ContainerBits { get; }

		// Token: 0x06000030 RID: 48
		int GetTotalPageCount();

		// Token: 0x06000031 RID: 49
		DataPacket GetNextPacket();

		// Token: 0x06000032 RID: 50
		DataPacket PeekNextPacket();

		// Token: 0x06000033 RID: 51
		DataPacket GetPacket(int packetIndex);

		// Token: 0x06000034 RID: 52
		long GetGranuleCount();

		// Token: 0x06000035 RID: 53
		DataPacket FindPacket(long granulePos, Func<DataPacket, DataPacket, int> packetGranuleCountCallback);

		// Token: 0x06000036 RID: 54
		void SeekToPacket(DataPacket packet, int preRoll);

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000037 RID: 55
		// (remove) Token: 0x06000038 RID: 56
		event EventHandler<ParameterChangeEventArgs> ParameterChange;
	}
}
