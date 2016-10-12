//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using Sce.Atf.Controls.PropertyEditing;
using Sce.Atf.Dom;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Sce.Atf.Controls
{
    /// <summary>
    /// Property data editor context. wrapper class accessing property value so that it can support undo/redo</summary>
    public class DataEditorPropertyContext
    {
        private readonly PropertyDescriptor m_descriptor;
        private ITransactionContext m_transactionContext;
        private readonly DomNode m_object;

        /// <summary>
        /// Gets the transaction context</summary>
        public ITransactionContext TransactionContext
        {
            get { return m_transactionContext; }
            set { m_transactionContext = value; }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="node">DOM node</param>
        /// <param name="descriptor">property descriptor to bind</param>
        /// <param name="transactionContext">transaction context</param>
        public DataEditorPropertyContext(DomNode node, PropertyDescriptor descriptor, ITransactionContext transactionContext)
        {
            m_object = node;
            m_descriptor = descriptor;
            m_transactionContext = transactionContext;
        }

        /// <summary>
        /// Gets the property value on the last selected object
        /// </summary>
        /// <returns>Property value on the last selected object</returns>
        public virtual object GetValue()
        {
            return m_descriptor.GetValue(m_object);
        }

        /// <summary>
        /// Resets the property value on all selected objects.</summary>
        public virtual void ResetValue()
        {
            m_transactionContext.DoTransaction(delegate
            {
                PropertyUtils.ResetProperty(new object[] { m_object }, m_descriptor);
            },
               string.Format("Reset: {0}".Localize(), m_descriptor.DisplayName));
        }

        /// <summary>
        /// Sets the property value on all selected objects</summary>
        /// <param name="newValue">New property value</param>
        public virtual void SetValue(object newValue)
        {
            m_transactionContext.DoTransaction(delegate
            {
                PropertyUtils.SetProperty(m_object, m_descriptor, newValue);
            },
               string.Format("Edit: {0}".Localize(), m_descriptor.DisplayName));
        }

    }

}

