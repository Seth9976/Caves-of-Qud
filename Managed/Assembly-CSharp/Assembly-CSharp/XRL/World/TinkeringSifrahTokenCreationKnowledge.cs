using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000910 RID: 2320
	[Serializable]
	public class TinkeringSifrahTokenCreationKnowledge : SifrahPrioritizableToken
	{
		// Token: 0x0600587F RID: 22655 RVA: 0x001AC840 File Offset: 0x001AAA40
		public TinkeringSifrahTokenCreationKnowledge()
		{
			this.Description = "apply knowledge of this artifact's manufacture";
			this.FailureDescription = "You do not know how to manufacture this kind of artifact.";
			this.Tile = "Items/sw_anvil.bmp";
			this.RenderString = "Ò";
			this.ColorString = "&K";
			this.DetailColor = 'w';
		}

		// Token: 0x06005880 RID: 22656 RVA: 0x001AC894 File Offset: 0x001AAA94
		public TinkeringSifrahTokenCreationKnowledge(string Blueprint)
			: this()
		{
			this.Blueprint = Blueprint;
			if (!string.IsNullOrEmpty(Blueprint))
			{
				GameObject gameObject = GameObject.CreateSample(Blueprint);
				string pluralName = gameObject.GetPluralName(false, false, false, false);
				this.Description = "apply knowledge of the manufacture of " + pluralName;
				this.FailureDescription = "You do not know how to manufacture " + pluralName + ".";
				gameObject.Obliterate(null, false, null);
			}
		}

		// Token: 0x06005881 RID: 22657 RVA: 0x001AC8F7 File Offset: 0x001AAAF7
		public TinkeringSifrahTokenCreationKnowledge(GameObject Object)
			: this(Object.GetTinkeringBlueprint())
		{
		}

		// Token: 0x06005882 RID: 22658 RVA: 0x001AC905 File Offset: 0x001AAB05
		public override int GetPriority()
		{
			if (this.IsAvailable())
			{
				return int.MaxValue;
			}
			if (!this.IsPotentiallyAvailable())
			{
				return int.MinValue;
			}
			return 1;
		}

		// Token: 0x06005883 RID: 22659 RVA: 0x001AC924 File Offset: 0x001AAB24
		public override int GetTiebreakerPriority()
		{
			if (!this.IsPotentiallyAvailable())
			{
				return int.MinValue;
			}
			return int.MaxValue;
		}

		// Token: 0x06005884 RID: 22660 RVA: 0x001AC93C File Offset: 0x001AAB3C
		public bool IsAvailable()
		{
			if (!string.IsNullOrEmpty(this.Blueprint))
			{
				using (List<TinkerData>.Enumerator enumerator = TinkerData.KnownRecipes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Blueprint == this.Blueprint)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005885 RID: 22661 RVA: 0x001AC9AC File Offset: 0x001AABAC
		public bool IsPotentiallyAvailable()
		{
			return TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor(this.Blueprint);
		}

		// Token: 0x06005886 RID: 22662 RVA: 0x001AC9BC File Offset: 0x001AABBC
		public static bool IsPotentiallyAvailableFor(string Blueprint)
		{
			if (!string.IsNullOrEmpty(Blueprint))
			{
				using (List<TinkerData>.Enumerator enumerator = TinkerData.TinkerRecipes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Blueprint == Blueprint)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06005887 RID: 22663 RVA: 0x001ACA24 File Offset: 0x001AAC24
		public static bool IsPotentiallyAvailableFor(GameObject Object)
		{
			return TinkeringSifrahTokenCreationKnowledge.IsPotentiallyAvailableFor((Object != null) ? Object.Blueprint : null);
		}

		// Token: 0x06005888 RID: 22664 RVA: 0x001ACA37 File Offset: 0x001AAC37
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005889 RID: 22665 RVA: 0x001ACA4C File Offset: 0x001AAC4C
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail(this.FailureDescription, true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x04002C1E RID: 11294
		public string Blueprint;

		// Token: 0x04002C1F RID: 11295
		public string FailureDescription;
	}
}
