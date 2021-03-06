using System.Collections.Generic;
using UnityEngine;

namespace ProxyOfUs.Roles
{
    public class Swapper : Role
    {

        public readonly List<bool> ListOfActives = new List<bool>();
        public readonly List<GameObject> Buttons = new List<GameObject>();


        public Swapper(PlayerControl player) : base(player)
        {
            Name = "Swapper";
            ImpostorText = () => "Swap the votes of two people";
            TaskText = () => "Swap two people's votes and wreak havoc!";
            Color = new Color(0.4f, 0.9f, 0.4f, 1f);
            RoleType = RoleEnum.Swapper;
        }
    }
}