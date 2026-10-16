using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000134 RID: 308
	public struct FourCC : IEquatable<FourCC>
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x000522E9 File Offset: 0x000504E9
		public FourCC(int code)
		{
			this.m_Code = code;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000522F2 File Offset: 0x000504F2
		public FourCC(char a, char b = ' ', char c = ' ', char d = ' ')
		{
			this.m_Code = (int)(((int)a << 24) | ((int)b << 16) | ((int)c << 8) | d);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0005230C File Offset: 0x0005050C
		public FourCC(string str)
		{
			this = default(FourCC);
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (length < 1 || length > 4)
			{
				throw new ArgumentException("FourCC string must be one to four characters long!", "str");
			}
			char c = str[0];
			char c2 = ((length > 1) ? str[1] : ' ');
			char c3 = ((length > 2) ? str[2] : ' ');
			char c4 = ((length > 3) ? str[3] : ' ');
			this.m_Code = (int)(((int)c << 24) | ((int)c2 << 16) | ((int)c3 << 8) | c4);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0005239C File Offset: 0x0005059C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000523A4 File Offset: 0x000505A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator FourCC(int i)
		{
			return new FourCC(i);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x000523AC File Offset: 0x000505AC
		public override string ToString()
		{
			return string.Format("{0}{1}{2}{3}", new object[]
			{
				(char)(this.m_Code >> 24),
				(char)((this.m_Code & 16711680) >> 16),
				(char)((this.m_Code & 65280) >> 8),
				(char)(this.m_Code & 255)
			});
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0005241F File Offset: 0x0005061F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(FourCC other)
		{
			return this.m_Code == other.m_Code;
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00052430 File Offset: 0x00050630
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is FourCC)
			{
				FourCC fourCC = (FourCC)obj;
				return this.Equals(fourCC);
			}
			return false;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0005245A File Offset: 0x0005065A
		public override int GetHashCode()
		{
			return this.m_Code;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00052462 File Offset: 0x00050662
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(FourCC left, FourCC right)
		{
			return left.m_Code == right.m_Code;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00052472 File Offset: 0x00050672
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(FourCC left, FourCC right)
		{
			return left.m_Code != right.m_Code;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00052485 File Offset: 0x00050685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static FourCC FromInt32(int i)
		{
			return i;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0005248D File Offset: 0x0005068D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ToInt32(FourCC fourCC)
		{
			return fourCC.m_Code;
		}

		// Token: 0x04000716 RID: 1814
		private int m_Code;
	}
}
