using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using Newtonsoft.Json;
using Occult.Engine.CodeGeneration;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DCE RID: 3534
	[HasWishCommand]
	[HasGameBasedStaticCache]
	[GenerateSerializationPartial]
	[Serializable]
	public class Examiner : IPart
	{
		// Token: 0x06008FA6 RID: 36774 RVA: 0x0033CCD4 File Offset: 0x0033AED4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Complexity);
			Writer.WriteOptimized(this.Difficulty);
			Writer.WriteOptimized(this.Alternate);
			Writer.WriteOptimized(this.Unknown);
			Writer.WriteOptimized(this.EpistemicStatus);
			Writer.WriteOptimized(this.Flags);
		}

		// Token: 0x06008FA7 RID: 36775 RVA: 0x0033CD2C File Offset: 0x0033AF2C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Complexity = Reader.ReadOptimizedInt32();
			this.Difficulty = Reader.ReadOptimizedInt32();
			this.Alternate = Reader.ReadOptimizedString();
			this.Unknown = Reader.ReadOptimizedString();
			this.EpistemicStatus = Reader.ReadOptimizedInt32();
			this.Flags = Reader.ReadOptimizedInt32();
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06008FA8 RID: 36776 RVA: 0x0033CD81 File Offset: 0x0033AF81
		// (set) Token: 0x06008FA9 RID: 36777 RVA: 0x0033CD8E File Offset: 0x0033AF8E
		[JsonIgnore]
		public bool KeepTile
		{
			get
			{
				return (this.Flags & 1) == 1;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1) : (this.Flags & -2));
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06008FAA RID: 36778 RVA: 0x0033CDAC File Offset: 0x0033AFAC
		// (set) Token: 0x06008FAB RID: 36779 RVA: 0x0033CDB9 File Offset: 0x0033AFB9
		[JsonIgnore]
		public bool InjectUnknownColorString
		{
			get
			{
				return (this.Flags & 2) == 2;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2) : (this.Flags & -3));
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06008FAC RID: 36780 RVA: 0x0033CDD7 File Offset: 0x0033AFD7
		// (set) Token: 0x06008FAD RID: 36781 RVA: 0x0033CDE4 File Offset: 0x0033AFE4
		[JsonIgnore]
		public bool InjectUnknownTileColor
		{
			get
			{
				return (this.Flags & 4) == 4;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4) : (this.Flags & -5));
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06008FAE RID: 36782 RVA: 0x0033CE02 File Offset: 0x0033B002
		// (set) Token: 0x06008FAF RID: 36783 RVA: 0x0033CE0F File Offset: 0x0033B00F
		[JsonIgnore]
		public bool InjectUnknownDetailColor
		{
			get
			{
				return (this.Flags & 8) == 8;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8) : (this.Flags & -9));
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06008FB0 RID: 36784 RVA: 0x0033CE2D File Offset: 0x0033B02D
		// (set) Token: 0x06008FB1 RID: 36785 RVA: 0x0033CE3C File Offset: 0x0033B03C
		[JsonIgnore]
		public bool InjectAlternateColorString
		{
			get
			{
				return (this.Flags & 16) == 16;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16) : (this.Flags & -17));
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06008FB2 RID: 36786 RVA: 0x0033CE5B File Offset: 0x0033B05B
		// (set) Token: 0x06008FB3 RID: 36787 RVA: 0x0033CE6A File Offset: 0x0033B06A
		[JsonIgnore]
		public bool InjectAlternateTileColor
		{
			get
			{
				return (this.Flags & 32) == 32;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32) : (this.Flags & -33));
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06008FB4 RID: 36788 RVA: 0x0033CE89 File Offset: 0x0033B089
		// (set) Token: 0x06008FB5 RID: 36789 RVA: 0x0033CE98 File Offset: 0x0033B098
		[JsonIgnore]
		public bool InjectAlternateDetailColor
		{
			get
			{
				return (this.Flags & 64) == 64;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 64) : (this.Flags & -65));
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06008FB6 RID: 36790 RVA: 0x0033CEB7 File Offset: 0x0033B0B7
		[JsonIgnore]
		public bool InjectAnyUnknown
		{
			get
			{
				return (this.Flags & 14) != 0;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06008FB7 RID: 36791 RVA: 0x0033CEC5 File Offset: 0x0033B0C5
		[JsonIgnore]
		public bool InjectAnyAlternate
		{
			get
			{
				return (this.Flags & 112) != 0;
			}
		}

		// Token: 0x06008FB8 RID: 36792 RVA: 0x0033CED3 File Offset: 0x0033B0D3
		public static void LoadGlobals(SerializationReader Reader)
		{
			Examiner.UnderstandingTable = Reader.ReadDictionary<string, int>();
		}

		// Token: 0x06008FB9 RID: 36793 RVA: 0x0033CEE0 File Offset: 0x0033B0E0
		public static void SaveGlobals(SerializationWriter Writer)
		{
			Writer.Write<string, int>(Examiner.UnderstandingTable);
		}

		// Token: 0x06008FBA RID: 36794 RVA: 0x0033CEED File Offset: 0x0033B0ED
		public static void ResetGlobals()
		{
			Examiner.UnderstandingTable = new Dictionary<string, int>();
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06008FBB RID: 36795 RVA: 0x0033CEF9 File Offset: 0x0033B0F9
		// (set) Token: 0x06008FBC RID: 36796 RVA: 0x0033CF0B File Offset: 0x0033B10B
		public int Understanding
		{
			get
			{
				return Examiner.GetUnderstanding(this.ParentObject.GetTinkeringBlueprint());
			}
			set
			{
				Examiner.SetUnderstanding(this.ParentObject.GetTinkeringBlueprint(), value);
			}
		}

		// Token: 0x06008FBD RID: 36797 RVA: 0x0033CF20 File Offset: 0x0033B120
		public static int GetUnderstanding(string Blueprint)
		{
			if (Blueprint == null)
			{
				return 0;
			}
			if (Examiner.UnderstandingTable == null)
			{
				Examiner.ResetGlobals();
			}
			int num;
			if (Examiner.UnderstandingTable.TryGetValue(Blueprint, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06008FBE RID: 36798 RVA: 0x0033CF50 File Offset: 0x0033B150
		public static void SetUnderstanding(string Blueprint, int Level)
		{
			if (Blueprint == null)
			{
				return;
			}
			if (Examiner.UnderstandingTable == null)
			{
				Examiner.ResetGlobals();
			}
			int num;
			if (Examiner.UnderstandingTable.TryGetValue(Blueprint, out num))
			{
				if (num < Level)
				{
					Examiner.UnderstandingTable[Blueprint] = Level;
					return;
				}
			}
			else
			{
				Examiner.UnderstandingTable.Add(Blueprint, Level);
			}
		}

		// Token: 0x06008FBF RID: 36799 RVA: 0x0033CF9C File Offset: 0x0033B19C
		public override bool SameAs(IPart p)
		{
			Examiner examiner = p as Examiner;
			return examiner.Complexity == this.Complexity && examiner.Difficulty == this.Difficulty && !(examiner.Alternate != this.Alternate) && !(examiner.Unknown != this.Unknown) && examiner.Flags == this.Flags && examiner.EpistemicStatus == this.EpistemicStatus && base.SameAs(p);
		}

		// Token: 0x06008FC0 RID: 36800 RVA: 0x0033D024 File Offset: 0x0033B224
		public void CopyFrom(Examiner source)
		{
			this.Complexity = source.Complexity;
			this.Difficulty = source.Difficulty;
			this.Alternate = source.Alternate;
			this.Unknown = source.Unknown;
			this.Flags = source.Flags;
			this.EpistemicStatus = source.EpistemicStatus;
		}

		// Token: 0x06008FC1 RID: 36801 RVA: 0x0033D07C File Offset: 0x0033B27C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == AfterObjectCreatedEvent.ID || ID == PooledEvent<AnimateEvent>.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetGenderEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetInventoryCategoryEvent>.ID || ID == ObjectCreatedEvent.ID || ID == InventoryActionEvent.ID || ID == ZoneActivatedEvent.ID || (this.Complexity > 0 && Options.AutogetArtifacts && ID == AutoexploreObjectEvent.ID);
		}

		// Token: 0x06008FC2 RID: 36802 RVA: 0x0033D125 File Offset: 0x0033B325
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (this.Complexity > 0 && E.Command == null && Options.AutogetArtifacts && this.ParentObject.CanAutoget(true))
			{
				E.Command = "Autoget";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC3 RID: 36803 RVA: 0x0033D160 File Offset: 0x0033B360
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Complexity", this.Complexity);
			E.AddEntry(this, "Difficulty", this.Difficulty);
			E.AddEntry(this, "Alternate", this.Alternate);
			E.AddEntry(this, "Unknown", this.Unknown);
			E.AddEntry(this, "EpistemicStatus", this.EpistemicStatus);
			E.AddEntry(this, "Flags", this.Flags);
			E.AddEntry(this, "KeepTile", this.KeepTile);
			E.AddEntry(this, "InjectUnknownColorString", this.InjectUnknownColorString);
			E.AddEntry(this, "InjectUnknownTileColor", this.InjectUnknownTileColor);
			E.AddEntry(this, "InjectUnknownDetailColor", this.InjectUnknownDetailColor);
			E.AddEntry(this, "InjectAlternateColorString", this.InjectAlternateColorString);
			E.AddEntry(this, "InjectAlternateTileColor", this.InjectAlternateTileColor);
			E.AddEntry(this, "InjectAlternateDetailColor", this.InjectAlternateDetailColor);
			E.AddEntry(this, "UnknownSample", this.GetUnknownSample());
			E.AddEntry(this, "AlternateSample", this.GetAlternateSample());
			E.AddEntry(this, "ActiveSample", this.GetActiveSample());
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC4 RID: 36804 RVA: 0x0033D294 File Offset: 0x0033B494
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.AsIfKnown)
			{
				GameObject activeSample = this.GetActiveSample();
				if (activeSample != null)
				{
					activeSample.ReplaceDisplayName(E);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC5 RID: 36805 RVA: 0x0033D2B7 File Offset: 0x0033B4B7
		public override bool HandleEvent(AnimateEvent E)
		{
			if (E.Object == this.ParentObject && this.GetEpistemicStatus() != 2)
			{
				this.SetEpistemicStatus(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC6 RID: 36806 RVA: 0x0033D2E0 File Offset: 0x0033B4E0
		public override bool HandleEvent(AdjustValueEvent E)
		{
			if (this.GetEpistemicStatus() != 2)
			{
				GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject.InInventory;
				if (gameObject != null && gameObject.IsPlayer())
				{
					E.AdjustValue((this.GetEpistemicStatus() == 1) ? 0.2 : 0.1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC7 RID: 36807 RVA: 0x0033D348 File Offset: 0x0033B548
		public override bool HandleEvent(GetGenderEvent E)
		{
			if (!E.AsIfKnown)
			{
				GameObject activeSample = this.GetActiveSample();
				if (activeSample != null)
				{
					Gender gender = activeSample.GetGender(false);
					if (gender != null)
					{
						E.Name = gender.Name;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC8 RID: 36808 RVA: 0x0033D388 File Offset: 0x0033B588
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.ParentObject.Understood(this))
			{
				E.AddAction("Examine", "examine", "Examine", null, 'x', false, 0, 0, false, false, false, false, true, null, false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FC9 RID: 36809 RVA: 0x0033D3D0 File Offset: 0x0033B5D0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Examine")
			{
				if (base.IsBroken())
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"Whatever ",
						this.ParentObject.it,
						this.ParentObject.Is,
						", ",
						this.ParentObject.itis,
						" broken..."
					}), true, true, true);
					return false;
				}
				if (!E.Actor.CanMoveExtremities("Examine", true, false, false))
				{
					return false;
				}
				bool flag = false;
				int num = E.Actor.Stat("Intelligence", 0) - this.Difficulty;
				int totalConfusion = E.Actor.GetTotalConfusion();
				if (totalConfusion > 0)
				{
					num -= totalConfusion;
				}
				int num2 = E.Actor.GetIntProperty("InspectorEquipped", 0);
				num2 = GetTinkeringBonusEvent.GetFor(E.Actor, this.ParentObject, "Inspect", num, num2, ref flag, true, false);
				if (flag)
				{
					return false;
				}
				num += num2;
				if (E.Actor.IsPlayer())
				{
					if (!string.IsNullOrEmpty(this.ParentObject.Owner) && !this.ParentObject.HasPropertyOrTag("DontWarnOnExamine") && Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						this.ParentObject.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" not owned by you, and examining ",
						this.ParentObject.them,
						" risks damaging ",
						this.ParentObject.them,
						". Are you sure you want to do so?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
					GameObject inInventory = this.ParentObject.InInventory;
					if (inInventory != null && inInventory != E.Actor && !string.IsNullOrEmpty(inInventory.Owner) && inInventory.Owner != this.ParentObject.Owner && !inInventory.HasPropertyOrTag("DontWarnOnExamine") && Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						inInventory.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" not owned by you, and examining ",
						this.ParentObject.an(2147483647, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
						" inside ",
						inInventory.them,
						" risks causing damage. Are you sure you want to do so?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
				}
				if (Options.SifrahExamine && E.Actor.IsPlayer())
				{
					if (totalConfusion > 0)
					{
						Popup.ShowFail("You're too confused to do that.", true, true, true);
					}
					else
					{
						ExamineSifrah examineSifrah = new ExamineSifrah(this.ParentObject, this.Complexity, this.Difficulty, this.Understanding, num);
						examineSifrah.Play(this.ParentObject);
						if (examineSifrah.InterfaceExitRequested)
						{
							E.RequestInterfaceExit();
						}
					}
				}
				else
				{
					int num3 = Stat.RollResult(num);
					num3 = TutorialManager.AdjustExaminationRoll(num3);
					if (num3 >= 10 || num3 > this.Complexity)
					{
						num3 = this.Complexity;
					}
					if (num3 <= 0 && !this.ParentObject.HasPropertyOrTag("CantBreakOnExamine"))
					{
						this.ResultCriticalFailure(E.Actor);
						E.RequestInterfaceExit();
						if (E.Actor.IsPlayer())
						{
							AutoAct.Interrupt(null, null, null, false);
						}
					}
					else if (totalConfusion > 0)
					{
						this.ResultFakeConfusionFailure(E.Actor);
						E.RequestInterfaceExit();
						if (E.Actor.IsPlayer())
						{
							AutoAct.Interrupt(null, null, null, false);
						}
					}
					else if (num3 > 0 && num3 > this.Understanding)
					{
						this.ResultPartialSuccess(E.Actor, num3);
					}
					else
					{
						this.ResultFailure(E.Actor);
					}
				}
				E.Actor.UseEnergy(1000, "Examine", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FCA RID: 36810 RVA: 0x0033D7DC File Offset: 0x0033B9DC
		public override bool HandleEvent(GetInventoryCategoryEvent E)
		{
			if (!this.ParentObject.Understood(this) && !E.AsIfKnown)
			{
				E.Category = "Artifacts";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FCB RID: 36811 RVA: 0x0033D806 File Offset: 0x0033BA06
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || this.ParentObject.Understood(this)) && base.HandleEvent(E);
		}

		// Token: 0x06008FCC RID: 36812 RVA: 0x0033D82C File Offset: 0x0033BA2C
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			if (E.Actor.IsPlayer() && this.CheckEpistemicStatus() != 2)
			{
				this.ParentObject.Twiddle(false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FCD RID: 36813 RVA: 0x0033D85A File Offset: 0x0033BA5A
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckEpistemicStatus();
			return base.HandleEvent(E);
		}

		// Token: 0x06008FCE RID: 36814 RVA: 0x0033D86C File Offset: 0x0033BA6C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.InjectAnyUnknown)
			{
				GameObject unknownSample = this.GetUnknownSample();
				if (unknownSample != null)
				{
					if (this.InjectUnknownColorString)
					{
						this.ParentObject.Render.ColorString = unknownSample.Render.ColorString;
					}
					if (this.InjectUnknownTileColor)
					{
						this.ParentObject.Render.TileColor = unknownSample.Render.TileColor;
					}
					if (this.InjectUnknownDetailColor)
					{
						this.ParentObject.Render.DetailColor = unknownSample.Render.DetailColor;
					}
				}
			}
			if (this.InjectAnyAlternate)
			{
				GameObject alternateSample = this.GetAlternateSample();
				if (alternateSample != null)
				{
					if (this.InjectAlternateColorString)
					{
						this.ParentObject.Render.ColorString = alternateSample.Render.ColorString;
					}
					if (this.InjectAlternateTileColor)
					{
						this.ParentObject.Render.TileColor = alternateSample.Render.TileColor;
					}
					if (this.InjectAlternateDetailColor)
					{
						this.ParentObject.Render.DetailColor = alternateSample.Render.DetailColor;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008FCF RID: 36815 RVA: 0x0033D976 File Offset: 0x0033BB76
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.CheckEpistemicStatus();
			return base.HandleEvent(E);
		}

		// Token: 0x06008FD0 RID: 36816 RVA: 0x0033D986 File Offset: 0x0033BB86
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckEpistemicStatus();
			return base.HandleEvent(E);
		}

		// Token: 0x06008FD1 RID: 36817 RVA: 0x0033D996 File Offset: 0x0033BB96
		public static int GetBlueprintComplexity(GameObjectBlueprint Blueprint)
		{
			if (Blueprint == null)
			{
				return 0;
			}
			return Blueprint.GetPartParameter<int>("Examiner", "Complexity", 0);
		}

		// Token: 0x06008FD2 RID: 36818 RVA: 0x0033D9AE File Offset: 0x0033BBAE
		public static int GetBlueprintComplexity(string Blueprint)
		{
			return Examiner.GetBlueprintComplexity(GameObjectFactory.Factory.GetBlueprintIfExists(Blueprint));
		}

		// Token: 0x06008FD3 RID: 36819 RVA: 0x0033D9C0 File Offset: 0x0033BBC0
		public static int GetBlueprintEpistemicStatus(GameObjectBlueprint Blueprint)
		{
			if (Blueprint == null)
			{
				return 0;
			}
			int understanding = Examiner.GetUnderstanding(Blueprint.Name);
			int blueprintComplexity = Examiner.GetBlueprintComplexity(Blueprint);
			if (understanding >= blueprintComplexity)
			{
				return 2;
			}
			if (understanding > 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06008FD4 RID: 36820 RVA: 0x0033D9F2 File Offset: 0x0033BBF2
		public static int GetBlueprintEpistemicStatus(string Blueprint)
		{
			return Examiner.GetBlueprintEpistemicStatus(GameObjectFactory.Factory.GetBlueprintIfExists(Blueprint));
		}

		// Token: 0x06008FD5 RID: 36821 RVA: 0x0033DA04 File Offset: 0x0033BC04
		public int GetBlueprintEpistemicStatus()
		{
			return Examiner.GetBlueprintEpistemicStatus(this.ParentObject.GetTinkeringBlueprint());
		}

		// Token: 0x06008FD6 RID: 36822 RVA: 0x0033DA18 File Offset: 0x0033BC18
		public static int GetAppropriateEpistemicStatus(int Understanding, int Complexity, GameObject Object = null)
		{
			if (Understanding >= Complexity)
			{
				return 2;
			}
			if (GameObject.Validate(ref Object))
			{
				int scanEpistemicStatus = Scanning.GetScanEpistemicStatus(The.Player, Object);
				if (scanEpistemicStatus == 2 || scanEpistemicStatus == 1)
				{
					return scanEpistemicStatus;
				}
			}
			if (Understanding > 0)
			{
				return 1;
			}
			if (GameObject.Validate(ref Object) && Object.HasProperty("PartiallyUnderstood"))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06008FD7 RID: 36823 RVA: 0x0033DA69 File Offset: 0x0033BC69
		public int GetAppropriateEpistemicStatus()
		{
			return Examiner.GetAppropriateEpistemicStatus(this.Understanding, this.Complexity, this.ParentObject);
		}

		// Token: 0x06008FD8 RID: 36824 RVA: 0x0033DA82 File Offset: 0x0033BC82
		public override int GetEpistemicStatus()
		{
			if (this.EpistemicStatus == -1)
			{
				this.SetEpistemicStatus(this.GetAppropriateEpistemicStatus());
			}
			return this.EpistemicStatus;
		}

		// Token: 0x06008FD9 RID: 36825 RVA: 0x0033DAA0 File Offset: 0x0033BCA0
		public void SetEpistemicStatus(int status)
		{
			this.EpistemicStatus = status;
			if (status == 2 || this.KeepTile)
			{
				this.ParentObject.RemoveEffect<EpistemicDisguise>();
				return;
			}
			GameObject activeSample = this.GetActiveSample(status);
			if (activeSample == null)
			{
				this.ParentObject.RemoveEffect<EpistemicDisguise>();
				return;
			}
			EpistemicDisguise.ApplyTo(this.ParentObject, activeSample);
		}

		// Token: 0x06008FDA RID: 36826 RVA: 0x0033DAF2 File Offset: 0x0033BCF2
		public int CheckEpistemicStatus()
		{
			if (this.EpistemicStatus != 2)
			{
				this.SetEpistemicStatus(this.GetAppropriateEpistemicStatus());
			}
			return this.EpistemicStatus;
		}

		// Token: 0x06008FDB RID: 36827 RVA: 0x0033DB0F File Offset: 0x0033BD0F
		public static bool MakeBlueprintUnderstood(string Blueprint, int Understanding)
		{
			if (Examiner.GetUnderstanding(Blueprint) > Understanding)
			{
				return false;
			}
			The.Game.BlueprintSeen(Blueprint);
			Examiner.SetUnderstanding(Blueprint, Understanding);
			return true;
		}

		// Token: 0x06008FDC RID: 36828 RVA: 0x0033DB30 File Offset: 0x0033BD30
		public bool MakeUnderstood(bool ShowMessage = false)
		{
			if (this.Understanding >= this.Complexity)
			{
				return false;
			}
			string text = (ShowMessage ? this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) : null);
			this.ParentObject.Seen();
			this.Understanding = this.Complexity;
			this.CheckEpistemicStatus();
			if (ShowMessage)
			{
				Popup.Show(this.GetIdentifyMessage(text), null, "sfx_artifact_examination_success_total", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x06008FDD RID: 36829 RVA: 0x0033DBB8 File Offset: 0x0033BDB8
		public bool MakeUnderstood(out string Message)
		{
			Message = null;
			if (this.Understanding >= this.Complexity)
			{
				return false;
			}
			string text = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
			this.ParentObject.Seen();
			this.Understanding = this.Complexity;
			this.CheckEpistemicStatus();
			Message = this.GetIdentifyMessage(text);
			return true;
		}

		// Token: 0x06008FDE RID: 36830 RVA: 0x0033DC2C File Offset: 0x0033BE2C
		public bool MakePartiallyUnderstood(bool ShowMessage = false)
		{
			if (this.Understanding >= this.Complexity)
			{
				return false;
			}
			string text = (ShowMessage ? this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) : null);
			this.ParentObject.Seen();
			this.Understanding = this.Complexity - 1;
			this.CheckEpistemicStatus();
			if (ShowMessage)
			{
				Popup.Show(this.GetIdentifyMessage(text), null, "sfx_artifact_examination_success_partial", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x06008FDF RID: 36831 RVA: 0x0033DCB8 File Offset: 0x0033BEB8
		public bool MakePartiallyUnderstood(out string Message)
		{
			Message = null;
			if (this.Understanding >= this.Complexity)
			{
				return false;
			}
			string text = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
			this.ParentObject.Seen();
			this.Understanding = this.Complexity - 1;
			this.CheckEpistemicStatus();
			Message = this.GetIdentifyMessage(text);
			return true;
		}

		// Token: 0x06008FE0 RID: 36832 RVA: 0x0033DD30 File Offset: 0x0033BF30
		public string GetIdentifyMessage(string PriorDesc)
		{
			string text = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
			if (text == PriorDesc)
			{
				return "You commit the distinguishing characteristics of " + text + " to memory.";
			}
			return string.Concat(new string[]
			{
				"You identify ",
				PriorDesc,
				" as ",
				this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
				"."
			});
		}

		// Token: 0x06008FE1 RID: 36833 RVA: 0x0033DDD8 File Offset: 0x0033BFD8
		public void ResultSuccess(GameObject Actor)
		{
			this.Understanding = this.Complexity;
			if (Actor.IsPlayer())
			{
				Popup.Show("You now understand " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, false, false, null, false, true, false, null, true, null, false) + ".", null, "sfx_artifact_examination_success_total", true, true, true, true, null);
			}
			ExamineSuccessEvent.Send(this.ParentObject, Actor, true);
			this.CheckEpistemicStatus();
		}

		// Token: 0x06008FE2 RID: 36834 RVA: 0x0033DE58 File Offset: 0x0033C058
		public void ResultExceptionalSuccess(GameObject Actor)
		{
			string text = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
			bool flag = false;
			if (20.in100())
			{
				if (RelicGenerator.ApplyBasicBestowal(this.ParentObject, null, 1, null, false, true))
				{
					flag = true;
				}
				else if (ModificationFactory.ApplyModifications(this.ParentObject, this.ParentObject.GetBlueprint(true), -999, 1, "Examination") > 0)
				{
					flag = true;
				}
			}
			else if (ModificationFactory.ApplyModifications(this.ParentObject, this.ParentObject.GetBlueprint(true), -999, 1, "Examination") > 0)
			{
				flag = true;
			}
			else if (50.in100() && RelicGenerator.ApplyBasicBestowal(this.ParentObject, null, 1, null, false, true))
			{
				flag = true;
			}
			if (flag && Actor.IsPlayer())
			{
				Popup.Show("You discover something about " + text + " that was hidden!", null, "sfx_artifact_examination_success_total", true, true, true, true, null);
			}
			this.ResultSuccess(this.ParentObject);
			if (flag && Actor.IsPlayer())
			{
				InventoryActionEvent.Check(this.ParentObject, Actor, this.ParentObject, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
			}
		}

		// Token: 0x06008FE3 RID: 36835 RVA: 0x0033DF88 File Offset: 0x0033C188
		public void ResultPartialSuccess(GameObject Actor, int newLevel = -1)
		{
			if (newLevel == -1)
			{
				newLevel = Math.Min(Stat.Random(1, this.Complexity - 1), this.Complexity - 1);
			}
			int blueprintComplexity = Examiner.GetBlueprintComplexity(this.ParentObject.GetBlueprint(true));
			bool flag = false;
			string text = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
			string text2 = this.ParentObject.Does("seem", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false);
			if (newLevel < this.Complexity && newLevel >= blueprintComplexity && this.Understanding < blueprintComplexity)
			{
				GameObject gameObject = GameObject.CreateSample(this.ParentObject.GetTinkeringBlueprint());
				Examiner examiner;
				if (gameObject.TryGetPart<Examiner>(out examiner) && examiner.Complexity > 0 && newLevel >= examiner.Complexity && !gameObject.DisplayNameOnlyDirect.StartsWith("[") && !gameObject.HasTag("BaseObject"))
				{
					flag = true;
					if (newLevel > this.Understanding)
					{
						this.Understanding = newLevel;
					}
					if (Actor.IsPlayer())
					{
						string text3 = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
						string displayName = gameObject.GetDisplayName(int.MaxValue, null, null, true, false, false, false, false, false, true, false, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
						string text4 = gameObject.GetxTag("Grammar", "adjunctNoun", null);
						if (text4.IsNullOrEmpty())
						{
							text4 = (gameObject.IsPlural ? "set" : "one");
						}
						string text5;
						if (text3 != text)
						{
							text5 = string.Concat(new string[]
							{
								"You make some progress understanding ",
								text,
								". ",
								text2,
								" to be ",
								this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
								", and you think ",
								this.ParentObject.itis,
								" probably a variety of ",
								displayName,
								"; you believe you would be able to recognize an ordinary ",
								text4,
								" of ",
								(gameObject.a == "some ") ? "that" : "those",
								" now."
							});
						}
						else
						{
							text5 = string.Concat(new string[]
							{
								"You make some progress understanding ",
								text,
								". You think ",
								this.ParentObject.itis,
								" probably a variety of ",
								displayName,
								", and you believe you would be able to recognize an ordinary ",
								text4,
								" of ",
								(gameObject.a == "some ") ? "that" : "those",
								" now."
							});
						}
						Popup.Show(text5, null, "sfx_artifact_examination_success_partial", true, true, true, true, null);
					}
				}
			}
			if (!flag)
			{
				if (newLevel > this.Understanding)
				{
					this.Understanding = newLevel;
				}
				if (Actor.IsPlayer())
				{
					string text6 = this.ParentObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
					string text7;
					if (this.Understanding >= this.Complexity && text != text6)
					{
						text7 = this.GetIdentifyMessage(text);
					}
					else if (text != text6)
					{
						text7 = string.Concat(new string[]
						{
							"You make some progress understanding ",
							text,
							". ",
							text2,
							" to be ",
							this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
							"."
						});
					}
					else
					{
						text7 = "You make some progress understanding " + text6 + ".";
					}
					Popup.Show(text7, null, (this.Understanding >= this.Complexity) ? "sfx_artifact_examination_success_total" : "sfx_artifact_examination_success_partial", true, true, true, true, null);
				}
			}
			if (this.Understanding >= this.Complexity)
			{
				ExamineSuccessEvent.Send(this.ParentObject, Actor, true);
				this.CheckEpistemicStatus();
				return;
			}
			ExamineSuccessEvent.Send(this.ParentObject, Actor, false);
		}

		// Token: 0x06008FE4 RID: 36836 RVA: 0x0033E404 File Offset: 0x0033C604
		public void ResultFailure(GameObject Actor)
		{
			if (ExamineFailureEvent.Check(Actor, this.ParentObject, false))
			{
				if (Actor.IsPlayer())
				{
					Popup.Show("You are puzzled by " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, false, false, null, false, true, false, null, true, null, false) + ".", null, "sfx_artifact_examination_noProgress", true, true, true, true, null);
				}
				AfterExamineFailureEvent.Send(Actor, this.ParentObject, false);
			}
		}

		// Token: 0x06008FE5 RID: 36837 RVA: 0x0033E480 File Offset: 0x0033C680
		public void ResultFakeConfusionFailure(GameObject Actor)
		{
			if (ExamineFailureEvent.Check(Actor, this.ParentObject, true))
			{
				if (Actor.IsPlayer())
				{
					Popup.Show("You think you broke " + this.ParentObject.them + "...", null, "sfx_artifact_examination_fail", true, true, true, true, null);
				}
				AfterExamineFailureEvent.Send(Actor, this.ParentObject, true);
			}
		}

		// Token: 0x06008FE6 RID: 36838 RVA: 0x0033E4DC File Offset: 0x0033C6DC
		public void ResultCriticalFailure(GameObject Actor)
		{
			if (ExamineCriticalFailureEvent.Check(Actor, this.ParentObject))
			{
				string text = "You think you broke " + this.ParentObject.them + "...";
				if (this.ParentObject.ApplyEffect(new Broken(false, true, false, false), null) && this.ParentObject.IsBroken())
				{
					if (Actor.IsPlayer())
					{
						Popup.Show(text, null, "sfx_artifact_examination_fail", true, true, true, true, null);
					}
					if (this.ParentObject.IsValid())
					{
						this.ParentObject.PotentiallyAngerOwner(Actor, "DontWarnOnExamine");
					}
				}
				else if (Actor.IsPlayer())
				{
					Popup.Show("You are puzzled by " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, false, false, null, false, true, false, null, true, null, false) + ".", null, "sfx_artifact_examination_fail", true, true, true, true, null);
				}
				AfterExamineCriticalFailureEvent.Send(Actor, this.ParentObject);
			}
		}

		// Token: 0x06008FE7 RID: 36839 RVA: 0x0033E5D0 File Offset: 0x0033C7D0
		[WishCommand(null, null)]
		public static void IDAllHere()
		{
			foreach (GameObject gameObject in GetContentsEvent.GetFor(IComponent<GameObject>.ThePlayer.CurrentZone, null))
			{
				gameObject.MakeUnderstood(false);
			}
		}

		// Token: 0x06008FE8 RID: 36840 RVA: 0x0033E628 File Offset: 0x0033C828
		[WishCommand(null, null)]
		public static void IDAllHerePartial()
		{
			foreach (GameObject gameObject in GetContentsEvent.GetFor(IComponent<GameObject>.ThePlayer.CurrentZone, null))
			{
				gameObject.MakePartiallyUnderstood(false);
			}
		}

		// Token: 0x06008FE9 RID: 36841 RVA: 0x0033E680 File Offset: 0x0033C880
		[WishCommand(null, null)]
		public static void IDAll()
		{
			foreach (GameObject gameObject in GetContentsEvent.GetFor(IComponent<GameObject>.ThePlayer.CurrentZone, null))
			{
				gameObject.MakeUnderstood(false);
			}
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (gameObjectBlueprint.HasPart("Examiner") && gameObjectBlueprint.GetPartParameter<string>("TinkerItem", "SubstituteBlueprint", null) == null)
				{
					Examiner.UnderstandingTable[gameObjectBlueprint.Name] = gameObjectBlueprint.GetPartParameter<int>("Examiner", "Complexity", 0) + 10;
				}
			}
		}

		// Token: 0x06008FEA RID: 36842 RVA: 0x0033E75C File Offset: 0x0033C95C
		[WishCommand(null, null)]
		public static void IDAllPartial()
		{
			foreach (GameObject gameObject in GetContentsEvent.GetFor(IComponent<GameObject>.ThePlayer.CurrentZone, null))
			{
				gameObject.MakePartiallyUnderstood(false);
			}
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (gameObjectBlueprint.HasPart("Examiner") && gameObjectBlueprint.GetPartParameter<string>("TinkerItem", "SubstituteBlueprint", null) == null)
				{
					Examiner.UnderstandingTable[gameObjectBlueprint.Name] = gameObjectBlueprint.GetPartParameter<int>("Examiner", "Complexity", 0) - 1;
				}
			}
		}

		// Token: 0x06008FEB RID: 36843 RVA: 0x0033E834 File Offset: 0x0033CA34
		private static GameObject GetSample(string SampleBlueprint)
		{
			if (SampleBlueprint == null)
			{
				return null;
			}
			if (Examiner.Samples == null)
			{
				Examiner.Samples = new Dictionary<string, GameObject>();
			}
			GameObject gameObject;
			if (Examiner.Samples.TryGetValue(SampleBlueprint, out gameObject))
			{
				return gameObject;
			}
			GameObject gameObject2 = GameObject.CreateSample(SampleBlueprint);
			Examiner.Samples[SampleBlueprint] = gameObject2;
			return gameObject2;
		}

		// Token: 0x06008FEC RID: 36844 RVA: 0x0033E87C File Offset: 0x0033CA7C
		private GameObject GetFinalSample(string SampleBlueprint)
		{
			if (SampleBlueprint == null)
			{
				return null;
			}
			if (Examiner.RandomSamples == null)
			{
				Examiner.RandomSamples = new Dictionary<string, GameObject>();
			}
			GameObject gameObject;
			if (Examiner.RandomSamples.TryGetValue(this.ParentObject.Blueprint, out gameObject))
			{
				return gameObject;
			}
			GameObject gameObject2 = Examiner.GetSample(SampleBlueprint);
			if (gameObject2 != null)
			{
				string tag = gameObject2.GetTag("ExaminerRandom", null);
				if (!tag.IsNullOrEmpty() && !this.ParentObject.GetBlueprint(true).IsBaseBlueprint())
				{
					string text = "ExaminerRandomSelectionFor" + this.ParentObject.Blueprint;
					XRLGame game = The.Game;
					string text2 = ((game != null) ? game.GetStringGameState(text, "") : null);
					if (!text2.IsNullOrEmpty())
					{
						gameObject2 = Examiner.GetSample(text2);
						Examiner.RandomSamples[this.ParentObject.Blueprint] = gameObject2;
					}
					else
					{
						List<string> list = new List<string>();
						foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
						{
							if (gameObjectBlueprint.HasTag(tag, false))
							{
								string usageKey = Examiner.GetUsageKey(gameObjectBlueprint.Name, SampleBlueprint);
								XRLGame game2 = The.Game;
								if (game2 == null || !game2.GetBooleanGameState(usageKey, false))
								{
									list.Add(gameObjectBlueprint.Name);
								}
							}
						}
						string randomElement = list.GetRandomElement<string>(null);
						if (randomElement == null)
						{
							MetricsManager.LogError(string.Concat(new string[]
							{
								"unable to find an unused blueprint tagged ",
								tag,
								" for ",
								this.ParentObject.Blueprint,
								", using ",
								SampleBlueprint
							}));
						}
						else
						{
							XRLGame game3 = The.Game;
							if (game3 != null)
							{
								game3.SetStringGameState(text, randomElement);
							}
							string usageKey2 = Examiner.GetUsageKey(randomElement, SampleBlueprint);
							XRLGame game4 = The.Game;
							if (game4 != null)
							{
								game4.SetBooleanGameState(usageKey2, true);
							}
							gameObject2 = Examiner.GetSample(randomElement);
							Examiner.RandomSamples[this.ParentObject.Blueprint] = gameObject2;
						}
					}
				}
			}
			return gameObject2;
		}

		// Token: 0x06008FED RID: 36845 RVA: 0x0033EA7C File Offset: 0x0033CC7C
		private static string GetUsageKey(string Blueprint, string SampleBlueprint)
		{
			return "ExaminerRandomUsed" + Blueprint + "For" + SampleBlueprint;
		}

		// Token: 0x06008FEE RID: 36846 RVA: 0x0033EA8F File Offset: 0x0033CC8F
		public GameObject GetUnknownSample()
		{
			return this.GetFinalSample(this.Unknown);
		}

		// Token: 0x06008FEF RID: 36847 RVA: 0x0033EA9D File Offset: 0x0033CC9D
		public GameObject GetAlternateSample()
		{
			return this.GetFinalSample(this.Alternate);
		}

		// Token: 0x06008FF0 RID: 36848 RVA: 0x0033EAAB File Offset: 0x0033CCAB
		public GameObject GetActiveSample(int EpistemicStatus)
		{
			if (EpistemicStatus == 0)
			{
				return this.GetUnknownSample();
			}
			if (EpistemicStatus != 1)
			{
				return null;
			}
			return this.GetAlternateSample();
		}

		// Token: 0x06008FF1 RID: 36849 RVA: 0x0033EAC5 File Offset: 0x0033CCC5
		public GameObject GetActiveSample()
		{
			return this.GetActiveSample(this.CheckEpistemicStatus());
		}

		// Token: 0x06008FF2 RID: 36850 RVA: 0x0033EAD4 File Offset: 0x0033CCD4
		public string GetActiveShortDescription(int EpistemicStatus)
		{
			GameObject activeSample = this.GetActiveSample(EpistemicStatus);
			if (activeSample != null)
			{
				Description part = activeSample.GetPart<Description>();
				if (part != null)
				{
					return part._Short;
				}
			}
			return null;
		}

		// Token: 0x06008FF3 RID: 36851 RVA: 0x0033EAFE File Offset: 0x0033CCFE
		public string GetActiveShortDescription()
		{
			return this.GetActiveShortDescription(this.CheckEpistemicStatus());
		}

		// Token: 0x04003BFC RID: 15356
		public const string OWNER_ANGER_SUPPRESS = "DontWarnOnExamine";

		// Token: 0x04003BFD RID: 15357
		public const int EPISTEMIC_STATUS_UNINITIALIZED = -1;

		// Token: 0x04003BFE RID: 15358
		public const int EPISTEMIC_STATUS_UNKNOWN = 0;

		// Token: 0x04003BFF RID: 15359
		public const int EPISTEMIC_STATUS_PARTIAL = 1;

		// Token: 0x04003C00 RID: 15360
		public const int EPISTEMIC_STATUS_KNOWN = 2;

		// Token: 0x04003C01 RID: 15361
		public const int EXAMINER_KEEP_TILE = 1;

		// Token: 0x04003C02 RID: 15362
		public const int EXAMINER_INJECT_UNKNOWN_COLOR_STRING = 2;

		// Token: 0x04003C03 RID: 15363
		public const int EXAMINER_INJECT_UNKNOWN_TILE_COLOR = 4;

		// Token: 0x04003C04 RID: 15364
		public const int EXAMINER_INJECT_UNKNOWN_DETAIL_COLOR = 8;

		// Token: 0x04003C05 RID: 15365
		public const int EXAMINER_INJECT_ALTERNATE_COLOR_STRING = 16;

		// Token: 0x04003C06 RID: 15366
		public const int EXAMINER_INJECT_ALTERNATE_TILE_COLOR = 32;

		// Token: 0x04003C07 RID: 15367
		public const int EXAMINER_INJECT_ALTERNATE_DETAIL_COLOR = 64;

		// Token: 0x04003C08 RID: 15368
		public const int EXAMINER_INJECT_ANY_UNKNOWN = 14;

		// Token: 0x04003C09 RID: 15369
		public const int EXAMINER_INJECT_ANY_ALTERNATE = 112;

		// Token: 0x04003C0A RID: 15370
		public int Complexity;

		// Token: 0x04003C0B RID: 15371
		public int Difficulty;

		// Token: 0x04003C0C RID: 15372
		public string Alternate = "BaseUnknown";

		// Token: 0x04003C0D RID: 15373
		public string Unknown = "BaseUnknown";

		// Token: 0x04003C0E RID: 15374
		public int EpistemicStatus = -1;

		// Token: 0x04003C0F RID: 15375
		public int Flags;

		// Token: 0x04003C10 RID: 15376
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		private static Dictionary<string, GameObject> Samples;

		// Token: 0x04003C11 RID: 15377
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		private static Dictionary<string, GameObject> RandomSamples;

		// Token: 0x04003C12 RID: 15378
		[GameBasedStaticCache(true, false, CreateInstance = false)]
		[NonSerialized]
		public static Dictionary<string, int> UnderstandingTable;
	}
}
