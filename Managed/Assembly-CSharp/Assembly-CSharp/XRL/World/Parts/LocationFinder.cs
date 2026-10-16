using System;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001274 RID: 4724
	[Serializable]
	public class LocationFinder : IPart
	{
		// Token: 0x0600BB15 RID: 47893 RVA: 0x00445589 File Offset: 0x00443789
		public LocationFinder()
		{
			this.Trigger = "Created";
		}

		// Token: 0x0600BB16 RID: 47894 RVA: 0x0044559C File Offset: 0x0044379C
		public override void Attach()
		{
			this.WantBeforeRenderEvent = this.Trigger == "Created";
		}

		// Token: 0x0600BB17 RID: 47895 RVA: 0x004455B4 File Offset: 0x004437B4
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BB18 RID: 47896 RVA: 0x004455B8 File Offset: 0x004437B8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.Trigger == "OnScreen")
			{
				Registrar.Register("EndTurn");
			}
			if (this.Trigger == "Taken")
			{
				Registrar.Register("Taken");
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BB19 RID: 47897 RVA: 0x00445608 File Offset: 0x00443808
		public void TriggerFind()
		{
			if (this.Activated)
			{
				return;
			}
			this.Activated = true;
			if (!The.Game.StringGameState.ContainsKey("LairFound_" + this.ID))
			{
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.ID);
				string text;
				if (mapNote.Category == "Ruins")
				{
					text = ((mapNote.Text == "some forgotten ruins") ? Grammar.InitLower(mapNote.Text) : Grammar.MakeTitleCaseWithArticle(mapNote.Text));
				}
				else if (mapNote.Category == "Lairs")
				{
					text = Grammar.InitLower(mapNote.Text);
					Achievement.LAIRS_100.Progress.Increment(1);
					The.Game.StringGameState.Add("LairFound_" + this.ID, "1");
					The.Game.IncrementIntGameState("LairsFound", 1);
				}
				else
				{
					mapNote.Category == "Settlements";
					text = Grammar.InitLowerIfArticle(mapNote.Text);
				}
				if (!mapNote.Revealed)
				{
					Popup.Show("You discover " + text + "!", null, "sfx_newLocation_discovered", true, true, true, true, null);
					JournalAPI.AddAccomplishment("You discovered " + text + ".", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered " + text + ", once thought lost to the sands of time.", string.Concat(new string[]
					{
						"In =year=, =name= won a decisive victory against the combined force of ",
						Factions.GetMostHatedFormattedName(true),
						" from ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						" at the bloody Battle of ",
						text,
						"."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
					if (this.Value > 0)
					{
						GameObject player = The.Player;
						int value = this.Value;
						int num = -1;
						int num2 = 0;
						int maxValue = int.MaxValue;
						GameObject gameObject = null;
						GameObject gameObject2 = null;
						GameObject gameObject3 = null;
						GameObject gameObject4 = null;
						Zone currentZone = this.ParentObject.GetCurrentZone();
						player.AwardXP(value, num, num2, maxValue, gameObject, gameObject2, gameObject3, gameObject4, (currentZone != null) ? currentZone.ZoneID : null, null);
					}
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
				else if (this.Value > 0)
				{
					Popup.Show("You traveled to " + text + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					if (this.Value > 0)
					{
						GameObject player2 = The.Player;
						int value2 = this.Value;
						int num3 = -1;
						int num4 = 0;
						int maxValue2 = int.MaxValue;
						GameObject gameObject5 = null;
						GameObject gameObject6 = null;
						GameObject gameObject7 = null;
						GameObject gameObject8 = null;
						Zone currentZone2 = this.ParentObject.GetCurrentZone();
						player2.AwardXP(value2, num3, num4, maxValue2, gameObject5, gameObject6, gameObject7, gameObject8, (currentZone2 != null) ? currentZone2.ZoneID : null, null);
					}
					JournalAPI.AddAccomplishment("You traveled to " + text + ".", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered " + text + ", once thought lost to the sands of time.", string.Concat(new string[]
					{
						"In =year=, =name= won a decisive victory against the combined force of ",
						Factions.GetMostHatedFormattedName(true),
						" from ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						" at the bloody Battle of ",
						text,
						"."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Low, null, -1L, true);
				}
			}
			if (this.ParentObject.HasTag("Non"))
			{
				this.ParentObject.Obliterate(null, true, null);
				return;
			}
			GameManager.Instance.gameQueue.queueTask(delegate
			{
				this.ParentObject.RemovePart(this);
			}, 0);
		}

		// Token: 0x0600BB1A RID: 47898 RVA: 0x00445930 File Offset: 0x00443B30
		public override bool Render(RenderEvent E)
		{
			if (this.Trigger == "Seen")
			{
				GameObject parentObject = this.ParentObject;
				string text;
				if (parentObject == null)
				{
					text = null;
				}
				else
				{
					Zone currentZone = parentObject.CurrentZone;
					text = ((currentZone != null) ? currentZone.ZoneID : null);
				}
				GameObject player = The.Player;
				string text2;
				if (player == null)
				{
					text2 = null;
				}
				else
				{
					Zone currentZone2 = player.CurrentZone;
					text2 = ((currentZone2 != null) ? currentZone2.ZoneID : null);
				}
				if (text == text2)
				{
					GameObject parentObject2 = this.ParentObject;
					bool flag;
					if (parentObject2 == null)
					{
						flag = null != null;
					}
					else
					{
						Zone currentZone3 = parentObject2.CurrentZone;
						flag = ((currentZone3 != null) ? currentZone3.ZoneID : null) != null;
					}
					if (flag)
					{
						this.TriggerFind();
					}
				}
			}
			return true;
		}

		// Token: 0x0600BB1B RID: 47899 RVA: 0x004459B8 File Offset: 0x00443BB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == BeforeRenderEvent.ID && this.WantBeforeRenderEvent);
		}

		// Token: 0x0600BB1C RID: 47900 RVA: 0x004459D8 File Offset: 0x00443BD8
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.Trigger == "Created")
			{
				GameObject parentObject = this.ParentObject;
				string text;
				if (parentObject == null)
				{
					text = null;
				}
				else
				{
					Zone currentZone = parentObject.CurrentZone;
					text = ((currentZone != null) ? currentZone.ZoneID : null);
				}
				GameObject player = The.Player;
				string text2;
				if (player == null)
				{
					text2 = null;
				}
				else
				{
					Zone currentZone2 = player.CurrentZone;
					text2 = ((currentZone2 != null) ? currentZone2.ZoneID : null);
				}
				if (text == text2)
				{
					GameObject parentObject2 = this.ParentObject;
					bool flag;
					if (parentObject2 == null)
					{
						flag = null != null;
					}
					else
					{
						Zone currentZone3 = parentObject2.CurrentZone;
						flag = ((currentZone3 != null) ? currentZone3.ZoneID : null) != null;
					}
					if (flag)
					{
						this.TriggerFind();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB1D RID: 47901 RVA: 0x00445A68 File Offset: 0x00443C68
		public override bool FireEvent(Event E)
		{
			if (this.Trigger == "Taken" && E.ID == "Taken")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("TakingObject");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					this.TriggerFind();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB1E RID: 47902 RVA: 0x00445ABD File Offset: 0x00443CBD
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.Value);
			Writer.WriteOptimized(this.Trigger);
		}

		// Token: 0x0600BB1F RID: 47903 RVA: 0x00445AE3 File Offset: 0x00443CE3
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.ID = Reader.ReadOptimizedString();
			this.Value = Reader.ReadOptimizedInt32();
			this.Trigger = Reader.ReadOptimizedString();
		}

		// Token: 0x04004B1C RID: 19228
		public string ID;

		// Token: 0x04004B1D RID: 19229
		[Obsolete("Unused, the text of the associated map note is always used.")]
		public string Text;

		// Token: 0x04004B1E RID: 19230
		public int Value;

		// Token: 0x04004B1F RID: 19231
		public string Trigger;

		// Token: 0x04004B20 RID: 19232
		[NonSerialized]
		private bool Activated;

		// Token: 0x04004B21 RID: 19233
		[NonSerialized]
		private bool WantBeforeRenderEvent;
	}
}
