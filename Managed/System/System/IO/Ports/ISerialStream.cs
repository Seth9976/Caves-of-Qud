using System;

namespace System.IO.Ports
{
	// Token: 0x0200052E RID: 1326
	internal interface ISerialStream : IDisposable
	{
		// Token: 0x06002A97 RID: 10903
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x06002A98 RID: 10904
		void Write(byte[] buffer, int offset, int count);

		// Token: 0x06002A99 RID: 10905
		void SetAttributes(int baud_rate, Parity parity, int data_bits, StopBits sb, Handshake hs);

		// Token: 0x06002A9A RID: 10906
		void DiscardInBuffer();

		// Token: 0x06002A9B RID: 10907
		void DiscardOutBuffer();

		// Token: 0x06002A9C RID: 10908
		SerialSignal GetSignals();

		// Token: 0x06002A9D RID: 10909
		void SetSignal(SerialSignal signal, bool value);

		// Token: 0x06002A9E RID: 10910
		void SetBreakState(bool value);

		// Token: 0x06002A9F RID: 10911
		void Close();

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002AA0 RID: 10912
		int BytesToRead { get; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002AA1 RID: 10913
		int BytesToWrite { get; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002AA2 RID: 10914
		// (set) Token: 0x06002AA3 RID: 10915
		int ReadTimeout { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002AA4 RID: 10916
		// (set) Token: 0x06002AA5 RID: 10917
		int WriteTimeout { get; set; }
	}
}
