using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000178 RID: 376
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ExternalFileLocation : Location, IEquatable<ExternalFileLocation>
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002DFF4 File Offset: 0x0002C1F4
		internal ExternalFileLocation(string filePath, TextSpan sourceSpan, LinePositionSpan lineSpan)
		{
			this._sourceSpan = sourceSpan;
			this._lineSpan = new FileLinePositionSpan(filePath, lineSpan);
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0002E010 File Offset: 0x0002C210
		public override TextSpan SourceSpan
		{
			get
			{
				return this._sourceSpan;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0002E018 File Offset: 0x0002C218
		public string FilePath
		{
			get
			{
				return this._lineSpan.Path;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002E033 File Offset: 0x0002C233
		public override FileLinePositionSpan GetLineSpan()
		{
			return this._lineSpan;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002E03B File Offset: 0x0002C23B
		public override FileLinePositionSpan GetMappedLineSpan()
		{
			return this._lineSpan;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0002E043 File Offset: 0x0002C243
		public override LocationKind Kind
		{
			get
			{
				return LocationKind.ExternalFile;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002E046 File Offset: 0x0002C246
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ExternalFileLocation);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002E054 File Offset: 0x0002C254
		[NullableContext(2)]
		public bool Equals(ExternalFileLocation obj)
		{
			return obj == this || (obj != null && this._sourceSpan == obj._sourceSpan && this._lineSpan.Equals(obj._lineSpan));
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002E09C File Offset: 0x0002C29C
		public override int GetHashCode()
		{
			return Hash.Combine(this._lineSpan.GetHashCode(), this._sourceSpan.GetHashCode());
		}

		// Token: 0x0400044B RID: 1099
		private readonly TextSpan _sourceSpan;

		// Token: 0x0400044C RID: 1100
		private readonly FileLinePositionSpan _lineSpan;
	}
}
