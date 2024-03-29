﻿using Qualification.Domain.Configurations;

namespace Qualification.Service.DTOs;

public class Filter
{
    public string Property { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string OrderBy { get; set; }
    public string OrderType { get; set; }
}

public class Filters : List<Filter>
{
}

public class Grid
{
    public PaginationParams Pagination { get; set; }
    public Filters Filters { get; set; }
}