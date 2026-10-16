using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x0200057A RID: 1402
	internal sealed class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x06002D43 RID: 11587 RVA: 0x0009B13C File Offset: 0x0009933C
		internal TrackingStringDictionary()
			: this(false)
		{
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0009B145 File Offset: 0x00099345
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this._isReadOnly = isReadOnly;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x0009B154 File Offset: 0x00099354
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x0009B15C File Offset: 0x0009935C
		internal bool IsChanged
		{
			get
			{
				return this._isChanged;
			}
			set
			{
				this._isChanged = value;
			}
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0009B165 File Offset: 0x00099365
		public override void Add(string key, string value)
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Add(key, value);
			this._isChanged = true;
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0009B189 File Offset: 0x00099389
		public override void Clear()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Clear();
			this._isChanged = true;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0009B1AB File Offset: 0x000993AB
		public override void Remove(string key)
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Remove(key);
			this._isChanged = true;
		}

		// Token: 0x17000917 RID: 2327
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (this._isReadOnly)
				{
					throw new InvalidOperationException("The collection is read-only.");
				}
				base[key] = value;
				this._isChanged = true;
			}
		}

		// Token: 0x040018BA RID: 6330
		private readonly bool _isReadOnly;

		// Token: 0x040018BB RID: 6331
		private bool _isChanged;
	}
}
