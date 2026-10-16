using System;
using System.Collections.Generic;
using XRL.World.Skills.Cooking;

namespace XRL.World.Parts
{
	// Token: 0x02000E69 RID: 3689
	[Serializable]
	public class Chef : IPart
	{
		// Token: 0x0600950F RID: 38159 RVA: 0x0035E85B File Offset: 0x0035CA5B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009510 RID: 38160 RVA: 0x0035E85E File Offset: 0x0035CA5E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009511 RID: 38161 RVA: 0x0035E873 File Offset: 0x0035CA73
		public override IPart DeepCopy(GameObject Parent)
		{
			Chef chef = base.DeepCopy(Parent) as Chef;
			chef.signatureDishes = new List<CookingRecipe>(this.signatureDishes);
			return chef;
		}

		// Token: 0x06009512 RID: 38162 RVA: 0x0035E894 File Offset: 0x0035CA94
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.bCreated)
				{
					return true;
				}
				this.bCreated = true;
				int newTier = this.ParentObject.Physics.CurrentCell.ParentZone.NewTier;
				GameObject gameObject = GameObjectFactory.Factory.CreateObject("BaseCookbook");
				if (this.ParentObject.HasPart<GivesRep>())
				{
					gameObject.AddPart<Cookbook>(new Cookbook("Generic_LegendaryChef", "3", newTier, this.ParentObject.DisplayNameOnlyStripped), true, false);
				}
				else
				{
					gameObject.AddPart<Cookbook>(new Cookbook(), true, false);
				}
				gameObject.GetPart<Cookbook>().GenerateCookbook();
				this.ParentObject.ReceiveObject(gameObject, false, null);
				GameObject gameObject2 = GameObjectFactory.Factory.CreateObject("ChefOven");
				Cookbook part = gameObject.GetPart<Cookbook>();
				if (this.ParentObject.HasPart<GivesRep>())
				{
					this.signatureDishes = part.recipes;
				}
				else
				{
					this.signatureDishes.Add(part.recipes.GetRandomElement<CookingRecipe>(null));
				}
				if (this.ParentObject.HasTag("StiltChef"))
				{
					CookingRecipe cookingRecipe = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Skills.Cooking.HotandSpiny", false, false, true)) as CookingRecipe;
					this.signatureDishes.Add(cookingRecipe);
				}
				if (gameObject2.HasPart<Campfire>())
				{
					gameObject2.RemovePart<Campfire>();
				}
				gameObject2.AddPart<Campfire>(new Campfire(this.signatureDishes), true, false);
				Cell connectedSpawnLocation = this.ParentObject.Physics.CurrentCell.GetConnectedSpawnLocation(true);
				if (connectedSpawnLocation != null)
				{
					connectedSpawnLocation.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DC6 RID: 15814
		public bool bCreated;

		// Token: 0x04003DC7 RID: 15815
		public List<CookingRecipe> signatureDishes = new List<CookingRecipe>();
	}
}
