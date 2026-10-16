using System;

namespace UnityEngine.AI
{
	// Token: 0x02000015 RID: 21
	public struct NavMeshLinkInstance
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00003DF7 File Offset: 0x00001FF7
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00003DFF File Offset: 0x00001FFF
		internal int id { readonly get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003E08 File Offset: 0x00002008
		[Obsolete("valid has been deprecated. Use NavMesh.IsLinkValid() instead.")]
		public bool valid
		{
			get
			{
				return NavMesh.IsValidLinkHandle(this.id);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003E15 File Offset: 0x00002015
		[Obsolete("Remove() has been deprecated. Use NavMesh.RemoveLink() instead.")]
		public void Remove()
		{
			NavMesh.RemoveLinkInternal(this.id);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00003E24 File Offset: 0x00002024
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00003E34 File Offset: 0x00002034
		[Obsolete("owner has been deprecated. Use NavMesh.GetLinkOwner() and NavMesh.SetLinkOwner() instead.")]
		public Object owner
		{
			get
			{
				return NavMesh.InternalGetLinkOwner(this.id);
			}
			set
			{
				int num = ((value != null) ? value.GetInstanceID() : 0);
				bool flag = !NavMesh.InternalSetLinkOwner(this.id, num);
				if (flag)
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
				}
			}
		}
	}
}
