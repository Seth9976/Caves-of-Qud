using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CE5 RID: 3301
	public class OnHealProceduralCookingTrigger : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008318 RID: 33560 RVA: 0x002FA027 File Offset: 0x002F8227
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.Armed = false;
		}

		// Token: 0x06008319 RID: 33561 RVA: 0x002FA037 File Offset: 0x002F8237
		public override string GetTriggerDescription()
		{
			return "when you heal to full health from half";
		}

		// Token: 0x0600831A RID: 33562 RVA: 0x002FA03E File Offset: 0x002F823E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600831B RID: 33563 RVA: 0x002FA054 File Offset: 0x002F8254
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (!base.Object.isDamaged() && this.Armed)
				{
					this.Armed = false;
					base.Trigger();
				}
				else if (base.Object.isDamaged(0.5, true))
				{
					this.Armed = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400395D RID: 14685
		public bool Armed;
	}
}
