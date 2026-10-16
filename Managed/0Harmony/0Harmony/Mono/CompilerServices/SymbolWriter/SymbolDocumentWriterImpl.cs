using System;
using System.Diagnostics.SymbolStore;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000317 RID: 791
	internal class SymbolDocumentWriterImpl : ISymbolDocumentWriter, ISourceFile, ICompileUnit
	{
		// Token: 0x060013F3 RID: 5107 RVA: 0x000410A6 File Offset: 0x0003F2A6
		public SymbolDocumentWriterImpl(CompileUnitEntry comp_unit)
		{
			this.comp_unit = comp_unit;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00018659 File Offset: 0x00016859
		public void SetCheckSum(Guid algorithmId, byte[] checkSum)
		{
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00018659 File Offset: 0x00016859
		public void SetSource(byte[] source)
		{
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x000410B5 File Offset: 0x0003F2B5
		SourceFileEntry ISourceFile.Entry
		{
			get
			{
				return this.comp_unit.SourceFile;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x000410C2 File Offset: 0x0003F2C2
		public CompileUnitEntry Entry
		{
			get
			{
				return this.comp_unit;
			}
		}

		// Token: 0x04000A60 RID: 2656
		private CompileUnitEntry comp_unit;
	}
}
