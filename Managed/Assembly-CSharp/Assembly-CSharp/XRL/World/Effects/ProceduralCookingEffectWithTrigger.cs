using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.Language;

namespace XRL.World.Effects
{
	// Token: 0x02000CE4 RID: 3300
	[Serializable]
	public class ProceduralCookingEffectWithTrigger : ProceduralCookingEffect
	{
		// Token: 0x0600830C RID: 33548 RVA: 0x002F9A45 File Offset: 0x002F7C45
		public virtual string GetTriggerDescription()
		{
			return "[when something happens]";
		}

		// Token: 0x0600830D RID: 33549 RVA: 0x002F9A4C File Offset: 0x002F7C4C
		public virtual string GetTemplatedTriggerDescription()
		{
			return this.GetTriggerDescription();
		}

		// Token: 0x0600830E RID: 33550 RVA: 0x002F9A54 File Offset: 0x002F7C54
		public override bool SameAs(ProceduralCookingEffect effect)
		{
			if (!base.SameAs(effect))
			{
				return false;
			}
			ProceduralCookingEffectWithTrigger proceduralCookingEffectWithTrigger = effect as ProceduralCookingEffectWithTrigger;
			if (proceduralCookingEffectWithTrigger == null)
			{
				return false;
			}
			if (this.units.Count != effect.units.Count)
			{
				return false;
			}
			using (List<ProceduralCookingTriggeredAction>.Enumerator enumerator = this.triggeredActions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ProceduralCookingTriggeredAction action = enumerator.Current;
					if (!proceduralCookingEffectWithTrigger.triggeredActions.Any<ProceduralCookingTriggeredAction>((ProceduralCookingTriggeredAction e) => e.GetType() == action.GetType()))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600830F RID: 33551 RVA: 0x002F9AFC File Offset: 0x002F7CFC
		public override string GetTemplatedProceduralEffectDescription()
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetTemplatedProceduralEffectDescription());
			stringBuilder.Append(this.GetTemplatedTriggerDescription());
			stringBuilder.Append(' ');
			for (int i = 0; i < this.triggeredActions.Count; i++)
			{
				if (i != 0)
				{
					if (i != this.triggeredActions.Count - 1)
					{
						stringBuilder.Append(", ");
					}
					else
					{
						stringBuilder.Append(" and ");
					}
				}
				stringBuilder.Append(this.triggeredActions[i].GetTemplatedDescription());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008310 RID: 33552 RVA: 0x002F9B8C File Offset: 0x002F7D8C
		public override string GetProceduralEffectDescription()
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetProceduralEffectDescription());
			stringBuilder.Append(this.GetTriggerDescription());
			stringBuilder.Append(' ');
			for (int i = 0; i < this.triggeredActions.Count; i++)
			{
				if (i != 0)
				{
					if (i != this.triggeredActions.Count - 1)
					{
						stringBuilder.Append(", ");
					}
					else
					{
						stringBuilder.Append(" and ");
					}
				}
				stringBuilder.Append(this.triggeredActions[i].GetDescription());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008311 RID: 33553 RVA: 0x002F9C1C File Offset: 0x002F7E1C
		public override void Init(GameObject target)
		{
			base.Init(target);
			foreach (ProceduralCookingTriggeredAction proceduralCookingTriggeredAction in this.triggeredActions)
			{
				proceduralCookingTriggeredAction.Init(target);
			}
		}

		// Token: 0x06008312 RID: 33554 RVA: 0x002F9C74 File Offset: 0x002F7E74
		public override Effect DeepCopy(GameObject Parent)
		{
			ProceduralCookingEffectWithTrigger proceduralCookingEffectWithTrigger = (ProceduralCookingEffectWithTrigger)base.DeepCopy(Parent);
			proceduralCookingEffectWithTrigger.triggeredActions = new List<ProceduralCookingTriggeredAction>(this.triggeredActions.Count);
			foreach (ProceduralCookingTriggeredAction proceduralCookingTriggeredAction in this.triggeredActions)
			{
				proceduralCookingEffectWithTrigger.triggeredActions.Add(proceduralCookingTriggeredAction.DeepCopy());
			}
			return proceduralCookingEffectWithTrigger;
		}

		// Token: 0x06008313 RID: 33555 RVA: 0x002F9CF8 File Offset: 0x002F7EF8
		public void Trigger()
		{
			foreach (ProceduralCookingTriggeredAction proceduralCookingTriggeredAction in this.triggeredActions)
			{
				proceduralCookingTriggeredAction.Apply(base.Object);
				string notification = proceduralCookingTriggeredAction.GetNotification();
				if (notification == null)
				{
					break;
				}
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(Grammar.CapAfterNewlines(Grammar.InitCap(notification.Replace("@thisCreature", "you").Replace("@s", "").Replace("@es", "")
						.Replace("@is", "are")
						.Replace("@they", "you")
						.Replace("@their", "your")
						.Replace("@them", "you"))), null, true);
				}
				else if (base.Visible())
				{
					IComponent<GameObject>.AddPlayerMessage(Grammar.CapAfterNewlines(Grammar.InitCap(notification.Replace("@thisCreature", base.Object.IsPlural ? "these creatures" : "this creature").Replace("@s", base.Object.IsPlural ? "" : "s").Replace("@es", base.Object.IsPlural ? "" : "es")
						.Replace("@is", base.Object.Is)
						.Replace("@they", base.Object.it)
						.Replace("@their", base.Object.its)
						.Replace("@them", base.Object.them))), null, true);
				}
			}
		}

		// Token: 0x06008314 RID: 33556 RVA: 0x002F9EDC File Offset: 0x002F80DC
		public override void Remove(GameObject Object)
		{
			Object.FireEvent("RemoveProceduralCookingEffects");
			base.Remove(Object);
			foreach (ProceduralCookingTriggeredAction proceduralCookingTriggeredAction in this.triggeredActions)
			{
				proceduralCookingTriggeredAction.Remove(Object);
			}
		}

		// Token: 0x06008315 RID: 33557 RVA: 0x002F9F40 File Offset: 0x002F8140
		public override bool WantEvent(int ID, int cascade)
		{
			using (List<ProceduralCookingTriggeredAction>.Enumerator enumerator = this.triggeredActions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.WantEvent(ID, cascade))
					{
						return true;
					}
				}
			}
			return base.WantEvent(ID, cascade);
		}

		// Token: 0x06008316 RID: 33558 RVA: 0x002F9FA4 File Offset: 0x002F81A4
		public override bool HandleEvent(MinEvent E)
		{
			foreach (ProceduralCookingTriggeredAction proceduralCookingTriggeredAction in this.triggeredActions)
			{
				if (!E.Dispatch(proceduralCookingTriggeredAction))
				{
					return false;
				}
				if (!proceduralCookingTriggeredAction.HandleEvent(E))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400395C RID: 14684
		public List<ProceduralCookingTriggeredAction> triggeredActions = new List<ProceduralCookingTriggeredAction>();
	}
}
