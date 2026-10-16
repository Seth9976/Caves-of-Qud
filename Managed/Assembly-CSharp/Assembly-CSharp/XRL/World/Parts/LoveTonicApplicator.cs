using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE3 RID: 3555
	[Serializable]
	public class LoveTonicApplicator : IPart
	{
		// Token: 0x06009061 RID: 36961 RVA: 0x003415B6 File Offset: 0x0033F7B6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009062 RID: 36962 RVA: 0x003415CC File Offset: 0x0033F7CC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyTonic")
			{
				int intParameter = E.GetIntParameter("Dosage", 0);
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Subject");
				if (gameObjectParameter2.IsPlayer())
				{
					if (intParameter <= 0)
					{
						return false;
					}
					LoveTonic loveTonic = new LoveTonic(GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Love", Stat.Random(500 * intParameter, 700 * intParameter), intParameter, false));
					if (!gameObjectParameter2.ApplyEffect(loveTonic, null))
					{
						return false;
					}
				}
				else
				{
					GameObject gameObject;
					if ((gameObject = E.GetGameObjectParameter("Attacker")) == null)
					{
						Cell currentCell = gameObjectParameter2.CurrentCell;
						GameObject gameObject2;
						if (currentCell == null)
						{
							gameObject2 = null;
						}
						else
						{
							List<GameObject> list = currentCell.FastFloodVisibility("Combat", 20);
							gameObject2 = ((list != null) ? list.GetRandomElement<GameObject>(null) : null);
						}
						gameObject = gameObject2 ?? The.Player;
					}
					GameObject gameObject3 = gameObject;
					if (gameObject3 != null && intParameter > 0)
					{
						GameObject gameObject4 = gameObjectParameter2;
						GameObject gameObject5 = gameObject3;
						if (gameObject4.CheckInfluence(base.Name, gameObject5, false) && this.GetChance(gameObject3, gameObjectParameter2).in100())
						{
							Lovesick lovesick = new Lovesick(Stat.Random(3000 * intParameter, 3600 * intParameter), gameObject3);
							if (!gameObjectParameter2.ApplyEffect(lovesick, null))
							{
								return false;
							}
							goto IL_016D;
						}
					}
					IComponent<GameObject>.AddPlayerMessage(gameObjectParameter2.Does("look", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you over and" + gameObjectParameter2.GetVerb("metabolize", true, false, false, true, false) + " the love tonic with no effect.", null, true);
				}
			}
			IL_016D:
			return base.FireEvent(E);
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x0034174D File Offset: 0x0033F94D
		public int GetChance(GameObject Attacker, GameObject Target)
		{
			return 95 + Attacker.Stat("Level", 0) - Target.Stat("Level", 0) - Target.GetIntProperty("LoveTonicResistance", 0);
		}
	}
}
