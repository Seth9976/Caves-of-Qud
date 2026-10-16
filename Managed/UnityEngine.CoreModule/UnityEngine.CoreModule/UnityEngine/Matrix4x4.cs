using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000216 RID: 534
	[NativeClass("Matrix4x4f")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeType(Header = "Runtime/Math/Matrix4x4.h")]
	[Il2CppEagerStaticClassConstruction]
	[NativeHeader("Runtime/Math/MathScripting.h")]
	public struct Matrix4x4 : IEquatable<Matrix4x4>, IFormattable
	{
		// Token: 0x06001BEB RID: 7147 RVA: 0x000327D0 File Offset: 0x000309D0
		[ThreadSafe]
		private Quaternion GetRotation()
		{
			Quaternion quaternion;
			Matrix4x4.GetRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x000327E8 File Offset: 0x000309E8
		[ThreadSafe]
		private Vector3 GetLossyScale()
		{
			Vector3 vector;
			Matrix4x4.GetLossyScale_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06001BED RID: 7149
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsIdentity();

		// Token: 0x06001BEE RID: 7150
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetDeterminant();

		// Token: 0x06001BEF RID: 7151 RVA: 0x00032800 File Offset: 0x00030A00
		[ThreadSafe]
		private FrustumPlanes DecomposeProjection()
		{
			FrustumPlanes frustumPlanes;
			Matrix4x4.DecomposeProjection_Injected(ref this, out frustumPlanes);
			return frustumPlanes;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x00032818 File Offset: 0x00030A18
		public Quaternion rotation
		{
			get
			{
				return this.GetRotation();
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x00032830 File Offset: 0x00030A30
		public Vector3 lossyScale
		{
			get
			{
				return this.GetLossyScale();
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x00032848 File Offset: 0x00030A48
		public bool isIdentity
		{
			get
			{
				return this.IsIdentity();
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00032860 File Offset: 0x00030A60
		public float determinant
		{
			get
			{
				return this.GetDeterminant();
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x00032878 File Offset: 0x00030A78
		public FrustumPlanes decomposeProjection
		{
			get
			{
				return this.DecomposeProjection();
			}
		}

		// Token: 0x06001BF5 RID: 7157
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool ValidTRS();

		// Token: 0x06001BF6 RID: 7158 RVA: 0x00032890 File Offset: 0x00030A90
		public static float Determinant(Matrix4x4 m)
		{
			return m.determinant;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000328AC File Offset: 0x00030AAC
		[FreeFunction("MatrixScripting::TRS", IsThreadSafe = true)]
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.TRS_Injected(ref pos, ref q, ref s, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000328C7 File Offset: 0x00030AC7
		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000328D8 File Offset: 0x00030AD8
		[FreeFunction("MatrixScripting::Inverse3DAffine", IsThreadSafe = true)]
		public static bool Inverse3DAffine(Matrix4x4 input, ref Matrix4x4 result)
		{
			return Matrix4x4.Inverse3DAffine_Injected(ref input, ref result);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000328F0 File Offset: 0x00030AF0
		[FreeFunction("MatrixScripting::Inverse", IsThreadSafe = true)]
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Inverse_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x00032908 File Offset: 0x00030B08
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x00032928 File Offset: 0x00030B28
		[FreeFunction("MatrixScripting::Transpose", IsThreadSafe = true)]
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Transpose_Injected(ref m, out matrix4x);
			return matrix4x;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00032940 File Offset: 0x00030B40
		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x00032960 File Offset: 0x00030B60
		[FreeFunction("MatrixScripting::Ortho", IsThreadSafe = true)]
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Ortho_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00032980 File Offset: 0x00030B80
		[FreeFunction("MatrixScripting::Perspective", IsThreadSafe = true)]
		public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Perspective_Injected(fov, aspect, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0003299C File Offset: 0x00030B9C
		[FreeFunction("MatrixScripting::LookAt", IsThreadSafe = true)]
		public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.LookAt_Injected(ref from, ref to, ref up, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000329B8 File Offset: 0x00030BB8
		[FreeFunction("MatrixScripting::Frustum", IsThreadSafe = true)]
		public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
		{
			Matrix4x4 matrix4x;
			Matrix4x4.Frustum_Injected(left, right, bottom, top, zNear, zFar, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000329D8 File Offset: 0x00030BD8
		public static Matrix4x4 Frustum(FrustumPlanes fp)
		{
			return Matrix4x4.Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
		}

		// Token: 0x06001C03 RID: 7171 RVA: 0x00032A14 File Offset: 0x00030C14
		[FreeFunction("MatrixScripting::Internal_CompareApproximately", IsThreadSafe = true)]
		internal static bool CompareApproximately(Matrix4x4 a, Matrix4x4 b, float threshold)
		{
			return Matrix4x4.CompareApproximately_Injected(ref a, ref b, threshold);
		}

		// Token: 0x06001C04 RID: 7172 RVA: 0x00032A2C File Offset: 0x00030C2C
		public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
		{
			this.m00 = column0.x;
			this.m01 = column1.x;
			this.m02 = column2.x;
			this.m03 = column3.x;
			this.m10 = column0.y;
			this.m11 = column1.y;
			this.m12 = column2.y;
			this.m13 = column3.y;
			this.m20 = column0.z;
			this.m21 = column1.z;
			this.m22 = column2.z;
			this.m23 = column3.z;
			this.m30 = column0.w;
			this.m31 = column1.w;
			this.m32 = column2.w;
			this.m33 = column3.w;
		}

		// Token: 0x1700049B RID: 1179
		public float this[int row, int column]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this[row + column * 4];
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		public float this[int index]
		{
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.m00;
					break;
				case 1:
					num = this.m10;
					break;
				case 2:
					num = this.m20;
					break;
				case 3:
					num = this.m30;
					break;
				case 4:
					num = this.m01;
					break;
				case 5:
					num = this.m11;
					break;
				case 6:
					num = this.m21;
					break;
				case 7:
					num = this.m31;
					break;
				case 8:
					num = this.m02;
					break;
				case 9:
					num = this.m12;
					break;
				case 10:
					num = this.m22;
					break;
				case 11:
					num = this.m32;
					break;
				case 12:
					num = this.m03;
					break;
				case 13:
					num = this.m13;
					break;
				case 14:
					num = this.m23;
					break;
				case 15:
					num = this.m33;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
				return num;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00032D38 File Offset: 0x00030F38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00032DA8 File Offset: 0x00030FA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			Matrix4x4 matrix4x;
			bool flag;
			if (other is Matrix4x4)
			{
				matrix4x = (Matrix4x4)other;
				flag = true;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			return flag2 && this.Equals(matrix4x);
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00032DDC File Offset: 0x00030FDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Matrix4x4 other)
		{
			return this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1)) && this.GetColumn(2).Equals(other.GetColumn(2)) && this.GetColumn(3).Equals(other.GetColumn(3));
		}

		// Token: 0x06001C0C RID: 7180 RVA: 0x00032E54 File Offset: 0x00031054
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30;
			matrix4x.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31;
			matrix4x.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32;
			matrix4x.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33;
			matrix4x.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30;
			matrix4x.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31;
			matrix4x.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32;
			matrix4x.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33;
			matrix4x.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30;
			matrix4x.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31;
			matrix4x.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32;
			matrix4x.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33;
			matrix4x.m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30;
			matrix4x.m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31;
			matrix4x.m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32;
			matrix4x.m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33;
			return matrix4x;
		}

		// Token: 0x06001C0D RID: 7181 RVA: 0x00033248 File Offset: 0x00031448
		public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
		{
			Vector4 vector2;
			vector2.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z + lhs.m03 * vector.w;
			vector2.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z + lhs.m13 * vector.w;
			vector2.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z + lhs.m23 * vector.w;
			vector2.w = lhs.m30 * vector.x + lhs.m31 * vector.y + lhs.m32 * vector.z + lhs.m33 * vector.w;
			return vector2;
		}

		// Token: 0x06001C0E RID: 7182 RVA: 0x00033354 File Offset: 0x00031554
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x000333C4 File Offset: 0x000315C4
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000333E0 File Offset: 0x000315E0
		public Vector4 GetColumn(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m10, this.m20, this.m30);
				break;
			case 1:
				vector = new Vector4(this.m01, this.m11, this.m21, this.m31);
				break;
			case 2:
				vector = new Vector4(this.m02, this.m12, this.m22, this.m32);
				break;
			case 3:
				vector = new Vector4(this.m03, this.m13, this.m23, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid column index!");
			}
			return vector;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x0003349C File Offset: 0x0003169C
		public Vector4 GetRow(int index)
		{
			Vector4 vector;
			switch (index)
			{
			case 0:
				vector = new Vector4(this.m00, this.m01, this.m02, this.m03);
				break;
			case 1:
				vector = new Vector4(this.m10, this.m11, this.m12, this.m13);
				break;
			case 2:
				vector = new Vector4(this.m20, this.m21, this.m22, this.m23);
				break;
			case 3:
				vector = new Vector4(this.m30, this.m31, this.m32, this.m33);
				break;
			default:
				throw new IndexOutOfRangeException("Invalid row index!");
			}
			return vector;
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00033558 File Offset: 0x00031758
		public Vector3 GetPosition()
		{
			return new Vector3(this.m03, this.m13, this.m23);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00033581 File Offset: 0x00031781
		public void SetColumn(int index, Vector4 column)
		{
			this[0, index] = column.x;
			this[1, index] = column.y;
			this[2, index] = column.z;
			this[3, index] = column.w;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000335C0 File Offset: 0x000317C0
		public void SetRow(int index, Vector4 row)
		{
			this[index, 0] = row.x;
			this[index, 1] = row.y;
			this[index, 2] = row.z;
			this[index, 3] = row.w;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00033600 File Offset: 0x00031800
		public Vector3 MultiplyPoint(Vector3 point)
		{
			Vector3 vector;
			vector.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			vector.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			vector.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			float num = this.m30 * point.x + this.m31 * point.y + this.m32 * point.z + this.m33;
			num = 1f / num;
			vector.x *= num;
			vector.y *= num;
			vector.z *= num;
			return vector;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00033718 File Offset: 0x00031918
		public Vector3 MultiplyPoint3x4(Vector3 point)
		{
			Vector3 vector;
			vector.x = this.m00 * point.x + this.m01 * point.y + this.m02 * point.z + this.m03;
			vector.y = this.m10 * point.x + this.m11 * point.y + this.m12 * point.z + this.m13;
			vector.z = this.m20 * point.x + this.m21 * point.y + this.m22 * point.z + this.m23;
			return vector;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x000337D0 File Offset: 0x000319D0
		public Vector3 MultiplyVector(Vector3 vector)
		{
			Vector3 vector2;
			vector2.x = this.m00 * vector.x + this.m01 * vector.y + this.m02 * vector.z;
			vector2.y = this.m10 * vector.x + this.m11 * vector.y + this.m12 * vector.z;
			vector2.z = this.m20 * vector.x + this.m21 * vector.y + this.m22 * vector.z;
			return vector2;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00033874 File Offset: 0x00031A74
		public Plane TransformPlane(Plane plane)
		{
			Matrix4x4 inverse = this.inverse;
			float x = plane.normal.x;
			float y = plane.normal.y;
			float z = plane.normal.z;
			float distance = plane.distance;
			float num = inverse.m00 * x + inverse.m10 * y + inverse.m20 * z + inverse.m30 * distance;
			float num2 = inverse.m01 * x + inverse.m11 * y + inverse.m21 * z + inverse.m31 * distance;
			float num3 = inverse.m02 * x + inverse.m12 * y + inverse.m22 * z + inverse.m32 * distance;
			float num4 = inverse.m03 * x + inverse.m13 * y + inverse.m23 * z + inverse.m33 * distance;
			return new Plane(new Vector3(num, num2, num3), num4);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0003396C File Offset: 0x00031B6C
		public static Matrix4x4 Scale(Vector3 vector)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = vector.x;
			matrix4x.m01 = 0f;
			matrix4x.m02 = 0f;
			matrix4x.m03 = 0f;
			matrix4x.m10 = 0f;
			matrix4x.m11 = vector.y;
			matrix4x.m12 = 0f;
			matrix4x.m13 = 0f;
			matrix4x.m20 = 0f;
			matrix4x.m21 = 0f;
			matrix4x.m22 = vector.z;
			matrix4x.m23 = 0f;
			matrix4x.m30 = 0f;
			matrix4x.m31 = 0f;
			matrix4x.m32 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00033A44 File Offset: 0x00031C44
		public static Matrix4x4 Translate(Vector3 vector)
		{
			Matrix4x4 matrix4x;
			matrix4x.m00 = 1f;
			matrix4x.m01 = 0f;
			matrix4x.m02 = 0f;
			matrix4x.m03 = vector.x;
			matrix4x.m10 = 0f;
			matrix4x.m11 = 1f;
			matrix4x.m12 = 0f;
			matrix4x.m13 = vector.y;
			matrix4x.m20 = 0f;
			matrix4x.m21 = 0f;
			matrix4x.m22 = 1f;
			matrix4x.m23 = vector.z;
			matrix4x.m30 = 0f;
			matrix4x.m31 = 0f;
			matrix4x.m32 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00033B1C File Offset: 0x00031D1C
		public static Matrix4x4 Rotate(Quaternion q)
		{
			float num = q.x * 2f;
			float num2 = q.y * 2f;
			float num3 = q.z * 2f;
			float num4 = q.x * num;
			float num5 = q.y * num2;
			float num6 = q.z * num3;
			float num7 = q.x * num2;
			float num8 = q.x * num3;
			float num9 = q.y * num3;
			float num10 = q.w * num;
			float num11 = q.w * num2;
			float num12 = q.w * num3;
			Matrix4x4 matrix4x;
			matrix4x.m00 = 1f - (num5 + num6);
			matrix4x.m10 = num7 + num12;
			matrix4x.m20 = num8 - num11;
			matrix4x.m30 = 0f;
			matrix4x.m01 = num7 - num12;
			matrix4x.m11 = 1f - (num4 + num6);
			matrix4x.m21 = num9 + num10;
			matrix4x.m31 = 0f;
			matrix4x.m02 = num8 + num11;
			matrix4x.m12 = num9 - num10;
			matrix4x.m22 = 1f - (num4 + num5);
			matrix4x.m32 = 0f;
			matrix4x.m03 = 0f;
			matrix4x.m13 = 0f;
			matrix4x.m23 = 0f;
			matrix4x.m33 = 1f;
			return matrix4x;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x00033C84 File Offset: 0x00031E84
		public static Matrix4x4 zero
		{
			get
			{
				return Matrix4x4.zeroMatrix;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00033C9C File Offset: 0x00031E9C
		public static Matrix4x4 identity
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Matrix4x4.identityMatrix;
			}
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00033CB4 File Offset: 0x00031EB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00033CD0 File Offset: 0x00031ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00033CEC File Offset: 0x00031EEC
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
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format, formatProvider),
				this.m01.ToString(format, formatProvider),
				this.m02.ToString(format, formatProvider),
				this.m03.ToString(format, formatProvider),
				this.m10.ToString(format, formatProvider),
				this.m11.ToString(format, formatProvider),
				this.m12.ToString(format, formatProvider),
				this.m13.ToString(format, formatProvider),
				this.m20.ToString(format, formatProvider),
				this.m21.ToString(format, formatProvider),
				this.m22.ToString(format, formatProvider),
				this.m23.ToString(format, formatProvider),
				this.m30.ToString(format, formatProvider),
				this.m31.ToString(format, formatProvider),
				this.m32.ToString(format, formatProvider),
				this.m33.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001C22 RID: 7202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);

		// Token: 0x06001C23 RID: 7203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);

		// Token: 0x06001C24 RID: 7204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);

		// Token: 0x06001C25 RID: 7205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void TRS_Injected([In] ref Vector3 pos, [In] ref Quaternion q, [In] ref Vector3 s, out Matrix4x4 ret);

		// Token: 0x06001C26 RID: 7206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Inverse3DAffine_Injected([In] ref Matrix4x4 input, ref Matrix4x4 result);

		// Token: 0x06001C27 RID: 7207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Inverse_Injected([In] ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x06001C28 RID: 7208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Transpose_Injected([In] ref Matrix4x4 m, out Matrix4x4 ret);

		// Token: 0x06001C29 RID: 7209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x06001C2A RID: 7210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x06001C2B RID: 7211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LookAt_Injected([In] ref Vector3 from, [In] ref Vector3 to, [In] ref Vector3 up, out Matrix4x4 ret);

		// Token: 0x06001C2C RID: 7212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);

		// Token: 0x06001C2D RID: 7213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareApproximately_Injected([In] ref Matrix4x4 a, [In] ref Matrix4x4 b, float threshold);

		// Token: 0x0400087F RID: 2175
		[NativeName("m_Data[0]")]
		public float m00;

		// Token: 0x04000880 RID: 2176
		[NativeName("m_Data[1]")]
		public float m10;

		// Token: 0x04000881 RID: 2177
		[NativeName("m_Data[2]")]
		public float m20;

		// Token: 0x04000882 RID: 2178
		[NativeName("m_Data[3]")]
		public float m30;

		// Token: 0x04000883 RID: 2179
		[NativeName("m_Data[4]")]
		public float m01;

		// Token: 0x04000884 RID: 2180
		[NativeName("m_Data[5]")]
		public float m11;

		// Token: 0x04000885 RID: 2181
		[NativeName("m_Data[6]")]
		public float m21;

		// Token: 0x04000886 RID: 2182
		[NativeName("m_Data[7]")]
		public float m31;

		// Token: 0x04000887 RID: 2183
		[NativeName("m_Data[8]")]
		public float m02;

		// Token: 0x04000888 RID: 2184
		[NativeName("m_Data[9]")]
		public float m12;

		// Token: 0x04000889 RID: 2185
		[NativeName("m_Data[10]")]
		public float m22;

		// Token: 0x0400088A RID: 2186
		[NativeName("m_Data[11]")]
		public float m32;

		// Token: 0x0400088B RID: 2187
		[NativeName("m_Data[12]")]
		public float m03;

		// Token: 0x0400088C RID: 2188
		[NativeName("m_Data[13]")]
		public float m13;

		// Token: 0x0400088D RID: 2189
		[NativeName("m_Data[14]")]
		public float m23;

		// Token: 0x0400088E RID: 2190
		[NativeName("m_Data[15]")]
		public float m33;

		// Token: 0x0400088F RID: 2191
		private static readonly Matrix4x4 zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));

		// Token: 0x04000890 RID: 2192
		private static readonly Matrix4x4 identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
	}
}
