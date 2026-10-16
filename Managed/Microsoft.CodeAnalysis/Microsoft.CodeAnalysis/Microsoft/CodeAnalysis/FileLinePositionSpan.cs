using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000179 RID: 377
	[NullableContext(1)]
	[Nullable(0)]
	public struct FileLinePositionSpan : IEquatable<FileLinePositionSpan>
	{
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0002E0D3 File Offset: 0x0002C2D3
		public string Path
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0002E0DB File Offset: 0x0002C2DB
		public bool HasMappedPath
		{
			get
			{
				return this._hasMappedPath;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002E0E3 File Offset: 0x0002C2E3
		public LinePosition StartLinePosition
		{
			get
			{
				return this._span.Start;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		public LinePosition EndLinePosition
		{
			get
			{
				return this._span.End;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002E0FD File Offset: 0x0002C2FD
		public LinePositionSpan Span
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002E105 File Offset: 0x0002C305
		public FileLinePositionSpan(string path, LinePosition start, LinePosition end)
		{
			this = new FileLinePositionSpan(path, new LinePositionSpan(start, end));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0002E115 File Offset: 0x0002C315
		public FileLinePositionSpan(string path, LinePositionSpan span)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			this._path = path;
			this._span = span;
			this._hasMappedPath = false;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0002E13A File Offset: 0x0002C33A
		internal FileLinePositionSpan(string path, LinePositionSpan span, bool hasMappedPath)
		{
			this._path = path;
			this._span = span;
			this._hasMappedPath = hasMappedPath;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0002E151 File Offset: 0x0002C351
		public bool IsValid
		{
			get
			{
				return this._path != null;
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0002E15C File Offset: 0x0002C35C
		public bool Equals(FileLinePositionSpan other)
		{
			return this._span.Equals(other._span) && this._hasMappedPath == other._hasMappedPath && string.Equals(this._path, other._path, 4);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0002E193 File Offset: 0x0002C393
		[NullableContext(2)]
		public override bool Equals(object other)
		{
			return other is FileLinePositionSpan && this.Equals((FileLinePositionSpan)other);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002E1AB File Offset: 0x0002C3AB
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this._path, Hash.Combine(this._hasMappedPath, this._span.GetHashCode()));
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002E1D4 File Offset: 0x0002C3D4
		public override string ToString()
		{
			return this._path + ": " + this._span.ToString();
		}

		// Token: 0x0400044D RID: 1101
		private readonly string _path;

		// Token: 0x0400044E RID: 1102
		private readonly LinePositionSpan _span;

		// Token: 0x0400044F RID: 1103
		private readonly bool _hasMappedPath;
	}
}
