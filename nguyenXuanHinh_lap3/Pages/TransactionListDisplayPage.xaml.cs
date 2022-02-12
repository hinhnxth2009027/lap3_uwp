using nguyenXuanHinh_lap3.DB;
using nguyenXuanHinh_lap3.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nguyenXuanHinh_lap3.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TransactionListDisplayPage : Page
    {
        private DatabaseInitialize database = new DatabaseInitialize();
        private List<PersonalTransaction> listTransaction;
        private PersonalTransaction personal;
        private string checkedStartDate;
        private string checkedEndDate;

      
        public TransactionListDisplayPage()
        {
            this.InitializeComponent();
            this.Loaded += ListPersonalTransactionPageLoaded;
        }

        private void ListPersonalTransactionPageLoaded(object sender, RoutedEventArgs e)
        {
            ListData.ItemsSource = database.ListData();
        }


        private void GotoCreateNewTransaction(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.NewTransactionRecordPage));
        }

        private void FindByCategory(object sender, RoutedEventArgs e)
        {
            personal = null;
            List<PersonalTransaction> listTransactionByCategory = database.FindByCategory(Convert.ToInt32(searchCategory.Text));
            ListData.ItemsSource = listTransactionByCategory;
            btnTotalMoney.Text = database.totalMoney.ToString();
        }

        private void FindByStartDateAndEndDate(object sender, RoutedEventArgs e)
        {
            checkedStartDate = startDate.Date.ToString("yyyy-dd-MM");
            checkedEndDate = endDate.Date.ToString("yyyy-dd-MM");
            ListData.ItemsSource = database.FindByStartDateAndEndDate(checkedStartDate, checkedEndDate);
            btnTotalMoney.Text = database.totalMoney.ToString();
        }

    }
}
