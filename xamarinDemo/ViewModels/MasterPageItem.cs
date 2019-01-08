using System;
namespace xamarinDemo.ViewModels
{
    public class MasterPageItem: BaseModel
    {
        string title;
        public string Title { 
            get 
            { 
                return title; 
            } 
            set 
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(Title);
                }
            }
        }

        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }
}
