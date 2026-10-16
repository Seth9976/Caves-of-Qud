using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200021C RID: 540
	[UsedByNativeCode]
	[Il2CppEagerStaticClassConstruction]
	[NativeType("Runtime/Math/Vector2Int.h")]
	public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
	{
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000375C0 File Offset: 0x000357C0
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x000375D8 File Offset: 0x000357D8
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

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x000375E4 File Offset: 0x000357E4
		// (set) Token: 0x06001D3E RID: 7486 RVA: 0x000375FC File Offset: 0x000357FC
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

		// Token: 0x06001D3F RID: 7487 RVA: 0x00037606 File Offset: 0x00035806
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00037606 File Offset: 0x00035806
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x170004C0 RID: 1216
		public int this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					num = this.y;
				}
				else
				{
					num = this.x;
				}
				return num;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x000376A8 File Offset: 0x000358A8
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000376DC File Offset: 0x000358DC
		public int sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0003770C File Offset: 0x0003590C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			float num = (float)(a.x - b.x);
			float num2 = (float)(a.y - b.y);
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00037750 File Offset: 0x00035950
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00037790 File Offset: 0x00035990
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000377D0 File Offset: 0x000359D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00037805 File Offset: 0x00035A05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector2Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00037834 File Offset: 0x00035A34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clamp(Vector2Int min, Vector2Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000378A8 File Offset: 0x00035AA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000378D0 File Offset: 0x00035AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector3Int(Vector2Int v)
		{
			return new Vector3Int(v.x, v.y, 0);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000378F8 File Offset: 0x00035AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00037928 File Offset: 0x00035B28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int CeilToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00037958 File Offset: 0x00035B58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int RoundToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00037988 File Offset: 0x00035B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator -(Vector2Int v)
		{
			return new Vector2Int(-v.x, -v.y);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000379B0 File Offset: 0x00035BB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000379E8 File Offset: 0x00035BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00037A20 File Offset: 0x00035C20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00037A58 File Offset: 0x00035C58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(int a, Vector2Int b)
		{
			return new Vector2Int(a * b.x, a * b.y);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00037A84 File Offset: 0x00035C84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00037AB0 File Offset: 0x00035CB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00037ADC File Offset: 0x00035CDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00037B14 File Offset: 0x00035D14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00037B30 File Offset: 0x00035D30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector2Int vector2Int;
			bool flag;
			if (other is Vector2Int)
			{
				vector2Int = (Vector2Int)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(vector2Int);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00037B64 File Offset: 0x00035D64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector2Int other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00037B98 File Offset: 0x00035D98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (this.x * 73856093) ^ (this.y * 83492791);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00037BC4 File Offset: 0x00035DC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00037BE0 File Offset: 0x00035DE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00037BFC File Offset: 0x00035DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00037C58 File Offset: 0x00035E58
		public static Vector2Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Zero;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00037C70 File Offset: 0x00035E70
		public static Vector2Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_One;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00037C88 File Offset: 0x00035E88
		public static Vector2Int up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Up;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00037CA0 File Offset: 0x00035EA0
		public static Vector2Int down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Down;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00037CB8 File Offset: 0x00035EB8
		public static Vector2Int left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Left;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00037CD0 File Offset: 0x00035ED0
		public static Vector2Int right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Right;
			}
		}

		// Token: 0x040008BA RID: 2234
		private int m_X;

		// Token: 0x040008BB RID: 2235
		private int m_Y;

		// Token: 0x040008BC RID: 2236
		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		// Token: 0x040008BD RID: 2237
		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		// Token: 0x040008BE RID: 2238
		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		// Token: 0x040008BF RID: 2239
		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		// Token: 0x040008C0 RID: 2240
		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		// Token: 0x040008C1 RID: 2241
		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
	}
}
