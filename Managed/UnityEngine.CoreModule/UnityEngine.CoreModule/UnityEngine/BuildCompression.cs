using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000149 RID: 329
	[UsedByNativeCode]
	[Serializable]
	public struct BuildCompression
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00010F78 File Offset: 0x0000F178
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00010F90 File Offset: 0x0000F190
		public CompressionType compression
		{
			get
			{
				return this._compression;
			}
			private set
			{
				this._compression = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00010F9C File Offset: 0x0000F19C
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		public CompressionLevel level
		{
			get
			{
				return this._level;
			}
			private set
			{
				this._level = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public uint blockSize
		{
			get
			{
				return this._blockSize;
			}
			private set
			{
				this._blockSize = value;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00010FE2 File Offset: 0x0000F1E2
		private BuildCompression(CompressionType in_compression, CompressionLevel in_level, uint in_blockSize)
		{
			this = default(BuildCompression);
			this.compression = in_compression;
			this.level = in_level;
			this.blockSize = in_blockSize;
		}

		// Token: 0x0400045B RID: 1115
		public static readonly BuildCompression Uncompressed = new BuildCompression(CompressionType.None, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400045C RID: 1116
		public static readonly BuildCompression LZ4 = new BuildCompression(CompressionType.Lz4HC, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400045D RID: 1117
		public static readonly BuildCompression LZMA = new BuildCompression(CompressionType.Lzma, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400045E RID: 1118
		public static readonly BuildCompression UncompressedRuntime = BuildCompression.Uncompressed;

		// Token: 0x0400045F RID: 1119
		public static readonly BuildCompression LZ4Runtime = new BuildCompression(CompressionType.Lz4, CompressionLevel.Maximum, 131072U);

		// Token: 0x04000460 RID: 1120
		[NativeName("compression")]
		private CompressionType _compression;

		// Token: 0x04000461 RID: 1121
		[NativeName("level")]
		private CompressionLevel _level;

		// Token: 0x04000462 RID: 1122
		[NativeName("blockSize")]
		private uint _blockSize;
	}
}
