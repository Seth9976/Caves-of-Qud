using System;
using System.Linq;
using XRL.Rules;

namespace XRL.World.Biomes
{
	// Token: 0x02000A1D RID: 2589
	public class IBiome
	{
		// Token: 0x06006D45 RID: 27973 RVA: 0x00249AF8 File Offset: 0x00247CF8
		public virtual int GetBiomeValue(string ZoneID)
		{
			return Stat.SeededRandom("BiomeValue" + ZoneID, 0, 3);
		}

		// Token: 0x06006D46 RID: 27974 RVA: 0x00249B0C File Offset: 0x00247D0C
		public virtual string MutateZoneName(string Name, string ZoneID, int NameOrder)
		{
			return Name;
		}

		// Token: 0x06006D47 RID: 27975 RVA: 0x00249B10 File Offset: 0x00247D10
		protected static string MutateZoneNameWith(string Name, int Which, string Name1, string Adjective1, string Name2, string Adjective2, string Name3, string Adjective3)
		{
			if (string.IsNullOrEmpty(Name) || Name == "surface")
			{
				switch (Which)
				{
				case 1:
					return Name1;
				case 2:
					return Name2;
				case 3:
					return Name3;
				}
			}
			else
			{
				string text = null;
				switch (Which)
				{
				case 1:
					text = Adjective1;
					break;
				case 2:
					text = Adjective2;
					break;
				case 3:
					text = Adjective3;
					break;
				}
				if (!text.IsNullOrEmpty())
				{
					if (Name.StartsWith("some "))
					{
						return "some " + text + " " + Name.Substring(5);
					}
					if (Name.StartsWith("The "))
					{
						return "The " + text + " " + Name.Substring(4);
					}
					return text + " " + Name;
				}
				else
				{
					switch (Which)
					{
					case 1:
						return Name + " and " + Name1;
					case 2:
						return Name + " and " + Name2;
					case 3:
						return Name + " and " + Name3;
					}
				}
			}
			return Name;
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x00249C16 File Offset: 0x00247E16
		protected static string MutateZoneNameWith(string Name, int Which, string Name1, string Name2, string Name3)
		{
			return IBiome.MutateZoneNameWith(Name, Which, Name1, null, Name2, null, Name3, null);
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x00249C26 File Offset: 0x00247E26
		protected static string MutateZoneNameWith(string Base, string Name, string Adjective)
		{
			return IBiome.MutateZoneNameWith(Base, 1, Name, Adjective, null, null, null, null);
		}

		// Token: 0x06006D4A RID: 27978 RVA: 0x00249C35 File Offset: 0x00247E35
		public virtual GameObject MutateGameObject(GameObject GO, string ZoneID)
		{
			return GO;
		}

		// Token: 0x06006D4B RID: 27979 RVA: 0x00249C38 File Offset: 0x00247E38
		public virtual void MutateZone(Zone Z)
		{
		}

		// Token: 0x06006D4C RID: 27980 RVA: 0x00249C3A File Offset: 0x00247E3A
		public virtual bool IsNotable(string ZoneID)
		{
			return this.GetBiomeValue(ZoneID) >= 3 && BiomeManager.GetTopBiomes(ZoneID, 2).Any<IBiome>((IBiome b) => b.GetType() == base.GetType());
		}
	}
}
