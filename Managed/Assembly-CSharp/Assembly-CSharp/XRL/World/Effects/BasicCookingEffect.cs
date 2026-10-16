using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C65 RID: 3173
	[Serializable]
	public class BasicCookingEffect : Effect
	{
		// Token: 0x060080EF RID: 33007 RVA: 0x002F5678 File Offset: 0x002F3878
		public BasicCookingEffect()
		{
			this.DisplayName = "{{W|well fed}}";
			this.Duration = 1;
		}

		// Token: 0x060080F0 RID: 33008 RVA: 0x002F569D File Offset: 0x002F389D
		public override int GetEffectType()
		{
			return 67108868;
		}

		// Token: 0x060080F1 RID: 33009 RVA: 0x002F56A4 File Offset: 0x002F38A4
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060080F2 RID: 33010 RVA: 0x002F56A7 File Offset: 0x002F38A7
		public virtual void ApplyEffect(GameObject Object)
		{
		}

		// Token: 0x060080F3 RID: 33011 RVA: 0x002F56A9 File Offset: 0x002F38A9
		public virtual void RemoveEffect(GameObject Object)
		{
		}

		// Token: 0x060080F4 RID: 33012 RVA: 0x002F56AB File Offset: 0x002F38AB
		public override bool Apply(GameObject Object)
		{
			if (Object.FireEvent("ApplyWellFed"))
			{
				this.ApplyEffect(Object);
				return true;
			}
			return false;
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x002F56C4 File Offset: 0x002F38C4
		public override void Remove(GameObject Object)
		{
			if (this.Duration > 0)
			{
				this.Duration = 0;
			}
			if (!this.removed)
			{
				this.RemoveEffect(Object);
				this.removed = true;
			}
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x002F56EC File Offset: 0x002F38EC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyWellFed");
			Registrar.Register("BecameHungry");
			Registrar.Register("BecameFamished");
			Registrar.Register("ClearFoodEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x002F5724 File Offset: 0x002F3924
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BecameHungry" || E.ID == "BecameFamished" || E.ID == "ApplyWellFed" || E.ID == "ClearFoodEffects")
			{
				this.Remove(base.Object);
			}
			return true;
		}

		// Token: 0x04003909 RID: 14601
		public string wellFedMessage = "You eat the meal. It's tastier than usual.";

		// Token: 0x0400390A RID: 14602
		public bool removed;
	}
}
