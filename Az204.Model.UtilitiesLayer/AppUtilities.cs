using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Az204.Model.UtilitiesLayer
{
    public class AppUtilities
    {
        public enum PersistenceTechnologies
        {
            [EnumMember]
            AZURE_COSMOS_DB,

            [EnumMember]
            AZURE_TABLE_STORAGE,

            [EnumMember]
            AZURE_BLOB_STORAGE
        }
    }
}
