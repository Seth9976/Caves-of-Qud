using System;
using XRL.World.Anatomy;

namespace XRL.World
{
	// Token: 0x02000739 RID: 1849
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetContextEvent : PooledEvent<GetContextEvent>
	{
		// Token: 0x06004C8A RID: 19594 RVA: 0x0017A12D File Offset: 0x0017832D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x0017A136 File Offset: 0x00178336
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.ObjectContext = null;
			this.CellContext = null;
			this.BodyPartContext = null;
			this.Relation = 0;
			this.RelationManager = null;
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0017A168 File Offset: 0x00178368
		public static void Get(GameObject Object, out GameObject ObjectContext, out Cell CellContext, out BodyPart BodyPartContext, out int Relation, out IContextRelationManager RelationManager)
		{
			if (GameObject.Validate(ref Object))
			{
				GetContextEvent getContextEvent = PooledEvent<GetContextEvent>.FromPool();
				getContextEvent.Object = Object;
				Object.HandleEvent(getContextEvent);
				ObjectContext = getContextEvent.ObjectContext;
				CellContext = getContextEvent.CellContext;
				BodyPartContext = getContextEvent.BodyPartContext;
				Relation = getContextEvent.Relation;
				RelationManager = getContextEvent.RelationManager;
				PooledEvent<GetContextEvent>.ResetTo(ref getContextEvent);
				return;
			}
			ObjectContext = null;
			CellContext = null;
			BodyPartContext = null;
			Relation = 0;
			RelationManager = null;
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0017A1D8 File Offset: 0x001783D8
		public static void Get(GameObject Object, out GameObject ObjectContext, out Cell CellContext, out int Relation, out IContextRelationManager RelationManager)
		{
			BodyPart bodyPart;
			GetContextEvent.Get(Object, out ObjectContext, out CellContext, out bodyPart, out Relation, out RelationManager);
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x0017A1F4 File Offset: 0x001783F4
		public static void Get(GameObject Object, out GameObject ObjectContext, out Cell CellContext, out int Relation)
		{
			IContextRelationManager contextRelationManager;
			GetContextEvent.Get(Object, out ObjectContext, out CellContext, out Relation, out contextRelationManager);
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x0017A20C File Offset: 0x0017840C
		public static void Get(GameObject Object, out GameObject ObjectContext, out Cell CellContext)
		{
			int num;
			GetContextEvent.Get(Object, out ObjectContext, out CellContext, out num);
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0017A224 File Offset: 0x00178424
		public static bool HasAny(GameObject Object)
		{
			GameObject gameObject;
			Cell cell;
			GetContextEvent.Get(Object, out gameObject, out cell);
			return gameObject != null || cell != null;
		}

		// Token: 0x0400250D RID: 9485
		public GameObject Object;

		// Token: 0x0400250E RID: 9486
		public GameObject ObjectContext;

		// Token: 0x0400250F RID: 9487
		public Cell CellContext;

		// Token: 0x04002510 RID: 9488
		public BodyPart BodyPartContext;

		// Token: 0x04002511 RID: 9489
		public int Relation;

		// Token: 0x04002512 RID: 9490
		public IContextRelationManager RelationManager;
	}
}
