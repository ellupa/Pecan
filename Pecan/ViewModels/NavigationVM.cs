using Pecan.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pecan.ViewModels
{
    public class NavigationVM : ViewModelBase
    {
        private object? _currentView;

        public object? CurrentView 
        { 
            get { return _currentView; } 
            set { _currentView= value; OnPropertyChanged(); }
        }

        public ICommand SalesCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand PurchasesCommand { get; set; }
        public ICommand SuppliersCommand { get; set; }

        private void Sales(object obj) => CurrentView = new SaleVM();
        private void Products(object obj) => CurrentView = new ProductVM();
        private void Purchases(object obj) => CurrentView = new PurchaseVM();
        private void Suppliers(object obj) => CurrentView = new SupplierVM();

        public NavigationVM()
        {
            SalesCommand = new RelayCommand(Sales);
            ProductsCommand = new RelayCommand(Products);
            PurchasesCommand= new RelayCommand(Purchases);
            SuppliersCommand= new RelayCommand(Suppliers);

            //Startup View
            CurrentView = new SaleVM();
        }

    }
}
