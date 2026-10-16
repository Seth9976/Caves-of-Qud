using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004F8 RID: 1272
	[UsedByNativeCode]
	[NativeHeader("Runtime/Export/Math/SphericalHarmonicsL2.bindings.h")]
	public struct SphericalHarmonicsL2 : IEquatable<SphericalHarmonicsL2>
	{
		// Token: 0x060031AE RID: 12718 RVA: 0x0005F7CE File Offset: 0x0005D9CE
		public void Clear()
		{
			this.SetZero();
		}

		// Token: 0x060031AF RID: 12719
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetZero();

		// Token: 0x060031B0 RID: 12720 RVA: 0x0005F7D8 File Offset: 0x0005D9D8
		public void AddAmbientLight(Color color)
		{
			SphericalHarmonicsL2.AddAmbientLight_Injected(ref this, ref color);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x0005F7F0 File Offset: 0x0005D9F0
		public void AddDirectionalLight(Vector3 direction, Color color, float intensity)
		{
			Color color2 = color * (2f * intensity);
			SphericalHarmonicsL2.AddDirectionalLightInternal(ref this, direction, color2);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0005F818 File Offset: 0x0005DA18
		[FreeFunction]
		private static void AddDirectionalLightInternal(ref SphericalHarmonicsL2 sh, Vector3 direction, Color color)
		{
			SphericalHarmonicsL2.AddDirectionalLightInternal_Injected(ref sh, ref direction, ref color);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x0005F830 File Offset: 0x0005DA30
		public void Evaluate(Vector3[] directions, Color[] results)
		{
			bool flag = directions == null;
			if (flag)
			{
				throw new ArgumentNullException("directions");
			}
			bool flag2 = results == null;
			if (flag2)
			{
				throw new ArgumentNullException("results");
			}
			bool flag3 = directions.Length == 0;
			if (!flag3)
			{
				bool flag4 = directions.Length != results.Length;
				if (flag4)
				{
					throw new ArgumentException("Length of the directions array and the results array must match.");
				}
				SphericalHarmonicsL2.EvaluateInternal(ref this, directions, results);
			}
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x0005F894 File Offset: 0x0005DA94
		[FreeFunction]
		private unsafe static void EvaluateInternal(ref SphericalHarmonicsL2 sh, Vector3[] directions, [Out] Color[] results)
		{
			try
			{
				Span<Vector3> span = new Span<Vector3>(directions);
				fixed (Vector3* ptr = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
					BlittableArrayWrapper blittableArrayWrapper;
					if (results != null)
					{
						fixed (Color[] array = results)
						{
							if (array.Length != 0)
							{
								blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
							}
						}
					}
					SphericalHarmonicsL2.EvaluateInternal_Injected(ref sh, ref managedSpanWrapper, out blittableArrayWrapper);
				}
			}
			finally
			{
				Vector3* ptr = null;
				Color[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Color>(ref array);
			}
		}

		// Token: 0x170007DE RID: 2014
		public float this[int rgb, int coefficient]
		{
			get
			{
				float num;
				switch (rgb * 9 + coefficient)
				{
				case 0:
					num = this.shr0;
					break;
				case 1:
					num = this.shr1;
					break;
				case 2:
					num = this.shr2;
					break;
				case 3:
					num = this.shr3;
					break;
				case 4:
					num = this.shr4;
					break;
				case 5:
					num = this.shr5;
					break;
				case 6:
					num = this.shr6;
					break;
				case 7:
					num = this.shr7;
					break;
				case 8:
					num = this.shr8;
					break;
				case 9:
					num = this.shg0;
					break;
				case 10:
					num = this.shg1;
					break;
				case 11:
					num = this.shg2;
					break;
				case 12:
					num = this.shg3;
					break;
				case 13:
					num = this.shg4;
					break;
				case 14:
					num = this.shg5;
					break;
				case 15:
					num = this.shg6;
					break;
				case 16:
					num = this.shg7;
					break;
				case 17:
					num = this.shg8;
					break;
				case 18:
					num = this.shb0;
					break;
				case 19:
					num = this.shb1;
					break;
				case 20:
					num = this.shb2;
					break;
				case 21:
					num = this.shb3;
					break;
				case 22:
					num = this.shb4;
					break;
				case 23:
					num = this.shb5;
					break;
				case 24:
					num = this.shb6;
					break;
				case 25:
					num = this.shb7;
					break;
				case 26:
					num = this.shb8;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
				return num;
			}
			set
			{
				switch (rgb * 9 + coefficient)
				{
				case 0:
					this.shr0 = value;
					break;
				case 1:
					this.shr1 = value;
					break;
				case 2:
					this.shr2 = value;
					break;
				case 3:
					this.shr3 = value;
					break;
				case 4:
					this.shr4 = value;
					break;
				case 5:
					this.shr5 = value;
					break;
				case 6:
					this.shr6 = value;
					break;
				case 7:
					this.shr7 = value;
					break;
				case 8:
					this.shr8 = value;
					break;
				case 9:
					this.shg0 = value;
					break;
				case 10:
					this.shg1 = value;
					break;
				case 11:
					this.shg2 = value;
					break;
				case 12:
					this.shg3 = value;
					break;
				case 13:
					this.shg4 = value;
					break;
				case 14:
					this.shg5 = value;
					break;
				case 15:
					this.shg6 = value;
					break;
				case 16:
					this.shg7 = value;
					break;
				case 17:
					this.shg8 = value;
					break;
				case 18:
					this.shb0 = value;
					break;
				case 19:
					this.shb1 = value;
					break;
				case 20:
					this.shb2 = value;
					break;
				case 21:
					this.shb3 = value;
					break;
				case 22:
					this.shb4 = value;
					break;
				case 23:
					this.shb5 = value;
					break;
				case 24:
					this.shb6 = value;
					break;
				case 25:
					this.shb7 = value;
					break;
				case 26:
					this.shb8 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid index!");
				}
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0005FC84 File Offset: 0x0005DE84
		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + this.shr0.GetHashCode();
			num = num * 23 + this.shr1.GetHashCode();
			num = num * 23 + this.shr2.GetHashCode();
			num = num * 23 + this.shr3.GetHashCode();
			num = num * 23 + this.shr4.GetHashCode();
			num = num * 23 + this.shr5.GetHashCode();
			num = num * 23 + this.shr6.GetHashCode();
			num = num * 23 + this.shr7.GetHashCode();
			num = num * 23 + this.shr8.GetHashCode();
			num = num * 23 + this.shg0.GetHashCode();
			num = num * 23 + this.shg1.GetHashCode();
			num = num * 23 + this.shg2.GetHashCode();
			num = num * 23 + this.shg3.GetHashCode();
			num = num * 23 + this.shg4.GetHashCode();
			num = num * 23 + this.shg5.GetHashCode();
			num = num * 23 + this.shg6.GetHashCode();
			num = num * 23 + this.shg7.GetHashCode();
			num = num * 23 + this.shg8.GetHashCode();
			num = num * 23 + this.shb0.GetHashCode();
			num = num * 23 + this.shb1.GetHashCode();
			num = num * 23 + this.shb2.GetHashCode();
			num = num * 23 + this.shb3.GetHashCode();
			num = num * 23 + this.shb4.GetHashCode();
			num = num * 23 + this.shb5.GetHashCode();
			num = num * 23 + this.shb6.GetHashCode();
			num = num * 23 + this.shb7.GetHashCode();
			return num * 23 + this.shb8.GetHashCode();
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x0005FE68 File Offset: 0x0005E068
		public override bool Equals(object other)
		{
			return other is SphericalHarmonicsL2 && this.Equals((SphericalHarmonicsL2)other);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x0005FE94 File Offset: 0x0005E094
		public bool Equals(SphericalHarmonicsL2 other)
		{
			return this == other;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0005FEB4 File Offset: 0x0005E0B4
		public static SphericalHarmonicsL2 operator *(SphericalHarmonicsL2 lhs, float rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = lhs.shr0 * rhs,
				shr1 = lhs.shr1 * rhs,
				shr2 = lhs.shr2 * rhs,
				shr3 = lhs.shr3 * rhs,
				shr4 = lhs.shr4 * rhs,
				shr5 = lhs.shr5 * rhs,
				shr6 = lhs.shr6 * rhs,
				shr7 = lhs.shr7 * rhs,
				shr8 = lhs.shr8 * rhs,
				shg0 = lhs.shg0 * rhs,
				shg1 = lhs.shg1 * rhs,
				shg2 = lhs.shg2 * rhs,
				shg3 = lhs.shg3 * rhs,
				shg4 = lhs.shg4 * rhs,
				shg5 = lhs.shg5 * rhs,
				shg6 = lhs.shg6 * rhs,
				shg7 = lhs.shg7 * rhs,
				shg8 = lhs.shg8 * rhs,
				shb0 = lhs.shb0 * rhs,
				shb1 = lhs.shb1 * rhs,
				shb2 = lhs.shb2 * rhs,
				shb3 = lhs.shb3 * rhs,
				shb4 = lhs.shb4 * rhs,
				shb5 = lhs.shb5 * rhs,
				shb6 = lhs.shb6 * rhs,
				shb7 = lhs.shb7 * rhs,
				shb8 = lhs.shb8 * rhs
			};
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00060064 File Offset: 0x0005E264
		public static SphericalHarmonicsL2 operator *(float lhs, SphericalHarmonicsL2 rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = rhs.shr0 * lhs,
				shr1 = rhs.shr1 * lhs,
				shr2 = rhs.shr2 * lhs,
				shr3 = rhs.shr3 * lhs,
				shr4 = rhs.shr4 * lhs,
				shr5 = rhs.shr5 * lhs,
				shr6 = rhs.shr6 * lhs,
				shr7 = rhs.shr7 * lhs,
				shr8 = rhs.shr8 * lhs,
				shg0 = rhs.shg0 * lhs,
				shg1 = rhs.shg1 * lhs,
				shg2 = rhs.shg2 * lhs,
				shg3 = rhs.shg3 * lhs,
				shg4 = rhs.shg4 * lhs,
				shg5 = rhs.shg5 * lhs,
				shg6 = rhs.shg6 * lhs,
				shg7 = rhs.shg7 * lhs,
				shg8 = rhs.shg8 * lhs,
				shb0 = rhs.shb0 * lhs,
				shb1 = rhs.shb1 * lhs,
				shb2 = rhs.shb2 * lhs,
				shb3 = rhs.shb3 * lhs,
				shb4 = rhs.shb4 * lhs,
				shb5 = rhs.shb5 * lhs,
				shb6 = rhs.shb6 * lhs,
				shb7 = rhs.shb7 * lhs,
				shb8 = rhs.shb8 * lhs
			};
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x00060214 File Offset: 0x0005E414
		public static SphericalHarmonicsL2 operator +(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return new SphericalHarmonicsL2
			{
				shr0 = lhs.shr0 + rhs.shr0,
				shr1 = lhs.shr1 + rhs.shr1,
				shr2 = lhs.shr2 + rhs.shr2,
				shr3 = lhs.shr3 + rhs.shr3,
				shr4 = lhs.shr4 + rhs.shr4,
				shr5 = lhs.shr5 + rhs.shr5,
				shr6 = lhs.shr6 + rhs.shr6,
				shr7 = lhs.shr7 + rhs.shr7,
				shr8 = lhs.shr8 + rhs.shr8,
				shg0 = lhs.shg0 + rhs.shg0,
				shg1 = lhs.shg1 + rhs.shg1,
				shg2 = lhs.shg2 + rhs.shg2,
				shg3 = lhs.shg3 + rhs.shg3,
				shg4 = lhs.shg4 + rhs.shg4,
				shg5 = lhs.shg5 + rhs.shg5,
				shg6 = lhs.shg6 + rhs.shg6,
				shg7 = lhs.shg7 + rhs.shg7,
				shg8 = lhs.shg8 + rhs.shg8,
				shb0 = lhs.shb0 + rhs.shb0,
				shb1 = lhs.shb1 + rhs.shb1,
				shb2 = lhs.shb2 + rhs.shb2,
				shb3 = lhs.shb3 + rhs.shb3,
				shb4 = lhs.shb4 + rhs.shb4,
				shb5 = lhs.shb5 + rhs.shb5,
				shb6 = lhs.shb6 + rhs.shb6,
				shb7 = lhs.shb7 + rhs.shb7,
				shb8 = lhs.shb8 + rhs.shb8
			};
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x0006044C File Offset: 0x0005E64C
		public static bool operator ==(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return lhs.shr0 == rhs.shr0 && lhs.shr1 == rhs.shr1 && lhs.shr2 == rhs.shr2 && lhs.shr3 == rhs.shr3 && lhs.shr4 == rhs.shr4 && lhs.shr5 == rhs.shr5 && lhs.shr6 == rhs.shr6 && lhs.shr7 == rhs.shr7 && lhs.shr8 == rhs.shr8 && lhs.shg0 == rhs.shg0 && lhs.shg1 == rhs.shg1 && lhs.shg2 == rhs.shg2 && lhs.shg3 == rhs.shg3 && lhs.shg4 == rhs.shg4 && lhs.shg5 == rhs.shg5 && lhs.shg6 == rhs.shg6 && lhs.shg7 == rhs.shg7 && lhs.shg8 == rhs.shg8 && lhs.shb0 == rhs.shb0 && lhs.shb1 == rhs.shb1 && lhs.shb2 == rhs.shb2 && lhs.shb3 == rhs.shb3 && lhs.shb4 == rhs.shb4 && lhs.shb5 == rhs.shb5 && lhs.shb6 == rhs.shb6 && lhs.shb7 == rhs.shb7 && lhs.shb8 == rhs.shb8;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x00060614 File Offset: 0x0005E814
		public static bool operator !=(SphericalHarmonicsL2 lhs, SphericalHarmonicsL2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060031BF RID: 12735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddAmbientLight_Injected(ref SphericalHarmonicsL2 _unity_self, [In] ref Color color);

		// Token: 0x060031C0 RID: 12736
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDirectionalLightInternal_Injected(ref SphericalHarmonicsL2 sh, [In] ref Vector3 direction, [In] ref Color color);

		// Token: 0x060031C1 RID: 12737
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EvaluateInternal_Injected(ref SphericalHarmonicsL2 sh, ref ManagedSpanWrapper directions, out BlittableArrayWrapper results);

		// Token: 0x04000F5C RID: 3932
		private float shr0;

		// Token: 0x04000F5D RID: 3933
		private float shr1;

		// Token: 0x04000F5E RID: 3934
		private float shr2;

		// Token: 0x04000F5F RID: 3935
		private float shr3;

		// Token: 0x04000F60 RID: 3936
		private float shr4;

		// Token: 0x04000F61 RID: 3937
		private float shr5;

		// Token: 0x04000F62 RID: 3938
		private float shr6;

		// Token: 0x04000F63 RID: 3939
		private float shr7;

		// Token: 0x04000F64 RID: 3940
		private float shr8;

		// Token: 0x04000F65 RID: 3941
		private float shg0;

		// Token: 0x04000F66 RID: 3942
		private float shg1;

		// Token: 0x04000F67 RID: 3943
		private float shg2;

		// Token: 0x04000F68 RID: 3944
		private float shg3;

		// Token: 0x04000F69 RID: 3945
		private float shg4;

		// Token: 0x04000F6A RID: 3946
		private float shg5;

		// Token: 0x04000F6B RID: 3947
		private float shg6;

		// Token: 0x04000F6C RID: 3948
		private float shg7;

		// Token: 0x04000F6D RID: 3949
		private float shg8;

		// Token: 0x04000F6E RID: 3950
		private float shb0;

		// Token: 0x04000F6F RID: 3951
		private float shb1;

		// Token: 0x04000F70 RID: 3952
		private float shb2;

		// Token: 0x04000F71 RID: 3953
		private float shb3;

		// Token: 0x04000F72 RID: 3954
		private float shb4;

		// Token: 0x04000F73 RID: 3955
		private float shb5;

		// Token: 0x04000F74 RID: 3956
		private float shb6;

		// Token: 0x04000F75 RID: 3957
		private float shb7;

		// Token: 0x04000F76 RID: 3958
		private float shb8;
	}
}
