using System;
using Genkit;
using Qud.API;

namespace XRL.World.WorldBuilders
{
	// Token: 0x02000999 RID: 2457
	[Serializable]
	public class GeneratedLocationInfo : IComposite
	{
		// Token: 0x06006A21 RID: 27169 RVA: 0x00218A96 File Offset: 0x00216C96
		public int distanceTo(Location2D location)
		{
			return this.zoneLocation.Distance(location);
		}

		// Token: 0x06006A22 RID: 27170 RVA: 0x00218AA4 File Offset: 0x00216CA4
		public bool isUndiscovered()
		{
			return !string.IsNullOrEmpty(this.secretID) && !JournalAPI.IsMapOrVillageNoteRevealed(this.secretID);
		}

		// Token: 0x06006A23 RID: 27171 RVA: 0x00218AC3 File Offset: 0x00216CC3
		public void Write(SerializationWriter Writer)
		{
			Writer.Write(this.zoneLocation);
		}

		// Token: 0x06006A24 RID: 27172 RVA: 0x00218AD1 File Offset: 0x00216CD1
		public void Read(SerializationReader Reader)
		{
			this.zoneLocation = Reader.ReadLocation2D();
		}

		// Token: 0x04003088 RID: 12424
		public string name;

		// Token: 0x04003089 RID: 12425
		public string ownerID;

		// Token: 0x0400308A RID: 12426
		public string secretID;

		// Token: 0x0400308B RID: 12427
		public string attribute;

		// Token: 0x0400308C RID: 12428
		[NonSerialized]
		public Location2D zoneLocation;

		// Token: 0x0400308D RID: 12429
		public string targetZone;
	}
}
