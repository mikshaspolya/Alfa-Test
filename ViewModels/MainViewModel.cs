using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using TestWpfApp.Models;
using TestWpfApp.Utils;
using TestWpfApp.Utils.Exports;
using TestWpfApp.ViewModels.Commands;


namespace TestWpfApp.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        const string READ_FILE_NAME = "data.xml";
        const string EXPORT_FILE_NAME = "exportData";

        private ObservableCollection<Item> _items;
        private ObservableCollection<string> _categories;
        private ObservableCollection<string> _formats;
        private string _selectedCategory;
        private string _selectedFormat;

        private RelayCommand _readXmlDataModelCommand;
        private RelayCommand _readXmlRegexCommand;
        private RelayCommand _sortByDateCommand;
        private RelayCommand _exportCommand;

        private readonly IParser _dataModelParser = new DataModelXmlParser();
        private readonly IParser _regexParser = new RegexXmlParser();


        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ObservableCollection<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public ObservableCollection<string> Formats
        {
            get { return _formats; }
            set
            {
                _formats = value;
                OnPropertyChanged("Formats");
            }
        }

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");

                Items = FilterItems();
            }
        }

        public string SelectedFormat
        {
            get { return _selectedFormat; }
            set
            {
                _selectedFormat = value;
                OnPropertyChanged("SelectedFormat");

                ExportData();
            }
        }

        public async void ExportData()
        {
            IDataExporter exporter = null;
            switch (SelectedFormat)
            {
                case "Word":
                    exporter = new WordDataExporter();
                    break;
                case "Excel":
                    exporter = new ExcelDataExporter();
                    break;
                case "JSON":
                    exporter = new JsonDataExporter();
                    break;
                default:
                    break;

            }

            IEnumerable<Item> data = Items.Cast<Item>().ToList();

            await exporter.ExportDataAsync(data, EXPORT_FILE_NAME);
            MessageBox.Show("success");
 
        }


        public RelayCommand ReadXmlDataModelCommand
        {
            get
            {
                if (_readXmlDataModelCommand == null)
                {
                    _readXmlDataModelCommand = new RelayCommand(
                        async () =>
                        {
                            _items.Clear();

                            var items = await _dataModelParser.ParseAsync("data2.xml");
                            foreach (var item in items)
                            {
                                _items.Add(item);
                            }
                            UpdateCategories();
                        },
                        () => true
                    );
                }
                return _readXmlDataModelCommand;
            }
        }

        public RelayCommand ReadXmlRegexCommand
        {
            get
            {
                if (_readXmlRegexCommand == null)
                {
                    _readXmlRegexCommand = new RelayCommand(
                        async () =>
                        {
                            _items.Clear();

                            var items = await _regexParser.ParseAsync(READ_FILE_NAME);
                            foreach (var item in items)
                            {
                                Items.Add(item);
                            }
                            UpdateCategories();
                        },
                        () => true
                    );
                }
                return _readXmlRegexCommand;
            }
        }

        public RelayCommand SortByDateCommand
        {
            get
            {
                if (_sortByDateCommand == null)
                {
                    _sortByDateCommand = new RelayCommand(
                        () => SortItems(),
                        () => true
                    );
                }
                return _sortByDateCommand;
            }
        }

        public RelayCommand ExportCommand
        {
            get
            {
                if(_exportCommand == null)
                {
                    _exportCommand = new RelayCommand(() => { }, () => true);
                    return _exportCommand;
                }
                else
                {
                    IDataExporter exporter = null;
                    switch (SelectedFormat)
                    {
                        case "Word":
                            exporter = new WordDataExporter();
                            break;
                        case "Excel":
                            exporter = new ExcelDataExporter();
                            break;
                        case "JSON":
                            exporter = new JsonDataExporter();
                            break;
                        default:
                            break;

                    }

                    IEnumerable<Item> data = Items.OfType<Item>();
                    if (_exportCommand != null)
                    {
                        _exportCommand = new RelayCommand(
                            async () =>
                            {
                                await exporter.ExportDataAsync(data, EXPORT_FILE_NAME);
                            },
                            () => true
                        );

                    }
                }

                return _exportCommand;
            }
        }


        private void UpdateCategories()
        {
            _categories.Clear();
            Categories.Clear();
            Categories = new ObservableCollection<string>(Items.Select(x => x.Category).Distinct());
            OnPropertyChanged("Categories");
        }

        public ObservableCollection<Item> FilterItems()
        {
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(Items);
                view.Filter = item =>
                {
                    return ((Item)item).Category == _selectedCategory;
                };
            }
            return Items;
        }

        private void SortItems()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Items);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("PubDate", ListSortDirection.Ascending));
        }

        public MainViewModel()
        {
            _items = new ObservableCollection<Item>();
            _categories = new ObservableCollection<string>();
            _formats = new ObservableCollection<string>() { "JSON", "Word", "Excel" };
            _selectedCategory = "";
            _selectedFormat = "";

            Items.CollectionChanged += OnItemsCollectionChanged;
        }

        private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(Items);
                view.Filter = item => true;
                view.SortDescriptions.Clear();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //_exportCommand.RaiseCanExecuteChanged();
        }

    }
}