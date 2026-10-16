using System;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000E0D RID: 3597
	[Serializable]
	public class Body : IPart
	{
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06009158 RID: 37208 RVA: 0x0034747F File Offset: 0x0034567F
		// (set) Token: 0x06009159 RID: 37209 RVA: 0x00347487 File Offset: 0x00345687
		public string Anatomy
		{
			get
			{
				return this._Anatomy;
			}
			set
			{
				Anatomies.GetAnatomyOrFail(value).ApplyTo(this);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600915A RID: 37210 RVA: 0x00347495 File Offset: 0x00345695
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x0600915B RID: 37211 RVA: 0x0034749C File Offset: 0x0034569C
		public override void Initialize()
		{
			if (this._Anatomy.IsNullOrEmpty())
			{
				string tag = this.ParentObject.GetTag("BodyType", null);
				if (!tag.IsNullOrEmpty())
				{
					this.Anatomy = tag;
				}
			}
			if (this.RequiredMobility > 0 && !this._Anatomy.IsNullOrEmpty())
			{
				int totalMobility = this.GetTotalMobility();
				if (totalMobility < this.RequiredMobility)
				{
					BodyPart bodyPart;
					if ((bodyPart = this.GetFirstPart((BodyPart x) => x.Mobility > 0)) == null)
					{
						bodyPart = this.GetFirstPart(delegate(BodyPart x)
						{
							int? mobility = x.TypeModel().Mobility;
							int num = 0;
							return (mobility.GetValueOrDefault() > num) & (mobility != null);
						}) ?? this.GetBody();
					}
					bodyPart.Mobility += this.RequiredMobility - totalMobility;
				}
			}
		}

		// Token: 0x0600915C RID: 37212 RVA: 0x0034756C File Offset: 0x0034576C
		public override void Attach()
		{
			this.ParentObject.Body = this;
		}

		// Token: 0x0600915D RID: 37213 RVA: 0x0034757A File Offset: 0x0034577A
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Body : null) == this)
			{
				this.ParentObject.Body = null;
			}
		}

		// Token: 0x0600915E RID: 37214 RVA: 0x003475A0 File Offset: 0x003457A0
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			this._Body.Write(Writer);
			if (this.DismemberedParts == null)
			{
				Writer.Write(0);
			}
			else
			{
				Writer.Write(this.DismemberedParts.Count);
				if (this.DismemberedParts.Count > 0)
				{
					foreach (Body.DismemberedPart dismemberedPart in new List<Body.DismemberedPart>(this.DismemberedParts))
					{
						dismemberedPart.Save(Writer);
					}
				}
			}
			base.Write(Basis, Writer);
		}

		// Token: 0x0600915F RID: 37215 RVA: 0x0034763C File Offset: 0x0034583C
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this._Body = new BodyPart(this);
			this._Body.Read(Reader);
			int num = Reader.ReadInt32();
			if (num > 0)
			{
				this.DismemberedParts = new List<Body.DismemberedPart>(num);
				for (int i = 0; i < num; i++)
				{
					Body.DismemberedPart dismemberedPart = new Body.DismemberedPart();
					dismemberedPart.Load(Reader);
					this.DismemberedParts.Add(dismemberedPart);
				}
			}
			else
			{
				this.DismemberedParts = null;
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x06009160 RID: 37216 RVA: 0x003476AE File Offset: 0x003458AE
		public void Clear()
		{
			this._Body.Clear();
		}

		// Token: 0x06009161 RID: 37217 RVA: 0x003476BB File Offset: 0x003458BB
		[Obsolete("version with MapInv argument should always be called")]
		public override IPart DeepCopy(GameObject Parent)
		{
			return base.DeepCopy(Parent);
		}

		// Token: 0x06009162 RID: 37218 RVA: 0x003476C4 File Offset: 0x003458C4
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Body.DeepCopyEquipMap = new Dictionary<GameObject, BodyPart>(8);
			Body body = base.DeepCopy(Parent, MapInv) as Body;
			body.built = false;
			body._Body = this._Body.DeepCopy(Parent, body, null, MapInv, true);
			body.built = true;
			body.UpdateBodyParts(0);
			return body;
		}

		// Token: 0x06009163 RID: 37219 RVA: 0x00347718 File Offset: 0x00345918
		public override void FinalizeCopy(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			base.FinalizeCopy(Source, CopyEffects, CopyID, MapInv);
			if (this.ParentObject.DeepCopyInventoryObjectMap != null && Body.DeepCopyEquipMap != null)
			{
				foreach (GameObject gameObject in Body.DeepCopyEquipMap.Keys)
				{
					if (gameObject.IsValid())
					{
						if (gameObject.IsImplant)
						{
							if (gameObject.HasTag("CyberneticsUsesEqSlot"))
							{
								gameObject.Physics._Equipped = this.ParentObject;
								Body.DeepCopyEquipMap[gameObject].DoEquip(gameObject, false, false);
							}
							Body.DeepCopyEquipMap[gameObject].Implant(gameObject, true, false);
						}
						else
						{
							gameObject.Physics._Equipped = this.ParentObject;
							Body.DeepCopyEquipMap[gameObject].DoEquip(gameObject, false, true);
						}
					}
				}
			}
			Body.DeepCopyEquipMap = null;
		}

		// Token: 0x06009164 RID: 37220 RVA: 0x00347818 File Offset: 0x00345A18
		public int GetWeight()
		{
			if (this.WeightCache == -1)
			{
				return this.RecalculateWeight();
			}
			return this.WeightCache;
		}

		// Token: 0x06009165 RID: 37221 RVA: 0x00347830 File Offset: 0x00345A30
		public int RecalculateWeight()
		{
			this.WeightCache = this._Body.GetWeight();
			return this.WeightCache;
		}

		// Token: 0x06009166 RID: 37222 RVA: 0x00347849 File Offset: 0x00345A49
		public void FlushWeightCache()
		{
			this.WeightCache = -1;
			this.ParentObject.FlushCarriedWeightCache();
		}

		// Token: 0x06009167 RID: 37223 RVA: 0x0034785D File Offset: 0x00345A5D
		public string GetPrimaryLimbType()
		{
			return this.ParentObject.GetPropertyOrTag("PrimaryLimbType", "Hand");
		}

		// Token: 0x06009168 RID: 37224 RVA: 0x00347874 File Offset: 0x00345A74
		public GameObject GetShield(Predicate<GameObject> Filter = null, GameObject Attacker = null)
		{
			GameObject gameObject = null;
			this._Body.GetShield(ref gameObject, Filter, Attacker, this.ParentObject);
			return gameObject;
		}

		// Token: 0x06009169 RID: 37225 RVA: 0x0034789C File Offset: 0x00345A9C
		public GameObject GetShieldWithHighestAV(Predicate<GameObject> Filter = null, GameObject Attacker = null)
		{
			GameObject gameObject = null;
			int minValue = int.MinValue;
			this._Body.GetShieldWithHighestAV(ref gameObject, ref minValue, Filter, Attacker, this.ParentObject);
			return gameObject;
		}

		// Token: 0x0600916A RID: 37226 RVA: 0x003478CC File Offset: 0x00345ACC
		public GameObject GetMainWeapon(out int PossibleWeapons, out BodyPart PrimaryWeaponPart, GameObject Target = null, bool NeedPrimary = false, bool FailDownFromPrimary = false, List<BodyPart> PartList = null)
		{
			PrimaryWeaponPart = null;
			GameObject gameObject = null;
			bool flag = false;
			int num = 0;
			PossibleWeapons = 0;
			this._Body.ScanForWeapon(NeedPrimary, Target, ref gameObject, ref PrimaryWeaponPart, ref num, ref PossibleWeapons, ref flag, PartList);
			if (NeedPrimary && !FailDownFromPrimary && !flag)
			{
				return null;
			}
			return gameObject;
		}

		// Token: 0x0600916B RID: 37227 RVA: 0x0034790C File Offset: 0x00345B0C
		public GameObject GetMainWeapon(GameObject Target = null, bool NeedPrimary = false, bool FailDownFromPrimary = false)
		{
			int num;
			BodyPart bodyPart;
			return this.GetMainWeapon(out num, out bodyPart, Target, NeedPrimary, FailDownFromPrimary, null);
		}

		// Token: 0x0600916C RID: 37228 RVA: 0x00347927 File Offset: 0x00345B27
		public bool HasWeaponOfType(string Type, bool NeedPrimary = false)
		{
			return this._Body.HasWeaponOfType(Type, NeedPrimary);
		}

		// Token: 0x0600916D RID: 37229 RVA: 0x00347936 File Offset: 0x00345B36
		public bool HasPrimaryWeaponOfType(string Type)
		{
			return this.HasWeaponOfType(Type, true);
		}

		// Token: 0x0600916E RID: 37230 RVA: 0x00347940 File Offset: 0x00345B40
		public bool HasWeaponOfTypeOnBodyPartOfType(string WeaponType, string BodyPartType, bool NeedPrimary = false)
		{
			return this._Body.HasWeaponOfTypeOnBodyPartOfType(WeaponType, BodyPartType, NeedPrimary);
		}

		// Token: 0x0600916F RID: 37231 RVA: 0x00347950 File Offset: 0x00345B50
		public GameObject GetWeaponOfType(string Type, bool NeedPrimary = false, bool PreferPrimary = false)
		{
			if (PreferPrimary && !NeedPrimary)
			{
				GameObject weaponOfType = this._Body.GetWeaponOfType(Type, true);
				if (weaponOfType != null)
				{
					return weaponOfType;
				}
			}
			return this._Body.GetWeaponOfType(Type, NeedPrimary);
		}

		// Token: 0x06009170 RID: 37232 RVA: 0x00347983 File Offset: 0x00345B83
		public GameObject GetPrimaryWeaponOfType(string Type)
		{
			return this.GetWeaponOfType(Type, true, false);
		}

		// Token: 0x06009171 RID: 37233 RVA: 0x00347990 File Offset: 0x00345B90
		public GameObject GetPrimaryWeaponOfType(string Type, bool AcceptFirstHandForNonHandPrimary)
		{
			GameObject gameObject = this.GetWeaponOfType(Type, true, false);
			if (gameObject != null)
			{
				return gameObject;
			}
			if (AcceptFirstHandForNonHandPrimary && this.GetPrimaryLimbType() != "Hand")
			{
				BodyPart firstPart = this.GetFirstPart("Hand");
				if (firstPart != null)
				{
					gameObject = firstPart.ThisPartWeaponOfType(Type, false);
					if (gameObject != null)
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x06009172 RID: 37234 RVA: 0x003479E0 File Offset: 0x00345BE0
		public GameObject GetWeaponOfTypeOnBodyPartOfType(string WeaponType, string BodyPartType, bool NeedPrimary = false, bool PreferPrimary = false)
		{
			if (PreferPrimary && !NeedPrimary)
			{
				GameObject weaponOfTypeOnBodyPartOfType = this._Body.GetWeaponOfTypeOnBodyPartOfType(WeaponType, BodyPartType, true);
				if (weaponOfTypeOnBodyPartOfType != null)
				{
					return weaponOfTypeOnBodyPartOfType;
				}
			}
			return this._Body.GetWeaponOfTypeOnBodyPartOfType(WeaponType, BodyPartType, NeedPrimary);
		}

		// Token: 0x06009173 RID: 37235 RVA: 0x00347A16 File Offset: 0x00345C16
		public GameObject GetPrimaryWeaponOfTypeOnBodyPartOfType(string WeaponType, string BodyPartType)
		{
			return this.GetWeaponOfTypeOnBodyPartOfType(WeaponType, BodyPartType, true, false);
		}

		// Token: 0x06009174 RID: 37236 RVA: 0x00347A22 File Offset: 0x00345C22
		public GameObject GetWeapon(Predicate<GameObject> Filter = null)
		{
			return this._Body.GetWeapon(Filter);
		}

		// Token: 0x06009175 RID: 37237 RVA: 0x00347A30 File Offset: 0x00345C30
		public bool HasWeapon(Predicate<GameObject> Filter = null)
		{
			return this._Body.HasWeapon(Filter);
		}

		// Token: 0x06009176 RID: 37238 RVA: 0x00347A3E File Offset: 0x00345C3E
		public bool IsPrimaryWeapon(GameObject Object)
		{
			return this._Body.IsPrimaryWeapon(Object);
		}

		// Token: 0x06009177 RID: 37239 RVA: 0x00347A4C File Offset: 0x00345C4C
		public void ClearShieldBlocks()
		{
			this._Body.ClearShieldBlocks();
		}

		// Token: 0x06009178 RID: 37240 RVA: 0x00347A5C File Offset: 0x00345C5C
		public List<GameObject> StripAllEquipment()
		{
			List<GameObject> list = new List<GameObject>();
			this._Body.StripAllEquipment(list);
			return list;
		}

		// Token: 0x06009179 RID: 37241 RVA: 0x00347A7C File Offset: 0x00345C7C
		public List<GameObject> GetPrimaryHandEquippedObjects()
		{
			List<GameObject> list = new List<GameObject>();
			this._Body.GetPrimaryHandEquippedObjects(list);
			return list;
		}

		// Token: 0x0600917A RID: 37242 RVA: 0x00347A9C File Offset: 0x00345C9C
		public List<GameObject> GetPrimaryEquippedObjects()
		{
			List<GameObject> list = new List<GameObject>();
			this._Body.GetPrimaryEquippedObjects(list);
			return list;
		}

		// Token: 0x0600917B RID: 37243 RVA: 0x00347ABC File Offset: 0x00345CBC
		public void GetEquippedObjects(List<GameObject> Return)
		{
			this._Body.GetEquippedObjects(Return);
		}

		// Token: 0x0600917C RID: 37244 RVA: 0x00347ACA File Offset: 0x00345CCA
		public void GetEquippedObjects(List<GameObject> Return, Predicate<GameObject> Filter)
		{
			this._Body.GetEquippedObjects(Return, Filter);
		}

		// Token: 0x0600917D RID: 37245 RVA: 0x00347ADC File Offset: 0x00345CDC
		public List<GameObject> GetEquippedObjects()
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetEquippedObjectCount());
			this._Body.GetEquippedObjects(list);
			return list;
		}

		// Token: 0x0600917E RID: 37246 RVA: 0x00347B08 File Offset: 0x00345D08
		public List<GameObject> GetEquippedObjects(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetEquippedObjects();
			}
			List<GameObject> list = new List<GameObject>(this._Body.GetEquippedObjectCount(Filter));
			this._Body.GetEquippedObjects(list, Filter);
			return list;
		}

		// Token: 0x0600917F RID: 37247 RVA: 0x00347B3F File Offset: 0x00345D3F
		public List<GameObject> GetEquippedObjectsReadonly()
		{
			return this._Body.GetEquippedObjectsReadonly();
		}

		// Token: 0x06009180 RID: 37248 RVA: 0x00347B4C File Offset: 0x00345D4C
		public List<GameObject> GetEquippedObjectsReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetEquippedObjectsReadonly();
			}
			return this._Body.GetEquippedObjectsReadonly(Filter);
		}

		// Token: 0x06009181 RID: 37249 RVA: 0x00347B64 File Offset: 0x00345D64
		public void GetEquippedObjectsExceptNatural(IList<GameObject> Return)
		{
			this._Body.GetEquippedObjectsExceptNatural(Return);
		}

		// Token: 0x06009182 RID: 37250 RVA: 0x00347B74 File Offset: 0x00345D74
		public List<GameObject> GetEquippedObjectsExceptNatural()
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetEquippedObjectCount());
			this._Body.GetEquippedObjectsExceptNatural(list);
			return list;
		}

		// Token: 0x06009183 RID: 37251 RVA: 0x00347BA0 File Offset: 0x00345DA0
		public List<GameObject> GetEquippedObjectsExceptNatural(Predicate<GameObject> Filter)
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetEquippedObjectCount(Filter));
			this._Body.GetEquippedObjectsExceptNatural(list, Filter);
			return list;
		}

		// Token: 0x06009184 RID: 37252 RVA: 0x00347BCD File Offset: 0x00345DCD
		public void ForeachEquippedObject(Action<GameObject> Proc)
		{
			this._Body.ForeachEquippedObject(Proc);
		}

		// Token: 0x06009185 RID: 37253 RVA: 0x00347BDB File Offset: 0x00345DDB
		public void SafeForeachEquippedObject(Action<GameObject> Proc)
		{
			this._Body.SafeForeachEquippedObject(Proc);
		}

		// Token: 0x06009186 RID: 37254 RVA: 0x00347BE9 File Offset: 0x00345DE9
		public void ForeachDefaultBehavior(Action<GameObject> Proc)
		{
			this._Body.ForeachDefaultBehavior(Proc);
		}

		// Token: 0x06009187 RID: 37255 RVA: 0x00347BF7 File Offset: 0x00345DF7
		public void SafeForeachDefaultBehavior(Action<GameObject> Proc)
		{
			this._Body.SafeForeachDefaultBehavior(Proc);
		}

		// Token: 0x06009188 RID: 37256 RVA: 0x00347C05 File Offset: 0x00345E05
		public bool HasInstalledCybernetics(string Blueprint = null, Predicate<GameObject> Filter = null)
		{
			return this._Body.HasInstalledCybernetics(Blueprint, Filter);
		}

		// Token: 0x06009189 RID: 37257 RVA: 0x00347C14 File Offset: 0x00345E14
		public void GetInstalledCybernetics(List<GameObject> Return)
		{
			this._Body.GetInstalledCybernetics(Return);
		}

		// Token: 0x0600918A RID: 37258 RVA: 0x00347C24 File Offset: 0x00345E24
		public List<GameObject> GetInstalledCybernetics()
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetInstalledCyberneticsCount());
			this._Body.GetInstalledCybernetics(list);
			return list;
		}

		// Token: 0x0600918B RID: 37259 RVA: 0x00347C4F File Offset: 0x00345E4F
		public List<GameObject> GetInstalledCyberneticsReadonly()
		{
			return this._Body.GetInstalledCyberneticsReadonly();
		}

		// Token: 0x0600918C RID: 37260 RVA: 0x00347C5C File Offset: 0x00345E5C
		public List<GameObject> GetInstalledCybernetics(Predicate<GameObject> Filter)
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetInstalledCyberneticsCount(Filter));
			this._Body.GetInstalledCybernetics(list, Filter);
			return list;
		}

		// Token: 0x0600918D RID: 37261 RVA: 0x00347C89 File Offset: 0x00345E89
		public bool AnyInstalledCybernetics()
		{
			return this._Body.AnyInstalledCybernetics();
		}

		// Token: 0x0600918E RID: 37262 RVA: 0x00347C96 File Offset: 0x00345E96
		public bool AnyInstalledCybernetics(Predicate<GameObject> pFilter)
		{
			return this._Body.AnyInstalledCybernetics(pFilter);
		}

		// Token: 0x0600918F RID: 37263 RVA: 0x00347CA4 File Offset: 0x00345EA4
		public void ForeachInstalledCybernetics(Action<GameObject> aProc)
		{
			this._Body.ForeachInstalledCybernetics(aProc);
		}

		// Token: 0x06009190 RID: 37264 RVA: 0x00347CB2 File Offset: 0x00345EB2
		public void SafeForeachInstalledCybernetics(Action<GameObject> aProc)
		{
			this._Body.SafeForeachInstalledCybernetics(aProc);
		}

		// Token: 0x06009191 RID: 37265 RVA: 0x00347CC0 File Offset: 0x00345EC0
		public void GetEquippedObjectsAndInstalledCybernetics(List<GameObject> Return)
		{
			this._Body.GetEquippedObjectsAndInstalledCybernetics(Return);
		}

		// Token: 0x06009192 RID: 37266 RVA: 0x00347CD0 File Offset: 0x00345ED0
		public List<GameObject> GetEquippedObjectsAndInstalledCybernetics()
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetEquippedObjectCount() + this._Body.GetInstalledCyberneticsCount());
			this._Body.GetEquippedObjectsAndInstalledCybernetics(list);
			return list;
		}

		// Token: 0x06009193 RID: 37267 RVA: 0x00347D07 File Offset: 0x00345F07
		public List<GameObject> GetEquippedObjectsAndInstalledCyberneticsReadonly()
		{
			return this._Body.GetEquippedObjectsAndInstalledCyberneticsReadonly();
		}

		// Token: 0x06009194 RID: 37268 RVA: 0x00347D14 File Offset: 0x00345F14
		public List<GameObject> GetEquippedObjectsAndInstalledCybernetics(Predicate<GameObject> Filter)
		{
			List<GameObject> list = new List<GameObject>(this._Body.GetEquippedObjectCount(Filter) + this._Body.GetInstalledCyberneticsCount(Filter));
			this._Body.GetEquippedObjectsAndInstalledCybernetics(list, Filter);
			return list;
		}

		// Token: 0x06009195 RID: 37269 RVA: 0x00347D4E File Offset: 0x00345F4E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009196 RID: 37270 RVA: 0x00347D54 File Offset: 0x00345F54
		public bool HasPrimaryHand()
		{
			using (List<BodyPart>.Enumerator enumerator = this.GetPart("Hand").GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Primary)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06009197 RID: 37271 RVA: 0x00347DB4 File Offset: 0x00345FB4
		public BodyPart GetBody()
		{
			return this._Body;
		}

		// Token: 0x06009198 RID: 37272 RVA: 0x00347DBC File Offset: 0x00345FBC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._Body.ToString(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06009199 RID: 37273 RVA: 0x00347DE1 File Offset: 0x00345FE1
		public bool AnyRegisteredEvent(string ID)
		{
			return this._Body.AnyRegisteredEvent(ID);
		}

		// Token: 0x0600919A RID: 37274 RVA: 0x00347DF0 File Offset: 0x00345FF0
		public BodyPart GetPartByManager(string Manager, bool EvenIfDismembered = false)
		{
			BodyPart bodyPart = this._Body.FindByManager(Manager);
			if (bodyPart != null)
			{
				return bodyPart;
			}
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				for (int i = this.DismemberedParts.Count - 1; i >= 0; i--)
				{
					if (this.DismemberedParts[i].Part.Manager == Manager)
					{
						return this.DismemberedParts[i].Part;
					}
				}
			}
			return null;
		}

		// Token: 0x0600919B RID: 37275 RVA: 0x00347E64 File Offset: 0x00346064
		public BodyPart GetPartByManager(string Manager, string Type, bool EvenIfDismembered = false)
		{
			BodyPart bodyPart = this._Body.FindByManager(Manager, Type);
			if (bodyPart != null)
			{
				return bodyPart;
			}
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				for (int i = this.DismemberedParts.Count - 1; i >= 0; i--)
				{
					if (this.DismemberedParts[i].Part.Manager == Manager && this.DismemberedParts[i].Part.Type == Type)
					{
						return this.DismemberedParts[i].Part;
					}
				}
			}
			return null;
		}

		// Token: 0x0600919C RID: 37276 RVA: 0x00347EF8 File Offset: 0x003460F8
		public void GetPartsByManager(string Manager, List<BodyPart> Store, bool EvenIfDismembered = false)
		{
			this._Body.FindByManager(Manager, Store);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				for (int i = this.DismemberedParts.Count - 1; i >= 0; i--)
				{
					if (this.DismemberedParts[i].Part.Manager == Manager)
					{
						Store.Add(this.DismemberedParts[i].Part);
					}
				}
			}
		}

		// Token: 0x0600919D RID: 37277 RVA: 0x00347F6C File Offset: 0x0034616C
		public void GetPartsByManager(string Manager, string Type, List<BodyPart> Store, bool EvenIfDismembered = false)
		{
			this._Body.FindByManager(Manager, Store);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				for (int i = this.DismemberedParts.Count - 1; i >= 0; i--)
				{
					if (this.DismemberedParts[i].Part.Manager == Manager && this.DismemberedParts[i].Part.Type == Type)
					{
						Store.Add(this.DismemberedParts[i].Part);
					}
				}
			}
		}

		// Token: 0x0600919E RID: 37278 RVA: 0x00348000 File Offset: 0x00346200
		public int RemovePartsByManager(string Manager, bool EvenIfDismembered = false)
		{
			int num = 0;
			BodyPart partByManager;
			while ((partByManager = this.GetPartByManager(Manager, false)) != null)
			{
				this.RemovePart(partByManager, false);
				num++;
			}
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				for (int i = this.DismemberedParts.Count - 1; i >= 0; i--)
				{
					if (this.DismemberedParts[i].Part.Manager == Manager)
					{
						this.DismemberedParts.RemoveAt(i);
						num++;
					}
				}
			}
			if (num > 0)
			{
				this.UpdateBodyParts(0);
				this.RecalculateArmor();
			}
			return num;
		}

		// Token: 0x0600919F RID: 37279 RVA: 0x0034808D File Offset: 0x0034628D
		public void FindPartsEquipping(GameObject GO, List<BodyPart> Return)
		{
			this._Body.FindPartsEquipping(GO, Return);
		}

		// Token: 0x060091A0 RID: 37280 RVA: 0x0034809C File Offset: 0x0034629C
		public List<BodyPart> FindPartsEquipping(GameObject GO)
		{
			List<BodyPart> list = new List<BodyPart>();
			this.FindPartsEquipping(GO, list);
			return list;
		}

		// Token: 0x060091A1 RID: 37281 RVA: 0x003480B8 File Offset: 0x003462B8
		public BodyPart FindParentPartOf(BodyPart FindPart)
		{
			return this._Body.FindParentPartOf(FindPart);
		}

		// Token: 0x060091A2 RID: 37282 RVA: 0x003480C6 File Offset: 0x003462C6
		public BodyPart FindPreviousPartOf(BodyPart FindPart)
		{
			return this._Body.FindPreviousPartOf(FindPart);
		}

		// Token: 0x060091A3 RID: 37283 RVA: 0x003480D4 File Offset: 0x003462D4
		public BodyPart FindNextPartOf(BodyPart FindPart)
		{
			return this._Body.FindNextPartOf(FindPart);
		}

		// Token: 0x060091A4 RID: 37284 RVA: 0x003480E2 File Offset: 0x003462E2
		public BodyPart FindEquippedItem(GameObject GO)
		{
			return this._Body.FindEquippedItem(GO);
		}

		// Token: 0x060091A5 RID: 37285 RVA: 0x003480F0 File Offset: 0x003462F0
		public BodyPart FindEquippedItem(string Blueprint)
		{
			return this._Body.FindEquippedItem(Blueprint);
		}

		// Token: 0x060091A6 RID: 37286 RVA: 0x003480FE File Offset: 0x003462FE
		public BodyPart FindDefaultOrEquippedItem(GameObject GO)
		{
			return this._Body.FindDefaultOrEquippedItem(GO);
		}

		// Token: 0x060091A7 RID: 37287 RVA: 0x0034810C File Offset: 0x0034630C
		public BodyPart FindDefaultOrEquippedItem(string Blueprint)
		{
			return this._Body.FindDefaultOrEquippedItem(Blueprint);
		}

		// Token: 0x060091A8 RID: 37288 RVA: 0x0034811A File Offset: 0x0034631A
		public GameObject FindEquipmentOrDefaultByBlueprint(string Blueprint)
		{
			return this._Body.FindEquipmentOrDefaultByBlueprint(Blueprint);
		}

		// Token: 0x060091A9 RID: 37289 RVA: 0x00348128 File Offset: 0x00346328
		public GameObject FindEquipmentOrDefaultByID(string ID)
		{
			return this._Body.FindEquipmentOrDefaultByID(ID);
		}

		// Token: 0x060091AA RID: 37290 RVA: 0x00348136 File Offset: 0x00346336
		public GameObject FindEquipmentByEvent(string ID)
		{
			return this._Body.FindEquipmentByEvent(ID);
		}

		// Token: 0x060091AB RID: 37291 RVA: 0x00348144 File Offset: 0x00346344
		public GameObject FindEquipmentByEvent(Event E)
		{
			return this._Body.FindEquipmentByEvent(E);
		}

		// Token: 0x060091AC RID: 37292 RVA: 0x00348152 File Offset: 0x00346352
		public GameObject FindEquipmentOrCyberneticsByEvent(string ID)
		{
			return this._Body.FindEquipmentOrCyberneticsByEvent(ID);
		}

		// Token: 0x060091AD RID: 37293 RVA: 0x00348160 File Offset: 0x00346360
		public GameObject FindEquipmentOrCyberneticsByEvent(Event E)
		{
			return this._Body.FindEquipmentOrCyberneticsByEvent(E);
		}

		// Token: 0x060091AE RID: 37294 RVA: 0x0034816E File Offset: 0x0034636E
		public GameObject FindEquippedItem(Predicate<GameObject> Filter)
		{
			return this._Body.FindEquippedItem(Filter);
		}

		// Token: 0x060091AF RID: 37295 RVA: 0x0034817C File Offset: 0x0034637C
		public bool HasEquippedItem(GameObject GO)
		{
			return this._Body.HasEquippedItem(GO);
		}

		// Token: 0x060091B0 RID: 37296 RVA: 0x0034818A File Offset: 0x0034638A
		public bool HasEquippedItem(string Blueprint)
		{
			return this._Body.HasEquippedItem(Blueprint);
		}

		// Token: 0x060091B1 RID: 37297 RVA: 0x00348198 File Offset: 0x00346398
		public bool HasEquippedItem(Predicate<GameObject> Filter)
		{
			return this._Body.HasEquippedItem(Filter);
		}

		// Token: 0x060091B2 RID: 37298 RVA: 0x003481A6 File Offset: 0x003463A6
		public bool IsItemEquippedOnLimbType(GameObject GO, string FindType)
		{
			return this._Body.IsItemEquippedOnLimbType(GO, FindType);
		}

		// Token: 0x060091B3 RID: 37299 RVA: 0x003481B5 File Offset: 0x003463B5
		public BodyPart FindCybernetics(GameObject GO)
		{
			return this._Body.FindCybernetics(GO);
		}

		// Token: 0x060091B4 RID: 37300 RVA: 0x003481C3 File Offset: 0x003463C3
		public bool IsItemImplantedInLimbType(GameObject GO, string FindType)
		{
			return this._Body.IsItemImplantedInLimbType(GO, FindType);
		}

		// Token: 0x060091B5 RID: 37301 RVA: 0x003481D2 File Offset: 0x003463D2
		public bool IsADefaultBehavior(GameObject obj)
		{
			return this._Body.IsADefaultBehavior(obj);
		}

		// Token: 0x060091B6 RID: 37302 RVA: 0x003481E0 File Offset: 0x003463E0
		public bool IsItemDefaultBehaviorOnLimbType(GameObject GO, string FindType)
		{
			return this._Body.IsItemDefaultBehaviorOnLimbType(GO, FindType);
		}

		// Token: 0x060091B7 RID: 37303 RVA: 0x003481EF File Offset: 0x003463EF
		public BodyPart FindDefaultBehavior(GameObject GO)
		{
			return this._Body.FindDefaultBehavior(GO);
		}

		// Token: 0x060091B8 RID: 37304 RVA: 0x00348200 File Offset: 0x00346400
		public GameObject FindObject(Predicate<GameObject> pFilter)
		{
			BodyPart part = this._Body.GetPart((BodyPart P) => P.Equipped != null && pFilter(P.Equipped));
			if (part != null)
			{
				return part.Equipped;
			}
			return null;
		}

		// Token: 0x060091B9 RID: 37305 RVA: 0x00348240 File Offset: 0x00346440
		public GameObject FindObjectByBlueprint(string Blueprint)
		{
			BodyPart part = this._Body.GetPart((BodyPart P) => P.Equipped != null && P.Equipped.Blueprint == Blueprint);
			if (part != null)
			{
				return part.Equipped;
			}
			return null;
		}

		// Token: 0x060091BA RID: 37306 RVA: 0x0034827D File Offset: 0x0034647D
		public void ForeachPart(Action<BodyPart> aProc)
		{
			this._Body.ForeachPart(aProc);
		}

		// Token: 0x060091BB RID: 37307 RVA: 0x0034828B File Offset: 0x0034648B
		public bool ForeachPart(Predicate<BodyPart> pProc)
		{
			return this._Body.ForeachPart(pProc);
		}

		// Token: 0x060091BC RID: 37308 RVA: 0x0034829C File Offset: 0x0034649C
		public List<BodyPart> GetEquippedParts()
		{
			int partCount = this.GetPartCount((BodyPart P) => P.Equipped != null);
			List<BodyPart> Return = new List<BodyPart>(partCount);
			if (partCount > 0)
			{
				this._Body.ForeachPart(delegate(BodyPart P)
				{
					if (P.Equipped != null)
					{
						Return.Add(P);
					}
				});
			}
			return Return;
		}

		// Token: 0x060091BD RID: 37309 RVA: 0x00348302 File Offset: 0x00346502
		public void GetParts(List<BodyPart> Return)
		{
			this._Body.GetParts(Return);
		}

		// Token: 0x060091BE RID: 37310 RVA: 0x00348310 File Offset: 0x00346510
		public void GetParts(List<BodyPart> Return, bool EvenIfDismembered)
		{
			this._Body.GetParts(Return);
			if (EvenIfDismembered && this.DismemberedParts != null && this.DismemberedParts.Count > 0)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					Return.Add(dismemberedPart.Part);
				}
				Return.Sort(new Comparison<BodyPart>(BodyPart.Sort));
			}
		}

		// Token: 0x060091BF RID: 37311 RVA: 0x003483A0 File Offset: 0x003465A0
		public List<BodyPart> GetParts()
		{
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount());
			this.GetParts(list);
			return list;
		}

		// Token: 0x060091C0 RID: 37312 RVA: 0x003483C4 File Offset: 0x003465C4
		public List<BodyPart> GetParts(bool EvenIfDismembered)
		{
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount());
			this.GetParts(list, EvenIfDismembered);
			return list;
		}

		// Token: 0x060091C1 RID: 37313 RVA: 0x003483E6 File Offset: 0x003465E6
		public IEnumerable<BodyPart> LoopParts()
		{
			foreach (BodyPart bodyPart in this._Body.LoopParts())
			{
				yield return bodyPart;
			}
			IEnumerator<BodyPart> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060091C2 RID: 37314 RVA: 0x003483F6 File Offset: 0x003465F6
		public void GetTopLevelDynamicParts(List<BodyPart> Return)
		{
			this._Body.GetTopLevelDynamicParts(Return);
		}

		// Token: 0x060091C3 RID: 37315 RVA: 0x00348404 File Offset: 0x00346604
		public List<BodyPart> GetTopLevelDynamicParts()
		{
			List<BodyPart> list = new List<BodyPart>();
			this.GetTopLevelDynamicParts(list);
			return list;
		}

		// Token: 0x060091C4 RID: 37316 RVA: 0x0034841F File Offset: 0x0034661F
		public void GetPartsSkippingDynamicTrees(List<BodyPart> Return)
		{
			this._Body.GetPartsSkippingDynamicTrees(Return);
		}

		// Token: 0x060091C5 RID: 37317 RVA: 0x00348430 File Offset: 0x00346630
		public List<BodyPart> GetPartsSkippingDynamicTrees()
		{
			List<BodyPart> list = new List<BodyPart>();
			this.GetPartsSkippingDynamicTrees(list);
			return list;
		}

		// Token: 0x060091C6 RID: 37318 RVA: 0x0034844B File Offset: 0x0034664B
		public void GetConcreteParts(List<BodyPart> Return)
		{
			this._Body.GetConcreteParts(Return);
		}

		// Token: 0x060091C7 RID: 37319 RVA: 0x0034845C File Offset: 0x0034665C
		public List<BodyPart> GetConcreteParts()
		{
			List<BodyPart> list = new List<BodyPart>(this.GetConcretePartCount());
			this.GetConcreteParts(list);
			return list;
		}

		// Token: 0x060091C8 RID: 37320 RVA: 0x0034847D File Offset: 0x0034667D
		public void GetAbstractParts(List<BodyPart> Return)
		{
			this._Body.GetAbstractParts(Return);
		}

		// Token: 0x060091C9 RID: 37321 RVA: 0x0034848C File Offset: 0x0034668C
		public List<BodyPart> GetAbstractParts()
		{
			List<BodyPart> list = new List<BodyPart>(this.GetAbstractPartCount());
			this.GetAbstractParts(list);
			return list;
		}

		// Token: 0x060091CA RID: 37322 RVA: 0x003484AD File Offset: 0x003466AD
		public int GetPartCount()
		{
			return this._Body.GetPartCount();
		}

		// Token: 0x060091CB RID: 37323 RVA: 0x003484BA File Offset: 0x003466BA
		public int GetPartCount(string RequiredType)
		{
			return this._Body.GetPartCount(RequiredType);
		}

		// Token: 0x060091CC RID: 37324 RVA: 0x003484C8 File Offset: 0x003466C8
		public int GetPartCount(string RequiredType, int RequiredLaterality)
		{
			return this._Body.GetPartCount(RequiredType, RequiredLaterality);
		}

		// Token: 0x060091CD RID: 37325 RVA: 0x003484D7 File Offset: 0x003466D7
		public int GetPartCount(Predicate<BodyPart> Filter)
		{
			return this._Body.GetPartCount(Filter);
		}

		// Token: 0x060091CE RID: 37326 RVA: 0x003484E5 File Offset: 0x003466E5
		public int GetConcretePartCount()
		{
			return this._Body.GetConcretePartCount();
		}

		// Token: 0x060091CF RID: 37327 RVA: 0x003484F2 File Offset: 0x003466F2
		public int GetAbstractPartCount()
		{
			return this._Body.GetAbstractPartCount();
		}

		// Token: 0x060091D0 RID: 37328 RVA: 0x003484FF File Offset: 0x003466FF
		public bool AnyCategoryParts(int FindCategory)
		{
			return this._Body.AnyCategoryParts(FindCategory);
		}

		// Token: 0x060091D1 RID: 37329 RVA: 0x0034850D File Offset: 0x0034670D
		public int GetCategoryPartCount(int FindCategory)
		{
			return this._Body.GetCategoryPartCount(FindCategory);
		}

		// Token: 0x060091D2 RID: 37330 RVA: 0x0034851B File Offset: 0x0034671B
		public int GetCategoryPartCount(int FindCategory, string FindType)
		{
			return this._Body.GetCategoryPartCount(FindCategory, FindType);
		}

		// Token: 0x060091D3 RID: 37331 RVA: 0x0034852A File Offset: 0x0034672A
		public int GetCategoryPartCount(int FindCategory, Predicate<BodyPart> pFilter)
		{
			return this._Body.GetCategoryPartCount(FindCategory, pFilter);
		}

		// Token: 0x060091D4 RID: 37332 RVA: 0x00348539 File Offset: 0x00346739
		public int GetNativePartCount()
		{
			return this._Body.GetNativePartCount();
		}

		// Token: 0x060091D5 RID: 37333 RVA: 0x00348546 File Offset: 0x00346746
		public int GetNativePartCount(string RequiredType)
		{
			return this._Body.GetNativePartCount(RequiredType);
		}

		// Token: 0x060091D6 RID: 37334 RVA: 0x00348554 File Offset: 0x00346754
		public int GetNativePartCount(Predicate<BodyPart> pFilter)
		{
			return this._Body.GetNativePartCount(pFilter);
		}

		// Token: 0x060091D7 RID: 37335 RVA: 0x00348562 File Offset: 0x00346762
		public int GetAddedPartCount()
		{
			return this._Body.GetAddedPartCount();
		}

		// Token: 0x060091D8 RID: 37336 RVA: 0x0034856F File Offset: 0x0034676F
		public int GetAddedPartCount(string RequiredType)
		{
			return this._Body.GetAddedPartCount(RequiredType);
		}

		// Token: 0x060091D9 RID: 37337 RVA: 0x0034857D File Offset: 0x0034677D
		public int GetAddedPartCount(Predicate<BodyPart> pFilter)
		{
			return this._Body.GetAddedPartCount(pFilter);
		}

		// Token: 0x060091DA RID: 37338 RVA: 0x0034858B File Offset: 0x0034678B
		public int GetMortalPartCount()
		{
			return this._Body.GetMortalPartCount();
		}

		// Token: 0x060091DB RID: 37339 RVA: 0x00348598 File Offset: 0x00346798
		public int GetMortalPartCount(string RequiredType)
		{
			return this._Body.GetMortalPartCount(RequiredType);
		}

		// Token: 0x060091DC RID: 37340 RVA: 0x003485A6 File Offset: 0x003467A6
		public int GetMortalPartCount(Predicate<BodyPart> pFilter)
		{
			return this._Body.GetMortalPartCount(pFilter);
		}

		// Token: 0x060091DD RID: 37341 RVA: 0x003485B4 File Offset: 0x003467B4
		public bool AnyMortalParts()
		{
			return this._Body.AnyMortalParts();
		}

		// Token: 0x060091DE RID: 37342 RVA: 0x003485C1 File Offset: 0x003467C1
		public bool IsEquippedOnType(GameObject FindObj, string FindType)
		{
			return this._Body.IsEquippedOnType(FindObj, FindType);
		}

		// Token: 0x060091DF RID: 37343 RVA: 0x003485D0 File Offset: 0x003467D0
		public bool IsEquippedOnCategory(GameObject FindObj, int FindCategory)
		{
			return this._Body.IsEquippedOnCategory(FindObj, FindCategory);
		}

		// Token: 0x060091E0 RID: 37344 RVA: 0x003485DF File Offset: 0x003467DF
		public bool IsEquippedOnPrimary(GameObject FindObj)
		{
			return this._Body.IsEquippedOnPrimary(FindObj);
		}

		// Token: 0x060091E1 RID: 37345 RVA: 0x003485ED File Offset: 0x003467ED
		public bool IsImplantedInCategory(GameObject FindObj, int FindCategory)
		{
			return this._Body.IsImplantedInCategory(FindObj, FindCategory);
		}

		// Token: 0x060091E2 RID: 37346 RVA: 0x003485FC File Offset: 0x003467FC
		public void GetMobilityProvidingParts(List<BodyPart> Return)
		{
			this._Body.GetMobilityProvidingParts(Return);
		}

		// Token: 0x060091E3 RID: 37347 RVA: 0x0034860C File Offset: 0x0034680C
		public List<BodyPart> GetMobilityProvidingParts()
		{
			List<BodyPart> list = new List<BodyPart>();
			this.GetMobilityProvidingParts(list);
			return list;
		}

		// Token: 0x060091E4 RID: 37348 RVA: 0x00348627 File Offset: 0x00346827
		public void GetConcreteMobilityProvidingParts(List<BodyPart> Return)
		{
			this._Body.GetConcreteMobilityProvidingParts(Return);
		}

		// Token: 0x060091E5 RID: 37349 RVA: 0x00348638 File Offset: 0x00346838
		public List<BodyPart> GetConcreteMobilityProvidingParts()
		{
			List<BodyPart> list = new List<BodyPart>();
			this.GetConcreteMobilityProvidingParts(list);
			return list;
		}

		// Token: 0x060091E6 RID: 37350 RVA: 0x00348653 File Offset: 0x00346853
		public int GetTotalMobility()
		{
			return this._Body.GetTotalMobility();
		}

		// Token: 0x060091E7 RID: 37351 RVA: 0x00348660 File Offset: 0x00346860
		public int GetBodyMobility()
		{
			return this._Body.Mobility;
		}

		// Token: 0x060091E8 RID: 37352 RVA: 0x0034866D File Offset: 0x0034686D
		public void MarkAllNative()
		{
			this._Body.MarkAllNative();
		}

		// Token: 0x060091E9 RID: 37353 RVA: 0x0034867A File Offset: 0x0034687A
		public void CategorizeAll(int ApplyCategory)
		{
			this._Body.CategorizeAll(ApplyCategory);
		}

		// Token: 0x060091EA RID: 37354 RVA: 0x00348688 File Offset: 0x00346888
		public void CategorizeAllExcept(int ApplyCategory, int SkipCategory)
		{
			this._Body.CategorizeAllExcept(ApplyCategory, SkipCategory);
		}

		// Token: 0x060091EB RID: 37355 RVA: 0x00348697 File Offset: 0x00346897
		public void GetPartsEquippedOn(GameObject obj, List<BodyPart> Result)
		{
			this._Body.GetPartsEquippedOn(obj, Result);
		}

		// Token: 0x060091EC RID: 37356 RVA: 0x003486A6 File Offset: 0x003468A6
		public List<BodyPart> GetPartsEquippedOn(GameObject obj)
		{
			return this._Body.GetPartsEquippedOn(obj);
		}

		// Token: 0x060091ED RID: 37357 RVA: 0x003486B4 File Offset: 0x003468B4
		public int GetPartCountEquippedOn(GameObject obj)
		{
			return this._Body.GetPartCountEquippedOn(obj);
		}

		// Token: 0x060091EE RID: 37358 RVA: 0x003486C4 File Offset: 0x003468C4
		public List<BodyPart> GetUnequippedPart(string RequiredType)
		{
			List<BodyPart> list = new List<BodyPart>();
			this._Body.GetUnequippedPart(RequiredType, list);
			return list;
		}

		// Token: 0x060091EF RID: 37359 RVA: 0x003486E8 File Offset: 0x003468E8
		public List<BodyPart> GetUnequippedPart(string RequiredType, int RequiredLaterality)
		{
			List<BodyPart> list = new List<BodyPart>();
			this._Body.GetUnequippedPart(RequiredType, RequiredLaterality, list);
			return list;
		}

		// Token: 0x060091F0 RID: 37360 RVA: 0x0034870B File Offset: 0x0034690B
		public int GetUnequippedPartCount(string RequiredType)
		{
			return this._Body.GetUnequippedPartCount(RequiredType);
		}

		// Token: 0x060091F1 RID: 37361 RVA: 0x00348719 File Offset: 0x00346919
		public int GetUnequippedPartCount(string RequiredType, int RequiredLaterality)
		{
			return this._Body.GetUnequippedPartCount(RequiredType, RequiredLaterality);
		}

		// Token: 0x060091F2 RID: 37362 RVA: 0x00348728 File Offset: 0x00346928
		public int GetUnequippedPartCountExcept(string RequiredType, BodyPart ExceptPart)
		{
			return this._Body.GetUnequippedPartCountExcept(RequiredType, ExceptPart);
		}

		// Token: 0x060091F3 RID: 37363 RVA: 0x00348737 File Offset: 0x00346937
		public int GetUnequippedPartCountExcept(string RequiredType, int RequiredLaterality, BodyPart ExceptPart)
		{
			return this._Body.GetUnequippedPartCountExcept(RequiredType, RequiredLaterality, ExceptPart);
		}

		// Token: 0x060091F4 RID: 37364 RVA: 0x00348747 File Offset: 0x00346947
		public List<BodyPart> GetPartStatic(string RequiredType)
		{
			if (Body.partStatic == null)
			{
				Body.partStatic = new List<BodyPart>(4);
			}
			else
			{
				Body.partStatic.Clear();
			}
			this._Body.GetPart(RequiredType, Body.partStatic);
			return Body.partStatic;
		}

		// Token: 0x060091F5 RID: 37365 RVA: 0x00348780 File Offset: 0x00346980
		public List<BodyPart> GetPart(string RequiredType)
		{
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount(RequiredType));
			this._Body.GetPart(RequiredType, list);
			return list;
		}

		// Token: 0x060091F6 RID: 37366 RVA: 0x003487AC File Offset: 0x003469AC
		public List<BodyPart> GetPart(string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetPart(RequiredType);
			}
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount(RequiredType, RequiredLaterality));
			this._Body.GetPart(RequiredType, RequiredLaterality, list);
			return list;
		}

		// Token: 0x060091F7 RID: 37367 RVA: 0x003487E8 File Offset: 0x003469E8
		public List<BodyPart> GetPart(string RequiredType, bool EvenIfDismembered)
		{
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount(RequiredType));
			this._Body.GetPart(RequiredType, list);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Type == RequiredType || dismemberedPart.Part.VariantType == RequiredType)
					{
						list.Add(dismemberedPart.Part);
					}
				}
			}
			return list;
		}

		// Token: 0x060091F8 RID: 37368 RVA: 0x00348890 File Offset: 0x00346A90
		public List<BodyPart> GetPart(string RequiredType, int RequiredLaterality, bool EvenIfDismembered)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetPart(RequiredType, EvenIfDismembered);
			}
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount(RequiredType, RequiredLaterality));
			this._Body.GetPart(RequiredType, RequiredLaterality, list);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if ((dismemberedPart.Part.Type == RequiredType || dismemberedPart.Part.VariantType == RequiredType) && Laterality.Match(dismemberedPart.Part, RequiredLaterality))
					{
						list.Add(dismemberedPart.Part);
					}
				}
			}
			return list;
		}

		// Token: 0x060091F9 RID: 37369 RVA: 0x00348958 File Offset: 0x00346B58
		public IEnumerable<BodyPart> LoopPart(string RequiredType)
		{
			foreach (BodyPart bodyPart in this._Body.LoopPart(RequiredType))
			{
				yield return bodyPart;
			}
			IEnumerator<BodyPart> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060091FA RID: 37370 RVA: 0x0034896F File Offset: 0x00346B6F
		public IEnumerable<BodyPart> LoopPart(string RequiredType, int RequiredLaterality)
		{
			foreach (BodyPart bodyPart in this._Body.LoopPart(RequiredType, RequiredLaterality))
			{
				yield return bodyPart;
			}
			IEnumerator<BodyPart> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060091FB RID: 37371 RVA: 0x0034898D File Offset: 0x00346B8D
		public BodyPart GetFirstPart()
		{
			return this._Body;
		}

		// Token: 0x060091FC RID: 37372 RVA: 0x00348995 File Offset: 0x00346B95
		public BodyPart GetFirstPart(string RequiredType)
		{
			return this._Body.GetFirstPart(RequiredType);
		}

		// Token: 0x060091FD RID: 37373 RVA: 0x003489A3 File Offset: 0x00346BA3
		public BodyPart GetFirstPart(string RequiredType, int RequiredLaterality)
		{
			return this._Body.GetFirstPart(RequiredType, RequiredLaterality);
		}

		// Token: 0x060091FE RID: 37374 RVA: 0x003489B2 File Offset: 0x00346BB2
		public BodyPart GetFirstPart(Predicate<BodyPart> Filter)
		{
			return this._Body.GetFirstPart(Filter);
		}

		// Token: 0x060091FF RID: 37375 RVA: 0x003489C0 File Offset: 0x00346BC0
		public BodyPart GetFirstPart(string RequiredType, Predicate<BodyPart> Filter)
		{
			return this._Body.GetFirstPart(RequiredType, Filter);
		}

		// Token: 0x06009200 RID: 37376 RVA: 0x003489CF File Offset: 0x00346BCF
		public BodyPart GetFirstPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter)
		{
			return this._Body.GetFirstPart(RequiredType, RequiredLaterality, Filter);
		}

		// Token: 0x06009201 RID: 37377 RVA: 0x003489E0 File Offset: 0x00346BE0
		public BodyPart GetFirstPart(string RequiredType, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstPart(RequiredType);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Type == RequiredType && dismemberedPart.Part.Position < num)
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009202 RID: 37378 RVA: 0x00348A80 File Offset: 0x00346C80
		public BodyPart GetFirstPart(string RequiredType, int RequiredLaterality, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstPart(RequiredType, RequiredLaterality);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Type == RequiredType && dismemberedPart.Part.Position < num && Laterality.Match(dismemberedPart.Part, RequiredLaterality))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009203 RID: 37379 RVA: 0x00348B30 File Offset: 0x00346D30
		public BodyPart GetFirstPart(Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstPart(Filter);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Position < num && Filter(dismemberedPart.Part))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009204 RID: 37380 RVA: 0x00348BCC File Offset: 0x00346DCC
		public BodyPart GetFirstPart(string RequiredType, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstPart(RequiredType, Filter);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Type == RequiredType && dismemberedPart.Part.Position < num && Filter(dismemberedPart.Part))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009205 RID: 37381 RVA: 0x00348C7C File Offset: 0x00346E7C
		public BodyPart GetFirstPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstPart(RequiredType, RequiredLaterality, Filter);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Type == RequiredType && dismemberedPart.Part.Position < num && Laterality.Match(dismemberedPart.Part, RequiredLaterality) && Filter(dismemberedPart.Part))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009206 RID: 37382 RVA: 0x00348D40 File Offset: 0x00346F40
		public BodyPart GetFirstVariantPart(string RequiredType)
		{
			return this._Body.GetFirstVariantPart(RequiredType);
		}

		// Token: 0x06009207 RID: 37383 RVA: 0x00348D4E File Offset: 0x00346F4E
		public BodyPart GetFirstVariantPart(string RequiredType, int RequiredLaterality)
		{
			return this._Body.GetFirstVariantPart(RequiredType, RequiredLaterality);
		}

		// Token: 0x06009208 RID: 37384 RVA: 0x00348D5D File Offset: 0x00346F5D
		public BodyPart GetFirstVariantPart(Predicate<BodyPart> Filter)
		{
			return this._Body.GetFirstVariantPart(Filter);
		}

		// Token: 0x06009209 RID: 37385 RVA: 0x00348D6B File Offset: 0x00346F6B
		public BodyPart GetFirstVariantPart(string RequiredType, Predicate<BodyPart> Filter)
		{
			return this._Body.GetFirstVariantPart(RequiredType, Filter);
		}

		// Token: 0x0600920A RID: 37386 RVA: 0x00348D7A File Offset: 0x00346F7A
		public BodyPart GetFirstVariantPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter)
		{
			return this._Body.GetFirstVariantPart(RequiredType, RequiredLaterality, Filter);
		}

		// Token: 0x0600920B RID: 37387 RVA: 0x00348D8C File Offset: 0x00346F8C
		public BodyPart GetFirstVariantPart(string RequiredType, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstVariantPart(RequiredType);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.VariantType == RequiredType && dismemberedPart.Part.Position < num)
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x0600920C RID: 37388 RVA: 0x00348E2C File Offset: 0x0034702C
		public BodyPart GetFirstVariantPart(string RequiredType, int RequiredLaterality, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstVariantPart(RequiredType, RequiredLaterality);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.VariantType == RequiredType && dismemberedPart.Part.Position < num && Laterality.Match(dismemberedPart.Part, RequiredLaterality))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x0600920D RID: 37389 RVA: 0x00348EDC File Offset: 0x003470DC
		public BodyPart GetFirstVariantPart(Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstVariantPart(Filter);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Position < num && Filter(dismemberedPart.Part))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x0600920E RID: 37390 RVA: 0x00348F78 File Offset: 0x00347178
		public BodyPart GetFirstVariantPart(string RequiredType, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstVariantPart(RequiredType, Filter);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.VariantType == RequiredType && dismemberedPart.Part.Position < num && Filter(dismemberedPart.Part))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x0600920F RID: 37391 RVA: 0x00349028 File Offset: 0x00347228
		public BodyPart GetFirstVariantPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetFirstVariantPart(RequiredType, RequiredLaterality, Filter);
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.VariantType == RequiredType && dismemberedPart.Part.Position < num && Laterality.Match(dismemberedPart.Part, RequiredLaterality) && Filter(dismemberedPart.Part))
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009210 RID: 37392 RVA: 0x003490EC File Offset: 0x003472EC
		public bool HasPart(string RequiredType)
		{
			return this.GetFirstPart(RequiredType) != null;
		}

		// Token: 0x06009211 RID: 37393 RVA: 0x003490F8 File Offset: 0x003472F8
		public bool HasPart(string RequiredType, int RequiredLaterality)
		{
			return this.GetFirstPart(RequiredType, RequiredLaterality) != null;
		}

		// Token: 0x06009212 RID: 37394 RVA: 0x00349105 File Offset: 0x00347305
		public bool HasPart(Predicate<BodyPart> Filter)
		{
			return this.GetFirstPart(Filter) != null;
		}

		// Token: 0x06009213 RID: 37395 RVA: 0x00349111 File Offset: 0x00347311
		public bool HasPart(string RequiredType, Predicate<BodyPart> Filter)
		{
			return this.GetFirstPart(RequiredType, Filter) != null;
		}

		// Token: 0x06009214 RID: 37396 RVA: 0x0034911E File Offset: 0x0034731E
		public bool HasPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter)
		{
			return this.GetFirstPart(RequiredType, RequiredLaterality, Filter) != null;
		}

		// Token: 0x06009215 RID: 37397 RVA: 0x0034912C File Offset: 0x0034732C
		public bool HasPart(string RequiredType, bool EvenIfDismembered)
		{
			return this.GetFirstPart(RequiredType, EvenIfDismembered) != null;
		}

		// Token: 0x06009216 RID: 37398 RVA: 0x00349139 File Offset: 0x00347339
		public bool HasPart(string RequiredType, int RequiredLaterality, bool EvenIfDismembered)
		{
			return this.GetFirstPart(RequiredType, RequiredLaterality, EvenIfDismembered) != null;
		}

		// Token: 0x06009217 RID: 37399 RVA: 0x00349147 File Offset: 0x00347347
		public bool HasPart(Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			return this.GetFirstPart(Filter, EvenIfDismembered) != null;
		}

		// Token: 0x06009218 RID: 37400 RVA: 0x00349154 File Offset: 0x00347354
		public bool HasPart(string RequiredType, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			return this.GetFirstPart(RequiredType, Filter, EvenIfDismembered) != null;
		}

		// Token: 0x06009219 RID: 37401 RVA: 0x00349162 File Offset: 0x00347362
		public bool HasPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			return this.GetFirstPart(RequiredType, RequiredLaterality, Filter, EvenIfDismembered) != null;
		}

		// Token: 0x0600921A RID: 37402 RVA: 0x00349172 File Offset: 0x00347372
		public bool HasVariantPart(string RequiredType)
		{
			return this.GetFirstVariantPart(RequiredType) != null;
		}

		// Token: 0x0600921B RID: 37403 RVA: 0x0034917E File Offset: 0x0034737E
		public bool HasVariantPart(string RequiredType, int RequiredLaterality)
		{
			return this.GetFirstVariantPart(RequiredType, RequiredLaterality) != null;
		}

		// Token: 0x0600921C RID: 37404 RVA: 0x0034918B File Offset: 0x0034738B
		public bool HasVariantPart(Predicate<BodyPart> Filter)
		{
			return this.GetFirstVariantPart(Filter) != null;
		}

		// Token: 0x0600921D RID: 37405 RVA: 0x00349197 File Offset: 0x00347397
		public bool HasVariantPart(string RequiredType, Predicate<BodyPart> Filter)
		{
			return this.GetFirstVariantPart(RequiredType, Filter) != null;
		}

		// Token: 0x0600921E RID: 37406 RVA: 0x003491A4 File Offset: 0x003473A4
		public bool HasVariantPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter)
		{
			return this.GetFirstVariantPart(RequiredType, RequiredLaterality, Filter) != null;
		}

		// Token: 0x0600921F RID: 37407 RVA: 0x003491B2 File Offset: 0x003473B2
		public bool HasVariantPart(string RequiredType, bool EvenIfDismembered)
		{
			return this.GetFirstVariantPart(RequiredType, EvenIfDismembered) != null;
		}

		// Token: 0x06009220 RID: 37408 RVA: 0x003491BF File Offset: 0x003473BF
		public bool HasVariantPart(string RequiredType, int RequiredLaterality, bool EvenIfDismembered)
		{
			return this.GetFirstVariantPart(RequiredType, RequiredLaterality, EvenIfDismembered) != null;
		}

		// Token: 0x06009221 RID: 37409 RVA: 0x003491CD File Offset: 0x003473CD
		public bool HasVariantPart(Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			return this.GetFirstVariantPart(Filter, EvenIfDismembered) != null;
		}

		// Token: 0x06009222 RID: 37410 RVA: 0x003491DA File Offset: 0x003473DA
		public bool HasVariantPart(string RequiredType, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			return this.GetFirstVariantPart(RequiredType, Filter, EvenIfDismembered) != null;
		}

		// Token: 0x06009223 RID: 37411 RVA: 0x003491E8 File Offset: 0x003473E8
		public bool HasVariantPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter, bool EvenIfDismembered)
		{
			return this.GetFirstVariantPart(RequiredType, RequiredLaterality, Filter, EvenIfDismembered) != null;
		}

		// Token: 0x06009224 RID: 37412 RVA: 0x003491F8 File Offset: 0x003473F8
		public BodyPart GetPartByName(string RequiredPart)
		{
			return this._Body.GetPartByName(RequiredPart);
		}

		// Token: 0x06009225 RID: 37413 RVA: 0x00349208 File Offset: 0x00347408
		public BodyPart GetPartByName(string RequiredPart, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this._Body.GetPartByName(RequiredPart);
			if (bodyPart == null && EvenIfDismembered && this.DismemberedParts != null)
			{
				int num = int.MaxValue;
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Type == RequiredPart && dismemberedPart.Part.Position < num)
					{
						bodyPart = dismemberedPart.Part;
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009226 RID: 37414 RVA: 0x003492AC File Offset: 0x003474AC
		public BodyPart GetPartByNameStartsWith(string RequiredPart)
		{
			return this._Body.GetPartByNameStartsWith(RequiredPart);
		}

		// Token: 0x06009227 RID: 37415 RVA: 0x003492BA File Offset: 0x003474BA
		public BodyPart GetPartByNameWithoutCybernetics(string RequiredPart)
		{
			return this._Body.GetPartByNameWithoutCybernetics(RequiredPart);
		}

		// Token: 0x06009228 RID: 37416 RVA: 0x003492C8 File Offset: 0x003474C8
		public BodyPart GetPartByDescription(string RequiredPart)
		{
			return this._Body.GetPartByDescription(RequiredPart);
		}

		// Token: 0x06009229 RID: 37417 RVA: 0x003492D6 File Offset: 0x003474D6
		public BodyPart GetPartByDescriptionStartsWith(string RequiredPart)
		{
			return this._Body.GetPartByDescriptionStartsWith(RequiredPart);
		}

		// Token: 0x0600922A RID: 37418 RVA: 0x003492E4 File Offset: 0x003474E4
		public bool RemoveUnmanagedPartsByVariantPrefix(string Prefix)
		{
			if (this._Body.RemoveUnmanagedPartsByVariantPrefix(Prefix))
			{
				this.UpdateBodyParts(0);
				this.RecalculateArmor();
				return true;
			}
			return false;
		}

		// Token: 0x0600922B RID: 37419 RVA: 0x00349304 File Offset: 0x00347504
		public bool RemovePart(BodyPart removePart, bool DoUpdate = true)
		{
			bool flag = false;
			if (this._Body.RemovePart(removePart, DoUpdate))
			{
				flag = true;
			}
			if (this.DismemberedParts != null)
			{
				bool flag2 = false;
				using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Part == removePart)
						{
							flag2 = true;
							flag = true;
							break;
						}
					}
				}
				if (flag2)
				{
					foreach (Body.DismemberedPart dismemberedPart in new List<Body.DismemberedPart>(this.DismemberedParts))
					{
						if (dismemberedPart.Part == removePart)
						{
							this.DismemberedParts.Remove(dismemberedPart);
						}
						else if (dismemberedPart.ParentID == removePart.ID)
						{
							this.RemovePart(dismemberedPart.Part, false);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600922C RID: 37420 RVA: 0x003493F8 File Offset: 0x003475F8
		public bool RemovePartByID(int removeID)
		{
			bool flag = false;
			if (this._Body.RemovePartByID(removeID, true))
			{
				return true;
			}
			if (this.DismemberedParts != null)
			{
				bool flag2 = false;
				using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Part.IDMatch(removeID))
						{
							flag2 = true;
							flag = true;
							break;
						}
					}
				}
				if (flag2)
				{
					foreach (Body.DismemberedPart dismemberedPart in new List<Body.DismemberedPart>(this.DismemberedParts))
					{
						if (dismemberedPart.Part.IDMatch(removeID))
						{
							this.DismemberedParts.Remove(dismemberedPart);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600922D RID: 37421 RVA: 0x003494D8 File Offset: 0x003476D8
		public BodyPart GetPartByID(int findID, bool EvenIfDismembered = false)
		{
			BodyPart partByID = this._Body.GetPartByID(findID);
			if (partByID != null)
			{
				return partByID;
			}
			if (EvenIfDismembered && this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.IDMatch(findID))
					{
						return dismemberedPart.Part;
					}
				}
			}
			return null;
		}

		// Token: 0x0600922E RID: 37422 RVA: 0x0034955C File Offset: 0x0034775C
		public List<BodyPart> GetPartsBySupportsDependent(string findSupportsDependent)
		{
			this._Body.GetPartBySupportsDependent(findSupportsDependent, Body.parts2);
			return Body.parts2;
		}

		// Token: 0x0600922F RID: 37423 RVA: 0x00349574 File Offset: 0x00347774
		public BodyPart GetPartBySupportsDependent(string findSupportsDependent)
		{
			return this._Body.GetPartBySupportsDependent(findSupportsDependent);
		}

		// Token: 0x06009230 RID: 37424 RVA: 0x00349582 File Offset: 0x00347782
		public bool CheckSlotEquippedMatch(GameObject obj, string SlotSpec)
		{
			if (SlotSpec.IndexOf(',') != -1)
			{
				return this._Body.CheckSlotEquippedMatch(obj, SlotSpec.CachedCommaExpansion());
			}
			return this._Body.CheckSlotEquippedMatch(obj, SlotSpec);
		}

		// Token: 0x06009231 RID: 37425 RVA: 0x003495AF File Offset: 0x003477AF
		public bool CheckSlotCyberneticsMatch(GameObject obj, string SlotSpec)
		{
			if (SlotSpec.IndexOf(',') != -1)
			{
				return this._Body.CheckSlotCyberneticsMatch(obj, SlotSpec.CachedCommaExpansion());
			}
			return this._Body.CheckSlotCyberneticsMatch(obj, SlotSpec);
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x003495DC File Offset: 0x003477DC
		public bool CheckSlotDefaultBehaviorMatch(GameObject obj, string SlotSpec)
		{
			if (SlotSpec.IndexOf(',') != -1)
			{
				return this._Body.CheckSlotDefaultBehaviorMatch(obj, SlotSpec.CachedCommaExpansion());
			}
			return this._Body.CheckSlotDefaultBehaviorMatch(obj, SlotSpec);
		}

		// Token: 0x06009233 RID: 37427 RVA: 0x00349609 File Offset: 0x00347809
		public bool HasReadyMissileWeapon(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			return this._Body.HasReadyMissileWeapon(Filter, PartFilter);
		}

		// Token: 0x06009234 RID: 37428 RVA: 0x00349618 File Offset: 0x00347818
		public bool HasMissileWeapon(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			return this._Body.HasMissileWeapon(Filter, PartFilter);
		}

		// Token: 0x06009235 RID: 37429 RVA: 0x00349627 File Offset: 0x00347827
		public bool HasHeavyWeaponEquipped()
		{
			return this._Body.HasHeavyWeaponEquipped();
		}

		// Token: 0x06009236 RID: 37430 RVA: 0x00349634 File Offset: 0x00347834
		public void GetMissileWeapons(List<GameObject> List, Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			this._Body.GetMissileWeapons(ref List, Filter, PartFilter);
		}

		// Token: 0x06009237 RID: 37431 RVA: 0x00349648 File Offset: 0x00347848
		public List<GameObject> GetMissileWeapons(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			List<GameObject> list = null;
			this._Body.GetMissileWeapons(ref list, Filter, PartFilter);
			return list;
		}

		// Token: 0x06009238 RID: 37432 RVA: 0x00349667 File Offset: 0x00347867
		public GameObject GetFirstThrownWeapon(Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			return this._Body.GetFirstThrownWeapon(Filter, PartFilter);
		}

		// Token: 0x06009239 RID: 37433 RVA: 0x00349676 File Offset: 0x00347876
		public void GetThrownWeapons(IList<GameObject> List, Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			this._Body.GetThrownWeapons(ref List, Filter, PartFilter);
		}

		// Token: 0x0600923A RID: 37434 RVA: 0x00349688 File Offset: 0x00347888
		public IList<GameObject> GetThrownWeapons(Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			IList<GameObject> list = null;
			this._Body.GetThrownWeapons(ref list, Filter, PartFilter);
			return list;
		}

		// Token: 0x0600923B RID: 37435 RVA: 0x003496A7 File Offset: 0x003478A7
		public int GetDismemberedPartCount()
		{
			if (this.DismemberedParts == null)
			{
				return 0;
			}
			return this.DismemberedParts.Count;
		}

		// Token: 0x0600923C RID: 37436 RVA: 0x003496C0 File Offset: 0x003478C0
		public BodyPart GetDismemberedPartByID(int ID)
		{
			if (this.DismemberedParts == null)
			{
				return null;
			}
			foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
			{
				if (dismemberedPart.Part.IDMatch(ID))
				{
					return dismemberedPart.Part;
				}
			}
			return null;
		}

		// Token: 0x0600923D RID: 37437 RVA: 0x00349730 File Offset: 0x00347930
		public BodyPart GetDismemberedPartByType(string RequiredType)
		{
			if (this.DismemberedParts == null)
			{
				return null;
			}
			foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
			{
				if (dismemberedPart.Part.Type == RequiredType || dismemberedPart.Part.VariantType == RequiredType)
				{
					return dismemberedPart.Part;
				}
			}
			return null;
		}

		// Token: 0x0600923E RID: 37438 RVA: 0x003497B8 File Offset: 0x003479B8
		public BodyPart GetDismemberedPartBySupportsDependent(string RequiredSupportsDependent)
		{
			if (this.DismemberedParts == null)
			{
				return null;
			}
			foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
			{
				if (dismemberedPart.Part.SupportsDependent == RequiredSupportsDependent)
				{
					return dismemberedPart.Part;
				}
			}
			return null;
		}

		// Token: 0x0600923F RID: 37439 RVA: 0x00349830 File Offset: 0x00347A30
		public BodyPart GetDismemberedPartByDependsOn(string RequiredDependsOn)
		{
			if (this.DismemberedParts == null)
			{
				return null;
			}
			foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
			{
				if (dismemberedPart.Part.DependsOn == RequiredDependsOn)
				{
					return dismemberedPart.Part;
				}
			}
			return null;
		}

		// Token: 0x06009240 RID: 37440 RVA: 0x003498A8 File Offset: 0x00347AA8
		public bool ValidateDismemberedParentPart(BodyPart Parent, BodyPart Child)
		{
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part == Child && Parent.IDMatch(dismemberedPart.ParentID))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06009241 RID: 37441 RVA: 0x0034991C File Offset: 0x00347B1C
		public BodyPart GetFirstDismemberedPartByType(BodyPart Parent, string RequiredType)
		{
			BodyPart bodyPart = null;
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (Parent.IDMatch(dismemberedPart.ParentID) && dismemberedPart.Part.Type == RequiredType && (bodyPart == null || dismemberedPart.Part.Position < bodyPart.Position))
					{
						bodyPart = dismemberedPart.Part;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009242 RID: 37442 RVA: 0x003499B4 File Offset: 0x00347BB4
		public BodyPart GetFirstDismemberedPartByTypeAndLaterality(BodyPart Parent, string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetFirstDismemberedPartByType(Parent, RequiredType);
			}
			BodyPart bodyPart = null;
			if (this.DismemberedParts != null)
			{
				if (RequiredLaterality == 0)
				{
					using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Body.DismemberedPart dismemberedPart = enumerator.Current;
							if (Parent.IDMatch(dismemberedPart.ParentID) && dismemberedPart.Part.Type == RequiredType && dismemberedPart.Part.Laterality == 0 && (bodyPart == null || dismemberedPart.Part.Position < bodyPart.Position))
							{
								bodyPart = dismemberedPart.Part;
							}
						}
						return bodyPart;
					}
				}
				foreach (Body.DismemberedPart dismemberedPart2 in this.DismemberedParts)
				{
					if (Parent.IDMatch(dismemberedPart2.ParentID) && dismemberedPart2.Part.Type == RequiredType && (dismemberedPart2.Part.Laterality & RequiredLaterality) == RequiredLaterality && (bodyPart == null || dismemberedPart2.Part.Position < bodyPart.Position))
					{
						bodyPart = dismemberedPart2.Part;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06009243 RID: 37443 RVA: 0x00349AFC File Offset: 0x00347CFC
		public bool IsDismembered(BodyPart checkPart)
		{
			if (this.DismemberedParts == null)
			{
				return false;
			}
			using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Part == checkPart)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06009244 RID: 37444 RVA: 0x00349B60 File Offset: 0x00347D60
		public bool HasPartOrDismemberedPart(string partName)
		{
			return this.GetPartByName(partName) != null || this.HasDismemberedPartNamed(partName);
		}

		// Token: 0x06009245 RID: 37445 RVA: 0x00349B7C File Offset: 0x00347D7C
		public bool HasDismemberedPartNamed(string RequiredPart)
		{
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.Name == RequiredPart || dismemberedPart.Part.Description == RequiredPart)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06009246 RID: 37446 RVA: 0x00349C00 File Offset: 0x00347E00
		public bool AnyDismemberedMortalParts()
		{
			if (this.DismemberedParts != null)
			{
				using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Part.Mortal)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06009247 RID: 37447 RVA: 0x00349C68 File Offset: 0x00347E68
		public int GetFirstDismemberedPartPosition(BodyPart Parent, BodyPart ExceptPart = null)
		{
			int num = -1;
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (Parent.IDMatch(dismemberedPart.ParentID) && dismemberedPart.Part != ExceptPart && (num == -1 || dismemberedPart.Part.Position < num))
					{
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return num;
		}

		// Token: 0x06009248 RID: 37448 RVA: 0x00349CF4 File Offset: 0x00347EF4
		public int GetLastDismemberedPartPosition(BodyPart Parent, BodyPart ExceptPart = null)
		{
			int num = -1;
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (Parent.IDMatch(dismemberedPart.ParentID) && dismemberedPart.Part != ExceptPart && (num == -1 || dismemberedPart.Part.Position > num))
					{
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return num;
		}

		// Token: 0x06009249 RID: 37449 RVA: 0x00349D80 File Offset: 0x00347F80
		public int GetFirstDismemberedPartTypePosition(BodyPart Parent, string Type, BodyPart ExceptPart = null)
		{
			int num = -1;
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (Parent.IDMatch(dismemberedPart.ParentID) && dismemberedPart.Part.Type == Type && dismemberedPart.Part != ExceptPart && (num == -1 || dismemberedPart.Part.Position < num))
					{
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return num;
		}

		// Token: 0x0600924A RID: 37450 RVA: 0x00349E20 File Offset: 0x00348020
		public int GetLastDismemberedPartTypePosition(BodyPart Parent, string Type, BodyPart ExceptPart = null)
		{
			int num = -1;
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (Parent.IDMatch(dismemberedPart.ParentID) && dismemberedPart.Part.Type == Type && dismemberedPart.Part != ExceptPart && dismemberedPart.Part.Position > num)
					{
						num = dismemberedPart.Part.Position;
					}
				}
			}
			return num;
		}

		// Token: 0x0600924B RID: 37451 RVA: 0x00349EBC File Offset: 0x003480BC
		public Body.DismemberedPart FindRegenerablePart(int? ParentID = null, int? Category = null, int[] Categories = null, int? ExceptCategory = null, int[] ExceptCategories = null)
		{
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					if (dismemberedPart.Part.IsRegenerable())
					{
						if (ParentID != null)
						{
							int parentID = dismemberedPart.ParentID;
							int? num = ParentID;
							if (!((parentID == num.GetValueOrDefault()) & (num != null)))
							{
								continue;
							}
						}
						if (Category != null)
						{
							int category = dismemberedPart.Part.Category;
							int? num = Category;
							if (!((category == num.GetValueOrDefault()) & (num != null)))
							{
								continue;
							}
						}
						if (Categories == null || Array.IndexOf<int>(Categories, dismemberedPart.Part.Category) != -1)
						{
							if (ExceptCategory != null)
							{
								int category2 = dismemberedPart.Part.Category;
								int? num = ExceptCategory;
								if ((category2 == num.GetValueOrDefault()) & (num != null))
								{
									continue;
								}
							}
							if ((ExceptCategories == null || Array.IndexOf<int>(ExceptCategories, dismemberedPart.Part.Category) == -1) && dismemberedPart.IsReattachable(this))
							{
								return dismemberedPart;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600924C RID: 37452 RVA: 0x00349FE4 File Offset: 0x003481E4
		public Body.DismemberedPart FindRegenerablePart(ILimbRegenerationEvent E)
		{
			return this.FindRegenerablePart(E.ParentID, E.Category, E.Categories, E.ExceptCategory, E.ExceptCategories);
		}

		// Token: 0x0600924D RID: 37453 RVA: 0x0034A00C File Offset: 0x0034820C
		public List<Body.DismemberedPart> FindRecoverableParts()
		{
			if (this.DismemberedParts == null)
			{
				return null;
			}
			List<Body.DismemberedPart> list = null;
			foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
			{
				if (dismemberedPart.Part.IsRecoverable(this) && dismemberedPart.IsReattachable(this))
				{
					if (list == null)
					{
						list = new List<Body.DismemberedPart>(1) { dismemberedPart };
					}
					else
					{
						list.Add(dismemberedPart);
					}
				}
			}
			return list;
		}

		// Token: 0x0600924E RID: 37454 RVA: 0x0034A098 File Offset: 0x00348298
		public void CheckDismemberedPartRenumberingOnPositionAssignment(BodyPart Parent, int AssignedPosition, BodyPart ExceptPart = null)
		{
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					dismemberedPart.CheckRenumberingOnPositionAssignment(Parent, AssignedPosition, ExceptPart);
				}
			}
		}

		// Token: 0x0600924F RID: 37455 RVA: 0x0034A0F4 File Offset: 0x003482F4
		public bool DismemberedPartHasPosition(BodyPart Parent, int Position, BodyPart ExceptPart = null)
		{
			if (this.DismemberedParts != null)
			{
				using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.HasPosition(Parent, Position, ExceptPart))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06009250 RID: 37456 RVA: 0x0034A158 File Offset: 0x00348358
		public void GetTypeArmorInfo(string ForType, ref GameObject First, ref int Count, ref int AV, ref int DV)
		{
			this._Body.GetTypeArmorInfo(ForType, ref First, ref Count, ref AV, ref DV);
		}

		// Token: 0x06009251 RID: 37457 RVA: 0x0034A16C File Offset: 0x0034836C
		public void RecalculateArmor()
		{
			if (!this.built)
			{
				return;
			}
			this._Body.RecalculateArmor();
		}

		// Token: 0x06009252 RID: 37458 RVA: 0x0034A182 File Offset: 0x00348382
		public void RecalculateArmorExcept(GameObject obj)
		{
			if (!this.built)
			{
				return;
			}
			this._Body.RecalculateArmorExcept(obj);
		}

		// Token: 0x06009253 RID: 37459 RVA: 0x0034A199 File Offset: 0x00348399
		public void RecalculateTypeArmor(string ForType)
		{
			if (!this.built)
			{
				return;
			}
			this._Body.RecalculateTypeArmor(ForType);
		}

		// Token: 0x06009254 RID: 37460 RVA: 0x0034A1B0 File Offset: 0x003483B0
		public void RecalculateTypeArmorExcept(string ForType, GameObject obj)
		{
			if (!this.built)
			{
				return;
			}
			this._Body.RecalculateTypeArmorExcept(ForType, obj);
		}

		// Token: 0x06009255 RID: 37461 RVA: 0x0034A1C8 File Offset: 0x003483C8
		public void RecalculateFirsts()
		{
			if (this.built)
			{
				bool flag = false;
				bool flag2 = false;
				BodyPart bodyPart = null;
				this.ForeachPart(delegate(BodyPart x)
				{
					x.Primary = false;
					x.DefaultPrimary = false;
					return true;
				});
				this._Body.SetPrimaryScan(this.GetPrimaryLimbType(), ref flag, ref flag2, ref bodyPart);
				if (bodyPart == null)
				{
					bodyPart = this._Body;
					this._Body.DefaultPrimary = true;
					this._Body.Primary = true;
				}
				if (!flag2 && bodyPart != null)
				{
					bodyPart.PreferredPrimary = true;
				}
				this.ParentObject.FireEvent("PrimaryLimbRecalculated");
			}
		}

		// Token: 0x06009256 RID: 37462 RVA: 0x0034A264 File Offset: 0x00348464
		public void RecalculateFirstEquipped(GameObject Object)
		{
			bool flag = false;
			this._Body.RecalculateFirstEquipped(Object, ref flag);
		}

		// Token: 0x06009257 RID: 37463 RVA: 0x0034A284 File Offset: 0x00348484
		public void RecalculateFirstCybernetics(GameObject Object)
		{
			bool flag = false;
			this._Body.RecalculateFirstCybernetics(Object, ref flag);
		}

		// Token: 0x06009258 RID: 37464 RVA: 0x0034A2A4 File Offset: 0x003484A4
		public void RecalculateFirstDefaultBehavior(GameObject Object)
		{
			bool flag = false;
			this._Body.RecalculateFirstDefaultBehavior(Object, ref flag);
		}

		// Token: 0x06009259 RID: 37465 RVA: 0x0034A2C4 File Offset: 0x003484C4
		public void CheckUnsupportedPartLoss()
		{
			List<BodyPart> list = this._Body.FindUnsupportedParts();
			if (list == null)
			{
				return;
			}
			foreach (BodyPart bodyPart in list)
			{
				if (this.ParentObject.IsPlayer())
				{
					string ordinalName = bodyPart.GetOrdinalName();
					this.CutAndQueueForRegeneration(bodyPart);
					IComponent<GameObject>.AddPlayerMessage("You have lost the use of your " + ordinalName + ".", 'R', true);
				}
				else
				{
					this.CutAndQueueForRegeneration(bodyPart);
				}
			}
		}

		// Token: 0x0600925A RID: 37466 RVA: 0x0034A358 File Offset: 0x00348558
		public void CheckPartRecovery()
		{
			List<Body.DismemberedPart> list = this.FindRecoverableParts();
			if (list == null)
			{
				return;
			}
			foreach (Body.DismemberedPart dismemberedPart in list)
			{
				dismemberedPart.Reattach(this);
				if (this.ParentObject.IsPlayer() && dismemberedPart.Part.IsAbstractlyDependent())
				{
					IComponent<GameObject>.AddPlayerMessage("You have recovered the use of your " + dismemberedPart.Part.GetOrdinalName() + ".", 'G', true);
				}
			}
		}

		// Token: 0x0600925B RID: 37467 RVA: 0x0034A3F0 File Offset: 0x003485F0
		public void RegenerateDefaultEquipment()
		{
			List<BodyPart> list;
			if (Body._bodyPartsInUse)
			{
				list = new List<BodyPart>(this.GetPartCount());
			}
			else
			{
				list = Body._bodyParts;
				Body._bodyPartsInUse = true;
			}
			this.GetParts(list);
			try
			{
				foreach (BodyPart bodyPart in list)
				{
					GameObject gameObject = bodyPart.DefaultBehavior;
					if (gameObject != null && ((bodyPart.DefaultBehaviorBlueprint != null && gameObject.Blueprint == bodyPart.DefaultBehaviorBlueprint) || gameObject.HasTagOrProperty("TemporaryDefaultBehavior")))
					{
						gameObject.Obliterate(null, false, null);
						gameObject = (bodyPart._DefaultBehavior = null);
					}
					if (gameObject == null && bodyPart.DefaultBehaviorBlueprint != null)
					{
						bodyPart.DefaultBehavior = GameObject.CreateUnmodified(bodyPart.DefaultBehaviorBlueprint);
						bodyPart.FirstSlotForDefaultBehavior = true;
					}
				}
				RegenerateDefaultEquipmentEvent.Send(this.ParentObject, this);
				DecorateDefaultEquipmentEvent.Send(this.ParentObject, this);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("RegenerateDefaultEquipment", ex, "game_exception");
			}
			finally
			{
				if (list == Body._bodyParts)
				{
					Body._bodyParts.Clear();
					Body._bodyPartsInUse = false;
				}
			}
		}

		// Token: 0x0600925C RID: 37468 RVA: 0x0034A528 File Offset: 0x00348728
		public void UpdateBodyParts(int Depth = 0)
		{
			if (!this.built)
			{
				return;
			}
			this.RegenerateDefaultEquipment();
			this.RecalculateFirsts();
			this.UpdateMobilitySpeedPenalty();
			this.CheckUnsupportedPartLoss();
			this.CheckPartRecovery();
			this.FireEventOnBodyparts(Body.eBodypartsUpdated);
		}

		// Token: 0x0600925D RID: 37469 RVA: 0x0034A560 File Offset: 0x00348760
		public void CutAndQueueForRegeneration(BodyPart Part)
		{
			BodyPart parentPart = Part.GetParentPart();
			Body.DismemberedPart dismemberedPart = (Part.Extrinsic ? null : new Body.DismemberedPart(Part, parentPart));
			if (Part.Parts != null && Part.Parts.Count > 0)
			{
				if (Part.Parts.Count > 1)
				{
					using (List<BodyPart>.Enumerator enumerator = new List<BodyPart>(Part.Parts).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							BodyPart bodyPart = enumerator.Current;
							this.CutAndQueueForRegeneration(bodyPart);
						}
						goto IL_008B;
					}
				}
				this.CutAndQueueForRegeneration(Part.Parts[0]);
				IL_008B:
				Part.Parts = null;
			}
			if (parentPart != null)
			{
				parentPart.RemovePart(Part, false);
			}
			Body parentBody = Part.ParentBody;
			if (parentBody != null)
			{
				parentBody.RecalculateTypeArmor(Part.Type);
			}
			if (dismemberedPart != null)
			{
				if (this.DismemberedParts == null)
				{
					this.DismemberedParts = new List<Body.DismemberedPart>(1) { dismemberedPart };
				}
				else
				{
					this.DismemberedParts.Add(dismemberedPart);
				}
			}
			Part.ParentBody = null;
			Part.Primary = false;
			Part.PreferredPrimary = false;
			Part.DefaultPrimary = false;
		}

		/// Determines the speed penalty we should have based on our current
		/// intact and dismembered mobility-providing limbs.
		// Token: 0x0600925E RID: 37470 RVA: 0x0034A67C File Offset: 0x0034887C
		public int CalculateMobilitySpeedPenalty(out bool AnyDismembered)
		{
			AnyDismembered = false;
			if (this.ParentObject.IsFlying)
			{
				return 0;
			}
			int totalMobility = this.GetTotalMobility();
			if (totalMobility == 0)
			{
				if (this.DismemberedParts != null)
				{
					using (List<Body.DismemberedPart>.Enumerator enumerator = this.DismemberedParts.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Part.GetTotalMobility() > 0)
							{
								AnyDismembered = true;
								break;
							}
						}
					}
				}
				return 60;
			}
			int num = 0;
			if (this.DismemberedParts != null)
			{
				foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
				{
					num += dismemberedPart.Part.GetTotalMobility();
				}
			}
			if (num > 0)
			{
				AnyDismembered = true;
			}
			if (totalMobility >= 2)
			{
				if (num == 0)
				{
					return 0;
				}
				return 60 * num / (totalMobility + num + 2);
			}
			else
			{
				if (num == 0)
				{
					return 60 * totalMobility / 2;
				}
				return 60 * num / (totalMobility + num);
			}
		}

		// Token: 0x0600925F RID: 37471 RVA: 0x0034A780 File Offset: 0x00348980
		public int CalculateMobilitySpeedPenalty()
		{
			bool flag;
			return this.CalculateMobilitySpeedPenalty(out flag);
		}

		// Token: 0x06009260 RID: 37472 RVA: 0x0034A798 File Offset: 0x00348998
		public void UpdateMobilitySpeedPenalty()
		{
			bool flag;
			int num = this.CalculateMobilitySpeedPenalty(out flag);
			if (num != this.MobilitySpeedPenaltyApplied && this.ParentObject.Statistics.ContainsKey("MoveSpeed"))
			{
				this.ParentObject.Statistics["MoveSpeed"].Bonus -= this.MobilitySpeedPenaltyApplied;
				this.ParentObject.Statistics["MoveSpeed"].Bonus += num;
				this.MobilitySpeedPenaltyApplied = num;
				if (this.MobilitySpeedPenaltyApplied == 0)
				{
					this.ParentObject.RemoveEffect<MobilityImpaired>();
					return;
				}
				if (flag)
				{
					if (this.ParentObject.HasEffect<MobilityImpaired>())
					{
						this.ParentObject.GetEffect<MobilityImpaired>().Amount = this.MobilitySpeedPenaltyApplied;
						return;
					}
					this.ParentObject.ApplyEffect(new MobilityImpaired(this.MobilitySpeedPenaltyApplied), null);
				}
			}
		}

		// Token: 0x06009261 RID: 37473 RVA: 0x0034A878 File Offset: 0x00348A78
		public GameObject Dismember(BodyPart Part, GameObject Actor = null, IInventory Where = null, bool Obliterate = false, bool Silent = false, IEvent ParentEvent = null)
		{
			if (Part == null)
			{
				return null;
			}
			if (!BeforeDismemberEvent.Check(Actor, this.ParentObject, Part, Where, Silent, Obliterate) && !Obliterate)
			{
				return null;
			}
			this.ParentObject.StopMoving();
			GameObject gameObject = null;
			GameObject gameObject2 = Part.Unimplant(true);
			Part.UnequipPartAndChildren(false, Where, false);
			IInventory inventory = Where ?? this.ParentObject.GetDropInventory();
			if (!Part.Extrinsic && !Obliterate)
			{
				BodyPartType bodyPartType = Part.VariantTypeModel();
				gameObject = GameObject.Create(this.ParentObject.GetPropertyOrTag(bodyPartType.LimbBlueprintProperty, bodyPartType.LimbBlueprintDefault), 0, 0, null, null, null, "Dismember", null);
				string displayName = this.ParentObject.GetDisplayName(int.MaxValue, null, null, true, false, true, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true);
				this.ParentObject.An(int.MaxValue, null, null, true, false, true, false, true, true, true, true, false, false, false, null, false, null, false);
				if (!gameObject.HasPropertyOrTag("SeveredLimbKeepAppearance"))
				{
					Render render = gameObject.Render;
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append(Grammar.MakePossessive(displayName)).Append(' ');
					string color = BodyPartCategory.GetColor(Part.Category);
					if (color != null)
					{
						stringBuilder.Append("{{").Append(color).Append('|');
					}
					stringBuilder.Append(Part.Name);
					if (color != null)
					{
						stringBuilder.Append("}}");
					}
					render.DisplayName = stringBuilder.ToString();
					if (this.ParentObject.HasProperName)
					{
						gameObject.HasProperName = true;
					}
					else
					{
						string text = this.ParentObject.GetxTag("Grammar", "iArticle", gameObject.GetStringProperty("OverrideIArticle", null));
						text = this.ParentObject.GetPropertyOrTag("OverrideIArticle", text);
						if (!text.IsNullOrEmpty())
						{
							gameObject.SetStringProperty("OverrideIArticle", text, false);
						}
					}
					string primaryLiquidNameForLiquidSpecification = Bleeding.GetPrimaryLiquidNameForLiquidSpecification(this.ParentObject.GetBleedLiquid("blood-1000"));
					gameObject.GetPart<Description>().Short = string.Concat(new string[]
					{
						"Dried ",
						primaryLiquidNameForLiquidSpecification,
						" crusts on the severed ",
						Part.Name,
						" of ",
						this.ParentObject.a,
						this.ParentObject.ShortDisplayNameSingleStripped,
						"."
					});
					if (this.ParentObject.HasPropertyOrTag("SeveredLimbColorString"))
					{
						gameObject.Render.ColorString = this.ParentObject.GetPropertyOrTag("SeveredLimbColorString", null);
					}
					if (this.ParentObject.HasPropertyOrTag("SeveredLimbTileColor"))
					{
						gameObject.Render.TileColor = this.ParentObject.GetPropertyOrTag("SeveredLimbTileColor", null);
					}
					if (this.ParentObject.HasPropertyOrTag("SeveredLimbDetailColor"))
					{
						gameObject.Render.DetailColor = this.ParentObject.GetPropertyOrTag("SeveredLimbDetailColor", null);
					}
				}
				gameObject.RequirePart<DismemberedProperties>(false).SetFrom(Part);
				if (this.ParentObject.IsGiganticCreature)
				{
					gameObject.IsGiganticEquipment = true;
					gameObject.ModIntProperty("ModGiganticNoDisplayName", 1, true);
				}
				if (this.ParentObject.IsPlayer())
				{
					gameObject.AddPart<EatenAchievement>(new EatenAchievement(Achievement.EAT_OWN_LIMB), true, false);
				}
				if (this.ParentObject.HasPart<Extradimensional>())
				{
					gameObject.AddPart<CookedAchievement>(new CookedAchievement(Achievement.COOKED_EXTRADIMENSIONAL), true, false);
				}
				if (gameObject2 != null)
				{
					gameObject2.RemoveFromContext(null);
					gameObject.AddPart<CyberneticsButcherableCybernetic>(new CyberneticsButcherableCybernetic(gameObject2), true, false);
					gameObject.RemovePart<Food>();
				}
				if (Part.Type == "Face")
				{
					Armor armor = gameObject.RequirePart<Armor>(false);
					armor.WornOn = "Face";
					if (this.ParentObject.IsPlayer() || this.ParentObject.HasProperty("PlayerCopy"))
					{
						gameObject.AddPart<EquippedAchievement>(new EquippedAchievement(Achievement.WEAR_OWN_FACE), true, false);
					}
					int value = this.ParentObject.Statistics["Level"]._Value;
					int num;
					if (value < 20)
					{
						num = 1;
					}
					else if (value < 35)
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
					armor.Ego = num;
					foreach (KeyValuePair<string, int> keyValuePair in this.ParentObject.Brain.GetBaseAllegiance())
					{
						if (Factions.Get(keyValuePair.Key).Visible)
						{
							AddsRep.AddModifier(gameObject, keyValuePair.Key, -500);
						}
					}
				}
				Temporary.CarryOver(this.ParentObject, gameObject, false);
				Phase.carryOver(this.ParentObject, gameObject, 50);
				if (inventory != null)
				{
					inventory.AddObjectToInventory(gameObject, Actor, false, false, true, null, ParentEvent);
				}
				DroppedEvent.Send(this.ParentObject, gameObject, false);
			}
			AfterDismemberEvent.Send(Actor, this.ParentObject, gameObject, Part, inventory, Silent, Obliterate);
			if (this.ParentObject.IsPlayer() && Part.Name != null && !Obliterate)
			{
				string ordinalName = Part.GetOrdinalName();
				bool plural = Part.Plural;
				if (!Silent)
				{
					Popup.Show(string.Concat(new string[]
					{
						"Your ",
						ordinalName,
						" ",
						plural ? "are" : "is",
						" dismembered!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				JournalAPI.AddAccomplishment(string.Concat(new string[]
				{
					"Your ",
					ordinalName,
					" ",
					plural ? "were" : "was",
					" dismembered."
				}), string.Concat(new string[]
				{
					"While fighting a battle to protect the practice of ",
					HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".practices.!random>", null),
					", =name= valorously had ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" ",
					ordinalName,
					" dismembered."
				}), string.Concat(new string[]
				{
					"While fighting a battle to protect the practice of ",
					HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".practices.!random>", null),
					", =name= valorously had ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" ",
					ordinalName,
					" dismembered."
				}), null, "general", MuralCategory.BodyExperienceBad, MuralWeight.Medium, null, -1L, true);
			}
			if (!Obliterate)
			{
				this.CutAndQueueForRegeneration(Part);
			}
			this.UpdateBodyParts(0);
			this.RecalculateArmor();
			return gameObject;
		}

		// Token: 0x06009262 RID: 37474 RVA: 0x0034AF00 File Offset: 0x00349100
		public bool RegenerateLimb(bool WholeLimb = false, Body.DismemberedPart Part = null, int? ParentID = null, int? Category = null, int[] Categories = null, int? ExceptCategory = null, int[] ExceptCategories = null, bool DoUpdate = true)
		{
			if (Part == null)
			{
				Part = this.FindRegenerablePart(ParentID, Category, Categories, ExceptCategory, ExceptCategories);
				if (Part == null)
				{
					return false;
				}
			}
			else
			{
				if (ParentID != null)
				{
					int parentID = Part.ParentID;
					int? num = ParentID;
					if (!((parentID == num.GetValueOrDefault()) & (num != null)))
					{
						return false;
					}
				}
				if (Category != null)
				{
					int category = Part.Part.Category;
					int? num = Category;
					if (!((category == num.GetValueOrDefault()) & (num != null)))
					{
						return false;
					}
				}
				if (Categories != null && Array.IndexOf<int>(Categories, Part.Part.Category) == -1)
				{
					return false;
				}
				if (ExceptCategory != null)
				{
					int category2 = Part.Part.Category;
					int? num = ExceptCategory;
					if ((category2 == num.GetValueOrDefault()) & (num != null))
					{
						return false;
					}
				}
				if (ExceptCategories != null && Array.IndexOf<int>(ExceptCategories, Part.Part.Category) != -1)
				{
					return false;
				}
			}
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_regeneration_limbRegrowth", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Part.Reattach(this);
			if (this.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You regenerate your " + Part.Part.GetOrdinalName() + "!", 'G', true);
				Achievement.REGENERATE_LIMB.Unlock();
			}
			else if (base.Visible())
			{
				base.DidX("regenerate", this.ParentObject.its + " " + Part.Part.GetOrdinalName(), "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			if (WholeLimb && Part.Part.HasID())
			{
				Body.DismemberedPart dismemberedPart;
				while ((dismemberedPart = this.FindRegenerablePart(new int?(Part.Part.ID), Category, Categories, ExceptCategory, ExceptCategories)) != null)
				{
					this.RegenerateLimb(WholeLimb, dismemberedPart, new int?(Part.Part.ID), Category, Categories, ExceptCategory, ExceptCategories, false);
				}
			}
			if (DoUpdate)
			{
				this.UpdateBodyParts(0);
			}
			return true;
		}

		// Token: 0x06009263 RID: 37475 RVA: 0x0034B104 File Offset: 0x00349304
		public bool RegenerateLimb(ILimbRegenerationEvent E)
		{
			return this.RegenerateLimb(E.Whole, null, E.ParentID, E.Category, E.Categories, E.ExceptCategory, E.ExceptCategories, true);
		}

		// Token: 0x06009264 RID: 37476 RVA: 0x0034B140 File Offset: 0x00349340
		private List<string> SummarizeMissingBodyParts(List<BodyPart> Parts)
		{
			Dictionary<string, int> Counts = new Dictionary<string, int>(Parts.Count);
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(Parts.Count);
			foreach (BodyPart bodyPart in Parts)
			{
				string name = bodyPart.Name;
				if (Counts.ContainsKey(name))
				{
					Dictionary<string, int> counts = Counts;
					string text = name;
					int num = counts[text];
					counts[text] = num + 1;
				}
				else
				{
					Counts.Add(name, 1);
					dictionary[name] = bodyPart.Plural;
				}
			}
			List<string> list = new List<string>(Counts.Keys);
			list.Sort(delegate(string n1, string n2)
			{
				int num3 = Counts[n1].CompareTo(Counts[n2]);
				if (num3 != 0)
				{
					return -num3;
				}
				return n1.CompareTo(n2);
			});
			List<string> list2 = new List<string>(list.Count);
			foreach (string text2 in list)
			{
				int num2 = Counts[text2];
				if (num2 == 1)
				{
					if (this.GetPartByName(text2) == null)
					{
						list2.Add(this.ParentObject.its + " " + text2);
					}
					else if (dictionary[text2])
					{
						list2.Add("a set of " + text2);
					}
					else
					{
						list2.Add(Grammar.A(text2, false));
					}
				}
				else if (dictionary[text2])
				{
					list2.Add(Grammar.Cardinal(num2) + " sets of " + text2);
				}
				else
				{
					list2.Add(Grammar.Cardinal(num2) + " " + Grammar.Pluralize(text2));
				}
			}
			return list2;
		}

		// Token: 0x06009265 RID: 37477 RVA: 0x0034B31C File Offset: 0x0034951C
		public bool GetMissingLimbsDescription(StringBuilder SB, bool PrependNewlineIfContent = false)
		{
			if (this.DismemberedParts == null)
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			foreach (Body.DismemberedPart dismemberedPart in this.DismemberedParts)
			{
				if (dismemberedPart.Part.Abstract)
				{
					num2++;
				}
				else if (dismemberedPart.Part.SupportsDependent == null || this.GetDismemberedPartByDependsOn(dismemberedPart.Part.SupportsDependent) == null)
				{
					num++;
				}
			}
			if (num == 0)
			{
				return false;
			}
			if (PrependNewlineIfContent)
			{
				SB.Append('\n');
			}
			if (num2 > 0)
			{
				List<BodyPart> list = new List<BodyPart>(num);
				List<BodyPart> list2 = new List<BodyPart>(num2);
				foreach (Body.DismemberedPart dismemberedPart2 in this.DismemberedParts)
				{
					if (dismemberedPart2.Part.Abstract)
					{
						list2.Add(dismemberedPart2.Part);
					}
					else if (dismemberedPart2.Part.SupportsDependent == null || this.GetDismemberedPartByDependsOn(dismemberedPart2.Part.SupportsDependent) == null)
					{
						list.Add(dismemberedPart2.Part);
					}
				}
				List<string> list3 = this.SummarizeMissingBodyParts(list);
				List<string> list4 = this.SummarizeMissingBodyParts(list2);
				SB.Append(this.ParentObject.Itis).Append(" missing ").Append(Grammar.MakeAndList(list3, true))
					.Append(", and so ")
					.Append(this.ParentObject.it)
					.Append(this.ParentObject.GetVerb("do", true, true, false, true, false))
					.Append(" not have the use of ")
					.Append(Grammar.MakeOrList(list4, true))
					.Append('.');
			}
			else
			{
				List<BodyPart> list5 = new List<BodyPart>(num);
				foreach (Body.DismemberedPart dismemberedPart3 in this.DismemberedParts)
				{
					if (!dismemberedPart3.Part.Abstract && (dismemberedPart3.Part.SupportsDependent == null || this.GetDismemberedPartByDependsOn(dismemberedPart3.Part.SupportsDependent) == null))
					{
						list5.Add(dismemberedPart3.Part);
					}
				}
				List<string> list6 = this.SummarizeMissingBodyParts(list5);
				SB.Append(this.ParentObject.Itis).Append(" missing ").Append(Grammar.MakeAndList(list6, true))
					.Append('.');
			}
			return true;
		}

		// Token: 0x06009266 RID: 37478 RVA: 0x0034B5B0 File Offset: 0x003497B0
		public string GetMissingLimbsDescription(bool PrependNewlineIfContent = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			this.GetMissingLimbsDescription(stringBuilder, PrependNewlineIfContent);
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06009267 RID: 37479 RVA: 0x0034B5E0 File Offset: 0x003497E0
		public bool Rebuild(string AsAnatomy)
		{
			List<BodyPart> topLevelDynamicParts = this.GetTopLevelDynamicParts();
			List<BodyPartPositionHint> list = null;
			List<BodyPart> partsSkippingDynamicTrees = this.GetPartsSkippingDynamicTrees();
			List<GameObject> list2 = Event.NewGameObjectList();
			Dictionary<GameObject, string> dictionary = new Dictionary<GameObject, string>(partsSkippingDynamicTrees.Count);
			List<GameObject> list3 = Event.NewGameObjectList();
			Dictionary<GameObject, bool> dictionary2 = new Dictionary<GameObject, bool>(partsSkippingDynamicTrees.Count);
			List<GameObject> list4 = Event.NewGameObjectList();
			List<GameObject> list5 = null;
			bool flag = false;
			if (topLevelDynamicParts.Count > 0)
			{
				list = new List<BodyPartPositionHint>();
				foreach (BodyPart bodyPart in topLevelDynamicParts)
				{
					list.Add(new BodyPartPositionHint(bodyPart));
				}
			}
			foreach (BodyPart bodyPart2 in partsSkippingDynamicTrees)
			{
				GameObject cybernetics = bodyPart2.Cybernetics;
				if (cybernetics != null && !list2.Contains(cybernetics))
				{
					if (cybernetics.IsStackable())
					{
						cybernetics.SetIntProperty("NeverStack", 1, false);
					}
					list2.Add(cybernetics);
					dictionary.Add(cybernetics, bodyPart2.Type);
					bodyPart2.Unimplant(true);
				}
			}
			foreach (BodyPart bodyPart3 in partsSkippingDynamicTrees)
			{
				GameObject equipped = bodyPart3.Equipped;
				if (GameObject.Validate(equipped))
				{
					if (equipped.IsStackable())
					{
						if (bodyPart3.TryUnequip(true, true, true, false))
						{
							list3.Add(equipped);
							dictionary2.Add(equipped, bodyPart3.Type == "Hand");
							if (!list4.Contains(equipped))
							{
								list4.Add(equipped);
							}
						}
					}
					else if (bodyPart3.TryUnequip(true, true, false, false))
					{
						list3.Add(equipped);
						dictionary2.Add(equipped, bodyPart3.Type == "Hand");
					}
				}
			}
			Mutations part = this.ParentObject.GetPart<Mutations>();
			List<BaseMutation> list6 = ((part != null) ? part.ActiveMutationList : null);
			List<BaseMutation> list7 = null;
			if (list6 != null)
			{
				foreach (BaseMutation baseMutation in list6)
				{
					if (baseMutation.AffectsBodyParts() || baseMutation.GeneratesEquipment())
					{
						baseMutation.Unmutate(baseMutation.ParentObject);
						if (list7 == null)
						{
							list7 = new List<BaseMutation>(3) { baseMutation };
						}
						else
						{
							list7.Add(baseMutation);
						}
					}
				}
			}
			foreach (BodyPart bodyPart4 in this.GetParts())
			{
				GameObject equipped2 = bodyPart4.Equipped;
				if (GameObject.Validate(equipped2) && !list3.Contains(equipped2) && bodyPart4.ForceUnequip(true, true, false))
				{
					list3.Add(equipped2);
					dictionary2.Add(equipped2, bodyPart4.Type == "Hand");
					if (!list4.Contains(equipped2) && equipped2.IsStackable())
					{
						list4.Add(equipped2);
					}
					if (!equipped2.HasPropertyOrTag("CursedBodyRebuildAllowUnequip"))
					{
						if (list5 == null)
						{
							list5 = new List<GameObject>();
						}
						list5.Add(equipped2);
					}
				}
			}
			this.Anatomy = AsAnatomy;
			if (list != null)
			{
				foreach (BodyPartPositionHint bodyPartPositionHint in list)
				{
					BodyPart body;
					int num;
					bodyPartPositionHint.GetBestParentAndPosition(this, out body, out num);
					if (body == null)
					{
						body = this._Body;
						num = -1;
						if (this._Body.Parts != null)
						{
							foreach (BodyPart bodyPart5 in this._Body.Parts)
							{
								if (bodyPart5.Type == "Thrown Weapon" || bodyPart5.Type == "Floating Nearby")
								{
									num = bodyPart5.Position - 1;
									break;
								}
							}
						}
					}
					body.AddPart(bodyPartPositionHint.Self, num, false);
					flag = true;
				}
			}
			if (list7 != null)
			{
				ActivatedAbilities activatedAbilities = this.ParentObject.ActivatedAbilities;
				bool flag2 = false;
				if (activatedAbilities != null)
				{
					flag2 = activatedAbilities.Silent;
					if (!flag2)
					{
						activatedAbilities.Silent = true;
					}
				}
				try
				{
					foreach (BaseMutation baseMutation2 in list7)
					{
						baseMutation2.Mutate(baseMutation2.ParentObject, baseMutation2.BaseLevel);
					}
				}
				finally
				{
					if (activatedAbilities != null && !flag2)
					{
						activatedAbilities.Silent = false;
					}
				}
			}
			if (list2.Count > 0)
			{
				List<GameObject> list8 = null;
				foreach (GameObject gameObject in list2)
				{
					if (gameObject.IsValid())
					{
						bool flag3 = false;
						foreach (BodyPart bodyPart6 in this.GetPart(dictionary[gameObject]))
						{
							if (bodyPart6.Cybernetics == null)
							{
								gameObject.RemoveFromContext(null);
								bodyPart6.Implant(gameObject, false, false);
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							if (list8 == null)
							{
								list8 = new List<GameObject>(list2.Count) { gameObject };
							}
							else
							{
								list8.Add(gameObject);
							}
						}
					}
				}
				if (list8 != null)
				{
					using (List<GameObject>.Enumerator enumerator4 = list8.GetEnumerator())
					{
						IL_061E:
						while (enumerator4.MoveNext())
						{
							GameObject gameObject2 = enumerator4.Current;
							foreach (string text in gameObject2.GetPart<CyberneticsBaseItem>().Slots.Split(',', StringSplitOptions.None))
							{
								foreach (BodyPart bodyPart7 in this.GetPart(text))
								{
									if (bodyPart7.Cybernetics == null)
									{
										gameObject2.RemoveFromContext(null);
										bodyPart7.Implant(gameObject2, false, false);
										goto IL_061E;
									}
								}
							}
							if (gameObject2.HasTagOrProperty("CyberneticsNoRemove") || gameObject2.HasTagOrProperty("CyberneticsDestroyOnRemoval"))
							{
								gameObject2.Destroy(null, false, false, null);
							}
						}
					}
				}
			}
			if (list3.Count > 0)
			{
				foreach (GameObject gameObject3 in list3)
				{
					this.ParentObject.AutoEquip(gameObject3, true, dictionary2[gameObject3], true);
				}
			}
			foreach (GameObject gameObject4 in list4)
			{
				if (GameObject.Validate(gameObject4))
				{
					gameObject4.CheckStack();
				}
			}
			if (list5 != null)
			{
				foreach (GameObject gameObject5 in list5)
				{
					if (GameObject.Validate(gameObject5))
					{
						Physics physics = gameObject5.Physics;
						if (((physics != null) ? physics.Equipped : null) == null)
						{
							gameObject5.Obliterate(null, false, null);
						}
					}
				}
			}
			foreach (BodyPart bodyPart8 in partsSkippingDynamicTrees)
			{
				bodyPart8._Equipped = null;
				bodyPart8._Cybernetics = null;
				bodyPart8.DefaultBehavior = null;
			}
			if (flag)
			{
				this.UpdateBodyParts(0);
			}
			return true;
		}

		// Token: 0x06009268 RID: 37480 RVA: 0x0034BE2C File Offset: 0x0034A02C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009269 RID: 37481 RVA: 0x0034BE2F File Offset: 0x0034A02F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600926A RID: 37482 RVA: 0x0034BE39 File Offset: 0x0034A039
		public bool FireEventOnBodyparts(Event E)
		{
			return this._Body.FireEvent(E);
		}

		// Token: 0x0600926B RID: 37483 RVA: 0x0034BE47 File Offset: 0x0034A047
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "BeforeDeathRemoval") || this.FireEventOnBodyparts(E)) && base.FireEvent(E);
		}

		// Token: 0x0600926C RID: 37484 RVA: 0x0034BE6D File Offset: 0x0034A06D
		public override void TurnTick(long TimeTick, int Amount)
		{
			this._Body.TurnTick(TimeTick, Amount);
		}

		// Token: 0x0600926D RID: 37485 RVA: 0x0034BE7C File Offset: 0x0034A07C
		public void TypeDump(StringBuilder SB)
		{
			this._Body.TypeDump(SB);
		}

		// Token: 0x0600926E RID: 37486 RVA: 0x0034BE8C File Offset: 0x0034A08C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AnyRegenerableLimbsEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<FlushWeightCacheEvent>.ID || ID == PooledEvent<GenericQueryEvent>.ID || ID == GetCarriedWeightEvent.ID || ID == PooledEvent<GetContentsEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == GetExtrinsicValueEvent.ID || ID == GetExtrinsicWeightEvent.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<HasBlueprintEvent>.ID || ID == PooledEvent<QuerySlotListEvent>.ID || ID == RegenerateLimbEvent.ID || ID == PooledEvent<StripContentsEvent>.ID || (MinEvent.CascadeTo(cascade, 1) && this._Body.WantEvent(ID, cascade));
		}

		// Token: 0x0600926F RID: 37487 RVA: 0x0034BF4E File Offset: 0x0034A14E
		public override bool HandleEvent(HasBlueprintEvent E)
		{
			return !this._Body.HasBlueprint(E.Blueprint) && base.HandleEvent(E);
		}

		// Token: 0x06009270 RID: 37488 RVA: 0x0034BF6C File Offset: 0x0034A16C
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.UpdateMobilitySpeedPenalty();
			return base.HandleEvent(E);
		}

		// Token: 0x06009271 RID: 37489 RVA: 0x0034BF7B File Offset: 0x0034A17B
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.UpdateMobilitySpeedPenalty();
			return base.HandleEvent(E);
		}

		// Token: 0x06009272 RID: 37490 RVA: 0x0034BF8A File Offset: 0x0034A18A
		public override bool HandleEvent(EndTurnEvent E)
		{
			this._Body.HandleEvent(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06009273 RID: 37491 RVA: 0x0034BF9F File Offset: 0x0034A19F
		public override bool HandleEvent(MinEvent E)
		{
			return base.HandleEvent(E) && (!E.CascadeTo(1) || this._Body.HandleEvent(E));
		}

		// Token: 0x06009274 RID: 37492 RVA: 0x0034BFC6 File Offset: 0x0034A1C6
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (!this.ParentObject.HasTag("NoDropOnDeath") && !this.ParentObject.IsTemporary && this._Body != null)
			{
				this._Body.UnequipPartAndChildren(true, null, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009275 RID: 37493 RVA: 0x0034C004 File Offset: 0x0034A204
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 2)
			{
				GameObject gameObject;
				int maximumEffectiveElectricalConductivityOfMobilityBodyParts = this.GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out gameObject);
				int meanElectricalConductivity = this.GetMeanElectricalConductivity();
				int num;
				if (maximumEffectiveElectricalConductivityOfMobilityBodyParts * 3 < meanElectricalConductivity)
				{
					num = Math.Max(maximumEffectiveElectricalConductivityOfMobilityBodyParts * 3 * meanElectricalConductivity / 100, maximumEffectiveElectricalConductivityOfMobilityBodyParts);
					if (gameObject != null && E.ReductionObject == null)
					{
						E.ReductionObject = gameObject;
					}
				}
				else
				{
					num = meanElectricalConductivity;
				}
				E.MinValue(num);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009276 RID: 37494 RVA: 0x0034C063 File Offset: 0x0034A263
		public override bool HandleEvent(GetExtrinsicValueEvent E)
		{
			return this._Body.ProcessExtrinsicValue(E) && base.HandleEvent(E);
		}

		// Token: 0x06009277 RID: 37495 RVA: 0x0034C07C File Offset: 0x0034A27C
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			return this._Body.ProcessExtrinsicWeight(E) && base.HandleEvent(E);
		}

		// Token: 0x06009278 RID: 37496 RVA: 0x0034C095 File Offset: 0x0034A295
		public override bool HandleEvent(GetCarriedWeightEvent E)
		{
			E.Weight += (double)this.GetWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x06009279 RID: 37497 RVA: 0x0034C0B2 File Offset: 0x0034A2B2
		public override bool HandleEvent(QuerySlotListEvent E)
		{
			return this._Body.ProcessQuerySlotList(E) && base.HandleEvent(E);
		}

		// Token: 0x0600927A RID: 37498 RVA: 0x0034C0CB File Offset: 0x0034A2CB
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.DismemberedParts != null && this.DismemberedParts.Count > 0)
			{
				this.GetMissingLimbsDescription(E.Postfix, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600927B RID: 37499 RVA: 0x0034C0F8 File Offset: 0x0034A2F8
		public override bool HandleEvent(StripContentsEvent E)
		{
			List<GameObject> list = Event.NewGameObjectList();
			this.GetEquippedObjects(list);
			List<GameObject> list2 = Event.NewGameObjectList();
			this.GetInstalledCybernetics(list2);
			foreach (GameObject gameObject in list)
			{
				if ((!E.KeepNatural || !gameObject.IsNatural()) && (gameObject.Physics == null || gameObject.Physics.IsReal) && !list2.Contains(gameObject))
				{
					list2.Add(gameObject);
				}
			}
			foreach (GameObject gameObject2 in list2)
			{
				gameObject2.Obliterate(null, E.Silent, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600927C RID: 37500 RVA: 0x0034C1D8 File Offset: 0x0034A3D8
		public override bool HandleEvent(GetContentsEvent E)
		{
			this._Body.GetContents(E.Objects);
			return base.HandleEvent(E);
		}

		// Token: 0x0600927D RID: 37501 RVA: 0x0034C1F4 File Offset: 0x0034A3F4
		public override bool HandleEvent(RegenerateLimbEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				if (E.All)
				{
					int num = Math.Max(100, this.GetDismemberedPartCount() * 2);
					int num2 = 0;
					while (this.RegenerateLimb(E))
					{
						if (++num2 >= num)
						{
							break;
						}
					}
				}
				else
				{
					this.RegenerateLimb(E);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600927E RID: 37502 RVA: 0x0034C24B File Offset: 0x0034A44B
		public override bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return this.FindRegenerablePart(E) == null && base.HandleEvent(E);
		}

		// Token: 0x0600927F RID: 37503 RVA: 0x0034C25F File Offset: 0x0034A45F
		public override bool HandleEvent(FlushWeightCacheEvent E)
		{
			this.FlushWeightCache();
			return base.HandleEvent(E);
		}

		// Token: 0x06009280 RID: 37504 RVA: 0x0034C26E File Offset: 0x0034A46E
		public int GetPartDepth(BodyPart Part)
		{
			return this._Body.GetPartDepth(Part, 0);
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x0034C280 File Offset: 0x0034A480
		public void GetBodyPartsImplying(out List<BodyPart> Result, BodyPart Part, bool EvenIfDismembered = true)
		{
			Result = null;
			BodyPartType bodyPartType = Part.VariantTypeModel();
			if (bodyPartType.ImpliedBy.IsNullOrEmpty())
			{
				return;
			}
			int impliedPer = bodyPartType.GetImpliedPer();
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			Body.SomeBodyParts.Clear();
			Body.OtherBodyParts.Clear();
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart.VariantType == bodyPartType.ImpliedBy && bodyPart.Laterality == Part.Laterality)
				{
					Body.SomeBodyParts.Add(bodyPart);
				}
				if (bodyPart.VariantType == bodyPartType.Type || bodyPart.VariantType == bodyPart.VariantTypeModel().ImpliedBy)
				{
					Body.OtherBodyParts.Add(bodyPart);
				}
				i++;
			}
			if (Body.OtherBodyParts.Count == 1 && Body.OtherBodyParts[0] == Part && Body.SomeBodyParts.Count <= impliedPer)
			{
				Result = new List<BodyPart>(Body.SomeBodyParts);
			}
			else
			{
				Result = new List<BodyPart>(impliedPer);
				int num = 0;
				int j = 0;
				int count2 = Body.OtherBodyParts.Count;
				while (j < count2)
				{
					if (Body.OtherBodyParts[j] == Part)
					{
						for (int k = 0; k < impliedPer; k++)
						{
							if (num < Body.SomeBodyParts.Count)
							{
								Result.Add(Body.SomeBodyParts[num]);
							}
							num++;
						}
					}
					else
					{
						num += impliedPer;
					}
					j++;
				}
			}
			if (!EvenIfDismembered && Result != null && Result.Count > 0 && this.DismemberedParts != null && this.DismemberedParts.Count > 0)
			{
				Body.SomeBodyParts.Clear();
				Body.SomeBodyParts.AddRange(Result);
				foreach (BodyPart bodyPart2 in Body.SomeBodyParts)
				{
					if (this.IsDismembered(bodyPart2))
					{
						Result.Remove(bodyPart2);
					}
				}
			}
		}

		// Token: 0x06009282 RID: 37506 RVA: 0x0034C4A0 File Offset: 0x0034A6A0
		public List<BodyPart> GetBodyPartsImplying(BodyPart Part, bool EvenIfDismembered = true)
		{
			List<BodyPart> list;
			this.GetBodyPartsImplying(out list, Part, EvenIfDismembered);
			return list;
		}

		// Token: 0x06009283 RID: 37507 RVA: 0x0034C4B8 File Offset: 0x0034A6B8
		public int GetBodyPartCountImplying(BodyPart Part)
		{
			if (Part.VariantTypeModel().ImpliedBy.IsNullOrEmpty())
			{
				return 0;
			}
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			return this.GetBodyPartCountImplyingInternal(Part, null);
		}

		// Token: 0x06009284 RID: 37508 RVA: 0x0034C4EC File Offset: 0x0034A6EC
		private int GetBodyPartCountImplyingInternal(BodyPart Part, BodyPartType Type = null)
		{
			if (Type == null)
			{
				Type = Part.VariantTypeModel();
				if (Type.ImpliedBy.IsNullOrEmpty())
				{
					return 0;
				}
			}
			int impliedPer = Type.GetImpliedPer();
			Body.SomeBodyParts.Clear();
			Body.OtherBodyParts.Clear();
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart.VariantType == Type.ImpliedBy && bodyPart.Laterality == Part.Laterality)
				{
					Body.SomeBodyParts.Add(bodyPart);
				}
				if (bodyPart.VariantType == Type.Type || bodyPart.VariantType == bodyPart.VariantTypeModel().ImpliedBy)
				{
					Body.OtherBodyParts.Add(bodyPart);
				}
				i++;
			}
			if (Body.OtherBodyParts.Count == 1 && Body.OtherBodyParts[0] == Part && Body.SomeBodyParts.Count <= impliedPer)
			{
				return Body.SomeBodyParts.Count;
			}
			int num = 0;
			int num2 = 0;
			int j = 0;
			int count2 = Body.OtherBodyParts.Count;
			while (j < count2)
			{
				if (Body.OtherBodyParts[j] == Part)
				{
					for (int k = 0; k < impliedPer; k++)
					{
						if (num2 < Body.SomeBodyParts.Count)
						{
							num++;
						}
						num2++;
					}
				}
				else
				{
					num2 += impliedPer;
				}
				j++;
			}
			return num;
		}

		// Token: 0x06009285 RID: 37509 RVA: 0x0034C64C File Offset: 0x0034A84C
		public bool AnyBodyPartsImplying(BodyPart Part)
		{
			if (Part.VariantTypeModel().ImpliedBy.IsNullOrEmpty())
			{
				return false;
			}
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			return this.AnyBodyPartsImplyingInternal(Part, null);
		}

		// Token: 0x06009286 RID: 37510 RVA: 0x0034C680 File Offset: 0x0034A880
		private bool AnyBodyPartsImplyingInternal(BodyPart Part, BodyPartType Type = null)
		{
			if (Type == null)
			{
				Type = Part.VariantTypeModel();
				if (Type.ImpliedBy.IsNullOrEmpty())
				{
					return false;
				}
			}
			int impliedPer = Type.GetImpliedPer();
			Body.SomeBodyParts.Clear();
			Body.OtherBodyParts.Clear();
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart.VariantType == Type.ImpliedBy && bodyPart.Laterality == Part.Laterality)
				{
					Body.SomeBodyParts.Add(bodyPart);
				}
				if (bodyPart.VariantType == Type.Type || bodyPart.VariantType == bodyPart.VariantTypeModel().ImpliedBy)
				{
					Body.OtherBodyParts.Add(bodyPart);
				}
				i++;
			}
			if (Body.OtherBodyParts.Count == 1 && Body.OtherBodyParts[0] == Part && Body.SomeBodyParts.Count <= impliedPer)
			{
				if (Body.SomeBodyParts.Count > 0)
				{
					return true;
				}
			}
			else
			{
				int num = 0;
				int j = 0;
				int count2 = Body.OtherBodyParts.Count;
				while (j < count2)
				{
					if (Body.OtherBodyParts[j] == Part)
					{
						for (int k = 0; k < impliedPer; k++)
						{
							if (num < Body.SomeBodyParts.Count)
							{
								return true;
							}
							num++;
						}
					}
					else
					{
						num += impliedPer;
					}
					j++;
				}
			}
			return false;
		}

		// Token: 0x06009287 RID: 37511 RVA: 0x0034C7DC File Offset: 0x0034A9DC
		public bool ShouldRemoveDueToLackOfImplication(BodyPart Part)
		{
			if (Part.VariantTypeModel().ImpliedBy.IsNullOrEmpty())
			{
				return false;
			}
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			return this.ShouldRemoveDueToLackOfImplicationInternal(Part, null);
		}

		// Token: 0x06009288 RID: 37512 RVA: 0x0034C810 File Offset: 0x0034AA10
		private bool ShouldRemoveDueToLackOfImplicationInternal(BodyPart Part, BodyPartType Type = null)
		{
			if (Type == null)
			{
				Type = Part.VariantTypeModel();
				if (Type.ImpliedBy.IsNullOrEmpty())
				{
					return false;
				}
			}
			int impliedPer = Type.GetImpliedPer();
			Body.SomeBodyParts.Clear();
			Body.OtherBodyParts.Clear();
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart.VariantType == Type.ImpliedBy && bodyPart.Laterality == Part.Laterality)
				{
					Body.SomeBodyParts.Add(bodyPart);
				}
				if (bodyPart.VariantType == Type.Type || bodyPart.VariantType == bodyPart.VariantTypeModel().ImpliedBy)
				{
					Body.OtherBodyParts.Add(bodyPart);
				}
				i++;
			}
			if (Body.OtherBodyParts.Count == 1 && Body.OtherBodyParts[0] == Part && Body.SomeBodyParts.Count <= impliedPer)
			{
				if (Body.SomeBodyParts.Count > 0)
				{
					return false;
				}
			}
			else
			{
				int num = 0;
				int j = 0;
				int count2 = Body.OtherBodyParts.Count;
				while (j < count2)
				{
					if (Body.OtherBodyParts[j] == Part)
					{
						for (int k = 0; k < impliedPer; k++)
						{
							if (num < Body.SomeBodyParts.Count)
							{
								return false;
							}
							num++;
						}
					}
					else
					{
						num += impliedPer;
					}
					j++;
				}
			}
			return true;
		}

		// Token: 0x06009289 RID: 37513 RVA: 0x0034C96C File Offset: 0x0034AB6C
		public bool DoesPartImplyPart(BodyPart PartMaybeImplying, BodyPart PartMaybeImplied)
		{
			if (PartMaybeImplied.VariantTypeModel().ImpliedBy.IsNullOrEmpty())
			{
				return false;
			}
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			return this.DoesPartImplyPartInternal(PartMaybeImplied, PartMaybeImplying, null, false);
		}

		// Token: 0x0600928A RID: 37514 RVA: 0x0034C9A4 File Offset: 0x0034ABA4
		private bool DoesPartImplyPartInternal(BodyPart PartMaybeImplying, BodyPart PartMaybeImplied, BodyPartType Type = null, bool TypeOnly = false)
		{
			if (Type == null)
			{
				Type = PartMaybeImplied.VariantTypeModel();
				if (Type.ImpliedBy.IsNullOrEmpty())
				{
					return false;
				}
			}
			Body.SomeBodyParts.Clear();
			Body.OtherBodyParts.Clear();
			int impliedPer = Type.GetImpliedPer();
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart.VariantType == Type.ImpliedBy && bodyPart.Laterality == PartMaybeImplied.Laterality)
				{
					Body.SomeBodyParts.Add(bodyPart);
				}
				if (bodyPart.VariantType == Type.Type || (!TypeOnly && bodyPart.VariantType == bodyPart.VariantTypeModel().ImpliedBy))
				{
					Body.OtherBodyParts.Add(bodyPart);
				}
				i++;
			}
			if (Body.OtherBodyParts.Count == 1 && Body.OtherBodyParts[0] == PartMaybeImplied && Body.SomeBodyParts.Count <= impliedPer)
			{
				if (Body.SomeBodyParts.Contains(PartMaybeImplying))
				{
					return true;
				}
			}
			else
			{
				int num = 0;
				int j = 0;
				int count2 = Body.OtherBodyParts.Count;
				while (j < count2)
				{
					if (Body.OtherBodyParts[j] == PartMaybeImplied)
					{
						for (int k = 0; k < impliedPer; k++)
						{
							if (num < Body.SomeBodyParts.Count && Body.SomeBodyParts[num] == PartMaybeImplying)
							{
								return true;
							}
							num++;
						}
					}
					else
					{
						num += impliedPer;
					}
					j++;
				}
			}
			return false;
		}

		// Token: 0x0600928B RID: 37515 RVA: 0x0034CB14 File Offset: 0x0034AD14
		public void GetBodyPartsImpliedBy(out List<BodyPart> Result, BodyPart Part)
		{
			Result = null;
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart != Part && this.DoesPartImplyPartInternal(Part, bodyPart, null, false))
				{
					if (Result == null)
					{
						Result = new List<BodyPart>();
					}
					Result.Add(bodyPart);
				}
				i++;
			}
		}

		// Token: 0x0600928C RID: 37516 RVA: 0x0034CB80 File Offset: 0x0034AD80
		public List<BodyPart> GetBodyPartsImpliedBy(BodyPart Part)
		{
			List<BodyPart> list;
			this.GetBodyPartsImpliedBy(out list, Part);
			return list;
		}

		// Token: 0x0600928D RID: 37517 RVA: 0x0034CB97 File Offset: 0x0034AD97
		public BodyPart GetBodyPartOfTypeImpliedBy(BodyPart Part, BodyPartType Type)
		{
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			return this.GetBodyPartOfTypeImpliedByInternal(Part, Type);
		}

		// Token: 0x0600928E RID: 37518 RVA: 0x0034CBB8 File Offset: 0x0034ADB8
		private BodyPart GetBodyPartOfTypeImpliedByInternal(BodyPart Part, BodyPartType Type)
		{
			int i = 0;
			int count = Body.AllBodyParts.Count;
			while (i < count)
			{
				BodyPart bodyPart = Body.AllBodyParts[i];
				if (bodyPart != Part && bodyPart.VariantType == Type.Type && this.DoesPartImplyPartInternal(Part, bodyPart, Type, true))
				{
					return bodyPart;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600928F RID: 37519 RVA: 0x0034CC10 File Offset: 0x0034AE10
		public void CheckImpliedParts(int Depth = 0)
		{
			Body.AllBodyParts.Clear();
			this.GetParts(Body.AllBodyParts, true);
			bool flag = false;
			int i = 0;
			int count = Anatomies.BodyPartTypeList.Count;
			while (i < count)
			{
				BodyPartType bodyPartType = Anatomies.BodyPartTypeList[i];
				if (!bodyPartType.ImpliedBy.IsNullOrEmpty())
				{
					int num = 0;
					int num2 = 0;
					int num3 = -1;
					BodyPart bodyPart = null;
					int j = 0;
					int count2 = Body.AllBodyParts.Count;
					while (j < count2)
					{
						BodyPart bodyPart2 = Body.AllBodyParts[j];
						if (bodyPart2.VariantType == bodyPartType.ImpliedBy)
						{
							num++;
						}
						if (bodyPart2.VariantType == bodyPartType.Type)
						{
							num2++;
							if (bodyPart2.Position > num3)
							{
								num3 = bodyPart2.Position;
								bodyPart = bodyPart2;
							}
						}
						j++;
					}
					if (num > 0)
					{
						int impliedPer = bodyPartType.GetImpliedPer();
						int num4 = num / impliedPer;
						if (num % impliedPer != 0)
						{
							num4++;
						}
						for (int k = num2; k < num4; k++)
						{
							BodyPart bodyPart3 = null;
							int l = 0;
							int count3 = Body.AllBodyParts.Count;
							while (l < count3)
							{
								BodyPart bodyPart4 = Body.AllBodyParts[l];
								if (bodyPart4.VariantType == bodyPartType.ImpliedBy && this.GetBodyPartOfTypeImpliedByInternal(bodyPart4, bodyPartType) == null)
								{
									bodyPart3 = bodyPart4;
									break;
								}
								l++;
							}
							if (bodyPart3 != null)
							{
								BodyPart bodyPart5;
								if (bodyPart != null)
								{
									BodyPart body = this._Body;
									BodyPartType bodyPartType2 = bodyPartType;
									int num5 = bodyPart3.Laterality;
									bodyPart5 = body.AddPartAt(bodyPart, bodyPartType2, num5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, false);
								}
								else
								{
									BodyPart body2 = this._Body;
									BodyPartType bodyPartType2 = bodyPartType;
									int num5 = bodyPart3.Laterality;
									bodyPart5 = body2.AddPartAt(new string[] { "Feet", "Roots", "Thrown Weapon" }, bodyPartType2, num5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, false);
								}
								bodyPart = bodyPart5;
								flag = true;
							}
						}
					}
				}
				i++;
			}
			List<BodyPart> list = null;
			int m = 0;
			int count4 = Body.AllBodyParts.Count;
			while (m < count4)
			{
				if (this.ShouldRemoveDueToLackOfImplicationInternal(Body.AllBodyParts[m], null))
				{
					if (list == null)
					{
						list = new List<BodyPart>();
					}
					list.Add(Body.AllBodyParts[m]);
				}
				m++;
			}
			if (list != null)
			{
				int n = 0;
				int count5 = list.Count;
				while (n < count5)
				{
					BodyPart bodyPart6 = list[n];
					this.RemovePart(bodyPart6, false);
					flag = true;
					n++;
				}
			}
			if (flag)
			{
				this.UpdateBodyParts(Depth + 1);
				this.RecalculateArmor();
			}
		}

		// Token: 0x06009290 RID: 37520 RVA: 0x0034CF9F File Offset: 0x0034B19F
		public bool IsDefaultBehaviorClearOfEquipped(GameObject GO)
		{
			return this._Body.IsDefaultBehaviorClearOfEquipped(GO);
		}

		// Token: 0x06009291 RID: 37521 RVA: 0x0034CFAD File Offset: 0x0034B1AD
		public int GetMaximumElectricalConductivityOfMobilityBodyParts()
		{
			return this._Body.GetMaximumElectricalConductivityOfMobilityBodyParts();
		}

		// Token: 0x06009292 RID: 37522 RVA: 0x0034CFBA File Offset: 0x0034B1BA
		public int GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out GameObject CriticalObject)
		{
			return this._Body.GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out CriticalObject);
		}

		// Token: 0x06009293 RID: 37523 RVA: 0x0034CFC8 File Offset: 0x0034B1C8
		public int GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts()
		{
			return this._Body.GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts();
		}

		// Token: 0x06009294 RID: 37524 RVA: 0x0034CFD5 File Offset: 0x0034B1D5
		public int GetTotalElectricalConductivity()
		{
			return this._Body.GetTotalElectricalConductivity();
		}

		// Token: 0x06009295 RID: 37525 RVA: 0x0034CFE2 File Offset: 0x0034B1E2
		public int GetMeanElectricalConductivity()
		{
			return this.GetTotalElectricalConductivity() / this.GetPartCount();
		}

		// Token: 0x06009296 RID: 37526 RVA: 0x0034CFF1 File Offset: 0x0034B1F1
		public int GetElectricalConductivity()
		{
			return Math.Min(this.GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(), this.GetMeanElectricalConductivity());
		}

		// Token: 0x06009297 RID: 37527 RVA: 0x0034D004 File Offset: 0x0034B204
		public int GetElectricalConductivity(out GameObject CriticalObject)
		{
			return Math.Min(this.GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out CriticalObject), this.GetMeanElectricalConductivity());
		}

		// Token: 0x06009298 RID: 37528 RVA: 0x0034D018 File Offset: 0x0034B218
		public BodyPart ReduceParts(Func<BodyPart, BodyPart, BodyPart> Reduction)
		{
			return this._Body.ReduceParts(Reduction, null);
		}

		// Token: 0x06009299 RID: 37529 RVA: 0x0034D028 File Offset: 0x0034B228
		public BodyPart GetUnequippedPreferredBodyPartOrAlternate(string PreferredType, string AlternateType)
		{
			BodyPart bodyPart = null;
			return this._Body.GetUnequippedPreferredBodyPartOrAlternate(PreferredType, AlternateType, ref bodyPart) ?? bodyPart;
		}

		// Token: 0x04003CA7 RID: 15527
		public const int MAXIMUM_MOBILITY_MOVE_SPEED_PENALTY = 60;

		// Token: 0x04003CA8 RID: 15528
		public const int BASIC_FULL_MOBILITY = 2;

		// Token: 0x04003CA9 RID: 15529
		public int MobilitySpeedPenaltyApplied;

		// Token: 0x04003CAA RID: 15530
		[NonSerialized]
		public BodyPart _Body;

		// Token: 0x04003CAB RID: 15531
		[NonSerialized]
		public int RequiredMobility;

		// Token: 0x04003CAC RID: 15532
		public bool built;

		// Token: 0x04003CAD RID: 15533
		[NonSerialized]
		public List<Body.DismemberedPart> DismemberedParts;

		// Token: 0x04003CAE RID: 15534
		public string _Anatomy;

		// Token: 0x04003CAF RID: 15535
		[NonSerialized]
		private static Event eBodypartsUpdated = new ImmutableEvent("BodypartsUpdated");

		// Token: 0x04003CB0 RID: 15536
		[NonSerialized]
		public static Dictionary<GameObject, BodyPart> DeepCopyEquipMap = null;

		// Token: 0x04003CB1 RID: 15537
		public int WeightCache = -1;

		// Token: 0x04003CB2 RID: 15538
		[NonSerialized]
		public static List<BodyPart> partStatic = new List<BodyPart>();

		// Token: 0x04003CB3 RID: 15539
		[NonSerialized]
		private static List<BodyPart> parts2 = new List<BodyPart>();

		// Token: 0x04003CB4 RID: 15540
		[NonSerialized]
		private static List<BodyPart> _bodyParts = new List<BodyPart>();

		// Token: 0x04003CB5 RID: 15541
		[NonSerialized]
		private static bool _bodyPartsInUse;

		// Token: 0x04003CB6 RID: 15542
		private static List<BodyPart> AllBodyParts = new List<BodyPart>(16);

		// Token: 0x04003CB7 RID: 15543
		private static List<BodyPart> SomeBodyParts = new List<BodyPart>(4);

		// Token: 0x04003CB8 RID: 15544
		private static List<BodyPart> OtherBodyParts = new List<BodyPart>(4);

		// Token: 0x02001C56 RID: 7254
		[Serializable]
		public class DismemberedPart
		{
			// Token: 0x0600F0D6 RID: 61654 RVA: 0x0054ADF6 File Offset: 0x00548FF6
			public DismemberedPart()
			{
			}

			// Token: 0x0600F0D7 RID: 61655 RVA: 0x0054ADFE File Offset: 0x00548FFE
			public DismemberedPart(BodyPart P, BodyPart ParentPart)
				: this()
			{
				this.Part = P;
				if (ParentPart != null)
				{
					this.ParentID = ParentPart.ID;
				}
			}

			// Token: 0x0600F0D8 RID: 61656 RVA: 0x0054AE1C File Offset: 0x0054901C
			public void Save(SerializationWriter Writer)
			{
				this.Part.Write(Writer);
				Writer.WriteOptimized(this.ParentID);
			}

			// Token: 0x0600F0D9 RID: 61657 RVA: 0x0054AE36 File Offset: 0x00549036
			public void Load(SerializationReader Reader)
			{
				this.Part = new BodyPart();
				this.Part.Read(Reader);
				this.ParentID = Reader.ReadOptimizedInt32();
			}

			// Token: 0x0600F0DA RID: 61658 RVA: 0x0054AE5B File Offset: 0x0054905B
			public bool IsReattachable(Body ParentBody)
			{
				return ParentBody.GetPartByID(this.ParentID, false) != null;
			}

			// Token: 0x0600F0DB RID: 61659 RVA: 0x0054AE70 File Offset: 0x00549070
			public void Reattach(Body ParentBody)
			{
				BodyPart partByID = ParentBody.GetPartByID(this.ParentID, false);
				if (partByID == null)
				{
					throw new Exception("cannot reattach, parent " + this.ParentID.ToString() + " missing");
				}
				ParentBody.DismemberedParts.Remove(this);
				partByID.AddPart(this.Part, this.Part.Position, false);
				ParentBody.RecalculateTypeArmor(this.Part.Type);
			}

			// Token: 0x0600F0DC RID: 61660 RVA: 0x0054AEE3 File Offset: 0x005490E3
			public void CheckRenumberingOnPositionAssignment(BodyPart Parent, int AssignedPosition, BodyPart ExceptPart = null)
			{
				if (Parent.IDMatch(this.ParentID) && this.Part.Position >= AssignedPosition && this.Part != ExceptPart)
				{
					this.Part.Position++;
				}
			}

			// Token: 0x0600F0DD RID: 61661 RVA: 0x0054AF1D File Offset: 0x0054911D
			public bool HasPosition(BodyPart Parent, int Position, BodyPart ExceptPart = null)
			{
				return Parent.IDMatch(this.ParentID) && this.Part.Position == Position && this.Part != ExceptPart;
			}

			// Token: 0x04006FD3 RID: 28627
			public BodyPart Part;

			// Token: 0x04006FD4 RID: 28628
			public int ParentID;
		}
	}
}
