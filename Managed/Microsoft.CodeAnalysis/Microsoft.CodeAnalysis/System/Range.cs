using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace System
{
	// Token: 0x02000010 RID: 16
	internal readonly struct Range : IEquatable<Range>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public Index Start { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002ABC File Offset: 0x00000CBC
		public Index End { get; }

		// Token: 0x06000047 RID: 71 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public Range(Index start, Index end)
		{
			this.Start = start;
			this.End = end;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AD4 File Offset: 0x00000CD4
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			if (value is Range)
			{
				Range range = (Range)value;
				if (range.Start.Equals(this.Start))
				{
					return range.End.Equals(this.End);
				}
			}
			return false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B20 File Offset: 0x00000D20
		public bool Equals(Range other)
		{
			return other.Start.Equals(this.Start) && other.End.Equals(this.End);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B5C File Offset: 0x00000D5C
		public override int GetHashCode()
		{
			return Hash.Combine(this.Start.GetHashCode(), this.End.GetHashCode());
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B98 File Offset: 0x00000D98
		[NullableContext(1)]
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				Range.<ToString>g__getFromEndSpecifier|10_0(this.Start),
				Range.<ToString>g__toString|10_1(this.Start),
				"..",
				Range.<ToString>g__getFromEndSpecifier|10_0(this.End),
				Range.<ToString>g__toString|10_1(this.End)
			});
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public static Range StartAt(Index start)
		{
			return new Range(start, Index.End);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BFD File Offset: 0x00000DFD
		public static Range EndAt(Index end)
		{
			return new Range(Index.Start, end);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002C0A File Offset: 0x00000E0A
		public static Range All
		{
			get
			{
				return new Range(Index.Start, Index.End);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C1C File Offset: 0x00000E1C
		[MethodImpl(256)]
		[return: TupleElementNames(new string[] { "Offset", "Length" })]
		public ValueTuple<int, int> GetOffsetAndLength(int length)
		{
			Index start = this.Start;
			int num;
			if (start.IsFromEnd)
			{
				num = length - start.Value;
			}
			else
			{
				num = start.Value;
			}
			Index end = this.End;
			int num2;
			if (end.IsFromEnd)
			{
				num2 = length - end.Value;
			}
			else
			{
				num2 = end.Value;
			}
			if (num2 > length || num > num2)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			return new ValueTuple<int, int>(num, num2 - num);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C8D File Offset: 0x00000E8D
		[NullableContext(1)]
		[CompilerGenerated]
		internal static string <ToString>g__getFromEndSpecifier|10_0(Index index)
		{
			if (!index.IsFromEnd)
			{
				return string.Empty;
			}
			return "^";
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CA4 File Offset: 0x00000EA4
		[NullableContext(1)]
		[CompilerGenerated]
		internal static string <ToString>g__toString|10_1(Index index)
		{
			return ((uint)index.Value).ToString();
		}
	}
}
