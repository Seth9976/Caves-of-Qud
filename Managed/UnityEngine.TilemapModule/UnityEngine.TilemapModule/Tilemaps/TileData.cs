using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000015 RID: 21
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileData
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003C40 File Offset: 0x00001E40
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00003C62 File Offset: 0x00001E62
		public Sprite sprite
		{
			get
			{
				return Object.ForceLoadFromInstanceID(this.m_Sprite) as Sprite;
			}
			set
			{
				this.m_Sprite = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003C80 File Offset: 0x00001E80
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00003C98 File Offset: 0x00001E98
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003CA4 File Offset: 0x00001EA4
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00003CBC File Offset: 0x00001EBC
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003CC8 File Offset: 0x00001EC8
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00003CEA File Offset: 0x00001EEA
		public GameObject gameObject
		{
			get
			{
				return Object.ForceLoadFromInstanceID(this.m_GameObject) as GameObject;
			}
			set
			{
				this.m_GameObject = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003D08 File Offset: 0x00001F08
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00003D20 File Offset: 0x00001F20
		public TileFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003D2C File Offset: 0x00001F2C
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00003D44 File Offset: 0x00001F44
		public Tile.ColliderType colliderType
		{
			get
			{
				return this.m_ColliderType;
			}
			set
			{
				this.m_ColliderType = value;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003D50 File Offset: 0x00001F50
		private static TileData CreateDefault()
		{
			return new TileData
			{
				color = Color.white,
				transform = Matrix4x4.identity,
				flags = TileFlags.None,
				colliderType = Tile.ColliderType.None
			};
		}

		// Token: 0x0400004F RID: 79
		private int m_Sprite;

		// Token: 0x04000050 RID: 80
		private Color m_Color;

		// Token: 0x04000051 RID: 81
		private Matrix4x4 m_Transform;

		// Token: 0x04000052 RID: 82
		private int m_GameObject;

		// Token: 0x04000053 RID: 83
		private TileFlags m_Flags;

		// Token: 0x04000054 RID: 84
		private Tile.ColliderType m_ColliderType;

		// Token: 0x04000055 RID: 85
		internal static readonly TileData Default = TileData.CreateDefault();
	}
}
