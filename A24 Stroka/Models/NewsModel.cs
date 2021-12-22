using A24_Stroka.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A24_Stroka.Models
{
    public class NewsModel : OnPropertyChangedClass
    {
        private int _iD;
        private string _text;
        private bool _in_Work;
        private int _order_By;
        private bool _view;
        private bool _isSelect = false;

        public int ID { get => _iD; set => SetProperty(ref _iD, value); }
        public string Text { get => _text; set => SetProperty(ref _text, value?.Trim()); }
        public bool In_Work { get => _in_Work; set => SetProperty(ref _in_Work, value); }
        public int Order_By { get => _order_By; set => SetProperty(ref _order_By, value); }
        public bool View { get => _view; set => SetProperty(ref _view, value); }

        public NewsModel() { }
        public bool IsSelected { get => _isSelect; set => SetProperty(ref _isSelect, value); }
    }
}
