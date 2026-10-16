using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeHeader("Modules/Grid/Public/Grid.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapTile.h")]
	[NativeType(Header = "Modules/Tilemap/Public/Tilemap.h")]
	public sealed class Tilemap : GridLayout
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000040 RID: 64 RVA: 0x000028B8 File Offset: 0x00000AB8
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x000028EC File Offset: 0x00000AEC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Tilemap, Tilemap.SyncTile[]> tilemapTileChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002920 File Offset: 0x00000B20
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002954 File Offset: 0x00000B54
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Tilemap, NativeArray<Vector3Int>> tilemapPositionsChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000044 RID: 68 RVA: 0x00002988 File Offset: 0x00000B88
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x000029BC File Offset: 0x00000BBC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<Tilemap, NativeArray<Vector3Int>> loopEndedForTileAnimation;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000029F0 File Offset: 0x00000BF0
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002A08 File Offset: 0x00000C08
		internal bool bufferSyncTile
		{
			get
			{
				return this.m_BufferSyncTile;
			}
			set
			{
				bool flag = !value && this.m_BufferSyncTile != value && Tilemap.HasSyncTileCallback();
				if (flag)
				{
					this.SendAndClearSyncTileBuffer();
				}
				this.m_BufferSyncTile = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A3C File Offset: 0x00000C3C
		internal static bool HasLoopEndedForTileAnimationCallback()
		{
			return Tilemap.loopEndedForTileAnimation != null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A58 File Offset: 0x00000C58
		private unsafe void HandleLoopEndedForTileAnimationCallback(int count, IntPtr positionsIntPtr)
		{
			bool flag = !Tilemap.HasLoopEndedForTileAnimationCallback();
			if (!flag)
			{
				void* ptr = positionsIntPtr.ToPointer();
				NativeArray<Vector3Int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(ptr, count, Allocator.Invalid);
				this.SendLoopEndedForTileAnimationCallback(nativeArray);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A90 File Offset: 0x00000C90
		private void SendLoopEndedForTileAnimationCallback(NativeArray<Vector3Int> positions)
		{
			try
			{
				Tilemap.loopEndedForTileAnimation(this, positions);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002ACC File Offset: 0x00000CCC
		internal static bool HasSyncTileCallback()
		{
			return Tilemap.tilemapTileChanged != null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002AE8 File Offset: 0x00000CE8
		internal static bool HasPositionsChangedCallback()
		{
			return Tilemap.tilemapPositionsChanged != null;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B04 File Offset: 0x00000D04
		private void HandleSyncTileCallback(Tilemap.SyncTile[] syncTiles)
		{
			bool flag = Tilemap.tilemapTileChanged == null;
			if (!flag)
			{
				this.SendTilemapTileChangedCallback(syncTiles);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B28 File Offset: 0x00000D28
		private unsafe void HandlePositionsChangedCallback(int count, IntPtr positionsIntPtr)
		{
			bool flag = !Tilemap.HasPositionsChangedCallback();
			if (!flag)
			{
				void* ptr = positionsIntPtr.ToPointer();
				NativeArray<Vector3Int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3Int>(ptr, count, Allocator.Invalid);
				this.SendTilemapPositionsChangedCallback(nativeArray);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B60 File Offset: 0x00000D60
		private void SendTilemapTileChangedCallback(Tilemap.SyncTile[] syncTiles)
		{
			try
			{
				Tilemap.tilemapTileChanged(this, syncTiles);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B9C File Offset: 0x00000D9C
		private void SendTilemapPositionsChangedCallback(NativeArray<Vector3Int> positions)
		{
			try
			{
				Tilemap.tilemapPositionsChanged(this, positions);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002BD8 File Offset: 0x00000DD8
		internal static void SetSyncTileCallback(Action<Tilemap, Tilemap.SyncTile[]> callback)
		{
			Tilemap.tilemapTileChanged += callback;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002BE2 File Offset: 0x00000DE2
		internal static void RemoveSyncTileCallback(Action<Tilemap, Tilemap.SyncTile[]> callback)
		{
			Tilemap.tilemapTileChanged -= callback;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002BEC File Offset: 0x00000DEC
		public Grid layoutGrid
		{
			[NativeMethod(Name = "GetAttachedGrid")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Grid>(Tilemap.get_layoutGrid_Injected(intPtr));
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C14 File Offset: 0x00000E14
		public Vector3 GetCellCenterLocal(Vector3Int position)
		{
			return base.CellToLocalInterpolated(position) + base.CellToLocalInterpolated(this.tileAnchor);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C44 File Offset: 0x00000E44
		public Vector3 GetCellCenterWorld(Vector3Int position)
		{
			return base.LocalToWorld(base.CellToLocalInterpolated(position) + base.CellToLocalInterpolated(this.tileAnchor));
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002C7C File Offset: 0x00000E7C
		public BoundsInt cellBounds
		{
			get
			{
				return new BoundsInt(this.origin, this.size);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002CA0 File Offset: 0x00000EA0
		[NativeProperty("TilemapBoundsScripting")]
		public Bounds localBounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Tilemap.get_localBounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002CC8 File Offset: 0x00000EC8
		[NativeProperty("TilemapFrameBoundsScripting")]
		internal Bounds localFrameBounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Tilemap.get_localFrameBounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002CF0 File Offset: 0x00000EF0
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002D14 File Offset: 0x00000F14
		public float animationFrameRate
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Tilemap.get_animationFrameRate_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_animationFrameRate_Injected(intPtr, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D38 File Offset: 0x00000F38
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002D60 File Offset: 0x00000F60
		public Color color
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Color color;
				Tilemap.get_color_Injected(intPtr, out color);
				return color;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_color_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D84 File Offset: 0x00000F84
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002DAC File Offset: 0x00000FAC
		public Vector3Int origin
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3Int vector3Int;
				Tilemap.get_origin_Injected(intPtr, out vector3Int);
				return vector3Int;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_origin_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002DD0 File Offset: 0x00000FD0
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public Vector3Int size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3Int vector3Int;
				Tilemap.get_size_Injected(intPtr, out vector3Int);
				return vector3Int;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002E1C File Offset: 0x0000101C
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002E44 File Offset: 0x00001044
		[NativeProperty(Name = "TileAnchorScripting")]
		public Vector3 tileAnchor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Tilemap.get_tileAnchor_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_tileAnchor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002E8C File Offset: 0x0000108C
		public Tilemap.Orientation orientation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Tilemap.get_orientation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_orientation_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002EB0 File Offset: 0x000010B0
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002ED8 File Offset: 0x000010D8
		public Matrix4x4 orientationMatrix
		{
			[NativeMethod(Name = "GetTileOrientationMatrix")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Matrix4x4 matrix4x;
				Tilemap.get_orientationMatrix_Injected(intPtr, out matrix4x);
				return matrix4x;
			}
			[NativeMethod(Name = "SetOrientationMatrix")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Tilemap.set_orientationMatrix_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002EFC File Offset: 0x000010FC
		internal Object GetTileAsset(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(Tilemap.GetTileAsset_Injected(intPtr, ref position));
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002F28 File Offset: 0x00001128
		public TileBase GetTile(Vector3Int position)
		{
			return this.GetTileAsset(position) as TileBase;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002F48 File Offset: 0x00001148
		public T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return this.GetTileAsset(position) as T;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F6C File Offset: 0x0000116C
		internal Object[] GetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetTileAssetsBlock_Injected(intPtr, ref position, ref blockDimensions);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F94 File Offset: 0x00001194
		public TileBase[] GetTilesBlock(BoundsInt bounds)
		{
			Object[] tileAssetsBlock = this.GetTileAssetsBlock(bounds.min, bounds.size);
			TileBase[] array = new TileBase[tileAssetsBlock.Length];
			for (int i = 0; i < tileAssetsBlock.Length; i++)
			{
				array[i] = (TileBase)tileAssetsBlock[i];
			}
			return array;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FE8 File Offset: 0x000011E8
		[FreeFunction(Name = "TilemapBindings::GetTileAssetsBlockNonAlloc", HasExplicitThis = true)]
		internal int GetTileAssetsBlockNonAlloc(Vector3Int startPosition, Vector3Int endPosition, [Unmarshalled] Object[] tiles)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetTileAssetsBlockNonAlloc_Injected(intPtr, ref startPosition, ref endPosition, tiles);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003010 File Offset: 0x00001210
		public int GetTilesBlockNonAlloc(BoundsInt bounds, TileBase[] tiles)
		{
			return this.GetTileAssetsBlockNonAlloc(bounds.min, bounds.size, tiles);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000303C File Offset: 0x0000123C
		public int GetTilesRangeCount(Vector3Int startPosition, Vector3Int endPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetTilesRangeCount_Injected(intPtr, ref startPosition, ref endPosition);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003064 File Offset: 0x00001264
		[FreeFunction(Name = "TilemapBindings::GetTileAssetsRangeNonAlloc", HasExplicitThis = true)]
		internal unsafe int GetTileAssetsRangeNonAlloc(Vector3Int startPosition, Vector3Int endPosition, Vector3Int[] positions, [Unmarshalled] Object[] tiles)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3Int> span = new Span<Vector3Int>(positions);
			int tileAssetsRangeNonAlloc_Injected;
			fixed (Vector3Int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				tileAssetsRangeNonAlloc_Injected = Tilemap.GetTileAssetsRangeNonAlloc_Injected(intPtr, ref startPosition, ref endPosition, ref managedSpanWrapper, tiles);
			}
			return tileAssetsRangeNonAlloc_Injected;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000030B4 File Offset: 0x000012B4
		public int GetTilesRangeNonAlloc(Vector3Int startPosition, Vector3Int endPosition, Vector3Int[] positions, TileBase[] tiles)
		{
			return this.GetTileAssetsRangeNonAlloc(startPosition, endPosition, positions, tiles);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000030D4 File Offset: 0x000012D4
		internal void SetTileAsset(Vector3Int position, Object tile)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetTileAsset_Injected(intPtr, ref position, Object.MarshalledUnityObject.Marshal<Object>(tile));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030FE File Offset: 0x000012FE
		public void SetTile(Vector3Int position, TileBase tile)
		{
			this.SetTileAsset(position, tile);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000310C File Offset: 0x0000130C
		internal unsafe void SetTileAssets(Vector3Int[] positionArray, Object[] tileArray)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3Int> span = new Span<Vector3Int>(positionArray);
			fixed (Vector3Int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Tilemap.SetTileAssets_Injected(intPtr, ref managedSpanWrapper, tileArray);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003158 File Offset: 0x00001358
		public void SetTiles(Vector3Int[] positionArray, TileBase[] tileArray)
		{
			this.SetTileAssets(positionArray, tileArray);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003174 File Offset: 0x00001374
		[NativeMethod(Name = "SetTileAssetsBlock")]
		private void INTERNAL_CALL_SetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions, Object[] tileArray)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.INTERNAL_CALL_SetTileAssetsBlock_Injected(intPtr, ref position, ref blockDimensions, tileArray);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000319C File Offset: 0x0000139C
		public void SetTilesBlock(BoundsInt position, TileBase[] tileArray)
		{
			this.INTERNAL_CALL_SetTileAssetsBlock(position.min, position.size, tileArray);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000031C4 File Offset: 0x000013C4
		[NativeMethod(Name = "SetTileChangeData")]
		public void SetTile(TileChangeData tileChangeData, bool ignoreLockFlags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetTile_Injected(intPtr, ref tileChangeData, ignoreLockFlags);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000031EC File Offset: 0x000013EC
		[NativeMethod(Name = "SetTileChangeDataArray")]
		public void SetTiles(TileChangeData[] tileChangeDataArray, bool ignoreLockFlags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetTiles_Injected(intPtr, tileChangeDataArray, ignoreLockFlags);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003210 File Offset: 0x00001410
		public bool HasTile(Vector3Int position)
		{
			return this.GetTileAsset(position) != null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003230 File Offset: 0x00001430
		[NativeMethod(Name = "RefreshTileAsset")]
		public void RefreshTile(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RefreshTile_Injected(intPtr, ref position);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003254 File Offset: 0x00001454
		[FreeFunction(Name = "TilemapBindings::RefreshTileAssetsNative", HasExplicitThis = true)]
		internal unsafe void RefreshTilesNative(void* positions, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RefreshTilesNative_Injected(intPtr, positions, count);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003278 File Offset: 0x00001478
		[NativeMethod(Name = "RefreshAllTileAssets")]
		public void RefreshAllTiles()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RefreshAllTiles_Injected(intPtr);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000329C File Offset: 0x0000149C
		internal void SwapTileAsset(Object changeTile, Object newTile)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SwapTileAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(changeTile), Object.MarshalledUnityObject.Marshal<Object>(newTile));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000032CA File Offset: 0x000014CA
		public void SwapTile(TileBase changeTile, TileBase newTile)
		{
			this.SwapTileAsset(changeTile, newTile);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000032D8 File Offset: 0x000014D8
		internal bool ContainsTileAsset(Object tileAsset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.ContainsTileAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(tileAsset));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003300 File Offset: 0x00001500
		public bool ContainsTile(TileBase tileAsset)
		{
			return this.ContainsTileAsset(tileAsset);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000331C File Offset: 0x0000151C
		public int GetUsedTilesCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetUsedTilesCount_Injected(intPtr);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003340 File Offset: 0x00001540
		public int GetUsedSpritesCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetUsedSpritesCount_Injected(intPtr);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003364 File Offset: 0x00001564
		public int GetUsedTilesNonAlloc(TileBase[] usedTiles)
		{
			return this.Internal_GetUsedTilesNonAlloc(usedTiles);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003380 File Offset: 0x00001580
		public int GetUsedSpritesNonAlloc(Sprite[] usedSprites)
		{
			return this.Internal_GetUsedSpritesNonAlloc(usedSprites);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000339C File Offset: 0x0000159C
		[FreeFunction(Name = "TilemapBindings::GetUsedTilesNonAlloc", HasExplicitThis = true)]
		internal int Internal_GetUsedTilesNonAlloc([Unmarshalled] Object[] usedTiles)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.Internal_GetUsedTilesNonAlloc_Injected(intPtr, usedTiles);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000033C0 File Offset: 0x000015C0
		[FreeFunction(Name = "TilemapBindings::GetUsedSpritesNonAlloc", HasExplicitThis = true)]
		internal int Internal_GetUsedSpritesNonAlloc([Unmarshalled] Object[] usedSprites)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.Internal_GetUsedSpritesNonAlloc_Injected(intPtr, usedSprites);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000033E4 File Offset: 0x000015E4
		public Sprite GetSprite(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Sprite>(Tilemap.GetSprite_Injected(intPtr, ref position));
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003410 File Offset: 0x00001610
		public Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			Tilemap.GetTransformMatrix_Injected(intPtr, ref position, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003438 File Offset: 0x00001638
		public void SetTransformMatrix(Vector3Int position, Matrix4x4 transform)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetTransformMatrix_Injected(intPtr, ref position, ref transform);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003460 File Offset: 0x00001660
		[NativeMethod(Name = "GetTileColor")]
		public Color GetColor(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			Tilemap.GetColor_Injected(intPtr, ref position, out color);
			return color;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003488 File Offset: 0x00001688
		[NativeMethod(Name = "SetTileColor")]
		public void SetColor(Vector3Int position, Color color)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetColor_Injected(intPtr, ref position, ref color);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000034B0 File Offset: 0x000016B0
		public TileFlags GetTileFlags(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetTileFlags_Injected(intPtr, ref position);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000034D4 File Offset: 0x000016D4
		public void SetTileFlags(Vector3Int position, TileFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetTileFlags_Injected(intPtr, ref position, flags);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000034FC File Offset: 0x000016FC
		public void AddTileFlags(Vector3Int position, TileFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.AddTileFlags_Injected(intPtr, ref position, flags);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003524 File Offset: 0x00001724
		public void RemoveTileFlags(Vector3Int position, TileFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RemoveTileFlags_Injected(intPtr, ref position, flags);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000354C File Offset: 0x0000174C
		[NativeMethod(Name = "GetTileInstantiatedObject")]
		public GameObject GetInstantiatedObject(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<GameObject>(Tilemap.GetInstantiatedObject_Injected(intPtr, ref position));
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003578 File Offset: 0x00001778
		[NativeMethod(Name = "GetTileObjectToInstantiate")]
		public GameObject GetObjectToInstantiate(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<GameObject>(Tilemap.GetObjectToInstantiate_Injected(intPtr, ref position));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000035A4 File Offset: 0x000017A4
		[NativeMethod(Name = "SetTileColliderType")]
		public void SetColliderType(Vector3Int position, Tile.ColliderType colliderType)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetColliderType_Injected(intPtr, ref position, colliderType);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000035CC File Offset: 0x000017CC
		[NativeMethod(Name = "GetTileColliderType")]
		public Tile.ColliderType GetColliderType(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetColliderType_Injected(intPtr, ref position);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000035F0 File Offset: 0x000017F0
		[NativeMethod(Name = "GetTileAnimationFrameCount")]
		public int GetAnimationFrameCount(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetAnimationFrameCount_Injected(intPtr, ref position);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003614 File Offset: 0x00001814
		[NativeMethod(Name = "GetTileAnimationFrame")]
		public int GetAnimationFrame(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetAnimationFrame_Injected(intPtr, ref position);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003638 File Offset: 0x00001838
		[NativeMethod(Name = "SetTileAnimationFrame")]
		public void SetAnimationFrame(Vector3Int position, int frame)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetAnimationFrame_Injected(intPtr, ref position, frame);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003660 File Offset: 0x00001860
		[NativeMethod(Name = "GetTileAnimationTime")]
		public float GetAnimationTime(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetAnimationTime_Injected(intPtr, ref position);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003684 File Offset: 0x00001884
		[NativeMethod(Name = "SetTileAnimationTime")]
		public void SetAnimationTime(Vector3Int position, float time)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetAnimationTime_Injected(intPtr, ref position, time);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000036AC File Offset: 0x000018AC
		public TileAnimationFlags GetTileAnimationFlags(Vector3Int position)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Tilemap.GetTileAnimationFlags_Injected(intPtr, ref position);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000036D0 File Offset: 0x000018D0
		public void SetTileAnimationFlags(Vector3Int position, TileAnimationFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SetTileAnimationFlags_Injected(intPtr, ref position, flags);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000036F8 File Offset: 0x000018F8
		public void AddTileAnimationFlags(Vector3Int position, TileAnimationFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.AddTileAnimationFlags_Injected(intPtr, ref position, flags);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003720 File Offset: 0x00001920
		public void RemoveTileAnimationFlags(Vector3Int position, TileAnimationFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.RemoveTileAnimationFlags_Injected(intPtr, ref position, flags);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003745 File Offset: 0x00001945
		public void FloodFill(Vector3Int position, TileBase tile)
		{
			this.FloodFillTileAsset(position, tile);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003754 File Offset: 0x00001954
		[NativeMethod(Name = "FloodFill")]
		private void FloodFillTileAsset(Vector3Int position, Object tile)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.FloodFillTileAsset_Injected(intPtr, ref position, Object.MarshalledUnityObject.Marshal<Object>(tile));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000377E File Offset: 0x0000197E
		public void BoxFill(Vector3Int position, TileBase tile, int startX, int startY, int endX, int endY)
		{
			this.BoxFillTileAsset(position, tile, startX, startY, endX, endY);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003794 File Offset: 0x00001994
		[NativeMethod(Name = "BoxFill")]
		private void BoxFillTileAsset(Vector3Int position, Object tile, int startX, int startY, int endX, int endY)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.BoxFillTileAsset_Injected(intPtr, ref position, Object.MarshalledUnityObject.Marshal<Object>(tile), startX, startY, endX, endY);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000037C5 File Offset: 0x000019C5
		public void InsertCells(Vector3Int position, Vector3Int insertCells)
		{
			this.InsertCells(position, insertCells.x, insertCells.y, insertCells.z);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000037E8 File Offset: 0x000019E8
		public void InsertCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.InsertCells_Injected(intPtr, ref position, numColumns, numRows, numLayers);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003810 File Offset: 0x00001A10
		public void DeleteCells(Vector3Int position, Vector3Int deleteCells)
		{
			this.DeleteCells(position, deleteCells.x, deleteCells.y, deleteCells.z);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003830 File Offset: 0x00001A30
		public void DeleteCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.DeleteCells_Injected(intPtr, ref position, numColumns, numRows, numLayers);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003858 File Offset: 0x00001A58
		public void ClearAllTiles()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.ClearAllTiles_Injected(intPtr);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000387C File Offset: 0x00001A7C
		public void ResizeBounds()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.ResizeBounds_Injected(intPtr);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000038A0 File Offset: 0x00001AA0
		[NativeMethod(Name = "CompressBounds")]
		private void CompressTilemapBounds(bool keepEditorPreview)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.CompressTilemapBounds_Injected(intPtr, keepEditorPreview);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000038C3 File Offset: 0x00001AC3
		public void CompressBounds()
		{
			this.CompressTilemapBounds(false);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000038CE File Offset: 0x00001ACE
		[RequiredByNativeCode]
		internal void GetLoopEndedForTileAnimationCallbackSettings(ref bool hasEndLoopForTileAnimationCallback)
		{
			hasEndLoopForTileAnimationCallback = Tilemap.HasLoopEndedForTileAnimationCallback();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000038D8 File Offset: 0x00001AD8
		[RequiredByNativeCode]
		private void DoLoopEndedForTileAnimationCallback(int count, IntPtr positionsIntPtr)
		{
			this.HandleLoopEndedForTileAnimationCallback(count, positionsIntPtr);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000038E4 File Offset: 0x00001AE4
		[RequiredByNativeCode]
		internal void GetSyncTileCallbackSettings(ref Tilemap.SyncTileCallbackSettings settings)
		{
			settings.hasSyncTileCallback = Tilemap.HasSyncTileCallback();
			settings.hasPositionsChangedCallback = Tilemap.HasPositionsChangedCallback();
			settings.isBufferSyncTile = this.bufferSyncTile;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000390C File Offset: 0x00001B0C
		internal void SendAndClearSyncTileBuffer()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Tilemap>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Tilemap.SendAndClearSyncTileBuffer_Injected(intPtr);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000392E File Offset: 0x00001B2E
		[RequiredByNativeCode]
		private void DoSyncTileCallback(Tilemap.SyncTile[] syncTiles)
		{
			this.HandleSyncTileCallback(syncTiles);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003939 File Offset: 0x00001B39
		[RequiredByNativeCode]
		private void DoPositionsChangedCallback(int count, IntPtr positionsIntPtr)
		{
			this.HandlePositionsChangedCallback(count, positionsIntPtr);
		}

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_layoutGrid_Injected(IntPtr _unity_self);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localFrameBounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_animationFrameRate_Injected(IntPtr _unity_self);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_animationFrameRate_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(IntPtr _unity_self, out Color ret);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_origin_Injected(IntPtr _unity_self, out Vector3Int ret);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_origin_Injected(IntPtr _unity_self, [In] ref Vector3Int value);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3Int ret);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3Int value);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_tileAnchor_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_tileAnchor_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Tilemap.Orientation get_orientation_Injected(IntPtr _unity_self);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orientation_Injected(IntPtr _unity_self, Tilemap.Orientation value);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_orientationMatrix_Injected(IntPtr _unity_self, out Matrix4x4 ret);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_orientationMatrix_Injected(IntPtr _unity_self, [In] ref Matrix4x4 value);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTileAsset_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] GetTileAssetsBlock_Injected(IntPtr _unity_self, [In] ref Vector3Int position, [In] ref Vector3Int blockDimensions);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTileAssetsBlockNonAlloc_Injected(IntPtr _unity_self, [In] ref Vector3Int startPosition, [In] ref Vector3Int endPosition, Object[] tiles);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTilesRangeCount_Injected(IntPtr _unity_self, [In] ref Vector3Int startPosition, [In] ref Vector3Int endPosition);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTileAssetsRangeNonAlloc_Injected(IntPtr _unity_self, [In] ref Vector3Int startPosition, [In] ref Vector3Int endPosition, ref ManagedSpanWrapper positions, Object[] tiles);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTileAsset_Injected(IntPtr _unity_self, [In] ref Vector3Int position, IntPtr tile);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTileAssets_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positionArray, Object[] tileArray);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTileAssetsBlock_Injected(IntPtr _unity_self, [In] ref Vector3Int position, [In] ref Vector3Int blockDimensions, Object[] tileArray);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTile_Injected(IntPtr _unity_self, [In] ref TileChangeData tileChangeData, bool ignoreLockFlags);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTiles_Injected(IntPtr _unity_self, TileChangeData[] tileChangeDataArray, bool ignoreLockFlags);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RefreshTile_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void RefreshTilesNative_Injected(IntPtr _unity_self, void* positions, int count);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RefreshAllTiles_Injected(IntPtr _unity_self);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SwapTileAsset_Injected(IntPtr _unity_self, IntPtr changeTile, IntPtr newTile);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContainsTileAsset_Injected(IntPtr _unity_self, IntPtr tileAsset);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetUsedTilesCount_Injected(IntPtr _unity_self);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetUsedSpritesCount_Injected(IntPtr _unity_self);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetUsedTilesNonAlloc_Injected(IntPtr _unity_self, Object[] usedTiles);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetUsedSpritesNonAlloc_Injected(IntPtr _unity_self, Object[] usedSprites);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSprite_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTransformMatrix_Injected(IntPtr _unity_self, [In] ref Vector3Int position, out Matrix4x4 ret);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransformMatrix_Injected(IntPtr _unity_self, [In] ref Vector3Int position, [In] ref Matrix4x4 transform);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColor_Injected(IntPtr _unity_self, [In] ref Vector3Int position, out Color ret);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColor_Injected(IntPtr _unity_self, [In] ref Vector3Int position, [In] ref Color color);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TileFlags GetTileFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTileFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position, TileFlags flags);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTileFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position, TileFlags flags);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveTileFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position, TileFlags flags);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInstantiatedObject_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetObjectToInstantiate_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColliderType_Injected(IntPtr _unity_self, [In] ref Vector3Int position, Tile.ColliderType colliderType);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Tile.ColliderType GetColliderType_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAnimationFrameCount_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAnimationFrame_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAnimationFrame_Injected(IntPtr _unity_self, [In] ref Vector3Int position, int frame);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAnimationTime_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAnimationTime_Injected(IntPtr _unity_self, [In] ref Vector3Int position, float time);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TileAnimationFlags GetTileAnimationFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTileAnimationFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position, TileAnimationFlags flags);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddTileAnimationFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position, TileAnimationFlags flags);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveTileAnimationFlags_Injected(IntPtr _unity_self, [In] ref Vector3Int position, TileAnimationFlags flags);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FloodFillTileAsset_Injected(IntPtr _unity_self, [In] ref Vector3Int position, IntPtr tile);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BoxFillTileAsset_Injected(IntPtr _unity_self, [In] ref Vector3Int position, IntPtr tile, int startX, int startY, int endX, int endY);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InsertCells_Injected(IntPtr _unity_self, [In] ref Vector3Int position, int numColumns, int numRows, int numLayers);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DeleteCells_Injected(IntPtr _unity_self, [In] ref Vector3Int position, int numColumns, int numRows, int numLayers);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearAllTiles_Injected(IntPtr _unity_self);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResizeBounds_Injected(IntPtr _unity_self);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CompressTilemapBounds_Injected(IntPtr _unity_self, bool keepEditorPreview);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendAndClearSyncTileBuffer_Injected(IntPtr _unity_self);

		// Token: 0x04000026 RID: 38
		private bool m_BufferSyncTile;

		// Token: 0x0200000C RID: 12
		public enum Orientation
		{
			// Token: 0x04000028 RID: 40
			XY,
			// Token: 0x04000029 RID: 41
			XZ,
			// Token: 0x0400002A RID: 42
			YX,
			// Token: 0x0400002B RID: 43
			YZ,
			// Token: 0x0400002C RID: 44
			ZX,
			// Token: 0x0400002D RID: 45
			ZY,
			// Token: 0x0400002E RID: 46
			Custom
		}

		// Token: 0x0200000D RID: 13
		[RequiredByNativeCode]
		public struct SyncTile
		{
			// Token: 0x1700001B RID: 27
			// (get) Token: 0x060000F2 RID: 242 RVA: 0x00003950 File Offset: 0x00001B50
			public Vector3Int position
			{
				get
				{
					return this.m_Position;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003968 File Offset: 0x00001B68
			public TileBase tile
			{
				get
				{
					return this.m_Tile;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003980 File Offset: 0x00001B80
			public TileData tileData
			{
				get
				{
					return this.m_TileData;
				}
			}

			// Token: 0x0400002F RID: 47
			internal Vector3Int m_Position;

			// Token: 0x04000030 RID: 48
			internal TileBase m_Tile;

			// Token: 0x04000031 RID: 49
			internal TileData m_TileData;
		}

		// Token: 0x0200000E RID: 14
		internal struct SyncTileCallbackSettings
		{
			// Token: 0x04000032 RID: 50
			internal bool hasSyncTileCallback;

			// Token: 0x04000033 RID: 51
			internal bool hasPositionsChangedCallback;

			// Token: 0x04000034 RID: 52
			internal bool isBufferSyncTile;
		}
	}
}
