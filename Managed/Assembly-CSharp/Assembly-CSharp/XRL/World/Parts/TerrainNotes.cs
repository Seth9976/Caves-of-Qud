using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Occult.Engine.CodeGeneration;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000DD0 RID: 3536
	[GenerateSerializationPartial]
	[Serializable]
	public class TerrainNotes : IPartWithPrefabImposter
	{
		// Token: 0x06008FFC RID: 36860 RVA: 0x0033EB88 File Offset: 0x0033CD88
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.tracked);
			Writer.Write(this.shown);
			Writer.WriteOptimized(this.prefabID);
			Writer.Write(this.ImposterActive);
			Writer.Write(this.VisibleOnly);
			Writer.WriteOptimized(this.X);
			Writer.WriteOptimized(this.Y);
		}

		// Token: 0x06008FFD RID: 36861 RVA: 0x0033EBEC File Offset: 0x0033CDEC
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.tracked = Reader.ReadBoolean();
			this.shown = Reader.ReadBoolean();
			this.prefabID = Reader.ReadOptimizedString();
			this.ImposterActive = Reader.ReadBoolean();
			this.VisibleOnly = Reader.ReadBoolean();
			this.X = Reader.ReadOptimizedInt32();
			this.Y = Reader.ReadOptimizedInt32();
		}

		// Token: 0x06008FFE RID: 36862 RVA: 0x0033EC4D File Offset: 0x0033CE4D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x06008FFF RID: 36863 RVA: 0x0033EC6C File Offset: 0x0033CE6C
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.descriptionCache = null;
			this.notes = null;
			this.tracked = false;
			this.InitNotes();
			this.shown = false;
			using (List<JournalMapNote>.Enumerator enumerator = this.notes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (JournalAPI.GetCategoryMapNoteToggle(enumerator.Current.Category))
					{
						this.shown = true;
						break;
					}
				}
			}
			IEnumerable<JournalMapNote> enumerable = this.notes.Where<JournalMapNote>(delegate(JournalMapNote n)
			{
				string text2 = ((n != null) ? n.WorldID : null);
				GameObject parentObject = this.ParentObject;
				string text3;
				if (parentObject == null)
				{
					text3 = null;
				}
				else
				{
					Cell currentCell = parentObject.CurrentCell;
					if (currentCell == null)
					{
						text3 = null;
					}
					else
					{
						Zone parentZone = currentCell.ParentZone;
						text3 = ((parentZone != null) ? parentZone.GetZoneWorld() : null);
					}
				}
				return text2 == text3;
			});
			if (enumerable.Count<JournalMapNote>() > 0 && this.shown)
			{
				string text = "b";
				if (enumerable.Any<JournalMapNote>((JournalMapNote item) => item.Tracked))
				{
					this.tracked = true;
				}
				if (enumerable.Any<JournalMapNote>((JournalMapNote item) => item.Category == "lairs"))
				{
					text = "M";
				}
				else if (enumerable.Any<JournalMapNote>((JournalMapNote item) => item.Category == "oddities"))
				{
					text = "G";
				}
				else if (enumerable.Any<JournalMapNote>((JournalMapNote item) => item.Category == "settlements"))
				{
					text = "W";
				}
				this.ParentObject.SetStringProperty("OverlayDetailColor", text, false);
				this.ParentObject.SetStringProperty("OverlayTile", "assets_content_textures_text_42.bmp", false);
				this.ParentObject.SetStringProperty("OverlayRenderString", "*", false);
				this.prefabID = "Prefabs/Imposters/NoteMarker";
			}
			else
			{
				this.ParentObject.DeleteStringProperty("OverlayDetailColor");
				this.ParentObject.DeleteStringProperty("OverlayTile");
				this.ParentObject.DeleteStringProperty("OverlayRenderString");
				this.prefabID = null;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009000 RID: 36864 RVA: 0x0033EE68 File Offset: 0x0033D068
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.descriptionCache == null)
			{
				this.InitNotes();
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				IEnumerable<JournalMapNote> enumerable = this.notes.Where<JournalMapNote>(delegate(JournalMapNote n)
				{
					string text = ((n != null) ? n.WorldID : null);
					GameObject parentObject = this.ParentObject;
					string text2;
					if (parentObject == null)
					{
						text2 = null;
					}
					else
					{
						Cell currentCell = parentObject.CurrentCell;
						if (currentCell == null)
						{
							text2 = null;
						}
						else
						{
							Zone parentZone = currentCell.ParentZone;
							text2 = ((parentZone != null) ? parentZone.GetZoneWorld() : null);
						}
					}
					return text == text2;
				});
				if (enumerable.Count<JournalMapNote>() > 0)
				{
					stringBuilder.AppendLine(" ");
					stringBuilder.AppendLine("&CNotes:&y");
				}
				foreach (JournalMapNote journalMapNote in enumerable)
				{
					stringBuilder.AppendLine(journalMapNote.Text);
				}
				this.descriptionCache = stringBuilder.ToString();
			}
			if (!string.IsNullOrEmpty(this.descriptionCache))
			{
				E.Postfix.Append(this.descriptionCache);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009001 RID: 36865 RVA: 0x0033EF38 File Offset: 0x0033D138
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009002 RID: 36866 RVA: 0x0033EF3B File Offset: 0x0033D13B
		public void InitNotes()
		{
			if (this.notes == null)
			{
				this.notes = JournalAPI.GetRevealedMapNotesForWorldMapCell(this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y);
			}
		}

		// Token: 0x06009003 RID: 36867 RVA: 0x0033EF70 File Offset: 0x0033D170
		public override bool Render(RenderEvent E)
		{
			if (this.tracked)
			{
				if (DateTime.Now.Millisecond % 500 <= 250)
				{
					E.ColorString += "&g^G";
					E.DetailColor = "G";
				}
				else
				{
					E.ColorString += "&g^g";
					E.DetailColor = "g";
				}
			}
			return base.Render(E);
		}

		// Token: 0x04003C13 RID: 15379
		public bool tracked;

		// Token: 0x04003C14 RID: 15380
		public bool shown;

		// Token: 0x04003C15 RID: 15381
		[NonSerialized]
		private string descriptionCache;

		// Token: 0x04003C16 RID: 15382
		[NonSerialized]
		public List<JournalMapNote> notes;
	}
}
