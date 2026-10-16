using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000240 RID: 576
	[NativeHeader("Runtime/Export/Random/Random.bindings.h")]
	public static class Random
	{
		// Token: 0x06001E81 RID: 7809
		[NativeMethod("SetSeed")]
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void InitState(int seed);

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0003AC2C File Offset: 0x00038E2C
		// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0003AC44 File Offset: 0x00038E44
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static Random.State state
		{
			get
			{
				Random.State state;
				Random.get_state_Injected(out state);
				return state;
			}
			set
			{
				Random.set_state_Injected(ref value);
			}
		}

		// Token: 0x06001E84 RID: 7812
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Range(float minInclusive, float maxInclusive);

		// Token: 0x06001E85 RID: 7813 RVA: 0x0003AC58 File Offset: 0x00038E58
		public static int Range(int minInclusive, int maxExclusive)
		{
			return Random.RandomRangeInt(minInclusive, maxExclusive);
		}

		// Token: 0x06001E86 RID: 7814
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RandomRangeInt(int minInclusive, int maxExclusive);

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001E87 RID: 7815
		public static extern float value
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0003AC74 File Offset: 0x00038E74
		public static Vector3 insideUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 vector;
				Random.get_insideUnitSphere_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x06001E89 RID: 7817
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRandomUnitCircle(out Vector2 output);

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0003AC8C File Offset: 0x00038E8C
		public static Vector2 insideUnitCircle
		{
			get
			{
				Vector2 vector;
				Random.GetRandomUnitCircle(out vector);
				return vector;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0003ACA8 File Offset: 0x00038EA8
		public static Vector3 onUnitSphere
		{
			[FreeFunction]
			get
			{
				Vector3 vector;
				Random.get_onUnitSphere_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0003ACC0 File Offset: 0x00038EC0
		public static Quaternion rotation
		{
			[FreeFunction]
			get
			{
				Quaternion quaternion;
				Random.get_rotation_Injected(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0003ACD8 File Offset: 0x00038ED8
		public static Quaternion rotationUniform
		{
			[FreeFunction]
			get
			{
				Quaternion quaternion;
				Random.get_rotationUniform_Injected(out quaternion);
				return quaternion;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001E8E RID: 7822
		// (set) Token: 0x06001E8F RID: 7823
		[Obsolete("Deprecated. Use InitState() function or Random.state property instead.")]
		[StaticAccessor("GetScriptingRand()", StaticAccessorType.Dot)]
		public static extern int seed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0003ACF0 File Offset: 0x00038EF0
		[Obsolete("Use Random.Range instead")]
		public static float RandomRange(float min, float max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x0003AD0C File Offset: 0x00038F0C
		[Obsolete("Use Random.Range instead")]
		public static int RandomRange(int min, int max)
		{
			return Random.Range(min, max);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x0003AD28 File Offset: 0x00038F28
		public static Color ColorHSV()
		{
			return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x0003AD68 File Offset: 0x00038F68
		public static Color ColorHSV(float hueMin, float hueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x0003ADD0 File Offset: 0x00038FD0
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
		{
			return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x0003ADFC File Offset: 0x00038FFC
		public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
		{
			float num = Mathf.Lerp(hueMin, hueMax, Random.value);
			float num2 = Mathf.Lerp(saturationMin, saturationMax, Random.value);
			float num3 = Mathf.Lerp(valueMin, valueMax, Random.value);
			Color color = Color.HSVToRGB(num, num2, num3, true);
			color.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
			return color;
		}

		// Token: 0x06001E97 RID: 7831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_state_Injected(out Random.State ret);

		// Token: 0x06001E98 RID: 7832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_state_Injected([In] ref Random.State value);

		// Token: 0x06001E99 RID: 7833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_insideUnitSphere_Injected(out Vector3 ret);

		// Token: 0x06001E9A RID: 7834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_onUnitSphere_Injected(out Vector3 ret);

		// Token: 0x06001E9B RID: 7835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotation_Injected(out Quaternion ret);

		// Token: 0x06001E9C RID: 7836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationUniform_Injected(out Quaternion ret);

		// Token: 0x02000241 RID: 577
		[Serializable]
		public struct State
		{
			// Token: 0x04000914 RID: 2324
			[SerializeField]
			private int s0;

			// Token: 0x04000915 RID: 2325
			[SerializeField]
			private int s1;

			// Token: 0x04000916 RID: 2326
			[SerializeField]
			private int s2;

			// Token: 0x04000917 RID: 2327
			[SerializeField]
			private int s3;
		}
	}
}
