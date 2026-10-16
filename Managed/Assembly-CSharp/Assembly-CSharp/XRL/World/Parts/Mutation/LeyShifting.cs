using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013EF RID: 5103
	[Serializable]
	public class LeyShifting : BaseMutation
	{
		// Token: 0x0600C57C RID: 50556 RVA: 0x0047E0A4 File Offset: 0x0047C2A4
		public LeyShifting()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C57D RID: 50557 RVA: 0x0047E0FC File Offset: 0x0047C2FC
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C57E RID: 50558 RVA: 0x0047E0FF File Offset: 0x0047C2FF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C57F RID: 50559 RVA: 0x0047E102 File Offset: 0x0047C302
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C580 RID: 50560 RVA: 0x0047E11F File Offset: 0x0047C31F
		public int GetCooldown(int Level)
		{
			return 250;
		}

		// Token: 0x0600C581 RID: 50561 RVA: 0x0047E126 File Offset: 0x0047C326
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || (ID == BeforeRenderEvent.ID && this.Active) || ID == PooledEvent<CommandEvent>.ID || ID == GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x0600C582 RID: 50562 RVA: 0x0047E15C File Offset: 0x0047C35C
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Ley Shift", "CommandLeyShift", 25000, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C583 RID: 50563 RVA: 0x0047E188 File Offset: 0x0047C388
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 2 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add("CommandLeyShift", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C584 RID: 50564 RVA: 0x0047E1BC File Offset: 0x0047C3BC
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.Active)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				currentCell.ParentZone.AddLight(currentCell.X, currentCell.Y, 1);
				currentCell.ParentZone.AddVisibility(currentCell.X, currentCell.Y, 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C585 RID: 50565 RVA: 0x0047E214 File Offset: 0x0047C414
		public override bool HandleEvent(CommandEvent E)
		{
			if (!(E.Command == "CommandLeyShift"))
			{
				return base.HandleEvent(E);
			}
			if (this.ParentObject.OnWorldMap())
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("You cannot do that on the world map.", true, true, true);
				}
				return false;
			}
			string text;
			int num;
			if (!this.TryGetDestination(out text, out num))
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("You cannot do that here.", true, true, true);
				}
				return false;
			}
			Event.PinCurrentPool();
			bool useOverlayCombatEffects = Options.UseOverlayCombatEffects;
			int renderLayer = this.ParentObject.Render.RenderLayer;
			this.ParentObject.Render.RenderLayer = 100;
			this.Active = true;
			base.DidX("shift", "spacetime in the local region", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			if (useOverlayCombatEffects)
			{
				CombatJuice.Hover(this.ParentObject.CurrentCell.Location, (float)num * 0.3f + 1f, 2f);
				The.Core.RenderDelay(2000, false, true, false);
			}
			GameManager.Instance.Fuzzing = true;
			for (int i = 1; i <= num; i++)
			{
				this.Shift(text);
				SoundManager.PlaySound((i == num) ? "sfx_statusEffect_spacetimeWeirdness" : "sfx_ability_leyShift_repetition", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				if (useOverlayCombatEffects)
				{
					CombatJuice.cameraShake((i == num) ? 2f : 0.3f, false);
				}
				The.Core.RenderDelay(300, false, true, false);
				Event.ResetToPin();
				MinEvent.ResetPools();
			}
			if (useOverlayCombatEffects)
			{
				The.Core.RenderDelay(3000, true, true, false);
				CombatJuice.finishAll();
			}
			this.ParentObject.Render.RenderLayer = renderLayer;
			this.Active = false;
			base.UseEnergy(1000, "Mental Mutation LeyShifting");
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 250, this.ParentObject, null, false);
			return false;
		}

		// Token: 0x0600C586 RID: 50566 RVA: 0x0047E408 File Offset: 0x0047C608
		public bool TryGetDestination(out string Direction, out int Amount)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			Zone parentZone = currentCell.ParentZone;
			int num = 100;
			bool flag;
			do
			{
				Amount = Stat.Random(8, 12) + 1;
				Direction = Location2D.Directions.GetRandomElement<string>(null);
				Point2D point2D = Point2D.zero.FromDirection(Direction);
				Cell cell = parentZone.GetCell(this.WrapX(parentZone, currentCell.X - point2D.x * Amount), this.WrapY(parentZone, currentCell.Y - point2D.y * Amount));
				flag = cell != null && ((num > 50) ? cell.IsEmptyAtRenderLayer(1) : cell.IsEmpty());
			}
			while (!flag && num-- > 0);
			return flag;
		}

		// Token: 0x0600C587 RID: 50567 RVA: 0x0047E4BC File Offset: 0x0047C6BC
		public override string GetDescription()
		{
			return "" + "You shift spacetime in the local region.\n\n" + "Cooldown: " + 250.ToString();
		}

		// Token: 0x0600C588 RID: 50568 RVA: 0x0047E4EF File Offset: 0x0047C6EF
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C589 RID: 50569 RVA: 0x0047E4F8 File Offset: 0x0047C6F8
		public void Shift(string Direction)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			Point2D point2D = Point2D.zero.FromDirection(Direction);
			int i = currentZone.Width * currentZone.Height;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (point2D.x == 0 && point2D.y != 0)
			{
				num2 = 1;
			}
			else
			{
				num4 = 1;
			}
			while (i > 0)
			{
				int num5 = num;
				int num6 = num3;
				do
				{
					Cell cell = currentZone.Map[num][num3];
					this.TakeObjects(cell);
					this.TakePaint(cell);
					this.ApplyObjects(cell);
					this.ApplyPaint(cell);
					num = this.WrapX(currentZone, num + point2D.x);
					num3 = this.WrapY(currentZone, num3 + point2D.y);
					i--;
				}
				while (num != num5 || num3 != num6);
				Cell cell2 = currentZone.Map[num5][num6];
				this.ApplyObjects(cell2);
				this.ApplyPaint(cell2);
				num += num2;
				num3 += num4;
			}
			ZoneManager.PaintWalls(currentZone, 0, 0, -1, -1);
			ZoneManager.PaintWater(currentZone, 0, 0, -1, -1);
		}

		// Token: 0x0600C58A RID: 50570 RVA: 0x0047E600 File Offset: 0x0047C800
		public void TakeObjects(Cell C)
		{
			for (int i = C.Objects.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = C.Objects[i];
				if (gameObject != this.ParentObject && (gameObject.IsReal || (gameObject.Render != null && gameObject.Render.Visible)))
				{
					this.ShiftList.Add(C.Objects[i]);
					C.RemoveObject(C.Objects[i], false, false, false, false, false, false, true, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x0600C58B RID: 50571 RVA: 0x0047E690 File Offset: 0x0047C890
		public void ApplyObjects(Cell C)
		{
			for (int i = this.ShiftList2.Count - 1; i >= 0; i--)
			{
				C.AddObject(this.ShiftList2[i], false, false, false, false, false, false, true, null, null, null, null, null, null, null, null);
			}
			List<GameObject> shiftList = this.ShiftList;
			this.ShiftList = this.ShiftList2;
			this.ShiftList2 = shiftList;
			this.ShiftList.Clear();
		}

		// Token: 0x0600C58C RID: 50572 RVA: 0x0047E6FC File Offset: 0x0047C8FC
		public void ApplyPaint(Cell C)
		{
			if (this.ShiftPaint.RenderString != null)
			{
				C.PaintTile = this.ShiftPaint.Tile;
				C.PaintRenderString = this.ShiftPaint.RenderString;
				C.PaintColorString = this.ShiftPaint.ColorString;
				C.PaintTileColor = this.ShiftPaint.TileColor;
				C.PaintDetailColor = ((this.ShiftPaint.DetailColor == '\0') ? null : this.ShiftPaint.DetailColor.ToString());
				this.ShiftPaint.RenderString = null;
			}
			Renderable shiftPaint = this.ShiftPaint;
			this.ShiftPaint = this.ShiftPaint2;
			this.ShiftPaint2 = shiftPaint;
		}

		// Token: 0x0600C58D RID: 50573 RVA: 0x0047E7A8 File Offset: 0x0047C9A8
		public void TakePaint(Cell C)
		{
			this.ShiftPaint2.Tile = C.PaintTile;
			this.ShiftPaint2.RenderString = C.PaintRenderString;
			this.ShiftPaint2.ColorString = C.PaintColorString;
			this.ShiftPaint2.TileColor = C.PaintTileColor;
			this.ShiftPaint2.DetailColor = (string.IsNullOrEmpty(C.PaintDetailColor) ? '\0' : C.PaintDetailColor[0]);
		}

		// Token: 0x0600C58E RID: 50574 RVA: 0x0047E820 File Offset: 0x0047CA20
		public int WrapX(Zone Z, int X)
		{
			while (X >= Z.Width)
			{
				X -= Z.Width;
			}
			while (X < 0)
			{
				X += Z.Width;
			}
			return X;
		}

		// Token: 0x0600C58F RID: 50575 RVA: 0x0047E848 File Offset: 0x0047CA48
		public int WrapY(Zone Z, int Y)
		{
			while (Y >= Z.Height)
			{
				Y -= Z.Height;
			}
			while (Y < 0)
			{
				Y += Z.Height;
			}
			return Y;
		}

		// Token: 0x0600C590 RID: 50576 RVA: 0x0047E870 File Offset: 0x0047CA70
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C591 RID: 50577 RVA: 0x0047E87C File Offset: 0x0047CA7C
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Ley Shift", "CommandLeyShift", "Mental Mutations", null, "\u00a8", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C592 RID: 50578 RVA: 0x0047E8C5 File Offset: 0x0047CAC5
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DFB RID: 19963
		public const string COMMAND_NAME = "CommandLeyShift";

		// Token: 0x04004DFC RID: 19964
		public const int ABL_CLD = 250;

		// Token: 0x04004DFD RID: 19965
		[NonSerialized]
		private bool Active;

		// Token: 0x04004DFE RID: 19966
		[NonSerialized]
		private List<GameObject> ShiftList = new List<GameObject>();

		// Token: 0x04004DFF RID: 19967
		[NonSerialized]
		private List<GameObject> ShiftList2 = new List<GameObject>();

		// Token: 0x04004E00 RID: 19968
		[NonSerialized]
		private Renderable ShiftPaint = new Renderable
		{
			RenderString = null
		};

		// Token: 0x04004E01 RID: 19969
		[NonSerialized]
		private Renderable ShiftPaint2 = new Renderable
		{
			RenderString = null
		};
	}
}
