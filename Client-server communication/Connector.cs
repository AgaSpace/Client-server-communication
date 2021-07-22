using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Net;
using TerrariaApi.Server;
using TShockAPI;

namespace CSConnection
{
    [ApiVersion(2, 1)]
    public class Connector : TerrariaPlugin
    {
        public Connector(Main game) : base(game) { base.Order = 1; }
        public override string Author => "Zoom L1";
        public override string Name => "Client-server communication API";

        public override void Initialize() => 
            ServerApi.Hooks.GamePostInitialize.Register(this, PostInitialize);

        public void PostInitialize(EventArgs nonArgs)
        {
            NetManager.Instance.Register<SCModule>();
        }
    }
}
