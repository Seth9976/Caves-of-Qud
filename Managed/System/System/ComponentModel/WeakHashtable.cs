using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200043B RID: 1083
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class WeakHashtable : Hashtable
	{
		// Token: 0x06002375 RID: 9077 RVA: 0x00080D5E File Offset: 0x0007EF5E
		internal WeakHashtable()
			: base(WeakHashtable._comparer)
		{
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00080D6B File Offset: 0x0007EF6B
		public override void Clear()
		{
			base.Clear();
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x00080D73 File Offset: 0x0007EF73
		public override void Remove(object key)
		{
			base.Remove(key);
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00080D7C File Offset: 0x0007EF7C
		public void SetWeak(object key, object value)
		{
			this.ScavengeKeys();
			this[new WeakHashtable.EqualityWeakReference(key)] = value;
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00080D94 File Offset: 0x0007EF94
		private void ScavengeKeys()
		{
			int count = this.Count;
			if (count == 0)
			{
				return;
			}
			if (this._lastHashCount == 0)
			{
				this._lastHashCount = count;
				return;
			}
			long totalMemory = GC.GetTotalMemory(false);
			if (this._lastGlobalMem == 0L)
			{
				this._lastGlobalMem = totalMemory;
				return;
			}
			float num = (float)(totalMemory - this._lastGlobalMem) / (float)this._lastGlobalMem;
			float num2 = (float)(count - this._lastHashCount) / (float)this._lastHashCount;
			if (num < 0f && num2 >= 0f)
			{
				ArrayList arrayList = null;
				foreach (object obj in this.Keys)
				{
					WeakReference weakReference = obj as WeakReference;
					if (weakReference != null && !weakReference.IsAlive)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(weakReference);
					}
				}
				if (arrayList != null)
				{
					foreach (object obj2 in arrayList)
					{
						this.Remove(obj2);
					}
				}
			}
			this._lastGlobalMem = totalMemory;
			this._lastHashCount = count;
		}

		// Token: 0x040010C8 RID: 4296
		private static IEqualityComparer _comparer = new WeakHashtable.WeakKeyComparer();

		// Token: 0x040010C9 RID: 4297
		private long _lastGlobalMem;

		// Token: 0x040010CA RID: 4298
		private int _lastHashCount;

		// Token: 0x0200043C RID: 1084
		private class WeakKeyComparer : IEqualityComparer
		{
			// Token: 0x0600237B RID: 9083 RVA: 0x00080EE0 File Offset: 0x0007F0E0
			bool IEqualityComparer.Equals(object x, object y)
			{
				if (x == null)
				{
					return y == null;
				}
				if (y != null && x.GetHashCode() == y.GetHashCode())
				{
					WeakReference weakReference = x as WeakReference;
					WeakReference weakReference2 = y as WeakReference;
					if (weakReference != null)
					{
						if (!weakReference.IsAlive)
						{
							return false;
						}
						x = weakReference.Target;
					}
					if (weakReference2 != null)
					{
						if (!weakReference2.IsAlive)
						{
							return false;
						}
						y = weakReference2.Target;
					}
					return x == y;
				}
				return false;
			}

			// Token: 0x0600237C RID: 9084 RVA: 0x00080F44 File Offset: 0x0007F144
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x0200043D RID: 1085
		private sealed class EqualityWeakReference : WeakReference
		{
			// Token: 0x0600237E RID: 9086 RVA: 0x00080F4C File Offset: 0x0007F14C
			internal EqualityWeakReference(object o)
				: base(o)
			{
				this._hashCode = o.GetHashCode();
			}

			// Token: 0x0600237F RID: 9087 RVA: 0x00080F61 File Offset: 0x0007F161
			public override bool Equals(object o)
			{
				return o != null && o.GetHashCode() == this._hashCode && (o == this || (this.IsAlive && o == this.Target));
			}

			// Token: 0x06002380 RID: 9088 RVA: 0x00080F90 File Offset: 0x0007F190
			public override int GetHashCode()
			{
				return this._hashCode;
			}

			// Token: 0x040010CB RID: 4299
			private int _hashCode;
		}
	}
}
