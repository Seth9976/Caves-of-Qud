using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.Liquids;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	/// <summary>
	/// Base type representing a single spatial cell ("square" or "space"
	/// in player-facing terminology) within a zone.
	/// </summary>
	// Token: 0x02000979 RID: 2425
	public class Cell : IRenderable, IInventory
	{
		// Token: 0x06006462 RID: 25698 RVA: 0x001EF860 File Offset: 0x001EDA60
		public int GetCourseFrom(Cell Target)
		{
			int num = 0;
			if (Target == null || Target == this)
			{
				return num;
			}
			Zone parentZone = Target.ParentZone;
			int num2;
			int num3;
			int num4;
			int num5;
			if (parentZone != this.ParentZone)
			{
				if (parentZone.ZoneWorld != this.ParentZone.ZoneWorld)
				{
					return num;
				}
				num2 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
				num3 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
				num4 = parentZone.GetZonewX() * Definitions.Width * 80 + parentZone.GetZoneX() * 80 + Target.X;
				num5 = parentZone.GetZonewY() * Definitions.Height * 25 + parentZone.GetZoneY() * 25 + Target.Y;
			}
			else
			{
				num2 = this.X;
				num3 = this.Y;
				num4 = Target.X;
				num5 = Target.Y;
			}
			if (num2 < num4)
			{
				num |= 2;
			}
			else if (num2 > num4)
			{
				num |= 8;
			}
			if (num3 < num5)
			{
				num |= 4;
			}
			else if (num3 > num5)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x001EF98B File Offset: 0x001EDB8B
		public int GetCourseTo(Cell Target)
		{
			return Courses.Opposite[this.GetCourseFrom(Target)];
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x001EF99C File Offset: 0x001EDB9C
		public int GetGeneralCourseFrom(Cell Target)
		{
			int num = 0;
			if (Target == null)
			{
				return num;
			}
			Zone parentZone = Target.ParentZone;
			int num2;
			int num3;
			int num4;
			int num5;
			if (parentZone != this.ParentZone)
			{
				if (parentZone.ZoneWorld != this.ParentZone.ZoneWorld)
				{
					return num;
				}
				num2 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
				num3 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
				num4 = parentZone.GetZonewX() * Definitions.Width * 80 + parentZone.GetZoneX() * 80 + Target.X;
				num5 = parentZone.GetZonewY() * Definitions.Height * 25 + parentZone.GetZoneY() * 25 + Target.Y;
			}
			else
			{
				num2 = this.X;
				num3 = this.Y;
				num4 = Target.X;
				num5 = Target.Y;
			}
			int num6 = Math.Abs(num2 - num4);
			int num7 = Math.Abs(num3 - num5);
			if (num6 * 2 < num7)
			{
				if (num3 < num5)
				{
					num |= 4;
				}
				else if (num3 > num5)
				{
					num |= 1;
				}
			}
			else if (num7 * 2 < num6)
			{
				if (num2 < num4)
				{
					num |= 2;
				}
				else if (num2 > num4)
				{
					num |= 8;
				}
			}
			else
			{
				if (num3 < num5)
				{
					num |= 4;
				}
				else if (num3 > num5)
				{
					num |= 1;
				}
				if (num2 < num4)
				{
					num |= 2;
				}
				else if (num2 > num4)
				{
					num |= 8;
				}
			}
			return num;
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x001EFB12 File Offset: 0x001EDD12
		public int GetGeneralCourseTo(Cell Target)
		{
			return Courses.Opposite[this.GetGeneralCourseFrom(Target)];
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x001EFB24 File Offset: 0x001EDD24
		public void Render(RenderEvent E, bool Visible = true, LightLevel Lit = LightLevel.Light, bool Explored = true, bool Alt = false, bool DisableFullscreenColorEffects = false, List<GameObject> WantsToPaint = null)
		{
			E.Reset();
			E.DisableFullscreenColorEffects = DisableFullscreenColorEffects;
			E.RenderString = (Explored ? "." : " ");
			E.Lit = Lit;
			E.Alt = Alt;
			E.Visible = Visible;
			bool flag = Globals.RenderMode == RenderModeType.Tiles;
			GameObject player = The.Player;
			Cell currentCell = player.CurrentCell;
			GameObject[] array = this.Objects.GetArray();
			int count = this.Objects.Count;
			bool flag2 = false;
			bool flag3 = Lit > LightLevel.None;
			int num = this.TransientCache;
			if ((num & 1) == 0)
			{
				for (int i = 0; i < count; i++)
				{
					GameObject gameObject = array[i];
					if ((num & 2) == 0 && gameObject.HasRegisteredEvent(RenderEvent.ID))
					{
						num |= 2;
					}
					if ((num & 4) == 0 && gameObject.HasRegisteredEvent(RenderEvent.OverlayID))
					{
						num |= 4;
					}
					if ((num & 8) == 0 && gameObject.HasRegisteredEvent(RenderEvent.FinalID))
					{
						num |= 8;
					}
				}
				num |= 1;
				this.TransientCache = num;
			}
			if (Explored)
			{
				if (Alt)
				{
					E.DetailColor = "k";
					E.ColorString = "&k";
					E.BackgroundString = "^k";
				}
				else if (XRLCore.RenderFloorTextures)
				{
					if (flag && !this.PaintTile.IsNullOrEmpty())
					{
						E.Tile = this.PaintTile;
					}
					if (!this.PaintRenderString.IsNullOrEmpty())
					{
						E.RenderString = this.PaintRenderString;
					}
					if (Visible)
					{
						if (flag && !this.PaintTileColor.IsNullOrEmpty())
						{
							E.ColorString = this.PaintTileColor;
						}
						else if (!this.PaintColorString.IsNullOrEmpty())
						{
							E.ColorString = this.PaintColorString;
						}
						if (!this.PaintDetailColor.IsNullOrEmpty())
						{
							E.DetailColor = this.PaintDetailColor;
						}
					}
				}
				bool flag4 = flag3 && Visible;
				bool flag5 = currentCell != null && currentCell.ParentZone == this.ParentZone && currentCell.DistanceTo(this.X, this.Y) <= 1;
				GameObject gameObject2 = null;
				for (int j = 0; j < count; j++)
				{
					GameObject gameObject3 = array[j];
					Render render = gameObject3.Render;
					if (render != null && !render.Never)
					{
						Render render2 = render;
						bool flag8;
						if (gameObject3 != player)
						{
							GameObject partyLeader = gameObject3.PartyLeader;
							bool? flag6;
							if (partyLeader == null)
							{
								flag6 = null;
							}
							else
							{
								Render render3 = partyLeader.Render;
								flag6 = ((render3 != null) ? new bool?(render3.PartyFlip) : null);
							}
							bool? flag7 = flag6;
							flag8 = flag7.GetValueOrDefault();
						}
						else
						{
							flag8 = true;
						}
						render2.PartyFlip = flag8;
						bool flag9 = XRLCore.RenderHiddenPlayer && gameObject3.IsPlayer();
						if (!flag2 || flag9 || (flag5 ? gameObject3.ConsiderSolidInRenderingContextFor(player) : gameObject3.ConsiderSolidInRenderingContext()))
						{
							if (render.CustomRender && gameObject3.HasRegisteredEvent("CustomRender"))
							{
								gameObject3.FireEvent(Event.New("CustomRender", "RenderEvent", E));
							}
							if (flag9 || (render.Visible && (flag4 || render.RenderIfDark)))
							{
								if (render.RenderLayer >= E.HighestLayer)
								{
									gameObject2 = gameObject3;
									E.HighestLayer = render.RenderLayer;
								}
								gameObject3.Seen();
								if (gameObject3 == Sidebar.CurrentTarget)
								{
									XRLCore.CludgeTargetRendered = true;
								}
								if (!flag2 && (flag5 ? gameObject3.ConsiderSolidInRenderingContextFor(player) : gameObject3.ConsiderSolidInRenderingContext()))
								{
									flag2 = true;
								}
							}
							if (WantsToPaint != null && (gameObject3.Flags & 128) != 0)
							{
								WantsToPaint.Add(gameObject3);
							}
						}
						if (Alt && (gameObject3.Flags & 256) != 0)
						{
							break;
						}
					}
				}
				if (gameObject2 != null)
				{
					if (flag2 && count > 1 && (Lit == LightLevel.Radar || Lit == LightLevel.LitRadar) && XRLCore.CurrentFrame10 % 125 >= 95)
					{
						gameObject2 = this.GetRadarReveal(array, count) ?? gameObject2;
					}
					Render render4 = gameObject2.Render;
					this.ParentZone.RenderedObjects++;
					if (XRLCore.RenderFloorTextures || render4.RenderLayer > 0)
					{
						if (!flag3 && gameObject2 == player)
						{
							E.RenderString = render4.RenderString;
							if (flag && !string.IsNullOrEmpty(render4.TileColor))
							{
								E.ColorString = render4.TileColor;
							}
							else
							{
								E.ColorString = render4.ColorString;
							}
							E.HighestLayer = render4.RenderLayer;
							if (flag)
							{
								E.Tile = render4.Tile;
								E.HFlip = render4.getHFlip();
								E.VFlip = render4.getVFlip();
								E.DetailColor = "y";
							}
							E.WantsToPaint = false;
							if ((num & 2) != 0)
							{
								gameObject2.ComponentRender(E);
							}
							if (WantsToPaint != null && E.WantsToPaint)
							{
								WantsToPaint.Add(gameObject2);
							}
						}
						else if (flag4 || render4.RenderIfDark)
						{
							if (flag)
							{
								E.Tile = render4.Tile;
								E.HFlip = render4.getHFlip();
								E.VFlip = render4.getVFlip();
							}
							if (Alt)
							{
								this.RenderAlt(E, player, gameObject2, render4);
							}
							else
							{
								E.RenderString = render4.RenderString;
								E.DetailColor = render4.DetailColor;
								if (flag && !string.IsNullOrEmpty(render4.TileColor))
								{
									E.ColorString = render4.TileColor;
								}
								else
								{
									E.ColorString = render4.ColorString;
								}
								bool flag10 = gameObject2 == Sidebar.CurrentTarget;
								if (flag10)
								{
									int currentFrame = XRLCore.CurrentFrame;
									if (currentFrame > 30)
									{
										if (currentFrame >= 45)
										{
											goto IL_056B;
										}
									}
									else if (currentFrame >= 15)
									{
										goto IL_056B;
									}
									bool flag11 = true;
									goto IL_056E;
									IL_056B:
									flag11 = false;
									IL_056E:
									flag10 = flag11;
								}
								if (flag10)
								{
									this.RenderTarget(E, player, gameObject2);
								}
							}
							E.HighestLayer = render4.RenderLayer;
							E.WantsToPaint = false;
							if (Alt && (num & 4) != 0)
							{
								gameObject2.OverlayRender(E);
							}
							else if ((num & 2) != 0)
							{
								gameObject2.ComponentRender(E);
							}
							if (WantsToPaint != null && E.WantsToPaint && !WantsToPaint.Contains(gameObject2))
							{
								WantsToPaint.Add(gameObject2);
							}
						}
					}
				}
			}
			if ((num & 8) != 0)
			{
				for (int k = 0; k < count; k++)
				{
					GameObject gameObject4 = array[k];
					E.WantsToPaint = false;
					gameObject4.FinalRender(E);
					if (WantsToPaint != null && E.WantsToPaint && !WantsToPaint.Contains(gameObject4))
					{
						WantsToPaint.Add(gameObject4);
					}
				}
			}
			if (E.RenderString.Length == 1)
			{
				if (E.RenderString[0] == '^')
				{
					E.RenderString = "^^";
				}
				else if (E.RenderString[0] == '&')
				{
					E.RenderString = "&&";
				}
			}
			if (!Alt && !E.CustomDraw)
			{
				if (!Visible || !flag3)
				{
					E.ColorString = "&K";
					if (flag)
					{
						E.DetailColor = "k";
						return;
					}
				}
				else if (!DisableFullscreenColorEffects && Lit != LightLevel.Light && Lit != LightLevel.Omniscient && Lit != LightLevel.Interpolight && Lit != LightLevel.Dimvision && Lit != LightLevel.Darkvision)
				{
					if (Lit == LightLevel.Safelight)
					{
						E.ColorString = "&r";
						E.DetailColor = "R";
						return;
					}
					if (Lit == LightLevel.Radar || Lit == LightLevel.LitRadar)
					{
						int currentFrame2 = XRLCore.CurrentFrame;
						if (currentFrame2 >= 27 && currentFrame2 <= 44 && this.BlocksRadar())
						{
							E.ColorString = "&R";
							E.DetailColor = "r";
							return;
						}
						if (Lit == LightLevel.Radar)
						{
							E.ColorString = "&C";
							E.DetailColor = "c";
						}
					}
				}
			}
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x001F0284 File Offset: 0x001EE484
		private GameObject GetRadarReveal(GameObject[] Objects, int Count)
		{
			GameObject gameObject = null;
			int i = 0;
			int num = -1;
			while (i < Count)
			{
				GameObject gameObject2 = Objects[i];
				if (gameObject2.Physics != null && !gameObject2.Physics.Solid && gameObject2.IsReal && gameObject2.Render != null && gameObject2.Render.Visible && gameObject2.Render.RenderLayer > num && gameObject2.Render.RenderLayer > 0)
				{
					num = gameObject2.Render.RenderLayer;
					gameObject = gameObject2;
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x001F0304 File Offset: 0x001EE504
		private void RenderTarget(RenderEvent E, GameObject Player, GameObject Object)
		{
			Brain brain = Object.Brain;
			if (Object.IsPlayer())
			{
				E.BackgroundString = "^B";
				return;
			}
			if (brain != null)
			{
				GameObject partyLeader = brain.PartyLeader;
				if (partyLeader != null && partyLeader.IsPlayer())
				{
					E.BackgroundString = "^b";
					return;
				}
			}
			if (brain != null && brain.IsHostileTowards(Player))
			{
				E.BackgroundString = "^r";
				return;
			}
			E.BackgroundString = "^g";
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x001F0374 File Offset: 0x001EE574
		private void RenderAlt(RenderEvent E, GameObject Player, GameObject Object, Render Render)
		{
			E.RenderString = "Û";
			string propertyOrTag = Object.GetPropertyOrTag("OverlayColor", null);
			if (propertyOrTag != null)
			{
				E.RenderString = Render.RenderString;
				E.ColorString = propertyOrTag;
				E.BackgroundString = "^k";
				if (E.ColorString.Length > 1)
				{
					E.DetailColor = E.ColorString.Substring(1, 1);
				}
			}
			else
			{
				E.ColorString = "&k";
				E.BackgroundString = "^k";
				E.DetailColor = "k";
			}
			string stringProperty = Object.GetStringProperty("OverlayDetailColor", null);
			if (stringProperty != null)
			{
				E.DetailColor = stringProperty;
			}
			string stringProperty2 = Object.GetStringProperty("OverlayRenderString", null);
			if (stringProperty2 != null)
			{
				E.RenderString = stringProperty2;
			}
			string stringProperty3 = Object.GetStringProperty("OverlayTile", null);
			if (stringProperty3 != null)
			{
				E.Tile = stringProperty3;
			}
			if (Object.Brain != null && Player != null)
			{
				Brain brain = Object.Brain;
				if (Object.IsPlayer())
				{
					E.BackgroundString = "^k";
					E.ColorString = "&B";
					E.DetailColor = "B";
					return;
				}
				GameObject partyLeader = brain.PartyLeader;
				if (partyLeader != null && partyLeader.IsPlayer())
				{
					E.BackgroundString = "^k";
					E.ColorString = "&b";
					E.DetailColor = "b";
					return;
				}
				if (brain.IsHostileTowards(Player))
				{
					E.RenderString = Render.RenderString;
					E.ColorString = "&R";
					E.BackgroundString = "^k";
					E.DetailColor = "R";
					return;
				}
				E.RenderString = Render.RenderString;
				E.ColorString = "&G";
				E.BackgroundString = "^k";
				E.DetailColor = "G";
				return;
			}
			else
			{
				Tinkering_Mine tinkering_Mine;
				if (!Object.TryGetPart<Tinkering_Mine>(out tinkering_Mine))
				{
					if (Player != null)
					{
						if (Player.HasPart<TrashRifling>() && Object.HasPart<Garbage>())
						{
							E.RenderString = Render.RenderString;
							E.DetailColor = "w";
							E.ColorString = "&w";
							E.BackgroundString = "^k";
							return;
						}
						Harvestable harvestable;
						if (Player.HasPart<CookingAndGathering_Harvestry>() && Object.TryGetPart<Harvestable>(out harvestable) && harvestable != null && harvestable.Ripe)
						{
							E.RenderString = Render.RenderString;
							E.DetailColor = "w";
							E.ColorString = "&w";
							E.BackgroundString = "^k";
						}
					}
					return;
				}
				if (tinkering_Mine.Timer != -1 || tinkering_Mine.ConsiderHostile(Player))
				{
					E.RenderString = Render.RenderString;
					E.ColorString = "&R";
					E.BackgroundString = "^k";
					E.DetailColor = "R";
					return;
				}
				E.RenderString = Render.RenderString;
				E.ColorString = "&G";
				E.BackgroundString = "^k";
				E.DetailColor = "G";
				return;
			}
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x001F0634 File Offset: 0x001EE834
		public RenderEvent Render(bool Visible = true, LightLevel Lit = LightLevel.Light, bool Explored = true, bool Alt = false, bool DisableFullscreenColorEffects = false, List<GameObject> WantsToPaint = null)
		{
			RenderEvent renderEvent = Cell.eRender;
			this.Render(renderEvent, Visible, Lit, Explored, Alt, DisableFullscreenColorEffects, WantsToPaint);
			return renderEvent;
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x001F0658 File Offset: 0x001EE858
		[Obsolete("ConsoleChar is no longer provided directly")]
		public RenderEvent Render(ConsoleChar Char, bool Visible, LightLevel Lit, bool Explored, bool Alt, bool DisableFullscreenColorEffects = false, List<GameObject> WantsToPaint = null)
		{
			RenderEvent renderEvent = Cell.eRender;
			this.Render(renderEvent, Visible, Lit, Explored, Alt, DisableFullscreenColorEffects, WantsToPaint);
			return renderEvent;
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x001F067D File Offset: 0x001EE87D
		[Obsolete]
		public void RenderSound(ConsoleChar Char, ConsoleChar[,] Buffer)
		{
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x001F0680 File Offset: 0x001EE880
		public void PlayWorldSound(string Clip, float Volume = 0.5f, float PitchVariance = 0f, bool Combat = false, float Delay = 0f, float Pitch = 1f, float CostMultiplier = 1f, int CostMaximum = 2147483647)
		{
			if (Clip.IsNullOrEmpty() || !Options.Sound || (Combat && !Options.UseCombatSounds) || this.ParentZone == null || !this.ParentZone.IsActive())
			{
				return;
			}
			int num;
			bool flag;
			if (!this.TryGetSoundPropagation(Mathf.RoundToInt(105f * Volume), out num, out flag))
			{
				return;
			}
			num = Mathf.RoundToInt((float)num * CostMultiplier);
			num = Math.Min(num, CostMaximum);
			SoundManager.PlayWorldSound(Clip, num, flag, Volume, this.Location, PitchVariance, Delay, Pitch);
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x001F06FE File Offset: 0x001EE8FE
		public void CheckSoundWall(GameObject Object, int Expected)
		{
			if (!Zone.SoundMapDirty && Zone.SoundWalls[this.X, this.Y] != Expected && Object.IsWall())
			{
				Zone.SoundMapDirty = true;
			}
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x001F0730 File Offset: 0x001EE930
		public bool TryGetSoundPropagation(int Range, out int Cost, out bool Occluded)
		{
			Zone parentZone = this.ParentZone;
			Cell playerCell = The.PlayerCell;
			if (playerCell == null || playerCell.ParentZone != parentZone || (!parentZone.IsWorldMap() && this.PathDistanceTo(playerCell) > Range))
			{
				Cost = int.MaxValue;
				Occluded = true;
				return false;
			}
			if (Zone.SoundMapDirty)
			{
				parentZone.UpdateSoundMap();
			}
			Location2D location = this.Location;
			Occluded = !this.IsVisible();
			Cost = Zone.SoundMap.GetCostAtPoint(location);
			if (Cost == 2147483647)
			{
				Cost = Zone.SoundMap.GetCostFromPointDirection(location, Zone.SoundMap.GetLowestCostDirectionFrom(location));
			}
			return true;
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x001F07C2 File Offset: 0x001EE9C2
		public Cell()
		{
			this.Objects = new Cell.ObjectRack(this);
		}

		// Token: 0x06006471 RID: 25713 RVA: 0x001F07E8 File Offset: 0x001EE9E8
		public Cell(Zone ParentZone)
			: this()
		{
			this.ParentZone = ParentZone;
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06006472 RID: 25714 RVA: 0x001F07F7 File Offset: 0x001EE9F7
		public Point2D Pos2D
		{
			get
			{
				return new Point2D(this.X, this.Y);
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06006473 RID: 25715 RVA: 0x001F080A File Offset: 0x001EEA0A
		public Point Point
		{
			get
			{
				return new Point(this.X, this.Y);
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x001F081D File Offset: 0x001EEA1D
		public Location2D Location
		{
			get
			{
				return Location2D.Get(this.X, this.Y);
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x001F0830 File Offset: 0x001EEA30
		public int LocalCoordKey
		{
			get
			{
				return (this.X << 16) | this.Y;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06006476 RID: 25718 RVA: 0x001F0842 File Offset: 0x001EEA42
		public string DebugName
		{
			get
			{
				return this.X.ToString() + "," + this.Y.ToString();
			}
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x001F0864 File Offset: 0x001EEA64
		public bool AllInDirections(IEnumerable<string> directions, int distance, Predicate<Cell> test)
		{
			foreach (string text in directions)
			{
				if (!this.AllInDirection(text, distance, test))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x001F08B8 File Offset: 0x001EEAB8
		public GlobalLocation GetGlobalLocation()
		{
			return new GlobalLocation(this);
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x001F08C0 File Offset: 0x001EEAC0
		public bool AnyInDirection(string Direction, int Distance, Predicate<Cell> Test)
		{
			Cell cell = this;
			for (int i = 0; i < Distance; i++)
			{
				cell = cell.GetCellFromDirection(Direction, true);
				if (cell == null)
				{
					return false;
				}
				if (Test(cell))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x001F08F8 File Offset: 0x001EEAF8
		public bool AllInDirection(string direction, int distance, Predicate<Cell> test)
		{
			Cell cell = this;
			for (int i = 0; i < distance; i++)
			{
				cell = cell.GetCellFromDirection(direction, true);
				if (!test(cell))
				{
					return false;
				}
				if (cell == null)
				{
					break;
				}
			}
			return true;
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x001F092C File Offset: 0x001EEB2C
		public string GetAddress()
		{
			Cell.addressBuilder.Length = 0;
			Cell.addressBuilder.Append(this.ParentZone.ZoneID).Append('@').Append(this.X)
				.Append(',')
				.Append(this.Y);
			return Cell.addressBuilder.ToString();
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x001F0988 File Offset: 0x001EEB88
		public void walk(Func<Cell, Cell> next, Predicate<Cell> walker)
		{
			if (!walker(this))
			{
				return;
			}
			Cell cell = next(this);
			if (cell == null)
			{
				return;
			}
			cell.walk(next, walker);
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x001F09B4 File Offset: 0x001EEBB4
		public static Cell FromAddress(string CellAddress)
		{
			Cell cell;
			try
			{
				string[] array = CellAddress.Split('@', StringSplitOptions.None);
				string[] array2 = array[1].Split(',', StringSplitOptions.None);
				cell = XRLCore.Core.Game.ZoneManager.GetZone(array[0]).GetCell(int.Parse(array2[0]), int.Parse(array2[1]));
			}
			catch (Exception ex)
			{
				MetricsManager.LogError(ex);
				cell = null;
			}
			return cell;
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x001F0A20 File Offset: 0x001EEC20
		public bool ConsideredOutside()
		{
			return this.ParentZone.IsOutside() || this.HasObject("FlyingWhitelistArea");
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x001F0A44 File Offset: 0x001EEC44
		public bool HasSemanticTag(string tag)
		{
			return this.SemanticTags != null && this.SemanticTags.Any<string>((string t) => t.EqualsNoCase(tag));
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x001F0A7F File Offset: 0x001EEC7F
		public void AddSemanticTag(string tag)
		{
			if (this.SemanticTags == null)
			{
				this.SemanticTags = new List<string>();
			}
			if (!this.HasSemanticTag(tag))
			{
				this.SemanticTags.Add(tag);
			}
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x001F0AA9 File Offset: 0x001EECA9
		public void RemoveSemanticTag(string tag)
		{
			if (this.SemanticTags == null)
			{
				return;
			}
			if (this.HasSemanticTag(tag))
			{
				this.SemanticTags.Remove(tag);
			}
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x001F0ACC File Offset: 0x001EECCC
		public override string ToString()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.X);
			stringBuilder.Append(",");
			stringBuilder.Append(this.Y);
			stringBuilder.Append(" Objects:");
			foreach (GameObject gameObject in this.Objects)
			{
				stringBuilder.Append(gameObject.ToString() + " ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x001F0B70 File Offset: 0x001EED70
		public LightLevel GetLight()
		{
			Zone parentZone = this.ParentZone;
			if (parentZone == null)
			{
				return LightLevel.None;
			}
			return parentZone.GetLight(this.X, this.Y);
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06006484 RID: 25732 RVA: 0x001F0B90 File Offset: 0x001EED90
		public int RenderedObjectsCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.Objects.Count; i++)
				{
					if (this.Objects[i].Render != null)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06006486 RID: 25734 RVA: 0x001F0BD6 File Offset: 0x001EEDD6
		// (set) Token: 0x06006485 RID: 25733 RVA: 0x001F0BCD File Offset: 0x001EEDCD
		public string GroundLiquid
		{
			get
			{
				if (this._GroundLiquid != null)
				{
					return this._GroundLiquid;
				}
				if (this.ParentZone != null)
				{
					return this.ParentZone.GroundLiquid;
				}
				return null;
			}
			set
			{
				this._GroundLiquid = value;
			}
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x001F0BFC File Offset: 0x001EEDFC
		internal void SetMinimapColorFrom(Color32 c)
		{
			this.minimapCacheValid = true;
			this.minimapCacheColor.r = c.r;
			this.minimapCacheColor.g = c.g;
			this.minimapCacheColor.b = c.b;
			this.minimapCacheColor.a = c.a;
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x001F0C54 File Offset: 0x001EEE54
		public void RefreshMinimapColor()
		{
			if (this.lastMinimapVisibile != this.IsVisible())
			{
				this.lastMinimapVisibile = this.IsVisible();
				this.minimapCacheValid = false;
			}
			if (this.lastMinimapLit != this.IsLit())
			{
				this.lastMinimapVisibile = this.IsVisible();
			}
			if (this.lastMinimapExplored != this.IsExplored())
			{
				this.lastMinimapVisibile = this.IsVisible();
			}
			if (this.minimapCacheValid)
			{
				return;
			}
			GameObject player = The.Player;
			if (this == ((player != null) ? player.GetCurrentCell() : null))
			{
				this.SetMinimapColorFrom(Cell.WhiteAlphaD);
				return;
			}
			if (!this.IsExplored())
			{
				this.SetMinimapColorFrom(Cell.BlackAlpha32);
				return;
			}
			if (this.IsLit())
			{
				this.SetMinimapColorFrom(Cell.BlackAlpha164);
			}
			else
			{
				this.SetMinimapColorFrom(Cell.BlackAlpha128);
			}
			if (this.ParentZone.IsWorldMap())
			{
				GameObject gameObject;
				if ((gameObject = this.GetFirstObjectWithPropertyOrTag("MinimapColor")) != null)
				{
					this.SetMinimapColorFrom(ConsoleLib.Console.ColorUtility.ColorFromString(gameObject.GetPropertyOrTag("MinimapColor", null)));
					return;
				}
				this.SetMinimapColorFrom(Cell.BlackAlpha32);
				return;
			}
			else
			{
				if ((this.HasObjectWithPart("StairsUp") || this.HasObjectWithPart("StairsDown")) && !this.HasObjectWithPart("HiddenRender"))
				{
					this.SetMinimapColorFrom(Cell.VioletAlphaD);
					return;
				}
				if (this.IsVisible() && this.IsLit() && this.HasObjectWithPart("Combat"))
				{
					GameObject firstObjectWithPart = this.GetFirstObjectWithPart("Combat");
					if (firstObjectWithPart.Brain == null)
					{
						this.SetMinimapColorFrom(Cell.RedAlphaD);
						return;
					}
					if (firstObjectWithPart.Brain.IsHostileTowards(The.Player))
					{
						this.SetMinimapColorFrom(Cell.RedAlphaD);
						return;
					}
					this.SetMinimapColorFrom(Cell.GreenAlphaD);
					return;
				}
				else
				{
					if (this.HasObjectWithTag("Chest"))
					{
						this.SetMinimapColorFrom(Cell.DarkYellowAlphaD);
						return;
					}
					if (this.HasObjectWithTag("LiquidVolume"))
					{
						this.SetMinimapColorFrom(Cell.DarkBlueAlphaD);
						return;
					}
					if (this.HasWall())
					{
						this.SetMinimapColorFrom(Cell.GrayAlphaD);
						return;
					}
					if (this.HasObjectWithTag("Door"))
					{
						this.SetMinimapColorFrom(Cell.CanaryAlphaD);
						return;
					}
					GameObject gameObject;
					if ((gameObject = this.GetFirstObjectWithPropertyOrTag("MinimapColor")) != null)
					{
						this.SetMinimapColorFrom(ConsoleLib.Console.ColorUtility.ColorFromString(gameObject.GetPropertyOrTag("MinimapColor", null)));
					}
					return;
				}
			}
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x001F0E7C File Offset: 0x001EF07C
		public List<GameObject> FastFloodSearch(string SearchPart, int Radius)
		{
			return this.ParentZone.FastFloodSearch(this.X, this.Y, Radius, SearchPart, null, null);
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x001F0E99 File Offset: 0x001EF099
		public List<GameObject> FastFloodVisibility(string SearchPart, int Radius)
		{
			return this.ParentZone.FastFloodVisibility(this.X, this.Y, Radius, SearchPart, null, null, null);
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x001F0EB8 File Offset: 0x001EF0B8
		public static Cell Load(SerializationReader Reader, int x, int y, Zone ParentZone)
		{
			Cell cell = new Cell(ParentZone);
			cell.X = x;
			cell.Y = y;
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Reader.ReadGameObject(null);
				cell.Objects.Add(gameObject);
				Reader.Locations[gameObject] = cell;
			}
			num = Reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				cell.AddObjectWithoutEvents(GameObject.CreateUnmodified(Reader.ReadString()), null);
			}
			cell.PaintTile = Reader.ReadString();
			cell.PaintTileColor = Reader.ReadString();
			cell.PaintRenderString = Reader.ReadString();
			cell.PaintColorString = Reader.ReadString();
			cell.PaintDetailColor = Reader.ReadString();
			cell.Live = true;
			int num2 = Reader.ReadInt32();
			if (num2 > 0)
			{
				cell.SemanticTags = new List<string>(num2);
				for (int k = 0; k < num2; k++)
				{
					cell.SemanticTags.Add(Reader.ReadString());
				}
			}
			return cell;
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x001F0FB7 File Offset: 0x001EF1B7
		public bool ShouldWrite(GameObject Object)
		{
			return true;
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x001F0FBC File Offset: 0x001EF1BC
		public void Save(SerializationWriter Writer)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < this.Objects.Count; i++)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Physics == null || gameObject.Physics._CurrentCell != this)
				{
					this.LogInvalidPhysics(gameObject);
					this.Objects.RemoveAt(i--);
				}
				else if (!this.ShouldWrite(gameObject))
				{
					num2++;
				}
				else
				{
					num++;
				}
			}
			Writer.Write(num);
			int j = 0;
			int count = this.Objects.Count;
			while (j < count)
			{
				GameObject gameObject2 = this.Objects[j];
				if (this.ShouldWrite(gameObject2))
				{
					Writer.WriteGameObject(gameObject2);
				}
				j++;
			}
			Writer.Write(num2);
			int k = 0;
			int count2 = this.Objects.Count;
			while (k < count2)
			{
				GameObject gameObject3 = this.Objects[k];
				if (!this.ShouldWrite(gameObject3))
				{
					Writer.Write(gameObject3.Blueprint);
				}
				k++;
			}
			Writer.Write(this.PaintTile);
			Writer.Write(this.PaintTileColor);
			Writer.Write(this.PaintRenderString);
			Writer.Write(this.PaintColorString);
			Writer.Write(this.PaintDetailColor);
			if (this.SemanticTags == null)
			{
				Writer.Write(0);
				return;
			}
			Writer.Write(this.SemanticTags.Count);
			foreach (string text in this.SemanticTags)
			{
				Writer.Write(text);
			}
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x001F116C File Offset: 0x001EF36C
		public void FlushRenderCache()
		{
			this.TransientCache &= -16;
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x001F117D File Offset: 0x001EF37D
		public void FlushNavigationCache()
		{
			this.NavigationWeightCache.Clear();
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x001F118C File Offset: 0x001EF38C
		public int GetNavigationWeightFor(GameObject Looker, bool Autoexploring = false, bool Juggernaut = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool FlexPhase = false)
		{
			return this.NavigationWeight(Looker, Zone.CalculateNav(Looker, Autoexploring, Juggernaut, IgnoreCreatures, IgnoreGases, FlexPhase, true));
		}

		// Token: 0x06006491 RID: 25745 RVA: 0x001F11B0 File Offset: 0x001EF3B0
		public int NavigationWeight(GameObject Looker = null, bool Smart = false, bool Burrower = false, bool Autoexploring = false, bool Flying = false, bool WallWalker = false, bool IgnoresWalls = false, bool Swimming = false, bool Slimewalking = false, bool Aquatic = false, bool Polypwalking = false, bool Strutwalking = false, bool Juggernaut = false, bool Reefer = false, bool IgnoreCreatures = false, bool IgnoreGases = false, bool Unbreathing = false, bool FilthAffinity = false, bool OutOfPhase = false, bool Omniphase = false, bool Nullphase = false, bool FlexPhase = false)
		{
			return this.NavigationWeight(Looker, Zone.CalculateNav(Smart, Burrower, Autoexploring, Flying, WallWalker, IgnoresWalls, Swimming, Slimewalking, Aquatic, Polypwalking, Strutwalking, Juggernaut, Reefer, IgnoreCreatures, IgnoreGases, Unbreathing, FilthAffinity, OutOfPhase, Omniphase, Nullphase, FlexPhase));
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x001F11F4 File Offset: 0x001EF3F4
		public int NavigationWeight(GameObject Looker, int Nav)
		{
			short num;
			short num2;
			this.NavigationWeight(Looker, Nav, out num, out num2);
			return (int)num;
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x001F1210 File Offset: 0x001EF410
		internal void NavigationWeight(GameObject Looker, int Nav, out short Weight, out short SocialWeight)
		{
			if ((Nav & 4) != 0 && this.GetLight() == LightLevel.Blackout)
			{
				Weight = (SocialWeight = 101);
				return;
			}
			if ((Nav & 2048) != 0)
			{
				Weight = (SocialWeight = 1);
				return;
			}
			int num;
			if (this.NavigationWeightCache.TryGetValue(Nav, out num))
			{
				Weight = (short)(num & 65535);
				SocialWeight = (short)((num >> 16) & 65535);
				return;
			}
			Weight = 0;
			bool flag = false;
			if ((Nav & 16) != 0)
			{
				bool flag2 = false;
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (this.Objects[i].IsWalkableWall(Looker, ref flag))
					{
						flag2 = true;
						break;
					}
					i++;
				}
				if (!flag2)
				{
					Weight = 101;
				}
				else if (this.HasObjectWithPart("Combat"))
				{
					Weight = 99;
				}
			}
			if ((Nav & 256) != 0 && Weight < 100 && !this.HasAquaticSupportFor(Looker))
			{
				Weight = 101;
			}
			if (Weight < 3)
			{
				int num2 = Nav & 1536;
				if (num2 != 0)
				{
					if (num2 == 1536)
					{
						if (!this.HasObjectWithTag("Reef"))
						{
							Weight = 3;
						}
					}
					else if (num2 == 512)
					{
						if (!this.HasObjectWithTag("NavigationPolyp"))
						{
							Weight = 3;
						}
					}
					else if (num2 == 1024 && !this.HasObjectWithTag("NavigationStrut"))
					{
						Weight = 3;
					}
				}
			}
			if (Weight < 25 && (Nav & 4096) != 0 && this.HasObjectWithTag("Reef"))
			{
				Weight = 25;
			}
			GetNavigationWeightEvent getNavigationWeightEvent = GetNavigationWeightEvent.HandleFor(this, Looker, flag, (int)Weight, Nav);
			GetAdjacentNavigationWeightEvent.Process(getNavigationWeightEvent);
			ActorGetNavigationWeightEvent.Process(getNavigationWeightEvent);
			Weight = (short)Math.Clamp(getNavigationWeightEvent.Weight, -32768, 32767);
			SocialWeight = (short)Math.Clamp(getNavigationWeightEvent.SocialWeight, -32768, 32767);
			if (!getNavigationWeightEvent.Uncacheable)
			{
				this.NavigationWeightCache[Nav] = (int)((ushort)Weight) | ((int)((ushort)SocialWeight) << 16);
			}
			GetNavigationWeightEvent.ResetTo(ref getNavigationWeightEvent);
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x001F13E0 File Offset: 0x001EF5E0
		public int NavigationWeight(GameObject Looker, ref int Nav)
		{
			if ((Nav & 268435456) != 0)
			{
				Nav = Zone.CalculateNav(Looker, false, false, false, false, false, true) | (Nav & -268435457);
			}
			return this.NavigationWeight(Looker, Nav);
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x001F140C File Offset: 0x001EF60C
		[Obsolete]
		public bool IsGraveyard()
		{
			return false;
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x001F1410 File Offset: 0x001EF610
		public GameObject FastFloodVisibilityFirstBlueprint(string blueprint, GameObject looker)
		{
			return this.ParentZone.FastSquareVisibilityFirstBlueprint(this.X, this.Y, 10, blueprint, looker, false, false, null);
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x001F143B File Offset: 0x001EF63B
		public bool HasObjectNearby(string blueprint, GameObject looker = null, bool visibleOnly = false, bool exploredOnly = true)
		{
			return this.ParentZone.FastFloodFindAnyBlueprint(this.X, this.Y, 24, blueprint, looker ?? The.Player, visibleOnly, exploredOnly);
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x001F1464 File Offset: 0x001EF664
		public GameObject FindObjectNearby(string blueprint, GameObject looker = null, bool visibleOnly = false, bool exploredOnly = true)
		{
			return this.ParentZone.FastFloodFindFirstBlueprint(this.X, this.Y, 24, blueprint, looker ?? The.Player, visibleOnly, exploredOnly);
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x001F1490 File Offset: 0x001EF690
		public bool HasObjectWithBlueprint(string[] Blueprints)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				int j = 0;
				int num = Blueprints.Length;
				while (j < num)
				{
					if (this.Objects[i].Blueprint == Blueprints[j])
					{
						return true;
					}
					j++;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x001F14E4 File Offset: 0x001EF6E4
		public bool HasObjectInDirection(string Direction, string Blueprint)
		{
			Cell cellFromDirection = this.GetCellFromDirection(Direction, true);
			return cellFromDirection != null && cellFromDirection.HasObject(Blueprint);
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x001F1506 File Offset: 0x001EF706
		public bool HasStairs()
		{
			return this.HasObjectWithTagOrProperty("Stairs");
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x001F1514 File Offset: 0x001EF714
		public bool HasObjectWithTag(string Tag)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Tag))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x001F1550 File Offset: 0x001EF750
		public bool HasObjectWithTag(string Tag, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Tag) && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x001F15A0 File Offset: 0x001EF7A0
		public bool HasSpawnBlocker()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsSpawnBlocker())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001F15DC File Offset: 0x001EF7DC
		public bool HasObjectWithPartOrTagOrProperty(string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Name))
				{
					return true;
				}
				if (this.Objects[i].HasTagOrProperty(Name))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001F1630 File Offset: 0x001EF830
		public bool HasObjectWithTagOrProperty(string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTagOrProperty(Name))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x001F166C File Offset: 0x001EF86C
		public bool HasObjectWithPropertyOrTag(string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x001F16A8 File Offset: 0x001EF8A8
		public bool HasObjectWithPropertyOrTagEqualToValue(string Name, string Value)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name))
				{
					return string.Equals(this.Objects[i].GetPropertyOrTag(Name, null), Value);
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x001F16FC File Offset: 0x001EF8FC
		public bool HasObjectWithPropertyOrTagOtherThan(string Name, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPropertyOrTag(Name))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x001F1748 File Offset: 0x001EF948
		public bool HasObjectWithTagAdjacent(string Tag)
		{
			using (List<Cell>.Enumerator enumerator = this.GetLocalAdjacentCells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasObjectWithTag(Tag))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x001F17A4 File Offset: 0x001EF9A4
		public int GetObjectCountWithTagsOrProperties(string Name1, string Name2)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x001F17F8 File Offset: 0x001EF9F8
		public int GetObjectCountWithTagsOrProperties(string Name1, string Name2, Predicate<GameObject> Filter)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2)) && Filter(this.Objects[i]))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x001F1860 File Offset: 0x001EFA60
		public GameObject GetFirstObjectWithTagsOrProperties(string Name1, string Name2)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064A8 RID: 25768 RVA: 0x001F18BC File Offset: 0x001EFABC
		public GameObject GetFirstObjectWithTagsOrProperties(string Name1, string Name2, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2)) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064A9 RID: 25769 RVA: 0x001F192C File Offset: 0x001EFB2C
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2)
		{
			int objectCountWithTagsOrProperties = this.GetObjectCountWithTagsOrProperties(Name1, Name2);
			if (objectCountWithTagsOrProperties == 0)
			{
				return null;
			}
			List<GameObject> list = new List<GameObject>(objectCountWithTagsOrProperties);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x001F19A4 File Offset: 0x001EFBA4
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, Predicate<GameObject> Filter)
		{
			int objectCountWithTagsOrProperties = this.GetObjectCountWithTagsOrProperties(Name1, Name2);
			if (objectCountWithTagsOrProperties == 0)
			{
				return null;
			}
			List<GameObject> list = new List<GameObject>(objectCountWithTagsOrProperties);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2)) && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x060064AB RID: 25771 RVA: 0x001F1A30 File Offset: 0x001EFC30
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, List<GameObject> result)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2))
				{
					result.Add(this.Objects[i]);
				}
				i++;
			}
			return result;
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x001F1A90 File Offset: 0x001EFC90
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, Predicate<GameObject> Filter, List<GameObject> result)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2)) && Filter(this.Objects[i]))
				{
					result.Add(this.Objects[i]);
				}
				i++;
			}
			return result;
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x001F1B08 File Offset: 0x001EFD08
		public int GetObjectCountWithTagsOrProperties(string Name1, string Name2, string Name3)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060064AE RID: 25774 RVA: 0x001F1B70 File Offset: 0x001EFD70
		public int GetObjectCountWithTagsOrProperties(string Name1, string Name2, string Name3, Predicate<GameObject> Filter)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3)) && Filter(this.Objects[i]))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060064AF RID: 25775 RVA: 0x001F1BF0 File Offset: 0x001EFDF0
		public GameObject GetFirstObjectWithTagsOrProperties(string Name1, string Name2, string Name3)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064B0 RID: 25776 RVA: 0x001F1C60 File Offset: 0x001EFE60
		public GameObject GetFirstObjectWithTagsOrProperties(string Name1, string Name2, string Name3, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3)) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064B1 RID: 25777 RVA: 0x001F1CE4 File Offset: 0x001EFEE4
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, string Name3)
		{
			int objectCountWithTagsOrProperties = this.GetObjectCountWithTagsOrProperties(Name1, Name2, Name3);
			if (objectCountWithTagsOrProperties == 0)
			{
				return null;
			}
			List<GameObject> list = new List<GameObject>(objectCountWithTagsOrProperties);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x060064B2 RID: 25778 RVA: 0x001F1D70 File Offset: 0x001EFF70
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, string Name3, Predicate<GameObject> Filter)
		{
			int objectCountWithTagsOrProperties = this.GetObjectCountWithTagsOrProperties(Name1, Name2, Name3);
			if (objectCountWithTagsOrProperties == 0)
			{
				return null;
			}
			List<GameObject> list = new List<GameObject>(objectCountWithTagsOrProperties);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3)) && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x001F1E10 File Offset: 0x001F0010
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, string Name3, List<GameObject> result)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3))
				{
					result.Add(this.Objects[i]);
				}
				i++;
			}
			return result;
		}

		// Token: 0x060064B4 RID: 25780 RVA: 0x001F1E88 File Offset: 0x001F0088
		public List<GameObject> GetObjectsWithTagsOrProperties(string Name1, string Name2, string Name3, Predicate<GameObject> Filter, List<GameObject> result)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((this.Objects[i].HasPropertyOrTag(Name1) || this.Objects[i].HasPropertyOrTag(Name2) || this.Objects[i].HasPropertyOrTag(Name3)) && Filter(this.Objects[i]))
				{
					result.Add(this.Objects[i]);
				}
				i++;
			}
			return result;
		}

		// Token: 0x060064B5 RID: 25781 RVA: 0x001F1F14 File Offset: 0x001F0114
		public int CountObjectsWithTag(string Tag)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Tag))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x001F1F54 File Offset: 0x001F0154
		public int CountObjectWithTagCardinalAdjacent(string Tag)
		{
			int n = 0;
			this.ForeachCardinalAdjacentLocalCell(delegate(Cell c)
			{
				if (c.HasObjectWithTag(Tag))
				{
					int i = n;
					n = i + 1;
				}
			});
			return n;
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x001F1F90 File Offset: 0x001F0190
		public GameObject GetFirstObjectWithTag(string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Name))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064B8 RID: 25784 RVA: 0x001F1FD8 File Offset: 0x001F01D8
		public GameObject GetFirstObjectWithTag(string Name, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Name) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064B9 RID: 25785 RVA: 0x001F2034 File Offset: 0x001F0234
		public GameObject GetFirstObjectWithPropertyOrTag(string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x001F207C File Offset: 0x001F027C
		public GameObject GetFirstObjectWithPropertyOrTag(string Name, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x001F20D8 File Offset: 0x001F02D8
		public GameObject GetFirstObjectWithPropertyOrTagExcept(string Name, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPropertyOrTag(Name))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x001F2130 File Offset: 0x001F0330
		public GameObject GetFirstObjectWithTagAndNotTag(string Name1, string Name2)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Name1) && !this.Objects[i].HasTag(Name2))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x001F218C File Offset: 0x001F038C
		public GameObject GetFirstObjectWithPropertyOrTagAndNotPropertyOrTag(string Name1, string Name2)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) && !this.Objects[i].HasPropertyOrTag(Name2))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x001F21E8 File Offset: 0x001F03E8
		public GameObject GetFirstObjectWithPropertyOrTagAndNotPropertyOrTag(string Name1, string Name2, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPropertyOrTag(Name1) && !this.Objects[i].HasPropertyOrTag(Name2) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x001F2257 File Offset: 0x001F0457
		public GameObject GetFirstObject()
		{
			if (this.Objects.Count <= 0)
			{
				return null;
			}
			return this.Objects[0];
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x001F2278 File Offset: 0x001F0478
		public GameObject GetFirstVisibleObject()
		{
			if (!this.IsVisible())
			{
				return null;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsVisible())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x001F22C8 File Offset: 0x001F04C8
		public GameObject GetFirstObject(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x001F2314 File Offset: 0x001F0514
		public GameObject GetFirstVisibleObject(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint && this.Objects[i].IsVisible())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x001F2374 File Offset: 0x001F0574
		public GameObject GetFirstObjectExcept(string Blueprint, GameObject skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].Blueprint == Blueprint)
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x001F23D0 File Offset: 0x001F05D0
		public GameObject GetFirstVisibleObjectExcept(string Blueprint, GameObject skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].Blueprint == Blueprint && this.Objects[i].IsVisible())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x001F2440 File Offset: 0x001F0640
		public GameObject GetFirstObject(Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x001F2488 File Offset: 0x001F0688
		public IPart GetFirstObjectPart(Type Type)
		{
			GameObject[] array = this.Objects.GetArray();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				IPart part = array[i].GetPart(Type);
				if (part != null)
				{
					return part;
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x001F24CC File Offset: 0x001F06CC
		public T GetFirstObjectPart<T>() where T : IPart
		{
			GameObject[] array = this.Objects.GetArray();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				T t;
				if (array[i].TryGetPart<T>(out t))
				{
					return t;
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x001F2518 File Offset: 0x001F0718
		public bool TryGetFirstObjectPart<T>(out T Part) where T : IPart
		{
			GameObject[] array = this.Objects.GetArray();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (array[i].TryGetPart<T>(out Part))
				{
					return true;
				}
				i++;
			}
			Part = default(T);
			return false;
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x001F2560 File Offset: 0x001F0760
		public GameObject GetFirstObjectWithPart(string Part)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Part))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064CA RID: 25802 RVA: 0x001F25A8 File Offset: 0x001F07A8
		public GameObject GetFirstObjectWithPart(Type Type)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Type))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x001F25F0 File Offset: 0x001F07F0
		public GameObject GetFirstObjectWithPartExcept(string Part, GameObject skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x001F2648 File Offset: 0x001F0848
		public GameObject GetFirstVisibleObjectWithPartExcept(string Part, GameObject skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part) && this.Objects[i].IsVisible())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x001F26B4 File Offset: 0x001F08B4
		public GameObject GetFirstObjectWithPartExcept(string Part, GameObject skip, int IgnoreEasierThan, GameObject Looker)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part))
				{
					int? num = this.Objects[i].Con(Looker, false);
					if (num != null)
					{
						int? num2 = num;
						if ((num2.GetValueOrDefault() >= IgnoreEasierThan) & (num2 != null))
						{
							return this.Objects[i];
						}
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x001F2748 File Offset: 0x001F0948
		public GameObject GetFirstVisibleObjectWithPartExcept(string Part, GameObject skip, int IgnoreEasierThan, GameObject Looker)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part) && this.Objects[i].IsVisible())
				{
					int? num = this.Objects[i].Con(Looker, false);
					if (num != null)
					{
						int? num2 = num;
						if ((num2.GetValueOrDefault() >= IgnoreEasierThan) & (num2 != null))
						{
							return this.Objects[i];
						}
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x001F27F4 File Offset: 0x001F09F4
		public GameObject GetFirstObjectWithPart(List<string> Parts)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				int j = 0;
				int count2 = Parts.Count;
				while (j < count2)
				{
					if (this.Objects[i].HasPart(Parts[j]))
					{
						return this.Objects[i];
					}
					j++;
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x001F2854 File Offset: 0x001F0A54
		public GameObject GetFirstObjectWithPart(string Part, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetFirstObjectWithPart(Part);
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x001F28BC File Offset: 0x001F0ABC
		public GameObject GetFirstObjectWithPart(string Part, Predicate<GameObject> Filter1, Predicate<GameObject> Filter2)
		{
			if (Filter1 == null)
			{
				if (Filter2 == null)
				{
					return this.GetFirstObjectWithPart(Part);
				}
				return this.GetFirstObjectWithPart(Part, Filter2);
			}
			else
			{
				if (Filter2 == null)
				{
					return this.GetFirstObjectWithPart(Part, Filter1);
				}
				int i = 0;
				int count = this.Objects.Count;
				while (i < count)
				{
					if (this.Objects[i].HasPart(Part) && Filter1(this.Objects[i]) && Filter2(this.Objects[i]))
					{
						return this.Objects[i];
					}
					i++;
				}
				return null;
			}
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x001F2950 File Offset: 0x001F0B50
		public GameObject GetFirstObjectWithPartExcept(string Part, Predicate<GameObject> Filter, GameObject skip = null)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x001F29BC File Offset: 0x001F0BBC
		public GameObject GetFirstVisibleObjectWithPartExcept(string Part, Predicate<GameObject> Filter, GameObject skip = null)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]) && this.Objects[i].IsVisible())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x001F2A3C File Offset: 0x001F0C3C
		public GameObject GetFirstObjectWithPartExcept(string Part, Predicate<GameObject> Filter, GameObject skip, int IgnoreEasierThan, GameObject Looker)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					int? num = this.Objects[i].Con(Looker, false);
					if (num != null)
					{
						int? num2 = num;
						if ((num2.GetValueOrDefault() >= IgnoreEasierThan) & (num2 != null))
						{
							return this.Objects[i];
						}
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D5 RID: 25813 RVA: 0x001F2AE8 File Offset: 0x001F0CE8
		public GameObject GetFirstVisibleObjectWithPartExcept(string Part, Predicate<GameObject> Filter, GameObject skip, int IgnoreEasierThan, GameObject Looker)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]) && this.Objects[i].IsVisible())
				{
					int? num = this.Objects[i].Con(Looker, false);
					if (num != null)
					{
						int? num2 = num;
						if ((num2.GetValueOrDefault() >= IgnoreEasierThan) & (num2 != null))
						{
							return this.Objects[i];
						}
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D6 RID: 25814 RVA: 0x001F2BAC File Offset: 0x001F0DAC
		public GameObject GetFirstObjectWithPart(string Part, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.HasPart(Part) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					return gameObject;
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D7 RID: 25815 RVA: 0x001F2C30 File Offset: 0x001F0E30
		public GameObject GetFirstRealObject(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Physics != null && gameObject.Physics.IsReal && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					return gameObject;
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D8 RID: 25816 RVA: 0x001F2CBC File Offset: 0x001F0EBC
		public GameObject GetFirstObjectWithAnyPart(List<string> Parts)
		{
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(Parts[j]))
					{
						return this.Objects[i];
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x001F2D1C File Offset: 0x001F0F1C
		public GameObject GetFirstObjectWithAnyPart(List<string> Parts, Predicate<GameObject> filter)
		{
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(Parts[j]) && filter(this.Objects[i]))
					{
						return this.Objects[i];
					}
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x001F2D90 File Offset: 0x001F0F90
		public GameObject GetHighestRenderLayerObject()
		{
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064DB RID: 25819 RVA: 0x001F2E00 File Offset: 0x001F1000
		public GameObject GetHighestRenderLayerObject(Predicate<GameObject> Filter)
		{
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer) && Filter(this.Objects[i]))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x001F2E84 File Offset: 0x001F1084
		public GameObject GetHighestRenderLayerObjectWithPart(string Part)
		{
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer) && this.Objects[i].HasPart(Part))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064DD RID: 25821 RVA: 0x001F2F08 File Offset: 0x001F1108
		public GameObject GetHighestRenderLayerObjectWithPart(string Part, Predicate<GameObject> Filter)
		{
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer) && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x001F2FA0 File Offset: 0x001F11A0
		public GameObject GetHighestRenderLayerObjectWithAnyPart(List<string> Parts)
		{
			GameObject gameObject = null;
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				GameObject gameObject2 = this.Objects[i];
				if (gameObject2.Render != null && (gameObject == null || gameObject2.Render.RenderLayer > gameObject.Render.RenderLayer))
				{
					for (int j = 0; j < count; j++)
					{
						if (gameObject2.HasPart(Parts[j]))
						{
							gameObject = gameObject2;
							break;
						}
					}
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x001F3028 File Offset: 0x001F1228
		public GameObject GetHighestRenderLayerObjectWithAnyPart(List<string> Parts, Predicate<GameObject> Filter)
		{
			GameObject gameObject = null;
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				GameObject gameObject2 = this.Objects[i];
				if (gameObject2.Render != null && (gameObject == null || gameObject2.Render.RenderLayer > gameObject.Render.RenderLayer))
				{
					for (int j = 0; j < count; j++)
					{
						if (gameObject2.HasPart(Parts[j]) && Filter(gameObject2))
						{
							gameObject = gameObject2;
							break;
						}
					}
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x001F30BC File Offset: 0x001F12BC
		public GameObject GetHighestRenderLayerInteractableObjectFor(GameObject Actor)
		{
			GameObject gameObject = null;
			bool flag = this.IsSolidFor(Actor, false);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((!flag || this.Objects[i].CanInteractInCellWithSolid(Actor)) && this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x001F314C File Offset: 0x001F134C
		public GameObject GetHighestRenderLayerInteractableObjectFor(GameObject Actor, Predicate<GameObject> Filter)
		{
			GameObject gameObject = null;
			bool flag = this.IsSolidFor(Actor, false);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((!flag || this.Objects[i].CanInteractInCellWithSolid(Actor)) && this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer) && Filter(this.Objects[i]))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x001F31F0 File Offset: 0x001F13F0
		public GameObject GetHighestRenderLayerInteractableObjectWithPartFor(GameObject Actor, string Part)
		{
			GameObject gameObject = null;
			bool flag = this.IsSolidFor(Actor, false);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((!flag || this.Objects[i].CanInteractInCellWithSolid(Actor)) && this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer) && this.Objects[i].HasPart(Part))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x001F3294 File Offset: 0x001F1494
		public GameObject GetHighestRenderLayerInteractableObjectWithPartFor(GameObject Actor, string Part, Predicate<GameObject> Filter)
		{
			GameObject gameObject = null;
			bool flag = this.IsSolidFor(Actor, false);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if ((!flag || this.Objects[i].CanInteractInCellWithSolid(Actor)) && this.Objects[i].Render != null && (gameObject == null || this.Objects[i].Render.RenderLayer > gameObject.Render.RenderLayer) && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					gameObject = this.Objects[i];
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x001F3350 File Offset: 0x001F1550
		public GameObject GetHighestRenderLayerInteractableObjectWithAnyPartFor(GameObject Actor, List<string> Parts)
		{
			GameObject gameObject = null;
			bool flag = this.IsSolidFor(Actor, false);
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				GameObject gameObject2 = this.Objects[i];
				if ((!flag || gameObject2.CanInteractInCellWithSolid(Actor)) && gameObject2.Render != null && (gameObject == null || gameObject2.Render.RenderLayer > gameObject.Render.RenderLayer))
				{
					for (int j = 0; j < count; j++)
					{
						if (gameObject2.HasPart(Parts[j]))
						{
							gameObject = gameObject2;
							break;
						}
					}
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x001F33F0 File Offset: 0x001F15F0
		public GameObject GetHighestRenderLayerInteractableObjectWithAnyPartFor(GameObject Actor, List<string> Parts, Predicate<GameObject> Filter)
		{
			GameObject gameObject = null;
			bool flag = this.IsSolidFor(Actor, false);
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				GameObject gameObject2 = this.Objects[i];
				if ((!flag || gameObject2.CanInteractInCellWithSolid(Actor)) && gameObject2.Render != null && (gameObject == null || gameObject2.Render.RenderLayer > gameObject.Render.RenderLayer))
				{
					for (int j = 0; j < count; j++)
					{
						if (gameObject2.HasPart(Parts[j]) && Filter(gameObject2))
						{
							gameObject = gameObject2;
							break;
						}
					}
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x001F349C File Offset: 0x001F169C
		public bool HasObjectWithEffect(string EffectType)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasEffect(EffectType))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064E7 RID: 25831 RVA: 0x001F34D8 File Offset: 0x001F16D8
		public bool HasObjectWithEffect(Type EffectType)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasEffect(EffectType))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x001F3514 File Offset: 0x001F1714
		public bool HasObjectWithEffect(string EffectType, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasEffect(EffectType) && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064E9 RID: 25833 RVA: 0x001F3564 File Offset: 0x001F1764
		public bool HasObjectWithEffect(Type EffectType, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasEffect(EffectType) && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x001F35B4 File Offset: 0x001F17B4
		public bool HasRelevantFriendlyExcept(GameObject Actor, GameObject Except)
		{
			GameObject[] array = this.Objects.GetArray();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = array[i];
				if (gameObject.IsCombatObject() && gameObject != Except && !Actor.IsHostileTowards(gameObject) && gameObject.PhaseMatches(Actor))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x001F360C File Offset: 0x001F180C
		public GameObject GetFirstObjectWithEffect(string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasEffect(Name))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x001F3654 File Offset: 0x001F1854
		public List<GameObject> GetObjectsWithEffect(string Name)
		{
			List<GameObject> list = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasEffect(Name))
				{
					if (list == null)
					{
						list = new List<GameObject> { this.Objects[i] };
					}
					else
					{
						list.Add(this.Objects[i]);
					}
				}
				i++;
			}
			return list;
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x001F36C0 File Offset: 0x001F18C0
		public bool HasFurniture()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].isFurniture())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x001F36FC File Offset: 0x001F18FC
		public bool HasCombatObject(bool NoBrainOnly = false)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsCombatObject())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x001F3738 File Offset: 0x001F1938
		public bool HasVisibleCombatObject(bool NoBrainOnly = false)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsVisible() && this.Objects[i].IsCombatObject())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x001F3788 File Offset: 0x001F1988
		public bool HasRealObject()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsReal)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x001F37C3 File Offset: 0x001F19C3
		public Cell GetInventoryCell()
		{
			return this;
		}

		// Token: 0x060064F2 RID: 25842 RVA: 0x001F37C6 File Offset: 0x001F19C6
		public Zone GetInventoryZone()
		{
			return this.ParentZone;
		}

		// Token: 0x060064F3 RID: 25843 RVA: 0x001F37CE File Offset: 0x001F19CE
		public bool InventoryContains(GameObject Object)
		{
			return this.HasObject(Object);
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x001F37D7 File Offset: 0x001F19D7
		public bool HasObject()
		{
			return this.Objects.Count > 0;
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x001F37E7 File Offset: 0x001F19E7
		public bool HasObject(GameObject Object)
		{
			return this.Objects.Contains(Object);
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x001F37F8 File Offset: 0x001F19F8
		public bool HasObject(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x001F383C File Offset: 0x001F1A3C
		public bool HasObject(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.Objects.Count > 0;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x001F388C File Offset: 0x001F1A8C
		public bool HasObjectOtherThan(Predicate<GameObject> Filter, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x001F38D8 File Offset: 0x001F1AD8
		public bool HasObjectWithPart(string Part)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Part))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x001F3914 File Offset: 0x001F1B14
		public bool HasObjectWithPart(Type Part)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Part))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x001F3950 File Offset: 0x001F1B50
		public bool HasObjectWithPartOtherThan(Type Part, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPart(Part))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x001F399C File Offset: 0x001F1B9C
		public bool HasObjectWithPartExcept(string Part, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPart(Part))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064FD RID: 25853 RVA: 0x001F39E8 File Offset: 0x001F1BE8
		public bool HasVisibleObjectWithPartExcept(string Part, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPart(Part) && this.Objects[i].IsVisible())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x001F3A48 File Offset: 0x001F1C48
		public bool HasObjectWithPart(string Part, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x001F3A98 File Offset: 0x001F1C98
		public bool HasObjectWithPartExcept(string Part, Predicate<GameObject> Filter, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006500 RID: 25856 RVA: 0x001F3AF8 File Offset: 0x001F1CF8
		public bool HasVisibleObjectWithPartExcept(string Part, Predicate<GameObject> Filter, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPart(Part) && Filter(this.Objects[i]) && this.Objects[i].IsVisible())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006501 RID: 25857 RVA: 0x001F3B6C File Offset: 0x001F1D6C
		public bool HasObjectWithPartOtherThan(string Part, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].HasPart(Part))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x001F3BB8 File Offset: 0x001F1DB8
		public bool HasObjectWithAnyPart(List<string> Parts)
		{
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(Parts[j]))
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006503 RID: 25859 RVA: 0x001F3C10 File Offset: 0x001F1E10
		public bool HasObjectWithAnyPart(List<string> Parts, Predicate<GameObject> Filter)
		{
			int count = Parts.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(Parts[j]) && Filter(this.Objects[i]))
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x001F3C7C File Offset: 0x001F1E7C
		public bool HasObjectWithBlueprint(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x001F3CC0 File Offset: 0x001F1EC0
		public bool HasObjectWithBlueprintEndsWith(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint != null && this.Objects[i].Blueprint.EndsWith(Blueprint))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x001F3D14 File Offset: 0x001F1F14
		public bool HasObjectWithBlueprintStartsWith(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint != null && this.Objects[i].Blueprint.StartsWith(Blueprint))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x001F3D68 File Offset: 0x001F1F68
		public List<GameObject> GetObjectsInCell()
		{
			return new List<GameObject>(this.Objects);
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x001F3D78 File Offset: 0x001F1F78
		public int GetAdjacentObjectTestCount(Predicate<GameObject> test, int Radius = 1)
		{
			int num = 0;
			Func<GameObject, bool> <>9__0;
			foreach (Cell cell in this.GetLocalAdjacentCells(Radius, false))
			{
				int num2 = num;
				IEnumerable<GameObject> objects = cell.Objects;
				Func<GameObject, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (GameObject o) => test(o));
				}
				num = num2 + objects.Where<GameObject>(func).Count<GameObject>();
			}
			return num;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x001F3E0C File Offset: 0x001F200C
		public int GetAdjacentObjectCount(string Blueprint, int Radius = 1)
		{
			int num = 0;
			foreach (Cell cell in this.GetLocalAdjacentCells(Radius, false))
			{
				num += cell.GetObjectCount(Blueprint);
			}
			return num;
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x001F3E68 File Offset: 0x001F2068
		public int GetObjectCount()
		{
			return this.Objects.Count;
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x001F3E78 File Offset: 0x001F2078
		public int GetObjectCount(string Blueprint)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x001F3EC0 File Offset: 0x001F20C0
		public int GetObjectCount(Predicate<GameObject> Filter)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x001F3F00 File Offset: 0x001F2100
		public bool AnyObject()
		{
			return this.Objects.Count > 0;
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x001F3F10 File Offset: 0x001F2110
		public bool AnyObject(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x001F3F54 File Offset: 0x001F2154
		public bool AnyObject(Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x001F3F90 File Offset: 0x001F2190
		public bool AnyTakeable()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				XRL.World.Parts.Physics physics = this.Objects[i].Physics;
				if (physics != null && physics.Takeable)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x001F3FD7 File Offset: 0x001F21D7
		public GameObject GetObjectInCell(int n)
		{
			if (this.Objects.Count > n)
			{
				return this.Objects[n];
			}
			return null;
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x001F3FF8 File Offset: 0x001F21F8
		public GameObject FindObject(Predicate<GameObject> test)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != null && test(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x001F4050 File Offset: 0x001F2250
		public GameObject FindObjectExcept(Predicate<GameObject> test, GameObject skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != null && this.Objects[i] != skip && test(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x001F40B4 File Offset: 0x001F22B4
		public GameObject FindObject(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x001F4100 File Offset: 0x001F2300
		public GameObject FindObjectExcept(string Blueprint, GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint && this.Objects[i] != Skip)
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x001F415C File Offset: 0x001F235C
		public void FindObjects(List<GameObject> List, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				List.AddRange(this.Objects);
				return;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != null && Filter(this.Objects[i]))
				{
					List.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x001F41C5 File Offset: 0x001F23C5
		public GameObject ClearAndAddObject(string Blueprint)
		{
			this.Clear(null, false, false, null);
			return this.AddObject(Blueprint, null, null, null, null);
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x001F41E0 File Offset: 0x001F23E0
		public GameObject AddTableObject(string Table)
		{
			GameObject gameObject = GameObjectFactory.create(PopulationManager.RollOneFrom(Table, null, null).Blueprint);
			if (gameObject == null)
			{
				throw new Exception("failed to roll a population result from " + Table);
			}
			return this.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x001F422C File Offset: 0x001F242C
		public List<GameObject> AddPopulation(string Table)
		{
			List<GameObject> list = new List<GameObject>();
			foreach (PopulationResult populationResult in PopulationManager.Generate(Table, new Dictionary<string, string> { 
			{
				"zonetier",
				ZoneManager.zoneGenerationContextTier.ToString()
			} }, null))
			{
				list.Add(this.AddObject(populationResult.Blueprint, null, null, null, null));
			}
			return list;
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x001F42B0 File Offset: 0x001F24B0
		public void AddObject(string Blueprint, int Number, List<GameObject> Tracking = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			for (int i = 0; i < Number; i++)
			{
				this.AddObject(Blueprint, null, Tracking, BeforeObjectCreated, AfterObjectCreated);
			}
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x001F42D8 File Offset: 0x001F24D8
		public GameObject ClearAndAddObject(GameObject obj, bool Clear = true, List<GameObject> Tracking = null)
		{
			if (Clear)
			{
				this.Clear(null, false, false, null);
			}
			return this.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, Tracking, null);
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x001F4309 File Offset: 0x001F2509
		public GameObject ClearAndAddObject(string Blueprint, bool Clear = true, List<GameObject> Tracking = null)
		{
			if (Clear)
			{
				this.Clear(null, false, false, null);
			}
			return this.AddObject(Blueprint, null, Tracking, null, null);
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x001F4324 File Offset: 0x001F2524
		public void ClearAndAddObject(string Blueprint, int Number, bool Clear = true, List<GameObject> Tracking = null)
		{
			if (Clear)
			{
				this.Clear(null, false, false, null);
			}
			this.AddObject(Blueprint, Number, Tracking, null, null);
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x001F4340 File Offset: 0x001F2540
		public GameObject RequireObject(string Blueprint, string Context = null, List<GameObject> Tracking = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			if (this.HasObject(Blueprint))
			{
				return this.FindObject(Blueprint);
			}
			GameObject gameObject = GameObject.Create(Blueprint, 0, 0, null, BeforeObjectCreated, AfterObjectCreated, Context, null);
			if (gameObject == null)
			{
				throw new Exception("failed to generate object from blueprint " + Blueprint);
			}
			return this.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, Tracking, null);
		}

		// Token: 0x0600651F RID: 25887 RVA: 0x001F439C File Offset: 0x001F259C
		public GameObject AddObject(string Blueprint, string Context = null, List<GameObject> Tracking = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			GameObject gameObject = GameObject.Create(Blueprint, 0, 0, null, BeforeObjectCreated, AfterObjectCreated, Context, null);
			if (gameObject == null)
			{
				throw new Exception("failed to generate object from blueprint " + Blueprint);
			}
			return this.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, Tracking, null);
		}

		// Token: 0x06006520 RID: 25888 RVA: 0x001F43E8 File Offset: 0x001F25E8
		public GameObject AddObject(string Blueprint, Action<GameObject> BeforeObjectCreated, List<GameObject> Tracking = null)
		{
			GameObject gameObject = GameObject.Create(Blueprint, 0, 0, null, BeforeObjectCreated, null, null, null);
			if (gameObject == null)
			{
				throw new Exception("failed to generate object from blueprint " + Blueprint);
			}
			return this.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, Tracking, null);
		}

		// Token: 0x06006521 RID: 25889 RVA: 0x001F4430 File Offset: 0x001F2630
		public GameObject AddObjectToInventory(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, string Context = null, IEvent ParentEvent = null)
		{
			return this.AddObject(Object, false, false, false, NoStack, Silent, true, FlushTransient, null, null, null, Actor, null, null, null, ParentEvent);
		}

		// Token: 0x06006522 RID: 25890 RVA: 0x001F445C File Offset: 0x001F265C
		public GameObject AddObject(GameObject Object, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, bool Silent = false, bool Repaint = true, bool FlushTransient = true, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, List<GameObject> Tracking = null, IEvent ParentEvent = null)
		{
			this.minimapCacheValid = false;
			this.OccludeCache = -1;
			if (FlushTransient)
			{
				this.FlushNavigationCache();
				this.TransientCache = 0;
			}
			XRL.World.Parts.Physics physics = Object.Physics;
			if (physics != null && !physics.EnterCell(this))
			{
				return Object;
			}
			this.Objects.Add(Object);
			if (Tracking != null)
			{
				Tracking.Add(Object);
			}
			Object.ProcessEnterCell(this, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, ParentEvent);
			Object.ProcessEnteredCell(this, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, ParentEvent);
			Object.ProcessObjectEnteredCell(this, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, ParentEvent);
			if (Object.IsPlayer())
			{
				Zone.SoundMapDirty = true;
			}
			if (Repaint && this.ParentZone.Built)
			{
				this.CheckPaintWallsAround(Object);
				this.CheckPaintLiquidsAround(Object);
				this.CheckSoundWall(Object, 1);
			}
			if (!Object.Live && this.ParentZone.Built && this.ParentZone.IsActive())
			{
				Object.MakeLive();
			}
			return Object;
		}

		// Token: 0x06006523 RID: 25891 RVA: 0x001F456C File Offset: 0x001F276C
		protected GameObject AddObjectWithoutEvents(GameObject GO, List<GameObject> Tracking = null)
		{
			XRL.World.Parts.Physics physics = GO.Physics;
			if (physics != null && !physics.EnterCell(this))
			{
				return GO;
			}
			this.Objects.Add(GO);
			if (Tracking != null)
			{
				Tracking.Add(GO);
			}
			return GO;
		}

		// Token: 0x06006524 RID: 25892 RVA: 0x001F459F File Offset: 0x001F279F
		public void SetReachable(bool State)
		{
			if (this.ParentZone.ReachableMap == null)
			{
				this.ParentZone.ClearReachableMap();
			}
			this.ParentZone.ReachableMap[this.X, this.Y] = State;
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x001F45D6 File Offset: 0x001F27D6
		public bool Explored
		{
			get
			{
				return this.ParentZone.GetExplored(this.X, this.Y);
			}
		}

		// Token: 0x06006526 RID: 25894 RVA: 0x001F45EF File Offset: 0x001F27EF
		public void SetExplored(bool State)
		{
			this.ParentZone.SetExplored(this.X, this.Y, State);
		}

		// Token: 0x06006527 RID: 25895 RVA: 0x001F4609 File Offset: 0x001F2809
		public void SetExplored()
		{
			this.ParentZone.SetExplored(this.X, this.Y, true);
		}

		// Token: 0x06006528 RID: 25896 RVA: 0x001F4623 File Offset: 0x001F2823
		[Obsolete("Legacy amnesia behaviour, use SetExplored")]
		public void SetFakeUnexplored(bool State)
		{
			this.ParentZone.SetFakeUnexplored(this.X, this.Y, State);
		}

		// Token: 0x06006529 RID: 25897 RVA: 0x001F463D File Offset: 0x001F283D
		[Obsolete("Legacy amnesia behaviour, use SetExplored")]
		public void MakeFakeUnexplored()
		{
			this.ParentZone.SetFakeUnexplored(this.X, this.Y, true);
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x001F4657 File Offset: 0x001F2857
		public bool IsExplored()
		{
			return this.ParentZone.GetExplored(this.X, this.Y);
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x001F4670 File Offset: 0x001F2870
		[Obsolete("Legacy amnesia behaviour, use IsExplored")]
		public bool IsReallyExplored()
		{
			return this.IsExplored();
		}

		// Token: 0x0600652C RID: 25900 RVA: 0x001F4678 File Offset: 0x001F2878
		public bool IsExploredFor(GameObject obj)
		{
			return this.ParentZone.GetExplored(this.X, this.Y);
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x001F4691 File Offset: 0x001F2891
		public bool IsLit()
		{
			return this.GetLight() > LightLevel.None;
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x001F469C File Offset: 0x001F289C
		public bool IsVisible()
		{
			return this.ParentZone != null && this.ParentZone.IsActive() && this.ParentZone.GetVisibility(this.X, this.Y);
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x001F46CC File Offset: 0x001F28CC
		public Cell getClosestCellFromList(List<Cell> cells)
		{
			cells.Sort((Cell a, Cell b) => a.ManhattanDistanceTo(this).CompareTo(b.ManhattanDistanceTo(this)));
			return cells.FirstOrDefault<Cell>();
		}

		// Token: 0x06006530 RID: 25904 RVA: 0x001F46E6 File Offset: 0x001F28E6
		public void PaintWallsAround()
		{
			ZoneManager.PaintWalls(this.ParentZone, this.X - 1, this.Y - 1, this.X + 1, this.Y + 1);
		}

		// Token: 0x06006531 RID: 25905 RVA: 0x001F4714 File Offset: 0x001F2914
		public void CheckPaintWallsAround(GameObject GO)
		{
			if (GO.HasTagOrProperty("PaintedWall") || GO.HasTagOrProperty("PaintedFence") || GO.HasTagOrProperty("ForceRepaintSolid"))
			{
				this.PaintWallsAround();
			}
			if (GO.HasTagOrProperty("PaintedCrystal"))
			{
				ZoneManager.PaintWalls(this.ParentZone, this.X - 3, this.Y - 3, this.X + 3, this.Y + 3);
			}
		}

		// Token: 0x06006532 RID: 25906 RVA: 0x001F4786 File Offset: 0x001F2986
		public void PaintLiquidsAround()
		{
			ZoneManager.PaintWater(this.ParentZone, this.X - 1, this.Y - 1, this.X + 1, this.Y + 1);
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x001F47B3 File Offset: 0x001F29B3
		public void CheckPaintLiquidsAround(GameObject GO)
		{
			if (GO.HasTagOrProperty("PaintedLiquidAtlas") || GO.HasTagOrProperty("ForceRepaintLiquid"))
			{
				this.PaintLiquidsAround();
			}
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x001F47D8 File Offset: 0x001F29D8
		public List<GameObject> RemoveObjects(Predicate<GameObject> test)
		{
			List<GameObject> list = Event.NewGameObjectList();
			foreach (GameObject gameObject in this.GetObjects())
			{
				if (test(gameObject))
				{
					list.Add(gameObject);
				}
			}
			list.ForEach(delegate(GameObject o)
			{
				this.RemoveObject(o, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			});
			return list;
		}

		// Token: 0x06006535 RID: 25909 RVA: 0x001F4850 File Offset: 0x001F2A50
		public bool RemoveObjectFromInventory(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, string Context = null, IEvent ParentEvent = null)
		{
			return this.Objects.Contains(Object) && this.RemoveObject(Object, false, false, false, Silent, NoStack, true, FlushTransient, null, null, null, Actor, null, null, ParentEvent);
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x001F4888 File Offset: 0x001F2A88
		public bool RemoveObject(GameObject Object, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool Silent = false, bool NoStack = false, bool Repaint = true, bool FlushTransient = true, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			GameObject gameObject = null;
			Object.ProcessLeavingCell(this, ref gameObject, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, ParentEvent);
			if (FlushTransient)
			{
				this.TransientCache = 0;
				this.minimapCacheValid = false;
				this.OccludeCache = -1;
				this.FlushNavigationCache();
			}
			this.Objects.Remove(Object);
			Object.ProcessLeftCell(this, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, ParentEvent);
			if (Repaint && this.ParentZone.Built)
			{
				this.CheckPaintWallsAround(Object);
				this.CheckPaintLiquidsAround(Object);
				this.CheckSoundWall(Object, 0);
			}
			return true;
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x001F4928 File Offset: 0x001F2B28
		public Cell getClosestPassableCell()
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsPassable(null, true));
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x001F498C File Offset: 0x001F2B8C
		public Cell getClosestPassableCell(Predicate<Cell> filter)
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsPassable(null, true) && filter(c));
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x001F49F4 File Offset: 0x001F2BF4
		public Cell getClosestPassableCell(Cell alsoClosestTo)
		{
			if (alsoClosestTo == null)
			{
				return this.getClosestPassableCell();
			}
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsPassable(null, true));
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => (c1.Location.Distance(this.Location) + c1.Location.Distance(alsoClosestTo.Location)).CompareTo(c2.Location.Distance(this.Location) + c2.Location.Distance(alsoClosestTo.Location)));
			}
			return cells[0];
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x001F4A7C File Offset: 0x001F2C7C
		public Cell getClosestPassableCellExcept(List<Cell> except)
		{
			List<Cell> cellsViaEventList = this.ParentZone.GetCellsViaEventList((Cell c) => c.IsPassable(null, true) && !except.Contains(c));
			if (cellsViaEventList.Count == 0)
			{
				return this;
			}
			if (cellsViaEventList.Count > 1)
			{
				cellsViaEventList.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cellsViaEventList[0];
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x001F4AE4 File Offset: 0x001F2CE4
		public Cell getClosestPassableCellFor(GameObject who)
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsPassable(who, true));
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x001F4B4C File Offset: 0x001F2D4C
		public Cell getClosestPassableCellForExcept(GameObject who, List<Cell> except)
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsPassable(who, true) && !except.Contains(c));
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x001F4BB8 File Offset: 0x001F2DB8
		public Cell getClosestEmptyCell()
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsEmpty());
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x001F4C1C File Offset: 0x001F2E1C
		public Cell getClosestEmptyCell(Cell alsoClosestTo)
		{
			if (alsoClosestTo == null)
			{
				return this.getClosestEmptyCell();
			}
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsEmpty());
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => (c1.Location.Distance(this.Location) + c1.Location.Distance(alsoClosestTo.Location)).CompareTo(c2.Location.Distance(this.Location) + c2.Location.Distance(alsoClosestTo.Location)));
			}
			return cells[0];
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x001F4CA4 File Offset: 0x001F2EA4
		public Cell getClosestEmptyCellExcept(List<Cell> except)
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsEmpty() && !except.Contains(c));
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x001F4D0C File Offset: 0x001F2F0C
		public Cell getClosestReachableCell()
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsEmpty() && c.IsReachable());
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x001F4D70 File Offset: 0x001F2F70
		public Cell getClosestReachableCellFor(GameObject who)
		{
			List<Cell> cells = this.ParentZone.GetCells((Cell c) => c.IsEmptyFor(who) && c.IsReachable());
			if (cells.Count == 0)
			{
				return this;
			}
			if (cells.Count > 1)
			{
				cells.Sort((Cell c1, Cell c2) => c1.Location.Distance(this.Location).CompareTo(c2.Location.Distance(this.Location)));
			}
			return cells[0];
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x001F4DD5 File Offset: 0x001F2FD5
		public bool IsReachable()
		{
			return this.ParentZone.IsReachable(this.X, this.Y);
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x001F4DF0 File Offset: 0x001F2FF0
		public int DistanceToEdge()
		{
			int num = Math.Min(this.ParentZone.Width - this.X, this.X);
			int num2 = Math.Min(this.ParentZone.Height - this.Y, this.Y);
			return Math.Min(num, num2);
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x001F4E3E File Offset: 0x001F303E
		public int DistanceTo(GameObject GO)
		{
			return this.PathDistanceTo((GO != null) ? GO.CurrentCell : null);
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x001F4E52 File Offset: 0x001F3052
		public int DistanceTo(Cell C)
		{
			return this.PathDistanceTo(C);
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x001F4E5B File Offset: 0x001F305B
		public int CosmeticDistanceTo(Point2D location)
		{
			return this.CosmeticDistanceTo(location.x, location.y);
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x001F4E6F File Offset: 0x001F306F
		public int CosmeticDistanceto(Location2D location)
		{
			return this.CosmeticDistanceTo(location.X, location.Y);
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x001F4E83 File Offset: 0x001F3083
		public int CosmeticDistanceTo(int x, int y)
		{
			return (int)Math.Sqrt((double)((float)(x - this.X) * 0.6666f * ((float)(x - this.X) * 0.6666f) + (float)((y - this.Y) * (y - this.Y))));
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x001F4EBE File Offset: 0x001F30BE
		public int DistanceTo(int x, int y)
		{
			return Math.Max(Math.Abs(this.X - x), Math.Abs(this.Y - y));
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x001F4EDF File Offset: 0x001F30DF
		public int ManhattanDistanceTo(GameObject Object)
		{
			if (Object == null)
			{
				return 9999999;
			}
			return this.ManhattanDistanceTo(Object.CurrentCell);
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x001F4EF8 File Offset: 0x001F30F8
		public int ManhattanDistanceTo(Cell C)
		{
			if (C == null)
			{
				return 9999999;
			}
			if (C.ParentZone.IsWorldMap() != this.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			if (C.ParentZone == this.ParentZone)
			{
				return Math.Abs(C.X - this.X) + Math.Abs(C.Y - this.Y);
			}
			if (C.ParentZone.ZoneWorld != this.ParentZone.ZoneWorld)
			{
				return 9999999;
			}
			int num = C.ParentZone.GetZonewX() * Definitions.Width * 80 + C.ParentZone.GetZoneX() * 80 + C.X;
			int num2 = C.ParentZone.GetZonewY() * Definitions.Height * 25 + C.ParentZone.GetZoneY() * 25 + C.Y;
			int zoneZ = C.ParentZone.GetZoneZ();
			int num3 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
			int num4 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
			int zoneZ2 = this.ParentZone.GetZoneZ();
			if (num == num3 && num2 == num4)
			{
				return Math.Abs(zoneZ2 - zoneZ);
			}
			return Math.Abs(num3 - num) + Math.Abs(num4 - num2) + Math.Abs(zoneZ2 - zoneZ);
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x001F507B File Offset: 0x001F327B
		public int ManhattanDistanceTo(int X, int Y)
		{
			return Math.Abs(this.X - X) + Math.Abs(this.Y - Y);
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x001F5098 File Offset: 0x001F3298
		public int PathDistanceTo(int X, int Y)
		{
			return Math.Max(Math.Abs(this.X - X), Math.Abs(this.Y - Y));
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x001F50B9 File Offset: 0x001F32B9
		public int PathDistanceTo(Location2D L)
		{
			if (L == null)
			{
				return 9999999;
			}
			return Math.Max(Math.Abs(L.X - this.X), Math.Abs(L.Y - this.Y));
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x001F50F4 File Offset: 0x001F32F4
		public int PathDistanceTo(Cell C)
		{
			if (C == null)
			{
				return 9999999;
			}
			if (C.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			if (this.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			if (C.ParentZone == this.ParentZone)
			{
				return Math.Max(Math.Abs(C.X - this.X), Math.Abs(C.Y - this.Y));
			}
			if (C.ParentZone.ZoneWorld != this.ParentZone.ZoneWorld)
			{
				return 9999999;
			}
			int num = C.ParentZone.GetZonewX() * Definitions.Width * 80 + C.ParentZone.GetZoneX() * 80 + C.X;
			int num2 = C.ParentZone.GetZonewY() * Definitions.Height * 25 + C.ParentZone.GetZoneY() * 25 + C.Y;
			int zoneZ = C.ParentZone.GetZoneZ();
			int num3 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
			int num4 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
			int zoneZ2 = this.ParentZone.GetZoneZ();
			if (num == num3 && num2 == num4)
			{
				return Math.Abs(zoneZ2 - zoneZ);
			}
			return Math.Max(Math.Max(Math.Abs(num3 - num), Math.Abs(num4 - num2)), Math.Abs(zoneZ2 - zoneZ));
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x001F528C File Offset: 0x001F348C
		public Point2D PathDifferenceTo(Cell C)
		{
			if (C.ParentZone != this.ParentZone)
			{
				int num = C.ParentZone.GetZonewX() * Definitions.Width * 80 + C.ParentZone.GetZoneX() * 80 + C.X;
				int num2 = C.ParentZone.GetZonewY() * Definitions.Height * 25 + C.ParentZone.GetZoneY() * 25 + C.Y;
				int num3 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
				int num4 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
				return new Point2D(num3 - num, num4 - num2);
			}
			return new Point2D(this.X - C.X, this.Y - C.Y);
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x001F537F File Offset: 0x001F357F
		public double RealDistanceTo(GameObject GO)
		{
			if (GO == null)
			{
				return 9999999.0;
			}
			if (GO.Physics == null)
			{
				return 9999999.0;
			}
			return this.RealDistanceTo(GO.CurrentCell, true);
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x001F53B0 File Offset: 0x001F35B0
		public double RealDistanceTo(Cell C, bool indefiniteWorld = true)
		{
			if (C == null)
			{
				return 9999999.0;
			}
			if (indefiniteWorld)
			{
				if (C.ParentZone.IsWorldMap())
				{
					return 9999999.0;
				}
				if (this.ParentZone.IsWorldMap())
				{
					return 9999999.0;
				}
			}
			if (C.ParentZone == this.ParentZone)
			{
				return Math.Sqrt((double)((C.X - this.X) * (C.X - this.X) + (C.Y - this.Y) * (C.Y - this.Y)));
			}
			if (C.ParentZone.ZoneWorld != this.ParentZone.ZoneWorld)
			{
				return 9999999.0;
			}
			int num = C.ParentZone.GetZonewX() * Definitions.Width * 80 + C.ParentZone.GetZoneX() * 80 + C.X;
			int num2 = C.ParentZone.GetZonewY() * Definitions.Height * 25 + C.ParentZone.GetZoneY() * 25 + C.Y;
			int zoneZ = C.ParentZone.GetZoneZ();
			int num3 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
			int num4 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
			int zoneZ2 = this.ParentZone.GetZoneZ();
			return Math.Sqrt((double)((num - num3) * (num - num3) + (num2 - num4) * (num2 - num4) + (zoneZ - zoneZ2) * (zoneZ - zoneZ2)));
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x001F5558 File Offset: 0x001F3758
		public int DistanceToRespectStairs(Cell C)
		{
			if (C == null)
			{
				return 9999999;
			}
			if (C.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			if (this.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			if (C.ParentZone == this.ParentZone)
			{
				return Math.Max(Math.Abs(C.X - this.X), Math.Abs(C.Y - this.Y));
			}
			if (C.ParentZone.ZoneWorld != this.ParentZone.ZoneWorld)
			{
				return 9999999;
			}
			int num = C.ParentZone.GetZonewX() * Definitions.Width * 80 + C.ParentZone.GetZoneX() * 80 + C.X;
			int num2 = C.ParentZone.GetZonewY() * Definitions.Height * 25 + C.ParentZone.GetZoneY() * 25 + C.Y;
			int zoneZ = C.ParentZone.GetZoneZ();
			int num3 = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
			int num4 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
			int zoneZ2 = this.ParentZone.GetZoneZ();
			if (zoneZ != zoneZ2)
			{
				if (num != num3 || num2 != num4)
				{
					return int.MaxValue;
				}
				if (Math.Abs(zoneZ2 - zoneZ) == 1)
				{
					if (zoneZ < zoneZ2)
					{
						if (!this.HasObjectWithPart("StairsUp"))
						{
							return int.MaxValue;
						}
					}
					else if (!this.HasObjectWithPart("StairsDown"))
					{
						return int.MaxValue;
					}
				}
			}
			if (num == num3 && num2 == num4)
			{
				return Math.Abs(zoneZ2 - zoneZ);
			}
			return Math.Max(Math.Max(Math.Abs(num3 - num), Math.Abs(num4 - num2)), Math.Abs(zoneZ2 - zoneZ));
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x001F573C File Offset: 0x001F393C
		public List<GameObject> GetObjectsWithRegisteredEvent(string EventName)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x001F5790 File Offset: 0x001F3990
		public List<GameObject> GetObjectsWithRegisteredEvent(string EventName, Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName) && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x001F57F8 File Offset: 0x001F39F8
		public GameObject GetFirstObjectWithRegisteredEvent(string EventName)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x001F5840 File Offset: 0x001F3A40
		public GameObject GetFirstObjectWithRegisteredEvent(string EventName, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName) && Filter(this.Objects[i]))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x001F589C File Offset: 0x001F3A9C
		public bool HasObjectWithRegisteredEvent(string EventName)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x001F58D8 File Offset: 0x001F3AD8
		public bool HasObjectWithRegisteredEvent(string EventName, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName) && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x001F5928 File Offset: 0x001F3B28
		public void ForeachObjectWithRegisteredEvent(string EventName, Action<GameObject> aProc)
		{
			int i = 0;
			int num = this.Objects.Count;
			while (i < num)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName))
				{
					aProc(this.Objects[i]);
					if (this.Objects.Count < num)
					{
						int num2 = num - this.Objects.Count;
						i -= num2;
						num -= num2;
					}
				}
				i++;
			}
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x001F5998 File Offset: 0x001F3B98
		public void GetObjectsWithTagOrProperty(List<GameObject> List, string Name)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.HasTagOrProperty(Name))
				{
					List.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x001F59DC File Offset: 0x001F3BDC
		public List<GameObject> GetObjectsWithTagOrProperty(string Name)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetObjectsWithTagOrProperty(list, Name);
			return list;
		}

		// Token: 0x0600655D RID: 25949 RVA: 0x001F59F8 File Offset: 0x001F3BF8
		public bool ForeachObjectWithRegisteredEvent(string EventName, Predicate<GameObject> pProc)
		{
			int i = 0;
			int num = this.Objects.Count;
			while (i < num)
			{
				if (this.Objects[i].HasRegisteredEvent(EventName))
				{
					if (!pProc(this.Objects[i]))
					{
						return false;
					}
					if (this.Objects.Count < num)
					{
						int num2 = num - this.Objects.Count;
						i -= num2;
						num -= num2;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600655E RID: 25950 RVA: 0x001F5A6C File Offset: 0x001F3C6C
		public List<GameObject> GetObjectsWithProperty(string PropertyName)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasProperty(PropertyName))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x001F5AC0 File Offset: 0x001F3CC0
		public void GetObjectsWithProperty(string PropertyName, List<GameObject> Return)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasProperty(PropertyName))
				{
					Return.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x001F5B0C File Offset: 0x001F3D0C
		public int GetObjectCountWithProperty(string PropertyName)
		{
			int num = 0;
			for (int i = 0; i < this.Objects.Count; i++)
			{
				if (this.Objects[i].HasProperty(PropertyName))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x001F5B4C File Offset: 0x001F3D4C
		public List<GameObject> GetObjectsWithIntProperty(string PropertyName)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetIntProperty(PropertyName, 0) > 0)
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x001F5BA0 File Offset: 0x001F3DA0
		public List<GameObject> GetObjectsWithIntProperty(string PropertyName, Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetIntProperty(PropertyName, 0) > 0 && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x001F5C08 File Offset: 0x001F3E08
		public int GetObjectCountWithIntProperty(string PropertyName)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetIntProperty(PropertyName, 0) > 0)
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x06006564 RID: 25956 RVA: 0x001F5C4C File Offset: 0x001F3E4C
		public bool HasObjectWithIntProperty(string PropertyName)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetIntProperty(PropertyName, 0) > 0)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006565 RID: 25957 RVA: 0x001F5C8C File Offset: 0x001F3E8C
		public bool HasObjectWithIntProperty(string PropertyName, Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetIntProperty(PropertyName, 0) > 0 && Filter(this.Objects[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006566 RID: 25958 RVA: 0x001F5CE0 File Offset: 0x001F3EE0
		public GameObject GetObjectWithTagOrProperty(string TagName)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTagOrProperty(TagName))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x001F5D28 File Offset: 0x001F3F28
		public GameObject GetObjectWithTag(string TagName)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(TagName))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006568 RID: 25960 RVA: 0x001F5D70 File Offset: 0x001F3F70
		public List<GameObject> GetObjectsWithTag(string TagName)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(TagName))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006569 RID: 25961 RVA: 0x001F5DC4 File Offset: 0x001F3FC4
		public List<GameObject> GetObjectsWithTag(string TagName, Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(TagName) && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x001F5E2C File Offset: 0x001F402C
		public void ForeachObjectWithTagOrProperty(string Name, Action<GameObject> aProc)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Name) || this.Objects[i].HasProperty(Name))
				{
					aProc(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x001F5E8C File Offset: 0x001F408C
		public void ForeachObjectWithTag(string TagName, Action<GameObject> aProc)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(TagName))
				{
					aProc(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x001F5ED8 File Offset: 0x001F40D8
		public void ForeachObjectWithTag(string TagName, Predicate<GameObject> aProc)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(TagName) && !aProc(this.Objects[i]))
				{
					return;
				}
				i++;
			}
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x001F5F28 File Offset: 0x001F4128
		public int GetObjectCountWithTag(string TagName)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(TagName))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600656E RID: 25966 RVA: 0x001F5F68 File Offset: 0x001F4168
		public void ForeachObjectWithPart(string PartName, Action<GameObject> aProc)
		{
			int i = 0;
			int num = this.Objects.Count;
			while (i < num)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.HasPart(PartName))
				{
					aProc(gameObject);
					if (this.Objects.Count < num)
					{
						int num2 = num - this.Objects.Count;
						i -= num2;
						num -= num2;
					}
				}
				i++;
			}
		}

		// Token: 0x0600656F RID: 25967 RVA: 0x001F5FD0 File Offset: 0x001F41D0
		public bool ForeachObjectWithPart(string PartName, Predicate<GameObject> pProc)
		{
			int i = 0;
			int num = this.Objects.Count;
			while (i < num)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.HasPart(PartName))
				{
					if (!pProc(gameObject))
					{
						return false;
					}
					if (this.Objects.Count < num)
					{
						int num2 = num - this.Objects.Count;
						i -= num2;
						num -= num2;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x06006570 RID: 25968 RVA: 0x001F603C File Offset: 0x001F423C
		public void ForeachObject(Action<GameObject> aProc)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				aProc(this.Objects[0]);
				return;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				aProc(gameObject);
				aProc(gameObject2);
				return;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				aProc(gameObject3);
				aProc(gameObject4);
				aProc(gameObject5);
				return;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					aProc(list[i]);
					i++;
				}
				break;
			}
			}
		}

		// Token: 0x06006571 RID: 25969 RVA: 0x001F6120 File Offset: 0x001F4320
		public bool ForeachObject(Predicate<GameObject> pProc)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				if (!pProc(this.Objects[0]))
				{
					return false;
				}
				break;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				if (!pProc(gameObject))
				{
					return false;
				}
				if (!pProc(gameObject2))
				{
					return false;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				if (!pProc(gameObject3))
				{
					return false;
				}
				if (!pProc(gameObject4))
				{
					return false;
				}
				if (!pProc(gameObject5))
				{
					return false;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (!pProc(list[i]))
					{
						return false;
					}
					i++;
				}
				break;
			}
			}
			return true;
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x001F6228 File Offset: 0x001F4428
		public void ForeachObject(Action<GameObject> aProc, Predicate<GameObject> Filter)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				if (Filter(this.Objects[0]))
				{
					aProc(this.Objects[0]);
					return;
				}
				break;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				if (Filter(gameObject))
				{
					aProc(gameObject);
				}
				if (Filter(gameObject2))
				{
					aProc(gameObject2);
					return;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				if (Filter(gameObject3))
				{
					aProc(gameObject3);
				}
				if (Filter(gameObject4))
				{
					aProc(gameObject4);
				}
				if (Filter(gameObject5))
				{
					aProc(gameObject5);
					return;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (Filter(list[i]))
					{
						aProc(list[i]);
					}
					i++;
				}
				break;
			}
			}
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x001F636C File Offset: 0x001F456C
		public bool ForeachObject(Predicate<GameObject> pProc, Predicate<GameObject> Filter)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				if (Filter(this.Objects[0]))
				{
					pProc(this.Objects[0]);
					return false;
				}
				break;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				if (Filter(gameObject) && !pProc(gameObject))
				{
					return false;
				}
				if (Filter(gameObject2))
				{
					pProc(gameObject2);
					return false;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				if (Filter(gameObject3) && !pProc(gameObject3))
				{
					return false;
				}
				if (Filter(gameObject4) && !pProc(gameObject4))
				{
					return false;
				}
				if (Filter(gameObject5))
				{
					pProc(gameObject5);
					return false;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (Filter(list[i]) && !pProc(list[i]))
					{
						return false;
					}
					i++;
				}
				break;
			}
			}
			return false;
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x001F64C4 File Offset: 0x001F46C4
		public void SafeForeachObject(Action<GameObject> aProc)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				aProc(this.Objects[0]);
				return;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				aProc(gameObject);
				if (gameObject2.IsValid())
				{
					aProc(gameObject2);
					return;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				aProc(gameObject3);
				if (gameObject4.IsValid())
				{
					aProc(gameObject4);
				}
				if (gameObject5.IsValid())
				{
					aProc(gameObject5);
					return;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				aProc(list[0]);
				int i = 1;
				int count = list.Count;
				while (i < count)
				{
					if (list[i].IsValid())
					{
						aProc(list[i]);
					}
					i++;
				}
				break;
			}
			}
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x001F65E8 File Offset: 0x001F47E8
		public bool SafeForeachObject(Predicate<GameObject> pProc)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				if (!pProc(this.Objects[0]))
				{
					return false;
				}
				break;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				if (!pProc(gameObject))
				{
					return false;
				}
				if (gameObject2.IsValid() && !pProc(gameObject2))
				{
					return false;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				if (!pProc(gameObject3))
				{
					return false;
				}
				if (gameObject4.IsValid() && !pProc(gameObject4))
				{
					return false;
				}
				if (gameObject5.IsValid() && !pProc(gameObject5))
				{
					return false;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				if (!pProc(list[0]))
				{
					return false;
				}
				int i = 1;
				int count = list.Count;
				while (i < count)
				{
					if (list[i].IsValid() && !pProc(list[i]))
					{
						return false;
					}
					i++;
				}
				break;
			}
			}
			return true;
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x001F6730 File Offset: 0x001F4930
		public void SafeForeachObject(Action<GameObject> aProc, Predicate<GameObject> Filter)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				if (Filter(this.Objects[0]))
				{
					aProc(this.Objects[0]);
					return;
				}
				break;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				if (Filter(gameObject))
				{
					aProc(gameObject);
				}
				if (gameObject2.IsValid() && Filter(gameObject2))
				{
					aProc(gameObject2);
					return;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				if (Filter(gameObject3))
				{
					aProc(gameObject3);
				}
				if (gameObject4.IsValid() && Filter(gameObject4))
				{
					aProc(gameObject4);
				}
				if (gameObject5.IsValid() && Filter(gameObject5))
				{
					aProc(gameObject5);
					return;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				aProc(list[0]);
				int i = 1;
				int count = list.Count;
				while (i < count)
				{
					if (list[i].IsValid() && Filter(list[i]))
					{
						aProc(list[i]);
					}
					i++;
				}
				break;
			}
			}
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x001F68AC File Offset: 0x001F4AAC
		public bool SafeForeachObject(Predicate<GameObject> pProc, Predicate<GameObject> Filter)
		{
			switch (this.Objects.Count)
			{
			case 0:
				break;
			case 1:
				if (Filter(this.Objects[0]) && !pProc(this.Objects[0]))
				{
					return false;
				}
				break;
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				if (Filter(gameObject) && !pProc(gameObject))
				{
					return false;
				}
				if (gameObject2.IsValid() && Filter(gameObject2) && !pProc(gameObject2))
				{
					return false;
				}
				break;
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				if (Filter(gameObject3) && !pProc(gameObject3))
				{
					return false;
				}
				if (gameObject4.IsValid() && Filter(gameObject4) && !pProc(gameObject4))
				{
					return false;
				}
				if (gameObject5.IsValid() && Filter(gameObject5) && !pProc(gameObject5))
				{
					return false;
				}
				break;
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				if (Filter(list[0]) && !pProc(list[0]))
				{
					return false;
				}
				int i = 1;
				int count = list.Count;
				while (i < count)
				{
					if (list[i].IsValid() && Filter(list[i]) && !pProc(list[i]))
					{
						return false;
					}
					i++;
				}
				break;
			}
			}
			return true;
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x001F6A62 File Offset: 0x001F4C62
		public List<GameObject> GetObjectsViaEventList()
		{
			return Event.NewGameObjectList(this.Objects);
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x001F6A6F File Offset: 0x001F4C6F
		public List<GameObject> GetObjects()
		{
			return new List<GameObject>(this.Objects);
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x001F6A7C File Offset: 0x001F4C7C
		public void GetObjects(List<GameObject> Store)
		{
			Store.AddRange(this.Objects);
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x001F6A8C File Offset: 0x001F4C8C
		public List<GameObject> GetObjects(string Blueprint)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					num++;
				}
				i++;
			}
			List<GameObject> list = new List<GameObject>(num);
			if (num > 0)
			{
				int j = 0;
				int count2 = this.Objects.Count;
				while (j < count2)
				{
					if (this.Objects[j].Blueprint == Blueprint)
					{
						list.Add(this.Objects[j]);
						if (list.Count >= num)
						{
							break;
						}
					}
					j++;
				}
			}
			return list;
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x001F6B30 File Offset: 0x001F4D30
		public void GetObjects(string Blueprint, List<GameObject> List)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].Blueprint == Blueprint)
				{
					List.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x001F6B80 File Offset: 0x001F4D80
		public List<GameObject> GetObjectsViaEventList(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjects();
			}
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					num++;
				}
				i++;
			}
			List<GameObject> list = Event.NewGameObjectList();
			if (num > 0)
			{
				int j = 0;
				int count2 = this.Objects.Count;
				while (j < count2)
				{
					if (Filter(this.Objects[j]))
					{
						list.Add(this.Objects[j]);
						if (list.Count >= num)
						{
							break;
						}
					}
					j++;
				}
			}
			return list;
		}

		// Token: 0x0600657E RID: 25982 RVA: 0x001F6C24 File Offset: 0x001F4E24
		public List<GameObject> GetObjects(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjects();
			}
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					num++;
				}
				i++;
			}
			List<GameObject> list = new List<GameObject>(num);
			if (num > 0)
			{
				int j = 0;
				int count2 = this.Objects.Count;
				while (j < count2)
				{
					if (Filter(this.Objects[j]))
					{
						list.Add(this.Objects[j]);
						if (list.Count >= num)
						{
							break;
						}
					}
					j++;
				}
			}
			return list;
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x001F6CC8 File Offset: 0x001F4EC8
		public void GetObjects(Predicate<GameObject> Filter, List<GameObject> Store)
		{
			if (Filter == null)
			{
				this.GetObjects(Store);
				return;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (Filter(this.Objects[i]))
				{
					Store.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x001F6D20 File Offset: 0x001F4F20
		public List<GameObject> GetObjectsThatInheritFrom(string Blueprint)
		{
			List<GameObject> list = new List<GameObject>();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetBlueprint(true).InheritsFrom(Blueprint))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x001F6D78 File Offset: 0x001F4F78
		public GameObject GetFirstObjectThatInheritsFrom(string Blueprint)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetBlueprint(true).InheritsFrom(Blueprint))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x001F6DC8 File Offset: 0x001F4FC8
		public List<GameObject> GetObjectsWithPartReadonly(string PartName)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(PartName))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x001F6E1C File Offset: 0x001F501C
		public List<GameObject> GetObjectsWithPart(string PartName)
		{
			List<GameObject> list = new List<GameObject>(this.GetObjectCountWithPart(PartName));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(PartName))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006584 RID: 25988 RVA: 0x001F6E78 File Offset: 0x001F5078
		public void GetObjectsWithPart(string PartName, List<GameObject> Return)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(PartName))
				{
					Return.Add(this.Objects[i]);
				}
				i++;
			}
		}

		// Token: 0x06006585 RID: 25989 RVA: 0x001F6EC3 File Offset: 0x001F50C3
		public IEnumerable<GameObject> LoopObjects()
		{
			int i = 0;
			int j = this.Objects.Count;
			while (i < j)
			{
				yield return this.Objects[i];
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06006586 RID: 25990 RVA: 0x001F6ED3 File Offset: 0x001F50D3
		public IEnumerable<GameObject> LoopObjectsWithPart(string PartName)
		{
			int i = 0;
			int j = this.Objects.Count;
			while (i < j)
			{
				if (this.Objects[i].HasPart(PartName))
				{
					yield return this.Objects[i];
				}
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x001F6EEC File Offset: 0x001F50EC
		public List<GameObject> GetObjectsWithPart(List<string> PartNames)
		{
			List<GameObject> list = new List<GameObject>(this.GetObjectCountWithPart(PartNames));
			int count = PartNames.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(PartNames[j]))
					{
						list.Add(this.Objects[i]);
						break;
					}
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006588 RID: 25992 RVA: 0x001F6F65 File Offset: 0x001F5165
		public IEnumerable<GameObject> LoopObjectsWithPart(List<string> PartNames)
		{
			int i = PartNames.Count;
			int j = 0;
			int k = this.Objects.Count;
			while (j < k)
			{
				for (int l = 0; l < i; l++)
				{
					if (this.Objects[j].HasPart(PartNames[l]))
					{
						yield return this.Objects[j];
						break;
					}
				}
				int num = j;
				j = num + 1;
			}
			yield break;
		}

		// Token: 0x06006589 RID: 25993 RVA: 0x001F6F7C File Offset: 0x001F517C
		public void GetObjectsWithPart(List<string> PartNames, List<GameObject> Return)
		{
			int count = PartNames.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(PartNames[j]))
					{
						Return.Add(this.Objects[i]);
						break;
					}
				}
				i++;
			}
		}

		// Token: 0x0600658A RID: 25994 RVA: 0x001F6FE4 File Offset: 0x001F51E4
		public List<GameObject> GetObjectsWithPart(string PartName, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetObjectsWithPart(PartName);
			}
			List<GameObject> list = new List<GameObject>(this.GetObjectCountWithPart(PartName, Filter));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(PartName) && Filter(this.Objects[i]))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600658B RID: 25995 RVA: 0x001F705D File Offset: 0x001F525D
		public IEnumerable<GameObject> LoopObjectsWithPart(string PartName, Predicate<GameObject> Filter)
		{
			int i = 0;
			int j = this.Objects.Count;
			while (i < j)
			{
				if (this.Objects[i].HasPart(PartName) && (Filter == null || Filter(this.Objects[i])))
				{
					yield return this.Objects[i];
				}
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x001F707C File Offset: 0x001F527C
		public List<GameObject> GetObjectsWithPart(string PartName, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			List<GameObject> list = new List<GameObject>(this.GetObjectCountWithPart(PartName, Phase, FlightPOV, AttackPOV, SolidityPOV, Projectile, Skip, SkipList, CheckFlight, CheckAttackable, CheckSolidity));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600658D RID: 25997 RVA: 0x001F7138 File Offset: 0x001F5338
		public List<GameObject> GetObjectsWithPart(string PartName, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = new List<GameObject>(this.GetObjectCountWithPart(PartName, Phase, FlightPOV, AttackPOV, SolidityPOV, Projectile, Skip, SkipList, CheckFlight, CheckAttackable, CheckSolidity, Filter));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600658E RID: 25998 RVA: 0x001F7208 File Offset: 0x001F5408
		public List<GameObject> GetRealObjects(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			List<GameObject> list = new List<GameObject>(this.GetRealObjectCount(Phase, FlightPOV, AttackPOV, SolidityPOV, Projectile, Skip, SkipList, CheckFlight, CheckAttackable, CheckSolidity));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x001F72D0 File Offset: 0x001F54D0
		public List<GameObject> GetRealObjects(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = new List<GameObject>(this.GetRealObjectCount(Phase, FlightPOV, AttackPOV, SolidityPOV, Projectile, Skip, SkipList, CheckFlight, CheckAttackable, CheckSolidity, Filter));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x001F73A8 File Offset: 0x001F55A8
		public List<GameObject> GetRealNonSceneryObjects()
		{
			List<GameObject> list = new List<GameObject>(this.GetRealNonSceneryObjectCount());
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery)
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x001F740C File Offset: 0x001F560C
		public List<GameObject> GetRealNonSceneryObjects(Predicate<GameObject> Filter)
		{
			List<GameObject> list = new List<GameObject>(this.GetRealNonSceneryObjectCount(Filter));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && (Filter == null || Filter(gameObject)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x001F747C File Offset: 0x001F567C
		public List<GameObject> GetRealNonSceneryObjects(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			List<GameObject> list = new List<GameObject>(this.GetRealNonSceneryObjectCount(Phase, FlightPOV, AttackPOV, SolidityPOV, Projectile, Skip, SkipList, CheckFlight, CheckAttackable, CheckSolidity));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x001F754C File Offset: 0x001F574C
		public List<GameObject> GetRealNonSceneryObjects(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = new List<GameObject>(this.GetRealNonSceneryObjectCount(Phase, FlightPOV, AttackPOV, SolidityPOV, Projectile, Skip, SkipList, CheckFlight, CheckAttackable, CheckSolidity, Filter));
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					list.Add(gameObject);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x001F762C File Offset: 0x001F582C
		public void GetObjectsWithPart(string PartName, List<GameObject> Return, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					Return.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x001F76C8 File Offset: 0x001F58C8
		public void GetObjectsWithPart(string PartName, IList<GameObject> Return, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					Return.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x001F7778 File Offset: 0x001F5978
		public void GetRealObjects(List<GameObject> Return, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)))
				{
					XRL.World.Parts.Physics physics = gameObject.Physics;
					if (physics != null && physics.IsReal && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
					{
						Return.Add(gameObject);
					}
				}
				i++;
			}
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x001F7830 File Offset: 0x001F5A30
		public void GetRealNonSceneryObjects(List<GameObject> Return, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					Return.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x001F78E4 File Offset: 0x001F5AE4
		public void GetRealNonSceneryObjects(IList<GameObject> Return, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					Return.Add(gameObject);
				}
				i++;
			}
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x001F79A4 File Offset: 0x001F5BA4
		public int GetObjectCountWithPart(string PartName)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(PartName))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x001F79E4 File Offset: 0x001F5BE4
		public int GetObjectCountWithPart(List<string> PartNames)
		{
			int num = 0;
			int count = PartNames.Count;
			int i = 0;
			int count2 = this.Objects.Count;
			while (i < count2)
			{
				for (int j = 0; j < count; j++)
				{
					if (this.Objects[i].HasPart(PartNames[j]))
					{
						num++;
						break;
					}
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x001F7A44 File Offset: 0x001F5C44
		public int GetObjectCountWithPart(string PartName, Predicate<GameObject> Filter)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(PartName) && Filter(this.Objects[i]))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x001F7A98 File Offset: 0x001F5C98
		public int GetObjectCountWithPart(string PartName, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x001F7B34 File Offset: 0x001F5D34
		public int GetObjectCountWithPart(string PartName, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x001F7BE4 File Offset: 0x001F5DE4
		public int GetObjectCountAndFirstObjectWithPart(out GameObject FirstObject, string PartName, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			FirstObject = null;
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					if (num == 0)
					{
						FirstObject = gameObject;
					}
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x001F7C88 File Offset: 0x001F5E88
		public int GetObjectCountAndFirstObjectWithPart(out GameObject FirstObject, string PartName, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			FirstObject = null;
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.HasPart(PartName) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					if (num == 0)
					{
						FirstObject = gameObject;
					}
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x001F7D40 File Offset: 0x001F5F40
		public int GetRealObjectCount(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x001F7DE8 File Offset: 0x001F5FE8
		public int GetRealObjectCount(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x001F7EA0 File Offset: 0x001F60A0
		public int GetRealObjectCountAndFirstObject(out GameObject FirstObject, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			FirstObject = null;
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					if (num == 0)
					{
						FirstObject = gameObject;
					}
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x001F7F54 File Offset: 0x001F6154
		public int GetRealObjectCountAndFirstObject(out GameObject FirstObject, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			FirstObject = null;
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					if (num == 0)
					{
						FirstObject = gameObject;
					}
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x001F8018 File Offset: 0x001F6218
		public int GetRealNonSceneryObjectCount()
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery)
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x001F8070 File Offset: 0x001F6270
		public int GetRealNonSceneryObjectCount(Predicate<GameObject> Filter)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && (Filter == null || Filter(gameObject)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x001F80D4 File Offset: 0x001F62D4
		public int GetRealNonSceneryObjectCount(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x001F8184 File Offset: 0x001F6384
		public int GetRealNonSceneryObjectCount(int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x001F8244 File Offset: 0x001F6444
		public int GetRealNonSceneryObjectCountAndFirstObject(out GameObject FirstObject, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false)
		{
			FirstObject = null;
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)))
				{
					if (num == 0)
					{
						FirstObject = gameObject;
					}
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x001F8300 File Offset: 0x001F6500
		public int GetRealNonSceneryObjectCountAndFirstObject(out GameObject FirstObject, int Phase = 5, GameObject FlightPOV = null, GameObject AttackPOV = null, GameObject SolidityPOV = null, GameObject Projectile = null, GameObject Skip = null, List<GameObject> SkipList = null, bool CheckFlight = false, bool CheckAttackable = false, bool CheckSolidity = false, Predicate<GameObject> Filter = null)
		{
			FirstObject = null;
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				XRL.World.Parts.Physics physics = gameObject.Physics;
				if (physics != null && physics.IsReal && !gameObject.IsScenery && gameObject != Skip && (SkipList == null || !SkipList.Contains(gameObject)) && gameObject.PhaseMatches(Phase) && (!CheckFlight || gameObject.FlightMatches(FlightPOV)) && (!CheckAttackable || CheckAttackableEvent.Check(gameObject, AttackPOV)) && (!CheckSolidity || gameObject.ConsiderSolidForProjectile(Projectile, AttackPOV, null, null, new bool?(false), new bool?(false), false, false)) && (Filter == null || Filter(gameObject)))
				{
					if (num == 0)
					{
						FirstObject = gameObject;
					}
					num++;
				}
				i++;
			}
			return num;
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x001F83CC File Offset: 0x001F65CC
		public bool IsEmptyExcludingCombat(bool NoBrainOnly = false)
		{
			if (this.Objects.Count == 0)
			{
				return true;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Render != null && gameObject.Render.RenderLayer > 5 && !gameObject.IsCombatObject() && (!gameObject.IsDoor() || gameObject.GetPart<Door>().Locked) && !gameObject.HasPart<StairsDown>() && !gameObject.HasPart<StairsUp>())
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x001F8454 File Offset: 0x001F6654
		public bool IsEmptyAtRenderLayer(int Layer)
		{
			if (this.Objects.Count == 0)
			{
				return true;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Render != null && gameObject.Render.Visible && gameObject.Render.RenderLayer >= Layer)
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065AC RID: 26028 RVA: 0x001F84BC File Offset: 0x001F66BC
		public bool IsOpenForPlacement()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Render != null)
				{
					if (gameObject.Physics.Solid)
					{
						return false;
					}
					if (gameObject.HasPart<StairsDown>())
					{
						return false;
					}
					if (gameObject.HasPart<StairsUp>())
					{
						return false;
					}
					if (gameObject.IsCombat)
					{
						return false;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x001F8524 File Offset: 0x001F6724
		public bool IsEmptyForPopulation()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Physics.Solid)
				{
					return false;
				}
				if (gameObject.IsCombatObject())
				{
					return false;
				}
				if (gameObject.HasTag("Furniture"))
				{
					return false;
				}
				if (gameObject.HasTag("Door"))
				{
					return false;
				}
				if (gameObject.IsWall())
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065AE RID: 26030 RVA: 0x001F8598 File Offset: 0x001F6798
		public bool IsSpawnable()
		{
			if (this.HasSpawnBlocker())
			{
				return false;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Physics.Solid)
				{
					return false;
				}
				if (gameObject.IsCombatObject())
				{
					return false;
				}
				StairsDown part = gameObject.GetPart<StairsDown>();
				if (part != null && part.PullDown)
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065AF RID: 26031 RVA: 0x001F8604 File Offset: 0x001F6804
		public bool IsCorner()
		{
			return this.X != 0 && this.X != 79 && this.Y != 0 && this.Y != 24 && ((this.GetCellFromDirection("N", true).HasWall() && this.GetCellFromDirection("NE", true).HasWall() && this.GetCellFromDirection("E", true).HasWall()) || (this.GetCellFromDirection("E", true).HasWall() && this.GetCellFromDirection("SE", true).HasWall() && this.GetCellFromDirection("S", true).HasWall()) || (this.GetCellFromDirection("S", true).HasWall() && this.GetCellFromDirection("SW", true).HasWall() && this.GetCellFromDirection("W", true).HasWall()) || (this.GetCellFromDirection("W", true).HasWall() && this.GetCellFromDirection("NW", true).HasWall() && this.GetCellFromDirection("N", true).HasWall()));
		}

		// Token: 0x060065B0 RID: 26032 RVA: 0x001F8724 File Offset: 0x001F6924
		public bool IsEmpty()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				Door door;
				if (gameObject.Render != null && gameObject.Render.RenderLayer > 5 && (!gameObject.TryGetPart<Door>(out door) || door.Locked) && !gameObject.HasPart<StairsDown>() && !gameObject.HasPart<StairsUp>())
				{
					return false;
				}
				if (gameObject.IsCombatObject())
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065B1 RID: 26033 RVA: 0x001F879C File Offset: 0x001F699C
		public bool IsEmptyFor(GameObject Object)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				Door door;
				if (gameObject.Render != null && gameObject.Render.RenderLayer > 5 && (!gameObject.TryGetPart<Door>(out door) || door.Locked) && !gameObject.HasPart<StairsDown>() && !gameObject.HasPart<StairsUp>() && gameObject.PhaseMatches(Object))
				{
					return false;
				}
				if (gameObject.IsCombatObject() && gameObject.PhaseAndFlightMatches(Object))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x001F8828 File Offset: 0x001F6A28
		public bool IsEmptyIgnoring(Predicate<GameObject> Filter)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (!Filter(gameObject))
				{
					Door door;
					if (gameObject.Render != null && gameObject.Render.RenderLayer > 5 && (!gameObject.TryGetPart<Door>(out door) || door.Locked) && !gameObject.HasPart<StairsDown>() && !gameObject.HasPart<StairsUp>())
					{
						return false;
					}
					if (gameObject.IsCombatObject())
					{
						return false;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065B3 RID: 26035 RVA: 0x001F88A8 File Offset: 0x001F6AA8
		public bool ClearImpassableObjects(GameObject Object = null, bool IncludeCombatObjects = false)
		{
			List<GameObject> list = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				Door door;
				if (!gameObject.ConsiderSolidFor(Object, false) || (gameObject.TryGetPart<Door>(out door) && !door.Locked))
				{
					goto IL_00FF;
				}
				StairsDown part = gameObject.GetPart<StairsDown>();
				if (part != null && part.PullDown && (Object == null || part.IsValidForPullDown(Object)))
				{
					if (list == null)
					{
						list = Event.NewGameObjectList();
					}
					list.Add(this.Objects[i]);
				}
				else
				{
					if (part != null || gameObject.HasPart<StairsUp>())
					{
						goto IL_00FF;
					}
					if (Object == null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
					else
					{
						Cell.eBeforePhysicsRejectObjectEntringCell.SetParameter("Object", Object);
						bool flag = gameObject.FireEvent(Cell.eBeforePhysicsRejectObjectEntringCell);
						Cell.eBeforePhysicsRejectObjectEntringCell.SetParameter("Object", null);
						if (!flag)
						{
							goto IL_00FF;
						}
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
				}
				IL_0129:
				i++;
				continue;
				IL_00FF:
				if (IncludeCombatObjects && gameObject.IsCombatObject() && gameObject != Object)
				{
					if (list == null)
					{
						list = Event.NewGameObjectList();
					}
					list.Add(this.Objects[i]);
					goto IL_0129;
				}
				goto IL_0129;
			}
			if (list != null)
			{
				list.ForEach(delegate(GameObject o)
				{
					this.Objects.Remove(o);
				});
			}
			return true;
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x001F8A00 File Offset: 0x001F6C00
		public bool IsPassable(GameObject Object = null, bool IncludeCombatObjects = true)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				StairsDown stairsDown;
				if (gameObject.ConsiderSolidFor(Object, false))
				{
					Door door;
					if (!gameObject.TryGetPart<Door>(out door) || door.Locked)
					{
						if (Object == null)
						{
							return false;
						}
						Cell.eBeforePhysicsRejectObjectEntringCell.SetParameter("Object", Object);
						bool flag = gameObject.FireEvent(Cell.eBeforePhysicsRejectObjectEntringCell);
						Cell.eBeforePhysicsRejectObjectEntringCell.SetParameter("Object", null);
						if (flag)
						{
							return false;
						}
					}
				}
				else if (gameObject.TryGetPart<StairsDown>(out stairsDown) && stairsDown.PullDown && (Object == null || stairsDown.IsValidForPullDown(Object)))
				{
					return false;
				}
				if (IncludeCombatObjects && gameObject.IsCombatObject() && gameObject != Object)
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x001F8AC0 File Offset: 0x001F6CC0
		public bool IsEmptyOfSolid(bool IncludeCombatObjects = true)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Render != null && gameObject.Render.RenderLayer > 5 && (!gameObject.IsDoor() || gameObject.GetPart<Door>().Locked) && !gameObject.HasPart<StairsDown>() && !gameObject.HasPart<StairsUp>() && gameObject.ConsiderSolid())
				{
					return false;
				}
				if (IncludeCombatObjects && gameObject.IsCombatObject())
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x001F8B48 File Offset: 0x001F6D48
		public bool IsEmptyOfSolidFor(GameObject Object, bool IncludeCombatObjects = true)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.Render != null && gameObject.Render.RenderLayer > 5 && (!gameObject.IsDoor() || gameObject.GetPart<Door>().Locked) && !gameObject.HasPart<StairsDown>() && !gameObject.HasPart<StairsUp>() && gameObject.ConsiderSolidFor(Object, false) && gameObject.PhaseAndFlightMatches(Object))
				{
					return false;
				}
				if (IncludeCombatObjects && gameObject.IsCombatObject() && gameObject.PhaseAndFlightMatches(Object))
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x060065B7 RID: 26039 RVA: 0x001F8BE1 File Offset: 0x001F6DE1
		public void ClearOccludeCache()
		{
			this.OccludeCache = -1;
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x001F8BEA File Offset: 0x001F6DEA
		public bool IsEdge()
		{
			return this.X == 0 || this.X == this.ParentZone.Width - 1 || this.Y == 0 || this.Y == this.ParentZone.Height - 1;
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x001F8C28 File Offset: 0x001F6E28
		public bool HasExternalWall(Predicate<Cell> test = null)
		{
			if (this.HasWall())
			{
				foreach (string text in Directions.DirectionList)
				{
					Cell cellFromDirection = this.GetCellFromDirection(text, true);
					if (cellFromDirection != null && !cellFromDirection.HasWall() && (test == null || test(cellFromDirection)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x001F8C78 File Offset: 0x001F6E78
		public bool HasWallInDirection(string dir)
		{
			Cell cellFromDirection = this.GetCellFromDirection(dir, true);
			return cellFromDirection == null || cellFromDirection.HasWall();
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x001F8C9C File Offset: 0x001F6E9C
		public bool HasWall()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWall())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x001F8CD8 File Offset: 0x001F6ED8
		public List<GameObject> GetWalls()
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWall())
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x001F8D2C File Offset: 0x001F6F2C
		public GameObject GetFirstWall()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWall())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x001F8D74 File Offset: 0x001F6F74
		public bool IsOccluding()
		{
			if (this.OccludeCache != -1)
			{
				return this.OccludeCache == 1;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (((gameObject != null) ? gameObject.Render : null) != null && this.Objects[i].Render.Occluding)
				{
					this.OccludeCache = 1;
					return true;
				}
				i++;
			}
			this.OccludeCache = 0;
			return false;
		}

		// Token: 0x060065BF RID: 26047 RVA: 0x001F8DF0 File Offset: 0x001F6FF0
		public bool IsOccludingFor(GameObject What)
		{
			if (this.OccludeCache == 0)
			{
				return false;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = this.Objects[i];
				if (((gameObject != null) ? gameObject.Render : null) != null && this.Objects[i].Render.Occluding && this.Objects[i].PhaseAndFlightMatches(What))
				{
					this.OccludeCache = 1;
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C0 RID: 26048 RVA: 0x001F8E70 File Offset: 0x001F7070
		public bool IsOccludingOtherThan(GameObject skip)
		{
			if (this.OccludeCache == 0)
			{
				return false;
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != skip)
				{
					GameObject gameObject = this.Objects[i];
					if (((gameObject != null) ? gameObject.Render : null) != null && this.Objects[i].Render.Occluding)
					{
						this.OccludeCache = 1;
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C1 RID: 26049 RVA: 0x001F8EEC File Offset: 0x001F70EC
		public bool IsSolid()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].ConsiderSolid())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C2 RID: 26050 RVA: 0x001F8F28 File Offset: 0x001F7128
		public bool IsSolid(bool ForFluid)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].ConsiderSolid(ForFluid))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x001F8F64 File Offset: 0x001F7164
		public bool IsSolid(bool ForFluid, int Phase)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].ConsiderSolid(ForFluid, Phase))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C4 RID: 26052 RVA: 0x001F8FA4 File Offset: 0x001F71A4
		public bool IsSolidOtherThan(GameObject Skip)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].ConsiderSolid())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C5 RID: 26053 RVA: 0x001F8FF0 File Offset: 0x001F71F0
		public bool IsSolidFor(GameObject Actor, bool ForFluid = false)
		{
			if (Actor == null)
			{
				return this.IsSolid();
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].ConsiderSolidFor(Actor, ForFluid))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x001F9038 File Offset: 0x001F7238
		public bool IsSolidForOtherThan(GameObject Actor, GameObject Skip, bool ForFluid = false)
		{
			if (Actor == null)
			{
				return this.IsSolidOtherThan(Skip);
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i] != Skip && this.Objects[i].ConsiderSolidFor(Actor, false))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x001F9090 File Offset: 0x001F7290
		public bool IsSolidForProjectile(GameObject Projectile, GameObject Attacker, out bool RecheckHit, out bool RecheckPhase, GameObject Launcher = null, GameObject ApparentTarget = null, bool? PenetrateCreatures = null, bool? PenetrateWalls = null, bool Prospective = false)
		{
			RecheckHit = false;
			RecheckPhase = false;
			if (Projectile == null)
			{
				return this.IsSolidFor(Attacker, true);
			}
			bool flag = PenetrateCreatures.GetValueOrDefault();
			bool flag2 = PenetrateWalls.GetValueOrDefault();
			if (PenetrateCreatures == null || PenetrateWalls == null)
			{
				GetMissileWeaponPerformanceEvent @for = GetMissileWeaponPerformanceEvent.GetFor(Attacker, Launcher, Projectile, null, null, null, null, null, null, null, null, null, null, false);
				flag = PenetrateCreatures ?? @for.PenetrateCreatures;
				flag2 = PenetrateWalls ?? @for.PenetrateWalls;
			}
			TreatAsSolid treatAsSolid = ((Projectile != null) ? Projectile.GetPart<TreatAsSolid>() : null);
			if (treatAsSolid != null)
			{
				GameObject gameObject;
				bool flag4;
				bool flag5;
				bool flag3 = treatAsSolid.Match(this, Attacker, out gameObject, out flag4, out flag5, Launcher, ApparentTarget, Projectile, flag, flag2, Prospective);
				if (flag5)
				{
					RecheckPhase = true;
				}
				if (flag4)
				{
					RecheckHit = true;
				}
				if (flag3)
				{
					return true;
				}
			}
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				bool flag7;
				bool flag8;
				bool flag6 = this.Objects[i].ConsiderSolidForProjectile(Projectile, Attacker, out flag7, out flag8, Launcher, ApparentTarget, new bool?(flag), new bool?(flag2), Prospective, true);
				if (flag8)
				{
					RecheckPhase = true;
				}
				if (flag7)
				{
					RecheckHit = true;
				}
				if (flag6)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x001F91EC File Offset: 0x001F73EC
		public bool IsSolidForProjectile(GameObject Projectile = null, GameObject Attacker = null, GameObject Launcher = null, GameObject ApparentTarget = null, bool? PenetrateCreatures = null, bool? PenetrateWalls = null, bool Prospective = false)
		{
			bool flag;
			bool flag2;
			return this.IsSolidForProjectile(Projectile, Attacker, out flag, out flag2, Launcher, ApparentTarget, PenetrateCreatures, PenetrateWalls, Prospective);
		}

		// Token: 0x060065C9 RID: 26057 RVA: 0x001F9210 File Offset: 0x001F7410
		public void FindSolidObjectForMissile(GameObject Attacker, GameObject Launcher, GameObject Projectile, out GameObject SolidObject, out bool IsSolid, out bool IsCover, out bool RecheckHit, out bool RecheckPhase, bool? PenetrateCreatures = null, bool? PenetrateWalls = null, GameObject ApparentTarget = null)
		{
			RecheckHit = false;
			RecheckPhase = false;
			bool flag = PenetrateCreatures.GetValueOrDefault();
			bool flag2 = PenetrateWalls.GetValueOrDefault();
			if ((PenetrateCreatures == null || PenetrateWalls == null) && Launcher != null)
			{
				int? num = null;
				int? num2 = null;
				string text = null;
				string text2 = null;
				string text3 = null;
				bool? flag3 = null;
				bool? flag4 = flag3;
				flag3 = null;
				bool? flag5 = flag3;
				flag3 = null;
				GetMissileWeaponPerformanceEvent @for = GetMissileWeaponPerformanceEvent.GetFor(Attacker, Launcher, Projectile, num, num2, text, text2, text3, flag4, flag5, flag3, null, null, false);
				flag3 = PenetrateCreatures;
				flag = flag3 ?? @for.PenetrateCreatures;
				flag3 = PenetrateWalls;
				flag2 = flag3 ?? @for.PenetrateWalls;
			}
			IsSolid = false;
			IsCover = false;
			TreatAsSolid treatAsSolid = ((Projectile != null) ? Projectile.GetPart<TreatAsSolid>() : null);
			if (treatAsSolid != null)
			{
				TreatAsSolid treatAsSolid2 = treatAsSolid;
				GameObject gameObject = null;
				GameObject gameObject2 = Projectile ?? Attacker;
				bool flag6 = flag;
				bool flag7 = flag2;
				bool flag9;
				bool flag10;
				bool flag8 = treatAsSolid2.Match(this, Attacker, out SolidObject, out flag9, out flag10, gameObject, ApparentTarget, gameObject2, flag6, flag7, false);
				if (flag10)
				{
					RecheckPhase = true;
				}
				if (flag9)
				{
					RecheckHit = true;
				}
				if (flag8)
				{
					IsSolid = true;
					return;
				}
			}
			SolidObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				GameObject gameObject3 = this.Objects[i];
				GameObject gameObject4 = null;
				bool? flag3 = new bool?(flag);
				bool? flag11 = new bool?(flag2);
				bool flag13;
				bool flag14;
				bool flag12 = gameObject3.ConsiderSolidForProjectile(Projectile, Attacker, out flag13, out flag14, gameObject4, ApparentTarget, flag3, flag11, false, true);
				if (flag14)
				{
					RecheckPhase = true;
				}
				if (flag13)
				{
					RecheckHit = true;
				}
				if (flag12 && (!flag2 || !this.Objects[i].IsWall()))
				{
					IsSolid = true;
					SolidObject = this.Objects[i];
					return;
				}
				i++;
			}
			int j = 0;
			int count2 = this.Objects.Count;
			while (j < count2)
			{
				bool flag15;
				bool flag16;
				if (GetMissileCoverPercentageEvent.GetFor(this.Objects[j], Attacker, Projectile, flag, flag2).in100() && this.Objects[j].PhaseMatches(Projectile ?? Attacker) && (FungalVisionary.VisionLevel > 0 || !this.Objects[j].HasPart<FungalVision>() || Attacker.HasPart<FungalVision>()) && BeforeProjectileHitEvent.Check(Projectile, Attacker, this.Objects[j], out flag15, out flag16, flag, flag2, Launcher, ApparentTarget, null, null, false, false) && ((!flag16 && !flag15) || this.Objects[j].PhaseMatches(Projectile ?? Attacker)))
				{
					IsSolid = true;
					IsCover = true;
					SolidObject = this.Objects[j];
					return;
				}
				j++;
			}
		}

		// Token: 0x060065CA RID: 26058 RVA: 0x001F9498 File Offset: 0x001F7698
		public GameObject FindSolidObjectForMissile(GameObject Attacker, GameObject Launcher = null, GameObject Projectile = null, bool? PenetrateCreatures = null, bool? PenetrateWalls = null, GameObject ApparentTarget = null)
		{
			GameObject gameObject;
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			this.FindSolidObjectForMissile(Attacker, Launcher, Projectile, out gameObject, out flag, out flag2, out flag3, out flag4, PenetrateCreatures, PenetrateWalls, ApparentTarget);
			return gameObject;
		}

		// Token: 0x060065CB RID: 26059 RVA: 0x001F94C0 File Offset: 0x001F76C0
		public bool HasSolidObjectForMissile(GameObject Attacker, GameObject Launcher = null, GameObject Projectile = null, bool PenetrateCreatures = false, bool PenetrateWalls = false)
		{
			GameObject gameObject;
			bool flag;
			bool flag2;
			bool flag3;
			bool flag4;
			this.FindSolidObjectForMissile(Attacker, Launcher, Projectile, out gameObject, out flag, out flag2, out flag3, out flag4, new bool?(PenetrateCreatures), new bool?(PenetrateWalls), null);
			return flag;
		}

		// Token: 0x060065CC RID: 26060 RVA: 0x001F94F0 File Offset: 0x001F76F0
		public bool BroadcastEvent(Event E)
		{
			switch (this.Objects.Count)
			{
			case 0:
				return true;
			case 1:
				return this.Objects[0].BroadcastEvent(E);
			case 2:
			{
				GameObject gameObject = this.Objects[1];
				return this.Objects[0].BroadcastEvent(E) && gameObject.BroadcastEvent(E);
			}
			default:
			{
				List<GameObject> list = Event.NewGameObjectList(this.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (!list[i].BroadcastEvent(E))
					{
						return false;
					}
					i++;
				}
				return true;
			}
			}
		}

		// Token: 0x060065CD RID: 26061 RVA: 0x001F9595 File Offset: 0x001F7795
		public bool BroadcastEvent(Event E, IEvent PE)
		{
			bool flag = this.BroadcastEvent(E);
			if (PE != null)
			{
				PE.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x060065CE RID: 26062 RVA: 0x001F95A8 File Offset: 0x001F77A8
		public bool BroadcastEvent(Event E, Event PE)
		{
			bool flag = this.BroadcastEvent(E);
			if (PE != null)
			{
				PE.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x060065CF RID: 26063 RVA: 0x001F95BC File Offset: 0x001F77BC
		public bool FireEvent(string ID)
		{
			if (ID.IndexOf(',') != -1)
			{
				bool flag = true;
				foreach (string text in ID.CachedCommaExpansion())
				{
					if (!this.FireEvent(text))
					{
						flag = false;
					}
				}
				return flag;
			}
			switch (this.Objects.Count)
			{
			case 0:
				return true;
			case 1:
				return this.Objects[0].FireEvent(ID);
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				return gameObject.FireEvent(ID) && (gameObject2 == null || !gameObject2.IsValid() || gameObject2.FireEvent(ID));
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				return gameObject3.FireEvent(ID) && (gameObject4 == null || !gameObject4.IsValid() || gameObject4.FireEvent(ID)) && (gameObject5 == null || !gameObject5.IsValid() || gameObject5.FireEvent(ID));
			}
			case 4:
			{
				GameObject gameObject6 = this.Objects[0];
				GameObject gameObject7 = this.Objects[1];
				GameObject gameObject8 = this.Objects[2];
				GameObject gameObject9 = this.Objects[3];
				return gameObject6.FireEvent(ID) && (gameObject7 == null || !gameObject7.IsValid() || gameObject7.FireEvent(ID)) && (gameObject8 == null || !gameObject8.IsValid() || gameObject8.FireEvent(ID)) && (gameObject9 == null || !gameObject9.IsValid() || gameObject9.FireEvent(ID));
			}
			default:
			{
				if (!Cell.EventListInUse)
				{
					Cell.EventListInUse = true;
					try
					{
						Cell.EventList.Clear();
						Cell.EventList.AddRange(this.Objects);
						int i = 0;
						int count = Cell.EventList.Count;
						while (i < count)
						{
							if (Cell.EventList[i].IsValid() && !Cell.EventList[i].FireEvent(ID))
							{
								return false;
							}
							i++;
						}
					}
					finally
					{
						Cell.EventList.Clear();
						Cell.EventListInUse = false;
					}
					return true;
				}
				List<GameObject> list = new List<GameObject>(this.Objects);
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					if (list[j].IsValid() && !list[j].FireEvent(ID))
					{
						return false;
					}
					j++;
				}
				return true;
			}
			}
		}

		// Token: 0x060065D0 RID: 26064 RVA: 0x001F9874 File Offset: 0x001F7A74
		public bool FireEvent(Event E)
		{
			switch (this.Objects.Count)
			{
			case 0:
				return true;
			case 1:
				return this.Objects[0].FireEvent(E);
			case 2:
			{
				GameObject gameObject = this.Objects[0];
				GameObject gameObject2 = this.Objects[1];
				return gameObject.FireEvent(E) && (gameObject2 == null || !gameObject2.IsValid() || gameObject2.FireEvent(E));
			}
			case 3:
			{
				GameObject gameObject3 = this.Objects[0];
				GameObject gameObject4 = this.Objects[1];
				GameObject gameObject5 = this.Objects[2];
				return gameObject3.FireEvent(E) && (gameObject4 == null || !gameObject4.IsValid() || gameObject4.FireEvent(E)) && (gameObject5 == null || !gameObject5.IsValid() || gameObject5.FireEvent(E));
			}
			case 4:
			{
				GameObject gameObject6 = this.Objects[0];
				GameObject gameObject7 = this.Objects[1];
				GameObject gameObject8 = this.Objects[2];
				GameObject gameObject9 = this.Objects[3];
				return gameObject6.FireEvent(E) && (gameObject7 == null || !gameObject7.IsValid() || gameObject7.FireEvent(E)) && (gameObject8 == null || !gameObject8.IsValid() || gameObject8.FireEvent(E)) && (gameObject9 == null || !gameObject9.IsValid() || gameObject9.FireEvent(E));
			}
			default:
			{
				if (!Cell.EventListInUse)
				{
					Cell.EventListInUse = true;
					try
					{
						Cell.EventList.Clear();
						Cell.EventList.AddRange(this.Objects);
						int i = 0;
						int count = Cell.EventList.Count;
						while (i < count)
						{
							if (Cell.EventList[i].IsValid() && !Cell.EventList[i].FireEvent(E))
							{
								return false;
							}
							i++;
						}
					}
					finally
					{
						Cell.EventList.Clear();
						Cell.EventListInUse = false;
					}
					return true;
				}
				List<GameObject> list = new List<GameObject>(this.Objects);
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					if (list[j].IsValid() && !list[j].FireEvent(E))
					{
						return false;
					}
					j++;
				}
				return true;
			}
			}
		}

		// Token: 0x060065D1 RID: 26065 RVA: 0x001F9ACC File Offset: 0x001F7CCC
		public bool FireEvent(Event E, IEvent PE)
		{
			bool flag = this.FireEvent(E);
			if (PE != null)
			{
				PE.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x060065D2 RID: 26066 RVA: 0x001F9ADF File Offset: 0x001F7CDF
		public bool FireEvent(Event E, Event PE)
		{
			bool flag = this.FireEvent(E);
			if (PE != null)
			{
				PE.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x001F9AF4 File Offset: 0x001F7CF4
		public bool FireEventDirect(Event E)
		{
			for (int i = 0; i < this.Objects.Count; i++)
			{
				if (!this.Objects[i].FireEvent(E))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060065D4 RID: 26068 RVA: 0x001F9B30 File Offset: 0x001F7D30
		public void QuickGetAdjacentCells(List<Cell> Return, bool bLocalOnly)
		{
			Return.Add(this);
			for (int i = 0; i < Cell.DirectionList.Length; i++)
			{
				string text = Cell.DirectionList[i];
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null)
				{
					Return.Add(localCellFromDirection);
				}
			}
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x001F9B71 File Offset: 0x001F7D71
		public IEnumerable<Cell> YieldAdjacentCells(int Radius, bool LocalOnly = false, bool BuiltOnly = true)
		{
			Radius = Radius * 2 + 1;
			int x = this.X;
			int y = this.Y;
			int x2 = 1;
			int y2 = 0;
			int i = 1;
			int j = 0;
			int p = 1;
			int c = Radius * Radius - 1;
			while (j < c)
			{
				x += x2;
				y += y2;
				int num2;
				if (p >= i)
				{
					p = 0;
					int num = x2;
					x2 = -y2;
					y2 = num;
					if (y2 == 0)
					{
						num2 = i;
						i = num2 + 1;
					}
				}
				Cell cellGlobal = this.ParentZone.GetCellGlobal(x, y, LocalOnly, BuiltOnly);
				if (cellGlobal != null)
				{
					yield return cellGlobal;
				}
				num2 = j;
				j = num2 + 1;
				num2 = p;
				p = num2 + 1;
			}
			yield break;
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x001F9B98 File Offset: 0x001F7D98
		public void GetAdjacentCells(int Radius, List<Cell> Return, bool LocalOnly = true, bool BuiltOnly = true)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			Return.Add(this);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, BuiltOnly);
					if (cellFromDirectionGlobal != null && !Return.CleanContains(cellFromDirectionGlobal) && cellFromDirectionGlobal.PathDistanceTo(this) <= Radius)
					{
						cleanQueue.Enqueue(cellFromDirectionGlobal);
						Return.Add(cellFromDirectionGlobal);
					}
				}
			}
		}

		// Token: 0x060065D7 RID: 26071 RVA: 0x001F9C1C File Offset: 0x001F7E1C
		public List<Cell> GetAdjacentCells(int Radius, bool LocalOnly = true, bool BuiltOnly = true)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2) + 1);
			this.GetAdjacentCells(Radius, list, LocalOnly, BuiltOnly);
			return list;
		}

		// Token: 0x060065D8 RID: 26072 RVA: 0x001F9C44 File Offset: 0x001F7E44
		public void GetRealAdjacentCells(int Radius, List<Cell> Return, bool LocalOnly = true)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			Return.Add(this);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, true);
					if (cellFromDirectionGlobal != null && !Return.CleanContains(cellFromDirectionGlobal) && cellFromDirectionGlobal.RealDistanceTo(this, true) <= (double)Radius)
					{
						cleanQueue.Enqueue(cellFromDirectionGlobal);
						Return.Add(cellFromDirectionGlobal);
					}
				}
			}
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x001F9CC8 File Offset: 0x001F7EC8
		public List<Cell> GetRealAdjacentCells(int Radius, bool LocalOnly = true)
		{
			List<Cell> list = Event.NewCellList();
			this.GetRealAdjacentCells(Radius, list, LocalOnly);
			return list;
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x001F9CE5 File Offset: 0x001F7EE5
		public bool IsAdjacentTo(Cell C, bool BuiltOnly = true)
		{
			return C != null && this.GetCellFromDirectionOfCell(C, BuiltOnly) == C;
		}

		// Token: 0x060065DB RID: 26075 RVA: 0x001F9CF7 File Offset: 0x001F7EF7
		public Cell GetCellOrFirstConnectedSpawnLocation(bool bLocalOnly = true)
		{
			if (this.IsEmpty())
			{
				return this;
			}
			return this.GetConnectedSpawnLocation(bLocalOnly) ?? this;
		}

		// Token: 0x060065DC RID: 26076 RVA: 0x001F9D10 File Offset: 0x001F7F10
		public Cell GetConnectedSpawnLocation(bool bLocalOnly = true)
		{
			List<Cell> list = Event.NewCellList();
			this.GetConnectedSpawnLocations(1, list, bLocalOnly);
			if (list.Count <= 0)
			{
				return this.GetFirstEmptyAdjacentCell();
			}
			return list[0] ?? this;
		}

		// Token: 0x060065DD RID: 26077 RVA: 0x001F9D48 File Offset: 0x001F7F48
		public List<Cell> GetConnectedSpawnLocations(int HowMany)
		{
			List<Cell> list = Event.NewCellList();
			this.GetConnectedSpawnLocations(HowMany, list, true);
			return list;
		}

		// Token: 0x060065DE RID: 26078 RVA: 0x001F9D68 File Offset: 0x001F7F68
		public void GetConnectedSpawnLocations(int HowMany, List<Cell> Return, bool bLocalOnly = true)
		{
			List<Cell> list = new List<Cell>();
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			while (cleanQueue.Count > 0 && Return.Count < HowMany)
			{
				Cell cell = cleanQueue.Dequeue();
				list.Add(cell);
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, bLocalOnly, true);
					if (cellFromDirectionGlobal != null && cellFromDirectionGlobal != this && !list.Contains(cellFromDirectionGlobal))
					{
						if (cellFromDirectionGlobal.IsPassable(null, true) && cellFromDirectionGlobal.IsSpawnable())
						{
							Return.Add(cellFromDirectionGlobal);
						}
						if (cellFromDirectionGlobal.IsPassable(null, false))
						{
							cleanQueue.Enqueue(cellFromDirectionGlobal);
						}
					}
				}
			}
		}

		// Token: 0x060065DF RID: 26079 RVA: 0x001F9E1C File Offset: 0x001F801C
		public void GetPassableConnectedAdjacentCells(int Radius, List<Cell> Return, bool LocalOnly = true, GameObject Object = null, bool IncludeCombatObjects = true)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, true);
					if (cellFromDirectionGlobal != null && cellFromDirectionGlobal != this && !Return.Contains(cellFromDirectionGlobal) && cellFromDirectionGlobal.IsPassable(Object, IncludeCombatObjects) && cellFromDirectionGlobal.PathDistanceTo(this) <= Radius)
					{
						cleanQueue.Enqueue(cellFromDirectionGlobal);
						Return.Add(cellFromDirectionGlobal);
					}
				}
			}
		}

		// Token: 0x060065E0 RID: 26080 RVA: 0x001F9EA8 File Offset: 0x001F80A8
		public Cell GetFirstPassableConnectedAdjacentCell(int Radius = 80, bool LocalOnly = true, Predicate<Cell> Filter = null, GameObject Object = null, bool IncludeCombatObjects = true)
		{
			HashSet<Cell> hashSet = new HashSet<Cell>();
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			List<string> list = new List<string>(Cell.DirectionList);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				list.ShuffleInPlace<string>(null);
				foreach (string text in list)
				{
					Cell cell2 = ((cell != null) ? cell.GetCellFromDirectionGlobal(text, LocalOnly, true) : null);
					if (cell2 != null && cell2 != this && cell2.IsPassable(Object, IncludeCombatObjects) && cell2.PathDistanceTo(this) <= Radius && (Filter == null || Filter(cell2)))
					{
						return cell2;
					}
					if (!hashSet.Contains(cell2))
					{
						hashSet.Add(cell2);
						cleanQueue.Enqueue(cell2);
					}
				}
			}
			return null;
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x001F9F94 File Offset: 0x001F8194
		public List<Cell> GetPassableConnectedAdjacentCells(int Radius, bool LocalOnly = true, GameObject Object = null, bool IncludeCombatObjects = true)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2));
			this.GetPassableConnectedAdjacentCells(Radius, list, LocalOnly, Object, IncludeCombatObjects);
			return list;
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x001F9FBC File Offset: 0x001F81BC
		public void GetEmptyConnectedAdjacentCells(int Radius, List<Cell> Return, bool LocalOnly = true)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, true);
					if (cellFromDirectionGlobal != null && cellFromDirectionGlobal != this && !Return.Contains(cellFromDirectionGlobal) && cellFromDirectionGlobal.IsEmpty() && cellFromDirectionGlobal.PathDistanceTo(this) <= Radius)
					{
						cleanQueue.Enqueue(cellFromDirectionGlobal);
						Return.Add(cellFromDirectionGlobal);
					}
				}
			}
		}

		// Token: 0x060065E3 RID: 26083 RVA: 0x001FA044 File Offset: 0x001F8244
		public Cell GetFirstEmptyConnectedAdjacentCell(int Radius, bool LocalOnly = true, Predicate<Cell> Filter = null)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			List<string> list = new List<string>(Cell.DirectionList);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				list.ShuffleInPlace<string>(null);
				foreach (string text in list)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, true);
					if (cellFromDirectionGlobal != null && cellFromDirectionGlobal != this && cellFromDirectionGlobal.IsEmpty() && cellFromDirectionGlobal.PathDistanceTo(this) <= Radius && (Filter == null || Filter(cellFromDirectionGlobal)))
					{
						return cellFromDirectionGlobal;
					}
				}
			}
			return null;
		}

		// Token: 0x060065E4 RID: 26084 RVA: 0x001FA100 File Offset: 0x001F8300
		public List<Cell> GetEmptyConnectedAdjacentCells(int Radius, bool LocalOnly = true)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2));
			this.GetEmptyConnectedAdjacentCells(Radius, list, LocalOnly);
			return list;
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x001FA124 File Offset: 0x001F8324
		public void GetEmptyConnectedAdjacentCellsIgnoring(int Radius, List<Cell> Return, Predicate<GameObject> Filter, bool LocalOnly = true)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, true);
					if (cellFromDirectionGlobal != null && cellFromDirectionGlobal != this && !Return.CleanContains(cellFromDirectionGlobal) && cellFromDirectionGlobal.IsEmptyIgnoring(Filter) && cellFromDirectionGlobal.PathDistanceTo(this) <= Radius)
					{
						cleanQueue.Enqueue(cellFromDirectionGlobal);
						Return.Add(cellFromDirectionGlobal);
					}
				}
			}
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x001FA1B0 File Offset: 0x001F83B0
		public List<Cell> GetEmptyConnectedAdjacentCellsIgnoring(int Radius, Predicate<GameObject> Filter, bool LocalOnly = true)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2));
			this.GetEmptyConnectedAdjacentCellsIgnoring(Radius, list, Filter, LocalOnly);
			return list;
		}

		// Token: 0x060065E7 RID: 26087 RVA: 0x001FA1D8 File Offset: 0x001F83D8
		public void GetPassableConnectedAdjacentCellsFor(List<Cell> Return, GameObject For, int Radius, Predicate<Cell> Filter = null, bool LocalOnly = true, bool ExcludeWalls = true)
		{
			CleanQueue<Cell> cleanQueue = new CleanQueue<Cell>();
			cleanQueue.Enqueue(this);
			while (cleanQueue.Count > 0)
			{
				Cell cell = cleanQueue.Dequeue();
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirectionGlobal = cell.GetCellFromDirectionGlobal(text, LocalOnly, true);
					if (cellFromDirectionGlobal != null && cellFromDirectionGlobal != this && !Return.Contains(cellFromDirectionGlobal) && cellFromDirectionGlobal.IsPassable(For, true) && cellFromDirectionGlobal.PathDistanceTo(this) <= Radius)
					{
						cleanQueue.Enqueue(cellFromDirectionGlobal);
						if (Filter == null || Filter(cellFromDirectionGlobal))
						{
							Return.Add(cellFromDirectionGlobal);
						}
					}
				}
			}
		}

		// Token: 0x060065E8 RID: 26088 RVA: 0x001FA274 File Offset: 0x001F8474
		public List<Cell> GetPassableConnectedAdjacentCellsFor(GameObject For, int Radius, Predicate<Cell> Filter = null, bool LocalOnly = true, bool ExcludeWalls = true)
		{
			List<Cell> list = new List<Cell>();
			this.GetPassableConnectedAdjacentCellsFor(list, For, Radius, Filter, LocalOnly, ExcludeWalls);
			return list;
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x001FA298 File Offset: 0x001F8498
		public List<Cell> GetCardinalAdjacentCellsWhere(Predicate<Cell> test, bool bLocalOnly = false, bool BuiltOnly = true, bool IncludeThis = false)
		{
			List<Cell> list = new List<Cell>(Cell.DirectionListCardinalOnly.Length);
			foreach (string text in Cell.DirectionListCardinalOnly)
			{
				Cell cell = (bLocalOnly ? this.GetLocalCellFromDirection(text, true) : this.GetCellFromDirection(text, true));
				if (cell != null && test(cell))
				{
					list.Add(cell);
				}
			}
			if (IncludeThis)
			{
				list.Add(this);
			}
			return list;
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x001FA304 File Offset: 0x001F8504
		public List<Cell> GetCardinalAdjacentCells(bool bLocalOnly = false, bool BuiltOnly = true, bool IncludeThis = false)
		{
			List<Cell> list = new List<Cell>(Cell.DirectionListCardinalOnly.Length);
			foreach (string text in Cell.DirectionListCardinalOnly)
			{
				Cell cell = (bLocalOnly ? this.GetLocalCellFromDirection(text, true) : this.GetCellFromDirection(text, true));
				if (cell != null)
				{
					list.Add(cell);
				}
			}
			if (IncludeThis)
			{
				list.Add(this);
			}
			return list;
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x001FA364 File Offset: 0x001F8564
		public void ForeachCardinalAdjacentCell(Action<Cell> aProc)
		{
			foreach (string text in Cell.DirectionListCardinalOnly)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, true);
				if (cellFromDirection != null)
				{
					aProc(cellFromDirection);
				}
			}
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x001FA39C File Offset: 0x001F859C
		public bool ForeachCardinalAdjacentCell(Predicate<Cell> pProc)
		{
			foreach (string text in Cell.DirectionListCardinalOnly)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, true);
				if (cellFromDirection != null && !pProc(cellFromDirection))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x001FA3DC File Offset: 0x001F85DC
		public void ForeachCardinalAdjacentLocalCell(Action<Cell> aProc)
		{
			foreach (string text in Cell.DirectionListCardinalOnly)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null)
				{
					aProc(localCellFromDirection);
				}
			}
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x001FA414 File Offset: 0x001F8614
		public bool ForeachCardinalAdjacentLocalCell(Predicate<Cell> pProc)
		{
			foreach (string text in Cell.DirectionListCardinalOnly)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && !pProc(localCellFromDirection))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x001FA454 File Offset: 0x001F8654
		public bool HasLOSTo(GameObject go)
		{
			if (this.ParentZone == null)
			{
				return false;
			}
			if (go == null)
			{
				return false;
			}
			Cell currentCell = go.GetCurrentCell();
			return currentCell != null && this.ParentZone.CalculateLOS(this.X, this.Y, currentCell.X, currentCell.Y, false, false, null, null, 0, false, true);
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x001FA4A8 File Offset: 0x001F86A8
		public bool HasAdjacentAquaticSupportFor(GameObject obj)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && localCellFromDirection.HasAquaticSupportFor(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x001FA4E8 File Offset: 0x001F86E8
		public bool HasAdjacentNonAquaticSupportFor(GameObject obj)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && !localCellFromDirection.HasAquaticSupportFor(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060065F2 RID: 26098 RVA: 0x001FA528 File Offset: 0x001F8728
		public bool HasAdjacentLocalNonwallCell()
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && !localCellFromDirection.HasWall())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060065F3 RID: 26099 RVA: 0x001FA564 File Offset: 0x001F8764
		public bool HasAdjacentLocalWallCell()
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && localCellFromDirection.HasWall())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060065F4 RID: 26100 RVA: 0x001FA5A0 File Offset: 0x001F87A0
		public List<Cell> GetEmptyAdjacentCells(int MinRadius, int MaxRadius)
		{
			List<Cell> list = new List<Cell>();
			for (int i = this.Y - MaxRadius; i <= this.Y + MaxRadius; i++)
			{
				for (int j = this.X - MaxRadius; j <= this.X + MaxRadius; j++)
				{
					Cell cell = this.ParentZone.GetCell(j, i);
					if (cell != null && this.PathDistanceTo(cell) >= MinRadius && this.PathDistanceTo(cell) <= MaxRadius && cell.IsEmpty())
					{
						list.Add(cell);
					}
				}
			}
			return list;
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x001FA61C File Offset: 0x001F881C
		public List<Cell> GetEmptyAdjacentCells()
		{
			List<Cell> list = new List<Cell>();
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, true);
				if (cellFromDirection != null && cellFromDirection.IsEmpty())
				{
					list.Add(cellFromDirection);
				}
			}
			return list;
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x001FA668 File Offset: 0x001F8868
		public Cell GetFirstEmptyAdjacentCell(int MinRadius, int MaxRadius)
		{
			for (int i = this.Y - MaxRadius; i <= this.Y + MaxRadius; i++)
			{
				for (int j = this.X - MaxRadius; j <= this.X + MaxRadius; j++)
				{
					Cell cell = this.ParentZone.GetCell(j, i);
					if (cell != null && this.PathDistanceTo(cell) >= MinRadius && this.PathDistanceTo(cell) <= MaxRadius && cell.IsEmpty())
					{
						return cell;
					}
				}
			}
			return null;
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x001FA6DC File Offset: 0x001F88DC
		public Cell GetFirstEmptyAdjacentCell()
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, true);
				if (cellFromDirection != null && cellFromDirection.IsEmpty())
				{
					return cellFromDirection;
				}
			}
			return null;
		}

		// Token: 0x060065F8 RID: 26104 RVA: 0x001FA718 File Offset: 0x001F8918
		public List<Cell> GetLocalEmptyAdjacentCells()
		{
			List<Cell> list = new List<Cell>();
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && localCellFromDirection.IsEmpty())
				{
					list.Add(localCellFromDirection);
				}
			}
			return list;
		}

		// Token: 0x060065F9 RID: 26105 RVA: 0x001FA764 File Offset: 0x001F8964
		public List<Cell> GetLocalEmptyOfSolidAdjacentCells()
		{
			List<Cell> list = new List<Cell>();
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && localCellFromDirection.IsEmptyOfSolid(true))
				{
					list.Add(localCellFromDirection);
				}
			}
			return list;
		}

		// Token: 0x060065FA RID: 26106 RVA: 0x001FA7B0 File Offset: 0x001F89B0
		public List<Cell> GetNavigableAdjacentCells(GameObject who, int MaxWeight = 5, bool builtOnly = true)
		{
			List<Cell> list = Event.NewCellList();
			if (this.ParentZone != null)
			{
				this.ParentZone.CalculateNavigationMap(who, false, false, false, false, false, false, false);
				foreach (string text in Cell.DirectionList)
				{
					Cell cellFromDirection = this.GetCellFromDirection(text, builtOnly);
					if (cellFromDirection != null && this.ParentZone.NavigationMap[cellFromDirection.X, cellFromDirection.Y].Weight <= MaxWeight)
					{
						list.Add(cellFromDirection);
					}
				}
			}
			return list;
		}

		// Token: 0x060065FB RID: 26107 RVA: 0x001FA834 File Offset: 0x001F8A34
		public List<Cell> GetLocalNavigableAdjacentCells(GameObject who, int MaxWeight = 5)
		{
			List<Cell> list = Event.NewCellList();
			if (this.ParentZone != null)
			{
				this.ParentZone.CalculateNavigationMap(who, false, false, false, false, false, false, false);
				foreach (string text in Cell.DirectionList)
				{
					Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
					if (localCellFromDirection != null && this.ParentZone.NavigationMap[localCellFromDirection.X, localCellFromDirection.Y].Weight <= MaxWeight)
					{
						list.Add(localCellFromDirection);
					}
				}
			}
			return list;
		}

		// Token: 0x060065FC RID: 26108 RVA: 0x001FA8B8 File Offset: 0x001F8AB8
		public List<Cell> GetAdjacentCells(bool BuiltOnly = true)
		{
			List<Cell> list = Event.NewCellList();
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection != null)
				{
					list.Add(cellFromDirection);
				}
			}
			return list;
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x001FA8FC File Offset: 0x001F8AFC
		public bool AnyAdjacentCell(Predicate<Cell> Filter, bool BuiltOnly = true)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection != null && Filter(cellFromDirection))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060065FE RID: 26110 RVA: 0x001FA93C File Offset: 0x001F8B3C
		public Cell GetFirstAdjacentCell(Predicate<Cell> Filter)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, true);
				if (cellFromDirection != null && Filter(cellFromDirection))
				{
					return cellFromDirection;
				}
			}
			return null;
		}

		/// <summary>Get nearest navigable cell, a random cell is picked among the ones with the same distance.</summary>
		/// <param name="Object">An actor used for recalculating the navigation map.</param>
		/// <param name="MaxWeight">The maximum navigation weight to allow for returned cell.</param>
		/// <param name="Random">A generator used to select a pivot point when multiple cells are the same distance away.</param>
		/// <param name="Recalculate">Recalculate the zone navigation map before processing.</param>
		// Token: 0x060065FF RID: 26111 RVA: 0x001FA97C File Offset: 0x001F8B7C
		public Cell GetNearestNavigableCell(GameObject Object, int MaxWeight = 5, global::System.Random Random = null, bool Recalculate = true)
		{
			if (this.ParentZone == null)
			{
				return null;
			}
			if (Recalculate)
			{
				this.ParentZone.CalculateNavigationMap(Object, false, false, false, false, false, false, false);
			}
			if (this.ParentZone.NavigationMap[this.X, this.Y].Weight <= MaxWeight)
			{
				return this;
			}
			if (Random == null)
			{
				Random = Stat.Rnd;
			}
			Cell cell = null;
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			int num3 = Random.Next(this.ParentZone.Width) + Random.Next(this.ParentZone.Height) * this.ParentZone.Width;
			for (int i = 0; i < this.ParentZone.Height; i++)
			{
				for (int j = 0; j < this.ParentZone.Width; j++)
				{
					if (this.ParentZone.NavigationMap[j, i].Weight <= MaxWeight)
					{
						int num4 = this.DistanceTo(j, i);
						if (num4 <= num)
						{
							if (num4 == num)
							{
								int num5 = Math.Abs(num3 - j - i * this.ParentZone.Width);
								if (num5 >= num2)
								{
									goto IL_0105;
								}
								num2 = num5;
							}
							num = num4;
							cell = this.ParentZone.Map[j][i];
						}
					}
					IL_0105:;
				}
			}
			return cell;
		}

		// Token: 0x06006600 RID: 26112 RVA: 0x001FAABC File Offset: 0x001F8CBC
		public void ForeachAdjacentCell(Action<Cell> aProc, bool BuiltOnly = true)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection != null)
				{
					aProc(cellFromDirection);
				}
			}
		}

		// Token: 0x06006601 RID: 26113 RVA: 0x001FAAF4 File Offset: 0x001F8CF4
		public bool ForeachAdjacentCell(Predicate<Cell> pProc, bool BuiltOnly = true)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection != null && !pProc(cellFromDirection))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006602 RID: 26114 RVA: 0x001FAB34 File Offset: 0x001F8D34
		public bool HasUnexploredAdjacentCell(bool BuiltOnly = true)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection != null && !cellFromDirection.IsExplored())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006603 RID: 26115 RVA: 0x001FAB70 File Offset: 0x001F8D70
		public Cell GetFirstNonOccludingAdjacentCell(bool BuiltOnly = true)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell cellFromDirection = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection != null && !cellFromDirection.IsOccluding())
				{
					return cellFromDirection;
				}
			}
			return null;
		}

		// Token: 0x06006604 RID: 26116 RVA: 0x001FABAC File Offset: 0x001F8DAC
		public List<Cell> GetLocalAdjacentCells(int Radius, bool IncludeSelf = false)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2) + (IncludeSelf ? 1 : 0));
			if (IncludeSelf)
			{
				list.Add(this);
			}
			int num = Math.Max(this.Y - Radius, 0);
			while (num <= this.Y + Radius && num <= this.ParentZone.Height - 1)
			{
				int num2 = Math.Max(this.X - Radius, 0);
				while (num2 <= this.X + Radius && num2 <= this.ParentZone.Width - 1)
				{
					Cell cell = this.ParentZone.GetCell(num2, num);
					if (cell != this)
					{
						list.Add(cell);
					}
					num2++;
				}
				num++;
			}
			return list;
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x001FAC54 File Offset: 0x001F8E54
		public Cell GetRandomLocalAdjacentCell()
		{
			List<Cell> localAdjacentCells = this.GetLocalAdjacentCells(1, false);
			if (localAdjacentCells == null || localAdjacentCells.Count == 0)
			{
				return this;
			}
			return localAdjacentCells.GetRandomElement<Cell>(null);
		}

		// Token: 0x06006606 RID: 26118 RVA: 0x001FAC80 File Offset: 0x001F8E80
		public Cell GetRandomLocalAdjacentCell(Predicate<Cell> Filter)
		{
			List<Cell> localAdjacentCells = this.GetLocalAdjacentCells();
			if (localAdjacentCells == null || localAdjacentCells.Count == 0)
			{
				return this;
			}
			return localAdjacentCells.GetRandomElement<Cell>(Filter, null);
		}

		// Token: 0x06006607 RID: 26119 RVA: 0x001FACA9 File Offset: 0x001F8EA9
		public Cell GetRandomLocalAdjacentCell(int Radius, bool IncludeSelf = false)
		{
			return this.GetLocalAdjacentCellsAtRadius(Radius, IncludeSelf).GetRandomElement<Cell>(null);
		}

		// Token: 0x06006608 RID: 26120 RVA: 0x001FACBC File Offset: 0x001F8EBC
		public Cell GetRandomLocalAdjacentCell(int Radius, Predicate<Cell> Filter, bool IncludeSelf = false)
		{
			return (from C in this.GetLocalAdjacentCellsAtRadius(Radius, IncludeSelf)
				where Filter(C)
				select C).GetRandomElement<Cell>(null);
		}

		// Token: 0x06006609 RID: 26121 RVA: 0x001FACF8 File Offset: 0x001F8EF8
		public List<Cell> GetLocalAdjacentCellsCircular(int Radius, bool includeSelf = false)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2));
			int num = Math.Max(this.Y - Radius, 0);
			while (num <= this.Y + Radius && num <= this.ParentZone.Height - 1)
			{
				int num2 = Math.Max(this.X - Radius, 0);
				while (num2 <= this.X + Radius && num2 <= this.ParentZone.Width - 1)
				{
					Cell cell = this.ParentZone.GetCell(num2, num);
					if (cell != this && cell.Location.Distance(this.Location) <= Radius)
					{
						list.Add(cell);
					}
					num2++;
				}
				num++;
			}
			if (includeSelf)
			{
				list.Add(this);
			}
			return list;
		}

		// Token: 0x0600660A RID: 26122 RVA: 0x001FADAC File Offset: 0x001F8FAC
		public List<Cell> GetLocalAdjacentCellsAtRadius(int Radius, bool includeSelf = true)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2) - (Radius + 1) * (Radius + 1));
			int num = Math.Max(this.Y - Radius, 0);
			while (num <= this.Y + Radius && num <= this.ParentZone.Height - 1)
			{
				int num2 = Math.Max(this.X - Radius, 0);
				while (num2 <= this.X + Radius && num2 <= this.ParentZone.Width - 1)
				{
					Cell cell = this.ParentZone.GetCell(num2, num);
					if ((cell != this || includeSelf) && cell.DistanceTo(this) == Radius)
					{
						list.Add(cell);
					}
					num2++;
				}
				num++;
			}
			return list;
		}

		// Token: 0x0600660B RID: 26123 RVA: 0x001FAE58 File Offset: 0x001F9058
		public List<Cell> GetLocalAdjacentCellsAtRadius(int Radius, Predicate<Cell> Filter)
		{
			List<Cell> list = new List<Cell>((Radius + 2) * (Radius + 2) - (Radius + 1) * (Radius + 1));
			int num = Math.Max(this.Y - Radius, 0);
			while (num <= this.Y + Radius && num <= this.ParentZone.Height - 1)
			{
				int num2 = Math.Max(this.X - Radius, 0);
				while (num2 <= this.X + Radius && num2 <= this.ParentZone.Width - 1)
				{
					Cell cell = this.ParentZone.GetCell(num2, num);
					if (cell.DistanceTo(this) == Radius && (Filter == null || Filter(cell)))
					{
						list.Add(cell);
					}
					num2++;
				}
				num++;
			}
			return list;
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x001FAF06 File Offset: 0x001F9106
		public Cell GetRandomLocalAdjacentCellAtRadius(int Radius)
		{
			return this.GetLocalAdjacentCellsAtRadius(Radius, true).GetRandomElement<Cell>(null);
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x001FAF16 File Offset: 0x001F9116
		public Cell GetRandomLocalAdjacentCellAtRadius(int Radius, Predicate<Cell> Filter)
		{
			return this.GetLocalAdjacentCellsAtRadius(Radius, Filter).GetRandomElement<Cell>(null);
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x001FAF28 File Offset: 0x001F9128
		public List<Cell> GetLocalAdjacentCells()
		{
			if (this._LocalAdjacentCellCache == null)
			{
				this._LocalAdjacentCellCache = new List<Cell>();
				for (int i = 0; i < Cell.DirectionList.Length; i++)
				{
					string text = Cell.DirectionList[i];
					Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
					if (localCellFromDirection != null)
					{
						this._LocalAdjacentCellCache.Add(localCellFromDirection);
					}
				}
			}
			return this._LocalAdjacentCellCache;
		}

		// Token: 0x0600660F RID: 26127 RVA: 0x001FAF80 File Offset: 0x001F9180
		public void ForeachLocalAdjacentCell(Action<Cell> aProc)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null)
				{
					aProc(localCellFromDirection);
				}
			}
		}

		// Token: 0x06006610 RID: 26128 RVA: 0x001FAFB8 File Offset: 0x001F91B8
		public bool ForeachLocalAdjacentCell(Predicate<Cell> pProc)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && !pProc(localCellFromDirection))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006611 RID: 26129 RVA: 0x001FAFF8 File Offset: 0x001F91F8
		public void ForeachLocalAdjacentCellAndSelf(Action<Cell> aProc)
		{
			aProc(this);
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null)
				{
					aProc(localCellFromDirection);
				}
			}
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x001FB038 File Offset: 0x001F9238
		public bool ForeachLocalAdjacentCellAndSelf(Predicate<Cell> pProc)
		{
			if (!pProc(this))
			{
				return false;
			}
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && !pProc(localCellFromDirection))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x001FB080 File Offset: 0x001F9280
		public Cell FindLocalAdjacentCell(Predicate<Cell> Filter)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && Filter(localCellFromDirection))
				{
					return localCellFromDirection;
				}
			}
			return null;
		}

		// Token: 0x06006614 RID: 26132 RVA: 0x001FB0C0 File Offset: 0x001F92C0
		public bool AnyLocalAdjacentCell(Predicate<Cell> Filter)
		{
			foreach (string text in Cell.DirectionList)
			{
				Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
				if (localCellFromDirection != null && Filter(localCellFromDirection))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006615 RID: 26133 RVA: 0x001FB100 File Offset: 0x001F9300
		public List<Cell> GetLocalCardinalAdjacentCells()
		{
			if (this._LocalCardinalAdjacentCellCache == null)
			{
				this._LocalCardinalAdjacentCellCache = new List<Cell>(Cell.DirectionListCardinalOnly.Length);
				for (int i = 0; i < Cell.DirectionListCardinalOnly.Length; i++)
				{
					string text = Cell.DirectionListCardinalOnly[i];
					Cell localCellFromDirection = this.GetLocalCellFromDirection(text, true);
					if (localCellFromDirection != null)
					{
						this._LocalCardinalAdjacentCellCache.Add(localCellFromDirection);
					}
				}
			}
			return this._LocalCardinalAdjacentCellCache;
		}

		// Token: 0x06006616 RID: 26134 RVA: 0x001FB160 File Offset: 0x001F9360
		public Cell GetRandomLocalCardinalAdjacentCell()
		{
			List<Cell> localCardinalAdjacentCells = this.GetLocalCardinalAdjacentCells();
			if (localCardinalAdjacentCells == null || localCardinalAdjacentCells.Count == 0)
			{
				return null;
			}
			return localCardinalAdjacentCells.GetRandomElement<Cell>(null);
		}

		// Token: 0x06006617 RID: 26135 RVA: 0x001FB188 File Offset: 0x001F9388
		public Cell GetRandomLocalCardinalAdjacentCell(Predicate<Cell> Filter)
		{
			if (Filter == null)
			{
				return this.GetRandomLocalCardinalAdjacentCell();
			}
			List<Cell> localCardinalAdjacentCells = this.GetLocalCardinalAdjacentCells();
			if (localCardinalAdjacentCells == null || localCardinalAdjacentCells.Count == 0)
			{
				return null;
			}
			return localCardinalAdjacentCells.GetRandomElement<Cell>(Filter, null);
		}

		// Token: 0x06006618 RID: 26136 RVA: 0x001FB1BC File Offset: 0x001F93BC
		public string GetDirectionFrom(Location2D Target)
		{
			if (Target == null || (Target.X == this.X && Target.Y == this.Y))
			{
				return ".";
			}
			int x = this.X;
			int y = this.Y;
			int x2 = Target.X;
			int y2 = Target.Y;
			if (x == x2)
			{
				if (y == y2)
				{
					return ".";
				}
				if (y < y2)
				{
					return "S";
				}
				return "N";
			}
			else if (x < x2)
			{
				if (y == y2)
				{
					return "E";
				}
				if (y < y2)
				{
					return "SE";
				}
				return "NE";
			}
			else
			{
				if (y == y2)
				{
					return "W";
				}
				if (y < y2)
				{
					return "SW";
				}
				return "NW";
			}
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x001FB268 File Offset: 0x001F9468
		public string GetGeneralDirectionFrom(Location2D Target)
		{
			if (Target == null)
			{
				return ".";
			}
			int x = this.X;
			int y = this.Y;
			int x2 = Target.X;
			int y2 = Target.Y;
			bool flag = x == x2 || Math.Abs(x - x2) < Math.Abs(y - y2) / 2;
			bool flag2 = y == y2 || Math.Abs(y - y2) < Math.Abs(x - x2) / 2;
			if (flag)
			{
				if (flag2)
				{
					return ".";
				}
				if (y < y2)
				{
					return "S";
				}
				return "N";
			}
			else if (x < x2)
			{
				if (flag2)
				{
					return "E";
				}
				if (y < y2)
				{
					return "SE";
				}
				return "NE";
			}
			else
			{
				if (flag2)
				{
					return "W";
				}
				if (y < y2)
				{
					return "SW";
				}
				return "NW";
			}
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x001FB32C File Offset: 0x001F952C
		public string GetDirectionFromCell(Cell Target, bool NullIfSame = false)
		{
			if (Target == null || Target == this)
			{
				if (!NullIfSame)
				{
					return ".";
				}
				return null;
			}
			else
			{
				Zone parentZone = Target.ParentZone;
				int num;
				int num2;
				int num3;
				int num4;
				if (parentZone != this.ParentZone)
				{
					if (parentZone.ZoneWorld != this.ParentZone.ZoneWorld)
					{
						return ".";
					}
					num = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
					num2 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
					num3 = parentZone.GetZonewX() * Definitions.Width * 80 + parentZone.GetZoneX() * 80 + Target.X;
					num4 = parentZone.GetZonewY() * Definitions.Height * 25 + parentZone.GetZoneY() * 25 + Target.Y;
				}
				else
				{
					num = this.X;
					num2 = this.Y;
					num3 = Target.X;
					num4 = Target.Y;
				}
				if (num == num3)
				{
					if (num2 == num4)
					{
						if (!NullIfSame)
						{
							return ".";
						}
						return null;
					}
					else
					{
						if (num2 < num4)
						{
							return "S";
						}
						return "N";
					}
				}
				else if (num < num3)
				{
					if (num2 == num4)
					{
						return "E";
					}
					if (num2 < num4)
					{
						return "SE";
					}
					return "NE";
				}
				else
				{
					if (num2 == num4)
					{
						return "W";
					}
					if (num2 < num4)
					{
						return "SW";
					}
					return "NW";
				}
			}
		}

		// Token: 0x0600661B RID: 26139 RVA: 0x001FB494 File Offset: 0x001F9694
		public string GetGeneralDirectionFromCell(Cell Target)
		{
			if (Target == null)
			{
				return ".";
			}
			Zone parentZone = Target.ParentZone;
			int num;
			int num2;
			int num3;
			int num4;
			if (parentZone != this.ParentZone)
			{
				if (parentZone.ZoneWorld != this.ParentZone.ZoneWorld)
				{
					return ".";
				}
				num = this.ParentZone.GetZonewX() * Definitions.Width * 80 + this.ParentZone.GetZoneX() * 80 + this.X;
				num2 = this.ParentZone.GetZonewY() * Definitions.Height * 25 + this.ParentZone.GetZoneY() * 25 + this.Y;
				num3 = parentZone.GetZonewX() * Definitions.Width * 80 + parentZone.GetZoneX() * 80 + Target.X;
				num4 = parentZone.GetZonewY() * Definitions.Height * 25 + parentZone.GetZoneY() * 25 + Target.Y;
			}
			else
			{
				num = this.X;
				num2 = this.Y;
				num3 = Target.X;
				num4 = Target.Y;
			}
			bool flag = num == num3 || Math.Abs(num - num3) < Math.Abs(num2 - num4) / 2;
			bool flag2 = num2 == num4 || Math.Abs(num2 - num4) < Math.Abs(num - num3) / 2;
			if (flag)
			{
				if (flag2)
				{
					return ".";
				}
				if (num2 < num4)
				{
					return "S";
				}
				return "N";
			}
			else if (num < num3)
			{
				if (flag2)
				{
					return "E";
				}
				if (num2 < num4)
				{
					return "SE";
				}
				return "NE";
			}
			else
			{
				if (flag2)
				{
					return "W";
				}
				if (num2 < num4)
				{
					return "SW";
				}
				return "NW";
			}
		}

		// Token: 0x0600661C RID: 26140 RVA: 0x001FB624 File Offset: 0x001F9824
		public Cell GetCellFromDelta(ref float xp, ref float yp, float xd, float yd, bool global = false)
		{
			if (xp == 0f && yp == 0f)
			{
				return this;
			}
			while ((int)xp == this.X && (int)yp == this.Y)
			{
				xp += xd;
				yp += yd;
			}
			int num = (int)xp;
			int num2 = (int)yp;
			if (num < 0)
			{
				return null;
			}
			if (num > 79)
			{
				return null;
			}
			if (num2 < 0)
			{
				return null;
			}
			if (num2 > 24)
			{
				return null;
			}
			return this.ParentZone.GetCell(num, num2);
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x001FB697 File Offset: 0x001F9897
		public Cell GetCellFromOffset(int xd, int yd)
		{
			return this.ParentZone.GetCell(this.X + xd, this.Y + yd);
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x001FB6B4 File Offset: 0x001F98B4
		public IEnumerable<Cell> GetCellsInACosmeticCircle(int radius)
		{
			int yradius = (int)Math.Max(1.0, (double)radius * 0.66);
			float radius_squared = (float)(radius * radius);
			int num4;
			for (int x = this.X - radius; x <= this.X + radius; x = num4 + 1)
			{
				for (int y = this.Y - yradius; y <= this.Y + yradius; y = num4 + 1)
				{
					float num = (float)Math.Abs(x - this.X);
					float num2 = (float)Math.Abs(y - this.Y) * 1.3333f;
					float num3 = num * num + num2 * num2;
					Debug.Log(string.Concat(new string[]
					{
						"xd: ",
						num.ToString(),
						" yd:",
						num2.ToString(),
						" d=",
						num3.ToString()
					}));
					if (num3 <= radius_squared && this.ParentZone.GetCell(x, y) != null)
					{
						yield return this.ParentZone.GetCell(x, y);
					}
					num4 = y;
				}
				num4 = x;
			}
			yield break;
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x001FB6CB File Offset: 0x001F98CB
		public IEnumerable<Cell> GetCellsInABox(int width, int height)
		{
			int num;
			for (int x = this.X; x <= this.X + width; x = num + 1)
			{
				for (int y = this.Y; y <= this.Y + height; y = num + 1)
				{
					yield return this.ParentZone.GetCell(x, y);
					num = y;
				}
				num = x;
			}
			yield break;
		}

		// Token: 0x06006620 RID: 26144 RVA: 0x001FB6E9 File Offset: 0x001F98E9
		public Cell GetCellFromDirection(string Direction, bool BuiltOnly = true)
		{
			return this.GetCellFromDirectionGlobal(Direction, false, BuiltOnly);
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x001FB6F4 File Offset: 0x001F98F4
		public List<Cell> GetDirectionAndAdjacentCells(string Direction, bool LocalOnly = true, bool BuiltOnly = true)
		{
			List<Cell> list = new List<Cell>((Direction == "." || Direction == "?") ? 10 : 3);
			Cell cellFromDirectionGlobal = this.GetCellFromDirectionGlobal(Direction, LocalOnly, BuiltOnly);
			if (cellFromDirectionGlobal != null)
			{
				list.Add(cellFromDirectionGlobal);
			}
			foreach (string text in Directions.GetAdjacentDirections(Direction))
			{
				Cell cellFromDirectionGlobal2 = this.GetCellFromDirectionGlobal(text, LocalOnly, BuiltOnly);
				if (cellFromDirectionGlobal2 != null)
				{
					list.Add(cellFromDirectionGlobal2);
				}
			}
			return list;
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x001FB770 File Offset: 0x001F9970
		public Dictionary<string, Cell> GetAdjacentDirectionCellMap(string Direction, bool BuiltOnly = true)
		{
			Dictionary<string, Cell> dictionary = new Dictionary<string, Cell>((Direction == "." || Direction == "?") ? 10 : 3);
			Cell cellFromDirection = this.GetCellFromDirection(Direction, BuiltOnly);
			if (cellFromDirection != null)
			{
				dictionary.Add(Direction, cellFromDirection);
			}
			foreach (string text in Directions.GetAdjacentDirections(Direction))
			{
				Cell cellFromDirection2 = this.GetCellFromDirection(text, BuiltOnly);
				if (cellFromDirection2 != null)
				{
					dictionary.Add(text, cellFromDirection2);
				}
			}
			return dictionary;
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x001FB7EA File Offset: 0x001F99EA
		public Cell GetLocalCellFromDirection(string Direction, bool BuiltOnly = true)
		{
			return this.GetCellFromDirectionGlobal(Direction, true, BuiltOnly);
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x001FB7F8 File Offset: 0x001F99F8
		public List<Cell> GetLocalDirectionAndAdjacentCells(string Direction, bool BuiltOnly = true)
		{
			List<Cell> list = new List<Cell>((Direction == "." || Direction == "?") ? 10 : 3);
			Cell localCellFromDirection = this.GetLocalCellFromDirection(Direction, BuiltOnly);
			if (localCellFromDirection != null)
			{
				list.Add(localCellFromDirection);
			}
			foreach (string text in Directions.GetAdjacentDirections(Direction))
			{
				if (this.GetLocalCellFromDirection(text, BuiltOnly) != null)
				{
					list.Add(localCellFromDirection);
				}
			}
			return list;
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x001FB86C File Offset: 0x001F9A6C
		public Cell GetCellFromDirectionOfCell(Cell C, bool BuiltOnly = true)
		{
			string directionFromCell = this.GetDirectionFromCell(C, false);
			if (directionFromCell == "." || directionFromCell == "?")
			{
				return null;
			}
			return this.GetCellFromDirection(directionFromCell, BuiltOnly);
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x001FB8A8 File Offset: 0x001F9AA8
		public Cell GetLocalCellFromDirectionOfCell(Cell C, bool BuiltOnly = true)
		{
			string directionFromCell = this.GetDirectionFromCell(C, false);
			if (directionFromCell == "." || directionFromCell == "?")
			{
				return null;
			}
			return this.GetLocalCellFromDirection(directionFromCell, BuiltOnly);
		}

		/// <summary>
		/// We use this in the inner loop of pathfinding where it's really slammed thousands of times.
		///
		/// Performance matters a lot.
		///
		/// Don't generate garbage!
		/// </summary>
		/// <param name="Direction" />
		/// <param name="validExternalDirection" />
		/// <param name="validDestinationZone" />
		/// <returns />
		// Token: 0x06006627 RID: 26151 RVA: 0x001FB8E4 File Offset: 0x001F9AE4
		public Cell GetCellFromDirectionWithOneValidExternalDirection(string Direction, string validExternalDirection, string validDestinationZone)
		{
			int num = this.X;
			int num2 = this.Y;
			int zoneZ = this.ParentZone.GetZoneZ();
			if (Direction == "." || Direction == "?")
			{
				return this;
			}
			Directions.ApplyDirection(Direction, ref num, ref num2, ref zoneZ, 1);
			Zone zone = null;
			ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
			if (this.ParentZone.IsWorldMap())
			{
				if (num < 0)
				{
					return null;
				}
				if (num2 < 0)
				{
					return null;
				}
				if (num > 79)
				{
					return null;
				}
				if (num2 > 24)
				{
					return null;
				}
			}
			if (num < 0 || num2 < 0 || num >= this.ParentZone.Width || num2 >= this.ParentZone.Height || zoneZ != this.ParentZone.GetZoneZ())
			{
				if (Direction != validExternalDirection)
				{
					return null;
				}
				this.ParentZone.GetZoneWorld();
				int num3;
				int num4;
				int num5;
				int num6;
				try
				{
					num3 = this.ParentZone.GetZonewX();
					num4 = this.ParentZone.GetZonewY();
					num5 = this.ParentZone.GetZoneX();
					num6 = this.ParentZone.GetZoneY();
					this.ParentZone.GetZoneZ();
				}
				catch
				{
					return null;
				}
				if (num < 0 || num >= this.ParentZone.Width)
				{
					if (num < 0 && num5 == 0)
					{
						num3--;
						num5 = Definitions.Width - 1;
						num = this.ParentZone.Width - 1;
					}
					else if (num < 0)
					{
						num5--;
						num = this.ParentZone.Width - 1;
					}
					else if (num >= this.ParentZone.Width && num5 == Definitions.Width - 1)
					{
						num3++;
						num5 = 0;
						num = 0;
					}
					else
					{
						num5++;
						num = 0;
					}
				}
				if (num2 < 0 || num2 >= this.ParentZone.Height)
				{
					if (num2 < 0 && num6 == 0)
					{
						num4--;
						num6 = Definitions.Height - 1;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 < 0)
					{
						num6--;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 >= this.ParentZone.Height && num6 == Definitions.Height - 1)
					{
						num4++;
						num6 = 0;
						num2 = 0;
					}
					else
					{
						num6++;
						num2 = 0;
					}
				}
				if (num3 < 0)
				{
					return null;
				}
				if (num4 < 0)
				{
					return null;
				}
				if (num3 > 79)
				{
					return null;
				}
				if (num4 > 24)
				{
					return null;
				}
				zone = zoneManager.GetZone(validDestinationZone);
			}
			IL_023D:
			if (zone == null)
			{
				return this.ParentZone.GetCell(num, num2);
			}
			return zone.GetCell(num, num2);
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x001FBB5C File Offset: 0x001F9D5C
		public Cell GetCellFromDirectionFiltered(string Direction, List<string> ZoneFilter)
		{
			int num = this.X;
			int num2 = this.Y;
			int zoneZ = this.ParentZone.GetZoneZ();
			if (Direction == "." || Direction == "?")
			{
				return this;
			}
			Directions.ApplyDirection(Direction, ref num, ref num2, ref zoneZ, 1);
			Zone zone = null;
			ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
			if (this.ParentZone.IsWorldMap())
			{
				if (num < 0)
				{
					return null;
				}
				if (num2 < 0)
				{
					return null;
				}
				if (num > 79)
				{
					return null;
				}
				if (num2 > 24)
				{
					return null;
				}
			}
			if (num < 0 || num2 < 0 || num >= this.ParentZone.Width || num2 >= this.ParentZone.Height || zoneZ != this.ParentZone.GetZoneZ())
			{
				string zoneWorld = this.ParentZone.GetZoneWorld();
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				try
				{
					num3 = this.ParentZone.GetZonewX();
					num4 = this.ParentZone.GetZonewY();
					num5 = this.ParentZone.GetZoneX();
					num6 = this.ParentZone.GetZoneY();
					num7 = this.ParentZone.GetZoneZ();
				}
				catch
				{
					return null;
				}
				if (num < 0 || num >= this.ParentZone.Width)
				{
					if (num < 0 && num5 == 0)
					{
						num3--;
						num5 = Definitions.Width - 1;
						num = this.ParentZone.Width - 1;
					}
					else if (num < 0)
					{
						num5--;
						num = this.ParentZone.Width - 1;
					}
					else if (num >= this.ParentZone.Width && num5 == Definitions.Width - 1)
					{
						num3++;
						num5 = 0;
						num = 0;
					}
					else
					{
						num5++;
						num = 0;
					}
				}
				if (num2 < 0 || num2 >= this.ParentZone.Height)
				{
					if (num2 < 0 && num6 == 0)
					{
						num4--;
						num6 = Definitions.Height - 1;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 < 0)
					{
						num6--;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 >= this.ParentZone.Height && num6 == Definitions.Height - 1)
					{
						num4++;
						num6 = 0;
						num2 = 0;
					}
					else
					{
						num6++;
						num2 = 0;
					}
				}
				if (num3 < 0)
				{
					return null;
				}
				if (num4 < 0)
				{
					return null;
				}
				if (num3 > 79)
				{
					return null;
				}
				if (num4 > 24)
				{
					return null;
				}
				num7 = zoneZ;
				string text = ZoneID.Assemble(zoneWorld, num3, num4, num5, num6, num7);
				if (!ZoneFilter.Contains(text))
				{
					return null;
				}
				zone = zoneManager.GetZone(text);
				if (zone == null)
				{
					return null;
				}
			}
			IL_025C:
			if (zone == null)
			{
				return this.ParentZone.GetCell(num, num2);
			}
			return zone.GetCell(num, num2);
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x001FBDF4 File Offset: 0x001F9FF4
		public Cell GetCellFromDirectionGlobalIfBuilt(string Direction)
		{
			int num = this.X;
			int num2 = this.Y;
			int zoneZ = this.ParentZone.GetZoneZ();
			if (Direction == "." || Direction == "?")
			{
				return this;
			}
			Directions.ApplyDirection(Direction, ref num, ref num2, ref zoneZ, 1);
			Zone zone = null;
			ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
			if (this.ParentZone.IsWorldMap())
			{
				if (num < 0)
				{
					return null;
				}
				if (num2 < 0)
				{
					return null;
				}
				if (num > 79)
				{
					return null;
				}
				if (num2 > 24)
				{
					return null;
				}
			}
			if (num < 0 || num2 < 0 || num >= this.ParentZone.Width || num2 >= this.ParentZone.Height || zoneZ != this.ParentZone.GetZoneZ())
			{
				string zoneWorld = this.ParentZone.GetZoneWorld();
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				try
				{
					num3 = this.ParentZone.GetZonewX();
					num4 = this.ParentZone.GetZonewY();
					num5 = this.ParentZone.GetZoneX();
					num6 = this.ParentZone.GetZoneY();
					num7 = this.ParentZone.GetZoneZ();
				}
				catch
				{
					return null;
				}
				if (num < 0 || num >= this.ParentZone.Width)
				{
					if (num < 0 && num5 == 0)
					{
						num3--;
						num5 = Definitions.Width - 1;
						num = this.ParentZone.Width - 1;
					}
					else if (num < 0)
					{
						num5--;
						num = this.ParentZone.Width - 1;
					}
					else if (num >= this.ParentZone.Width && num5 == Definitions.Width - 1)
					{
						num3++;
						num5 = 0;
						num = 0;
					}
					else
					{
						num5++;
						num = 0;
					}
				}
				if (num2 < 0 || num2 >= this.ParentZone.Height)
				{
					if (num2 < 0 && num6 == 0)
					{
						num4--;
						num6 = Definitions.Height - 1;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 < 0)
					{
						num6--;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 >= this.ParentZone.Height && num6 == Definitions.Height - 1)
					{
						num4++;
						num6 = 0;
						num2 = 0;
					}
					else
					{
						num6++;
						num2 = 0;
					}
				}
				if (num3 < 0)
				{
					return null;
				}
				if (num4 < 0)
				{
					return null;
				}
				if (num3 > 79)
				{
					return null;
				}
				if (num4 > 24)
				{
					return null;
				}
				num7 = zoneZ;
				string text = ZoneID.Assemble(zoneWorld, num3, num4, num5, num6, num7);
				if (!XRLCore.Core.Game.ZoneManager.IsZoneBuilt(text))
				{
					return null;
				}
				zone = zoneManager.GetZone(text);
				if (zone == null)
				{
					return null;
				}
			}
			IL_026A:
			if (zone == null)
			{
				return this.ParentZone.GetCell(num, num2);
			}
			return zone.GetCell(num, num2);
		}

		// Token: 0x0600662A RID: 26154 RVA: 0x001FC098 File Offset: 0x001FA298
		public Cell GetCellFromDirectionGlobal(string Direction, bool bLocalOnly = true, bool bLiveZonesOnly = true)
		{
			int num = this.X;
			int num2 = this.Y;
			int zoneZ = this.ParentZone.GetZoneZ();
			if (Direction == "." || Direction == "?")
			{
				return this;
			}
			Directions.ApplyDirection(Direction, ref num, ref num2, ref zoneZ, 1);
			Zone zone = null;
			ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
			if (this.ParentZone.IsWorldMap() || bLocalOnly)
			{
				if (num < 0)
				{
					return null;
				}
				if (num2 < 0)
				{
					return null;
				}
				if (num > 79)
				{
					return null;
				}
				if (num2 > 24)
				{
					return null;
				}
			}
			if (num < 0 || num2 < 0 || num >= this.ParentZone.Width || num2 >= this.ParentZone.Height || zoneZ != this.ParentZone.GetZoneZ())
			{
				string zoneWorld = this.ParentZone.GetZoneWorld();
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				try
				{
					num3 = this.ParentZone.GetZonewX();
					num4 = this.ParentZone.GetZonewY();
					num5 = this.ParentZone.GetZoneX();
					num6 = this.ParentZone.GetZoneY();
					num7 = this.ParentZone.GetZoneZ();
				}
				catch
				{
					return null;
				}
				if (num < 0 || num >= this.ParentZone.Width)
				{
					if (num < 0 && num5 == 0)
					{
						num3--;
						num5 = Definitions.Width - 1;
						num = this.ParentZone.Width - 1;
					}
					else if (num < 0)
					{
						num5--;
						num = this.ParentZone.Width - 1;
					}
					else if (num >= this.ParentZone.Width && num5 == Definitions.Width - 1)
					{
						num3++;
						num5 = 0;
						num = 0;
					}
					else
					{
						num5++;
						num = 0;
					}
				}
				if (num2 < 0 || num2 >= this.ParentZone.Height)
				{
					if (num2 < 0 && num6 == 0)
					{
						num4--;
						num6 = Definitions.Height - 1;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 < 0)
					{
						num6--;
						num2 = this.ParentZone.Height - 1;
					}
					else if (num2 >= this.ParentZone.Height && num6 == Definitions.Height - 1)
					{
						num4++;
						num6 = 0;
						num2 = 0;
					}
					else
					{
						num6++;
						num2 = 0;
					}
				}
				if (num3 < 0)
				{
					return null;
				}
				if (num4 < 0)
				{
					return null;
				}
				if (num3 > 79)
				{
					return null;
				}
				if (num4 > 24)
				{
					return null;
				}
				num7 = zoneZ;
				if (bLiveZonesOnly && (zoneManager == null || !zoneManager.IsZoneLive(zoneWorld, num3, num4, num5, num6, num7)))
				{
					return null;
				}
				zone = zoneManager.GetZone(zoneWorld, num3, num4, num5, num6, num7);
				if (zone == null)
				{
					return null;
				}
			}
			IL_0267:
			if (zone == null)
			{
				return this.ParentZone.GetCell(num, num2);
			}
			return zone.GetCell(num, num2);
		}

		// Token: 0x0600662B RID: 26155 RVA: 0x001FC338 File Offset: 0x001FA538
		public void ClearObjectsWithPart(string Part, bool Important = false, bool Combat = false)
		{
			GameObject gameObject = null;
			List<GameObject> list = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasPart(Part) && this.Objects[i].CanClear(Important, Combat))
				{
					if (gameObject != null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
					else
					{
						gameObject = this.Objects[i];
					}
				}
				i++;
			}
			if (gameObject != null)
			{
				this.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			if (list != null)
			{
				foreach (GameObject gameObject2 in list)
				{
					this.RemoveObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x0600662C RID: 26156 RVA: 0x001FC428 File Offset: 0x001FA628
		public void ClearObjectsWithProperty(string Property)
		{
			GameObject gameObject = null;
			List<GameObject> list = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasStringProperty(Property) && this.Objects[i].CanClear(false, false))
				{
					if (gameObject != null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
					else
					{
						gameObject = this.Objects[i];
					}
				}
				i++;
			}
			if (gameObject != null)
			{
				this.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			if (list != null)
			{
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					this.RemoveObject(list[j], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					j++;
				}
			}
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x001FC4FC File Offset: 0x001FA6FC
		public void ClearObjectsWithIntProperty(string Property)
		{
			List<GameObject> list = null;
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].GetIntProperty(Property, 0) > 0 && this.Objects[i].CanClear(false, false))
				{
					if (gameObject != null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
					else
					{
						gameObject = this.Objects[i];
					}
				}
				i++;
			}
			if (gameObject != null)
			{
				this.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			if (list != null)
			{
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					this.RemoveObject(list[j], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					j++;
				}
			}
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x001FC5D0 File Offset: 0x001FA7D0
		public void ClearObjectsWithTag(string Tag)
		{
			List<GameObject> list = null;
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].HasTag(Tag) && this.Objects[i].CanClear(false, false))
				{
					if (gameObject != null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
					else
					{
						gameObject = this.Objects[i];
					}
				}
				i++;
			}
			if (gameObject != null)
			{
				this.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			if (list != null)
			{
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					this.RemoveObject(list[j], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					j++;
				}
			}
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x001FC6A4 File Offset: 0x001FA8A4
		public void ClearWalls()
		{
			List<GameObject> list = null;
			GameObject gameObject = null;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWall() && this.Objects[i].CanClear(false, false))
				{
					if (gameObject != null)
					{
						if (list == null)
						{
							list = Event.NewGameObjectList();
						}
						list.Add(this.Objects[i]);
					}
					else
					{
						gameObject = this.Objects[i];
					}
				}
				i++;
			}
			if (gameObject != null)
			{
				this.RemoveObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			}
			if (list != null)
			{
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					if (list[j] != null && !list[j].IsImportant())
					{
						this.RemoveObject(list[j], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
					j++;
				}
			}
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x001FC790 File Offset: 0x001FA990
		public Cell ClearTerrain()
		{
			List<GameObject> list = Event.NewGameObjectList(this.Objects);
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWall() && this.Objects[i].CanClear(false, false))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				GameObject gameObject = list[j];
				if (gameObject != null && gameObject.CanClear(false, false))
				{
					this.RemoveObject(list[j], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				}
				j++;
			}
			return this;
		}

		/// <summary>
		/// Clear this cell's <see cref="F:XRL.World.Cell.Objects" />.
		/// </summary>
		/// <param name="Blueprint">A replacement blueprint to place in this cell after clearing.</param>
		/// <param name="Important"><c>true</c> if important objects should be cleared; otherwise, <c>false</c>.</param>
		/// <param name="Combat"><c>true</c> if combat objects should be cleared; otherwise, <c>false</c>.</param>
		/// <param name="alsoExclude">A predicate which will prevent objects from being cleared if it returns true.</param>
		/// <seealso cref="M:XRL.World.GameObject.IsImportant" />
		/// <seealso cref="M:XRL.World.GameObject.IsCombatObject" />
		// Token: 0x06006631 RID: 26161 RVA: 0x001FC844 File Offset: 0x001FAA44
		public Cell Clear(string Blueprint = null, bool Important = false, bool Combat = false, Func<GameObject, bool> alsoExclude = null)
		{
			if (this.Objects.Count == 0)
			{
				return this;
			}
			if (this.Objects.Count == 1)
			{
				GameObject gameObject = this.Objects[0];
				if (gameObject != null && gameObject.CanClear(Important, Combat) && (alsoExclude == null || !alsoExclude(this.Objects[0])))
				{
					this.RemoveObject(this.Objects[0], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				}
				return this;
			}
			List<GameObject> list = Event.NewGameObjectList(this.Objects);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject2 = list[i];
				if (gameObject2 != null && gameObject2.CanClear(Important, Combat) && (alsoExclude == null || !alsoExclude(list[i])))
				{
					this.RemoveObject(list[i], false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				}
				i++;
			}
			if (Blueprint != null)
			{
				this.AddObject(Blueprint, null, null, null, null);
			}
			return this;
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x001FC93C File Offset: 0x001FAB3C
		public void DustPuff()
		{
			if (!this.InActiveZone)
			{
				return;
			}
			for (int i = 0; i < 15; i++)
			{
				float num = (float)Stat.Random(0, 359) / 58f;
				float num2 = (float)Math.Sin((double)num) / 4f;
				float num3 = (float)Math.Cos((double)num) / 4f;
				if (Stat.Random(1, 4) <= 3)
				{
					The.ParticleManager.Add("&y.", (float)this.X, (float)this.Y, num2, num3, 15, 0f, 0f, 0L);
				}
				else
				{
					The.ParticleManager.Add("&y±", (float)this.X, (float)this.Y, num2, num3, 15, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06006633 RID: 26163 RVA: 0x001FCA06 File Offset: 0x001FAC06
		public void FugueVFX()
		{
			CombatJuice.playPrefabAnimation(this.Location, "Particles/FugueClone", null, null, null, false);
		}

		/// <summary>
		/// Creates an ImpactVFXFlameburst if particle VFX are enabled. Otherwise it creates text particles if screenbuffer is not provided, otherwise renders directly into the buffer.
		/// </summary>
		/// <param name="Buffer" />
		// Token: 0x06006634 RID: 26164 RVA: 0x001FCA20 File Offset: 0x001FAC20
		public void Flameburst(ScreenBuffer Buffer = null)
		{
			if (Options.UseParticleVFX)
			{
				CombatJuice.playPrefabAnimation(this.Location, "Impacts/ImpactVFXFlameburst", null, null, null, false);
				return;
			}
			if (Buffer == null)
			{
				for (int i = 0; i < 3; i++)
				{
					string text = "&C";
					int num = Stat.Random(1, 3);
					if (num == 1)
					{
						text = "&R";
					}
					if (num == 2)
					{
						text = "&r";
					}
					if (num == 3)
					{
						text = "&W";
					}
					int num2 = Stat.Random(1, 3);
					if (num2 == 1)
					{
						text += "^R";
					}
					if (num2 == 2)
					{
						text += "^r";
					}
					if (num2 == 3)
					{
						text += "^W";
					}
					XRLCore.ParticleManager.Add(text + ((char)(219 + Stat.Random(0, 4))).ToString(), (float)this.X, (float)this.Y, 0f, 0f, 10 + (6 - 2 * i));
				}
				return;
			}
			Buffer.Goto(this.X, this.Y);
			string text2 = "&C";
			int num3 = Stat.Random(1, 3);
			if (num3 == 1)
			{
				text2 = "&R";
			}
			if (num3 == 2)
			{
				text2 = "&r";
			}
			if (num3 == 3)
			{
				text2 = "&W";
			}
			int num4 = Stat.Random(1, 3);
			if (num4 == 1)
			{
				text2 += "^R";
			}
			if (num4 == 2)
			{
				text2 += "^r";
			}
			if (num4 == 3)
			{
				text2 += "^W";
			}
			if (this.ParentZone == The.ActiveZone)
			{
				Stat.Random(1, 3);
				Buffer.Write(text2 + ((char)(219 + Stat.Random(0, 4))).ToString(), true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(Buffer, null, false);
				Thread.Sleep(10);
			}
		}

		// Token: 0x06006635 RID: 26165 RVA: 0x001FCBD0 File Offset: 0x001FADD0
		public void Smoke(int StartAngle, int EndAngle)
		{
			if (this == null || !this.ParentZone.IsActive())
			{
				return;
			}
			ParticleFX.Smoke(this.X, this.Y, StartAngle, EndAngle);
		}

		// Token: 0x06006636 RID: 26166 RVA: 0x001FCC03 File Offset: 0x001FAE03
		public void Smoke()
		{
			this.Smoke(85, 185);
		}

		// Token: 0x06006637 RID: 26167 RVA: 0x001FCC14 File Offset: 0x001FAE14
		public void PsychicPulse()
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					this.ParticleText("&B" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
				}
				for (int k = 0; k < 5; k++)
				{
					this.ParticleText("&b" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
				}
				for (int l = 0; l < 5; l++)
				{
					this.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
				}
			}
		}

		// Token: 0x06006638 RID: 26168 RVA: 0x001FCCE0 File Offset: 0x001FAEE0
		public void LargeFireblast()
		{
			for (int i = 2; i < 5; i++)
			{
				this.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 2);
			}
			for (int j = 2; j < 5; j++)
			{
				this.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 2);
			}
			for (int k = 2; k < 5; k++)
			{
				this.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 2);
			}
		}

		// Token: 0x06006639 RID: 26169 RVA: 0x001FCD98 File Offset: 0x001FAF98
		public void SmallFireblast()
		{
			for (int i = 0; i < 3; i++)
			{
				this.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
			}
			for (int j = 0; j < 3; j++)
			{
				this.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
			}
			for (int k = 0; k < 3; k++)
			{
				this.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
			}
		}

		// Token: 0x0600663A RID: 26170 RVA: 0x001FCE50 File Offset: 0x001FB050
		public void SpatialDistortionBlip(string Color = "&C")
		{
			if (!this.InActiveZone)
			{
				return;
			}
			The.ParticleManager.AddRadial(Color + "ù", (float)this.X, (float)this.Y, (float)Stat.Random(0, 5), (float)Stat.Random(4, 8), 0.01f * (float)Stat.Random(3, 5), -0.05f * (float)Stat.Random(2, 6));
		}

		// Token: 0x0600663B RID: 26171 RVA: 0x001FCEB8 File Offset: 0x001FB0B8
		public void TileParticleBlip(string Tile, string ColorString, string DetailColor, int Duration = 10, bool IgnoreVisibility = false, bool HFlip = false, bool VFlip = false, long DelayMS = 0L)
		{
			if (!this.InActiveZone)
			{
				return;
			}
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			The.ParticleManager.AddTile(Tile, ColorString, DetailColor, (float)this.X, (float)this.Y, 0f, 0f, Duration, 0f, 0f, HFlip, VFlip, DelayMS);
		}

		// Token: 0x0600663C RID: 26172 RVA: 0x001FCF14 File Offset: 0x001FB114
		public void ParticleBlip(string Text, int Duration = 10, long DelayMS = 0L, bool IgnoreVisibility = false)
		{
			if (!this.InActiveZone)
			{
				return;
			}
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			The.ParticleManager.Add(Text, (float)this.X, (float)this.Y, 0f, 0f, Duration, 0f, 0f, DelayMS);
		}

		// Token: 0x0600663D RID: 26173 RVA: 0x001FCF68 File Offset: 0x001FB168
		public void ParticleText(string Text, float Velocity, int Life)
		{
			if (!this.InActiveZone)
			{
				return;
			}
			float num = (float)Stat.Random(0, 359) / 58f;
			float num2 = (float)Math.Sin((double)num) / 4f;
			float num3 = (float)Math.Cos((double)num) / 4f;
			num2 *= Velocity;
			num3 *= Velocity;
			The.ParticleManager.Add(Markup.Transform(Text, false), (float)this.X, (float)this.Y, num2, num3, Life, 0f, 0f, 0L);
		}

		// Token: 0x0600663E RID: 26174 RVA: 0x001FCFE8 File Offset: 0x001FB1E8
		public void ParticleText(string Text, bool IgnoreVisibility = true)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			this.ParticleText(Text, 1f, 999);
		}

		// Token: 0x0600663F RID: 26175 RVA: 0x001FD008 File Offset: 0x001FB208
		public void ParticleText(string Text, int angleMin = 0, int angleMax = 359, bool IgnoreVisibility = true)
		{
			if (!this.InActiveZone)
			{
				return;
			}
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			float num = (float)Stat.Random(0, 359) / 58f;
			float num2 = (float)Math.Sin((double)num) / 4f;
			float num3 = (float)Math.Cos((double)num) / 4f;
			The.ParticleManager.Add(Text, (float)this.X, (float)this.Y, num2, num3, 999, 0f, 0f, 0L);
		}

		// Token: 0x06006640 RID: 26176 RVA: 0x001FD088 File Offset: 0x001FB288
		public void ParticleText(string Text, float xVel, float yVel, char Color = ' ', bool IgnoreVisibility = true)
		{
			if (!this.InActiveZone)
			{
				return;
			}
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			if (Color != ' ')
			{
				Text = string.Concat(new string[]
				{
					"{{",
					Color.ToString(),
					"|",
					Text,
					"}}"
				});
			}
			The.ParticleManager.Add(Text, (float)this.X, (float)this.Y, xVel, yVel, 999, 0f, 0f, 0L);
		}

		// Token: 0x06006641 RID: 26177 RVA: 0x001FD110 File Offset: 0x001FB310
		public void ParticleText(string Text, char Color, bool IgnoreVisibility = true, float juiceDuration = 1.5f, float floatLength = -8f, GameObject emitting = null)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			if (this.juiceEnabled)
			{
				if (Color == ' ')
				{
					Text = Markup.Transform(Text, false);
					Color = ConsoleLib.Console.ColorUtility.ParseForegroundColor(Text, 'y');
					Text = ConsoleLib.Console.ColorUtility.StripFormatting(Text);
				}
				CombatJuice.floatingText(this, Text, ConsoleLib.Console.ColorUtility.ColorMap[Color], juiceDuration, floatLength, 1f, true, emitting);
				return;
			}
			float num = (float)Stat.Random(0, 359) / 58f;
			float num2 = (float)Math.Sin((double)num) / 4f;
			float num3 = (float)Math.Cos((double)num) / 4f;
			if (Color != ' ')
			{
				Text = string.Concat(new string[]
				{
					"{{",
					Color.ToString(),
					"|",
					Text,
					"}}"
				});
			}
			The.ParticleManager.Add(Text, (float)this.X, (float)this.Y, num2, num3, 999, 0f, 0f, 0L);
		}

		// Token: 0x06006642 RID: 26178 RVA: 0x001FD208 File Offset: 0x001FB408
		public void ParticleText(string Text)
		{
			if (!this.InActiveZone)
			{
				return;
			}
			float num = (float)Stat.Random(0, 359) / 58f;
			float num2 = (float)Math.Sin((double)num) / 4f;
			float num3 = (float)Math.Cos((double)num) / 4f;
			The.ParticleManager.Add(Text, (float)this.X, (float)this.Y, num2, num3, 999, 0f, 0f, 0L);
		}

		// Token: 0x06006643 RID: 26179 RVA: 0x001FD27C File Offset: 0x001FB47C
		public GameObject GetCombatObject()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsCombatObject())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006644 RID: 26180 RVA: 0x001FD2C4 File Offset: 0x001FB4C4
		public GameObject GetCombatTarget(GameObject Attacker = null, bool IgnoreFlight = false, bool IgnoreAttackable = false, bool IgnorePhase = false, int Phase = 0, GameObject Projectile = null, GameObject Launcher = null, GameObject CheckPhaseAgainst = null, GameObject Skip = null, List<GameObject> SkipList = null, bool AllowInanimate = true, bool InanimateSolidOnly = false, Predicate<GameObject> Filter = null)
		{
			GameObject gameObject = null;
			if (IgnorePhase)
			{
				Phase = 5;
			}
			else if (Phase == 0)
			{
				if (CheckPhaseAgainst == null)
				{
					CheckPhaseAgainst = Projectile ?? Attacker;
				}
				Phase = ((CheckPhaseAgainst != null) ? CheckPhaseAgainst.GetPhase() : 5);
			}
			bool flag = !IgnoreFlight;
			bool flag2 = !IgnoreAttackable;
			bool flag3 = false;
			GameObject gameObject2 = Projectile ?? Attacker;
			GameObject gameObject3;
			using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
			{
				this.GetObjectsWithPart("Combat", fromPool, Phase, Attacker, Attacker, gameObject2, Projectile, Skip, SkipList, flag, flag2, flag3, Filter);
				if (fromPool.Count == 1)
				{
					gameObject = fromPool[0];
				}
				else if (fromPool.Count > 0)
				{
					fromPool.Sort(new CombatSorter(Attacker));
					gameObject = fromPool.FirstOrDefault<GameObject>();
				}
				if (gameObject == null && AllowInanimate)
				{
					if (!flag3)
					{
						if (InanimateSolidOnly)
						{
							flag3 = true;
						}
						else
						{
							gameObject3 = Launcher;
							if (this.IsSolidForProjectile(Projectile, Attacker, gameObject3, (Attacker != null) ? Attacker.Target : null, null, null, false))
							{
								flag3 = true;
							}
						}
					}
					this.GetRealNonSceneryObjects(fromPool, Phase, Attacker, Attacker, gameObject2, Projectile, Skip, SkipList, flag, flag2, flag3, Filter);
					if (fromPool.Count == 1)
					{
						gameObject = fromPool[0];
					}
					else if (fromPool.Count > 1)
					{
						fromPool.Sort(new CombatSorter(Attacker));
						gameObject = fromPool.FirstOrDefault<GameObject>();
					}
				}
				gameObject3 = gameObject;
			}
			return gameObject3;
		}

		// Token: 0x06006645 RID: 26181 RVA: 0x001FD430 File Offset: 0x001FB630
		public int GetTotalHostileDifficultyLevel(GameObject who)
		{
			int num = 0;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsHostileTowards(who))
				{
					num += this.Objects[i].Con(who, false).GetValueOrDefault();
				}
				i++;
			}
			return num;
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x001FD48C File Offset: 0x001FB68C
		public int GetTotalAdjacentHostileDifficultyLevel(GameObject who)
		{
			int num = 0;
			foreach (Cell cell in this.GetAdjacentCells(true))
			{
				num += cell.GetTotalHostileDifficultyLevel(who);
			}
			return num;
		}

		// Token: 0x06006647 RID: 26183 RVA: 0x001FD4E8 File Offset: 0x001FB6E8
		public GameObject GetOpenLiquidVolume()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsOpenLiquidVolume())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006648 RID: 26184 RVA: 0x001FD530 File Offset: 0x001FB730
		public GameObject GetDangerousOpenLiquidVolume()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsDangerousOpenLiquidVolume())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006649 RID: 26185 RVA: 0x001FD578 File Offset: 0x001FB778
		public GameObject GetWadingDepthLiquid()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWadingDepthLiquid())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600664A RID: 26186 RVA: 0x001FD5C0 File Offset: 0x001FB7C0
		public GameObject GetSwimmingDepthLiquid()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsSwimmingDepthLiquid())
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x001FD608 File Offset: 0x001FB808
		public bool HasSwimmingDepthLiquid()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsSwimmingDepthLiquid())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600664C RID: 26188 RVA: 0x001FD644 File Offset: 0x001FB844
		public bool MightBlockPaths()
		{
			return this.X > 0 && this.X < this.ParentZone.Width - 1 && this.Y > 0 && this.Y < this.ParentZone.Height - 1 && ((this.GetCellFromDirection("N", true).HasWall() && this.GetCellFromDirection("S", true).HasWall()) || (this.GetCellFromDirection("E", true).HasWall() && this.GetCellFromDirection("W", true).HasWall()));
		}

		// Token: 0x0600664D RID: 26189 RVA: 0x001FD6E4 File Offset: 0x001FB8E4
		public bool HasOpenLiquidVolume()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsOpenLiquidVolume())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x001FD720 File Offset: 0x001FB920
		public bool HasWadingDepthLiquid()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWadingDepthLiquid())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600664F RID: 26191 RVA: 0x001FD75C File Offset: 0x001FB95C
		public GameObject GetAquaticSupportFor(GameObject who)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsSwimmableFor(who))
				{
					return this.Objects[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06006650 RID: 26192 RVA: 0x001FD7A4 File Offset: 0x001FB9A4
		public bool HasAquaticSupportFor(GameObject who)
		{
			bool flag = false;
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsBridge)
				{
					return false;
				}
				if (!flag && this.Objects[i].IsSwimmableFor(who))
				{
					flag = true;
				}
				i++;
			}
			return flag;
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x001FD7FC File Offset: 0x001FB9FC
		public bool HasWalkableWallFor(GameObject who)
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsWalkableWall(who))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x001FD838 File Offset: 0x001FBA38
		public bool HasHealingPool()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsHealingPool())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x001FD874 File Offset: 0x001FBA74
		public bool HasTryingToJoinPartyLeader()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsTryingToJoinPartyLeader())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006654 RID: 26196 RVA: 0x001FD8B0 File Offset: 0x001FBAB0
		public bool HasTryingToJoinPartyLeaderForZoneUncaching()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsTryingToJoinPartyLeaderForZoneUncaching())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x001FD8EC File Offset: 0x001FBAEC
		public bool HasPlayerLed()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsPlayerLed())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x001FD928 File Offset: 0x001FBB28
		public bool HasWasPlayer()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].WasPlayer())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x001FD964 File Offset: 0x001FBB64
		public bool HasLeftBehindByPlayer()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].LeftBehindByPlayer())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x001FD99F File Offset: 0x001FBB9F
		public int GetHealingLocationValue(GameObject Actor)
		{
			return PollForHealingLocationEvent.GetFor(Actor, this, false);
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x001FD9A9 File Offset: 0x001FBBA9
		public bool IsHealingLocation(GameObject Actor)
		{
			return PollForHealingLocationEvent.GetFor(Actor, this, true) > 0;
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x001FD9B6 File Offset: 0x001FBBB6
		public void UseHealingLocation(GameObject Actor)
		{
			UseHealingLocationEvent.Send(Actor, this);
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x001FD9C0 File Offset: 0x001FBBC0
		public GameObject FindObjectByID(string id)
		{
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = FindObjectByIdEvent.Find(this.Objects[i], id);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x001FDA00 File Offset: 0x001FBC00
		public GameObject FindObjectByID(int ID)
		{
			for (int i = this.Objects.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = FindObjectByIdEvent.Find(this.Objects[i], ID);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x001FDA3E File Offset: 0x001FBC3E
		public bool OnWorldMap()
		{
			return this.ParentZone != null && this.ParentZone.IsWorldMap();
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x001FDA55 File Offset: 0x001FBC55
		public bool WantEvent(int ID, int Cascade)
		{
			return this.Objects.WantEvent(ID, Cascade);
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x001FDA64 File Offset: 0x001FBC64
		public bool HandleEvent(MinEvent E)
		{
			return this.Objects.HandleEvent(E);
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x001FDA72 File Offset: 0x001FBC72
		public bool HandleEvent(MinEvent E, IEvent ParentEvent)
		{
			bool flag = this.HandleEvent(E);
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x001FDA88 File Offset: 0x001FBC88
		public List<GameObject> GetObjectsThatWantEvent(int ID, int cascade)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].WantEvent(ID, cascade))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x001FDADC File Offset: 0x001FBCDC
		public int TemperatureChange(int Amount, GameObject Actor = null, bool Radiant = false, bool MinAmbient = false, bool MaxAmbient = false, bool IgnoreResistance = false, int Phase = 0)
		{
			if (Phase == 0 && Actor != null)
			{
				Phase = Actor.GetPhase();
			}
			int num = 0;
			int i = 0;
			int num2 = this.Objects.Count;
			while (i < num2)
			{
				GameObject gameObject = this.Objects[i];
				if (gameObject.TemperatureChange(Amount, Actor, Radiant, MinAmbient, MaxAmbient, IgnoreResistance, Phase, null, null))
				{
					num++;
				}
				if (num2 != this.Objects.Count)
				{
					num2 = this.Objects.Count;
					if (i < num2 && this.Objects[i] != gameObject)
					{
						i--;
					}
				}
				i++;
			}
			return num;
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x001FDB7C File Offset: 0x001FBD7C
		public void Splash(string Particle)
		{
			if (this.InActiveZone)
			{
				for (int i = 0; i < 3; i++)
				{
					float num = (float)Stat.RandomCosmetic(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 3f;
					float num3 = (float)Math.Cos((double)num) / 3f;
					The.ParticleManager.Add(Particle, (float)this.X, (float)this.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x001FDC00 File Offset: 0x001FBE00
		public void LiquidSplash(string Color)
		{
			if (this.InActiveZone)
			{
				for (int i = 0; i < 3; i++)
				{
					float num = (float)Stat.RandomCosmetic(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 3f;
					float num3 = (float)Math.Cos((double)num) / 3f;
					char c = '.';
					switch (Stat.Random(0, 5))
					{
					case 1:
						c = 'ú';
						break;
					case 2:
						c = 'ø';
						break;
					case 3:
						c = '~';
						break;
					case 4:
						c = 'ù';
						break;
					case 5:
						c = '\a';
						break;
					}
					The.ParticleManager.Add("&" + Color + c.ToString(), (float)this.X, (float)this.Y, num2, num3, 5, 0f, 0f, 0L);
					Thread.Sleep(Stat.Random(5, 15));
				}
			}
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x001FDCF8 File Offset: 0x001FBEF8
		public void LiquidSplash(List<string> Colors)
		{
			if (this.ParentZone.IsActive())
			{
				for (int i = 0; i < 3; i++)
				{
					float num = (float)Stat.RandomCosmetic(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 3f;
					float num3 = (float)Math.Cos((double)num) / 3f;
					char c = '.';
					switch (Stat.Random(0, 5))
					{
					case 1:
						c = 'ú';
						break;
					case 2:
						c = 'ø';
						break;
					case 3:
						c = '~';
						break;
					case 4:
						c = 'ù';
						break;
					case 5:
						c = '\a';
						break;
					}
					The.ParticleManager.Add("&" + Colors.GetRandomElement<string>(null) + c.ToString(), (float)this.X, (float)this.Y, num2, num3, 5, 0f, 0f, (long)(Stat.Random(5, 15) * i));
				}
			}
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x001FDDF6 File Offset: 0x001FBFF6
		public void LiquidSplash(BaseLiquid Liquid)
		{
			if (Liquid != null)
			{
				this.LiquidSplash(Liquid.GetColors());
			}
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x001FDE08 File Offset: 0x001FC008
		public void TelekinesisBlip()
		{
			if (this.InActiveZone)
			{
				int i = 0;
				int num = Stat.Random(2, 4);
				while (i < num)
				{
					int num2 = Stat.Random(0, 359);
					float num3 = (float)Stat.RandomCosmetic(4, 14) / 5f;
					The.ParticleManager.Add("@", (float)this.X, (float)this.Y, (float)Math.Sin((double)num2 * 0.017) / num3, (float)Math.Cos((double)num2 * 0.017) / num3, Stat.Random(3, 20));
					i++;
				}
			}
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x001FDE9C File Offset: 0x001FC09C
		public void DilationSplat()
		{
			if (this.InActiveZone)
			{
				for (int i = 0; i < 360; i++)
				{
					float num = (float)Stat.RandomCosmetic(4, 14) / 3f;
					The.ParticleManager.Add("@", (float)this.X, (float)this.Y, (float)Math.Sin((double)i * 0.017) / num, (float)Math.Cos((double)i * 0.017) / num);
				}
			}
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x001FDF18 File Offset: 0x001FC118
		public void ImplosionSplat(int Radius = 12)
		{
			if (this.InActiveZone)
			{
				for (int i = 0; i < 360; i++)
				{
					float num = (float)Stat.RandomCosmetic(1, 5) / ((float)Radius / 4f);
					The.ParticleManager.Add("@", (float)((int)Math.Round((double)this.X + (double)Radius * Math.Sin((double)i * 0.017))), (float)((int)Math.Round((double)this.Y + (double)Radius * Math.Cos((double)i * 0.017))), (float)(-(float)Math.Sin((double)i * 0.017)) / num, (float)(-(float)Math.Cos((double)i * 0.017)) / num, (int)Math.Round((double)((float)Radius * num)));
				}
			}
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x001FDFE4 File Offset: 0x001FC1E4
		public bool HasBridge()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].IsBridge)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x001FE020 File Offset: 0x001FC220
		public List<GameObject> GetSolidObjects()
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].ConsiderSolid())
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x001FE074 File Offset: 0x001FC274
		public List<GameObject> GetSolidObjectsFor(GameObject Actor)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].ConsiderSolidFor(Actor, false))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x001FE0C8 File Offset: 0x001FC2C8
		public List<GameObject> GetCanInteractInCellWithSolidObjectsFor(GameObject Actor)
		{
			List<GameObject> list = Event.NewGameObjectList();
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				if (this.Objects[i].CanInteractInCellWithSolid(Actor))
				{
					list.Add(this.Objects[i]);
				}
				i++;
			}
			return list;
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x001FE11C File Offset: 0x001FC31C
		public bool IsAudible(GameObject By, int Volume = 20)
		{
			return this.ParentZone != null && !this.ParentZone.IsWorldMap() && this.ParentZone.FastFloodAudibilityAny(this.X, this.Y, Volume, (GameObject GO) => GO == By, By);
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x001FE178 File Offset: 0x001FC378
		public bool IsSmellable(GameObject By, int Intensity = 5)
		{
			return this.ParentZone != null && !this.ParentZone.IsWorldMap() && this.ParentZone.FastFloodOlfactionAny(this.X, this.Y, Intensity, (GameObject GO) => GO == By, By);
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x001FE1D4 File Offset: 0x001FC3D4
		public Cell GetReversibleAccessUpCell()
		{
			if (this.HasObjectWithPart("StairsUp"))
			{
				Cell cellFromDirectionGlobalIfBuilt = this.GetCellFromDirectionGlobalIfBuilt("U");
				if (cellFromDirectionGlobalIfBuilt != null && cellFromDirectionGlobalIfBuilt.HasObjectWithPart("StairsDown"))
				{
					return cellFromDirectionGlobalIfBuilt;
				}
			}
			return null;
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x001FE210 File Offset: 0x001FC410
		public Cell GetReversibleAccessDownCell()
		{
			if (this.HasObjectWithPart("StairsDown"))
			{
				Cell cellFromDirectionGlobalIfBuilt = this.GetCellFromDirectionGlobalIfBuilt("D");
				if (cellFromDirectionGlobalIfBuilt != null && cellFromDirectionGlobalIfBuilt.HasObjectWithPart("StairsUp"))
				{
					return cellFromDirectionGlobalIfBuilt;
				}
			}
			return null;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06006672 RID: 26226 RVA: 0x001FE249 File Offset: 0x001FC449
		public bool InActiveZone
		{
			get
			{
				return this.ParentZone != null && this.ParentZone.IsActive();
			}
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x001FE260 File Offset: 0x001FC460
		public bool BlocksRadar()
		{
			return BlocksRadarEvent.Check(this);
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x001FE268 File Offset: 0x001FC468
		public bool IsSameOrAdjacent(Cell C, bool BuiltOnly = true)
		{
			return C == this || this.IsAdjacentTo(C, BuiltOnly);
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x001FE278 File Offset: 0x001FC478
		public bool Is(GlobalLocation loc)
		{
			string zoneID = loc.ZoneID;
			Zone parentZone = this.ParentZone;
			return !(zoneID != ((parentZone != null) ? parentZone.ZoneID : null)) && loc.CellX == this.X && loc.CellY == this.Y;
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x001FE2C7 File Offset: 0x001FC4C7
		public bool FastFloodVisibilityAny(int Radius, string SearchPart, GameObject Looker)
		{
			return this.ParentZone != null && this.ParentZone.FastFloodVisibilityAny(this.X, this.Y, Radius, SearchPart, Looker);
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x001FE2ED File Offset: 0x001FC4ED
		public bool FastFloodVisibilityAny(int Radius, Predicate<GameObject> Filter, GameObject Looker)
		{
			return this.ParentZone != null && this.ParentZone.FastFloodVisibilityAny(this.X, this.Y, Radius, Filter, Looker);
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x001FE313 File Offset: 0x001FC513
		public bool FastFloodAudibilityAny(int Radius, Predicate<GameObject> Filter, GameObject Hearer)
		{
			return this.ParentZone != null && this.ParentZone.FastFloodAudibilityAny(this.X, this.Y, Radius, Filter, Hearer);
		}

		// Token: 0x06006679 RID: 26233 RVA: 0x001FE339 File Offset: 0x001FC539
		public bool FastFloodOlfactionAny(int Radius, Predicate<GameObject> Filter, GameObject Smeller)
		{
			return this.ParentZone != null && this.ParentZone.FastFloodOlfactionAny(this.X, this.Y, Radius, Filter, Smeller);
		}

		// Token: 0x0600667A RID: 26234 RVA: 0x001FE35F File Offset: 0x001FC55F
		public List<Point> LineFromAngle(int degrees)
		{
			if (this.ParentZone == null)
			{
				return new List<Point>();
			}
			return this.ParentZone.LineFromAngle(this.X, this.Y, degrees);
		}

		// Token: 0x0600667B RID: 26235 RVA: 0x001FE388 File Offset: 0x001FC588
		public void Indicate(bool AsThreat = false)
		{
			if (this.InActiveZone)
			{
				if (this.juiceEnabled)
				{
					if (Options.UseTextAutoactInterruptionIndicator)
					{
						this.ParticleText("v", AsThreat ? 'R' : 'W', true, 1.5f, -8f, null);
						return;
					}
					CombatJuice.playPrefabAnimation(this.Location, AsThreat ? "Particles/AutostopNotificationThreat" : "Particles/AutostopNotificationNonThreat", null, null, null, false);
					return;
				}
				else
				{
					this.ParticleBlip(AsThreat ? "&RX" : "&WX", 10, 0L, true);
				}
			}
		}

		// Token: 0x0600667C RID: 26236 RVA: 0x001FE40C File Offset: 0x001FC60C
		public bool IsSolidGround()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				StairsDown stairsDown;
				if (this.Objects[i].TryGetPart<StairsDown>(out stairsDown) && stairsDown.PullDown)
				{
					return false;
				}
				i++;
			}
			return true;
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x0600667D RID: 26237 RVA: 0x001FE451 File Offset: 0x001FC651
		public bool juiceEnabled
		{
			get
			{
				return Options.UseOverlayCombatEffects;
			}
		}

		// Token: 0x0600667E RID: 26238 RVA: 0x001FE458 File Offset: 0x001FC658
		public string getTile()
		{
			return this.PaintTile;
		}

		// Token: 0x0600667F RID: 26239 RVA: 0x001FE460 File Offset: 0x001FC660
		public string getRenderString()
		{
			return this.PaintRenderString;
		}

		// Token: 0x06006680 RID: 26240 RVA: 0x001FE468 File Offset: 0x001FC668
		public string getColorString()
		{
			return this.PaintColorString;
		}

		// Token: 0x06006681 RID: 26241 RVA: 0x001FE470 File Offset: 0x001FC670
		public string getTileColor()
		{
			return this.PaintTileColor;
		}

		// Token: 0x06006682 RID: 26242 RVA: 0x001FE478 File Offset: 0x001FC678
		public char getDetailColor()
		{
			if (!this.PaintDetailColor.IsNullOrEmpty())
			{
				return this.PaintDetailColor[0];
			}
			return '\0';
		}

		// Token: 0x06006683 RID: 26243 RVA: 0x001FE498 File Offset: 0x001FC698
		public ColorChars getColorChars()
		{
			char c = 'y';
			char c2 = 'k';
			string text;
			if (Globals.RenderMode == RenderModeType.Tiles)
			{
				text = this.getTileColor();
				if (text.IsNullOrEmpty())
				{
					text = this.getColorString();
				}
			}
			else
			{
				text = this.getColorString();
			}
			if (!string.IsNullOrEmpty(text))
			{
				int num = text.LastIndexOf(ColorChars.FOREGROUND_INDICATOR);
				int num2 = text.LastIndexOf(ColorChars.BACKGROUND_INDICATOR);
				if (num >= 0 && num < text.Length - 1)
				{
					c = text[num + 1];
				}
				if (num2 >= 0 && num2 < text.Length - 1)
				{
					c2 = text[num2 + 1];
				}
			}
			return new ColorChars
			{
				detail = this.getDetailColor(),
				foreground = c,
				background = c2
			};
		}

		// Token: 0x06006684 RID: 26244 RVA: 0x001FE54F File Offset: 0x001FC74F
		public bool getHFlip()
		{
			return false;
		}

		// Token: 0x06006685 RID: 26245 RVA: 0x001FE552 File Offset: 0x001FC752
		public bool getVFlip()
		{
			return false;
		}

		// Token: 0x06006686 RID: 26246 RVA: 0x001FE555 File Offset: 0x001FC755
		public bool IsBlackedOut()
		{
			return this.GetLight() == LightLevel.Blackout;
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x001FE560 File Offset: 0x001FC760
		public bool IsAnyLocalAdjacentCellVisible()
		{
			using (List<Cell>.Enumerator enumerator = this.GetLocalAdjacentCells().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsVisible())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006688 RID: 26248 RVA: 0x001FE5BC File Offset: 0x001FC7BC
		public int GetAdjacencyCount(List<Cell> Cells)
		{
			int num = 0;
			foreach (Cell cell in Cells)
			{
				if (cell != this && this.IsAdjacentTo(cell, true))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06006689 RID: 26249 RVA: 0x001FE618 File Offset: 0x001FC818
		public int GetTotalDistance(List<Cell> Cells)
		{
			int num = 0;
			foreach (Cell cell in Cells)
			{
				if (cell != this)
				{
					num += this.DistanceTo(cell);
				}
			}
			return num;
		}

		// Token: 0x0600668A RID: 26250 RVA: 0x001FE670 File Offset: 0x001FC870
		public void WakeCreatures()
		{
			int i = 0;
			int count = this.Objects.Count;
			while (i < count)
			{
				Brain brain = this.Objects[i].Brain;
				if (brain != null)
				{
					brain.Wake();
				}
				i++;
			}
		}

		// Token: 0x0600668B RID: 26251 RVA: 0x001FE6B4 File Offset: 0x001FC8B4
		public void WakeCreaturesInArea()
		{
			this.WakeCreatures();
			List<Cell> localAdjacentCells = this.GetLocalAdjacentCells();
			int i = 0;
			int count = localAdjacentCells.Count;
			while (i < count)
			{
				localAdjacentCells[i].WakeCreatures();
				i++;
			}
		}

		// Token: 0x0600668C RID: 26252 RVA: 0x001FE6ED File Offset: 0x001FC8ED
		public float GetCover()
		{
			Zone parentZone = this.ParentZone;
			if (parentZone == null)
			{
				return 0f;
			}
			return parentZone.GetCoverAt(this.X, this.Y);
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x001FE710 File Offset: 0x001FC910
		public Cell.SpiralEnumerator IterateAdjacent(int Radius = 1, bool IncludeSelf = false, bool LocalOnly = false, bool BuiltOnly = true)
		{
			return new Cell.SpiralEnumerator(this, Radius, IncludeSelf, LocalOnly, BuiltOnly);
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x001FE71D File Offset: 0x001FC91D
		[Obsolete("Use FindObjectById")]
		public GameObject findObjectById(string id)
		{
			return this.FindObjectByID(id);
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600668F RID: 26255 RVA: 0x001FE726 File Offset: 0x001FC926
		[Obsolete("Use Location")]
		public Location2D location
		{
			get
			{
				return Location2D.Get(this.X, this.Y);
			}
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x001FE73C File Offset: 0x001FC93C
		internal void LogInvalidPhysics(GameObject Object)
		{
			XRL.World.Parts.Physics physics = Object.Physics;
			Cell cell = ((physics != null) ? physics._CurrentCell : null);
			string text = "Invalid physics object '{0}' in {1}, cell [{2},{3},{4}], actual [{5},{6},{7}] in {8}";
			object[] array = new object[9];
			array[0] = Object.DebugName;
			int num = 1;
			Zone parentZone = this.ParentZone;
			array[num] = ((parentZone != null) ? parentZone._ZoneID : null);
			array[2] = this.X;
			array[3] = this.Y;
			array[4] = this.HasStairs();
			array[5] = ((cell != null) ? cell.X : (-1));
			array[6] = ((cell != null) ? cell.Y : (-1));
			array[7] = ((cell != null) ? new bool?(cell.HasStairs()) : null);
			int num2 = 8;
			object obj;
			if (cell == null)
			{
				obj = null;
			}
			else
			{
				Zone parentZone2 = cell.ParentZone;
				obj = ((parentZone2 != null) ? parentZone2.ZoneID : null);
			}
			array[num2] = obj;
			string text2 = string.Format(text, array);
			string stringProperty = Object.GetStringProperty("LastInventoryActionCommand", null);
			if (!stringProperty.IsNullOrEmpty())
			{
				text2 = text2 + ", last action [" + stringProperty + "]";
			}
			XRLGame game = The.Game;
			RingDeque<Cell> ringDeque;
			if (game == null)
			{
				ringDeque = null;
			}
			else
			{
				GamePlayer player = game.Player;
				ringDeque = ((player != null) ? player.PlayerCells : null);
			}
			RingDeque<Cell> ringDeque2 = ringDeque;
			int num3 = ((ringDeque2 != null) ? ringDeque2.IndexOf(this) : (-1));
			if (num3 != -1)
			{
				Cell cell2 = ((num3 > 0) ? ringDeque2[num3 - 1] : null);
				Cell cell3 = ((num3 < ringDeque2.Count - 1) ? ringDeque2[num3 + 1] : null);
				text2 += string.Format(", last [{0},{1},{2}], next [{3},{4},{5}]", new object[]
				{
					(cell2 != null) ? cell2.X : (-1),
					(cell2 != null) ? cell2.Y : (-1),
					(cell2 != null) ? new bool?(cell2.HasStairs()) : null,
					(cell3 != null) ? cell3.X : (-1),
					(cell3 != null) ? cell3.Y : (-1),
					(cell3 != null) ? new bool?(cell3.HasStairs()) : null
				});
			}
			MetricsManager.LogError(text2);
			if (Object.IsPlayer() && !Cell.AskedReport)
			{
				Popup.Show("There was a duplication glitch involving your player character. It'd be helpful to send the save folder and Player.log to support@freeholdgames.com along with what you were currently doing.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Cell.AskedReport = true;
			}
		}

		// Token: 0x04002F15 RID: 12053
		[NonSerialized]
		private static RenderEvent eRender = new RenderEvent();

		// Token: 0x04002F16 RID: 12054
		[NonSerialized]
		private static Color ColorBlack = The.Color.DarkBlack;

		// Token: 0x04002F17 RID: 12055
		[NonSerialized]
		private static Color ColorBrightCyan = new Color(0f, 1f, 1f);

		// Token: 0x04002F18 RID: 12056
		[NonSerialized]
		private static Color ColorBrightGreen = new Color(0f, 0f, 1f);

		// Token: 0x04002F19 RID: 12057
		[NonSerialized]
		private static Color ColorBrightRed = new Color(1f, 0f, 0f);

		// Token: 0x04002F1A RID: 12058
		[NonSerialized]
		private static Color ColorDarkCyan = new Color(0f, 0.5f, 0.5f);

		// Token: 0x04002F1B RID: 12059
		[NonSerialized]
		private static Color ColorDarkGreen = new Color(0f, 0.5f, 0f);

		// Token: 0x04002F1C RID: 12060
		[NonSerialized]
		private static Color ColorDarkRed = new Color(0.5f, 0f, 0f);

		// Token: 0x04002F1D RID: 12061
		[NonSerialized]
		private static Color ColorGray = The.Color.Gray;

		// Token: 0x04002F1E RID: 12062
		public const int DISTANCE_INDEFINITE = 9999999;

		// Token: 0x04002F1F RID: 12063
		private const int FLAG_RENDER_INITIALIZED = 1;

		// Token: 0x04002F20 RID: 12064
		private const int FLAG_RENDER_COMPONENT = 2;

		// Token: 0x04002F21 RID: 12065
		private const int FLAG_RENDER_OVERLAY = 4;

		// Token: 0x04002F22 RID: 12066
		private const int FLAG_RENDER_FINAL = 8;

		// Token: 0x04002F23 RID: 12067
		public string PaintRenderString;

		// Token: 0x04002F24 RID: 12068
		public string PaintTileColor;

		// Token: 0x04002F25 RID: 12069
		public string PaintColorString;

		// Token: 0x04002F26 RID: 12070
		public string PaintTile;

		// Token: 0x04002F27 RID: 12071
		public string PaintDetailColor;

		// Token: 0x04002F28 RID: 12072
		[NonSerialized]
		internal bool Live;

		// Token: 0x04002F29 RID: 12073
		[NonSerialized]
		private int TransientCache;

		// Token: 0x04002F2A RID: 12074
		private static StringBuilder addressBuilder = new StringBuilder();

		// Token: 0x04002F2B RID: 12075
		public int X;

		// Token: 0x04002F2C RID: 12076
		public int Y;

		// Token: 0x04002F2D RID: 12077
		public Zone ParentZone;

		// Token: 0x04002F2E RID: 12078
		public Cell.ObjectRack Objects;

		// Token: 0x04002F2F RID: 12079
		public List<string> SemanticTags;

		// Token: 0x04002F30 RID: 12080
		public string _GroundLiquid;

		// Token: 0x04002F31 RID: 12081
		public const int DEFAULT_ALPHA = 230;

		// Token: 0x04002F32 RID: 12082
		[NonSerialized]
		private bool minimapCacheValid;

		// Token: 0x04002F33 RID: 12083
		[NonSerialized]
		public Color32 minimapCacheColor;

		// Token: 0x04002F34 RID: 12084
		[NonSerialized]
		public bool lastMinimapVisibile;

		// Token: 0x04002F35 RID: 12085
		[NonSerialized]
		public bool lastMinimapLit;

		// Token: 0x04002F36 RID: 12086
		[NonSerialized]
		public bool lastMinimapExplored;

		// Token: 0x04002F37 RID: 12087
		[NonSerialized]
		private static Color32 WhiteAlphaD = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 230);

		// Token: 0x04002F38 RID: 12088
		[NonSerialized]
		private static Color32 BlackAlpha32 = new Color32(0, 0, 0, 32);

		// Token: 0x04002F39 RID: 12089
		[NonSerialized]
		private static Color32 BlackAlpha128 = new Color32(0, 0, 0, 128);

		// Token: 0x04002F3A RID: 12090
		[NonSerialized]
		private static Color32 BlackAlpha164 = new Color32(0, 0, 0, 164);

		// Token: 0x04002F3B RID: 12091
		[NonSerialized]
		private static Color32 VioletAlphaD = new Color32(byte.MaxValue, 0, byte.MaxValue, 230);

		// Token: 0x04002F3C RID: 12092
		[NonSerialized]
		private static Color32 RedAlphaD = new Color32(byte.MaxValue, 0, 0, 230);

		// Token: 0x04002F3D RID: 12093
		[NonSerialized]
		private static Color32 GreenAlphaD = new Color32(0, byte.MaxValue, 0, 230);

		// Token: 0x04002F3E RID: 12094
		[NonSerialized]
		private static Color32 DarkYellowAlphaD = new Color32(128, 128, 0, 230);

		// Token: 0x04002F3F RID: 12095
		[NonSerialized]
		private static Color32 DarkBlueAlphaD = new Color32(0, 0, 128, 230);

		// Token: 0x04002F40 RID: 12096
		[NonSerialized]
		private static Color32 GrayAlphaD = new Color32(128, 128, 128, 230);

		// Token: 0x04002F41 RID: 12097
		[NonSerialized]
		private static Color32 CanaryAlphaD = new Color32(byte.MaxValue, byte.MaxValue, 128, 230);

		// Token: 0x04002F42 RID: 12098
		[NonSerialized]
		public Dictionary<int, int> NavigationWeightCache = new Dictionary<int, int>();

		// Token: 0x04002F43 RID: 12099
		public static readonly Color32 INVALID_CACHE = new Color32(1, 2, 3, 4);

		// Token: 0x04002F44 RID: 12100
		[NonSerialized]
		private static Event eBeforePhysicsRejectObjectEntringCell = new Event("BeforePhysicsRejectObjectEntringCell", "Object", null);

		// Token: 0x04002F45 RID: 12101
		public int OccludeCache = -1;

		// Token: 0x04002F46 RID: 12102
		private static List<GameObject> EventList = new List<GameObject>(8);

		// Token: 0x04002F47 RID: 12103
		private static bool EventListInUse = false;

		// Token: 0x04002F48 RID: 12104
		public static string[] DirectionList = new string[] { "NW", "N", "NE", "E", "SE", "S", "SW", "W" };

		// Token: 0x04002F49 RID: 12105
		public static string[] DirectionListCardinalFirst = new string[] { "N", "E", "S", "W", "NW", "SE", "SW", "NE" };

		// Token: 0x04002F4A RID: 12106
		public static string[] DirectionListCardinalOnly = new string[] { "N", "E", "S", "W" };

		// Token: 0x04002F4B RID: 12107
		[NonSerialized]
		private List<Cell> _LocalAdjacentCellCache;

		// Token: 0x04002F4C RID: 12108
		[NonSerialized]
		private List<Cell> _LocalCardinalAdjacentCellCache;

		// Token: 0x04002F4D RID: 12109
		[NonSerialized]
		internal static bool AskedReport = false;

		// Token: 0x02001A7B RID: 6779
		public class ObjectRack : Rack<GameObject>
		{
			// Token: 0x0600E7D7 RID: 59351 RVA: 0x00536E9C File Offset: 0x0053509C
			public ObjectRack()
			{
			}

			// Token: 0x0600E7D8 RID: 59352 RVA: 0x00536EA4 File Offset: 0x005350A4
			public ObjectRack(Cell ParentCell)
			{
				this.ParentCell = ParentCell;
			}

			// Token: 0x0600E7D9 RID: 59353 RVA: 0x00536EB4 File Offset: 0x005350B4
			public bool WantEvent(int ID, int Cascade)
			{
				if (ID == PooledEvent<GetContentsEvent>.ID)
				{
					return true;
				}
				int i = 0;
				int length = this.Length;
				while (i < length)
				{
					if (this.Items[i].WantEvent(ID, Cascade))
					{
						return true;
					}
					i++;
				}
				return false;
			}

			// Token: 0x0600E7DA RID: 59354 RVA: 0x00536EF4 File Offset: 0x005350F4
			public bool HandleEvent(MinEvent E)
			{
				GetContentsEvent getContentsEvent = E as GetContentsEvent;
				if (getContentsEvent != null)
				{
					for (int i = 0; i < this.Length; i++)
					{
						getContentsEvent.Objects.Add(this.Items[i]);
					}
				}
				int num = -1;
				int num2 = this.Length;
				int cascadeLevel = E.GetCascadeLevel();
				for (int j = 0; j < num2; j++)
				{
					if (this.Items[j].WantEvent(E.ID, cascadeLevel))
					{
						num = j;
						break;
					}
				}
				if (num != -1)
				{
					for (int k = num; k < num2; k++)
					{
						GameObject gameObject = this.Items[k];
						if (!E.Dispatch(gameObject))
						{
							return false;
						}
						if (num2 != this.Length)
						{
							num2 = this.Length;
							if (k < num2 && this.Items[k] != gameObject)
							{
								k--;
							}
						}
					}
				}
				return true;
			}

			// Token: 0x0600E7DB RID: 59355 RVA: 0x00536FC4 File Offset: 0x005351C4
			public override void Add(GameObject Item)
			{
				base.Add(Item);
				if (this.ParentCell.Live && (Item.Physics == null || Item.Physics._CurrentCell != this.ParentCell))
				{
					this.ParentCell.LogInvalidPhysics(Item);
				}
			}

			// Token: 0x0400696B RID: 26987
			[NonSerialized]
			public Cell ParentCell;
		}

		// Token: 0x02001A7C RID: 6780
		public struct SpiralEnumerator
		{
			// Token: 0x17000ECE RID: 3790
			// (get) Token: 0x0600E7DC RID: 59356 RVA: 0x00537001 File Offset: 0x00535201
			public Cell Current
			{
				get
				{
					return this.Cell;
				}
			}

			// Token: 0x0600E7DD RID: 59357 RVA: 0x0053700C File Offset: 0x0053520C
			public SpiralEnumerator(Cell Origin, int Radius = 1, bool IncludeSelf = false, bool LocalOnly = false, bool BuiltOnly = true)
			{
				this.Zone = Origin.ParentZone;
				this.Cell = Origin;
				this.X = Origin.X;
				this.Y = Origin.Y;
				this.StepX = 1;
				this.StepY = 0;
				this.Position = 0;
				this.Length = 1;
				Radius = Radius * 2 + 1;
				this.Count = Radius * Radius;
				this.LocalOnly = LocalOnly;
				this.BuiltOnly = BuiltOnly;
				if (!IncludeSelf)
				{
					this.X += this.StepX;
					this.Y += this.StepY;
					this.Position++;
					this.Count--;
				}
			}

			// Token: 0x0600E7DE RID: 59358 RVA: 0x005370C5 File Offset: 0x005352C5
			public Cell.SpiralEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600E7DF RID: 59359 RVA: 0x005370D0 File Offset: 0x005352D0
			public bool MoveNext()
			{
				for (;;)
				{
					int num = this.Count - 1;
					this.Count = num;
					if (num < 0)
					{
						break;
					}
					if (this.Position >= this.Length)
					{
						int stepX = this.StepX;
						this.StepX = -this.StepY;
						this.StepY = stepX;
						this.Position = 0;
						if (this.StepY == 0)
						{
							this.Length++;
						}
					}
					this.Cell = this.Zone.GetCellGlobal(this.X, this.Y, this.LocalOnly, this.BuiltOnly);
					this.X += this.StepX;
					this.Y += this.StepY;
					this.Position++;
					if (this.Cell != null)
					{
						return true;
					}
				}
				this.Cell = null;
				return false;
			}

			// Token: 0x0400696C RID: 26988
			private Zone Zone;

			// Token: 0x0400696D RID: 26989
			private Cell Cell;

			// Token: 0x0400696E RID: 26990
			private int X;

			// Token: 0x0400696F RID: 26991
			private int Y;

			// Token: 0x04006970 RID: 26992
			private int StepX;

			// Token: 0x04006971 RID: 26993
			private int StepY;

			// Token: 0x04006972 RID: 26994
			private int Position;

			// Token: 0x04006973 RID: 26995
			private int Length;

			// Token: 0x04006974 RID: 26996
			private int Count;

			// Token: 0x04006975 RID: 26997
			private bool LocalOnly;

			// Token: 0x04006976 RID: 26998
			private bool BuiltOnly;
		}
	}
}
