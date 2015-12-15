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
    public partial class NewIncident : Form
    {
        public NewIncident(Form1 main, string name)
        {
            InitializeComponent();
            Main = main;
            Name = Name;
        }

        private Form1 Main;
        private string Name;
        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();

        }

        private void productsBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.productsBindingSource.EndEdit();

        }

        private void NewIncident_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'techSupport_DataDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.techSupport_DataDataSet.Products);
            txtID.Text = Main.customerIDToolStripTextBox.Text;
            txtName.Text = Name;

        }

        private bool isValid()
        {
            if (TextBoxValidator.IsPresent(txtTitle, "Title") &&
                TextBoxValidator.IsPresent(txtDescription, "Description") && cboProduct.SelectedIndex > -1)
            {
                return true;
            }
            return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Main.addIncident(Int32.Parse(txtID.Text), cboProduct.SelectedValue.ToString(),DateTime.Now, txtTitle.Text,txtDescription.Text);
            Main.Show();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Main.Show();
            Close();
        }
    }
}
