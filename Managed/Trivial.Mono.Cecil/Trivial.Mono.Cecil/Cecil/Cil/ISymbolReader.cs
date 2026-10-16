using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000119 RID: 281
	public interface ISymbolReader : IDisposable
	{
		// Token: 0x06000AC7 RID: 2759
		ISymbolWriterProvider GetWriterProvider();

		// Token: 0x06000AC8 RID: 2760
		bool ProcessDebugHeader(ImageDebugHeader header);

		// Token: 0x06000AC9 RID: 2761
		MethodDebugInformation Read(MethodDefinition method);
	}
}
