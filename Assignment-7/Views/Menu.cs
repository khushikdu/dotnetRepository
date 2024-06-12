using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_7.Views
{
    internal class Menu
    {
        public void ShowMainScreenOptions()
        {
            Console.WriteLine("\nAzure Blob Storage Operations");
            Console.WriteLine("1. Create Container");
            Console.WriteLine("2. Set Access policy to public");
            Console.WriteLine("3. Upload Blob");
            Console.WriteLine("4. List Blobs");
            Console.WriteLine("5. Download Blob");
            Console.WriteLine("6. Clean Up");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");
        }
    }
}
