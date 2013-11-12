using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 
using Catcher.GameObjects;

namespace Catcher.FileStorageHelper
{
    
    [DataContract]
    public class GameRecordData
    {
        [DataMember]
        public int SavePeopleNumber { get; set; }

        List<DropObjectsKeyEnum> caughtObjects;

        [DataMember]
        public List<DropObjectsKeyEnum> CaughtDropObjects { get; set; }
    }
}
