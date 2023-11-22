using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;

namespace TeklaSubComponentAttributesList
{
    internal class MainWindowDataModel
    {
        public string ConnectionStatus { get; private set; }
        public string SubCompName { get; set; }
        public int SubCompNumber { get; set; }
        public List<string> SubCompAttributeFiles { get; set; } = new List<string>();





        public MainWindowDataModel()
        {
            ConnectionStatus=GetConnectionStatus();
        }
        private string GetConnectionStatus()
        {
            string output = "No Connection to Tekla!";
            Model model = new Model();
            var result= model.GetConnectionStatus();

            if (result)
            {
                output = "Connected to Tekla.";
            }

            return output;
        }
    }
}
