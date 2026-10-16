using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200109E RID: 4254
	[Serializable]
	public class SpringBoots : IPart
	{
		// Token: 0x0600A935 RID: 43317 RVA: 0x003DDC41 File Offset: 0x003DBE41
		public override bool SameAs(IPart p)
		{
			return (p as SpringBoots).Timer == this.Timer && base.SameAs(p);
		}

		// Token: 0x0600A936 RID: 43318 RVA: 0x003DDC5F File Offset: 0x003DBE5F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A937 RID: 43319 RVA: 0x003DDC88 File Offset: 0x003DBE88
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Timer > 0)
			{
				this.Timer--;
				if (this.Timer == 0)
				{
					this.Timer--;
					GameObject equipped = this.ParentObject.Equipped;
					if (equipped != null)
					{
						equipped.ApplyEffect(new Springing(this.ParentObject), null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A938 RID: 43320 RVA: 0x003DDCEC File Offset: 0x003DBEEC
		public override bool HandleEvent(EquippedEvent E)
		{
			this.Timer = 100;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A939 RID: 43321 RVA: 0x003DDD00 File Offset: 0x003DBF00
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.Timer = 0;
			Effect effect = E.Actor.GetEffect<Springing>((Springing fx) => fx.Source == this.ParentObject);
			if (effect != null)
			{
				E.Actor.RemoveEffect(effect, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A93A RID: 43322 RVA: 0x003DDD44 File Offset: 0x003DBF44
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040045D8 RID: 17880
		public int Timer;
	}
}
