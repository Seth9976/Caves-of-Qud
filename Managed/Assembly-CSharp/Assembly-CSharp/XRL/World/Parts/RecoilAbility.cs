using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000EF6 RID: 3830
	[Serializable]
	public class RecoilAbility : IPart
	{
		// Token: 0x060098F9 RID: 39161 RVA: 0x0037861C File Offset: 0x0037681C
		public override void Initialize()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Recoil", RecoilAbility.COMMAND_NAME, "Items", null, "\u001b", null, false, false, false, false, false, true, false, false, true, true, false, -1, null, null, null, null, null, null);
			base.Initialize();
		}

		// Token: 0x060098FA RID: 39162 RVA: 0x00378663 File Offset: 0x00376863
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.Remove();
		}

		// Token: 0x060098FB RID: 39163 RVA: 0x00378679 File Offset: 0x00376879
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == GetMovementCapabilitiesEvent.ID;
		}

		// Token: 0x060098FC RID: 39164 RVA: 0x00378697 File Offset: 0x00376897
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Recoil", RecoilAbility.COMMAND_NAME, 30000, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x060098FD RID: 39165 RVA: 0x003786C4 File Offset: 0x003768C4
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == RecoilAbility.COMMAND_NAME)
			{
				List<GameObject> @for = GetRecoilersEvent.GetFor(E.Actor);
				if (@for == null || @for.Count <= 0)
				{
					return E.Actor.Fail("You do not have any recoilers.");
				}
				RecoilAbility.Recoilers.Clear();
				RecoilAbility.Recoilers.AddRange(@for);
				if (RecoilAbility.Recoilers.Count > 1)
				{
					RecoilAbility.Recoilers.Sort(new Comparison<GameObject>(this.SortRecoilers));
				}
				int num = 0;
				for (;;)
				{
					GameObject gameObject = Popup.PickGameObject("Use which recoiler?", RecoilAbility.Recoilers, true, false, true, new Func<GameObject, string>(this.LabelRecoiler), num, false);
					if (gameObject == null)
					{
						break;
					}
					num = RecoilAbility.Recoilers.IndexOf(gameObject);
					bool flag = gameObject.Twiddle(false);
					if (flag)
					{
						return false;
					}
					for (int i = RecoilAbility.Recoilers.Count - 1; i >= 0; i--)
					{
						if (RecoilAbility.Recoilers[i] == null || RecoilAbility.Recoilers[i].IsNowhere())
						{
							RecoilAbility.Recoilers.RemoveAt(i);
							if (num >= i)
							{
								num--;
							}
						}
					}
					if (flag)
					{
						goto IL_010E;
					}
				}
				return false;
			}
			IL_010E:
			return base.HandleEvent(E);
		}

		// Token: 0x060098FE RID: 39166 RVA: 0x003787E6 File Offset: 0x003769E6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060098FF RID: 39167 RVA: 0x003787EC File Offset: 0x003769EC
		private string LabelRecoiler(GameObject Object)
		{
			ITeleporter firstPartDescendedFrom = Object.GetFirstPartDescendedFrom<ITeleporter>();
			string text = ((firstPartDescendedFrom != null) ? firstPartDescendedFrom.GetStatusSummary() : null);
			if (text == "EMP" || text == "warming up")
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06009900 RID: 39168 RVA: 0x0037882C File Offset: 0x00376A2C
		private int SortRecoilers(GameObject A, GameObject B)
		{
			ITeleporter firstPartDescendedFrom = A.GetFirstPartDescendedFrom<ITeleporter>();
			ITeleporter firstPartDescendedFrom2 = B.GetFirstPartDescendedFrom<ITeleporter>();
			int num = (firstPartDescendedFrom != null).CompareTo(firstPartDescendedFrom2 != null);
			if (num != 0)
			{
				return num;
			}
			long num2 = The.CurrentTurn - firstPartDescendedFrom.LastTurnUsed;
			long num3 = The.CurrentTurn - firstPartDescendedFrom2.LastTurnUsed;
			int num4 = num2.CompareTo(num3);
			if (num4 != 0)
			{
				return num4;
			}
			return A.GetCachedDisplayNameForSort().CompareTo(B.GetCachedDisplayNameForSort());
		}

		// Token: 0x04003FBA RID: 16314
		public static readonly string COMMAND_NAME = "CommandRecoil";

		// Token: 0x04003FBB RID: 16315
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04003FBC RID: 16316
		[NonSerialized]
		private static List<GameObject> Recoilers = new List<GameObject>(32);
	}
}
