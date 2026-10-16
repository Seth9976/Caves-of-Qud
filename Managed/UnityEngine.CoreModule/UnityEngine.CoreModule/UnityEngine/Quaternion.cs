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
	// Token: 0x02000218 RID: 536
	[NativeType(Header = "Runtime/Math/Quaternion.h")]
	[Il2CppEagerStaticClassConstruction]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	public struct Quaternion : IEquatable<Quaternion>, IFormattable
	{
		// Token: 0x06001C73 RID: 7283 RVA: 0x0003504C File Offset: 0x0003324C
		[FreeFunction("FromToQuaternionSafe", IsThreadSafe = true)]
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			Quaternion quaternion;
			Quaternion.FromToRotation_Injected(ref fromDirection, ref toDirection, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00035068 File Offset: 0x00033268
		[FreeFunction(IsThreadSafe = true)]
		public static Quaternion Inverse(Quaternion rotation)
		{
			Quaternion quaternion;
			Quaternion.Inverse_Injected(ref rotation, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x00035080 File Offset: 0x00033280
		[FreeFunction("QuaternionScripting::Slerp", IsThreadSafe = true)]
		public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.Slerp_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x0003509C File Offset: 0x0003329C
		[FreeFunction("QuaternionScripting::SlerpUnclamped", IsThreadSafe = true)]
		public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000350B8 File Offset: 0x000332B8
		[FreeFunction("QuaternionScripting::Lerp", IsThreadSafe = true)]
		public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.Lerp_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000350D4 File Offset: 0x000332D4
		[FreeFunction("QuaternionScripting::LerpUnclamped", IsThreadSafe = true)]
		public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
		{
			Quaternion quaternion;
			Quaternion.LerpUnclamped_Injected(ref a, ref b, t, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000350F0 File Offset: 0x000332F0
		[FreeFunction("EulerToQuaternion", IsThreadSafe = true)]
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			Quaternion quaternion;
			Quaternion.Internal_FromEulerRad_Injected(ref euler, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x00035108 File Offset: 0x00033308
		[FreeFunction("QuaternionScripting::ToEuler", IsThreadSafe = true)]
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			Vector3 vector;
			Quaternion.Internal_ToEulerRad_Injected(ref rotation, out vector);
			return vector;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x00035120 File Offset: 0x00033320
		[FreeFunction("QuaternionScripting::ToAxisAngle", IsThreadSafe = true)]
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00035138 File Offset: 0x00033338
		[FreeFunction("QuaternionScripting::AngleAxis", IsThreadSafe = true)]
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			Quaternion quaternion;
			Quaternion.AngleAxis_Injected(angle, ref axis, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x00035150 File Offset: 0x00033350
		[FreeFunction("QuaternionScripting::LookRotation", IsThreadSafe = true)]
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			Quaternion quaternion;
			Quaternion.LookRotation_Injected(ref forward, ref upwards, out quaternion);
			return quaternion;
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x0003516C File Offset: 0x0003336C
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			return Quaternion.LookRotation(forward, Vector3.up);
		}

		// Token: 0x170004AE RID: 1198
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x00035241 File Offset: 0x00033441
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00035241 File Offset: 0x00033441
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00035264 File Offset: 0x00033464
		public static Quaternion identity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.identityQuaternion;
			}
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x0003527C File Offset: 0x0003347C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00035370 File Offset: 0x00033570
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 vector;
			vector.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			vector.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			vector.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return vector;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000354A0 File Offset: 0x000336A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsEqualUsingDot(float dot)
		{
			return dot > 0.999999f;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000354BC File Offset: 0x000336BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000354DC File Offset: 0x000336DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000354F8 File Offset: 0x000336F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00035544 File Offset: 0x00033744
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00035561 File Offset: 0x00033761
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation(view, up);
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00035574 File Offset: 0x00033774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Angle(Quaternion a, Quaternion b)
		{
			float num = Mathf.Min(Mathf.Abs(Quaternion.Dot(a, b)), 1f);
			return Quaternion.IsEqualUsingDot(num) ? 0f : (Mathf.Acos(num) * 2f * 57.29578f);
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x000355C0 File Offset: 0x000337C0
		private static Vector3 Internal_MakePositive(Vector3 euler)
		{
			float num = -0.005729578f;
			float num2 = 360f + num;
			bool flag = euler.x < num;
			if (flag)
			{
				euler.x += 360f;
			}
			else
			{
				bool flag2 = euler.x > num2;
				if (flag2)
				{
					euler.x -= 360f;
				}
			}
			bool flag3 = euler.y < num;
			if (flag3)
			{
				euler.y += 360f;
			}
			else
			{
				bool flag4 = euler.y > num2;
				if (flag4)
				{
					euler.y -= 360f;
				}
			}
			bool flag5 = euler.z < num;
			if (flag5)
			{
				euler.z += 360f;
			}
			else
			{
				bool flag6 = euler.z > num2;
				if (flag6)
				{
					euler.z -= 360f;
				}
			}
			return euler;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x000356A0 File Offset: 0x000338A0
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x000356CC File Offset: 0x000338CC
		public Vector3 eulerAngles
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this = Quaternion.Internal_FromEulerRad(value * 0.017453292f);
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000356E8 File Offset: 0x000338E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00035714 File Offset: 0x00033914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00035736 File Offset: 0x00033936
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00035751 File Offset: 0x00033951
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00035764 File Offset: 0x00033964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			bool flag = num == 0f;
			Quaternion quaternion;
			if (flag)
			{
				quaternion = to;
			}
			else
			{
				quaternion = Quaternion.SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / num));
			}
			return quaternion;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000357A4 File Offset: 0x000339A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion Normalize(Quaternion q)
		{
			float num = Mathf.Sqrt(Quaternion.Dot(q, q));
			bool flag = num < Mathf.Epsilon;
			Quaternion quaternion;
			if (flag)
			{
				quaternion = Quaternion.identity;
			}
			else
			{
				quaternion = new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
			}
			return quaternion;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000357FC File Offset: 0x000339FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Normalize()
		{
			this = Quaternion.Normalize(this);
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x00035810 File Offset: 0x00033A10
		public Quaternion normalized
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Quaternion.Normalize(this);
			}
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00035830 File Offset: 0x00033A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00035878 File Offset: 0x00033A78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Quaternion quaternion;
			bool flag;
			if (other is Quaternion)
			{
				quaternion = (Quaternion)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(quaternion);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000358AC File Offset: 0x00033AAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Quaternion other)
		{
			return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0003590C File Offset: 0x00033B0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00035928 File Offset: 0x00033B28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00035944 File Offset: 0x00033B44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F5";
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

		// Token: 0x06001C9E RID: 7326 RVA: 0x000359CC File Offset: 0x00033BCC
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000359EC File Offset: 0x00033BEC
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00035A04 File Offset: 0x00033C04
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00035A1A File Offset: 0x00033C1A
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00035A2C File Offset: 0x00033C2C
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00035A4C File Offset: 0x00033C4C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00035A6C File Offset: 0x00033C6C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00035A84 File Offset: 0x00033C84
		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00035A95 File Offset: 0x00033C95
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new Vector3(x, y, z));
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00035AA7 File Offset: 0x00033CA7
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetEulerAngles(Vector3 euler)
		{
			this = Quaternion.EulerRotation(euler);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x00035AB8 File Offset: 0x00033CB8
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00035AD0 File Offset: 0x00033CD0
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3 ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00035AED File Offset: 0x00033CED
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees.")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00035B00 File Offset: 0x00033D00
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Quaternion AxisAngle(Vector3 axis, float angle)
		{
			return Quaternion.AngleAxis(57.29578f * angle, axis);
		}

		// Token: 0x06001CAD RID: 7341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FromToRotation_Injected([In] ref Vector3 fromDirection, [In] ref Vector3 toDirection, out Quaternion ret);

		// Token: 0x06001CAE RID: 7342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected([In] ref Quaternion rotation, out Quaternion ret);

		// Token: 0x06001CAF RID: 7343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Slerp_Injected([In] ref Quaternion a, [In] ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001CB0 RID: 7344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SlerpUnclamped_Injected([In] ref Quaternion a, [In] ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001CB1 RID: 7345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Lerp_Injected([In] ref Quaternion a, [In] ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001CB2 RID: 7346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LerpUnclamped_Injected([In] ref Quaternion a, [In] ref Quaternion b, float t, out Quaternion ret);

		// Token: 0x06001CB3 RID: 7347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_FromEulerRad_Injected([In] ref Vector3 euler, out Quaternion ret);

		// Token: 0x06001CB4 RID: 7348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToEulerRad_Injected([In] ref Quaternion rotation, out Vector3 ret);

		// Token: 0x06001CB5 RID: 7349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ToAxisAngleRad_Injected([In] ref Quaternion q, out Vector3 axis, out float angle);

		// Token: 0x06001CB6 RID: 7350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AngleAxis_Injected(float angle, [In] ref Vector3 axis, out Quaternion ret);

		// Token: 0x06001CB7 RID: 7351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookRotation_Injected([In] ref Vector3 forward, [DefaultValue("Vector3.up")] [In] ref Vector3 upwards, out Quaternion ret);

		// Token: 0x040008A0 RID: 2208
		public float x;

		// Token: 0x040008A1 RID: 2209
		public float y;

		// Token: 0x040008A2 RID: 2210
		public float z;

		// Token: 0x040008A3 RID: 2211
		public float w;

		// Token: 0x040008A4 RID: 2212
		private static readonly Quaternion identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);

		// Token: 0x040008A5 RID: 2213
		public const float kEpsilon = 1E-06f;
	}
}
