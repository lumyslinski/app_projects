using GalaSoft.MvvmLight;
using WebAnalyzer.Contracts;

namespace WebAnalyzer.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ModalBoxViewModel : ViewModelBase, IModalBox
    {
        /// <summary>
        /// Initializes a new instance of the ModalBoxViewModel class.
        /// </summary>
        public ModalBoxViewModel()
        {
        }

        public void ShowMessage(string message)
        {
           
        }

        public void Close()
        {
           
        }
    }
}