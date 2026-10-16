using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.Language;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200128F RID: 4751
	[Serializable]
	public class StiltWell : IPart
	{
		// Token: 0x0600BBEB RID: 48107 RVA: 0x00449CB9 File Offset: 0x00447EB9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CheckAttackableEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600BBEC RID: 48108 RVA: 0x00449CDF File Offset: 0x00447EDF
		public override bool HandleEvent(CheckAttackableEvent E)
		{
			return false;
		}

		// Token: 0x0600BBED RID: 48109 RVA: 0x00449CE4 File Offset: 0x00447EE4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Sacrifice", "sacrifice", "Sacrifice", null, 's', false, 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBEE RID: 48110 RVA: 0x00449D1C File Offset: 0x00447F1C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Sacrifice" && StiltWell.GiveArtifacts(E.Actor, this.ParentObject))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BBEF RID: 48111 RVA: 0x00449D50 File Offset: 0x00447F50
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BBF0 RID: 48112 RVA: 0x00449D53 File Offset: 0x00447F53
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BBF1 RID: 48113 RVA: 0x00449D74 File Offset: 0x00447F74
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				return false;
			}
			if (E.ID == "CommandSmartUse")
			{
				StiltWell.GiveArtifacts(E.GetGameObjectParameter("User"), this.ParentObject);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BBF2 RID: 48114 RVA: 0x00449DC8 File Offset: 0x00447FC8
		public static int GetArtifactReputationValue(GameObject obj)
		{
			Examiner part = obj.GetPart<Examiner>();
			int num = 0;
			if (part != null && part.Complexity > 0 && Scanning.GetScanTypeFor(obj) != Scanning.Scan.Bio)
			{
				Commerce part2 = obj.GetPart<Commerce>();
				if (part2 == null)
				{
					num = 10;
				}
				else
				{
					num = Math.Max(1, (int)(part2.Value / 10.0));
				}
			}
			return num;
		}

		// Token: 0x0600BBF3 RID: 48115 RVA: 0x00449E1A File Offset: 0x0044801A
		public static bool IsValuedArtifact(GameObject Object)
		{
			return StiltWell.GetArtifactReputationValue(Object) > 0;
		}

		// Token: 0x0600BBF4 RID: 48116 RVA: 0x00449E28 File Offset: 0x00448028
		public static bool GiveArtifacts(GameObject Object, GameObject Well)
		{
			Inventory inventory = Object.Inventory;
			List<GameObject> list = new List<GameObject>();
			List<string> list2 = new List<string>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			List<IRenderable> list5 = new List<IRenderable>();
			bool flag = false;
			foreach (GameObject gameObject in inventory.Objects)
			{
				int artifactReputationValue = StiltWell.GetArtifactReputationValue(gameObject);
				if (artifactReputationValue > 0)
				{
					if (gameObject.IsMarkedImportantByPlayer())
					{
						flag = true;
					}
					else
					{
						list.Add(gameObject);
						list3.Add(artifactReputationValue);
						list4.Add(gameObject.Count);
						list5.Add(gameObject.RenderForUI(null, false));
						list2.Add(gameObject.GetDisplayName(1120, null, null, false, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true) + " [{{C|+" + artifactReputationValue.ToString() + "}} reputation]");
					}
				}
			}
			if (list2.Count <= 0)
			{
				return The.Player.ShowFailure(flag ? "You have no artifacts to offer that are not important." : "You have no artifacts to offer.");
			}
			List<ValueTuple<int, int>> list6;
			if (!Object.IsPlayer())
			{
				(list6 = new List<ValueTuple<int, int>>()).Add(new ValueTuple<int, int>(0, 1));
			}
			else
			{
				list6 = Popup.PickSeveral("", "Choose artifacts to throw down the well.", "", "Sounds/UI/ui_notification", list2, HotkeySpread.get(new string[] { "Menus", "UINav" }), list4, list5, Well, null, null, -1, 1, 60, 0, -1, false, true, false, true, false);
			}
			List<ValueTuple<int, int>> list7 = list6;
			if (list7.IsNullOrEmpty<ValueTuple<int, int>>())
			{
				return false;
			}
			int num = 0;
			List<GameObject> list8 = new List<GameObject>(list7.Count);
			foreach (ValueTuple<int, int> valueTuple in list7)
			{
				GameObject gameObject2 = list[valueTuple.Item1];
				int item = valueTuple.Item2;
				if (gameObject2.ConfirmUseImportant(Object, "throw", "down", item))
				{
					gameObject2.SplitStack(item, The.Player, false);
					if (gameObject2.TryRemoveFromContext(null))
					{
						num += list3[valueTuple.Item1] * item;
						list8.Add(gameObject2);
						if (list3[valueTuple.Item1] >= 200 && Object.IsPlayer())
						{
							Achievement.DONATE_ITEM_200_REP.Unlock();
						}
					}
				}
			}
			if (!list8.IsNullOrEmpty<GameObject>())
			{
				Well.Physics.PlayWorldSound("Sounds/Interact/sfx_interact_well_throw", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				string text = Grammar.MakeAndList(list8.Select<GameObject, string>((GameObject x) => x.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false)).ToList<string>(), true);
				IComponent<GameObject>.XDidYToZ(Object, "throw", text + " down", Well, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
				if (Object.IsPlayer())
				{
					The.Game.PlayerReputation.Modify("Mechanimists", num, "StiltWell", null, null, false, false);
				}
			}
			return true;
		}
	}
}
