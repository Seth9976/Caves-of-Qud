using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Genkit;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.Liquids;
using XRL.Messages;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.AI.Pathfinding;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;
using XRL.World.Skills;
using XRL.World.Text;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// <summary>
	/// Base game object
	/// </summary>
	// Token: 0x0200094A RID: 2378
	public sealed class GameObject : IEventSource, IEventHandler
	{
		// Token: 0x06005C33 RID: 23603 RVA: 0x001BD75B File Offset: 0x001BB95B
		public bool ShouldShowInNearbyItemsList()
		{
			if (this.HasTagOrProperty("HideInNearbyItemsList"))
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null || !currentCell.IsSolid())
			{
				return this.IsVisible();
			}
			return this.CanInteractInCellWithSolid(XRL.The.Player);
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x001BD794 File Offset: 0x001BB994
		public void Clear()
		{
			if (this.RegisteredEvents != null)
			{
				this.RegisteredEvents.Dispose();
				this.RegisteredEvents = null;
			}
			this.ClearParts();
			this.ClearEffects();
			this._BaseID = 0;
			this.Flags = 0;
			this.Blueprint = "Object";
			this._BlueprintCache = null;
			this.Stacker = null;
			this.Energy = null;
			this.Render = null;
			this.Physics = null;
			this.Brain = null;
			this.Body = null;
			this.LiquidVolume = null;
			this.Inventory = null;
			this.Abilities = null;
			this.PronounSetName = null;
			this.GenderName = null;
			this.DeepCopyInventoryObjectMap = null;
			this._CachedDisplayNameForSort = null;
			this.CarriedWeightCache = -1;
			this.MaxCarriedWeightCache = -1;
			this.Dying = false;
			this.Live = false;
			this.Statistics.Clear();
			this.FlushTransientCache();
			this.ResetNameCache();
			this.IntProperty.Clear();
			this.Property.Clear();
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x001BD88C File Offset: 0x001BBA8C
		public void ClearParts()
		{
			Dictionary<string, List<IPart>> registeredPartEvents = this.RegisteredPartEvents;
			if (registeredPartEvents != null)
			{
				registeredPartEvents.Clear();
			}
			PartRack partsList = this.PartsList;
			for (int i = partsList.Count - 1; i >= 0; i--)
			{
				IPart part = partsList[i];
				try
				{
					part.ApplyUnregistrar(this, false);
					part.Reset();
					IPartPool pool = part.Pool;
					if (pool != null)
					{
						pool.Return(part);
					}
				}
				catch (Exception ex)
				{
					if (part == null)
					{
						Type type;
						if (i <= 0)
						{
							type = null;
						}
						else
						{
							IPart part2 = partsList[i - 1];
							type = ((part2 != null) ? part2.GetType() : null);
						}
						Type type2 = type;
						Type type3;
						if (i - 1 >= partsList.Count)
						{
							type3 = null;
						}
						else
						{
							IPart part3 = partsList[i + 1];
							type3 = ((part3 != null) ? part3.GetType() : null);
						}
						Type type4 = type3;
						MetricsManager.LogException(string.Format("Null part at index {0} of {1}, before: {2}, after: {3}", new object[] { i, this.DebugName, type2, type4 }), ex, "game_exception");
					}
					else
					{
						MetricsManager.LogException(string.Format("Failed to {0} part of type '{1}' on {2}", (part.Pool == null) ? "clear" : "pool", part.GetType(), this.DebugName), ex, "game_exception");
					}
				}
			}
			partsList.Clear();
			this.PartsCascade = 0;
			if (!this.RegisteredPartEvents.IsNullOrEmpty<KeyValuePair<string, List<IPart>>>())
			{
				MetricsManager.LogError(string.Format("GamePoolError::{0} still has {1} part registrations after unregistering:\n", this.DebugName, this.RegisteredPartEvents.Count) + string.Join(", ", this.RegisteredPartEvents.Keys.Take<string>(5)));
				this.RegisteredPartEvents.Clear();
			}
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x001BDA28 File Offset: 0x001BBC28
		public void ClearEffects()
		{
			Dictionary<string, List<Effect>> registeredEffectEvents = this.RegisteredEffectEvents;
			if (registeredEffectEvents != null)
			{
				registeredEffectEvents.Clear();
			}
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				for (int i = effects.Count - 1; i >= 0; i--)
				{
					effects[i].ApplyUnregistrar(this, false);
					effects[i]._Object = null;
				}
				effects.Clear();
			}
			if (!this.RegisteredEffectEvents.IsNullOrEmpty<KeyValuePair<string, List<Effect>>>())
			{
				MetricsManager.LogError(string.Format("GamePoolError::{0} still has {1} effect registrations after unregistering:\n", this.DebugName, this.RegisteredEffectEvents.Count) + string.Join(", ", this.RegisteredEffectEvents.Keys.Take<string>(5)));
				this.RegisteredEffectEvents.Clear();
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x001BDAE1 File Offset: 0x001BBCE1
		public bool IsCombat
		{
			get
			{
				return (this.Flags & 2) != 0;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06005C38 RID: 23608 RVA: 0x001BDAEE File Offset: 0x001BBCEE
		public bool IsTemporary
		{
			get
			{
				return (this.Flags & 8) != 0;
			}
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x001BDAFC File Offset: 0x001BBCFC
		public string GetFlagsDebugString()
		{
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				if ((this.Flags & 1) != 0)
				{
					textBuilder.Compound("FLAG_PRONOUNS_KNOWN");
				}
				if ((this.Flags & 2) != 0)
				{
					textBuilder.Compound("FLAG_COMBAT");
				}
				if ((this.Flags & 4) != 0)
				{
					textBuilder.Compound("FLAG_GRAVEYARD");
				}
				if ((this.Flags & 8) != 0)
				{
					textBuilder.Compound("FLAG_TEMPORARY");
				}
				if ((this.Flags & 16) != 0)
				{
					textBuilder.Compound("FLAG_POOLED");
				}
				if ((this.Flags & 32) != 0)
				{
					textBuilder.Compound("FLAG_SERIALIZED");
				}
				if ((this.Flags & 64) != 0)
				{
					textBuilder.Compound("FLAG_PLACEHOLDER");
				}
				if ((this.Flags & 128) != 0)
				{
					textBuilder.Compound("FLAG_ALWAYS_PAINT");
				}
				if ((this.Flags & 256) != 0)
				{
					textBuilder.Compound("FLAG_IMPORTANT_OVERLAY_OBJECT");
				}
				text = textBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x001BDC08 File Offset: 0x001BBE08
		public void MakeLive()
		{
			if (this.Live)
			{
				return;
			}
			XRL.The.ActionManager.AddLiveObject(this);
			this.Live = true;
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x001BDC25 File Offset: 0x001BBE25
		public void SetPronounSetKnown(bool Value)
		{
			(ref this.Flags).SetBit(1, Value);
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x001BDC34 File Offset: 0x001BBE34
		public bool IsCombatObject()
		{
			return (this.Flags & 2) != 0;
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x001BDC41 File Offset: 0x001BBE41
		[Obsolete("Use IsCombatObject method or IsCombat property, combat flagged objects always have Brain part.")]
		public bool IsCombatObject(bool NoBrainOnly)
		{
			return (this.Flags & 2) != 0;
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x001BDC4E File Offset: 0x001BBE4E
		public void FlushTransientCache()
		{
			this.TransientCache = 0;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06005C3F RID: 23615 RVA: 0x001BDC57 File Offset: 0x001BBE57
		// (set) Token: 0x06005C40 RID: 23616 RVA: 0x001BDC5F File Offset: 0x001BBE5F
		[Obsolete("use ID, will be removed after Q1 2024")]
		public string id
		{
			get
			{
				return this.ID;
			}
			set
			{
				this.ID = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x001BDC68 File Offset: 0x001BBE68
		[Obsolete("use HasID, will be removed after Q1 2024")]
		public bool hasid
		{
			get
			{
				return this.HasID;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06005C42 RID: 23618 RVA: 0x001BDC70 File Offset: 0x001BBE70
		[Obsolete("Use Render - Will not be removed before Q1 2025")]
		public Render pRender
		{
			get
			{
				return this.Render;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x001BDC78 File Offset: 0x001BBE78
		[Obsolete("Use Render - Will not be removed before Q1 2025")]
		public Render _pRender
		{
			get
			{
				return this.Render;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x001BDC80 File Offset: 0x001BBE80
		[Obsolete("Use Physics - Will not be removed before Q1 2025")]
		public XRL.World.Parts.Physics pPhysics
		{
			get
			{
				return this.Physics;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x001BDC88 File Offset: 0x001BBE88
		[Obsolete("Use Physics - Will not be removed before Q1 2025")]
		public XRL.World.Parts.Physics _pPhysics
		{
			get
			{
				return this.Physics;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06005C46 RID: 23622 RVA: 0x001BDC90 File Offset: 0x001BBE90
		[Obsolete("Use Brain instead - Will not be removed before Q1 2025")]
		public Brain pBrain
		{
			get
			{
				return this.Brain;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x001BDC98 File Offset: 0x001BBE98
		[Obsolete("Use Brain instead - Will not be removed before Q1 2025")]
		public Brain _pBrain
		{
			get
			{
				return this.Brain;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06005C48 RID: 23624 RVA: 0x001BDCA0 File Offset: 0x001BBEA0
		// (set) Token: 0x06005C49 RID: 23625 RVA: 0x001BDCA8 File Offset: 0x001BBEA8
		[Obsolete("Use IntProperty")]
		public Dictionary<string, int> _IntProperty
		{
			get
			{
				return this.IntProperty;
			}
			set
			{
				this.IntProperty = value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06005C4A RID: 23626 RVA: 0x001BDCB1 File Offset: 0x001BBEB1
		// (set) Token: 0x06005C4B RID: 23627 RVA: 0x001BDCB9 File Offset: 0x001BBEB9
		[Obsolete("Use Property")]
		public Dictionary<string, string> _Property
		{
			get
			{
				return this.Property;
			}
			set
			{
				this.Property = value;
			}
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x001BDCC2 File Offset: 0x001BBEC2
		[Obsolete("use IDMatch(), will be removed after Q1 2024")]
		public bool idmatch(string ID)
		{
			return this.IDMatch(ID);
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x001BDCCB File Offset: 0x001BBECB
		[Obsolete("use IDMatch(), will be removed after Q1 2024")]
		public bool idmatch(GameObject Object)
		{
			return this.IDMatch(Object);
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x001BDCD4 File Offset: 0x001BBED4
		[Obsolete("use FindByID(), will be removed after Q1 2024")]
		public static GameObject findById(string ID)
		{
			return GameObject.FindByID(ID);
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x001BDCDC File Offset: 0x001BBEDC
		[Obsolete("Use Reference()")]
		public GameObjectReference takeReference()
		{
			return new GameObjectReference(this);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x001BDCE4 File Offset: 0x001BBEE4
		[Obsolete("use FindByBlueprint(), will be removed after Q1 2024")]
		public static GameObject findByBlueprint(string Name)
		{
			return GameObject.FindByBlueprint(Name);
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x001BDCEC File Offset: 0x001BBEEC
		[Obsolete("use Find(), will be removed after Q1 2024")]
		public static GameObject find(Predicate<GameObject> Filter)
		{
			return GameObject.Find(Filter);
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x001BDCF4 File Offset: 0x001BBEF4
		[Obsolete("use FindAll(), will be removed after Q1 2024")]
		public static List<GameObject> findAll(Predicate<GameObject> Filter)
		{
			return GameObject.FindAll(Filter);
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x001BDCFC File Offset: 0x001BBEFC
		[Obsolete]
		public bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x001BDCFF File Offset: 0x001BBEFF
		[Obsolete("use Create(), will be removed after Q1 2024")]
		public static GameObject create(string Blueprint)
		{
			return GameObject.Create(Blueprint);
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x001BDD07 File Offset: 0x001BBF07
		[Obsolete("use Create(), will be removed after Q1 2024")]
		public static GameObject create(string Blueprint, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> ProvideInventory = null)
		{
			return GameObject.Create(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, ProvideInventory);
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x001BDD1A File Offset: 0x001BBF1A
		[Obsolete("use Create(), will be removed after Q1 2024")]
		public static GameObject create(GameObjectBlueprint Blueprint, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> ProvideInventory = null)
		{
			return GameObject.Create(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, ProvideInventory);
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x001BDD2D File Offset: 0x001BBF2D
		[Obsolete("use CreateSample(), will be removed after Q1 2024")]
		public static GameObject createSample(string Blueprint)
		{
			return GameObject.CreateSample(Blueprint);
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x001BDD35 File Offset: 0x001BBF35
		[Obsolete("use CreateUnmodified(), will be removed after Q1 2024")]
		public static GameObject createUnmodified(string Blueprint)
		{
			return GameObject.CreateUnmodified(Blueprint);
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x001BDD3D File Offset: 0x001BBF3D
		[Obsolete("use CreateUnmodified(), will be removed after Q1 2024")]
		public static GameObject createUnmodified(string Blueprint, string Context = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			return GameObject.CreateUnmodified(Blueprint, Context, BeforeObjectCreated, AfterObjectCreated);
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x001BDD48 File Offset: 0x001BBF48
		[Obsolete("use Validate(), will be removed after Q1 2024")]
		public static bool validate(ref GameObject Object)
		{
			return GameObject.Validate(Object);
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x001BDD51 File Offset: 0x001BBF51
		[Obsolete("use Validate(), will be removed after Q1 2024")]
		public static bool validate(GameObject Object)
		{
			return GameObject.Validate(Object);
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x001BDD5C File Offset: 0x001BBF5C
		[Obsolete("You may want to switch to using the new StatShifter API")]
		public bool ApplyStatShift(string Name, int Amount)
		{
			if (Name.IsNullOrEmpty() || this.Statistics == null)
			{
				return false;
			}
			Statistic statistic;
			if (!this.Statistics.TryGetValue(Name, out statistic))
			{
				return false;
			}
			if (Amount > 0)
			{
				statistic.Bonus += Amount;
			}
			else if (Amount < 0)
			{
				statistic.Penalty += -Amount;
			}
			return true;
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x001BDDB8 File Offset: 0x001BBFB8
		[Obsolete("You may want to switch to using the new StatShifter API")]
		public bool UnapplyStatShift(string Name, int Amount)
		{
			if (Name.IsNullOrEmpty())
			{
				return false;
			}
			Statistic statistic;
			if (!this.Statistics.TryGetValue(Name, out statistic))
			{
				return false;
			}
			if (Amount > 0)
			{
				statistic.Bonus -= Amount;
			}
			else if (Amount < 0)
			{
				statistic.Penalty -= -Amount;
			}
			return true;
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x001BDE09 File Offset: 0x001BC009
		[Obsolete("Use GetActivatedAbilityCooldownRounds instead, will be removed after Q1 2024")]
		public int GetActivatedAbilityCooldownTurns(Guid ID)
		{
			return this.GetActivatedAbilityCooldownRounds(ID);
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x001BDE12 File Offset: 0x001BC012
		[Obsolete("Use GetHighestActivatedAbilityCooldownRounds, will be removed Q1 2024")]
		public int GetHighestActivatedAbilityCooldownTurns()
		{
			return this.GetHighestActivatedAbilityCooldownRounds();
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x001BDE1A File Offset: 0x001BC01A
		[Obsolete("Use FlushTransientCache")]
		public void FlushWantTurnTickCache()
		{
			this.FlushTransientCache();
		}

		/// <summary>
		///     Checks for the "Merchant" tag, property, and GenericInventoryRestocker part.
		/// </summary>
		// Token: 0x06005C61 RID: 23649 RVA: 0x001BDE22 File Offset: 0x001BC022
		public bool IsMerchant()
		{
			return this.HasTagOrProperty("Merchant") || this.HasPart<GenericInventoryRestocker>();
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x001BDE3C File Offset: 0x001BC03C
		public bool RenderTile(ConsoleChar Char)
		{
			if (this._Effects != null)
			{
				for (int i = 0; i < this._Effects.Count; i++)
				{
					if (!this._Effects[i].RenderTile(Char))
					{
						return false;
					}
				}
			}
			for (int j = 0; j < this.PartsList.Count; j++)
			{
				if (!this.PartsList[j].RenderTile(Char))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x001BDEAC File Offset: 0x001BC0AC
		public RenderEvent RenderForUI(string Context = null, bool AsIfKnown = false)
		{
			if (this.Render == null)
			{
				return null;
			}
			if (this._contextRender == null)
			{
				this._contextRender = new RenderEvent();
			}
			this._contextRender.Reset();
			this._contextRender.RenderString = this.Render.RenderString;
			if (!this.Render.TileColor.IsNullOrEmpty() && Options.UseTiles)
			{
				this._contextRender.ColorString = this.Render.TileColor;
			}
			else
			{
				this._contextRender.ColorString = this.Render.ColorString;
			}
			this._contextRender.DetailColor = this.Render.DetailColor;
			this._contextRender.BackgroundString = this.Render.GetBackgroundColor();
			this._contextRender.HighestLayer = this.Render.RenderLayer;
			this._contextRender.Tile = this.Render.Tile;
			this._contextRender.HFlip = this.Render.getHFlip();
			this._contextRender.VFlip = this.Render.getVFlip();
			this._contextRender.WantsToPaint = false;
			this._contextRender.AsIfKnown = AsIfKnown;
			this._contextRender.Visible = true;
			this._contextRender.UI = true;
			this._contextRender.Context = Context;
			this._contextRender.Lit = LightLevel.Light;
			this.ComponentRender(this._contextRender);
			this.FinalRender(this._contextRender);
			if (this._contextRender.BackdropBleedthrough)
			{
				Cell cellContext = this.GetCellContext();
				if (cellContext != null)
				{
					this._contextRender.x = cellContext.X;
					this._contextRender.y = cellContext.Y;
				}
			}
			return this._contextRender;
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x001BE068 File Offset: 0x001BC268
		public void Paint(ScreenBuffer Buffer)
		{
			if (this._Effects != null)
			{
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					this._Effects[i].OnPaint(Buffer);
					i++;
				}
			}
			int j = 0;
			int count2 = this.PartsList.Count;
			while (j < count2)
			{
				this.PartsList[j].OnPaint(Buffer);
				j++;
			}
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x001BE0D1 File Offset: 0x001BC2D1
		[Obsolete]
		public bool RenderSound(ConsoleChar Char, ConsoleChar[,] Buffer)
		{
			return true;
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x001BE0D4 File Offset: 0x001BC2D4
		public bool ComponentRender(RenderEvent E)
		{
			EventRegistry.List list;
			if (this.RegisteredEvents != null && this.RegisteredEvents.TryGetValue(RenderEvent.ID, out list))
			{
				foreach (EventRegistry.List.Entry entry in list)
				{
					IComponent<GameObject> component = entry.Handler as IComponent<GameObject>;
					if (component != null && !component.Render(E))
					{
						return false;
					}
				}
			}
			if (E.ColorsVisible && this.IsPlayerControlled() && this.GetIntProperty("NoStatusColor", 0) <= 0)
			{
				if (Options.AlwaysHPColor)
				{
					E.ApplyColors(this.GetHPColor(), 10000);
				}
				else if (Options.HPColor)
				{
					string text;
					int num;
					this.GetHPColorAndPriority(out text, out num);
					E.ApplyColors(text, num);
				}
			}
			return true;
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x001BE1A8 File Offset: 0x001BC3A8
		public bool OverlayRender(RenderEvent E)
		{
			EventRegistry.List list;
			if (this.RegisteredEvents != null && this.RegisteredEvents.TryGetValue(RenderEvent.OverlayID, out list))
			{
				foreach (EventRegistry.List.Entry entry in list)
				{
					IComponent<GameObject> component = entry.Handler as IComponent<GameObject>;
					if (component != null && !component.OverlayRender(E))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x001BE228 File Offset: 0x001BC428
		public bool FinalRender(RenderEvent E)
		{
			EventRegistry.List list;
			if (this.RegisteredEvents != null && this.RegisteredEvents.TryGetValue(RenderEvent.FinalID, out list))
			{
				foreach (EventRegistry.List.Entry entry in list)
				{
					IComponent<GameObject> component = entry.Handler as IComponent<GameObject>;
					if (component != null && !component.FinalRender(E))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06005C69 RID: 23657 RVA: 0x001BE2A8 File Offset: 0x001BC4A8
		[Obsolete("Use FinalRender(RenderEvent)")]
		public bool FinalRender(RenderEvent E, bool Alt)
		{
			return this.FinalRender(E);
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x001BE2B4 File Offset: 0x001BC4B4
		public void Save(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this._BaseID);
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.Blueprint);
			Writer.WriteOptimized(this.GenderName);
			Writer.WriteOptimized(this.PronounSetName);
			if (this.Property.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				Writer.WriteOptimized(0);
			}
			else
			{
				Writer.WriteOptimized(this.Property.Count);
				foreach (KeyValuePair<string, string> keyValuePair in this.Property)
				{
					Writer.WriteOptimized(keyValuePair.Key);
					Writer.WriteOptimized(keyValuePair.Value);
				}
			}
			if (this.IntProperty.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				Writer.WriteOptimized(0);
			}
			else
			{
				Writer.WriteOptimized(this.IntProperty.Count);
				foreach (KeyValuePair<string, int> keyValuePair2 in this.IntProperty)
				{
					Writer.WriteOptimized(keyValuePair2.Key);
					Writer.WriteOptimized(keyValuePair2.Value);
				}
			}
			Writer.WriteOptimized(this.Statistics.Count);
			foreach (KeyValuePair<string, Statistic> keyValuePair3 in this.Statistics)
			{
				keyValuePair3.Value.Save(Writer);
			}
			if (this._Effects.IsNullOrEmpty<Effect>())
			{
				Writer.WriteOptimized(0);
			}
			else
			{
				Writer.WriteOptimized(this._Effects.Count);
				foreach (Effect effect in this._Effects)
				{
					Effect.Save(effect, Writer);
				}
			}
			if (this.PartsList.IsNullOrEmpty<IPart>())
			{
				Writer.WriteOptimized(0);
			}
			else
			{
				Writer.WriteOptimized(this.PartsList.Count);
				for (int i = 0; i < this.PartsList.Count; i++)
				{
					IPart.Save(this.PartsList[i], Writer);
				}
			}
			Writer.Write(this.RegisteredEvents);
			if (this.RegisteredEffectEvents.IsNullOrEmpty<KeyValuePair<string, List<Effect>>>())
			{
				Writer.WriteOptimized(0);
			}
			else
			{
				Writer.WriteOptimized(this.RegisteredEffectEvents.Count);
				foreach (KeyValuePair<string, List<Effect>> keyValuePair4 in this.RegisteredEffectEvents)
				{
					Writer.WriteOptimized(keyValuePair4.Key);
					List<Effect> value = keyValuePair4.Value;
					int count = value.Count;
					Writer.WriteOptimized(count);
					for (int j = 0; j < count; j++)
					{
						Writer.Write(value[j].ID);
					}
				}
			}
			if (this.RegisteredPartEvents.IsNullOrEmpty<KeyValuePair<string, List<IPart>>>())
			{
				Writer.WriteOptimized(0);
				return;
			}
			int num = this.RegisteredPartEvents.Count;
			foreach (KeyValuePair<string, List<IPart>> keyValuePair5 in this.RegisteredPartEvents)
			{
				List<IPart> value2 = keyValuePair5.Value;
				int num2 = keyValuePair5.Value.Count;
				for (int k = num2 - 1; k >= 0; k--)
				{
					if (value2[k].AllowStaticRegistration())
					{
						num2--;
					}
				}
				if (num2 <= 0)
				{
					num--;
				}
			}
			Writer.WriteOptimized(num);
			foreach (KeyValuePair<string, List<IPart>> keyValuePair6 in this.RegisteredPartEvents)
			{
				List<IPart> value3 = keyValuePair6.Value;
				int count2 = value3.Count;
				int num3 = count2;
				for (int l = count2 - 1; l >= 0; l--)
				{
					if (value3[l].AllowStaticRegistration())
					{
						num3--;
					}
				}
				if (num3 > 0)
				{
					Writer.WriteOptimized(keyValuePair6.Key);
					Writer.WriteOptimized(num3);
					for (int m = 0; m < count2; m++)
					{
						IPart part = value3[m];
						if (!part.AllowStaticRegistration())
						{
							if (part._ParentObject == this)
							{
								Writer.Write(0);
								Writer.WriteTokenized(part.GetType());
							}
							else
							{
								Writer.Write(1);
								Writer.WriteTokenized(part.GetType());
								Writer.WriteGameObject(part._ParentObject);
							}
						}
					}
				}
			}
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x001BE784 File Offset: 0x001BC984
		public void Load(SerializationReader Reader)
		{
			this._BaseID = Reader.ReadOptimizedInt32();
			this.Flags = Reader.ReadOptimizedInt32();
			this.Blueprint = Reader.ReadOptimizedString();
			this.GenderName = Reader.ReadOptimizedString();
			this.PronounSetName = Reader.ReadOptimizedString();
			int num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.Property.EnsureCapacity(num);
				for (int i = 0; i < num; i++)
				{
					this.Property.Add(Reader.ReadOptimizedString(), Reader.ReadOptimizedString());
				}
			}
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.IntProperty.EnsureCapacity(num);
				for (int j = 0; j < num; j++)
				{
					this.IntProperty.Add(Reader.ReadOptimizedString(), Reader.ReadOptimizedInt32());
				}
			}
			object obj = null;
			num = Reader.ReadOptimizedInt32();
			for (int k = 0; k < num; k++)
			{
				Statistic statistic = Statistic.Load(Reader, this);
				this.Statistics.Add(statistic.Name, statistic);
				obj = statistic;
			}
			this.Energy = this.Statistics.GetValue("Energy", null);
			Dictionary<string, List<Effect>> registeredEffectEvents = this.RegisteredEffectEvents;
			if (registeredEffectEvents != null)
			{
				registeredEffectEvents.Clear();
			}
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				Rack<Effect> effects = this.Effects;
				effects.EnsureCapacity(num);
				for (int l = 0; l < num; l++)
				{
					try
					{
						Effect effect = Effect.Load(this, Reader);
						if (effect != null)
						{
							effects.Add(effect);
							obj = effect;
						}
					}
					catch (Exception ex)
					{
						if (obj != null)
						{
							ex.Data["LastType"] = obj.GetType();
						}
						ExceptionDispatchInfo.Throw(ex);
					}
				}
			}
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.PartsList.EnsureCapacity(num);
				for (int m = 0; m < num; m++)
				{
					try
					{
						IPart part = IPart.Load(this, Reader);
						if (part != null)
						{
							this.AddPartInternals(part, false, false, true);
							obj = part;
						}
					}
					catch (Exception ex2)
					{
						if (obj != null)
						{
							ex2.Data["LastType"] = obj.GetType();
						}
						ExceptionDispatchInfo.Throw(ex2);
					}
				}
			}
			this.RegisteredEvents = Reader.ReadEventRegistry();
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				if (this.RegisteredEffectEvents == null)
				{
					this.RegisteredEffectEvents = new Dictionary<string, List<Effect>>(num);
				}
				this.RegisteredEffectEvents.EnsureCapacity(num);
				for (int n = 0; n < num; n++)
				{
					string text = Reader.ReadOptimizedString();
					int num2 = Reader.ReadOptimizedInt32();
					List<Effect> list = new List<Effect>(num2);
					this.RegisteredEffectEvents[text] = list;
					for (int num3 = 0; num3 < num2; num3++)
					{
						Effect effect2 = this.GetEffect(Reader.ReadGuid());
						if (effect2 != null)
						{
							list.Add(effect2);
						}
					}
				}
			}
			num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				if (this.RegisteredPartEvents == null)
				{
					this.RegisteredPartEvents = new Dictionary<string, List<IPart>>(num);
				}
				this.RegisteredPartEvents.EnsureCapacity(num);
				for (int num4 = 0; num4 < num; num4++)
				{
					string text2 = Reader.ReadOptimizedString();
					int num5 = Reader.ReadOptimizedInt32();
					List<IPart> list2 = new List<IPart>(num5);
					this.RegisteredPartEvents[text2] = list2;
					int num6 = 0;
					while (num6 < num5)
					{
						bool flag = Reader.ReadByte() == 1;
						Type type = Reader.ReadTokenizedType();
						GameObject gameObject = this;
						if (!flag)
						{
							goto IL_03AF;
						}
						GameObject gameObject2 = Reader.ReadGameObject(null);
						if (gameObject2 == null)
						{
							MetricsManager.LogError(string.Concat(new string[]
							{
								this.DebugName,
								".",
								text2,
								": External parent of part '",
								(type != null) ? type.FullName : null,
								"' is null, skipping."
							}));
						}
						else
						{
							if (gameObject2.PartsList.Count != 0)
							{
								gameObject = gameObject2;
								goto IL_03AF;
							}
							List<GameObject.ExternalEventBind> list3;
							if (!GameObject.ExternalLoadBindings.TryGetValue(this, out list3))
							{
								list3 = (GameObject.ExternalLoadBindings[this] = new List<GameObject.ExternalEventBind>());
							}
							list3.Add(new GameObject.ExternalEventBind(text2, gameObject2, type));
						}
						IL_0410:
						num6++;
						continue;
						IL_03AF:
						IPart part2 = gameObject.GetPart(type);
						if (part2 == null)
						{
							MetricsManager.LogError(string.Concat(new string[]
							{
								this.DebugName,
								".",
								text2,
								": Bad event bind from part '",
								(type != null) ? type.FullName : null,
								"'."
							}));
							goto IL_0410;
						}
						list2.Add(part2);
						goto IL_0410;
					}
				}
			}
			if (this.PartsList != null)
			{
				int num7 = 0;
				int count = this.PartsList.Count;
				while (num7 < count)
				{
					this.PartsList[num7].ObjectLoaded();
					num7++;
				}
			}
			this.ResetNameCache();
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x001BEC18 File Offset: 0x001BCE18
		public void FinalizeRead(SerializationReader Reader)
		{
			List<GameObject.ExternalEventBind> list;
			if (GameObject.ExternalLoadBindings.TryGetValue(this, out list))
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					GameObject.ExternalEventBind externalEventBind = list[i];
					IPart part = externalEventBind.Object.GetPart(externalEventBind.Type);
					if (part == null)
					{
						string[] array = new string[8];
						array[0] = this.DebugName;
						array[1] = ".";
						array[2] = externalEventBind.Event;
						array[3] = ": Bad event bind from ";
						array[4] = externalEventBind.Object.DebugName;
						array[5] = ", part '";
						int num = 6;
						Type type = externalEventBind.Type;
						array[num] = ((type != null) ? type.FullName : null);
						array[7] = "'.";
						MetricsManager.LogError(string.Concat(array));
					}
					else
					{
						List<IPart> list2;
						if (!this.RegisteredPartEvents.TryGetValue(externalEventBind.Event, out list2))
						{
							list2 = (this.RegisteredPartEvents[externalEventBind.Event] = new List<IPart>());
						}
						list2.Add(part);
					}
					i++;
				}
			}
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				effects.FinalizeRead(this, Reader);
			}
			this.PartsList.FinalizeRead(this, Reader);
			this.Flags &= -33;
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x001BED95 File Offset: 0x001BCF95
		public GameObjectReference Reference()
		{
			return new GameObjectReference(this);
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x001BED9D File Offset: 0x001BCF9D
		public GameObject Split(int n, bool NoRemove = false)
		{
			this.SplitStack(n, null, NoRemove);
			return this;
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x001BEDAA File Offset: 0x001BCFAA
		public GameObject SplitFromStack()
		{
			this.SplitStack(1, null, false);
			return this;
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x001BEDB7 File Offset: 0x001BCFB7
		public GameObject RemoveOne()
		{
			if (this.Stacker != null)
			{
				return this.Stacker.RemoveOne();
			}
			return this;
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x001BEDCE File Offset: 0x001BCFCE
		public void Release(bool RemoveFromContext = true, bool AllowPool = true)
		{
			if (RemoveFromContext)
			{
				this.RemoveFromContext(null);
			}
			if (AllowPool)
			{
				this.Pool();
				return;
			}
			this.Clear();
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x001BEDEA File Offset: 0x001BCFEA
		public static void Release(ref GameObject Object, bool RemoveFromContext = true, bool AllowPool = true)
		{
			Object.Release(RemoveFromContext, AllowPool);
			Object = null;
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x001BEDF8 File Offset: 0x001BCFF8
		public static GameObject Create(string Blueprint)
		{
			return GameObjectFactory.Factory.CreateObject(Blueprint);
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x001BEE08 File Offset: 0x001BD008
		public static GameObject Create(string Blueprint, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> ProvideInventory = null)
		{
			return GameObjectFactory.Factory.CreateObject(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, ProvideInventory);
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x001BEE2C File Offset: 0x001BD02C
		public static GameObject Create(GameObjectBlueprint Blueprint, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> ProvideInventory = null)
		{
			return GameObjectFactory.Factory.CreateObject(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, ProvideInventory);
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x001BEE4F File Offset: 0x001BD04F
		public static GameObject CreateSample(string Blueprint)
		{
			return GameObjectFactory.Factory.CreateSampleObject(Blueprint, null);
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x001BEE60 File Offset: 0x001BD060
		public static GameObject CreateUnmodified(string Blueprint)
		{
			return GameObjectFactory.Factory.CreateObject(Blueprint, -9999, 0, null, null, null, null, null);
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x001BEE84 File Offset: 0x001BD084
		public static GameObject CreateUnmodified(string Blueprint, string Context = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			return GameObjectFactory.Factory.CreateObject(Blueprint, -9999, 0, null, BeforeObjectCreated, AfterObjectCreated, Context, null);
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x001BEEA9 File Offset: 0x001BD0A9
		public static bool Validate(ref GameObject Object)
		{
			if (Object == null)
			{
				return false;
			}
			if (Object.IsInvalid())
			{
				Object = null;
				return false;
			}
			return true;
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x001BEEC0 File Offset: 0x001BD0C0
		public static bool Validate(GameObject Object)
		{
			return Object != null && !Object.IsInvalid();
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06005C7C RID: 23676 RVA: 0x001BEED2 File Offset: 0x001BD0D2
		public bool IsReal
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				return physics != null && physics.IsReal;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06005C7D RID: 23677 RVA: 0x001BEEE5 File Offset: 0x001BD0E5
		// (set) Token: 0x06005C7E RID: 23678 RVA: 0x001BEEF8 File Offset: 0x001BD0F8
		public bool IsOrganic
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				return physics != null && physics.Organic;
			}
			set
			{
				if (this.Physics != null)
				{
					this.Physics.Organic = value;
				}
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x001BEF0E File Offset: 0x001BD10E
		public int Temperature
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return 0;
				}
				return physics.Temperature;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06005C80 RID: 23680 RVA: 0x001BEF21 File Offset: 0x001BD121
		public bool Takeable
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				return physics != null && physics.Takeable;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x001BEF34 File Offset: 0x001BD134
		public bool IsScenery
		{
			get
			{
				Render render = this.Render;
				return ((render != null) ? render.RenderLayer : 0) <= 0;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x001BEF50 File Offset: 0x001BD150
		// (set) Token: 0x06005C83 RID: 23683 RVA: 0x001BEF84 File Offset: 0x001BD184
		public bool Slimewalking
		{
			get
			{
				int num;
				if (!this.IntProperty.TryGetValue("Slimewalking", out num) || num == 0)
				{
					return this.HasTag("Slimewalking");
				}
				return num > 0;
			}
			set
			{
				this.SetIntProperty("Slimewalking", value ? 1 : (-1), false);
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x001BEF9C File Offset: 0x001BD19C
		// (set) Token: 0x06005C85 RID: 23685 RVA: 0x001BEFD0 File Offset: 0x001BD1D0
		public bool Polypwalking
		{
			get
			{
				int num;
				if (!this.IntProperty.TryGetValue("Polypwalking", out num) || num == 0)
				{
					return this.HasTag("Polypwalking");
				}
				return num > 0;
			}
			set
			{
				this.SetIntProperty("Polypwalking", value ? 1 : (-1), false);
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06005C86 RID: 23686 RVA: 0x001BEFE8 File Offset: 0x001BD1E8
		// (set) Token: 0x06005C87 RID: 23687 RVA: 0x001BF01C File Offset: 0x001BD21C
		public bool Strutwalking
		{
			get
			{
				int num;
				if (!this.IntProperty.TryGetValue("Strutwalking", out num) || num == 0)
				{
					return this.HasTag("Strutwalking");
				}
				return num > 0;
			}
			set
			{
				this.SetIntProperty("Strutwalking", value ? 1 : (-1), false);
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06005C88 RID: 23688 RVA: 0x001BF034 File Offset: 0x001BD234
		// (set) Token: 0x06005C89 RID: 23689 RVA: 0x001BF068 File Offset: 0x001BD268
		public bool Reefer
		{
			get
			{
				int num;
				if (!this.IntProperty.TryGetValue("Reefer", out num) || num == 0)
				{
					return this.HasTag("Reefer");
				}
				return num > 0;
			}
			set
			{
				this.SetIntProperty("Reefer", value ? 1 : (-1), false);
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x001BF080 File Offset: 0x001BD280
		// (set) Token: 0x06005C8B RID: 23691 RVA: 0x001BF0A7 File Offset: 0x001BD2A7
		public bool IsCurrency
		{
			get
			{
				int num;
				return this.IntProperty.TryGetValue("Currency", out num) && num > 0;
			}
			set
			{
				this.ModIntProperty("Currency", value ? 1 : (-1), true);
			}
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x001BF0BD File Offset: 0x001BD2BD
		public bool InACell()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			object obj;
			if (physics == null)
			{
				obj = null;
			}
			else
			{
				Cell currentCell = physics.CurrentCell;
				obj = ((currentCell != null) ? currentCell.ParentZone : null);
			}
			return obj != null;
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x001BF0E0 File Offset: 0x001BD2E0
		public bool OnWorldMap()
		{
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.OnWorldMap();
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x001BF0F4 File Offset: 0x001BD2F4
		public bool InZone(Zone Zone)
		{
			if (Zone == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.ParentZone == Zone;
		}

		// Token: 0x06005C8F RID: 23695 RVA: 0x001BF11C File Offset: 0x001BD31C
		public bool InZone(string ZoneID)
		{
			if (ZoneID == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.ParentZone != null && currentCell.ParentZone.ZoneID == ZoneID;
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x001BF155 File Offset: 0x001BD355
		public bool InSameZone(Cell Cell)
		{
			return this.InZone((Cell != null) ? Cell.ParentZone : null);
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x001BF169 File Offset: 0x001BD369
		public bool InSameZone(GameObject Object)
		{
			return this.InZone((Object != null) ? Object.GetCurrentZone() : null);
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x001BF180 File Offset: 0x001BD380
		public void MeleeAttackWithWeapon(GameObject Target, GameObject Weapon, bool Autohit = false, bool Autopen = false)
		{
			string text = null;
			if (Autohit)
			{
				if (Autopen)
				{
					text = "Autohit,Autopen";
				}
				else
				{
					text = "Autohit";
				}
			}
			else if (Autopen)
			{
				text = "Autopen";
			}
			Combat.MeleeAttackWithWeapon(this, Target, Weapon, this.Body.FindDefaultOrEquippedItem(Weapon), text, 0, 0, 0, 0, 0, false, true);
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x001BF1D0 File Offset: 0x001BD3D0
		public bool CellTeleport(Cell C, IEvent FromEvent = null, GameObject Device = null, GameObject DeviceOperator = null, IPart Mutation = null, string SuccessMessage = null, int? EnergyCost = 0, bool Forced = false, bool VisualEffects = true, bool ReducedVisualEffects = false, bool SkipRealityDistortion = false, string LeaveVerb = "disappear", string ArriveVerb = "appear", Cell FromCell = null)
		{
			if (FromCell == null)
			{
				FromCell = this.CurrentCell;
			}
			Zone zone = ((FromCell != null) ? FromCell.ParentZone : null);
			Zone zone2 = ((C != null) ? C.ParentZone : null);
			if (zone2 == null)
			{
				return false;
			}
			bool flag = SkipRealityDistortion;
			if (!flag)
			{
				Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
				@event.SetParameter("Object", this);
				@event.SetParameter("Cell", C);
				if (Device != null)
				{
					@event.SetParameter("Device", Device);
				}
				if (DeviceOperator != null)
				{
					@event.SetParameter("Operator", DeviceOperator);
				}
				if (Mutation != null)
				{
					@event.SetParameter("Mutation", Mutation);
				}
				flag = this.FireEvent(@event, FromEvent) && C.FireEvent(@event, FromEvent);
			}
			if (flag)
			{
				if (!SuccessMessage.IsNullOrEmpty() && this.IsPlayer())
				{
					Popup.Show(SuccessMessage, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				if (this.TeleportTo(C, EnergyCost, true, false, Forced, false, LeaveVerb, ArriveVerb))
				{
					if (VisualEffects && !this.IsPlayer() && this.ThePlayer != null && this.ThePlayer.InZone(zone))
					{
						if (ReducedVisualEffects)
						{
							this.SmallTeleportSwirl(FromCell, "&C", !Forced, null, false);
						}
						else
						{
							this.TeleportSwirl(FromCell, "&C", !Forced, null, 'ù', false);
						}
					}
					if (VisualEffects && (this.IsPlayer() || (this.ThePlayer != null && this.ThePlayer.InZone(zone2))))
					{
						if (zone != null && (zone2.ResolveZoneWorld() == zone.ResolveZoneWorld() || !this.IsPlayer()))
						{
							if (ReducedVisualEffects)
							{
								this.SmallTeleportSwirl(null, "&C", false, null, false);
							}
							else
							{
								this.TeleportSwirl(null, "&C", false, null, 'ù', false);
							}
						}
						else
						{
							GameManager.Instance.Spacefolding = true;
							this.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_spacetimeWeirdness", 1f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x001BF3C0 File Offset: 0x001BD5C0
		public bool ZoneTeleport(string Zone, int X = -1, int Y = -1, IEvent FromEvent = null, GameObject Device = null, GameObject DeviceOperator = null, IPart Mutation = null, string SuccessMessage = "You are transported!", bool VisualEffects = true, Cell FromCell = null)
		{
			if (FromCell == null)
			{
				FromCell = this.CurrentCell;
			}
			Zone zone = ((FromCell != null) ? FromCell.ParentZone : null);
			Zone zone2 = XRL.The.ZoneManager.GetZone(Zone);
			Cell cell = zone2.GetCell(X, Y);
			if (X == -1 || Y == -1 || cell == null || !IComponent<GameObject>.CheckRealityDistortionAccessibility(null, cell, DeviceOperator, Device, null, 0))
			{
				try
				{
					List<Cell> emptyReachableCells = zone2.GetEmptyReachableCells((Cell c) => IComponent<GameObject>.CheckRealityDistortionAccessibility(null, c, DeviceOperator, Device, null, 0));
					if (emptyReachableCells.Count > 0)
					{
						cell = emptyReachableCells.GetRandomElement<Cell>(null);
					}
					else
					{
						cell = zone2.GetCell(40, 20);
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					cell = zone2.GetCell(40, 20);
				}
			}
			Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
			@event.SetParameter("Object", this);
			@event.SetParameter("Cell", cell);
			if (Device != null)
			{
				@event.SetParameter("Device", Device);
			}
			if (DeviceOperator != null)
			{
				@event.SetParameter("Operator", DeviceOperator);
			}
			if (Mutation != null)
			{
				@event.SetParameter("Mutation", Mutation);
			}
			bool flag = this.FireEvent(@event, FromEvent) && cell.FireEvent(@event, FromEvent);
			if (flag)
			{
				if (!SuccessMessage.IsNullOrEmpty() && this.IsPlayer())
				{
					Popup.Show(SuccessMessage, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				if (this.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", "appear"))
				{
					if (VisualEffects && !this.IsPlayer() && this.ThePlayer != null && this.ThePlayer.InZone(zone))
					{
						this.TeleportSwirl(FromCell, "&C", false, null, 'ù', false);
					}
					if (VisualEffects && (this.IsPlayer() || (this.ThePlayer != null && this.ThePlayer.InZone(zone2))))
					{
						if (zone != null && (zone2.ResolveZoneWorld() == zone.ResolveZoneWorld() || !this.IsPlayer()))
						{
							this.TeleportSwirl(null, "&C", false, null, 'ù', false);
						}
						else
						{
							GameManager.Instance.Spacefolding = true;
							this.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_spacetimeWeirdness", 1f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
					}
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x001BF634 File Offset: 0x001BD834
		public void StopMoving()
		{
			if (this.IsPlayer())
			{
				AutoAct.Interrupt(null, null, null, false);
			}
			if (this.Brain != null)
			{
				this.Brain.StopMoving();
			}
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x001BF65C File Offset: 0x001BD85C
		public Cell GetRandomTeleportTargetCell(int MaxDistance = 0)
		{
			Cell CC = this.CurrentCell;
			if (CC == null)
			{
				return null;
			}
			Zone parentZone = CC.ParentZone;
			if (parentZone == null)
			{
				return null;
			}
			List<Cell> list = parentZone.GetPassableCells(this);
			list.RemoveAll((Cell c) => !IComponent<GameObject>.CheckRealityDistortionAccessibility(c));
			if (MaxDistance > 0)
			{
				list = list.Where<Cell>((Cell EC) => EC.PathDistanceTo(CC) <= MaxDistance).ToList<Cell>();
			}
			if (list.Contains(CC))
			{
				list.Remove(CC);
			}
			return list.GetRandomElement<Cell>(null);
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x001BF70E File Offset: 0x001BD90E
		public void Flameburst(ScreenBuffer Buffer)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.Flameburst(Buffer);
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x001BF724 File Offset: 0x001BD924
		public bool RandomTeleport(bool Swirl = false, IPart Mutation = null, GameObject Device = null, GameObject DeviceOperator = null, IEvent FromEvent = null, int EnergyCost = 0, int MaxDistance = 0, bool InterruptMovement = true, Cell TargetCell = null, bool Forced = false, bool IgnoreCombat = true, bool Voluntary = true, bool UsePopups = false)
		{
			if (this.OnWorldMap())
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (TargetCell == null)
			{
				TargetCell = this.GetRandomTeleportTargetCell(MaxDistance);
				if (TargetCell == null)
				{
					return false;
				}
			}
			Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
			@event.SetParameter("Object", this);
			@event.SetParameter("Cell", TargetCell);
			if (Mutation != null)
			{
				@event.SetParameter("Mutation", Mutation);
				if (Mutation.ParentObject != null && Mutation.ParentObject != this && !Mutation.ParentObject.FireEvent(@event, FromEvent))
				{
					return false;
				}
			}
			if (Device != null)
			{
				@event.SetParameter("Device", Device);
				if (DeviceOperator != null)
				{
					@event.SetParameter("Operator", DeviceOperator);
				}
				if (Device != this && !Device.FireEvent(@event, FromEvent))
				{
					return false;
				}
				if (DeviceOperator != this && DeviceOperator != Device && !DeviceOperator.FireEvent(@event, FromEvent))
				{
					return false;
				}
			}
			if (!this.FireEvent(@event, FromEvent) || !TargetCell.FireEvent(@event, FromEvent))
			{
				return false;
			}
			this.ParticleBlip("&C\u000f", 10, 0L, false);
			if (Voluntary)
			{
				this.PlayWorldSound("Sounds/Abilities/sfx_ability_teleport_voluntary_out", 1f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			else
			{
				this.PlayWorldSound("Sounds/Abilities/sfx_ability_teleport_involuntary_out", 1f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (!this.TeleportTo(TargetCell, new int?(EnergyCost), IgnoreCombat, false, Forced, UsePopups, "disappear", "appear"))
			{
				return false;
			}
			if (Swirl && currentCell != null && currentCell.ParentZone.IsActive())
			{
				if (currentCell.PathDistanceTo(TargetCell) > 5)
				{
					this.TeleportSwirl(null, "&C", Voluntary, null, 'ù', false);
				}
				else
				{
					this.SmallTeleportSwirl(null, "&C", Voluntary, null, false);
				}
			}
			this.ParticleBlip("&C\u000f", 10, 0L, false);
			if (InterruptMovement)
			{
				this.StopMoving();
			}
			if (this.IsPlayer())
			{
				if (currentCell.X > 42 && Sidebar.State == "right")
				{
					Sidebar.SetSidebarState("left");
				}
				if (currentCell.X < 38 && Sidebar.State == "left")
				{
					Sidebar.SetSidebarState("right");
				}
			}
			return true;
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x001BF958 File Offset: 0x001BDB58
		public void TeleportSwirl(Cell C = null, string Color = "&C", bool Voluntary = false, string Sound = null, char Char = 'ù', bool IsOut = false)
		{
			if (C == null)
			{
				C = this.GetCurrentCell();
			}
			if (C != null && C.ParentZone.IsActive())
			{
				if (IsOut)
				{
					this.PlayWorldSound(Sound ?? (Voluntary ? "Sounds/Abilities/sfx_ability_teleport_voluntary_out" : "Sounds/Abilities/sfx_ability_teleport_involuntary_out"), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				else
				{
					this.PlayWorldSound(Sound ?? (Voluntary ? "Sounds/Abilities/sfx_ability_teleport_voluntary_in" : "Sounds/Abilities/sfx_ability_teleport_involuntary_in"), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Options.UseParticleVFX && C.ParentZone != null)
				{
					Cell currentCell = this.GetCurrentCell();
					if (currentCell != null && currentCell.InActiveZone)
					{
						ParticleTeleportInVFX.ConfigureObject configureObject = new ParticleTeleportInVFX.ConfigureObject(this);
						if (IsOut)
						{
							Cell currentCell2 = this.CurrentCell;
							CombatJuice.playPrefabAnimation((currentCell2 != null) ? currentCell2.Location : null, "Abilities/AbilityVFXTeleportOut", null, null, configureObject, false);
							return;
						}
						Cell currentCell3 = this.CurrentCell;
						CombatJuice.playPrefabAnimation((currentCell3 != null) ? currentCell3.Location : null, "Abilities/AbilityVFXTeleportIn", null, null, configureObject, false);
						return;
					}
				}
				else
				{
					for (int i = 0; i < 30; i++)
					{
						XRL.The.ParticleManager.AddRadial(Color + Char.ToString(), (float)C.X, (float)C.Y, (float)XRL.Rules.Stat.Random(0, 7), (float)XRL.Rules.Stat.Random(5, 10), 0.01f * (float)XRL.Rules.Stat.Random(4, 6), -0.05f * (float)XRL.Rules.Stat.Random(3, 7));
					}
				}
			}
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x001BFAD8 File Offset: 0x001BDCD8
		public void SmallTeleportSwirl(Cell C = null, string Color = "&C", bool Voluntary = false, string Sound = null, bool IsOut = false)
		{
			if (C == null)
			{
				C = this.GetCurrentCell();
			}
			if (C != null && C.ParentZone.IsActive())
			{
				if (IsOut)
				{
					this.PlayWorldSound(Sound ?? (Voluntary ? "Sounds/Abilities/sfx_ability_teleport_voluntary_out" : "Sounds/Abilities/sfx_ability_teleport_involuntary_out"), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				else
				{
					this.PlayWorldSound(Sound ?? (Voluntary ? "Sounds/Abilities/sfx_ability_teleport_voluntary_in" : "Sounds/Abilities/sfx_ability_teleport_involuntary_in"), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				for (int i = 0; i < 10; i++)
				{
					XRL.The.ParticleManager.AddRadial(Color + "ù", (float)C.X, (float)C.Y, (float)XRL.Rules.Stat.Random(0, 5), (float)XRL.Rules.Stat.Random(4, 8), 0.01f * (float)XRL.Rules.Stat.Random(3, 5), -0.05f * (float)XRL.Rules.Stat.Random(2, 6));
				}
			}
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x001BFBDC File Offset: 0x001BDDDC
		public void SpatialDistortionBlip(Cell C = null, string Color = "&C")
		{
			Cell cell = C ?? this.GetCurrentCell();
			if (cell == null)
			{
				return;
			}
			cell.SpatialDistortionBlip(Color);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x001BFBF4 File Offset: 0x001BDDF4
		public void TechTeleportSwirlOut(Cell C = null, string Color = "&B", string Sound = null)
		{
			this.TeleportSwirl(C, Color, true, Sound, 'ù', true);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x001BFC06 File Offset: 0x001BDE06
		public void TechTeleportSwirlIn(Cell C = null, string Color = "&C", string Sound = null)
		{
			this.TeleportSwirl(C, Color, true, Sound, 'ù', false);
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x001BFC18 File Offset: 0x001BDE18
		public bool DirectMoveTo(GlobalLocation targetLocation, int EnergyCost = 0, bool Forced = false, bool IgnoreCombat = false, bool IgnoreGravity = false)
		{
			if (targetLocation != null)
			{
				Zone zone = ZoneManager.instance.GetZone(targetLocation.ZoneID);
				return this.DirectMoveTo(zone.GetCell(targetLocation.CellX, targetLocation.CellY), EnergyCost, Forced, IgnoreCombat, IgnoreGravity, null);
			}
			return false;
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x001BFC5C File Offset: 0x001BDE5C
		public bool DirectMoveTo(Cell targetCell, int EnergyCost = 0, bool Forced = false, bool IgnoreCombat = false, bool IgnoreGravity = false, GameObject Ignore = null)
		{
			return this.Physics != null && this.Physics.ProcessTargetedMove(targetCell, "DirectMove", "BeforeDirectMove", "AfterDirectMove", new int?(EnergyCost), Forced, false, IgnoreCombat, IgnoreGravity, false, false, null, null, Ignore);
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x001BFCA0 File Offset: 0x001BDEA0
		public bool SystemLongDistanceMoveTo(Cell targetCell, int? energyCost = null, bool forced = false, bool ignoreCombat = true)
		{
			return this.Physics != null && this.Physics.ProcessTargetedMove(targetCell, "SystemLongDistanceMove", "BeforeSystemLongDistanceMove", "AfterSystemLongDistanceMove", energyCost, forced, true, ignoreCombat, false, false, false, null, null, null);
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x001BFCE0 File Offset: 0x001BDEE0
		public bool SystemMoveTo(Cell targetCell, int? energyCost = null, bool forced = false, bool ignoreCombat = true, bool ignoreGravity = false, bool noStack = false)
		{
			return this.Physics != null && this.Physics.ProcessTargetedMove(targetCell, "SystemMove", "BeforeSystemMove", "AfterSystemMove", energyCost, forced, true, ignoreCombat, ignoreGravity, noStack, false, null, null, null);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x001BFD20 File Offset: 0x001BDF20
		public bool TeleportTo(Cell targetCell, int? energyCost = 0, bool ignoreCombat = true, bool ignoreGravity = false, bool forced = false, bool UsePopups = false, string leaveVerb = "disappear", string arriveVerb = "appear")
		{
			return this.Physics != null && this.Physics.ProcessTargetedMove(targetCell, "Teleporting", "BeforeTeleport", "AfterTeleport", energyCost, forced, false, ignoreCombat, ignoreGravity, false, UsePopups, leaveVerb, arriveVerb, null);
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x001BFD63 File Offset: 0x001BDF63
		public void PerformMeleeAttack(GameObject target)
		{
			Combat.PerformMeleeAttack(this, target, 1000, 0, 0, 0, null, false);
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x001BFD78 File Offset: 0x001BDF78
		public Cell FastGetCurrentCell()
		{
			if (this.Physics == null)
			{
				return null;
			}
			if (this.Physics.CurrentCell != null)
			{
				return this.Physics.CurrentCell;
			}
			if (this.Physics.Equipped != null)
			{
				return this.Physics.Equipped.FastGetCurrentCell();
			}
			if (this.Physics.InInventory != null)
			{
				return this.Physics.InInventory.FastGetCurrentCell();
			}
			return null;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x001BFDE5 File Offset: 0x001BDFE5
		public void SyncMutationLevelAndGlimmer()
		{
			SyncMutationLevelsEvent.Send(this);
			GlimmerChangeEvent.Send(this);
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x001BFDF4 File Offset: 0x001BDFF4
		public Cell GetCurrentCell()
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell != null)
			{
				return currentCell;
			}
			GameObject gameObject;
			Cell cell;
			GetContextEvent.Get(this, out gameObject, out cell);
			if (cell != null)
			{
				return cell;
			}
			if (gameObject != null)
			{
				return gameObject.GetCurrentCell();
			}
			return null;
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x001BFE27 File Offset: 0x001BE027
		public Zone GetCurrentZone()
		{
			Zone zone;
			if ((zone = this.CurrentZone) == null)
			{
				Cell currentCell = this.GetCurrentCell();
				if (currentCell == null)
				{
					return null;
				}
				zone = currentCell.ParentZone;
			}
			return zone;
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x001BFE44 File Offset: 0x001BE044
		public bool Contains(GameObject Object)
		{
			return ContainsEvent.Check(this, Object);
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x001BFE4D File Offset: 0x001BE04D
		public bool ContainsBlueprint(string Blueprint)
		{
			return ContainsBlueprintEvent.Check(this, Blueprint);
		}

		// Token: 0x06005CAA RID: 23722 RVA: 0x001BFE56 File Offset: 0x001BE056
		public bool ContainsAnyBlueprint(List<string> Blueprints)
		{
			return ContainsAnyBlueprintEvent.Check(this, Blueprints);
		}

		// Token: 0x06005CAB RID: 23723 RVA: 0x001BFE5F File Offset: 0x001BE05F
		public GameObject FindContainedObjectByBlueprint(string Blueprint)
		{
			return ContainsBlueprintEvent.Find(this, Blueprint);
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x001BFE68 File Offset: 0x001BE068
		public GameObject FindContainedObjectByAnyBlueprint(List<string> Blueprints)
		{
			return ContainsAnyBlueprintEvent.Find(this, Blueprints);
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x001BFE74 File Offset: 0x001BE074
		public GenotypeEntry genotypeEntry
		{
			get
			{
				string genotype = this.GetGenotype();
				GenotypeEntry genotypeEntry;
				if (genotype != null && GenotypeFactory.GenotypesByName.TryGetValue(genotype, out genotypeEntry))
				{
					return genotypeEntry;
				}
				return null;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06005CAE RID: 23726 RVA: 0x001BFEA0 File Offset: 0x001BE0A0
		public SubtypeEntry subtypeEntry
		{
			get
			{
				string stringProperty;
				SubtypeEntry subtypeEntry;
				if ((stringProperty = this.GetStringProperty("Subtype", null)) != null && SubtypeFactory.SubtypesByName.TryGetValue(stringProperty, out subtypeEntry))
				{
					return subtypeEntry;
				}
				return null;
			}
		}

		/// <summary>
		/// Check if object blueprint has a specific tag.
		/// </summary>
		/// <param name="TagName">The tag to search for.</param>
		// Token: 0x06005CAF RID: 23727 RVA: 0x001BFECF File Offset: 0x001BE0CF
		public bool HasTag(string TagName)
		{
			if (TagName != null)
			{
				GameObjectBlueprint blueprint = this.GetBlueprint(false);
				return blueprint != null && blueprint.HasTag(TagName, false);
			}
			return false;
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x001BFEEA File Offset: 0x001BE0EA
		public bool IsInGraveyard()
		{
			return (this.Flags & 4) != 0;
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x001BFEF7 File Offset: 0x001BE0F7
		public bool HasHitpoints()
		{
			return this.Stat("Hitpoints", 0) > 0;
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001BFF08 File Offset: 0x001BE108
		public bool HasContext()
		{
			return GetContextEvent.HasAny(this);
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x001BFF10 File Offset: 0x001BE110
		public void GetContext(out GameObject ObjectContext, out Cell CellContext, out BodyPart BodyPartContext, out int Relation, out IContextRelationManager RelationManager)
		{
			GetContextEvent.Get(this, out ObjectContext, out CellContext, out BodyPartContext, out Relation, out RelationManager);
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x001BFF20 File Offset: 0x001BE120
		public void GetContext(out GameObject ObjectContext, out Cell CellContext, out BodyPart BodyPartContext)
		{
			int num;
			IContextRelationManager contextRelationManager;
			GetContextEvent.Get(this, out ObjectContext, out CellContext, out BodyPartContext, out num, out contextRelationManager);
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x001BFF3C File Offset: 0x001BE13C
		public void GetContext(out GameObject ObjectContext, out Cell CellContext)
		{
			BodyPart bodyPart;
			int num;
			IContextRelationManager contextRelationManager;
			GetContextEvent.Get(this, out ObjectContext, out CellContext, out bodyPart, out num, out contextRelationManager);
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x001BFF58 File Offset: 0x001BE158
		public GameObject GetObjectContext()
		{
			GameObject gameObject;
			Cell cell;
			BodyPart bodyPart;
			int num;
			IContextRelationManager contextRelationManager;
			this.GetContext(out gameObject, out cell, out bodyPart, out num, out contextRelationManager);
			return gameObject;
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x001BFF78 File Offset: 0x001BE178
		public GameObject GetObjectContext(out BodyPart BodyPartContext)
		{
			GameObject gameObject;
			Cell cell;
			int num;
			IContextRelationManager contextRelationManager;
			this.GetContext(out gameObject, out cell, out BodyPartContext, out num, out contextRelationManager);
			return gameObject;
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x001BFF98 File Offset: 0x001BE198
		public GameObject GetObjectContext(out BodyPart BodyPartContext, out int Relation)
		{
			GameObject gameObject;
			Cell cell;
			IContextRelationManager contextRelationManager;
			this.GetContext(out gameObject, out cell, out BodyPartContext, out Relation, out contextRelationManager);
			return gameObject;
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x001BFFB4 File Offset: 0x001BE1B4
		public GameObject GetObjectContext(out BodyPart BodyPartContext, out int Relation, out IContextRelationManager RelationManager)
		{
			GameObject gameObject;
			Cell cell;
			this.GetContext(out gameObject, out cell, out BodyPartContext, out Relation, out RelationManager);
			return gameObject;
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x001BFFD0 File Offset: 0x001BE1D0
		public Cell GetCellContext()
		{
			GameObject gameObject;
			Cell cell;
			BodyPart bodyPart;
			int num;
			IContextRelationManager contextRelationManager;
			this.GetContext(out gameObject, out cell, out bodyPart, out num, out contextRelationManager);
			return cell;
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x001BFFF0 File Offset: 0x001BE1F0
		public Cell GetCellContext(out int Relation)
		{
			GameObject gameObject;
			Cell cell;
			BodyPart bodyPart;
			IContextRelationManager contextRelationManager;
			this.GetContext(out gameObject, out cell, out bodyPart, out Relation, out contextRelationManager);
			return cell;
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x001C0010 File Offset: 0x001BE210
		public Cell GetCellContext(out int Relation, out IContextRelationManager RelationManager)
		{
			GameObject gameObject;
			Cell cell;
			BodyPart bodyPart;
			this.GetContext(out gameObject, out cell, out bodyPart, out Relation, out RelationManager);
			return cell;
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x001C002C File Offset: 0x001BE22C
		public int GetMatterPhase()
		{
			return MatterPhase.getMatterPhase(this);
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x001C0034 File Offset: 0x001BE234
		public bool IsNowhere()
		{
			if (this.IsInGraveyard())
			{
				return true;
			}
			if (this.CurrentCell != null)
			{
				return false;
			}
			GameObject gameObject;
			Cell cell;
			this.GetContext(out gameObject, out cell);
			if (gameObject != null)
			{
				return gameObject.IsNowhere();
			}
			return cell == null;
		}

		// Token: 0x06005CBF RID: 23743 RVA: 0x001C006F File Offset: 0x001BE26F
		public bool IsOwned()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return !string.IsNullOrEmpty((physics != null) ? physics.Owner : null);
		}

		// Token: 0x06005CC0 RID: 23744 RVA: 0x001C008B File Offset: 0x001BE28B
		public void RemoveFromContext(IEvent ParentEvent = null)
		{
			RemoveFromContextEvent.Send(this, ParentEvent);
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x001C0094 File Offset: 0x001BE294
		public bool TryRemoveFromContext(IEvent ParentEvent = null)
		{
			return TryRemoveFromContextEvent.Check(this, null);
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x001C00A0 File Offset: 0x001BE2A0
		public GameObject ThePlayer
		{
			get
			{
				return XRL.The.Player;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06005CC3 RID: 23747 RVA: 0x001C00A8 File Offset: 0x001BE2A8
		// (set) Token: 0x06005CC4 RID: 23748 RVA: 0x001C00F0 File Offset: 0x001BE2F0
		public int BaseID
		{
			get
			{
				if (this._BaseID == 0)
				{
					XRLGame game = XRL.The.Game;
					if (game == null)
					{
						this._BaseID = int.MinValue;
					}
					else
					{
						XRLGame xrlgame = game;
						int num = xrlgame.GameObjectIDSequence + 1;
						xrlgame.GameObjectIDSequence = num;
						this._BaseID = num;
					}
				}
				return this._BaseID;
			}
			set
			{
				this._BaseID = value;
			}
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x001C00F9 File Offset: 0x001BE2F9
		public string RequireID()
		{
			string id = this.ID;
			return this.ID;
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x001C0108 File Offset: 0x001BE308
		// (set) Token: 0x06005CC7 RID: 23751 RVA: 0x001C0116 File Offset: 0x001BE316
		public string IDIfAssigned
		{
			get
			{
				return this.GetStringProperty("id", null);
			}
			set
			{
				this.SetStringProperty("id", value, false);
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x001C0128 File Offset: 0x001BE328
		// (set) Token: 0x06005CC9 RID: 23753 RVA: 0x001C0162 File Offset: 0x001BE362
		public string ID
		{
			get
			{
				string text = this.GetStringProperty("id", null);
				if (text == null)
				{
					text = this.BaseID.ToString();
					this.SetStringProperty("id", text, false);
				}
				return text;
			}
			set
			{
				this.SetStringProperty("id", value, false);
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x001C0171 File Offset: 0x001BE371
		public bool HasID
		{
			get
			{
				return this._BaseID > 0;
			}
		}

		// Token: 0x06005CCB RID: 23755 RVA: 0x001C017C File Offset: 0x001BE37C
		public bool IDMatch(string TestID)
		{
			return TestID != null && this.GetStringProperty("id", null) == TestID;
		}

		// Token: 0x06005CCC RID: 23756 RVA: 0x001C0195 File Offset: 0x001BE395
		public bool IDMatch(int TestID)
		{
			return TestID != 0 && this.BaseID == TestID;
		}

		// Token: 0x06005CCD RID: 23757 RVA: 0x001C01A8 File Offset: 0x001BE3A8
		public bool IDMatch(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			string stringProperty = this.GetStringProperty("id", null);
			return stringProperty != null && Object.IDMatch(stringProperty);
		}

		// Token: 0x06005CCE RID: 23758 RVA: 0x001C01DC File Offset: 0x001BE3DC
		public static GameObject FindByID(string ID)
		{
			if (ID.IsNullOrEmpty())
			{
				return null;
			}
			XRLGame game = XRL.The.Game;
			if (game == null)
			{
				return null;
			}
			if (ID == game.lastFindId && GameObject.Validate(ref game.lastFind) && game.lastFind.IDMatch(game.lastFindId))
			{
				return game.lastFind;
			}
			Zone activeZone = game.ZoneManager.ActiveZone;
			if (activeZone != null)
			{
				GameObject gameObject = activeZone.FindObjectByID(ID);
				if (gameObject != null)
				{
					game.lastFindId = ID;
					game.lastFind = gameObject;
					return gameObject;
				}
			}
			foreach (Zone zone in game.ZoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					GameObject gameObject2 = zone.FindObjectByID(ID);
					if (gameObject2 != null)
					{
						game.lastFindId = ID;
						game.lastFind = gameObject2;
						return gameObject2;
					}
				}
			}
			return null;
		}

		// Token: 0x06005CCF RID: 23759 RVA: 0x001C02D4 File Offset: 0x001BE4D4
		public static GameObject FindByID(int ID)
		{
			if (ID <= 0)
			{
				return null;
			}
			XRLGame game = XRL.The.Game;
			if (game == null)
			{
				return null;
			}
			Zone activeZone = game.ZoneManager.ActiveZone;
			if (activeZone != null)
			{
				GameObject gameObject = activeZone.FindObjectByID(ID);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			foreach (Zone zone in game.ZoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					GameObject gameObject2 = zone.FindObjectByID(ID);
					if (gameObject2 != null)
					{
						return gameObject2;
					}
				}
			}
			return null;
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06005CD0 RID: 23760 RVA: 0x001C0378 File Offset: 0x001BE578
		public string GeneID
		{
			get
			{
				string text = this.GetStringProperty("GeneID", null);
				if (text == null)
				{
					if (XRL.The.Game != null)
					{
						int intGameState = XRL.The.Game.GetIntGameState("NextGeneID", 0);
						XRL.The.Game.SetIntGameState("NextGeneID", intGameState + 1);
						text = intGameState.ToString();
						this.SetStringProperty("GeneID", text, false);
					}
					else
					{
						text = "[pre-game]";
						this.SetStringProperty("GeneID", text, false);
					}
				}
				return text;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06005CD1 RID: 23761 RVA: 0x001C03EA File Offset: 0x001BE5EA
		public bool HasGeneID
		{
			get
			{
				return this.HasStringProperty("GeneID");
			}
		}

		// Token: 0x06005CD2 RID: 23762 RVA: 0x001C03F7 File Offset: 0x001BE5F7
		public bool GeneIDMatch(string TestID)
		{
			return TestID != null && this.GetStringProperty("GeneID", null) == TestID;
		}

		// Token: 0x06005CD3 RID: 23763 RVA: 0x001C0410 File Offset: 0x001BE610
		public bool GeneIDMatch(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			string stringProperty = this.GetStringProperty("GeneID", null);
			return stringProperty != null && Object.GeneIDMatch(stringProperty);
		}

		// Token: 0x06005CD4 RID: 23764 RVA: 0x001C0441 File Offset: 0x001BE641
		public void InjectGeneID(string ID)
		{
			this.SetStringProperty("GeneID", ID, false);
		}

		// Token: 0x06005CD5 RID: 23765 RVA: 0x001C0450 File Offset: 0x001BE650
		public static GameObject FindByBlueprint(string Name)
		{
			if (Name.IsNullOrEmpty())
			{
				return null;
			}
			ZoneManager zoneManager = XRL.The.ZoneManager;
			if (zoneManager == null)
			{
				return null;
			}
			Zone activeZone = zoneManager.ActiveZone;
			if (activeZone != null)
			{
				GameObject gameObject = activeZone.FindFirstObject(Name);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			foreach (Zone zone in zoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					GameObject gameObject2 = zone.FindFirstObject(Name);
					if (gameObject2 != null)
					{
						return gameObject2;
					}
				}
			}
			return null;
		}

		// Token: 0x06005CD6 RID: 23766 RVA: 0x001C04EC File Offset: 0x001BE6EC
		public static GameObject Find(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return null;
			}
			ZoneManager zoneManager = XRL.The.ZoneManager;
			if (zoneManager == null)
			{
				return null;
			}
			Zone activeZone = zoneManager.ActiveZone;
			if (activeZone != null)
			{
				GameObject gameObject = activeZone.FindFirstObject(Filter);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			foreach (Zone zone in zoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					GameObject gameObject2 = zone.FindFirstObject(Filter);
					if (gameObject2 != null)
					{
						return gameObject2;
					}
				}
			}
			return null;
		}

		// Token: 0x06005CD7 RID: 23767 RVA: 0x001C0584 File Offset: 0x001BE784
		public static List<GameObject> FindAll(Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			if (Filter == null)
			{
				return list;
			}
			ZoneManager zoneManager = XRL.The.ZoneManager;
			if (zoneManager == null)
			{
				return null;
			}
			Zone activeZone = zoneManager.ActiveZone;
			if (activeZone != null)
			{
				activeZone.FindObjects(list, Filter);
			}
			foreach (Zone zone in zoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					zone.FindObjects(list, Filter);
				}
			}
			return list;
		}

		// Token: 0x06005CD8 RID: 23768 RVA: 0x001C0610 File Offset: 0x001BE810
		public int? Con(GameObject Target = null, bool IgnoreHideCon = false)
		{
			return DifficultyEvaluation.GetDifficultyRating(this, Target, false);
		}

		// Token: 0x06005CD9 RID: 23769 RVA: 0x001C061C File Offset: 0x001BE81C
		public string GetDirectionToward(Location2D Location, bool General = false)
		{
			if (Location == null)
			{
				return "?";
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return "?";
			}
			if (!General)
			{
				return currentCell.GetDirectionFrom(Location);
			}
			return currentCell.GetGeneralDirectionFrom(Location);
		}

		// Token: 0x06005CDA RID: 23770 RVA: 0x001C065C File Offset: 0x001BE85C
		public string GetDirectionToward(Cell Cell, bool General = false)
		{
			if (Cell == null)
			{
				return "?";
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return "?";
			}
			if (!General)
			{
				return currentCell.GetDirectionFromCell(Cell, false);
			}
			return currentCell.GetGeneralDirectionFromCell(Cell);
		}

		// Token: 0x06005CDB RID: 23771 RVA: 0x001C0695 File Offset: 0x001BE895
		public string GetDirectionToward(GameObject Object, bool General = false)
		{
			return this.GetDirectionToward((Object != null) ? Object.GetCurrentCell() : null, General);
		}

		// Token: 0x06005CDC RID: 23772 RVA: 0x001C06AC File Offset: 0x001BE8AC
		public string DescribeDirectionToward(Location2D L, bool General = false, bool Short = false)
		{
			string directionToward = this.GetDirectionToward(L, General);
			if (Short)
			{
				return Directions.GetDirectionShortDescription(directionToward);
			}
			return Directions.GetDirectionDescription(directionToward);
		}

		// Token: 0x06005CDD RID: 23773 RVA: 0x001C06D4 File Offset: 0x001BE8D4
		public string DescribeDirectionToward(Cell C, bool General = false, bool Short = false)
		{
			string directionToward = this.GetDirectionToward(C, General);
			if (Short)
			{
				return Directions.GetDirectionShortDescription(directionToward);
			}
			return Directions.GetDirectionDescription(directionToward);
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x001C06FA File Offset: 0x001BE8FA
		public string DescribeRelativeDirectionToward(Location2D L, bool General = false)
		{
			return Directions.GetDirectionDescription(this, this.GetDirectionToward(L, General));
		}

		// Token: 0x06005CDF RID: 23775 RVA: 0x001C070A File Offset: 0x001BE90A
		public string DescribeRelativeDirectionToward(Cell C, bool General = false)
		{
			return Directions.GetDirectionDescription(this, this.GetDirectionToward(C, General));
		}

		// Token: 0x06005CE0 RID: 23776 RVA: 0x001C071A File Offset: 0x001BE91A
		public string DescribeDirectionToward(GameObject Object, bool General = false, bool Short = false)
		{
			return this.DescribeDirectionToward((Object != null) ? Object.GetCurrentCell() : null, General, Short);
		}

		// Token: 0x06005CE1 RID: 23777 RVA: 0x001C0730 File Offset: 0x001BE930
		public string DescribeRelativeDirectionToward(GameObject Object, bool General = false)
		{
			return Directions.GetDirectionDescription(this, this.GetDirectionToward(Object, General));
		}

		// Token: 0x06005CE2 RID: 23778 RVA: 0x001C0740 File Offset: 0x001BE940
		public string DescribeDirectionFrom(Location2D L, bool General = false)
		{
			return Directions.GetIncomingDirectionDescription(this.GetDirectionToward(L, General));
		}

		// Token: 0x06005CE3 RID: 23779 RVA: 0x001C074F File Offset: 0x001BE94F
		public string DescribeDirectionFrom(Cell C, bool General = false)
		{
			return Directions.GetIncomingDirectionDescription(this.GetDirectionToward(C, General));
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x001C075E File Offset: 0x001BE95E
		public string DescribeRelativeDirectionFrom(Location2D L, bool General = false)
		{
			return Directions.GetIncomingDirectionDescription(this, this.GetDirectionToward(L, General));
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x001C076E File Offset: 0x001BE96E
		public string DescribeRelativeDirectionFrom(Cell C, bool General = false)
		{
			return Directions.GetIncomingDirectionDescription(this, this.GetDirectionToward(C, General));
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x001C077E File Offset: 0x001BE97E
		public string DescribeDirectionFrom(GameObject Object, bool General = false)
		{
			return Directions.GetIncomingDirectionDescription(this.GetDirectionToward(Object, General));
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x001C078D File Offset: 0x001BE98D
		public string DescribeRelativeDirectionFrom(GameObject Object, bool General = false)
		{
			return Directions.GetIncomingDirectionDescription(this, this.GetDirectionToward(Object, General));
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x001C07A0 File Offset: 0x001BE9A0
		public int Heal(int Amount, bool Message = false, bool FloatText = false, bool RandomMinimum = false)
		{
			int num = 0;
			GameObject.eHealing.SetParameter("Amount", Amount);
			if (this.FireEvent(GameObject.eHealing))
			{
				Statistic stat = this.GetStat("Hitpoints");
				if (stat != null)
				{
					int num2 = GameObject.eHealing.GetIntParameter("Amount", 0);
					if (num2 <= 0 && RandomMinimum && XRL.Rules.Stat.Random(1, 1 + Amount) > 1)
					{
						num2 = 1;
					}
					if (num2 > 0)
					{
						int value = stat.Value;
						stat.Penalty -= num2;
						int value2 = stat.Value;
						if (value != value2)
						{
							num = value2 - value;
							if (Message || FloatText)
							{
								if (num > 0)
								{
									char c = ColorCoding.ConsequentialColorChar(this, null);
									if (Message && this.IsVisible())
									{
										MessageQueue.AddPlayerMessage(string.Concat(new string[]
										{
											this.Does("heal", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											" for ",
											num.ToString(),
											" hit ",
											(Amount == 1) ? "point" : "points",
											"."
										}), c, true);
									}
									if (FloatText)
									{
										this.ParticleText(num.Signed(false), c, false, 1.5f, 24f);
									}
								}
								else
								{
									char c2 = ColorCoding.ConsequentialColorChar(null, this);
									if (Message && this.IsVisible())
									{
										MessageQueue.AddPlayerMessage(string.Concat(new string[]
										{
											this.Does("lose", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											num.ToString(),
											" hit ",
											(num == 1) ? "point" : "points",
											"."
										}), c2, true);
									}
									if (FloatText)
									{
										this.ParticleText(num.ToString(), c2, false, 1.5f, 24f);
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x001C09A4 File Offset: 0x001BEBA4
		public double Health()
		{
			Statistic statistic;
			if (!this.Statistics.TryGetValue("Hitpoints", out statistic))
			{
				return 1.0;
			}
			double value = (double)statistic.Value;
			int baseValue = statistic.BaseValue;
			return value / (double)baseValue;
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x001C09E0 File Offset: 0x001BEBE0
		public bool GoToPartyLeader()
		{
			Brain brain = this.Brain;
			return brain != null && brain.GoToPartyLeader();
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x001C09F3 File Offset: 0x001BEBF3
		public string GetPrimaryFaction(bool Base = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return null;
			}
			return brain.GetPrimaryFaction(Base);
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x001C0A07 File Offset: 0x001BEC07
		public string GetPrimaryFactionName(bool VisibleOnly = true, bool Formatted = true, bool Base = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return null;
			}
			return brain.GetPrimaryFactionName(VisibleOnly, Formatted, Base);
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x001C0A1D File Offset: 0x001BEC1D
		public bool BelongsToFaction(string Faction)
		{
			return !Faction.IsNullOrEmpty() && this.Brain != null && this.Brain.GetPrimaryFaction(false) == Faction;
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x001C0A4A File Offset: 0x001BEC4A
		public bool HasGoal(string GoalName)
		{
			Brain brain = this.Brain;
			return brain != null && brain.HasGoal(GoalName);
		}

		// Token: 0x06005CEF RID: 23791 RVA: 0x001C0A5E File Offset: 0x001BEC5E
		public bool HasGoal()
		{
			Brain brain = this.Brain;
			return brain != null && brain.HasGoal();
		}

		// Token: 0x06005CF0 RID: 23792 RVA: 0x001C0A71 File Offset: 0x001BEC71
		public bool HasGoalOtherThan(string what)
		{
			Brain brain = this.Brain;
			return brain != null && brain.HasGoalOtherThan(what);
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x001C0A85 File Offset: 0x001BEC85
		public bool IsBusy()
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsBusy();
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06005CF2 RID: 23794 RVA: 0x001C0A98 File Offset: 0x001BEC98
		// (set) Token: 0x06005CF3 RID: 23795 RVA: 0x001C0ABD File Offset: 0x001BECBD
		public GameObject Target
		{
			get
			{
				if (this.IsPlayer())
				{
					return Sidebar.CurrentTarget;
				}
				if (this.Brain == null)
				{
					return null;
				}
				return this.Brain.Target;
			}
			set
			{
				if (this.IsPlayer())
				{
					Sidebar.CurrentTarget = value;
					return;
				}
				if (this.Brain != null)
				{
					this.Brain.Target = value;
					return;
				}
			}
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x001C0AE4 File Offset: 0x001BECE4
		public GameObject GetHostilityTarget()
		{
			GameObject target = this.Target;
			if (target == null || !this.IsHostileTowards(target))
			{
				return null;
			}
			return target;
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06005CF5 RID: 23797 RVA: 0x001C0B07 File Offset: 0x001BED07
		public GameObject Equipped
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return null;
				}
				return physics.Equipped;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x001C0B1A File Offset: 0x001BED1A
		public GameObject InInventory
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return null;
				}
				return physics.InInventory;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06005CF7 RID: 23799 RVA: 0x001C0B2D File Offset: 0x001BED2D
		public GameObject Implantee
		{
			get
			{
				CyberneticsBaseItem part = this.GetPart<CyberneticsBaseItem>();
				if (part == null)
				{
					return null;
				}
				return part.ImplantedOn;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06005CF8 RID: 23800 RVA: 0x001C0B40 File Offset: 0x001BED40
		public GameObject Holder
		{
			get
			{
				GameObject gameObject;
				if ((gameObject = this.Equipped) == null)
				{
					gameObject = this.InInventory ?? this.Implantee;
				}
				return gameObject;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06005CF9 RID: 23801 RVA: 0x001C0B5C File Offset: 0x001BED5C
		// (set) Token: 0x06005CFA RID: 23802 RVA: 0x001C0B6F File Offset: 0x001BED6F
		public Cell CurrentCell
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return null;
				}
				return physics.CurrentCell;
			}
			set
			{
				if (this.Physics != null)
				{
					this.Physics.CurrentCell = value;
				}
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06005CFB RID: 23803 RVA: 0x001C0B85 File Offset: 0x001BED85
		public Zone CurrentZone
		{
			get
			{
				Cell currentCell = this.CurrentCell;
				if (currentCell == null)
				{
					return null;
				}
				return currentCell.ParentZone;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06005CFC RID: 23804 RVA: 0x001C0B98 File Offset: 0x001BED98
		// (set) Token: 0x06005CFD RID: 23805 RVA: 0x001C0BAB File Offset: 0x001BEDAB
		public GameObject PartyLeader
		{
			get
			{
				Brain brain = this.Brain;
				if (brain == null)
				{
					return null;
				}
				return brain.PartyLeader;
			}
			set
			{
				if (this.Brain != null)
				{
					this.Brain.PartyLeader = value;
				}
			}
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x001C0BC1 File Offset: 0x001BEDC1
		public void MakeActive(bool Force = false)
		{
			if (Force || this.Brain != null)
			{
				ActionManager actionManager = XRL.The.ActionManager;
				if (actionManager == null)
				{
					return;
				}
				actionManager.AddActiveObject(this);
			}
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x001C0BDE File Offset: 0x001BEDDE
		public void MakeInactive()
		{
			ActionManager actionManager = XRL.The.ActionManager;
			if (actionManager == null)
			{
				return;
			}
			actionManager.RemoveActiveObject(this);
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x001C0BF0 File Offset: 0x001BEDF0
		public void BecomeCompanionOf(GameObject Object, bool Trifling = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.BecomeCompanionOf(Object, Trifling, false);
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x001C0C05 File Offset: 0x001BEE05
		public bool IsUnderSky()
		{
			Zone currentZone = this.CurrentZone;
			return currentZone != null && currentZone.IsOutside();
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x001C0C18 File Offset: 0x001BEE18
		public bool IsFrozen()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.IsFrozen();
		}

		// Token: 0x06005D03 RID: 23811 RVA: 0x001C0C2C File Offset: 0x001BEE2C
		public bool CheckFrozen(bool Telepathic = false, bool Telekinetic = false, bool Silent = false, GameObject Target = null)
		{
			if (this.IsFrozen() && (!Telepathic || ((Target == null) ? (!this.HasPart<Telepathy>()) : (!this.CanMakeTelepathicContactWith(Target)))) && (!Telekinetic || ((Target == null) ? (!this.HasPart<Telekinesis>()) : (!this.CanManipulateTelekinetically(Target)))))
			{
				if (!Silent && this.IsPlayer())
				{
					Popup.ShowFail("You are frozen solid!", true, true, true);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x001C0C9A File Offset: 0x001BEE9A
		public bool IsFreezing()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.IsFreezing();
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x001C0CAD File Offset: 0x001BEEAD
		public bool IsAflame()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.IsAflame();
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x001C0CC0 File Offset: 0x001BEEC0
		public bool IsVaporizing()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.IsVaporizing();
		}

		// Token: 0x06005D07 RID: 23815 RVA: 0x001C0CD4 File Offset: 0x001BEED4
		public bool IsMissingTongue()
		{
			Glotrot glotrot;
			return this.TryGetEffect<Glotrot>(out glotrot) && glotrot.Stage >= 3;
		}

		/// <summary>
		/// Gets how many drams of space you have available for storing a
		/// specified liquid.
		/// </summary>
		// Token: 0x06005D08 RID: 23816 RVA: 0x001C0CF9 File Offset: 0x001BEEF9
		public int GetStorableDrams(string liquidType = "water", GameObject skip = null, List<GameObject> skipList = null, Predicate<GameObject> filter = null, bool safeOnly = true, LiquidVolume liquidVolume = null)
		{
			return GetStorableDramsEvent.GetFor(this, liquidType, skip, skipList, filter, safeOnly, liquidVolume);
		}

		/// <summary>
		/// Gets how many drams of space you have available for autocollecting a
		/// specified liquid.
		/// </summary>
		// Token: 0x06005D09 RID: 23817 RVA: 0x001C0D0A File Offset: 0x001BEF0A
		public int GetAutoCollectDrams(string liquidType = "water", GameObject skip = null, List<GameObject> skipList = null)
		{
			return GetAutoCollectDramsEvent.GetFor(this, liquidType, skip, skipList);
		}

		/// <summary>
		/// Gets whether you have any drams of space available for autocollecting a
		/// specified liquid.
		/// </summary>
		// Token: 0x06005D0A RID: 23818 RVA: 0x001C0D15 File Offset: 0x001BEF15
		public bool AnyAutoCollectDrams(string liquidType = "water", GameObject skip = null, List<GameObject> skipList = null)
		{
			return AnyAutoCollectDramsEvent.Check(this, liquidType, skip, skipList);
		}

		/// <summary>
		/// Gets how many drams of a specified liquid you have usable on hand.
		/// </summary>
		// Token: 0x06005D0B RID: 23819 RVA: 0x001C0D20 File Offset: 0x001BEF20
		public int GetFreeDrams(string liquidType = "water", GameObject skip = null, List<GameObject> skipList = null, Predicate<GameObject> filter = null, bool impureOkay = false)
		{
			return GetFreeDramsEvent.GetFor(this, liquidType, skip, skipList, filter, impureOkay);
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x001C0D30 File Offset: 0x001BEF30
		public bool GiveDrams(int drams, string liquidType = "water", bool auto = false, GameObject skip = null, List<GameObject> skipList = null, Predicate<GameObject> filter = null, List<GameObject> storedIn = null, bool safeOnly = true, LiquidVolume liquidVolume = null)
		{
			return !GiveDramsEvent.Check(this, liquidType, drams, skip, skipList, filter, auto, storedIn, safeOnly, liquidVolume);
		}

		// Token: 0x06005D0D RID: 23821 RVA: 0x001C0D58 File Offset: 0x001BEF58
		public bool UseDrams(int drams, string liquidType = "water", GameObject skip = null, List<GameObject> skipList = null, Predicate<GameObject> filter = null, List<GameObject> trackContainers = null, bool drinking = false)
		{
			return !UseDramsEvent.Check(this, liquidType, drams, skip, skipList, filter, false, trackContainers, drinking);
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x001C0D7C File Offset: 0x001BEF7C
		public bool UseImpureDrams(int drams, string liquidType = "water", GameObject skip = null, List<GameObject> skipList = null, Predicate<GameObject> filter = null, List<GameObject> trackContainers = null, bool drinking = false)
		{
			return !UseDramsEvent.Check(this, liquidType, drams, skip, skipList, filter, true, trackContainers, drinking);
		}

		// Token: 0x06005D0F RID: 23823 RVA: 0x001C0D9E File Offset: 0x001BEF9E
		public bool AllowLiquidCollection(string liquidType = "water", GameObject actor = null)
		{
			return AllowLiquidCollectionEvent.Check(this, actor, liquidType);
		}

		// Token: 0x06005D10 RID: 23824 RVA: 0x001C0DA8 File Offset: 0x001BEFA8
		public bool WantsLiquidCollection(string liquidType = "water", GameObject actor = null)
		{
			return WantsLiquidCollectionEvent.Check(this, actor, liquidType);
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x001C0DB4 File Offset: 0x001BEFB4
		public bool UseEnergy(int Amount, string Type, string Context = null, int? MoveSpeed = null, bool Passive = false)
		{
			int num = 0;
			if (this.IsFreezing() && !this.IsFrozen())
			{
				int num2 = this.Physics.FreezeTemperature - this.Physics.Temperature;
				int num3 = this.Physics.FreezeTemperature - this.Physics.BrittleTemperature;
				num -= num2 * 100 / num3;
			}
			int num4 = 0;
			if (Type != null && Type.Contains("Movement"))
			{
				num4 = Amount / 20;
				if (!this.IsFlying && this.HasStat("MoveSpeed"))
				{
					int num5 = MoveSpeed ?? this.Stat("MoveSpeed", 0);
					if (num5 != 100)
					{
						num += 100 - (int)(100f / ((float)(100 - num5) / 100f + 1f));
					}
				}
			}
			Amount = GetEnergyCostEvent.GetFor(this, Amount, Type, num, 0, num4, Passive);
			if (this.Energy != null)
			{
				Amount = Math.Max(Amount * (900 + XRL.Rules.Stat.Random(0, 200)) / 1000, 0);
				this.Energy.BaseValue -= Amount;
				UseEnergyEvent.Send(this, Amount, Type, Passive);
			}
			return Amount > 0;
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x001C0EDC File Offset: 0x001BF0DC
		public void UseEnergy(int Amount)
		{
			this.UseEnergy(Amount, "None", null, null, false);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06005D13 RID: 23827 RVA: 0x001C0F01 File Offset: 0x001BF101
		public Armor Armor
		{
			get
			{
				return this.GetPart<Armor>();
			}
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x001C0F09 File Offset: 0x001BF109
		public string GetWeaponSkill()
		{
			MeleeWeapon part = this.GetPart<MeleeWeapon>();
			return ((part != null) ? part.Skill : null) ?? "";
		}

		// Token: 0x06005D15 RID: 23829 RVA: 0x001C0F26 File Offset: 0x001BF126
		public bool WillTrade()
		{
			return !this.HasTagOrProperty("NoTrade") && !this.HasTagOrProperty("FugueCopy") && !this.HasTagOrProperty("Nullphase");
		}

		// Token: 0x06005D16 RID: 23830 RVA: 0x001C0F58 File Offset: 0x001BF158
		public int DistanceTo(Location2D cellLocation)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null || currentCell.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			return this.DistanceTo(currentCell.ParentZone.GetCell(cellLocation));
		}

		// Token: 0x06005D17 RID: 23831 RVA: 0x001C0F9C File Offset: 0x001BF19C
		public int DistanceTo(GameObject Object)
		{
			Cell cell = ((Object != null) ? Object.CurrentCell : null);
			if (cell == null || cell.ParentZone == null || cell.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null || currentCell.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			return cell.PathDistanceTo(currentCell);
		}

		// Token: 0x06005D18 RID: 23832 RVA: 0x001C1000 File Offset: 0x001BF200
		public int DistanceTo(Cell C)
		{
			if (C.ParentZone == null || C.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null || currentCell.ParentZone.IsWorldMap())
			{
				return 9999999;
			}
			return C.PathDistanceTo(currentCell);
		}

		// Token: 0x06005D19 RID: 23833 RVA: 0x001C1054 File Offset: 0x001BF254
		public double RealDistanceTo(GameObject Object)
		{
			if (Object == null)
			{
				return 9999999.0;
			}
			Cell currentCell = Object.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null || currentCell.ParentZone.IsWorldMap())
			{
				return 9999999.0;
			}
			Cell currentCell2 = this.CurrentCell;
			if (currentCell2 == null || currentCell2.ParentZone == null || currentCell2.ParentZone.IsWorldMap())
			{
				return 9999999.0;
			}
			return currentCell.RealDistanceTo(currentCell2, true);
		}

		// Token: 0x06005D1A RID: 23834 RVA: 0x001C10C8 File Offset: 0x001BF2C8
		public List<Tuple<Cell, char>> GetLineTo(Cell OC, bool IncludeSolid = true, bool UseTargetability = false)
		{
			if (OC == null)
			{
				return null;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return null;
			}
			if (OC.ParentZone != currentCell.ParentZone)
			{
				return null;
			}
			return currentCell.ParentZone.GetLine(currentCell.X, currentCell.Y, OC.X, OC.Y, IncludeSolid, UseTargetability ? this : null);
		}

		// Token: 0x06005D1B RID: 23835 RVA: 0x001C1121 File Offset: 0x001BF321
		public List<Tuple<Cell, char>> GetLineTo(GameObject Object, bool bIncludeSolid = true, bool UseTargetability = false)
		{
			return this.GetLineTo(Object.CurrentCell, bIncludeSolid, UseTargetability);
		}

		// Token: 0x06005D1C RID: 23836 RVA: 0x001C1134 File Offset: 0x001BF334
		public List<Tuple<Cell, char>> GetLineToNLong(GameObject Object, int N, bool bIncludeSolid = true, bool UseTargetability = false)
		{
			Cell currentCell = Object.CurrentCell;
			if (currentCell == null)
			{
				return null;
			}
			Cell currentCell2 = this.CurrentCell;
			if (currentCell2 == null)
			{
				return null;
			}
			if (currentCell.ParentZone != currentCell2.ParentZone)
			{
				return null;
			}
			return currentCell2.ParentZone.GetLine(currentCell2.X, currentCell2.Y, currentCell.X, currentCell.Y, bIncludeSolid, UseTargetability ? this : null);
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x001C1198 File Offset: 0x001BF398
		public bool HasLOSTo(int x, int y, bool IncludeSolid = true, bool BlackoutStops = false, bool UseTargetability = false, bool CheckOcclusion = true, Predicate<Cell> OverrideBlocking = null)
		{
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.ParentZone.CalculateLOS(currentCell, x, y, IncludeSolid, BlackoutStops, UseTargetability ? this : null, OverrideBlocking, 0, false, CheckOcclusion);
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x001C11D4 File Offset: 0x001BF3D4
		public bool HasLOSTo(Cell C, bool IncludeSolid = true, bool BlackoutStops = false, bool UseTargetability = false, bool CheckOcclusion = true, Predicate<Cell> OverrideBlocking = null)
		{
			if (C == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && C.ParentZone == currentCell.ParentZone && this.HasLOSTo(C.X, C.Y, IncludeSolid, BlackoutStops, UseTargetability, CheckOcclusion, OverrideBlocking);
		}

		// Token: 0x06005D1F RID: 23839 RVA: 0x001C121A File Offset: 0x001BF41A
		public bool HasLOSTo(GameObject Object, bool IncludeSolid = true, bool BlackoutStops = false, bool UseTargetability = false, bool CheckOcclusion = true, Predicate<Cell> OverrideBlocking = null)
		{
			return Object != null && this.HasLOSTo(Object.CurrentCell, IncludeSolid, BlackoutStops, UseTargetability, CheckOcclusion, OverrideBlocking);
		}

		// Token: 0x06005D20 RID: 23840 RVA: 0x001C1238 File Offset: 0x001BF438
		public bool HasUnobstructedLineTo(Cell C, bool BlackoutStops = false, bool UseTargetability = false)
		{
			if (C == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.ParentZone == C.ParentZone && currentCell.ParentZone.HasUnobstructedLineTo(currentCell.X, currentCell.Y, C.X, C.Y, BlackoutStops, UseTargetability ? this : null);
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x001C128F File Offset: 0x001BF48F
		public bool HasUnobstructedLineTo(GameObject Object, bool BlackoutStops = false, bool UseTargetability = false)
		{
			return this.HasUnobstructedLineTo(Object.CurrentCell, BlackoutStops, UseTargetability);
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x001C129F File Offset: 0x001BF49F
		public bool TryUnequip(bool Silent = false, bool SemiForced = false)
		{
			BodyPart bodyPart = this.EquippedOn();
			return bodyPart != null && bodyPart.TryUnequip(Silent, SemiForced, false, false);
		}

		// Token: 0x06005D23 RID: 23843 RVA: 0x001C12B6 File Offset: 0x001BF4B6
		public bool ForceUnequip(bool Silent = false)
		{
			BodyPart bodyPart = this.EquippedOn();
			return bodyPart != null && bodyPart.ForceUnequip(Silent, false, false);
		}

		// Token: 0x06005D24 RID: 23844 RVA: 0x001C12CC File Offset: 0x001BF4CC
		public BodyPart EquippedOn()
		{
			GameObject equipped = this.Equipped;
			if (equipped == null)
			{
				return null;
			}
			Body body = equipped.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindEquippedItem(this);
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x001C12EB File Offset: 0x001BF4EB
		public BodyPart DefaultOrEquippedPart()
		{
			GameObject equipped = this.Equipped;
			if (equipped == null)
			{
				return null;
			}
			Body body = equipped.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindDefaultOrEquippedItem(this);
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x001C130C File Offset: 0x001BF50C
		public bool IsEquippedOnType(string FindType)
		{
			GameObject equipped = this.Equipped;
			bool? flag;
			if (equipped == null)
			{
				flag = null;
			}
			else
			{
				Body body = equipped.Body;
				flag = ((body != null) ? new bool?(body.IsEquippedOnType(this, FindType)) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x001C1358 File Offset: 0x001BF558
		public bool IsEquippedOnCategory(int FindCategory)
		{
			GameObject equipped = this.Equipped;
			bool? flag;
			if (equipped == null)
			{
				flag = null;
			}
			else
			{
				Body body = equipped.Body;
				flag = ((body != null) ? new bool?(body.IsEquippedOnCategory(this, FindCategory)) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x001C13A2 File Offset: 0x001BF5A2
		public bool IsEquippedOnPrimary()
		{
			GameObject equipped = this.Equipped;
			return equipped != null && equipped.HasEquippedOnPrimary(this);
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x001C13B6 File Offset: 0x001BF5B6
		public bool HasEquippedOnPrimary(GameObject Object)
		{
			Body body = this.Body;
			return body != null && body.IsEquippedOnPrimary(Object);
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x001C13CC File Offset: 0x001BF5CC
		public void GainSP(int amount, bool message = true)
		{
			if (message && this.IsPlayer())
			{
				Popup.Show("You gain {{C|" + amount.ToString() + "}} skill points!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.Statistics["SP"].BaseValue += amount;
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x001C1428 File Offset: 0x001BF628
		public void GainEgo(int amount, bool message = true)
		{
			if (message && this.IsPlayer())
			{
				Popup.Show("Your Ego is increased by {{G|" + amount.ToString() + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.GetStat("Ego").BaseValue += amount;
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x001C1480 File Offset: 0x001BF680
		public void LoseEgo(int amount, bool message = true)
		{
			if (message && this.IsPlayer())
			{
				Popup.Show("Your Ego is decreased by {{R|" + amount.ToString() + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.GetStat("Ego").BaseValue -= amount;
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x001C14D8 File Offset: 0x001BF6D8
		public void GainIntelligence(int amount, bool message = true)
		{
			if (message && this.IsPlayer())
			{
				Popup.Show("Your Intelligence is increased by {{G|" + amount.ToString() + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.GetStat("Intelligence").BaseValue += amount;
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x001C1530 File Offset: 0x001BF730
		public void GainWillpower(int amount, bool message = true)
		{
			if (message && this.IsPlayer())
			{
				Popup.Show("Your Willpower is increased by {{G|" + amount.ToString() + "}}!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.GetStat("Willpower").BaseValue += amount;
		}

		// Token: 0x06005D2F RID: 23855 RVA: 0x001C1586 File Offset: 0x001BF786
		public GameObject GetShield(Predicate<GameObject> Filter = null, GameObject Attacker = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetShield(Filter, Attacker);
		}

		// Token: 0x06005D30 RID: 23856 RVA: 0x001C159B File Offset: 0x001BF79B
		public GameObject GetShieldWithHighestAV(Predicate<GameObject> Filter = null, GameObject Attacker = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetShieldWithHighestAV(Filter, Attacker);
		}

		// Token: 0x06005D31 RID: 23857 RVA: 0x001C15B0 File Offset: 0x001BF7B0
		public GameObject GetWeaponOfType(string Type, bool PreferPrimary = false)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetWeaponOfType(Type, false, PreferPrimary);
		}

		// Token: 0x06005D32 RID: 23858 RVA: 0x001C15C6 File Offset: 0x001BF7C6
		public GameObject GetPrimaryWeaponOfType(string Type)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetPrimaryWeaponOfType(Type);
		}

		// Token: 0x06005D33 RID: 23859 RVA: 0x001C15DA File Offset: 0x001BF7DA
		public GameObject GetPrimaryWeaponOfType(string Type, bool AcceptFirstHandForNonHandPrimary)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetPrimaryWeaponOfType(Type, AcceptFirstHandForNonHandPrimary);
		}

		// Token: 0x06005D34 RID: 23860 RVA: 0x001C15EF File Offset: 0x001BF7EF
		public GameObject GetWeapon(Predicate<GameObject> Filter = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetWeapon(Filter);
		}

		// Token: 0x06005D35 RID: 23861 RVA: 0x001C1603 File Offset: 0x001BF803
		public bool HasWeaponOfType(string Type, bool NeedPrimary = false)
		{
			Body body = this.Body;
			return body != null && body.HasWeaponOfType(Type, NeedPrimary);
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x001C1618 File Offset: 0x001BF818
		public bool HasPrimaryWeaponOfType(string Type)
		{
			Body body = this.Body;
			return body != null && body.HasPrimaryWeaponOfType(Type);
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x001C162C File Offset: 0x001BF82C
		public bool HasWeapon(Predicate<GameObject> Filter = null)
		{
			Body body = this.Body;
			return body != null && body.HasWeapon(Filter);
		}

		// Token: 0x06005D38 RID: 23864 RVA: 0x001C1640 File Offset: 0x001BF840
		public void ClearShieldBlocks()
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.ClearShieldBlocks();
		}

		// Token: 0x06005D39 RID: 23865 RVA: 0x001C1654 File Offset: 0x001BF854
		public bool IsImplantedInCategory(int FindCategory)
		{
			GameObject equipped = this.Equipped;
			bool? flag;
			if (equipped == null)
			{
				flag = null;
			}
			else
			{
				Body body = equipped.Body;
				flag = ((body != null) ? new bool?(body.IsImplantedInCategory(this, FindCategory)) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x001C169E File Offset: 0x001BF89E
		public GameObject ReplaceWith(GameObject NewObject)
		{
			this.SplitFromStack();
			ReplaceInContextEvent.Send(this, NewObject);
			this.Obliterate(null, false, null);
			return NewObject;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x001C16B9 File Offset: 0x001BF8B9
		public GameObject ReplaceWith(string NewObject)
		{
			return this.ReplaceWith(GameObject.Create(NewObject));
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x001C16C7 File Offset: 0x001BF8C7
		public string GetSoundTag(string Tag, string Default = null)
		{
			return this.GetPropertyOrTag(Tag, Default);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x001C16D4 File Offset: 0x001BF8D4
		public void PlayWorldSoundTag(string Tag, string Default = null, Cell Source = null, float Volume = 0.5f)
		{
			string text = this.GetTagOrStringProperty(Tag, null).Coalesce(Default);
			if (!text.IsNullOrEmpty())
			{
				if (Source == null)
				{
					Source = this.GetCurrentCell();
				}
				if (Source != null)
				{
					Source.PlayWorldSound(text, Volume, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x001C172C File Offset: 0x001BF92C
		public void PlayCombatSoundTag(string Tag, string Default = null, Cell Source = null, float Volume = 0.5f)
		{
			string text = this.GetTagOrStringProperty(Tag, null).Coalesce(Default);
			if (!text.IsNullOrEmpty())
			{
				if (Source == null)
				{
					Source = this.GetCurrentCell();
				}
				if (Source != null)
				{
					Source.PlayWorldSound(text, Volume, 0f, true, 0f, 1f, 1f, int.MaxValue);
				}
			}
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x001C1781 File Offset: 0x001BF981
		public bool Obliterate(string Reason = null, bool Silent = false, string ThirdPersonReason = null)
		{
			return this.Destroy(Reason, Silent, true, ThirdPersonReason);
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x001C1790 File Offset: 0x001BF990
		public bool Destroy(string Reason = null, bool Silent = false, bool Obliterate = false, string ThirdPersonReason = null)
		{
			if (this.IsInGraveyard())
			{
				return true;
			}
			if (!BeforeDestroyObjectEvent.Check(this, Obliterate, Silent, Reason, ThirdPersonReason))
			{
				return false;
			}
			if (this.IsPlayer())
			{
				if (this.HasEffect<Dominated>())
				{
					MetricsManager.LogInfo("Player dominating something when it was destroyed");
					XRLCore.Core.RenderBase(true, false);
					Achievement.WINKED_OUT.Unlock();
					if (CheckpointingSystem.ShowDeathMessage("Your mind winks out of existence.", null))
					{
						return true;
					}
					if (Options.AllowReallydie && Popup.ShowYesNo("DEBUG: Do you really want to die?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
					{
						this.RemoveEffect<Dominated>();
					}
					else
					{
						XRLCore.Core.Game.DeathReason = "Your mind winked out of existence.";
					}
				}
				else
				{
					MetricsManager.LogInfo("PlayerDestroyed (probably alright but just in case)", Environment.StackTrace);
					XRLCore.Core.RenderBase(true, false);
					if (CheckpointingSystem.ShowDeathMessage("You die! (good job)", null))
					{
						return true;
					}
					if (Options.AllowReallydie && Popup.ShowYesNo("DEBUG: Do you really want to die?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
					{
						this.RestorePristineHealth(false, false);
						return false;
					}
					XRLCore.Core.Game.DeathReason = Reason ?? ("You were " + (Obliterate ? "obliterated" : "destroyed"));
				}
				if (this.IsPlayer())
				{
					XRLCore.Core.Game.Running = false;
					return true;
				}
			}
			else if (this.Brain != null && XRL.The.Player != null && this.Brain.PartyLeader == XRL.The.Player && !this.IsTrifling)
			{
				string propertyOrTag = this.GetPropertyOrTag("CustomDeathVerb", "died");
				string text = null;
				if (!this.HasTagOrProperty("CustomDeathVerb"))
				{
					if (!ThirdPersonReason.IsNullOrEmpty())
					{
						text = ThirdPersonReason.Replace("@@", "");
						text = Regex.Replace(text, "##.*?##", "");
						string text2 = "by " + this.ThePlayer.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, false, false, null, false, null, false);
						text = text.Replace(text2, "by you");
					}
					else
					{
						text = GameText.RoughConvertSecondPersonToThirdPerson(Reason, this);
					}
				}
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append("Your companion, ").Append(this.HasProperName ? this.BaseDisplayName : Grammar.A(this.BaseDisplayName, false)).Append(", ")
					.Append(propertyOrTag)
					.Append('.');
				if (!text.IsNullOrEmpty())
				{
					stringBuilder.Append(' ').Append(text);
				}
				if (this.HasTagOrProperty("NoFollowerDeathPopup"))
				{
					MessageQueue.AddPlayerMessage(stringBuilder.ToString(), null, true);
				}
				else
				{
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (this.Energy != null)
			{
				this.Energy.BaseValue = 0;
			}
			OnDestroyObjectEvent.Send(this, Obliterate, Silent, Reason, ThirdPersonReason);
			if (this == Sidebar.CurrentTarget)
			{
				Sidebar.CurrentTarget = null;
			}
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics != null)
			{
				physics.TeardownForDestroy(true, Silent);
			}
			return true;
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x001C1A70 File Offset: 0x001BFC70
		public GameObject DeepCopy(bool CopyEffects = false, bool CopyID = false, Func<GameObject, GameObject> MapInv = null)
		{
			GameObject gameObject = GameObject.Get();
			try
			{
				this.FireEvent(CopyEffects ? "BeforeDeepCopyWithEffects" : "BeforeDeepCopyWithoutEffects");
				gameObject.DeepCopyInventoryObjectMap = new Dictionary<GameObject, GameObject>();
				gameObject.Blueprint = this.Blueprint;
				gameObject.GenderName = this.GenderName;
				gameObject.PronounSetName = this.PronounSetName;
				gameObject.Flags = this.Flags;
				if (CopyID)
				{
					gameObject._BaseID = this._BaseID;
				}
				foreach (string text in this.Property.Keys)
				{
					if (!(text == "id") || CopyID)
					{
						gameObject.Property.Add(text, this.Property[text]);
					}
				}
				foreach (string text2 in this.IntProperty.Keys)
				{
					gameObject.IntProperty.Add(text2, this.IntProperty[text2]);
				}
				foreach (string text3 in this.Statistics.Keys)
				{
					Statistic statistic = new Statistic(this.Statistics[text3]);
					statistic.Owner = gameObject;
					gameObject.Statistics.Add(text3, statistic);
				}
				if (CopyEffects)
				{
					using (Container<Effect>.Enumerator enumerator4 = this.Effects.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							Effect effect = enumerator4.Current;
							if (effect != null)
							{
								gameObject.Effects.Add(effect.DeepCopy(gameObject, MapInv));
							}
						}
						goto IL_0300;
					}
				}
				foreach (Effect effect2 in this.Effects)
				{
					if (effect2 != null)
					{
						if (effect2.allowCopyOnNoEffectDeepCopy())
						{
							gameObject.Effects.Add(effect2.DeepCopy(gameObject, MapInv));
						}
						else
						{
							foreach (KeyValuePair<string, Statistic> keyValuePair in gameObject.Statistics)
							{
								if (keyValuePair.Value.Shifts != null && effect2._StatShifter != null && effect2._StatShifter.ActiveShifts != null)
								{
									foreach (KeyValuePair<string, Dictionary<string, Guid>> keyValuePair2 in effect2._StatShifter.ActiveShifts)
									{
										foreach (KeyValuePair<string, Guid> keyValuePair3 in keyValuePair2.Value)
										{
											keyValuePair.Value.RemoveShift(keyValuePair3.Value);
										}
									}
								}
							}
						}
					}
				}
				IL_0300:
				for (int i = 0; i < this.PartsList.Count; i++)
				{
					IPart part = this.PartsList[i].DeepCopy(gameObject, MapInv);
					if (part != null)
					{
						gameObject.AddPartInternals(part, false, false, true);
					}
					else if (this.PartsList[i].HasStatShifts())
					{
						foreach (KeyValuePair<string, Statistic> keyValuePair4 in gameObject.Statistics)
						{
							foreach (KeyValuePair<string, Dictionary<string, Guid>> keyValuePair5 in this.PartsList[i].StatShifter.ActiveShifts)
							{
								foreach (KeyValuePair<string, Guid> keyValuePair6 in keyValuePair5.Value)
								{
									keyValuePair4.Value.RemoveShift(keyValuePair6.Value);
								}
							}
						}
					}
				}
				gameObject.Energy = gameObject.Statistics.GetValue("Energy", null);
				if (this.RegisteredPartEvents != null)
				{
					gameObject.RegisteredPartEvents = new Dictionary<string, List<IPart>>();
					using (Dictionary<string, List<IPart>>.Enumerator enumerator8 = this.RegisteredPartEvents.GetEnumerator())
					{
						while (enumerator8.MoveNext())
						{
							KeyValuePair<string, List<IPart>> keyValuePair7 = enumerator8.Current;
							string text4;
							List<IPart> list;
							keyValuePair7.Deconstruct(out text4, out list);
							string text5 = text4;
							List<IPart> list2 = list;
							List<IPart> list3 = new List<IPart>(list2.Count);
							gameObject.RegisteredPartEvents.Add(text5, list3);
							foreach (IPart part2 in list2)
							{
								GameObject gameObject2;
								if (part2.ParentObject == this)
								{
									IPart part3 = gameObject.GetPart(part2.Name);
									if (part3 != null)
									{
										list3.Add(part3);
									}
									else
									{
										MetricsManager.LogError("Null registration part for deep copy: " + part2.Name + "." + text5);
									}
								}
								else if (part2.ParentObject != null && gameObject.DeepCopyInventoryObjectMap.TryGetValue(part2.ParentObject, out gameObject2))
								{
									IPart part4 = gameObject2.GetPart(part2.Name);
									if (part4 != null)
									{
										list3.Add(part4);
									}
									else
									{
										MetricsManager.LogError("Null registration part for deep copy: " + part2.Name + "." + text5);
									}
								}
							}
						}
						goto IL_058A;
					}
				}
				gameObject.RegisteredPartEvents = null;
				IL_058A:
				gameObject.FinalizeCopy(this, CopyEffects, CopyID, null);
				if (gameObject.PartsList != null && !CopyID && this.ID != gameObject.ID)
				{
					foreach (IPart part5 in gameObject.PartsList)
					{
						if (part5 != null && part5.HasStatShifts() && part5.StatShifter.ActiveShifts.ContainsKey(this.ID))
						{
							if (!part5.StatShifter.ActiveShifts.ContainsKey(gameObject.ID))
							{
								part5.StatShifter.ActiveShifts.Add(gameObject.ID, new Dictionary<string, Guid>());
							}
							try
							{
								part5.StatShifter.ActiveShifts[gameObject.ID].AddRange<string, Guid>(part5.StatShifter.ActiveShifts[this.ID]);
								part5.StatShifter.ActiveShifts.Remove(this.ID);
							}
							catch (Exception ex)
							{
								MetricsManager.LogException("Clone PartsList", ex, "game_exception");
							}
						}
					}
				}
				List<GameObject> list4 = ((gameObject != null) ? gameObject.GetInventoryAndEquipmentAndDefaultEquipment() : null);
				if (list4 != null && !CopyID && this.ID != gameObject.ID)
				{
					foreach (GameObject gameObject3 in list4)
					{
						if (((gameObject3 != null) ? gameObject3.PartsList : null) != null)
						{
							foreach (IPart part6 in gameObject3.PartsList)
							{
								if (part6 != null && part6.HasStatShifts() && part6.StatShifter.ActiveShifts.ContainsKey(this.ID))
								{
									if (!part6.StatShifter.ActiveShifts.ContainsKey(gameObject.ID))
									{
										part6.StatShifter.ActiveShifts.Add(gameObject.ID, new Dictionary<string, Guid>());
									}
									try
									{
										part6.StatShifter.ActiveShifts[gameObject.ID].AddRange<string, Guid>(part6.StatShifter.ActiveShifts[this.ID]);
										part6.StatShifter.ActiveShifts.Remove(this.ID);
									}
									catch (Exception ex2)
									{
										MetricsManager.LogException("Clone Inventory", ex2, "game_exception");
									}
								}
							}
						}
					}
				}
				if (gameObject.Effects != null && !CopyID && this.ID != gameObject.ID)
				{
					foreach (Effect effect3 in gameObject.Effects)
					{
						if (effect3 != null && effect3.HasStatShifts() && effect3.StatShifter.ActiveShifts.ContainsKey(this.ID))
						{
							if (!effect3.StatShifter.ActiveShifts.ContainsKey(gameObject.ID))
							{
								effect3.StatShifter.ActiveShifts.Add(gameObject.ID, new Dictionary<string, Guid>());
							}
							try
							{
								effect3.StatShifter.ActiveShifts[gameObject.ID].AddRange<string, Guid>(effect3.StatShifter.ActiveShifts[this.ID]);
								effect3.StatShifter.ActiveShifts.Remove(this.ID);
							}
							catch (Exception ex3)
							{
								MetricsManager.LogException("Clone Effects", ex3, "game_exception");
							}
						}
					}
				}
				gameObject.DeepCopyInventoryObjectMap = null;
				gameObject.ResetNameCache();
			}
			finally
			{
				this.FireEvent(CopyEffects ? "AfterDeepCopyWithEffects" : "AfterDeepCopyWithoutEffects");
			}
			return gameObject;
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x001C25F4 File Offset: 0x001C07F4
		public void FinalizeCopy(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv = null)
		{
			List<IPart> list = new List<IPart>(this.PartsList.Count + (8 - this.PartsList.Count % 8));
			list.Clear();
			list.AddRange(this.PartsList);
			foreach (IPart part in list)
			{
				if (this.PartsList.Contains(part))
				{
					part.FinalizeCopyEarly(Source, CopyEffects, CopyID, MapInv);
				}
			}
			list.Clear();
			list.AddRange(this.PartsList);
			foreach (IPart part2 in list)
			{
				if (this.PartsList.Contains(part2))
				{
					part2.FinalizeCopy(Source, CopyEffects, CopyID, MapInv);
				}
			}
			list.Clear();
			list.AddRange(this.PartsList);
			foreach (IPart part3 in list)
			{
				if (this.PartsList.Contains(part3))
				{
					part3.FinalizeCopyLate(Source, CopyEffects, CopyID, MapInv);
				}
			}
			list.Clear();
			list.AddRange(this.PartsList);
			foreach (IPart part4 in list)
			{
				if (this.PartsList.Contains(part4))
				{
					part4.ApplyRegistrar(this, false);
				}
			}
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x001C27B0 File Offset: 0x001C09B0
		public void WasUnstackedFrom(GameObject Object)
		{
			if (this.Effects != null)
			{
				int i = 0;
				int num = this.Effects.Count;
				while (i < num)
				{
					Effect effect = this.Effects[i];
					effect.WasUnstackedFrom(Object);
					if (num != this.Effects.Count)
					{
						num = this.Effects.Count;
						if (i < num && this.Effects[i] != effect)
						{
							i--;
						}
					}
					i++;
				}
			}
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x001C2824 File Offset: 0x001C0A24
		public bool StripContents(bool KeepNatural = false, bool Silent = false)
		{
			StripContentsEvent stripContentsEvent = StripContentsEvent.FromPool(this, KeepNatural, Silent);
			bool flag = this.HandleEvent(stripContentsEvent);
			PooledEvent<StripContentsEvent>.ResetTo(ref stripContentsEvent);
			return flag;
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x001C2848 File Offset: 0x001C0A48
		public GameObjectBlueprint GetBlueprint(bool UseDefault = true)
		{
			if (this._BlueprintCache != null || GameObjectFactory.Factory.Blueprints.TryGetValue(this.Blueprint, out this._BlueprintCache))
			{
				return this._BlueprintCache;
			}
			if (!UseDefault)
			{
				return null;
			}
			MetricsManager.LogError(new Exception("GameObject::GetBlueprint() Unknown Blueprint " + this.Blueprint));
			return GameObjectFactory.Factory.Blueprints["Object"];
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x001C28B4 File Offset: 0x001C0AB4
		public void SetBlueprint(GameObjectBlueprint Blueprint)
		{
			this.Blueprint = Blueprint.Name;
			this._BlueprintCache = Blueprint;
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x001C28CC File Offset: 0x001C0ACC
		public void CombatJuiceWait(float t)
		{
			int num = (int)(t * 1000f / 10f);
			if (GameObject.lastWaitTurn != XRL.The.Game.Turns)
			{
				Keyboard.ClearInput(false, false);
				GameObject.lastWaitTurn = XRL.The.Game.Turns;
			}
			int num2 = 0;
			while (num2 < num && !Keyboard.kbhit())
			{
				Thread.Sleep(10);
				num2++;
			}
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x001C2929 File Offset: 0x001C0B29
		public ActivatedAbilities RequireAbilities()
		{
			if (this.Abilities == null)
			{
				this.AddPart<ActivatedAbilities>(new ActivatedAbilities(), true, false);
			}
			return this.Abilities;
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x001C2947 File Offset: 0x001C0B47
		public void SetAlliedLeader<T>(GameObject Object, int Flags = 0, bool Silent = false) where T : IAllyReason, new()
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.SetAlliedLeader<T>(Object, Flags, Silent);
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x001C295C File Offset: 0x001C0B5C
		public void TakeAllegiance<T>(GameObject Object) where T : IAllyReason, new()
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.TakeAllegiance<T>(Object);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x001C296F File Offset: 0x001C0B6F
		public void TakeBaseAllegiance(GameObject Object)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.TakeBaseAllegiance(Object);
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x001C2982 File Offset: 0x001C0B82
		public void TakeDemeanor(GameObject Object)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.TakeDemeanor(Object);
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x001C2995 File Offset: 0x001C0B95
		public void CopyTarget(GameObject Object)
		{
			if (this.Brain != null)
			{
				Brain brain = this.Brain;
				GameObject gameObject;
				if (Object == null)
				{
					gameObject = null;
				}
				else
				{
					Brain brain2 = Object.Brain;
					gameObject = ((brain2 != null) ? brain2.Target : null);
				}
				brain.Target = gameObject;
			}
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x001C29C2 File Offset: 0x001C0BC2
		public void CopyLeader(GameObject Object)
		{
			if (this.Brain != null)
			{
				Brain brain = this.Brain;
				GameObject gameObject;
				if (Object == null)
				{
					gameObject = null;
				}
				else
				{
					Brain brain2 = Object.Brain;
					gameObject = ((brain2 != null) ? brain2.PartyLeader : null);
				}
				brain.PartyLeader = gameObject;
			}
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x001C29F0 File Offset: 0x001C0BF0
		public bool SupportsFollower(GameObject Object)
		{
			Brain brain = this.Brain;
			bool? flag;
			if (brain == null)
			{
				flag = null;
			}
			else
			{
				PartyCollection partyMembers = brain.PartyMembers;
				flag = ((partyMembers != null) ? new bool?(partyMembers.ContainsKey(Object.BaseID)) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x001C2A40 File Offset: 0x001C0C40
		public bool SupportsFollower(GameObject Object, int Mask)
		{
			PartyMember partyMember;
			return this.Brain != null && this.Brain.PartyMembers.TryGetValue(Object.BaseID, out partyMember) && partyMember.Flags.HasBit(Mask);
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x001C2A7F File Offset: 0x001C0C7F
		public Brain.AllegianceLevel GetAllegianceLevel(string Faction)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return Brain.AllegianceLevel.None;
			}
			return brain.GetAllegianceLevel(Faction);
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x001C2A93 File Offset: 0x001C0C93
		public bool IsMemberOfFaction(string Faction)
		{
			return this.GetAllegianceLevel(Faction) > Brain.AllegianceLevel.None;
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x001C2A9F File Offset: 0x001C0C9F
		public bool TakeOnAttitudesOf(GameObject Object, bool CopyLeader = false, bool CopyTarget = false)
		{
			Brain brain = this.Brain;
			return brain != null && brain.TakeOnAttitudesOf(Object, CopyLeader, CopyTarget);
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x001C2AB5 File Offset: 0x001C0CB5
		public bool Owns(string Owner)
		{
			return !Owner.IsNullOrEmpty() && this.GetAllegianceLevel(Owner) == Brain.AllegianceLevel.Member;
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x001C2ACB File Offset: 0x001C0CCB
		public bool Owns(GameObject Object)
		{
			return this.Owns((Object != null) ? Object.Owner : null);
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x001C2ADF File Offset: 0x001C0CDF
		public bool IsNatural()
		{
			return this.HasPropertyOrTag("Natural") || this.HasPropertyOrTag("NaturalGear");
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x001C2AFC File Offset: 0x001C0CFC
		private string GetDirectionFromCellXY(int X, int Y, bool showCenter = false)
		{
			if (Y == 0)
			{
				if (X == 0)
				{
					return "NW";
				}
				if (X == 1)
				{
					return "N";
				}
				if (X == 2)
				{
					return "NE";
				}
			}
			else if (Y == 2)
			{
				if (X == 0)
				{
					return "SW";
				}
				if (X == 1)
				{
					return "S";
				}
				if (X == 2)
				{
					return "SE";
				}
			}
			else
			{
				if (X == 0)
				{
					return "W";
				}
				if (X == 2)
				{
					return "E";
				}
			}
			if (!showCenter)
			{
				return null;
			}
			return "C";
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x001C2B68 File Offset: 0x001C0D68
		public void PullDown(bool AllowAlternate = false)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			if (!currentCell.OnWorldMap())
			{
				return;
			}
			if (!this.IsPlayer())
			{
				return;
			}
			if (this.HasTagOrProperty("DisallowAlternatePullDown"))
			{
				AllowAlternate = false;
			}
			XRLGame game = XRLCore.Core.Game;
			GameObject firstObjectWithPart = currentCell.GetFirstObjectWithPart(typeof(TerrainNotes));
			string zoneWorld = game.ZoneManager.ActiveZone.GetZoneWorld();
			string stringGameState = XRLCore.Core.Game.GetStringGameState("LastLocationOnSurface", "");
			int x = currentCell.X;
			int y = currentCell.Y;
			string text = null;
			JournalMapNote journalMapNote = null;
			Point3D landingLocation = game.ZoneManager.GetLandingLocation(zoneWorld, x, y);
			int num = landingLocation.x;
			int num2 = landingLocation.y;
			int z = landingLocation.z;
			if (!BeforeTravelDownEvent.Check(this, zoneWorld, currentCell, firstObjectWithPart, new Point3D?(landingLocation)))
			{
				return;
			}
			if (AllowAlternate)
			{
				List<string> list = new List<string>();
				List<char> list2 = new List<char>();
				List<GameObject.PullDownChoice> list3 = new List<GameObject.PullDownChoice>();
				char c = 'a';
				if (!stringGameState.IsNullOrEmpty())
				{
					string text2 = "Current location";
					Cell cell = Cell.FromAddress(stringGameState);
					if (cell != null)
					{
						Zone parentZone = cell.ParentZone;
						int num3;
						int num4;
						int num5;
						int num6;
						if (ZoneID.Parse((parentZone != null) ? parentZone.ZoneID : null, out num3, out num4, out num5, out num6))
						{
							this.GetDirectionFromCellXY(num5, num6, true);
							list.Add(text2);
							List<char> list4 = list2;
							int num7;
							if (c > 'z')
							{
								num7 = 32;
							}
							else
							{
								c = (char)((num7 = (int)c) + 1);
							}
							list4.Add((char)num7);
							list3.Add(new GameObject.PullDownChoice
							{
								location = stringGameState,
								X = num5,
								Y = num6
							});
						}
					}
					if (list.Count == 0)
					{
						list.Add(text2);
						List<char> list5 = list2;
						int num8;
						if (c > 'z')
						{
							num8 = 32;
						}
						else
						{
							c = (char)((num8 = (int)c) + 1);
						}
						list5.Add((char)num8);
						list3.Add(new GameObject.PullDownChoice
						{
							location = stringGameState,
							X = num,
							Y = num2
						});
					}
				}
				list.Add((num == 1 && num2 == 1) ? "Center" : "Arrival location");
				List<char> list6 = list2;
				char c2 = c;
				c = c2 + '\u0001';
				list6.Add(c2);
				list3.Add(new GameObject.PullDownChoice
				{
					X = num,
					Y = num2
				});
				if (firstObjectWithPart != null)
				{
					TerrainNotes part = firstObjectWithPart.GetPart<TerrainNotes>();
					if (part != null && part.notes != null)
					{
						foreach (JournalMapNote journalMapNote2 in part.notes)
						{
							if (journalMapNote2.ZoneZ == 10)
							{
								bool flag = false;
								int i = 0;
								int count = list3.Count;
								while (i < count)
								{
									GameObject.PullDownChoice pullDownChoice = list3[i];
									if (pullDownChoice.location == null && pullDownChoice.X == journalMapNote2.ZoneX && pullDownChoice.Y == journalMapNote2.ZoneY)
									{
										List<string> list7 = list;
										int num9 = i;
										list7[num9] = list7[num9] + ", " + journalMapNote2.GetShortText();
										flag = true;
										break;
									}
									i++;
								}
								if (!flag)
								{
									list.Add(journalMapNote2.GetShortText());
									List<char> list8 = list2;
									char c3 = c;
									c = c3 + '\u0001';
									list8.Add(c3);
									list3.Add(new GameObject.PullDownChoice
									{
										X = journalMapNote2.ZoneX,
										Y = journalMapNote2.ZoneY
									});
								}
							}
						}
					}
				}
				if (list3.Count > 1)
				{
					int j = 0;
					int count2 = list3.Count;
					while (j < count2)
					{
						GameObject.PullDownChoice pullDownChoice2 = list3[j];
						string directionFromCellXY = this.GetDirectionFromCellXY(pullDownChoice2.X, pullDownChoice2.Y, false);
						if (!directionFromCellXY.IsNullOrEmpty())
						{
							List<string> list7 = list;
							int num9 = j;
							list7[num9] = list7[num9] + " (" + directionFromCellXY + ")";
						}
						else
						{
							List<string> list7 = list;
							int num9 = j;
							list7[num9] += " (C)";
						}
						j++;
					}
					int num10 = Popup.PickOption("Select a destination", null, "", "Sounds/UI/ui_notification", list.ToArray(), list2.ToArray(), null, null, null, null, null, 1, 60, 0, -1, true, false, false, true, false, null, null);
					if (num10 < 0)
					{
						return;
					}
					text = list3[num10].location;
					num = list3[num10].X;
					num2 = list3[num10].Y;
				}
				else if (list3.Count == 1)
				{
					text = list3[0].location;
					num = list3[0].X;
					num2 = list3[0].Y;
				}
			}
			if (text != null)
			{
				Cell cell2 = Cell.FromAddress(stringGameState);
				if (cell2 != null)
				{
					this.PlayWorldOrUISound("sfx_worldMap_exit", null);
					this.DirectMoveTo(cell2, 0, false, true, false, null);
					return;
				}
			}
			if (journalMapNote != null)
			{
				num = journalMapNote.ZoneX;
				num2 = journalMapNote.ZoneY;
			}
			Zone zone = XRL.The.ZoneManager.GetZone(zoneWorld, x, y, num, num2, z);
			Cell pullDownLocation = zone.GetPullDownLocation(this);
			if (pullDownLocation == null)
			{
				MetricsManager.LogError("failed to get pulldown location from " + zone.ZoneID);
			}
			else if (stringGameState.IsNullOrEmpty())
			{
				this.PlayWorldOrUISound("sfx_worldMap_exit", null);
				this.SystemMoveTo(pullDownLocation, new int?(0), false, true, false, false);
			}
			else
			{
				this.PlayWorldOrUISound("sfx_worldMap_exit", null);
				this.SystemLongDistanceMoveTo(pullDownLocation, new int?(0), false, true);
			}
			zone.CheckWeather();
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x001C3114 File Offset: 0x001C1314
		public void FinalizeStats()
		{
			this.Energy = this.Statistics.GetValue("Energy", null);
			foreach (Statistic statistic in this.Statistics.Values)
			{
				if (statistic.sValue != "")
				{
					if (statistic.sValue == "*XP")
					{
						float num = (float)this.Statistics["Level"].Value;
						num /= 2f;
						string propertyOrTag = this.GetPropertyOrTag("Role", "Minion");
						if (propertyOrTag == "Minion")
						{
							statistic.BaseValue = (int)(num * 20f);
						}
						else if (propertyOrTag == "Leader")
						{
							statistic.BaseValue = (int)(num * 100f);
						}
						else if (propertyOrTag == "Hero")
						{
							statistic.BaseValue = (int)(num * 200f);
						}
						else
						{
							statistic.BaseValue = (int)(num * 50f);
						}
					}
					else
					{
						if (this.GetPropertyOrTag("Role", "NPC") == "Minion" && (statistic.Name == "Strength" || statistic.Name == "Agility" || statistic.Name == "Toughness" || statistic.Name == "Willpower" || statistic.Name == "Intelligence" || statistic.Name == "Ego"))
						{
							statistic.Boost--;
						}
						int num2 = 0;
						int num3 = 0;
						Statistic statistic2;
						if (this.Statistics.TryGetValue("Level", out statistic2))
						{
							num3 = statistic2.Value / 5 + 1;
						}
						if (statistic.sValue.Contains(","))
						{
							string[] array = statistic.sValue.Split(',', StringSplitOptions.None);
							for (int i = 0; i < array.Length; i++)
							{
								if (array[i].Contains("("))
								{
									if (array[i].Contains("(t)"))
									{
										array[i] = array[i].Replace("(t)", num3.ToString());
									}
									if (array[i].Contains("(t-1)"))
									{
										array[i] = array[i].Replace("(t-1)", (num3 - 1).ToString());
									}
									if (array[i].Contains("(t+1)"))
									{
										array[i] = array[i].Replace("(t+1)", (num3 + 1).ToString());
									}
									array[i] = array[i].Replace("(v)", statistic.BaseValue.ToString());
								}
							}
							for (int j = 0; j < array.Length; j++)
							{
								num2 += array[j].RollCached();
							}
						}
						else
						{
							string text = statistic.sValue;
							if (text.Contains("("))
							{
								if (text.Contains("(t)"))
								{
									text = text.Replace("(t)", num3.ToString());
								}
								if (text.Contains("(t-1)"))
								{
									text = text.Replace("(t-1)", (num3 - 1).ToString());
								}
								if (text.Contains("(t+1)"))
								{
									text = text.Replace("(t+1)", (num3 + 1).ToString());
								}
								text = text.Replace("(v)", statistic.BaseValue.ToString());
							}
							num2 += text.RollCached();
						}
						statistic.BaseValue = num2;
						if (statistic.Boost > 0)
						{
							statistic.BaseValue += (int)Math.Ceiling((double)((float)num2 * 0.25f * (float)statistic.Boost));
						}
						else
						{
							statistic.BaseValue += (int)Math.Ceiling((double)((float)num2 * 0.2f * (float)statistic.Boost));
						}
					}
				}
			}
			Statistic statistic3;
			Statistic statistic4;
			if (this.Statistics.TryGetValue("XP", out statistic3) && this.Statistics.TryGetValue("Level", out statistic4))
			{
				statistic3.BaseValue = Leveler.GetXPForLevel(statistic4.Value);
			}
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x001C359C File Offset: 0x001C179C
		public bool HasLongProperty(string Name)
		{
			string text;
			if (Name != null && this.Property.TryGetValue(Name, out text))
			{
				try
				{
					Convert.ToInt64(text);
					return true;
				}
				catch
				{
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x001C35E0 File Offset: 0x001C17E0
		public long GetLongProperty(string Name, long Default = 0L)
		{
			string text;
			if (Name != null && this.Property.TryGetValue(Name, out text))
			{
				try
				{
					return Convert.ToInt64(text);
				}
				catch
				{
				}
				return Default;
			}
			return Default;
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x001C3620 File Offset: 0x001C1820
		public string GetStringProperty(string Name, string Default = null)
		{
			string text;
			if (this.Property.TryGetValue(Name, out text))
			{
				return text;
			}
			return Default;
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x001C3640 File Offset: 0x001C1840
		public void SetStringProperty(string Name, string Value, bool RemoveIfNull = false)
		{
			if (Name != null)
			{
				if (RemoveIfNull && Value == null)
				{
					this.Property.Remove(Name);
					return;
				}
				this.Property[Name] = Value;
			}
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x001C3666 File Offset: 0x001C1866
		public void RemoveStringProperty(string Name)
		{
			if (Name != null)
			{
				this.Property.Remove(Name);
			}
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x001C3678 File Offset: 0x001C1878
		public void DeleteStringProperty(string sProperty)
		{
			this.RemoveStringProperty(sProperty);
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x001C3681 File Offset: 0x001C1881
		public bool TryGetStringProperty(string Name, out string Result)
		{
			return this.Property.TryGetValue(Name, out Result);
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x001C3690 File Offset: 0x001C1890
		public int GetIntProperty(string Name, int Default = 0)
		{
			if (Name == null)
			{
				return Default;
			}
			int num;
			if (this.IntProperty.TryGetValue(Name, out num))
			{
				return num;
			}
			return Default;
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x001C36B8 File Offset: 0x001C18B8
		public int? GetIntPropertyIfSet(string Name)
		{
			int num;
			if (Name != null && this.IntProperty.TryGetValue(Name, out num))
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x001C36E8 File Offset: 0x001C18E8
		public bool TryGetIntProperty(string Name, out int Result)
		{
			return this.IntProperty.TryGetValue(Name, out Result);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x001C36F7 File Offset: 0x001C18F7
		public void SetLongProperty(string sProperty, long Value)
		{
			if (!this.Property.ContainsKey(sProperty))
			{
				this.Property.Add(sProperty, Value.ToString());
				return;
			}
			this.Property[sProperty] = Value.ToString();
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x001C3730 File Offset: 0x001C1930
		public bool canPathTo(Cell TC, bool Global = false)
		{
			if (TC == null || TC.ParentZone == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.ParentZone != null && new FindPath(currentCell.ParentZone.ZoneID, currentCell.X, currentCell.Y, TC.ParentZone.ZoneID, TC.X, TC.Y, Global, false, this, false, false, false, false, 100).Usable;
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x001C379F File Offset: 0x001C199F
		public GameObject SetIntProperty(string Name, int Value, bool RemoveIfZero = false)
		{
			if (!RemoveIfZero || Value != 0)
			{
				this.IntProperty[Name] = Value;
			}
			else
			{
				this.IntProperty.Remove(Name);
			}
			return this;
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x001C37C4 File Offset: 0x001C19C4
		public int ModIntProperty(string Name, int Value, bool RemoveIfZero = false)
		{
			int num;
			if (!this.IntProperty.TryGetValue(Name, out num))
			{
				if (!RemoveIfZero || Value != 0)
				{
					this.IntProperty.Add(Name, Value);
				}
				return Value;
			}
			int num2 = num + Value;
			if (!RemoveIfZero || num2 != 0)
			{
				this.IntProperty[Name] = num2;
			}
			else
			{
				this.IntProperty.Remove(Name);
			}
			return num2;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x001C381D File Offset: 0x001C1A1D
		public void RemoveIntProperty(string Name)
		{
			if (Name != null)
			{
				this.IntProperty.Remove(Name);
			}
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001C382F File Offset: 0x001C1A2F
		public void RemoveProperty(string Name)
		{
			this.RemoveStringProperty(Name);
			this.RemoveIntProperty(Name);
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x001C3840 File Offset: 0x001C1A40
		public int GetStatValue(string Stat, int DefaultValue = 0)
		{
			Statistic statistic;
			if (this.Statistics != null && this.Statistics.TryGetValue(Stat, out statistic))
			{
				return statistic.Value;
			}
			return DefaultValue;
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x001C386D File Offset: 0x001C1A6D
		public bool CanGainMP()
		{
			return this.Statistics.ContainsKey("MP");
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x001C3880 File Offset: 0x001C1A80
		public bool GainMP(int amount)
		{
			Statistic statistic;
			if (this.Statistics.TryGetValue("MP", out statistic))
			{
				statistic.BaseValue += amount;
				this.FireEvent(Event.New("GainedMP", "Amount", amount));
				return true;
			}
			return false;
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x001C38CC File Offset: 0x001C1ACC
		public bool UseMP(int amount, string context = "default")
		{
			Statistic statistic;
			if (this.Statistics.TryGetValue("MP", out statistic))
			{
				statistic.Penalty += amount;
				this.FireEvent(Event.New("UsedMP", "Amount", amount, "Context", context));
				return true;
			}
			return false;
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06005D6E RID: 23918 RVA: 0x001C391C File Offset: 0x001C1B1C
		public int Speed
		{
			get
			{
				Statistic statistic;
				if (this.Statistics.TryGetValue("Speed", out statistic))
				{
					return statistic.Value;
				}
				return 0;
			}
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x001C3945 File Offset: 0x001C1B45
		public bool HasStat(string Name)
		{
			return this.Statistics != null && this.Statistics.ContainsKey(Name);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x001C3960 File Offset: 0x001C1B60
		public Statistic GetStat(string Name)
		{
			if (Name.IsNullOrEmpty())
			{
				return null;
			}
			Statistic statistic;
			if (this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				return statistic;
			}
			return null;
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x001C3994 File Offset: 0x001C1B94
		public int Stat(string Name, int Default = 0)
		{
			if (Name.IsNullOrEmpty() || this.Statistics == null)
			{
				return Default;
			}
			Statistic statistic;
			if (this.Statistics.TryGetValue(Name, out statistic))
			{
				return statistic.Value;
			}
			return Default;
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x001C39CC File Offset: 0x001C1BCC
		public int BaseStat(string Name, int Default = 0)
		{
			if (Name.IsNullOrEmpty() || this.Statistics == null)
			{
				return Default;
			}
			Statistic statistic;
			if (this.Statistics.TryGetValue(Name, out statistic))
			{
				return statistic.BaseValue;
			}
			return Default;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x001C3A04 File Offset: 0x001C1C04
		public int StatMod(string Name, int Default = 0)
		{
			if (Name.IsNullOrEmpty() || this.Statistics == null)
			{
				return Default;
			}
			if (Name.IndexOf(',') != -1)
			{
				int num = int.MinValue;
				bool flag = false;
				foreach (string text in Name.CachedCommaExpansion())
				{
					Statistic statistic;
					if (this.Statistics.TryGetValue(text, out statistic))
					{
						flag = true;
						int modifier = statistic.Modifier;
						if (modifier > num)
						{
							num = modifier;
						}
					}
				}
				if (!flag)
				{
					return Default;
				}
				return num;
			}
			else
			{
				Statistic statistic2;
				if (this.Statistics.TryGetValue(Name, out statistic2))
				{
					return statistic2.Modifier;
				}
				return Default;
			}
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x001C3AB8 File Offset: 0x001C1CB8
		public void AddStatBonus(string Name, int Amount)
		{
			Statistic statistic;
			if (!Name.IsNullOrEmpty() && this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				statistic.Bonus += Amount;
			}
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x001C3AF4 File Offset: 0x001C1CF4
		public void AddStatPenalty(string Name, int Amount)
		{
			Statistic statistic;
			if (!Name.IsNullOrEmpty() && this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				statistic.Penalty += Amount;
			}
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x001C3B30 File Offset: 0x001C1D30
		public void AddBaseStat(string Name, int Amount)
		{
			Statistic statistic;
			if (!Name.IsNullOrEmpty() && this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				statistic.BaseValue += Amount;
			}
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x001C3B6C File Offset: 0x001C1D6C
		public void BoostStat(string Name, int Amount)
		{
			Statistic statistic;
			if (!Name.IsNullOrEmpty() && this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				statistic.BoostStat(Amount);
			}
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x001C3BA0 File Offset: 0x001C1DA0
		public void BoostStat(string Name, double Amount)
		{
			Statistic statistic;
			if (!Name.IsNullOrEmpty() && this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				statistic.BoostStat(Amount);
			}
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x001C3BD4 File Offset: 0x001C1DD4
		public void MultiplyStat(string Name, int Factor)
		{
			Statistic statistic;
			if (!Name.IsNullOrEmpty() && this.Statistics != null && this.Statistics.TryGetValue(Name, out statistic))
			{
				statistic.BaseValue *= Factor;
			}
		}

		// Token: 0x06005D7A RID: 23930 RVA: 0x001C3C10 File Offset: 0x001C1E10
		public void ShowActiveEffects()
		{
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(true);
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (Effect effect in this.Effects)
			{
				if (effect.GetDescription() != null)
				{
					if (num != 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.AppendLine("{{Y|");
					stringBuilder.AppendLine(effect.GetDescription());
					stringBuilder.Append("}}  ");
					stringBuilder.AppendLine(Campfire.ProcessEffectDescription(effect.GetDetails(), this).Replace("\n", "\n  "));
					num++;
				}
			}
			if (num <= 0)
			{
				if (this.IsPlayer())
				{
					BookUI.ShowBook("No active effects.", "&WActive Effects&Y - " + this.DisplayName, "Sounds/Interact/sfx_interact_book_read", null, null);
				}
				else
				{
					BookUI.ShowBook("No active effects.", "&WActive Effects&Y - " + this.DisplayName, "Sounds/Interact/sfx_interact_book_read", null, null);
				}
			}
			else
			{
				BookUI.ShowBook(stringBuilder.ToString(), "&WActive Effects&Y - " + this.DisplayName, "Sounds/Interact/sfx_interact_book_read", null, null);
			}
			scrapBuffer.Draw();
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x001C3D48 File Offset: 0x001C1F48
		public Rack<Effect> Effects
		{
			get
			{
				EffectRack effectRack;
				if ((effectRack = this._Effects) == null)
				{
					effectRack = (this._Effects = new EffectRack());
				}
				return effectRack;
			}
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x001C3D6D File Offset: 0x001C1F6D
		public List<GameObject> GetEquippedObjects()
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetEquippedObjects();
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x001C3D80 File Offset: 0x001C1F80
		public List<GameObject> GetEquippedObjectsReadonly()
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetEquippedObjectsReadonly();
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x001C3D93 File Offset: 0x001C1F93
		public void GetEquippedObjects(List<GameObject> result)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.GetEquippedObjects(result);
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x001C3DA6 File Offset: 0x001C1FA6
		public bool HasInstalledCybernetics(string Blueprint = null, Predicate<GameObject> Filter = null)
		{
			Body body = this.Body;
			return body != null && body.HasInstalledCybernetics(Blueprint, Filter);
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x001C3DBB File Offset: 0x001C1FBB
		public List<GameObject> GetInstalledCybernetics()
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetInstalledCybernetics();
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x001C3DCE File Offset: 0x001C1FCE
		public List<GameObject> GetInstalledCyberneticsReadonly()
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetInstalledCyberneticsReadonly();
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x001C3DE1 File Offset: 0x001C1FE1
		public void GetInstalledCybernetics(List<GameObject> result)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.GetInstalledCybernetics(result);
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x001C3DF4 File Offset: 0x001C1FF4
		public List<GameObject> GetEquippedObjectsAndInstalledCybernetics()
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetEquippedObjectsAndInstalledCybernetics();
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x001C3E07 File Offset: 0x001C2007
		public List<GameObject> GetEquippedObjectsAndInstalledCyberneticsReadonly()
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetEquippedObjectsAndInstalledCyberneticsReadonly();
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x001C3E1A File Offset: 0x001C201A
		public void GetEquippedObjectsAndInstalledCybernetics(List<GameObject> result)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.GetEquippedObjectsAndInstalledCybernetics(result);
		}

		// Token: 0x06005D86 RID: 23942 RVA: 0x001C3E30 File Offset: 0x001C2030
		public List<GameObject> GetWholeInventory()
		{
			List<GameObject> list = new List<GameObject>();
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.GetObjects(list);
			}
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(list);
				body.GetInstalledCybernetics(list);
			}
			return list;
		}

		// Token: 0x06005D87 RID: 23943 RVA: 0x001C3E70 File Offset: 0x001C2070
		public List<GameObject> GetWholeInventoryReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.GetObjects(list);
			}
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(list);
				body.GetInstalledCybernetics(list);
			}
			return list;
		}

		// Token: 0x06005D88 RID: 23944 RVA: 0x001C3EAE File Offset: 0x001C20AE
		public List<GameObject> GetInventory()
		{
			Inventory inventory = this.Inventory;
			return ((inventory != null) ? inventory.GetObjects() : null) ?? new List<GameObject>();
		}

		// Token: 0x06005D89 RID: 23945 RVA: 0x001C3ECB File Offset: 0x001C20CB
		public void GetInventory(List<GameObject> Store)
		{
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjects(Store);
		}

		// Token: 0x06005D8A RID: 23946 RVA: 0x001C3EDE File Offset: 0x001C20DE
		public void GetInventoryDirect(List<GameObject> Store)
		{
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjectsDirect(Store);
		}

		// Token: 0x06005D8B RID: 23947 RVA: 0x001C3EF1 File Offset: 0x001C20F1
		public List<GameObject> GetInventory(Predicate<GameObject> Filter)
		{
			Inventory inventory = this.Inventory;
			return ((inventory != null) ? inventory.GetObjects(Filter) : null) ?? new List<GameObject>();
		}

		// Token: 0x06005D8C RID: 23948 RVA: 0x001C3F0F File Offset: 0x001C210F
		public List<GameObject> GetInventoryDirect(Predicate<GameObject> Filter)
		{
			Inventory inventory = this.Inventory;
			return ((inventory != null) ? inventory.GetObjectsDirect(Filter) : null) ?? new List<GameObject>();
		}

		// Token: 0x06005D8D RID: 23949 RVA: 0x001C3F30 File Offset: 0x001C2130
		public List<GameObject> GetInventoryAndEquipmentAndDefaultEquipment()
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjects() : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects() : null);
			int num = ((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0);
			List<GameObject> result = new List<GameObject>(num);
			if (list != null)
			{
				result.AddRange(list);
			}
			if (list2 != null)
			{
				result.AddRange(list2);
			}
			Body body2 = this.Body;
			if (body2 != null)
			{
				body2.ForeachDefaultBehavior(delegate(GameObject d)
				{
					if (d != null)
					{
						result.Add(d);
					}
				});
			}
			return result;
		}

		// Token: 0x06005D8E RID: 23950 RVA: 0x001C3FD0 File Offset: 0x001C21D0
		public List<GameObject> GetInventoryAndEquipment()
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjects() : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects() : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x001C4037 File Offset: 0x001C2237
		public void GetInventoryAndEquipment(List<GameObject> Store)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(Store);
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjects(Store);
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x001C405C File Offset: 0x001C225C
		public List<GameObject> GetInventoryAndEquipment(Predicate<GameObject> Filter)
		{
			List<GameObject> list = (this.HasPart<Inventory>() ? this.Inventory.GetObjects(Filter) : null);
			List<GameObject> list2 = (this.HasPart<Body>() ? this.Body.GetEquippedObjects(Filter) : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x001C40D0 File Offset: 0x001C22D0
		public List<GameObject> GetInventoryAndEquipment(List<GameObject> Store, Predicate<GameObject> Filter)
		{
			List<GameObject> list = (this.HasPart<Inventory>() ? this.Inventory.GetObjects(Filter) : null);
			List<GameObject> list2 = (this.HasPart<Body>() ? this.Body.GetEquippedObjects(Filter) : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x001C4144 File Offset: 0x001C2344
		public List<GameObject> GetInventoryAndEquipmentReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryAndEquipment(list);
			return list;
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x001C4160 File Offset: 0x001C2360
		public List<GameObject> GetInventoryAndEquipmentReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetInventoryAndEquipmentReadonly();
			}
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryAndEquipment(list, Filter);
			return list;
		}

		// Token: 0x06005D94 RID: 23956 RVA: 0x001C4188 File Offset: 0x001C2388
		public List<GameObject> GetInventoryDirectAndEquipment()
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjectsDirect() : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects() : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D95 RID: 23957 RVA: 0x001C41F0 File Offset: 0x001C23F0
		public List<GameObject> GetInventoryDirectAndEquipmentAndAdjacentCells(Predicate<GameObject> Filter)
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjectsDirect(Filter) : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects(Filter) : null);
			int num = ((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0);
			List<GameObject> result = new List<GameObject>(num);
			if (list != null)
			{
				result.AddRange(list);
			}
			if (list2 != null)
			{
				result.AddRange(list2);
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell != null)
			{
				currentCell.ForeachLocalAdjacentCellAndSelf(delegate(Cell c)
				{
					if (c != null)
					{
						result.AddRange(c.GetObjects(Filter));
					}
				});
			}
			return result;
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x001C42A4 File Offset: 0x001C24A4
		public List<GameObject> GetInventoryDirectAndEquipment(Predicate<GameObject> Filter)
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjectsDirect(Filter) : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects(Filter) : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x001C430D File Offset: 0x001C250D
		public void GetInventoryDirectAndEquipment(List<GameObject> Store)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(Store);
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjectsDirect(Store);
		}

		// Token: 0x06005D98 RID: 23960 RVA: 0x001C4332 File Offset: 0x001C2532
		public void GetInventoryDirectAndEquipment(List<GameObject> Store, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				this.GetInventoryDirectAndEquipment(Store);
				return;
			}
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(Store, Filter);
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjectsDirect(Store, Filter);
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x001C4364 File Offset: 0x001C2564
		public List<GameObject> GetInventoryDirectAndEquipmentReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryDirectAndEquipment(list);
			return list;
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x001C4380 File Offset: 0x001C2580
		public List<GameObject> GetInventoryDirectAndEquipmentReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetInventoryDirectAndEquipmentReadonly();
			}
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryDirectAndEquipment(list, Filter);
			return list;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x001C43A8 File Offset: 0x001C25A8
		public List<GameObject> GetInventoryEquipmentAndCybernetics()
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjects() : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects() : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x001C440F File Offset: 0x001C260F
		public void GetInventoryEquipmentAndCybernetics(List<GameObject> Store)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(Store);
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjects(Store);
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x001C4434 File Offset: 0x001C2634
		public List<GameObject> GetInventoryEquipmentAndCybernetics(Predicate<GameObject> Filter)
		{
			List<GameObject> list = (this.HasPart<Inventory>() ? this.Inventory.GetObjects(Filter) : null);
			List<GameObject> list2 = (this.HasPart<Body>() ? this.Body.GetEquippedObjects(Filter) : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x001C44A5 File Offset: 0x001C26A5
		public List<GameObject> GetInventoryEquipmentAndCybernetics(List<GameObject> Store, Predicate<GameObject> Filter)
		{
			if (this.HasPart<Inventory>())
			{
				this.Inventory.GetObjects(Store, Filter);
			}
			if (this.HasPart<Body>())
			{
				this.Body.GetEquippedObjects(Store, Filter);
			}
			return Store;
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x001C44D4 File Offset: 0x001C26D4
		public List<GameObject> GetInventoryEquipmentAndCyberneticsReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryEquipmentAndCybernetics(list);
			return list;
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x001C44F0 File Offset: 0x001C26F0
		public List<GameObject> GetInventoryEquipmentAndCyberneticsReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetInventoryEquipmentAndCyberneticsReadonly();
			}
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryEquipmentAndCybernetics(list, Filter);
			return list;
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x001C4518 File Offset: 0x001C2718
		public List<GameObject> GetInventoryDirectEquipmentAndCybernetics()
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjectsDirect() : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects() : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005DA2 RID: 23970 RVA: 0x001C4580 File Offset: 0x001C2780
		public List<GameObject> GetInventoryDirectEquipmentAndCybernetics(Predicate<GameObject> Filter)
		{
			Inventory inventory = this.Inventory;
			List<GameObject> list = ((inventory != null) ? inventory.GetObjectsDirect(Filter) : null);
			Body body = this.Body;
			List<GameObject> list2 = ((body != null) ? body.GetEquippedObjects(Filter) : null);
			List<GameObject> list3 = new List<GameObject>(((list != null) ? list.Count : 0) + ((list2 != null) ? list2.Count : 0));
			if (list != null)
			{
				list3.AddRange(list);
			}
			if (list2 != null)
			{
				list3.AddRange(list2);
			}
			return list3;
		}

		// Token: 0x06005DA3 RID: 23971 RVA: 0x001C45E9 File Offset: 0x001C27E9
		public void GetInventoryDirectEquipmentAndCybernetics(List<GameObject> Store)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(Store);
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjectsDirect(Store);
		}

		// Token: 0x06005DA4 RID: 23972 RVA: 0x001C460E File Offset: 0x001C280E
		public void GetInventoryDirectEquipmentAndCybernetics(List<GameObject> Store, Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				this.GetInventoryDirectEquipmentAndCybernetics(Store);
				return;
			}
			Body body = this.Body;
			if (body != null)
			{
				body.GetEquippedObjects(Store, Filter);
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return;
			}
			inventory.GetObjectsDirect(Store, Filter);
		}

		// Token: 0x06005DA5 RID: 23973 RVA: 0x001C4640 File Offset: 0x001C2840
		public List<GameObject> GetInventoryDirectEquipmentAndCyberneticsReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryDirectEquipmentAndCybernetics(list);
			return list;
		}

		// Token: 0x06005DA6 RID: 23974 RVA: 0x001C465C File Offset: 0x001C285C
		public List<GameObject> GetInventoryDirectEquipmentAndCyberneticsReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetInventoryDirectEquipmentAndCyberneticsReadonly();
			}
			List<GameObject> list = Event.NewGameObjectList();
			this.GetInventoryDirectEquipmentAndCybernetics(list, Filter);
			return list;
		}

		// Token: 0x06005DA7 RID: 23975 RVA: 0x001C4682 File Offset: 0x001C2882
		public void ForeachEquippedObject(Action<GameObject> Proc)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.ForeachEquippedObject(Proc);
		}

		// Token: 0x06005DA8 RID: 23976 RVA: 0x001C4695 File Offset: 0x001C2895
		public void SafeForeachEquippedObject(Action<GameObject> Proc)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.SafeForeachEquippedObject(Proc);
		}

		// Token: 0x06005DA9 RID: 23977 RVA: 0x001C46A8 File Offset: 0x001C28A8
		public void ForeachInstalledCybernetics(Action<GameObject> Proc)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.ForeachInstalledCybernetics(Proc);
		}

		// Token: 0x06005DAA RID: 23978 RVA: 0x001C46BB File Offset: 0x001C28BB
		public void SafeForeachInstalledCybernetics(Action<GameObject> Proc)
		{
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.SafeForeachInstalledCybernetics(Proc);
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x001C46D0 File Offset: 0x001C28D0
		public void ForeachEquipmentAndCybernetics(Action<GameObject> Proc)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.ForeachEquippedObject(Proc);
				body.ForeachInstalledCybernetics(Proc);
			}
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x001C46F8 File Offset: 0x001C28F8
		public void SafeForeachEquipmentAndCybernetics(Action<GameObject> Proc)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.SafeForeachEquippedObject(Proc);
				body.SafeForeachInstalledCybernetics(Proc);
			}
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x001C471D File Offset: 0x001C291D
		public void ForeachInventoryAndEquipment(Action<GameObject> Proc)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.ForeachObject(Proc);
			}
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.ForeachEquippedObject(Proc);
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x001C4742 File Offset: 0x001C2942
		public void SafeForeachInventoryAndEquipment(Action<GameObject> Proc)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.SafeForeachObject(Proc);
			}
			Body body = this.Body;
			if (body == null)
			{
				return;
			}
			body.SafeForeachEquippedObject(Proc);
		}

		// Token: 0x06005DAF RID: 23983 RVA: 0x001C4768 File Offset: 0x001C2968
		public void ForeachInventoryEquipmentAndCybernetics(Action<GameObject> Proc)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.ForeachObject(Proc);
			}
			Body body = this.Body;
			if (body != null)
			{
				body.ForeachEquippedObject(Proc);
				body.ForeachInstalledCybernetics(Proc);
			}
		}

		// Token: 0x06005DB0 RID: 23984 RVA: 0x001C47A0 File Offset: 0x001C29A0
		public void SafeForeachInventoryEquipmentAndCybernetics(Action<GameObject> Proc)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.SafeForeachObject(Proc);
			}
			Body body = this.Body;
			if (body != null)
			{
				body.SafeForeachEquippedObject(Proc);
				body.SafeForeachInstalledCybernetics(Proc);
			}
		}

		// Token: 0x06005DB1 RID: 23985 RVA: 0x001C47D8 File Offset: 0x001C29D8
		public void ForeachInventoryEquipmentDefaultBehaviorAndCybernetics(Action<GameObject> Proc)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.ForeachObject(Proc);
			}
			Body body = this.Body;
			if (body != null)
			{
				body.ForeachEquippedObject(Proc);
				body.ForeachDefaultBehavior(Proc);
				body.ForeachInstalledCybernetics(Proc);
			}
		}

		// Token: 0x06005DB2 RID: 23986 RVA: 0x001C4818 File Offset: 0x001C2A18
		public void SafeForeachInventoryEquipmentDefaultBehaviorAndCybernetics(Action<GameObject> Proc)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				inventory.SafeForeachObject(Proc);
			}
			Body body = this.Body;
			if (body != null)
			{
				body.SafeForeachEquippedObject(Proc);
				body.SafeForeachDefaultBehavior(Proc);
				body.SafeForeachInstalledCybernetics(Proc);
			}
		}

		// Token: 0x06005DB3 RID: 23987 RVA: 0x001C4858 File Offset: 0x001C2A58
		public bool EquipObject(GameObject Object, BodyPart Part, bool Silent = false, int? EnergyCost = null)
		{
			Event @event = Event.New("CommandEquipObject", "Object", Object, "BodyPart", Part);
			if (EnergyCost != null)
			{
				@event.SetParameter("EnergyCost", EnergyCost.Value);
			}
			if (Silent)
			{
				@event.SetSilent(Silent);
			}
			return this.FireEvent(@event);
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x001C48AC File Offset: 0x001C2AAC
		public bool EquipObject(GameObject Object, string Slot, bool Silent = false, int? EnergyCost = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				Debug.LogError("Object with blueprint " + this.Blueprint + " had no body to equip on");
				return false;
			}
			BodyPart firstPart = body.GetFirstPart(Slot);
			if (body == null)
			{
				Debug.LogError(string.Concat(new string[] { "Object with blueprint ", this.Blueprint, " had no ", Slot, " body part to equip on" }));
				return false;
			}
			return this.EquipObject(Object, firstPart, Silent, null);
		}

		// Token: 0x06005DB5 RID: 23989 RVA: 0x001C4934 File Offset: 0x001C2B34
		public bool ForceEquipObject(GameObject Object, BodyPart Part, bool Silent = false, int? EnergyCost = null)
		{
			Event @event = Event.New("CommandForceEquipObject", 0, 0, 0);
			@event.SetParameter("Object", Object);
			@event.SetParameter("BodyPart", Part);
			if (EnergyCost != null)
			{
				@event.SetParameter("EnergyCost", EnergyCost.Value);
			}
			if (Silent)
			{
				@event.SetSilent(Silent);
			}
			return this.FireEvent(@event);
		}

		// Token: 0x06005DB6 RID: 23990 RVA: 0x001C4998 File Offset: 0x001C2B98
		public bool ForceEquipObject(GameObject Object, string Slot, bool Silent = false, int? EnergyCost = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				Debug.LogError("Object with blueprint " + this.Blueprint + " had no body to equip on");
				return false;
			}
			BodyPart firstPart = body.GetFirstPart(Slot);
			if (body == null)
			{
				Debug.LogError(string.Concat(new string[] { "Object with blueprint ", this.Blueprint, " had no ", Slot, " body part to equip on" }));
				return false;
			}
			return this.ForceEquipObject(Object, firstPart, Silent, EnergyCost);
		}

		// Token: 0x06005DB7 RID: 23991 RVA: 0x001C4A18 File Offset: 0x001C2C18
		public bool IsNonStackableFromParts()
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (!this.PartsList[i].SameAs(this.PartsList[i]))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005DB8 RID: 23992 RVA: 0x001C4A5F File Offset: 0x001C2C5F
		public bool IsStackable()
		{
			return !this.HasIntProperty("NeverStack") && !this.IsNonStackableFromParts();
		}

		// Token: 0x06005DB9 RID: 23993 RVA: 0x001C4A7C File Offset: 0x001C2C7C
		public bool PartsPreventGeneratingStacked()
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (!this.PartsList[i].CanGenerateStacked())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005DBA RID: 23994 RVA: 0x001C4AB8 File Offset: 0x001C2CB8
		public bool CanGenerateStacked()
		{
			if (this.HasIntProperty("NeverStack"))
			{
				return false;
			}
			if (this.HasTag("AlwaysStack"))
			{
				return true;
			}
			if (this.PartsPreventGeneratingStacked())
			{
				return false;
			}
			string tag = this.GetTag("Mods", null);
			return tag.IsNullOrEmpty() || !(tag != "None");
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x001C4B14 File Offset: 0x001C2D14
		public void TakePopulation(string Population)
		{
			foreach (PopulationResult populationResult in PopulationManager.Generate(Population, null, null, null, null, null))
			{
				this.TakeObject(populationResult.Blueprint, populationResult.Number, false, false, new int?(0), null, 0, 0, null, null, null, null);
			}
		}

		// Token: 0x06005DBC RID: 23996 RVA: 0x001C4B88 File Offset: 0x001C2D88
		public void ReceivePopulation(string Population)
		{
			foreach (PopulationResult populationResult in PopulationManager.Generate(Population, null, null, null, null, null))
			{
				this.ReceiveObject(populationResult.Blueprint, populationResult.Number, false, 0, 0, null, null, null, null);
			}
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x001C4BF4 File Offset: 0x001C2DF4
		public int TakeObjectsFromPopulation(string Table, int Number, Dictionary<string, string> Variables = null, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> Tracking = null)
		{
			Dictionary<string, int> dictionary;
			if (GameObject.TakeObjectsFromTableGenerationInUse)
			{
				dictionary = new Dictionary<string, int>();
			}
			else
			{
				GameObject.TakeObjectsFromTableGenerationInUse = true;
				dictionary = GameObject.TakeObjectsFromTableGeneration;
				dictionary.Clear();
			}
			for (int i = 0; i < Number; i++)
			{
				string blueprint = PopulationManager.RollOneFrom(Table, Variables, null).Blueprint;
				if (!blueprint.IsNullOrEmpty())
				{
					if (dictionary.ContainsKey(blueprint))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string text = blueprint;
						int num = dictionary2[text];
						dictionary2[text] = num + 1;
					}
					else
					{
						dictionary.Add(blueprint, 1);
					}
				}
			}
			int num2 = 0;
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				num2 += this.TakeObject(keyValuePair.Key, keyValuePair.Value, NoStack, Silent, EnergyCost, Context, BonusModChance, SetModNumber, AutoMod, Tracking, BeforeObjectCreated, AfterObjectCreated);
			}
			if (dictionary == GameObject.TakeObjectsFromTableGeneration)
			{
				dictionary.Clear();
				GameObject.TakeObjectsFromTableGenerationInUse = false;
			}
			return num2;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x001C4D04 File Offset: 0x001C2F04
		public bool TakeObjectFromPopulation(string Table, Dictionary<string, string> Variables = null, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> Tracking = null)
		{
			string blueprint = PopulationManager.RollOneFrom(Table, Variables, null).Blueprint;
			return this.TakeObject(blueprint, NoStack, Silent, EnergyCost, BonusModChance, SetModNumber, AutoMod, Context, Tracking, BeforeObjectCreated, AfterObjectCreated);
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x001C4D40 File Offset: 0x001C2F40
		public int TakeObject(List<GameObject> List, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, string Context = null, List<GameObject> Tracking = null)
		{
			int num = 0;
			foreach (GameObject gameObject in List)
			{
				if (this.TakeObject(gameObject, NoStack, Silent, EnergyCost, Context, Tracking))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x001C4DA0 File Offset: 0x001C2FA0
		public int TakeObject(string Blueprint, int Number, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, string Context = null, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, List<GameObject> Tracking = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			if (Number <= 0)
			{
				return 0;
			}
			GameObject gameObject = GameObject.Create(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, null);
			if (Number <= 1 || NoStack || !gameObject.CanGenerateStacked() || gameObject.Stacker == null || gameObject.Stacker.StackCount != 1)
			{
				int num = 0;
				for (int i = 0; i < Number; i++)
				{
					if (this.TakeObject(Blueprint, NoStack, Silent, EnergyCost, BonusModChance, SetModNumber, AutoMod, Context, Tracking, BeforeObjectCreated, AfterObjectCreated))
					{
						num++;
					}
				}
				return num;
			}
			gameObject.Stacker.StackCount = Number;
			if (!this.TakeObject(gameObject, false, Silent, EnergyCost, Context, Tracking))
			{
				return 0;
			}
			return Number;
		}

		// Token: 0x06005DC1 RID: 24001 RVA: 0x001C4E40 File Offset: 0x001C3040
		public bool TakeObject(string Blueprint, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, string Context = null, List<GameObject> Tracking = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			return this.TakeObject(GameObject.Create(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, null), NoStack, Silent, EnergyCost, Context, Tracking);
		}

		// Token: 0x06005DC2 RID: 24002 RVA: 0x001C4E70 File Offset: 0x001C3070
		public bool TakeObject(string Blueprint, out GameObject Object, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, string Context = null, List<GameObject> Tracking = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null)
		{
			Object = GameObject.Create(Blueprint, BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, null);
			return this.TakeObject(Object, NoStack, Silent, EnergyCost, Context, Tracking);
		}

		// Token: 0x06005DC3 RID: 24003 RVA: 0x001C4EA8 File Offset: 0x001C30A8
		public bool TakeObject(GameObject Object, bool NoStack = false, bool Silent = false, int? EnergyCost = 0, string Context = null, List<GameObject> Tracking = null)
		{
			Event.PinCurrentPool();
			Event @event;
			if (EnergyCost == null)
			{
				@event = (Silent ? GameObject.eCommandTakeObjectSilent : GameObject.eCommandTakeObject);
			}
			else
			{
				@event = (Silent ? GameObject.eCommandTakeObjectSilentWithEnergyCost : GameObject.eCommandTakeObjectWithEnergyCost);
				@event.SetParameter("EnergyCost", EnergyCost.Value);
			}
			@event.SetParameter("Object", Object);
			@event.SetParameter("Context", Context);
			@event.SetFlag("NoStack", NoStack);
			bool flag = this.FireEvent(@event);
			Event.ResetToPin();
			if (flag && Tracking != null)
			{
				Tracking.Add(Object);
			}
			return flag;
		}

		// Token: 0x06005DC4 RID: 24004 RVA: 0x001C4F3C File Offset: 0x001C313C
		public int ReceiveObjectsFromPopulation(string Table, int Number, Dictionary<string, string> Variables = null, bool NoStack = false, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> Tracking = null)
		{
			return this.TakeObjectsFromPopulation(Table, Number, Variables, NoStack, true, new int?(0), 0, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, Tracking);
		}

		// Token: 0x06005DC5 RID: 24005 RVA: 0x001C4F68 File Offset: 0x001C3168
		public bool ReceiveObjectFromPopulation(string Table, Dictionary<string, string> Variables = null, bool NoStack = false, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null, List<GameObject> Tracking = null)
		{
			return this.TakeObjectsFromPopulation(Table, 1, Variables, NoStack, true, new int?(0), BonusModChance, SetModNumber, AutoMod, BeforeObjectCreated, AfterObjectCreated, Context, Tracking) > 0;
		}

		// Token: 0x06005DC6 RID: 24006 RVA: 0x001C4F97 File Offset: 0x001C3197
		public bool ReceiveObject(GameObject Object, bool NoStack = false, string Context = null)
		{
			return this.TakeObject(Object, NoStack, true, new int?(0), Context, null);
		}

		// Token: 0x06005DC7 RID: 24007 RVA: 0x001C4FAC File Offset: 0x001C31AC
		public bool ReceiveObject(string Blueprint, bool NoStack = false, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null)
		{
			return this.TakeObject(Blueprint, NoStack, true, new int?(0), BonusModChance, SetModNumber, AutoMod, Context, null, BeforeObjectCreated, AfterObjectCreated);
		}

		// Token: 0x06005DC8 RID: 24008 RVA: 0x001C4FD8 File Offset: 0x001C31D8
		public int ReceiveObject(string Blueprint, int Number, bool NoStack = false, int BonusModChance = 0, int SetModNumber = 0, string AutoMod = null, Action<GameObject> BeforeObjectCreated = null, Action<GameObject> AfterObjectCreated = null, string Context = null)
		{
			return this.TakeObject(Blueprint, Number, NoStack, true, new int?(0), Context, BonusModChance, SetModNumber, AutoMod, null, BeforeObjectCreated, AfterObjectCreated);
		}

		// Token: 0x06005DC9 RID: 24009 RVA: 0x001C5010 File Offset: 0x001C3210
		public int ReceiveObject(List<GameObject> List, bool NoStack = false, string Context = null)
		{
			int num = 0;
			foreach (GameObject gameObject in List)
			{
				if (this.TakeObject(gameObject, NoStack, true, new int?(0), Context, null))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x001C5074 File Offset: 0x001C3274
		public bool ForceApplyEffect(Effect E, GameObject Owner = null)
		{
			if (!ForceApplyEffectEvent.Check(this, E.ClassName, E, null))
			{
				return this.ApplyEffect(E, null);
			}
			ApplyEffectEvent.Check(this, E.ClassName, E, Owner);
			if (!E.CanApplyToStack() && !this.HasTag("AlwaysStack"))
			{
				this.SplitStack(1, null, true);
			}
			E.Object = this;
			if (E.Apply(this))
			{
				this.Effects.Add(E);
				E.ApplyRegistrar(this, false);
				E.Applied(this);
				EffectForceAppliedEvent.Send(this, E.ClassName, E, null);
				EffectAppliedEvent.Send(this, E.ClassName, E, null);
				this.CheckStack();
				return true;
			}
			this.CheckStack();
			return false;
		}

		// Token: 0x06005DCB RID: 24011 RVA: 0x001C5120 File Offset: 0x001C3320
		public bool TakeDamage(ref int Amount, string Attributes = null, string DeathReason = null, string ThirdPersonDeathReason = null, GameObject Owner = null, GameObject Attacker = null, GameObject Source = null, GameObject Perspective = null, GameObject DescribeAsFrom = null, string Message = "from %t attack.", bool Accidental = false, bool Environmental = false, bool Indirect = false, bool ShowUninvolved = false, bool IgnoreVisibility = false, bool ShowForInanimate = false, bool SilentIfNoDamage = false, bool NoSetTarget = false, bool UsePopups = false, int Phase = 5, string ShowDamageType = null)
		{
			if (Phase == 0 && Attacker != null)
			{
				Phase = Attacker.GetPhase();
			}
			Damage damage = new Damage(Amount);
			damage.AddAttributes(Attributes);
			if (Accidental)
			{
				damage.AddAttribute("Accidental");
			}
			if (Environmental)
			{
				damage.AddAttribute("Environmental");
			}
			Event @event = Event.New("TakeDamage", 0, 0, 0);
			@event.SetParameter("Damage", damage);
			@event.SetParameter("Owner", Owner ?? Attacker);
			@event.SetParameter("Attacker", Attacker);
			@event.SetParameter("Source", Source);
			@event.SetParameter("Perspective", Perspective);
			@event.SetParameter("DescribeAsFrom", DescribeAsFrom);
			@event.SetParameter("Phase", Phase);
			if (!Message.IsNullOrEmpty())
			{
				@event.SetParameter("Message", Message);
			}
			if (!DeathReason.IsNullOrEmpty())
			{
				@event.SetParameter("DeathReason", DeathReason);
			}
			if (!ThirdPersonDeathReason.IsNullOrEmpty())
			{
				@event.SetParameter("ThirdPersonDeathReason", ThirdPersonDeathReason);
			}
			if (ShowDamageType != null)
			{
				@event.SetParameter("ShowDamageType", ShowDamageType);
			}
			if (Indirect)
			{
				@event.SetFlag("Indirect", true);
			}
			if (ShowForInanimate)
			{
				@event.SetFlag("ShowForInanimate", true);
			}
			if (ShowUninvolved)
			{
				@event.SetFlag("ShowUninvolved", true);
			}
			if (IgnoreVisibility)
			{
				@event.SetFlag("IgnoreVisibility", true);
			}
			if (SilentIfNoDamage)
			{
				@event.SetFlag("SilentIfNoDamage", true);
			}
			if (NoSetTarget)
			{
				@event.SetFlag("NoSetTarget", true);
			}
			if (UsePopups)
			{
				@event.SetFlag("UsePopups", true);
			}
			bool flag = this.FireEvent(@event);
			Amount = damage.Amount;
			if (flag && Amount > 0)
			{
				damage.PlaySound(this ?? Owner ?? Attacker);
			}
			return flag;
		}

		// Token: 0x06005DCC RID: 24012 RVA: 0x001C52D8 File Offset: 0x001C34D8
		public bool TakeDamage(int Amount, string Message, string Attributes = null, string DeathReason = null, string ThirdPersonDeathReason = null, GameObject Owner = null, GameObject Attacker = null, GameObject Source = null, GameObject Perspective = null, GameObject DescribeAsFrom = null, bool Accidental = false, bool Environmental = false, bool Indirect = false, bool ShowUninvolved = false, bool IgnoreVisibility = false, bool ShowForInanimate = false, bool SilentIfNoDamage = false, bool NoSetTarget = false, bool UsePopups = false, int Phase = 5, string ShowDamageType = null)
		{
			return this.TakeDamage(ref Amount, Attributes, DeathReason, ThirdPersonDeathReason, Owner, Attacker, Source, Perspective, DescribeAsFrom, Message, Accidental, Environmental, Indirect, ShowUninvolved, IgnoreVisibility, ShowForInanimate, SilentIfNoDamage, NoSetTarget, UsePopups, Phase, ShowDamageType);
		}

		// Token: 0x06005DCD RID: 24013 RVA: 0x001C5314 File Offset: 0x001C3514
		public bool TakeDamage(int Amount, StringBuilder Message, string Attributes = null, string DeathReason = null, string ThirdPersonDeathReason = null, GameObject Owner = null, GameObject Attacker = null, GameObject Source = null, GameObject Perspective = null, bool Accidental = false, bool Environmental = false, bool Indirect = false, bool ShowUninvolved = false, bool IgnoreVisibility = false, bool ShowForInanimate = false, bool SilentIfNoDamage = false, bool UsePopups = false, int Phase = 5, string ShowDamageType = null)
		{
			if (Message != null)
			{
				return this.TakeDamage(ref Amount, Attributes, DeathReason, ThirdPersonDeathReason, Owner, Attacker, Source, Perspective, null, Message.ToString(), Accidental, Environmental, Indirect, ShowUninvolved, IgnoreVisibility, ShowForInanimate, SilentIfNoDamage, false, UsePopups, Phase, ShowDamageType);
			}
			return this.TakeDamage(ref Amount, Attributes, DeathReason, ThirdPersonDeathReason, Owner, Attacker, Source, null, null, "from %t attack.", Accidental, Environmental, Indirect, ShowUninvolved, IgnoreVisibility, ShowForInanimate, SilentIfNoDamage, false, UsePopups, Phase, ShowDamageType);
		}

		// Token: 0x06005DCE RID: 24014 RVA: 0x001C5388 File Offset: 0x001C3588
		public bool TakeDamage(int DamageAmount, GameObject FromAttacker, string ShowMessage)
		{
			return this.TakeDamage(DamageAmount, ShowMessage, null, null, null, null, FromAttacker, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
		}

		// Token: 0x06005DCF RID: 24015 RVA: 0x001C53B4 File Offset: 0x001C35B4
		public bool ApplyEffect(Effect E, GameObject Owner = null)
		{
			if (this.HasTag("NoEffects") && this.GetIntProperty("ForceEffects", 0) == 0)
			{
				return false;
			}
			if (ApplyEffectEvent.Check(this, E.ClassName, E, Owner))
			{
				if (!E.CanApplyToStack() && !this.HasTag("AlwaysStack"))
				{
					this.SplitStack(1, null, true);
				}
				E.Object = this;
				this.PlayWorldSound(E.ApplySound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (E.Apply(this))
				{
					this.Effects.Add(E);
					E.ApplyRegistrar(this, false);
					E.Applied(this);
					EffectAppliedEvent.Send(this, E.ClassName, E, null);
					this.CheckStack();
					return true;
				}
				this.CheckStack();
			}
			return false;
		}

		// Token: 0x06005DD0 RID: 24016 RVA: 0x001C5485 File Offset: 0x001C3685
		public bool CheckStack()
		{
			Stacker stacker = this.Stacker;
			return stacker != null && stacker.Check();
		}

		// Token: 0x06005DD1 RID: 24017 RVA: 0x001C5498 File Offset: 0x001C3698
		public Effect RemoveEffectAt(int Index, bool NeedStackCheck = true)
		{
			Effect effect = this._Effects.TakeAt(Index);
			this.PlayWorldSound(effect.RemoveSound, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			effect.ApplyUnregistrar(this, false);
			effect.Remove(this);
			EffectRemovedEvent.Send(this, effect.ClassName, effect, null);
			effect.Object = null;
			if (NeedStackCheck)
			{
				this.CheckStack();
			}
			return effect;
		}

		// Token: 0x06005DD2 RID: 24018 RVA: 0x001C550C File Offset: 0x001C370C
		public bool RemoveEffect(Effect E, bool NeedStackCheck = true)
		{
			if (E == null || this._Effects == null)
			{
				return false;
			}
			int num = Array.IndexOf<Effect>(this._Effects.GetArray(), E, 0, this._Effects.Count);
			if (num == -1)
			{
				return false;
			}
			this.RemoveEffectAt(num, NeedStackCheck);
			return true;
		}

		// Token: 0x06005DD3 RID: 24019 RVA: 0x001C5554 File Offset: 0x001C3754
		public bool RemoveEffect(Type EffectType)
		{
			if (this._Effects != null)
			{
				Effect[] array = this._Effects.GetArray();
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					if (array[i].GetType() == EffectType)
					{
						this.RemoveEffectAt(i, true);
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005DD4 RID: 24020 RVA: 0x001C55A4 File Offset: 0x001C37A4
		public bool RemoveEffect<T>()
		{
			if (this._Effects != null)
			{
				Effect[] array = this._Effects.GetArray();
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					if (array[i].GetType() == typeof(T))
					{
						this.RemoveEffectAt(i, true);
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x001C5600 File Offset: 0x001C3800
		public bool RemoveEffect(Predicate<Effect> Filter)
		{
			if (this._Effects != null)
			{
				Effect[] array = this._Effects.GetArray();
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					if (Filter(array[i]))
					{
						this.RemoveEffectAt(i, true);
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005DD6 RID: 24022 RVA: 0x001C5650 File Offset: 0x001C3850
		public bool RemoveEffect(Type EffectType, Predicate<Effect> Filter)
		{
			if (this._Effects != null)
			{
				Effect[] array = this._Effects.GetArray();
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					if (array[i].GetType() == EffectType && Filter(array[i]))
					{
						this.RemoveEffectAt(i, true);
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005DD7 RID: 24023 RVA: 0x001C56AC File Offset: 0x001C38AC
		public bool RemoveEffectDescendedFrom<T>()
		{
			if (this._Effects != null)
			{
				Effect[] array = this._Effects.GetArray();
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					if (array[i] is T)
					{
						this.RemoveEffectAt(i, true);
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005DD8 RID: 24024 RVA: 0x001C56FC File Offset: 0x001C38FC
		public int RemoveAllEffects(bool NeedStackCheck = true)
		{
			if (this._Effects == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = this._Effects.Count - 1; i >= 0; i--)
			{
				this.RemoveEffectAt(i, false);
				num++;
			}
			if (num > 0 && NeedStackCheck)
			{
				this.CheckStack();
			}
			return num;
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x001C574C File Offset: 0x001C394C
		public int RemoveAllEffects<T>(bool NeedStackCheck = true)
		{
			if (this._Effects == null)
			{
				return 0;
			}
			int num = 0;
			Effect[] array = this._Effects.GetArray();
			for (int i = this._Effects.Count - 1; i >= 0; i--)
			{
				if (array[i].GetType() == typeof(T))
				{
					this.RemoveEffectAt(i, false);
					num++;
				}
			}
			if (num > 0 && NeedStackCheck)
			{
				this.CheckStack();
			}
			return num;
		}

		// Token: 0x06005DDA RID: 24026 RVA: 0x001C57BC File Offset: 0x001C39BC
		public int RemoveEffectsOfType(int Mask, Predicate<Effect> Filter = null, bool NeedStackCheck = true)
		{
			int num = 0;
			if (this._Effects != null)
			{
				for (int i = this._Effects.Count - 1; i >= 0; i--)
				{
					if (this._Effects[i].IsOfTypes(Mask) && (Filter == null || Filter(this._Effects[i])))
					{
						this.RemoveEffectAt(i, false);
						num++;
					}
				}
			}
			if (num > 0 && NeedStackCheck)
			{
				this.CheckStack();
			}
			return num;
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x001C5834 File Offset: 0x001C3A34
		public int RemoveEffectsOfPartialType(int Mask, Predicate<Effect> Filter = null, bool NeedStackCheck = true)
		{
			int num = 0;
			if (this._Effects != null)
			{
				for (int i = this._Effects.Count - 1; i >= 0; i--)
				{
					if (this._Effects[i].IsOfType(Mask) && (Filter == null || Filter(this._Effects[i])))
					{
						this.RemoveEffectAt(i, false);
						num++;
					}
				}
			}
			if (num > 0 && NeedStackCheck)
			{
				this.CheckStack();
			}
			return num;
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x001C58AC File Offset: 0x001C3AAC
		public GameObject FindObjectInInventory(string Blueprint)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (gameObject.Blueprint == Blueprint)
					{
						return gameObject;
					}
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && bodyPart.Equipped.Blueprint == Blueprint)
					{
						return bodyPart.Equipped;
					}
				}
			}
			return null;
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x001C5988 File Offset: 0x001C3B88
		public GameObject FindObjectInInventory(Predicate<GameObject> Filter)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (Filter == null || Filter(gameObject))
					{
						return gameObject;
					}
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && (Filter == null || Filter(bodyPart.Equipped)))
					{
						return bodyPart.Equipped;
					}
				}
			}
			return null;
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x001C5A60 File Offset: 0x001C3C60
		public List<GameObject> FindObjectsInInventory(string Blueprint)
		{
			List<GameObject> list = Event.NewGameObjectList();
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (gameObject.Blueprint == Blueprint)
					{
						list.Add(gameObject);
					}
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && bodyPart.Equipped.Blueprint == Blueprint)
					{
						list.Add(bodyPart.Equipped);
					}
				}
			}
			return list;
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x001C5B44 File Offset: 0x001C3D44
		public List<GameObject> FindObjectsInInventory(Predicate<GameObject> Filter)
		{
			List<GameObject> list = Event.NewGameObjectList();
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (Filter == null || Filter(gameObject))
					{
						list.Add(gameObject);
					}
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && (Filter == null || Filter(bodyPart.Equipped)))
					{
						list.Add(bodyPart.Equipped);
					}
				}
			}
			return list;
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x001C5C24 File Offset: 0x001C3E24
		public bool HasObjectInInventory(Func<GameObject, bool> test, int n = 1)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (test(gameObject))
					{
						if (gameObject.Stacker != null)
						{
							n -= gameObject.Stacker.Number;
						}
						else
						{
							n--;
						}
					}
					if (n <= 0)
					{
						return true;
					}
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && test(bodyPart.Equipped))
					{
						Stacker stacker = bodyPart.Equipped.Stacker;
						if (stacker != null)
						{
							n -= stacker.Number;
						}
						else
						{
							n--;
						}
					}
					if (n <= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005DE1 RID: 24033 RVA: 0x001C5D3C File Offset: 0x001C3F3C
		public bool HasObjectInInventory(string Blueprint, int n = 1)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjectsDirect())
				{
					if (gameObject.Blueprint == Blueprint)
					{
						Stacker stacker = gameObject.Stacker;
						if (stacker != null)
						{
							n -= stacker.Number;
						}
						else
						{
							n--;
						}
					}
					if (n <= 0)
					{
						return true;
					}
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && bodyPart.Equipped.Blueprint == Blueprint)
					{
						Stacker stacker2 = bodyPart.Equipped.Stacker;
						if (stacker2 != null)
						{
							n -= stacker2.Number;
						}
						else
						{
							n--;
						}
					}
					if (n <= 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005DE2 RID: 24034 RVA: 0x001C5E60 File Offset: 0x001C4060
		public bool HasObjectWithPartInDirection(string part, string direction)
		{
			Cell cell = this.GetCurrentCell();
			if (cell == null)
			{
				return false;
			}
			cell = cell.GetCellFromDirection(direction, true);
			return cell != null && cell.HasObjectWithPart(part);
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x001C5E90 File Offset: 0x001C4090
		public bool HasObjectEquippedOrDefault(string Blueprint, int n = 1)
		{
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && bodyPart.Equipped.Blueprint == Blueprint && bodyPart.Equipped.IsEquippedProperly(null))
					{
						Stacker stacker = bodyPart.Equipped.Stacker;
						if (stacker != null)
						{
							n -= stacker.Number;
						}
						else
						{
							n--;
						}
					}
					if (bodyPart.DefaultBehavior != null && bodyPart.DefaultBehavior.Blueprint == Blueprint)
					{
						Stacker stacker2 = bodyPart.DefaultBehavior.Stacker;
						if (stacker2 != null)
						{
							n -= stacker2.Number;
						}
						else
						{
							n--;
						}
					}
					if (n <= 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x001C5F80 File Offset: 0x001C4180
		public bool HasObjectEquipped(string Blueprint, int n = 1)
		{
			Body body = this.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.LoopParts())
				{
					if (bodyPart.Equipped != null && bodyPart.Equipped.Blueprint == Blueprint && bodyPart.Equipped.IsEquippedProperly(null))
					{
						Stacker stacker = bodyPart.Equipped.Stacker;
						if (stacker != null)
						{
							n -= stacker.Number;
						}
						else
						{
							n--;
						}
					}
					if (n <= 0)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005DE5 RID: 24037 RVA: 0x001C6028 File Offset: 0x001C4228
		public void UseObject(Predicate<GameObject> test)
		{
			Inventory inventory = this.Inventory;
			GameObject gameObject = ((inventory != null) ? inventory.FindObject(test) : null);
			if (gameObject != null)
			{
				gameObject.Destroy(null, false, false, null);
				return;
			}
			Body body = this.Body;
			gameObject = ((body != null) ? body.FindObject(test) : null);
			if (gameObject != null)
			{
				gameObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x06005DE6 RID: 24038 RVA: 0x001C607C File Offset: 0x001C427C
		public void UseObject(string Blueprint)
		{
			Inventory inventory = this.Inventory;
			GameObject gameObject = ((inventory != null) ? inventory.FindObjectByBlueprint(Blueprint) : null);
			if (gameObject != null)
			{
				gameObject.Destroy(null, false, false, null);
				return;
			}
			Body body = this.Body;
			gameObject = ((body != null) ? body.FindObjectByBlueprint(Blueprint) : null);
			if (gameObject != null)
			{
				gameObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06005DE7 RID: 24039 RVA: 0x001C60D0 File Offset: 0x001C42D0
		public double ValueEach
		{
			get
			{
				Commerce part = this.GetPart<Commerce>();
				double num = ((part != null) ? part.Value : 0.01);
				if (this.WantEvent(GetIntrinsicValueEvent.ID, MinEvent.CascadeLevel))
				{
					GetIntrinsicValueEvent getIntrinsicValueEvent = GetIntrinsicValueEvent.FromPool(this, num);
					this.HandleEvent(getIntrinsicValueEvent);
					num = getIntrinsicValueEvent.Value;
				}
				if (this.WantEvent(AdjustValueEvent.ID, MinEvent.CascadeLevel))
				{
					AdjustValueEvent adjustValueEvent = AdjustValueEvent.FromPool(this, num);
					this.HandleEvent(adjustValueEvent);
					num = adjustValueEvent.Value;
				}
				if (this.WantEvent(GetExtrinsicValueEvent.ID, MinEvent.CascadeLevel))
				{
					GetExtrinsicValueEvent getExtrinsicValueEvent = GetExtrinsicValueEvent.FromPool(this, num);
					this.HandleEvent(getExtrinsicValueEvent);
					num = getExtrinsicValueEvent.Value;
				}
				return num;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06005DE8 RID: 24040 RVA: 0x001C6178 File Offset: 0x001C4378
		public double Value
		{
			get
			{
				return this.ValueEach * (double)this.Count;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x001C6188 File Offset: 0x001C4388
		public int Weight
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return 0;
				}
				return physics.Weight;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x001C619B File Offset: 0x001C439B
		public int WeightEach
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return 0;
				}
				return physics.WeightEach;
			}
		}

		// Token: 0x06005DEB RID: 24043 RVA: 0x001C61AE File Offset: 0x001C43AE
		public double GetWeight()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return 0.0;
			}
			return physics.GetWeight();
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x001C61C9 File Offset: 0x001C43C9
		public int GetWeightTimes(double Factor)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return 0;
			}
			return physics.GetWeightTimes(Factor);
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06005DED RID: 24045 RVA: 0x001C61DD File Offset: 0x001C43DD
		public int IntrinsicWeight
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return 0;
				}
				return physics.IntrinsicWeight;
			}
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x001C61F0 File Offset: 0x001C43F0
		public double GetIntrinsicWeight()
		{
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return 0.0;
			}
			return physics.GetIntrinsicWeight();
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x001C620B File Offset: 0x001C440B
		public int GetIntrinsicWeightTimes(double Factor)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return 0;
			}
			return physics.GetIntrinsicWeightTimes(Factor);
		}

		// Token: 0x06005DF0 RID: 24048 RVA: 0x001C6220 File Offset: 0x001C4420
		public int GetCarriedWeight()
		{
			if (this.CarriedWeightCache != -1)
			{
				return this.CarriedWeightCache;
			}
			return this.CarriedWeightCache = GetCarriedWeightEvent.GetFor(this);
		}

		// Token: 0x06005DF1 RID: 24049 RVA: 0x001C624C File Offset: 0x001C444C
		public int GetMaxCarriedWeight()
		{
			if (this.MaxCarriedWeightCache != -1)
			{
				return this.MaxCarriedWeightCache;
			}
			return this.MaxCarriedWeightCache = GetMaxCarriedWeightEvent.GetFor(this, (double)(this.Stat("Strength", 0) * RuleSettings.MAXIMUM_CARRIED_WEIGHT_PER_STRENGTH));
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06005DF2 RID: 24050 RVA: 0x001C628B File Offset: 0x001C448B
		// (set) Token: 0x06005DF3 RID: 24051 RVA: 0x001C629E File Offset: 0x001C449E
		public int BaseElectricalConductivity
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return 0;
				}
				return physics.BaseElectricalConductivity;
			}
			set
			{
				if (this.Physics != null)
				{
					this.Physics.BaseElectricalConductivity = value;
				}
			}
		}

		// Token: 0x06005DF4 RID: 24052 RVA: 0x001C62B4 File Offset: 0x001C44B4
		public int GetElectricalConductivity(out GameObject ReductionObject, out string ReductionReason, GameObject Source = null, int Phase = 0)
		{
			ReductionObject = null;
			ReductionReason = null;
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return 0;
			}
			return physics.GetElectricalConductivity(out ReductionObject, out ReductionReason, Source, Phase);
		}

		// Token: 0x06005DF5 RID: 24053 RVA: 0x001C62D4 File Offset: 0x001C44D4
		public int GetElectricalConductivity(GameObject Source = null, int Phase = 0)
		{
			GameObject gameObject;
			string text;
			return this.GetElectricalConductivity(out gameObject, out text, Source, Phase);
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06005DF6 RID: 24054 RVA: 0x001C62ED File Offset: 0x001C44ED
		public int ElectricalConductivity
		{
			get
			{
				return this.GetElectricalConductivity(null, 0);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06005DF7 RID: 24055 RVA: 0x001C62F7 File Offset: 0x001C44F7
		// (set) Token: 0x06005DF8 RID: 24056 RVA: 0x001C630E File Offset: 0x001C450E
		public int Count
		{
			get
			{
				if (this.Stacker != null)
				{
					return this.Stacker.Number;
				}
				return 1;
			}
			set
			{
				if (this.Stacker != null)
				{
					this.Stacker.StackCount = value;
				}
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06005DF9 RID: 24057 RVA: 0x001C6324 File Offset: 0x001C4524
		public int Level
		{
			get
			{
				Statistic stat = this.GetStat("Level");
				if (stat == null)
				{
					return 1;
				}
				return stat.Value;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06005DFA RID: 24058 RVA: 0x001C633C File Offset: 0x001C453C
		public string Owner
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return null;
				}
				return physics.Owner;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x001C634F File Offset: 0x001C454F
		// (set) Token: 0x06005DFC RID: 24060 RVA: 0x001C6362 File Offset: 0x001C4562
		public string UsesSlots
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null)
				{
					return null;
				}
				return physics.UsesSlots;
			}
			set
			{
				if (this.Physics != null)
				{
					this.Physics.UsesSlots = value;
				}
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06005DFD RID: 24061 RVA: 0x001C6378 File Offset: 0x001C4578
		public string DebugName
		{
			get
			{
				if (this.IsPlayer())
				{
					string[] array = new string[5];
					array[0] = "Player:";
					array[1] = this.Blueprint;
					array[2] = " (";
					int num = 3;
					Render render = this.Render;
					array[num] = ((render != null) ? render.DisplayName : null) ?? "EMPTY";
					array[4] = ")";
					return string.Concat(array);
				}
				if (this.HasID)
				{
					string[] array2 = new string[6];
					array2[0] = this.ID;
					array2[1] = ":";
					array2[2] = this.Blueprint;
					array2[3] = " (";
					int num2 = 4;
					Render render2 = this.Render;
					array2[num2] = ((render2 != null) ? render2.DisplayName : null) ?? "EMPTY";
					array2[5] = ")";
					return string.Concat(array2);
				}
				string blueprint = this.Blueprint;
				string text = " (";
				Render render3 = this.Render;
				return blueprint + text + (((render3 != null) ? render3.DisplayName : null) ?? "EMPTY") + ")";
			}
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x001C6464 File Offset: 0x001C4664
		public string GetCachedDisplayNameForSort()
		{
			string text;
			if ((text = this._CachedDisplayNameForSort) == null)
			{
				text = (this._CachedDisplayNameForSort = this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, true, false, false, false, false, null, false, false, false, false, null, false, null, false, true));
			}
			return text;
		}

		// Token: 0x06005DFF RID: 24063 RVA: 0x001C64B0 File Offset: 0x001C46B0
		public void ResetNameCache()
		{
			this._CachedDisplayNameForSort = null;
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x001C64B9 File Offset: 0x001C46B9
		private string DisplayNameBase
		{
			get
			{
				Render render = this.Render;
				return ((render != null) ? render.DisplayName : null) ?? this.Blueprint;
			}
		}

		/// <summary>
		/// The full object display name with all modifiers.
		/// </summary>
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x001C64D8 File Offset: 0x001C46D8
		// (set) Token: 0x06005E02 RID: 24066 RVA: 0x001C6503 File Offset: 0x001C4703
		public string DisplayName
		{
			get
			{
				return GetDisplayNameEvent.GetFor(this, this.DisplayNameBase, int.MaxValue, null, false, false, false, false, false, true, false, false, false, false, false, true);
			}
			set
			{
				if (this.Render != null)
				{
					this.Render.DisplayName = value;
				}
			}
		}

		/// <summary>
		/// The full object display name with colors removed.
		/// </summary>
		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06005E03 RID: 24067 RVA: 0x001C651C File Offset: 0x001C471C
		public string DisplayNameStripped
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object display name without tags. Adjectives and clauses are
		/// included. This generally amounts to roughly "item + mods".
		/// Currently the same as ShortDisplayName.
		/// </summary>
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06005E04 RID: 24068 RVA: 0x001C6558 File Offset: 0x001C4758
		public string DisplayNameOnly
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object display name without tags and with colors removed.
		/// </summary>
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06005E05 RID: 24069 RVA: 0x001C6594 File Offset: 0x001C4794
		public string DisplayNameOnlyStripped
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06005E06 RID: 24070 RVA: 0x001C65CD File Offset: 0x001C47CD
		public string DisplayNameOnlyDirect
		{
			get
			{
				if (this.Render == null)
				{
					return "<unknown>";
				}
				return this.Render.DisplayName;
			}
		}

		/// <summary>
		/// The unmodified display name setting from the object's Render part,
		/// with any colors removed.
		/// </summary>
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06005E07 RID: 24071 RVA: 0x001C65E8 File Offset: 0x001C47E8
		public string DisplayNameOnlyDirectAndStripped
		{
			get
			{
				return this.DisplayNameOnlyDirect.Strip();
			}
		}

		/// <summary>
		/// The object display name without tags, suppressing any modification
		/// by the player's confusion state.
		/// </summary>
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06005E08 RID: 24072 RVA: 0x001C65F8 File Offset: 0x001C47F8
		public string DisplayNameOnlyUnconfused
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, true, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's full display name with any information about multiple
		/// stacked items suppressed.
		/// </summary>
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06005E09 RID: 24073 RVA: 0x001C6634 File Offset: 0x001C4834
		public string DisplayNameSingle
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without tags and with any information about
		/// multiple stacked items suppressed.
		/// </summary>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06005E0A RID: 24074 RVA: 0x001C6670 File Offset: 0x001C4870
		public string DisplayNameOnlySingle
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object display name without tags. Adjectives and clauses are
		/// included. This generally amounts to roughly "item + mods".
		/// Currently the same as DisplayNameOnly.
		/// </summary>
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06005E0B RID: 24075 RVA: 0x001C66AC File Offset: 0x001C48AC
		public string ShortDisplayName
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without tags and with any information about
		/// multiple stacked items suppressed.
		/// </summary>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06005E0C RID: 24076 RVA: 0x001C66E8 File Offset: 0x001C48E8
		public string ShortDisplayNameSingle
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without tags and with colors removed.
		/// </summary>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06005E0D RID: 24077 RVA: 0x001C6724 File Offset: 0x001C4924
		public string ShortDisplayNameStripped
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without tags, with any information about
		/// multiple stacked items suppressed, and with colors removed.
		/// </summary>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06005E0E RID: 24078 RVA: 0x001C6760 File Offset: 0x001C4960
		public string ShortDisplayNameSingleStripped
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without tags and with any name portion
		/// following a comma removed.
		/// </summary>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06005E0F RID: 24079 RVA: 0x001C679C File Offset: 0x001C499C
		public string ShortDisplayNameWithoutTitles
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without tags, with any name portion
		/// following a comma removed, and with colors removed.
		/// </summary>
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06005E10 RID: 24080 RVA: 0x001C67D8 File Offset: 0x001C49D8
		public string ShortDisplayNameWithoutTitlesStripped
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without adjectives, clauses, or tags;
		/// only alterations to the display name that are considered core
		/// to the object's identity are included.
		/// </summary>
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06005E11 RID: 24081 RVA: 0x001C6814 File Offset: 0x001C4A14
		public string BaseDisplayName
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, true, true, false, false, true, false, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name, as if fully known, without adjectives,
		/// clauses, or tags; only alterations to the display name that are
		/// considered core to the object's identity are included.
		/// </summary>
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06005E12 RID: 24082 RVA: 0x001C6850 File Offset: 0x001C4A50
		public string BaseKnownDisplayName
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, true, false, true, true, false, false, true, false, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The object's display name without adjectives, clauses, or tags, and
		/// with colors removed.
		/// </summary>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06005E13 RID: 24083 RVA: 0x001C688C File Offset: 0x001C4A8C
		public string BaseDisplayNameStripped
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, true, true, true, false, true, false, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// The main color of the object's display name.
		/// </summary>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06005E14 RID: 24084 RVA: 0x001C68C8 File Offset: 0x001C4AC8
		public string DisplayNameColor
		{
			get
			{
				return this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, true, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
		}

		/// <summary>
		/// Flexible retrieval of the object's display name.
		/// </summary>
		/// <param Name="AdjunctNounActive">
		/// Is set to true if conditions indicated that the object's adjunct
		/// noun should be used and one was available for use.
		/// </param>
		/// <param Name="Cutoff">
		/// If specified, description elements specified with an order greater
		/// than or equal to this value are excluded from the name being
		/// constructed. If Cutoff is low enough to exclude tags, the event
		/// used for retrieval will be GetShortDisplayNameEvent rather than
		/// GetDisplayNameEvent.
		/// </param>
		/// <param Name="Base">
		/// If specified, this will be used as the base name for the object in
		/// the name being constructed.
		/// </param>
		/// <param Name="Context">
		/// A name generation context that will be passed to the event dispatched.
		/// </param>
		/// <param Name="AsIfKnown">
		/// If true, the object will be treated as fully understood.
		/// </param>
		/// <param Name="Single">
		/// If true, stack information will be suppressed.
		/// </param>
		/// <param Name="NoConfusion">
		/// If true, effects of player confusion will be suppressed.
		/// </param>
		/// <param Name="NoColor">
		/// If true, color added via abstract mechanisms in the event will be
		/// suppressed. (This does not strip all color.)
		/// </param>
		/// <param Name="Stripped">
		/// If true, all color will be stripped from the display name.
		/// </param>
		/// <param Name="ColorOnly">
		/// If true, attempts to find the main color of the display name and
		/// return it.
		/// </param>
		/// <param Name="Visible">
		/// If true (the default), the object is being requested to be described
		/// as if it is visible. If false, we are requesting that it be
		/// described how it might be if one cannot see it.
		/// </param>
		/// <param Name="WithoutTitles">
		/// If true, display names are requested to be generated without titles.
		/// </param>
		/// <param Name="ForSort">
		/// If true, the name being requested is for sorting purposes.
		/// </param>
		/// <param Name="Short">
		/// If true, Cutoff is set to so as to exclude tags.
		/// </param>
		/// <param Name="BaseOnly">
		/// If true, only Base elements of the display name are included.
		/// </param>
		/// <param Name="WithIndefiniteArticle">
		/// If true, the return value should have the appropriate indefinite article,
		/// if any, prepended to the name.
		/// </param>
		/// <param Name="WithDefiniteArticle">
		/// If true, the return value should have the appropriate definite article,
		/// if any, prepended to the name.
		/// </param>
		/// <param Name="DefaultDefiniteArticle">
		/// If true, the provided value should be used as the default definite article
		/// rather than "the".
		/// </param>
		/// <param Name="IndicateHidden">
		/// If true and the object is hidden, the return value should have "hidden"
		/// inserted after any article and before the name if the object is hidden, and
		/// an indefinite article should be used rather than a definite one if any is.
		/// </param>
		/// <param Name="Capitalize">
		/// If true, the return value should be capitalized.
		/// </param>
		/// <param Name="SecondPerson">
		/// If true, the return value will be based on using "you" if this object is
		/// the player.
		/// </param>
		/// <param Name="Reflexive">
		/// If true, a return value that would have been "you" will instead be "yourself"
		/// or "yourselves".
		/// </param>
		/// <param Name="IncludeAdjunctNoun">
		/// If true, the return value will incorporate the object's adjunct noun, if
		/// any (so might be referred to as "a pair of boots" rather than "some boots",
		/// or "a copy of Corpus Choliys" rather than "Corpus Choliys", for example).
		/// If null, is treated as true if WithIndefiniteArticle is true and the item
		/// responds false to IsMassNoun().
		/// </param>
		/// <param Name="AsPossessed">
		/// If true, the object will be described as possessed by anyone equipping it,
		/// carrying it in inventory, or having it implanted found.
		/// </param>
		/// <param Name="AsPossessedBy">
		/// If not null and AsPossessed is true, the argument for this parameter will be
		/// used as the possessing object instead of one obtained automatically.
		/// </param>
		/// <param Name="Reference">
		/// If true, a long-term-stable reference name is being requested which should
		/// not include transient modifiers.
		/// </param>
		/// <returns>
		/// The display name to use for the object.
		/// </returns>
		/// <seealso cref="T:XRL.World.DescriptionBuilder">
		/// Defines the order mechanics that Cutoff interacts with.
		/// </seealso>
		// Token: 0x06005E15 RID: 24085 RVA: 0x001C6904 File Offset: 0x001C4B04
		public string GetDisplayName(out bool AdjunctNounActive, int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool ColorOnly = false, bool Visible = true, bool WithoutTitles = false, bool ForSort = false, bool Short = false, bool BaseOnly = false, bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, string DefaultDefiniteArticle = null, bool IndicateHidden = false, bool Capitalize = false, bool SecondPerson = false, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = false, GameObject AsPossessedBy = null, bool Reference = false, bool IncludeImplantPrefix = true)
		{
			AdjunctNounActive = false;
			if (Short)
			{
				Cutoff = 1040;
			}
			string text;
			if (SecondPerson && this.IsPlayer() && Grammar.AllowSecondPerson)
			{
				if (Reflexive)
				{
					if (this.GetPlurality(AsIfKnown))
					{
						text = (Capitalize ? "Yourselves" : "yourselves");
					}
					else
					{
						text = (Capitalize ? "Yourself" : "yourself");
					}
				}
				else
				{
					text = (Capitalize ? "You" : "you");
				}
			}
			else
			{
				AdjunctNounActive = IncludeAdjunctNoun ?? (WithIndefiniteArticle && !this.IsMassNoun(AsIfKnown));
				text = GetDisplayNameEvent.GetFor(this, Base ?? this.DisplayNameBase, Cutoff, Context, AsIfKnown, Single, NoConfusion, NoColor || Stripped, ColorOnly, Visible, BaseOnly, AdjunctNounActive, WithoutTitles, ForSort, Reference, true);
				if (Stripped)
				{
					text = text.Strip();
				}
				if (IndicateHidden && this.IsHidden)
				{
					text = "hidden " + text;
					if (WithDefiniteArticle)
					{
						WithDefiniteArticle = false;
						WithIndefiniteArticle = true;
					}
				}
				if (AdjunctNounActive)
				{
					string text2;
					bool flag;
					this.GetAdjunctNoun(out text2, out flag, AsIfKnown);
					if (!text2.IsNullOrEmpty())
					{
						if (flag)
						{
							text = text + " " + text2;
							AdjunctNounActive = false;
						}
						else
						{
							text = text2 + " of " + text;
						}
					}
					else
					{
						AdjunctNounActive = false;
					}
				}
				if (AsPossessed)
				{
					if (AsPossessedBy == null)
					{
						GameObject holder = this.Holder;
						if (holder != null && holder.IsCreature)
						{
							AsPossessedBy = holder;
						}
					}
					if (AsPossessedBy != null)
					{
						WithIndefiniteArticle = false;
						WithDefiniteArticle = true;
						if (SecondPerson && AsPossessedBy.IsPlayer())
						{
							DefaultDefiniteArticle = "your";
						}
						else
						{
							DefaultDefiniteArticle = Grammar.MakePossessive(AsPossessedBy.GetDisplayName(int.MaxValue, null, Context, AsIfKnown, false, NoConfusion, NoColor, Stripped, false, Visible, true, ForSort, true, false, false, Visible, null, IndicateHidden, false, false, false, null, false, null, Reference, true));
						}
					}
				}
				if (WithIndefiniteArticle)
				{
					text = this.IndefiniteArticle(Capitalize, text, AsIfKnown, AdjunctNounActive) + text;
					Capitalize = false;
				}
				if (WithDefiniteArticle)
				{
					text = this.DefiniteArticle(Capitalize, text, DefaultDefiniteArticle, AsIfKnown, AdjunctNounActive) + text;
					Capitalize = false;
				}
				if (Capitalize)
				{
					text = text.Capitalize();
				}
			}
			return text;
		}

		// Token: 0x06005E16 RID: 24086 RVA: 0x001C6B10 File Offset: 0x001C4D10
		public string GetDisplayName(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool ColorOnly = false, bool Visible = true, bool WithoutTitles = false, bool ForSort = false, bool Short = false, bool BaseOnly = false, bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, string DefaultDefiniteArticle = null, bool IndicateHidden = false, bool Capitalize = false, bool SecondPerson = false, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = false, GameObject AsPossessedBy = null, bool Reference = false, bool IncludeImplantPrefix = true)
		{
			bool flag;
			return this.GetDisplayName(out flag, Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, ColorOnly, Visible, WithoutTitles, ForSort, Short, BaseOnly, WithIndefiniteArticle, WithDefiniteArticle, DefaultDefiniteArticle, IndicateHidden, Capitalize, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, IncludeImplantPrefix);
		}

		// Token: 0x06005E17 RID: 24087 RVA: 0x001C6B58 File Offset: 0x001C4D58
		public string GetReferenceDisplayName(int Cutoff = 2147483647, string Base = null, string Context = null, bool NoColor = false, bool Stripped = false, bool ColorOnly = false, bool WithoutTitles = false, bool Short = false, bool BaseOnly = false, bool WithIndefiniteArticle = false, bool WithDefiniteArticle = false, string DefaultDefiniteArticle = null, bool Capitalize = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, true, true, true, NoColor, Stripped, ColorOnly, false, WithoutTitles, false, Short, BaseOnly, WithIndefiniteArticle, WithDefiniteArticle, DefaultDefiniteArticle, false, Capitalize, false, false, new bool?((WithIndefiniteArticle || WithDefiniteArticle) && this.HasPart<CyberneticsBaseItem>()), false, null, true, true);
		}

		// Token: 0x06005E18 RID: 24088 RVA: 0x001C6BA4 File Offset: 0x001C4DA4
		public string an(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = false, bool Short = true, bool BaseOnly = false, bool IndicateHidden = false, bool SecondPerson = true, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = false, GameObject AsPossessedBy = null, bool Reference = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, false, true, WithoutTitles, false, Short, BaseOnly, true, false, null, IndicateHidden, false, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
		}

		// Token: 0x06005E19 RID: 24089 RVA: 0x001C6BE0 File Offset: 0x001C4DE0
		public string An(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, bool IndicateHidden = false, bool SecondPerson = true, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = false, GameObject AsPossessedBy = null, bool Reference = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, false, true, WithoutTitles, false, Short, BaseOnly, true, false, null, IndicateHidden, true, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
		}

		// Token: 0x06005E1A RID: 24090 RVA: 0x001C6C1C File Offset: 0x001C4E1C
		public string t(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, string DefaultDefiniteArticle = null, bool IndicateHidden = false, bool SecondPerson = true, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = true, GameObject AsPossessedBy = null, bool Reference = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, false, true, WithoutTitles, false, Short, BaseOnly, false, true, DefaultDefiniteArticle, IndicateHidden, false, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
		}

		// Token: 0x06005E1B RID: 24091 RVA: 0x001C6C5C File Offset: 0x001C4E5C
		public string T(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, string DefaultDefiniteArticle = null, bool IndicateHidden = false, bool SecondPerson = true, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = true, GameObject AsPossessedBy = null, bool Reference = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, false, true, WithoutTitles, false, Short, BaseOnly, false, true, DefaultDefiniteArticle, IndicateHidden, true, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x001C6C9C File Offset: 0x001C4E9C
		public string one(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, bool WithIndefiniteArticle = false, string DefaultDefiniteArticle = null, bool IndicateHidden = true, bool SecondPerson = true, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = false, GameObject AsPossessedBy = null, bool Reference = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, false, true, WithoutTitles, false, Short, BaseOnly, WithIndefiniteArticle, !WithIndefiniteArticle, DefaultDefiniteArticle, IndicateHidden, false, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
		}

		// Token: 0x06005E1D RID: 24093 RVA: 0x001C6CE0 File Offset: 0x001C4EE0
		public string One(int Cutoff = 2147483647, string Base = null, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, bool WithIndefiniteArticle = false, string DefaultDefiniteArticle = null, bool IndicateHidden = true, bool SecondPerson = true, bool Reflexive = false, bool? IncludeAdjunctNoun = null, bool AsPossessed = false, GameObject AsPossessedBy = null, bool Reference = false)
		{
			return this.GetDisplayName(Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, false, true, WithoutTitles, false, Short, BaseOnly, WithIndefiniteArticle, !WithIndefiniteArticle, DefaultDefiniteArticle, IndicateHidden, true, SecondPerson, Reflexive, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x001C6D24 File Offset: 0x001C4F24
		public string does(string Verb, int Cutoff = 2147483647, string Base = null, string Context = null, string Adverb = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, bool WithIndefiniteArticle = false, string DefaultDefiniteArticle = null, bool IndicateHidden = false, bool Pronoun = false, bool SecondPerson = true, bool? IncludeAdjunctNoun = null, bool AsPossessed = true, GameObject AsPossessedBy = null, bool Reference = false)
		{
			bool flag = false;
			string text;
			if (!Pronoun)
			{
				bool flag2 = false;
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = !WithIndefiniteArticle;
				text = this.GetDisplayName(out flag, Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, flag2, flag3, WithoutTitles, flag4, Short, BaseOnly, WithIndefiniteArticle, flag5, DefaultDefiniteArticle, IndicateHidden, false, SecondPerson, false, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
			}
			else
			{
				text = ((SecondPerson && Grammar.AllowSecondPerson) ? this.it : this.GetPronounProvider(false).Subjective);
			}
			string text2 = text;
			string verb = this.GetVerb(Verb, true, Pronoun, flag, SecondPerson, AsIfKnown);
			if (text2.Contains(","))
			{
				if (!Adverb.IsNullOrEmpty())
				{
					return text2 + ", " + Adverb + verb;
				}
				return text2 + "," + verb;
			}
			else
			{
				if (!Adverb.IsNullOrEmpty())
				{
					return text2 + " " + Adverb + verb;
				}
				return text2 + verb;
			}
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x001C6DF4 File Offset: 0x001C4FF4
		public string Does(string Verb, int Cutoff = 2147483647, string Base = null, string Context = null, string Adverb = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool Stripped = false, bool WithoutTitles = true, bool Short = true, bool BaseOnly = false, bool WithIndefiniteArticle = false, string DefaultDefiniteArticle = null, bool IndicateHidden = false, bool Pronoun = false, bool SecondPerson = true, bool? IncludeAdjunctNoun = null, bool AsPossessed = true, GameObject AsPossessedBy = null, bool Reference = false)
		{
			bool flag = false;
			string text;
			if (!Pronoun)
			{
				bool flag2 = false;
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = !WithIndefiniteArticle;
				text = this.GetDisplayName(out flag, Cutoff, Base, Context, AsIfKnown, Single, NoConfusion, NoColor, Stripped, flag2, flag3, WithoutTitles, flag4, Short, BaseOnly, WithIndefiniteArticle, flag5, DefaultDefiniteArticle, IndicateHidden, true, SecondPerson, false, IncludeAdjunctNoun, AsPossessed, AsPossessedBy, Reference, true);
			}
			else
			{
				text = ((SecondPerson && Grammar.AllowSecondPerson) ? this.It : this.GetPronounProvider(false).CapitalizedSubjective);
			}
			string text2 = text;
			string verb = this.GetVerb(Verb, true, Pronoun, flag, SecondPerson, AsIfKnown);
			if (text2.Contains(","))
			{
				if (!Adverb.IsNullOrEmpty())
				{
					return text2 + ", " + Adverb + verb;
				}
				return text2 + "," + verb;
			}
			else
			{
				if (!Adverb.IsNullOrEmpty())
				{
					return text2 + " " + Adverb + verb;
				}
				return text2 + verb;
			}
		}

		// Token: 0x06005E20 RID: 24096 RVA: 0x001C6EC4 File Offset: 0x001C50C4
		public bool LooksLikeOneOfGroup()
		{
			if (this.Count > 1)
			{
				return true;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			string shortDisplayName = this.ShortDisplayName;
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				if (currentCell.Objects[i] != this && currentCell.Objects[i].ShortDisplayName == shortDisplayName)
				{
					return true;
				}
				i++;
			}
			if (this.IsCreature)
			{
				foreach (Cell cell in currentCell.GetLocalAdjacentCells())
				{
					int j = 0;
					int count2 = cell.Objects.Count;
					while (j < count2)
					{
						if (cell.Objects[j] != this && cell.Objects[j].ShortDisplayName == shortDisplayName)
						{
							return true;
						}
						j++;
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>
		/// The object blueprint tags that control gender and pronoun set setup are:
		///
		/// Gender: this can be used to specify exactly one gender name from Genders.xml that
		/// will be assigned to the object.  If both Gender and RandomGender are specified,
		/// Gender controls.
		///
		/// RandomGender: this can be used to specify a comma-separated list (no spaces around
		/// the commas) of gender specifiers, one of which will be randomly selected.  The
		/// specifiers may be gender names or abstract specifications from the following list.
		/// ("Personal" means UseBareIndicative is false, which essentially means the gender
		/// is treated as being for a person rather than a thing.  "Singular" means Plural is
		/// false, that is, the gender addresses a singular subject.  "Generic" means the
		/// gender is considered generic to the world rather than specific to an individual or
		/// group.)
		///
		///   - generate: if EnableGeneration is true in Genders.xml, procedurally generate
		///     a singular personal gender that will be registered with the system as non-generic;
		///     otherwise select a random personal singular gender
		///   - generatemaybeplural: as generate, but with a 10% chance of being plural
		///   - generatemaybenonpersonal: as generate, but with a 10% chance of being non-personal
		///   - generatemaybepluralmaybenonpersonal: as generate, but with a 10% chance
		///     of being plural and a 10% chance of being non-personal
		///   - any: randomly select from any gender in the system
		///   - anyplural: randomly select a plural gender
		///   - anysingular: randomly select a singular gender
		///   - generic: randomly select a generic gender
		///   - genericplural: randomly select a generic plural gender
		///   - genericsingular: randomly select a generic singular gender
		///   - personal: randomly select a personal gender
		///   - personalplural: randomly select a personal plural gender
		///   - personalsingular: randomly select a personal singular gender
		///   - genericpersonal: randomly select a generic personal gender
		///   - genericpersonalplural: randomly select a generic personal plural gender
		///   - genericpersonalsingular: randomly select a generic personal singular gender
		///   - nonpersonal: randomly select a nonpersonal gender
		///   - nonpersonalplural: randomly select a nonpersonal plural gender
		///   - nonpersonalsingular: randomly select a nonpersonal singular gender
		///   - genericnonpersonal: randomly select a generic nonpersonal gender
		///   - genericnonpersonalplural: randomly select a generic nonpersonal plural gender
		///   - genericnonpersonalsingular: randomly select a generic nonpersonal singular gender
		///
		/// PronounSet: this can be used to specify a pronoun set that will be assigned to the
		/// object.  This can be the full slash-separated set of values that make up a fully
		/// characterized pronoun set name (see PronounSet.CalculateName()) or a limited subset
		/// of these, like "xe/xem/xyr", from which the system will attempt to derive the full
		/// set of pronouns as best it can.  If both PronounSet and RandomPronounSet are specified,
		/// PronounSet controls.
		///
		/// RandomPronounSet: similar to RandomGender, but operating on pronoun sets.  The same set
		/// of abstract specifications are available, referring to pronoun sets rather than genders
		/// (and the generation control used is the one in PronounSets.xml).
		///
		/// RandomPronounSetChance: if specified, RandomPronounSet will only take effect this
		/// percentage of the time.  Otherwise, it always takes effect.
		/// </summary>
		// Token: 0x06005E21 RID: 24097 RVA: 0x001C6FD4 File Offset: 0x001C51D4
		public Gender GetGender(bool AsIfKnown = false)
		{
			if (this.GenderName == null)
			{
				if (!this.IsOriginalPlayerBody())
				{
					this.GenderName = this.GetPropertyOrTag("Gender", null);
					if (this.GenderName.IsNullOrEmpty())
					{
						string propertyOrTag = this.GetPropertyOrTag("RandomGender", null);
						if (!propertyOrTag.IsNullOrEmpty())
						{
							this.GenderName = propertyOrTag.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
						}
						this.GenderName = Gender.CheckSpecial(this.GenderName);
					}
				}
				if (this.GenderName == null)
				{
					string text = ((this.Brain == null) ? "neuter" : "nonspecific");
					this.GenderName = (Gender.Exists(text) ? text : Gender.GetAllGenericPersonalSingular()[0].Name);
				}
			}
			return Gender.Get(GetGenderEvent.GetFor(this, this.GenderName, AsIfKnown), false);
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x001C709F File Offset: 0x001C529F
		public void SetGender(Gender Spec)
		{
			this.GenderName = Spec.Name;
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x001C70AD File Offset: 0x001C52AD
		public void SetGender(string Name)
		{
			this.SetGender(Gender.Get(Gender.CheckSpecial(Name), false));
		}

		// Token: 0x06005E24 RID: 24100 RVA: 0x001C70C4 File Offset: 0x001C52C4
		public PronounSet GetPronounSet()
		{
			if (this.PronounSetName == "")
			{
				return null;
			}
			if (this.PronounSetName == null)
			{
				this.PronounSetName = this.GetPropertyOrTag("PronounSet", null);
				if (this.PronounSetName.IsNullOrEmpty())
				{
					string propertyOrTag = this.GetPropertyOrTag("RandomPronounSet", null);
					if (!propertyOrTag.IsNullOrEmpty())
					{
						string propertyOrTag2 = this.GetPropertyOrTag("RandomPronounSetChance", null);
						if (propertyOrTag2.IsNullOrEmpty() || Convert.ToInt32(propertyOrTag2).in100())
						{
							this.PronounSetName = propertyOrTag.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
						}
						this.PronounSetName = PronounSet.CheckSpecial(this.PronounSetName);
					}
					if (this.PronounSetName == null)
					{
						this.PronounSetName = "";
						return null;
					}
				}
			}
			return PronounSet.Get(this.PronounSetName);
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x001C718C File Offset: 0x001C538C
		public void SetPronounSet(PronounSet Spec)
		{
			this.PronounSetName = ((Spec != null) ? Spec.Name : null);
			this.SetPronounSetKnown(false);
		}

		// Token: 0x06005E26 RID: 24102 RVA: 0x001C71A7 File Offset: 0x001C53A7
		public void SetPronounSet(string Name)
		{
			this.SetPronounSet(PronounSet.Get(PronounSet.CheckSpecial(Name)));
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x001C71BA File Offset: 0x001C53BA
		public void ClearPronounSet()
		{
			this.PronounSetName = null;
			this.SetPronounSetKnown(false);
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x001C71CC File Offset: 0x001C53CC
		public bool IsPronounSetKnown()
		{
			return this.Flags.HasBit(1) || !PronounSet.EnableConversationalExchange || this.IsPlayer() || this.WasPlayer() || this.HasCopyRelationship(this.ThePlayer) || XRL.The.Player == null || XRL.The.Player.HasSkill("Customs_Tactful");
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x001C7233 File Offset: 0x001C5433
		public PronounSet GetPronounSetIfKnown()
		{
			if (!this.IsPronounSetKnown())
			{
				return null;
			}
			return this.GetPronounSet();
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x001C7248 File Offset: 0x001C5448
		public IPronounProvider GetPronounProvider(bool AsIfKnown = false)
		{
			IPronounProvider pronounSetIfKnown = this.GetPronounSetIfKnown();
			return pronounSetIfKnown ?? this.GetGender(AsIfKnown);
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x001C7268 File Offset: 0x001C5468
		public string Its
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedPossessiveAdjective;
				}
				return "Your";
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06005E2C RID: 24108 RVA: 0x001C728B File Offset: 0x001C548B
		public string its
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).PossessiveAdjective;
				}
				return "your";
			}
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x001C72AE File Offset: 0x001C54AE
		public string Its_(GameObject Object)
		{
			if (Object.HasProperName)
			{
				return Object.ShortDisplayName.Capitalize();
			}
			return this.Its + " " + Object.ShortDisplayName;
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x001C72DC File Offset: 0x001C54DC
		public string its_(GameObject Object, bool IncludeAdjunctNoun = false)
		{
			return Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, this.its, false, false, false, false, new bool?(IncludeAdjunctNoun), false, null, false, true);
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x001C7318 File Offset: 0x001C5518
		public void its_(GameObject Object, StringBuilder AppendTo, bool IncludeAdjunctNoun = false)
		{
			if (AppendTo != null)
			{
				AppendTo.Append(Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, this.its, false, false, false, false, new bool?(IncludeAdjunctNoun), false, null, false, true));
			}
		}

		// Token: 0x06005E30 RID: 24112 RVA: 0x001C7360 File Offset: 0x001C5560
		public string Poss(GameObject Object, bool Definite = true, bool? IncludeAdjunctNoun = null)
		{
			if (this.IsPlayer() && Grammar.AllowSecondPerson)
			{
				return Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, "Your", false, true, false, false, IncludeAdjunctNoun, false, null, false, true);
			}
			return Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, Grammar.MakePossessive(this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, !Definite, Definite, null, false, false, false, false, null, false, null, false, true)), false, true, false, false, IncludeAdjunctNoun, false, null, false, true);
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x001C7400 File Offset: 0x001C5600
		public string Poss(string text, bool Definite = true)
		{
			if (this.IsPlayer() && Grammar.AllowSecondPerson)
			{
				return "Your " + text;
			}
			return Grammar.MakePossessive(this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, !Definite, Definite, null, false, true, false, false, null, false, null, false, true)) + " " + text;
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x001C746C File Offset: 0x001C566C
		public string poss(GameObject Object, bool Definite = true, bool? IncludeAdjunctNoun = null)
		{
			if (this.IsPlayer() && Grammar.AllowSecondPerson)
			{
				return Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, "your", false, false, false, false, IncludeAdjunctNoun, false, null, false, true);
			}
			return Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, Grammar.MakePossessive(this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, !Definite, Definite, null, false, false, false, false, null, false, null, false, true)), false, false, false, false, IncludeAdjunctNoun, false, null, false, true);
		}

		// Token: 0x06005E33 RID: 24115 RVA: 0x001C750C File Offset: 0x001C570C
		public string poss(string text, bool Definite = true)
		{
			if (this.IsPlayer() && Grammar.AllowSecondPerson)
			{
				return "your " + text;
			}
			return Grammar.MakePossessive(this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, !Definite, Definite, null, false, false, false, false, null, false, null, false, true)) + " " + text;
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06005E34 RID: 24116 RVA: 0x001C7575 File Offset: 0x001C5775
		public string It
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedSubjective;
				}
				return "You";
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06005E35 RID: 24117 RVA: 0x001C7598 File Offset: 0x001C5798
		public string it
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).Subjective;
				}
				return "you";
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06005E36 RID: 24118 RVA: 0x001C75BB File Offset: 0x001C57BB
		public string Itself
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedReflexive;
				}
				if (!this.IsPlural)
				{
					return "Yourself";
				}
				return "Yourselves";
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06005E37 RID: 24119 RVA: 0x001C75EC File Offset: 0x001C57EC
		public string itself
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).Reflexive;
				}
				if (!this.IsPlural)
				{
					return "yourself";
				}
				return "yourselves";
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06005E38 RID: 24120 RVA: 0x001C761D File Offset: 0x001C581D
		public string Them
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedObjective;
				}
				return "You";
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06005E39 RID: 24121 RVA: 0x001C7640 File Offset: 0x001C5840
		public string them
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).Objective;
				}
				return "you";
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06005E3A RID: 24122 RVA: 0x001C7663 File Offset: 0x001C5863
		public string theirs
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).SubstantivePossessive;
				}
				return "yours";
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06005E3B RID: 24123 RVA: 0x001C7686 File Offset: 0x001C5886
		public string Theirs
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedSubstantivePossessive;
				}
				return "Yours";
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x001C76A9 File Offset: 0x001C58A9
		public string indicativeProximal
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).IndicativeProximal;
				}
				return "you";
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x001C76CC File Offset: 0x001C58CC
		public string IndicativeProximal
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedIndicativeProximal;
				}
				return "you";
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x001C76EF File Offset: 0x001C58EF
		public string indicativeDistal
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).IndicativeDistal;
				}
				return "you";
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x001C7712 File Offset: 0x001C5912
		public string IndicativeDistal
		{
			get
			{
				if (!this.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return this.GetPronounProvider(false).CapitalizedIndicativeDistal;
				}
				return "You";
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06005E40 RID: 24128 RVA: 0x001C7735 File Offset: 0x001C5935
		public bool UseBareIndicative
		{
			get
			{
				return this.GetPronounProvider(false).UseBareIndicative;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x001C7744 File Offset: 0x001C5944
		public string YouAre
		{
			get
			{
				if (this.IsPlayer() && Grammar.AllowSecondPerson)
				{
					return "You are";
				}
				IPronounProvider pronounProvider = this.GetPronounProvider(false);
				if (!pronounProvider.Plural && !pronounProvider.PseudoPlural)
				{
					return pronounProvider.CapitalizedSubjective + " is";
				}
				return pronounProvider.CapitalizedSubjective + " are";
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x001C77A0 File Offset: 0x001C59A0
		public string Itis
		{
			get
			{
				if (this.IsPlayer() && Grammar.AllowSecondPerson)
				{
					return "You're";
				}
				IPronounProvider pronounProvider = this.GetPronounProvider(false);
				if (!pronounProvider.Plural && !pronounProvider.PseudoPlural)
				{
					return pronounProvider.CapitalizedSubjective + "'s";
				}
				return pronounProvider.CapitalizedSubjective + "'re";
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x001C77FC File Offset: 0x001C59FC
		public string itis
		{
			get
			{
				if (this.IsPlayer() && Grammar.AllowSecondPerson)
				{
					return "you're";
				}
				IPronounProvider pronounProvider = this.GetPronounProvider(false);
				if (!pronounProvider.Plural && !pronounProvider.PseudoPlural)
				{
					return pronounProvider.Subjective + "'s";
				}
				return pronounProvider.Subjective + "'re";
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06005E44 RID: 24132 RVA: 0x001C7858 File Offset: 0x001C5A58
		public string Ithas
		{
			get
			{
				if (this.IsPlayer() && Grammar.AllowSecondPerson)
				{
					return "You've";
				}
				IPronounProvider pronounProvider = this.GetPronounProvider(false);
				if (!pronounProvider.Plural && !pronounProvider.PseudoPlural)
				{
					return pronounProvider.CapitalizedSubjective + "'s";
				}
				return pronounProvider.CapitalizedSubjective + "'ve";
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06005E45 RID: 24133 RVA: 0x001C78B4 File Offset: 0x001C5AB4
		public string ithas
		{
			get
			{
				if (this.IsPlayer() && Grammar.AllowSecondPerson)
				{
					return "you've";
				}
				IPronounProvider pronounProvider = this.GetPronounProvider(false);
				if (!pronounProvider.Plural && !pronounProvider.PseudoPlural)
				{
					return pronounProvider.Subjective + "'s";
				}
				return pronounProvider.Subjective + "'ve";
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x001C790F File Offset: 0x001C5B0F
		public string personTerm
		{
			get
			{
				return this.GetPronounProvider(false).PersonTerm;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x001C791D File Offset: 0x001C5B1D
		public string PersonTerm
		{
			get
			{
				return this.GetPronounProvider(false).CapitalizedPersonTerm;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06005E48 RID: 24136 RVA: 0x001C792B File Offset: 0x001C5B2B
		public string immaturePersonTerm
		{
			get
			{
				return this.GetPronounProvider(false).ImmaturePersonTerm;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06005E49 RID: 24137 RVA: 0x001C7939 File Offset: 0x001C5B39
		public string ImmaturePersonTerm
		{
			get
			{
				return this.GetPronounProvider(false).CapitalizedImmaturePersonTerm;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06005E4A RID: 24138 RVA: 0x001C7947 File Offset: 0x001C5B47
		public string formalAddressTerm
		{
			get
			{
				return this.GetPronounProvider(false).FormalAddressTerm;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06005E4B RID: 24139 RVA: 0x001C7955 File Offset: 0x001C5B55
		public string FormalAddressTerm
		{
			get
			{
				return this.GetPronounProvider(false).CapitalizedFormalAddressTerm;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06005E4C RID: 24140 RVA: 0x001C7963 File Offset: 0x001C5B63
		public string offspringTerm
		{
			get
			{
				return this.GetPronounProvider(false).OffspringTerm;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06005E4D RID: 24141 RVA: 0x001C7971 File Offset: 0x001C5B71
		public string OffspringTerm
		{
			get
			{
				return this.GetPronounProvider(false).CapitalizedOffspringTerm;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06005E4E RID: 24142 RVA: 0x001C797F File Offset: 0x001C5B7F
		public string siblingTerm
		{
			get
			{
				return this.GetPronounProvider(false).SiblingTerm;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x001C798D File Offset: 0x001C5B8D
		public string SiblingTerm
		{
			get
			{
				return this.GetPronounProvider(false).CapitalizedSiblingTerm;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06005E50 RID: 24144 RVA: 0x001C799B File Offset: 0x001C5B9B
		public string parentTerm
		{
			get
			{
				return this.GetPronounProvider(false).ParentTerm;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x001C79A9 File Offset: 0x001C5BA9
		public string ParentTerm
		{
			get
			{
				return this.GetPronounProvider(false).CapitalizedParentTerm;
			}
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x001C79B8 File Offset: 0x001C5BB8
		public bool IsMassNoun(bool AsIfKnown = false)
		{
			Examiner examiner;
			if (!AsIfKnown && this.TryGetPart<Examiner>(out examiner))
			{
				GameObject activeSample = examiner.GetActiveSample();
				if (activeSample != null)
				{
					return activeSample.IsMassNoun(false);
				}
			}
			return this.GetxTag("Grammar", "massNoun", null) == "true";
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x001C7A00 File Offset: 0x001C5C00
		public string IndefiniteArticle(bool Capital = false, string Word = null, bool AsIfKnown = false, bool UsingAdjunctNoun = false)
		{
			Examiner examiner;
			if (!AsIfKnown && this.TryGetPart<Examiner>(out examiner))
			{
				GameObject activeSample = examiner.GetActiveSample();
				if (activeSample != null)
				{
					return activeSample.IndefiniteArticle(Capital, Word, false, UsingAdjunctNoun);
				}
			}
			string propertyOrTag = this.GetPropertyOrTag("IndefiniteArticle", null);
			if (!propertyOrTag.IsNullOrEmpty())
			{
				return (Capital ? propertyOrTag.Capitalize() : propertyOrTag) + " ";
			}
			if (!UsingAdjunctNoun && this.HasProperName)
			{
				return "";
			}
			if (!UsingAdjunctNoun)
			{
				string propertyOrTag2 = this.GetPropertyOrTag("OverrideIArticle", this.GetxTag("Grammar", "iArticle", null));
				if (!propertyOrTag2.IsNullOrEmpty())
				{
					return (Capital ? propertyOrTag2.Capitalize() : propertyOrTag2) + " ";
				}
				if (this.IsMassNoun(AsIfKnown) || this.GetPlurality(AsIfKnown))
				{
					if (!Capital)
					{
						return "some ";
					}
					return "Some ";
				}
			}
			if (Word == null)
			{
				Word = this.ShortDisplayName;
			}
			if (!Grammar.IndefiniteArticleShouldBeAn(Word))
			{
				if (!Capital)
				{
					return "a ";
				}
				return "A ";
			}
			else
			{
				if (!Capital)
				{
					return "an ";
				}
				return "An ";
			}
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x001C7B00 File Offset: 0x001C5D00
		public void IndefiniteArticle(StringBuilder SB, bool Capital = false, string Word = null, bool AsIfKnown = false, bool UsingAdjunctNoun = false)
		{
			Examiner examiner;
			if (!AsIfKnown && this.TryGetPart<Examiner>(out examiner))
			{
				GameObject activeSample = examiner.GetActiveSample();
				if (activeSample != null)
				{
					activeSample.IndefiniteArticle(SB, Capital, Word, false, UsingAdjunctNoun);
					return;
				}
			}
			string propertyOrTag = this.GetPropertyOrTag("IndefiniteArticle", null);
			if (!propertyOrTag.IsNullOrEmpty())
			{
				SB.Append(Capital ? propertyOrTag.Capitalize() : propertyOrTag).Append(' ');
				return;
			}
			if (!UsingAdjunctNoun && this.HasProperName)
			{
				return;
			}
			if (!UsingAdjunctNoun)
			{
				string text = this.GetxTag("Grammar", "iArticle", null);
				if (this.HasPropertyOrTag("OverrideIArticle"))
				{
					text = this.GetPropertyOrTag("OverrideIArticle", null);
				}
				if (!text.IsNullOrEmpty())
				{
					SB.Append(Capital ? text.Capitalize() : text).Append(' ');
					return;
				}
				if (this.IsMassNoun(AsIfKnown) || this.GetPlurality(AsIfKnown))
				{
					SB.Append(Capital ? "Some " : "some ");
					return;
				}
			}
			if (Word == null)
			{
				Word = this.ShortDisplayName;
			}
			SB.Append(Grammar.IndefiniteArticleShouldBeAn(Word) ? (Capital ? "An " : "an ") : (Capital ? "A " : "a "));
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x001C7C28 File Offset: 0x001C5E28
		public string DefiniteArticle(bool Capital = false, string Word = null, string UseAsDefault = null, bool AsIfKnown = false, bool UsingAdjunctNoun = false)
		{
			Examiner examiner;
			if (!AsIfKnown && this.TryGetPart<Examiner>(out examiner))
			{
				GameObject activeSample = examiner.GetActiveSample();
				if (activeSample != null)
				{
					return activeSample.DefiniteArticle(Capital, Word, UseAsDefault, false, UsingAdjunctNoun);
				}
			}
			string propertyOrTag = this.GetPropertyOrTag("DefiniteArticle", null);
			if (!propertyOrTag.IsNullOrEmpty())
			{
				return (Capital ? propertyOrTag.Capitalize() : propertyOrTag) + " ";
			}
			if (!UsingAdjunctNoun && this.HasProperName)
			{
				return "";
			}
			if (!UsingAdjunctNoun)
			{
				string text = this.GetxTag("Grammar", "dArticle", null);
				if (!text.IsNullOrEmpty())
				{
					return (Capital ? text.Capitalize() : text) + " ";
				}
			}
			if (UseAsDefault != null)
			{
				if (UseAsDefault == "")
				{
					return "";
				}
				return (Capital ? UseAsDefault.Capitalize() : UseAsDefault) + " ";
			}
			else
			{
				if (!Capital)
				{
					return "the ";
				}
				return "The ";
			}
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x001C7D08 File Offset: 0x001C5F08
		public void DefiniteArticle(StringBuilder SB, bool Capital = false, string Word = null, string UseAsDefault = null, bool AsIfKnown = false, bool UsingAdjunctNoun = false)
		{
			Examiner examiner;
			if (!AsIfKnown && this.TryGetPart<Examiner>(out examiner))
			{
				GameObject activeSample = examiner.GetActiveSample();
				if (activeSample != null)
				{
					activeSample.DefiniteArticle(SB, Capital, Word, UseAsDefault, false, false);
					return;
				}
			}
			string propertyOrTag = this.GetPropertyOrTag("DefiniteArticle", null);
			if (!propertyOrTag.IsNullOrEmpty())
			{
				SB.Append(Capital ? propertyOrTag.Capitalize() : propertyOrTag).Append(' ');
				return;
			}
			if (!UsingAdjunctNoun && this.HasProperName)
			{
				return;
			}
			if (!UsingAdjunctNoun)
			{
				string text = this.GetxTag("Grammar", "dArticle", null);
				if (!text.IsNullOrEmpty())
				{
					SB.Append(Capital ? text.Capitalize() : text).Append(' ');
					return;
				}
			}
			if (UseAsDefault != null)
			{
				if (UseAsDefault != "")
				{
					SB.Append(Capital ? UseAsDefault.Capitalize() : UseAsDefault).Append(' ');
				}
				return;
			}
			SB.Append(Capital ? "The " : "the ");
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x001C7DF8 File Offset: 0x001C5FF8
		public string A
		{
			get
			{
				string text;
				bool flag;
				this.GetAdjunctNoun(out text, out flag, false);
				if (!text.IsNullOrEmpty() && !flag)
				{
					return this.IndefiniteArticle(true, text, false, true) + text + " of ";
				}
				return this.IndefiniteArticle(true, null, false, false);
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x001C7E3C File Offset: 0x001C603C
		public string a
		{
			get
			{
				string text;
				bool flag;
				this.GetAdjunctNoun(out text, out flag, false);
				if (!text.IsNullOrEmpty() && !flag)
				{
					return this.IndefiniteArticle(false, text, false, true) + text + " of ";
				}
				return this.IndefiniteArticle(false, null, false, false);
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x001C7E7F File Offset: 0x001C607F
		public string The
		{
			get
			{
				return this.DefiniteArticle(true, null, null, false, false);
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x001C7E8C File Offset: 0x001C608C
		public string the
		{
			get
			{
				return this.DefiniteArticle(false, null, null, false, false);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06005E5B RID: 24155 RVA: 0x001C7E99 File Offset: 0x001C6099
		public string Is
		{
			get
			{
				return this.GetVerb("are", true, false, false, true, false);
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06005E5C RID: 24156 RVA: 0x001C7EAB File Offset: 0x001C60AB
		public string Has
		{
			get
			{
				return this.GetVerb("have", true, false, false, true, false);
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06005E5D RID: 24157 RVA: 0x001C7EC0 File Offset: 0x001C60C0
		// (set) Token: 0x06005E5E RID: 24158 RVA: 0x001C7F14 File Offset: 0x001C6114
		public bool HasProperName
		{
			get
			{
				Examiner part = this.GetPart<Examiner>();
				if (part != null)
				{
					GameObject activeSample = part.GetActiveSample();
					if (activeSample != null)
					{
						return activeSample.HasProperName;
					}
				}
				int intProperty = this.GetIntProperty("ProperNoun", 0);
				if (intProperty > 0)
				{
					return true;
				}
				if (intProperty < 0)
				{
					return false;
				}
				GameObjectBlueprint blueprint = this.GetBlueprint(false);
				return blueprint != null && blueprint.HasProperName();
			}
			set
			{
				if (value)
				{
					this.SetIntProperty("ProperNoun", 1, false);
					return;
				}
				this.SetIntProperty("ProperNoun", -1, false);
			}
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x001C7F36 File Offset: 0x001C6136
		public bool GetPlurality(bool AsIfKnown = false)
		{
			return this.GetPronounProvider(AsIfKnown).Plural;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06005E60 RID: 24160 RVA: 0x001C7F44 File Offset: 0x001C6144
		public bool IsPlural
		{
			get
			{
				return this.GetPronounProvider(false).Plural;
			}
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x001C7F52 File Offset: 0x001C6152
		public bool GetPseudoPlurality(bool AsIfKnown = false)
		{
			return this.GetPronounProvider(AsIfKnown).PseudoPlural;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06005E62 RID: 24162 RVA: 0x001C7F60 File Offset: 0x001C6160
		public bool IsPseudoPlural
		{
			get
			{
				return this.GetPronounProvider(false).PseudoPlural;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06005E63 RID: 24163 RVA: 0x001C7F6E File Offset: 0x001C616E
		public bool IsPluralIfKnown
		{
			get
			{
				return this.GetPronounProvider(true).Plural;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06005E64 RID: 24164 RVA: 0x001C7F7C File Offset: 0x001C617C
		public bool IsPseudoPluralIfKnown
		{
			get
			{
				return this.GetPronounProvider(true).PseudoPlural;
			}
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x001C7F8A File Offset: 0x001C618A
		public string getSingularSemantic(string name, string defaultResult)
		{
			return Semantics.GetSingularSemantic(name, this, defaultResult);
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x001C7F94 File Offset: 0x001C6194
		public string getPluralSemantic(string name, string defaultResult)
		{
			return Semantics.GetPluralSemantic(name, this, defaultResult);
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x001C7FA0 File Offset: 0x001C61A0
		public bool Twiddle(Action After, bool Distant, bool MouseClick = false)
		{
			bool flag = false;
			EquipmentAPI.TwiddleObject(this, ref flag, After, Distant, false, MouseClick);
			return flag;
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x001C7FBC File Offset: 0x001C61BC
		public bool Twiddle(Action After, bool MouseClick = false)
		{
			bool flag = ((this.CurrentCell != null) ? this.ThePlayer.DistanceTo(this) : 0) > 1 || this.ThePlayer.IsFrozen();
			return this.Twiddle(After, flag, MouseClick);
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x001C7FFB File Offset: 0x001C61FB
		public bool Twiddle(bool MouseClick = false)
		{
			return this.Twiddle(null, MouseClick);
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x001C8008 File Offset: 0x001C6208
		public bool TelekineticTwiddle(Action After = null)
		{
			bool flag = false;
			EquipmentAPI.TwiddleObject(this, ref flag, After, true, true, false);
			return flag;
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x001C8024 File Offset: 0x001C6224
		public string GetVerb(string Verb, bool PrependSpace = true, bool PronounAntecedent = false, bool AdjunctAntecedent = false, bool SecondPerson = true, bool AsIfKnown = false)
		{
			if (!AdjunctAntecedent)
			{
				if ((SecondPerson && this.IsPlayer() && Grammar.AllowSecondPerson) || this.GetPlurality(AsIfKnown))
				{
					if (!PrependSpace)
					{
						return Verb;
					}
					return " " + Verb;
				}
				else if (PronounAntecedent && this.GetPseudoPlurality(AsIfKnown))
				{
					if (!PrependSpace)
					{
						return Verb;
					}
					return " " + Verb;
				}
			}
			return Grammar.ThirdPerson(Verb, PrependSpace);
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x001C8088 File Offset: 0x001C6288
		public bool isFurniture()
		{
			return this.HasTag("Furniture");
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x001C8095 File Offset: 0x001C6295
		public void RegisterEvent(IEventHandler Handler, int EventID, int Order = 0, bool Serialize = false)
		{
			if (this.RegisteredEvents == null)
			{
				this.RegisteredEvents = EventRegistry.Get();
			}
			this.RegisteredEvents.Register(Handler, EventID, Order, Serialize);
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x001C80BA File Offset: 0x001C62BA
		public void UnregisterEvent(IEventHandler Handler, int EventID)
		{
			EventRegistry registeredEvents = this.RegisteredEvents;
			if (registeredEvents == null)
			{
				return;
			}
			registeredEvents.Unregister(Handler, EventID);
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x001C80D0 File Offset: 0x001C62D0
		public bool HasRegisteredEvent(string Event)
		{
			return Event == "TakeDamage" || Event == "BeforeDeathRemoval" || Event == "CommandAutoEquipObject" || Event == "Regenera" || (this.RegisteredPartEvents != null && this.RegisteredPartEvents.ContainsKey(Event)) || (this.RegisteredEffectEvents != null && this.RegisteredEffectEvents.ContainsKey(Event));
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x001C814A File Offset: 0x001C634A
		public bool HasRegisteredEvent(int EventID)
		{
			return this.RegisteredEvents != null && this.RegisteredEvents.ContainsKey(EventID);
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x001C8162 File Offset: 0x001C6362
		public bool HasRegisteredEventDirect(string Event)
		{
			return (this.RegisteredPartEvents != null && this.RegisteredPartEvents.ContainsKey(Event)) || (this.RegisteredEffectEvents != null && this.RegisteredEffectEvents.ContainsKey(Event));
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x001C8198 File Offset: 0x001C6398
		public bool HasRegisteredEventFrom(int EventID, IEventHandler Handler)
		{
			EventRegistry.List list;
			return this.RegisteredEvents != null && this.RegisteredEvents.TryGetValue(EventID, out list) && list.IndexOf(Handler) != -1;
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x001C81CC File Offset: 0x001C63CC
		public bool HasRegisteredEventFrom(string Event, IPart P)
		{
			return this.RegisteredPartEvents != null && this.RegisteredPartEvents.ContainsKey(Event) && this.RegisteredPartEvents[Event].Contains(P);
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x001C81FB File Offset: 0x001C63FB
		public bool HasRegisteredEventFrom(string Event, Effect E)
		{
			return this.RegisteredEffectEvents != null && this.RegisteredEffectEvents.ContainsKey(Event) && this.RegisteredEffectEvents[Event].Contains(E);
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x001C822C File Offset: 0x001C642C
		public void RegisterEffectEvent(Effect Effect, string Event)
		{
			if (this.RegisteredEffectEvents == null)
			{
				this.RegisteredEffectEvents = new Dictionary<string, List<Effect>>();
			}
			List<Effect> list;
			if (!this.RegisteredEffectEvents.TryGetValue(Event, out list))
			{
				list = (this.RegisteredEffectEvents[Event] = new List<Effect>());
			}
			list.Add(Effect);
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x001C8278 File Offset: 0x001C6478
		public void UnregisterEffectEvent(Effect Ef, string Event)
		{
			if (this.RegisteredEffectEvents == null)
			{
				return;
			}
			List<Effect> list;
			if (this.RegisteredEffectEvents.TryGetValue(Event, out list))
			{
				for (int i = list.IndexOf(Ef); i >= 0; i = list.IndexOf(Ef))
				{
					list.RemoveAt(i);
				}
				if (list.Count == 0)
				{
					this.RegisteredEffectEvents.Remove(Event);
				}
			}
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x001C82D0 File Offset: 0x001C64D0
		public void RegisterPartEvent(IPart Ef, string Event)
		{
			if (Event == null)
			{
				return;
			}
			if (this.RegisteredPartEvents == null)
			{
				this.RegisteredPartEvents = new Dictionary<string, List<IPart>>();
			}
			List<IPart> list;
			if (!this.RegisteredPartEvents.TryGetValue(Event, out list))
			{
				list = new List<IPart>();
				this.RegisteredPartEvents.Add(Event, list);
			}
			if (!list.Contains(Ef))
			{
				list.Add(Ef);
			}
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x001C8328 File Offset: 0x001C6528
		public void UnregisterPartEvent(IPart Part, string Event)
		{
			if (this.RegisteredPartEvents == null)
			{
				return;
			}
			if (Event == null)
			{
				return;
			}
			List<IPart> list;
			if (this.RegisteredPartEvents.TryGetValue(Event, out list))
			{
				for (int i = list.IndexOf(Part); i >= 0; i = list.IndexOf(Part))
				{
					list.RemoveAt(i);
				}
				if (list.Count == 0)
				{
					this.RegisteredPartEvents.Remove(Event);
				}
			}
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x001C8384 File Offset: 0x001C6584
		public bool HasOtherRegisteredEvent(string Event, IPart Part)
		{
			if (Event == "TakeDamage" && Part != this.Physics)
			{
				return true;
			}
			if (Event == "BeforeDeathRemoval" && !(Part is Body))
			{
				return true;
			}
			if (Event == "CommandAutoEquipObject")
			{
				return true;
			}
			if (Event == "Regenera")
			{
				return true;
			}
			List<IPart> list;
			if (this.RegisteredPartEvents != null && this.RegisteredPartEvents.TryGetValue(Event, out list))
			{
				using (List<IPart>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current != Part)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005E7A RID: 24186 RVA: 0x001C843C File Offset: 0x001C663C
		public bool HasOtherRegisteredEvent(string Event, Effect fx)
		{
			if (Event == "TakeDamage")
			{
				return true;
			}
			if (Event == "BeforeDeathRemoval")
			{
				return true;
			}
			if (Event == "CommandAutoEquipObject")
			{
				return true;
			}
			if (Event == "Regenera")
			{
				return true;
			}
			List<Effect> list;
			if (this.RegisteredEffectEvents != null && this.RegisteredEffectEvents.TryGetValue(Event, out list))
			{
				using (List<Effect>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current != fx)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x001C84E0 File Offset: 0x001C66E0
		public bool FireEvent(string ID)
		{
			return !this.HasRegisteredEvent(ID) || this.FireEvent(Event.New(ID, 0, 0, 0));
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x001C84FC File Offset: 0x001C66FC
		public bool FireEvent(string ID, IEvent ParentEvent)
		{
			return !this.HasRegisteredEvent(ID) || this.FireEvent(Event.New(ID, 0, 0, 0), ParentEvent);
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x001C8519 File Offset: 0x001C6719
		public bool FireEvent(string ID, Event ParentEvent)
		{
			return !this.HasRegisteredEvent(ID) || this.FireEvent(Event.New(ID, 0, 0, 0), ParentEvent);
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x001C8536 File Offset: 0x001C6736
		public bool FireEvent(Event E, IEvent ParentEvent)
		{
			bool flag = this.FireEvent(E);
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x001C8549 File Offset: 0x001C6749
		public bool FireEvent(Event E, Event ParentEvent)
		{
			bool flag = this.FireEvent(E);
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x001C855C File Offset: 0x001C675C
		public bool FireEventDirect(Event E)
		{
			for (int i = 0; i < this.Effects.Count; i++)
			{
				if (!this.Effects[i].FireEvent(E))
				{
					return false;
				}
			}
			for (int j = 0; j < this.PartsCascade; j++)
			{
				if (!this.PartsList[j].FireEvent(E))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x001C85C0 File Offset: 0x001C67C0
		public bool FireRegisteredEvent(Event E)
		{
			List<Effect> list;
			if (this.RegisteredEffectEvents != null && this.RegisteredEffectEvents.TryGetValue(E.ID, out list))
			{
				int i = 0;
				int num = list.Count;
				while (i < num)
				{
					Effect effect = list[i];
					if (!effect.FireEvent(E))
					{
						return false;
					}
					if (list.Count != num)
					{
						num = list.Count;
						if (i < num && list[i] != effect)
						{
							i--;
						}
					}
					i++;
				}
			}
			List<IPart> list2;
			if (this.RegisteredPartEvents != null && this.RegisteredPartEvents.TryGetValue(E.ID, out list2))
			{
				int j = 0;
				int num2 = list2.Count;
				while (j < num2)
				{
					IPart part = list2[j];
					if (!part.FireEvent(E))
					{
						return false;
					}
					if (list2.Count != num2)
					{
						num2 = list2.Count;
						if (j < num2 && list2[j] != part)
						{
							j--;
						}
					}
					j++;
				}
			}
			return true;
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x001C86B0 File Offset: 0x001C68B0
		public bool BroadcastEvent(Event E)
		{
			if (this.PartsList != null)
			{
				int i = 0;
				int num = this.PartsCascade;
				while (i < num)
				{
					if (!this.PartsList[i].FireEvent(E))
					{
						return false;
					}
					if (this.PartsCascade < num)
					{
						int num2 = num - this.PartsCascade;
						i -= num2;
						num -= num2;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x001C870C File Offset: 0x001C690C
		public bool LocalEvent(Event E)
		{
			if (!this.FireEvent(E))
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell != null)
			{
				int i = 0;
				int num = currentCell.Objects.Count;
				while (i < num)
				{
					GameObject gameObject = currentCell.Objects[i];
					if (gameObject != this && !gameObject.FireEvent(E))
					{
						return false;
					}
					if (currentCell.Objects.Count != num)
					{
						num = currentCell.Objects.Count;
						if (i < num && currentCell.Objects[i] != gameObject)
						{
							i--;
						}
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x001C8794 File Offset: 0x001C6994
		public bool LocalEvent(string Name)
		{
			return this.LocalEvent(Event.New(Name, 0, 0, 0));
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x001C87A8 File Offset: 0x001C69A8
		public bool ExtendedLocalEvent(Event E)
		{
			if (!this.FireEvent(E))
			{
				return false;
			}
			if (this.CurrentCell != null)
			{
				foreach (GameObject gameObject in this.CurrentCell.Objects)
				{
					if (gameObject != this && !gameObject.FireEvent(E))
					{
						return false;
					}
				}
				foreach (Cell cell in this.CurrentCell.GetLocalAdjacentCells())
				{
					using (Container<GameObject>.Enumerator enumerator = cell.Objects.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (!enumerator.Current.FireEvent(E))
							{
								return false;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x001C88AC File Offset: 0x001C6AAC
		public bool ExtendedLocalEvent(string Name)
		{
			return this.ExtendedLocalEvent(Event.New(Name, 0, 0, 0));
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x001C88BD File Offset: 0x001C6ABD
		public bool FireEventOnBodyparts(Event E)
		{
			Body body = this.Body;
			return body == null || body.FireEventOnBodyparts(E);
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x001C88D4 File Offset: 0x001C6AD4
		public bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeathRemoval")
			{
				Body body = this.Body;
				if (body != null && !body.FireEvent(E))
				{
					return false;
				}
			}
			else if (E.ID == "TakeDamage")
			{
				if (this.Physics != null && !this.Physics.ProcessTakeDamage(E))
				{
					return false;
				}
			}
			else if (E.ID == "CommandAutoEquipObject")
			{
				if (!this.AutoEquip(E.GetGameObjectParameter("Object"), false, false, false))
				{
					return false;
				}
			}
			else if (E.ID == "Regenera")
			{
				int num = 9244;
				int num2 = 100663296;
				if (E.GetIntParameter("Level", 0) < 5)
				{
					num2 |= 16777216;
				}
				GameObject.TargetEffects.Clear();
				int i = 0;
				int count = this.Effects.Count;
				while (i < count)
				{
					Effect effect = this.Effects[i];
					if (effect.IsOfType(num) && effect.IsOfTypes(num2))
					{
						GameObject.TargetEffects.Add(effect);
					}
					i++;
				}
				Effect randomElement = GameObject.TargetEffects.GetRandomElement<Effect>(null);
				if (randomElement != null)
				{
					if (this.IsPlayer())
					{
						GameObject gameObjectParameter = E.GetGameObjectParameter("Source");
						string text;
						if (gameObjectParameter != null)
						{
							text = gameObjectParameter.Does("cure", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " you of";
						}
						else
						{
							text = E.GetStringParameter("SourceDescription", null);
							if (text != null)
							{
								ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(text);
							}
						}
						if (text == null)
						{
							text = "Your regenerative metabolism cures you of";
						}
						string text2 = text;
						if (randomElement.DisplayName == randomElement.ClassName)
						{
							text2 += " a malady.";
						}
						else
						{
							text2 = text2 + " " + randomElement.DisplayName + ".";
						}
						MessageQueue.AddPlayerMessage(text2, null, true);
					}
					this.RemoveEffect(randomElement, true);
				}
				GameObject.TargetEffects.Clear();
			}
			List<Effect> list;
			if (this.RegisteredEffectEvents != null && this.RegisteredEffectEvents.TryGetValue(E.ID, out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (!list[j].FireEvent(E))
					{
						return false;
					}
				}
			}
			List<IPart> list2;
			if (this.RegisteredPartEvents != null && this.RegisteredPartEvents.TryGetValue(E.ID, out list2))
			{
				for (int k = 0; k < list2.Count; k++)
				{
					int count2 = list2.Count;
					if (!list2[k].FireEvent(E))
					{
						return false;
					}
					if (list2.Count < count2)
					{
						k--;
					}
				}
			}
			return true;
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x001C8B90 File Offset: 0x001C6D90
		public void CompanionDirectionEnergyCost(GameObject GO, int EnergyCost, string Action)
		{
			if (this.CanMakeTelepathicContactWith(GO))
			{
				EnergyCost /= 10;
				Action = "Mental Direct Companion " + Action;
			}
			else
			{
				Action = "Direct Companion " + Action;
			}
			this.UseEnergy(EnergyCost, Action, null, null, false);
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x001C8BDC File Offset: 0x001C6DDC
		public int GetTier()
		{
			string tag = this.GetTag("Tier", null);
			if (!tag.IsNullOrEmpty())
			{
				try
				{
					return Convert.ToInt32(tag);
				}
				catch
				{
				}
			}
			return (this.Stat("Level", 0) - 1) / 5 + 1;
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x001C8C30 File Offset: 0x001C6E30
		public int GetTechTier()
		{
			string tag = this.GetTag("TechTier", null);
			if (!tag.IsNullOrEmpty())
			{
				try
				{
					return Convert.ToInt32(tag);
				}
				catch
				{
				}
			}
			return this.GetTier();
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x001C8C78 File Offset: 0x001C6E78
		public bool CleanEffects()
		{
			bool flag = false;
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int num = effects.Count;
				while (i < num)
				{
					Effect effect = effects[i];
					if (effect.Duration <= 0)
					{
						effect.Expired();
						this.RemoveEffect(effect, false);
						i = -1;
						num = effects.Count;
						flag = true;
					}
					i++;
				}
			}
			if (flag)
			{
				this.CheckStack();
			}
			return flag;
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x001C8CDE File Offset: 0x001C6EDE
		public bool AnyEffects()
		{
			return this._Effects != null && this._Effects.Count > 0;
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x001C8CF8 File Offset: 0x001C6EF8
		public int GetPhase()
		{
			return Phase.getPhase(this);
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x001C8D00 File Offset: 0x001C6F00
		public bool PhaseMatches(int VsPhase)
		{
			return Phase.phaseMatches(this, VsPhase);
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x001C8D09 File Offset: 0x001C6F09
		public bool PhaseMatches(GameObject GO)
		{
			return Phase.phaseMatches(this, GO);
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x001C8D12 File Offset: 0x001C6F12
		public bool FlightMatches(GameObject GO)
		{
			return GO == null || GO.IsFlying == this.IsFlying;
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x001C8D27 File Offset: 0x001C6F27
		public bool PhaseAndFlightMatches(GameObject GO)
		{
			return GO == null || (this.PhaseMatches(GO) && this.FlightMatches(GO));
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x001C8D40 File Offset: 0x001C6F40
		public bool FlightCanReach(GameObject GO)
		{
			return GO == null || !GO.IsFlying || this.IsFlying;
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x001C8D5C File Offset: 0x001C6F5C
		public bool HasEffect(string EffectType)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					Effect effect = effects[i];
					if (effect.Duration > 0 && EffectType == effect.ClassName)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x001C8DA8 File Offset: 0x001C6FA8
		public bool HasEffect(Type EffectType)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == EffectType && effects[i].Duration > 0)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x001C8DF4 File Offset: 0x001C6FF4
		public bool HasEffect(Predicate<Effect> filter)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (filter(effects[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x001C8E30 File Offset: 0x001C7030
		public bool HasEffect(Type EffectType, Predicate<Effect> filter)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == EffectType && filter(effects[i]))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x001C8E80 File Offset: 0x001C7080
		public bool HasEffect<T>() where T : Effect
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == typeof(T))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x001C8EC8 File Offset: 0x001C70C8
		public bool HasEffect<T>(Predicate<T> Filter) where T : Effect
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == typeof(T))
					{
						T t = effects[i] as T;
						if (t != null && Filter(t))
						{
							return true;
						}
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x001C8F30 File Offset: 0x001C7130
		public bool HasEffectDescendedFrom<T>() where T : class
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i] is T)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x001C8F6C File Offset: 0x001C716C
		public bool HasEffectOtherThan(Type EffectType, Effect Skip)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i] != Skip && effects[i].GetType() == EffectType)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x001C8FB8 File Offset: 0x001C71B8
		public bool HasEffectOtherThan(Predicate<Effect> Filter, Effect Skip)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i] != Skip && (Filter == null || Filter(effects[i])))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x001C9001 File Offset: 0x001C7201
		public IEnumerable<T> YieldEffects<T>() where T : Effect
		{
			GameObject.<YieldEffects>d__801<T> <YieldEffects>d__ = new GameObject.<YieldEffects>d__801<T>(-2);
			<YieldEffects>d__.<>4__this = this;
			return <YieldEffects>d__;
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x001C9011 File Offset: 0x001C7211
		public IEnumerable<T> YieldEffects<T>(Predicate<T> Filter) where T : Effect
		{
			GameObject.<YieldEffects>d__802<T> <YieldEffects>d__ = new GameObject.<YieldEffects>d__802<T>(-2);
			<YieldEffects>d__.<>4__this = this;
			<YieldEffects>d__.<>3__Filter = Filter;
			return <YieldEffects>d__;
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x001C9028 File Offset: 0x001C7228
		public IEnumerable<Effect> YieldEffects(Predicate<Effect> Filter)
		{
			GameObject.<YieldEffects>d__803 <YieldEffects>d__ = new GameObject.<YieldEffects>d__803(-2);
			<YieldEffects>d__.<>4__this = this;
			<YieldEffects>d__.<>3__Filter = Filter;
			return <YieldEffects>d__;
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x001C903F File Offset: 0x001C723F
		public IEnumerable<T> YieldEffectsDescendedFrom<T>() where T : class
		{
			GameObject.<YieldEffectsDescendedFrom>d__804<T> <YieldEffectsDescendedFrom>d__ = new GameObject.<YieldEffectsDescendedFrom>d__804<T>(-2);
			<YieldEffectsDescendedFrom>d__.<>4__this = this;
			return <YieldEffectsDescendedFrom>d__;
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x001C904F File Offset: 0x001C724F
		public IEnumerable<T> YieldEffectsDescendedFrom<T>(Predicate<T> Filter) where T : class
		{
			GameObject.<YieldEffectsDescendedFrom>d__805<T> <YieldEffectsDescendedFrom>d__ = new GameObject.<YieldEffectsDescendedFrom>d__805<T>(-2);
			<YieldEffectsDescendedFrom>d__.<>4__this = this;
			<YieldEffectsDescendedFrom>d__.<>3__Filter = Filter;
			return <YieldEffectsDescendedFrom>d__;
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x001C9066 File Offset: 0x001C7266
		public int GetEffectCount()
		{
			EffectRack effects = this._Effects;
			if (effects == null)
			{
				return 0;
			}
			return effects.Count;
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x001C907C File Offset: 0x001C727C
		public int GetEffectCount(Type EffectType)
		{
			int num = 0;
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == EffectType)
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x001C90C4 File Offset: 0x001C72C4
		public int GetEffectCount(Type EffectType, Predicate<Effect> Filter)
		{
			if (Filter == null)
			{
				return this.GetEffectCount(EffectType);
			}
			int num = 0;
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == EffectType && Filter(effects[i]))
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x001C9124 File Offset: 0x001C7324
		public int GetEffectCount(Predicate<Effect> Filter)
		{
			if (Filter == null)
			{
				return this.GetEffectCount();
			}
			int num = 0;
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (Filter(effects[i]))
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x001C9170 File Offset: 0x001C7370
		public Effect GetEffect(Guid ID)
		{
			if (ID == Guid.Empty)
			{
				return null;
			}
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].ID == ID)
					{
						return effects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x001C91C8 File Offset: 0x001C73C8
		public Effect GetEffect(Type Type)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == Type)
					{
						return effects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x001C920C File Offset: 0x001C740C
		public Effect GetEffect(Type Type, Predicate<Effect> Predicate)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					Effect effect = effects[i];
					if (effect.GetType() == Type && Predicate(effect))
					{
						return effect;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x001C9254 File Offset: 0x001C7454
		public T GetEffect<T>() where T : Effect
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == typeof(T))
					{
						return (T)((object)effects[i]);
					}
					i++;
				}
			}
			return default(T);
		}

		// Token: 0x06005EAA RID: 24234 RVA: 0x001C92AC File Offset: 0x001C74AC
		public T GetEffect<T>(Predicate<T> Filter) where T : Effect
		{
			if (Filter == null)
			{
				return this.GetEffect<T>();
			}
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == typeof(T))
					{
						T t = (T)((object)effects[i]);
						if (Filter(t))
						{
							return t;
						}
					}
					i++;
				}
			}
			return default(T);
		}

		// Token: 0x06005EAB RID: 24235 RVA: 0x001C931C File Offset: 0x001C751C
		public T GetEffectDescendedFrom<T>() where T : class
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					T t = effects[i] as T;
					if (t != null)
					{
						return t;
					}
					i++;
				}
			}
			return default(T);
		}

		// Token: 0x06005EAC RID: 24236 RVA: 0x001C936C File Offset: 0x001C756C
		public Effect GetEffect(Predicate<Effect> Filter)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				if (Filter == null)
				{
					if (effects.Count > 0)
					{
						return effects[0];
					}
				}
				else
				{
					int i = 0;
					int count = effects.Count;
					while (i < count)
					{
						if (Filter(effects[i]))
						{
							return effects[i];
						}
						i++;
					}
				}
			}
			return null;
		}

		// Token: 0x06005EAD RID: 24237 RVA: 0x001C93C4 File Offset: 0x001C75C4
		public bool HasEffectOfType(int Mask, Predicate<Effect> Filter = null)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].IsOfTypes(Mask) && (Filter == null || Filter(effects[i])))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005EAE RID: 24238 RVA: 0x001C9414 File Offset: 0x001C7614
		public Effect GetEffectOfType(int Mask, Predicate<Effect> Filter = null)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].IsOfTypes(Mask) && (Filter == null || Filter(effects[i])))
					{
						return effects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06005EAF RID: 24239 RVA: 0x001C9468 File Offset: 0x001C7668
		public bool HasEffectOfPartialType(int Mask, Predicate<Effect> Filter = null)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].IsOfType(Mask) && (Filter == null || Filter(effects[i])))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06005EB0 RID: 24240 RVA: 0x001C94B8 File Offset: 0x001C76B8
		public Effect GetEffectOfPartialType(int Mask, Predicate<Effect> Filter = null)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].IsOfType(Mask) && (Filter == null || Filter(effects[i])))
					{
						return effects[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x001C950C File Offset: 0x001C770C
		public bool TryGetEffect<T>(out T Effect) where T : Effect
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].GetType() == typeof(T))
					{
						Effect = effects[i] as T;
						return true;
					}
					i++;
				}
			}
			Effect = default(T);
			return false;
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x001C9570 File Offset: 0x001C7770
		public bool TryGetEffect<T>(Guid ID, out T Effect) where T : Effect
		{
			EffectRack effects = this._Effects;
			if (ID != Guid.Empty && effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					if (effects[i].ID == ID)
					{
						T t = effects[i] as T;
						if (t != null)
						{
							Effect = t;
							return true;
						}
					}
					i++;
				}
			}
			Effect = default(T);
			return false;
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x001C95E8 File Offset: 0x001C77E8
		public void ForeachEffect(Action<Effect> Proc)
		{
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int i = 0;
				int count = effects.Count;
				while (i < count)
				{
					Proc(effects[i]);
					i++;
				}
			}
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x001C9620 File Offset: 0x001C7820
		public void GetWantEventHandlers(int ID, int Cascade, Rack<IEventHandler> Result)
		{
			int i = 0;
			int partsCascade = this.PartsCascade;
			while (i < partsCascade)
			{
				IPart part = this.PartsList[i];
				if (part.WantEvent(ID, Cascade))
				{
					Result.Add(part);
				}
				i++;
			}
			EffectRack effects = this._Effects;
			if (effects != null)
			{
				int j = 0;
				int count = effects.Count;
				while (j < count)
				{
					Effect effect = effects[j];
					if (effect.WantEvent(ID, Cascade))
					{
						Result.Add(effect);
					}
					j++;
				}
			}
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x001C96A0 File Offset: 0x001C78A0
		public bool IsWaterPuddle()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.MaxVolume < 0 && liquidVolume.IsWater(false);
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x001C96CC File Offset: 0x001C78CC
		public bool IsFreshWaterPuddle()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.MaxVolume < 0 && liquidVolume.IsFreshWater(false);
		}

		// Token: 0x06005EB7 RID: 24247 RVA: 0x001C96F8 File Offset: 0x001C78F8
		public bool ContainsFreshWater()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.MaxVolume >= 0 && liquidVolume.IsFreshWater(false);
		}

		// Token: 0x06005EB8 RID: 24248 RVA: 0x001C9721 File Offset: 0x001C7921
		public bool IsTakeable()
		{
			return this.Takeable && this.FireEvent("CanBeTaken");
		}

		// Token: 0x06005EB9 RID: 24249 RVA: 0x001C9738 File Offset: 0x001C7938
		public int GetHPPercent()
		{
			int num = 100;
			int baseHitpoints = this.baseHitpoints;
			if (baseHitpoints != 0)
			{
				num = this.hitpoints * 100 / baseHitpoints;
			}
			return num;
		}

		// Token: 0x06005EBA RID: 24250 RVA: 0x001C9760 File Offset: 0x001C7960
		public string GetHPColor()
		{
			int hppercent = this.GetHPPercent();
			if (hppercent < 15)
			{
				return "&r";
			}
			if (hppercent < 33)
			{
				return "&R";
			}
			if (hppercent < 66)
			{
				return "&W";
			}
			if (hppercent < 100)
			{
				return "&G";
			}
			return "&Y";
		}

		// Token: 0x06005EBB RID: 24251 RVA: 0x001C97A8 File Offset: 0x001C79A8
		public void GetHPColorAndPriority(out string Color, out int Priority)
		{
			int hppercent = this.GetHPPercent();
			if (hppercent < 15)
			{
				Color = "&r";
				Priority = 400;
				return;
			}
			if (hppercent < 33)
			{
				Color = "&R";
				Priority = 300;
				return;
			}
			if (hppercent < 66)
			{
				Color = "&W";
				Priority = 200;
				return;
			}
			if (hppercent < 100)
			{
				Color = "&G";
				Priority = 75;
				return;
			}
			Color = "&Y";
			Priority = 5;
		}

		// Token: 0x06005EBC RID: 24252 RVA: 0x001C9814 File Offset: 0x001C7A14
		public bool ShouldAutoexploreAsChest()
		{
			Inventory inventory = this.Inventory;
			return inventory != null && this.Brain == null && this.HasPart<Container>() && !this.HasIntProperty("Autoexplored") && this.Owner.IsNullOrEmpty() && inventory.Objects.Count != 0 && this.Understood() && (FungalVisionary.VisionLevel > 0 || !this.HasPart<FungalVision>());
		}

		// Token: 0x06005EBD RID: 24253 RVA: 0x001C988C File Offset: 0x001C7A8C
		public bool CanAutoget(bool takeableOnly = true)
		{
			return this.Physics != null && this.Physics.Owner == null && this.Physics.IsReal && !this.IsTemporary && (FungalVisionary.VisionLevel > 0 || !this.HasPart<FungalVision>()) && this.PhaseMatches(XRL.The.Player) && !this.IsHidden && (!takeableOnly || this.IsTakeable()) && !this.HasTagOrProperty("NoAutoget") && !this.HasIntProperty("DroppedByPlayer");
		}

		// Token: 0x06005EBE RID: 24254 RVA: 0x001C9924 File Offset: 0x001C7B24
		public bool ShouldAutoget()
		{
			if (!this.CanAutoget(true))
			{
				return false;
			}
			if (Options.AutogetSpecialItems && this.IsSpecialItem())
			{
				return true;
			}
			if (this.HasTagOrProperty("Nugget"))
			{
				return Options.AutogetNuggets;
			}
			string inventoryCategory = this.GetInventoryCategory(false);
			bool flag;
			if (!(inventoryCategory == "Trade Goods"))
			{
				if (!(inventoryCategory == "Food"))
				{
					flag = inventoryCategory == "Books" && Options.AutogetBooks;
				}
				else
				{
					flag = Options.AutogetFood;
				}
			}
			else
			{
				flag = Options.AutogetTradeGoods;
			}
			if (flag)
			{
				return true;
			}
			bool flag2 = false;
			double num = 0.0;
			if (Options.AutogetFreshWater && this.ContainsFreshWater())
			{
				if (!flag2)
				{
					num = this.GetWeight();
					flag2 = true;
				}
				if (num <= 1.0)
				{
					return true;
				}
			}
			if (Options.AutogetZeroWeight)
			{
				if (!flag2)
				{
					num = this.GetWeight();
				}
				if (Math.Floor(num) <= 0.0)
				{
					return true;
				}
			}
			return Options.AutogetScrap && TinkeringHelpers.ConsiderScrap(this, this.ThePlayer);
		}

		// Token: 0x06005EBF RID: 24255 RVA: 0x001C9A23 File Offset: 0x001C7C23
		public bool ShouldTakeAll()
		{
			return this.IsTakeable() && (!(this.GetInventoryCategory(false) == "Corpses") || Options.TakeallCorpses);
		}

		// Token: 0x06005EC0 RID: 24256 RVA: 0x001C9A4C File Offset: 0x001C7C4C
		public bool HasPropertyOrTag(string Name)
		{
			return this.HasProperty(Name) || this.HasTag(Name);
		}

		// Token: 0x06005EC1 RID: 24257 RVA: 0x001C9A60 File Offset: 0x001C7C60
		public bool HasIntProperty(string Name)
		{
			return Name != null && this.IntProperty.ContainsKey(Name);
		}

		// Token: 0x06005EC2 RID: 24258 RVA: 0x001C9A73 File Offset: 0x001C7C73
		public bool HasProperty(string Name)
		{
			return Name != null && (this.Property.ContainsKey(Name) || this.IntProperty.ContainsKey(Name));
		}

		// Token: 0x06005EC3 RID: 24259 RVA: 0x001C9A9B File Offset: 0x001C7C9B
		public bool HasStringProperty(string Name)
		{
			return Name != null && this.Property.ContainsKey(Name);
		}

		// Token: 0x06005EC4 RID: 24260 RVA: 0x001C9AAE File Offset: 0x001C7CAE
		public bool IsInvisibile()
		{
			return this.HasPart<Invisibility>();
		}

		// Token: 0x06005EC5 RID: 24261 RVA: 0x001C9ABC File Offset: 0x001C7CBC
		public bool IsEsper()
		{
			string text;
			return this.HasPart<Esper>() || (this.Property.TryGetValue("MutationLevel", out text) && text != null && text.Contains("Esper"));
		}

		/// <summary>
		/// Important objects are objects that, if removed, will break quest resolution/game flow.
		/// Quest objects, important travel and transition objects, etc.
		///
		/// Functions that clear cell contents should generally respect importance unless told very explicitly not to.
		/// </summary>
		/// <returns>If it's important.</returns>
		// Token: 0x06005EC6 RID: 24262 RVA: 0x001C9AFC File Offset: 0x001C7CFC
		public bool IsImportant()
		{
			int intProperty = this.GetIntProperty("Important", 0);
			return intProperty >= 1 || (intProperty > -1 && (this.HasTagOrProperty("Important") || this.HasTagOrProperty("QuestItem") || this.HasPart<StairsUp>() || this.HasPart<StairsDown>() || this.HasTagOrProperty("Storied")));
		}

		// Token: 0x06005EC7 RID: 24263 RVA: 0x001C9B64 File Offset: 0x001C7D64
		public bool IsMarkedImportantByPlayer()
		{
			return this.GetIntProperty("Important", 0) == 2;
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x001C9B78 File Offset: 0x001C7D78
		public void SetImportant(bool flag, bool force = false, bool player = false)
		{
			if (flag)
			{
				if (force || this.GetIntProperty("Important", 0) >= 0)
				{
					this.SetIntProperty("Important", player ? 2 : 1, false);
					return;
				}
			}
			else
			{
				if (force)
				{
					this.SetIntProperty("Important", player ? (-2) : (-1), false);
					return;
				}
				this.RemoveIntProperty("Important");
			}
		}

		/// <summary>
		/// Display a formatted confirmation popup if this object is important and the provided actor is the player.
		/// </summary>
		/// <returns><c>true</c> if this object is not important or the player confirmed its usage; otherwise, <c>false</c>.</returns>
		// Token: 0x06005EC9 RID: 24265 RVA: 0x001C9BD3 File Offset: 0x001C7DD3
		public bool ShouldConfirmUseImportant(GameObject Actor = null)
		{
			return this.IsImportant() && (Actor == null || Actor.IsPlayer());
		}

		/// <summary>
		/// Display a formatted confirmation popup if this object is important and the provided actor is the player.
		/// </summary>
		/// <returns><c>true</c> if this object is not important or the player confirmed its usage; otherwise, <c>false</c>.</returns>
		// Token: 0x06005ECA RID: 24266 RVA: 0x001C9BF0 File Offset: 0x001C7DF0
		public async Task<bool> ConfirmUseImportantAsync(GameObject Actor = null, string Verb = "use", string Extra = null, int Amount = -1)
		{
			bool flag;
			if (!this.ShouldConfirmUseImportant(Actor))
			{
				flag = false;
			}
			else
			{
				if (Extra.IsNullOrEmpty())
				{
					Extra = "";
				}
				else
				{
					Extra = " " + Extra;
				}
				if (Amount == -1)
				{
					Amount = this.Count;
				}
				if (Amount > 1 && !this.IsPlural)
				{
					TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync(string.Concat(new string[]
					{
						this.T(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false).Pluralize(),
						" are important. Are you sure you want to ",
						Verb,
						" them",
						Extra,
						"?"
					})).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<DialogResult> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<DialogResult>);
					}
					if (taskAwaiter.GetResult() != DialogResult.Yes)
					{
						return false;
					}
				}
				else
				{
					TaskAwaiter<DialogResult> taskAwaiter = Popup.ShowYesNoAsync(string.Concat(new string[]
					{
						this.T(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						this.Is,
						" important. Are you sure you want to ",
						Verb,
						" ",
						this.them,
						Extra,
						"?"
					})).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<DialogResult> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<DialogResult>);
					}
					if (taskAwaiter.GetResult() != DialogResult.Yes)
					{
						return false;
					}
				}
				flag = true;
			}
			return flag;
		}

		/// <summary>
		/// Display a formatted confirmation popup if this object is important and the provided actor is the player.
		/// </summary>
		/// <returns><c>true</c> if this object is not important or the player confirmed its usage; otherwise, <c>false</c>.</returns>
		// Token: 0x06005ECB RID: 24267 RVA: 0x001C9C54 File Offset: 0x001C7E54
		public bool ConfirmUseImportant(GameObject Actor = null, string Verb = "use", string Extra = null, int Amount = -1)
		{
			if (!this.ShouldConfirmUseImportant(Actor))
			{
				return true;
			}
			if (Extra.IsNullOrEmpty())
			{
				Extra = "";
			}
			else
			{
				Extra = " " + Extra;
			}
			if (Amount == -1)
			{
				Amount = this.Count;
			}
			if (Amount > 1 && !this.IsPlural)
			{
				if (Popup.ShowYesNo(string.Concat(new string[]
				{
					this.T(2147483647, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false).Pluralize(),
					" are important. Are you sure you want to ",
					Verb,
					" them",
					Extra,
					"?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return false;
				}
			}
			else if (Popup.ShowYesNo(string.Concat(new string[]
			{
				this.T(2147483647, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
				this.Is,
				" important. Are you sure you want to ",
				Verb,
				" ",
				this.them,
				Extra,
				"?"
			}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06005ECC RID: 24268 RVA: 0x001C9D84 File Offset: 0x001C7F84
		public bool IsChimera()
		{
			string text;
			return this.HasPart<Chimera>() || (this.Property.TryGetValue("MutationLevel", out text) && text != null && text.Contains("Chimera"));
		}

		// Token: 0x06005ECD RID: 24269 RVA: 0x001C9DC2 File Offset: 0x001C7FC2
		public bool IsWall()
		{
			return this.GetBlueprint(true).IsWall();
		}

		// Token: 0x06005ECE RID: 24270 RVA: 0x001C9DD0 File Offset: 0x001C7FD0
		public bool IsDoor()
		{
			return this.HasPart<Door>();
		}

		// Token: 0x06005ECF RID: 24271 RVA: 0x001C9DD8 File Offset: 0x001C7FD8
		public bool IsDiggable()
		{
			return this.HasTagOrProperty("Diggable");
		}

		// Token: 0x06005ED0 RID: 24272 RVA: 0x001C9DE5 File Offset: 0x001C7FE5
		public bool IsSpawnBlocker()
		{
			return this.HasPart<SpawnBlocker>() || this.HasTagOrProperty("SpawnBlocker");
		}

		// Token: 0x06005ED1 RID: 24273 RVA: 0x001C9DFC File Offset: 0x001C7FFC
		public bool IsHero()
		{
			return this.HasPart<GivesRep>();
		}

		// Token: 0x06005ED2 RID: 24274 RVA: 0x001C9E04 File Offset: 0x001C8004
		public bool HasPart(string Name)
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].Name == Name)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005ED3 RID: 24275 RVA: 0x001C9E48 File Offset: 0x001C8048
		public bool HasPart(Type type)
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].GetType() == type)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005ED4 RID: 24276 RVA: 0x001C9E84 File Offset: 0x001C8084
		public bool HasPart<T>() where T : IPart
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].GetType() == typeof(T))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005ED5 RID: 24277 RVA: 0x001C9EC9 File Offset: 0x001C80C9
		public bool AnyInstalledCybernetics()
		{
			Body body = this.Body;
			return body != null && body.AnyInstalledCybernetics();
		}

		// Token: 0x06005ED6 RID: 24278 RVA: 0x001C9EDC File Offset: 0x001C80DC
		public bool AnyInstalledCybernetics(Predicate<GameObject> Filter)
		{
			Body body = this.Body;
			return body != null && body.AnyInstalledCybernetics(Filter);
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x001C9EF0 File Offset: 0x001C80F0
		public bool UnequipAndRemove()
		{
			bool flag = true;
			if (this.Equipped != null)
			{
				flag = EquipmentAPI.UnequipObject(this);
			}
			if (flag)
			{
				GameObject inInventory = this.InInventory;
				if (inInventory != null)
				{
					Inventory inventory = inInventory.Inventory;
					if (inventory != null)
					{
						inventory.RemoveObject(this, null);
					}
				}
			}
			return flag;
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x001C9F30 File Offset: 0x001C8130
		public bool ForceUnequipAndRemove(bool Silent = false)
		{
			bool flag = true;
			if (this.Equipped != null)
			{
				flag = EquipmentAPI.ForceUnequipObject(this, Silent);
			}
			if (flag)
			{
				GameObject inInventory = this.InInventory;
				if (inInventory != null)
				{
					Inventory inventory = inInventory.Inventory;
					if (inventory != null)
					{
						inventory.RemoveObject(this, null);
					}
				}
			}
			return flag;
		}

		// Token: 0x06005ED9 RID: 24281 RVA: 0x001C9F74 File Offset: 0x001C8174
		public void ForceUnequipRemoveAndRemoveContents(bool Silent = false, bool ExcludeNatural = true)
		{
			Cell currentCell = this.GetCurrentCell();
			GameObject gameObject = null;
			if (this.Equipped != null)
			{
				gameObject = this.Equipped;
				EquipmentAPI.ForceUnequipObject(this, Silent);
			}
			if (this.InInventory != null)
			{
				gameObject = this.InInventory;
				this.InInventory.Inventory.RemoveObject(this, null);
			}
			if (gameObject != null)
			{
				using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
				{
					this.GetContents(fromPool);
					using (PooledContainer<GameObject>.Enumerator enumerator = fromPool.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject2 = enumerator.Current;
							if (!ExcludeNatural || !gameObject2.HasTag("NaturalGear"))
							{
								gameObject2.RemoveFromContext(null);
								GameObject gameObject3 = gameObject;
								GameObject gameObject4 = gameObject2;
								bool flag = false;
								int? num = new int?(0);
								gameObject3.TakeObject(gameObject4, flag, Silent, num, null, null);
							}
						}
						return;
					}
				}
			}
			if (currentCell != null)
			{
				using (ScopeDisposedList<GameObject> fromPool2 = ScopeDisposedList<GameObject>.GetFromPool())
				{
					this.GetContents(fromPool2);
					foreach (GameObject gameObject5 in fromPool2)
					{
						if (!ExcludeNatural || !gameObject5.HasTag("NaturalGear"))
						{
							gameObject5.RemoveFromContext(null);
							currentCell.AddObject(gameObject5, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
					}
				}
			}
		}

		// Token: 0x06005EDA RID: 24282 RVA: 0x001CA0EC File Offset: 0x001C82EC
		public void RemoveContents(bool Silent = false, bool ExcludeNatural = true)
		{
			GameObject gameObject;
			Cell cell;
			this.GetContext(out gameObject, out cell);
			if (gameObject != null || cell != null)
			{
				using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
				{
					this.GetContents(fromPool);
					foreach (GameObject gameObject2 in fromPool)
					{
						if (!ExcludeNatural || !gameObject2.HasTag("NaturalGear"))
						{
							gameObject2.RemoveFromContext(null);
							if (gameObject != null)
							{
								GameObject gameObject3 = gameObject;
								GameObject gameObject4 = gameObject2;
								bool flag = false;
								int? num = new int?(0);
								gameObject3.TakeObject(gameObject4, flag, Silent, num, null, null);
							}
							else
							{
								cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x06005EDB RID: 24283 RVA: 0x001CA1B8 File Offset: 0x001C83B8
		public bool Unimplant(bool MoveToInventory = false, List<BodyPart> Parts = null)
		{
			GameObject implantee = this.Implantee;
			if (implantee == null)
			{
				return false;
			}
			Body body = implantee.Body;
			if (body == null)
			{
				return false;
			}
			int num = 0;
			BodyPart bodyPart;
			while ((bodyPart = body.FindCybernetics(this)) != null)
			{
				if (Parts != null)
				{
					Parts.Add(bodyPart);
				}
				if (++num >= 100)
				{
					Debug.LogError("infinite looping trying to unimplant " + this.DebugName);
					break;
				}
				bodyPart.Unimplant(MoveToInventory);
			}
			if (body != null)
			{
				body.RegenerateDefaultEquipment();
			}
			return true;
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x001CA228 File Offset: 0x001C8428
		public IList<GameObject> GetContents(IList<GameObject> listToFill = null)
		{
			return GetContentsEvent.GetFor(this, listToFill);
		}

		/// <summary>
		/// Searches the objects inventory and equipment for an item with a given blueprint.
		/// </summary>
		/// <param name="bp">The blueprint ID</param>
		/// <returns>The first matched item in invetory, or equipment.</returns>
		// Token: 0x06005EDD RID: 24285 RVA: 0x001CA234 File Offset: 0x001C8434
		public GameObject HasItemWithBlueprint(string bp)
		{
			GameObject gameObject = null;
			Inventory inventory = this.Inventory;
			Body body = this.Body;
			if (inventory != null)
			{
				foreach (GameObject gameObject2 in inventory.Objects)
				{
					if (gameObject2.Blueprint == bp)
					{
						return gameObject2;
					}
				}
			}
			if (body != null)
			{
				foreach (GameObject gameObject3 in body.GetEquippedObjects())
				{
					if (!gameObject3.IsNatural() && gameObject3.Physics.IsReal && gameObject3.Blueprint == bp)
					{
						return gameObject3;
					}
				}
			}
			return gameObject;
		}

		/// <summary>
		/// Searches the objects inventory and equipment for an item that has a specific tag.
		/// </summary>
		/// <param name="tag">The tag name</param>
		/// <returns>The first matched object in the inventory, or equipment.</returns>
		// Token: 0x06005EDE RID: 24286 RVA: 0x001CA318 File Offset: 0x001C8518
		public GameObject HasItemWithTag(string tag)
		{
			GameObject gameObject = null;
			Inventory inventory = this.Inventory;
			Body body = this.Body;
			if (inventory != null)
			{
				foreach (GameObject gameObject2 in inventory.Objects)
				{
					if (gameObject2.HasTag(tag))
					{
						return gameObject2;
					}
				}
			}
			if (body != null)
			{
				foreach (GameObject gameObject3 in body.GetEquippedObjects())
				{
					if (!gameObject3.IsNatural() && gameObject3.Physics.IsReal && gameObject3.HasTag(tag))
					{
						return gameObject3;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x06005EDF RID: 24287 RVA: 0x001CA3F4 File Offset: 0x001C85F4
		public GameObject GetMostValuableItem()
		{
			double num = double.MinValue;
			GameObject gameObject = null;
			Inventory inventory = this.Inventory;
			Body body = this.Body;
			if (inventory != null)
			{
				foreach (GameObject gameObject2 in inventory.Objects)
				{
					if (!gameObject2.IsNatural() && gameObject2.Physics.IsReal)
					{
						double num2 = gameObject2.Value;
						if (gameObject2.HasPart<Commerce>())
						{
							num2 = gameObject2.GetPart<Commerce>().Value;
						}
						if (num2 > num)
						{
							num = gameObject2.Value;
							gameObject = gameObject2;
						}
					}
				}
			}
			if (body != null)
			{
				foreach (GameObject gameObject3 in body.GetEquippedObjects())
				{
					if (!gameObject3.IsNatural() && gameObject3.Physics.IsReal && gameObject3.Value > num)
					{
						num = gameObject3.Value;
						gameObject = gameObject3;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x001CA514 File Offset: 0x001C8714
		public BaseSkill AddSkill(string Class, GameObject Source = null, string Context = null)
		{
			Type type = ModManager.ResolveType("XRL.World.Parts.Skill." + Class, false, false, true);
			return this.AddSkill(type, Source, Context);
		}

		// Token: 0x06005EE1 RID: 24289 RVA: 0x001CA540 File Offset: 0x001C8740
		public BaseSkill AddSkill(Type Class, GameObject Source = null, string Context = null)
		{
			Skills skills;
			if (this.TryGetPart<Skills>(out skills))
			{
				BaseSkill baseSkill = Activator.CreateInstance(Class) as BaseSkill;
				if (skills.AddSkill(baseSkill, Source, Context))
				{
					return baseSkill;
				}
			}
			return null;
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x001CA574 File Offset: 0x001C8774
		public T AddSkill<T>(GameObject Source = null, string Context = null) where T : BaseSkill, new()
		{
			Skills skills;
			if (this.TryGetPart<Skills>(out skills))
			{
				T t = new T();
				if (skills.AddSkill(t, Source, Context))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x001CA5AC File Offset: 0x001C87AC
		public T RequireSkill<T>(GameObject Source = null, string Context = null) where T : BaseSkill, new()
		{
			T t;
			if (this.TryGetPart<T>(out t))
			{
				return t;
			}
			return this.AddSkill<T>(Source, Context);
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x001CA5D0 File Offset: 0x001C87D0
		public void RemoveSkill(string Class)
		{
			Skills part = this.GetPart<Skills>();
			if (part != null)
			{
				part.RemoveSkill(this.GetPart(Class) as BaseSkill);
				this.RemovePart(Class);
			}
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x001CA604 File Offset: 0x001C8804
		public bool RemoveSkill<T>() where T : BaseSkill
		{
			T t;
			if (this.TryGetPart<T>(out t))
			{
				BaseSkill baseSkill = t;
				Skills skills;
				if (baseSkill != null && this.TryGetPart<Skills>(out skills))
				{
					skills.RemoveSkill(baseSkill);
					this.RemovePart(t);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005EE6 RID: 24294 RVA: 0x001CA645 File Offset: 0x001C8845
		public bool HasSkill(string Name)
		{
			return this.HasPart(Name);
		}

		// Token: 0x06005EE7 RID: 24295 RVA: 0x001CA64E File Offset: 0x001C884E
		public bool HasTagOrStringProperty(string Name)
		{
			return this.HasStringProperty(Name) || this.HasTag(Name);
		}

		// Token: 0x06005EE8 RID: 24296 RVA: 0x001CA662 File Offset: 0x001C8862
		public string GetTagOrStringProperty(string Name, string Default = null)
		{
			string text;
			if ((text = this.GetStringProperty(Name, null)) == null)
			{
				text = this.GetTag(Name, null) ?? Default;
			}
			return text;
		}

		// Token: 0x06005EE9 RID: 24297 RVA: 0x001CA67D File Offset: 0x001C887D
		public string GetTagOrStringProperty_RandomSplit(string Name, string Default = null, char Delimiter = ',')
		{
			return this.GetTagOrStringProperty(Name, Default).Split(Delimiter, StringSplitOptions.None).GetRandomElement<string>(null);
		}

		// Token: 0x06005EEA RID: 24298 RVA: 0x001CA694 File Offset: 0x001C8894
		public string GetTag(string Tag, string Default = null)
		{
			return this.GetBlueprint(true).GetTag(Tag, Default);
		}

		// Token: 0x06005EEB RID: 24299 RVA: 0x001CA6A4 File Offset: 0x001C88A4
		public bool HasTagOrProperty(string Name)
		{
			return this.HasTag(Name) || this.HasProperty(Name);
		}

		// Token: 0x06005EEC RID: 24300 RVA: 0x001CA6B8 File Offset: 0x001C88B8
		public bool HasTagOrIntProperty(string Name)
		{
			return this.HasTag(Name) || this.HasIntProperty(Name);
		}

		// Token: 0x06005EED RID: 24301 RVA: 0x001CA6CC File Offset: 0x001C88CC
		public string GetPropertyOrTag(string Name, string Default = null)
		{
			if (Name == null)
			{
				return Default;
			}
			if (this.HasStringProperty(Name))
			{
				return this.GetStringProperty(Name, Default);
			}
			if (this.HasIntProperty(Name))
			{
				return this.GetIntProperty(Name, 0).ToString();
			}
			return this.GetBlueprint(true).GetTag(Name, Default);
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x001CA718 File Offset: 0x001C8918
		public string GetxTag(string Tag, string Value, string Default = null)
		{
			return this.GetBlueprint(true).GetxTag(Tag, Value, Default);
		}

		// Token: 0x06005EEF RID: 24303 RVA: 0x001CA729 File Offset: 0x001C8929
		public string GetxTag_CommaDelimited(string Tag, string Value, string Default = null, global::System.Random R = null)
		{
			return this.GetBlueprint(true).GetxTag_CommaDelimited(Tag, Value, Default, R);
		}

		// Token: 0x06005EF0 RID: 24304 RVA: 0x001CA73C File Offset: 0x001C893C
		public List<string> GetMutationNames()
		{
			Mutations part = this.GetPart<Mutations>();
			if (part == null || part.MutationList == null || part.MutationList.Count <= 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (BaseMutation baseMutation in part.MutationList)
			{
				list.Add(baseMutation.GetDisplayName(true));
			}
			return list;
		}

		// Token: 0x06005EF1 RID: 24305 RVA: 0x001CA7C0 File Offset: 0x001C89C0
		public List<BaseMutation> GetPhysicalMutations()
		{
			return this.GetMutationsOfCategory("Physical");
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x001CA7CD File Offset: 0x001C89CD
		public List<BaseMutation> GetMentalMutations()
		{
			return this.GetMutationsOfCategory("Mental");
		}

		// Token: 0x06005EF3 RID: 24307 RVA: 0x001CA7DC File Offset: 0x001C89DC
		public List<BaseMutation> GetMutationsOfCategory(string category)
		{
			Mutations part = this.GetPart<Mutations>();
			if (part == null)
			{
				return new List<BaseMutation>();
			}
			List<BaseMutation> list = new List<BaseMutation>();
			list.AddRange(part.MutationList.Where<BaseMutation>((BaseMutation m) => m.isCategory(category)));
			return list;
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x001CA828 File Offset: 0x001C8A28
		public T GetPart<T>() where T : IPart
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].GetType() == typeof(T))
				{
					return (T)((object)this.PartsList[i]);
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x001CA888 File Offset: 0x001C8A88
		public IPart GetPart(Type Type)
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].GetType() == Type)
				{
					return this.PartsList[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06005EF6 RID: 24310 RVA: 0x001CA8D0 File Offset: 0x001C8AD0
		public IPart GetPart(string Name)
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].Name == Name)
				{
					return this.PartsList[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x001CA91C File Offset: 0x001C8B1C
		public IPart GetPartExcept(string Name, IPart skip)
		{
			if (skip == null)
			{
				return this.GetPart(Name);
			}
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].Name == Name && this.PartsList[i] != skip)
				{
					return this.PartsList[i];
				}
				i++;
			}
			return null;
		}

		// Token: 0x06005EF8 RID: 24312 RVA: 0x001CA984 File Offset: 0x001C8B84
		public bool TryGetPart<T>(out T Part) where T : IPart
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i].GetType() == typeof(T))
				{
					Part = this.PartsList[i] as T;
					return true;
				}
				i++;
			}
			Part = default(T);
			return false;
		}

		// Token: 0x06005EF9 RID: 24313 RVA: 0x001CA9EC File Offset: 0x001C8BEC
		public bool TryGetPartDescendedFrom<T>(out T Part) where T : class
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T)
				{
					Part = this.PartsList[i] as T;
					return true;
				}
				i++;
			}
			Part = default(T);
			return false;
		}

		// Token: 0x06005EFA RID: 24314 RVA: 0x001CAA4C File Offset: 0x001C8C4C
		protected void AddPartInternals(IPart P, bool DoRegistration = true, bool Initial = false, bool Creation = false)
		{
			this.FlushTransientCache();
			if (P == null)
			{
				return;
			}
			int priority = P.Priority;
			if (priority == -2147483648)
			{
				this.PartsList.Add(P);
			}
			else
			{
				int num = this.PartsList.Count;
				while (num > 0 && this.PartsList[num - 1].Priority < priority)
				{
					num--;
				}
				this.PartsList.Insert(num, P);
				this.PartsCascade++;
			}
			if (DoRegistration)
			{
				P.ParentObject = this;
				P.ApplyRegistrar(this, false);
			}
			if (Initial)
			{
				P.Initialize();
			}
			P.Attach();
			if (!Creation)
			{
				P.AddedAfterCreation();
			}
			if (Initial)
			{
				PartAddedEvent.Send(this, P, P.GetType(), DoRegistration, Creation);
			}
		}

		// Token: 0x06005EFB RID: 24315 RVA: 0x001CAB08 File Offset: 0x001C8D08
		public IPart AddPart(IPart P, bool DoRegistration = true, bool Creation = false)
		{
			IModification modification = P as IModification;
			if (modification == null)
			{
				this.AddPartInternals(P, DoRegistration, true, Creation);
			}
			else
			{
				if (modification.Tier == 0)
				{
					modification.ApplyTier(this.GetTier());
				}
				this.ApplyModification(modification, DoRegistration, null, Creation);
			}
			if (!Creation)
			{
				this.ResetNameCache();
			}
			return P;
		}

		// Token: 0x06005EFC RID: 24316 RVA: 0x001CAB54 File Offset: 0x001C8D54
		public IModification AddPart(IModification P, bool DoRegistration = true, bool Creation = false)
		{
			this.AddPartInternals(P, DoRegistration, true, Creation);
			return P;
		}

		// Token: 0x06005EFD RID: 24317 RVA: 0x001CAB61 File Offset: 0x001C8D61
		public T AddPart<T>(T P, bool DoRegistration = true, bool Creation = false) where T : IPart
		{
			return this.AddPart(P, DoRegistration, Creation) as T;
		}

		// Token: 0x06005EFE RID: 24318 RVA: 0x001CAB7B File Offset: 0x001C8D7B
		public T AddPart<T>(bool DoRegistration = true, bool Creation = false) where T : IPart, new()
		{
			return this.AddPart(new T(), DoRegistration, Creation) as T;
		}

		// Token: 0x06005EFF RID: 24319 RVA: 0x001CAB9C File Offset: 0x001C8D9C
		public T RequirePart<T>(bool Creation = false) where T : IPart, new()
		{
			T part = this.GetPart<T>();
			if (part != null)
			{
				return part;
			}
			return this.AddPart<T>(true, Creation);
		}

		// Token: 0x06005F00 RID: 24320 RVA: 0x001CABC2 File Offset: 0x001C8DC2
		public bool ApplyModification(IModification ModPart, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			return ItemModding.ApplyModification(this, ModPart, DoRegistration, Actor, Creation);
		}

		// Token: 0x06005F01 RID: 24321 RVA: 0x001CABCF File Offset: 0x001C8DCF
		public bool ApplyModification(string ModPartName, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			return ItemModding.ApplyModification(this, ModPartName, DoRegistration, Actor, Creation);
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x001CABDC File Offset: 0x001C8DDC
		public bool ApplyModification(string ModPartName, int Tier, bool DoRegistration = true, GameObject Actor = null, bool Creation = false)
		{
			return ItemModding.ApplyModification(this, ModPartName, Tier, DoRegistration, Actor, Creation);
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x001CABEC File Offset: 0x001C8DEC
		public bool HasPartDescendedFrom<T>() where T : class
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x001CAC28 File Offset: 0x001C8E28
		public void ApplyActiveRegistrar()
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				this.PartsList[i].ApplyRegistrar(this, true);
				i++;
			}
			if (this._Effects != null)
			{
				int j = 0;
				int count2 = this._Effects.Count;
				while (j < count2)
				{
					this._Effects[j].ApplyRegistrar(this, true);
					j++;
				}
			}
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x001CAC94 File Offset: 0x001C8E94
		public void ApplyActiveUnregistrar()
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				this.PartsList[i].ApplyUnregistrar(this, true);
				i++;
			}
			if (this._Effects != null)
			{
				int j = 0;
				int count2 = this._Effects.Count;
				while (j < count2)
				{
					this._Effects[j].ApplyUnregistrar(this, true);
					j++;
				}
			}
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x001CAD00 File Offset: 0x001C8F00
		public T GetPartDescendedFrom<T>() where T : class
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T)
				{
					return this.PartsList[i] as T;
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x001CAD58 File Offset: 0x001C8F58
		public T GetPartDescendedFrom<T>(Predicate<T> Filter) where T : class
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T && Filter(this.PartsList[i] as T))
				{
					return this.PartsList[i] as T;
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x001CADD0 File Offset: 0x001C8FD0
		public List<T> GetPartsDescendedFrom<T>() where T : class
		{
			List<T> list = new List<T>();
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T)
				{
					list.Add(this.PartsList[i] as T);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x001CAE2C File Offset: 0x001C902C
		public List<T> GetPartsDescendedFrom<T>(Predicate<T> Filter) where T : class
		{
			List<T> list = new List<T>();
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T && Filter(this.PartsList[i] as T))
				{
					list.Add(this.PartsList[i] as T);
				}
				i++;
			}
			return list;
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x001CAEA8 File Offset: 0x001C90A8
		public T GetFirstPartDescendedFrom<T>() where T : class
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T)
				{
					return this.PartsList[i] as T;
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x001CAF00 File Offset: 0x001C9100
		public T GetFirstPartDescendedFrom<T>(Predicate<T> Filter) where T : class
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is T && Filter(this.PartsList[i] as T))
				{
					return this.PartsList[i] as T;
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x001CAF78 File Offset: 0x001C9178
		public IFlightSource GetFirstFlightSourcePart()
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is IFlightSource)
				{
					return this.PartsList[i] as IFlightSource;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x001CAFC4 File Offset: 0x001C91C4
		public IFlightSource GetFirstFlightSourcePart(Predicate<IFlightSource> Filter)
		{
			if (Filter == null)
			{
				return this.GetFirstFlightSourcePart();
			}
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (this.PartsList[i] is IFlightSource && Filter(this.PartsList[i] as IFlightSource))
				{
					return this.PartsList[i] as IFlightSource;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x001CB032 File Offset: 0x001C9232
		public IEnumerable<IPart> LoopParts()
		{
			foreach (IPart part in this.PartsList)
			{
				yield return part;
			}
			Container<IPart>.Enumerator enumerator = default(Container<IPart>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x001CB044 File Offset: 0x001C9244
		public void ForeachPart(Action<IPart> Proc)
		{
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				Proc(this.PartsList[i]);
				i++;
			}
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x001CB07C File Offset: 0x001C927C
		public bool ForeachPart(Predicate<IPart> Proc)
		{
			foreach (IPart part in this.PartsList)
			{
				if (!Proc(part))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x001CB0D8 File Offset: 0x001C92D8
		public void ForeachPartDescendedFrom<T>(Action<T> Proc) where T : class
		{
			foreach (IPart part in this.PartsList)
			{
				T t = part as T;
				if (t != null)
				{
					Proc(t);
				}
			}
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x001CB140 File Offset: 0x001C9340
		public bool ForeachPartDescendedFrom<T>(Predicate<T> Proc) where T : class
		{
			foreach (IPart part in this.PartsList)
			{
				T t = part as T;
				if (t != null && !Proc(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x001CB1B0 File Offset: 0x001C93B0
		public bool RemovePart(Type Type)
		{
			IPart[] array = this.PartsList.GetArray();
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (array[i].GetType() == Type)
				{
					this.RemovePartAt(i);
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x001CB1F8 File Offset: 0x001C93F8
		public bool RemovePart(string Name)
		{
			IPart[] array = this.PartsList.GetArray();
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (array[i].Name == Name)
				{
					this.RemovePartAt(i);
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x001CB244 File Offset: 0x001C9444
		public bool RemovePart<T>() where T : IPart
		{
			IPart[] array = this.PartsList.GetArray();
			int i = 0;
			int count = this.PartsList.Count;
			while (i < count)
			{
				if (array[i].GetType() == typeof(T))
				{
					this.RemovePartAt(i);
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x001CB294 File Offset: 0x001C9494
		public IPart RemovePartAt(int Index)
		{
			IPart part = this.PartsList.TakeAt(Index);
			if (part.Priority != -2147483648)
			{
				this.PartsCascade--;
			}
			part.ApplyUnregistrar(this, false);
			if (this.RegisteredPartEvents != null)
			{
				foreach (KeyValuePair<string, List<IPart>> keyValuePair in this.RegisteredPartEvents)
				{
					keyValuePair.Value.Remove(part);
				}
				this.CleanRegisteredPartEvents();
			}
			part.Remove();
			PartRemovedEvent.Send(this, part, part.GetType());
			part.ParentObject = null;
			this.FlushTransientCache();
			return part;
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x001CB350 File Offset: 0x001C9550
		public void RemovePart(IPart Part)
		{
			if (Part == null)
			{
				return;
			}
			int num = Array.IndexOf<IPart>(this.PartsList.GetArray(), Part, 0, this.PartsList.Count);
			if (num != -1)
			{
				this.RemovePartAt(num);
			}
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x001CB38C File Offset: 0x001C958C
		public void RemovePartsDescendedFrom<T>() where T : class
		{
			for (int i = this.PartsList.Count - 1; i >= 0; i--)
			{
				if (this.PartsList[i] is T)
				{
					this.RemovePartAt(i);
				}
			}
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x001CB3CC File Offset: 0x001C95CC
		public void CleanRegisteredPartEvents()
		{
			if (this.RegisteredPartEvents != null)
			{
				string text = null;
				for (;;)
				{
					text = null;
					foreach (KeyValuePair<string, List<IPart>> keyValuePair in this.RegisteredPartEvents)
					{
						if (keyValuePair.Value.Count <= 0)
						{
							text = keyValuePair.Key;
							break;
						}
					}
					if (text == null)
					{
						break;
					}
					this.RegisteredPartEvents.Remove(text);
					text = null;
				}
			}
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x001CB454 File Offset: 0x001C9654
		public bool isDamaged()
		{
			Statistic statistic;
			return this.Statistics != null && this.Statistics.TryGetValue("Hitpoints", out statistic) && statistic.Penalty > 0;
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x001CB48C File Offset: 0x001C968C
		public bool isDamaged(double howMuch = 1.0, bool inclusive = false)
		{
			if (this.Statistics == null)
			{
				return false;
			}
			Statistic statistic;
			if (!this.Statistics.TryGetValue("Hitpoints", out statistic))
			{
				return false;
			}
			if (inclusive)
			{
				return (double)statistic.Value <= (double)statistic.BaseValue * howMuch;
			}
			return (double)statistic.Value < (double)statistic.BaseValue * howMuch;
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x001CB4E4 File Offset: 0x001C96E4
		public bool isDamaged(int percentageHowMuch, bool inclusive = false)
		{
			if (this.Statistics == null)
			{
				return false;
			}
			Statistic statistic;
			if (!this.Statistics.TryGetValue("Hitpoints", out statistic))
			{
				return false;
			}
			if (inclusive)
			{
				return statistic.Value <= statistic.BaseValue * percentageHowMuch / 100;
			}
			return statistic.Value < statistic.BaseValue * percentageHowMuch / 100;
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x001CB540 File Offset: 0x001C9740
		public int GetPercentDamaged()
		{
			Statistic statistic;
			if (!this.Statistics.TryGetValue("Hitpoints", out statistic))
			{
				return 0;
			}
			return 100 - statistic.Value * 100 / statistic.BaseValue;
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06005F1E RID: 24350 RVA: 0x001CB578 File Offset: 0x001C9778
		public int baseHitpoints
		{
			get
			{
				Statistic statistic;
				if (this.Statistics != null && this.Statistics.TryGetValue("Hitpoints", out statistic))
				{
					return statistic.BaseValue;
				}
				return 0;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06005F1F RID: 24351 RVA: 0x001CB5AC File Offset: 0x001C97AC
		// (set) Token: 0x06005F20 RID: 24352 RVA: 0x001CB5E0 File Offset: 0x001C97E0
		public int hitpoints
		{
			get
			{
				Statistic statistic;
				if (this.Statistics != null && this.Statistics.TryGetValue("Hitpoints", out statistic))
				{
					return statistic.Value;
				}
				return 0;
			}
			set
			{
				Statistic statistic;
				if (this.Statistics != null && this.Statistics.TryGetValue("Hitpoints", out statistic))
				{
					int num = value - statistic.Value;
					if (num != 0)
					{
						statistic.Penalty -= num;
					}
				}
			}
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x001CB623 File Offset: 0x001C9823
		public GameObject equippedOrSelf()
		{
			return this.Equipped ?? this;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x001CB630 File Offset: 0x001C9830
		public bool IsVisible()
		{
			if (this.IsPlayer())
			{
				return true;
			}
			if (this.HasPropertyOrTag("Non"))
			{
				return false;
			}
			if (this.Physics == null)
			{
				return false;
			}
			if (this.Render == null || !this.Render.Visible)
			{
				return false;
			}
			if (FungalVisionary.VisionLevel <= 0 && this.HasPart<FungalVision>())
			{
				return false;
			}
			if (this.IsHidden)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell != null)
			{
				if (this.OnWorldMap())
				{
					return true;
				}
				if (this.ThePlayer == null)
				{
					return false;
				}
				int num = currentCell.DistanceTo(this.ThePlayer);
				if (num > XRLCore.PlayerVisibilityRadius)
				{
					return false;
				}
				if (!this.ConsiderSolidInRenderingContext() && currentCell.GetLight() < LightLevel.Radar)
				{
					int i = 0;
					int count = currentCell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = currentCell.Objects[i];
						if (gameObject != this && gameObject.ConsiderSolidInRenderingContext())
						{
							return false;
						}
						i++;
					}
				}
				if (currentCell.IsVisible())
				{
					return true;
				}
				if (num == 1)
				{
					Zone parentZone = currentCell.ParentZone;
					Zone currentZone = this.ThePlayer.CurrentZone;
					if (parentZone != currentZone)
					{
						if (parentZone.Z == currentZone.Z)
						{
							return true;
						}
						if (parentZone.Z == currentZone.Z + 1)
						{
							if (currentCell.HasObjectWithPart("StairsUp"))
							{
								return true;
							}
						}
						else if (parentZone.Z == currentZone.Z - 1 && currentCell.HasObjectWithPart("StairsDown"))
						{
							return true;
						}
					}
				}
			}
			GameObject inInventory = this.InInventory;
			if (inInventory != null)
			{
				return inInventory.IsVisible();
			}
			GameObject equipped = this.Equipped;
			if (equipped == null)
			{
				GameObject implantee = this.Implantee;
				return implantee != null && implantee.IsVisible();
			}
			return equipped.IsVisible();
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x001CB7C4 File Offset: 0x001C99C4
		public string GetGenotype()
		{
			return this.GetPropertyOrTag("Genotype", null);
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x001CB7D2 File Offset: 0x001C99D2
		public string GetSubtype()
		{
			return this.GetPropertyOrTag("Subtype", null);
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x001CB7E0 File Offset: 0x001C99E0
		public bool IsTrueKin()
		{
			return IsTrueKinEvent.Check(this);
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x001CB7E8 File Offset: 0x001C99E8
		public bool IsMutant()
		{
			return IsMutantEvent.Check(this);
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x001CB7F0 File Offset: 0x001C99F0
		public int GetEpistemicStatus(Examiner Ex)
		{
			if (Ex == null)
			{
				return 2;
			}
			return Ex.GetEpistemicStatus();
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x001CB800 File Offset: 0x001C9A00
		public bool SetEpistemicStatus(int EpistemicStatus)
		{
			Examiner examiner;
			if (this.TryGetPart<Examiner>(out examiner))
			{
				examiner.SetEpistemicStatus(EpistemicStatus);
				return true;
			}
			return false;
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x001CB821 File Offset: 0x001C9A21
		public int GetEpistemicStatus()
		{
			return this.GetEpistemicStatus(this.GetPart<Examiner>());
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x001CB82F File Offset: 0x001C9A2F
		public int GetBlueprintEpistemicStatus()
		{
			return Examiner.GetBlueprintEpistemicStatus(this.GetTinkeringBlueprint());
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x001CB83C File Offset: 0x001C9A3C
		public bool MakeUnderstood(bool ShowMessage = false)
		{
			Examiner part = this.GetPart<Examiner>();
			return part != null && part.MakeUnderstood(ShowMessage);
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x001CB850 File Offset: 0x001C9A50
		public bool MakeUnderstood(out string Message)
		{
			Message = null;
			Examiner part = this.GetPart<Examiner>();
			return part != null && part.MakeUnderstood(out Message);
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x001CB867 File Offset: 0x001C9A67
		public bool Understood(Examiner Ex)
		{
			return this.GetEpistemicStatus(Ex) == 2;
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x001CB873 File Offset: 0x001C9A73
		public bool Understood()
		{
			return this.GetEpistemicStatus() == 2;
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x001CB87E File Offset: 0x001C9A7E
		public bool IsBlueprintUnderstood()
		{
			return this.GetBlueprintEpistemicStatus() == 2;
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x001CB889 File Offset: 0x001C9A89
		public bool MakePartiallyUnderstood(bool ShowMessage = false)
		{
			Examiner part = this.GetPart<Examiner>();
			return part != null && part.MakePartiallyUnderstood(ShowMessage);
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x001CB89D File Offset: 0x001C9A9D
		public bool MakePartiallyUnderstood(out string Message)
		{
			Message = null;
			Examiner part = this.GetPart<Examiner>();
			return part != null && part.MakePartiallyUnderstood(out Message);
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x001CB8B4 File Offset: 0x001C9AB4
		public bool PartiallyUnderstood(Examiner Ex)
		{
			return this.GetEpistemicStatus(Ex) == 1;
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x001CB8C0 File Offset: 0x001C9AC0
		public bool PartiallyUnderstood()
		{
			return this.GetEpistemicStatus() == 1;
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x001CB8CB File Offset: 0x001C9ACB
		public bool IsBlueprintPartiallyUnderstood()
		{
			return this.GetBlueprintEpistemicStatus() == 1;
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x001CB8D8 File Offset: 0x001C9AD8
		public int QueryCharge(bool LiveOnly = false, long GridMask = 0L, bool IncludeTransient = true, bool IncludeBiological = true)
		{
			return QueryChargeEvent.Retrieve(this, this, 1, GridMask, false, LiveOnly, IncludeTransient, IncludeBiological);
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x001CB8F8 File Offset: 0x001C9AF8
		public bool TestCharge(int Charge, bool LiveOnly = false, long GridMask = 0L, bool IncludeTransient = true, bool IncludeBiological = true)
		{
			return Charge <= 0 || TestChargeEvent.Check(this, this, Charge, 1, GridMask, false, LiveOnly, IncludeTransient, IncludeBiological);
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x001CB920 File Offset: 0x001C9B20
		public bool UseCharge(int Charge, bool LiveOnly = false, long GridMask = 0L, bool IncludeTransient = true, bool IncludeBiological = true, int? PowerLoadLevel = null)
		{
			if (Charge <= 0)
			{
				return true;
			}
			int num = PowerLoadLevel ?? GetPowerLoadLevelEvent.GetFor(this, 100);
			if (this.Equipped != null)
			{
				UsingChargeEvent.Send(this, this, Charge, 1, GridMask, false, LiveOnly, IncludeTransient, IncludeBiological, num);
			}
			return UseChargeEvent.Check(this, this, Charge, 1, GridMask, false, LiveOnly, IncludeTransient, IncludeBiological, num);
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x001CB97F File Offset: 0x001C9B7F
		public int QueryChargeStorage(bool IncludeTransient = true, bool IncludeBiological = true, GameObject Source = null)
		{
			return QueryChargeStorageEvent.Retrieve(this, Source ?? this, IncludeTransient, 0L, false, false, IncludeBiological);
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x001CB994 File Offset: 0x001C9B94
		public int QueryChargeStorage(out int Transient, out bool UnlimitedTransient, GameObject Source = null, bool IncludeBiological = true)
		{
			QueryChargeStorageEvent queryChargeStorageEvent;
			QueryChargeStorageEvent.Retrieve(out queryChargeStorageEvent, this, Source ?? this, true, 0L, false, false, IncludeBiological);
			if (queryChargeStorageEvent != null)
			{
				Transient = queryChargeStorageEvent.Transient;
				UnlimitedTransient = queryChargeStorageEvent.UnlimitedTransient;
				return queryChargeStorageEvent.Amount;
			}
			Transient = 0;
			UnlimitedTransient = false;
			return 0;
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x001CB9D8 File Offset: 0x001C9BD8
		public int ChargeAvailable(int Charge, long GridMask = 0L, int MultipleCharge = 1, bool Forced = false, GameObject Source = null)
		{
			if (Charge <= 0)
			{
				return 0;
			}
			Charge *= MultipleCharge;
			ChargeAvailableEvent chargeAvailableEvent;
			ChargeAvailableEvent.Send(out chargeAvailableEvent, this, Source ?? this, Charge, MultipleCharge, GridMask, Forced);
			if (chargeAvailableEvent == null)
			{
				return FinishChargeAvailableEvent.Send(this, Source ?? this, Charge, MultipleCharge, GridMask, Forced, true, true);
			}
			return FinishChargeAvailableEvent.Send(this, chargeAvailableEvent);
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x001CBA24 File Offset: 0x001C9C24
		public int QueryChargeProduction()
		{
			return QueryChargeProductionEvent.Retrieve(this, this, 0L, false, false);
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x001CBA31 File Offset: 0x001C9C31
		public override string ToString()
		{
			if (this.IsPlayer())
			{
				return "The Player";
			}
			if (this == GameObject.Invalid)
			{
				return "Invalid";
			}
			return this.Blueprint;
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x001CBA58 File Offset: 0x001C9C58
		public void StripOffGear()
		{
			Body body = this.Body;
			if (body != null)
			{
				BodyPart body2 = body.GetBody();
				if (body2 != null)
				{
					body2.UnequipPartAndChildren(false, null, false);
				}
			}
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				foreach (GameObject gameObject in inventory.GetObjects())
				{
					if (!gameObject.IsNatural())
					{
						inventory.RemoveObject(gameObject, null);
					}
				}
			}
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x001CBAE0 File Offset: 0x001C9CE0
		public void RandomlySpendPoints(int maxAPtospend = 2147483647, int maxSPtospend = 2147483647, int maxMPtospend = 2147483647, StringBuilder result = null)
		{
			int num = Math.Max(0, this.Stat("AP", 0) - maxAPtospend);
			while (this.Stat("AP", 0) > num)
			{
				int num2 = this.Stat("AP", 0);
				Statistic statistic = this.Statistics["AP"];
				int num3 = statistic.Penalty;
				statistic.Penalty = num3 + 1;
				int num4 = XRL.Rules.Stat.Random(1, 6);
				if (num4 == 1)
				{
					Statistic statistic2 = this.Statistics["Strength"];
					num3 = statistic2.BaseValue;
					statistic2.BaseValue = num3 + 1;
				}
				if (num4 == 2)
				{
					Statistic statistic3 = this.Statistics["Intelligence"];
					num3 = statistic3.BaseValue;
					statistic3.BaseValue = num3 + 1;
				}
				if (num4 == 3)
				{
					Statistic statistic4 = this.Statistics["Willpower"];
					num3 = statistic4.BaseValue;
					statistic4.BaseValue = num3 + 1;
				}
				if (num4 == 4)
				{
					Statistic statistic5 = this.Statistics["Agility"];
					num3 = statistic5.BaseValue;
					statistic5.BaseValue = num3 + 1;
				}
				if (num4 == 5)
				{
					Statistic statistic6 = this.Statistics["Toughness"];
					num3 = statistic6.BaseValue;
					statistic6.BaseValue = num3 + 1;
				}
				if (num4 == 6)
				{
					Statistic statistic7 = this.Statistics["Ego"];
					num3 = statistic7.BaseValue;
					statistic7.BaseValue = num3 + 1;
				}
				if (this.Stat("AP", 0) == num2)
				{
					break;
				}
			}
			int num5 = Math.Max(0, this.Stat("SP", 0) - maxSPtospend);
			while (this.Stat("SP", 0) > num5)
			{
				int num6 = this.Stat("SP", 0);
				List<PowerEntry> list = new List<PowerEntry>(64);
				List<int> list2 = new List<int>();
				foreach (SkillEntry skillEntry in SkillFactory.Factory.SkillList.Values)
				{
					if (!skillEntry.Initiatory || this.HasSkill(skillEntry.Class))
					{
						foreach (PowerEntry powerEntry in skillEntry.Powers.Values)
						{
							int num7 = powerEntry.Cost;
							if (num7 == 0)
							{
								num7 = skillEntry.Cost;
							}
							if (num7 <= this.GetStatValue("SP", 0) && powerEntry.MeetsRequirements(this, false))
							{
								list.Add(powerEntry);
								list2.Add(num7);
							}
						}
					}
				}
				if (list.Count <= 0)
				{
					break;
				}
				int num8 = XRL.Rules.Stat.Random(0, list2.Count - 1);
				this.Statistics["SP"].Penalty += list2[num8];
				object obj = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Parts.Skill." + list[num8].Class, false, false, true));
				this.GetPart<Skills>().AddSkill(obj as BaseSkill, null, null);
				if (this.Stat("SP", 0) == num6)
				{
					break;
				}
			}
			int num9 = Math.Max(0, this.Stat("MP", 0) - maxMPtospend);
			while (this.Stat("MP", 0) > num9)
			{
				int num10 = this.Stat("MP", 0);
				Mutations part = this.GetPart<Mutations>();
				if (part == null)
				{
					break;
				}
				if (this.Stat("MP", 0) - num9 >= 4)
				{
					if (MutationsAPI.RandomlyMutate(this, null, null, false, result) == null)
					{
						break;
					}
					this.Statistics["MP"].Penalty += 4;
				}
				else
				{
					if (part.MutationList.Count == 0)
					{
						break;
					}
					BaseMutation randomElement = part.MutationList.Where<BaseMutation>((BaseMutation m) => m.CanIncreaseLevel()).GetRandomElement<BaseMutation>(null);
					if (randomElement != null)
					{
						part.LevelMutation(randomElement, randomElement.BaseLevel + 1);
						if (result != null)
						{
							result.Append(" ");
							result.Append(this.Poss(string.Concat(new string[]
							{
								"base rank in ",
								randomElement.GetDisplayName(true),
								" increases to {{C|",
								randomElement.BaseLevel.ToString(),
								"}}!"
							}), true));
						}
						this.UseMP(1, "RandomlySpendPoints");
					}
				}
				if (this.Stat("MP", 0) == num10)
				{
					break;
				}
			}
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x001CBF64 File Offset: 0x001CA164
		public int GetPsychicGlimmer(List<GameObject> domChain = null)
		{
			if (this.HasEffect<Dominated>())
			{
				Dominated effect = this.GetEffect<Dominated>();
				if (effect != null && effect.Dominator != null)
				{
					if (domChain == null)
					{
						domChain = new List<GameObject>(1) { this };
					}
					else
					{
						if (domChain.Contains(this))
						{
							goto IL_004B;
						}
						domChain.Add(this);
					}
					return effect.Dominator.GetPsychicGlimmer(domChain);
				}
			}
			IL_004B:
			return GetPsychicGlimmerEvent.GetFor(this, this.GetIntProperty("GlimmerModifier", 0));
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x001CBFCE File Offset: 0x001CA1CE
		public bool InActiveZone()
		{
			return this.CurrentZone == XRLCore.Core.Game.ZoneManager.ActiveZone;
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x001CBFEC File Offset: 0x001CA1EC
		public bool IsPotentiallyMobile()
		{
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			Brain brain = this.Brain;
			if (brain != null)
			{
				brain.CheckMobility(out flag, out flag2, out flag3);
			}
			return !flag;
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x001CC01A File Offset: 0x001CA21A
		public bool IsMobile()
		{
			return this.IsPotentiallyMobile() && !this.IsFrozen() && this.FireEvent(GameObject.eIsMobile);
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x001CC03B File Offset: 0x001CA23B
		public bool CanHypersensesDetect()
		{
			return this.FireEvent(GameObject.eCanHypersensesDetect);
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x001CC048 File Offset: 0x001CA248
		public bool IsCarryingObject(string Blueprint)
		{
			Inventory inventory = this.Inventory;
			if (inventory != null)
			{
				for (int i = 0; i < inventory.Objects.Count; i++)
				{
					if (inventory.Objects[i].Blueprint == Blueprint)
					{
						return true;
					}
				}
			}
			Body body = this.Body;
			return body != null && body.HasEquippedItem(Blueprint);
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x001CC0A8 File Offset: 0x001CA2A8
		public GameObject FindEquippedItem(Predicate<GameObject> Filter)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindEquippedItem(Filter);
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x001CC0BC File Offset: 0x001CA2BC
		public bool HasEquippedItem(string Blueprint)
		{
			Body body = this.Body;
			return body != null && body.HasEquippedItem(Blueprint);
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x001CC0D0 File Offset: 0x001CA2D0
		public bool HasEquippedItem(Predicate<GameObject> Filter)
		{
			Body body = this.Body;
			return body != null && body.HasEquippedItem(Filter);
		}

		// Token: 0x06005F48 RID: 24392 RVA: 0x001CC0E4 File Offset: 0x001CA2E4
		public bool MovingIntoWouldCreateContainmentLoop(GameObject Object)
		{
			if (Object == this)
			{
				return true;
			}
			GameObject inInventory = Object.InInventory;
			return inInventory != null && this.MovingIntoWouldCreateContainmentLoop(inInventory);
		}

		// Token: 0x06005F49 RID: 24393 RVA: 0x001CC10A File Offset: 0x001CA30A
		public bool IsPlayer()
		{
			XRLCore core = XRLCore.Core;
			return ((core != null) ? core.Game : null) != null && this == XRLCore.Core.Game.Player.Body;
		}

		// Token: 0x06005F4A RID: 24394 RVA: 0x001CC138 File Offset: 0x001CA338
		public bool IsActivePlayerBody()
		{
			return this.IsPlayer() && (this.IsOriginalPlayerBody() || !this.HasEffect<Dominated>());
		}

		// Token: 0x06005F4B RID: 24395 RVA: 0x001CC157 File Offset: 0x001CA357
		public bool IsOriginalPlayerBody()
		{
			return this.HasStringProperty("OriginalPlayerBody");
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06005F4C RID: 24396 RVA: 0x001CC164 File Offset: 0x001CA364
		public bool IsImplant
		{
			get
			{
				return this.HasPart<CyberneticsBaseItem>();
			}
		}

		// Token: 0x06005F4D RID: 24397 RVA: 0x001CC16C File Offset: 0x001CA36C
		private string RetrieveEquipProfile(out GameObject User, out bool IsCyber)
		{
			IsCyber = false;
			User = null;
			CyberneticsBaseItem part = this.GetPart<CyberneticsBaseItem>();
			if (part != null)
			{
				IsCyber = true;
				string text = part.Slots;
				User = part.ImplantedOn;
				if (User == null)
				{
					return null;
				}
				return text;
			}
			else
			{
				User = this.Equipped;
				if (User == null)
				{
					return null;
				}
				string text = this.UsesSlots;
				if (!text.IsNullOrEmpty())
				{
					return text;
				}
				Armor part2 = this.GetPart<Armor>();
				if (part2 != null)
				{
					text = part2.WornOn;
					if (!text.IsNullOrEmpty())
					{
						return text;
					}
				}
				XRL.World.Parts.Shield part3 = this.GetPart<XRL.World.Parts.Shield>();
				if (part3 != null)
				{
					text = part3.WornOn;
					if (!text.IsNullOrEmpty())
					{
						return text;
					}
				}
				MissileWeapon part4 = this.GetPart<MissileWeapon>();
				if (part4 != null)
				{
					text = part4.SlotType;
					if (!text.IsNullOrEmpty())
					{
						return text;
					}
				}
				MeleeWeapon part5 = this.GetPart<MeleeWeapon>();
				if (part5 != null)
				{
					return part5.Slot;
				}
				return null;
			}
		}

		// Token: 0x06005F4E RID: 24398 RVA: 0x001CC230 File Offset: 0x001CA430
		public GameObject EquippedProperlyBy()
		{
			bool flag = false;
			GameObject gameObject = null;
			string text = this.RetrieveEquipProfile(out gameObject, out flag);
			if (text.IsNullOrEmpty())
			{
				return null;
			}
			Body body = ((gameObject != null) ? gameObject.Body : null);
			if (body == null)
			{
				return null;
			}
			if (flag)
			{
				if (!body.CheckSlotCyberneticsMatch(this, text))
				{
					return null;
				}
			}
			else if (this.EquipAsDefaultBehavior())
			{
				if (!body.CheckSlotDefaultBehaviorMatch(this, text))
				{
					return null;
				}
			}
			else if (!body.CheckSlotEquippedMatch(this, text))
			{
				return null;
			}
			return gameObject;
		}

		// Token: 0x06005F4F RID: 24399 RVA: 0x001CC298 File Offset: 0x001CA498
		public bool IsEquippedProperly(BodyPart ProspectivelyCheckAgainstPart = null)
		{
			bool flag = false;
			GameObject gameObject = null;
			string text = this.RetrieveEquipProfile(out gameObject, out flag);
			if (text.IsNullOrEmpty())
			{
				return false;
			}
			if (ProspectivelyCheckAgainstPart != null)
			{
				if (text.IndexOf(',') != -1)
				{
					List<string> list = text.CachedCommaExpansion();
					if (!list.Contains("*") && !list.Contains(ProspectivelyCheckAgainstPart.Type) && !list.Contains(ProspectivelyCheckAgainstPart.VariantType))
					{
						return false;
					}
				}
				else if (text != "*" && ProspectivelyCheckAgainstPart.Type != text && ProspectivelyCheckAgainstPart.VariantType != text)
				{
					return false;
				}
				return true;
			}
			if (this.EquipAsDefaultBehavior())
			{
				Body body = gameObject.Body;
				return body != null && body.CheckSlotDefaultBehaviorMatch(this, text);
			}
			if (flag)
			{
				Body body2 = gameObject.Body;
				return body2 != null && body2.CheckSlotCyberneticsMatch(this, text);
			}
			Body body3 = gameObject.Body;
			return body3 != null && body3.CheckSlotEquippedMatch(this, text);
		}

		// Token: 0x06005F50 RID: 24400 RVA: 0x001CC374 File Offset: 0x001CA574
		public bool IsEquippedProperly(string OnType)
		{
			bool flag = false;
			GameObject gameObject = null;
			string text = this.RetrieveEquipProfile(out gameObject, out flag);
			if (text.IsNullOrEmpty())
			{
				return false;
			}
			if (text.IndexOf(',') != -1)
			{
				List<string> list = text.CachedCommaExpansion();
				if (!list.Contains("*") && !list.Contains(OnType))
				{
					return false;
				}
			}
			else if (text != "*" && OnType != text)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06005F51 RID: 24401 RVA: 0x001CC3E0 File Offset: 0x001CA5E0
		public bool IsWorn(BodyPart ProspectivelyCheckAgainstPart = null)
		{
			Armor part = this.GetPart<Armor>();
			string text;
			if (part == null)
			{
				XRL.World.Parts.Shield part2 = this.GetPart<XRL.World.Parts.Shield>();
				if (part2 == null)
				{
					return false;
				}
				text = this.UsesSlots;
				if (text.IsNullOrEmpty())
				{
					text = part2.WornOn;
				}
			}
			else
			{
				text = this.UsesSlots;
				if (text.IsNullOrEmpty())
				{
					text = part.WornOn;
				}
			}
			if (ProspectivelyCheckAgainstPart != null)
			{
				if (text.IndexOf(',') != -1)
				{
					List<string> list = text.CachedCommaExpansion();
					if (!list.Contains("*") && !list.Contains(ProspectivelyCheckAgainstPart.Type))
					{
						return false;
					}
				}
				else if (text != "*" && ProspectivelyCheckAgainstPart.Type != text)
				{
					return false;
				}
				return true;
			}
			bool? flag2;
			if (this.EquipAsDefaultBehavior())
			{
				GameObject equipped = this.Equipped;
				bool? flag;
				if (equipped == null)
				{
					flag = null;
				}
				else
				{
					Body body = equipped.Body;
					flag = ((body != null) ? new bool?(body.CheckSlotDefaultBehaviorMatch(this, text)) : null);
				}
				flag2 = flag;
				return flag2.GetValueOrDefault();
			}
			GameObject equipped2 = this.Equipped;
			bool? flag3;
			if (equipped2 == null)
			{
				flag3 = null;
			}
			else
			{
				Body body2 = equipped2.Body;
				flag3 = ((body2 != null) ? new bool?(body2.CheckSlotEquippedMatch(this, text)) : null);
			}
			flag2 = flag3;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06005F52 RID: 24402 RVA: 0x001CC510 File Offset: 0x001CA710
		public bool IsEquippedOnLimbType(string Type)
		{
			if (this.Equipped == null)
			{
				return false;
			}
			Body body = this.Equipped.Body;
			return body != null && body.IsItemEquippedOnLimbType(this, Type);
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x001CC540 File Offset: 0x001CA740
		public bool IsHeld()
		{
			return this.IsEquippedOnLimbType("Hand") || this.IsEquippedOnLimbType("Missile Weapon");
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x001CC55C File Offset: 0x001CA75C
		public bool IsEquippedOrDefaultOfPrimary(GameObject holder)
		{
			if (holder == null)
			{
				return false;
			}
			Body body = holder.Body;
			return body != null && body.IsPrimaryWeapon(this);
		}

		// Token: 0x06005F55 RID: 24405 RVA: 0x001CC584 File Offset: 0x001CA784
		public bool IsEquippedInMainHand()
		{
			BodyPart bodyPart = this.EquippedOn();
			return bodyPart != null && bodyPart.Primary;
		}

		// Token: 0x06005F56 RID: 24406 RVA: 0x001CC5A3 File Offset: 0x001CA7A3
		public bool IsEquippedAsThrownWeapon()
		{
			return this.IsEquippedOnLimbType("Thrown Weapon");
		}

		// Token: 0x06005F57 RID: 24407 RVA: 0x001CC5B0 File Offset: 0x001CA7B0
		public bool SameAs(GameObject GO)
		{
			if (this.Blueprint != GO.Blueprint)
			{
				return false;
			}
			if (this.PartsList.Count != GO.PartsList.Count)
			{
				return false;
			}
			Dictionary<string, List<IPart>> registeredPartEvents = this.RegisteredPartEvents;
			int num = ((registeredPartEvents != null) ? registeredPartEvents.Count : 0);
			Dictionary<string, List<IPart>> registeredPartEvents2 = GO.RegisteredPartEvents;
			if (num != ((registeredPartEvents2 != null) ? registeredPartEvents2.Count : 0))
			{
				return false;
			}
			EffectRack effects = this._Effects;
			int num2 = ((effects != null) ? effects.Count : 0);
			EffectRack effects2 = GO._Effects;
			if (num2 != ((effects2 != null) ? effects2.Count : 0))
			{
				return false;
			}
			Dictionary<string, List<Effect>> registeredEffectEvents = this.RegisteredEffectEvents;
			int num3 = ((registeredEffectEvents != null) ? registeredEffectEvents.Count : 0);
			Dictionary<string, List<Effect>> registeredEffectEvents2 = GO.RegisteredEffectEvents;
			if (num3 != ((registeredEffectEvents2 != null) ? registeredEffectEvents2.Count : 0))
			{
				return false;
			}
			if (this.Statistics.Count != GO.Statistics.Count)
			{
				return false;
			}
			foreach (Statistic statistic in this.Statistics.Values)
			{
				Statistic statistic2;
				if (!GO.Statistics.TryGetValue(statistic.Name, out statistic2))
				{
					return false;
				}
				if (!statistic2.SameAs(statistic))
				{
					return false;
				}
			}
			for (int i = 0; i < this.PartsList.Count; i++)
			{
				IPart part = GO.GetPart(this.PartsList[i].Name);
				if (part == null)
				{
					return false;
				}
				if (!part.SameAs(this.PartsList[i]))
				{
					return false;
				}
			}
			if (this._Effects != null && this._Effects.Count > 0)
			{
				GameObject.SameAsEffectsUsed.Clear();
				foreach (Effect effect in GO._Effects)
				{
					string name = effect.GetType().Name;
					bool flag = false;
					foreach (Effect effect2 in this._Effects)
					{
						if (effect2.GetType().Name == name && effect2.SameAs(effect) && !GameObject.SameAsEffectsUsed.Contains(effect2))
						{
							flag = true;
							GameObject.SameAsEffectsUsed.Add(effect2);
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (GameObject.SameAsEffectsUsed.Count != this._Effects.Count)
				{
					return false;
				}
			}
			return this.GetIntProperty("Important", 0) == GO.GetIntProperty("Important", 0);
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x001CC878 File Offset: 0x001CAA78
		public int GetBodyPartCountEquippedOn(GameObject Object)
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.GetPartCountEquippedOn(Object);
		}

		// Token: 0x06005F59 RID: 24409 RVA: 0x001CC88C File Offset: 0x001CAA8C
		public int RemoveBodyPartsByManager(string Manager, bool EvenIfDismembered = false)
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.RemovePartsByManager(Manager, EvenIfDismembered);
		}

		// Token: 0x06005F5A RID: 24410 RVA: 0x001CC8A4 File Offset: 0x001CAAA4
		public List<BodyPart> GetBodyPartsByManager(string Manager, bool EvenIfDismembered = false)
		{
			List<BodyPart> list = new List<BodyPart>();
			Body body = this.Body;
			if (body != null)
			{
				body.GetPartsByManager(Manager, list, EvenIfDismembered);
			}
			return list;
		}

		// Token: 0x06005F5B RID: 24411 RVA: 0x001CC8CC File Offset: 0x001CAACC
		public BodyPart GetBodyPartByManager(string Manager, bool EvenIfDismembered = false)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetPartByManager(Manager, EvenIfDismembered);
		}

		// Token: 0x06005F5C RID: 24412 RVA: 0x001CC8E1 File Offset: 0x001CAAE1
		public BodyPart GetBodyPartByManager(string Manager, string Type, bool EvenIfDismembered = false)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetPartByManager(Manager, Type, EvenIfDismembered);
		}

		// Token: 0x06005F5D RID: 24413 RVA: 0x001CC8F7 File Offset: 0x001CAAF7
		public BodyPart GetBodyPartByID(int ID, bool EvenIfDismembered = false)
		{
			return this.Body.GetPartByID(ID, EvenIfDismembered);
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x001CC906 File Offset: 0x001CAB06
		public BodyPart FindEquippedObject(GameObject GO)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindEquippedItem(GO);
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x001CC91A File Offset: 0x001CAB1A
		public BodyPart FindEquippedObject(string Blueprint)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindEquippedItem(Blueprint);
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x001CC92E File Offset: 0x001CAB2E
		public bool HasEquippedObject(string Blueprint)
		{
			Body body = this.Body;
			return ((body != null) ? body.FindEquippedItem(Blueprint) : null) != null;
		}

		// Token: 0x06005F61 RID: 24417 RVA: 0x001CC946 File Offset: 0x001CAB46
		public BodyPart FindCybernetics(GameObject GO)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindCybernetics(GO);
		}

		// Token: 0x06005F62 RID: 24418 RVA: 0x001CC95A File Offset: 0x001CAB5A
		public bool IsADefaultBehavior(GameObject obj)
		{
			Body body = this.Body;
			return body != null && body.IsADefaultBehavior(obj);
		}

		// Token: 0x06005F63 RID: 24419 RVA: 0x001CC970 File Offset: 0x001CAB70
		public void FugueVFX()
		{
			if (!Options.DisableImposters)
			{
				Cell currentCell = this.GetCurrentCell();
				if (currentCell != null && currentCell.InActiveZone)
				{
					CombatJuice.playPrefabAnimation(currentCell.Location, "Abilities/AbilityVFXFugueClone", this.ID, null, null, false);
				}
			}
		}

		// Token: 0x06005F64 RID: 24420 RVA: 0x001CC9B0 File Offset: 0x001CABB0
		public void SmokePuff()
		{
			if (!Options.DisableImposters)
			{
				Cell currentCell = this.GetCurrentCell();
				if (currentCell != null && currentCell.InActiveZone)
				{
					CombatJuice.playPrefabAnimation(currentCell.Location, "Particles/SmokePuff", null, null, null, false);
					return;
				}
			}
			else
			{
				this.Smoke(150, 180);
			}
		}

		// Token: 0x06005F65 RID: 24421 RVA: 0x001CC9FC File Offset: 0x001CABFC
		public void Smoke(int StartAngle, int EndAngle)
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || !currentCell.ParentZone.IsActive())
			{
				return;
			}
			ParticleFX.Smoke(currentCell.X, currentCell.Y, StartAngle, EndAngle);
		}

		// Token: 0x06005F66 RID: 24422 RVA: 0x001CCA34 File Offset: 0x001CAC34
		public void Smoke()
		{
			this.Smoke(85, 185);
		}

		// Token: 0x06005F67 RID: 24423 RVA: 0x001CCA43 File Offset: 0x001CAC43
		public GameObject GetPrimaryWeapon()
		{
			Body body = this.Body;
			return ((body != null) ? body.GetMainWeapon(null, true, true) : null) ?? GameObject.Create("DefaultFist");
		}

		// Token: 0x06005F68 RID: 24424 RVA: 0x001CCA68 File Offset: 0x001CAC68
		public Brain.FeelingLevel GetFeelingLevel(GameObject Object)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return Brain.FeelingLevel.Neutral;
			}
			return brain.GetFeelingLevel(Object);
		}

		// Token: 0x06005F69 RID: 24425 RVA: 0x001CCA7C File Offset: 0x001CAC7C
		public bool IsHostileTowards(GameObject Object)
		{
			if (XRL.The.Core.IgnoreMe && Object != null && Object.IsPlayer())
			{
				return false;
			}
			Brain brain = this.Brain;
			return brain != null && brain.IsHostileTowards(Object);
		}

		// Token: 0x06005F6A RID: 24426 RVA: 0x001CCAA9 File Offset: 0x001CACA9
		public bool IsAlliedTowards(GameObject Object)
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsAlliedTowards(Object);
		}

		// Token: 0x06005F6B RID: 24427 RVA: 0x001CCABD File Offset: 0x001CACBD
		public bool IsNeutralTowards(GameObject Object)
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsNeutralTowards(Object);
		}

		// Token: 0x06005F6C RID: 24428 RVA: 0x001CCAD1 File Offset: 0x001CACD1
		public bool IsRegardedWithHostilityBy(GameObject Object)
		{
			return Object != null && Object.IsHostileTowards(this);
		}

		// Token: 0x06005F6D RID: 24429 RVA: 0x001CCADF File Offset: 0x001CACDF
		private bool IsRegardedWithLocalHostilityBy(GameObject Object)
		{
			return Object != null && Object.IsHostileTowards(this) && Object.PhaseAndFlightMatches(this);
		}

		// Token: 0x06005F6E RID: 24430 RVA: 0x001CCAF8 File Offset: 0x001CACF8
		public bool IsRegardedAsAnAllyBy(GameObject Object)
		{
			return Object != null && Object.IsAlliedTowards(this);
		}

		// Token: 0x06005F6F RID: 24431 RVA: 0x001CCB06 File Offset: 0x001CAD06
		public bool IsRegardedNeutrallyBy(GameObject Object)
		{
			return Object != null && Object.IsNeutralTowards(this);
		}

		// Token: 0x06005F70 RID: 24432 RVA: 0x001CCB14 File Offset: 0x001CAD14
		public bool IsNonAggressive()
		{
			Brain brain = this.Brain;
			return brain == null || brain.IsNonAggressive();
		}

		// Token: 0x06005F71 RID: 24433 RVA: 0x001CCB28 File Offset: 0x001CAD28
		public bool IsRelevantHostile(GameObject Object)
		{
			int num = int.MinValue;
			int num2 = 9999999;
			if (this.IsPlayer())
			{
				if (XRLCore.Core.IDKFA)
				{
					return false;
				}
				num = Options.AutoexploreIgnoreEasyEnemies;
				num2 = Options.AutoexploreIgnoreDistantEnemies;
			}
			if (Object == this || Object.HasTag("ExcludeFromHostiles"))
			{
				return false;
			}
			int? num3 = Object.Con(this, false);
			if (num3 == null)
			{
				return false;
			}
			GetHostilityRecognitionLimitsEvent.GetFor(this, Object, ref num, ref num2);
			int? num4 = num3;
			int num5 = num;
			if ((num4.GetValueOrDefault() < num5) & (num4 != null))
			{
				return false;
			}
			if (Object.IsHostileTowards(this) && !Object.IsNonAggressive())
			{
				int num6 = this.DistanceTo(Object);
				int hostileWalkRadius = Object.GetHostileWalkRadius(this);
				if ((hostileWalkRadius <= 0 || num6 <= hostileWalkRadius) && num6 <= num2 && num6 <= this.GetVisibilityRadius())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005F72 RID: 24434 RVA: 0x001CCBF4 File Offset: 0x001CADF4
		public bool IsIrrelevantHostile(GameObject Object)
		{
			int num = int.MinValue;
			int num2 = 9999999;
			if (this.IsPlayer())
			{
				if (XRLCore.Core.IDKFA)
				{
					return Object.IsHostileTowards(this) && !Object.IsNonAggressive();
				}
				num = Options.AutoexploreIgnoreEasyEnemies;
				num2 = Options.AutoexploreIgnoreDistantEnemies;
			}
			if (Object == this || Object.HasTag("ExcludeFromHostiles"))
			{
				return false;
			}
			if (!Object.IsHostileTowards(this) || Object.IsNonAggressive())
			{
				return false;
			}
			int? num3 = Object.Con(this, false);
			if (num3 == null)
			{
				return true;
			}
			GetHostilityRecognitionLimitsEvent.GetFor(this, Object, ref num, ref num2);
			int? num4 = num3;
			int num5 = num;
			if ((num4.GetValueOrDefault() < num5) & (num4 != null))
			{
				return true;
			}
			int num6 = this.DistanceTo(Object);
			if (num6 > num2)
			{
				return true;
			}
			int hostileWalkRadius = Object.GetHostileWalkRadius(this);
			return (hostileWalkRadius > 0 && num6 > hostileWalkRadius) || num6 > this.GetVisibilityRadius();
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x001CCCD4 File Offset: 0x001CAED4
		public bool IsVisibleHostile(GameObject Object)
		{
			if (this.IsPlayer() && XRLCore.Core.IDKFA)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (XRL.The.ZoneManager.ActiveZone != null)
			{
				XRL.The.Player.HandleEvent(BeforeRenderEvent.Instance);
				XRL.The.ZoneManager.ActiveZone.AddVisibility(currentCell.X, currentCell.Y, this.GetVisibilityRadius());
			}
			int num = int.MinValue;
			int num2 = 9999999;
			if (this.IsPlayer())
			{
				num = Options.AutoexploreIgnoreEasyEnemies;
				num2 = Options.AutoexploreIgnoreDistantEnemies;
			}
			return this.IsVisibleHostileInternal(Object, num, num2);
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x001CCD6C File Offset: 0x001CAF6C
		private bool IsVisibleHostileInternal(GameObject Object, int IgnoreEasierThan = -2147483648, int IgnoreFartherThan = 2147483647)
		{
			if (Object == this || Object.HasTag("ExcludeFromHostiles"))
			{
				return false;
			}
			if (!Object.IsVisible())
			{
				return false;
			}
			GetHostilityRecognitionLimitsEvent.GetFor(this, Object, ref IgnoreEasierThan, ref IgnoreFartherThan);
			if (IgnoreEasierThan > -2147483648)
			{
				int? num = Object.Con(this, false);
				if (num != null)
				{
					int? num2 = num;
					int num3 = IgnoreEasierThan;
					if (!((num2.GetValueOrDefault() < num3) & (num2 != null)))
					{
						goto IL_005D;
					}
				}
				return false;
			}
			IL_005D:
			if (Object.IsHostileTowards(this) && !Object.IsNonAggressive())
			{
				int hostileWalkRadius = Object.GetHostileWalkRadius(this);
				if (hostileWalkRadius > 0)
				{
					int num4 = this.DistanceTo(Object);
					if (num4 <= hostileWalkRadius && num4 <= IgnoreFartherThan && num4 < this.GetVisibilityRadius())
					{
						Cell currentCell = Object.CurrentCell;
						if (currentCell.IsVisible() && currentCell.IsLit())
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x001CCE2D File Offset: 0x001CB02D
		private bool IsVisibleHostileInternal(GameObject Object)
		{
			return this.IsVisibleHostileInternal(Object, int.MinValue, int.MaxValue);
		}

		// Token: 0x06005F76 RID: 24438 RVA: 0x001CCE40 File Offset: 0x001CB040
		public string GenerateSpotMessage(GameObject who, string Description = null, OngoingAction Action = null, string verb = "see", bool CheckingPrior = false, string setting = null, bool treatAsVisible = true)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("You ").Append(verb).Append(' ')
				.Append(who.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, treatAsVisible, false, false, true, false, true, false, null, false, false, false, false, null, false, null, false, true));
			Cell currentCell = this.CurrentCell;
			if (currentCell != null)
			{
				string generalDirectionFromCell = currentCell.GetGeneralDirectionFromCell(who.CurrentCell);
				if (generalDirectionFromCell != ".")
				{
					stringBuilder.Append(" to the ").Append(Directions.GetExpandedDirection(generalDirectionFromCell));
				}
			}
			stringBuilder.Append(CheckingPrior ? ", so you refrain from " : " and stop ").Append(Description ?? ((setting != AutoAct.Setting) ? AutoAct.GetDescription(setting, Action) : AutoAct.GetDescription())).Append('.');
			return stringBuilder.ToString();
		}

		// Token: 0x06005F77 RID: 24439 RVA: 0x001CCF28 File Offset: 0x001CB128
		public bool ArePerceptibleHostilesNearby(bool logSpot = false, bool popSpot = false, string Description = null, OngoingAction Action = null, string setting = null, int IgnoreEasierThan = -2147483648, int IgnoreFartherThan = 40, bool IgnorePlayerTarget = false, bool CheckingPrior = false)
		{
			if (XRLCore.Core.IDKFA)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (currentCell.ParentZone == null)
			{
				return false;
			}
			if (CheckingPrior && XRL.The.ZoneManager.ActiveZone != null)
			{
				this.ThePlayer.HandleEvent(BeforeRenderEvent.Instance);
				XRL.The.ZoneManager.ActiveZone.AddVisibility(currentCell.X, currentCell.Y, this.GetVisibilityRadius());
			}
			GameObject[] array = XRL.The.ActionManager.ActionQueue.GetArray();
			GameObject gameObject = null;
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				GameObject gameObject2 = array[i];
				if (gameObject2.IsValid() && gameObject2.CurrentZone == currentCell.ParentZone && (!IgnorePlayerTarget || gameObject2 != Sidebar.CurrentTarget) && this.IsVisibleHostileInternal(gameObject2, IgnoreEasierThan, IgnoreFartherThan))
				{
					gameObject = gameObject2;
					break;
				}
				i++;
			}
			string text = null;
			bool flag = true;
			GameObject gameObject3;
			string text2;
			bool flag2;
			if (gameObject == null && ExtraHostilePerceptionEvent.Check(this, out gameObject3, out text2, out flag2))
			{
				gameObject = gameObject3;
				text = text2;
				flag = flag2;
			}
			if (gameObject != null)
			{
				if (logSpot || popSpot)
				{
					string text3 = this.GenerateSpotMessage(gameObject, Description, Action, (text == null) ? "see" : text, CheckingPrior, setting, flag);
					if (popSpot)
					{
						Popup.Show(text3, null, "Sounds/UI/ui_notification", true, true, true, logSpot, null);
					}
					else if (logSpot)
					{
						MessageQueue.AddPlayerMessage(text3, null, true);
					}
					gameObject.Indicate(true);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06005F78 RID: 24440 RVA: 0x001CD07C File Offset: 0x001CB27C
		public bool isAdjacentTo(GameObject go)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (currentCell.HasObject(go))
			{
				return true;
			}
			foreach (Cell cell in currentCell.GetLocalAdjacentCells())
			{
				if (cell != null && cell.HasObject(go))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005F79 RID: 24441 RVA: 0x001CD0F4 File Offset: 0x001CB2F4
		public bool AreHostilesAdjacent(bool RequireCombat = true)
		{
			if (RequireCombat)
			{
				return this.CurrentCell.AnyAdjacentCell((Cell C) => C.HasObjectWithPart("Combat", (GameObject GO) => GO != this && GO.IsHostileTowards(this)), true);
			}
			return this.CurrentCell.AnyAdjacentCell((Cell C) => C.HasObjectWithPart("Brain", (GameObject GO) => GO != this && GO.IsHostileTowards(this)), true);
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x001CD12C File Offset: 0x001CB32C
		public bool AreViableHostilesAdjacent(bool IgnoreFlight = false, bool IgnorePhase = false)
		{
			foreach (Cell cell in this.CurrentCell.IterateAdjacent(1, false, false, true))
			{
				foreach (GameObject gameObject in cell.Objects)
				{
					if (gameObject.IsCombatObject() && !gameObject.HasPropertyOrTag("ExcludeFromHostiles") && this.IsRegardedWithHostilityBy(gameObject) && (IgnoreFlight || this.FlightMatches(gameObject)) && (IgnorePhase || this.PhaseMatches(gameObject)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005F7B RID: 24443 RVA: 0x001CD1E0 File Offset: 0x001CB3E0
		public void EquipFromPopulationTable(string Table, int ZoneTier = 1, Action<GameObject> Process = null, string Context = null, bool NoStack = false, bool Silent = true)
		{
			if (Table.IsNullOrEmpty())
			{
				return;
			}
			if (this.Inventory == null)
			{
				return;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"ownertier",
					this.GetTier().ToString()
				},
				{
					"ownertechtier",
					this.GetTechTier().ToString()
				},
				{
					"zonetier",
					ZoneTier.ToString()
				},
				{
					"zonetier+1",
					(ZoneTier + 1).ToString()
				}
			};
			foreach (PopulationResult populationResult in PopulationManager.Generate(Table, dictionary, null))
			{
				try
				{
					int num = 0;
					if (populationResult.Hint != null && populationResult.Hint.Contains("SetBonusModChance:"))
					{
						num = populationResult.Hint.Split(':', StringSplitOptions.None)[1].RollCached();
					}
					if (populationResult.Blueprint.StartsWith("*relic:"))
					{
						int i = 0;
						int number = populationResult.Number;
						while (i < number)
						{
							GameObject gameObject = RelicGenerator.GenerateRelic(XRL.The.Game.sultanHistory.entities.GetRandomElement<HistoricEntity>(null).GetCurrentSnapshot(), ZoneTier, populationResult.Blueprint.Split(':', StringSplitOptions.None)[1], false);
							if (Process != null)
							{
								Process(gameObject);
							}
							this.ReceiveObject(gameObject, NoStack, null);
							i++;
						}
					}
					else
					{
						this.ReceiveObject(populationResult.Blueprint, populationResult.Number, NoStack, num, 0, null, null, Process, Context);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Exception creating item from population table: " + populationResult.Blueprint, ex);
				}
			}
		}

		// Token: 0x06005F7C RID: 24444 RVA: 0x001CD3B0 File Offset: 0x001CB5B0
		public void MutateFromPopulationTable(string Table, int ZoneTier = 1)
		{
			Mutations mutations = this.RequirePart<Mutations>(false);
			foreach (PopulationResult populationResult in PopulationManager.Generate(Table, new Dictionary<string, string>
			{
				{
					"ownertier",
					this.GetTier().ToString()
				},
				{
					"ownertechtier",
					this.GetTechTier().ToString()
				},
				{
					"zonetier",
					ZoneTier.ToString()
				},
				{
					"zonetier+1",
					(ZoneTier + 1).ToString()
				}
			}, null))
			{
				if (populationResult.Number > 0)
				{
					Type type = ModManager.ResolveType("XRL.World.Parts.Mutation." + populationResult.Blueprint, false, false, true);
					if (type == null)
					{
						MetricsManager.LogError("Unknown mutation " + populationResult.Blueprint);
					}
					else
					{
						BaseMutation baseMutation = Activator.CreateInstance(type) as BaseMutation;
						if (baseMutation == null)
						{
							MetricsManager.LogError("Mutation " + populationResult.Blueprint + " is not a BaseMutation");
						}
						else
						{
							mutations.AddMutation(baseMutation, populationResult.Number, true);
							if (baseMutation.CapOverride == -1)
							{
								baseMutation.CapOverride = baseMutation.Level;
							}
						}
					}
				}
			}
		}

		// Token: 0x06005F7D RID: 24445 RVA: 0x001CD508 File Offset: 0x001CB708
		private bool CheckHostile(GameObject GO)
		{
			if (GO == this)
			{
				return false;
			}
			if (GO.Brain == null)
			{
				return false;
			}
			if (GO.HasTag("ExcludeFromHostiles"))
			{
				return false;
			}
			if (GO.IsHostileTowards(this) && !GO.IsNonAggressive())
			{
				int hostileWalkRadius = GO.GetHostileWalkRadius(this);
				if (hostileWalkRadius > 0 && this.DistanceTo(GO) <= hostileWalkRadius)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005F7E RID: 24446 RVA: 0x001CD560 File Offset: 0x001CB760
		public bool AreHostilesNearby()
		{
			if (XRLCore.Core.IDKFA)
			{
				return false;
			}
			if (this.OnWorldMap())
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			Zone currentZone = this.CurrentZone;
			return currentZone != null && currentZone.GlobalFloodAny(currentCell.X, currentCell.Y, 7, "Combat", new Predicate<GameObject>(this.CheckHostile), this, true, true);
		}

		// Token: 0x06005F7F RID: 24447 RVA: 0x001CD5C0 File Offset: 0x001CB7C0
		public bool IsInCombat()
		{
			if (this.OnWorldMap())
			{
				return false;
			}
			GameObject target = this.Target;
			if (target != null && target.IsCombatObject())
			{
				return true;
			}
			foreach (Zone zone in XRL.The.ZoneManager.CachedZones.Values)
			{
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width; j++)
					{
						Cell cell = zone.GetCell(j, i);
						int k = 0;
						int count = cell.Objects.Count;
						while (k < count)
						{
							GameObject target2 = cell.Objects[k].Target;
							if (target2 != null)
							{
								if (target2 == this)
								{
									return true;
								}
								if (target2.IsLedBy(this) || this.IsLedBy(target2))
								{
									return true;
								}
							}
							k++;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06005F80 RID: 24448 RVA: 0x001CD6CC File Offset: 0x001CB8CC
		public void Splash(string Particle)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.Splash(Particle);
		}

		// Token: 0x06005F81 RID: 24449 RVA: 0x001CD6DF File Offset: 0x001CB8DF
		public void LiquidSplash(string Color)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.LiquidSplash(Color);
		}

		// Token: 0x06005F82 RID: 24450 RVA: 0x001CD6F2 File Offset: 0x001CB8F2
		public void LiquidSplash(List<string> Colors)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.LiquidSplash(Colors);
		}

		// Token: 0x06005F83 RID: 24451 RVA: 0x001CD705 File Offset: 0x001CB905
		public void LiquidSplash(BaseLiquid Liquid)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			currentCell.LiquidSplash(Liquid);
		}

		// Token: 0x06005F84 RID: 24452 RVA: 0x001CD718 File Offset: 0x001CB918
		public void Splatter(string Particle)
		{
			if (this.CurrentZone != null && this.CurrentZone.IsActive() && !this.IsInStasis())
			{
				for (int i = 0; i < 5; i++)
				{
					float num = (float)XRL.Rules.Stat.RandomCosmetic(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 2f;
					float num3 = (float)Math.Cos((double)num) / 2f;
					XRLCore.ParticleManager.Add(Particle, (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F85 RID: 24453 RVA: 0x001CD7C4 File Offset: 0x001CB9C4
		public void ShatterSplatter()
		{
			if (this.CurrentZone != null && this.CurrentZone.IsActive() && !this.IsInStasis())
			{
				for (int i = 0; i < 5; i++)
				{
					float num = (float)XRL.Rules.Stat.RandomCosmetic(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 2f;
					float num3 = (float)Math.Cos((double)num) / 2f;
					string text = ".";
					if (i == 0)
					{
						text = "&b.";
					}
					if (i == 0)
					{
						text = "&b,";
					}
					if (i == 0)
					{
						text = "&k'";
					}
					if (i == 0)
					{
						text = "&b.";
					}
					if (i == 0)
					{
						text = "&W.";
					}
					XRLCore.ParticleManager.Add(text, (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F86 RID: 24454 RVA: 0x001CD8AC File Offset: 0x001CBAAC
		public void FlingBlood()
		{
			if (Options.DisableBloodsplatter)
			{
				return;
			}
			if (this.Physics == null)
			{
				return;
			}
			if (this.GetIntProperty("Bleeds", 0) <= 0)
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(1, false);
			localAdjacentCells.Add(currentCell);
			string bleedLiquid = this.GetBleedLiquid("blood-1000");
			foreach (Cell cell in localAdjacentCells)
			{
				bool flag = false;
				if (50.in100())
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Render"))
					{
						LiquidVolume liquidVolume = gameObject.LiquidVolume;
						if (liquidVolume != null && liquidVolume.MaxVolume == -1)
						{
							liquidVolume.MixWith(new LiquidVolume(bleedLiquid, XRL.Rules.Stat.Random(1, 3)), null, null, null, false);
							flag = true;
						}
						else
						{
							gameObject.MakeBloody(bleedLiquid, XRL.Rules.Stat.Random(1, 3), 1);
						}
					}
				}
				if (!flag && 5.in100())
				{
					GameObject gameObject2 = GameObject.Create("BloodSplash");
					gameObject2.LiquidVolume.InitialLiquid = bleedLiquid;
					cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x06005F87 RID: 24455 RVA: 0x001CDA28 File Offset: 0x001CBC28
		public void Bloodsplatter()
		{
			this.Bloodsplatter(true);
		}

		// Token: 0x06005F88 RID: 24456 RVA: 0x001CDA34 File Offset: 0x001CBC34
		public void BloodsplatterBurst(bool SelfSplatter, float Angle, int ConeWidth)
		{
			if (Options.DisableBloodsplatter)
			{
				return;
			}
			if (this.GetIntProperty("Bleeds", 0) <= 0)
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			if (currentCell.ParentZone == XRLCore.Core.Game.ZoneManager.ActiveZone && !currentCell.ParentZone.IsWorldMap())
			{
				if (this.HasPart<Robot>())
				{
					this.Sparksplatter();
				}
				else if (this.HasTag("Ooze"))
				{
					this.Slimesplatter(true, "&g.");
				}
				else
				{
					for (int i = 0; i < 10; i++)
					{
						float num = ((float)XRL.Rules.Stat.Random(-ConeWidth / 2, ConeWidth / 2) + Angle) / 58f;
						float num2 = (float)Math.Sin((double)num) / 2f;
						float num3 = (float)Math.Cos((double)num) / 2f;
						if (XRL.Rules.Stat.Random(1, 2) == 1)
						{
							XRLCore.ParticleManager.Add("&r.", (float)currentCell.X, (float)currentCell.Y, num2, num3, 7, 0f, 0f, 0L);
						}
						else
						{
							XRLCore.ParticleManager.Add("&R.", (float)currentCell.X, (float)currentCell.Y, num2, num3, 7, 0f, 0f, 0L);
						}
					}
				}
				List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(1, false);
				if (SelfSplatter)
				{
					localAdjacentCells.Add(currentCell);
				}
				string bleedLiquid = this.GetBleedLiquid("blood-1000");
				foreach (Cell cell in localAdjacentCells)
				{
					bool flag = false;
					if (50.in100())
					{
						foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Render"))
						{
							LiquidVolume liquidVolume = gameObject.LiquidVolume;
							if (liquidVolume != null && liquidVolume.MaxVolume == -1)
							{
								liquidVolume.MixWith(new LiquidVolume(bleedLiquid, XRL.Rules.Stat.Random(1, 3)), null, null, null, false);
								flag = true;
							}
							else
							{
								gameObject.MakeBloody(bleedLiquid, XRL.Rules.Stat.Random(1, 3), 1);
							}
						}
					}
					if (!flag && 10.in100())
					{
						GameObject gameObject2 = GameObject.Create("BloodSplash");
						gameObject2.LiquidVolume.InitialLiquid = bleedLiquid;
						cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x06005F89 RID: 24457 RVA: 0x001CDCCC File Offset: 0x001CBECC
		public void SetActive()
		{
			XRLCore.Core.Game.ActionManager.AddActiveObject(this);
		}

		// Token: 0x06005F8A RID: 24458 RVA: 0x001CDCE3 File Offset: 0x001CBEE3
		public bool MakeBloody(string Liquid = "blood", int Amount = 1, int Duration = 1)
		{
			return this.ForceApplyEffect(new LiquidCovered(Liquid, Amount, Duration, false, null, false), null);
		}

		// Token: 0x06005F8B RID: 24459 RVA: 0x001CDCF7 File Offset: 0x001CBEF7
		public bool MakeBloodstained(string Liquid = "blood", int Amount = 1, int Duration = 9999)
		{
			return this.ForceApplyEffect(new LiquidStained(Liquid, Amount, Duration), null);
		}

		// Token: 0x06005F8C RID: 24460 RVA: 0x001CDD08 File Offset: 0x001CBF08
		public void BloodsplatterCone(bool SelfSplatter, float Angle, int ConeWidth)
		{
			if (Options.DisableBloodsplatter)
			{
				return;
			}
			if (this.GetIntProperty("Bleeds", 0) <= 0)
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			string text = "&r.";
			string text2 = "&R.";
			string bleedLiquid = this.GetBleedLiquid("blood-1000");
			string delimitedSubstring = bleedLiquid.GetDelimitedSubstring('-', 0, false);
			BaseLiquid baseLiquid;
			if (LiquidVolume.Liquids.TryGetValue(delimitedSubstring, out baseLiquid))
			{
				char c = baseLiquid.GetColor()[0];
				char c2 = (char.IsLower(c) ? char.ToUpperInvariant(c) : char.ToLowerInvariant(c));
				text = "&" + c.ToString() + ".";
				text2 = "&" + c2.ToString() + ".";
			}
			if (currentCell.ParentZone == XRLCore.Core.Game.ZoneManager.ActiveZone && !currentCell.ParentZone.IsWorldMap())
			{
				if (this.HasPart<Robot>())
				{
					this.Sparksplatter();
				}
				else if (this.HasTag("Ooze"))
				{
					this.Slimesplatter(true, "&g.");
				}
				else
				{
					for (int i = 0; i < 10; i++)
					{
						float num = ((float)XRL.Rules.Stat.Random(-ConeWidth / 2, ConeWidth / 2) + Angle) / 58f;
						float num2 = (float)Math.Sin((double)num) / 2f;
						float num3 = (float)Math.Cos((double)num) / 2f;
						if (XRL.Rules.Stat.Random(1, 2) == 1)
						{
							XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
						}
						else
						{
							XRLCore.ParticleManager.Add(text2, (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
						}
					}
				}
				List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(1, false);
				if (SelfSplatter)
				{
					localAdjacentCells.Add(currentCell);
				}
				foreach (Cell cell in localAdjacentCells)
				{
					bool flag = false;
					if (50.in100())
					{
						foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Render"))
						{
							LiquidVolume liquidVolume = gameObject.LiquidVolume;
							if (liquidVolume != null && liquidVolume.MaxVolume == -1)
							{
								liquidVolume.MixWith(new LiquidVolume(bleedLiquid, XRL.Rules.Stat.Random(1, 3)), null, null, null, false);
								flag = true;
							}
							else
							{
								gameObject.MakeBloody(bleedLiquid, XRL.Rules.Stat.Random(1, 3), 1);
							}
						}
					}
					if (!flag && 10.in100())
					{
						GameObject gameObject2 = GameObject.Create("BloodSplash");
						gameObject2.LiquidVolume.InitialLiquid = bleedLiquid;
						cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x06005F8D RID: 24461 RVA: 0x001CE024 File Offset: 0x001CC224
		public void HolographicBloodsplatter(bool SelfSplatter = true)
		{
			if (this.IsInStasis())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			string text = "&r.";
			string bleedLiquid = this.GetBleedLiquid("blood-1000");
			string delimitedSubstring = bleedLiquid.GetDelimitedSubstring('-', 0, false);
			BaseLiquid baseLiquid;
			if (LiquidVolume.Liquids.TryGetValue(delimitedSubstring, out baseLiquid))
			{
				text = "&" + baseLiquid.GetColor() + ".";
			}
			if (currentCell.ParentZone.IsActive())
			{
				if (this.HasPart<Robot>())
				{
					this.Sparksplatter();
				}
				else if (this.HasTag("Ooze"))
				{
					this.Slimesplatter(true, "&g.");
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
						float num2 = (float)Math.Sin((double)num) / 2f;
						float num3 = (float)Math.Cos((double)num) / 2f;
						XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num3, 5, 0f, 0f, 0L);
					}
				}
			}
			List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(1, false);
			if (SelfSplatter)
			{
				localAdjacentCells.Add(currentCell);
			}
			foreach (Cell cell in localAdjacentCells)
			{
				if (10.in100())
				{
					cell.ForeachObjectWithPart("Render", new Action<GameObject>(this.SplashHolographicBlood));
				}
				if (10.in100())
				{
					GameObject gameObject = GameObject.Create("BloodSplash");
					gameObject.LiquidVolume.InitialLiquid = bleedLiquid;
					gameObject.AddPart<XRL.World.Parts.Temporary>(new XRL.World.Parts.Temporary(25), true, false);
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x06005F8E RID: 24462 RVA: 0x001CE20C File Offset: 0x001CC40C
		private void SplashHolographicBlood(GameObject GO)
		{
			LiquidVolume liquidVolume = GO.LiquidVolume;
			if (liquidVolume == null || liquidVolume.MaxVolume != -1)
			{
				GO.MakeBloody("blood", XRL.Rules.Stat.Random(1, 3), 1);
			}
		}

		// Token: 0x06005F8F RID: 24463 RVA: 0x001CE240 File Offset: 0x001CC440
		public void BigBloodsplatter(bool SelfSplatter = true)
		{
			if (this.GetIntProperty("Bleeds", 0) <= 0)
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			string text = "&r.";
			string bleedLiquid = this.GetBleedLiquid("blood-1000");
			string delimitedSubstring = bleedLiquid.GetDelimitedSubstring('-', 0, false);
			BaseLiquid baseLiquid;
			if (LiquidVolume.Liquids.TryGetValue(delimitedSubstring, out baseLiquid))
			{
				text = "&" + baseLiquid.GetColor() + ".";
			}
			if (currentCell.ParentZone.IsActive())
			{
				if (this.HasPart<Robot>())
				{
					this.Sparksplatter();
				}
				else if (this.HasTag("Ooze"))
				{
					this.Slimesplatter(true, "&g.");
				}
				else
				{
					for (int i = 0; i < 15; i++)
					{
						float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
						float num2 = (float)Math.Sin((double)num) / 2f;
						float num3 = (float)Math.Cos((double)num) / 2f;
						XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
					}
				}
			}
			List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(3, false);
			if (SelfSplatter)
			{
				localAdjacentCells.Add(currentCell);
			}
			foreach (Cell cell in localAdjacentCells)
			{
				bool flag = false;
				if (50.in100())
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Render"))
					{
						LiquidVolume liquidVolume = gameObject.LiquidVolume;
						if (liquidVolume != null && liquidVolume.MaxVolume == -1)
						{
							liquidVolume.MixWith(new LiquidVolume(bleedLiquid, XRL.Rules.Stat.Random(1, 3)), null, null, null, false);
							flag = true;
						}
						else
						{
							gameObject.MakeBloody(bleedLiquid, XRL.Rules.Stat.Random(1, 3), 1);
						}
					}
				}
				if (!flag && 10.in100())
				{
					GameObject gameObject2 = GameObject.Create("BloodSplash");
					gameObject2.LiquidVolume.InitialLiquid = bleedLiquid;
					cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x06005F90 RID: 24464 RVA: 0x001CE4B4 File Offset: 0x001CC6B4
		public void Bloodsplatter(bool SelfSplatter)
		{
			if (this.GetIntProperty("Bleeds", 0) <= 0)
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			string text = "&r.";
			string bleedLiquid = this.GetBleedLiquid("blood-1000");
			string delimitedSubstring = bleedLiquid.GetDelimitedSubstring('-', 0, false);
			BaseLiquid baseLiquid;
			if (LiquidVolume.Liquids.TryGetValue(delimitedSubstring, out baseLiquid))
			{
				text = "&" + baseLiquid.GetColor() + ".";
			}
			if (currentCell.ParentZone.IsActive())
			{
				if (this.HasPart<Robot>())
				{
					this.Sparksplatter();
				}
				else if (this.HasTag("Ooze"))
				{
					this.Slimesplatter(true, "&g.");
				}
				else
				{
					for (int i = 0; i < 5; i++)
					{
						float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
						float num2 = (float)Math.Sin((double)num) / 2f;
						float num3 = (float)Math.Cos((double)num) / 2f;
						XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num3, 5, 0f, 0f, 0L);
					}
				}
			}
			List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(1, false);
			if (SelfSplatter)
			{
				localAdjacentCells.Add(currentCell);
			}
			foreach (Cell cell in localAdjacentCells)
			{
				bool flag = false;
				if (50.in100())
				{
					foreach (GameObject gameObject in cell.GetObjectsWithPartReadonly("Render"))
					{
						LiquidVolume liquidVolume = gameObject.LiquidVolume;
						if (liquidVolume != null && liquidVolume.MaxVolume == -1)
						{
							liquidVolume.MixWith(new LiquidVolume(bleedLiquid, XRL.Rules.Stat.Random(1, 3)), null, null, null, false);
							flag = true;
						}
						else
						{
							gameObject.MakeBloody(bleedLiquid, XRL.Rules.Stat.Random(1, 3), 1);
						}
					}
				}
				if (!flag && 10.in100())
				{
					GameObject gameObject2 = GameObject.Create("BloodSplash");
					gameObject2.LiquidVolume.InitialLiquid = bleedLiquid;
					cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
		}

		// Token: 0x06005F91 RID: 24465 RVA: 0x001CE724 File Offset: 0x001CC924
		public void DilationSplat()
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.DilationSplat();
		}

		// Token: 0x06005F92 RID: 24466 RVA: 0x001CE736 File Offset: 0x001CC936
		public void ImplosionSplat(int Radius = 12)
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.ImplosionSplat(Radius);
		}

		// Token: 0x06005F93 RID: 24467 RVA: 0x001CE749 File Offset: 0x001CC949
		public void TelekinesisBlip()
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.TelekinesisBlip();
		}

		// Token: 0x06005F94 RID: 24468 RVA: 0x001CE75C File Offset: 0x001CC95C
		public void Acidsplatter()
		{
			if (this.OnWorldMap())
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			if (this.CurrentZone != null && this.CurrentZone.IsActive())
			{
				for (int i = 0; i < 5; i++)
				{
					float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 2f;
					float num3 = (float)Math.Cos((double)num) / 2f;
					int num4 = XRL.Rules.Stat.Random(1, 2);
					string text = "";
					if (num4 == 0)
					{
						text += "&G";
					}
					if (num4 == 1)
					{
						text += "&g";
					}
					text += ".";
					XRLCore.ParticleManager.Add(text, (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F95 RID: 24469 RVA: 0x001CE84C File Offset: 0x001CCA4C
		public void Firesplatter()
		{
			if (this.OnWorldMap())
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			if (this.CurrentZone != null && this.CurrentZone.IsActive())
			{
				for (int i = 0; i < 5; i++)
				{
					float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 2f;
					float num3 = (float)Math.Cos((double)num) / 2f;
					int num4 = XRL.Rules.Stat.Random(1, 2);
					string text = "";
					if (num4 == 0)
					{
						text += "&R";
					}
					if (num4 == 1)
					{
						text += "&W";
					}
					text += ".";
					XRLCore.ParticleManager.Add(text, (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F96 RID: 24470 RVA: 0x001CE93C File Offset: 0x001CCB3C
		public void Icesplatter()
		{
			if (this.OnWorldMap())
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			if (this.CurrentZone != null && this.CurrentZone.IsActive())
			{
				for (int i = 0; i < 5; i++)
				{
					float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 2f;
					float num3 = (float)Math.Cos((double)num) / 2f;
					int num4 = XRL.Rules.Stat.Random(1, 2);
					string text = "";
					if (num4 == 0)
					{
						text += "&C";
					}
					if (num4 == 1)
					{
						text += "&Y";
					}
					text += ".";
					XRLCore.ParticleManager.Add(text, (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F97 RID: 24471 RVA: 0x001CEA2C File Offset: 0x001CCC2C
		public void Sparksplatter()
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || currentCell.OnWorldMap() || currentCell.ParentZone == null || !currentCell.ParentZone.IsActive() || this.IsInStasis())
			{
				return;
			}
			int phase = this.GetPhase();
			for (int i = 0; i < 5; i++)
			{
				float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
				float num2 = (float)(Math.Sin((double)num) / 2.0);
				float num3 = (float)(Math.Cos((double)num) / 2.0);
				string text = "&" + Phase.getRandomElectricArcColor(phase).ToString() + ((char)XRL.Rules.Stat.RandomCosmetic(191, 198)).ToString();
				XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num3, 5, 0f, 0f, 0L);
			}
		}

		// Token: 0x06005F98 RID: 24472 RVA: 0x001CEB18 File Offset: 0x001CCD18
		public void Rainbowsplatter()
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap() || !currentCell.ParentZone.IsActive())
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			for (int i = 0; i < 5; i++)
			{
				float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
				float num2 = (float)Math.Sin((double)num) / 2f;
				float num3 = (float)Math.Cos((double)num) / 2f;
				int num4 = XRL.Rules.Stat.Random(1, 2);
				string text = "";
				if (num4 == 0)
				{
					text = text + "&" + Crayons.GetRandomColor(null);
				}
				if (num4 == 1)
				{
					text = text + "&" + Crayons.GetRandomColor(null).ToLower();
				}
				text += ((char)XRL.Rules.Stat.RandomCosmetic(191, 198)).ToString();
				XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num3, 5, 0f, 0f, 0L);
			}
		}

		// Token: 0x06005F99 RID: 24473 RVA: 0x001CEC2C File Offset: 0x001CCE2C
		public void Slimesplatter(bool SelfSplatter, string particle = "&g.")
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			if (this.IsInStasis())
			{
				return;
			}
			if (currentCell.ParentZone.IsActive())
			{
				for (int i = 0; i < 5; i++)
				{
					float num = (float)XRL.Rules.Stat.Random(0, 359) / 58f;
					float num2 = (float)Math.Sin((double)num) / 2f;
					float num3 = (float)Math.Cos((double)num) / 2f;
					XRLCore.ParticleManager.Add(particle, (float)currentCell.X, (float)currentCell.Y, num2, num3, 5, 0f, 0f, 0L);
				}
			}
			List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(1, false);
			if (SelfSplatter)
			{
				localAdjacentCells.Add(currentCell);
			}
			foreach (Cell cell in localAdjacentCells)
			{
				if (50.in100())
				{
					cell.ForeachObjectWithPart("Render", delegate(GameObject GO)
					{
						if (GO.HasPart<LiquidVolume>() && GO.LiquidVolume.MaxVolume == -1)
						{
							LiquidVolume liquidVolume = GO.LiquidVolume;
							LiquidVolume liquidVolume2 = GameObjectFactory.Factory.CreateObject("Water").LiquidVolume;
							liquidVolume2.InitialLiquid = "slime-1000";
							liquidVolume2.Volume = 2;
							liquidVolume.MixWith(liquidVolume2, null, null, null, false);
						}
					});
				}
			}
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x001CED5C File Offset: 0x001CCF5C
		public void DotPuff(string Color)
		{
			if (this.CurrentZone == null || !this.CurrentZone.IsActive())
			{
				return;
			}
			for (int i = 0; i < 15; i++)
			{
				float num = (float)XRL.Rules.Stat.RandomCosmetic(0, 359) / 58f;
				float num2 = (float)Math.Sin((double)num) / 4f;
				float num3 = (float)Math.Cos((double)num) / 4f;
				if (XRL.Rules.Stat.RandomCosmetic(1, 4) <= 3)
				{
					XRLCore.ParticleManager.Add(Color + ".", (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 15, 0f, 0f, 0L);
				}
				else
				{
					XRLCore.ParticleManager.Add(Color + "ù", (float)this.CurrentCell.X, (float)this.CurrentCell.Y, num2, num3, 15, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x001CEE54 File Offset: 0x001CD054
		public void PistonPuff(string Color = "&y", int Intensity = 15)
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			if (!currentCell.ParentZone.IsActive())
			{
				return;
			}
			for (int i = 0; i < Intensity; i++)
			{
				float num = (float)XRL.Rules.Stat.RandomCosmetic(0, 359) / 58f;
				float num2 = (float)Math.Sin((double)num) / 4f;
				float num3 = (float)Math.Cos((double)num) / 4f;
				if (XRL.Rules.Stat.RandomCosmetic(1, 4) <= 2)
				{
					XRL.The.ParticleManager.Add(Color + ".", (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
				}
				else
				{
					XRL.The.ParticleManager.Add(Color + "±", (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F9C RID: 24476 RVA: 0x001CEF3C File Offset: 0x001CD13C
		public void DustPuff(string Color = "&y", int Intensity = 15)
		{
			if (!this.IsVisible())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			if (currentCell.ParentZone != XRLCore.Core.Game.ZoneManager.ActiveZone)
			{
				return;
			}
			for (int i = 0; i < Intensity; i++)
			{
				float num = (float)XRL.Rules.Stat.RandomCosmetic(0, 359) / 58f;
				float num2 = (float)Math.Sin((double)num) / 4f;
				float num3 = (float)Math.Cos((double)num) / 4f;
				if (XRL.Rules.Stat.RandomCosmetic(1, 4) <= 3)
				{
					XRLCore.ParticleManager.Add(Color + ".", (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
				}
				else
				{
					XRLCore.ParticleManager.Add(Color + "±", (float)currentCell.X, (float)currentCell.Y, num2, num3, 15, 0f, 0f, 0L);
				}
			}
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x001CF03C File Offset: 0x001CD23C
		public void PsychicPulse()
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					this.ParticleText("&B" + ((char)(219 + XRL.Rules.Stat.RandomCosmetic(0, 4))).ToString(), 4.9f, 5);
				}
				for (int k = 0; k < 5; k++)
				{
					this.ParticleText("&b" + ((char)(219 + XRL.Rules.Stat.RandomCosmetic(0, 4))).ToString(), 4.9f, 5);
				}
				for (int l = 0; l < 5; l++)
				{
					this.ParticleText("&W" + ((char)(219 + XRL.Rules.Stat.RandomCosmetic(0, 4))).ToString(), 4.9f, 5);
				}
			}
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x001CF108 File Offset: 0x001CD308
		public void Soundwave()
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell != null && currentCell.ParentZone == XRLCore.Core.Game.ZoneManager.ActiveZone)
			{
				for (int i = 0; i < 3; i++)
				{
					XRLCore.ParticleManager.AddRadial("&R!", (float)currentCell.X, (float)currentCell.Y, (float)XRL.Rules.Stat.RandomCosmetic(0, 7), (float)XRL.Rules.Stat.RandomCosmetic(1, 1), 0.015f * (float)XRL.Rules.Stat.RandomCosmetic(8, 12), 0.3f + 0.05f * (float)XRL.Rules.Stat.RandomCosmetic(1, 3), 40, 0L);
					XRLCore.ParticleManager.AddRadial("&r!", (float)currentCell.X, (float)currentCell.Y, (float)XRL.Rules.Stat.RandomCosmetic(0, 7), (float)XRL.Rules.Stat.RandomCosmetic(1, 1), 0.015f * (float)XRL.Rules.Stat.RandomCosmetic(8, 12), 0.3f + 0.05f * (float)XRL.Rules.Stat.RandomCosmetic(1, 3), 40, 0L);
					XRLCore.ParticleManager.AddRadial("&R\r", (float)currentCell.X, (float)currentCell.Y, (float)XRL.Rules.Stat.RandomCosmetic(0, 7), (float)XRL.Rules.Stat.RandomCosmetic(1, 1), 0.015f * (float)XRL.Rules.Stat.RandomCosmetic(8, 12), 0.3f + 0.05f * (float)XRL.Rules.Stat.RandomCosmetic(1, 3), 40, 0L);
					XRLCore.ParticleManager.AddRadial("&r\u000e", (float)currentCell.X, (float)currentCell.Y, (float)XRL.Rules.Stat.RandomCosmetic(0, 7), (float)XRL.Rules.Stat.RandomCosmetic(1, 1), 0.015f * (float)XRL.Rules.Stat.RandomCosmetic(8, 12), 0.3f + 0.05f * (float)XRL.Rules.Stat.RandomCosmetic(1, 3), 40, 0L);
				}
			}
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x001CF2A4 File Offset: 0x001CD4A4
		public bool IsInActiveZone()
		{
			Zone currentZone = this.CurrentZone;
			return currentZone != null && currentZone.IsActive();
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x001CF2C4 File Offset: 0x001CD4C4
		public void ParticleSpray(string Text1, string Text2, string Text3, string Text4, int amount)
		{
			if (this.IsInActiveZone())
			{
				Cell currentCell = this.CurrentCell;
				for (int i = 0; i < 16; i++)
				{
					string text = Text1;
					if (XRL.Rules.Stat.RandomCosmetic(0, 3) == 0)
					{
						text = Text2;
					}
					if (XRL.Rules.Stat.RandomCosmetic(0, 3) == 0)
					{
						text = Text3;
					}
					if (XRL.Rules.Stat.RandomCosmetic(0, 3) == 0)
					{
						text = Text4;
					}
					float num = -0.5f + (float)XRL.Rules.Stat.RandomCosmetic(1, 4) * -0.2f;
					float num2 = -1f + (float)XRL.Rules.Stat.RandomCosmetic(1, 30) * 0.06f;
					XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num, 60, 0f, 0.05f, 0L);
				}
			}
		}

		// Token: 0x06005FA1 RID: 24481 RVA: 0x001CF370 File Offset: 0x001CD570
		public void CrystalSpray()
		{
			if (this.IsInActiveZone())
			{
				Cell currentCell = this.CurrentCell;
				for (int i = 0; i < 4; i++)
				{
					string text = "&m/";
					if (XRL.Rules.Stat.RandomCosmetic(0, 1) == 0)
					{
						text = "&y/";
					}
					if (XRL.Rules.Stat.RandomCosmetic(0, 3) == 0)
					{
						text = "&Y.";
					}
					if (XRL.Rules.Stat.RandomCosmetic(0, 3) == 0)
					{
						text = "&y,";
					}
					if (XRL.Rules.Stat.RandomCosmetic(0, 3) == 0)
					{
						text = "&M<";
					}
					float num = -0.2f + (float)XRL.Rules.Stat.RandomCosmetic(1, 4) * -0.1f;
					float num2 = -1f + (float)XRL.Rules.Stat.RandomCosmetic(1, 30) * 0.06f;
					XRLCore.ParticleManager.Add(text, (float)currentCell.X, (float)currentCell.Y, num2, num, 20, 0f, 0.05f, 0L);
				}
			}
		}

		// Token: 0x06005FA2 RID: 24482 RVA: 0x001CF43C File Offset: 0x001CD63C
		public void SleepytimeParticles()
		{
			Cell currentCell = this.CurrentCell;
			for (int i = 0; i < 16; i++)
			{
				string text = "Z";
				for (int j = 0; j < XRL.Rules.Stat.RandomCosmetic(0, 3); j++)
				{
					text += "Z";
				}
				for (int k = 0; k < XRL.Rules.Stat.RandomCosmetic(0, 4); k++)
				{
					text += "z";
				}
				float num = -0.25f;
				float num2 = 0.05f + (float)XRL.Rules.Stat.RandomCosmetic(1, 4) * 0.01f;
				XRLCore.ParticleManager.Add("&b" + text, (float)currentCell.X, (float)currentCell.Y, num2, num, 120, 0f, -0.05f, (long)(50 * i));
			}
		}

		// Token: 0x06005FA3 RID: 24483 RVA: 0x001CF500 File Offset: 0x001CD700
		public void Heartspray(string Color1 = "&M", string Color2 = "&R", string Color3 = "&r", string Color4 = "&Y", char c = '\u0003')
		{
			this.ParticleSpray(Color1 + c.ToString(), Color4 + ".", Color2 + c.ToString(), Color3 + c.ToString(), 16);
		}

		// Token: 0x06005FA4 RID: 24484 RVA: 0x001CF540 File Offset: 0x001CD740
		public void ParticlePulse(string particle)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					this.ParticleText(particle, 4.9f, 5);
				}
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06005FA5 RID: 24485 RVA: 0x001CF572 File Offset: 0x001CD772
		public bool juiceEnabled
		{
			get
			{
				return Options.UseOverlayCombatEffects;
			}
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x001CF57C File Offset: 0x001CD77C
		public void TileParticleBlip(string Tile, string ColorString, string DetailColor, int Duration = 10, bool IgnoreVisibility = false, bool HFlip = false, bool VFlip = false, long DelayMS = 0L)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.TileParticleBlip(Tile, ColorString, DetailColor, Duration, true, HFlip, VFlip, DelayMS);
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x001CF5B2 File Offset: 0x001CD7B2
		public void ParticleBlip(string Text, int Duration = 10, long DelayMS = 0L, bool IgnoreVisibility = false)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.ParticleBlip(Text, Duration, DelayMS, true);
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x001CF5D5 File Offset: 0x001CD7D5
		public void ParticleText(string Text, float Velocity, int Life)
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.ParticleText(Text, Velocity, Life);
		}

		// Token: 0x06005FA9 RID: 24489 RVA: 0x001CF5EA File Offset: 0x001CD7EA
		public void ParticleText(string Text, bool IgnoreVisibility = false)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.ParticleText(Text, true);
		}

		// Token: 0x06005FAA RID: 24490 RVA: 0x001CF60A File Offset: 0x001CD80A
		public void ParticleText(string Text, float xVel, float yVel, char Color = ' ', bool IgnoreVisibility = false)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.ParticleText(Text, xVel, yVel, Color, true);
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x001CF62F File Offset: 0x001CD82F
		public void ParticleText(string Text, char Color, bool IgnoreVisibility = false, float juiceDuration = 1.5f, float floatLength = -8f)
		{
			if (!IgnoreVisibility && !this.IsVisible())
			{
				return;
			}
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.ParticleText(Text, Color, true, juiceDuration, floatLength, this);
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x001CF658 File Offset: 0x001CD858
		public void GetAdjunctNoun(out string Noun, out bool Post, bool AsIfKnown = false)
		{
			Examiner examiner;
			if (!AsIfKnown && this.TryGetPart<Examiner>(out examiner))
			{
				GameObject activeSample = examiner.GetActiveSample();
				if (activeSample != null)
				{
					activeSample.GetAdjunctNoun(out Noun, out Post, false);
					return;
				}
			}
			Noun = null;
			Post = false;
			string text = this.GetxTag("Grammar", "adjunctNoun", null);
			if (!text.IsNullOrEmpty())
			{
				Noun = text;
				if (this.GetxTag("Grammar", "adjunctNounPost", null) == "true")
				{
					Post = true;
				}
				return;
			}
			if (this.GetPlurality(AsIfKnown) && !this.IsCreature)
			{
				Noun = "set";
			}
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x001CF6E4 File Offset: 0x001CD8E4
		public string GetPluralName(bool AsIfKnown = false, bool NoConfusion = false, bool Stripped = false, bool BaseOnly = false)
		{
			int maxValue = int.MaxValue;
			string text = null;
			string text2 = null;
			bool? flag = new bool?(true);
			return this.GetDisplayName(maxValue, text, text2, AsIfKnown, false, NoConfusion, false, Stripped, false, true, false, false, true, BaseOnly, false, false, null, false, false, false, false, flag, false, null, false, true).Pluralize();
		}

		// Token: 0x06005FAE RID: 24494 RVA: 0x001CF724 File Offset: 0x001CD924
		public string GetDemandName(int DemandCount)
		{
			string text = this.Render.DisplayName.Strip();
			StringBuilder stringBuilder = new StringBuilder();
			string text2;
			bool flag;
			this.GetAdjunctNoun(out text2, out flag, true);
			if (!text2.IsNullOrEmpty() && flag)
			{
				if (DemandCount > 1)
				{
					stringBuilder.Append(Grammar.Cardinal(DemandCount)).Append(' ').Append(text)
						.Append(' ')
						.Append(text2.Pluralize());
				}
				else
				{
					stringBuilder.Append(this.IndefiniteArticle(false, text, false, false)).Append(text).Append(' ')
						.Append(text2);
				}
			}
			else if (!text2.IsNullOrEmpty())
			{
				if (DemandCount > 1)
				{
					stringBuilder.Append(Grammar.Cardinal(DemandCount)).Append(' ').Append(text2.Pluralize());
				}
				else
				{
					stringBuilder.Append(Grammar.A(text2, false));
				}
				stringBuilder.Append(" of ").Append(text);
			}
			else if (this.IsPluralIfKnown)
			{
				if (DemandCount > 1)
				{
					stringBuilder.Append(Grammar.Cardinal(DemandCount)).Append(" sets of ");
				}
				else
				{
					stringBuilder.Append("a set of ");
				}
				stringBuilder.Append(text);
			}
			else if (DemandCount > 1)
			{
				stringBuilder.Append(Grammar.Cardinal(DemandCount)).Append(' ').Append(text.Pluralize());
			}
			else
			{
				stringBuilder.Append(this.IndefiniteArticle(false, text, false, false)).Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x001CF88C File Offset: 0x001CDA8C
		public GameObject GetNearestVisibleObject(bool Hostile = false, string SearchPart = "Physics", int Radius = 80, bool IncludeSolid = true, bool IgnoreLOS = false, Predicate<GameObject> ExtraVisibility = null)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null)
			{
				return null;
			}
			GameObject gameObject = null;
			int num = 9999999;
			List<GameObject> list = ((Radius >= currentCell.ParentZone.Width) ? currentCell.ParentZone.GetObjectsWithPartReadonly(SearchPart) : currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, Radius, SearchPart, this, ExtraVisibility, null));
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject2 = list[i];
				if (gameObject2 != this && gameObject2.IsVisible() && (IgnoreLOS || this.HasLOSTo(gameObject2, IncludeSolid, false, true, true, null)))
				{
					Cell currentCell2 = gameObject2.CurrentCell;
					if (currentCell2 != null)
					{
						int num2 = currentCell2.PathDistanceTo(currentCell);
						if (num2 < num)
						{
							if (Hostile)
							{
								if (gameObject2.IsHostileTowards(this))
								{
									gameObject = gameObject2;
									num = num2;
								}
							}
							else
							{
								gameObject = gameObject2;
								num = num2;
							}
						}
					}
				}
				i++;
			}
			return gameObject;
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x001CF96C File Offset: 0x001CDB6C
		public List<GameObject> GetVisibleCombatObjects()
		{
			Zone currentZone = this.CurrentZone;
			if (currentZone == null)
			{
				return null;
			}
			return currentZone.FastFloodVisibility(this.CurrentCell.X, this.CurrentCell.Y, 9999, "Combat", this, null, null);
		}

		// Token: 0x06005FB1 RID: 24497 RVA: 0x001CF9A4 File Offset: 0x001CDBA4
		public bool InSameCellAs(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			Cell cell = this.CurrentCell;
			Cell cell2 = Object.CurrentCell;
			if (cell == cell2)
			{
				return true;
			}
			if (cell != null && cell2 != null)
			{
				return false;
			}
			if (cell == null)
			{
				cell = this.GetCurrentCell();
			}
			else if (cell2 == null)
			{
				cell2 = Object.GetCurrentCell();
			}
			return cell == cell2;
		}

		// Token: 0x06005FB2 RID: 24498 RVA: 0x001CF9F4 File Offset: 0x001CDBF4
		public bool InAdjacentCellTo(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			Cell cell = this.CurrentCell;
			Cell cell2 = Object.CurrentCell;
			if (cell != null && cell2 != null && cell.IsAdjacentTo(cell2, true))
			{
				return true;
			}
			if (cell != null && cell2 != null)
			{
				return false;
			}
			if (cell == null)
			{
				cell = this.GetCurrentCell();
				if (cell == null)
				{
					return false;
				}
			}
			else if (cell2 == null)
			{
				cell2 = Object.GetCurrentCell();
				if (cell2 == null)
				{
					return false;
				}
			}
			return cell.IsAdjacentTo(cell2, true);
		}

		// Token: 0x06005FB3 RID: 24499 RVA: 0x001CFA5C File Offset: 0x001CDC5C
		public bool InSameOrAdjacentCellTo(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			Cell cell = this.CurrentCell;
			Cell cell2 = Object.CurrentCell;
			if (cell == cell2 && cell != null)
			{
				return true;
			}
			if (cell != null && cell2 != null && cell.IsAdjacentTo(cell2, true))
			{
				return true;
			}
			if (cell != null && cell2 != null)
			{
				return false;
			}
			if (cell == null)
			{
				cell = this.GetCurrentCell();
				if (cell == null)
				{
					return false;
				}
			}
			else if (cell2 == null)
			{
				cell2 = Object.GetCurrentCell();
				if (cell2 == null)
				{
					return false;
				}
			}
			return cell == cell2 || cell.IsAdjacentTo(cell2, true);
		}

		// Token: 0x06005FB4 RID: 24500 RVA: 0x001CFAD4 File Offset: 0x001CDCD4
		public bool IsEngagedInMelee()
		{
			if (this.Brain == null)
			{
				return false;
			}
			GameObject target = this.Brain.Target;
			return target != null && this.DistanceTo(target) <= 1 && target.IsCombatObject();
		}

		// Token: 0x06005FB5 RID: 24501 RVA: 0x001CFB0C File Offset: 0x001CDD0C
		public bool IsRealityDistortionUsable(GameObject Device = null, IPart Mutation = null)
		{
			return CheckRealityDistortionUsabilityEvent.Check(this, null, this, Device, Mutation, new int?(100), 0);
		}

		// Token: 0x06005FB6 RID: 24502 RVA: 0x001CFB20 File Offset: 0x001CDD20
		public bool IsSelfControlledPlayer()
		{
			return this.IsPlayer() && !this.HasStringProperty("Skittishing");
		}

		// Token: 0x06005FB7 RID: 24503 RVA: 0x001CFB3C File Offset: 0x001CDD3C
		public bool IsPlayerLed()
		{
			return this.Brain != null && this.Brain.IsPlayerLed();
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x001CFB53 File Offset: 0x001CDD53
		public bool IsPlayerControlled()
		{
			return this.IsPlayer() || this.IsPlayerLed();
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x001CFB65 File Offset: 0x001CDD65
		public bool IsPlayerLedAndPerceptible()
		{
			return this.IsPlayerLed() && (this.IsVisible() || this.IsAudible(this.ThePlayer, 20));
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x001CFB89 File Offset: 0x001CDD89
		public bool IsPlayerControlledAndPerceptible()
		{
			return this.IsPlayer() || this.IsPlayerLedAndPerceptible();
		}

		// Token: 0x06005FBB RID: 24507 RVA: 0x001CFB9B File Offset: 0x001CDD9B
		public string GetPerceptionVerb()
		{
			if (!this.IsVisible())
			{
				return "hear";
			}
			return "see";
		}

		// Token: 0x06005FBC RID: 24508 RVA: 0x001CFBB0 File Offset: 0x001CDDB0
		public bool IsOverburdened()
		{
			return this.IsPlayerControlled() && this.GetCarriedWeight() > this.GetMaxCarriedWeight();
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x001CFBCA File Offset: 0x001CDDCA
		public bool WouldBeOverburdened(int Weight)
		{
			return this.IsPlayerControlled() && Weight + this.GetCarriedWeight() > this.GetMaxCarriedWeight();
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x001CFBE6 File Offset: 0x001CDDE6
		public bool WouldBeOverburdened(GameObject Object)
		{
			return this.WouldBeOverburdened(Object.Weight);
		}

		// Token: 0x06005FBF RID: 24511 RVA: 0x001CFBF4 File Offset: 0x001CDDF4
		public bool MakeSave(out int SuccessMargin, out int FailureMargin, string Stat, int Difficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool IgnoreNaturals = false, bool IgnoreNatural1 = false, bool IgnoreNatural20 = false, bool IgnoreGodmode = false, GameObject Source = null)
		{
			return XRL.Rules.Stat.MakeSave(out SuccessMargin, out FailureMargin, this, Stat, Difficulty, Attacker, AttackerStat, Vs, IgnoreNaturals, IgnoreNatural1, IgnoreNatural20, IgnoreGodmode, Source);
		}

		// Token: 0x06005FC0 RID: 24512 RVA: 0x001CFC1C File Offset: 0x001CDE1C
		public bool MakeSave(string Stat, int Difficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool IgnoreNaturals = false, bool IgnoreNatural1 = false, bool IgnoreNatural20 = false, bool IgnoreGodmode = false, GameObject Source = null)
		{
			int num;
			int num2;
			return this.MakeSave(out num, out num2, Stat, Difficulty, Attacker, AttackerStat, Vs, IgnoreNaturals, IgnoreNatural1, IgnoreNatural20, IgnoreGodmode, Source);
		}

		// Token: 0x06005FC1 RID: 24513 RVA: 0x001CFC44 File Offset: 0x001CDE44
		public int SaveChance(string Stat, int Difficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool IgnoreNaturals = false, bool IgnoreNatural1 = false, bool IgnoreNatural20 = false, bool IgnoreGodmode = false, GameObject Source = null)
		{
			return XRL.Rules.Stat.SaveChance(this, Stat, Difficulty, Attacker, AttackerStat, Vs, IgnoreNaturals, IgnoreNatural1, IgnoreNatural20, IgnoreGodmode, Source);
		}

		// Token: 0x06005FC2 RID: 24514 RVA: 0x001CFC68 File Offset: 0x001CDE68
		public bool IsCopyOf(GameObject who)
		{
			if (who.IsPlayer() && this.HasStringProperty("PlayerCopy"))
			{
				return true;
			}
			string stringProperty = this.GetStringProperty("FugueCopy", null);
			return stringProperty != null && who.IDMatch(stringProperty);
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x001CFCA8 File Offset: 0x001CDEA8
		public bool HasCopyRelationship(GameObject who)
		{
			if (who == null)
			{
				return false;
			}
			if (who == this)
			{
				return true;
			}
			if (who.IsPlayer() && this.HasStringProperty("PlayerCopy"))
			{
				return true;
			}
			if (this.IsPlayer() && who.HasStringProperty("PlayerCopy"))
			{
				return true;
			}
			string stringProperty = this.GetStringProperty("FugueCopy", null);
			if (stringProperty != null && who.IDMatch(stringProperty))
			{
				return true;
			}
			string stringProperty2 = who.GetStringProperty("FugueCopy", null);
			if (stringProperty2 != null)
			{
				if (stringProperty2 == stringProperty)
				{
					return true;
				}
				if (this.IDMatch(stringProperty2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005FC4 RID: 24516 RVA: 0x001CFD30 File Offset: 0x001CDF30
		public int GetMark()
		{
			string tag = this.GetTag("Mark", null);
			if (tag != null)
			{
				return Convert.ToInt32(tag);
			}
			return 0;
		}

		// Token: 0x06005FC5 RID: 24517 RVA: 0x001CFD58 File Offset: 0x001CDF58
		public bool LeftBehindByPlayer()
		{
			GameObject gameObject = this.ThePlayer;
			for (;;)
			{
				Dominated dominated = ((gameObject != null) ? gameObject.GetEffect<Dominated>() : null);
				if (dominated != null)
				{
					if (dominated.Dominator == this)
					{
						break;
					}
					gameObject = dominated.Dominator;
				}
				if (dominated == null || gameObject == null || gameObject == this.ThePlayer)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005FC6 RID: 24518 RVA: 0x001CFD9E File Offset: 0x001CDF9E
		public bool WasPlayer()
		{
			return this.IsOriginalPlayerBody() || this.LeftBehindByPlayer();
		}

		// Token: 0x06005FC7 RID: 24519 RVA: 0x001CFDB8 File Offset: 0x001CDFB8
		public bool CanChangeMovementMode(string To = null, bool ShowMessage = false, bool Involuntary = false, bool AllowTelekinetic = false, bool FrozenOkay = false)
		{
			if (!FrozenOkay)
			{
				bool flag = false;
				bool flag2 = !ShowMessage;
				if (!this.CheckFrozen(flag, AllowTelekinetic, flag2, null))
				{
					return false;
				}
			}
			return (AllowTelekinetic && this.CanManipulateTelekinetically(this)) || CanChangeMovementModeEvent.Check(this, To, false, ShowMessage, AllowTelekinetic, FrozenOkay);
		}

		// Token: 0x06005FC8 RID: 24520 RVA: 0x001CFDFC File Offset: 0x001CDFFC
		public bool CanChangeBodyPosition(string To = null, bool ShowMessage = false, bool Involuntary = false, bool AllowTelekinetic = false, bool FrozenOkay = false)
		{
			if (!FrozenOkay)
			{
				bool flag = false;
				bool flag2 = !ShowMessage;
				if (!this.CheckFrozen(flag, AllowTelekinetic, flag2, null))
				{
					return false;
				}
			}
			if (AllowTelekinetic && this.CanManipulateTelekinetically(this))
			{
				return true;
			}
			GameObject.eCanChangeBodyPosition.SetParameter("To", To);
			GameObject.eCanChangeBodyPosition.SetFlag("ShowMessage", ShowMessage);
			GameObject.eCanChangeBodyPosition.SetFlag("Involuntary", Involuntary);
			return this.FireEvent(GameObject.eCanChangeBodyPosition);
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x001CFE70 File Offset: 0x001CE070
		public bool CanMoveExtremities(string To = null, bool ShowMessage = false, bool Involuntary = false, bool AllowTelekinetic = false)
		{
			bool flag = false;
			bool flag2 = !ShowMessage;
			if (!this.CheckFrozen(flag, AllowTelekinetic, flag2, null))
			{
				return false;
			}
			if (AllowTelekinetic && this.CanManipulateTelekinetically(this))
			{
				return true;
			}
			GameObject.eCanMoveExtremities.SetParameter("To", To);
			GameObject.eCanMoveExtremities.SetFlag("ShowMessage", ShowMessage);
			GameObject.eCanMoveExtremities.SetFlag("Involuntary", Involuntary);
			return this.FireEvent(GameObject.eCanMoveExtremities);
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x001CFEDE File Offset: 0x001CE0DE
		public void MovementModeChanged(string To = null, bool Involuntary = false)
		{
			MovementModeChangedEvent.Send(this, To, Involuntary);
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x001CFEE8 File Offset: 0x001CE0E8
		public void BodyPositionChanged(string To = null, bool Involuntary = false)
		{
			BodyPositionChangedEvent.Send(this, To, Involuntary);
		}

		// Token: 0x06005FCC RID: 24524 RVA: 0x001CFEF2 File Offset: 0x001CE0F2
		public void ExtremitiesMoved(string To = null, bool Involuntary = false)
		{
			ExtremitiesMovedEvent.Send(this, To, Involuntary);
		}

		// Token: 0x06005FCD RID: 24525 RVA: 0x001CFEFC File Offset: 0x001CE0FC
		public bool WasThrown(GameObject By, GameObject At = null)
		{
			return BeforeAfterThrownEvent.Check(By, this, At) && AfterThrownEvent.Check(By, this, At) && AfterAfterThrownEvent.Check(By, this, At);
		}

		// Token: 0x06005FCE RID: 24526 RVA: 0x001CFF24 File Offset: 0x001CE124
		public bool IsOpenLiquidVolume()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsOpenVolume();
		}

		// Token: 0x06005FCF RID: 24527 RVA: 0x001CFF44 File Offset: 0x001CE144
		public bool IsDangerousOpenLiquidVolume()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsOpenVolume() && liquidVolume.ConsiderLiquidDangerousToContact();
		}

		// Token: 0x06005FD0 RID: 24528 RVA: 0x001CFF70 File Offset: 0x001CE170
		public bool IsSwimmingDepthLiquid()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsSwimmingDepth();
		}

		// Token: 0x06005FD1 RID: 24529 RVA: 0x001CFF90 File Offset: 0x001CE190
		public bool IsWadingDepthLiquid()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsWadingDepth();
		}

		// Token: 0x06005FD2 RID: 24530 RVA: 0x001CFFB0 File Offset: 0x001CE1B0
		public bool IsSwimmableFor(GameObject who)
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.IsSwimmableFor(who);
		}

		// Token: 0x06005FD3 RID: 24531 RVA: 0x001CFFD0 File Offset: 0x001CE1D0
		public bool IsHealingPool()
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return liquidVolume != null && liquidVolume.MaxVolume == -1 && liquidVolume.ContainsSignificantLiquid("convalessence");
		}

		// Token: 0x06005FD4 RID: 24532 RVA: 0x001CFFFF File Offset: 0x001CE1FF
		public bool HasReadyMissileWeapon()
		{
			Body body = this.Body;
			return body != null && body.HasReadyMissileWeapon(null, null);
		}

		// Token: 0x06005FD5 RID: 24533 RVA: 0x001D0014 File Offset: 0x001CE214
		public bool HasMissileWeapon(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			Body body = this.Body;
			return body != null && body.HasMissileWeapon(Filter, PartFilter);
		}

		// Token: 0x06005FD6 RID: 24534 RVA: 0x001D0029 File Offset: 0x001CE229
		public List<GameObject> GetMissileWeapons(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetMissileWeapons(Filter, PartFilter);
		}

		// Token: 0x06005FD7 RID: 24535 RVA: 0x001D003E File Offset: 0x001CE23E
		public bool HasHeavyWeaponEquipped()
		{
			Body body = this.Body;
			return body != null && body.HasHeavyWeaponEquipped();
		}

		// Token: 0x06005FD8 RID: 24536 RVA: 0x001D0051 File Offset: 0x001CE251
		public GameObject GetFirstThrownWeapon(Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstThrownWeapon(Filter, PartFilter);
		}

		// Token: 0x06005FD9 RID: 24537 RVA: 0x001D0066 File Offset: 0x001CE266
		public IList<GameObject> GetThrownWeapons(Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetThrownWeapons(Filter, PartFilter);
		}

		// Token: 0x06005FDA RID: 24538 RVA: 0x001D007B File Offset: 0x001CE27B
		public IList<GameObject> GetThrownWeapons(IList<GameObject> List, Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			Body body = this.Body;
			if (body != null)
			{
				body.GetThrownWeapons(List, Filter, PartFilter);
			}
			return List;
		}

		// Token: 0x06005FDB RID: 24539 RVA: 0x001D0092 File Offset: 0x001CE292
		public BodyPart GetFirstBodyPart(string Type)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type);
		}

		// Token: 0x06005FDC RID: 24540 RVA: 0x001D00A6 File Offset: 0x001CE2A6
		public BodyPart GetFirstBodyPart(string Type, int Laterality)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, Laterality);
		}

		// Token: 0x06005FDD RID: 24541 RVA: 0x001D00BB File Offset: 0x001CE2BB
		public BodyPart GetFirstBodyPart(Predicate<BodyPart> Filter)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Filter);
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x001D00CF File Offset: 0x001CE2CF
		public BodyPart GetFirstBodyPart(string Type, Predicate<BodyPart> Filter)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, Filter);
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x001D00E4 File Offset: 0x001CE2E4
		public BodyPart GetFirstBodyPart(string Type, int Laterality, Predicate<BodyPart> Filter)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, Laterality, Filter);
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x001D00FA File Offset: 0x001CE2FA
		public BodyPart GetFirstBodyPart(string Type, bool EvenIfDismembered)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, EvenIfDismembered);
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x001D010F File Offset: 0x001CE30F
		public BodyPart GetFirstBodyPart(string Type, int Laterality, bool EvenIfDismembered)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, Laterality, EvenIfDismembered);
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x001D0125 File Offset: 0x001CE325
		public BodyPart GetFirstBodyPart(Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Filter, EvenIfDismembered);
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x001D013A File Offset: 0x001CE33A
		public BodyPart GetFirstBodyPart(string Type, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, Filter, EvenIfDismembered);
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x001D0150 File Offset: 0x001CE350
		public BodyPart GetFirstBodyPart(string Type, int Laterality, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetFirstPart(Type, Laterality, Filter, EvenIfDismembered);
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x001D0168 File Offset: 0x001CE368
		public bool HasBodyPart(string Type)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type);
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x001D017C File Offset: 0x001CE37C
		public bool HasBodyPart(string Type, int Laterality)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type, Laterality);
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x001D0191 File Offset: 0x001CE391
		public bool HasBodyPart(Predicate<BodyPart> Filter)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Filter);
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x001D01A5 File Offset: 0x001CE3A5
		public bool HasBodyPart(string Type, Predicate<BodyPart> Filter)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type, Filter);
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x001D01BA File Offset: 0x001CE3BA
		public bool HasBodyPart(string Type, bool EvenIfDismembered)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type, EvenIfDismembered);
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x001D01CF File Offset: 0x001CE3CF
		public bool HasBodyPart(string Type, int Laterality, bool EvenIfDismembered)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type, Laterality, EvenIfDismembered);
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x001D01E5 File Offset: 0x001CE3E5
		public bool HasBodyPart(Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Filter, EvenIfDismembered);
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x001D01FA File Offset: 0x001CE3FA
		public bool HasBodyPart(string Type, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type, Filter, EvenIfDismembered);
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x001D0210 File Offset: 0x001CE410
		public bool HasBodyPart(string Type, int Laterality, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			Body body = this.Body;
			return body != null && body.HasPart(Type, Laterality, Filter, EvenIfDismembered);
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x001D0228 File Offset: 0x001CE428
		public bool CheckInfluence(ref string FailureMessage, string Type = null, GameObject By = null, bool Silent = false)
		{
			if (By == null)
			{
				By = this.ThePlayer;
				if (By == null)
				{
					return false;
				}
			}
			Event @event = Event.New("CanBeInfluenced", 0, 0, 0);
			@event.SetParameter("Subject", this);
			@event.SetParameter("Type", Type);
			@event.SetParameter("By", By);
			@event.SetParameter("Message", FailureMessage);
			bool flag = this.FireEvent(@event);
			FailureMessage = @event.GetStringParameter("Message", null);
			return flag;
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x001D02A0 File Offset: 0x001CE4A0
		public bool CheckInfluence(string Type = null, GameObject By = null, bool Silent = false)
		{
			string text = null;
			bool flag = this.CheckInfluence(ref text, Type, By, Silent);
			if (!flag && GameObject.Validate(ref By) && By.IsPlayer())
			{
				if (text.IsNullOrEmpty())
				{
					By.Fail("Nothing happens.");
					return flag;
				}
				By.Fail(GameText.VariableReplace(text, this, null, true));
			}
			return flag;
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x001D02F4 File Offset: 0x001CE4F4
		public bool IsAudible(GameObject By, int Volume = 20)
		{
			if (By == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.IsAudible(By, Volume);
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x001D031C File Offset: 0x001CE51C
		public bool IsSmellable(GameObject By)
		{
			if (By == null)
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			return currentCell != null && currentCell.IsSmellable(By, this.GetIntProperty("SmellIntensity", 5));
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x001D0350 File Offset: 0x001CE550
		public Cell GetDropCell()
		{
			Cell cell = this.GetCurrentCell();
			if (cell != null && cell.IsSolidOtherThan(this))
			{
				bool flag = false;
				int num = int.MaxValue;
				foreach (Cell cell2 in cell.GetAdjacentCells(true))
				{
					if (!cell2.IsSolid())
					{
						if (flag)
						{
							int num2 = XRL.The.Player.DistanceTo(cell2);
							if (num2 < num)
							{
								cell = cell2;
								num = num2;
							}
						}
						else
						{
							flag = true;
							cell = cell2;
							if (XRL.The.Player == null)
							{
								break;
							}
							num = XRL.The.Player.DistanceTo(cell2);
						}
					}
				}
			}
			return cell;
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x001D0400 File Offset: 0x001CE600
		public IInventory GetDropInventory()
		{
			GameObject inInventory = this.InInventory;
			if (inInventory != null)
			{
				Inventory inventory = inInventory.Inventory;
				if (inventory != null)
				{
					return inventory;
				}
			}
			if (this._Effects != null)
			{
				int i = 0;
				int count = this._Effects.Count;
				while (i < count)
				{
					Engulfed engulfed = this._Effects[i] as Engulfed;
					if (engulfed != null && engulfed.IsEngulfedByValid())
					{
						Inventory inventory2 = engulfed.EngulfedBy.Inventory;
						if (inventory2 != null)
						{
							return inventory2;
						}
					}
					i++;
				}
			}
			return this.GetDropCell();
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x001D047E File Offset: 0x001CE67E
		public bool CanBeTargetedByPlayer()
		{
			return !this.IsPlayerControlled() && this.HasStat("Hitpoints") && this.CurrentCell != null;
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x001D04A4 File Offset: 0x001CE6A4
		public int AwardXP(int Amount, int Tier = -1, int Minimum = 0, int Maximum = 2147483647, GameObject Kill = null, GameObject InfluencedBy = null, GameObject PassedUpFrom = null, GameObject PassedDownFrom = null, string ZoneID = null, string Deed = null)
		{
			return AwardXPEvent.Send(this, Amount, Tier, Minimum, Maximum, Kill, InfluencedBy, PassedUpFrom, PassedDownFrom, ZoneID, Deed);
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x001D04C8 File Offset: 0x001CE6C8
		public int AwardXPTo(GameObject Subject, bool ForKill = true, string Deed = null, bool MockAward = false)
		{
			int num = 0;
			Statistic statistic;
			if (!this.HasTagOrProperty("NoXP") && this.Statistics.TryGetValue("XPValue", out statistic))
			{
				int intProperty = this.GetIntProperty("*XPValue", statistic.Value);
				if (intProperty > 0)
				{
					int num2 = -1;
					Statistic statistic2;
					if (this.Statistics.TryGetValue("Level", out statistic2))
					{
						num2 = statistic2.Value / 5;
					}
					if (MockAward)
					{
						num = intProperty;
					}
					else
					{
						num = Subject.AwardXP(intProperty, num2, 0, int.MaxValue, ForKill ? this : null, ForKill ? null : this, null, null, null, Deed);
					}
				}
				this.Statistics.Remove("XPValue");
			}
			return num;
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x001D056C File Offset: 0x001CE76C
		public void StopFighting(bool Involuntary = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.StopFighting(Involuntary);
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x001D057F File Offset: 0x001CE77F
		public void StopFighting(GameObject Object, bool Involuntary = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.StopFighting(Object, Involuntary);
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x001D0593 File Offset: 0x001CE793
		public void StopFight(bool Involuntary = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.StopFight(Involuntary);
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x001D05A6 File Offset: 0x001CE7A6
		public void StopFight(GameObject Object, bool Involuntary = false, bool Reciprocal = false)
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.StopFight(Object, Involuntary, Reciprocal);
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x001D05BB File Offset: 0x001CE7BB
		public bool GetAngryAt(GameObject who, int Amount = -50)
		{
			Brain brain = this.Brain;
			return brain != null && brain.GetAngryAt(who, Amount);
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x001D05D0 File Offset: 0x001CE7D0
		public void AddOpinion<T>(GameObject Subject, float Magnitude = 1f) where T : IOpinionSubject, new()
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.AddOpinion<T>(Subject, Magnitude);
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x001D05E4 File Offset: 0x001CE7E4
		public void AddOpinion<T>(GameObject Subject, GameObject Object, float Magnitude = 1f) where T : IOpinionObject, new()
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return;
			}
			brain.AddOpinion<T>(Subject, Object, Magnitude);
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x001D05F9 File Offset: 0x001CE7F9
		public bool LikeBetter(GameObject who, int Amount = 50)
		{
			Brain brain = this.Brain;
			return brain != null && brain.LikeBetter(who, Amount);
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x001D060E File Offset: 0x001CE80E
		public string GetWaterRitualLiquid(GameObject Actor = null)
		{
			return GetWaterRitualLiquidEvent.GetFor(Actor ?? this.ThePlayer, this);
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x001D0621 File Offset: 0x001CE821
		public string GetWaterRitualLiquidName(GameObject Actor = null)
		{
			return LiquidVolume.GetLiquid(this.GetWaterRitualLiquid(Actor)).GetName();
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x001D063C File Offset: 0x001CE83C
		public string GetMythicDomain(int Range = 5, global::System.Random Random = null)
		{
			GetItemElementsEvent mythicFor = GetItemElementsEvent.GetMythicFor(this, Range, Random);
			string text = mythicFor.Bag.PeekOne();
			PooledEvent<GetItemElementsEvent>.ResetTo(ref mythicFor);
			return text ?? "might";
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x001D0670 File Offset: 0x001CE870
		public int ResistMentalIntrusion(string Type, int Magnitude, GameObject Attacker)
		{
			Event @event = Event.New("ResistMentalIntrusion", 0, 0, 0);
			@event.SetParameter("Type", Type);
			@event.SetParameter("Magnitude", Magnitude);
			@event.SetParameter("Attacker", Attacker);
			@event.SetParameter("Defender", this);
			this.FireEvent(@event);
			return @event.GetIntParameter("Magnitude", 0);
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x001D06D3 File Offset: 0x001CE8D3
		public void FlushNavigationCaches()
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.FlushNavigationCache();
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x001D06E5 File Offset: 0x001CE8E5
		public void FlushContextWeightCaches()
		{
			GameObject gameObject = this.InInventory ?? this.Equipped;
			if (gameObject == null)
			{
				return;
			}
			gameObject.FlushWeightCaches();
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x001D0701 File Offset: 0x001CE901
		public void FlushCarriedWeightCache()
		{
			this.CarriedWeightCache = -1;
			this.MaxCarriedWeightCache = -1;
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x001D0711 File Offset: 0x001CE911
		public void FlushWeightCaches()
		{
			this.FlushCarriedWeightCache();
			FlushWeightCacheEvent.Send(this);
		}

		// Token: 0x06006007 RID: 24583 RVA: 0x001D0720 File Offset: 0x001CE920
		public void TurnTick(long TimeTick, int Amount)
		{
			if (this.RegisteredEvents == null)
			{
				return;
			}
			EventRegistry.List list;
			if (!this.RegisteredEvents.TryGetValue(IComponent<GameObject>.TurnTickID, out list))
			{
				return;
			}
			EventRegistry.List.Entry[] array = list.GetArray();
			int i = 0;
			int num = list.Count;
			while (i < num)
			{
				IEventHandler handler = array[i].Handler;
				handler.TurnTick(TimeTick, Amount);
				if (num != list.Count)
				{
					num = list.Count;
					if (i < num && array[i].Handler != handler)
					{
						i--;
					}
				}
				i++;
			}
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x001D07A4 File Offset: 0x001CE9A4
		public GameObject AddAsActiveObject()
		{
			ActionManager actionManager = XRL.The.ActionManager;
			if (actionManager != null)
			{
				actionManager.AddActiveObject(this);
			}
			return this;
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06006009 RID: 24585 RVA: 0x001D07B8 File Offset: 0x001CE9B8
		public ActivatedAbilities ActivatedAbilities
		{
			get
			{
				return this.Abilities;
			}
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x001D07C0 File Offset: 0x001CE9C0
		public ActivatedAbilityEntry GetActivatedAbilityByCommand(string Command)
		{
			ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
			if (activatedAbilities == null)
			{
				return null;
			}
			return activatedAbilities.GetAbilityByCommand(Command);
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x001D07D4 File Offset: 0x001CE9D4
		public ActivatedAbilityEntry GetActivatedAbility(Guid ID)
		{
			ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
			if (activatedAbilities == null)
			{
				return null;
			}
			if (activatedAbilities.AbilityByGuid == null)
			{
				return null;
			}
			ActivatedAbilityEntry activatedAbilityEntry;
			if (activatedAbilities.AbilityByGuid.TryGetValue(ID, out activatedAbilityEntry))
			{
				return activatedAbilityEntry;
			}
			return null;
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x001D080C File Offset: 0x001CEA0C
		public bool RemoveActivatedAbility(ref Guid ID)
		{
			bool flag = false;
			if (ID != Guid.Empty)
			{
				ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
				if (activatedAbilities != null)
				{
					flag = activatedAbilities.RemoveAbility(ID);
				}
				ID = Guid.Empty;
			}
			return flag;
		}

		// Token: 0x0600600D RID: 24589 RVA: 0x001D0850 File Offset: 0x001CEA50
		public bool RemoveActivatedAbilityByCommand(string Command)
		{
			if (!Command.IsNullOrEmpty())
			{
				ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
				if (activatedAbilities != null && activatedAbilities.RemoveAbilityByCommand(Command))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600600E RID: 24590 RVA: 0x001D087C File Offset: 0x001CEA7C
		public bool EnableActivatedAbility(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				activatedAbility.Enabled = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600600F RID: 24591 RVA: 0x001D08A0 File Offset: 0x001CEAA0
		public bool DisableActivatedAbility(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				activatedAbility.Enabled = false;
				return true;
			}
			return false;
		}

		// Token: 0x06006010 RID: 24592 RVA: 0x001D08C4 File Offset: 0x001CEAC4
		public bool ToggleActivatedAbility(Guid ID, bool Silent = false, bool? SetState = null)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				bool flag = SetState ?? (!activatedAbility.ToggleState);
				if (!Silent && this.IsPlayer() && flag != activatedAbility.ToggleState)
				{
					string text = (flag ? "on" : "off");
					SoundManager.PlayUISound("ui_checkbox_toggle", 0.5f, false, false, SoundRequest.SoundEffectType.None);
					MessageQueue.AddPlayerMessage(string.Concat(new string[]
					{
						"You toggle ",
						Markup.Color("c", activatedAbility.DisplayName),
						" ",
						text,
						"."
					}), null, true);
				}
				activatedAbility.ToggleState = flag;
				return true;
			}
			return false;
		}

		// Token: 0x06006011 RID: 24593 RVA: 0x001D0980 File Offset: 0x001CEB80
		public bool IsActivatedAbilityToggledOn(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if ((activatedAbility == null || !activatedAbility.ActiveToggle) && (activatedAbility == null || !activatedAbility.Toggleable))
			{
				return false;
			}
			ActivatedAbilityEntry activatedAbility2 = this.GetActivatedAbility(ID);
			return activatedAbility2 != null && activatedAbility2.ToggleState;
		}

		// Token: 0x06006012 RID: 24594 RVA: 0x001D09BF File Offset: 0x001CEBBF
		public int GetActivatedAbilityCooldown(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility == null)
			{
				return 0;
			}
			return activatedAbility.Cooldown;
		}

		// Token: 0x06006013 RID: 24595 RVA: 0x001D09D3 File Offset: 0x001CEBD3
		public bool IsActivatedAbilityCoolingDown(Guid ID)
		{
			return this.GetActivatedAbilityCooldown(ID) > 0;
		}

		// Token: 0x06006014 RID: 24596 RVA: 0x001D09DF File Offset: 0x001CEBDF
		public int GetActivatedAbilityCooldownRounds(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility == null)
			{
				return 0;
			}
			return activatedAbility.CooldownRounds;
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x001D09F3 File Offset: 0x001CEBF3
		public string GetActivatedAbilityCooldownDescription(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			return ((activatedAbility != null) ? activatedAbility.Description : null) ?? "";
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x001D0A14 File Offset: 0x001CEC14
		public bool CooldownActivatedAbility(Guid ID, int Turns, string Tags = null, bool Involuntary = false)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			Turns = this.GetCooldownTurns(activatedAbility, Turns, Tags, Involuntary);
			if (Turns <= 0)
			{
				return true;
			}
			if (activatedAbility != null)
			{
				activatedAbility.SetScaledCooldown(Turns * 10);
				return true;
			}
			return false;
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x001D0A4C File Offset: 0x001CEC4C
		public int GetCooldownTurns(Guid ActivatedAbilityID, int Turns, string Tags = null, bool Involuntary = false)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ActivatedAbilityID);
			if (activatedAbility != null)
			{
				return this.GetCooldownTurns(activatedAbility, Turns, Tags, Involuntary);
			}
			return -1;
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x001D0A74 File Offset: 0x001CEC74
		public int GetCooldownTurns(ActivatedAbilityEntry ability, int Turns, string tags = null, bool Involuntary = false)
		{
			Event @event = Event.New("BeforeCooldownActivatedAbility", 0, 0, 0);
			@event.SetParameter("AbilityEntry", ability);
			@event.SetParameter("Turns", Turns);
			@event.SetParameter("Tags", tags);
			@event.SetFlag("Involuntary", Involuntary);
			if (!this.FireEvent(@event))
			{
				return -1;
			}
			return @event.GetIntParameter("Turns", 0);
		}

		// Token: 0x06006019 RID: 24601 RVA: 0x001D0ADC File Offset: 0x001CECDC
		public bool TakeActivatedAbilityOffCooldown(Guid ID)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				activatedAbility.Cooldown = 0;
				return true;
			}
			return false;
		}

		// Token: 0x0600601A RID: 24602 RVA: 0x001D0AFE File Offset: 0x001CECFE
		public bool IsActivatedAbilityUsable(ActivatedAbilityEntry Ability)
		{
			return Ability != null && Ability.IsUsable;
		}

		// Token: 0x0600601B RID: 24603 RVA: 0x001D0B0B File Offset: 0x001CED0B
		public bool IsActivatedAbilityUsable(Guid ID)
		{
			return this.IsActivatedAbilityUsable(this.GetActivatedAbility(ID));
		}

		// Token: 0x0600601C RID: 24604 RVA: 0x001D0B1A File Offset: 0x001CED1A
		public bool IsActivatedAbilityUsable(string Command)
		{
			return this.IsActivatedAbilityUsable(this.GetActivatedAbilityByCommand(Command));
		}

		// Token: 0x0600601D RID: 24605 RVA: 0x001D0B29 File Offset: 0x001CED29
		public bool IsActivatedAbilityAIUsable(ActivatedAbilityEntry Ability)
		{
			return Ability != null && Ability.IsAIUsable;
		}

		// Token: 0x0600601E RID: 24606 RVA: 0x001D0B36 File Offset: 0x001CED36
		public bool IsActivatedAbilityAIUsable(Guid ID)
		{
			return this.IsActivatedAbilityAIUsable(this.GetActivatedAbility(ID));
		}

		// Token: 0x0600601F RID: 24607 RVA: 0x001D0B45 File Offset: 0x001CED45
		public bool IsActivatedAbilityAIUsable(string Command)
		{
			return this.IsActivatedAbilityAIUsable(this.GetActivatedAbilityByCommand(Command));
		}

		// Token: 0x06006020 RID: 24608 RVA: 0x001D0B54 File Offset: 0x001CED54
		public bool IsActivatedAbilityAIDisabled(ActivatedAbilityEntry Ability)
		{
			return Ability != null && Ability.AIDisable;
		}

		// Token: 0x06006021 RID: 24609 RVA: 0x001D0B61 File Offset: 0x001CED61
		public bool IsActivatedAbilityAIDisabled(Guid ID)
		{
			return this.IsActivatedAbilityAIDisabled(this.GetActivatedAbility(ID));
		}

		// Token: 0x06006022 RID: 24610 RVA: 0x001D0B70 File Offset: 0x001CED70
		public bool IsActivatedAbilityAIDisabled(string Command)
		{
			return this.IsActivatedAbilityAIDisabled(this.GetActivatedAbilityByCommand(Command));
		}

		// Token: 0x06006023 RID: 24611 RVA: 0x001D0B7F File Offset: 0x001CED7F
		public bool IsActivatedAbilityVoluntarilyUsable(ActivatedAbilityEntry Ability)
		{
			if (!this.IsPlayer())
			{
				return this.IsActivatedAbilityAIUsable(Ability);
			}
			return this.IsActivatedAbilityUsable(Ability);
		}

		// Token: 0x06006024 RID: 24612 RVA: 0x001D0B98 File Offset: 0x001CED98
		public bool IsActivatedAbilityVoluntarilyUsable(Guid ID)
		{
			if (!this.IsPlayer())
			{
				return this.IsActivatedAbilityAIUsable(ID);
			}
			return this.IsActivatedAbilityUsable(ID);
		}

		// Token: 0x06006025 RID: 24613 RVA: 0x001D0BB1 File Offset: 0x001CEDB1
		public bool IsActivatedAbilityVoluntarilyUsable(string Command)
		{
			if (!this.IsPlayer())
			{
				return this.IsActivatedAbilityAIUsable(Command);
			}
			return this.IsActivatedAbilityUsable(Command);
		}

		// Token: 0x06006026 RID: 24614 RVA: 0x001D0BCC File Offset: 0x001CEDCC
		public bool DescribeActivatedAbility(Guid ID, Action<Templates.StatCollector> statCollector)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				Templates.Template template;
				if (Templates.TemplateByID.TryGetValue("ActivatedAbility." + activatedAbility.CommandForDescription, out template))
				{
					activatedAbility.Description = template.Build(statCollector, "ability " + activatedAbility.CommandForDescription);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06006027 RID: 24615 RVA: 0x001D0C24 File Offset: 0x001CEE24
		public bool DescribeActivatedAbility(Guid ID, Templates.StatCollector values)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				Templates.Template template;
				if (Templates.TemplateByID.TryGetValue("ActivatedAbility." + activatedAbility.CommandForDescription, out template))
				{
					activatedAbility.Description = template.Build(values);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06006028 RID: 24616 RVA: 0x001D0C6C File Offset: 0x001CEE6C
		public bool DescribeActivatedAbility(Guid ID, string description)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				activatedAbility.Description = description;
				return true;
			}
			return false;
		}

		// Token: 0x06006029 RID: 24617 RVA: 0x001D0C90 File Offset: 0x001CEE90
		public bool SetActivatedAbilityDisplayName(Guid ID, string DisplayName)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				activatedAbility.DisplayName = DisplayName;
				return true;
			}
			return false;
		}

		// Token: 0x0600602A RID: 24618 RVA: 0x001D0CB4 File Offset: 0x001CEEB4
		public bool SetActivatedAbilityDisabledMessage(Guid ID, string DisabledMessage)
		{
			ActivatedAbilityEntry activatedAbility = this.GetActivatedAbility(ID);
			if (activatedAbility != null)
			{
				activatedAbility.DisabledMessage = DisabledMessage;
				return true;
			}
			return false;
		}

		// Token: 0x0600602B RID: 24619 RVA: 0x001D0CD8 File Offset: 0x001CEED8
		public Guid AddDynamicCommand(out string Command, string CommandForDescription, string Name, string Class, string Description = null, string Icon = "\a", string DisabledMessage = null, bool Toggleable = false, bool DefaultToggleState = false, bool ActiveToggle = false, bool IsAttack = false, bool IsRealityDistortionBased = false, bool IsWorldMapUsable = false, bool Silent = false, bool AIDisable = false, bool AlwaysAllowToggleOff = true, bool AffectedByWillpower = true, bool TickPerTurn = false, bool Distinct = false, int Cooldown = -1, Renderable UITileDefault = null, Renderable UITileToggleOn = null, Renderable UITileDisabled = null, Renderable UITileCoolingDown = null)
		{
			Command = null;
			ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
			if (activatedAbilities == null)
			{
				return Guid.Empty;
			}
			Command = activatedAbilities.GetNextAvailableCommandString(CommandForDescription);
			return this.AddActivatedAbility(Name, Command, Class, Description, Icon, DisabledMessage, Toggleable, DefaultToggleState, ActiveToggle, IsAttack, IsRealityDistortionBased, IsWorldMapUsable, Silent, AIDisable, AlwaysAllowToggleOff, AffectedByWillpower, TickPerTurn, Distinct, Cooldown, CommandForDescription, UITileDefault, UITileToggleOn, UITileDisabled, UITileCoolingDown);
		}

		// Token: 0x0600602C RID: 24620 RVA: 0x001D0D38 File Offset: 0x001CEF38
		public Guid AddActivatedAbility(string Name, string Command, string Class, string Description = null, string Icon = "\a", string DisabledMessage = null, bool Toggleable = false, bool DefaultToggleState = false, bool ActiveToggle = false, bool IsAttack = false, bool IsRealityDistortionBased = false, bool IsWorldMapUsable = false, bool Silent = false, bool AIDisable = false, bool AlwaysAllowToggleOff = true, bool AffectedByWillpower = true, bool TickPerTurn = false, bool Distinct = false, int Cooldown = -1, string CommandForDescription = null, Renderable UITileDefault = null, Renderable UITileToggleOn = null, Renderable UITileDisabled = null, Renderable UITileCoolingDown = null)
		{
			ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
			if (activatedAbilities == null)
			{
				return Guid.Empty;
			}
			return activatedAbilities.AddAbility(Name, Command, Class, Description, Icon, DisabledMessage, Toggleable, DefaultToggleState, ActiveToggle, IsAttack, IsRealityDistortionBased, IsWorldMapUsable, Silent, AIDisable, AlwaysAllowToggleOff, AffectedByWillpower, TickPerTurn, Distinct, Cooldown, CommandForDescription, UITileDefault, UITileToggleOn, UITileDisabled, UITileCoolingDown);
		}

		// Token: 0x0600602D RID: 24621 RVA: 0x001D0D88 File Offset: 0x001CEF88
		public int GetHighestActivatedAbilityCooldown()
		{
			int num = -1;
			ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
			if (activatedAbilities != null && activatedAbilities.AbilityByGuid != null)
			{
				foreach (ActivatedAbilityEntry activatedAbilityEntry in activatedAbilities.AbilityByGuid.Values)
				{
					int cooldown = activatedAbilityEntry.Cooldown;
					if (cooldown > num)
					{
						num = cooldown;
					}
				}
			}
			return num;
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x001D0DFC File Offset: 0x001CEFFC
		public int GetHighestActivatedAbilityCooldownRounds()
		{
			int num = -1;
			ActivatedAbilities activatedAbilities = this.ActivatedAbilities;
			if (activatedAbilities != null && activatedAbilities.AbilityByGuid != null)
			{
				foreach (ActivatedAbilityEntry activatedAbilityEntry in activatedAbilities.AbilityByGuid.Values)
				{
					int cooldownRounds = activatedAbilityEntry.CooldownRounds;
					if (cooldownRounds > num)
					{
						num = cooldownRounds;
					}
				}
			}
			return num;
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x001D0E70 File Offset: 0x001CF070
		public bool Seen()
		{
			this.GetBlueprint(true).Seen();
			if ((this.TransientCache & 1) == 0)
			{
				this.TransientCache |= 1;
				Factions.RegisterWorshippable(this);
			}
			return true;
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x001D0E9F File Offset: 0x001CF09F
		public bool BlueprintSeen()
		{
			return this.GetBlueprint(true).HasBeenSeen();
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x001D0EB0 File Offset: 0x001CF0B0
		public string GetSpecies()
		{
			string text;
			if ((text = this.GetPropertyOrTag("Species", null)) == null)
			{
				text = this.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			}
			return text;
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x001D0EF9 File Offset: 0x001CF0F9
		public string GetApparentSpecies(GameObject Viewer = null)
		{
			return GetApparentSpeciesEvent.GetFor(this, Viewer, this.GetSpecies());
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x001D0F08 File Offset: 0x001CF108
		public string GetClass()
		{
			return this.GetPropertyOrTag("Class", null) ?? this.Blueprint;
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x001D0F20 File Offset: 0x001CF120
		public string GetCulture()
		{
			return this.GetPropertyOrTag("Culture", null) ?? this.GetSpecies();
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x001D0F38 File Offset: 0x001CF138
		public bool WantEvent(int ID, int Cascade)
		{
			if (MinEvent.CascadeTo(Cascade, 256))
			{
				return true;
			}
			if (this.RegisteredEvents != null && this.RegisteredEvents.ContainsKey(ID))
			{
				return true;
			}
			if (MinEvent.CascadeTo(Cascade, 64))
			{
				return false;
			}
			IPart[] array = this.PartsList.GetArray();
			int i = 0;
			int partsCascade = this.PartsCascade;
			while (i < partsCascade)
			{
				if (array[i].WantEvent(ID, Cascade))
				{
					return true;
				}
				i++;
			}
			if (this._Effects != null && this._Effects.Count > 0)
			{
				Effect[] array2 = this._Effects.GetArray();
				int j = 0;
				int count = this._Effects.Count;
				while (j < count)
				{
					if (array2[j].WantEvent(ID, Cascade))
					{
						return true;
					}
					j++;
				}
			}
			return false;
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x001D0FF5 File Offset: 0x001CF1F5
		public bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			return this.HandleOwnerGetInventoryActionsEvent(E) && this.HandleEventInner(E);
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x001D1009 File Offset: 0x001CF209
		public bool HandleEvent(EnteredCellEvent E)
		{
			return this.HandleEnteredCellEvent(E) && this.HandleEventInner(E);
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x001D101D File Offset: 0x001CF21D
		public bool HandleEvent(InventoryActionEvent E)
		{
			return this.HandleInventoryActionEvent(E) && this.HandleEventInner(E);
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x001D1031 File Offset: 0x001CF231
		public bool HandleEvent(ContainsAnyBlueprintEvent E)
		{
			if (E.Blueprints.Contains(this.Blueprint) && E.Container != this)
			{
				E.Object = this;
				return false;
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x001D105F File Offset: 0x001CF25F
		public bool HandleEvent(ContainsEvent E)
		{
			return (E.Object != this || E.Container == this) && this.HandleEventInner(E);
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x001D107C File Offset: 0x001CF27C
		public bool HandleEvent(ContainsBlueprintEvent E)
		{
			if (E.Blueprint == this.Blueprint && E.Container != this)
			{
				E.Object = this;
				return false;
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x001D10AA File Offset: 0x001CF2AA
		public bool HandleEvent(FindObjectByIdEvent E)
		{
			if (this.IDMatch(E.FindID) || this.IDMatch(E.FindBaseID))
			{
				E.Object = this;
				return false;
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x001D10D8 File Offset: 0x001CF2D8
		public bool HandleEvent(MakeTemporaryEvent E)
		{
			return this.HandleMakeTemporaryEvent(E) && this.HandleEventInner(E);
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x001D10EC File Offset: 0x001CF2EC
		public bool HandleEvent(GlimmerChangeEvent E)
		{
			if (this.IsPlayer())
			{
				PsychicGlimmer.Update(this);
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x001D1103 File Offset: 0x001CF303
		public bool HandleEvent(CommandReplaceCellEvent E)
		{
			if (this.IsPlayer() && E.Actor == this)
			{
				GameObject.PerformReplaceCell(this);
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x001D1124 File Offset: 0x001CF324
		public bool HandleEvent(GetPointsOfInterestEvent E)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell != null && currentCell.Explored && ((this.IsCreature && this.HasProperName && !this.IsPlayerControlled()) || this.IsMarkedImportantByPlayer()) && E.StandardChecks(null, null, this))
			{
				E.Add(this, this.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, this.CurrentCell.Location, null, -1, null, 0);
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x001D11A8 File Offset: 0x001CF3A8
		public bool HandleEvent(IsRepairableEvent E)
		{
			return ((this.HasTag("Creature") && this.IsOrganic) || !this.isDamaged()) && this.HandleEventInner(E);
		}

		// Token: 0x06006042 RID: 24642 RVA: 0x001D11D0 File Offset: 0x001CF3D0
		public bool HandleEvent(RepairedEvent E)
		{
			if (!this.HasTag("Creature") || !this.IsOrganic)
			{
				this.Heal(500, false, true, false);
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x06006043 RID: 24643 RVA: 0x001D1200 File Offset: 0x001CF400
		public bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			if (this.IsPlayer())
			{
				E.Add("Move to Square", "CmdMoveTo", 1000, null, false);
				E.Add("Move to Zone Edge", "CmdMoveToEdge", 2000, null, false);
				E.Add("Move to Point of Interest".Color(GetPointsOfInterestEvent.AnyFor(this, null) ? null : "K"), "CmdMoveToPointOfInterest", 3000, null, false);
				E.Add("Move Direction Until Stopped", "CmdWalk", 4000, null, false);
				E.Add("Move Direction One Square", "CmdMoveDirection", 5000, null, false);
				E.Add("Attack Direction", "CmdAttackDirection", 6000, null, true);
			}
			return this.HandleEventInner(E);
		}

		// Token: 0x06006044 RID: 24644 RVA: 0x001D12C0 File Offset: 0x001CF4C0
		public bool HandleEvent(EndTurnEvent E)
		{
			EventRegistry registeredEvents = this.RegisteredEvents;
			bool flag = registeredEvents == null || registeredEvents.Dispatch(E);
			if (flag)
			{
				flag = this.FireRegisteredEvent(EndTurnEvent.registeredInstance);
			}
			this.CleanEffects();
			return flag;
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x001D12F8 File Offset: 0x001CF4F8
		public bool HandleEvent(AutoexploreObjectEvent E)
		{
			bool flag = this.HandleEventInner(E);
			if (E.Actor == this)
			{
				if (E.Command == null && E.Item.ShouldAutoget())
				{
					E.Command = "Autoget";
				}
				if (E.Command == "Autoget" && !E.Item.CanAutoget(true))
				{
					E.Command = null;
					E.AllowRetry = false;
				}
			}
			return flag;
		}

		// Token: 0x06006046 RID: 24646 RVA: 0x001D1363 File Offset: 0x001CF563
		public bool HandleEvent(MinEvent E)
		{
			return this.HandleEventInner(E);
		}

		// Token: 0x06006047 RID: 24647 RVA: 0x001D136C File Offset: 0x001CF56C
		public bool HandleEvent<T>(T E, IEvent ParentEvent) where T : MinEvent
		{
			bool flag = this.HandleEvent(E);
			if (ParentEvent != null)
			{
				ParentEvent.ProcessChildEvent(E);
			}
			return flag;
		}

		// Token: 0x06006048 RID: 24648 RVA: 0x001D138C File Offset: 0x001CF58C
		private bool HandleEventInner(MinEvent E)
		{
			int cascadeLevel = E.GetCascadeLevel();
			if (MinEvent.CascadeTo(cascadeLevel, 64))
			{
				EventRegistry registeredEvents = this.RegisteredEvents;
				return registeredEvents == null || registeredEvents.Dispatch(E);
			}
			int id = E.ID;
			EventRegistry registeredEvents2 = this.RegisteredEvents;
			EventRegistry.List list = ((registeredEvents2 != null) ? registeredEvents2[id] : null);
			if (list != null && !list.DispatchRange(E, -2147483648, -1))
			{
				return false;
			}
			IPart[] array = this.PartsList.GetArray();
			int i = 0;
			int num = this.PartsCascade;
			while (i < num)
			{
				IPart part = array[i];
				if (part.WantEvent(id, cascadeLevel))
				{
					if (!E.Dispatch(part))
					{
						return false;
					}
					if (!part.HandleEvent(E))
					{
						return false;
					}
					if (num != this.PartsCascade)
					{
						num = this.PartsCascade;
						array = this.PartsList.GetArray();
						if (i < num && array[i] != part)
						{
							i--;
						}
					}
				}
				i++;
			}
			if (this._Effects != null && this._Effects.Count > 0)
			{
				Effect[] array2 = this._Effects.GetArray();
				int j = 0;
				int num2 = this._Effects.Count;
				while (j < num2)
				{
					Effect effect = array2[j];
					if (effect.WantEvent(id, cascadeLevel))
					{
						if (!E.Dispatch(effect))
						{
							return false;
						}
						if (!effect.HandleEvent(E))
						{
							return false;
						}
						if (num2 != this._Effects.Count)
						{
							num2 = this._Effects.Count;
							array2 = this._Effects.GetArray();
							if (j < num2 && array2[j] != effect)
							{
								j--;
							}
						}
					}
					j++;
				}
			}
			return list == null || list.DispatchRange(E, 0, int.MaxValue);
		}

		// Token: 0x06006049 RID: 24649 RVA: 0x001D1530 File Offset: 0x001CF730
		private bool HandleMakeTemporaryEvent(MakeTemporaryEvent E)
		{
			XRL.World.Parts.Temporary part = this.GetPart<XRL.World.Parts.Temporary>();
			int num = E.Duration;
			GameObject gameObject = E.DependsOn;
			if (E.RootObject != this)
			{
				num = -1;
				gameObject = E.RootObject;
			}
			if (part == null)
			{
				this.AddPart<XRL.World.Parts.Temporary>(new XRL.World.Parts.Temporary(num, E.TurnInto), true, false);
			}
			else
			{
				if (num != -1 && (part.Duration == -1 || part.Duration < num))
				{
					part.Duration = num;
				}
				if (!E.TurnInto.IsNullOrEmpty() && part.TurnInto.IsNullOrEmpty())
				{
					part.TurnInto = E.TurnInto;
				}
			}
			if (gameObject != null)
			{
				ExistenceSupport existenceSupport = this.RequirePart<ExistenceSupport>(false);
				existenceSupport.SupportedBy = gameObject;
				if (E.RootObjectValidateEveryTurn && this == E.RootObject)
				{
					existenceSupport.ValidateEveryTurn = true;
				}
			}
			return true;
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x001D15EC File Offset: 0x001CF7EC
		private bool HandleOwnerGetInventoryActionsEvent(OwnerGetInventoryActionsEvent E)
		{
			if (E.Actor == this && this.IsPlayer() && E.Object != null)
			{
				bool flag = E.Object.IsPlayerLed();
				if (flag && !E.Object.IsPlayer())
				{
					E.AddAction("Attack Target", "direct to attack target", "CompanionAttackTarget", "attack", 'a', true, 0, 0, false, true, false, false, true, null, false);
					if (E.Object.Brain != null)
					{
						if (E.Object.Brain.Passive)
						{
							E.AddAction("Aggressive Engagement", "direct to engage aggressively", "CompanionToggleEngagement", "engage", 'e', true, 0, 0, false, true, false, false, true, null, false);
						}
						else
						{
							E.AddAction("Passive Engagement", "direct to engage defensively only", "CompanionToggleEngagement", "engage", 'e', true, 0, 0, false, true, false, false, true, null, false);
						}
						if (E.Object.IsPotentiallyMobile() && E.Object.Brain.Staying)
						{
							E.AddAction("Come", "direct to come along", "CompanionCome", "come", 'c', true, 0, 0, false, true, false, false, true, null, false);
						}
					}
					if (E.Object.WillTrade())
					{
						E.AddAction("Give Items", "give items", "CompanionGiveItems", null, 'g', true, 0, 0, false, false, false, false, true, null, false);
					}
					if (E.Object.IsPotentiallyMobile())
					{
						E.AddAction("Move To", "direct to move", "CompanionMoveTo", null, 'm', true, 0, 0, false, true, false, false, true, null, false);
						E.AddAction("Change Follow Distance", "direct to change follow distance", "CompanionChangeFollowDistance", null, 'f', true, 0, 0, false, true, false, false, true, null, false);
					}
					if (E.Object.IsPotentiallyMobile() && E.Object.Brain != null && !E.Object.Brain.Staying)
					{
						E.AddAction("Stay", "direct to stay there", "CompanionStay", null, 's', true, 0, 0, false, true, false, false, true, null, false);
					}
					ActivatedAbilities activatedAbilities = E.Object.ActivatedAbilities;
					if (activatedAbilities != null && activatedAbilities.GetAbilityCount() > 0)
					{
						E.AddAction("Change Ability Use", "direct ability use", "CompanionAbilityUse", null, 'u', true, 0, 0, false, true, false, false, true, null, false);
					}
				}
				if (flag || (E.Object.IsPlayer() && E.Object.GetIntProperty("Renamed", 0) == 1))
				{
					E.AddAction("Rename", "rename", "CompanionRename", null, 'r', true, 0, 0, false, true, false, false, true, null, false);
				}
				if (E.Object == E.Actor.Target)
				{
					E.AddAction("Untarget", "untarget", "Untarget", null, 't', true, 0, 0, false, true, false, false, true, null, false);
				}
				else if (!E.Actor.IsPlayer() || E.Object.CanBeTargetedByPlayer())
				{
					E.AddAction("Target", "target", "Target", null, 't', true, 0, 0, false, true, false, false, true, null, false);
				}
				E.AddAction("Show Effects", "show effects", "ShowEffects", null, 'w', true, 0, 0, false, true, false, false, true, null, false);
			}
			return true;
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x001D1908 File Offset: 0x001CFB08
		private bool HandleEnteredCellEvent(EnteredCellEvent E)
		{
			if (this.IsPlayer() && (!AutoAct.IsActive(false) || AutoAct.IsMovement()) && E.Cell.AnyTakeable() && !AutoAct.ShouldHostilesPreventAutoget())
			{
				Sidebar.ClearAutogotItems();
				AutoAct.ResumeSetting = AutoAct.Setting;
				AutoAct.Setting = "g";
			}
			return true;
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x001D195C File Offset: 0x001CFB5C
		private bool HandleInventoryActionEvent(InventoryActionEvent E)
		{
			E.Item.ModIntProperty("InventoryActions", 1, false);
			E.Item.ModIntProperty("InventoryActions" + E.Command, 1, false);
			E.Item.SetStringProperty("LastInventoryActionCommand", E.Command, false);
			E.Item.SetLongProperty("LastInventoryActionTurn", XRLCore.CurrentTurn);
			if (E.Command == "Target")
			{
				if (E.Item.CanBeTargetedByPlayer())
				{
					Sidebar.CurrentTarget = E.Item;
				}
				E.RequestInterfaceExit();
			}
			else if (E.Command == "Untarget")
			{
				if (E.Actor.Target == E.Item)
				{
					E.Actor.Target = null;
				}
				foreach (GameObject gameObject in E.Actor.GetCompanions())
				{
					gameObject.StopFighting(E.Item, false);
				}
				E.RequestInterfaceExit();
			}
			else if (E.Command == "ShowEffects")
			{
				E.Item.ShowActiveEffects();
			}
			else if (E.Command == "Autoget")
			{
				if (E.Actor.TakeObject(E.Item, false, false, new int?(0), null, null) && GameObject.Validate(ref E.Item))
				{
					Sidebar.AddAutogotItem(E.Item);
					Sidebar.Update();
				}
			}
			else if (E.Command.StartsWith("Companion"))
			{
				if (E.Command == "CompanionRename")
				{
					this.HandleRename(E);
				}
				else if (E.Command == "CompanionGiveItems")
				{
					if (E.Item.IsPlayerLed() && this.DistanceTo(E.Item) <= 1 && !this.HasProperty("FugueCopy"))
					{
						TradeUI.ShowTradeScreen(E.Item, 0f, TradeUI.TradeScreenMode.Trade);
						this.UseEnergy(1000, "Companion Trade", null, null, false);
						E.RequestInterfaceExit();
					}
				}
				else if (E.Command == "CompanionAttackTarget")
				{
					if (this.CheckCompanionDirection(E.Item))
					{
						Look.HideTooltips();
						Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.Burst, 0, 9999, this.CurrentCell.X, this.CurrentCell.Y, true, AllowVis.OnlyVisible, null, null, null, null, "Companion Attack Target", false, true);
						if (cell != null)
						{
							GameObject combatTarget = cell.GetCombatTarget(E.Item, true, false, false, 5, null, null, null, null, null, true, false, null);
							if (combatTarget != null)
							{
								E.Item.Brain.Goals.Clear();
								E.Item.Brain.WantToKill(combatTarget, "because my leader directed me", true);
								E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								this.CompanionDirectionEnergyCost(E.Item, 100, "Attack Target");
								E.RequestInterfaceExit();
							}
						}
					}
				}
				else if (E.Command == "CompanionMoveTo")
				{
					if (E.Item.IsPotentiallyMobile() && this.CheckCompanionDirection(E.Item))
					{
						Look.HideTooltips();
						Cell cell2 = PickTarget.ShowPicker(PickTarget.PickStyle.Burst, 0, 9999, E.Item.CurrentCell.X, E.Item.CurrentCell.Y, false, AllowVis.OnlyExplored, null, null, null, null, "Companion Move To", false, true);
						if (cell2 != null && cell2 != E.Item.CurrentCell)
						{
							if (E.Item.Brain.Staying)
							{
								E.Item.Brain.Stay(cell2);
							}
							E.Item.Brain.Goals.Clear();
							E.Item.Brain.PushGoal(new MoveTo(cell2, false, true, 0, false, false, false, 100, -1, 95));
							E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							this.CompanionDirectionEnergyCost(E.Item, 100, "Move");
							E.RequestInterfaceExit();
						}
					}
				}
				else if (E.Command == "CompanionChangeFollowDistance")
				{
					if (E.Item.IsPotentiallyMobile() && this.CheckCompanionDirection(E.Item))
					{
						int num = Popup.PickOption("", "Instruct " + E.Item.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to follow at what distance?", "", "Sounds/UI/ui_notification", new string[] { "close", "medium", "far" }, new char[] { 'c', 'm', 'f' }, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num == 0)
						{
							E.Item.SetIntProperty("PartyLeaderFollowDistance", 1, false);
							E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							this.CompanionDirectionEnergyCost(E.Item, 100, "Change Follow Distance");
							E.RequestInterfaceExit();
						}
						else if (num == 1)
						{
							E.Item.SetIntProperty("PartyLeaderFollowDistance", 5, false);
							E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							this.CompanionDirectionEnergyCost(E.Item, 100, "Change Follow Distance");
							E.RequestInterfaceExit();
						}
						else if (num == 2)
						{
							E.Item.SetIntProperty("PartyLeaderFollowDistance", 9, false);
							E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
							this.CompanionDirectionEnergyCost(E.Item, 100, "Change Follow Distance");
							E.RequestInterfaceExit();
						}
					}
				}
				else if (E.Command == "CompanionStay")
				{
					if (E.Item.IsPotentiallyMobile() && !E.Item.Brain.Staying && this.CheckCompanionDirection(E.Item))
					{
						E.Item.Brain.Goals.Clear();
						E.Item.Brain.Stay(E.Item.CurrentCell);
						E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						this.CompanionDirectionEnergyCost(E.Item, 100, "Stay");
						E.RequestInterfaceExit();
					}
				}
				else if (E.Command == "CompanionCome")
				{
					if (E.Item.IsPotentiallyMobile() && E.Item.Brain.Staying && this.CheckCompanionDirection(E.Item))
					{
						E.Item.Brain.Stay(null);
						E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						this.CompanionDirectionEnergyCost(E.Item, 100, "Come");
						E.RequestInterfaceExit();
					}
				}
				else if (E.Command == "CompanionToggleEngagement")
				{
					if (this.CheckCompanionDirection(E.Item))
					{
						E.Item.Brain.Passive = !E.Item.Brain.Passive;
						E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						this.CompanionDirectionEnergyCost(E.Item, 100, "Engage");
						E.RequestInterfaceExit();
					}
				}
				else if (E.Command == "CompanionAbilityUse")
				{
					ActivatedAbilities activatedAbilities = E.Item.ActivatedAbilities;
					if (activatedAbilities != null && activatedAbilities.GetAbilityCount() > 0 && this.CheckCompanionDirection(E.Item))
					{
						this.ChangeCompanionAbilityUse(E.Item, activatedAbilities);
						E.Actor.PlayWorldSound("Sounds/Interact/sfx_interact_follower_command", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						this.CompanionDirectionEnergyCost(E.Item, 100, "Ability Use");
						E.RequestInterfaceExit();
					}
				}
			}
			return true;
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x001D2284 File Offset: 0x001D0484
		private void HandleRename(InventoryActionEvent E)
		{
			if (!CanBeNamedEvent.Check(E.Actor, E.Item))
			{
				return;
			}
			if (E.Item.HasProperName && E.Item.GetIntProperty("Renamed", 0) != 1)
			{
				Popup.ShowFail(E.Item.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " want a new name.", true, true, true);
				return;
			}
			Look.HideTooltips();
			bool flag = E.Item.IsPlayer();
			string text = E.Item.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, flag, flag, null, true, null, false);
			List<string> list = new List<string>();
			List<GameObject> list2 = new List<GameObject>();
			list.Add("Enter a name for " + text + ".");
			list2.Add(null);
			list.Add("Choose a random name from " + Grammar.MakePossessive(E.Item.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, flag, false, null, true, null, false)) + " culture.");
			list2.Add(E.Item);
			if (this.CurrentCell != null)
			{
				foreach (Cell cell in this.CurrentCell.IterateAdjacent(1, false, true, true))
				{
					foreach (GameObject gameObject in cell.Objects)
					{
						if (gameObject.Brain != null && !list2.Contains(gameObject) && !gameObject.IsTemporary && !gameObject.IsPlayer() && !gameObject.IsHostileTowards(this) && !gameObject.IsHostileTowards(E.Item))
						{
							list.Add("Choose a random name from " + Grammar.MakePossessive(gameObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false)) + " culture.");
							list2.Add(gameObject);
						}
					}
				}
			}
			if (!flag)
			{
				list.Add("Choose a random name from your own culture.");
				list2.Add(this);
			}
			int num = Popup.PickOption(flag ? "Rename yourself" : "Rename your companion", null, "", "Sounds/UI/ui_notification", list, HotkeySpread.get(new string[] { "Menus", "UINav" }), null, null, null, null, null, 1, 60, 0, -1, true, false, false, true, false, null, null);
			string text2 = "";
			if (num >= 0 && num < list2.Count)
			{
				GameObject gameObject2 = list2[num];
				if (gameObject2 == null)
				{
					text2 = Popup.AskString("Enter a new name for " + text + ".", E.Item.Render.DisplayName, "Sounds/UI/ui_notification", null, null, 128, 0, true, true, null);
				}
				else
				{
					text2 = NameMaker.MakeName(gameObject2, null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
				}
			}
			if (!text2.IsNullOrEmpty())
			{
				Popup.Show(string.Concat(new string[] { "You start calling ", text, " by the name '", text2, "'." }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				if (!E.Item.HasProperName)
				{
					if (!flag)
					{
						string text3 = string.Concat(new string[]
						{
							"You started calling ",
							E.Item.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false),
							" by the name '",
							text2,
							"'."
						});
						string text4 = HistoricStringExpander.ExpandString(string.Concat(new string[]
						{
							"<spice.instancesOf.justice.!random.capitalize>! =name= brought enlightenment to a simple ",
							E.Item.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true),
							" and bestowed onto ",
							E.Item.them,
							" the name ",
							text2,
							"."
						}), null);
						string[] array = new string[11];
						array[0] = "<spice.elements.";
						array[1] = XRL.The.Player.GetMythicDomain(5, null);
						array[2] = ".weddingConditions.!random.capitalize>, =name= cemented ";
						array[3] = XRL.The.Player.GetPronounProvider(false).PossessiveAdjective;
						array[4] = " friendship with ";
						int num2 = 5;
						GameObject item = E.Item;
						Faction ifExists = Factions.GetIfExists((item != null) ? item.GetPrimaryFaction(false) : null);
						array[num2] = ((ifExists != null) ? ifExists.GetFormattedName() : null);
						array[6] = " by marrying ";
						array[7] = text;
						array[8] = ", who took the name ";
						array[9] = text2;
						array[10] = ".";
						JournalAPI.AddAccomplishment(text3, text4, string.Concat(array), null, "general", MuralCategory.Treats, MuralWeight.Medium, null, -1L, true);
					}
					string propertyOrTag = this.GetPropertyOrTag("TitleIfNamed", null);
					if (!propertyOrTag.IsNullOrEmpty())
					{
						this.RequirePart<Titles>(false).AddTitle(propertyOrTag, 0);
					}
				}
				E.Item.GiveProperName(text2, true, null, false, null, null, null, null);
				E.Item.SetIntProperty("Renamed", 1, false);
				E.RequestInterfaceExit();
				this.UseEnergy(1000, "Companion Rename", null, null, false);
			}
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x001D2810 File Offset: 0x001D0A10
		private void ChangeCompanionAbilityUse(GameObject Subject, ActivatedAbilities Abilities)
		{
			List<ActivatedAbilityEntry> list = new List<ActivatedAbilityEntry>(Abilities.AbilityByGuid.Values);
			list.Sort((ActivatedAbilityEntry a, ActivatedAbilityEntry b) => a.DisplayName.CompareTo(b.DisplayName));
			List<string> list2 = new List<string>(list.Count);
			List<char> list3 = new List<char>(list.Count);
			char c = 'a';
			foreach (ActivatedAbilityEntry activatedAbilityEntry in list)
			{
				string text = activatedAbilityEntry.DisplayName;
				if (activatedAbilityEntry.Toggleable && !activatedAbilityEntry.ActiveToggle && !activatedAbilityEntry.Command.IsNullOrEmpty())
				{
					if (activatedAbilityEntry.ToggleState)
					{
						text += " {{g|[toggled on]}}";
					}
					else
					{
						text += " {{y|[toggled off]}}";
					}
				}
				else if (activatedAbilityEntry.AIDisable)
				{
					text += " {{K|[forbidden]}}";
				}
				else
				{
					text += " {{Y|[allowed]}}";
				}
				list2.Add(text);
				List<char> list4 = list3;
				int num;
				if (c > 'z')
				{
					num = 32;
				}
				else
				{
					c = (char)((num = (int)c) + 1);
				}
				list4.Add((char)num);
			}
			int num2 = Popup.PickOption("", "Choose one of " + Grammar.MakePossessive(Subject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + " abilities to forbid or allow.", "", "Sounds/UI/ui_notification", list2.ToArray(), list3.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
			if (num2 >= 0)
			{
				ActivatedAbilityEntry activatedAbilityEntry2 = list[num2];
				if (activatedAbilityEntry2.Toggleable && !activatedAbilityEntry2.ActiveToggle && !activatedAbilityEntry2.Command.IsNullOrEmpty())
				{
					bool toggleState = activatedAbilityEntry2.ToggleState;
					Subject.FireEvent(Event.New(activatedAbilityEntry2.Command, 0, 0, 0));
					if (toggleState != activatedAbilityEntry2.ToggleState)
					{
						Popup.Show(Subject.Poss(activatedAbilityEntry2.DisplayName + " ability", true) + " is now toggled " + (activatedAbilityEntry2.ToggleState ? "on" : "off") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return;
					}
					Popup.Show(Subject.Poss(activatedAbilityEntry2.DisplayName + " ability", true) + " cannot be toggled at this time.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				else
				{
					activatedAbilityEntry2.AIDisable = !activatedAbilityEntry2.AIDisable;
					Popup.Show(Subject.Poss(activatedAbilityEntry2.DisplayName + " ability", true) + " is now " + (activatedAbilityEntry2.AIDisable ? "forbidden" : "allowed") + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x001D2AF4 File Offset: 0x001D0CF4
		public bool CheckCompanionDirection(GameObject Subject)
		{
			if (!Subject.IsPlayerLed())
			{
				return false;
			}
			if (!this.IsAudible(Subject, 20) && !this.CanMakeTelepathicContactWith(Subject))
			{
				Popup.ShowFail(Subject.Does("can't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " hear you!", true, true, true);
				return false;
			}
			return true;
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x001D2B60 File Offset: 0x001D0D60
		public string GetInventoryCategory(bool AsIfKnown = false)
		{
			GetInventoryCategoryEvent getInventoryCategoryEvent = GetInventoryCategoryEvent.FromPool(this, "");
			getInventoryCategoryEvent.AsIfKnown = AsIfKnown;
			this.HandleEvent(getInventoryCategoryEvent);
			string category = getInventoryCategoryEvent.Category;
			PooledEvent<GetInventoryCategoryEvent>.ResetTo(ref getInventoryCategoryEvent);
			if (category == null)
			{
				MetricsManager.LogWarning("Unknown inventory category for " + ((this != null) ? this.Blueprint : null));
				return "unknown";
			}
			return category;
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06006051 RID: 24657 RVA: 0x001D2BBB File Offset: 0x001D0DBB
		public bool IsDying
		{
			get
			{
				return this.Dying;
			}
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001D2BC4 File Offset: 0x001D0DC4
		public bool Die(GameObject Killer = null, string KillerText = null, string Reason = null, string ThirdPersonReason = null, bool Accidental = false, GameObject Weapon = null, GameObject Projectile = null, bool Force = false, bool AlwaysUsePopups = false, string Message = null, string DeathVerb = null, string DeathCategory = null)
		{
			if (this.Dying)
			{
				return true;
			}
			bool flag = true;
			try
			{
				this.Dying = true;
				if (BeforeDieEvent.Check(this, Killer, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText, Reason, ThirdPersonReason))
				{
					AfterDieEvent.Send(this, Killer, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText, Reason, ThirdPersonReason);
					this.StopMoving();
					if (this.IsFrozen())
					{
						this.PlayWorldSound("Sounds/Damage/sfx_destroy_ice", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					if (Options.UseParticleVFX && this.Render != null && this.Render.Tile != null)
					{
						if (DeathCategory == "lased to death")
						{
							Cell currentCell = this.GetCurrentCell();
							if (currentCell != null && currentCell.InActiveZone)
							{
								CombatJuice.playPrefabAnimation(currentCell.Location, "Deaths/DeathVFXLased", null, this.Render.Tile, null, false);
							}
						}
						else if (DeathCategory == "immolated")
						{
							Cell currentCell2 = this.GetCurrentCell();
							if (currentCell2 != null && currentCell2.InActiveZone)
							{
								CombatJuice.playPrefabAnimation(currentCell2.Location, "Deaths/DeathVFXImmolated", null, this.Render.Tile, null, false);
							}
						}
					}
					if (this.IsPlayer())
					{
						if (XRLCore.Core.Game.Running)
						{
							XRLGame game = XRL.The.Game;
							if (((game != null) ? game.GetStringGameState("JoppaWorldTutorial", "") : null) == "Yes")
							{
								TutorialManager.ShowIntermissionPopupAsync("Oh, you died. That's okay. It's a very common occurance in the world of Qud.\n\nBecause the tutorial drops you off in Classic mode with permadeath, your character is wiped when you die.\n\nThe good news is: you get to make a whole new character! Now with a little more knowledge than you had before.", delegate
								{
									TutorialManager.ShowIntermissionPopupAsync("Next time, if you want a more forgiving mode of play, try Roleplay, which lets you checkpoint at settlements.\n\nOr try Wander mode, where most creatures are neutral to you and you gain experience via discovery instead of killing.\n\nOr keep playing Classic if the challenge appeals to you and you'd like to explore a breadth of character types.", delegate
									{
										TutorialManager.ShowIntermissionPopupAsync("Whatever you choose next, feel free to return to the tutorial if you need a refresher.\n\nLive and drink, friend!", null, 0, 0f, "sw");
									}, 0, 0f, "sw");
								}, 0, 0f, "sw");
							}
							KilledPlayerEvent.Send(this, Killer, ref Reason, ref ThirdPersonReason, Weapon, Projectile, Accidental, AlwaysUsePopups, null);
							XRLCore.Core.RenderBase(true, false);
							if (CheckpointingSystem.ShowDeathMessage("You died.\n\n" + (Reason ?? XRLCore.Core.Game.DeathReason), XRLCore.Core.Game.DeathCategory))
							{
								return true;
							}
							if (!Force && (!Options.AllowReallydie || Popup.ShowYesNo("DEBUG: Do you really want to die?", "Sounds/UI/ui_notification", true, DialogResult.No, null) == DialogResult.Yes))
							{
								if (Reason != null)
								{
									XRLCore.Core.Game.DeathReason = Reason;
								}
								if (DeathCategory != null)
								{
									XRLCore.Core.Game.DeathCategory = DeathCategory;
								}
								XRLCore.Core.Game.Running = false;
								string text = XRLCore.Core.Game.DeathReason;
								if (!text.IsNullOrEmpty())
								{
									text = text[0].ToString().ToLower() + text.Substring(1);
								}
								JournalAPI.AddAccomplishment(string.Concat(new string[]
								{
									"On the ",
									Calendar.GetDay(),
									" of ",
									Calendar.GetMonth(),
									", ",
									text.Replace("!", ".")
								}), null, null, null, "general", MuralCategory.Generic, MuralWeight.Nil, null, -1L, true);
								MetricsManager.LogEvent("Death:Reason:" + text.Replace(':', '_'));
								MetricsManager.LogEvent("Death:Category:" + (string.IsNullOrEmpty(DeathCategory) ? "" : DeathCategory).Replace(':', '_'));
								MetricsManager.LogEvent("Death:Turns:" + XRLCore.CurrentTurn.ToString());
								MetricsManager.LogEvent("Death:Walltime:" + XRLCore.Core.Game._walltime.ToString());
								if (this.HasStat("Level"))
								{
									MetricsManager.LogEvent("Death:Level:" + this.Statistics["Level"].BaseValue.ToString());
								}
								Achievement.DIE.Unlock();
							}
							else
							{
								this.Statistics["Hitpoints"].Penalty = 0;
								Stomach stomach;
								if (this.TryGetPart<Stomach>(out stomach))
								{
									stomach.Water = RuleSettings.WATER_MAXIMUM;
								}
								flag = false;
							}
						}
					}
					else
					{
						string propertyOrTag = this.GetPropertyOrTag("DeathSounds", null);
						if (!propertyOrTag.IsNullOrEmpty())
						{
							this.PlayWorldSound(propertyOrTag.CachedCommaExpansion().GetRandomElement<string>(null), 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
						}
						if (Message != null)
						{
							if (Message != "")
							{
								this.EmitMessage(Message, null, null, false);
							}
						}
						else if (!this.HasTagOrProperty("NoDeathVerb"))
						{
							if (!DeathVerb.IsNullOrEmpty())
							{
								this.Physics.DidX(DeathVerb, null, "!", null, null, null, this, false, false, null, null, false, false, false, false, AlwaysUsePopups, null);
							}
							else if (this.HasTagOrProperty("CustomDeathVerb"))
							{
								this.Physics.DidX(this.GetTagOrStringProperty("CustomDeathVerb", "die"), null, "!", null, null, null, this, false, false, null, null, false, false, false, false, AlwaysUsePopups, null);
							}
							else if (this.HasTagOrProperty("CustomDeathMessage"))
							{
								Message = this.GetTagOrStringProperty("CustomDeathMessage", null);
								if (!Message.IsNullOrEmpty())
								{
									this.EmitMessage(GameText.VariableReplace(Message, this, Killer, false), null, null, false);
								}
							}
							else if (this.Brain != null && !this.HasTagOrProperty("DeathMessageAsInanimate"))
							{
								this.Brain.DidX("die", null, "!", null, null, null, this, false, false, null, null, false, false, false, false, AlwaysUsePopups, null);
							}
							else
							{
								XRL.World.Parts.Physics physics = this.Physics;
								if (physics != null)
								{
									physics.DidX("are", "destroyed", "!", null, null, null, this, false, false, null, null, false, false, false, false, AlwaysUsePopups, null);
								}
							}
						}
						if (Killer != null)
						{
							KilledEvent.Send(this, Killer, ref Reason, ref ThirdPersonReason, Weapon, Projectile, Accidental, AlwaysUsePopups, null);
							this.AwardXPTo(Killer, true, null, false);
							if (Killer.IsPlayer())
							{
								MetricsManager.LogEvent("PlayerKill:" + this.Blueprint);
							}
						}
						WeaponUsageTracking.TrackKill(Killer, this, Weapon, Projectile, Accidental);
						EarlyBeforeDeathRemovalEvent.Send(this, Killer, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText, Reason, ThirdPersonReason);
						BeforeDeathRemovalEvent.Send(this, Killer, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText, Reason, ThirdPersonReason);
						OnDeathRemovalEvent.Send(this, Killer, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText, Reason, ThirdPersonReason);
						DeathEvent.Send(this, Killer, Weapon, Projectile, Accidental, AlwaysUsePopups, KillerText, Reason, ThirdPersonReason);
						this.Destroy(Reason, false, false, ThirdPersonReason);
						MetricsManager.LogEvent("Kill:" + this.Blueprint);
					}
				}
				else
				{
					flag = false;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("GameObject::Die", ex, "game_exception");
			}
			this.Dying = false;
			return flag;
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x001D324C File Offset: 0x001D144C
		public int GetComplexity()
		{
			Examiner part = this.GetPart<Examiner>();
			if (part == null)
			{
				return this.GetTechTier();
			}
			return part.Complexity;
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x001D3264 File Offset: 0x001D1464
		public int GetExamineDifficulty()
		{
			Examiner part = this.GetPart<Examiner>();
			if (part == null)
			{
				return 0;
			}
			return part.Difficulty;
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x001D3278 File Offset: 0x001D1478
		public bool Explode(int Force, GameObject Owner = null, string BonusDamage = null, float DamageModifier = 1f, bool Neutron = false, bool SuppressDestroy = false, bool Indirect = false, int Phase = 0, List<GameObject> Hit = null)
		{
			this.SplitFromStack();
			if (Hit == null)
			{
				Hit = Event.NewGameObjectList();
				Hit.Add(this);
			}
			else if (!Hit.Contains(this))
			{
				Hit.Add(this);
			}
			Cell currentCell = this.GetCurrentCell();
			if (!SuppressDestroy)
			{
				this.RemoveFromContext(null);
			}
			XRL.World.Parts.Physics.ApplyExplosion(currentCell, Force, null, Hit, false, true, Owner, BonusDamage, (Phase == 0) ? this.GetPhase() : Phase, Neutron, Indirect, DamageModifier, this);
			if (!SuppressDestroy)
			{
				if (this.IsCreature)
				{
					if (Neutron)
					{
						if (this.IsPlayer())
						{
							Achievement.CRUSHED_UNDER_SUNS.Unlock();
						}
						this.Die(Owner, "laws of physics", "You were crushed under the weight of a thousand suns.", this.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " @@crushed under the weight of a thousand suns.", false, null, null, false, false, null, null, null);
					}
					else
					{
						this.Die(Owner, "laws of physics", "You exploded.", this.It + " @@exploded.", false, null, null, false, false, null, null, null);
					}
				}
				else
				{
					this.Destroy(null, true, false, null);
				}
			}
			return true;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x001D339C File Offset: 0x001D159C
		public int Discharge(Cell TargetCell = null, int Voltage = 3, int Damage = 0, string DamageRange = null, DieRoll DamageRoll = null, GameObject Owner = null, GameObject Source = null, GameObject Target = null, GameObject DescribeAsFrom = null, GameObject Skip = null, List<GameObject> SkipList = null, Cell StartCell = null, int Phase = 0, bool Accidental = false, bool Environmental = false, bool UsePopups = false)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return 0;
			}
			return physics.ApplyDischarge(StartCell ?? this.GetCurrentCell(), TargetCell, Voltage, Damage, DamageRange, DamageRoll, Target, null, Owner, Source, DescribeAsFrom, Skip, SkipList, null, null, null, Phase, Accidental, Environmental, null, null, null, UsePopups);
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x001D3400 File Offset: 0x001D1600
		public int GetBaseThrowRange(GameObject Thrown = null, GameObject ApparentTarget = null, Cell TargetCell = null, int Distance = 0)
		{
			int num;
			int num2;
			int num3;
			bool flag;
			GetThrowProfileEvent.Process(out num, out num2, out num3, out flag, this, Thrown, ApparentTarget, TargetCell, Distance);
			return num;
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x001D3422 File Offset: 0x001D1622
		public int GetThrowRangeRandomVariance()
		{
			return XRL.Rules.Stat.Random(RuleSettings.THROW_RANGE_RANDOM_VARIANCE_MIN, RuleSettings.THROW_RANGE_RANDOM_VARIANCE_MAX);
		}

		// Token: 0x06006059 RID: 24665 RVA: 0x001D3433 File Offset: 0x001D1633
		public int GetThrowDistanceRandomVariance()
		{
			return RuleSettings.THROW_DISTANCE_VARIANCE.RollCached();
		}

		// Token: 0x0600605A RID: 24666 RVA: 0x001D3440 File Offset: 0x001D1640
		protected bool EquipReplacementThrownWeapon(GameObject PreviouslyEquipped, BodyPart ThrowPart)
		{
			if (!ReplaceThrownWeaponEvent.Check(this, PreviouslyEquipped, ThrowPart))
			{
				return true;
			}
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return false;
			}
			string propertyOrTag = this.GetPropertyOrTag("NoEquip", null);
			List<string> list = (propertyOrTag.IsNullOrEmpty() ? null : new List<string>(propertyOrTag.CachedCommaExpansion()));
			List<GameObject> objectsDirect = inventory.GetObjectsDirect();
			GameObject gameObject = null;
			foreach (GameObject gameObject2 in objectsDirect)
			{
				if (gameObject2.SameAs(PreviouslyEquipped) && (this.IsPlayer() || !gameObject2.HasPropertyOrTag("NoAIEquip")) && (list == null || !list.Contains(gameObject2.Blueprint)) && !gameObject2.IsBroken() && !gameObject2.IsRusted() && !gameObject2.HasTag("HiddenInInventory"))
				{
					gameObject = gameObject2;
					break;
				}
			}
			if (gameObject == null)
			{
				foreach (GameObject gameObject3 in objectsDirect)
				{
					if (gameObject3.Blueprint == PreviouslyEquipped.Blueprint && (this.IsPlayer() || !gameObject3.HasPropertyOrTag("NoAIEquip")) && (list == null || !list.Contains(gameObject3.Blueprint)) && !gameObject3.IsBroken() && !gameObject3.IsRusted() && !gameObject3.HasTag("HiddenInInventory"))
					{
						gameObject = gameObject3;
						break;
					}
				}
				if (gameObject == null && !this.IsPlayer() && PreviouslyEquipped.HasTag("Grenade"))
				{
					foreach (GameObject gameObject4 in objectsDirect)
					{
						if (gameObject4.HasTag("Grenade") && (this.IsPlayer() || !gameObject4.HasPropertyOrTag("NoAIEquip")) && (list == null || !list.Contains(gameObject4.Blueprint)) && !gameObject4.IsBroken() && !gameObject4.IsRusted() && !gameObject4.HasTag("HiddenInInventory"))
						{
							gameObject = gameObject4;
							break;
						}
					}
				}
			}
			return gameObject != null && this.FireEvent(Event.New("CommandEquipObject", "Object", gameObject, "BodyPart", ThrowPart));
		}

		// Token: 0x0600605B RID: 24667 RVA: 0x001D36A4 File Offset: 0x001D18A4
		public bool PerformThrow(GameObject Weapon, Cell TargetCell, GameObject ApparentTarget = null, MissilePath MPath = null, int Phase = 0, int? RangeVariance = null, int? DistanceVariance = null, int? EnergyCost = null)
		{
			GameObject.<>c__DisplayClass1253_0 CS$<>8__locals1 = new GameObject.<>c__DisplayClass1253_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.Weapon = Weapon;
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			BodyPart bodyPart = CS$<>8__locals1.Weapon.EquippedOn();
			if (bodyPart == null)
			{
				return false;
			}
			if (!this.FireEvent("BeginAttack"))
			{
				return false;
			}
			int num = this.DistanceTo(TargetCell) + 1;
			if (ApparentTarget == null)
			{
				ApparentTarget = TargetCell.GetCombatTarget(this, true, false, false, 5, null, null, null, null, null, true, true, null);
			}
			if (ApparentTarget == this && ApparentTarget.IsPlayer() && Popup.ShowYesNoCancel("Are you sure you want to target " + this.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
			{
				return false;
			}
			if (Phase == 0)
			{
				Phase = Phase.getWeaponPhase(this, GetActivationPhaseEvent.GetFor(CS$<>8__locals1.Weapon, 0));
			}
			Event @event = Event.New("BeforeThrown", 0, 0, 0);
			@event.SetParameter("TargetCell", TargetCell);
			@event.SetParameter("Thrower", this);
			@event.SetParameter("ApparentTarget", ApparentTarget);
			@event.SetParameter("Phase", Phase);
			if (!CS$<>8__locals1.Weapon.FireEvent(@event))
			{
				return false;
			}
			IThrownWeaponFlexPhaseProvider @for = GetThrownWeaponFlexPhaseProviderEvent.GetFor(CS$<>8__locals1.Weapon, this);
			bodyPart.Unequip();
			this.EquipReplacementThrownWeapon(CS$<>8__locals1.Weapon, bodyPart);
			MissileWeapon.SetupProjectile(CS$<>8__locals1.Weapon, this, null, null);
			if (MPath == null)
			{
				if (GameObject.ThrowPathInUse)
				{
					MPath = new MissilePath();
				}
				else
				{
					MPath = GameObject.ThrowPath;
					GameObject.ThrowPathInUse = true;
				}
				MissileWeapon.CalculateMissilePath(MPath, currentCell.ParentZone, currentCell.X, currentCell.Y, TargetCell.X, TargetCell.Y, false, false, false, this);
			}
			CS$<>8__locals1.Weapon.PlayCombatSoundTag("ThrownSound", "Sounds/Throw/sfx_throwing_generic_throw", null, 0.5f);
			if (@for != null)
			{
				@for.ThrownWeaponFlexPhaseStart(CS$<>8__locals1.Weapon);
			}
			try
			{
				if (this.IsPlayer() && CS$<>8__locals1.Weapon != null)
				{
					CS$<>8__locals1.Weapon.RemoveIntProperty("AutoexploreActionAutoget");
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Throw::ClearFlags", ex, "game_exception");
			}
			bool flag31;
			try
			{
				int num2 = XRL.Rules.Stat.RollPenetratingSuccesses("1d" + this.Stat("Agility", 0).ToString(), 3, 0, 1, null);
				int num3 = 0;
				bool flag = false;
				bool flag2 = false;
				int num4 = currentCell.X - TargetCell.X;
				int num5 = currentCell.Y - TargetCell.Y;
				int num6 = (int)Math.Sqrt((double)(num4 * num4 + num5 * num5));
				int num7;
				int num8;
				int num9;
				bool flag3;
				GetThrowProfileEvent.Process(out num7, out num8, out num9, out flag3, this, CS$<>8__locals1.Weapon, ApparentTarget, TargetCell, num6);
				num7 += RangeVariance ?? this.GetThrowRangeRandomVariance();
				if (num <= num7 && (this.HasIntProperty("CloseThrowRangeAccuracyBonus") || this.HasIntProperty("CloseThrowRangeAccuracySkillBonus")))
				{
					float num10 = (100f - Math.Max((float)this.GetIntProperty("CloseThrowRangeAccuracyBonus", 0), (float)this.GetIntProperty("CloseThrowRangeAccuracySkillBonus", 0))) / 100f;
					num9 = (int)((float)num9 * num10);
				}
				else
				{
					num += DistanceVariance ?? this.GetThrowDistanceRandomVariance();
					if (num < 2 && num7 >= 2)
					{
						num = 2;
					}
				}
				List<Point> list = MissileWeapon.CalculateBulletTrajectory(MPath, CS$<>8__locals1.Weapon, CS$<>8__locals1.Weapon, this, currentCell.ParentZone, num9.ToString(), 0, 0, false);
				list.Insert(0, currentCell.Point);
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer2(false);
				Zone parentZone = currentCell.ParentZone;
				Cell cell = null;
				List<GameObject> objectsThatWantEvent = TargetCell.ParentZone.GetObjectsThatWantEvent(PooledEvent<ProjectileMovingEvent>.ID, ProjectileMovingEvent.CascadeLevel);
				ProjectileMovingEvent projectileMovingEvent = null;
				if (objectsThatWantEvent.Count > 0)
				{
					projectileMovingEvent = PooledEvent<ProjectileMovingEvent>.FromPool();
					projectileMovingEvent.Attacker = this;
					projectileMovingEvent.Projectile = CS$<>8__locals1.Weapon;
					projectileMovingEvent.ScreenBuffer = scrapBuffer;
					projectileMovingEvent.TargetCell = TargetCell;
					projectileMovingEvent.ApparentTarget = ApparentTarget;
					projectileMovingEvent.Path = list;
					projectileMovingEvent.Throw = true;
				}
				if (flag3)
				{
					this.TelekinesisBlip();
				}
				Cell cell2 = currentCell;
				GameObject.<>c__DisplayClass1253_1 CS$<>8__locals2;
				CS$<>8__locals2.showJuice = false;
				CS$<>8__locals2.juiceStart = this.CurrentCell.Location;
				CS$<>8__locals2.juiceEnd = Location2D.Invalid;
				RenderEvent renderEvent = CS$<>8__locals1.Weapon.RenderForUI(null, false);
				CS$<>8__locals2.juiceTile = renderEvent.Tile;
				CS$<>8__locals2.juiceColor = renderEvent.GetForegroundColorChar().ToString();
				CS$<>8__locals2.juiceDetail = renderEvent.getDetailColor().ToString();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					int num11 = currentCell.X - list[i].X;
					int num12 = currentCell.Y - list[i].Y;
					int num13 = (int)Math.Sqrt((double)(num11 * num11 + num12 * num12));
					if (num13 >= num7 || num13 >= num)
					{
						break;
					}
					cell = parentZone.GetCell(list[i].X, list[i].Y);
					if (cell == null)
					{
						break;
					}
					if (cell.IsVisible())
					{
						CS$<>8__locals2.showJuice = true;
					}
					CS$<>8__locals2.juiceEnd = cell.Location;
					cell.WakeCreaturesInArea();
					if (flag3)
					{
						cell.TelekinesisBlip();
					}
					GameObject gameObject = null;
					if (cell != currentCell)
					{
						Cell cell3 = cell;
						bool flag4 = true;
						bool flag5 = false;
						bool flag6 = false;
						int num14 = Phase;
						GameObject gameObject2 = null;
						GameObject gameObject3 = null;
						GameObject gameObject4 = null;
						GameObject gameObject5 = null;
						List<GameObject> list2 = null;
						bool flag7 = true;
						bool flag8 = true;
						Predicate<GameObject> predicate;
						if ((predicate = CS$<>8__locals1.<>9__2) == null)
						{
							predicate = (CS$<>8__locals1.<>9__2 = (GameObject o) => o != CS$<>8__locals1.<>4__this);
						}
						gameObject = cell3.GetCombatTarget(this, flag4, flag5, flag6, num14, gameObject2, gameObject3, gameObject4, gameObject5, list2, flag7, flag8, predicate);
					}
					if ((num2 <= 0 || gameObject == null || gameObject == this) && count > 1 && i != count - 1)
					{
						Cell cell4 = parentZone.GetCell(list[i + 1].X, list[i + 1].Y);
						if (cell4 == null)
						{
							CS$<>8__locals1.Weapon.PlayCombatSoundTag("ImpactSound", null, null, 0.5f);
							flag2 = true;
							break;
						}
						Cell cell5 = cell4;
						GameObject gameObject6 = null;
						GameObject weapon = CS$<>8__locals1.Weapon;
						GameObject gameObject7 = ApparentTarget;
						GameObject gameObject8;
						bool flag9;
						bool flag10;
						bool flag11;
						bool flag12;
						cell5.FindSolidObjectForMissile(this, gameObject6, weapon, out gameObject8, out flag9, out flag10, out flag11, out flag12, null, null, gameObject7);
						if (gameObject8 != null)
						{
							gameObject = gameObject8;
							flag = true;
						}
						else if (flag9)
						{
							CS$<>8__locals1.Weapon.PlayCombatSoundTag("ImpactSound", null, null, 0.5f);
							flag2 = true;
							break;
						}
						if (flag12)
						{
							Phase = CS$<>8__locals1.Weapon.GetPhase();
						}
						if (flag11)
						{
							Cell cell6 = cell;
							bool flag13 = true;
							bool flag14 = false;
							bool flag15 = false;
							int num15 = Phase;
							GameObject gameObject9 = null;
							GameObject gameObject10 = null;
							GameObject gameObject11 = null;
							GameObject gameObject12 = null;
							List<GameObject> list3 = null;
							bool flag16 = true;
							bool flag17 = true;
							Predicate<GameObject> predicate2;
							if ((predicate2 = CS$<>8__locals1.<>9__3) == null)
							{
								predicate2 = (CS$<>8__locals1.<>9__3 = (GameObject o) => o != CS$<>8__locals1.<>4__this);
							}
							gameObject = cell6.GetCombatTarget(this, flag13, flag14, flag15, num15, gameObject9, gameObject10, gameObject11, gameObject12, list3, flag16, flag17, predicate2);
						}
					}
					bool flag18 = false;
					bool flag19 = false;
					if (@for != null && cell2 != cell)
					{
						bool flag20;
						bool flag21;
						if (!@for.ThrownWeaponFlexPhaseTraversal(this, gameObject, ApparentTarget, CS$<>8__locals1.Weapon, Phase, cell2, cell, out flag20, out flag21, false))
						{
							flag18 = true;
							break;
						}
						if (flag21)
						{
							Phase = CS$<>8__locals1.Weapon.GetPhase();
						}
						if (flag20)
						{
							Cell cell7 = cell;
							bool flag22 = true;
							bool flag23 = false;
							bool flag24 = false;
							int num16 = Phase;
							GameObject gameObject13 = null;
							GameObject gameObject14 = null;
							GameObject gameObject15 = null;
							GameObject gameObject16 = null;
							List<GameObject> list4 = null;
							bool flag25 = true;
							bool flag26 = true;
							Predicate<GameObject> predicate3;
							if ((predicate3 = CS$<>8__locals1.<>9__4) == null)
							{
								predicate3 = (CS$<>8__locals1.<>9__4 = (GameObject o) => o != CS$<>8__locals1.<>4__this);
							}
							gameObject = cell7.GetCombatTarget(this, flag22, flag23, flag24, num16, gameObject13, gameObject14, gameObject15, gameObject16, list4, flag25, flag26, predicate3);
						}
					}
					if (!MissileTraversingCellEvent.Check(CS$<>8__locals1.Weapon, cell, cell2.GetDirectionFromCell(cell, false), this, true))
					{
						flag18 = true;
						break;
					}
					if (!GameObject.Validate(ref CS$<>8__locals1.Weapon))
					{
						return true;
					}
					if (projectileMovingEvent != null)
					{
						projectileMovingEvent.Defender = gameObject;
						projectileMovingEvent.Cell = cell;
						projectileMovingEvent.PathIndex = i;
						using (List<GameObject>.Enumerator enumerator = objectsThatWantEvent.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (!enumerator.Current.HandleEvent(projectileMovingEvent))
								{
									flag18 = true;
									break;
								}
							}
						}
						if (projectileMovingEvent.HitOverride != null)
						{
							gameObject = projectileMovingEvent.HitOverride;
							projectileMovingEvent.HitOverride = null;
						}
						if (projectileMovingEvent.ActivateShowUninvolved)
						{
							flag19 = true;
						}
						if (projectileMovingEvent.RecheckPhase)
						{
							Phase = CS$<>8__locals1.Weapon.GetPhase();
							projectileMovingEvent.RecheckPhase = false;
						}
					}
					if (!GameObject.Validate(CS$<>8__locals1.Weapon))
					{
						CS$<>8__locals1.<PerformThrow>g__ExecuteJuice|0(ref CS$<>8__locals2);
						return true;
					}
					if (flag18)
					{
						break;
					}
					if ((flag || num2 > 0) && gameObject != null && gameObject != this)
					{
						bool flag27 = false;
						bool flag28 = false;
						if (DefenderMissileHitEvent.Check(null, this, gameObject, this, CS$<>8__locals1.Weapon, null, null, ApparentTarget, null, FireType.Normal, 0, 20, 20, gameObject.IsPlayer(), null, ref flag18, ref flag27, ref flag28) || flag)
						{
							GameObject weapon2 = CS$<>8__locals1.Weapon;
							if (weapon2 != null)
							{
								weapon2.PlayCombatSoundTag("ImpactSound", null, null, 0.5f);
							}
							if (gameObject != null)
							{
								gameObject.PlayCombatSoundTag("ImpactSound", null, null, 0.5f);
							}
							flag2 = true;
							GameObject weapon3 = CS$<>8__locals1.Weapon;
							if (weapon3 != null)
							{
								XRL.World.Parts.Physics physics = weapon3.Physics;
								if (physics != null)
								{
									physics.DidXToY("hit", gameObject, null, "!", null, null, null, null, false, false, false, false, false, this, null, null, false, false, false, false, false, gameObject);
								}
							}
							bool flag29 = gameObject != ApparentTarget;
							bool isCreature = gameObject.IsCreature;
							string blueprint = gameObject.Blueprint;
							WeaponUsageTracking.TrackThrownWeaponHit(this, CS$<>8__locals1.Weapon, isCreature, blueprint, flag29);
							num2 -= num6;
							ThrownWeapon part = CS$<>8__locals1.Weapon.GetPart<ThrownWeapon>();
							string text = ((part != null) ? part.Damage : null) ?? "1d2";
							int num17 = ((part != null) ? part.Penetration : 2);
							int num18 = ((part != null) ? part.PenetrationBonus : 0);
							int num19 = Math.Max(XRL.Rules.Stat.GetScoreModifier(num8), 1);
							bool flag30 = false;
							GetThrownWeaponPerformanceEvent.GetFor(CS$<>8__locals1.Weapon, ref text, ref num17, ref num18, ref num19, ref flag30, false, this, gameObject);
							int combatAV = Stats.GetCombatAV(gameObject);
							if (flag30)
							{
								num19 = combatAV;
								num17 = combatAV;
							}
							if (num18 != 0)
							{
								num17 += num18;
								num19 += num18;
							}
							int num20 = XRL.Rules.Stat.RollDamagePenetrations(combatAV, num19, num17);
							DieRoll cachedDieRoll = text.GetCachedDieRoll();
							for (int j = 0; j < num20; j++)
							{
								num3 += cachedDieRoll.Resolve();
							}
							if (num3 < 0)
							{
								num3 = 0;
							}
							Damage damage = new Damage(num3);
							damage.AddAttributes(((part != null) ? part.Attributes : null) ?? "Cudgel");
							damage.AddAttribute("Thrown");
							if (flag30)
							{
								damage.AddAttribute("Vorpal");
							}
							if (flag29)
							{
								damage.AddAttribute("Accidental");
							}
							Event event2 = Event.New("WeaponThrowHit", 0, 0, 0);
							event2.SetParameter("Damage", damage);
							event2.SetParameter("Penetrations", num20);
							event2.SetParameter("Owner", this);
							event2.SetParameter("Attacker", this);
							event2.SetParameter("Defender", gameObject);
							event2.SetParameter("Weapon", CS$<>8__locals1.Weapon);
							event2.SetParameter("Projectile", CS$<>8__locals1.Weapon);
							event2.SetParameter("Phase", Phase);
							event2.SetParameter("ApparentTarget", ApparentTarget);
							if (!CS$<>8__locals1.Weapon.FireEvent(event2))
							{
								break;
							}
							num20 = event2.GetIntParameter("Penetrations", 0);
							Event event3 = Event.New("TakeDamage", 0, 0, 0);
							event3.SetParameter("Damage", damage);
							event3.SetParameter("Penetrations", num20);
							event3.SetParameter("Owner", this);
							event3.SetParameter("Attacker", this);
							event3.SetParameter("Defender", gameObject);
							event3.SetParameter("Weapon", CS$<>8__locals1.Weapon);
							event3.SetParameter("Phase", Phase);
							event3.SetParameter("Projectile", CS$<>8__locals1.Weapon);
							event3.SetFlag("ShowForInanimate", true);
							if (flag19)
							{
								event3.SetFlag("ShowUninvolved", true);
							}
							if (gameObject.FireEvent(event3))
							{
								num20 = event3.GetIntParameter("Penetrations", 0);
								WeaponUsageTracking.TrackThrownWeaponDamage(this, CS$<>8__locals1.Weapon, isCreature, blueprint, flag29, damage);
								if (damage.Amount > 0 && Options.ShowMonsterHPHearts)
								{
									gameObject.ParticleBlip(gameObject.GetHPColor() + "\u0003", 10, 0L, false);
								}
								if (damage.Amount > 0 || !damage.SuppressionMessageDone)
								{
									damage.PlaySound(gameObject);
									if (this.IsPlayer())
									{
										MessageQueue.AddPlayerMessage(string.Concat(new string[]
										{
											"You hit ",
											(gameObject == this) ? this.itself : gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" with ",
											CS$<>8__locals1.Weapon.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" (x",
											num20.ToString(),
											") for ",
											damage.Amount.ToString(),
											" damage!"
										}), XRL.Rules.Stat.GetResultColor(num20), true);
									}
									else if (gameObject.IsPlayer())
									{
										MessageQueue.AddPlayerMessage(string.Concat(new string[]
										{
											this.Does("hit", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											" with ",
											CS$<>8__locals1.Weapon.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" (x",
											num20.ToString(),
											") for ",
											damage.Amount.ToString(),
											" damage!"
										}), ColorCoding.ConsequentialColor(null, gameObject), true);
									}
									else if (gameObject.IsVisible())
									{
										MessageQueue.AddPlayerMessage(string.Concat(new string[]
										{
											this.Does("hit", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											" ",
											(gameObject == this) ? this.itself : gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" with ",
											CS$<>8__locals1.Weapon.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" (x",
											num20.ToString(),
											") for ",
											damage.Amount.ToString(),
											" damage!"
										}), ColorCoding.ConsequentialColor(null, gameObject), true);
									}
								}
							}
							Event event4 = new Event("ThrownProjectileHit");
							event4.SetParameter("Damage", damage);
							event4.SetParameter("Penetrations", num20);
							event4.SetParameter("Owner", this);
							event4.SetParameter("Attacker", this);
							event4.SetParameter("Defender", gameObject);
							event4.SetParameter("Weapon", CS$<>8__locals1.Weapon);
							event4.SetParameter("Projectile", CS$<>8__locals1.Weapon);
							event4.SetParameter("Phase", Phase);
							event4.SetParameter("ApparentTarget", ApparentTarget);
							CS$<>8__locals1.Weapon.FireEvent(event4);
							if (this.IsPlayer())
							{
								Sidebar.CurrentTarget = gameObject;
								break;
							}
							break;
						}
					}
					if (!Options.UseParticleVFX && parentZone.IsActive() && cell.IsVisible())
					{
						XRLCore.Core.RenderBaseToBuffer(scrapBuffer);
						scrapBuffer.Goto(cell);
						RenderEvent renderEvent2 = CS$<>8__locals1.Weapon.RenderForUI(null, false);
						if (!renderEvent2.Tile.IsNullOrEmpty())
						{
							XRL.The.ParticleManager.AddTile(renderEvent2.Tile, renderEvent2.ColorString, renderEvent2.DetailColor, (float)cell.X, (float)cell.Y, 0f, 0f, 2, 0f, 0f, renderEvent2.HFlip, renderEvent2.VFlip, 0L);
							XRL.The.ParticleManager.Frame();
							XRL.The.ParticleManager.Render(scrapBuffer);
						}
						else
						{
							scrapBuffer.Write(renderEvent2.ColorString + renderEvent2.RenderString, true, false, false, null, -1);
						}
						XRLCore._Console.DrawBuffer(scrapBuffer, null, false);
						Thread.Sleep(25);
					}
					cell2 = cell;
					i++;
				}
				CS$<>8__locals1.<PerformThrow>g__ExecuteJuice|0(ref CS$<>8__locals2);
				if (!flag2)
				{
					CS$<>8__locals1.Weapon.PlayCombatSoundTag("ImpactSound", null, null, 0.5f);
					if (cell != null)
					{
						GameObject gameObject17 = (from o in cell.GetObjectsWithTagOrProperty("ImpactSound")
							where o != CS$<>8__locals1.Weapon
							select o).FirstOrDefault<GameObject>();
						if (gameObject17 != null)
						{
							gameObject17.PlayCombatSoundTag("ImpactSound", null, null, 0.5f);
						}
					}
					flag2 = true;
				}
				int num21 = EnergyCost ?? 1000;
				if (num21 > 0)
				{
					this.UseEnergy(num21, "Missile Combat Throw", null, null, false);
				}
				if (GameObject.Validate(ref CS$<>8__locals1.Weapon))
				{
					(cell ?? TargetCell).AddObject(CS$<>8__locals1.Weapon, false, false, false, false, false, true, true, null, "Thrown", null, null, null, null, null, null);
					CS$<>8__locals1.Weapon.WasThrown(this, ApparentTarget);
					MissileWeapon.CleanupProjectile(CS$<>8__locals1.Weapon);
				}
				flag31 = true;
			}
			finally
			{
				if (@for != null)
				{
					@for.ThrownWeaponFlexPhaseEnd(CS$<>8__locals1.Weapon);
				}
				if (GameObject.ThrowPathInUse && MPath == GameObject.ThrowPath)
				{
					GameObject.ThrowPath.Reset();
					GameObject.ThrowPathInUse = false;
				}
			}
			return flag31;
		}

		// Token: 0x0600605C RID: 24668 RVA: 0x001D4848 File Offset: 0x001D2A48
		public bool LimitToAquatic()
		{
			Brain brain = this.Brain;
			return brain != null && brain.LimitToAquatic();
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x001D485C File Offset: 0x001D2A5C
		public bool AutoMove(string Direction)
		{
			if (!this.IsPlayer() || !Options.InterruptHeldMovement)
			{
				return this.Move(Direction, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
			}
			AutoAct.Setting = "m";
			if (AutoAct.CheckHostileInterrupt(true))
			{
				AutoAct.Setting = "";
				return false;
			}
			Cell cellFromDirection = this.CurrentCell.GetCellFromDirection(Direction, true);
			string text;
			GameObject gameObject;
			Cell cell;
			bool flag;
			if ((cellFromDirection != null || Options.GetOption("OptionInterruptHeldMovementOnZoneTransition", "") == "Yes") && InterruptAutowalkEvent.Check(this, cellFromDirection, out text, out gameObject, out cell, out flag))
			{
				if (text == null && GameObject.Validate(ref gameObject))
				{
					text = "of " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " " + this.DescribeDirectionToward(gameObject, false, false);
				}
				string text2 = text;
				GameObject gameObject2 = gameObject;
				AutoAct.Interrupt(text2, cell, gameObject2, flag);
				AutoAct.Setting = "";
				return false;
			}
			if (GameObject.AutomoveInterruptTurn == XRL.The.Game.Turns || GameObject.AutomoveInterruptTurn == XRL.The.Game.Turns - 1L)
			{
				text = GameObject.AutomoveInterruptBecause;
				AutoAct.Setting = "";
				return false;
			}
			AutoAct.Setting = "";
			return this.Move(Direction, false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
		}

		// Token: 0x0600605E RID: 24670 RVA: 0x001D49D0 File Offset: 0x001D2BD0
		public bool Move(string Direction, out GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, bool AllowDashing = true, bool DoConfirmations = true, GameObject Dragging = null, GameObject Actor = null, bool NearestAvailable = false, int? EnergyCost = null, string Type = null, int? MoveSpeed = null, bool Peaceful = false, bool IgnoreMobility = false, GameObject ForceSwap = null, GameObject Ignore = null, int CallDepth = 0)
		{
			Blocking = null;
			CallDepth++;
			if (CallDepth > 80)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int num2 = 20;
			if (Actor == null)
			{
				Actor = Dragging ?? this;
			}
			int num3 = EnergyCost ?? ((Forced || (Actor != null && Actor != this)) ? 0 : 1000);
			int num4 = num3;
			int num5 = MoveSpeed ?? this.Stat("MoveSpeed", 0);
			if (this.CurrentCell == null)
			{
				return false;
			}
			Zone parentZone = this.CurrentCell.ParentZone;
			if (parentZone == null)
			{
				return false;
			}
			bool flag2 = parentZone.IsWorldMap();
			if (Actor != null && Actor != this && !this.CanBeInvoluntarilyMoved())
			{
				return false;
			}
			if (AllowDashing && !Forced && this.HasEffect<Dashing>() && !flag2)
			{
				num3 = 0;
				flag = true;
				num2 = 20;
				this.PlayWorldSound("Sounds/StatusEffects/sfx_ability_mutation_flamingRay_attack", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Cell cell;
			GameObject gameObject;
			GameObject gameObject3;
			GameObject gameObject4;
			char c;
			string text;
			GameObject gameObject5;
			for (;;)
			{
				cell = this.CurrentCell.GetCellFromDirection(Direction, false);
				if (cell != null && NearestAvailable && !cell.IsEmptyOfSolidFor(this, false))
				{
					List<Cell> list = cell.GetLocalAdjacentCells(1, false).ShuffleInPlace<Cell>(null);
					for (int i = 0; i < list.Count; i++)
					{
						Cell cell2 = list[i];
						if (cell2.IsAdjacentTo(this.CurrentCell, true) && cell2.IsEmptyOfSolidFor(this, false))
						{
							cell = cell2;
							break;
						}
					}
				}
				if (cell == null)
				{
					goto IL_0DC0;
				}
				if (this.IsPlayer() && !TutorialManager.BeforePlayerEnterCell(cell))
				{
					break;
				}
				gameObject = null;
				bool flag3 = !flag2 && (this.IsPlayer() || this.IsCombatObject() || this.ConsiderSolid());
				if (flag3)
				{
					gameObject = cell.GetCombatTarget(this, false, true, false, 0, null, null, null, Ignore, null, false, false, null);
				}
				GameObject gameObject2 = (Peaceful ? null : gameObject);
				bool flag4 = true;
				bool flag5 = false;
				bool flag6 = false;
				Brain brain = this.Brain;
				if (brain != null)
				{
					brain.CheckMobility(out flag4, out flag5, out flag6);
				}
				if (!Forced && gameObject2 == null && flag4 && !IgnoreMobility)
				{
					goto Block_33;
				}
				if (!Forced)
				{
					if (!this.CheckFrozen(false, false, false, null))
					{
						goto IL_0DD4;
					}
					if (this.HasEffect<Paralyzed>())
					{
						goto Block_36;
					}
				}
				if (!Forced && gameObject2 == null)
				{
					if ((!this.IsPlayer() || !XRLCore.Core.IDKFA) && this.IsOverburdened())
					{
						goto Block_40;
					}
					if (flag6 && !cell.HasWalkableWallFor(this))
					{
						goto Block_42;
					}
					if (flag5)
					{
						if (!cell.HasAquaticSupportFor(this))
						{
							goto Block_44;
						}
					}
					else if (DoConfirmations && this.IsPlayer() && !this.IsConfused)
					{
						if (AutoAct.IsActive(false))
						{
							gameObject3 = (Options.ConfirmDangerousLiquid ? cell.GetDangerousOpenLiquidVolume() : null);
							if (gameObject3 != null && gameObject3 != Ignore && gameObject3.PhaseAndFlightMatches(this) && Popup.WarnYesNo("Your path would take you into " + gameObject3.an(2147483647, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ". Are you sure you want to do this?", "Sounds/UI/ui_notification_warning", true, DialogResult.No) != DialogResult.Yes)
							{
								goto Block_53;
							}
						}
						else
						{
							gameObject4 = (Options.ConfirmSwimming ? cell.GetSwimmingDepthLiquid() : null);
							if (gameObject4 != null && gameObject4 != Ignore && !this.HasEffect<Swimming>() && !cell.HasBridge() && gameObject4.PhaseAndFlightMatches(this))
							{
								if (XRL.The.Core.MoveConfirmDirection != Direction)
								{
									bool flag7 = gameObject4.IsDangerousOpenLiquidVolume();
									c = (flag7 ? 'R' : 'W');
									text = string.Concat(new string[]
									{
										"There",
										gameObject4.Is,
										" ",
										flag7 ? ("a dangerous-looking " + gameObject4.ShortDisplayName) : gameObject4.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
										" that way."
									});
									if (!(Direction == "U") && !(Direction == "D"))
									{
										goto IL_04BB;
									}
									if (Popup.ShowYesNo(text + " Do you want to go " + Directions.GetIndicativeDirection(Direction) + " and start swimming?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
									{
										return false;
									}
								}
							}
							else if ((gameObject4 = (Options.ConfirmDangerousLiquid ? cell.GetDangerousOpenLiquidVolume() : null)) != null && gameObject4 != Ignore && gameObject4.PhaseAndFlightMatches(this))
							{
								if (XRL.The.Core.MoveConfirmDirection != Direction)
								{
									if (!(Direction == "U") && !(Direction == "D"))
									{
										goto IL_05C5;
									}
									if (Popup.WarnYesNo(string.Concat(new string[] { "There", gameObject4.Is, " a dangerous-looking ", gameObject4.ShortDisplayName, " that way. Are you sure you want to move into ", gameObject4.them, "?" }), "Sounds/UI/ui_notification_warning", true, DialogResult.No) != DialogResult.Yes)
									{
										return false;
									}
								}
							}
							else if (cell.ParentZone.HasZoneProperty("ConfirmPullDown"))
							{
								gameObject4 = cell.GetFirstObjectWithPropertyOrTag("Stairs");
								if (gameObject4 != null && gameObject4 != Ignore)
								{
									StairsDown part = gameObject4.GetPart<StairsDown>();
									if (part != null && part.PullDown && XRL.The.Core.MoveConfirmDirection != Direction && !part.IsLongFall() && part.IsValidForPullDown(this))
									{
										if (!(Direction == "U") && !(Direction == "D"))
										{
											goto IL_072E;
										}
										if (Popup.WarnYesNo(string.Concat(new string[]
										{
											"There",
											gameObject4.Is,
											" ",
											gameObject4.an(2147483647, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" that way. Are you sure you want to move there and drop down a level?"
										}), "Sounds/UI/ui_notification_warning", true, DialogResult.No) != DialogResult.Yes)
										{
											return false;
										}
									}
								}
							}
						}
					}
				}
				int j = 0;
				int count = cell.Objects.Count;
				while (j < count)
				{
					if (cell.Objects[j] != null && cell.Objects[j] != Ignore && cell.Objects[j].HasTagOrStringProperty("TerrainMovementEnergyCostMultiplier"))
					{
						try
						{
							float num6 = float.Parse(cell.Objects[j].GetTagOrStringProperty("TerrainMovementEnergyCostMultiplier", "1"));
							num3 += (int)((float)num4 * num6);
						}
						catch (Exception)
						{
							MetricsManager.LogError("Object " + cell.Objects[j].Blueprint + " had invalid TerrainMovementEnergyCostMultiplier.");
						}
					}
					j++;
				}
				bool flag8;
				if (!this.ProcessBeginMove(out flag8, cell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, null, null))
				{
					goto IL_0DD4;
				}
				if (flag8 && flag3)
				{
					gameObject = cell.GetCombatTarget(this, false, true, false, 0, null, null, null, Ignore, null, false, false, null);
					gameObject2 = (Peaceful ? null : gameObject);
				}
				if (gameObject2 == null && !Forced && this.HasPart<Digging>())
				{
					int k = 0;
					int count2 = cell.Objects.Count;
					while (k < count2)
					{
						gameObject5 = cell.Objects[k];
						if (gameObject5.IsWall() && Options.DigOnMove && gameObject5.ConsiderSolidFor(this, false) && OkayToDamageEvent.Check(gameObject5, this))
						{
							goto Block_95;
						}
						k++;
					}
				}
				if (gameObject != null)
				{
					bool flag9 = gameObject.IsHostileTowards(this);
					if (!Forced && ForceSwap == null && flag9)
					{
						goto Block_100;
					}
					if (flag9 && ForceSwap != gameObject)
					{
						goto Block_107;
					}
					if (gameObject.IsPlayer() || this.CurrentCell == null)
					{
						goto IL_0DD4;
					}
					if (ForceSwap != gameObject && !gameObject.CanBePositionSwapped(this))
					{
						goto Block_112;
					}
					if (ForceSwap != gameObject && gameObject.HasEffect<Stuck>())
					{
						goto Block_115;
					}
					GameObject gameObject6 = gameObject;
					string directionFromCell = cell.GetDirectionFromCell(this.CurrentCell, false);
					bool flag10 = true;
					bool flag11 = false;
					bool flag12 = false;
					bool flag13 = false;
					bool flag14 = false;
					bool flag15 = false;
					GameObject gameObject7 = null;
					GameObject gameObject8 = null;
					bool flag16 = false;
					int? num7 = new int?(0);
					string text2 = null;
					int num8 = CallDepth + 1;
					if (!gameObject6.Move(directionFromCell, flag10, flag11, flag12, flag13, flag14, flag15, gameObject7, gameObject8, flag16, num7, text2, null, true, false, null, this, num8))
					{
						goto Block_117;
					}
				}
				if (!this.ProcessObjectLeavingCell(this.CurrentCell, ref Blocking, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null) || !this.ProcessEnteringCell(cell, ref Blocking, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null) || !this.ProcessObjectEnteringCell(cell, ref Blocking, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null) || (this.CurrentCell.ParentZone != cell.ParentZone && !this.ProcessEnteringZone(this.CurrentCell, cell, ref Blocking, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null)) || !this.ProcessLeaveCell(this.CurrentCell, ref Blocking, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null))
				{
					goto IL_0DD4;
				}
				Cell currentCell = this.CurrentCell;
				if (this.CurrentCell != null)
				{
					this.CurrentCell.RemoveObject(this, Forced, System, IgnoreGravity, false, NoStack, true, true, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null);
				}
				cell.AddObject(this, Forced, System, IgnoreGravity, NoStack, false, true, true, Direction, Type, Dragging, Actor, ForceSwap, Ignore, null, null);
				num++;
				if (this.IsPlayer())
				{
					XRL.The.Core.MoveConfirmDirection = null;
				}
				if (num3 > 0)
				{
					this.UseEnergy(num3, "Movement", null, new int?(num5), false);
				}
				Event @event = Event.New("AfterMoved", "FromCell", currentCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				this.FireEvent(@event);
				if (parentZone != cell.ParentZone)
				{
					if (this.IsPlayer())
					{
						XRL.The.ZoneManager.SetActiveZone(cell.ParentZone);
					}
					XRL.The.ZoneManager.ProcessGoToPartyLeader();
				}
				if (flag)
				{
					num2--;
					this.Smoke();
					int num9 = XRL.Rules.Stat.Random(0, 10);
					if (num9 == 0)
					{
						this.ParticleBlip("&R*", 10, 0L, false);
					}
					if (num9 == 1)
					{
						this.ParticleBlip("&Y*", 10, 0L, false);
					}
					if (num9 == 2)
					{
						this.ParticleBlip("&r*", 10, 0L, false);
					}
					if (num9 == 3)
					{
						this.ParticleBlip("&W*", 10, 0L, false);
					}
					if (num9 == 4)
					{
						this.ParticleBlip("&Rú", 10, 0L, false);
					}
					if (num9 == 5)
					{
						this.ParticleBlip("&Yú", 10, 0L, false);
					}
					if (num9 == 6)
					{
						this.ParticleBlip("&rú", 10, 0L, false);
					}
					if (num9 == 7)
					{
						this.ParticleBlip("&Wú", 10, 0L, false);
					}
					if (num9 == 8)
					{
						this.ParticleBlip("&Rù", 10, 0L, false);
					}
					if (num9 == 9)
					{
						this.ParticleBlip("&Yù", 10, 0L, false);
					}
					if (num9 == 10)
					{
						this.ParticleBlip("&rù", 10, 0L, false);
					}
				}
				if (flag && this.IsPlayer())
				{
					ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
					TextConsole textConsole = Popup._TextConsole;
					XRLCore.Core.RenderBaseToBuffer(scrapBuffer);
					textConsole.DrawBuffer(scrapBuffer, null, false);
					XRLCore.ParticleManager.Frame();
					XRLCore.Core.Game.ZoneManager.Tick(true);
				}
				if (!flag || num2 <= 0)
				{
					goto IL_0F5D;
				}
			}
			return false;
			Block_33:
			this.Fail("You are not a mobile creature.");
			goto IL_0DD4;
			Block_36:
			this.Fail("You are paralyzed!");
			goto IL_0DD4;
			Block_40:
			this.StopMoving();
			this.Fail("You are carrying too much to move!");
			goto IL_0DD4;
			Block_42:
			this.Fail("You are a wall-dwelling creature and may only move onto walls.");
			goto IL_0DD4;
			Block_44:
			this.Fail("You are an aquatic creature and may not move onto land!");
			goto IL_0DD4;
			Block_53:
			AutoAct.Interrupt("you chose not to enter " + gameObject3.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false), null, gameObject3, true);
			return false;
			IL_04BB:
			MessageQueue.AddPlayerMessage(string.Concat(new string[]
			{
				text,
				" Move ",
				Directions.GetExpandedDirection(Direction),
				" again to enter ",
				gameObject4.them,
				" and start swimming."
			}), c, true);
			XRL.The.Core.MoveConfirmDirection = Direction;
			return false;
			IL_05C5:
			MessageQueue.AddPlayerMessage(string.Concat(new string[]
			{
				"Are you sure you want to move into ",
				gameObject4.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
				"? Move ",
				Directions.GetExpandedDirection(Direction),
				" again to confirm."
			}), 'R', true);
			XRL.The.Core.MoveConfirmDirection = Direction;
			return false;
			IL_072E:
			MessageQueue.AddPlayerMessage("Are you sure you want to drop down a level? Move " + Directions.GetExpandedDirection(Direction) + " again to confirm.", 'R', true);
			XRL.The.Core.MoveConfirmDirection = Direction;
			return false;
			Block_95:
			if (Peaceful)
			{
				Blocking = gameObject5;
				goto IL_0DD4;
			}
			if (flag)
			{
				int num10 = Math.Min(num / 3, 20);
				int num11 = Math.Min(num / 3, 20);
				GameObject gameObject9 = gameObject5;
				string text3 = null;
				int num8 = num10;
				int num12 = num10;
				Combat.AttackObject(this, gameObject9, text3, num11, num8, num12, false);
				goto IL_0DD4;
			}
			Combat.AttackObject(this, gameObject5, null, 0, 0, 0, false);
			goto IL_0DD4;
			Block_100:
			if (!this.IsPlayer())
			{
				goto IL_0DD4;
			}
			if (AutoAct.IsActive(false))
			{
				AutoAct.Interrupt(gameObject.HasProperName ? (gameObject.does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " in your way") : string.Concat(new string[]
				{
					"there",
					gameObject.Is,
					" ",
					gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					" in your way"
				}), null, gameObject, true);
				goto IL_0DD4;
			}
			if (Peaceful)
			{
				goto IL_0DD4;
			}
			if (flag)
			{
				int num13 = num / 3 * 2;
				int num14 = num / 3 * 2;
				GameObject gameObject10 = gameObject;
				string text4 = null;
				int num12 = num13;
				int num8 = num13;
				Combat.AttackObject(this, gameObject10, text4, num14, num12, num8, false);
				goto IL_0DD4;
			}
			Combat.AttackObject(this, gameObject, null, 0, 0, 0, false);
			goto IL_0DD4;
			Block_107:
			if (this.IsPlayer())
			{
				MessageQueue.AddPlayerMessage("You are stopped short by " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
				goto IL_0DD4;
			}
			goto IL_0DD4;
			Block_112:
			if (this.IsPlayer())
			{
				MessageQueue.AddPlayerMessage(gameObject.Does("cannot", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " be moved.", null, true);
				goto IL_0DD4;
			}
			goto IL_0DD4;
			Block_115:
			if (this.IsPlayer())
			{
				MessageQueue.AddPlayerMessage(gameObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " stuck.", null, true);
				goto IL_0DD4;
			}
			goto IL_0DD4;
			Block_117:
			if (this.IsPlayer())
			{
				MessageQueue.AddPlayerMessage("You can't budge " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, true);
				goto IL_0DD4;
			}
			goto IL_0DD4;
			IL_0DC0:
			if (this.IsPlayer())
			{
				MessageQueue.AddPlayerMessage("You cannot go that way.", null, true);
			}
			IL_0DD4:
			AfterMoveFailedEvent.Send(this, this.CurrentCell, cell, Forced, System, IgnoreGravity, NoStack, null, Type, null, null, null, Ignore);
			if (flag)
			{
				this.UseEnergy(1000, "Movement", null, null, false);
			}
			return false;
			IL_0F5D:
			if (flag)
			{
				this.UseEnergy(1000, "Movement", null, null, false);
			}
			if (this.IsPlayer())
			{
				Zone currentZone = this.CurrentZone;
				if (currentZone != null)
				{
					currentZone.SetActive();
				}
			}
			return true;
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x001D5984 File Offset: 0x001D3B84
		public bool Move(string Direction, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, bool AllowDashing = true, bool DoConfirmations = true, GameObject Dragging = null, GameObject Actor = null, bool NearestAvailable = false, int? EnergyCost = null, string Type = null, int? MoveSpeed = null, bool Peaceful = false, bool IgnoreMobility = false, GameObject ForceSwap = null, GameObject Ignore = null, int CallDepth = 0)
		{
			GameObject gameObject;
			return this.Move(Direction, out gameObject, Forced, System, IgnoreGravity, NoStack, AllowDashing, DoConfirmations, Dragging, Actor, NearestAvailable, EnergyCost, Type, MoveSpeed, Peaceful, IgnoreMobility, ForceSwap, Ignore, CallDepth + 1);
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x001D59BC File Offset: 0x001D3BBC
		public bool Push(string Direction, int Force, int MaxDistance = 9999999, bool IgnoreGravity = false, bool Involuntary = true)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.Push(Direction, Force, MaxDistance, IgnoreGravity, Involuntary, null);
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x001D59D8 File Offset: 0x001D3BD8
		public int Accelerate(int Force, string Direction = null, Cell Toward = null, Cell AwayFrom = null, string Type = null, GameObject Actor = null, bool Accidental = false, GameObject IntendedTarget = null, string BonusDamage = null, double DamageFactor = 1.0, bool SuspendFalling = true, bool OneShort = false, bool Repeat = false, bool BuiltOnly = true, bool MessageForInanimate = true, bool DelayForDisplay = true)
		{
			if (this.Physics == null)
			{
				return 0;
			}
			return this.Physics.Accelerate(Force, Direction, Toward, AwayFrom, Type, Actor, Accidental, IntendedTarget, BonusDamage, DamageFactor, SuspendFalling, OneShort, Repeat, BuiltOnly, MessageForInanimate, DelayForDisplay);
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x001D5A18 File Offset: 0x001D3C18
		public bool TemperatureChange(int Amount, GameObject Actor = null, bool Radiant = false, bool MinAmbient = false, bool MaxAmbient = false, bool IgnoreResistance = false, int Phase = 0, int? Min = null, int? Max = null)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.ProcessTemperatureChange(Amount, Actor, Radiant, MinAmbient, MaxAmbient, IgnoreResistance, Phase, Min, Max);
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x001D5A48 File Offset: 0x001D3C48
		private void ProcessMoveEvent(Event E, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null)
		{
			if (Forced)
			{
				E.SetFlag("Forced", true);
			}
			if (System)
			{
				E.SetFlag("System", true);
			}
			if (IgnoreGravity)
			{
				E.SetFlag("IgnoreGravity", true);
			}
			if (NoStack)
			{
				E.SetFlag("NoStack", true);
			}
			if (Direction != null)
			{
				E.SetParameter("Direction", Direction);
			}
			if (Type != null)
			{
				E.SetParameter("Type", Type);
				E.SetParameter(Type, 1);
			}
			if (Dragging != null)
			{
				E.SetParameter("Dragging", Dragging);
			}
			if (Actor != null)
			{
				E.SetParameter("Actor", Actor);
			}
			if (ForceSwap != null)
			{
				E.SetParameter("ForceSwap", ForceSwap);
			}
			if (Ignore != null)
			{
				E.SetParameter("Ignore", Ignore);
			}
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x001D5B10 File Offset: 0x001D3D10
		public bool ProcessBeginMove(out bool ReobtainCombatants, Cell DestinationCell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			ReobtainCombatants = false;
			if (DestinationCell == null)
			{
				return false;
			}
			if (this.HasRegisteredEvent("BeginMove"))
			{
				ReobtainCombatants = true;
				Event @event = Event.New("BeginMove", "DestinationCell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			if (this.HasRegisteredEvent("BeginMoveLate"))
			{
				ReobtainCombatants = true;
				Event event2 = Event.New("BeginMoveLate", "DestinationCell", DestinationCell);
				this.ProcessMoveEvent(event2, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(event2, ParentEvent) && !System)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x001D5BC0 File Offset: 0x001D3DC0
		public bool ProcessObjectLeavingCell(Cell CC, ref GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (CC == null)
			{
				return true;
			}
			if (CC.HasObjectWithRegisteredEvent("ObjectLeavingCell"))
			{
				Event @event = Event.New("ObjectLeavingCell", "Object", this);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!CC.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			if (CC.WantEvent(ObjectLeavingCellEvent.ID, MinEvent.CascadeLevel))
			{
				ObjectLeavingCellEvent objectLeavingCellEvent = ObjectLeavingCellEvent.FromPool(this, CC, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				bool flag = CC.HandleEvent(objectLeavingCellEvent, ParentEvent);
				if (Blocking == null)
				{
					Blocking = objectLeavingCellEvent.Blocking;
				}
				if (!flag && !System)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x001D5C68 File Offset: 0x001D3E68
		public bool ProcessEnteringCell(Cell DestinationCell, ref GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (this.HasRegisteredEvent("EnteringCell"))
			{
				Event @event = Event.New("EnteringCell", "Cell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			if (this.WantEvent(EnteringCellEvent.ID, MinEvent.CascadeLevel))
			{
				EnteringCellEvent enteringCellEvent = EnteringCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				bool flag = this.HandleEvent<EnteringCellEvent>(enteringCellEvent, ParentEvent);
				if (Blocking == null)
				{
					Blocking = enteringCellEvent.Blocking;
				}
				if (!flag && !System)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x001D5D0C File Offset: 0x001D3F0C
		public bool ProcessObjectEnteringCell(Cell DestinationCell, ref GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (DestinationCell.HasObjectWithRegisteredEvent("ObjectEnteringCell"))
			{
				Event @event = Event.New("ObjectEnteringCell", "Object", this);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!DestinationCell.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			if (DestinationCell.WantEvent(ObjectEnteringCellEvent.ID, MinEvent.CascadeLevel))
			{
				ObjectEnteringCellEvent objectEnteringCellEvent = ObjectEnteringCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				bool flag = DestinationCell.HandleEvent(objectEnteringCellEvent, ParentEvent);
				if (Blocking == null)
				{
					Blocking = objectEnteringCellEvent.Blocking;
				}
				if (!flag && !System)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x001D5DB0 File Offset: 0x001D3FB0
		public bool ProcessEnteringZone(Cell CurrentCell, Cell DestinationCell, ref GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (this.HasRegisteredEvent("EnteringZone"))
			{
				Event @event = Event.New("EnteringZone", "Origin", CurrentCell, "Cell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent))
				{
					return false;
				}
			}
			bool flag = this.WantEvent(EnteringZoneEvent.ID, EnteringZoneEvent.CascadeLevel);
			bool flag2 = DestinationCell.ParentZone.WantEvent(EnteringZoneEvent.ID, EnteringZoneEvent.CascadeLevel);
			if (flag || flag2)
			{
				EnteringZoneEvent enteringZoneEvent = EnteringZoneEvent.FromPool(this, CurrentCell, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				bool flag3 = true;
				if (flag)
				{
					flag3 = this.HandleEvent<EnteringZoneEvent>(enteringZoneEvent, ParentEvent);
				}
				if (flag2 && flag3)
				{
					flag3 = DestinationCell.ParentZone.HandleEvent<EnteringZoneEvent>(enteringZoneEvent, ParentEvent);
				}
				if (Blocking == null)
				{
					Blocking = enteringZoneEvent.Blocking;
				}
				if (!flag3)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x001D5E8C File Offset: 0x001D408C
		public bool ProcessLeaveCell(Cell CC, ref GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (CC == null)
			{
				return true;
			}
			if (this.HasRegisteredEvent("LeaveCell"))
			{
				Event @event = Event.New("LeaveCell", "Cell", CC);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			if (this.WantEvent(LeaveCellEvent.ID, MinEvent.CascadeLevel))
			{
				LeaveCellEvent leaveCellEvent = LeaveCellEvent.FromPool(this, CC, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				bool flag = this.HandleEvent<LeaveCellEvent>(leaveCellEvent, ParentEvent);
				if (Blocking == null)
				{
					Blocking = leaveCellEvent.Blocking;
				}
				if (!flag && !System)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x001D5F34 File Offset: 0x001D4134
		public bool ProcessLeavingCell(Cell DestinationCell, ref GameObject Blocking, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (this.HasRegisteredEvent("LeavingCell"))
			{
				Event @event = Event.New("LeavingCell", "Cell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			if (this.WantEvent(LeavingCellEvent.ID, MinEvent.CascadeLevel))
			{
				LeavingCellEvent leavingCellEvent = LeavingCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				bool flag = this.HandleEvent<LeavingCellEvent>(leavingCellEvent, ParentEvent);
				if (Blocking == null)
				{
					Blocking = leavingCellEvent.Blocking;
				}
				if (!flag && !System)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x001D5FD8 File Offset: 0x001D41D8
		public bool ProcessLeftCell(Cell DestinationCell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (this.HasRegisteredEvent("LeftCell"))
			{
				Event @event = Event.New("LeftCell", "Cell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			return !this.WantEvent(LeftCellEvent.ID, MinEvent.CascadeLevel) || this.HandleEvent<LeftCellEvent>(LeftCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore), ParentEvent) || System;
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x001D6068 File Offset: 0x001D4268
		public bool ProcessEnterCell(Cell DestinationCell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (this.HasRegisteredEvent("EnterCell"))
			{
				Event @event = Event.New("EnterCell", "Cell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			return !this.WantEvent(EnterCellEvent.ID, MinEvent.CascadeLevel) || this.HandleEvent<EnterCellEvent>(EnterCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore), ParentEvent) || System;
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x001D60F8 File Offset: 0x001D42F8
		public bool ProcessEnteredCell(Cell DestinationCell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (this.HasRegisteredEvent("EnteredCell"))
			{
				Event @event = Event.New("EnteredCell", "Cell", DestinationCell);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!this.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			return (!this.IsPlayer() && !this.WantEvent(EnteredCellEvent.ID, MinEvent.CascadeLevel)) || this.HandleEvent(EnteredCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore)) || System;
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x001D6190 File Offset: 0x001D4390
		public bool ProcessObjectEnteredCell(Cell DestinationCell, bool Forced = false, bool System = false, bool IgnoreGravity = false, bool NoStack = false, string Direction = null, string Type = null, GameObject Dragging = null, GameObject Actor = null, GameObject ForceSwap = null, GameObject Ignore = null, IEvent ParentEvent = null)
		{
			if (DestinationCell.HasObjectWithRegisteredEvent("ObjectEnteredCell"))
			{
				Event @event = Event.New("ObjectEnteredCell", "Object", this);
				this.ProcessMoveEvent(@event, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore);
				if (!DestinationCell.FireEvent(@event, ParentEvent) && !System)
				{
					return false;
				}
			}
			return !DestinationCell.WantEvent(ObjectEnteredCellEvent.ID, MinEvent.CascadeLevel) || DestinationCell.HandleEvent(ObjectEnteredCellEvent.FromPool(this, DestinationCell, Forced, System, IgnoreGravity, NoStack, Direction, Type, Dragging, Actor, ForceSwap, Ignore), ParentEvent) || System;
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x001D621F File Offset: 0x001D441F
		public GameObject SplitStack(int Count, GameObject OwningObject = null, bool NoRemove = false)
		{
			if (this.Stacker != null)
			{
				return this.Stacker.SplitStack(Count, OwningObject, NoRemove);
			}
			return null;
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x001D623C File Offset: 0x001D443C
		private void AutoEquipFail(GameObject obj, bool Silent = false, Cell WasInCell = null, GameObject WasInInventory = null, Event E = null, List<GameObject> WasUnequipped = null)
		{
			if (this.IsPlayer() && !Silent)
			{
				string text = null;
				if (E != null)
				{
					text = E.GetStringParameter("FailureMessage", null);
					if (WasUnequipped == null)
					{
						WasUnequipped = E.GetParameter("WasUnequipped") as List<GameObject>;
					}
				}
				if (E == null || !E.HasFlag("OwnershipViolationDeclined"))
				{
					if (text == null)
					{
						text = "";
					}
					else if (text != "")
					{
						text += " ";
					}
					text = text + "You can't auto-equip " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".";
				}
				string text2 = this.DescribeUnequip(WasUnequipped);
				if (!text2.IsNullOrEmpty())
				{
					if (text == null)
					{
						text = "";
					}
					else if (text != "")
					{
						text += "\n\n";
					}
					text += text2;
				}
				if (!text.IsNullOrEmpty())
				{
					Popup.ShowFail(text, true, true, true);
				}
			}
			if (WasInCell != null)
			{
				if (GameObject.Validate(ref obj) && obj.CurrentCell != WasInCell)
				{
					obj.RemoveFromContext(null);
					WasInCell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					return;
				}
			}
			else if (((WasInInventory != null) ? WasInInventory.Inventory : null) != null && GameObject.Validate(ref obj) && obj.InInventory != WasInInventory)
			{
				obj.RemoveFromContext(null);
				WasInInventory.Inventory.AddObject(obj, null, false, false, true, null);
			}
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x001D63B0 File Offset: 0x001D45B0
		private bool AutoEquipSucceed(bool Silent, List<GameObject> WasUnequipped)
		{
			if (!this.IsPlayer())
			{
				return true;
			}
			if (Silent)
			{
				return true;
			}
			string text = this.DescribeUnequip(WasUnequipped);
			if (!text.IsNullOrEmpty())
			{
				Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return true;
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x001D63F0 File Offset: 0x001D45F0
		public string DescribeUnequip(List<GameObject> WasUnequipped)
		{
			if (WasUnequipped == null || WasUnequipped.Count == 0)
			{
				return null;
			}
			WasUnequipped.Sort((GameObject a, GameObject b) => a.HasProperName.CompareTo(b.HasProperName));
			List<string> list = new List<string>(WasUnequipped.Count);
			bool flag = false;
			foreach (GameObject gameObject in WasUnequipped)
			{
				if (gameObject.IsValid())
				{
					list.Add(gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false));
					if (gameObject.IsPlural)
					{
						flag = true;
					}
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return Grammar.MakeAndList(list, true).Capitalize() + " " + ((flag || list.Count > 1) ? "were" : "was") + " unequipped.";
		}

		// Token: 0x06006073 RID: 24691 RVA: 0x001D64F4 File Offset: 0x001D46F4
		public BodyPart getComparisonBodypart(GameObject GO)
		{
			if (!GameObject.Validate(ref GO))
			{
				return null;
			}
			if (GO.HasPropertyOrTag("CannotEquip"))
			{
				return null;
			}
			string inventoryCategory = GO.GetInventoryCategory(false);
			if (GO.IsThrownWeapon && !GO.HasPropertyOrTag("NoAIEquipAsThrownWeapon") && GO.Understood())
			{
				Body body = this.Body;
				return body.GetFirstPart("Thrown Weapon", (BodyPart p) => p.Equipped == null) ?? body.GetFirstPart("Thrown Weapon");
			}
			if (inventoryCategory == "Shields")
			{
				XRL.World.Parts.Shield part = GO.GetPart<XRL.World.Parts.Shield>();
				List<BodyPart> part2 = this.Body.GetPart(part.WornOn);
				foreach (BodyPart bodyPart in part2)
				{
					if (bodyPart.Equipped != null && bodyPart.Equipped.HasPart<XRL.World.Parts.Shield>())
					{
						return bodyPart;
					}
				}
				if (part2.Count > 0)
				{
					return part2[0];
				}
			}
			else
			{
				if (inventoryCategory == "Armor" || inventoryCategory == "Clothes")
				{
					Armor part3 = GO.GetPart<Armor>();
					Body body2 = this.Body;
					if (body2 == null)
					{
						goto IL_03F1;
					}
					foreach (BodyPart bodyPart2 in ((part3.WornOn == "*") ? body2.LoopParts() : body2.LoopPart(part3.WornOn)))
					{
						if (bodyPart2.Equipped == null)
						{
							return bodyPart2;
						}
					}
					using (IEnumerator<BodyPart> enumerator2 = ((part3.WornOn == "*") ? body2.LoopParts() : body2.LoopPart(part3.WornOn)).GetEnumerator())
					{
						if (!enumerator2.MoveNext())
						{
							goto IL_03F1;
						}
						return enumerator2.Current;
					}
				}
				if (inventoryCategory == "Missile Weapons")
				{
					MissileWeapon missileWeapon;
					if (!GO.TryGetPart<MissileWeapon>(out missileWeapon))
					{
						return null;
					}
					Body body3 = this.Body;
					if (body3 == null)
					{
						return null;
					}
					List<BodyPart> part4 = body3.GetPart(missileWeapon.GetSlotType(false));
					foreach (BodyPart bodyPart3 in part4)
					{
						if (bodyPart3.Equipped == null)
						{
							return bodyPart3;
						}
					}
					using (List<BodyPart>.Enumerator enumerator = part4.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							return enumerator.Current;
						}
					}
					return null;
				}
				else
				{
					if (inventoryCategory == "Ammo")
					{
						return null;
					}
					if (inventoryCategory == "Melee Weapons")
					{
						MeleeWeapon part5 = GO.GetPart<MeleeWeapon>();
						List<BodyPart> part6 = this.Body.GetPart(part5.Slot);
						if (part6.Count <= 0)
						{
							goto IL_03F1;
						}
						BodyPart bodyPart4 = null;
						using (List<BodyPart>.Enumerator enumerator = part6.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								BodyPart bodyPart5 = enumerator.Current;
								if (bodyPart5.Primary)
								{
									if (bodyPart4 == null)
									{
										bodyPart4 = bodyPart5;
									}
									if (bodyPart5.Equipped != null)
									{
										return bodyPart5;
									}
								}
							}
							goto IL_03F1;
						}
					}
					Body body4 = this.Body;
					List<BodyPart> list = body4.GetPart("Hand");
					if (GO.HasPart<Armor>())
					{
						Armor part7 = GO.GetPart<Armor>();
						if (part7.WornOn != "Body")
						{
							if (part7.WornOn == "*")
							{
								list = body4.GetParts();
							}
							else
							{
								list = body4.GetPart(part7.WornOn);
							}
						}
					}
					foreach (BodyPart bodyPart6 in list)
					{
						if (!bodyPart6.Primary && bodyPart6.Equipped != null)
						{
							return bodyPart6;
						}
					}
					return null;
				}
			}
			IL_03F1:
			return null;
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x001D6950 File Offset: 0x001D4B50
		public bool AutoEquip(GameObject GO, bool Forced = false, bool ForceHeld = false, bool Silent = false)
		{
			if (!GameObject.Validate(ref GO))
			{
				this.AutoEquipFail(GO, Silent, null, null, null, null);
				return false;
			}
			if (!Forced && GO.HasPropertyOrTag("CannotEquip"))
			{
				this.AutoEquipFail(GO, Silent, null, null, null, null);
				return false;
			}
			Cell currentCell = GO.CurrentCell;
			GameObject inInventory = GO.InInventory;
			if (inInventory != this && GO.Equipped != this)
			{
				GO.SplitFromStack();
				if (!this.ReceiveObject(GO, true, null))
				{
					GO.CheckStack();
					return false;
				}
			}
			List<GameObject> list = Event.NewGameObjectList();
			string text = (ForceHeld ? "Melee Weapons" : GO.GetInventoryCategory(false));
			if (ForceHeld || !GO.IsThrownWeapon || GO.HasPropertyOrTag("NoAIEquipAsThrownWeapon") || !GO.Understood())
			{
				if (text == "Shields")
				{
					XRL.World.Parts.Shield part = GO.GetPart<XRL.World.Parts.Shield>();
					Event @event = Event.New("CommandEquipObject", "Object", GO);
					@event.SetParameter("WasUnequipped", list);
					if (Silent)
					{
						@event.SetSilent(true);
					}
					List<BodyPart> part2 = this.Body.GetPart(part.WornOn);
					int num = 0;
					foreach (BodyPart bodyPart in part2)
					{
						if (bodyPart.Equipped != null && bodyPart.Equipped.HasPart<XRL.World.Parts.Shield>())
						{
							@event.SetParameter("BodyPart", bodyPart);
							@event.SetParameter("AutoEquipTry", ++num);
							if (this.FireEvent(@event))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
					}
					if (part2.Count > 0)
					{
						@event.SetParameter("BodyPart", part2[0]);
						@event.SetParameter("AutoEquipTry", ++num);
						if (this.FireEvent(@event))
						{
							return this.AutoEquipSucceed(Silent, list);
						}
					}
					this.AutoEquipFail(GO, Silent, currentCell, inInventory, @event, null);
					return false;
				}
				if (text == "Armor" || text == "Clothes")
				{
					Armor part3 = GO.GetPart<Armor>();
					Event event2 = Event.New("CommandEquipObject", "Object", GO);
					event2.SetParameter("WasUnequipped", list);
					if (Silent)
					{
						event2.SetSilent(true);
					}
					int num2 = 0;
					Body body = this.Body;
					if (body != null)
					{
						foreach (BodyPart bodyPart2 in ((part3.WornOn == "*") ? body.LoopParts() : body.LoopPart(part3.WornOn)))
						{
							if (bodyPart2.Equipped == null)
							{
								event2.SetParameter("BodyPart", bodyPart2);
								event2.SetParameter("AutoEquipTry", ++num2);
								if (this.FireEvent(event2))
								{
									return this.AutoEquipSucceed(Silent, list);
								}
							}
						}
						foreach (BodyPart bodyPart3 in ((part3.WornOn == "*") ? body.LoopParts() : body.LoopPart(part3.WornOn)))
						{
							if (bodyPart3.Equipped != null)
							{
								event2.SetParameter("BodyPart", bodyPart3);
								event2.SetParameter("AutoEquipTry", ++num2);
								if (this.FireEvent(event2))
								{
									return this.AutoEquipSucceed(Silent, list);
								}
							}
						}
					}
					this.AutoEquipFail(GO, Silent, currentCell, inInventory, event2, null);
					return false;
				}
				if (text == "Missile Weapons")
				{
					Event event3 = Event.New("CommandEquipObject", "Object", GO);
					event3.SetParameter("WasUnequipped", list);
					if (Silent)
					{
						event3.SetSilent(true);
					}
					MissileWeapon missileWeapon;
					if (!GO.TryGetPart<MissileWeapon>(out missileWeapon))
					{
						MetricsManager.LogError("Item for missile weapon auto-equip had no MissileWeapon part: " + GO.DebugName);
						this.AutoEquipFail(GO, Silent, currentCell, inInventory, event3, null);
						return false;
					}
					Body body2 = this.Body;
					if (body2 == null)
					{
						MetricsManager.LogError("Creature trying to equip missile weapon had no body: " + this.DebugName);
						this.AutoEquipFail(GO, Silent, currentCell, inInventory, event3, null);
						return false;
					}
					List<BodyPart> part4 = body2.GetPart(missileWeapon.GetSlotType(false));
					int num3 = 0;
					foreach (BodyPart bodyPart4 in part4)
					{
						if (bodyPart4.Equipped == null)
						{
							event3.SetParameter("BodyPart", bodyPart4);
							event3.SetParameter("AutoEquipTry", ++num3);
							if (this.FireEvent(event3))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
					}
					foreach (BodyPart bodyPart5 in part4)
					{
						event3.SetParameter("BodyPart", bodyPart5);
						event3.SetParameter("AutoEquipTry", ++num3);
						if (this.FireEvent(event3))
						{
							return this.AutoEquipSucceed(Silent, list);
						}
					}
					if (part4.Count > 0)
					{
						event3.SetParameter("BodyPart", part4[0]);
						event3.SetParameter("AutoEquipTry", ++num3);
						if (this.FireEvent(event3))
						{
							return this.AutoEquipSucceed(Silent, list);
						}
					}
					this.AutoEquipFail(GO, Silent, currentCell, inInventory, event3, null);
					return false;
				}
				else if (text == "Ammo")
				{
					Event event4 = Event.New("CommandEquipObject", "Object", GO);
					event4.SetParameter("WasUnequipped", list);
					if (Silent)
					{
						event4.SetSilent(true);
					}
					List<GameObject> missileWeapons = this.GetMissileWeapons(null, null);
					if (missileWeapons == null || missileWeapons.Count == 0)
					{
						if (!Silent && this.IsPlayer())
						{
							Popup.ShowFail("You don't have a missile weapon equipped that uses that ammunition.", true, true, true);
						}
						return false;
					}
					foreach (GameObject gameObject in missileWeapons)
					{
						MagazineAmmoLoader part5 = gameObject.GetPart<MagazineAmmoLoader>();
						if (part5 != null && (GO.HasPart(part5.AmmoPart) || GO.HasTag(part5.AmmoPart)))
						{
							part5.Unload(this);
							part5.Load(this, GO, false);
							this.UseEnergy(part5.ReloadEnergy, "Reload", null, null, false);
							return this.AutoEquipSucceed(Silent, list);
						}
					}
					if (!Silent && this.IsPlayer())
					{
						Popup.ShowFail("You don't have a missile weapon equipped that uses that ammunition.", true, true, true);
						return false;
					}
					return false;
				}
				else
				{
					if (text == "Melee Weapons")
					{
						MeleeWeapon part6 = GO.GetPart<MeleeWeapon>();
						Event event5 = Event.New("CommandEquipObject", "Object", GO);
						event5.SetParameter("WasUnequipped", list);
						if (Silent)
						{
							event5.SetSilent(true);
						}
						List<BodyPart> part7 = this.Body.GetPart(part6.Slot);
						if (part7.Count > 0)
						{
							int num4 = 0;
							BodyPart bodyPart6 = null;
							foreach (BodyPart bodyPart7 in part7)
							{
								if (bodyPart7.Primary)
								{
									if (bodyPart6 == null)
									{
										bodyPart6 = bodyPart7;
									}
									if (bodyPart7.Equipped == null)
									{
										event5.SetParameter("BodyPart", bodyPart7);
										event5.SetParameter("AutoEquipTry", ++num4);
										if (this.FireEvent(event5))
										{
											return this.AutoEquipSucceed(Silent, list);
										}
									}
								}
							}
							foreach (BodyPart bodyPart8 in part7)
							{
								if (bodyPart8.Equipped == null)
								{
									event5.SetParameter("BodyPart", bodyPart8);
									event5.SetParameter("AutoEquipTry", ++num4);
									if (this.FireEvent(event5))
									{
										return this.AutoEquipSucceed(Silent, list);
									}
								}
							}
							event5.SetParameter("BodyPart", bodyPart6 ?? part7[0]);
							event5.SetParameter("AutoEquipTry", ++num4);
							if (this.FireEvent(event5))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
						this.AutoEquipFail(GO, Silent, currentCell, inInventory, event5, null);
						return false;
					}
					Event event6 = Event.New("CommandEquipObject", "Object", GO);
					event6.SetParameter("WasUnequipped", list);
					if (Silent)
					{
						event6.SetSilent(true);
					}
					Body body3 = this.Body;
					List<BodyPart> list2 = body3.GetPart("Hand");
					if (!ForceHeld && GO.HasPart<Armor>())
					{
						Armor part8 = GO.GetPart<Armor>();
						if (part8.WornOn != "Body")
						{
							if (part8.WornOn == "*")
							{
								list2 = body3.GetParts();
							}
							else
							{
								list2 = body3.GetPart(part8.WornOn);
							}
						}
					}
					int num5 = 0;
					foreach (BodyPart bodyPart9 in list2)
					{
						if (!bodyPart9.Primary && bodyPart9.Equipped == null)
						{
							event6.SetParameter("BodyPart", bodyPart9);
							event6.SetParameter("AutoEquipTry", ++num5);
							if (this.FireEvent(event6))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
					}
					foreach (BodyPart bodyPart10 in list2)
					{
						if (!bodyPart10.Primary && bodyPart10.Equipped != null)
						{
							event6.SetParameter("BodyPart", bodyPart10);
							event6.SetParameter("AutoEquipTry", ++num5);
							if (this.FireEvent(event6))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
					}
					foreach (BodyPart bodyPart11 in list2)
					{
						if (bodyPart11.Primary && bodyPart11.Equipped == null)
						{
							event6.SetParameter("BodyPart", bodyPart11);
							event6.SetParameter("AutoEquipTry", ++num5);
							if (this.FireEvent(event6))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
					}
					foreach (BodyPart bodyPart12 in list2)
					{
						if (bodyPart12.Primary && bodyPart12.Equipped != null)
						{
							event6.SetParameter("BodyPart", bodyPart12);
							event6.SetParameter("AutoEquipTry", ++num5);
							if (this.FireEvent(event6))
							{
								return this.AutoEquipSucceed(Silent, list);
							}
						}
					}
					this.AutoEquipFail(GO, Silent, currentCell, inInventory, event6, null);
					return false;
				}
				bool flag;
				return flag;
			}
			Body body4 = this.Body;
			BodyPart bodyPart13 = body4.GetFirstPart("Thrown Weapon", (BodyPart p) => p.Equipped == null) ?? body4.GetFirstPart("Thrown Weapon");
			if (bodyPart13 == null)
			{
				this.AutoEquipFail(GO, Silent, currentCell, inInventory, null, null);
			}
			else
			{
				Event event7 = Event.New("CommandEquipObject", "Object", GO, "BodyPart", bodyPart13);
				event7.SetParameter("WasUnequipped", list);
				if (Silent)
				{
					event7.SetSilent(true);
				}
				event7.SetParameter("AutoEquipTry", 1);
				if (this.FireEvent(event7))
				{
					return this.AutoEquipSucceed(Silent, list);
				}
				this.AutoEquipFail(GO, Silent, currentCell, inInventory, event7, null);
			}
			return false;
		}

		/// <summary>
		/// This is just an interface into the UsesTwoSlots setting on physics,
		/// which controls whether the object is "two-handed" at base.  It is not
		/// a way of determining how many hands a creature actually needs in order
		/// to wield a weapon; use GetSlotsRequiredFor() for that.
		/// </summary>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06006075 RID: 24693 RVA: 0x001D75B0 File Offset: 0x001D57B0
		// (set) Token: 0x06006076 RID: 24694 RVA: 0x001D75C3 File Offset: 0x001D57C3
		public bool UsesTwoSlots
		{
			get
			{
				XRL.World.Parts.Physics physics = this.Physics;
				return physics != null && physics.UsesTwoSlots;
			}
			set
			{
				if (this.Physics != null)
				{
					this.Physics.UsesTwoSlots = value;
				}
			}
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x001D75DC File Offset: 0x001D57DC
		public int GetSlotsRequiredFor(GameObject Actor, string SlotType, bool FloorAtOne = true)
		{
			int num = GetSlotsRequiredEvent.GetFor(this, Actor, SlotType, 1, 0, 0, true, false);
			if (FloorAtOne && num < 1)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x001D7601 File Offset: 0x001D5801
		public string GetTile()
		{
			Render render = this.Render;
			if (render == null)
			{
				return null;
			}
			return render.Tile;
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x001D7614 File Offset: 0x001D5814
		public string GetForegroundColor()
		{
			Render render = this.Render;
			if (render == null)
			{
				return null;
			}
			return render.GetForegroundColor();
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x001D7627 File Offset: 0x001D5827
		public void SetForegroundColor(string Color)
		{
			Render render = this.Render;
			if (render == null)
			{
				return;
			}
			render.SetForegroundColor(Color);
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x001D763A File Offset: 0x001D583A
		public void SetForegroundColor(char Color)
		{
			Render render = this.Render;
			if (render == null)
			{
				return;
			}
			render.SetForegroundColor(Color);
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x001D764D File Offset: 0x001D584D
		public string GetBackgroundColor()
		{
			Render render = this.Render;
			if (render == null)
			{
				return null;
			}
			return render.GetBackgroundColor();
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x001D7660 File Offset: 0x001D5860
		public void SetBackgroundColor(string Color)
		{
			Render render = this.Render;
			if (render == null)
			{
				return;
			}
			render.SetBackgroundColor(Color);
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x001D7673 File Offset: 0x001D5873
		public string GetDetailColor()
		{
			Render render = this.Render;
			if (render == null)
			{
				return null;
			}
			return render.DetailColor;
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001D7686 File Offset: 0x001D5886
		public void SetBackgroundColor(char Color)
		{
			Render render = this.Render;
			if (render == null)
			{
				return;
			}
			render.SetBackgroundColor(Color);
		}

		// Token: 0x06006080 RID: 24704 RVA: 0x001D7699 File Offset: 0x001D5899
		public void SetDetailColor(string Color)
		{
			if (this.Render != null)
			{
				this.Render.DetailColor = Color;
			}
		}

		// Token: 0x06006081 RID: 24705 RVA: 0x001D76AF File Offset: 0x001D58AF
		public void SetDetailColor(char Color)
		{
			if (this.Render != null)
			{
				this.Render.DetailColor = Color.ToString() ?? "";
			}
		}

		// Token: 0x06006082 RID: 24706 RVA: 0x001D76D4 File Offset: 0x001D58D4
		public List<Effect> GetTonicEffects()
		{
			List<Effect> list = new List<Effect>();
			if (this._Effects != null)
			{
				foreach (Effect effect in this.Effects)
				{
					if (effect.Duration > 0 && effect.IsTonic())
					{
						list.Add(effect);
					}
				}
			}
			return list;
		}

		// Token: 0x06006083 RID: 24707 RVA: 0x001D7748 File Offset: 0x001D5948
		public int GetTonicEffectCount()
		{
			int num = 0;
			if (this._Effects != null)
			{
				int i = 0;
				int count = this.Effects.Count;
				while (i < count)
				{
					if (this.Effects[i].Duration > 0 && this.Effects[i].IsTonic())
					{
						num++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x001D77A3 File Offset: 0x001D59A3
		public int GetTonicCapacity()
		{
			return GetTonicCapacityEvent.GetFor(this, 1);
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x001D77AC File Offset: 0x001D59AC
		public bool IsBroken()
		{
			return this.HasEffect<Broken>();
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x001D77B4 File Offset: 0x001D59B4
		public bool IsRusted()
		{
			return this.HasEffect<Rusted>();
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x001D77BC File Offset: 0x001D59BC
		public bool IsEMPed()
		{
			return this.HasEffect<ElectromagneticPulsed>();
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x001D77C4 File Offset: 0x001D59C4
		public bool IsInStasis()
		{
			return this.HasEffect<Stasis>();
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x001D77CC File Offset: 0x001D59CC
		public bool IsLedBy(GameObject GO)
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsLedBy(GO);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x001D77E0 File Offset: 0x001D59E0
		public bool InSamePartyAs(GameObject GO)
		{
			Brain brain = this.Brain;
			return brain != null && brain.InSamePartyAs(GO);
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x001D77F4 File Offset: 0x001D59F4
		public bool IsTryingToJoinPartyLeader()
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsTryingToJoinPartyLeader();
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x001D7807 File Offset: 0x001D5A07
		public bool IsTryingToJoinPartyLeaderForZoneUncaching()
		{
			return this.IsTryingToJoinPartyLeader() && this.FireEvent("KeepZoneCachedForPlayerJoin");
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x001D7820 File Offset: 0x001D5A20
		public bool IsBondedBy(GameObject Object)
		{
			IBondedCompanion bondedCompanion = this.GetPartDescendedFrom<IBondedCompanion>();
			while (((bondedCompanion != null) ? bondedCompanion.CompanionOf : null) != null)
			{
				if (bondedCompanion.CompanionOf == this)
				{
					MetricsManager.LogError("bond cycle in " + this.DebugName + " + " + bondedCompanion.DebugName);
					break;
				}
				if (bondedCompanion.CompanionOf == Object)
				{
					return true;
				}
				bondedCompanion = bondedCompanion.CompanionOf.GetPartDescendedFrom<IBondedCompanion>();
			}
			return false;
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x001D7887 File Offset: 0x001D5A87
		public bool IsFluid()
		{
			return this.HasPart<Gas>() || this.IsOpenLiquidVolume();
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x001D789E File Offset: 0x001D5A9E
		public bool ConsiderSolid()
		{
			return this.Physics != null && this.Physics.Solid && this.Physics.IsReal;
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x001D78C8 File Offset: 0x001D5AC8
		public bool ConsiderSolidInRenderingContext()
		{
			return (this.Brain != null && this.Brain.LivesOnWalls) || ((this.Render == null || this.Render.Occluding) && (this.Physics != null && this.Physics.Solid && this.Physics.IsReal));
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x001D7928 File Offset: 0x001D5B28
		public bool ConsiderSolid(bool ForFluid)
		{
			if (!this.ConsiderSolid())
			{
				return false;
			}
			if (ForFluid)
			{
				if (this.HasTagOrProperty("Flyover"))
				{
					return false;
				}
				if (this.GetIntProperty("AllowMissiles", 0) != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x001D7957 File Offset: 0x001D5B57
		public bool ConsiderSolid(bool ForFluid, int Phase)
		{
			if (!this.ConsiderSolid())
			{
				return false;
			}
			if (ForFluid)
			{
				if (this.HasTagOrProperty("Flyover"))
				{
					return false;
				}
				if (this.GetIntProperty("AllowMissiles", 0) != 0)
				{
					return false;
				}
			}
			return this.PhaseMatches(Phase);
		}

		// Token: 0x06006093 RID: 24723 RVA: 0x001D7994 File Offset: 0x001D5B94
		public bool ConsiderSolidFor(GameObject Object, bool ForFluid = false)
		{
			if (!this.ConsiderSolid(ForFluid))
			{
				return false;
			}
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			if (Object != null)
			{
				Brain brain = Object.Brain;
				if (brain != null)
				{
					brain.CheckMobility(out flag, out flag2, out flag3);
				}
			}
			if (flag3 && this.IsWalkableWall(Object))
			{
				return false;
			}
			if (Object == null)
			{
				return true;
			}
			if (XRLCore.Core.IDKFA && Object.IsPlayer())
			{
				return false;
			}
			if (Object.IsFluid() || Object.IsFlying)
			{
				if (this.HasTagOrProperty("Flyover"))
				{
					return false;
				}
				if (this.GetIntProperty("AllowMissiles", 0) != 0)
				{
					return false;
				}
			}
			Forcefield forcefield;
			return this.PhaseMatches(Object) && (FungalVisionary.VisionLevel > 0 || !this.HasPart<FungalVision>() || Object.HasPart<FungalVision>()) && (!this.TryGetPart<Forcefield>(out forcefield) || this.HasPart<Stasisfield>() || !forcefield.CanPass(Object));
		}

		// Token: 0x06006094 RID: 24724 RVA: 0x001D7A67 File Offset: 0x001D5C67
		public bool ConsiderSolidInRenderingContextFor(GameObject Object)
		{
			return (this.Brain != null && this.Brain.LivesOnWalls) || this.ConsiderSolidFor(Object, false);
		}

		// Token: 0x06006095 RID: 24725 RVA: 0x001D7A88 File Offset: 0x001D5C88
		public bool ConsiderUnableToOccupySameCell(GameObject Object)
		{
			if (this.Physics == null || !this.Physics.IsReal)
			{
				return false;
			}
			if (Object.Physics == null || !Object.Physics.IsReal)
			{
				return false;
			}
			if (this.HasTagOrProperty("IgnoreOccupationChecks") || Object.HasTagOrProperty("IgnoreOccupationChecks"))
			{
				return false;
			}
			if (this.IsCombat && Object.IsCombat)
			{
				return this.PhaseAndFlightMatches(Object);
			}
			return Object.ConsiderSolidFor(this, false) && (this.Brain == null || !this.Brain.LivesOnWalls || !Object.IsWall());
		}

		// Token: 0x06006096 RID: 24726 RVA: 0x001D7B24 File Offset: 0x001D5D24
		public bool ConsiderSolidForProjectile(GameObject Projectile, GameObject Attacker, out bool RecheckHit, out bool RecheckPhase, GameObject Launcher = null, GameObject ApparentTarget = null, bool? PenetrateCreatures = false, bool? PenetrateWalls = false, bool Prospective = false, bool TreatAsSolidHandled = false)
		{
			RecheckHit = false;
			RecheckPhase = false;
			if (Projectile == null)
			{
				return this.ConsiderSolidFor(Attacker, false);
			}
			if (FungalVisionary.VisionLevel <= 0 && this.HasPart<FungalVision>() && !Attacker.HasPart<FungalVision>())
			{
				return false;
			}
			bool flag = PenetrateCreatures.GetValueOrDefault();
			bool flag2 = PenetrateWalls.GetValueOrDefault();
			if (PenetrateCreatures == null || PenetrateWalls == null)
			{
				GetMissileWeaponPerformanceEvent @for = GetMissileWeaponPerformanceEvent.GetFor(Attacker, Launcher, Projectile, null, null, null, null, null, null, null, null, null, null, false);
				flag = PenetrateCreatures ?? @for.PenetrateCreatures;
				flag2 = PenetrateWalls ?? @for.PenetrateWalls;
			}
			int num = 0;
			for (;;)
			{
				if (!TreatAsSolidHandled)
				{
					TreatAsSolid part = Projectile.GetPart<TreatAsSolid>();
					if (part != null)
					{
						bool flag4;
						bool flag5;
						bool flag3 = part.Match(this, Attacker, out flag4, out flag5, Launcher, ApparentTarget, null, flag, flag2, Prospective, null);
						if (flag5)
						{
							RecheckPhase = true;
						}
						if (flag4)
						{
							RecheckHit = true;
							if (++num < 100)
							{
								continue;
							}
						}
						if (flag3)
						{
							break;
						}
					}
				}
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics == null || !physics.Solid)
				{
					return false;
				}
				if (this.HasTagOrProperty("Flyover"))
				{
					return false;
				}
				if (this.GetIntProperty("AllowMissiles", 0) != 0)
				{
					return false;
				}
				if (!this.PhaseMatches(Projectile))
				{
					return false;
				}
				Forcefield part2 = this.GetPart<Forcefield>();
				if (part2 != null && !this.HasPart<Stasisfield>() && part2.CanMissilePassFrom(Attacker, Projectile))
				{
					return false;
				}
				if (Projectile.HasTagOrProperty("Light") && this.HasTagOrProperty("Transparent"))
				{
					return false;
				}
				bool flag7;
				bool flag8;
				bool flag6 = BeforeProjectileHitEvent.Check(Projectile, Attacker, this, out flag7, out flag8, flag, flag2, Launcher, ApparentTarget, null, null, false, Prospective);
				if (flag8)
				{
					RecheckPhase = true;
				}
				if (!flag7)
				{
					return flag6;
				}
				RecheckHit = true;
				if (++num >= 100)
				{
					return flag6;
				}
			}
			return true;
		}

		// Token: 0x06006097 RID: 24727 RVA: 0x001D7D04 File Offset: 0x001D5F04
		public bool ConsiderSolidForProjectile(GameObject Projectile, GameObject Attacker, GameObject Launcher = null, GameObject ApparentTarget = null, bool? PenetrateCreatures = false, bool? PenetrateWalls = false, bool Prospective = false, bool TreatAsSolidHandled = false)
		{
			bool flag;
			bool flag2;
			return this.ConsiderSolidForProjectile(Projectile, Attacker, out flag, out flag2, Launcher, ApparentTarget, PenetrateCreatures, PenetrateWalls, Prospective, TreatAsSolidHandled);
		}

		// Token: 0x06006098 RID: 24728 RVA: 0x001D7D28 File Offset: 0x001D5F28
		public bool CanInteractInCellWithSolid(GameObject Actor)
		{
			if (Actor != null && Actor.IsPlayer() && XRLCore.Core.IDKFA)
			{
				return true;
			}
			if (this.ConsiderSolidFor(Actor, false))
			{
				return true;
			}
			Brain brain = this.Brain;
			return brain != null && brain.LivesOnWalls;
		}

		// Token: 0x06006099 RID: 24729 RVA: 0x001D7D66 File Offset: 0x001D5F66
		public bool ConsiderOccludingFor(GameObject Object)
		{
			Render render = this.Render;
			return render != null && render.Occluding;
		}

		// Token: 0x0600609A RID: 24730 RVA: 0x001D7D79 File Offset: 0x001D5F79
		public int GetBodyPartCount()
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.GetPartCount();
		}

		// Token: 0x0600609B RID: 24731 RVA: 0x001D7D8C File Offset: 0x001D5F8C
		public int GetBodyPartCount(string Type)
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.GetPartCount(Type);
		}

		// Token: 0x0600609C RID: 24732 RVA: 0x001D7DA0 File Offset: 0x001D5FA0
		public int GetBodyPartCount(Predicate<BodyPart> Filter)
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.GetPartCount(Filter);
		}

		// Token: 0x0600609D RID: 24733 RVA: 0x001D7DB4 File Offset: 0x001D5FB4
		public int GetConcreteBodyPartCount()
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.GetConcretePartCount();
		}

		// Token: 0x0600609E RID: 24734 RVA: 0x001D7DC7 File Offset: 0x001D5FC7
		public int GetAbstractBodyPartCount()
		{
			Body body = this.Body;
			if (body == null)
			{
				return 0;
			}
			return body.GetAbstractPartCount();
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x001D7DDC File Offset: 0x001D5FDC
		public BodyPart GetRandomConcreteBodyPart(string preferType = null)
		{
			Body body = this.Body;
			if (preferType != null)
			{
				BodyPart randomElement = body.GetPart(preferType).GetRandomElement<BodyPart>(null);
				if (randomElement != null && !randomElement.Abstract)
				{
					return randomElement;
				}
			}
			return body.GetConcreteParts().GetRandomElement<BodyPart>(null);
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x001D7E1C File Offset: 0x001D601C
		public string GetGeneralTermForBodyParts(string Type, string Default = null)
		{
			int bodyPartCount = this.GetBodyPartCount(Type);
			if (bodyPartCount == 0)
			{
				return Default;
			}
			if (bodyPartCount == 1)
			{
				return this.GetFirstBodyPart(Type).GetOrdinalName();
			}
			string text = null;
			List<string> list = null;
			foreach (BodyPart bodyPart in this.Body.GetPart(Type))
			{
				string name = bodyPart.TypeModel().Name;
				if (text == null)
				{
					text = name;
				}
				else if (text != name)
				{
					if (list == null)
					{
						list = new List<string> { text, name };
					}
					if (!list.Contains(name))
					{
						list.Add(name);
					}
				}
			}
			if (list == null)
			{
				return text.Pluralize();
			}
			list.Sort();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				list[i] = list[i].Pluralize();
				i++;
			}
			return Grammar.MakeAndList(list, true);
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x001D7F1C File Offset: 0x001D611C
		public BodyPart GetUnequippedPreferredBodyPartOrAlternate(string PreferredType, string AlternateType)
		{
			Body body = this.Body;
			if (body == null)
			{
				return null;
			}
			return body.GetUnequippedPreferredBodyPartOrAlternate(PreferredType, AlternateType);
		}

		// Token: 0x060060A2 RID: 24738 RVA: 0x001D7F31 File Offset: 0x001D6131
		public int GetSeededRange(string Channel, int low, int high)
		{
			return this.GetSeededRandom(Channel).Next(low, high + 1);
		}

		/// <summary>Pulls a seeded System.Random from the RandomSeed:Channel intproperty, re-seeding the next call.  The seed
		/// for the next RNG on the same channel will NOT reflect how many numbers you've pulled out of this RNG, unlike WithSeededRandom.
		/// </summary>
		// Token: 0x060060A3 RID: 24739 RVA: 0x001D7F43 File Offset: 0x001D6143
		public global::System.Random GetSeededRandom(string Channel = "")
		{
			return this.WithSeededRandom<global::System.Random>((global::System.Random i) => i, Channel);
		}

		/// <summary>Creates a System.Random from the RandomSeed:Channel intproperty (initialized from worldseed + object id + channel)
		/// calling your function with it, and then writing a new seed to the intproperty based on the next number out of the RNG.
		/// Seeding is sensitive to the number of numbers you pull out of the RNG given to your proc</summary>
		// Token: 0x060060A4 RID: 24740 RVA: 0x001D7F6C File Offset: 0x001D616C
		public T WithSeededRandom<T>(Func<global::System.Random, T> Proc, string Channel = "")
		{
			string text = "RandomSeed:" + Channel;
			int num;
			if (!this.TryGetIntProperty(text, out num))
			{
				num = Hash.String(XRL.The.Game.GetWorldSeed(null).ToString() + this.ID + text);
			}
			global::System.Random random = new global::System.Random(num);
			T t = Proc(random);
			this.SetIntProperty(text, random.Next(), false);
			return t;
		}

		// Token: 0x060060A5 RID: 24741 RVA: 0x001D7FD4 File Offset: 0x001D61D4
		public void WithSeededRandom(Action<global::System.Random> Proc, string Channel = "")
		{
			string text = "RandomSeed:" + Channel;
			int num;
			if (!this.TryGetIntProperty(text, out num))
			{
				num = Hash.String(XRL.The.Game.GetWorldSeed(null).ToString() + this.ID + text);
			}
			global::System.Random random = new global::System.Random(num);
			Proc(random);
			this.SetIntProperty(text, random.Next(), false);
		}

		// Token: 0x060060A6 RID: 24742 RVA: 0x001D803A File Offset: 0x001D623A
		public void PermuteRandomMutationBuys()
		{
			this.GetSeededRandom("RandomMutationBuy");
			this.GetSeededRandom("brainbrine");
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x001D8054 File Offset: 0x001D6254
		public bool CanMakeTelepathicContactWith(GameObject who)
		{
			return who != null && this.HasPart<Telepathy>() && CanReceiveTelepathyEvent.Check(who, this);
		}

		// Token: 0x060060A8 RID: 24744 RVA: 0x001D8071 File Offset: 0x001D6271
		public bool CanMakeEmpathicContactWith(GameObject who)
		{
			return who != null && this.HasPart<Empathy>() && CanReceiveEmpathyEvent.Check(who, this);
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x001D8090 File Offset: 0x001D6290
		public bool CanManipulateTelekinetically(GameObject Object = null)
		{
			Telekinesis telekinesis;
			return this.TryGetPart<Telekinesis>(out telekinesis) && this.DistanceTo(Object) <= telekinesis.GetTelekineticRange();
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060060AA RID: 24746 RVA: 0x001D80BB File Offset: 0x001D62BB
		public bool IsCreature
		{
			get
			{
				return this.HasPropertyOrTag("Creature");
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x001D80C8 File Offset: 0x001D62C8
		public bool IsProjectile
		{
			get
			{
				return this.HasPropertyOrTag("Projectile");
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060060AC RID: 24748 RVA: 0x001D80D5 File Offset: 0x001D62D5
		// (set) Token: 0x060060AD RID: 24749 RVA: 0x001D80E2 File Offset: 0x001D62E2
		public bool IsTrifling
		{
			get
			{
				return this.HasIntProperty("trifling");
			}
			set
			{
				if (value)
				{
					this.SetIntProperty("trifling", 1, false);
					return;
				}
				this.RemoveIntProperty("trifling");
			}
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x001D8101 File Offset: 0x001D6301
		public Cell PickDirection(string Label = null)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			if (physics == null)
			{
				return null;
			}
			return physics.PickDirection(Label, null);
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x001D8118 File Offset: 0x001D6318
		public int GetBodyWeight()
		{
			int num = this.Stat("Strength", 0);
			int num2 = ((num > 0) ? (num * num - num * 15 + 150) : 0);
			if (num2 != 0 && this.IsGiganticCreature)
			{
				num2 *= 5;
			}
			return num2;
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x001D8158 File Offset: 0x001D6358
		public int GetKineticResistance()
		{
			return GetKineticResistanceEvent.GetFor(this, 0, 0, 0, 0);
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x001D8164 File Offset: 0x001D6364
		public int GetSpringiness()
		{
			return GetSpringinessEvent.GetFor(this, 0, 0, 0, 0);
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x001D8170 File Offset: 0x001D6370
		public int GetKineticAbsorption()
		{
			return Math.Max(this.GetKineticResistance() + this.GetSpringiness(), 1);
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060060B3 RID: 24755 RVA: 0x001D8185 File Offset: 0x001D6385
		public bool IsSubjectToGravity
		{
			get
			{
				return SubjectToGravityEvent.Check(this);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060060B4 RID: 24756 RVA: 0x001D818D File Offset: 0x001D638D
		public bool CanFall
		{
			get
			{
				return CanFallEvent.Check(this);
			}
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x001D8195 File Offset: 0x001D6395
		public void Gravitate()
		{
			if (this.IsSubjectToGravity)
			{
				GravitationEvent.Check(this, this.CurrentCell);
			}
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x001D81AB File Offset: 0x001D63AB
		public int GetVisibilityRadius()
		{
			if (XRLCore.Core.VisAllToggle && this.IsPlayer())
			{
				return 80;
			}
			return AdjustVisibilityRadiusEvent.GetFor(this, this.GetIntProperty("VisibilityRadius", 80));
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x001D81D8 File Offset: 0x001D63D8
		public void PotentiallyAngerOwner(GameObject who, string Suppress = null)
		{
			if (!this.Owner.IsNullOrEmpty() && (Suppress.IsNullOrEmpty() || !this.HasPropertyOrTag(Suppress)))
			{
				XRL.World.Parts.Physics physics = this.Physics;
				if (physics != null)
				{
					physics.BroadcastForHelp(who);
				}
			}
			GameObject inInventory = this.InInventory;
			if (!string.IsNullOrEmpty((inInventory != null) ? inInventory.Owner : null) && inInventory.Owner != this.Owner && (Suppress.IsNullOrEmpty() || !inInventory.HasPropertyOrTag(Suppress)))
			{
				XRL.World.Parts.Physics physics2 = inInventory.Physics;
				if (physics2 == null)
				{
					return;
				}
				physics2.BroadcastForHelp(who);
			}
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x001D8264 File Offset: 0x001D6464
		public int GetFuriousConfusion()
		{
			int num = 0;
			foreach (Effect effect in this.Effects)
			{
				FuriouslyConfused furiouslyConfused = effect as FuriouslyConfused;
				if (furiouslyConfused != null)
				{
					num += furiouslyConfused.Level;
				}
			}
			num += this.GetIntProperty("FuriousConfusionLevel", 0);
			return num;
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x001D82D4 File Offset: 0x001D64D4
		public int GetConfusion()
		{
			int num = 0;
			foreach (Effect effect in this.Effects)
			{
				XRL.World.Effects.Confused confused = effect as XRL.World.Effects.Confused;
				if (confused != null)
				{
					num += confused.Level;
				}
				else
				{
					FuriouslyConfused furiouslyConfused = effect as FuriouslyConfused;
					if (furiouslyConfused != null)
					{
						num += furiouslyConfused.Level;
					}
					else if (effect is HulkHoney_Tonic_Allergy)
					{
						num++;
					}
				}
			}
			num += this.GetIntProperty("ConfusionLevel", 0);
			return num;
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x001D836C File Offset: 0x001D656C
		public int GetTotalConfusion()
		{
			return this.GetConfusion();
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060060BB RID: 24763 RVA: 0x001D8374 File Offset: 0x001D6574
		public bool IsConfused
		{
			get
			{
				return this.GetConfusion() > 0;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x001D837F File Offset: 0x001D657F
		public bool Respires
		{
			get
			{
				return RespiresEvent.Check(this);
			}
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x001D8388 File Offset: 0x001D6588
		public void RestorePristineHealth(bool UseHeal = false, bool SkipEffects = false)
		{
			Statistic stat = this.GetStat("Hitpoints");
			if (stat != null)
			{
				if (UseHeal)
				{
					this.Heal(stat.Penalty, true, true, false);
				}
				else
				{
					stat.Penalty = 0;
				}
			}
			Body body = this.Body;
			if (body != null)
			{
				int num = 0;
				while (body.DismemberedParts != null && body.DismemberedParts.Count > 0 && ++num < 100)
				{
					body.RegenerateLimb(false, null, null, null, null, null, null, true);
				}
			}
			if (this.Physics != null)
			{
				this.Physics.Temperature = 25;
			}
			Stomach stomach;
			if (this.TryGetPart<Stomach>(out stomach))
			{
				stomach.Water = RuleSettings.WATER_MAXIMUM;
			}
			if (this._Effects != null && !SkipEffects)
			{
				List<Effect> list = null;
				foreach (Effect effect in this.Effects)
				{
					if (effect.IsOfTypes(100663296) && !effect.IsOfType(134217728))
					{
						if (list == null)
						{
							list = new List<Effect>();
						}
						list.Add(effect);
					}
				}
				if (list != null)
				{
					foreach (Effect effect2 in list)
					{
						if (this.Effects.Contains(effect2))
						{
							this.RemoveEffect(effect2, false);
						}
					}
					this.CheckStack();
				}
			}
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x001D8524 File Offset: 0x001D6724
		public bool HasInventoryActionWithName(string Name, GameObject Actor = null)
		{
			using (List<InventoryAction>.Enumerator enumerator = EquipmentAPI.GetInventoryActions(this, Actor ?? this.ThePlayer, false, true).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name == Name)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x001D8590 File Offset: 0x001D6790
		public bool HasInventoryActionWithCommand(string Command, GameObject Actor = null)
		{
			using (List<InventoryAction>.Enumerator enumerator = EquipmentAPI.GetInventoryActions(this, Actor ?? this.ThePlayer, false, true).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Command == Command)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x001D85FC File Offset: 0x001D67FC
		public bool IsSpecialItem()
		{
			return this.HasProperty("RelicName") || this.HasTagOrProperty("QuestItem") || this.HasPart(typeof(SpecialItem)) || this.HasPart(typeof(ModExtradimensional)) || this.IsImportant();
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x001D865A File Offset: 0x001D685A
		public bool Fail(string msg)
		{
			if (this.IsPlayer())
			{
				Popup.ShowFail(msg, true, true, true);
			}
			return false;
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x001D8670 File Offset: 0x001D6870
		public bool IsHidden
		{
			get
			{
				Hidden part = this.GetPart<Hidden>();
				return part != null && !part.Found;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x001D8692 File Offset: 0x001D6892
		public bool IsAlive
		{
			get
			{
				return (this.IsCreature || this.HasTagOrProperty("LivePlant") || this.HasTagOrProperty("LiveFungus") || this.HasTagOrProperty("LiveAnimal")) && this.IsOrganic;
			}
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x001D86D0 File Offset: 0x001D68D0
		public void ForfeitTurn(bool EnergyNeutral = false)
		{
			if (!EnergyNeutral)
			{
				Statistic energy = this.Energy;
				if (energy != null)
				{
					energy.BaseValue = 0;
				}
			}
			if (this.IsPlayer())
			{
				XRL.The.ActionManager.SkipPlayerTurn = true;
			}
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x001D8704 File Offset: 0x001D6904
		public void PassTurn()
		{
			this.UseEnergy(1000, "Pass", null, null, true);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x001D8730 File Offset: 0x001D6930
		public string GetListDisplayContext(GameObject who)
		{
			if (this == who)
			{
				return "self";
			}
			GameObject inInventory = this.InInventory;
			if (inInventory != null)
			{
				if (inInventory == who)
				{
					return "inventory";
				}
				if (inInventory.InInventory == who || inInventory.Equipped == who)
				{
					return inInventory.DisplayNameOnlyStripped;
				}
			}
			if (this.Equipped == who)
			{
				BodyPart bodyPart = who.FindEquippedObject(this);
				if (bodyPart != null)
				{
					return bodyPart.Name;
				}
				return "equipped";
			}
			else
			{
				GameObject gameObject;
				Cell cell;
				this.GetContext(out gameObject, out cell);
				if (gameObject != null)
				{
					if (gameObject == who)
					{
						return "held";
					}
					return gameObject.DisplayNameOnlyStripped;
				}
				else
				{
					if (cell == null)
					{
						return null;
					}
					if (who.DistanceTo(this) <= 1)
					{
						return Directions.GetDirectionDescription(who.CurrentCell.GetDirectionFromCell(cell, false));
					}
					return "elsewhere";
				}
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060060C7 RID: 24775 RVA: 0x001D87DA File Offset: 0x001D69DA
		public bool IsBridge
		{
			get
			{
				return this.HasTagOrProperty("Bridge");
			}
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x001D87E8 File Offset: 0x001D69E8
		public int GetHeartCount()
		{
			int num = 1 + this.GetIntProperty("ExtraHearts", 0);
			if (this.HasPart<TwoHearted>())
			{
				num++;
			}
			return num;
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x001D8811 File Offset: 0x001D6A11
		public void SetHasMarkOfDeath(bool flag)
		{
			if (flag)
			{
				this.SetIntProperty("HasMarkOfDeath", 1, true);
				return;
			}
			this.SetIntProperty("HasMarkOfDeath", 2, true);
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x001D8834 File Offset: 0x001D6A34
		public bool FindMarkOfDeath(IPart skip = null)
		{
			Tattoos tattoos = this.GetPartExcept("Tattoos", skip) as Tattoos;
			if (tattoos == null)
			{
				return false;
			}
			string stringGameState = XRL.The.Game.GetStringGameState("MarkOfDeath", "");
			foreach (List<string> list in tattoos.Descriptions.Values)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (list[i].Contains(stringGameState))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x001D88E4 File Offset: 0x001D6AE4
		public bool HasMarkOfDeath(IPart skip = null)
		{
			int intProperty = this.GetIntProperty("HasMarkOfDeath", 0);
			if (intProperty == 1)
			{
				return true;
			}
			if (intProperty == 2)
			{
				return false;
			}
			bool flag = this.FindMarkOfDeath(null);
			this.SetHasMarkOfDeath(flag);
			return flag;
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x001D891A File Offset: 0x001D6B1A
		public void CheckMarkOfDeath(IPart skip = null)
		{
			this.SetHasMarkOfDeath(this.FindMarkOfDeath(skip));
		}

		// Token: 0x060060CD RID: 24781 RVA: 0x001D8929 File Offset: 0x001D6B29
		public void ToggleMarkOfDeath()
		{
			this.SetHasMarkOfDeath(!this.HasMarkOfDeath(null));
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x001D893B File Offset: 0x001D6B3B
		public int GetMaximumLiquidExposure()
		{
			return GetMaximumLiquidExposureEvent.GetFor(this, 0, 0, 0, 0);
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x001D8947 File Offset: 0x001D6B47
		public double GetMaximumLiquidExposureAsDouble()
		{
			return GetMaximumLiquidExposureEvent.GetDoubleFor(this, 0, 0, 0, 0);
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x001D8953 File Offset: 0x001D6B53
		public bool CanBeInvoluntarilyMoved()
		{
			return CanBeInvoluntarilyMovedEvent.Check(this);
		}

		// Token: 0x060060D1 RID: 24785 RVA: 0x001D895B File Offset: 0x001D6B5B
		public bool CanBeDismembered(string Attributes = null)
		{
			return CanBeDismemberedEvent.Check(this, Attributes);
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x001D8964 File Offset: 0x001D6B64
		public bool CanBeDismembered(GameObject Weapon)
		{
			string text;
			if (Weapon == null)
			{
				text = null;
			}
			else
			{
				MeleeWeapon part = Weapon.GetPart<MeleeWeapon>();
				text = ((part != null) ? part.Attributes : null);
			}
			return this.CanBeDismembered(text);
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x001D8984 File Offset: 0x001D6B84
		public bool CanTravel()
		{
			return CanTravelEvent.Check(this);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x001D898C File Offset: 0x001D6B8C
		public bool CanHaveNosebleed()
		{
			return Effect.CanEffectTypeBeAppliedTo(24, this) && this.Respires && this.Body.HasVariantPart("Face");
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x001D89B9 File Offset: 0x001D6BB9
		public bool CanClear(bool Important = false, bool Combat = false)
		{
			return (Important || !this.IsImportant()) && !this.HasPropertyOrTag("NoClear") && (Combat || !this.IsCombatObject());
		}

		// Token: 0x060060D6 RID: 24790 RVA: 0x001D89E3 File Offset: 0x001D6BE3
		public int GetHostileWalkRadius(GameObject who)
		{
			return GetHostileWalkRadiusEvent.GetFor(who, this);
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x001D89EC File Offset: 0x001D6BEC
		public bool IsWalkableWall(GameObject By, ref bool Uncacheable)
		{
			if (!this.IsWall())
			{
				return false;
			}
			if (!this.PhaseMatches(By))
			{
				return false;
			}
			if (this.IsCreature)
			{
				Uncacheable = true;
				if (!this.IsAlliedTowards(By))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x001D8A1C File Offset: 0x001D6C1C
		public bool IsWalkableWall(GameObject By)
		{
			bool flag = false;
			return this.IsWalkableWall(By, ref flag);
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x001D8A34 File Offset: 0x001D6C34
		public bool CanBePositionSwapped(GameObject By = null)
		{
			return !this.IsPlayer() && (!this.HasPropertyOrTag("Noswap") || this.IsMobile());
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060060DA RID: 24794 RVA: 0x001D8A5A File Offset: 0x001D6C5A
		public bool PathAsBurrower
		{
			get
			{
				return PathAsBurrowerEvent.Check(this);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060060DB RID: 24795 RVA: 0x001D8A62 File Offset: 0x001D6C62
		public bool IsFlying
		{
			get
			{
				return this.HasEffect(typeof(Flying)) && this.GetIntProperty("SuspendFlight", 0) <= 0;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x060060DC RID: 24796 RVA: 0x001D8A8A File Offset: 0x001D6C8A
		public bool IsThrownWeapon
		{
			get
			{
				return this.HasPart<ThrownWeapon>() || this.HasPart<GeomagneticDisc>();
			}
		}

		// Token: 0x060060DD RID: 24797 RVA: 0x001D8AA1 File Offset: 0x001D6CA1
		public string GetBleedLiquid(string Default = "blood-1000")
		{
			return GetBleedLiquidEvent.GetFor(this, Default);
		}

		// Token: 0x060060DE RID: 24798 RVA: 0x001D8AAA File Offset: 0x001D6CAA
		public int GetPowerLoadLevel()
		{
			return GetPowerLoadLevelEvent.GetFor(this, 100);
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x001D8AB4 File Offset: 0x001D6CB4
		public Cell MovingTo()
		{
			Brain brain = this.Brain;
			if (brain == null)
			{
				return null;
			}
			return brain.MovingTo();
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x001D8AC7 File Offset: 0x001D6CC7
		public bool IsFleeing()
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsFleeing();
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x001D8ADC File Offset: 0x001D6CDC
		public bool ShouldShunt()
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (!this.IsCombatObject())
			{
				return false;
			}
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = currentCell.Objects[i];
				if (gameObject != this && gameObject.IsCombatObject())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060060E2 RID: 24802 RVA: 0x001D8B33 File Offset: 0x001D6D33
		public bool IsFactionMember(string Faction)
		{
			Brain brain = this.Brain;
			return brain != null && brain.IsFactionMember(Faction);
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x001D8B47 File Offset: 0x001D6D47
		public bool IsSafeContainerForLiquid(string liquid)
		{
			return LiquidVolume.IsGameObjectSafeContainerForLiquid(this, liquid);
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x001D8B50 File Offset: 0x001D6D50
		public void Indicate(bool AsThreat = false)
		{
			Cell currentCell = this.GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.Indicate(AsThreat);
		}

		// Token: 0x060060E5 RID: 24805 RVA: 0x001D8B64 File Offset: 0x001D6D64
		public void EmitMessage(string Message, GameObject Object = null, string Color = null, bool UsePopup = false)
		{
			if (!Message.IsNullOrEmpty() && this.IsVisible())
			{
				string text = GameText.VariableReplace(Message, this, Object, UsePopup);
				if (!text.IsNullOrEmpty())
				{
					if (UsePopup)
					{
						Popup.Show(text.Color(Color), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return;
					}
					MessageQueue.AddPlayerMessage(text, Color, true);
				}
			}
		}

		// Token: 0x060060E6 RID: 24806 RVA: 0x001D8BB8 File Offset: 0x001D6DB8
		public void PlayWorldOrUISound(string Clip, float? Volume = null)
		{
			if (this.IsPlayer())
			{
				SoundManager.PlayUISound(Clip, Volume ?? 1f, false, false, SoundRequest.SoundEffectType.None);
				return;
			}
			this.PlayWorldSound(Clip, Volume ?? 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x060060E7 RID: 24807 RVA: 0x001D8C2C File Offset: 0x001D6E2C
		public void PlayWorldSound(string Clip, float Volume = 0.5f, float PitchVariance = 0f, bool Combat = false, float Delay = 0f, float Pitch = 1f, float CostMultiplier = 1f, int CostMaximum = 2147483647)
		{
			if (Clip.IsNullOrEmpty())
			{
				return;
			}
			Cell currentCell = (this.Holder ?? this).GetCurrentCell();
			if (currentCell == null)
			{
				return;
			}
			currentCell.PlayWorldSound(Clip, Volume, PitchVariance, Combat, Delay, Pitch, CostMultiplier, CostMaximum);
		}

		// Token: 0x060060E8 RID: 24808 RVA: 0x001D8C69 File Offset: 0x001D6E69
		public string GetLiquidColor(string Default = "K")
		{
			LiquidVolume liquidVolume = this.LiquidVolume;
			return ((liquidVolume != null) ? liquidVolume.GetPrimaryLiquidColor() : null) ?? Default;
		}

		// Token: 0x060060E9 RID: 24809 RVA: 0x001D8C82 File Offset: 0x001D6E82
		public string GetTinkeringBlueprint()
		{
			TinkerItem part = this.GetPart<TinkerItem>();
			return ((part != null) ? part.ActiveBlueprint : null) ?? this.Blueprint;
		}

		// Token: 0x060060EA RID: 24810 RVA: 0x001D8CA0 File Offset: 0x001D6EA0
		public bool AttackDirection(string Direction, bool EnableSwoop = true)
		{
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			Cell cellFromDirection = currentCell.GetCellFromDirection(Direction, false);
			if (cellFromDirection == null)
			{
				return false;
			}
			if (EnableSwoop && this.IsFlying && cellFromDirection.GetCombatTarget(this, false, false, false, 0, null, null, null, null, null, true, false, null) == null && cellFromDirection.GetCombatTarget(this, true, false, false, 0, null, null, null, null, null, true, false, null) != null)
			{
				return this.IsActivatedAbilityVoluntarilyUsable(Flight.SWOOP_ATTACK_COMMAND_NAME) && Combat.SwoopAttack(this, Direction);
			}
			return Combat.AttackDirection(this, Direction);
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x001D8D1A File Offset: 0x001D6F1A
		public bool HasBeenRead(GameObject By = null)
		{
			return HasBeenReadEvent.Check(this, By ?? XRL.The.Player);
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x001D8D2C File Offset: 0x001D6F2C
		public bool MakeNonflammable()
		{
			if (!this.HasTag("Creature") && this.Physics != null)
			{
				this.Physics.FlameTemperature = 99999;
				return true;
			}
			return false;
		}

		// Token: 0x060060ED RID: 24813 RVA: 0x001D8D56 File Offset: 0x001D6F56
		public bool MakeImperviousToHeat()
		{
			if (!this.HasTag("Creature") && this.Physics != null)
			{
				this.Physics.FlameTemperature = 99999;
				this.Physics.VaporTemperature = 99999;
				return true;
			}
			return false;
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x060060EE RID: 24814 RVA: 0x001D8D90 File Offset: 0x001D6F90
		public bool OwnedByPlayer
		{
			get
			{
				return this.GetIntProperty("StoredByPlayer", 0) > 0 || this.GetIntProperty("FromStoredByPlayer", 0) > 0;
			}
		}

		// Token: 0x060060EF RID: 24815 RVA: 0x001D8DB2 File Offset: 0x001D6FB2
		public bool CheckHP(int? CurrentHP = null, int? PreviousHP = null, int? MaxHP = null, bool Preregistered = false)
		{
			XRL.World.Parts.Physics physics = this.Physics;
			return physics != null && physics.CheckHP(CurrentHP, PreviousHP, MaxHP, Preregistered);
		}

		// Token: 0x060060F0 RID: 24816 RVA: 0x001D8DCC File Offset: 0x001D6FCC
		public bool WillCheckHP(bool? Registering = null)
		{
			bool? flag = Registering;
			bool flag2 = true;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				return this.ModIntProperty("WillCheckHP", 1, false) > 0;
			}
			flag = Registering;
			flag2 = false;
			if ((flag.GetValueOrDefault() == flag2) & (flag != null))
			{
				this.SetIntProperty("WillCheckHP", 0, false);
				return true;
			}
			return this.GetIntProperty("WillCheckHP", 0) > 0;
		}

		// Token: 0x060060F1 RID: 24817 RVA: 0x001D8E3C File Offset: 0x001D703C
		public bool NeedsRecharge()
		{
			foreach (IRechargeable rechargeable in this.GetPartsDescendedFrom<IRechargeable>())
			{
				if (rechargeable.CanBeRecharged() && rechargeable.GetRechargeAmount() > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060060F2 RID: 24818 RVA: 0x001D8EA0 File Offset: 0x001D70A0
		public bool CanApplyEffect(string Name, int Duration = 0, string EventName = null)
		{
			return this.FireEvent(EventName ?? ("CanApply" + Name)) && CanApplyEffectEvent.Check(this, Name, Duration);
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x001D8ECC File Offset: 0x001D70CC
		public bool IsEquipmentSolelyForSlotType(string Type)
		{
			string usesSlots = this.UsesSlots;
			if (!usesSlots.IsNullOrEmpty())
			{
				List<string> list = usesSlots.CachedCommaExpansion();
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					if (list[i] != Type)
					{
						return false;
					}
					i++;
				}
				return true;
			}
			Armor armor;
			XRL.World.Parts.Shield shield;
			return (this.TryGetPart<Armor>(out armor) && armor.WornOn == Type) || (this.TryGetPart<XRL.World.Parts.Shield>(out shield) && shield.WornOn == Type) || Type == "Hand";
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x001D8F5B File Offset: 0x001D715B
		public bool IsEntirelyFloating()
		{
			return this.IsEquipmentSolelyForSlotType("Floating Nearby");
		}

		// Token: 0x060060F5 RID: 24821 RVA: 0x001D8F68 File Offset: 0x001D7168
		public void ReplaceDisplayName(GetDisplayNameEvent E)
		{
			GetDisplayNameEvent.ReplaceFor(this, E, this.DisplayNameBase);
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x001D8F78 File Offset: 0x001D7178
		// (set) Token: 0x060060F7 RID: 24823 RVA: 0x001D8FB3 File Offset: 0x001D71B3
		public bool IsGiganticCreature
		{
			get
			{
				if (this.HasTag("Gigantic"))
				{
					return true;
				}
				int intProperty = this.GetIntProperty("Gigantic", 0);
				return intProperty > 0 || (intProperty >= 0 && this.HasPart<Gigantism>());
			}
			set
			{
				this.SetIntProperty("Gigantic", value ? 1 : (-1), false);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x001D8FC9 File Offset: 0x001D71C9
		// (set) Token: 0x060060F9 RID: 24825 RVA: 0x001D8FD1 File Offset: 0x001D71D1
		public bool IsGiganticEquipment
		{
			get
			{
				return this.HasPart<ModGigantic>();
			}
			set
			{
				if (value)
				{
					this.RequirePart<ModGigantic>(false);
					return;
				}
				this.RemovePart<ModGigantic>();
			}
		}

		// Token: 0x060060FA RID: 24826 RVA: 0x001D8FE8 File Offset: 0x001D71E8
		public bool IsSizeCompatibleWithEquipper(GameObject Equipper)
		{
			if (Equipper.IsGiganticCreature)
			{
				if (!this.IsGiganticEquipment && this.HasPropertyOrTag("GiganticEquippable") && !this.IsNatural() && !this.IsEntirelyFloating())
				{
					return false;
				}
			}
			else if (this.IsGiganticEquipment && !this.IsNatural() && !this.IsEquipmentSolelyForSlotType("Hand") && !this.IsEquipmentSolelyForSlotType("Missile Weapon") && !this.IsEntirelyFloating())
			{
				return false;
			}
			return true;
		}

		// Token: 0x060060FB RID: 24827 RVA: 0x001D905C File Offset: 0x001D725C
		public bool EquipAsDefaultBehavior()
		{
			return this.IsNatural() && (!this.HasPart<Armor>() || this.HasTagOrProperty("AllowArmorDefaultBehavior")) && (!this.HasPart<XRL.World.Parts.Shield>() || this.HasTagOrProperty("AllowShieldDefaultBehavior")) && !this.HasTagOrProperty("NoDefaultBehavior");
		}

		// Token: 0x060060FC RID: 24828 RVA: 0x001D90B1 File Offset: 0x001D72B1
		public void Understand(GameObject Object)
		{
			if (this.IsPlayer())
			{
				Object.MakeUnderstood(false);
			}
		}

		// Token: 0x060060FD RID: 24829 RVA: 0x001D90C3 File Offset: 0x001D72C3
		public void CheckDefaultBehaviorGiganticness(GameObject Equipper)
		{
			if (GameObject.Validate(ref Equipper) && Equipper.IsGiganticCreature && !this.IsGiganticEquipment)
			{
				this.IsGiganticEquipment = true;
				this.ModIntProperty("ModGiganticNoDisplayName", 1, true);
			}
		}

		// Token: 0x060060FE RID: 24830 RVA: 0x001D90F3 File Offset: 0x001D72F3
		public bool IsInteresting()
		{
			return GetPointsOfInterestEvent.Check(this, XRL.The.Player, null);
		}

		// Token: 0x060060FF RID: 24831 RVA: 0x001D9104 File Offset: 0x001D7304
		public int GetSkillAndPowerCountInSkill(string Skill)
		{
			if (Skill.IsNullOrEmpty())
			{
				return 0;
			}
			int num = 0;
			if (this.HasSkill(Skill))
			{
				num++;
			}
			SkillEntry skillIfExists = SkillFactory.Factory.GetSkillIfExists(Skill);
			if (skillIfExists != null)
			{
				foreach (string text in skillIfExists.Powers.Keys)
				{
					if (this.HasSkill(text))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06006100 RID: 24832 RVA: 0x001D918C File Offset: 0x001D738C
		public bool IsEMPSensitive()
		{
			return !TransparentToEMPEvent.Check(this);
		}

		// Token: 0x06006101 RID: 24833 RVA: 0x001D9197 File Offset: 0x001D7397
		public bool IsAffliction()
		{
			return IsAfflictionEvent.Check(this);
		}

		// Token: 0x06006102 RID: 24834 RVA: 0x001D919F File Offset: 0x001D739F
		public string GetNativeRegion()
		{
			return this.GetPropertyOrTag("NativeRegion", null);
		}

		// Token: 0x06006103 RID: 24835 RVA: 0x001D91AD File Offset: 0x001D73AD
		public bool ShouldAttackToReachTarget(GameObject Object, GameObject Target = null)
		{
			return ShouldAttackToReachTargetEvent.Check(this, Object, Target ?? this.Target, false);
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x001D91C4 File Offset: 0x001D73C4
		public int SortVs(GameObject Object, Dictionary<GameObject, string> CategoryCache = null, bool UseCategory = true, bool UseDisplayName = true, bool UseEvent = true, bool UseRenderLayer = false, List<GameObject> Items = null)
		{
			string text = null;
			string text2 = null;
			if (UseCategory)
			{
				if (CategoryCache == null || !CategoryCache.TryGetValue(this, out text))
				{
					text = this.GetInventoryCategory(false);
					if (CategoryCache != null)
					{
						CategoryCache.Add(this, text);
					}
				}
				if (CategoryCache == null || !CategoryCache.TryGetValue(Object, out text2))
				{
					text2 = Object.GetInventoryCategory(false);
					if (CategoryCache != null)
					{
						CategoryCache.Add(Object, text2);
					}
				}
				int num = string.Compare(text ?? "", text2 ?? "", true);
				if (num != 0)
				{
					return num;
				}
			}
			if (UseRenderLayer)
			{
				Render render = Object.Render;
				int num2 = ((render != null) ? render.RenderLayer : 0);
				Render render2 = this.Render;
				int num3 = num2.CompareTo((render2 != null) ? render2.RenderLayer : 0);
				if (num3 != 0)
				{
					return num3;
				}
			}
			if (UseDisplayName)
			{
				int num4 = string.Compare(this.GetCachedDisplayNameForSort(), Object.GetCachedDisplayNameForSort(), true);
				if (num4 != 0)
				{
					return num4;
				}
			}
			if (UseEvent)
			{
				int @for = GetGameObjectSortEvent.GetFor(this, Object, text ?? this.GetInventoryCategory(false), text2 ?? Object.GetInventoryCategory(false));
				if (@for != 0)
				{
					return @for;
				}
			}
			if (Items != null)
			{
				int num2 = Items.IndexOf(this);
				return num2.CompareTo(Items.IndexOf(Object));
			}
			return 0;
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x001D92E8 File Offset: 0x001D74E8
		public int GetModificationCount()
		{
			int num = 0;
			using (Container<IPart>.Enumerator enumerator = this.PartsList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current is IModification)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x001D9344 File Offset: 0x001D7544
		public int GetModificationSlotsUsed()
		{
			int num = 0;
			foreach (IPart part in this.PartsList)
			{
				IModification modification = part as IModification;
				if (modification != null)
				{
					num += modification.GetModificationSlotUsage();
				}
			}
			return num;
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x001D93A4 File Offset: 0x001D75A4
		public bool IsInLoveWith(GameObject Subject)
		{
			if (this._Effects != null)
			{
				foreach (Effect effect in this.Effects)
				{
					Lovesick lovesick = effect as Lovesick;
					if (lovesick != null && lovesick.Beauty == Subject)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x001D9410 File Offset: 0x001D7610
		public string GetDescriptiveCategory()
		{
			if (!this.Understood())
			{
				return "artifact";
			}
			if (this.IsCreature)
			{
				return "creature";
			}
			if (this.HasPart<CyberneticsBaseItem>())
			{
				return "implant";
			}
			if (this.HasPart<MissileWeapon>() || this.HasPart<ThrownWeapon>())
			{
				return "weapon";
			}
			MeleeWeapon meleeWeapon;
			if (this.TryGetPart<MeleeWeapon>(out meleeWeapon) && !meleeWeapon.IsImprovisedWeapon())
			{
				return "weapon";
			}
			Scanning.Scan scanTypeFor = Scanning.GetScanTypeFor(this);
			if (scanTypeFor == Scanning.Scan.Tech)
			{
				return "artifact";
			}
			if (scanTypeFor == Scanning.Scan.Bio)
			{
				return "organism";
			}
			if (this.HasPart<Armor>() && !this.IsPluralIfKnown)
			{
				return "armor";
			}
			if (this.HasPart<XRL.World.Parts.Shield>() && !this.IsPluralIfKnown)
			{
				return "shield";
			}
			if (!this.Takeable)
			{
				return "object";
			}
			return "item";
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x001D94D0 File Offset: 0x001D76D0
		public bool WithinSensePsychicRange(Cell Cell)
		{
			if (Cell == null)
			{
				return false;
			}
			SensePsychic part = this.GetPart<SensePsychic>();
			return part != null && this.DistanceTo(Cell) <= part.Radius;
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x001D9500 File Offset: 0x001D7700
		public bool WithinSensePsychicRange(GameObject Object)
		{
			return this.WithinSensePsychicRange((Object != null) ? Object.CurrentCell : null);
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x001D9514 File Offset: 0x001D7714
		public bool CanBeUnequipped(GameObject Equipper = null, GameObject Actor = null, bool Forced = false, bool SemiForced = false)
		{
			if (Equipper == null)
			{
				Equipper = this.Equipped;
			}
			if (Actor == null)
			{
				Actor = Equipper;
			}
			return CanBeUnequippedEvent.Check(this, Equipper, Actor, Forced, SemiForced);
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x001D9534 File Offset: 0x001D7734
		public bool BeginBeingUnequipped(ref string FailureMessage, ref bool DestroyOnUnequipDeclined, GameObject Equipper = null, GameObject Actor = null, BodyPart BodyPart = null, bool Silent = false, bool Forced = false, bool SemiForced = false, int AutoEquipTry = 0)
		{
			if (Equipper == null)
			{
				Equipper = this.Equipped;
			}
			if (Actor == null)
			{
				Actor = Equipper;
			}
			return BeginBeingUnequippedEvent.Check(this, ref FailureMessage, ref DestroyOnUnequipDeclined, Equipper, Actor, BodyPart, Silent, Forced, SemiForced, AutoEquipTry);
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x001D9568 File Offset: 0x001D7768
		public bool BeginBeingUnequipped(GameObject Equipper = null, GameObject Actor = null, BodyPart BodyPart = null, bool Silent = false, bool Forced = false, bool SemiForced = false, int AutoEquipTry = 0)
		{
			string text = null;
			bool flag = false;
			return this.BeginBeingUnequipped(ref text, ref flag, Equipper, Actor, BodyPart, Silent, Forced, SemiForced, AutoEquipTry);
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x001D9590 File Offset: 0x001D7790
		public bool HasAdjacentAllyOf(GameObject Actor)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			Cell currentCell = this.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				GameObject gameObject = currentCell.Objects[i];
				if (gameObject != Actor && gameObject != this && gameObject.IsCombatObject() && Actor.IsAlliedTowards(gameObject))
				{
					return true;
				}
				i++;
			}
			foreach (Cell cell in currentCell.GetLocalAdjacentCells())
			{
				int j = 0;
				int count2 = cell.Objects.Count;
				while (j < count2)
				{
					GameObject gameObject2 = cell.Objects[j];
					if (gameObject2 != Actor && gameObject2.IsCombatObject() && Actor.IsAlliedTowards(gameObject2))
					{
						return true;
					}
					j++;
				}
			}
			return false;
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x001D9688 File Offset: 0x001D7888
		public static bool PerformReplaceCell(GameObject Actor)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			GetReplaceCellInteractionsEvent @for = GetReplaceCellInteractionsEvent.GetFor(Actor);
			if (@for.Objects == null || @for.Objects.Count == 0)
			{
				return Actor.Fail("You have no devices that use energy cells.");
			}
			GameObject gameObject = Popup.PickGameObject("Choose an item to slot a cell into.", @for.Objects, true, true, true, null, 0, false);
			return GameObject.Validate(ref gameObject) && InventoryActionEvent.Check(gameObject, Actor, gameObject, @for.Interactions[gameObject], false, false, false, false, false, 0, 0, 0, null, null, null, null);
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x001D970C File Offset: 0x001D790C
		public string GetCreatureType(bool Capitalized = false, bool SkipSetting = false)
		{
			string text = (SkipSetting ? null : this.GetPropertyOrTag("CreatureType", null)) ?? (this.HasTagOrProperty("UseExtendedDisplayNameForCreatureType") ? this.GetReferenceDisplayName(int.MaxValue, null, "CreatureType", false, true, false, true, false, false, false, false, null, false) : (this.HasPropertyOrTag("NoAnimatedNamePrefix") ? this.DisplayNameOnlyDirectAndStripped : (this.HasPart<AnimatedObject>() ? ("animated " + this.DisplayNameOnlyDirectAndStripped) : this.ShortDisplayNameWithoutTitlesStripped)));
			if (Capitalized)
			{
				text = Grammar.MakeTitleCase(text);
			}
			return text;
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x001D979C File Offset: 0x001D799C
		public string GiveProperName(string Name = null, bool Force = false, string Special = null, bool SpecialFaildown = false, bool? HasHonorific = null, bool? HasEpithet = null, Dictionary<string, string> NamingContext = null, Func<string, string> Process = null)
		{
			if (!Force && this.HasProperName)
			{
				return null;
			}
			if (!this.HasPropertyOrTag("CreatureType"))
			{
				this.SetStringProperty("CreatureType", this.GetCreatureType(false, true), false);
			}
			if (Name.IsNullOrEmpty())
			{
				Name = NameMaker.MakeName(this, null, null, null, null, null, null, null, null, null, Special, null, NamingContext, false, SpecialFaildown, HasHonorific, HasEpithet);
			}
			if (Process != null)
			{
				Name = Process(Name);
			}
			this.DisplayName = Name;
			this.HasProperName = true;
			if (this.IsOriginalPlayerBody() && XRL.The.Game != null)
			{
				XRL.The.Game.PlayerName = Name;
			}
			return Name;
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06006112 RID: 24850 RVA: 0x001D9834 File Offset: 0x001D7A34
		public bool HasHonorific
		{
			get
			{
				Honorifics honorifics;
				return this.TryGetPart<Honorifics>(out honorifics) && !honorifics.HonorificList.IsNullOrEmpty();
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x001D985C File Offset: 0x001D7A5C
		public bool HasEpithet
		{
			get
			{
				Epithets epithets;
				return this.TryGetPart<Epithets>(out epithets) && !epithets.EpithetList.IsNullOrEmpty();
			}
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x001D9883 File Offset: 0x001D7A83
		public string GetFactionRank(string Faction)
		{
			if (Faction.IsNullOrEmpty())
			{
				return null;
			}
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.GetFactionRank(Faction);
			}
			return GetFactionRankEvent.GetFor(this, Faction, null);
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x001D98B0 File Offset: 0x001D7AB0
		public int GetFactionStanding(string Faction)
		{
			if (Faction.IsNullOrEmpty())
			{
				return 0;
			}
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.GetFactionStanding(Faction);
			}
			return Faction.GetRankStanding(Faction, this.GetFactionRank(Faction));
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x001D98E4 File Offset: 0x001D7AE4
		public bool IsBelowFactionRank(string Faction, string Rank)
		{
			if (Faction.IsNullOrEmpty())
			{
				return true;
			}
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.IsBelowRank(Faction, Rank);
			}
			string factionRank = this.GetFactionRank(Faction);
			if (factionRank == null)
			{
				return true;
			}
			if (factionRank == Rank)
			{
				return false;
			}
			int rankStanding = Faction.GetRankStanding(Faction, factionRank);
			int rankStanding2 = Faction.GetRankStanding(Faction, Rank);
			return rankStanding < rankStanding2;
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x001D993F File Offset: 0x001D7B3F
		public bool IsAtLeastFactionRank(string Faction, string Rank)
		{
			return !this.IsBelowFactionRank(Faction, Rank);
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x001D994C File Offset: 0x001D7B4C
		public bool IsBelowFactionStanding(string Faction, int Standing)
		{
			if (Faction.IsNullOrEmpty())
			{
				return true;
			}
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.IsBelowStanding(Faction, Standing);
			}
			return this.GetFactionStanding(Faction) < Standing;
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x001D997C File Offset: 0x001D7B7C
		public bool IsAtLeastFactionStanding(string Faction, int Standing)
		{
			return !this.IsBelowFactionStanding(Faction, Standing);
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x001D9989 File Offset: 0x001D7B89
		public bool PromoteIfBelow(string Faction, string Rank, bool Message = true, bool IgnoreVisibility = false, bool Capitalize = true)
		{
			if (Faction.IsNullOrEmpty())
			{
				return false;
			}
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.PromoteIfBelow(Faction, Rank, Message, Capitalize);
			}
			return this.RequirePart<FactionRank>(false).PromoteIfBelow(Faction, Rank, Message, IgnoreVisibility, Capitalize);
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x001D99C8 File Offset: 0x001D7BC8
		public bool HasWorshipped(string Faction, int WithinTurns = 0)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasWorshipped(Faction, WithinTurns);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasWorshipped(Faction, WithinTurns);
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x001D9A04 File Offset: 0x001D7C04
		public bool HasWorshipped(Worshippable Being, int WithinTurns = 0)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasWorshipped(Being, WithinTurns);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasWorshipped(Being, WithinTurns);
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x001D9A40 File Offset: 0x001D7C40
		public bool HasWorshippedInName(string Name, string Faction = null, int WithinTurns = 0)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasWorshippedInName(Name, Faction, WithinTurns);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasWorshippedInName(Name, Faction, WithinTurns);
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x001D9A80 File Offset: 0x001D7C80
		public bool HasWorshippedBySpec(string Spec, string ContextFaction = null)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasWorshippedBySpec(Spec, ContextFaction);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasWorshippedBySpec(Spec, ContextFaction);
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x001D9ABC File Offset: 0x001D7CBC
		public List<WorshipTracking> GetWorshipTracking()
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.GetWorshipTracking();
			}
			SacralTracking sacralTracking;
			if (this.TryGetPart<SacralTracking>(out sacralTracking))
			{
				return sacralTracking.GetWorshipTracking();
			}
			return null;
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x001D9AF4 File Offset: 0x001D7CF4
		public bool HasBlasphemed(string Faction, int WithinTurns = 0)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasBlasphemed(Faction, WithinTurns);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasBlasphemed(Faction, WithinTurns);
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x001D9B30 File Offset: 0x001D7D30
		public bool HasBlasphemed(Worshippable Being, int WithinTurns = 0)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasBlasphemed(Being, WithinTurns);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasBlasphemed(Being, WithinTurns);
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x001D9B6C File Offset: 0x001D7D6C
		public bool HasBlasphemedAgainstName(string Name, string Faction = null, int WithinTurns = 0)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasBlasphemedAgainstName(Name, Faction, WithinTurns);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasBlasphemedAgainstName(Name, Faction, WithinTurns);
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x001D9BAC File Offset: 0x001D7DAC
		public bool HasBlasphemedBySpec(string Spec, string ContextFaction = null)
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.HasBlasphemedBySpec(Spec, ContextFaction);
			}
			SacralTracking sacralTracking;
			return this.TryGetPart<SacralTracking>(out sacralTracking) && sacralTracking.HasBlasphemedBySpec(Spec, ContextFaction);
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x001D9BE8 File Offset: 0x001D7DE8
		public List<WorshipTracking> GetBlasphemyTracking()
		{
			if (this.IsPlayer())
			{
				return XRL.The.Game.PlayerReputation.GetBlasphemyTracking();
			}
			SacralTracking sacralTracking;
			if (this.TryGetPart<SacralTracking>(out sacralTracking))
			{
				return sacralTracking.GetBlasphemyTracking();
			}
			return null;
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x001D9C20 File Offset: 0x001D7E20
		public void GetCompanions(List<GameObject> Store)
		{
			ZoneManager zoneManager = XRL.The.ZoneManager;
			Zone activeZone = zoneManager.ActiveZone;
			if (activeZone != null)
			{
				activeZone.FindObjects(Store, this.IsPlayer() ? new Predicate<GameObject>(GameObject.ObjectIsPlayerLed) : ((GameObject o) => o.IsLedBy(this)));
			}
			foreach (Zone zone in zoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					zone.FindObjects(Store, this.IsPlayer() ? new Predicate<GameObject>(GameObject.ObjectIsPlayerLed) : ((GameObject o) => o.IsLedBy(this)));
				}
			}
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x001D9CD8 File Offset: 0x001D7ED8
		public bool AnyCompanion(int MaxDistance = 0, Predicate<GameObject> Filter = null)
		{
			GameObject.<>c__DisplayClass1475_0 CS$<>8__locals1 = new GameObject.<>c__DisplayClass1475_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.MaxDistance = MaxDistance;
			CS$<>8__locals1.Filter = Filter;
			ZoneManager zoneManager = XRL.The.ZoneManager;
			Zone activeZone = zoneManager.ActiveZone;
			CS$<>8__locals1.leadFilter = (this.IsPlayer() ? new Predicate<GameObject>(GameObject.ObjectIsPlayerLed) : ((GameObject o) => o.IsLedBy(CS$<>8__locals1.<>4__this)));
			GameObject.<>c__DisplayClass1475_0 CS$<>8__locals2 = CS$<>8__locals1;
			Predicate<GameObject> predicate;
			if (CS$<>8__locals1.MaxDistance != 0)
			{
				predicate = (GameObject o) => CS$<>8__locals1.<>4__this.DistanceTo(o) <= CS$<>8__locals1.MaxDistance;
			}
			else
			{
				predicate = (GameObject o) => true;
			}
			CS$<>8__locals2.distanceFilter = predicate;
			if (activeZone != null && activeZone.HasObject((GameObject o) => CS$<>8__locals1.leadFilter(o) && CS$<>8__locals1.distanceFilter(o) && CS$<>8__locals1.Filter(o)))
			{
				return true;
			}
			foreach (Zone zone in zoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					Zone zone2 = zone;
					Predicate<GameObject> predicate2;
					if ((predicate2 = CS$<>8__locals1.<>9__4) == null)
					{
						predicate2 = (CS$<>8__locals1.<>9__4 = (GameObject o) => CS$<>8__locals1.leadFilter(o) && CS$<>8__locals1.distanceFilter(o) && CS$<>8__locals1.Filter(o));
					}
					if (zone2.HasObject(predicate2))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x001D9E08 File Offset: 0x001D8008
		public void GetCompanions(List<GameObject> Store, int MaxDistance = 0, Predicate<GameObject> Filter = null)
		{
			ZoneManager zoneManager = XRL.The.ZoneManager;
			Zone activeZone = zoneManager.ActiveZone;
			if (activeZone != null)
			{
				activeZone.FindObjects(Store, this.IsPlayer() ? new Predicate<GameObject>(GameObject.ObjectIsPlayerLed) : ((GameObject o) => o.IsLedBy(this)));
			}
			foreach (Zone zone in zoneManager.CachedZones.Values)
			{
				if (zone != activeZone)
				{
					zone.FindObjects(Store, this.IsPlayer() ? new Predicate<GameObject>(GameObject.ObjectIsPlayerLed) : ((GameObject o) => o.IsLedBy(this)));
				}
			}
			if (MaxDistance > 0)
			{
				for (int i = Store.Count - 1; i >= 0; i--)
				{
					if (this.DistanceTo(Store[i]) > MaxDistance)
					{
						Store.RemoveAt(i);
					}
				}
			}
			if (Filter != null)
			{
				for (int j = Store.Count - 1; j >= 0; j--)
				{
					if (!Filter(Store[j]))
					{
						Store.RemoveAt(j);
					}
				}
			}
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x001D9F20 File Offset: 0x001D8120
		public List<GameObject> GetCompanions()
		{
			List<GameObject> list = new List<GameObject>();
			this.GetCompanions(list);
			return list;
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x001D9F3C File Offset: 0x001D813C
		public List<GameObject> GetCompanions(int MaxDistance = 0, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = new List<GameObject>();
			this.GetCompanions(list, MaxDistance, Filter);
			return list;
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x001D9F5C File Offset: 0x001D815C
		public List<GameObject> GetCompanionsReadonly()
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetCompanions(list);
			return list;
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x001D9F78 File Offset: 0x001D8178
		public List<GameObject> GetCompanionsReadonly(int MaxDistance = 0, Predicate<GameObject> Filter = null)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetCompanions(list, MaxDistance, Filter);
			return list;
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x001D9F95 File Offset: 0x001D8195
		private static bool ObjectIsPlayerLed(GameObject Object)
		{
			return Object.IsPlayerLed();
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x001D9F9D File Offset: 0x001D819D
		public bool HealsNaturally()
		{
			return HealsNaturallyEvent.Check(this);
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x001D9FA5 File Offset: 0x001D81A5
		public bool CanBeReplicated(GameObject Actor, string Context = null, bool Temporary = false)
		{
			return CanBeReplicatedEvent.Check(this, Actor, Context, Temporary);
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x001D9FB0 File Offset: 0x001D81B0
		public void WantToReequip()
		{
			if (!this.IsPlayer())
			{
				Brain brain = this.Brain;
				if (brain == null)
				{
					return;
				}
				brain.WantToReequip();
			}
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x001D9FCA File Offset: 0x001D81CA
		public int CheckStacks()
		{
			Inventory inventory = this.Inventory;
			if (inventory == null)
			{
				return 0;
			}
			return inventory.CheckStacks();
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x001D9FE0 File Offset: 0x001D81E0
		public void Pool()
		{
			if ((this.Flags & 32) != 0)
			{
				MetricsManager.LogError("!POOLEDOBJECT Attempting to pool a presently serialized object: '" + this.DebugName + "'.");
				return;
			}
			if ((this.Flags & 16) != 0)
			{
				MetricsManager.LogError("Attempting to pool object multiple times.");
				return;
			}
			if (this.IsPlayer() && XRL.The.Game.Running)
			{
				MetricsManager.LogError("Attempting to pool player.");
				return;
			}
			string debugName = this.DebugName;
			ActionManager actionManager = XRL.The.ActionManager;
			if (actionManager != null)
			{
				actionManager.RemoveActiveObject(this);
			}
			try
			{
				this.Clear();
				this.Blueprint = "*PooledObject";
				this.Flags |= 16;
				GameObjectFactory.ObjectPool.Enqueue(this);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Failed to pool object '" + debugName + "'", ex, "game_exception");
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x001DA0BC File Offset: 0x001D82BC
		public static GameObject Get()
		{
			GameObject gameObject;
			if (!GameObjectFactory.ObjectPool.TryDequeue(out gameObject))
			{
				return new GameObject();
			}
			if (gameObject == null)
			{
				XRLCore.LogError("Got null object from gameObjectPool");
				return new GameObject();
			}
			gameObject.Flags = 0;
			return gameObject;
		}

		// Token: 0x04002D82 RID: 11650
		internal static Dictionary<GameObject, List<GameObject.ExternalEventBind>> ExternalLoadBindings = new Dictionary<GameObject, List<GameObject.ExternalEventBind>>();

		// Token: 0x04002D83 RID: 11651
		public static readonly GameObject Invalid = new GameObject();

		// Token: 0x04002D84 RID: 11652
		public static long AutomoveInterruptTurn = -1L;

		// Token: 0x04002D85 RID: 11653
		public static string AutomoveInterruptBecause = "";

		// Token: 0x04002D86 RID: 11654
		private static List<IPart> TurnTickParts = new List<IPart>(8);

		// Token: 0x04002D87 RID: 11655
		private static List<Effect> TargetEffects = new List<Effect>();

		// Token: 0x04002D88 RID: 11656
		private static List<Effect> SameAsEffectsUsed = new List<Effect>(16);

		// Token: 0x04002D89 RID: 11657
		private static long lastWaitTurn = 0L;

		// Token: 0x04002D8A RID: 11658
		private static MissilePath ThrowPath = new MissilePath();

		// Token: 0x04002D8B RID: 11659
		private static bool ThrowPathInUse;

		// Token: 0x04002D8C RID: 11660
		private static Dictionary<string, int> TakeObjectsFromTableGeneration = new Dictionary<string, int>();

		// Token: 0x04002D8D RID: 11661
		private static bool TakeObjectsFromTableGenerationInUse = false;

		// Token: 0x04002D8E RID: 11662
		private static Event eIsMobile = new ImmutableEvent("IsMobile");

		// Token: 0x04002D8F RID: 11663
		private static Event eCanHypersensesDetect = new ImmutableEvent("CanHypersensesDetect");

		// Token: 0x04002D90 RID: 11664
		private static Event eCanChangeBodyPosition = new Event("CanChangeBodyPosition", "To", null, "ShowMessage", 0, "Involuntary", 0);

		// Token: 0x04002D91 RID: 11665
		private static Event eCanMoveExtremities = new Event("CanMoveExtremities", "ShowMessage", 0, "Involuntary", 0);

		// Token: 0x04002D92 RID: 11666
		private static Event eHealing = new Event("Healing", "Amount", 0);

		// Token: 0x04002D93 RID: 11667
		private static Event eCommandTakeObject = new Event("CommandTakeObject").SetParameter("Object", null).SetParameter("Context", null).SetFlag("NoStack", false);

		// Token: 0x04002D94 RID: 11668
		private static Event eCommandTakeObjectSilent = new Event("CommandTakeObject").SetParameter("Object", null).SetParameter("Context", null).SetSilent(true)
			.SetFlag("NoStack", false);

		// Token: 0x04002D95 RID: 11669
		private static Event eCommandTakeObjectWithEnergyCost = new Event("CommandTakeObject").SetParameter("Object", null).SetParameter("Context", null).SetParameter("EnergyCost", 0)
			.SetFlag("NoStack", false);

		// Token: 0x04002D96 RID: 11670
		private static Event eCommandTakeObjectSilentWithEnergyCost = new Event("CommandTakeObject").SetParameter("Object", null).SetParameter("Context", null).SetParameter("EnergyCost", 0)
			.SetSilent(true)
			.SetFlag("NoStack", false);

		// Token: 0x04002D97 RID: 11671
		public int _BaseID;

		// Token: 0x04002D98 RID: 11672
		public int Flags;

		// Token: 0x04002D99 RID: 11673
		public string Blueprint = "Object";

		// Token: 0x04002D9A RID: 11674
		public string GenderName;

		// Token: 0x04002D9B RID: 11675
		public string PronounSetName;

		// Token: 0x04002D9C RID: 11676
		public bool Live;

		// Token: 0x04002D9D RID: 11677
		public XRL.World.Parts.Physics Physics;

		// Token: 0x04002D9E RID: 11678
		public Render Render;

		// Token: 0x04002D9F RID: 11679
		public Brain Brain;

		// Token: 0x04002DA0 RID: 11680
		public Body Body;

		// Token: 0x04002DA1 RID: 11681
		public Inventory Inventory;

		// Token: 0x04002DA2 RID: 11682
		public LiquidVolume LiquidVolume;

		// Token: 0x04002DA3 RID: 11683
		public ActivatedAbilities Abilities;

		// Token: 0x04002DA4 RID: 11684
		public Statistic Energy;

		// Token: 0x04002DA5 RID: 11685
		public Stacker Stacker;

		// Token: 0x04002DA6 RID: 11686
		public PartRack PartsList = new PartRack(8);

		// Token: 0x04002DA7 RID: 11687
		public EffectRack _Effects;

		// Token: 0x04002DA8 RID: 11688
		public Dictionary<string, Statistic> Statistics = new Dictionary<string, Statistic>();

		// Token: 0x04002DA9 RID: 11689
		public Dictionary<string, int> IntProperty = new Dictionary<string, int>();

		// Token: 0x04002DAA RID: 11690
		public Dictionary<string, string> Property = new Dictionary<string, string>();

		// Token: 0x04002DAB RID: 11691
		public Dictionary<string, List<Effect>> RegisteredEffectEvents;

		// Token: 0x04002DAC RID: 11692
		public Dictionary<string, List<IPart>> RegisteredPartEvents;

		// Token: 0x04002DAD RID: 11693
		public EventRegistry RegisteredEvents;

		// Token: 0x04002DAE RID: 11694
		private GameObjectBlueprint _BlueprintCache;

		// Token: 0x04002DAF RID: 11695
		public Dictionary<GameObject, GameObject> DeepCopyInventoryObjectMap;

		// Token: 0x04002DB0 RID: 11696
		private RenderEvent _contextRender;

		// Token: 0x04002DB1 RID: 11697
		public string _CachedDisplayNameForSort;

		// Token: 0x04002DB2 RID: 11698
		private int PartsCascade;

		// Token: 0x04002DB3 RID: 11699
		private int CarriedWeightCache = -1;

		// Token: 0x04002DB4 RID: 11700
		private int MaxCarriedWeightCache = -1;

		// Token: 0x04002DB5 RID: 11701
		private byte TransientCache;

		// Token: 0x04002DB6 RID: 11702
		private bool Dying;

		// Token: 0x04002DB7 RID: 11703
		private const int TRANSIENT_CACHE_WORSHIP_PROCESSED = 1;

		// Token: 0x04002DB8 RID: 11704
		public const int FLAG_PRONOUNS_KNOWN = 1;

		// Token: 0x04002DB9 RID: 11705
		public const int FLAG_COMBAT = 2;

		// Token: 0x04002DBA RID: 11706
		public const int FLAG_GRAVEYARD = 4;

		// Token: 0x04002DBB RID: 11707
		public const int FLAG_TEMPORARY = 8;

		// Token: 0x04002DBC RID: 11708
		public const int FLAG_POOLED = 16;

		// Token: 0x04002DBD RID: 11709
		public const int FLAG_SERIALIZED = 32;

		// Token: 0x04002DBE RID: 11710
		private const int FLAG_PLACEHOLDER = 64;

		// Token: 0x04002DBF RID: 11711
		public const int FLAG_ALWAYS_PAINT = 128;

		// Token: 0x04002DC0 RID: 11712
		public const int FLAG_IMPORTANT_OVERLAY_OBJECT = 256;

		// Token: 0x02001A44 RID: 6724
		public struct ExternalEventBind
		{
			// Token: 0x0600E6BD RID: 59069 RVA: 0x00532C70 File Offset: 0x00530E70
			public ExternalEventBind(string Event, GameObject Object, Type Type)
			{
				this.Event = Event;
				this.Object = Object;
				this.Type = Type;
			}

			// Token: 0x0400687C RID: 26748
			public string Event;

			// Token: 0x0400687D RID: 26749
			public GameObject Object;

			// Token: 0x0400687E RID: 26750
			public Type Type;
		}

		// Token: 0x02001A45 RID: 6725
		public class DisplayNameSort : Comparer<GameObject>
		{
			// Token: 0x0600E6BE RID: 59070 RVA: 0x00532C88 File Offset: 0x00530E88
			public override int Compare(GameObject a, GameObject b)
			{
				return ConsoleLib.Console.ColorUtility.CompareExceptFormattingAndCase(a.GetDisplayName(int.MaxValue, null, null, false, false, false, true, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true), b.GetDisplayName(int.MaxValue, null, null, false, false, false, true, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true));
			}
		}

		// Token: 0x02001A46 RID: 6726
		private struct PullDownChoice
		{
			// Token: 0x0400687F RID: 26751
			public string location;

			// Token: 0x04006880 RID: 26752
			public int X;

			// Token: 0x04006881 RID: 26753
			public int Y;
		}
	}
}
