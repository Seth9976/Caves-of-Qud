using System;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000382 RID: 898
	[Serializable]
	public struct LoadSceneParameters
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		// (set) Token: 0x06002965 RID: 10597 RVA: 0x0004FCD0 File Offset: 0x0004DED0
		public LoadSceneMode loadSceneMode
		{
			get
			{
				return this.m_LoadSceneMode;
			}
			set
			{
				this.m_LoadSceneMode = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x0004FCDC File Offset: 0x0004DEDC
		// (set) Token: 0x06002967 RID: 10599 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
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

		// Token: 0x06002968 RID: 10600 RVA: 0x0004FCFE File Offset: 0x0004DEFE
		public LoadSceneParameters(LoadSceneMode mode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = LocalPhysicsMode.None;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x0004FD0F File Offset: 0x0004DF0F
		public LoadSceneParameters(LoadSceneMode mode, LocalPhysicsMode physicsMode)
		{
			this.m_LoadSceneMode = mode;
			this.m_LocalPhysicsMode = physicsMode;
		}

		// Token: 0x04000BB1 RID: 2993
		[SerializeField]
		private LoadSceneMode m_LoadSceneMode;

		// Token: 0x04000BB2 RID: 2994
		[SerializeField]
		private LocalPhysicsMode m_LocalPhysicsMode;
	}
}
