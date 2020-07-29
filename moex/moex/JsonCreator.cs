using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace moex
{
    public class JsonCreator
    {
        public string Create (StreamReader objReader)
        {
            string sLine = "";
            string sLineTotal = "";
            int j = 0;

            while (sLine != null)
            {
                j++;
                sLine = objReader.ReadLine();
                if (sLine != null)
                    sLineTotal = sLineTotal + sLine.Trim();
            }

            return sLineTotal;
        }
    }
}
