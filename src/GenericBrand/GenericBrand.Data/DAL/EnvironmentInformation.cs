using System;
using System.Collections.Generic;
using System.Text;

namespace GenericBrand.Data.DAL
{
    internal class EnvironmentInformation
    {
        public virtual string GetEnvironmentName()
        {
            return Environment.GetEnvironmentVariable("Hosting:Environment");
        }

        public virtual string GetBasePath()
        {
            return AppContext.BaseDirectory;
        }
    }
}
