using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E7 RID: 743
	[DataContract]
	public readonly struct LinePositionSpan : IEquatable<LinePositionSpan>
	{
		// Token: 0x06001FD7 RID: 8151 RVA: 0x00062606 File Offset: 0x00060806
		public LinePositionSpan(LinePosition start, LinePosition end)
		{
			if (end < start)
			{
				throw new ArgumentException(CodeAnalysisResources.EndMustNotBeLessThanStart, "end");
			}
			this._start = start;
			this._end = end;
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0006262F File Offset: 0x0006082F
		public LinePosition Start
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x00062637 File Offset: 0x00060837
		public LinePosition End
		{
			get
			{
				return this._end;
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0006263F File Offset: 0x0006083F
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LinePositionSpan && this.Equals((LinePositionSpan)obj);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x00062657 File Offset: 0x00060857
		public bool Equals(LinePositionSpan other)
		{
			return this._start.Equals(other._start) && this._end.Equals(other._end);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0006267F File Offset: 0x0006087F
		public override int GetHashCode()
		{
			return Hash.Combine(this._start.GetHashCode(), this._end.GetHashCode());
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000626A8 File Offset: 0x000608A8
		public static bool operator ==(LinePositionSpan left, LinePositionSpan right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000626B2 File Offset: 0x000608B2
		public static bool operator !=(LinePositionSpan left, LinePositionSpan right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000626BF File Offset: 0x000608BF
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Format("({0})-({1})", this._start, this._end);
		}

		// Token: 0x04000DFC RID: 3580
		[DataMember(Order = 0)]
		private readonly LinePosition _start;

		// Token: 0x04000DFD RID: 3581
		[DataMember(Order = 1)]
		private readonly LinePosition _end;
	}
}
