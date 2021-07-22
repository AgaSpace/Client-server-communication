using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Terraria.Net;

namespace CSConnection
{
    public class SCModule : NetModule
    {
        public const byte Id = 11;

        public override bool Deserialize(BinaryReader reader, int userId)
        {
            CSApi.Deserialize(reader, userId);

            return false;
        }
    }
}
