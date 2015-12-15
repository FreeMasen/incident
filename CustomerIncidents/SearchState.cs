using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Validator;

namespace CustomerIncidents
{
    public partial class SearchState : Form
    {
        private Form1 Main;
        public SearchState(Form1 mainForm)
        {
            InitializeComponent();
            Main = mainForm;
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.techSupport_DataDataSet);

        }

        private bool isValid()
        {
            if (ToolStripValidator.IsPresent(stateToolStripTextBox, "State"))
            {
                return true;
            }
            return false;
        }


        private void fillByStateToolStripButton_Click(object sender, EventArgs e)
        {
            if (isValid())
                try
                {
                    this.customersTableAdapter.FillByState(this.techSupport_DataDataSet.Customers,
                        stateToolStripTextBox.Text);
                }
                catch (System.Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (customersDataGridView.SelectedRows.Count > 0)
            {
                Main.setCustomer((int)customersDataGridView.SelectedRows[0].Cells[0].Value);
                Main.Show();
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Main.Visible = true;
            Close();
        }


        private void SearchState_Load(object sender, EventArgs e)
        {
            stateToolStripTextBox.Focus();
        }
    }
}
