using System;

namespace Gestor_API.Repository
{
    public class Valida
    {

        //private static string ConverterData(DateTime dt)
        //{
        //    DateTime dat = Convert.ToDateTime(dt);
        //    var dt_data = dat.ToString("yyyy-MM-dd");
        //    if (dt_data == "0001-01-01")
        //    {
        //        dt_data = null;
        //    }
        //    return dt_data;
        //}

        public string ConverterData(DateTime dt)
        {
            DateTime dat = Convert.ToDateTime(dt);
            var dt_data = dat.ToString("yyyy-MM-dd");
            if (dt_data == "0001-01-01")
            {
                dt_data = null;
            }
            return dt_data;
        }
    }
}
