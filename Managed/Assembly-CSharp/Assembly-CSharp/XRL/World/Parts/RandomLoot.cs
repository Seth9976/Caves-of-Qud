using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000EF5 RID: 3829
	[Serializable]
	public class RandomLoot : IPart
	{
		// Token: 0x060098F3 RID: 39155 RVA: 0x003783C7 File Offset: 0x003765C7
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060098F4 RID: 39156 RVA: 0x003783CA File Offset: 0x003765CA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x060098F5 RID: 39157 RVA: 0x003783E8 File Offset: 0x003765E8
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.Check(true, E.Context, E);
			return base.HandleEvent(E);
		}

		// Token: 0x060098F6 RID: 39158 RVA: 0x003783FF File Offset: 0x003765FF
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			this.Check(false, "Death", E);
			return base.HandleEvent(E);
		}

		// Token: 0x060098F7 RID: 39159 RVA: 0x00378418 File Offset: 0x00376618
		private void Check(bool Create, string Context = null, IEvent ParentEvent = null)
		{
			if (this.LootCreated)
			{
				return;
			}
			this.LootCreated = true;
			if (this.ParentObject.HasTagOrProperty("NoLoot"))
			{
				return;
			}
			if (Create && this.ParentObject.GetIntProperty("Humanoid", 0) == 0)
			{
				if (this.ParentObject.GetIntProperty("Ape", 0) == 0)
				{
					return;
				}
				if (90.in100())
				{
					return;
				}
			}
			List<GameObject> list = new List<GameObject>(8);
			if (this.ParentObject.HasIntProperty("RareLoot"))
			{
				int intProperty = this.ParentObject.GetIntProperty("RareLoot", 0);
				int num = Stat.Random(1, 3);
				for (int i = 0; i < num; i++)
				{
					list.Add(PopulationManager.CreateOneFrom("Junk " + intProperty.ToString() + "R", null, 100, 0, null, Context, null, null, null));
				}
			}
			else
			{
				int j = Stat.Random(1, 100) + this.ParentObject.Stat("Level", 0);
				RandomLoot.Deaths++;
				for (j += RandomLoot.Deaths; j > 98; j -= 50 + Stat.Random(1, 100))
				{
					list.Add(PopulationManager.CreateOneFrom("Junk " + Tier.Constrain(this.ParentObject.Stat("Level", 0) / 5 + 1).ToString(), null, 0, 0, null, Context, null, null, null));
					RandomLoot.Deaths = 0;
				}
			}
			if (Create)
			{
				this.ParentObject.ReceiveObject(list, false, null);
				this.LootCreated = true;
				return;
			}
			IInventory dropInventory = this.ParentObject.GetDropInventory();
			if (dropInventory != null)
			{
				foreach (GameObject gameObject in list)
				{
					if (GameObject.Validate(gameObject) && gameObject.IsReal)
					{
						dropInventory.AddObjectToInventory(gameObject, null, false, false, true, null, ParentEvent);
						DroppedEvent.Send(this.ParentObject, gameObject, false);
					}
				}
			}
		}

		// Token: 0x04003FB8 RID: 16312
		[NonSerialized]
		public static int Deaths;

		// Token: 0x04003FB9 RID: 16313
		public bool LootCreated;
	}
}
