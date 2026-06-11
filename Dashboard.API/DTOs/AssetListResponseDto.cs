using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.DTOs
{
    public class AssetListResponseDto
    {
        public int TotalRecords { get; set; }

        public int FilteredRecords { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public List<AssetListDto> Assets { get; set; } = [];
    }
}