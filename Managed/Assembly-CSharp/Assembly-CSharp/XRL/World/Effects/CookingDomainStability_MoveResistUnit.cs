using System;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D0B RID: 3339
	[Serializable]
	public class CookingDomainStability_MoveResistUnit : ProceduralCookingEffectUnit
	{
		// Token: 0x060083CA RID: 33738 RVA: 0x002FB835 File Offset: 0x002F9A35
		public override void Init(GameObject target)
		{
		}

		// Token: 0x060083CB RID: 33739 RVA: 0x002FB837 File Offset: 0x002F9A37
		public override string GetDescription()
		{
			return SavingThrows.GetSaveBonusDescription(this.Amount, this.Vs, false);
		}

		// Token: 0x060083CC RID: 33740 RVA: 0x002FB84B File Offset: 0x002F9A4B
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "ModifyDefendingSave");
		}

		// Token: 0x060083CD RID: 33741 RVA: 0x002FB859 File Offset: 0x002F9A59
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "ModifyDefendingSave");
		}

		// Token: 0x060083CE RID: 33742 RVA: 0x002FB868 File Offset: 0x002F9A68
		public override void FireEvent(Event E)
		{
			if (E.ID == "ModifyDefendingSave")
			{
				try
				{
					if (this.parent != null && this.parent.Object != null && SavingThrows.Applicable(this.Vs, E))
					{
						E.SetParameter("Roll", E.GetIntParameter("Roll", 0) + this.Amount);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("CookingDomainStability_MoveResistUnit::EndTurn", ex, "game_exception");
				}
				return;
			}
		}

		// Token: 0x0400396A RID: 14698
		public string Vs = "Move,Knockdown,Restraint";

		// Token: 0x0400396B RID: 14699
		public int Amount = 6;
	}
}
