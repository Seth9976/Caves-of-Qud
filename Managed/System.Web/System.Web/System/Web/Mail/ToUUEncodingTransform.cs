using System;
using System.Security.Cryptography;

namespace System.Web.Mail
{
	// Token: 0x020000FF RID: 255
	internal class ToUUEncodingTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00024158 File Offset: 0x00022358
		public int InputBlockSize
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0002415C File Offset: 0x0002235C
		public int OutputBlockSize
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000D6B RID: 3435 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00024160 File Offset: 0x00022360
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			outputBuffer[0] = 77;
			for (int i = 0; i < 15; i++)
			{
				this.TransformTriplet(inputBuffer, inputOffset + i * 3, 3, outputBuffer, outputOffset + i * 4 + 1);
			}
			return this.OutputBlockSize;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000241A0 File Offset: 0x000223A0
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount / 3 + 1;
			byte[] array = new byte[num * 3];
			Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			byte[] array2 = new byte[num * 4 + 1];
			array2[0] = (byte)(inputCount + 32);
			for (int i = 0; i < num; i++)
			{
				this.TransformTriplet(inputBuffer, inputOffset + i * 3, 3, array2, i * 4 + 1);
			}
			return array2;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x000241FC File Offset: 0x000223FC
		protected int TransformTriplet(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			byte b = inputBuffer[inputOffset];
			byte b2 = inputBuffer[inputOffset + 1];
			byte b3 = inputBuffer[inputOffset + 2];
			outputBuffer[outputOffset] = (byte)(32 + ((b >> 2) & 63));
			outputBuffer[outputOffset + 1] = (byte)(32 + ((((int)b << 4) | ((b2 >> 4) & 15)) & 63));
			outputBuffer[outputOffset + 2] = (byte)(32 + ((((int)b2 << 2) | ((b3 >> 6) & 3)) & 63));
			outputBuffer[outputOffset + 3] = 32 + (b3 & 63);
			for (int i = 0; i < 4; i++)
			{
				if (outputBuffer[outputOffset + i] == 32)
				{
					outputBuffer[outputOffset + i] = 96;
				}
			}
			return 4;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00003946 File Offset: 0x00001B46
		public void Dispose()
		{
		}
	}
}
