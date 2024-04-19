using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RenameTool_3
{
    public class Renameinfos
    {
        public string value1 { get; set; }
        public string value2 { get; set; }
        public string value3 { get; set; }  


        public Renameinfos(string value1, string value2, string value3)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
         
        }

        public override string ToString()
        {
            return $"Value1: {value1}, Value2: {value2}"; // oder return $"Value1: {value1}, Value2: {value2}, Value3: {value3}"; wenn Sie alle Werte anzeigen möchten
        }
    }
}
