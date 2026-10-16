using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x0200114A RID: 4426
	[Serializable]
	public class ModJewelEncrusted : IModification
	{
		// Token: 0x0600AFA4 RID: 44964 RVA: 0x0040BA48 File Offset: 0x00409C48
		public ModJewelEncrusted()
		{
		}

		// Token: 0x0600AFA5 RID: 44965 RVA: 0x0040BA63 File Offset: 0x00409C63
		public ModJewelEncrusted(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AFA6 RID: 44966 RVA: 0x0040BA7F File Offset: 0x00409C7F
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AFA7 RID: 44967 RVA: 0x0040BA88 File Offset: 0x00409C88
		public override void ApplyModification(GameObject Object)
		{
			AddsRep.AddModifier(Object, "Water:" + this.Amount.ToString() + ":hidden");
		}

		// Token: 0x0600AFA8 RID: 44968 RVA: 0x0040BAAC File Offset: 0x00409CAC
		public override bool SameAs(IPart p)
		{
			ModJewelEncrusted modJewelEncrusted = p as ModJewelEncrusted;
			return modJewelEncrusted.Amount == this.Amount && !(modJewelEncrusted.adjColored != this.adjColored) && modJewelEncrusted.bNamed == this.bNamed && base.SameAs(p);
		}

		// Token: 0x0600AFA9 RID: 44969 RVA: 0x0040BAFC File Offset: 0x00409CFC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AFAA RID: 44970 RVA: 0x0040BB24 File Offset: 0x00409D24
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				string text = "jewel-encrusted";
				if (!this.bNamed)
				{
					List<char> list = new List<char>
					{
						'b', 'B', 'g', 'G', 'W', 'r', 'R', 'm', 'M', 'c',
						'C'
					};
					char c = 'y';
					char c2 = 'y';
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("{{");
					for (int i = 0; i < 5; i++)
					{
						while (c2 == c)
						{
							c2 = list.GetRandomElement<char>(null);
						}
						if (i > 0)
						{
							stringBuilder.Append('-');
						}
						stringBuilder.Append(c2);
						c = c2;
					}
					stringBuilder.Append("-y");
					for (int j = 6; j < 15; j++)
					{
						while (c2 == c)
						{
							c2 = list.GetRandomElement<char>(null);
						}
						stringBuilder.Append('-').Append(c2);
						c = c2;
					}
					stringBuilder.Append(" sequence|").Append(text).Append("}}");
					this.adjColored = stringBuilder.ToString();
					this.bNamed = true;
				}
				E.AddAdjective(this.adjColored, -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFAB RID: 44971 RVA: 0x0040BC8B File Offset: 0x00409E8B
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFAC RID: 44972 RVA: 0x0040BCA6 File Offset: 0x00409EA6
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("jewels", 10);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AFAD RID: 44973 RVA: 0x0040BCCA File Offset: 0x00409ECA
		public string GetInstanceDescription()
		{
			return "Jewel-Encrusted: This item is much more valuable than usual and grants the wearer " + this.Amount.Signed(false) + " reputation with water barons.";
		}

		// Token: 0x04004801 RID: 18433
		public int Amount = 100;

		// Token: 0x04004802 RID: 18434
		public string adjColored = "";

		// Token: 0x04004803 RID: 18435
		public bool bNamed;
	}
}
