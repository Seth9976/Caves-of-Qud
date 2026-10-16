using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013C3 RID: 5059
	public class BaseDefaultEquipmentMutation : BaseMutation
	{
		// Token: 0x0600C32F RID: 49967 RVA: 0x00474465 File Offset: 0x00472665
		public bool HasRegisteredSlot(string id)
		{
			return this.RegisteredSlots != null && this.RegisteredSlots.ContainsKey(id);
		}

		// Token: 0x0600C330 RID: 49968 RVA: 0x0047447D File Offset: 0x0047267D
		public int RegisterSlot(string id, BodyPart part)
		{
			if (this.RegisteredSlots == null)
			{
				this.RegisteredSlots = new Dictionary<string, int>();
			}
			this.RegisteredSlots[id] = part.ID;
			return part.ID;
		}

		// Token: 0x0600C331 RID: 49969 RVA: 0x004744AC File Offset: 0x004726AC
		public BodyPart GetRegisteredSlot(string id, bool evenIfDismembered)
		{
			if (id == null)
			{
				return null;
			}
			if (this.ParentObject == null)
			{
				return null;
			}
			if (this.RegisteredSlots == null)
			{
				return null;
			}
			int num = 0;
			this.RegisteredSlots.TryGetValue(id, out num);
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetPartByID(num, evenIfDismembered);
		}

		// Token: 0x0600C332 RID: 49970 RVA: 0x004744FC File Offset: 0x004726FC
		public BodyPart GetRegisteredSlot(Body Body, string Type)
		{
			int num;
			if (this.RegisteredSlots != null && this.RegisteredSlots.TryGetValue(Type, out num))
			{
				return Body._Body.GetPartByID(num);
			}
			return null;
		}

		// Token: 0x0600C333 RID: 49971 RVA: 0x00474530 File Offset: 0x00472730
		public BodyPart RequireRegisteredSlot(Body Body, string Type)
		{
			BodyPart firstPart;
			if (this.TryGetRegisteredSlot(Body, Type, out firstPart, false))
			{
				return firstPart;
			}
			firstPart = Body.GetFirstPart(Type);
			if (firstPart == null)
			{
				return null;
			}
			if (this.RegisteredSlots == null)
			{
				this.RegisteredSlots = new Dictionary<string, int>();
			}
			this.RegisteredSlots[Type] = firstPart.ID;
			return firstPart;
		}

		/// <returns>
		/// True if registration exists and is valid, false if new registration should be made.
		/// Can still output null for a true case if dismembered.
		/// </returns>
		// Token: 0x0600C334 RID: 49972 RVA: 0x00474580 File Offset: 0x00472780
		public bool TryGetRegisteredSlot(Body Body, string Type, out BodyPart Part, bool EvenIfDismembered = false)
		{
			int num;
			if (this.RegisteredSlots == null || !this.RegisteredSlots.TryGetValue(Type, out num))
			{
				Part = null;
				return false;
			}
			Part = Body._Body.GetPartByID(num);
			if (Part != null)
			{
				return true;
			}
			Part = Body.GetDismemberedPartByID(num);
			if (Part == null)
			{
				this.RegisteredSlots.Remove(Type);
				return false;
			}
			if (!EvenIfDismembered)
			{
				Part = null;
			}
			return true;
		}

		// Token: 0x0600C335 RID: 49973 RVA: 0x004745E2 File Offset: 0x004727E2
		public override bool ChangeLevel(int NewLevel)
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				Body body = parentObject.Body;
				if (body != null)
				{
					body.UpdateBodyParts(0);
				}
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C336 RID: 49974 RVA: 0x00474608 File Offset: 0x00472808
		public override void AfterMutate()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				Body body = parentObject.Body;
				if (body != null)
				{
					body.UpdateBodyParts(0);
				}
			}
			base.AfterMutate();
		}

		// Token: 0x0600C337 RID: 49975 RVA: 0x0047462D File Offset: 0x0047282D
		public override void AfterUnmutate(GameObject GO)
		{
			if (GO != null)
			{
				Body body = GO.Body;
				if (body != null)
				{
					body.UpdateBodyParts(0);
				}
			}
			base.AfterUnmutate(GO);
		}

		// Token: 0x0600C338 RID: 49976 RVA: 0x0047464B File Offset: 0x0047284B
		public virtual void OnRegenerateDefaultEquipment(Body body)
		{
		}

		// Token: 0x0600C339 RID: 49977 RVA: 0x0047464D File Offset: 0x0047284D
		public virtual void OnDecorateDefaultEquipment(Body body)
		{
		}

		// Token: 0x0600C33A RID: 49978 RVA: 0x0047464F File Offset: 0x0047284F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<DecorateDefaultEquipmentEvent>.ID || ID == PooledEvent<RegenerateDefaultEquipmentEvent>.ID;
		}

		// Token: 0x0600C33B RID: 49979 RVA: 0x0047466D File Offset: 0x0047286D
		public override bool HandleEvent(RegenerateDefaultEquipmentEvent E)
		{
			this.OnRegenerateDefaultEquipment(E.Body);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C33C RID: 49980 RVA: 0x00474682 File Offset: 0x00472882
		public override bool HandleEvent(DecorateDefaultEquipmentEvent E)
		{
			this.OnDecorateDefaultEquipment(E.Body);
			return base.HandleEvent(E);
		}

		// Token: 0x04004DB1 RID: 19889
		public Dictionary<string, int> RegisteredSlots;
	}
}
