using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XRL;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace Qud.API
{
	// Token: 0x0200040A RID: 1034
	public static class MutationsAPI
	{
		// Token: 0x06002751 RID: 10065 RVA: 0x000CBE58 File Offset: 0x000CA058
		public static bool IsNewMutationValidFor(global::XRL.World.GameObject go, MutationEntry entry, Predicate<MutationEntry> filter = null)
		{
			if (go == null)
			{
				return false;
			}
			if (entry == null)
			{
				return false;
			}
			if ((go.HasTagOrProperty("Esper") || go.GetTagOrStringProperty("MutationLevel", null) == "Esper") && entry.Mutation != null && !string.IsNullOrEmpty(entry.Mutation.GetMutationType()) && !entry.Mutation.GetMutationType().Contains("Mental"))
			{
				return false;
			}
			if ((go.HasTagOrProperty("Chimera") || go.GetTagOrStringProperty("MutationLevel", null) == "Chimera") && entry.Mutation != null && !string.IsNullOrEmpty(entry.Mutation.GetMutationType()) && !entry.Mutation.GetMutationType().Contains("Physical"))
			{
				return false;
			}
			if (go.HasPart(entry.Class))
			{
				return false;
			}
			foreach (string text in entry.GetExclusions())
			{
				if (!string.IsNullOrEmpty(text) && go.HasPart(text))
				{
					return false;
				}
			}
			return filter == null || filter(entry);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000CBF6C File Offset: 0x000CA16C
		public static MutationEntry FindRandomMutationFor(global::XRL.World.GameObject go, Predicate<MutationEntry> filter = null, global::System.Random rng = null, bool allowMultipleDefects = false)
		{
			List<MutationEntry> list = new List<MutationEntry>(go.GetPart<Mutations>().GetMutatePool(filter, allowMultipleDefects));
			if (list.Count <= 0)
			{
				return null;
			}
			return list.GetRandomElement<MutationEntry>(rng);
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000CBFA0 File Offset: 0x000CA1A0
		public static bool ApplyMutationTo(global::XRL.World.GameObject go, MutationEntry entry)
		{
			Mutations part = go.GetPart<Mutations>();
			if (part == null)
			{
				return false;
			}
			part.AddMutation(entry.CreateInstance(), 1, true);
			return true;
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000CBFCC File Offset: 0x000CA1CC
		public static MutationEntry RandomlyMutate(global::XRL.World.GameObject go, Predicate<MutationEntry> filter = null, global::System.Random rng = null, bool allowMultipleDefects = false, StringBuilder result = null)
		{
			MutationEntry mutationEntry = MutationsAPI.FindRandomMutationFor(go, filter, rng, allowMultipleDefects);
			if (mutationEntry == null)
			{
				return null;
			}
			MutationsAPI.ApplyMutationTo(go, mutationEntry);
			if (result != null && go.IsPlayer())
			{
				result.Append(" ");
				result.Append(" You gain " + mutationEntry.GetDisplayName(false) + "!");
			}
			if (go.IsChimera() && Stat.Random(1, 3) <= 1)
			{
				Mutations part = go.GetPart<Mutations>();
				if (part != null)
				{
					part.AddChimericBodyPart(false, "Chimera", null);
				}
			}
			return mutationEntry;
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000CC054 File Offset: 0x000CA254
		public static bool BuyRandomMutation(global::XRL.World.GameObject Object, int Cost = 4, bool Confirm = true, string MutationTerm = null)
		{
			try
			{
				if (MutationTerm == null)
				{
					MutationTerm = GetMutationTermEvent.GetFor(Object, null);
				}
				if (!StatusScreen.TreatAsMutant(Object))
				{
					return Object.ShowFailure("Your type of creature may not buy " + Grammar.Pluralize(MutationTerm) + ".");
				}
				if (Object.Stat("MP", 0) < Cost)
				{
					return Object.ShowFailure("You don't have " + Cost.ToString() + " mutation points!");
				}
				if (!Confirm || !Object.IsPlayer() || Popup.ShowYesNo(string.Concat(new string[]
				{
					"Are you sure you want to spend ",
					Cost.ToString(),
					" mutation points to buy a new ",
					MutationTerm,
					"?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
				{
					bool flag;
					if (Object.HasPart<IrritableGenome>())
					{
						flag = MutationsAPI.RandomlyMutate(Object, null, null, false, null) != null;
					}
					else
					{
						flag = StatusScreen.BuyRandomMutation(Object);
					}
					if (flag)
					{
						Object.UseMP(Cost, "BuyNew");
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("BuyRandomMutation", ex, "game_exception");
			}
			return false;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000CC170 File Offset: 0x000CA370
		public static bool AddNewMentalMutation(List<BaseMutation> pMutations, int MinCost = -1, int MaxCost = 999)
		{
			List<MutationEntry> mutationsOfCategory = MutationFactory.GetMutationsOfCategory("Mental", false);
			mutationsOfCategory.ShuffleInPlace<MutationEntry>(null);
			foreach (MutationEntry mutationEntry in mutationsOfCategory)
			{
				if (MutationsAPI.MutationOk(pMutations, mutationEntry.CreateInstance(), MinCost, MaxCost))
				{
					pMutations.Add(mutationEntry.CreateInstance());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000CC1EC File Offset: 0x000CA3EC
		public static bool AddNewPhysicalMutation(List<BaseMutation> pMutations, int MinCost = -1, int MaxCost = 999)
		{
			List<MutationEntry> mutationsOfCategory = MutationFactory.GetMutationsOfCategory("Physical", false);
			mutationsOfCategory.ShuffleInPlace<MutationEntry>(null);
			foreach (MutationEntry mutationEntry in mutationsOfCategory)
			{
				if (MutationsAPI.MutationOk(pMutations, mutationEntry.CreateInstance(), MinCost, MaxCost))
				{
					pMutations.Add(mutationEntry.CreateInstance());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000CC268 File Offset: 0x000CA468
		public static bool AddNewDefectMutation(List<BaseMutation> pMutations)
		{
			List<MutationEntry> mutationsOfCategory = MutationFactory.GetMutationsOfCategory("MentalDefects,PhysicalDefects", false);
			mutationsOfCategory.ShuffleInPlace<MutationEntry>(null);
			foreach (MutationEntry mutationEntry in mutationsOfCategory)
			{
				if (MutationsAPI.MutationOk(pMutations, mutationEntry.CreateInstance(), -1, 999))
				{
					pMutations.Add(mutationEntry.CreateInstance());
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000CC2E8 File Offset: 0x000CA4E8
		public static bool MutationOk(List<BaseMutation> Current, BaseMutation New, int MinCost = -1, int MaxCost = 999)
		{
			if (New.GetMutationEntry() == null)
			{
				return true;
			}
			try
			{
				using (List<BaseMutation>.Enumerator enumerator = Current.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.GetMutationEntry().Name == New.GetMutationEntry().Name)
						{
							return false;
						}
					}
				}
				if (New.GetMutationEntry().Cost < MinCost || New.GetMutationEntry().Cost > MaxCost)
				{
					return false;
				}
				foreach (string text in New.GetMutationEntry().GetExclusions())
				{
					if (!string.IsNullOrEmpty(text) && MutationsAPI.MutationInList(Current, text))
					{
						return false;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error with mutation " + New.Name + " cost: " + ex.ToString());
			}
			return true;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000CC3E8 File Offset: 0x000CA5E8
		public static bool MutationInList(List<BaseMutation> List, string Name)
		{
			using (List<BaseMutation>.Enumerator enumerator = List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetMutationEntry().Name == Name)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
