using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x020006C2 RID: 1730
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Singleton)]
	public class CanEnterInteriorEvent : SingletonEvent<CanEnterInteriorEvent>
	{
		// Token: 0x0600494F RID: 18767 RVA: 0x001714E7 File Offset: 0x0016F6E7
		public override int GetCascadeLevel()
		{
			return CanEnterInteriorEvent.CascadeLevel;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x001714EE File Offset: 0x0016F6EE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x001714F7 File Offset: 0x0016F6F7
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Interior = null;
			this.Status = 0;
			this.Action = false;
			this.ShowMessage = false;
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x0017152C File Offset: 0x0016F72C
		public static bool Check(GameObject Actor, GameObject Object, Interior Interior, ref int Status, ref bool Action, ref bool ShowMessage)
		{
			SingletonEvent<CanEnterInteriorEvent>.Instance.Actor = Actor;
			SingletonEvent<CanEnterInteriorEvent>.Instance.Object = Object;
			SingletonEvent<CanEnterInteriorEvent>.Instance.Interior = Interior;
			SingletonEvent<CanEnterInteriorEvent>.Instance.Status = Status;
			SingletonEvent<CanEnterInteriorEvent>.Instance.Action = Action;
			SingletonEvent<CanEnterInteriorEvent>.Instance.ShowMessage = ShowMessage;
			Actor.HandleEvent(SingletonEvent<CanEnterInteriorEvent>.Instance);
			Object.HandleEvent(SingletonEvent<CanEnterInteriorEvent>.Instance);
			Status = SingletonEvent<CanEnterInteriorEvent>.Instance.Status;
			Action = SingletonEvent<CanEnterInteriorEvent>.Instance.Action;
			ShowMessage = SingletonEvent<CanEnterInteriorEvent>.Instance.ShowMessage;
			SingletonEvent<CanEnterInteriorEvent>.Instance.Reset();
			return Status == 0;
		}

		// Token: 0x04002333 RID: 9011
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002334 RID: 9012
		public GameObject Actor;

		// Token: 0x04002335 RID: 9013
		public GameObject Object;

		// Token: 0x04002336 RID: 9014
		public Interior Interior;

		// Token: 0x04002337 RID: 9015
		public int Status;

		// Token: 0x04002338 RID: 9016
		public bool Action;

		// Token: 0x04002339 RID: 9017
		public bool ShowMessage;
	}
}
