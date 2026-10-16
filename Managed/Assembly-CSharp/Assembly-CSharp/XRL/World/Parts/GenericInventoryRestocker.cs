using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using XRL.World.Capabilities;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02000EBB RID: 3771
	[Serializable]
	public class GenericInventoryRestocker : IPart
	{
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06009737 RID: 38711 RVA: 0x0036DDEE File Offset: 0x0036BFEE
		// (set) Token: 0x06009738 RID: 38712 RVA: 0x0036DE0B File Offset: 0x0036C00B
		public string Table
		{
			get
			{
				if (!this.Tables.IsNullOrEmpty<string>())
				{
					return this.Tables[0];
				}
				return null;
			}
			set
			{
				this.SetTables(ref this.Tables, value);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06009739 RID: 38713 RVA: 0x0036DE1A File Offset: 0x0036C01A
		// (set) Token: 0x0600973A RID: 38714 RVA: 0x0036DE37 File Offset: 0x0036C037
		public string HeroTable
		{
			get
			{
				if (!this.HeroTables.IsNullOrEmpty<string>())
				{
					return this.HeroTables[0];
				}
				return null;
			}
			set
			{
				this.SetTables(ref this.HeroTables, value);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (set) Token: 0x0600973B RID: 38715 RVA: 0x0036DE46 File Offset: 0x0036C046
		[JsonIgnore]
		public double RestockFrequencyDays
		{
			set
			{
				this.RestockFrequency = (long)(1200.0 * value);
			}
		}

		// Token: 0x0600973C RID: 38716 RVA: 0x0036DE5A File Offset: 0x0036C05A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600973D RID: 38717 RVA: 0x0036DE5D File Offset: 0x0036C05D
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.LastRestockTick);
			Writer.WriteOptimized(this.RestockFrequency);
			Writer.WriteOptimized(this.Chance);
			Writer.Write(this.Tables);
			Writer.Write(this.HeroTables);
		}

		// Token: 0x0600973E RID: 38718 RVA: 0x0036DE9B File Offset: 0x0036C09B
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.LastRestockTick = Reader.ReadOptimizedInt64();
			this.RestockFrequency = Reader.ReadOptimizedInt64();
			this.Chance = Reader.ReadOptimizedInt32();
			this.Tables = Reader.ReadStringList();
			this.HeroTables = Reader.ReadStringList();
		}

		// Token: 0x0600973F RID: 38719 RVA: 0x0036DED9 File Offset: 0x0036C0D9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == SingletonEvent<StartTradeEvent>.ID || ID == PooledEvent<AllowTradeWithNoInventoryEvent>.ID;
		}

		// Token: 0x06009740 RID: 38720 RVA: 0x0036DEFF File Offset: 0x0036C0FF
		public override bool HandleEvent(AllowTradeWithNoInventoryEvent E)
		{
			return false;
		}

		// Token: 0x06009741 RID: 38721 RVA: 0x0036DF02 File Offset: 0x0036C102
		public override bool HandleEvent(StartTradeEvent E)
		{
			if (this.LastRestockTick == 0L)
			{
				this.PerformRestock(true);
				this.LastRestockTick = The.Game.TimeTicks;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009742 RID: 38722 RVA: 0x0036DF2C File Offset: 0x0036C12C
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "LastRestockTick", (float)this.LastRestockTick);
			E.AddEntry(this, "RestockFrequency", (float)this.RestockFrequency);
			E.AddEntry(this, "Chance", this.Chance);
			E.AddEntry(this, "Tables", this.Tables.IsNullOrEmpty<string>() ? "None" : string.Join(", ", this.Tables));
			E.AddEntry(this, "HeroTables", this.HeroTables.IsNullOrEmpty<string>() ? "None" : string.Join(", ", this.HeroTables));
			return base.HandleEvent(E);
		}

		// Token: 0x06009743 RID: 38723 RVA: 0x0036DFD8 File Offset: 0x0036C1D8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009744 RID: 38724 RVA: 0x0036DFDC File Offset: 0x0036C1DC
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.LastRestockTick == 0L)
			{
				this.PerformRestock(true);
				this.LastRestockTick = TimeTick;
				return;
			}
			long num = TimeTick - this.LastRestockTick;
			if (num >= this.RestockFrequency && this.ParentObject.InSameZone(The.Player))
			{
				this.LastRestockTick = TimeTick;
				if (!this.ParentObject.IsPlayerControlled() && !this.ParentObject.WasPlayer() && ((long)this.Chance * (num / this.RestockFrequency)).in100())
				{
					this.PerformRestock(false);
				}
			}
		}

		// Token: 0x06009745 RID: 38725 RVA: 0x0036E063 File Offset: 0x0036C263
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009746 RID: 38726 RVA: 0x0036E066 File Offset: 0x0036C266
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("MadeHero");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009747 RID: 38727 RVA: 0x0036E07C File Offset: 0x0036C27C
		public static Action<GameObject> GetCraftmarkApplication(GameObject Actor)
		{
			HasMakersMark part = Actor.GetPart<HasMakersMark>();
			string mark = ((part != null) ? part.Mark : null);
			if (!mark.IsNullOrEmpty())
			{
				string markColor = ((part != null) ? part.Color : null) ?? "R";
				int basicBestowalChances;
				int.TryParse(Actor.GetPropertyOrTag("HeroGenericInventoryBasicBestowalChances", null), out basicBestowalChances);
				int basicBestowalPercentage;
				int.TryParse(Actor.GetPropertyOrTag("HeroGenericInventoryBasicBestowalPercentage", null), out basicBestowalPercentage);
				return delegate(GameObject obj)
				{
					if (TinkeringHelpers.EligibleForMakersMark(obj))
					{
						int num = 5;
						obj.RequirePart<MakersMark>(false).AddCrafter(Actor, mark, markColor);
						int num2 = 0;
						while (num2 < basicBestowalChances && basicBestowalPercentage.in100())
						{
							if (RelicGenerator.ApplyBasicBestowal(obj, null, 1, null, false, true))
							{
								num += 30;
							}
							num2++;
						}
						obj.RequirePart<Commerce>(false).Value += (double)num;
					}
				};
			}
			return null;
		}

		// Token: 0x06009748 RID: 38728 RVA: 0x0036E124 File Offset: 0x0036C324
		public void PerformStock(bool Restock = false, bool Silent = false)
		{
			if (this.ParentObject.IsTemporary)
			{
				return;
			}
			Inventory inventory = this.ParentObject.Inventory;
			string text = (Restock ? "Restock" : "Stock");
			Action<GameObject> craftmarkApplication = GenericInventoryRestocker.GetCraftmarkApplication(this.ParentObject);
			if (!Restock)
			{
				this.ParentObject.SetIntProperty("Merchant", 1, false);
				foreach (GameObject gameObject in inventory.Objects)
				{
					if (!gameObject.HasPropertyOrTag("norestock") && !gameObject.HasProperty("_stock"))
					{
						gameObject.SetIntProperty("norestock", 1, false);
					}
				}
			}
			int intProperty = this.ParentObject.GetIntProperty("InventoryTier", ZoneManager.zoneGenerationContextTier);
			List<GameObject> list = new List<GameObject>(inventory.Objects);
			if (!this.Tables.IsNullOrEmpty<string>())
			{
				foreach (string text2 in this.Tables)
				{
					this.ParentObject.EquipFromPopulationTable(text2, intProperty, craftmarkApplication, text, false, true);
				}
			}
			if (this.ParentObject.GetIntProperty("Hero", 0) > 0 && !this.HeroTables.IsNullOrEmpty<string>())
			{
				foreach (string text3 in this.HeroTables)
				{
					this.ParentObject.EquipFromPopulationTable(text3, intProperty, craftmarkApplication, text, false, true);
				}
			}
			bool flag = false;
			foreach (GameObject gameObject2 in inventory.Objects)
			{
				if (!list.Contains(gameObject2))
				{
					gameObject2.SetIntProperty("_stock", 1, false);
					flag = true;
				}
			}
			if (flag)
			{
				StockedEvent.Send(this.ParentObject, text);
				if (Restock && !Silent)
				{
					GameObject player = The.Player;
					if (player != null && player.InSameZone(this.ParentObject))
					{
						Messaging.XDidY(this.ParentObject, "have", "restocked " + this.ParentObject.its + " inventory", "!", null, null, The.Player, null, true, false, null, null, false, false, true, false, false, null);
					}
				}
			}
		}

		// Token: 0x06009749 RID: 38729 RVA: 0x0036E3A8 File Offset: 0x0036C5A8
		public void PerformRestock(bool Silent = false)
		{
			Inventory inventory = this.ParentObject.Inventory;
			List<GameObject> list = Event.NewGameObjectList();
			list.AddRange(inventory.Objects);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject = list[i];
				if (gameObject.HasProperty("_stock") && !gameObject.HasPropertyOrTag("norestock") && !gameObject.IsImportant())
				{
					inventory.RemoveObject(gameObject, null);
					gameObject.Obliterate(null, false, null);
				}
				i++;
			}
			this.PerformStock(this.LastRestockTick != 0L, Silent);
		}

		// Token: 0x0600974A RID: 38730 RVA: 0x0036E439 File Offset: 0x0036C639
		public void Clear()
		{
			this.Tables = null;
			this.HeroTables = null;
			this.LastRestockTick = 0L;
		}

		// Token: 0x0600974B RID: 38731 RVA: 0x0036E451 File Offset: 0x0036C651
		public void AddTable(string Table)
		{
			if (this.Tables == null)
			{
				this.Tables = new List<string>();
			}
			this.Tables.Add(Table);
		}

		// Token: 0x0600974C RID: 38732 RVA: 0x0036E472 File Offset: 0x0036C672
		public void AddHeroTable(string Table)
		{
			if (this.HeroTables == null)
			{
				this.HeroTables = new List<string>();
			}
			this.HeroTables.Add(Table);
		}

		// Token: 0x0600974D RID: 38733 RVA: 0x0036E494 File Offset: 0x0036C694
		private void SetTables(ref List<string> List, string Value)
		{
			if (Value.IsNullOrEmpty())
			{
				List = null;
				return;
			}
			if (List == null)
			{
				List = new List<string>();
			}
			else
			{
				List.Clear();
			}
			foreach (ReadOnlySpan<char> readOnlySpan in Value.DelimitedBy(','))
			{
				if (readOnlySpan.Length == Value.Length)
				{
					List.Add(Value);
					return;
				}
				List.Add(new string(readOnlySpan));
			}
		}

		// Token: 0x0600974E RID: 38734 RVA: 0x0036E50A File Offset: 0x0036C70A
		public override bool FireEvent(Event E)
		{
			if (E.ID == "MadeHero")
			{
				this.PerformRestock(true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003EE5 RID: 16101
		public long LastRestockTick;

		// Token: 0x04003EE6 RID: 16102
		public long RestockFrequency = 6000L;

		// Token: 0x04003EE7 RID: 16103
		public int Chance = 100;

		// Token: 0x04003EE8 RID: 16104
		public List<string> Tables;

		// Token: 0x04003EE9 RID: 16105
		public List<string> HeroTables;
	}
}
