using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002CB RID: 715
	internal sealed class EmbeddedPortablePdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x0600123E RID: 4670 RVA: 0x0003BD40 File Offset: 0x00039F40
		internal EmbeddedPortablePdbReader(PortablePdbReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException();
			}
			this.reader = reader;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0003BD58 File Offset: 0x00039F58
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new EmbeddedPortablePdbWriterProvider();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0003BD5F File Offset: 0x00039F5F
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			return this.reader.ProcessDebugHeader(header);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0003BD6D File Offset: 0x00039F6D
		public MethodDebugInformation Read(MethodDefinition method)
		{
			return this.reader.Read(method);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0003BD7B File Offset: 0x00039F7B
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x04000938 RID: 2360
		private readonly PortablePdbReader reader;
	}
}
