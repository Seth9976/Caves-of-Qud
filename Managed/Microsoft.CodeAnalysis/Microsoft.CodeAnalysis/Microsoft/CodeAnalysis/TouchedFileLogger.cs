using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000136 RID: 310
	[NullableContext(1)]
	[Nullable(0)]
	internal class TouchedFileLogger
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x000234BC File Offset: 0x000216BC
		public TouchedFileLogger()
		{
			this._readFiles = new ConcurrentSet<string>();
			this._writtenFiles = new ConcurrentSet<string>();
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000234DA File Offset: 0x000216DA
		public void AddRead(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(path);
			}
			this._readFiles.Add(path);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000234F3 File Offset: 0x000216F3
		public void AddWritten(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(path);
			}
			this._writtenFiles.Add(path);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002350C File Offset: 0x0002170C
		public void AddReadWritten(string path)
		{
			this.AddRead(path);
			this.AddWritten(path);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002351C File Offset: 0x0002171C
		public void WriteReadPaths(TextWriter s)
		{
			string[] array = new string[this._readFiles.Count];
			int num = 0;
			foreach (string text in Interlocked.Exchange<ConcurrentSet<string>>(ref this._readFiles, null))
			{
				array[num] = text.ToUpperInvariant();
				num++;
			}
			Array.Sort<string>(array);
			foreach (string text2 in array)
			{
				s.WriteLine(text2);
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002359C File Offset: 0x0002179C
		public void WriteWrittenPaths(TextWriter s)
		{
			string[] array = new string[this._writtenFiles.Count];
			int num = 0;
			foreach (string text in Interlocked.Exchange<ConcurrentSet<string>>(ref this._writtenFiles, null))
			{
				array[num] = text.ToUpperInvariant();
				num++;
			}
			Array.Sort<string>(array);
			foreach (string text2 in array)
			{
				s.WriteLine(text2);
			}
		}

		// Token: 0x04000342 RID: 834
		private ConcurrentSet<string> _readFiles;

		// Token: 0x04000343 RID: 835
		private ConcurrentSet<string> _writtenFiles;
	}
}
