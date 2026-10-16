using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200021E RID: 542
	[NativeHeader("Runtime/Math/Vector4.h")]
	[Il2CppEagerStaticClassConstruction]
	[NativeClass("Vector4f")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x170004D7 RID: 1239
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				float num;
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
				case 3:
					num = this.w;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
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
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00038755 File Offset: 0x00036955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00038775 File Offset: 0x00036975
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00038798 File Offset: 0x00036998
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00038755 File Offset: 0x00036955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000387C0 File Offset: 0x000369C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00038838 File Offset: 0x00036A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000388A8 File Offset: 0x00036AA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = target.w - current.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			bool flag = num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta);
			Vector4 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num6 = (float)Math.Sqrt((double)num5);
				vector = new Vector4(current.x + num / num6 * maxDistanceDelta, current.y + num2 / num6 * maxDistanceDelta, current.z + num3 / num6 * maxDistanceDelta, current.w + num4 / num6 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00038978 File Offset: 0x00036B78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000389C4 File Offset: 0x00036BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00038A20 File Offset: 0x00036C20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00038A68 File Offset: 0x00036C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector4 vector;
			bool flag;
			if (other is Vector4)
			{
				vector = (Vector4)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(vector);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00038A9C File Offset: 0x00036C9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector4 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00038AEC File Offset: 0x00036CEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 a)
		{
			float num = Vector4.Magnitude(a);
			bool flag = num > 1E-05f;
			Vector4 vector;
			if (flag)
			{
				vector = a / num;
			}
			else
			{
				vector = Vector4.zero;
			}
			return vector;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00038B20 File Offset: 0x00036D20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float num = Vector4.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector4.zero;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x00038B68 File Offset: 0x00036D68
		public Vector4 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00038B88 File Offset: 0x00036D88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00038BD4 File Offset: 0x00036DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Project(Vector4 a, Vector4 b)
		{
			return b * (Vector4.Dot(a, b) / Vector4.Dot(b, b));
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00038BFC File Offset: 0x00036DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 a, Vector4 b)
		{
			return Vector4.Magnitude(a - b);
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00038C1C File Offset: 0x00036E1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector4 a)
		{
			return (float)Math.Sqrt((double)Vector4.Dot(a, a));
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x00038C3C File Offset: 0x00036E3C
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)Vector4.Dot(this, this));
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x00038C68 File Offset: 0x00036E68
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00038C8C File Offset: 0x00036E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00038CE8 File Offset: 0x00036EE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x00038D44 File Offset: 0x00036F44
		public static Vector4 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.zeroVector;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00038D5C File Offset: 0x00036F5C
		public static Vector4 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.oneVector;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00038D74 File Offset: 0x00036F74
		public static Vector4 positiveInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.positiveInfinityVector;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00038D8C File Offset: 0x00036F8C
		public static Vector4 negativeInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector4.negativeInfinityVector;
			}
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00038DA4 File Offset: 0x00036FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00038DF0 File Offset: 0x00036FF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00038E3C File Offset: 0x0003703C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00038E70 File Offset: 0x00037070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00038EA8 File Offset: 0x000370A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00038EE0 File Offset: 0x000370E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00038F18 File Offset: 0x00037118
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = lhs.w - rhs.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			return num5 < 9.9999994E-11f;
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00038F80 File Offset: 0x00037180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00038F9C File Offset: 0x0003719C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00038FCC File Offset: 0x000371CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00038FF8 File Offset: 0x000371F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00039028 File Offset: 0x00037228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0003904C File Offset: 0x0003724C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00039068 File Offset: 0x00037268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00039084 File Offset: 0x00037284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0003910C File Offset: 0x0003730C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00039128 File Offset: 0x00037328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float SqrMagnitude()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x040008CD RID: 2253
		public const float kEpsilon = 1E-05f;

		// Token: 0x040008CE RID: 2254
		public float x;

		// Token: 0x040008CF RID: 2255
		public float y;

		// Token: 0x040008D0 RID: 2256
		public float z;

		// Token: 0x040008D1 RID: 2257
		public float w;

		// Token: 0x040008D2 RID: 2258
		private static readonly Vector4 zeroVector = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040008D3 RID: 2259
		private static readonly Vector4 oneVector = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x040008D4 RID: 2260
		private static readonly Vector4 positiveInfinityVector = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040008D5 RID: 2261
		private static readonly Vector4 negativeInfinityVector = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
