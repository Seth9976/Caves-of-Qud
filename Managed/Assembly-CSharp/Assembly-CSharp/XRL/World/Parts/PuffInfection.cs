using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Language;
using XRL.Rules;
using XRL.World.Anatomy;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001037 RID: 4151
	[Serializable]
	public class PuffInfection : IPart
	{
		// Token: 0x0600A552 RID: 42322 RVA: 0x003C730E File Offset: 0x003C550E
		public override bool SameAs(IPart p)
		{
			return !((p as PuffInfection).ColorString != this.ColorString) && base.SameAs(p);
		}

		// Token: 0x0600A553 RID: 42323 RVA: 0x003C7331 File Offset: 0x003C5531
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A554 RID: 42324 RVA: 0x003C7334 File Offset: 0x003C5534
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Equipped");
			Registrar.Register("Unequipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A555 RID: 42325 RVA: 0x003C7354 File Offset: 0x003C5554
		public bool Puff()
		{
			if (this.ColorString.Length == 1)
			{
				Stat.ReseedFrom("PufferType", false);
				string text = Algorithms.RandomShuffle<string>(SporePuffer.PufferList)[Convert.ToInt32(this.ColorString)];
				GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.Blueprints[text];
				this.ColorString = gameObjectBlueprint.GetPartParameter<string>("Render", "ColorString", null);
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell == null)
			{
				return false;
			}
			List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells();
			bool flag = false;
			if (localAdjacentCells != null)
			{
				foreach (Cell cell in localAdjacentCells)
				{
					bool flag2 = false;
					bool flag3 = false;
					foreach (GameObject gameObject in cell.LoopObjects())
					{
						if (!flag3 && gameObject.Brain != null)
						{
							flag3 = true;
						}
						if (!flag2 && gameObject.HasPart<GasFungalSpores>())
						{
							flag2 = true;
						}
						if (flag3 && flag2)
						{
							break;
						}
					}
					if (flag3 && !flag2)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				GameObject gameObject2 = this.ParentObject.Equipped ?? this.ParentObject;
				if (gameObject2.CurrentCell != null)
				{
					gameObject2.ParticleBlip("&W*", 10, 0L, false);
				}
				for (int i = 0; i < localAdjacentCells.Count; i++)
				{
					if (!localAdjacentCells[i].HasObjectWithPart("GasFungalSpores"))
					{
						GameObject gameObject3 = localAdjacentCells[i].AddObject("FungalSporeGasPuff", null, null, null, null);
						gameObject3.GetPart<Gas>().ColorString = this.ColorString;
						gameObject3.GetPart<Gas>().Creator = gameObject2;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600A556 RID: 42326 RVA: 0x003C7520 File Offset: 0x003C5720
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				if (25.in100() && this.Puff())
				{
					GameObject equipped = this.ParentObject.Equipped;
					if (equipped != null)
					{
						if (equipped.IsPlayer())
						{
							BodyPart bodyPart = this.ParentObject.EquippedOn();
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"&yYour ",
								bodyPart.GetOrdinalName(),
								" ",
								bodyPart.Plural ? "spew" : "spews",
								" a cloud of spores."
							}), null, true);
						}
						else if (IComponent<GameObject>.Visible(equipped))
						{
							BodyPart bodyPart2 = this.ParentObject.EquippedOn();
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								Grammar.MakePossessive(equipped.The + equipped.ShortDisplayName),
								"&y ",
								bodyPart2.GetOrdinalName(),
								" ",
								bodyPart2.Plural ? "spew" : "spews",
								" a cloud of spores."
							}), null, true);
						}
					}
				}
			}
			else if (E.ID == "Equipped")
			{
				E.GetGameObjectParameter("EquippingObject").RegisterPartEvent(this, "BeforeApplyDamage");
			}
			else if (E.ID == "Unequipped")
			{
				E.GetGameObjectParameter("UnequippingObject").UnregisterPartEvent(this, "BeforeApplyDamage");
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004457 RID: 17495
		public string ColorString = "0";
	}
}
