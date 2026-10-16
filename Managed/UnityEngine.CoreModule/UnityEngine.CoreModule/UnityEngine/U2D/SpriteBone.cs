using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x020005B5 RID: 1461
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/SpriteDataMarshalling.h")]
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "ScriptingSpriteBone")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[Serializable]
	public struct SpriteBone
	{
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060037DF RID: 14303 RVA: 0x0006DC30 File Offset: 0x0006BE30
		// (set) Token: 0x060037E0 RID: 14304 RVA: 0x0006DC48 File Offset: 0x0006BE48
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060037E1 RID: 14305 RVA: 0x0006DC54 File Offset: 0x0006BE54
		// (set) Token: 0x060037E2 RID: 14306 RVA: 0x0006DC6C File Offset: 0x0006BE6C
		public string guid
		{
			get
			{
				return this.m_Guid;
			}
			set
			{
				this.m_Guid = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060037E3 RID: 14307 RVA: 0x0006DC78 File Offset: 0x0006BE78
		// (set) Token: 0x060037E4 RID: 14308 RVA: 0x0006DC90 File Offset: 0x0006BE90
		public Vector3 position
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

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060037E5 RID: 14309 RVA: 0x0006DC9C File Offset: 0x0006BE9C
		// (set) Token: 0x060037E6 RID: 14310 RVA: 0x0006DCB4 File Offset: 0x0006BEB4
		public Quaternion rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060037E7 RID: 14311 RVA: 0x0006DCC0 File Offset: 0x0006BEC0
		// (set) Token: 0x060037E8 RID: 14312 RVA: 0x0006DCD8 File Offset: 0x0006BED8
		public float length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060037E9 RID: 14313 RVA: 0x0006DCE4 File Offset: 0x0006BEE4
		// (set) Token: 0x060037EA RID: 14314 RVA: 0x0006DCFC File Offset: 0x0006BEFC
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060037EB RID: 14315 RVA: 0x0006DD08 File Offset: 0x0006BF08
		// (set) Token: 0x060037EC RID: 14316 RVA: 0x0006DD20 File Offset: 0x0006BF20
		public Color32 color
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

		// Token: 0x04001408 RID: 5128
		[NativeName("name")]
		[SerializeField]
		private string m_Name;

		// Token: 0x04001409 RID: 5129
		[SerializeField]
		[NativeName("guid")]
		private string m_Guid;

		// Token: 0x0400140A RID: 5130
		[SerializeField]
		[NativeName("position")]
		private Vector3 m_Position;

		// Token: 0x0400140B RID: 5131
		[NativeName("rotation")]
		[SerializeField]
		private Quaternion m_Rotation;

		// Token: 0x0400140C RID: 5132
		[NativeName("length")]
		[SerializeField]
		private float m_Length;

		// Token: 0x0400140D RID: 5133
		[NativeName("parentId")]
		[SerializeField]
		private int m_ParentId;

		// Token: 0x0400140E RID: 5134
		[SerializeField]
		[NativeName("color")]
		private Color32 m_Color;
	}
}
