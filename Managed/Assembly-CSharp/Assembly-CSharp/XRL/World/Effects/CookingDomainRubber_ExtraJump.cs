using System;
using XRL.UI;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D02 RID: 3330
	[Serializable]
	public class CookingDomainRubber_ExtraJump : ProceduralCookingEffectUnit
	{
		// Token: 0x06008397 RID: 33687 RVA: 0x002FB06E File Offset: 0x002F926E
		public override string GetDescription()
		{
			return "Whenever @thisCreature jump@s, @they may immediately jump again.";
		}

		// Token: 0x06008398 RID: 33688 RVA: 0x002FB075 File Offset: 0x002F9275
		public override string GetTemplatedDescription()
		{
			return "Whenever @thisCreature jump@s, @they may immediately jump again.";
		}

		// Token: 0x06008399 RID: 33689 RVA: 0x002FB07C File Offset: 0x002F927C
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "Jumped");
		}

		// Token: 0x0600839A RID: 33690 RVA: 0x002FB08A File Offset: 0x002F928A
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "Jumped");
		}

		// Token: 0x0600839B RID: 33691 RVA: 0x002FB098 File Offset: 0x002F9298
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
				The.Core.RenderBase(true, false);
				if (gameObject.IsPlayer())
				{
					Popup.Show("You bounce.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				Acrobatics_Jump.Jump(gameObject, 0, null, base.GetType().Name);
			}
		}
	}
}
