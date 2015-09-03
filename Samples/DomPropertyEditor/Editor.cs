//Copyright © 2014 Sony Computer Entertainment America LLC. See License.txt.

using System;
using System.ComponentModel.Composition;

using Sce.Atf;
using Sce.Atf.Dom;
using Sce.Atf.Applications;
using Sce.Atf.Adaptation;

namespace DomPropertyEditorSample
{
    /// <summary>
    /// Creates rootnode and one child of type Orc.</summary>
    [Export(typeof(IInitializable))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Editor : IInitializable
    {

        #region IInitializable Members

        /// <summary>
        /// Finish MEF intialization for the component by creating DomNode tree for application data.</summary>
        void IInitializable.Initialize()
        {
            m_mainform.Shown += (sender, e) =>
                {
                    // create root node.
                    var rootNode = new DomNode(GameSchema.gameType.Type, GameSchema.gameRootElement);
                    rootNode.SetAttribute(GameSchema.gameType.nameAttribute, "Game");

                    // create Orc game object and add it to rootNode.
                    var orc = CreateOrc();
                    rootNode.GetChildList(GameSchema.gameType.gameObjectChild).Add(orc);

                    // add a child Orc.
                    var orcChildList = orc.GetChildList(GameSchema.orcType.orcChild);
                    orcChildList.Add(CreateOrc("Child Orc1"));

                    rootNode.InitializeExtensions();

                    var edContext = rootNode.Cast<GameEditingContext>();
                    edContext.Set(orc);

                    // set active context and select orc object.
                    m_contextRegistry.ActiveContext = rootNode;
                    
                };
        }

        #endregion

        /// <summary>
        /// Helper method to create instance of orcType.</summary>
        private static DomNode CreateOrc(string name = "Orc")
        {
            var orc = new DomNode(GameSchema.orcType.Type);
            orc.SetAttribute(GameSchema.orcType.nameAttribute, name);
            orc.SetAttribute(GameSchema.orcType.TextureRevDateAttribute, DateTime.Now);
            orc.SetAttribute(GameSchema.orcType.resourceFolderAttribute,System.Windows.Forms.Application.StartupPath);
            orc.SetAttribute(GameSchema.orcType.skinColorAttribute, System.Drawing.Color.DarkGray.ToArgb());
            orc.SetAttribute(GameSchema.orcType.healthAttribute, 80);
            var armorList = orc.GetChildList(GameSchema.orcType.armorChild);

            armorList.Add(CreateArmor("Iron breast plate",20,300));

            var clubList = orc.GetChildList(GameSchema.orcType.clubChild);
            clubList.Add(CreateClub(true, 20, 30));

            return orc;
        }

        private static DomNode CreateArmor(string name, int defense, int price)
        {
            var armor = new DomNode(GameSchema.armorType.Type);
            armor.SetAttribute(GameSchema.armorType.nameAttribute, name);
            armor.SetAttribute(GameSchema.armorType.defenseAttribute, defense);
            armor.SetAttribute(GameSchema.armorType.priceAttribute, price);
            return armor;
        }

        private static DomNode CreateClub(bool hasSpikes, int damage, float weight)
        {
            var club = new DomNode(GameSchema.clubType.Type);
            club.SetAttribute(GameSchema.clubType.spikesAttribute, hasSpikes);
            club.SetAttribute(GameSchema.clubType.DamageAttribute, damage);
            club.SetAttribute(GameSchema.clubType.wieghtAttribute, weight);
            return club;
        }
    
    
        [Import(AllowDefault = false)]
        private MainForm m_mainform = null; //initialize to null to avoid incorrect compiler warning

        [Import(AllowDefault = false)]
        private IContextRegistry m_contextRegistry = null; //initialize to null to avoid incorrect compiler warning
    }   
}
