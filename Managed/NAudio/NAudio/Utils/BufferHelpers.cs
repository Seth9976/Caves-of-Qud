using System;

namespace NAudio.Utils
{
	// Token: 0x0200010E RID: 270
	public static class BufferHelpers
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00012E96 File Offset: 0x00011096
		public static byte[] Ensure(byte[] buffer, int bytesRequired)
		{
			if (buffer == null || buffer.Length < bytesRequired)
			{
				buffer = new byte[bytesRequired];
			}
			return buffer;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00012EAA File Offset: 0x000110AA
		public static float[] Ensure(float[] buffer, int samplesRequired)
		{
			if (buffer == null || buffer.Length < samplesRequired)
			{
				buffer = new float[samplesRequired];
			}
			return buffer;
		}
	}
}
