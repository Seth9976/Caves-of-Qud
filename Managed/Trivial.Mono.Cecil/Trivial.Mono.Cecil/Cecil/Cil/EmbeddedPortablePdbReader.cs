using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000104 RID: 260
	public sealed class EmbeddedPortablePdbReader : ISymbolReader, IDisposable
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x000226F9 File Offset: 0x000208F9
		internal EmbeddedPortablePdbReader(PortablePdbReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException();
			}
			this.reader = reader;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00022711 File Offset: 0x00020911
		public ISymbolWriterProvider GetWriterProvider()
		{
			return new EmbeddedPortablePdbWriterProvider();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00022718 File Offset: 0x00020918
		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			return this.reader.ProcessDebugHeader(header);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00022726 File Offset: 0x00020926
		public MethodDebugInformation Read(MethodDefinition method)
		{
			return this.reader.Read(method);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00022734 File Offset: 0x00020934
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x04000536 RID: 1334
		private readonly PortablePdbReader reader;
	}
}
