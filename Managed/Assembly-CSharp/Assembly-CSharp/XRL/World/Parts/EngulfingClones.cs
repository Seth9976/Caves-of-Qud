using System;
using XRL.Language;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000EAB RID: 3755
	[Serializable]
	public class EngulfingClones : IPart
	{
		// Token: 0x060096CD RID: 38605 RVA: 0x0036B477 File Offset: 0x00369677
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060096CE RID: 38606 RVA: 0x0036B47A File Offset: 0x0036967A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurnEngulfing");
			base.Register(Object, Registrar);
		}

		// Token: 0x060096CF RID: 38607 RVA: 0x0036B490 File Offset: 0x00369690
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurnEngulfing")
			{
				if (this.TurnsLeft < 0)
				{
					this.TurnsLeft = Stat.Roll(this.Cooldown, null);
				}
				else if (this.TurnsLeft == 0)
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
					Cell currentCell = this.ParentObject.CurrentCell;
					if (gameObjectParameter != null && currentCell != null)
					{
						int num = Stat.Roll(this.Clones, null);
						int num2 = 0;
						for (int i = 0; i < num; i++)
						{
							Cell randomElement = currentCell.GetEmptyAdjacentCells().GetRandomElement<Cell>(null);
							if (randomElement != null)
							{
								Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
								@event.SetParameter("Object", gameObjectParameter);
								@event.SetParameter("Cell", randomElement);
								@event.SetParameter("Mutation", this);
								@event.SetParameter("RealityStabilizationPenetration", this.RealityStabilizationPenetration);
								if (gameObjectParameter.FireEvent(@event) && randomElement.FireEvent(@event))
								{
									GameObject gameObject = gameObjectParameter;
									string prefix = this.Prefix;
									string description = this.Description;
									string message = this.Message;
									if (EvilTwin.CreateEvilTwin(gameObject, prefix, randomElement, message, this.ColorString, this.ParentObject, null, true, description, null))
									{
										num2++;
									}
								}
							}
						}
						if (num2 > 0)
						{
							base.PlayWorldSound(this.Sound, 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
							base.DidXToY("refract", gameObjectParameter, "into " + Grammar.Cardinal(num2) + " additional " + ((num2 == 1) ? "clone" : "clones"), null, null, null, null, gameObjectParameter, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
						else if (num > 0)
						{
							base.DidXToY("try", "to refract", gameObjectParameter, "but fails to push through the normality lattice in the local region of spacetime", null, null, null, null, this.ParentObject, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						}
						this.TurnsLeft = Stat.Roll(this.Cooldown, null);
					}
				}
				else
				{
					this.TurnsLeft--;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E97 RID: 16023
		public int TurnsLeft = -1;

		// Token: 0x04003E98 RID: 16024
		public string Cooldown = "8";

		// Token: 0x04003E99 RID: 16025
		public string Clones = "1";

		// Token: 0x04003E9A RID: 16026
		public string Prefix = "Refracted";

		// Token: 0x04003E9B RID: 16027
		public string ColorString = "&Y";

		// Token: 0x04003E9C RID: 16028
		public int RealityStabilizationPenetration;

		// Token: 0x04003E9D RID: 16029
		public string Sound = "sfx_light_refract";

		// Token: 0x04003E9E RID: 16030
		public string Description = "It's refracted you.";

		// Token: 0x04003E9F RID: 16031
		public string Message;
	}
}
