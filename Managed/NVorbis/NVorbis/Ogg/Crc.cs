using System;

namespace NVorbis.Ogg
{
	// Token: 0x0200000E RID: 14
	internal class Crc
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004988 File Offset: 0x00002B88
		static Crc()
		{
			for (uint num = 0U; num < 256U; num += 1U)
			{
				uint num2 = num << 24;
				for (int i = 0; i < 8; i++)
				{
					num2 = (num2 << 1) ^ ((num2 >= 2147483648U) ? 79764919U : 0U);
				}
				Crc.crcTable[(int)((UIntPtr)num)] = num2;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000049E3 File Offset: 0x00002BE3
		public Crc()
		{
			this.Reset();
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000049F1 File Offset: 0x00002BF1
		public void Reset()
		{
			this._crc = 0U;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000049FA File Offset: 0x00002BFA
		public void Update(int nextVal)
		{
			checked
			{
				this._crc = (this._crc << 8) ^ Crc.crcTable[(int)((IntPtr)(unchecked((long)nextVal ^ (long)((ulong)(this._crc >> 24)))))];
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004A1F File Offset: 0x00002C1F
		public bool Test(uint checkCrc)
		{
			return this._crc == checkCrc;
		}

		// Token: 0x04000043 RID: 67
		private const uint CRC32_POLY = 79764919U;

		// Token: 0x04000044 RID: 68
		private static uint[] crcTable = new uint[256];

		// Token: 0x04000045 RID: 69
		private uint _crc;
	}
}
