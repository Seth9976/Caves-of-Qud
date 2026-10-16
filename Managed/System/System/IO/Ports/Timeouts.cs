using System;
using System.Runtime.InteropServices;

namespace System.IO.Ports
{
	// Token: 0x0200053F RID: 1343
	[StructLayout(LayoutKind.Sequential)]
	internal class Timeouts
	{
		// Token: 0x06002B66 RID: 11110 RVA: 0x000945A2 File Offset: 0x000927A2
		public Timeouts(int read_timeout, int write_timeout)
		{
			this.SetValues(read_timeout, write_timeout);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000945B2 File Offset: 0x000927B2
		public void SetValues(int read_timeout, int write_timeout)
		{
			this.ReadIntervalTimeout = uint.MaxValue;
			this.ReadTotalTimeoutMultiplier = uint.MaxValue;
			this.ReadTotalTimeoutConstant = (uint)((read_timeout == -1) ? (-2) : read_timeout);
			this.WriteTotalTimeoutMultiplier = 0U;
			this.WriteTotalTimeoutConstant = (uint)((write_timeout == -1) ? (-1) : write_timeout);
		}

		// Token: 0x040017A2 RID: 6050
		public uint ReadIntervalTimeout;

		// Token: 0x040017A3 RID: 6051
		public uint ReadTotalTimeoutMultiplier;

		// Token: 0x040017A4 RID: 6052
		public uint ReadTotalTimeoutConstant;

		// Token: 0x040017A5 RID: 6053
		public uint WriteTotalTimeoutMultiplier;

		// Token: 0x040017A6 RID: 6054
		public uint WriteTotalTimeoutConstant;

		// Token: 0x040017A7 RID: 6055
		public const uint MaxDWord = 4294967295U;
	}
}
