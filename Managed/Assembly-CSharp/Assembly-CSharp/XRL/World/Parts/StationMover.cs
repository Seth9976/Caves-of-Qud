using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x020012AF RID: 4783
	[Serializable]
	public class StationMover : SoundRendererPart
	{
		// Token: 0x0600BCFB RID: 48379 RVA: 0x00452BB8 File Offset: 0x00450DB8
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteringCellEvent.ID || ID == EnteredCellEvent.ID || ID == EnteringZoneEvent.ID || ID == AfterMoveFailedEvent.ID || ID == PooledEvent<AfterPilotChangeEvent>.ID || ID == ActorGetNavigationWeightEvent.ID || ID == PooledEvent<GetTransitiveLocationEvent>.ID;
		}

		// Token: 0x0600BCFC RID: 48380 RVA: 0x00452C0C File Offset: 0x00450E0C
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			bool flag = E.Origin.OnWorldMap();
			bool flag2 = E.Cell.OnWorldMap();
			if (this.ParentObject.IsVisible() && this.Animation == 0)
			{
				if (!flag && flag2)
				{
					this.Direction = StationMoverChute.GetAdjacentDirection(E.Origin);
					if (!this.Direction.IsNullOrEmpty())
					{
						this.Animation = 1;
						this.Destination = E.Cell;
						return false;
					}
				}
				else if (flag && !flag2)
				{
					this.Direction = StationMoverChute.GetAdjacentDirection(E.Cell);
					if (!this.Direction.IsNullOrEmpty())
					{
						this.Animation = 2;
						this.Destination = E.Cell;
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCFD RID: 48381 RVA: 0x00452CC4 File Offset: 0x00450EC4
		public override bool HandleEvent(AfterMoveFailedEvent E)
		{
			if (this.Animation != 0 && this.Destination != null)
			{
				if (this.Animation == 1)
				{
					Cell cell = ((this.Direction == "S") ? E.Origin.ParentZone.GetCell(E.Origin.X, 24) : E.Origin.ParentZone.GetCell(E.Origin.X, 0));
					this.Animate(E.Origin, cell);
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.PlayUISound("sfx_worldMap_enter", 1f, false, SoundRequest.SoundEffectType.None);
					}
				}
				else
				{
					Cell cell2 = ((this.Direction == "S") ? this.Destination.ParentZone.GetCell(this.Destination.X, 24) : this.Destination.ParentZone.GetCell(this.Destination.X, 0));
					this.Animate(cell2, this.Destination);
				}
				this.ParentObject.DirectMoveTo(this.Destination, 0, true, true, true, null);
				Vehicle vehicle;
				if (this.ParentObject.IsPlayer() && this.Animation == 2 && this.ParentObject.TryGetPart<Vehicle>(out vehicle))
				{
					vehicle.Pilot = null;
					CheckpointingSystem.ManualCheckpoint();
				}
				this.Destination = null;
				this.Animation = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCFE RID: 48382 RVA: 0x00452E24 File Offset: 0x00451024
		public void Animate(Cell Start, Cell End)
		{
			Zone parentZone = Start.ParentZone;
			Cell cell = Start;
			string directionFromCell = Start.GetDirectionFromCell(End, false);
			int num = Start.PathDistanceTo(End);
			bool flag = End.Y != 0 && End.Y != 24;
			int x = Start.X;
			int num2 = Start.Y;
			Render render = this.ParentObject.Render;
			try
			{
				if (flag)
				{
					base.PlayWorldSound("sfx_endgame_worldMap_spacestation_mover_arrive", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				else
				{
					base.PlayWorldSound("sfx_endgame_worldMap_spacestation_mover_launch", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					Acrobatics_Jump.Leap(Start, End, 6, 12);
				}
				for (int i = 0; i <= num; i++)
				{
					this.ParentObject.DirectMoveTo(cell, 0, true, true, true, null);
					this.BlipChute(parentZone.GetCell(x - 1, num2), 20);
					this.BlipChute(parentZone.GetCell(x + 1, num2), 20);
					if (!flag || i != num)
					{
						if (!The.Core.RenderDelay(flag ? Math.Min(20 + i * 5, 50) : Math.Max(150 - i * 20, 20), false, true, false))
						{
							return;
						}
						cell.TileParticleBlip(render.Tile, "&K", render.DetailColor, 20, false, render.HFlip, render.VFlip, 0L);
						if (directionFromCell == "N")
						{
							num2--;
						}
						else if (directionFromCell == "S")
						{
							num2++;
						}
						cell = parentZone.GetCell(x, num2);
					}
				}
				if (flag)
				{
					Acrobatics_Jump.Land(Start, End, 8, 8);
				}
				else
				{
					this.ParentObject.Render.Visible = false;
				}
				if (this.ParentObject.IsPlayer())
				{
					The.Core.RenderDelay(750, false, true, true);
				}
			}
			finally
			{
				this.ParentObject.Render.Visible = true;
			}
		}

		// Token: 0x0600BCFF RID: 48383 RVA: 0x00453050 File Offset: 0x00451250
		public void BlipChute(Cell Cell, int Duration)
		{
			GameObject gameObject = ((Cell != null) ? Cell.GetObjectWithTag("MoverChute") : null);
			if (gameObject != null)
			{
				Render render = gameObject.Render;
				string text = (render.TileColor.IsNullOrEmpty() ? render.ColorString : render.TileColor);
				gameObject.TileParticleBlip(render.Tile, text, "R", Duration, false, render.HFlip, render.VFlip, 0L);
			}
		}

		// Token: 0x0600BD00 RID: 48384 RVA: 0x004530B7 File Offset: 0x004512B7
		public override bool HandleEvent(EnteringCellEvent E)
		{
			return (E.Cell.HasObject(new Predicate<GameObject>(StationMover.HasTransitTag)) || E.Direction == null || Array.IndexOf<string>(Directions.DirectionList, E.Direction) == -1) && base.HandleEvent(E);
		}

		// Token: 0x0600BD01 RID: 48385 RVA: 0x004530F8 File Offset: 0x004512F8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Moving && E.Direction != null && this.ParentObject.IsPlayer() && Array.IndexOf<string>(Directions.DirectionList, E.Direction) != -1)
			{
				base.PlayWorldSound("sfx_endgame_worldMap_spacestation_mover_launch", 0.25f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				string text = this.GetSingularDirection(E.Cell, Directions.GetOppositeDirection(E.Direction));
				if (text == null)
				{
					return base.HandleEvent(E);
				}
				this.Moving = true;
				try
				{
					bool flag = false;
					while (!this.ParentObject.CurrentCell.HasObject(new Predicate<GameObject>(StationMover.IsMoverStop)) && The.Core.RenderDelay(150, true, true, false))
					{
						if (!this.ParentObject.Move(text, false, false, false, false, true, false, null, null, false, null, null, null, false, false, null, null, 0))
						{
							text = null;
						}
						else
						{
							flag = true;
							text = this.GetSingularDirection(this.ParentObject.CurrentCell, Directions.GetOppositeDirection(text));
						}
						if (text == null)
						{
							break;
						}
					}
					if (flag)
					{
						base.PlayWorldSound("sfx_endgame_worldMap_spacestation_mover_arrive", 0.25f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					}
				}
				finally
				{
					this.Moving = false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD02 RID: 48386 RVA: 0x0045326C File Offset: 0x0045146C
		public override bool HandleEvent(AfterPilotChangeEvent E)
		{
			if (E.NewPilot != null && E.Vehicle == this.ParentObject && E.Vehicle.IsPlayer() && !E.Vehicle.OnWorldMap())
			{
				Zone currentZone = E.Vehicle.CurrentZone;
				Cell cell = ((currentZone != null) ? currentZone.GetWorldCell() : null);
				if (cell != null)
				{
					E.Vehicle.DirectMoveTo(cell, 0, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD03 RID: 48387 RVA: 0x004532DD File Offset: 0x004514DD
		public override bool HandleEvent(ActorGetNavigationWeightEvent E)
		{
			if (E.Weight < 100 && !E.Cell.HasObject(new Predicate<GameObject>(StationMover.HasTransitTag)))
			{
				E.Weight = 100;
				E.Uncacheable = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD04 RID: 48388 RVA: 0x00453318 File Offset: 0x00451518
		public override bool HandleEvent(GetTransitiveLocationEvent E)
		{
			if (E.IsEgress && E.Zone == this.ParentObject.CurrentZone)
			{
				Cell origin = E.Origin;
				InteriorZone interiorZone = ((origin != null) ? origin.ParentZone : null) as InteriorZone;
				if (interiorZone != null && interiorZone.ParentObject == this.ParentObject)
				{
					Cell currentCell = this.ParentObject.CurrentCell;
					string adjacentDirection = StationMoverChute.GetAdjacentDirection(currentCell);
					if (!adjacentDirection.IsNullOrEmpty())
					{
						string oppositeDirection = Directions.GetOppositeDirection(adjacentDirection);
						Cell cell = currentCell.GetCellFromDirection(oppositeDirection, true);
						if (cell.IsPassable(E.Actor, true))
						{
							E.AddLocation(cell, this.ParentObject, 750);
						}
						foreach (string text in Directions.GetAdjacentDirections(oppositeDirection))
						{
							cell = currentCell.GetCellFromDirection(text, true);
							if (cell.IsPassable(E.Actor, true))
							{
								E.AddLocation(cell, this.ParentObject, 700);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD05 RID: 48389 RVA: 0x0045341E File Offset: 0x0045161E
		public static bool IsMoverStop(GameObject Object)
		{
			return Object.GetBlueprint(true).DescendsFrom("TerrainMoverStop");
		}

		// Token: 0x0600BD06 RID: 48390 RVA: 0x00453431 File Offset: 0x00451631
		public static bool HasTransitTag(GameObject Object)
		{
			return Object.HasTagOrIntProperty("MoverTransit");
		}

		// Token: 0x0600BD07 RID: 48391 RVA: 0x00453440 File Offset: 0x00451640
		public string GetSingularDirection(Cell Origin, string ExcludeDirection)
		{
			string text = null;
			foreach (string text2 in Directions.CardinalDirectionList)
			{
				if (!(text2 == ExcludeDirection))
				{
					Cell cellFromDirection = Origin.GetCellFromDirection(text2, true);
					if (cellFromDirection != null && cellFromDirection.HasObject(new Predicate<GameObject>(StationMover.HasTransitTag)))
					{
						if (text != null)
						{
							return null;
						}
						text = text2;
					}
				}
			}
			return text;
		}

		// Token: 0x0600BD08 RID: 48392 RVA: 0x0045349C File Offset: 0x0045169C
		public override void Render(RenderSoundEvent E)
		{
			if (this.Moving)
			{
				if (this.SoundID == null)
				{
					this.SoundID = "StationMover" + "." + Guid.NewGuid().ToString();
				}
				E.Char.soundExtra.Add(this.SoundID, "sfx_endgame_worldMap_spacestation_mover_move_lp", 0.25f, 0f, 1f, int.MaxValue);
			}
		}

		// Token: 0x04004BB8 RID: 19384
		[NonSerialized]
		public bool Moving;

		// Token: 0x04004BB9 RID: 19385
		[NonSerialized]
		public byte Animation;

		// Token: 0x04004BBA RID: 19386
		[NonSerialized]
		public string Direction;

		// Token: 0x04004BBB RID: 19387
		[NonSerialized]
		public Cell Destination;

		// Token: 0x04004BBC RID: 19388
		[NonSerialized]
		private string SoundID;
	}
}
