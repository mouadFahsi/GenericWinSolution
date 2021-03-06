﻿using App;
using App.Gwin;
using App.Gwin.Application.BAL.GwinApplication;
using App.Gwin.Application.Presentation.EntityManagement;
using App.Gwin.Application.Presentation.MainForm;
using App.Gwin.Entities;
using App.Gwin.Entities.Application;
using App.Gwin.Entities.Authentication;
using SplashScreen;
using System;
using System.Windows.Forms;

namespace GenericWinForm.Demo
{
    public partial class FormMenuApplication : FormApplication
    {
        public FormMenuApplication()
        {
           // GwinApp.Loading_Status("Creating Application Menu...");
            InitializeComponent();
         
        }

        private void FormMenuApplication_Load(object sender, EventArgs e)
        {
            // Application User
            User user = new User();
            user.Language = GwinApp.Languages.ar;

            // Start Gwin Application
            GwinApp.Start(typeof(ModelContext), typeof(BaseBLO<>),this, user);
            
        }

        ///// <summary>
        ///// Reload the form after language change
        ///// </summary>
        //public override void Reload()
        //{
        //    base.Reload();
        //    InitializeComponent();
        //}


    }
}
