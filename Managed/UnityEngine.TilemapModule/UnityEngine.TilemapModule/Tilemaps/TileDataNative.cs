using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000016 RID: 22
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	internal struct TileDataNative
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003DA4 File Offset: 0x00001FA4
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00003DBC File Offset: 0x00001FBC
		public int sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				this.m_Sprite = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003DC8 File Offset: 0x00001FC8
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00003DE0 File Offset: 0x00001FE0
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003DEC File Offset: 0x00001FEC
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00003E04 File Offset: 0x00002004
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00003E10 File Offset: 0x00002010
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00003E28 File Offset: 0x00002028
		public int gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003E34 File Offset: 0x00002034
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00003E4C File Offset: 0x0000204C
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003E58 File Offset: 0x00002058
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00003E70 File Offset: 0x00002070
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

		// Token: 0x06000134 RID: 308 RVA: 0x00003E7C File Offset: 0x0000207C
		public static implicit operator TileDataNative(TileData td)
		{
			return new TileDataNative
			{
				sprite = ((td.sprite != null) ? td.sprite.GetInstanceID() : 0),
				color = td.color,
				transform = td.transform,
				gameObject = ((td.gameObject != null) ? td.gameObject.GetInstanceID() : 0),
				flags = td.flags,
				colliderType = td.colliderType
			};
		}

		// Token: 0x04000056 RID: 86
		private int m_Sprite;

		// Token: 0x04000057 RID: 87
		private Color m_Color;

		// Token: 0x04000058 RID: 88
		private Matrix4x4 m_Transform;

		// Token: 0x04000059 RID: 89
		private int m_GameObject;

		// Token: 0x0400005A RID: 90
		private TileFlags m_Flags;

		// Token: 0x0400005B RID: 91
		private Tile.ColliderType m_ColliderType;
	}
}
