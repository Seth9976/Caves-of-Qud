using System;
using System.IO;
using System.Reflection;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000CD RID: 205
	public sealed class WriterParameters
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x0001C5BD File Offset: 0x0001A7BD
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x0001C5C5 File Offset: 0x0001A7C5
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001C5CE File Offset: 0x0001A7CE
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0001C5DF File Offset: 0x0001A7DF
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x0001C5E7 File Offset: 0x0001A7E7
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

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
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

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001C601 File Offset: 0x0001A801
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0001C609 File Offset: 0x0001A809
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

		// Token: 0x0400032A RID: 810
		private uint? timestamp;

		// Token: 0x0400032B RID: 811
		private Stream symbol_stream;

		// Token: 0x0400032C RID: 812
		private ISymbolWriterProvider symbol_writer_provider;

		// Token: 0x0400032D RID: 813
		private bool write_symbols;

		// Token: 0x0400032E RID: 814
		private StrongNameKeyPair key_pair;
	}
}
