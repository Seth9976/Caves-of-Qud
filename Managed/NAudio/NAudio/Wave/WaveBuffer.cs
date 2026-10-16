using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave
{
	// Token: 0x020001BF RID: 447
	[StructLayout(2, Pack = 2)]
	public class WaveBuffer : IWaveBuffer
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x0001B178 File Offset: 0x00019378
		public WaveBuffer(int sizeToAllocateInBytes)
		{
			int num = sizeToAllocateInBytes % 4;
			sizeToAllocateInBytes = ((num == 0) ? sizeToAllocateInBytes : (sizeToAllocateInBytes + 4 - num));
			this.byteBuffer = new byte[sizeToAllocateInBytes];
			this.numberOfBytes = 0;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001B1AF File Offset: 0x000193AF
		public WaveBuffer(byte[] bufferToBoundTo)
		{
			this.BindTo(bufferToBoundTo);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001B1BE File Offset: 0x000193BE
		public void BindTo(byte[] bufferToBoundTo)
		{
			this.byteBuffer = bufferToBoundTo;
			this.numberOfBytes = 0;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001B1CE File Offset: 0x000193CE
		public static implicit operator byte[](WaveBuffer waveBuffer)
		{
			return waveBuffer.byteBuffer;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001B1D6 File Offset: 0x000193D6
		public static implicit operator float[](WaveBuffer waveBuffer)
		{
			return waveBuffer.floatBuffer;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001B1DE File Offset: 0x000193DE
		public static implicit operator int[](WaveBuffer waveBuffer)
		{
			return waveBuffer.intBuffer;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001B1E6 File Offset: 0x000193E6
		public static implicit operator short[](WaveBuffer waveBuffer)
		{
			return waveBuffer.shortBuffer;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001B1EE File Offset: 0x000193EE
		public byte[] ByteBuffer
		{
			get
			{
				return this.byteBuffer;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001B1F6 File Offset: 0x000193F6
		public float[] FloatBuffer
		{
			get
			{
				return this.floatBuffer;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0001B1FE File Offset: 0x000193FE
		public short[] ShortBuffer
		{
			get
			{
				return this.shortBuffer;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0001B206 File Offset: 0x00019406
		public int[] IntBuffer
		{
			get
			{
				return this.intBuffer;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0001B20E File Offset: 0x0001940E
		public int MaxSize
		{
			get
			{
				return this.byteBuffer.Length;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0001B218 File Offset: 0x00019418
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0001B220 File Offset: 0x00019420
		public int ByteBufferCount
		{
			get
			{
				return this.numberOfBytes;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("ByteBufferCount", value, 1);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0001B235 File Offset: 0x00019435
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0001B23F File Offset: 0x0001943F
		public int FloatBufferCount
		{
			get
			{
				return this.numberOfBytes / 4;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("FloatBufferCount", value, 4);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x0001B254 File Offset: 0x00019454
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x0001B25E File Offset: 0x0001945E
		public int ShortBufferCount
		{
			get
			{
				return this.numberOfBytes / 2;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("ShortBufferCount", value, 2);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x0001B273 File Offset: 0x00019473
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x0001B27D File Offset: 0x0001947D
		public int IntBufferCount
		{
			get
			{
				return this.numberOfBytes / 4;
			}
			set
			{
				this.numberOfBytes = this.CheckValidityCount("IntBufferCount", value, 4);
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001B292 File Offset: 0x00019492
		public void Clear()
		{
			Array.Clear(this.byteBuffer, 0, this.byteBuffer.Length);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001B2A8 File Offset: 0x000194A8
		public void Copy(Array destinationArray)
		{
			Array.Copy(this.byteBuffer, destinationArray, this.numberOfBytes);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001B2BC File Offset: 0x000194BC
		private int CheckValidityCount(string argName, int value, int sizeOfValue)
		{
			int num = value * sizeOfValue;
			if (num % 4 != 0)
			{
				throw new ArgumentOutOfRangeException(argName, string.Format("{0} cannot set a count ({1}) that is not 4 bytes aligned ", argName, num));
			}
			if (value < 0 || value > this.byteBuffer.Length / sizeOfValue)
			{
				throw new ArgumentOutOfRangeException(argName, string.Format("{0} cannot set a count that exceed max count {1}", argName, this.byteBuffer.Length / sizeOfValue));
			}
			return num;
		}

		// Token: 0x04000AE3 RID: 2787
		[FieldOffset(0)]
		public int numberOfBytes;

		// Token: 0x04000AE4 RID: 2788
		[FieldOffset(8)]
		private byte[] byteBuffer;

		// Token: 0x04000AE5 RID: 2789
		[FieldOffset(8)]
		private float[] floatBuffer;

		// Token: 0x04000AE6 RID: 2790
		[FieldOffset(8)]
		private short[] shortBuffer;

		// Token: 0x04000AE7 RID: 2791
		[FieldOffset(8)]
		private int[] intBuffer;
	}
}
