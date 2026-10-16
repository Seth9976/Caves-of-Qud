using System;
using XRL.UI;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D03 RID: 3331
	[Serializable]
	public class CookingDomainRubber_Extra2Jumps : ProceduralCookingEffectUnit
	{
		// Token: 0x0600839D RID: 33693 RVA: 0x002FB148 File Offset: 0x002F9348
		public override string GetDescription()
		{
			return "Whenever @thisCreature jump@s, @they may immediately jump two more times.";
		}

		// Token: 0x0600839E RID: 33694 RVA: 0x002FB14F File Offset: 0x002F934F
		public override string GetTemplatedDescription()
		{
			return "Whenever @thisCreature jump@s, @they may immediately jump two more times.";
		}

		// Token: 0x0600839F RID: 33695 RVA: 0x002FB156 File Offset: 0x002F9356
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "Jumped");
		}

		// Token: 0x060083A0 RID: 33696 RVA: 0x002FB164 File Offset: 0x002F9364
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "Jumped");
		}

		// Token: 0x060083A1 RID: 33697 RVA: 0x002FB174 File Offset: 0x002F9374
		public override void FireEvent(Event E)
		{
			if (E.ID == "Jumped")
			{
				if (E.GetIntParameter("Pass", 0) != JumpedEvent.PASSES)
				{
					return;
				}
				string stringParameter = E.GetStringParameter("SourceKey", null);
				if (stringParameter != null && stringParameter.Contains("CookingDomainRubber"))
				{
					return;
				}
				ProceduralCookingEffect parent = this.parent;
				GameObject gameObject = ((parent != null) ? parent.Object : null);
				if (gameObject == null)
				{
					return;
				}
				for (int i = 0; i < 2; i++)
				{
					The.Core.RenderBase(true, false);
					if (gameObject.IsPlayer())
					{
						Popup.Show("You bounce.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					if (!Acrobatics_Jump.Jump(gameObject, 0, null, base.GetType().Name))
					{
						break;
					}
				}
			}
		}
	}
}
