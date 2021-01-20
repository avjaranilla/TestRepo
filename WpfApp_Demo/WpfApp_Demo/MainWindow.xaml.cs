using Entities.Interface;
using System.Data;
using System.Linq;
using System.Windows;
using Entities.Model;
using System;


namespace WpfApp_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly IListPropertyRepository listPropertyRepository;
        public readonly IItemPropertyRepository itemPropertyRepository;


        public MainWindow(IListPropertyRepository listPropertyRepository, IItemPropertyRepository itemPropertyRepository)
        {
            this.listPropertyRepository = listPropertyRepository;
            this.itemPropertyRepository = itemPropertyRepository;

            InitializeComponent();
            PopulateListview();
        }

        public void PopulateListview()
        {
            listPropertyRepository.GetLists();
            var data = listPropertyRepository.GetLists();

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ListID");
            dataTable.Columns.Add("ListName");
            dataTable.Columns.Add("ListDesc");

            foreach (var list in data.ToList())
            {
                var row = dataTable.NewRow();

                row["ListID"] = list.ListID;
                row["ListName"] = list.ListName;
                row["ListDesc"] = list.ListDesc;

                dataTable.Rows.Add(row);
            }
            lvwList.ItemsSource = dataTable.AsDataView();
        }

        //Fetch Property List from txtBoxes
        public void FetchListProperty(ListProperty listProperty)
        {
            listProperty.ListName = txtItemName.Text;
            listProperty.ListDesc = txtItemDesc.Text;

            if (txtItemID.Text.Trim() == string.Empty)
            {
                listProperty.ListID = 0;
            }
            else
            {
                string id = txtItemID.Text.ToString();
                listProperty.ListID = Int16.Parse(id);
            }
        }
        //Insert new List record
        public void InsertList(ListProperty listProperty)
        {
            listPropertyRepository.InsertList(listProperty);
        }

        //update selected List
        public void UpdateList(ListProperty listProperty)
        {
            listPropertyRepository.UpdateList(listProperty);
        }

        public void clear_itemInfo_txtboxes()
        {
            txtItemID.Text = "";
            txtItemName.Text = "";
            txtItemDesc.Text = "";
        }

        private void lvwList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvwList.SelectedItem is not DataRowView row) //clear textboxes if no selecteditem
            {
                clear_itemInfo_txtboxes();
            }
            else
            {
                string iid = row.Row.ItemArray[0].ToString();
                ListProperty listProperty = new ListProperty
                {
                    ListID = Int16.Parse(iid),
                    ListName = row.Row.ItemArray[1].ToString(),
                    ListDesc = row.Row.ItemArray[2].ToString()
                };
                
                txtItemID.Text = listProperty.ListID.ToString();
                txtItemName.Text = listProperty.ListName.ToString();
                txtItemDesc.Text = listProperty.ListDesc.ToString();
                //PopulateItemGrid(listProperty.ID);
            }
        }

        private void lvwList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            grpItem.Visibility = Visibility.Visible;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            clear_itemInfo_txtboxes();
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clear_itemInfo_txtboxes();
            grpItem.Visibility = Visibility.Collapsed;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ListProperty listProperty = new ListProperty();
            FetchListProperty(listProperty);

            if (listProperty.ListID == 0)
            {
                //Insert new record
                InsertList(listProperty);
                MessageBox.Show("New record added.");
            }
            else
            {
                //Update current record
                UpdateList(listProperty);
                MessageBox.Show("Updated record.");
            }
            PopulateListview();
        }

        private void btnManage_Click(object sender, RoutedEventArgs e)
        {
            if (txtItemID.Text == "")
            {
                MessageBox.Show("No selected item.");
                return;
            }
            string iid = txtItemID.Text;

            //Populate item form
            ItemForm itemForm = new ItemForm(itemPropertyRepository);
            itemForm.txtListID.Text = iid.ToString();
            itemForm.ShowDialog();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvwList.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("No selected item.");
                return;
            }
            //Remove item attachment from list
            MessageBoxResult result = MessageBox.Show("Do you want to remove this item?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            string lid = row.Row.ItemArray[0].ToString();

            if (result == MessageBoxResult.Yes)
            {
                itemPropertyRepository.DeleteItemByListID(Int16.Parse(lid));
                listPropertyRepository.DeleteList(Int16.Parse(lid));
                PopulateListview();
            }
            else
            {
                //Do Nothing
                return;
            }

        }
    }
}

