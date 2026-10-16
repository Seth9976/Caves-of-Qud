using System;
using System.IO;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000578 RID: 1400
	internal class ClosableStream : DelegatedStream
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x0009AE67 File Offset: 0x00099067
		internal ClosableStream(Stream stream, EventHandler onClose)
			: base(stream)
		{
			this._onClose = onClose;
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0009AE77 File Offset: 0x00099077
		public override void Close()
		{
			if (Interlocked.Increment(ref this._closed) == 1)
			{
				EventHandler onClose = this._onClose;
				if (onClose == null)
				{
					return;
				}
				onClose(this, new EventArgs());
			}
		}

		// Token: 0x040018B6 RID: 6326
		private readonly EventHandler _onClose;

		// Token: 0x040018B7 RID: 6327
		private int _closed;
	}
}
