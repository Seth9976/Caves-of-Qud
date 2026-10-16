using System;

namespace System.IO
{
	// Token: 0x02000B04 RID: 2820
	internal static class Error
	{
		// Token: 0x060064C3 RID: 25795 RVA: 0x001567BF File Offset: 0x001549BF
		internal static Exception GetStreamIsClosed()
		{
			return new ObjectDisposedException(null, "Cannot access a closed Stream.");
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x001567CC File Offset: 0x001549CC
		internal static Exception GetEndOfFile()
		{
			return new EndOfStreamException("Unable to read beyond the end of the stream.");
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x001567D8 File Offset: 0x001549D8
		internal static Exception GetFileNotOpen()
		{
			return new ObjectDisposedException(null, "Cannot access a closed file.");
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x001567E5 File Offset: 0x001549E5
		internal static Exception GetReadNotSupported()
		{
			return new NotSupportedException("Stream does not support reading.");
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x001567F1 File Offset: 0x001549F1
		internal static Exception GetSeekNotSupported()
		{
			return new NotSupportedException("Stream does not support seeking.");
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x001567FD File Offset: 0x001549FD
		internal static Exception GetWriteNotSupported()
		{
			return new NotSupportedException("Stream does not support writing.");
		}
	}
}
