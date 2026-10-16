using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006E9 RID: 1769
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Singleton)]
	public class CommandReloadEvent : SingletonEvent<CommandReloadEvent>
	{
		// Token: 0x06004A43 RID: 19011 RVA: 0x00173E57 File Offset: 0x00172057
		public override int GetCascadeLevel()
		{
			return CommandReloadEvent.CascadeLevel;
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x00173E5E File Offset: 0x0017205E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x00173E68 File Offset: 0x00172068
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Weapon = null;
			this.LastAmmo = null;
			this.CheckedForReload.Clear();
			this.NeededReload.Clear();
			this.TriedToReload.Clear();
			this.Reloaded.Clear();
			this.ObjectsReloaded.Clear();
			this.FreeAction = false;
			this.FromDialog = false;
			this.MinimumCharge = 0;
			this.MaxEnergyCost = 0;
			this.TotalEnergyCost = 0;
			this.Pass = 0;
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x00173EF1 File Offset: 0x001720F1
		public void EnergyCost(int amount)
		{
			this.TotalEnergyCost += amount;
			if (amount > this.MaxEnergyCost)
			{
				this.MaxEnergyCost = amount;
			}
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00173F14 File Offset: 0x00172114
		public static bool Execute(GameObject Actor, bool FreeAction = false, bool FromDialog = false, int MinimumCharge = 0)
		{
			if (Actor.WantEvent(SingletonEvent<CommandReloadEvent>.ID, CommandReloadEvent.CascadeLevel))
			{
				if (!Actor.CanMoveExtremities("Reload", true, false, true))
				{
					return false;
				}
				SingletonEvent<CommandReloadEvent>.Instance.Reset();
				SingletonEvent<CommandReloadEvent>.Instance.Actor = Actor;
				SingletonEvent<CommandReloadEvent>.Instance.Weapon = null;
				SingletonEvent<CommandReloadEvent>.Instance.FreeAction = FreeAction;
				SingletonEvent<CommandReloadEvent>.Instance.FromDialog = FromDialog;
				SingletonEvent<CommandReloadEvent>.Instance.MinimumCharge = MinimumCharge;
				for (int i = 1; i <= 3; i++)
				{
					SingletonEvent<CommandReloadEvent>.Instance.Pass = i;
					if (!Actor.HandleEvent(SingletonEvent<CommandReloadEvent>.Instance))
					{
						return false;
					}
				}
				if (!FreeAction)
				{
					Actor.UseEnergy(SingletonEvent<CommandReloadEvent>.Instance.MaxEnergyCost, "Reload", null, null, false);
				}
			}
			return true;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00173FD4 File Offset: 0x001721D4
		public static bool Execute(GameObject Actor, GameObject Weapon, GameObject LastAmmo = null, bool FreeAction = false, bool FromDialog = false, int MinimumCharge = 0)
		{
			if (Weapon.WantEvent(SingletonEvent<CommandReloadEvent>.ID, CommandReloadEvent.CascadeLevel))
			{
				if (!Actor.CanMoveExtremities("Reload", true, false, true))
				{
					return false;
				}
				SingletonEvent<CommandReloadEvent>.Instance.Reset();
				SingletonEvent<CommandReloadEvent>.Instance.Actor = Actor;
				SingletonEvent<CommandReloadEvent>.Instance.Weapon = Weapon;
				SingletonEvent<CommandReloadEvent>.Instance.LastAmmo = LastAmmo;
				SingletonEvent<CommandReloadEvent>.Instance.FreeAction = FreeAction;
				SingletonEvent<CommandReloadEvent>.Instance.FromDialog = FromDialog;
				SingletonEvent<CommandReloadEvent>.Instance.MinimumCharge = MinimumCharge;
				for (int i = 1; i <= 3; i++)
				{
					SingletonEvent<CommandReloadEvent>.Instance.Pass = i;
					if (!Weapon.HandleEvent(SingletonEvent<CommandReloadEvent>.Instance))
					{
						return false;
					}
				}
				if (!FreeAction)
				{
					Actor.UseEnergy(SingletonEvent<CommandReloadEvent>.Instance.MaxEnergyCost, "Reload", null, null, false);
				}
			}
			return true;
		}

		// Token: 0x040023C0 RID: 9152
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040023C1 RID: 9153
		public const int PASSES = 3;

		// Token: 0x040023C2 RID: 9154
		public GameObject Actor;

		// Token: 0x040023C3 RID: 9155
		public GameObject Weapon;

		// Token: 0x040023C4 RID: 9156
		public GameObject LastAmmo;

		// Token: 0x040023C5 RID: 9157
		public List<IComponent<GameObject>> CheckedForReload = new List<IComponent<GameObject>>();

		// Token: 0x040023C6 RID: 9158
		public List<IComponent<GameObject>> NeededReload = new List<IComponent<GameObject>>();

		// Token: 0x040023C7 RID: 9159
		public List<IComponent<GameObject>> TriedToReload = new List<IComponent<GameObject>>();

		// Token: 0x040023C8 RID: 9160
		public List<IComponent<GameObject>> Reloaded = new List<IComponent<GameObject>>();

		// Token: 0x040023C9 RID: 9161
		public List<GameObject> ObjectsReloaded = new List<GameObject>();

		// Token: 0x040023CA RID: 9162
		public bool FreeAction;

		// Token: 0x040023CB RID: 9163
		public bool FromDialog;

		// Token: 0x040023CC RID: 9164
		public int MinimumCharge;

		// Token: 0x040023CD RID: 9165
		public int MaxEnergyCost;

		// Token: 0x040023CE RID: 9166
		public int TotalEnergyCost;

		// Token: 0x040023CF RID: 9167
		public int Pass;
	}
}
