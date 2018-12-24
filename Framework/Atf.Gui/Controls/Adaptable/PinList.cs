//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sce.Atf.Dom;


namespace Sce.Atf.Controls.Adaptable.Graphs
{
    /// <summary>Pin list with name map</summary>
    public class PinList<PinType> : IList<PinType>
        where PinType : ICircuitPin
    {
        public PinList()
        {
            m_Pins = new List<PinType>();
            m_pinByName = new Dictionary<NameString, PinType>();
        }

        /// <summary>
        /// This method is for supporting list from outside. the synchronization should be made manually.
        /// </summary>
        /// <param name="sourceList"></param>
        public PinList(IList<PinType> sourceList, bool manualUpdate = false)
        {
            m_Pins = sourceList;
            m_pinByName = new Dictionary<NameString, PinType>();
            if(!manualUpdate)
                UpddateNameMap();
        }

        /// <summary>
        /// This method is for supporting list from outside. the synchronization should be made manually.
        /// </summary>
        /// <param name="sourceList"></param>
        internal PinList(IList<PinType> sourceList, IDictionary<NameString, PinType> sourceMap)
        {
            m_Pins = sourceList;
            m_pinByName = sourceMap;
            System.Diagnostics.Debug.Assert(m_Pins.Count == m_pinByName.Count);
        }

        public int Count
        {
            get { return m_pinByName.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public PinType this[int index]
        {
            get
            {
                return m_Pins[index];
            }

            set
            {
                m_Pins[index] = value;
                if (value != null)
                {
                    m_Pins[index] = value;
                    m_pinByName[value.Name] = value;
                }
            }
        }

        public PinType this[NameString pinName]
        {
            get
            {
                return m_pinByName[pinName];
            }

            set
            {
                PinType found;
                m_pinByName.TryGetValue(value.Name, out found);
                if (found != null)
                {
                    return;
                }

                m_Pins.Add(value);
                m_pinByName[value.Name] = value;
            }
        }

        public PinType GetPinByName(NameString pinName)
        {
            PinType found;
            m_pinByName.TryGetValue(pinName, out found);
            return found;
        }

        public int IndexOf(NameString pinName)
        {
            for(int iItem = 0; iItem < m_Pins.Count; iItem++)
            {
                if (m_Pins[iItem].Name == pinName)
                    return iItem;
            }

            return -1;
        }

        public int IndexOf(PinType item)
        {
            return m_Pins.IndexOf(item);
        }

        public void Insert(int index, PinType item)
        {
            PinType found;
            m_pinByName.TryGetValue(item.Name, out found);
            if(found != null)
            {
                if (found.Equals(item))
                    return;

                System.Diagnostics.Debug.Assert(false, "Duplicated item");
            }

            m_Pins.Insert(index, item);

            // Fix up index
            for (int iPin = index; iPin < m_Pins.Count; iPin++)
                m_Pins[iPin].Index = iPin;

            m_pinByName[item.Name] = item;
            System.Diagnostics.Debug.Assert(m_Pins.Count == m_pinByName.Count);
        }

        public void RemoveAt(int index)
        {
            var pin = m_Pins[index];
            m_Pins.RemoveAt(index);
            if (pin == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }


            // Fix up index
            for (int iPin = index; iPin < m_Pins.Count; iPin++)
                m_Pins[iPin].Index = iPin;

            m_pinByName.Remove(pin.Name);
        }

        public void Add(PinType item)
        {
            PinType found;
            m_pinByName.TryGetValue(item.Name, out found);
            if (found != null)
            {
                if (!found.Equals(item))
                    throw new Exception("Duplicated name key");

                return;
            }

            item.Index = m_Pins.Count;
            m_Pins.Add(item);
            m_pinByName[item.Name] = item;
            System.Diagnostics.Debug.Assert(m_Pins.Count == m_pinByName.Count);
        }

        public void Clear()
        {
            m_Pins.Clear();
            m_pinByName.Clear();
        }

        public bool Contains(PinType item)
        {
            PinType found;
            if (m_pinByName.Count == m_Pins.Count) // In group pin,It need to check whether it's input or output pin, and call this function which will be before the group is fully is initialized.
                m_pinByName.TryGetValue(item.Name, out found);
            else
                return m_Pins.Contains(item);
            return found != null;
        }

        public void CopyTo(PinType[] array, int arrayIndex)
        {
            m_Pins.CopyTo(array, arrayIndex);
        }

        public bool Remove(PinType item)
        {
            PinType found;
            m_pinByName.TryGetValue(item.Name, out found);
            if (found == null)
                return true;

            m_pinByName.Remove(item.Name);
            m_Pins.Remove(item);

            // Fix up index
            for (int iPin = 0; iPin < m_Pins.Count; iPin++)
                m_Pins[iPin].Index = iPin;

            return true;
        }

        public IEnumerator<PinType> GetEnumerator()
        {
            return m_Pins.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Pins.GetEnumerator();
        }
        public PinList<TResult> Cast<TResult>() where TResult : ICircuitPin
        {
            return new PinList<TResult>(m_Pins.Cast<TResult>().ToList(), new DictionaryAdapter<NameString, TResult, PinType>(m_pinByName));
        }

        // Update name map 
        public void UpddateNameMap()
        {
            m_pinByName.Clear();
            foreach(var itPin in m_Pins)
            {
                m_pinByName.Add(itPin.Name, itPin);
            }
        }

        private IList<PinType> m_Pins;
        private IDictionary<NameString, PinType> m_pinByName;
    }



}