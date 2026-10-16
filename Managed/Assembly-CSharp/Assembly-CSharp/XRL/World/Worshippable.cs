using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Occult.Engine.CodeGeneration;

namespace XRL.World
{
	// Token: 0x02000855 RID: 2133
	[GenerateSerializationPartial]
	[Serializable]
	public class Worshippable : IComposite
	{
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x00197C9E File Offset: 0x00195E9E
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600552E RID: 21806 RVA: 0x00197CA1 File Offset: 0x00195EA1
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this.Faction);
			Writer.WriteOptimized(this.Sources);
			Writer.WriteOptimized(this.Blueprints);
			Writer.WriteOptimized(this.Power);
		}

		// Token: 0x0600552F RID: 21807 RVA: 0x00197CDF File Offset: 0x00195EDF
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.Name = Reader.ReadOptimizedString();
			this.Faction = Reader.ReadOptimizedString();
			this.Sources = Reader.ReadOptimizedString();
			this.Blueprints = Reader.ReadOptimizedString();
			this.Power = Reader.ReadOptimizedInt32();
		}

		// Token: 0x06005531 RID: 21809 RVA: 0x00197D28 File Offset: 0x00195F28
		public int GetRelevance(GameObject Object = null, int Base = 0)
		{
			int num = Base + this.Power.DiminishingReturns(1.0);
			if (((Object != null) ? Object.GetPrimaryFaction(false) : null) == this.Faction)
			{
				num *= 2;
				num += 10;
			}
			return num;
		}

		// Token: 0x06005532 RID: 21810 RVA: 0x00197D70 File Offset: 0x00195F70
		public int GetCounterRelevance(GameObject Object = null, int Base = 0)
		{
			int num = Base + this.Power.DiminishingReturns(1.0);
			string text = ((Object != null) ? Object.GetPrimaryFaction(false) : null);
			if (!text.IsNullOrEmpty())
			{
				int factionWorshipAttitude = Factions.Get(text).GetFactionWorshipAttitude(this.Faction);
				if (factionWorshipAttitude < 0)
				{
					num = num * (100 - factionWorshipAttitude) / 100;
					num += factionWorshipAttitude / -10;
				}
			}
			return num;
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x00197DD4 File Offset: 0x00195FD4
		public static int Sort(Worshippable A, Worshippable B)
		{
			int num = A.Power.CompareTo(B.Power);
			if (num != 0)
			{
				return -num;
			}
			int num2 = A.Name.CompareTo(B.Name);
			if (num2 != 0)
			{
				return num2;
			}
			return A.Faction.CompareTo(B.Faction);
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x00197E24 File Offset: 0x00196024
		public static bool Multiple(List<Worshippable> List, string Name)
		{
			if (List == null)
			{
				return false;
			}
			bool flag = false;
			using (List<Worshippable>.Enumerator enumerator = List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name == Name)
					{
						if (flag)
						{
							return true;
						}
						flag = true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005535 RID: 21813 RVA: 0x00197E8C File Offset: 0x0019608C
		public static bool Multiple(List<Worshippable> List, Worshippable Being)
		{
			return Worshippable.Multiple(List, Being.Name);
		}

		// Token: 0x06005536 RID: 21814 RVA: 0x00197E9C File Offset: 0x0019609C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Name: ",
				this.Name,
				"\nFaction: ",
				this.Faction,
				"\nSources: ",
				this.Sources,
				"\nBlueprints: ",
				this.Blueprints,
				"\nPower: ",
				this.Power.ToString()
			});
		}

		// Token: 0x04002AD6 RID: 10966
		public string Name;

		// Token: 0x04002AD7 RID: 10967
		public string Faction;

		// Token: 0x04002AD8 RID: 10968
		public string Sources;

		// Token: 0x04002AD9 RID: 10969
		public string Blueprints;

		// Token: 0x04002ADA RID: 10970
		public int Power;
	}
}
