﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Plugins
{
    public interface IPluginControl
    {
        PluginBase Plugin { get; set; }

        PluginView View { get; set; }

        PluginMode Mode { get; set; }

        void SaveForm();
    }
}
