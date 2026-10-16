using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using XRL.Collections;
using XRL.Core;
using XRL.Language;
using XRL.UI;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x0200119A RID: 4506
	[HasWishCommand]
	[HasModSensitiveStaticCache]
	[Serializable]
	public class Mutations : IPart
	{
		// Token: 0x0600B31A RID: 45850 RVA: 0x0041563B File Offset: 0x0041383B
		[ModSensitiveCacheInit]
		public static void ResetCaches()
		{
			Mutations.MutationEquipment.Clear();
		}

		// Token: 0x0600B31B RID: 45851 RVA: 0x00415648 File Offset: 0x00413848
		public static List<string> GetVariants(string Mutation)
		{
			List<string> list;
			if (!Mutations.MutationVariants.TryGetValue(Mutation, out list))
			{
				BaseMutation genericMutation = Mutations.GetGenericMutation(Mutation, null, null);
				list = (Mutations.MutationVariants[Mutation] = genericMutation.CreateVariants() ?? new List<string>());
			}
			return list;
		}

		// Token: 0x0600B31C RID: 45852 RVA: 0x0041568C File Offset: 0x0041388C
		public static List<GameObjectBlueprint> GetAllMutationEquipment()
		{
			if (Mutations.MutationEquipment.IsNullOrEmpty<GameObjectBlueprint>())
			{
				foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
				{
					if (gameObjectBlueprint.Tags.ContainsKey("MutationEquipment"))
					{
						Mutations.MutationEquipment.Add(gameObjectBlueprint);
					}
				}
			}
			return Mutations.MutationEquipment;
		}

		// Token: 0x0600B31D RID: 45853 RVA: 0x0041570C File Offset: 0x0041390C
		public unsafe static BaseMutation GetGenericMutation(string Mutation, string Variant = null, GameObject Actor = null)
		{
			BaseMutation baseMutation = ((Actor != null) ? Actor.GetPart(Mutation) : null) as BaseMutation;
			if (baseMutation != null)
			{
				return baseMutation;
			}
			int length = Mutation.Length;
			int num = (Variant.IsNullOrEmpty() ? 0 : (Variant.Length + 1));
			int num2 = length + num;
			Span<char> span;
			checked
			{
				span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				Mutation.CopyTo(0, span, 0, length);
			}
			if (num != 0)
			{
				*span[length] = '.';
				Variant.CopyTo(0, span, length + 1, num - 1);
			}
			if (Mutations.GenericMutations.TryGetValue(span, out baseMutation))
			{
				return baseMutation;
			}
			Type type = ModManager.ResolveType("XRL.World.Parts.Mutation." + Mutation, false, false, true);
			if (type == null)
			{
				global::UnityEngine.Debug.LogWarning("Cannot resolve mutation type for " + Mutation);
				return null;
			}
			baseMutation = (Mutations.GenericMutations[span] = BaseMutation.Create(type, Variant));
			return baseMutation;
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600B31E RID: 45854 RVA: 0x004157E2 File Offset: 0x004139E2
		public List<BaseMutation> ActiveMutationList
		{
			get
			{
				return this.MutationList.Where<BaseMutation>((BaseMutation m) => m.Level > 0).ToList<BaseMutation>();
			}
		}

		// Token: 0x0600B31F RID: 45855 RVA: 0x00415814 File Offset: 0x00413A14
		public override void FinalizeCopyEarly(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopyEarly(Source, CopyEffects, CopyID, MapInv);
			Mutations part = Source.GetPart<Mutations>();
			if (part != null)
			{
				foreach (BaseMutation baseMutation in part.MutationList)
				{
					BaseMutation baseMutation2 = this.ParentObject.GetPart(baseMutation.Name) as BaseMutation;
					if (baseMutation2 != null)
					{
						this.MutationList.Add(baseMutation2);
					}
				}
				foreach (Mutations.MutationModifierTracker mutationModifierTracker in part.MutationMods)
				{
					if (this.ParentObject.HasPart(mutationModifierTracker.mutationName))
					{
						this.MutationMods.Add(new Mutations.MutationModifierTracker(mutationModifierTracker, CopyID || CopyEffects));
					}
				}
			}
		}

		// Token: 0x0600B320 RID: 45856 RVA: 0x00415904 File Offset: 0x00413B04
		public override void FinalizeCopyLate(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopyLate(Source, CopyEffects, CopyID, MapInv);
			if (!CopyID)
			{
				if (CopyEffects)
				{
					using (List<Mutations.MutationModifierTracker>.Enumerator enumerator = new List<Mutations.MutationModifierTracker>(this.MutationMods).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Mutations.MutationModifierTracker mutationModifierTracker = enumerator.Current;
							if (mutationModifierTracker.sourceType != Mutations.MutationModifierTracker.SourceType.StatMod && mutationModifierTracker.sourceType != Mutations.MutationModifierTracker.SourceType.Equipment)
							{
								this.RemoveMutationMod(mutationModifierTracker.id);
							}
						}
						return;
					}
				}
				foreach (Mutations.MutationModifierTracker mutationModifierTracker2 in new List<Mutations.MutationModifierTracker>(this.MutationMods))
				{
					this.RemoveMutationMod(mutationModifierTracker2.id);
				}
			}
		}

		// Token: 0x0600B321 RID: 45857 RVA: 0x004159D4 File Offset: 0x00413BD4
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.MutationList.Count);
			foreach (BaseMutation baseMutation in this.MutationList)
			{
				try
				{
					Writer.Write(baseMutation.Name);
				}
				catch (Exception ex)
				{
					XRLCore.LogError("Exception serializing mutation " + baseMutation.Name + " : " + ex.ToString());
				}
			}
			Writer.Write<Mutations.MutationModifierTracker>(this.MutationMods);
			base.Write(Basis, Writer);
		}

		// Token: 0x0600B322 RID: 45858 RVA: 0x00415A84 File Offset: 0x00413C84
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			this.MutationList.Clear();
			if (num > 0)
			{
				this.FinalizeMutations = new List<string>();
			}
			for (int i = 0; i < num; i++)
			{
				this.FinalizeMutations.Add(Reader.ReadString());
			}
			this.MutationMods = Reader.ReadList<Mutations.MutationModifierTracker>();
			base.Read(Basis, Reader);
		}

		// Token: 0x0600B323 RID: 45859 RVA: 0x00415AE4 File Offset: 0x00413CE4
		public override void FinalizeRead(SerializationReader Reader)
		{
			if (this.FinalizeMutations == null)
			{
				return;
			}
			if (this.MutationList == null)
			{
				this.MutationList = new List<BaseMutation>();
			}
			foreach (string text in this.FinalizeMutations)
			{
				BaseMutation baseMutation = (BaseMutation)this.ParentObject.GetPart(text);
				if (baseMutation != null)
				{
					this.MutationList.Add(baseMutation);
				}
			}
			this.FinalizeMutations = null;
			base.FinalizeRead(Reader);
		}

		// Token: 0x0600B324 RID: 45860 RVA: 0x00415B7C File Offset: 0x00413D7C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B325 RID: 45861 RVA: 0x00415B80 File Offset: 0x00413D80
		public BaseMutation GetMutation(string MutationName)
		{
			if (this.MutationList != null)
			{
				int i = 0;
				int count = this.MutationList.Count;
				while (i < count)
				{
					if (this.MutationList[i].Name == MutationName)
					{
						return this.MutationList[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600B326 RID: 45862 RVA: 0x00415BD4 File Offset: 0x00413DD4
		public BaseMutation GetMutationByName(string Name)
		{
			if (this.MutationList != null)
			{
				int i = 0;
				int count = this.MutationList.Count;
				while (i < count)
				{
					if (this.MutationList[i].Name == Name)
					{
						return this.MutationList[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600B327 RID: 45863 RVA: 0x00415C28 File Offset: 0x00413E28
		public bool HasMutation(string MutationName)
		{
			if (MutationName == "Chimera")
			{
				return this.ParentObject.IsChimera();
			}
			if (MutationName == "Esper")
			{
				return this.ParentObject.IsEsper();
			}
			if (this.MutationList != null)
			{
				int i = 0;
				int count = this.MutationList.Count;
				while (i < count)
				{
					if (this.MutationList[i].Name == MutationName)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600B328 RID: 45864 RVA: 0x00415CA4 File Offset: 0x00413EA4
		public bool HasMutation(BaseMutation CheckedMutation)
		{
			if (CheckedMutation == null)
			{
				return false;
			}
			if (this.MutationList != null)
			{
				int i = 0;
				int count = this.MutationList.Count;
				while (i < count)
				{
					if (this.MutationList[i].Name == CheckedMutation.Name)
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600B329 RID: 45865 RVA: 0x00415CF8 File Offset: 0x00413EF8
		public Guid AddMutationMod(string Mutation, string Variant = null, int Level = 1, Mutations.MutationModifierTracker.SourceType SourceType = Mutations.MutationModifierTracker.SourceType.Unknown, string SourceName = null)
		{
			BaseMutation genericMutation = Mutations.GetGenericMutation(Mutation, Variant, null);
			if (genericMutation != null)
			{
				return this.AddMutationMod(genericMutation, Variant, Level, SourceType, SourceName);
			}
			return Guid.Empty;
		}

		// Token: 0x0600B32A RID: 45866 RVA: 0x00415D24 File Offset: 0x00413F24
		public Guid AddMutationMod(MutationEntry Mutation, string Variant, int Level = 1, Mutations.MutationModifierTracker.SourceType SourceType = Mutations.MutationModifierTracker.SourceType.Unknown, string SourceName = null)
		{
			return this.AddMutationMod(Mutation.Class, Variant, Level, SourceType, SourceName);
		}

		// Token: 0x0600B32B RID: 45867 RVA: 0x00415D38 File Offset: 0x00413F38
		public Guid AddMutationMod(MutationEntry Mutation, int Level = 1, Mutations.MutationModifierTracker.SourceType SourceType = Mutations.MutationModifierTracker.SourceType.Unknown, string SourceName = null)
		{
			return this.AddMutationMod(Mutation.Class, Mutation.Variant, Level, SourceType, SourceName);
		}

		// Token: 0x0600B32C RID: 45868 RVA: 0x00415D50 File Offset: 0x00413F50
		public Guid AddMutationMod(Type Mutation, string Variant = null, int Level = 1, Mutations.MutationModifierTracker.SourceType SourceType = Mutations.MutationModifierTracker.SourceType.Unknown, string SourceName = null)
		{
			return this.AddMutationMod(Mutation.Name, Variant, Level, SourceType, SourceName);
		}

		// Token: 0x0600B32D RID: 45869 RVA: 0x00415D64 File Offset: 0x00413F64
		public Guid AddMutationMod(BaseMutation Mutation, string Variant = null, int Level = 1, Mutations.MutationModifierTracker.SourceType SourceType = Mutations.MutationModifierTracker.SourceType.Unknown, string SourceName = null)
		{
			Guid guid = Guid.Empty;
			Type type = Mutation.GetType();
			if (!this.ParentObject.HasPart(type))
			{
				if (!Mutation.CompatibleWith(this.ParentObject))
				{
					return Guid.Empty;
				}
				guid = this.AddTracker(Mutation.Name, Level, SourceType, SourceName);
				BaseMutation baseMutation = BaseMutation.Create(type, Variant);
				baseMutation.ParentObject = this.ParentObject;
				if (baseMutation.Mutate(this.ParentObject, 0))
				{
					this.ParentObject.AddPart<BaseMutation>(baseMutation, true, false);
					baseMutation.AfterMutate();
				}
			}
			else
			{
				guid = this.AddTracker(Mutation.Name, Level, SourceType, SourceName);
			}
			this.ParentObject.SyncMutationLevelAndGlimmer();
			return guid;
		}

		// Token: 0x0600B32E RID: 45870 RVA: 0x00415E0C File Offset: 0x0041400C
		private Guid AddTracker(string Name, int Level = 1, Mutations.MutationModifierTracker.SourceType SourceType = Mutations.MutationModifierTracker.SourceType.Unknown, string SourceName = null)
		{
			Mutations.MutationModifierTracker mutationModifierTracker = new Mutations.MutationModifierTracker
			{
				id = Guid.NewGuid(),
				mutationName = Name,
				sourceType = SourceType,
				sourceName = SourceName,
				bonus = Level
			};
			this.MutationMods.Add(mutationModifierTracker);
			return mutationModifierTracker.id;
		}

		// Token: 0x0600B32F RID: 45871 RVA: 0x00415E5C File Offset: 0x0041405C
		public int GetLevelAdjustmentsForMutation(string className)
		{
			return this.MutationMods.Where<Mutations.MutationModifierTracker>((Mutations.MutationModifierTracker m) => m.mutationName == className).Aggregate(0, (int a, Mutations.MutationModifierTracker b) => a + b.bonus);
		}

		// Token: 0x0600B330 RID: 45872 RVA: 0x00415EB4 File Offset: 0x004140B4
		public void RemoveMutationMod(Guid id)
		{
			Mutations.MutationModifierTracker tracker = this.MutationMods.Find((Mutations.MutationModifierTracker m) => m.id == id);
			if (tracker != null)
			{
				this.MutationMods.Remove(tracker);
				if (!this.MutationList.Exists((BaseMutation m) => m.Name == tracker.mutationName) && !this.MutationMods.Exists((Mutations.MutationModifierTracker m) => m.mutationName == tracker.mutationName))
				{
					BaseMutation baseMutation = this.ParentObject.GetPart(tracker.mutationName) as BaseMutation;
					if (baseMutation.Unmutate(this.ParentObject))
					{
						this.ParentObject.RemovePart(baseMutation);
						baseMutation.AfterUnmutate(this.ParentObject);
					}
				}
			}
			this.ParentObject.SyncMutationLevelAndGlimmer();
		}

		// Token: 0x0600B331 RID: 45873 RVA: 0x00415F87 File Offset: 0x00414187
		public int AddMutation(string NewMutationClass, int Level = 1)
		{
			return this.AddMutation(BaseMutation.Create(NewMutationClass, null), Level, true);
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x00415F98 File Offset: 0x00414198
		public int AddMutation(string Class, string Variant, int Level = 1)
		{
			return this.AddMutation(BaseMutation.Create(Class, Variant), Level, true);
		}

		// Token: 0x0600B333 RID: 45875 RVA: 0x00415FA9 File Offset: 0x004141A9
		public int AddMutation(MutationEntry NewMutation, int Level = 1)
		{
			return this.AddMutation(NewMutation.CreateInstance(), Level, true);
		}

		// Token: 0x0600B334 RID: 45876 RVA: 0x00415FBC File Offset: 0x004141BC
		public int AddMutation(BaseMutation NewMutation, int Level = 1, bool Sync = true)
		{
			if (NewMutation == null)
			{
				return -1;
			}
			if (this.MutationList == null)
			{
				this.MutationList = new List<BaseMutation>();
			}
			if (NewMutation.Name != null)
			{
				if (this.HasMutation(NewMutation.Name))
				{
					MutationEntry mutationEntry = NewMutation.GetMutationEntry();
					if (mutationEntry != null && mutationEntry.Ranked)
					{
						BaseMutation mutation = this.GetMutation(NewMutation.Name);
						(mutation as IRankedMutation).AdjustRank(1);
						return this.MutationList.IndexOf(mutation);
					}
				}
				else
				{
					if (NewMutation.Variant.IsNullOrEmpty())
					{
						List<string> variants = NewMutation.GetVariants();
						if (!variants.IsNullOrEmpty<string>())
						{
							NewMutation.SetVariant(variants.GetRandomElement<string>(null));
						}
					}
					if (this.ParentObject.HasPart(NewMutation.Name))
					{
						BaseMutation baseMutation = this.ParentObject.GetPart(NewMutation.Name) as BaseMutation;
						if (baseMutation != null)
						{
							baseMutation.Unmutate(this.ParentObject);
							this.ParentObject.RemovePart(baseMutation);
							baseMutation.AfterUnmutate(this.ParentObject);
						}
						else
						{
							MetricsManager.LogError(string.Concat(new string[]
							{
								"Could not retrieve ",
								NewMutation.Name,
								" part as BaseMutation from ",
								this.ParentObject.DebugName,
								", using stopgap unmutate procedure"
							}));
							this.ParentObject.RemovePart(NewMutation.Name);
							NewMutation.AfterUnmutate(this.ParentObject);
						}
					}
					NewMutation.ParentObject = this.ParentObject;
					if (NewMutation.Mutate(this.ParentObject, Level))
					{
						if (this.ParentObject.HasRegisteredEvent("BeforeMutationAdded"))
						{
							this.ParentObject.FireEvent(Event.New("BeforeMutationAdded", "Object", this.ParentObject, "Mutation", NewMutation.GetMutationClass()));
						}
						this.ParentObject.AddPart<BaseMutation>(NewMutation, true, false);
						this.MutationList.Add(NewMutation);
						try
						{
							NewMutation.AfterMutate();
							if (this.ParentObject.HasRegisteredEvent("MutationAdded"))
							{
								this.ParentObject.FireEvent(Event.New("MutationAdded", "Object", this.ParentObject, "Mutation", NewMutation.GetMutationClass()));
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogError(ex);
						}
						if (Sync)
						{
							this.ParentObject.SyncMutationLevelAndGlimmer();
						}
						if (this.ParentObject != null && this.ParentObject.IsPlayer() && this.MutationList.Count >= 10)
						{
							Achievement.HAVE_10_MUTATIONS.Unlock();
						}
						return this.MutationList.Count - 1;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600B335 RID: 45877 RVA: 0x00416238 File Offset: 0x00414438
		public BodyPart CheckAddChimericBodyPart(bool Silent = false)
		{
			if (this.ParentObject != null && this.ParentObject.CurrentCell != null && this.ParentObject.IsChimera() && GlobalConfig.GetIntSetting("ChimericBodyPartChance", 0).in100())
			{
				return this.AddChimericBodyPart(Silent, "Chimera", null);
			}
			return null;
		}

		// Token: 0x0600B336 RID: 45878 RVA: 0x00416288 File Offset: 0x00414488
		public BodyPart AddChimericBodyPart(bool Silent = false, string Manager = "Chimera", BodyPart AttachAt = null)
		{
			BodyPartType randomBodyPartType = Anatomies.GetRandomBodyPartType(true, new bool?(true), new bool?(false), true, null, null, true);
			if (randomBodyPartType == null)
			{
				MetricsManager.LogWarning("could not generate a random body part type");
				return null;
			}
			bool flag = GlobalConfig.GetIntSetting("ChimericBodyPartStandardChance", 0).in100();
			if (AttachAt == null)
			{
				AttachAt = Mutations.GetChimericBodyPartAttachmentPoint(this.ParentObject, randomBodyPartType, flag);
			}
			if (AttachAt == null)
			{
				MetricsManager.LogWarning("could not find " + (flag ? "standard" : "random") + " attachment point for " + randomBodyPartType.FinalType);
				return null;
			}
			BodyPartType bodyPartType = randomBodyPartType;
			Body parentBody = AttachAt.ParentBody;
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			bool? flag2 = new bool?(true);
			BodyPart bodyPart = new BodyPart(bodyPartType, parentBody, text, text2, text3, text4, text5, text6, null, null, null, null, null, null, null, null, flag2, null, null, null, null)
			{
				Manager = Manager
			};
			List<BodyPartType> list = Anatomies.FindUsualChildBodyPartTypes(randomBodyPartType);
			if (list != null)
			{
				foreach (BodyPartType bodyPartType2 in list)
				{
					BodyPart bodyPart2 = bodyPart;
					BodyPartType bodyPartType3 = bodyPartType2;
					Body parentBody2 = bodyPart.ParentBody;
					string text7 = null;
					string text8 = null;
					string text9 = null;
					string text10 = null;
					string text11 = null;
					int? num = null;
					int? num2 = null;
					int? num3 = null;
					flag2 = null;
					bool? flag3 = flag2;
					flag2 = null;
					bool? flag4 = flag2;
					flag2 = null;
					bool? flag5 = flag2;
					flag2 = null;
					bool? flag6 = flag2;
					flag2 = null;
					bool? flag7 = flag2;
					flag2 = null;
					bool? flag8 = flag2;
					flag2 = null;
					bool? flag9 = flag2;
					flag2 = null;
					bool? flag10 = flag2;
					flag2 = null;
					bool? flag11 = flag2;
					flag2 = null;
					bodyPart2.AddPart(new BodyPart(bodyPartType3, parentBody2, text7, text8, text9, text10, text11, Manager, num, num2, num3, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag2), -1, true);
				}
			}
			base.PlayWorldSound("sfx_characterMod_limb_acquire", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			if (!Silent)
			{
				string text12;
				if (bodyPart.Mass)
				{
					text12 = "Some " + bodyPart.Name;
				}
				else if (bodyPart.Plural)
				{
					text12 = "A set of " + bodyPart.Name;
				}
				else
				{
					text12 = Grammar.A(bodyPart.Name, true);
				}
				base.EmitMessage(string.Concat(new string[]
				{
					text12,
					" grows out of ",
					this.ParentObject.IsPlayer() ? "your" : Grammar.MakePossessive(this.ParentObject.the + this.ParentObject.ShortDisplayName),
					" ",
					AttachAt.GetOrdinalName(),
					"!"
				}), ' ', true, false, false, null, null);
			}
			if (bodyPart.Laterality == 0)
			{
				if (!string.IsNullOrEmpty(randomBodyPartType.UsuallyOn) && randomBodyPartType.UsuallyOn != AttachAt.Type)
				{
					BodyPartType bodyPartType4 = AttachAt.VariantTypeModel();
					bodyPart.ModifyNameAndDescriptionRecursively(bodyPartType4.Name.Replace(" ", "-"), bodyPartType4.Description.Replace(" ", "-"));
				}
				if (AttachAt.Laterality != 0)
				{
					bodyPart.ChangeLaterality(AttachAt.Laterality | bodyPart.Laterality, true);
				}
			}
			AttachAt.AddPart(bodyPart, bodyPart.Type, new string[] { "Thrown Weapon", "Floating Nearby" }, true);
			GlobalConfig.GetIntSetting("ChimericBodyPartMirrorChance", 0).in100();
			return bodyPart;
		}

		// Token: 0x0600B337 RID: 45879 RVA: 0x00416638 File Offset: 0x00414838
		public static BodyPart GetChimericBodyPartAttachmentPoint(GameObject Subject, BodyPartType NewType, bool Standard = true)
		{
			if (Subject == null)
			{
				return null;
			}
			Body body = Subject.Body;
			if (body == null)
			{
				return null;
			}
			if (Standard)
			{
				if (string.IsNullOrEmpty(NewType.UsuallyOn))
				{
					return body.GetBody();
				}
			}
			else if (GlobalConfig.GetIntSetting("ChimericBodyPartRandomFromBodyChance", 0).in100())
			{
				return body.GetBody();
			}
			List<BodyPart> list = new List<BodyPart>();
			List<BodyPart> list2 = new List<BodyPart>();
			foreach (BodyPart bodyPart in body.GetParts())
			{
				if (!bodyPart.Abstract && bodyPart.Contact && !bodyPart.Extrinsic && string.IsNullOrEmpty(bodyPart.DependsOn) && string.IsNullOrEmpty(bodyPart.RequiresType) && BodyPartCategory.IsLiveCategory(bodyPart.Category))
				{
					if (Standard)
					{
						if (NewType.UsuallyOn == bodyPart.Type)
						{
							list2.Add(bodyPart);
							if (string.IsNullOrEmpty(NewType.UsuallyOnVariant) || NewType.UsuallyOnVariant == bodyPart.VariantType)
							{
								list.Add(bodyPart);
							}
						}
					}
					else
					{
						list.Add(bodyPart);
					}
				}
			}
			BodyPart bodyPart2;
			if ((bodyPart2 = list.GetRandomElement<BodyPart>(null)) == null)
			{
				bodyPart2 = list2.GetRandomElement<BodyPart>(null) ?? body.GetBody();
			}
			return bodyPart2;
		}

		// Token: 0x0600B338 RID: 45880 RVA: 0x00416794 File Offset: 0x00414994
		public void RemoveMutation(BaseMutation Mutation, bool Sync = true)
		{
			if (this.MutationMods.Any<Mutations.MutationModifierTracker>((Mutations.MutationModifierTracker x) => x.mutationName == Mutation.Name))
			{
				this.MutationList.Remove(Mutation);
				Mutation.BaseLevel = 0;
				if (Sync)
				{
					this.ParentObject.SyncMutationLevelAndGlimmer();
				}
				return;
			}
			if (Mutation.Unmutate(this.ParentObject))
			{
				this.ParentObject.RemovePart(Mutation);
				this.MutationList.Remove(Mutation);
				Mutation.AfterUnmutate(this.ParentObject);
			}
		}

		// Token: 0x0600B339 RID: 45881 RVA: 0x0041683B File Offset: 0x00414A3B
		public void LevelMutation(BaseMutation Mutation, int Level)
		{
			Mutation.BaseLevel = Level;
			Mutation.ChangeLevel(Mutation.Level);
			this.ParentObject.SyncMutationLevelAndGlimmer();
		}

		// Token: 0x0600B33A RID: 45882 RVA: 0x0041685C File Offset: 0x00414A5C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.MutationList != null)
			{
				foreach (BaseMutation baseMutation in this.MutationList)
				{
					stringBuilder.Append(baseMutation.GetDisplayName(true) + " (" + baseMutation.Level.ToString() + ")\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600B33B RID: 45883 RVA: 0x004168E8 File Offset: 0x00414AE8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<StatChangeEvent>.ID || ID == PooledEvent<SyncMutationLevelsEvent>.ID;
		}

		// Token: 0x0600B33C RID: 45884 RVA: 0x00416908 File Offset: 0x00414B08
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (this.MutationList != null && MutationFactory.StatsUsedByMutations.Contains(E.Name))
			{
				bool flag = false;
				foreach (BaseMutation baseMutation in this.MutationList)
				{
					if (baseMutation.GetStat() == E.Name)
					{
						int level = baseMutation.Level;
						if (level != baseMutation.LastLevel)
						{
							if (level > 0 && baseMutation.LastLevel <= 0)
							{
								baseMutation.Mutate(baseMutation.ParentObject, baseMutation.BaseLevel);
							}
							else if (level <= 0 && baseMutation.LastLevel > 0)
							{
								baseMutation.Unmutate(baseMutation.ParentObject);
							}
							else
							{
								baseMutation.ChangeLevel(baseMutation.Level);
							}
							flag = true;
						}
					}
				}
				if (flag)
				{
					this.ParentObject.SyncMutationLevelAndGlimmer();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B33D RID: 45885 RVA: 0x00416A00 File Offset: 0x00414C00
		public override bool HandleEvent(SyncMutationLevelsEvent E)
		{
			if (this.MutationList != null)
			{
				if (this.SyncAttempts > 0)
				{
					if (this.SyncAttempts > 100)
					{
						MetricsManager.LogError("Too many mutation sync attempts", new StackTrace());
					}
					else
					{
						this.RestartSync = true;
					}
				}
				else
				{
					try
					{
						bool flag = false;
						for (;;)
						{
							IL_0041:
							this.SyncAttempts++;
							this.RestartSync = false;
							using (List<BaseMutation>.Enumerator enumerator = this.MutationList.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (Mutations.SyncMutation(enumerator.Current, false))
									{
										flag = true;
									}
									if (this.RestartSync)
									{
										goto IL_0041;
									}
								}
							}
							List<string> list = null;
							using (List<Mutations.MutationModifierTracker>.Enumerator enumerator2 = this.MutationMods.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									Mutations.MutationModifierTracker tracker = enumerator2.Current;
									if (!this.MutationList.Exists((BaseMutation m) => m.Name == tracker.mutationName))
									{
										if (list == null)
										{
											list = new List<string>();
										}
										else if (list.Contains(tracker.mutationName))
										{
											continue;
										}
										list.Add(tracker.mutationName);
									}
								}
							}
							if (list != null)
							{
								foreach (string text in list)
								{
									if (Mutations.SyncMutation(this.ParentObject.GetPart(text) as BaseMutation, false))
									{
										flag = true;
									}
									if (this.RestartSync)
									{
										goto IL_0041;
									}
								}
								break;
							}
							break;
						}
						if (flag)
						{
							GlimmerChangeEvent.Send(this.ParentObject);
						}
					}
					finally
					{
						this.SyncAttempts = 0;
						this.RestartSync = false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B33E RID: 45886 RVA: 0x00416C10 File Offset: 0x00414E10
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B33F RID: 45887 RVA: 0x00416C14 File Offset: 0x00414E14
		public static bool SyncMutation(BaseMutation mutation, bool SyncGlimmer = false)
		{
			if (((mutation != null) ? mutation.ParentObject : null) == null)
			{
				return false;
			}
			int level = mutation.Level;
			if (level != mutation.LastLevel)
			{
				if (level <= 0 && mutation.LastLevel > 0)
				{
					mutation.Unmutate(mutation.ParentObject);
				}
				else
				{
					mutation.ChangeLevel(mutation.Level);
				}
				if (SyncGlimmer)
				{
					GlimmerChangeEvent.Send(mutation.ParentObject);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600B340 RID: 45888 RVA: 0x00416C7B File Offset: 0x00414E7B
		public bool IncludedInMutatePool(MutationEntry Entry, bool AllowMultipleDefects = false)
		{
			return Mutations.IncludedInMutatePool(this.ParentObject, this.MutationList, Entry, AllowMultipleDefects);
		}

		// Token: 0x0600B341 RID: 45889 RVA: 0x00416C90 File Offset: 0x00414E90
		private static bool IncludedInMutatePool(GameObject who, List<BaseMutation> CurrentMutations, MutationEntry entry, bool allowMultipleDefects = false)
		{
			if (entry != null && entry.ExcludeFromPool)
			{
				return false;
			}
			string text;
			if (who.Property.TryGetValue("MutationLevel", out text) && !string.IsNullOrEmpty(text) && !MutationFactory.GetMutationEntryByName(text).OkWith(entry, true, allowMultipleDefects))
			{
				return false;
			}
			if (CurrentMutations != null)
			{
				foreach (BaseMutation baseMutation in CurrentMutations)
				{
					if (baseMutation.GetMutationEntry() == entry && (entry == null || !entry.Ranked))
					{
						return false;
					}
					if (!entry.OkWith(baseMutation.GetMutationEntry(), true, allowMultipleDefects))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600B342 RID: 45890 RVA: 0x00416D44 File Offset: 0x00414F44
		public static List<MutationEntry> GetMutatePool(GameObject who, List<BaseMutation> CurrentMutations = null, Predicate<MutationEntry> filter = null, bool allowMultipleDefects = false)
		{
			Mutations._pool.Clear();
			if (CurrentMutations == null)
			{
				Mutations part = who.GetPart<Mutations>();
				if (part != null)
				{
					CurrentMutations = part.MutationList;
				}
			}
			if (filter == null)
			{
				filter = (MutationEntry x) => !x.Defect;
			}
			foreach (MutationCategory mutationCategory in MutationFactory.GetCategories())
			{
				foreach (MutationEntry mutationEntry in mutationCategory.Entries)
				{
					if (Mutations.IncludedInMutatePool(who, CurrentMutations, mutationEntry, allowMultipleDefects) && filter(mutationEntry))
					{
						Mutations._pool.Add(mutationEntry);
					}
				}
			}
			return Mutations._pool;
		}

		// Token: 0x0600B343 RID: 45891 RVA: 0x00416E30 File Offset: 0x00415030
		public List<MutationEntry> GetMutatePool(Predicate<MutationEntry> filter = null, bool allowMultipleDefects = false)
		{
			return Mutations.GetMutatePool(this.ParentObject, this.MutationList, filter, allowMultipleDefects);
		}

		// Token: 0x0600B344 RID: 45892 RVA: 0x00416E45 File Offset: 0x00415045
		[WishCommand("chimericpart", null)]
		public static bool HandleChimericPartWish()
		{
			IComponent<GameObject>.ThePlayer.RequirePart<Mutations>(false).AddChimericBodyPart(false, "Chimera", null);
			return true;
		}

		// Token: 0x0600B345 RID: 45893 RVA: 0x00416E60 File Offset: 0x00415060
		[WishCommand(null, null, Command = "mutation")]
		public static void WishMutation(string argument)
		{
			MutationEntry mutationEntry = null;
			int num = int.MaxValue;
			foreach (MutationEntry mutationEntry2 in MutationFactory.AllMutationEntries())
			{
				if (mutationEntry2.Name.EqualsNoCase(argument))
				{
					Mutations.WishMutationAdd(mutationEntry2.Class, mutationEntry2.Variant);
					return;
				}
				int num2 = Grammar.LevenshteinDistance(argument, mutationEntry2.Name, true);
				if (num2 < num)
				{
					mutationEntry = mutationEntry2;
					num = num2;
				}
			}
			if (mutationEntry != null)
			{
				int num3 = Math.Max(mutationEntry.Name.Length, argument.Length);
				if ((float)(num3 - num) * 1f / (float)num3 > 0.75f && Popup.ShowYesNo("Did you mean " + mutationEntry.Name + "?", "Sounds/UI/ui_notification", true, DialogResult.No, null) == DialogResult.Yes)
				{
					Mutations.WishMutationAdd(mutationEntry.Class, mutationEntry.Variant);
					return;
				}
			}
			string text;
			string text2;
			argument.Split(':', out text, out text2);
			foreach (MutationEntry mutationEntry3 in MutationFactory.AllMutationEntries())
			{
				if (mutationEntry3.Class.EqualsNoCase(text))
				{
					if (text2.IsNullOrEmpty())
					{
						Mutations.WishMutationAdd(mutationEntry3.Class, null);
						return;
					}
					foreach (string text3 in mutationEntry3.GetVariants())
					{
						if (text3.EndsWith(text2, StringComparison.OrdinalIgnoreCase))
						{
							Mutations.WishMutationAdd(mutationEntry3.Class, text3);
							return;
						}
					}
				}
			}
			foreach (Type type in ModManager.GetTypesAssignableFrom(typeof(BaseMutation), false))
			{
				if (!type.IsAbstract)
				{
					BaseMutation genericMutation = Mutations.GetGenericMutation(type.Name, null, null);
					if (genericMutation.GetDisplayName(true).EqualsNoCase(text) || genericMutation.Name.EqualsNoCase(text))
					{
						if (text2.IsNullOrEmpty())
						{
							Mutations.WishMutationAdd(genericMutation.Name, null);
							return;
						}
						foreach (string text4 in genericMutation.GetVariants())
						{
							if (text4.EndsWith(text2, StringComparison.OrdinalIgnoreCase))
							{
								Mutations.WishMutationAdd(genericMutation.Name, text4);
								return;
							}
						}
					}
				}
			}
			Popup.Show(text2.IsNullOrEmpty() ? ("No mutation by the name '" + text + "' could be found.") : string.Concat(new string[] { "No mutation by the name '", text, "' and variant '", text2, "' could be found." }), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600B346 RID: 45894 RVA: 0x00417180 File Offset: 0x00415380
		public static void WishMutationAdd(string Class, string Variant = null)
		{
			BaseMutation baseMutation = BaseMutation.Create(Class, Variant);
			if (Variant.IsNullOrEmpty() && baseMutation.HasVariants)
			{
				baseMutation.SelectVariant(The.Player, false);
			}
			The.Player.RequirePart<Mutations>(false).AddMutation(baseMutation, 1, true);
			IComponent<GameObject>.XDidY(IComponent<GameObject>.ThePlayer, "gain", "the mutation " + baseMutation.GetDisplayName(false), "!", null, null, null, null, false, false, null, null, false, false, false, true, false, null);
		}

		// Token: 0x04004846 RID: 18502
		[NonSerialized]
		private static StringMap<List<string>> MutationVariants = new StringMap<List<string>>(32, 14695981039346656037UL);

		// Token: 0x04004847 RID: 18503
		[NonSerialized]
		private static List<GameObjectBlueprint> MutationEquipment = new List<GameObjectBlueprint>(32);

		// Token: 0x04004848 RID: 18504
		[NonSerialized]
		private static StringMap<BaseMutation> GenericMutations = new StringMap<BaseMutation>(32, 14695981039346656037UL);

		// Token: 0x04004849 RID: 18505
		[NonSerialized]
		public List<BaseMutation> MutationList = new List<BaseMutation>();

		// Token: 0x0400484A RID: 18506
		[NonSerialized]
		public List<Mutations.MutationModifierTracker> MutationMods = new List<Mutations.MutationModifierTracker>();

		// Token: 0x0400484B RID: 18507
		private List<string> FinalizeMutations;

		// Token: 0x0400484C RID: 18508
		private int SyncAttempts;

		// Token: 0x0400484D RID: 18509
		private bool RestartSync;

		// Token: 0x0400484E RID: 18510
		[NonSerialized]
		private static List<MutationEntry> _pool = new List<MutationEntry>();

		// Token: 0x02001CF8 RID: 7416
		[Serializable]
		public class MutationModifierTracker : IComposite
		{
			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x0600F2B3 RID: 62131 RVA: 0x0054EDCD File Offset: 0x0054CFCD
			public bool WantFieldReflection
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600F2B4 RID: 62132 RVA: 0x0054EDD0 File Offset: 0x0054CFD0
			public MutationModifierTracker()
			{
			}

			// Token: 0x0600F2B5 RID: 62133 RVA: 0x0054EDD8 File Offset: 0x0054CFD8
			public MutationModifierTracker(Mutations.MutationModifierTracker Source, bool CopyID = false)
			{
				this.id = (CopyID ? Source.id : Guid.NewGuid());
				this.bonus = Source.bonus;
				this.mutationName = Source.mutationName;
				this.sourceType = Source.sourceType;
				this.sourceName = Source.sourceName;
			}

			// Token: 0x0600F2B6 RID: 62134 RVA: 0x0054EE31 File Offset: 0x0054D031
			public void Write(SerializationWriter Writer)
			{
				Writer.Write(this.id);
				Writer.WriteOptimized(this.bonus);
				Writer.WriteOptimized(this.mutationName);
				Writer.WriteOptimized((int)this.sourceType);
				Writer.WriteOptimized(this.sourceName);
			}

			// Token: 0x0600F2B7 RID: 62135 RVA: 0x0054EE6F File Offset: 0x0054D06F
			public void Read(SerializationReader Reader)
			{
				this.id = Reader.ReadGuid();
				this.bonus = Reader.ReadOptimizedInt32();
				this.mutationName = Reader.ReadOptimizedString();
				this.sourceType = (Mutations.MutationModifierTracker.SourceType)Reader.ReadOptimizedInt32();
				this.sourceName = Reader.ReadOptimizedString();
			}

			// Token: 0x04007175 RID: 29045
			public Guid id;

			// Token: 0x04007176 RID: 29046
			public int bonus;

			// Token: 0x04007177 RID: 29047
			public string mutationName;

			// Token: 0x04007178 RID: 29048
			public Mutations.MutationModifierTracker.SourceType sourceType;

			// Token: 0x04007179 RID: 29049
			public string sourceName;

			// Token: 0x02001E96 RID: 7830
			[Serializable]
			public enum SourceType
			{
				// Token: 0x040076C6 RID: 30406
				Unknown,
				// Token: 0x040076C7 RID: 30407
				External,
				// Token: 0x040076C8 RID: 30408
				StatMod,
				// Token: 0x040076C9 RID: 30409
				Equipment,
				// Token: 0x040076CA RID: 30410
				Cooking,
				// Token: 0x040076CB RID: 30411
				Tonic
			}
		}
	}
}
