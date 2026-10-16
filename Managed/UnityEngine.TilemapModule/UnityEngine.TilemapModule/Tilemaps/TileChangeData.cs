using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000017 RID: 23
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	[Serializable]
	public struct TileChangeData
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00003F20 File Offset: 0x00002120
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00003F38 File Offset: 0x00002138
		public Vector3Int position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00003F44 File Offset: 0x00002144
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00003F61 File Offset: 0x00002161
		public TileBase tile
		{
			get
			{
				return (TileBase)this.m_TileAsset;
			}
			set
			{
				this.m_TileAsset = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003F6C File Offset: 0x0000216C
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003F84 File Offset: 0x00002184
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003F90 File Offset: 0x00002190
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00003FA8 File Offset: 0x000021A8
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

		// Token: 0x0600013D RID: 317 RVA: 0x00003FB2 File Offset: 0x000021B2
		public TileChangeData(Vector3Int position, TileBase tile, Color color, Matrix4x4 transform)
		{
			this.m_Position = position;
			this.m_TileAsset = tile;
			this.m_Color = color;
			this.m_Transform = transform;
		}

		// Token: 0x0400005C RID: 92
		[SerializeField]
		private Vector3Int m_Position;

		// Token: 0x0400005D RID: 93
		[SerializeField]
		private Object m_TileAsset;

		// Token: 0x0400005E RID: 94
		[SerializeField]
		private Color m_Color;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		private Matrix4x4 m_Transform;
	}
}
