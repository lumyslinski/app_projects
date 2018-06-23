using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WebAnalyzer.Concrete;
using WebAnalyzer.Contracts;
using WebAnalyzer.Core;

namespace WebAnalyzer.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AnalyzerViewModel : ViewModelBase
    {
        private ObservableCollection<AnalyzerResultModel> foundKeywords;
        private string status;
        private string typedUrl;
        private bool isNotProccessing;

        public bool IsNotProccessing
        {
            get { return isNotProccessing; }
            set { isNotProccessing = value; RaisePropertyChanged(); }
        }

        public string TypedUrl
        {
            get { return typedUrl; }
            set { typedUrl = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<AnalyzerResultModel> FoundKeywords
        {
            get { return foundKeywords; }
            set { foundKeywords = value; RaisePropertyChanged(); Status = string.Format("Found {0} keywords",foundKeywords.Count); }
        }

        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged() ;}
        }

        public RelayCommand AnalyzeUrlCommand { get; set; }
        
        /// <summary>
        /// Initializes a new instance of the AnalyzerViewModel class.
        /// </summary>
        public AnalyzerViewModel()
        {
            FoundKeywords = new ObservableCollection<AnalyzerResultModel>();
            Status = "Type url in order to find occurences of found keywords from meta tag of the page's content";
            AnalyzeUrlCommand = new RelayCommand(AnalyzeUrl);
            IsNotProccessing = true;
        }

        private void AnalyzeUrl()
        {
            if (AnalyzerHelper.IsValidUrl(TypedUrl))
            {
                SetStatus(false, "Processing...");
                var task = AnalyzerHelper.GetFoundKeywords(TypedUrl).ContinueWith((resultTask) =>
                {
                    Thread.Sleep(1000);
                    if (resultTask.Result != null && resultTask.IsCompleted)
                    {
                        if (!String.IsNullOrEmpty(resultTask.Result.Error))
                        {
                             SetStatus(false,"Error: " + resultTask.Result.Error);
                        }
                        else
                        {
                            InvokeOnDispatcher(new Action(() => { FoundKeywords = new ObservableCollection<AnalyzerResultModel>(resultTask.Result.FoundKeywords); }));
                        }
                    }
                }, TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled).ContinueWith( (t) => {
                    SetStatus(true);
                });
            }
            else
            {
                Status = "Type correct url with http:// or https://!";
            }
        }

        private void SetStatus(bool isNotProccessing,string statusText=null)
        {
            if(statusText != null)
             InvokeOnDispatcher(new Action( () => { Status = statusText; IsNotProccessing = isNotProccessing; }));
            else
             InvokeOnDispatcher(new Action( () => { IsNotProccessing = isNotProccessing; }));
        }

        private void InvokeOnDispatcher(Action action)
        {
            Dispatcher.CurrentDispatcher.Invoke(action, DispatcherPriority.Send);
        }
    }
}