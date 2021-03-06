﻿using App.Gwin.Application.BAL;
using App.Gwin.Application.BAL.GwinApplication;
using App.Gwin.Application.Presentation.EntityManagement;
using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.Application;
using App.Gwin.Exceptions.Gwin;
using App.Gwin.ModelData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App.Gwin.Application.Presentation.MainForm
{
    /// <summary>
    /// Application Menu Configuration
    /// </summary>
    public class CreateApplicationMenu
    {
        #region Params
        private IApplicationMenu formMenu;
        private EntityManagementCreator ShowManagementForm { set; get; }
        #endregion
        #region Variables
        private MenuStrip menuStrip;
        private Dictionary<string, Type> MenuItems { set; get; }
        private IBaseBLO Service { get;  set; }
        #endregion

        public CreateApplicationMenu(IApplicationMenu formMenu)
        {
            this.formMenu = formMenu;
            this.menuStrip = formMenu.getMenuStrip();
            MenuItems = new Dictionary<string, Type>();
            this.ShowManagementForm = new EntityManagementCreator(GwinApp.Instance.TypeDBContext,formMenu);
            this.Service = BaseEntityBLO<BaseEntity>
                .CreateBLOInstanceByTypeEntity(typeof(MenuItemApplication),GwinApp.Instance.TypeBaseBLO, this.ShowManagementForm.CreateContext());
            this.CreateMenu();
        }


        /// <summary>
        /// Create Menu from ModelConfig
        /// </summary>
        private void CreateMenu()
        {
            // Create Parent Menu from ManuItemApplication Table
            foreach (MenuItemApplication menuItemApplication in this.Service.GetAll())
            {
                // ToolStripMenu
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Name = "toolStripMenuItem" + menuItemApplication.Code;
                toolStripMenuItem.Size = new System.Drawing.Size(82, 20);
                if(menuItemApplication.Title.Current != string.Empty)
                {
                    toolStripMenuItem.Text = menuItemApplication.Title.Current;
                }
               
                else
                {
                    toolStripMenuItem.Text = menuItemApplication.Code;
                }
                this.menuStrip.Items.Add(toolStripMenuItem);
            }


            // Create MenuItems from ModelCondiguration Entities
            Dictionary<Type, MenuAttribute> MenuAttributes_And_Types = new ModelConfiguration().Get_All_Type_And_MenuAttributes();
            foreach (var menuAttributes_And_Types in MenuAttributes_And_Types)
            {

                ConfigEntity configEntity = ConfigEntity.CreateConfigEntity(menuAttributes_And_Types.Key);

                // ToolStripMenu
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
                toolStripMenuItem.Name = "toolStripMenuItem" + configEntity.Menu.Title;
                toolStripMenuItem.Size = new System.Drawing.Size(82, 20);
                toolStripMenuItem.Text = configEntity.Menu.Title;
                toolStripMenuItem.Click += ToolStripMenuItem_Click;
                MenuItems.Add(toolStripMenuItem.Name, menuAttributes_And_Types.Key);

                // Find groupe
                if (configEntity.Menu.Group != null) {
                    string toolStripMenuItem_key = "toolStripMenuItem" + configEntity.Menu.Group;
                    ToolStripItem GroupeToolStripItem = this.menuStrip.Items.Find(toolStripMenuItem_key, true).SingleOrDefault();
                    ToolStripMenuItem GroupeToolStripMenuItem = GroupeToolStripItem as ToolStripMenuItem;
                    if (GroupeToolStripMenuItem != null)
                        GroupeToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
                    else
                        throw new GwinException("toolStripMenuItem :" + toolStripMenuItem_key + " not exist in Menu Application");
                }
                else
                {
                    this.menuStrip.Items.Add(toolStripMenuItem);
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            this.ShowManagementForm.ShowManagementForm(MenuItems[item.Name]);
        }
    }
}