﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
