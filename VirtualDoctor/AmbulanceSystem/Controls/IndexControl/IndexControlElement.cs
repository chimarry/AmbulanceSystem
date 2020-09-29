﻿using System;
using System.Collections.Generic;

namespace AmbulanceSystem.Controls.IndexControl
{
    public abstract class IndexControlElement
    {
        public abstract void Create();

        public abstract void Delete(object selectedItem);

        public abstract void Edit(object selectedItem);

        public virtual void Details(object selectedItem)
        {
            throw new NotImplementedException();
        }
    }
}
