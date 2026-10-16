using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[UsedByNativeCode]
	[NativeHeader("TerrainScriptingClasses.h")]
	public sealed class TerrainData : Object
	{
		// Token: 0x06000148 RID: 328
		[ThreadSafe]
		[StaticAccessor("TerrainDataScriptingInterface", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBoundaryValue(TerrainData.BoundaryValueType type);

		// Token: 0x06000149 RID: 329 RVA: 0x00003FEE File Offset: 0x000021EE
		public TerrainData()
		{
			TerrainData.Internal_Create(this);
		}

		// Token: 0x0600014A RID: 330
		[FreeFunction("TerrainDataScriptingInterface::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] TerrainData terrainData);

		// Token: 0x0600014B RID: 331 RVA: 0x00003FFF File Offset: 0x000021FF
		[Obsolete("Please use DirtyHeightmapRegion instead.", false)]
		public void UpdateDirtyRegion(int x, int y, int width, int height, bool syncHeightmapTextureImmediately)
		{
			this.DirtyHeightmapRegion(new RectInt(x, y, width, height), syncHeightmapTextureImmediately ? TerrainHeightmapSyncControl.HeightOnly : TerrainHeightmapSyncControl.None);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000401B File Offset: 0x0000221B
		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapWidth
		{
			get
			{
				return this.heightmapResolution;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000401B File Offset: 0x0000221B
		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapHeight
		{
			get
			{
				return this.heightmapResolution;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004024 File Offset: 0x00002224
		public RenderTexture heightmapTexture
		{
			[NativeName("GetHeightmap().GetHeightmapTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RenderTexture>(TerrainData.get_heightmapTexture_Injected(intPtr));
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000404C File Offset: 0x0000224C
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004064 File Offset: 0x00002264
		public int heightmapResolution
		{
			get
			{
				return this.internalHeightmapResolution;
			}
			set
			{
				int num = value;
				bool flag = value < 0 || value > TerrainData.k_MaximumResolution;
				if (flag)
				{
					Debug.LogWarning("heightmapResolution is clamped to the range of [0, " + TerrainData.k_MaximumResolution.ToString() + "].");
					num = Math.Min(TerrainData.k_MaximumResolution, Math.Max(value, 0));
				}
				this.internalHeightmapResolution = num;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000040C4 File Offset: 0x000022C4
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000040E8 File Offset: 0x000022E8
		private int internalHeightmapResolution
		{
			[NativeName("GetHeightmap().GetResolution")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_internalHeightmapResolution_Injected(intPtr);
			}
			[NativeName("GetHeightmap().SetResolution")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_internalHeightmapResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000410C File Offset: 0x0000230C
		public Vector3 heightmapScale
		{
			[NativeName("GetHeightmap().GetScale")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				TerrainData.get_heightmapScale_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004134 File Offset: 0x00002334
		public Texture holesTexture
		{
			get
			{
				bool flag = this.IsHolesTextureCompressed();
				Texture texture;
				if (flag)
				{
					texture = this.GetCompressedHolesTexture();
				}
				else
				{
					texture = this.GetHolesTexture();
				}
				return texture;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004164 File Offset: 0x00002364
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00004188 File Offset: 0x00002388
		public bool enableHolesTextureCompression
		{
			[NativeName("GetHeightmap().GetEnableHolesTextureCompression")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_enableHolesTextureCompression_Injected(intPtr);
			}
			[NativeName("GetHeightmap().SetEnableHolesTextureCompression")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_enableHolesTextureCompression_Injected(intPtr, value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000041AC File Offset: 0x000023AC
		internal RenderTexture holesRenderTexture
		{
			get
			{
				return this.GetHolesTexture();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000041C4 File Offset: 0x000023C4
		[NativeName("GetHeightmap().IsHolesTextureCompressed")]
		internal bool IsHolesTextureCompressed()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.IsHolesTextureCompressed_Injected(intPtr);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000041E8 File Offset: 0x000023E8
		[NativeName("GetHeightmap().GetHolesTexture")]
		internal RenderTexture GetHolesTexture()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(TerrainData.GetHolesTexture_Injected(intPtr));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00004210 File Offset: 0x00002410
		[NativeName("GetHeightmap().GetCompressedHolesTexture")]
		internal Texture2D GetCompressedHolesTexture()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(TerrainData.GetCompressedHolesTexture_Injected(intPtr));
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004237 File Offset: 0x00002437
		public int holesResolution
		{
			get
			{
				return this.heightmapResolution - 1;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004244 File Offset: 0x00002444
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000426C File Offset: 0x0000246C
		public Vector3 size
		{
			[NativeName("GetHeightmap().GetSize")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				TerrainData.get_size_Injected(intPtr, out vector);
				return vector;
			}
			[NativeName("GetHeightmap().SetSize")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004290 File Offset: 0x00002490
		public Bounds bounds
		{
			[NativeName("GetHeightmap().CalculateBounds")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				TerrainData.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000042B8 File Offset: 0x000024B8
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002E77 File Offset: 0x00001077
		[Obsolete("Terrain thickness is no longer required by the physics engine. Set appropriate continuous collision detection modes to fast moving bodies.")]
		public float thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000042D0 File Offset: 0x000024D0
		[NativeName("GetHeightmap().GetHeight")]
		public float GetHeight(int x, int y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetHeight_Injected(intPtr, x, y);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000042F4 File Offset: 0x000024F4
		[NativeName("GetHeightmap().GetInterpolatedHeight")]
		public float GetInterpolatedHeight(float x, float y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetInterpolatedHeight_Injected(intPtr, x, y);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004318 File Offset: 0x00002518
		public float[,] GetInterpolatedHeights(float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			bool flag = xCount <= 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			bool flag2 = yCount <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			float[,] array = new float[yCount, xCount];
			this.Internal_GetInterpolatedHeights(array, xCount, 0, 0, xBase, yBase, xCount, yCount, xInterval, yInterval);
			return array;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004378 File Offset: 0x00002578
		public void GetInterpolatedHeights(float[,] results, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			bool flag = results == null;
			if (flag)
			{
				throw new ArgumentNullException("results");
			}
			bool flag2 = xCount <= 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			bool flag3 = yCount <= 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			bool flag4 = resultXOffset < 0 || resultXOffset + xCount > results.GetLength(1);
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("resultXOffset");
			}
			bool flag5 = resultYOffset < 0 || resultYOffset + yCount > results.GetLength(0);
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("resultYOffset");
			}
			this.Internal_GetInterpolatedHeights(results, results.GetLength(1), resultXOffset, resultYOffset, xBase, yBase, xCount, yCount, xInterval, yInterval);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000442C File Offset: 0x0000262C
		[FreeFunction("TerrainDataScriptingInterface::GetInterpolatedHeights", HasExplicitThis = true)]
		private unsafe void Internal_GetInterpolatedHeights(float[,] results, int resultXDimension, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (results == null || (num = results.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&results[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_GetInterpolatedHeights_Injected(intPtr, ref managedSpanWrapper, resultXDimension, resultXOffset, resultYOffset, xBase, yBase, xCount, yCount, xInterval, yInterval);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000448C File Offset: 0x0000268C
		public float[,] GetHeights(int xBase, int yBase, int width, int height)
		{
			bool flag = xBase < 0 || yBase < 0 || xBase + width < 0 || yBase + height < 0 || xBase + width > this.heightmapResolution || yBase + height > this.heightmapResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain height information.");
			}
			return this.Internal_GetHeights(xBase, yBase, width, height);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000044E8 File Offset: 0x000026E8
		[FreeFunction("TerrainDataScriptingInterface::GetHeights", HasExplicitThis = true)]
		[return: Unmarshalled]
		private float[,] Internal_GetHeights(int xBase, int yBase, int width, int height)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.Internal_GetHeights_Injected(intPtr, xBase, yBase, width, height);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004510 File Offset: 0x00002710
		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			bool flag = heights == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = xBase + heights.GetLength(1) > this.heightmapResolution || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > this.heightmapResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{2}", new object[]
				{
					xBase + heights.GetLength(1),
					yBase + heights.GetLength(0),
					this.heightmapResolution
				}));
			}
			this.Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000045D8 File Offset: 0x000027D8
		[FreeFunction("TerrainDataScriptingInterface::SetHeights", HasExplicitThis = true)]
		private unsafe void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (heights == null || (num = heights.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&heights[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_SetHeights_Injected(intPtr, xBase, yBase, width, height, ref managedSpanWrapper);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000462C File Offset: 0x0000282C
		[FreeFunction("TerrainDataScriptingInterface::GetPatchMinMaxHeights", HasExplicitThis = true)]
		public PatchExtents[] GetPatchMinMaxHeights()
		{
			PatchExtents[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				TerrainData.GetPatchMinMaxHeights_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				PatchExtents[] array;
				blittableArrayWrapper.Unmarshal<PatchExtents>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004670 File Offset: 0x00002870
		[FreeFunction("TerrainDataScriptingInterface::OverrideMinMaxPatchHeights", HasExplicitThis = true)]
		public unsafe void OverrideMinMaxPatchHeights(PatchExtents[] minMaxHeights)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<PatchExtents> span = new Span<PatchExtents>(minMaxHeights);
			fixed (PatchExtents* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				TerrainData.OverrideMinMaxPatchHeights_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000046B8 File Offset: 0x000028B8
		[FreeFunction("TerrainDataScriptingInterface::GetMaximumHeightError", HasExplicitThis = true)]
		public float[] GetMaximumHeightError()
		{
			float[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				TerrainData.GetMaximumHeightError_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000046FC File Offset: 0x000028FC
		[FreeFunction("TerrainDataScriptingInterface::OverrideMaximumHeightError", HasExplicitThis = true)]
		public unsafe void OverrideMaximumHeightError(float[] maxError)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(maxError);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				TerrainData.OverrideMaximumHeightError_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004744 File Offset: 0x00002944
		public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			bool flag = heights == null;
			if (flag)
			{
				throw new ArgumentNullException("heights");
			}
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 < 0 || xBase + length2 > this.heightmapResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.heightmapResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length < 0 || yBase + length > this.heightmapResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.heightmapResolution
				}));
			}
			this.Internal_SetHeightsDelayLOD(xBase, yBase, length2, length, heights);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000482C File Offset: 0x00002A2C
		[FreeFunction("TerrainDataScriptingInterface::SetHeightsDelayLOD", HasExplicitThis = true)]
		private unsafe void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (heights == null || (num = heights.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&heights[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_SetHeightsDelayLOD_Injected(intPtr, xBase, yBase, width, height, ref managedSpanWrapper);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00004880 File Offset: 0x00002A80
		public bool IsHole(int x, int y)
		{
			bool flag = x < 0 || x >= this.holesResolution || y < 0 || y >= this.holesResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return this.Internal_IsHole(x, y);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000048CC File Offset: 0x00002ACC
		public bool[,] GetHoles(int xBase, int yBase, int width, int height)
		{
			bool flag = xBase < 0 || yBase < 0 || width <= 0 || height <= 0 || xBase + width > this.holesResolution || yBase + height > this.holesResolution;
			if (flag)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return this.Internal_GetHoles(xBase, yBase, width, height);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004924 File Offset: 0x00002B24
		public void SetHoles(int xBase, int yBase, bool[,] holes)
		{
			bool flag = holes == null;
			if (flag)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 > this.holesResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.holesResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length > this.holesResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.holesResolution
				}));
			}
			this.Internal_SetHoles(xBase, yBase, holes.GetLength(1), holes.GetLength(0), holes);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004A0C File Offset: 0x00002C0C
		public void SetHolesDelayLOD(int xBase, int yBase, bool[,] holes)
		{
			bool flag = holes == null;
			if (flag)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			bool flag2 = xBase < 0 || xBase + length2 > this.holesResolution;
			if (flag2)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					xBase,
					xBase + length2,
					this.holesResolution
				}));
			}
			bool flag3 = yBase < 0 || yBase + length > this.holesResolution;
			if (flag3)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", new object[]
				{
					yBase,
					yBase + length,
					this.holesResolution
				}));
			}
			this.Internal_SetHolesDelayLOD(xBase, yBase, length2, length, holes);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00004AE8 File Offset: 0x00002CE8
		[FreeFunction("TerrainDataScriptingInterface::SetHoles", HasExplicitThis = true)]
		private unsafe void Internal_SetHoles(int xBase, int yBase, int width, int height, bool[,] holes)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (holes == null || (num = holes.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&holes[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_SetHoles_Injected(intPtr, xBase, yBase, width, height, ref managedSpanWrapper);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004B3C File Offset: 0x00002D3C
		[FreeFunction("TerrainDataScriptingInterface::GetHoles", HasExplicitThis = true)]
		[return: Unmarshalled]
		private bool[,] Internal_GetHoles(int xBase, int yBase, int width, int height)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.Internal_GetHoles_Injected(intPtr, xBase, yBase, width, height);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004B64 File Offset: 0x00002D64
		[FreeFunction("TerrainDataScriptingInterface::IsHole", HasExplicitThis = true)]
		private bool Internal_IsHole(int x, int y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.Internal_IsHole_Injected(intPtr, x, y);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004B88 File Offset: 0x00002D88
		[FreeFunction("TerrainDataScriptingInterface::SetHolesDelayLOD", HasExplicitThis = true)]
		private unsafe void Internal_SetHolesDelayLOD(int xBase, int yBase, int width, int height, bool[,] holes)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (holes == null || (num = holes.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&holes[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_SetHolesDelayLOD_Injected(intPtr, xBase, yBase, width, height, ref managedSpanWrapper);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004BDC File Offset: 0x00002DDC
		[NativeName("GetHeightmap().GetSteepness")]
		public float GetSteepness(float x, float y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetSteepness_Injected(intPtr, x, y);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004C00 File Offset: 0x00002E00
		[NativeName("GetHeightmap().GetInterpolatedNormal")]
		public Vector3 GetInterpolatedNormal(float x, float y)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			TerrainData.GetInterpolatedNormal_Injected(intPtr, x, y, out vector);
			return vector;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004C28 File Offset: 0x00002E28
		[NativeName("GetHeightmap().GetAdjustedSize")]
		internal int GetAdjustedSize(int size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetAdjustedSize_Injected(intPtr, size);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00004C4C File Offset: 0x00002E4C
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00004C70 File Offset: 0x00002E70
		public float wavingGrassStrength
		{
			[NativeName("GetDetailDatabase().GetWavingGrassStrength")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_wavingGrassStrength_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassStrength", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_wavingGrassStrength_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00004C94 File Offset: 0x00002E94
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00004CB8 File Offset: 0x00002EB8
		public float wavingGrassAmount
		{
			[NativeName("GetDetailDatabase().GetWavingGrassAmount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_wavingGrassAmount_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassAmount", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_wavingGrassAmount_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00004CDC File Offset: 0x00002EDC
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00004D00 File Offset: 0x00002F00
		public float wavingGrassSpeed
		{
			[NativeName("GetDetailDatabase().GetWavingGrassSpeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_wavingGrassSpeed_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassSpeed", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_wavingGrassSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00004D24 File Offset: 0x00002F24
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00004D4C File Offset: 0x00002F4C
		public Color wavingGrassTint
		{
			[NativeName("GetDetailDatabase().GetWavingGrassTint")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				TerrainData.get_wavingGrassTint_Injected(intPtr, out color);
				return color;
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassTint", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_wavingGrassTint_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00004D70 File Offset: 0x00002F70
		public int detailWidth
		{
			[NativeName("GetDetailDatabase().GetWidth")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailWidth_Injected(intPtr);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00004D94 File Offset: 0x00002F94
		public int detailHeight
		{
			[NativeName("GetDetailDatabase().GetHeight")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00004DB8 File Offset: 0x00002FB8
		public int maxDetailScatterPerRes
		{
			[NativeName("GetDetailDatabase().GetMaximumScatterPerRes")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_maxDetailScatterPerRes_Injected(intPtr);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004DDC File Offset: 0x00002FDC
		public void SetDetailResolution(int detailResolution, int resolutionPerPatch)
		{
			bool flag = detailResolution < 0;
			if (flag)
			{
				Debug.LogWarning("detailResolution must not be negative.");
				detailResolution = 0;
			}
			bool flag2 = resolutionPerPatch < TerrainData.k_MinimumDetailResolutionPerPatch || resolutionPerPatch > TerrainData.k_MaximumDetailResolutionPerPatch;
			if (flag2)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"resolutionPerPatch is clamped to the range of [",
					TerrainData.k_MinimumDetailResolutionPerPatch.ToString(),
					", ",
					TerrainData.k_MaximumDetailResolutionPerPatch.ToString(),
					"]."
				}));
				resolutionPerPatch = Math.Min(TerrainData.k_MaximumDetailResolutionPerPatch, Math.Max(resolutionPerPatch, TerrainData.k_MinimumDetailResolutionPerPatch));
			}
			int num = detailResolution / resolutionPerPatch;
			bool flag3 = num > TerrainData.k_MaximumDetailPatchCount;
			if (flag3)
			{
				Debug.LogWarning("Patch count (detailResolution / resolutionPerPatch) is clamped to the range of [0, " + TerrainData.k_MaximumDetailPatchCount.ToString() + "].");
				num = Math.Min(TerrainData.k_MaximumDetailPatchCount, Math.Max(num, 0));
			}
			this.Internal_SetDetailResolution(num, resolutionPerPatch);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004EC4 File Offset: 0x000030C4
		[NativeName("GetDetailDatabase().SetDetailResolution")]
		private void Internal_SetDetailResolution(int patchCount, int resolutionPerPatch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_SetDetailResolution_Injected(intPtr, patchCount, resolutionPerPatch);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004EE8 File Offset: 0x000030E8
		public void SetDetailScatterMode(DetailScatterMode scatterMode)
		{
			this.Internal_SetDetailScatterMode(scatterMode);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004EF4 File Offset: 0x000030F4
		[NativeName("GetDetailDatabase().SetDetailScatterMode")]
		private void Internal_SetDetailScatterMode(DetailScatterMode scatterMode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_SetDetailScatterMode_Injected(intPtr, scatterMode);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00004F18 File Offset: 0x00003118
		public int detailPatchCount
		{
			[NativeName("GetDetailDatabase().GetPatchCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailPatchCount_Injected(intPtr);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00004F3C File Offset: 0x0000313C
		public int detailResolution
		{
			[NativeName("GetDetailDatabase().GetResolution")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailResolution_Injected(intPtr);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00004F60 File Offset: 0x00003160
		public int detailResolutionPerPatch
		{
			[NativeName("GetDetailDatabase().GetResolutionPerPatch")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailResolutionPerPatch_Injected(intPtr);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004F84 File Offset: 0x00003184
		public DetailScatterMode detailScatterMode
		{
			[NativeName("GetDetailDatabase().GetDetailScatterMode")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailScatterMode_Injected(intPtr);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004FA8 File Offset: 0x000031A8
		[NativeName("GetDetailDatabase().ResetDirtyDetails")]
		internal void ResetDirtyDetails()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.ResetDirtyDetails_Injected(intPtr);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004FCC File Offset: 0x000031CC
		[FreeFunction("TerrainDataScriptingInterface::RefreshPrototypes", HasExplicitThis = true)]
		public void RefreshPrototypes()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.RefreshPrototypes_Injected(intPtr);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00004FF0 File Offset: 0x000031F0
		// (set) Token: 0x06000191 RID: 401 RVA: 0x00005014 File Offset: 0x00003214
		public DetailPrototype[] detailPrototypes
		{
			[FreeFunction("TerrainDataScriptingInterface::GetDetailPrototypes", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_detailPrototypes_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetDetailPrototypes", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_detailPrototypes_Injected(intPtr, value);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005038 File Offset: 0x00003238
		[FreeFunction("TerrainDataScriptingInterface::GetSupportedLayers", HasExplicitThis = true)]
		public int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight)
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				TerrainData.GetSupportedLayers_Injected(intPtr, xBase, yBase, totalWidth, totalHeight, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005080 File Offset: 0x00003280
		public int[] GetSupportedLayers(Vector2Int positionBase, Vector2Int size)
		{
			return this.GetSupportedLayers(positionBase.x, positionBase.y, size.x, size.y);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000050B4 File Offset: 0x000032B4
		[FreeFunction("TerrainDataScriptingInterface::GetDetailLayer", HasExplicitThis = true)]
		[return: Unmarshalled]
		public int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetDetailLayer_Injected(intPtr, xBase, yBase, width, height, layer);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000050E0 File Offset: 0x000032E0
		public int[,] GetDetailLayer(Vector2Int positionBase, Vector2Int size, int layer)
		{
			return this.GetDetailLayer(positionBase.x, positionBase.y, size.x, size.y, layer);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00005118 File Offset: 0x00003318
		[FreeFunction("TerrainDataScriptingInterface::ComputeDetailInstanceTransforms", HasExplicitThis = true)]
		public DetailInstanceTransform[] ComputeDetailInstanceTransforms(int patchX, int patchY, int layer, float density, out Bounds bounds)
		{
			DetailInstanceTransform[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				TerrainData.ComputeDetailInstanceTransforms_Injected(intPtr, patchX, patchY, layer, density, out bounds, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				DetailInstanceTransform[] array;
				blittableArrayWrapper.Unmarshal<DetailInstanceTransform>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00005164 File Offset: 0x00003364
		[FreeFunction("TerrainDataScriptingInterface::ComputeDetailCoverage", HasExplicitThis = true)]
		public float ComputeDetailCoverage(int detailPrototypeIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.ComputeDetailCoverage_Injected(intPtr, detailPrototypeIndex);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005187 File Offset: 0x00003387
		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			this.Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000051A6 File Offset: 0x000033A6
		public void SetDetailLayer(Vector2Int basePosition, int layer, int[,] details)
		{
			this.SetDetailLayer(basePosition.x, basePosition.y, layer, details);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000051C0 File Offset: 0x000033C0
		[FreeFunction("TerrainDataScriptingInterface::SetDetailLayer", HasExplicitThis = true)]
		private unsafe void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (data == null || (num = data.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&data[0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_SetDetailLayer_Injected(intPtr, xBase, yBase, totalWidth, totalHeight, detailIndex, ref managedSpanWrapper);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005218 File Offset: 0x00003418
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00005230 File Offset: 0x00003430
		public TreeInstance[] treeInstances
		{
			get
			{
				return this.Internal_GetTreeInstances();
			}
			set
			{
				this.SetTreeInstances(value, false);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000523C File Offset: 0x0000343C
		[NativeName("GetTreeDatabase().GetInstances")]
		private TreeInstance[] Internal_GetTreeInstances()
		{
			TreeInstance[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				TerrainData.Internal_GetTreeInstances_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				TreeInstance[] array;
				blittableArrayWrapper.Unmarshal<TreeInstance>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005280 File Offset: 0x00003480
		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstances", HasExplicitThis = true)]
		public unsafe void SetTreeInstances([NotNull] TreeInstance[] instances, bool snapToHeightmap)
		{
			if (instances == null)
			{
				ThrowHelper.ThrowArgumentNullException(instances, "instances");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<TreeInstance> span = new Span<TreeInstance>(instances);
			fixed (TreeInstance* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				TerrainData.SetTreeInstances_Injected(intPtr, ref managedSpanWrapper, snapToHeightmap);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000052D8 File Offset: 0x000034D8
		public TreeInstance GetTreeInstance(int index)
		{
			bool flag = index < 0 || index >= this.treeInstanceCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return this.Internal_GetTreeInstance(index);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005314 File Offset: 0x00003514
		[FreeFunction("TerrainDataScriptingInterface::GetTreeInstance", HasExplicitThis = true)]
		private TreeInstance Internal_GetTreeInstance(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TreeInstance treeInstance;
			TerrainData.Internal_GetTreeInstance_Injected(intPtr, index, out treeInstance);
			return treeInstance;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000533C File Offset: 0x0000353C
		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstance", HasExplicitThis = true)]
		[NativeThrows]
		public void SetTreeInstance(int index, TreeInstance instance)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.SetTreeInstance_Injected(intPtr, index, ref instance);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005364 File Offset: 0x00003564
		public int treeInstanceCount
		{
			[NativeName("GetTreeDatabase().GetInstances().size")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_treeInstanceCount_Injected(intPtr);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00005388 File Offset: 0x00003588
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x000053AC File Offset: 0x000035AC
		public TreePrototype[] treePrototypes
		{
			[FreeFunction("TerrainDataScriptingInterface::GetTreePrototypes", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_treePrototypes_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetTreePrototypes", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_treePrototypes_Injected(intPtr, value);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000053D0 File Offset: 0x000035D0
		[NativeName("GetTreeDatabase().RemoveTreePrototype")]
		internal void RemoveTreePrototype(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.RemoveTreePrototype_Injected(intPtr, index);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000053F4 File Offset: 0x000035F4
		[NativeName("GetDetailDatabase().RemoveDetailPrototype")]
		public void RemoveDetailPrototype(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.RemoveDetailPrototype_Injected(intPtr, index);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005418 File Offset: 0x00003618
		[NativeName("GetTreeDatabase().NeedUpgradeScaledPrototypes")]
		internal bool NeedUpgradeScaledTreePrototypes()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.NeedUpgradeScaledTreePrototypes_Injected(intPtr);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000543C File Offset: 0x0000363C
		[FreeFunction("TerrainDataScriptingInterface::UpgradeScaledTreePrototype", HasExplicitThis = true)]
		internal void UpgradeScaledTreePrototype()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.UpgradeScaledTreePrototype_Injected(intPtr);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005460 File Offset: 0x00003660
		public int alphamapLayers
		{
			[NativeName("GetSplatDatabase().GetSplatCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_alphamapLayers_Injected(intPtr);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005484 File Offset: 0x00003684
		public float[,,] GetAlphamaps(int x, int y, int width, int height)
		{
			bool flag = x < 0 || y < 0 || width < 0 || height < 0;
			if (flag)
			{
				throw new ArgumentException("Invalid argument for GetAlphaMaps");
			}
			return this.Internal_GetAlphamaps(x, y, width, height);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000054C4 File Offset: 0x000036C4
		[FreeFunction("TerrainDataScriptingInterface::GetAlphamaps", HasExplicitThis = true)]
		[return: Unmarshalled]
		private float[,,] Internal_GetAlphamaps(int x, int y, int width, int height)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.Internal_GetAlphamaps_Injected(intPtr, x, y, width, height);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000054EC File Offset: 0x000036EC
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005504 File Offset: 0x00003704
		public int alphamapResolution
		{
			get
			{
				return this.Internal_alphamapResolution;
			}
			set
			{
				int num = value;
				bool flag = value < TerrainData.k_MinimumAlphamapResolution || value > TerrainData.k_MaximumAlphamapResolution;
				if (flag)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"alphamapResolution is clamped to the range of [",
						TerrainData.k_MinimumAlphamapResolution.ToString(),
						", ",
						TerrainData.k_MaximumAlphamapResolution.ToString(),
						"]."
					}));
					num = Math.Min(TerrainData.k_MaximumAlphamapResolution, Math.Max(value, TerrainData.k_MinimumAlphamapResolution));
				}
				this.Internal_alphamapResolution = num;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005590 File Offset: 0x00003790
		[NativeName("GetSplatDatabase().GetAlphamapResolution")]
		[RequiredByNativeCode]
		internal float GetAlphamapResolutionInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.GetAlphamapResolutionInternal_Injected(intPtr);
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000055B4 File Offset: 0x000037B4
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000055D8 File Offset: 0x000037D8
		private int Internal_alphamapResolution
		{
			[NativeName("GetSplatDatabase().GetAlphamapResolution")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_Internal_alphamapResolution_Injected(intPtr);
			}
			[NativeName("GetSplatDatabase().SetAlphamapResolution")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_Internal_alphamapResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000055FC File Offset: 0x000037FC
		public int alphamapWidth
		{
			get
			{
				return this.alphamapResolution;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005614 File Offset: 0x00003814
		public int alphamapHeight
		{
			get
			{
				return this.alphamapResolution;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000562C File Offset: 0x0000382C
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00005644 File Offset: 0x00003844
		public int baseMapResolution
		{
			get
			{
				return this.Internal_baseMapResolution;
			}
			set
			{
				int num = value;
				bool flag = value < TerrainData.k_MinimumBaseMapResolution || value > TerrainData.k_MaximumBaseMapResolution;
				if (flag)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"baseMapResolution is clamped to the range of [",
						TerrainData.k_MinimumBaseMapResolution.ToString(),
						", ",
						TerrainData.k_MaximumBaseMapResolution.ToString(),
						"]."
					}));
					num = Math.Min(TerrainData.k_MaximumBaseMapResolution, Math.Max(value, TerrainData.k_MinimumBaseMapResolution));
				}
				this.Internal_baseMapResolution = num;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000056D0 File Offset: 0x000038D0
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x000056F4 File Offset: 0x000038F4
		private int Internal_baseMapResolution
		{
			[NativeName("GetSplatDatabase().GetBaseMapResolution")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_Internal_baseMapResolution_Injected(intPtr);
			}
			[NativeName("GetSplatDatabase().SetBaseMapResolution")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_Internal_baseMapResolution_Injected(intPtr, value);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005718 File Offset: 0x00003918
		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			bool flag = map.GetLength(2) != this.alphamapLayers;
			if (flag)
			{
				throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", new object[] { this.alphamapLayers }));
			}
			this.Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000577C File Offset: 0x0000397C
		[FreeFunction("TerrainDataScriptingInterface::SetAlphamaps", HasExplicitThis = true)]
		private unsafe void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			int num;
			void* ptr;
			if (map == null || (num = map.Length) == 0)
			{
				num = 0;
				ptr = null;
			}
			else
			{
				ptr = (void*)(&map[0, 0, 0]);
			}
			ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper(ptr, num);
			TerrainData.Internal_SetAlphamaps_Injected(intPtr, x, y, width, height, ref managedSpanWrapper);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000057D0 File Offset: 0x000039D0
		[NativeName("GetSplatDatabase().SetBaseMapsDirty")]
		public void SetBaseMapDirty()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.SetBaseMapDirty_Injected(intPtr);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000057F4 File Offset: 0x000039F4
		[NativeName("GetSplatDatabase().GetAlphaTexture")]
		public Texture2D GetAlphamapTexture(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(TerrainData.GetAlphamapTexture_Injected(intPtr, index));
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000581C File Offset: 0x00003A1C
		public int alphamapTextureCount
		{
			[NativeName("GetSplatDatabase().GetAlphaTextureCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_alphamapTextureCount_Injected(intPtr);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005840 File Offset: 0x00003A40
		public Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[this.alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.GetAlphamapTexture(i);
				}
				return array;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000587C File Offset: 0x00003A7C
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000058A0 File Offset: 0x00003AA0
		[Obsolete("TerrainData.splatPrototypes is obsolete. Use TerrainData.terrainLayers instead.", false)]
		public SplatPrototype[] splatPrototypes
		{
			[FreeFunction("TerrainDataScriptingInterface::GetSplatPrototypes", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_splatPrototypes_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetSplatPrototypes", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_splatPrototypes_Injected(intPtr, value);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000058C4 File Offset: 0x00003AC4
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000058E8 File Offset: 0x00003AE8
		public TerrainLayer[] terrainLayers
		{
			[FreeFunction("TerrainDataScriptingInterface::GetTerrainLayers", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_terrainLayers_Injected(intPtr);
			}
			[FreeFunction("TerrainDataScriptingInterface::SetTerrainLayers", HasExplicitThis = true)]
			[param: Unmarshalled]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TerrainData.set_terrainLayers_Injected(intPtr, value);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000590C File Offset: 0x00003B0C
		[NativeName("GetTreeDatabase().AddTree")]
		internal void AddTree(ref TreeInstance tree)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.AddTree_Injected(intPtr, ref tree);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005930 File Offset: 0x00003B30
		[NativeName("GetTreeDatabase().RemoveTrees")]
		internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return TerrainData.RemoveTrees_Injected(intPtr, ref position, radius, prototypeIndex);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005958 File Offset: 0x00003B58
		[NativeName("GetHeightmap().CopyHeightmapFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHeightmap(RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_CopyActiveRenderTextureToHeightmap_Injected(intPtr, ref rect, destX, destY, syncControl);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005980 File Offset: 0x00003B80
		[NativeName("GetHeightmap().DirtyHeightmapRegion")]
		private void Internal_DirtyHeightmapRegion(int x, int y, int width, int height, TerrainHeightmapSyncControl syncControl)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_DirtyHeightmapRegion_Injected(intPtr, x, y, width, height, syncControl);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000059AC File Offset: 0x00003BAC
		[NativeName("GetHeightmap().SyncHeightmapGPUModifications")]
		public void SyncHeightmap()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.SyncHeightmap_Injected(intPtr);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000059D0 File Offset: 0x00003BD0
		[NativeName("GetHeightmap().CopyHolesFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHoles(RectInt rect, int destX, int destY, bool allowDelayedCPUSync)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_CopyActiveRenderTextureToHoles_Injected(intPtr, ref rect, destX, destY, allowDelayedCPUSync);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000059F8 File Offset: 0x00003BF8
		[NativeName("GetHeightmap().DirtyHolesRegion")]
		private void Internal_DirtyHolesRegion(int x, int y, int width, int height, bool allowDelayedCPUSync)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_DirtyHolesRegion_Injected(intPtr, x, y, width, height, allowDelayedCPUSync);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005A24 File Offset: 0x00003C24
		[NativeName("GetHeightmap().SyncHolesGPUModifications")]
		private void Internal_SyncHoles()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_SyncHoles_Injected(intPtr);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005A48 File Offset: 0x00003C48
		[NativeName("GetSplatDatabase().MarkDirtyRegion")]
		private void Internal_MarkAlphamapDirtyRegion(int alphamapIndex, int x, int y, int width, int height)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_MarkAlphamapDirtyRegion_Injected(intPtr, alphamapIndex, x, y, width, height);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005A74 File Offset: 0x00003C74
		[NativeName("GetSplatDatabase().ClearDirtyRegion")]
		private void Internal_ClearAlphamapDirtyRegion(int alphamapIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_ClearAlphamapDirtyRegion_Injected(intPtr, alphamapIndex);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005A98 File Offset: 0x00003C98
		[NativeName("GetSplatDatabase().SyncGPUModifications")]
		private void Internal_SyncAlphamaps()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TerrainData.Internal_SyncAlphamaps_Injected(intPtr);
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00005ABC File Offset: 0x00003CBC
		internal TextureFormat atlasFormat
		{
			[NativeName("GetDetailDatabase().GetAtlasTextureFormat")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_atlasFormat_Injected(intPtr);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00005AE0 File Offset: 0x00003CE0
		internal Terrain[] users
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TerrainData>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TerrainData.get_users_Injected(intPtr);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00005B04 File Offset: 0x00003D04
		private static bool SupportsCopyTextureBetweenRTAndTexture
		{
			get
			{
				return (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005B24 File Offset: 0x00003D24
		public void CopyActiveRenderTextureToHeightmap(RectInt sourceRect, Vector2Int dest, TerrainHeightmapSyncControl syncControl)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = active == null;
			if (flag)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			bool flag2 = sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			bool flag3 = dest.x < 0 || dest.x + sourceRect.width > this.heightmapResolution;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			bool flag4 = dest.y < 0 || dest.y + sourceRect.height > this.heightmapResolution;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			this.Internal_CopyActiveRenderTextureToHeightmap(sourceRect, dest.x, dest.y, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005C40 File Offset: 0x00003E40
		public void DirtyHeightmapRegion(RectInt region, TerrainHeightmapSyncControl syncControl)
		{
			int heightmapResolution = this.heightmapResolution;
			bool flag = region.x < 0 || region.x >= heightmapResolution;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			bool flag2 = region.width <= 0 || region.xMax > heightmapResolution;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			bool flag3 = region.y < 0 || region.y >= heightmapResolution;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			bool flag4 = region.height <= 0 || region.yMax > heightmapResolution;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			this.Internal_DirtyHeightmapRegion(region.x, region.y, region.width, region.height, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, region, syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00005D25 File Offset: 0x00003F25
		public static string AlphamapTextureName
		{
			get
			{
				return "alphamap";
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005D2C File Offset: 0x00003F2C
		public static string HolesTextureName
		{
			get
			{
				return "holes";
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005D34 File Offset: 0x00003F34
		public void CopyActiveRenderTextureToTexture(string textureName, int textureIndex, RectInt sourceRect, Vector2Int dest, bool allowDelayedCPUSync)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			RenderTexture active = RenderTexture.active;
			bool flag2 = active == null;
			if (flag2)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			bool flag3 = textureName == TerrainData.HolesTextureName;
			int num2;
			int num;
			if (flag3)
			{
				bool flag4 = textureIndex != 0;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				bool flag5 = active == this.holesTexture;
				if (flag5)
				{
					throw new ArgumentException("source", "Active RenderTexture cannot be holesTexture.");
				}
				num = (num2 = this.holesResolution);
			}
			else
			{
				bool flag6 = textureName == TerrainData.AlphamapTextureName;
				if (!flag6)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				bool flag7 = textureIndex < 0 || textureIndex >= this.alphamapTextureCount;
				if (flag7)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				num = (num2 = this.alphamapResolution);
			}
			bool flag8 = sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height;
			if (flag8)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			bool flag9 = dest.x < 0 || dest.x + sourceRect.width > num2;
			if (flag9)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			bool flag10 = dest.y < 0 || dest.y + sourceRect.height > num;
			if (flag10)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			bool flag11 = textureName == TerrainData.HolesTextureName;
			if (flag11)
			{
				this.Internal_CopyActiveRenderTextureToHoles(sourceRect, dest.x, dest.y, allowDelayedCPUSync);
			}
			else
			{
				Texture2D alphamapTexture = this.GetAlphamapTexture(textureIndex);
				allowDelayedCPUSync = allowDelayedCPUSync && TerrainData.SupportsCopyTextureBetweenRTAndTexture && QualitySettings.globalTextureMipmapLimit == 0;
				bool flag12 = allowDelayedCPUSync;
				if (flag12)
				{
					bool flag13 = alphamapTexture.mipmapCount > 1;
					if (flag13)
					{
						RenderTexture temporary = RenderTexture.GetTemporary(new RenderTextureDescriptor(alphamapTexture.width, alphamapTexture.height, active.graphicsFormat, active.depthStencilFormat)
						{
							sRGB = false,
							useMipMap = true,
							autoGenerateMips = false
						});
						bool flag14 = !temporary.IsCreated();
						if (flag14)
						{
							temporary.Create();
						}
						Graphics.CopyTexture(alphamapTexture, 0, 0, temporary, 0, 0);
						Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, temporary, 0, 0, dest.x, dest.y);
						temporary.GenerateMips();
						Graphics.CopyTexture(temporary, alphamapTexture);
						RenderTexture.ReleaseTemporary(temporary);
					}
					else
					{
						Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, alphamapTexture, 0, 0, dest.x, dest.y);
					}
					this.Internal_MarkAlphamapDirtyRegion(textureIndex, dest.x, dest.y, sourceRect.width, sourceRect.height);
				}
				else
				{
					alphamapTexture.ReadPixels(new Rect((float)sourceRect.x, (float)sourceRect.y, (float)sourceRect.width, (float)sourceRect.height), dest.x, dest.y);
					alphamapTexture.Apply(true);
					this.Internal_ClearAlphamapDirtyRegion(textureIndex);
				}
				TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), !allowDelayedCPUSync);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000060D4 File Offset: 0x000042D4
		public void DirtyTextureRegion(string textureName, RectInt region, bool allowDelayedCPUSync)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			bool flag2 = textureName == TerrainData.AlphamapTextureName;
			int num;
			if (flag2)
			{
				num = this.alphamapResolution;
			}
			else
			{
				bool flag3 = textureName == TerrainData.HolesTextureName;
				if (!flag3)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				num = this.holesResolution;
			}
			bool flag4 = region.x < 0 || region.x >= num;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			bool flag5 = region.width <= 0 || region.xMax > num;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			bool flag6 = region.y < 0 || region.y >= num;
			if (flag6)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			bool flag7 = region.height <= 0 || region.yMax > num;
			if (flag7)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			bool flag8 = textureName == TerrainData.HolesTextureName;
			if (flag8)
			{
				this.Internal_DirtyHolesRegion(region.x, region.y, region.width, region.height, allowDelayedCPUSync);
			}
			else
			{
				this.Internal_MarkAlphamapDirtyRegion(-1, region.x, region.y, region.width, region.height);
				bool flag9 = !allowDelayedCPUSync;
				if (flag9)
				{
					this.SyncTexture(textureName);
				}
				else
				{
					TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, region, false);
				}
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006264 File Offset: 0x00004464
		public void SyncTexture(string textureName)
		{
			bool flag = string.IsNullOrEmpty(textureName);
			if (flag)
			{
				throw new ArgumentNullException("textureName");
			}
			bool flag2 = textureName == TerrainData.AlphamapTextureName;
			if (flag2)
			{
				this.Internal_SyncAlphamaps();
			}
			else
			{
				bool flag3 = textureName == TerrainData.HolesTextureName;
				if (!flag3)
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				bool flag4 = this.IsHolesTextureCompressed();
				if (flag4)
				{
					throw new InvalidOperationException("Holes texture is compressed. Compressed holes texture can not be read back from GPU. Use TerrainData.enableHolesTextureCompression to disable holes texture compression.");
				}
				this.Internal_SyncHoles();
			}
		}

		// Token: 0x060001D7 RID: 471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_heightmapTexture_Injected(IntPtr _unity_self);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_internalHeightmapResolution_Injected(IntPtr _unity_self);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_internalHeightmapResolution_Injected(IntPtr _unity_self, int value);

		// Token: 0x060001DA RID: 474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_heightmapScale_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001DB RID: 475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enableHolesTextureCompression_Injected(IntPtr _unity_self);

		// Token: 0x060001DC RID: 476
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enableHolesTextureCompression_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001DD RID: 477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsHolesTextureCompressed_Injected(IntPtr _unity_self);

		// Token: 0x060001DE RID: 478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetHolesTexture_Injected(IntPtr _unity_self);

		// Token: 0x060001DF RID: 479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCompressedHolesTexture_Injected(IntPtr _unity_self);

		// Token: 0x060001E0 RID: 480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001E1 RID: 481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060001E2 RID: 482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060001E3 RID: 483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetHeight_Injected(IntPtr _unity_self, int x, int y);

		// Token: 0x060001E4 RID: 484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetInterpolatedHeight_Injected(IntPtr _unity_self, float x, float y);

		// Token: 0x060001E5 RID: 485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetInterpolatedHeights_Injected(IntPtr _unity_self, ref ManagedSpanWrapper results, int resultXDimension, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval);

		// Token: 0x060001E6 RID: 486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float[,] Internal_GetHeights_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height);

		// Token: 0x060001E7 RID: 487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHeights_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height, ref ManagedSpanWrapper heights);

		// Token: 0x060001E8 RID: 488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPatchMinMaxHeights_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060001E9 RID: 489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverrideMinMaxPatchHeights_Injected(IntPtr _unity_self, ref ManagedSpanWrapper minMaxHeights);

		// Token: 0x060001EA RID: 490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMaximumHeightError_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverrideMaximumHeightError_Injected(IntPtr _unity_self, ref ManagedSpanWrapper maxError);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHeightsDelayLOD_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height, ref ManagedSpanWrapper heights);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHoles_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height, ref ManagedSpanWrapper holes);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool[,] Internal_GetHoles_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsHole_Injected(IntPtr _unity_self, int x, int y);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHolesDelayLOD_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height, ref ManagedSpanWrapper holes);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetSteepness_Injected(IntPtr _unity_self, float x, float y);

		// Token: 0x060001F2 RID: 498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterpolatedNormal_Injected(IntPtr _unity_self, float x, float y, out Vector3 ret);

		// Token: 0x060001F3 RID: 499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAdjustedSize_Injected(IntPtr _unity_self, int size);

		// Token: 0x060001F4 RID: 500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wavingGrassStrength_Injected(IntPtr _unity_self);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wavingGrassStrength_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wavingGrassAmount_Injected(IntPtr _unity_self);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wavingGrassAmount_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_wavingGrassSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wavingGrassSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001FA RID: 506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_wavingGrassTint_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_wavingGrassTint_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060001FC RID: 508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_detailWidth_Injected(IntPtr _unity_self);

		// Token: 0x060001FD RID: 509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_detailHeight_Injected(IntPtr _unity_self);

		// Token: 0x060001FE RID: 510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_maxDetailScatterPerRes_Injected(IntPtr _unity_self);

		// Token: 0x060001FF RID: 511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetDetailResolution_Injected(IntPtr _unity_self, int patchCount, int resolutionPerPatch);

		// Token: 0x06000200 RID: 512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetDetailScatterMode_Injected(IntPtr _unity_self, DetailScatterMode scatterMode);

		// Token: 0x06000201 RID: 513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_detailPatchCount_Injected(IntPtr _unity_self);

		// Token: 0x06000202 RID: 514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_detailResolution_Injected(IntPtr _unity_self);

		// Token: 0x06000203 RID: 515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_detailResolutionPerPatch_Injected(IntPtr _unity_self);

		// Token: 0x06000204 RID: 516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DetailScatterMode get_detailScatterMode_Injected(IntPtr _unity_self);

		// Token: 0x06000205 RID: 517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetDirtyDetails_Injected(IntPtr _unity_self);

		// Token: 0x06000206 RID: 518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RefreshPrototypes_Injected(IntPtr _unity_self);

		// Token: 0x06000207 RID: 519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DetailPrototype[] get_detailPrototypes_Injected(IntPtr _unity_self);

		// Token: 0x06000208 RID: 520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_detailPrototypes_Injected(IntPtr _unity_self, DetailPrototype[] value);

		// Token: 0x06000209 RID: 521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSupportedLayers_Injected(IntPtr _unity_self, int xBase, int yBase, int totalWidth, int totalHeight, out BlittableArrayWrapper ret);

		// Token: 0x0600020A RID: 522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int[,] GetDetailLayer_Injected(IntPtr _unity_self, int xBase, int yBase, int width, int height, int layer);

		// Token: 0x0600020B RID: 523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeDetailInstanceTransforms_Injected(IntPtr _unity_self, int patchX, int patchY, int layer, float density, out Bounds bounds, out BlittableArrayWrapper ret);

		// Token: 0x0600020C RID: 524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float ComputeDetailCoverage_Injected(IntPtr _unity_self, int detailPrototypeIndex);

		// Token: 0x0600020D RID: 525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetDetailLayer_Injected(IntPtr _unity_self, int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, ref ManagedSpanWrapper data);

		// Token: 0x0600020E RID: 526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTreeInstances_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x0600020F RID: 527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTreeInstances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper instances, bool snapToHeightmap);

		// Token: 0x06000210 RID: 528
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTreeInstance_Injected(IntPtr _unity_self, int index, out TreeInstance ret);

		// Token: 0x06000211 RID: 529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTreeInstance_Injected(IntPtr _unity_self, int index, [In] ref TreeInstance instance);

		// Token: 0x06000212 RID: 530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_treeInstanceCount_Injected(IntPtr _unity_self);

		// Token: 0x06000213 RID: 531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TreePrototype[] get_treePrototypes_Injected(IntPtr _unity_self);

		// Token: 0x06000214 RID: 532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_treePrototypes_Injected(IntPtr _unity_self, TreePrototype[] value);

		// Token: 0x06000215 RID: 533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveTreePrototype_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000216 RID: 534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveDetailPrototype_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000217 RID: 535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool NeedUpgradeScaledTreePrototypes_Injected(IntPtr _unity_self);

		// Token: 0x06000218 RID: 536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpgradeScaledTreePrototype_Injected(IntPtr _unity_self);

		// Token: 0x06000219 RID: 537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_alphamapLayers_Injected(IntPtr _unity_self);

		// Token: 0x0600021A RID: 538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float[,,] Internal_GetAlphamaps_Injected(IntPtr _unity_self, int x, int y, int width, int height);

		// Token: 0x0600021B RID: 539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAlphamapResolutionInternal_Injected(IntPtr _unity_self);

		// Token: 0x0600021C RID: 540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_Internal_alphamapResolution_Injected(IntPtr _unity_self);

		// Token: 0x0600021D RID: 541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Internal_alphamapResolution_Injected(IntPtr _unity_self, int value);

		// Token: 0x0600021E RID: 542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_Internal_baseMapResolution_Injected(IntPtr _unity_self);

		// Token: 0x0600021F RID: 543
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Internal_baseMapResolution_Injected(IntPtr _unity_self, int value);

		// Token: 0x06000220 RID: 544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetAlphamaps_Injected(IntPtr _unity_self, int x, int y, int width, int height, ref ManagedSpanWrapper map);

		// Token: 0x06000221 RID: 545
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBaseMapDirty_Injected(IntPtr _unity_self);

		// Token: 0x06000222 RID: 546
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAlphamapTexture_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000223 RID: 547
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_alphamapTextureCount_Injected(IntPtr _unity_self);

		// Token: 0x06000224 RID: 548
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SplatPrototype[] get_splatPrototypes_Injected(IntPtr _unity_self);

		// Token: 0x06000225 RID: 549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_splatPrototypes_Injected(IntPtr _unity_self, SplatPrototype[] value);

		// Token: 0x06000226 RID: 550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TerrainLayer[] get_terrainLayers_Injected(IntPtr _unity_self);

		// Token: 0x06000227 RID: 551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_terrainLayers_Injected(IntPtr _unity_self, TerrainLayer[] value);

		// Token: 0x06000228 RID: 552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTree_Injected(IntPtr _unity_self, ref TreeInstance tree);

		// Token: 0x06000229 RID: 553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RemoveTrees_Injected(IntPtr _unity_self, [In] ref Vector2 position, float radius, int prototypeIndex);

		// Token: 0x0600022A RID: 554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CopyActiveRenderTextureToHeightmap_Injected(IntPtr _unity_self, [In] ref RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl);

		// Token: 0x0600022B RID: 555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DirtyHeightmapRegion_Injected(IntPtr _unity_self, int x, int y, int width, int height, TerrainHeightmapSyncControl syncControl);

		// Token: 0x0600022C RID: 556
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SyncHeightmap_Injected(IntPtr _unity_self);

		// Token: 0x0600022D RID: 557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CopyActiveRenderTextureToHoles_Injected(IntPtr _unity_self, [In] ref RectInt rect, int destX, int destY, bool allowDelayedCPUSync);

		// Token: 0x0600022E RID: 558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DirtyHolesRegion_Injected(IntPtr _unity_self, int x, int y, int width, int height, bool allowDelayedCPUSync);

		// Token: 0x0600022F RID: 559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SyncHoles_Injected(IntPtr _unity_self);

		// Token: 0x06000230 RID: 560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_MarkAlphamapDirtyRegion_Injected(IntPtr _unity_self, int alphamapIndex, int x, int y, int width, int height);

		// Token: 0x06000231 RID: 561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ClearAlphamapDirtyRegion_Injected(IntPtr _unity_self, int alphamapIndex);

		// Token: 0x06000232 RID: 562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SyncAlphamaps_Injected(IntPtr _unity_self);

		// Token: 0x06000233 RID: 563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureFormat get_atlasFormat_Injected(IntPtr _unity_self);

		// Token: 0x06000234 RID: 564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Terrain[] get_users_Injected(IntPtr _unity_self);

		// Token: 0x0400009B RID: 155
		private const string k_ScriptingInterfaceName = "TerrainDataScriptingInterface";

		// Token: 0x0400009C RID: 156
		private const string k_ScriptingInterfacePrefix = "TerrainDataScriptingInterface::";

		// Token: 0x0400009D RID: 157
		private const string k_HeightmapPrefix = "GetHeightmap().";

		// Token: 0x0400009E RID: 158
		private const string k_DetailDatabasePrefix = "GetDetailDatabase().";

		// Token: 0x0400009F RID: 159
		private const string k_TreeDatabasePrefix = "GetTreeDatabase().";

		// Token: 0x040000A0 RID: 160
		private const string k_SplatDatabasePrefix = "GetSplatDatabase().";

		// Token: 0x040000A1 RID: 161
		internal static readonly int k_MaximumResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxHeightmapRes);

		// Token: 0x040000A2 RID: 162
		internal static readonly int k_MinimumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinDetailResPerPatch);

		// Token: 0x040000A3 RID: 163
		internal static readonly int k_MaximumDetailResolutionPerPatch = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailResPerPatch);

		// Token: 0x040000A4 RID: 164
		internal static readonly int k_MaximumDetailPatchCount = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxDetailPatchCount);

		// Token: 0x040000A5 RID: 165
		internal static readonly int k_MinimumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinAlphamapRes);

		// Token: 0x040000A6 RID: 166
		internal static readonly int k_MaximumAlphamapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxAlphamapRes);

		// Token: 0x040000A7 RID: 167
		internal static readonly int k_MinimumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MinBaseMapRes);

		// Token: 0x040000A8 RID: 168
		internal static readonly int k_MaximumBaseMapResolution = TerrainData.GetBoundaryValue(TerrainData.BoundaryValueType.MaxBaseMapRes);

		// Token: 0x0200002B RID: 43
		private enum BoundaryValueType
		{
			// Token: 0x040000AA RID: 170
			MaxHeightmapRes,
			// Token: 0x040000AB RID: 171
			MinDetailResPerPatch,
			// Token: 0x040000AC RID: 172
			MaxDetailResPerPatch,
			// Token: 0x040000AD RID: 173
			MaxDetailPatchCount,
			// Token: 0x040000AE RID: 174
			MaxCoveragePerRes,
			// Token: 0x040000AF RID: 175
			MinAlphamapRes,
			// Token: 0x040000B0 RID: 176
			MaxAlphamapRes,
			// Token: 0x040000B1 RID: 177
			MinBaseMapRes,
			// Token: 0x040000B2 RID: 178
			MaxBaseMapRes
		}
	}
}
