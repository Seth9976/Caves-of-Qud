using System;
using System.Collections.Generic;
using Genkit;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E79 RID: 3705
	[Serializable]
	public class GreaterVoider : IPart
	{
		// Token: 0x0600958A RID: 38282 RVA: 0x00361839 File Offset: 0x0035FA39
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600958B RID: 38283 RVA: 0x0036183C File Offset: 0x0035FA3C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600958C RID: 38284 RVA: 0x0036183F File Offset: 0x0035FA3F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GreaterVoiderBiteHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600958D RID: 38285 RVA: 0x00361854 File Offset: 0x0035FA54
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GreaterVoiderBiteHit")
			{
				if (this.lairRect == Rect2D.invalid)
				{
					return true;
				}
				if (this.lairZone == null)
				{
					return true;
				}
				if (this.ParentObject.InZone(this.lairZone) && this.lairRect.Contains(this.ParentObject.CurrentCell.Pos2D))
				{
					return true;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Target");
				if (gameObjectParameter != null)
				{
					List<Cell> emptyCells = The.ZoneManager.GetZone(this.lairZone).GetEmptyCells(this.lairRect);
					Cell cell = emptyCells.RemoveRandomElement<Cell>(null);
					Cell cell2 = emptyCells.RemoveRandomElement<Cell>(null);
					if (cell == null || cell2 == null)
					{
						return true;
					}
					Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
					@event.SetParameter("Object", gameObjectParameter);
					@event.SetParameter("Cell", cell);
					@event.SetParameter("Mutation", this);
					@event.SetParameter("RealityStabilizationPenetration", this.RealityStabilizationPenetration);
					Event event2 = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
					event2.SetParameter("Object", gameObjectParameter);
					event2.SetParameter("Cell", cell);
					event2.SetParameter("Mutation", this);
					event2.SetParameter("RealityStabilizationPenetration", this.RealityStabilizationPenetration);
					if (gameObjectParameter.FireEvent(@event) && cell.FireEvent(event2) && this.ParentObject.FireEvent(event2) && cell2.FireEvent(event2))
					{
						base.DidXToY("teleport", gameObjectParameter, "to " + this.ParentObject.its + " lair", "!", null, null, null, gameObjectParameter, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						if (gameObjectParameter.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", "appear"))
						{
							gameObjectParameter.TeleportSwirl(null, "&C", false, null, 'ù', true);
							if (this.ParentObject.TeleportTo(cell2, new int?(0), true, false, false, false, "disappear", "appear"))
							{
								this.ParentObject.TeleportSwirl(null, "&C", false, null, 'ù', false);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600958E RID: 38286 RVA: 0x00361A7F File Offset: 0x0035FC7F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600958F RID: 38287 RVA: 0x00361A98 File Offset: 0x0035FC98
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.createdLair)
			{
				try
				{
					this.CreateLair();
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Voider lair creation", ex, "game_exception");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009590 RID: 38288 RVA: 0x00361AE0 File Offset: 0x0035FCE0
		public FastNoise NoiseSetup(Zone Z)
		{
			FastNoise fastNoise = new FastNoise(The.Game.GetWorldSeed(Z.ZoneID));
			fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			fastNoise.SetFrequency(0.25f);
			fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			fastNoise.SetFractalOctaves(2);
			fastNoise.SetFractalLacunarity(1.7f);
			fastNoise.SetFractalGain(0.5f);
			return fastNoise;
		}

		// Token: 0x06009591 RID: 38289 RVA: 0x00361B38 File Offset: 0x0035FD38
		public void CreateLair()
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			if (GreaterVoider.LastLairZone == currentZone.ZoneID && GreaterVoider.LastLairRect.Area > 1)
			{
				this.lairRect = GreaterVoider.LastLairRect;
				this.lairZone = GreaterVoider.LastLairZone;
				this.createdLair = true;
				return;
			}
			int[,] unreachableOrWallGrid = currentZone.GetUnreachableOrWallGrid();
			Rect2D rect2D = GridTools.MaxRectByArea(unreachableOrWallGrid, false, 4, 4);
			bool flag = false;
			if (rect2D.Area <= 1)
			{
				for (int i = 0; i < currentZone.Height; i++)
				{
					for (int j = 0; j < currentZone.Width; j++)
					{
						unreachableOrWallGrid[j, i] = (currentZone.Map[j][i].HasObject(new Predicate<GameObject>(this.IsBlocker)) ? 1 : 0);
					}
				}
				flag = true;
				rect2D = GridTools.MaxRectByArea(unreachableOrWallGrid, false, 4, 4);
				if (rect2D.Area <= 1)
				{
					return;
				}
			}
			if (rect2D.Width > 7)
			{
				int num = (rect2D.Width - 7) / 2;
				rect2D.x1 += num;
				rect2D.x2 -= num + (rect2D.Width - 7) % 2;
			}
			if (rect2D.Height > 7)
			{
				int num2 = (rect2D.Height - 7) / 2;
				rect2D.y1 += num2;
				rect2D.y2 -= num2 + (rect2D.Height - 7) % 2;
			}
			string wall = this.GetWall(currentZone, rect2D);
			currentZone.ClearBox(rect2D);
			currentZone.FillHollowBox(rect2D, wall);
			FastNoise fastNoise = this.NoiseSetup(currentZone);
			Rect2D rect2D2 = (flag ? rect2D.ReduceBy(-1, -1) : rect2D);
			Rect2D rect2D3 = rect2D.ReduceBy(2, 2);
			for (int k = rect2D2.y1; k <= rect2D2.y2; k++)
			{
				for (int l = rect2D2.x1; l <= rect2D2.x2; l++)
				{
					if (!rect2D3.Contains(l, k))
					{
						Cell cell = currentZone.GetCell(l, k);
						if (cell != null && !cell.HasObject(new Predicate<GameObject>(this.IsBlocker)) && fastNoise.GetNoise((float)l, (float)k, (float)currentZone.Z) > 1f - (float)rect2D.Area / 80f)
						{
							cell.ClearAndAddObject(wall);
						}
					}
				}
			}
			GreaterVoider.LastLairZone = (this.lairZone = this.ParentObject.Physics.CurrentCell.ParentZone.ZoneID);
			GreaterVoider.LastLairRect = (this.lairRect = rect2D.ReduceBy(1, 1));
			this.createdLair = true;
			List<Cell> emptyCells = currentZone.GetEmptyCells(rect2D);
			if (10.in100())
			{
				emptyCells.RemoveRandomElement<Cell>(null).AddPopulation("DeadHumanoidWithGear");
			}
			foreach (Cell cell2 in emptyCells)
			{
				if (Stat.Random(1, 100) <= 25)
				{
					cell2.AddObject("Web", null, null, null, null);
				}
				if (Stat.Random(1, 100) <= 25)
				{
					cell2.AddObject("Bones", null, null, null, null);
				}
			}
		}

		// Token: 0x06009592 RID: 38290 RVA: 0x00361E68 File Offset: 0x00360068
		public string GetWall(Zone Z, Box R)
		{
			string text = null;
			GameObject terrainObject = Z.GetTerrainObject();
			if (Z.Z == 10 && GreaterVoider.SurfaceWalls.TryGetValue(terrainObject.GetPropertyOrTag("Terrain", ""), out text))
			{
				return text;
			}
			Location2D center = R.center;
			int num = 9999999;
			for (int i = 0; i < Z.Height; i++)
			{
				for (int j = 0; j < Z.Width; j++)
				{
					int num2 = center.ManhattanDistance(j, i);
					if (num2 < num)
					{
						int k = 0;
						int count = Z.Map[j][i].Objects.Count;
						while (k < count)
						{
							if (Z.Map[j][i].Objects[k].IsWall())
							{
								text = Z.Map[j][i].Objects[k].Blueprint;
								num = num2;
								break;
							}
							k++;
						}
					}
				}
			}
			return text ?? terrainObject.GetPropertyOrTag("TerrainWall", "Shale");
		}

		// Token: 0x06009593 RID: 38291 RVA: 0x00361F7C File Offset: 0x0036017C
		private bool IsBlocker(GameObject Object)
		{
			return !Object.CanClear(false, false) || (!Object.IsWall() && !Object.HasTag("Plant"));
		}

		// Token: 0x04003DDE RID: 15838
		public const int LOOT_CORPSE_CHANCE = 10;

		// Token: 0x04003DDF RID: 15839
		public static string LastLairZone = null;

		// Token: 0x04003DE0 RID: 15840
		public static Rect2D LastLairRect = Rect2D.invalid;

		// Token: 0x04003DE1 RID: 15841
		public int RealityStabilizationPenetration = 60;

		// Token: 0x04003DE2 RID: 15842
		public bool createdLair;

		// Token: 0x04003DE3 RID: 15843
		public string lairZone;

		// Token: 0x04003DE4 RID: 15844
		public Rect2D lairRect = Rect2D.invalid;

		// Token: 0x04003DE5 RID: 15845
		private static readonly Dictionary<string, string> SurfaceWalls = new Dictionary<string, string> { { "DeepJungle", "PlantWall" } };
	}
}
