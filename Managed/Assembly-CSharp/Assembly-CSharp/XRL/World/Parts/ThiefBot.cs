using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Language;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E93 RID: 3731
	[Serializable]
	public class ThiefBot : IPart
	{
		// Token: 0x06009630 RID: 38448 RVA: 0x003665DB File Offset: 0x003647DB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIBeginKill");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009631 RID: 38449 RVA: 0x003665F0 File Offset: 0x003647F0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AIBeginKill" && this.ParentObject.Inventory != null)
			{
				if (this.ParentObject.Inventory.Objects.Count > 0)
				{
					Debug.Log("I should drop off my inventory");
					this.ParentObject.Brain.PushGoal(new DropOffStolenGoods());
				}
				else
				{
					GameObject target = this.ParentObject.Target;
					if (target != null && this.ParentObject.DistanceTo(target) <= 1 && (this.ParentObject.IsFlying || !target.IsFlying))
					{
						if (this.ParentObject.Brain != null)
						{
							this.ParentObject.Brain.Think("I'm going to steal something!");
						}
						this.ParentObject.UseEnergy(1000);
						if (!this.ParentObject.PhaseMatches(target))
						{
							if (target.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage(ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(Grammar.MakePossessive(this.ParentObject.The + this.ParentObject.ShortDisplayName)) + " pincers pass through you harmlessly.", 'G', true);
							}
						}
						else if (target.MakeSave("Agility", 10, this.ParentObject, "Agility", "Disarm", false, false, false, false, null))
						{
							int num = Stat.Random(1, 100);
							GameObject gameObject = null;
							if (num <= 25 && target.Body != null)
							{
								List<BodyPart> parts = target.Body.GetParts();
								parts.RemoveAll((BodyPart x) => x.Equipped == null || !x.Equipped.CanBeUnequipped(null, null, false, false));
								BodyPart randomElement = parts.GetRandomElement<BodyPart>(null);
								gameObject = ((randomElement != null) ? randomElement.Equipped : null);
							}
							if (num > 25 || gameObject == null)
							{
								Inventory inventory = target.Inventory;
								gameObject = ((inventory != null) ? inventory.Objects.GetRandomElement<GameObject>(null) : null);
							}
							if (gameObject != null && this.ParentObject.TakeObject(gameObject, false, true, new int?(0), null, null))
							{
								if (this.ParentObject.IsPlayerControlled() || target.IsPlayerControlled())
								{
									string text = Event.NewStringBuilder(null).Append(this.ParentObject.Does("snag", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false)).Compound(target.poss(gameObject, true, null))
										.Append('!')
										.ToString();
									base.EmitMessage(text, ColorCoding.ConsequentialColorChar(null, target), false, false, false, null, null);
								}
								target.DustPuff("&y", 15);
							}
						}
						else if (target.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You avoid " + Grammar.MakePossessive(this.ParentObject.the + this.ParentObject.ShortDisplayName) + " pincers.", 'G', true);
						}
						return false;
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
