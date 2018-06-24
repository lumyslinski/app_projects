//
// Autor:
//   Łukasz Myśliński, 109178
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lab2
{
    // The class that handles the creation of the application windows 
    class MyApplicationContext : ApplicationContext
    {
        private Out formOut;

        

        private MyApplicationContext()
        {
            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);



            try
            {
                CreateForms();
            }
            catch (OutOfMemoryException e)
            {
                MessageBox.Show("Nie ma wystarczającej ilości pamięci! \n" + e.StackTrace);
                ExitThread();
            }
            catch (InvalidDataException e)
            {
                MessageBox.Show("Format danych jest nieprawidłowy! \n"+e.StackTrace);
                ExitThread();
            }
            catch (Exception e)
            {
                // Inform the user that an error occurred.
                MessageBox.Show("Wystąpił błąd: " + e.ToString());

                // Exit the current thread instead of showing the windows.
                ExitThread();
            }

            // Show forms
            formOut.Show();
        }

        private void CreateForms()
        {
            formOut = new Out();
            formOut.Closed += new EventHandler(OnFormClosed);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {

            try
            {
                // Ignore any errors that might occur while closing the file handle.
                
            }
            catch { }
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            ExitThread();
        }

        [STAThread]
        static void Main(string[] args)
        {

            // Create the MyApplicationContext, that derives from ApplicationContext, 
            // that manages when the application should exit.

            MyApplicationContext context = new MyApplicationContext();

            // Run the application with the specific context. It will exit when 
            // all forms are closed.
            Application.Run(context);

        }
    }
}
