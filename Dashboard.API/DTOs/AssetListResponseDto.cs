using System;
using System.Collections.Generic;

namespace Dashboard.API.DTOs
{
    public class AssetListResponseDto
    {
        public int TotalRecords { get; set; }

        public int FilteredRecords { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public List<AssetListDto> Items { get; set; } = [];
    }
}