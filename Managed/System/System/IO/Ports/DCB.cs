using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x0200053E RID: 1342
	[StructLayout(LayoutKind.Sequential)]
	internal class DCB
	{
		// Token: 0x06002B64 RID: 11108 RVA: 0x000944EC File Offset: 0x000926EC
		public void SetValues(int baud_rate, Parity parity, int byte_size, StopBits sb, Handshake hs)
		{
			switch (sb)
			{
			case StopBits.One:
				this.stop_bits = 0;
				break;
			case StopBits.Two:
				this.stop_bits = 2;
				break;
			case StopBits.OnePointFive:
				this.stop_bits = 1;
				break;
			}
			this.baud_rate = baud_rate;
			this.parity = (byte)parity;
			this.byte_size = (byte)byte_size;
			this.flags &= -8965;
			switch (hs)
			{
			case Handshake.None:
				break;
			case Handshake.XOnXOff:
				this.flags |= 768;
				return;
			case Handshake.RequestToSend:
				this.flags |= 8196;
				return;
			case Handshake.RequestToSendXOnXOff:
				this.flags |= 8964;
				break;
			default:
				return;
			}
		}

		// Token: 0x0400178F RID: 6031
		public int dcb_length;

		// Token: 0x04001790 RID: 6032
		public int baud_rate;

		// Token: 0x04001791 RID: 6033
		public int flags;

		// Token: 0x04001792 RID: 6034
		public short w_reserved;

		// Token: 0x04001793 RID: 6035
		public short xon_lim;

		// Token: 0x04001794 RID: 6036
		public short xoff_lim;

		// Token: 0x04001795 RID: 6037
		public byte byte_size;

		// Token: 0x04001796 RID: 6038
		public byte parity;

		// Token: 0x04001797 RID: 6039
		public byte stop_bits;

		// Token: 0x04001798 RID: 6040
		public byte xon_char;

		// Token: 0x04001799 RID: 6041
		public byte xoff_char;

		// Token: 0x0400179A RID: 6042
		public byte error_char;

		// Token: 0x0400179B RID: 6043
		public byte eof_char;

		// Token: 0x0400179C RID: 6044
		public byte evt_char;

		// Token: 0x0400179D RID: 6045
		public short w_reserved1;

		// Token: 0x0400179E RID: 6046
		private const int fOutxCtsFlow = 4;

		// Token: 0x0400179F RID: 6047
		private const int fOutX = 256;

		// Token: 0x040017A0 RID: 6048
		private const int fInX = 512;

		// Token: 0x040017A1 RID: 6049
		private const int fRtsControl2 = 8192;
	}
}
