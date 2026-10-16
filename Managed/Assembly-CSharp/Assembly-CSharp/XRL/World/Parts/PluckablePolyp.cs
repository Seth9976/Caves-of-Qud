using System;
using XRL.Collections;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200127A RID: 4730
	[HasModSensitiveStaticCache]
	[Serializable]
	public class PluckablePolyp : IPart
	{
		// Token: 0x0600BB45 RID: 47941 RVA: 0x004463AE File Offset: 0x004445AE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB46 RID: 47942 RVA: 0x004463B4 File Offset: 0x004445B4
		public override void FinalizeRead(SerializationReader Reader)
		{
			base.FinalizeRead(Reader);
			if (Reader.FileVersion < 409 && !this.Plucked)
			{
				Render render = this.ParentObject.Render;
				render.ColorString = "&C";
				string tile = render.Tile;
				render.Tile = ((tile != null) ? tile.Replace("_both_", "_metal_") : null);
				render.DisplayName = render.DisplayName.Replace(" with coral growth", "");
			}
		}

		// Token: 0x0600BB47 RID: 47943 RVA: 0x0044642F File Offset: 0x0044462F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600BB48 RID: 47944 RVA: 0x00446468 File Offset: 0x00444668
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.Plucked && this.CanBePlucked(E.Actor))
			{
				E.AddAction("Pluck", "pluck", PluckablePolyp.COMMAND_NAME, null, 'p', false, 50, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB49 RID: 47945 RVA: 0x004464B6 File Offset: 0x004446B6
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == PluckablePolyp.COMMAND_NAME)
			{
				this.Pluck(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB4A RID: 47946 RVA: 0x004464E0 File Offset: 0x004446E0
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (!this.Plucked)
			{
				GameObject @object = E.Object;
				if (((@object != null) ? @object.Brain : null) != null && this.CanBePlucked(E.Object) && !E.Object.HasPropertyOrTag("Polypwalking"))
				{
					this.Pluck(E.Object);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB4B RID: 47947 RVA: 0x0044653C File Offset: 0x0044473C
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command == null && !this.Plucked && this.CanBePlucked(E.Actor))
			{
				E.Command = PluckablePolyp.COMMAND_NAME;
				E.AllowRetry = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB4C RID: 47948 RVA: 0x00446588 File Offset: 0x00444788
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.Plucked)
			{
				E.AddWithClause("coral growth");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB4D RID: 47949 RVA: 0x004465A4 File Offset: 0x004447A4
		public override bool Render(RenderEvent E)
		{
			if (!this.Plucked)
			{
				if (!E.Tile.IsNullOrEmpty())
				{
					string tile = this.ParentObject.Render.Tile;
					if (E.Tile == tile)
					{
						string text;
						if (!PluckablePolyp._pluckableTileCache.TryGetValue(tile, out text))
						{
							if (tile.Contains("_metal_"))
							{
								text = tile.Replace("_metal_", "_both_");
							}
							PluckablePolyp._pluckableTileCache.Add(tile, text);
						}
						if (text != null)
						{
							E.Tile = text;
						}
					}
				}
				else if (E.ColorsVisible)
				{
					E.ColorString = "&r";
				}
			}
			return base.Render(E);
		}

		// Token: 0x0600BB4E RID: 47950 RVA: 0x00446647 File Offset: 0x00444847
		public override bool WantTurnTick()
		{
			return this.Plucked;
		}

		// Token: 0x0600BB4F RID: 47951 RVA: 0x0044664F File Offset: 0x0044484F
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckUnpluck();
		}

		// Token: 0x0600BB50 RID: 47952 RVA: 0x00446658 File Offset: 0x00444858
		public void Pluck(GameObject Actor)
		{
			if (!this.Plucked && this.CanBePlucked(Actor))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				Cell currentCell2 = Actor.GetCurrentCell();
				base.PlayWorldSound("Sounds/Interact/sfx_interact_coralpolyp_pluck", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				IComponent<GameObject>.XDidY(Actor, "pluck", string.Concat(new string[]
				{
					The.Player.IsConfused ? this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) : "a coral polyp",
					" off the strut",
					(currentCell == currentCell2) ? "" : (" " + Actor.DescribeDirectionToward(currentCell, false, false)),
					" and",
					Actor.GetVerb("toss", true, false, false, true, false),
					" it aside"
				}), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				this.Plucked = true;
				this.PluckTime = The.Game.Turns;
				if (Stat.Random(1, this.CacheChance) <= 1)
				{
					if (IComponent<GameObject>.Visible(Actor) && AutoAct.IsInterruptable())
					{
						AutoAct.Interrupt(null, null, null, false);
					}
					if (IComponent<GameObject>.Visible(Actor))
					{
						CombatJuice.playPrefabAnimation(Actor, "Particles/CoralCachePluck", null, null, null, false);
					}
					GameObject gameObject = this.ParentObject.CurrentCell.AddObject(this.RevealObject, null, null, null, null);
					IComponent<GameObject>.XDidYToZ(Actor, "reveal", gameObject, null, null, null, null, Actor, null, false, false, true, false, false, null, null, null, false, false, false, false, Actor.IsPlayer(), null);
					return;
				}
				if (IComponent<GameObject>.Visible(Actor))
				{
					CombatJuice.playPrefabAnimation(Actor, "Particles/CoralPluck", null, null, null, false);
				}
			}
		}

		// Token: 0x0600BB51 RID: 47953 RVA: 0x0044681C File Offset: 0x00444A1C
		public void Unpluck()
		{
			this.Plucked = false;
			this.ParentObject.CurrentCell.GetObjects("PolypCache").ForEach(delegate(GameObject o)
			{
				o.Obliterate(null, false, null);
			});
		}

		// Token: 0x0600BB52 RID: 47954 RVA: 0x00446869 File Offset: 0x00444A69
		public void CheckUnpluck()
		{
			if (this.Plucked && The.Game.Turns - this.PluckTime >= this.RegrowTime)
			{
				this.Unpluck();
			}
		}

		// Token: 0x0600BB53 RID: 47955 RVA: 0x00446894 File Offset: 0x00444A94
		public bool CanBePlucked(GameObject Actor = null)
		{
			Zone currentZone = this.ParentObject.CurrentZone;
			return currentZone != null && currentZone.Z <= 10 && AllowPolypPluckingEvent.Check(currentZone, this.ParentObject, Actor);
		}

		// Token: 0x04004B2C RID: 19244
		public static readonly string COMMAND_NAME = "PluckPolyp";

		// Token: 0x04004B2D RID: 19245
		[ModSensitiveStaticCache(true)]
		private static StringMap<string> _pluckableTileCache = new StringMap<string>();

		// Token: 0x04004B2E RID: 19246
		public bool Plucked;

		// Token: 0x04004B2F RID: 19247
		public long PluckTime;

		// Token: 0x04004B30 RID: 19248
		public long RegrowTime = 3600L;

		// Token: 0x04004B31 RID: 19249
		public int CacheChance = 500;

		// Token: 0x04004B32 RID: 19250
		public string RevealObject = "PolypCache";

		// Token: 0x04004B33 RID: 19251
		[Obsolete]
		public string OldTile;

		// Token: 0x04004B34 RID: 19252
		[Obsolete]
		public string OldDisplayName;

		// Token: 0x04004B35 RID: 19253
		[Obsolete]
		public string PluckSounds;
	}
}
