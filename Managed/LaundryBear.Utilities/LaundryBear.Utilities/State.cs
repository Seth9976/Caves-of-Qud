using System;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x0200000D RID: 13
	public abstract class State<TStateEnum, TTransitionEnum, TOwner> : ScriptableObject where TOwner : global::UnityEngine.Object
	{
		// Token: 0x06000043 RID: 67
		public abstract TStateEnum GetEnum();

		// Token: 0x06000044 RID: 68 RVA: 0x000029D4 File Offset: 0x00000BD4
		public virtual void UpdateState(TOwner owner, object userData)
		{
		}

		// Token: 0x06000045 RID: 69
		public abstract bool CanTransition(TTransitionEnum transition);

		// Token: 0x06000046 RID: 70
		public abstract TStateEnum HandleTransition(TTransitionEnum transition);

		// Token: 0x06000047 RID: 71 RVA: 0x000029D6 File Offset: 0x00000BD6
		public virtual void OnEnterState(TStateEnum previousState, TOwner owner, object userData)
		{
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029D8 File Offset: 0x00000BD8
		public virtual void OnExitState(TOwner owner, object userData)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029DA File Offset: 0x00000BDA
		public virtual void FixedUpdateState(TOwner owner, object userData)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029DC File Offset: 0x00000BDC
		public virtual void OnOwnerAnimatorMove(TOwner owner, object userData)
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029DE File Offset: 0x00000BDE
		public virtual void OnOwnerAnimatorIK(TOwner owner, object userData)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029E0 File Offset: 0x00000BE0
		public virtual void LateUpdateState(TOwner owner, object userData)
		{
		}
	}
}
