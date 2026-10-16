using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020012B2 RID: 4786
	[HasGameBasedStaticCache]
	[Serializable]
	public class VehicleRecord : IComposite
	{
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600BD3D RID: 48445 RVA: 0x00454354 File Offset: 0x00452554
		public static Dictionary<string, VehicleRecord> All
		{
			get
			{
				if (VehicleRecord._All == null)
				{
					VehicleRecord._All = The.Game.GetObjectGameState("Vehicle.Records") as Dictionary<string, VehicleRecord>;
					if (VehicleRecord._All == null)
					{
						VehicleRecord._All = new Dictionary<string, VehicleRecord>();
						The.Game.SetObjectGameState("Vehicle.Records", VehicleRecord._All);
					}
				}
				return VehicleRecord._All;
			}
		}

		// Token: 0x0600BD3E RID: 48446 RVA: 0x004543AC File Offset: 0x004525AC
		public static List<VehicleRecord> GetRecordsFor(GameObject Owner = null, string Blueprint = null, string Type = null)
		{
			List<VehicleRecord> list = new List<VehicleRecord>();
			foreach (KeyValuePair<string, VehicleRecord> keyValuePair in VehicleRecord.All)
			{
				if ((Blueprint.IsNullOrEmpty() || !(keyValuePair.Value.Blueprint != Blueprint)) && (Type.IsNullOrEmpty() || !(keyValuePair.Value.Type != Type)))
				{
					if (Owner != null)
					{
						if (Owner.IsPlayer())
						{
							if (keyValuePair.Value.OwnerID != "Player")
							{
								continue;
							}
						}
						else if (!Owner.IDMatch(keyValuePair.Value.OwnerID))
						{
							continue;
						}
					}
					list.Add(keyValuePair.Value);
				}
			}
			return list;
		}

		// Token: 0x0600BD3F RID: 48447 RVA: 0x00454484 File Offset: 0x00452684
		public static List<GameObject> ResolveRecordsFor(GameObject Owner = null, string Blueprint = null, string Type = null)
		{
			List<VehicleRecord> recordsFor = VehicleRecord.GetRecordsFor(Owner, Blueprint, Type);
			List<GameObject> list = new List<GameObject>(recordsFor.Count);
			foreach (VehicleRecord vehicleRecord in recordsFor)
			{
				GameObject gameObject;
				if (The.ZoneManager.CachedObjects.TryGetValue(vehicleRecord.ID, out gameObject))
				{
					list.Add(gameObject);
				}
				else
				{
					Cell cell = vehicleRecord.Location.ResolveCell();
					if (cell != null)
					{
						gameObject = cell.FindObjectByID(vehicleRecord.ID) ?? cell.ParentZone.FindObjectByID(vehicleRecord.ID);
						if (gameObject != null)
						{
							if (gameObject.IsTemporary)
							{
								VehicleRecord.All.Remove(vehicleRecord.ID);
							}
							else
							{
								list.Add(gameObject);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x04004BD1 RID: 19409
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static Dictionary<string, VehicleRecord> _All;

		// Token: 0x04004BD2 RID: 19410
		public string ID;

		// Token: 0x04004BD3 RID: 19411
		public string OwnerID;

		// Token: 0x04004BD4 RID: 19412
		public string Blueprint;

		// Token: 0x04004BD5 RID: 19413
		public string Type;

		// Token: 0x04004BD6 RID: 19414
		public GlobalLocation Location;
	}
}
