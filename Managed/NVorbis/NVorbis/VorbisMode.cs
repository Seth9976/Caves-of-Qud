using System;

namespace NVorbis
{
	// Token: 0x02000025 RID: 37
	internal class VorbisMode
	{
		// Token: 0x0600011E RID: 286 RVA: 0x000081C0 File Offset: 0x000063C0
		internal static VorbisMode Init(VorbisStreamDecoder vorbis, DataPacket packet)
		{
			VorbisMode vorbisMode = new VorbisMode(vorbis);
			vorbisMode.BlockFlag = packet.ReadBit();
			vorbisMode.WindowType = (int)packet.ReadBits(16);
			vorbisMode.TransformType = (int)packet.ReadBits(16);
			int num = (int)packet.ReadBits(8);
			if (vorbisMode.WindowType != 0 || vorbisMode.TransformType != 0 || num >= vorbis.Maps.Length)
			{
				throw new InvalidDataException();
			}
			vorbisMode.Mapping = vorbis.Maps[num];
			vorbisMode.BlockSize = (vorbisMode.BlockFlag ? vorbis.Block1Size : vorbis.Block0Size);
			if (vorbisMode.BlockFlag)
			{
				vorbisMode._windows = new float[4][];
				vorbisMode._windows[0] = new float[vorbis.Block1Size];
				vorbisMode._windows[1] = new float[vorbis.Block1Size];
				vorbisMode._windows[2] = new float[vorbis.Block1Size];
				vorbisMode._windows[3] = new float[vorbis.Block1Size];
			}
			else
			{
				vorbisMode._windows = new float[1][];
				vorbisMode._windows[0] = new float[vorbis.Block0Size];
			}
			vorbisMode.CalcWindows();
			return vorbisMode;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000082DA File Offset: 0x000064DA
		private VorbisMode(VorbisStreamDecoder vorbis)
		{
			this._vorbis = vorbis;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000082EC File Offset: 0x000064EC
		private void CalcWindows()
		{
			for (int i = 0; i < this._windows.Length; i++)
			{
				float[] array = this._windows[i];
				int num = (((i & 1) == 0) ? this._vorbis.Block0Size : this._vorbis.Block1Size) / 2;
				int blockSize = this.BlockSize;
				int num2 = (((i & 2) == 0) ? this._vorbis.Block0Size : this._vorbis.Block1Size) / 2;
				int num3 = blockSize / 4 - num / 2;
				int num4 = blockSize - blockSize / 4 - num2 / 2;
				for (int j = 0; j < num; j++)
				{
					float num5 = (float)Math.Sin(((double)j + 0.5) / (double)num * 1.5707963705062866);
					num5 *= num5;
					array[num3 + j] = (float)Math.Sin((double)(num5 * 1.5707964f));
				}
				for (int k = num3 + num; k < num4; k++)
				{
					array[k] = 1f;
				}
				for (int l = 0; l < num2; l++)
				{
					float num6 = (float)Math.Sin(((double)(num2 - l) - 0.5) / (double)num2 * 1.5707963705062866);
					num6 *= num6;
					array[num4 + l] = (float)Math.Sin((double)(num6 * 1.5707964f));
				}
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008438 File Offset: 0x00006638
		internal float[] GetWindow(bool prev, bool next)
		{
			if (this.BlockFlag)
			{
				if (next)
				{
					if (prev)
					{
						return this._windows[3];
					}
					return this._windows[2];
				}
				else if (prev)
				{
					return this._windows[1];
				}
			}
			return this._windows[0];
		}

		// Token: 0x040000B3 RID: 179
		private const float M_PI = 3.1415927f;

		// Token: 0x040000B4 RID: 180
		private const float M_PI2 = 1.5707964f;

		// Token: 0x040000B5 RID: 181
		private VorbisStreamDecoder _vorbis;

		// Token: 0x040000B6 RID: 182
		private float[][] _windows;

		// Token: 0x040000B7 RID: 183
		internal bool BlockFlag;

		// Token: 0x040000B8 RID: 184
		internal int WindowType;

		// Token: 0x040000B9 RID: 185
		internal int TransformType;

		// Token: 0x040000BA RID: 186
		internal VorbisMapping Mapping;

		// Token: 0x040000BB RID: 187
		internal int BlockSize;
	}
}
