using System;
using System.IO;
using Mono.Cecil.Cil;

namespace Mono.Cecil
{
	// Token: 0x02000244 RID: 580
	public sealed class ReaderParameters
	{
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0002BEE2 File Offset: 0x0002A0E2
		// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0002BEEA File Offset: 0x0002A0EA
		public ReadingMode ReadingMode
		{
			get
			{
				return this.reading_mode;
			}
			set
			{
				this.reading_mode = value;
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002BEF3 File Offset: 0x0002A0F3
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x0002BEFB File Offset: 0x0002A0FB
		public bool InMemory
		{
			get
			{
				return this.in_memory;
			}
			set
			{
				this.in_memory = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002BF04 File Offset: 0x0002A104
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x0002BF0C File Offset: 0x0002A10C
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
			set
			{
				this.assembly_resolver = value;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002BF15 File Offset: 0x0002A115
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0002BF1D File Offset: 0x0002A11D
		public IMetadataResolver MetadataResolver
		{
			get
			{
				return this.metadata_resolver;
			}
			set
			{
				this.metadata_resolver = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0002BF26 File Offset: 0x0002A126
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0002BF2E File Offset: 0x0002A12E
		public IMetadataImporterProvider MetadataImporterProvider
		{
			get
			{
				return this.metadata_importer_provider;
			}
			set
			{
				this.metadata_importer_provider = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0002BF37 File Offset: 0x0002A137
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0002BF3F File Offset: 0x0002A13F
		public IReflectionImporterProvider ReflectionImporterProvider
		{
			get
			{
				return this.reflection_importer_provider;
			}
			set
			{
				this.reflection_importer_provider = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0002BF48 File Offset: 0x0002A148
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0002BF50 File Offset: 0x0002A150
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002BF59 File Offset: 0x0002A159
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0002BF61 File Offset: 0x0002A161
		public ISymbolReaderProvider SymbolReaderProvider
		{
			get
			{
				return this.symbol_reader_provider;
			}
			set
			{
				this.symbol_reader_provider = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0002BF6A File Offset: 0x0002A16A
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0002BF72 File Offset: 0x0002A172
		public bool ReadSymbols
		{
			get
			{
				return this.read_symbols;
			}
			set
			{
				this.read_symbols = value;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0002BF7B File Offset: 0x0002A17B
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x0002BF83 File Offset: 0x0002A183
		public bool ThrowIfSymbolsAreNotMatching
		{
			get
			{
				return this.throw_symbols_mismatch;
			}
			set
			{
				this.throw_symbols_mismatch = value;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0002BF8C File Offset: 0x0002A18C
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0002BF94 File Offset: 0x0002A194
		public bool ReadWrite
		{
			get
			{
				return this.read_write;
			}
			set
			{
				this.read_write = value;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0002BF9D File Offset: 0x0002A19D
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x0002BFA5 File Offset: 0x0002A1A5
		public bool ApplyWindowsRuntimeProjections
		{
			get
			{
				return this.projections;
			}
			set
			{
				this.projections = value;
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002BFAE File Offset: 0x0002A1AE
		public ReaderParameters()
			: this(ReadingMode.Deferred)
		{
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002BFB7 File Offset: 0x0002A1B7
		public ReaderParameters(ReadingMode readingMode)
		{
			this.reading_mode = readingMode;
			this.throw_symbols_mismatch = true;
		}

		// Token: 0x040003EA RID: 1002
		private ReadingMode reading_mode;

		// Token: 0x040003EB RID: 1003
		internal IAssemblyResolver assembly_resolver;

		// Token: 0x040003EC RID: 1004
		internal IMetadataResolver metadata_resolver;

		// Token: 0x040003ED RID: 1005
		internal IMetadataImporterProvider metadata_importer_provider;

		// Token: 0x040003EE RID: 1006
		internal IReflectionImporterProvider reflection_importer_provider;

		// Token: 0x040003EF RID: 1007
		private Stream symbol_stream;

		// Token: 0x040003F0 RID: 1008
		private ISymbolReaderProvider symbol_reader_provider;

		// Token: 0x040003F1 RID: 1009
		private bool read_symbols;

		// Token: 0x040003F2 RID: 1010
		private bool throw_symbols_mismatch;

		// Token: 0x040003F3 RID: 1011
		private bool projections;

		// Token: 0x040003F4 RID: 1012
		private bool in_memory;

		// Token: 0x040003F5 RID: 1013
		private bool read_write;
	}
}
