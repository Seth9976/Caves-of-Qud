using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000145 RID: 325
	internal struct PredictiveParser
	{
		// Token: 0x060011CC RID: 4556 RVA: 0x00054F94 File Offset: 0x00053194
		public unsafe void ExpectSingleChar(ReadOnlySpan<char> str, char c)
		{
			if (*str[this.m_Position] != (ushort)c)
			{
				throw new InvalidOperationException(string.Format("Expected a '{0}' character at position {1} in : {2}", c, this.m_Position, str.ToString()));
			}
			this.m_Position++;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00054FF0 File Offset: 0x000531F0
		public unsafe int ExpectInt(ReadOnlySpan<char> str)
		{
			int num = this.m_Position;
			int num2 = 1;
			if (*str[num] == 45)
			{
				num2 = -1;
				num++;
			}
			int num3 = 0;
			for (;;)
			{
				char c = (char)(*str[num]);
				if (c < '0' || c > '9')
				{
					break;
				}
				num3 *= 10;
				num3 += (int)(c - '0');
				num++;
			}
			if (this.m_Position == num)
			{
				throw new InvalidOperationException(string.Format("Expected an int at position {0} in {1}", this.m_Position, str.ToString()));
			}
			this.m_Position = num;
			return num3 * num2;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0005507C File Offset: 0x0005327C
		public unsafe ReadOnlySpan<char> ExpectString(ReadOnlySpan<char> str)
		{
			int position = this.m_Position;
			if (*str[position] != 34)
			{
				throw new InvalidOperationException(string.Format("Expected a '\"' character at position {0} in {1}", this.m_Position, str.ToString()));
			}
			this.m_Position++;
			for (;;)
			{
				char c = (char)(*str[this.m_Position]);
				c |= ' ';
				if (c < 'a' || c > 'z')
				{
					break;
				}
				this.m_Position++;
			}
			if (*str[this.m_Position] != 34)
			{
				throw new InvalidOperationException(string.Format("Expected a closing '\"' character at position {0} in string: {1}", this.m_Position, str.ToString()));
			}
			if (this.m_Position - position == 1)
			{
				return ReadOnlySpan<char>.Empty;
			}
			ReadOnlySpan<char> readOnlySpan = str.Slice(position + 1, this.m_Position - position - 1);
			this.m_Position++;
			return readOnlySpan;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0005516F File Offset: 0x0005336F
		public unsafe bool AcceptSingleChar(ReadOnlySpan<char> str, char c)
		{
			if (*str[this.m_Position] != (ushort)c)
			{
				return false;
			}
			this.m_Position++;
			return true;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00055194 File Offset: 0x00053394
		public unsafe bool AcceptString(ReadOnlySpan<char> input, out ReadOnlySpan<char> output)
		{
			output = default(ReadOnlySpan<char>);
			int position = this.m_Position;
			int num = position;
			if (*input[num] != 34)
			{
				return false;
			}
			num++;
			for (;;)
			{
				char c = (char)(*input[num]);
				c |= ' ';
				if (c < 'a' || c > 'z')
				{
					break;
				}
				num++;
			}
			if (*input[num] != 34)
			{
				return false;
			}
			if (this.m_Position - position == 1)
			{
				output = ReadOnlySpan<char>.Empty;
			}
			else
			{
				output = input.Slice(position + 1, num - position - 1);
			}
			this.m_Position = num + 1;
			return true;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0005522C File Offset: 0x0005342C
		public unsafe void AcceptInt(ReadOnlySpan<char> str)
		{
			if (*str[this.m_Position] == 45)
			{
				this.m_Position++;
			}
			for (;;)
			{
				char c = (char)(*str[this.m_Position]);
				if (c < '0' || c > '9')
				{
					break;
				}
				this.m_Position++;
			}
		}

		// Token: 0x04000734 RID: 1844
		private int m_Position;
	}
}
