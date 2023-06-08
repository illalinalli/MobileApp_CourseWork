using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplication.Models
{
    public class UserDetails
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SquareCount { get; set; }
        public int EqualTriangleCount { get; set; }
        public int ParallelogramCount { get; set; }
        public int TrapeziumCount { get; set; }
        public int RentagonCount { get; set; }
        public int IsoscelesTriangleCount { get; set; }
        public int HexagonCount { get; set; }
        public int DoubleSquareCount { get; set; }
    }
}
