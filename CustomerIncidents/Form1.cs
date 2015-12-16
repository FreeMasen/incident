using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using Validator;

namespace CustomerIncidents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            customerIDToolStripTextBox.Focus();

        }

        private bool isValid()
        {
            if (ToolStripValidator.IsPresent(customerIDToolStripTextBox, "Customer ID") &&
                ToolStripValidator.IsInt(customerIDToolStripTextBox, "Customer ID"))
            {
                return true;
            }
            return false;
        }

        private void fillByCustomerIDToolStripButton_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                setCustomer(Int32.Parse(customerIDToolStripTextBox.Text));
            }
        }

        public void setCustomer(int customerID)
        {
            
                try
                {
                    this.customersTableAdapter.FillByCustomerID(this.techSupport_DataDataSet.Customers,
                        customerID);
                    

                    if (customersBindingSource.Count == 0)
                    {
                        MessageBox.Show("No customer's with ID, " + customerID + ". Please Try again");
                        customerIDToolStripTextBox.Focus();
                    }
                    else
                    {
                        try
                        {
                        this.incidentsTableAdapter.FillByCust(this.techSupport_DataDataSet.Incidents, customerID);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Data Error");
                    }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            SearchState search = new SearchState(this);
            this.Hide();
            search.ShowDialog();
        }

        public void addIncident(int customerID, string productCode, DateTime dateOpened, string title,
            string description)
        {
            this.incidentsTableAdapter.InsertQuery(customerID, productCode, dateOpened, title, description);
            setCustomer(customerID);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TextBoxValidator.IsPresent(nameTextBox, "Customer Information"))
            {
                NewIncident ni = new NewIncident(this, nameTextBox.Text, Int32.Parse(customerIDTextBox.Text));
                Hide();
                ni.ShowDialog();
            }
        }
    }
}
