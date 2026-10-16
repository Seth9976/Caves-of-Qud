using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001237 RID: 4663
	[Serializable]
	public class Tinkering_Layable : IPart, IContextRelationManager
	{
		// Token: 0x0600B967 RID: 47463 RVA: 0x0043B0B2 File Offset: 0x004392B2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetContextEvent>.ID || ID == RemoveFromContextEvent.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == TryRemoveFromContextEvent.ID;
		}

		// Token: 0x0600B968 RID: 47464 RVA: 0x0043B0E0 File Offset: 0x004392E0
		public override bool HandleEvent(GetContextEvent E)
		{
			if (GameObject.Validate(ref this.ComponentOf))
			{
				E.ObjectContext = this.ComponentOf;
				E.Relation = 6;
				E.RelationManager = this;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B969 RID: 47465 RVA: 0x0043B114 File Offset: 0x00439314
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			if (GameObject.Validate(ref this.ComponentOf))
			{
				Tinkering_Mine part = this.ComponentOf.GetPart<Tinkering_Mine>();
				if (part != null && part.Explosive == this.ParentObject)
				{
					part.SetExplosive(E.Replacement);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B96A RID: 47466 RVA: 0x0043B160 File Offset: 0x00439360
		public override bool HandleEvent(RemoveFromContextEvent E)
		{
			if (GameObject.Validate(ref this.ComponentOf))
			{
				Tinkering_Mine part = this.ComponentOf.GetPart<Tinkering_Mine>();
				if (part != null && part.Explosive == this.ParentObject)
				{
					part.SetExplosive(null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B96B RID: 47467 RVA: 0x0043B1A5 File Offset: 0x004393A5
		public override bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return !GameObject.Validate(ref this.ComponentOf) && base.HandleEvent(E);
		}

		// Token: 0x0600B96C RID: 47468 RVA: 0x0043B1C0 File Offset: 0x004393C0
		public bool RestoreContextRelation(GameObject Object, GameObject ObjectContext, Cell CellContext, BodyPart BodyPartContext, int Relation, bool Silent = true)
		{
			if (Relation == 6 && ObjectContext != null)
			{
				Tinkering_Mine part = ObjectContext.GetPart<Tinkering_Mine>();
				if (part != null)
				{
					if (part.Explosive != Object || this.ComponentOf != ObjectContext)
					{
						part.SetExplosive(Object);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004A87 RID: 19079
		public string DetonationMessage = "AfterThrown";

		// Token: 0x04004A88 RID: 19080
		public GameObject ComponentOf;
	}
}
