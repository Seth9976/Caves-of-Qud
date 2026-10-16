using System;
using System.Collections.Generic;
using Qud.API;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x02001085 RID: 4229
	[Serializable]
	public class RandomFigurine : IPart
	{
		// Token: 0x0600A84B RID: 43083 RVA: 0x003D8BCB File Offset: 0x003D6DCB
		public RandomFigurine()
		{
		}

		// Token: 0x0600A84C RID: 43084 RVA: 0x003D8BE5 File Offset: 0x003D6DE5
		public RandomFigurine(string Creature)
			: this()
		{
			this.Creature = Creature;
		}

		// Token: 0x0600A84D RID: 43085 RVA: 0x003D8BF4 File Offset: 0x003D6DF4
		public override bool SameAs(IPart p)
		{
			RandomFigurine randomFigurine = p as RandomFigurine;
			return !(randomFigurine.Material != this.Material) && !(randomFigurine.Creature != this.Creature) && randomFigurine.IncludeCreatureDescription == this.IncludeCreatureDescription && randomFigurine.IncludeCreatureColors == this.IncludeCreatureColors && base.SameAs(p);
		}

		// Token: 0x0600A84E RID: 43086 RVA: 0x003D8C59 File Offset: 0x003D6E59
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600A84F RID: 43087 RVA: 0x003D8C5C File Offset: 0x003D6E5C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A850 RID: 43088 RVA: 0x003D8C74 File Offset: 0x003D6E74
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			GameObject gameObject;
			if (this.Creature != null)
			{
				gameObject = GameObjectFactory.Factory.CreateSampleObject(this.Creature, null);
			}
			else
			{
				gameObject = EncountersAPI.GetASampleCreature((GameObjectBlueprint ob) => !ob.HasTag("NoFigurine", false));
			}
			GameObject gameObject2 = gameObject;
			this.Creature = gameObject2.Blueprint;
			this.ParentObject.Render.DisplayName = this.ParentObject.Render.DisplayName.Replace("*creature*", gameObject2.DisplayNameOnlyStripped);
			Description part = gameObject2.GetPart<Description>();
			Description part2 = this.ParentObject.GetPart<Description>();
			if (gameObject2.HasPart<Lovely>() && !this.ParentObject.HasPart<Lovely>())
			{
				this.ParentObject.AddPart<Lovely>(new Lovely(), true, false);
			}
			if (gameObject2.Brain == null)
			{
				Debug.LogWarning("had brainless creature " + gameObject2.Blueprint);
			}
			else
			{
				string text = "";
				foreach (KeyValuePair<string, int> keyValuePair in gameObject2.Brain.Allegiance)
				{
					string text2;
					int num;
					keyValuePair.Deconstruct(out text2, out num);
					string text3 = text2;
					text = text + text3 + ",";
				}
				text = text.TrimEnd(',');
				AddsRep.AddModifier(this.ParentObject, text, RandomFigurine.GetFigurineRepValue(this.Material));
			}
			part2._Short = part2._Short.Replace("*material*", this.Material).Replace("*creature.a*", gameObject2.a).Replace("*creature.an*", gameObject2.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false))
				.Replace("*creature*", gameObject2.DisplayNameOnlyStripped) + (this.IncludeCreatureDescription ? GameText.VariableReplace(part._Short, gameObject2, null, false) : "");
			if (this.IncludeCreatureColors)
			{
				string foregroundColor = gameObject2.GetForegroundColor();
				string detailColor = gameObject2.GetDetailColor();
				if (foregroundColor == detailColor)
				{
					this.ParentObject.DisplayName = string.Concat(new string[]
					{
						"{{",
						foregroundColor,
						"|",
						this.ParentObject.DisplayNameOnlyDirect,
						"}}"
					});
				}
				else
				{
					this.ParentObject.DisplayName = string.Concat(new string[]
					{
						"{{",
						foregroundColor,
						"-",
						foregroundColor,
						"-",
						foregroundColor,
						"-",
						detailColor,
						" sequence|",
						this.ParentObject.DisplayNameOnlyDirect,
						"}}"
					});
				}
				this.ParentObject.SetForegroundColor(foregroundColor);
				this.ParentObject.SetDetailColor(detailColor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A851 RID: 43089 RVA: 0x003D8F64 File Offset: 0x003D7164
		public static int GetFigurineRepValue(string material)
		{
			if (material == "copper")
			{
				return RandomFigurine.COPPER_REP_VALUE;
			}
			if (material == "silver")
			{
				return RandomFigurine.SILVER_REP_VALUE;
			}
			if (material == "gold")
			{
				return RandomFigurine.GOLD_REP_VALUE;
			}
			if (material == "agate")
			{
				return RandomFigurine.AGATE_REP_VALUE;
			}
			if (material == "topaz")
			{
				return RandomFigurine.TOPAZ_REP_VALUE;
			}
			if (material == "jasper")
			{
				return RandomFigurine.JASPER_REP_VALUE;
			}
			if (material == "amethyst")
			{
				return RandomFigurine.AMETHYST_REP_VALUE;
			}
			if (material == "sapphire")
			{
				return RandomFigurine.SAPPHIRE_REP_VALUE;
			}
			if (material == "emerald")
			{
				return RandomFigurine.EMERALD_REP_VALUE;
			}
			if (material == "peridot")
			{
				return RandomFigurine.PERIDOT_REP_VALUE;
			}
			return RandomFigurine.DEFAULT_REP_VALUE;
		}

		// Token: 0x0400457C RID: 17788
		public string Material = "copper";

		// Token: 0x0400457D RID: 17789
		public string Creature;

		// Token: 0x0400457E RID: 17790
		public bool IncludeCreatureDescription = true;

		// Token: 0x0400457F RID: 17791
		public bool IncludeCreatureColors;

		// Token: 0x04004580 RID: 17792
		public static readonly int DEFAULT_REP_VALUE = 60;

		// Token: 0x04004581 RID: 17793
		public static readonly int COPPER_REP_VALUE = 60;

		// Token: 0x04004582 RID: 17794
		public static readonly int SILVER_REP_VALUE = 100;

		// Token: 0x04004583 RID: 17795
		public static readonly int GOLD_REP_VALUE = 200;

		// Token: 0x04004584 RID: 17796
		public static readonly int AGATE_REP_VALUE = 100;

		// Token: 0x04004585 RID: 17797
		public static readonly int TOPAZ_REP_VALUE = 150;

		// Token: 0x04004586 RID: 17798
		public static readonly int JASPER_REP_VALUE = 200;

		// Token: 0x04004587 RID: 17799
		public static readonly int AMETHYST_REP_VALUE = 250;

		// Token: 0x04004588 RID: 17800
		public static readonly int SAPPHIRE_REP_VALUE = 300;

		// Token: 0x04004589 RID: 17801
		public static readonly int EMERALD_REP_VALUE = 350;

		// Token: 0x0400458A RID: 17802
		public static readonly int PERIDOT_REP_VALUE = 350;
	}
}
