using System;
using XRL.World;
using XRL.World.Parts;

namespace XRL.CharacterBuilds
{
	/// <summary>
	///     Abstract base class for Qud EmbarkBuilder modules.
	/// </summary>
	// Token: 0x02000592 RID: 1426
	public abstract class AbstractQudEmbarkBuilderModule : AbstractEmbarkBuilderModule
	{
		// Token: 0x06003D39 RID: 15673 RVA: 0x0012C130 File Offset: 0x0012A330
		protected static void PostprocessPlayerItem(GameObject Object)
		{
			Object.Seen();
			Object.MakeUnderstood(false);
			EnergyCellSocket part = Object.GetPart<EnergyCellSocket>();
			if (part != null)
			{
				GameObject cell = part.Cell;
				if (cell != null)
				{
					cell.MakeUnderstood(false);
				}
			}
			MagazineAmmoLoader part2 = Object.GetPart<MagazineAmmoLoader>();
			if (part2 == null)
			{
				return;
			}
			GameObject ammo = part2.Ammo;
			if (ammo == null)
			{
				return;
			}
			ammo.MakeUnderstood(false);
		}

		// Token: 0x06003D3A RID: 15674 RVA: 0x0012C186 File Offset: 0x0012A386
		protected static void AddItem(GameObject Object, GameObject Player)
		{
			AbstractQudEmbarkBuilderModule.PostprocessPlayerItem(Object);
			Player.ReceiveObject(Object, false, null);
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x0012C198 File Offset: 0x0012A398
		protected static void AddItem(string Blueprint, GameObject Player)
		{
			try
			{
				Player.ReceiveObject(Blueprint, false, 0, 0, null, null, new Action<GameObject>(AbstractQudEmbarkBuilderModule.PostprocessPlayerItem), null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("exception creating " + Blueprint, ex);
			}
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x0012C1E8 File Offset: 0x0012A3E8
		protected static void AddItem(string Blueprint, int Number, GameObject Player)
		{
			try
			{
				Player.ReceiveObject(Blueprint, Number, false, 0, 0, null, null, new Action<GameObject>(AbstractQudEmbarkBuilderModule.PostprocessPlayerItem), null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("exception creating " + Blueprint, ex);
			}
		}
	}
}
