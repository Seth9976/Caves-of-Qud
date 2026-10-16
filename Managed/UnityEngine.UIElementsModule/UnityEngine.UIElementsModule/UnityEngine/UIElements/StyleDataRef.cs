using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000467 RID: 1127
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct StyleDataRef<T> : IEquatable<StyleDataRef<T>> where T : struct, IEquatable<T>, IStyleDataGroup<T>
	{
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x0008277A File Offset: 0x0008097A
		public int refCount
		{
			get
			{
				StyleDataRef<T>.RefCounted @ref = this.m_Ref;
				return (@ref != null) ? @ref.refCount : 0;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x0008278E File Offset: 0x0008098E
		public uint id
		{
			get
			{
				StyleDataRef<T>.RefCounted @ref = this.m_Ref;
				return (@ref != null) ? @ref.id : 0U;
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000827A4 File Offset: 0x000809A4
		public StyleDataRef<T> Acquire()
		{
			this.m_Ref.Acquire();
			return this;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000827C8 File Offset: 0x000809C8
		public void Release()
		{
			this.m_Ref.Release();
			this.m_Ref = null;
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000827E0 File Offset: 0x000809E0
		public void CopyFrom(StyleDataRef<T> other)
		{
			bool flag = this.m_Ref.refCount == 1;
			if (flag)
			{
				this.m_Ref.value.CopyFrom(ref other.m_Ref.value);
			}
			else
			{
				this.m_Ref.Release();
				this.m_Ref = other.m_Ref;
				this.m_Ref.Acquire();
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0008284C File Offset: 0x00080A4C
		public readonly ref T Read()
		{
			return ref this.m_Ref.value;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0008285C File Offset: 0x00080A5C
		public ref T Write()
		{
			bool flag = this.m_Ref.refCount == 1;
			ref T ptr;
			if (flag)
			{
				ptr = ref this.m_Ref.value;
			}
			else
			{
				StyleDataRef<T>.RefCounted @ref = this.m_Ref;
				this.m_Ref = this.m_Ref.Copy();
				@ref.Release();
				ptr = ref this.m_Ref.value;
			}
			return ref ptr;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000828B8 File Offset: 0x00080AB8
		public static StyleDataRef<T> Create()
		{
			return new StyleDataRef<T>
			{
				m_Ref = new StyleDataRef<T>.RefCounted()
			};
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000828E0 File Offset: 0x00080AE0
		public override int GetHashCode()
		{
			return (this.m_Ref != null) ? this.m_Ref.value.GetHashCode() : 0;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x00082914 File Offset: 0x00080B14
		public static bool operator ==(StyleDataRef<T> lhs, StyleDataRef<T> rhs)
		{
			return lhs.m_Ref == rhs.m_Ref || lhs.m_Ref.value.Equals(rhs.m_Ref.value);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00082958 File Offset: 0x00080B58
		public static bool operator !=(StyleDataRef<T> lhs, StyleDataRef<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x00082974 File Offset: 0x00080B74
		public bool Equals(StyleDataRef<T> other)
		{
			return other == this;
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00082994 File Offset: 0x00080B94
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is StyleDataRef<T>)
			{
				StyleDataRef<T> styleDataRef = (StyleDataRef<T>)obj;
				flag = this.Equals(styleDataRef);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000829C0 File Offset: 0x00080BC0
		public bool ReferenceEquals(StyleDataRef<T> other)
		{
			return this.m_Ref == other.m_Ref;
		}

		// Token: 0x04000FBA RID: 4026
		private StyleDataRef<T>.RefCounted m_Ref;

		// Token: 0x02000468 RID: 1128
		private class RefCounted
		{
			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x06002336 RID: 9014 RVA: 0x000829E0 File Offset: 0x00080BE0
			public int refCount
			{
				get
				{
					return this.m_RefCount;
				}
			}

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x06002337 RID: 9015 RVA: 0x000829E8 File Offset: 0x00080BE8
			public uint id
			{
				get
				{
					return this.m_Id;
				}
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x000829F0 File Offset: 0x00080BF0
			public RefCounted()
			{
				this.m_RefCount = 1;
				this.m_Id = (StyleDataRef<T>.RefCounted.m_NextId += 1U);
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x00082A14 File Offset: 0x00080C14
			public void Acquire()
			{
				this.m_RefCount++;
			}

			// Token: 0x0600233A RID: 9018 RVA: 0x00082A24 File Offset: 0x00080C24
			public void Release()
			{
				this.m_RefCount--;
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x00082A38 File Offset: 0x00080C38
			public StyleDataRef<T>.RefCounted Copy()
			{
				return new StyleDataRef<T>.RefCounted
				{
					value = this.value.Copy()
				};
			}

			// Token: 0x04000FBB RID: 4027
			private static uint m_NextId = 1U;

			// Token: 0x04000FBC RID: 4028
			private int m_RefCount;

			// Token: 0x04000FBD RID: 4029
			private readonly uint m_Id;

			// Token: 0x04000FBE RID: 4030
			public T value;
		}
	}
}
