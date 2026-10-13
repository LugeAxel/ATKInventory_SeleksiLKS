using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATKInventory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aTKInventoryDBDataSet.items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.aTKInventoryDBDataSet.items);
            Clear();

        }


        private void Clear()
        {
            txtCategory.MaxLength = 50;
            txtItemCode.MaxLength = 20;
            txtItemName.MaxLength = 100;
            numPrice.Minimum = 0;
            numStock.Minimum = 0;
            txtItemCode.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtCategory.Text = string.Empty;
            numPrice.Value = 0;
            numStock.Value = 0;
            itemsTableAdapter.Fill(aTKInventoryDBDataSet.items);
            itemsBindingSource.ResetBindings(false);
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = false;
        }

        private bool ValidateInput()
        {
            
            if (txtCategory.TextLength == txtCategory.MaxLength)
            {
                MessageBox.Show("Maximum Length Surpassed for Category");
                return false;
            }

            if (txtItemCode.TextLength == txtItemCode.MaxLength)
            {
                MessageBox.Show("Maximum Length Surpassed for Item Code");
                return false;
            }

            if (txtItemName.TextLength == txtItemName.MaxLength) 
            {
                MessageBox.Show("Maximum Length Surpassed for Item Name");
                return false;
            }
            if (numPrice.Value < numPrice.Minimum)
            {
                MessageBox.Show("No Negative Numbers");
                return false;
            }
            if (numStock.Value < numStock.Minimum)
            {
                MessageBox.Show("No Negative Numbers");
                return false;
            }



            return true;
         }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentRow.Index >= 0)
            {
                txtItemCode.Text = dataGridView1.CurrentRow.Cells["itemCode"].Value.ToString();
                txtItemName.Text = dataGridView1.CurrentRow.Cells["ItemName"].Value.ToString();
                txtCategory.Text = dataGridView1.CurrentRow.Cells["Category"].Value.ToString();
                numPrice.Value = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Price"].Value);
                numStock.Value = Convert.ToInt64(dataGridView1.CurrentRow.Cells["Stock"].Value);
                btnAdd.Enabled = false;
                btnDelete.Enabled = true;
                btnEdit.Enabled=true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text) || string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtCategory.Text) || numPrice.Value < 0 || numStock.Value < 0)
            {
                MessageBox.Show("Invalid input, please check your input (empty field, or negative numbers)");
            } else
            {
                if (ValidateInput() == true)
                {
                    try
                    {
                        items1TableAdapter1.InsertData(
                            txtItemCode.Text,
                            txtItemName.Text,
                            txtCategory.Text,
                            Convert.ToDecimal(numPrice.Value),
                            Convert.ToInt32(numStock.Value)
                         );
                        Clear();
                        MessageBox.Show("Data Added Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index < 0 && string.IsNullOrEmpty(txtItemCode.Text) || string.IsNullOrEmpty(txtItemName.Text) || string.IsNullOrEmpty(txtCategory.Text) || numPrice.Value < 0 || numStock.Value < 0)
            {
                MessageBox.Show("Invalid Action");
            }
            else
            {
                int itemsid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnSave.Enabled = true;
                if (ValidateInput() == true) 
                {
                    try
                    {
                        items1TableAdapter1.UpdateQuery(
                                txtItemCode.Text,
                                txtItemName.Text,
                                txtCategory.Text,
                                Convert.ToDecimal(numPrice.Value),
                                Convert.ToInt32(numStock.Value),
                                itemsid
                            );
                        Clear();
                        MessageBox.Show("Data Updated Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }

                }
                
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int itemsid = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
            try
            {
                items1TableAdapter1.DeleteQuery(
                        itemsid
                    );
                Clear();
                MessageBox.Show("Data Deleted Successfully");
            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
