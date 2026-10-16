using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a collection container that enables ASP.NET server controls to maintain a list of their child controls.</summary>
	// Token: 0x020001B3 RID: 435
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ControlCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ControlCollection" /> class for the specified parent server control.</summary>
		/// <param name="owner">The ASP.NET server control that the control collection is created for. </param>
		/// <exception cref="T:System.ArgumentNullException">Occurs if the <paramref name="owner" /> parameter is null. </exception>
		// Token: 0x060011A5 RID: 4517 RVA: 0x0002EC65 File Offset: 0x0002CE65
		public ControlCollection(Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentException("owner");
			}
			this.owner = owner;
		}

		/// <summary>Gets the number of server controls in the <see cref="T:System.Web.UI.ControlCollection" /> object for the specified ASP.NET server control.</summary>
		/// <returns>The number of server controls in the <see cref="T:System.Web.UI.ControlCollection" />.</returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x0002EC82 File Offset: 0x0002CE82
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ControlCollection" /> object is read-only.</summary>
		/// <returns>true if the control is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x0002EC8A File Offset: 0x0002CE8A
		public bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.ControlCollection" /> object is synchronized.</summary>
		/// <returns>This property is always false.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a reference to the server control at the specified index location in the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <returns>The reference to the control.</returns>
		/// <param name="index">The location of the server control in the <see cref="T:System.Web.UI.ControlCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to <see cref="P:System.Web.UI.ControlCollection.Count" />. </exception>
		// Token: 0x170005CB RID: 1483
		public virtual Control this[int index]
		{
			get
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.controls[index];
			}
		}

		/// <summary>Gets the ASP.NET server control to which the <see cref="T:System.Web.UI.ControlCollection" /> object belongs.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> to which the <see cref="T:System.Web.UI.ControlCollection" /> belongs.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		protected Control Owner
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection of controls.</summary>
		/// <returns>The <see cref="T:System.Object" /> used to synchronize the collection.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0002ECBC File Offset: 0x0002CEBC
		private void EnsureControls()
		{
			if (this.controls == null)
			{
				this.controls = new Control[5];
				return;
			}
			if (this.controls.Length < this.count + 1)
			{
				int num = ((this.controls.Length == 5) ? 3 : 2);
				Control[] array = new Control[this.controls.Length * num];
				Array.Copy(this.controls, 0, array, 0, this.controls.Length);
				this.controls = array;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection.</summary>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">Thrown if the <paramref name="child" /> parameter does not specify a control. </exception>
		/// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
		// Token: 0x060011AD RID: 4525 RVA: 0x0002ED30 File Offset: 0x0002CF30
		public virtual void Add(Control child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.readOnly)
			{
				throw new HttpException(global::Locale.GetText("Collection is read-only."));
			}
			if (this.owner == child)
			{
				throw new HttpException(global::Locale.GetText("Cannot add collection's owner."));
			}
			this.EnsureControls();
			this.version++;
			Control[] array = this.controls;
			int num = this.count;
			this.count = num + 1;
			array[num] = child;
			this.owner.AddedControl(child, this.count - 1);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.Control" /> object to the collection at the specified index location.</summary>
		/// <param name="index">The location in the array at which to add the child control. </param>
		/// <param name="child">The <see cref="T:System.Web.UI.Control" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter does not specify a control. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the <see cref="P:System.Web.UI.ControlCollection.Count" /> property. </exception>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
		// Token: 0x060011AE RID: 4526 RVA: 0x0002EDC0 File Offset: 0x0002CFC0
		public virtual void AddAt(int index, Control child)
		{
			if (child == null)
			{
				throw new ArgumentNullException();
			}
			if (index < -1 || index > this.count)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (this.readOnly)
			{
				throw new HttpException(global::Locale.GetText("Collection is read-only."));
			}
			if (this.owner == child)
			{
				throw new HttpException(global::Locale.GetText("Cannot add collection's owner."));
			}
			if (index == -1)
			{
				this.Add(child);
				return;
			}
			this.EnsureControls();
			this.version++;
			Array.Copy(this.controls, index, this.controls, index + 1, this.count - index);
			this.count++;
			this.controls[index] = child;
			this.owner.AddedControl(child, index);
		}

		/// <summary>Removes all controls from the current server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		// Token: 0x060011AF RID: 4527 RVA: 0x0002EE7C File Offset: 0x0002D07C
		public virtual void Clear()
		{
			if (this.controls == null)
			{
				return;
			}
			this.version++;
			for (int i = 0; i < this.count; i++)
			{
				this.owner.RemovedControl(this.controls[i]);
			}
			this.count = 0;
			if (this.owner != null)
			{
				this.owner.ResetChildNames();
			}
		}

		/// <summary>Determines whether the specified server control is in the parent server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <returns>true if the specified server control exists in the collection; otherwise, false.</returns>
		/// <param name="c">The server control to search for in the collection. </param>
		// Token: 0x060011B0 RID: 4528 RVA: 0x0002EEDE File Offset: 0x0002D0DE
		public virtual bool Contains(Control c)
		{
			return this.controls != null && Array.IndexOf<Control>(this.controls, c) != -1;
		}

		/// <summary>Copies the child controls stored in the <see cref="T:System.Web.UI.ControlCollection" /> object to an <see cref="T:System.Array" /> object, beginning at the specified index location in the <see cref="T:System.Array" />.</summary>
		/// <param name="array">The <see cref="T:System.Array" /> to copy the child controls to. </param>
		/// <param name="index">The zero-based relative index in <paramref name="array" /> where copying begins. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="array" /> is not null and not one-dimensional. </exception>
		// Token: 0x060011B1 RID: 4529 RVA: 0x0002EEFC File Offset: 0x0002D0FC
		public virtual void CopyTo(Array array, int index)
		{
			if (this.controls == null)
			{
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index + this.count > array.GetLowerBound(0) + array.GetLength(0))
			{
				throw new ArgumentException();
			}
			if (array.Rank > 1)
			{
				throw new RankException(global::Locale.GetText("Only single dimension arrays are supported."));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", global::Locale.GetText("Value has to be >= 0."));
			}
			for (int i = 0; i < this.count; i++)
			{
				array.SetValue(this.controls[i], i + index);
			}
		}

		/// <summary>Retrieves an enumerator that can iterate through the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <returns>The enumerator to iterate through the collection.</returns>
		// Token: 0x060011B2 RID: 4530 RVA: 0x0002EF93 File Offset: 0x0002D193
		public virtual IEnumerator GetEnumerator()
		{
			return new ControlCollection.SimpleEnumerator(this);
		}

		/// <summary>Retrieves the index of a specified <see cref="T:System.Web.UI.Control" /> object in the collection.</summary>
		/// <returns>The index of the specified server control. If the server control is not currently a member of the collection, it returns -1.</returns>
		/// <param name="value">The <see cref="T:System.Web.UI.Control" /> for which the index is returned. </param>
		// Token: 0x060011B3 RID: 4531 RVA: 0x0002EF9B File Offset: 0x0002D19B
		public virtual int IndexOf(Control value)
		{
			if (this.controls == null || value == null)
			{
				return -1;
			}
			return Array.IndexOf<Control>(this.controls, value);
		}

		/// <summary>Removes the specified server control from the parent server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="value">The server control to be removed. </param>
		// Token: 0x060011B4 RID: 4532 RVA: 0x0002EFB8 File Offset: 0x0002D1B8
		public virtual void Remove(Control value)
		{
			int num = this.IndexOf(value);
			if (num == -1)
			{
				return;
			}
			this.RemoveAt(num);
		}

		/// <summary>Removes a child control, at the specified index location, from the <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="index">The ordinal index of the server control to be removed from the collection. </param>
		/// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection" /> is read-only. </exception>
		// Token: 0x060011B5 RID: 4533 RVA: 0x0002EFDC File Offset: 0x0002D1DC
		public virtual void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new HttpException();
			}
			this.version++;
			Control control = this.controls[index];
			this.count--;
			if (this.count - index > 0)
			{
				Array.Copy(this.controls, index + 1, this.controls, index, this.count - index);
			}
			this.controls[this.count] = null;
			this.owner.RemovedControl(control);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0002F05E File Offset: 0x0002D25E
		internal void SetReadonly(bool readOnly)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x040013F9 RID: 5113
		private Control owner;

		// Token: 0x040013FA RID: 5114
		private Control[] controls;

		// Token: 0x040013FB RID: 5115
		private int version;

		// Token: 0x040013FC RID: 5116
		private int count;

		// Token: 0x040013FD RID: 5117
		private bool readOnly;

		// Token: 0x020001B4 RID: 436
		private sealed class SimpleEnumerator : IEnumerator
		{
			// Token: 0x060011B7 RID: 4535 RVA: 0x0002F067 File Offset: 0x0002D267
			public SimpleEnumerator(ControlCollection coll)
			{
				this.coll = coll;
				this.index = -1;
				this.version = coll.version;
			}

			// Token: 0x060011B8 RID: 4536 RVA: 0x0002F08C File Offset: 0x0002D28C
			public bool MoveNext()
			{
				if (this.version != this.coll.version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				if (this.index >= -1)
				{
					int num = this.index + 1;
					this.index = num;
					if (num < this.coll.Count)
					{
						this.currentElement = this.coll[this.index];
						return true;
					}
				}
				this.index = -2;
				return false;
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0002F100 File Offset: 0x0002D300
			public object Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException((this.index == -1) ? "Enumerator not started" : "Enumerator ended");
					}
					return this.currentElement;
				}
			}

			// Token: 0x060011BA RID: 4538 RVA: 0x0002F12C File Offset: 0x0002D32C
			public void Reset()
			{
				if (this.version != this.coll.version)
				{
					throw new InvalidOperationException("List has changed.");
				}
				this.index = -1;
			}

			// Token: 0x040013FE RID: 5118
			private ControlCollection coll;

			// Token: 0x040013FF RID: 5119
			private int index;

			// Token: 0x04001400 RID: 5120
			private int version;

			// Token: 0x04001401 RID: 5121
			private object currentElement;
		}
	}
}
