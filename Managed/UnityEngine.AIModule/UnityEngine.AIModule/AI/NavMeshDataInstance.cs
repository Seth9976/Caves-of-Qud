using System;

namespace UnityEngine.AI
{
	// Token: 0x02000013 RID: 19
	public struct NavMeshDataInstance
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003CF1 File Offset: 0x00001EF1
		public bool valid
		{
			get
			{
				return this.id != 0 && NavMesh.IsValidNavMeshDataHandle(this.id);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00003D09 File Offset: 0x00001F09
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00003D11 File Offset: 0x00001F11
		internal int id { readonly get; set; }

		// Token: 0x0600016A RID: 362 RVA: 0x00003D1A File Offset: 0x00001F1A
		public void Remove()
		{
			NavMesh.RemoveNavMeshDataInternal(this.id);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003D29 File Offset: 0x00001F29
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00003D38 File Offset: 0x00001F38
		public Object owner
		{
			get
			{
				return NavMesh.InternalGetOwner(this.id);
			}
			set
			{
				int num = ((value != null) ? value.GetInstanceID() : 0);
				bool flag = !NavMesh.InternalSetOwner(this.id, num);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshDataInstance");
				}
			}
		}
	}
}
