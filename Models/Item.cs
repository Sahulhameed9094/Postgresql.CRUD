﻿using System;
using System.Collections.Generic;

namespace Postgresql.CRUD.Models;

public partial class Item
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
