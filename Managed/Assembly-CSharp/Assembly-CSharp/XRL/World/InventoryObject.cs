using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200094C RID: 2380
	public class InventoryObject
	{
		// Token: 0x0600613E RID: 24894 RVA: 0x001DA359 File Offset: 0x001D8559
		private InventoryObject()
		{
		}

		// Token: 0x0600613F RID: 24895 RVA: 0x001DA380 File Offset: 0x001D8580
		public InventoryObject(string Blueprint, string Number, int Chance, int SetMods, bool BoostModChance, bool NoEquip, bool NoSell, bool NotReal, bool Full, int? CellChance, int? CellFullChance, string CellType, string AutoMod, Dictionary<string, string> StringProperties, Dictionary<string, int> IntProperties)
			: this()
		{
			this.Blueprint = Blueprint;
			this.Number = Number;
			this.Chance = Chance;
			this.SetMods = SetMods;
			this.BoostModChance = BoostModChance;
			this.NoEquip = NoEquip;
			this.NoSell = NoSell;
			this.NotReal = NotReal;
			this.Full = Full;
			this.CellChance = CellChance;
			this.CellFullChance = CellFullChance;
			this.CellType = CellType;
			this.AutoMod = AutoMod;
			this.StringProperties = StringProperties;
			this.IntProperties = IntProperties;
		}

		// Token: 0x06006140 RID: 24896 RVA: 0x001DA408 File Offset: 0x001D8608
		public InventoryObject(InventoryObject Source)
		{
			this.CopyFrom(Source);
		}

		// Token: 0x06006141 RID: 24897 RVA: 0x001DA438 File Offset: 0x001D8638
		public void CopyFrom(InventoryObject IO)
		{
			this.Blueprint = IO.Blueprint;
			this.Number = IO.Number;
			this.Chance = IO.Chance;
			this.SetMods = IO.SetMods;
			this.BoostModChance = IO.BoostModChance;
			this.NoEquip = IO.NoEquip;
			this.NoSell = IO.NoSell;
			this.NotReal = IO.NotReal;
			this.Full = IO.Full;
			this.CellChance = IO.CellChance;
			this.CellFullChance = IO.CellFullChance;
			this.CellType = IO.CellType;
			this.AutoMod = IO.AutoMod;
			this.StringProperties = IO.StringProperties;
			this.IntProperties = IO.IntProperties;
		}

		// Token: 0x06006142 RID: 24898 RVA: 0x001DA4F9 File Offset: 0x001D86F9
		public bool NeedsToPreconfigureObject()
		{
			return this.CellChance != null || this.CellFullChance != null || this.CellType != null || this.StringProperties != null || this.IntProperties != null;
		}

		// Token: 0x06006143 RID: 24899 RVA: 0x001DA530 File Offset: 0x001D8730
		public void PreconfigureObject(GameObject obj)
		{
			EnergyCellSocket energyCellSocket;
			if ((this.CellChance != null || this.CellFullChance != null || this.CellType != null) && obj.TryGetPart<EnergyCellSocket>(out energyCellSocket))
			{
				if (this.CellChance != null)
				{
					energyCellSocket.ChanceSlotted = this.CellChance.Value;
				}
				if (this.CellFullChance != null)
				{
					energyCellSocket.ChanceFullCell = this.CellFullChance.Value;
				}
				if (this.CellType != null)
				{
					energyCellSocket.SlottedType = this.CellType;
				}
			}
			if (this.StringProperties != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.StringProperties)
				{
					obj.SetStringProperty(keyValuePair.Key, keyValuePair.Value, false);
				}
			}
			if (this.IntProperties != null)
			{
				foreach (KeyValuePair<string, int> keyValuePair2 in this.IntProperties)
				{
					obj.SetIntProperty(keyValuePair2.Key, keyValuePair2.Value, false);
				}
			}
		}

		// Token: 0x06006144 RID: 24900 RVA: 0x001DA670 File Offset: 0x001D8870
		public override string ToString()
		{
			return this.Blueprint + " x" + this.Number;
		}

		// Token: 0x04002DC1 RID: 11713
		public string Blueprint = "";

		// Token: 0x04002DC2 RID: 11714
		public string Number = "1";

		// Token: 0x04002DC3 RID: 11715
		public int Chance = 100;

		// Token: 0x04002DC4 RID: 11716
		public int SetMods;

		// Token: 0x04002DC5 RID: 11717
		public bool BoostModChance;

		// Token: 0x04002DC6 RID: 11718
		public bool NoEquip;

		// Token: 0x04002DC7 RID: 11719
		public bool NoSell;

		// Token: 0x04002DC8 RID: 11720
		public bool NotReal;

		// Token: 0x04002DC9 RID: 11721
		public bool Full;

		// Token: 0x04002DCA RID: 11722
		public int? CellChance;

		// Token: 0x04002DCB RID: 11723
		public int? CellFullChance;

		// Token: 0x04002DCC RID: 11724
		public string CellType;

		// Token: 0x04002DCD RID: 11725
		public string AutoMod;

		// Token: 0x04002DCE RID: 11726
		public Dictionary<string, string> StringProperties;

		// Token: 0x04002DCF RID: 11727
		public Dictionary<string, int> IntProperties;
	}
}
