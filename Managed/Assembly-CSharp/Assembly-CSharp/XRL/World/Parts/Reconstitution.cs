using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200120E RID: 4622
	[Serializable]
	public class Reconstitution : IPart
	{
		// Token: 0x0600B7CB RID: 47051 RVA: 0x0042DAD2 File Offset: 0x0042BCD2
		public override bool WantEvent(int ID, int cascade)
		{
			if (this.Activated)
			{
				return ID == ZoneActivatedEvent.ID || ID == ZoneDeactivatedEvent.ID || ID == ZoneThawedEvent.ID;
			}
			return ID == BeforeDestroyObjectEvent.ID || base.WantEvent(ID, cascade);
		}

		// Token: 0x0600B7CC RID: 47052 RVA: 0x0042DB09 File Offset: 0x0042BD09
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.TryReconstitute(this.Tick);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7CD RID: 47053 RVA: 0x0042DB1F File Offset: 0x0042BD1F
		public override bool HandleEvent(ZoneDeactivatedEvent E)
		{
			this.TryReconstitute(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7CE RID: 47054 RVA: 0x0042DB30 File Offset: 0x0042BD30
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.TryReconstitute(false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7CF RID: 47055 RVA: 0x0042DB41 File Offset: 0x0042BD41
		public override bool WantTurnTick()
		{
			return this.Activated && this.Tick;
		}

		// Token: 0x0600B7D0 RID: 47056 RVA: 0x0042DB53 File Offset: 0x0042BD53
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.TryReconstitute(true);
			base.TurnTick(TimeTick, Amount);
		}

		// Token: 0x0600B7D1 RID: 47057 RVA: 0x0042DB68 File Offset: 0x0042BD68
		public bool TryReconstitute(bool WithEffect = false)
		{
			if (!this.Activated)
			{
				return false;
			}
			if (The.Game.TimeTicks < this.CompleteTurn)
			{
				return false;
			}
			if (this.Object.IsNullOrEmpty())
			{
				this.ParentObject.Obliterate(null, true, null);
				return false;
			}
			GameObject gameObject = (this.Restore ? The.ZoneManager.CachedObjects.PullValue(this.Object, null) : GameObjectFactory.Factory.CreateObject(this.Object));
			if (gameObject == null)
			{
				MetricsManager.LogError("Reconstitution", string.Concat(new string[]
				{
					"No '",
					this.Object,
					"' object found to ",
					this.Restore ? "restore" : "create",
					"."
				}));
				return false;
			}
			GameUnique gameUnique;
			if (gameObject.TryGetPart<GameUnique>(out gameUnique))
			{
				string stringGameState = The.Game.GetStringGameState(gameUnique.State, "");
				if (stringGameState.IsNullOrEmpty() || stringGameState.HasDelimitedSubstring(',', "Dead", StringComparison.Ordinal))
				{
					The.Game.SetStringGameState(gameUnique.State, gameObject.ID);
				}
				if (this.RemoveDeathState && !gameUnique.DeathState.IsNullOrEmpty())
				{
					The.Game.RemoveBooleanGameState(gameUnique.DeathState);
				}
			}
			if (this.Restore)
			{
				gameObject.RestorePristineHealth(false, false);
				gameObject.FireEvent(Event.New("Regenera", "Level", 10, "Source", this.ParentObject));
				RepairedEvent.Send(this.ParentObject, gameObject, this.ParentObject, null, 0);
				if (this.StopFighting)
				{
					gameObject.StopFighting(false);
				}
			}
			if (this.RemoveXP && gameObject.HasStat("XPValue"))
			{
				gameObject.Statistics.Remove("XPValue");
			}
			try
			{
				this.ParseObliterate(gameObject);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Reconstitution", ex, "game_exception");
			}
			this.Object = null;
			this.ParentObject.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.ParentObject.Obliterate(null, true, null);
			if (WithEffect && gameObject.IsVisible())
			{
				gameObject.DustPuff("&y", 15);
				string text = gameObject.GetPropertyOrTag("AmbientIdleSound", null).Coalesce("Sounds/StatusEffects/sfx_statusEffect_budding");
				gameObject.PlayWorldSound(text, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x0600B7D2 RID: 47058 RVA: 0x0042DDE0 File Offset: 0x0042BFE0
		public void ParseObliterate(GameObject Object)
		{
			if (this.Obliterate.IsNullOrEmpty())
			{
				return;
			}
			if (Object.Inventory == null)
			{
				return;
			}
			if (this.Obliterate == "*")
			{
				Object.Inventory.Clear();
				return;
			}
			string[] array = this.Obliterate.Split(',', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				string[] parts = text.Split(':', StringSplitOptions.None);
				if (parts.Length > 1)
				{
					string text2 = parts[0];
					if (!(text2 == "Descends"))
					{
						if (!(text2 == "Inherits"))
						{
							if (!(text2 == "Tag"))
							{
								if (!(text2 == "Property"))
								{
									if (!(text2 == "TagOrProperty"))
									{
										if (text2 == "Part")
										{
											Object.Inventory.RemoveAll((GameObject x) => x.HasPart(parts[1]), true);
										}
									}
									else
									{
										Object.Inventory.RemoveAll((GameObject x) => x.HasTagOrProperty(parts[1]), true);
									}
								}
								else
								{
									Object.Inventory.RemoveAll((GameObject x) => x.HasProperty(parts[1]), true);
								}
							}
							else
							{
								Object.Inventory.RemoveAll((GameObject x) => x.HasTag(parts[1]), true);
							}
						}
						else
						{
							Object.Inventory.RemoveAll((GameObject x) => x.GetBlueprint(true).InheritsFrom(parts[1]), true);
						}
					}
					else
					{
						Object.Inventory.RemoveAll((GameObject x) => x.GetBlueprint(true).DescendsFrom(parts[1]), true);
					}
				}
				else if (parts.Length != 0)
				{
					Object.Inventory.RemoveAll((GameObject x) => x.Blueprint == parts[1], true);
				}
			}
		}

		// Token: 0x0600B7D3 RID: 47059 RVA: 0x0042DF98 File Offset: 0x0042C198
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			if (!this.Activated && !this.ParentObject.IsPlayer())
			{
				if (this.Destroyed && !E.Obliterate)
				{
					return !this.Restore;
				}
				this.Destroyed = true;
				this.DropMarker(null);
				if (this.Restore)
				{
					this.ParentObject.Physics.TeardownForDestroy(false, true);
					The.ZoneManager.CachedObjects[this.ParentObject.ID] = this.ParentObject;
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7D4 RID: 47060 RVA: 0x0042E028 File Offset: 0x0042C228
		public void DropMarker(Cell Cell = null)
		{
			GameObject gameObject = GameObject.Create(this.Marker);
			if ((gameObject.Render == null || !gameObject.Render.Visible) && Cell == null)
			{
				Brain brain = this.ParentObject.Brain;
				Cell cell;
				if (brain == null)
				{
					cell = null;
				}
				else
				{
					GlobalLocation startingCell = brain.StartingCell;
					cell = ((startingCell != null) ? startingCell.ResolveCell() : null);
				}
				Cell = cell;
			}
			Reconstitution reconstitution = (Reconstitution)gameObject.AddPart(this.DeepCopy(gameObject), true, false);
			reconstitution.Activated = true;
			reconstitution.Object = (this.Restore ? this.ParentObject.ID : this.ParentObject.Blueprint);
			reconstitution.CompleteTurn = (this.Turns.IsNullOrEmpty() ? (-1L) : (The.Game.TimeTicks + (long)Stat.Roll(this.Turns, null)));
			if (Cell == null)
			{
				Cell = this.ParentObject.CurrentCell;
			}
			Cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			if (!this.DropMessage.IsNullOrEmpty())
			{
				string text = GameText.VariableReplace(this.DropMessage, this.ParentObject, gameObject, false);
				if (!text.IsNullOrEmpty())
				{
					base.EmitMessage(text, ' ', false, this.DropMessageUsePopup, this.DropMessageAlwaysVisible, null, null);
				}
			}
		}

		/// <summary>A marker to drop on death which can in turn be destroyed to prevent reconstitution, defaults to invisible widget.</summary>
		// Token: 0x040049ED RID: 18925
		public string Marker = "Widget";

		/// <summary>Comma separated filters to obliterate items from inventory on reconstitution.</summary>
		/// <example>Obliterate="Magnetic Bottle,Part:DataDisk,Tag:Golem,Inherits:Raw Meat"</example>
		// Token: 0x040049EE RID: 18926
		public string Obliterate;

		/// <summary>The ID of the original object to retrieve from cache, or blueprint to create.</summary>
		/// <remarks>Do not set manually.</remarks>
		// Token: 0x040049EF RID: 18927
		public string Object;

		/// <summary>Roll of time ticks before reconstitution is available.</summary>
		// Token: 0x040049F0 RID: 18928
		public string Turns;

		/// <summary>The specific XRLGame.TimeTicks to make reconstitution available.</summary>
		/// <remarks>Do not set manually.</remarks>
		// Token: 0x040049F1 RID: 18929
		public long CompleteTurn = -1L;

		/// <summary>Clear GameUnique.DeathState on reconstitution.</summary>
		// Token: 0x040049F2 RID: 18930
		public bool RemoveDeathState = true;

		/// <summary>Do not award additional XP after first death.</summary>
		// Token: 0x040049F3 RID: 18931
		public bool RemoveXP = true;

		/// <summary>Clear hostile memory on reconstitution.</summary>
		// Token: 0x040049F4 RID: 18932
		public bool StopFighting = true;

		/// <summary>Preserve and restore original game object, otherwise create new from blueprint.</summary>
		// Token: 0x040049F5 RID: 18933
		public bool Restore = true;

		/// <summary>Allow reconstituting on turn tick, with effects if the object is visible.</summary>
		// Token: 0x040049F6 RID: 18934
		public bool Tick;

		/// <summary>Message to display when dropping marker.</summary>
		// Token: 0x040049F7 RID: 18935
		public string DropMessage;

		/// <summary>Whether to use a popup for DropMessage.</summary>
		// Token: 0x040049F8 RID: 18936
		public bool DropMessageUsePopup;

		/// <summary>Whether to ignore visibility for DropMessage.</summary>
		// Token: 0x040049F9 RID: 18937
		public bool DropMessageAlwaysVisible;

		/// <summary>Activated reconstitution part on marker.</summary>
		/// <remarks>Do not set manually.</remarks>
		// Token: 0x040049FA RID: 18938
		public bool Activated;

		// Token: 0x040049FB RID: 18939
		[NonSerialized]
		private bool Destroyed;
	}
}
