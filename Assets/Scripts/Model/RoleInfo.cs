using App.Generic;
using System;

namespace App.Model
{
    [Serializable]
    public struct RoleInfo
    {
        public TableGroup group;
        public RoleType role; 
    }
}