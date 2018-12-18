//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.
using System;
using System.Collections;
using System.Collections.Generic;

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
        }

        /// <summary>
        /// This method is for supporting list from outside. the synchronization should be made manually.
        /// </summary>
        /// <param name="sourceList"></param>
        public PinList(IList<PinType> sourceList)
        {
            m_Pins = sourceList;
            UpddateNameMap();
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
            m_pinByName[item.Name] = item;

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

            m_Pins.Add(item);
            m_pinByName[item.Name] = item;

            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            m_Pins.Clear();
            m_pinByName.Clear();
        }

        public bool Contains(PinType item)
        {
            PinType found;
            m_pinByName.TryGetValue(item.Name, out found);
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
        private Dictionary<NameString, PinType> m_pinByName = new Dictionary<NameString, PinType>();
    }
}