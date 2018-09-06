﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
   public interface IAddressService
    {
        int Create(address address);
        bool Delete(int id);
        List<address> GetAll();
        address GetByID(int id);
        bool Update(address address);
    }
}
