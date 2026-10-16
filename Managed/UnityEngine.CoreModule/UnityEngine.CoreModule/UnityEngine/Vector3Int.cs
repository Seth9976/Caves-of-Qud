using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200021D RID: 541
	[Il2CppEagerStaticClassConstruction]
	[UsedByNativeCode]
	public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00037D40 File Offset: 0x00035F40
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x00037D58 File Offset: 0x00035F58
		public int x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_X;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x00037D64 File Offset: 0x00035F64
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x00037D7C File Offset: 0x00035F7C
		public int y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Y;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00037D88 File Offset: 0x00035F88
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x00037DA0 File Offset: 0x00035FA0
		public int z
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Z;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00037DAA File Offset: 0x00035FAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = 0;
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00037DC2 File Offset: 0x00035FC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3Int(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00037DC2 File Offset: 0x00035FC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x170004CC RID: 1228
		public int this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int num;
				switch (index)
				{
				case 0:
					num = this.x;
					break;
				case 1:
					num = this.y;
					break;
				case 2:
					num = this.z;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[] { index }));
				}
				return num;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[] { index }));
				}
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x00037EA4 File Offset: 0x000360A4
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00037EE8 File Offset: 0x000360E8
		public int sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00037F24 File Offset: 0x00036124
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00037F48 File Offset: 0x00036148
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00037F98 File Offset: 0x00036198
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00037FE8 File Offset: 0x000361E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0003802C File Offset: 0x0003622C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector3Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0003807C File Offset: 0x0003627C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clamp(Vector3Int min, Vector3Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
			this.z = Math.Max(min.z, this.z);
			this.z = Math.Min(max.z, this.z);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00038120 File Offset: 0x00036320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00038150 File Offset: 0x00036350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2Int(Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00038178 File Offset: 0x00036378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int FloorToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x000381B0 File Offset: 0x000363B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int CeilToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000381E8 File Offset: 0x000363E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int RoundToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00038220 File Offset: 0x00036420
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00038264 File Offset: 0x00036464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000382A8 File Offset: 0x000364A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000382EC File Offset: 0x000364EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.x, -a.y, -a.z);
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0003831C File Offset: 0x0003651C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00038350 File Offset: 0x00036550
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(int a, Vector3Int b)
		{
			return new Vector3Int(a * b.x, a * b.y, a * b.z);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00038384 File Offset: 0x00036584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000383B8 File Offset: 0x000365B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00038400 File Offset: 0x00036600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0003841C File Offset: 0x0003661C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector3Int vector3Int;
			bool flag;
			if (other is Vector3Int)
			{
				vector3Int = (Vector3Int)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(vector3Int);
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00038450 File Offset: 0x00036650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00038470 File Offset: 0x00036670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			int hashCode = this.y.GetHashCode();
			int hashCode2 = this.z.GetHashCode();
			return this.x.GetHashCode() ^ (hashCode << 4) ^ (hashCode >> 28) ^ (hashCode2 >> 4) ^ (hashCode2 << 28);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000384C0 File Offset: 0x000366C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000384DC File Offset: 0x000366DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000384F8 File Offset: 0x000366F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00038568 File Offset: 0x00036768
		public static Vector3Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Zero;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x00038580 File Offset: 0x00036780
		public static Vector3Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_One;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00038598 File Offset: 0x00036798
		public static Vector3Int up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Up;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x000385B0 File Offset: 0x000367B0
		public static Vector3Int down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Down;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000385C8 File Offset: 0x000367C8
		public static Vector3Int left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Left;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x000385E0 File Offset: 0x000367E0
		public static Vector3Int right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Right;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x000385F8 File Offset: 0x000367F8
		public static Vector3Int forward
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Forward;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00038610 File Offset: 0x00036810
		public static Vector3Int back
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Back;
			}
		}

		// Token: 0x040008C2 RID: 2242
		private int m_X;

		// Token: 0x040008C3 RID: 2243
		private int m_Y;

		// Token: 0x040008C4 RID: 2244
		private int m_Z;

		// Token: 0x040008C5 RID: 2245
		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		// Token: 0x040008C6 RID: 2246
		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		// Token: 0x040008C7 RID: 2247
		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		// Token: 0x040008C8 RID: 2248
		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		// Token: 0x040008C9 RID: 2249
		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		// Token: 0x040008CA RID: 2250
		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);

		// Token: 0x040008CB RID: 2251
		private static readonly Vector3Int s_Forward = new Vector3Int(0, 0, 1);

		// Token: 0x040008CC RID: 2252
		private static readonly Vector3Int s_Back = new Vector3Int(0, 0, -1);
	}
}
