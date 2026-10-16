using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C66 RID: 3174
	[Serializable]
	public class BasicTriggeredCookingEffect : Effect
	{
		// Token: 0x060080F8 RID: 33016 RVA: 0x002F5786 File Offset: 0x002F3986
		public BasicTriggeredCookingEffect()
		{
			this.DisplayName = "{{W|well fed}}";
			this.Duration = 1;
		}

		// Token: 0x060080F9 RID: 33017 RVA: 0x002F57AB File Offset: 0x002F39AB
		public override int GetEffectType()
		{
			return 67108868;
		}

		// Token: 0x060080FA RID: 33018 RVA: 0x002F57B2 File Offset: 0x002F39B2
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060080FB RID: 33019 RVA: 0x002F57B5 File Offset: 0x002F39B5
		public override string GetDescription()
		{
			return "{{w|metabolized effect}}";
		}

		// Token: 0x060080FC RID: 33020 RVA: 0x002F57BC File Offset: 0x002F39BC
		public virtual void ApplyEffect(GameObject Object)
		{
		}

		// Token: 0x060080FD RID: 33021 RVA: 0x002F57BE File Offset: 0x002F39BE
		public virtual void RemoveEffect(GameObject Object)
		{
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x002F57C0 File Offset: 0x002F39C0
		public override bool Apply(GameObject Object)
		{
			if (Object.FireEvent(Event.New("ApplyBasicTriggeredCookingEffect", 0, 0, 0)))
			{
				this.ApplyEffect(Object);
				return true;
			}
			return false;
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x002F57E1 File Offset: 0x002F39E1
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

		// Token: 0x06008100 RID: 33024 RVA: 0x002F5809 File Offset: 0x002F3A09
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008101 RID: 33025 RVA: 0x002F581E File Offset: 0x002F3A1E
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.Duration--;
			}
			return true;
		}

		// Token: 0x0400390B RID: 14603
		public string wellFedMessage = "You eat the meal. It's tastier than usual.";

		// Token: 0x0400390C RID: 14604
		public bool removed;
	}
}
