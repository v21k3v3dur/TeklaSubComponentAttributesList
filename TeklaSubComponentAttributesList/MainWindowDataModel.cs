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
        public string ConnectionStatus { get; set; }
        public string SubCompName { get; set; }
        public int SubCompNumber { get; set; }
        public string SubCompSelectedAttr { get; set; }
    }
}
