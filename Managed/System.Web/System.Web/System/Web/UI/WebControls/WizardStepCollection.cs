using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in a control that acts as a wizard. This class cannot be inherited.</summary>
	// Token: 0x0200044A RID: 1098
	public sealed class WizardStepCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060032D6 RID: 13014 RVA: 0x00086B34 File Offset: 0x00084D34
		internal WizardStepCollection(Wizard wizard)
		{
			this.list = new ArrayList();
			base..ctor();
			this.wizard = wizard;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control's <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x00086B4E File Offset: 0x00084D4E
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the collection can be modified.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection can be modified; otherwise, false. </returns>
		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection at the specified index location.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> object to retrieve.</param>
		// Token: 0x17001027 RID: 4135
		public WizardStepBase this[int index]
		{
			get
			{
				return (WizardStepBase)this.list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</returns>
		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to the end of the collection.</summary>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to append to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x060032DC RID: 13020 RVA: 0x00086B6E File Offset: 0x00084D6E
		public void Add(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			wizardStep.SetWizard(this.wizard);
			this.list.Add(wizardStep);
			this.wizard.UpdateViews();
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to the collection at the specified index location.</summary>
		/// <param name="index">The index location at which to insert <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</param>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to append to the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x060032DD RID: 13021 RVA: 0x00086BA2 File Offset: 0x00084DA2
		public void AddAt(int index, WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			wizardStep.SetWizard(this.wizard);
			this.list.Insert(index, wizardStep);
			this.wizard.UpdateViews();
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects from the collection.</summary>
		// Token: 0x060032DE RID: 13022 RVA: 0x00086BD6 File Offset: 0x00084DD6
		public void Clear()
		{
			this.list.Clear();
			this.wizard.UpdateViews();
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection contains a specific <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object is found in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection; otherwise, false.</returns>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to find in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="wizardStep" /> is null.</exception>
		// Token: 0x060032DF RID: 13023 RVA: 0x00086BEE File Offset: 0x00084DEE
		public bool Contains(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.list.Contains(wizardStep);
		}

		/// <summary>Copies all the items from a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that receives the items copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x060032E0 RID: 13024 RVA: 0x00086C0A File Offset: 0x00084E0A
		public void CopyTo(WizardStepBase[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" />-implemented object that can be used to iterate through the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" />-implemented object that contains all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</returns>
		// Token: 0x060032E1 RID: 13025 RVA: 0x00086C19 File Offset: 0x00084E19
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines the index value that represents the position of the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object in the collection.</summary>
		/// <returns>If found, the zero-based index of the first occurrence of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in within the current <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection; otherwise, -1.</returns>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to search for in the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x060032E2 RID: 13026 RVA: 0x00086C26 File Offset: 0x00084E26
		public int IndexOf(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			return this.list.IndexOf(wizardStep);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object into the collection at the specified index location.</summary>
		/// <param name="index">The index location at which to insert the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object.</param>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to insert into the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		// Token: 0x060032E3 RID: 13027 RVA: 0x00086C42 File Offset: 0x00084E42
		public void Insert(int index, WizardStepBase wizardStep)
		{
			this.AddAt(index, wizardStep);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection.</summary>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		// Token: 0x060032E4 RID: 13028 RVA: 0x00086C4C File Offset: 0x00084E4C
		public void Remove(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			this.list.Remove(wizardStep);
			this.wizard.UpdateViews();
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object from the collection at the specified location.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to remove.</param>
		// Token: 0x060032E5 RID: 13029 RVA: 0x00086C73 File Offset: 0x00084E73
		public void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
			this.wizard.UpdateViews();
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>true if the collection has a fixed size; otherwise, false.</returns>
		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object at the specified index in the collection.</summary>
		/// <returns>The object to be retrieved.</returns>
		/// <param name="index">The index of the object to get from the collection.</param>
		// Token: 0x1700102A RID: 4138
		object IList.this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Appends the specified object to the end of the collection.</summary>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to append to the end of the collection.</param>
		// Token: 0x060032E9 RID: 13033 RVA: 0x00086CA9 File Offset: 0x00084EA9
		int IList.Add(object ob)
		{
			int num = this.list.Add((WizardStepBase)ob);
			this.wizard.UpdateViews();
			return num;
		}

		/// <summary>Determines whether the collection contains the specified object.</summary>
		/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		// Token: 0x060032EA RID: 13034 RVA: 0x00086CC7 File Offset: 0x00084EC7
		bool IList.Contains(object ob)
		{
			return this.Contains((WizardStepBase)ob);
		}

		/// <summary>Determines the index value that represents the position of the specified object in the collection.</summary>
		/// <returns>The index value of the specified object in the collection.</returns>
		/// <param name="value">The object to search for in the collection.</param>
		// Token: 0x060032EB RID: 13035 RVA: 0x00086CD5 File Offset: 0x00084ED5
		int IList.IndexOf(object ob)
		{
			return this.IndexOf((WizardStepBase)ob);
		}

		/// <summary>Inserts the specified object in the collection at the specified position.</summary>
		/// <param name="index">The index at which to insert the object into the collection.</param>
		/// <param name="value">The object to insert into the collection.</param>
		// Token: 0x060032EC RID: 13036 RVA: 0x00086CE3 File Offset: 0x00084EE3
		void IList.Insert(int index, object ob)
		{
			this.AddAt(index, (WizardStepBase)ob);
		}

		/// <summary>Removes the specified object from the collection.</summary>
		/// <param name="value">The object to remove from the collection.</param>
		// Token: 0x060032ED RID: 13037 RVA: 0x00086CF2 File Offset: 0x00084EF2
		void IList.Remove(object ob)
		{
			this.Remove((WizardStepBase)ob);
		}

		/// <summary>Copies all the items from a <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection to a one-dimensional array, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based <see cref="T:System.Array" /> that receives the items copied from the <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> collection.</param>
		/// <param name="index">The position in the target array at which to start receiving the copied content.</param>
		// Token: 0x060032EE RID: 13038 RVA: 0x00086C0A File Offset: 0x00084E0A
		void ICollection.CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal WizardStepCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001CB7 RID: 7351
		private ArrayList list;

		// Token: 0x04001CB8 RID: 7352
		private Wizard wizard;
	}
}
