using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Terrain/Public/TerrainLayerScriptingInterface.h")]
	[NativeHeader("TerrainScriptingClasses.h")]
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TerrainLayer : Object
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000634D File Offset: 0x0000454D
		public TerrainLayer()
		{
			TerrainLayer.Internal_Create(this);
		}

		// Token: 0x06000236 RID: 566
		[FreeFunction("TerrainLayerScriptingInterface::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] TerrainLayer layer);

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00006360 File Offset: 0x00004560
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00006388 File Offset: 0x00004588
		public Texture2D diffuseTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(TerrainLayer.get_diffuseTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_diffuseTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture2D>(value));
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000063B0 File Offset: 0x000045B0
		// (set) Token: 0x0600023A RID: 570 RVA: 0x000063D8 File Offset: 0x000045D8
		public Texture2D normalMapTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(TerrainLayer.get_normalMapTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_normalMapTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture2D>(value));
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00006400 File Offset: 0x00004600
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00006428 File Offset: 0x00004628
		public Texture2D maskMapTexture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(TerrainLayer.get_maskMapTexture_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_maskMapTexture_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Texture2D>(value));
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00006450 File Offset: 0x00004650
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00006478 File Offset: 0x00004678
		public Vector2 tileSize
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				TerrainLayer.get_tileSize_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_tileSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000649C File Offset: 0x0000469C
		// (set) Token: 0x06000240 RID: 576 RVA: 0x000064C4 File Offset: 0x000046C4
		public Vector2 tileOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				TerrainLayer.get_tileOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_tileOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000064E8 File Offset: 0x000046E8
		// (set) Token: 0x06000242 RID: 578 RVA: 0x00006510 File Offset: 0x00004710
		[NativeProperty("SpecularColor")]
		public Color specular
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				TerrainLayer.get_specular_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_specular_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00006534 File Offset: 0x00004734
		// (set) Token: 0x06000244 RID: 580 RVA: 0x00006558 File Offset: 0x00004758
		public float metallic
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainLayer.get_metallic_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_metallic_Injected(intPtr, value);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000657C File Offset: 0x0000477C
		// (set) Token: 0x06000246 RID: 582 RVA: 0x000065A0 File Offset: 0x000047A0
		public float smoothness
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainLayer.get_smoothness_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_smoothness_Injected(intPtr, value);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000065C4 File Offset: 0x000047C4
		// (set) Token: 0x06000248 RID: 584 RVA: 0x000065E8 File Offset: 0x000047E8
		public float normalScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainLayer.get_normalScale_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_normalScale_Injected(intPtr, value);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000660C File Offset: 0x0000480C
		// (set) Token: 0x0600024A RID: 586 RVA: 0x00006634 File Offset: 0x00004834
		public Vector4 diffuseRemapMin
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				TerrainLayer.get_diffuseRemapMin_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_diffuseRemapMin_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00006658 File Offset: 0x00004858
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00006680 File Offset: 0x00004880
		public Vector4 diffuseRemapMax
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				TerrainLayer.get_diffuseRemapMax_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_diffuseRemapMax_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000066A4 File Offset: 0x000048A4
		// (set) Token: 0x0600024E RID: 590 RVA: 0x000066CC File Offset: 0x000048CC
		public Vector4 maskMapRemapMin
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				TerrainLayer.get_maskMapRemapMin_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_maskMapRemapMin_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024F RID: 591 RVA: 0x000066F0 File Offset: 0x000048F0
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00006718 File Offset: 0x00004918
		public Vector4 maskMapRemapMax
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				TerrainLayer.get_maskMapRemapMax_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_maskMapRemapMax_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000673C File Offset: 0x0000493C
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00006760 File Offset: 0x00004960
		public TerrainLayerSmoothnessSource smoothnessSource
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainLayer.get_smoothnessSource_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainLayer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainLayer.set_smoothnessSource_Injected(intPtr, value);
			}
		}

		// Token: 0x06000253 RID: 595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_diffuseTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000254 RID: 596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_diffuseTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000255 RID: 597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_normalMapTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000256 RID: 598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_normalMapTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000257 RID: 599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_maskMapTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000258 RID: 600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskMapTexture_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000259 RID: 601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tileSize_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600025A RID: 602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tileSize_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x0600025B RID: 603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tileOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600025C RID: 604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tileOffset_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x0600025D RID: 605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_specular_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x0600025E RID: 606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_specular_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x0600025F RID: 607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_metallic_Injected(IntPtr _unity_self);

		// Token: 0x06000260 RID: 608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_metallic_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000261 RID: 609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_smoothness_Injected(IntPtr _unity_self);

		// Token: 0x06000262 RID: 610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_smoothness_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000263 RID: 611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_normalScale_Injected(IntPtr _unity_self);

		// Token: 0x06000264 RID: 612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_normalScale_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000265 RID: 613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_diffuseRemapMin_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x06000266 RID: 614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_diffuseRemapMin_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x06000267 RID: 615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_diffuseRemapMax_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x06000268 RID: 616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_diffuseRemapMax_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x06000269 RID: 617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_maskMapRemapMin_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x0600026A RID: 618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskMapRemapMin_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x0600026B RID: 619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_maskMapRemapMax_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x0600026C RID: 620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maskMapRemapMax_Injected(IntPtr _unity_self, [In] ref Vector4 value);

		// Token: 0x0600026D RID: 621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TerrainLayerSmoothnessSource get_smoothnessSource_Injected(IntPtr _unity_self);

		// Token: 0x0600026E RID: 622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_smoothnessSource_Injected(IntPtr _unity_self, TerrainLayerSmoothnessSource value);
	}
}
