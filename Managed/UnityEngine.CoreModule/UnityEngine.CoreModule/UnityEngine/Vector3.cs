using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000217 RID: 535
	[NativeHeader("Runtime/Math/Vector3.h")]
	[Il2CppEagerStaticClassConstruction]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("Vector3f")]
	[NativeType(Header = "Runtime/Math/Vector3.h")]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		// Token: 0x06001C2E RID: 7214 RVA: 0x00033F28 File Offset: 0x00032128
		[FreeFunction("VectorScripting::Slerp", IsThreadSafe = true)]
		public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.Slerp_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00033F44 File Offset: 0x00032144
		[FreeFunction("VectorScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			Vector3 vector;
			Vector3.SlerpUnclamped_Injected(ref a, ref b, t, out vector);
			return vector;
		}

		// Token: 0x06001C30 RID: 7216
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		// Token: 0x06001C31 RID: 7217 RVA: 0x00033F5E File Offset: 0x0003215E
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.OrthoNormalize2(ref normal, ref tangent);
		}

		// Token: 0x06001C32 RID: 7218
		[FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		// Token: 0x06001C33 RID: 7219 RVA: 0x00033F69 File Offset: 0x00032169
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00033F78 File Offset: 0x00032178
		[FreeFunction(IsThreadSafe = true)]
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			Vector3 vector;
			Vector3.RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out vector);
			return vector;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00033F94 File Offset: 0x00032194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00033FF8 File Offset: 0x000321F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
		{
			return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00034054 File Offset: 0x00032254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			bool flag = num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta);
			Vector3 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num5 = (float)Math.Sqrt((double)num4);
				vector = new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x00034100 File Offset: 0x00032300
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x00034124 File Offset: 0x00032324
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x00034150 File Offset: 0x00032350
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			float num6 = current.z - target.z;
			Vector3 vector = target;
			float num7 = maxSpeed * smoothTime;
			float num8 = num7 * num7;
			float num9 = num4 * num4 + num5 * num5 + num6 * num6;
			bool flag = num9 > num8;
			if (flag)
			{
				float num10 = (float)Math.Sqrt((double)num9);
				num4 = num4 / num10 * num7;
				num5 = num5 / num10 * num7;
				num6 = num6 / num10 * num7;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			target.z = current.z - num6;
			float num11 = (currentVelocity.x + num * num4) * deltaTime;
			float num12 = (currentVelocity.y + num * num5) * deltaTime;
			float num13 = (currentVelocity.z + num * num6) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num11) * num3;
			currentVelocity.y = (currentVelocity.y - num * num12) * num3;
			currentVelocity.z = (currentVelocity.z - num * num13) * num3;
			float num14 = target.x + (num4 + num11) * num3;
			float num15 = target.y + (num5 + num12) * num3;
			float num16 = target.z + (num6 + num13) * num3;
			float num17 = vector.x - current.x;
			float num18 = vector.y - current.y;
			float num19 = vector.z - current.z;
			float num20 = num14 - vector.x;
			float num21 = num15 - vector.y;
			float num22 = num16 - vector.z;
			bool flag2 = num17 * num20 + num18 * num21 + num19 * num22 > 0f;
			if (flag2)
			{
				num14 = vector.x;
				num15 = vector.y;
				num16 = vector.z;
				currentVelocity.x = (num14 - vector.x) / deltaTime;
				currentVelocity.y = (num15 - vector.y) / deltaTime;
				currentVelocity.z = (num16 - vector.z) / deltaTime;
			}
			return new Vector3(num14, num15, num16);
		}

		// Token: 0x1700049F RID: 1183
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00034474 File Offset: 0x00032674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0003448C File Offset: 0x0003268C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x00034474 File Offset: 0x00032674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000344A8 File Offset: 0x000326A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000344E6 File Offset: 0x000326E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00034524 File Offset: 0x00032724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x0003458C File Offset: 0x0003278C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2);
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000345C8 File Offset: 0x000327C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Vector3 vector;
			bool flag;
			if (other is Vector3)
			{
				vector = (Vector3)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(vector);
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000345FC File Offset: 0x000327FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x0003463C File Offset: 0x0003283C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			float num = -2f * Vector3.Dot(inNormal, inDirection);
			return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00034690 File Offset: 0x00032890
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			bool flag = num > 1E-05f;
			Vector3 vector;
			if (flag)
			{
				vector = value / num;
			}
			else
			{
				vector = Vector3.zero;
			}
			return vector;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000346C4 File Offset: 0x000328C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x0003470C File Offset: 0x0003290C
		public Vector3 normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x0003472C File Offset: 0x0003292C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x00034768 File Offset: 0x00032968
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = Vector3.zero;
			}
			else
			{
				float num2 = Vector3.Dot(vector, onNormal);
				vector2 = new Vector3(onNormal.x * num2 / num, onNormal.y * num2 / num, onNormal.z * num2 / num);
			}
			return vector2;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000347C4 File Offset: 0x000329C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			float num = Vector3.Dot(planeNormal, planeNormal);
			bool flag = num < Mathf.Epsilon;
			Vector3 vector2;
			if (flag)
			{
				vector2 = vector;
			}
			else
			{
				float num2 = Vector3.Dot(vector, planeNormal);
				vector2 = new Vector3(vector.x - planeNormal.x * num2 / num, vector.y - planeNormal.y * num2 / num, vector.z - planeNormal.z * num2 / num);
			}
			return vector2;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00034830 File Offset: 0x00032A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Vector3 from, Vector3 to)
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
				float num3 = Mathf.Clamp(Vector3.Dot(from, to) / num, -1f, 1f);
				num2 = (float)Math.Acos((double)num3) * 57.29578f;
			}
			return num2;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00034894 File Offset: 0x00032A94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
		{
			float num = Vector3.Angle(from, to);
			float num2 = from.y * to.z - from.z * to.y;
			float num3 = from.z * to.x - from.x * to.z;
			float num4 = from.x * to.y - from.y * to.x;
			float num5 = Mathf.Sign(axis.x * num2 + axis.y * num3 + axis.z * num4);
			return num * num5;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x0003492C File Offset: 0x00032B2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 a, Vector3 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			float num3 = a.z - b.z;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x0003497C File Offset: 0x00032B7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector3 vector2;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				float num4 = vector.z / num;
				vector2 = new Vector3(num2 * maxLength, num3 * maxLength, num4 * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000349E0 File Offset: 0x00032BE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Magnitude(Vector3 vector)
		{
			return (float)Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00034A24 File Offset: 0x00032C24
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00034A68 File Offset: 0x00032C68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float SqrMagnitude(Vector3 vector)
		{
			return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00034AA4 File Offset: 0x00032CA4
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00034AE0 File Offset: 0x00032CE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00034B2C File Offset: 0x00032D2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00034B78 File Offset: 0x00032D78
		public static Vector3 zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.zeroVector;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00034B90 File Offset: 0x00032D90
		public static Vector3 one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.oneVector;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00034BA8 File Offset: 0x00032DA8
		public static Vector3 forward
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.forwardVector;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x00034BC0 File Offset: 0x00032DC0
		public static Vector3 back
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.backVector;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00034BD8 File Offset: 0x00032DD8
		public static Vector3 up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.upVector;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00034BF0 File Offset: 0x00032DF0
		public static Vector3 down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.downVector;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00034C08 File Offset: 0x00032E08
		public static Vector3 left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.leftVector;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00034C20 File Offset: 0x00032E20
		public static Vector3 right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.rightVector;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00034C38 File Offset: 0x00032E38
		public static Vector3 positiveInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.positiveInfinityVector;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00034C50 File Offset: 0x00032E50
		public static Vector3 negativeInfinity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3.negativeInfinityVector;
			}
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00034C68 File Offset: 0x00032E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00034CA8 File Offset: 0x00032EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00034CE8 File Offset: 0x00032EE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00034D14 File Offset: 0x00032F14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00034D44 File Offset: 0x00032F44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x00034D74 File Offset: 0x00032F74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00034DA4 File Offset: 0x00032FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			return num4 < 9.9999994E-11f;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00034DF8 File Offset: 0x00032FF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00034E14 File Offset: 0x00033014
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00034E30 File Offset: 0x00033030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00034E4C File Offset: 0x0003304C
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
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00034EC4 File Offset: 0x000330C4
		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x00034EEC File Offset: 0x000330EC
		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return (float)Math.Acos((double)Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00034F28 File Offset: 0x00033128
		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return Vector3.ProjectOnPlane(fromThat, excludeThis);
		}

		// Token: 0x06001C70 RID: 7280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected([In] ref Vector3 a, [In] ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001C71 RID: 7281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected([In] ref Vector3 a, [In] ref Vector3 b, float t, out Vector3 ret);

		// Token: 0x06001C72 RID: 7282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RotateTowards_Injected([In] ref Vector3 current, [In] ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);

		// Token: 0x04000891 RID: 2193
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000892 RID: 2194
		public const float kEpsilonNormalSqrt = 1E-15f;

		// Token: 0x04000893 RID: 2195
		public float x;

		// Token: 0x04000894 RID: 2196
		public float y;

		// Token: 0x04000895 RID: 2197
		public float z;

		// Token: 0x04000896 RID: 2198
		private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

		// Token: 0x04000897 RID: 2199
		private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

		// Token: 0x04000898 RID: 2200
		private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

		// Token: 0x04000899 RID: 2201
		private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

		// Token: 0x0400089A RID: 2202
		private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

		// Token: 0x0400089B RID: 2203
		private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

		// Token: 0x0400089C RID: 2204
		private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

		// Token: 0x0400089D RID: 2205
		private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

		// Token: 0x0400089E RID: 2206
		private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x0400089F RID: 2207
		private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
