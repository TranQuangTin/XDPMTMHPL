﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win_Tour
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel1.Controls.Clear();
            Tour1 dms = new Tour1();
            dms.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(dms);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel1.Controls.Clear();
            UCDiaDiem dms = new UCDiaDiem();
            dms.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(dms);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel1.Controls.Clear();
            UCGia dms = new UCGia();
            dms.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(dms);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel1.Controls.Clear();
            UCDDTQ dms = new UCDDTQ();
            dms.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(dms);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel1.Controls.Clear();
            UCThemTour dms = new UCThemTour();
            dms.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(dms);
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            panel1.Controls.Clear();
            UCThongKe dms = new UCThongKe();
            dms.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(dms);
        }
    }
}
