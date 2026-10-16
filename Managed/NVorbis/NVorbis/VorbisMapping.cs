using System;

namespace NVorbis
{
	// Token: 0x02000021 RID: 33
	internal abstract class VorbisMapping
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00007F54 File Offset: 0x00006154
		internal static VorbisMapping Init(VorbisStreamDecoder vorbis, DataPacket packet)
		{
			int num = (int)packet.ReadBits(16);
			VorbisMapping vorbisMapping = null;
			int num2 = num;
			if (num2 == 0)
			{
				vorbisMapping = new VorbisMapping.Mapping0(vorbis);
			}
			if (vorbisMapping == null)
			{
				throw new InvalidDataException();
			}
			vorbisMapping.Init(packet);
			return vorbisMapping;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007F8B File Offset: 0x0000618B
		protected VorbisMapping(VorbisStreamDecoder vorbis)
		{
			this._vorbis = vorbis;
		}

		// Token: 0x06000119 RID: 281
		protected abstract void Init(DataPacket packet);

		// Token: 0x040000AB RID: 171
		private VorbisStreamDecoder _vorbis;

		// Token: 0x040000AC RID: 172
		internal VorbisMapping.Submap[] Submaps;

		// Token: 0x040000AD RID: 173
		internal VorbisMapping.Submap[] ChannelSubmap;

		// Token: 0x040000AE RID: 174
		internal VorbisMapping.CouplingStep[] CouplingSteps;

		// Token: 0x02000022 RID: 34
		private class Mapping0 : VorbisMapping
		{
			// Token: 0x0600011A RID: 282 RVA: 0x00007F9A File Offset: 0x0000619A
			internal Mapping0(VorbisStreamDecoder vorbis)
				: base(vorbis)
			{
			}

			// Token: 0x0600011B RID: 283 RVA: 0x00007FA4 File Offset: 0x000061A4
			protected override void Init(DataPacket packet)
			{
				int num = 1;
				if (packet.ReadBit())
				{
					num += (int)packet.ReadBits(4);
				}
				int num2 = 0;
				if (packet.ReadBit())
				{
					num2 = (int)packet.ReadBits(8) + 1;
				}
				int num3 = Utils.ilog(this._vorbis._channels - 1);
				this.CouplingSteps = new VorbisMapping.CouplingStep[num2];
				for (int i = 0; i < num2; i++)
				{
					int num4 = (int)packet.ReadBits(num3);
					int num5 = (int)packet.ReadBits(num3);
					if (num4 == num5 || num4 > this._vorbis._channels - 1 || num5 > this._vorbis._channels - 1)
					{
						throw new InvalidDataException();
					}
					this.CouplingSteps[i] = new VorbisMapping.CouplingStep
					{
						Angle = num5,
						Magnitude = num4
					};
				}
				if (packet.ReadBits(2) != 0UL)
				{
					throw new InvalidDataException();
				}
				int[] array = new int[this._vorbis._channels];
				if (num > 1)
				{
					for (int j = 0; j < this.ChannelSubmap.Length; j++)
					{
						array[j] = (int)packet.ReadBits(4);
						if (array[j] >= num)
						{
							throw new InvalidDataException();
						}
					}
				}
				this.Submaps = new VorbisMapping.Submap[num];
				for (int k = 0; k < num; k++)
				{
					packet.ReadBits(8);
					int num6 = (int)packet.ReadBits(8);
					if (num6 >= this._vorbis.Floors.Length)
					{
						throw new InvalidDataException();
					}
					int num7 = (int)packet.ReadBits(8);
					if (num7 >= this._vorbis.Residues.Length)
					{
						throw new InvalidDataException();
					}
					this.Submaps[k] = new VorbisMapping.Submap
					{
						Floor = this._vorbis.Floors[num6],
						Residue = this._vorbis.Residues[num6]
					};
				}
				this.ChannelSubmap = new VorbisMapping.Submap[this._vorbis._channels];
				for (int l = 0; l < this.ChannelSubmap.Length; l++)
				{
					this.ChannelSubmap[l] = this.Submaps[array[l]];
				}
			}
		}

		// Token: 0x02000023 RID: 35
		internal class Submap
		{
			// Token: 0x0600011C RID: 284 RVA: 0x000081AE File Offset: 0x000063AE
			internal Submap()
			{
			}

			// Token: 0x040000AF RID: 175
			internal VorbisFloor Floor;

			// Token: 0x040000B0 RID: 176
			internal VorbisResidue Residue;
		}

		// Token: 0x02000024 RID: 36
		internal class CouplingStep
		{
			// Token: 0x0600011D RID: 285 RVA: 0x000081B6 File Offset: 0x000063B6
			internal CouplingStep()
			{
			}

			// Token: 0x040000B1 RID: 177
			internal int Magnitude;

			// Token: 0x040000B2 RID: 178
			internal int Angle;
		}
	}
}
