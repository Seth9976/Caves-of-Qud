using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Roslyn.Utilities
{
	// Token: 0x02000053 RID: 83
	[NullableContext(1)]
	[Nullable(0)]
	internal class NoThrowStreamDisposer : IDisposable
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000068AC File Offset: 0x00004AAC
		public Stream Stream { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000068B4 File Offset: 0x00004AB4
		public bool HasFailedToDispose
		{
			get
			{
				return this._failed.GetValueOrDefault();
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000068C1 File Offset: 0x00004AC1
		public NoThrowStreamDisposer(Stream stream, string filePath, DiagnosticBag diagnostics, CommonMessageProvider messageProvider)
		{
			this.Stream = stream;
			this._failed = default(bool?);
			this._filePath = filePath;
			this._diagnostics = diagnostics;
			this._messageProvider = messageProvider;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000068F4 File Offset: 0x00004AF4
		public void Dispose()
		{
			try
			{
				this.Stream.Dispose();
				if (this._failed == null)
				{
					this._failed = new bool?(false);
				}
			}
			catch (Exception ex)
			{
				this._messageProvider.ReportStreamWriteException(ex, this._filePath, this._diagnostics);
				this._failed = new bool?(true);
			}
		}

		// Token: 0x0400007B RID: 123
		private bool? _failed;

		// Token: 0x0400007C RID: 124
		private readonly string _filePath;

		// Token: 0x0400007D RID: 125
		private readonly DiagnosticBag _diagnostics;

		// Token: 0x0400007E RID: 126
		private readonly CommonMessageProvider _messageProvider;
	}
}
