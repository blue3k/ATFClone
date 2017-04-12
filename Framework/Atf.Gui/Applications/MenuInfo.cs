//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Container for menu information</summary>
    public class MenuInfo
    {
        /// <summary>
        /// Unique menu tag, identifying the menu</summary>
        public readonly object MenuTag;

        /// <summary>
        /// Menu text</summary>
        public readonly string MenuText;

        /// <summary>
        /// Menu description</summary>
        public readonly string Description;

        /// <summary>
        /// Gets or sets the ICommandService to which this menu info is registered</summary>
        public ICommandService CommandService 
        { 
            get { return m_commandService; } 
            set
            {
                if (m_commandService != null)
                    throw new InvalidOperationException("MenuInfo already has been registered");
                m_commandService = value;
            }
        }

        /// <summary>
        /// Constructor with parameters</summary>
        /// <param name="menuTag">Menu tag object</param>
        /// <param name="menuText">Menu text visible in UI</param>
        /// <param name="description">Menu description</param>
        public MenuInfo(
            object menuTag,
            string menuText,
            string description)
        {
            MenuTag = menuTag;
            MenuText = menuText;
            Description = description;
        }

        /// <summary>
        /// Number of commands in this menu</summary>
        public int Commands;

        private ICommandService m_commandService;
    }
}
