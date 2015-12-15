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
using Validator;

namespace CustomerIncidents
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.techSupport_DataDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            customerIDToolStripTextBox.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                
            }
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

        private void customerIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (customerIDTextBox.Text == "")
            {
                incidentsDataGridView.DataSource = null;
            }
            else
            {
                incidentsTableAdapter.FillByCust(this.techSupport_DataDataSet.Incidents,
                    Int32.Parse(customerIDTextBox.Text));
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
            //    DataRowBuilder drb = new DataRowBuilder(this.incidentsDataGridView, this.incidentsDataGridView.RowCount + 1);

            //    TechSupport_DataDataSet.IncidentsRow row = new TechSupport_DataDataSet.IncidentsRow();
            //    row.CustomerID = customerID;
            //    row.ProductCode = productCode;
            //    row.DateOpened = dateOpened;
            //    row.Title = title;
            //    row.Description = description;
            //    techSupport_DataDataSet.Incidents.AddIncidentsRow();
            //int rows = incidentsTableAdapter.GetData().Count;
            //incidentsBindingSource.AddNew();

            this.incidentsTableAdapter.InsertQuery(customerID, productCode, dateOpened, title, description);
            this.incidentsTableAdapter.Insert(customerID, productCode, null, dateOpened, null, title, description);
            //rows = incidentsTableAdapter.GetData().Count;
            //incidentsBindingSource.EndEdit();
            //tableAdapterManager.UpdateAll(techSupport_DataDataSet);
            //SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\TechSupport_Data.MDF;Integrated Security=True;");
            //SqlCommand insert = new SqlCommand();
            //insert.CommandText =
            //    "INSERT INTO[dbo].[Incidents] ([CustomerID], [ProductCode], [DateOpened], [Title], [Description]) " +
            //    "VALUES('" + customerID + "','" + productCode + "','" + dateOpened + "','" + title + "','" + description +
            //    "')";
            //insert.Connection = sql;
            //insert.Parameters.Add("@CustomerID", SqlDbType.Int);
            //insert.Parameters.Add("@ProductCode", SqlDbType.VarChar);
            //insert.Parameters.Add("@DateOpened", SqlDbType.Date);
            //insert.Parameters.Add("@Title", SqlDbType.VarChar);
            //insert.Parameters.Add("@Description", SqlDbType.VarChar);

            //insert.Parameters["@CustomerID"].Value = customerID;
            //insert.Parameters["@ProductCode"].Value = productCode;
            //insert.Parameters["@DateOpened"].Value = dateOpened;
            //insert.Parameters["@Title"].Value = title;
            //insert.Parameters["@Description"].Value = description;

            //try
            //{
            //    sql.Open();
            //    insert.ExecuteNonQuery();
            //    sql.Close();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (TextBoxValidator.IsPresent(nameTextBox, "Customer Information"))
            {
                NewIncident ni = new NewIncident(this, nameTextBox.Text);
                Hide();
                ni.ShowDialog();
            }
        }
    }
}
