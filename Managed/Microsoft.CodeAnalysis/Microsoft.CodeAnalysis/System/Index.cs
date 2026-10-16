using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200000F RID: 15
	internal readonly struct Index : IEquatable<Index>
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002960 File Offset: 0x00000B60
		[MethodImpl(256)]
		public Index(int value, bool fromEnd = false)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", value, "Non-negative number required.");
			}
			if (fromEnd)
			{
				this._value = ~value;
				return;
			}
			this._value = value;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000298F File Offset: 0x00000B8F
		private Index(int value)
		{
			this._value = value;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002998 File Offset: 0x00000B98
		public static Index Start
		{
			get
			{
				return new Index(0);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static Index End
		{
			get
			{
				return new Index(-1);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029A8 File Offset: 0x00000BA8
		[MethodImpl(256)]
		public static Index FromStart(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", value, "Non-negative number required.");
			}
			return new Index(value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029CA File Offset: 0x00000BCA
		[MethodImpl(256)]
		public static Index FromEnd(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value", value, "Non-negative number required.");
			}
			return new Index(~value);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000029ED File Offset: 0x00000BED
		public int Value
		{
			get
			{
				if (this._value < 0)
				{
					return ~this._value;
				}
				return this._value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002A06 File Offset: 0x00000C06
		public bool IsFromEnd
		{
			get
			{
				return this._value < 0;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A14 File Offset: 0x00000C14
		[MethodImpl(256)]
		public int GetOffset(int length)
		{
			int num = this._value;
			if (this.IsFromEnd)
			{
				num += length + 1;
			}
			return num;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A37 File Offset: 0x00000C37
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			return value is Index && this._value == ((Index)value)._value;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A56 File Offset: 0x00000C56
		public bool Equals(Index other)
		{
			return this._value == other._value;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A66 File Offset: 0x00000C66
		public override int GetHashCode()
		{
			return this._value;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A6E File Offset: 0x00000C6E
		public static implicit operator Index(int value)
		{
			return Index.FromStart(value);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A78 File Offset: 0x00000C78
		[NullableContext(1)]
		public override string ToString()
		{
			if (this.IsFromEnd)
			{
				return "^" + ((uint)this.Value).ToString();
			}
			return ((uint)this.Value).ToString();
		}

		// Token: 0x04000013 RID: 19
		private readonly int _value;
	}
}
