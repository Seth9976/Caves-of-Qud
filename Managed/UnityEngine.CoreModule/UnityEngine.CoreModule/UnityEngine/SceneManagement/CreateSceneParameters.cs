using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000383 RID: 899
	[Serializable]
	public struct CreateSceneParameters
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x0004FD20 File Offset: 0x0004DF20
		// (set) Token: 0x0600296B RID: 10603 RVA: 0x0004FD38 File Offset: 0x0004DF38
		public LocalPhysicsMode localPhysicsMode
		{
			get
			{
				return this.m_LocalPhysicsMode;
			}
			set
			{
				this.m_LocalPhysicsMode = value;
			}
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0004FD38 File Offset: 0x0004DF38
		public CreateSceneParameters(LocalPhysicsMode physicsMode)
		{
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x04000BB3 RID: 2995
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
