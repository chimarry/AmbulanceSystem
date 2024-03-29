﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace AmbulanceSystem
{
    public abstract class ComboBoxDataSource<T> : INotifyPropertyChanged
    {
        protected readonly int? idCurrentModel;

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<T> selectedItems = new ObservableCollection<T>();
        private string selectedItemsText = string.Empty;

        public ObservableCollection<T> Items { get; set; }

        public ComboBoxDataSource(int? idCurrentModel = null)
        {
            this.idCurrentModel = idCurrentModel;
            InitializeItems();
        }

        protected abstract void InitializeItems();

        public ObservableCollection<T> SelectedItems
        {
            get
            {
                SelectedItemsText = WriteSelectedString(selectedItems);
                selectedItems.CollectionChanged +=
                    (s, e) =>
                    {
                        SelectedItemsText = WriteSelectedString(selectedItems);
                        OnPropertyChanged("SelectedItems");
                    };

                return selectedItems;
            }
            set
            {
                selectedItems = value;
            }
        }

        public string SelectedItemsText
        {
            get { return selectedItemsText; }
            set
            {
                selectedItemsText = value;
                OnPropertyChanged("SelectedItemsText");
            }
        }

        private static string WriteSelectedString(IList<T> list)
        {
            if (list.Count == 0)
                return String.Empty;

            StringBuilder builder = new StringBuilder(list[0].ToString());

            for (int i = 1; i < list.Count; i++)
            {
                builder.Append(", ");
                builder.Append(list[i].ToString());
            }

            return builder.ToString();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

