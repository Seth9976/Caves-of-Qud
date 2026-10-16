using System;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	// Token: 0x020010A2 RID: 4258
	[Serializable]
	public class StrideMason : IPoweredPart
	{
		// Token: 0x0600A97B RID: 43387 RVA: 0x003DF6C0 File Offset: 0x003DD8C0
		public StrideMason()
		{
			this.ChargeUse = 10;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A97C RID: 43388 RVA: 0x003DF6EC File Offset: 0x003DD8EC
		public bool IsReady(bool UseCharge = false)
		{
			return this.Blueprint != null && base.IsReady(UseCharge, false, false, false, false, false, false, false, false, false, 1, new int?(this.GetChargeUse()), false, 0L, null);
		}

		// Token: 0x0600A97D RID: 43389 RVA: 0x003DF72B File Offset: 0x003DD92B
		public bool IsValidCell(Cell C)
		{
			return C != null && !C.OnWorldMap() && C.IsEmpty();
		}

		// Token: 0x0600A97E RID: 43390 RVA: 0x003DF740 File Offset: 0x003DD940
		public bool IsValidWall(GameObject Wall)
		{
			return Wall != null && Wall.IsWall() && !Wall.HasTag("Plant") && !Wall.HasTag("PlantLike") && !Wall.IsTemporary && Wall.Physics.IsReal && !Wall.HasPart<Forcefield>();
		}

		// Token: 0x0600A97F RID: 43391 RVA: 0x003DF792 File Offset: 0x003DD992
		public bool IsImitable(GameObject Wall)
		{
			return Wall.Render != null && !Wall.HasProperName && (Wall.HasIntProperty("ForceMutableSave") || !Wall.HasTag("Immutable")) && !Wall.HasTagOrProperty("QuestItem");
		}

		// Token: 0x0600A980 RID: 43392 RVA: 0x003DF7D0 File Offset: 0x003DD9D0
		public void ApplyRenderable(GameObject Wall)
		{
			Render render = Wall.Render;
			render.DisplayName = this.DisplayName ?? render.DisplayName;
			render.Tile = this.Renderable.Tile ?? render.Tile;
			render.RenderString = this.Renderable.RenderString ?? render.RenderString;
			render.ColorString = this.Renderable.ColorString ?? render.ColorString;
			render.TileColor = this.Renderable.TileColor ?? render.TileColor;
			if (this.Renderable.DetailColor != '\0')
			{
				render.DetailColor = this.Renderable.DetailColor.ToString();
			}
			Description description;
			if (Wall.TryGetPart<Description>(out description))
			{
				description.Short = this.Description ?? description._Short;
			}
			if (Wall.HasTag("Immutable"))
			{
				Wall.SetIntProperty("ForceMutableSave", 1, false);
			}
		}

		// Token: 0x0600A981 RID: 43393 RVA: 0x003DF8C4 File Offset: 0x003DDAC4
		public int GetChargeUse()
		{
			if (!this.DynamicCharge)
			{
				return this.ChargeUse;
			}
			GameObjectBlueprint gameObjectBlueprint;
			if (this.BlueprintChargeUse == -1 && GameObjectFactory.Factory.Blueprints.TryGetValue(this.Blueprint, out gameObjectBlueprint))
			{
				this.BlueprintChargeUse = gameObjectBlueprint.Stat("AV", 1) * gameObjectBlueprint.Stat("Hitpoints", 1) / 500;
			}
			return Math.Max(this.ChargeUse, this.BlueprintChargeUse);
		}

		// Token: 0x0600A982 RID: 43394 RVA: 0x003DF938 File Offset: 0x003DDB38
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A983 RID: 43395 RVA: 0x003DF93B File Offset: 0x003DDB3B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A984 RID: 43396 RVA: 0x003DF969 File Offset: 0x003DDB69
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, LeftCellEvent.ID, 0, true);
			if (this.Source == "Look")
			{
				E.Actor.RegisterPartEvent(this, "LookedAt");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A985 RID: 43397 RVA: 0x003DF9A8 File Offset: 0x003DDBA8
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, LeftCellEvent.ID);
			E.Actor.UnregisterPartEvent(this, "LookedAt");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A986 RID: 43398 RVA: 0x003DF9D3 File Offset: 0x003DDBD3
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A987 RID: 43399 RVA: 0x003DF9E9 File Offset: 0x003DDBE9
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A988 RID: 43400 RVA: 0x003DFA00 File Offset: 0x003DDC00
		public override bool HandleEvent(LeftCellEvent E)
		{
			if (this.IsValidCell(E.Cell) && this.IsReady(true))
			{
				E.Cell.AddObject(this.GenerateProduct(null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				E.Cell.SetReachable(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A989 RID: 43401 RVA: 0x003DFA59 File Offset: 0x003DDC59
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A98A RID: 43402 RVA: 0x003DFA5C File Offset: 0x003DDC5C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LookedAt" && this.Source == "Look")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (this.IsValidWall(gameObjectParameter) && this.IsReady(false))
				{
					this.Blueprint = gameObjectParameter.Blueprint;
					this.BlueprintChargeUse = -1;
					if (this.Imitate && this.IsImitable(gameObjectParameter))
					{
						this.DisplayName = gameObjectParameter.Render.DisplayName;
						Description part = gameObjectParameter.GetPart<Description>();
						this.Description = ((part != null) ? part._Short : null);
						if (this.Renderable == null)
						{
							this.Renderable = new Renderable();
						}
						this.Renderable.Copy(gameObjectParameter.Render);
					}
					else
					{
						this.DisplayName = null;
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A98B RID: 43403 RVA: 0x003DFB38 File Offset: 0x003DDD38
		private GameObject GenerateProduct(GameObject Actor = null)
		{
			GameObject gameObject = GameObject.Create(this.Blueprint);
			if (this.Imitate && this.DisplayName != null)
			{
				this.ApplyRenderable(gameObject);
			}
			return gameObject;
		}

		// Token: 0x0600A98C RID: 43404 RVA: 0x003DFB6C File Offset: 0x003DDD6C
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null))
			{
				for (int i = 0; i < 3; i++)
				{
					Cell currentCell = this.ParentObject.GetCurrentCell();
					Cell cell = ((currentCell != null) ? currentCell.GetRandomLocalAdjacentCell() : null);
					if (this.IsValidCell(cell))
					{
						GameObject gameObject = this.GenerateProduct(E.Actor);
						cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						base.DidXToY("extrude", gameObject, null, "!", null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, E.Actor.IsPlayer(), null);
						E.Identify = true;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040045E4 RID: 17892
		public const int BP_COST_DIVISOR = 500;

		// Token: 0x040045E5 RID: 17893
		public string Source;

		/// <summary>Calculate charge cost based on blueprint HP/AV.</summary>
		// Token: 0x040045E6 RID: 17894
		public bool DynamicCharge;

		/// <summary>Copy the appearance of the sourced object, not just its blueprint.</summary>
		// Token: 0x040045E7 RID: 17895
		public bool Imitate;

		// Token: 0x040045E8 RID: 17896
		public string Blueprint = "Sandstone";

		// Token: 0x040045E9 RID: 17897
		public string DisplayName;

		// Token: 0x040045EA RID: 17898
		public string Description;

		// Token: 0x040045EB RID: 17899
		public Renderable Renderable;

		// Token: 0x040045EC RID: 17900
		[NonSerialized]
		public int BlueprintChargeUse = -1;
	}
}
