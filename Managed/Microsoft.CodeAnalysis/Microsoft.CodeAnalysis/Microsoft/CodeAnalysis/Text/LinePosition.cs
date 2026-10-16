using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E6 RID: 742
	[DataContract]
	public readonly struct LinePosition : IEquatable<LinePosition>, IComparable<LinePosition>
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x00062484 File Offset: 0x00060684
		public static LinePosition Zero
		{
			get
			{
				return default(LinePosition);
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0006249A File Offset: 0x0006069A
		public LinePosition(int line, int character)
		{
			if (line < 0)
			{
				throw new ArgumentOutOfRangeException("line");
			}
			if (character < 0)
			{
				throw new ArgumentOutOfRangeException("character");
			}
			this._line = line;
			this._character = character;
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000624C8 File Offset: 0x000606C8
		internal LinePosition(int character)
		{
			if (character < 0)
			{
				throw new ArgumentOutOfRangeException("character");
			}
			this._line = -1;
			this._character = character;
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x000624E7 File Offset: 0x000606E7
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x000624EF File Offset: 0x000606EF
		public int Character
		{
			get
			{
				return this._character;
			}
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x000624F7 File Offset: 0x000606F7
		public static bool operator ==(LinePosition left, LinePosition right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00062501 File Offset: 0x00060701
		public static bool operator !=(LinePosition left, LinePosition right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0006250E File Offset: 0x0006070E
		public bool Equals(LinePosition other)
		{
			return other.Line == this.Line && other.Character == this.Character;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x00062530 File Offset: 0x00060730
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LinePosition && this.Equals((LinePosition)obj);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x00062548 File Offset: 0x00060748
		public override int GetHashCode()
		{
			return Hash.Combine(this.Line, this.Character);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0006255C File Offset: 0x0006075C
		[NullableContext(1)]
		public override string ToString()
		{
			return this.Line.ToString() + "," + this.Character.ToString();
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x00062590 File Offset: 0x00060790
		public int CompareTo(LinePosition other)
		{
			int num = this._line.CompareTo(other._line);
			if (num == 0)
			{
				return this._character.CompareTo(other.Character);
			}
			return num;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000625CC File Offset: 0x000607CC
		public static bool operator >(LinePosition left, LinePosition right)
		{
			return left.CompareTo(right) > 0;
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x000625D9 File Offset: 0x000607D9
		public static bool operator >=(LinePosition left, LinePosition right)
		{
			return left.CompareTo(right) >= 0;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x000625E9 File Offset: 0x000607E9
		public static bool operator <(LinePosition left, LinePosition right)
		{
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x000625F6 File Offset: 0x000607F6
		public static bool operator <=(LinePosition left, LinePosition right)
		{
			return left.CompareTo(right) <= 0;
		}

		// Token: 0x04000DFA RID: 3578
		[DataMember(Order = 0)]
		private readonly int _line;

		// Token: 0x04000DFB RID: 3579
		[DataMember(Order = 1)]
		private readonly int _character;
	}
}
