using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006F RID: 111
	public struct Bone
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000389B6 File Offset: 0x00036BB6
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x000389BE File Offset: 0x00036BBE
		public uint parentBoneIndex
		{
			get
			{
				return this.m_ParentBoneIndex;
			}
			set
			{
				this.m_ParentBoneIndex = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x000389C7 File Offset: 0x00036BC7
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x000389CF File Offset: 0x00036BCF
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

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000389D8 File Offset: 0x00036BD8
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x000389E0 File Offset: 0x00036BE0
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

		// Token: 0x04000381 RID: 897
		public uint m_ParentBoneIndex;

		// Token: 0x04000382 RID: 898
		public Vector3 m_Position;

		// Token: 0x04000383 RID: 899
		public Quaternion m_Rotation;
	}
}
