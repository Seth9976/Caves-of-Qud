using System;
using Qud.API;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000FAE RID: 4014
	[Serializable]
	public class RandomStatue : IPart
	{
		// Token: 0x06009FAA RID: 40874 RVA: 0x003A059A File Offset: 0x0039E79A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009FAB RID: 40875 RVA: 0x003A059D File Offset: 0x0039E79D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x06009FAC RID: 40876 RVA: 0x003A05B4 File Offset: 0x0039E7B4
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			GameObject gameObject = ((this.BaseBlueprint == null) ? EncountersAPI.GetACreature(null) : GameObjectFactory.Factory.CreateObject(this.BaseBlueprint));
			this.SetCreature(gameObject);
			return base.HandleEvent(E);
		}

		// Token: 0x06009FAD RID: 40877 RVA: 0x003A05F0 File Offset: 0x0039E7F0
		public void SetCreature(GameObject creatureObject)
		{
			this.BaseBlueprint = creatureObject.Blueprint;
			if (!string.IsNullOrEmpty(this.StatueType))
			{
				this.ParentObject.Render.DisplayName = string.Concat(new string[]
				{
					Grammar.InitLower(this.Material),
					" ",
					this.StatueType,
					" of ",
					creatureObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, null, false, null, false)
				});
			}
			else
			{
				this.ParentObject.Render.DisplayName = Grammar.InitLower(this.Material) + " statue of " + creatureObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, null, false, null, false);
			}
			this.ParentObject.Render.RenderString = creatureObject.Render.RenderString;
			this.ParentObject.Render.Tile = creatureObject.Render.Tile;
			Description part = creatureObject.GetPart<Description>();
			Description part2 = this.ParentObject.GetPart<Description>();
			if (!string.IsNullOrEmpty(this.StatueType))
			{
				part2._Short = string.Concat(new string[]
				{
					"This ",
					this.StatueType,
					" worked from ",
					this.Material,
					" intricately depicts ",
					creatureObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, null, false, null, false),
					":\n\n",
					GameText.VariableReplace(part._Short, creatureObject, null, false)
				});
			}
			else
			{
				part2._Short = string.Concat(new string[]
				{
					"This statue worked from ",
					this.Material,
					" intricately depicts ",
					creatureObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, null, false, null, false),
					":\n\n",
					GameText.VariableReplace(part._Short, creatureObject, null, false)
				});
			}
			if (creatureObject.HasPart<Lovely>())
			{
				this.ParentObject.RequirePart<Lovely>(false);
				return;
			}
			this.ParentObject.RemovePart<Lovely>();
		}

		// Token: 0x04004244 RID: 16964
		public string Material = "Stone";

		// Token: 0x04004245 RID: 16965
		public string BaseBlueprint;

		// Token: 0x04004246 RID: 16966
		public string StatueType;
	}
}
