using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000557 RID: 1367
	public struct ShaderTagId : IEquatable<ShaderTagId>
	{
		// Token: 0x060034BE RID: 13502 RVA: 0x0006786A File Offset: 0x00065A6A
		public ShaderTagId(string name)
		{
			this.m_Id = Shader.TagToID(name);
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x0006787C File Offset: 0x00065A7C
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x00067894 File Offset: 0x00065A94
		internal int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000678A0 File Offset: 0x00065AA0
		public string name
		{
			get
			{
				return Shader.IDToTag(this.id);
			}
		}

		// Token: 0x060034C2 RID: 13506 RVA: 0x000678C0 File Offset: 0x00065AC0
		public override bool Equals(object obj)
		{
			return obj is ShaderTagId && this.Equals((ShaderTagId)obj);
		}

		// Token: 0x060034C3 RID: 13507 RVA: 0x000678EC File Offset: 0x00065AEC
		public bool Equals(ShaderTagId other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x0006790C File Offset: 0x00065B0C
		public override int GetHashCode()
		{
			int num = 2079669542;
			return num * -1521134295 + this.m_Id.GetHashCode();
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x0006793C File Offset: 0x00065B3C
		public static bool operator ==(ShaderTagId tag1, ShaderTagId tag2)
		{
			return tag1.Equals(tag2);
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x00067958 File Offset: 0x00065B58
		public static bool operator !=(ShaderTagId tag1, ShaderTagId tag2)
		{
			return !(tag1 == tag2);
		}

		// Token: 0x060034C7 RID: 13511 RVA: 0x00067974 File Offset: 0x00065B74
		public static explicit operator ShaderTagId(string name)
		{
			return new ShaderTagId(name);
		}

		// Token: 0x060034C8 RID: 13512 RVA: 0x0006798C File Offset: 0x00065B8C
		public static explicit operator string(ShaderTagId tagId)
		{
			return tagId.name;
		}

		// Token: 0x040011AE RID: 4526
		public static readonly ShaderTagId none;

		// Token: 0x040011AF RID: 4527
		private int m_Id;
	}
}
