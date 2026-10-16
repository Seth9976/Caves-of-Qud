using System;
using System.IO;
using System.Reflection;
using Mono.Cecil.Cil;

namespace Mono.Cecil
{
	// Token: 0x02000246 RID: 582
	public sealed class WriterParameters
	{
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0002C095 File Offset: 0x0002A295
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x0002C09D File Offset: 0x0002A29D
		public uint? Timestamp
		{
			get
			{
				return this.timestamp;
			}
			set
			{
				this.timestamp = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0002C0A6 File Offset: 0x0002A2A6
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0002C0AE File Offset: 0x0002A2AE
		public Stream SymbolStream
		{
			get
			{
				return this.symbol_stream;
			}
			set
			{
				this.symbol_stream = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0002C0B7 File Offset: 0x0002A2B7
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0002C0BF File Offset: 0x0002A2BF
		public ISymbolWriterProvider SymbolWriterProvider
		{
			get
			{
				return this.symbol_writer_provider;
			}
			set
			{
				this.symbol_writer_provider = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0002C0C8 File Offset: 0x0002A2C8
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		public bool WriteSymbols
		{
			get
			{
				return this.write_symbols;
			}
			set
			{
				this.write_symbols = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0002C0D9 File Offset: 0x0002A2D9
		public bool HasStrongNameKey
		{
			get
			{
				return this.key_pair != null || this.key_blob != null || this.key_container != null;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0002C0F6 File Offset: 0x0002A2F6
		// (set) Token: 0x06000D90 RID: 3472 RVA: 0x0002C0FE File Offset: 0x0002A2FE
		public byte[] StrongNameKeyBlob
		{
			get
			{
				return this.key_blob;
			}
			set
			{
				this.key_blob = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0002C107 File Offset: 0x0002A307
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0002C10F File Offset: 0x0002A30F
		public string StrongNameKeyContainer
		{
			get
			{
				return this.key_container;
			}
			set
			{
				this.key_container = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x0002C118 File Offset: 0x0002A318
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0002C120 File Offset: 0x0002A320
		public StrongNameKeyPair StrongNameKeyPair
		{
			get
			{
				return this.key_pair;
			}
			set
			{
				this.key_pair = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0002C129 File Offset: 0x0002A329
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0002C131 File Offset: 0x0002A331
		public bool DeterministicMvid { get; set; }

		// Token: 0x040003FE RID: 1022
		private uint? timestamp;

		// Token: 0x040003FF RID: 1023
		private Stream symbol_stream;

		// Token: 0x04000400 RID: 1024
		private ISymbolWriterProvider symbol_writer_provider;

		// Token: 0x04000401 RID: 1025
		private bool write_symbols;

		// Token: 0x04000402 RID: 1026
		private byte[] key_blob;

		// Token: 0x04000403 RID: 1027
		private string key_container;

		// Token: 0x04000404 RID: 1028
		private StrongNameKeyPair key_pair;
	}
}
