using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A7 RID: 167
	public sealed class ReaderParameters
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00015133 File Offset: 0x00013333
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001513B File Offset: 0x0001333B
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00015144 File Offset: 0x00013344
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001514C File Offset: 0x0001334C
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00015155 File Offset: 0x00013355
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0001515D File Offset: 0x0001335D
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00015166 File Offset: 0x00013366
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0001516E File Offset: 0x0001336E
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

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00015177 File Offset: 0x00013377
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0001517F File Offset: 0x0001337F
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00015188 File Offset: 0x00013388
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00015190 File Offset: 0x00013390
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

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00015199 File Offset: 0x00013399
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x000151A1 File Offset: 0x000133A1
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000151AA File Offset: 0x000133AA
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x000151B2 File Offset: 0x000133B2
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x000151BB File Offset: 0x000133BB
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x000151C3 File Offset: 0x000133C3
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

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x000151CC File Offset: 0x000133CC
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x000151D4 File Offset: 0x000133D4
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

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x000151DD File Offset: 0x000133DD
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x000151E5 File Offset: 0x000133E5
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

		// Token: 0x060006F7 RID: 1783 RVA: 0x000151EE File Offset: 0x000133EE
		public ReaderParameters()
			: this(ReadingMode.Deferred)
		{
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000151F7 File Offset: 0x000133F7
		public ReaderParameters(ReadingMode readingMode)
		{
			this.reading_mode = readingMode;
			this.in_memory = true;
		}

		// Token: 0x04000242 RID: 578
		private ReadingMode reading_mode;

		// Token: 0x04000243 RID: 579
		internal IAssemblyResolver assembly_resolver;

		// Token: 0x04000244 RID: 580
		internal IMetadataResolver metadata_resolver;

		// Token: 0x04000245 RID: 581
		internal IMetadataImporterProvider metadata_importer_provider;

		// Token: 0x04000246 RID: 582
		internal IReflectionImporterProvider reflection_importer_provider;

		// Token: 0x04000247 RID: 583
		private Stream symbol_stream;

		// Token: 0x04000248 RID: 584
		private ISymbolReaderProvider symbol_reader_provider;

		// Token: 0x04000249 RID: 585
		private bool read_symbols;

		// Token: 0x0400024A RID: 586
		private bool projections;

		// Token: 0x0400024B RID: 587
		private bool in_memory;

		// Token: 0x0400024C RID: 588
		private bool read_write;
	}
}
