//Copyright 2018 Kyungkun Ko

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Sce.Atf.Adaptation;

namespace Sce.Atf.Dom
{
    /// <summary>
    ///   
    /// </summary>
    public class DomNodeTypeCollection
    {
        /// <summary>Constructor</summary>
        public DomNodeTypeCollection()
        {
        }

        /// <summary>
        /// Gets a node type</summary>
        /// <param name="name">Qualified name of node type</param>
        /// <returns>Node type or null if unknown name</returns>
        public DomNodeType GetNodeType(string name)
        {
            DomNodeType nodeType;
            m_nodeTypes.TryGetValue(name, out nodeType);
            return nodeType;
        }

        /// <summary>
        /// Gets node types for the given namespace</summary>
        /// <param name="ns">Namespace</param>
        /// <returns>Enumeration of all node types in the given namespace</returns>
        public IEnumerable<DomNodeType> GetNodeTypes(string ns)
        {
            ns += ":";
            foreach (KeyValuePair<string, DomNodeType> kvp in m_nodeTypes)
                if (kvp.Key.StartsWith(ns))
                    yield return kvp.Value;
        }

        /// <summary>
        /// Gets all node types derived from base type</summary>
        /// <param name="baseType">Base type</param>
        /// <remarks>Base type is not returned in the array.</remarks>
        /// <returns>Enumeration of all node types derived from baseType</returns>
        public IEnumerable<DomNodeType> GetNodeTypes(DomNodeType baseType)
        {
            if (baseType == null)
                throw new ArgumentNullException("baseType");

            foreach (DomNodeType type in m_nodeTypes.Values)
                if (type != baseType && baseType.IsAssignableFrom(type))
                    yield return type;
        }

        /// <summary>Gets all node types</summary>
        /// <returns>All node types</returns>
        public IEnumerable<DomNodeType> GetNodeTypes()
        {
            return m_nodeTypes.Values;
        }

        /// <summary>Adds a node type to the ones defined by the schema</summary>
        /// <remarks>If the node type is already defined, it is overwritten</remarks>
        /// <param name="name">Name of node type</param>
        /// <param name="type">New node type</param>
        public void AddNodeType(string name, DomNodeType type)
        {
            m_nodeTypes[name] = type;
        }

        /// <summary>dRemoves the node type with the specified name </summary>
        /// <param name="name">Name of node type</param>
        /// <returns>
        /// true if the node typ is successfully found and removed; otherwise, false.  
        /// This method returns false if name is not found in the defined node type.
        /// </returns>
        public bool RemoveNodeType(string name)
        {
            return m_nodeTypes.Remove(name);
        }

        /// <summary>
        /// Gets the root element with the given name</summary>
        /// <param name="name">Name of element</param>
        /// <returns>Child info for root element or null if unknown name</returns>
        public ChildInfo GetRootElement(string name)
        {
            ChildInfo childInfo;
            m_rootElements.TryGetValue(name, out childInfo);
            return childInfo;
        }

        /// <summary>
        /// Gets root elements for the given namespace</summary>
        /// <param name="ns">Namespace to match</param>
        /// <returns>Enumeration of child info for root elements in the given namespace</returns>
        public IEnumerable<ChildInfo> GetRootElements(string ns)
        {
            ns += ":";
            foreach (KeyValuePair<string, ChildInfo> kvp in m_rootElements)
                if (kvp.Key.StartsWith(ns))
                    yield return kvp.Value;
        }

        /// <summary>
        /// Gets all root elements</summary>
        /// <returns>Enumeration of child info for all root elements</returns>
        public IEnumerable<ChildInfo> GetRootElements()
        {
            return m_rootElements.Values;
        }

        /// <summary>
        /// Gets all root elements</summary>
        /// <returns>Enumeration of child info for all root elements</returns>
        public void AddRootElement(string name, ChildInfo childInfo)
        {
            if (!m_rootElements.ContainsKey(name))
            {
                m_rootElements[name] = childInfo;
            }
        }

        private readonly Dictionary<string, DomNodeType> m_nodeTypes = new Dictionary<string, DomNodeType>();
        private readonly Dictionary<string, ChildInfo> m_rootElements = new Dictionary<string, ChildInfo>();
    }
}
