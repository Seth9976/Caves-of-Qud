using System;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010D3 RID: 4307
	[Serializable]
	public class CloneOnHit : IPart
	{
		// Token: 0x0600AB38 RID: 43832 RVA: 0x003EA926 File Offset: 0x003E8B26
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB39 RID: 43833 RVA: 0x003EA93C File Offset: 0x003E8B3C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Part CloneOnHit Activation";
				GameObject gameObject2 = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, null, true, false).in100())
				{
					gameObject2 = gameObjectParameter;
					if (Cloning.CanBeCloned(gameObjectParameter2, gameObject2, this.ReplicationContext, false))
					{
						gameObjectParameter2.ApplyEffect(new Budding(gameObjectParameter, 1, this.ReplicationContext), null);
						if (this.DestabilizeChance.in100())
						{
							base.DidX("destabilize", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
							this.ParentObject.Destroy(null, false, false, null);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004697 RID: 18071
		public int Chance = 100;

		// Token: 0x04004698 RID: 18072
		public string ReplicationContext = "CloningDraught";

		// Token: 0x04004699 RID: 18073
		private int DestabilizeChance = 33;
	}
}
