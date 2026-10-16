using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Occult.Engine.CodeGeneration;
using Qud.UI;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000DC0 RID: 3520
	[GenerateSerializationPartial]
	[GeneratePoolingPartial(Capacity = 64)]
	[Serializable]
	public class Description : IPart
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06008D35 RID: 36149 RVA: 0x0032363E File Offset: 0x0032183E
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return Description.DescriptionPool;
			}
		}

		// Token: 0x06008D36 RID: 36150 RVA: 0x00323645 File Offset: 0x00321845
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.Mark = "";
			this._Short = "A really ugly specimen.";
			this.FeatureItems.Clear();
			this.EquipItems.Clear();
		}

		// Token: 0x06008D37 RID: 36151 RVA: 0x00323679 File Offset: 0x00321879
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Mark);
			Writer.WriteOptimized(this._Short);
		}

		// Token: 0x06008D38 RID: 36152 RVA: 0x00323693 File Offset: 0x00321893
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Mark = Reader.ReadOptimizedString();
			this._Short = Reader.ReadOptimizedString();
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06008D39 RID: 36153 RVA: 0x003236AD File Offset: 0x003218AD
		// (set) Token: 0x06008D3A RID: 36154 RVA: 0x003236B8 File Offset: 0x003218B8
		public string Short
		{
			get
			{
				return this.GetShortDescription(false, false, null);
			}
			set
			{
				this._Short = (value.Contains("~") ? value.Split('~', StringSplitOptions.None)[0] : value);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06008D3B RID: 36155 RVA: 0x003236DB File Offset: 0x003218DB
		public string Long
		{
			get
			{
				return this.GetLongDescription();
			}
		}

		// Token: 0x06008D3C RID: 36156 RVA: 0x003236E4 File Offset: 0x003218E4
		public string GetShortDescription(bool AsIfKnown = false, bool NoConfusion = false, string Context = null)
		{
			if (!NoConfusion && The.Player != null && The.Player.IsConfused)
			{
				return "???";
			}
			IShortDescriptionEvent shortDescriptionEvent = null;
			int num = (AsIfKnown ? 2 : this.ParentObject.GetEpistemicStatus());
			IShortDescriptionEvent shortDescriptionEvent2;
			if (num != 0)
			{
				if (num != 1)
				{
					shortDescriptionEvent2 = GetShortDescriptionEvent.FromPool(this.ParentObject, this._Short, Context, AsIfKnown);
					if (IComponent<GameObject>.ThePlayer != null)
					{
						shortDescriptionEvent = OwnerGetShortDescriptionEvent.FromPool();
					}
				}
				else
				{
					GameObject parentObject = this.ParentObject;
					Examiner part = this.ParentObject.GetPart<Examiner>();
					shortDescriptionEvent2 = GetUnknownShortDescriptionEvent.FromPool(parentObject, (part != null) ? part.GetActiveShortDescription(num) : null, Context, AsIfKnown);
					if (IComponent<GameObject>.ThePlayer != null)
					{
						shortDescriptionEvent = OwnerGetUnknownShortDescriptionEvent.FromPool();
					}
				}
			}
			else
			{
				GameObject parentObject2 = this.ParentObject;
				Examiner part2 = this.ParentObject.GetPart<Examiner>();
				shortDescriptionEvent2 = GetUnknownShortDescriptionEvent.FromPool(parentObject2, (part2 != null) ? part2.GetActiveShortDescription(num) : null, Context, AsIfKnown);
				if (IComponent<GameObject>.ThePlayer != null)
				{
					shortDescriptionEvent = OwnerGetUnknownShortDescriptionEvent.FromPool();
				}
			}
			return this.GetDescription(shortDescriptionEvent2, shortDescriptionEvent);
		}

		// Token: 0x06008D3D RID: 36157 RVA: 0x003237C0 File Offset: 0x003219C0
		private string GetDescription(IShortDescriptionEvent E, IShortDescriptionEvent OE)
		{
			E.Process(this.ParentObject, null);
			if (OE != null && IComponent<GameObject>.ThePlayer != null)
			{
				OE.Process(IComponent<GameObject>.ThePlayer, E);
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (E.Prefix.Length > 0)
			{
				stringBuilder.Append(E.Prefix);
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '\n')
				{
					stringBuilder.Append('\n');
				}
			}
			stringBuilder.Append(E.Base);
			if (E.Infix.Length > 0)
			{
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '\n')
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append(E.Infix);
			}
			if (E.Postfix.Length > 0)
			{
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '\n')
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append(E.Postfix);
			}
			if (!this.Mark.IsNullOrEmpty())
			{
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '\n')
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append('\n').Append(this.Mark);
			}
			if (this.ParentObject.Physics != null && this.ParentObject.Physics.Takeable)
			{
				if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '\n')
				{
					stringBuilder.Append('\n');
				}
				stringBuilder.Append("\n{{K|Weight: ").Append(this.ParentObject.Weight).Append(" lbs.}}");
			}
			return GameText.VariableReplace(stringBuilder, this.ParentObject, null, false);
		}

		// Token: 0x06008D3E RID: 36158 RVA: 0x00323980 File Offset: 0x00321B80
		public string GetLongDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			this.GetLongDescription(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06008D3F RID: 36159 RVA: 0x003239A4 File Offset: 0x00321BA4
		public void GetLongDescription(StringBuilder SB)
		{
			SB.Append(this.Short);
			if (!this.ParentObject.HasProperty("HideCon"))
			{
				Body body = this.ParentObject.Body;
				if (body != null)
				{
					this.FeatureItems.Clear();
					this.EquipItems.Clear();
					List<GameObject> list = Event.NewGameObjectList();
					int length = SB.Length;
					foreach (BodyPart bodyPart in body.LoopParts())
					{
						if (bodyPart.Equipped != null && !bodyPart.Equipped.HasPropertyOrTag("SuppressInLookDisplay") && !list.Contains(bodyPart.Equipped))
						{
							if (bodyPart.Equipped.HasPropertyOrTag("ShowAsPhysicalFeature") || (bodyPart.Equipped.HasPropertyOrTag("VisibleAsDefaultBehavior") && !bodyPart.Equipped.HasPropertyOrTag("UndesirableWeapon")))
							{
								this.FeatureItems.Add(bodyPart.Equipped.ShortDisplayName);
							}
							else
							{
								this.EquipItems.Add(bodyPart.Equipped.ShortDisplayName);
							}
							list.Add(bodyPart.Equipped);
						}
						if (bodyPart.Cybernetics != null && !bodyPart.Cybernetics.HasPropertyOrTag("SuppressInLookDisplay") && !list.Contains(bodyPart.Cybernetics))
						{
							this.EquipItems.Add(bodyPart.Cybernetics.ShortDisplayName);
							list.Add(bodyPart.Cybernetics);
						}
						if (bodyPart.DefaultBehavior != null && !bodyPart.DefaultBehavior.HasPropertyOrTag("SuppressInLookDisplay") && !list.Contains(bodyPart.DefaultBehavior) && (bodyPart.DefaultBehaviorBlueprint == null || bodyPart.DefaultBehaviorBlueprint != bodyPart.DefaultBehavior.Blueprint || bodyPart.DefaultBehavior.HasPropertyOrTag("ShowAsPhysicalFeature") || (bodyPart.DefaultBehavior.HasPropertyOrTag("VisibleAsDefaultBehavior") && !bodyPart.DefaultBehavior.HasPropertyOrTag("UndesirableWeapon"))))
						{
							this.FeatureItems.Add(bodyPart.DefaultBehavior.ShortDisplayName);
							list.Add(bodyPart.DefaultBehavior);
						}
					}
					GetExtraPhysicalFeaturesEvent.Send(this.ParentObject, this.FeatureItems);
					if (Gender.EnableDisplay)
					{
						Gender gender = this.ParentObject.GetGender(false);
						string text = ((gender != null) ? gender.Name : null);
						if (!text.IsNullOrEmpty())
						{
							SB.Append("\n\nGender: ").Append(text);
						}
					}
					if (this.FeatureItems.Count > 0)
					{
						SB.Append("\n\nPhysical features: ").Append(this.FeatureItems[0]);
						int i = 1;
						int count = this.FeatureItems.Count;
						while (i < count)
						{
							SB.Append(", ").Append(this.FeatureItems[i]);
							i++;
						}
					}
					if (this.EquipItems.Count > 0)
					{
						if (this.FeatureItems.Count <= 0)
						{
							SB.Append('\n');
						}
						SB.Append("\nEquipped: ").Append(this.EquipItems[0]);
						int j = 1;
						int count2 = this.EquipItems.Count;
						while (j < count2)
						{
							SB.Append(", ").Append(this.EquipItems[j]);
							j++;
						}
					}
				}
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.ParentObject.Effects != null)
			{
				foreach (Effect effect in this.ParentObject.Effects)
				{
					if (!effect.SuppressInLookDisplay())
					{
						string description = effect.GetDescription();
						if (!string.IsNullOrEmpty(description))
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(", ");
							}
							stringBuilder.Append(description);
						}
					}
				}
			}
			this.ParentObject.FireEvent(Event.New("GetEffectsBlock", "Block", stringBuilder));
			if (stringBuilder.Length > 0)
			{
				SB.Append("\n\n").Append(stringBuilder);
			}
		}

		// Token: 0x06008D40 RID: 36160 RVA: 0x00323E04 File Offset: 0x00322004
		public string GetFeelingDescription(GameObject who = null)
		{
			if (who == null)
			{
				who = IComponent<GameObject>.ThePlayer;
				if (who == null)
				{
					return null;
				}
			}
			if (this.ParentObject.HasProperty("HideCon"))
			{
				return null;
			}
			Brain brain = this.ParentObject.Brain;
			if (brain == null)
			{
				return null;
			}
			Brain.FeelingLevel feelingLevel = brain.GetFeelingLevel(who);
			if (feelingLevel == Brain.FeelingLevel.Allied)
			{
				return "{{G|Friendly}}";
			}
			if (feelingLevel == Brain.FeelingLevel.Hostile)
			{
				return "{{R|Hostile}}";
			}
			return "Neutral";
		}

		// Token: 0x06008D41 RID: 36161 RVA: 0x00323E68 File Offset: 0x00322068
		public string GetDifficultyDescription(GameObject who = null)
		{
			return DifficultyEvaluation.GetDifficultyDescription(this.ParentObject, who, null, false);
		}

		// Token: 0x06008D42 RID: 36162 RVA: 0x00323E8C File Offset: 0x0032208C
		public override bool SameAs(IPart p)
		{
			Description description = p as Description;
			return !(description.Mark != this.Mark) && !(description._Short != this._Short) && base.SameAs(p);
		}

		// Token: 0x06008D43 RID: 36163 RVA: 0x00323ED1 File Offset: 0x003220D1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06008D44 RID: 36164 RVA: 0x00323EF0 File Offset: 0x003220F0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (base.Visible())
			{
				E.AddAction("Look", "look", "Look", null, 'l', false, 0, 0, false, true, false, false, true, null, false);
				if (this.ParentObject.Understood() && this.ParentObject.HasStringProperty("Story"))
				{
					E.AddAction("Recall Story", "recall story", "ReadStory", null, 's', false, 0, 0, false, true, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008D45 RID: 36165 RVA: 0x00323F74 File Offset: 0x00322174
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Look")
			{
				Look.TooltipInformation tooltipInformation = Look.GenerateTooltipInformation(this.ParentObject);
				StringBuilder stringBuilder = Event.NewStringBuilder(null).Append(tooltipInformation.LongDescription);
				if (!string.IsNullOrEmpty(tooltipInformation.WoundLevel))
				{
					stringBuilder.Append("\n\n").Append(tooltipInformation.WoundLevel);
				}
				StringBuilder stringBuilder2 = Event.NewStringBuilder(null).Append(tooltipInformation.DisplayName);
				if (!string.IsNullOrEmpty(tooltipInformation.SubHeader))
				{
					stringBuilder2.Append('\n').Append(tooltipInformation.SubHeader);
				}
				List<QudMenuItem> list = new List<QudMenuItem>();
				list.AddRange(PopupMessage.SingleButton);
				if (this.ParentObject.HasStringProperty("Story") && this.ParentObject.Understood())
				{
					list.Add(new QudMenuItem
					{
						command = "Story",
						hotkey = "S",
						text = "Recall {{W|S}}tory"
					});
				}
				if (Options.ModernUI)
				{
					if (Popup.NewPopupMessageAsync(stringBuilder.ToString(), list, null, null, null, 0, stringBuilder2.ToString(), tooltipInformation.IconRenderable, null, true, true, false, default(CancellationToken), false, "", null, null, null).Result.command == "Story")
					{
						BookUI.ShowBookByID(this.ParentObject.Property["Story"], "Sounds/Interact/sfx_interact_book_read", null);
					}
				}
				else
				{
					stringBuilder2.Append("\n\n").Append(stringBuilder);
					if (Popup.ShowBlockPrompt(stringBuilder2.ToString(), (list.Count == 2) ? "[press {{W|space}} or recall {{W|s}}tory]" : "[press {{W|space}}]", "Sounds/UI/ui_notification", this.ParentObject.RenderForUI("Look", false), false, true, false, true, false) == Keys.S && list.Count == 2)
					{
						BookUI.ShowBookByID(this.ParentObject.Property["Story"], "Sounds/Interact/sfx_interact_book_read", null);
					}
				}
				E.Actor.FireEvent(Event.New("LookedAt", "Object", this.ParentObject));
				this.ParentObject.FireEvent(Event.New("AfterLookedAt", "Looker", E.Actor));
			}
			else if (E.Command == "ReadStory")
			{
				BookUI.ShowBookByID(this.ParentObject.GetStringProperty("Story", null), "Sounds/Interact/sfx_interact_book_read", null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003B35 RID: 15157
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool DescriptionPool = new IPartPool(64);

		// Token: 0x04003B36 RID: 15158
		public string Mark = "";

		// Token: 0x04003B37 RID: 15159
		public string _Short = "A really ugly specimen.";

		// Token: 0x04003B38 RID: 15160
		[NonSerialized]
		private List<string> FeatureItems = new List<string>();

		// Token: 0x04003B39 RID: 15161
		[NonSerialized]
		private List<string> EquipItems = new List<string>();
	}
}
