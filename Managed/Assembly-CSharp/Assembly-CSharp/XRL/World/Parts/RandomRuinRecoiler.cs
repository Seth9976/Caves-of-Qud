using System;
using System.Collections.Generic;
using System.Text;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001088 RID: 4232
	[Serializable]
	public class RandomRuinRecoiler : IPart
	{
		// Token: 0x0600A865 RID: 43109 RVA: 0x003D94D1 File Offset: 0x003D76D1
		public override bool SameAs(IPart p)
		{
			return !((p as RandomRuinRecoiler).additionalDescription != this.additionalDescription) && base.SameAs(p);
		}

		// Token: 0x0600A866 RID: 43110 RVA: 0x003D94F4 File Offset: 0x003D76F4
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600A867 RID: 43111 RVA: 0x003D94F7 File Offset: 0x003D76F7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A868 RID: 43112 RVA: 0x003D9520 File Offset: 0x003D7720
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				if (The.ZoneManager != null && (this.NameCache == null || The.ZoneManager.NameUpdateTick > this.NameCacheTick))
				{
					Teleporter part = this.ParentObject.GetPart<Teleporter>();
					if (part != null)
					{
						string destinationZone = part.DestinationZone;
						if (!destinationZone.IsNullOrEmpty())
						{
							string text = RandomRuinRecoiler.FindZoneName(destinationZone);
							if (!text.IsNullOrEmpty())
							{
								text += " ";
							}
							this.NameCache = text;
						}
					}
					this.NameCacheTick = The.ZoneManager.NameUpdateTick;
				}
				if (!this.NameCache.IsNullOrEmpty())
				{
					string text2 = E.DB.PrimaryBase;
					int num = 10;
					if (text2 == null)
					{
						text2 = "recoiler";
					}
					else
					{
						num = E.DB[text2];
						E.DB.Remove(text2);
					}
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append(this.NameCache);
					if (text2.StartsWith("random-point "))
					{
						stringBuilder.Append(text2.Substring(13));
					}
					else
					{
						stringBuilder.Append(text2);
					}
					E.AddBase(stringBuilder.ToString(), num - 10, false);
				}
			}
			return true;
		}

		// Token: 0x0600A869 RID: 43113 RVA: 0x003D9650 File Offset: 0x003D7850
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.additionalDescription.IsNullOrEmpty())
			{
				E.Postfix.Append(this.additionalDescription);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A86A RID: 43114 RVA: 0x003D9678 File Offset: 0x003D7878
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (E.Context != "Sample" && E.Context != "Initialization" && E.Context != "GameStarted" && The.Game != null)
			{
				List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote note) => note.Has("ruins") || note.Has("historic"));
				if (mapNotes.Count > 0)
				{
					JournalMapNote randomElement = mapNotes.GetRandomElement<JournalMapNote>(null);
					Teleporter teleporter = this.ParentObject.RequirePart<Teleporter>(false);
					teleporter.DestinationZone = randomElement.ZoneID;
					teleporter.DestinationX = -1;
					teleporter.DestinationY = -1;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A86B RID: 43115 RVA: 0x003D9728 File Offset: 0x003D7928
		private static string FindZoneName(string ZoneID)
		{
			string text = null;
			if (The.ZoneManager.GetZoneHasProperName(ZoneID))
			{
				text = The.ZoneManager.GetZoneBaseDisplayName(ZoneID, true);
			}
			if (text.IsNullOrEmpty())
			{
				text = The.ZoneManager.GetZoneNameContext(ZoneID) ?? The.ZoneManager.GetZoneDisplayName(ZoneID, false, false, true, true);
			}
			if (text.StartsWith("the ") || text.StartsWith("The "))
			{
				text = text.Substring(4);
			}
			return text;
		}

		// Token: 0x0400458F RID: 17807
		public string additionalDescription;

		// Token: 0x04004590 RID: 17808
		[NonSerialized]
		private int NameCacheTick;

		// Token: 0x04004591 RID: 17809
		[NonSerialized]
		private string NameCache;
	}
}
