using System;
using System.CodeDom.Compiler;
using ConsoleLib.Console;
using Occult.Engine.CodeGeneration;
using XRL.Messages;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000DC9 RID: 3529
	[GenerateSerializationPartial]
	[Serializable]
	public class ActivatedAbilityEntry : IEquatable<ActivatedAbilityEntry>, IComposite
	{
		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06008F40 RID: 36672 RVA: 0x0033A24F File Offset: 0x0033844F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008F41 RID: 36673 RVA: 0x0033A254 File Offset: 0x00338454
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.Write(this.ID);
			Writer.WriteOptimized(this.DisplayName);
			Writer.WriteOptimized(this.Command);
			Writer.WriteOptimized(this.Class);
			Writer.WriteOptimized(this.Description);
			Writer.WriteOptimized(this.Icon);
			Writer.WriteOptimized(this.DisabledMessage);
			Writer.WriteOptimized(this.Flags);
			Writer.WriteTokenized(this.CommandCooldown);
			Writer.Write(this.UITileDefault);
			Writer.Write(this.UITileToggleOn);
			Writer.Write(this.UITileDisabled);
			Writer.Write(this.UITileCoolingDown);
			Writer.WriteOptimized(this._DescriptionCommand);
		}

		// Token: 0x06008F42 RID: 36674 RVA: 0x0033A30C File Offset: 0x0033850C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.ID = Reader.ReadGuid();
			this.DisplayName = Reader.ReadOptimizedString();
			this.Command = Reader.ReadOptimizedString();
			this.Class = Reader.ReadOptimizedString();
			this.Description = Reader.ReadOptimizedString();
			this.Icon = Reader.ReadOptimizedString();
			this.DisabledMessage = Reader.ReadOptimizedString();
			this.Flags = Reader.ReadOptimizedInt32();
			this.CommandCooldown = (CommandCooldown)Reader.ReadTokenized();
			this.UITileDefault = (Renderable)Reader.ReadComposite();
			this.UITileToggleOn = (Renderable)Reader.ReadComposite();
			this.UITileDisabled = (Renderable)Reader.ReadComposite();
			this.UITileCoolingDown = (Renderable)Reader.ReadComposite();
			this._DescriptionCommand = Reader.ReadOptimizedString();
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06008F43 RID: 36675 RVA: 0x0033A3DA File Offset: 0x003385DA
		// (set) Token: 0x06008F44 RID: 36676 RVA: 0x0033A3E8 File Offset: 0x003385E8
		public bool Toggleable
		{
			get
			{
				return this.Flags.HasBit(4);
			}
			set
			{
				(ref this.Flags).SetBit(4, value);
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06008F45 RID: 36677 RVA: 0x0033A3F7 File Offset: 0x003385F7
		// (set) Token: 0x06008F46 RID: 36678 RVA: 0x0033A405 File Offset: 0x00338605
		public bool ToggleState
		{
			get
			{
				return this.Flags.HasBit(8);
			}
			set
			{
				(ref this.Flags).SetBit(8, value);
			}
		}

		/// <summary>
		///     Though not nessecarily <see cref="P:XRL.World.Parts.ActivatedAbilityEntry.Toggleable" />, this ability uses ToggleState to show the "Active" part of its
		///     Cooldown, and should be allowed to Toggle Off while active (and on cooldown).
		/// </summary>
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06008F47 RID: 36679 RVA: 0x0033A414 File Offset: 0x00338614
		// (set) Token: 0x06008F48 RID: 36680 RVA: 0x0033A423 File Offset: 0x00338623
		public bool ActiveToggle
		{
			get
			{
				return this.Flags.HasBit(16);
			}
			set
			{
				(ref this.Flags).SetBit(16, value);
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06008F49 RID: 36681 RVA: 0x0033A433 File Offset: 0x00338633
		// (set) Token: 0x06008F4A RID: 36682 RVA: 0x0033A441 File Offset: 0x00338641
		public bool Enabled
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06008F4B RID: 36683 RVA: 0x0033A450 File Offset: 0x00338650
		// (set) Token: 0x06008F4C RID: 36684 RVA: 0x0033A45F File Offset: 0x0033865F
		public bool IsAttack
		{
			get
			{
				return this.Flags.HasBit(64);
			}
			set
			{
				(ref this.Flags).SetBit(64, value);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06008F4D RID: 36685 RVA: 0x0033A46F File Offset: 0x0033866F
		// (set) Token: 0x06008F4E RID: 36686 RVA: 0x0033A481 File Offset: 0x00338681
		public bool IsRealityDistortionBased
		{
			get
			{
				return this.Flags.HasBit(128);
			}
			set
			{
				(ref this.Flags).SetBit(128, value);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06008F4F RID: 36687 RVA: 0x0033A494 File Offset: 0x00338694
		// (set) Token: 0x06008F50 RID: 36688 RVA: 0x0033A4A6 File Offset: 0x003386A6
		public bool IsWorldMapUsable
		{
			get
			{
				return this.Flags.HasBit(1024);
			}
			set
			{
				(ref this.Flags).SetBit(1024, value);
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06008F51 RID: 36689 RVA: 0x0033A4B9 File Offset: 0x003386B9
		// (set) Token: 0x06008F52 RID: 36690 RVA: 0x0033A4CB File Offset: 0x003386CB
		public bool AIDisable
		{
			get
			{
				return this.Flags.HasBit(2048);
			}
			set
			{
				(ref this.Flags).SetBit(2048, value);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06008F53 RID: 36691 RVA: 0x0033A4DE File Offset: 0x003386DE
		// (set) Token: 0x06008F54 RID: 36692 RVA: 0x0033A4ED File Offset: 0x003386ED
		public bool AlwaysAllowToggleOff
		{
			get
			{
				return this.Flags.HasBit(32);
			}
			set
			{
				(ref this.Flags).SetBit(32, value);
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06008F55 RID: 36693 RVA: 0x0033A4FD File Offset: 0x003386FD
		// (set) Token: 0x06008F56 RID: 36694 RVA: 0x0033A50B File Offset: 0x0033870B
		public bool Visible
		{
			get
			{
				return this.Flags.HasBit(2);
			}
			set
			{
				(ref this.Flags).SetBit(2, value);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06008F57 RID: 36695 RVA: 0x0033A51A File Offset: 0x0033871A
		// (set) Token: 0x06008F58 RID: 36696 RVA: 0x0033A52C File Offset: 0x0033872C
		public bool AffectedByWillpower
		{
			get
			{
				return this.Flags.HasBit(256);
			}
			set
			{
				(ref this.Flags).SetBit(256, value);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06008F59 RID: 36697 RVA: 0x0033A53F File Offset: 0x0033873F
		// (set) Token: 0x06008F5A RID: 36698 RVA: 0x0033A551 File Offset: 0x00338751
		public bool TickPerTurn
		{
			get
			{
				return this.Flags.HasBit(512);
			}
			set
			{
				(ref this.Flags).SetBit(512, value);
			}
		}

		/// <summary>
		///     The description and icon entry data to use from the <see cref="F:XRL.World.Parts.ActivatedAbilities.XmlData.DataByCommand" />.
		///     Defaults to <see cref="!:CommandForHotkey" />, should probably just be the same unless you want to share hotkeys,
		///     but have different icon and description, such as the case of rocket skates and sprint. 
		///
		///     <para>
		///         Changing this value will cause all UITile attributes to be reset to null and filled in with the XmlData defaults
		///         on next access.
		///     </para>
		/// </summary>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06008F5B RID: 36699 RVA: 0x0033A564 File Offset: 0x00338764
		// (set) Token: 0x06008F5C RID: 36700 RVA: 0x0033A58A File Offset: 0x0033878A
		public string CommandForDescription
		{
			get
			{
				string text;
				if ((text = this._DescriptionCommand) == null)
				{
					text = (this._DescriptionCommand = this.Command);
				}
				return text;
			}
			set
			{
				this._DescriptionCommand = value;
				this.UITileCoolingDown = null;
				this.UITileDefault = null;
				this.UITileDisabled = null;
				this.UITileToggleOn = null;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06008F5D RID: 36701 RVA: 0x0033A5AF File Offset: 0x003387AF
		public GameObject ParentObject
		{
			get
			{
				return this.Abilities._ParentObject;
			}
		}

		// Token: 0x06008F5E RID: 36702 RVA: 0x0033A5BC File Offset: 0x003387BC
		public ActivatedAbilityEntry()
		{
		}

		// Token: 0x06008F5F RID: 36703 RVA: 0x0033A5CF File Offset: 0x003387CF
		public ActivatedAbilityEntry(ActivatedAbilityEntry Source)
			: this()
		{
			this.CopyFrom(Source);
		}

		// Token: 0x06008F60 RID: 36704 RVA: 0x0033A5E0 File Offset: 0x003387E0
		public ActivatedAbilityEntry Clear()
		{
			this.ID = Guid.Empty;
			this.DisplayName = null;
			this.Command = null;
			this.Class = null;
			this.Description = null;
			this.Icon = null;
			this.DisabledMessage = null;
			this.Flags = 0;
			if (this.CommandCooldown != null)
			{
				this.CommandCooldown.Command = null;
				this.CommandCooldown.Segments = 0;
			}
			this.CommandForDescription = null;
			this.UITileDefault = null;
			this.UITileCoolingDown = null;
			this.UITileDisabled = null;
			this.UITileToggleOn = null;
			this._Data = null;
			return this;
		}

		// Token: 0x06008F61 RID: 36705 RVA: 0x0033A674 File Offset: 0x00338874
		public ActivatedAbilityEntry CopyFrom(ActivatedAbilityEntry Source)
		{
			this.ID = Source.ID;
			this.DisplayName = Source.DisplayName;
			this.Command = Source.Command;
			this.Class = Source.Class;
			this.Description = Source.Description;
			this.Icon = Source.Icon;
			this.DisabledMessage = Source.DisabledMessage;
			this.Flags = Source.Flags;
			if (this.CommandCooldown == null)
			{
				this.CommandCooldown = new CommandCooldown();
			}
			CommandCooldown commandCooldown = this.CommandCooldown;
			CommandCooldown commandCooldown2 = Source.CommandCooldown;
			commandCooldown.Command = ((commandCooldown2 != null) ? commandCooldown2.Command : null);
			CommandCooldown commandCooldown3 = this.CommandCooldown;
			CommandCooldown commandCooldown4 = Source.CommandCooldown;
			commandCooldown3.Segments = ((commandCooldown4 != null) ? commandCooldown4.Segments : 0);
			this.CommandForDescription = Source.CommandForDescription;
			this.UITileDefault = Source.UITileDefault;
			this.UITileCoolingDown = Source.UITileCoolingDown;
			this.UITileDisabled = Source.UITileDisabled;
			this.UITileToggleOn = Source.UITileToggleOn;
			this._Data = null;
			return this;
		}

		// Token: 0x06008F62 RID: 36706 RVA: 0x0033A772 File Offset: 0x00338972
		public ActivatedAbilities.XmlData.Data GetData()
		{
			if (this._Data == null)
			{
				ActivatedAbilities.XmlData.DataByCommand.TryGetValue(this.CommandForDescription, out this._Data);
			}
			return this._Data;
		}

		// Token: 0x06008F63 RID: 36707 RVA: 0x0033A79C File Offset: 0x0033899C
		public bool TryGetTag(string Name, out string Value)
		{
			ActivatedAbilities.XmlData.Data data = this.GetData();
			if (data == null)
			{
				Value = null;
				return false;
			}
			return data.TryGetTag(Name, out Value);
		}

		// Token: 0x06008F64 RID: 36708 RVA: 0x0033A7C0 File Offset: 0x003389C0
		public bool HasTag(string Name)
		{
			ActivatedAbilities.XmlData.Data data = this.GetData();
			return data != null && data.HasTag(Name);
		}

		// Token: 0x06008F65 RID: 36709 RVA: 0x0033A7E0 File Offset: 0x003389E0
		public Renderable GetUITile()
		{
			ActivatedAbilities.XmlData.Data data = this.GetData();
			if (data != null)
			{
				Renderable renderable;
				if (this.UITileDefault == null && data.UITiles.TryGetValue(ActivatedAbilities.XmlData.UITileStates.Default, out renderable))
				{
					this.UITileDefault = renderable;
				}
				Renderable renderable2;
				if (this.UITileCoolingDown == null && data.UITiles.TryGetValue(ActivatedAbilities.XmlData.UITileStates.CoolingDown, out renderable2))
				{
					this.UITileCoolingDown = renderable2;
				}
				Renderable renderable3;
				if (this.UITileDisabled == null && data.UITiles.TryGetValue(ActivatedAbilities.XmlData.UITileStates.CoolingDown, out renderable3))
				{
					this.UITileDisabled = renderable3;
				}
				Renderable renderable4;
				if (this.UITileToggleOn == null && data.UITiles.TryGetValue(ActivatedAbilities.XmlData.UITileStates.ToggleOn, out renderable4))
				{
					this.UITileToggleOn = renderable4;
				}
			}
			if (this.UITileDefault == null)
			{
				this.UITileDefault = new Renderable(null, this.Icon ?? "", "&w", null, 'W');
			}
			if (this.ToggleState)
			{
				Renderable renderable5;
				if ((renderable5 = this.UITileToggleOn) == null)
				{
					string tile = this.UITileDefault.Tile;
					string text = ((tile != null) ? tile.Replace("_off", "_on") : null);
					string renderString = this.UITileDefault.RenderString;
					string colorString = this.UITileDefault.ColorString;
					renderable5 = (this.UITileToggleOn = new Renderable(text, renderString, (colorString != null) ? colorString.ToUpper() : null, null, char.ToUpper(this.UITileDefault.DetailColor)));
				}
				return renderable5;
			}
			if (this.Cooldown > 0)
			{
				Renderable renderable6;
				if ((renderable6 = this.UITileCoolingDown) == null)
				{
					renderable6 = (this.UITileCoolingDown = new Renderable(this.UITileDefault, null, null, "&c", "&c", new char?('c')));
				}
				return renderable6;
			}
			if (!this.Enabled)
			{
				Renderable renderable7;
				if ((renderable7 = this.UITileDisabled) == null)
				{
					renderable7 = (this.UITileDisabled = new Renderable(this.UITileDefault, null, null, "&K", "&K", new char?('K')));
				}
				return renderable7;
			}
			return this.UITileDefault;
		}

		// Token: 0x06008F66 RID: 36710 RVA: 0x0033A998 File Offset: 0x00338B98
		public bool Equals(ActivatedAbilityEntry Entry)
		{
			return Entry != null && this.ID == Entry.ID && this.DisplayName == Entry.DisplayName && this.Command == Entry.Command && this.CommandForDescription == Entry.CommandForDescription && this.Class == Entry.Class && this.Description == Entry.Description && this.Icon == Entry.Icon && this.DisabledMessage == Entry.DisabledMessage && this.Toggleable == Entry.Toggleable && this.ToggleState == Entry.ToggleState && this.ActiveToggle == Entry.ActiveToggle && this.IsAttack == Entry.IsAttack && this.IsRealityDistortionBased == Entry.IsRealityDistortionBased && this.AlwaysAllowToggleOff == Entry.AlwaysAllowToggleOff && this.Visible == Entry.Visible && this.AffectedByWillpower == Entry.AffectedByWillpower && this.TickPerTurn == Entry.TickPerTurn && this.Cooldown == Entry.Cooldown && this.GetUITile() == Entry.GetUITile();
		}

		// Token: 0x06008F67 RID: 36711 RVA: 0x0033AAFA File Offset: 0x00338CFA
		public override bool Equals(object Object)
		{
			return this.Equals(Object as ActivatedAbilityEntry);
		}

		// Token: 0x06008F68 RID: 36712 RVA: 0x0033AB08 File Offset: 0x00338D08
		public override int GetHashCode()
		{
			return new
			{
				this.ID, this.DisplayName, this.Command, this.CommandForDescription, this.Class, this.Description, this.Icon, this.DisabledMessage, this.Toggleable, this.ActiveToggle,
				this.IsAttack, this.IsRealityDistortionBased, this.AlwaysAllowToggleOff, this.Visible, this.AffectedByWillpower, this.TickPerTurn
			}.GetHashCode();
		}

		// Token: 0x06008F69 RID: 36713 RVA: 0x0033AB80 File Offset: 0x00338D80
		public int GetBaseHashCode()
		{
			return new { this.Command, this.Class, this.Toggleable, this.ActiveToggle, this.IsAttack, this.IsRealityDistortionBased, this.AlwaysAllowToggleOff, this.Visible, this.AffectedByWillpower, this.TickPerTurn }.GetHashCode();
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06008F6B RID: 36715 RVA: 0x0033AC35 File Offset: 0x00338E35
		// (set) Token: 0x06008F6A RID: 36714 RVA: 0x0033ABD4 File Offset: 0x00338DD4
		public int Cooldown
		{
			get
			{
				if (!this.AlwaysAllowToggleOff || !this.ToggleState || !this.Toggleable)
				{
					return this.CommandCooldown.Segments;
				}
				return 0;
			}
			set
			{
				if (this.CommandCooldown.Segments <= 0 && value > 0)
				{
					this.Abilities.AddCooldown(this.CommandCooldown);
				}
				else if (this.CommandCooldown.Segments > 0 && value <= 0)
				{
					this.Abilities.RemoveCooldown(this.CommandCooldown);
				}
				this.CommandCooldown.Segments = value;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06008F6C RID: 36716 RVA: 0x0033AC5C File Offset: 0x00338E5C
		[Obsolete("Use CooldownRounds instead of CooldownTurns")]
		public int CooldownTurns
		{
			get
			{
				return this.CooldownRounds;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06008F6D RID: 36717 RVA: 0x0033AC64 File Offset: 0x00338E64
		public int CooldownRounds
		{
			get
			{
				return (int)Math.Ceiling((double)this.Cooldown / 10.0);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06008F6E RID: 36718 RVA: 0x0033AC7D File Offset: 0x00338E7D
		public string CooldownDescription
		{
			get
			{
				return this.CooldownRounds.Things("round", null);
			}
		}

		// Token: 0x06008F6F RID: 36719 RVA: 0x0033AC90 File Offset: 0x00338E90
		public override string ToString()
		{
			string text = string.Concat(new string[]
			{
				this.DisplayName,
				" (",
				this.Command,
				") ",
				this.Class,
				"/",
				this.DisplayName,
				" [",
				this.ID.ToString(),
				"]"
			});
			if (!this.Enabled)
			{
				text += " {{K|[disabled]}}";
			}
			return text;
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06008F70 RID: 36720 RVA: 0x0033AD20 File Offset: 0x00338F20
		public bool IsUsable
		{
			get
			{
				return this.Enabled && (this.Cooldown <= 0 || (this.ToggleState && this.ActiveToggle));
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06008F71 RID: 36721 RVA: 0x0033AD48 File Offset: 0x00338F48
		public bool IsAIUsable
		{
			get
			{
				return this.IsUsable && !this.AIDisable;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06008F72 RID: 36722 RVA: 0x0033AD60 File Offset: 0x00338F60
		public string NotUsableDescription
		{
			get
			{
				if (!this.Enabled)
				{
					if (!this.DisabledMessage.IsNullOrEmpty())
					{
						return this.DisabledMessage;
					}
					return Markup.Color("C", this.DisplayName) + " can't be used at this time.";
				}
				else
				{
					if (this.Cooldown > 0 && (!this.ToggleState || !this.ActiveToggle))
					{
						return string.Concat(new string[]
						{
							"You must wait ",
							Markup.Color("C", this.CooldownDescription),
							" before using ",
							Markup.Color("C", this.DisplayName),
							"."
						});
					}
					return null;
				}
			}
		}

		// Token: 0x06008F73 RID: 36723 RVA: 0x0033AE08 File Offset: 0x00339008
		public void TrySendCommandEventOnPlayer()
		{
			string executeCommand = this.Command;
			string failMessage = this.NotUsableDescription;
			GameManager.Instance.gameQueue.queueTask(delegate
			{
				if (The.Player.OnWorldMap() && !AbilityManager.IsWorldMapUsable(executeCommand))
				{
					MessageQueue.AddPlayerMessage("You cannot do that on the world map.", null, true);
					return;
				}
				if (failMessage == null)
				{
					CommandEvent.Send(The.Player, executeCommand, null, null, 0, false, false, null);
					return;
				}
				MessageQueue.AddPlayerMessage(failMessage, null, true);
			}, 0);
		}

		/// <param name="Source">The source of the ability refresh, e.g. a mutation, skill, or item.</param>
		// Token: 0x06008F74 RID: 36724 RVA: 0x0033AE50 File Offset: 0x00339050
		public void Refresh(object Source = null)
		{
			this.Cooldown = 0;
			XRLGame game = The.Game;
			long num = ((game != null) ? game.Segments : (-1L));
			if (num != ActivatedAbilityEntry.SoundSegment)
			{
				ActivatedAbilities abilities = this.Abilities;
				if (abilities != null)
				{
					abilities.PlayWorldSound("sfx_characterTrigger_cooldown_refreshed", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				}
				ActivatedAbilityEntry.SoundSegment = num;
			}
		}

		// Token: 0x06008F75 RID: 36725 RVA: 0x0033AEBC File Offset: 0x003390BC
		public void AddScaledCooldown(int Cooldown)
		{
			if (The.Core.cool && this.ParentObject != null && this.ParentObject.IsPlayer())
			{
				this.Cooldown = 0;
				return;
			}
			int @for = GetCooldownEvent.GetFor(this.ParentObject, this, Cooldown);
			this.Cooldown += @for;
		}

		// Token: 0x06008F76 RID: 36726 RVA: 0x0033AF10 File Offset: 0x00339110
		public void SetScaledCooldown(int Cooldown)
		{
			if (The.Core.cool && this.ParentObject != null && this.ParentObject.IsPlayer())
			{
				this.Cooldown = 0;
				return;
			}
			int num = GetCooldownEvent.GetFor(this.ParentObject, this, Cooldown);
			if (this.TickPerTurn)
			{
				num += 10;
			}
			else
			{
				GameObject parentObject = this.ParentObject;
				if (((parentObject != null) ? parentObject.Energy : null) != null)
				{
					double num2 = (1000.0 - (double)this.ParentObject.Energy.Value) / (double)((this.ParentObject.Speed > 0) ? this.ParentObject.Speed : 100);
					num += Math.Max(0, (int)Math.Ceiling(num2));
				}
			}
			this.Cooldown = num;
		}

		// Token: 0x04003BB3 RID: 15283
		public const int FLAG_ENABLED = 1;

		// Token: 0x04003BB4 RID: 15284
		public const int FLAG_VISIBLE = 2;

		// Token: 0x04003BB5 RID: 15285
		public const int FLAG_TOGGLE = 4;

		// Token: 0x04003BB6 RID: 15286
		public const int FLAG_TOGGLE_STATE = 8;

		// Token: 0x04003BB7 RID: 15287
		public const int FLAG_TOGGLE_ACTIVE = 16;

		// Token: 0x04003BB8 RID: 15288
		public const int FLAG_TOGGLE_ALLOW_OFF = 32;

		// Token: 0x04003BB9 RID: 15289
		public const int FLAG_ATTACK = 64;

		// Token: 0x04003BBA RID: 15290
		public const int FLAG_REALITY_DISTORT = 128;

		// Token: 0x04003BBB RID: 15291
		public const int FLAG_WILLPOWER = 256;

		// Token: 0x04003BBC RID: 15292
		public const int FLAG_TICK_TURN = 512;

		// Token: 0x04003BBD RID: 15293
		public const int FLAG_WORLD_MAP = 1024;

		// Token: 0x04003BBE RID: 15294
		public const int FLAG_AI_DISABLE = 2048;

		// Token: 0x04003BBF RID: 15295
		public Guid ID;

		// Token: 0x04003BC0 RID: 15296
		public string DisplayName;

		// Token: 0x04003BC1 RID: 15297
		public string Command;

		// Token: 0x04003BC2 RID: 15298
		public string Class;

		// Token: 0x04003BC3 RID: 15299
		public string Description;

		// Token: 0x04003BC4 RID: 15300
		public string Icon;

		// Token: 0x04003BC5 RID: 15301
		public string DisabledMessage;

		// Token: 0x04003BC6 RID: 15302
		public int Flags = 289;

		// Token: 0x04003BC7 RID: 15303
		public CommandCooldown CommandCooldown;

		// Token: 0x04003BC8 RID: 15304
		public Renderable UITileDefault;

		// Token: 0x04003BC9 RID: 15305
		public Renderable UITileToggleOn;

		// Token: 0x04003BCA RID: 15306
		public Renderable UITileDisabled;

		// Token: 0x04003BCB RID: 15307
		public Renderable UITileCoolingDown;

		// Token: 0x04003BCC RID: 15308
		[NonSerialized]
		public string DisplayForHotkey;

		/// <summary>
		///     This property is only public to make it easy to serialize, please use <see cref="P:XRL.World.Parts.ActivatedAbilityEntry.CommandForDescription" /> instead.
		/// </summary>
		// Token: 0x04003BCD RID: 15309
		public string _DescriptionCommand;

		// Token: 0x04003BCE RID: 15310
		[NonSerialized]
		public ActivatedAbilities Abilities;

		// Token: 0x04003BCF RID: 15311
		[NonSerialized]
		private ActivatedAbilities.XmlData.Data _Data;

		// Token: 0x04003BD0 RID: 15312
		private static long SoundSegment = -1L;
	}
}
