using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200021B RID: 539
	[NativeClass("Vector2f")]
	[Il2CppEagerStaticClassConstruction]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		// Token: 0x170004B2 RID: 1202
		public float this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				float num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000369EE File Offset: 0x00034BEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000369EE File Offset: 0x00034BEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY)
		{
			this.x = newX;
			this.y = newY;
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x00036A00 File Offset: 0x00034C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x00036A4C File Offset: 0x00034C4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x00036A90 File Offset: 0x00034C90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = num * num + num2 * num2;
			bool flag = num3 == 0f || (maxDistanceDelta >= 0f && num3 <= maxDistanceDelta * maxDistanceDelta);
			Vector2 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num4 = (float)Math.Sqrt((double)num3);
				vector = new Vector2(current.x + num / num4 * maxDistanceDelta, current.y + num2 / num4 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x00036B18 File Offset: 0x00034D18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x00036B49 File Offset: 0x00034D49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x00036B74 File Offset: 0x00034D74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float magnitude = this.magnitude;
			bool flag = magnitude > 1E-05f;
			if (flag)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x00036BB4 File Offset: 0x00034DB4
		public Vector2 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				Vector2 vector = new Vector2(this.x, this.y);
				vector.Normalize();
				return vector;
			}
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x00036BE4 File Offset: 0x00034DE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x00036C00 File Offset: 0x00034E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x00036C1C File Offset: 0x00034E1C
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
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00036C84 File Offset: 0x00034E84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00036CB0 File Offset: 0x00034EB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector2 vector;
			bool flag;
			if (other is Vector2)
			{
				vector = (Vector2)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(vector);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00036CE4 File Offset: 0x00034EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x00036D18 File Offset: 0x00034F18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
		{
			float num = -2f * Vector2.Dot(inNormal, inDirection);
			return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x00036D5C File Offset: 0x00034F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Perpendicular(Vector2 inDirection)
		{
			return new Vector2(-inDirection.y, inDirection.x);
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00036D80 File Offset: 0x00034F80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x00036DB0 File Offset: 0x00034FB0
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00036DE4 File Offset: 0x00034FE4
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x00036E14 File Offset: 0x00035014
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Vector2 from, Vector2 to)
		{
			float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = num < 1E-15f;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = Mathf.Clamp(Vector2.Dot(from, to) / num, -1f, 1f);
				num2 = (float)Math.Acos((double)num3) * 57.29578f;
			}
			return num2;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x00036E78 File Offset: 0x00035078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SignedAngle(Vector2 from, Vector2 to)
		{
			float num = Vector2.Angle(from, to);
			float num2 = Mathf.Sign(from.x * to.y - from.y * to.x);
			return num * num2;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00036EB8 File Offset: 0x000350B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2 a, Vector2 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00036EF4 File Offset: 0x000350F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector2 vector2;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				vector2 = new Vector2(num2 * maxLength, num3 * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00036F48 File Offset: 0x00035148
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x00036F78 File Offset: 0x00035178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float SqrMagnitude()
		{
			return this.x * this.x + this.y * this.y;
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x00036FA8 File Offset: 0x000351A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x06001D22 RID: 7458 RVA: 0x00036FE4 File Offset: 0x000351E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x00037020 File Offset: 0x00035220
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x00037044 File Offset: 0x00035244
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x00037070 File Offset: 0x00035270
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			Vector2 vector = target;
			float num6 = maxSpeed * smoothTime;
			float num7 = num6 * num6;
			float num8 = num4 * num4 + num5 * num5;
			bool flag = num8 > num7;
			if (flag)
			{
				float num9 = (float)Math.Sqrt((double)num8);
				num4 = num4 / num9 * num6;
				num5 = num5 / num9 * num6;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			float num10 = (currentVelocity.x + num * num4) * deltaTime;
			float num11 = (currentVelocity.y + num * num5) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num10) * num3;
			currentVelocity.y = (currentVelocity.y - num * num11) * num3;
			float num12 = target.x + (num4 + num10) * num3;
			float num13 = target.y + (num5 + num11) * num3;
			float num14 = vector.x - current.x;
			float num15 = vector.y - current.y;
			float num16 = num12 - vector.x;
			float num17 = num13 - vector.y;
			bool flag2 = num14 * num16 + num15 * num17 > 0f;
			if (flag2)
			{
				num12 = vector.x;
				num13 = vector.y;
				currentVelocity.x = (num12 - vector.x) / deltaTime;
				currentVelocity.y = (num13 - vector.y) / deltaTime;
			}
			return new Vector2(num12, num13);
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x0003723C File Offset: 0x0003543C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x00037270 File Offset: 0x00035470
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000372A4 File Offset: 0x000354A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000372D8 File Offset: 0x000354D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0003730C File Offset: 0x0003550C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x00037334 File Offset: 0x00035534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0003735C File Offset: 0x0003555C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00037384 File Offset: 0x00035584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000373AC File Offset: 0x000355AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			return num * num + num2 * num2 < 9.9999994E-11f;
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000373E8 File Offset: 0x000355E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x00037404 File Offset: 0x00035604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x00037428 File Offset: 0x00035628
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00037450 File Offset: 0x00035650
		public static Vector2 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.zeroVector;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00037468 File Offset: 0x00035668
		public static Vector2 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.oneVector;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00037480 File Offset: 0x00035680
		public static Vector2 up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.upVector;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00037498 File Offset: 0x00035698
		public static Vector2 down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.downVector;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x000374B0 File Offset: 0x000356B0
		public static Vector2 left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.leftVector;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x000374C8 File Offset: 0x000356C8
		public static Vector2 right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.rightVector;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x000374E0 File Offset: 0x000356E0
		public static Vector2 positiveInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.positiveInfinityVector;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000374F8 File Offset: 0x000356F8
		public static Vector2 negativeInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2.negativeInfinityVector;
			}
		}

		// Token: 0x040008AE RID: 2222
		public float x;

		// Token: 0x040008AF RID: 2223
		public float y;

		// Token: 0x040008B0 RID: 2224
		private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

		// Token: 0x040008B1 RID: 2225
		private static readonly Vector2 oneVector = new Vector2(1f, 1f);

		// Token: 0x040008B2 RID: 2226
		private static readonly Vector2 upVector = new Vector2(0f, 1f);

		// Token: 0x040008B3 RID: 2227
		private static readonly Vector2 downVector = new Vector2(0f, -1f);

		// Token: 0x040008B4 RID: 2228
		private static readonly Vector2 leftVector = new Vector2(-1f, 0f);

		// Token: 0x040008B5 RID: 2229
		private static readonly Vector2 rightVector = new Vector2(1f, 0f);

		// Token: 0x040008B6 RID: 2230
		private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040008B7 RID: 2231
		private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

		// Token: 0x040008B8 RID: 2232
		public const float kEpsilon = 1E-05f;

		// Token: 0x040008B9 RID: 2233
		public const float kEpsilonNormalSqrt = 1E-15f;
	}
}
