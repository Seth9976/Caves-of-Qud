using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000F8 RID: 248
	[ExcludeFromPreset]
	[NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/Graphics/SpriteUtility.h")]
	[NativeType("Runtime/Graphics/SpriteFrame.h")]
	public sealed class Sprite : Object
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00008C6F File Offset: 0x00006E6F
		[RequiredByNativeCode]
		private Sprite()
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00008C7C File Offset: 0x00006E7C
		internal int GetPackingMode()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPackingMode_Injected(intPtr);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00008CA0 File Offset: 0x00006EA0
		internal int GetPackingRotation()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPackingRotation_Injected(intPtr);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00008CC4 File Offset: 0x00006EC4
		internal int GetPacked()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPacked_Injected(intPtr);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00008CE8 File Offset: 0x00006EE8
		internal Rect GetTextureRect()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rect rect;
			Sprite.GetTextureRect_Injected(intPtr, out rect);
			return rect;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00008D10 File Offset: 0x00006F10
		internal Vector2 GetTextureRectOffset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Sprite.GetTextureRectOffset_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00008D38 File Offset: 0x00006F38
		internal Vector4 GetInnerUVs()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Sprite.GetInnerUVs_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00008D60 File Offset: 0x00006F60
		internal Vector4 GetOuterUVs()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Sprite.GetOuterUVs_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00008D88 File Offset: 0x00006F88
		internal Vector4 GetPadding()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Sprite.GetPadding_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00008DB0 File Offset: 0x00006FB0
		[FreeFunction("SpritesBindings::CreateSpriteWithoutTextureScripting")]
		internal static Sprite CreateSpriteWithoutTextureScripting(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Unmarshal.UnmarshalUnityObject<Sprite>(Sprite.CreateSpriteWithoutTextureScripting_Injected(ref rect, ref pivot, pixelsToUnits, Object.MarshalledUnityObject.Marshal<Texture2D>(texture)));
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00008DD4 File Offset: 0x00006FD4
		[FreeFunction("SpritesBindings::CreateSprite", ThrowsException = true)]
		internal static Sprite CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape, [Unmarshalled] SecondarySpriteTexture[] secondaryTexture)
		{
			return Unmarshal.UnmarshalUnityObject<Sprite>(Sprite.CreateSprite_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture), ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape, secondaryTexture));
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00008E00 File Offset: 0x00007000
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Sprite.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00008E28 File Offset: 0x00007028
		public Rect rect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Sprite.get_rect_Injected(intPtr, out rect);
				return rect;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00008E50 File Offset: 0x00007050
		public Vector4 border
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				Sprite.get_border_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00008E78 File Offset: 0x00007078
		public Texture2D texture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Sprite.get_texture_Injected(intPtr));
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00008EA0 File Offset: 0x000070A0
		internal uint extrude
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_extrude_Injected(intPtr);
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00008EC4 File Offset: 0x000070C4
		internal Texture2D GetSecondaryTexture(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(Sprite.GetSecondaryTexture_Injected(intPtr, index));
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00008EEC File Offset: 0x000070EC
		public int GetSecondaryTextureCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetSecondaryTextureCount_Injected(intPtr);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00008F10 File Offset: 0x00007110
		[FreeFunction("SpritesBindings::GetSecondaryTextures", ThrowsException = true, HasExplicitThis = true)]
		public int GetSecondaryTextures([NotNull] [Unmarshalled] SecondarySpriteTexture[] secondaryTexture)
		{
			if (secondaryTexture == null)
			{
				ThrowHelper.ThrowArgumentNullException(secondaryTexture, "secondaryTexture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetSecondaryTextures_Injected(intPtr, secondaryTexture);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00008F44 File Offset: 0x00007144
		public float pixelsPerUnit
		{
			[NativeMethod("GetPixelsToUnits")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_pixelsPerUnit_Injected(intPtr);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00008F68 File Offset: 0x00007168
		public float spriteAtlasTextureScale
		{
			[NativeMethod("GetSpriteAtlasTextureScale")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_spriteAtlasTextureScale_Injected(intPtr);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00008F8C File Offset: 0x0000718C
		public Texture2D associatedAlphaSplitTexture
		{
			[NativeMethod("GetAlphaTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Sprite.get_associatedAlphaSplitTexture_Injected(intPtr));
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00008FB4 File Offset: 0x000071B4
		public Vector2 pivot
		{
			[NativeMethod("GetPivotInPixels")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Sprite.get_pivot_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x00008FDC File Offset: 0x000071DC
		public bool packed
		{
			get
			{
				return this.GetPacked() == 1;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00008FF8 File Offset: 0x000071F8
		public SpritePackingMode packingMode
		{
			get
			{
				return (SpritePackingMode)this.GetPackingMode();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00009010 File Offset: 0x00007210
		public SpritePackingRotation packingRotation
		{
			get
			{
				return (SpritePackingRotation)this.GetPackingRotation();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00009028 File Offset: 0x00007228
		public Rect textureRect
		{
			get
			{
				return this.GetTextureRect();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00009040 File Offset: 0x00007240
		public Vector2 textureRectOffset
		{
			get
			{
				return this.GetTextureRectOffset();
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00009058 File Offset: 0x00007258
		public Vector2[] vertices
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteVertices", HasExplicitThis = true)]
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_vertices_Injected(intPtr);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000907C File Offset: 0x0000727C
		public ushort[] triangles
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteIndices", HasExplicitThis = true)]
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_triangles_Injected(intPtr);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x000090A0 File Offset: 0x000072A0
		public Vector2[] uv
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteUVs", HasExplicitThis = true)]
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_uv_Injected(intPtr);
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000090C4 File Offset: 0x000072C4
		public int GetPhysicsShapeCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPhysicsShapeCount_Injected(intPtr);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000090E8 File Offset: 0x000072E8
		public uint GetScriptableObjectsCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetScriptableObjectsCount_Injected(intPtr);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000910C File Offset: 0x0000730C
		[FreeFunction("SpritesBindings::GetScriptableObjects", ThrowsException = true, HasExplicitThis = true)]
		public uint GetScriptableObjects([NotNull] [Unmarshalled] ScriptableObject[] scriptableObjects)
		{
			if (scriptableObjects == null)
			{
				ThrowHelper.ThrowArgumentNullException(scriptableObjects, "scriptableObjects");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetScriptableObjects_Injected(intPtr, scriptableObjects);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00009140 File Offset: 0x00007340
		public bool AddScriptableObject([NotNull] ScriptableObject obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ScriptableObject>(obj);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Sprite.AddScriptableObject_Injected(intPtr, intPtr2);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00009188 File Offset: 0x00007388
		public bool RemoveScriptableObjectAt(uint i)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.RemoveScriptableObjectAt_Injected(intPtr, i);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000091AC File Offset: 0x000073AC
		public bool SetScriptableObjectAt([NotNull] ScriptableObject obj, uint i)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ScriptableObject>(obj);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Sprite.SetScriptableObjectAt_Injected(intPtr, intPtr2, i);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000091F4 File Offset: 0x000073F4
		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			return this.Internal_GetPhysicsShapePointCount(shapeIdx);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00009244 File Offset: 0x00007444
		[NativeMethod("GetPhysicsShapePointCount")]
		private int Internal_GetPhysicsShapePointCount(int shapeIdx)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.Internal_GetPhysicsShapePointCount_Injected(intPtr, shapeIdx);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00009268 File Offset: 0x00007468
		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			Sprite.GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
			return physicsShape.Count;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000092C0 File Offset: 0x000074C0
		[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
		private unsafe static void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, [NotNull] List<Vector2> physicsShape)
		{
			if (physicsShape == null)
			{
				ThrowHelper.ThrowArgumentNullException(physicsShape, "physicsShape");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Sprite>(sprite);
				fixed (Vector2[] array = NoAllocHelpers.ExtractArrayFromList<Vector2>(physicsShape))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, physicsShape.Count);
					Sprite.GetPhysicsShapeImpl_Injected(intPtr, shapeIdx, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(physicsShape);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00009334 File Offset: 0x00007534
		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			bool flag = physicsShapes == null;
			if (flag)
			{
				throw new ArgumentNullException("physicsShapes");
			}
			for (int i = 0; i < physicsShapes.Count; i++)
			{
				Vector2[] array = physicsShapes[i];
				bool flag2 = array == null;
				if (flag2)
				{
					throw new ArgumentNullException("physicsShape", string.Format("Physics Shape at {0} is null.", i));
				}
				bool flag3 = array.Length < 3;
				if (flag3)
				{
					throw new ArgumentException(string.Format("Physics Shape at {0} has less than 3 vertices ({1}).", i, array.Length));
				}
			}
			Sprite.OverridePhysicsShapeCount(this, physicsShapes.Count);
			for (int j = 0; j < physicsShapes.Count; j++)
			{
				Sprite.OverridePhysicsShape(this, physicsShapes[j], j);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00009400 File Offset: 0x00007600
		[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
		private static void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount)
		{
			Sprite.OverridePhysicsShapeCount_Injected(Object.MarshalledUnityObject.Marshal<Sprite>(sprite), physicsShapeCount);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000941C File Offset: 0x0000761C
		[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
		private unsafe static void OverridePhysicsShape(Sprite sprite, [NotNull] Vector2[] physicsShape, int idx)
		{
			if (physicsShape == null)
			{
				ThrowHelper.ThrowArgumentNullException(physicsShape, "physicsShape");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Sprite>(sprite);
			Span<Vector2> span = new Span<Vector2>(physicsShape);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Sprite.OverridePhysicsShape_Injected(intPtr, ref managedSpanWrapper, idx);
			}
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000946C File Offset: 0x0000766C
		[FreeFunction("SpritesBindings::OverrideGeometry", HasExplicitThis = true)]
		public unsafe void OverrideGeometry([NotNull] Vector2[] vertices, [NotNull] ushort[] triangles)
		{
			if (vertices == null)
			{
				ThrowHelper.ThrowArgumentNullException(vertices, "vertices");
			}
			if (triangles == null)
			{
				ThrowHelper.ThrowArgumentNullException(triangles, "triangles");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(vertices);
			fixed (Vector2* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<ushort> span2 = new Span<ushort>(triangles);
				fixed (ushort* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					Sprite.OverrideGeometry_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000094FC File Offset: 0x000076FC
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, texture);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00009518 File Offset: 0x00007718
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, null);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00009534 File Offset: 0x00007734
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape, null);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00009558 File Offset: 0x00007758
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape, SecondarySpriteTexture[] secondaryTextures)
		{
			bool flag = texture == null;
			Sprite sprite;
			if (flag)
			{
				sprite = null;
			}
			else
			{
				bool flag2 = rect.xMax > (float)texture.width || rect.yMax > (float)texture.height;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Could not create sprite ({0}, {1}, {2}, {3}) from a {4}x{5} texture.", new object[] { rect.x, rect.y, rect.width, rect.height, texture.width, texture.height }));
				}
				bool flag3 = pixelsPerUnit <= 0f;
				if (flag3)
				{
					throw new ArgumentException("pixelsPerUnit must be set to a positive non-zero value.");
				}
				bool flag4 = secondaryTextures != null;
				if (flag4)
				{
					foreach (SecondarySpriteTexture secondarySpriteTexture in secondaryTextures)
					{
						bool flag5 = secondarySpriteTexture.texture == texture;
						if (flag5)
						{
							throw new ArgumentException(string.Format("{0} is using source Texture as Secondary Texture.", secondarySpriteTexture.name));
						}
					}
				}
				sprite = Sprite.CreateSprite(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape, secondaryTextures);
			}
			return sprite;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000969C File Offset: 0x0000789C
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, false);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000096C0 File Offset: 0x000078C0
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, Vector4.zero);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000096E4 File Offset: 0x000078E4
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, SpriteMeshType.Tight);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00009704 File Offset: 0x00007904
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0U);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00009720 File Offset: 0x00007920
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture, rect, pivot, 100f);
		}

		// Token: 0x060004F2 RID: 1266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPackingMode_Injected(IntPtr _unity_self);

		// Token: 0x060004F3 RID: 1267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPackingRotation_Injected(IntPtr _unity_self);

		// Token: 0x060004F4 RID: 1268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPacked_Injected(IntPtr _unity_self);

		// Token: 0x060004F5 RID: 1269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureRect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060004F6 RID: 1270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureRectOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060004F7 RID: 1271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInnerUVs_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060004F8 RID: 1272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOuterUVs_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060004F9 RID: 1273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPadding_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060004FA RID: 1274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateSpriteWithoutTextureScripting_Injected([In] ref Rect rect, [In] ref Vector2 pivot, float pixelsToUnits, IntPtr texture);

		// Token: 0x060004FB RID: 1275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateSprite_Injected(IntPtr texture, [In] ref Rect rect, [In] ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, [In] ref Vector4 border, bool generateFallbackPhysicsShape, SecondarySpriteTexture[] secondaryTexture);

		// Token: 0x060004FC RID: 1276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060004FD RID: 1277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060004FE RID: 1278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_border_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060004FF RID: 1279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_texture_Injected(IntPtr _unity_self);

		// Token: 0x06000500 RID: 1280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_extrude_Injected(IntPtr _unity_self);

		// Token: 0x06000501 RID: 1281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSecondaryTexture_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000502 RID: 1282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSecondaryTextureCount_Injected(IntPtr _unity_self);

		// Token: 0x06000503 RID: 1283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSecondaryTextures_Injected(IntPtr _unity_self, SecondarySpriteTexture[] secondaryTexture);

		// Token: 0x06000504 RID: 1284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pixelsPerUnit_Injected(IntPtr _unity_self);

		// Token: 0x06000505 RID: 1285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spriteAtlasTextureScale_Injected(IntPtr _unity_self);

		// Token: 0x06000506 RID: 1286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_associatedAlphaSplitTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000507 RID: 1287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivot_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x06000508 RID: 1288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector2[] get_vertices_Injected(IntPtr _unity_self);

		// Token: 0x06000509 RID: 1289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort[] get_triangles_Injected(IntPtr _unity_self);

		// Token: 0x0600050A RID: 1290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector2[] get_uv_Injected(IntPtr _unity_self);

		// Token: 0x0600050B RID: 1291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPhysicsShapeCount_Injected(IntPtr _unity_self);

		// Token: 0x0600050C RID: 1292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetScriptableObjectsCount_Injected(IntPtr _unity_self);

		// Token: 0x0600050D RID: 1293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetScriptableObjects_Injected(IntPtr _unity_self, ScriptableObject[] scriptableObjects);

		// Token: 0x0600050E RID: 1294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddScriptableObject_Injected(IntPtr _unity_self, IntPtr obj);

		// Token: 0x0600050F RID: 1295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveScriptableObjectAt_Injected(IntPtr _unity_self, uint i);

		// Token: 0x06000510 RID: 1296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetScriptableObjectAt_Injected(IntPtr _unity_self, IntPtr obj, uint i);

		// Token: 0x06000511 RID: 1297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPhysicsShapePointCount_Injected(IntPtr _unity_self, int shapeIdx);

		// Token: 0x06000512 RID: 1298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsShapeImpl_Injected(IntPtr sprite, int shapeIdx, ref BlittableListWrapper physicsShape);

		// Token: 0x06000513 RID: 1299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShapeCount_Injected(IntPtr sprite, int physicsShapeCount);

		// Token: 0x06000514 RID: 1300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShape_Injected(IntPtr sprite, ref ManagedSpanWrapper physicsShape, int idx);

		// Token: 0x06000515 RID: 1301
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverrideGeometry_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vertices, ref ManagedSpanWrapper triangles);
	}
}
