using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000904 RID: 2308
	[Serializable]
	public class SocialSifrahTokenApplySocialCoprocessor : SifrahPrioritizableToken
	{
		// Token: 0x06005851 RID: 22609 RVA: 0x001ABF33 File Offset: 0x001AA133
		public SocialSifrahTokenApplySocialCoprocessor()
		{
			this.Description = "apply social coprocessor";
			this.Tile = "Items/sw_social_coprocessor.bmp";
			this.RenderString = "\u0001";
			this.ColorString = "&r";
			this.DetailColor = 'C';
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x001ABF6F File Offset: 0x001AA16F
		public override int GetPriority()
		{
			if (!SocialSifrahTokenApplySocialCoprocessor.IsPotentiallyAvailable())
			{
				return 0;
			}
			if (!SocialSifrahTokenApplySocialCoprocessor.IsAvailable())
			{
				return 536870911;
			}
			return 1610612733;
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x001ABF8C File Offset: 0x001AA18C
		public override int GetTiebreakerPriority()
		{
			if (!The.Player.IsTrueKin())
			{
				return 0;
			}
			return 1610612733;
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x001ABFA1 File Offset: 0x001AA1A1
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.UsabilityCheckedThisTurn && !SocialSifrahTokenApplySocialCoprocessor.IsAvailable())
			{
				this.DisabledThisTurn = true;
				return false;
			}
			return true;
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x001ABFBC File Offset: 0x001AA1BC
		public static bool IsPotentiallyAvailable()
		{
			return SocialSifrahTokenApplySocialCoprocessor.HasAnySocialCoprocessor(The.Player);
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x001ABFC8 File Offset: 0x001AA1C8
		public static bool IsAvailable()
		{
			return SocialSifrahTokenApplySocialCoprocessor.HasUsableSocialCoprocessor(The.Player);
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x001ABFD4 File Offset: 0x001AA1D4
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			GameObject usableSocialCoprocessor = SocialSifrahTokenApplySocialCoprocessor.GetUsableSocialCoprocessor(The.Player);
			if (usableSocialCoprocessor != null)
			{
				usableSocialCoprocessor.ModIntProperty("SifrahActions", 1, false);
				usableSocialCoprocessor.SetLongProperty("LastSifrahActionTurn", The.CurrentTurn);
			}
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x001AC018 File Offset: 0x001AA218
		public static GameObject GetAnySocialCoprocessor(GameObject Actor)
		{
			List<GameObject> list = ((Actor != null) ? Actor.GetInstalledCybernetics() : null);
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			foreach (GameObject gameObject in list)
			{
				if (gameObject.HasPart<CyberneticsSocialCoprocessor>())
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x06005859 RID: 22617 RVA: 0x001AC08C File Offset: 0x001AA28C
		public static bool HasAnySocialCoprocessor(GameObject Actor)
		{
			return SocialSifrahTokenApplySocialCoprocessor.GetAnySocialCoprocessor(Actor) != null;
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x001AC098 File Offset: 0x001AA298
		public static GameObject GetUsableSocialCoprocessor(GameObject Actor)
		{
			List<GameObject> list = ((Actor != null) ? Actor.GetInstalledCybernetics() : null);
			if (list == null || list.Count <= 0)
			{
				return null;
			}
			foreach (GameObject gameObject in list)
			{
				CyberneticsSocialCoprocessor cyberneticsSocialCoprocessor;
				if (gameObject.TryGetPart<CyberneticsSocialCoprocessor>(out cyberneticsSocialCoprocessor) && cyberneticsSocialCoprocessor.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return gameObject;
				}
			}
			return null;
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x001AC138 File Offset: 0x001AA338
		public static bool HasUsableSocialCoprocessor(GameObject Actor)
		{
			return SocialSifrahTokenApplySocialCoprocessor.GetUsableSocialCoprocessor(Actor) != null;
		}
	}
}
