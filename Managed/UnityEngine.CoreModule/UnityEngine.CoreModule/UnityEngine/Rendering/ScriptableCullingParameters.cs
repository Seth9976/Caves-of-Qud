using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000531 RID: 1329
	[UsedByNativeCode]
	public struct ScriptableCullingParameters : IEquatable<ScriptableCullingParameters>
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x0006323C File Offset: 0x0006143C
		// (set) Token: 0x060032D1 RID: 13009 RVA: 0x00063254 File Offset: 0x00061454
		public int maximumVisibleLights
		{
			get
			{
				return this.m_maximumVisibleLights;
			}
			set
			{
				this.m_maximumVisibleLights = value;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x00063260 File Offset: 0x00061460
		// (set) Token: 0x060032D3 RID: 13011 RVA: 0x00063278 File Offset: 0x00061478
		public bool conservativeEnclosingSphere
		{
			get
			{
				return this.m_ConservativeEnclosingSphere;
			}
			set
			{
				this.m_ConservativeEnclosingSphere = value;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x00063284 File Offset: 0x00061484
		// (set) Token: 0x060032D5 RID: 13013 RVA: 0x0006329C File Offset: 0x0006149C
		public int numIterationsEnclosingSphere
		{
			get
			{
				return this.m_NumIterationsEnclosingSphere;
			}
			set
			{
				this.m_NumIterationsEnclosingSphere = value;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000632A8 File Offset: 0x000614A8
		// (set) Token: 0x060032D7 RID: 13015 RVA: 0x000632C0 File Offset: 0x000614C0
		public int cullingPlaneCount
		{
			get
			{
				return this.m_CullingPlaneCount;
			}
			set
			{
				bool flag = value < 0 || value > 10;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "value", value, 10));
				}
				this.m_CullingPlaneCount = value;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00063308 File Offset: 0x00061508
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x00063325 File Offset: 0x00061525
		public bool isOrthographic
		{
			get
			{
				return this.m_LODParameters.isOrthographic;
			}
			set
			{
				this.m_LODParameters.isOrthographic = value;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x00063338 File Offset: 0x00061538
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x00063350 File Offset: 0x00061550
		public LODParameters lodParameters
		{
			get
			{
				return this.m_LODParameters;
			}
			set
			{
				this.m_LODParameters = value;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x0006335C File Offset: 0x0006155C
		// (set) Token: 0x060032DD RID: 13021 RVA: 0x00063374 File Offset: 0x00061574
		public uint cullingMask
		{
			get
			{
				return this.m_CullingMask;
			}
			set
			{
				this.m_CullingMask = value;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x00063380 File Offset: 0x00061580
		// (set) Token: 0x060032DF RID: 13023 RVA: 0x00063398 File Offset: 0x00061598
		public Matrix4x4 cullingMatrix
		{
			get
			{
				return this.m_CullingMatrix;
			}
			set
			{
				this.m_CullingMatrix = value;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x000633A4 File Offset: 0x000615A4
		// (set) Token: 0x060032E1 RID: 13025 RVA: 0x000633BC File Offset: 0x000615BC
		public Vector3 origin
		{
			get
			{
				return this.m_Origin;
			}
			set
			{
				this.m_Origin = value;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x000633C8 File Offset: 0x000615C8
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x000633E0 File Offset: 0x000615E0
		public float shadowDistance
		{
			get
			{
				return this.m_ShadowDistance;
			}
			set
			{
				this.m_ShadowDistance = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x000633EC File Offset: 0x000615EC
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x00063404 File Offset: 0x00061604
		public float shadowNearPlaneOffset
		{
			get
			{
				return this.m_ShadowNearPlaneOffset;
			}
			set
			{
				this.m_ShadowNearPlaneOffset = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x00063410 File Offset: 0x00061610
		// (set) Token: 0x060032E7 RID: 13031 RVA: 0x00063428 File Offset: 0x00061628
		public CullingOptions cullingOptions
		{
			get
			{
				return this.m_CullingOptions;
			}
			set
			{
				this.m_CullingOptions = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x00063434 File Offset: 0x00061634
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x0006344C File Offset: 0x0006164C
		public ReflectionProbeSortingCriteria reflectionProbeSortingCriteria
		{
			get
			{
				return this.m_ReflectionProbeSortingCriteria;
			}
			set
			{
				this.m_ReflectionProbeSortingCriteria = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x00063458 File Offset: 0x00061658
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x00063470 File Offset: 0x00061670
		public CameraProperties cameraProperties
		{
			get
			{
				return this.m_CameraProperties;
			}
			set
			{
				this.m_CameraProperties = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x0006347C File Offset: 0x0006167C
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x00063494 File Offset: 0x00061694
		public Matrix4x4 stereoViewMatrix
		{
			get
			{
				return this.m_StereoViewMatrix;
			}
			set
			{
				this.m_StereoViewMatrix = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x000634A0 File Offset: 0x000616A0
		// (set) Token: 0x060032EF RID: 13039 RVA: 0x000634B8 File Offset: 0x000616B8
		public Matrix4x4 stereoProjectionMatrix
		{
			get
			{
				return this.m_StereoProjectionMatrix;
			}
			set
			{
				this.m_StereoProjectionMatrix = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x000634C4 File Offset: 0x000616C4
		// (set) Token: 0x060032F1 RID: 13041 RVA: 0x000634DC File Offset: 0x000616DC
		public float stereoSeparationDistance
		{
			get
			{
				return this.m_StereoSeparationDistance;
			}
			set
			{
				this.m_StereoSeparationDistance = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000634E8 File Offset: 0x000616E8
		// (set) Token: 0x060032F3 RID: 13043 RVA: 0x00063500 File Offset: 0x00061700
		public float accurateOcclusionThreshold
		{
			get
			{
				return this.m_AccurateOcclusionThreshold;
			}
			set
			{
				this.m_AccurateOcclusionThreshold = Mathf.Max(-1f, value);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x00063514 File Offset: 0x00061714
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x0006352C File Offset: 0x0006172C
		public int maximumPortalCullingJobs
		{
			get
			{
				return this.m_MaximumPortalCullingJobs;
			}
			set
			{
				bool flag = value < 1 || value > 16;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be in range {2} to {3}", new object[] { "maximumPortalCullingJobs", this.maximumPortalCullingJobs, 1, 16 }));
				}
				this.m_MaximumPortalCullingJobs = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x00063590 File Offset: 0x00061790
		public static int cullingJobsLowerLimit
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x000635A4 File Offset: 0x000617A4
		public static int cullingJobsUpperLimit
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000635B8 File Offset: 0x000617B8
		public unsafe float GetLayerCullingDistance(int layerIndex)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				return ptr2[layerIndex];
			}
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x00063618 File Offset: 0x00061818
		public unsafe void SetLayerCullingDistance(int layerIndex, float distance)
		{
			bool flag = layerIndex < 0 || layerIndex >= 32;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "layerIndex", layerIndex, 32));
			}
			fixed (float* ptr = &this.m_LayerFarCullDistances.FixedElementField)
			{
				float* ptr2 = ptr;
				ptr2[layerIndex] = distance;
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x00063678 File Offset: 0x00061878
		public unsafe Plane GetCullingPlane(int index)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				return ptr3[index];
			}
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000636EC File Offset: 0x000618EC
		public unsafe void SetCullingPlane(int index, Plane plane)
		{
			bool flag = index < 0 || index >= this.cullingPlaneCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} was {1}, but must be at least 0 and less than {2}", "index", index, this.cullingPlaneCount));
			}
			fixed (byte* ptr = &this.m_CullingPlanes.FixedElementField)
			{
				byte* ptr2 = ptr;
				Plane* ptr3 = (Plane*)ptr2;
				ptr3[index] = plane;
			}
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x00063760 File Offset: 0x00061960
		public bool Equals(ScriptableCullingParameters other)
		{
			for (int i = 0; i < 32; i++)
			{
				bool flag = !this.GetLayerCullingDistance(i).Equals(other.GetLayerCullingDistance(i));
				if (flag)
				{
					return false;
				}
			}
			for (int j = 0; j < this.cullingPlaneCount; j++)
			{
				bool flag2 = !this.GetCullingPlane(j).Equals(other.GetCullingPlane(j));
				if (flag2)
				{
					return false;
				}
			}
			return this.m_LODParameters.Equals(other.m_LODParameters) && this.m_CullingPlaneCount == other.m_CullingPlaneCount && this.m_CullingMask == other.m_CullingMask && this.m_SceneMask == other.m_SceneMask && this.m_ViewID == other.m_ViewID && this.m_LayerCull == other.m_LayerCull && this.m_CullingMatrix.Equals(other.m_CullingMatrix) && this.m_Origin.Equals(other.m_Origin) && this.m_ShadowDistance.Equals(other.m_ShadowDistance) && this.m_ShadowNearPlaneOffset.Equals(other.m_ShadowNearPlaneOffset) && this.m_CullingOptions == other.m_CullingOptions && this.m_ReflectionProbeSortingCriteria == other.m_ReflectionProbeSortingCriteria && this.m_CameraProperties.Equals(other.m_CameraProperties) && this.m_AccurateOcclusionThreshold.Equals(other.m_AccurateOcclusionThreshold) && this.m_StereoViewMatrix.Equals(other.m_StereoViewMatrix) && this.m_StereoProjectionMatrix.Equals(other.m_StereoProjectionMatrix) && this.m_StereoSeparationDistance.Equals(other.m_StereoSeparationDistance) && this.m_maximumVisibleLights == other.m_maximumVisibleLights && this.m_ConservativeEnclosingSphere == other.m_ConservativeEnclosingSphere && this.m_NumIterationsEnclosingSphere == other.m_NumIterationsEnclosingSphere;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x00063970 File Offset: 0x00061B70
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ScriptableCullingParameters && this.Equals((ScriptableCullingParameters)obj);
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000639A8 File Offset: 0x00061BA8
		public override int GetHashCode()
		{
			int num = this.m_LODParameters.GetHashCode();
			num = (num * 397) ^ this.m_CullingPlaneCount;
			num = (num * 397) ^ (int)this.m_CullingMask;
			num = (num * 397) ^ this.m_SceneMask.GetHashCode();
			num = (num * 397) ^ this.m_ViewID.GetHashCode();
			num = (num * 397) ^ this.m_LayerCull;
			num = (num * 397) ^ this.m_CullingMatrix.GetHashCode();
			num = (num * 397) ^ this.m_Origin.GetHashCode();
			num = (num * 397) ^ this.m_ShadowDistance.GetHashCode();
			num = (num * 397) ^ this.m_ShadowNearPlaneOffset.GetHashCode();
			num = (num * 397) ^ (int)this.m_CullingOptions;
			num = (num * 397) ^ (int)this.m_ReflectionProbeSortingCriteria;
			num = (num * 397) ^ this.m_CameraProperties.GetHashCode();
			num = (num * 397) ^ this.m_AccurateOcclusionThreshold.GetHashCode();
			num = (num * 397) ^ this.m_MaximumPortalCullingJobs.GetHashCode();
			num = (num * 397) ^ this.m_StereoViewMatrix.GetHashCode();
			num = (num * 397) ^ this.m_StereoProjectionMatrix.GetHashCode();
			num = (num * 397) ^ this.m_StereoSeparationDistance.GetHashCode();
			num = (num * 397) ^ this.m_maximumVisibleLights;
			num = (num * 397) ^ this.m_ConservativeEnclosingSphere.GetHashCode();
			return (num * 397) ^ this.m_NumIterationsEnclosingSphere.GetHashCode();
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x00063B60 File Offset: 0x00061D60
		public static bool operator ==(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x00063B7C File Offset: 0x00061D7C
		public static bool operator !=(ScriptableCullingParameters left, ScriptableCullingParameters right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040010ED RID: 4333
		private LODParameters m_LODParameters;

		// Token: 0x040010EE RID: 4334
		private const int k_MaximumCullingPlaneCount = 10;

		// Token: 0x040010EF RID: 4335
		public static readonly int maximumCullingPlaneCount = 10;

		// Token: 0x040010F0 RID: 4336
		[FixedBuffer(typeof(byte), 160)]
		internal ScriptableCullingParameters.<m_CullingPlanes>e__FixedBuffer m_CullingPlanes;

		// Token: 0x040010F1 RID: 4337
		private int m_CullingPlaneCount;

		// Token: 0x040010F2 RID: 4338
		private uint m_CullingMask;

		// Token: 0x040010F3 RID: 4339
		private ulong m_SceneMask;

		// Token: 0x040010F4 RID: 4340
		private ulong m_ViewID;

		// Token: 0x040010F5 RID: 4341
		private const int k_LayerCount = 32;

		// Token: 0x040010F6 RID: 4342
		public static readonly int layerCount = 32;

		// Token: 0x040010F7 RID: 4343
		[FixedBuffer(typeof(float), 32)]
		internal ScriptableCullingParameters.<m_LayerFarCullDistances>e__FixedBuffer m_LayerFarCullDistances;

		// Token: 0x040010F8 RID: 4344
		private int m_LayerCull;

		// Token: 0x040010F9 RID: 4345
		private Matrix4x4 m_CullingMatrix;

		// Token: 0x040010FA RID: 4346
		private Vector3 m_Origin;

		// Token: 0x040010FB RID: 4347
		private float m_ShadowDistance;

		// Token: 0x040010FC RID: 4348
		private float m_ShadowNearPlaneOffset;

		// Token: 0x040010FD RID: 4349
		private CullingOptions m_CullingOptions;

		// Token: 0x040010FE RID: 4350
		private ReflectionProbeSortingCriteria m_ReflectionProbeSortingCriteria;

		// Token: 0x040010FF RID: 4351
		private CameraProperties m_CameraProperties;

		// Token: 0x04001100 RID: 4352
		private float m_AccurateOcclusionThreshold;

		// Token: 0x04001101 RID: 4353
		private int m_MaximumPortalCullingJobs;

		// Token: 0x04001102 RID: 4354
		private const int k_CullingJobCountLowerLimit = 1;

		// Token: 0x04001103 RID: 4355
		private const int k_CullingJobCountUpperLimit = 16;

		// Token: 0x04001104 RID: 4356
		private Matrix4x4 m_StereoViewMatrix;

		// Token: 0x04001105 RID: 4357
		private Matrix4x4 m_StereoProjectionMatrix;

		// Token: 0x04001106 RID: 4358
		private float m_StereoSeparationDistance;

		// Token: 0x04001107 RID: 4359
		private int m_maximumVisibleLights;

		// Token: 0x04001108 RID: 4360
		private bool m_ConservativeEnclosingSphere;

		// Token: 0x04001109 RID: 4361
		private int m_NumIterationsEnclosingSphere;

		// Token: 0x02000532 RID: 1330
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 160)]
		public struct <m_CullingPlanes>e__FixedBuffer
		{
			// Token: 0x0400110A RID: 4362
			public byte FixedElementField;
		}

		// Token: 0x02000533 RID: 1331
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 128)]
		public struct <m_LayerFarCullDistances>e__FixedBuffer
		{
			// Token: 0x0400110B RID: 4363
			public float FixedElementField;
		}
	}
}
