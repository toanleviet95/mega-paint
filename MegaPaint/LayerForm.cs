using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MegaPaint
{
    public partial class frmLayerForm : Form
    {
        public List<LayerEdit> layerList = new List<LayerEdit>();
        public frmLayerForm(Layers _layers)
        {
            InitializeComponent();
            for (int i = 0; i < _layers.Count; i++)
            {
                LayerEdit le = new LayerEdit();
                le.LayerName = _layers[i].LayerName;
                le.LayerVisible = _layers[i].IsVisible;
                le.LayerActive = _layers[i].IsActive;
                layerList.Add(le);
            }
            SetDataGrid();
        }

        private void SetDataGrid()
        {
            dgvLayers.DataSource = layerList;
            dgvLayers.Columns[4].Visible = false;
            dgvLayers.Columns[0].HeaderText = "Layer Name";
            dgvLayers.Columns[1].HeaderText = "Active";
            dgvLayers.Columns[2].HeaderText = "Hide/Show";
            dgvLayers.Columns[3].HeaderText = "Delete";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LayerEdit le = new LayerEdit();
            le.LayerName = "New Layer " + layerList.Count.ToString();
            le.LayerNew = true;
            layerList.Add(le);
            dgvLayers.DataSource = null;
            SetDataGrid();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int active = 0;
            for (int i = 0; i < layerList.Count; i++)
                if (layerList[i].LayerActive)
                    active++;
            if (active > 1)
                MessageBox.Show("Only ONE layer is active at a time!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                Close();
        }

        private void frmLayerForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }
    }
}
