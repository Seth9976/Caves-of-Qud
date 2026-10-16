using System;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000037 RID: 55
	[ExcludeFromObjectFactory]
	[Serializable]
	public abstract class TextAsset : ScriptableObject
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000B23C File Offset: 0x0000943C
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000B254 File Offset: 0x00009454
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000B260 File Offset: 0x00009460
		public int instanceID
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				if (flag)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000B294 File Offset: 0x00009494
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000B2CA File Offset: 0x000094CA
		public int hashCode
		{
			get
			{
				bool flag = this.m_HashCode == 0;
				if (flag)
				{
					this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
				}
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000B2D3 File Offset: 0x000094D3
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000B2DB File Offset: 0x000094DB
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B2E4 File Offset: 0x000094E4
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000B335 File Offset: 0x00009535
		public int materialHashCode
		{
			get
			{
				bool flag = this.m_MaterialHashCode == 0;
				if (flag)
				{
					bool flag2 = this.m_Material == null;
					if (flag2)
					{
						return 0;
					}
					this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
				}
				return this.m_MaterialHashCode;
			}
			set
			{
				this.m_MaterialHashCode = value;
			}
		}

		// Token: 0x04000150 RID: 336
		[SerializeField]
		internal string m_Version;

		// Token: 0x04000151 RID: 337
		internal int m_InstanceID;

		// Token: 0x04000152 RID: 338
		internal int m_HashCode;

		// Token: 0x04000153 RID: 339
		[SerializeField]
		[FormerlySerializedAs("material")]
		internal Material m_Material;

		// Token: 0x04000154 RID: 340
		internal int m_MaterialHashCode;
	}
}
