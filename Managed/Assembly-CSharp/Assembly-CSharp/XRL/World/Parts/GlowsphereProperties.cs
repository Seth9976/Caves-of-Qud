using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001041 RID: 4161
	[Serializable]
	public class GlowsphereProperties : IPart
	{
		// Token: 0x0600A5AF RID: 42415 RVA: 0x003CA5D0 File Offset: 0x003C87D0
		public override bool SameAs(IPart p)
		{
			return !((p as GlowsphereProperties).AllowedParts != this.AllowedParts) && base.SameAs(p);
		}

		// Token: 0x0600A5B0 RID: 42416 RVA: 0x003CA5F3 File Offset: 0x003C87F3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600A5B1 RID: 42417 RVA: 0x003CA609 File Offset: 0x003C8809
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("stars", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5B2 RID: 42418 RVA: 0x003CA62C File Offset: 0x003C882C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Equipped");
			Registrar.Register("Unequipped");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A5B3 RID: 42419 RVA: 0x003CA658 File Offset: 0x003C8858
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Equipped")
			{
				BodyPart bodyPart = E.GetParameter("BodyPart") as BodyPart;
				if (this.AllowedParts.Contains(bodyPart.Type))
				{
					LightSource part = this.ParentObject.GetPart<LightSource>();
					if (part != null)
					{
						part.Lit = true;
					}
				}
				else
				{
					LightSource part2 = this.ParentObject.GetPart<LightSource>();
					if (part2 != null)
					{
						part2.Lit = false;
					}
				}
			}
			if (E.ID == "Unequipped")
			{
				LightSource part3 = this.ParentObject.GetPart<LightSource>();
				if (part3 != null)
				{
					part3.Lit = true;
				}
				return true;
			}
			if (E.ID == "EnteredCell")
			{
				LightSource part4 = this.ParentObject.GetPart<LightSource>();
				if (part4 != null)
				{
					part4.Lit = true;
				}
				return true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004471 RID: 17521
		public string AllowedParts = "Hand";
	}
}
