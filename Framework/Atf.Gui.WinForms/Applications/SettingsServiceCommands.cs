//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

using Sce.Atf.Controls;
using Sce.Atf.Controls.PropertyEditing;

namespace Sce.Atf.Applications
{
    /// <summary>
    /// Service that manages user editable settings (preferences) and application settings persistence</summary>
    [Export(typeof(ICommandClient))]
    [Export(typeof(IInitializable))]
    [Export(typeof(SettingsServiceCommands))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SettingsServiceCommands : ICommandClient, IInitializable
    {
        /// <summary>
        /// Constructor</summary>
        public SettingsServiceCommands()
        {
        }


        /// <summary>
        /// Finishes initializing this instance. Is called once, after the app's main window has loaded.
        /// Registers commands and calls LoadSettings().</summary>
        void IInitializable.Initialize()
        {
            // register our menu commands
            if (CommandService != null && RegisterMenuCommands)
            {
                if (m_settingService.AllowUserEdits)
                {
                    CommandService.RegisterCommand(
                        CommandId.EditPreferences,
                        StandardMenu.Edit,
                        StandardCommandGroup.EditPreferences,
                        "Preferences...".Localize("Edit user preferences"),
                        "Edit user preferences".Localize(),
                        this);
                }

                if (m_settingService.AllowUserLoadSave)
                {
                    CommandService.RegisterCommand(
                        CommandId.EditImportExportSettings,
                        StandardMenu.Edit,
                        StandardCommandGroup.EditPreferences,
                        "Load or Save Settings...".Localize(),
                        "User can save or load application settings from files".Localize(),
                        this);
                }
            }

            // load settings as late as possible, so that other components have a chance to
            //  register their settings first
            m_settingService.LoadSettings();
        }


        #region ICommandClient Members

        /// <summary>
        /// Checks if the client can do the command</summary>
        /// <param name="tag">Command</param>
        /// <returns>True if client can do the command</returns>
        public virtual bool CanDoCommand(object tag)
        {
            bool enabled = false;
            if (tag is CommandId)
            {
                switch ((CommandId)tag)
                {
                    case CommandId.EditPreferences:
                    case CommandId.EditImportExportSettings:
                        enabled = true;
                        break;
                }
            }
            return enabled;
        }

        /// <summary>
        /// Does a command</summary>
        /// <param name="tag">Command</param>
        public virtual void DoCommand(object tag)
        {
            if (tag is CommandId)
            {
                switch ((CommandId)tag)
                {
                    case CommandId.EditPreferences:
                        m_settingService.PresentUserSettings(null);
                        break;

                    case CommandId.EditImportExportSettings:
                        if(m_mainWindow != null)
                        {
                            var settingsLoadSaveDialog = new SettingsLoadSaveDialog(m_settingService);
                            settingsLoadSaveDialog.ShowDialog(m_mainWindow.DialogOwner);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Updates command state for given command</summary>
        /// <param name="commandTag">Command</param>
        /// <param name="state">Command state to update</param>
        public virtual void UpdateCommand(object commandTag, CommandState state)
        {
        }

        #endregion


        /// <summary>
        /// Gets whether menu commands should be registered. The default is 'true'. A derived class
        /// can return 'false' to prevent all menu commands from being registered.</summary>
        protected virtual bool RegisterMenuCommands { get { return true; } }

        /// <summary>
        /// Gets or sets the command service used to register commands. May be null.</summary>
        [Import(AllowDefault = true)]
        protected ICommandService CommandService = null;

        [Import(AllowDefault = false)]
        IMainWindow m_mainWindow = null;

        [Import(AllowDefault = false)]
        SettingsService m_settingService = null;
    }
}
