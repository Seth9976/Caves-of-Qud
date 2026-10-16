using System;
using System.Diagnostics.SymbolStore;

namespace Trivial.Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200001A RID: 26
	internal class SymbolDocumentWriterImpl : ISymbolDocumentWriter, ISourceFile, ICompileUnit
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000564D File Offset: 0x0000384D
		SourceFileEntry ISourceFile.Entry
		{
			get
			{
				return this.comp_unit.SourceFile;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000565A File Offset: 0x0000385A
		public CompileUnitEntry Entry
		{
			get
			{
				return this.comp_unit;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005662 File Offset: 0x00003862
		public SymbolDocumentWriterImpl(CompileUnitEntry comp_unit)
		{
			this.comp_unit = comp_unit;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void SetCheckSum(Guid algorithmId, byte[] checkSum)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000048C7 File Offset: 0x00002AC7
		public void SetSource(byte[] source)
		{
		}

		// Token: 0x04000095 RID: 149
		private CompileUnitEntry comp_unit;
	}
}
