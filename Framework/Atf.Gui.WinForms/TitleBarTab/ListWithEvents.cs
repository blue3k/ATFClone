
using System.Linq;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections.Specialized;

namespace Sce.Atf.Gui.TitleBarTab
{
	/// <summary>Represents a strongly typed list of objects with events.</summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	public class ListWithEvents<T> : ObservableCollection<T>
        where T : class
	{

        /// <summary>Occurs whenever the list's content is modified.</summary>
        public event EventHandler<NotifyCollectionChangedEventArgs> CollectionModified;
        
		/// <summary>Initializes a new instance of the <see cref="ListWithEvents{T}" /> class that is empty and has the default initial capacity.</summary>
		public ListWithEvents()
		{
            CollectionChanged += NotifyCollectionChangedEventHandler;
        }

        void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (m_suppressEvents) return;

            if(CollectionModified != null)
                CollectionModified(this, e);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListWithEvents{T}" /> class that contains elements copied from the specified collection and has
        /// sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new list.</param>
        /// <exception cref="ArgumentNullException">The collection is null.</exception>
        public ListWithEvents(IEnumerable<T> collection)
			: base(collection)
		{
		}

		/// <summary>Gets whether the events are currently being suppressed.</summary>
		protected bool EventsSuppressed
		{
			get
			{
				return m_suppressEvents;
			}
		}

        public int FindIndex(Predicate<T> checkAction)
        {
            for(int iIndex = 0; iIndex < Count; iIndex++)
            {
                if (checkAction(this[iIndex])) return iIndex;
            }

            return -1;
        }

        /// <summary>Stops raising events until <see cref="ResumeEvents" /> is called.</summary>
        public void SuppressEvents()
		{
			m_suppressEvents = true;
		}

		/// <summary>Resumes raising events after <see cref="SuppressEvents" /> call.</summary>
		public void ResumeEvents()
		{
			m_suppressEvents = false;
		}



        /// <summary>Flag indicating whether events are being suppressed during an operation.</summary>
        private bool m_suppressEvents;

    }
}