using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;
using HistoryKit;
using UnityEngine;
using XRL.Rules;

namespace XRL.World.WorldBuilders
{
	// Token: 0x0200099F RID: 2463
	[Serializable]
	public class WorldInfo : IComposite
	{
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06006A87 RID: 27271 RVA: 0x002237BC File Offset: 0x002219BC
		public IEnumerable<GeneratedLocationInfo> allLocationTypes
		{
			get
			{
				return this.lairs.Concat<GeneratedLocationInfo>(this.ruins).Concat<GeneratedLocationInfo>(this.villages).Concat<GeneratedLocationInfo>(this.enemySettlements)
					.Concat<GeneratedLocationInfo>(this.friendlySettlements);
			}
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x002237F0 File Offset: 0x002219F0
		public void Write(SerializationWriter Writer)
		{
			Writer.Write(this.terrainLocations.Count);
			foreach (KeyValuePair<string, List<Location2D>> keyValuePair in this.terrainLocations)
			{
				Writer.Write(keyValuePair.Key);
				Writer.Write(keyValuePair.Value.Count);
				foreach (Location2D location2D in keyValuePair.Value)
				{
					Writer.Write(location2D);
				}
			}
			Writer.Write(this.tierLocations.Count);
			foreach (KeyValuePair<int, List<Location2D>> keyValuePair2 in this.tierLocations)
			{
				Writer.Write(keyValuePair2.Key);
				Writer.Write(keyValuePair2.Value.Count);
				foreach (Location2D location2D2 in keyValuePair2.Value)
				{
					Writer.Write(location2D2);
				}
			}
			int num = this.RoadSystem.GetLength(0);
			int num2 = this.RoadSystem.GetLength(1);
			Writer.Write(num);
			Writer.Write(num2);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num2; j++)
				{
					Writer.WriteOptimized(this.RoadSystem[i, j]);
				}
			}
			num = this.RiverSystem.GetLength(0);
			num2 = this.RiverSystem.GetLength(1);
			Writer.Write(num);
			Writer.Write(num2);
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < num2; l++)
				{
					Writer.WriteOptimized(this.RiverSystem[k, l]);
				}
			}
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x00223A20 File Offset: 0x00221C20
		public void Read(SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			this.terrainLocations.EnsureCapacity(num);
			for (int i = 0; i < num; i++)
			{
				this.terrainLocations[Reader.ReadString()] = Reader.ReadLocation2DList();
			}
			num = Reader.ReadInt32();
			this.tierLocations.EnsureCapacity(num);
			for (int j = 0; j < num; j++)
			{
				this.tierLocations[Reader.ReadInt32()] = Reader.ReadLocation2DList();
			}
			int num2 = Reader.ReadInt32();
			int num3 = Reader.ReadInt32();
			this.RoadSystem = new uint[num2, num3];
			for (int k = 0; k < num2; k++)
			{
				for (int l = 0; l < num3; l++)
				{
					this.RoadSystem[k, l] = Reader.ReadOptimizedUInt32();
				}
			}
			num2 = Reader.ReadInt32();
			num3 = Reader.ReadInt32();
			this.RiverSystem = new uint[num2, num3];
			for (int m = 0; m < num2; m++)
			{
				for (int n = 0; n < num3; n++)
				{
					this.RiverSystem[m, n] = Reader.ReadOptimizedUInt32();
				}
			}
		}

		// Token: 0x06006A8A RID: 27274 RVA: 0x00223B40 File Offset: 0x00221D40
		public void ForeachGeneratedLocation(Action<GeneratedLocationInfo> action)
		{
			foreach (GeneratedLocationInfo generatedLocationInfo in this.allLocationTypes)
			{
				action(generatedLocationInfo);
			}
		}

		// Token: 0x06006A8B RID: 27275 RVA: 0x00223B90 File Offset: 0x00221D90
		public bool AnyGeneratedLocation(Predicate<GeneratedLocationInfo> check)
		{
			foreach (GeneratedLocationInfo generatedLocationInfo in this.allLocationTypes)
			{
				if (check(generatedLocationInfo))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006A8C RID: 27276 RVA: 0x00223BE8 File Offset: 0x00221DE8
		private void RequireLocationToLocationMap()
		{
			if (this.locationTolocationMap == null)
			{
				this.locationTolocationMap = new Dictionary<Location2D, List<GeneratedLocationInfo>>();
				foreach (GeneratedLocationInfo generatedLocationInfo in this.allLocationTypes)
				{
					List<GeneratedLocationInfo> list;
					if (!this.locationTolocationMap.TryGetValue(generatedLocationInfo.zoneLocation, out list))
					{
						list = new List<GeneratedLocationInfo>();
						this.locationTolocationMap.Add(generatedLocationInfo.zoneLocation, list);
					}
					list.Add(generatedLocationInfo);
				}
			}
		}

		// Token: 0x06006A8D RID: 27277 RVA: 0x00223C78 File Offset: 0x00221E78
		public GeneratedLocationInfo GetGeneratedLocationAt(Location2D location)
		{
			if (location == null)
			{
				return null;
			}
			this.RequireLocationToLocationMap();
			List<GeneratedLocationInfo> list;
			if (this.locationTolocationMap.TryGetValue(location, out list))
			{
				return list.GetRandomElement<GeneratedLocationInfo>(null);
			}
			return null;
		}

		// Token: 0x06006A8E RID: 27278 RVA: 0x00223CB0 File Offset: 0x00221EB0
		public List<GeneratedLocationInfo> GetGeneratedLocationsAt(Location2D location)
		{
			if (location == null)
			{
				return null;
			}
			this.RequireLocationToLocationMap();
			List<GeneratedLocationInfo> list;
			if (this.locationTolocationMap.TryGetValue(location, out list))
			{
				return list;
			}
			return null;
		}

		// Token: 0x06006A8F RID: 27279 RVA: 0x00223CE4 File Offset: 0x00221EE4
		public GeneratedLocationInfo FindLocationAlongPathFromLandmark(Location2D landmarkLocation, out string pathType, out string directionToDestination, out string directionFromLandmark, Predicate<Location2D> isValid = null)
		{
			List<uint[,]> list = new List<uint[,]> { this.RoadSystem, this.RiverSystem };
			List<string> list2 = new List<string> { "road", "river" };
			GeneratedLocationInfo generatedLocationInfo = null;
			pathType = null;
			directionToDestination = null;
			directionFromLandmark = null;
			string text = null;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i][landmarkLocation.X, landmarkLocation.Y] != 0U)
				{
					WorldInfo.LocationsToVisit.Clear();
					WorldInfo.VisitedLocations.Clear();
					WorldInfo.VisitedLocations.Add(landmarkLocation);
					WorldInfo.LocationsToVisit.Push(landmarkLocation);
					WorldInfo.LastStepDirectionStack.Push(".");
					while (WorldInfo.LocationsToVisit.Count > 0)
					{
						Location2D location2D = WorldInfo.LocationsToVisit.Pop();
						string text2 = WorldInfo.LastStepDirectionStack.Pop();
						if (WorldInfo.OriginalDirectionStack.Count > 0)
						{
							text = WorldInfo.OriginalDirectionStack.Pop();
						}
						if (!(location2D == null))
						{
							if (location2D != landmarkLocation)
							{
								generatedLocationInfo = this.GetGeneratedLocationAt(location2D);
								if (generatedLocationInfo != null && (isValid == null || isValid(generatedLocationInfo.zoneLocation)))
								{
									directionToDestination = Directions.GetExpandedDirection(text);
									directionFromLandmark = Directions.GetExpandedDirection(Directions.GetOppositeDirection(text2));
									pathType = list2[i];
									goto IL_03FE;
								}
							}
							if (location2D != null && location2D.X >= 0 && location2D.Y >= 0 && location2D.X < 240 && location2D.Y < 75)
							{
								if ((list[i][location2D.X, location2D.Y] & WorldInfo.ROAD_NORTH) > 0U && !WorldInfo.VisitedLocations.Contains(location2D.FromDirection("N")))
								{
									WorldInfo.LocationsToVisit.Push(location2D.FromDirection("N"));
									WorldInfo.VisitedLocations.Add(location2D.FromDirection("N"));
									if (text == null)
									{
										WorldInfo.OriginalDirectionStack.Push("N");
									}
									else
									{
										WorldInfo.OriginalDirectionStack.Push(text);
									}
									WorldInfo.LastStepDirectionStack.Push("N");
								}
								if ((list[i][location2D.X, location2D.Y] & WorldInfo.ROAD_SOUTH) > 0U && !WorldInfo.VisitedLocations.Contains(location2D.FromDirection("S")))
								{
									WorldInfo.LocationsToVisit.Push(location2D.FromDirection("S"));
									WorldInfo.VisitedLocations.Add(location2D.FromDirection("S"));
									if (text == null)
									{
										WorldInfo.OriginalDirectionStack.Push("S");
									}
									else
									{
										WorldInfo.OriginalDirectionStack.Push(text);
									}
									WorldInfo.LastStepDirectionStack.Push("S");
								}
								if ((list[i][location2D.X, location2D.Y] & WorldInfo.ROAD_EAST) > 0U && !WorldInfo.VisitedLocations.Contains(location2D.FromDirection("E")))
								{
									WorldInfo.LocationsToVisit.Push(location2D.FromDirection("E"));
									WorldInfo.VisitedLocations.Add(location2D.FromDirection("E"));
									if (text == null)
									{
										WorldInfo.OriginalDirectionStack.Push("E");
									}
									else
									{
										WorldInfo.OriginalDirectionStack.Push(text);
									}
									WorldInfo.LastStepDirectionStack.Push("E");
								}
								if ((list[i][location2D.X, location2D.Y] & WorldInfo.ROAD_WEST) > 0U && !WorldInfo.VisitedLocations.Contains(location2D.FromDirection("W")))
								{
									WorldInfo.LocationsToVisit.Push(location2D.FromDirection("W"));
									WorldInfo.VisitedLocations.Add(location2D.FromDirection("W"));
									if (text == null)
									{
										WorldInfo.OriginalDirectionStack.Push("W");
									}
									else
									{
										WorldInfo.OriginalDirectionStack.Push(text);
									}
									WorldInfo.LastStepDirectionStack.Push("W");
								}
							}
						}
					}
				}
			}
			IL_03FE:
			WorldInfo.OriginalDirectionStack.Clear();
			WorldInfo.LocationsToVisit.Clear();
			WorldInfo.VisitedLocations.Clear();
			WorldInfo.LastStepDirectionStack.Clear();
			return generatedLocationInfo;
		}

		// Token: 0x06006A90 RID: 27280 RVA: 0x00224118 File Offset: 0x00222318
		public DynamicQuestDeliveryTarget ResolveDeliveryTargetForVillage(HistoricEntitySnapshot village, int minDistanceInclusive = 12, int maxDistanceInclusive = 18, int tierBroaden = 0, Predicate<Location2D> isLocationVaild = null)
		{
			DynamicQuestDeliveryTarget dynamicQuestDeliveryTarget = new DynamicQuestDeliveryTarget();
			dynamicQuestDeliveryTarget.type = PopulationManager.RollOneFrom("Delivery Quest Locations", null, null).Blueprint;
			Location2D villageLocation = Zone.zoneIDTo240x72Location(village.GetProperty("zoneID", "unknown"));
			if (dynamicQuestDeliveryTarget.type == "Lair")
			{
				GeneratedLocationInfo randomElement = this.lairs.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => l != null && (isLocationVaild(l.zoneLocation) && villageLocation.ManhattanDistance(l.zoneLocation) >= minDistanceInclusive) && villageLocation.ManhattanDistance(l.zoneLocation) <= maxDistanceInclusive).GetRandomElement<GeneratedLocationInfo>(null);
				if (randomElement == null)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Couldn't find lair for village: ",
						village.GetProperty("name", "unknown"),
						" at ",
						minDistanceInclusive.ToString(),
						" to ",
						maxDistanceInclusive.ToString()
					}));
					return null;
				}
				dynamicQuestDeliveryTarget.displayName = randomElement.name;
				dynamicQuestDeliveryTarget.zoneId = randomElement.targetZone;
				dynamicQuestDeliveryTarget.secretId = randomElement.secretID;
				dynamicQuestDeliveryTarget.location = randomElement.zoneLocation;
			}
			else if (dynamicQuestDeliveryTarget.type == "Ruins")
			{
				GeneratedLocationInfo randomElement2 = this.ruins.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => isLocationVaild(l.zoneLocation) && villageLocation.ManhattanDistance(l.zoneLocation) >= minDistanceInclusive && villageLocation.ManhattanDistance(l.zoneLocation) <= maxDistanceInclusive).GetRandomElement<GeneratedLocationInfo>(null);
				if (randomElement2 == null)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Couldn't find ruin for village: ",
						village.GetProperty("name", "unknown"),
						" at ",
						minDistanceInclusive.ToString(),
						" to ",
						maxDistanceInclusive.ToString()
					}));
					return null;
				}
				dynamicQuestDeliveryTarget.displayName = randomElement2.name;
				dynamicQuestDeliveryTarget.zoneId = randomElement2.targetZone;
				dynamicQuestDeliveryTarget.secretId = randomElement2.secretID;
				dynamicQuestDeliveryTarget.location = randomElement2.zoneLocation;
			}
			else if (dynamicQuestDeliveryTarget.type == "Village")
			{
				GeneratedLocationInfo randomElement3 = this.villages.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => isLocationVaild(l.zoneLocation) && villageLocation.ManhattanDistance(l.zoneLocation) >= minDistanceInclusive && villageLocation.ManhattanDistance(l.zoneLocation) <= maxDistanceInclusive).GetRandomElement<GeneratedLocationInfo>(null);
				if (randomElement3 == null)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Couldn't find village for village: ",
						village.GetProperty("name", "unknown"),
						" at ",
						minDistanceInclusive.ToString(),
						" to ",
						maxDistanceInclusive.ToString()
					}));
					return null;
				}
				dynamicQuestDeliveryTarget.displayName = randomElement3.name;
				dynamicQuestDeliveryTarget.zoneId = randomElement3.targetZone;
				dynamicQuestDeliveryTarget.secretId = randomElement3.secretID;
				dynamicQuestDeliveryTarget.location = randomElement3.zoneLocation;
			}
			else if (dynamicQuestDeliveryTarget.type == "EnemySettlement")
			{
				GeneratedLocationInfo randomElement4 = this.enemySettlements.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => isLocationVaild(l.zoneLocation) && villageLocation.ManhattanDistance(l.zoneLocation) >= minDistanceInclusive && villageLocation.ManhattanDistance(l.zoneLocation) <= maxDistanceInclusive).GetRandomElement<GeneratedLocationInfo>(null);
				if (randomElement4 == null)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Couldn't find enemy settlement for village: ",
						village.GetProperty("name", "unknown"),
						" at ",
						minDistanceInclusive.ToString(),
						" to ",
						maxDistanceInclusive.ToString()
					}));
					return null;
				}
				dynamicQuestDeliveryTarget.displayName = randomElement4.name;
				dynamicQuestDeliveryTarget.zoneId = randomElement4.targetZone;
				dynamicQuestDeliveryTarget.secretId = randomElement4.secretID;
				dynamicQuestDeliveryTarget.location = randomElement4.zoneLocation;
			}
			else if (dynamicQuestDeliveryTarget.type == "FriendlySettlement")
			{
				GeneratedLocationInfo randomElement5 = this.friendlySettlements.Where<GeneratedLocationInfo>((GeneratedLocationInfo l) => isLocationVaild(l.zoneLocation) && villageLocation.ManhattanDistance(l.zoneLocation) >= minDistanceInclusive && villageLocation.ManhattanDistance(l.zoneLocation) <= maxDistanceInclusive).GetRandomElement<GeneratedLocationInfo>(null);
				if (randomElement5 == null)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Couldn't find friendly settlement for village: ",
						village.GetProperty("name", "unknown"),
						" at ",
						minDistanceInclusive.ToString(),
						" to ",
						maxDistanceInclusive.ToString()
					}));
					return null;
				}
				dynamicQuestDeliveryTarget.displayName = randomElement5.name;
				dynamicQuestDeliveryTarget.zoneId = randomElement5.targetZone;
				dynamicQuestDeliveryTarget.secretId = randomElement5.secretID;
				dynamicQuestDeliveryTarget.location = randomElement5.zoneLocation;
			}
			else if (dynamicQuestDeliveryTarget.type == "Wilds")
			{
				throw new NotImplementedException();
			}
			return dynamicQuestDeliveryTarget;
		}

		// Token: 0x06006A91 RID: 27281 RVA: 0x00224575 File Offset: 0x00222775
		public void init()
		{
		}

		// Token: 0x06006A92 RID: 27282 RVA: 0x00224577 File Offset: 0x00222777
		public void worldBuild()
		{
		}

		// Token: 0x040030BA RID: 12474
		[NonSerialized]
		public Dictionary<string, List<Location2D>> terrainLocations = new Dictionary<string, List<Location2D>>();

		// Token: 0x040030BB RID: 12475
		[NonSerialized]
		public Dictionary<int, List<Location2D>> tierLocations = new Dictionary<int, List<Location2D>>();

		// Token: 0x040030BC RID: 12476
		public List<GeneratedLocationInfo> lairs = new List<GeneratedLocationInfo>();

		// Token: 0x040030BD RID: 12477
		public List<GeneratedLocationInfo> ruins = new List<GeneratedLocationInfo>();

		// Token: 0x040030BE RID: 12478
		public List<GeneratedLocationInfo> villages = new List<GeneratedLocationInfo>();

		// Token: 0x040030BF RID: 12479
		public List<GeneratedLocationInfo> enemySettlements = new List<GeneratedLocationInfo>();

		// Token: 0x040030C0 RID: 12480
		public List<GeneratedLocationInfo> friendlySettlements = new List<GeneratedLocationInfo>();

		// Token: 0x040030C1 RID: 12481
		public static uint ROAD_NORTH = 1U;

		// Token: 0x040030C2 RID: 12482
		public static uint ROAD_SOUTH = 2U;

		// Token: 0x040030C3 RID: 12483
		public static uint ROAD_EAST = 4U;

		// Token: 0x040030C4 RID: 12484
		public static uint ROAD_WEST = 8U;

		// Token: 0x040030C5 RID: 12485
		public static uint ROAD_NONE = 16U;

		// Token: 0x040030C6 RID: 12486
		public static uint ROAD_START = 32U;

		// Token: 0x040030C7 RID: 12487
		[NonSerialized]
		public uint[,] RoadSystem;

		// Token: 0x040030C8 RID: 12488
		[NonSerialized]
		public uint[,] RiverSystem;

		// Token: 0x040030C9 RID: 12489
		private static Stack<string> OriginalDirectionStack = new Stack<string>();

		// Token: 0x040030CA RID: 12490
		private static Stack<Location2D> LocationsToVisit = new Stack<Location2D>();

		// Token: 0x040030CB RID: 12491
		private static Stack<string> LastStepDirectionStack = new Stack<string>();

		// Token: 0x040030CC RID: 12492
		private static HashSet<Location2D> VisitedLocations = new HashSet<Location2D>();

		// Token: 0x040030CD RID: 12493
		[NonSerialized]
		public Dictionary<Location2D, List<GeneratedLocationInfo>> locationTolocationMap;
	}
}
