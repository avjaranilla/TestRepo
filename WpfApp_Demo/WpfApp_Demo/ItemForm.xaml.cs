using Entities.Interface;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WpfApp_Demo.MainWindow;

namespace WpfApp_Demo
{
    /// <summary>
    /// Interaction logic for ItemForm.xaml
    /// </summary>
    public partial class ItemForm : Window
    {
        public readonly IItemPropertyRepository itemPropertyRepository;
        public ItemForm(IItemPropertyRepository itemPropertyRepository)
        {
            this.itemPropertyRepository = itemPropertyRepository;
            InitializeComponent();
        }
        
        public void PopulateItemView()
        {
            int listID = Convert.ToInt32(txtListID.Text);

            itemPropertyRepository.GetItems(listID);
            var data = itemPropertyRepository.GetItems(listID);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ItemID");
            dataTable.Columns.Add("ItemName");
            dataTable.Columns.Add("ItemDesc");
            dataTable.Columns.Add("ItemStatus");

            foreach (var list in data.ToList())
            {
                var row = dataTable.NewRow();

                row["ItemID"] = list.ItemID;
                row["ItemName"] = list.ItemName;
                row["ItemDesc"] = list.ItemDesc;
                row["ItemStatus"] = ConvertStatusToString(list.ItemStatus);

                dataTable.Rows.Add(row);
            }
            lvwItems.ItemsSource = dataTable.AsDataView();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateItemView();
        }

        public void ClearItemInfoTextboxes()
        {
            txtItemID.Text = "";
            txtItemName.Text = "";
            txtItemDesc.Text = "";
        }

        public void FetchItemProperty(ItemProperty itemProperty)
        {

            string lid = txtListID.Text;
            int itemStatus = ConvertStatusToInt(cboStatus.Text);
            itemProperty.ListID = Int16.Parse(lid);
            itemProperty.ItemName = txtItemName.Text;
            itemProperty.ItemDesc = txtItemDesc.Text;
            itemProperty.ItemStatus = itemStatus;

            if (txtItemID.Text.Trim() == string.Empty)
            {
                itemProperty.ItemID = 0;
            }
            else
            {
                string id = txtItemID.Text.ToString();
                itemProperty.ItemID = Int16.Parse(id);
            }
        }

        //Convert ItemStatus to String (For Display)
        public string ConvertStatusToString(int itemStatus)
        {
            string result = "Unavailable";
            if (itemStatus == 1)
            {
                result = "Available";
            }

            return result;
        }

        public int ConvertStatusToInt(string itemStatus)
        {
            int result = 0;
            if (itemStatus == "Available")
            {
                result = 1;
            }
            return result;
        }


        public void InsertItem(ItemProperty itemProperty)
        {
            itemPropertyRepository.InsertItem(itemProperty);
        }

        public void UpdateItem(ItemProperty itemProperty)
        {
            itemPropertyRepository.UpdateItem(itemProperty);
        }
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            ClearItemInfoTextboxes();
            if (grpItem.Visibility != Visibility.Visible)
            {
                grpItem.Visibility = Visibility.Visible;
            }
            else
            {
                //Do Nothing
            }
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            if (grpItem.Visibility != Visibility.Visible)
            {
                grpItem.Visibility = Visibility.Visible;

            }
            else
            {
                //Do Nothing
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtItemName.Text == "" || txtItemDesc.Text == "" || cboStatus.Text == "")
            {
                MessageBox.Show("Please enter item Name, Description and Status.");
                return;
            }


            ItemProperty itemProperty = new ItemProperty();
            FetchItemProperty(itemProperty);
            if (itemProperty.ItemID == 0)
            {
                //Insert new record
                InsertItem(itemProperty);
                MessageBox.Show("New record added.");
            }
            else
            {
                //Update current record
                UpdateItem(itemProperty);
                MessageBox.Show("Updated record.");
            }
            PopulateItemView();
        }

        private void lvwItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvwItems.SelectedItem is not DataRowView row)
            {
                ClearItemInfoTextboxes();
            }
            else
            {
                string iid = row.Row.ItemArray[0].ToString();
                ItemProperty itemProperty = new ItemProperty
                {
                    ItemID = Int16.Parse(iid),
                    ItemName = row.Row.ItemArray[1].ToString(),
                    ItemDesc = row.Row.ItemArray[2].ToString(),
                };

                txtItemID.Text = itemProperty.ItemID.ToString();
                txtItemName.Text = itemProperty.ItemName.ToString();
                txtItemDesc.Text = itemProperty.ItemDesc.ToString();
                cboStatus.Text = row.Row.ItemArray[3].ToString();
            }
        }

        private void lvwItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            grpItem.Visibility = Visibility.Visible;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearItemInfoTextboxes();
            grpItem.Visibility = Visibility.Collapsed;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvwItems.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("No selected item.");
                return;
            }
            //Remove item attachment from list
            MessageBoxResult result = MessageBox.Show("Do you want to remove this item?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            string iid = row.Row.ItemArray[0].ToString();
            string lid = txtListID.Text;

            if (result == MessageBoxResult.Yes)
            {
                itemPropertyRepository.DeleteItemByID(Int16.Parse(iid), Int16.Parse(lid));
                PopulateItemView();
            }
            else
            {
                //MessageBox.Show("ignore");
                //Do Nothing
                return;
            }
        }
    }
}
