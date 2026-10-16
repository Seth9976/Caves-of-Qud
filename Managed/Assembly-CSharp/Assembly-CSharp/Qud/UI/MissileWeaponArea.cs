using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.Core;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x02000489 RID: 1161
	public class MissileWeaponArea : MonoBehaviour
	{
		// Token: 0x06002CAB RID: 11435 RVA: 0x000F15A3 File Offset: 0x000EF7A3
		public void OnReloadClicked()
		{
			Keyboard.PushMouseEvent("Command:CmdReload", null);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000F15B0 File Offset: 0x000EF7B0
		public void OnFireClicked()
		{
			Keyboard.PushMouseEvent("Command:CmdFire", null);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x000F15BD File Offset: 0x000EF7BD
		public void Init()
		{
			XRLCore.RegisterAfterRenderCallback(new Action<XRLCore, ScreenBuffer>(this.AfterRender));
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000F15D0 File Offset: 0x000EF7D0
		public void Update()
		{
			this.fireHotkeyText.Apply();
			this.reloadHotkeyText.Apply();
			if (!this.StatusLock.TryEnterWriteLock(0))
			{
				return;
			}
			try
			{
				List<global::XRL.World.GameObject> list = null;
				foreach (KeyValuePair<global::XRL.World.GameObject, MissileWeaponArea.MissileWeaponAreaWeaponStatus> keyValuePair in this.weaponStatus)
				{
					if (!keyValuePair.Value.updated)
					{
						if (list == null)
						{
							list = new List<global::XRL.World.GameObject>();
						}
						list.Add(keyValuePair.Key);
						this.needsLayoutRefresh = true;
					}
				}
				if (list != null)
				{
					list.ForEach(delegate(global::XRL.World.GameObject k)
					{
						this.weaponStatus[k].pool();
						this.weaponStatus.Remove(k);
					});
				}
				foreach (KeyValuePair<global::XRL.World.GameObject, MissileWeaponArea.MissileWeaponAreaWeaponStatus> keyValuePair2 in this.weaponStatus)
				{
					keyValuePair2.Value.update();
				}
			}
			finally
			{
				this.StatusLock.ExitWriteLock();
			}
			if (this.needsLayoutRefresh)
			{
				HorizontalLayoutGroup component = base.GetComponent<HorizontalLayoutGroup>();
				component.enabled = false;
				component.enabled = true;
				this.needsLayoutRefresh = false;
			}
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000F1708 File Offset: 0x000EF908
		private void AfterRender(XRLCore core, ScreenBuffer sb)
		{
			if (!this.StatusLock.TryEnterWriteLock(100))
			{
				return;
			}
			try
			{
				List<global::XRL.World.GameObject> missileWeapons = The.Player.GetMissileWeapons(null, null);
				foreach (KeyValuePair<global::XRL.World.GameObject, MissileWeaponArea.MissileWeaponAreaWeaponStatus> keyValuePair in this.weaponStatus)
				{
					keyValuePair.Value.updated = false;
				}
				if (missileWeapons != null && missileWeapons.Count > 0)
				{
					for (int i = 0; i < missileWeapons.Count; i++)
					{
						try
						{
							if (missileWeapons[i] != null)
							{
								MissileWeapon part = missileWeapons[i].GetPart<MissileWeapon>();
								if (part != null)
								{
									if (!this.weaponStatus.ContainsKey(missileWeapons[i]))
									{
										this.weaponStatus.Add(missileWeapons[i], MissileWeaponArea.MissileWeaponAreaWeaponStatus.next());
										this.needsLayoutRefresh = true;
									}
									this.weaponStatus[missileWeapons[i]].area = this;
									part.Status(this.weaponStatus[missileWeapons[i]]);
									this.weaponStatus[missileWeapons[i]].updated = true;
								}
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("MissileWeaponArea::AfterRender", ex, "game_exception");
						}
					}
					this.fireHotkeyText.text = "{{W|[" + ControlManager.getCommandInputDescription("CmdFire", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} fire";
					this.reloadHotkeyText.text = "{{W|[" + ControlManager.getCommandInputDescription("CmdReload", true, false, ControlManager.InputDeviceType.Unknown, false) + "]}} reload";
				}
				else
				{
					this.fireHotkeyText.text = "{{K|You have no missile weapons equipped.}}";
					this.reloadHotkeyText.text = "";
				}
			}
			finally
			{
				this.StatusLock.ExitWriteLock();
			}
		}

		// Token: 0x040018DF RID: 6367
		public UITextSkin fireHotkeyText;

		// Token: 0x040018E0 RID: 6368
		public UITextSkin reloadHotkeyText;

		// Token: 0x040018E1 RID: 6369
		public global::UnityEngine.GameObject missileWeaponInfoPrefab;

		// Token: 0x040018E2 RID: 6370
		public global::UnityEngine.GameObject missileWeaponList;

		// Token: 0x040018E3 RID: 6371
		private static Stack<MissileWeaponArea.MissileWeaponAreaWeaponStatus> statusPool = new Stack<MissileWeaponArea.MissileWeaponAreaWeaponStatus>();

		// Token: 0x040018E4 RID: 6372
		private static Stack<global::UnityEngine.GameObject> missileWeaponInfoPrefabPool = new Stack<global::UnityEngine.GameObject>();

		// Token: 0x040018E5 RID: 6373
		private Dictionary<global::XRL.World.GameObject, MissileWeaponArea.MissileWeaponAreaWeaponStatus> weaponStatus = new Dictionary<global::XRL.World.GameObject, MissileWeaponArea.MissileWeaponAreaWeaponStatus>();

		// Token: 0x040018E6 RID: 6374
		private ReaderWriterLockSlim StatusLock = new ReaderWriterLockSlim();

		// Token: 0x040018E7 RID: 6375
		public bool needsLayoutRefresh;

		// Token: 0x020018ED RID: 6381
		public class MissileWeaponAreaWeaponStatus
		{
			// Token: 0x17000E48 RID: 3656
			// (get) Token: 0x0600E1D3 RID: 57811 RVA: 0x00520392 File Offset: 0x0051E592
			public int displayAmmoTotalBars
			{
				get
				{
					if (this.ammoTotal > 30)
					{
						return (int)Math.Ceiling((double)((float)this.ammoTotal / 5f));
					}
					return this.ammoTotal;
				}
			}

			// Token: 0x17000E49 RID: 3657
			// (get) Token: 0x0600E1D4 RID: 57812 RVA: 0x005203B9 File Offset: 0x0051E5B9
			public int displayAmmoRemainingBars
			{
				get
				{
					if (this.ammoTotal > 30)
					{
						return (int)Math.Ceiling((double)((float)this.ammoRemaining / 5f));
					}
					return this.ammoRemaining;
				}
			}

			// Token: 0x0600E1D5 RID: 57813 RVA: 0x005203E0 File Offset: 0x0051E5E0
			public void pool()
			{
				this.text = null;
				this.display = null;
				this.renderable = null;
				this.ammoTotal = 0;
				this.ammoRemaining = 0;
				if (this.infoObject != null)
				{
					global::UnityEngine.Object.Destroy(this.infoObject.gameObject);
				}
				this.infoObject = null;
				this.area = null;
			}

			// Token: 0x0600E1D6 RID: 57814 RVA: 0x0052043C File Offset: 0x0051E63C
			public static MissileWeaponArea.MissileWeaponAreaWeaponStatus next()
			{
				if (MissileWeaponArea.statusPool.Count > 0)
				{
					return MissileWeaponArea.statusPool.Pop();
				}
				return new MissileWeaponArea.MissileWeaponAreaWeaponStatus();
			}

			// Token: 0x0600E1D7 RID: 57815 RVA: 0x0052045C File Offset: 0x0051E65C
			public void update()
			{
				if (this.infoObject == null)
				{
					this.infoObject = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(this.area.missileWeaponInfoPrefab).GetComponent<MissileWeaponAreaInfo>();
					this.infoObject.gameObject.SetActive(true);
					this.infoObject.transform.parent = this.area.missileWeaponList.transform;
				}
				this.infoObject.UpdateFrom(this);
			}

			// Token: 0x0400641F RID: 25631
			public bool updated;

			// Token: 0x04006420 RID: 25632
			public string text;

			// Token: 0x04006421 RID: 25633
			public string display;

			// Token: 0x04006422 RID: 25634
			public IRenderable renderable;

			// Token: 0x04006423 RID: 25635
			public int ammoTotal;

			// Token: 0x04006424 RID: 25636
			public int ammoRemaining;

			// Token: 0x04006425 RID: 25637
			public MissileWeaponArea area;

			// Token: 0x04006426 RID: 25638
			public MissileWeaponAreaInfo infoObject;
		}
	}
}
