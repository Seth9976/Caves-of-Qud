using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000EA3 RID: 3747
	[Serializable]
	public class DropOnDamage : IPart
	{
		// Token: 0x06009689 RID: 38537 RVA: 0x00368B54 File Offset: 0x00366D54
		public override bool SameAs(IPart p)
		{
			DropOnDamage dropOnDamage = p as DropOnDamage;
			return dropOnDamage.MinimumDamage == this.MinimumDamage && dropOnDamage.Chance == this.Chance && !(dropOnDamage.Number != this.Number) && !(dropOnDamage.Blueprint != this.Blueprint) && dropOnDamage.PopulationRollsAreStatic == this.PopulationRollsAreStatic && dropOnDamage.SpawnAsDropColor == this.SpawnAsDropColor && base.SameAs(p);
		}

		// Token: 0x0600968A RID: 38538 RVA: 0x00368BD9 File Offset: 0x00366DD9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeTookDamage");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600968B RID: 38539 RVA: 0x00368BFC File Offset: 0x00366DFC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.SpawnAsDropColor)
				{
					this.SpawnAsDropColor = false;
					string text = this.Blueprint;
					if (text.Length > 0 && text[0] == '@')
					{
						text = PopulationManager.RollOneFrom(this.Blueprint.Substring(1), null, null).Blueprint;
					}
					if (this.PopulationRollsAreStatic)
					{
						this.Blueprint = text;
					}
					GameObject gameObject = GameObjectFactory.create(this.Blueprint);
					this.ParentObject.Render.DetailColor = gameObject.Render.GetForegroundColor();
				}
			}
			else if (E.ID == "BeforeTookDamage")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell == null)
				{
					return true;
				}
				if (this.MinimumDamage > 0 && E.GetParameter<Damage>("Damage").Amount < this.MinimumDamage)
				{
					return true;
				}
				if (this.Chance.in100() && !string.IsNullOrEmpty(this.Blueprint))
				{
					int i = 0;
					int num = this.Number.RollCached();
					while (i < num)
					{
						string text2 = this.Blueprint;
						if (text2.StartsWith("@"))
						{
							text2 = PopulationManager.RollOneFrom(text2.Substring(1), null, null).Blueprint;
							if (this.PopulationRollsAreStatic)
							{
								this.Blueprint = text2;
							}
						}
						GameObject gameObject2 = GameObject.Create(text2);
						Temporary.CarryOver(this.ParentObject, gameObject2, false);
						Phase.carryOver(this.ParentObject, gameObject2);
						currentCell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						base.DidXToY("drop", gameObject2, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
						i++;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E5C RID: 15964
		public int MinimumDamage;

		// Token: 0x04003E5D RID: 15965
		public int Chance = 100;

		// Token: 0x04003E5E RID: 15966
		public string Number = "1";

		// Token: 0x04003E5F RID: 15967
		public string Blueprint;

		// Token: 0x04003E60 RID: 15968
		public bool PopulationRollsAreStatic = true;

		// Token: 0x04003E61 RID: 15969
		public bool SpawnAsDropColor = true;
	}
}
