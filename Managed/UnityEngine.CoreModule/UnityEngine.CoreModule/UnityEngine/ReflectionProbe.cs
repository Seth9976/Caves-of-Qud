using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000130 RID: 304
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	public sealed class ReflectionProbe : Behaviour
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0000E710 File Offset: 0x0000C910
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0000E734 File Offset: 0x0000C934
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("type property has been deprecated. Starting with Unity 5.4, the only supported reflection probe type is Cube.", true)]
		[NativeName("ProbeType")]
		public ReflectionProbeType type
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_type_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_type_Injected(intPtr, value);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x0000E758 File Offset: 0x0000C958
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0000E780 File Offset: 0x0000C980
		[NativeName("BoxSize")]
		public Vector3 size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ReflectionProbe.get_size_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		[NativeName("BoxOffset")]
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				ReflectionProbe.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x0000E814 File Offset: 0x0000CA14
		[NativeName("Near")]
		public float nearClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_nearClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_nearClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0000E838 File Offset: 0x0000CA38
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0000E85C File Offset: 0x0000CA5C
		[NativeName("Far")]
		public float farClipPlane
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_farClipPlane_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_farClipPlane_Injected(intPtr, value);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0000E880 File Offset: 0x0000CA80
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		[NativeName("IntensityMultiplier")]
		public float intensity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_intensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_intensity_Injected(intPtr, value);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		[NativeName("GlobalAABB")]
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				ReflectionProbe.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0000E914 File Offset: 0x0000CB14
		[NativeName("HDR")]
		public bool hdr
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_hdr_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_hdr_Injected(intPtr, value);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0000E938 File Offset: 0x0000CB38
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0000E95C File Offset: 0x0000CB5C
		[NativeName("RenderDynamicObjects")]
		public bool renderDynamicObjects
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_renderDynamicObjects_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_renderDynamicObjects_Injected(intPtr, value);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0000E980 File Offset: 0x0000CB80
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
		public float shadowDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_shadowDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_shadowDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public int resolution
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_resolution_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_resolution_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0000EA10 File Offset: 0x0000CC10
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0000EA34 File Offset: 0x0000CC34
		public int cullingMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_cullingMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_cullingMask_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0000EA58 File Offset: 0x0000CC58
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		public ReflectionProbeClearFlags clearFlags
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_clearFlags_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_clearFlags_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0000EAA0 File Offset: 0x0000CCA0
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
		public Color backgroundColor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				ReflectionProbe.get_backgroundColor_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_backgroundColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public float blendDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_blendDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_blendDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0000EB34 File Offset: 0x0000CD34
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x0000EB58 File Offset: 0x0000CD58
		public bool boxProjection
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_boxProjection_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_boxProjection_Injected(intPtr, value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public ReflectionProbeMode mode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_mode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_mode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public int importance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_importance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_importance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000EC30 File Offset: 0x0000CE30
		public ReflectionProbeRefreshMode refreshMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_refreshMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_refreshMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000EC54 File Offset: 0x0000CE54
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public ReflectionProbeTimeSlicingMode timeSlicingMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ReflectionProbe.get_timeSlicingMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_timeSlicingMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		public Texture bakedTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(ReflectionProbe.get_bakedTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_bakedTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		// (set) Token: 0x06000899 RID: 2201 RVA: 0x0000ED14 File Offset: 0x0000CF14
		public Texture customBakedTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(ReflectionProbe.get_customBakedTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_customBakedTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		// (set) Token: 0x0600089B RID: 2203 RVA: 0x0000ED64 File Offset: 0x0000CF64
		public RenderTexture realtimeTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(ReflectionProbe.get_realtimeTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ReflectionProbe.set_realtimeTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RenderTexture>(value));
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		public Texture texture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture>(ReflectionProbe.get_texture_Injected(intPtr));
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public Vector4 textureHDRDecodeValues
		{
			[NativeName("CalculateHDRDecodeValues")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				ReflectionProbe.get_textureHDRDecodeValues_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public void Reset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReflectionProbe.Reset_Injected(intPtr);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0000EE00 File Offset: 0x0000D000
		public int RenderProbe()
		{
			return this.RenderProbe(null);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public int RenderProbe([DefaultValue("null")] RenderTexture targetTexture)
		{
			return this.ScheduleRender(this.timeSlicingMode, targetTexture);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000EE3C File Offset: 0x0000D03C
		public bool IsFinishedRendering(int renderId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ReflectionProbe.IsFinishedRendering_Injected(intPtr, renderId);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000EE60 File Offset: 0x0000D060
		private int ScheduleRender(ReflectionProbeTimeSlicingMode timeSlicingMode, RenderTexture targetTexture)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ReflectionProbe>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ReflectionProbe.ScheduleRender_Injected(intPtr, timeSlicingMode, Object.MarshalledUnityObject.Marshal<RenderTexture>(targetTexture));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000EE8C File Offset: 0x0000D08C
		[NativeHeader("Runtime/Camera/CubemapGPUUtility.h")]
		[FreeFunction("CubemapGPUBlend")]
		public static bool BlendCubemap(Texture src, Texture dst, float blend, RenderTexture target)
		{
			return ReflectionProbe.BlendCubemap_Injected(Object.MarshalledUnityObject.Marshal<Texture>(src), Object.MarshalledUnityObject.Marshal<Texture>(dst), blend, Object.MarshalledUnityObject.Marshal<RenderTexture>(target));
		}

		// Token: 0x060008A4 RID: 2212
		[StaticAccessor("GetReflectionProbes()")]
		[NativeMethod("UpdateSampleData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateCachedState();

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008A5 RID: 2213
		[StaticAccessor("GetReflectionProbes()")]
		public static extern int minBakedCubemapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060008A6 RID: 2214
		[StaticAccessor("GetReflectionProbes()")]
		public static extern int maxBakedCubemapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		[StaticAccessor("GetReflectionProbes()")]
		public static Vector4 defaultTextureHDRDecodeValues
		{
			get
			{
				Vector4 vector;
				ReflectionProbe.get_defaultTextureHDRDecodeValues_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0000EECC File Offset: 0x0000D0CC
		[StaticAccessor("GetReflectionProbes()")]
		public static Texture defaultTexture
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture>(ReflectionProbe.get_defaultTexture_Injected());
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060008A9 RID: 2217 RVA: 0x0000EEE4 File Offset: 0x0000D0E4
		// (remove) Token: 0x060008AA RID: 2218 RVA: 0x0000EF18 File Offset: 0x0000D118
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> reflectionProbeChanged;

		// Token: 0x060008AB RID: 2219 RVA: 0x0000EF4C File Offset: 0x0000D14C
		[RequiredByNativeCode]
		private static void CallReflectionProbeEvent(ReflectionProbe probe, ReflectionProbe.ReflectionProbeEvent probeEvent)
		{
			Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action = ReflectionProbe.reflectionProbeChanged;
			bool flag = action != null;
			if (flag)
			{
				action(probe, probeEvent);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060008AC RID: 2220 RVA: 0x0000EF74 File Offset: 0x0000D174
		// (remove) Token: 0x060008AD RID: 2221 RVA: 0x0000EFDC File Offset: 0x0000D1DC
		[Obsolete("ReflectionProbe.defaultReflectionSet has been deprecated. Use ReflectionProbe.defaultReflectionTexture. (UnityUpgradable) -> UnityEngine.ReflectionProbe.defaultReflectionTexture", false)]
		public static event Action<Cubemap> defaultReflectionSet
		{
			add
			{
				bool flag = ReflectionProbe.registeredDefaultReflectionTextureActions.Any<Action<Texture>>((Action<Texture> h) => h.Method == value.Method);
				if (!flag)
				{
					Action<Texture> action = delegate(Texture b)
					{
						Cubemap cubemap = b as Cubemap;
						bool flag2 = cubemap != null;
						if (flag2)
						{
							value(cubemap);
						}
					};
					ReflectionProbe.defaultReflectionTexture += action;
					ReflectionProbe.registeredDefaultReflectionSetActions[value.Method.GetHashCode()] = action;
				}
			}
			remove
			{
				Action<Texture> action;
				bool flag = ReflectionProbe.registeredDefaultReflectionSetActions.TryGetValue(value.Method.GetHashCode(), out action);
				if (flag)
				{
					ReflectionProbe.defaultReflectionTexture -= action;
					ReflectionProbe.registeredDefaultReflectionSetActions.Remove(value.Method.GetHashCode());
				}
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060008AE RID: 2222 RVA: 0x0000F024 File Offset: 0x0000D224
		// (remove) Token: 0x060008AF RID: 2223 RVA: 0x0000F08C File Offset: 0x0000D28C
		public static event Action<Texture> defaultReflectionTexture
		{
			add
			{
				bool flag = ReflectionProbe.registeredDefaultReflectionTextureActions.Any<Action<Texture>>((Action<Texture> h) => h.Method == value.Method) || ReflectionProbe.registeredDefaultReflectionSetActions.ContainsKey(value.Method.GetHashCode());
				if (!flag)
				{
					ReflectionProbe.registeredDefaultReflectionTextureActions.Add(value);
				}
			}
			remove
			{
				ReflectionProbe.registeredDefaultReflectionTextureActions.Remove(value);
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000F09C File Offset: 0x0000D29C
		[RequiredByNativeCode]
		private static void CallSetDefaultReflection(Texture defaultReflectionCubemap)
		{
			foreach (Action<Texture> action in ReflectionProbe.registeredDefaultReflectionTextureActions)
			{
				action(defaultReflectionCubemap);
			}
		}

		// Token: 0x060008B3 RID: 2227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeType get_type_Injected(IntPtr _unity_self);

		// Token: 0x060008B4 RID: 2228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_type_Injected(IntPtr _unity_self, ReflectionProbeType value);

		// Token: 0x060008B5 RID: 2229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060008B6 RID: 2230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060008B7 RID: 2231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060008B8 RID: 2232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060008B9 RID: 2233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_nearClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x060008BA RID: 2234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nearClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x060008BB RID: 2235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_farClipPlane_Injected(IntPtr _unity_self);

		// Token: 0x060008BC RID: 2236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_farClipPlane_Injected(IntPtr _unity_self, float value);

		// Token: 0x060008BD RID: 2237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_intensity_Injected(IntPtr _unity_self);

		// Token: 0x060008BE RID: 2238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_intensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x060008BF RID: 2239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060008C0 RID: 2240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hdr_Injected(IntPtr _unity_self);

		// Token: 0x060008C1 RID: 2241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_hdr_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060008C2 RID: 2242
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_renderDynamicObjects_Injected(IntPtr _unity_self);

		// Token: 0x060008C3 RID: 2243
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_renderDynamicObjects_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060008C4 RID: 2244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_shadowDistance_Injected(IntPtr _unity_self);

		// Token: 0x060008C5 RID: 2245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shadowDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060008C6 RID: 2246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_resolution_Injected(IntPtr _unity_self);

		// Token: 0x060008C7 RID: 2247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_resolution_Injected(IntPtr _unity_self, int value);

		// Token: 0x060008C8 RID: 2248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cullingMask_Injected(IntPtr _unity_self);

		// Token: 0x060008C9 RID: 2249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x060008CA RID: 2250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeClearFlags get_clearFlags_Injected(IntPtr _unity_self);

		// Token: 0x060008CB RID: 2251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clearFlags_Injected(IntPtr _unity_self, ReflectionProbeClearFlags value);

		// Token: 0x060008CC RID: 2252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_backgroundColor_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060008CD RID: 2253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_backgroundColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060008CE RID: 2254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_blendDistance_Injected(IntPtr _unity_self);

		// Token: 0x060008CF RID: 2255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_blendDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060008D0 RID: 2256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_boxProjection_Injected(IntPtr _unity_self);

		// Token: 0x060008D1 RID: 2257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_boxProjection_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060008D2 RID: 2258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeMode get_mode_Injected(IntPtr _unity_self);

		// Token: 0x060008D3 RID: 2259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_mode_Injected(IntPtr _unity_self, ReflectionProbeMode value);

		// Token: 0x060008D4 RID: 2260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_importance_Injected(IntPtr _unity_self);

		// Token: 0x060008D5 RID: 2261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_importance_Injected(IntPtr _unity_self, int value);

		// Token: 0x060008D6 RID: 2262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeRefreshMode get_refreshMode_Injected(IntPtr _unity_self);

		// Token: 0x060008D7 RID: 2263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_refreshMode_Injected(IntPtr _unity_self, ReflectionProbeRefreshMode value);

		// Token: 0x060008D8 RID: 2264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ReflectionProbeTimeSlicingMode get_timeSlicingMode_Injected(IntPtr _unity_self);

		// Token: 0x060008D9 RID: 2265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeSlicingMode_Injected(IntPtr _unity_self, ReflectionProbeTimeSlicingMode value);

		// Token: 0x060008DA RID: 2266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_bakedTexture_Injected(IntPtr _unity_self);

		// Token: 0x060008DB RID: 2267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bakedTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060008DC RID: 2268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_customBakedTexture_Injected(IntPtr _unity_self);

		// Token: 0x060008DD RID: 2269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_customBakedTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060008DE RID: 2270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_realtimeTexture_Injected(IntPtr _unity_self);

		// Token: 0x060008DF RID: 2271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_realtimeTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060008E0 RID: 2272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_texture_Injected(IntPtr _unity_self);

		// Token: 0x060008E1 RID: 2273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_textureHDRDecodeValues_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060008E2 RID: 2274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reset_Injected(IntPtr _unity_self);

		// Token: 0x060008E3 RID: 2275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsFinishedRendering_Injected(IntPtr _unity_self, int renderId);

		// Token: 0x060008E4 RID: 2276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ScheduleRender_Injected(IntPtr _unity_self, ReflectionProbeTimeSlicingMode timeSlicingMode, IntPtr targetTexture);

		// Token: 0x060008E5 RID: 2277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BlendCubemap_Injected(IntPtr src, IntPtr dst, float blend, IntPtr target);

		// Token: 0x060008E6 RID: 2278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultTextureHDRDecodeValues_Injected(out Vector4 ret);

		// Token: 0x060008E7 RID: 2279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_defaultTexture_Injected();

		// Token: 0x0400040C RID: 1036
		private static Dictionary<int, Action<Texture>> registeredDefaultReflectionSetActions = new Dictionary<int, Action<Texture>>();

		// Token: 0x0400040D RID: 1037
		private static List<Action<Texture>> registeredDefaultReflectionTextureActions = new List<Action<Texture>>();

		// Token: 0x02000131 RID: 305
		public enum ReflectionProbeEvent
		{
			// Token: 0x0400040F RID: 1039
			ReflectionProbeAdded,
			// Token: 0x04000410 RID: 1040
			ReflectionProbeRemoved
		}
	}
}
