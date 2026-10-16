using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ionic.Crc
{
	// Token: 0x0200006F RID: 111
	[ComVisible(true)]
	[ClassInterface(1)]
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000C")]
	public class CRC32
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0002161C File Offset: 0x0001F81C
		public long TotalBytesRead
		{
			get
			{
				return this._TotalBytesRead;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00021624 File Offset: 0x0001F824
		public int Crc32Result
		{
			get
			{
				return (int)(~(int)this._register);
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0002162D File Offset: 0x0001F82D
		public int GetCrc32(Stream input)
		{
			return this.GetCrc32AndCopy(input, null);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00021638 File Offset: 0x0001F838
		public int GetCrc32AndCopy(Stream input, Stream output)
		{
			if (input == null)
			{
				throw new Exception("The input stream must not be null.");
			}
			byte[] array = new byte[8192];
			int num = 8192;
			this._TotalBytesRead = 0L;
			int i = input.Read(array, 0, num);
			if (output != null)
			{
				output.Write(array, 0, i);
			}
			this._TotalBytesRead += (long)i;
			while (i > 0)
			{
				this.SlurpBlock(array, 0, i);
				i = input.Read(array, 0, num);
				if (output != null)
				{
					output.Write(array, 0, i);
				}
				this._TotalBytesRead += (long)i;
			}
			return (int)(~(int)this._register);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000216CC File Offset: 0x0001F8CC
		public int ComputeCrc32(int W, byte B)
		{
			return this._InternalComputeCrc32((uint)W, B);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000216D6 File Offset: 0x0001F8D6
		internal int _InternalComputeCrc32(uint W, byte B)
		{
			return (int)(this.crc32Table[(int)((UIntPtr)((W ^ (uint)B) & 255U))] ^ (W >> 8));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000216F0 File Offset: 0x0001F8F0
		public void SlurpBlock(byte[] block, int offset, int count)
		{
			if (block == null)
			{
				throw new Exception("The data buffer must not be null.");
			}
			for (int i = 0; i < count; i++)
			{
				int num = offset + i;
				byte b = block[num];
				if (this.reverseBits)
				{
					uint num2 = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)((UIntPtr)num2)];
				}
				else
				{
					uint num3 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)((UIntPtr)num3)];
				}
			}
			this._TotalBytesRead += (long)count;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00021788 File Offset: 0x0001F988
		public void UpdateCRC(byte b)
		{
			if (this.reverseBits)
			{
				uint num = (this._register >> 24) ^ (uint)b;
				this._register = (this._register << 8) ^ this.crc32Table[(int)((UIntPtr)num)];
				return;
			}
			uint num2 = (this._register & 255U) ^ (uint)b;
			this._register = (this._register >> 8) ^ this.crc32Table[(int)((UIntPtr)num2)];
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000217EC File Offset: 0x0001F9EC
		public void UpdateCRC(byte b, int n)
		{
			while (n-- > 0)
			{
				if (this.reverseBits)
				{
					uint num = (this._register >> 24) ^ (uint)b;
					this._register = (this._register << 8) ^ this.crc32Table[(int)((UIntPtr)((num >= 0U) ? num : (num + 256U)))];
				}
				else
				{
					uint num2 = (this._register & 255U) ^ (uint)b;
					this._register = (this._register >> 8) ^ this.crc32Table[(int)((UIntPtr)((num2 >= 0U) ? num2 : (num2 + 256U)))];
				}
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00021874 File Offset: 0x0001FA74
		private static uint ReverseBits(uint data)
		{
			uint num = ((data & 1431655765U) << 1) | ((data >> 1) & 1431655765U);
			num = ((num & 858993459U) << 2) | ((num >> 2) & 858993459U);
			num = ((num & 252645135U) << 4) | ((num >> 4) & 252645135U);
			return (num << 24) | ((num & 65280U) << 8) | ((num >> 8) & 65280U) | (num >> 24);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000218E0 File Offset: 0x0001FAE0
		private static byte ReverseBits(byte data)
		{
			uint num = (uint)data * 131586U;
			uint num2 = 17055760U;
			uint num3 = num & num2;
			uint num4 = (num << 2) & (num2 << 1);
			return (byte)(16781313U * (num3 + num4) >> 24);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00021914 File Offset: 0x0001FB14
		private void GenerateLookupTable()
		{
			this.crc32Table = new uint[256];
			byte b = 0;
			do
			{
				uint num = (uint)b;
				for (byte b2 = 8; b2 > 0; b2 -= 1)
				{
					if ((num & 1U) == 1U)
					{
						num = (num >> 1) ^ this.dwPolynomial;
					}
					else
					{
						num >>= 1;
					}
				}
				if (this.reverseBits)
				{
					this.crc32Table[(int)CRC32.ReverseBits(b)] = CRC32.ReverseBits(num);
				}
				else
				{
					this.crc32Table[(int)b] = num;
				}
				b += 1;
			}
			while (b != 0);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00021988 File Offset: 0x0001FB88
		private uint gf2_matrix_times(uint[] matrix, uint vec)
		{
			uint num = 0U;
			int num2 = 0;
			while (vec != 0U)
			{
				if ((vec & 1U) == 1U)
				{
					num ^= matrix[num2];
				}
				vec >>= 1;
				num2++;
			}
			return num;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000219B4 File Offset: 0x0001FBB4
		private void gf2_matrix_square(uint[] square, uint[] mat)
		{
			for (int i = 0; i < 32; i++)
			{
				square[i] = this.gf2_matrix_times(mat, mat[i]);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000219DC File Offset: 0x0001FBDC
		public void Combine(int crc, int length)
		{
			uint[] array = new uint[32];
			uint[] array2 = new uint[32];
			if (length == 0)
			{
				return;
			}
			uint num = ~this._register;
			array2[0] = this.dwPolynomial;
			uint num2 = 1U;
			for (int i = 1; i < 32; i++)
			{
				array2[i] = num2;
				num2 <<= 1;
			}
			this.gf2_matrix_square(array, array2);
			this.gf2_matrix_square(array2, array);
			uint num3 = (uint)length;
			do
			{
				this.gf2_matrix_square(array, array2);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array, num);
				}
				num3 >>= 1;
				if (num3 == 0U)
				{
					break;
				}
				this.gf2_matrix_square(array2, array);
				if ((num3 & 1U) == 1U)
				{
					num = this.gf2_matrix_times(array2, num);
				}
				num3 >>= 1;
			}
			while (num3 != 0U);
			num ^= (uint)crc;
			this._register = ~num;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00021A93 File Offset: 0x0001FC93
		public CRC32()
			: this(false)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00021A9C File Offset: 0x0001FC9C
		public CRC32(bool reverseBits)
			: this(-306674912, reverseBits)
		{
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00021AAA File Offset: 0x0001FCAA
		public CRC32(int polynomial, bool reverseBits)
		{
			this.reverseBits = reverseBits;
			this.dwPolynomial = (uint)polynomial;
			this.GenerateLookupTable();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00021ACD File Offset: 0x0001FCCD
		public void Reset()
		{
			this._register = uint.MaxValue;
		}

		// Token: 0x040003D1 RID: 977
		private const int BUFFER_SIZE = 8192;

		// Token: 0x040003D2 RID: 978
		private uint dwPolynomial;

		// Token: 0x040003D3 RID: 979
		private long _TotalBytesRead;

		// Token: 0x040003D4 RID: 980
		private bool reverseBits;

		// Token: 0x040003D5 RID: 981
		private uint[] crc32Table;

		// Token: 0x040003D6 RID: 982
		private uint _register = uint.MaxValue;
	}
}
