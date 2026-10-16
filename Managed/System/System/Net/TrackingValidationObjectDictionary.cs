using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x0200057B RID: 1403
	internal sealed class TrackingValidationObjectDictionary : StringDictionary
	{
		// Token: 0x06002D4C RID: 11596 RVA: 0x0009B1FB File Offset: 0x000993FB
		internal TrackingValidationObjectDictionary(Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators)
		{
			this.IsChanged = false;
			this._validators = validators;
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x0009B214 File Offset: 0x00099414
		private void PersistValue(string key, string value, bool addValue)
		{
			key = key.ToLowerInvariant();
			if (!string.IsNullOrEmpty(value))
			{
				TrackingValidationObjectDictionary.ValidateAndParseValue validateAndParseValue;
				if (this._validators != null && this._validators.TryGetValue(key, out validateAndParseValue))
				{
					object obj = validateAndParseValue(value);
					if (this._internalObjects == null)
					{
						this._internalObjects = new Dictionary<string, object>();
					}
					if (addValue)
					{
						this._internalObjects.Add(key, obj);
						base.Add(key, obj.ToString());
					}
					else
					{
						this._internalObjects[key] = obj;
						base[key] = obj.ToString();
					}
				}
				else if (addValue)
				{
					base.Add(key, value);
				}
				else
				{
					base[key] = value;
				}
				this.IsChanged = true;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x0009B2BE File Offset: 0x000994BE
		// (set) Token: 0x06002D4F RID: 11599 RVA: 0x0009B2C6 File Offset: 0x000994C6
		internal bool IsChanged { get; set; }

		// Token: 0x06002D50 RID: 11600 RVA: 0x0009B2D0 File Offset: 0x000994D0
		internal object InternalGet(string key)
		{
			object obj;
			if (this._internalObjects != null && this._internalObjects.TryGetValue(key, out obj))
			{
				return obj;
			}
			return base[key];
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0009B2FE File Offset: 0x000994FE
		internal void InternalSet(string key, object value)
		{
			if (this._internalObjects == null)
			{
				this._internalObjects = new Dictionary<string, object>();
			}
			this._internalObjects[key] = value;
			base[key] = value.ToString();
			this.IsChanged = true;
		}

		// Token: 0x17000919 RID: 2329
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				this.PersistValue(key, value, false);
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0009B33F File Offset: 0x0009953F
		public override void Add(string key, string value)
		{
			this.PersistValue(key, value, true);
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0009B34A File Offset: 0x0009954A
		public override void Clear()
		{
			if (this._internalObjects != null)
			{
				this._internalObjects.Clear();
			}
			base.Clear();
			this.IsChanged = true;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0009B36C File Offset: 0x0009956C
		public override void Remove(string key)
		{
			if (this._internalObjects != null)
			{
				this._internalObjects.Remove(key);
			}
			base.Remove(key);
			this.IsChanged = true;
		}

		// Token: 0x040018BC RID: 6332
		private readonly Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> _validators;

		// Token: 0x040018BD RID: 6333
		private Dictionary<string, object> _internalObjects;

		// Token: 0x0200057C RID: 1404
		// (Invoke) Token: 0x06002D58 RID: 11608
		internal delegate object ValidateAndParseValue(object valueToValidate);
	}
}
