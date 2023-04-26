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
        private string? _title;

        public object? CurrentView 
        { 
            get { return _currentView; } 
            set { _currentView= value; OnPropertyChanged(); }
        }

        public string? Title
        {
            get { return _title; }
            set { _title= value; OnPropertyChanged();}
        }

        public ICommand SalesCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand PurchasesCommand { get; set; }
        public ICommand SuppliersCommand { get; set; }

        private void Sales(object obj)
        {
            Title = "Ventas";
            CurrentView = new SaleVM();
        }
        private void Products(object obj)
        {
            Title = "Produtos";
            CurrentView = new ProductVM();
        }
        private void Purchases(object obj)
        {
            Title = "Compras";
            CurrentView = new PurchaseVM();
        }
        private void Suppliers(object obj)
        {
            Title = "Proveedores";
            CurrentView = new SupplierVM();
        }

        public NavigationVM()
        {
            SalesCommand = new RelayCommand(Sales);
            ProductsCommand = new RelayCommand(Products);
            PurchasesCommand= new RelayCommand(Purchases);
            SuppliersCommand= new RelayCommand(Suppliers);

            //Startup View
            Title = "Ventas";
            CurrentView = new SaleVM();
        }

    }
}
