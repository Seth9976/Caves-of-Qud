using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x02000FAA RID: 4010
	[Serializable]
	public class Mill : IPoweredPart
	{
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06009F7E RID: 40830 RVA: 0x0039F597 File Offset: 0x0039D797
		// (set) Token: 0x06009F7D RID: 40829 RVA: 0x0039F587 File Offset: 0x0039D787
		public string Transformations
		{
			get
			{
				return this._Transformations;
			}
			set
			{
				this._Transformations = value;
				this.Transforms = null;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06009F80 RID: 40832 RVA: 0x0039F5AF File Offset: 0x0039D7AF
		// (set) Token: 0x06009F7F RID: 40831 RVA: 0x0039F59F File Offset: 0x0039D79F
		public string TagTransformations
		{
			get
			{
				return this._TagTransformations;
			}
			set
			{
				this._TagTransformations = value;
				this.TagTransforms = null;
			}
		}

		// Token: 0x06009F81 RID: 40833 RVA: 0x0039F5B7 File Offset: 0x0039D7B7
		public Mill()
		{
			this.ChargeUse = 1;
			this.WorksOnInventory = true;
		}

		// Token: 0x06009F82 RID: 40834 RVA: 0x0039F5D0 File Offset: 0x0039D7D0
		public override bool SameAs(IPart p)
		{
			Mill mill = p as Mill;
			return !(mill._Transformations != this._Transformations) && !(mill._TagTransformations != this._TagTransformations) && base.SameAs(p);
		}

		// Token: 0x06009F83 RID: 40835 RVA: 0x0039F615 File Offset: 0x0039D815
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009F84 RID: 40836 RVA: 0x0039F62C File Offset: 0x0039D82C
		private bool ProcessItem(GameObject obj)
		{
			string transform = this.GetTransform(obj);
			if (transform == null)
			{
				return true;
			}
			if (transform == "")
			{
				Butcherable part = obj.GetPart<Butcherable>();
				if (part != null && part.AttemptButcher(this.ParentObject, false, true, true, null, null, null))
				{
					return false;
				}
				if (obj.GetPart<PreservableItem>() != null)
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append(this.ParentObject.The).Append(this.ParentObject.DisplayNameOnly).Append(this.ParentObject.GetVerb("mill", true, false, false, true, false))
						.Append(' ');
					if (Campfire.PerformPreserve(obj, stringBuilder, this.ParentObject, false, true))
					{
						if (base.Visible())
						{
							stringBuilder.Append('.');
							IComponent<GameObject>.AddPlayerMessage(stringBuilder.ToString(), null, true);
						}
						return false;
					}
				}
			}
			else
			{
				StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
				bool flag = base.Visible();
				if (flag)
				{
					stringBuilder2.Append(this.ParentObject.The).Append(this.ParentObject.DisplayNameOnly).Append(this.ParentObject.GetVerb("mill", true, false, false, true, false))
						.Append(' ')
						.Append(obj.a)
						.Append(obj.DisplayNameOnly);
				}
				GameObject gameObject = obj.ReplaceWith(transform);
				if (gameObject != null && flag)
				{
					stringBuilder2.Append(" into").Append(gameObject.a).Append(gameObject.DisplayNameOnly)
						.Append('.');
					IComponent<GameObject>.AddPlayerMessage(stringBuilder2.ToString(), null, true);
				}
			}
			return true;
		}

		// Token: 0x06009F85 RID: 40837 RVA: 0x0039F7B8 File Offset: 0x0039D9B8
		public bool ProcessItems()
		{
			bool flag = this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.ProcessItem), true);
			if (!flag && this.ChargeUse > 0)
			{
				this.ParentObject.UseCharge(this.ChargeUse, false, 0L, true, true, null);
			}
			return flag;
		}

		// Token: 0x06009F86 RID: 40838 RVA: 0x0039F804 File Offset: 0x0039DA04
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ProcessItems();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009F87 RID: 40839 RVA: 0x0039F85C File Offset: 0x0039DA5C
		public string GetTransform(GameObject obj)
		{
			if (this.Transforms == null)
			{
				this.DetermineTransforms();
			}
			string text;
			if (this.Transforms.TryGetValue(obj.Blueprint, out text))
			{
				return text;
			}
			if (this.TagTransforms == null)
			{
				this.DetermineTagTransforms();
			}
			foreach (KeyValuePair<string, string> keyValuePair in this.TagTransforms)
			{
				if (obj.HasTag(keyValuePair.Key))
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06009F88 RID: 40840 RVA: 0x0039F8F8 File Offset: 0x0039DAF8
		private void DetermineTransforms()
		{
			if (string.IsNullOrEmpty(this._Transformations))
			{
				this.Transforms = new Dictionary<string, string>(0);
				return;
			}
			List<string> list = this._Transformations.CachedCommaExpansion();
			this.Transforms = new Dictionary<string, string>(list.Count);
			foreach (string text in list)
			{
				string[] array = text.Split(':', StringSplitOptions.None);
				if (this.Transforms.ContainsKey(array[0]))
				{
					Debug.LogError("duplicate transformation on " + array[0]);
				}
				else if (array.Length > 1)
				{
					this.Transforms.Add(array[0], array[1]);
				}
				else
				{
					this.Transforms.Add(array[0], "");
				}
			}
		}

		// Token: 0x06009F89 RID: 40841 RVA: 0x0039F9D0 File Offset: 0x0039DBD0
		private void DetermineTagTransforms()
		{
			if (string.IsNullOrEmpty(this._TagTransformations))
			{
				this.TagTransforms = new Dictionary<string, string>(0);
				return;
			}
			List<string> list = this._TagTransformations.CachedCommaExpansion();
			this.TagTransforms = new Dictionary<string, string>(list.Count);
			foreach (string text in list)
			{
				string[] array = text.Split(':', StringSplitOptions.None);
				if (this.TagTransforms.ContainsKey(array[0]))
				{
					Debug.LogError("duplicate tag transformation on " + array[0]);
				}
				else if (array.Length > 1)
				{
					this.TagTransforms.Add(array[0], array[1]);
				}
				else
				{
					this.TagTransforms.Add(array[0], "");
				}
			}
		}

		// Token: 0x04004231 RID: 16945
		public string _Transformations;

		// Token: 0x04004232 RID: 16946
		public string _TagTransformations;

		// Token: 0x04004233 RID: 16947
		[NonSerialized]
		private Dictionary<string, string> Transforms;

		// Token: 0x04004234 RID: 16948
		[NonSerialized]
		private Dictionary<string, string> TagTransforms;
	}
}
